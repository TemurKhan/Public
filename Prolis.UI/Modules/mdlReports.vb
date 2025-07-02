Imports System.ComponentModel
Imports System.IO
Imports System.Security.AccessControl
Imports System.Xml
Imports DYMO
Imports Microsoft.Data.SqlClient
Imports Newtonsoft.Json
Imports Prolis.Reporting
Imports Prolis.Utils
Imports Syncfusion
Imports DataTable = System.Data.DataTable


Module mdlReports

    Public Class ConnInfo
        Public Property SqlCS As String
        Public Property ServerName As String
        Public Property DSN As String = "ProlisQC"
        Public Property DbName As String
        Public Property DbUser As String
        Public Property PWD As String
        Public Property UID As String
        Public Property GenericResults As String
        Public Property CustomResults As String
        Public Property accids As String()
        Public Property ReportPath As String
        Public Property IsCustomRPT As Boolean
    End Class
    'TODO: Dymo Code
    'Public DymoAddIn As DYMO.DymoAddIn
    'Public DymoLabel As DYMO.DymoLabels

    'Public Sub SetGlobalSettings()
    '    GlobalSettings.sqlCS = connString
    '    GlobalSettings.ServerName = My.Settings.ProlisServer
    '    GlobalSettings.DbName = My.Settings.Database
    '    GlobalSettings.DbUser = CryptoHelper.Decrypt(My.Settings.UID)
    '    GlobalSettings.PWD = CryptoHelper.Decrypt(My.Settings.PWD)
    '    ' GlobalSettings.UID = CryptoHelper.Decrypt(My.Settings.UID)
    'End Sub
    Public Async Sub GenerateReports(ByVal AccIDs() As String, ByVal Device As Integer, IsCustomRPT As Boolean, Optional SaveTo As String = "", Optional PrintQty As Integer = 1)

        If AccIDs Is Nothing OrElse AccIDs.Length = 0 OrElse String.IsNullOrEmpty(AccIDs(0)) Then Return

        LoggerHelper.LogInfo("Starting GenerateReports")


        ' Create an instance of ConnInfo with connection details
        'Dim conn As New ReportEngine.ConnInfo
        'conn.sqlCS = connString
        'conn.ServerName = ""
        'conn.DbName = "SampleDB"
        'conn.UID = "admin"
        'conn.PWD = "password123"

        '' Pass the structure values to ReportEngine through ConnInfoHandler
        'ReportEngine.SetConnInfo(conn)
        'ReportEngine.GenerateReports(AccIDs, Device, IsCustomRPT, SaveTo, PrintQty)

        ' Dim myReportEngine As ReportEngine

        Dim builder As New SqlConnectionStringBuilder(connString)

        Dim userId As String = builder.UserID
        Dim password As String = builder.Password


        ' 1. Create the connection information object.
        Dim connInfo As New ConnInfo With {
            .SqlCS = connString,
            .ServerName = My.Settings.ProlisServer,
            .DbName = My.Settings.Database,
            .DbUser = userId,
            .PWD = password,
            .UID = ThisUser.ID,
            .accids = AccIDs,
            .ReportPath = Environment.CurrentDirectory(),
            .IsCustomRPT = IsCustomRPT
        }

        '' 2. Create the instance of the engine.
        '' Use a Try...Catch block in case the connInfo is invalid, though we know it's not here.
        'Try
        '    myReportEngine = New ReportEngine(connInfo)
        'Catch ex As ArgumentNullException
        '    MessageBox.Show("Could not initialize the report engine: " & ex.Message, "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    ' You might want to disable the report button here
        '    'btnGenerateReport.Enabled = False
        'End Try

        ' 3. Later, whenever you need to generate a report...
        ' The list of IDs you want to report on
        ' Dim accessionsToReport() As String = AccIDs() ' "9999" might be a failing one for testing

        Try
            ' 4. Call the async method and AWAIT the result.
            '    This is a non-blocking call. The UI will remain responsive.

            Dim jsn As JsonSerializer = New JsonSerializer()
            Dim tempFilePath As String = Path.Combine(Path.GetTempPath(), "report_input.json")

            Dim srj = JsonConvert.SerializeObject(connInfo)
            Using TextWrite As New StreamWriter(tempFilePath)
                TextWrite.Write(srj)
            End Using



            Dim startInfo As New ProcessStartInfo()

            startInfo.FileName = Path.Combine("C:\Users\hp\Downloads\Prolis.Net\Prolis.Net\Prolis.ReportingHost\bin\Debug", "Prolis.ReportingHost.exe")
            startInfo.Arguments = tempFilePath
            startInfo.UseShellExecute = False
            startInfo.CreateNoWindow = True
            startInfo.RedirectStandardOutput = True

            Dim process1 = Process.Start(startInfo)
            Dim output As String = process1.StandardOutput.ReadToEnd()

            process1.WaitForExit()
            Dim hostResult = JsonConvert.DeserializeObject(Of HostResult)(output.Split(New String() {".json"}, StringSplitOptions.None)(1))
            Try


            Catch ex As Exception

            End Try


            Dim procs = Process.GetProcessesByName("Prolis.ReportingHost.exe")

            For Each pr In procs
                pr.Kill()
            Next
            Dim f As New frmWebView()
            f.ShowFromPath(hostResult.ReportPath)
            f.MdiParent = frmDashboard
            f.Show()
            Return
            'Dim result As ReportEngine.ReportGenerationResult =
            '    Await myReportEngine.GenerateReportsAsync(AccIDs,
            '                                               isCustomRPT:=False)

            '' 5. Check the result object returned by the library.
            'If result.IsSuccess Then
            '    MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'Else
            '    ' Build a detailed error message
            '    Dim errorMessage As String = result.Message
            '    If result.FailedAccessionIDs.Count > 0 Then
            '        errorMessage &= vbCrLf & "Failed IDs: " & String.Join(", ", result.FailedAccessionIDs)
            '    End If
            '    MessageBox.Show(errorMessage, "Report Generation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            'End If

        Catch ex As Exception
            ' 6. Catch any unexpected exceptions thrown by the library (e.g., file not found, DB connection failed)
            MessageBox.Show("A critical error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Re-enable the button and restore the cursor regardless of success or failure
            'btnGenerateReport.Enabled = True
            'Me.Cursor = Cursors.Default
        End Try


        Try

            Dim SPDFS As New List(Of Byte())
            Dim ExtCount As Integer = 0

            For Each AccID As String In AccIDs
                If String.IsNullOrEmpty(AccID) Then Continue For

                ExecuteSqlProcedure("Exec usp_Acc_Result '" & AccID & "' ")
                LoggerHelper.LogInfo("Exec usp_Acc_Result")

                Dim RPTFile As String = ValidateReportFile(AccID, IsCustomRPT)
                LoggerHelper.LogInfo("RptFileName: " + RPTFile)

                'TODO: Crystal Report
                '==================================
                'Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument

                'Try
                '    oRpt.Load(RPTFile, CrystalDecisions.Shared.OpenReportMethod.OpenReportByTempCopy)
                '    ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)
                '    oRpt.SummaryInfo.ReportTitle = GetFlagInfo(AccID)
                '    oRpt.RecordSelectionFormula = "{Requisitions.ID} = " & AccID & " AND {Acc_Results.Result} <> '.'"

                '    Using ms As New MemoryStream()
                '        oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat).CopyTo(ms)
                '        SPDFS.Add(ms.ToArray())
                '    End Using

                '    Using cner As New SqlClient.SqlConnection(connString)
                '        cner.Open()
                '        Using cmder As New SqlClient.SqlCommand("SELECT Result FROM Extend_Results WHERE Accession_ID = " & AccID, cner)
                '            Using drer As SqlClient.SqlDataReader = cmder.ExecuteReader()
                '                While drer.Read()
                '                    SPDFS.Add(CType(drer("Result"), Byte()))
                '                    ExtCount += 1
                '                End While
                '            End Using
                '        End Using
                '    End Using
                'Catch ex As Exception
                '    MessageBox.Show(ex.Message)
                '    LoggerHelper.LogError(ex, "Error GenerateReports")
                'Finally
                '    oRpt.Close()
                'End Try

                '==================================
                If Device = 1 Then Continue For

                Dim FPDF As Byte() = PdfHelper.MergePDFStreams(SPDFS)
                Dim FolderPath As String = GetTempFolder()

                For Each FlToDel As String In Directory.GetFiles(FolderPath, "*.*", SearchOption.AllDirectories)
                    File.Delete(FlToDel)
                Next

                Dim pdfFilePath As String = Path.Combine(FolderPath, AccID & ".PDF")

                LoggerHelper.LogInfo("pdf File Path: " + pdfFilePath)

                File.WriteAllBytes(pdfFilePath, FPDF)

                Select Case Device
                    Case 0
                        'Dim PS As New System.Drawing.Printing.PrinterSettings
                        'Using document As PdfDocument = PdfDocument.Load(pdfFilePath)
                        '    Using printDoc = document.CreatePrintDocument()
                        '        printDoc.PrinterSettings.PrinterName = PS.PrinterName
                        '        For index = 1 To PrintQty
                        '            printDoc.Print()
                        '        Next
                        '    End Using
                        'End Using

                        Dim printerName As String = "" ' Optional: Specify printer name
                        Dim errorMessage As String = ""

                        Dim success As Boolean = PdfHelper.PrintPdfDocument(pdfFilePath, printerName, errorMessage)

                        If success = False Then
                            MessageBox.Show("Error printing PDF: " & errorMessage)

                            LoggerHelper.LogInfo(errorMessage, "Error printing PDF")

                        End If

                        My.Application.DoEvents()
                    Case 6
                        Try
                            File.Copy(pdfFilePath, SaveTo & ".PDF", True)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                            LoggerHelper.LogError(ex, "Error GenerateReports")
                        End Try
                End Select
            Next

            If Device = 1 Then
                Dim FPDF As Byte() = PdfHelper.MergePDFStreams(SPDFS)
                Dim f As New frmWebView()
                f.ShowFromStream(FPDF)
                f.MdiParent = frmDashboard
                f.Show()
            End If
        Catch ex As Exception
            LoggerHelper.LogError(ex, "Error GenerateReports")
        Finally
            LoggerHelper.LogInfo("Exit from GenerateReports")
        End Try
    End Sub
    Public Class HostResult
        Public Property Success As Boolean
        Public Property Message As String
        Public Property ReportPath As String
    End Class

    Public Function GetReportConfigs(ByVal AccID As Long) As String()
        Dim Configs() As String = {String.Empty}
        '"", "", "", "", "", "", "", "", "", ""
        '0=ProviderID, 1=Complete, 2=Print, 3=Prolison, 4=Interface, 5=RPTFax
        '6=Fax#, 7=RPTEmail, 8=Email, 9=RPTFile
        Dim cnrc As New SqlClient.SqlConnection(connString)
        cnrc.Open()
        Dim cmdrc As New SqlClient.SqlCommand("Select a.*, b.ResRPTFile from Req_RPT a " &
        "inner join Providers b on a.Provider_ID = b.ID where a.Base_ID = " & AccID, cnrc)
        cmdrc.CommandType = CommandType.Text
        Dim drrc As SqlClient.SqlDataReader = cmdrc.ExecuteReader
        If drrc.HasRows Then
            While drrc.Read
                If Configs(UBound(Configs)) <> String.Empty Then ReDim Preserve Configs(UBound(Configs) + 1)
                Configs(UBound(Configs)) = drrc("Provider_ID").ToString &
                "|" & Convert.ToInt32(drrc("RPT_Complete")) & "|" &
                Convert.ToInt32(drrc("RPT_Print")) & "|" &
                Convert.ToInt32(drrc("RPT_ProlisOn")) & "|" &
                Convert.ToInt32(drrc("RPT_Interface")) & "|" &
                Convert.ToInt32(drrc("RPT_Fax")) & "|"
                If drrc("Fax") IsNot DBNull.Value AndAlso Trim(drrc("Fax")) <> String.Empty Then
                    Configs(UBound(Configs)) += drrc("Fax") & "|"
                Else
                    Configs(UBound(Configs)) += "|"
                End If
                Configs(UBound(Configs)) += Convert.ToInt32(drrc("RPT_Email")) & "|"
                If drrc("Email") IsNot DBNull.Value AndAlso Trim(drrc("Email")) <> String.Empty Then
                    Configs(UBound(Configs)) += drrc("Email") & "|"
                Else
                    Configs(UBound(Configs)) += "|"
                End If
                If drrc("ResRPTFile") IsNot DBNull.Value AndAlso Trim(drrc("ResRPTFile")) <> String.Empty Then
                    Configs(UBound(Configs)) += Trim(drrc("ResRPTFile")) & vbCr
                Else
                    Configs(UBound(Configs)) += vbCr
                End If
            End While
        End If
        cnrc.Close()
        cnrc = Nothing
        Return Configs
    End Function

    'Public Function GetTempFolder_OLD() As String
    '    Try

    '        Dim FolderPath As String = Path.Combine(My.Application.Info.DirectoryPath, "Temp", ThisUser.ID.ToString())

    '        'Dim FolderPath As String = My.Application.Info.DirectoryPath & "\Temp\" & ThisUser.ID.ToString & "\"
    '        If Not Directory.Exists(FolderPath) Then
    '            Directory.CreateDirectory(FolderPath)
    '            Dim UserAccount As String = "everyone" 'Specify the user here
    '            Dim FolderInfo As DirectoryInfo = New DirectoryInfo(FolderPath)
    '            Dim FolderAcl As New DirectorySecurity
    '            FolderAcl.AddAccessRule(New FileSystemAccessRule(UserAccount,
    '            FileSystemRights.Modify, InheritanceFlags.ContainerInherit Or
    '            InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow))
    '            FolderAcl.SetAccessRuleProtection(True, False) 'uncomment to remove existing permissions
    '            FolderInfo.SetAccessControl(FolderAcl)
    '        End If

    '        ' Delete files in the temporary folder
    '        For Each FlToDel As String In Directory.GetFiles(FolderPath, "*.*", SearchOption.TopDirectoryOnly)
    '            File.Delete(FlToDel)
    '        Next
    '        Return FolderPath

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Function

    Public Function GetTempFolder() As String
        Try
            ' Get the path to the user's AppData directory
            'Dim appDataPath As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
            'Dim folderPath As String = Path.Combine(appDataPath, Application.ProductName, "Temp", ThisUser.ID.ToString())
            Dim folderPath As String = Path.Combine(My.Application.Info.DirectoryPath, "Temp", ThisUser.ID.ToString())

            ' Create the directory if it doesn't exist
            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)

                ' Set access permissions for the folder
                Dim userAccount As String = Environment.UserName ' Use the current user
                Dim folderInfo As New DirectoryInfo(folderPath)
                Dim folderAcl As DirectorySecurity = folderInfo.GetAccessControl()

                folderAcl.AddAccessRule(New FileSystemAccessRule(userAccount,
                FileSystemRights.Modify, InheritanceFlags.ContainerInherit Or
                InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow))

                folderInfo.SetAccessControl(folderAcl)
            End If

            ' Delete files in the temporary folder
            For Each fileToDelete As String In Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly)
                File.Delete(fileToDelete)
            Next

            Return folderPath

        Catch ex As UnauthorizedAccessException
            MessageBox.Show("Access denied: " & ex.Message)
            Return String.Empty
        Catch ex As IOException
            MessageBox.Show("I/O error: " & ex.Message)
            Return String.Empty
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message)
            Return String.Empty
        End Try
    End Function

    Public Sub LogEventAndAuditTrail(AccID As String, EventID As Integer, RPTStatus As String, Optional OrdProvID As String = "")
        If String.IsNullOrEmpty(OrdProvID) Then
            OrdProvID = GetOrdProvIDFromAccID(AccID)
        End If

        LogEvent(AccID, EventID, OrdProvID, RPTStatus, False, ThisUser.UserName, "Result Reports")

        If SystemConfig.AuditTrail Then
            LogUserEvent(ThisUser.ID, EventID, Date.Now, "REPORT", AccID, String.Empty, String.Empty)
        End If
    End Sub

    Public Sub ProcessEmail(dt As DataTable, Recs As Integer, BW As BackgroundWorker, PB As ToolStripProgressBar, lblStatus As ToolStripLabel)
        For i As Integer = 0 To Recs - 1
            If Not BW.CancellationPending Then
                ExecuteSqlProcedure("Delete from Event_Capture where Event_ID = 13 and Accession_ID = " & dt.Rows(i).Item("AccID"))
                PB.Value = (i + 1) * 100 / Recs
                lblStatus.Text = (i + 1).ToString & " of " & Recs.ToString
                Application.DoEvents()
            End If
        Next
    End Sub

    Public Sub ProcessFax(dt As DataTable, Recs As Integer, BW As BackgroundWorker, PB As ToolStripProgressBar, lblStatus As ToolStripLabel)
        For i As Integer = 0 To Recs - 1
            If Not BW.CancellationPending Then
                ExecuteSqlProcedure("Delete from Event_Capture where Event_ID = 12 and Accession_ID = " & dt.Rows(i).Item("AccID"))
                ExecuteSqlProcedure("Delete from Fax_Log where Status = 'Failed' and Accession_ID = " & dt.Rows(i).Item("AccID"))
                PB.Value = (i + 1) * 100 / Recs
                lblStatus.Text = (i + 1).ToString & " of " & Recs.ToString
                Application.DoEvents()
            End If
        Next
    End Sub

    Public Sub ProcessInterface(dt As DataTable, Recs As Integer, BW As BackgroundWorker, PB As ToolStripProgressBar, lblStatus As ToolStripLabel)
        For i As Integer = 0 To Recs - 1
            If Not BW.CancellationPending Then
                ExecuteSqlProcedure("Delete from Event_Capture where Event_ID in (14, 15) and Accession_ID = " & dt.Rows(i).Item("AccID"))
                PB.Value = (i + 1) * 100 / Recs
                lblStatus.Text = (i + 1).ToString & " of " & Recs.ToString
                Application.DoEvents()
            End If
        Next
    End Sub

    Public Sub ProcessProlison(dt As DataTable, Recs As Integer, BW As BackgroundWorker, PB As ToolStripProgressBar, lblStatus As ToolStripLabel)
        For i As Integer = 0 To Recs - 1
            If Not BW.CancellationPending Then
                ExecuteSqlProcedure("Delete from Event_Capture where Event_ID = 16 and Accession_ID = " & dt.Rows(i).Item("AccID"))
                PB.Value = (i + 1) * 100 / Recs
                lblStatus.Text = (i + 1).ToString & " of " & Recs.ToString
                Application.DoEvents()
            End If
        Next
    End Sub

    Public Function SaveToPdf(FPDF As Byte()) As String
        Try
            Dim pdfFilePath As String = Path.Combine(GetTempFolder(), Guid.NewGuid.ToString & ".PDF")
            File.WriteAllBytes(pdfFilePath, FPDF)
            Return pdfFilePath
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return ""
    End Function

    'Private Function PrintSingleReport(ByVal RPTFile As String, ByVal AccID As String, ByVal Device As Integer) As Task
    '    'Dim SPDFS As New List(Of Byte())
    '    'Dim FPDF As Byte() = Nothing
    '    'Dim ExtCount As Integer = 0

    '    'If AccID <> String.Empty Then
    '    '    Dim FolderPath As String = Path.Combine(My.Application.Info.DirectoryPath, "Temp", ThisUser.ID.ToString())

    '    '    ' Create the temporary folder if it doesn't exist
    '    '    If Not Directory.Exists(FolderPath) Then
    '    '        Directory.CreateDirectory(FolderPath)

    '    '        ' Set permissions for the folder
    '    '        Dim UserAccount As String = "everyone" ' Specify the user here
    '    '        Dim FolderInfo As DirectoryInfo = New DirectoryInfo(FolderPath)
    '    '        Dim FolderAcl As New DirectorySecurity
    '    '        FolderAcl.AddAccessRule(New FileSystemAccessRule(UserAccount,
    '    '    FileSystemRights.Modify, InheritanceFlags.ContainerInherit Or
    '    '    InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow))
    '    '        FolderAcl.SetAccessRuleProtection(True, False) ' Uncomment to remove existing permissions
    '    '        FolderInfo.SetAccessControl(FolderAcl)
    '    '    End If

    '    '    '*************************** FOR RPT OPTIMIZATION TASK BY AQEEL 14 MAY 2024
    '    '    ExecuteSqlProcedure("Exec usp_Acc_Result '" & AccID & "' ")
    '    '    '***************************

    '    '    ' Load the Crystal Report
    '    '    Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
    '    '    oRpt.Load(RPTFile)
    '    '    ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database,
    '    'My.Settings.UID, My.Settings.PWD)
    '    '    oRpt.RecordSelectionFormula = "{Requisitions.ID} = " & AccID & " AND {Acc_Results.Result} <> '.'"

    '    '    'frmRV.CRRV.ReportSource = oRpt
    '    '    'frmRV.Show()

    '    '    'Stop

    '    '    'Return
    '    '    ' Export the Crystal Report to a MemoryStream
    '    '    Try
    '    '        Dim S As New MemoryStream()
    '    '        oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat).CopyTo(S)
    '    '        S.Position = 0
    '    '        SPDFS.Add(S.ToArray())
    '    '        S.Close()
    '    '        S.Dispose()
    '    '    Catch ex As Exception
    '    '        Dim err As String = ex.Message()
    '    '        ' Handle the exception
    '    '    End Try

    '    '    ' Get additional PDF data from the database
    '    '    Using cner As New SqlClient.SqlConnection(connString)
    '    '        cner.Open()
    '    '        Using cmder As New SqlClient.SqlCommand("Select Result from Extend_Results where Accession_ID = " & AccID, cner)
    '    '            cmder.CommandType = CommandType.Text
    '    '            Using drer As SqlClient.SqlDataReader = cmder.ExecuteReader
    '    '                If drer.HasRows Then
    '    '                    While drer.Read
    '    '                        SPDFS.Add(drer("Result"))
    '    '                        ExtCount += 1
    '    '                    End While
    '    '                End If
    '    '            End Using
    '    '        End Using
    '    '    End Using

    '    '    ' Merge the PDF streams
    '    '    FPDF = PDFMerger.MergePDFStreams(SPDFS)

    '    '    ' Delete files in the temporary folder
    '    '    For Each FlToDel As String In Directory.GetFiles(FolderPath, "*.*", SearchOption.TopDirectoryOnly)
    '    '        File.Delete(FlToDel)
    '    '    Next

    '    '    ' Write the merged PDF to a file
    '    '    Dim pdfFilePath As String = Path.Combine(FolderPath, AccID & ".PDF")
    '    '    File.WriteAllBytes(pdfFilePath, FPDF)

    '    '    If Device = 0 Then  ' Printer
    '    '        ' Print the PDF file
    '    '        Dim PDFDOC As New Spire.Pdf.PdfDocument
    '    '        PDFDOC.LoadFromFile(pdfFilePath)
    '    '        Dim PS As New System.Drawing.Printing.PrinterSettings
    '    '        PDFDOC.PrintSettings.PrinterName = PS.PrinterName
    '    '        PDFDOC.Print()
    '    '        My.Application.DoEvents()

    '    '    ElseIf Device = 1 Then  ' Screen

    '    '        Dim f As New frmWebView()
    '    '        f.LoadPdfData(FPDF)
    '    '        f.MdiParent = ProlisQC
    '    '        f.Show()


    '    '    End If
    '    'End If
    'End Function

    Public Sub PrintLabels(ByVal Printer As String, ByVal Accession As String, ByVal Qty As Integer, Optional strData As String = "", Optional SpecimenList As List(Of String) = Nothing, Optional formNo As Integer = 0)

        Dim ReqNo As String = String.Empty
        Dim Initial As String = String.Empty
        Dim Date1 As String = String.Empty
        Dim P As String = String.Empty

        Dim i As Integer

        If Not String.IsNullOrWhiteSpace(strData) Then

            Dim pmData() As String = Split(strData, "|")

            If pmData.Length > 0 Then
                ReqNo = pmData(0)
            End If

            If pmData.Length > 1 Then
                Initial = pmData(1)
            End If

            If pmData.Length > 2 Then
                Date1 = pmData(2)
            End If

            If pmData.Length > 3 Then
                P = pmData(3)
            End If


        End If

        If Printer = "Brady" Then

            If String.IsNullOrEmpty(ReqNo) Then
                Dim req = CommonData.RetrieveColumnValue("Requisitions", "RequisitionNo", "ID", Accession, "")
                ReqNo = req.ToString()
            End If
            'Dim Initial = ""
            'Dim Date1 = ""
            'Dim P = ""
            Dim par() As Object = {Accession + "|" + ReqNo.Trim() + "|" + P + "|" + Initial, Qty.ToString()}
            Dim res = Services.InvokeMethod("BradyPrinter.dll", "Automation", "SaveTextFile", par)

            Return
        End If

        Dim filePath As String = ""

        If SystemConfig.AccLabel <> "" Then
            'lblPath = ValidateLabelFile(SystemConfig.AccLabel)
            filePath = GetReportPath(SystemConfig.AccLabel)
        Else
            If InStr(Printer, "DYMO") > 0 Then
                'lblPath = My.Application.Info.DirectoryPath & "\Reports\Dymo30334Tst.Label"
                filePath = GetReportPath("Dymo30334Tst.Label")
            Else    'Zebra
                'lblPath = My.Application.Info.DirectoryPath & "\Reports\ZebraAccTst.rpt"
                filePath = GetReportPath("ZebraAccTst.rpt")
            End If
        End If

        If InStr(Printer, "DYMO") > 0 Then

            Dim LabelInfo() As String = GetLabelInfo(Accession, Qty, SpecimenList, formNo)
            Dim RawData As String = ""

            If InStr(Printer, "Prolis Remote") > 0 And ThisUser.UseRemotePrinter Then
                For i = LBound(LabelInfo) To UBound(LabelInfo)
                    RawData = LabelInfo(i).ToString

                    If formNo = 0 Then
                        RawData += "|1|"
                    End If

                    ExecuteSqlProcedure("insert into LabelPrintJobs(Processed,PrintJob,PrinterPC) values(0,'" & RawData & "','" & ThisUser.PrinterPC & "')")
                    ExecuteSqlProcedure("delete from LabelPrintJobs where Processed = 1 ")
                Next
                Return
            End If

            For i = LBound(LabelInfo) To UBound(LabelInfo)
                Try
                    RawData = LabelInfo(i).ToString
                    Dim Data() As String = Split(RawData, "|")
                    'TODO: Dymo Code
                    'DymoAddIn = New DYMO.DymoAddIn
                    'DymoLabel = New DYMO.DymoLabels

                    Dim objectNames As List(Of String) = New List(Of String)()

                    '**************************************
                    If File.Exists(filePath) Then
                        ' Read the contents of the label file
                        Dim labelXml As String = File.ReadAllText(filePath)

                        ' Parse the XML to extract object names
                        objectNames = ExtractObjectNames(labelXml, Path.GetExtension(filePath))

                    End If
                    '**************************************

                    'lblPath = "C:\Reports\DymoAcc1x2-18.Label"
                    'Todo: Dymo Code
                    '=============================
                    'If DymoAddIn.Open(filePath) Then

                    '    If objectNames.Contains("Provider") Then

                    '        DymoLabel.SetField("Provider", Data(0))
                    '    End If

                    '    If objectNames.Contains("Patient") Then

                    '        DymoLabel.SetField("Patient", Data(1))
                    '    End If

                    '    If objectNames.Contains("AccID") Then

                    '        DymoLabel.SetField("AccID", Data(2))
                    '    End If

                    '    If objectNames.Contains("AccDate") Then

                    '        DymoLabel.SetField("AccDate", Data(3))
                    '    End If

                    '    If objectNames.Contains("Tests") Then

                    '        DymoLabel.SetField("Tests", Data(4))
                    '    End If

                    '    If objectNames.Contains("EMRNo") Then

                    '        DymoLabel.SetField("EMRNo", Data(5))
                    '    End If

                    '    If objectNames.Contains("ReqNo") AndAlso Data.Length > 7 Then

                    '        DymoLabel.SetField("ReqNo", Data(7))
                    '    End If


                    '    Try
                    '        If filePath.ToLower().Contains("skippa") Then
                    '            Dim pname = Data(1).Split("-")(0).TrimEnd(",")
                    '            Dim Gender = "" & Data(1).Split("-")(1)
                    '            Dim dob = "" & Data(1).Split("-")(2)
                    '            Dim currentDate As Date = Format(Date.Now, SystemConfig.DateFormat)
                    '            Dim birthdate As Date = dob

                    '            Dim age As Integer = currentDate.Year - birthdate.Year

                    '            DymoLabel.SetField("Patient", pname)
                    '            DymoLabel.SetField("Patient_1", "DOB:" & dob & "    Age:" & age & " " & Gender)
                    '        End If
                    '    Catch ex As Exception
                    '        MsgBox("560 Dymo Error:  " & ex.Message, MsgBoxStyle.Critical, "Prolis")
                    '    End Try

                    '    '==========================
                    '    If SystemConfig.P_inputOnLabel Then

                    '        If objectNames.Contains("Initial") Then
                    '            DymoLabel.SetField("Initial", Initial)
                    '        End If
                    '        If objectNames.Contains("Date") Then
                    '            DymoLabel.SetField("Date", Date1)

                    '        End If
                    '        If objectNames.Contains("P?") Then
                    '            DymoLabel.SetField("P?", P)

                    '        End If

                    '    End If

                    '    '==========================\tr
                    '    Try
                    '        DymoAddIn.Print(Val(Data(6)), False)
                    '    Catch ex As Exception
                    '        DymoAddIn.Print(1, False)
                    '    End Try



                    'Else
                    '    MsgBox("Dymo Label file can not be opened", MsgBoxStyle.Critical, "Prolis")
                    'End If
                    'DymoAddIn = Nothing
                    'DymoLabel = Nothing
                    '=============================
                Catch ex As Exception
                    Dim gg = ""
                    MsgBox("DymoLabel couldn't loaded: " & ex.Message, MsgBoxStyle.Critical, "Prolis")
                End Try
            Next
        Else
            Try
                'TODO: Crystal Report 
                '=================================
                'Dim UID As String = My.Settings.UID.ToString
                'Dim PWD As String = My.Settings.PWD.ToString
                'Dim gReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                'If Not SystemConfig.AccLabel Is Nothing AndAlso SystemConfig.AccLabel <> "" Then
                '    gReport.Load(filePath)
                'Else
                '    If InStr(Printer, "Zebra") > 0 Then
                '        gReport.Load(GetReportPath("ZebraLabel.rpt"))
                '    Else
                '        gReport.Load(GetReportPath("GenLabel.rpt"))
                '    End If
                'End If
                'Dim pSize As CrystalDecisions.Shared.PaperSize = gReport.PrintOptions.PaperSize
                'gReport.SetDatabaseLogon(UID, PWD)
                'gReport.RecordSelectionFormula = "{Requisitions.ID} = " & Val(Accession)
                'gReport.PrintOptions.PrinterName = Printer
                'gReport.PrintOptions.PaperSize = pSize
                'gReport.PrintToPrinter(Qty, False, 0, 0)
                'gReport.Close()
                'gReport = Nothing

                '=================================
            Catch ex As Exception
                MsgBox("622 ZebraLabel Error:  " & ex.Message, MsgBoxStyle.Critical, "Prolis")

            End Try

        End If
    End Sub

    Function ExtractObjectNames(labelXml As String, fileExt As String) As List(Of String)
        Dim objectNames As New List(Of String)()

        ' Load the label XML document
        Dim xmlDoc As New XmlDocument()
        xmlDoc.LoadXml(labelXml)

        Dim mainNode, childNode As String

        If fileExt = ".label" Then
            mainNode = "//ObjectInfo"
            childNode = ".//*[local-name()='Name']"
        Else
            mainNode = "//LabelObjects/*"
            childNode = "Name"

        End If

        ' Select all ObjectInfo nodes
        Dim objectNodes As XmlNodeList = xmlDoc.SelectNodes(mainNode)

        ' Extract object names
        For Each objNode As XmlNode In objectNodes
            ' Select the Name node irrespective of the parent node type
            Dim nameNode As XmlNode = objNode.SelectSingleNode(childNode)
            If nameNode IsNot Nothing Then
                Dim objName As String = nameNode.InnerText
                If Not String.IsNullOrEmpty(objName) Then
                    objectNames.Add(objName)
                End If
            End If
        Next

        Return objectNames
    End Function
    Private Function GetLabelInfo(ByVal AccID As Long, ByVal QTY As Integer, Optional SpecimenList As List(Of String) = Nothing, Optional formNo As Integer = 0) As String()
        ' Initialize labelInfo and Sources as lists
        Dim labelInfo As New List(Of String)()
        Dim Sources As New List(Of String)()

        Dim Provider As String = ""
        Dim Patient As String = ""
        Dim Tests As String = ""
        Dim AccDate As String = ""
        Dim EMRNo As String = ""

        Try
            Using conn As New SqlConnection(connString)
                conn.Open()

                ' Fetching Sources and their quantities
                Dim selcmd As New SqlCommand("SELECT a.ID as ID, a.Name as Source, " &
                                                        "b.SourceQuantity as QTY FROM Sources a " &
                                                        "INNER JOIN Specimens b ON a.ID = b.Source_ID " &
                                                        "WHERE b.Accession_ID = @AccID", conn)
                selcmd.Parameters.AddWithValue("@AccID", AccID)
                Using DR As SqlDataReader = selcmd.ExecuteReader()
                    If DR.HasRows Then
                        While DR.Read
                            Sources.Add($"{DR("ID").ToString}^{Trim(DR("Source").ToString)}^{DR("QTY").ToString}")
                        End While
                    End If
                End Using

                ' Fetching Provider details
                Dim prcmd As New SqlCommand("SELECT a.OrderingProvider_ID as ClinicID, b.* FROM " &
                                                       "Requisitions a INNER JOIN Providers b ON a.AttendingProvider_ID = b.ID " &
                                                       "WHERE a.ID = @AccID", conn)
                prcmd.Parameters.AddWithValue("@AccID", AccID)
                Using pDR As SqlDataReader = prcmd.ExecuteReader()
                    If pDR.HasRows Then
                        While pDR.Read
                            If pDR("IsIndividual") IsNot DBNull.Value AndAlso pDR("IsIndividual") = 0 Then
                                Provider = $"{pDR("ClinicID")}-{Trim(pDR("LastName_BSN").ToString)}"
                            Else
                                Provider = $"{pDR("ClinicID")}-{Trim(pDR("LastName_BSN").ToString)}, {Trim(pDR("FirstName").ToString)} " &
                                       $"{If(Not IsDBNull(pDR("Degree")), Trim(pDR("Degree").ToString), "")}"
                            End If
                        End While
                    End If
                End Using

                ' Fetching Patient details
                Dim patcmd As New SqlCommand("SELECT * FROM Patients WHERE ID IN " &
                                                        "(SELECT Patient_ID FROM Requisitions WHERE ID = @AccID)", conn)
                patcmd.Parameters.AddWithValue("@AccID", AccID)
                Using tDR As SqlDataReader = patcmd.ExecuteReader()
                    If tDR.HasRows Then
                        While tDR.Read
                            Patient = If(Not IsDBNull(tDR("MiddleName")) AndAlso Trim(tDR("MiddleName").ToString) <> "",
                                     $"{tDR("LastName")}, {tDR("FirstName")} {Trim(tDR("MiddleName").ToString).Substring(0, 1)}-" &
                                     $"{Microsoft.VisualBasic.Left(tDR("Sex").ToString, 1)}-{Format(tDR("DOB"), SystemConfig.DateFormat)}",
                                     $"{tDR("LastName")}, {tDR("FirstName")}-{Microsoft.VisualBasic.Left(tDR("Sex").ToString, 1)}-" &
                                     $"{Format(tDR("DOB"), SystemConfig.DateFormat)}")
                        End While
                    End If
                End Using

                ' Fetching Tests
                Dim csncmd As New SqlCommand("SELECT (SELECT b.Abbr FROM Tests b WHERE b.ID = a.TGP_ID " &
                                                        "UNION SELECT c.Abbr FROM Groups c WHERE c.ID = a.TGP_ID " &
                                                        "UNION SELECT d.Abbr FROM Profiles d WHERE d.ID = a.TGP_ID) as TGPName " &
                                                        "FROM Req_TGP a WHERE a.Accession_ID = @AccID", conn)
                csncmd.Parameters.AddWithValue("@AccID", AccID)
                Using csnDR As SqlDataReader = csncmd.ExecuteReader()
                    If csnDR.HasRows Then
                        Dim comps As New List(Of String)()
                        While csnDR.Read
                            comps.Add(Trim(csnDR("TGPName").ToString))
                        End While
                        Tests = String.Join(",", comps)
                    End If
                End Using

                ' Fetching AccDate and EMRNo
                Dim srccmd As New SqlCommand("SELECT (SELECT Min(SourceDate) FROM Specimens WHERE Accession_ID = @AccID) as DOC, EMRNo " &
                                                        "FROM Requisitions WHERE ID = @AccID", conn)
                srccmd.Parameters.AddWithValue("@AccID", AccID)
                Using srcDR As SqlDataReader = srccmd.ExecuteReader()
                    If srcDR.HasRows Then
                        While srcDR.Read
                            AccDate = Format(srcDR("DOC"), SystemConfig.DateFormat)
                            If Not IsDBNull(srcDR("EMRNo")) Then
                                EMRNo = Trim(srcDR("EMRNo").ToString)
                            End If
                        End While
                    End If
                End Using

                If SpecimenList Is Nothing Then
                    SpecimenList = New List(Of String)()

                End If

                ' Construct labelInfo based on QTY and Sources
                'If QTY >= Sources.Count Then
                If Sources.Count > 0 Then
                    '===============================================
                    ' THIS CODE IS SPECIFIC FOR CDL
                    If MyLab.LabName = "Clinical Diagnostic Laboratories" Then
                        Dim printCounter As Boolean = False
                        Dim cmder As New SqlCommand("SELECT AddCounterWithLabels FROM System_Config WHERE Company_ID = @CompanyID", conn)
                        cmder.Parameters.AddWithValue("@CompanyID", MyLab.ID)
                        Dim result As Object = cmder.ExecuteScalar()
                        If Not IsDBNull(result) Then
                            printCounter = Convert.ToBoolean(result)
                        End If

                        If SpecimenList.Count > 0 Then
                            For Each source In Sources

                                Dim PrintQty As Integer
                                Dim SRC() As String = source.Split("^"c)

                                Dim matchFound As Boolean = False
                                For Each specimen In SpecimenList
                                    Dim splter = "^"

                                    splter = "|"
                                    Dim specimenParts() As String = specimen.Split(splter)

                                    PrintQty = specimenParts(1)
                                    If specimenParts(0) = SRC(0) Then
                                        matchFound = True
                                        Exit For
                                    End If
                                Next

                                ' If match is found, proceed
                                If matchFound Then

                                    For n As Integer = 1 To PrintQty
                                        Dim var As String = If(printCounter, n.ToString(), SRC(0))
                                        labelInfo.Add($"{Provider}|{Patient}|{AccID}-{var}|{AccDate}|{SRC(1)}|{EMRNo}|1")
                                    Next
                                End If
                            Next
                            If QTY > labelInfo.Count Then
                                For n As Integer = 1 To QTY - labelInfo.Count
                                    labelInfo.Add($"{Provider}|{Patient}|{AccID}|{AccDate}|{Tests}|{EMRNo}|1")
                                Next
                            End If
                        Else

                            If QTY >= Sources.Count Then
                                For Each source In Sources
                                    Dim SRC() As String = source.Split("^"c)
                                    For n As Integer = 0 To Val(SRC(2)) - 1

                                        labelInfo.Add($"{Provider}|{Patient}|{AccID}-{SRC(0)}|{AccDate}|{SRC(1)}|{EMRNo}|1")
                                    Next
                                Next

                                If QTY > labelInfo.Count Then
                                    For n As Integer = 1 To QTY - labelInfo.Count
                                        labelInfo.Add($"{Provider}|{Patient}|{AccID}|{AccDate}|{Tests}|{EMRNo}|1")
                                    Next
                                End If
                            Else
                                For n As Integer = 1 To QTY
                                    labelInfo.Add($"{Provider}|{Patient}|{AccID}|{AccDate}|{Tests}|{EMRNo}|1")
                                Next
                            End If


                        End If





                        Return labelInfo.ToArray()
                    Else
                        If formNo = 1 And SpecimenList.Count > 0 Then
                            For Each source In Sources
                                Dim applicble = False
                                Dim SRC() As String = source.Split("^"c)
                                For Each sp In SpecimenList
                                    Dim spparts = sp.Split("|")
                                    If spparts(0) = SRC(0) Then
                                        applicble = True
                                        SRC(2) = spparts(1)
                                    End If
                                Next
                                If applicble Then
                                    For n As Integer = 0 To Val(SRC(2)) - 1

                                        labelInfo.Add($"{Provider}|{Patient}|{AccID}-{SRC(0)}|{AccDate}|{SRC(1)}|{EMRNo}|1")
                                    Next
                                End If


                            Next
                            If QTY >= labelInfo.Count Then
                                For n As Integer = 0 To QTY - labelInfo.Count
                                    labelInfo.Add($"{Provider}|{Patient}|{AccID}|{AccDate}|{Tests}|{EMRNo}|1")
                                Next

                            End If
                        Else
                            If SpecimenList.Count > 0 Then
                                For Each source In Sources
                                    Dim applicble = False
                                    Dim SRC() As String = source.Split("^"c)
                                    For Each sp In SpecimenList
                                        Dim spparts = sp.Split("|")
                                        If spparts(0) = SRC(0) Then
                                            applicble = True
                                            SRC(2) = spparts(1)
                                        End If
                                    Next
                                    If applicble Then
                                        For n As Integer = 0 To Val(SRC(2)) - 1

                                            labelInfo.Add($"{Provider}|{Patient}|{AccID}-{SRC(0)}|{AccDate}|{SRC(1)}|{EMRNo}|1")
                                        Next
                                    End If


                                Next
                                If QTY > labelInfo.Count Then
                                    For n As Integer = 1 To QTY - labelInfo.Count
                                        labelInfo.Add($"{Provider}|{Patient}|{AccID}|{AccDate}|{Tests}|{EMRNo}|1")
                                    Next

                                End If
                            Else
                                If formNo = 2 Then

                                    For Each source In Sources
                                        Dim SRC() As String = source.Split("^"c)
                                        For n As Integer = 0 To Val(SRC(2)) - 1

                                            labelInfo.Add($"{Provider}|{Patient}|{AccID}-{SRC(0)}|{AccDate}|{SRC(1)}|{EMRNo}|1")
                                        Next
                                    Next

                                    If QTY > labelInfo.Count Then
                                        For n As Integer = 1 To QTY - labelInfo.Count
                                            labelInfo.Add($"{Provider}|{Patient}|{AccID}|{AccDate}|{Tests}|{EMRNo}|1")
                                        Next
                                    Else
                                        If QTY > 0 Then
                                            For n As Integer = 0 To QTY - 1
                                                labelInfo.Add($"{Provider}|{Patient}|{AccID}|{AccDate}|{Tests}|{EMRNo}|1")
                                            Next

                                        End If
                                    End If



                                Else
                                    If formNo = 1 And SpecimenList.Count <= 0 Then
                                        For n As Integer = 1 To QTY
                                            labelInfo.Add($"{Provider}|{Patient}|{AccID}|{AccDate}|{Tests}|{EMRNo}|1")
                                        Next
                                    Else
                                        If QTY >= Sources.Count Then
                                            For Each source In Sources
                                                Dim SRC() As String = source.Split("^"c)
                                                For n As Integer = 0 To Val(SRC(2)) - 1

                                                    labelInfo.Add($"{Provider}|{Patient}|{AccID}-{SRC(0)}|{AccDate}|{SRC(1)}|{EMRNo}|1")
                                                Next
                                            Next

                                            If QTY > labelInfo.Count Then
                                                For n As Integer = 1 To QTY - labelInfo.Count
                                                    labelInfo.Add($"{Provider}|{Patient}|{AccID}|{AccDate}|{Tests}|{EMRNo}|1")
                                                Next
                                            End If


                                        Else
                                            For n As Integer = 1 To QTY
                                                labelInfo.Add($"{Provider}|{Patient}|{AccID}|{AccDate}|{Tests}|{EMRNo}|1")
                                            Next
                                        End If
                                    End If
                                End If


                            End If
                        End If

                        '===============================================


                    End If
                Else
                    If QTY > 0 Then
                        For n As Integer = 0 To QTY - 1
                            labelInfo.Add($"{Provider}|{Patient}|{AccID}|{AccDate}|{Tests}|{EMRNo}|1")
                        Next
                    End If
                End If
            End Using

            ' Return labelInfo as an array
            Return labelInfo.ToArray()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return Nothing
        End Try
    End Function

    Private Function GetLabelInfo0(ByVal AccID As Long, ByVal QTY As Integer) As String()

        Dim labelInfo() As String = {""}
        'Dim labelInfo As New List(Of String)()
        Dim Sources() As String = {""}
        'Dim Sources As New List(Of String)()

        Dim Provider As String = ""
        Dim Patient As String = ""
        Dim Tests As String = ""
        Dim AccDate As String = ""
        Dim EMRNo As String = ""
        '
        Try

            Dim conn As New SqlConnection(connString)
            conn.Open()

            Dim selcmd As New SqlCommand("Select a.ID as ID, a.Name as Source, " &
            "b.SourceQuantity as QTY from Sources a inner join Specimens b on a.ID = b.Source_ID " &
            "where b.Accession_ID = " & AccID, conn)
            selcmd.CommandType = Data.CommandType.Text
            Dim DR As SqlDataReader = selcmd.ExecuteReader
            If DR.HasRows Then
                While DR.Read
                    If Sources(UBound(Sources)) <> "" Then ReDim Preserve Sources(UBound(Sources) + 1)
                    Sources(UBound(Sources)) = DR("ID").ToString & "^" &
                    Trim(DR("Source")) & "^" & DR("QTY").ToString


                    ' Build the source string and add it to the list
                    'Dim sourceInfo As String = $"{DR("ID")}^" &
                    '                               $"{DR("Source").ToString().Trim()}^" &
                    '                               $"{DR("QTY") * QTY}"
                    'Sources.Add(sourceInfo)
                End While
            End If
            DR.Close()
            selcmd.Dispose()
            selcmd = Nothing
            'cnn.Close()
            'cnn = Nothing
            '
            'Dim cn1 As New SqlConnection(connString)
            'cn1.Open()
            Dim prcmd As New SqlCommand("Select a.OrderingProvider_ID as ClinicID, b.* from " &
            "Requisitions a inner join Providers b on a.AttendingProvider_ID = b.ID where a.ID = " & AccID, conn)
            prcmd.CommandType = Data.CommandType.Text
            Dim pDR As SqlDataReader = prcmd.ExecuteReader
            If pDR.HasRows Then
                While pDR.Read
                    If pDR("IsIndividual") IsNot DBNull.Value AndAlso pDR("IsIndividual") = 0 Then
                        Provider = pDR("ClinicID").ToString & "-" & Trim(pDR("LastName_BSN"))
                    Else
                        If pDR("Degree") Is DBNull.Value OrElse pDR("Degree") = "" Then
                            Provider = pDR("ClinicID").ToString & "-" & Trim(pDR("LastName_BSN")) & ", " & Trim(pDR("FirstName"))
                        Else
                            Provider = pDR("ClinicID").ToString & "-" & Trim(pDR("LastName_BSN")) & ", " & Trim(pDR("FirstName")) &
                            " " & Trim(pDR("Degree"))
                        End If
                    End If
                End While
            End If
            pDR.Close()
            prcmd.Dispose()
            prcmd = Nothing
            'cn1.Close()
            'cn1 = Nothing
            '
            'Dim cn2 As New SqlConnection(connString)
            'cn2.Open()
            Dim patcmd As New SqlCommand("Select * from Patients where ID in " &
            "(Select Patient_ID from Requisitions where ID = " & AccID & ")", conn)
            patcmd.CommandType = Data.CommandType.Text
            Dim tDR As SqlDataReader = patcmd.ExecuteReader
            If tDR.HasRows Then
                While tDR.Read
                    If tDR("MiddleName") IsNot DBNull.Value AndAlso Trim(tDR("MiddleName")) <> "" Then
                        Patient = tDR("LastName") & ", " & tDR("FirstName") & " " & tDR("MiddleName").ToString.Substring(0, 1) _
                        & "-" & Microsoft.VisualBasic.Left(tDR("Sex"), 1) & "-" & Format(tDR("DOB"), SystemConfig.DateFormat)
                    Else
                        Patient = tDR("LastName") & ", " & tDR("FirstName") & "-" & tDR("Sex").ToString.Substring(0, 1) &
                        "-" & Format(tDR("DOB"), SystemConfig.DateFormat)
                    End If
                End While
            End If
            tDR.Close()
            patcmd.Dispose()
            patcmd = Nothing
            'cn2.Close()
            'cn2 = Nothing
            '
            Dim Comps As String = ""
            'Dim cn3 As New SqlConnection(connString)
            'cn3.Open()
            Dim csncmd As New SqlCommand("Select (Select b.Abbr from Tests b where b.ID = a.TGP_ID " &
            "Union Select c.Abbr from Groups c where c.ID = a.TGP_ID Union Select d.Abbr from Profiles d where " &
            "d.ID = a.TGP_ID ) as TGPName from Req_TGP a where a.Accession_ID = " & AccID, conn)
            csncmd.CommandType = Data.CommandType.Text
            Dim csnDR As SqlDataReader = csncmd.ExecuteReader
            If csnDR.HasRows Then
                While csnDR.Read
                    If InStr(Comps, Trim(csnDR("TGPName"))) = 0 Then Comps += Trim(csnDR("TGPName")) & ","
                End While
                Comps = Comps.Substring(0, Len(Comps) - 1)
                Tests = Comps
            End If
            csnDR.Close()
            csncmd.Dispose()
            'cn3.Close()
            'cn3 = Nothing
            '
            'Dim cn4 As New SqlConnection(connString)
            'cn4.Open()
            Dim srccmd As New SqlCommand("Select (Select Min(SourceDate) from Specimens where " &
            "Accession_ID = " & AccID & ") as DOC, EMRNo from Requisitions where ID = " & AccID, conn)
            srccmd.CommandType = Data.CommandType.Text
            Dim srcDR As SqlDataReader = srccmd.ExecuteReader
            If srcDR.HasRows Then
                While srcDR.Read
                    AccDate = Format(srcDR("DOC"), SystemConfig.DateFormat)
                    If srcDR("EMRNo") IsNot DBNull.Value AndAlso
                    Trim(srcDR("EMRNo")) <> "" Then EMRNo = Trim(srcDR("EMRNo"))
                End While
            End If
            srcDR.Close()
            srccmd.Dispose()
            srccmd = Nothing
            'cn4.Close()
            'cn4 = Nothing
            '
            Dim SRC() As String
            Dim SUFX As String = ""

            Dim printCounter As Boolean = False

            'Using conn '' As New SqlClient.SqlConnection(connString)
            'cner.Open()
            Dim cmder As New SqlCommand("select AddCounterWithLabels from System_Config where Company_ID = " & MyLab.ID, conn)
            cmder.CommandType = CommandType.Text
            Dim result As Object = cmder.ExecuteScalar()

            ' Check if the result is NULL
            If Not IsDBNull(result) Then
                ' If result is not NULL, assign it to printCounter
                printCounter = Convert.ToBoolean(result)

            End If
            conn.Close()
            conn.Dispose()
            'End Using

            'For Each source In Sources
            '    ' Split the source string by "^"
            '    Dim srcParts() As String = source.Split("^"c)

            '    ' Get the repeat count from the third element (SRC(2)) and parse it
            '    Dim repeatCount As Integer = Integer.Parse(srcParts(2))

            '    ' Loop through and add the appropriate data to labelInfo
            '    For n As Integer = 0 To repeatCount - 1
            '        ' Construct the label info string using string interpolation for clarity

            '        Dim var As String = IIf(printCounter = True, n + 1, srcParts(0))

            '        'Dim info As String = $"{Provider}|{Patient}|{AccID}-{srcParts(0)}|{AccDate}|{srcParts(1)}|{EMRNo}"
            '        Dim info As String = $"{Provider}|{Patient}|{AccID}-{var}|{AccDate}|{srcParts(1)}|{EMRNo}"
            '        labelInfo.Add(info)
            '    Next
            'Next

            If QTY >= Sources.Length Then
                For i As Integer = LBound(Sources) To UBound(Sources)
                    SRC = Split(Sources(i), "^")
                    For n As Integer = 0 To Val(SRC(2)) - 1
                        If labelInfo(UBound(labelInfo)) <> "" Then _
                        ReDim Preserve labelInfo(UBound(labelInfo) + 1)
                        labelInfo(UBound(labelInfo)) = Provider & "|" &
                        Patient & "|" & AccID.ToString & "-" & SRC(0) &
                        "|" & AccDate & "|" & SRC(1) & "|" & EMRNo
                    Next
                Next

                If QTY > labelInfo.Length Then
                    For n As Integer = 1 To QTY - labelInfo.Length

                        Dim Acc_New As String = AccID.ToString & IIf(printCounter = True, "-" & n.ToString("D2"), "")

                        If labelInfo(UBound(labelInfo)) <> "" Then _
                        ReDim Preserve labelInfo(UBound(labelInfo) + 1)
                        labelInfo(UBound(labelInfo)) = Provider & "|" &
                        Patient & "|" & Acc_New & "|" &
                        AccDate & "|" & Tests & "|" & EMRNo
                    Next
                End If
            Else
                For n As Integer = 1 To QTY

                    Dim Acc_New As String = AccID.ToString & IIf(printCounter = True, "-" & n.ToString("D2"), "")

                    If labelInfo(UBound(labelInfo)) <> "" Then _
                    ReDim Preserve labelInfo(UBound(labelInfo) + 1)
                    labelInfo(UBound(labelInfo)) = Provider & "|" &
                    Patient & "|" & Acc_New & "|" &
                    AccDate & "|" & Tests & "|" & EMRNo
                Next
            End If
            Return labelInfo
            'Return labelInfo.ToArray()

        Catch ex As Exception
            Return labelInfo
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Function GetReportPath(fileName As String) As String
        ' Path to the "Custom Reports" folder
        Dim customReportsPath As String = Path.Combine(Application.StartupPath, "Reports", "Custom Reports")

        ' Check if the file exists in "Custom Reports" folder
        Dim customReportFile As String = Path.Combine(customReportsPath, fileName)
        If File.Exists(customReportFile) Then
            ' If the file exists in "Custom Reports" folder, return its path
            Return customReportFile
        Else
            ' If the file doesn't exist in "Custom Reports" folder,
            ' fallback to the main "Reports" folder
            Dim reportsPath As String = Path.Combine(Application.StartupPath, "Reports")
            Dim reportFile As String = Path.Combine(reportsPath, fileName)

            ' Check if the file exists in the main "Reports" folder
            If File.Exists(reportFile) Then
                ' If the file exists in the main "Reports" folder, return its path
                Return reportFile
            Else
                ' If the file doesn't exist in both "Custom Reports" and main "Reports" folders, return an empty string or handle as needed
                Return String.Empty
            End If
        End If
    End Function

    Private Function GetFlagInfo(ByVal AccID As Long) As String

        Dim info As String = ""
        '
        Try

            Dim conn As New SqlConnection(connString)
            conn.Open()

            Dim query As String = $"SELECT STRING_AGG(flag,', ') as 'Flag_Abbr' from (
                                    SELECT DISTINCT
                                        CASE
                                            WHEN LOWER(LTRIM(RTRIM(Acc_Results.Flag))) = 'critical low' THEN 'CL = Critical Low'
                                            WHEN LOWER(LTRIM(RTRIM(Acc_Results.Flag))) = 'critical high' THEN 'CH = Critical High'
                                            WHEN LOWER(LTRIM(RTRIM(Acc_Results.Flag))) = 'critical abnormal' THEN 'CA = Critical Abnormal'
                                            WHEN LOWER(LTRIM(RTRIM(Acc_Results.Flag))) = 'abnormal' THEN 'A = Abnormal'
                                            --WHEN LOWER(LTRIM(RTRIM(Acc_Results.Flag))) = 'abnormal' THEN 'A = Abnormal'
                                            WHEN LOWER(LTRIM(RTRIM(Acc_Results.Flag))) = 'low' THEN 'L = Low'
		                                    WHEN LOWER(LTRIM(RTRIM(Acc_Results.Flag))) = 'l' THEN 'L = Low'
                                            WHEN LOWER(LTRIM(RTRIM(Acc_Results.Flag))) = 'high' THEN 'H = High'
                                            WHEN LOWER(LTRIM(RTRIM(Acc_Results.Flag))) = 'h' THEN 'H = High'

                                            ELSE LEFT(LTRIM(RTRIM(Acc_Results.Flag)), 2)
                                        END AS flag  
                                    FROM Acc_Results where Accession_ID = {AccID} and Acc_Results.Flag is not null 
                                    and len(LTRIM(RTRIM(Acc_Results.Flag))) > 0
                                    and Behavior <> 'Ignore'
                                    )z"
            Dim selcmd As New SqlCommand(query, conn)
            selcmd.CommandType = Data.CommandType.Text
            Dim DR As SqlDataReader = selcmd.ExecuteReader
            If DR.HasRows Then
                While DR.Read

                    info = DR(0).ToString

                End While
            End If
            DR.Close()
            selcmd.Dispose()
            selcmd = Nothing

            Return info
            'Return labelInfo.ToArray()

        Catch ex As Exception
            Return info
            MessageBox.Show(ex.Message)
        End Try
    End Function


End Module