Imports System.Drawing.Printing
Imports System.Net.Mail
Imports System.IO
Imports System.Net
Imports Microsoft.Data.SqlClient

Public Class frmSystemConfig

    Private initialValues As New Dictionary(Of Control, Object)
    Private Sub txtTax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTax.KeyPress
        Prices(txtTax, e)
    End Sub

    Private Sub chkCustomRpt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCustomRpt.CheckedChanged
        If chkCustomRpt.Checked = False Then
            chkCustomRpt.Text = "Generic"
        Else
            chkCustomRpt.Text = "Custom"
        End If
    End Sub

    Private Sub chkBarcode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBarcode.CheckedChanged
        If chkBarcode.Checked = False Then
            chkBarcode.Text = "Disable"
            cmbLabelPrinter.SelectedIndex = -1
            cmbLabelPrinter.Enabled = False
            txtAddLabels.Enabled = False
            ChkRemotePrint.Enabled = False
        Else
            chkBarcode.Text = "Enable"
            ChkRemotePrint.Enabled = True
            cmbLabelPrinter.Enabled = True
            CMBRemotPrintPC.Enabled = True
            txtAddLabels.Enabled = True
            txtAddLabels.Text = "0"
        End If
    End Sub

    Private Sub cmbLabelPrinter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLabelPrinter.SelectedIndexChanged
        If cmbLabelPrinter.SelectedIndex = -1 Then
            chkBarcode.Checked = False
        Else
            chkBarcode.Checked = True
            If cmbLabelPrinter.SelectedItem.Contains("Remote") Then
                CMBRemotPrintPC.Enabled = True
                ChkRemotePrint.Enabled = True
                PopulateRemotePrintPCs()
            Else
                CMBRemotPrintPC.SelectedIndex = -1
                CMBRemotPrintPC.Enabled = False
                ChkRemotePrint.Enabled = False
            End If
        End If
    End Sub
    Private Sub txtTax_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTax.Validated
        If txtTax.Text = "" Then txtTax.Text = "0.00"
    End Sub

    Private Sub txtDueDays_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDueDays.Validated
        If txtDueDays.Text = "" Then txtDueDays.Text = "15"
    End Sub

    Private Sub txtTerm_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTerm.Validated
        If txtTerm.Text = "" Then txtTerm.Text = "Due Upon Receipt"
    End Sub

    Private Sub chkSMCA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSMCA.CheckedChanged
        If chkSMCA.Checked = True Then
            chkSMCA.Text = "Yes"
        Else
            chkSMCA.Text = "No"
        End If
    End Sub

    Private Sub frmSystemConfig_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        If HasChanges(Me.Controls) Then
            Dim q As DialogResult = MessageBox.Show("You have unsaved changes. Do you want to save them?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
            If q = DialogResult.Cancel Then
                e.Cancel = True ' Cancel the closing event
            ElseIf q = DialogResult.Yes Then
                SaveChanges() ' Call your save method
            End If
        End If

        'UseWaitCursor = True

        'Dim SpecificPrinter = cmbuserPrinter.Items(cmbuserPrinter.SelectedIndex).ToString()
        'ThisUser.SpecificPrinter = SpecificPrinter
        'If Not cmbuserPrinter.SelectedText = "Default" Then

        '    Dim sd = "if exists (select * from Users where UserName = '" & ThisUser.UserName.Trim() & "') update users set SpecificPrinter ='" & SpecificPrinter & "' where UserName = '" & ThisUser.UserName.Trim() & "'"
        '    ExecuteSqlProcedure(sd)
        'End If
    End Sub

    Private Sub SaveChanges()
        Me.Cursor = Cursors.WaitCursor

        wait.Visible = True
        SaveConfiguration()
        SaveBarcode()
        InitializeConfiguration(MyLab.ID)
        wait.Visible = False
        ThisUser.UseRemotePrinter = ChkRemotePrint.Checked
        Dim result = 0
        If ThisUser.UseRemotePrinter Then
            result = 1
            Dim sd = "if exists (select * from Users where UserName = '" & ThisUser.UserName.Trim() & "') update users set UseRemotePrinter =" & result & " where UserName = '" & ThisUser.UserName.Trim() & "'"
            ExecuteSqlProcedure(sd)
        End If

        ' Store Fresh values of controls
        StoreInitialValues(Me.Controls)
        btnSave.Enabled = False
        btnCancel.Enabled = False

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub frmSystemConfig_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CN.Open("DSN=frmDashboard")

        PopulateSources()
        PopulatePrinters()
        LoadConfiguration()
        'If ThisUser.SpecificPrinter Is DBNull.Value Or ThisUser.SpecificPrinter = "" Then
        '    ThisUser.SpecificPrinter = "Default"
        '    cmbuserPrinter.SelectedIndex = 0
        'Else

        '    cmbuserPrinter.SelectedIndex = cmbuserPrinter.Items.IndexOf(ThisUser.SpecificPrinter)
        'End If

        'LoadPortalConfig()
        LoadBarcode()
        chkAuditTrail.Checked = LIC.AuditTrail
        chkAuditTrail.Enabled = False
        gbBilling.Enabled = LIC.Bill    'attach billing to license
        ChkRemotePrint.Checked = ThisUser.UseRemotePrinter
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)

        ' Store initial values of controls
        StoreInitialValues(Me.Controls)
        AddHandlers(Me.Controls)

    End Sub

    ' Recursively store initial values of controls
    Private Sub StoreInitialValues(controls As Control.ControlCollection)
        For Each ctrl As Control In controls
            If TypeOf ctrl Is TextBox OrElse TypeOf ctrl Is ComboBox OrElse TypeOf ctrl Is CheckBox OrElse TypeOf ctrl Is RadioButton Then
                initialValues(ctrl) = GetControlValue(ctrl)
            End If
            ' Recursively handle container controls like panels, group boxes, etc.
            If ctrl.HasChildren Then
                StoreInitialValues(ctrl.Controls)
            End If
        Next
    End Sub

    ' Get control value based on control type
    Private Function GetControlValue(ctrl As Control) As Object
        If TypeOf ctrl Is TextBox Then
            Return DirectCast(ctrl, TextBox).Text
        ElseIf TypeOf ctrl Is ComboBox Then
            Return DirectCast(ctrl, ComboBox).SelectedItem
        ElseIf TypeOf ctrl Is CheckBox Then
            Return DirectCast(ctrl, CheckBox).Checked
        ElseIf TypeOf ctrl Is RadioButton Then
            Return DirectCast(ctrl, RadioButton).Checked
        End If
        Return Nothing
    End Function

    ' Recursively check for changes in controls
    Private Function HasChanges(controls As Control.ControlCollection) As Boolean
        For Each ctrl As Control In controls
            If TypeOf ctrl Is TextBox OrElse TypeOf ctrl Is ComboBox OrElse TypeOf ctrl Is CheckBox OrElse TypeOf ctrl Is RadioButton Then
                If initialValues.ContainsKey(ctrl) AndAlso Not Object.Equals(initialValues(ctrl), GetControlValue(ctrl)) Then
                    Return True
                End If
            End If
            ' Recursively check container controls
            If ctrl.HasChildren Then
                If HasChanges(ctrl.Controls) Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function
    Private Sub PopulateSources()
        Dim sSQL As String = "Select * from Sources"
        Dim cnps As New SqlConnection(connString)
        cnps.Open()
        Dim cmdps As New SqlCommand(sSQL, cnps)
        cmdps.CommandType = Data.CommandType.Text
        Dim drps As SqlDataReader = cmdps.ExecuteReader
        If drps.HasRows Then
            While drps.Read
                cmbSource.Items.Add(New MyList(drps("Name"), drps("ID")))
            End While
        End If
        cnps.Close()
        cnps = Nothing
    End Sub


    Private Sub PopulateRemotePrintPCs()
        Try
            Dim sSQL As String = "Select * from RemotePrintPCs"
            Dim cnps As New SqlConnection(connString)
            cnps.Open()
            Dim cmdps As New SqlCommand(sSQL, cnps)
            cmdps.CommandType = Data.CommandType.Text
            Dim drps As SqlDataReader = cmdps.ExecuteReader
            If drps.HasRows Then
                While drps.Read
                    CMBRemotPrintPC.Items.Add(New MyList(drps("Name"), drps("ID")))
                End While
            End If
            cnps.Close()
            cnps = Nothing
            For i = 0 To CMBRemotPrintPC.Items.Count - 1
                If CMBRemotPrintPC.Items(i).ToString() = ThisUser.PrinterPC Then
                    CMBRemotPrintPC.SelectedIndex = i
                End If
            Next
        Catch ex As Exception

        End Try

    End Sub

    Private Sub PopulatePrinters()
        cmbLabelPrinter.Items.Clear()
        'cmbuserPrinter.Items.Clear()
        'cmbuserPrinter.Items.Add("Default")

        Dim InstalledPrinter As String


        For Each InstalledPrinter In PrinterSettings.InstalledPrinters
            cmbLabelPrinter.Items.Add(InstalledPrinter)
            'cmbuserPrinter.Items.Add(InstalledPrinter)
        Next
        cmbLabelPrinter.Items.Add("Prolis Remote Print (DYMO)")
        cmbLabelPrinter.Items.Add("Brady")
    End Sub
    Private Sub LoadPortalConfig()
        'Dim sSQL As String = "Select * from System_Config where Company_ID = " & MyLab.ID
        'Dim cnls As New SqlConnection(connString)
        'cnls.Open()
        'Dim cmdls As New SqlCommand(sSQL, cnls)
        'cmdls.CommandType = Data.CommandType.Text
        'Dim drls As SqlDataReader = cmdls.ExecuteReader
        'If drls.HasRows Then

        'End If
    End Sub
    Private Sub LoadConfiguration()
        Dim i As Integer
        Dim sSQL As String = "Select * from System_Config where Company_ID = " & MyLab.ID
        Dim cnls As New SqlConnection(connString)
        cnls.Open()
        Dim cmdls As New SqlCommand(sSQL, cnls)
        cmdls.CommandType = Data.CommandType.Text
        Dim drls As SqlDataReader = cmdls.ExecuteReader
        If drls.HasRows Then
            While drls.Read
                Dim accd = False
                Try
                    accd = IIf(drls("Portal_AccDate_required") Is DBNull.Value, False, drls("Portal_AccDate_required"))
                Catch ex As Exception
                    accd = False
                End Try
                Try
                    QRChecked.Checked = IIf(drls("QR_Checked") Is DBNull.Value, False, drls("QR_Checked"))
                Catch ex As Exception
                    QRChecked.Checked = False
                    QRChecked.Enabled = False
                End Try

                Try
                    ReportableFormat.Checked = IIf(drls("ReportableFormat") Is DBNull.Value, False, drls("ReportableFormat"))
                Catch ex As Exception
                    ReportableFormat.Checked = False
                    ReportableFormat.Enabled = False
                End Try

                Try
                    MergeSameDayAccession.Checked = IIf(drls("MergeSameDayAccession") Is DBNull.Value, False, drls("MergeSameDayAccession"))
                    ToolTip1.Show("YES: Merge accession of same patient into one if on same day.", MergeSameDayAccession)

                Catch ex As Exception
                    MergeSameDayAccession.Checked = False
                    MergeSameDayAccession.Enabled = False
                    ToolTip1.Show("MergeSameDayAccession column does not exists, Please update the database.", MergeSameDayAccession)
                End Try

                Try
                    P_inputOnLabel.Checked = IIf(drls("P_inputOnLabel") Is DBNull.Value, False, drls("P_inputOnLabel"))
                    ToolTip1.Show("YES: Show extra input P n Label printing window ", P_inputOnLabel)

                Catch ex As Exception
                    P_inputOnLabel.Checked = False
                    P_inputOnLabel.Enabled = False
                    ToolTip1.Show("P_inputOnLabel column does not exists, Please update the database.", P_inputOnLabel)
                End Try
                Try
                    useColorApiChk.Checked = IIf(drls("UseColorApi") Is DBNull.Value, False, drls("UseColorApi"))
                Catch ex As Exception
                    useColorApiChk.Checked = False
                    useColorApiChk.Enabled = False
                End Try

                AccDateRequired.Checked = accd
                If LIC.DiagTarget = "H" Then
                    lblDiagTarget.Text = "H"
                    Me.Text += " (Human)"
                Else
                    lblDiagTarget.Text = "V"
                    Me.Text += " (Veterinary)"
                End If
                chkCustomRpt.Checked = drls("CustomRPT")
                cmbPatPrice.SelectedIndex = drls("PatientPriceLevel")
                txtTax.Text = drls("Tax").ToString
                txtDueDays.Text = drls("DueDays").ToString
                txtTerm.Text = drls("Term")
                If drls("AdrLblFile") IsNot DBNull.Value Then txtAdrLblFile.Text = Trim(drls("AdrLblFile"))
                cmbSPType.SelectedIndex = drls("Specimen_Type")
                txtPanicSpan.Text = drls("PanicSpan")
                If drls("SendoutSpan") IsNot DBNull.Value Then txtSendoutSpan.Text = drls("SendoutSpan")
                If drls("AccSeed") IsNot DBNull.Value Then txtAccSeed.Text = drls("Accseed")
                If drls("PDFPath") IsNot DBNull.Value Then txtPDFPath.Text = drls("PDFPath")
                cmbDefaultBilling.SelectedIndex = drls("DefaultBilling")
                If drls("PatientInvoiceFile") IsNot DBNull.Value Then txtPatInvFile.Text = drls("PatientInvoiceFile")

                If drls("GenericResults") IsNot DBNull.Value Then txtGRR.Text = drls("GenericResults")
                If drls("CustomResults") IsNot DBNull.Value Then txtCRR.Text = drls("CustomResults")
                chkSMCA.Checked = drls("Mark_Source_Constraint")
                chkProfile.Checked = drls("ProfileBreak")
                chkRelWEntry.Checked = drls("ReleaseWithEntry")
                Try
                    bufferCHk.Checked = drls("EQ_Results_BufferChk")
                Catch ex As Exception

                End Try
                If drls("FaxServer") IsNot DBNull.Value Then txtFaxServer.Text = drls("FaxServer")
                txtFaxRetries.Text = drls("FaxRetries")
                txtRetryDelay.Text = drls("FaxRetryDelay")
                If drls("ProlisSMTP") IsNot DBNull.Value Then txtProlisSMTP.Text = drls("ProlisSMTP")
                If drls("ProlisEmail") IsNot DBNull.Value Then txtProlisEmail.Text = drls("ProlisEmail")
                If drls("ProlisPWD") IsNot DBNull.Value Then txtProlisPWD.Text = drls("ProlisPWD")
                If drls("ProlisEmailPort") IsNot DBNull.Value Then txtProlisEmailPort.Text = drls("ProlisEmailPort")
                chkProlisSSL.Checked = drls("ProlisUseSSL")
                If drls("OutreachSMTP") IsNot DBNull.Value Then txtOutreachSMTP.Text = drls("OutreachSMTP")
                If drls("OutreachEmail") IsNot DBNull.Value Then txtOutreachEmail.Text = drls("OutreachEmail")
                If drls("OutreachPWD") IsNot DBNull.Value Then txtOutreachPWD.Text = drls("OutreachPWD")
                If drls("OREmailPort") IsNot DBNull.Value Then txtOREmailPort.Text = drls("OREmailPort")
                chkORSSL.Checked = drls("ORUseSSL")
                txtRDMInterval.Text = drls("RDMInterval").ToString
                If drls("PhoneMask") IsNot DBNull.Value Then txtPhoneMask.Text = drls("PhoneMask")
                If drls("InPhlebTGP") IsNot DBNull.Value Then txtInPhleb.Text = drls("InPhlebTGP")
                If drls("HPhlebTGP") IsNot DBNull.Value Then txtHPhleb.Text = drls("HPhlebTGP")
                If drls("CPhlebTGP") IsNot DBNull.Value Then txtCPhleb.Text = drls("CPhlebTGP")
                chkOutreachPub.Checked = drls("Outreach_Publish")
                chkProlisOnPub.Checked = drls("ProlisOn_Publish")
                chkFaxPub.Checked = drls("Fax_Publish")
                chkHL7AutoPub.Checked = drls("HL7AutoPub")
                chkBillIntegrate.Checked = drls("Billing_Integrate")
                chkBARHistory.Checked = drls("BAR_HISTORY")
                chkResHistory.Checked = drls("RES_HISTORY")
                chkCumRef.Checked = drls("Cumulative_Reflex")
                txtInstances.Text = drls("Max_Instances")
                chkAuditTrail.Checked = drls("AuditTrail")
                chkParInHouse.Checked = drls("ParInHouse")
                For i = 0 To cmbSource.Items.Count - 1
                    If cmbSource.Items(i).ToString = drls("Source") Then
                        cmbSource.SelectedIndex = i
                        Exit For
                    End If
                Next
                If cmbSource.SelectedIndex = -1 Then cmbSource.SelectedIndex = 0
                For i = 0 To cmbTemp.Items.Count - 1
                    If cmbTemp.Items(i).ToString = drls("Temperature") Then
                        cmbTemp.SelectedIndex = i
                        Exit For
                    End If
                Next
                If cmbTemp.SelectedIndex = -1 Then cmbTemp.SelectedIndex = 1
                If drls("Environment") IsNot DBNull.Value AndAlso drls("Environment") <> "" Then
                    txtEnvironment.Text = drls("Environment")
                Else
                    txtEnvironment.Text = "United States-English"
                End If
                If drls("Culture") IsNot DBNull.Value AndAlso drls("Culture") <> "" Then
                    txtCulture.Text = drls("Culture")
                Else
                    txtCulture.Text = "en-US"
                End If
                If drls("DateFormat") IsNot DBNull.Value AndAlso drls("DateFormat") <> "" Then
                    txtDateFormat.Text = drls("DateFormat")
                Else
                    txtDateFormat.Text = "MM/dd/yyyy"
                End If

                Try
                    chkAddCounterWithLabels.Checked = IIf(drls("AddCounterWithLabels") Is DBNull.Value, False, drls("AddCounterWithLabels"))
                Catch ex As Exception
                    chkAddCounterWithLabels.Checked = False
                End Try

            End While
        End If
        cnls.Close()
        cnls = Nothing
    End Sub

    Private Sub LoadBarcode()
        Dim i As Integer
        Dim PC As String = My.Computer.Name
        Dim sSQL As String = "Select * from LabelPrinters where PC_Name = '" & PC & "'"
        If ThisUser.UseRemotePrinter Then
            sSQL = "Select * from LabelPrinters where UserID = '" & ThisUser.Name.Trim() & "'  "
            cmbLabelPrinter.SelectedIndex = cmbLabelPrinter.Items.Count() - 1
            Dim cnlb As New SqlConnection(connString)
            cnlb.Open()
            Dim cmdlb As New SqlCommand(sSQL, cnlb)
            cmdlb.CommandType = Data.CommandType.Text
            Dim drlb As SqlDataReader = cmdlb.ExecuteReader
            If drlb.HasRows Then
                While drlb.Read
                    For i = 0 To cmbLabelPrinter.Items.Count - 1
                        If cmbLabelPrinter.Items(i).ToString = drlb("Printer_Name") Then
                            cmbLabelPrinter.SelectedIndex = i
                            Exit For
                        End If
                    Next
                    txtAddLabels.Text = drlb("AddLabels")
                    txtAddLabels.Text = drlb("AddLabels")
                    Try
                        txtAccLabel.Text = drlb("LabelFileName")
                    Catch ex As Exception
                        txtAccLabel.Text = CommonData.RetrieveColumnValue("System_Config", "AccLabel", "1", "1", "")
                    End Try
                End While
            Else
                txtAddLabels.Text = "0"
            End If
            If txtAccLabel.Text = "" Then
                txtAccLabel.Text = CommonData.RetrieveColumnValue("System_Config", "AccLabel", "1", "1", "")
            End If
            cnlb.Close()
            cnlb = Nothing
        Else
            Dim cnlb As New SqlConnection(connString)
            cnlb.Open()
            Dim cmdlb As New SqlCommand(sSQL, cnlb)
            cmdlb.CommandType = Data.CommandType.Text
            Dim drlb As SqlDataReader = cmdlb.ExecuteReader
            If drlb.HasRows Then
                While drlb.Read
                    For i = 0 To cmbLabelPrinter.Items.Count - 1
                        If cmbLabelPrinter.Items(i).ToString = drlb("Printer_Name") Then
                            cmbLabelPrinter.SelectedIndex = i
                            Exit For
                        End If
                    Next

                    txtAddLabels.Text = drlb("AddLabels")
                    Try
                        txtAccLabel.Text = drlb("LabelFileName")
                    Catch ex As Exception
                        txtAccLabel.Text = CommonData.RetrieveColumnValue("System_Config", "AccLabel", "1", "1", "")
                    End Try

                End While
            Else

                txtAddLabels.Text = "0"

            End If
            If txtAccLabel.Text = "" Then
                txtAccLabel.Text = CommonData.RetrieveColumnValue("System_Config", "AccLabel", "1", "1", "")
            End If
            cnlb.Close()
            cnlb = Nothing

            '===============================================
            cmbLabelPrinter.SelectedItem = My.Settings.LabelPrinter
            '===============================================

        End If

    End Sub

    Private Sub SaveBarcode()
        Dim PC As String = My.Computer.Name

        If ThisUser.UseRemotePrinter Then
            PC = ThisUser.PrinterPC
            If PC = Nothing Then
                ThisUser.PrinterPC = My.Computer.Name
            End If
            If cmbLabelPrinter.SelectedIndex <> -1 Then
                Dim sSQL As String = "If Exists (Select * from LabelPrinters where PC_Name = '" & PC & "' and UserID = '" & ThisUser.Name & "') Update " &
                "LabelPrinters Set LabelFileName ='" & Trim(txtAccLabel.Text) & "',Printer_Name = '" & cmbLabelPrinter.SelectedItem.ToString & "', PC_Name = '" &
                PC & "', AddLabels = " & Val(txtAddLabels.Text) & " ,UserID = '" & ThisUser.Name & "' where UserID = '" & ThisUser.Name & "' Else Insert into " &
                "LabelPrinters (LabelFileName,Printer_Name, PC_Name, AddLabels,UserID) values ('" & Trim(txtAccLabel.Text) & "','" & cmbLabelPrinter.SelectedItem.ToString &
                "', '" & PC & "', " & Val(txtAddLabels.Text) & ",'" & ThisUser.Name & "')"
                ExecuteSqlProcedure(sSQL)
                Dim sd = "if exists (select * from Users where UserName = '" & ThisUser.UserName.Trim() & "') update users set PrinterPC ='" & PC & "' where UserName = '" & ThisUser.UserName.Trim() & "'"
                ExecuteSqlProcedure(sd)
                ExecuteSqlProcedure("Delete from LabelPrinters where PC_Name = '" & PC &
                "' and UserID= '" & ThisUser.Name & "' and Not Printer_Name = '" & cmbLabelPrinter.SelectedItem.ToString & "'")
            Else
                ExecuteSqlProcedure("Delete from LabelPrinters where PC_Name = '" & PC & "'")
            End If
        Else
            PC = My.Computer.Name
            If cmbLabelPrinter.SelectedIndex <> -1 Then

                Dim sSQL As String = "If Exists (Select * from LabelPrinters where PC_Name = '" & PC & "') Update " &
                "LabelPrinters Set  Printer_Name = '" & cmbLabelPrinter.SelectedItem.ToString & "', PC_Name = '" &
                PC & "', AddLabels = " & Val(txtAddLabels.Text) & " where PC_Name = '" & PC & "' Else Insert into " &
                "LabelPrinters (Printer_Name, PC_Name, AddLabels) values ('" & cmbLabelPrinter.SelectedItem.ToString &
                "', '" & PC & "', " & Val(txtAddLabels.Text) & ")"
                Dim TargetPath As String = My.Application.Info.DirectoryPath & "\Reports\"
                If txtAccLabel.Text IsNot Nothing AndAlso
                IO.File.Exists(TargetPath & Trim(txtAccLabel.Text)) Then


                    sSQL = "If Exists (Select * from LabelPrinters where PC_Name = '" & PC & "') Update " &
                "LabelPrinters Set LabelFileName ='" & Trim(txtAccLabel.Text) & "', Printer_Name = '" & cmbLabelPrinter.SelectedItem.ToString & "', PC_Name = '" &
                PC & "', AddLabels = " & Val(txtAddLabels.Text) & " where PC_Name = '" & PC & "' Else Insert into " &
                "LabelPrinters (LabelFileName,Printer_Name, PC_Name, AddLabels) values ('" & Trim(txtAccLabel.Text) & "','" & cmbLabelPrinter.SelectedItem.ToString &
                "', '" & PC & "', " & Val(txtAddLabels.Text) & ")"

                End If

                ExecuteSqlProcedure(sSQL)
                ExecuteSqlProcedure("Delete from LabelPrinters where PC_Name = '" & PC &
                "' and Not Printer_Name = '" & cmbLabelPrinter.SelectedItem.ToString & "'")
            Else
                ExecuteSqlProcedure("Delete from LabelPrinters where PC_Name = '" & PC & "'")
            End If
            Try
                If Not cmbLabelPrinter.SelectedItem Is Nothing Then
                    My.Settings.LabelPrinter = cmbLabelPrinter.SelectedItem.ToString
                    My.Settings.Save()
                End If

            Catch ex As Exception

            End Try


        End If

    End Sub

    Private Sub SaveConfiguration()
        Try
            Dim TargetPath As String = Path.Combine(AppContext.BaseDirectory, "Reports")
            Dim query As String = "SELECT * FROM System_Config WHERE Company_ID = @CompanyID"

            Using conn As New SqlConnection(connString)
                conn.Open()

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.Add(New SqlParameter("@CompanyID", MyLab.ID))

                    ' Load existing configuration or insert new row if it doesn't exist
                    Dim dt As New System.Data.DataTable()
                    Dim adapter As New SqlDataAdapter(cmd) ' Declare and initialize the adapter here
                    adapter.Fill(dt)

                    If dt.Rows.Count = 0 Then
                        Dim row As System.Data.DataRow = dt.NewRow()
                        row("Company_ID") = MyLab.ID
                        dt.Rows.Add(row)
                    End If

                    ' Update fields
                    Dim configRow As System.Data.DataRow = dt.Rows(0)
                    configRow("CustomRPT") = chkCustomRpt.Checked
                    configRow("PatientPriceLevel") = cmbPatPrice.SelectedIndex
                    configRow("Tax") = Val(txtTax.Text)
                    configRow("DueDays") = Val(txtDueDays.Text)
                    configRow("Term") = txtTerm.Text
                    configRow("AdrLblFile") = txtAdrLblFile.Text.Trim()
                    configRow("Specimen_Type") = cmbSPType.SelectedIndex
                    configRow("SendoutSpan") = Val(txtSendoutSpan.Text)
                    configRow("PanicSpan") = Val(txtPanicSpan.Text)
                    configRow("AccSeed") = txtAccSeed.Text.Trim()
                    configRow("PDFPath") = txtPDFPath.Text.Trim()
                    configRow("DefaultBilling") = cmbDefaultBilling.SelectedIndex
                    configRow("PatientInvoiceFile") = txtPatInvFile.Text
                    configRow("InPhlebTGP") = txtInPhleb.Text.Trim()
                    configRow("HPhlebTGP") = txtHPhleb.Text.Trim()
                    configRow("CPhlebTGP") = txtCPhleb.Text.Trim()
                    configRow("Portal_AccDate_required") = AccDateRequired.Checked
                    configRow("UseColorApi") = useColorApiChk.Checked

                    ' File Checks
                    If IO.File.Exists(Path.Combine(TargetPath, txtGRR.Text.Trim())) Then
                        configRow("GenericResults") = txtGRR.Text.Trim()
                    Else
                        configRow("GenericResults") = ""
                    End If

                    If IO.File.Exists(Path.Combine(TargetPath, txtCRR.Text.Trim())) Then
                        configRow("CustomResults") = txtCRR.Text.Trim()
                    Else
                        configRow("CustomResults") = ""
                    End If

                    Try
                        ' Convert files to binary and save
                        Dim reportPath As String = Path.Combine(TargetPath, txtGRR.Text.Trim())
                        Dim customReportPath As String = Path.Combine(TargetPath, txtCRR.Text.Trim())
                        configRow("Resulting_Rpt") = File.ReadAllBytes(reportPath)
                        configRow("Custom_Resulting_Rpt") = File.ReadAllBytes(customReportPath)
                    Catch ex As Exception
                        Console.WriteLine("Error processing files: " & ex.Message)
                    End Try

                    ' Additional fields
                    configRow("Mark_Source_Constraint") = chkSMCA.Checked
                    configRow("ProfileBreak") = chkProfile.Checked
                    configRow("ReleaseWithEntry") = chkRelWEntry.Checked
                    configRow("EQ_Results_BufferChk") = bufferCHk.Checked
                    configRow("RDMInterval") = Val(txtRDMInterval.Text)
                    configRow("FaxServer") = txtFaxServer.Text.Trim()
                    configRow("FaxRetries") = Val(txtFaxRetries.Text)
                    configRow("FaxRetryDelay") = Val(txtRetryDelay.Text)
                    configRow("ProlisSMTP") = txtProlisSMTP.Text
                    configRow("ProlisEmail") = txtProlisEmail.Text
                    configRow("ProlisPWD") = txtProlisPWD.Text
                    configRow("ProlisEmailPort") = txtProlisEmailPort.Text
                    configRow("ProlisUseSSL") = chkProlisSSL.Checked
                    configRow("OutreachSMTP") = txtOutreachSMTP.Text
                    configRow("OutreachEmail") = txtOutreachEmail.Text
                    configRow("OutreachPWD") = txtOutreachPWD.Text
                    configRow("OREmailPort") = txtOREmailPort.Text
                    configRow("ORUseSSL") = chkORSSL.Checked
                    configRow("PhoneMask") = txtPhoneMask.Text.Trim()
                    configRow("Outreach_Publish") = chkOutreachPub.Checked
                    configRow("ProlisOn_Publish") = chkProlisOnPub.Checked
                    configRow("QR_Checked") = QRChecked.Checked
                    configRow("ReportableFormat") = ReportableFormat.Checked
                    configRow("MergeSameDayAccession") = MergeSameDayAccession.Checked
                    configRow("P_inputOnLabel") = P_inputOnLabel.Checked
                    configRow("Fax_Publish") = chkFaxPub.Checked
                    configRow("Billing_Integrate") = chkBillIntegrate.Checked
                    configRow("HL7AutoPub") = chkHL7AutoPub.Checked
                    configRow("BAR_HISTORY") = chkBARHistory.Checked
                    configRow("RES_HISTORY") = chkResHistory.Checked
                    configRow("Cumulative_Reflex") = chkCumRef.Checked
                    configRow("Max_Instances") = Val(txtInstances.Text)
                    configRow("AuditTrail") = chkAuditTrail.Checked
                    configRow("Source") = cmbSource.SelectedItem.ToString()
                    configRow("Temperature") = cmbTemp.SelectedItem.ToString()
                    configRow("ParInHouse") = chkParInHouse.Checked
                    configRow("Environment") = txtEnvironment.Text
                    configRow("Culture") = txtCulture.Text
                    configRow("DateFormat") = txtDateFormat.Text
                    configRow("AddCounterWithLabels") = chkAddCounterWithLabels.Checked

                    ' Save changes
                    Using updateCmd As New SqlCommandBuilder(adapter)
                        adapter.Update(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Function UploadPatientInvoice(ByVal oFile As String) As String
        Dim FLName As String = ""
        Dim TargetPath As String = My.Application.Info.DirectoryPath & "\Reports\"
        If Trim(oFile) <> "" Then
            FLName = Microsoft.VisualBasic.Mid(Trim(oFile),
            InStrRev(Trim(oFile), "\") + 1)
            If Trim(oFile) <> TargetPath & FLName Then _
            System.IO.File.Copy(Trim(oFile), TargetPath & FLName, True)
        End If
        Return FLName
    End Function

    Private Function UploadAdrLabel(ByVal oFile As String) As String
        Dim FLName As String = ""
        Dim TargetPath As String = My.Application.Info.DirectoryPath & "\Reports\"
        If Trim(oFile) <> "" Then
            FLName = Microsoft.VisualBasic.Mid(Trim(oFile),
            InStrRev(Trim(oFile), "\") + 1)
            If Trim(oFile) <> TargetPath & FLName Then _
            System.IO.File.Copy(Trim(oFile), TargetPath & FLName, True)
        End If
        Return FLName
    End Function

    Private Function UploadAccLabel(ByVal oFile As String) As String
        Dim FLName As String = ""
        Dim TargetPath As String = My.Application.Info.DirectoryPath & "\Reports\"
        If Trim(oFile) <> "" Then
            FLName = Microsoft.VisualBasic.Mid(Trim(oFile),
            InStrRev(Trim(oFile), "\") + 1)
            If Trim(oFile) <> TargetPath & FLName Then _
            System.IO.File.Copy(Trim(oFile), TargetPath & FLName, True)
        End If
        Return FLName
    End Function

    Private Function UploadGenericReport(ByVal oFile As String) As String
        Dim FLName As String = ""
        Dim TargetPath As String = My.Application.Info.DirectoryPath & "\Reports\"
        If Trim(oFile) <> "" Then
            FLName = Microsoft.VisualBasic.Mid(Trim(oFile),
            InStrRev(Trim(oFile), "\") + 1)
            If Trim(oFile) <> TargetPath & FLName Then _
            System.IO.File.Copy(Trim(oFile), TargetPath & FLName, True)
        End If
        Return FLName
    End Function

    Private Function UploadCustomReport(ByVal oFile As String) As String
        Dim FLName As String = ""
        Dim TargetPath As String = My.Application.Info.DirectoryPath & "\Reports\"
        If Trim(oFile) <> "" Then
            FLName = Microsoft.VisualBasic.Mid(Trim(oFile),
            InStrRev(Trim(oFile), "\") + 1)
            If Trim(oFile) <> TargetPath & FLName Then _
            System.IO.File.Copy(Trim(oFile), TargetPath & FLName, True)
        End If
        Return FLName
    End Function

    Private Function SetupFaxServer(ByVal SRVR As String, ByVal Retries As Integer, ByVal Delay As Integer) As Boolean
        Dim IsThere As Boolean = False
        'Try
        '    Dim FaxServer As New FAXCOMEXLib.FaxServer
        '    Dim FaxOutQue As FAXCOMEXLib.FaxOutgoingQueue
        '    FaxServer.Connect(Trim(SRVR))
        '    FaxOutQue = FaxServer.Folders.OutgoingQueue
        '    FaxOutQue.Blocked = False
        '    FaxOutQue.Paused = False
        '    FaxOutQue.Retries = Retries
        '    FaxOutQue.RetryDelay = Delay
        '    FaxOutQue.Save()
        '    IsThere = True
        'Catch Ex As Exception
        '    IsThere = False
        'End Try
        SetupFaxServer = IsThere
    End Function

    Private Sub txtFaxRetries_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFaxRetries.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtRetryDelay_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRetryDelay.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAddLabels_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAddLabels.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAddLabels_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAddLabels.Validated
        If txtAddLabels.Text = "" Then txtAddLabels.Text = "0"
    End Sub

    Private Sub txtSendoutSpan_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSendoutSpan.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtSendoutSpan_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSendoutSpan.Validated
        If Val(txtSendoutSpan.Text) < 1 Then txtSendoutSpan.Text = "7"
    End Sub

    Private Sub btnLabelLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLabelLook.Click
        Dim DLG As New OpenFileDialog
        DLG.Filter = "Crystal Report Files (*.RPT)|*.rpt|All files (*.*)|*.*"
        If MsgBoxResult.Ok = DLG.ShowDialog Then
            txtAccLabel.Text = UploadAccLabel(DLG.FileName)
        Else
            txtAccLabel.Text = ""
        End If
        DLG.Dispose()
        DLG = Nothing
    End Sub

    Private Sub btnGRRLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGRRLook.Click
        Dim DLG As New OpenFileDialog
        DLG.Filter = "Crystal Report File (*.RPT)|*.rpt"
        If MsgBoxResult.Ok = DLG.ShowDialog Then
            txtGRR.Text = UploadGenericReport(DLG.FileName)
        Else
            txtGRR.Text = ""
        End If
        DLG.Dispose()
        DLG = Nothing
    End Sub

    Private Sub btnCRRLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCRRLook.Click
        Dim DLG As New OpenFileDialog
        DLG.Filter = "Crystal Report File (*.RPT)|*.rpt"
        If MsgBoxResult.Ok = DLG.ShowDialog Then
            txtCRR.Text = UploadCustomReport(DLG.FileName)
        Else
            txtCRR.Text = ""
        End If
        DLG.Dispose()
        DLG = Nothing
    End Sub

    Private Sub chkProfile_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkProfile.CheckedChanged
        If chkProfile.Checked = False Then
            chkProfile.Text = "Integral"
        Else
            chkProfile.Text = "Break"
        End If
    End Sub

    Private Sub chkRelWEntry_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRelWEntry.CheckedChanged
        If chkRelWEntry.Checked = True Then
            chkRelWEntry.Text = "Yes"
        Else
            chkRelWEntry.Text = "No"
        End If
    End Sub

    Private Sub chkBARHistory_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBARHistory.CheckedChanged
        If chkBARHistory.Checked = False Then
            chkBARHistory.Text = "No"
        Else
            chkBARHistory.Text = "Yes"
        End If
    End Sub

    Private Sub chkResHistory_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkResHistory.CheckedChanged
        If chkResHistory.Checked = False Then
            chkResHistory.Text = "No"
        Else
            chkResHistory.Text = "Yes"
        End If
    End Sub

    Private Sub chkFaxPub_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFaxPub.CheckedChanged
        If chkFaxPub.Checked = False Then
            chkFaxPub.Text = "No"
        Else
            chkFaxPub.Text = "Yes"
        End If
    End Sub

    Private Sub chkBillIntegrate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBillIntegrate.CheckedChanged
        If chkBillIntegrate.Checked = False Then
            chkBillIntegrate.Text = "No"
        Else
            chkBillIntegrate.Text = "Yes"
        End If
    End Sub

    Private Sub txtInstances_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtInstances.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtInstances_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInstances.Validated
        If txtInstances.Text = "" Then txtInstances.Text = "12"
    End Sub

    Private Sub btnHL7AutoPub_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkHL7AutoPub.CheckedChanged
        If chkHL7AutoPub.Checked = False Then
            chkHL7AutoPub.Text = "No"
        Else
            chkHL7AutoPub.Text = "Yes"
        End If
    End Sub

    Private Sub chkAditTrail_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAuditTrail.CheckedChanged
        If chkAuditTrail.Checked = False Then
            chkAuditTrail.Text = "NO"
        Else
            chkAuditTrail.Text = "YES"
        End If
    End Sub

    Private Sub btnAddLblLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddLblLook.Click
        Dim DLG As New OpenFileDialog
        DLG.Filter = "Dymo Label Files (*.Label)|*.Label|All files (*.*)|*.*"
        If MsgBoxResult.Ok = DLG.ShowDialog Then
            txtAdrLblFile.Text = UploadAdrLabel(DLG.FileName)
        Else
            txtAdrLblFile.Text = ""
        End If
        DLG.Dispose()
        DLG = Nothing
    End Sub

    Private Sub chkParInHouse_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkParInHouse.CheckedChanged
        If chkParInHouse.Checked = False Then
            chkParInHouse.Text = "No"
        Else
            chkParInHouse.Text = "Yes"
        End If
    End Sub

    Private Sub chkProlisOnPub_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkProlisOnPub.CheckedChanged
        If chkProlisOnPub.Checked = False Then
            chkProlisOnPub.Text = "No"
        Else
            chkProlisOnPub.Text = "Yes"
        End If
    End Sub

    Private Sub chkOutreachPub_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOutreachPub.CheckedChanged
        If chkOutreachPub.Checked = False Then
            chkOutreachPub.Text = "No"
        Else
            chkOutreachPub.Text = "Yes"
        End If
    End Sub

    Private Sub chkCumRef_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCumRef.CheckedChanged
        If chkCumRef.Checked = True Then
            chkCumRef.Text = "Yes"
        Else
            chkCumRef.Text = "No"
        End If
    End Sub

    Private Sub btnPartRPTQualifier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPartRPTQualifier.Click
        frmPartRPTQualifiers.ShowDialog()
    End Sub

    Private Sub btnCulLookup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCulLookup.Click
        Dim EnvInfo As String = frmCultureLookUp.ShowDialog
        If EnvInfo <> "" Then
            Dim TempVals() As String = Split(EnvInfo, "|")
            txtEnvironment.Text = TempVals(0) & "-" & TempVals(1)
            txtCulture.Text = TempVals(2)
            If TempVals(2) <> "" Then
                Dim CINFO As System.Globalization.CultureInfo =
                New System.Globalization.CultureInfo(TempVals(2))
                txtDateFormat.Text = CINFO.DateTimeFormat.ShortDatePattern
                If txtDateFormat.Text = "M/d/yyyy" Then
                    txtDateFormat.Text = "MM/dd/yyyy"
                ElseIf txtDateFormat.Text = "d/M/yyyy" Then
                    txtDateFormat.Text = "dd/MM/yyyy"
                ElseIf txtDateFormat.Text = "d/M/yy" Then
                    txtDateFormat.Text = "dd/MM/yyyy"
                ElseIf txtDateFormat.Text = "M/d/yy" Then
                    txtDateFormat.Text = "MM/dd/yyyy"
                End If
            Else
                txtDateFormat.Text = "MM/dd/yyyy"
            End If
        End If
    End Sub

    Private Sub btnTimeSensitive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTimeSensitive.Click
        frmTimeSensitive.ShowDialog()
    End Sub

    Private Sub btnPDFPathLookup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDFPathLookup.Click
        Dim DLG As New FolderBrowserDialog
        'Dim MyFolder As System.Environment.SpecialFolder
        'MyFolder = CType(txtPDFPath.Text, System.Environment.SpecialFolder)
        'DLG.RootFolder = Environment.SpecialFolder.Recent
        If MsgBoxResult.Ok = DLG.ShowDialog Then
            txtPDFPath.Text = DLG.SelectedPath
        End If
        DLG.Dispose()
        DLG = Nothing
    End Sub

    Private Sub chkProlisSSL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkProlisSSL.CheckedChanged
        If chkProlisSSL.Checked = False Then    'No
            chkProlisSSL.Text = "No"
        Else
            chkProlisSSL.Text = "Yes"
        End If
    End Sub

    Private Sub chkORSSL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkORSSL.CheckedChanged
        If chkORSSL.Checked = False Then    'No
            chkORSSL.Text = "No"
        Else
            chkORSSL.Text = "Yes"
        End If
    End Sub

    Private Sub btnPatInvLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatInvLook.Click
        Dim DLG As New OpenFileDialog
        DLG.Filter = "Crystal Report Files (*.RPT)|*.RPT|All files (*.*)|*.*"
        If MsgBoxResult.Ok = DLG.ShowDialog Then
            txtPatInvFile.Text = UploadPatientInvoice(DLG.FileName)
        Else
            txtAdrLblFile.Text = ""
        End If
        DLG.Dispose()
        DLG = Nothing
    End Sub

    Private Sub btnPTestEmail_Click(sender As Object, e As EventArgs) Handles btnPTestEmail.Click
        If Trim(txtProlisSMTP.Text) <> "" And Trim(txtProlisEmailPort.Text) <> "" And Trim(txtProlisPWD.Text) <> "" _
        And ValidateEmail(Trim(txtProlisEmail.Text)) And ValidateEmail(Trim(txtPTestEmail.Text)) Then
            Dim SmtpServer As New SmtpClient()
            SmtpServer.Host = Trim(txtProlisSMTP.Text)  '"smtp.readyhosting.com"
            SmtpServer.Credentials = New Net.NetworkCredential(Trim(txtProlisEmail.Text), Trim(txtProlisPWD.Text))
            'SmtpServer.Credentials = New Net.NetworkCredential("rama@prolis.info", "RamaOR15")
            SmtpServer.Port = Trim(txtProlisEmailPort.Text) '587
            SmtpServer.EnableSsl = chkProlisSSL.Checked
            '
            Dim mail As New MailMessage()
            'mail = New MailMessage()
            mail.From = New MailAddress(Trim(txtProlisEmail.Text))
            mail.To.Add(Trim(txtPTestEmail.Text))
            mail.Subject = "Testing email functionality In prolis"
            mail.Body = "Congratulation! the email configuration In Prolis was successful."
            Try
                SmtpServer.Send(mail)
                My.Application.DoEvents()
                txtPTestEmail.Text = ""
                btnPTestEmail.Enabled = False
                '
                MsgBox("An email sent To the address provided, sucessfully.")
            Catch ex As Exception
                MsgBox("The following Error occured In the configuration." & vbCrLf & "'" & ex.Message & "'")
            End Try
        Else
            MsgBox("All pink color labeled field require your input. Try again.")
        End If
    End Sub

    Private Sub txtPTestEmail_TextChanged(sender As Object, e As EventArgs) Handles txtPTestEmail.TextChanged
        If Trim(txtProlisSMTP.Text) <> "" And Trim(txtProlisEmailPort.Text) <> "" And Trim(txtProlisPWD.Text) <> "" And
        ValidateEmail(Trim(txtProlisEmail.Text)) And ValidateEmail(Trim(txtPTestEmail.Text)) Then btnPTestEmail.Enabled = True
    End Sub

    Private Sub QRChecked_CheckedChanged(sender As Object, e As EventArgs) Handles QRChecked.CheckedChanged
        If QRChecked.Checked = True Then
            QRChecked.Text = "Yes"
        Else
            QRChecked.Text = "No"
        End If
    End Sub


    Private Sub MergeSameDayAccession_CheckedChanged(sender As Object, e As EventArgs) Handles MergeSameDayAccession.CheckedChanged
        If MergeSameDayAccession.Checked = True Then
            MergeSameDayAccession.Text = "Yes"
        Else
            MergeSameDayAccession.Text = "No"
        End If
    End Sub

    Private Sub P_inputOnLabel_CheckedChanged(sender As Object, e As EventArgs) Handles P_inputOnLabel.CheckedChanged
        If P_inputOnLabel.Checked = True Then
            P_inputOnLabel.Text = "Yes"
        Else
            P_inputOnLabel.Text = "No"
        End If
    End Sub

    Private Sub CMBRemotPrintPC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CMBRemotPrintPC.SelectedIndexChanged
        If CMBRemotPrintPC.Enabled Then
            If Not CMBRemotPrintPC.SelectedItem Is Nothing Then
                ThisUser.PrinterPC = CMBRemotPrintPC.SelectedItem.ToString()
            End If


        End If

    End Sub

    Private Sub ReportableFormat_CheckedChanged(sender As Object, e As EventArgs) Handles ReportableFormat.CheckedChanged
        If ReportableFormat.Checked = True Then
            ReportableFormat.Text = "Yes"
        Else
            ReportableFormat.Text = "No"
        End If
    End Sub

    Private Sub cmbuserPrinter_SelectedIndexChanged(sender As Object, e As EventArgs)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ' Reset controls to initial values
        ResetControls(Me.Controls)

        ' Disable the Save button
        btnSave.Enabled = False
    End Sub

    ' Recursively reset controls to initial values
    Private Sub ResetControls(controls As Control.ControlCollection)
        For Each ctrl As Control In controls
            If TypeOf ctrl Is TextBox OrElse TypeOf ctrl Is ComboBox OrElse TypeOf ctrl Is CheckBox OrElse TypeOf ctrl Is RadioButton Then
                If initialValues.ContainsKey(ctrl) Then
                    SetControlValue(ctrl, initialValues(ctrl))
                End If
            End If
            ' Recursively reset container controls
            If ctrl.HasChildren Then
                ResetControls(ctrl.Controls)
            End If
        Next
    End Sub

    ' Set control value based on control type
    Private Sub SetControlValue(ctrl As Control, value As Object)
        If TypeOf ctrl Is TextBox Then
            DirectCast(ctrl, TextBox).Text = value.ToString()
        ElseIf TypeOf ctrl Is ComboBox Then
            DirectCast(ctrl, ComboBox).SelectedItem = value
        ElseIf TypeOf ctrl Is CheckBox Then
            DirectCast(ctrl, CheckBox).Checked = Convert.ToBoolean(value)
        ElseIf TypeOf ctrl Is RadioButton Then
            DirectCast(ctrl, RadioButton).Checked = Convert.ToBoolean(value)
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveChanges()
    End Sub

    ' Add event handlers to monitor changes in controls
    Private Sub AddHandlers(controls As Control.ControlCollection)
        For Each ctrl As Control In controls
            If TypeOf ctrl Is TextBox Then
                AddHandler DirectCast(ctrl, TextBox).TextChanged, AddressOf ControlChanged
            ElseIf TypeOf ctrl Is ComboBox Then
                AddHandler DirectCast(ctrl, ComboBox).SelectedIndexChanged, AddressOf ControlChanged
            ElseIf TypeOf ctrl Is CheckBox Then
                AddHandler DirectCast(ctrl, CheckBox).CheckedChanged, AddressOf ControlChanged
            ElseIf TypeOf ctrl Is RadioButton Then
                AddHandler DirectCast(ctrl, RadioButton).CheckedChanged, AddressOf ControlChanged
            End If
            ' Recursively handle container controls like panels, group boxes, etc.
            If ctrl.HasChildren Then
                AddHandlers(ctrl.Controls)
            End If
        Next
    End Sub

    ' Event handler to enable the Save button when a control value changes
    Private Sub ControlChanged(sender As Object, e As EventArgs)
        btnSave.Enabled = HasChanges(Me.Controls)
        btnCancel.Enabled = btnSave.Enabled
    End Sub


    ' Function to save Crystal Reports files into System_Config table
    Public Sub SaveReportsToDatabase(reportPath As String, customReportPath As String, Optional path As String = "")
        Dim connectionString As String = connString

        Try
            reportPath = path & Trim(reportPath)
            customReportPath = path & Trim(customReportPath)
            ' Read the report files as binary data
            Dim reportData As Byte() = File.ReadAllBytes(reportPath)
            Dim customReportData As Byte() = File.ReadAllBytes(customReportPath)

            Using con As New SqlConnection(connectionString)
                con.Open()
                Dim query As String = "UPDATE System_Config SET Resulting_Rpt = @ReportData, Custom_Resulting_Rpt = @CustomReportData where Company_ID = " & MyLab.ID

                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.Add("@ReportData", SqlDbType.VarBinary).Value = reportData
                    cmd.Parameters.Add("@CustomReportData", SqlDbType.VarBinary).Value = customReportData

                    cmd.ExecuteNonQuery()
                End Using
            End Using

            'MessageBox.Show("Reports saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Shared Sub RetrieveAndSaveReports()
        Dim connectionString As String = connString
        Dim reportsDirectory As String = My.Application.Info.DirectoryPath & "\Reports"

        ' Ensure the Reports directory exists
        If Not Directory.Exists(reportsDirectory) Then
            Directory.CreateDirectory(reportsDirectory)
        End If

        Try
            Using con As New SqlConnection(connectionString)
                con.Open()
                Dim query As String = "SELECT Resulting_Rpt, Custom_Resulting_Rpt, GenericResults, CustomResults FROM System_Config  where Company_ID = " & MyLab.ID

                Using cmd As New SqlCommand(query, con)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            ' Retrieve binary data from database
                            Dim reportData As Byte() = If(Not reader.IsDBNull(0), CType(reader(0), Byte()), Nothing)
                            Dim customReportData As Byte() = If(Not reader.IsDBNull(1), CType(reader(1), Byte()), Nothing)

                            ' Retrieve file names from GenericResults and CustomResults columns
                            Try
                                Dim reportFileName As String = If(Not reader.IsDBNull(2), reader.GetString(2).Trim(), "")
                                If String.IsNullOrEmpty(reportFileName) Then reportFileName = "Default_Report.rpt"
                                If reportData IsNot Nothing AndAlso reportFileName <> "" Then
                                    Dim reportFullPath As String = Path.Combine(reportsDirectory, reportFileName)
                                    File.WriteAllBytes(reportFullPath, reportData)
                                End If
                            Catch ex As Exception

                            End Try
                            Try
                                Dim customReportFileName As String = If(Not reader.IsDBNull(3), reader.GetString(3).Trim(), "")
                                If String.IsNullOrEmpty(customReportFileName) Then customReportFileName = "Default_Custom_Report.rpt"

                                If customReportData IsNot Nothing AndAlso customReportFileName <> "" Then
                                    Dim customReportFullPath As String = Path.Combine(reportsDirectory, customReportFileName)
                                    File.WriteAllBytes(customReportFullPath, customReportData)
                                End If
                            Catch ex As Exception

                            End Try

                            ' Ensure filenames are valid

                            ' Save files if data is available




                            'MessageBox.Show("Reports retrieved and saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            ' MessageBox.Show("No reports found in the database.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Function IsFTPConnected() As Boolean
        Try
            Dim ft As FTPConfig = New FTPConfig()

            If ft.FTPUsername Is Nothing OrElse ft.FTPUsername = "" Then
                ft = GetFTPSettings()

            End If
            Dim ftpServer As String = ft.FTPServer
            Dim ftpUsername As String = ft.FTPUsername
            Dim ftpPassword As String = ft.FTPPassword


            ' Create an FTP request to the root directory
            Dim request As FtpWebRequest = CType(WebRequest.Create(ftpServer), FtpWebRequest)
            request.Method = WebRequestMethods.Ftp.ListDirectory
            request.Credentials = New NetworkCredential(ftpUsername, ftpPassword)
            request.UsePassive = True
            request.UseBinary = True
            request.KeepAlive = False

            ' Try to get a response from the FTP server
            Using response As FtpWebResponse = CType(request.GetResponse(), FtpWebResponse)
                ' If the response status code is positive, connection is successful
                MessageBox.Show("FTP Connection Successful: " & response.StatusDescription)
                Return True
            End Using
        Catch ex As WebException
            MessageBox.Show("FTP Connection Failed: " & ex.Message)
            Return False
        End Try
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        IsFTPConnected()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles bufferCHk.CheckedChanged
        If bufferCHk.Checked = True Then
            bufferCHk.Text = "Yes"
        Else
            bufferCHk.Text = "No"
        End If
    End Sub
End Class
