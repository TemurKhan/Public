Option Compare Text
Imports System.Globalization
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Printing.PrinterSettings
Imports System.Net.Mail
Imports System.Windows.Forms
Imports System.SqlClient
Imports System.Timers
Imports System.Drawing
Imports Prolis.UI
Imports Microsoft.Data.SqlClient
Imports Microsoft.Office.Interop.Word

Module QCPROCS

    Public ReadOnly IMAGE_FOLDER_PATH As String = Path.Combine(AppContext.BaseDirectory, "Images\")

    'Public CN As ADODB.Connection
    Public MyLab As New Laboratory
    'Public connString As String = ""
    Public connString As String = ""
    Public LIC As LicenseManager.ProlisLicense
    Public LicDate As Date
    Public Seats As Integer
    Public SEAT As String
    Public DateMask As String
    Private key As String = "<!&/?@*\>#>"
    'Public ProlisDateFormat As System.Windows.Forms.DateTimePickerFormat
    Public WithEvents ALOTIMER As System.Windows.Forms.Timer
    Public WithEvents ALO As AutoLogout
    Public ChoiceTestID As Integer
    Public NFCOLOR As Color
    Public FCOLOR As Color = Color.LightCyan
    Public MaxSize As New Size(CInt(My.Computer.Screen.WorkingArea.Height * 1.38), My.Computer.Screen.WorkingArea.Height)
    Private CURMINS As Integer
    '
    Public PanicStyle As New DataGridViewCellStyle
    Public HLAStyle As New DataGridViewCellStyle

    'Public Sub ALO_LogoutUser() Handles ALO.LogoutUser
    '    'frmDashboard.btnLogout_Click(Nothing, Nothing)
    'End Sub

    Public Sub ALO_UserActivity() Handles ALO.UserActivity
        ALOTIMER.Stop()
        CURMINS = 0
        ALOTIMER.Start()
    End Sub
    Public Function ConvertToOdbcConnectionString(adoConnStr As String) As String
        Dim uid As String = ""
        Dim pwd As String = ""
        Dim database As String = ""

        ' Split by ; and loop through key=value pairs
        Dim parts = adoConnStr.Split(";"c)
        For Each part In parts
            Dim kv = part.Split("="c)
            If kv.Length = 2 Then
                Dim key = kv(0).Trim().ToLower()
                Dim value = kv(1).Trim()

                Select Case key
                    Case "user id", "uid"
                        uid = value
                    Case "password", "pwd"
                        pwd = value
                    Case "initial catalog", "database"
                        database = value
                End Select
            End If
        Next

        ' Build ODBC DSN connection string
        Return $"DSN=ProlisQC;UID={uid};PWD={pwd};Database={database};"
    End Function

    Public Sub InsertChargesStatusHistory(connectionString As String, accessionID As Integer, chargeID As Integer, dateTime As DateTime, userName As String)
        Dim query As String = "INSERT INTO ChargesStatus_history (Accession_ID, Charge_ID, Date_Time, UserName) VALUES (@Accession_ID, @Charge_ID, @Date_Time, @UserName)"
        connectionString = connString
        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Accession_ID", accessionID)
                command.Parameters.AddWithValue("@Charge_ID", chargeID)
                command.Parameters.AddWithValue("@Date_Time", dateTime)
                command.Parameters.AddWithValue("@UserName", userName)

                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub
    Public Function IsValidFilePath(filePath As String) As Boolean
        If String.IsNullOrWhiteSpace(filePath) Then
            Return False
        End If

        ' Check if the path is a valid file path
        Try
            Dim pathRoot As String = Path.GetPathRoot(filePath)
            If String.IsNullOrWhiteSpace(pathRoot) Then
                Return False
            End If

            ' Check if the file exists
            Return File.Exists(filePath)
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Sub ALOTIMER_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ALOTIMER.Tick
        Dim LOGOUTMINS As Integer = ThisUser.LogoutMins
        CURMINS += 1
        If CURMINS >= LOGOUTMINS Then
            frmDashboard.btnLogout_Click(Nothing, Nothing)
        End If
    End Sub

    Public Function CleanIt(ByVal Str As String) As String
        If Str <> "" Then
            Str = Replace(Str, "(", "") : Str = Replace(Str, ")", "")
            Str = Replace(Str, "-", "") : Str = Replace(Str, " ", "")
        End If
        Return Str
    End Function

    Public Function GetPatientPCoverage(ByVal PatientID As Long) As String()
        Dim COV() As String = {"", "", "", "", "", ""}  '0=PayerID, 1=GroupNo, 2=PolicyNo, 3=Rel, 4=InsuredID
        Dim cnppc As New SqlConnection(connString)
        cnppc.Open()
        Dim cmdppc As New SqlCommand("Select * from Coverages " &
        "where Preference = 'P' and Patient_ID = " & PatientID, cnppc)
        cmdppc.CommandType = CommandType.Text
        Dim drppc As SqlDataReader = cmdppc.ExecuteReader
        If drppc.HasRows Then
            While drppc.Read
                COV(0) = drppc("Insurance_ID")
                If drppc("GroupNo") IsNot DBNull.Value _
                AndAlso Trim(drppc("GroupNo")) <> "" Then _
                COV(1) = Trim(drppc("GroupNo"))
                COV(2) = Trim(drppc("PolicyNo"))
                COV(3) = drppc("Relation").ToString
                COV(4) = drppc("Insured_ID").ToString
            End While
        End If
        cnppc.Close()
        cnppc = Nothing
        Return COV
    End Function

    Public Function GetStateCode(ByVal State As String) As String
        Dim Code As String = ""
        If State = "Alaska" Or State = "AK" Then
            Code = "AK"
        ElseIf State = "Alabama" Or State = "AL" Then
            Code = "AL"
        ElseIf State = "Arkansas" Or State = "AR" Then
            Code = "AR"
        ElseIf State = "Arizona" Or State = "AZ" Then
            Code = "AZ"
        ElseIf State = "California" Or State = "CA" Then
            Code = "CA"
        ElseIf State = "Colorado" Or State = "CO" Then
            Code = "CO"
        ElseIf State = "Connecticut" Or State = "CT" Then
            Code = "CT"
        ElseIf State = "District of Columbia" Or State = "DC" Then
            Code = "DC"
        ElseIf State = "Delaware" Or State = "DE" Then
            Code = "DE"
        ElseIf State = "Florida" Or State = "FL" Then
            Code = "FL"
        ElseIf State = "Georgia" Or State = "GA" Then
            Code = "GA"
        ElseIf State = "Guam" Or State = "GU" Then
            Code = "GU"
        ElseIf State = "Hawai" Or State = "HI" Then
            Code = "HI"
        ElseIf State = "IOWA" Or State = "IA" Then
            Code = "IA"
        ElseIf State = "IDAHO" Or State = "ID" Then
            Code = "ID"
        ElseIf State = "ILLINOIS" Or State = "IL" Then
            Code = "IL"
        ElseIf State = "Indiana" Or State = "IN" Then
            Code = "IN"
        ElseIf State = "Kansas" Or State = "KS" Then
            Code = "KS"
        ElseIf State = "Kentucky" Or State = "KY" Then
            Code = "KY"
        ElseIf State = "Louisiana" Or State = "LA" Then
            Code = "LA"
        ElseIf State = "Massachusetts" Or State = "MA" Then
            Code = "MA"
        ElseIf State = "Maryland" Or State = "MD" Then
            Code = "MD"
        ElseIf State = "Maine" Or State = "ME" Then
            Code = "ME"
        ElseIf State = "Michigan" Or State = "MI" Then
            Code = "MI"
        ElseIf State = "Minnesota" Or State = "MN" Then
            Code = "MN"
        ElseIf State = "Missouri" Or State = "MO" Then
            Code = "MO"
        ElseIf State = "Mississippi" Or State = "MS" Then
            Code = "MS"
        ElseIf State = "Montana" Or State = "MT" Then
            Code = "MT"
        ElseIf State = "North Carolina" Or State = "NC" Then
            Code = "NC"
        ElseIf State = "North Dakota" Or State = "ND" Then
            Code = "ND"
        ElseIf State = "Nebraska" Or State = "NE" Then
            Code = "NE"
        ElseIf State = "New Hampshire" Or State = "NH" Then
            Code = "NH"
        ElseIf State = "New Jersey" Or State = "NJ" Then
            Code = "NJ"
        ElseIf State = "New Mexico" Or State = "NM" Then
            Code = "NM"
        ElseIf State = "Nevada" Or State = "NV" Then
            Code = "NV"
        ElseIf State = "New York" Or State = "NY" Then
            Code = "NY"
        ElseIf State = "Ohio" Or State = "OH" Then
            Code = "OH"
        ElseIf State = "Oklahoma" Or State = "OK" Then
            Code = "OK"
        ElseIf State = "Oregon" Or State = "OR" Then
            Code = "OR"
        ElseIf State = "Pennsylvania" Or State = "PA" Then
            Code = "PA"
        ElseIf State = "Puerto Rico" Or State = "PR" Then
            Code = "PR"
        ElseIf State = "Rhode Island" Or State = "RI" Then
            Code = "RI"
        ElseIf State = "South Carolina" Or State = "SC" Then
            Code = "SC"
        ElseIf State = "South Dakota" Or State = "SD" Then
            Code = "SD"
        ElseIf State = "Tenessee" Or State = "TN" Then
            Code = "TN"
        ElseIf State = "Texas" Or State = "TX" Then
            Code = "TX"
        ElseIf State = "Utah" Or State = "UT" Then
            Code = "UT"
        ElseIf State = "Virginia" Or State = "VA" Then
            Code = "VA"
        ElseIf State = "Virgin Island" Or State = "VI" Then
            Code = "VI"
        ElseIf State = "Vermont" Or State = "VT" Then
            Code = "VT"
        ElseIf State = "Washington" Or State = "WA" Then
            Code = "WA"
        ElseIf State = "Wisconsin" Or State = "WI" Then
            Code = "WI"
        ElseIf State = "West Virginia" Or State = "WV" Then
            Code = "WV"
        ElseIf State = "Wyoming" Or State = "WY" Then
            Code = "WY"
        End If
        Return UCase(Code)
    End Function

    Public Function GetNextPaymentID() As Long
        Dim NPID As Long = 1
        Dim cnpid As New SqlConnection(connString)
        cnpid.Open()
        Dim cmdpid As New SqlCommand("Select max(ID) as LastID from Payments", cnpid)
        cmdpid.CommandType = CommandType.Text
        Dim drpid As SqlDataReader = cmdpid.ExecuteReader
        If drpid.HasRows Then
            While drpid.Read
                If drpid("LastID") Is DBNull.Value Then
                    NPID = 1
                Else
                    NPID = drpid("LastID") + 1
                End If
            End While
        End If
        cnpid.Close()
        cnpid = Nothing
        Return NPID
    End Function

    Public Sub InitializeMylab(ByVal CompanyID As Long)
        'Dim MyLab As Laboratory
        Dim cniml As New SqlConnection(connString)
        cniml.Open()
        Dim cmdiml As New SqlCommand("Select * from Company c join system_config s on c.id = s.Company_ID   where ID = " & CompanyID, cniml)
        cmdiml.CommandType = CommandType.Text
        Dim driml As SqlDataReader = cmdiml.ExecuteReader
        MyLab.QRSec = New QRSec
        If driml.HasRows Then
            While driml.Read
                MyLab.ID = driml("ID")
                Try
                    MyLab.QRSec.Token = driml("QR_Token")
                    MyLab.QRSec.DNS = driml("QR_DNS")
                    Try
                        MyLab.QRSec.QRChecked = driml("QR_Checked")
                    Catch ex As Exception

                    End Try

                Catch ex As Exception

                End Try
                'colorapi
                Try
                    Dim tokn = driml("ColorToken")
                    Dim Url = driml("ColorBaseUrl")
                    MyLab.ColorAPIStruct.Token_BaseUrl = tokn & "|" & Url
                    If String.IsNullOrEmpty(tokn) Or String.IsNullOrEmpty(Url) Then
                        ColorAPI.Active = False
                    Else
                        ColorAPI.Active = True
                    End If
                    Dim colorchk = driml("UseColorApi")
                    If colorchk = 1 Then
                        ColorAPI.Active = True
                    ElseIf colorchk = 0 Then
                        ColorAPI.Active = False
                    End If

                Catch ex As Exception
                    ColorAPI.Active = False
                End Try
                MyLab.IsIndividual = driml("IsIndividual")
                If driml("IsIndividual") = 0 Then
                    MyLab.LabName = driml("LastName_BSN")
                Else
                    MyLab.LabName = driml("LastName_BSN") & ", " & driml("FirstName")
                    If driml("Degree") IsNot DBNull.Value _
                    AndAlso driml("Degree") <> "" Then
                        MyLab.LabName += " " & driml("Degree")
                    End If
                End If
                If driml("License") IsNot DBNull.Value Then MyLab.License = driml("License")
                If driml("NPI") IsNot DBNull.Value Then MyLab.NPI = driml("NPI")
                If driml("Phone") IsNot DBNull.Value Then MyLab.Phone = driml("Phone")
            End While
        End If
        cniml.Close()
        cniml = Nothing
        '
        ExecuteSqlProcedure("Update System_Config Set DiagTarget = '" &
        LIC.DiagTarget & "' where Company_ID = " & CompanyID)
        '
    End Sub

    Public Function ConvertBase64ToImage(ByVal base64 As String) As Image
        Dim BA As Byte() = Convert.FromBase64String(base64) 'Convert the base64 back to byte array.
        Dim ms As MemoryStream = New MemoryStream(BA)
        Dim image = System.Drawing.Image.FromStream(ms)
        Return image
    End Function


    Public Function CompressByte(ByVal fileBytes As Byte()) As Byte()
        'Dim doc As New Spire.Pdf.PdfDocument()
        'Using Stream = New MemoryStream(fileBytes)
        '    doc.LoadFromStream(Stream)
        'End Using

        ''Compress the content in document
        'doc.FileInfo.IncrementalUpdate = False
        ''Set the compression level to best
        'doc.CompressionLevel = Spire.Pdf.PdfCompressionLevel.Best

        ''Compress the images in document
        'doc.FileInfo.IncrementalUpdate = False
        ''Traverse all pages
        'For Each page As Spire.Pdf.PdfPageBase In doc.Pages
        '    If page IsNot Nothing Then
        '        If page.ImagesInfo IsNot Nothing Then
        '            For Each info As Spire.Pdf.Exporting.PdfImageInfo In page.ImagesInfo
        '                page.TryCompressImage(info.Index)
        '            Next info
        '        End If
        '    End If
        'Next page

        'Dim fileTo() As Byte
        'Using to_stream = New System.IO.MemoryStream()
        '    doc.SaveToStream(to_stream)
        '    fileTo = to_stream.ToArray()
        'End Using

        'Return fileTo
    End Function

    Public Function GetPatientEmailFromInvoice(ByVal InvID As Long) As String
        Dim Email As String = ""
        Dim sSQL As String = "Select IsNull(a.Email, '') as Email from Patients a inner " &
        "join Charges b on b.Ar_ID = a.ID where b.ArType = 2 and b.ID = " & InvID
        Dim cnpe As New SqlConnection(connString)
        cnpe.Open()
        Dim cmdpe As New SqlCommand(sSQL, cnpe)
        cmdpe.CommandType = CommandType.Text
        Dim drpe As SqlDataReader = cmdpe.ExecuteReader
        If drpe.HasRows Then
            While drpe.Read
                Email = drpe("Email")
            End While
        End If
        cnpe.Close()
        cnpe = Nothing
        Return Email
    End Function

    Public Function Reflexable(ByVal AccID As Long, ByVal ReflexedID As String, ByVal ReflexerID As String) As Boolean
        Dim Able As Boolean = False
        Dim sSQL As String = "Select Test_ID as TGPID from Acc_Results where Accession_ID = " & AccID &
        " and Test_ID = " & Val(ReflexedID) & " Union Select TGP_ID as TGPID from Req_TGP where " &
        "Accession_ID = " & AccID & " and TGP_ID = " & Val(ReflexedID) & " Union Select Reflexed_ID " &
        "as TGPID from Ref_Results where Accession_ID = " & AccID & " and Reflexed_ID = " & Val(ReflexedID)
        Dim cnr As New SqlConnection(connString)
        cnr.Open()
        Dim cmdr As New SqlCommand(sSQL, cnr)
        cmdr.CommandType = CommandType.Text
        Dim drr As SqlDataReader = cmdr.ExecuteReader
        If Not drr.HasRows Then Able = True
        cnr.Close()
        cnr = Nothing
        '
        sSQL = "Select ReflexMulti from C_Triggers where Test_ID = " & ReflexerID & " and Marked_ID = " & Val(ReflexedID) &
        " Union Select ReflexMulti from N_Triggers where Test_ID = " & ReflexerID & " and Marked_ID = " & Val(ReflexedID)
        Dim cnt As New SqlConnection(connString)
        cnt.Open()
        Dim cmdt As New SqlCommand(sSQL, cnt)
        cmdt.CommandType = CommandType.Text
        Dim drt As SqlDataReader = cmdt.ExecuteReader
        If drt.HasRows Then
            While drt.Read
                If drt("ReflexMulti") = False Then
                    If Able = True Then Able = False
                Else    'multireflexing
                    Able = True
                End If
            End While
        End If
        cnt.Close()
        cnt = Nothing
        '
        Return Able
    End Function

    Public Function GetSalesPersonID(ByVal ProviderID As Long) As Long
        Dim SPID As Long = Nothing
        Dim sSQL As String = "Select SalesPerson_ID as SPID from Providers where ID = " & ProviderID
        If connString <> "" Then
            Dim cnsp As New SqlConnection(connString)
            cnsp.Open()
            Dim cmdsp As New SqlCommand(sSQL, cnsp)
            cmdsp.CommandType = CommandType.Text
            Dim DRsp As SqlDataReader = cmdsp.ExecuteReader
            If DRsp.HasRows Then
                While DRsp.Read
                    If DRsp("SPID") IsNot DBNull.Value _
                    Then SPID = DRsp("SPID")
                End While
            End If
            cnsp.Close()
            cnsp = Nothing
        Else
            Dim cnsp As New sqlConnection(connString)
            cnsp.Open()
            Dim cmdsp As New sqlCommand(sSQL, cnsp)
            cmdsp.CommandType = CommandType.Text
            Dim DRsp As sqlDataReader = cmdsp.ExecuteReader
            If DRsp.HasRows Then
                While DRsp.Read
                    If DRsp("SPID") IsNot DBNull.Value _
                    Then SPID = DRsp("SPID")
                End While
            End If
            cnsp.Close()
            cnsp = Nothing
        End If
        Return SPID
    End Function

    Public Sub PopulateRaces(ByRef cmbrace As ComboBox)
        cmbrace.Items.Clear()
        Dim sSQL As String = "Select * from Races"
        '
        Dim cnpr As New SqlConnection(connString)
        cnpr.Open()
        Dim cmdpr As New SqlCommand(sSQL, cnpr)
        cmdpr.CommandType = CommandType.Text
        Dim drpr As SqlDataReader = cmdpr.ExecuteReader
        If drpr.HasRows Then
            While drpr.Read
                cmbrace.Items.Add(New MyList(drpr("Race"), drpr("ID")))
            End While
        End If
        cnpr.Close()
        cnpr = Nothing
    End Sub


    Public Function ExecuteSqlProcedure(ByVal sSql As String) As String
        Dim RetStr As String = ""
        'sSql = Replace(sSql, "'", "''")
        'sSql = Replace(sSql, "/", "//")
        'sSql = Replace(sSql, "\", "\\")
        'sSql = Replace(sSql, "&", "&&")
        Dim cnx As New SqlConnection(connString)
        cnx.Open()
        Dim cmdx As New SqlCommand(sSql, cnx)
        cmdx.CommandType = CommandType.Text
        Try
            cmdx.ExecuteNonQuery()
        Catch ex As Exception
            RetStr = ex.Message
        Finally
            cnx.Close()
            cnx = Nothing
        End Try
        Return RetStr
    End Function

    Public Function ExecuteSqlProcedureWithTransaction(ByVal sSql As String, ByVal cnx As SqlConnection, ByVal transaction As SqlTransaction) As String
        Dim RetStr As String = ""
        Dim cmdx As New SqlCommand(sSql, cnx, transaction)
        cmdx.CommandType = CommandType.Text
        Try
            cmdx.ExecuteNonQuery()
        Catch ex As Exception
            RetStr = ex.Message
            Throw
        End Try
        Return RetStr
    End Function

    Public Sub AdjustAppliedPayment(ByVal ChargeID As Long, ByVal TGPID As Integer, ByVal paid As Single, ByVal PR As Single)
        Dim cnadj As New SqlConnection(connString)
        cnadj.Open()
        Dim cmdadj As New SqlCommand("Select * from Payment_Detail " &
        "where Charge_ID = " & ChargeID & " and TGP_ID = " & TGPID, cnadj)
        cmdadj.CommandType = CommandType.Text
        Dim dradj As SqlDataReader = cmdadj.ExecuteReader
        If dradj.HasRows Then
            While dradj.Read
                If dradj("AppliedAmount") + paid = 0 Then  'delete
                    ExecuteSqlProcedure("Delete from Charge_Detail where " &
                    "Charge_ID = " & ChargeID & " and TGP_ID = " & TGPID)
                Else    'partial adjustment
                    ExecuteSqlProcedure("Update Charge_Detail set AppliedAmount = " &
                    Math.Round(dradj("AppliedAmount") + paid, 2) & ", WrittenOff = " &
                    "WrittenOff + " & Math.Round(dradj("AppliedAmount") + paid, 2) &
                    " where Charge_ID = " & ChargeID & " and TGP_ID = " & TGPID)
                End If
                '
                If PR < 0 Then  'delete secondary 
                    ExecuteSqlProcedure("Delete from Charge_Detail where Charge_ID in (Select ID from " &
                    "Charges where IsPrimary = 0 and ArType = 2 and Accession_ID in (Select Accession_ID " &
                    "from Charges where ID = " & ChargeID & ")) and TGP_ID = " & TGPID)
                End If
            End While
        End If
        cnadj.Close()
        cnadj = Nothing
    End Sub

    Public Sub UpdatePayments(ByVal ArType As Int16, ByVal ArID As Long, ByVal DocNo As String, ByVal EOBDate As Date,
    ByVal EOBAmount As Single, ByVal AccID As String, ByVal ChargeID As Long, ByVal PrClmID As String, ByVal TGPID As _
    Integer, ByVal ChAmt As Single, ByVal PmtAmt As Single, ByVal WO As Single, ByVal RBAmt As Single, ByVal RemCode As String)
        Dim PmtID As Long = -1
        Dim sSQL As String = "Select * from Payments where ArType = " &
        ArType & " and ar_ID = " & ArID & " and DocNo = '" & DocNo & "'"
        Dim cnup As New SqlConnection(connString)
        cnup.Open()
        Dim cmdup As New SqlCommand(sSQL, cnup)
        cmdup.CommandType = CommandType.Text
        Dim drup As SqlDataReader = cmdup.ExecuteReader
        If drup.HasRows Then
            While drup.Read
                PmtID = drup("ID")
            End While
        Else
            PmtID = GetNextPaymentID()
            ExecuteSqlProcedure("Insert into Payments (ID, ArType, Ar_ID, DocNo, PaymentType, PaymentDate, Accession_ID, " &
            "Amount, UnApplied, CC_ID, LastEditedOn, EditedBy) values (" & PmtID & ", " & ArType & ", " & ArID & ", '" & DocNo &
            "', 1, '" & EOBDate & "', " & AccID & ", " & EOBAmount & ", 0, null, '" & Date.Now & "', " & ThisUser.ID & ")")
        End If
        cnup.Close()
        cnup = Nothing
        '
        RemCode = Trim(RemCode)
        If Microsoft.VisualBasic.Right(RemCode, 1) = "," Then RemCode = Microsoft.VisualBasic.Mid(RemCode, 1, Len(RemCode) - 1)
        sSQL = "If Exists (Select * from Payment_Detail where Payment_ID = " & PmtID & " and Charge_ID = " & ChargeID &
        " and TGP_ID = " & TGPID & ") Update Payment_Detail set Ordinal = 0, PayerClaimID = '" & Trim(PrClmID) & "', " &
        "ChargeAmount = " & ChAmt & ", AppliedAmount = " & PmtAmt & ", WrittenOff = " & WO & ", RebillAmount = " & RBAmt &
        ", Rem_Code = '" & RemCode & "' where Payment_ID = " & PmtID & " and Charge_ID = " & ChargeID & " and TGP_ID = " &
        TGPID & " Else Insert into Payment_Detail (Payment_ID, Charge_ID, TGP_ID, Ordinal, PayerClaimID, ChargeAmount, " &
        "Balance, AppliedAmount, UnAppliedAmount, WORB, WrittenOff, RebillAmount, DeductibleAmount, CopayAmount, Rem_Code) " &
        "values (" & PmtID & ", " & ChargeID & ", " & TGPID & ", 0, '" & Trim(PrClmID) & "', " & ChAmt & ", 0, " & PmtAmt &
        ", 0, 0, " & WO & ", " & RBAmt & ", 0, 0, '" & RemCode & "')"
        ExecuteSqlProcedure(sSQL)
        '
        'sSQL = "If Exists (Select * from Refunds where Charge_ID = " & ChargeID & ") Update Refunds set Amount = (Select Sum(Amount) " & _
        '"from Refund_Detail where Charge_ID = " & ChargeID & ") where Charge_ID = " & ChargeID & " Else Insert into Refunds (Charge_ID, " & _
        '"DocNo, RefundType, RefundDate, Amount, OutCheckNo, Reason, LastEditedOn, EditedBy) values (" & ChargeID & ", '" & Trim(DocNo) & _
        '"', 1, '" & RefundDate & "', " & Trim(PrClmID) & "', " & ChAmt & ", '', '" & RemCode & "', '" & Date.Now & "', " & ThisUser.ID & ")"
        'ExecuteSqlProcedure(sSQL)
    End Sub

    Public Function GetAccIDfromChargeID(ByVal ChargeID As Long) As String
        Dim AccID As String = ""
        Dim sSQL As String = "Select Accession_ID from Charges where ID = " & ChargeID
        '
        Dim cnafc As New SqlConnection(connString)
        cnafc.Open()
        Dim cmdafc As New SqlCommand(sSQL, cnafc)
        cmdafc.CommandType = CommandType.Text
        Dim drafc As SqlDataReader = cmdafc.ExecuteReader
        If drafc.HasRows Then
            While drafc.Read
                AccID = drafc("Accession_ID").ToString
            End While
        End If
        cnafc.Close()
        cnafc = Nothing
        Return AccID
    End Function

    Public Function GetERACodeAction(ByVal Code As String) As String
        Dim Act As String = ""
        If Trim(Code).EndsWith(",") Then Code = Code.Substring(0, InStr(Trim(Code), ",") - 1)
        Dim sSQL As String = "Select Action from ReasonCodes where ReasonCode = '" & Code & "'"
        If connString <> "" Then
            Dim cnca As New SqlConnection(connString)
            cnca.Open()
            Dim cmdca As New SqlCommand(sSQL, cnca)
            cmdca.CommandType = CommandType.Text
            Dim drca As SqlDataReader = cmdca.ExecuteReader
            If drca.HasRows Then
                While drca.Read
                    Act = Trim(drca("Action"))
                End While
            End If
            cnca.Close()
            cnca = Nothing
        Else
            Dim cnca As New sqlConnection(connString)
            cnca.Open()
            Dim cmdca As New sqlCommand(sSQL, cnca)
            cmdca.CommandType = CommandType.Text
            Dim drca As sqlDataReader = cmdca.ExecuteReader
            If drca.HasRows Then
                While drca.Read
                    Act = Trim(drca("Action"))
                End While
            End If
            cnca.Close()
            cnca = Nothing
        End If
        Return Act
    End Function

    Public Sub UpdateERACodeAction(ByVal Code As String, ByVal Act As String)
        If Trim(Code).EndsWith(",") Then Code = Code.Substring(0, InStr(Trim(Code), ",") - 1)
        Dim sSQL As String = "If Exists (Select * from ReasonCodes where ReasonCode = '" & Code &
        "') update ReasonCodes set Action = '" & Act & "' where ReasonCode = '" & Code & "' Else " &
        "insert into ReasonCodes (ReasonCode, Action) values ('" & Code & "', '" & Act & "')"
        ExecuteSqlProcedure(sSQL)
    End Sub

    'Public Sub ResetSendMail(ByVal frm As String, ByVal proc As String, ByVal msg As String, ByVal email As String, ByVal subject As String)
    '    Dim SmtpServer As New SmtpClient()
    '    Dim mail As New MailMessage()
    '    'SmtpServer.Credentials = New  _
    '    'Net.NetworkCredential("rama@prolis.info", "RamaOR15")
    '    SmtpServer.Credentials = New  _
    '  Net.NetworkCredential("aneet@prolis.info", "Justgotin#10")
    '    SmtpServer.Port = 587
    '    'SmtpServer.EnableSsl = False
    '    SmtpServer.Host = "smtp.readyhosting.com"
    '    mail = New MailMessage()
    '    mail.From = New MailAddress("aneet@prolis.info")
    '    mail.To.Add(email)
    '    mail.Subject = subject
    '    mail.IsBodyHtml = True
    '    mail.Body = msg
    '    ' mail.Body = "The error occured at " & Date.Now & vbCrLf & " Page: " & frm & vbCrLf & "Procedure: " & proc & vbCrLf & msg
    '    Try
    '        SmtpServer.Send(mail)
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Public Sub SendMail(ByVal frm As String, ByVal proc As String, ByVal msg As String)
        Dim SmtpServer As New SmtpClient()
        Dim mail As New MailMessage()
        SmtpServer.Credentials = New _
        Net.NetworkCredential("rama@prolis.info", "RamaOR15")
        SmtpServer.Port = 587
        'SmtpServer.EnableSsl = False
        SmtpServer.Host = "smtp.readyhosting.com"
        mail = New MailMessage()
        mail.From = New MailAddress("prolis@prolis.info")
        mail.To.Add("taimur1ali@gmail.com")
        mail.Subject = "Error in prolis"
        mail.Body = "The error occured at " & Date.Now & vbCrLf &
        LIC.Licensee & vbCrLf & "Screen: " & frm & vbCrLf &
        "Procedure: " & proc & vbCrLf & msg
        SmtpServer.Send(mail)
    End Sub

    Public Function encryptString(ByVal strtext As String) As String
        Return Encrypt(strtext, key)
    End Function

    Public Function decryptString(ByVal strtext As String) As String
        Return Decrypt(strtext, key)
    End Function

    'The function used to encrypt the text
    Private Function Encrypt(ByVal strText As String, ByVal strEncrKey _
             As String) As String
        Dim byKey() As Byte = {}
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}

        Try
            byKey = System.Text.Encoding.UTF8.GetBytes(Left(strEncrKey, 8))

            Dim des As New DESCryptoServiceProvider()
            Dim inputByteArray() As Byte = Encoding.UTF8.GetBytes(strText)
            Dim ms As New MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Return Convert.ToBase64String(ms.ToArray())

        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    'The function used to decrypt the text
    Private Function Decrypt(ByVal strText As String, ByVal sDecrKey _
               As String) As String
        Dim byKey() As Byte = {}
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
        Dim inputByteArray(strText.Length) As Byte

        Try
            byKey = System.Text.Encoding.UTF8.GetBytes(Left(sDecrKey, 8))
            Dim des As New DESCryptoServiceProvider()
            inputByteArray = Convert.FromBase64String(strText)
            Dim ms As New MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write)

            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
            Dim ff = encoding.GetString(ms.ToArray())
            Return encoding.GetString(ms.ToArray())

        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Sub SaveNecessity(ByVal CPT As String, ByVal Dx As String)
        Dim sSQL As String = "If Not Exists (Select * from MedicalNecessity where " &
        "CPT_Code = '" & CPT & "' and Dx_Code = '" & Dx & "') Insert into " &
        "MedicalNecessity (CPT_Code, Dx_Code) values ('" & CPT & "', '" & Dx & "')"
        ExecuteSqlProcedure(sSQL)
    End Sub

    Public Function NextPatientID() As String
        Dim PatientID As Long = 1
        Dim sSQL As String = "Select max(ID) as LastID from Patients"
        '
        Dim cnx As New SqlConnection(connString)
        cnx.Open()
        Dim CMDx As New SqlCommand(sSQL, cnx)
        CMDx.CommandType = CommandType.Text
        Dim DRx As SqlDataReader = CMDx.ExecuteReader
        If DRx.HasRows Then
            While DRx.Read
                If DRx("LastID") IsNot DBNull.Value _
                Then PatientID = DRx("LastID") + 1
            End While
        End If
        cnx.Close()
        cnx = Nothing
        Return PatientID
    End Function

    Public Sub UpdatePatPCoverage(ByVal PatientID As Long, ByVal PayerID As Long, ByVal Policy As String, ByVal _
    Group As String, ByVal Rel As Int16, ByVal InsuredID As Long, ByVal SDate As Date, ByVal TDate As Date)
        '
        ExecuteSqlProcedure("If Exists (Select * from Coverages where Patient_ID = " &
        PatientID & " And Insurance_ID = " & PayerID & ") Update Coverages set " &
        "Relation = " & Rel & ", Insured_ID = " & InsuredID & ", Preference = 'P', " &
        "GroupNo = '" & Trim(Group) & "', PolicyNo = '" & Trim(Policy) & "', " &
        "StartDate = '" & SDate & "', ExpireDate = '" & TDate & "', CoPayment = 0, " &
        "LastEditedOn = '" & Date.Today & "', EditedBy = " & ThisUser.ID & " where " &
        "Patient_ID = " & PatientID & " And Insurance_ID = " & PayerID & " Else " &
        "Insert into Coverages (Patient_ID, Insurance_ID, Relation,  Insured_ID, " &
        "Preference, GroupNo, PolicyNo, StartDate, ExpireDate, CoPayment, " &
        "LastEditedOn, EditedBy) values (" & PatientID & ", " & PayerID & ", " & Rel &
        ", " & InsuredID & ", 'P', '" & Trim(Group) & "', '" & Trim(Policy) & "', '" &
        SDate & "', '" & TDate & "', 0, '" & Date.Now & "', " & ThisUser.ID & ")")
        '
        ExecuteSqlProcedure("Delete from Coverages where Patient_ID = " & PatientID &
        " And Preference = 'P' and Insurance_ID <> " & PayerID)
    End Sub

    Public Sub UpdatePatSCoverage(ByVal PatientID As Long, ByVal PayerID As Long, ByVal Policy As String, ByVal _
    Group As String, ByVal Rel As Int16, ByVal InsuredID As Long, ByVal SDate As Date, ByVal TDate As Date)
        '

        ExecuteSqlProcedure("If Exists (Select * from Coverages where Patient_ID = " &
        PatientID & " And Insurance_ID = " & PayerID & ") Update Coverages set " &
        "Relation = " & Rel & ", Insured_ID = " & InsuredID & ", Preference = 'S', " &
        "GroupNo = '" & Trim(Group) & "', PolicyNo = '" & Trim(Policy) & "', " &
        "StartDate = '" & SDate & "', ExpireDate = '" & TDate & "', CoPayment = 0, " &
        "LastEditedOn = '" & Date.Today & "', EditedBy = " & ThisUser.ID & " where " &
        "Patient_ID = " & PatientID & " And Insurance_ID = " & PayerID & " Else " &
        "Insert into Coverages (Patient_ID, Insurance_ID, Relation,  Insured_ID, " &
        "Preference, GroupNo, PolicyNo, StartDate, ExpireDate, CoPayment, " &
        "LastEditedOn, EditedBy) values (" & PatientID & ", " & PayerID & ", " & Rel &
        ", " & InsuredID & ", 'S', '" & Trim(Group) & "', '" & Trim(Policy) & "', '" &
        SDate & "', '" & TDate & "', 0, '" & Date.Now & "', " & ThisUser.ID & ")")
        '
    End Sub

    Public Function GetPatientIDbyNames(ByVal LName As String, ByVal FName As String, ByVal DOB As Date, ByVal Sex As String) As Long
        Dim PatientID As Long = NextPatientID()
        Dim sSQL As String = "Select * from Patients where LastName = '" & Trim(Replace(LName, "'", "''")) &
        "' and FirstName = '" & Trim(Replace(FName, "'", "''")) & "' and Sex = '" & Trim(Sex) & "' and DOB = '" &
        Format(DOB, SystemConfig.DateFormat) & "'"
        Dim cn1 As New SqlConnection(connString)
        cn1.Open()
        Dim cmdpat As New SqlCommand(sSQL, cn1)
        cmdpat.CommandType = CommandType.Text
        Dim DRsel As SqlDataReader = cmdpat.ExecuteReader
        If DRsel.HasRows Then
            While DRsel.Read
                PatientID = DRsel("ID")
            End While
        End If
        cn1.Close()
        cn1 = Nothing
        'If PatientID = -1 Then
        '    PatientID = NextPatientID()
        '    ExecuteSqlProcedure("Insert into Patients (ID, LastName, FirstName, MiddleName, DOB, Sex, Species_ID, " & _
        '    "Breed_ID, Race_ID) values (" & PatientID & ", '" & Trim(Replace(LName, "'", "''")) & "', '" & _
        '    Trim(Replace(FName, "'", "''")) & "', '', '" & Format(DOB, SystemConfig.DateFormat) & "', '" & _
        '    Trim(Sex) & "', 0, 0, 7)")
        'End If
        Return PatientID
    End Function

    Public Function IsPWDValid(ByVal PWD As String) As Boolean
        Dim VALS() As String = {"0", "0", "0", "0", "0"}
        '0=length, 1=one cap, 2=one small, 3=Numeric, 4=Special
        'Minimum 8 chars but les or equal to 20
        If PWD.Length >= 8 And PWD.Length <= 20 Then VALS(0) = "1"
        'Minimum 1 cap char 
        Dim CHARS() As Char = PWD.ToCharArray
        For i As Integer = 0 To CHARS.Length - 1
            If Asc(CHARS(i)) >= 65 And Asc(CHARS(i)) <= 90 Then
                VALS(1) = "1"
                Exit For
            End If
        Next
        'Minimum 1 small char 
        For i As Integer = 0 To CHARS.Length - 1
            If Asc(CHARS(i)) >= 97 And Asc(CHARS(i)) <= 122 Then
                VALS(2) = "1"
                Exit For
            End If
        Next
        'Minimum 1 numeric char 
        For i As Integer = 0 To CHARS.Length - 1
            If Asc(CHARS(i)) >= 48 And Asc(CHARS(i)) <= 57 Then
                VALS(3) = "1"
                Exit For
            End If
        Next
        'Minimum 1 special char 
        For i As Integer = 0 To CHARS.Length - 1
            If InStr("!@#$%^&*()", CHARS(i)) > 0 Then
                VALS(4) = "1"
                Exit For
            End If
        Next
        If VALS(0) = "1" And VALS(1) = "1" And
        VALS(2) = "1" And VALS(3) = "1" And VALS(4) = "1" Then
            IsPWDValid = True
        Else
            IsPWDValid = False
        End If
    End Function

    Public Sub UpdateRPTDestination(ByVal ProvID As Long, ByVal AccID As Long, ByVal Src As String,
    ByVal Media As String, ByVal MedSup As String, ByVal EventID As Integer)
        'Media = Print, Prolison, Fax, Email, Interface
        'MedSup = Fax No, Email ID
        Dim cnur As New SqlConnection(connString)
        cnur.Open()
        Dim cmdupsert As New SqlCommand("Req_RPT_SP", cnur)
        cmdupsert.CommandType = CommandType.StoredProcedure
        cmdupsert.Parameters.AddWithValue("@act", "Upsert")
        cmdupsert.Parameters.AddWithValue("@Provider_ID", ProvID)
        cmdupsert.Parameters.AddWithValue("@Base_ID", AccID)
        cmdupsert.Parameters.AddWithValue("@EntrySource", Src)
        cmdupsert.Parameters.AddWithValue("@RPT_Type", "ACC")
        cmdupsert.Parameters.AddWithValue("@EntryDate", Date.Now)
        cmdupsert.Parameters.AddWithValue("@Ordinal", 0)
        cmdupsert.Parameters.AddWithValue("@RDM_Auto", 0)
        If Media = "Prolison" Then
            cmdupsert.Parameters.AddWithValue("@RPT_Prolison", 1)
        ElseIf Media = "Interface" Then
            cmdupsert.Parameters.AddWithValue("@RPT_Interface", 1)
        ElseIf Media = "Print" Then
            cmdupsert.Parameters.AddWithValue("@RPT_Print", 1)
        ElseIf Media = "Fax" AndAlso MedSup <> "" Then
            cmdupsert.Parameters.AddWithValue("@RPT_Fax", 1)
            cmdupsert.Parameters.AddWithValue("@Fax", MedSup)
        ElseIf Media = "Email" AndAlso MedSup <> "" Then
            cmdupsert.Parameters.AddWithValue("@RPT_Email", 1)
            cmdupsert.Parameters.AddWithValue("@Email", MedSup)
        End If
        cmdupsert.Parameters.AddWithValue("@Priority", 1)
        cmdupsert.Parameters.AddWithValue("@Executed", 0)
        cmdupsert.Parameters.AddWithValue("@Executor", Nothing)
        cmdupsert.Parameters.AddWithValue("@ExecutedOn", Nothing)
        cmdupsert.Parameters.AddWithValue("@Comment", "")
        Try
            cmdupsert.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cnur.Close()
            cnur = Nothing
        End Try
        '
        ExecuteSqlProcedure("Delete from Event_Capture where " &
        "Event_ID = " & EventID & " and Accession_ID = " & AccID)
    End Sub

    Public Function TESTinTESTS(ByVal Test As String, ByVal TESTS() As String) As Boolean
        Dim TESTIN As Boolean = False
        For i As Integer = 0 To UBound(TESTS)
            If Test = TESTS(i) Then
                TESTIN = True
                Exit For
            End If
        Next
        Return TESTIN
    End Function

    Public Function AddTESTinTESTS(ByVal TEST As String, ByVal TESTS() As String) As String()
        If TESTS(UBound(TESTS)) <> "" Then ReDim Preserve TESTS(UBound(TESTS) + 1)
        TESTS(UBound(TESTS)) = TEST
        Return TESTS
    End Function

    Public Function TGPinTGPS(ByVal TGP As String, ByVal TGPs(,) As String) As Boolean
        Dim TGPIN As Boolean = False
        For i As Integer = 0 To UBound(TGPs, 2)
            If TGP = TGPs(0, i) Then
                TGPIN = True
                Exit For
            End If
        Next
        Return TGPIN
    End Function

    Public Function AddTGPinTGPS(ByVal TGP As String, ByVal Stat As Boolean, ByVal TGPS(,) As String) As String(,)
        If TGPS(0, UBound(TGPS, 2)) <> "" Then ReDim Preserve TGPS(1, UBound(TGPS, 2) + 1)
        TGPS(0, UBound(TGPS, 2)) = TGP
        If Stat = Nothing Then
            TGPS(1, UBound(TGPS, 2)) = 0
        Else
            TGPS(1, UBound(TGPS, 2)) = Convert.ToInt16(Stat).ToString
        End If
        Return TGPS
    End Function

    '' **** Start of Main function ***************
    'Public Sub ApplyBillingRules(ByVal AccID As Long)
    '    Dim IsThere As Boolean = False
    '    Dim Existings() As Integer
    '    Dim i As Integer
    '    Dim n As Integer
    '    Dim sSQL As String = "Select TGPsFrom, TGPTo from Payer_BillRules where Payer_ID in (Select " & _
    '    "PrimePayer_ID from Requisitions where BillingType_ID = 1 and ID = " & AccID & ")"
    '    Dim cnabr As New SqlConnection(connString)
    '    cnabr.Open()
    '    Dim cmdabr As New SqlCommand(sSQL, cnabr)
    '    cmdabr.CommandType = CommandType.Text
    '    Dim drabr As SqlDataReader = cmdabr.ExecuteReader
    '    If drabr.HasRows Then
    '        While drabr.Read
    '            If (drabr("TGPsFrom") IsNot DBNull.Value AndAlso drabr("TGPsFrom") <> "") _
    '            And (drabr("TGPTo") IsNot DBNull.Value AndAlso drabr("TGPTo") <> "") Then
    '                Dim TGPSF As String = Trim(drabr("TGPsFrom"))
    '                Dim TGPT As String = Trim(drabr("TGPTo"))
    '                If TGPSF.StartsWith("|") Then TGPSF = Microsoft.VisualBasic.Mid(TGPSF, 2)
    '                If TGPSF.EndsWith("|") Then TGPSF = Microsoft.VisualBasic.Mid(TGPSF, 1, Len(TGPSF) - 1)
    '                Dim FTGPS() As String = Split(TGPSF, "|")
    '                Dim Billables() As String = GetTPBillableTGPS(AccID)
    '                If Billables(0) <> "" Then
    '                    ReDim Existings(FTGPS.Length - 1)
    '                    For i = 0 To FTGPS.Length - 1
    '                        'Existings(i) = 0
    '                        For n = 0 To Billables.Length - 1
    '                            If FTGPS(i) = Billables(n) Then
    '                                Existings(i) = 1
    '                                Exit For
    '                            End If
    '                        Next
    '                    Next
    '                    IsThere = True
    '                    For i = 0 To Existings.Length - 1
    '                        If Existings(i) = 0 Then
    '                            IsThere = False
    '                        End If
    '                    Next
    '                    If IsThere = True Then SwappTGPsToTGPT(AccID, FTGPS, TGPT)
    '                End If
    '            End If
    '        End While
    '    End If
    '    cnabr.Close()
    '    cnabr = Nothing
    'End Sub

    Public Function GetDefaultDirectorID() As String
        Dim DirID As String = ""
        Dim cndid As New SqlConnection(connString)
        cndid.Open()
        Dim cmddid As New SqlCommand("Select ID as " &
        "DirID from Lab_Directors where IsDefault <> 0 and " &
        "(EffectiveTo is NULL or EffectiveTo < '" &
        Format(Date.Now, SystemConfig.DateFormat) & "')", cndid)
        cmddid.CommandType = CommandType.Text
        Dim drdid As SqlDataReader = cmddid.ExecuteReader
        If drdid.HasRows Then
            While drdid.Read
                DirID = drdid("DirID").ToString
            End While
        End If
        cndid.Close()
        cndid = Nothing
        Return DirID
    End Function

    Public Sub Wait(ByVal Secs As Integer)
        Dim Started As Date = Date.Now
        Do Until Date.Now >= DateAdd(DateInterval.Second, Secs, Started)
            My.Application.DoEvents()
        Loop
    End Sub

    Public Function UpdateRefResults(ByVal AccID As Long, ByRef ReflexerIDs() As String, ByVal TestID As Integer,
     ByVal Result As String, ByVal Flag As String, ByVal NormalRange As String, ByVal Note As String, ByVal RTF As String,
     ByVal Status As String, ByVal UOM As String, ByVal LabID As String, ByVal Release As Boolean) As Boolean
        Dim ReflexedIDs() As String
        Dim RetVal As Integer
        Dim Done As Boolean = False
        Dim IsDirty As Boolean = False
        '
        For i As Integer = 0 To ReflexerIDs.Length - 1
            If ReflexerIDs(i) <> "" Then
                ReflexedIDs = GetConfigAllReflexedIDs(ReflexerIDs(i))
                If Trim(Status) = "C" Then StoreResultInHistory(AccID, ReflexerIDs(i), TestID, Date.Now, "", ThisUser.ID)
                For n As Integer = 0 To ReflexedIDs.Length - 1
                    If ReflexedIDs(n) <> "" Then
                        Dim cnacc As New SqlConnection(connString)
                        cnacc.Open()
                        Dim cmdacc As New SqlCommand("UpdateRefResults_SP", cnacc)
                        cmdacc.CommandType = CommandType.StoredProcedure
                        cmdacc.Parameters.AddWithValue("@AccID", AccID)
                        cmdacc.Parameters.AddWithValue("@Reflexer_ID", ReflexerIDs(i))
                        cmdacc.Parameters.AddWithValue("@Reflexed_ID", ReflexedIDs(n))
                        cmdacc.Parameters.AddWithValue("@TestID", TestID)
                        cmdacc.Parameters.AddWithValue("@Result", Result)
                        cmdacc.Parameters.AddWithValue("@Flag", Flag)
                        cmdacc.Parameters.AddWithValue("@NormalRange", NormalRange)
                        cmdacc.Parameters.AddWithValue("@Comment", Note)
                        cmdacc.Parameters.AddWithValue("@TResult", RTF)
                        cmdacc.Parameters.AddWithValue("@UOM", UOM)
                        cmdacc.Parameters.AddWithValue("@LabID", LabID)
                        cmdacc.Parameters.AddWithValue("@Released", Release)
                        If Release = True Then
                            cmdacc.Parameters.AddWithValue("@ReleaseTime", Date.Now)
                            cmdacc.Parameters.AddWithValue("@UID", ThisUser.ID)
                        Else
                            cmdacc.Parameters.AddWithValue("@ReleaseTime", DBNull.Value)
                            cmdacc.Parameters.AddWithValue("@UID", DBNull.Value)
                        End If
                        Try
                            RetVal = cmdacc.ExecuteNonQuery()
                            If RetVal = 1 Then Done = True
                        Catch ex As Exception
                        Finally
                            cnacc.Close()
                            cnacc = Nothing
                        End Try
                    End If
                Next
            End If
        Next
        Return Done
    End Function

    Public Function UpdateAccInfoResults(ByVal AccID As Long, ByVal InfoID As Integer, ByVal Result As String,
    ByVal Flag As String, ByVal NormalRange As String, ByVal Note As String, ByVal RTF As String, ByVal Status _
    As String, ByVal UOM As String, ByVal LabID As String, ByVal Release As Boolean) As Boolean
        Dim Done As Boolean = False
        Dim RetVal As Integer
        '
        If Trim(Status) = "C" Then _
        StoreResultInHistory(AccID, -1, InfoID, Date.Now, Result, ThisUser.ID)
        Dim cnn As New SqlConnection(connString)
        cnn.Open()
        Dim selcmd As New SqlCommand("UpdateAccInfoResults_SP", cnn)
        selcmd.CommandType = CommandType.StoredProcedure
        selcmd.Parameters.AddWithValue("@AccID", AccID)
        selcmd.Parameters.AddWithValue("@InfoID", InfoID)
        selcmd.Parameters.AddWithValue("@Result", Result)
        selcmd.Parameters.AddWithValue("@Flag", Flag)
        selcmd.Parameters.AddWithValue("@NormalRange", NormalRange)
        selcmd.Parameters.AddWithValue("@Comment", Note)
        selcmd.Parameters.AddWithValue("@TResult", RTF)
        selcmd.Parameters.AddWithValue("@UOM", UOM)
        selcmd.Parameters.AddWithValue("@LabID", LabID)
        selcmd.Parameters.AddWithValue("@Released", Convert.ToInt16(Release))
        If Release = True Then
            selcmd.Parameters.AddWithValue("@ReleaseTime", Date.Now)
            selcmd.Parameters.AddWithValue("@UID", ThisUser.ID)
        Else
            selcmd.Parameters.AddWithValue("@ReleaseTime", DBNull.Value)
            selcmd.Parameters.AddWithValue("@UID", DBNull.Value)
        End If
        Try
            RetVal = selcmd.ExecuteNonQuery()
            If RetVal = 1 Then Done = True
        Catch ex As Exception
        Finally
            selcmd.Dispose()
            cnn.Close()
            cnn = Nothing
        End Try
        Return Done
    End Function

    Public Function UpdateAccResults(ByVal AccID As Long, ByVal TestID As Integer, ByVal Result As String,
    ByVal Flag As String, ByVal NormalRange As String, ByVal Note As String, ByVal RTF As String, ByVal Status As _
    String, ByVal UOM As String, ByVal LabID As String, ByVal Release As Boolean) As Boolean
        Dim Done As Boolean = False
        Dim RetVal As Integer
        Dim IsDirty As Boolean = False
        Dim CurrRes As String = ""
        Dim ReleaseTime As Date = Nothing
        '
        If Trim(Status) = "C" Then _
        StoreResultInHistory(AccID, -1, TestID, ReleaseTime, CurrRes, ThisUser.ID)
        Dim cnacc As New SqlConnection(connString)
        cnacc.Open()
        Dim cmdacc As New SqlCommand("UpdateAccResults", cnacc)
        cmdacc.CommandType = CommandType.StoredProcedure
        cmdacc.Parameters.AddWithValue("@AccID", AccID)
        cmdacc.Parameters.AddWithValue("@TestID", TestID)
        cmdacc.Parameters.AddWithValue("@Result", Result)
        cmdacc.Parameters.AddWithValue("@Flag", Flag)
        cmdacc.Parameters.AddWithValue("@NormalRange", NormalRange)
        cmdacc.Parameters.AddWithValue("@Comment", Note)
        cmdacc.Parameters.AddWithValue("@TResult", RTF)
        cmdacc.Parameters.AddWithValue("@UOM", UOM)
        cmdacc.Parameters.AddWithValue("@LabID", LabID)
        cmdacc.Parameters.AddWithValue("@Released", Release)
        If Release = True Then
            cmdacc.Parameters.AddWithValue("@ReleaseTime", Date.Now)
            cmdacc.Parameters.AddWithValue("@UID", ThisUser.ID)
        Else
            cmdacc.Parameters.AddWithValue("@ReleaseTime", DBNull.Value)
            cmdacc.Parameters.AddWithValue("@UID", DBNull.Value)
        End If
        Try
            RetVal = cmdacc.ExecuteNonQuery()
            If RetVal = 1 Then Done = True
        Catch ex As Exception
        Finally
            cnacc.Close()
            cnacc = Nothing
        End Try
        Return Done
    End Function

    Public Function GetAssociatedTests(ByVal TGPID As Integer) As String()
        Dim TestIDs As String = ""
        Dim Tests() As String = {TGPID.ToString}
        If IsCalculated(TGPID) Then
            TestIDs = GetOprands(TGPID)
        Else
            Dim sSQL As String = "Select * from TGP_Info where TGP_ID = " & TGPID
            If connString <> "" Then
                Dim cnat As New SqlConnection(connString)
                cnat.Open()
                Dim cmdat As New SqlCommand(sSQL, cnat)
                cmdat.CommandType = CommandType.Text
                Dim drat As SqlDataReader = cmdat.ExecuteReader
                If drat.HasRows Then
                    While drat.Read
                        TestIDs += drat("Info_ID").ToString & ","
                    End While
                    If TestIDs.EndsWith(",") Then TestIDs = Microsoft.VisualBasic.Mid(TestIDs, 1, Len(TestIDs) - 1)
                End If
                cnat.Close()
                cnat = Nothing
                'Else
                '    Dim cnat As New sqlConnection(connstring)
                '    cnat.Open()
                '    Dim cmdat As New sqlCommand(sSQL, cnat)
                '    cmdat.CommandType = CommandType.Text
                '    Dim drat As sqlDataReader = cmdat.ExecuteReader
                '    If drat.HasRows Then
                '        While drat.Read
                '            TestIDs += drat("Info_ID").ToString & ","
                '        End While
                '        If TestIDs.EndsWith(",") Then TestIDs = Microsoft.VisualBasic.Mid(TestIDs, 1, Len(TestIDs) - 1)
                '    End If
                '    cnat.Close()
                '    cnat = Nothing
            End If
        End If
        If TestIDs <> "" Then Tests = Split(TestIDs, ",")
        Return Tests
    End Function

    Public Function IsInHouseTest(ByVal AccID As Long, ByVal TestID As Integer) As Boolean
        Dim InHouse As Boolean = True
        Dim sSQL As String = "Select * from Sendout_Disbursement where Interface_ID in (Select ID from External_Interfaces " &
        "where IsActive <> 0 and FacilityType_ID = 4) and Sendout_ID in (Select ID from Sendouts where Accession_ID = " & AccID & ")"
        '
        Dim cnih As New SqlConnection(connString)
        cnih.Open()
        Dim cmdih As New SqlCommand(sSQL, cnih)
        cmdih.CommandType = CommandType.Text
        Dim drih As SqlDataReader = cmdih.ExecuteReader
        If drih.HasRows Then InHouse = False
        cnih.Close()
        cnih = Nothing
        Return InHouse
    End Function

    Public Function GetPanicAccessions(ByVal DateFrom As Date, ByVal DateTo As Date) As String
        Dim AccIDs As String = ""
        Dim sSQL As String = "Select distinct a.ID as AccID from Requisitions a inner join Acc_Results b on a.ID = b.Accession_ID " &
        "where a.AccessionDate between '" & Format(DateFrom, SystemConfig.DateFormat) & "' and '" & Format(DateTo, SystemConfig.DateFormat) &
        " 23:59:00' and b.Released <> 0 and (b.Flag like '%Panic%' or b.Flag like '%LP%' or b.Flag like '%HP%') and Not b.Accession_ID in " &
        "(Select Accession_ID from Panic_Notifications and Test_ID = b.Test_ID)"
        '
        Dim cnpas As New SqlConnection(connString)
        cnpas.Open()
        Dim cmdpas As New SqlCommand(sSQL, cnpas)
        cmdpas.CommandType = CommandType.Text
        Dim drpas As SqlDataReader = cmdpas.ExecuteReader
        If drpas.HasRows Then
            While drpas.Read
                AccIDs += drpas("AccID").ToString & ", "
            End While
            If AccIDs.EndsWith(", ") Then AccIDs = Microsoft.VisualBasic.Mid(AccIDs, 1, Len(AccIDs) - 2)
        End If
        cnpas.Close()
        cnpas = Nothing
        Return AccIDs
    End Function

    Public Function GetTestUOM(ByVal TestID As Integer) As String
        Dim UOM As String = ""
        Dim sSQL As String = "Select UOM from Tests where ID = " & TestID
        '
        Dim cnuom As New SqlConnection(connString)
        cnuom.Open()
        Dim cmduom As New SqlCommand(sSQL, cnuom)
        cmduom.CommandType = CommandType.Text
        Dim druom As SqlDataReader = cmduom.ExecuteReader
        If druom.HasRows Then
            While druom.Read
                If druom("UOM") IsNot DBNull.Value AndAlso
                Trim(druom("UOM")) <> "" Then UOM = Trim(druom("UOM"))
            End While
        End If
        cnuom.Close()
        cnuom = Nothing
        Return UOM
    End Function

    Private Function InReqTGP(ByVal AccID As Long, ByVal TGPID As Integer) As Boolean
        Dim INREQ As Boolean = False
        Dim sSQL As String = "Select * from Req_TGP where Accession_ID = " & AccID & " and TGP_ID = " & TGPID
        '
        Dim cnir As New SqlConnection(connString)
        cnir.Open()
        Dim cmdir As New SqlCommand(sSQL, cnir)
        cmdir.CommandType = CommandType.Text
        Dim drir As SqlDataReader = cmdir.ExecuteReader
        If drir.HasRows Then INREQ = True
        cnir.Close()
        cnir = Nothing
        Return INREQ
    End Function

    Private Function GetTPBillableTGPS(ByVal AccID As Long) As String()
        Dim Billables() As String = {""}
        Dim sSQL As String = "Select * from vReqTBillable where AccID = " & AccID
        '
        Dim cnbas As New SqlConnection(connString)
        cnbas.Open()
        Dim cmdbas As New SqlCommand(sSQL, cnbas)
        cmdbas.CommandType = CommandType.Text
        Dim drbas As SqlDataReader = cmdbas.ExecuteReader
        If drbas.HasRows Then
            While drbas.Read
                If Billables(UBound(Billables)) <> "" Then _
                ReDim Preserve Billables(UBound(Billables) + 1)
                Billables(UBound(Billables)) = drbas("TBBID").ToString
            End While
        End If
        cnbas.Close()
        cnbas = Nothing
        Return Billables
    End Function
    ' **** End of ApplyBillingRules Routine ***************

    Public Sub StoreResultInHistory(ByVal AccID As Long, ByVal Reflexer As Integer, ByVal _
    TestID As Integer, ByVal Dated As Date, ByVal CurrRes As String, ByVal UserID As Long)
        Dim sSQL As String = "Select Result, Released, Release_Time from Acc_Results where Accession_ID = " &
        AccID & " and Test_ID = " & TestID & " Union Select Result, Released, Release_Time from Acc_Info_Results " &
        "where Accession_ID = " & AccID & " And Info_ID = " & TestID & " Union Select Result, Released, Release_Time " &
        "from Ref_Results where Accession_ID = " & AccID & " And Reflexer_ID = " & Reflexer & " And Test_ID = " & TestID
        Dim cnhr As New SqlConnection(connString)
        cnhr.Open()
        Dim cmdhr As New SqlCommand(sSQL, cnhr)
        cmdhr.CommandType = CommandType.Text
        Dim drhr As SqlDataReader = cmdhr.ExecuteReader
        If drhr.HasRows Then
            While drhr.Read
                CurrRes = drhr("Result")
                Dated = drhr("Release_Time")
            End While
        End If
        cnhr.Close()
        cnhr = Nothing
        If CurrRes <> "" Then
            sSQL = "If Not Exists (Select * from Acc_Results_History where Accession_ID = " & AccID & " and " &
            "Test_ID = " & TestID & " And ResultTime = '" & Dated & "') Insert into Acc_Results_History (" &
            "Accession_ID, Test_ID, ResultTime, Result, User_ID) values (" & AccID & ", " & TestID & ", '" &
            Dated & "', '" & CurrRes & "', " & UserID & ")"
            ExecuteSqlProcedure(sSQL)
        End If
    End Sub

    Public Function GetResultHistory(ByVal AccID As Long) As Boolean
        Dim ResHist As Boolean = False
        Dim cnhh As New SqlConnection(connString)
        cnhh.Open()
        Dim cmdhh As New SqlCommand("Select " &
        "ResultHistory from Requisitions where ID = " & AccID, cnhh)
        cmdhh.CommandType = CommandType.Text
        Dim drhh As SqlDataReader = cmdhh.ExecuteReader
        If drhh.HasRows Then
            While drhh.Read
                ResHist = drhh("ResultHistory")
            End While
        End If
        cnhh.Close()
        cnhh = Nothing
        Return ResHist
    End Function

    Public Sub UpdateInfoResults(ByVal AccID As Long, ByVal TestID As Integer)
        Dim Infos() As String = {""}
        Dim InfoTests As String = ""
        Dim sSQL As String = "Select a.* from Tests a inner join TGP_Info b on a.ID = b.Info_ID where a.IsActive " &
        "<> 0 and a.HasResult <> 0 and a.PreAnalytical = 0 and b.TGP_ID = " & TestID & " order by b.Ordinal"
        Dim cni As New SqlConnection(connString)
        cni.Open()
        Dim selcmd As New SqlCommand(sSQL, cni)
        selcmd.CommandType = CommandType.Text
        Dim selDA As SqlDataReader = selcmd.ExecuteReader
        If selDA.HasRows Then
            While selDA.Read
                If Infos(UBound(Infos)) <> "" Then ReDim Preserve Infos(UBound(Infos) + 1)
                Infos(UBound(Infos)) = selDA("ID").ToString
            End While
        End If
        selDA.Close()
        selcmd.Dispose()
        cni.Close()
        cni = Nothing
        '
        For i As Integer = 0 To Infos.Length - 1
            If Infos(i) <> "" Then
                Dim cns As New SqlConnection(connString)
                cns.Open()
                Dim cmdupsert As New SqlCommand("Acc_Info_Results_SP", cns) 'modified SP to insert only 06/05/2017
                cmdupsert.CommandType = CommandType.StoredProcedure
                cmdupsert.Parameters.AddWithValue("@act", "Upsert")
                cmdupsert.Parameters.AddWithValue("@Accession_ID", AccID)
                cmdupsert.Parameters.AddWithValue("@Test_ID", TestID)
                cmdupsert.Parameters.AddWithValue("@Info_ID", Infos(i))
                cmdupsert.Parameters.AddWithValue("@Ordinal", i)
                cmdupsert.Parameters.AddWithValue("@NormalRange", GetNormalRange(AccID, Infos(i)))
                cmdupsert.Parameters.AddWithValue("@UOM", "")
                cmdupsert.Parameters.AddWithValue("@LabID", "")
                cmdupsert.ExecuteNonQuery()
                InfoTests += Infos(i).ToString & ", "
                cmdupsert.Dispose()
                cns.Close()
                cns = Nothing
            End If
        Next
        '
        If InfoTests.EndsWith(", ") Then InfoTests = Microsoft.VisualBasic.Mid(InfoTests, 1, Len(InfoTests) - 2)
        If InfoTests <> "" Then
            sSQL = "Delete from Acc_Info_Results where Accession_ID = " & AccID _
            & " and Test_ID = " & TestID & " and Not Info_ID in (" & InfoTests & ")"
        Else
            sSQL = "Delete from Acc_Info_Results where " &
            "Test_ID = " & TestID & " and Accession_ID = " & AccID
        End If
        ExecuteSqlProcedure(sSQL)
        '
        InfoTests = ""
        Dim Preanas() As String = {""}
        sSQL = "Select a.* from Tests a inner join TGP_Info b on a.ID = b.Info_ID where a.IsActive " &
        "<> 0 and a.HasResult <> 0 and a.PreAnalytical <> 0 and b.TGP_ID = " & TestID & " order by Ordinal"
        Dim cnpr As New SqlConnection(connString)
        cnpr.Open()
        Dim cmdpr As New SqlCommand(sSQL, cnpr)
        cmdpr.CommandType = CommandType.Text
        Dim dapr As SqlDataReader = cmdpr.ExecuteReader
        If dapr.HasRows Then
            While dapr.Read
                If Preanas(UBound(Preanas)) <> "" Then ReDim Preserve Preanas(UBound(Preanas) + 1)
                Preanas(UBound(Preanas)) = dapr("ID").ToString
            End While
        End If
        cmdpr.Dispose()
        cnpr.Close()
        cnpr = Nothing
        '
        For i As Integer = 0 To Preanas.Length - 1
            If Preanas(i) <> "" Then
                sSQL = "If Not Exists (Select * from Req_Info_Response where Accession_ID = " & AccID &
                " and TGP_ID = " & TestID & " and Info_ID = " & Preanas(i) & ") Insert into Req_Info_Response " &
                "(Accession_ID, TGP_ID, Info_ID, Ordinal, Response, Flag, Behavior, UOM, LastEdited_On, " &
                "LastEdited_By) values (" & AccID & ", " & TestID & ", " & Preanas(i) & ", " & i & ", '', '', " &
                "'', '', '" & Date.Now & "', " & ThisUser.ID & ")"
                ExecuteSqlProcedure(sSQL)
                InfoTests += Preanas(i).ToString & ", "
            End If
        Next
        If InfoTests.EndsWith(", ") Then InfoTests = Microsoft.VisualBasic.Mid(InfoTests, 1, Len(InfoTests) - 2)
        If InfoTests <> "" Then
            sSQL = "Delete from Req_Info_Response where Accession_ID = " & AccID _
            & " and TGP_ID = " & TestID & " and Not Info_ID in (" & InfoTests & ")"
        Else
            sSQL = "Delete from Req_Info_Response where " &
            "TGP_ID = " & TestID & " and Accession_ID = " & AccID
        End If
        ExecuteSqlProcedure(sSQL)
    End Sub

    Public Function ValidateMySeat(ByVal LicSeats As Integer, ByVal PC As String) As String
        Dim PCS As Integer = 0
        Dim sSQL As String = "Select COUNT(MacName) as PCCOUNT from SystemStations"
        If connString <> "" Then
            Dim cnvms As New SqlConnection(connString)
            cnvms.Open()
            Dim cmdvms As New SqlCommand(sSQL, cnvms)
            cmdvms.CommandType = CommandType.Text
            Dim drvms As SqlDataReader = cmdvms.ExecuteReader
            If drvms.HasRows Then
                While drvms.Read
                    If drvms("PCCOUNT") IsNot DBNull.Value _
                    Then PCS = drvms("PCCOUNT")
                End While
            End If
            cnvms.Close()
            cnvms = Nothing
            '
            If PCS < LicSeats Then  'add and validate ok
                sSQL = "If Exists (Select * from SystemStations where MacName = '" & Trim(PC) & "') Update " &
                "SystemStations set LastRunTime = '" & Date.Now & "' where MacName = '" & Trim(PC) & "' Else " &
                "Insert into SystemStations (MacName, MacINIDate, LastRunTime, LastUserID) values ('" &
                Trim(PC) & "', '" & Date.Now & "', '" & Date.Now & "', " & ThisUser.ID & ")"
                ExecuteSqlProcedure(sSQL)
            Else
                PC = ""
            End If
            'ElseIf connstring <> "" Then
            '    Dim cnvms As New sqlConnection(connstring)
            '    cnvms.Open()
            '    Dim cmdvms As New sqlCommand(sSQL, cnvms)
            '    cmdvms.CommandType = CommandType.Text
            '    Dim drvms As sqlDataReader = cmdvms.ExecuteReader
            '    If drvms.HasRows Then
            '        While drvms.Read
            '            If drvms("PCCOUNT") IsNot DBNull.Value _
            '            Then PCS = drvms("PCCOUNT")
            '        End While
            '    End If
            '    cnvms.Close()
            '    cnvms = Nothing
            '    '
            '    If PCS < LicSeats Then  'add and validate ok
            '        sSQL = "If Exists (Select * from SystemStations where MacName = '" & Trim(PC) & "') Update " &
            '        "SystemStations set LastRunTime = '" & Date.Now & "' where MacName = '" & Trim(PC) & "' Else " &
            '        "Insert into SystemStations (MacName, MacINIDate, LastRunTime, LastUserID) values ('" &
            '        Trim(PC) & "', '" & Date.Now & "', '" & Date.Now & "', 'Null')"
            '        ExecuteSqlProcedure(sSQL)
            '    Else
            '        PC = ""
            '    End If
        End If
        Return PC
    End Function

    Public Function IsTGPInHouse(ByVal TGPID As Integer) As Boolean
        Dim InHouse As Boolean = True
        Dim sSQL As String = "Select InHouse from Tests where ID = " &
        TGPID & " Union Select InHouse from Groups where ID = " &
        TGPID & " Union Select InHouse from Profiles where ID = " & TGPID
        If connString <> "" Then
            Dim cni As New SqlConnection(connString)
            cni.Open()
            Dim cmdi As New SqlCommand(sSQL, cni)
            cmdi.CommandType = CommandType.Text
            Dim dri As SqlDataReader = cmdi.ExecuteReader
            If dri.HasRows Then
                While dri.Read
                    InHouse = dri("InHouse")
                End While
            End If
            cni.Close()
            cni = Nothing
            'Else
            '    Dim cni As New sqlConnection(connstring)
            '    cni.Open()
            '    Dim cmdi As New sqlCommand(sSQL, cni)
            '    cmdi.CommandType = CommandType.Text
            '    Dim dri As sqlDataReader = cmdi.ExecuteReader
            '    If dri.HasRows Then
            '        While dri.Read
            '            InHouse = dri("InHouse")
            '        End While
            '    End If
            '    cni.Close()
            '    cni = Nothing
        End If
        Return InHouse
    End Function

    Public Function AccessionResulted(ByVal AccID As Long) As Boolean
        Dim Resd As Boolean = False
        Dim cnr As New SqlConnection(connString)
        cnr.Open()
        Dim cmdr As New SqlCommand("Select Accession_ID from " &
        "Acc_Results where Released <> 0 and Accession_ID = " & AccID &
        " Union Select Accession_ID from Ref_Results where Released <> 0 " &
        "and Accession_ID = " & AccID & " Union Select Accession_ID from " &
        "Acc_Info_Results where Released <> 0 and Accession_ID = " & AccID, cnr)
        cmdr.CommandType = CommandType.Text
        Dim drr As SqlDataReader = cmdr.ExecuteReader
        If drr.HasRows Then Resd = True
        cnr.Close()
        cnr = Nothing
        Return Resd
    End Function

    Public Sub AuditTrail_Shutdown()
        ExecuteSqlProcedure("Update System_Config Set AuditTrail = 0")
    End Sub

    Public Function ValidateLabelFile(ByVal AccLabel As String) As String

        Return GetReportPath(AccLabel)


        'If InStr(AccLabel, "\") > 0 Then   'Full path
        '    AccLabel = My.Application.Info.DirectoryPath & "\Reports\" &
        '    Microsoft.VisualBasic.Mid(AccLabel, InStrRev(AccLabel, "\") + 1)
        'Else
        '    AccLabel = My.Application.Info.DirectoryPath & "\Reports\" & AccLabel
        'End If
        'Return AccLabel
    End Function

    Public Function ValidateReportFile(ByVal AccID As String,
    ByVal IsCustomRPT As Boolean) As String
        Dim RPT As String = ""
        Dim sSQL As String = "Select ResRPTFile from Providers where ID in (Select " &
        "OrderingProvider_ID from Requisitions where ID = " & Val(AccID) & ")"
        Dim cnvrf As New SqlConnection(connString)
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
                If SystemConfig.GenericResults <> "" Then
                    RPT = SystemConfig.GenericResults
                Else
                    RPT = "AccRes_Navy.RPT"
                End If
            Else    'Custom
                If SystemConfig.CustomResults <> "" Then
                    RPT = SystemConfig.CustomResults
                Else
                    RPT = "AccRes_Navy.RPT"
                End If
            End If
        End If
        '

        Return GetReportPath(RPT)

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

    Public Function RTF_To_Text(ByVal RTF As String) As String
        Dim sTXT As String = ""
        Dim RTFB As New RichTextBox
        Try
            RTFB.Rtf = RTF

        Catch ex As Exception
            Return RTF
        End Try
        sTXT = RTFB.Text
        RTFB = Nothing
        Return sTXT
    End Function



    'Public Sub ApplyNewServer(ByVal oRPT As ReportDocument,
    '                      ByVal serverName As String,
    '                      ByVal databaseName As String,
    '                      ByVal userName As String,
    '                      ByVal password As String)

    '    If String.IsNullOrEmpty(serverName) Then Exit Sub

    '    ' Create a ConnectionInfo object to hold the server login details
    '    Dim connectionInfo As New ConnectionInfo With {
    '    .serverName = serverName,
    '    .databaseName = databaseName,
    '    .UserID = userName,
    '    .password = password,
    '    .IntegratedSecurity = String.IsNullOrEmpty(userName) AndAlso String.IsNullOrEmpty(password)
    '}

    '    ' Apply the login information to the main report tables
    '    ApplyLogonInfoToTables(oRPT.Database.Tables, connectionInfo)

    '    ' Apply the login information to subreport tables
    '    For Each subReport As ReportDocument In oRPT.Subreports
    '        ApplyLogonInfoToTables(subReport.Database.Tables, connectionInfo)
    '    Next
    'End Sub


    'Private Sub ApplyLogonInfoToTables(ByVal tables As Tables, ByVal connectionInfo As ConnectionInfo)
    '    For Each crTable As Table In tables
    '        Dim tableLogOnInfo As TableLogOnInfo = crTable.LogOnInfo
    '        tableLogOnInfo.ConnectionInfo = connectionInfo

    '        ' Apply the connection info to the table
    '        crTable.ApplyLogOnInfo(tableLogOnInfo)

    '        ' Optional: Update the table location if the database name changes
    '        If Not String.IsNullOrEmpty(connectionInfo.DatabaseName) Then
    '            Dim tableName As String = crTable.Location.Substring(crTable.Location.LastIndexOf(".") + 1)
    '            crTable.Location = $"{connectionInfo.DatabaseName}.dbo.{tableName}"
    '        End If
    '    Next
    'End Sub


    Public Sub ResizeToClient(ByVal FRM As Form)
        Dim origWidth As Integer = FRM.Width
        Dim origHeight As Integer = FRM.Height
        Dim newWidth As Integer = CInt(My.Computer.Screen.WorkingArea.Height _
        * (FRM.Width / FRM.Height))
        Dim newHeight As Integer = CInt(My.Computer.Screen.WorkingArea.Height)
        Dim fSize As New SizeF((newWidth / origWidth), (newHeight / origHeight))
        'origWidth = FRM.Width
        'origHeight = FRM.Height
        Dim cControl As Control
        For Each cControl In FRM.Controls
            If TypeOf cControl Is DataGridView Then
                Dim GRD As DataGridView = cControl
                Dim CLM As DataGridViewColumn
                Dim RW As DataGridViewRow
                For Each CLM In GRD.Columns
                    'If TypeOf CLM Is DataGridViewImageColumn And _
                    'TypeOf CLM Is DataGridViewCheckBoxColumn Then
                    CLM.FillWeight = CInt(CLM.FillWeight * (FRM.Width / origWidth))
                    CLM.Width = CInt(CLM.Width * (FRM.Width / origWidth))
                    'End If
                Next
                For Each RW In GRD.Rows
                    RW.Height = CInt(RW.Height * (FRM.Height / origHeight))
                Next
            End If
            'If TypeOf cControl Is TabControl Then
            '    Dim pControl As Control = cControl
            '    Dim GRD As DataGridView
            '    If pControl.Contains(GRD) Then

            '    End If
            'End If
            cControl.Scale(fSize)
        Next cControl
    End Sub

    Public Function GetProviderConfigs(ByVal ProvID As Long) As String()
        Dim Configs() As String = {"", "", "", "", "", "", "", "", "", ""}
        '0=ProviderID, 1=Complete, 2=Print, 3=Prolison, 4=Interface, 5=RPTFax
        '6=Fax#, 7=RPTEmail, 8=Email, 9=RPTFile
        Dim cnpc As New SqlConnection(connString)
        cnpc.Open()
        Dim cmdpc As New SqlCommand("Select " &
        "* from Providers where ID = " & ProvID, cnpc)
        cmdpc.CommandType = CommandType.Text
        Dim drpc As SqlDataReader = cmdpc.ExecuteReader
        If drpc.HasRows Then
            While drpc.Read
                Configs(0) = ProvID.ToString
                If drpc("RPTComplete") IsNot DBNull.Value Then _
                Configs(1) = Convert.ToInt16(drpc("RPTComplete"))
                If drpc("RDM_Print") IsNot DBNull.Value _
                Then Configs(2) = Convert.ToInt16(drpc("RDM_Print"))
                If drpc("RDM_Prolison") IsNot DBNull.Value _
                Then Configs(3) = Convert.ToInt16(drpc("RDM_Prolison"))
                If drpc("RDM_Interface") IsNot DBNull.Value _
                Then Configs(4) = Convert.ToInt16(drpc("RDM_Interface"))
                If drpc("RDM_Fax") IsNot DBNull.Value _
                Then Configs(5) = Convert.ToInt16(drpc("RDM_Fax"))
                If drpc("Fax") IsNot DBNull.Value Then Configs(6) = Trim(drpc("Fax"))
                If drpc("RDM_Email") IsNot DBNull.Value _
                Then Configs(7) = Convert.ToInt16(drpc("RDM_Email"))
                If drpc("Email") IsNot DBNull.Value Then Configs(8) = Trim(drpc("Email"))
                If drpc("ResRPTFile") IsNot DBNull.Value Then Configs(9) = Trim(drpc("ResRPTFile"))
            End While
        End If
        cnpc.Close()
        cnpc = Nothing
        Return Configs
    End Function

    Public Sub MeResize(ByVal FRM As System.Windows.Forms.Form,
    ByVal origWidth As Integer, ByVal origHeight As Integer)
        Dim fSize As New SizeF((FRM.Width / origWidth), (FRM.Height / origHeight))
        Dim cControl As Control
        Dim i As Integer
        For Each cControl In FRM.Controls
            If TypeOf cControl Is DataGridView Then
                Dim GRD As DataGridView = cControl
                Dim CLM As DataGridViewColumn
                Dim RW As DataGridViewRow
                For Each CLM In GRD.Columns
                    CLM.FillWeight = CInt(CLM.FillWeight * (FRM.Width / origWidth))
                    CLM.Width = CInt(CLM.Width * (FRM.Width / origWidth))
                Next
                For Each RW In GRD.Rows
                    RW.Height = CInt(RW.Height * (FRM.Height / origHeight))
                Next
            End If
            If (TypeOf cControl Is TabControl) Then
                Dim TC As TabControl = cControl
                Dim page As TabPage
                Dim controlInTab As Control
                For i = 0 To TC.TabCount - 1
                    page = TC.TabPages(i)
                    For Each controlInTab In page.Controls
                        If TypeOf controlInTab Is DataGridView Then
                            Dim GRID As DataGridView = controlInTab
                            Dim CLUM As DataGridViewColumn
                            Dim ROW As DataGridViewRow
                            For Each CLUM In GRID.Columns
                                CLUM.FillWeight = CInt(CLUM.FillWeight * (FRM.Width / origWidth))
                                CLUM.Width = CInt(CLUM.Width * (FRM.Width / origWidth))
                            Next
                            For Each ROW In GRID.Rows
                                ROW.Height = CInt(ROW.Height * (FRM.Height / origHeight))
                            Next
                        End If
                    Next
                Next
            End If
            cControl.Scale(fSize)
        Next cControl
        origWidth = FRM.Width
        origHeight = FRM.Height
        '
        'Dim i As Integer
        'Dim cControl As Control
        'For Each cControl In FRM.Controls
        '    If TypeOf cControl Is DataGridView Then
        '        Dim GRD As DataGridView = cControl
        '        Dim CLM As DataGridViewColumn
        '        Dim RW As DataGridViewRow
        '        For Each CLM In GRD.Columns
        '            CLM.FillWeight = CInt(CLM.FillWeight * (FRM.Width / origWidth))
        '            CLM.Width = CInt(CLM.Width * (FRM.Width / origWidth))
        '        Next
        '        For Each RW In GRD.Rows
        '            RW.Height = CInt(RW.Height * (FRM.Height / origHeight))
        '        Next
        '    End If
        '    cControl.Scale(fSize)
        'Next cControl
    End Sub

    Public Function TestInArray(ByVal TestID As String, ByVal Results() As String) As Boolean
        Dim InArr As Boolean = False
        Dim i As Integer
        Dim Seg As String
        For i = 0 To Results.Length - 1
            Seg = Results(i)
            If InStr(Seg, "=") > 0 Then 'valid seg
                If Microsoft.VisualBasic.Mid(Seg, 1, InStr(Seg, "=") - 1) = TestID Then
                    InArr = True
                    Exit For
                End If
            End If
        Next
        Return InArr
    End Function

    Public Sub LogUserEvent(ByVal UserID As Long, ByVal EventID As Integer, ByVal EventTime As String,
    ByVal ObjType As String, ByVal ObjID As Long, ByVal StatusFrom As String, ByVal StatusTo As String)
        Dim sSQL As String = "If not Exists (Select * from User_Event where User_ID = " & UserID & " and " &
        "Event_ID = " & EventID & " and Event_Time = '" & EventTime & "') Insert into User_Event (User_ID, " &
        "Event_ID, Event_Time, Object_Type, Object_ID, StatusFrom, StatusTo) values (" & UserID & ", " & EventID &
        ", '" & EventTime & "', '" & ObjType & "', '" & ObjID & "', '" & StatusFrom & "', '" & StatusTo & "')"
        ExecuteSqlProcedure(sSQL)
    End Sub

    Public Function GetPhrase(ByVal InPutKeys As Long) As String
        Dim Phrase As String = Nothing
        Dim cngp As New SqlConnection(connString)
        cngp.Open()
        Dim cmdgp As New SqlCommand("Select * " &
        "from Phrases where InputKeys = " & InPutKeys, cngp)
        cmdgp.CommandType = CommandType.Text
        Dim drgp As SqlDataReader = cmdgp.ExecuteReader
        If drgp.HasRows Then
            While drgp.Read
                If drgp("Phrase") IsNot DBNull.Value _
                Then Phrase = Trim(drgp("Phrase"))
            End While
        End If
        cngp.Close()
        cngp = Nothing
        Return Phrase
    End Function

    Public Function GetClient(ByVal AccID As Long) As String()
        Dim Client() As String = {"", ""}
        Dim sSQL As String = "Select * from Providers where ID in (Select OrderingProvider_ID from " &
        "Requisitions where Received <> 0 and ID = " & AccID & ")"
        If connString <> "" Then
            Dim cngc As New SqlConnection(connString)
            cngc.Open()
            Dim cmdgc As New SqlCommand(sSQL, cngc)
            cmdgc.CommandType = CommandType.Text
            Dim drgc As SqlDataReader = cmdgc.ExecuteReader
            If drgc.HasRows Then
                While drgc.Read
                    If drgc("IsIndividual") IsNot DBNull.Value AndAlso drgc("IsIndividual") = 0 Then 'False
                        Client(0) = drgc("LastName_BSN")
                    Else
                        If drgc("Degree") Is DBNull.Value Then
                            Client(0) = drgc("LastName_BSN") & ", " & drgc("FirstName")
                        Else
                            Client(0) = drgc("LastName_BSN") & ", " &
                            drgc("FirstName") & " " & Trim(drgc("Degree"))
                        End If
                    End If
                End While
            End If
            cngc.Close()
            cngc = Nothing
            '
            sSQL = "Select * from Providers where ID in (Select AttendingProvider_ID " &
            "from Requisitions where Received <> 0 and ID = " & AccID & ")"
            Dim cngca As New SqlConnection(connString)
            cngca.Open()
            Dim cmdgca As New SqlCommand(sSQL, cngca)
            cmdgca.CommandType = CommandType.Text
            Dim drgca As SqlDataReader = cmdgca.ExecuteReader
            If drgca.HasRows Then
                While drgca.Read
                    If drgca("IsIndividual") IsNot DBNull.Value AndAlso drgca("IsIndividual") = 0 Then 'False
                        Client(1) = drgca("LastName_BSN")
                    Else
                        If drgca("Degree") Is DBNull.Value Then
                            Client(1) = drgca("LastName_BSN") & ", " & drgca("FirstName")
                        Else
                            Client(1) = drgca("LastName_BSN") & ", " &
                            drgca("FirstName") & " " & Trim(drgca("Degree"))
                        End If
                    End If
                End While
            End If
            cngca.Close()
            cngca = Nothing
        End If
        Return Client
    End Function

    Public Function AccessionReceived(ByVal AccID As Long) As Boolean
        Dim Recd As Boolean = False
        Dim sSQL As String = "Select * from Requisitions where Received <> 0 and ID = " & AccID
        Dim cnar As New SqlConnection(connString)
        cnar.Open()
        Dim cmdar As New SqlCommand(sSQL, cnar)
        cmdar.CommandType = CommandType.Text
        Dim drar As SqlDataReader = cmdar.ExecuteReader
        If drar.HasRows Then Recd = True
        cnar.Close()
        cnar = Nothing
        Return Recd
    End Function

    Public Function GetDrawnDate(ByVal AccID As Long) As Date
        Dim Drawn As Date = Nothing
        Dim sSQL As String = "Select (Select Min(SourceDate) from Specimens where Accession_ID = " &
        AccID & ") as DrawnDate, AccessionDate from Requisitions where Received <> 0 and ID = " & AccID
        If connString <> "" Then
            Dim cngdd As New SqlConnection(connString)
            cngdd.Open()
            Dim cmdgdd As New SqlCommand(sSQL, cngdd)
            cmdgdd.CommandType = CommandType.Text
            Dim drgdd As SqlDataReader = cmdgdd.ExecuteReader
            If drgdd.HasRows Then
                While drgdd.Read
                    If drgdd("DrawnDate") IsNot DBNull.Value Then
                        Drawn = drgdd("DrawnDate")
                    Else
                        Drawn = drgdd("AccessionDate")
                    End If
                End While
            End If
            cngdd.Close()
            cngdd = Nothing
        End If
        Return Drawn
    End Function

    Public Function GetDefaultPrinter() As String
        Dim P As New System.Drawing.Printing.PrinterSettings
        Return P.PrinterName
    End Function

    Public Function PhoneNeat(ByVal Phone As String) As String
        If String.IsNullOrWhiteSpace(Phone) Then
            Return ""
        End If

        Phone = Replace(Phone, "(", "")
        Phone = Replace(Phone, ")", "")
        Phone = Replace(Phone, "-", "")
        Phone = Replace(Phone, " ", "")
        Phone = Replace(Phone, ".", "")
        Phone = Replace(Phone, ",", "")
        Phone = Replace(Phone, "/", "")
        Phone = Replace(Phone, "\", "")
        Phone = Replace(Phone, "_", "")
        Return Phone
    End Function

    Private Function GetTriggerOrdinal(ByVal AccID As Long, ByVal TrigID As Integer) As Integer
        Dim TRID As Integer = 0
        Dim cnto As New SqlConnection(connString)
        cnto.Open()
        Dim cmdto As New SqlCommand("Select Max(Ordinal) as MaxOrd from Ref_Results " &
        "where Accession_ID = " & AccID & " And Reflexer_ID = " & TrigID & " Union Select Max(Ordinal) " &
        "as MaxOrd from Acc_Results where Accession_ID = " & AccID & " And Test_ID = " & TrigID, cnto)
        cmdto.CommandType = CommandType.Text
        Dim drto As SqlDataReader = cmdto.ExecuteReader
        If drto.HasRows Then
            While drto.Read
                If drto("MaxOrd") IsNot DBNull.Value Then
                    If drto("MaxOrd") > TRID Then TRID = drto("MaxOrd")
                End If
            End While
        End If
        cnto.Close()
        cnto = Nothing
        Return TRID
    End Function

    Private Function TGPIDTriggered(ByVal AccID As Long, ByVal TGPID As Integer) As Boolean
        Dim Triged As Boolean = False
        Dim sSQL As String = "Select * from Ref_Results where Accession_ID = " & AccID & " and Reflexed_ID = " & TGPID
        If connString <> "" Then
            Dim cnn As New SqlConnection(connString)
            cnn.Open()
            Dim cmdsel As New SqlCommand(sSQL, cnn)
            cmdsel.CommandType = CommandType.Text
            Dim DRsel As SqlDataReader = cmdsel.ExecuteReader
            If DRsel.HasRows Then Triged = True
            cnn.Close()
            cnn = Nothing
        Else
            Dim cnn As New sqlConnection(connString)
            cnn.Open()
            Dim cmdsel As New sqlCommand(sSQL, cnn)
            cmdsel.CommandType = CommandType.Text
            Dim DRsel As sqlDataReader = cmdsel.ExecuteReader
            If DRsel.HasRows Then Triged = True
            cnn.Close()
            cnn = Nothing
        End If
        Return Triged
    End Function

    Public Function GetTestsOfGroup(ByVal GroupID As Integer) As String()
        Dim Tests() As String = {""}
        Dim cngt As New SqlConnection(connString)
        cngt.Open()
        Dim cmdgt As New SqlCommand("Select Test_ID " &
        "from Group_Test where Group_ID = " & GroupID, cngt)
        cmdgt.CommandType = CommandType.Text
        Dim drgt As SqlDataReader = cmdgt.ExecuteReader
        If drgt.HasRows Then
            While drgt.Read
                If Tests(UBound(Tests)) <> "" Then ReDim Preserve Tests(UBound(Tests) + 1)
                Tests(UBound(Tests)) = drgt("Test_ID").ToString
            End While
        End If
        cngt.Close()
        cngt = Nothing
        Return Tests
    End Function

    Public Function ProcessTriggers(ByVal AccID As Long, ByVal ReflexerID As Integer, ByVal ReflexedIDs() As String) As Boolean
        Dim IsDirty As Boolean = False
        Dim TGPType As String = ""
        For i As Integer = 0 To ReflexedIDs.Length - 1
            If ReflexedIDs(i) <> "" Then
                TGPType = GetTGPType(ReflexedIDs(i))
                If TGPType = "T" Then
                    ExecuteSqlProcedure("If not Exists (Select * from Ref_Results where Accession_ID = " & AccID & " and " &
                    "Reflexer_ID = " & ReflexerID & " and Reflexed_ID = " & ReflexedIDs(i) & " and Test_ID = " & ReflexedIDs(i) &
                    ") Insert into Ref_Results (Accession_ID, Reflexer_ID, Reflexed_ID, Test_ID, Ordinal, Result, " &
                    "Interpretation, Flag, Behavior, NormalRange, T_Result, I_Result, Comment, Released, UOM, " &
                    "Released_By, Release_Time, LabID) Values (" & AccID & ", " & ReflexerID & ", " & ReflexedIDs(i) &
                    ", " & ReflexedIDs(i) & ", " & GetTriggerOrdinal(AccID, ReflexerID) & ", '', '', '', '', '" &
                    GetNormalRange(AccID, ReflexedIDs(i)) & "', 'Null', 'Null', '', 0, '', 'Null', 'Null', '')")
                    IsDirty = True
                ElseIf TGPType = "G" Then
                    Dim Tests() As String = GetTestsOfGroup(ReflexedIDs(i))
                    For t As Integer = 0 To Tests.Length - 1
                        If Tests(t) <> "" Then
                            ExecuteSqlProcedure("If not Exists (Select * from Ref_Results where Accession_ID = " & AccID &
                            " and Reflexer_ID = " & ReflexerID & " and Reflexed_ID = " & ReflexedIDs(i) & " and Test_ID = " &
                            Tests(t) & ") Insert into Ref_Results (Accession_ID, Reflexer_ID, Reflexed_ID, Test_ID, Ordinal, " &
                            "Result, Interpretation, Flag, Behavior, NormalRange, T_Result, I_Result, Comment, Released, UOM, " &
                            "Released_By, Release_Time, LabID) Values (" & AccID & ", " & ReflexerID & ", " & ReflexedIDs(i) &
                            ", " & Tests(t) & ", " & GetTriggerOrdinal(AccID, ReflexerID) & ", '', '', '', '', '" &
                            GetNormalRange(AccID, ReflexedIDs(i)) & "', 'Null', 'Null', '', 0, '', 'Null', 'Null', '')")
                            IsDirty = True
                        End If
                    Next
                End If
            End If
        Next
        Return IsDirty
    End Function

    Public Sub CancelFaxSchedule(ByVal AccID As Long)

    End Sub

    Public Function ResultTriggering(ByVal AccID As Long, ByVal TestID As Integer, ByVal Result As String) As Boolean
        Dim Triggering As Boolean = False
        Dim NumRes As String = Result
        Dim RefdID As String = ""
        Dim Multi As Boolean = False
        NumRes = Replace(NumRes, "<", "") : NumRes = Replace(NumRes, ">", "")
        NumRes = Replace(NumRes, " ", "")
        NumRes = Replace(NumRes, ",", "")
        Dim sSQL As String = "Select a.Marked_ID as TrigedID, a.ReflexMulti as Multi from C_Triggers a inner join Tests b on b.ID = " &
        "a.Test_ID where b.Qualitative <> 0 and a.Choice = '" & Result & "' and a.Test_ID = " & TestID & " Union Select c.Marked_ID " &
        "as TrigedID, c.ReflexMulti as Multi from N_Triggers c inner join Tests d on d.ID = c.Test_ID where d.Qualitative = 0 and " &
        "IsNumeric('" & NumRes & "') <> 0 and d.ID = " & TestID & " and convert(real, '" & NumRes & "') between c.ValueFrom and c.ValueTo"
        Dim cnn As New SqlConnection(connString)
        cnn.Open()
        Dim cmdsel As New SqlCommand(sSQL, cnn)
        cmdsel.CommandType = CommandType.Text
        Dim DRsel As SqlDataReader = cmdsel.ExecuteReader
        If DRsel.HasRows Then
            While DRsel.Read
                RefdID = DRsel("TrigedID").ToString
                Multi = CType(DRsel("Multi"), Boolean)
            End While
        End If
        cnn.Close()
        cnn = Nothing
        If RefdID <> "" Then    'Reflexing
            sSQL = "Select distinct Reflexed_ID from Ref_Results where Accession_ID = " &
            AccID & " and not Reflexer_ID = " & TestID & " And Reflexed_ID = " & RefdID
            Dim cn1 As New SqlConnection(connString)
            cn1.Open()
            Dim cmd1 As New SqlCommand(sSQL, cn1)
            cmd1.CommandType = CommandType.Text
            Dim DR1 As SqlDataReader = cmd1.ExecuteReader
            If DR1.HasRows Then
                If Multi = False Then
                    Triggering = False
                Else
                    Triggering = True
                End If
            Else
                Triggering = True
            End If
            cn1.Close()
            cn1 = Nothing
        End If
        '
        Return Triggering
    End Function

    Public Function GetRefReflexerIDs(ByVal AccID As Long) As String()
        Dim Trigers() As String = {""}
        Dim sSQL As String = "select distinct reflexer_ID from Ref_Results " &
        "where not Reflexer_ID in (Select Test_ID from Acc_Results where " &
        "Accession_ID = " & AccID & ") and Accession_ID = " & AccID
        '
        Dim cnrrf As New SqlConnection(connString)
        cnrrf.Open()
        Dim cmdrrf As New SqlCommand(sSQL, cnrrf)
        cmdrrf.CommandType = CommandType.Text
        Dim drrrf As SqlDataReader = cmdrrf.ExecuteReader
        If drrrf.HasRows Then
            While drrrf.Read
                If Trigers(UBound(Trigers)) <> "" Then ReDim Preserve Trigers(UBound(Trigers) + 1)
                Trigers(UBound(Trigers)) = drrrf("Reflexer_ID").ToString
            End While
        End If
        cnrrf.Close()
        cnrrf = Nothing
        Return Trigers
    End Function

    Public Function GetRefReflexedIDs(ByVal AccID As Long, ByVal ReflexerID As Integer) As String()
        Dim TrigIDs() As String = {""}
        Dim sSQL As String = "select distinct reflexed_ID from Ref_Results " &
        "where Reflexer_ID = " & ReflexerID & " and Accession_ID = " & AccID
        '
        Dim cnrrf As New SqlConnection(connString)
        cnrrf.Open()
        Dim cmdrrf As New SqlCommand(sSQL, cnrrf)
        cmdrrf.CommandType = CommandType.Text
        Dim drrrf As SqlDataReader = cmdrrf.ExecuteReader
        If drrrf.HasRows Then
            While drrrf.Read
                If TrigIDs(UBound(TrigIDs)) <> "" Then ReDim Preserve TrigIDs(UBound(TrigIDs) + 1)
                TrigIDs(UBound(TrigIDs)) = drrrf("Reflexed_ID").ToString
            End While
        End If
        cnrrf.Close()
        cnrrf = Nothing
        Return TrigIDs
    End Function

    Public Function GetAccReflexedIDs(ByVal AccID As Long, ByVal ReflexerID As Integer) As String()
        Dim TrigIDs() As String = {""}
        Dim sSQL As String = "Select distinct Reflexed_ID from Ref_Results " &
        "where Reflexer_ID = " & ReflexerID & " and Accession_ID = " & AccID
        Dim cnn As New SqlConnection(connString)
        cnn.Open()
        Dim selcmd As New SqlCommand(sSQL, cnn)
        selcmd.CommandType = CommandType.Text
        Dim selDR As SqlDataReader = selcmd.ExecuteReader
        If selDR.HasRows Then
            While selDR.Read
                If TrigIDs(UBound(TrigIDs)) <> "" Then ReDim Preserve TrigIDs(UBound(TrigIDs) + 1)
                TrigIDs(UBound(TrigIDs)) = selDR("Reflexed_ID").ToString
            End While
        End If
        cnn.Close()
        cnn = Nothing
        Return TrigIDs
    End Function

    Public Function Rtf_To_Html1(ByVal sRTF As String) As String
        ''Declare a Word Application Object and a Word WdSaveOptions object
        'Dim MyWord As Microsoft.Office.Interop.Word.Application = Nothing
        'Dim oDoNotSaveChanges As Object =
        '     Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges
        ''Declare two strings to handle the data
        'Dim sReturnString As String = ""
        'Dim sConvertedString As String = ""
        'Try
        '    'Instantiate the Word application,
        '    'set visible to false and create a document
        '    MyWord = CreateObject("Word.application")
        '    MyWord.Visible = False
        '    MyWord.Documents.Add()
        '    'Create a DataObject to hold the Rich Text
        '    'and copy it to the clipboard
        '    Dim doRTF As New System.Windows.Forms.DataObject
        '    doRTF.SetData("Rich Text Format", sRTF)
        '    Clipboard.SetDataObject(doRTF)
        '    'Paste the contents of the clipboard to the empty,
        '    'hidden Word Document
        '    MyWord.Windows(1).Selection.Paste()
        '    '…then, select the entire contents of the document
        '    'and copy back to the clipboard
        '    MyWord.Windows(1).Selection.WholeStory()
        '    MyWord.Windows(1).Selection.Copy()
        '    'Now retrieve the HTML property of the DataObject
        '    'stored on the clipboard
        '    sConvertedString =
        '         Clipboard.GetData(System.Windows.Forms.DataFormats.Html)
        '    'Remove some leading text that shows up in some instances
        '    '(like when you insert it into an email in Outlook
        '    sConvertedString =
        '         sConvertedString.Substring(sConvertedString.IndexOf("<html"))
        '    'Also remove multiple Â characters that somehow end up in there
        '    sConvertedString = sConvertedString.Replace("Â", "")
        '    '…and you're done.
        '    sReturnString = sConvertedString
        '    If Not MyWord Is Nothing Then
        '        MyWord.Quit(oDoNotSaveChanges)
        '        MyWord = Nothing
        '    End If
        'Catch ex As Exception
        '    If Not MyWord Is Nothing Then
        '        MyWord.Quit(oDoNotSaveChanges)
        '        MyWord = Nothing
        '    End If
        '    MsgBox("Error converting Rich Text to HTML")
        'End Try
        'Return sReturnString
    End Function

    Public Function Rtf_To_Html(rtfText As String) As String
        Try
            Using rtb As New RichTextBox()
                ' Load the RTF
                rtb.Rtf = rtfText

                ' Get plain text from RTF
                Dim plainText As String = rtb.Text

                ' Convert to very basic HTML (line breaks and encoding)
                Dim sb As New StringBuilder()
                sb.Append("<html><body>")

                ' Simple conversion: wrap lines in <p> tags
                For Each line In plainText.Split({Environment.NewLine}, StringSplitOptions.None)
                    If Not String.IsNullOrWhiteSpace(line) Then
                        sb.AppendFormat("<p>{0}</p>", System.Net.WebUtility.HtmlEncode(line))
                    End If
                Next

                sb.Append("</body></html>")
                Return sb.ToString()

            End Using
        Catch ex As Exception
            MessageBox.Show("Error converting RTF to HTML: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return String.Empty
        End Try
    End Function

    Public Function GetConfigAllReflexedIDs(ByVal ReflexerID As Integer) As String()
        Dim TrigedIDs() As String = {""}
        Try
            If IsQualitative(ReflexerID) Then
                Dim cncrd As New SqlConnection(connString)
                cncrd.Open()
                Dim cmdcrd As New SqlCommand("Select * from C_Triggers where Test_ID = " & ReflexerID, cncrd)
                cmdcrd.CommandType = CommandType.Text
                Dim drcrd As SqlDataReader = cmdcrd.ExecuteReader
                If drcrd.HasRows Then
                    While drcrd.Read
                        If TrigedIDs(UBound(TrigedIDs)) <> "" Then ReDim Preserve TrigedIDs(UBound(TrigedIDs) + 1)
                        TrigedIDs(UBound(TrigedIDs)) = drcrd("Marked_ID").ToString
                    End While
                End If
                cncrd.Close()
                cncrd = Nothing
            Else
                Dim cncrd As New SqlConnection(connString)
                cncrd.Open()
                Dim cmdcrd As New SqlCommand("Select * from N_Triggers where Test_ID = " & ReflexerID, cncrd)
                cmdcrd.CommandType = CommandType.Text
                Dim drcrd As SqlDataReader = cmdcrd.ExecuteReader
                If drcrd.HasRows Then
                    While drcrd.Read
                        If TrigedIDs(UBound(TrigedIDs)) <> "" Then ReDim Preserve TrigedIDs(UBound(TrigedIDs) + 1)
                        TrigedIDs(UBound(TrigedIDs)) = drcrd("Marked_ID").ToString
                    End While
                End If
                cncrd.Close()
                cncrd = Nothing
            End If
        Catch Ex As Exception
            'Write_Error_Log(152, Ex.Message)
            Return TrigedIDs
        End Try
        Return TrigedIDs
    End Function

    Public Function GetConfigReflexedIDs(ByVal TestID As Integer, ByVal Result As String) As String()
        Dim TrigIDs() As String = {""}
        Try
            If IsQualitative(TestID) Then
                Dim cngrid As New SqlConnection(connString)
                cngrid.Open()
                Dim cmdgrid As New SqlCommand("Select * from C_Triggers " &
                "where Test_ID = " & TestID & " and Choice = '" & Result & "'", cngrid)
                cmdgrid.CommandType = CommandType.Text
                Dim drgrid As SqlDataReader = cmdgrid.ExecuteReader
                If drgrid.HasRows Then
                    While drgrid.Read
                        If TrigIDs(UBound(TrigIDs)) <> "" Then ReDim Preserve TrigIDs(UBound(TrigIDs) + 1)
                        TrigIDs(UBound(TrigIDs)) = drgrid("Marked_ID").ToString
                    End While
                End If
                cngrid.Close()
                cngrid = Nothing
            Else
                Dim TempRes As String = Result
                Dim NumRes As Single = Nothing
                Dim Decs As Integer = GetTestDecimals(TestID)
                Dim Frac As Single = 1
                If Decs > 0 Then
                    Frac = 1 / 10 ^ Decs
                End If
                If InStr(TempRes, "<") > 0 Then
                    TempRes = Replace(TempRes, "<", "")
                    TempRes = Replace(TempRes, " ", "")
                    If IsNumeric(TempRes) Then
                        NumRes = CSng(TempRes) - Frac
                    End If
                ElseIf InStr(TempRes, ">") > 0 Then
                    TempRes = Replace(TempRes, ">", "")
                    TempRes = Replace(TempRes, " ", "")
                    If IsNumeric(TempRes) Then
                        NumRes = CSng(TempRes) + Frac
                    End If
                Else
                    TempRes = Replace(TempRes, " ", "")
                    If IsNumeric(TempRes) Then NumRes = CSng(TempRes)
                End If
                If NumRes <> Nothing Then
                    Dim cnrid As New SqlConnection(connString)
                    cnrid.Open()
                    Dim cmdrid As New SqlCommand("Select * from N_Triggers where " &
                    "Test_ID = " & TestID & " and " & NumRes & " between ValueFrom and ValueTo", cnrid)
                    cmdrid.CommandType = CommandType.Text
                    Dim drrid As SqlDataReader = cmdrid.ExecuteReader
                    If drrid.HasRows Then
                        While drrid.Read
                            If TrigIDs(UBound(TrigIDs)) <> "" Then ReDim Preserve TrigIDs(UBound(TrigIDs) + 1)
                            TrigIDs(UBound(TrigIDs)) = drrid("Marked_ID").ToString
                        End While
                    End If
                    cnrid.Close()
                    cnrid = Nothing
                Else
                    Dim NumVals() As String = GetNumVals(TestID, Trim(Result))
                    If NumVals(0) <> "" And NumVals(1) <> "" Then
                        Dim cnrid As New SqlConnection(connString)
                        cnrid.Open()
                        Dim cmdrid As New SqlCommand("Select * from N_Triggers where Test_ID = " & TestID &
                        " and ValueFrom <= " & CSng(NumVals(0)) & " and ValueTo >= " & CSng(NumVals(1)), cnrid)
                        cmdrid.CommandType = CommandType.Text
                        Dim drrid As SqlDataReader = cmdrid.ExecuteReader
                        If drrid.HasRows Then
                            While drrid.Read
                                If TrigIDs(UBound(TrigIDs)) <> "" Then ReDim Preserve TrigIDs(UBound(TrigIDs) + 1)
                                TrigIDs(UBound(TrigIDs)) = drrid("Marked_ID").ToString
                            End While
                        End If
                        cnrid.Close()
                        cnrid = Nothing
                    End If
                End If
            End If
        Catch Ex As Exception
            'Write_Error_Log(152, Ex.Message)
            Return TrigIDs
        End Try
        Return TrigIDs
    End Function

    Private Function GetNumVals(ByVal TestID As Integer, ByVal Result As String) As String()
        Dim Vals() As String = {"", ""}
        Dim cnnv As New SqlConnection(connString)
        cnnv.Open()
        Dim cmdnv As New SqlCommand("Select * from " &
        "N_Ranges where Test_ID = " & TestID & " and Flag = '" &
        Result & "'", cnnv)
        cmdnv.CommandType = CommandType.Text
        Dim drnv As SqlDataReader = cmdnv.ExecuteReader
        If drnv.HasRows Then
            While drnv.Read
                Vals(0) = drnv("ValueFrom").ToString
                Vals(1) = drnv("ValueTo").ToString
            End While
        End If
        cnnv.Close()
        cnnv = Nothing
        Return Vals
    End Function

    Public Function IsCodeComplete(ByVal TCode As String) As Boolean
        Dim Comp As Boolean = False
        Dim cncc As New SqlConnection(connString)
        cncc.Open()
        Dim cmdcc As New SqlCommand("Select Status " &
        "from DiagCodes where Code = '" & TCode & "'", cncc)
        cmdcc.CommandType = CommandType.Text
        Dim drcc As SqlDataReader = cmdcc.ExecuteReader
        If drcc.HasRows Then
            While drcc.Read
                If drcc("Status") IsNot DBNull.Value _
                AndAlso (drcc("Status") = "1") Then Comp = True
            End While
        End If
        cncc.Close()
        cncc = Nothing
        Return Comp
    End Function

    Public Function ValidateEmail(ByVal Email As String) As Boolean
        Dim EmailProper As Boolean = True
        If Trim(Email) = "" Then
            EmailProper = False
        ElseIf Len(Email) < 7 Then
            EmailProper = False
        ElseIf InStr(Email, "@") = 0 Or InStr(Email, ".") = 0 Then
            EmailProper = False
        ElseIf Not (Microsoft.VisualBasic.Right(Email, Len(Email) - InStrRev(Email, ".")) = "com" Or
                Microsoft.VisualBasic.Right(Email, Len(Email) - InStrRev(Email, ".")) = "info" Or
                Microsoft.VisualBasic.Right(Email, Len(Email) - InStrRev(Email, ".")) = "net" Or
                Microsoft.VisualBasic.Right(Email, Len(Email) - InStrRev(Email, ".")) = "biz" Or
                Microsoft.VisualBasic.Right(Email, Len(Email) - InStrRev(Email, ".")) = "us" Or
                Microsoft.VisualBasic.Right(Email, Len(Email) - InStrRev(Email, ".")) = "ca" Or
                Microsoft.VisualBasic.Right(Email, Len(Email) - InStrRev(Email, ".")) = "mobi" Or
                Microsoft.VisualBasic.Right(Email, Len(Email) - InStrRev(Email, ".")) = "gov" Or
                Microsoft.VisualBasic.Right(Email, Len(Email) - InStrRev(Email, ".")) = "org" Or
                Microsoft.VisualBasic.Right(Email, Len(Email) - InStrRev(Email, ".")) = "edu") Then
            EmailProper = False
        End If
        Return EmailProper
    End Function

    Public Function GetReported(ByVal AccID As Long) As String
        Dim Reported As String = ""
        Dim sSQL As String = "Select * from Requisitions where ID = " & AccID
        If connString <> "" Then
            Dim cngr As New SqlConnection(connString)
            cngr.Open()
            Dim cmdgr As New SqlCommand(sSQL, cngr)
            cmdgr.CommandType = CommandType.Text
            Dim drgr As SqlDataReader = cmdgr.ExecuteReader
            If drgr.HasRows Then
                While drgr.Read
                    If ReportFullResulted(AccID) Then 'Use Final
                        If drgr("Reported_Final") IsNot DBNull.Value _
                        AndAlso drgr("Reported_Final") <> "#12:00:00 AM#" Then _
                            Reported = drgr("Reported_Final").ToString
                    Else    'Partial
                        If drgr("ReportedOn") IsNot DBNull.Value _
                        AndAlso drgr("ReportedOn") <> "#12:00:00 AM#" Then _
                            Reported = drgr("ReportedOn").ToString
                    End If
                End While
            End If
            cngr.Close()
            cngr = Nothing
        End If
        Return Reported
    End Function

    Public Function GetExistingResult(ByVal AccID As Long, ByVal Cause0 As String,
    ByVal Cause1 As String, ByVal Cause2 As String, ByVal TestID As Integer) As String
        Dim Result As String = ""
        Dim sSQL As String = ""
        If Cause0.StartsWith("ACC") Then
            sSQL = "Select Result from Acc_Results where Accession_ID = " & AccID & " and Test_ID = " & TestID
        ElseIf Cause0.StartsWith("REF") Then
            sSQL = "Select Result from Ref_Results where Accession_ID = " & AccID & " and " &
            "Reflexer_ID = " & Cause1 & " and Reflexed_ID = " & Cause2 & " and Test_ID = " & TestID
        ElseIf Cause0.StartsWith("INF") Then
            sSQL = "Select Result from Acc_Info_Results where Accession_ID = " & AccID & " and Test_ID = " & Cause1 & " and Info_ID = " & TestID
        End If
        Dim cnger As New SqlConnection(connString)
        cnger.Open()
        Dim cmdger As New SqlCommand(sSQL, cnger)
        cmdger.CommandType = CommandType.Text
        Dim drger As SqlDataReader = cmdger.ExecuteReader
        If drger.HasRows Then
            While drger.Read
                If drger("Result") IsNot DBNull.Value _
                AndAlso Trim(drger("Result")) <> "" Then _
                Result = Trim(drger("Result"))
            End While
        End If
        cnger.Close()
        cnger = Nothing
        Return Result
    End Function

    Public Sub UpdateResultHistory(ByVal AccID As Long, ByVal TestID As Integer,
    ByVal Result As String, ByVal Changed As Date, ByVal UserID As Long)
        Dim sSQL As String = "If Exists (Select * from Acc_Results_History where Accession_ID = " & AccID & " and " &
        "Test_ID = " & TestID & ") Update Acc_Results_History Set ResultTime = '" & Changed & "', Result = '" &
        Result & "', User_ID = " & UserID & " where Accession_ID = " & AccID & " and Test_ID = " & TestID &
        " Else Insert into Acc_Results_History (Accession_ID, Test_ID, ResultTime, Result, User_ID) values (" &
        AccID & ", " & TestID & ", '" & Changed & "', '" & Result & "', " & UserID & ")"
        ExecuteSqlProcedure(sSQL)
    End Sub

    Public Function ResultHistoryExists(ByVal AccID As Long, ByVal TestID As Integer) As Boolean
        Dim HasHist As Boolean = False
        Dim sSQL As String = "Select * from Acc_Results_History where Accession_ID = " & AccID & " and Test_ID = " & TestID
        Dim cnrhe As New SqlConnection(connString)
        cnrhe.Open()
        Dim cmdrhe As New SqlCommand(sSQL, cnrhe)
        cmdrhe.CommandType = CommandType.Text
        Dim drrhe As SqlDataReader = cmdrhe.ExecuteReader
        If drrhe.HasRows Then HasHist = True
        cnrhe.Close()
        cnrhe = Nothing
        Return HasHist
    End Function

    Public Function GetReportStatus(ByVal AccID As Long) As String
        LoggerHelper.LogInfo("Loading GetReportStatus...")

        Dim Status As String = ""
        Dim sSQL As String = "Select * from Requisitions where Received <> 0 and ID = " & AccID
        Dim cnrs As New SqlConnection(connString)
        cnrs.Open()
        Dim cmdrs As New SqlCommand(sSQL, cnrs)
        cmdrs.CommandType = CommandType.Text
        Dim drrs As SqlDataReader = cmdrs.ExecuteReader
        If drrs.HasRows Then
            While drrs.Read
                If drrs("RPT_Status") IsNot DBNull.Value _
                AndAlso Trim(drrs("RPT_Status")) <> "" Then
                    Status = drrs("RPT_Status")
                ElseIf drrs("Reported_Final") IsNot DBNull.Value _
                AndAlso drrs("Reported_Final") <> "#12:00:00 AM#" Then
                    If drrs("RPT_Status") IsNot DBNull.Value _
                    AndAlso InStr(drrs("RPT_Status"), "FINAL") > 0 Then
                        Status = drrs("RPT_Status")
                    Else
                        Status = "FINAL"
                    End If
                ElseIf drrs("ReportedOn") IsNot DBNull.Value _
                AndAlso drrs("ReportedOn") <> "#12:00:00 AM#" Then    'Partial
                    Status = "PARTIAL"
                ElseIf drrs("Reported_Initial") IsNot DBNull.Value _
                AndAlso drrs("Reported_Initial") <> "#12:00:00 AM#" Then    'Initial
                    Status = "INITIAL"
                Else
                    Status = "PENDING"
                End If
            End While
        End If
        cnrs.Close()
        cnrs = Nothing
        '
        If InStr(Status, "FINAL") > 0 Or Status = "Partial" Then
            sSQL = "Update Requisitions set RPT_Status = '" &
            Status & "' where ID = " & AccID & " and not (RPT_Status = '" & Status & "')"
            ExecuteSqlProcedure(sSQL)
        End If
        '
        Return Status
    End Function

    Public Sub UpsertUser_Event(ByVal UserID As Long, ByVal EventID As Integer, ByVal Dated _
    As Date, ByVal ObjType As String, ByVal ObjID As Long, ByVal StatusFrom As String, ByVal StatusTo As String)
        Dim cnue As New SqlConnection(connString)
        cnue.Open()
        Dim cmdupsert As New SqlCommand("User_Event_SP", cnue)
        cmdupsert.CommandType = CommandType.StoredProcedure
        cmdupsert.Parameters.AddWithValue("@act", "Upsert")
        cmdupsert.Parameters.AddWithValue("@User_ID", UserID)
        cmdupsert.Parameters.AddWithValue("@Event_ID", EventID)
        cmdupsert.Parameters.AddWithValue("@Event_Time", Dated)
        cmdupsert.Parameters.AddWithValue("@Object_Type", ObjType)
        cmdupsert.Parameters.AddWithValue("@Object_ID", ObjID)
        cmdupsert.Parameters.AddWithValue("@StatusFrom", StatusFrom)
        cmdupsert.Parameters.AddWithValue("@StatusTo", StatusTo)
        Try
            cmdupsert.ExecuteNonQuery()
        Catch ex As Exception
            'MsgBox(ex.Message)
        Finally
            cnue.Close()
            cnue = Nothing
        End Try
    End Sub

    Public Function AccessionRejected(ByVal AccID As Long) As Boolean
        Dim Rejected As Boolean = False
        Dim cnrj As New SqlConnection(connString)
        cnrj.Open()
        Dim cmdrj As New SqlCommand("Select * from Requisitions where Rejected <> 0 and ID = " & AccID, cnrj)
        cmdrj.CommandTimeout = 90
        cmdrj.CommandType = CommandType.Text
        Dim drrj As SqlDataReader = cmdrj.ExecuteReader
        If drrj.HasRows Then Rejected = True
        cnrj.Close()
        cnrj = Nothing
        Return Rejected
    End Function

    Public Sub UpdateAccDisbursement(ByVal AccID As Long)
        Dim AskStatus() As String = GetAccAutoReportable(AccID, "Interface")  '0=Ask, 1=status
        Try
            Dim FacilityID As Integer = -1
            Dim cnhi As New SqlConnection(connString)
            cnhi.Open()
            Dim cmdhi As New SqlCommand("Select * from External_Interfaces " &
            "where IsActive <> 0 and FacilityType_ID = 5 and Facility_ID in (Select " &
            "OrderingProvider_ID from Requisitions where ID = " & AccID & ")", cnhi)
            cmdhi.CommandTimeout = 90
            cmdhi.CommandType = CommandType.Text
            Dim drhi As SqlDataReader = cmdhi.ExecuteReader
            If drhi.HasRows Then
                FacilityID = drhi("Facility_ID")
            End If
            cnhi.Close()
            cnhi = Nothing
            If FacilityID <> -1 And Not AskStatus(1).Contains("FINAL") Then
                ExecuteSqlProcedure("Delete from Event_Capture where Event_ID = 14 and Accession_ID = " & AccID &
                " and Provider_ID in (Select OrderingProvider_ID from Requisitions where ID = " & AccID & ")")
            End If
        Catch Ex As Exception
        End Try
    End Sub

    Public Function RunPanicAlert() As Boolean
        Dim HasPanic As Boolean = False
        Dim sSQL As String = "Select a.ID as AccID from Requisitions a inner join Acc_Results b on a.ID = " &
        "b.Accession_ID where b.Released <> 0 and (b.Behavior = 'Panic' or b.Flag like '%Panic%' or " &
        "b.Flag like '%HP%' or b.Flag like '%LP%') and Not a.ID in (Select c.Accession_ID from " &
        "Panic_Notifications c where c.Test_ID = b.Test_ID) and a.AccessionDate between '" &
        Format(DateAdd(DateInterval.Day, -SystemConfig.PanicSpan, Date.Today), "MM/dd/yyyy HH:mm:ss") _
        & "' and '" & Format(Date.Now, "MM/dd/yyyy HH:mm:ss") & "'"
        Dim cnrpa As New SqlConnection(connString)
        cnrpa.Open()
        Dim cmdrpa As New SqlCommand(sSQL, cnrpa)
        cmdrpa.CommandType = CommandType.Text
        Dim drrpa As SqlDataReader = cmdrpa.ExecuteReader
        If drrpa.HasRows Then HasPanic = True
        cnrpa.Close()
        cnrpa = Nothing
        Return HasPanic
    End Function

    Public Function ProlisScriptFunc(ByRef Formula As String) As String
        ProlisScriptFunc = Formula
    End Function

    Public Function GetInterfaceID(ByVal FacilityTypeID As Integer, ByVal FacilityID As Long) As String
        Dim FacID As String = ""
        Dim cnhi As New SqlConnection(connString)
        cnhi.Open()
        Dim cmdhi As New SqlCommand("Select ID from External_Interfaces " &
        "where FacilityType_ID = " & FacilityTypeID & " and Facility_ID = " & FacilityID, cnhi)
        cmdhi.CommandType = CommandType.Text
        Dim drhi As SqlDataReader = cmdhi.ExecuteReader
        If drhi.HasRows Then
            While drhi.Read
                FacID = drhi("ID").ToString
            End While
        End If
        cnhi.Close()
        cnhi = Nothing
        Return FacID
    End Function

    Public Function GetAccessionDate(ByVal AccID As Long) As String
        Dim AccDate As String = ""
        Dim cnad As New SqlConnection(connString)
        cnad.Open()
        Dim cmdad As New SqlCommand("Select AccessionDate " &
        "from Requisitions where ID = " & AccID, cnad)
        cmdad.CommandType = CommandType.Text
        Dim drad As SqlDataReader = cmdad.ExecuteReader
        If drad.HasRows Then
            AccDate = drad("AccessionDate").ToString
        End If
        cnad.Close()
        cnad = Nothing
        Return AccDate
    End Function

    Public Function GetUserID(ByVal Name As String) As Long
        Dim UserID As Long = -1
        Dim cnui As New SqlConnection(connString)
        cnui.Open()
        Dim cmdui As New SqlCommand("Select * " &
        "from Users where username = '" & Name & "'", cnui)
        cmdui.CommandType = CommandType.Text
        Dim drui As SqlDataReader = cmdui.ExecuteReader
        If drui.HasRows Then
            UserID = drui("ID")
        End If
        cnui.Close()
        cnui = Nothing
        Return UserID
    End Function

    Private Function IsTestResultable(ByVal TGPID As Integer) As Boolean
        Dim Resultable As Boolean = False
        Dim cnitr As New SqlConnection(connString)
        cnitr.Open()
        Dim cmditr As New SqlCommand("Select ID from Tests where HasResult <> 0 and ID = " &
        TGPID & " Union Select ID from Tests where HasResult <> 0 and ID in (Select TGP_ID from " &
        "TGP_Info where Info_ID = " & TGPID & ")", cnitr)
        cmditr.CommandType = CommandType.Text
        Dim dritr As SqlDataReader = cmditr.ExecuteReader
        If dritr.HasRows Then Resultable = True
        cnitr.Close()
        cnitr = Nothing
        Return Resultable
    End Function

    Private Function UpdateResultBillStatus(ByVal AccID As Long, ByVal TGPID _
    As Integer, ByVal TGPType As String, ByVal EXCEP As String) As String
        Dim UnBillRes() As String = Split(EXCEP, "|")
        Dim BADRES As String = ""
        Dim i As Integer
        For i = 0 To UnBillRes.Length - 1
            If Trim(UnBillRes(i)) <> "" Then
                BADRES += "'" & Trim(UnBillRes(i)) & "', "
            End If
        Next
        If BADRES.EndsWith(", ") Then BADRES = Microsoft.VisualBasic.Mid(BADRES, 1, Len(BADRES) - 2)
        Dim Billable As String = "0"
        If BADRES = "" Or IsTestResultable(TGPID) = False Then
            Billable = "1"
        Else
            Dim sSQL As String = ""
            If TGPType = "T" Then
                sSQL = "Select distinct Accession_ID from Acc_Results where Test_ID = " & TGPID & " and Accession_ID = " &
                AccID & " and not Result in (" & BADRES & ") Union Select distinct a.Accession_ID from Acc_Results a inner " &
                "join (TGP_Info b inner join Tests c on c.ID = b.Info_ID) on b.TGP_ID = a.Test_ID where c.ID = " & TGPID &
                " and Accession_ID = " & AccID & " and not Result in (" & BADRES & ") Union Select distinct Accession_ID " &
                "from Acc_Info_Results where Info_ID = " & TGPID & " and Accession_ID = " & AccID & " and not Result in (" &
                BADRES & ") Union Select distinct Accession_ID from Ref_Results where Test_ID = " & TGPID & " and " &
                "Accession_ID = " & AccID & " and not Result in (" & BADRES & ")"
                If connString <> "" Then
                    Dim cnrbs As New SqlConnection(connString)
                    cnrbs.Open()
                    Dim cmdrbs As New SqlCommand(sSQL, cnrbs)
                    cmdrbs.CommandType = CommandType.Text
                    Dim drrbs As SqlDataReader = cmdrbs.ExecuteReader
                    If drrbs.HasRows Then Billable = "1"
                    cnrbs.Close()
                    cnrbs = Nothing
                Else
                    Dim cnrbs As New sqlConnection(connString)
                    cnrbs.Open()
                    Dim cmdrbs As New sqlCommand(sSQL, cnrbs)
                    cmdrbs.CommandType = CommandType.Text
                    Dim drrbs As sqlDataReader = cmdrbs.ExecuteReader
                    If drrbs.HasRows Then Billable = "1"
                    cnrbs.Close()
                    cnrbs = Nothing
                End If
            ElseIf TGPType = "G" Then
                sSQL = "Select distinct Accession_ID from acc_Results where Accession_ID = " & AccID & " and Test_ID in " &
                "(Select Test_ID from Group_Test where Group_ID = " & TGPID & ") and Not Result in (" & BADRES & ") Union " &
                "Select distinct Accession_ID from acc_Info_Results where Accession_ID = " & AccID & " and Info_ID in (Select " &
                "Test_ID from Group_Test where Group_ID = " & TGPID & ") and Not Result in (" & BADRES & ") Union Select " &
                "distinct Accession_ID from Ref_Results where Accession_ID = " & AccID & " and Test_ID in (Select Test_ID " &
                "from Group_Test where Group_ID = " & TGPID & ") and Not Result in (" & BADRES & ")"
                If connString <> "" Then
                    Dim cnrbs As New SqlConnection(connString)
                    cnrbs.Open()
                    Dim cmdrbs As New SqlCommand(sSQL, cnrbs)
                    cmdrbs.CommandType = CommandType.Text
                    Dim drrbs As SqlDataReader = cmdrbs.ExecuteReader
                    If drrbs.HasRows Then Billable = "1"
                    cnrbs.Close()
                    cnrbs = Nothing
                Else
                    Dim cnrbs As New sqlConnection(connString)
                    cnrbs.Open()
                    Dim cmdrbs As New sqlCommand(sSQL, cnrbs)
                    cmdrbs.CommandType = CommandType.Text
                    Dim drrbs As sqlDataReader = cmdrbs.ExecuteReader
                    If drrbs.HasRows Then Billable = "1"
                    cnrbs.Close()
                    cnrbs = Nothing
                End If
            Else    'Profile
                sSQL = "Select distinct Accession_ID from Acc_Results where Accession_ID = " & AccID & " and Test_ID in (Select Test_ID " &
                "from Group_Test where Group_ID in (Select GrpTst_ID from Prof_GrpTst where Profile_ID = " & TGPID & ") Union Select " &
                "GrpTst_ID as Test_ID from Prof_GrpTst where Profile_ID = " & TGPID & ") and Not Result in (" & BADRES & ") Union Select " &
                "distinct Accession_ID from Acc_Info_Results where Accession_ID = " & AccID & " and Info_ID in (Select Test_ID from " &
                "Group_Test where Group_ID in (Select GrpTst_ID from Prof_GrpTst where Profile_ID = " & TGPID & ") Union Select GrpTst_ID " &
                "as Test_ID from Prof_GrpTst where Profile_ID = " & TGPID & ") and Not Result in (" & BADRES & ") Union Select distinct " &
                "Accession_ID from Ref_Results where Accession_ID = " & AccID & " and Test_ID in (Select Test_ID from Group_Test where " &
                "Group_ID in (Select GrpTst_ID from Prof_GrpTst where Profile_ID = " & TGPID & ") Union Select GrpTst_ID as Test_ID from " &
                "Prof_GrpTst where Profile_ID = " & TGPID & ") and Not Result in (" & BADRES & ")"
                If connString <> "" Then
                    Dim cnrbs As New SqlConnection(connString)
                    cnrbs.Open()
                    Dim cmdrbs As New SqlCommand(sSQL, cnrbs)
                    cmdrbs.CommandType = CommandType.Text
                    Dim drrbs As SqlDataReader = cmdrbs.ExecuteReader
                    If drrbs.HasRows Then Billable = "1"
                    cnrbs.Close()
                    cnrbs = Nothing
                Else
                    Dim cnrbs As New sqlConnection(connString)
                    cnrbs.Open()
                    Dim cmdrbs As New sqlCommand(sSQL, cnrbs)
                    cmdrbs.CommandType = CommandType.Text
                    Dim drrbs As sqlDataReader = cmdrbs.ExecuteReader
                    If drrbs.HasRows Then Billable = "1"
                    cnrbs.Close()
                    cnrbs = Nothing
                End If
            End If
        End If
        Return Billable
    End Function

    Public Function RemoveLowestOfMDArray(ByVal arr(,) As String) As Array
        Dim UB As Long = arr.GetUpperBound(1)
        Dim LB As Long = arr.GetLowerBound(1)
        If LB < UB Then
            Dim arrLen As Long = UB - LB
            Dim outArr(UBound(arr, 1), arrLen - 1)
            Array.Copy(arr, LB + 1, outArr, 0, arrLen - 1)
            '
            Return outArr
        Else
            Return arr
        End If
    End Function

    Public Function GetMedications(ByVal AccID As String) As String
        Dim Meds As String = ""
        Dim sSQL As String = "Select Medication from Req_Med where Accession_ID = " & AccID
        '
        Dim cnm As New SqlConnection(connString)
        cnm.Open()
        Dim cmdm As New SqlCommand(sSQL, cnm)
        cmdm.CommandType = CommandType.Text
        Dim drm As SqlDataReader = cmdm.ExecuteReader
        If drm.HasRows Then
            While drm.Read
                If drm("Medication") IsNot DBNull.Value _
                AndAlso Trim(drm("Medication")) <> "" Then _
                Meds += Trim(drm("Medication")) & ", "
            End While
            If Meds.EndsWith(", ") Then Meds = Microsoft.VisualBasic.Mid(Meds, 1, Len(Meds) - 2)
        End If
        cnm.Close()
        cnm = Nothing
        Return Meds
    End Function

    Public Function GetServiceDate(ByVal AccID As Long) As Date
        Dim SvcDate As Date = Date.Now
        Dim sSQL As String = "Select AccessionDate, (Select Min(SourceDate) from Specimens where " &
        "Accession_ID = " & AccID & ") as CollDate from Requisitions where ID = " & AccID
        Dim cnsd As New SqlConnection(connString)
        cnsd.Open()
        Dim cmdsd As New SqlCommand(sSQL, cnsd)
        cmdsd.CommandType = CommandType.Text
        Dim drsd As SqlDataReader = cmdsd.ExecuteReader
        If drsd.HasRows Then
            While drsd.Read
                If drsd("CollDate") IsNot DBNull.Value AndAlso
                drsd("CollDate") <> "#12:00:00AM#" Then
                    SvcDate = drsd("CollDate")
                Else
                    SvcDate = drsd("AccessionDate")
                End If
            End While
        End If
        cnsd.Close()
        cnsd = Nothing
        Return SvcDate
    End Function

    Private Function UpdateDxPointers(ByVal AccID As Long, ByVal TGPID As Integer) As String
        Dim Ptrs As String = ""
        Dim Codes As String = ""
        Dim sSQL As String = "Select * from Req_Dx where Accession_ID = " & AccID & " order by Ordinal"
        Dim cndp As New SqlConnection(connString)
        cndp.Open()
        Dim cmddp As New SqlCommand(sSQL, cndp)
        cmddp.CommandType = CommandType.Text
        Dim drdp As SqlDataReader = cmddp.ExecuteReader
        If drdp.HasRows Then
            While drdp.Read
                If drdp("Dx_Code") IsNot DBNull.Value _
                AndAlso Trim(drdp("Dx_Code")) <> "" Then
                    Codes += Trim(drdp("Dx_Code")) & "|"
                End If
            End While
            If Codes.EndsWith("|") Then Codes = Codes.Substring(0, Len(Codes) - 1)
        End If
        cndp.Close()
        cndp = Nothing
        '
        If Codes <> "" Then
            Ptrs = GetNecessityPtrs(TGPID, Codes)
        Else
            Ptrs = ""
        End If
        Return Ptrs
    End Function

    Private Function GetNecessityPtrs(ByVal TGPID As Integer, ByVal ICD9S As String) As String
        Dim POINTERS() As String = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"}
        Dim Ptr As String = ""
        Dim Codes() As String = Split(ICD9S, "|")
        If Codes.Length > POINTERS.Length Then ReDim Preserve Codes(POINTERS.Length - 1)
        Dim Dxs As String = "'" & Join(Codes, "', '") & "'"
        Dim IsOpenMileage As Boolean = IsTGPOpenMileage(TGPID)
        If TGPHasNecessity(TGPID) = False Then    'No Necessity
            If Trim(Codes(0)) <> "" And Not IsOpenMileage Then Ptr = "1"
        Else    'Found Necessity
            For i As Integer = 0 To Codes.Length - 1
                If Trim(Codes(i)) <> "" Then
                    If IsDxValid(TGPID, Trim(Codes(i))) Then
                        If InStr(Ptr, POINTERS(i)) = 0 Then
                            Ptr += POINTERS(i) & ","
                        End If
                    End If
                End If
                'If GetPtrVal(Ptr) >= 5 Then Exit For
            Next
        End If
        If Ptr.EndsWith(",") Then Ptr = Ptr.Substring(0, Len(Ptr) - 1)
        Return Ptr
    End Function

    Private Function TGPHasNecessity(ByVal TGPID As Integer) As Boolean
        Dim HasNec As Boolean = False
        Dim sSQL As String = "Select Dx_Code from MedicalNecessity where " &
        "CPT_Code in (Select CPT_Code from Tests where ID = " & TGPID &
        " Union Select CPT_Code from Groups where ID = " & TGPID & " Union " &
        "Select CPT_Code from Profiles where ID = " & TGPID & ")"
        Dim cnhn As New SqlConnection(connString)
        cnhn.Open()
        Dim cmdhn As New SqlCommand(sSQL, cnhn)
        cmdhn.CommandType = CommandType.Text
        Dim drhn As SqlDataReader = cmdhn.ExecuteReader
        If drhn.HasRows Then HasNec = True
        cnhn.Close()
        cnhn = Nothing
        Return HasNec
    End Function

    Private Function IsDxValid(ByVal TGPID As Integer, ByVal Dx As String) As Boolean
        Dim DxValid As Boolean = False
        Dim sSQL As String = "Select * from MedicalNecessity where Dx_Code = '" &
        Dx & "' and CPT_Code in (Select CPT_Code from Tests where ID = " & TGPID &
        " Union Select CPT_Code from Groups where ID = " & TGPID & " Union " &
        "Select CPT_Code from Profiles where ID = " & TGPID & ")"
        Dim cndx As New SqlConnection(connString)
        cndx.Open()
        Dim cmddx As New SqlCommand(sSQL, cndx)
        cmddx.CommandType = CommandType.Text
        Dim drdx As SqlDataReader = cmddx.ExecuteReader
        If drdx.HasRows Then DxValid = True
        cndx.Close()
        cndx = Nothing
        Return DxValid
    End Function

    Private Function GetPtrVal(ByVal Ptr As String) As Integer
        Dim VAL As Integer = 0
        Dim PTRS() As String
        If Ptr <> "" Then
            If InStr(Ptr, ",") > 0 Then 'has coma
                PTRS = Split(Ptr, ",")
                VAL = PTRS.Length
            Else
                VAL = 1
            End If
        End If
        Return VAL
    End Function

    Private Function IsTGPOpenMileage(ByVal TGPID As Integer) As Boolean
        Dim OPM As Boolean = False
        Dim sSQL As String = "Select CPT_Code from Tests where ID = " &
        TGPID & " Union Select CPT_Code from Groups where ID = " & TGPID &
        " Union Select CPT_Code from Profiles where ID = " & TGPID
        Dim cnom As New SqlConnection(connString)
        cnom.Open()
        Dim cmdom As New SqlCommand(sSQL, cnom)
        cmdom.CommandType = CommandType.Text
        Dim drom As SqlDataReader = cmdom.ExecuteReader
        If drom.HasRows Then
            While drom.Read
                If drom("CPT_Code") IsNot DBNull.Value AndAlso
                Trim(drom("CPT_Code")) = "P9603" Then OPM = True
            End While
        End If
        cnom.Close()
        cnom = Nothing
        Return OPM
    End Function

    Private Function GetChildren(ByVal Father As Integer) As String
        Dim children As String = ""
        Dim cngc As New SqlConnection(connString)
        cngc.Open()
        Dim cmdgc As New SqlCommand("Select Info_ID from TGP_Info where TGP_ID = " & Father, cngc)
        cmdgc.CommandType = CommandType.Text
        Dim drgc As SqlDataReader = cmdgc.ExecuteReader
        If drgc.HasRows Then
            While drgc.Read
                children += drgc("Info_ID").ToString & ", "
            End While
        End If
        cngc.Close()
        cngc = Nothing
        Return children
    End Function

    Private Function ApplyPayerRules(ByVal PayerID As Long, ByVal BillCandidates(,) As String) As String(,)
        Dim Found As Boolean = True
        Dim TBBIDS As String = ""
        Dim i As Integer
        For i = 0 To UBound(BillCandidates, 2)
            TBBIDS += BillCandidates(0, i) & ", "
        Next
        Dim cnapr As New SqlConnection(connString)
        cnapr.Open()
        Dim cmdapr As New SqlCommand("Select Origin, Target from Payer_BillRules " &
        "where Element = 'TGP_ID' and Action = 'Replace' and Payer_ID = " & PayerID, cnapr)
        cmdapr.CommandType = CommandType.Text
        Dim drapr As SqlDataReader = cmdapr.ExecuteReader
        If drapr.HasRows Then
            While drapr.Read
                If (drapr("Origin") IsNot DBNull.Value AndAlso
                Trim(drapr("Origin")) <> "") AndAlso (drapr("Target") _
                IsNot DBNull.Value AndAlso drapr("Target") <> "") Then
                    Dim TGPSF As String = Trim(drapr("Origin"))
                    Dim TGPT As String = Trim(drapr("Target"))
                    If TGPSF.StartsWith("|") Then TGPSF = Microsoft.VisualBasic.Mid(TGPSF, 2)
                    If TGPSF.EndsWith("|") Then TGPSF = Microsoft.VisualBasic.Mid(TGPSF, 1, Len(TGPSF) - 1)
                    Dim FTGPS() As String = Split(TGPSF, "|")
                    '
                    Found = True
                    For i = 0 To FTGPS.Length - 1
                        If InStr(TBBIDS, FTGPS(i) & ", ") = 0 Then
                            Found = False
                            Exit For
                        End If
                    Next
                    If Found Then
                        For i = 0 To FTGPS.Length - 1
                            TBBIDS = Replace(TBBIDS, FTGPS(i) & ", ", "")
                        Next
                        TBBIDS += TGPT & ", "
                    End If
                End If
            End While
        End If
        cnapr.Close()
        cnapr = Nothing
        If Microsoft.VisualBasic.Right(TBBIDS, 2) = ", " Then _
        TBBIDS = Microsoft.VisualBasic.Mid(TBBIDS, 1, Len(TBBIDS) - 2)
        Dim Billables(1, 0) As String
        Dim HasUnit As Boolean = False
        Dim TMPS() As String = Split(TBBIDS, ",")
        For t As Integer = 0 To TMPS.Length - 1
            If Billables(1, UBound(Billables, 2)) <> "" Then _
            ReDim Preserve Billables(1, UBound(Billables, 2) + 1)
            Billables(0, UBound(Billables, 2)) = TMPS(t)
            HasUnit = False
            For i = 0 To UBound(BillCandidates, 2)
                If Trim(TMPS(t)) = BillCandidates(0, i) Then
                    Billables(1, UBound(Billables, 2)) = BillCandidates(1, i)
                    HasUnit = True
                    Exit For
                End If
            Next
            If Not HasUnit Then Billables(1, UBound(Billables, 2)) = "1"
        Next
        Return Billables
    End Function

    Private Function GetTGPSToBreak(ByVal AccID As Long) As String()
        Dim TGPSTB() As String = {""}
        Dim TGPS() As String = {""}
        Dim sSQL As String = "Select Origin from Payer_BillRules where Action = 'BREAK' " &
        "and Payer_ID in (Select Payer_ID from Req_Coverage where Preference = 'P' and " &
        "Accession_ID = " & AccID & ")"
        Dim cntb As New SqlConnection(connString)
        cntb.Open()
        Dim cmdtb As New SqlCommand(sSQL, cntb)
        cmdtb.CommandType = CommandType.Text
        Dim drtb As SqlDataReader = cmdtb.ExecuteReader
        If drtb.HasRows Then
            While drtb.Read
                If InStr(drtb("Origin"), "|") > 0 Then 'composite
                    TGPS = Split(drtb("Origin"), "|")
                Else
                    TGPS(0) = drtb("Origin")
                End If
                '
                For i As Integer = 0 To TGPS.Length - 1
                    If Trim(TGPS(i)) <> "" Then
                        If TGPSTB(UBound(TGPSTB)) <> "" Then _
                        ReDim Preserve TGPSTB(UBound(TGPSTB) + 1)
                        TGPSTB(UBound(TGPSTB)) = Trim(TGPS(i))
                    End If
                Next
            End While
        End If
        cntb.Close()
        cntb = Nothing
        Return TGPSTB
    End Function

    Private Function UpdateWBrkBillables(ByVal AccID As Long, ByVal TGPSTB() As String) As String(,)
        Dim Billables(1, 0) As String   '0 = TBBID, 1 = Units
        Dim TGPType As String = ""
        Dim TGType As String = ""
        Dim sSQL As String = ""
        For i As Integer = 0 To TGPSTB.Length - 1
            If TGPSTB(i) <> "" Then
                TGPType = GetTGPType(TGPSTB(i))
                If TGPType = "G" Then
                    sSQL = "Select ID, Bill_Unit from Tests where IsActive <> 0 and CPT_Code <> '' and " &
                    "TBillable <> 0 and ID in (Select a.Test_ID from Group_Test a inner join Req_TGP b on " &
                    "b.TGP_ID = a.Group_ID where b.TGP_ID = " & TGPSTB(i) & " and b.Accession_ID = " & AccID & ")"
                    Dim cnt As New SqlConnection(connString)
                    cnt.Open()
                    Dim cmdt As New SqlCommand(sSQL, cnt)
                    cmdt.CommandType = CommandType.Text
                    Dim drt As SqlDataReader = cmdt.ExecuteReader
                    If drt.HasRows Then
                        While drt.Read
                            If Billables(1, UBound(Billables, 2)) <> "" Then _
                            ReDim Preserve Billables(1, UBound(Billables, 2) + 1)
                            Billables(0, UBound(Billables, 2)) = drt("ID").ToString
                            Billables(1, UBound(Billables, 2)) = drt("Bill_Unit").ToString
                        End While
                    End If
                    cnt.Close()
                    cnt = Nothing
                ElseIf TGPType = "P" Then
                    sSQL = "Select a.GrpTst_ID from Prof_GrpTst a inner join Req_TGP b on a.Profile_ID = " &
                    "b.TGP_ID where a.Profile_ID = " & TGPSTB(i) & " and b.Accession_ID = " & AccID
                    Dim cnp As New SqlConnection(connString)
                    cnp.Open()
                    Dim cmdp As New SqlCommand(sSQL, cnp)
                    cmdp.CommandType = CommandType.Text
                    Dim drp As SqlDataReader = cmdp.ExecuteReader
                    If drp.HasRows Then
                        While drp.Read
                            TGType = GetTGPType(drp("GrpTst_ID"))
                            If TGType = "T" Then    'Test within a profile
                                Dim cnt As New SqlConnection(connString)
                                cnt.Open()
                                Dim cmdt As New SqlCommand("Select ID, Bill_Unit from Tests where " &
                                "IsActive <> 0 and CPT_Code <> '' and TBillable <> 0 and ID = " & drp("GrpTst_ID"), cnt)
                                cmdt.CommandType = CommandType.Text
                                Dim drt As SqlDataReader = cmdt.ExecuteReader
                                If drt.HasRows Then
                                    While drt.Read
                                        If Billables(1, UBound(Billables, 2)) <> "" Then _
                                        ReDim Preserve Billables(1, UBound(Billables, 2) + 1)
                                        Billables(0, UBound(Billables, 2)) = drt("ID").ToString
                                        Billables(1, UBound(Billables, 2)) = drt("Bill_Unit").ToString
                                    End While
                                End If
                                cnt.Close()
                                cnt = Nothing
                            Else   'Group within a profile
                                If GrpToBreak(drp("GrpTst_ID"), TGPSTB) Then
                                    Dim cngt As New SqlConnection(connString)
                                    cngt.Open()
                                    Dim cmdgt As New SqlCommand("Select ID, Bill_Unit from Tests " &
                                    "where IsActive <> 0 and CPT_Code <> '' and TBillable <> 0 and ID in (Select " &
                                    "Test_ID from Group_Test where Group_ID = " & drp("GrpTst_ID") & ")", cngt)
                                    cmdgt.CommandType = CommandType.Text
                                    Dim drgt As SqlDataReader = cmdgt.ExecuteReader
                                    If drgt.HasRows Then
                                        While drgt.Read
                                            If Billables(1, UBound(Billables, 2)) <> "" Then _
                                            ReDim Preserve Billables(1, UBound(Billables, 2) + 1)
                                            Billables(0, UBound(Billables, 2)) = drgt("ID").ToString
                                            Billables(1, UBound(Billables, 2)) = drgt("Bill_Unit").ToString
                                        End While
                                    End If
                                    cngt.Close()
                                    cngt = Nothing
                                Else
                                    Dim cng As New SqlConnection(connString)
                                    cng.Open()
                                    Dim cmdg As New SqlCommand("Select ID, Bill_Unit from Groups where " &
                                    "IsActive <> 0 and CPT_Code <> '' and TBillable <> 0 and ID = " & drp("GrpTst_ID"), cng)
                                    cmdg.CommandType = CommandType.Text
                                    Dim drg As SqlDataReader = cmdg.ExecuteReader
                                    If drg.HasRows Then
                                        While drg.Read
                                            If Billables(1, UBound(Billables, 2)) <> "" Then _
                                            ReDim Preserve Billables(1, UBound(Billables, 2) + 1)
                                            Billables(0, UBound(Billables, 2)) = drg("ID").ToString
                                            Billables(1, UBound(Billables, 2)) = drg("Bill_Unit").ToString
                                        End While
                                    End If
                                    cng.Close()
                                    cng = Nothing
                                End If
                            End If
                        End While
                    End If
                    cnp.Close()
                    cnp = Nothing
                End If
            End If
        Next
        Return Billables
    End Function

    Private Function GrpToBreak(ByVal GrpID As String, ByVal TGPSTB() As String) As Boolean
        Dim GTB As Boolean = False
        For i As Integer = 0 To TGPSTB.Length - 1
            If TGPSTB(i) = GrpID Then
                GTB = True
                Exit For
            End If
        Next
        Return GTB
    End Function

    Private Function GetTPBillables(ByVal AccID As Long) As String(,)
        Dim Billables(1, 0) As String   '0 = TBBID, 1 = Units
        Dim TGPSTB() As String = GetTGPSToBreak(AccID)
        Dim Brokens As String = ""
        If TGPSTB(0) <> "" Then
            Billables = UpdateWBrkBillables(AccID, TGPSTB)
            Brokens = Join(TGPSTB, ", ")
        End If
        Dim sSQL As String = ""
        If Brokens <> "" Then
            sSQL = "Select * from vReqTBillable where Not TBBID in (" & Brokens & ") and AccID = " & AccID
        Else
            sSQL = "Select * from vReqTBillable where AccID = " & AccID
        End If
        Dim cnb As New SqlConnection(connString)
        cnb.Open()
        Dim cmdb As New SqlCommand(sSQL, cnb)
        cmdb.CommandType = CommandType.Text
        Dim drb As SqlDataReader = cmdb.ExecuteReader
        If drb.HasRows Then
            While drb.Read
                If Billables(1, UBound(Billables, 2)) <> "" Then _
                ReDim Preserve Billables(1, UBound(Billables, 2) + 1)
                Billables(0, UBound(Billables, 2)) = drb("TBBID")
                Billables(1, UBound(Billables, 2)) = drb("Units")
            End While
        End If
        cnb.Close()
        cnb = Nothing
        Return Billables
    End Function

    Public Function SynchronizeAccession(ByVal AccID As String, ByVal bResults As Boolean, ByVal EXCEP As String) As String()
        Dim Outputs() As String = {AccID.ToString, "", ""}
        Dim BillType As Integer = -1
        Dim ArID As Long = -1
        Dim TGPDET() As String
        Dim TSTDET() As String
        Dim PRODET() As String
        Dim OSDET() As String
        Dim SynchedTGPs As String = ""
        Dim Children As String = ""
        Dim FatherBill As String = ""
        Dim Billables(1, 0) As String
        Dim TBBIDS As String = ""
        Dim i As Integer = 0
        Dim Svc_Date As Date
        Dim sSQL As String = "Select * from Requisitions where IsGratis = 0 and (InHouse <> 0 or (InHouse = 0 " &
        "and ID in (Select Accession_ID from Sendouts where BillingType_ID <> 1))) and ID = " & AccID
        Dim cnbas As New SqlConnection(connString)
        cnbas.Open()
        Dim cmdbas As New SqlCommand(sSQL, cnbas)
        cmdbas.CommandType = CommandType.Text
        Dim drbas As SqlDataReader = cmdbas.ExecuteReader
        If drbas.HasRows Then
            While drbas.Read
                BillType = drbas("BillingType_ID")
                If drbas("PrimePayer_ID") IsNot DBNull.Value _
                Then ArID = drbas("PrimePayer_ID")
                Svc_Date = GetServiceDate(AccID)
            End While
        End If
        cnbas.Close()
        cnbas = Nothing
        '
        If BillType <> -1 And ArID <> -1 Then  'chargeable
            If BillType = 1 Then    'Insurance
                Billables = GetTPBillables(AccID)
                Billables = ApplyPayerRules(ArID, Billables)
            Else
                If BillType = 0 Then    'Client
                    sSQL = "Select TBBID, Units from vReqCBillable where AccID = " & AccID
                Else
                    sSQL = "Select TBBID, Units from vReqPBillable where AccID = " & AccID
                End If
                '
                Dim cnbills As New SqlConnection(connString)
                cnbills.Open()
                Dim cmdbills As New SqlCommand(sSQL, cnbills)
                cmdbills.CommandType = CommandType.Text
                Dim drbills As SqlDataReader = cmdbills.ExecuteReader
                If drbills.HasRows Then
                    While drbills.Read
                        If Billables(1, UBound(Billables, 2)) <> "" Then _
                        ReDim Preserve Billables(1, UBound(Billables, 2) + 1)
                        Billables(0, UBound(Billables, 2)) = drbills("TBBID")
                        Billables(1, UBound(Billables, 2)) = drbills("Units")
                    End While
                End If
                cnbills.Close()
                cnbills = Nothing
            End If
        Else
            Outputs(1) = "Failed"
            Outputs(2) = "Gratis or billed by Reference Lab"
        End If
        '
        For b As Integer = 0 To UBound(Billables, 2)
            If Billables(0, b) <> "" Then
                TGPDET = GetTGPDetail(BillType, ArID, Billables(0, b), AccID)
                TGPDET(6) = Billables(1, b)
                '0=TGPID, 1=Type, 2=Billable, 3=CPT, 4=ICD9, 5=PRICE, 
                '6=Unit, 7=Mod1, 8=Mod2, 0=Mod3, 10=Mod4, 11=POS_Code
                If TGPDET(2) = "1" AndAlso bResults = True Then TGPDET(2) =
                UpdateResultBillStatus(AccID, TGPDET(0), TGPDET(1), EXCEP)
                'BilledTGP = GetTGPBillStatus(BillType, ArID, AccID, Rs.Fields("TGP_ID").Value)
                '0=TGPID, 1=BillStatus, 2=CPT, 3=Dx, 4=UnitPrice, 5=Unit, 6=BillDate, 
                '7=Billedby, 8=Mod1, 9=Mod2, 10=Mod3, 11=Mod4, 12=POS_Code
                OSDET = GetOSDetail(AccID, TGPDET(0), TGPDET(6))
                If OSDET(4) <> "" Then TGPDET(6) = OSDET(4)
                If OSDET(3) <> "" Then
                    Children = OSDET(3)
                    FatherBill = OSDET(1)
                End If
                '0=TGPID, 1=Billable, 2=Modifier, 3=children, 4=NetUnits
                If (TGPDET(2) = "1" And OSDET(0) = "") Or (OSDET(0) <> "" And
                OSDET(1) = "1" And TGPDET(2) = "1") Or (InStr(Children,
                TGPDET(0) & ", ") > 0 And FatherBill = "1") Then
                    '
                    If BillType = 1 Then _
                    TGPDET(4) = UpdateDxPointers(AccID, TGPDET(0))
                    ' UpdateResultBillStatus(AccID, Rs.Fields("TGP_ID").Value, EXCEP) = True Then
                    'Upsert Req_Billable
                    sSQL = "If Exists (Select * from Req_Billable where TGP_ID = " & TGPDET(0) & " and Accession_ID = " & AccID &
                    ") Update Req_Billable set Ordinal = " & b & ", CPT_Code = '" & Trim(TGPDET(3)) & "', Bill_Status = 'U', " &
                    "ICD9 = '" & TGPDET(4) & "', Unit = " & Val(TGPDET(6)) & ", LinePrice = " & Val(TGPDET(5)) & ", Extend = " &
                    IIf(TGPDET(6) = "", Val(TGPDET(5)), Val(TGPDET(5)) * Val(TGPDET(6))) & IIf(OSDET(2) <> "", ", Mod1 = '" &
                    OSDET(2) & "', Mod2 = '" & TGPDET(7) & "', Mod3 = '" & TGPDET(8) & "', Mod4 = '" & TGPDET(9) & "'",
                    ", Mod1 = '" & TGPDET(7) & "', Mod2 = '" & TGPDET(8) & "', Mod3 = '" & TGPDET(9) & "', Mod4 = '" &
                    TGPDET(10) & "'") & ", POS_Code = '" & TGPDET(11) & "', Svc_Date = '" & Svc_Date & "', HL7Output = 0 Where " &
                    "TGP_ID = " & TGPDET(0) & " and Accession_ID = " & AccID & " Else Insert into Req_Billable (Accession_ID, " &
                    "TGP_ID, Ordinal, CPT_Code, Bill_Status, ICD9, Unit, LinePrice, Extend, Mod1, Mod2, Mod3, Mod4, POS_Code, " &
                    "Svc_Date, HL7Output) values (" & AccID & ", " & TGPDET(0) & ", " & b & ", '" & Trim(TGPDET(3)) & "', 'U'" &
                    ", '" & TGPDET(4) & "', " & Val(TGPDET(6)) & ", " & Val(TGPDET(5)) & ", " & IIf(TGPDET(6) = "", Val(TGPDET(5)),
                    Val(TGPDET(5)) * Val(TGPDET(6))) & IIf(OSDET(2) <> "", ", '" & OSDET(2) & "', '" & TGPDET(7) & "', '" &
                    TGPDET(8) & "', '" & TGPDET(9) & "'", ", '" & TGPDET(7) & "', '" & TGPDET(8) & "', '" & TGPDET(9) & "', '" &
                    TGPDET(10) & "'") & ", '" & TGPDET(11) & "', '" & Svc_Date & "', 0)"
                    ExecuteSqlProcedure(sSQL)
                    '
                    If InStr(SynchedTGPs, TGPDET(0) & ", ") = 0 Then SynchedTGPs += TGPDET(0) & ", "
                    If InStr(Children, TGPDET(0) & ", ") > 0 _
                    Then Children = Replace(Children, TGPDET(0) & ", ", "")
                    '
                    If Children = "" Then FatherBill = ""
                    '
                End If    'end billable identification
            End If
        Next
        '----------------------------- End of regular accession
        ReDim Billables(1, 0)
        Dim REFDET() As String
        sSQL = "Select Distinct Reflexed_ID from Ref_Results where Accession_ID = " & AccID
        '
        Dim cnrefd As New SqlConnection(connString)
        cnrefd.Open()
        Dim cmdrefd As New SqlCommand(sSQL, cnrefd)
        cmdrefd.CommandType = CommandType.Text
        Dim drrefd As SqlDataReader = cmdrefd.ExecuteReader
        If drrefd.HasRows Then
            While drrefd.Read
                If Billables(1, UBound(Billables, 2)) <> "" Then _
                ReDim Preserve Billables(1, UBound(Billables, 2) + 1)
                Billables(0, UBound(Billables, 2)) = drrefd("Reflexed_ID")
                Billables(1, UBound(Billables, 2)) = "1"
            End While
        End If
        cnrefd.Close()
        cnrefd = Nothing
        '
        If BillType = 1 Then    'Insurance
            Billables = ApplyPayerRules(ArID, Billables)
        End If
        '    
        For b As Integer = 0 To UBound(Billables, 2)
            If Billables(0, b) <> "" Then
                REFDET = GetTGPDetail(BillType, ArID, Billables(0, b), AccID)
                If GetTGPType(REFDET(0)) = "T" AndAlso REFDET(2) = "1" AndAlso bResults = True _
                Then REFDET(2) = UpdateResultBillStatus(AccID, REFDET(0), REFDET(1), EXCEP)
                'BilledTGP = GetTGPBillStatus(BillType, ArID, AccID, Rs.Fields("Reflexed_ID").Value)
                OSDET = GetOSDetail(AccID, REFDET(0), Billables(1, b))
                '0=TGPID, 1=Billable, 2=Modifier
                If (REFDET(2) = "1" And OSDET(1) = "") Or (OSDET(1) <> "" AndAlso CType(OSDET(1), Boolean) = True) Then
                    If BillType = 1 Then REFDET(4) = UpdateDxPointers(AccID, Val(REFDET(0)))
                    '
                    sSQL = "If Exists (Select * from Req_Billable where TGP_ID = " & REFDET(0) & " and Accession_ID = " & AccID &
                    ") Update Req_Billable set Ordinal = " & b & ", CPT_Code = '" & Trim(REFDET(3)) & "', Bill_Status = 'U', " &
                    "ICD9 = '" & REFDET(4) & "', Unit = " & Val(REFDET(6)) & ", LinePrice = " & Val(REFDET(5)) & ", Extend = " &
                    IIf(REFDET(6) = "", Val(REFDET(5)), Val(REFDET(5)) * Val(REFDET(6))) & IIf(OSDET(2) <> "", ", Mod1 = '" &
                    OSDET(2) & "', Mod2 = '" & REFDET(7) & "', Mod3 = '" & REFDET(8) & "', Mod4 = '" & REFDET(9) & "'",
                    ", Mod1 = '" & REFDET(7) & "', Mod2 = '" & REFDET(8) & "', Mod3 = '" & REFDET(9) & "', Mod4 = '" &
                    REFDET(10) & "'") & ", POS_Code = '" & REFDET(11) & "', Svc_Date = '" & Svc_Date & "', HL7Output = 0 Where " &
                    "TGP_ID = " & REFDET(0) & " and Accession_ID = " & AccID & " Else Insert into Req_Billable (Accession_ID, " &
                    "TGP_ID, Ordinal, CPT_Code, Bill_Status, ICD9, Unit, LinePrice, Extend, Mod1, Mod2, Mod3, Mod4, POS_Code, " &
                    "Svc_Date, HL7Output) values (" & AccID & ", " & REFDET(0) & ", " & b & ", '" & Trim(REFDET(3)) & "', 'U'" &
                    ", '" & REFDET(4) & "', " & Val(REFDET(6)) & ", " & Val(REFDET(5)) & ", " & IIf(REFDET(6) = "", Val(REFDET(5)),
                    Val(REFDET(5)) * Val(REFDET(6))) & IIf(OSDET(2) <> "", ", '" & OSDET(2) & "', '" & REFDET(7) & "', '" &
                    REFDET(8) & "', '" & REFDET(9) & "'", ", '" & REFDET(7) & "', '" & REFDET(8) & "', '" & REFDET(9) & "', '" &
                    REFDET(10) & "'") & ", '" & REFDET(11) & "', '" & Svc_Date & "', 0)"
                    ExecuteSqlProcedure(sSQL)
                    If InStr(SynchedTGPs, REFDET(0) & ", ") = 0 Then SynchedTGPs += REFDET(0) & ", "
                Else    'Unbillable 
                    If REFDET(1) = "G" Then 'Group but not billable
                        Dim TestIDs() As String = {""}
                        sSQL = "Select * from Group_Test where Group_ID = " & Val(REFDET(0))
                        '
                        Dim cngts As New SqlConnection(connString)
                        cngts.Open()
                        Dim cmdgts As New SqlCommand(sSQL, cngts)
                        cmdgts.CommandType = CommandType.Text
                        Dim drgts As SqlDataReader = cmdgts.ExecuteReader
                        If drgts.HasRows Then
                            While drgts.Read
                                If TestIDs(UBound(TestIDs)) <> "" Then ReDim Preserve TestIDs(UBound(TestIDs) + 1)
                                TestIDs(UBound(TestIDs)) = drgts("Test_ID").ToString
                            End While
                        End If
                        cngts.Close()
                        cngts = Nothing
                        '
                        For t As Integer = 0 To TestIDs.Length - 1
                            TSTDET = GetTGPDetail(BillType, ArID, TestIDs(t), AccID)
                            OSDET = GetOSDetail(AccID, TestIDs(t), Billables(0, b))
                            If (TSTDET(2) = "1" And OSDET(1) = "") Or (OSDET(1) <> "" AndAlso CType(OSDET(1),
                            Boolean) = True) AndAlso ((bResults = True And UpdateResultBillStatus(AccID,
                            TestIDs(t), "T", EXCEP) = "1") Or (bResults = False)) Then
                                If BillType = 1 Then TSTDET(4) = UpdateDxPointers(AccID, Val(TSTDET(0)))
                                '
                                sSQL = "If Exists (Select * from Req_Billable where TGP_ID = " & TSTDET(0) & " and Accession_ID = " & AccID &
                                ") Update Req_Billable set Ordinal = " & b & ", CPT_Code = '" & Trim(TSTDET(3)) & "', Bill_Status = 'U', " &
                                "ICD9 = '" & TSTDET(4) & "', Unit = " & Val(TSTDET(6)) & ", LinePrice = " & Val(TSTDET(5)) & ", Extend = " &
                                IIf(TSTDET(6) = "", Val(TSTDET(5)), Val(TSTDET(5)) * Val(TSTDET(6))) & IIf(OSDET(2) <> "", ", Mod1 = '" &
                                OSDET(2) & "', Mod2 = '" & TSTDET(7) & "', Mod3 = '" & TSTDET(8) & "', Mod4 = '" & TSTDET(9) & "'",
                                ", Mod1 = '" & TSTDET(7) & "', Mod2 = '" & TSTDET(8) & "', Mod3 = '" & TSTDET(9) & "', Mod4 = '" &
                                TSTDET(10) & "'") & ", POS_Code = '" & TSTDET(11) & "', Svc_Date = '" & Svc_Date & "', HL7Output = 0 Where " &
                                "TGP_ID = " & TSTDET(0) & " and Accession_ID = " & AccID & " Else Insert into Req_Billable (Accession_ID, " &
                                "TGP_ID, Ordinal, CPT_Code, Bill_Status, ICD9, Unit, LinePrice, Extend, Mod1, Mod2, Mod3, Mod4, POS_Code, " &
                                "Svc_Date, HL7Output) values (" & AccID & ", " & TSTDET(0) & ", " & b & ", '" & Trim(TSTDET(3)) & "', 'U'" &
                                ", '" & TSTDET(4) & "', " & Val(TSTDET(6)) & ", " & Val(TSTDET(5)) & ", " & IIf(TSTDET(6) = "", Val(TSTDET(5)),
                                Val(TSTDET(5)) * Val(TSTDET(6))) & IIf(OSDET(2) <> "", ", '" & OSDET(2) & "', '" & TSTDET(7) & "', '" &
                                TSTDET(8) & "', '" & TSTDET(9) & "'", ", '" & TSTDET(7) & "', '" & TSTDET(8) & "', '" & TSTDET(9) & "', '" &
                                TSTDET(10) & "'") & ", '" & TSTDET(11) & "', '" & Svc_Date & "', 0)"
                                ExecuteSqlProcedure(sSQL)
                                If InStr(SynchedTGPs, REFDET(0) & ", ") = 0 Then SynchedTGPs += TSTDET(0) & ", "
                            End If
                        Next
                    ElseIf REFDET(1) = "P" Then   'Profile
                        Dim PGTIDs() As String = {""}
                        sSQL = "Select * from Prof_GrpTst where Profile_ID = " & Val(REFDET(0))
                        '
                        Dim cnpgts As New SqlConnection(connString)
                        cnpgts.Open()
                        Dim cmdpgts As New SqlCommand(sSQL, cnpgts)
                        cmdpgts.CommandType = CommandType.Text
                        Dim drpgts As SqlDataReader = cmdpgts.ExecuteReader
                        If drpgts.HasRows Then
                            While drpgts.Read
                                If PGTIDs(UBound(PGTIDs)) <> "" Then ReDim Preserve PGTIDs(UBound(PGTIDs) + 1)
                                PGTIDs(UBound(PGTIDs)) = drpgts("GrpTst_ID").ToString
                            End While
                        End If
                        cnpgts.Close()
                        cnpgts = Nothing
                        '
                        For p As Integer = 0 To PGTIDs.Length - 1
                            PRODET = GetTGPDetail(BillType, ArID, PGTIDs(p), AccID)
                            'BilledTGP = GetTGPBillStatus(BillType, ArID, AccID, Rsp.Fields("GrpTst_ID").Value)
                            OSDET = GetOSDetail(AccID, PGTIDs(p), Billables(0, b))
                            '0=TGPID, 1=Billable, 2=Modifier
                            If (PRODET(2) = "1" And OSDET(1) = "") Or
                            (OSDET(1) <> "" AndAlso CType(OSDET(1), Boolean) = True) AndAlso
                            ((bResults = True And UpdateResultBillStatus(AccID,
                            PGTIDs(p), PRODET(1), EXCEP) = "1") Or (bResults = False)) Then
                                If BillType = 1 Then PRODET(4) = UpdateDxPointers(AccID, Val(PRODET(0)))
                                If BillType = 1 Then PRODET(4) = UpdateDxPointers(AccID, Val(PRODET(0)))
                                '
                                sSQL = "If Exists (Select * from Req_Billable where TGP_ID = " & PRODET(0) & " and Accession_ID = " & AccID &
                                ") Update Req_Billable set Ordinal = " & b & ", CPT_Code = '" & Trim(PRODET(3)) & "', Bill_Status = 'U', " &
                                "ICD9 = '" & PRODET(4) & "', Unit = " & Val(PRODET(6)) & ", LinePrice = " & Val(PRODET(5)) & ", Extend = " &
                                IIf(PRODET(6) = "", Val(PRODET(5)), Val(PRODET(5)) * Val(PRODET(6))) & IIf(OSDET(2) <> "", ", Mod1 = '" &
                                OSDET(2) & "', Mod2 = '" & PRODET(7) & "', Mod3 = '" & PRODET(8) & "', Mod4 = '" & PRODET(9) & "'",
                                ", Mod1 = '" & PRODET(7) & "', Mod2 = '" & PRODET(8) & "', Mod3 = '" & PRODET(9) & "', Mod4 = '" &
                                PRODET(10) & "'") & ", POS_Code = '" & PRODET(11) & "', Svc_Date = '" & Svc_Date & "', HL7Output = 0 Where " &
                                "TGP_ID = " & PRODET(0) & " and Accession_ID = " & AccID & " Else Insert into Req_Billable (Accession_ID, " &
                                "TGP_ID, Ordinal, CPT_Code, Bill_Status, ICD9, Unit, LinePrice, Extend, Mod1, Mod2, Mod3, Mod4, POS_Code, " &
                                "Svc_Date, HL7Output) values (" & AccID & ", " & PRODET(0) & ", " & b & ", '" & Trim(PRODET(3)) & "', 'U'" &
                                ", '" & PRODET(4) & "', " & Val(PRODET(6)) & ", " & Val(PRODET(5)) & ", " & IIf(PRODET(6) = "", Val(PRODET(5)),
                                Val(PRODET(5)) * Val(PRODET(6))) & IIf(OSDET(2) <> "", ", '" & OSDET(2) & "', '" & PRODET(7) & "', '" &
                                PRODET(8) & "', '" & PRODET(9) & "'", ", '" & PRODET(7) & "', '" & PRODET(8) & "', '" & PRODET(9) & "', '" &
                                PRODET(10) & "'") & ", '" & PRODET(11) & "', '" & Svc_Date & "', 0)"
                                ExecuteSqlProcedure(sSQL)
                                If InStr(SynchedTGPs, REFDET(0) & ", ") = 0 Then SynchedTGPs += PRODET(0) & ", "
                            Else    'not billable
                                If PRODET(1) = "G" Then
                                    Dim TestIDs() As String = {""}
                                    sSQL = "Select * from Group_Test where Group_ID = " & Val(PRODET(0))
                                    '
                                    Dim cngts As New SqlConnection(connString)
                                    cngts.Open()
                                    Dim cmdgts As New SqlCommand(sSQL, cngts)
                                    cmdgts.CommandType = CommandType.Text
                                    Dim drgts As SqlDataReader = cmdgts.ExecuteReader
                                    If drgts.HasRows Then
                                        While drgts.Read
                                            If TestIDs(UBound(TestIDs)) <> "" Then ReDim Preserve TestIDs(UBound(TestIDs) + 1)
                                            TestIDs(UBound(TestIDs)) = drgts("Test_ID").ToString
                                        End While
                                    End If
                                    cngts.Close()
                                    cngts = Nothing
                                    '
                                    For t As Integer = 0 To TestIDs.Length - 1
                                        TSTDET = GetTGPDetail(BillType, ArID, TestIDs(t), AccID)
                                        OSDET = GetOSDetail(AccID, TestIDs(t), Billables(0, b))
                                        If (TSTDET(2) = "1" And OSDET(1) = "") Or (OSDET(1) <> "" AndAlso CType(OSDET(1),
                                        Boolean) = True) AndAlso ((bResults = True And UpdateResultBillStatus(AccID,
                                        TestIDs(t), "T", EXCEP) = "1") Or (bResults = False)) Then
                                            If BillType = 1 Then TSTDET(4) = UpdateDxPointers(AccID, Val(TSTDET(0)))
                                            '
                                            sSQL = "If Exists (Select * from Req_Billable where TGP_ID = " & TSTDET(0) & " and Accession_ID = " & AccID &
                                            ") Update Req_Billable set Ordinal = " & b & ", CPT_Code = '" & Trim(TSTDET(3)) & "', Bill_Status = 'U', " &
                                            "ICD9 = '" & TSTDET(4) & "', Unit = " & Val(TSTDET(6)) & ", LinePrice = " & Val(TSTDET(5)) & ", Extend = " &
                                            IIf(TSTDET(6) = "", Val(TSTDET(5)), Val(TSTDET(5)) * Val(TSTDET(6))) & IIf(OSDET(2) <> "", ", Mod1 = '" &
                                            OSDET(2) & "', Mod2 = '" & TSTDET(7) & "', Mod3 = '" & TSTDET(8) & "', Mod4 = '" & TSTDET(9) & "'",
                                            ", Mod1 = '" & TSTDET(7) & "', Mod2 = '" & TSTDET(8) & "', Mod3 = '" & TSTDET(9) & "', Mod4 = '" &
                                            TSTDET(10) & "'") & ", POS_Code = '" & TSTDET(11) & "', Svc_Date = '" & Svc_Date & "', HL7Output = 0 Where " &
                                            "TGP_ID = " & TSTDET(0) & " and Accession_ID = " & AccID & " Else Insert into Req_Billable (Accession_ID, " &
                                            "TGP_ID, Ordinal, CPT_Code, Bill_Status, ICD9, Unit, LinePrice, Extend, Mod1, Mod2, Mod3, Mod4, POS_Code, " &
                                            "Svc_Date, HL7Output) values (" & AccID & ", " & TSTDET(0) & ", " & b & ", '" & Trim(TSTDET(3)) & "', 'U'" &
                                            ", '" & TSTDET(4) & "', " & Val(TSTDET(6)) & ", " & Val(TSTDET(5)) & ", " & IIf(TSTDET(6) = "", Val(TSTDET(5)),
                                            Val(TSTDET(5)) * Val(TSTDET(6))) & IIf(OSDET(2) <> "", ", '" & OSDET(2) & "', '" & TSTDET(7) & "', '" &
                                            TSTDET(8) & "', '" & TSTDET(9) & "'", ", '" & TSTDET(7) & "', '" & TSTDET(8) & "', '" & TSTDET(9) & "', '" &
                                            TSTDET(10) & "'") & ", '" & TSTDET(11) & "', '" & Svc_Date & "', 0)"
                                            ExecuteSqlProcedure(sSQL)
                                            If InStr(SynchedTGPs, REFDET(0) & ", ") = 0 Then SynchedTGPs += TSTDET(0) & ", "
                                        End If
                                    Next
                                End If
                            End If
                        Next
                    End If
                End If
            End If
        Next
        If SynchedTGPs.EndsWith(", ") Then SynchedTGPs = Microsoft.VisualBasic.Mid(SynchedTGPs, 1, Len(SynchedTGPs) - 2)
        If SynchedTGPs <> "" Then
            ExecuteSqlProcedure("Delete from Req_Billable where Bill_Status <> 'B' and " &
            "Accession_ID = " & AccID & " and Not TGP_ID in (" & SynchedTGPs & ")")
            Outputs(1) = "Success"
            Outputs(2) = SynchedTGPs
        Else
            ExecuteSqlProcedure("Delete from Req_Billable where Bill_Status <> 'B' and " &
            "Accession_ID = " & AccID)
            Outputs(1) = "Failed"
            If Outputs(2) = "" Then Outputs(2) = "Gratis or billed by Reference Lab"
        End If
        ApplyPayerBillRules(AccID)
        'Catch Ex As Exception
        'MsgBox("An error: '" & Ex.Message & vbCrLf & "' occured while processing " & _
        '"Accession: " & AccID, MsgBoxStyle.Critical, "Prolis")
        'End Try
        Return Outputs
    End Function

    Private Sub ApplyPayerBillRules(ByVal AccID As Long)
        Dim cnpbr As New SqlConnection(connString)
        cnpbr.Open()
        Dim cmdpbr As New SqlCommand("Select * from Payer_BillRules where Payer_ID in " &
        "(Select PrimePayer_ID from Requisitions where BillingType_ID = 1 and ID = " & AccID & ")", cnpbr)
        cmdpbr.CommandType = CommandType.Text
        Dim drpbr As SqlDataReader = cmdpbr.ExecuteReader
        If drpbr.HasRows Then
            While drpbr.Read
                If drpbr("Element") = "CPT_Code" Then
                    ExecuteSqlProcedure("Update Req_Billable set CPT_Code = '" & drpbr("Target") _
                    & "' where CPT_Code = '" & drpbr("Origin") & "' and Accession_ID = " & AccID)
                ElseIf drpbr("Element") = "POS_Code" Then
                    Dim pos = CommonData.GetPOS(AccID.ToString()) 'drpbr("Target")
                    ExecuteSqlProcedure("Update Req_Billable set POS_Code = '" & pos _
                    & "' where POS_Code = '" & drpbr("Origin") & "' and Accession_ID = " & AccID)
                End If
            End While
        End If
        cnpbr.Close()
        cnpbr = Nothing
    End Sub

    Public Sub SynchronizeBillables(ByVal AccID As Long, ByVal IgnoreRes As Boolean, ByVal EXCEP As String)
        'Try
        Dim BillType As Integer = -1
        Dim ArID As Long = -1
        Dim TGPDET() As String
        Dim TSTDET() As String
        Dim PRODET() As String
        Dim OSDET() As String
        Dim SynchedTGPs As String = ""
        Dim Children As String = ""
        Dim FatherBill As String = ""
        Dim Billables(1, 0) As String
        Dim TBBIDS As String = ""
        Dim i As Integer = 0
        Dim Svc_Date As Date
        Dim sSQL As String = "Select * from Requisitions where IsGratis = 0 and (InHouse <> 0 or (InHouse = 0 " &
        "and ID in (Select Accession_ID from Sendouts where BillingType_ID <> 1))) and ID = " & AccID
        Dim cnbas As New SqlConnection(connString)
        cnbas.Open()
        Dim cmdbas As New SqlCommand(sSQL, cnbas)
        cmdbas.CommandType = CommandType.Text
        Dim drbas As SqlDataReader = cmdbas.ExecuteReader
        If drbas.HasRows Then
            While drbas.Read
                BillType = drbas("BillingType_ID")
                If drbas("PrimePayer_ID") IsNot DBNull.Value _
                Then ArID = drbas("PrimePayer_ID")
                Svc_Date = GetServiceDate(AccID)
            End While
        End If
        cnbas.Close()
        cnbas = Nothing
        '
        If BillType <> -1 And ArID <> -1 Then  'chargeable
            If BillType = 1 Then    'Insurance
                Billables = GetTPBillables(AccID)
                Billables = ApplyPayerRules(ArID, Billables)
            Else
                If BillType = 0 Then    'Client
                    sSQL = "Select TBBID, Units from vReqCBillable where AccID = " & AccID
                Else
                    sSQL = "Select TBBID, Units from vReqPBillable where AccID = " & AccID
                End If
                '
                Dim cnbills As New SqlConnection(connString)
                cnbills.Open()
                Dim cmdbills As New SqlCommand(sSQL, cnbills)
                cmdbills.CommandType = CommandType.Text
                Dim drbills As SqlDataReader = cmdbills.ExecuteReader
                If drbills.HasRows Then
                    While drbills.Read
                        If Billables(1, UBound(Billables, 2)) <> "" Then _
                        ReDim Preserve Billables(1, UBound(Billables, 2) + 1)
                        Billables(0, UBound(Billables, 2)) = drbills("TBBID")
                        Billables(1, UBound(Billables, 2)) = drbills("Units")
                    End While
                End If
                cnbills.Close()
                cnbills = Nothing
            End If
        End If
        '
        For b As Integer = 0 To UBound(Billables, 2)
            If Billables(0, b) <> "" Then
                TGPDET = GetTGPDetail(BillType, ArID, Billables(0, b), AccID)
                TGPDET(6) = Billables(1, b)
                '0=TGPID, 1=Type, 2=Billable, 3=CPT, 4=ICD9, 5=PRICE, 
                '6=Unit, 7=Mod1, 8=Mod2, 0=Mod3, 10=Mod4, 11=POS_Code
                If TGPDET(2) = "1" AndAlso IgnoreRes = False Then TGPDET(2) =
                UpdateResultBillStatus(AccID, TGPDET(0), TGPDET(1), EXCEP)
                'BilledTGP = GetTGPBillStatus(BillType, ArID, AccID, Rs.Fields("TGP_ID").Value)
                '0=TGPID, 1=BillStatus, 2=CPT, 3=Dx, 4=UnitPrice, 5=Unit, 6=BillDate, 
                '7=Billedby, 8=Mod1, 9=Mod2, 10=Mod3, 11=Mod4, 12=POS_Code
                OSDET = GetOSDetail(AccID, TGPDET(0), TGPDET(6))
                If OSDET(4) <> "" Then TGPDET(6) = OSDET(4)
                If OSDET(3) <> "" Then
                    Children = OSDET(3)
                    FatherBill = OSDET(1)
                End If
                '0=TGPID, 1=Billable, 2=Modifier, 3=children, 4=NetUnits
                If (TGPDET(2) = "1" And OSDET(0) = "") Or (OSDET(0) <> "" And
                OSDET(1) = "1" And TGPDET(2) = "1") Or (InStr(Children,
                TGPDET(0) & ", ") > 0 And FatherBill = "1") Then
                    '
                    TGPDET(4) = UpdateDxPointers(AccID, TGPDET(0))
                    ' UpdateResultBillStatus(AccID, Rs.Fields("TGP_ID").Value, EXCEP) = True Then
                    'Upsert Req_Billable
                    sSQL = "If Exists (Select * from Req_Billable where TGP_ID = " & TGPDET(0) & " and Accession_ID = " & AccID &
                    ") Update Req_Billable set Ordinal = " & b & ", CPT_Code = '" & Trim(TGPDET(3)) & "', Bill_Status = 'U', " &
                    "ICD9 = '" & TGPDET(4) & "', Unit = " & Val(TGPDET(6)) & ", LinePrice = " & Val(TGPDET(5)) & ", Extend = " &
                    IIf(TGPDET(6) = "", Val(TGPDET(5)), Val(TGPDET(5)) * Val(TGPDET(6))) & IIf(OSDET(2) <> "", ", Mod1 = '" &
                    OSDET(2) & "', Mod2 = '" & TGPDET(7) & "', Mod3 = '" & TGPDET(8) & "', Mod4 = '" & TGPDET(9) & "'",
                    ", Mod1 = '" & TGPDET(7) & "', Mod2 = '" & TGPDET(8) & "', Mod3 = '" & TGPDET(9) & "', Mod4 = '" &
                    TGPDET(10) & "'") & ", POS_Code = '" & TGPDET(11) & "', Svc_Date = '" & Svc_Date & "', HL7Output = 0 Where " &
                    "TGP_ID = " & TGPDET(0) & " and Accession_ID = " & AccID & " Else Insert into Req_Billable (Accession_ID, " &
                    "TGP_ID, Ordinal, CPT_Code, Bill_Status, ICD9, Unit, LinePrice, Extend, Mod1, Mod2, Mod3, Mod4, POS_Code, " &
                    "Svc_Date, HL7Output) values (" & AccID & ", " & TGPDET(0) & ", " & b & ", '" & Trim(TGPDET(3)) & "', 'U'" &
                    ", '" & TGPDET(4) & "', " & Val(TGPDET(6)) & ", " & Val(TGPDET(5)) & ", " & IIf(TGPDET(6) = "", Val(TGPDET(5)),
                    Val(TGPDET(5)) * Val(TGPDET(6))) & IIf(OSDET(2) <> "", ", '" & OSDET(2) & "', '" & TGPDET(7) & "', '" &
                    TGPDET(8) & "', '" & TGPDET(9) & "'", ", '" & TGPDET(7) & "', '" & TGPDET(8) & "', '" & TGPDET(9) & "', '" &
                    TGPDET(10) & "'") & ", '" & TGPDET(11) & "', '" & Svc_Date & "', 0)"
                    ExecuteSqlProcedure(sSQL)
                    '
                    If InStr(SynchedTGPs, TGPDET(0) & ", ") = 0 Then SynchedTGPs += TGPDET(0) & ", "
                    If InStr(Children, TGPDET(0) & ", ") > 0 _
                    Then Children = Replace(Children, TGPDET(0) & ", ", "")
                    '
                    If Children = "" Then FatherBill = ""
                    '
                End If    'end billable identification
            End If
        Next
        '----------------------------- End of regular accession
        ReDim Billables(1, 0)
        Dim REFDET() As String
        sSQL = "Select Distinct Reflexed_ID from Ref_Results where Accession_ID = " & AccID
        '
        Dim cnrefd As New SqlConnection(connString)
        cnrefd.Open()
        Dim cmdrefd As New SqlCommand(sSQL, cnrefd)
        cmdrefd.CommandType = CommandType.Text
        Dim drrefd As SqlDataReader = cmdrefd.ExecuteReader
        If drrefd.HasRows Then
            While drrefd.Read
                If Billables(1, UBound(Billables, 2)) <> "" Then _
                ReDim Preserve Billables(1, UBound(Billables, 2) + 1)
                Billables(0, UBound(Billables, 2)) = drrefd("Reflexed_ID")
                Billables(1, UBound(Billables, 2)) = "1"
            End While
        End If
        cnrefd.Close()
        cnrefd = Nothing
        '
        If BillType = 1 Then    'Insurance
            Billables = ApplyPayerRules(ArID, Billables)
        End If
        '    
        For b As Integer = 0 To UBound(Billables, 2)
            If Billables(0, b) <> "" Then
                REFDET = GetTGPDetail(BillType, ArID, Billables(0, b), AccID)
                If GetTGPType(REFDET(0)) = "T" _
                AndAlso REFDET(2) = "1" AndAlso IgnoreRes = False Then _
                    REFDET(2) = UpdateResultBillStatus(AccID,
                    REFDET(0), REFDET(1), EXCEP)
                'BilledTGP = GetTGPBillStatus(BillType, ArID, AccID, Rs.Fields("Reflexed_ID").Value)
                OSDET = GetOSDetail(AccID, REFDET(0), Billables(1, b))
                '0=TGPID, 1=Billable, 2=Modifier
                If (REFDET(2) = "1" And OSDET(1) = "") Or
                (OSDET(1) <> "" AndAlso CType(OSDET(1), Boolean) = True) Then
                    If BillType = 1 Then REFDET(4) = UpdateDxPointers(AccID, Val(REFDET(0)))
                    '
                    sSQL = "If Exists (Select * from Req_Billable where TGP_ID = " & REFDET(0) & " and Accession_ID = " & AccID &
                    ") Update Req_Billable set Ordinal = " & b & ", CPT_Code = '" & Trim(REFDET(3)) & "', Bill_Status = 'U', " &
                    "ICD9 = '" & REFDET(4) & "', Unit = " & Val(REFDET(6)) & ", LinePrice = " & Val(REFDET(5)) & ", Extend = " &
                    IIf(REFDET(6) = "", Val(REFDET(5)), Val(REFDET(5)) * Val(REFDET(6))) & IIf(OSDET(2) <> "", ", Mod1 = '" &
                    OSDET(2) & "', Mod2 = '" & REFDET(7) & "', Mod3 = '" & REFDET(8) & "', Mod4 = '" & REFDET(9) & "'",
                    ", Mod1 = '" & REFDET(7) & "', Mod2 = '" & REFDET(8) & "', Mod3 = '" & REFDET(9) & "', Mod4 = '" &
                    REFDET(10) & "'") & ", POS_Code = '" & REFDET(11) & "', Svc_Date = '" & Svc_Date & "', HL7Output = 0 Where " &
                    "TGP_ID = " & REFDET(0) & " and Accession_ID = " & AccID & " Else Insert into Req_Billable (Accession_ID, " &
                    "TGP_ID, Ordinal, CPT_Code, Bill_Status, ICD9, Unit, LinePrice, Extend, Mod1, Mod2, Mod3, Mod4, POS_Code, " &
                    "Svc_Date, HL7Output) values (" & AccID & ", " & REFDET(0) & ", " & b & ", '" & Trim(REFDET(3)) & "', 'U'" &
                    ", '" & REFDET(4) & "', " & Val(REFDET(6)) & ", " & Val(REFDET(5)) & ", " & IIf(REFDET(6) = "", Val(REFDET(5)),
                    Val(REFDET(5)) * Val(REFDET(6))) & IIf(OSDET(2) <> "", ", '" & OSDET(2) & "', '" & REFDET(7) & "', '" &
                    REFDET(8) & "', '" & REFDET(9) & "'", ", '" & REFDET(7) & "', '" & REFDET(8) & "', '" & REFDET(9) & "', '" &
                    REFDET(10) & "'") & ", '" & REFDET(11) & "', '" & Svc_Date & "', 0)"
                    ExecuteSqlProcedure(sSQL)
                    If InStr(SynchedTGPs, REFDET(0) & ", ") = 0 Then SynchedTGPs += REFDET(0) & ", "
                Else    'Unbillable 
                    If REFDET(1) = "G" Then 'Group but not billable
                        Dim TestIDs() As String = {""}
                        sSQL = "Select * from Group_Test where Group_ID = " & Val(REFDET(0))
                        '
                        Dim cngts As New SqlConnection(connString)
                        cngts.Open()
                        Dim cmdgts As New SqlCommand(sSQL, cngts)
                        cmdgts.CommandType = CommandType.Text
                        Dim drgts As SqlDataReader = cmdgts.ExecuteReader
                        If drgts.HasRows Then
                            While drgts.Read
                                If TestIDs(UBound(TestIDs)) <> "" Then ReDim Preserve TestIDs(UBound(TestIDs) + 1)
                                TestIDs(UBound(TestIDs)) = drgts("Test_ID").ToString
                            End While
                        End If
                        cngts.Close()
                        cngts = Nothing
                        '
                        For t As Integer = 0 To TestIDs.Length - 1
                            TSTDET = GetTGPDetail(BillType, ArID, TestIDs(t), AccID.ToString())
                            OSDET = GetOSDetail(AccID, TestIDs(t), Billables(0, b))
                            If (TSTDET(2) = "1" And OSDET(1) = "") Or (OSDET(1) <> "" AndAlso CType(OSDET(1),
                            Boolean) = True) AndAlso ((IgnoreRes = False And UpdateResultBillStatus(AccID,
                            TestIDs(t), "T", EXCEP) = "1") Or (IgnoreRes = True)) Then
                                If BillType = 1 Then TSTDET(4) = UpdateDxPointers(AccID, Val(TSTDET(0)))
                                '
                                sSQL = "If Exists (Select * from Req_Billable where TGP_ID = " & TSTDET(0) & " and Accession_ID = " & AccID &
                                ") Update Req_Billable set Ordinal = " & b & ", CPT_Code = '" & Trim(TSTDET(3)) & "', Bill_Status = 'U', " &
                                "ICD9 = '" & TSTDET(4) & "', Unit = " & Val(TSTDET(6)) & ", LinePrice = " & Val(TSTDET(5)) & ", Extend = " &
                                IIf(TSTDET(6) = "", Val(TSTDET(5)), Val(TSTDET(5)) * Val(TSTDET(6))) & IIf(OSDET(2) <> "", ", Mod1 = '" &
                                OSDET(2) & "', Mod2 = '" & TSTDET(7) & "', Mod3 = '" & TSTDET(8) & "', Mod4 = '" & TSTDET(9) & "'",
                                ", Mod1 = '" & TSTDET(7) & "', Mod2 = '" & TSTDET(8) & "', Mod3 = '" & TSTDET(9) & "', Mod4 = '" &
                                TSTDET(10) & "'") & ", POS_Code = '" & TSTDET(11) & "', Svc_Date = '" & Svc_Date & "', HL7Output = 0 Where " &
                                "TGP_ID = " & TSTDET(0) & " and Accession_ID = " & AccID & " Else Insert into Req_Billable (Accession_ID, " &
                                "TGP_ID, Ordinal, CPT_Code, Bill_Status, ICD9, Unit, LinePrice, Extend, Mod1, Mod2, Mod3, Mod4, POS_Code, " &
                                "Svc_Date, HL7Output) values (" & AccID & ", " & TSTDET(0) & ", " & b & ", '" & Trim(TSTDET(3)) & "', 'U'" &
                                ", '" & TSTDET(4) & "', " & Val(TSTDET(6)) & ", " & Val(TSTDET(5)) & ", " & IIf(TSTDET(6) = "", Val(TSTDET(5)),
                                Val(TSTDET(5)) * Val(TSTDET(6))) & IIf(OSDET(2) <> "", ", '" & OSDET(2) & "', '" & TSTDET(7) & "', '" &
                                TSTDET(8) & "', '" & TSTDET(9) & "'", ", '" & TSTDET(7) & "', '" & TSTDET(8) & "', '" & TSTDET(9) & "', '" &
                                TSTDET(10) & "'") & ", '" & TSTDET(11) & "', '" & Svc_Date & "', 0)"
                                ExecuteSqlProcedure(sSQL)
                                If InStr(SynchedTGPs, REFDET(0) & ", ") = 0 Then SynchedTGPs += TSTDET(0) & ", "
                            End If
                        Next
                    ElseIf REFDET(1) = "P" Then   'Profile
                        Dim PGTIDs() As String = {""}
                        sSQL = "Select * from Prof_GrpTst where Profile_ID = " & Val(REFDET(0))
                        '
                        Dim cnpgts As New SqlConnection(connString)
                        cnpgts.Open()
                        Dim cmdpgts As New SqlCommand(sSQL, cnpgts)
                        cmdpgts.CommandType = CommandType.Text
                        Dim drpgts As SqlDataReader = cmdpgts.ExecuteReader
                        If drpgts.HasRows Then
                            While drpgts.Read
                                If PGTIDs(UBound(PGTIDs)) <> "" Then ReDim Preserve PGTIDs(UBound(PGTIDs) + 1)
                                PGTIDs(UBound(PGTIDs)) = drpgts("GrpTst_ID").ToString
                            End While
                        End If
                        cnpgts.Close()
                        cnpgts = Nothing
                        '
                        For p As Integer = 0 To PGTIDs.Length - 1
                            PRODET = GetTGPDetail(BillType, ArID, PGTIDs(p), AccID.ToString())
                            'BilledTGP = GetTGPBillStatus(BillType, ArID, AccID, Rsp.Fields("GrpTst_ID").Value)
                            OSDET = GetOSDetail(AccID, PGTIDs(p), Billables(0, b))
                            '0=TGPID, 1=Billable, 2=Modifier
                            If (PRODET(2) = "1" And OSDET(1) = "") Or
                            (OSDET(1) <> "" AndAlso CType(OSDET(1), Boolean) = True) AndAlso
                            ((IgnoreRes = False And UpdateResultBillStatus(AccID,
                            PGTIDs(p), PRODET(1), EXCEP) = "1") Or (IgnoreRes = True)) Then
                                If BillType = 1 Then PRODET(4) = UpdateDxPointers(AccID, Val(PRODET(0)))
                                If BillType = 1 Then PRODET(4) = UpdateDxPointers(AccID, Val(PRODET(0)))
                                '
                                sSQL = "If Exists (Select * from Req_Billable where TGP_ID = " & PRODET(0) & " and Accession_ID = " & AccID &
                                ") Update Req_Billable set Ordinal = " & b & ", CPT_Code = '" & Trim(PRODET(3)) & "', Bill_Status = 'U', " &
                                "ICD9 = '" & PRODET(4) & "', Unit = " & Val(PRODET(6)) & ", LinePrice = " & Val(PRODET(5)) & ", Extend = " &
                                IIf(PRODET(6) = "", Val(PRODET(5)), Val(PRODET(5)) * Val(PRODET(6))) & IIf(OSDET(2) <> "", ", Mod1 = '" &
                                OSDET(2) & "', Mod2 = '" & PRODET(7) & "', Mod3 = '" & PRODET(8) & "', Mod4 = '" & PRODET(9) & "'",
                                ", Mod1 = '" & PRODET(7) & "', Mod2 = '" & PRODET(8) & "', Mod3 = '" & PRODET(9) & "', Mod4 = '" &
                                PRODET(10) & "'") & ", POS_Code = '" & PRODET(11) & "', Svc_Date = '" & Svc_Date & "', HL7Output = 0 Where " &
                                "TGP_ID = " & PRODET(0) & " and Accession_ID = " & AccID & " Else Insert into Req_Billable (Accession_ID, " &
                                "TGP_ID, Ordinal, CPT_Code, Bill_Status, ICD9, Unit, LinePrice, Extend, Mod1, Mod2, Mod3, Mod4, POS_Code, " &
                                "Svc_Date, HL7Output) values (" & AccID & ", " & PRODET(0) & ", " & b & ", '" & Trim(PRODET(3)) & "', 'U'" &
                                ", '" & PRODET(4) & "', " & Val(PRODET(6)) & ", " & Val(PRODET(5)) & ", " & IIf(PRODET(6) = "", Val(PRODET(5)),
                                Val(PRODET(5)) * Val(PRODET(6))) & IIf(OSDET(2) <> "", ", '" & OSDET(2) & "', '" & PRODET(7) & "', '" &
                                PRODET(8) & "', '" & PRODET(9) & "'", ", '" & PRODET(7) & "', '" & PRODET(8) & "', '" & PRODET(9) & "', '" &
                                PRODET(10) & "'") & ", '" & PRODET(11) & "', '" & Svc_Date & "', 0)"
                                ExecuteSqlProcedure(sSQL)
                                If InStr(SynchedTGPs, REFDET(0) & ", ") = 0 Then SynchedTGPs += PRODET(0) & ", "
                            Else    'not billable
                                If PRODET(1) = "G" Then
                                    Dim TestIDs() As String = {""}
                                    sSQL = "Select * from Group_Test where Group_ID = " & Val(PRODET(0))
                                    '
                                    Dim cngts As New SqlConnection(connString)
                                    cngts.Open()
                                    Dim cmdgts As New SqlCommand(sSQL, cngts)
                                    cmdgts.CommandType = CommandType.Text
                                    Dim drgts As SqlDataReader = cmdgts.ExecuteReader
                                    If drgts.HasRows Then
                                        While drgts.Read
                                            If TestIDs(UBound(TestIDs)) <> "" Then ReDim Preserve TestIDs(UBound(TestIDs) + 1)
                                            TestIDs(UBound(TestIDs)) = drgts("Test_ID").ToString
                                        End While
                                    End If
                                    cngts.Close()
                                    cngts = Nothing
                                    '
                                    For t As Integer = 0 To TestIDs.Length - 1
                                        TSTDET = GetTGPDetail(BillType, ArID, TestIDs(t), AccID.ToString())
                                        OSDET = GetOSDetail(AccID, TestIDs(t), Billables(0, b))
                                        If (TSTDET(2) = "1" And OSDET(1) = "") Or (OSDET(1) <> "" AndAlso CType(OSDET(1),
                                        Boolean) = True) AndAlso ((IgnoreRes = False And UpdateResultBillStatus(AccID,
                                        TestIDs(t), "T", EXCEP) = "1") Or (IgnoreRes = True)) Then
                                            If BillType = 1 Then TSTDET(4) = UpdateDxPointers(AccID, Val(TSTDET(0)))
                                            '
                                            sSQL = "If Exists (Select * from Req_Billable where TGP_ID = " & TSTDET(0) & " and Accession_ID = " & AccID &
                                            ") Update Req_Billable set Ordinal = " & b & ", CPT_Code = '" & Trim(TSTDET(3)) & "', Bill_Status = 'U', " &
                                            "ICD9 = '" & TSTDET(4) & "', Unit = " & Val(TSTDET(6)) & ", LinePrice = " & Val(TSTDET(5)) & ", Extend = " &
                                            IIf(TSTDET(6) = "", Val(TSTDET(5)), Val(TSTDET(5)) * Val(TSTDET(6))) & IIf(OSDET(2) <> "", ", Mod1 = '" &
                                            OSDET(2) & "', Mod2 = '" & TSTDET(7) & "', Mod3 = '" & TSTDET(8) & "', Mod4 = '" & TSTDET(9) & "'",
                                            ", Mod1 = '" & TSTDET(7) & "', Mod2 = '" & TSTDET(8) & "', Mod3 = '" & TSTDET(9) & "', Mod4 = '" &
                                            TSTDET(10) & "'") & ", POS_Code = '" & TSTDET(11) & "', Svc_Date = '" & Svc_Date & "', HL7Output = 0 Where " &
                                            "TGP_ID = " & TSTDET(0) & " and Accession_ID = " & AccID & " Else Insert into Req_Billable (Accession_ID, " &
                                            "TGP_ID, Ordinal, CPT_Code, Bill_Status, ICD9, Unit, LinePrice, Extend, Mod1, Mod2, Mod3, Mod4, POS_Code, " &
                                            "Svc_Date, HL7Output) values (" & AccID & ", " & TSTDET(0) & ", " & b & ", '" & Trim(TSTDET(3)) & "', 'U'" &
                                            ", '" & TSTDET(4) & "', " & Val(TSTDET(6)) & ", " & Val(TSTDET(5)) & ", " & IIf(TSTDET(6) = "", Val(TSTDET(5)),
                                            Val(TSTDET(5)) * Val(TSTDET(6))) & IIf(OSDET(2) <> "", ", '" & OSDET(2) & "', '" & TSTDET(7) & "', '" &
                                            TSTDET(8) & "', '" & TSTDET(9) & "'", ", '" & TSTDET(7) & "', '" & TSTDET(8) & "', '" & TSTDET(9) & "', '" &
                                            TSTDET(10) & "'") & ", '" & TSTDET(11) & "', '" & Svc_Date & "', 0)"
                                            ExecuteSqlProcedure(sSQL)
                                            If InStr(SynchedTGPs, REFDET(0) & ", ") = 0 Then SynchedTGPs += TSTDET(0) & ", "
                                        End If
                                    Next
                                End If
                            End If
                        Next
                    End If
                End If
            End If
        Next
        If SynchedTGPs.EndsWith(", ") Then SynchedTGPs = Microsoft.VisualBasic.Mid(SynchedTGPs, 1, Len(SynchedTGPs) - 2)
        If SynchedTGPs <> "" Then
            ExecuteSqlProcedure("Delete from Req_Billable where Bill_Status <> 'B' and " &
            "Accession_ID = " & AccID & " and Not TGP_ID in (" & SynchedTGPs & ")")
        Else
            ExecuteSqlProcedure("Delete from Req_Billable where Bill_Status <> 'B' and " &
            "Accession_ID = " & AccID)
        End If
        'Catch Ex As Exception
        'MsgBox("An error: '" & Ex.Message & vbCrLf & "' occured while processing " & _
        '"Accession: " & AccID, MsgBoxStyle.Critical, "Prolis")
        'End Try
    End Sub

    Public Function GetOSDetail(ByVal AccID As Long, ByVal TGPID As Integer, ByVal Units As String) As String()
        '0=TGPID, 1=Billable, 2=Modifier, 3=children, 4=Units
        Dim OSU() As String = {"", "", "", "", ""}
        Dim sSQL As String = "Select * from Sendouts where Accession_ID = " & AccID & " and ID in (Select " &
        "distinct Sendout_ID from Sendout_TGP where TGP_ID = " & TGPID & " Union Select distinct Sendout_ID " &
        "from Sendout_TGP where TGP_ID in (Select Group_ID from Group_Test where Test_ID = " & TGPID & ") " &
        "Union Select distinct Sendout_ID from Sendout_TGP where TGP_ID in (Select Profile_ID from Prof_GrpTst " &
        "where GrpTst_ID = " & TGPID & ") Union Select distinct Sendout_ID from Sendout_TGP where TGP_ID in " &
        "(Select Profile_ID from Prof_GrpTst where GrpTst_ID in (Select Group_ID from Group_Test where " &
        "Test_ID = " & TGPID & ")) Union Select distinct Sendout_ID from Sendout_TGP where TGP_ID in (Select " &
        "TGP_ID from TGP_Info where Info_ID = " & TGPID & "))"
        Dim cnosd As New SqlConnection(connString)
        cnosd.Open()
        Dim cmdosd As New SqlCommand(sSQL, cnosd)
        cmdosd.CommandType = CommandType.Text
        Dim drosd As SqlDataReader = cmdosd.ExecuteReader
        If drosd.HasRows Then
            While drosd.Read
                If drosd("BillingType_ID") = 1 Then   'TP
                    OSU(0) = TGPID.ToString
                    OSU(1) = "0"
                    OSU(2) = ""
                Else    'non TP
                    OSU(0) = TGPID.ToString
                    OSU(1) = "1"
                    OSU(2) = "90"
                    OSU(4) = Units
                End If
            End While
        End If
        cnosd.Close()
        cnosd = Nothing
        '
        sSQL = "Select b.BillingType_ID from Ref_Results a inner join (Sendouts b inner join Sendout_TGP c on " &
        "b.ID = c.Sendout_ID) on a.Accession_ID = b.Accession_ID where a.Accession_ID = " & AccID & " and " &
        "a.Test_ID in (Select ID from Tests where InHouse = 0) and a.Test_ID = " & TGPID & " and a.Reflexer_ID = c.TGP_ID"
        Dim cnosr As New SqlConnection(connString)
        cnosr.Open()
        Dim cmdosr As New SqlCommand(sSQL, cnosr)
        cmdosr.CommandType = CommandType.Text
        Dim drosr As SqlDataReader = cmdosr.ExecuteReader
        If drosr.HasRows Then
            While drosr.Read
                If drosr("BillingType_ID") = 1 Then   'TP
                    OSU(0) = TGPID.ToString
                    OSU(1) = "0"
                    OSU(2) = ""
                Else    'non TP
                    OSU(0) = TGPID.ToString
                    OSU(1) = "1"
                    OSU(2) = "90"
                    OSU(4) = Units
                End If
            End While
        End If
        cnosr.Close()
        cnosr = Nothing
        '
        If OSU(0) <> "" Then
            Dim Children As String = GetChildren(TGPID.ToString)
            If Children <> "" Then
                'ReDim Preserve OSU(UBound(OSU) + 1)
                OSU(3) = Children
            End If
        End If
        'Get net units to bill
        Dim BillType As String = ""
        Dim OUnits As Single = 0
        sSQL = "Select a.BillingType_ID, b.TGP_ID, c.Bill_Unit from Sendouts a inner join (Sendout_TGP b " &
        "inner join Tests c on c.ID = b.TGP_ID) on b.Sendout_ID = a.ID where c.CPT_Code in (Select CPT_Code " &
        "from Tests where ID = " & TGPID & " Union Select CPT_Code from Groups where ID = " & TGPID & " Union " &
        "Select CPT_Code from Profiles where ID = " & TGPID & ") and a.Accession_ID = " & AccID & " Union " &
        "Select a.BillingType_ID, b.TGP_ID, c.Bill_Unit from Sendouts a inner join (Sendout_TGP b inner join " &
        "Groups c on c.ID = b.TGP_ID) on b.Sendout_ID = a.ID where c.CPT_Code in (Select CPT_Code from Tests " &
        "where ID = " & TGPID & " Union Select CPT_Code from Groups where ID = " & TGPID & " Union Select " &
        "CPT_Code from Profiles where ID = " & TGPID & ") and a.Accession_ID = " & AccID & " Union Select " &
        "a.BillingType_ID, b.TGP_ID, c.Bill_Unit from Sendouts a inner join (Sendout_TGP b inner join Profiles " &
        "c on c.ID = b.TGP_ID) on b.Sendout_ID = a.ID where c.CPT_Code in (Select CPT_Code from Tests where " &
        "ID = " & TGPID & " Union Select CPT_Code from Groups where ID = " & TGPID & " Union Select CPT_Code " &
        "from Profiles where ID = " & TGPID & ") and a.Accession_ID = " & AccID
        Dim cnosu As New SqlConnection(connString)
        cnosu.Open()
        Dim cmdosu As New SqlCommand(sSQL, cnosu)
        cmdosu.CommandType = CommandType.Text
        Dim drosu As SqlDataReader = cmdosu.ExecuteReader
        If drosu.HasRows Then
            While drosu.Read
                BillType = drosu("BillingType_ID")
                OUnits += drosu("Bill_Unit")
            End While
        End If
        cnosu.Close()
        cnosu = Nothing
        '
        If OUnits > 0 AndAlso BillType <> "" Then
            If BillType = "1" Then 'minus units
                If Val(Units) > OUnits Then
                    OSU(0) = TGPID.ToString
                    OSU(1) = "1"
                    OSU(2) = ""
                    OSU(4) = Math.Round(Val(Units) - OUnits, 0)
                Else
                    OSU(0) = TGPID.ToString
                    OSU(1) = "0"
                    OSU(2) = ""
                    OSU(4) = Math.Round(Val(Units) - OUnits, 0)
                End If
            Else
                OSU(0) = TGPID.ToString
                OSU(1) = "1"
                OSU(2) = "90"
                OSU(4) = Units
            End If
        End If
        Return OSU
    End Function

    Public Function GetTGPDetail(ByVal BillType As Integer, ByVal ArID As Long, ByVal TGPID As Integer, AccID As String) As String()
        Dim TGPDET() As String = {"", "", "", "", "", "", "", "", "", "", "", ""}
        '0=TGPID, 1=Type, 2=Billable, 3=CPT, 4=ICD9, 5=PRICE, 6=Unit, 7=Mod1, 8=Mod2, 0=Mod3, 10=Mod4, 11=POS_Code
        TGPDET(0) = TGPID.ToString : TGPDET(1) = GetTGPType(TGPID)

        Dim PriceLevel As Int16 = 0
        Dim sSQL As String = ""
        If BillType = 0 Then    'Client
            sSQL = "Select a.PriceLevel,a.POS_CODE ,(Select Price from Provider_TGP where " &
            "Provider_ID = a.ID and TGP_ID = " & TGPID & ") as Price from " &
            "Providers a where a.ID = " & ArID
            Dim cnpc As New SqlConnection(connString)
            cnpc.Open()
            Dim cmdpc As New SqlCommand(sSQL, cnpc)
            cmdpc.CommandType = CommandType.Text
            Dim drpc As SqlDataReader = cmdpc.ExecuteReader
            If drpc.HasRows Then
                While drpc.Read
                    PriceLevel = drpc("PriceLevel")
                    If drpc("Price") IsNot DBNull.Value _
                    Then TGPDET(5) = drpc("Price").ToString
                    If drpc("POS_Code") IsNot DBNull.Value Then TGPDET(11) = Trim(drpc("POS_Code"))
                End While
            End If
            cnpc.Close()
            cnpc = Nothing
        ElseIf BillType = 1 Then    'Third Party
            sSQL = "Select a.PriceLevel, b.Price, b.Modifier from Payers a Left Outer Join " &
            "Payer_TGP b on a.ID = b.Payer_ID where a.ID = " & ArID & " and b.TGP_ID = " & TGPID
            Dim cnpc As New SqlConnection(connString)
            cnpc.Open()
            Dim cmdpc As New SqlCommand(sSQL, cnpc)
            cmdpc.CommandType = CommandType.Text
            Dim drpc As SqlDataReader = cmdpc.ExecuteReader
            If drpc.HasRows Then
                While drpc.Read
                    PriceLevel = drpc("PriceLevel")
                    If drpc("Price") IsNot DBNull.Value _
                    Then TGPDET(5) = drpc("Price").ToString
                    If drpc("Modifier") IsNot DBNull.Value AndAlso
                    Trim(drpc("Modifier")) <> "" Then TGPDET(7) = Trim(drpc("Modifier"))
                    'If drpc("POS_Code") IsNot DBNull.Value Then TGPDET(11) = Trim(drpc("POS_Code"))
                End While
            End If
            cnpc.Close()
            cnpc = Nothing
        Else    'Patient
            PriceLevel = SystemConfig.PatientPriceLevel
        End If
        '
        If TGPDET(1) = "T" Then   'Individual Test
            sSQL = "Select * from Tests where ID = " & TGPID
        ElseIf TGPDET(1) = "G" Then   'Group
            sSQL = "Select * from Groups where ID = " & TGPID
        Else
            sSQL = "Select * from Profiles where ID = " & TGPID
        End If
        Dim cncp As New SqlConnection(connString)
        cncp.Open()
        Dim cmdcp As New SqlCommand(sSQL, cncp)
        cmdcp.CommandType = CommandType.Text
        Dim drcp As SqlDataReader = cmdcp.ExecuteReader
        If drcp.HasRows Then
            While drcp.Read
                If BillType = 0 Then    'Client
                    If drcp("CBillable") = False Then
                        TGPDET(2) = "0"
                    Else
                        TGPDET(2) = "1"
                    End If
                ElseIf BillType = 1 Then    'TP
                    If drcp("TBillable") = False Then
                        TGPDET(2) = "0"
                    Else
                        TGPDET(2) = "1"
                    End If
                Else
                    If drcp("PBillable") = False Then
                        TGPDET(2) = "0"
                    Else
                        TGPDET(2) = "1"
                    End If
                End If
                If drcp("Bill_Unit") IsNot DBNull.Value Then TGPDET(6) = drcp("Bill_Unit").ToString
                If drcp("CPT_Code") IsNot DBNull.Value Then
                    If TGPDET(3) = "" Then TGPDET(3) = drcp("CPT_Code")
                End If
                If TGPDET(5) = "" Then
                    If PriceLevel = 1 Then
                        TGPDET(5) = drcp("Price1").ToString
                    ElseIf PriceLevel = 2 Then
                        TGPDET(5) = drcp("Price2").ToString
                    ElseIf PriceLevel = 3 Then
                        TGPDET(5) = drcp("Price3").ToString
                    ElseIf PriceLevel = 4 Then
                        TGPDET(5) = drcp("Price4").ToString
                    ElseIf PriceLevel = 5 Then
                        TGPDET(5) = drcp("Price5").ToString
                    ElseIf PriceLevel = 6 Then
                        TGPDET(5) = drcp("Price6").ToString
                    ElseIf PriceLevel = 7 Then
                        TGPDET(5) = drcp("Price7").ToString
                    ElseIf PriceLevel = 8 Then
                        TGPDET(5) = drcp("Price8").ToString
                    ElseIf PriceLevel = 9 Then
                        TGPDET(5) = drcp("Price9").ToString
                    Else
                        TGPDET(5) = drcp("ListPrice").ToString
                    End If
                End If
                If drcp("Mod1") IsNot DBNull.Value Then
                    If TGPDET(7) = "" Then TGPDET(7) = Trim(drcp("Mod1"))
                End If
                If drcp("Mod2") IsNot DBNull.Value Then TGPDET(8) = Trim(drcp("Mod2"))
                If drcp("Mod3") IsNot DBNull.Value Then TGPDET(9) = Trim(drcp("Mod3"))
                If drcp("Mod4") IsNot DBNull.Value Then TGPDET(10) = Trim(drcp("Mod4"))

            End While
        End If
        cncp.Close()
        cncp = Nothing

        TGPDET(11) = Trim(CommonData.GetPOS(AccID))

        ' If drcp("POS_Code") IsNot DBNull.Value And TGPDET(11) = "" Then TGPDET(11) = Trim(drcp("POS_Code"))
        Return TGPDET
    End Function

    Private Function GetTGPBillStatus(ByVal BillType As Integer, ByVal ArID As Long,
    ByVal AccID As Long, ByVal TGPID As Integer) As String()
        Dim BStatus() As String = {"", "", "", "", "", "", "", "", "", "", "", "", ""}
        'TGPID, Type, Billable, CPT, ICD9, PRICE, Unit (TGPDETAIL)
        '0=TGPID, 1=BillStatus, 2=CPT, 3=Dx, 4=UnitPrice, 5=Unit, 6=BillDate, 
        '7=Billedby, 8=Mod1, 9=Mod2, 10=Mod3, 11=Mod4, 12=POS_Code
        Dim sSQL As String = "Select * from Charge_Detail where Charge_ID in (Select ID from Charges where " &
        "ArType = " & BillType & " and Ar_ID = " & ArID & " and Accession_ID = " & AccID & ") and TGP_ID = " & TGPID
        If connString <> "" Then
            Dim cnbs As New SqlConnection(connString)
            cnbs.Open()
            Dim cmdbs As New SqlCommand(sSQL, cnbs)
            cmdbs.CommandType = CommandType.Text
            Dim drbs As SqlDataReader = cmdbs.ExecuteReader
            If drbs.HasRows Then
                While drbs.Read
                    BStatus(0) = TGPID.ToString : BStatus(1) = "B" : BStatus(2) = drbs("CPT_Code")
                    BStatus(3) = drbs("ICD9") : BStatus(4) = drbs("LinePrice").ToString
                    BStatus(5) = drbs("Unit").ToString : BStatus(6) = Format(drbs("Billed_On"),
                    SystemConfig.DateFormat) : BStatus(7) = drbs("Billed_By").ToString
                    If drbs("Mod1") IsNot DBNull.Value Then BStatus(8) = Trim(drbs("Mod1"))
                    If drbs("Mod2") IsNot DBNull.Value Then BStatus(9) = Trim(drbs("Mod2"))
                    If drbs("Mod3") IsNot DBNull.Value Then BStatus(10) = Trim(drbs("Mod3"))
                    If drbs("Mod4") IsNot DBNull.Value Then BStatus(11) = Trim(drbs("Mod4"))
                    If drbs("POS_Code") IsNot DBNull.Value Then BStatus(12) = Trim(drbs("POS_Code"))
                End While
            Else
                BStatus(0) = TGPID.ToString : BStatus(1) = "U"
            End If
            cnbs.Close()
            cnbs = Nothing
            '
            sSQL = "Select * from Payment_Detail where Charge_ID in (Select ID from Charges where ArType = " &
            BillType & " and Ar_ID = " & ArID & " and Accession_ID = " & AccID & ") and TGP_ID = " & TGPID
            Dim cnps As New SqlConnection(connString)
            cnps.Open()
            Dim cmdps As New SqlCommand(sSQL, cnps)
            cmdps.CommandType = CommandType.Text
            Dim drps As SqlDataReader = cmdps.ExecuteReader
            If drps.HasRows Then BStatus(1) = "P"
            cnps.Close()
            cnps = Nothing
        Else
            Dim cnbs As New sqlConnection(connString)
            cnbs.Open()
            Dim cmdbs As New sqlCommand(sSQL, cnbs)
            cmdbs.CommandType = CommandType.Text
            Dim drbs As sqlDataReader = cmdbs.ExecuteReader
            If drbs.HasRows Then
                While drbs.Read
                    BStatus(0) = TGPID.ToString : BStatus(1) = "B" : BStatus(2) = drbs("CPT_Code")
                    BStatus(3) = drbs("ICD9") : BStatus(4) = drbs("LinePrice").ToString
                    BStatus(5) = drbs("Unit").ToString : BStatus(6) = Format(drbs("Billed_On"),
                    SystemConfig.DateFormat) : BStatus(7) = drbs("Billed_By").ToString
                    If drbs("Mod1") IsNot DBNull.Value Then BStatus(8) = Trim(drbs("Mod1"))
                    If drbs("Mod2") IsNot DBNull.Value Then BStatus(9) = Trim(drbs("Mod2"))
                    If drbs("Mod3") IsNot DBNull.Value Then BStatus(10) = Trim(drbs("Mod3"))
                    If drbs("Mod4") IsNot DBNull.Value Then BStatus(11) = Trim(drbs("Mod4"))
                    If drbs("POS_Code") IsNot DBNull.Value Then BStatus(12) = Trim(drbs("POS_Code"))
                End While
            Else
                BStatus(0) = TGPID.ToString : BStatus(1) = "U"
            End If
            cnbs.Close()
            cnbs = Nothing
            '
            sSQL = "Select * from Payment_Detail where Charge_ID in (Select ID from Charges where ArType = " &
            BillType & " and Ar_ID = " & ArID & " and Accession_ID = " & AccID & ") and TGP_ID = " & TGPID
            Dim cnps As New sqlConnection(connString)
            cnps.Open()
            Dim cmdps As New sqlCommand(sSQL, cnps)
            cmdps.CommandType = CommandType.Text
            Dim drps As sqlDataReader = cmdps.ExecuteReader
            If drps.HasRows Then BStatus(1) = "P"
            cnps.Close()
            cnps = Nothing
        End If
        Return BStatus
    End Function

    Public Function GetBARValuesB(ByVal AccID As Long) As String()
        Dim VALS() As String = {"", "", "", "", "", ""}
        '0=AccID, 1=ArType, 2=ArID, 3=1, 4=InvID, 5=Amount
        Dim sSQL As String = "Select * from Charges where Accession_ID = " & AccID
        Dim cnbvb As New SqlConnection(connString)
        cnbvb.Open()
        Dim cmdbvb As New SqlCommand(sSQL, cnbvb)
        cmdbvb.CommandType = CommandType.Text
        Dim drbvb As SqlDataReader = cmdbvb.ExecuteReader
        If drbvb.HasRows Then
            While drbvb.Read
                VALS(0) = drbvb("Accession_ID").ToString
                VALS(1) = drbvb("ArType").ToString
                VALS(2) = drbvb("Ar_ID").ToString
                VALS(3) = drbvb("IsPrimary").ToString
                VALS(4) = drbvb("ID").ToString
                VALS(5) = drbvb("GrossAmount").ToString
            End While
        End If
        cnbvb.Close()
        cnbvb = Nothing
        Return VALS
    End Function

    Public Function GetBARValuesP(ByVal AccID As Long) As String()
        Dim VALS() As String = {AccID.ToString, "", "", "", "", ""}
        '0=AccID, 1=ArType, 2=ArID, 3=ValueType(3), 4=Check#, 5=Amount
        Dim Amt As Single = 0
        Dim sSQL As String = "Select b.*, a.ArType, a.Ar_ID, c.DocNo from Charges a inner join (Payment_Detail b " &
        "inner join Payments c on b.Payment_ID = c.ID) on a.ID = b.Charge_ID where a.Accession_ID = " & AccID
        If connString <> "" Then
            Dim cnbv As New SqlConnection(connString)
            cnbv.Open()
            Dim cmdbv As New SqlCommand(sSQL, cnbv)
            cmdbv.CommandType = CommandType.Text
            Dim drbv As SqlDataReader = cmdbv.ExecuteReader
            If drbv.HasRows Then
                While drbv.Read
                    VALS(1) = drbv("ArType").ToString
                    VALS(2) = drbv("Ar_ID").ToString
                    VALS(3) = "3"
                    VALS(4) = Trim(drbv("DocNo"))
                    Amt += drbv("AppliedAmount")
                End While
                VALS(5) = Amt.ToString
            End If
            cnbv.Close()
            cnbv = Nothing
        Else
            Dim cnbv As New sqlConnection(connString)
            cnbv.Open()
            Dim cmdbv As New sqlCommand(sSQL, cnbv)
            cmdbv.CommandType = CommandType.Text
            Dim drbv As sqlDataReader = cmdbv.ExecuteReader
            If drbv.HasRows Then
                While drbv.Read
                    VALS(1) = drbv("ArType").ToString
                    VALS(2) = drbv("Ar_ID").ToString
                    VALS(3) = "3"
                    VALS(4) = Trim(drbv("DocNo"))
                    Amt += drbv("AppliedAmount")
                End While
                VALS(5) = Amt.ToString
            End If
            cnbv.Close()
            cnbv = Nothing
        End If
        Return VALS
    End Function

    Public Sub Log_BAR_Event(ByVal AccID As Long, ByVal ArTypeID As Int16, ByVal ArID _
    As Long, ByVal BAREvTypeID As Int16, ByVal DocNo As String, ByVal Amount As Single)
        Dim sSQL As String = "Insert into REQ_BAR_HISTORY (ID, Accession_ID, ArType_ID, Ar_ID, BAR_Event_Type_ID, " &
        "BAR_Event_Date, Doc_No, Amount, Performed_By) values (" & GetNextBAREventID() & ", " & AccID & ", " & ArTypeID &
        ", " & ArID & ", " & BAREvTypeID & ", '" & Date.Now & "', '" & DocNo & "', " & Amount & ", " & ThisUser.ID & ")"
        ExecuteSqlProcedure(sSQL)
    End Sub

    Private Function GetNextBAREventID() As Long
        Dim NextID As Long = 1
        Dim sSQL As String = "Select Max(ID) as LastID from REQ_BAR_HISTORY"
        If connString <> "" Then
            Dim cnbi As New SqlConnection(connString)
            cnbi.Open()
            Dim cmdbi As New SqlCommand(sSQL, cnbi)
            cmdbi.CommandType = CommandType.Text
            Dim drbi As SqlDataReader = cmdbi.ExecuteReader
            If drbi.HasRows Then
                While drbi.Read
                    If drbi("LastID") IsNot DBNull.Value Then NextID = drbi("LastID") + 1
                End While
            End If
            cnbi.Close()
            cnbi = Nothing
        Else
            Dim cnbi As New sqlConnection(connString)
            cnbi.Open()
            Dim cmdbi As New sqlCommand(sSQL, cnbi)
            cmdbi.CommandType = CommandType.Text
            Dim drbi As sqlDataReader = cmdbi.ExecuteReader
            If drbi.HasRows Then
                While drbi.Read
                    If drbi("LastID") IsNot DBNull.Value Then NextID = drbi("LastID") + 1
                End While
            End If
            cnbi.Close()
            cnbi = Nothing
        End If
        Return NextID
    End Function

    Public Sub WriteLog(ByVal msg As String)
        Dim LogPath As String = "C:\Prolis_Log\" & Format(Date.Today, "yy-MM-dd")
        If Not Directory.Exists(LogPath) Then Directory.CreateDirectory(LogPath)
        Dim FS As New StreamWriter(LogPath & "\" & Format(Date.Today, "yy-MM-dd") & ".Log", True)
        FS.Write(msg & vbCrLf)
        FS.Close()
        FS = Nothing
    End Sub

    Public Function GetPatientName(ByVal AccID As Long) As String
        Dim PatName As String = ""
        Dim sSQL As String = "Select * from Patients where ID in (Select Patient_ID from Requisitions where ID = " & AccID & ")"
        If connString <> "" Then
            Dim cnpn As New SqlConnection(connString)
            cnpn.Open()
            Dim cmdpn As New SqlCommand(sSQL, cnpn)
            cmdpn.CommandType = CommandType.Text
            Dim drpn As SqlDataReader = cmdpn.ExecuteReader
            If drpn.HasRows Then
                While drpn.Read
                    If drpn("MiddleName") IsNot DBNull.Value _
                    AndAlso drpn("MiddleName") <> "" Then
                        PatName = drpn("LastName") & ", " & drpn("FirstName") &
                        " " & Microsoft.VisualBasic.Left(drpn("MiddleName"), 1)
                    Else
                        PatName = drpn("LastName") & ", " & drpn("FirstName")
                    End If
                End While
            End If
            cnpn.Close()
            cnpn = Nothing
        Else
            Dim cnpn As New sqlConnection(connString)
            cnpn.Open()
            Dim cmdpn As New sqlCommand(sSQL, cnpn)
            cmdpn.CommandType = CommandType.Text
            Dim drpn As sqlDataReader = cmdpn.ExecuteReader
            If drpn.HasRows Then
                While drpn.Read
                    If drpn("MiddleName") IsNot DBNull.Value _
                    AndAlso drpn("MiddleName") <> "" Then
                        PatName = drpn("LastName") & ", " & drpn("FirstName") &
                        " " & Microsoft.VisualBasic.Left(drpn("MiddleName"), 1)
                    Else
                        PatName = drpn("LastName") & ", " & drpn("FirstName")
                    End If
                End While
            End If
            cnpn.Close()
            cnpn = Nothing
        End If
        Return PatName
    End Function

    Public Function GetFlag(ByVal AccID As Long, ByVal Result As String, ByVal TestID As Integer) As String()
        Dim AgeSex() As String = GetAgeSex(AccID)
        Dim ProviderID As Long = GetOrdProvIDFromAcc(AccID)
        Dim FlagInfo() As String = {"", ""}
        Dim Qualitative As Boolean
        Dim ForI As Boolean = False     'Flag
        Dim AG As Boolean = False
        Dim PTR As String = "0.00"
        Dim PTC As Integer = 2
        Dim MedEval As Boolean = False
        Dim sSQL As String = "Select * from Tests where ID = " & TestID
        '
        Dim cngf As New SqlConnection(connString)
        cngf.Open()
        Dim cmdgf As New SqlCommand(sSQL, cngf)
        cmdgf.CommandType = CommandType.Text
        Dim drgf As SqlDataReader = cmdgf.ExecuteReader
        If drgf.HasRows Then
            While drgf.Read
                ForI = drgf("ForI")
                Qualitative = drgf("Qualitative")
                MedEval = drgf("MedEval")
                If drgf("AGRanges") IsNot DBNull.Value Then
                    AG = drgf("AGRanges")
                Else
                    AG = False
                End If
                If drgf("DecimalPlaces") = 0 Then
                    PTR = "0"
                    PTC = 0
                ElseIf drgf("DecimalPlaces") = 1 Then
                    PTR = "0.0"
                    PTC = 1
                ElseIf drgf("DecimalPlaces") = 3 Then
                    PTR = "0.000"
                    PTC = 3
                ElseIf drgf("DecimalPlaces") = 4 Then
                    PTR = "0.0000"
                    PTC = 4
                End If
            End While
        End If
        cngf.Close()
        cngf = Nothing
        Dim NumRes As String = OptimizeResult(Result, PTR)
        '
        If SystemConfig.DiagTarget = "V" Then   'Veterinary
            Dim SPID As Integer = GetAccessionSpeciesID(AccID)
            sSQL = "Select * from S_Ranges where Species_ID = " & SPID & " and Test_ID = " & TestID
            Dim cnnr As New SqlConnection(connString)
            cnnr.Open()
            Dim cmdnr As New SqlCommand(sSQL, cnnr)
            cmdnr.CommandType = CommandType.Text
            Dim drnr As SqlDataReader = cmdnr.ExecuteReader
            If drnr.HasRows Then
                While drnr.Read
                    If IsNumeric(NumRes) Then       'Numeric Result
                        If InStr(drnr("ValueFrom"), "<") > 0 Then
                            If CSng(NumRes) < drnr("ValueTo") Then
                                FlagInfo(0) = drnr("Flag")
                                If drnr("Behavior") IsNot DBNull.Value Then FlagInfo(1) = drnr("Behavior")
                                If ForI = False Then    'Flag
                                    If UCase(drnr("Flag")).ToString.StartsWith("LP") Or
                                    (UCase(drnr("Flag")).ToString.StartsWith("LOW") And
                                    (InStr(UCase(drnr("Flag")), "PANIC") > 0 Or
                                    InStr(UCase(drnr("Flag")), "CRITIC") > 0)) Then
                                        FlagInfo(0) = "LP"
                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                    ElseIf UCase(drnr("Flag")).ToString.StartsWith("LOW") Or
                                    (UCase(drnr("Flag")).ToString.StartsWith("L ") And
                                    InStr(UCase(drnr("Flag")), "LOW") > 0) Then
                                        FlagInfo(0) = "L"
                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                    ElseIf (UCase(drnr("Flag")).ToString.StartsWith("N") And
                                    InStr(UCase(drnr("Flag")), "NORMAL") > 0) Or
                                    UCase(drnr("Flag")).ToString.StartsWith("NEG") Or
                                    (UCase(drnr("Flag")).ToString.StartsWith("NON") And
                                    InStr(UCase(drnr("Flag")), "REACT") > 0) Or
                                    UCase(drnr("Flag")).ToString.StartsWith("SENSIT") Or
                                    UCase(drnr("Flag")).ToString.StartsWith("SUSCEP") Then
                                        FlagInfo(0) = "N"
                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Ignore"
                                    ElseIf UCase(drnr("Flag")).ToString.StartsWith("AB") Or
                                    (UCase(drnr("Flag")).ToString.StartsWith("A") And
                                    InStr(UCase(drnr("Flag")), "ABNORMAL") > 0) Or
                                    UCase(drnr("Flag")).ToString.StartsWith("REAC") Or
                                    UCase(drnr("Flag")).ToString.StartsWith("POS") Or
                                    UCase(drnr("Flag")).StartsWith("MEDIUM POS") Or
                                    UCase(drnr("Flag")).StartsWith("HIGH POS") Or
                                    UCase(drnr("Flag")).StartsWith("LOW POS") Or
                                    UCase(drnr("Flag")).ToString.StartsWith("DETEC") Or
                                    UCase(drnr("Flag")).ToString.StartsWith("RESIST") Or
                                    (UCase(drnr("Flag")).ToString.StartsWith("NON") And
                                    (InStr(UCase(drnr("Flag")), "NEG") > 0 Or
                                    InStr(UCase(drnr("Flag")), "POS") > 0)) Then
                                        FlagInfo(0) = "A"
                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                    ElseIf UCase(drnr("Flag")).ToString.StartsWith("HP") Or
                                    UCase(drnr("Flag")).ToString.StartsWith("HIGH PANIC") Or
                                    UCase(drnr("Flag")).ToString.StartsWith("HIGH CRITIC") Then
                                        FlagInfo(0) = "HP"
                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                    ElseIf UCase(drnr("Flag")).ToString.StartsWith("HIGH") Or
                                    (UCase(drnr("Flag")).ToString.StartsWith("H") And
                                    InStr(UCase(drnr("Flag")), "HIGH") > 0) Then
                                        FlagInfo(0) = "H"
                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                    End If
                                End If
                                '
                                Exit While
                            End If
                        ElseIf InStr(drnr("ValueFrom"), ">") > 0 Then
                            If CSng(NumRes) > drnr("ValueTo") Then
                                FlagInfo(0) = drnr("Flag")
                                If drnr("Behavior") IsNot DBNull.Value Then FlagInfo(1) = drnr("Behavior")
                                If ForI = False Then    'Flag
                                    If UCase(drnr("Flag")).ToString.StartsWith("LP") Or
                                        (UCase(drnr("Flag")).ToString.StartsWith("LOW") And
                                        (InStr(UCase(drnr("Flag")), "PANIC") > 0 Or
                                        InStr(UCase(drnr("Flag")), "CRITIC") > 0)) Then
                                        FlagInfo(0) = "LP"
                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                    ElseIf UCase(drnr("Flag")).ToString.StartsWith("LOW") Or
                                    (UCase(drnr("Flag")).ToString.StartsWith("L ") And
                                    InStr(UCase(drnr("Flag")), "LOW") > 0) Then
                                        FlagInfo(0) = "L"
                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                    ElseIf (UCase(drnr("Flag")).ToString.StartsWith("N") And
                                    InStr(UCase(drnr("Flag")), "NORMAL") > 0) Or
                                    UCase(drnr("Flag")).ToString.StartsWith("NEG") Or
                                    (UCase(drnr("Flag")).ToString.StartsWith("NON") And
                                    InStr(UCase(drnr("Flag")), "REACT") > 0) Or
                                    UCase(drnr("Flag")).ToString.StartsWith("SENSIT") Or
                                    UCase(drnr("Flag")).ToString.StartsWith("SUSCEP") Then
                                        FlagInfo(0) = "N"
                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Ignore"
                                    ElseIf UCase(drnr("Flag")).ToString.StartsWith("AB") Or
                                    (UCase(drnr("Flag")).ToString.StartsWith("A") And
                                    InStr(UCase(drnr("Flag")), "ABNORMAL") > 0) Or
                                    UCase(drnr("Flag")).ToString.StartsWith("REAC") Or
                                    UCase(drnr("Flag")).ToString.StartsWith("POS") Or
                                    UCase(drnr("Flag")).StartsWith("MEDIUM POS") Or
                                    UCase(drnr("Flag")).StartsWith("HIGH POS") Or
                                    UCase(drnr("Flag")).StartsWith("LOW POS") Or
                                    UCase(drnr("Flag")).ToString.StartsWith("DETEC") Or
                                    UCase(drnr("Flag")).ToString.StartsWith("RESIST") Or
                                    (UCase(drnr("Flag")).ToString.StartsWith("NON") And
                                    (InStr(UCase(drnr("Flag")), "NEG") > 0 Or
                                    InStr(UCase(drnr("Flag")), "POS") > 0)) Then
                                        FlagInfo(0) = "A"
                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                    ElseIf UCase(drnr("Flag")).ToString.StartsWith("HP") Or
                                    UCase(drnr("Flag")).ToString.StartsWith("HIGH PANIC") Or
                                    UCase(drnr("Flag")).ToString.StartsWith("HIGH CRITIC") Then
                                        FlagInfo(0) = "HP"
                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                    ElseIf UCase(drnr("Flag")).ToString.StartsWith("HIGH") Or
                                    (UCase(drnr("Flag")).ToString.StartsWith("H") And
                                    InStr(UCase(drnr("Flag")), "HIGH") > 0) Then
                                        FlagInfo(0) = "H"
                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                    End If
                                End If
                                Exit While
                            End If
                        ElseIf IsNumeric(drnr("ValueFrom")) Then
                            If CSng(NumRes) >= CSng(drnr("ValueFrom").ToString) And CSng(NumRes) <= drnr("ValueTo") Then
                                FlagInfo(0) = drnr("Flag")
                                If drnr("Behavior") IsNot DBNull.Value Then FlagInfo(1) = drnr("Behavior")
                                If ForI = False Then    'Flag
                                    If UCase(drnr("Flag")).ToString.StartsWith("LP") Or (UCase(drnr("Flag")).ToString.StartsWith("LOW") And
                                        (InStr(UCase(drnr("Flag")), "PANIC") > 0 Or InStr(UCase(drnr("Flag")), "CRITIC") > 0)) Then
                                        FlagInfo(0) = "LP"
                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                    ElseIf UCase(drnr("Flag")).ToString.StartsWith("LOW") Or
                                    (UCase(drnr("Flag")).ToString.StartsWith("L ") And
                                    InStr(UCase(drnr("Flag")), "LOW") > 0) Then
                                        FlagInfo(0) = "L"
                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                    ElseIf (UCase(drnr("Flag")).ToString.StartsWith("N") And
                                    InStr(UCase(drnr("Flag")), "NORMAL") > 0) Or
                                    UCase(drnr("Flag")).ToString.StartsWith("NEG") Or
                                    ((UCase(drnr("Flag")).ToString.StartsWith("NON") Or
                                    UCase(drnr("Flag")).ToString.StartsWith("NOT")) And
                                    (InStr(UCase(drnr("Flag")), "REACT") > 0 Or
                                    InStr(UCase(drnr("Flag")), "DETECT") > 0 Or
                                    InStr(UCase(drnr("Flag")), "FOUND") > 0)) Or
                                    UCase(drnr("Flag")).ToString.StartsWith("SENSIT") Or
                                    UCase(drnr("Flag")).ToString.StartsWith("SUSCEP") Then
                                        FlagInfo(0) = "N"
                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Ignore"
                                    ElseIf ((UCase(drnr("Flag")).ToString.StartsWith("AB") Or
                                    UCase(drnr("Flag")).ToString.StartsWith("A")) And
                                    InStr(UCase(drnr("Flag")), "ABNORMAL") > 0) Or
                                    UCase(drnr("Flag")).ToString.StartsWith("REAC") Or
                                    UCase(drnr("Flag")).ToString.StartsWith("POS") Or
                                    UCase(drnr("Flag")).StartsWith("MEDIUM POS") Or
                                    UCase(drnr("Flag")).StartsWith("HIGH POS") Or
                                    UCase(drnr("Flag")).StartsWith("LOW POS") Or
                                    UCase(drnr("Flag")).ToString.StartsWith("DETEC") Or
                                    UCase(drnr("Flag")).ToString.StartsWith("RESIST") Or
                                    (UCase(drnr("Flag")).ToString.StartsWith("NON") And
                                    (InStr(UCase(drnr("Flag")), "NEG") > 0 Or
                                    InStr(UCase(drnr("Flag")), "POS") > 0)) Then
                                        FlagInfo(0) = "A"
                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                    ElseIf UCase(drnr("Flag")).ToString.StartsWith("HP") Or
                                    UCase(drnr("Flag")).ToString.StartsWith("HIGH PANIC") Or
                                    UCase(drnr("Flag")).ToString.StartsWith("HIGH CRITIC") Then
                                        FlagInfo(0) = "HP"
                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                    ElseIf UCase(drnr("Flag")).ToString.StartsWith("HIGH") Or
                                    (UCase(drnr("Flag")).ToString.StartsWith("H") And
                                    InStr(UCase(drnr("Flag")), "HIGH") > 0) Then
                                        FlagInfo(0) = "H"
                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                    End If
                                End If
                                Exit While
                            End If
                        End If
                    Else    'Non-Numeric result
                        If drnr("Flag") IsNot DBNull.Value Then
                            If UCase(NumRes) = UCase(drnr("Flag")) Then
                                If ForI = True Then    'interp
                                    FlagInfo(0) = drnr("Flag")
                                    If UCase(FlagInfo(0)).ToString.StartsWith("LP") Or (UCase(FlagInfo(0)).ToString.StartsWith("LOW") And
                                    (InStr(UCase(FlagInfo(0)), "PANIC") > 0 Or InStr(UCase(FlagInfo(0)), "CRITIC") > 0)) Then
                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                    ElseIf UCase(FlagInfo(0)).ToString.StartsWith("LOW") Or (UCase(FlagInfo(0)).ToString.StartsWith("L ") And
                                    InStr(UCase(FlagInfo(0)), "LOW") > 0) Then
                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                    ElseIf (UCase(FlagInfo(0)).ToString.StartsWith("N") And InStr(UCase(FlagInfo(0)), "NORMAL") > 0) Or
                                    UCase(FlagInfo(0)).ToString.StartsWith("NEG") Or (UCase(FlagInfo(0)).ToString.StartsWith("NON") And
                                    InStr(UCase(FlagInfo(0)), "REACT") > 0) Or UCase(FlagInfo(0)).ToString.StartsWith("SENSIT") Or
                                    UCase(FlagInfo(0)).ToString.StartsWith("SUSCEP") Then
                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Ignore"
                                    ElseIf UCase(FlagInfo(0)).ToString.StartsWith("AB") Or (UCase(FlagInfo(0)).ToString.StartsWith("A") And
                                    InStr(UCase(FlagInfo(0)), "ABNORMAL") > 0) Or UCase(FlagInfo(0)).ToString.StartsWith("REAC") Or
                                    UCase(FlagInfo(0)).ToString.StartsWith("POS") Or UCase(FlagInfo(0)).StartsWith("MEDIUM POS") Or
                                    UCase(FlagInfo(0)).StartsWith("HIGH POS") Or UCase(FlagInfo(0)).StartsWith("LOW POS") Or
                                    UCase(FlagInfo(0)).ToString.StartsWith("DETEC") Or UCase(FlagInfo(0)).ToString.StartsWith("RESIST") Or
                                    (UCase(FlagInfo(0)).ToString.StartsWith("NON") And (InStr(UCase(FlagInfo(0)), "NEG") > 0 Or
                                    InStr(UCase(FlagInfo(0)), "POS") > 0)) Then
                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                    ElseIf UCase(FlagInfo(0)).ToString.StartsWith("HP") Or UCase(FlagInfo(0)).ToString.StartsWith("HIGH PANIC") Or
                                    UCase(FlagInfo(0)).ToString.StartsWith("HIGH CRITIC") Then
                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                    ElseIf UCase(FlagInfo(0)).ToString.StartsWith("HIGH") Or (UCase(FlagInfo(0)).ToString.StartsWith("H") And
                                    InStr(UCase(FlagInfo(0)), "HIGH") > 0) Then
                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                    End If
                                End If
                                Exit While
                            End If
                        End If
                    End If
                End While
            End If
            cnnr.Close()
            cnnr = Nothing
        Else    'Human
            If ProviderID <> -1 Then
                sSQL = "Select * from Provider_Ranges where Provider_ID = " & ProviderID & " and Test_ID = " & TestID
                Dim cnpr As New SqlConnection(connString)
                cnpr.Open()
                Dim cmdpr As New SqlCommand(sSQL, cnpr)
                cmdpr.CommandType = CommandType.Text
                Dim drpr As SqlDataReader = cmdpr.ExecuteReader
                If drpr.HasRows Then
                    While drpr.Read
                        If Qualitative = True Then  'QL test
                            If (Val(AgeSex(0)) >= drpr("AgeFrom") And Val(AgeSex(0)) <= drpr("AgeTo") _
                            And AgeSex(1) = drpr("Sex")) AndAlso drpr("ValueFrom") = Result Then
                                FlagInfo(0) = Trim(drpr("Flag"))
                                If drpr("Behavior") IsNot DBNull.Value Then FlagInfo(1) = drpr("Flag")
                                If ForI = False Then    'Flag
                                    If ((UCase(FlagInfo(0)).StartsWith("N") And InStr(UCase(FlagInfo(0)), "NORM") > 0) Or
                                    UCase(FlagInfo(0)).StartsWith("NORM") Or UCase(FlagInfo(0)).StartsWith("NEG") Or
                                    UCase(FlagInfo(0)).StartsWith("SENSITIVE") Or UCase(FlagInfo(0)).StartsWith("SUSCEPT") Or
                                    UCase(FlagInfo(0)).StartsWith("UNDETECT") Or ((UCase(FlagInfo(0)).StartsWith("NON") Or
                                    UCase(FlagInfo(0)).StartsWith("NOT")) And (InStr(UCase(FlagInfo(0)), "REACT") > 0 Or
                                    InStr(UCase(FlagInfo(0)), "POS") > 0 Or InStr(UCase(FlagInfo(0)), "DETECT") > 0 Or
                                    InStr(UCase(FlagInfo(0)), "RESIST") > 0 Or InStr(UCase(FlagInfo(0)), "FOUND") > 0))) Then
                                        FlagInfo(0) = "N"
                                        If FlagInfo(1) = "" Then FlagInfo(0) = "Ignore"
                                    ElseIf ((UCase(FlagInfo(0)).StartsWith("A") Or UCase(FlagInfo(0)).StartsWith("EQUIVOC") Or
                                    UCase(FlagInfo(0)).StartsWith("LOW POS") Or UCase(FlagInfo(0)).StartsWith("MEDIUM POS") Or
                                    UCase(FlagInfo(0)).StartsWith("HIGH POS") Or UCase(FlagInfo(0)).StartsWith("L") Or
                                    UCase(FlagInfo(0)).StartsWith("POS") Or UCase(FlagInfo(0)).StartsWith("REAC") Or
                                    UCase(FlagInfo(0)).StartsWith("DETECT") Or UCase(FlagInfo(0)).StartsWith("RESIST") Or
                                    UCase(FlagInfo(0)).StartsWith("FOUND") Or UCase(FlagInfo(0)).StartsWith("H")) And
                                    (InStr(UCase(FlagInfo(0)), "PANIC") = 0 Or InStr(UCase(FlagInfo(0)), "CRITIC") = 0)) Then
                                        FlagInfo(0) = "A"
                                        If FlagInfo(1) = "" Then FlagInfo(0) = "Caution"
                                    ElseIf ((UCase(FlagInfo(0)).StartsWith("A") Or UCase(FlagInfo(0)).StartsWith("EQUIVOC") Or
                                    UCase(FlagInfo(0)).StartsWith("LOW POS") Or UCase(FlagInfo(0)).StartsWith("MEDIUM POS") Or
                                    UCase(FlagInfo(0)).StartsWith("HIGH POS") Or UCase(FlagInfo(0)).StartsWith("L") Or
                                    UCase(FlagInfo(0)).StartsWith("POS") Or UCase(FlagInfo(0)).StartsWith("REAC") Or
                                    UCase(FlagInfo(0)).StartsWith("DETECT") Or UCase(FlagInfo(0)).StartsWith("RESIST") Or
                                    UCase(FlagInfo(0)).StartsWith("FOUND") Or UCase(FlagInfo(0)).StartsWith("H")) And
                                    (InStr(UCase(FlagInfo(0)), "PANIC") > 0 Or InStr(UCase(FlagInfo(0)), "CRITIC") > 0)) Then
                                        FlagInfo(0) = "P"
                                        If FlagInfo(1) = "" Then FlagInfo(0) = "Panic"
                                    End If
                                End If
                                '
                                If MedEval = True AndAlso MedsInAccession(AccID) Then  'has meds
                                    If TestInMeds(TestID, AccID) Then   'has med
                                        If UCase(FlagInfo(0)).StartsWith("POS") Or UCase(FlagInfo(0)).StartsWith("REACT") Or
                                        UCase(FlagInfo(0)).StartsWith("DETECT") Or UCase(FlagInfo(0)).StartsWith("FOUND") Or
                                        ((UCase(FlagInfo(0)).StartsWith("NON") Or UCase(FlagInfo(0)).StartsWith("NOT")) And
                                        InStr(UCase(FlagInfo(0)), "NEG") > 0) Or (UCase(FlagInfo(0)).StartsWith("SENT") And
                                        InStr(UCase(FlagInfo(0)), "CONFIRM") > 0) Then
                                            FlagInfo(0) = "CONSISTENT"
                                            FlagInfo(1) = "Ignore"
                                        Else
                                            FlagInfo(0) = "INCONSISTENT"
                                            FlagInfo(1) = "Caution"
                                        End If
                                    Else    'No Meds         
                                        If ((UCase(FlagInfo(0)).StartsWith("A") Or UCase(FlagInfo(0)).StartsWith("EQUIVOC") Or
                                        UCase(FlagInfo(0)).StartsWith("LOW POS") Or UCase(FlagInfo(0)).StartsWith("MEDIUM POS") Or
                                        UCase(FlagInfo(0)).StartsWith("HIGH POS") Or UCase(FlagInfo(0)).StartsWith("L") Or
                                        UCase(FlagInfo(0)).StartsWith("POS") Or UCase(FlagInfo(0)).StartsWith("REAC") Or
                                        UCase(FlagInfo(0)).StartsWith("DETECT") Or UCase(FlagInfo(0)).StartsWith("RESIST") Or
                                        UCase(FlagInfo(0)).StartsWith("FOUND") Or UCase(FlagInfo(0)).StartsWith("H")) And
                                        (InStr(UCase(FlagInfo(0)), "PANIC") = 0 Or InStr(UCase(FlagInfo(0)), "CRITIC") = 0)) Or
                                        ((UCase(FlagInfo(0)).StartsWith("NON") Or UCase(FlagInfo(0)).StartsWith("NOT")) And
                                        InStr(UCase(FlagInfo(0)), "NEG") > 0) Or (UCase(FlagInfo(0)).StartsWith("SENT") And
                                        InStr(UCase(FlagInfo(0)), "CONFIRM") > 0) Then
                                            FlagInfo(0) = "INCONSISTENT"
                                            FlagInfo(1) = "Caution"
                                        Else
                                            FlagInfo(0) = "CONSISTENT"
                                            FlagInfo(1) = "Ignore"
                                        End If
                                    End If
                                End If        '    
                                Exit While
                            End If
                        Else    'QN test
                            If IsNumeric(NumRes) Then       'Numeric Result
                                If Val(AgeSex(0)) >= drpr("AgeFrom") And Val(AgeSex(0)) <= drpr("AgeTo") And AgeSex(1) = drpr("Sex") Then
                                    If InStr(drpr("ValueFrom"), "<") > 0 Then
                                        If CSng(NumRes) < drpr("ValueTo") Then
                                            FlagInfo(0) = Trim(drpr("Flag"))
                                            If drpr("Behavior") IsNot DBNull.Value Then FlagInfo(1) = drpr("Flag")
                                            If ForI = False Then    'Flag
                                                If UCase(FlagInfo(0)).ToString.StartsWith("LP") Or
                                                (UCase(FlagInfo(0)).ToString.StartsWith("LOW") And
                                                (InStr(UCase(FlagInfo(0)), "PANIC") > 0 Or
                                                InStr(UCase(FlagInfo(0)), "CRITIC") > 0)) Then
                                                    FlagInfo(0) = "LP"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                                ElseIf UCase(FlagInfo(0)).ToString.StartsWith("LOW") Or
                                                (UCase(FlagInfo(0)).ToString.StartsWith("L ") And
                                                InStr(UCase(FlagInfo(0)), "LOW") > 0) Then
                                                    FlagInfo(0) = "L"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                ElseIf (UCase(FlagInfo(0)).ToString.StartsWith("N") And
                                                InStr(UCase(FlagInfo(0)), "NORMAL") > 0) Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("NEG") Or
                                                (UCase(FlagInfo(0)).ToString.StartsWith("NON") And
                                                InStr(UCase(FlagInfo(0)), "REACT") > 0) Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("SENSIT") Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("SUSCEP") Then
                                                    FlagInfo(0) = "N"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Ignore"
                                                ElseIf UCase(FlagInfo(0)).ToString.StartsWith("AB") Or
                                                (UCase(FlagInfo(0)).ToString.StartsWith("A") And
                                                InStr(UCase(FlagInfo(0)), "ABNORMAL") > 0) Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("REAC") Or
                                                UCase(FlagInfo(0)).StartsWith("MEDIUM POS") Or
                                                UCase(FlagInfo(0)).StartsWith("HIGH POS") Or
                                                UCase(FlagInfo(0)).StartsWith("LOW POS") Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("POS") Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("DETEC") Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("RESIST") Or
                                                (UCase(FlagInfo(0)).ToString.StartsWith("NON") And
                                                (InStr(UCase(FlagInfo(0)), "NEG") > 0 Or
                                                InStr(UCase(FlagInfo(0)), "POS") > 0)) Then
                                                    FlagInfo(0) = "A"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                ElseIf UCase(FlagInfo(0)).ToString.StartsWith("HP") Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("HIGH PANIC") Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("HIGH CRITIC") Then
                                                    FlagInfo(0) = "HP"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                                ElseIf UCase(FlagInfo(0)).ToString.StartsWith("HIGH") Or
                                                (UCase(FlagInfo(0)).ToString.StartsWith("H") And
                                                InStr(UCase(FlagInfo(0)), "HIGH") > 0) Then
                                                    FlagInfo(0) = "H"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                End If
                                                '
                                                If MedEval = True AndAlso MedsInAccession(AccID) Then  'has meds
                                                    If TestInMeds(TestID, AccID) Then   'has med
                                                        If UCase(FlagInfo(0)).StartsWith("POS") Or UCase(FlagInfo(0)).StartsWith("REACT") Or
                                                        UCase(FlagInfo(0)).StartsWith("DETECT") Or UCase(FlagInfo(0)).StartsWith("FOUND") Or
                                                        ((UCase(FlagInfo(0)).StartsWith("NON") Or UCase(FlagInfo(0)).StartsWith("NOT")) And
                                                        InStr(UCase(FlagInfo(0)), "NEG") > 0) Or (UCase(FlagInfo(0)).StartsWith("SENT") And
                                                        InStr(UCase(FlagInfo(0)), "CONFIRM") > 0) Then
                                                            FlagInfo(0) = "CONSISTENT"
                                                            FlagInfo(1) = "Ignore"
                                                        Else
                                                            FlagInfo(0) = "INCONSISTENT"
                                                            FlagInfo(1) = "Caution"
                                                        End If
                                                    Else    'No Meds
                                                        If UCase(FlagInfo(0)).StartsWith("POS") Or UCase(FlagInfo(0)).StartsWith("MEDIUM POS") Or
                                                        UCase(FlagInfo(0)).StartsWith("HIGH POS") Or UCase(FlagInfo(0)).StartsWith("LOW POS") Or
                                                        UCase(FlagInfo(0)).StartsWith("REACT") Or UCase(FlagInfo(0)).StartsWith("DETECT") Or
                                                        UCase(FlagInfo(0)).StartsWith("FOUND") Or ((UCase(FlagInfo(0)).StartsWith("NON") Or
                                                        UCase(FlagInfo(0)).StartsWith("NOT")) And InStr(UCase(FlagInfo(0)), "NEG") > 0) Or
                                                        (UCase(FlagInfo(0)).StartsWith("SENT") And InStr(UCase(FlagInfo(0)), "CONFIRM") > 0) Then
                                                            FlagInfo(0) = "INCONSISTENT"
                                                            FlagInfo(1) = "Caution"
                                                        Else
                                                            FlagInfo(0) = "CONSISTENT"
                                                            FlagInfo(1) = "Ignore"
                                                        End If
                                                    End If
                                                End If
                                            End If
                                            Exit While
                                        End If
                                    ElseIf InStr(drpr("ValueFrom"), ">") > 0 Then
                                        If CSng(NumRes) > drpr("ValueTo") Then
                                            FlagInfo(0) = Trim(drpr("Flag"))
                                            If drpr("Behavior") IsNot DBNull.Value Then FlagInfo(1) = drpr("Flag")
                                            If ForI = False Then    'Flag
                                                If UCase(FlagInfo(0)).ToString.StartsWith("LP") Or
                                                (UCase(FlagInfo(0)).ToString.StartsWith("LOW") And
                                                (InStr(UCase(FlagInfo(0)), "PANIC") > 0 Or
                                                InStr(UCase(FlagInfo(0)), "CRITIC") > 0)) Then
                                                    FlagInfo(0) = "LP"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                                ElseIf UCase(FlagInfo(0)).ToString.StartsWith("LOW") Or
                                                (UCase(FlagInfo(0)).ToString.StartsWith("L ") And
                                                InStr(UCase(FlagInfo(0)), "LOW") > 0) Then
                                                    FlagInfo(0) = "L"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                ElseIf (UCase(FlagInfo(0)).ToString.StartsWith("N") And
                                                InStr(UCase(FlagInfo(0)), "NORMAL") > 0) Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("NEG") Or
                                                (UCase(FlagInfo(0)).ToString.StartsWith("NON") And
                                                InStr(UCase(FlagInfo(0)), "REACT") > 0) Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("SENSIT") Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("SUSCEP") Then
                                                    FlagInfo(0) = "N"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Ignore"
                                                ElseIf UCase(FlagInfo(0)).ToString.StartsWith("AB") Or
                                                (UCase(FlagInfo(0)).ToString.StartsWith("A") And
                                                InStr(UCase(FlagInfo(0)), "ABNORMAL") > 0) Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("REAC") Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("POS") Or
                                                UCase(FlagInfo(0)).StartsWith("MEDIUM POS") Or
                                                UCase(FlagInfo(0)).StartsWith("HIGH POS") Or
                                                UCase(FlagInfo(0)).StartsWith("LOW POS") Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("DETEC") Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("RESIST") Or
                                                (UCase(FlagInfo(0)).ToString.StartsWith("NON") And
                                                (InStr(UCase(FlagInfo(0)), "NEG") > 0 Or
                                                InStr(UCase(FlagInfo(0)), "POS") > 0)) Then
                                                    FlagInfo(0) = "A"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                ElseIf UCase(FlagInfo(0)).ToString.StartsWith("HP") Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("HIGH PANIC") Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("HIGH CRITIC") Then
                                                    FlagInfo(0) = "HP"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                                ElseIf UCase(FlagInfo(0)).ToString.StartsWith("HIGH") Or
                                                (UCase(FlagInfo(0)).ToString.StartsWith("H") And
                                                InStr(UCase(FlagInfo(0)), "HIGH") > 0) Then
                                                    FlagInfo(0) = "H"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                End If
                                            End If
                                            '
                                            If MedEval = True AndAlso MedsInAccession(AccID) Then  'has meds
                                                If TestInMeds(TestID, AccID) Then   'has med
                                                    If UCase(FlagInfo(0)).StartsWith("POS") Or UCase(FlagInfo(0)).StartsWith("REACT") Or
                                                    UCase(FlagInfo(0)).StartsWith("DETECT") Or UCase(FlagInfo(0)).StartsWith("FOUND") Or
                                                    ((UCase(FlagInfo(0)).StartsWith("NON") Or UCase(FlagInfo(0)).StartsWith("NOT")) And
                                                    InStr(UCase(FlagInfo(0)), "NEG") > 0) Or (UCase(FlagInfo(0)).StartsWith("SENT") And
                                                    InStr(UCase(FlagInfo(0)), "CONFIRM") > 0) Then
                                                        FlagInfo(0) = "CONSISTENT"
                                                        FlagInfo(1) = "Ignore"
                                                    Else
                                                        FlagInfo(0) = "INCONSISTENT"
                                                        FlagInfo(1) = "Caution"
                                                    End If
                                                Else    'No Meds
                                                    If UCase(FlagInfo(0)).StartsWith("POS") Or UCase(FlagInfo(0)).StartsWith("MEDIUM POS") Or
                                                    UCase(FlagInfo(0)).StartsWith("HIGH POS") Or UCase(FlagInfo(0)).StartsWith("LOW POS") Or
                                                    UCase(FlagInfo(0)).StartsWith("REACT") Or UCase(FlagInfo(0)).StartsWith("DETECT") Or
                                                    UCase(FlagInfo(0)).StartsWith("FOUND") Or ((UCase(FlagInfo(0)).StartsWith("NON") Or
                                                    UCase(FlagInfo(0)).StartsWith("NOT")) And InStr(UCase(FlagInfo(0)), "NEG") > 0) Or
                                                    (UCase(FlagInfo(0)).StartsWith("SENT") And InStr(UCase(FlagInfo(0)), "CONFIRM") > 0) Then
                                                        FlagInfo(0) = "INCONSISTENT"
                                                        FlagInfo(1) = "Caution"
                                                    Else
                                                        FlagInfo(0) = "CONSISTENT"
                                                        FlagInfo(1) = "Ignore"
                                                    End If
                                                End If
                                            End If
                                            Exit While
                                        End If
                                    ElseIf IsNumeric(drpr("ValueFrom")) Then
                                        If CSng(NumRes) >= CSng(drpr("ValueFrom").ToString) And
                                        CSng(NumRes) <= CSng(drpr("ValueTo").ToString) Then
                                            FlagInfo(0) = drpr("Flag")
                                            If drpr("Behavior") IsNot DBNull.Value Then
                                                FlagInfo(1) = drpr("Behavior")
                                            Else
                                                If drpr("Flag") = "Positive" Or drpr("Flag") = "Inconsistent" Or drpr("Flag") = "Detected" _
                                                Or drpr("Flag") = "Reactive" Or drpr("Flag") = "L" Or drpr("Flag") = "H" Or drpr("Flag") _
                                                = "A" Or drpr("Flag") = "Low" Or drpr("Flag") = "High" Or drpr("Flag") = "Abnormal" Then
                                                    FlagInfo(1) = "Caution"
                                                ElseIf drpr("Flag") = "HP" Or drpr("Flag") = "LP" Or drpr("Flag") = "AP" Or
                                                    drpr("Flag").ToString.Contains("PANIC") Then
                                                    FlagInfo(1) = "Panic"
                                                Else
                                                    FlagInfo(1) = "Ignore"
                                                End If
                                            End If
                                            '
                                            If ForI = False Then    'Flag
                                                If UCase(FlagInfo(0)).ToString.StartsWith("LP") Or
                                                (UCase(FlagInfo(0)).ToString.StartsWith("LOW") And
                                                (InStr(UCase(FlagInfo(0)), "PANIC") > 0 Or
                                                InStr(UCase(FlagInfo(0)), "CRITIC") > 0)) Then
                                                    FlagInfo(0) = "LP"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                                ElseIf UCase(FlagInfo(0)).ToString.StartsWith("LOW") Or
                                                (UCase(FlagInfo(0)).ToString.StartsWith("L ") And
                                                InStr(UCase(FlagInfo(0)), "LOW") > 0) Then
                                                    FlagInfo(0) = "L"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                ElseIf (UCase(FlagInfo(0)).ToString.StartsWith("N") And
                                                InStr(UCase(FlagInfo(0)), "NORMAL") > 0) Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("NEG") Or
                                                (UCase(FlagInfo(0)).ToString.StartsWith("NON") And
                                                InStr(UCase(FlagInfo(0)), "REACT") > 0) Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("SENSIT") Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("SUSCEP") Then
                                                    FlagInfo(0) = "N"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Ignore"
                                                ElseIf UCase(FlagInfo(0)).ToString.StartsWith("AB") Or
                                                (UCase(FlagInfo(0)).ToString.StartsWith("A") And
                                                InStr(UCase(FlagInfo(0)), "ABNORMAL") > 0) Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("REAC") Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("POS") Or
                                                UCase(FlagInfo(0)).StartsWith("MEDIUM POS") Or
                                                UCase(FlagInfo(0)).StartsWith("HIGH POS") Or
                                                UCase(FlagInfo(0)).StartsWith("LOW POS") Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("DETEC") Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("RESIST") Or
                                                (UCase(FlagInfo(0)).ToString.StartsWith("NON") And
                                                (InStr(UCase(FlagInfo(0)), "NEG") > 0 Or
                                                InStr(UCase(FlagInfo(0)), "POS") > 0)) Then
                                                    FlagInfo(0) = "A"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                ElseIf UCase(FlagInfo(0)).ToString.StartsWith("HP") Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("HIGH PANIC") Or
                                                UCase(FlagInfo(0)).ToString.StartsWith("HIGH CRITIC") Then
                                                    FlagInfo(0) = "HP"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                                ElseIf UCase(FlagInfo(0)).ToString.StartsWith("HIGH") Or
                                                (UCase(FlagInfo(0)).ToString.StartsWith("H") And
                                                InStr(UCase(FlagInfo(0)), "HIGH") > 0) Then
                                                    FlagInfo(0) = "H"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                End If
                                                '
                                                If MedEval = True AndAlso MedsInAccession(AccID) Then  'has meds
                                                    If TestInMeds(TestID, AccID) Then   'has med
                                                        If UCase(FlagInfo(0)).StartsWith("POS") Or UCase(FlagInfo(0)).StartsWith("REACT") Or
                                                        UCase(FlagInfo(0)).StartsWith("DETECT") Or UCase(FlagInfo(0)).StartsWith("FOUND") Or
                                                        ((UCase(FlagInfo(0)).StartsWith("NON") Or UCase(FlagInfo(0)).StartsWith("NOT")) And
                                                        InStr(UCase(FlagInfo(0)), "NEG") > 0) Or (UCase(FlagInfo(0)).StartsWith("SENT") And
                                                        InStr(UCase(FlagInfo(0)), "CONFIRM") > 0) Then
                                                            FlagInfo(0) = "CONSISTENT"
                                                            FlagInfo(1) = "Ignore"
                                                        Else
                                                            FlagInfo(0) = "INCONSISTENT"
                                                            FlagInfo(1) = "Caution"
                                                        End If
                                                    Else    'No Meds
                                                        If UCase(FlagInfo(0)).StartsWith("POS") Or UCase(FlagInfo(0)).StartsWith("MEDIUM POS") Or
                                                        UCase(FlagInfo(0)).StartsWith("HIGH POS") Or UCase(FlagInfo(0)).StartsWith("LOW POS") Or
                                                        UCase(FlagInfo(0)).StartsWith("REACT") Or UCase(FlagInfo(0)).StartsWith("DETECT") Or
                                                        UCase(FlagInfo(0)).StartsWith("FOUND") Or ((UCase(FlagInfo(0)).StartsWith("NON") Or
                                                        UCase(FlagInfo(0)).StartsWith("NOT")) And InStr(UCase(FlagInfo(0)), "NEG") > 0) Or
                                                        (UCase(FlagInfo(0)).StartsWith("SENT") And InStr(UCase(FlagInfo(0)), "CONFIRM") > 0) Then
                                                            FlagInfo(0) = "INCONSISTENT"
                                                            FlagInfo(1) = "Caution"
                                                        Else
                                                            FlagInfo(0) = "CONSISTENT"
                                                            FlagInfo(1) = "Ignore"
                                                        End If
                                                    End If
                                                End If
                                            End If
                                            Exit While
                                        End If
                                    End If
                                End If
                            Else            'Non-Numeric Result
                                If Val(AgeSex(0)) >= drpr("AgeFrom") And Val(AgeSex(0)) <= drpr("AgeTo") _
                                And AgeSex(1) = drpr("Sex") And NumRes = drpr("ValueFrom") Then
                                    FlagInfo(0) = drpr("Flag")
                                    If drpr("Behavior") IsNot DBNull.Value Then FlagInfo(1) = drpr("Flag")
                                    If ForI = False Then    'Flag
                                        If (UCase(FlagInfo(0)).StartsWith("N ") Or UCase(FlagInfo(0)).StartsWith("NORM") Or
                                        UCase(FlagInfo(0)).StartsWith("NEG") Or UCase(FlagInfo(0)).StartsWith("NON") Or
                                        UCase(FlagInfo(0)).StartsWith("SENSITIVE") Or UCase(FlagInfo(0)).StartsWith("UNDETECT")) Then
                                            FlagInfo(0) = "N"
                                            If FlagInfo(1) = "" Then FlagInfo(1) = "Ignore"
                                        ElseIf (UCase(FlagInfo(0)).StartsWith("A ") Or UCase(FlagInfo(0)).StartsWith("AB ") Or
                                        UCase(FlagInfo(0)).StartsWith("ABN") Or UCase(FlagInfo(0)).StartsWith("EQUIVOC") Or
                                        UCase(FlagInfo(0)).StartsWith("LOW POS") Or UCase(FlagInfo(0)).StartsWith("L") Or
                                        UCase(FlagInfo(0)).StartsWith("L ") Or UCase(FlagInfo(0)).StartsWith("POS") Or
                                        UCase(FlagInfo(0)).StartsWith("MEDIUM POS") Or UCase(FlagInfo(0)).StartsWith("HIGH POS") Or
                                        UCase(FlagInfo(0)).StartsWith("LOW POS") Or UCase(FlagInfo(0)).StartsWith("REAC") Or
                                        UCase(FlagInfo(0)).StartsWith("DETECT") Or UCase(FlagInfo(0)).StartsWith("RESIST") Or
                                        UCase(FlagInfo(0)).StartsWith("FOUND") Or UCase(FlagInfo(0)).StartsWith("H") And
                                        InStr(FlagInfo(0), "PANIC") = 0) Then
                                            FlagInfo(0) = "A"
                                            If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                        ElseIf (UCase(FlagInfo(0)).StartsWith("A ") Or UCase(FlagInfo(0)).StartsWith("AB ") Or
                                        UCase(FlagInfo(0)).StartsWith("ABN") Or UCase(FlagInfo(0)).StartsWith("EQUIVOC") Or
                                        UCase(FlagInfo(0)).StartsWith("LOW POS") Or UCase(FlagInfo(0)).StartsWith("L") Or
                                        UCase(FlagInfo(0)).StartsWith("L ") Or UCase(FlagInfo(0)).StartsWith("POS") Or
                                        UCase(FlagInfo(0)).StartsWith("MEDIUM POS") Or UCase(FlagInfo(0)).StartsWith("HIGH POS") Or
                                        UCase(FlagInfo(0)).StartsWith("LOW POS") Or UCase(FlagInfo(0)).StartsWith("REAC") Or
                                        UCase(FlagInfo(0)).StartsWith("DETECT") Or UCase(FlagInfo(0)).StartsWith("RESIST") Or
                                        UCase(FlagInfo(0)).StartsWith("FOUND") Or UCase(FlagInfo(0)).StartsWith("H") And
                                        (InStr(FlagInfo(0), "PANIC") > 0 Or FlagInfo(0) = "PANIC")) Then
                                            FlagInfo(0) = "P"
                                            If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                        End If
                                        '
                                        If MedEval = True AndAlso MedsInAccession(AccID) Then  'has meds
                                            If TestInMeds(TestID, AccID) Then   'has med
                                                If UCase(FlagInfo(0)).StartsWith("POS") Or UCase(FlagInfo(0)).StartsWith("REACT") Or
                                                UCase(FlagInfo(0)).StartsWith("DETECT") Or UCase(FlagInfo(0)).StartsWith("FOUND") Or
                                                ((UCase(FlagInfo(0)).StartsWith("NON") Or UCase(FlagInfo(0)).StartsWith("NOT")) And
                                                InStr(UCase(FlagInfo(0)), "NEG") > 0) Or (UCase(FlagInfo(0)).StartsWith("SENT") And
                                                InStr(UCase(FlagInfo(0)), "CONFIRM") > 0) Then
                                                    FlagInfo(0) = "CONSISTENT"
                                                    FlagInfo(1) = "Ignore"
                                                Else
                                                    FlagInfo(0) = "INCONSISTENT"
                                                    FlagInfo(1) = "Caution"
                                                End If
                                            Else    'No Meds
                                                If UCase(FlagInfo(0)).StartsWith("POS") Or UCase(FlagInfo(0)).StartsWith("MEDIUM POS") Or
                                                UCase(FlagInfo(0)).StartsWith("HIGH POS") Or UCase(FlagInfo(0)).StartsWith("LOW POS") Or
                                                UCase(FlagInfo(0)).StartsWith("REACT") Or UCase(FlagInfo(0)).StartsWith("DETECT") Or
                                                UCase(FlagInfo(0)).StartsWith("FOUND") Or ((UCase(FlagInfo(0)).StartsWith("NON") Or
                                                UCase(FlagInfo(0)).StartsWith("NOT")) And InStr(UCase(FlagInfo(0)), "NEG") > 0) Or
                                                (UCase(FlagInfo(0)).StartsWith("SENT") And InStr(UCase(FlagInfo(0)), "CONFIRM") > 0) Then
                                                    FlagInfo(0) = "INCONSISTENT"
                                                    FlagInfo(1) = "Caution"
                                                Else
                                                    FlagInfo(0) = "CONSISTENT"
                                                    FlagInfo(1) = "Ignore"
                                                End If
                                            End If
                                        End If
                                    End If
                                    Exit While
                                End If
                            End If
                        End If
                    End While
                End If
                cnpr.Close()
                cnpr = Nothing
            End If  'End of provider both ranges
            '
            If FlagInfo(0) = "" Then
                If Qualitative = True Then   'C_Ranges
                    sSQL = "Select * from C_Ranges where Test_ID = " & TestID
                    Dim cncr As New SqlConnection(connString)
                    cncr.Open()
                    Dim cmdcr As New SqlCommand(sSQL, cncr)
                    cmdcr.CommandType = CommandType.Text
                    Dim drcr As SqlDataReader = cmdcr.ExecuteReader
                    If drcr.HasRows Then
                        While drcr.Read
                            If UCase(Trim(Result)) = UCase(Trim(drcr("Choice"))) Then
                                'FlagInfo(0) = UCase(drcr("Flag"))
                                If drcr("Behavior") IsNot DBNull.Value Then
                                    FlagInfo(1) = drcr("Behavior")
                                Else
                                    FlagInfo(0) = "Ignore"
                                End If
                                If ForI = False AndAlso Not MyLab.LabName.ToLower().Contains("anderson") Then    'Flag

                                    If FlagInfo(1) = "Ignore" Then
                                        FlagInfo(0) = "N"
                                    ElseIf FlagInfo(1) = "Caution" Then
                                        FlagInfo(0) = "A"
                                    ElseIf FlagInfo(1) = "Panic" Then
                                        FlagInfo(0) = "AP"
                                    End If
                                Else    'interpretation
                                    If MedEval = True AndAlso MedsInAccession(AccID) Then  'has meds
                                        If TestInMeds(TestID, AccID) Then   'has med
                                            If UCase(Trim(drcr("Choice"))).StartsWith("POS") Or UCase(Trim(drcr("Choice"))).StartsWith("REACT") Or
                                            UCase(Trim(drcr("Choice"))).StartsWith("DETECT") Or UCase(Trim(drcr("Choice"))).StartsWith("FOUND") Or
                                            ((UCase(Trim(drcr("Choice"))).StartsWith("NON") Or UCase(Trim(drcr("Choice"))).StartsWith("NOT")) And
                                            InStr(UCase(Trim(drcr("Choice"))), "NEG") > 0 Or (UCase(Trim(drcr("Choice"))).StartsWith("SENT") And
                                            InStr(UCase(Trim(drcr("Choice"))), "CONFIRM") > 0)) Then
                                                FlagInfo(0) = "CONSISTENT"
                                                FlagInfo(1) = "Ignore"
                                            Else
                                                FlagInfo(0) = "INCONSISTENT"
                                                FlagInfo(1) = "Caution"
                                            End If
                                        Else    'No Meds
                                            If UCase(Trim(drcr("Choice"))).StartsWith("POS") Or UCase(Trim(drcr("Choice"))).StartsWith("LOW POS") Or
                                            UCase(Trim(drcr("Choice"))).StartsWith("MEDIUM POS") Or UCase(Trim(drcr("Choice"))).StartsWith("HIGH POS") Or
                                            UCase(Trim(drcr("Choice"))).StartsWith("REACT") Or UCase(Trim(drcr("Choice"))).StartsWith("DETECT") Or
                                            UCase(Trim(drcr("Choice"))).StartsWith("FOUND") Or ((UCase(Trim(drcr("Choice"))).StartsWith("NON") Or
                                            UCase(Trim(drcr("Choice"))).StartsWith("NOT")) And InStr(UCase(Trim(drcr("Choice"))), "NEG") > 0) Or
                                            (UCase(Trim(drcr("Choice"))).StartsWith("SENT") And InStr(UCase(Trim(drcr("Choice"))), "CONFIRM") > 0) Then
                                                FlagInfo(0) = "INCONSISTENT"
                                                FlagInfo(1) = "Caution"
                                            Else
                                                FlagInfo(0) = "CONSISTENT"
                                                FlagInfo(1) = "Ignore"
                                            End If
                                        End If
                                    Else    'No med evaluation
                                        FlagInfo(0) = Trim(drcr("Flag"))
                                    End If
                                End If
                                Exit While
                            End If
                        End While
                    End If
                    cncr.Close()
                    cncr = Nothing
                Else    'Quantitative  'N_Range or AG_Range
                    If AG = True Then   'AG_Range
                        sSQL = "Select * from AG_Ranges where Test_ID = " & TestID
                        Dim cnagr As New SqlConnection(connString)
                        cnagr.Open()
                        Dim cmdagr As New SqlCommand(sSQL, cnagr)
                        cmdagr.CommandType = CommandType.Text
                        Dim dragr As SqlDataReader = cmdagr.ExecuteReader
                        If dragr.HasRows Then
                            While dragr.Read
                                If IsNumeric(NumRes) Then       'Numeric Result
                                    If Val(AgeSex(0)) >= dragr("AgeFrom") And Val(AgeSex(0)) <= dragr("AgeTo") And AgeSex(1) = dragr("Sex") Then
                                        If InStr(dragr("ValueFrom"), "<") > 0 Then
                                            If CSng(NumRes) < dragr("ValueTo") Then
                                                FlagInfo(0) = dragr("Flag")
                                                If dragr("Behavior") IsNot DBNull.Value Then FlagInfo(1) = dragr("Behavior")
                                                If ForI = False Then    'Flag
                                                    If UCase(dragr("Flag")).ToString.StartsWith("LP") Or (UCase(dragr("Flag")).ToString.StartsWith("LOW") And
                                                    (InStr(UCase(dragr("Flag")), "PANIC") > 0 Or InStr(UCase(dragr("Flag")), "CRITIC") > 0)) Then
                                                        FlagInfo(0) = "LP"
                                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                                    ElseIf UCase(dragr("Flag")).ToString.StartsWith("LOW") Or (UCase(dragr("Flag")).ToString.StartsWith("L ") And
                                                    InStr(UCase(dragr("Flag")), "LOW") > 0) Then
                                                        FlagInfo(0) = "L"
                                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                    ElseIf (UCase(dragr("Flag")).ToString.StartsWith("N") And InStr(UCase(dragr("Flag")), "NORMAL") > 0) Or
                                                    UCase(dragr("Flag")).ToString.StartsWith("NEG") Or (UCase(dragr("Flag")).ToString.StartsWith("NON") And
                                                    InStr(UCase(dragr("Flag")), "REACT") > 0) Or UCase(dragr("Flag")).ToString.StartsWith("SENSIT") Or
                                                    UCase(dragr("Flag")).ToString.StartsWith("SUSCEP") Then
                                                        FlagInfo(0) = "N"
                                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Ignore"
                                                    ElseIf UCase(dragr("Flag")).ToString.StartsWith("AB") Or (UCase(dragr("Flag")).ToString.StartsWith("A") And
                                                    InStr(UCase(dragr("Flag")), "ABNORMAL") > 0) Or UCase(dragr("Flag")).ToString.StartsWith("REAC") Or
                                                    UCase(dragr("Flag")).ToString.StartsWith("POS") Or UCase(dragr("Flag")).StartsWith("MEDIUM POS") Or
                                                    UCase(dragr("Flag")).StartsWith("HIGH POS") Or UCase(dragr("Flag")).StartsWith("LOW POS") Or
                                                    UCase(dragr("Flag")).ToString.StartsWith("DETEC") Or UCase(dragr("Flag")).ToString.StartsWith("RESIST") Or
                                                    (UCase(dragr("Flag")).ToString.StartsWith("NON") And (InStr(UCase(dragr("Flag")), "NEG") > 0 Or
                                                    InStr(UCase(dragr("Flag")), "POS") > 0)) Then
                                                        FlagInfo(0) = "A"
                                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                    ElseIf UCase(dragr("Flag")).ToString.StartsWith("HP") Or UCase(dragr("Flag")).ToString.StartsWith("HIGH PANIC") Or
                                                    UCase(dragr("Flag")).ToString.StartsWith("HIGH CRITIC") Then
                                                        FlagInfo(0) = "HP"
                                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                                    ElseIf UCase(dragr("Flag")).ToString.StartsWith("HIGH") Or (UCase(dragr("Flag")).ToString.StartsWith("H") And
                                                    InStr(UCase(dragr("Flag")), "HIGH") > 0) Then
                                                        FlagInfo(0) = "H"
                                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                    End If
                                                End If
                                                '
                                                If MedEval = True AndAlso MedsInAccession(AccID) Then  'has meds
                                                    If TestInMeds(TestID, AccID) Then   'has med
                                                        If UCase(dragr("Flag")).StartsWith("POS") Or UCase(dragr("Flag")).StartsWith("REACT") Or
                                                        UCase(dragr("Flag")).StartsWith("DETECT") Or UCase(dragr("Flag")).StartsWith("FOUND") Or
                                                        ((UCase(dragr("Flag")).StartsWith("NON") Or UCase(dragr("Flag")).StartsWith("NOT")) And
                                                        InStr(UCase(dragr("Flag")), "NEG") > 0) Or (UCase(dragr("Flag")).StartsWith("SENT") And
                                                        InStr(UCase(dragr("Flag")), "CONFIRM") > 0) Then
                                                            FlagInfo(0) = "CONSISTENT"
                                                            FlagInfo(1) = "Ignore"
                                                        Else
                                                            FlagInfo(0) = "INCONSISTENT"
                                                            FlagInfo(1) = "Caution"
                                                        End If
                                                    Else    'No Meds
                                                        If UCase(dragr("Flag")).StartsWith("POS") Or UCase(dragr("Flag")).StartsWith("MEDIUM POS") Or
                                                        UCase(dragr("Flag")).StartsWith("HIGH POS") Or UCase(dragr("Flag")).StartsWith("LOW POS") Or
                                                        UCase(dragr("Flag")).StartsWith("REACT") Or UCase(dragr("Flag")).StartsWith("DETECT") Or
                                                        UCase(dragr("Flag")).StartsWith("FOUND") Or ((UCase(dragr("Flag")).StartsWith("NON") Or
                                                        UCase(dragr("Flag")).StartsWith("NOT")) And InStr(UCase(dragr("Flag")), "NEG") > 0) Or
                                                        (UCase(dragr("Flag")).StartsWith("SENT") And InStr(UCase(dragr("Flag")), "CONFIRM") > 0) Then
                                                            FlagInfo(0) = "INCONSISTENT"
                                                            FlagInfo(1) = "Caution"
                                                        Else
                                                            FlagInfo(0) = "CONSISTENT"
                                                            FlagInfo(1) = "Ignore"
                                                        End If
                                                    End If
                                                End If
                                                Exit While
                                            End If
                                        ElseIf InStr(dragr("ValueFrom"), ">") > 0 Then
                                            If CSng(NumRes) > dragr("ValueTo") Then
                                                FlagInfo(0) = dragr("Flag")
                                                If dragr("Behavior") IsNot DBNull.Value Then FlagInfo(1) = dragr("Behavior")
                                                If ForI = False Then    'Flag
                                                    If UCase(dragr("Flag")).ToString.StartsWith("LP") Or (UCase(dragr("Flag")).ToString.StartsWith("LOW") And
                                                    (InStr(UCase(dragr("Flag")), "PANIC") > 0 Or InStr(UCase(dragr("Flag")), "CRITIC") > 0)) Then
                                                        FlagInfo(0) = "LP"
                                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                                    ElseIf UCase(dragr("Flag")).ToString.StartsWith("LOW") Or (UCase(dragr("Flag")).ToString.StartsWith("L ") And
                                                    InStr(UCase(dragr("Flag")), "LOW") > 0) Then
                                                        FlagInfo(0) = "L"
                                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                    ElseIf (UCase(dragr("Flag")).ToString.StartsWith("N") And InStr(UCase(dragr("Flag")), "NORMAL") > 0) Or
                                                    UCase(dragr("Flag")).ToString.StartsWith("NEG") Or (UCase(dragr("Flag")).ToString.StartsWith("NON") And
                                                    InStr(UCase(dragr("Flag")), "REACT") > 0) Or UCase(dragr("Flag")).ToString.StartsWith("SENSIT") Or
                                                    UCase(dragr("Flag")).ToString.StartsWith("SUSCEP") Then
                                                        FlagInfo(0) = "N"
                                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Ignore"
                                                    ElseIf UCase(dragr("Flag")).ToString.StartsWith("AB") Or (UCase(dragr("Flag")).ToString.StartsWith("A") And
                                                    InStr(UCase(dragr("Flag")), "ABNORMAL") > 0) Or UCase(dragr("Flag")).ToString.StartsWith("REAC") Or
                                                    UCase(dragr("Flag")).ToString.StartsWith("POS") Or UCase(dragr("Flag")).StartsWith("MEDIUM POS") Or
                                                    UCase(dragr("Flag")).StartsWith("HIGH POS") Or UCase(dragr("Flag")).StartsWith("LOW POS") Or
                                                    UCase(dragr("Flag")).ToString.StartsWith("DETEC") Or UCase(dragr("Flag")).ToString.StartsWith("RESIST") Or
                                                    (UCase(dragr("Flag")).ToString.StartsWith("NON") And (InStr(UCase(dragr("Flag")), "NEG") > 0 Or
                                                    InStr(UCase(dragr("Flag")), "POS") > 0)) Then
                                                        FlagInfo(0) = "A"
                                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                    ElseIf UCase(dragr("Flag")).ToString.StartsWith("HP") Or UCase(dragr("Flag")).ToString.StartsWith("HIGH PANIC") Or
                                                    UCase(dragr("Flag")).ToString.StartsWith("HIGH CRITIC") Then
                                                        FlagInfo(0) = "HP"
                                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                                    ElseIf UCase(dragr("Flag")).ToString.StartsWith("HIGH") Or (UCase(dragr("Flag")).ToString.StartsWith("H") And
                                                    InStr(UCase(dragr("Flag")), "HIGH") > 0) Then
                                                        FlagInfo(0) = "H"
                                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                    End If
                                                End If
                                                '
                                                If MedEval = True AndAlso MedsInAccession(AccID) Then  'has meds
                                                    If TestInMeds(TestID, AccID) Then   'has med
                                                        If UCase(dragr("Flag")).StartsWith("POS") Or UCase(dragr("Flag")).StartsWith("REACT") Or
                                                        UCase(dragr("Flag")).StartsWith("DETECT") Or UCase(dragr("Flag")).StartsWith("FOUND") Or
                                                        ((UCase(dragr("Flag")).StartsWith("NON") Or UCase(dragr("Flag")).StartsWith("NOT")) And
                                                        InStr(UCase(dragr("Flag")), "NEG") > 0) Or (UCase(dragr("Flag")).StartsWith("SENT") And
                                                        InStr(UCase(dragr("Flag")), "CONFIRM") > 0) Then
                                                            FlagInfo(0) = "CONSISTENT"
                                                            FlagInfo(1) = "Ignore"
                                                        Else
                                                            FlagInfo(0) = "INCONSISTENT"
                                                            FlagInfo(1) = "Caution"
                                                        End If
                                                    Else    'No Meds
                                                        If UCase(dragr("Flag")).StartsWith("POS") Or UCase(dragr("Flag")).StartsWith("MEDIUM POS") Or
                                                        UCase(dragr("Flag")).StartsWith("HIGH POS") Or UCase(dragr("Flag")).StartsWith("LOW POS") Or
                                                        UCase(dragr("Flag")).StartsWith("REACT") Or UCase(dragr("Flag")).StartsWith("DETECT") Or
                                                        UCase(dragr("Flag")).StartsWith("FOUND") Or ((UCase(dragr("Flag")).StartsWith("NON") Or
                                                        UCase(dragr("Flag")).StartsWith("NOT")) And InStr(UCase(dragr("Flag")), "NEG") > 0) Or
                                                        (UCase(dragr("Flag")).StartsWith("SENT") And InStr(UCase(dragr("Flag")), "CONFIRM") > 0) Then
                                                            FlagInfo(0) = "INCONSISTENT"
                                                            FlagInfo(1) = "Caution"
                                                        Else
                                                            FlagInfo(0) = "CONSISTENT"
                                                            FlagInfo(1) = "Ignore"
                                                        End If
                                                    End If
                                                End If
                                                Exit While
                                            End If
                                        ElseIf IsNumeric(dragr("ValueFrom")) Then
                                            If CSng(NumRes) >= CSng(dragr("ValueFrom").ToString) And
                                            CSng(NumRes) <= CSng(dragr("ValueTo").ToString) Then 'Or CSng(NumRes) >= CSng(dragr("ValueTo").ToString) Temur

                                                FlagInfo(0) = dragr("Flag")
                                                If dragr("Behavior") IsNot DBNull.Value Then FlagInfo(1) = dragr("Behavior")
                                                If ForI = False Then    'Flag
                                                    If UCase(dragr("Flag")).ToString.StartsWith("LP") Or
                                                    (UCase(dragr("Flag")).ToString.StartsWith("LOW") And
                                                    (InStr(UCase(dragr("Flag")), "PANIC") > 0 Or
                                                    InStr(UCase(dragr("Flag")), "CRITIC") > 0)) Then
                                                        FlagInfo(0) = "LP"
                                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                                    ElseIf UCase(dragr("Flag")).ToString.StartsWith("LOW") Or
                                                    (UCase(dragr("Flag")).ToString.StartsWith("L ") And
                                                    InStr(UCase(dragr("Flag")), "LOW") > 0) Then
                                                        FlagInfo(0) = "L"
                                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                    ElseIf (UCase(dragr("Flag")).ToString.StartsWith("N") And
                                                    InStr(UCase(dragr("Flag")), "NORMAL") > 0) Or
                                                    UCase(dragr("Flag")).ToString.StartsWith("NEG") Or
                                                    (UCase(dragr("Flag")).ToString.StartsWith("NON") And
                                                    InStr(UCase(dragr("Flag")), "REACT") > 0) Or
                                                    UCase(dragr("Flag")).ToString.StartsWith("SENSIT") Or
                                                    UCase(dragr("Flag")).ToString.StartsWith("SUSCEP") Then
                                                        FlagInfo(0) = "N"
                                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Ignore"
                                                    ElseIf UCase(dragr("Flag")).ToString.StartsWith("AB") Or
                                                    (UCase(dragr("Flag")).ToString.StartsWith("A") And
                                                    InStr(UCase(dragr("Flag")), "ABNORMAL") > 0) Or
                                                    UCase(dragr("Flag")).ToString.StartsWith("REAC") Or
                                                    UCase(dragr("Flag")).ToString.StartsWith("POS") Or
                                                    UCase(dragr("Flag")).StartsWith("MEDIUM POS") Or
                                                    UCase(dragr("Flag")).StartsWith("HIGH POS") Or
                                                    UCase(dragr("Flag")).StartsWith("LOW POS") Or
                                                    UCase(dragr("Flag")).ToString.StartsWith("DETEC") Or
                                                    UCase(dragr("Flag")).ToString.StartsWith("RESIST") Or
                                                    (UCase(dragr("Flag")).ToString.StartsWith("NON") And
                                                    (InStr(UCase(dragr("Flag")), "NEG") > 0 Or
                                                    InStr(UCase(dragr("Flag")), "POS") > 0)) Then
                                                        FlagInfo(0) = "A"
                                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                    ElseIf UCase(dragr("Flag")).ToString.StartsWith("HP") Or
                                                    UCase(dragr("Flag")).ToString.StartsWith("HIGH PANIC") Or
                                                    UCase(dragr("Flag")).ToString.StartsWith("HIGH CRITIC") Then
                                                        FlagInfo(0) = "HP"
                                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                                    ElseIf UCase(dragr("Flag")).ToString.StartsWith("HIGH") Or
                                                    (UCase(dragr("Flag")).ToString.StartsWith("H") And
                                                    InStr(UCase(dragr("Flag")), "HIGH") > 0) Then
                                                        FlagInfo(0) = "H"
                                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                    End If
                                                End If
                                                '
                                                If MedEval = True AndAlso MedsInAccession(AccID) Then  'has meds
                                                    If TestInMeds(TestID, AccID) Then   'has med
                                                        If UCase(dragr("Flag")).StartsWith("POS") Or UCase(dragr("Flag")).StartsWith("REACT") Or
                                                        UCase(dragr("Flag")).StartsWith("DETECT") Or UCase(dragr("Flag")).StartsWith("FOUND") Or
                                                        ((UCase(dragr("Flag")).StartsWith("NON") Or UCase(dragr("Flag")).StartsWith("NOT")) And
                                                        InStr(UCase(dragr("Flag")), "NEG") > 0) Or (UCase(dragr("Flag")).StartsWith("SENT") And
                                                        InStr(UCase(dragr("Flag")), "CONFIRM") > 0) Then
                                                            FlagInfo(0) = "CONSISTENT"
                                                            FlagInfo(1) = "Ignore"
                                                        Else
                                                            FlagInfo(0) = "INCONSISTENT"
                                                            FlagInfo(1) = "Caution"
                                                        End If
                                                    Else    'No Meds
                                                        If UCase(dragr("Flag")).StartsWith("POS") Or UCase(dragr("Flag")).StartsWith("MEDIUM POS") Or
                                                        UCase(dragr("Flag")).StartsWith("HIGH POS") Or UCase(dragr("Flag")).StartsWith("LOW POS") Or
                                                        UCase(dragr("Flag")).StartsWith("REACT") Or UCase(dragr("Flag")).StartsWith("DETECT") Or
                                                        UCase(dragr("Flag")).StartsWith("FOUND") Or ((UCase(dragr("Flag")).StartsWith("NON") Or
                                                        UCase(dragr("Flag")).StartsWith("NOT")) And InStr(UCase(dragr("Flag")), "NEG") > 0) Or
                                                        (UCase(dragr("Flag")).StartsWith("SENT") And InStr(UCase(dragr("Flag")), "CONFIRM") > 0) Then
                                                            FlagInfo(0) = "INCONSISTENT"
                                                            FlagInfo(1) = "Caution"
                                                        Else
                                                            FlagInfo(0) = "CONSISTENT"
                                                            FlagInfo(1) = "Ignore"
                                                        End If
                                                    End If
                                                End If
                                                Exit While
                                            End If
                                        End If
                                    End If
                                Else            'Non-Numeric Result
                                    If NumRes = dragr("Flag") Then
                                        If ForI = False Then    'Flag
                                            If UCase(dragr("Flag")).ToString.StartsWith("LP") Or (UCase(dragr("Flag")).ToString.StartsWith("LOW") And
                                            (InStr(UCase(dragr("Flag")), "PANIC") > 0 Or InStr(UCase(dragr("Flag")), "CRITIC") > 0)) Then
                                                FlagInfo(0) = "LP"
                                                If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                            ElseIf UCase(dragr("Flag")).ToString.StartsWith("LOW") Or (UCase(dragr("Flag")).ToString.StartsWith("L ") And
                                            InStr(UCase(dragr("Flag")), "LOW") > 0) Then
                                                FlagInfo(0) = "L"
                                                If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                            ElseIf (UCase(dragr("Flag")).ToString.StartsWith("N") And InStr(UCase(dragr("Flag")), "NORMAL") > 0) Or
                                            UCase(dragr("Flag")).ToString.StartsWith("NEG") Or (UCase(dragr("Flag")).ToString.StartsWith("NON") And
                                            InStr(UCase(dragr("Flag")), "REACT") > 0) Or UCase(dragr("Flag")).ToString.StartsWith("SENSIT") Or
                                            UCase(dragr("Flag")).ToString.StartsWith("SUSCEP") Then
                                                FlagInfo(0) = "N"
                                                If FlagInfo(1) = "" Then FlagInfo(1) = "Ignore"
                                            ElseIf UCase(dragr("Flag")).ToString.StartsWith("AB") Or (UCase(dragr("Flag")).ToString.StartsWith("A") And
                                            InStr(UCase(dragr("Flag")), "ABNORMAL") > 0) Or UCase(dragr("Flag")).ToString.StartsWith("REAC") Or
                                            UCase(dragr("Flag")).ToString.StartsWith("POS") Or UCase(dragr("Flag")).StartsWith("MEDIUM POS") Or
                                            UCase(dragr("Flag")).StartsWith("HIGH POS") Or UCase(dragr("Flag")).StartsWith("LOW POS") Or
                                            UCase(dragr("Flag")).ToString.StartsWith("DETEC") Or UCase(dragr("Flag")).ToString.StartsWith("RESIST") Or
                                            (UCase(dragr("Flag")).ToString.StartsWith("NON") And (InStr(UCase(dragr("Flag")), "NEG") > 0 Or
                                            InStr(UCase(dragr("Flag")), "POS") > 0)) Then
                                                FlagInfo(0) = "A"
                                                If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                            ElseIf UCase(dragr("Flag")).ToString.StartsWith("HP") Or UCase(dragr("Flag")).ToString.StartsWith("HIGH PANIC") Or
                                            UCase(dragr("Flag")).ToString.StartsWith("HIGH CRITIC") Then
                                                FlagInfo(0) = "HP"
                                                If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                            ElseIf UCase(dragr("Flag")).ToString.StartsWith("HIGH") Or (UCase(dragr("Flag")).ToString.StartsWith("H") And
                                            InStr(UCase(dragr("Flag")), "HIGH") > 0) Then
                                                FlagInfo(0) = "H"
                                                If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                            End If
                                        Else
                                            If MedEval = True AndAlso MedsInAccession(AccID) Then  'has meds
                                                If TestInMeds(TestID, AccID) Then   'has med
                                                    If UCase(dragr("Flag")).StartsWith("POS") Or UCase(dragr("Flag")).StartsWith("REACT") Or
                                                    UCase(dragr("Flag")).StartsWith("DETECT") Or UCase(dragr("Flag")).StartsWith("FOUND") Or
                                                    ((UCase(dragr("Flag")).StartsWith("NON") Or UCase(dragr("Flag")).StartsWith("NOT")) And
                                                    InStr(UCase(dragr("Flag")), "NEG") > 0) Or (UCase(dragr("Flag")).StartsWith("SENT") And
                                                    InStr(UCase(dragr("Flag")), "CONFIRM") > 0) Then
                                                        FlagInfo(0) = "CONSISTENT"
                                                        FlagInfo(1) = "Ignore"
                                                    Else
                                                        FlagInfo(0) = "INCONSISTENT"
                                                        FlagInfo(1) = "Caution"
                                                    End If
                                                Else    'No Meds
                                                    If UCase(dragr("Flag")).StartsWith("POS") Or UCase(dragr("Flag")).StartsWith("MEDIUM POS") Or
                                                    UCase(dragr("Flag")).StartsWith("HIGH POS") Or UCase(dragr("Flag")).StartsWith("LOW POS") Or
                                                    UCase(dragr("Flag")).StartsWith("REACT") Or UCase(dragr("Flag")).StartsWith("DETECT") Or
                                                    UCase(dragr("Flag")).StartsWith("FOUND") Or ((UCase(dragr("Flag")).StartsWith("NON") Or
                                                    UCase(dragr("Flag")).StartsWith("NOT")) And InStr(UCase(dragr("Flag")), "NEG") > 0) Or
                                                    (UCase(dragr("Flag")).StartsWith("SENT") And InStr(UCase(dragr("Flag")), "CONFIRM") > 0) Then
                                                        FlagInfo(0) = "INCONSISTENT"
                                                        FlagInfo(1) = "Caution"
                                                    Else
                                                        FlagInfo(0) = "CONSISTENT"
                                                        FlagInfo(1) = "Ignore"
                                                    End If
                                                End If
                                            End If
                                        End If
                                        Exit While
                                    End If
                                End If
                            End While
                        End If
                        cnagr.Close()
                        cnagr = Nothing
                    Else  'Numeric(ranges)
                        sSQL = "Select * from N_Ranges where Test_ID = " & TestID
                        Dim cnnr As New SqlConnection(connString)
                        cnnr.Open()
                        Dim cmdnr As New SqlCommand(sSQL, cnnr)
                        cmdnr.CommandType = CommandType.Text
                        Dim drnr As SqlDataReader = cmdnr.ExecuteReader
                        If drnr.HasRows Then
                            While drnr.Read
                                If IsNumeric(NumRes) Then       'Numeric Result
                                    If InStr(drnr("ValueFrom"), "<") > 0 Then
                                        If CSng(NumRes) < drnr("ValueTo") Then
                                            FlagInfo(0) = drnr("Flag")
                                            If drnr("Behavior") IsNot DBNull.Value Then FlagInfo(1) = drnr("Behavior")
                                            If ForI = False Then    'Flag
                                                If UCase(drnr("Flag")).ToString.StartsWith("LP") Or
                                                (UCase(drnr("Flag")).ToString.StartsWith("LOW") And
                                                (InStr(UCase(drnr("Flag")), "PANIC") > 0 Or
                                                InStr(UCase(drnr("Flag")), "CRITIC") > 0)) Then
                                                    FlagInfo(0) = "LP"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                                ElseIf UCase(drnr("Flag")).ToString.StartsWith("LOW") Or
                                                (UCase(drnr("Flag")).ToString.StartsWith("L ") And
                                                InStr(UCase(drnr("Flag")), "LOW") > 0) Then
                                                    FlagInfo(0) = "L"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                ElseIf (UCase(drnr("Flag")).ToString.StartsWith("N") And
                                                InStr(UCase(drnr("Flag")), "NORMAL") > 0) Or
                                                UCase(drnr("Flag")).ToString.StartsWith("NEG") Or
                                                (UCase(drnr("Flag")).ToString.StartsWith("NON") And
                                                InStr(UCase(drnr("Flag")), "REACT") > 0) Or
                                                UCase(drnr("Flag")).ToString.StartsWith("SENSIT") Or
                                                UCase(drnr("Flag")).ToString.StartsWith("SUSCEP") Then
                                                    FlagInfo(0) = "N"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Ignore"
                                                ElseIf UCase(drnr("Flag")).ToString.StartsWith("AB") Or
                                                (UCase(drnr("Flag")).ToString.StartsWith("A") And
                                                InStr(UCase(drnr("Flag")), "ABNORMAL") > 0) Or
                                                UCase(drnr("Flag")).ToString.StartsWith("REAC") Or
                                                UCase(drnr("Flag")).ToString.StartsWith("POS") Or
                                                UCase(drnr("Flag")).StartsWith("MEDIUM POS") Or
                                                UCase(drnr("Flag")).StartsWith("HIGH POS") Or
                                                UCase(drnr("Flag")).StartsWith("LOW POS") Or
                                                UCase(drnr("Flag")).ToString.StartsWith("DETEC") Or
                                                UCase(drnr("Flag")).ToString.StartsWith("RESIST") Or
                                                (UCase(drnr("Flag")).ToString.StartsWith("NON") And
                                                (InStr(UCase(drnr("Flag")), "NEG") > 0 Or
                                                InStr(UCase(drnr("Flag")), "POS") > 0)) Then
                                                    FlagInfo(0) = "A"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                ElseIf UCase(drnr("Flag")).ToString.StartsWith("HP") Or
                                                UCase(drnr("Flag")).ToString.StartsWith("HIGH PANIC") Or
                                                UCase(drnr("Flag")).ToString.StartsWith("HIGH CRITIC") Then
                                                    FlagInfo(0) = "HP"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                                ElseIf UCase(drnr("Flag")).ToString.StartsWith("HIGH") Or
                                                (UCase(drnr("Flag")).ToString.StartsWith("H") And
                                                InStr(UCase(drnr("Flag")), "HIGH") > 0) Then
                                                    FlagInfo(0) = "H"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                End If
                                            End If
                                            '
                                            If MedEval = True AndAlso MedsInAccession(AccID) Then  'has meds
                                                If TestInMeds(TestID, AccID) Then   'has med
                                                    If UCase(drnr("Flag")).StartsWith("POS") Or UCase(drnr("Flag")).StartsWith("REACT") Or
                                                    UCase(drnr("Flag")).StartsWith("DETECT") Or UCase(drnr("Flag")).StartsWith("FOUND") Or
                                                    ((UCase(drnr("Flag")).StartsWith("NON") Or UCase(drnr("Flag")).StartsWith("NOT")) And
                                                    InStr(UCase(drnr("Flag")), "NEG") > 0) Or (UCase(drnr("Flag")).StartsWith("SENT") And
                                                    InStr(UCase(drnr("Flag")), "CONFIRM") > 0) Then
                                                        FlagInfo(0) = "CONSISTENT"
                                                        FlagInfo(1) = "Ignore"
                                                    Else
                                                        FlagInfo(0) = "INCONSISTENT"
                                                        FlagInfo(1) = "Caution"
                                                    End If
                                                Else    'No Meds
                                                    If UCase(drnr("Flag")).StartsWith("POS") Or UCase(drnr("Flag")).StartsWith("MEDIUM POS") Or
                                                    UCase(drnr("Flag")).StartsWith("HIGH POS") Or UCase(drnr("Flag")).StartsWith("LOW POS") Or
                                                    UCase(drnr("Flag")).StartsWith("REACT") Or UCase(drnr("Flag")).StartsWith("DETECT") Or
                                                    UCase(drnr("Flag")).StartsWith("FOUND") Or ((UCase(drnr("Flag")).StartsWith("NON") Or
                                                    UCase(drnr("Flag")).StartsWith("NOT")) And InStr(UCase(drnr("Flag")), "NEG") > 0) Or
                                                    (UCase(drnr("Flag")).StartsWith("SENT") And InStr(UCase(drnr("Flag")), "CONFIRM") > 0) Then
                                                        FlagInfo(0) = "INCONSISTENT"
                                                        FlagInfo(1) = "Caution"
                                                    Else
                                                        FlagInfo(0) = "CONSISTENT"
                                                        FlagInfo(1) = "Ignore"
                                                    End If
                                                End If
                                            End If
                                            Exit While
                                        End If
                                    ElseIf InStr(drnr("ValueFrom"), ">") > 0 Then
                                        If CSng(NumRes) > drnr("ValueTo") Then
                                            FlagInfo(0) = drnr("Flag")
                                            If drnr("Behavior") IsNot DBNull.Value Then FlagInfo(1) = drnr("Behavior")
                                            If ForI = False Then    'Flag
                                                If UCase(drnr("Flag")).ToString.StartsWith("LP") Or
                                                    (UCase(drnr("Flag")).ToString.StartsWith("LOW") And
                                                    (InStr(UCase(drnr("Flag")), "PANIC") > 0 Or
                                                    InStr(UCase(drnr("Flag")), "CRITIC") > 0)) Then
                                                    FlagInfo(0) = "LP"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                                ElseIf UCase(drnr("Flag")).ToString.StartsWith("LOW") Or
                                                (UCase(drnr("Flag")).ToString.StartsWith("L ") And
                                                InStr(UCase(drnr("Flag")), "LOW") > 0) Then
                                                    FlagInfo(0) = "L"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                ElseIf (UCase(drnr("Flag")).ToString.StartsWith("N") And
                                                InStr(UCase(drnr("Flag")), "NORMAL") > 0) Or
                                                UCase(drnr("Flag")).ToString.StartsWith("NEG") Or
                                                (UCase(drnr("Flag")).ToString.StartsWith("NON") And
                                                InStr(UCase(drnr("Flag")), "REACT") > 0) Or
                                                UCase(drnr("Flag")).ToString.StartsWith("SENSIT") Or
                                                UCase(drnr("Flag")).ToString.StartsWith("SUSCEP") Then
                                                    FlagInfo(0) = "N"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Ignore"
                                                ElseIf UCase(drnr("Flag")).ToString.StartsWith("AB") Or
                                                (UCase(drnr("Flag")).ToString.StartsWith("A") And
                                                InStr(UCase(drnr("Flag")), "ABNORMAL") > 0) Or
                                                UCase(drnr("Flag")).ToString.StartsWith("REAC") Or
                                                UCase(drnr("Flag")).ToString.StartsWith("POS") Or
                                                UCase(drnr("Flag")).StartsWith("MEDIUM POS") Or
                                                UCase(drnr("Flag")).StartsWith("HIGH POS") Or
                                                UCase(drnr("Flag")).StartsWith("LOW POS") Or
                                                UCase(drnr("Flag")).ToString.StartsWith("DETEC") Or
                                                UCase(drnr("Flag")).ToString.StartsWith("RESIST") Or
                                                (UCase(drnr("Flag")).ToString.StartsWith("NON") And
                                                (InStr(UCase(drnr("Flag")), "NEG") > 0 Or
                                                InStr(UCase(drnr("Flag")), "POS") > 0)) Then
                                                    FlagInfo(0) = "A"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                ElseIf UCase(drnr("Flag")).ToString.StartsWith("HP") Or
                                                UCase(drnr("Flag")).ToString.StartsWith("HIGH PANIC") Or
                                                UCase(drnr("Flag")).ToString.StartsWith("HIGH CRITIC") Then
                                                    FlagInfo(0) = "HP"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                                ElseIf UCase(drnr("Flag")).ToString.StartsWith("HIGH") Or
                                                (UCase(drnr("Flag")).ToString.StartsWith("H") And
                                                InStr(UCase(drnr("Flag")), "HIGH") > 0) Then
                                                    FlagInfo(0) = "H"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                End If
                                            End If
                                            '
                                            If MedEval = True AndAlso MedsInAccession(AccID) Then  'has meds
                                                If TestInMeds(TestID, AccID) Then   'has med
                                                    If UCase(drnr("Flag")).StartsWith("POS") Or UCase(drnr("Flag")).StartsWith("REACT") Or
                                                    UCase(drnr("Flag")).StartsWith("DETECT") Or UCase(drnr("Flag")).StartsWith("FOUND") Or
                                                    ((UCase(drnr("Flag")).StartsWith("NON") Or UCase(drnr("Flag")).StartsWith("NOT")) And
                                                    InStr(UCase(drnr("Flag")), "NEG") > 0) Or (UCase(drnr("Flag")).StartsWith("SENT") And
                                                    InStr(UCase(drnr("Flag")), "CONFIRM") > 0) Then
                                                        FlagInfo(0) = "CONSISTENT"
                                                        FlagInfo(1) = "Ignore"
                                                    Else
                                                        FlagInfo(0) = "INCONSISTENT"
                                                        FlagInfo(1) = "Caution"
                                                    End If
                                                Else    'No Meds
                                                    If UCase(drnr("Flag")).StartsWith("POS") Or UCase(drnr("Flag")).StartsWith("MEDIUM POS") Or
                                                    UCase(drnr("Flag")).StartsWith("HIGH POS") Or UCase(drnr("Flag")).StartsWith("LOW POS") Or
                                                    UCase(drnr("Flag")).StartsWith("REACT") Or UCase(drnr("Flag")).StartsWith("DETECT") Or
                                                    UCase(drnr("Flag")).StartsWith("FOUND") Or ((UCase(drnr("Flag")).StartsWith("NON") Or
                                                    UCase(drnr("Flag")).StartsWith("NOT")) And InStr(UCase(drnr("Flag")), "NEG") > 0) Or
                                                    (UCase(drnr("Flag")).StartsWith("SENT") And InStr(UCase(drnr("Flag")), "CONFIRM") > 0) Then
                                                        FlagInfo(0) = "INCONSISTENT"
                                                        FlagInfo(1) = "Caution"
                                                    Else
                                                        FlagInfo(0) = "CONSISTENT"
                                                        FlagInfo(1) = "Ignore"
                                                    End If
                                                End If
                                            End If
                                            Exit While
                                        End If
                                    ElseIf IsNumeric(drnr("ValueFrom")) Then
                                        If CSng(NumRes) >= CSng(drnr("ValueFrom").ToString) And CSng(NumRes) <= drnr("ValueTo") Then
                                            FlagInfo(0) = drnr("Flag")
                                            If drnr("Behavior") IsNot DBNull.Value Then FlagInfo(1) = drnr("Behavior")
                                            If ForI = False Then    'Flag
                                                If UCase(drnr("Flag")).ToString.StartsWith("LP") Or (UCase(drnr("Flag")).ToString.StartsWith("LOW") And
                                                    (InStr(UCase(drnr("Flag")), "PANIC") > 0 Or InStr(UCase(drnr("Flag")), "CRITIC") > 0)) Then
                                                    FlagInfo(0) = "LP"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                                ElseIf UCase(drnr("Flag")).ToString.StartsWith("LOW") Or
                                                (UCase(drnr("Flag")).ToString.StartsWith("L ") And
                                                InStr(UCase(drnr("Flag")), "LOW") > 0) Then
                                                    FlagInfo(0) = "L"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                ElseIf (UCase(drnr("Flag")).ToString.StartsWith("N") And
                                                InStr(UCase(drnr("Flag")), "NORMAL") > 0) Or
                                                UCase(drnr("Flag")).ToString.StartsWith("NEG") Or
                                                ((UCase(drnr("Flag")).ToString.StartsWith("NON") Or
                                                UCase(drnr("Flag")).ToString.StartsWith("NOT")) And
                                                (InStr(UCase(drnr("Flag")), "REACT") > 0 Or
                                                InStr(UCase(drnr("Flag")), "DETECT") > 0 Or
                                                InStr(UCase(drnr("Flag")), "FOUND") > 0)) Or
                                                UCase(drnr("Flag")).ToString.StartsWith("SENSIT") Or
                                                UCase(drnr("Flag")).ToString.StartsWith("SUSCEP") Then
                                                    FlagInfo(0) = "N"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Ignore"
                                                ElseIf ((UCase(drnr("Flag")).ToString.StartsWith("AB") Or
                                                UCase(drnr("Flag")).ToString.StartsWith("A")) And
                                                InStr(UCase(drnr("Flag")), "ABNORMAL") > 0) Or
                                                UCase(drnr("Flag")).ToString.StartsWith("REAC") Or
                                                UCase(drnr("Flag")).ToString.StartsWith("POS") Or
                                                UCase(drnr("Flag")).StartsWith("MEDIUM POS") Or
                                                UCase(drnr("Flag")).StartsWith("HIGH POS") Or
                                                UCase(drnr("Flag")).StartsWith("LOW POS") Or
                                                UCase(drnr("Flag")).ToString.StartsWith("DETEC") Or
                                                UCase(drnr("Flag")).ToString.StartsWith("RESIST") Or
                                                (UCase(drnr("Flag")).ToString.StartsWith("NON") And
                                                (InStr(UCase(drnr("Flag")), "NEG") > 0 Or
                                                InStr(UCase(drnr("Flag")), "POS") > 0)) Then
                                                    FlagInfo(0) = "A"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                ElseIf UCase(drnr("Flag")).ToString.StartsWith("HP") Or
                                                UCase(drnr("Flag")).ToString.StartsWith("HIGH PANIC") Or
                                                UCase(drnr("Flag")).ToString.StartsWith("HIGH CRITIC") Then
                                                    FlagInfo(0) = "HP"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                                ElseIf UCase(drnr("Flag")).ToString.StartsWith("HIGH") Or
                                                (UCase(drnr("Flag")).ToString.StartsWith("H") And
                                                InStr(UCase(drnr("Flag")), "HIGH") > 0) Then
                                                    FlagInfo(0) = "H"
                                                    If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                End If
                                            End If
                                            '
                                            If MedEval = True AndAlso MedsInAccession(AccID) Then  'has meds
                                                If TestInMeds(TestID, AccID) Then   'has med
                                                    If UCase(drnr("Flag")).StartsWith("POS") Or UCase(drnr("Flag")).StartsWith("REACT") Or
                                                    UCase(drnr("Flag")).StartsWith("DETECT") Or UCase(drnr("Flag")).StartsWith("FOUND") Or
                                                    ((UCase(drnr("Flag")).StartsWith("NON") Or UCase(drnr("Flag")).StartsWith("NOT")) And
                                                    InStr(UCase(drnr("Flag")), "NEG") > 0) Or (UCase(drnr("Flag")).StartsWith("SENT") And
                                                    InStr(UCase(drnr("Flag")), "CONFIRM") > 0) Then
                                                        FlagInfo(0) = "CONSISTENT"
                                                        FlagInfo(1) = "Ignore"
                                                    Else
                                                        FlagInfo(0) = "INCONSISTENT"
                                                        FlagInfo(1) = "Caution"
                                                    End If
                                                Else    'No Meds
                                                    If UCase(drnr("Flag")).StartsWith("POS") Or UCase(drnr("Flag")).StartsWith("MEDIUM POS") Or
                                                    UCase(drnr("Flag")).StartsWith("HIGH POS") Or UCase(drnr("Flag")).StartsWith("LOW POS") Or
                                                    UCase(drnr("Flag")).StartsWith("REACT") Or UCase(drnr("Flag")).StartsWith("DETECT") Or
                                                    UCase(drnr("Flag")).StartsWith("FOUND") Or ((UCase(drnr("Flag")).StartsWith("NON") Or
                                                    UCase(drnr("Flag")).StartsWith("NOT")) And InStr(UCase(drnr("Flag")), "NEG") > 0) Or
                                                    (UCase(drnr("Flag")).StartsWith("SENT") And InStr(UCase(drnr("Flag")), "CONFIRM") > 0) Then
                                                        FlagInfo(0) = "INCONSISTENT"
                                                        FlagInfo(1) = "Caution"
                                                    Else
                                                        FlagInfo(0) = "CONSISTENT"
                                                        FlagInfo(1) = "Ignore"
                                                    End If
                                                End If
                                            End If
                                            Exit While
                                        End If
                                    End If
                                Else    'Non-Numeric result
                                    If drnr("Flag") IsNot DBNull.Value Then
                                        If UCase(NumRes) = UCase(drnr("Flag")) Then
                                            If ForI = True Then    'interp
                                                If MedEval = True AndAlso MedsInAccession(AccID) Then  'has meds
                                                    If TestInMeds(TestID, AccID) Then   'has med
                                                        If UCase(drnr("Flag")).StartsWith("POS") Or UCase(drnr("Flag")).StartsWith("MEDIUM POS") Or
                                                        UCase(drnr("Flag")).StartsWith("HIGH POS") Or UCase(drnr("Flag")).StartsWith("LOW POS") Or
                                                        UCase(drnr("Flag")).StartsWith("REACT") Or UCase(drnr("Flag")).StartsWith("DETECT") Or
                                                        UCase(drnr("Flag")).StartsWith("FOUND") Or ((UCase(drnr("Flag")).StartsWith("NON") Or
                                                        UCase(drnr("Flag")).StartsWith("NOT")) And InStr(UCase(drnr("Flag")), "NEG") > 0) Or
                                                        (UCase(drnr("Flag")).StartsWith("SENT") And InStr(UCase(drnr("Flag")), "CONFIRM") > 0) Then
                                                            FlagInfo(0) = "CONSISTENT"
                                                            FlagInfo(1) = "Ignore"
                                                        Else
                                                            FlagInfo(0) = "INCONSISTENT"
                                                            FlagInfo(1) = "Caution"
                                                        End If
                                                    Else    'No Meds
                                                        If UCase(drnr("Flag")).StartsWith("POS") Or UCase(drnr("Flag")).StartsWith("MEDIUM POS") Or
                                                        UCase(drnr("Flag")).StartsWith("HIGH POS") Or UCase(drnr("Flag")).StartsWith("LOW POS") Or
                                                        UCase(drnr("Flag")).StartsWith("REACT") Or UCase(drnr("Flag")).StartsWith("DETECT") Or
                                                        UCase(drnr("Flag")).StartsWith("FOUND") Or ((UCase(drnr("Flag")).StartsWith("NON") Or
                                                        UCase(drnr("Flag")).StartsWith("NOT")) And InStr(UCase(drnr("Flag")), "NEG") > 0) Or
                                                        (UCase(drnr("Flag")).StartsWith("SENT") And InStr(UCase(drnr("Flag")), "CONFIRM") > 0) Then
                                                            FlagInfo(0) = "INCONSISTENT"
                                                            FlagInfo(1) = "Caution"
                                                        Else
                                                            FlagInfo(0) = "CONSISTENT"
                                                            FlagInfo(1) = "Ignore"
                                                        End If
                                                    End If
                                                Else
                                                    FlagInfo(0) = drnr("Flag")
                                                    If UCase(FlagInfo(0)).ToString.StartsWith("LP") Or (UCase(FlagInfo(0)).ToString.StartsWith("LOW") And
                                                    (InStr(UCase(FlagInfo(0)), "PANIC") > 0 Or InStr(UCase(FlagInfo(0)), "CRITIC") > 0)) Then
                                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                                    ElseIf UCase(FlagInfo(0)).ToString.StartsWith("LOW") Or (UCase(FlagInfo(0)).ToString.StartsWith("L ") And
                                                    InStr(UCase(FlagInfo(0)), "LOW") > 0) Then
                                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                    ElseIf (UCase(FlagInfo(0)).ToString.StartsWith("N") And InStr(UCase(FlagInfo(0)), "NORMAL") > 0) Or
                                                    UCase(FlagInfo(0)).ToString.StartsWith("NEG") Or (UCase(FlagInfo(0)).ToString.StartsWith("NON") And
                                                    InStr(UCase(FlagInfo(0)), "REACT") > 0) Or UCase(FlagInfo(0)).ToString.StartsWith("SENSIT") Or
                                                    UCase(FlagInfo(0)).ToString.StartsWith("SUSCEP") Then
                                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Ignore"
                                                    ElseIf UCase(FlagInfo(0)).ToString.StartsWith("AB") Or (UCase(FlagInfo(0)).ToString.StartsWith("A") And
                                                    InStr(UCase(FlagInfo(0)), "ABNORMAL") > 0) Or UCase(FlagInfo(0)).ToString.StartsWith("REAC") Or
                                                    UCase(FlagInfo(0)).ToString.StartsWith("POS") Or UCase(FlagInfo(0)).StartsWith("MEDIUM POS") Or
                                                    UCase(FlagInfo(0)).StartsWith("HIGH POS") Or UCase(FlagInfo(0)).StartsWith("LOW POS") Or
                                                    UCase(FlagInfo(0)).ToString.StartsWith("DETEC") Or UCase(FlagInfo(0)).ToString.StartsWith("RESIST") Or
                                                    (UCase(FlagInfo(0)).ToString.StartsWith("NON") And (InStr(UCase(FlagInfo(0)), "NEG") > 0 Or
                                                    InStr(UCase(FlagInfo(0)), "POS") > 0)) Then
                                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                    ElseIf UCase(FlagInfo(0)).ToString.StartsWith("HP") Or UCase(FlagInfo(0)).ToString.StartsWith("HIGH PANIC") Or
                                                    UCase(FlagInfo(0)).ToString.StartsWith("HIGH CRITIC") Then
                                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Panic"
                                                    ElseIf UCase(FlagInfo(0)).ToString.StartsWith("HIGH") Or (UCase(FlagInfo(0)).ToString.StartsWith("H") And
                                                    InStr(UCase(FlagInfo(0)), "HIGH") > 0) Then
                                                        If FlagInfo(1) = "" Then FlagInfo(1) = "Caution"
                                                    End If
                                                End If
                                                Exit While
                                            End If
                                        End If
                                    End If
                                End If
                            End While
                        End If
                        cnnr.Close()
                        cnnr = Nothing
                    End If
                End If
            End If
        End If
        Return FlagInfo
    End Function

    Public Function GetTestIDsfromReqTGPS(ByVal AccID As Long) As String()
        Dim TGPS() As String = {""}
        Dim TestIDs() As String = {""}
        Dim sSQL As String = "Select * from Req_TGP where Accession_ID = " & AccID & " order by Ordinal"
        Dim cntgp As New SqlConnection(connString)
        cntgp.Open()
        Dim cmdtgp As New SqlCommand(sSQL, cntgp)
        cmdtgp.CommandType = CommandType.Text
        Dim drtgp As SqlDataReader = cmdtgp.ExecuteReader
        If drtgp.HasRows Then
            While drtgp.Read
                If TGPS(UBound(TGPS)) <> "" Then ReDim Preserve TGPS(UBound(TGPS) + 1)
                TGPS(UBound(TGPS)) = drtgp("TGP_ID").ToString
            End While
        End If
        cntgp.Close()
        cntgp = Nothing
        '
        For i As Integer = 0 To TGPS.Length - 1
            If TGPS(i) <> "" Then
                If GetTGPType(TGPS(i)) = "P" Then   'P
                    sSQL = "Select c.Test_ID as TestID from (Group_Test c inner join Tests d on " &
                    "c.Test_ID = d.ID) inner join Prof_GrpTst e on e.GrpTst_ID = c.Group_ID where " &
                    "d.IsActive <> 0 and d.HasResult <> 0 and e.GrpTst_ID in (Select ID from Groups " &
                    "where IsActive <> 0) and e.Profile_ID = " & Val(TGPS(i)) & " order by e.Ordinal, c.Ordinal"
                    Dim cn1 As New SqlConnection(connString)
                    cn1.Open()
                    Dim cmd1 As New SqlCommand(sSQL, cn1)
                    cmd1.CommandType = CommandType.Text
                    Dim dr1 As SqlDataReader = cmd1.ExecuteReader
                    If dr1.HasRows Then
                        While dr1.Read
                            If Not TESTinTESTS(dr1("TestID"), TestIDs) Then
                                TestIDs = AddTESTinTESTS(dr1("TestID"), TestIDs)
                            End If
                        End While
                    End If
                    cn1.Close()
                    cn1 = Nothing
                    '
                    sSQL = "Select a.GrpTst_ID as TestID from Prof_GrpTst a inner join Tests b on b.ID = a.GrpTst_ID " &
                    "where a.Profile_ID = " & Val(TGPS(i)) & " and b.IsActive <> 0 and b.HasResult <> 0 order by a.ordinal"
                    Dim cn2 As New SqlConnection(connString)
                    cn2.Open()
                    Dim cmd2 As New SqlCommand(sSQL, cn2)
                    cmd2.CommandType = CommandType.Text
                    Dim dr2 As SqlDataReader = cmd2.ExecuteReader
                    If dr2.HasRows Then
                        While dr2.Read
                            If Not TESTinTESTS(dr2("TestID"), TestIDs) Then
                                TestIDs = AddTESTinTESTS(dr2("TestID"), TestIDs)
                            End If
                        End While
                    End If
                    cn2.Close()
                    cn2 = Nothing
                ElseIf GetTGPType(TGPS(i)) = "G" Then   'G
                    sSQL = "Select a.Test_ID as TestID from Group_Test a inner join Tests b on b.ID = a.Test_ID " &
                    "where b.IsActive <> 0 and b.HasResult <> 0 and a.Group_ID = " & TGPS(i) & " order by a.ordinal"
                    Dim cn3 As New SqlConnection(connString)
                    cn3.Open()
                    Dim cmd3 As New SqlCommand(sSQL, cn3)
                    cmd3.CommandType = CommandType.Text
                    Dim dr3 As SqlDataReader = cmd3.ExecuteReader
                    If dr3.HasRows Then
                        While dr3.Read
                            If Not TESTinTESTS(dr3("TestID").ToString, TestIDs) Then
                                TestIDs = AddTESTinTESTS(dr3("TestID"), TestIDs)
                            End If
                        End While
                    End If
                    cn3.Close()
                    cn3 = Nothing
                Else    'T
                    sSQL = "Select ID from Tests where IsActive <> 0 and HasResult <> 0 and ID = " & TGPS(i)
                    Dim cnt As New SqlConnection(connString)
                    cnt.Open()
                    Dim tcmd As New SqlCommand(sSQL, cnt)
                    tcmd.CommandType = CommandType.Text
                    Dim tDR As SqlDataReader = tcmd.ExecuteReader
                    If tDR.HasRows Then
                        While tDR.Read
                            If Not TESTinTESTS(tDR("ID").ToString, TestIDs) Then
                                TestIDs = AddTESTinTESTS(tDR("ID").ToString, TestIDs)
                            End If
                        End While
                    End If
                    tDR.Close()
                    tcmd.Dispose()
                    cnt.Close()
                    cnt = Nothing
                End If
            End If
        Next
        TestIDs = RemoveDuplicates(TestIDs)
        '
        Return TestIDs
    End Function

    Public Function MedsInAccession(ByVal AccID As Long) As Boolean
        Dim MIA As Boolean = False
        Dim sSQL As String = "Select Medication from Req_Med where Accession_ID = " & AccID
        Dim cnma As New SqlConnection(connString)
        cnma.Open()
        Dim cmdma As New SqlCommand(sSQL, cnma)
        cmdma.CommandType = CommandType.Text
        Dim drma As SqlDataReader = cmdma.ExecuteReader
        If drma.HasRows Then MIA = True
        cnma.Close()
        cnma = Nothing
        Return MIA
    End Function

    Public Function TestInMeds(ByVal TestID As Integer, ByVal AccID As Long) As Boolean
        Dim TiM As Boolean = False
        Dim i As Integer
        Dim TGPS() As String = GetTOXTGPNames(TestID)
        Dim sSQL As String = "Select Medication from Req_Med where Accession_ID = " & AccID
        Dim cntim As New SqlConnection(connString)
        cntim.Open()
        Dim cmdtim As New SqlCommand(sSQL, cntim)
        cmdtim.CommandType = CommandType.Text
        Dim drtim As SqlDataReader = cmdtim.ExecuteReader
        If drtim.HasRows Then
            While drtim.Read
                If TGPS(0) <> "" Then
                    For i = 0 To TGPS.Length - 1
                        If UCase(TGPS(i)) = UCase(drtim("Medication")) Then
                            TiM = True
                            Exit While
                        End If
                    Next
                End If
            End While
        End If
        cntim.Close()
        cntim = Nothing
        Return TiM
    End Function

    Private Function OptimizeResult(ByVal Result As String, ByVal PTR As String) As String
        Dim Fac As Single
        If PTR = "0" Then
            Fac = 1
        ElseIf PTR = "0.0" Then
            Fac = 1 / 10
        ElseIf PTR = "0.000" Then
            Fac = 1 / 1000
        ElseIf PTR = "0.0000" Then
            Fac = 1 / 10000
        Else
            Fac = 1 / 100
        End If
        Dim NumRes As String = Result
        NumRes = Replace(NumRes, "<", "") : NumRes = Replace(NumRes, ">", "")
        NumRes = Replace(NumRes, " ", "")
        If IsNumeric(NumRes) Then
            If InStr(Result, "<") > 0 Then
                Result = Format(CSng(NumRes) - Fac, PTR)
            ElseIf InStr(Result, ">") > 0 Then
                Result = Format(CSng(NumRes) + Fac, PTR)
            End If
        Else
            Result = Trim(Result)
        End If
        Return Result
    End Function

    Public Function GetOrdProvIDFromAcc(ByVal AccID As Long) As Long
        Dim ProvID As Long = -1
        Dim sSQL As String = "Select OrderingProvider_ID from Requisitions where ID = " & AccID
        Dim cngpr As New SqlConnection(connString)
        cngpr.Open()
        Dim cmdgpr As New SqlCommand(sSQL, cngpr)
        cmdgpr.CommandType = CommandType.Text
        Try
            Dim drgpr As SqlDataReader = cmdgpr.ExecuteReader
            If drgpr.HasRows Then
                While drgpr.Read
                    ProvID = drgpr("OrderingProvider_ID")
                End While
            End If
        Catch ex As Exception
            'SendMail("QCPROCS", "GetOrdProvIDFromAcc", ex.Message)
        Finally
            cngpr.Close()
            cngpr = Nothing
        End Try
        Return ProvID
    End Function

    Public Function GetNormalRange(ByVal AccID As Long, ByVal TestID As Integer) As String
        Dim AgeSex() As String = GetAgeSex(AccID)
        Dim ProviderID As Long = GetOrdProvIDFromAcc(AccID)
        Dim NormalRange As String = ""
        Dim Qualitative As Boolean = False
        Dim AG As Boolean = False
        Dim NorC As Boolean = False
        Dim Decs As Integer = 2
        Dim DSTR As String = "0.00"
        '
        Dim cnt As New SqlConnection(connString)
        cnt.Open()
        Dim tcmd As New SqlCommand("Select * from Tests where IsActive <> 0 and ID = " & TestID, cnt)
        tcmd.CommandType = CommandType.Text
        Try
            Dim tDR As SqlDataReader = tcmd.ExecuteReader
            If tDR.HasRows Then
                While tDR.Read
                    If tDR("Qualitative") IsNot Nothing AndAlso tDR("Qualitative") _
                    IsNot DBNull.Value Then Qualitative = tDR("Qualitative")
                    If tDR("AGRanges") IsNot Nothing AndAlso tDR("AGRanges") _
                    IsNot DBNull.Value Then AG = tDR("AGRanges")
                    If tDR("DecimalPlaces") IsNot Nothing AndAlso tDR("DecimalPlaces") _
                    IsNot DBNull.Value Then Decs = tDR("DecimalPlaces")
                    If tDR("NormCut") IsNot Nothing AndAlso tDR("NormCut") _
                    IsNot DBNull.Value Then NorC = tDR("NormCut")
                End While
            End If
            tDR.Close()
        Catch ex As Exception
            'SendMail("QCPROCS", "GetNormalRange1", ex.Message)
        Finally
            tcmd.Dispose()
            cnt.Close()
            cnt = Nothing
        End Try
        '
        If Decs = 0 Then
            DSTR = "0"
        ElseIf Decs = 1 Then
            DSTR = "0.0"
        ElseIf Decs = 3 Then
            DSTR = "0.000"
        ElseIf Decs = 4 Then
            DSTR = "0.0000"
        Else
            DSTR = "0.00"
        End If
        '
        Dim Digs As String
        If Decs = 1 Then
            Digs = "0.0"
        ElseIf Decs = 2 Then
            Digs = "0.00"
        ElseIf Decs = 3 Then
            Digs = "0.000"
        ElseIf Decs = 4 Then
            Digs = "0.0000"
        Else
            Digs = "0"
        End If
        If SystemConfig.DiagTarget <> "V" Then  'Human
            If ProviderID <> -1 Then    'Provider specific ranges for QL and QN
                Dim cnp As New SqlConnection(connString)
                cnp.Open()
                Dim pcmd As New SqlCommand("Select * from Provider_Ranges " &
                "where Provider_ID = " & ProviderID & " and Test_ID = " & TestID, cnp)
                pcmd.CommandType = CommandType.Text
                Try
                    Dim pDR As SqlDataReader = pcmd.ExecuteReader
                    If pDR.HasRows Then
                        While pDR.Read
                            If (pDR("AgeFrom") IsNot DBNull.Value And pDR("AgeTo") IsNot DBNull.Value And pDR("Sex") _
                            IsNot DBNull.Value And pDR("Flag") IsNot DBNull.Value) AndAlso (Val(AgeSex(0)) >=
                            pDR("AgeFrom") And Val(AgeSex(0)) <= pDR("AgeTo") And AgeSex(1) = pDR("Sex")) Then
                                If Qualitative = False Then 'QN
                                    If NorC = False Then    'Normal
                                        If pDR("Behavior") = "Ignore" Then
                                            NormalRange = Format(Val(pDR("ValueFrom")), DSTR) _
                                            & " - " & Format(Val(pDR("ValueTo")), DSTR)
                                            Exit While
                                        End If
                                    Else    'CotOff
                                        If pDR("Behavior") <> "Ignore" Then
                                            NormalRange = Format(Val(pDR("ValueFrom")), DSTR)
                                            Exit While
                                        End If
                                    End If
                                Else
                                    If pDR("Behavior") = "Ignore" Then
                                        NormalRange = Trim(pDR("Flag"))
                                        Exit While
                                    End If
                                End If
                            End If
                        End While
                    End If
                    pDR.Close()
                Catch ex As Exception
                    'SendMail("QCPROCS", "GetNormalRange2", ex.Message)
                Finally
                    tcmd.Dispose()
                    cnp.Close()
                    cnp = Nothing
                End Try
            End If      'end of Provider ranges
            '
            If NormalRange = "" Then    'system ranges
                If Qualitative = True Then
                    Dim cncr As New SqlConnection(connString)
                    cncr.Open()
                    Dim crcmd As New SqlCommand("Select * from C_Ranges where Test_ID = " & TestID, cncr)
                    crcmd.CommandType = CommandType.Text
                    Try
                        Dim crDR As SqlDataReader = crcmd.ExecuteReader
                        If crDR.HasRows Then
                            While crDR.Read
                                If NorC = False Then    'Normal
                                    If crDR("Flag") IsNot DBNull.Value AndAlso
                                    Trim(crDR("Flag")) <> "" AndAlso
                                    Trim(crDR("Behavior")) = "Ignore" Then
                                        NormalRange = Trim(crDR("Choice"))
                                    End If
                                Else    'Interp
                                    If crDR("Flag") IsNot DBNull.Value AndAlso
                                    Trim(crDR("Flag")) <> "" AndAlso
                                    Trim(crDR("Behavior")) <> "Ignore" Then
                                        NormalRange = Trim(crDR("Choice"))
                                    End If
                                End If
                                If NormalRange <> "" Then Exit While
                            End While
                        End If
                        crDR.Close()
                    Catch ex As Exception
                        'SendMail("QCPROCS", "GetNormalRange3", ex.Message)
                    Finally
                        crcmd.Dispose()
                        cncr.Close()
                        cncr = Nothing
                    End Try
                Else    'QN (AG or N)
                    If AG = True Then   'AG_Range
                        Dim cnag As New SqlConnection(connString)
                        cnag.Open()
                        Dim agcmd As New SqlCommand("Select * from AG_Ranges where Test_ID = " & TestID, cnag)
                        agcmd.CommandType = CommandType.Text
                        Try
                            Dim agDR As SqlDataReader = agcmd.ExecuteReader
                            If agDR.HasRows Then
                                While agDR.Read
                                    If NorC = False Then    'Normal
                                        If Val(AgeSex(0)) >= agDR("AgeFrom") And Val(AgeSex(0)) <=
                                        agDR("AgeTo") And AgeSex(1) = agDR("Sex") _
                                        And (agDR("Flag") IsNot DBNull.Value AndAlso
                                        Trim(agDR("Flag")) <> "" AndAlso
                                        Trim(agDR("Behavior")) = "Ignore") Then
                                            If IsNumeric(agDR("ValueFrom")) Then
                                                NormalRange = Format(agDR("ValueFrom"), Digs) &
                                                " - " & Format(agDR("ValueTo"), Digs)
                                            Else
                                                NormalRange = Trim(agDR("ValueFrom")) & " " &
                                                Format(agDR("ValueTo"), Digs)
                                            End If
                                            Exit While
                                        End If
                                    Else
                                        If Val(AgeSex(0)) >= agDR("AgeFrom") And Val(AgeSex(0)) <=
                                        agDR("AgeTo") And AgeSex(1) = agDR("Sex") Then
                                            If agDR("Flag") IsNot DBNull.Value AndAlso
                                            Trim(agDR("Flag")) <> "" AndAlso
                                            Trim(agDR("Behavior")) = "Caution" Then
                                                If IsNumeric(agDR("ValueFrom")) Then
                                                    NormalRange = Format(agDR("ValueFrom"), Digs)
                                                Else
                                                    NormalRange = Trim(agDR("ValueFrom"))
                                                End If
                                                Exit While
                                            End If
                                        End If
                                    End If
                                End While
                            End If
                            agDR.Close()
                        Catch ex As Exception
                            SendMail("QCPROCS", "GetNormalRange4", ex.Message)
                        Finally
                            agcmd.Dispose()
                            cnag.Close()
                            cnag = Nothing
                        End Try
                    Else    'N_Range
                        Dim cnn As New SqlConnection(connString)
                        cnn.Open()
                        Dim ncmd As New SqlCommand("Select * from N_Ranges where Test_ID = " & TestID, cnn)
                        ncmd.CommandType = CommandType.Text
                        Try
                            Dim nDR As SqlDataReader = ncmd.ExecuteReader
                            If nDR.HasRows Then
                                While nDR.Read
                                    If NorC = False Then    'Normal
                                        If nDR("Flag") IsNot DBNull.Value AndAlso
                                        Trim(nDR("Flag")) <> "" AndAlso
                                        Trim(nDR("Behavior")) = "Ignore" Then
                                            If IsNumeric(nDR("ValueFrom")) Then
                                                NormalRange = Format(nDR("ValueFrom"), Digs) &
                                                " - " & Format(nDR("ValueTo"), Digs)
                                            Else
                                                NormalRange = Trim(nDR("ValueFrom")) & " " &
                                                Format(nDR("ValueTo"), Digs)
                                            End If
                                            Exit While
                                        End If
                                    Else                    'Cutof
                                        If nDR("Flag") IsNot DBNull.Value AndAlso
                                        Trim(nDR("Flag")) <> "" AndAlso
                                        Trim(nDR("Behavior")) = "Caution" Then
                                            If IsNumeric(nDR("ValueFrom")) Then
                                                NormalRange = Format(nDR("ValueFrom"), Digs)
                                            Else
                                                NormalRange = Trim(nDR("ValueFrom"))
                                            End If
                                            Exit While
                                        End If
                                    End If
                                End While
                            End If
                            nDR.Close()
                        Catch ex As Exception
                            'SendMail("QCPROCS", "GetNormalRange5", ex.Message)
                        Finally
                            ncmd.Dispose()
                            cnn.Close()
                            cnn = Nothing
                        End Try
                    End If
                End If
            End If      'end of system ranges of sql proc
        Else    'Veterinary
            Dim SID As Integer = GetAccessionSpeciesID(AccID)
            Dim cnv As New SqlConnection(connString)
            cnv.Open()
            Dim cmdv As New SqlCommand("Select * from S_Ranges " &
            "where Species_ID = " & SID & " and Test_ID = " & TestID, cnv)
            cmdv.CommandType = CommandType.Text
            Try
                Dim drv As SqlDataReader = cmdv.ExecuteReader
                If drv.HasRows Then
                    While drv.Read
                        If Qualitative = True Then  'QL
                            If NorC = False Then    'Normal
                                If drv("Flag") IsNot DBNull.Value AndAlso
                                Trim(drv("Flag")) <> "" AndAlso
                                Trim(drv("Behavior")) = "Ignore" Then
                                    NormalRange = Trim(drv("Choice"))
                                End If
                            Else    'Interp
                                If drv("Flag") IsNot DBNull.Value AndAlso
                                Trim(drv("Flag")) <> "" AndAlso
                                Trim(drv("Behavior")) <> "Ignore" Then
                                    NormalRange = Trim(drv("Choice"))
                                End If
                            End If
                            If NormalRange <> "" Then Exit While
                        Else
                            If NorC = False Then    'Normal
                                If drv("Flag") IsNot DBNull.Value AndAlso
                                Trim(drv("Flag")) <> "" AndAlso
                                Trim(drv("Behavior")) = "Ignore" Then
                                    If IsNumeric(drv("ValueFrom")) Then
                                        NormalRange = Format(Val(drv("ValueFrom")), Digs) &
                                        " - " & Format(Val(drv("ValueTo")), Digs)
                                    Else
                                        NormalRange = Trim(drv("ValueFrom"))
                                    End If
                                    Exit While
                                End If
                            Else                    'Cutof
                                If drv("Flag") IsNot DBNull.Value AndAlso
                                Trim(drv("Flag")) <> "" AndAlso
                                Trim(drv("Behavior")) = "Caution" Then
                                    If IsNumeric(drv("ValueFrom")) Then
                                        NormalRange = Format(Val(drv("ValueFrom")), Digs)
                                    Else
                                        NormalRange = Trim(drv("ValueFrom"))
                                    End If
                                    Exit While
                                End If
                            End If
                        End If
                    End While
                End If
                drv.Close()
            Catch ex As Exception
                'SendMail("QCPROCS", "GetNormalRange5", ex.Message)
            Finally
                cnv.Close()
                cnv = Nothing
            End Try
        End If
        '
        Return NormalRange
    End Function

    Private Function GetAccessionSpeciesID(ByVal AccID As Long) As Integer
        Dim SPID As Integer = 0
        Dim sSQL As String = "Select Species_ID from Patients where ID in " &
        "(Select Patient_ID from Requisitions where ID = " & AccID & ")"
        Dim cnsid As New SqlConnection(connString)
        cnsid.Open()
        Dim cmdsid As New SqlCommand(sSQL, cnsid)
        cmdsid.CommandType = CommandType.Text
        Dim drsid As SqlDataReader = cmdsid.ExecuteReader
        If drsid.HasRows Then
            While drsid.Read
                SPID = drsid("Species_ID")
            End While
        End If
        cnsid.Close()
        cnsid = Nothing
        Return SPID
    End Function

    Public Function HasPreanas(ByVal TGPID As String) As Boolean
        Dim Has As Boolean = False
        Dim sSQL As String = "Select * from TGP_Info a inner join Tests b on a.Info_ID = b.ID where b.IsActive <> 0 " &
        "and b.HasResult <> 0 and b.Preanalytical <> 0 and a.TGP_ID = " & TGPID & " Union Select * from TGP_Info a " &
        "inner join Tests b on a.Info_ID = b.ID where b.IsActive <> 0 and b.HasResult <> 0 and b.Preanalytical <> 0 " &
        "and a.TGP_ID in (Select Test_ID from Group_Test where Group_ID = " & TGPID & ") Union Select * from TGP_Info " &
        "a inner join Tests b on a.Info_ID = b.ID where b.IsActive <> 0 and b.HasResult <> 0 and b.Preanalytical <> 0 " &
        "and a.TGP_ID in (Select GrpTst_ID from Prof_GrpTst where Profile_ID = " & TGPID & ") Union Select * from TGP_Info " &
        "a inner join Tests b on a.Info_ID = b.ID where b.IsActive <> 0 and b.HasResult <> 0 and b.Preanalytical <> 0 and " &
        "a.TGP_ID in (Select Test_ID from Group_Test where Group_ID in (Select GrpTst_ID from Prof_GrpTst where Profile_ID = " & TGPID & "))"
        '
        Dim cnhp As New SqlConnection(connString)
        cnhp.Open()
        Dim cmdhp As New SqlCommand(sSQL, cnhp)
        cmdhp.CommandType = CommandType.Text
        Try
            Dim drhp As SqlDataReader = cmdhp.ExecuteReader
            If drhp.HasRows Then Has = True
        Catch ex As Exception
            'SendMail("QCPROCS", "GetAgeSex", ex.Message)
        Finally
            cnhp.Close()
            cnhp = Nothing
        End Try
        Return Has
    End Function

    Public Function HasAOE(ByVal TGPID As String) As Boolean
        Dim Has As Boolean = False
        Dim sSQL As String = $"select * from AOE_TGP_Questions where TGP_ID =  {TGPID}"
        '
        Dim cnhp As New SqlConnection(connString)
        cnhp.Open()
        Dim cmdhp As New SqlCommand(sSQL, cnhp)
        cmdhp.CommandType = CommandType.Text
        Try
            Dim drhp As SqlDataReader = cmdhp.ExecuteReader
            If drhp.HasRows Then Has = True
        Catch ex As Exception
            'SendMail("QCPROCS", "GetAgeSex", ex.Message)
        Finally
            cnhp.Close()
            cnhp = Nothing
        End Try
        Return Has
    End Function

    Public Function GetAgeSex(ByVal AccID As Long) As String()
        Dim AgeSex() As String = {"", ""}
        Dim sSQL As String = "Select a.AccessionDate as AccDate, b.DOB as DOB, b.Sex as Sex " &
        "from Requisitions a inner join Patients b on a.Patient_ID = b.ID where a.ID = " & AccID
        Dim cnn As New SqlConnection(connString)
        cnn.Open()
        Dim selcmd As New SqlCommand(sSQL, cnn)
        Try
            Dim selDR As SqlDataReader = selcmd.ExecuteReader
            If selDR.HasRows Then
                While selDR.Read
                    AgeSex(0) = CStr(CInt(DateDiff(DateInterval.Year, selDR("DOB"), selDR("AccDate"))))
                    AgeSex(1) = Microsoft.VisualBasic.Left(selDR("Sex"), 1)
                End While
            End If
            selDR.Close()
        Catch ex As Exception
            'SendMail("QCPROCS", "GetAgeSex", ex.Message)
        Finally
            selcmd.Dispose()
            cnn.Close()
            cnn = Nothing
        End Try
        Return AgeSex
    End Function

    Public Sub Write_Error_Log(ByVal ErrType As Integer, ByVal Msg As String)
        Try
            Dim NextID As Long = GetNextErrorID()
            ExecuteSqlProcedure("Insert into Error_Log (Error_ID, Error_Type_ID, " &
            "Error_Time, Error_Msg) values (" & NextID & ", " & ErrType & ", '" &
            Date.Now & "', '" & Microsoft.VisualBasic.Left(Msg, 960) & "')")
        Catch Ex As Exception
            WriteLog("Error: " & Ex.Message & vbCrLf & "while writing the Error Type = " & ErrType &
            "; " & Msg)
        End Try
    End Sub

    Private Function GetNextErrorID() As Long
        Dim NID As Long = 1
        Dim cncid As New SqlConnection(connString)
        cncid.Open()
        Dim cmdcid As New SqlCommand("Select " &
        "Max(Error_ID) as lastid from Error_Log", cncid)
        cmdcid.CommandType = CommandType.Text
        Dim drcid As SqlDataReader = cmdcid.ExecuteReader
        If drcid.HasRows Then
            While drcid.Read
                If drcid("LastID") IsNot DBNull.Value _
                Then NID = drcid("LastID") + 1
            End While
        End If
        cncid.Close()
        cncid = Nothing
        Return NID
    End Function

    Public Function GetTGPTests(ByVal TGPID As Integer) As String()
        Dim Tests() As String = {""}
        Dim sSQL As String = ""
        Dim TGPType As String = GetTGPType(TGPID)
        If TGPType = "T" Then
            Tests(0) = TGPID.ToString
        ElseIf TGPType = "G" Then
            sSQL = "Select Test_ID from Group_Test where Group_ID = " & TGPID & " Order by Ordinal"
            '
            Dim cnggt As New SqlConnection(connString)
            cnggt.Open()
            Dim cmdggt As New SqlCommand(sSQL, cnggt)
            cmdggt.CommandType = CommandType.Text
            Dim drggt As SqlDataReader = cmdggt.ExecuteReader
            If drggt.HasRows Then
                While drggt.Read
                    If Tests(UBound(Tests)) <> "" Then ReDim Preserve Tests(UBound(Tests) + 1)
                    Tests(UBound(Tests)) = drggt("Test_ID").ToString
                End While
            End If
            cnggt.Close()
            cnggt = Nothing
        ElseIf TGPType = "P" Then    'Profile
            Dim Ord As Integer = 0
            sSQL = "Select GrpTst_ID from Prof_GrpTst where Profile_ID = " & TGPID & " Order by Ordinal"
            '
            Dim cnpgt As New SqlConnection(connString)
            cnpgt.Open()
            Dim cmdpgt As New SqlCommand(sSQL, cnpgt)
            cmdpgt.CommandType = CommandType.Text
            Dim drpgt As SqlDataReader = cmdpgt.ExecuteReader
            If drpgt.HasRows Then
                While drpgt.Read
                    If GetTGPType(drpgt("GrpTst_ID")) = "T" Then
                        If Tests(UBound(Tests)) <> "" Then ReDim Preserve Tests(UBound(Tests) + 1)
                        Tests(UBound(Tests)) = drpgt("GrpTst_ID").ToString
                    Else
                        sSQL = "Select Test_ID from Group_Test where Group_ID = " & drpgt("GrpTst_ID") & " Order by Ordinal"
                        Dim cngt As New SqlConnection(connString)
                        cngt.Open()
                        Dim cmdgt As New SqlCommand(sSQL, cngt)
                        cmdgt.CommandType = CommandType.Text
                        Dim drgt As SqlDataReader = cmdgt.ExecuteReader
                        If drgt.HasRows Then
                            While drgt.Read
                                If Tests(UBound(Tests)) <> "" Then ReDim Preserve Tests(UBound(Tests) + 1)
                                Tests(UBound(Tests)) = drgt("Test_ID").ToString
                            End While
                        End If
                        cngt.Close()
                        cngt = Nothing
                    End If
                End While
            End If
            cnpgt.Close()
            cnpgt = Nothing
        End If
        Return Tests
    End Function

    Public Sub ProcessConditionalTriggers(ByVal AccID As Long,
    ByVal ReflexerID As Integer, ByVal condition As String)
        Dim TGPType As String = ""
        Dim GorT As String = ""
        Dim Ord As Integer = 0
        Dim MarkedID As String = ""
        Dim sSQL As String = "Select Marked_ID from Conditional_Triggers where Condition = '" & condition & "' and Test_ID = " & ReflexerID
        '
        Dim cnpt1 As New SqlConnection(connString)
        cnpt1.Open()
        Dim cmdpt1 As New SqlCommand(sSQL, cnpt1)
        cmdpt1.CommandType = CommandType.Text
        Dim drpt1 As SqlDataReader = cmdpt1.ExecuteReader
        If drpt1.HasRows Then
            While drpt1.Read
                MarkedID = drpt1("Marked_ID").ToString
            End While
        End If
        cnpt1.Close()
        cnpt1 = Nothing
        '
        If MarkedID <> "" Then
            Dim Tests() As String = GetTGPTests(Val(MarkedID))
            For i As Integer = 0 To Tests.Length - 1
                Dim cnpt2 As New SqlConnection(connString)
                cnpt2.Open()
                Dim cmdpt2 As New SqlCommand("Ref_Results_SP", cnpt2)
                cmdpt2.CommandType = CommandType.StoredProcedure
                cmdpt2.Parameters.AddWithValue("@act", "upsert")
                cmdpt2.Parameters.AddWithValue("@Accession_ID", AccID)
                cmdpt2.Parameters.AddWithValue("@Reflexer_ID", ReflexerID)
                cmdpt2.Parameters.AddWithValue("@Reflexed_ID", Val(MarkedID))
                cmdpt2.Parameters.AddWithValue("@Test_ID", Val(MarkedID))
                cmdpt2.Parameters.AddWithValue("@Ordinal", Ord)
                cmdpt2.Parameters.AddWithValue("@Flag", "")
                cmdpt2.Parameters.AddWithValue("@NormalRange", GetNormalRange(AccID, Val(MarkedID)))
                cmdpt2.ExecuteNonQuery()
                cnpt2.Close()
                cnpt2 = Nothing
                Ord += 1
            Next
        Else    'No condition
            ExecuteSqlProcedure("Delete from Ref_Results where Accession_ID = " & AccID & " and " &
            "Reflexer_ID = " & ReflexerID & " and Reflexed_ID in (Select a.Marked_ID from " &
            "Conditional_Triggers a inner join Acc_Results b on a.Test_ID = b.Test_ID and " &
            "a.Condition = b.Flag where b.Accession_ID = " & AccID & ")")
        End If
    End Sub

    Public Function UserPermittedReport(ByVal Rpt As String, ByVal UserID As Long) As Boolean
        Dim Permitted As Boolean = False
        Dim sSQL As String = "Select * from Report_User where Report_File = '" & Rpt & "' and User_ID = " & UserID
        '
        Dim cnpr As New SqlConnection(connString)
        cnpr.Open()
        Dim cmdpr As New SqlCommand(sSQL, cnpr)
        cmdpr.CommandType = CommandType.Text
        Dim DRpr As SqlDataReader = cmdpr.ExecuteReader
        If DRpr.HasRows Then Permitted = True
        cnpr.Close()
        cnpr = Nothing
        Return Permitted
    End Function

    Public Function ReportFullResulted(ByVal AccID As Long) As Boolean
        Dim Resulted As Boolean = False
        Dim sSQL As String = "Select Accession_ID from Acc_Results where (Released is Null or Released = 0) and " &
        "Accession_ID = " & AccID & " Union Select Accession_ID from Acc_Info_Results where (Released is Null " &
        "or Released = 0) and Accession_ID = " & AccID & " Union Select Accession_ID from Ref_Results where " &
        "(Released is Null or Released = 0) and Accession_ID = " & AccID & "  and not Accession_ID  in (
		   Select Accession_ID from Ref_Results where
		   Result is null and Interpretation is null and Flag is null and Behavior is null and T_Result is null
		   and Release_Time is null and Comment is null and
		   Accession_ID = " & AccID & "
		   ) "
        '
        Dim cnr As New SqlConnection(connString)
        cnr.Open()
        Dim cmdr As New SqlCommand(sSQL, cnr)
        cmdr.CommandType = CommandType.Text
        Try
            Dim DRr As SqlDataReader = cmdr.ExecuteReader
            If Not DRr.HasRows Then Resulted = True
            DRr.Close()
        Catch ex As Exception
            'SendMail("QCPROCS", "GetAgeSex", ex.Message)
        Finally
            cnr.Close()
            cnr = Nothing
        End Try
        Return Resulted
    End Function

    Public Function ReportPartialResulted(ByVal AccID As Long) As Boolean
        Dim HasAnynotresulted As Boolean = False
        Dim HasAnyresulted As Boolean = False
        Dim Resulted As Boolean = False
        Dim sSQL1HasAnyReleased As String = "Select Accession_ID from Acc_Results where (Released = 1) and " &
        "Accession_ID = " & AccID & " Union Select Accession_ID from Acc_Info_Results where ( " &
        "  Released = 1) and Accession_ID = " & AccID & " Union Select Accession_ID from Ref_Results where " &
        "(  Released = 1) and Accession_ID = " & AccID

        Dim sSQL2HasAnyNotReleased As String = "Select Accession_ID from Acc_Results where (Released is Null or Released = 0) and " &
       "Accession_ID = " & AccID & " Union Select Accession_ID from Acc_Info_Results where (Released is Null " &
       "or Released = 0) and Accession_ID = " & AccID & " Union Select Accession_ID from Ref_Results where " &
       "(Released is Null or Released = 0) and Accession_ID = " & AccID
        '
        Dim cnr As New SqlConnection(connString)
        cnr.Open()

        Try
            Dim cmdr As New SqlCommand(sSQL2HasAnyNotReleased, cnr)
            cmdr.CommandType = CommandType.Text
            Dim DRr As SqlDataReader = cmdr.ExecuteReader
            If DRr.HasRows Then HasAnynotresulted = True
            DRr.Close()

            Dim cmdr1 As New SqlCommand(sSQL1HasAnyReleased, cnr)
            cmdr1.CommandType = CommandType.Text
            Dim DRr1 As SqlDataReader = cmdr1.ExecuteReader
            If DRr1.HasRows Then HasAnyresulted = True
            DRr1.Close()
        Catch ex As Exception
            SendMail("QCPROCS", "ReportPartialResulted", ex.Message)
        Finally
            cnr.Close()
            cnr = Nothing
        End Try
        If HasAnynotresulted AndAlso HasAnyresulted Then
            Resulted = True
        End If
        Return Resulted
    End Function

    Public Function GetOrdAtndSetting(ByVal OrdererID As Long, ByVal AttenderID As Long) As String()
        Dim RptSetting() As String = {""}
        Dim sSQL As String = "Select * from Providers where ID = " & OrdererID
        '
        Dim cnoa As New SqlConnection(connString)
        cnoa.Open()
        Dim cmdoa As New SqlCommand(sSQL, cnoa)
        Dim droa As SqlDataReader = cmdoa.ExecuteReader
        If droa.HasRows Then
            While droa.Read
                If RptSetting(UBound(RptSetting)).ToString <> "" Then _
                ReDim Preserve RptSetting(UBound(RptSetting) + 1)
                RptSetting(UBound(RptSetting)) = GetReportSetting(OrdererID)
                If droa("SetExtend") = True Then    'Get attender also
                    If OrdererID <> AttenderID Then  'different
                        If RptSetting(UBound(RptSetting)).ToString <> "" Then _
                        ReDim Preserve RptSetting(UBound(RptSetting) + 1)
                        RptSetting(UBound(RptSetting)) = GetReportSetting(AttenderID)
                    End If
                End If
            End While
        End If
        cnoa.Close()
        cnoa = Nothing
        Return RptSetting
    End Function

    Public Sub LogEvent(ByVal AccID As Long, ByVal EventID As Integer, ByVal ProviderID As Long,
    ByVal ObjStatus As String, ByVal IsAuto As Boolean, ByVal Source As String, ByVal Cmnt As String)
        Dim sSQL As String = "If Exists (Select * from Event_Capture where Accession_ID = " & AccID &
        " and Provider_ID = " & ProviderID & " and Event_ID = " & EventID & ") Update Event_Capture " &
        "Set Event_Date = '" & Date.Now & "', Object_Status = '" & ObjStatus & "', Event_Source = '" &
        Trim(Source) & "', Auto_Event = '" & IsAuto & "', Comment = '" & Cmnt & "' where Accession_ID = " &
        AccID & " and Provider_ID = " & ProviderID & " and Event_ID = " & EventID & " Else Insert " &
        "into Event_Capture (Accession_ID, Event_Date, Provider_ID, Event_ID, Object_Status, " &
        "Event_Source, Auto_Event, Comment) values (" & AccID & ", '" & Date.Now & "', " & ProviderID &
        ", " & EventID & ", '" & ObjStatus & "', '" & Trim(Source) & "', '" & IsAuto & "', '" & Cmnt & "')"
        ExecuteSqlProcedure(sSQL)
    End Sub

    Private Function GetReportSetting(ByVal ProviderID As Long) As String
        Dim SB As New StringBuilder
        Dim mSett As String = ""
        Dim sSQL As String = "Select * from Providers where ID = " & ProviderID
        '
        Dim cnrs As New SqlConnection(connString)
        cnrs.Open()
        Dim cmdrs As New SqlCommand(sSQL, cnrs)
        Dim drrs As SqlDataReader = cmdrs.ExecuteReader
        If drrs.HasRows Then
            While drrs.Read
                SB.Append(drrs("ID").ToString & "|" & GetProviderName(drrs("ID")))
                If drrs("RDM_Auto") Is DBNull.Value Then
                    SB.Append("|RDM_Auto=False")
                Else
                    SB.Append("|RDM_Auto=" & CType(drrs("RDM_Auto"), Boolean).ToString)
                End If
                If drrs("RptComplete") Is DBNull.Value Then
                    SB.Append("|RptComplete=False")
                Else
                    SB.Append("|RptComplete=" & CType(drrs("RptComplete"), Boolean))
                End If
                If drrs("RDM_Print") Is DBNull.Value Then
                    SB.Append("|RDM_Print=False")
                Else
                    SB.Append("|RDM_Print=" & CType(drrs("RDM_Print"), Boolean))
                End If
                If drrs("RDM_Prolison") Is DBNull.Value Then
                    SB.Append("|RDM_Prolison=False")
                Else
                    SB.Append("|RDM_Prolison=" & CType(drrs("RDM_Prolison"), Boolean))
                End If
                If drrs("RDM_Interface") Is DBNull.Value Then
                    SB.Append("|RDM_Interface=False")
                Else
                    SB.Append("|RDM_Interface=" & CType(drrs("RDM_Interface"), Boolean))
                End If

                If drrs("Fax") IsNot DBNull.Value _
                AndAlso Trim(drrs("Fax")) <> "" Then
                    If drrs("RDM_Fax") = True Then
                        SB.Append("|RDM_Fax=True^" & drrs("Fax"))
                    Else
                        SB.Append("|RDM_Fax=False")
                    End If
                Else
                    SB.Append("|RDM_Fax=False")
                End If
                If drrs("Email") IsNot DBNull.Value _
                AndAlso Trim(drrs("Email")) <> "" Then
                    If drrs("RDM_Email") = True Then
                        SB.Append("|RDM_Email=True^" & Trim(drrs("Email")))
                    Else
                        SB.Append("|RDM_Email=False")
                    End If
                Else
                    SB.Append("|RDM_Email=False")
                End If
                'ProviderID, ProviderName, RptComplete, IsPrint, IsFax, Fax, IsEmail, Email, Comment
                mSett = SB.ToString
            End While
        End If
        cnrs.Close()
        cnrs = Nothing
        SB = Nothing
        Return mSett
    End Function

    Public Function GetOrdProvIDFromAccID(ByVal AccID As Long) As Long
        Dim ProvID As Long = -1
        Dim sSQL As String = "Select OrderingProvider_ID from Requisitions where ID = " & AccID
        '
        Dim cnopr As New SqlConnection(connString)
        cnopr.Open()
        Dim cmdopr As New SqlCommand(sSQL, cnopr)
        Dim dropr As SqlDataReader = cmdopr.ExecuteReader
        If dropr.HasRows Then
            While dropr.Read
                ProvID = dropr("OrderingProvider_ID")
            End While
        End If
        cnopr.Close()
        cnopr = Nothing
        Return ProvID
    End Function

    Public Function GetProviderName(ByVal ProviderID As Long) As String
        Dim Provider As String = ""
        Dim sSQL As String = "Select * from Providers where ID = " & ProviderID
        '
        Dim cnpn As New SqlConnection(connString)
        cnpn.Open()
        Dim cmdpn As New SqlCommand(sSQL, cnpn)
        Dim drpn As SqlDataReader = cmdpn.ExecuteReader
        If drpn.HasRows Then
            While drpn.Read
                If drpn("IsIndividual") IsNot DBNull.Value AndAlso drpn("IsIndividual") = False Then     'Entity
                    Provider = drpn("LastName_BSN")
                Else    'Individual
                    If drpn("Degree") IsNot DBNull.Value _
                    AndAlso drpn("Degree") <> "" Then
                        Provider = drpn("LastName_BSN") & ", " & drpn("FirstName") _
                        & " " & drpn("Degree")
                    Else
                        Provider = drpn("LastName_BSN") & ", " & drpn("FirstName")
                    End If
                End If
            End While
        End If
        cnpn.Close()
        cnpn = Nothing
        Return Provider
    End Function

    Public Sub CaptureEvent(ByVal AccID As Long, ByVal EventID As Integer, ByVal obj As _
    String, ByVal IsAuto As Boolean, ByVal Source As String, ByVal Comment As String)
        Dim sSQL As String = "Insert Into Event_Capture (Accession_ID, Event_Date, Event_ID, " &
        "Object_Status, Auto_Event, Event_Source, Comment) values (" & AccID & ", '" & Date.Now &
        "', " & EventID & ", '" & obj & "', " & IsAuto & ", '" & Source & "', '" & Comment & "')"
        ExecuteSqlProcedure(sSQL)
    End Sub

    Public Function UpdateDecs(ByVal TestID As Integer) As String
        Dim Decs As String = "0.00"
        Dim sSQL As String = "Select * from Tests where ID = " & TestID
        '
        Dim cnn As New SqlConnection(connString)
        cnn.Open()
        Dim cmdSel As New SqlCommand(sSQL, cnn)
        cmdSel.CommandType = CommandType.Text
        Dim DRsel As SqlDataReader = cmdSel.ExecuteReader
        If DRsel.HasRows Then
            While DRsel.Read
                If DRsel("DecimalPlaces") = 0 Then
                    Decs = "0"
                ElseIf DRsel("DecimalPlaces") = 1 Then
                    Decs = "0.0"
                ElseIf DRsel("DecimalPlaces") = 2 Then
                    Decs = "0.00"
                ElseIf DRsel("DecimalPlaces") = 3 Then
                    Decs = "0.000"
                ElseIf DRsel("DecimalPlaces") = 4 Then
                    Decs = "0.0000"
                Else
                    Decs = "0.00"
                End If
            End While
        End If
        cnn.Close()
        cnn = Nothing
        Return Decs
    End Function

    Public Function IsEmailValid(ByVal Email As String) As Boolean
        Dim IsValid As Boolean = True
        If InStr(Email, "@") = 0 Then IsValid = False
        If InStr(Email, ".") = 0 Then IsValid = False
        If Len(Email) < 8 Then IsValid = False
        Return IsValid
    End Function

    Public Sub UpdateReportTime(ByVal AccID As Long, ByVal RptDate As Date)
        Dim IsComplete As Boolean = ReportFullResulted(AccID)
        Dim IsPartial As Boolean = QualifyPartial(AccID)
        If IsComplete = True Then  'FINAL
            If FinalRPTModified(AccID) Then
                ExecuteSqlProcedure("Update Requisitions set Reported_Final = '" & RptDate & "', RPT_Status = " &
                "'FINAL-CORRECTED' where (Reported_Final is Null or CharIndex('FINAL', RPT_Status) = 0) and ID = " & AccID)
                ExecuteSqlProcedure("Update Requisitions set ReportedOn = '" & RptDate & "' where ReportedOn is Null and ID = " & AccID)
                '
                ExecuteSqlProcedure("Delete from Event_Capture where Event_ID in (12, 13, 14, 120, " &
                "130, 140) and Object_Status in ('Complete', 'FINAL') and Accession_ID = " & AccID)
            Else    'first time finalyzed
                ExecuteSqlProcedure("Update Requisitions set Reported_Final = '" & RptDate & "', RPT_Status = " &
                "'FINAL' where (Reported_Final is Null or CharIndex('FINAL', RPT_Status) = 0) and ID = " & AccID)
                ExecuteSqlProcedure("Update Requisitions set ReportedOn = '" & RptDate & "' where ReportedOn is Null and ID = " & AccID)
                '
                ExecuteSqlProcedure("Delete from Event_Capture where Event_ID in (12, 13, 14, 120, " &
                "130, 140) and Object_Status in ('Incomplete', 'PARTIAL') and Accession_ID = " & AccID)
            End If
        ElseIf IsPartial Then   'partial
            ExecuteSqlProcedure("Update Requisitions set Reported_Final = Null, ReportedOn = '" &
            RptDate & "', RPT_Status = 'PARTIAL' where ID = " & AccID)
        Else    'Initial or pending
            ExecuteSqlProcedure("Update Requisitions set Reported_Final = Null, ReportedOn = Null, " &
            "Reported_Initial = '" & RptDate & "', RPT_Status = 'INITIAL' where ID = " & AccID)
            '
            ExecuteSqlProcedure("Delete from Event_Capture where Event_ID in " &
            "(12, 13, 14, 120, 130, 140) and Accession_ID = " & AccID)
        End If
    End Sub

    Private Function FinalRPTModified(ByVal AccID As Long) As Boolean
        Dim RPTChanged As Boolean = False
        Dim sSQL As String = "Select * from Acc_Results_History where Accession_ID = " & AccID
        Dim cnrm As New SqlConnection(connString)
        cnrm.Open()
        Dim cmdrm As New SqlCommand(sSQL, cnrm)
        Dim drrm As SqlDataReader = cmdrm.ExecuteReader
        If drrm.HasRows Then RPTChanged = True
        cnrm.Close()
        cnrm = Nothing
        Return RPTChanged
    End Function

    Private Function QualifyPartial(ByVal AccID As Long) As Boolean
        Dim QualPart As Boolean = False
        Dim AccTids As Integer = 0
        Dim AccQTids As Integer = 0
        'Dim DelayTIDS As String = GetDelayedTIDS()
        'Dim QuickTIDs As String = GetQuickTIDs()
        'Dim QIDS() As String = Split(QuickTIDs, ", ")
        Dim sSQL As String = "Select a.Test_ID as TIDs from Acc_Results a left outer join vPartialQualifiers b on b.Test_ID = " &
        "a.Test_ID where (Not (b.Test_ID is null) and (a.Released is null or a.Released = 0)) and a.Accession_ID = " & AccID &
        " Union Select c.Info_ID as TIDs from Acc_Info_Results c left outer join vPartialQualifiers d on d.Test_ID = c.Info_ID " &
        "where (Not (d.Test_ID is null) and (c.Released is null or c.Released = 0)) and c.Accession_ID = " & AccID & " Union " &
        "Select e.Test_ID as TIDs from Ref_Results e left outer join vPartialQualifiers f on f.Test_ID = e.Test_ID where (Not " &
        "(f.Test_ID is null) and (e.Released is null or e.Released = 0)) and e.Accession_ID = " & AccID
        If connString <> "" Then
            Dim cnp As New SqlConnection(connString)
            cnp.Open()
            Dim cmdp As New SqlCommand(sSQL, cnp)
            cmdp.CommandType = CommandType.Text
            Dim drp As SqlDataReader = cmdp.ExecuteReader
            If drp.HasRows Then 'has un-released records
                QualPart = False
            Else
                QualPart = True
            End If
            cnp.Close()
            cnp = Nothing
            If QualPart Then
                sSQL = "Select a.Test_ID as TID from Acc_Results a inner join vPartialQualifiers b on b.Test_ID = a.Test_ID where " &
                "a.Accession_ID = " & AccID & " Union Select c.Info_ID as TID from Acc_Info_Results c inner join vPartialQualifiers " &
                "d on d.Test_ID = c.Info_ID where c.Accession_ID = " & AccID & " Union Select e.Test_ID as TID from Ref_Results e " &
                "inner join vPartialQualifiers f on f.Test_ID = e.Test_ID where e.Accession_ID = " & AccID
                Dim cnq As New SqlConnection(connString)
                cnq.Open()
                Dim cmdq As New SqlCommand(sSQL, cnq)
                cmdq.CommandType = CommandType.Text
                Dim drq As SqlDataReader = cmdq.ExecuteReader
                If drq.HasRows Then 'accid includes qualifiers
                    QualPart = True
                Else
                    QualPart = False
                End If
                cnq.Close()
                cnq = Nothing
            End If
        Else    'ODBC
            Dim cnp As New sqlConnection(connString)
            cnp.Open()
            Dim cmdp As New sqlCommand(sSQL, cnp)
            cmdp.CommandType = CommandType.Text
            Dim drp As sqlDataReader = cmdp.ExecuteReader
            If drp.HasRows Then
                QualPart = False
            Else
                QualPart = True
            End If
            cnp.Close()
            cnp = Nothing
            If QualPart Then
                sSQL = "Select a.Test_ID as TID from Acc_Results a inner join vPartialQualifiers b on b.Test_ID = a.Test_ID where " &
                "a.Accession_ID = " & AccID & " Union Select c.Info_ID as TID from Acc_Info_Results c inner join vPartialQualifiers " &
                "d on d.Test_ID = c.Info_ID where c.Accession_ID = " & AccID & " Union Select e.Test_ID as TID from Ref_Results e " &
                "inner join vPartialQualifiers f on f.Test_ID = e.Test_ID where e.Accession_ID = " & AccID
                Dim cnq As New sqlConnection(connString)
                cnq.Open()
                Dim cmdq As New sqlCommand(sSQL, cnq)
                cmdq.CommandType = CommandType.Text
                Dim drq As sqlDataReader = cmdq.ExecuteReader
                If drq.HasRows Then 'accid includes qualifiers
                    QualPart = True
                Else
                    QualPart = False
                End If
                cnq.Close()
                cnq = Nothing
            End If
        End If
        Return QualPart
    End Function

    Public Function GetAccAutoReportable(ByVal AccID As Long, ByVal Medium As String) As String()
        Dim AskStatus() As String = {"", ""}
        Dim sSQL As String = ""
        If Medium = "Interface" Then
            sSQL = "Select a.RPT_Complete, b.Reported_Final, b.ReportedOn, b.Reported_Initial from Req_RPT a " &
            "inner join Requisitions b on b.ID = a.Base_ID where a.RPT_Interface <> 0 and a.Base_ID = " & AccID
        ElseIf Medium = "Fax" Then
            sSQL = "Select a.RPT_Complete, b.Reported_Final, b.ReportedOn, b.Reported_Initial from Req_RPT a " &
            "inner join Requisitions b on b.ID = a.Base_ID where a.RPT_Fax <> 0 and a.Base_ID = " & AccID
        ElseIf Medium = "Email" Then
            sSQL = "Select a.RPT_Complete, b.Reported_Final, b.ReportedOn, b.Reported_Initial from Req_RPT a " &
            "inner join Requisitions b on b.ID = a.Base_ID where a.RPT_Email <> 0 and a.Base_ID = " & AccID
        ElseIf Medium = "ProlisOn" Then
            sSQL = "Select a.RPT_Complete, b.Reported_Final, b.ReportedOn, b.Reported_Initial from Req_RPT a " &
            "inner join Requisitions b on b.ID = a.Base_ID where a.RPT_ProlisOn <> 0 and a.Base_ID = " & AccID
        Else
            sSQL = "Select a.RPT_Complete, b.Reported_Final, b.ReportedOn, b.Reported_Initial from Req_RPT a " &
            "inner join Requisitions b on b.ID = a.Base_ID where a.RPT_Print <> 0 and a.Base_ID = " & AccID
        End If
        '
        Dim cnacr As New SqlConnection(connString)
        cnacr.Open()
        Dim cmdacr As New SqlCommand(sSQL, cnacr)
        cmdacr.CommandType = CommandType.Text
        Dim dracr As SqlDataReader = cmdacr.ExecuteReader
        If dracr.HasRows Then
            While dracr.Read
                If dracr("RPT_Complete") = True Then
                    AskStatus(0) = "FINAL"
                Else
                    AskStatus(0) = "PARTIAL"
                End If
                '
                If dracr("Reported_Final") IsNot DBNull.Value AndAlso
                dracr("Reported_Final") <> "#12:00:00 AM#" Then
                    AskStatus(1) = "FINAL"
                ElseIf (dracr("ReportedOn") IsNot DBNull.Value _
                AndAlso dracr("ReportedOn") <> "#12:00:00 AM#") Then
                    AskStatus(1) = "PARTIAL"
                ElseIf (dracr("Reported_Initial") IsNot DBNull.Value _
                AndAlso dracr("Reported_Initial") <> "#12:00:00 AM#") Then
                    AskStatus(1) = "INITIAL"
                Else
                    AskStatus(1) = ""
                End If
            End While
        End If
        cnacr.Close()
        cnacr = Nothing
        Return AskStatus
    End Function

    Private Function GetQuickTIDs() As String
        Dim QTIDS As String = ""
        Dim sSQL As String = "Select a.TGP_ID as TestID from Partial_Qualifiers a inner join Tests b on b.id = a.TGP_ID where " &
        "Company_ID = " & MyLab.ID & " Union Select c.Test_ID as TestID from Tests d inner join (Group_Test c inner join " &
        "Partial_Qualifiers e on e.TGP_ID = c.Group_ID) on c.Test_ID = d.ID where e.Company_ID = " & MyLab.ID & " Union Select " &
        "g.ID as TestID from Tests g inner join (Prof_GrpTst f inner join Partial_Qualifiers h on h.TGP_ID = f.Profile_ID) on " &
        "f.GrpTst_ID = g.ID where h.Company_ID = " & MyLab.ID & " Union Select k.Test_ID as TestID from Tests i inner join " &
        "(Group_Test k inner join (Prof_GrpTst j inner join Partial_Qualifiers l on l.TGP_ID = j.Profile_ID) on j.GrpTst_ID = " &
        "k.Group_ID) on i.ID = k.Test_ID where l.Company_ID = " & MyLab.ID
        '
        Dim cnq As New SqlConnection(connString)
        cnq.Open()
        Dim cmdq As New SqlCommand(sSQL, cnq)
        Dim drq As SqlDataReader = cmdq.ExecuteReader
        If drq.HasRows Then
            While drq.Read
                If InStr(QTIDS, drq("TestID").ToString & ", ") _
                 = 0 Then QTIDS += drq("TestID").ToString & ", "
            End While
        End If
        cnq.Close()
        cnq = Nothing
        If QTIDS.EndsWith(", ") Then QTIDS = Microsoft.VisualBasic.Mid(QTIDS, 1, Len(QTIDS) - 2)
        Return QTIDS
    End Function

    Public Function GetCCExpireDate(ByVal MMYY As String) As Date
        Dim MM As String = Microsoft.VisualBasic.Left(MMYY, 2)
        Dim YY As String = Microsoft.VisualBasic.Right(MMYY, 2)
        Dim DD As String = ""
        Select Case MM
            Case "01", "03", "05", "07", "08", "10", "12"
                DD = "31"
            Case "04", "06", "09", "11"
                DD = "30"
            Case Else
                If Val(YY) \ 4 = 0 Then 'Leap
                    DD = "29"
                Else
                    DD = "28"
                End If
        End Select
        Return CDate(MM & "/" & DD & "/" & YY)
    End Function

    Public Sub EmailReport(ByVal Rpt As String, ByVal BaseID As Long, ByVal Email As String)
        If Not System.IO.Directory.Exists("C:\PROLIS LOGS\EMAILS\" & Format(Now, "MM-dd-yyyy") & "\") Then _
        System.IO.Directory.CreateDirectory("C:\PROLIS LOGS\EMAILS\" & Format(Now, "MM-dd-yyyy") & "\")
        Dim FS As New System.IO.StreamWriter("C:\PROLIS LOGS\EMAILS\" & Format(Now, "MM-dd-yyyy") & "\" &
        Format(Now, "MM-dd-yyyy") & ".LOG", True)
        'Try
        '    Dim UID As String = My.Settings.UID.ToString
        '    Dim PWD As String = My.Settings.PWD.ToString
        '    Dim gReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        '    If Rpt = "ACC" Then
        '        gReport.Load(Application.StartupPath & "\Reports\AccResults.rpt")
        '        gReport.SetDatabaseLogon(UID, PWD)
        '        gReport.RecordSelectionFormula = "{Requisitions.ID} = " & BaseID
        '    Else
        '        gReport.Load(Application.StartupPath & "\Reports\HistResults.rpt")
        '        gReport.SetDatabaseLogon(UID, PWD)
        '        gReport.RecordSelectionFormula = "{Requisitions.Patient_ID} = " & BaseID
        '    End If
        '    'gReport.PrintOptions.PrinterName = "fax"
        '    If System.IO.File.Exists("C:\ProlisEReport.PDF") Then System.IO.File.Delete("C:\ProlisEReport.PDF")
        '    gReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, "C:\ProlisEReport.PDF")
        '    '
        '    Dim MyData() As String
        '    MyData = GetMyData()
        '    Dim Sender As String = MyData(1)
        '    Dim Laboratory As String = MyData(0)
        '    Dim Subject As String = "Confidential Result Report from " & Laboratory
        '    Dim Body As String = "Dear Provider," & vbCrLf & "Please find herewith the " _
        '    & "attached Result Report for the laboratory services on the specimen subm" _
        '    & "itted by your office." & vbCrLf & "Please feel free to contact us for " _
        '    & "any question you may have." & vbCrLf & vbCrLf & "It is always a pleasure " _
        '    & "to serve you." & vbCrLf & vbCrLf & vbCrLf & "Sincerely," & vbCrLf &
        '    vbCrLf & vbCrLf & vbCrLf & Laboratory & vbCrLf & vbCrLf & vbCrLf & vbCrLf &
        '    "Note: Attached report needs to be opened with Adobe Acrobat Reader. If you" _
        '    & " don't have one, please download a copy free of charge from " &
        '    "http://www.adobe.com" & vbCrLf & vbCrLf & vbCrLf & "*** This email is a " &
        '    "professional correspondence. If you are not the intended recipient of " &
        '    "this message, please delete it from your system immediately ***" & vbCrLf &
        '    "**********************************************************************"
        '    Dim msg As New MailMessage(Sender, Email, Subject, Body)
        '    msg.Attachments.Add(New Attachment("C:\ProlisEReport.PDF"))
        '    Dim client As New SmtpClient(MyData(2))
        '    'client.Timeout = 1000
        '    'client.UseDefaultCredentials = False
        '    'Dim Auth As New System.Net.NetworkCredential(MyData(3), MyData(4))
        '    'client.Credentials = Auth
        '    client.UseDefaultCredentials = True
        '    client.Send(msg)
        '    System.Threading.Thread.Sleep(1000)
        '    msg.Dispose()
        '    msg = Nothing
        '    '            
        '    FS.WriteLine("SU: Emailed " & IIf(Rpt = "ACC", "Accession", "History") & " Report of " & BaseID _
        '    & " To " & Email & " at " & Now)
        'Catch Ex As Exception
        '    FS.WriteLine("ER: Failed " & IIf(Rpt = "ACC", "Accession", "History") & " Report of " & BaseID _
        '    & " To " & Email & " at " & Now)
        '    FS.WriteLine("Error: " & Ex.ToString)
        'Finally
        '    FS.Close()
        '    FS = Nothing
        'End Try
    End Sub

    Public Sub EmailInvoiceToPatient(ByVal ChargeID As String, ByVal Email As String)
        Dim InvoiceInfo() As String = GetInvoiceInfo(ChargeID) '0=AccID, 1=Name(First Last)
        Dim MyData() As String = GetMyData()    '0=Lab, 1=labEmail, 2=SMTP, 3=SMTPUID, 4=SMTPPWD, 5=Port, 6=UseSSL, 7=LabPhone, 8=invoicelink
        If MyData(8).EndsWith("/") Then
            MyData(8) += "Patientslogin.aspx"
        Else
            MyData(8) += "/Patientslogin.aspx"
        End If
        Dim Subject As String = "Invoice# " & ChargeID & " from " & MyData(0)
        Try
            If InvoiceInfo(1) <> "" AndAlso MyData(8) <> "" Then
                Dim Body As String = "Dear " & InvoiceInfo(1) & "," & vbCrLf & "Please find herewith the " _
                & "attached Invoice# " & ChargeID & ", representing the charges you are responsible " &
                "of, for the laboratory services provided to you. " & vbCrLf & "Please feel free to " &
                "contact us at: " & DecoratePhone(MyData(7)) & " for any question you may have." & vbCrLf _
                & vbCrLf
                '
                Body += "To view the invoice or pay online, please click the link below;" &
                vbCrLf & MyData(8) & vbCrLf & vbCrLf
                '
                Body += "It is always a pleasure to serve you." & vbCrLf & vbCrLf & vbCrLf & "Sincerely," &
                vbCrLf & vbCrLf & vbCrLf & MyData(0) & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf &
                "*** This email is a " &
                "professional correspondence. If you are not the intended recipient of " &
                "this message, please delete it from your system immediately ***" & vbCrLf &
                "**********************************************************************"
                '0=Lab, 1=labEmail, 2=SMTP, 3=SMTPUID, 4=SMTPPWD, 5=Port, 6=UseSSL, 7=LabPhone, 8=invoicelink
                Dim Client As New SmtpClient(MyData(2), MyData(5))
                Dim msg As New MailMessage(MyData(1), Email, Subject, Body)
                Client.Credentials = New Net.NetworkCredential(MyData(3), MyData(4))
                Client.Send(msg)
                'client.UseDefaultCredentials = False
                'Dim Auth As New System.Net.NetworkCredential(MyData(3), MyData(4))
                'client.Credentials = Auth
                'Client.UseDefaultCredentials = True
                'Client.Send(msg)
                'Client.DeliveryMethod = SmtpDeliveryMethod.Network
                System.Threading.Thread.Sleep(500)
                msg.Dispose()
                msg = Nothing
                ' 
                MakeEmailLogEntry(InvoiceInfo(0), ChargeID, Email, InvoiceInfo(1), Subject, Date.Now, 0, "Success", MyData(6))
            End If
        Catch Ex As Exception
            MakeEmailLogEntry(InvoiceInfo(0), ChargeID, Email, InvoiceInfo(1), Subject, Date.Now, 0, Ex.Message, MyData(6))
        End Try
    End Sub

    Public Function DecoratePhone(ByVal PhoneStr As String) As String
        PhoneStr = Replace(PhoneStr, "(", "")
        PhoneStr = Replace(PhoneStr, ")", "")
        PhoneStr = Replace(PhoneStr, " ", "")
        PhoneStr = Replace(PhoneStr, "-", "")
        PhoneStr = Replace(PhoneStr, ".", "")
        PhoneStr = Replace(PhoneStr, "/", "")
        PhoneStr = Replace(PhoneStr, "\", "")
        PhoneStr = Replace(PhoneStr, "^", "")
        If PhoneStr.Length = 10 Then
            PhoneStr = "(" & PhoneStr.Substring(0, 3) & ") " &
            PhoneStr.Substring(3, 3) & "-" & PhoneStr.Substring(6)
        End If
        Return PhoneStr
    End Function

    Public Sub MakeEmailLogEntry(ByVal AccID As Long, ByVal ChargeID As Long, ByVal Email As String, ByVal Recipient As String,
    ByVal Subject As String, ByVal SentTime As Date, ByVal Pages As Integer, ByVal Status As String, ByVal Comment As String)
        Dim sSQL As String = "Insert into Email_Log (ID, Accession_ID, Charge_ID, Email, Recipient, " &
        "Subject, SentTime, Attachments, Status, Comment) values (" & GetNextEmailID() & ", " & AccID &
        ", " & ChargeID & ", '" & Trim(Email) & "', '" & Recipient & "', '" & Trim(Subject) & "', '" &
        SentTime & "', " & Pages & ", '" & Status & "', '" & Comment & "')"
        ExecuteSqlProcedure(sSQL)
    End Sub

    Private Function GetNextEmailID() As Long
        Dim NextID As Integer = 1
        Dim cne As New SqlConnection(connString)
        cne.Open()
        Dim cmde As New SqlCommand("Select Max(ID) as LastID from Email_Log", cne)
        cmde.CommandType = CommandType.Text
        Dim dre As SqlDataReader = cmde.ExecuteReader
        If dre.HasRows Then
            While dre.Read
                If Not (dre("LastID") Is Nothing OrElse dre("LastID") Is DBNull.Value) Then
                    NextID = dre("LastID") + 1
                End If
            End While
        End If
        cne.Close()
        cne = Nothing
        Return NextID
    End Function

    Private Function GetInvoiceInfo(ByVal ChargeID As Long) As String()
        Dim Info() As String = {"", ""}
        Dim cnn As New SqlConnection(connString)
        cnn.Open()
        Dim cmdn As New SqlCommand("Select a.Accession_ID, b.FirstName, b.LastName from " &
        "Charges a inner join Patients b on a.Ar_ID = b.ID where a.ArType = 2 and a.ID = " & ChargeID, cnn)
        cmdn.CommandType = CommandType.Text
        Dim drn As SqlDataReader = cmdn.ExecuteReader
        If drn.HasRows Then
            While drn.Read
                Info(0) = drn("Accession_ID")
                Info(1) = drn("FirstName") & " " & drn("LastName")
            End While
        End If
        cnn.Close()
        cnn = Nothing
        Return Info
    End Function

    Public Sub EmailDocument(ByVal sPDFile As String, ByVal Email As String, ByVal sLogDir As String, ByVal sLog As String)
        If Not System.IO.Directory.Exists(sLogDir) Then System.IO.Directory.CreateDirectory(sLogDir)
        Dim FS As New System.IO.StreamWriter(sLogDir & "\" & Format(Now, "MM-dd-yyyy") & ".LOG", True)
        Try
            Dim MyData() As String
            MyData = GetMyData()
            Dim Sender As String = MyData(1)
            Dim Laboratory As String = MyData(0)
            Dim Subject As String = "Confidential Email from " & Laboratory
            Dim Body As String = "Dear Provider," & vbCrLf & "Please find herewith the " _
            & "attached letter, requesting missing information for the laboratory services on the specimen subm" _
            & "itted by your office." & vbCrLf & "Please feel free to contact us for " _
            & "any question you may have." & vbCrLf & vbCrLf & "It is always a pleasure " _
            & "to serve you." & vbCrLf & vbCrLf & vbCrLf & "Sincerely," & vbCrLf &
            vbCrLf & vbCrLf & vbCrLf & Laboratory & vbCrLf & vbCrLf & vbCrLf & vbCrLf &
            "*** This email is a " &
            "professional correspondence. If you are not the intended recipient of " &
            "this message, please delete it from your system immediately ***" & vbCrLf &
            "**********************************************************************"
            Dim msg As New MailMessage(Sender, Email, Subject, Body)
            msg.Attachments.Add(New Attachment(sPDFile))
            Dim client As New SmtpClient(MyData(2))
            'client.Timeout = 1000
            'client.UseDefaultCredentials = False
            'Dim Auth As New System.Net.NetworkCredential(MyData(3), MyData(4))
            'client.Credentials = Auth
            client.UseDefaultCredentials = True
            client.Send(msg)
            System.Threading.Thread.Sleep(1000)
            msg.Dispose()
            msg = Nothing
            '            
            FS.WriteLine("SUCCESS: Emailed " & sLog & " To " & Email & " at " & Now)
        Catch Ex As Exception
            FS.WriteLine("Error: " & Ex.ToString)
        Finally
            FS.Close()
            FS = Nothing
        End Try
    End Sub

    Private Function GetMyData() As String()
        Dim MyData(8) As String '0=Lab, 1=labEmail, 2=SMTP, 3=SMTPUID, 4=SMTPPWD, 5=Port, 6=UseSSL, 7=LabPhone, 8=invoicelink
        Dim Lab As String = ""
        Dim Email As String = ""
        Dim SMTP As String = ""
        Dim UserAcct As String = ""
        Dim PWD As String = ""
        Dim sSQL As String = "Select * from Company where ID = 1"
        '
        Dim cnmd As New SqlConnection(connString)
        cnmd.Open()
        Dim cmdmd As New SqlCommand(sSQL, cnmd)
        cmdmd.CommandType = CommandType.Text
        Dim drmd As SqlDataReader = cmdmd.ExecuteReader
        If drmd.HasRows Then
            While drmd.Read
                If drmd("IsIndividual") IsNot DBNull.Value AndAlso drmd("IsIndividual") = False Then
                    MyData(0) = drmd("LastName_BSN")
                Else
                    If drmd("Degree") Is DBNull.Value Then
                        MyData(0) = drmd("LastName_BSN") & ", " & drmd("FirstName")
                    Else
                        MyData(0) = drmd("LastName_BSN") & ", " & drmd("FirstName") & " " & drmd("Degree")
                    End If
                End If
                If drmd("Email") IsNot DBNull.Value Then
                    MyData(1) = Trim(drmd("Email"))
                Else
                    MyData(1) = ""
                End If
                If drmd("Phone") IsNot DBNull.Value Then
                    MyData(7) = Trim(drmd("Phone"))
                Else
                    MyData(7) = ""
                End If
                If drmd("Website") IsNot DBNull.Value Then
                    MyData(8) = Trim(drmd("Website"))
                Else
                    MyData(8) = ""
                End If
            End While
        End If
        cnmd.Close()
        cnmd = Nothing
        '
        sSQL = "Select * from System_Config where Company_ID = " & MyLab.ID
        Dim cnsc As New SqlConnection(connString)
        cnsc.Open()
        Dim cmdsc As New SqlCommand(sSQL, cnsc)
        cmdsc.CommandType = CommandType.Text
        Dim drsc As SqlDataReader = cmdsc.ExecuteReader
        If drsc.HasRows Then
            While drsc.Read
                '0=Lab, 1=labEmail, 2=SMTP, 3=SMTPUID, 4=SMTPPWD, 5=Port, 6=UseSSL, 7=LabPhone, 8=invoicelink
                If drsc("ProlisSMTP") IsNot DBNull.Value Then MyData(2) = drsc("ProlisSMTP")
                If drsc("ProlisEmail") IsNot DBNull.Value Then MyData(3) = drsc("ProlisEmail")
                If drsc("ProlisPWD") IsNot DBNull.Value Then MyData(4) = drsc("ProlisPWD")
                If drsc("ProlisEmailPort") IsNot DBNull.Value Then MyData(5) = drsc("ProlisEmailPort")
                If drsc("ProlisUseSSL") IsNot DBNull.Value Then MyData(6) = drsc("ProlisUseSSL")
            End While
        End If
        cnsc.Close()
        cnsc = Nothing
        '
        Return MyData
    End Function

    Public Function GetTestDecimals(ByVal TestID As Integer) As Integer
        Dim Decs As Integer = 0
        Dim sSQL As String = "Select * from Tests where ID = " & TestID
        Dim cntd As New SqlConnection(connString)
        cntd.Open()
        Dim cmdtd As New SqlCommand(sSQL, cntd)
        cmdtd.CommandType = CommandType.Text
        Dim drtd As SqlDataReader = cmdtd.ExecuteReader
        If drtd.HasRows Then
            While drtd.Read
                If drtd("DecimalPlaces") IsNot DBNull.Value _
                Then Decs = drtd("DecimalPlaces")
            End While
        End If
        cntd.Close()
        cntd = Nothing
        Return Decs
    End Function

    Public Function GetMaxDecimals(ByVal TestID As Integer) As Integer
        Dim MaxDec As Integer = 0
        Dim DP As Integer
        Dim sSQL As String = "Select * from N_Ranges where Test_ID = " & TestID
        If connString <> "" Then
            Dim cnmd1 As New SqlConnection(connString)
            cnmd1.Open()
            Dim cmdmd1 As New SqlCommand(sSQL, cnmd1)
            cmdmd1.CommandType = CommandType.Text
            Dim drmd1 As SqlDataReader = cmdmd1.ExecuteReader
            If drmd1.HasRows Then
                While drmd1.Read
                    If InStr(drmd1("ValueFrom").ToString, ".") = 0 Then
                        DP = 0
                    Else
                        DP = Len(Microsoft.VisualBasic.Mid(drmd1("ValueFrom").ToString, InStr(drmd1("ValueFrom").ToString, ".") + 1))
                    End If
                    If DP > MaxDec Then MaxDec = DP
                    If InStr(drmd1("ValueTo").ToString, ".") = 0 Then
                        DP = 0
                    Else
                        DP = Len(Microsoft.VisualBasic.Mid(drmd1("ValueTo").ToString, InStr(drmd1("ValueTo").ToString, ".") + 1))
                    End If
                    If DP > MaxDec Then MaxDec = DP
                End While
            End If
            cnmd1.Close()
            cnmd1 = Nothing
            '
            sSQL = "Select * from AG_Ranges where Test_ID = " & TestID
            Dim cnmd2 As New SqlConnection(connString)
            cnmd2.Open()
            Dim cmdmd2 As New SqlCommand(sSQL, cnmd2)
            cmdmd2.CommandType = CommandType.Text
            Dim drmd2 As SqlDataReader = cmdmd2.ExecuteReader
            If drmd2.HasRows Then
                While drmd2.Read
                    If InStr(drmd2("ValueFrom").ToString, ".") = 0 Then
                        DP = 0
                    Else
                        DP = Len(Microsoft.VisualBasic.Mid(drmd2("ValueFrom").ToString, InStr(drmd2("ValueFrom").ToString, ".") + 1))
                    End If
                    If DP > MaxDec Then MaxDec = DP
                    If InStr(drmd2("ValueTo").ToString, ".") = 0 Then
                        DP = 0
                    Else
                        DP = Len(Microsoft.VisualBasic.Mid(drmd2("ValueTo").ToString, InStr(drmd2("ValueTo").ToString, ".") + 1))
                    End If
                    If DP > MaxDec Then MaxDec = DP
                End While
            End If
            cnmd2.Close()
            cnmd2 = Nothing
        Else
            Dim cnmd1 As New sqlConnection(connString)
            cnmd1.Open()
            Dim cmdmd1 As New sqlCommand(sSQL, cnmd1)
            cmdmd1.CommandType = CommandType.Text
            Dim drmd1 As sqlDataReader = cmdmd1.ExecuteReader
            If drmd1.HasRows Then
                While drmd1.Read
                    If InStr(drmd1("ValueFrom").ToString, ".") = 0 Then
                        DP = 0
                    Else
                        DP = Len(Microsoft.VisualBasic.Mid(drmd1("ValueFrom").ToString, InStr(drmd1("ValueFrom").ToString, ".") + 1))
                    End If
                    If DP > MaxDec Then MaxDec = DP
                    If InStr(drmd1("ValueTo").ToString, ".") = 0 Then
                        DP = 0
                    Else
                        DP = Len(Microsoft.VisualBasic.Mid(drmd1("ValueTo").ToString, InStr(drmd1("ValueTo").ToString, ".") + 1))
                    End If
                    If DP > MaxDec Then MaxDec = DP
                End While
            End If
            cnmd1.Close()
            cnmd1 = Nothing
            '
            sSQL = "Select * from AG_Ranges where Test_ID = " & TestID
            Dim cnmd2 As New sqlConnection(connString)
            cnmd2.Open()
            Dim cmdmd2 As New sqlCommand(sSQL, cnmd2)
            cmdmd2.CommandType = CommandType.Text
            Dim drmd2 As sqlDataReader = cmdmd2.ExecuteReader
            If drmd2.HasRows Then
                While drmd2.Read
                    If InStr(drmd2("ValueFrom").ToString, ".") = 0 Then
                        DP = 0
                    Else
                        DP = Len(Microsoft.VisualBasic.Mid(drmd2("ValueFrom").ToString, InStr(drmd2("ValueFrom").ToString, ".") + 1))
                    End If
                    If DP > MaxDec Then MaxDec = DP
                    If InStr(drmd2("ValueTo").ToString, ".") = 0 Then
                        DP = 0
                    Else
                        DP = Len(Microsoft.VisualBasic.Mid(drmd2("ValueTo").ToString, InStr(drmd2("ValueTo").ToString, ".") + 1))
                    End If
                    If DP > MaxDec Then MaxDec = DP
                End While
            End If
            cnmd2.Close()
            cnmd2 = Nothing
        End If
        Return MaxDec
    End Function

    Public Function GetTestParams(ByVal TestID As Integer) As String()
        Dim PRMS() As String = {"", ""}
        Dim sSQL As String = "Select * from Tests where IsCalculated <> 0 and ID = " & TestID
        If connString <> "" Then
            Dim cnprm As New SqlConnection(connString)
            cnprm.Open()
            Dim cmdprm As New SqlCommand(sSQL, cnprm)
            cmdprm.CommandType = CommandType.Text
            Dim drprm As SqlDataReader = cmdprm.ExecuteReader
            If drprm.HasRows Then
                While drprm.Read
                    If drprm("Formula") IsNot DBNull.Value _
                    AndAlso Trim(drprm("Formula")) <> "" Then _
                    PRMS(0) = Trim(drprm("Formula"))
                    If drprm("DecimalPlaces") IsNot DBNull.Value _
                    AndAlso drprm("DecimalPlaces") <> -1 Then _
                    PRMS(1) = drprm("DecimalPlaces").ToString
                End While
            End If
            cnprm.Close()
            cnprm = Nothing
        Else
            Dim cnprm As New sqlConnection(connString)
            cnprm.Open()
            Dim cmdprm As New sqlCommand(sSQL, cnprm)
            cmdprm.CommandType = CommandType.Text
            Dim drprm As sqlDataReader = cmdprm.ExecuteReader
            If drprm.HasRows Then
                While drprm.Read
                    If drprm("Formula") IsNot DBNull.Value _
                    AndAlso Trim(drprm("Formula")) <> "" Then _
                    PRMS(0) = Trim(drprm("Formula"))
                    If drprm("DecimalPlaces") IsNot DBNull.Value _
                    AndAlso drprm("DecimalPlaces") <> -1 Then _
                    PRMS(1) = drprm("DecimalPlaces").ToString
                End While
            End If
            cnprm.Close()
            cnprm = Nothing
        End If
        Return PRMS
    End Function

    Public Function GetFormulaTests(ByVal TestID As Integer) As String
        Dim Tsts As String = ""
        Dim Formula As String = ""
        Dim sSQL As String = "Select Formula from Tests where IsCalculated <> 0 and ID = " & TestID
        If connString <> "" Then
            Dim cnft As New SqlConnection(connString)
            cnft.Open()
            Dim cmdft As New SqlCommand(sSQL, cnft)
            cmdft.CommandType = CommandType.Text
            Dim drft As SqlDataReader = cmdft.ExecuteReader
            If drft.HasRows Then
                While drft.Read
                    If drft("Formula") IsNot DBNull.Value AndAlso
                    Trim(drft("Formula")) <> "" Then Formula = Trim(drft("Formula"))
                End While
            End If
            cnft.Close()
            cnft = Nothing
        Else
            Dim cnft As New sqlConnection(connString)
            cnft.Open()
            Dim cmdft As New sqlCommand(sSQL, cnft)
            cmdft.CommandType = CommandType.Text
            Dim drft As sqlDataReader = cmdft.ExecuteReader
            If drft.HasRows Then
                While drft.Read
                    If drft("Formula") IsNot DBNull.Value AndAlso
                    Trim(drft("Formula")) <> "" Then Formula = Trim(drft("Formula"))
                End While
            End If
            cnft.Close()
            cnft = Nothing
        End If
        '
        If Formula <> "" Then
            Do Until Formula = ""
                If InStr(Formula, "{") > 0 Then
                    Formula = Microsoft.VisualBasic.Mid(Formula,
                    InStr(Formula, "{") + 1)
                    Tsts += Microsoft.VisualBasic.Mid(Formula,
                    1, InStr(Formula, "}") - 1) & "|"
                    Formula = Microsoft.VisualBasic.Mid(Formula,
                    InStr(Formula, "}") + 1)
                Else
                    Formula = ""
                End If
            Loop
            If Tsts.EndsWith("|") Then Tsts = Microsoft.VisualBasic.Mid(Tsts, 1, Len(Tsts) - 1)
        End If
        Return Tsts
    End Function

    Public Function GetFormula(ByVal TestID As Integer) As String
        Dim Formula As String = ""
        Dim sSQL As String = "Select Formula from Tests where IsCalculated <> 0 and ID = " & TestID
        If connString <> "" Then
            Dim cngf As New SqlConnection(connString)
            cngf.Open()
            Dim cmdgf As New SqlCommand(sSQL, cngf)
            cmdgf.CommandType = CommandType.Text
            Dim drgf As SqlDataReader = cmdgf.ExecuteReader
            If drgf.HasRows Then
                While drgf.Read
                    If drgf("Formula") IsNot DBNull.Value Then
                        Formula = drgf("Formula")
                    Else
                        Formula = ""
                    End If
                End While
            End If
            cngf.Close()
            cngf = Nothing
        Else
            Dim cngf As New sqlConnection(connString)
            cngf.Open()
            Dim cmdgf As New sqlCommand(sSQL, cngf)
            cmdgf.CommandType = CommandType.Text
            Dim drgf As sqlDataReader = cmdgf.ExecuteReader
            If drgf.HasRows Then
                While drgf.Read
                    If drgf("Formula") IsNot DBNull.Value Then
                        Formula = drgf("Formula")
                    Else
                        Formula = ""
                    End If
                End While
            End If
            cngf.Close()
            cngf = Nothing
        End If
        Return Formula
    End Function

    Public Function IsCalculated(ByVal Test_ID As Integer) As Boolean
        Dim Calculated As Boolean = False
        Dim sSQL As String = "Select * from Tests where not (Formula is Null or Formula = '') and ID = " & Test_ID
        If connString <> "" Then
            Dim cnic As New SqlConnection(connString)
            cnic.Open()
            Dim cmdic As New SqlCommand(sSQL, cnic)
            cmdic.CommandType = CommandType.Text
            Dim dric As SqlDataReader = cmdic.ExecuteReader
            If dric.HasRows Then Calculated = True
            cnic.Close()
            cnic = Nothing
        Else
            Dim cnic As New sqlConnection(connString)
            cnic.Open()
            Dim cmdic As New sqlCommand(sSQL, cnic)
            cmdic.CommandType = CommandType.Text
            Dim dric As sqlDataReader = cmdic.ExecuteReader
            If dric.HasRows Then Calculated = True
            cnic.Close()
            cnic = Nothing
        End If
        Return Calculated
    End Function

    Public Function GetLabelPrinterName() As String
        Dim Printer As String = ""
        Dim PC As String = My.Computer.Name
        Dim sSQL As String = "Select * from LabelPrinters where PC_Name = '" & PC & "'"
        If ThisUser.UseRemotePrinter Then
            Return "Prolis Remote Print (DYMO)"
        End If


        Dim cnlp As New SqlConnection(connString)
        cnlp.Open()
        Dim cmdlp As New SqlCommand(sSQL, cnlp)
        cmdlp.CommandType = CommandType.Text
        Dim drlp As SqlDataReader = cmdlp.ExecuteReader
        If drlp.HasRows Then
            While drlp.Read
                Printer = drlp("Printer_Name")
            End While
        End If
        cnlp.Close()
        cnlp = Nothing

        If Not String.IsNullOrEmpty(My.Settings.LabelPrinter) Then

            Printer = My.Settings.LabelPrinter
        End If

        Return Printer
    End Function

    Public Function NextPaymentID()
        Dim pmtid As Long = 1
        Dim sSQL As String = "Select max(ID) as LastID from Payments"
        If connString <> "" Then
            Dim cnpmt As New SqlConnection(connString)
            cnpmt.Open()
            Dim cmdpmt As New SqlCommand(sSQL, cnpmt)
            cmdpmt.CommandType = CommandType.Text
            Dim drpmt As SqlDataReader = cmdpmt.ExecuteReader
            If drpmt.HasRows Then
                While drpmt.Read
                    If drpmt("LastID") IsNot DBNull.Value _
                    Then pmtid = drpmt("LastID") + 1
                End While
            End If
            cnpmt.Close()
            cnpmt = Nothing
        Else
            Dim cnpmt As New sqlConnection(connString)
            cnpmt.Open()
            Dim cmdpmt As New sqlCommand(sSQL, cnpmt)
            cmdpmt.CommandType = CommandType.Text
            Dim drpmt As sqlDataReader = cmdpmt.ExecuteReader
            If drpmt.HasRows Then
                While drpmt.Read
                    If drpmt("LastID") IsNot DBNull.Value _
                    Then pmtid = drpmt("LastID") + 1
                End While
            End If
            cnpmt.Close()
            cnpmt = Nothing
        End If
        Return pmtid
    End Function

    Public Function SaveCreditCard(ByVal ArType As Integer, ByVal ArID As Long, ByVal CCType As String, ByVal CCNo As String, ByVal _
    ExpireDate As Date, ByVal CVV As String, ByVal BillName As String, ByVal BillAddress As String, ByVal BillZip As String) As Long
        Dim CCID As Long = NextCCID()
        '
        Dim sSQL As String = "If Exists (Select ID as CCID from CreditCards where ArType = " & ArType & " and " &
        "Ar_ID = " & ArID & " and CCType = '" & CCType & "' and CCNo = '" & CCNo & "' and ExpireDate = '" &
        ExpireDate & "') print CCID Update CreditCards Set CVV = '" & CVV & "', BillName = '" & BillName & "', " &
        "BillAddress = '" & BillAddress & "', BillZip = '" & BillZip & "' where ArType = " & ArType & " and " &
        "Ar_ID = " & ArID & " and CCType = '" & CCType & "' and CCNo = '" & CCNo & "' and ExpireDate = '" &
        ExpireDate & "' Else Insert into Creditcards (ID, ArType, Ar_ID, CCType, CCNo, ExpireDate, CVV, " &
        "BillName, BillAddress, BillZip) values (" & CCID & ", " & ArType & ", " & ArID & ", '" &
        CCType & "', '" & CCNo & "', '" & ExpireDate & "', '" & CVV & "', '" & BillName & "', '" &
        BillAddress & "', '" & BillZip & "')"
        ExecuteSqlProcedure(sSQL)
        Return CCID
    End Function

    Public Function NextCCID() As Long
        Dim CCID As Long = 1
        Dim sSQL As String = "Select max(ID) as LastID from CreditCards"
        '
        Dim cncc As New SqlConnection(connString)
        cncc.Open()
        Dim cmdcc As New SqlCommand(sSQL, cncc)
        cmdcc.CommandType = CommandType.Text
        Dim drcc As SqlDataReader = cmdcc.ExecuteReader
        If drcc.HasRows Then
            While drcc.Read
                If drcc("LastID") IsNot DBNull.Value _
                Then CCID = drcc("LastID") + 1
            End While
        End If
        cncc.Close()
        cncc = Nothing
        Return CCID
    End Function

    Public Function GetOprands(ByVal TID As Integer) As String
        Dim Astring As String = ""
        Dim Formula As String = ""
        Dim TestID As String = ""
        Dim sSQL As String = "Select * from Tests where ID = " & TID
        '
        Dim cngo As New SqlConnection(connString)
        cngo.Open()
        Dim cmdgo As New SqlCommand(sSQL, cngo)
        cmdgo.CommandType = CommandType.Text
        Dim drgo As SqlDataReader = cmdgo.ExecuteReader
        If drgo.HasRows Then
            While drgo.Read
                If drgo("IsCalculated") = True Then
                    Formula = drgo("Formula")
                    If Formula <> "" Then
                        Do Until InStr(Formula, "}") = 0
                            If InStr(Formula, "{") > 0 And InStr(Formula, "}") > 0 Then
                                TestID = Microsoft.VisualBasic.Mid(Formula, InStr(Formula, "{") + 1)
                                TestID = Microsoft.VisualBasic.Mid(TestID, 1, InStr(TestID, "}") - 1)
                                Astring += Trim(TestID) & ","
                                TestID = ""
                                Formula = Trim(Microsoft.VisualBasic.Mid(Formula, InStr(Formula, "}") + 1))
                            End If
                        Loop
                        If Astring.EndsWith(",") Then Astring = Microsoft.VisualBasic.Mid(Astring, 1, Len(Astring) - 1)
                    End If
                End If
            End While
        End If
        cngo.Close()
        cngo = Nothing
        GetOprands = Astring
    End Function

    Public Function IsAutomarker(ByVal TestID As Integer) As Boolean
        Dim Automarker As Boolean = False
        Dim sSQL As String = "Select * from Tests where Automarker <> 0 and ID = " & TestID
        Dim cnam As New SqlConnection(connString)
        cnam.Open()
        Dim cmdam As New SqlCommand(sSQL, cnam)
        cmdam.CommandType = CommandType.Text
        Dim dram As SqlDataReader = cmdam.ExecuteReader
        If dram.HasRows Then Automarker = True
        cnam.Close()
        cnam = Nothing
        Return Automarker
    End Function

    Public Function IsQualitative(ByVal TestID As Integer) As Boolean
        Dim Qualitative As Boolean = False
        Dim sSQL As String = "Select * from Tests where Qualitative <> 0 and ID = " & TestID
        '
        Dim cnln As New SqlConnection(connString)
        cnln.Open()
        Dim cmdln As New SqlCommand(sSQL, cnln)
        cmdln.CommandType = CommandType.Text
        Dim drln As SqlDataReader = cmdln.ExecuteReader
        If drln.HasRows Then Qualitative = True
        cnln.Close()
        cnln = Nothing
        Return Qualitative
    End Function

    Public Function GetTOXTGPNames(ByVal TestID As Integer) As String()
        Dim TGPNames() As String = {""}
        Dim AltNames() As String
        Dim sSQL As String = "Select * from Tests where ID = " & TestID
        Dim cnttn As New SqlConnection(connString)
        cnttn.Open()
        Dim cmdttn As New SqlCommand(sSQL, cnttn)
        cmdttn.CommandType = CommandType.Text
        Dim DRttn As SqlDataReader = cmdttn.ExecuteReader
        If DRttn.HasRows Then
            While DRttn.Read
                If TGPNames(UBound(TGPNames)) <> "" Then ReDim Preserve TGPNames(UBound(TGPNames) + 1)
                TGPNames(UBound(TGPNames)) = Trim(DRttn("Name"))
                If DRttn("AlternateNames") IsNot DBNull.Value _
                AndAlso Trim(DRttn("AlternateNames")) <> "" Then
                    If InStr(Trim(DRttn("AlternateNames")), "|") > 0 Then
                        AltNames = Split(DRttn("AlternateNames"), "|")
                        For i As Integer = 0 To AltNames.Length - 1
                            If Trim(AltNames(i)) <> "" Then
                                If TGPNames(UBound(TGPNames)) <> "" Then ReDim Preserve TGPNames(UBound(TGPNames) + 1)
                                TGPNames(UBound(TGPNames)) = Trim(AltNames(i))
                            End If
                        Next
                    Else
                        If TGPNames(UBound(TGPNames)) <> "" Then ReDim Preserve TGPNames(UBound(TGPNames) + 1)
                        TGPNames(UBound(TGPNames)) = Trim(DRttn("AlternateNames"))
                    End If
                End If
            End While
        End If
        cnttn.Close()
        cnttn = Nothing
        Return TGPNames
    End Function

    Public Function GetTGPNameByCPT(ByVal CPT As String) As String
        Dim TGPName As String = ""
        Dim sSQL As String = "Select Name from Tests where CPT_Code = '" _
        & CPT & "' Union Select Name from Groups where CPT_Code = '" & CPT &
        "' Union Select Name from Profiles where CPT_Code = '" & CPT & "'"
        If connString <> "" Then
            Dim cngcc As New SqlConnection(connString)
            cngcc.Open()
            Dim cmdgcc As New SqlCommand(sSQL, cngcc)
            cmdgcc.CommandType = CommandType.Text
            Dim drgcc As SqlDataReader = cmdgcc.ExecuteReader
            If drgcc.HasRows Then
                While drgcc.Read
                    If drgcc("Name") IsNot DBNull.Value _
                    AndAlso Trim(drgcc("Name")) <> "" _
                    Then TGPName = Trim(drgcc("Name"))
                End While
            End If
            cngcc.Close()
            cngcc = Nothing
        Else
            Dim cngcc As New sqlConnection(connString)
            cngcc.Open()
            Dim cmdgcc As New sqlCommand(sSQL, cngcc)
            cmdgcc.CommandType = CommandType.Text
            Dim drgcc As sqlDataReader = cmdgcc.ExecuteReader
            If drgcc.HasRows Then
                While drgcc.Read
                    If drgcc("Name") IsNot DBNull.Value _
                    AndAlso Trim(drgcc("Name")) <> "" _
                    Then TGPName = Trim(drgcc("Name"))
                End While
            End If
            cngcc.Close()
            cngcc = Nothing
        End If
        Return TGPName
    End Function

    Public Function GetTGPName(ByVal TGPID As Integer) As String
        Dim TGPName As String = ""
        Dim sSQL As String = "Select Name from Tests where ID = " & TGPID & " Union Select Name from " &
        "Groups where ID = " & TGPID & " Union Select Name from Profiles where ID = " & TGPID
        If connString <> "" Then
            Dim cntgpn As New SqlConnection(connString)
            cntgpn.Open()
            Dim selcmd As New SqlCommand(sSQL, cntgpn)
            selcmd.CommandType = CommandType.Text
            Dim selDR As SqlDataReader = selcmd.ExecuteReader
            If selDR.HasRows Then
                While selDR.Read
                    If selDR("Name") IsNot DBNull.Value _
                    AndAlso Trim(selDR("Name")) <> "" _
                    Then TGPName = Trim(selDR("Name"))
                End While
            End If
            cntgpn.Close()
            cntgpn = Nothing
        Else
            Dim cntgpn As New sqlConnection(connString)
            cntgpn.Open()
            Dim selcmd As New sqlCommand(sSQL, cntgpn)
            selcmd.CommandType = CommandType.Text
            Dim selDR As sqlDataReader = selcmd.ExecuteReader
            If selDR.HasRows Then
                While selDR.Read
                    If selDR("Name") IsNot DBNull.Value _
                    AndAlso Trim(selDR("Name")) <> "" _
                    Then TGPName = Trim(selDR("Name"))
                End While
            End If
            cntgpn.Close()
            cntgpn = Nothing
        End If
        Return TGPName
    End Function

    Public Function GetTGPShortName(ByVal TGPID As Integer) As String
        Dim TGPSName As String = ""
        Dim cnsn As New SqlConnection(connString)
        cnsn.Open()
        Dim selcmd As New SqlCommand("Select Abbr from Tests where ID = " _
        & TGPID & " Union Select Abbr from Groups where ID = " & TGPID &
        " Union Select Abbr from Profiles where ID = " & TGPID, cnsn)
        selcmd.CommandType = CommandType.Text
        Dim selDR As SqlDataReader = selcmd.ExecuteReader
        If selDR.HasRows Then
            While selDR.Read
                TGPSName = selDR("Abbr")
            End While
        End If
        cnsn.Close()
        cnsn = Nothing
        Return Trim(TGPSName)
    End Function

    Public Function GetTGPType(ByVal TGPID As Integer) As String
        Dim TGPType As String = ""
        Dim sSQL As String = "Select ComponentType from Tests where ID = " &
        TGPID & " Union Select ComponentType from Groups where ID = " &
        TGPID & " Union Select ComponentType from Profiles where ID = " & TGPID
        Dim cntpe As New SqlConnection(connString)
        cntpe.Open()
        Dim cmdtpe As New SqlCommand(sSQL, cntpe)
        cmdtpe.CommandType = CommandType.Text
        Dim drtpe As SqlDataReader = cmdtpe.ExecuteReader
        If drtpe.HasRows Then
            While drtpe.Read
                If drtpe("ComponentType") IsNot DBNull.Value _
                AndAlso drtpe("ComponentType") <> "" Then _
                TGPType = drtpe("ComponentType")
            End While
        End If
        cntpe.Close()
        cntpe = Nothing
        Return TGPType
    End Function

    Public Function GetUserName(ByVal ID As Long) As String
        Dim UserName As String = ""
        Dim sSQL As String = "Select * from Users where ID = " & ID
        '
        Dim cnu As New SqlConnection(connString)
        cnu.Open()
        Dim cmdu As New SqlCommand(sSQL, cnu)
        cmdu.CommandType = CommandType.Text
        Dim dru As SqlDataReader = cmdu.ExecuteReader
        If dru.HasRows Then
            While dru.Read
                UserName = dru("UserName")
            End While
        End If
        cnu.Close()
        cnu = Nothing
        Return UserName
    End Function

    Public Sub Numerals(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'If InStr("0123456789" & Chr(8), e.KeyChar) = 0 Then e.KeyChar = Chr(0)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Public Sub Prices(ByVal sender As TextBox, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If InStr("0123456789-" & IIf(InStr(sender.Text, ".") = 0, ".", "") & Chr(8), e.KeyChar) = 0 Then
            e.KeyChar = Chr(0)
        End If
    End Sub

    Public Sub NRanges(ByVal sender As TextBox, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If InStr("-0123456789><" & IIf(InStr(sender.Text, ".") = 0, ".", "") & Chr(8), e.KeyChar) = 0 Then
            e.KeyChar = Chr(0)
        End If
    End Sub

    Public Sub TimeEntry(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If InStr("0123456789:AaPpMm " & Chr(8), e.KeyChar) = 0 Then
            e.KeyChar = Chr(0)
        End If
    End Sub

    Public Function ReportableFormatRequired(ByVal TestID As Integer) As Boolean
        Dim RptRequired As Boolean = False
        Dim sSQL As String = "Select ReportableRange from Tests where ID = " & TestID
        If connString <> "" Then
            Dim cnrfr As New SqlConnection(connString)
            cnrfr.Open()
            Dim cmdrfr As New SqlCommand(sSQL, cnrfr)
            cmdrfr.CommandType = CommandType.Text
            Dim drrfr As SqlDataReader = cmdrfr.ExecuteReader
            If drrfr.HasRows Then
                While drrfr.Read
                    If drrfr("ReportableRange") IsNot DBNull.Value _
                    Then RptRequired = drrfr("ReportableRange")
                End While
            End If
            cnrfr.Close()
            cnrfr = Nothing
        Else
            Dim cnrfr As New sqlConnection(connString)
            cnrfr.Open()
            Dim cmdrfr As New sqlCommand(sSQL, cnrfr)
            cmdrfr.CommandType = CommandType.Text
            Dim drrfr As sqlDataReader = cmdrfr.ExecuteReader
            If drrfr.HasRows Then
                While drrfr.Read
                    If drrfr("ReportableRange") IsNot DBNull.Value _
                    Then RptRequired = drrfr("ReportableRange")
                End While
            End If
            cnrfr.Close()
            cnrfr = Nothing
        End If
        Return RptRequired
    End Function

    Public Function FormatToReportable(ByVal Result As String, ByVal TestID As Integer) As String
        Dim FinalRes As String = Result
        Dim NumRes As Single = -1
        Dim sSQL As String = "Select * from Reportable_Ranges where Test_ID = " & TestID
        If connString <> "" Then
            If IsNumeric(Result) Then
                NumRes = CSng(Result)
                Dim cntr1 As New SqlConnection(connString)
                cntr1.Open()
                Dim cmdtr1 As New SqlCommand(sSQL, cntr1)
                cmdtr1.CommandType = CommandType.Text
                Dim drtr1 As SqlDataReader = cmdtr1.ExecuteReader
                If drtr1.HasRows Then
                    While drtr1.Read
                        If drtr1("ValueFrom") IsNot DBNull.Value Then
                            If NumRes < drtr1("ValueFrom") Then _
                            FinalRes = "< " & drtr1("ValueFrom").ToString
                        End If
                        If drtr1("ValueTo") IsNot DBNull.Value Then
                            If NumRes > drtr1("ValueTo") Then _
                            FinalRes = "> " & drtr1("ValueTo").ToString
                        End If
                    End While
                End If
                cntr1.Close()
                cntr1 = Nothing
            Else
                Dim TempRes As String = CleanNumericResult(Result)
                If IsNumeric(TempRes) Then
                    NumRes = CSng(TempRes)
                    Dim cntr2 As New SqlConnection(connString)
                    cntr2.Open()
                    Dim cmdtr2 As New SqlCommand(sSQL, cntr2)
                    cmdtr2.CommandType = CommandType.Text
                    Dim drtr2 As SqlDataReader = cmdtr2.ExecuteReader
                    If drtr2.HasRows Then
                        While drtr2.Read
                            If drtr2("ValueFrom") IsNot DBNull.Value Then
                                If NumRes < drtr2("ValueFrom") Then _
                                FinalRes = "< " & drtr2("ValueFrom").ToString
                            End If
                            If drtr2("ValueTo") IsNot DBNull.Value Then
                                If NumRes > drtr2("ValueTo") Then _
                                FinalRes = "> " & drtr2("ValueTo").ToString
                            End If
                        End While
                    End If
                    cntr2.Close()
                    cntr2 = Nothing
                End If
                If TempRes = CleanNumericResult(FinalRes) Then FinalRes = Result
            End If
        Else    'odbc
            If IsNumeric(Result) Then
                NumRes = CSng(Result)
                Dim cntr1 As New sqlConnection(connString)
                cntr1.Open()
                Dim cmdtr1 As New sqlCommand(sSQL, cntr1)
                cmdtr1.CommandType = CommandType.Text
                Dim drtr1 As sqlDataReader = cmdtr1.ExecuteReader
                If drtr1.HasRows Then
                    While drtr1.Read
                        If drtr1("ValueFrom") IsNot DBNull.Value Then
                            If NumRes < drtr1("ValueFrom") Then _
                            FinalRes = "< " & drtr1("ValueFrom").ToString
                        End If
                        If drtr1("ValueTo") IsNot DBNull.Value Then
                            If NumRes > drtr1("ValueTo") Then _
                            FinalRes = "> " & drtr1("ValueTo").ToString
                        End If
                    End While
                End If
                cntr1.Close()
                cntr1 = Nothing
            Else
                Dim TempRes As String = CleanNumericResult(Result)
                If IsNumeric(TempRes) Then
                    NumRes = CSng(TempRes)
                    Dim cntr2 As New sqlConnection(connString)
                    cntr2.Open()
                    Dim cmdtr2 As New sqlCommand(sSQL, cntr2)
                    cmdtr2.CommandType = CommandType.Text
                    Dim drtr2 As sqlDataReader = cmdtr2.ExecuteReader
                    If drtr2.HasRows Then
                        While drtr2.Read
                            If drtr2("ValueFrom") IsNot DBNull.Value Then
                                If NumRes < drtr2("ValueFrom") Then _
                                FinalRes = "< " & drtr2("ValueFrom").ToString
                            End If
                            If drtr2("ValueTo") IsNot DBNull.Value Then
                                If NumRes > drtr2("ValueTo") Then _
                                FinalRes = "> " & drtr2("ValueTo").ToString
                            End If
                        End While
                    End If
                    cntr2.Close()
                    cntr2 = Nothing
                End If
                If TempRes = CleanNumericResult(FinalRes) Then FinalRes = Result
            End If
        End If
        Return FinalRes
    End Function

    Public Function CleanNumericResult(ByVal Res As String) As String
        Res = Replace(Res, "<", "") : Res = Replace(Res, ">", "") : Res = Replace(Res, " ", "")
        Return Res
    End Function

    Public Function GetFullGender(ByVal Sex As String) As String
        Dim FullName As String = ""
        If SystemConfig.DiagTarget = "V" Then
            If Sex.StartsWith("M") Then
                FullName = "Male"
            ElseIf Sex.StartsWith("F") Then
                FullName = "Female"
            ElseIf Sex.StartsWith("N") Then
                FullName = "Neutered"
            ElseIf Sex.StartsWith("S") Then
                FullName = "Spayed"
            ElseIf Sex.StartsWith("I") Then
                FullName = "Indermined"
            Else
                FullName = "Unreported"
            End If
        Else
            If Sex.StartsWith("M") Then
                FullName = "Male"
            ElseIf Sex.StartsWith("F") Then
                FullName = "Female"
            ElseIf Sex.StartsWith("G") Then
                FullName = "Transfemale"
            ElseIf Sex.StartsWith("N") Then
                FullName = "Transmale"
            ElseIf Sex.StartsWith("I") Then
                FullName = "Indermined"
            Else
                FullName = "Unreported"
            End If
        End If
        Return FullName
    End Function

    Public Function RemoveDuplicates2(ByVal initialArray As String(,)) As String(,)
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim newArray(UBound(initialArray, 1), 0) As String

        For i = 0 To UBound(initialArray, 2)
            For j = 0 To UBound(initialArray, 2)
                If Not initialArray(0, i) = "" Then
                    If Not j = i Then
                        If initialArray(0, i) = initialArray(0, j) Then
                            initialArray(0, j) = ""
                            initialArray(1, j) = ""
                        End If
                    End If
                End If
            Next
        Next
        '
        j = 0
        For i = 0 To UBound(initialArray, 2)
            If Not initialArray(0, i) = "" Then
                ReDim Preserve newArray(UBound(initialArray, 1), j)
                newArray(0, j) = initialArray(0, i)
                newArray(1, j) = initialArray(1, i)
                j = j + 1
            End If
        Next
        '
        Return newArray
    End Function

    Public Function RemoveDuplicates(ByVal initialArray As String()) As String()
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim newArray(0) As String

        For i = 0 To UBound(initialArray)
            For j = 0 To UBound(initialArray)
                If Not Trim(initialArray(i)) = "" Then
                    If Not j = i Then
                        If Trim(initialArray(i)) = Trim(initialArray(j)) Then
                            initialArray(j) = ""
                        End If
                    End If
                End If
            Next
        Next
        '
        j = 0
        For i = 0 To UBound(initialArray)
            If Not Trim(initialArray(i)) = "" Then
                ReDim Preserve newArray(j)
                newArray(j) = Trim(initialArray(i))
                j = j + 1
            End If
        Next
        '
        Return newArray
    End Function

    Public Function GetAddressID(ByVal Add1 As String, ByVal Add2 As String, ByVal City _
    As String, ByVal State As String, ByVal Zip As String, ByVal Country As String) As String
        Add1 = Replace(Add1, "'", "''")
        Add2 = Replace(Add2, "'", "''")
        City = Replace(City, "'", "''")
        State = Replace(State, "'", "''")
        Zip = Replace(Zip, "'", "''")
        Country = Replace(Country, "'", "''")
        Dim AddressID As String = ""
        Dim Exists As Boolean = False
        Dim sSQL As String = "Select ID from Addresses where Address1 = '" & Trim(Add1) & "' and " &
        "City = '" & Trim(City) & "' and State = '" & Trim(State) & "' and Zip = '" & Trim(Zip) & "'"
        Dim cnai As New SqlConnection(connString)
        cnai.Open()
        Dim cmdai As New SqlCommand(sSQL, cnai)
        cmdai.CommandType = CommandType.Text
        Dim drai As SqlDataReader = cmdai.ExecuteReader
        If drai.HasRows Then
            While drai.Read
                AddressID = drai("ID").ToString
                Exists = True
            End While
        End If
        cnai.Close()
        cnai = Nothing
        '
        If Exists = False Then  'new
            AddressID = GetNextAddressID()
            sSQL = "Insert into Addresses (ID, Address1, Address2, City, State, Zip, Country) values (" & AddressID &
            ", '" & Add1 & "', '" & Add2 & "', '" & City & "', '" & State & "', '" & Zip & "', '" & Country & "')"
        Else    'edit
            sSQL = "Update Addresses set Address1 = '" & Add1 & "', Address2 = '" & Add2 & "', City = '" & City &
            "', State = '" & State & "', Zip = '" & Zip & "', Country = '" & Country & "' where ID = " & AddressID
        End If
        ExecuteSqlProcedure(sSQL)
        '
        Return AddressID
    End Function
    Public Class AddressInfo
        Public Property Address1 As String
        Public Property Address2 As String
        Public Property City As String
        Public Property State As String
        Public Property Zip As String
    End Class

    Public Function GetAddressInfo(ByVal AddressID As String) As AddressInfo
        Dim addressInfo As New AddressInfo()
        Dim sSQL As String = "Select * from Addresses where ID = " & AddressID
        Dim connection As IDbConnection = Nothing
        Dim command As IDbCommand = Nothing
        Dim dataReader As SqlDataReader = Nothing

        Try
            If connString <> "" Then
                connection = New SqlConnection(connString)
            Else
                connection = New sqlConnection(connString)
            End If

            connection.Open()

            If connString <> "" Then
                command = New SqlCommand(sSQL, CType(connection, SqlConnection))
            Else
                command = New sqlCommand(sSQL, CType(connection, sqlConnection))
            End If

            command.CommandType = CommandType.Text
            dataReader = command.ExecuteReader

            If dataReader.HasRows Then
                If dataReader.Read Then
                    addressInfo.Address1 = If(dataReader("Address1") IsNot DBNull.Value, Trim(dataReader("Address1")), "")
                    addressInfo.Address2 = If(dataReader("Address2") IsNot DBNull.Value, Trim(dataReader("Address2")), "")
                    addressInfo.City = If(dataReader("City") IsNot DBNull.Value, Trim(dataReader("City")), "")
                    addressInfo.State = If(dataReader("State") IsNot DBNull.Value, Trim(dataReader("State")), "")
                    addressInfo.Zip = If(dataReader("Zip") IsNot DBNull.Value, Trim(dataReader("Zip")), "")
                End If
            End If


        Finally
            If dataReader IsNot Nothing Then
                dataReader.Close()
            End If

            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return addressInfo
    End Function

    Public Function GetAddress1(ByVal AddressID As Long) As String
        Dim Address As String = ""
        Dim sSQL As String = "Select * from Addresses where ID = " & AddressID
        If connString <> "" Then
            Dim cna1 As New SqlConnection(connString)
            cna1.Open()
            Dim cmda1 As New SqlCommand(sSQL, cna1)
            cmda1.CommandType = CommandType.Text
            Dim dra1 As SqlDataReader = cmda1.ExecuteReader
            If dra1.HasRows Then
                While dra1.Read
                    If dra1("Address1") IsNot DBNull.Value _
                    Then Address = Trim(dra1("Address1"))
                End While
            End If
            cna1.Close()
            cna1 = Nothing
        Else
            Dim cna1 As New sqlConnection(connString)
            cna1.Open()
            Dim cmda1 As New sqlCommand(sSQL, cna1)
            cmda1.CommandType = CommandType.Text
            Dim dra1 As sqlDataReader = cmda1.ExecuteReader
            If dra1.HasRows Then
                While dra1.Read
                    If dra1("Address1") IsNot DBNull.Value _
                    Then Address = Trim(dra1("Address1"))
                End While
            End If
            cna1.Close()
            cna1 = Nothing
        End If
        Return Address
    End Function

    Public Function GetAddress2(ByVal AddressID As Long) As String
        Dim Address As String = ""
        Dim sSQL As String = "Select * from Addresses where ID = " & AddressID
        If connString <> "" Then
            Dim cna2 As New SqlConnection(connString)
            cna2.Open()
            Dim cmda2 As New SqlCommand(sSQL, cna2)
            cmda2.CommandType = CommandType.Text
            Dim dra2 As SqlDataReader = cmda2.ExecuteReader
            If dra2.HasRows Then
                While dra2.Read
                    If dra2("Address2") IsNot DBNull.Value _
                    Then Address = Trim(dra2("Address2"))
                End While
            End If
            cna2.Close()
            cna2 = Nothing
        Else
            Dim cna2 As New sqlConnection(connString)
            cna2.Open()
            Dim cmda2 As New sqlCommand(sSQL, cna2)
            cmda2.CommandType = CommandType.Text
            Dim dra2 As sqlDataReader = cmda2.ExecuteReader
            If dra2.HasRows Then
                While dra2.Read
                    If dra2("Address2") IsNot DBNull.Value _
                    Then Address = Trim(dra2("Address2"))
                End While
            End If
            cna2.Close()
            cna2 = Nothing
        End If
        Return Address
    End Function

    Public Function GetAddressCity(ByVal AddressID As Long) As String
        Dim Address As String = ""
        Dim sSQL As String = "Select * from Addresses where ID = " & AddressID
        If connString <> "" Then
            Dim cnac As New SqlConnection(connString)
            cnac.Open()
            Dim cmdac As New SqlCommand(sSQL, cnac)
            cmdac.CommandType = CommandType.Text
            Dim drac As SqlDataReader = cmdac.ExecuteReader
            If drac.HasRows Then
                While drac.Read
                    If drac("City") IsNot DBNull.Value _
                    Then Address = Trim(drac("City"))
                End While
            End If
            cnac.Close()
            cnac = Nothing
        Else
            Dim cnac As New sqlConnection(connString)
            cnac.Open()
            Dim cmdac As New sqlCommand(sSQL, cnac)
            cmdac.CommandType = CommandType.Text
            Dim drac As sqlDataReader = cmdac.ExecuteReader
            If drac.HasRows Then
                While drac.Read
                    If drac("City") IsNot DBNull.Value _
                    Then Address = Trim(drac("City"))
                End While
            End If
            cnac.Close()
            cnac = Nothing
        End If
        Return Address
    End Function

    Public Function GetAddressState(ByVal AddressID As Long) As String
        Dim Address As String = ""
        Dim sSQL As String = "Select * from Addresses where ID = " & AddressID
        If connString <> "" Then
            Dim cnas As New SqlConnection(connString)
            cnas.Open()
            Dim cmdas As New SqlCommand(sSQL, cnas)
            cmdas.CommandType = CommandType.Text
            Dim dras As SqlDataReader = cmdas.ExecuteReader
            If dras.HasRows Then
                While dras.Read
                    If dras("State") IsNot DBNull.Value _
                    Then Address = Trim(dras("State"))
                End While
            End If
            cnas.Close()
            cnas = Nothing
        Else
            Dim cnas As New sqlConnection(connString)
            cnas.Open()
            Dim cmdas As New sqlCommand(sSQL, cnas)
            cmdas.CommandType = CommandType.Text
            Dim dras As sqlDataReader = cmdas.ExecuteReader
            If dras.HasRows Then
                While dras.Read
                    If dras("State") IsNot DBNull.Value _
                    Then Address = Trim(dras("State"))
                End While
            End If
            cnas.Close()
            cnas = Nothing
        End If
        Return Address
    End Function

    Public Function GetAddressZip(ByVal AddressID As Long) As String
        Dim Address As String = ""
        Dim sSQL As String = "Select * from Addresses where ID = " & AddressID
        If connString <> "" Then
            Dim cnaz As New SqlConnection(connString)
            cnaz.Open()
            Dim cmdaz As New SqlCommand(sSQL, cnaz)
            cmdaz.CommandType = CommandType.Text
            Dim draz As SqlDataReader = cmdaz.ExecuteReader
            If draz.HasRows Then
                While draz.Read
                    If draz("Zip") IsNot DBNull.Value _
                    Then Address = Trim(draz("Zip"))
                End While
            End If
            cnaz.Close()
            cnaz = Nothing
        Else
            Dim cnaz As New sqlConnection(connString)
            cnaz.Open()
            Dim cmdaz As New sqlCommand(sSQL, cnaz)
            cmdaz.CommandType = CommandType.Text
            Dim draz As sqlDataReader = cmdaz.ExecuteReader
            If draz.HasRows Then
                While draz.Read
                    If draz("Zip") IsNot DBNull.Value _
                    Then Address = Trim(draz("Zip"))
                End While
            End If
            cnaz.Close()
            cnaz = Nothing
        End If
        Return Address
    End Function

    Public Function GetAddressLines(ByVal AddressID As Long) As String
        Dim Address As String = ""
        Dim sSQL As String = "Select * from Addresses where ID = " & AddressID
        If connString <> "" Then
            Dim cnal As New SqlConnection(connString)
            cnal.Open()
            Dim cmdal As New SqlCommand(sSQL, cnal)
            cmdal.CommandType = CommandType.Text
            Dim dral As SqlDataReader = cmdal.ExecuteReader
            If dral.HasRows Then
                While dral.Read
                    If dral("Address2") IsNot DBNull.Value _
                    AndAlso Trim(dral("Address2")) <> "" Then
                        Address = Trim(dral("Address1")) & " " & Trim(dral("Address2"))
                    Else
                        Address = Trim(dral("Address1"))
                    End If
                End While
            End If
            cnal.Close()
            cnal = Nothing
        Else
            Dim cnal As New sqlConnection(connString)
            cnal.Open()
            Dim cmdal As New sqlCommand(sSQL, cnal)
            cmdal.CommandType = CommandType.Text
            Dim dral As sqlDataReader = cmdal.ExecuteReader
            If dral.HasRows Then
                While dral.Read
                    If dral("Address2") IsNot DBNull.Value _
                    AndAlso Trim(dral("Address2")) <> "" Then
                        Address = Trim(dral("Address1")) & " " & Trim(dral("Address2"))
                    Else
                        Address = Trim(dral("Address1"))
                    End If
                End While
            End If
            cnal.Close()
            cnal = Nothing
        End If
        Return Address
    End Function

    Public Function GetAddressCSZ(ByVal AddressID As Long) As String
        Dim Address As String = ""
        Dim sSQL As String = "Select * from Addresses where ID = " & AddressID
        If connString <> "" Then
            Dim cncsz As New SqlConnection(connString)
            cncsz.Open()
            Dim cmdcsz As New SqlCommand(sSQL, cncsz)
            cmdcsz.CommandType = CommandType.Text
            Dim drcsz As SqlDataReader = cmdcsz.ExecuteReader
            If drcsz.HasRows Then
                While drcsz.Read
                    Address = Trim(drcsz("City")) & ", " &
                    Trim(drcsz("State")) & " " & Trim(drcsz("Zip"))
                End While
            End If
            cncsz.Close()
            cncsz = Nothing
        Else
            Dim cncsz As New sqlConnection(connString)
            cncsz.Open()
            Dim cmdcsz As New sqlCommand(sSQL, cncsz)
            cmdcsz.CommandType = CommandType.Text
            Dim drcsz As sqlDataReader = cmdcsz.ExecuteReader
            If drcsz.HasRows Then
                While drcsz.Read
                    Address = Trim(drcsz("City")) & ", " &
                    Trim(drcsz("State")) & " " & Trim(drcsz("Zip"))
                End While
            End If
            cncsz.Close()
            cncsz = Nothing
        End If
        Return Address
    End Function

    Public Function GetAddressCountry(ByVal AddressID As Long) As String
        Dim Address As String = ""
        Dim sSQL As String = "Select * from Addresses where ID = " & AddressID
        If connString <> "" Then
            Dim cngac As New SqlConnection(connString)
            cngac.Open()
            Dim cmdgac As New SqlCommand(sSQL, cngac)
            cmdgac.CommandType = CommandType.Text
            Dim drgac As SqlDataReader = cmdgac.ExecuteReader
            If drgac.HasRows Then
                While drgac.Read
                    If drgac("Country") IsNot DBNull.Value Then Address = Trim(drgac("Country"))
                End While
            End If
            cngac.Close()
            cngac = Nothing
        Else
            Dim cngac As New sqlConnection(connString)
            cngac.Open()
            Dim cmdgac As New sqlCommand(sSQL, cngac)
            cmdgac.CommandType = CommandType.Text
            Dim drgac As sqlDataReader = cmdgac.ExecuteReader
            If drgac.HasRows Then
                While drgac.Read
                    If drgac("Country") IsNot DBNull.Value Then Address = Trim(drgac("Country"))
                End While
            End If
            cngac.Close()
            cngac = Nothing
        End If
        Return Address
    End Function

    Public Function GetAddress(ByVal AddressID As Long) As String
        Dim Address As String = ""
        Dim sSQL As String = "Select * from Addresses where ID = " & AddressID
        If connString <> "" Then
            Dim cnga As New SqlConnection(connString)
            cnga.Open()
            Dim cmdga As New SqlCommand(sSQL, cnga)
            cmdga.CommandType = CommandType.Text
            Dim drga As SqlDataReader = cmdga.ExecuteReader
            If drga.HasRows Then
                While drga.Read
                    If drga("Address2") IsNot DBNull.Value _
                    AndAlso Trim(drga("Address2")) <> "" Then
                        Address = Trim(drga("Address1")) & " " & Trim(drga("Address2")) & ", " &
                        Trim(drga("City")) & ", " & Trim(drga("State")) & " " & Trim(drga("Zip"))
                        If drga("Country") IsNot DBNull.Value Then Address += " " & Trim(drga("Country"))
                    Else
                        Address = Trim(drga("Address1")) & ", " & Trim(drga("City")) &
                        ", " & Trim(drga("State")) & " " & Trim(drga("Zip"))
                        If drga("Country") IsNot DBNull.Value Then Address += " " & Trim(drga("Country"))
                    End If
                End While
            End If
            cnga.Close()
            cnga = Nothing
        Else
            Dim cnga As New sqlConnection(connString)
            cnga.Open()
            Dim cmdga As New sqlCommand(sSQL, cnga)
            cmdga.CommandType = CommandType.Text
            Dim drga As sqlDataReader = cmdga.ExecuteReader
            If drga.HasRows Then
                While drga.Read
                    If drga("Address2") IsNot DBNull.Value _
                    AndAlso Trim(drga("Address2")) <> "" Then
                        Address = Trim(drga("Address1")) & " " & Trim(drga("Address2")) & ", " &
                        Trim(drga("City")) & ", " & Trim(drga("State")) & " " & Trim(drga("Zip"))
                        If drga("Country") IsNot DBNull.Value Then Address += " " & Trim(drga("Country"))
                    Else
                        Address = Trim(drga("Address1")) & ", " & Trim(drga("City")) &
                        ", " & Trim(drga("State")) & " " & Trim(drga("Zip"))
                        If drga("Country") IsNot DBNull.Value Then Address += " " & Trim(drga("Country"))
                    End If
                End While
            End If
            cnga.Close()
            cnga = Nothing
        End If
        Return Address
    End Function

    Public Function GetNextRefundID()
        Dim NextID As Long = 1
        Dim sSQL As String = "Select max(ID) as LastID from Refunds"
        Dim cnnai As New SqlConnection(connString)
        cnnai.Open()
        Dim cmdnai As New SqlCommand(sSQL, cnnai)
        cmdnai.CommandType = CommandType.Text
        Dim drnai As SqlDataReader = cmdnai.ExecuteReader
        If drnai.HasRows Then
            While drnai.Read
                If drnai("LastID") IsNot DBNull.Value _
                Then NextID = drnai("LastID") + 1
            End While
        End If
        cnnai.Close()
        cnnai = Nothing
        '
        Return NextID
    End Function

    Public Function GetNextProviderID()
        Dim NextID As Long = 1
        Dim sSQL As String = "Select max(ID) as LastID from Providers"
        If connString <> "" Then
            Dim cnnai As New SqlConnection(connString)
            cnnai.Open()
            Dim cmdnai As New SqlCommand(sSQL, cnnai)
            cmdnai.CommandType = CommandType.Text
            Dim drnai As SqlDataReader = cmdnai.ExecuteReader
            If drnai.HasRows Then
                While drnai.Read
                    If drnai("LastID") IsNot DBNull.Value _
                    Then NextID = drnai("LastID") + 1
                End While
            End If
            cnnai.Close()
            cnnai = Nothing
        Else
            Dim cnnai As New sqlConnection(connString)
            cnnai.Open()
            Dim cmdnai As New sqlCommand(sSQL, cnnai)
            cmdnai.CommandType = CommandType.Text
            Dim drnai As sqlDataReader = cmdnai.ExecuteReader
            If drnai.HasRows Then
                While drnai.Read
                    If drnai("LastID") IsNot DBNull.Value _
                    Then NextID = drnai("LastID") + 1
                End While
            End If
            cnnai.Close()
            cnnai = Nothing
        End If
        Return NextID
    End Function

    Private Function GetNextAddressID()
        Dim NextID As Long = 1
        Dim sSQL As String = "Select max(ID) as LastID from Addresses"
        If connString <> "" Then
            Dim cnnai As New SqlConnection(connString)
            cnnai.Open()
            Dim cmdnai As New SqlCommand(sSQL, cnnai)
            cmdnai.CommandType = CommandType.Text
            Dim drnai As SqlDataReader = cmdnai.ExecuteReader
            If drnai.HasRows Then
                While drnai.Read
                    If drnai("LastID") IsNot DBNull.Value _
                    Then NextID = drnai("LastID") + 1
                End While
            End If
            cnnai.Close()
            cnnai = Nothing
        Else
            Dim cnnai As New sqlConnection(connString)
            cnnai.Open()
            Dim cmdnai As New sqlCommand(sSQL, cnnai)
            cmdnai.CommandType = CommandType.Text
            Dim drnai As sqlDataReader = cmdnai.ExecuteReader
            If drnai.HasRows Then
                While drnai.Read
                    If drnai("LastID") IsNot DBNull.Value _
                    Then NextID = drnai("LastID") + 1
                End While
            End If
            cnnai.Close()
            cnnai = Nothing
        End If
        Return NextID
    End Function

    Public Function IsTGPUnique(ByVal TGPID As Integer) As Boolean
        Dim Uniq As Boolean = True
        Dim sSQL As String = "Select ID from Tests where ID = " & TGPID & " Union Select ID from Groups where ID = " & TGPID &
        " Union Select ID from Profiles where ID = " & TGPID & " Union Select ID from Departments where ID = " & TGPID
        Dim cncu As New SqlConnection(connString)
        cncu.Open()
        Dim cmdcu As New SqlCommand(sSQL, cncu)
        cmdcu.CommandType = CommandType.Text
        Dim drcu As SqlDataReader = cmdcu.ExecuteReader
        If drcu.HasRows Then Uniq = False
        cncu.Close()
        cncu = Nothing
        Return Uniq
    End Function

    Public Sub InitializeConfiguration(ByVal CompanyID As Long)
        Dim sSQL As String = "Select * from System_Config where Company_ID = " & CompanyID
        Dim cnic As New SqlConnection(connString)
        cnic.Open()
        Dim cmdic As New SqlCommand(sSQL, cnic)
        cmdic.CommandType = CommandType.Text
        Dim dric As SqlDataReader = cmdic.ExecuteReader
        If dric.HasRows Then
            While dric.Read
                SystemConfig.DiagTarget = dric("DiagTarget")
                SystemConfig.PatientPriceLevel = dric("PatientPriceLevel").ToString
                SystemConfig.Tax = dric("Tax").ToString
                SystemConfig.DueDays = dric("DueDays").ToString
                SystemConfig.Term = dric("Term").ToString
                SystemConfig.AdrLblFile = dric("AdrLblFile").ToString
                SystemConfig.InPhlebTGP = dric("InPhlebTGP")
                SystemConfig.HPhlebTGP = dric("HPhlebTGP")
                SystemConfig.CPhlebTGP = dric("CPhlebTGP")
                SystemConfig.ParInHouse = dric("ParInHouse")
                SystemConfig.RDMInterval = dric("RDMInterval")
                SystemConfig.CustomRPT = dric("CustomRPT")
                SystemConfig.SpecimenType = dric("Specimen_Type")
                SystemConfig.MarkSourceconnStringaint = dric("Mark_Source_Constraint")
                Try
                    SystemConfig.MergeSameDayAccession = dric("MergeSameDayAccession")
                Catch ex As Exception
                    SystemConfig.MergeSameDayAccession = False
                End Try

                Try
                    SystemConfig.P_inputOnLabel = dric("P_inputOnLabel")
                Catch ex As Exception
                    SystemConfig.P_inputOnLabel = False
                End Try

                Try
                    SystemConfig.ShowHorizontalAccIDonLabel = dric("ShowHorizontalAccIDonLabel")
                Catch ex As Exception
                    SystemConfig.ShowHorizontalAccIDonLabel = False
                End Try

                Try
                    SystemConfig.ReportableFormat = dric("ReportableFormat")
                Catch ex As Exception
                    SystemConfig.ReportableFormat = False
                End Try

                Try
                    SystemConfig.EQ_Results_BufferChk = dric("EQ_Results_BufferChk")
                Catch ex As Exception
                    SystemConfig.EQ_Results_BufferChk = False
                End Try

                SystemConfig.ProfileBreak = dric("ProfileBreak")
                SystemConfig.ReleaseWithEntry = dric("ReleaseWithEntry")
                SystemConfig.PanicSpan = dric("PanicSpan")
                SystemConfig.SendoutSpan = dric("SendoutSpan")
                Try
                    SystemConfig.AccLabel = CommonData.RetrieveColumnValue("LabelPrinters", "LabelFileName", "PC_Name", "'" & My.Computer.Name & "'", "")
                Catch ex As Exception
                    SystemConfig.AccLabel = ""
                End Try


                If SystemConfig.AccLabel = "" Then
                    If dric("AccLabel") IsNot DBNull.Value AndAlso dric("AccLabel") <> "" Then
                        SystemConfig.AccLabel = dric("AccLabel")
                    Else
                        SystemConfig.AccLabel = Application.StartupPath & "\Reports\Dymo30334Tst.Label"
                    End If
                End If

                If dric("AccSeed") IsNot DBNull.Value AndAlso dric("AccSeed") <> "" Then
                    SystemConfig.AccSeed = dric("AccSeed")
                Else
                    SystemConfig.AccSeed = ""
                End If
                SystemConfig.DefaultBilling = dric("DefaultBilling")
                'SystemConfig.PatConsolidate = Rs.Fields("PatConsolidate").Value
                If dric("GenericResults") IsNot DBNull.Value AndAlso dric("GenericResults") <> "" Then
                    SystemConfig.GenericResults = dric("GenericResults")
                Else
                    SystemConfig.GenericResults = Application.StartupPath & "\Reports\GenericResults.rpt"
                End If
                If dric("CustomResults") IsNot DBNull.Value AndAlso dric("CustomResults") <> "" Then
                    SystemConfig.CustomResults = dric("CustomResults")
                Else
                    SystemConfig.CustomResults = Application.StartupPath & "\Reports\CusResults.rpt"
                End If
                SystemConfig.FaxServer = dric("FaxServer")
                SystemConfig.FaxRetries = dric("FaxRetries")
                SystemConfig.FaxRetryDelay = dric("FaxRetryDelay")
                SystemConfig.ProlisSMTP = dric("ProlisSMTP")
                SystemConfig.ProlisEmail = dric("ProlisEmail")
                SystemConfig.ProlisEmailPort = dric("ProlisEmailPort")
                SystemConfig.ProlisEmailPWD = dric("ProlisPWD")
                SystemConfig.ProlisUseSSL = dric("ProlisUseSSL")
                SystemConfig.ORSMTP = dric("OutreachSMTP")
                SystemConfig.OREmail = dric("OutreachEmail")
                SystemConfig.OREmailPWD = dric("OutreachPWD")
                SystemConfig.OREmailPort = dric("OREmailPort")
                SystemConfig.ORUseSSL = dric("ORUseSSL")
                If dric("PhoneMask") IsNot DBNull.Value AndAlso dric("PhoneMask") <> "" Then
                    SystemConfig.PhoneMask = dric("PhoneMask")
                Else
                    SystemConfig.PhoneMask = ""
                End If
                SystemConfig.OutreachPub = dric("Outreach_Publish")
                SystemConfig.ProlisOnPub = dric("ProlisOn_Publish")
                SystemConfig.FaxPub = dric("Fax_Publish")
                SystemConfig.HL7AutoPub = dric("HL7AutoPub")
                SystemConfig.BillingIntegrate = dric("Billing_Integrate")
                SystemConfig.BARHistory = dric("BAR_HISTORY")
                SystemConfig.RESHistory = dric("RES_HISTORY")
                SystemConfig.CumRef = dric("Cumulative_Reflex")
                SystemConfig.Max_Instances = dric("Max_Instances")
                SystemConfig.AuditTrail = dric("AuditTrail")
                SystemConfig.DefSource = dric("Source")
                SystemConfig.DefTemp = dric("Temperature")
                If dric("DateFormat") IsNot DBNull.Value Then
                    SystemConfig.DateFormat = dric("DateFormat")
                Else
                    SystemConfig.DateFormat = "MM/dd/yyyy"
                End If
                If dric("Environment") IsNot DBNull.Value Then
                    SystemConfig.Environment = dric("Environment")
                Else
                    SystemConfig.Environment = "United States-English"
                End If
                If dric("Culture") IsNot DBNull.Value Then
                    SystemConfig.Culture = dric("Culture")
                Else
                    SystemConfig.Culture = "en-US"
                End If
            End While
        End If
        cnic.Close()
        cnic = Nothing
        '
        If SystemConfig.Culture <> "" Then
            System.Threading.Thread.CurrentThread.CurrentCulture = New CultureInfo(SystemConfig.Culture)
        Else
            System.Threading.Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
        End If
        DateMask = SystemConfig.DateFormat
        DateMask = Replace(DateMask, "d", "0")
        DateMask = Replace(DateMask, "M", "0")
        DateMask = Replace(DateMask, "y", "0")
        '
    End Sub

    Public Function UserEnteredText(ByVal TB As MaskedTextBox) As String
        Dim STR As String = ""
        Dim TMF As MaskFormat = TB.TextMaskFormat
        TB.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
        STR = TB.Text
        TB.TextMaskFormat = TMF
        Return STR
    End Function

    Public Function GetBillRequisits(ByVal PayerID As Long) As Long()
        Dim Requisits() As Long = {PayerID, 0, 0, 0, 0, 0, 0, 0, 0}
        '0=PayerID, 1=AddID, 2=NPI, 3=Policy, 4=Group, 5=Dx, 6=CPT, 7=Pointer, 8=Price
        Dim i As Integer = 1
        Dim sSQL As String = "Select Required from Payer_Requisit where Payer_ID = " & PayerID & " order by Requisit_ID"
        '
        Dim cnpr As New SqlConnection(connString)
        cnpr.Open()
        Dim cmdpr As New SqlCommand(sSQL, cnpr)
        cmdpr.CommandType = CommandType.Text
        Dim drpr As SqlDataReader = cmdpr.ExecuteReader
        If drpr.HasRows Then
            While drpr.Read
                If i <= UBound(Requisits) Then
                    Requisits(i) = Convert.ToInt16(drpr("Required"))
                    i += 1
                End If
            End While
        End If
        cnpr.Close()
        cnpr = Nothing
        Return Requisits
    End Function
    Public Structure QRSec
        Dim Token As String
        Dim DNS As String
        Dim QRChecked As Boolean
    End Structure

    Public Structure ColorAPIStruct
        Dim Token_BaseUrl As String
    End Structure
    Public Structure Laboratory
        Dim ID As Long
        Dim IsIndividual As Boolean

        Dim FedID As String
        Dim NPI As String
        Dim CLIA As String
        Dim License As String
        Dim MCR As String
        Dim LabName As String
        Dim Abbr As String
        Dim Address_ID As Long
        Dim Contact As String
        Dim Phone As String
        Dim Director As String
        Dim QRSec As QRSec
        Dim ColorAPIStruct As ColorAPIStruct

    End Structure

    Public Structure Payer
        Dim ID As Long
        Shared Name As String
        Shared PayerCode As String
    End Structure

    Public Structure SystemConfig
        Dim CompanyID As Integer
        Shared DiagTarget As String
        Shared PatientPriceLevel As Integer
        Shared Tax As Single
        Shared DueDays As Integer
        Shared Term As String
        Shared AdrLblFile As String
        Shared RDMInterval As Integer
        Shared CustomRPT As Boolean
        Shared SpecimenType As Integer
        Shared MarkSourceconnStringaint As Boolean
        Shared MergeSameDayAccession As Boolean
        Shared ProfileBreak As Boolean
        Shared ReleaseWithEntry As Boolean
        Shared PanicSpan As Integer
        Shared SendoutSpan As Integer
        Shared AccSeed As String
        Shared DefaultBilling As Integer
        Shared AccLabel As String
        Shared AuditTrail As Boolean
        Shared GenericResults As String
        Shared CustomResults As String
        Shared FaxServer As String
        Shared FaxRetries As Integer
        Shared FaxRetryDelay As Integer
        Shared ProlisSMTP As String
        Shared ProlisEmail As String
        Shared ProlisEmailPWD As String
        Shared ProlisEmailPort As String
        Shared ProlisUseSSL As Boolean
        Shared ORSMTP As String
        Shared OREmail As String
        Shared OREmailPWD As String
        Shared OREmailPort As String
        Shared ORUseSSL As Boolean
        Shared PhoneMask As String
        Shared OutreachPub As Boolean
        Shared ProlisOnPub As Boolean
        Shared FaxPub As Boolean
        Shared HL7AutoPub As Boolean
        Shared BillingIntegrate As Boolean
        Shared BARHistory As Boolean
        Shared RESHistory As Boolean
        Shared CumRef As Boolean
        Shared Max_Instances As Int16
        Shared DefSource As String
        Shared DefTemp As String
        Shared InPhlebTGP As String
        Shared HPhlebTGP As String
        Shared CPhlebTGP As String
        Shared ParInHouse As Boolean
        Shared DateFormat As String
        Shared Environment As String
        Shared Culture As String
        Shared P_inputOnLabel As Boolean
        Shared ShowHorizontalAccIDonLabel As Boolean
        Shared ReportableFormat As Boolean
        Shared EQ_Results_BufferChk As Boolean
    End Structure

    Public Structure ThisUser
        Dim User_Type_ID As Integer
        Shared ID As Long
        Shared LogoutMins As Integer
        Shared Name As String
        Shared UserName As String
        Shared Password As String
        Shared IsLoggedIn As Boolean
        Shared Change_PWD As Boolean
        Shared Cus_Svc As Boolean
        Shared Accession As Boolean
        Shared Analysis As Boolean
        Shared Result_Entry As Boolean
        Shared Result_Release As Boolean
        Shared QC_Layout As Boolean
        Shared Test_Mgmt As Boolean
        Shared Billing As Boolean
        Shared Report_Build As Boolean
        Shared Report_Process As Boolean
        Shared Hard_Deletion As Boolean
        Shared Soft_Deletion As Boolean
        Shared System_Config As Boolean
        Shared User_Mgmt As Boolean
        Shared Dictionary As Boolean
        Shared DicOnFly As Boolean
        Shared ARC As Boolean
        Shared Payment As Boolean
        Shared Pouring As Boolean
        Shared Testing As Boolean
        Shared Insurances As Boolean
        Shared Equips As Boolean
        Shared Interfaces As Boolean
        Shared Supervisor As Boolean
        Shared Director As Boolean
        Shared Owner As Boolean

        Shared PrinterPC As String
        Shared Property PrinterName As String = ""
        Shared Property UseRemotePrinter As Boolean = False
        Shared Property SpecificPrinter As String = ""
    End Structure

    Public Function DecryptIt(ByVal UserId As Long, ByVal txt As String) As String
        Dim i As Integer
        Dim NewTxt As String = ""
        Dim TCode As String = ""
        Dim IsEven As Boolean = (UserId Mod 2 = 0)
        Dim temptxt As String = txt
        If Not IsEven Then temptxt = Reverse(temptxt) 'Odd - Start Reverse
        For i = 0 To Len(temptxt) - 1
            If IsNumeric(temptxt.Chars(i)) Then
                TCode += temptxt.Chars(i)
                If TCode.Length = 2 Then
                    NewTxt += GetEquivalentChar(temptxt.Chars(i - 1) & temptxt.Chars(i))
                    TCode = ""
                End If
            Else
                If InStr("!@#$%^&*()", temptxt.Chars(i)) > 0 Then
                    NewTxt += GetEquivalentChar(temptxt.Chars(i))
                End If
            End If
        Next
        If IsEven Then NewTxt = Reverse(NewTxt)
        DecryptIt = NewTxt
    End Function

    Private Function GetEquivalentChar(ByVal OldStr As String) As String
        Dim NewStr As String = ""
        Select Case OldStr
            Case ")"
                NewStr = "1"
            Case "("
                NewStr = "2"
            Case "*"
                NewStr = "3"
            Case "&"
                NewStr = "4"
            Case "^"
                NewStr = "5"
            Case "%"
                NewStr = "6"
            Case "$"
                NewStr = "7"
            Case "#"
                NewStr = "8"
            Case "@"
                NewStr = "9"
            Case "!"
                NewStr = "0"
            Case "1"
                NewStr = ")"
            Case "2"
                NewStr = "("
            Case "3"
                NewStr = "*"
            Case "4"
                NewStr = "&"
            Case "5"
                NewStr = "^"
            Case "6"
                NewStr = "%"
            Case "7"
                NewStr = "$"
            Case "8"
                NewStr = "#"
            Case "9"
                NewStr = "@"
            Case "0"
                NewStr = "!"
            Case "a"
                NewStr = "52"
            Case "b"
                NewStr = "51"
            Case "c"
                NewStr = "50"
            Case "d"
                NewStr = "49"
            Case "e"
                NewStr = "48"
            Case "f"
                NewStr = "47"
            Case "g"
                NewStr = "46"
            Case "h"
                NewStr = "45"
            Case "i"
                NewStr = "44"
            Case "j"
                NewStr = "43"
            Case "k"
                NewStr = "42"
            Case "l"
                NewStr = "41"
            Case "m"
                NewStr = "40"
            Case "n"
                NewStr = "39"
            Case "o"
                NewStr = "38"
            Case "p"
                NewStr = "37"
            Case "q"
                NewStr = "36"
            Case "r"
                NewStr = "35"
            Case "s"
                NewStr = "34"
            Case "t"
                NewStr = "33"
            Case "u"
                NewStr = "32"
            Case "v"
                NewStr = "31"
            Case "w"
                NewStr = "30"
            Case "x"
                NewStr = "29"
            Case "y"
                NewStr = "28"
            Case "z"
                NewStr = "27"
            Case "A"
                NewStr = "26"
            Case "B"
                NewStr = "25"
            Case "C"
                NewStr = "24"
            Case "D"
                NewStr = "23"
            Case "E"
                NewStr = "22"
            Case "F"
                NewStr = "21"
            Case "G"
                NewStr = "20"
            Case "H"
                NewStr = "19"
            Case "I"
                NewStr = "18"
            Case "J"
                NewStr = "17"
            Case "K"
                NewStr = "16"
            Case "L"
                NewStr = "15"
            Case "M"
                NewStr = "14"
            Case "N"
                NewStr = "13"
            Case "O"
                NewStr = "12"
            Case "P"
                NewStr = "11"
            Case "Q"
                NewStr = "10"
            Case "R"
                NewStr = "09"
            Case "S"
                NewStr = "08"
            Case "T"
                NewStr = "07"
            Case "U"
                NewStr = "06"
            Case "V"
                NewStr = "05"
            Case "W"
                NewStr = "04"
            Case "X"
                NewStr = "03"
            Case "Y"
                NewStr = "02"
            Case "Z"
                NewStr = "01"
            Case "52"
                NewStr = "a"
            Case "51"
                NewStr = "b"
            Case "50"
                NewStr = "c"
            Case "49"
                NewStr = "d"
            Case "48"
                NewStr = "e"
            Case "47"
                NewStr = "f"
            Case "46"
                NewStr = "g"
            Case "45"
                NewStr = "h"
            Case "44"
                NewStr = "i"
            Case "43"
                NewStr = "j"
            Case "42"
                NewStr = "k"
            Case "41"
                NewStr = "l"
            Case "40"
                NewStr = "m"
            Case "39"
                NewStr = "n"
            Case "38"
                NewStr = "o"
            Case "37"
                NewStr = "p"
            Case "36"
                NewStr = "q"
            Case "35"
                NewStr = "r"
            Case "34"
                NewStr = "s"
            Case "33"
                NewStr = "t"
            Case "32"
                NewStr = "u"
            Case "31"
                NewStr = "v"
            Case "30"
                NewStr = "w"
            Case "29"
                NewStr = "x"
            Case "28"
                NewStr = "y"
            Case "27"
                NewStr = "z"
            Case "26"
                NewStr = "A"
            Case "25"
                NewStr = "B"
            Case "24"
                NewStr = "C"
            Case "23"
                NewStr = "D"
            Case "22"
                NewStr = "E"
            Case "21"
                NewStr = "F"
            Case "20"
                NewStr = "G"
            Case "19"
                NewStr = "H"
            Case "18"
                NewStr = "I"
            Case "17"
                NewStr = "J"
            Case "16"
                NewStr = "K"
            Case "15"
                NewStr = "L"
            Case "14"
                NewStr = "M"
            Case "13"
                NewStr = "N"
            Case "12"
                NewStr = "O"
            Case "11"
                NewStr = "P"
            Case "10"
                NewStr = "Q"
            Case "09"
                NewStr = "R"
            Case "08"
                NewStr = "S"
            Case "07"
                NewStr = "T"
            Case "06"
                NewStr = "U"
            Case "05"
                NewStr = "V"
            Case "04"
                NewStr = "W"
            Case "03"
                NewStr = "X"
            Case "02"
                NewStr = "Y"
            Case "01"
                NewStr = "Z"
        End Select
        Return NewStr
    End Function

    Public Function IsPWDok(ByVal txt As String) As Boolean
        Dim GoodPWD As Boolean = True
        Dim txtLength As Integer = Len(txt)
        If Not txt Is Nothing And txtLength >= 3 Then
            Dim i As Integer
            Dim Badtxt As String = "!@#$%^&*()"
            For i = 0 To Len(Badtxt) - 1
                If InStr(txt, Badtxt.Chars(i)) > 0 Then
                    GoodPWD = False
                    Exit For
                End If
            Next
        Else
            GoodPWD = False
        End If
        IsPWDok = GoodPWD
    End Function

    Public Function EncryptIt(ByVal UserID As Long, ByVal txt As String) As String
        Dim IsEven As Boolean = (UserID Mod 2 = 0)
        Dim temptxt As String = txt
        If IsEven Then temptxt = Reverse(temptxt) 'Even = Start-Reverse
        temptxt = Replace(temptxt, "1", ")")
        temptxt = Replace(temptxt, "2", "(")
        temptxt = Replace(temptxt, "3", "*")
        temptxt = Replace(temptxt, "4", "&")
        temptxt = Replace(temptxt, "5", "^")
        temptxt = Replace(temptxt, "6", "%")
        temptxt = Replace(temptxt, "7", "$")
        temptxt = Replace(temptxt, "8", "#")
        temptxt = Replace(temptxt, "9", "@")
        temptxt = Replace(temptxt, "0", "!")
        '
        'For i = 0 To Len(temptxt) - 1

        'Next
        temptxt = Replace(temptxt, "a", "52")
        temptxt = Replace(temptxt, "b", "51")
        temptxt = Replace(temptxt, "c", "50")
        temptxt = Replace(temptxt, "d", "49")
        temptxt = Replace(temptxt, "e", "48")
        temptxt = Replace(temptxt, "f", "47")
        temptxt = Replace(temptxt, "g", "46")
        temptxt = Replace(temptxt, "h", "45")
        temptxt = Replace(temptxt, "i", "44")
        temptxt = Replace(temptxt, "j", "43")
        temptxt = Replace(temptxt, "k", "42")
        temptxt = Replace(temptxt, "l", "41")
        temptxt = Replace(temptxt, "m", "40")
        temptxt = Replace(temptxt, "n", "39")
        temptxt = Replace(temptxt, "o", "38")
        temptxt = Replace(temptxt, "p", "37")
        temptxt = Replace(temptxt, "q", "36")
        temptxt = Replace(temptxt, "r", "35")
        temptxt = Replace(temptxt, "s", "34")
        temptxt = Replace(temptxt, "t", "33")
        temptxt = Replace(temptxt, "u", "32")
        temptxt = Replace(temptxt, "v", "31")
        temptxt = Replace(temptxt, "w", "30")
        temptxt = Replace(temptxt, "x", "29")
        temptxt = Replace(temptxt, "y", "28")
        temptxt = Replace(temptxt, "z", "27")
        temptxt = Replace(temptxt, "A", "26")
        temptxt = Replace(temptxt, "B", "25")
        temptxt = Replace(temptxt, "C", "24")
        temptxt = Replace(temptxt, "D", "23")
        temptxt = Replace(temptxt, "E", "22")
        temptxt = Replace(temptxt, "F", "21")
        temptxt = Replace(temptxt, "G", "20")
        temptxt = Replace(temptxt, "H", "19")
        temptxt = Replace(temptxt, "I", "18")
        temptxt = Replace(temptxt, "J", "17")
        temptxt = Replace(temptxt, "K", "16")
        temptxt = Replace(temptxt, "L", "15")
        temptxt = Replace(temptxt, "M", "14")
        temptxt = Replace(temptxt, "N", "13")
        temptxt = Replace(temptxt, "O", "12")
        temptxt = Replace(temptxt, "P", "11")
        temptxt = Replace(temptxt, "Q", "10")
        temptxt = Replace(temptxt, "R", "09")
        temptxt = Replace(temptxt, "S", "08")
        temptxt = Replace(temptxt, "T", "07")
        temptxt = Replace(temptxt, "U", "06")
        temptxt = Replace(temptxt, "V", "05")
        temptxt = Replace(temptxt, "W", "04")
        temptxt = Replace(temptxt, "X", "03")
        temptxt = Replace(temptxt, "Y", "02")
        temptxt = Replace(temptxt, "Z", "01")
        '
        If IsEven Then
            EncryptIt = temptxt
        Else                                'Odd - EndReverse
            EncryptIt = Reverse(temptxt)
        End If
    End Function

    Private Function Reverse(ByVal txt As String) As String
        Dim txtLength As Long = Len(txt)
        Dim l As Long
        Dim sChar As String
        Dim sAns As String = ""
        For l = txtLength To 1 Step -1
            sChar = Mid(txt, l, 1)
            sAns = sAns & sChar
        Next
        Reverse = sAns
    End Function

    Public Function SSNNeat(ByVal SSN As String)
        SSN = Replace(SSN, "-", "")
        SSN = Replace(SSN, "_", "")
        SSN = Replace(SSN, ".", "")
        SSN = Replace(SSN, ",", "")
        SSN = Replace(SSN, "/", "")
        SSN = Replace(SSN, "\", "")
        SSNNeat = SSN
    End Function

    Public Sub ConfigureDateTimePicker(dtp As DateTimePicker)
        dtp.MinDate = New DateTime(2000, 1, 1)
        dtp.MaxDate = DateAdd("yyyy", 1, Now)
        dtp.Format = DateTimePickerFormat.Custom
        dtp.CustomFormat = Format(Date.Today, SystemConfig.DateFormat)
    End Sub

    Public Sub ClearDateTimePicker(dtp As DateTimePicker)
        ' Clear the date by setting the Value to DateTime.MinValue and the format to custom
        dtp.Value = Date.Today
        dtp.Format = DateTimePickerFormat.Custom
        dtp.CustomFormat = " "  ' Clear date placeholder
    End Sub

    Public Sub CloseUpDateTimePicker(dtp As DateTimePicker)
        If dtp.Value <> DateTime.MinValue Then
            dtp.Format = DateTimePickerFormat.Short  ' Or another format
            dtp.CustomFormat = Format(Date.Today, SystemConfig.DateFormat)          ' Adjust the format
        End If
    End Sub
    'Public Function EncryptIt(ByVal txt As String) As String
    'Dim RSA As New RSACryptoServiceProvider()
    'Dim Encrypted As Byte() = RSA.Encrypt(System.Text.ASCIIEncoding.UTF8.GetBytes(Trim(txt)), False)
    '    EncryptIt = System.Text.ASCIIEncoding.UTF8.GetString(Encrypted)
    'End Function
    'Public Function DecryptIt(ByVal txt As String) As String
    ' Dim RSA As New RSACryptoServiceProvider()
    ' Dim Encrypted As Byte() = RSA.Decrypt(System.Text.ASCIIEncoding.UTF8.GetBytes(Trim(txt)), False)
    '     DecryptIt = System.Text.ASCIIEncoding.UTF8.GetString(Encrypted)
    ' End Function
    'Public Function EncryptIt(ByVal PlainText As String) As String
    'Dim RC2 As New RC2CryptoServiceProvider()
    ' Get the key and IV.
    'Dim key As Byte() = RC2.Key
    'Dim IV As Byte() = RC2.IV
    '' Get an encryptor.
    'Dim encryptor As ICryptoTransform = RC2.CreateEncryptor(key, IV)
    ' Encrypt the data as an array of encrypted bytes in memory.
    'Dim msEncrypt As New MemoryStream()
    'Dim csEncrypt As New CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)
    ' Convert the data to a byte array.
    'Dim toEncrypt As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(PlainText)
    ' Write all data to the crypto stream and flush it.
    '    csEncrypt.Write(toEncrypt, 0, toEncrypt.Length)
    '    csEncrypt.FlushFinalBlock()

    ' Get the encrypted array of bytes.
    'Dim encrypted As Byte() = msEncrypt.ToArray()

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' This is where the data could be transmitted or saved.          
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '    EncryptIt = System.Text.ASCIIEncoding.ASCII.GetString(encrypted)
    'End Function
    'Public Function DecryptIt(ByVal Encrypted As String) As String
    ' Dim RC2 As New RC2CryptoServiceProvider()
    ' Get the key and IV.
    'Dim key As Byte() = RC2.Key
    'Dim IV As Byte() = RC2.IV
    'Get a decryptor that uses the same key and IV as the encryptor.
    'Dim decryptor As ICryptoTransform = RC2.CreateDecryptor(key, IV)
    ' Now decrypt the previously encrypted message using the decryptor
    ' obtained in the above step.
    'Dim ToDecrypt As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(Encrypted)
    'Dim msDecrypt As New MemoryStream(ToDecrypt)
    'Dim csDecrypt As New CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)
    ' Read the decrypted bytes from the decrypting stream
    ' and place them in a StringBuilder class.
    'Dim roundtrip As New System.Text.StringBuilder()
    'Dim b As Integer = 0
    '    Do
    '        b = csDecrypt.ReadByte()
    '        If b <> -1 Then
    '            roundtrip.Append(ChrW(b))
    '        End If
    '    Loop While b <> -1
    '' Save or send 
    '    DecryptIt = roundtrip.ToString
    'End Function

    'Public Function DecryptIt(ByVal CypherText As String) As String
    ' Dim Key As New DESCryptoServiceProvider()
    ' Create a memory stream to the passed buffer.
    'Dim ms As New MemoryStream(System.Text.ASCIIEncoding.UTF8.GetBytes(CypherText))
    ' Create a CryptoStream using the memory stream and the 
    ' CSP DES key. 
    'Dim encStream As New CryptoStream(ms, Key.CreateDecryptor(), CryptoStreamMode.Read)
    ' Create a StreamReader for reading the stream.
    'Dim Decrypted As Byte() = System.Text.ASCIIEncoding.UTF8.GetString(encStream)
    'Dim sr As New StreamReader(encStream)
    ' Read the stream as a string.
    'Dim val As String = sr.ReadLine()
    ' Close the streams.
    '   sr.Close()
    '  encStream.Close()
    '    ms.Close()
    '    Return val
    'End Function 'Decrypt

    Public Function IsRtf(text As String) As Boolean
        ' Check if the text starts with "{\rtf"
        Return text.TrimStart().StartsWith("{\rtf")
    End Function

    'Public Function ConvertPlainTextToRtf(plainText As String) As String
    '    ' Escape special characters for RTF
    '    plainText = plainText.Replace("\", "\\").Replace("{", "\{").Replace("}", "\}")

    '    ' Wrap the text with basic RTF syntax
    '    Dim rtfHeader As String = "{\rtf1\ansi\deff0{\fonttbl{\f0\fswiss Arial;}}\f0\fs20 "
    '    Dim rtfFooter As String = "}"

    '    ' Combine the header, escaped text, and footer
    '    Return rtfHeader & plainText & rtfFooter
    'End Function

    Public Function ConvertPlainTextToRtf(plainText As String) As String
        ' Escape special characters for RTF
        plainText = plainText.Replace("\", "\\").Replace("{", "\{").Replace("}", "\}")

        ' Replace line breaks in plain text with RTF line breaks
        plainText = plainText.Replace(vbCrLf, "\par ").Replace(vbLf, "\par ")

        ' Add indentation to each line if needed
        plainText = plainText.Replace(vbTab, "\tab ")

        ' Wrap the text with basic RTF syntax
        Dim rtfHeader As String = "{\rtf1\ansi\deff0{\fonttbl{\f0\fswiss Arial;}}\f0\fs20 "
        Dim rtfFooter As String = "}"

        ' Combine the header, escaped text, and footer
        Return rtfHeader & plainText & rtfFooter
    End Function

    Sub ConvertAndUpdateRTF()

        Using conn As New SqlConnection(connString)
            conn.Open()

            ' Select rows with RTF data
            Dim selectCmd As New SqlCommand("SELECT ID, ResultNote FROM Tests WHERE ResultNote IS NOT NULL AND LEN(ResultNote)>0", conn)
            Dim reader As SqlDataReader = selectCmd.ExecuteReader()

            Dim updates As New List(Of String)

            While reader.Read()
                Dim id As Integer = reader.GetInt32(0)
                Dim rtfText As String = reader.GetString(1)

                If IsRtf(rtfText) Then

                    ' Convert RTF to plain text
                    Dim plainText As String = RTF_To_Text(rtfText)

                    ' Store the update command
                    updates.Add($"UPDATE Tests SET ResultNote = '{plainText.Replace("'", "''")}' WHERE ID = {id}")
                End If
            End While
            reader.Close()

            ' Execute all update commands
            For Each updateQuery As String In updates
                Dim updateCmd As New SqlCommand(updateQuery, conn)
                updateCmd.ExecuteNonQuery()
            Next
        End Using
    End Sub

    Public Function GetEventID(ByRef EventType As String, ByRef EventName As String, ByRef EventName2 As String) As Long
        Dim EventID As Long = 0
        Dim sSQL As String = $"SELECT top 1 ID FROM Events WHERE Event_Type = '{EventType}' and Event_Name LIKE '%{EventName}%' and Event_Name LIKE '%{EventName2}%'
"
        Dim cnpr As New SqlConnection(connString)
        cnpr.Open()
        Dim cmdpr As New SqlCommand(sSQL, cnpr)
        cmdpr.CommandType = CommandType.Text
        Dim drpr As SqlDataReader = cmdpr.ExecuteReader
        If drpr.HasRows Then
            While drpr.Read
                EventID = drpr("ID")
            End While
        End If
        cnpr.Close()
        cnpr = Nothing
        Return EventID
    End Function
    'Public Function GetReportPath(fileName As String) As String
    '    ' Path to the "Custom Reports" folder
    '    Dim customReportsPath As String = Path.Combine(Application.StartupPath, "Reports", "Custom Reports")

    '    ' Check if the file exists in "Custom Reports" folder
    '    Dim customReportFile As String = Path.Combine(customReportsPath, fileName)
    '    If File.Exists(customReportFile) Then
    '        ' If the file exists in "Custom Reports" folder, return its path
    '        Return customReportFile
    '    Else
    '        ' If the file doesn't exist in "Custom Reports" folder,
    '        ' fallback to the main "Reports" folder
    '        Dim reportsPath As String = Path.Combine(Application.StartupPath, "Reports")
    '        Dim reportFile As String = Path.Combine(reportsPath, fileName)

    '        ' Check if the file exists in the main "Reports" folder
    '        If File.Exists(reportFile) Then
    '            ' If the file exists in the main "Reports" folder, return its path
    '            Return reportFile
    '        Else
    '            ' If the file doesn't exist in both "Custom Reports" and main "Reports" folders, return an empty string or handle as needed
    '            Return String.Empty
    '        End If
    '    End If
    'End Function
End Module
