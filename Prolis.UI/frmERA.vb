Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class frmERA
    Dim EOB() As String = {"", "", "", "", "", "", "", ""}
    Dim EOBInfo() As String = {"", "", "", "", "", "", "", ""}
    '0=DocNo, 1=Payer, 2=PayerCode, 3=Date, 4=Billed, 5=Paid, 6=applied, 7=Refunds
    Private Commons As String = ""
    Private Delim As String
    Private Subs() As String = {""}
    Private ReversedInvoices() As String = {""}
    'Billed = 3,
    '5 = Paid
    '6 = PR
    '7 = WO
    '8=Bal
    'dgvClaims
    Public Shared dgvClaimsBilled3 As Integer = 3
    Public Shared dgvClaimsPaid5 As Integer = 3
    Public Shared dgvClaimsPR6 As Integer = 3
    Public Shared dgvClaimsWO7 As Integer = 3
    Public Shared dgvClaimsBal8 As Integer = 3
    Public Function IsValidFileExtension(filePath As String) As Boolean
        Dim validExtensions As String() = {".dat", ".clm", ".txt", ".835", ".837"}
        Dim fileExtension As String = Path.GetExtension(filePath).ToLower()

        Return validExtensions.Contains(fileExtension)
    End Function
    Function GetDocumentIdFrom835(filePath As String) As String
        Dim documentId As String = String.Empty

        ' Read all lines from the 835 file
        Dim lines() As String = File.ReadAllLines(filePath)

        ' Loop through each line to find the BPR segment
        For Each line As String In lines
            If line.StartsWith("TRN") Then
                ' Split the BPR segment into elements
                Dim elements() As String = line.Split("*"c)
                If elements.Length > 3 Then
                    ' Assuming the document ID is the 3rd element (index 2)
                    documentId = elements(2)
                    Exit For
                End If
            End If
        Next

        Return documentId
    End Function
    Public Function CreateERAFolders() As (ERAsFolder As String, UnprocessedFolder As String, ProcessedFolder As String)
        ' Get the path to the current user's Documents folder
        Dim documentsPath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

        ' Define full folder paths
        Dim erasFolderPath As String = Path.Combine(documentsPath, "ERAs")
        Dim unprocessedPath As String = Path.Combine(erasFolderPath, "Unprocessed")
        Dim processedPath As String = Path.Combine(erasFolderPath, "Processed")

        ' Create folders if they do not exist
        If Not Directory.Exists(erasFolderPath) Then
            Directory.CreateDirectory(erasFolderPath)
        End If

        If Not Directory.Exists(unprocessedPath) Then
            Directory.CreateDirectory(unprocessedPath)
        End If

        If Not Directory.Exists(processedPath) Then
            Directory.CreateDirectory(processedPath)
        End If

        ' Return the paths as a tuple
        Return (erasFolderPath, unprocessedPath, processedPath)
    End Function
    Private Sub btnERALookUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnERALookUp.Click
        Dim pths = CreateERAFolders()
        OpenFileDialog1.InitialDirectory = pths.UnprocessedFolder
        If vbOK = OpenFileDialog1.ShowDialog Then
            txtERAFile.Text = OpenFileDialog1.FileName
            ReDim EOBInfo(7)
            EOBInfo(0) = Microsoft.VisualBasic.Mid(txtERAFile.Text,
            InStrRev(txtERAFile.Text, "\") + 1)
            EOBInfo(2) = Microsoft.VisualBasic.Mid(txtERAFile.Text,
            1, InStrRev(txtERAFile.Text, "\"))
            EOBInfo(2) = Replace(EOBInfo(2), "Unprocessed", "Processed")
            If Not IsValidFileExtension(txtERAFile.Text) Then
                Return

            End If
            Dim documentIe As String = GetDocumentIdFrom835(txtERAFile.Text)
            documentID.Text = documentIe
            Dim Msg As String = OpenERAFile(txtERAFile.Text)
            Try
                If Msg <> "" Then
                    Dim rs = PopulateEOBs(Msg)
                    If rs = "1" Then
                        MoveFileToFolder(txtERAFile.Text, pths.ProcessedFolder)
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
    Public Sub MoveFileToFolder(filePath As String, destinationFolder As String)
        ' Check if file exists
        If Not File.Exists(filePath) Then
            Throw New FileNotFoundException("The specified file does not exist.", filePath)
        End If

        ' Create the destination folder if it doesn't exist
        If Not Directory.Exists(destinationFolder) Then
            Directory.CreateDirectory(destinationFolder)
        End If

        ' Get the file name from the path
        Dim fileName As String = Path.GetFileName(filePath)

        ' Combine destination path
        Dim destinationPath As String = Path.Combine(destinationFolder, fileName)

        ' Move the file
        File.Move(filePath, destinationPath)


    End Sub
    Private Function OpenERAFile(ByVal FileName As String) As String
        txtERAFile.Text = FileName
        Dim Msg As String = ""
        Try
            Dim SR As New IO.StreamReader(FileName)
            Msg = SR.ReadToEnd
            SR.Close()
            SR = Nothing
            'txtERAFile.text = FileName
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
        Return Msg
    End Function

    Private Function GetPartnerName(ByVal PartInfo As String) As String
        Dim Data() As String = Split(PartInfo, "|")
        Dim PartName As String = ""
        Dim sSQL As String = "Select Name from Partners where Submitter = '" & Data(0) & "' and " & "Receiver = '" & Data(1) & "'"
        Dim cnpn As New SqlClient.SqlConnection(connString)
        cnpn.Open()
        Dim cmdpn As New SqlClient.SqlCommand(sSQL, cnpn)
        cmdpn.CommandType = CommandType.Text
        Dim drpn As SqlClient.SqlDataReader = cmdpn.ExecuteReader
        If drpn.HasRows Then
            While drpn.Read
                PartName = Trim(drpn("Name"))
            End While
        End If
        cnpn.Close()
        cnpn = Nothing
        Return PartName
    End Function

    Private Function PopulateEOBs(ByVal Msg As String) As String
        Dim rs = ""
        Try
            If Msg.Contains("*") Then
                Delim = "*"
            Else
                Delim = "|"
            End If
            Dim MyMsg As String = Msg
            Dim Segs() As String
            Dim Fields() As String
            Dim i As Integer
            '
            For e As Integer = 0 To EOB.Length - 1
                EOB(e) = ""
            Next
            'For e As Integer = 0 To EOBInfo.Length - 1
            '    EOBInfo(e) = ""
            'Next
            'ReDim EOB(7)
            '0=DocNo, 1=Payer, 2=PayerCode, 3=Date, 4=Billed, 5=Paid, 6=applied, 7=Refunds
            Dim DocNo As String = ""
            Dim PayerInfo() As String = {"", ""}    '0=id, 1=Name
            Dim PayerID As String = ""
            Dim FederalID As String = ""
            Dim EOBAmt As Double = 0
            Dim DocDate As String = ""
            Dim ClaimIDs As String = ""
            '
            Dim ClaimID As String = ""
            If MyMsg <> "" Then
                dgvEOBs.Rows.Clear() : dgvClaims.Rows.Clear() : dgvClaimDetail.Rows.Clear()
                Segs = Split(MyMsg, "~")
                For i = 0 To Segs.Length - 1
                    Segs(i) = Replace(Segs(i), Chr(10), "")
                    Segs(i) = Replace(Segs(i), Chr(13), "")
                    If Segs(i) IsNot Nothing AndAlso Segs(i) <> "" Then
                        Dim hh = InStr(Segs(i), Delim)

                        Dim ddd = ""
                        If hh = 0 Then
                            ddd = Segs(i).Substring(0, hh)
                        Else
                            ddd = Segs(i).Substring(0, hh - 1)
                        End If

                        If ddd = "BPR" Then  'BPR
                            'BPR*H*0*C*NON************20151215
                            'BPR*I*1937.14*C*ACH*CCP*01*071000152*DA*99643*1361236610**01*071926731*DA*0300001500*20121206
                            Fields = Split(Segs(i), Delim)
                            If Fields(4).StartsWith("NON") Then
                                txtERAType.Text = "Informational"
                            Else
                                txtERAType.Text = "Transactional"
                            End If
                            '0=EOB, 1=Originator, 2=EOBPath, 3=EOBDate, 4=IsComposit, 5=EOBType, 6=EOBAmount
                            If Fields(3) = "C" Then
                                EOB(5) = Fields(2)  'Payment Amount
                            Else
                                EOB(5) = ""
                            End If
                            EOBInfo(5) = EOB(5) 'paid
                            EOB(3) = dtpPost.Value.ToString(SystemConfig.DateFormat)   'applydate
                            'DocDate = DateTime.ParseExact(Fields(16), "yyyyMMdd", _
                            'CultureInfo.InvariantCulture).ToString(SystemConfig.DateFormat)
                            'EOB(3) = DocDate
                        ElseIf ddd = "TRN" Then  'TRN
                            'TRN*1*151215190043066*1591031071
                            'TRN*1*C12338E28880020*1361236610*CP20121203E288800200
                            Fields = Split(Segs(i), Delim)
                            If Fields(3).Length = 10 Then FederalID = Fields(3).Substring(1)
                            If Fields(2).Contains(" ") Then
                                EOB(0) = Fields(2).Substring(0, InStr(Fields(2), " ") - 1)
                            ElseIf Fields(2).Contains("NO-PAY") Then
                                EOB(0) = Replace(Fields(2), "NO-PAY-", "")  'Check #
                            Else
                                EOB(0) = Fields(2)
                            End If
                            If PayerInfo(0) <> "" Then EOB(2) = PayerInfo(0)
                            If PayerInfo(1) <> "" Then EOB(1) = PayerInfo(1)
                        ElseIf ddd = "N1" Then  'N1 for Payer
                            'N1*PR*BLUECROSS BLUESHIELD OF ILLINOIS
                            If PayerInfo(1) = "" Then
                                Fields = Split(Segs(i), Delim)
                                If Fields(1) = "PR" Then    'Payer
                                    If EOB(1) = "" Then EOB(1) = Fields(2)
                                End If
                            End If
                        ElseIf ddd = "REF" Then  'N1 for Payer
                            'REF*2U*MR042
                            If PayerInfo(1) = "" Then
                                Fields = Split(Segs(i), Delim)
                                If Fields(1) = "2U" Then    'Payer code
                                    'PayerInfo = ValidatePayerCode(Fields(2))
                                    If PayerInfo(0) <> "" Then EOB(2) = PayerInfo(0)
                                    If PayerInfo(1) <> "" Then EOB(1) = PayerInfo(1)
                                End If
                            End If
                        ElseIf ddd = "TS3" Then
                            'TS3*1174529754*81*20171231*206*2772.57
                            Fields = Split(Segs(i), Delim)
                            EOB(4) = Fields(5)
                        ElseIf ddd = "CLP" Then
                            'CLP*19580*1*3000*0**16*VRG1801742300*81*1~
                            'CLP|001-28989|1|108|24.86||MC|1116583479~
                            Fields = Split(Segs(i), Delim)
                            If PayerInfo(1) = "" Then
                                If Fields(1).Contains("-") Then
                                    Fields(1) = Fields(1).Substring(InStr(Fields(1), "-"))
                                End If
                                PayerInfo = PayerInfoWithInvoice(Fields(1))
                                If PayerInfo(0) <> "" And PayerInfo(1) <> "" Then
                                    EOB(2) = PayerInfo(0)
                                    EOB(1) = PayerInfo(1)
                                ElseIf FederalID <> "" Then
                                    PayerInfo = GetPayerInfoWithTID(FederalID)
                                    If PayerInfo(0) <> "" And PayerInfo(1) <> "" Then
                                        EOB(2) = PayerInfo(0)
                                        EOB(1) = PayerInfo(1)
                                    End If
                                End If
                            End If
                            If (EOB(0) <> "" And EOB(1) <> "" And EOB(2) _
                            <> "" And EOB(3) <> "" And EOB(5) <> "") Then
                                If Not EOBProcessed(EOB(0), EOB(1)) Then
                                    If EOB(4) = Nothing Then EOB(4) = ""
                                    If Not EOBListed(EOB(0), EOB(1)) Then
                                        dgvEOBs.Rows.Add(EOB(2), EOB(1), EOB(0), EOB(3), EOB(4), EOB(5))
                                        EOB(0) = "" : EOB(1) = ""
                                    End If
                                Else
                                    rs = 1
                                    MsgBox("EOB already processed")
                                End If
                            Else
                                MsgBox("Prolis unable to locate the ERA payer. Neither the Claim referenced in the " &
                                "ERA, exists in Prolis, nor the payer record in Prolis, possesses the Federal ID.")
                            End If
                            Exit For
                        End If
                    End If
                Next
            End If
        Catch Ex As Exception
            MsgBox("System encountered an error '" & Ex.Message &
            "' with this EOB.", MsgBoxStyle.Critical, "Prolis")
            btnClear_Click(Nothing, Nothing)
        End Try
        Return rs
    End Function

    Private Function GetPayerInfoWithTID(ByVal EIN As String) As String()
        Dim info() As String = {"", ""}
        Dim cntid As New SqlClient.SqlConnection(connString)
        cntid.Open()
        Dim cmdtid As New SqlClient.SqlCommand("Select * " &
        "from Payers where FederalID = '" & EIN & "'", cntid)
        cmdtid.CommandType = CommandType.Text
        Dim drtid As SqlClient.SqlDataReader = cmdtid.ExecuteReader
        If drtid.HasRows Then
            While drtid.Read
                info(0) = drtid("ID").ToString
                info(1) = drtid("PayerName")
            End While
        End If
        cntid.Close()
        cntid = Nothing
        Return info
    End Function

    Private Function ValidatePayerCode(ByVal PayerCode As String) As String()
        Dim Info() As String = {"", ""}
        Dim cnvpc As New SqlClient.SqlConnection(connString)
        cnvpc.Open()
        Dim cmdvpc As New SqlClient.SqlCommand("Select * from " &
        "Payers where PayerCode = '" & PayerCode & "'", cnvpc)
        cmdvpc.CommandType = CommandType.Text
        Dim drvpc As SqlClient.SqlDataReader = cmdvpc.ExecuteReader
        If drvpc.HasRows Then
            While drvpc.Read
                Info(0) = drvpc("ID").ToString
                Info(1) = drvpc("PayerName")
            End While
        End If
        cnvpc.Close()
        cnvpc = Nothing
        Return Info
    End Function

    Private Function PayerInfoWithInvoice(ByVal InvoiceID As String) As String()
        Dim PayerInfo() As String = {"", ""}
        Dim sSQL As String = "Select c.ID, c.PayerName from Payers c inner join " &
        "(Req_Coverage a inner join Charges b on b.Accession_ID = a.Accession_ID) " &
        "on a.Payer_ID = c.ID where a.Preference = 'P' and b.ID = '" & InvoiceID & "'"
        Dim cnpc As New SqlClient.SqlConnection(connString)
        cnpc.Open()
        Dim cmdpc As New SqlClient.SqlCommand(sSQL, cnpc)
        cmdpc.CommandType = CommandType.Text
        Dim drpc As SqlClient.SqlDataReader = cmdpc.ExecuteReader
        If drpc.HasRows Then
            While drpc.Read
                PayerInfo(0) = drpc("ID").ToString
                PayerInfo(1) = drpc("PayerName")
            End While
        End If
        cnpc.Close()
        cnpc = Nothing
        Return PayerInfo
    End Function

    Private Function EOBListed(ByVal DocNo As String, ByVal PayerName As String) As Boolean
        Dim Listed As Boolean = False
        For i As Integer = 0 To dgvEOBs.RowCount - 1
            If dgvEOBs.Rows(i).Cells(2).Value = DocNo And dgvEOBs.Rows(i).Cells(1).Value = PayerName Then
                Listed = True
                Exit For
            End If
        Next
        Return Listed
    End Function

    Private Function GetPayerInfo(ByVal ReceiverID As String) As String()
        Dim Info() As String = {"", ""}
        If ReceiverID <> "" Then
            Dim sSQL As String = "Select * from Payers where NPI = '" & ReceiverID & "'"
            Dim cnpi As New SqlClient.SqlConnection(connString)
            cnpi.Open()
            Dim cmdpi As New SqlClient.SqlCommand(sSQL, cnpi)
            cmdpi.CommandType = CommandType.Text
            Dim drpi As SqlClient.SqlDataReader = cmdpi.ExecuteReader
            If drpi.HasRows Then
                While drpi.Read
                    Info(0) = drpi("ID").ToString
                    Info(1) = drpi("PayerName")
                End While
            End If
            cnpi.Close()
            cnpi = Nothing
        End If
        Return Info
    End Function

    Private Function EOBProcessed(ByVal DocNo As String, ByVal PayerName As String) As Boolean
        Dim Procd As Boolean = False
        Dim sSQL As String = "Select * from EOBs where DocNo = '" & DocNo & "' and Payer = '" & PayerName & "'"
        If connString <> "" Then
            Dim cnep As New SqlClient.SqlConnection(connString)
            cnep.Open()
            Dim cmdep As New SqlClient.SqlCommand(sSQL, cnep)
            cmdep.CommandType = CommandType.Text
            Dim drep As SqlClient.SqlDataReader = cmdep.ExecuteReader
            If drep.HasRows Then Procd = True
            cnep.Close()
            cnep = Nothing
            'Else
            '    Dim cnep As New Odbc.OdbcConnection(odbCS)
            '    cnep.Open()
            '    Dim cmdep As New Odbc.OdbcCommand(sSQL, cnep)
            '    cmdep.CommandType = CommandType.Text
            '    Dim drep As Odbc.OdbcDataReader = cmdep.ExecuteReader
            '    If drep.HasRows Then Procd = True
            '    cnep.Close()
            '    cnep = Nothing
        End If
        Return Procd
    End Function

    Private Function ClaimProcessed(ByVal DocNo As String, ByVal ChargeID As Long) As Boolean
        Dim Procd As Boolean = False
        Dim sSQL As String = "Select * from Payments where ArType = 1 and DocNo = '" & DocNo &
        "' and ID in (Select Payment_ID from Payment_Detail where Charge_ID = " & ChargeID & ")"
        Dim cncp As New SqlClient.SqlConnection(connString)
        cncp.Open()
        Dim cmdcp As New SqlClient.SqlCommand(sSQL, cncp)
        cmdcp.CommandType = CommandType.Text
        Dim drcp As SqlClient.SqlDataReader = cmdcp.ExecuteReader
        If drcp.HasRows Then Procd = True
        cncp.Close()
        cncp = Nothing
        Return Procd
    End Function

    Private Function DocIDinPayments(ByVal DocID As String) As Boolean
        Dim DocIn As Boolean = False
        Dim sSQL As String = "Select * from Payments where DocNo = '" & DocID & "'"
        If connString <> "" Then
            Dim cncp1 As New SqlClient.SqlConnection(connString)
            cncp1.Open()
            Dim cmdcp1 As New SqlClient.SqlCommand(sSQL, cncp1)
            cmdcp1.CommandType = CommandType.Text
            Dim drcp1 As SqlClient.SqlDataReader = cmdcp1.ExecuteReader
            If drcp1.HasRows Then DocIn = True
            cncp1.Close()
            cncp1 = Nothing
            'Else
            '    Dim cncp1 As New Odbc.OdbcConnection(odbCS)
            '    cncp1.Open()
            '    Dim cmdcp1 As New Odbc.OdbcCommand(sSQL, cncp1)
            '    cmdcp1.CommandType = CommandType.Text
            '    Dim drcp1 As Odbc.OdbcDataReader = cmdcp1.ExecuteReader
            '    If drcp1.HasRows Then DocIn = True
            '    cncp1.Close()
            '    cncp1 = Nothing
        End If
        Return DocIn
    End Function

    Private Function GetPatientName(ByVal ClaimID As Long) As String
        Dim PatName As String = ""
        Dim sSQL As String = "Select * from Patients where ID in (Select Patient_ID from " &
        "Requisitions where ID in (Select Accession_ID from Charges where ID = " & ClaimID & "))"
        If connString <> "" Then
            Dim cncpn As New SqlClient.SqlConnection(connString)
            cncpn.Open()
            Dim cmdcpn As New SqlClient.SqlCommand(sSQL, cncpn)
            cmdcpn.CommandType = CommandType.Text
            Dim drcpn As SqlClient.SqlDataReader = cmdcpn.ExecuteReader
            If drcpn.HasRows Then
                While drcpn.Read
                    PatName = drcpn("LastName") & ", " & drcpn("FirstName")
                End While
            End If
            cncpn.Close()
            cncpn = Nothing
            'Else
            '    Dim cncpn As New Odbc.OdbcConnection(odbCS)
            '    cncpn.Open()
            '    Dim cmdcpn As New Odbc.OdbcCommand(sSQL, cncpn)
            '    cmdcpn.CommandType = CommandType.Text
            '    Dim drcpn As Odbc.OdbcDataReader = cmdcpn.ExecuteReader
            '    If drcpn.HasRows Then
            '        While drcpn.Read
            '            PatName = drcpn("LastName") & ", " & drcpn("FirstName")
            '        End While
            '    End If
            '    cncpn.Close()
            '    cncpn = Nothing
        End If
        Return PatName
    End Function

    Private Function GetTGPIDbyCPTAmt(ByVal ChargeID As Long, ByVal CPT As String, ByVal Amt As Double) As String
        Dim TGPID As String = ""
        Dim SubedIDs As String = ""
        Dim sSQL As String = "Select * from Charge_Detail where " &
        "Charge_ID = " & ChargeID & " and CPT_Code = '" & CPT & "'"
        If connString <> "" Then
            Dim cntbc As New SqlClient.SqlConnection(connString)
            cntbc.Open()
            Dim cmdtbc As New SqlClient.SqlCommand(sSQL, cntbc)
            cmdtbc.CommandType = CommandType.Text
            Dim drtbc As SqlClient.SqlDataReader = cmdtbc.ExecuteReader
            If drtbc.HasRows Then
                While drtbc.Read
                    If drtbc("CPT_Code") IsNot DBNull.Value _
                    AndAlso Trim(drtbc("CPT_Code")) = CPT Then    'Commons
                        TGPID = drtbc("TGP_ID")
                        If InStr(Commons, drtbc("TGP_ID").ToString & ", ") = 0 _
                        Then Commons += drtbc("TGP_ID").ToString & ", "
                        Exit While
                    End If
                End While
            Else
                sSQL = "Select * from Charge_Detail where " &
        "Charge_ID = " & ChargeID & " "
                Dim values = CommonData.ExecuteQuery(sSQL)
                Dim find As Boolean = False
                For Each row In values
                    If row("CPT_Code") = CPT Then
                        find = True
                        Exit For
                    End If

                Next
                If find Then
                Else
                    Dim acc = CommonData.ExecuteQuery("select * from Charges  where ID = " & ChargeID)
                    Dim accession As String = ""
                    For Each row In acc
                        accession = row("Accession_ID")
                    Next
                    Clipboard.SetText(accession)
                    MessageBox.Show("There is a mismatch in the CPT Code " & CPT & " in the ERA file. Please investigate Accession ID " & accession & " and verify the CPT codes for the tests ordered. The Accession ID has been copied to the clipboard.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
            cntbc.Close()
            cntbc = Nothing
            'Else
            '    Dim cntbc As New Odbc.OdbcConnection(odbCS)
            '    cntbc.Open()
            '    Dim cmdtbc As New Odbc.OdbcCommand(sSQL, cntbc)
            '    cmdtbc.CommandType = CommandType.Text
            '    Dim drtbc As Odbc.OdbcDataReader = cmdtbc.ExecuteReader
            '    If drtbc.HasRows Then
            '        While drtbc.Read
            '            If drtbc("CPT_Code") IsNot DBNull.Value _
            '             AndAlso Trim(drtbc("CPT_Code")) = CPT Then    'Commons
            '                TGPID = drtbc("TGP_ID")
            '                If InStr(Commons, drtbc("TGP_ID").ToString & ", ") = 0 _
            '                Then Commons += drtbc("TGP_ID").ToString & ", "
            '                Exit While
            '            End If
            '        End While
            '    End If
            '    cntbc.Close()
            '    cntbc = Nothing
        End If
        '
        If TGPID <> "" AndAlso SubedIDs <> "" AndAlso
        InStr(Commons, SubedIDs) = 0 And InStr(Commons, TGPID & ", ") = 0 Then 'Suber and subed
            If Subs.Length = 1 AndAlso Subs(UBound(Subs)) = "" Then
                Subs(UBound(Subs)) += TGPID & "=" & SubedIDs
                'ElseIf InStr(Subs(UBound(Subs)), TGPID & "=") = 0 Then
                '    Subs(UBound(Subs)) += TGPID & "=" & SubedIDs
            ElseIf InStr(Subs(UBound(Subs)), TGPID & "=") > 0 Then
                Dim Data() As String = Split(Subs(UBound(Subs)), "=")
                If Data(0) = TGPID Then
                    If InStr(Data(1), SubedIDs) = 0 _
                    Then Data(1) += SubedIDs
                    Subs(UBound(Subs)) = Join(Data, "=")
                Else    'different line
                    ReDim Preserve Subs(UBound(Subs) + 1)
                    Subs(UBound(Subs)) += TGPID & "=" & SubedIDs
                End If
            Else
                ReDim Preserve Subs(UBound(Subs) + 1)
                Subs(UBound(Subs)) += TGPID & "=" & SubedIDs
            End If
        End If
        Return TGPID
    End Function

    Private Function GetTGPIDbyName(ByVal TGPName As String) As Integer
        Dim TGPID As Integer = -1
        Dim sSQL As String = "select ID from Tests where IsActive <> 0 and Tbillable <> 0 and Name Like '%" & TGPName &
        "%' Union select ID from Groups where IsActive <> 0 and Tbillable <> 0 and Name Like '%" & TGPName & "%' Union " &
        "select ID from Profiles where IsActive <> 0 and Tbillable <> 0 and Name Like '%" & TGPName & "%'"
        If connString <> "" Then
            Dim cnin As New SqlClient.SqlConnection(connString)
            cnin.Open()
            Dim cmdin As New SqlClient.SqlCommand(sSQL, cnin)
            cmdin.CommandType = CommandType.Text
            Dim drin As SqlClient.SqlDataReader = cmdin.ExecuteReader
            If drin.HasRows Then
                While drin.Read
                    TGPID = drin("ID")
                End While
            End If
            cnin.Close()
            cnin = Nothing
            'Else
            '    Dim cnin As New Odbc.OdbcConnection(odbCS)
            '    cnin.Open()
            '    Dim cmdin As New Odbc.OdbcCommand(sSQL, cnin)
            '    cmdin.CommandType = CommandType.Text
            '    Dim drin As Odbc.OdbcDataReader = cmdin.ExecuteReader
            '    If drin.HasRows Then
            '        While drin.Read
            '            TGPID = drin("ID")
            '        End While
            '    End If
            '    cnin.Close()
            '    cnin = Nothing
        End If
        Return TGPID
    End Function

    Private Function GetPayerbyName(ByVal Name As String) As Payer
        Dim Payer As Payer = Nothing
        Dim sSQL As String = "select * from Payers where Active <> 0 and PayerName Like '%" & Name & "%'"
        If connString <> "" Then
            Dim cnpbn As New SqlClient.SqlConnection(connString)
            cnpbn.Open()
            Dim cmdpbn As New SqlClient.SqlCommand(sSQL, cnpbn)
            cmdpbn.CommandType = CommandType.Text
            Dim drpbn As SqlClient.SqlDataReader = cmdpbn.ExecuteReader
            If drpbn.HasRows Then
                While drpbn.Read
                    Payer.ID = drpbn("ID")
                    Payer.Name = drpbn("PayerName")
                    Payer.PayerCode = drpbn("PayerCode")
                End While
            Else
                Payer.ID = -1
                Payer.Name = ""
                Payer.PayerCode = ""
            End If
            cnpbn.Close()
            cnpbn = Nothing
            'Else
            '    Dim cnpbn As New Odbc.OdbcConnection(odbCS)
            '    cnpbn.Open()
            '    Dim cmdpbn As New Odbc.OdbcCommand(sSQL, cnpbn)
            '    cmdpbn.CommandType = CommandType.Text
            '    Dim drpbn As Odbc.OdbcDataReader = cmdpbn.ExecuteReader
            '    If drpbn.HasRows Then
            '        While drpbn.Read
            '            Payer.ID = drpbn("ID")
            '            Payer.Name = drpbn("PayerName")
            '            Payer.PayerCode = drpbn("PayerCode")
            '        End While
            '    Else
            '        Payer.ID = -1
            '        Payer.Name = ""
            '        Payer.PayerCode = ""
            '    End If
            '    cnpbn.Close()
            '    cnpbn = Nothing
        End If
        Return Payer
    End Function

    Private Function GetPayerbyClaimID(ByVal Claim As Long) As Payer
        Dim Payer As Payer = Nothing
        Dim sSQL As String = "select * from Payers where Active <> 0 and ID in " &
        "(Select Ar_ID from Charges where ArType = 1 and ID = " & Claim & ")"
        If connString <> "" Then
            Dim cnpbn As New SqlClient.SqlConnection(connString)
            cnpbn.Open()
            Dim cmdpbn As New SqlClient.SqlCommand(sSQL, cnpbn)
            cmdpbn.CommandType = CommandType.Text
            Dim drpbn As SqlClient.SqlDataReader = cmdpbn.ExecuteReader
            If drpbn.HasRows Then
                While drpbn.Read
                    Payer.ID = drpbn("ID")
                    Payer.Name = drpbn("PayerName")
                    If drpbn("PayerCode") IsNot DBNull.Value _
                    Then Payer.PayerCode = Trim(drpbn("PayerCode"))
                End While
            Else
                Payer.ID = -1
                Payer.Name = ""
                Payer.PayerCode = ""
            End If
            cnpbn.Close()
            cnpbn = Nothing
            'Else
            '    Dim cnpbn As New Odbc.OdbcConnection(odbCS)
            '    cnpbn.Open()
            '    Dim cmdpbn As New Odbc.OdbcCommand(sSQL, cnpbn)
            '    cmdpbn.CommandType = CommandType.Text
            '    Dim drpbn As Odbc.OdbcDataReader = cmdpbn.ExecuteReader
            '    If drpbn.HasRows Then
            '        While drpbn.Read
            '            Payer.ID = drpbn("ID")
            '            Payer.Name = drpbn("PayerName")
            '            If drpbn("PayerCode") IsNot DBNull.Value _
            '            Then Payer.PayerCode = Trim(drpbn("PayerCode"))
            '        End While
            '    Else
            '        Payer.ID = -1
            '        Payer.Name = ""
            '        Payer.PayerCode = ""
            '    End If
            '    cnpbn.Close()
            '    cnpbn = Nothing
        End If
        Return Payer
    End Function

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearForm()
        btnClear.Enabled = False
    End Sub

    Private Sub ClearForm()
        txtERAFile.Text = ""
        dgvEOBs.Rows.Clear()
        txtERAType.Text = ""
        documentID.Text = ""
        dgvClaims.Rows.Clear()
        dgvClaimDetail.Rows.Clear()
    End Sub

    Private Sub UpdateRejectedClaims(ByVal ChargeID As Long, ByVal PayerCLMID As String, ByVal EOBDate As Date)
        Dim sSQL As String = "If Exists (Select * from RejectedClaims where Charge_ID = " & ChargeID &
        ") Update RejectedClaims set PayerClaimID = '" & PayerCLMID & "', RejectDate = '" & EOBDate &
        "' where Charge_ID = " & ChargeID & " Else Insert into RejectedClaims (Charge_ID, PayerClaimID, " &
        "RejectDate) values (" & ChargeID & ", '" & PayerCLMID & "', '" & EOBDate & "')"
        ExecuteSqlProcedure(sSQL)
    End Sub

    Private Function GetMarkedTGPIDs(ByVal RowIndex As Integer,
    ByVal Msg As String) As String()
        Dim TGPIDs() As String = {"", ""}   '0=BillPR, 1=paid
        If (dgvClaimDetail.RowCount = 0 OrElse
        dgvClaimDetail.Rows(0).Cells(0).Value.ToString <>
        dgvClaims.Rows(RowIndex).Cells(0).Value) AndAlso Msg <> "" Then _
        DisplayClaimDetail(dgvClaims.Rows(RowIndex).Cells(0).Value,
        dgvClaims.Rows(RowIndex).Cells(1).Value, Msg)
        If dgvClaimDetail.RowCount > 0 Then
            Dim TGPID As String = ""
            For i As Integer = 0 To dgvClaimDetail.RowCount - 1
                If TGPID = "" Then TGPID = dgvClaimDetail.Rows(i).Cells(1).Value
                If Val(dgvClaimDetail.Rows(i).Cells(7).Value) > 0 Then  'PR
                    If TGPID <> "" Then
                        If InStr(TGPIDs(0), TGPID & ", ") = 0 Then TGPIDs(0) += TGPID & ", "
                    ElseIf i > 0 And dgvClaimDetail.Rows(i - 1).Cells(1).Value <> "" Then
                        If InStr(TGPIDs(0), dgvClaimDetail.Rows(i - 1).Cells(1).Value & ", ") = 0 _
                        Then TGPIDs(0) += dgvClaimDetail.Rows(i - 1).Cells(1).Value & ", "
                    End If
                End If
                If Val(dgvClaimDetail.Rows(i).Cells(6).Value) > 0 Then  'Paid
                    If TGPID <> "" Then
                        If InStr(TGPIDs(1), TGPID & ", ") = 0 Then TGPIDs(1) += TGPID & ", "
                    ElseIf i > 0 AndAlso dgvClaimDetail.Rows(i - 1).Cells(1).Value <> "" Then
                        If InStr(TGPIDs(1), dgvClaimDetail.Rows(i - 1).Cells(1).Value & ", ") = 0 _
                        Then TGPIDs(1) += dgvClaimDetail.Rows(i - 1).Cells(1).Value & ", "
                    End If
                End If
                TGPID = ""
            Next
            If TGPIDs(0).EndsWith(", ") Then TGPIDs(0) = TGPIDs(0).Substring(0, Len(TGPIDs(0)) - 2)
            If TGPIDs(1).EndsWith(", ") Then TGPIDs(1) = TGPIDs(1).Substring(0, Len(TGPIDs(1)) - 2)
        End If
        Return TGPIDs
    End Function

    Private Function GetSvcDatefromInvID(ByVal InvID As Long) As String
        Dim SvcDate As String = ""
        Dim sSQL As String = "Select Svc_Date from Charges where ID = " & InvID
        Dim cnsvc As New SqlClient.SqlConnection(connString)
        cnsvc.Open()
        Dim cmdsvc As New SqlClient.SqlCommand(sSQL, cnsvc)
        cmdsvc.CommandType = CommandType.Text
        Dim drsvc As SqlClient.SqlDataReader = cmdsvc.ExecuteReader
        If drsvc.HasRows Then
            While drsvc.Read
                SvcDate = drsvc("Svc_Date").ToString
            End While
        End If
        cnsvc.Close()
        cnsvc = Nothing
        Return SvcDate
    End Function

    Private Function UpdateRefund(ByVal InvID As Long, ByVal RefundAmt As Double) As Double
        Dim PaymentID As String = ""
        Dim DocNo As String = ""
        Dim PmtAmt As Double = 0
        Dim i As Integer = 0
        Dim cnur As New SqlClient.SqlConnection(connString)
        cnur.Open()
        Dim cmdur As New SqlClient.SqlCommand("Select * from Payments " &
        "where ID in (Select distinct Payment_ID from Payment_Detail " &
        "where Charge_ID = " & InvID & ")", cnur)
        cmdur.CommandType = CommandType.Text
        Dim drur As SqlClient.SqlDataReader = cmdur.ExecuteReader
        If drur.HasRows Then
            While drur.Read
                PaymentID = drur("ID").ToString
                DocNo = drur("DocNo")
            End While
        End If
        cnur.Close()
        cnur = Nothing
        If PaymentID <> "" Then
            ExecuteSqlProcedure("Insert into Payment_Refund (Payment_ID, Charge_ID, " &
            "Amount, RefundDate, Reason, EditedBy) values (" & Val(PaymentID) & "," &
            InvID & "," & -1 * RefundAmt & ", '" & Format(Date.Now, "MM/dd/yyyy HH:mm") &
            "','" & Trim(txtBillReason.Text) & "', " & ThisUser.ID & ")")
            '
            ExecuteSqlProcedure("Delete from Payment_Detail where " &
            "Charge_ID = " & InvID & " and Payment_ID = " & PaymentID)
            ExecuteSqlProcedure("Update Payments set Amount = Amount - " &
            PmtAmt & " where ID = " & PaymentID)
        End If
        If PmtAmt + RefundAmt = 0 Then
            Return RefundAmt
        Else
            Return 0
        End If
    End Function

    Private Function GetNextRefundID() As Long
        Dim RID As Long = 1
        Dim cnri As New SqlClient.SqlConnection(connString)
        cnri.Open()
        Dim cmdri As New SqlClient.SqlCommand("Select " &
        "max(ID) as LastID from Refunds", cnri)
        cmdri.CommandType = CommandType.Text
        Dim drri As SqlClient.SqlDataReader = cmdri.ExecuteReader
        If drri.HasRows Then
            While drri.Read
                If drri("LastID") IsNot DBNull.Value _
                Then RID = drri("LastID") + 1
            End While
        End If
        cnri.Close()
        cnri = Nothing
        Return RID
    End Function

    Private Sub dgvClaims_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvClaims.CellContentClick
        Dim Msg As String = OpenERAFile(txtERAFile.Text)
        Dim PR As Integer = 6

        'Dim fff = dgvClaims.Rows(e.RowIndex).Cells(dgvClaimsPR6).Value
        If e.ColumnIndex = 0 Then   'ClaimID
            If e.RowIndex = -1 Then
                Return
            End If
            If Msg <> "" AndAlso dgvClaims.Rows(e.RowIndex).Cells(e.ColumnIndex).Value <> "R" _
            Then DisplayClaimDetail(dgvClaims.Rows(e.RowIndex).Cells(e.ColumnIndex).Value,
            dgvClaims.Rows(e.RowIndex).Cells(1).Value, Msg)
            'cmbAction.Focus()dgvClaims.Rows(e.RowIndex).Cells(6).Value
        ElseIf e.ColumnIndex = 10 Then   'select action
            If dgvClaims.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "Bill Insurance" Then
                dgvClaims.Rows(e.RowIndex).Cells(11).ReadOnly = False
            Else
                dgvClaims.Rows(e.RowIndex).Cells(11).Value = ""
                dgvClaims.Rows(e.RowIndex).Cells(9).ReadOnly = True
            End If
            UpdateSaveOption(e.RowIndex)
        ElseIf e.ColumnIndex = 12 Then   'save
            'Billed = 3,
            '5 = Paid
            '6 = PR
            '7 = WO
            '8=Bal
            'dgvClaims
            If Format(Val(dgvClaims.Rows(e.RowIndex).Cells(dgvClaimsBilled3).Value) -
            Val(dgvClaims.Rows(e.RowIndex).Cells(dgvClaimsPaid5).Value) -
            Val(dgvClaims.Rows(e.RowIndex).Cells(dgvClaimsWO7).Value) -
            Val(dgvClaims.Rows(e.RowIndex).Cells(dgvClaimsPR6).Value), "0.00") =
            Format(Val(dgvClaims.Rows(e.RowIndex).Cells(dgvClaimsBal8).Value), "0.00") OrElse
            Format(Val(dgvClaims.Rows(e.RowIndex).Cells(dgvClaimsBilled3).Value), "0.00") =
            Format(Val(dgvClaims.Rows(e.RowIndex).Cells(dgvClaimsBal8).Value), "0.00") Then
                Dim invcid = dgvClaims.Rows(e.RowIndex).Cells(0).Value
                Dim sql = "select * from Payments where  ArType  = 1 and ID in ( select Payment_ID  from Payment_Detail where Charge_ID in( select ID  from Charges where ID = " & invcid & "))"

                Dim Data = CommonData.ExecuteQuery(sql)
                Dim DocNo1 As String = ""
                If Data.Count() > 0 Then
                    For Each d In Data
                        Dim ID = d("ID")
                        DocNo1 = d("DocNo").ToString() + " , "
                        Dim dt = d("LastEditedOn")
                    Next

                    CustomMessageBox.ShowMe("This accession with invoice id " & invcid & " is already posted under document(s) " & DocNo1 & " . Do you want to proceed.?", "Warning")
                    Dim dd = CustomMessageBox.reply
                    If CustomMessageBox.reply = "NO" Then Return
                End If
                If dgvClaims.Rows(e.RowIndex).Cells(10).Value.ToString.StartsWith("Refund") Then
                    '0=DocNo, 1=Payer, 2=PayerCode, 3=Date, 4=Amount, 5=applied, 6=Refunds
                    '
                    Dim Amt As Double = UpdateRefund(Val(dgvClaims.Rows(e.RowIndex).Cells(0).Value),
                    Val(dgvClaims.Rows(e.RowIndex).Cells(dgvClaimsBilled3).Value))
                    If Amt = Val(dgvClaims.Rows(e.RowIndex).Cells(dgvClaimsBilled3).Value) Then
                        EOB(6) = Val(EOB(6)) - Val(dgvClaims.Rows(e.RowIndex).Cells(dgvClaimsBilled3).Value)
                    Else
                        EOB(6) = Math.Round(Val(EOB(6)) + Amt, 2)
                    End If
                    dgvClaimDetail.Rows.Clear()
                    dgvClaims.Rows.RemoveAt(e.RowIndex)
                    cmbBillLevel.SelectedIndex = 10
                    txtDisc.Text = "0.00"
                ElseIf dgvClaims.Rows(e.RowIndex).Cells(10).Value.ToString.StartsWith("None") OrElse
                dgvClaims.Rows(e.RowIndex).Cells(10).Value.ToString.StartsWith("Bill Patient") OrElse
                dgvClaims.Rows(e.RowIndex).Cells(10).Value.ToString.StartsWith("Correct") Then
                    Dim CMD As String = ""
                    Dim PaymentID As Long = -1
                    Dim Applied As Double = 0
                    Dim DocNo As String = dgvEOBs.SelectedRows(0).Cells(2).Value
                    Dim ChargeID As Long =
                    ValidateChargeID(dgvClaims.Rows(e.RowIndex).Cells(0).Value,
                    dgvClaims.Rows(e.RowIndex).Cells(2).Value,
                    GetSvcDatefromInvID(dgvClaims.Rows(e.RowIndex).Cells(0).Value))
                    Dim AccID As String = GetAccIDfromChargeID(ChargeID)
                    Dim TGPIDs() As String = GetMarkedTGPIDs(e.RowIndex, Msg)   '0=Del, 1=Rev
                    Dim PayerCLMID As String = dgvClaims.Rows(e.RowIndex).Cells(1).Value
                    Dim EOBDate As Date = CDate(dgvEOBs.Rows(dgvEOBs.SelectedRows(0).Index).Cells(3).Value)
                    '
                    If Val(dgvClaims.Rows(e.RowIndex).Cells(9).Value) <> 0 Then  'Sus <> 0
                        MsgBox("Sus Amount needs to be resolved before processing",
                         MsgBoxStyle.Critical, "Prolis")
                    Else    'Process Payment
                        If (dgvClaimDetail.RowCount = 0 OrElse
                        dgvClaimDetail.Rows(0).Cells(0).Value.ToString <>
                        dgvClaims.Rows(e.RowIndex).Cells(0).Value) AndAlso Msg <> "" Then _
                        DisplayClaimDetail(dgvClaims.Rows(e.RowIndex).Cells(0).Value,
                        dgvClaims.Rows(e.RowIndex).Cells(1).Value, Msg)
                        Dim PmtApld As Double = Math.Round(Val(dgvClaims.Rows(e.RowIndex).Cells(dgvClaimsPaid5).Value), 2)
                        If Val(dgvClaims.Rows(e.RowIndex).Cells(dgvClaimsBilled3).Value) <= 0 Then 'credit
                            Dim PayerID As String = GetPayerID(ChargeID, 1)
                            Dim Code As String = ""
                            Dim sSQL As String = ""
                            For x As Integer = 0 To dgvClaimDetail.RowCount - 1
                                If dgvClaimDetail.Rows(x).Cells(1).Value <> "" Then
                                    Code = Trim(dgvClaimDetail.Rows(x).Cells(11).Value)
                                    If Code <> "" Then
                                        If Code.EndsWith(",") Then Code = Code.Substring(0, Len(Code) - 1)
                                        sSQL = "If Not Exists (Select * from DeniedClaims where Accession_ID = " & AccID & " and " &
                                         "Charge_ID = " & ChargeID & " and TGP_ID = " & dgvClaimDetail.Rows(x).Cells(1).Value &
                                         ") Insert into DeniedClaims (Accession_ID, Charge_ID, TGP_ID, Payer_ID, PayerClaimID, " &
                                         "Dated, Amount, Rem_Code) values (" & AccID & ", " & ChargeID & ", " & dgvClaimDetail.Rows(x).Cells(1).Value &
                                         ", " & PayerID & ", '" & PayerCLMID & "', '" & Date.Now & "', " & dgvClaimDetail.Rows(x).Cells(4).Value &
                                         ", '" & Code & "')"
                                        ExecuteSqlProcedure(sSQL)
                                    End If
                                    '
                                    If Val(dgvClaimDetail.Rows(x).Cells(6).Value) <> 0 Then 'Refund or additional payment
                                        UpdatePayments(1, PayerID, DocNo, EOBDate, Val(dgvEOBs.SelectedRows(0).Cells(5).Value),
                                        AccID, dgvClaimDetail.Rows(x).Cells(0).Value, PayerCLMID, dgvClaimDetail.Rows(x).Cells(1).Value,
                                        Val(dgvClaimDetail.Rows(x).Cells(4).Value), Val(dgvClaimDetail.Rows(x).Cells(6).Value),
                                        Val(dgvClaimDetail.Rows(x).Cells(8).Value), Val(dgvClaimDetail.Rows(x).Cells(7).Value), Code)
                                        'ElseIf Val(dgvClaimDetail.Rows(x).Cells(6).Value) = 0 _
                                        'AndAlso Val(dgvClaimDetail.Rows(x).Cells(7).Value) = 0 Then 'complete denied

                                    End If
                                    Code = ""
                                End If
                            Next
                            '
                            If Val(dgvClaims.Rows(e.RowIndex).Cells(dgvClaimsPR6).Value) < 0 Then  'PR


                                'delete the seconday charge detail
                                ExecuteSqlProcedure("Delete from Charge_Detail where Charge_ID in (Select ID from Charges where IsPrimary " &
                                "= 0 and ArType = 2 and Accession_ID in (Select Accession_ID from Charges where ID = " & ChargeID & "))")
                                'delete the seconday charges
                                ExecuteSqlProcedure("Delete from Charges where IsPrimary = 0 and ArType = 2 and " &
                                "Accession_ID in (Select Accession_ID from Charges where ID = " & ChargeID & ")")
                            End If
                            '
                            If PmtApld < 0 Or Val(dgvClaims.Rows(e.RowIndex).Cells(dgvClaimsPR6).Value) < 0 Then
                                'Register ChargeID with EOB
                                UpdateEOBClaim(DocNo, dgvEOBs.SelectedRows(0).Cells(1).Value,
                                dgvClaims.Rows(e.RowIndex).Cells(0).Value,
                                dgvClaims.Rows(e.RowIndex).Cells(1).Value)
                            End If
                        Else
                            If dgvClaimDetail.RowCount > 0 Then 'detail displayed
                                Dim PayerID As String = GetPayerID(ChargeID, 1)
                                Dim DocID As String = dgvEOBs.Rows(dgvEOBs.SelectedRows(0).Index).Cells(2).Value
                                Dim DocDate As String = dgvEOBs.Rows(dgvEOBs.SelectedRows(0).Index).Cells(3).Value
                                Dim EobPmt As Double = dgvEOBs.Rows(dgvEOBs.SelectedRows(0).Index).Cells(5).Value
                                Dim RowIndex As Integer = dgvEOBs.SelectedRows(0).Index
                                Dim RebillInfo() As String
                                '
                                If Trim(PayerCLMID) <> "" Then _
                                UpdateAccCharge(dgvClaims.Rows(e.RowIndex).Cells(0).Value, PayerCLMID)
                                If TGPIDs(0) <> "" Or TGPIDs(1) <> "" Then
                                    PaymentID = SaveEPayment(PayerID, 1, DocID, PayerCLMID, DocDate, EobPmt)
                                    RebillInfo = UpdateEPaymentDetail(PaymentID, ChargeID)
                                    'Applied = Total - Applied
                                    UdatePayments(PaymentID, Math.Round(Val(EOBInfo(7)), 2))
                                    If RebillInfo(0) <> "" Then
                                        Dim AccPatInfo() As String =
                                        GetAccPatIDFromInvoice(dgvClaims.Rows(e.RowIndex).Cells(0).Value)
                                        RebillPatient(AccPatInfo, RebillInfo)
                                        txtBillReason.Text = ""
                                    End If
                                End If
                                dgvClaimDetail.Rows.Clear()
                            End If
                            SynchronizeChargeToDetail(Val(dgvClaims.Rows(e.RowIndex).Cells(0).Value))
                            '
                            If PmtApld <> Math.Round(Val(EOBInfo(7)), 2) Then
                                UpdateEOBClaim(DocNo, dgvEOBs.SelectedRows(0).Cells(1).Value,
                                dgvClaims.Rows(e.RowIndex).Cells(0).Value,
                                dgvClaims.Rows(e.RowIndex).Cells(1).Value)
                            Else
                                ExecuteSqlProcedure("Delete from EOB_Claim where DocNo = '" & DocNo & "' and " &
                                "Payer = '" & dgvEOBs.SelectedRows(0).Cells(1).Value & "' and " &
                                "Charge_ID = " & Val(dgvClaims.Rows(e.RowIndex).Cells(0).Value))
                                Dim RetVal As Integer = MsgBox("The payment of Claim: " & dgvClaims.Rows(e.RowIndex).Cells(0).Value &
                                " could not be applied because of the discrepancy between the details of the Claim and that of " &
                                "the payment. Please apply the payment of claim : " & dgvClaims.Rows(e.RowIndex).Cells(0).Value &
                                ", using the manual screen." & vbCrLf & "Do you want to print this message?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo, "Prolis")
                                If RetVal = vbYes Then
                                    Printer.Print("The payment of Claim: " & dgvClaims.Rows(e.RowIndex).Cells(0).Value &
                                    " could not be applied because of the discrepancy between the details of the Claim " &
                                    "and that of the payment. Please apply the payment of claim : " &
                                    dgvClaims.Rows(e.RowIndex).Cells(0).Value & ", using the manual screen.")
                                End If
                            End If
                        End If
                        dgvClaimDetail.Rows.Clear()
                        dgvClaims.Rows.RemoveAt(e.RowIndex)
                        cmbBillLevel.SelectedIndex = 10
                        txtDisc.Text = "0.00"
                    End If
                    If dgvClaims.RowCount = 0 Then
                        '0=DocNo, 1=Payer, 2=PayerCode, 3=Date, 4=Amount, 5=applied
                        EOB(0) = dgvEOBs.SelectedRows(0).Cells(2).Value
                        EOB(1) = dgvEOBs.SelectedRows(0).Cells(1).Value
                        EOB(2) = dgvEOBs.SelectedRows(0).Cells(0).Value
                        EOB(3) = dgvEOBs.SelectedRows(0).Cells(3).Value
                        EOB(4) = dgvEOBs.SelectedRows(0).Cells(4).Value
                        SaveEOB(EOB)
                        dgvEOBs.Rows.RemoveAt(dgvEOBs.SelectedRows(0).Index)
                        If ReversedInvoices(UBound(ReversedInvoices)) <> "" Then
                            Dim Invoices As String = ""
                            For i As Integer = 0 To ReversedInvoices.Length - 1
                                Invoices += ReversedInvoices(i) & vbCrLf
                            Next
                            Dim InvMsg As String = "Some line items of or entire of the following invoices were reversed by Prolis " &
                            "in this session;" & vbCrLf & "****************" & vbCrLf & Invoices & "****************" & vbCrLf
                            Dim RetVal = MsgBox(InvMsg & "Click the 'Yes' button to print. Do you " &
                            "want to print?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Prolis")
                            If RetVal = vbYes Then
                                Printer.Print(InvMsg)
                            End If
                        End If
                    End If
                    If dgvEOBs.RowCount = 0 Then
                        If Not IO.Directory.Exists(EOBInfo(2)) Then _
                        IO.Directory.CreateDirectory(EOBInfo(2))
                        If txtERAFile.Text <> EOBInfo(2) & EOBInfo(0) Then _
                        IO.File.Copy(txtERAFile.Text, EOBInfo(2) & EOBInfo(0), True)
                        IO.File.Delete(txtERAFile.Text)
                        btnClear_Click(Nothing, Nothing)
                    End If
                ElseIf dgvClaims.Rows(e.RowIndex).Cells(10).Value.ToString.StartsWith("Reverse") Then
                    For i As Integer = 0 To dgvClaimDetail.RowCount - 1
                        ExecuteSqlProcedure("Update Req_Billable Set Bill_Status = 'H', Billed_On = NULL, " &
                        "Billed_By = NULL where Accession_ID in (Select Accession_ID from Charges " &
                        "where ID = " & Val(dgvClaims.Rows(e.RowIndex).Cells(0).Value) & ") " &
                        "and TGP_ID = " & Val(dgvClaimDetail.Rows(i).Cells(1).Value))
                        ExecuteSqlProcedure("Update Req_TGP set Billed = 0 where TGP_ID = " &
                        dgvClaimDetail.Rows(i).Cells(1).Value & " and " &
                        "Accession_ID in (Select Accession_ID from Charges where " &
                        "ID = " & dgvClaims.Rows(e.RowIndex).Cells(0).Value & ")")
                    Next
                    '
                    ExecuteSqlProcedure("Update Charges Set NetAmount = (Select sum(Extend) from " &
                    "Charge_Detail where Charge_ID = " & Val(dgvClaims.Rows(e.RowIndex).Cells(0).Value) &
                    "), GrossAmount = (Select sum(Extend) from Charge_Detail where Charge_ID = " &
                    Val(dgvClaims.Rows(e.RowIndex).Cells(0).Value) & ") where ID = " &
                    Val(dgvClaims.Rows(e.RowIndex).Cells(0).Value))
                    '
                    ExecuteSqlProcedure("Delete from Charges where ID = " &
                    Val(dgvClaims.Rows(e.RowIndex).Cells(0).Value) & " and not ID in " &
                    "(Select Charge_ID from Charge_Detail where Charge_ID = " &
                    Val(dgvClaims.Rows(e.RowIndex).Cells(0).Value) & ")")
                    '
                    If ReversedInvoices(UBound(ReversedInvoices)) <> "" Then _
                    ReDim Preserve ReversedInvoices(UBound(ReversedInvoices) + 1)
                    ReversedInvoices(UBound(ReversedInvoices)) =
                    dgvClaims.Rows(e.RowIndex).Cells(0).Value
                    '
                    dgvClaimDetail.Rows.Clear()
                    dgvClaims.Rows.RemoveAt(e.RowIndex)
                    cmbBillLevel.SelectedIndex = 10
                    txtDisc.Text = "0.00"
                ElseIf dgvClaims.Rows(e.RowIndex).Cells(10).Value.ToString.StartsWith("Support") Then
                    Dim ChargeID As Long =
                        ValidateChargeID(dgvClaims.Rows(e.RowIndex).Cells(0).Value,
                        dgvClaims.Rows(e.RowIndex).Cells(2).Value,
                        GetSvcDatefromInvID(dgvClaims.Rows(e.RowIndex).Cells(0).Value))
                    Dim AccID As String = GetAccIDfromChargeID(ChargeID)
                    Dim PayerID As String = GetPayerID(ChargeID, 1)
                    Dim PayerCLMID As String = dgvClaims.Rows(e.RowIndex).Cells(1).Value
                    Dim Code As String = ""
                    Dim sSQL As String = ""
                    For x As Integer = 0 To dgvClaimDetail.RowCount - 1
                        If dgvClaimDetail.Rows(x).Cells(1).Value <> "" Then
                            Code = Trim(dgvClaimDetail.Rows(x).Cells(11).Value)
                            If Code <> "" Then
                                If Code.EndsWith(",") Then Code = Code.Substring(0, Len(Code) - 1)
                                sSQL = "If Not Exists (Select * from DeniedClaims where Accession_ID = " & AccID & " and " &
                                "Charge_ID = " & ChargeID & " and TGP_ID = " & dgvClaimDetail.Rows(x).Cells(1).Value &
                                ") Insert into DeniedClaims (Accession_ID, Charge_ID, TGP_ID, Payer_ID, PayerClaimID, " &
                                "Dated, Amount, Rem_Code) values (" & AccID & ", " & ChargeID & ", " & dgvClaimDetail.Rows(x).Cells(1).Value &
                                ", " & PayerID & ", '" & PayerCLMID & "', '" & Date.Now & "', " & dgvClaimDetail.Rows(x).Cells(4).Value &
                                ", '" & Code & "')"
                                ExecuteSqlProcedure(sSQL)
                                Code = ""
                            End If
                        End If
                    Next
                    dgvClaimDetail.Rows.Clear()
                    dgvClaims.Rows.RemoveAt(e.RowIndex)
                    cmbBillLevel.SelectedIndex = 10
                    txtDisc.Text = "0.00"
                Else    'Bill Insurance
                    If dgvClaims.Rows(e.RowIndex).Cells(11).Value.ToString <> "" Then
                        Dim CMD As String = ""
                        Dim PaymentID As Long = -1
                        Dim Applied As Double = 0
                        Dim DocNo As String = dgvEOBs.SelectedRows(0).Cells(2).Value
                        Dim ChargeID As Long =
                        ValidateChargeID(dgvClaims.Rows(e.RowIndex).Cells(0).Value,
                        dgvClaims.Rows(e.RowIndex).Cells(2).Value,
                        GetSvcDatefromInvID(dgvClaims.Rows(e.RowIndex).Cells(0).Value))
                        Dim AccID As String = GetAccIDfromChargeID(ChargeID)
                        Dim TGPIDs() As String = GetMarkedTGPIDs(e.RowIndex, Msg)   '0=Del, 1=Rev
                        Dim PayerCLMID As String = dgvClaims.Rows(e.RowIndex).Cells(1).Value
                        Dim EOBDate As Date = CDate(dgvEOBs.Rows(dgvEOBs.SelectedRows(0).Index).Cells(3).Value)
                        '
                        If Val(dgvClaims.Rows(e.RowIndex).Cells(9).Value) <> 0 Then  'Sus <> 0
                            MsgBox("Sus Amount needs to be resolved before processing",
                             MsgBoxStyle.Critical, "Prolis")
                        Else    'Process Payment
                            If (dgvClaimDetail.RowCount = 0 OrElse
                            dgvClaimDetail.Rows(0).Cells(0).Value.ToString <>
                            dgvClaims.Rows(e.RowIndex).Cells(0).Value) AndAlso Msg <> "" Then _
                            DisplayClaimDetail(dgvClaims.Rows(e.RowIndex).Cells(0).Value,
                            dgvClaims.Rows(e.RowIndex).Cells(1).Value, Msg)
                            If dgvClaimDetail.RowCount > 0 Then 'detail displayed
                                Dim PayerID As String = GetPayerID(ChargeID, 1)
                                Dim DocID As String = dgvEOBs.Rows(dgvEOBs.SelectedRows(0).Index).Cells(2).Value
                                Dim DocDate As String = dgvEOBs.Rows(dgvEOBs.SelectedRows(0).Index).Cells(3).Value
                                Dim EobPmt As Double = dgvEOBs.Rows(dgvEOBs.SelectedRows(0).Index).Cells(4).Value
                                Dim RowIndex As Integer = dgvEOBs.SelectedRows(0).Index
                                Dim RebillInfo() As String
                                '
                                If Trim(PayerCLMID) <> "" Then _
                                UpdateAccCharge(dgvClaims.Rows(e.RowIndex).Cells(1).Value, PayerCLMID)
                                PaymentID = SaveEPayment(PayerID, 1,
                                DocID, PayerCLMID, DocDate, EobPmt)
                                RebillInfo = UpdateEPaymentDetail(PaymentID, ChargeID)
                                'Applied = Total - Applied
                                UdatePayments(PaymentID, Math.Round(Val(EOBInfo(7)), 2))
                                If dgvClaims.Rows(e.RowIndex).Cells(10).Value.ToString.StartsWith("Bill Insurance") _
                                AndAlso RebillInfo(0) <> "" Then
                                    Dim AccPayerInfo() As String =
                                    GetAccPayerInfo(dgvClaims.Rows(e.RowIndex).Cells(0).Value,
                                    dgvClaims.Rows(e.RowIndex).Cells(11).Value)
                                    '0=AccID, 1=PayerID, 2=SvcDate
                                    RebillSecondInsurance(AccPayerInfo, RebillInfo)
                                    txtBillReason.Text = ""
                                End If
                                dgvClaimDetail.Rows.Clear()
                            End If
                            SynchronizeChargeToDetail(Val(dgvClaims.Rows(e.RowIndex).Cells(0).Value))
                            '
                            UpdateEOBClaim(DocNo, dgvEOBs.SelectedRows(0).Cells(1).Value,
                            dgvClaims.Rows(e.RowIndex).Cells(0).Value,
                            dgvClaims.Rows(e.RowIndex).Cells(1).Value)
                            dgvClaimDetail.Rows.Clear()
                            dgvClaims.Rows.RemoveAt(e.RowIndex)
                            cmbBillLevel.SelectedIndex = 10
                            txtDisc.Text = "0.00"
                        End If
                        If dgvClaims.RowCount = 0 Then
                            '0=DocNo, 1=Payer, 2=PayerCode, 3=Date, 4=Amount, 5=applied
                            EOB(0) = dgvEOBs.SelectedRows(0).Cells(2).Value
                            EOB(1) = dgvEOBs.SelectedRows(0).Cells(1).Value
                            EOB(2) = dgvEOBs.SelectedRows(0).Cells(0).Value
                            EOB(3) = dgvEOBs.SelectedRows(0).Cells(3).Value
                            EOB(4) = dgvEOBs.SelectedRows(0).Cells(4).Value
                            SaveEOB(EOB)
                            dgvEOBs.Rows.RemoveAt(dgvEOBs.SelectedRows(0).Index)
                        End If
                        If dgvEOBs.RowCount = 0 Then
                            If Not IO.Directory.Exists(EOBInfo(2)) Then _
                            IO.Directory.CreateDirectory(EOBInfo(2))
                            IO.File.Copy(txtERAFile.Text, EOBInfo(2) & EOBInfo(0), True)
                            IO.File.Delete(txtERAFile.Text)
                            btnClear_Click(Nothing, Nothing)
                        End If
                    Else
                        MsgBox("Prolis can not re-bill Insurance without having the associated patient " &
                        "the secondary coverage set up in his/her record. Open the Patient Management " &
                        "routine, setup the secondary coverage with all required information and try " &
                        "again.", MsgBoxStyle.Critical, "Prolis")
                    End If
                End If
                txtUnprocessed.Text = dgvClaims.RowCount.ToString
                txtProcessed.Text = (Val(txtClaims.Text) - dgvClaims.RowCount).ToString
                btnAction.Enabled = False
            End If
            'cmbAction.SelectedIndex = -1
        ElseIf e.ColumnIndex = 15 Then   'select to save
            'UpdateProcessAll()
        End If
    End Sub

    Private Sub UpdateAccCharge(ByVal ChargeID As Long, ByVal PayerClaim As String)
        Dim sSQL As String = "Update Accession_Charge Set PayerClaim = '" &
        Trim(PayerClaim) & "' where Charge_ID = " & ChargeID
        ExecuteSqlProcedure(sSQL)
    End Sub

    Private Sub RebillSecondInsurance(ByVal AccpayerInfo() As String, ByVal RebillInfo() As String)
        Dim AccID As String = AccpayerInfo(0)
        Dim PayerID As String = AccpayerInfo(1)
        Dim SvcDate As String = AccpayerInfo(2)
        Dim ChargeID As Long = GetNextPatChargeID(AccID, 0, 1)  'IsPrimary = False, ArType= Pat
        Dim CLMCharge As Double = 0
        For i As Integer = 0 To RebillInfo.Length - 1
            If RebillInfo(i) <> "" AndAlso InStr(RebillInfo(i), "|") > 0 Then
                Dim Data() As String = Split(RebillInfo(i), "|")
                '0=TGPID, 1=CPT, 2=Price, 3=ICD9, 4=Mod1, 5=Mod2, 6=Mod3, 7=Mod4, 8=POS
                If cmbBillLevel.SelectedIndex <> 10 Then _
                Data = UpdatePriceLevel(cmbBillLevel.SelectedIndex, Data)
                If Val(txtDisc.Text) > 0 Then
                    Data(2) = Format(Val(Data(2)) -
                    (Val(txtDisc.Text) / 100 * Val(Data(2))), "0.00")
                End If
                Dim cncd As New SqlClient.SqlConnection(connString)
                cncd.Open()
                Dim cmdupsert As New SqlClient.SqlCommand("Charge_Detail_SP", cncd)
                cmdupsert.CommandType = CommandType.StoredProcedure
                cmdupsert.Parameters.AddWithValue("@act", "Upsert")
                cmdupsert.Parameters.AddWithValue("@Charge_ID", ChargeID)
                cmdupsert.Parameters.AddWithValue("@TGP_ID", Data(0))
                cmdupsert.Parameters.AddWithValue("@Ordinal", i)
                cmdupsert.Parameters.AddWithValue("@CPT_Code", Data(1))
                cmdupsert.Parameters.AddWithValue("@ICD9", Data(3))
                cmdupsert.Parameters.AddWithValue("@Unit", 1)
                cmdupsert.Parameters.AddWithValue("@LinePrice", Data(2))
                cmdupsert.Parameters.AddWithValue("@Extend", Data(2))
                cmdupsert.Parameters.AddWithValue("@Mod1", Data(4))
                cmdupsert.Parameters.AddWithValue("@Mod2", Data(5))
                cmdupsert.Parameters.AddWithValue("@Mod3", Data(6))
                cmdupsert.Parameters.AddWithValue("@Mod4", Data(7))
                If Data(8) IsNot Nothing AndAlso Data(8) <> "" Then
                    cmdupsert.Parameters.AddWithValue("@POS_Code", Data(8))
                Else
                    cmdupsert.Parameters.AddWithValue("@POS_Code", "81")
                End If
                cmdupsert.Parameters.AddWithValue("@Billed_On", Date.Now)
                cmdupsert.Parameters.AddWithValue("@Billed_By", ThisUser.ID)
                Try
                    cmdupsert.ExecuteNonQuery()
                    CLMCharge += Data(2)
                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    cncd.Close()
                    cncd = Nothing
                End Try
            End If
        Next
        '
        ExecuteSqlProcedure("Delete from Charges where ID = " & ChargeID)
        ExecuteSqlProcedure("Insert into Charges (ID, Accession_ID, Ar_ID, ArType, IsPrimary, BillReason, " &
        "NetAmount, TaxAmount, GrossAmount, Bill_Date, Svc_Date, Due_Date, Term, Note, Output, " &
        "LastEditedOn, EditedBy) values(" & ChargeID & ", " & AccID & ", " & PayerID & ", 1, 0,'" &
        Trim(txtBillReason.Text) & "', " & Math.Round(CLMCharge, 2) & ", 0, " & Math.Round(CLMCharge,
        2) & ", '" & Date.Now & "', '" & SvcDate & "', '" & DateAdd(DateInterval.Day, 15, Date.Now) _
        & "', " & "'Net 15 Days', '', 0, '" & Date.Now & "', " & ThisUser.ID & ")")
    End Sub

    Private Function GetAccPayerInfo(ByVal ChargeID As String, ByVal PayerID As String) As String()
        Dim AccPyer() As String = {"", "", ""}
        Dim cnapi As New SqlClient.SqlConnection(connString)
        cnapi.Open()
        Dim cmdapi As New SqlClient.SqlCommand("Select AccessionID, " &
        "Svc_Date from Charges where ID = " & ChargeID, cnapi)
        cmdapi.CommandType = CommandType.Text
        Dim drapi As SqlClient.SqlDataReader = cmdapi.ExecuteReader
        If drapi.HasRows Then
            While drapi.Read
                AccPyer(0) = drapi("Accession_ID").ToString
                AccPyer(1) = PayerID
                AccPyer(2) = Format(drapi("Svc_Date"), SystemConfig.DateFormat)
            End While
        End If
        cnapi.Close()
        cnapi = Nothing
        Return AccPyer
    End Function


    Private Function GetRebillInfo(ByVal ChargeID As Long) As String()
        Dim RebillInfo() As String = {""}
        For i As Integer = 0 To dgvClaimDetail.RowCount - 1
            If Val(dgvClaimDetail.Rows(i).Cells(7).Value) > 0 Then
                If RebillInfo(UBound(RebillInfo)) <> "" Then _
                ReDim Preserve RebillInfo(UBound(RebillInfo) + 1)
                RebillInfo(UBound(RebillInfo)) =
                dgvClaimDetail.Rows(i).Cells(0).Value.ToString _
                & "|" & dgvClaimDetail.Rows(i).Cells(3).Value & "|" _
                & dgvClaimDetail.Rows(i).Cells(7).Value
            End If
        Next
        Return RebillInfo
    End Function

    Private Sub UpdateEOBClaim(ByVal DocNo As String, ByVal PayerName _
    As String, ByVal ChargeID As Long, ByVal PayerClaimID As String)
        ExecuteSqlProcedure("If Exists (Select * from EOB_Claim where DocNo = '" & DocNo &
        "' and Payer = '" & PayerName & "' and Charge_ID = " & ChargeID & ") Update " &
        "EOB_Claim set PayerClaimID = '" & PayerClaimID & "' where DocNo = '" & DocNo &
        "' and Payer = '" & PayerName & "' and Charge_ID = " & ChargeID & " Else Insert " &
        "into EOB_Claim (DocNo, Payer,  Charge_ID, PayerClaimID) values ('" & DocNo &
        "', '" & PayerName & "', " & ChargeID & ", '" & PayerClaimID & "')")
    End Sub

    Private Sub SynchronizeChargeToDetail(ByVal ChargeID As Long)
        ExecuteSqlProcedure("If (Round((Select sum(Extend) from Charge_Detail where " &
        "Charge_ID = " & ChargeID & "), 2) > 0) Update Charges set NetAmount = Round((Select " &
        "sum(Extend) from Charge_Detail where Charge_ID = " & ChargeID & "), 2), GrossAmount = " &
        "Round((Select sum(Extend) from Charge_Detail where Charge_ID = " & ChargeID & "), 2) " &
        "where ID = " & ChargeID & " Else Delete from Charges where ID = " & ChargeID)
    End Sub

    Private Sub UdatePayments(ByVal PmtID As Long, ByRef Applied As Double)
        ExecuteSqlProcedure("Update Payments set UnApplied = Amount - Round((Select " &
        "sum(AppliedAmount) from Payment_Detail where Payment_ID = " & PmtID & "), 2) where ID = " & PmtID)
    End Sub
    Public Function ReadERAFileContents() As String
        ' Get the file path from the TextBox
        Dim filePath As String = txtERAFile.Text.Trim() ' Assuming txtERAFile.Text contains the file path

        ' Check if the file exists
        If File.Exists(filePath) Then
            ' Read all text from the file
            Dim fileContents As String = File.ReadAllText(filePath)
            Return fileContents
        Else
            ' Handle case where file does not exist
            Return "File not found or path is invalid."
        End If
    End Function
    Private Sub SaveEOB(ByVal EOB() As String)
        '0=DocNo, 1=Payer, 2=PayerCode, 3=Date, 4=Amount, 5=Applied
        Dim eobContents As String = ReadERAFileContents()
        Dim sSQL As String = "If Exists (Select * from EOBS where DocNo = '" &
        EOB(0) & "' and Payer = '" & EOB(1) & "') Update EOBS Set PayerCode = '" &
        EOB(2) & "', EOBDate = '" & EOB(3) & "',EOB_Contents='" & eobContents & "', EOBAmount = " & EOB(5) &
        " where DocNo = '" & EOB(0) & "' and Payer = '" & EOB(1) & "' Else " &
        "Insert into EOBS (DocNo, Payer, PayerCode, EOBDate, EOBAmount,EOB_Contents) values " &
        "('" & EOB(0) & "', '" & EOB(1) & "', '" & EOB(2) & "', '" & EOB(3) &
        "', " & EOB(5) & ",'" & eobContents & "')"
        Dim rslt As String = ExecuteSqlProcedure(sSQL)
        If rslt <> "" Then
            MessageBox.Show(rslt)
        End If
    End Sub

    Private Function UpdatePriceLevel(ByVal Level As Int16, ByVal Data() As String) As String()
        Dim cnapi As New SqlClient.SqlConnection(connString)
        cnapi.Open()
        Dim cmdapi As New SqlClient.SqlCommand("Select ListPrice, Price1, Price2, " &
        "Price3, Price4, Price5, Price6, Price7, Price8, Price9 from Tests where " &
        "ID = " & Data(0) & " Union Select ListPrice, Price1, Price2, Price3, " &
        "Price4, Price5, Price6, Price7, Price8, Price9 from Groups where ID = " &
        Data(0) & " Union Select ListPrice, Price1, Price2, Price3, Price4, Price5, " &
        "Price6, Price7, Price8, Price9 from Profiles where ID = " & Data(0), cnapi)
        cmdapi.CommandType = CommandType.Text
        Dim drapi As SqlClient.SqlDataReader = cmdapi.ExecuteReader
        If drapi.HasRows Then
            While drapi.Read
                If Level = 1 Then   'Level 1
                    Data(2) = drapi("Price1").ToString
                ElseIf Level = 2 Then   'Level 2
                    Data(2) = drapi("Price2").ToString
                ElseIf Level = 3 Then   'Level 3
                    Data(2) = drapi("Price3").ToString
                ElseIf Level = 4 Then   'Level 4
                    Data(2) = drapi("Price4").ToString
                ElseIf Level = 5 Then   'Level 5
                    Data(2) = drapi("Price5").ToString
                ElseIf Level = 6 Then   'Level 6
                    Data(2) = drapi("Price6").ToString
                ElseIf Level = 7 Then   'Level 7
                    Data(2) = drapi("Price7").ToString
                ElseIf Level = 8 Then   'Level 8
                    Data(2) = drapi("Price8").ToString
                ElseIf Level = 9 Then   'Level 9
                    Data(2) = drapi("Price9").ToString
                Else
                    Data(2) = drapi("ListPrice").ToString
                End If
            End While
        End If
        cnapi.Close()
        cnapi = Nothing
        Return Data
    End Function

    Private Sub RebillPatientNew(ByVal AccPatInfo() As String, ByVal RebillInfo() As String)
        Dim AccID As String = AccPatInfo(0)
        Dim PatID As String = AccPatInfo(1)
        Dim SvcDate As String = AccPatInfo(2)
        Dim ChargeID As Long = GetNextPatChargeID(AccID, 0, 2)  'IsPrimary = False, ArType= Pat
        Dim CLMCharge As Double = 0
        Dim sSQL As String = ""
        For i As Integer = 0 To RebillInfo.Length - 1
            If RebillInfo(i) <> "" AndAlso InStr(RebillInfo(i), "|") > 0 Then
                Dim Data() As String = Split(RebillInfo(i), "|")
                '0=TGPID, 1=CPT, 2=Price
                If cmbBillLevel.SelectedIndex <> 10 Then _
                Data = UpdatePriceLevel(cmbBillLevel.SelectedIndex, Data)
                If Val(txtDisc.Text) > 0 Then
                    Data(2) = Format(Val(Data(2)) -
                    (Val(txtDisc.Text) / 100 * Val(Data(2))), "0.00")
                End If
                sSQL = "If exists (Select * from Charge_Detail where Charge_ID = " & ChargeID &
                " and TGP_ID = " & Data(0) & ") Update Charge_Detail set Ordinal = " & i & ", " &
                "CPT_Code = '" & Data(1) & "', ICD9 = '', Unit = 1, LinePrice = '" & Data(2) &
                "', Extend = '" & Data(2) & "', Mod1 = '', Mod2 = '', Mod3 = '', Mod4 = '', " &
                "POS_Code = '81', Billed_On = '" & Date.Now & "', Billed_By = " & ThisUser.ID &
                " where Charge_ID = " & ChargeID & " and TGP_ID = " & Data(0) & " Else Insert " &
                "into Charge_Detail (Charge_ID, TGP_ID, Ordinal, CPT_Code, ICD9, Unit, " &
                "LinePrice, Extend, Mod1, Mod2, Mod3, Mod4, POS_Code, Billed_On, Billed_By) " &
                "values (" & ChargeID & ", " & Data(0) & ", " & i & ", '" & Data(1) & "', ''" &
                ", 1, '" & Data(2) & "', '" & Data(2) & "', '', '', '', '', '81', '" & Date.Now &
                "', " & ThisUser.ID & ")"
                ExecuteSqlProcedure(sSQL)
            End If
        Next
        '
        ExecuteSqlProcedure("Delete from Charges where ID = " & ChargeID)
        ExecuteSqlProcedure("Insert into Charges (ID, Accession_ID, Ar_ID, ArType, IsPrimary, " &
        "BillReason, NetAmount, TaxAmount, GrossAmount, Bill_Date, Svc_Date, Due_Date, Term, " &
        "Note, Output, LastEditedOn, EditedBy) values(" & ChargeID & ", " & AccID & ", " & PatID &
        ", 2, 0,'" & Trim(txtBillReason.Text) & "', " & Math.Round(CLMCharge, 2) & ", 0, " &
        Math.Round(CLMCharge, 2) & ", '" & Date.Now & "', '" & SvcDate & "', '" &
        DateAdd(DateInterval.Day, 15, Date.Now) & "', " & "'Net 15 Days', '', 0, '" &
        Date.Now & "', " & ThisUser.ID & ")")
    End Sub

    Private Function TGPinCharge(ByVal ChargeID As Long, ByVal TGPID As Integer) As Boolean
        Dim TinC As Boolean = False
        Dim cncic As New SqlClient.SqlConnection(connString)
        cncic.Open()
        Dim cmdcic As New SqlClient.SqlCommand("Select * from Charge_Detail " &
        "where Charge_ID = " & ChargeID & " and TGP_ID = " & TGPID, cncic)
        cmdcic.CommandType = CommandType.Text
        Dim drcic As SqlClient.SqlDataReader = cmdcic.ExecuteReader
        If drcic.HasRows Then TinC = True
        cncic.Close()
        cncic = Nothing
        Return TinC
    End Function

    Private Function GetVariousTGPs(ByVal ChargeID As Long, ByVal Reporteds As String) As String()
        Dim VarTGPs() As String = {"", "", "", ""}  '0=Unrptds, 1=Subs, 2=Commons, 3=UCharges
        Dim Unreporteds As String = ""
        Dim Subs As String = ""
        Dim UCharges As Double = 0
        Dim Commons As String = ""
        If Reporteds <> "" Then
            Dim RPTDS() As String = Split(Reporteds, ",")
            'If Microsoft.VisualBasic.Right(Reporteds, 2) <> ", " Then Reporteds += ", "
            Dim cnc1 As New SqlClient.SqlConnection(connString)
            cnc1.Open()
            Dim cmdc1 As New SqlClient.SqlCommand("Select * from Charge_Detail where " &
            "Charge_ID = " & ChargeID & " and Not TGP_ID in (" & Reporteds & ")", cnc1)
            cmdc1.CommandType = CommandType.Text
            Dim drc1 As SqlClient.SqlDataReader = cmdc1.ExecuteReader
            If drc1.HasRows Then
                While drc1.Read
                    Unreporteds += drc1("TGP_ID").ToString & ", "
                    UCharges += drc1("Extend")
                End While
            End If
            cnc1.Close()
            cnc1 = Nothing
            '
            Dim cnc2 As New SqlClient.SqlConnection(connString)
            cnc2.Open()
            Dim cmdc2 As New SqlClient.SqlCommand("Select * from Charge_Detail " &
            "where Charge_ID = " & ChargeID & " and TGP_ID in (" & Reporteds & ")", cnc2)
            cmdc2.CommandType = CommandType.Text
            Dim drc2 As SqlClient.SqlDataReader = cmdc2.ExecuteReader
            If drc2.HasRows Then
                While drc2.Read
                    Commons += drc2("TGP_ID").ToString & ", "
                End While
            End If
            cnc2.Close()
            cnc2 = Nothing
            For i As Integer = 0 To RPTDS.Length - 1
                If InStr(Commons, Trim(RPTDS(i)) & ",") = 0 Then
                    Subs += Trim(RPTDS(i)) & ", "
                End If
            Next
        End If
        '0=Unrptds, 1=Subs, 2=Commons, 3=UCharges
        VarTGPs(0) = Unreporteds : VarTGPs(1) = Subs
        VarTGPs(2) = Commons : VarTGPs(3) = UCharges
        Return VarTGPs
    End Function

    Private Function GetReportedTGPs() As String
        Dim TGPS As String = ""
        For i As Integer = 0 To dgvClaimDetail.RowCount - 1
            If InStr(TGPS, dgvClaimDetail.Rows(i).Cells(1).Value & ",") = 0 _
            Then TGPS += dgvClaimDetail.Rows(i).Cells(1).Value & ", "
        Next
        If TGPS.Length > 2 AndAlso Microsoft.VisualBasic.Right(TGPS, 2) =
        ", " Then TGPS = Microsoft.VisualBasic.Mid(TGPS, 1, Len(TGPS) - 2)
        Return TGPS
    End Function

    Private Function GetUnReporteds(ByVal Unreporteds As String) As String()
        Dim TGPIDs() As String = {""}
        If Unreporteds.Length > 2 AndAlso
        Microsoft.VisualBasic.Right(Unreporteds, 2) = ", " Then _
        Unreporteds = Microsoft.VisualBasic.Mid(Unreporteds, 1, Len(Unreporteds) - 2)
        If InStr(Unreporteds, ",") > 0 Then
            TGPIDs = Split(Unreporteds, ",")
        Else
            TGPIDs(0) = Unreporteds
        End If
        Return TGPIDs
    End Function

    Private Function GetPrimeCodes(ByVal ChargeID As Long, ByVal TGPID As Integer) As String
        Dim Codes As String = "|||||"
        Dim cnpc As New SqlClient.SqlConnection(connString)
        cnpc.Open()
        Dim cmdpc As New SqlClient.SqlCommand("Select * from Charge_Detail where TGP_ID = " & TGPID &
        " and Charge_ID in (Select ID from Charges where ArType = 1 and Accession_ID in (Select " &
        "Accession_ID from Accession_Charge where IsPrimary <> 0 and Charge_ID = " & ChargeID & "))", cnpc)
        cmdpc.CommandType = CommandType.Text
        Dim drpc As SqlClient.SqlDataReader = cmdpc.ExecuteReader
        If drpc.HasRows Then
            While drpc.Read
                Codes = drpc("ICD9") & "|" & Trim(drpc("Mod1")) &
                "|" & Trim(drpc("Mod2")) & "|" & Trim(drpc("Mod3")) &
                "|" & Trim(drpc("Mod4")) & "|" & Trim(drpc("POS_Code"))
            End While
        End If
        cnpc.Close()
        cnpc = Nothing
        Return Codes
    End Function

    Private Function UpdateEPaymentDetail(ByVal PmtID As Long,
    ByVal ChargeID As Long) As String()
        Dim Rebills As String = ""
        Dim RebillInfo() As String = {""}
        Dim cs As Integer = 0
        'Dim TGPIDs() As String = {""}
        Dim SubTGPID As String = ""
        Dim SubAmt As Double = 0
        Dim SubBill As Boolean = False
        Dim SubAuth As Double = 0
        Dim SubPaid As Double = 0
        Dim SubPR As Double = 0
        Dim SubWO As Double = 0
        '
        Dim TGPID As String = ""
        Dim Billed As Double = 0
        Dim Auth As Double = 0
        Dim Paid As Double = 0
        Dim PR As Double = 0
        Dim WO As Double = 0
        Dim Sus As Double = 0
        Dim Bal As Double = 0
        '
        Dim sSQL As String = ""
        'Dim Reporteds As String = GetReportedTGPs()
        'Dim VarTGPs() As String = GetVariousTGPs(ChargeID, Reporteds)  '0=Unrptds, 1=Subs, 2=Commons, 3=UCharges
        'Dim Unreporteds As String = VarTGPs(0)
        'Dim URPTDS() As String = GetUnReporteds(Unreporteds)

        For i As Integer = 0 To dgvClaimDetail.RowCount - 1     'Commons
            If dgvClaimDetail.Rows(i).Cells(1).Value <> "" Then
                TGPID = dgvClaimDetail.Rows(i).Cells(1).Value
                Billed = 0 : Auth = 0 : Paid = 0 : PR = 0
                WO = 0 : Sus = 0 : Bal = 0
            End If
            '
            For c As Integer = i To dgvClaimDetail.RowCount - 1
                If dgvClaimDetail.Rows(c).Cells(1).Value = TGPID Then
                    Billed = Math.Round(Billed + Val(dgvClaimDetail.Rows(c).Cells(4).Value), 2)
                    Auth = Math.Round(Auth + Val(dgvClaimDetail.Rows(c).Cells(5).Value), 2)
                    Paid = Math.Round(Paid + Val(dgvClaimDetail.Rows(c).Cells(6).Value), 2)
                    PR = Math.Round(PR + Val(dgvClaimDetail.Rows(c).Cells(7).Value), 2)
                    WO = Math.Round(WO + Val(dgvClaimDetail.Rows(c).Cells(8).Value), 2)
                    Sus = Math.Round(Sus + Val(dgvClaimDetail.Rows(c).Cells(10).Value), 2)
                    Bal = Math.Round(Bal + Val(dgvClaimDetail.Rows(c).Cells(9).Value), 2)
                ElseIf dgvClaimDetail.Rows(c).Cells(1).Value = "" AndAlso c > i Then
                    Billed = Math.Round(Billed + Val(dgvClaimDetail.Rows(c).Cells(4).Value), 2)
                    Auth = Math.Round(Auth + Val(dgvClaimDetail.Rows(c).Cells(5).Value), 2)
                    Paid = Math.Round(Paid + Val(dgvClaimDetail.Rows(c).Cells(6).Value), 2)
                    PR = Math.Round(PR + Val(dgvClaimDetail.Rows(c).Cells(7).Value), 2)
                    WO = Math.Round(WO + Val(dgvClaimDetail.Rows(c).Cells(8).Value), 2)
                    Sus = Math.Round(Sus + Val(dgvClaimDetail.Rows(c).Cells(10).Value), 2)
                    Bal = Math.Round(Bal + Val(dgvClaimDetail.Rows(c).Cells(9).Value), 2)
                Else
                    Exit For
                End If
            Next
            '
            If TGPID <> "" Then
                If Billed > 0 And (Paid > 0 Or PR > 0 Or (Math.Round(Billed - WO, 2) = 0)) _
                AndAlso (dgvClaimDetail.Rows(i).Cells(12).Value.ToString.StartsWith("Write Off") Or
                dgvClaimDetail.Rows(i).Cells(12).Value.ToString.StartsWith("Bill")) Then     'Save the payment_Detail
                    sSQL = "If Exists (Select * from Payment_Detail where Payment_ID = " & PmtID & " and " &
                    "Charge_ID = " & ChargeID & " and TGP_ID = " & TGPID & ") Update Payment_Detail set " &
                    "Ordinal = " & i & ", ChargeAmount = " & Billed & ", AppliedAmount = " & Paid & ", " &
                    "Balance = " & Math.Round(Bal, 2) & ", WrittenOff = " & Math.Round(WO + PR, 2) & ", " &
                    "RebillAmount = 0 where Payment_ID = " & PmtID & " and Charge_ID = " & ChargeID & " and " &
                    "TGP_ID = " & TGPID & " Else Insert into Payment_Detail (Payment_ID, Charge_ID, TGP_ID, " &
                    "Ordinal, ChargeAmount, AppliedAmount, Balance, WrittenOff, RebillAmount) values (" & PmtID &
                    ", " & ChargeID & ", " & TGPID & ", " & i & ", " & Billed & ", " & Paid & ", " & Math.Round(Bal, 2) &
                    ", " & Math.Round(WO + PR, 2) & ", 0)"
                    'Temur i wrote but commeted it
                    'If Val(txtDisc.Text) > 0 Then
                    '    PR = Format((PR) - (Val(txtDisc.Text) / 100 * Val(PR)), "0.00")

                    'End If
                    'Temur WO Math.Round(WO + PR, 2) ---> Math.Round(Billed - (Paid + PR), 2)
                    sSQL = "If Exists (Select * from Payment_Detail where Payment_ID = " & PmtID & " and " &
                   "Charge_ID = " & ChargeID & " and TGP_ID = " & TGPID & ") Update Payment_Detail set " &
                   "Ordinal = " & i & ", ChargeAmount = " & Billed & ", AppliedAmount = " & Paid & ", " &
                   "Balance = " & Math.Round(Bal, 2) & ", WrittenOff = " & Math.Round(WO, 2) & ", " &
                   "RebillAmount = 0 where Payment_ID = " & PmtID & " and Charge_ID = " & ChargeID & " and " &
                   "TGP_ID = " & TGPID & " Else Insert into Payment_Detail (WORB,Payment_ID, Charge_ID, TGP_ID, " &
                   "Ordinal, ChargeAmount, AppliedAmount, Balance, WrittenOff, RebillAmount) values (0," & PmtID &
                   ", " & ChargeID & ", " & TGPID & ", " & i & ", " & Billed & ", " & Paid & ", " & Math.Round(Bal, 2) &
                   ", " & Math.Round(WO, 2) & ", 0)"

                    If dgvClaimDetail.Rows(i).Cells(12).Value.ToString.StartsWith("Bill Patient") Then
                        sSQL = "If Exists (Select * from Payment_Detail where Payment_ID = " & PmtID & " and " &
                            "Charge_ID = " & ChargeID & " and TGP_ID = " & TGPID & ") Update Payment_Detail set " &
                            "Ordinal = " & i & ", ChargeAmount = " & Billed & ", WORB =1" & ", AppliedAmount = " & Paid & ", " &
                            "Balance = " & Math.Round(Bal, 2) & ", WrittenOff = " & Math.Round(Billed - (Paid + PR), 2) & ", " &
                            "RebillAmount = 0 where Payment_ID = " & PmtID & " and Charge_ID = " & ChargeID & " and " &
                            "TGP_ID = " & TGPID & " Else Insert into Payment_Detail (Payment_ID, Charge_ID, TGP_ID, " &
                            "Ordinal, ChargeAmount, AppliedAmount, Balance, WrittenOff, RebillAmount) values (" & PmtID &
                            ", " & ChargeID & ", " & TGPID & ", " & i & ", " & Billed & ", " & Paid & ", " & Math.Round(Bal, 2) &
                            ", " & Math.Round(Billed - (Paid + PR), 2) & ", 0)"
                        'Temur  '  WO Math.Round(WO + PR, 2) ---> Math.Round(Billed - (Paid + PR), 2)
                        sSQL = "If Exists (Select * from Payment_Detail where Payment_ID = " & PmtID & " and " &
                         "Charge_ID = " & ChargeID & " and TGP_ID = " & TGPID & ") Update Payment_Detail set " &
                         "Ordinal = " & i & ", ChargeAmount = " & Billed & ", WORB =1" & ", AppliedAmount = " & Paid & ", " &
                         "Balance = " & Math.Round(Bal, 2) & ", WrittenOff = " & Math.Round(WO, 2) & ", " &
                         "RebillAmount = 0 where Payment_ID = " & PmtID & " and Charge_ID = " & ChargeID & " and " &
                         "TGP_ID = " & TGPID & " Else Insert into Payment_Detail (WORB,Payment_ID, Charge_ID, TGP_ID, " &
                         "Ordinal, ChargeAmount, AppliedAmount, Balance, WrittenOff, RebillAmount) values (1," & PmtID &
                         ", " & ChargeID & ", " & TGPID & ", " & i & ", " & Billed & ", " & Paid & ", " & Math.Round(Bal, 2) &
                         ", " & Math.Round(WO, 2) & ", 0)"
                    End If
                    ExecuteSqlProcedure(sSQL)
                    If dgvClaimDetail.Rows(i).Cells(12).Value.ToString.StartsWith("Bill Patient") Then
                        If RebillInfo(0) <> "" Then
                            Dim AccPatInfo() As String =
                            GetAccPatIDFromInvoice(dgvClaims.Rows(0).Cells(0).Value)
                            ' RebillPatient(AccPatInfo, RebillInfo)
                        End If
                    End If
                    EOBInfo(7) = Math.Round(Val(EOBInfo(7)) + Paid, 2)
                    If PR > 0 Then
                        Rebills = TGPID.ToString & "|" & dgvClaimDetail.Rows(i).Cells(3).Value &
                        "|" & PR & "|" & GetPrimeCodes(ChargeID, TGPID) & "|" & Math.Round(WO, 2)
                    End If
                    cs += 1
                    '
                    If Paid > 0 Then SavePaymentHistory(GetPayerID(ChargeID, 1), TGPID, Paid)
                    '
                    If Rebills <> "" Then
                        If RebillInfo(UBound(RebillInfo)) <> "" Then _
                        ReDim Preserve RebillInfo(UBound(RebillInfo) + 1)
                        RebillInfo(UBound(RebillInfo)) = Rebills
                        Rebills = ""
                    End If
                Else    'Denied
                    Dim AccID As String = GetAccIDfromChargeID(ChargeID)
                    Dim PayerCLMID As String = ""
                    For r As Integer = 0 To dgvClaims.RowCount - 1
                        If dgvClaims.Rows(r).Cells(0).Value = ChargeID Then
                            PayerCLMID = dgvClaims.Rows(r).Cells(1).Value
                            Exit For
                        End If
                    Next
                    Dim PayerID As String = GetPayerID(ChargeID, 1)
                    Dim Code As String = Trim(dgvClaimDetail.Rows(i).Cells(11).Value)
                    If Code <> "" Then
                        If Code.EndsWith(",") Then Code = Code.Substring(0, Len(Code) - 1)
                        sSQL = "If Not Exists (Select * from DeniedClaims where Accession_ID = " & AccID & " and " &
                        "Charge_ID = " & ChargeID & " and TGP_ID = " & dgvClaimDetail.Rows(i).Cells(1).Value &
                        ") Insert into DeniedClaims (Accession_ID, Charge_ID, TGP_ID, Payer_ID, PayerClaimID, " &
                        "Dated, Amount, Rem_Code) values (" & AccID & ", " & ChargeID & ", " & dgvClaimDetail.Rows(i).Cells(1).Value &
                        ", " & PayerID & ", '" & PayerCLMID & "', '" & Date.Now & "', " & dgvClaimDetail.Rows(i).Cells(4).Value &
                        ", '" & Code & "')"
                        ExecuteSqlProcedure(sSQL)
                        Code = ""
                    End If
                End If
                TGPID = ""
            End If
        Next    'of commons
        Commons = ""
        '
        If Subs(0) IsNot Nothing AndAlso Subs(0) <> "" Then       'Subs initialized
            For i As Integer = 0 To Subs.Length - 1
                SubTGPID = Microsoft.VisualBasic.Mid(Subs(i), 1, InStr(Subs(i), "=") - 1)
                For s As Integer = 0 To dgvClaimDetail.RowCount - 1
                    If dgvClaimDetail.Rows(s).Cells(0).Value = ChargeID AndAlso
                    dgvClaimDetail.Rows(s).Cells(1).Value = SubTGPID Then   'chargeid + TGPID
                        SubAmt = Val(dgvClaimDetail.Rows(s).Cells(4).Value)
                        SubAuth = Val(dgvClaimDetail.Rows(s).Cells(5).Value)
                        SubPaid = Val(dgvClaimDetail.Rows(s).Cells(6).Value)
                        SubPR = Val(dgvClaimDetail.Rows(s).Cells(7).Value)
                        SubWO = Val(dgvClaimDetail.Rows(s).Cells(8).Value)
                        Dim SubedIDs As String =
                        Microsoft.VisualBasic.Mid(Subs(i), InStr(Subs(i), "=") + 1)
                        Dim TGPIDS() As String = Split(SubedIDs, ",")
                        For n As Integer = 0 To TGPIDS.Length - 1
                            If Trim(TGPIDS(n)) <> "" Then
                                TGPID = Trim(TGPIDS(n))
                                Dim cnsb As New SqlClient.SqlConnection(connString)
                                cnsb.Open()
                                Dim cmdsb As New SqlClient.SqlCommand("Select * from Charge_Detail " &
                                "where Charge_ID = " & ChargeID & " and TGP_ID = " & TGPID, cnsb)
                                cmdsb.CommandType = CommandType.Text
                                Dim drsb As SqlClient.SqlDataReader = cmdsb.ExecuteReader
                                If drsb.HasRows Then
                                    While drsb.Read
                                        If SubAuth > 0 Then
                                            Dim cnpd As New SqlClient.SqlConnection(connString)
                                            cnpd.Open()
                                            Dim cmdupsert As New SqlClient.SqlCommand("Payment_Detail_SP", cnpd)
                                            cmdupsert.CommandType = CommandType.StoredProcedure
                                            cmdupsert.Parameters.AddWithValue("@act", "Upsert")
                                            cmdupsert.Parameters.AddWithValue("@Payment_ID", PmtID)
                                            cmdupsert.Parameters.AddWithValue("@Charge_ID", ChargeID)
                                            cmdupsert.Parameters.AddWithValue("@TGP_ID", TGPID)
                                            cmdupsert.Parameters.AddWithValue("@Ordinal", cs)
                                            cmdupsert.Parameters.AddWithValue("@PayerClaimID", "")
                                            cmdupsert.Parameters.AddWithValue("@ChargeAmount", Billed)
                                            cmdupsert.Parameters.AddWithValue("@Balance", Bal)
                                            cmdupsert.Parameters.AddWithValue("@AppliedAmount", Paid)
                                            cmdupsert.Parameters.AddWithValue("@UnappliedAmount", "")
                                            cmdupsert.Parameters.AddWithValue("@WORB", 0)

                                            cmdupsert.Parameters.AddWithValue("@WrittenOff", Math.Round(WO + PR, 2))
                                            cmdupsert.Parameters.AddWithValue("@RebillAmount", 0)
                                            cmdupsert.Parameters.AddWithValue("@Rem_Code", "")
                                            Try
                                                cmdupsert.ExecuteNonQuery()
                                                EOBInfo(7) = Math.Round(Val(EOBInfo(7)) + Paid, 2)
                                                If PR > 0 Then _
                                                Rebills = TGPID.ToString & "|" &
                                                dgvClaimDetail.Rows(s).Cells(3).Value & "|" & PR
                                            Catch ex As Exception
                                                MsgBox(ex.Message)
                                            Finally
                                                cnpd.Close()
                                                cnpd = Nothing
                                            End Try
                                            If Auth > 0 Then SavePaymentHistory(PmtID, Val(TGPID), Auth)
                                            If Rebills <> "" Then
                                                If RebillInfo(UBound(RebillInfo)) <> "" Then _
                                                ReDim Preserve RebillInfo(UBound(RebillInfo) + 1)
                                                RebillInfo(UBound(RebillInfo)) = Rebills
                                                Rebills = ""
                                            End If
                                            SubAmt = Math.Round(SubAmt - Billed, 2)
                                            SubPaid = Math.Round(SubPaid - Paid, 2)
                                            SubPR = Math.Round(SubPR - PR, 2)
                                            TGPID = "" : Billed = 0 : Auth = 0 : WO = 0
                                            PR = 0 : Paid = 0 : Bal = 0
                                        End If
                                    End While
                                End If
                                cnsb.Close()
                                cnsb = Nothing
                            End If
                        Next ' of tgpids
                        Exit For
                    End If   'row in effect
                Next    ' s of rows
            Next    ' i of subs
            ReDim Subs(1)
        End If
        '
        For i As Integer = 0 To RebillInfo.Length - 1
            If RebillInfo(i) <> "" Then
                Dim idata() As String = Split(RebillInfo(i), "|")
                For d As Integer = i + 1 To RebillInfo.Length - 1
                    If RebillInfo(d) <> "" Then
                        Dim ddata() As String = Split(RebillInfo(d), "|")
                        If idata(0) = ddata(0) Then
                            idata(2) = Math.Round(Val(idata(2)) + Val(ddata(2)), 2)
                            RebillInfo(d) = ""
                        End If
                    End If
                Next
            End If
        Next
        Return RebillInfo
    End Function

    Private Sub RebillPatient(ByVal AccPatInfo() As String, ByVal RebillInfo() As String)
        Dim AccID As String = AccPatInfo(0)
        Dim PatID As String = AccPatInfo(1)
        Dim SvcDate As String = AccPatInfo(2)
        Dim ChargeID As Long = GetNextPatChargeID(AccID, 0, 2)  'IsPrimary = False, ArType= Pat
        Dim CLMCharge As Double = 0
        Dim Grossamnt As Double = 0
        'Dim CNP As New ADODB.Connection
        'CNP.Open(odbCS)

        ' Open the SQL connection
        Dim conn As New SqlConnection(connString)
        conn.Open()
        Dim Reason As String = Trim(txtBillReason.Text)

        For i As Integer = 0 To RebillInfo.Length - 1
            If RebillInfo(i) <> "" AndAlso InStr(RebillInfo(i), "|") > 0 Then
                Dim Data() As String = Split(RebillInfo(i), "|")
                ' Data(0) = TGPID, Data(1) = CPT, Data(2) = Price

                ' Accumulate the gross amount
                Grossamnt += Convert.ToDouble(Data(2))

                ' Clean up the CPT code
                Do Until InStr(Data(1), "-") = 0
                    Data(1) = Mid(Data(1), InStr(Data(1), "-") + 1)
                Loop

                ' Parse the line price
                Dim linePriceValue As Double
                If Double.TryParse(Data(2), linePriceValue) Then
                    linePriceValue = Math.Round(linePriceValue, 2)
                Else
                    Throw New Exception("Invalid numeric value in Data(2): " & Data(2))
                End If
                CLMCharge += linePriceValue

            End If
            ' Update the CLMCharge
        Next

        If CLMCharge > 0 Then
            'CNP.Execute("Delete from Charges where ID = " & ChargeID)
            ExecuteSqlProcedure("Delete from Charges where ID = " & ChargeID)
            '
            'CNP.Execute("Insert into Charges (ID, Accession_ID, Ar_ID, " &
            '"ArType, IsPrimary, BillReason, NetAmount, TaxAmount, GrossAmount, " &
            '"Bill_Date, Svc_Date, Due_Date, Term, Note, Output, LastEditedOn, " &
            '"EditedBy) values(" & ChargeID & ", " & AccID & ", " & PatID &
            '", 2, 0, '" & Reason & "', " & Math.Round(CLMCharge, 2) & ", 0, " &
            'Math.Round(CLMCharge, 2) & ", '" & Date.Now & "', '" &
            'SvcDate & "', '" & DateAdd(DateInterval.Day, 15, Date.Now) &
            '"', 'Net 15 Days', '', 0, '" & Date.Now & "', " &
            'ThisUser.ID & ")")
            ''SynchronizeChargeToDetail(ChargeID)

            ExecuteSqlProcedure("Insert into Charges (ID, Accession_ID, Ar_ID, " &
            "ArType, IsPrimary, BillReason, NetAmount, TaxAmount, GrossAmount, " &
            "Bill_Date, Svc_Date, Due_Date, Term, Note, Output, LastEditedOn, " &
            "EditedBy) values(" & ChargeID & ", " & AccID & ", " & PatID &
            ", 2, 0, '" & Reason & "', " & Math.Round(CLMCharge, 2) & ", 0, " &
            Math.Round(CLMCharge, 2) & ", '" & Date.Now & "', '" &
            SvcDate & "', '" & DateAdd(DateInterval.Day, 15, Date.Now) &
            "', 'Net 15 Days', '', 0, '" & Date.Now & "', " &
            ThisUser.ID & ")")
            'SynchronizeChargeToDetail(ChargeID))
        Else
            'CNP.Execute("Delete from Charges where ID = " & ChargeID)
            ExecuteSqlProcedure("Delete from Charges where ID = " & ChargeID)
        End If

        For i As Integer = 0 To RebillInfo.Length - 1
            If RebillInfo(i) <> "" AndAlso InStr(RebillInfo(i), "|") > 0 Then
                Dim Data() As String = Split(RebillInfo(i), "|")
                ' Data(0) = TGPID, Data(1) = CPT, Data(2) = Price

                ' Accumulate the gross amount
                Grossamnt += Convert.ToDouble(Data(2))

                ' Clean up the CPT code
                Do Until InStr(Data(1), "-") = 0
                    Data(1) = Mid(Data(1), InStr(Data(1), "-") + 1)
                Loop

                ' Parse the line price
                Dim linePriceValue As Double
                If Double.TryParse(Data(2), linePriceValue) Then
                    linePriceValue = Math.Round(linePriceValue, 2)
                Else
                    Throw New Exception("Invalid numeric value in Data(2): " & Data(2))
                End If

                ' Retrieve the POS_Code
                Dim posCode As String = "81" ' Default value
                Dim posCodeSql As String = "
            SELECT TOP 1 POS_Code
            FROM Providers
            WHERE ID IN (
                SELECT Orderingprovider_ID
                FROM Requisitions
                WHERE ID IN (
                    SELECT Accession_ID
                    FROM Charges
                    WHERE ID = @ChargeID
                )
            )"
                Using posCmd As New SqlCommand(posCodeSql, conn)
                    posCmd.Parameters.Add("@ChargeID", SqlDbType.Int).Value = ChargeID
                    Dim posResult As Object = posCmd.ExecuteScalar()
                    If posResult IsNot Nothing AndAlso Not IsDBNull(posResult) Then
                        posCode = posResult.ToString().Trim()
                    End If
                End Using
                If posCode = "" Then
                    posCode = "81"
                End If
                ' Check if the record exists
                Dim recordExists As Boolean
                Dim checkSql As String = "
            SELECT COUNT(*)
            FROM Charge_Detail
            WHERE Charge_ID = @ChargeID AND TGP_ID = @TGP_ID"
                Using checkCmd As New SqlCommand(checkSql, conn)
                    checkCmd.Parameters.Add("@ChargeID", SqlDbType.Int).Value = ChargeID
                    checkCmd.Parameters.Add("@TGP_ID", SqlDbType.Int).Value = Data(0)
                    recordExists = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0
                End Using

                ' Prepare common parameters
                Dim parameters As New List(Of SqlParameter) From {
            New SqlParameter("@ChargeID", SqlDbType.Int) With {.Value = ChargeID},
            New SqlParameter("@TGP_ID", SqlDbType.Int) With {.Value = Data(0)},
            New SqlParameter("@Ordinal", SqlDbType.Int) With {.Value = i},
            New SqlParameter("@CPT_Code", SqlDbType.VarChar, 50) With {.Value = Data(1)},
            New SqlParameter("@ICD9", SqlDbType.VarChar, 50) With {.Value = ""},
            New SqlParameter("@Unit", SqlDbType.Int) With {.Value = 1},
            New SqlParameter("@LinePrice", SqlDbType.Decimal) With {.Value = linePriceValue},
            New SqlParameter("@Extend", SqlDbType.Decimal) With {.Value = linePriceValue},
            New SqlParameter("@Mod1", SqlDbType.VarChar, 50) With {.Value = ""},
            New SqlParameter("@Mod2", SqlDbType.VarChar, 50) With {.Value = ""},
            New SqlParameter("@Mod3", SqlDbType.VarChar, 50) With {.Value = ""},
            New SqlParameter("@Mod4", SqlDbType.VarChar, 50) With {.Value = ""},
            New SqlParameter("@POS_Code", SqlDbType.VarChar, 50) With {.Value = posCode},
            New SqlParameter("@Billed_On", SqlDbType.DateTime) With {.Value = DateTime.Now},
            New SqlParameter("@Billed_By", SqlDbType.Int) With {.Value = ThisUser.ID}
        }

                If recordExists Then
                    ' Update existing record
                    Dim updateSql As String = "
                UPDATE Charge_Detail
                SET
                    Ordinal = ISNULL(Ordinal, @Ordinal),
                    CPT_Code = @CPT_Code,
                    ICD9 = @ICD9,
                    Unit = @Unit,
                    LinePrice = ISNULL(LinePrice, 0) + @LinePrice,
                    Extend = ISNULL(Extend, 0) + @Extend,
                    Mod1 = @Mod1,
                    Mod2 = @Mod2,
                    Mod3 = @Mod3,
                    Mod4 = @Mod4,
                    POS_Code = @POS_Code,
                    Billed_On = @Billed_On,
                    Billed_By = @Billed_By
                WHERE Charge_ID = @ChargeID AND TGP_ID = @TGP_ID"
                    Using updateCmd As New SqlCommand(updateSql, conn)
                        updateCmd.Parameters.AddRange(parameters.ToArray())
                        updateCmd.ExecuteNonQuery()
                    End Using
                Else
                    ' Insert new record
                    Dim insertSql As String = "
                INSERT INTO Charge_Detail (
                    Charge_ID,
                    TGP_ID,
                    Ordinal,
                    CPT_Code,
                    ICD9,
                    Unit,
                    LinePrice,
                    Extend,
                    Mod1,
                    Mod2,
                    Mod3,
                    Mod4,
                    POS_Code,
                    Billed_On,
                    Billed_By
                ) VALUES (
                    @ChargeID,
                    @TGP_ID,
                    @Ordinal,
                    @CPT_Code,
                    @ICD9,
                    @Unit,
                    @LinePrice,
                    @Extend,
                    @Mod1,
                    @Mod2,
                    @Mod3,
                    @Mod4,
                    @POS_Code,
                    @Billed_On,
                    @Billed_By
                )"
                    Using insertCmd As New SqlCommand(insertSql, conn)
                        insertCmd.Parameters.AddRange(parameters.ToArray())
                        insertCmd.ExecuteNonQuery()
                    End Using
                End If

                ' Update the CLMCharge
                CLMCharge += linePriceValue
            End If
        Next

        ' Close the connection
        conn.Close()
        '        For i As Integer = 0 To RebillInfo.Length - 1
        '            If RebillInfo(i) <> "" AndAlso InStr(RebillInfo(i), "|") > 0 Then
        '                Dim Data() As String = Split(RebillInfo(i), "|")
        '                Grossamnt += Convert.ToDouble(Data(2))
        '                '0=TGPID, 1=CPT, 2=Price
        '                Dim Rs As New ADODB.Recordset
        '                Rs.Open("Select cd.* ,(select POS_Code from Providers where ID in (select Orderingprovider_ID from Requisitions where ID in (select Accession_ID from Charges where ID = cd.Charge_ID ))) as POS " & _
        '"from Charge_Detail cd where   Charge_ID = " & ChargeID & "and TGP_ID = " & Data(0), CNP, ADODB.CursorTypeEnum.adOpenDynamic, _
        '                ADODB.LockTypeEnum.adLockOptimistic)
        '                If Rs.BOF Then Rs.AddNew()
        '                Rs.Fields("Charge_ID").Value = ChargeID
        '                Rs.Fields("TGP_ID").Value = Data(0)
        '                If Rs.Fields("Ordinal").Value Is Nothing OrElse _
        '                Rs.Fields("Ordinal").Value Is System.DBNull.Value Then _
        '                Rs.Fields("Ordinal").Value = i
        '                Do Until InStr(Data(1), "-") = 0
        '                    Data(1) = Microsoft.VisualBasic.Mid(Data(1), InStr(Data(1), "-") + 1)
        '                Loop
        '                Rs.Fields("CPT_Code").Value = Data(1)
        '                Rs.Fields("ICD9").Value = ""
        '                Rs.Fields("Unit").Value = 1
        '                If Rs.Fields("LinePrice").Value Is Nothing OrElse _
        '                Rs.Fields("LinePrice").Value Is System.DBNull.Value Then
        '                    Rs.Fields("LinePrice").Value = Math.Round(Val(Data(2)), 2) 'Temur It was only Data(2)
        '                Else
        '                    Rs.Fields("LinePrice").Value = Math.Round( _
        '                    Rs.Fields("LinePrice").Value + Val(Data(2)), 2) 'Temur It was only Data(2)
        '                End If
        '                If Rs.Fields("Extend").Value Is Nothing OrElse _
        '                Rs.Fields("Extend").Value Is System.DBNull.Value Then
        '                    Rs.Fields("Extend").Value =  Math.Round(Val(Data(2)), 2) 'Temur It was only Data(2)
        '                Else
        '                    Rs.Fields("Extend").Value = Math.Round( _
        '                    Rs.Fields("Extend").Value + Val(Data(2)), 2) 'Temur It was only Data(2)
        '                End If
        '                Rs.Fields("Mod1").Value = ""
        '                Rs.Fields("Mod2").Value = ""
        '                Rs.Fields("Mod3").Value = ""
        '                Rs.Fields("Mod4").Value = ""
        '                Dim CDD As String = (IIf(Rs.Fields("POS_Code").Value Is Nothing Or Rs.Fields("POS_Code").Value Is System.DBNull.Value, "81", Rs.Fields("POS_Code").Value)).ToString().Trim()
        '                Rs.Fields("POS_Code").Value = CDD
        '                Rs.Fields("Billed_On").Value = Date.Now
        '                Rs.Fields("Billed_By").Value = ThisUser.ID
        '                CLMCharge += Rs.Fields("Extend").Value
        '                Rs.Update()
        '                Rs.Close()
        '                Rs = Nothing
        '            End If
        '        Next
        '

        'CNP.Close()
        'CNP = Nothing
    End Sub
    Private Sub SavePaymentHistory(ByVal PayerID As Long, ByVal TGPID As Integer, ByVal Pmt As Double)
        Dim sSQL As String = "If Exists (Select * from PaymentHistory where TGP_ID = " & TGPID &
        " and Ar_ID = " & PayerID & " and ArType = 1) Update PaymentHistory set Amount = " &
        Pmt & " where TGP_ID = " & TGPID & " and Ar_ID = " & PayerID & " and ArType = 1 Else " &
        "Insert into PaymentHistory (ArType, Ar_ID, TGP_ID, Amount, Edited_On, Edited_By) " &
        "values (1, " & PayerID & ", " & TGPID & ", " & Pmt & ", '" & Date.Now &
        "', " & ThisUser.ID & ")"
        ExecuteSqlProcedure(sSQL)
    End Sub

    Private Function SaveEPayment(ByVal ArID As String, ByVal ArType As Integer, ByVal DocID As _
    String, ByVal PayerCLMID As String, ByVal DocDate As String, ByVal EobPmt As Double) As Long
        '0=ChargeID, 1=TGPID, 2=Pmt, 3=PR, 4=CoIns, 5=WO, 6=Bal
        Dim PmtID As Long = -1
        Dim cnpid As New SqlClient.SqlConnection(connString)
        cnpid.Open()
        Dim cmdpid As New SqlClient.SqlCommand("Select * from Payments " &
        "where ArType = " & ArType & " and DocNo = '" & DocID & "'", cnpid)
        cmdpid.CommandType = CommandType.Text
        Dim drpid As SqlClient.SqlDataReader = cmdpid.ExecuteReader
        If drpid.HasRows Then
            While drpid.Read
                PmtID = drpid("ID")
            End While
        Else
            PmtID = GetNextPaymentID()
        End If
        cnpid.Close()
        cnpid = Nothing
        '
        ExecuteSqlProcedure("If Exists (Select * from Payments where ID = " & PmtID & ") Update " &
        "Payments set Ar_ID = " & ArID & ", ArType = " & ArType & ", DocNo = '" & DocID & "', " &
        "PaymentType = 3, Amount = " & EobPmt & ", UnApplied = " & EobPmt & ", CC_ID = Null, " &
        "LastEditedOn = '" & Date.Now & "', EditedBy = " & ThisUser.ID & ",PostDate='" & dgvEOBs.SelectedRows(0).Cells(3).Value & "' where ID = " & PmtID &
        " Else Insert into Payments (ID, Ar_ID, ArType, DocNo, PaymentType, PaymentDate, Amount, " &
        "UnApplied, CC_ID, LastEditedOn, EditedBy,PostDate) values (" & PmtID & ", " & ArID & ", " & ArType &
        ", '" & DocID & "', 3, '" & DocDate & " " & Date.Now.ToString("HH:mm") & "', " & EobPmt &
        ", " & EobPmt & ", Null, '" & Date.Now & "', " & ThisUser.ID & ",'" & dgvEOBs.SelectedRows(0).Cells(3).Value & "')")
        Return PmtID
    End Function

    Private Function GetPatientInvID(ByVal ChargeID As Long, ByVal ArType As _
    Integer, ByVal IsPrimary As Boolean, ByVal InvAmt As Double) As String
        Dim InvID As String = GetNextChargeID()
        Dim accPatID() As String = GetAccPatIDFromInvoice(ChargeID)
        ExecuteSqlProcedure("Insert into Charges (ID, Accession_ID, Ar_ID, ArType, " &
        "IsPrimary, BillReason, NetAmount, TaxAmount, GrossAmount, " &
        "Bill_Date, Svc_Date, Due_Date, Term, Note, Output, LastEditedOn, " &
        "EditedBy) values(" & InvID & ", " & accPatID(0) & ", " & accPatID(1) _
        & ", 2, 0,'', " & InvAmt & ", 0, " & InvAmt & ", '" & Date.Now & "', '" &
        accPatID(2) & "', '" & DateAdd(DateInterval.Day, 15, Date.Now) & "', " &
        "'Net 15 Days', '', " & "0, '" & Date.Now & "', " & ThisUser.ID & ")")
        Return InvID
    End Function

    Private Function GetAccPatIDFromInvoice(ByVal ChargeID As Long) As String()
        Dim AccPatID() As String = {"", "", ""}
        Dim cnap As New SqlClient.SqlConnection(connString)
        cnap.Open()
        Dim cmdap As New SqlClient.SqlCommand("Select ID, Patient_ID from Requisitions " &
        "where ID in (Select Accession_ID from Charges where ID = " & ChargeID & ")", cnap)
        cmdap.CommandType = CommandType.Text
        Dim drap As SqlClient.SqlDataReader = cmdap.ExecuteReader
        If drap.HasRows Then
            While drap.Read
                AccPatID(0) = drap("ID").ToString
                AccPatID(1) = drap("Patient_ID").ToString
                AccPatID(2) = GetServiceDate(drap("ID"))
            End While
        End If
        cnap.Close()
        cnap = Nothing
        Return AccPatID
    End Function

    Private Function GetNextPatChargeID(ByVal AccID As Long,
    ByVal Primary As Boolean, ByVal ArType As Int16) As Long  'IsPrimary = False, ArType= 1(Second Ins) or 2(Pat)
        Dim ChargeID As Long = Nothing
        Dim cnnid As New SqlClient.SqlConnection(connString)
        cnnid.Open()
        Dim cmdnid As New SqlClient.SqlCommand("Select * from Accession_Charge where " &
        "Accession_ID = " & AccID & " and IsPrimary = " & Val(Primary) & " and ArType = " &
        ArType, cnnid)
        cmdnid.CommandType = CommandType.Text
        Dim drnid As SqlClient.SqlDataReader = cmdnid.ExecuteReader
        If drnid.HasRows Then
            While drnid.Read
                If drnid("ArType") = ArType And drnid("IsPrimary") = Primary Then
                    If Not IsBilled(drnid("Charge_ID")) Then
                        ChargeID = drnid("Charge_ID")
                        Exit While
                    End If
                End If
            End While
        Else
            ChargeID = GetNextChargeID()
            ExecuteSqlProcedure("Insert into Accession_Charge (Accession_ID, Charge_ID, " &
            "IsPrimary, ArType, Created_On, Created_By, Edited_On, Edited_By) values (" &
            AccID & ", " & ChargeID & ", '" & Primary & "', " & ArType & ", '" & Date.Now &
            "', " & ThisUser.ID & ", '" & Date.Now & "', " & ThisUser.ID & ")")
        End If
        cnnid.Close()
        cnnid = Nothing
        '
        If ChargeID = Nothing OrElse ChargeID = 0 Then
            ChargeID = GetNextChargeID()
            ExecuteSqlProcedure("Insert into Accession_Charge (Accession_ID, Charge_ID, " &
            "IsPrimary, ArType, Created_On, Created_By, Edited_On, Edited_By) values (" &
            AccID & ", " & ChargeID & ", '" & Primary & "', " & ArType & ", '" & Date.Now &
            "', " & ThisUser.ID & ", '" & Date.Now & "', " & ThisUser.ID & ")")
        End If
        Return ChargeID
    End Function

    Private Function IsBilled(ByVal ChargeID As Long) As Boolean
        Dim Billed As Boolean = False
        Dim cnib As New SqlClient.SqlConnection(connString)
        cnib.Open()
        Dim cmdib As New SqlClient.SqlCommand("Select * from Charges where ID = " & ChargeID, cnib)
        cmdib.CommandType = CommandType.Text
        Dim drib As SqlClient.SqlDataReader = cmdib.ExecuteReader
        If drib.HasRows Then Billed = True
        cnib.Close()
        cnib = Nothing
        Return Billed
    End Function

    Private Function GetNextChargeID() As Long
        Dim NCID As Long = Nothing
        Dim cncid As New SqlClient.SqlConnection(connString)
        cncid.Open()
        Dim cmdcid As New SqlClient.SqlCommand("Select Max(ID) as LastID from Charges", cncid)
        cmdcid.CommandType = CommandType.Text
        Dim drcid As SqlClient.SqlDataReader = cmdcid.ExecuteReader
        If drcid.HasRows Then
            While drcid.Read
                If drcid("LastID") IsNot DBNull.Value _
                Then NCID = drcid("LastID") + 1
            End While
        Else
            NCID = 1
        End If
        cncid.Close()
        cncid = Nothing
        Return NCID
    End Function

    Private Function GetSecondPayerID(ByVal ChargeID As Long, ByVal PayerID As Long) As String
        Dim Payer2ID As String = ""
        Dim cnp2 As New SqlClient.SqlConnection(connString)
        cnp2.Open()
        Dim cmdp2 As New SqlClient.SqlCommand("Select a.Insurance_ID from Coverages a inner join " &
        "(Requisitions b inner join Charges c on c.Accession_ID = b.ID) on b.Patient_ID = a.Patient_ID " &
        "where c.ArType = 1 and c.ID = " & ChargeID & " and a.Insurance_ID <> " & PayerID, cnp2)
        cmdp2.CommandType = CommandType.Text
        Dim drp2 As SqlClient.SqlDataReader = cmdp2.ExecuteReader
        If drp2.HasRows Then
            While drp2.Read
                Payer2ID = drp2("Insurance_ID").ToString
            End While
        End If
        cnp2.Close()
        cnp2 = Nothing
        Return Payer2ID
    End Function

    Private Function GetPayerID(ByVal ChargeID As Long, ByVal ArType As Integer) As String
        Dim pid As String = ""
        Dim cnp As New SqlClient.SqlConnection(connString)
        cnp.Open()
        Dim cmdp As New SqlClient.SqlCommand("Select Ar_ID from " &
        "Charges where ArType = " & ArType & " and ID = " & ChargeID, cnp)
        cmdp.CommandType = CommandType.Text
        Dim drp As SqlClient.SqlDataReader = cmdp.ExecuteReader
        If drp.HasRows Then
            While drp.Read
                pid = drp("Ar_ID").ToString
            End While
        End If
        cnp.Close()
        cnp = Nothing
        Return pid
    End Function

    Private Function GetTGPofChargeID(ByVal ChargeID As Long, ByVal Amt As Double) As String
        Dim TGPID As String = ""
        Dim cntgp As New SqlClient.SqlConnection(connString)
        cntgp.Open()
        Dim cmdtgp As New SqlClient.SqlCommand("Select TGP_ID from Charge_Detail " &
        "where Charge_ID = " & ChargeID & " and Extend = " & Amt & " and Mod1 <> " &
        "'59' and Mod2 <> '59' and Mod3 <> '59' and Mod4 <> '59'", cntgp)
        cmdtgp.CommandType = CommandType.Text
        Dim drtgp As SqlClient.SqlDataReader = cmdtgp.ExecuteReader
        If drtgp.HasRows Then
            While drtgp.Read
                TGPID = drtgp("TGP_ID").ToString
            End While
        End If
        cntgp.Close()
        cntgp = Nothing
        Return TGPID
    End Function

    Private Function UpdateClaimLine(ByVal Msg As String,
    ByVal ClaimLine() As String, ByVal f As Integer) As String()
        Dim Segs() As String
        Dim Fields() As String
        Dim Comps() As String = Nothing
        Dim Matched As Boolean = True
        Dim LineBilled As Double = Val(ClaimLine(4))
        Dim LineAuth As Double = Val(ClaimLine(5))
        Dim LinePaid As Double = Val(ClaimLine(6))
        Dim LinePR As Double = Val(ClaimLine(7))
        Dim LineWO As Double = Val(ClaimLine(8))
        Dim LineSus As Double = Val(ClaimLine(10))
        Dim LineBal As Double = Val(ClaimLine(9))
        Dim LineCodes As String = ""
        Dim i As Integer
        If Msg <> "" Then
            Segs = Split(Msg, "~")
            For i = f To Segs.Length - 1
                Segs(i) = Replace(Segs(i), Chr(10), "")
                Segs(i) = Replace(Segs(i), Chr(13), "")
                If Segs(i).Substring(0, InStr(Segs(i), Delim) - 1) = "SVC" Then
                    Fields = Split(Segs(i), Delim)
                    If Fields(1).Contains(":") Then
                        Comps = Split(Fields(1), ":")
                    ElseIf Fields(1).Contains(">") Then
                        Comps = Split(Fields(1), ">")

                    ElseIf Fields(1).Contains("<") Then
                        Comps = Split(Fields(1), "<")
                    ElseIf Fields(1).Contains("^") Then
                        Comps = Split(Fields(1), "^")
                    End If
                    If ClaimLine(3) = Comps(1) Then
                        Matched = True
                        i += 1
                        Exit For
                    End If
                End If
            Next
            ' 0=ChargeID, 1=TGPID, 2=TGP, 3=CPT, 4=Billed, 5=Allow, 6=Paid, 7=PR, 8=WO, 9=Bal, 10=Sus, 11=Code, 12=Act, 13=Exec
            For s As Integer = i To Segs.Length - 1
                Segs(s) = Replace(Segs(s), Chr(10), "")
                Segs(s) = Replace(Segs(s), Chr(13), "")
                If Segs(s) IsNot Nothing AndAlso Segs(s) <> "" Then
                    If Segs(s).Substring(0, InStr(Segs(s), Delim) - 1) = "CAS" Then
                        If Matched Then
                            Fields = Split(Segs(s), Delim)
                            If Fields(1) = "PR" Then
                                For n As Integer = 2 To Fields.Length - 1 Step 3
                                    If (Fields(n) = "1" Or Fields(n) = "2" Or Fields(n) = "3") Then 'PR
                                        ClaimLine(7) = Format(Val(ClaimLine(7)) +
                                        Val(Fields(n + 1)), "0.00")
                                    Else    'Sus
                                        ClaimLine(10) = Format(Val(ClaimLine(10)) +
                                        Val(Fields(n + 1)), "0.00")
                                        If InStr(ClaimLine(11), "PR-" & Fields(n) & ", ") _
                                        = 0 Then ClaimLine(11) += "PR-" & Fields(n) & ", "
                                    End If
                                Next
                            ElseIf Fields(1) = "CO" Then    'WO
                                For n As Integer = 2 To Fields.Length - 1 Step 3
                                    'ClaimLine(0=TGPID, 1=CPT, 2=Billed, 3=R P V, 4=Auth, 
                                    '5=Paid, 6=PR, 7=WO, 8=Sus, 9=Bal, 10=SusCode)
                                    If Fields(n) = "45" Then    'WO
                                        ClaimLine(8) = Format(Val(ClaimLine(8)) +
                                        Val(Fields(n + 1)), "0.00")
                                    Else    'Sus
                                        ClaimLine(10) = Format(Val(ClaimLine(10)) +
                                        Val(Fields(n + 1)), "0.00")
                                        If InStr(ClaimLine(11), "CO-" & Fields(n) & ", ") _
                                        = 0 Then ClaimLine(11) += "CO-" & Fields(n) & ", "
                                    End If
                                Next
                            ElseIf Fields(1) = "OA" Then    'OA sus
                                For n As Integer = 2 To Fields.Length - 1 Step 3
                                    ClaimLine(10) = Format(Val(ClaimLine(10)) +
                                    Val(Fields(n + 1)), "0.00")
                                    If InStr(ClaimLine(11), "OA-" & Fields(n) & ", ") _
                                    = 0 Then ClaimLine(11) += "OA-" & Fields(n) & ", "
                                Next
                            ElseIf Fields(1) = "PI" Then    'PI Sus
                                'CAS*PI*236*37.4
                                For n As Integer = 2 To Fields.Length - 1 Step 3
                                    ClaimLine(10) = Format(Val(ClaimLine(10)) +
                                    Val(Fields(n + 1)), "0.00")
                                    If InStr(ClaimLine(11), "PI-" & Fields(n) & ", ") _
                                    = 0 Then ClaimLine(11) += "PI-" & Fields(n) & ", "
                                Next
                            End If
                        End If
                    ElseIf Segs(s).Substring(0, InStr(Segs(s), Delim) - 1) = "AMT" Then
                        If Matched Then
                            Fields = Split(Segs(s), Delim)
                            If Fields(1) = "B6" Then    'Auth
                                ClaimLine(5) = Format(Val(ClaimLine(5)) + Val(Fields(2)), "0.00")
                            End If
                        End If
                    ElseIf Segs(s).Substring(0, InStr(Segs(s), Delim) - 1) = "SVC" Then
                        Exit For
                    End If
                End If
            Next
            'ClaimLine(0=TGPID, 1=CPT, 2=Billed, 3=R P V, 4=Auth, 
            '5=Paid, 6=PR, 7=WO, 8=Sus, 9=Bal, 10=SusCode)
            'ClaimLine(9) = Format(Val(ClaimLine(6)), "0.00")    'Update balance
        End If
        'If ClaimLine(10) IsNot Nothing AndAlso ClaimLine(10).Length > 2 And _
        'Microsoft.VisualBasic.Right(ClaimLine(10), 2) = ", " Then ClaimLine(10) _
        '= Microsoft.VisualBasic.Mid(ClaimLine(10), 1, Len(ClaimLine(10)) - 2)
        '
        Return ClaimLine
    End Function

    Private Function GetClaimLines(ByVal ClaimID As Long, ByVal Msg As String) As String()
        Dim ClaimLines() As String = {""}
        Commons = ""
        ReDim Subs(0)
        Dim Segs() As String
        Dim Fields() As String
        Dim Comps() As String = Nothing
        Dim i As Integer
        Dim ClaimLine(10) As String
        'ClaimLine(0=TGPID, 1=CPT, 2=Billed, 3=R P V, 4=Auth, 
        '5=Paid, 6=PR, 7=WO, 8=Sus, 9=Bal, 10=SusCode)
        Dim SusCode As String = ""
        Dim SusInfo(3, 1) As String
        Dim SusAmt As Double = 0
        Dim Matched As Boolean = False
        Dim LineBilled As Double = 0
        Dim LineAuth As Double = 0
        Dim LinePaid As Double = 0
        Dim LinePR As Double = 0
        Dim LineWO As Double = 0
        Dim LineSus As Double = 0
        Dim LineBal As Double = 0
        Dim LineCodes As String = ""
        '
        If Msg <> "" Then
            Segs = Split(Msg, "~")
            For i = 0 To Segs.Length - 1
                If Microsoft.VisualBasic.Left(Segs(i), 3) = "CLP" Then
                    Fields = Split(Segs(i), Delim)
                    'CLP*43099*1*396.24*0*396.24*13*7681534197117*81*1
                    'CLP|001-28989|1|108|24.86||MC|1116583479~
                    If Fields(1).Contains("-") Then
                        Fields(1) = Fields(1).Substring(InStr(Fields(1), "-"))
                    End If
                    If ClaimID = Val(Fields(1)) Then
                        Matched = True
                        i += 1
                        Exit For
                    End If
                End If
            Next
            '
            For s As Integer = i To Segs.Length - 1
                If Microsoft.VisualBasic.Left(Segs(s), 3) = "SVC" Then
                    If Matched Then
                        ReDim ClaimLine(10)
                        'SVC*HC:80050*44.08*30.44**1*HC:85025~
                        'SVC*HC:80050*52.04*0
                        Fields = Split(Segs(s), Delim)
                        If Fields(1).Contains(":") Then
                            Comps = Split(Fields(1), ":")
                        ElseIf Fields(1).Contains(">") Then
                            Comps = Split(Fields(1), ">")
                        ElseIf Fields(1).Contains("^") Then
                            Comps = Split(Fields(1), "^")
                        End If
                        ClaimLine(1) = Comps(1)
                        If ClaimLine(1) <> "" Then _
                        ClaimLine(0) = GetTGPIDbyCPTAmt(ClaimID, ClaimLine(1), Val(Fields(2)))
                        'ClaimLine(0=TGPID, 1=CPT, 2=Billed, 3=R P V, 4=Auth, 
                        '5=Paid, 6=PR, 7=WO, 8=Sus, 9=Bal, 10=SusCode)
                        ClaimLine(2) = Fields(2)
                        ClaimLine(5) = Fields(3)
                        ClaimLine = UpdateClaimLine(Msg, ClaimLine, s)
                        '
                        If ClaimLine(7) Is Nothing AndAlso Val(ClaimLine(2)) > 0 Then
                            If Val(ClaimLine(2)) - Val(ClaimLine(4)) = 0 Then
                                ClaimLine(7) = ClaimLine(8)
                            Else
                                ClaimLine(7) = Math.Round(Val(ClaimLine(2)) -
                                Val(ClaimLine(4)) - Val(ClaimLine(8)), 2)
                            End If
                        End If
                        '
                        If ClaimLine(6) Is Nothing Then ClaimLine(6) = ""
                        If ClaimLine(9) Is Nothing Then ClaimLine(9) = ""
                        'If ClaimLine(6) Is Nothing Then _
                        'ClaimLine(9) = Math.Round(Val(ClaimLine(2)) - (Val(ClaimLine(5)) _
                        '+ Val(ClaimLine(6)) + Val(ClaimLine(7)) + Val(ClaimLine(8))), 2)
                        '
                        If LineInLines(ClaimLine, ClaimLines) Then
                            ClaimLines = UpdateLines(ClaimLines, ClaimLine)
                        Else
                            If ClaimLines(UBound(ClaimLines)) <> "" Then _
                            ReDim Preserve ClaimLines(UBound(ClaimLines) + 1)
                            ClaimLines(UBound(ClaimLines)) = ClaimLine(0) & "|" & ClaimLine(1) _
                            & "|" & ClaimLine(2) & "|" & ClaimLine(3) & "|" & ClaimLine(4) &
                            "|" & ClaimLine(5) & "|" & ClaimLine(6) & "|" & ClaimLine(7) _
                            & "|" & ClaimLine(8) & "|" & ClaimLine(9) & "|" & ClaimLine(10)
                        End If
                    End If
                ElseIf Microsoft.VisualBasic.Left(Segs(s), 3) = "CLP" Then
                    Fields = Split(Segs(s), Delim)
                    'CLP*43099*1*396.24*0*396.24*13*7681534197117*81*1
                    If Fields(1).Contains("-") Then
                        Fields(1) = Fields(1).Substring(InStr(Fields(1), "-"))
                    End If
                    If ClaimID <> Val(Fields(1)) Then
                        Exit For
                    End If
                ElseIf Microsoft.VisualBasic.Left(Segs(s), 2) = "SE" Then
                    Exit For
                End If
            Next
        End If
        Return ClaimLines
    End Function

    Private Function UpdateLines(ByVal ClaimLines() As _
    String, ByVal ClaimLine() As String) As String()
        For i As Integer = 0 To ClaimLines.Length - 1
            If ClaimLine(0) = Microsoft.VisualBasic.Left(ClaimLines(i),
            InStr(ClaimLines(i), "|") - 1) Then
                Dim Line() As String = Split(ClaimLines(i), "|")
                'ClaimLine(0=TGPID, 1=CPT, 2=Billed, 3=R P V, 4=Auth, 
                '5=Paid, 6=PR, 7=WO, 8=Sus, 9=Bal, 10=SusCode)
                Line(2) = Format(Val(Line(2)) + Val(ClaimLine(2)), "0.00")  'Bill
                Line(4) = Format(Val(Line(4)) + Val(ClaimLine(4)), "0.00")  'Auth
                Line(5) = Format(Val(Line(5)) + Val(ClaimLine(5)), "0.00")  'paid
                Line(6) = Format(Val(Line(6)) + Val(ClaimLine(6)), "0.00")  'PR
                Line(7) = Format(Val(Line(7)) + Val(ClaimLine(7)), "0.00")  'WO
                Line(8) = Format(Val(Line(8)) + Val(ClaimLine(8)), "0.00")  'Sus
                Line(9) = Format(Val(Line(9)) + Val(ClaimLine(9)), "0.00")  'Bal
                If ClaimLine(10) IsNot Nothing AndAlso ClaimLine(10) <> "" Then
                    If Line(10) Is Nothing OrElse Line(10) = "" Then
                        Line(10) = ClaimLine(10)
                    Else
                        If Line(10) <> ClaimLine(10) Then
                            Line(10) = ""
                        End If
                    End If
                End If
                If Line(10).Length > 1 AndAlso
                Microsoft.VisualBasic.Right(Line(10), 1) = "," Then Line(10) =
                Microsoft.VisualBasic.Mid(Line(10), 1, Len(Line(10)) - 1)
                'ClaimLine(0=TGPID, 1=CPT, 2=Billed, 3=R P V, 4=Auth, 
                '5=Paid, 6=PR, 7=WO, 8=Sus, 9=Bal, 10=SusCode)
                ClaimLines(i) = Join(Line, "|")
            End If
        Next
        Return ClaimLines
    End Function

    Private Function LineInLines(ByVal ClaimLine() As _
    String, ByVal ClaimLines() As String) As Boolean
        Dim LNL As Boolean = False
        Dim Fields() As String
        For i As Integer = 0 To ClaimLines.Length - 1
            If ClaimLines(i) <> "" Then
                Fields = Split(ClaimLines(i), "|")
                If ClaimLine(1) = Fields(1) Then
                    LNL = True
                    Exit For
                End If
            End If
        Next
        Return LNL
    End Function

    Private Sub DisplayClaimDetail(ByVal ClaimID As Long,
        ByVal PayerClaimID As String, ByVal Msg As String)
        dgvClaimDetail.Rows.Clear()
        Dim Segs() As String
        Dim Fields() As String
        Dim Comps() As String = Nothing
        Dim TGPName As String = ""
        Dim Matched As Boolean = False
        Dim Action As String = ""   'Balance, Rebill, Refund, Write Off
        Dim n As Integer = 0
        Dim ClaimLine() As String = {"", "", "", "", "", "", "", "", "", "", "", "", "", Nothing}
        '0=ChargeID, 1=TGPID, 2=TGP, 3=CPT, 4=Billed, 5=Allow, 6=Paid, 7=PR, 8=WO, 9=Bal, 10=Sus, 11=Code, 12=Act, 13=Exec
        Segs = Split(Msg, "~")
        For n = 0 To Segs.Length - 1
            Segs(n) = Replace(Segs(n), Chr(10), "")
            Segs(n) = Replace(Segs(n), Chr(13), "")
            If Segs(n).Substring(0, InStr(Segs(n), Delim) - 1) = "CLP" Then
                'CLP*43099*1*396.24*0*396.24*13*7681534197117*81*1
                Fields = Split(Segs(n), Delim)
                If Fields(1).Contains("-") Then
                    Fields(1) = Fields(1).Substring(InStr(Fields(1), "-"))
                End If
                If ClaimID.ToString = Fields(1) Then
                    Matched = True
                    Exit For
                End If
            End If
        Next
        '
        For s As Integer = n To Segs.Length - 1
            Segs(s) = Replace(Segs(s), Chr(10), "")
            Segs(s) = Replace(Segs(s), Chr(13), "")
            If Segs(s).Substring(0, InStr(Segs(s), Delim) - 1) = "CLP" Then
                Fields = Split(Segs(s), Delim)
                If Fields(1).Contains("-") Then
                    Fields(1) = Fields(1).Substring(InStr(Fields(1), "-"))
                End If
                If ClaimID.ToString = Fields(1) Then
                    Matched = True
                    ClaimLine(0) = Fields(1)
                Else
                    Matched = False
                End If
            ElseIf Segs(s).Substring(0, InStr(Segs(s), Delim) - 1) = "SVC" Then
                If Matched Then
                    'SVC*HC:80050*44.08*30.44**1*HC:85025~
                    'SVC*HC:80050*52.04*0
                    'SVC*HC>80307*1000*0**1
                    Fields = Split(Segs(s), Delim)
                    If Fields(1).Contains(":") Then
                        Comps = Split(Fields(1), ":")
                    ElseIf Fields(1).Contains(">") Then
                        Comps = Split(Fields(1), ">")
                    ElseIf Fields(1).Contains(">") Then
                        Comps = Split(Fields(1), ">")
                    ElseIf Fields(1).Contains("<") Then
                        Comps = Split(Fields(1), "<")
                    ElseIf Fields(1).Contains("^") Then
                        Comps = Split(Fields(1), "^")
                    End If
                    '0=ChargeID, 1=TGPID, 2=TGP, 3=CPT, 4=Billed, 5=Allow, 6=Paid, 7=PR, 8=WO, 9=Bal, 10=Sus, 11=Code, 12=Act, 13=Exec
                    ClaimLine(3) = Comps(1) 'CPT
                    If ClaimLine(3) <> "" Then _
                    ClaimLine(1) = GetTGPIDbyCPTAmt(ClaimID, ClaimLine(3), Val(Fields(2)))
                    ClaimLine(2) = GetTGPName(Val(ClaimLine(1)))
                    ClaimLine(4) = Format(Val(ClaimLine(4)) + Val(Fields(2)), "0.00")
                    ClaimLine(6) = Format(Val(ClaimLine(6)) + Val(Fields(3)), "0.00")
                    ClaimLine = UpdateClaimLine(Msg, ClaimLine, s)
                    '
                    If ClaimLine(11) <> "" Then
                        Action = GetERACodeAction(ClaimLine(11))
                    Else
                        Action = "Write Off"
                    End If
                    '
                    dgvClaimDetail.Rows.Add(ClaimID.ToString, ClaimLine(1), ClaimLine(2), ClaimLine(3),
                    Format(Val(ClaimLine(4)), "0.00"), Format(Val(ClaimLine(5)), "0.00"),
                    Format(Val(ClaimLine(6)), "0.00"), Format(Val(ClaimLine(7)), "0.00"),
                    Format(Val(ClaimLine(8)), "0.00"), Format(Val(ClaimLine(9)), "0.00"),
                    Format(Val(ClaimLine(10)), "0.00"), ClaimLine(11), Action, Nothing)
                    '
                    For e As Integer = 0 To ClaimLine.Length - 1
                        ClaimLine(e) = ""
                    Next
                Else
                    Exit For
                End If
            ElseIf Segs(s).Substring(0, InStr(Segs(s), Delim) - 1) = "CLP" Then
                Fields = Split(Segs(s), Delim)
                'CLP*43099*1*396.24*0*396.24*13*7681534197117*81*1
                If Fields(1).Contains("-") Then
                    Fields(1) = Fields(1).Substring(InStr(Fields(1), "-"))
                End If
                If ClaimID <> Val(Fields(1)) Then
                    Exit For
                End If
            ElseIf Segs(s).Substring(0, InStr(Segs(s), Delim) - 1) = "SE" Then
                Exit For
            End If
        Next
        '0=ChargeID, 1=TGPID, 2=TGP, 3=CPT, 4=Billed, 5=Allow, 6=Paid, 7=PR, 8=WO, 9=Bal, 10=Sus, 11=Code, 12=Act, 13=Exec
        Dim RowsToDel() As String = {""}
        Dim del As Integer = -1
        For i As Integer = 0 To dgvClaimDetail.RowCount - 1
            For j As Integer = 0 To dgvClaimDetail.RowCount - 1
                If dgvClaimDetail.Rows(i).Cells(3).Value =
                dgvClaimDetail.Rows(j).Cells(3).Value And i < j Then
                    dgvClaimDetail.Rows(i).Cells(4).Value = Format(
                    Val(dgvClaimDetail.Rows(i).Cells(4).Value) +
                    Val(dgvClaimDetail.Rows(j).Cells(4).Value), "0.00")
                    dgvClaimDetail.Rows(i).Cells(5).Value = Format(
                    Val(dgvClaimDetail.Rows(i).Cells(5).Value) +
                    Val(dgvClaimDetail.Rows(j).Cells(5).Value), "0.00")
                    dgvClaimDetail.Rows(i).Cells(6).Value = Format(
                    Val(dgvClaimDetail.Rows(i).Cells(6).Value) +
                    Val(dgvClaimDetail.Rows(j).Cells(6).Value), "0.00")
                    dgvClaimDetail.Rows(i).Cells(7).Value = Format(
                    Val(dgvClaimDetail.Rows(i).Cells(7).Value) +
                    Val(dgvClaimDetail.Rows(j).Cells(7).Value), "0.00")
                    dgvClaimDetail.Rows(i).Cells(8).Value = Format(
                    Val(dgvClaimDetail.Rows(i).Cells(8).Value) +
                    Val(dgvClaimDetail.Rows(j).Cells(8).Value), "0.00")
                    dgvClaimDetail.Rows(i).Cells(9).Value = Format(
                    Val(dgvClaimDetail.Rows(i).Cells(9).Value) +
                    Val(dgvClaimDetail.Rows(j).Cells(9).Value), "0.00")
                    dgvClaimDetail.Rows(i).Cells(10).Value = Format(
                    Val(dgvClaimDetail.Rows(i).Cells(10).Value) +
                    Val(dgvClaimDetail.Rows(j).Cells(10).Value), "0.00")
                    dgvClaimDetail.Rows(i).Cells(11).Value =
                    dgvClaimDetail.Rows(j).Cells(11).Value
                    dgvClaimDetail.Rows(i).Cells(12).Value =
                    dgvClaimDetail.Rows(j).Cells(12).Value

                    If RowsToDel(UBound(RowsToDel)) <> "" Then _
                    ReDim Preserve RowsToDel(UBound(RowsToDel) + 1)
                    RowsToDel(UBound(RowsToDel)) = j.ToString

                    'del = j
                    Exit For
                End If
            Next
            'If del <> -1 Then
            '    If RowsToDel(UBound(RowsToDel)) <> "" Then _
            '    ReDim Preserve RowsToDel(UBound(RowsToDel) + 1)
            '    RowsToDel(UBound(RowsToDel)) = del.ToString
            '    del = -1
            'End If
        Next
        If RowsToDel(0) <> "" Then
            'RowsToDel = RemoveDuplicates(RowsToDel)
            For e As Integer = RowsToDel.Length - 1 To 0 Step -1
                Try
                    dgvClaimDetail.Rows.RemoveAt(Val(RowsToDel(e)))
                Catch ex As Exception
                End Try
            Next
        End If
        '
        Dim AllClear As Boolean = True
        For e As Integer = 0 To dgvClaimDetail.RowCount - 1
            If Val(dgvClaimDetail.Rows(e).Cells(4).Value) <= 0 OrElse
            dgvClaimDetail.Rows(e).Cells(12).Value = "" Then
                AllClear = False
            End If
        Next
        btnAction.Enabled = AllClear
        If dgvClaimDetail.RowCount > 0 Then btnClear.Enabled = True
        '
    End Sub

    Private Sub DisplayClaimDetail_Old(ByVal ClaimID As Long,
    ByVal PayerClaimID As String, ByVal Msg As String)
        dgvClaimDetail.Rows.Clear()
        Dim Segs() As String
        Dim Fields() As String
        Dim TGPName As String = ""
        Dim Action As String = ""   'Balance, Rebill, Refund, Write Off
        Dim i As Integer
        '
        Segs = Split(Msg, "~")
        For n As Integer = 0 To Segs.Length - 1
            If Microsoft.VisualBasic.Left(Segs(n), 3) = "CLP" Then
                'CLP*43099*1*396.24*0*396.24*13*7681534197117*81*1
                Fields = Split(Segs(n), Delim)
                If Fields(1).Contains("-") Then
                    Fields(1) = Fields(1).Substring(InStr(Fields(1), "-"))
                End If
                If ClaimID.ToString = Fields(1) Then
                    i = n + 1
                    Exit For
                End If
            End If
        Next
        '
        Commons = ""
        ReDim Subs(0)
        Dim ClaimLines() As String = GetClaimLines(ClaimID, Msg)
        'Dim ActField As DataGridViewComboBoxCell
        For i = 0 To ClaimLines.Length - 1
            Fields = Split(ClaimLines(i), "|")
            If Fields.Length >= 10 Then
                'ClaimLine(0=TGPID, 1=CPT, 2=Billed, 3=R P V, 4=Auth, 
                '5=Paid, 6=PR, 7=WO, 8=Sus, 9=Bal, 10=SusCode)
                If Fields(0) <> "" Then
                    TGPName = GetTGPName(Fields(0))
                Else
                    TGPName = GetTGPNameByCPT(Fields(1))
                End If
                'ActField = New DataGridViewComboBoxCell
                ''
                'If Val(Fields(4)) > 0 Then  'authorized
                '    If Val(Fields(6)) > 0 Then
                '        ActField.Items.Add("Bill PR")
                '        ActField.Items.Add("Delete")
                '        ActField.Items.Add("Reverse")
                '    End If
                '    '
                '    If Val(Fields(8)) > 0 Then
                '        ActField.Items.Add("Balance")
                '        ActField.Items.Add("Write Off")
                '    End If
                'Else
                '    ActField.Items.Add("Delete")
                '    ActField.Items.Add("Reverse")
                'End If
                '
                '0 to 13 fields
                dgvClaimDetail.Rows.Add(ClaimID, Fields(0), TGPName,
                Fields(1), Format(Val(Fields(2)), "0.00"), Format(Val(Fields(4)),
                "0.00"), Format(Val(Fields(5)), "0.00"), Format(Val(Fields(6)),
                "0.00"), Format(Val(Fields(7)), "0.00"), Fields(9),
                Format(Val(Fields(8)), "0.00"), Fields(10), "", Nothing)
                '
                If Fields(10) <> "" Then
                    Action = GetERACodeAction(Fields(10))
                Else
                    Action = ""
                End If
                dgvClaimDetail.Rows(dgvClaimDetail.RowCount - 1).Cells(12).Value = Action
                TGPName = ""
            End If
        Next
        '
        If dgvClaimDetail.RowCount > 0 Then btnClear.Enabled = True
        '
    End Sub

    Private Function UpdateClaim(ByVal CLAIM() As String, ByVal ClaimLines() As String) As String()
        'ClaimLines(0=TGPID, 1=CPT, 2=Billed, 3=R P V, 4=Auth, 
        '5=Paid, 6=PR, 7=WO, 8=Sus, 9=Bal, 10=SusCode)
        Dim CLMBill As Double = 0 : Dim CLMAuth As Double = 0
        Dim CLMPaid As Double = 0 : Dim CLMPR As Double = 0
        Dim CLMWO As Double = 0 : Dim CLMSus As Double = 0
        Dim CLMBal As Double = 0 : Dim CLMCodes As String = ""
        For i As Integer = 0 To ClaimLines.Length - 1
            Dim ClaimLine() = Split(ClaimLines(i), "|")
            If ClaimLine.Length > 10 Then
                CLMBill += Val(ClaimLine(2))    'Bill
                CLMAuth += Val(ClaimLine(4))    'Auth
                CLMPaid += Val(ClaimLine(5))    'Paid
                CLMPR += Val(ClaimLine(6))      'PR
                CLMWO += Val(ClaimLine(7))      'WO
                CLMSus += Val(ClaimLine(8))     'Sus
                CLMBal += Val(ClaimLine(9))     'Bal
                If ClaimLine(10) <> "" Then _
                CLMCodes += ClaimLine(10) &
                IIf(Microsoft.VisualBasic.Right(ClaimLine(10),
                2) = ", ", "", ", ")
            End If
            ReDim ClaimLine(10)
        Next
        ReDim ClaimLines(10)
        'CLAIM(0=ID, 1=PCLMID, 2=PatName, 3=Billed, 4=RorP, 
        '5=Auth, 6=Paid, 7=PR, 8=WO, 9=Sus, 10=Bal)
        CLAIM(5) = Format(CLMAuth, "0.00")      'Auth
        CLAIM(6) = Format(CLMPaid, "0.00")      'Paid
        CLAIM(7) = Format(CLMPR, "0.00")        'PR
        CLAIM(8) = Format(CLMWO, "0.00")        'WO
        CLAIM(9) = Format(CLMSus, "0.00")      'Sus
        CLAIM(10) = Format(CLMBal, "0.00")      'Bal
        CLAIM(11) += CLMCodes
        Return CLAIM
    End Function

    Private Function GetClaimIDs(ByVal DocID As String, ByVal Msg As String) As String()
        Dim ClaimIDs() As String = {""}
        Dim Segs() As String
        Dim Fields() As String
        Dim i As Integer
        Dim Matched As Boolean = False
        Segs = Split(Msg, "~")
        For i = 0 To Segs.Length - 1
            Segs(i) = Replace(Segs(i), Chr(10), "")
            Segs(i) = Replace(Segs(i), Chr(13), "")
            If Segs(i) <> Nothing AndAlso Segs(i) <> "" Then
                'ReDim CLAIM(11)
                If Segs(i).Substring(0, InStr(Segs(i), Delim) - 1) = "TRN" Then  'TRN
                    'TRN*1*151215190043066*1591031071
                    'TRN*1*C12338E28880020*1361236610*CP20121203E288800200
                    Fields = Split(Segs(i), Delim)
                    If Fields(2).Contains(" ") Then
                        If DocID = Fields(2).Substring(0, InStr(Fields(2), " ") - 1) Then
                            Matched = True
                            i += 1
                            Exit For
                        End If
                    ElseIf Fields(2).Contains("NO-PAY") Then
                        If DocID = Replace(Fields(2), "NO-PAY-", "") Then
                            Matched = True
                            i += 1
                            Exit For
                        End If
                    Else
                        If DocID = Fields(2) Then
                            Matched = True
                            i += 1
                            Exit For
                        End If
                    End If
                End If
            End If
        Next
        For c As Integer = i To Segs.Length - 1
            Segs(c) = Replace(Segs(c), Chr(10), "")
            Segs(c) = Replace(Segs(c), Chr(13), "")
            If Segs(c) IsNot Nothing AndAlso Segs(c) <> "" Then
                If Segs(c).Substring(0, InStr(Segs(c), Delim) - 1) = "CLP" Then
                    If Matched Then
                        Fields = Split(Segs(c), Delim)
                        If Fields(1).Contains("-") Then
                            Fields(1) = Fields(1).Substring(InStr(Fields(1), "-"))
                        End If
                        If ClaimIDs(UBound(ClaimIDs)) <> "" Then _
                        ReDim Preserve ClaimIDs(UBound(ClaimIDs) + 1)
                        ClaimIDs(UBound(ClaimIDs)) = Fields(1)
                    End If
                ElseIf Segs(c).Substring(0, InStr(Segs(c), Delim) - 1) = "SE" Then
                    Exit For
                End If
            End If
        Next
        ClaimIDs = RemoveDuplicates(ClaimIDs)
        Return ClaimIDs
    End Function

    Private Function GetCLAIM(ByVal ClaimID As String, ByVal Msg As String) As String()
        Dim CLAIM() As String = {"", "", "", "", "", "", "", "", "", "", "", "", ""}
        '0=ID, 1=PCLMID, 2=PatName, 3=Billed, 4=PRV, 5=Auth, _
        '6=Paid, 7=PR, 8=WO, 9=Sus, 10=Bal, 11=Code, 12=date
        Dim Segs() As String
        Dim Fields() As String
        Dim Matched As Boolean = False
        Dim clmPat As String = ""
        Dim clmBilled As Double = 0
        Dim clmAuth As Double = 0
        Dim clmPaid As Double = 0
        Dim clmWO As Double = 0
        Dim clmPR As Double = 0
        Dim clmSus As Double = 0
        Dim clmCodes As String = ""
        Dim i As Integer
        Dim f As Integer
        'Dim ClaimLines() As String
        Segs = Split(Msg, "~")
        For f = 0 To Segs.Length - 1
            Segs(f) = Replace(Segs(f), Chr(10), "")
            Segs(f) = Replace(Segs(f), Chr(13), "")
            If Segs(f) <> Nothing AndAlso Segs(f) <> "" Then
                If Segs(f).Substring(0, InStr(Segs(f), Delim) - 1) = "CLP" Then
                    Fields = Split(Segs(f), Delim)
                    If Fields(1).Contains("-") Then
                        Fields(1) = Fields(1).Substring(InStr(Fields(1), "-"))
                    End If
                    If ClaimID = Fields(1) Then
                        Matched = True
                        Exit For
                    End If
                End If
            End If
        Next
        For i = f To Segs.Length - 1
            If Segs(i) IsNot Nothing AndAlso Segs(i) <> "" Then
                Segs(i) = Replace(Segs(i), Chr(10), "")
                Segs(i) = Replace(Segs(i), Chr(13), "")
                If Segs(i) IsNot Nothing AndAlso Segs(i) <> "" Then
                    If Segs(i).Substring(0, InStr(Segs(i), Delim) - 1) = "CLP" Then
                        'CLP*132772*1*120.04*1.47*10*16*NYC8833116200*81*1
                        'CLP*23283*1*21.59*12.21**MC*2017264703283801*81
                        Fields = Split(Segs(i), Delim)
                        If Fields(1).Contains("-") Then
                            Fields(1) = Fields(1).Substring(InStr(Fields(1), "-"))
                        End If
                        If ClaimID = Fields(1) Then
                            Matched = True
                        Else
                            Matched = False
                        End If
                        If Matched Then
                            CLAIM(0) = ClaimID
                            Try
                                CLAIM(1) = Fields(7)
                            Catch ex As Exception

                            End Try

                            CLAIM(3) = Format(Val(CLAIM(3)) + Val(Fields(3)), "0.00")
                            CLAIM(6) = Format(Val(CLAIM(6)) + Val(Fields(4)), "0.00")
                            If (Fields(2) = "1" Or Fields(2) = "2" Or Fields(2) = "3") Then
                                CLAIM(4) = "P"
                            ElseIf Fields(2) = "4" Then 'denied or refused
                                CLAIM(4) = "R"
                            ElseIf Fields(2) = "22" Then    'reversed
                                CLAIM(4) = "V"
                            End If
                        End If
                    ElseIf Segs(i).Substring(0, InStr(Segs(i), Delim) - 1) = "NM1" Then
                        If Matched Then
                            Fields = Split(Segs(i), Delim)
                            If Fields(1) = "QC" Then
                                CLAIM(2) = Fields(3) & ", " & Fields(4)
                            End If
                        End If
                    ElseIf Segs(i).Substring(0, InStr(Segs(i), Delim) - 1) = "AMT" Then
                        If Matched Then
                            Fields = Split(Segs(i), Delim)
                            If Fields(1) = "AU" Then _
                            CLAIM(5) = Format(Val(Fields(2)), "0.00")
                        End If
                    ElseIf Segs(i).Substring(0, InStr(Segs(i), Delim) - 1) = "DTM" Then
                        If Matched Then
                            Fields = Split(Segs(i), Delim)
                            If Fields(1) = "472" Then   'svc date 
                                CLAIM(12) = DateTime.ParseExact(Fields(2),
                                "yyyyMMdd", CultureInfo.InvariantCulture).ToString(SystemConfig.DateFormat)
                            End If
                        End If
                    ElseIf Matched = False Then
                        Exit For
                    End If
                End If
            End If
        Next
        '
        'CLAIM(0) = ClaimID
        'CLAIM(3) = Format(clmBilled, "0.00")
        'CLAIM(5) = Format(clmAuth, "0.00")
        'CLAIM(6) = Format(clmPaid, "0.00")
        'CLAIM(7) = Format(clmPR, "0.00")
        'CLAIM(8) = Format(clmWO, "0.00")
        'CLAIM(9) = Format(clmSus, "0.00")
        'CLAIM(11) = clmCodes
        Return CLAIM
    End Function

    Private Sub PopulateClaims(ByVal DocNo As String, ByVal Msg As String)
        'Try
        dgvClaims.Rows.Clear()
        dgvClaimDetail.Rows.Clear()
        Dim i As Integer
        Dim InvalidClaims As String = ""
        Dim ListedClaims As String = ""
        Dim ClaimIDs() As String = GetClaimIDs(DocNo, Msg)
        txtClaims.Text = ClaimIDs.Length
        dgvClaims.ReadOnly = True
        PB.Visible = True
        lblProgress.Visible = True
        PB.Maximum = ClaimIDs.Length
        For i = 0 To ClaimIDs.Length - 1
            Dim CLAIM() As String = GetCLAIM(ClaimIDs(i), Msg)
            '0=ID, 1=PCLMID, 2=PatName, 3=Billed, 4=PRV, 5=Auth, _
            '6=Paid, 7=PR, 8=WO, 9=Sus, 10=Bal, 11=Code, 12=date
            Dim ChargeID As String = ValidateChargeID(ClaimIDs(i), CLAIM(2), CLAIM(12))
            If ChargeID <> "" Then
                If Not ClaimProcessed(DocNo, ClaimIDs(i)) AndAlso
                InStr(ListedClaims, ClaimIDs(i) & ", ") = 0 Then
                    displayClaim(CLAIM)
                    ListedClaims += ClaimIDs(i) & ", "
                    'ReDim CLAIM(12)
                End If
            Else
                InvalidClaims += CLAIM(0) & " - " &
                CLAIM(2) & " - DOS: " & CLAIM(12) & vbCrLf
            End If
            'End If
            For c As Integer = 0 To CLAIM.Length - 1
                CLAIM(c) = ""
            Next
            PB.Value = i + 1
            lblProgress.Text = "Loading " & (i + 1).ToString & " of " & ClaimIDs.Length.ToString
            My.Application.DoEvents()
        Next
        dgvClaims.ReadOnly = False
        EOBInfo(4) = dgvClaims.RowCount.ToString
        txtUnprocessed.Text = EOBInfo(4)
        txtProcessed.Text = (Val(txtClaims.Text) - dgvClaims.RowCount).ToString
        PB.Visible = False
        lblProgress.Visible = False
        If InvalidClaims <> "" Then
            Dim RetVal = MsgBox("Following claim(s) after submitting the billing to " &
            "the Clearing House, were either deleted (billing reversed) and appear " &
            "to be never rebilled in Prolis. Please handle such claim(s) through the " &
            "billing system used to bill these claim(s)." & vbCrLf _
            & InvalidClaims & vbCrLf & "Do you want to print this message ?",
            MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Prolis")
            If RetVal = vbYes Then
                Printer.Print("Following claim(s) after submitting the billing to " &
                "the Clearing House, were either deleted (billing reversed) and appear " &
                "to be never rebilled in Prolis. Please handle such claim(s) through the " &
                "billing system used to rebill these claim(s)." & vbCrLf & InvalidClaims & vbCrLf)
            End If
        End If
        'Catch Ex As Exception
        '    MsgBox("Error: " & Ex.Message & " occured")
        'End Try
    End Sub

    Private Sub displayClaim(ByVal CLAIM() As String)
        If dgvClaims.ColumnCount > 14 Then
            '0=ID, 1=PCLMID, 2=PatName, 3=Billed, 4=RorP, 
            '5=Auth, 6=Paid, 7=PR, 8=WO, 9=Sus, 10=Bal, 11=codes, 12=SvcDate
            dgvClaims.Rows.Add(CLAIM(0), CLAIM(1), CLAIM(2), CLAIM(3), CLAIM(5), CLAIM(6),
            CLAIM(7), CLAIM(8), CLAIM(10), CLAIM(9), "None", "", Nothing, CLAIM(11), CLAIM(12))
            '
            If Val(CLAIM(5)) > 0 AndAlso (CLAIM(4) Is Nothing OrElse CLAIM(4) = "") Then
                CLAIM(4) = "P"
            ElseIf CLAIM(4) Is Nothing OrElse CLAIM(4) = "" And Val(CLAIM(5)) = 0 Then
                CLAIM(4) = "R"
            End If
            'setup bill level and discount
            If Val(CLAIM(7)) > 0 Then   'PR
                If Val(CLAIM(6)) > 0 Then   'Inherited
                    cmbBillLevel.SelectedIndex = 10
                    cmbBillLevel.Enabled = True
                    txtDisc.Text = "0.00"
                    txtDisc.Enabled = True
                Else
                    cmbBillLevel.SelectedIndex = 10
                    cmbBillLevel.Enabled = True
                    txtDisc.Text = "0.00"
                    txtDisc.Enabled = True
                End If
            End If
            '
            If Format(Val(CLAIM(3)) - Val(CLAIM(6)) - Val(CLAIM(8)) _
            - Val(CLAIM(7)), "0.00") = Format(Val(CLAIM(10)), "0.00") Then
                dgvClaims.Rows(dgvClaims.RowCount - 1).Cells(12).Value =
                Image.FromFile(My.Application.Info.DirectoryPath & "\Images\Save.ico")
            Else
                dgvClaims.Rows(dgvClaims.RowCount - 1).Cells(12).Value =
                Image.FromFile(My.Application.Info.DirectoryPath & "\Images\Blank.ico")
            End If
            Me.dgvClaims.FirstDisplayedScrollingRowIndex = Me.dgvClaims.RowCount - 1
        End If
    End Sub

    Private Function ValidateChargeID(ByVal ChargeID As String,
    ByVal PatName As String, ByVal SvcDate As String) As String
        Dim Myid As String = ""
        Dim Names() As String = Split(PatName, ",")
        Dim sSQL As String = ""
        If ChargeID <> "" And PatName = "" And Not IsDate(SvcDate) Then
            sSQL = "Select ID from Charges where ArType = 1 and IsPrimary <> 0 and ID = '" & ChargeID &
            "' Union Select a.ID from Charges a inner join (Requisitions b inner join Patients c on " &
            "c.ID = b.Patient_ID) on b.ID = a.Accession_ID where a.ArType = 1 and a.IsPrimary <> 0"
        ElseIf ChargeID <> "" And PatName <> "" And Not IsDate(SvcDate) Then
            sSQL = "Select ID from Charges where ArType = 1 and IsPrimary <> 0 and ID = '" & ChargeID &
            "' Union Select a.ID from Charges a inner join (Requisitions b inner join Patients c on " &
            "c.ID = b.Patient_ID) on b.ID = a.Accession_ID where a.ArType = 1 and a.IsPrimary <> 0 " &
            "and c.LastName like '" & Trim(Names(0)) & "%' and c.FirstName Like '" & Trim(Names(1)) & "%'"
        ElseIf ChargeID <> "" And PatName <> "" And IsDate(SvcDate) Then
            sSQL = "Select ID from Charges where ArType = 1 and IsPrimary <> 0 and ID = '" & ChargeID &
            "' Union Select a.ID from Charges a inner join (Requisitions b inner join Patients c on " &
            "c.ID = b.Patient_ID) on b.ID = a.Accession_ID where a.ArType = 1 and a.IsPrimary <> 0 and " &
            "a.Svc_Date between '" & Format(CDate(SvcDate), SystemConfig.DateFormat) & "' and '" &
            Format(CDate(SvcDate), SystemConfig.DateFormat) & " 23:59:00' and c.LastName like '" & Replace(Trim(Names(0)),
            "'", "''") & "%' and c.FirstName Like '" & Replace(Trim(Names(1)), "'", "''") & "%'"
        End If
        If sSQL <> "" Then
            Dim cnn As New Data.SqlClient.SqlConnection(connString)
            cnn.Open()
            Dim cmdsel As New Data.SqlClient.SqlCommand(sSQL, cnn)
            cmdsel.CommandType = Data.CommandType.Text
            Dim DRsel As Data.SqlClient.SqlDataReader = cmdsel.ExecuteReader
            If DRsel.HasRows Then Myid = ChargeID.ToString
            cnn.Close()
            cnn = Nothing
        End If
        Return Myid
    End Function

    Private Sub dgvEOBs_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEOBs.CellClick
        If e.RowIndex <> -1 Then
            Try
                Clipboard.SetText(dgvEOBs.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                clipboardMsg.ForeColor = Color.Red
                clipboardMsg.Text = dgvEOBs.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & ""
            Catch ex As Exception

            End Try
            Dim Msg As String = OpenERAFile(txtERAFile.Text)

            If Msg <> "" Then _
            PopulateClaims(dgvEOBs.Rows(e.RowIndex).Cells(2).Value, Msg)

        End If
    End Sub

    Private Sub dgvClaimDetail_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvClaimDetail.CellContentClick
        If e.ColumnIndex = 12 Then  'action
            'cmbAction.SelectedIndex = -1
        ElseIf e.ColumnIndex = 13 Then  'Update
            Dim RowIndex As Integer
            Dim CalcSus As Double = 0
            Dim CalcBal As Double = 0
            Dim clmBilled As Double = 0
            Dim clmAuth As Double = 0
            Dim clmPaid As Double = 0
            Dim clmPR As Double = 0
            Dim clmWO As Double = 0
            Dim clmBAL As Double = 0
            Dim clmSus As Double = 0
            For i As Integer = 0 To dgvClaims.RowCount - 1
                If dgvClaims.Rows(i).Cells(0).Value =
                dgvClaimDetail.Rows(e.RowIndex).Cells(0).Value Then
                    RowIndex = i
                    Exit For
                End If
            Next
            If dgvClaimDetail.Rows(e.RowIndex).Cells(1).Value <> "" _
            Or dgvClaimDetail.Rows(e.RowIndex).Cells(3).Value <> "" Then  'billed or substituted
                Dim LineAllow As Double = Val(dgvClaimDetail.Rows(e.RowIndex).Cells(5).Value)
                Dim lineBilled As Double = Val(dgvClaimDetail.Rows(e.RowIndex).Cells(4).Value)
                Dim LinePaid As Double = Val(dgvClaimDetail.Rows(e.RowIndex).Cells(6).Value)
                Dim LinePR As Double = Val(dgvClaimDetail.Rows(e.RowIndex).Cells(7).Value)
                Dim LineSus As Double = Val(dgvClaimDetail.Rows(e.RowIndex).Cells(10).Value)
                Dim LineWO As Double = Val(dgvClaimDetail.Rows(e.RowIndex).Cells(8).Value)
                Dim LineBal As Double = Val(dgvClaimDetail.Rows(e.RowIndex).Cells(9).Value)
                If LinePaid < 0 Then    'Refund
                    If lineBilled <= 0 Then
                        If dgvClaimDetail.Rows(e.RowIndex).Cells(12).Value.ToString.StartsWith("Correct") _
                        OrElse dgvClaimDetail.Rows(e.RowIndex).Cells(12).Value.ToString.StartsWith("Write") Then
                            LineWO = LinePaid * -1
                            'LineWO += (LinePaid * -1) + LineSus
                            LineSus = 0
                        ElseIf dgvClaimDetail.Rows(e.RowIndex).Cells(12).Value.ToString.StartsWith("Bill") Then
                            LinePR += LineSus
                            LineSus = 0
                        ElseIf dgvClaimDetail.Rows(e.RowIndex).Cells(12).Value.ToString.StartsWith("Bal") Then
                            LineBal += LineSus
                            LineSus = 0
                        End If
                    End If
                    'LineSus = 0
                Else    'paying 2nd phase
                    If dgvClaimDetail.Rows(e.RowIndex).Cells(12).Value.ToString.StartsWith("Correct") OrElse
                    dgvClaimDetail.Rows(e.RowIndex).Cells(12).Value.ToString.StartsWith("Support") Then 'To Original
                        LineBal += LineSus
                        'dgvClaimDetail.Rows(e.RowIndex).Cells(4).Value = Format( _
                        'Val(dgvClaimDetail.Rows(e.RowIndex).Cells(5).Value) * -1, "0.00")
                        'LinePR = "0.00"
                        'LineBal = "0.00"
                        'dgvClaimDetail.Rows(e.RowIndex).Cells(6).Value = "0.00"
                        'LineWO = "0.00"
                    Else    'none or bill patient
                        If lineBilled <= 0 Then
                            LineWO = (LinePaid * -1) + LineSus
                        Else    'original claim
                            If dgvClaimDetail.Rows(e.RowIndex).Cells(12).Value.ToString.StartsWith("Bill") Then
                                LinePR += LineSus
                            ElseIf dgvClaimDetail.Rows(e.RowIndex).Cells(12).Value.ToString.StartsWith("Write") Then
                                LineWO = Math.Round(LineWO + Val(dgvClaimDetail.Rows(e.RowIndex).Cells(4).Value) _
                                 - LinePaid - LineWO - LinePR, 2)
                            End If
                            LineBal = LinePR
                        End If
                    End If
                    LineSus = 0
                End If
                'dgvClaimDetail.Rows(e.RowIndex).Cells(6).Value = Format(LinePaid, "0.00")
                dgvClaimDetail.Rows(e.RowIndex).Cells(7).Value = Format(LinePR, "0.00")
                dgvClaimDetail.Rows(e.RowIndex).Cells(9).Value = Format(LineBal, "0.00")
                dgvClaimDetail.Rows(e.RowIndex).Cells(10).Value = Format(LineSus, "0.00")
                dgvClaimDetail.Rows(e.RowIndex).Cells(8).Value = Format(LineWO, "0.00")
                For d As Integer = 0 To dgvClaimDetail.RowCount - 1
                    clmBAL += Val(dgvClaimDetail.Rows(d).Cells(9).Value)
                    clmBilled += Val(dgvClaimDetail.Rows(d).Cells(4).Value)
                    clmAuth += Val(dgvClaimDetail.Rows(d).Cells(5).Value)
                    clmPaid += Val(dgvClaimDetail.Rows(d).Cells(6).Value)
                    clmPR += Val(dgvClaimDetail.Rows(d).Cells(7).Value)
                    clmWO += Val(dgvClaimDetail.Rows(d).Cells(8).Value)
                    clmSus += Val(dgvClaimDetail.Rows(d).Cells(10).Value)
                Next
                dgvClaims.Rows(RowIndex).Cells(3).Value = Format(clmBilled, "0.00")
                dgvClaims.Rows(RowIndex).Cells(4).Value = Format(clmAuth, "0.00")
                dgvClaims.Rows(RowIndex).Cells(5).Value = Format(clmPaid, "0.00")
                dgvClaims.Rows(RowIndex).Cells(6).Value = Format(clmPR, "0.00")
                dgvClaims.Rows(RowIndex).Cells(7).Value = Format(clmWO, "0.00")
                dgvClaims.Rows(RowIndex).Cells(8).Value = Format(clmBAL, "0.00")
                If dgvClaimDetail.Rows(e.RowIndex).Cells(12).Value.ToString.StartsWith("Bill") OrElse
                dgvClaimDetail.Rows(e.RowIndex).Cells(12).Value.ToString.StartsWith("Correct") OrElse
                dgvClaimDetail.Rows(e.RowIndex).Cells(12).Value.ToString.StartsWith("Support") Then
                    dgvClaims.Rows(RowIndex).Cells(10).Value =
                    dgvClaimDetail.Rows(e.RowIndex).Cells(12).Value
                Else
                    dgvClaims.Rows(RowIndex).Cells(10).Value = "None"
                End If
                If Val(dgvClaims.Rows(RowIndex).Cells(6).Value) > 0 Then   'PR
                    If Val(dgvClaims.Rows(RowIndex).Cells(5).Value) > 0 Then   'Inherited
                        cmbBillLevel.SelectedIndex = 10
                        'cmbBillLevel.Enabled = False
                        txtDisc.Text = "0.00"
                        txtDisc.Enabled = True
                    Else
                        cmbBillLevel.SelectedIndex = 10
                        cmbBillLevel.Enabled = True
                        txtDisc.Text = "0.00"
                        txtDisc.Enabled = True
                    End If
                    txtBillReason.Text = "Patient Responsibility"
                    'ElseIf Val(dgvClaims.Rows(RowIndex).Cells(6).Value) < 0 Then   'Credit PR billed
                End If
                '
                If Math.Round(clmBilled - clmPaid - clmWO - clmPR, 2) = 0 OrElse
                Format(clmBilled + Val(dgvClaims.Rows(RowIndex).Cells(4).Value), "0.00") = clmBAL.ToString OrElse
                clmBilled.ToString = Format(clmPaid + clmWO + clmPR + clmBAL, "0.00") Then
                    dgvClaims.Rows(RowIndex).Cells(12).Value =
                    Image.FromFile(My.Application.Info.DirectoryPath _
                    & "\Images\Save.ico")
                Else
                    dgvClaims.Rows(RowIndex).Cells(12).Value =
                    Image.FromFile(My.Application.Info.DirectoryPath _
                    & "\Images\Blank.ico")
                End If
                '
                If (dgvClaimDetail.Rows(e.RowIndex).Cells(11).Value IsNot Nothing _
                AndAlso Trim(dgvClaimDetail.Rows(e.RowIndex).Cells(11).Value) <> "") _
                AndAlso (dgvClaimDetail.Rows(e.RowIndex).Cells(12).Value IsNot Nothing _
                AndAlso Trim(dgvClaimDetail.Rows(e.RowIndex).Cells(12).Value) <> "") Then
                    UpdateERACodeAction(
                    Trim(dgvClaimDetail.Rows(e.RowIndex).Cells(11).Value),
                    Trim(dgvClaimDetail.Rows(e.RowIndex).Cells(12).Value))
                End If
            End If
        End If
    End Sub

    Private Sub txtDisc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDisc.KeyPress
        Prices(sender, e)
    End Sub

    'Private Sub cmbAction_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAction.SelectedIndexChanged
    '    If cmbAction.SelectedIndex = -1 Then
    '        btnAction.Enabled = False
    '    Else
    '        For i As Integer = 0 To dgvClaimDetail.RowCount - 1
    '            dgvClaimDetail.Rows(i).Cells(12).Value = cmbAction.SelectedItem.ToString
    '        Next
    '        btnAction.Enabled = True
    '    End If
    'End Sub

    Private Sub UpdateSaveOption(ByVal CIndex As Integer)
        'Setup the readiness 'Temur  _        Val(dgvClaims.Rows(CIndex).Cells(6).Value), "0.00") = Format(Val(dgvClaims.Rows(CIndex).Cells(8).Value), "0.00")
        'here i added + 
        If Format(Val(dgvClaims.Rows(CIndex).Cells(3).Value) - (Val(dgvClaims.Rows(CIndex).Cells(5).Value) + Val(dgvClaims.Rows(CIndex).Cells(7).Value) + Val(dgvClaims.Rows(CIndex).Cells(6).Value)), "0.00") = 0 _
    OrElse
        Format(Val(dgvClaims.Rows(CIndex).Cells(3).Value), "0.00") =
        Format(Val(dgvClaims.Rows(CIndex).Cells(8).Value), "0.00") Then
            dgvClaims.Rows(CIndex).Cells(12).Value =
            Image.FromFile(My.Application.Info.DirectoryPath _
            & "\Images\Save.ico")
        Else
            dgvClaims.Rows(CIndex).Cells(12).Value =
            Image.FromFile(My.Application.Info.DirectoryPath _
            & "\Images\Blank.ico")
        End If
    End Sub

    Private Sub btnAction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAction.Click
        If dgvClaimDetail.RowCount > 0 Then
            Dim CIndex As Integer
            For i As Integer = 0 To dgvClaims.RowCount - 1
                If dgvClaims.Rows(i).Cells(0).Value = dgvClaimDetail.Rows(0).Cells(0).Value Then
                    CIndex = i
                    Exit For
                End If
            Next
            For i As Integer = 0 To dgvClaimDetail.RowCount - 1
                Dim CalcSus As Double = 0
                Dim CalcBal As Double = 0
                Dim clmBilled As Double = 0
                Dim clmPaid As Double = 0
                Dim clmPR As Double = 0
                Dim clmWO As Double = 0
                Dim clmBAL As Double = 0
                Dim clmSus As Double = 0
                '


                If dgvClaimDetail.Rows(i).Cells(1).Value <> "" _
                AndAlso (dgvClaimDetail.Rows(i).Cells(2).Value <> "" _
                Or dgvClaimDetail.Rows(i).Cells(3).Value <> "") Then  'billed or substituted
                    Dim lineBilled As Double = Val(dgvClaimDetail.Rows(i).Cells(4).Value)
                    Dim LinePaid As Double = Val(dgvClaimDetail.Rows(i).Cells(6).Value)
                    Dim LinePR As Double = Val(dgvClaimDetail.Rows(i).Cells(7).Value)
                    Dim LineSus As Double = Val(dgvClaimDetail.Rows(i).Cells(10).Value)
                    Dim LineWO As Double = Val(dgvClaimDetail.Rows(i).Cells(8).Value)
                    Dim LineBal As Double = Val(dgvClaimDetail.Rows(i).Cells(9).Value)
                    If Math.Round(LineSus, 2) <> Math.Round(LineWO, 2) Then
                        If dgvClaimDetail.Rows(i).Cells(12).Value.ToString.StartsWith("Bill") Then
                            LinePR += LineSus
                        ElseIf dgvClaimDetail.Rows(i).Cells(12).Value.ToString.StartsWith("Bal") Then
                            LineBal += LineSus
                        ElseIf dgvClaimDetail.Rows(i).Cells(12).Value.ToString.StartsWith("Rev") Then
                            'CN.Execute("Update Req_Billable Set Bill_Status = 'U', Billed_On = NULL, " & _
                            '"Billed_By = NULL where Accession_ID in (Select Accession_ID from Charges " & _
                            '"where ID = " & Val(dgvClaims.Rows(RowIndex).Cells(0).Value) & ") " & _
                            '"and TGP_ID = " & Val(dgvClaimDetail.Rows(e.RowIndex).Cells(1).Value))
                            'CN.Execute("Delete from Charge_Detail where Charge_ID = " & _
                            'Val(dgvClaims.Rows(RowIndex).Cells(0).Value) & " and " & _
                            '"TGP_ID = " & Val(dgvClaimDetail.Rows(e.RowIndex).Cells(1).Value))
                            'CN.Execute("Update Charges Set NetAmount = (Select sum(Extend) from Charge_Detail " & _
                            '"where Charge_ID = " & Val(dgvClaims.Rows(RowIndex).Cells(0).Value) & "), GrossAmount " & _
                            '"= (Select sum(Extend) from Charge_Detail where Charge_ID = " & _
                            'Val(dgvClaims.Rows(RowIndex).Cells(0).Value) & ") where ID = " & _
                            'Val(dgvClaims.Rows(RowIndex).Cells(0).Value))
                            'If ReversedInvoices(UBound(ReversedInvoices)) <> "" Then _
                            'ReDim Preserve ReversedInvoices(UBound(ReversedInvoices) + 1)
                            'ReversedInvoices(UBound(ReversedInvoices)) = _
                            'dgvClaims.Rows(i).Cells(0).Value
                            LineWO += LineSus
                        Else
                            LineWO += LineSus
                        End If
                    End If
                    LineSus = 0
                    dgvClaimDetail.Rows(i).Cells(7).Value = Format(LinePR, "0.00")
                    dgvClaimDetail.Rows(i).Cells(9).Value = Format(LineBal, "0.00")
                    dgvClaimDetail.Rows(i).Cells(10).Value = Format(LineSus, "0.00")
                    dgvClaimDetail.Rows(i).Cells(8).Value = Format(LineWO, "0.00")
                    For d As Integer = 0 To dgvClaimDetail.RowCount - 1
                        clmBAL += Val(dgvClaimDetail.Rows(d).Cells(9).Value)
                        clmBilled += Val(dgvClaimDetail.Rows(d).Cells(4).Value)
                        clmPaid += Val(dgvClaimDetail.Rows(d).Cells(6).Value)
                        clmPR += Val(dgvClaimDetail.Rows(d).Cells(7).Value)
                        clmWO += Val(dgvClaimDetail.Rows(d).Cells(8).Value)
                        clmSus += Val(dgvClaimDetail.Rows(d).Cells(10).Value)
                    Next
                    dgvClaims.Rows(CIndex).Cells(3).Value = Format(clmBilled, "0.00")
                    dgvClaims.Rows(CIndex).Cells(5).Value = Format(clmPaid, "0.00")
                    dgvClaims.Rows(CIndex).Cells(6).Value = Format(clmPR, "0.00")
                    dgvClaims.Rows(CIndex).Cells(7).Value = Format(clmWO, "0.00")
                    dgvClaims.Rows(CIndex).Cells(8).Value = Format(clmBAL, "0.00")
                    If dgvClaimDetail.Rows(i).Cells(12).Value.ToString.StartsWith("Bill") Then
                        dgvClaims.Rows(CIndex).Cells(10).Value =
                        dgvClaimDetail.Rows(i).Cells(12).Value
                    Else
                        dgvClaims.Rows(CIndex).Cells(10).Value = "None"
                    End If
                    If Val(dgvClaims.Rows(CIndex).Cells(6).Value) > 0 Then   'PR
                        If Val(dgvClaims.Rows(CIndex).Cells(5).Value) > 0 Then   'Inherited
                            cmbBillLevel.SelectedIndex = 10
                            cmbBillLevel.Enabled = True
                            ''Temur'  txtDisc.Text = "0.00"
                            txtDisc.Enabled = True
                        Else
                            cmbBillLevel.SelectedIndex = 10
                            cmbBillLevel.Enabled = True
                            ''Temur' txtDisc.Text = "0.00"
                            txtDisc.Enabled = True
                        End If
                        txtBillReason.Text = "Patient Responsibility"
                    End If
                    '
                    'Temur start



                    If dgvClaimDetail.Rows(i).Cells(12).Value.ToString.StartsWith("Bill Patient") Then
                        Dim balance = Val(dgvClaimDetail.Rows(i).Cells(8 + 1).Value)
                        Dim pr = Val(dgvClaimDetail.Rows(i).Cells(6 + 1).Value)
                        If Val(txtDisc.Text) > 0 Then

                            Dim pr2 = Val(dgvClaimDetail.Rows(i).Cells(6 + 1).Value)  'PR
                            Dim wo = Val(dgvClaimDetail.Rows(i).Cells(7 + 1).Value)  'WO
                            'Dim balance = Val(dgvClaimDetail.Rows(d).Cells(8 + 1).Value)   'Balance
                            If Val(txtDisc.Text) > 0 Then
                                pr = Format(Val(pr) -
                                (Val(txtDisc.Text) / 100 * Val(pr)), "0.00")
                            End If
                            wo += pr2 - pr
                            dgvClaimDetail.Rows(i).Cells(7 + 1).Value = wo
                            dgvClaimDetail.Rows(i).Cells(6 + 1).Value = pr
                            dgvClaimDetail.Rows(i).Cells(9).Value = pr
                        End If
                        Dim charg = dgvClaims.Rows(CIndex).Cells(0).Value
                        'ExecuteSqlProcedure("update Charges set GrossAmount = GrossAmount - " & pr & ",NetAmount=NetAmount - " & pr & " where ID =" & charg)



                    End If

                    ''Temur'
                    txtDisc.Text = "0.00"
                    'Temur End

                    If Format(clmBilled - clmPaid - clmWO - clmPR, "0.00") = clmBAL Then
                        dgvClaims.Rows(CIndex).Cells(12).Value =
                        Image.FromFile(My.Application.Info.DirectoryPath _
                        & "\Images\Save.ico")
                    Else
                        dgvClaims.Rows(CIndex).Cells(12).Value =
                        Image.FromFile(My.Application.Info.DirectoryPath _
                        & "\Images\Blank.ico")
                    End If
                    '
                    If (dgvClaimDetail.Rows(i).Cells(11).Value IsNot Nothing _
                    AndAlso Trim(dgvClaimDetail.Rows(i).Cells(11).Value) <> "") _
                    AndAlso (dgvClaimDetail.Rows(i).Cells(12).Value IsNot Nothing _
                    AndAlso Trim(dgvClaimDetail.Rows(i).Cells(12).Value) <> "") Then
                        UpdateERACodeAction(
                        Trim(dgvClaimDetail.Rows(i).Cells(11).Value),
                        Trim(dgvClaimDetail.Rows(i).Cells(12).Value))
                    End If
                End If
            Next

            ''Dim CalcSus As Single = 0
            ''Dim CalcBal As Single = 0
            'Dim clmAuth As Single = Val(dgvClaims.Rows(CIndex).Cells(4).Value)
            'Dim clmPaid As Single = Val(dgvClaims.Rows(CIndex).Cells(5).Value)
            'Dim clmBilled As Single = Val(dgvClaims.Rows(CIndex).Cells(3).Value)
            'Dim clmPR As Single = Val(dgvClaims.Rows(CIndex).Cells(6).Value)
            'Dim clmWO As Single = Val(dgvClaims.Rows(CIndex).Cells(7).Value)
            'Dim clmBAL As Single = Val(dgvClaims.Rows(CIndex).Cells(8).Value)
            'Dim clmSus As Single = Val(dgvClaims.Rows(CIndex).Cells(9).Value)
            ''
            'If cmbAction.SelectedItem.ToString = "Balance" Then
            '    For i As Integer = 0 To dgvClaimDetail.RowCount - 1
            '        Dim LineSus As Single = Val(dgvClaimDetail.Rows(i).Cells(10).Value)
            '        Dim LineBal As Single = Val(dgvClaimDetail.Rows(i).Cells(9).Value)
            '        dgvClaimDetail.Rows(i).Cells(9).Value = _
            '        Format(LineBal + LineSus, "0.00")
            '        '
            '        clmBAL = Val(dgvClaims.Rows(CIndex).Cells(8).Value)
            '        clmSus = Val(dgvClaims.Rows(CIndex).Cells(9).Value)
            '        dgvClaims.Rows(CIndex).Cells(8).Value = _
            '        Format(clmBAL + LineSus, "0.00")
            '        dgvClaims.Rows(CIndex).Cells(9).Value = _
            '        Format(clmSus - LineSus, "0.00")
            '        dgvClaimDetail.Rows(i).Cells(10).Value = "0.00"
            '        '
            '        dgvClaims.Rows(CIndex).Cells(13).Value = _
            '        Microsoft.VisualBasic.Mid(dgvClaims.Rows(CIndex).Cells(13).Value, _
            '        InStr(dgvClaims.Rows(CIndex).Cells(13).Value, _
            '        dgvClaimDetail.Rows(i).Cells(11).Value) + _
            '        Len(dgvClaimDetail.Rows(i).Cells(11).Value))
            '    Next
            '    dgvClaims.Rows(CIndex).Cells(10).Value = "Balance"
            'ElseIf cmbAction.SelectedItem.ToString = "Bill Patient" Then
            '    For i As Integer = 0 To dgvClaimDetail.RowCount - 1
            '        If dgvClaimDetail.Rows(i).Cells(2).Value <> "" Then
            '            If Val(dgvClaimDetail.Rows(i).Cells(10).Value) <> 0 Then    'Sus 
            '                Dim LineSus As Single = Val(dgvClaimDetail.Rows(i).Cells(10).Value)
            '                dgvClaimDetail.Rows(i).Cells(7).Value = Format( _
            '                Val(dgvClaimDetail.Rows(i).Cells(7).Value) + LineSus, "0.00")
            '                clmPR += Math.Round(Val(dgvClaimDetail.Rows(i).Cells(7).Value), 2)
            '                dgvClaimDetail.Rows(i).Cells(10).Value = "0.00"
            '                clmBAL += Val(dgvClaimDetail.Rows(i).Cells(9).Value)
            '                '
            '                dgvClaims.Rows(CIndex).Cells(13).Value = _
            '                Microsoft.VisualBasic.Mid(dgvClaims.Rows(CIndex).Cells(13).Value, _
            '                InStr(dgvClaims.Rows(CIndex).Cells(13).Value, _
            '                dgvClaimDetail.Rows(i).Cells(11).Value) + _
            '                Len(dgvClaimDetail.Rows(i).Cells(11).Value))
            '            Else    'Sus = 0
            '                If Val(dgvClaimDetail.Rows(i).Cells(5).Value) > 0 Then 'auth
            '                    If Len(dgvClaims.Rows(CIndex).Cells(13).Value) + _
            '                    Len(dgvClaimDetail.Rows(i).Cells(11).Value) > 0 Then _
            '                    dgvClaims.Rows(CIndex).Cells(13).Value = _
            '                    Microsoft.VisualBasic.Mid(dgvClaims.Rows(CIndex).Cells(13).Value, _
            '                    InStr(dgvClaims.Rows(CIndex).Cells(13).Value, _
            '                    dgvClaimDetail.Rows(i).Cells(11).Value) + _
            '                    Len(dgvClaimDetail.Rows(i).Cells(11).Value))
            '                End If
            '            End If
            '        End If
            '    Next
            '    '
            '    dgvClaims.Rows(CIndex).Cells(6).Value = Format(clmPR, "0.00")
            '    dgvClaims.Rows(CIndex).Cells(10).Value = "Bill Patient"
            '    If clmPR > 0 Then
            '        If dgvClaims.Rows(CIndex).Cells(10).Value = "" OrElse _
            '        dgvClaims.Rows(CIndex).Cells(10).Value = "None" Then
            '            If dgvClaims.Rows(CIndex).Cells(11).Value <> "" Then    'Has PayerID
            '                dgvClaims.Rows(CIndex).Cells(10).Value = "Bill Insurance"
            '            Else
            '                dgvClaims.Rows(CIndex).Cells(10).Value = "Bill Patient"
            '                dgvClaims.Rows(CIndex).Cells(11).ReadOnly = True
            '            End If
            '        End If
            '        '
            '        If Val(dgvClaims.Rows(CIndex).Cells(5).Value) > 0 Then   'Inherited
            '            cmbBillLevel.SelectedIndex = 10
            '            cmbBillLevel.Enabled = False
            '            txtDisc.Text = "0.00"
            '            txtDisc.Enabled = False
            '        Else
            '            cmbBillLevel.SelectedIndex = 10
            '            cmbBillLevel.Enabled = True
            '            txtDisc.Text = "0.00"
            '            txtDisc.Enabled = True
            '        End If
            '        txtBillReason.Text = "Patient Responsibility"
            '    End If
            'ElseIf cmbAction.SelectedItem.ToString = "Bill Insurance" Then
            '    For i As Integer = 0 To dgvClaimDetail.RowCount - 1
            '        If dgvClaimDetail.Rows(i).Cells(2).Value <> "" Then
            '            If Val(dgvClaimDetail.Rows(i).Cells(10).Value) <> 0 Then    'Sus 
            '                Dim LineSus As Single = Val(dgvClaimDetail.Rows(i).Cells(10).Value)
            '                dgvClaimDetail.Rows(i).Cells(7).Value = Format( _
            '                Val(dgvClaimDetail.Rows(i).Cells(7).Value) + LineSus, "0.00")
            '                clmPR += Math.Round(Val(dgvClaimDetail.Rows(i).Cells(7).Value), 2)
            '                dgvClaimDetail.Rows(i).Cells(10).Value = "0.00"
            '                clmBAL += Val(dgvClaimDetail.Rows(i).Cells(9).Value)
            '                '
            '                dgvClaims.Rows(CIndex).Cells(13).Value = _
            '                Microsoft.VisualBasic.Mid(dgvClaims.Rows(CIndex).Cells(13).Value, _
            '                InStr(dgvClaims.Rows(CIndex).Cells(13).Value, _
            '                dgvClaimDetail.Rows(i).Cells(11).Value) + _
            '                Len(dgvClaimDetail.Rows(i).Cells(11).Value))
            '            Else    'Sus = 0
            '                If Val(dgvClaimDetail.Rows(i).Cells(5).Value) > 0 Then 'auth
            '                    If Len(dgvClaims.Rows(CIndex).Cells(13).Value) + _
            '                    Len(dgvClaimDetail.Rows(i).Cells(11).Value) > 0 Then _
            '                    dgvClaims.Rows(CIndex).Cells(13).Value = _
            '                    Microsoft.VisualBasic.Mid(dgvClaims.Rows(CIndex).Cells(13).Value, _
            '                    InStr(dgvClaims.Rows(CIndex).Cells(13).Value, _
            '                    dgvClaimDetail.Rows(i).Cells(11).Value) + _
            '                    Len(dgvClaimDetail.Rows(i).Cells(11).Value))
            '                End If
            '            End If
            '        End If
            '    Next
            '    txtBillReason.Text = ""
            '    dgvClaims.Rows(CIndex).Cells(6).Value = Format(clmPR, "0.00")
            '    dgvClaims.Rows(CIndex).Cells(10).Value = "Bill Insurance"
            '    If clmPR > 0 Then
            '        If Not dgvClaims.Rows(CIndex).Cells(10).Value.ToString.StartsWith("Bill") Then
            '            If dgvClaims.Rows(CIndex).Cells(11).Value <> "" Then    'Has PayerID
            '                dgvClaims.Rows(CIndex).Cells(10).Value = "Bill Insurance"
            '            Else
            '                dgvClaims.Rows(CIndex).Cells(10).Value = "Bill Patient"
            '                dgvClaims.Rows(CIndex).Cells(11).ReadOnly = True
            '            End If
            '        End If
            '    End If
            'ElseIf cmbAction.SelectedItem.ToString = "Refund" Then 'Refund
            '    For i As Integer = 0 To dgvClaimDetail.RowCount - 1
            '        'dgvDetail(0=ChargeID, 1=TGPID, 2=Component, 3=CPT, 4=Billed, 5=Auth, 
            '        '6=Paid, 7=PR, 8=WO, 9=Bal, 10=Susp, 11=Code, 12=Act Sel, 13=Act)
            '        If dgvClaimDetail.Rows(i).Cells(12).Value.ToString.StartsWith("Refund") Then
            '            dgvClaimDetail.Rows(i).Cells(4).Value = _
            '            Format(Val(dgvClaimDetail.Rows(i).Cells(5).Value) _
            '            + Val(dgvClaimDetail.Rows(i).Cells(8).Value), "0.00")
            '            clmBilled += Val(dgvClaimDetail.Rows(i).Cells(4).Value)
            '            dgvClaimDetail.Rows(i).Cells(10).Value = "0.00"
            '            clmSus = 0
            '            clmBAL += Val(dgvClaimDetail.Rows(i).Cells(9).Value)
            '            clmWO += Val(dgvClaimDetail.Rows(i).Cells(8).Value)
            '            clmPR += Val(dgvClaimDetail.Rows(i).Cells(7).Value)
            '            clmAuth += Val(dgvClaimDetail.Rows(i).Cells(5).Value)
            '            clmPaid += Val(dgvClaimDetail.Rows(i).Cells(6).Value)
            '        End If
            '    Next
            '    'dgvClaim(0=ChargeID, 1=PayerClm, 2=Patient, 3=Billed, 4=Auth, 5=Paid, 6=PR, 
            '    '7=WO, 8=Bal, 9=Susp, 10=Act Sel, 11=Payer ID, 12=act, 13=Codes, 14=SvcDate)
            '    dgvClaims.Rows(CIndex).Cells(3).Value = Format(clmBilled, "0.00")
            '    dgvClaims.Rows(CIndex).Cells(4).Value = Format(clmAuth, "0.00")
            '    dgvClaims.Rows(CIndex).Cells(5).Value = Format(clmPaid, "0.00")
            '    dgvClaims.Rows(CIndex).Cells(6).Value = Format(clmPR, "0.00")
            '    dgvClaims.Rows(CIndex).Cells(7).Value = Format(clmWO, "0.00")
            '    dgvClaims.Rows(CIndex).Cells(8).Value = Format(clmBAL, "0.00")
            '    dgvClaims.Rows(CIndex).Cells(9).Value = Format(clmSus, "0.00")
            '    dgvClaims.Rows(CIndex).Cells(10).Value = "Refund"
            '    dgvClaims.Rows(CIndex).Cells(13).Value = ""
            '    '
            'Else    'Write Off
            '    clmWO = 0 : clmSus = 0 : clmBAL = 0
            '    For i As Integer = 0 To dgvClaimDetail.RowCount - 1
            '        Dim LineSus As Single = Val(dgvClaimDetail.Rows(i).Cells(10).Value)
            '        Dim LineWO As Single = Val(dgvClaimDetail.Rows(i).Cells(8).Value)
            '        If dgvClaimDetail.Rows(i).Cells(2).Value <> "" Then
            '            If Math.Round(LineSus, 2) <> Math.Round(LineWO, 2) Then
            '                LineWO = LineWO + LineSus
            '                LineSus = 0
            '                clmSus = Math.Round(clmSus - LineSus, 2)
            '            Else
            '                LineSus = Math.Round(LineSus - LineWO, 2)
            '                clmSus = Math.Round(clmSus - LineSus, 2)
            '            End If
            '            If Math.Round(Val(dgvClaimDetail.Rows(i).Cells(4).Value) - _
            '            Val(dgvClaimDetail.Rows(i).Cells(6).Value), 2) <> LineWO Then
            '                LineWO = Math.Round(Val(dgvClaimDetail.Rows(i).Cells(4).Value) - _
            '                Val(dgvClaimDetail.Rows(i).Cells(6).Value), 2)
            '                clmWO = Math.Round(clmWO + LineWO, 2)
            '            End If
            '            '
            '            dgvClaimDetail.Rows(i).Cells(10).Value = Format(LineSus, "0.00")
            '            dgvClaimDetail.Rows(i).Cells(8).Value = Format(LineWO, "0.00")
            '            '
            '            If dgvClaims.Rows(CIndex).Cells(13).Value <> "" And _
            '            Len(dgvClaims.Rows(CIndex).Cells(13).Value) >= _
            '            Len(dgvClaimDetail.Rows(i).Cells(11).Value) Then
            '                dgvClaims.Rows(CIndex).Cells(13).Value = _
            '                Microsoft.VisualBasic.Mid(dgvClaims.Rows(CIndex).Cells(13).Value, _
            '                InStr(dgvClaims.Rows(CIndex).Cells(13).Value, _
            '                dgvClaimDetail.Rows(i).Cells(11).Value) + _
            '                Len(dgvClaimDetail.Rows(i).Cells(11).Value))
            '            End If
            '        Else    'CPT not in the ERA
            '            If Math.Round(LineSus, 2) <> Math.Round(LineWO, 2) Then
            '                LineWO = LineWO + LineSus
            '                clmWO = Math.Round(clmWO + LineSus, 2)
            '                clmSus = Math.Round(clmSus - LineSus, 2)
            '                LineSus = 0
            '            Else
            '                LineSus = Math.Round(LineSus - LineWO, 2)
            '                clmSus = Math.Round(clmSus - LineWO, 2)
            '            End If
            '            '
            '            dgvClaimDetail.Rows(i).Cells(10).Value = Format(LineSus, "0.00")
            '            dgvClaimDetail.Rows(i).Cells(8).Value = Format(LineWO, "0.00")
            '            '
            '            dgvClaims.Rows(CIndex).Cells(7).Value = Format(clmWO, "0.00")
            '            dgvClaims.Rows(CIndex).Cells(9).Value = Format(clmSus, "0.00")
            '            '
            '            If Len(dgvClaims.Rows(CIndex).Cells(13).Value) > _
            '            Len(dgvClaimDetail.Rows(i).Cells(11).Value) Then
            '                dgvClaims.Rows(CIndex).Cells(13).Value = _
            '                Microsoft.VisualBasic.Mid(dgvClaims.Rows(CIndex).Cells(13).Value, _
            '                InStr(dgvClaims.Rows(CIndex).Cells(13).Value, _
            '                dgvClaimDetail.Rows(i).Cells(11).Value) + _
            '                Len(dgvClaimDetail.Rows(i).Cells(11).Value))
            '                If Math.Round(Val(dgvClaims.Rows(CIndex).Cells(3).Value) - _
            '                Val(dgvClaims.Rows(CIndex).Cells(5).Value) - _
            '                Val(dgvClaims.Rows(CIndex).Cells(6).Value) - _
            '                Val(dgvClaims.Rows(CIndex).Cells(7).Value), 2) = 0 And _
            '                Math.Round(Val(dgvClaims.Rows(CIndex).Cells(9).Value), 2) = 0 AndAlso _
            '                (dgvClaims.Rows(CIndex).Cells(13).Value Is Nothing OrElse _
            '                dgvClaims.Rows(CIndex).Cells(13).Value = "") Then
            '                    dgvClaims.Rows(CIndex).Cells(12).Value = _
            '                    Image.FromFile(My.Application.Info.DirectoryPath _
            '                    & "\Images\Save.ico")
            '                Else
            '                    dgvClaims.Rows(CIndex).Cells(12).Value = _
            '                    Image.FromFile(My.Application.Info.DirectoryPath _
            '                    & "\Images\Blank.ico")
            '                End If
            '            End If
            '        End If
            '    Next
            '    dgvClaims.Rows(CIndex).Cells(5).Value = Format(clmPaid, "0.00")
            '    dgvClaims.Rows(CIndex).Cells(7).Value = Format(clmWO, "0.00")
            '    dgvClaims.Rows(CIndex).Cells(9).Value = Format(clmSus, "0.00")
            '    dgvClaims.Rows(CIndex).Cells(8).Value = Format(clmBAL, "0.00")
            '    dgvClaims.Rows(CIndex).Cells(6).Value = Format(clmPR, "0.00")
            '    dgvClaims.Rows(CIndex).Cells(10).Value = "None"
            '    If clmPR > 0 Then
            '        If Not dgvClaims.Rows(CIndex).Cells(10).Value.ToString.StartsWith("Bill") Then
            '            If dgvClaims.Rows(CIndex).Cells(11).Value <> "" Then    'Has PayerID
            '                dgvClaims.Rows(CIndex).Cells(10).Value = "Bill Insurance"
            '            Else
            '                dgvClaims.Rows(CIndex).Cells(10).Value = "Bill Patient"
            '                dgvClaims.Rows(CIndex).Cells(11).ReadOnly = True
            '            End If
            '        End If
            '    End If
            '    'dgvClaims.Rows(CIndex).Cells(7).Value = Format(clmWO, "0.00")
            '    'dgvClaims.Rows(CIndex).Cells(8).Value = Format(clmBAL, "0.00")
            '    'dgvClaims.Rows(CIndex).Cells(9).Value = Format(clmSus, "0.00")
            'End If
            UpdateSaveOption(CIndex)
        End If
    End Sub

    Private Sub dgvClaims_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvClaims.CellDoubleClick

        If e.ColumnIndex = 11 Then  'pull second payer ID
            If (dgvClaims.Rows(e.RowIndex).Cells(11).Value Is Nothing _
            OrElse dgvClaims.Rows(e.RowIndex).Cells(11).Value <> "") AndAlso
            dgvClaims.Rows(e.RowIndex).Cells(10).Value.ToString.StartsWith("Bill Ins") Then
                Dim cnsp As New SqlClient.SqlConnection(connString)
                cnsp.Open()
                Dim cmdsp As New SqlClient.SqlCommand("Select * from Coverages where " &
                "Preference = 'S' and Patient_ID in (Select Patient_ID from " &
                "Requisitions where ID in (Select Accession_ID from Charges where " &
                "ID = " & Val(dgvClaims.Rows(e.RowIndex).Cells(0).Value) & "))", cnsp)
                cmdsp.CommandType = CommandType.Text
                Dim drsp As SqlClient.SqlDataReader = cmdsp.ExecuteReader
                If drsp.HasRows Then
                    While drsp.Read
                        dgvClaims.Rows(e.RowIndex).Cells(11).Value = drsp("Insurance_ID")
                    End While
                End If
                cnsp.Close()
                cnsp = Nothing
            End If
        End If
    End Sub

    Private Sub frmERA_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If ReversedInvoices(UBound(ReversedInvoices)) <> "" Then
            Dim Invoices As String = ""
            For i As Integer = 0 To ReversedInvoices.Length - 1
                Invoices += ReversedInvoices(i) & vbCrLf
            Next
            Dim InvMsg As String = "Some line items of or entire of the following invoices were reversed by Prolis " &
            "in this session;" & vbCrLf & "****************" & vbCrLf & Invoices & "****************" & vbCrLf
            Dim RetVal = MsgBox(InvMsg & "Click the 'Yes' button to print. Do you " &
            "want to print?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Prolis")
            If RetVal = vbYes Then
                Printer.Print(InvMsg)
            End If
        End If
    End Sub

    Private Sub frmERA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpPost.Format = DateTimePickerFormat.Custom
        dtpPost.CustomFormat = SystemConfig.DateFormat
        dtpPost.Value = Date.Now
    End Sub

    Private Sub dtpPost_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpPost.ValueChanged
        If dgvEOBs.RowCount > 0 AndAlso dgvEOBs.SelectedRows(0).Index <> -1 Then
            EOB(3) = dtpPost.Value.ToString(SystemConfig.DateFormat)
            dgvEOBs.SelectedRows(0).Cells(3).Value = EOB(3)
        End If
    End Sub

    Private Sub dgvClaimDetail_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvClaimDetail.CellContentDoubleClick
        If e.ColumnIndex = 11 Then  'show code description
            If dgvClaimDetail.Rows(e.RowIndex).Cells(e.ColumnIndex).Value IsNot Nothing AndAlso
            dgvClaimDetail.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString <> "" Then
                frmReasonLookUp.txtTerm.Text = dgvClaimDetail.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                frmReasonLookUp.ShowDialog()
                'frmReasonLookUp.btnLook.PerformClick()
            End If
        End If
    End Sub

    Private Sub dgvEOBs_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEOBs.CellDoubleClick
        Try
            Clipboard.SetText(dgvEOBs.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
            clipboardMsg.ForeColor = Color.Red
            clipboardMsg.Text = dgvEOBs.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & ""
        Catch ex As Exception

        End Try


    End Sub

    Private Sub dgvEOBs_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEOBs.CellContentDoubleClick
        Try
            Clipboard.SetText(dgvEOBs.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
            clipboardMsg.ForeColor = Color.Red
            clipboardMsg.Text = dgvEOBs.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & ""
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgvClaims_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvClaims.CellContentDoubleClick
        Try
            Clipboard.SetText(dgvClaims.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
            clipboardMsg.ForeColor = Color.Red
            clipboardMsg.Text = dgvClaims.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & ""
        Catch ex As Exception

        End Try

    End Sub

    Private Sub dgvClaims_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvClaims.CellClick
        Try
            Clipboard.SetText(dgvClaims.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
            clipboardMsg.ForeColor = Color.Red
            clipboardMsg.Text = dgvClaims.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & ""
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgvClaimDetail_KeyUp(sender As Object, e As KeyEventArgs) Handles dgvClaimDetail.KeyUp

    End Sub

    Private Sub dgvClaimDetail_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvClaimDetail.CellEndEdit
        If e.ColumnIndex = 7 Then

            dgvClaimDetail.Rows(e.RowIndex).Cells(8).Value =
               Val(dgvClaimDetail.Rows(e.RowIndex).Cells(4).Value) - (Val(dgvClaimDetail.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) + Val(dgvClaimDetail.Rows(e.RowIndex).Cells(6).Value))
            dgvClaimDetail.Rows(e.RowIndex).Cells(9).Value = dgvClaimDetail.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
        ElseIf e.ColumnIndex = 8 Then
            dgvClaimDetail.Rows(e.RowIndex).Cells(7).Value =
               Val(dgvClaimDetail.Rows(e.RowIndex).Cells(4).Value) - (Val(dgvClaimDetail.Rows(e.RowIndex).Cells(6).Value) + Val(dgvClaimDetail.Rows(e.RowIndex).Cells(e.ColumnIndex).Value))
            dgvClaimDetail.Rows(e.RowIndex).Cells(9).Value = dgvClaimDetail.Rows(e.RowIndex).Cells(7).Value
        End If

    End Sub

    Private Sub ViewERAbtn_Click(sender As Object, e As EventArgs) Handles ViewERAbtn.Click
        frmHTML_VIEW.Close()
        frmHTML_VIEW.ERA_path_or_content = txtERAFile.Text
        frmHTML_VIEW.Show()
    End Sub

    Private Sub proceesedbtn_Click(sender As Object, e As EventArgs) Handles proceesedbtn.Click
        frmERA_Processed.Close()
        frmERA_Processed.Search.PerformClick()
        frmERA_Processed.Show()
        frmERA_Processed.MdiParent = frmDashboard
    End Sub
End Class