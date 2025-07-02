Imports System.Windows.Forms
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Data
'''spire
Imports System.Security.AccessControl
Imports Microsoft.SqlClient
Imports Microsoft.Data.SqlClient

Public Class frmResInq
    Public PatToSearch As String = ""
    Private origWidth As Integer
    Private origHeight As Integer

    Private Sub chkAccHist_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAccHist.CheckedChanged
        If chkAccHist.Checked = False Then
            chkAccHist.Text = "Accessioned"
            chkAccHist.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Acc.ico")
            grpHist.Enabled = False
            grpAcc.Enabled = True
            TabControl1.SelectTab(0)
            dgvHistResults.Visible = False
            dgvAccResults.Visible = True
        Else
            chkAccHist.Text = "History"
            chkAccHist.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\ViewHistory.ico")
            grpHist.Enabled = True
            grpAcc.Enabled = False
            TabControl1.SelectTab(1)
            dgvAccResults.Visible = False
            dgvHistResults.Visible = True
            If txtPatientID.Text <> "" Then
                PopulatePatProviders(Val(txtPatientID.Text))
                If cmbTest.Items.Count = 0 Then PopulateTests(Val(txtPatientID.Text))
            End If

        End If
        cmbAccession.SelectedIndex = -1
        cmbTest.SelectedIndex = -1
        chkReport.Checked = chkAccHist.Checked
        FaxEmailUpdate()
    End Sub

    Private Sub frmResInq_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If dgvReports.RowCount > 0 Then UpdateScheduler()
        Timer1.Enabled = False
    End Sub

    Private Function UpdateProviderLink(ByVal ProviderID As Long, ByVal AccID As Long) As String()
        Dim Providers() As String = {"", "", "", ""}    'Owner, AccID, Referring, Comment
        Dim cnpl As New SqlConnection(connString)
        cnpl.Open()
        Dim cmdpl As New SqlCommand("Select OrderingProvider_ID from Requisitions where ID = " & AccID, cnpl)
        cmdpl.CommandType = CommandType.Text
        Dim drpl As SqlDataReader = cmdpl.ExecuteReader
        If drpl.HasRows Then
            While drpl.Read
                If drpl("OrderingProvider_ID") <> ProviderID Then     'Not Owner
                    Providers(0) = drpl("OrderingProvider_ID").ToString  'Owner
                    Providers(1) = AccID.ToString
                    Providers(2) = ProviderID.ToString  'Referring
                    Providers(3) = "This report is being provided to " & GetProviderName(ProviderID) &
                    ", at the request of " & GetProviderName(drpl("OrderingProvider_ID"))
                Else
                    Providers(0) = ProviderID.ToString   'Owner
                    Providers(1) = AccID.ToString
                End If
            End While
        End If
        cnpl.Close()
        cnpl = Nothing
        '
        If Providers(2).ToString <> "" Then     'Referring instance
            ExecuteSqlProcedure("If not Exists (Select * from Req_Rpt where Provider_ID = " & Providers(0) _
            & " and RPT_Type = 'ACC' and Base_ID = " & Providers(1) & ") Insert into Req_RPT (Provider_ID, " &
            "Rpt_Type, Base_ID, EntryDate, EntrySource, Rpt_Complete, Rpt_Print, Rpt_Fax, Fax, Priority, " &
            "RPT_Email, Email, Executed, Comment) values (" & Providers(0) & ", 'ACC', " & Providers(1) &
            ", '" & Date.Now & "', 'Automation', 0, 1, " & Convert.ToInt16(Fax) & ", '', 0, 0, '', 0, '" &
            Providers(3) & "')")
        End If
        Return Providers
    End Function

    Private Sub UpdateScheduler()
        Dim Email As String = ""
        Dim ToEmail As Int16 = 0
        Dim Providers() As String = {"", "", "", ""}    'Owner, AccID, Referring, Comment
        Dim ProviderConfigs() As String = {"", "", "", "", "", "", "", "", "", ""}
        '0=ProviderID, 1=Complete, 2=Print, 3=Prolison, 4=Interface, 5=RPTFax
        '6=Fax#, 7=RPTEmail, 8=Email, 9=RPTFile
        For i As Integer = 0 To dgvReports.RowCount - 1
            GetProviderConfigs(dgvReports.Rows(i).Cells(0).Value)
            If ProviderConfigs(1) = "" Then ProviderConfigs(1) = "0"
            If ProviderConfigs(2) = "" Then ProviderConfigs(2) = "0"
            If ProviderConfigs(3) = "" Then ProviderConfigs(3) = "0"
            If ProviderConfigs(4) = "" Then ProviderConfigs(4) = "0"
            If ProviderConfigs(5) = "" Then ProviderConfigs(5) = "0"
            If ProviderConfigs(7) = "" Then ProviderConfigs(7) = "0"
            '
            ToEmail = Convert.ToInt16(dgvReports.Rows(i).Cells(8).Value)
            If ToEmail <> 0 Then
                If dgvReports.Rows(i).Cells(9).Value IsNot Nothing _
                AndAlso Trim(dgvReports.Rows(i).Cells(9).Value) <> "" Then
                    Email = Trim(dgvReports.Rows(i).Cells(9).Value)
                Else    'not provided
                    Email = ""
                    ToEmail = 0
                End If
            Else
                Email = Trim(dgvReports.Rows(i).Cells(9).Value)
            End If
            '
            ExecuteSqlProcedure("If not Exists (Select * from Req_Rpt where Provider_ID = " &
            dgvReports.Rows(i).Cells(0).Value & " and EntrySource = '" & dgvReports.Rows(i).Cells(4).Value &
            "' and Base_ID = " & dgvReports.Rows(i).Cells(1).Value & ") Insert into Req_RPT (Provider_ID, " &
            "Base_ID, EntrySource, Rpt_Type, EntryDate, RDM_Auto, Rpt_Complete, Rpt_Print, RPT_Prolison, " &
            "RPT_Interface, Rpt_Fax, Fax, Priority, RPT_Email, Email, Executed, Comment) values (" &
            dgvReports.Rows(i).Cells(0).Value & ", " & dgvReports.Rows(i).Cells(1).Value & ", '" &
            dgvReports.Rows(i).Cells(4).Value & "', '" & dgvReports.Rows(i).Cells(2).Value & "', '" &
            dgvReports.Rows(i).Cells(3).Value & "', 0, 0, " & Convert.ToInt16(ProviderConfigs(2)) & ", " &
            Convert.ToInt16(ProviderConfigs(3)) & ", " & Convert.ToInt16(ProviderConfigs(4)) & ", " &
            Convert.ToInt16(dgvReports.Rows(i).Cells(5).Value) & ", '" &
            PhoneNeat(dgvReports.Rows(i).Cells(6).Value) & "', 1, " & ToEmail & ", '" & Email & "', 0, '')")
            '
            If dgvReports.Rows(i).Cells(5).Value = True And dgvReports.Rows(i).Cells(8).Value = False Then
                ExecuteSqlProcedure("Delete from Event_Capture where Accession_ID = " & dgvReports.Rows(i).Cells(1).Value &
                " and Event_ID = 12 and Provider_ID = " & dgvReports.Rows(i).Cells(0).Value)
                ExecuteSqlProcedure("Delete from Fax_Log where Status = 'Failed' " &
                "and Accession_ID = " & dgvReports.Rows(i).Cells(1).Value)
            ElseIf dgvReports.Rows(i).Cells(5).Value = False And dgvReports.Rows(i).Cells(8).Value = True Then
                ExecuteSqlProcedure("Delete from Event_Capture where Accession_ID = " & dgvReports.Rows(i).Cells(1).Value &
                " and Event_ID = 13 and Provider_ID = " & dgvReports.Rows(i).Cells(0).Value)
            ElseIf dgvReports.Rows(i).Cells(5).Value = True And dgvReports.Rows(i).Cells(8).Value = True Then
                ExecuteSqlProcedure("Delete from Event_Capture where Accession_ID = " & dgvReports.Rows(i).Cells(1).Value &
                " and Event_ID in (12, 13) and Provider_ID = " & dgvReports.Rows(i).Cells(0).Value)
                ExecuteSqlProcedure("Delete from Fax_Log where Status = 'Failed' " &
                "and Accession_ID = " & dgvReports.Rows(i).Cells(1).Value)
            End If
            If SystemConfig.AuditTrail Then 'Audit Trail is ON
                If dgvReports.Rows(i).Cells(5).Value = True Then _
                LogUserEvent(ThisUser.ID, 72, Date.Now.ToString, "Accession",
                dgvReports.Rows(i).Cells(1).Value, "Provider: " &
                dgvReports.Rows(i).Cells(0).Value, "Fax: " &
                dgvReports.Rows(i).Cells(6).Value)
                If dgvReports.Rows(i).Cells(8).Value = True Then _
                LogUserEvent(ThisUser.ID, 73, Date.Now.ToString, "Accession",
                dgvReports.Rows(i).Cells(1).Value, "Provider: " &
                dgvReports.Rows(i).Cells(0).Value, "Email: " &
                dgvReports.Rows(i).Cells(9).Value)
            End If
        Next
        dgvReports.Rows.Clear()
    End Sub

    Private Sub frmResInq_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CN.Open("DSN=ProlisQC")
        Me.MaximumSize = MaxSize
        chkAccHist.Checked = False
        lblDOB.Text += " (" & SystemConfig.DateFormat & ")"
        lblAccDate.Text += " (" & SystemConfig.DateFormat & ")"
        txtFax.Mask = SystemConfig.PhoneMask
        Timer1.Interval = 300
        Timer1.Start()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub chkReport_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkReport.CheckedChanged
        If chkReport.Checked = False Then
            chkReport.Text = "ACC"
        Else
            chkReport.Text = "HIST"
        End If
        FaxEmailUpdate()
    End Sub

    Private Sub chkFax_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFax.CheckedChanged
        If chkFax.Checked = False Then
            txtFax.Enabled = False
        Else
            txtFax.Enabled = True
        End If
        UpdateRDMEntry()
        FaxEmailUpdate()
    End Sub

    Private Sub chkEmail_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEmail.CheckedChanged
        'If chkEmail.Checked = False Then
        '    txtEmail.Enabled = False
        'Else
        '    txtEmail.Enabled = True
        'End If
        'UpdateRDMEntry()
        'FaxEmailUpdate()
    End Sub

    Private Sub btnPatLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatLook.Click
        frmResPatLookUp.txtSearch.Text = txtName.Text
        frmResPatLookUp.btnPatSearch.PerformClick()
        Dim PatAcc As String = frmResPatLookUp.ShowDialog
        Dim data() As String = Split(PatAcc, "|")
        Dim PatientID As String = data(0)
        If PatientID <> "" Then
            DisplayPatient(Val(PatientID))
            PopulateAccessions(Val(PatientID))
            PopulateTests(Val(PatientID))
            PopulatePatProviders(Val(PatientID))
            Dim i As Integer
            For i = 0 To cmbAccession.Items.Count - 1
                If cmbAccession.Items(i).ToString = data(1) Then
                    cmbAccession.SelectedIndex = i
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub PopulatePatProviders(ByVal PatientID As Long)
        txtPatProvider.Text = ""
        Dim Provider As String = ""
        Dim cnpp As New SqlConnection(connString)
        cnpp.Open()
        Dim cmdpp As New SqlCommand("Select * from Providers " &
        "where ID in (Select distinct OrderingProvider_ID from Requisitions " &
        "where Patient_ID = " & PatientID & ")", cnpp)
        cmdpp.CommandType = CommandType.Text
        Dim drpp As SqlDataReader = cmdpp.ExecuteReader
        If drpp.HasRows Then
            While drpp.Read
                If drpp("IsIndividual") IsNot DBNull.Value AndAlso drpp("IsIndividual") = 0 Then
                    Provider = drpp("LastName_BSN")
                Else
                    If drpp("Degree") IsNot DBNull.Value AndAlso drpp("Degree") <> "" Then
                        Provider = drpp("LastName_BSN") & ", " & drpp("FirstName") & " " & drpp("Degree")
                    Else
                        Provider = drpp("LastName_BSN") & ", " & drpp("FirstName")
                    End If
                End If
                txtPatProvider.Text = Provider
                '
                If chkAccHist.Checked = True Then
                    PopulateProvider(drpp("ID"))
                End If
            End While
        End If
        cnpp.Close()
        cnpp = Nothing
    End Sub

    Private Sub PopulateTests(ByVal PatientID As Long)
        cmbTest.Items.Clear()
        Dim cnpt As New SqlConnection(connString)
        cnpt.Open()
        Dim cmdpt As New SqlCommand("Select * from Tests where ID in " &
        "(Select distinct a.Test_ID from Acc_Results a inner join Requisitions " &
        "b on a.Accession_ID = b.ID where b.Patient_ID = " & PatientID & ")", cnpt)
        cmdpt.CommandType = CommandType.Text
        Dim drpt As SqlDataReader = cmdpt.ExecuteReader
        If drpt.HasRows Then
            While drpt.Read
                cmbTest.Items.Add(New MyList(drpt("Name"), drpt("ID")))
            End While
        End If
        cnpt.Close()
        cnpt = Nothing
    End Sub

    Private Sub PopulateAccessions(ByVal PatientID As Long)
        cmbAccession.Items.Clear()
        Dim cnpa As New SqlConnection(connString)
        cnpa.Open()
        Dim cmdpa As New SqlCommand("Select distinct Accession_ID from " &
        "Acc_Results where Accession_ID in (Select ID from Requisitions where " &
        "Received <> 0 and Patient_ID = " & PatientID & ") order by Accession_ID DESC", cnpa)
        cmdpa.CommandType = CommandType.Text
        Dim drpa As SqlDataReader = cmdpa.ExecuteReader
        If drpa.HasRows Then
            While drpa.Read
                cmbAccession.Items.Add(drpa("Accession_ID").ToString)
            End While
            cmbAccession.DropDownStyle = ComboBoxStyle.DropDownList
        End If
        cnpa.Close()
        cnpa = Nothing
    End Sub

    Private Sub DisplayPatient(ByVal PatientID As Long)
        Dim cnp As New SqlConnection(connString)
        cnp.Open()
        Dim cmdp As New SqlCommand("Select * from Patients where ID = " & PatientID, cnp)
        cmdp.CommandType = CommandType.Text
        Dim drp As SqlDataReader = cmdp.ExecuteReader
        If drp.HasRows Then
            While drp.Read
                If drp("MiddleName") IsNot DBNull.Value _
                AndAlso Trim(drp("MiddleName")) <> "" Then
                    txtName.Text = Trim(drp("LastName")) & ", " &
                    Trim(drp("FirstName")) & " " & Trim(drp("MiddleName"))
                Else
                    txtName.Text = Trim(drp("LastName")) & ", " & Trim(drp("FirstName"))
                End If
                txtDOB.Text = Format(drp("DOB"), SystemConfig.DateFormat)
                txtSex.Text = drp("Sex")
                If drp("Sex") = "F" Then
                    pctSex.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Female.ico")
                Else
                    pctSex.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Male.ico")
                End If
                txtPatientID.Text = drp("ID").ToString
                If drp("Address_ID") IsNot DBNull.Value Then _
                txtAddress.Text = GetAddress(drp("Address_ID"))
                btnPatErase.Visible = True
            End While
        End If
        cnp.Close()
        cnp = Nothing
    End Sub

    Private Sub btnPatErase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatErase.Click
        ClearPatient()
        cmbAccession.SelectedIndex = -1
        cmbAccession.Items.Clear()
        cmbAccession.DropDownStyle = ComboBoxStyle.DropDown
        cmbTest.SelectedIndex = -1
        cmbTest.Items.Clear()
        btnPatErase.Visible = False
        btnAccErase.Visible = False
        btnAccErase_Click(sender, e)
        lblRPTStatus.Text = ""
    End Sub

    Private Sub ClearPatient()
        txtName.Text = ""
        txtDOB.Text = ""
        pctSex.Image = System.Drawing.Image.FromFile(Application.StartupPath _
        & "\Images\Blank.ico")
        txtSex.Text = ""
        txtPatientID.Text = ""
        txtAddress.Text = ""
        lblRPTStatus.Text = ""
    End Sub

    Private Sub btnAccErase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccErase.Click
        cmbAccession.SelectedIndex = -1
        If cmbAccession.Items.Count = 0 Then
            cmbAccession.Text = ""
            txtAccID.Text = ""
            txtMeds.Text = ""
            ClearPatient()
            btnPatErase.Visible = False
            txtSlideID.Text = ""
            txtAccDate.Text = ""
            txtPatProvider.Text = ""
            dgvAccResults.Rows.Clear()
            lblRPTStatus.Text = ""
            btnAccErase.Visible = False
        End If
    End Sub

    Private Sub cmbAccession_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAccession.GotFocus
        cmbAccession.BackColor = FCOLOR
    End Sub

    Private Sub cmbAccession_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbAccession.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbAccession_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAccession.LostFocus
        cmbAccession.BackColor = NFCOLOR
    End Sub

    Private Sub cmbAccession_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAccession.SelectedIndexChanged
        If cmbAccession.SelectedIndex <> -1 Then
            btnAccErase.Visible = True
            Dim ACCREQ As String = IIf(chkAccReq.Checked, "REQ", "ACC")
            DisplayProvider(Val(cmbAccession.SelectedItem.ToString))
            DisplayAccResults(Val(cmbAccession.SelectedItem.ToString))
            lblRPTStatus.Text = ""
            lblRPTStatus.Text = GetReportStatus(Val(cmbAccession.SelectedItem.ToString))
            If HasExtendedResult(cmbAccession.SelectedItem.ToString, ACCREQ) Then _
            lblRPTStatus.Text = lblRPTStatus.Text & "-Extended"
            btnPrint.Enabled = True
            If SystemConfig.AuditTrail = True Then _
            LogUserEvent(ThisUser.ID, 71, Date.Now, "Accession", cmbAccession.Text, "", "")
        Else
            btnAccErase.Visible = False
            txtAccDate.Text = ""
            txtPatProvider.Text = ""
            ClearProvider()
            lblRPTStatus.Text = ""
            dgvAccResults.Rows.Clear()
            btnPrint.Enabled = False
        End If
    End Sub

    Private Sub DisplayProvider(ByVal AccID As Long)
        Dim Provider As String = ""
        Dim sSQL As String = "Select a.AccessionDate, b.* from Requisitions a inner " &
        "join Providers b on b.ID = a.OrderingProvider_ID where a.ID = " & AccID
        '
        Dim cna As New SqlConnection(connString)
        cna.Open()
        Dim cmda As New SqlCommand(sSQL, cna)
        cmda.CommandType = CommandType.Text
        Dim dra As SqlDataReader = cmda.ExecuteReader
        If dra.HasRows Then
            While dra.Read
                If dra("AccessionDate") IsNot System.DBNull.Value Then _
                txtAccDate.Text = Format(dra("AccessionDate"), SystemConfig.DateFormat)
                If dra("IsIndividual") = False Then
                    Provider = dra("LastName_BSN")
                Else
                    If dra("Degree") IsNot DBNull.Value _
                    AndAlso dra("Degree") <> "" Then
                        Provider = dra("LastName_BSN") & ", " &
                        dra("FirstName") & " " & dra("Degree")
                    Else
                        Provider = dra("LastName_BSN") & ", " & dra("FirstName")
                    End If
                End If
                If dra("Address_ID") IsNot DBNull.Value Then Provider += ", " & GetAddress(dra("Address_ID"))
                txtPatProvider.Text = Provider
                PopulateProvider(dra("ID"))
            End While
        End If
        cna.Close()
        cna = Nothing
    End Sub

    Private Function GetAccResults(ByVal AccID As Long) As DataTable
        Dim AccRes As New DataTable
        'ID, Name, Result, Released, Flag, Range, UOM, T_Result, Comment 
        Dim sSQL As String = "Select b.ID, b.Name, a.Result, a.Released, a.Flag, a.NormalRange, b.UOM, a.T_Result, " &
        "a.Comment from Acc_Results a inner join Tests b on b.ID = a.Test_ID where a.Accession_ID = " & AccID
        '
        Dim cnacc As New SqlConnection(connString)
        cnacc.Open()
        Dim cmdacc As New SqlCommand(sSQL, cnacc)
        cmdacc.CommandType = CommandType.Text
        Dim da As New SqlDataAdapter(cmdacc)
        da.Fill(AccRes)
        cnacc.Close()
        cnacc = Nothing
        Return AccRes
    End Function

    Private Sub DisplayAccResults(ByVal AccID As Long)
        dgvAccResults.Rows.Clear()
        Dim Result As String = ""
        Dim Flag As String = ""
        Dim UOM As String = ""
        Dim TRes As String = ""
        Dim RTB As New RichTextBox
        Dim Comment As String = ""
        Dim AccRes As DataTable = GetAccResults(AccID)
        'ID, Name, Result, Released, Flag, Range, UOM, T_Result, Comment 
        For i As Integer = 0 To AccRes.Rows.Count - 1
            If AccRes.Rows(i).Item("Released") IsNot DBNull.Value AndAlso
            CType(AccRes.Rows(i).Item("Released"), Boolean) = True Then
                Result = AccRes.Rows(i).Item("Result")
            ElseIf AccRes.Rows(i).Item("Result") IsNot DBNull.Value _
            AndAlso Trim(AccRes.Rows(i).Item("Result")) <> "" Then
                Result = "Unreleased"
            Else
                Result = "Pending"
            End If
            If AccRes.Rows(i).Item("Flag") IsNot DBNull.Value Then
                Flag = AccRes.Rows(i).Item("Flag")
            Else
                Flag = ""
            End If
            If AccRes.Rows(i).Item("UOM") IsNot DBNull.Value Then
                UOM = AccRes.Rows(i).Item("UOM")
            Else
                UOM = ""
            End If
            If AccRes.Rows(i).Item("T_Result") IsNot DBNull.Value Then
                RTB.Rtf = AccRes.Rows(i).Item("T_Result")
                TRes = RTB.Text
            Else
                TRes = ""
            End If
            If AccRes.Rows(i).Item("Comment") IsNot DBNull.Value Then
                Comment = AccRes.Rows(i).Item("Comment")
            Else
                Comment = ""
            End If
            dgvAccResults.Rows.Add(AccRes.Rows(i).Item("ID").ToString, AccRes.Rows(i).Item("Name").ToString,
            Result, Flag, AccRes.Rows(i).Item("NormalRange"), UOM, TRes, Comment)
            '
            Dim cnref As New SqlConnection(connString)
            cnref.Open()
            Dim cmdref As New SqlCommand("Select a.*, b.Name from Ref_Results a inner join " &
            "Tests b on a.Test_ID = b.ID where a.Accession_ID = " & AccID & " and (a.Reflexer_ID = " &
            AccRes.Rows(i).Item("ID") & " or a.Reflexer_ID in (Select Reflexed_ID from Ref_Results " &
            "where Accession_ID = " & AccID & " and Reflexer_ID = " & AccRes.Rows(i).Item("ID") & "))", cnref)
            cmdref.CommandType = CommandType.Text
            Dim drref As SqlDataReader = cmdref.ExecuteReader
            If drref.HasRows Then
                While drref.Read
                    If drref("Released") IsNot DBNull.Value AndAlso
                    CType(drref("Released"), Boolean) = True Then
                        Result = drref("Result")
                    ElseIf drref("Result") IsNot DBNull.Value _
                    AndAlso Trim(drref("Result")) <> "" Then
                        Result = "Unreleased"
                    Else
                        Result = "Pending"
                    End If
                    If drref("Flag") IsNot DBNull.Value Then
                        Flag = drref("Flag")
                    Else
                        Flag = ""
                    End If
                    If drref("UOM") IsNot DBNull.Value Then
                        UOM = drref("UOM")
                    Else
                        UOM = ""
                    End If
                    If drref("T_Result") IsNot DBNull.Value Then
                        RTB.Rtf = drref("T_Result")
                        TRes = RTB.Text
                    Else
                        TRes = ""
                    End If
                    If drref("Comment") IsNot DBNull.Value Then
                        Comment = drref("Comment")
                    Else
                        Comment = ""
                    End If
                    '
                    dgvAccResults.Rows.Add(drref("Test_ID").ToString, drref("Name").ToString,
                    Result, Flag, drref("NormalRange"), UOM, TRes, Comment)
                End While
            End If

            cnref.Close()
            cnref = Nothing
            '
            Dim cninf As New SqlConnection(connString)
            cninf.Open()
            Dim cmdinf As New SqlCommand("Select a.*, b.Name from Acc_Info_Results " &
            "a inner join Tests b on b.ID = a.Info_ID where a.Accession_ID = " & AccID &
            " and a.Test_ID = " & AccRes.Rows(i).Item("ID"), cninf)
            cmdinf.CommandType = CommandType.Text
            Dim drinf As SqlDataReader = cmdinf.ExecuteReader
            If drinf.HasRows Then
                While drinf.Read
                    If drinf("Released") IsNot DBNull.Value AndAlso
                    CType(drinf("Released"), Boolean) = True Then
                        Result = drinf("Result")
                    ElseIf drinf("Result") IsNot DBNull.Value _
                    AndAlso Trim(drinf("Result")) <> "" Then
                        Result = "Unreleased"
                    Else
                        Result = "Pending"
                    End If
                    If drinf("Flag") IsNot DBNull.Value Then
                        Flag = drinf("Flag")
                    Else
                        Flag = ""
                    End If
                    If drinf("UOM") IsNot DBNull.Value Then
                        UOM = drinf("UOM")
                    Else
                        UOM = ""
                    End If
                    If drinf("T_Result") IsNot DBNull.Value Then
                        RTB.Rtf = drinf("T_Result")
                        TRes = RTB.Text
                    Else
                        TRes = ""
                    End If
                    If drinf("Comment") IsNot DBNull.Value Then
                        Comment = drinf("Comment")
                    Else
                        Comment = ""
                    End If
                    '
                    'dgvAccResults.Rows.Add(drinf("Info_ID").ToString, drref("Name").ToString,
                    'Result, Flag, drref("NormalRange"), UOM, TRes, Comment)

                    dgvAccResults.Rows.Add(drinf("Info_ID").ToString, drinf("Name").ToString,
                   Result, Flag, drinf("NormalRange"), UOM, TRes, Comment)

                End While
            End If
            cninf.Close()
            cninf = Nothing

        Next
        'lblRPTStatus.Text = ""
    End Sub

    Private Function IsQualitative(ByVal TestID As Integer) As Boolean
        Dim Qualitative As Boolean = False
        Dim cnq As New SqlConnection(connString)
        cnq.Open()
        Dim cmdq As New SqlCommand("Select " &
        "Qualitative from Tests where ID = " & TestID, cnq)
        cmdq.CommandType = CommandType.Text
        Dim drq As SqlDataReader = cmdq.ExecuteReader
        If drq.HasRows Then
            While drq.Read
                Qualitative = drq("Qualitative")
            End While
        End If
        cnq.Close()
        cnq = Nothing
        Return Qualitative
    End Function

    Private Function GetAccDate(ByVal AccID As Long) As Date
        Dim AccDate As Date = Now
        Dim cnad As New SqlConnection(connString)
        cnad.Open()
        Dim cmdad As New SqlCommand("Select AccessionDate from Requisitions where ID = " & AccID, cnad)
        cmdad.CommandType = CommandType.Text
        Dim drad As SqlDataReader = cmdad.ExecuteReader
        If drad.HasRows Then
            While drad.Read
                AccDate = drad("AccessionDate")
            End While
        End If
        cnad.Close()
        cnad = Nothing
        Return AccDate
    End Function

    Private Function HasExtendedResult(ByVal BaseID As String, ByVal BType As String) As Boolean
        Dim Extend As Boolean = False
        Dim sSQL As String = ""
        If BType = "ACC" Then
            sSQL = "Select * from Extend_Results where Accession_ID = " & Val(BaseID)
        Else
            sSQL = "Select * from Extend_Results where Accession_ID in (" &
            "Select ID from Requisitions where RequisitionNo = '" & BaseID & "')"
        End If
        '
        Dim cner As New SqlConnection(connString)
        cner.Open()
        Dim cmder As New SqlCommand(sSQL, cner)
        cmder.CommandType = CommandType.Text
        Dim drer As SqlDataReader = cmder.ExecuteReader
        If drer.HasRows Then
            While drer.Read
                If drer("Result") IsNot DBNull.Value Then Extend = True
            End While
        End If
        cner.Close()
        cner = Nothing
        Return Extend
    End Function

    Private Sub cmbAccession_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAccession.Validated
        If cmbAccession.SelectedIndex = -1 Then     'No Patient Record
            lblRPTStatus.Text = ""
            Dim ACCREQ As String = ""
            Dim sSQL As String = ""
            If chkAccReq.Checked = False Then   'Acc
                sSQL = "Select * from Requisitions where Received <> 0 and ID = " & Val(cmbAccession.Text)
                ACCREQ = "ACC"
            Else
                sSQL = "Select * from Requisitions where Received <> 0 and RequisitionNo = '" & Trim(cmbAccession.Text) & "'"
                ACCREQ = "REQ"
            End If
            '
            Dim cnacc As New SqlConnection(connString)
            cnacc.Open()
            Dim cmdacc As New SqlCommand(sSQL, cnacc)
            cmdacc.CommandType = CommandType.Text
            Dim dracc As SqlDataReader = cmdacc.ExecuteReader
            If dracc.HasRows Then
                While dracc.Read
                    cmbAccession.Text = dracc("ID").ToString
                    txtAccID.Text = dracc("ID").ToString
                    txtMeds.Text = GetMedications(dracc("ID").ToString)
                    If dracc("Patient_ID") IsNot DBNull.Value Then DisplayPatient(dracc("Patient_ID"))
                    btnAccErase.Visible = True
                    txtPatProvider.Text = ""
                    lblRPTStatus.Text = ""
                    lblRPTStatus.Text = GetReportStatus(Val(txtAccID.Text))
                    If HasExtendedResult(txtAccID.Text, ACCREQ) Then _
                    lblRPTStatus.Text = lblRPTStatus.Text & "-Extended"
                    DisplayProvider(Val(txtAccID.Text))
                    DisplayAccResults(Val(txtAccID.Text))
                End While
            Else
                btnAccErase.Visible = False
                txtAccDate.Text = ""
                txtPatProvider.Text = ""
                ClearProvider()
                dgvAccResults.Rows.Clear()
                'MsgBox("Either this Accession does not exist or the results have not been released.", MsgBoxStyle.Critical, "Prolis")
                cmbAccession.Text = ""
            End If
            cnacc.Close()
            cnacc = Nothing
        End If
        FaxEmailUpdate()
    End Sub

    Private Sub UpdateRDMEntry()
        If txtProviderID.Text <> "" And
        (chkFax.Checked = True And PhoneNeat(txtFax.Text).Length >= 10) Or
        (chkEmail.Checked = True And ValidateEmail(txtEmail.Text)) Then
            btnSchedule.Enabled = True
        Else
            btnSchedule.Enabled = False
        End If
    End Sub

    Private Sub cmbTest_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTest.SelectedIndexChanged
        If cmbTest.SelectedIndex <> -1 Then
            Dim ItemX As MyList = cmbTest.SelectedItem
            DisplayHistResults(Val(txtPatientID.Text), ItemX.ItemData)
        Else
            dgvHistResults.Rows.Clear()
        End If
        FaxEmailUpdate()
    End Sub

    Private Sub DisplayHistResults(ByVal PatientID As Long, ByVal TestID As Integer)
        dgvHistResults.Rows.Clear()
        Dim Flag() As String = {"", ""}
        Dim Range As String = ""
        Dim UOM As String = GetUOM(TestID)
        Dim Mimg As Image = Nothing
        Dim Comment As String = ""
        Dim sSQL As String = "Select a.*, b.ID as AccID, b.AccessionDate as AccDate from Acc_Results a inner join " &
        "Requisitions b on a.Accession_ID = b.ID  and a.Test_ID = " & TestID & " and b.Patient_ID = " & PatientID
        '
        Dim cnhr As New SqlConnection(connString)
        cnhr.Open()
        Dim cmdhr As New SqlCommand(sSQL, cnhr)
        cmdhr.CommandType = CommandType.Text
        Dim drhr As SqlDataReader = cmdhr.ExecuteReader
        If drhr.HasRows Then
            While drhr.Read
                If drhr("Result") IsNot DBNull.Value AndAlso Trim(drhr("Result")) <> "" Then
                    Flag = GetFlag(Val(cmbAccession.Text), Trim(drhr("Result")), TestID)
                Else
                    Flag(0) = ""
                End If
                If drhr("T_Result") Is DBNull.Value Then
                    Mimg = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\RTFBlank.ico")
                Else
                    Mimg = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\RTF.ico")
                End If
                If drhr("Comment") Is DBNull.Value Then
                    Comment = ""
                Else
                    Comment = drhr("Comment")
                End If
                If Range = "" Then Range = GetNormalRange(drhr("AccID"), TestID)
                dgvHistResults.Rows.Add(drhr("AccID"), Format(drhr("AccDate"),
                SystemConfig.DateFormat), drhr("Result"), Flag(0), Range, UOM, Mimg, Comment)
            End While
        End If
        cnhr.Close()
        cnhr = Nothing
    End Sub

    Private Function GetUOM(ByVal TestID As Integer) As String
        Dim UOM As String = ""
        Dim sSQL As String = "Select * from Tests where ID = " & TestID
        '
        Dim cnu As New SqlConnection(connString)
        cnu.Open()
        Dim cmdu As New SqlCommand(sSQL, cnu)
        cmdu.CommandType = CommandType.Text
        Dim dru As SqlDataReader = cmdu.ExecuteReader
        If dru.HasRows Then
            While dru.Read
                If dru("UOM") IsNot DBNull.Value AndAlso
                Trim(dru("UOM")) <> "" Then UOM = Trim(dru("UOM"))
            End While
        End If
        cnu.Close()
        cnu = Nothing
        Return UOM
    End Function

    'Private Sub txtEmail_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmail.Validated
    '    UpdateRDMEntry()
    '    FaxEmailUpdate()
    'End Sub

    Private Sub btnSchedule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSchedule.Click
        If txtProviderID.Text <> "" And
        (chkFax.Checked = True And PhoneNeat(txtFax.Text).Length >= 10) Or
        (chkEmail.Checked = True And ValidateEmail(txtEmail.Text)) Then
            If Not InReportList(txtProviderID.Text, IIf(chkReport.Text = "ACC",
            cmbAccession.Text, txtPatientID.Text), chkReport.Text,
            chkFax.Checked, txtFax.Text, False, chkEmail.Checked, txtEmail.Text) Then _
            dgvReports.Rows.Add(txtProviderID.Text, IIf(chkReport.Text = "ACC",
            cmbAccession.Text, txtPatientID.Text), chkReport.Text, Date.Now, "CS",
            chkFax.Checked, PhoneNeat(txtFax.Text), False, chkEmail.Checked, Trim(txtEmail.Text))
            'btnSchedule.Enabled = False
        Else
            MsgBox("To schedule a fax, check the fax option followed by entering a valid fax number " &
            "if not there. To schedule an email, check the Email option followed by entering a valid" &
            " email address (for example 'somebody@anydomain.com').", MsgBoxStyle.Critical, "Prolis")
        End If
    End Sub

    Private Function InReportList(ByVal ProviderID As Long, ByVal BaseID As Long, ByVal Rpt As String,
    ByVal Fax As Boolean, ByVal FaxID As String, ByVal Critical As Boolean,
    ByVal Email As Boolean, ByVal EmailID As String) As Boolean
        Dim i As Integer
        Dim InList As Boolean = False
        For i = 0 To dgvReports.RowCount - 1
            If dgvReports.Rows(i).Cells(0).Value = ProviderID And
            dgvReports.Rows(i).Cells(1).Value = BaseID And
            dgvReports.Rows(i).Cells(2).Value = Rpt And
            dgvReports.Rows(i).Cells(5).Value = Fax And
            dgvReports.Rows(i).Cells(6).Value = PhoneNeat(FaxID) And
            dgvReports.Rows(i).Cells(7).Value = Critical And
            dgvReports.Rows(i).Cells(8).Value = Email And
            dgvReports.Rows(i).Cells(9).Value = EmailID Then
                InList = True
                Exit For
            End If
        Next
        InReportList = InList
    End Function

    Private Sub dgvReports_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvReports.CellClick
        If e.RowIndex <> -1 Then btnRem.Enabled = True
    End Sub

    Private Sub btnRem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRem.Click
        If dgvReports.SelectedRows(0).Index <> -1 Then _
        dgvReports.Rows.RemoveAt(dgvReports.SelectedRows(0).Index)
        btnRem.Enabled = False
    End Sub

    Private Sub FaxEmailUpdate()
        If ((chkReport.Checked = False And cmbAccession.Text <> "") Or
        (chkReport.Checked = True And txtPatientID.Text <> "")) Then
            If chkFax.Checked = True And PhoneNeat(txtFax.Text).Length >= 10 Then
                btnFax.Enabled = True
            Else
                btnFax.Enabled = False
            End If
            If chkEmail.Checked = True And ValidateEmail(txtEmail.Text) Then
                btnEmail.Enabled = True
            Else
                btnEmail.Enabled = False
            End If
        Else
            btnFax.Enabled = False
            btnEmail.Enabled = False
        End If
    End Sub

    Private Sub btnFax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFax.Click
        If txtProviderID.Text <> "" And chkFax.Checked = True And PhoneNeat(txtFax.Text).Length >= 10 Then
            If Not InReportList(txtProviderID.Text, IIf(chkReport.Text = "ACC",
            cmbAccession.Text, txtPatientID.Text), chkReport.Text,
            chkFax.Checked, PhoneNeat(txtFax.Text), True, chkEmail.Checked, txtEmail.Text) Then _
            dgvReports.Rows.Add(txtProviderID.Text, IIf(chkReport.Text = "ACC",
            cmbAccession.Text, txtPatientID.Text), chkReport.Text, Date.Now, "CS",
            chkFax.Checked, PhoneNeat(txtFax.Text), True, chkEmail.Checked, Trim(txtEmail.Text))
        End If
    End Sub
    Private Function PhoneNeat(ByVal Phone As String) As String
        Phone = Replace(Phone, "(", "")
        Phone = Replace(Phone, ")", "")
        Phone = Replace(Phone, "-", "")
        Phone = Replace(Phone, "_", "")
        Phone = Replace(Phone, ".", "")
        Phone = Replace(Phone, "/", "")
        Phone = Replace(Phone, "\", "")
        Phone = Replace(Phone, "*", "")
        Phone = Replace(Phone, " ", "")
        Return Phone
    End Function

    Private Sub btnEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmail.Click
        Dim BaseID As Long
        Dim Rpt As String = ""
        Dim Email As String = Trim(txtEmail.Text)
        If chkReport.Checked = False And cmbAccession.Text <> "" Then
            BaseID = cmbAccession.Text
            Rpt = "ACC"
        ElseIf chkReport.Checked = True And txtPatientID.Text <> "" Then
            BaseID = txtPatientID.Text
            Rpt = "HIST"
        End If
        If chkEmail.Checked = True And Email <> "" Then
            EmailReport(Rpt, BaseID, Email)
            chkEmail.Checked = False
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub dgvAccResults_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAccResults.CellClick
        If e.RowIndex <> -1 And e.ColumnIndex = 6 Then
            If dgvAccResults.Rows(e.RowIndex).Cells(6).Value Is
            System.Drawing.Image.FromFile(Application.StartupPath & "\Images\RTF.ico") Then
                frmViewExtRes.ShowDialog()

            End If
        End If
    End Sub

    'TODO: Implement the function to view extended results
    'Private Function InstantiateReport(ByVal AccID As Long) As CrystalDecisions.CrystalReports.Engine.ReportDocument
    '    Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
    '    oRpt.Load(ValidateReportFile(AccID.ToString, False))
    '    ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)
    '    'Dim crDiscreteValue As ParameterDiscreteValue
    '    'Dim crRangeValue As ParameterRangeValue
    '    'Dim crFields As ParameterFields
    '    'Dim crField As ParameterField
    '    'Dim crValues As ParameterValues
    '    'crFieldDefs = CRS.ReportDocument.DataDefinition.ParameterFields
    '    'CRS.ReportDocument.RecordSelectionFormula = "{Patients.ID} = " & PatientID
    '    '
    '    'crField = oRpt.ParameterFields.Item("AccID")
    '    'crValues = crField.CurrentValues
    '    'crDiscreteValue = New ParameterDiscreteValue
    '    'crDiscreteValue.Value = AccID
    '    'crValues.Add(crDiscreteValue)
    '    'crField.CurrentValues.Add(crDiscreteValue)
    '    oRpt.SetParameterValue("AccID", AccID)
    '    Return oRpt
    'End Function

    Private Sub PrintReport(ByVal RPTFile As String, ByVal AccID As String)
        'TODO: Implement the function to print the report
        'Dim SPDFS As New List(Of Byte())
        'Dim FPDF As Byte() = Nothing
        'Dim ExtCount As Integer = 0
        'If AccID <> "" Then
        '    Dim FolderPath As String = My.Application.Info.DirectoryPath & "\Temp\"
        '    If Not Directory.Exists(FolderPath) Then
        '        Directory.CreateDirectory(FolderPath)
        '        Dim UserAccount As String = "everyone" 'Specify the user here
        '        Dim FolderInfo As DirectoryInfo = New DirectoryInfo(FolderPath)
        '        Dim FolderAcl As New DirectorySecurity
        '        FolderAcl.AddAccessRule(New FileSystemAccessRule(UserAccount,
        '        FileSystemRights.Modify, InheritanceFlags.ContainerInherit Or
        '        InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow))
        '        FolderAcl.SetAccessRuleProtection(True, False) 'uncomment to remove existing permissions
        '        FolderInfo.SetAccessControl(FolderAcl)
        '    End If
        '    Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        '    oRpt.Load(RPTFile)
        '    ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database,
        '    My.Settings.UID, My.Settings.PWD)
        '    oRpt.RecordSelectionFormula = "{Requisitions.ID} = " & AccID
        '    Dim S As MemoryStream = oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat)
        '    SPDFS.Add(S.ToArray)
        '    Dim sSQL As String = "Select Result from Extend_Results where Accession_ID = " & AccID
        '    If connString <> "" Then
        '        Dim cnrpt As New SqlConnection(connString)
        '        cnrpt.Open()
        '        Dim cmdrpt As New SqlCommand(sSQL, cnrpt)
        '        cmdrpt.CommandType = CommandType.Text
        '        Dim drrpt As SqlDataReader = cmdrpt.ExecuteReader
        '        If drrpt.HasRows Then
        '            While drrpt.Read
        '                SPDFS.Add(drrpt("Result"))
        '                ExtCount += 1
        '            End While
        '        End If
        '        cnrpt.Close()
        '        cnrpt = Nothing
        '    Else
        '        Dim cnrpt As New Odbc.OdbcConnection(connstring)
        '        cnrpt.Open()
        '        Dim cmdrpt As New Odbc.OdbcCommand(sSQL, cnrpt)
        '        cmdrpt.CommandType = CommandType.Text
        '        Dim drrpt As Odbc.OdbcDataReader = cmdrpt.ExecuteReader
        '        If drrpt.HasRows Then
        '            While drrpt.Read
        '                SPDFS.Add(drrpt("Result"))
        '                ExtCount += 1
        '            End While
        '        End If
        '        cnrpt.Close()
        '        cnrpt = Nothing
        '    End If
        '    '
        '    oRpt.Close()
        '    oRpt.Dispose()
        '    '
        '    FPDF = PdfHelper.MergePDFStreams(SPDFS)
        '    For Each FlToDel As String In Directory.GetFiles(FolderPath, "*.*", SearchOption.TopDirectoryOnly)
        '        File.Delete(FlToDel)
        '    Next
        '    '
        '    Dim ms As New FileStream(FolderPath & AccID & ".PDF", FileMode.Create, FileAccess.ReadWrite, FileShare.Read)
        '    ms.Write(FPDF, 0, FPDF.Length)
        '    ms.Close()
        '    ms = Nothing
        '    'Printing
        '    'Dim PDFDOC As New Spire.Pdf.PdfDocument
        '    'PDFDOC.LoadFromFile(FolderPath & AccID & ".PDF")
        '    'PDFDOC.PrinterName = GetDefaultPrinter()
        '    'PDFDOC.PrintDocument.Print()
        '    'View and print

        '    'If PDFRV_foxit.IsHandleCreated Then PDFRV_foxit.Close()
        '    'PDFRV_foxit.PdfViewer1.Refresh()
        '    'PDFRV_foxit.PdfViewer1.Open(New Foxit.PDF.Viewer.PdfDocument(FPDF)
        '    '   )
        '    'Wait(3 + (SPDFS.Count \ 4) + (ExtCount * 2))


        '    ''If PDFRV.IsHandleCreated Then PDFRV.Close()
        '    ''PDFRV.AxAcroPDF1.src = FolderPath & AccID & ".PDF"
        '    ''Wait(3 + (SPDFS.Count \ 4) + (ExtCount * 2))
        '    ''PDFRV.AxAcroPDF1.Refresh()
        '    ''PDFRV.Show()

        '    'PDFRV_foxit.FilePath = FolderPath & AccID & ".PDF"

        '    'PDFRV_foxit.Show()
        'End If
        ''
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If cmbAccession.Text <> "" Then
            'TODO: Implement the function to print the report

            ''Dim ProvChoice() As String = GetProviderChoice(cmbAccession.Text)
            'Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            ''Accession
            'If chkAccHist.Checked = False Then 'Accession
            '    'Dim RPTFile As String = ValidateReportFile(cmbAccession.Text, False)
            '    'oRpt.Load(ValidateReportFile(SystemConfig.GenericResults))
            '    'ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)
            '    'oRpt.RecordSelectionFormula = "{Requisitions.ID} = " & Val(cmbAccession.Text)
            '    ''oRpt = InstantiateReport(Val(cmbAccession.Text))
            '    'frmRV.CRRV.ReportSource = oRpt
            '    'frmRV.Show()
            '    'frmRV.MdiParent = ProlisQC
            '    'UpdateReportTime(Val(cmbAccession.Text), False)
            '    LogEvent(cmbAccession.Text, 10, GetOrdProvIDFromAccID(Val(cmbAccession.Text)),
            '    IIf(ReportFullResulted(Val(cmbAccession.Text)) = True, "FINAL", "PARTIAL"),
            '    False, "Result Reports", "")
            '    If SystemConfig.AuditTrail = True Then _
            '    LogUserEvent(ThisUser.ID, 10, Date.Now, "Report", cmbAccession.Text, "", "")
            '    'PrintReport(RPTFile, cmbAccession.Text)

            '    GenerateReports({cmbAccession.Text}, 1, False)
            'Else
            '    If txtPatientID.Text <> "" Then
            '        oRpt.Load(GetReportPath("History_Results.rpt"))
            '        ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)
            '        If cmbTest.SelectedIndex = -1 Then
            '            oRpt.RecordSelectionFormula = "{Requisitions.Patient_ID} = " & Val(txtPatientID.Text)
            '        Else
            '            Dim ItemX As MyList = cmbTest.SelectedItem
            '            oRpt.RecordSelectionFormula = "{Requisitions.Patient_ID} = " & Val(txtPatientID.Text) _
            '            & " and {Tests.ID} = " & ItemX.ItemData
            '        End If
            '        frmRV.CRRV.ReportSource = oRpt
            '        frmRV.Show()
            '        frmRV.MdiParent = ProlisQC
            '    End If
            'End If
        End If

    End Sub

    Private Function GetProviderChoice(ByVal AccID As Long) As String()
        Dim Choice() As String = {"", "", "", "", "", "", "", "", "", ""}
        ''0=ProviderID, 1=Complete, 2=Print, 3=Prolison, 4=Interface, 5=RPTFax
        '6=Fax#, 7=RPTEmail, 8=Email, 9=RPTFile
        Dim sSQL As String = "Select * from Providers where ID in (Select " &
        "OrderingProvider_ID from Requisitions where ID = " & AccID & ")"
        If connString <> "" Then
            Dim cnpc As New SqlConnection(connString)
            cnpc.Open()
            Dim cmdpc As New SqlCommand(sSQL, cnpc)
            cmdpc.CommandType = CommandType.Text
            Dim drpc As SqlDataReader = cmdpc.ExecuteReader
            If drpc.HasRows Then
                While drpc.Read
                    Choice(0) = drpc("ID").ToString
                    If drpc("RptComplete").Value IsNot DBNull.Value Then
                        If drpc("RptComplete") = False Then
                            Choice(1) = "Partial"
                        Else
                            Choice(1) = "Final"
                        End If
                    Else
                        Choice(1) = "Partial"
                    End If
                    If drpc("RDM_Print") IsNot DBNull.Value Then
                        Choice(2) = drpc("RDM_Print").ToString
                    Else
                        Choice(2) = "False"
                    End If
                    If drpc("RDM_Prolison") IsNot DBNull.Value Then
                        Choice(3) = drpc("RDM_Prolison").ToString
                    Else
                        Choice(3) = "False"
                    End If
                    If drpc("RDM_Interface") IsNot DBNull.Value Then
                        Choice(4) = drpc("RDM_Interface").ToString
                    Else
                        Choice(4) = "False"
                    End If
                    If drpc("RDM_Fax") IsNot DBNull.Value Then
                        Choice(5) = drpc("RDM_Fax").ToString
                    Else
                        Choice(5) = "False"
                    End If
                    If drpc("Fax") Is DBNull.Value Then
                        Choice(6) = ""
                    Else
                        Choice(6) = drpc("Fax")
                    End If
                    If drpc("RDM_Email") IsNot DBNull.Value Then
                        Choice(7) = drpc("RDM_Email").ToString
                    Else
                        Choice(7) = "False"
                    End If
                    If drpc("Email") IsNot DBNull.Value Then
                        Choice(8) = drpc("Email").ToString
                    Else
                        Choice(8) = ""
                    End If
                    If drpc("ResRPTFile") IsNot DBNull.Value Then
                        Choice(9) = Trim(drpc("ResRPTFile"))
                    Else
                        Choice(9) = ""
                    End If
                End While
            End If
            cnpc.Close()
            cnpc = Nothing
        Else
            'Dim cnpc As New Odbc.OdbcConnection(connstring)
            'cnpc.Open()
            'Dim cmdpc As New Odbc.OdbcCommand(sSQL, cnpc)
            'cmdpc.CommandType = CommandType.Text
            'Dim drpc As Odbc.OdbcDataReader = cmdpc.ExecuteReader
            'If drpc.HasRows Then
            '    While drpc.Read
            '        Choice(0) = drpc("ID").ToString
            '        If drpc("RptComplete").Value IsNot DBNull.Value Then
            '            If drpc("RptComplete") = False Then
            '                Choice(1) = "Partial"
            '            Else
            '                Choice(1) = "Final"
            '            End If
            '        Else
            '            Choice(1) = "Partial"
            '        End If
            '        If drpc("RDM_Print") IsNot DBNull.Value Then
            '            Choice(2) = drpc("RDM_Print").ToString
            '        Else
            '            Choice(2) = "False"
            '        End If
            '        If drpc("RDM_Prolison") IsNot DBNull.Value Then
            '            Choice(3) = drpc("RDM_Prolison").ToString
            '        Else
            '            Choice(3) = "False"
            '        End If
            '        If drpc("RDM_Interface") IsNot DBNull.Value Then
            '            Choice(4) = drpc("RDM_Interface").ToString
            '        Else
            '            Choice(4) = "False"
            '        End If
            '        If drpc("RDM_Fax") IsNot DBNull.Value Then
            '            Choice(5) = drpc("RDM_Fax").ToString
            '        Else
            '            Choice(5) = "False"
            '        End If
            '        If drpc("Fax") Is DBNull.Value Then
            '            Choice(6) = ""
            '        Else
            '            Choice(6) = drpc("Fax")
            '        End If
            '        If drpc("RDM_Email") IsNot DBNull.Value Then
            '            Choice(7) = drpc("RDM_Email").ToString
            '        Else
            '            Choice(7) = "False"
            '        End If
            '        If drpc("Email") IsNot DBNull.Value Then
            '            Choice(8) = drpc("Email").ToString
            '        Else
            '            Choice(8) = ""
            '        End If
            '        If drpc("ResRPTFile") IsNot DBNull.Value Then
            '            Choice(9) = Trim(drpc("ResRPTFile"))
            '        Else
            '            Choice(9) = ""
            '        End If
            '    End While
            'End If
            'cnpc.Close()
            'cnpc = Nothing
        End If
        Return Choice
    End Function

    Private Sub txtProviderID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProviderID.Validated
        If txtProviderID.Text <> "" Then
            If PopulateProvider(Val(txtProviderID.Text)) = False Then
                MsgBox("Invalid Provider ID", MsgBoxStyle.Critical, "Prolis")
                txtProviderID.Focus()
            Else
                chkFax.Focus()
            End If
        Else
            ClearProvider()
        End If
        FaxEmailUpdate()
    End Sub

    Private Function PopulateProvider(ByVal ProvID As Long) As Boolean
        ClearProvider()
        Dim sSQL As String = "Select * from Providers where ID = " & ProvID
        If connString <> "" Then
            Dim cnpp As New SqlConnection(connString)
            cnpp.Open()
            Dim cmdpp As New SqlCommand(sSQL, cnpp)
            cmdpp.CommandType = CommandType.Text
            Dim drpp As SqlDataReader = cmdpp.ExecuteReader
            If drpp.HasRows Then
                While drpp.Read
                    txtProviderID.Text = ProvID.ToString
                    If drpp("IsIndividual") = True Then
                        If drpp("Degree") IsNot DBNull.Value AndAlso Trim(drpp("Degree")) <> "" Then
                            txtProviderName.Text = drpp("LastName_BSN") & ", " & drpp("LastName_BSN")
                        Else
                            txtProviderName.Text = drpp("LastName_BSN") & ", " & drpp("LastName_BSN") & " " & Trim(drpp("Degree"))
                        End If
                    Else
                        txtProviderName.Text = drpp("LastName_BSN")
                    End If
                    chkReport.Checked = False
                    If drpp("Fax") IsNot DBNull.Value AndAlso
                    Trim(drpp("Fax")) <> "" Then txtFax.Text = Trim(drpp("Fax"))
                    If drpp("Email") Is DBNull.Value AndAlso
                    Trim(drpp("Email")) <> "" Then txtEmail.Text = Trim(drpp("Email"))
                End While
                Return True
            Else
                Return False
            End If
            cnpp.Close()
            cnpp = Nothing
        Else
            'Dim cnpp As New Odbc.OdbcConnection(connstring)
            'cnpp.Open()
            'Dim cmdpp As New Odbc.OdbcCommand(sSQL, cnpp)
            'cmdpp.CommandType = CommandType.Text
            'Dim drpp As Odbc.OdbcDataReader = cmdpp.ExecuteReader
            'If drpp.HasRows Then
            '    While drpp.Read
            '        txtProviderID.Text = ProvID.ToString
            '        If drpp("IsIndividual") = True Then
            '            If drpp("Degree") IsNot DBNull.Value AndAlso Trim(drpp("Degree")) <> "" Then
            '                txtProviderName.Text = drpp("LastName_BSN") & ", " & drpp("LastName_BSN")
            '            Else
            '                txtProviderName.Text = drpp("LastName_BSN") & ", " & drpp("LastName_BSN") & " " & Trim(drpp("Degree"))
            '            End If
            '        Else
            '            txtProviderName.Text = drpp("LastName_BSN")
            '        End If
            '        chkReport.Checked = False
            '        If drpp("Fax") IsNot DBNull.Value AndAlso
            '        Trim(drpp("Fax")) <> "" Then txtFax.Text = Trim(drpp("Fax"))
            '        If drpp("Email") Is DBNull.Value AndAlso
            '        Trim(drpp("Email")) <> "" Then txtEmail.Text = Trim(drpp("Email"))
            '    End While
            '    Return True
            'Else
            '    Return False
            'End If
            'cnpp.Close()
            'cnpp = Nothing
        End If
    End Function

    Private Sub ClearProvider()
        txtProviderID.Text = ""
        txtProviderName.Text = ""
        chkReport.Checked = False
        chkFax.Checked = False
        chkEmail.Checked = False
        txtFax.Text = ""
        txtEmail.Text = ""
    End Sub

    Private Sub btnProvLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProvLook.Click
        Dim ProvID As String = frmProviderLookup.ShowDialog
        If ProvID <> "" Then
            PopulateProvider(Val(ProvID))
        End If
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = vbCr Then
            e.KeyChar = Chr(0)
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub dgvReports_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvReports.DataError
        On Error Resume Next
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim i As Integer
        For i = 0 To dgvAccResults.RowCount - 1
            If Not dgvAccResults.Rows(i).Cells(3).Value Is System.DBNull.Value AndAlso
            InStr(dgvAccResults.Rows(i).Cells(3).Value, "P") > 0 Then
                If dgvAccResults.Rows(i).Cells(3).Style.ForeColor = Color.Red Then
                    dgvAccResults.Rows(i).Cells(3).Style.ForeColor = Color.White
                Else
                    dgvAccResults.Rows(i).Cells(3).Style.ForeColor = Color.Red
                End If
            End If
        Next
    End Sub

    Private Sub txtAccDate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccDate.GotFocus
        txtAccDate.BackColor = FCOLOR
    End Sub

    Private Sub txtAccDate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAccDate.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtFax_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFax.Validated
        If PhoneNeat(txtFax.Text).Length < 10 Then
            chkFax.Checked = False
        Else
            UpdateRDMEntry()
            FaxEmailUpdate()
        End If
    End Sub

    Private Sub frmResInq_ResizeBegin(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeBegin
        origWidth = Me.Width
        origHeight = Me.Height
    End Sub

    Private Sub frmResInq_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        MeResize(Me, origWidth, origHeight)
    End Sub

    Private Sub txtSlideID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSlideID.GotFocus
        txtSlideID.BackColor = FCOLOR
    End Sub

    Private Sub txtSlideID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSlideID.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtSlideID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSlideID.LostFocus
        txtSlideID.BackColor = NFCOLOR
    End Sub

    Private Sub txtSlideID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSlideID.TextChanged
        If txtSlideID.Text <> "" Then
            cmbAccession.SelectedIndex = -1
            cmbAccession.Text = ""
        End If
    End Sub

    Private Sub txtSlideID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSlideID.Validated
        If txtSlideID.Text <> "" Then
            Dim AccID As String = GetAccIDWithSlideID(Trim(txtSlideID.Text))
            If AccID <> "" Then
                cmbAccession.Text = AccID
                cmbAccession_Validated(Nothing, Nothing)
                'btnAccErase.Visible = True
                'DisplayProvider(Val(AccID))
                'DisplayAccResults(Val(AccID))
                'btnPrint.Enabled = True
                'If SystemConfig.AuditTrail = True Then _
                'LogUserEvent(ThisUser.ID, 71, Date.Now, "Accession", AccID, "", "")
            Else
                MsgBox("Invalid Source", MsgBoxStyle.Critical, "Prolis")
                txtSlideID.Text = ""
            End If
        End If
    End Sub

    Private Function GetAccIDWithSlideID(ByVal SlideID As String) As String
        Dim AccID As String = ""
        Dim sSQL As String = "Select Accession_ID from Req_Slide where SlideID = '" & SlideID & "'"
        If connString <> "" Then
            Dim cnsa As New SqlConnection(connString)
            cnsa.Open()
            Dim cmdsa As New SqlCommand(sSQL, cnsa)
            cmdsa.CommandType = CommandType.Text
            Dim drsa As SqlDataReader = cmdsa.ExecuteReader
            If drsa.HasRows Then
                While drsa.Read
                    AccID = drsa("Accession_ID").ToString
                End While
            End If
            cnsa.Close()
            cnsa = Nothing
        Else
            'Dim cnsa As New Odbc.OdbcConnection(connstring)
            'cnsa.Open()
            'Dim cmdsa As New Odbc.OdbcCommand(sSQL, cnsa)
            'cmdsa.CommandType = CommandType.Text
            'Dim drsa As Odbc.OdbcDataReader = cmdsa.ExecuteReader
            'If drsa.HasRows Then
            '    While drsa.Read
            '        AccID = drsa("Accession_ID").ToString
            '    End While
            'End If
            'cnsa.Close()
            'cnsa = Nothing
        End If
        Return AccID
    End Function

    Private Sub txtAccDate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccDate.LostFocus
        txtAccDate.BackColor = NFCOLOR
    End Sub

    Private Sub chkAccReq_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAccReq.CheckedChanged
        If chkAccReq.Checked = False Then   'Accession
            chkAccReq.Text = "Acc ID"
        Else
            chkAccReq.Text = "Req ID"
        End If
        cmbAccession.SelectedIndex = -1
        cmbAccession.Text = ""
        btnPatErase_Click(Nothing, Nothing)
        ClearProvider()
    End Sub
End Class
