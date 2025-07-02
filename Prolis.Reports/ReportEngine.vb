Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Threading.Tasks
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

''' <summary>
''' An instance-based, thread-safe engine for generating reports.
''' Create an instance of this class with the required connection info,
''' then reuse the instance for all report generation tasks using that connection.
''' </summary>
Public Class ReportEngine
    Private ReadOnly _connInfo As ConnInfo

#Region "Constructor"

    ''' <summary>
    ''' Initializes a new instance of the ReportEngine with specific connection details.
    ''' </summary>
    ''' <param name="connInfo">The database connection information to be used by this engine instance.</param>
    ''' <exception cref="ArgumentNullException">Thrown if connInfo is null.</exception>
    Public Sub New(ByVal connInfo As ConnInfo)
        If connInfo Is Nothing Then
            Throw New ArgumentNullException(NameOf(connInfo), "The connection information object cannot be null.")
        End If
        _connInfo = connInfo
    End Sub

#End Region

#Region "Public Data Structures & Result Objects"

    ''' <summary>
    ''' Contains all necessary connection parameters for database and report operations.
    ''' Declared as a Class so it can be checked for Nothing.
    ''' </summary>
    Public Class ConnInfo
        Public Property SqlCS As String
        Public Property ServerName As String
        Public Property DbName As String
        Public Property DbUser As String
        Public Property PWD As String
        Public Property UID As String
        Public Property GenericResults As String
        Public Property CustomResults As String
    End Class

    Public Class ReportGenerationResult
        Public Property IsSuccess As Boolean
        Public Property Message As String
        Public ReadOnly Property FailedAccessionIDs As New List(Of String)
    End Class

#End Region

#Region "Public Report Generation Method"

    ''' <summary>
    ''' Asynchronously generates a consolidated PDF report.
    ''' The connection information provided in the constructor will be used.
    ''' </summary>
    Public Async Function GenerateReportsAsync(
        ByVal accIDs() As String,
        isCustomRPT As Boolean
    ) As Task(Of ReportGenerationResult)

        ' Implementation of this method is correct and does not need to change.
        ' It will now work with the Class-based ConnInfo.
        If accIDs Is Nothing OrElse accIDs.Length = 0 Then
            Throw New ArgumentNullException(NameOf(accIDs), "Accession ID array cannot be null or empty.")
        End If

        Dim result As New ReportGenerationResult()
        Dim allPdfParts As New List(Of Byte())

        For Each accID As String In accIDs
            If String.IsNullOrWhiteSpace(accID) Then Continue For
            Try
                Await ExecuteStoredProcedureAsync("usp_Acc_Result", New SqlParameter("@AccID", accID))

                Dim rptFile As String = Path.Combine(Application.StartupPath, "Reports", ValidateReportFile(accID, isCustomRPT))
                If Not File.Exists(rptFile) Then
                    Throw New FileNotFoundException($"The required report file was not found.", rptFile)
                End If

                Using oRpt As New ReportDocument()
                    oRpt.Load(rptFile, OpenReportMethod.OpenReportByTempCopy)
                    ApplyNewServer(oRpt, _connInfo.ServerName, _connInfo.DbName, _connInfo.DbUser, _connInfo.PWD)
                    oRpt.SummaryInfo.ReportTitle = Await GetFlagInfoAsync(accID)
                    oRpt.RecordSelectionFormula = $"{{Requisitions.ID}} = {accID}"

                    Using ms As New MemoryStream()
                        oRpt.ExportToStream(ExportFormatType.PortableDocFormat).CopyTo(ms)
                        allPdfParts.Add(ms.ToArray())
                    End Using
                End Using

                Dim extendedPdfs = Await GetExtendedResultsAsync(accID)
                allPdfParts.AddRange(extendedPdfs)

            Catch ex As Exception
                result.FailedAccessionIDs.Add(accID)
            End Try
        Next

        If allPdfParts.Count = 0 Then
            result.IsSuccess = False
            result.Message = "No reports could be generated. Check logs for details."
            Return result
        End If

        Dim finalPdfBytes As Byte() = If(allPdfParts.Count > 1, PdfHelper.MergePDFStreams(allPdfParts), allPdfParts(0))
        Dim tempFolderPath As String = GetTempFolder(_connInfo.UID)
        Dim tempPdfPath As String = Path.Combine(tempFolderPath, $"Report_{Guid.NewGuid()}.pdf")

        Try
            File.WriteAllBytes(tempPdfPath, finalPdfBytes)

            'If String.IsNullOrWhiteSpace(saveToPath) Then
            '    Throw New ArgumentException("A valid 'saveToPath' must be provided when device is 6.", NameOf(saveToPath))
            'End If
            'File.Copy(tempPdfPath, $"{saveToPath}.pdf", True)

            result.IsSuccess = True
            result.Message = "Report processing completed."
            If result.FailedAccessionIDs.Count > 0 Then
                result.Message &= $" However, {result.FailedAccessionIDs.Count} accession(s) failed."
            End If

        Catch ex As Exception
            result.IsSuccess = False
            result.Message = $"A critical error occurred during the final save/print step: {ex.Message}"
        Finally
            If File.Exists(tempPdfPath) Then
                File.Delete(tempPdfPath)
            End If
        End Try

        Return result
    End Function

#End Region

#Region "Internal Helpers (Crystal, DB, File)"

    ' --- This region contains the internal helper methods ---
    ' They are largely unchanged from the previous version, but now they throw exceptions
    ' instead of showing message boxes.

    Private Sub ApplyNewServer(ByVal report As ReportDocument, ByVal serverName As String, ByVal databaseName As String, ByVal userName As String, ByVal password As String)
        If String.IsNullOrWhiteSpace(serverName) Then Return
        Dim connInfo As New ConnectionInfo With {.ServerName = serverName, .DatabaseName = databaseName, .UserID = userName, .Password = password, .IntegratedSecurity = String.IsNullOrWhiteSpace(userName)}
        ApplyLogonInfoToTables(report.Database.Tables, connInfo)
        For Each subReport As ReportDocument In report.Subreports
            ApplyLogonInfoToTables(subReport.Database.Tables, connInfo)
        Next
    End Sub

    Private Sub ApplyLogonInfoToTables(ByVal tables As Tables, ByVal connectionInfo As ConnectionInfo)
        For Each crTable As Table In tables
            Dim tableLogOnInfo As TableLogOnInfo = crTable.LogOnInfo
            tableLogOnInfo.ConnectionInfo = connectionInfo
            crTable.ApplyLogOnInfo(tableLogOnInfo)
            If Not String.IsNullOrEmpty(connectionInfo.DatabaseName) AndAlso crTable.Location IsNot Nothing Then
                Dim lastDotIndex = crTable.Location.LastIndexOf("."c)
                If lastDotIndex > -1 Then
                    Dim tableName = crTable.Location.Substring(lastDotIndex + 1)
                    crTable.Location = $"{connectionInfo.DatabaseName}.dbo.{tableName}"
                End If
            End If
        Next
    End Sub

    Private Async Function ExecuteStoredProcedureAsync(procedureName As String, ParamArray parameters As SqlParameter()) As Task
        Using cnx As New SqlConnection(_connInfo.SqlCS)
            Using cmd As New SqlCommand(procedureName, cnx)
                cmd.CommandType = CommandType.StoredProcedure
                If parameters IsNot Nothing Then cmd.Parameters.AddRange(parameters)
                Await cnx.OpenAsync()
                Await cmd.ExecuteNonQueryAsync()
            End Using
        End Using
    End Function

    Private Async Function GetFlagInfoAsync(ByVal accID As String) As Task(Of String)
        Dim query As String = "
            SELECT STRING_AGG(Flag_Abbr, ', ') FROM (
                SELECT DISTINCT CASE LOWER(LTRIM(RTRIM(Flag)))
                    WHEN 'critical low' THEN 'CL = Critical Low' WHEN 'critical high' THEN 'CH = Critical High'
                    WHEN 'critical abnormal' THEN 'CA = Critical Abnormal' WHEN 'abnormal' THEN 'A = Abnormal'
                    WHEN 'low' THEN 'L = Low' WHEN 'l' THEN 'L = Low' WHEN 'high' THEN 'H = High' WHEN 'h' THEN 'H = High'
                    ELSE LEFT(LTRIM(RTRIM(Flag)), 2)
                END AS Flag_Abbr
                FROM Acc_Results 
                WHERE Accession_ID = @AccID AND Flag IS NOT NULL AND LEN(LTRIM(RTRIM(Flag))) > 0 AND Behavior <> 'Ignore'
            ) AS Flags"
        Using cnx As New SqlConnection(_connInfo.SqlCS)
            Using cmd As New SqlCommand(query, cnx)
                cmd.Parameters.AddWithValue("@AccID", accID)
                Await cnx.OpenAsync()
                Dim result = Await cmd.ExecuteScalarAsync()
                Return If(result IsNot DBNull.Value, result?.ToString(), String.Empty)
            End Using
        End Using
    End Function

    Private Async Function GetExtendedResultsAsync(accID As String) As Task(Of List(Of Byte()))
        Dim results As New List(Of Byte())
        Using cnx As New SqlConnection(_connInfo.SqlCS)
            Using cmd As New SqlCommand("SELECT Result FROM Extend_Results WHERE Accession_ID = @AccID", cnx)
                cmd.Parameters.AddWithValue("@AccID", accID)
                Await cnx.OpenAsync()
                Using reader = Await cmd.ExecuteReaderAsync()
                    While Await reader.ReadAsync()
                        If Not Await reader.IsDBNullAsync(0) Then results.Add(CType(reader("Result"), Byte()))
                    End While
                End Using
            End Using
        End Using
        Return results
    End Function

    ''' <exception cref="InvalidOperationException">Thrown if the temporary folder cannot be created or accessed.</exception>
    Public Shared Function GetTempFolder(ByVal userID As String) As String
        ' Use AppContext.BaseDirectory for library-safe path resolution.
        Dim folderPath As String = Path.Combine(AppContext.BaseDirectory, "Temp", userID)
        Try
            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If
            For Each fileToDelete As String In Directory.GetFiles(folderPath)
                File.Delete(fileToDelete)
            Next
            Return folderPath
        Catch ex As Exception When TypeOf ex Is IOException OrElse TypeOf ex Is UnauthorizedAccessException
            Throw New InvalidOperationException($"Could not access or create temp folder at '{folderPath}'. See inner exception for details.", ex)
        End Try
    End Function

    Public Function ValidateReportFile(ByVal AccID As String,
     ByVal IsCustomRPT As Boolean) As String
        Dim RPT As String = ""
        Dim sSQL As String = "Select ResRPTFile from Providers where ID in (Select " &
        "OrderingProvider_ID from Requisitions where ID = " & Val(AccID) & ")"
        Dim cnvrf As New SqlConnection(_connInfo.SqlCS)
        cnvrf.Open()
        Dim cmdvrf As New SqlCommand(sSQL, cnvrf)
        cmdvrf.CommandType = CommandType.Text
        Dim drvrf As SqlDataReader = cmdvrf.ExecuteReader
        If drvrf.HasRows Then
            While drvrf.Read
                If drvrf("ResRPTFile") IsNot DBNull.Value _
                 AndAlso Trim(drvrf("ResRPTFile")) <> "" Then _
                 RPT = Trim(drvrf("ResRPTFile"))
            End While
        End If
        cnvrf.Close()
        cnvrf = Nothing
        '
        If RPT = "" Then
            If IsCustomRPT = False Then 'Generic
                If _connInfo.GenericResults <> "" Then
                    RPT = _connInfo.GenericResults
                Else
                    RPT = "AccRes_Navy.RPT"
                End If
            Else    'Custom
                If _connInfo.CustomResults <> "" Then
                    RPT = _connInfo.CustomResults
                Else
                    RPT = "AccRes_Navy.RPT"
                End If
            End If
        End If
        '
        Return RPT

        'If InStr(RPT, "\") > 0 Then   'Full path
        '    RPT = My.Application.Info.DirectoryPath & "\Reports\" &
        '    Microsoft.VisualBasic.Mid(RPT, InStrRev(RPT, "\") + 1)
        'Else
        '    RPT = My.Application.Info.DirectoryPath & "\Reports\" & RPT
        'End If
        ''
        'If Not IO.File.Exists(RPT) Then _
        'RPT = My.Application.Info.DirectoryPath & "\Reports\AccRes_Navy.RPT"
        'Return RPT
    End Function


#End Region

End Class


''' <summary>
''' Dummy helper class to allow the main code to compile.
''' You MUST replace this with your actual PDF merging and printing implementation using a library
''' like PdfSharp, iText, or by invoking an external process like Adobe Reader.
''' </summary>
'Public Class PdfHelper
'    Public Shared Function MergePDFStreams(streams As List(Of Byte())) As Byte()
'        If streams Is Nothing OrElse streams.Count = 0 Then Return Array.Empty(Of Byte)()
'        ' TODO: Implement actual PDF merging logic. For now, just returning the first stream.
'        Return streams(0)
'    End Function

'    Public Shared Function PrintPdfDocument(pdfPath As String, printerName As String, ByRef errorMessage As String, Optional copies As Integer = 1) As Boolean
'        ' TODO: Implement actual PDF printing logic.
'        errorMessage = "Printing is not implemented in the placeholder PdfHelper."
'        Return False ' Return true on success
'    End Function
'End Class