Imports System.Globalization
Imports DataTable = System.Data.DataTable

Public Class frmERA_Processed
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



    Private Sub PopulateClaims(ByVal DocNo As String, ByVal Msg As String)
        'Try
        dgvClaims.Rows.Clear()
        dgvDetail.Rows.Clear()
        Dim i As Integer
        Dim InvalidClaims As String = ""
        Dim ListedClaims As String = ""
        Dim ClaimIDs() As String = GetClaimIDs(DocNo, Msg)

        dgvClaims.ReadOnly = True



        For i = 0 To ClaimIDs.Length - 1
            Dim CLAIM() As String = GetCLAIM(ClaimIDs(i), Msg)
            '0=ID, 1=PCLMID, 2=PatName, 3=Billed, 4=PRV, 5=Auth, _
            '6=Paid, 7=PR, 8=WO, 9=Sus, 10=Bal, 11=Code, 12=date
            Dim ChargeID As String = ValidateChargeID(ClaimIDs(i), CLAIM(2), CLAIM(12))
            If ChargeID <> "" Then
                If Not ClaimProcessed(DocNo, ClaimIDs(i)) AndAlso _
                InStr(ListedClaims, ClaimIDs(i) & ", ") = 0 Then
                    displayClaim(CLAIM)
                    ListedClaims += ClaimIDs(i) & ", "
                    'ReDim CLAIM(12)
                End If
            Else
                InvalidClaims += CLAIM(0) & " - " & _
                CLAIM(2) & " - DOS: " & CLAIM(12) & vbCrLf
            End If
            'End If
            For c As Integer = 0 To CLAIM.Length - 1
                CLAIM(c) = ""
            Next

            My.Application.DoEvents()
        Next
        dgvClaims.ReadOnly = False
        EOBInfo(4) = dgvClaims.RowCount.ToString
 

        If InvalidClaims <> "" Then
            Dim RetVal = MsgBox("Following claim(s) after submitting the billing to " & _
            "the Clearing House, were either deleted (billing reversed) and appear " & _
            "to be never rebilled in Prolis. Please handle such claim(s) through the " & _
            "billing system used to bill these claim(s)." & vbCrLf _
            & InvalidClaims & vbCrLf & "Do you want to print this message ?", _
            MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Prolis")
            If RetVal = vbYes Then
                Printer.Print("Following claim(s) after submitting the billing to " & _
                "the Clearing House, were either deleted (billing reversed) and appear " & _
                "to be never rebilled in Prolis. Please handle such claim(s) through the " & _
                "billing system used to rebill these claim(s)." & vbCrLf & InvalidClaims & vbCrLf)
            End If
        End If
        'Catch Ex As Exception
        '    MsgBox("Error: " & Ex.Message & " occured")
        'End Try
    End Sub
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

    Private Sub dgvEOBs_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEOBs.CellClick
        If e.RowIndex <> -1 Then
            Try
                Clipboard.SetText(dgvEOBs.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                clipboardMsg.ForeColor = Color.Red
                clipboardMsg.Text = dgvEOBs.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & ""
            Catch ex As Exception

            End Try

            Dim sql = "select * from Charges where ID in ( select Charge_ID  from Payment_Detail where Payment_ID = " & dgvEOBs.Rows(e.RowIndex).Cells(0).Value & ")"
            dgvClaims.Rows.Clear()
            Dim Data = CommonData.ExecuteQuery(sql)
            For Each d In Data
                Dim Accession_ID = d("Accession_ID")
                Dim dt = d("LastEditedOn")
                dgvClaims.Rows.Add(Accession_ID)
            Next
            Dim Msg As String = "" ' OpenERAFile(txtERAFile.Text)

            If Msg <> "" Then _
            PopulateClaims(dgvEOBs.Rows(e.RowIndex).Cells(2).Value, Msg)

        End If
    End Sub
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
                            CLAIM(1) = Fields(7)
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
                                CLAIM(12) = DateTime.ParseExact(Fields(2), _
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
    Private Function ValidateChargeID(ByVal ChargeID As String, _
    ByVal PatName As String, ByVal SvcDate As String) As String
        Dim Myid As String = ""
        Dim Names() As String = Split(PatName, ",")
        Dim sSQL As String = ""
        If ChargeID <> "" And PatName = "" And Not IsDate(SvcDate) Then
            sSQL = "Select ID from Charges where ArType = 1 and IsPrimary <> 0 and ID = '" & ChargeID & _
            "' Union Select a.ID from Charges a inner join (Requisitions b inner join Patients c on " & _
            "c.ID = b.Patient_ID) on b.ID = a.Accession_ID where a.ArType = 1 and a.IsPrimary <> 0"
        ElseIf ChargeID <> "" And PatName <> "" And Not IsDate(SvcDate) Then
            sSQL = "Select ID from Charges where ArType = 1 and IsPrimary <> 0 and ID = '" & ChargeID & _
            "' Union Select a.ID from Charges a inner join (Requisitions b inner join Patients c on " & _
            "c.ID = b.Patient_ID) on b.ID = a.Accession_ID where a.ArType = 1 and a.IsPrimary <> 0 " & _
            "and c.LastName like '" & Trim(Names(0)) & "%' and c.FirstName Like '" & Trim(Names(1)) & "%'"
        ElseIf ChargeID <> "" And PatName <> "" And IsDate(SvcDate) Then
            sSQL = "Select ID from Charges where ArType = 1 and IsPrimary <> 0 and ID = '" & ChargeID & _
            "' Union Select a.ID from Charges a inner join (Requisitions b inner join Patients c on " & _
            "c.ID = b.Patient_ID) on b.ID = a.Accession_ID where a.ArType = 1 and a.IsPrimary <> 0 and " & _
            "a.Svc_Date between '" & Format(CDate(SvcDate), SystemConfig.DateFormat) & "' and '" & _
            Format(CDate(SvcDate), SystemConfig.DateFormat) & " 23:59:00' and c.LastName like '" & Replace(Trim(Names(0)), _
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

    Private Sub ERA_Processed_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpPost.CustomFormat = SystemConfig.DateFormat
        If ThisUser.UserName.ToLower.Contains("techteam") Then
            Button2.Enabled = True
        Else
            Button2.Enabled = False
        End If

        dtpPost.Format = DateTimePickerFormat.Custom
        dtpPost.CustomFormat = SystemConfig.DateFormat
        dtpPost.Value = Date.Now

        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = SystemConfig.DateFormat
        DateTimePicker2.Value = Date.Now
        'DateTime.ParseExact(Fields(2), _
        ' "yyyyMMdd", CultureInfo.InvariantCulture).ToString(SystemConfig.DateFormat)
    End Sub

    Private Sub Search_Click(sender As Object, e As EventArgs) Handles Search.Click

        If Accid.Text <> "" Then
            Dim fromd = Convert.ToDateTime(dtpPost.Text).ToString(SystemConfig.DateFormat)
            Dim ToD = Convert.ToDateTime(DateTimePicker2.Text).ToString(SystemConfig.DateFormat)
            Dim sql = "select * from Payments where ID in ( select Payment_ID  from Payment_Detail where Charge_ID in( select ID  from Charges where Accession_ID = " & Accid.Text & "))"
            dgvEOBs.Rows.Clear()
            Dim Data = CommonData.ExecuteQuery(sql)
            For Each d In Data
                Dim ID = d("ID")
                Dim DocNo = d("DocNo")
                Dim dt = d("LastEditedOn")
                Dim Artyp = d("ArType").ToString().ToARTypeString()
                dgvEOBs.Rows.Add(ID, DocNo, dt, Artyp)
            Next
        ElseIf Doc.Text <> "" Then
            Dim Sql = "select * from Payments where  DocNo='" & Doc.Text & "'"
            dgvEOBs.Rows.Clear()
            Dim Data = CommonData.ExecuteQuery(Sql)
            For Each d In Data
                Dim ID = d("ID")
                Dim DocNo = d("DocNo")
                Dim dt = d("LastEditedOn")
                Dim Artyp = d("ArType").ToString().ToARTypeString()
                dgvEOBs.Rows.Add(ID, DocNo, dt, Artyp)
            Next
        Else
            Dim fromd = Convert.ToDateTime(dtpPost.Text).ToString(SystemConfig.DateFormat)
            Dim ToD = Convert.ToDateTime(DateTimePicker2.Text).ToString(SystemConfig.DateFormat)
            Dim sql = "Select * from Payments where CONVERT(DATE,LastEditedOn)  between '" & fromd & "' and '" & ToD & "' order by LastEditedOn desc"
            dgvEOBs.Rows.Clear()
            Dim Data = CommonData.ExecuteQuery(sql)
            For Each d In Data
                Dim ID = d("ID")
                Dim DocNo = d("DocNo")
                Dim dt = d("LastEditedOn")
                Dim Artyp = d("ArType").ToString().ToARTypeString()
                dgvEOBs.Rows.Add(ID, DocNo, dt, Artyp)
            Next
        End If

    End Sub

    Private Sub dgvClaims_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvClaims.CellClick
        If e.RowIndex <> -1 Then
            Try
                Clipboard.SetText(dgvClaims.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                clipboardMsg.ForeColor = Color.Red
                clipboardMsg.Text = dgvClaims.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & ""
            Catch ex As Exception

            End Try
            '
            Dim AccCnt As Integer = 0
            Dim Pats As Integer = 0
            Dim CNT As Integer = 0
            Dim Age As Integer = 0
            Dim APD As Integer = 0
            Dim A0_30 As Integer = 0
            Dim A31_60 As Integer = 0
            Dim A61_90 As Integer = 0
            Dim A91Up As Integer = 0

            Dim TotalBilled As Double = 0
            Dim TotalPaid As Double = 0
            Dim TotalWO As Double = 0
            Dim Billee As String = ""
            Dim Provider As String = ""
            Dim SvcDate As String = ""
            Dim Payments(3, 0) As String '0=Ck, 1 = Date, 2 = Amt, 3 = WO
            Dim InvBal As Double = 0
            '
            Dim tbl As New DataTable
            Dim sql = "Select a.Accession_ID as Accession, a.ID as Invoice, IsNull(convert(nvarchar, a.Svc_Date, 101), '') as [Svc Date], (Case when a.ArType = 0 then d.LastName_BSN + ', ' + d.FirstName When a.ArType = 1 Then e.PayerName Else c.LastName + ', ' + c.FirstName End) as [Bill To], c.LastName + ', ' + c.FirstName as Patient, IsNull(a.GrossAmount, 0) as [Bill Amt], IsNull((Select top 1 f.DocNo from Payments f inner join (Payment_Detail g inner join Charge_Detail h on h.Charge_ID = g.Charge_ID) on g.Payment_ID = f.ID where g.Charge_ID = a.ID), '') as [Doc No], IsNull(Convert(nvarchar, (Select top 1 f.PaymentDate from Payments f inner join (Payment_Detail g inner join Charge_Detail h on h.Charge_ID = g.Charge_ID) on g.Payment_ID = f.ID where g.Charge_ID = a.ID), 101), '') as [Trn Date], IsNull((Select Sum(AppliedAmount) from Payment_Detail where Charge_ID = a.ID), 0) as Payment, IsNull((Select Sum(WrittenOff) from Payment_Detail where Charge_ID = a.ID), 0) as [W.O.], IsNull(a.GrossAmount - IsNull((Select Sum(AppliedAmount) from Payment_Detail where Charge_ID = a.ID), 0) - IsNull((Select Sum(WrittenOff) from Payment_Detail where Charge_ID = a.ID), 0), 0) as Balance, (Case when IsNull((Select top 1 f.DocNo from Payments f inner join (Payment_Detail g inner join Charge_Detail h on h.Charge_ID = g.Charge_ID) on g.Payment_ID = f.ID where g.Charge_ID = a.ID), '') <> '' Then DateDiff(day, a.Bill_Date, (Select top 1 f.PaymentDate from Payments f inner join (Payment_Detail g inner join Charge_Detail h on h.Charge_ID = g.Charge_ID) on g.Payment_ID = f.ID where g.Charge_ID = a.ID)) Else DateDiff(day, a.Bill_Date, GETDATE()) end) as Age from Charges a inner join (Patients c inner join (Providers d inner join (Requisitions b left outer join Payers e on e.ID = b.PrimePayer_ID) on b.OrderingProvider_ID = d.ID) on c.ID = b.Patient_ID) on b.ID = a.Accession_ID and b.ID = " & dgvClaims.Rows(e.RowIndex).Cells(0).Value & ""
            Dim Data = CommonData.ExecuteQuery(sql)
            dgvDetail.Rows.Clear()
            For Each drsrch In Data

                If drsrch("Age") >= 0 And drsrch("Age") <= 30 Then
                    A0_30 += drsrch("Bill Amt") - drsrch("Payment") - drsrch("W.O.")
                ElseIf drsrch("Age") >= 31 And drsrch("Age") <= 60 Then
                    A31_60 += drsrch("Bill Amt") - drsrch("Payment") - drsrch("W.O.")
                ElseIf drsrch("Age") >= 61 And drsrch("Age") <= 90 Then
                    A61_90 += drsrch("Bill Amt") - drsrch("Payment") - drsrch("W.O.")
                Else
                    A91Up += drsrch("Bill Amt") - drsrch("Payment") - drsrch("W.O.")
                End If
                TotalBilled += drsrch("Bill Amt")
                TotalPaid += drsrch("Payment")
                TotalWO += drsrch("W.O.")
                '
                AccCnt += 1
                dgvDetail.Rows.Add(drsrch("Accession"), drsrch("Invoice"), drsrch("Svc Date"),
                drsrch("Bill To"), drsrch("Patient"), drsrch("Bill Amt"), drsrch("Doc No"),
                drsrch("Trn Date"), drsrch("Payment"), drsrch("W.O."), drsrch("Balance"), drsrch("Age"))

            Next
        End If
    End Sub

    Private Sub dgvDetail_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDetail.CellClick
        If e.RowIndex <> -1 Then
            Try
                Clipboard.SetText(dgvDetail.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                clipboardMsg.ForeColor = Color.Red
                clipboardMsg.Text = dgvDetail.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & ""
            Catch ex As Exception

            End Try
        End If
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Accid.Text = "" Then
            Return

        End If
        Dim sqlQuery As String = "DECLARE @accid AS bigint = " & Accid.Text &
    " DECLARE @targetTGP TABLE (TGP_ID int)" &
    " INSERT INTO @targetTGP (TGP_ID)" &
    " SELECT DISTINCT TGP_ID" &
    " FROM Req_TGP" &
    " WHERE Accession_ID = @accid" &
    " DECLARE @CurrentTGP int" &
    " DECLARE cur CURSOR FOR" &
    " SELECT TGP_ID FROM @targetTGP" &
    " OPEN cur" &
    " FETCH NEXT FROM cur INTO @CurrentTGP" &
    " WHILE @@FETCH_STATUS = 0" &
    " BEGIN" &
    " DECLARE @targetChargeID AS int = (" &
    " SELECT DISTINCT Charge_ID" &
    " FROM Payment_Detail" &
    " WHERE Charge_ID IN (" &
    " SELECT ID" &
    " FROM Charges" &
    " WHERE Accession_ID = @accid" &
    " )" &
    " AND Charge_ID NOT IN (" &
    " SELECT DISTINCT Charge_ID" &
    " FROM Charge_Detail" &
    " WHERE Charge_ID IN (" &
    " SELECT ID" &
    " FROM Charges" &
    " WHERE Accession_ID = @accid" &
    " )" &
    " AND POS_Code = 81" &
    " ))" &
    " DECLARE @targetChargeID81 AS int = (" &
    " SELECT DISTINCT Charge_ID" &
    " FROM Charge_Detail" &
    " WHERE Charge_ID IN (" &
    " SELECT ID" &
    " FROM Charges" &
    " WHERE Accession_ID = @accid" &
    " )" &
    " AND POS_Code = 81" &
    " )" &
    " UPDATE Payment_Detail" &
    " SET WrittenOff = ChargeAmount - (" &
    " (SELECT LinePrice" &
    " FROM Charge_Detail" &
    " WHERE Charge_ID = @targetChargeID81" &
    " AND tgp_id = @CurrentTGP) + AppliedAmount" &
    " )" &
    " WHERE TGP_ID = @CurrentTGP" &
    " AND Charge_ID = @targetChargeID" &
    " UPDATE Payment_Detail" &
    " SET Balance = ChargeAmount - WrittenOff" &
    " WHERE TGP_ID = @CurrentTGP" &
    " AND Charge_ID = @targetChargeID" &
    " FETCH NEXT FROM cur INTO @CurrentTGP" &
    " END" &
    " CLOSE cur" &
    " DEALLOCATE cur"


        ExecuteSqlProcedure(sqlQuery)

        Dim s = dgvClaims.SelectedCells()(0).Value
        Dim AccCnt As Integer = 0
        Dim Pats As Integer = 0
        Dim CNT As Integer = 0
        Dim Age As Integer = 0
        Dim APD As Integer = 0
        Dim A0_30 As Integer = 0
        Dim A31_60 As Integer = 0
        Dim A61_90 As Integer = 0
        Dim A91Up As Integer = 0

        Dim TotalBilled As Double = 0
        Dim TotalPaid As Double = 0
        Dim TotalWO As Double = 0
        Dim Billee As String = ""
        Dim Provider As String = ""
        Dim SvcDate As String = ""
        Dim Payments(3, 0) As String '0=Ck, 1 = Date, 2 = Amt, 3 = WO
        Dim InvBal As Double = 0
        '
        Dim tbl As New DataTable
        Dim sql = "Select a.Accession_ID as Accession, a.ID as Invoice, IsNull(convert(nvarchar, a.Svc_Date, 101), '') as [Svc Date], (Case when a.ArType = 0 then d.LastName_BSN + ', ' + d.FirstName When a.ArType = 1 Then e.PayerName Else c.LastName + ', ' + c.FirstName End) as [Bill To], c.LastName + ', ' + c.FirstName as Patient, IsNull(a.GrossAmount, 0) as [Bill Amt], IsNull((Select top 1 f.DocNo from Payments f inner join (Payment_Detail g inner join Charge_Detail h on h.Charge_ID = g.Charge_ID) on g.Payment_ID = f.ID where g.Charge_ID = a.ID), '') as [Doc No], IsNull(Convert(nvarchar, (Select top 1 f.PaymentDate from Payments f inner join (Payment_Detail g inner join Charge_Detail h on h.Charge_ID = g.Charge_ID) on g.Payment_ID = f.ID where g.Charge_ID = a.ID), 101), '') as [Trn Date], IsNull((Select Sum(AppliedAmount) from Payment_Detail where Charge_ID = a.ID), 0) as Payment, IsNull((Select Sum(WrittenOff) from Payment_Detail where Charge_ID = a.ID), 0) as [W.O.], IsNull(a.GrossAmount - IsNull((Select Sum(AppliedAmount) from Payment_Detail where Charge_ID = a.ID), 0) - IsNull((Select Sum(WrittenOff) from Payment_Detail where Charge_ID = a.ID), 0), 0) as Balance, (Case when IsNull((Select top 1 f.DocNo from Payments f inner join (Payment_Detail g inner join Charge_Detail h on h.Charge_ID = g.Charge_ID) on g.Payment_ID = f.ID where g.Charge_ID = a.ID), '') <> '' Then DateDiff(day, a.Bill_Date, (Select top 1 f.PaymentDate from Payments f inner join (Payment_Detail g inner join Charge_Detail h on h.Charge_ID = g.Charge_ID) on g.Payment_ID = f.ID where g.Charge_ID = a.ID)) Else DateDiff(day, a.Bill_Date, GETDATE()) end) as Age from Charges a inner join (Patients c inner join (Providers d inner join (Requisitions b left outer join Payers e on e.ID = b.PrimePayer_ID) on b.OrderingProvider_ID = d.ID) on c.ID = b.Patient_ID) on b.ID = a.Accession_ID and b.ID = " & s & ""
        Dim Data = CommonData.ExecuteQuery(sql)
        dgvDetail.Rows.Clear()
        For Each drsrch In Data

            If drsrch("Age") >= 0 And drsrch("Age") <= 30 Then
                A0_30 += drsrch("Bill Amt") - drsrch("Payment") - drsrch("W.O.")
            ElseIf drsrch("Age") >= 31 And drsrch("Age") <= 60 Then
                A31_60 += drsrch("Bill Amt") - drsrch("Payment") - drsrch("W.O.")
            ElseIf drsrch("Age") >= 61 And drsrch("Age") <= 90 Then
                A61_90 += drsrch("Bill Amt") - drsrch("Payment") - drsrch("W.O.")
            Else
                A91Up += drsrch("Bill Amt") - drsrch("Payment") - drsrch("W.O.")
            End If
            TotalBilled += drsrch("Bill Amt")
            TotalPaid += drsrch("Payment")
            TotalWO += drsrch("W.O.")
            '
            AccCnt += 1
            dgvDetail.Rows.Add(drsrch("Accession"), drsrch("Invoice"), drsrch("Svc Date"), _
            drsrch("Bill To"), drsrch("Patient"), drsrch("Bill Amt"), drsrch("Doc No"), _
            drsrch("Trn Date"), drsrch("Payment"), drsrch("W.O."), drsrch("Balance"), drsrch("Age"))

        Next
    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click
        Accid.Text = Clipboard.GetText()
    End Sub

    Private Sub dgvDetail_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDetail.CellDoubleClick

        Dim c = e.ColumnIndex
        If e.RowIndex = -1 Then
            Return
        End If
        dgvDetail.Rows(e.RowIndex).Cells(e.ColumnIndex).ToString()
        If e.RowIndex <> -1 Then
            If e.ColumnIndex = 0 Then
                Try
                    Clipboard.SetText(dgvDetail.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                    clipboardMsg.ForeColor = Color.Red
                    clipboardMsg.Text = dgvDetail.Rows(e.RowIndex).Cells(e.ColumnIndex).Value

                    frmBillingEdit.Close()

                    frmBillingEdit.Show()
                    ' frmBillingEdit.MdiParent = ProlisQC
                    frmBillingEdit.dgvDiscrete.Rows.Clear()
                    frmBillingEdit.dgvDiscrete.Rows.Add(clipboardMsg.Text)
                    frmBillingEdit.cmbABU.SelectedIndex = 0
                    frmBillingEdit.btnTarget.PerformClick()
                    frmBillingEdit.btnLoad.PerformClick()

                Catch ex As Exception

                End Try
            ElseIf e.ColumnIndex = 6 Then
                Try
                    Clipboard.SetText(dgvDetail.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                    clipboardMsg.ForeColor = Color.Red
                    clipboardMsg.Text = dgvDetail.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                    frmPayments.Close()
                    Dim value1 As String = dgvDetail.Rows(e.RowIndex).Cells(0).Value
                    Dim value2 As String = dgvDetail.Rows(e.RowIndex).Cells(1).Value

                    frmPayments.txtID.Text = ""

                    frmPayments.Show()
                    frmPayments.chkEditNew.PerformClick()
                    frmPayments.txtDoc.Text = dgvDetail.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                    frmPayments.MdiParent = frmDashboard
                    frmPayments.txtDoc.Focus()
                    Dim shownHandler As EventHandler = Sub(s, ea)
                                                           OnFrmPaymentsShown(s, ea, value1, value2)
                                                           SelectInvoiceRow(value1, value2)
                                                           RemoveHandler frmPayments.Shown, shownHandler
                                                       End Sub

                    frmPayments.SelectNextControl(frmPayments.txtDoc, True, True, True, True)
                    SelectInvoiceRow(value1, value2)
                Catch ex As Exception

                End Try
            End If

        End If
    End Sub
    Private Sub OnFrmPaymentsShown(sender As Object, e As EventArgs, value1 As String, value2 As String)

        ' Simulate Tab key press to move focus to the next control
        frmPayments.SelectNextControl(frmPayments.txtDoc, True, True, True, True)
        SelectInvoiceRow(value1, value2)

    End Sub
    Private Sub SelectInvoiceRow(value1 As String, value2 As String)
        For Each row As DataGridViewRow In frmPayments.dgvInvoices.Rows
            If row.Cells(0).Value IsNot Nothing AndAlso row.Cells(1).Value IsNot Nothing Then
                If row.Cells(0).Value.ToString() = value2 AndAlso row.Cells(1).Value.ToString() = value1 Then
                    ' Select the row
                    row.Selected = True

                    ' Optionally, scroll to the row to make it visible
                    frmPayments.dgvInvoices.FirstDisplayedScrollingRowIndex = row.Index

                    ' Optionally, trigger the cell click event
                    frmPayments.dgvInvoices.CurrentCell = row.Cells(0)
                    frmPayments.dgvInvoices.BeginEdit(True)

                    Exit For
                End If
            End If
        Next
    End Sub
End Class