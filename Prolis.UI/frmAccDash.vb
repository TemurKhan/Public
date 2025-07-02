Imports System.Windows.Forms
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data
Imports System.Windows.Forms.DataVisualization.Charting
Imports Microsoft.Data.SqlClient


Public Class frmAccDash
    Private origWidth As Integer
    Private origHeight As Integer

    Private Sub frmAccDash_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MaximumSize = MaxSize
        If SystemConfig.AuditTrail Then
            cmbStatus.Enabled = True
        Else
            cmbStatus.Enabled = False
        End If
        cmbStatus.SelectedIndex = 0
        cmbDateType.SelectedIndex = 0
        cmbCriteria.SelectedIndex = -1
        lblFrom.Text += " (" & SystemConfig.DateFormat & ")"
        lblTo.Text += " (" & SystemConfig.DateFormat & ")"
        dtpFrom.Format = DateTimePickerFormat.Custom
        dtpFrom.CustomFormat = SystemConfig.DateFormat
        dtpFrom.Value = Date.Today
        dtpTo.Format = DateTimePickerFormat.Custom
        dtpTo.CustomFormat = SystemConfig.DateFormat
        dtpTo.Value = CDate(Format(Date.Today, SystemConfig.DateFormat) & " 23:59:00")
        lblStatus.Text = ""
        My.Application.DoEvents()
        Dim stopWatch As New Stopwatch()
        stopWatch.Start()
        '
        DisplayResults(cmbDateType.SelectedIndex)
        '
        stopWatch.Stop()
        lblStatus.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
        stopWatch.Elapsed.Hours, stopWatch.Elapsed.Minutes,
        stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds / 10)
        My.Application.DoEvents()
        '
        If dgvAccessions.RowCount > 0 Then
            btnPrintSum.Enabled = True
            btnPrintDet.Enabled = True
        End If
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub DisplayResults(ByVal DateType As Integer)   '0=Acc, 1=Coll, 2=Rec
        Dim Criteria As String = GetCriteria(DateType)
        txtProviders.Text = GetProviderCount(Criteria)
        txtPatients.Text = GetPatientCount(Criteria)
        txtGratis.Text = GetGratisCount(Criteria)
        txtClientBilled.Text = GetClientBilledCount(Criteria)
        txtPatBilled.Text = GetPatBilledCount(Criteria)
        txt3PBilled.Text = Get3PBilledCount(Criteria)
        txtTotal.Text = GetAccessionCount(Criteria)
        '
        'TODO: Add Chart control to the form and set its properties
        'Chart1.Series(0).Points.Clear()
        'Chart1.Series(1).Points.Clear()
        'Chart1.Series("Count").Points.AddXY("Gratis", Val(txtGratis.Text))
        'Chart1.Series("Percent").Points.AddXY("Gratis", Val(txtGratis.Text) / Val(txtTotal.Text) * 100)
        'Chart1.Series("Count").Points.AddXY("Client", Val(txtClientBilled.Text))
        'Chart1.Series("Percent").Points.AddXY("Client", Val(txtClientBilled.Text) / Val(txtTotal.Text) * 100)
        'Chart1.Series("Count").Points.AddXY("Insurance", Val(txt3PBilled.Text))
        'Chart1.Series("Percent").Points.AddXY("Insurance", Val(txt3PBilled.Text) / Val(txtTotal.Text) * 100)
        'Chart1.Series("Count").Points.AddXY("Patient", Val(txtPatBilled.Text))
        'Chart1.Series("Percent").Points.AddXY("Patient", Val(txtPatBilled.Text) / Val(txtTotal.Text) * 100)
        'Chart1.Series("Count").Points.AddXY("Total", Val(txtTotal.Text))
        'Chart1.Series("Percent").Points.AddXY("Total", Val(txtTotal.Text) / Val(txtTotal.Text) * 100)
        'Chart1.Refresh()
        '
        'Till here
        dgvAccessions.Rows.Clear()
        Dim PatName As String = ""
        btnPrintSum.Enabled = False : btnPrintDet.Enabled = False
        Dim sSQL As String = "Select a.ID as [Acc ID], a.AccessionDate as Dated, (Select CONCAT ( 'ID:' , b.ID) + ', ' + b.LastName + " &
        "', ' + b.FirstName + ', Gender: ' + b.Sex + ', DOB: ' + Convert(nvarchar(10), b.DOB, 101) from Patients b where " &
        "b.ID = a.Patient_ID) as Patient, ((Select c.LastName_BSN + ' [' + convert(nvarchar, c.ID) + ']' from Providers c " &
        "where c.ID = a.OrderingProvider_ID and c.IsIndividual = 0) Union (Select c.LastName_BSN + ', ' + FirstName + ' [' " &
        "+ convert(nvarchar, c.ID) + ']' from Providers c where c.ID = a.OrderingProvider_ID and c.IsIndividual <> 0)) as " &
        "Provider, a.BillingType_ID from Requisitions a where a.Received <> 0 and " & Criteria
        If cmbStatus.SelectedIndex = 2 Then
            sSQL = "Select a.ID as [Acc ID], a.AccessionDate as Dated, (Select CONCAT ( 'ID:' , b.ID) + ', ' + b.LastName + " &
      "', ' + b.FirstName + ', Gender: ' + b.Sex + ', DOB: ' + Convert(nvarchar(10), b.DOB, 101) from Patients b where " &
      "b.ID = a.Patient_ID) as Patient, ((Select c.LastName_BSN + ' [' + convert(nvarchar, c.ID) + ']' from Providers c " &
      "where c.ID = a.OrderingProvider_ID and c.IsIndividual = 0) Union (Select c.LastName_BSN + ', ' + FirstName + ' [' " &
      "+ convert(nvarchar, c.ID) + ']' from Providers c where c.ID = a.OrderingProvider_ID and c.IsIndividual <> 0)) as " &
      "Provider, a.BillingType_ID from Requisitions a where a.Received = 0 and " & Criteria
        End If
        If connString <> "" Then
            Dim cndr As New SqlConnection(connString)
            cndr.Open()
            Dim selcmd As New SqlCommand(sSQL, cndr)
            selcmd.CommandType = CommandType.Text
            Dim selDR As SqlDataReader = selcmd.ExecuteReader
            If selDR.HasRows Then
                While selDR.Read
                    dgvAccessions.Rows.Add(selDR("Acc ID"), Format(selDR("Dated"), SystemConfig.DateFormat),
                    selDR("Patient"), selDR("Provider"), selDR("BillingType_ID"))
                End While
            End If
            cndr.Close()
            cndr = Nothing
        End If
        If dgvAccessions.RowCount > 0 Then
            btnPrintSum.Enabled = True
            btnPrintDet.Enabled = True
        End If
    End Sub

    Private Function GetProviderName(ByVal ProviderID As Long) As String
        Dim PrName As String = ""
        Dim cngpn As New SqlConnection(connString)
        cngpn.Open()
        Dim cmdgpn As New SqlCommand("Select * " &
        "from Providers where ID = " & ProviderID, cngpn)
        cmdgpn.CommandType = CommandType.Text
        Dim drgpn As SqlDataReader = cmdgpn.ExecuteReader
        If drgpn.HasRows Then
            While drgpn.Read
                If drgpn("IsIndividual") IsNot DBNull.Value AndAlso drgpn("IsIndividual") = 0 Then
                    PrName = drgpn("LastName_BSN")
                Else
                    If drgpn("Degree") Is DBNull.Value Then
                        PrName = drgpn("LastName_BSN") & ", " & drgpn("FirstName")
                    Else
                        PrName = drgpn("LastName_BSN") & ", " &
                        drgpn("FirstName") & " " & drgpn("Degree")
                    End If
                End If
            End While
        End If
        cngpn.Close()
        cngpn = Nothing
        Return PrName
    End Function

    Private Function GetPatientName(ByVal PatID As Long) As String
        Dim PatName As String = ""
        Dim cnpat As New SqlConnection(connString)
        cnpat.Open()
        Dim cmdpat As New SqlCommand("Select " &
        "* from Patients where ID = " & PatID, cnpat)
        cmdpat.CommandType = CommandType.Text
        Dim drpat As SqlDataReader = cmdpat.ExecuteReader
        If drpat.HasRows Then
            While drpat.Read
                PatName = drpat("LastName") & ", " & drpat("FirstName") &
                " , Gender: " & drpat("Sex") & " , DOB: " &
                Format(drpat("DOB"), SystemConfig.DateFormat)
            End While
        End If
        cnpat.Close()
        cnpat = Nothing
        Return PatName
    End Function

    Private Function GetAccessionCount(ByVal Criteria As String) As Integer
        Dim AccCount As Integer = 0

        Dim sSQL As String = "Select Count(ID) as AccCount from " &
        "Requisitions where " & IIf(cmbStatus.SelectedIndex = 2, "Received = 0 and", "Received <> 0 and") & " " & Criteria
        Dim cngac As New SqlConnection(connString)
        cngac.Open()
        Dim cmdgac As New SqlCommand(sSQL, cngac)
        cmdgac.CommandType = CommandType.Text
        Dim drgac As SqlDataReader = cmdgac.ExecuteReader
        If drgac.HasRows Then
            While drgac.Read
                AccCount = drgac("AccCount")
            End While
        End If
        cngac.Close()
        cngac = Nothing
        Return AccCount
    End Function

    Private Function Get3PBilledCount(ByVal Criteria As String) As Integer
        Dim IBCount As Integer = 0
        Dim sSQL As String = "Select Count(ID) as IBCount from " &
        "Requisitions where  " & IIf(cmbStatus.SelectedIndex = 2, "Received = 0 and", "Received <> 0 and") & " BillingType_ID = 1 and " & Criteria
        Dim cn3bc As New SqlConnection(connString)
        cn3bc.Open()
        Dim cmd3bc As New SqlCommand(sSQL, cn3bc)
        cmd3bc.CommandType = CommandType.Text
        Dim dr3bc As SqlDataReader = cmd3bc.ExecuteReader
        If dr3bc.HasRows Then
            While dr3bc.Read
                IBCount = dr3bc("IBCount")
            End While
        End If
        cn3bc.Close()
        cn3bc = Nothing
        Return IBCount
    End Function

    Private Function GetPatBilledCount(ByVal Criteria As String) As Integer
        Dim PBCount As Integer = 0
        Dim sSQL As String = "Select Count(ID) as PBCount from " &
        "Requisitions where " & IIf(cmbStatus.SelectedIndex = 2, "Received = 0 and", "Received <> 0 and") & "  BillingType_ID = 2 and " & Criteria
        Dim cnpbc As New SqlConnection(connString)
        cnpbc.Open()
        Dim cmdpbc As New SqlCommand(sSQL, cnpbc)
        cmdpbc.CommandType = CommandType.Text
        Dim drpbc As SqlDataReader = cmdpbc.ExecuteReader
        If drpbc.HasRows Then
            While drpbc.Read
                PBCount = drpbc("PBCount")
            End While
        End If
        cnpbc.Close()
        cnpbc = Nothing
        Return PBCount
    End Function

    Private Function GetClientBilledCount(ByVal Criteria As String) As Integer
        Dim CBCount As Integer = 0
        Dim sSQL As String = "Select Count(ID) as CBCount from " &
        "Requisitions where  " & IIf(cmbStatus.SelectedIndex = 2, "Received = 0 and", "Received <> 0 and") & " BillingType_ID = 0 and " & Criteria
        Dim cngcb As New SqlConnection(connString)
        cngcb.Open()
        Dim cmdgcb As New SqlCommand(sSQL, cngcb)
        cmdgcb.CommandType = CommandType.Text
        Dim drgcb As SqlDataReader = cmdgcb.ExecuteReader
        If drgcb.HasRows Then
            While drgcb.Read
                CBCount = drgcb("CBCount")
            End While
        End If
        cngcb.Close()
        cngcb = Nothing
        Return CBCount
    End Function

    Private Function GetGratisCount(ByVal Criteria As String) As Integer
        Dim GCount As Integer = 0
        Dim sSQL As String = "Select Count(ID) as GCount from Requisitions " &
        "where " & IIf(cmbStatus.SelectedIndex = 2, "Received = 0 and", "Received <> 0 and") & " IsGratis <> 0 and " & Criteria
        Dim cnggc As New SqlConnection(connString)
        cnggc.Open()
        Dim cmdggc As New SqlCommand(sSQL, cnggc)
        cmdggc.CommandType = CommandType.Text
        Dim drggc As SqlDataReader = cmdggc.ExecuteReader
        If drggc.HasRows Then
            While drggc.Read
                GCount = drggc("GCount")
            End While
        End If
        cnggc.Close()
        cnggc = Nothing
        Return GCount
    End Function

    Private Function GetPatientCount(ByVal Criteria As String) As Integer
        Dim PatCount As Integer = 0
        Dim sSQL As String = "Select Count(ID) as PatCount from Patients where ID in (Select " &
        "distinct Patient_ID from Requisitions where  " & IIf(cmbStatus.SelectedIndex = 2, "Received = 0 and", "Received <> 0 and") & " " & Criteria & ")"
        Dim cngpc As New SqlConnection(connString)
        cngpc.Open()
        Dim cmdgpc As New SqlCommand(sSQL, cngpc)
        cmdgpc.CommandType = CommandType.Text
        Dim drgpc As SqlDataReader = cmdgpc.ExecuteReader
        If drgpc.HasRows Then
            While drgpc.Read
                PatCount = drgpc("PatCount")
            End While
        End If
        cngpc.Close()
        cngpc = Nothing
        Return PatCount
    End Function

    Private Function GetProviderCount(ByVal Criteria As String) As Integer
        Dim PrCount As Integer = 0
        Dim sSQL As String = "Select Count(ID) as PrCount from Providers where ID in (Select distinct " &
        "OrderingProvider_ID from Requisitions where  " & IIf(cmbStatus.SelectedIndex = 2, "Received = 0 and", "Received <> 0 and") & "  " & Criteria & ")"
        Dim cngpc As New SqlConnection(connString)
        cngpc.Open()
        Dim cmdgpc As New SqlCommand(sSQL, cngpc)
        cmdgpc.CommandTimeout = 120
        cmdgpc.CommandType = CommandType.Text
        Dim drgpc As SqlDataReader = cmdgpc.ExecuteReader
        If drgpc.HasRows Then
            While drgpc.Read
                PrCount = drgpc("PrCount")
            End While
        End If
        cngpc.Close()
        cngpc = Nothing
        Return PrCount
    End Function

    Private Function GetCriteria(ByVal DateType As Integer) As String
        Dim Criteria As String = ""
        If DateType = 0 Then    'Acc

            Criteria = "AccessionDate between '" & Format(dtpFrom.Value, SystemConfig.DateFormat) &
            " 00:00' and '" & Format(dtpTo.Value, SystemConfig.DateFormat) & " 23:59'"
        ElseIf DateType = 1 Then    'Coll
            Criteria = "ID in (Select distinct Accession_ID from Specimens where SourceDate " &
            "between '" & Format(dtpFrom.Value, SystemConfig.DateFormat) & " 00:00' and '" _
            & Format(dtpTo.Value, SystemConfig.DateFormat) & " 23:59')"
        Else    '2 Receive
            Criteria = "ReceivedTime between '" & Format(dtpFrom.Value, SystemConfig.DateFormat) &
            " 00:00' and '" & Format(dtpTo.Value, SystemConfig.DateFormat) & " 23:59'"
        End If
        If cmbCriteria.SelectedIndex = 0 Then   'Accession ID
            If txtTerm.Text.Contains("-") Then
                Dim acc = txtTerm.Text.Split("-")(0)
                txtTerm.Text = acc
            End If
            If Trim(txtTerm.Text) <> "" AndAlso IsNumeric(txtTerm.Text) _
            Then Criteria = "  ID = " & Val(txtTerm.Text)
        ElseIf cmbCriteria.SelectedIndex = 1 Then   'Provider ID
            If Trim(txtTerm.Text) <> "" AndAlso IsNumeric(txtTerm.Text) Then _
            Criteria += " and OrderingProvider_ID = " & Val(txtTerm.Text)
        ElseIf cmbCriteria.SelectedIndex = 2 Then   'Provider Name
            If Trim(txtTerm.Text) <> "" Then
                Dim Data() As String
                If InStr(txtTerm.Text, ",") > 0 Then
                    Data = Split(txtTerm.Text, ",")
                    If Trim(Data(0)) <> "" And Trim(Data(1)) <> "" Then 'Last and First Names
                        Criteria += " and OrderingProvider_ID in (Select ID from Providers where " &
                        "LastName_BSN like '" & Trim(Data(0)) & "%' and FirstName like '" &
                        Trim(Data(1)) & "%')"
                    ElseIf Trim(Data(0)) <> "" And Trim(Data(1)) = "" Then 'Last Name
                        Criteria += " and OrderingProvider_ID in (Select ID from Providers where " &
                        "LastName_BSN like '" & Trim(Data(0)) & "%')"
                    End If
                Else
                    Criteria += " and OrderingProvider_ID in (Select ID from Providers where " &
                    "LastName_BSN like '" & Trim(txtTerm.Text) & "%')"
                End If
            End If
        ElseIf cmbCriteria.SelectedIndex = 3 Then   'Patient ID
            If Trim(txtTerm.Text) <> "" AndAlso IsNumeric(txtTerm.Text) Then _
            Criteria += " and Patient_ID = " & Val(txtTerm.Text)
        ElseIf cmbCriteria.SelectedIndex = 4 Then   'Patient Name
            If Trim(txtTerm.Text) <> "" Then
                Dim Data() As String
                If InStr(txtTerm.Text, ",") > 0 Then
                    Data = Split(txtTerm.Text, ",")
                    If Trim(Data(0)) <> "" And Trim(Data(1)) <> "" Then 'Last and First Names
                        Criteria += " and Patient_ID in (Select ID from Patients where " &
                        "LastName like '" & Trim(Data(0)) & "%' and FirstName like '" &
                        Trim(Data(1)) & "%')"
                    ElseIf Trim(Data(0)) <> "" And Trim(Data(1)) = "" Then 'Last Name
                        Criteria += " and Patient_ID in (Select ID from Patients where " &
                        "LastName like '" & Trim(Data(0)) & "%')"
                    End If
                Else
                    Criteria += " and Patient_ID in (Select ID from Patients where " &
                    "LastName like '" & Trim(txtTerm.Text) & "%')"
                End If
            End If
        ElseIf cmbCriteria.SelectedIndex = 5 Then   'Client Billed
            Criteria += " and IsGratis = 0 and BillingType_ID = 0"
        ElseIf cmbCriteria.SelectedIndex = 6 Then   'Patient Billed
            Criteria += " and IsGratis = 0 and BillingType_ID = 2"
        ElseIf cmbCriteria.SelectedIndex = 7 Then   'Insurance billed
            Criteria += " and IsGratis = 0 and BillingType_ID = 1"
        ElseIf cmbCriteria.SelectedIndex = 8 Then   'Payer ID
            If Trim(txtTerm.Text) <> "" AndAlso IsNumeric(txtTerm.Text) Then _
            Criteria += " and IsGratis = 0 and BillingType_ID = 1 and ID in " &
            "(Select Accession_ID from Req_Coverage where Payer_ID = " &
            Trim(txtTerm.Text) & ")"
        ElseIf cmbCriteria.SelectedIndex = 9 Then   'Payer Name
            If Trim(txtTerm.Text) <> "" Then _
            Criteria += " and IsGratis = 0 and BillingType_ID = 1 and ID in " &
            "(Select a.Accession_ID from Req_Coverage a inner join Payers b " &
            "on a.Payer_ID = b.ID where b.PayerName Like '%" &
            Trim(txtTerm.Text) & "%')"
        ElseIf cmbCriteria.SelectedIndex = 10 Then    'Gratis
            Criteria += " and IsGratis <> 0"
        ElseIf cmbCriteria.SelectedIndex = 11 Then    'RequisitionNUmber
            Criteria = IIf(String.IsNullOrEmpty(txtTerm.Text), "", "  RequisitionNo ='" & txtTerm.Text & "'")
        End If
        Return Criteria


    End Function

    Private Sub txtTerm_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTerm.KeyPress
        Select Case cmbCriteria.SelectedIndex
            Case 0, 1, 3
                Numerals(sender, e)
        End Select
    End Sub

    Private Sub cmbCriteria_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCriteria.SelectedIndexChanged
        Select Case cmbCriteria.SelectedIndex
            Case 5, 6, 7, 10
                txtTerm.Text = ""
                txtTerm.ReadOnly = True
            Case Else
                txtTerm.ReadOnly = False
        End Select
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        If cmbStatus.SelectedIndex = 0 Or cmbStatus.SelectedIndex = 2 Then 'Live
            lblStatus.Text = ""
            My.Application.DoEvents()
            Dim stopWatch As New Stopwatch()
            stopWatch.Start()
            '
            DisplayResults(cmbDateType.SelectedIndex)
            '
            stopWatch.Stop()
            lblStatus.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            stopWatch.Elapsed.Hours, stopWatch.Elapsed.Minutes,
            stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds / 10)
            My.Application.DoEvents()
        ElseIf cmbStatus.SelectedIndex = 2 Then 'not received
            lblStatus.Text = ""
            My.Application.DoEvents()
            Dim stopWatch As New Stopwatch()
            stopWatch.Start()
            '
            DisplayResults(cmbDateType.SelectedIndex)
            '
            stopWatch.Stop()
            lblStatus.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            stopWatch.Elapsed.Hours, stopWatch.Elapsed.Minutes,
            stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds / 10)
            My.Application.DoEvents()
        Else
            If ThisUser.Supervisor Or ThisUser.Owner Then
                DisplayDeletedAccs()
            Else
                MsgBox(ThisUser.Name & ", you dont have sufficient permissions " &
                "to access this information", MsgBoxStyle.Exclamation, "Prolis")
            End If
        End If
    End Sub

    Private Sub DisplayDeletedAccs()
        dgvAccessions.Rows.Clear()
        txtTotal.Text = "0"
        Dim Dated As String = ""
        Dim Patient As String = ""
        Dim Client As String = ""
        Dim cndac As New SqlConnection(connString)
        cndac.Open()
        Dim cmddac As New SqlCommand("Select * from User_Event where " &
        "Event_ID = 4 and Event_Time between '" & Format(dtpFrom.Value, SystemConfig.DateFormat) &
        "' and '" & Format(dtpTo.Value, SystemConfig.DateFormat) & " 23:59:00' and not " &
        "Object_ID in(Select ID from Requisitions)", cndac)
        cmddac.CommandType = CommandType.Text
        Dim drdac As SqlDataReader = cmddac.ExecuteReader
        If drdac.HasRows Then
            While drdac.Read
                Dated = GetAccDateOfSF(drdac("StatusFrom"))
                Patient = GetPatientOfSF(drdac("StatusFrom"))
                Client = GetClientOfSF(drdac("StatusFrom"))
                dgvAccessions.Rows.Add(drdac("Object_ID"), Dated, Patient, Client)
            End While
        End If
        cndac.Close()
        cndac = Nothing
        txtTotal.Text = dgvAccessions.RowCount
    End Sub

    Private Function GetClientOfSF(ByVal Msg As String) As String
        Dim Client As String = ""
        Dim Fields() As String = Split(Msg, "|")
        Dim Comps() As String = Split(Fields(3), "=")
        Client = GetProviderName((Microsoft.VisualBasic.Mid(Comps(1),
        1, InStr(Comps(1), "^") - 1)))
        Return Client
    End Function

    Private Function GetPatientOfSF(ByVal Msg As String) As String
        Dim Patient As String = ""
        Dim Fields() As String = Split(Msg, "|")
        Dim Comps() As String = Split(Fields(4), "=")
        If Comps.Length > 1 Then
            Dim Data() As String = Split(Comps(1), "^")
            If Data.Length > 0 AndAlso Data(0) <> "" Then Patient = Data(0)
            If Data.Length > 1 AndAlso Data(1) <> "" Then Patient += ", " & Data(1)
            If Data.Length > 2 AndAlso Data(2) <> "" Then Patient += " " & Data(2)
            If Data.Length > 3 AndAlso Data(3) <> "" Then Patient += " " & Data(3)
        End If
        Return Patient
    End Function

    Private Function GetAccDateOfSF(ByVal Msg As String) As String
        Dim Dated As String = ""
        Dim Fields() As String = Split(Msg, "|")
        Dim Comps() As String = Split(Fields(1), "=")
        Dated = Format(CDate(Comps(1)), SystemConfig.DateFormat)
        Return Dated
    End Function

    Private Sub dgvAccessions_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAccessions.CellClick
        If e.RowIndex <> -1 Then
            Try
                Clipboard.SetText(dgvAccessions.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                Label2.Text = dgvAccessions.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & " copied to clipboard"
                Label2.ForeColor = Color.Red
                If IsNumeric(Clipboard.GetText()) Then
                    openInRequi.Show()
                    openInRequi.Enabled = True

                Else
                    openInRequi.Hide()
                End If
            Catch ex As Exception

            End Try

            lblStatus.Text = ""
            My.Application.DoEvents()
            Dim stopWatch As New Stopwatch()
            stopWatch.Start()
            '
            If cmbStatus.SelectedIndex = 0 Or cmbStatus.SelectedIndex = 2 Then
                DisplayAttendingProvider(dgvAccessions.Rows(e.RowIndex).Cells(0).Value)
                DisplayPatient(dgvAccessions.Rows(e.RowIndex).Cells(0).Value)
                DisplayDxs(dgvAccessions.Rows(e.RowIndex).Cells(0).Value)
                DisplaySpecimen(dgvAccessions.Rows(e.RowIndex).Cells(0).Value)
                DisplayOrders(dgvAccessions.Rows(e.RowIndex).Cells(0).Value)
                DisplayBillee(dgvAccessions.Rows(e.RowIndex).Cells(0).Value)
                btnPmtReceipt.Visible = AccessionPaymentExists(dgvAccessions.Rows(e.RowIndex).Cells(0).Value)
            ElseIf cmbStatus.SelectedIndex = 2 Then
                DisplayAttendingProvider(dgvAccessions.Rows(e.RowIndex).Cells(0).Value)
                DisplayPatient(dgvAccessions.Rows(e.RowIndex).Cells(0).Value)
                DisplayDxs(dgvAccessions.Rows(e.RowIndex).Cells(0).Value)
                DisplaySpecimen(dgvAccessions.Rows(e.RowIndex).Cells(0).Value)
                DisplayOrders(dgvAccessions.Rows(e.RowIndex).Cells(0).Value)
                DisplayBillee(dgvAccessions.Rows(e.RowIndex).Cells(0).Value)
                btnPmtReceipt.Visible = AccessionPaymentExists(dgvAccessions.Rows(e.RowIndex).Cells(0).Value)
            Else
                DisplayDeleter(dgvAccessions.Rows(e.RowIndex).Cells(0).Value)
            End If
            '
            stopWatch.Stop()
            lblStatus.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            stopWatch.Elapsed.Hours, stopWatch.Elapsed.Minutes,
            stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds / 10)
            My.Application.DoEvents()
        End If
    End Sub

    Private Function AccessionPaymentExists(ByVal AccID As String) As Boolean
        Dim Has As Boolean = False
        Try
            Dim sSQL As String = "Select * from Payments where Accession_ID = " & AccID & " or (ArType " &
            "<> 1 and Ar_ID in (Select PrimePayer_ID from Requisitions where ID = " & AccID & "))"
            Dim cnhp As New SqlConnection(connString)
            cnhp.Open()
            Dim cmdhp As New SqlCommand(sSQL, cnhp)
            cmdhp.CommandType = CommandType.Text
            Dim drhp As SqlDataReader = cmdhp.ExecuteReader
            If drhp.HasRows Then Has = True
            cnhp.Close()
            cnhp = Nothing
        Catch ex As Exception
            Has = False
        End Try
        Return Has
    End Function

    Private Sub DisplayDeleter(ByVal AccID As Long)
        txtUser.Text = ""
        Dim cndd As New SqlConnection(connString)
        cndd.Open()
        Dim cmddd As New SqlCommand("Select User_ID from " &
        "User_Event where Event_ID = 4 and Object_ID = " & AccID, cndd)
        cmddd.CommandType = CommandType.Text
        Dim drdd As SqlDataReader = cmddd.ExecuteReader
        If drdd.HasRows Then
            While drdd.Read
                txtUser.Text = Trim(GetUserName(drdd("User_ID")))
            End While
        End If
        cndd.Close()
        cndd = Nothing
    End Sub

    Private Sub DisplayAttendingProvider(ByVal AccID As Long)
        txtAttendingProvider.Text = ""
        Dim sSQL As String = "Select a.*, b.* from Providers a Left outer join Addresses b on a.Address_ID = b.ID " &
        "where a.ID in (Select AttendingProvider_ID from Requisitions where ID = " & AccID & ")"
        Dim cnsql As New SqlConnection(connString)
        cnsql.Open()
        Dim selcmd As New SqlCommand(sSQL, cnsql)
        selcmd.CommandType = CommandType.Text
        Dim selDR As SqlDataReader = selcmd.ExecuteReader
        If selDR.HasRows Then
            While selDR.Read
                If selDR("IsIndividual") Is DBNull.Value AndAlso selDR("IsIndividual") = 0 Then
                    txtAttendingProvider.Text = selDR("LastName_BSN")
                Else
                    txtAttendingProvider.Text = selDR("LastName_BSN") & ", " &
                    selDR("FirstName")
                    If selDR("Degree") IsNot DBNull.Value AndAlso
                    Trim(selDR("Degree")) <> "" Then
                        txtAttendingProvider.Text += " " & Trim(selDR("Degree"))
                    End If
                End If
                If selDR("Address_ID") IsNot DBNull.Value Then
                    txtAttendingProvider.Text += " " & GetAddress(selDR("Address_ID"))
                End If
            End While
        End If
        cnsql.Close()
        cnsql = Nothing
    End Sub

    Private Sub DisplaySpecimen(ByVal AccID As Long)
        txtSpecimen.Text = ""
        Dim SP As String = ""
        Dim NL As Integer = 0
        Dim sSQL As String = "Select a.*, b.Name as SName from Specimens a inner join Sources b on a.Source_ID = b.ID where a.Accession_ID = " & AccID
        Dim cnds As New SqlConnection(connString)
        cnds.Open()
        Dim cmdds As New SqlCommand(sSQL, cnds)
        cmdds.CommandType = CommandType.Text
        Dim dads As New SqlDataAdapter(cmdds)
        Dim tb As New DataTable
        dads.Fill(tb)
        cnds.Close()
        cnds = Nothing
        For i As Integer = 0 To tb.Rows.Count - 1
            If NL < Len(tb.Rows(i).Item("SName")) Then NL = Len(tb.Rows(i).Item("SName"))
        Next
        For i As Integer = 0 To tb.Rows.Count - 1
            SP += Format(tb.Rows(i).Item("SourceQuantity"), "#0") & " " & tb.Rows(i).Item("SName") &
            Space(NL + ((48 - NL) / Len(tb.Rows(i).Item("SName")))) & " Drawn: " &
            Format(tb.Rows(i).Item("SourceDate"), SystemConfig.DateFormat & " HH:mm") & vbCrLf
        Next
        If SP.EndsWith(vbCrLf) Then _
        SP = Microsoft.VisualBasic.Mid(SP, 1, Len(SP) - Len(vbCrLf))
        txtSpecimen.Text = SP
    End Sub

    Private Sub DisplayDxs(ByVal AccID As Long)
        txtDxs.Text = ""
        Dim Dxs As String = ""
        Dim sSQL As String = "Select Dx_Code from Req_Dx where Accession_ID = " & AccID
        Dim cndxs As New SqlConnection(connString)
        cndxs.Open()
        Dim cmddxs As New SqlCommand(sSQL, cndxs)
        cmddxs.CommandType = CommandType.Text
        Dim drdxs As SqlDataReader = cmddxs.ExecuteReader
        If drdxs.HasRows Then
            While drdxs.Read
                Dxs += drdxs("Dx_Code") & ", "
            End While
            Dxs = Microsoft.VisualBasic.Mid(Dxs, 1, Len(Dxs) - 2)
        End If
        cndxs.Close()
        cndxs = Nothing
        txtDxs.Text = Dxs
    End Sub

    Private Sub DisplayPatient(ByVal AccID As Long)
        txtPatient.Text = ""
        Dim sSQL As String = "Select a.*, b.* from Patients a Left outer join Addresses b on a.Address_ID = b.ID " &
        "where a.ID in (Select Patient_ID from Requisitions where ID = " & AccID & ")"
        Dim cnsql As New SqlConnection(connString)
        cnsql.Open()
        Dim selcmd As New SqlCommand(sSQL, cnsql)
        selcmd.CommandType = CommandType.Text
        Dim selDR As SqlDataReader = selcmd.ExecuteReader
        If selDR.HasRows Then
            While selDR.Read
                txtPatient.Text = "ID: " & selDR("ID") & vbCrLf
                txtPatient.Text += "Name: " & selDR("LastName") & ", " & selDR("FirstName") & vbCrLf
                txtPatient.Text += "DOB: " & Format(selDR("DOB"), SystemConfig.DateFormat) &
                "   Sex: " & selDR("Sex") & vbCrLf
                If selDR("Address_ID") IsNot DBNull.Value Then
                    txtPatient.Text += "Address: " & GetAddress1(selDR("Address_ID")) &
                    " " & GetAddress2(selDR("Address_ID")) & ", " & GetAddressCity(selDR("Address_ID")) _
                    & ", " & GetAddressState(selDR("Address_ID")) & " " & GetAddressZip(selDR("Address_ID"))
                Else
                    txtPatient.Text += "Address: None"
                End If
                If selDR("HomePhone") IsNot DBNull.Value Then _
                txtPatient.Text += vbCrLf & "Phone: " & selDR("HomePhone")
            End While
        End If
        cnsql.Close()
        cnsql = Nothing
    End Sub

    Private Sub DisplayBillee(ByVal AccID As Long)
        txtBillee.Text = ""
        Dim sSQL As String = "Select * from Requisitions where ID = " & AccID
        Dim cndb As New SqlConnection(connString)
        cndb.Open()
        Dim cmddb As New SqlCommand(sSQL, cndb)
        cmddb.CommandType = CommandType.Text
        Dim drdb As SqlDataReader = cmddb.ExecuteReader
        If drdb.HasRows Then
            While drdb.Read
                If drdb("BillingType_ID") = 0 AndAlso
                drdb("OrderingProvider_ID") IsNot DBNull.Value Then
                    lblResp.Text = "Responsible Party: Client"
                    txtBillee.Text = GetProvider(drdb("OrderingProvider_ID"))
                ElseIf drdb("BillingType_ID") = 2 AndAlso
                drdb("Patient_ID") IsNot DBNull.Value Then
                    lblResp.Text = "Responsible Party: Patient"
                    txtBillee.Text = GetPatient(drdb("Patient_ID"))
                ElseIf drdb("BillingType_ID") = 1 AndAlso
                drdb("PrimePayer_ID") IsNot DBNull.Value Then
                    lblResp.Text = "Responsible Party: Third Party"
                    txtBillee.Text = GetPolicy(AccID) & vbCrLf & GetInsurance(drdb("PrimePayer_ID"))
                Else
                    lblResp.Text = "Responsible Party: "
                    txtBillee.Text = ""
                End If
            End While
        End If
        cndb.Close()
        cndb = Nothing
    End Sub

    Private Function GetPolicy(ByVal AccID As Long) As String
        Dim Policy As String = "Policy: "
        Dim sSQL As String = "Select * from Req_Coverage " &
        "where Accession_ID = " & AccID & " and Preference = 'P'"
        Dim cngp As New SqlConnection(connString)
        cngp.Open()
        Dim cmdgp As New SqlCommand(sSQL, cngp)
        cmdgp.CommandType = CommandType.Text
        Dim drgp As SqlDataReader = cmdgp.ExecuteReader
        If drgp.HasRows Then
            While drgp.Read
                Policy += drgp("PolicyNo")
                If drgp("GroupNo") IsNot DBNull.Value _
                AndAlso Trim(drgp("GroupNo")) <> "" Then
                    Policy += "   Group: " & Trim(drgp("GroupNo"))
                End If
            End While
        End If
        cngp.Close()
        cngp = Nothing
        Return Policy
    End Function

    Private Function GetInsurance(ByVal PayerID As Long) As String
        Dim Payer As String = ""
        Dim sSQL As String = "Select * from Payers where ID = " & PayerID
        Dim cngi As New SqlConnection(connString)
        cngi.Open()
        Dim cmdgi As New SqlCommand(sSQL, cngi)
        cmdgi.CommandType = CommandType.Text
        Dim drgi As SqlDataReader = cmdgi.ExecuteReader
        If drgi.HasRows Then
            While drgi.Read
                Payer = drgi("PayerName") & vbCrLf
                If drgi("Address_ID") IsNot DBNull.Value Then
                    Payer += GetAddress1(drgi("Address_ID")) & " " &
                    GetAddress2(drgi("Address_ID")) & ", " & GetAddressCity(drgi("Address_ID")) &
                    ", " & GetAddressState(drgi("Address_ID")) & ", " & GetAddressZip(drgi("Address_ID")) &
                    vbCrLf & "Phone: "
                Else
                    Payer += "None" & vbCrLf & "Phone: "
                End If
                Payer += IIf(drgi("Phone") Is DBNull.Value, "", drgi("Phone"))
            End While
        End If
        cngi.Close()
        cngi = Nothing
        Return Payer
    End Function

    Private Function GetProvider(ByVal ProviderID As Long) As String
        Dim PrName As String = ""
        Dim sSQL As String = "Select * from Providers where ID = " & ProviderID
        Dim cngpr As New SqlConnection(connString)
        cngpr.Open()
        Dim cmdgpr As New SqlCommand(sSQL, cngpr)
        cmdgpr.CommandType = CommandType.Text
        Dim drgpr As SqlDataReader = cmdgpr.ExecuteReader
        If drgpr.HasRows Then
            While drgpr.Read
                If drgpr("IsIndividual") IsNot DBNull.Value AndAlso drgpr("IsIndividual") = 0 Then
                    PrName = drgpr("LastName_BSN")
                    If drgpr("Address_ID") IsNot DBNull.Value Then _
                    PrName += vbCrLf & GetAddress(drgpr("Address_ID"))
                    If drgpr("Phone") IsNot DBNull.Value Then _
                    PrName += vbCrLf & "Phone: " & drgpr("Phone")
                Else
                    PrName = drgpr("LastName_BSN") & ", " & drgpr("FirstName")
                    If drgpr("Degree") IsNot DBNull.Value Then PrName += " " & drgpr("Degree")
                    If drgpr("Address_ID") IsNot DBNull.Value Then PrName += vbCrLf & GetAddress(drgpr("Address_ID"))
                    If drgpr("Phone") IsNot DBNull.Value Then PrName += vbCrLf & "Phone: " & drgpr("Phone")
                End If
            End While
        End If
        cngpr.Close()
        cngpr = Nothing
        Return PrName
    End Function

    Private Function GetPatient(ByVal PatID As Long) As String
        Dim Pat As String = ""
        Dim sSQL As String = "Select * from Patients where ID = " & PatID
        Dim cngpt As New SqlConnection(connString)
        cngpt.Open()
        Dim cmdgpt As New SqlCommand(sSQL, cngpt)
        cmdgpt.CommandType = CommandType.Text
        Dim drgpt As SqlDataReader = cmdgpt.ExecuteReader
        If drgpt.HasRows Then
            While drgpt.Read
                Pat = drgpt("LastName") & ", " & drgpt("FirstName")
                If drgpt("Address_ID") IsNot DBNull.Value Then _
                Pat += vbCrLf & GetAddress(drgpt("Address_ID"))
                If drgpt("HomePhone") IsNot DBNull.Value Then _
                Pat += vbCrLf & "Phone: " & drgpt("HomePhone")
            End While
        End If
        cngpt.Close()
        cngpt = Nothing
        Return Pat
    End Function

    Private Sub DisplayOrders(ByVal AccID As Long)
        dgvOrders.Rows.Clear()
        Dim sSQL As String = "Select a.TGP_ID, (Select Name from Tests where ID = a.TGP_ID Union " &
        "Select Name from Groups where ID = a.TGP_ID Union Select Name from Profiles where ID = a.TGP_ID) " &
        "as TGPName, a.IsStat from Req_TGP a where a.Accession_ID = " & AccID
        Dim cno As New SqlConnection(connString)
        cno.Open()
        Dim cmdo As New SqlCommand(sSQL, cno)
        Dim dro As SqlDataReader = cmdo.ExecuteReader
        If dro.HasRows Then
            Try
                While dro.Read
                    If dro("TGP_ID") IsNot DBNull.Value AndAlso
                    dro("TGPName") IsNot DBNull.Value AndAlso
                    dro("IsStat") IsNot DBNull.Value Then _
                    dgvOrders.Rows.Add(dro("TGP_ID"), dro("TGPName"), dro("IsStat"))
                End While
            Catch ex As Exception
            End Try
        End If
        cno.Close()
        cno = Nothing
    End Sub

    Private Sub btnPrintSum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintSum.Click
        If dgvAccessions.RowCount > 0 Then
            ProcessSumReport()
        End If
    End Sub

    Private Sub ProcessSumReport()
        'TODO: Crystal Report code till end
        'Dim Temps() As String = {""}
        'Dim LName As String = ""
        'Dim FName As String = ""
        'Dim Formula As String = ""
        'Dim gReport As New ReportDocument
        'gReport.Load(GetReportPath("Accession_Dashboard_Summary.rpt"))
        'ApplyNewServer(gReport, My.Settings.DSN,
        'My.Settings.Database, My.Settings.UID, My.Settings.PWD)
        ''
        'If cmbDateType.SelectedIndex = 0 Then   'ACC
        '    Formula = "{Requisitions.AccessionDate} in Date(" & dtpFrom.Value.Year _
        '    & ", " & dtpFrom.Value.Month & ", " & dtpFrom.Value.Day & ") To Date(" &
        '    dtpTo.Value.Year & ", " & dtpTo.Value.Month & ", " & dtpTo.Value.Day & ")"
        'ElseIf cmbDateType.SelectedIndex = 1 Then   'COLL
        '    Formula = "{Specimens.SourceDate} in Date(" & dtpFrom.Value.Year _
        '    & ", " & dtpFrom.Value.Month & ", " & dtpFrom.Value.Day & ") To Date(" &
        '    dtpTo.Value.Year & ", " & dtpTo.Value.Month & ", " & dtpTo.Value.Day & ")"
        'Else                            'Receive
        '    Formula = "{Requisitions.ReceivedTime} in Date(" & dtpFrom.Value.Year _
        '    & ", " & dtpFrom.Value.Month & ", " & dtpFrom.Value.Day & ") To Date(" &
        '    dtpTo.Value.Year & ", " & dtpTo.Value.Month & ", " & dtpTo.Value.Day & ")"
        'End If
        ''
        'If cmbCriteria.SelectedIndex <> -1 Then
        '    If Trim(txtTerm.Text) <> "" And txtTerm.ReadOnly = False Then
        '        If cmbCriteria.SelectedItem.ToString = "Accession ID" Then
        '            Formula += " and {Requisitions.ID} = " & Val(Trim(txtTerm.Text))
        '        ElseIf cmbCriteria.SelectedItem.ToString = "Provider ID" Then
        '            Formula += " and {Providers.ID} = " & Val(Trim(txtTerm.Text))
        '        ElseIf cmbCriteria.SelectedItem.ToString = "Provider Name (Last, First)" Then
        '            If InStr(Trim(txtTerm.Text), ",") > 0 Then 'both Names
        '                Temps = Split(Trim(txtTerm.Text), ",")
        '                LName = Trim(Temps(0))
        '                FName = Trim(Temps(1))
        '            Else
        '                LName = Trim(txtTerm.Text)
        '            End If
        '            If LName <> "" And FName <> "" Then
        '                Formula += " and {Providers.LastName_BSN} Like " & LName & "* and " &
        '                "{Providers.FirstName} Like " & FName & "*"
        '            ElseIf LName <> "" And FName = "" Then
        '                Formula += " and {Providers.LastName_BSN} Like '" & LName & "*'"
        '            End If
        '        ElseIf cmbCriteria.SelectedItem.ToString = "Patient ID" Then
        '            Formula += " and {Patients.ID} = " & Val(Trim(txtTerm.Text))
        '        ElseIf cmbCriteria.SelectedItem.ToString = "Patient Name (Last, First)" Then
        '            If InStr(Trim(txtTerm.Text), ",") > 0 Then 'both Names
        '                Temps = Split(Trim(txtTerm.Text), ",")
        '                LName = Trim(Temps(0))
        '                FName = Trim(Temps(1))
        '            Else
        '                LName = Trim(txtTerm.Text)
        '            End If
        '            If LName <> "" And FName <> "" Then
        '                Formula += " and {Patients.LastName} Like '" & LName & "*' and " &
        '                "{Patients.FirstName} Like '" & FName & "*'"
        '            ElseIf LName <> "" And FName = "" Then
        '                Formula += " and {Patients.LastName} Like '" & LName & "*'"
        '            End If
        '        End If
        '    ElseIf Trim(txtTerm.Text) = "" And txtTerm.ReadOnly = True Then
        '        If cmbCriteria.SelectedItem.ToString = "Client Billed" Then
        '            Formula += " and {Requisitions.BillingType_ID} = 0"
        '        ElseIf cmbCriteria.SelectedItem.ToString = "Patient Billed" Then
        '            Formula += " and {Requisitions.BillingType_ID} = 2"
        '        ElseIf cmbCriteria.SelectedItem.ToString = "Insurance Billed" Then
        '            Formula += " and {Requisitions.BillingType_ID} = 1"
        '        ElseIf cmbCriteria.SelectedItem.ToString = "Gratis" Then
        '            Formula += " and {Requisitions.IsGratis} = True"
        '        ElseIf cmbCriteria.SelectedItem.ToString = "Requisition Number" Then
        '            Formula += " and {Requisitions.RequisitionNo} ='" & Trim(txtTerm.Text) & "'"
        '        End If
        '    End If
        'End If
        ''
        'gReport.RecordSelectionFormula = Formula
        ''frmRV.CRRV.ReportSource = gReport
        ''frmRV.CRRV.RefreshReport()
        ''' 

        'frmRV.CRRV.ReportSource = gReport
        'frmRV.CRRV.RefreshReport()
        'frmRV.Show()
        'frmRV.MdiParent = frmDashboard
    End Sub

    Private Sub ProcessDetReport()

        'Dim Temps() As String = {""}
        'Dim LName As String = ""
        'Dim FName As String = ""
        'Dim Formula As String = ""
        'Dim gReport As New ReportDocument
        'gReport.Load(GetReportPath("Accession_Dashboard_Detail.rpt"))
        'ApplyNewServer(gReport, My.Settings.DSN,
        'My.Settings.Database, My.Settings.UID, My.Settings.PWD)
        ''
        'If cmbDateType.SelectedIndex = 0 Then   'ACC
        '    Formula = "{Requisitions.AccessionDate} in Date(" & dtpFrom.Value.Year _
        '    & ", " & dtpFrom.Value.Month & ", " & dtpFrom.Value.Day & ") To Date(" &
        '    dtpTo.Value.Year & ", " & dtpTo.Value.Month & ", " & dtpTo.Value.Day & ")"
        'ElseIf cmbDateType.SelectedIndex = 1 Then   'COLL
        '    Formula = "{Specimens.SourceDate} in Date(" & dtpFrom.Value.Year _
        '    & ", " & dtpFrom.Value.Month & ", " & dtpFrom.Value.Day & ") To Date(" &
        '    dtpTo.Value.Year & ", " & dtpTo.Value.Month & ", " & dtpTo.Value.Day & ")"
        'Else                            'Receive
        '    Formula = "{Requisitions.ReceivedTime} in Date(" & dtpFrom.Value.Year _
        '    & ", " & dtpFrom.Value.Month & ", " & dtpFrom.Value.Day & ") To Date(" &
        '    dtpTo.Value.Year & ", " & dtpTo.Value.Month & ", " & dtpTo.Value.Day & ")"
        'End If
        ''
        'If cmbCriteria.SelectedIndex <> -1 Then
        '    If Trim(txtTerm.Text) <> "" And txtTerm.ReadOnly = False Then
        '        If cmbCriteria.SelectedItem.ToString = "Accession ID" Then
        '            Formula += " and {Requisitions.ID} = " & Val(Trim(txtTerm.Text))
        '        ElseIf cmbCriteria.SelectedItem.ToString = "Provider ID" Then
        '            Formula += " and {Providers.ID} = " & Val(Trim(txtTerm.Text))
        '        ElseIf cmbCriteria.SelectedItem.ToString = "Provider Name (Last, First)" Then
        '            If InStr(Trim(txtTerm.Text), ",") > 0 Then 'both Names
        '                Temps = Split(Trim(txtTerm.Text), ",")
        '                LName = Trim(Temps(0))
        '                FName = Trim(Temps(1))
        '            Else
        '                LName = Trim(txtTerm.Text)
        '            End If
        '            If LName <> "" And FName <> "" Then
        '                Formula += " and {Providers.LastName_BSN} Like " & LName & "* and " &
        '                "{Providers.FirstName} Like " & FName & "*"
        '            ElseIf LName <> "" And FName = "" Then
        '                Formula += " and {Providers.LastName_BSN} Like '" & LName & "*'"
        '            End If
        '        ElseIf cmbCriteria.SelectedItem.ToString = "Patient ID" Then
        '            Formula += " and {Patients.ID} = " & Val(Trim(txtTerm.Text))
        '        ElseIf cmbCriteria.SelectedItem.ToString = "Patient Name (Last, First)" Then
        '            If InStr(Trim(txtTerm.Text), ",") > 0 Then 'both Names
        '                Temps = Split(Trim(txtTerm.Text), ",")
        '                LName = Trim(Temps(0))
        '                FName = Trim(Temps(1))
        '            Else
        '                LName = Trim(txtTerm.Text)
        '            End If
        '            If LName <> "" And FName <> "" Then
        '                Formula += " and {Patients.LastName} Like '" & LName & "*' and " &
        '                "{Patients.FirstName} Like '" & FName & "*'"
        '            ElseIf LName <> "" And FName = "" Then
        '                Formula += " and {Patients.LastName} Like '" & LName & "*'"
        '            End If
        '        End If
        '    ElseIf Trim(txtTerm.Text) = "" And txtTerm.ReadOnly = True Then
        '        If cmbCriteria.SelectedItem.ToString = "Client Billed" Then
        '            Formula += " and {Requisitions.BillingType_ID} = 0"
        '        ElseIf cmbCriteria.SelectedItem.ToString = "Patient Billed" Then
        '            Formula += " and {Requisitions.BillingType_ID} = 2"
        '        ElseIf cmbCriteria.SelectedItem.ToString = "Insurance Billed" Then
        '            Formula += " and {Requisitions.BillingType_ID} = 1"
        '        ElseIf cmbCriteria.SelectedItem.ToString = "Gratis" Then
        '            Formula += " and {Requisitions.IsGratis} = True"
        '        End If
        '    End If
        'End If
        ''
        'gReport.RecordSelectionFormula = Formula
        'frmRV.CRRV.ReportSource = gReport
        'frmRV.CRRV.RefreshReport()
        '' 
        'frmRV.Show()
        'frmRV.MdiParent = frmDashboard
    End Sub

    Private Sub btnPrintDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintDet.Click
        If dgvAccessions.RowCount > 0 Then
            ProcessDetReport()
        End If
    End Sub

    Private Sub frmAccDash_ResizeBegin(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeBegin
        origWidth = Me.Width
        origHeight = Me.Height
    End Sub

    Private Sub frmAccDash_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        MeResize(Me, origWidth, origHeight)
    End Sub

    Private Sub cmbStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbStatus.SelectedIndexChanged
        If cmbStatus.SelectedIndex = 0 Or cmbStatus.SelectedIndex = 2 Then
            txtUser.Text = ""
            lblUser.Visible = False
            txtUser.Visible = False
        Else
            lblUser.Visible = True
            txtUser.Visible = True
        End If
        dgvAccessions.Rows.Clear()
        txtTotal.Text = "0"
        txtProviders.Text = "0"
        txtPatients.Text = "0"
        txtGratis.Text = "0"
        txtPatBilled.Text = "0"
        txtClientBilled.Text = "0"
        txt3PBilled.Text = "0"
        txtAttendingProvider.Text = ""
        txtPatient.Text = ""
        txtDxs.Text = ""
        dgvOrders.Rows.Clear()
        txtBillee.Text = ""
        txtSpecimen.Text = ""
    End Sub

    Private Sub btnPmtReceipt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPmtReceipt.Click
        'TODO: Crystal Report code till End
        'If IO.File.Exists(GetReportPath("Accession Payment Receipt.rpt")) Then
        '    Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        '    oRpt.Load(GetReportPath("Accession Payment Receipt.rpt"))
        '    oRpt.RecordSelectionFormula = "{Requisitions.ID} = " & dgvAccessions.SelectedRows(0).Cells(0).Value & ";"
        '    ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database,
        '    My.Settings.UID, My.Settings.PWD)
        '    frmRV.CRRV.ReportSource = oRpt
        '    'frmRV.MdiParent = Me
        '    frmRV.Show()
        'Else
        '    MsgBox("The 'Accession Payment Receipt' report does not exist in the app directory. Please contact Prolis support.", MsgBoxStyle.Critical, "Prolis")
        'End If
    End Sub

    Private Sub dgvAccessions_MouseClick(sender As Object, e As MouseEventArgs) Handles dgvAccessions.MouseClick

    End Sub

    Private Sub dgvAccessions_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAccessions.CellDoubleClick
        Try
            Clipboard.SetText(dgvAccessions.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
            Label2.Text = dgvAccessions.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & " copied to clipboard"
            Label2.ForeColor = Color.Red
            If IsNumeric(Clipboard.GetText()) Then
                openInRequi.Show()
            Else
                openInRequi.Hide()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub dgvAccessions_CellBorderStyleChanged(sender As Object, e As EventArgs) Handles dgvAccessions.CellBorderStyleChanged

    End Sub




    Private Sub openInRequi_Click(sender As Object, e As EventArgs) Handles openInRequi.Click
        Dim ff As String = Clipboard.GetText().ToString()
        If IsNumeric(Clipboard.GetText()) Then
            frmRequisitions.btnNew.Checked = True
            frmRequisitions.txtAccFrom.Text = Clipboard.GetText()

            frmRequisitions.StartPosition = FormStartPosition.CenterScreen
            frmRequisitions.MdiParent = frmDashboard
            frmRequisitions.Show()
            frmRequisitions.btnLoad.PerformClick()
        End If
    End Sub

    Private Sub Label19_Click(sender As Object, e As EventArgs) Handles Label19.Click
        txtTerm.Text = Clipboard.GetText()
    End Sub
End Class
