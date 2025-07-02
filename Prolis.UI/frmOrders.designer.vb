<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOrders
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOrders))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        btnNew = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnSave = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnDelete = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        lblOrders = New Label()
        lblBilling = New Label()
        lblPatient = New Label()
        lblOrderer = New Label()
        TC = New TabControl()
        tpGeneral = New TabPage()
        lblClearDates = New Label()
        Label4 = New Label()
        cmbPhlebLoc = New ComboBox()
        Label40 = New Label()
        Label24 = New Label()
        chklstDays = New CheckedListBox()
        Label2 = New Label()
        txtWorkCmnt = New TextBox()
        chkInfiniteTimed = New CheckBox()
        Label38 = New Label()
        txtAgencyContact = New TextBox()
        Label37 = New Label()
        Label13 = New Label()
        Label12 = New Label()
        Label11 = New Label()
        txtAgencyAddress = New TextBox()
        txtAgencyPhone = New MaskedTextBox()
        txtAgency = New TextBox()
        btnAgencyLookUp = New Button()
        txtAgencyID = New TextBox()
        btnNext1 = New Button()
        chkActive = New CheckBox()
        Label9 = New Label()
        Label8 = New Label()
        Label7 = New Label()
        dtpDischargeDate = New DateTimePicker()
        dtpOrderDate = New DateTimePicker()
        btnOrderLook = New Button()
        txtOrderID = New TextBox()
        Label1 = New Label()
        tpProvider = New TabPage()
        txtOrdFax = New MaskedTextBox()
        txtOrdPhone = New MaskedTextBox()
        btnPrev2 = New Button()
        btnNext2 = New Button()
        Label96 = New Label()
        txtProvEmail = New TextBox()
        lstProviders = New CheckedListBox()
        Label95 = New Label()
        btnRemProv = New Button()
        Label25 = New Label()
        txtCountry = New TextBox()
        Label19 = New Label()
        Label18 = New Label()
        Label17 = New Label()
        txtOrdCSZ = New TextBox()
        Label16 = New Label()
        txtOrdAddress = New TextBox()
        Label15 = New Label()
        txtOrdName = New TextBox()
        btnOrdLookup = New Button()
        Label14 = New Label()
        txtOrdID = New TextBox()
        tpPatient = New TabPage()
        lblDxs = New Label()
        dgvDxs = New DataGridView()
        DataGridViewTextBoxColumn1 = New DataGridViewTextBoxColumn()
        Lookup = New DataGridViewImageColumn()
        btnPatUpdate = New Button()
        txtPatHPhone = New MaskedTextBox()
        lblEMRReqd = New Label()
        txtEMRNo = New TextBox()
        btnPrev3 = New Button()
        btnNext3 = New Button()
        chkNeedFast = New CheckBox()
        Label100 = New Label()
        lblZip = New Label()
        lblState = New Label()
        txtPatCountry = New TextBox()
        txtPatZip = New TextBox()
        txtPatState = New TextBox()
        Label97 = New Label()
        txtPatAdr2 = New TextBox()
        btnRemDxAll = New Button()
        btnRemPat = New Button()
        Label36 = New Label()
        Label34 = New Label()
        Label35 = New Label()
        txtPatientID = New TextBox()
        Label30 = New Label()
        txtDOB = New MaskedTextBox()
        btnPatLook = New Button()
        cmbSex = New ComboBox()
        txtLName = New TextBox()
        lblHPhone = New Label()
        Label29 = New Label()
        txtPatAdr1 = New TextBox()
        Label32 = New Label()
        lblAdd1 = New Label()
        txtSSN = New MaskedTextBox()
        txtPatCity = New TextBox()
        Label31 = New Label()
        lblCity = New Label()
        txtMName = New TextBox()
        txtPatEmail = New TextBox()
        txtFName = New TextBox()
        Label26 = New Label()
        tpBilling = New TabPage()
        grpPSubs = New GroupBox()
        btnPrev5 = New Button()
        txtPSubLName = New TextBox()
        Label57 = New Label()
        txtPSubSSN = New MaskedTextBox()
        Label59 = New Label()
        txtPSubDOB = New MaskedTextBox()
        cmbPSubSex = New ComboBox()
        btnPSubLook = New Button()
        Label60 = New Label()
        txtPSubCountry = New TextBox()
        Label61 = New Label()
        txtPSubEmail = New TextBox()
        Label62 = New Label()
        txtPSubPhone = New TextBox()
        Label63 = New Label()
        txtPSubZip = New TextBox()
        Label64 = New Label()
        txtPSubState = New TextBox()
        Label65 = New Label()
        txtPSubCity = New TextBox()
        Label66 = New Label()
        txtPSubAdd2 = New TextBox()
        Label67 = New Label()
        txtPSubAdd1 = New TextBox()
        Label68 = New Label()
        Label69 = New Label()
        txtPSubMName = New TextBox()
        Label70 = New Label()
        txtPSubFName = New TextBox()
        Label71 = New Label()
        Label72 = New Label()
        txtPSubID = New TextBox()
        grpPrimary = New GroupBox()
        txtPCopay = New TextBox()
        Label58 = New Label()
        Label51 = New Label()
        cmbPRelation = New ComboBox()
        Label52 = New Label()
        txtPFrom = New MaskedTextBox()
        btnPIns = New Button()
        Label53 = New Label()
        cmbPIns = New ComboBox()
        txtPGroup = New TextBox()
        txtPTo = New MaskedTextBox()
        Label54 = New Label()
        Label55 = New Label()
        txtPInsID = New TextBox()
        Label56 = New Label()
        Label49 = New Label()
        Label48 = New Label()
        cmbPrimResp = New ComboBox()
        chkSvcGratis = New CheckBox()
        tpOrders = New TabPage()
        GroupBox1 = New GroupBox()
        txtMaxCount = New TextBox()
        Label10 = New Label()
        btnUpdate = New Button()
        dtpStartDate = New DateTimePicker()
        txtEndDate = New MaskedTextBox()
        cmbInterval = New ComboBox()
        Label6 = New Label()
        Label39 = New Label()
        Label5 = New Label()
        txtQty = New TextBox()
        Label3 = New Label()
        Label23 = New Label()
        Label22 = New Label()
        btnCompLookUp = New Button()
        txtTGPName = New TextBox()
        txtTGPID = New TextBox()
        Label20 = New Label()
        dgvTGPs = New DataGridView()
        Del = New DataGridViewImageColumn()
        Comp_ID = New DataGridViewTextBoxColumn()
        CompName = New DataGridViewTextBoxColumn()
        Starts = New DataGridViewTextBoxColumn()
        Frequency = New DataGridViewTextBoxColumn()
        QTY = New DataGridViewTextBoxColumn()
        MaxCount = New DataGridViewTextBoxColumn()
        EndDate = New DataGridViewTextBoxColumn()
        lblGeneral = New Label()
        ToolStrip1.SuspendLayout()
        TC.SuspendLayout()
        tpGeneral.SuspendLayout()
        tpProvider.SuspendLayout()
        tpPatient.SuspendLayout()
        CType(dgvDxs, ComponentModel.ISupportInitialize).BeginInit()
        tpBilling.SuspendLayout()
        grpPSubs.SuspendLayout()
        grpPrimary.SuspendLayout()
        tpOrders.SuspendLayout()
        GroupBox1.SuspendLayout()
        CType(dgvTGPs, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnNew, ToolStripSeparator1, btnSave, ToolStripSeparator2, btnDelete, ToolStripSeparator3, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(1120, 34)
        ToolStrip1.TabIndex = 1
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnNew
        ' 
        btnNew.CheckOnClick = True
        btnNew.Image = CType(resources.GetObject("btnNew.Image"), Image)
        btnNew.ImageTransparentColor = Color.Magenta
        btnNew.Name = "btnNew"
        btnNew.Size = New Size(75, 29)
        btnNew.Text = "New"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
        ' 
        ' btnSave
        ' 
        btnSave.Enabled = False
        btnSave.Image = CType(resources.GetObject("btnSave.Image"), Image)
        btnSave.ImageTransparentColor = Color.Magenta
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(77, 29)
        btnSave.Text = "Save"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 34)
        ' 
        ' btnDelete
        ' 
        btnDelete.Enabled = False
        btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), Image)
        btnDelete.ImageTransparentColor = Color.Magenta
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(90, 29)
        btnDelete.Text = "Delete"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(6, 34)
        ' 
        ' btnCancel
        ' 
        btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), Image)
        btnCancel.ImageTransparentColor = Color.Magenta
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(91, 29)
        btnCancel.Text = "Cancel"
        ' 
        ' ToolStripSeparator4
        ' 
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New Size(6, 34)
        ' 
        ' btnHelp
        ' 
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.ImageTransparentColor = Color.Magenta
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(77, 29)
        btnHelp.Text = "Help"
        ' 
        ' lblOrders
        ' 
        lblOrders.BackColor = Color.PeachPuff
        lblOrders.BorderStyle = BorderStyle.FixedSingle
        lblOrders.Font = New Font("Microsoft Sans Serif", 7F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblOrders.Location = New Point(802, 71)
        lblOrders.Margin = New Padding(5, 0, 5, 0)
        lblOrders.Name = "lblOrders"
        lblOrders.Size = New Size(155, 31)
        lblOrders.TabIndex = 25
        lblOrders.Text = "Components"
        lblOrders.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblBilling
        ' 
        lblBilling.BackColor = Color.PeachPuff
        lblBilling.BorderStyle = BorderStyle.FixedSingle
        lblBilling.Font = New Font("Microsoft Sans Serif", 7F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblBilling.Location = New Point(650, 71)
        lblBilling.Margin = New Padding(5, 0, 5, 0)
        lblBilling.Name = "lblBilling"
        lblBilling.Size = New Size(155, 31)
        lblBilling.TabIndex = 24
        lblBilling.Text = "Billing"
        lblBilling.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblPatient
        ' 
        lblPatient.BackColor = Color.PeachPuff
        lblPatient.BorderStyle = BorderStyle.FixedSingle
        lblPatient.Font = New Font("Microsoft Sans Serif", 7F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblPatient.Location = New Point(495, 71)
        lblPatient.Margin = New Padding(5, 0, 5, 0)
        lblPatient.Name = "lblPatient"
        lblPatient.Size = New Size(155, 31)
        lblPatient.TabIndex = 23
        lblPatient.Text = "Patient"
        lblPatient.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblOrderer
        ' 
        lblOrderer.BackColor = Color.PeachPuff
        lblOrderer.BorderStyle = BorderStyle.FixedSingle
        lblOrderer.Font = New Font("Microsoft Sans Serif", 7F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblOrderer.Location = New Point(340, 71)
        lblOrderer.Margin = New Padding(5, 0, 5, 0)
        lblOrderer.Name = "lblOrderer"
        lblOrderer.Size = New Size(155, 31)
        lblOrderer.TabIndex = 22
        lblOrderer.Text = "Ordering Provider"
        lblOrderer.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' TC
        ' 
        TC.Controls.Add(tpGeneral)
        TC.Controls.Add(tpProvider)
        TC.Controls.Add(tpPatient)
        TC.Controls.Add(tpBilling)
        TC.Controls.Add(tpOrders)
        TC.Location = New Point(11, 124)
        TC.Margin = New Padding(5, 6, 5, 6)
        TC.Name = "TC"
        TC.SelectedIndex = 0
        TC.Size = New Size(1095, 696)
        TC.TabIndex = 27
        ' 
        ' tpGeneral
        ' 
        tpGeneral.Controls.Add(lblClearDates)
        tpGeneral.Controls.Add(Label4)
        tpGeneral.Controls.Add(cmbPhlebLoc)
        tpGeneral.Controls.Add(Label40)
        tpGeneral.Controls.Add(Label24)
        tpGeneral.Controls.Add(chklstDays)
        tpGeneral.Controls.Add(Label2)
        tpGeneral.Controls.Add(txtWorkCmnt)
        tpGeneral.Controls.Add(chkInfiniteTimed)
        tpGeneral.Controls.Add(Label38)
        tpGeneral.Controls.Add(txtAgencyContact)
        tpGeneral.Controls.Add(Label37)
        tpGeneral.Controls.Add(Label13)
        tpGeneral.Controls.Add(Label12)
        tpGeneral.Controls.Add(Label11)
        tpGeneral.Controls.Add(txtAgencyAddress)
        tpGeneral.Controls.Add(txtAgencyPhone)
        tpGeneral.Controls.Add(txtAgency)
        tpGeneral.Controls.Add(btnAgencyLookUp)
        tpGeneral.Controls.Add(txtAgencyID)
        tpGeneral.Controls.Add(btnNext1)
        tpGeneral.Controls.Add(chkActive)
        tpGeneral.Controls.Add(Label9)
        tpGeneral.Controls.Add(Label8)
        tpGeneral.Controls.Add(Label7)
        tpGeneral.Controls.Add(dtpDischargeDate)
        tpGeneral.Controls.Add(dtpOrderDate)
        tpGeneral.Controls.Add(btnOrderLook)
        tpGeneral.Controls.Add(txtOrderID)
        tpGeneral.Controls.Add(Label1)
        tpGeneral.Location = New Point(4, 34)
        tpGeneral.Margin = New Padding(5, 6, 5, 6)
        tpGeneral.Name = "tpGeneral"
        tpGeneral.Padding = New Padding(5, 6, 5, 6)
        tpGeneral.Size = New Size(1087, 658)
        tpGeneral.TabIndex = 5
        tpGeneral.Text = "General"
        tpGeneral.UseVisualStyleBackColor = True
        ' 
        ' lblClearDates
        ' 
        lblClearDates.BackColor = SystemColors.Control
        lblClearDates.Image = My.Resources.Resources.Eraser
        lblClearDates.Location = New Point(638, 65)
        lblClearDates.Name = "lblClearDates"
        lblClearDates.Size = New Size(33, 46)
        lblClearDates.TabIndex = 94
        lblClearDates.Text = "      "
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(827, 337)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(202, 27)
        Label4.TabIndex = 41
        Label4.Text = "Collection Site"
        ' 
        ' cmbPhlebLoc
        ' 
        cmbPhlebLoc.DropDownStyle = ComboBoxStyle.DropDownList
        cmbPhlebLoc.FormattingEnabled = True
        cmbPhlebLoc.Items.AddRange(New Object() {"By Client", "In Lab", "By Lab at Home", "By Lab at Care"})
        cmbPhlebLoc.Location = New Point(813, 377)
        cmbPhlebLoc.Margin = New Padding(5, 6, 5, 6)
        cmbPhlebLoc.Name = "cmbPhlebLoc"
        cmbPhlebLoc.Size = New Size(241, 33)
        cmbPhlebLoc.TabIndex = 40
        ' 
        ' Label40
        ' 
        Label40.ForeColor = Color.Red
        Label40.Location = New Point(762, 27)
        Label40.Margin = New Padding(5, 0, 5, 0)
        Label40.Name = "Label40"
        Label40.Size = New Size(122, 31)
        Label40.TabIndex = 38
        Label40.Text = "Order Type"
        Label40.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label24
        ' 
        Label24.ForeColor = Color.Red
        Label24.Location = New Point(655, 337)
        Label24.Margin = New Padding(5, 0, 5, 0)
        Label24.Name = "Label24"
        Label24.Size = New Size(147, 27)
        Label24.TabIndex = 37
        Label24.Text = "Order Days"
        ' 
        ' chklstDays
        ' 
        chklstDays.CheckOnClick = True
        chklstDays.FormattingEnabled = True
        chklstDays.Items.AddRange(New Object() {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"})
        chklstDays.Location = New Point(643, 369)
        chklstDays.Margin = New Padding(5, 6, 5, 6)
        chklstDays.Name = "chklstDays"
        chklstDays.Size = New Size(156, 228)
        chklstDays.TabIndex = 36
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(55, 337)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(202, 27)
        Label2.TabIndex = 35
        Label2.Text = "Internal Note"
        ' 
        ' txtWorkCmnt
        ' 
        txtWorkCmnt.AccessibleDescription = "l"
        txtWorkCmnt.Location = New Point(37, 377)
        txtWorkCmnt.Margin = New Padding(5, 6, 5, 6)
        txtWorkCmnt.MaxLength = 960
        txtWorkCmnt.Multiline = True
        txtWorkCmnt.Name = "txtWorkCmnt"
        txtWorkCmnt.Size = New Size(577, 227)
        txtWorkCmnt.TabIndex = 34
        ' 
        ' chkInfiniteTimed
        ' 
        chkInfiniteTimed.Appearance = Appearance.Button
        chkInfiniteTimed.Location = New Point(717, 63)
        chkInfiniteTimed.Margin = New Padding(5, 6, 5, 6)
        chkInfiniteTimed.Name = "chkInfiniteTimed"
        chkInfiniteTimed.Size = New Size(202, 46)
        chkInfiniteTimed.TabIndex = 33
        chkInfiniteTimed.Text = "Infinite"
        chkInfiniteTimed.TextAlign = ContentAlignment.MiddleCenter
        chkInfiniteTimed.UseVisualStyleBackColor = True
        ' 
        ' Label38
        ' 
        Label38.ForeColor = Color.DarkBlue
        Label38.Location = New Point(723, 142)
        Label38.Margin = New Padding(5, 0, 5, 0)
        Label38.Name = "Label38"
        Label38.Size = New Size(160, 27)
        Label38.TabIndex = 32
        Label38.Text = "Agency Contact"
        ' 
        ' txtAgencyContact
        ' 
        txtAgencyContact.Location = New Point(717, 175)
        txtAgencyContact.Margin = New Padding(5, 6, 5, 6)
        txtAgencyContact.MaxLength = 70
        txtAgencyContact.Name = "txtAgencyContact"
        txtAgencyContact.Size = New Size(164, 31)
        txtAgencyContact.TabIndex = 31
        ' 
        ' Label37
        ' 
        Label37.ForeColor = Color.DarkBlue
        Label37.Location = New Point(903, 144)
        Label37.Margin = New Padding(5, 0, 5, 0)
        Label37.Name = "Label37"
        Label37.Size = New Size(153, 27)
        Label37.TabIndex = 30
        Label37.Text = "Agency Phone"
        ' 
        ' Label13
        ' 
        Label13.ForeColor = Color.DarkBlue
        Label13.Location = New Point(32, 240)
        Label13.Margin = New Padding(5, 0, 5, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(202, 27)
        Label13.TabIndex = 29
        Label13.Text = "Agency Address"
        ' 
        ' Label12
        ' 
        Label12.ForeColor = Color.DarkBlue
        Label12.Location = New Point(262, 142)
        Label12.Margin = New Padding(5, 0, 5, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(200, 27)
        Label12.TabIndex = 28
        Label12.Text = "Agency Name"
        ' 
        ' Label11
        ' 
        Label11.ForeColor = Color.DarkBlue
        Label11.Location = New Point(37, 144)
        Label11.Margin = New Padding(5, 0, 5, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(125, 27)
        Label11.TabIndex = 27
        Label11.Text = "Agency ID"
        ' 
        ' txtAgencyAddress
        ' 
        txtAgencyAddress.Location = New Point(32, 273)
        txtAgencyAddress.Margin = New Padding(5, 6, 5, 6)
        txtAgencyAddress.MaxLength = 180
        txtAgencyAddress.Name = "txtAgencyAddress"
        txtAgencyAddress.ReadOnly = True
        txtAgencyAddress.Size = New Size(1022, 31)
        txtAgencyAddress.TabIndex = 26
        ' 
        ' txtAgencyPhone
        ' 
        txtAgencyPhone.Location = New Point(900, 175)
        txtAgencyPhone.Margin = New Padding(5, 6, 5, 6)
        txtAgencyPhone.Mask = "(999) 000-0000"
        txtAgencyPhone.Name = "txtAgencyPhone"
        txtAgencyPhone.Size = New Size(154, 31)
        txtAgencyPhone.TabIndex = 25
        ' 
        ' txtAgency
        ' 
        txtAgency.Location = New Point(243, 175)
        txtAgency.Margin = New Padding(5, 6, 5, 6)
        txtAgency.MaxLength = 100
        txtAgency.Name = "txtAgency"
        txtAgency.ReadOnly = True
        txtAgency.Size = New Size(461, 31)
        txtAgency.TabIndex = 24
        ' 
        ' btnAgencyLookUp
        ' 
        btnAgencyLookUp.Image = CType(resources.GetObject("btnAgencyLookUp.Image"), Image)
        btnAgencyLookUp.Location = New Point(192, 171)
        btnAgencyLookUp.Margin = New Padding(5, 6, 5, 6)
        btnAgencyLookUp.Name = "btnAgencyLookUp"
        btnAgencyLookUp.Size = New Size(42, 44)
        btnAgencyLookUp.TabIndex = 23
        btnAgencyLookUp.UseVisualStyleBackColor = True
        ' 
        ' txtAgencyID
        ' 
        txtAgencyID.Location = New Point(32, 177)
        txtAgencyID.Margin = New Padding(5, 6, 5, 6)
        txtAgencyID.MaxLength = 10
        txtAgencyID.Name = "txtAgencyID"
        txtAgencyID.Size = New Size(147, 31)
        txtAgencyID.TabIndex = 22
        txtAgencyID.TextAlign = HorizontalAlignment.Center
        ' 
        ' btnNext1
        ' 
        btnNext1.Location = New Point(900, 556)
        btnNext1.Margin = New Padding(5, 6, 5, 6)
        btnNext1.Name = "btnNext1"
        btnNext1.Size = New Size(157, 44)
        btnNext1.TabIndex = 21
        btnNext1.Text = "Next"
        btnNext1.UseVisualStyleBackColor = True
        ' 
        ' chkActive
        ' 
        chkActive.Appearance = Appearance.Button
        chkActive.Checked = True
        chkActive.CheckState = CheckState.Checked
        chkActive.Location = New Point(935, 63)
        chkActive.Margin = New Padding(5, 6, 5, 6)
        chkActive.Name = "chkActive"
        chkActive.Size = New Size(122, 46)
        chkActive.TabIndex = 5
        chkActive.Text = "Yes"
        chkActive.TextAlign = ContentAlignment.MiddleCenter
        chkActive.UseVisualStyleBackColor = True
        ' 
        ' Label9
        ' 
        Label9.ForeColor = Color.DarkBlue
        Label9.Location = New Point(935, 23)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(122, 31)
        Label9.TabIndex = 18
        Label9.Text = "Active"
        Label9.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label8
        ' 
        Label8.ForeColor = Color.DarkBlue
        Label8.Location = New Point(433, 27)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(160, 29)
        Label8.TabIndex = 17
        Label8.Text = "Discharge Date"
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.DarkBlue
        Label7.Location = New Point(262, 27)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(143, 27)
        Label7.TabIndex = 16
        Label7.Text = "Start Date"
        ' 
        ' dtpDischargeDate
        ' 
        dtpDischargeDate.Format = DateTimePickerFormat.Short
        dtpDischargeDate.Location = New Point(438, 69)
        dtpDischargeDate.Margin = New Padding(5, 6, 5, 6)
        dtpDischargeDate.Name = "dtpDischargeDate"
        dtpDischargeDate.Size = New Size(177, 31)
        dtpDischargeDate.TabIndex = 3
        ' 
        ' dtpOrderDate
        ' 
        dtpOrderDate.Format = DateTimePickerFormat.Short
        dtpOrderDate.Location = New Point(243, 69)
        dtpOrderDate.Margin = New Padding(5, 6, 5, 6)
        dtpOrderDate.Name = "dtpOrderDate"
        dtpOrderDate.Size = New Size(177, 31)
        dtpOrderDate.TabIndex = 3
        ' 
        ' btnOrderLook
        ' 
        btnOrderLook.Enabled = False
        btnOrderLook.Image = CType(resources.GetObject("btnOrderLook.Image"), Image)
        btnOrderLook.Location = New Point(192, 65)
        btnOrderLook.Margin = New Padding(5, 6, 5, 6)
        btnOrderLook.Name = "btnOrderLook"
        btnOrderLook.Size = New Size(42, 44)
        btnOrderLook.TabIndex = 2
        btnOrderLook.UseVisualStyleBackColor = True
        ' 
        ' txtOrderID
        ' 
        txtOrderID.Location = New Point(32, 69)
        txtOrderID.Margin = New Padding(5, 6, 5, 6)
        txtOrderID.MaxLength = 10
        txtOrderID.Name = "txtOrderID"
        txtOrderID.Size = New Size(147, 31)
        txtOrderID.TabIndex = 1
        txtOrderID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.Red
        Label1.Location = New Point(32, 27)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(150, 27)
        Label1.TabIndex = 0
        Label1.Text = "Order ID"
        Label1.TextAlign = ContentAlignment.TopCenter
        ' 
        ' tpProvider
        ' 
        tpProvider.Controls.Add(txtOrdFax)
        tpProvider.Controls.Add(txtOrdPhone)
        tpProvider.Controls.Add(btnPrev2)
        tpProvider.Controls.Add(btnNext2)
        tpProvider.Controls.Add(Label96)
        tpProvider.Controls.Add(txtProvEmail)
        tpProvider.Controls.Add(lstProviders)
        tpProvider.Controls.Add(Label95)
        tpProvider.Controls.Add(btnRemProv)
        tpProvider.Controls.Add(Label25)
        tpProvider.Controls.Add(txtCountry)
        tpProvider.Controls.Add(Label19)
        tpProvider.Controls.Add(Label18)
        tpProvider.Controls.Add(Label17)
        tpProvider.Controls.Add(txtOrdCSZ)
        tpProvider.Controls.Add(Label16)
        tpProvider.Controls.Add(txtOrdAddress)
        tpProvider.Controls.Add(Label15)
        tpProvider.Controls.Add(txtOrdName)
        tpProvider.Controls.Add(btnOrdLookup)
        tpProvider.Controls.Add(Label14)
        tpProvider.Controls.Add(txtOrdID)
        tpProvider.Location = New Point(4, 34)
        tpProvider.Margin = New Padding(5, 6, 5, 6)
        tpProvider.Name = "tpProvider"
        tpProvider.Padding = New Padding(5, 6, 5, 6)
        tpProvider.Size = New Size(1087, 658)
        tpProvider.TabIndex = 0
        tpProvider.Text = "Ordering Provider"
        tpProvider.UseVisualStyleBackColor = True
        ' 
        ' txtOrdFax
        ' 
        txtOrdFax.Location = New Point(298, 483)
        txtOrdFax.Margin = New Padding(5, 6, 5, 6)
        txtOrdFax.Name = "txtOrdFax"
        txtOrdFax.Size = New Size(186, 31)
        txtOrdFax.TabIndex = 70
        ' 
        ' txtOrdPhone
        ' 
        txtOrdPhone.Location = New Point(32, 483)
        txtOrdPhone.Margin = New Padding(5, 6, 5, 6)
        txtOrdPhone.Name = "txtOrdPhone"
        txtOrdPhone.Size = New Size(186, 31)
        txtOrdPhone.TabIndex = 69
        ' 
        ' btnPrev2
        ' 
        btnPrev2.Location = New Point(792, 533)
        btnPrev2.Margin = New Padding(5, 6, 5, 6)
        btnPrev2.Name = "btnPrev2"
        btnPrev2.Size = New Size(125, 44)
        btnPrev2.TabIndex = 68
        btnPrev2.Text = "Previous"
        btnPrev2.UseVisualStyleBackColor = True
        ' 
        ' btnNext2
        ' 
        btnNext2.Location = New Point(927, 533)
        btnNext2.Margin = New Padding(5, 6, 5, 6)
        btnNext2.Name = "btnNext2"
        btnNext2.Size = New Size(125, 44)
        btnNext2.TabIndex = 67
        btnNext2.Text = "Next"
        btnNext2.UseVisualStyleBackColor = True
        ' 
        ' Label96
        ' 
        Label96.Location = New Point(522, 452)
        Label96.Margin = New Padding(5, 0, 5, 0)
        Label96.Name = "Label96"
        Label96.Size = New Size(157, 25)
        Label96.TabIndex = 66
        Label96.Text = "Email"
        ' 
        ' txtProvEmail
        ' 
        txtProvEmail.BackColor = Color.White
        txtProvEmail.Location = New Point(527, 483)
        txtProvEmail.Margin = New Padding(5, 6, 5, 6)
        txtProvEmail.MaxLength = 25
        txtProvEmail.Name = "txtProvEmail"
        txtProvEmail.ReadOnly = True
        txtProvEmail.Size = New Size(522, 31)
        txtProvEmail.TabIndex = 65
        ' 
        ' lstProviders
        ' 
        lstProviders.FormattingEnabled = True
        lstProviders.Location = New Point(527, 79)
        lstProviders.Margin = New Padding(5, 6, 5, 6)
        lstProviders.Name = "lstProviders"
        lstProviders.Size = New Size(522, 340)
        lstProviders.TabIndex = 64
        ' 
        ' Label95
        ' 
        Label95.Location = New Point(522, 33)
        Label95.Margin = New Padding(5, 0, 5, 0)
        Label95.Name = "Label95"
        Label95.Size = New Size(198, 25)
        Label95.TabIndex = 63
        Label95.Text = "Attending Provider"
        ' 
        ' btnRemProv
        ' 
        btnRemProv.Enabled = False
        btnRemProv.Image = CType(resources.GetObject("btnRemProv.Image"), Image)
        btnRemProv.Location = New Point(30, 56)
        btnRemProv.Margin = New Padding(5, 6, 5, 6)
        btnRemProv.Name = "btnRemProv"
        btnRemProv.Size = New Size(50, 50)
        btnRemProv.TabIndex = 62
        btnRemProv.UseVisualStyleBackColor = True
        ' 
        ' Label25
        ' 
        Label25.Location = New Point(33, 363)
        Label25.Margin = New Padding(5, 0, 5, 0)
        Label25.Name = "Label25"
        Label25.Size = New Size(188, 25)
        Label25.TabIndex = 61
        Label25.Text = "Country"
        ' 
        ' txtCountry
        ' 
        txtCountry.BackColor = Color.White
        txtCountry.Location = New Point(33, 394)
        txtCountry.Margin = New Padding(5, 6, 5, 6)
        txtCountry.MaxLength = 35
        txtCountry.Name = "txtCountry"
        txtCountry.ReadOnly = True
        txtCountry.Size = New Size(186, 31)
        txtCountry.TabIndex = 60
        ' 
        ' Label19
        ' 
        Label19.Location = New Point(298, 452)
        Label19.Margin = New Padding(5, 0, 5, 0)
        Label19.Name = "Label19"
        Label19.Size = New Size(157, 25)
        Label19.TabIndex = 57
        Label19.Text = "Fax"
        ' 
        ' Label18
        ' 
        Label18.Location = New Point(33, 452)
        Label18.Margin = New Padding(5, 0, 5, 0)
        Label18.Name = "Label18"
        Label18.Size = New Size(187, 25)
        Label18.TabIndex = 55
        Label18.Text = "Telephone"
        ' 
        ' Label17
        ' 
        Label17.Location = New Point(33, 277)
        Label17.Margin = New Padding(5, 0, 5, 0)
        Label17.Name = "Label17"
        Label17.Size = New Size(273, 25)
        Label17.TabIndex = 53
        Label17.Text = "City, State and Zip"
        ' 
        ' txtOrdCSZ
        ' 
        txtOrdCSZ.BackColor = Color.White
        txtOrdCSZ.Location = New Point(33, 308)
        txtOrdCSZ.Margin = New Padding(5, 6, 5, 6)
        txtOrdCSZ.MaxLength = 112
        txtOrdCSZ.Name = "txtOrdCSZ"
        txtOrdCSZ.ReadOnly = True
        txtOrdCSZ.Size = New Size(451, 31)
        txtOrdCSZ.TabIndex = 52
        ' 
        ' Label16
        ' 
        Label16.Location = New Point(33, 196)
        Label16.Margin = New Padding(5, 0, 5, 0)
        Label16.Name = "Label16"
        Label16.Size = New Size(187, 25)
        Label16.TabIndex = 51
        Label16.Text = "Address"
        ' 
        ' txtOrdAddress
        ' 
        txtOrdAddress.BackColor = Color.White
        txtOrdAddress.Location = New Point(33, 227)
        txtOrdAddress.Margin = New Padding(5, 6, 5, 6)
        txtOrdAddress.MaxLength = 75
        txtOrdAddress.Name = "txtOrdAddress"
        txtOrdAddress.ReadOnly = True
        txtOrdAddress.Size = New Size(451, 31)
        txtOrdAddress.TabIndex = 50
        ' 
        ' Label15
        ' 
        Label15.Location = New Point(28, 117)
        Label15.Margin = New Padding(5, 0, 5, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(238, 25)
        Label15.TabIndex = 49
        Label15.Text = "Ordering Provider Name"
        ' 
        ' txtOrdName
        ' 
        txtOrdName.BackColor = Color.White
        txtOrdName.Location = New Point(33, 148)
        txtOrdName.Margin = New Padding(5, 6, 5, 6)
        txtOrdName.MaxLength = 95
        txtOrdName.Name = "txtOrdName"
        txtOrdName.ReadOnly = True
        txtOrdName.Size = New Size(451, 31)
        txtOrdName.TabIndex = 48
        ' 
        ' btnOrdLookup
        ' 
        btnOrdLookup.Image = CType(resources.GetObject("btnOrdLookup.Image"), Image)
        btnOrdLookup.Location = New Point(255, 56)
        btnOrdLookup.Margin = New Padding(5, 6, 5, 6)
        btnOrdLookup.Name = "btnOrdLookup"
        btnOrdLookup.Size = New Size(50, 50)
        btnOrdLookup.TabIndex = 47
        btnOrdLookup.UseVisualStyleBackColor = True
        ' 
        ' Label14
        ' 
        Label14.Location = New Point(90, 33)
        Label14.Margin = New Padding(5, 0, 5, 0)
        Label14.Name = "Label14"
        Label14.Size = New Size(155, 25)
        Label14.TabIndex = 46
        Label14.Text = "Orderer ID"
        ' 
        ' txtOrdID
        ' 
        txtOrdID.Location = New Point(90, 63)
        txtOrdID.Margin = New Padding(5, 6, 5, 6)
        txtOrdID.MaxLength = 12
        txtOrdID.Name = "txtOrdID"
        txtOrdID.Size = New Size(152, 31)
        txtOrdID.TabIndex = 45
        txtOrdID.TextAlign = HorizontalAlignment.Center
        ' 
        ' tpPatient
        ' 
        tpPatient.Controls.Add(lblDxs)
        tpPatient.Controls.Add(dgvDxs)
        tpPatient.Controls.Add(btnPatUpdate)
        tpPatient.Controls.Add(txtPatHPhone)
        tpPatient.Controls.Add(lblEMRReqd)
        tpPatient.Controls.Add(txtEMRNo)
        tpPatient.Controls.Add(btnPrev3)
        tpPatient.Controls.Add(btnNext3)
        tpPatient.Controls.Add(chkNeedFast)
        tpPatient.Controls.Add(Label100)
        tpPatient.Controls.Add(lblZip)
        tpPatient.Controls.Add(lblState)
        tpPatient.Controls.Add(txtPatCountry)
        tpPatient.Controls.Add(txtPatZip)
        tpPatient.Controls.Add(txtPatState)
        tpPatient.Controls.Add(Label97)
        tpPatient.Controls.Add(txtPatAdr2)
        tpPatient.Controls.Add(btnRemDxAll)
        tpPatient.Controls.Add(btnRemPat)
        tpPatient.Controls.Add(Label36)
        tpPatient.Controls.Add(Label34)
        tpPatient.Controls.Add(Label35)
        tpPatient.Controls.Add(txtPatientID)
        tpPatient.Controls.Add(Label30)
        tpPatient.Controls.Add(txtDOB)
        tpPatient.Controls.Add(btnPatLook)
        tpPatient.Controls.Add(cmbSex)
        tpPatient.Controls.Add(txtLName)
        tpPatient.Controls.Add(lblHPhone)
        tpPatient.Controls.Add(Label29)
        tpPatient.Controls.Add(txtPatAdr1)
        tpPatient.Controls.Add(Label32)
        tpPatient.Controls.Add(lblAdd1)
        tpPatient.Controls.Add(txtSSN)
        tpPatient.Controls.Add(txtPatCity)
        tpPatient.Controls.Add(Label31)
        tpPatient.Controls.Add(lblCity)
        tpPatient.Controls.Add(txtMName)
        tpPatient.Controls.Add(txtPatEmail)
        tpPatient.Controls.Add(txtFName)
        tpPatient.Controls.Add(Label26)
        tpPatient.Location = New Point(4, 34)
        tpPatient.Margin = New Padding(5, 6, 5, 6)
        tpPatient.Name = "tpPatient"
        tpPatient.Padding = New Padding(5, 6, 5, 6)
        tpPatient.Size = New Size(1087, 658)
        tpPatient.TabIndex = 1
        tpPatient.Text = "Patient"
        tpPatient.UseVisualStyleBackColor = True
        ' 
        ' lblDxs
        ' 
        lblDxs.ForeColor = Color.DarkBlue
        lblDxs.Location = New Point(720, 63)
        lblDxs.Margin = New Padding(5, 0, 5, 0)
        lblDxs.Name = "lblDxs"
        lblDxs.Size = New Size(123, 25)
        lblDxs.TabIndex = 107
        lblDxs.Text = "Diag Codes"
        ' 
        ' dgvDxs
        ' 
        dgvDxs.AllowUserToAddRows = False
        dgvDxs.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(255), CByte(224), CByte(192))
        dgvDxs.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = SystemColors.Control
        DataGridViewCellStyle2.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle2.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.True
        dgvDxs.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        dgvDxs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvDxs.Columns.AddRange(New DataGridViewColumn() {DataGridViewTextBoxColumn1, Lookup})
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = SystemColors.Window
        DataGridViewCellStyle3.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle3.ForeColor = Color.DarkBlue
        DataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False
        dgvDxs.DefaultCellStyle = DataGridViewCellStyle3
        dgvDxs.Location = New Point(703, 98)
        dgvDxs.Margin = New Padding(5, 6, 5, 6)
        dgvDxs.Name = "dgvDxs"
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = SystemColors.Control
        DataGridViewCellStyle4.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle4.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = DataGridViewTriState.True
        dgvDxs.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        dgvDxs.RowHeadersVisible = False
        dgvDxs.RowHeadersWidth = 62
        dgvDxs.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvDxs.Size = New Size(348, 323)
        dgvDxs.TabIndex = 106
        ' 
        ' DataGridViewTextBoxColumn1
        ' 
        DataGridViewTextBoxColumn1.FillWeight = 136F
        DataGridViewTextBoxColumn1.HeaderText = "Dx Code"
        DataGridViewTextBoxColumn1.MaxInputLength = 12
        DataGridViewTextBoxColumn1.MinimumWidth = 8
        DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        DataGridViewTextBoxColumn1.Width = 136
        ' 
        ' Lookup
        ' 
        Lookup.FillWeight = 52F
        Lookup.HeaderText = "LookUp"
        Lookup.Image = CType(resources.GetObject("Lookup.Image"), Image)
        Lookup.MinimumWidth = 8
        Lookup.Name = "Lookup"
        Lookup.Width = 52
        ' 
        ' btnPatUpdate
        ' 
        btnPatUpdate.ForeColor = Color.DarkBlue
        btnPatUpdate.Location = New Point(703, 475)
        btnPatUpdate.Margin = New Padding(5, 6, 5, 6)
        btnPatUpdate.Name = "btnPatUpdate"
        btnPatUpdate.Size = New Size(158, 52)
        btnPatUpdate.TabIndex = 105
        btnPatUpdate.Text = "Validate Patient"
        btnPatUpdate.TextAlign = ContentAlignment.MiddleRight
        btnPatUpdate.TextImageRelation = TextImageRelation.ImageBeforeText
        btnPatUpdate.UseVisualStyleBackColor = True
        ' 
        ' txtPatHPhone
        ' 
        txtPatHPhone.Location = New Point(18, 475)
        txtPatHPhone.Margin = New Padding(5, 6, 5, 6)
        txtPatHPhone.Name = "txtPatHPhone"
        txtPatHPhone.Size = New Size(169, 31)
        txtPatHPhone.TabIndex = 104
        ' 
        ' lblEMRReqd
        ' 
        lblEMRReqd.ForeColor = Color.DarkBlue
        lblEMRReqd.Location = New Point(23, 537)
        lblEMRReqd.Margin = New Padding(5, 0, 5, 0)
        lblEMRReqd.Name = "lblEMRReqd"
        lblEMRReqd.Size = New Size(253, 25)
        lblEMRReqd.TabIndex = 103
        lblEMRReqd.Text = "Chart/EMR No/Prof ID"
        ' 
        ' txtEMRNo
        ' 
        txtEMRNo.Location = New Point(18, 567)
        txtEMRNo.Margin = New Padding(5, 6, 5, 6)
        txtEMRNo.MaxLength = 25
        txtEMRNo.Name = "txtEMRNo"
        txtEMRNo.Size = New Size(324, 31)
        txtEMRNo.TabIndex = 102
        ' 
        ' btnPrev3
        ' 
        btnPrev3.Location = New Point(792, 562)
        btnPrev3.Margin = New Padding(5, 6, 5, 6)
        btnPrev3.Name = "btnPrev3"
        btnPrev3.Size = New Size(125, 44)
        btnPrev3.TabIndex = 101
        btnPrev3.Text = "Previous"
        btnPrev3.UseVisualStyleBackColor = True
        ' 
        ' btnNext3
        ' 
        btnNext3.Location = New Point(927, 562)
        btnNext3.Margin = New Padding(5, 6, 5, 6)
        btnNext3.Name = "btnNext3"
        btnNext3.Size = New Size(125, 44)
        btnNext3.TabIndex = 100
        btnNext3.Text = "Next"
        btnNext3.UseVisualStyleBackColor = True
        ' 
        ' chkNeedFast
        ' 
        chkNeedFast.CheckAlign = ContentAlignment.MiddleRight
        chkNeedFast.ForeColor = Color.DarkBlue
        chkNeedFast.Location = New Point(397, 563)
        chkNeedFast.Margin = New Padding(5, 6, 5, 6)
        chkNeedFast.Name = "chkNeedFast"
        chkNeedFast.Size = New Size(273, 42)
        chkNeedFast.TabIndex = 99
        chkNeedFast.Text = "Should Patient be Fasting?"
        chkNeedFast.UseVisualStyleBackColor = True
        ' 
        ' Label100
        ' 
        Label100.ForeColor = Color.DarkBlue
        Label100.Location = New Point(475, 352)
        Label100.Margin = New Padding(5, 0, 5, 0)
        Label100.Name = "Label100"
        Label100.Size = New Size(140, 25)
        Label100.TabIndex = 98
        Label100.Text = "Country"
        ' 
        ' lblZip
        ' 
        lblZip.ForeColor = Color.DarkBlue
        lblZip.Location = New Point(272, 352)
        lblZip.Margin = New Padding(5, 0, 5, 0)
        lblZip.Name = "lblZip"
        lblZip.Size = New Size(130, 25)
        lblZip.TabIndex = 97
        lblZip.Text = "Zip Code"
        ' 
        ' lblState
        ' 
        lblState.ForeColor = Color.DarkBlue
        lblState.Location = New Point(20, 352)
        lblState.Margin = New Padding(5, 0, 5, 0)
        lblState.Name = "lblState"
        lblState.Size = New Size(187, 25)
        lblState.TabIndex = 96
        lblState.Text = "State/Province"
        ' 
        ' txtPatCountry
        ' 
        txtPatCountry.BackColor = Color.White
        txtPatCountry.Location = New Point(462, 383)
        txtPatCountry.Margin = New Padding(5, 6, 5, 6)
        txtPatCountry.MaxLength = 35
        txtPatCountry.Name = "txtPatCountry"
        txtPatCountry.Size = New Size(206, 31)
        txtPatCountry.TabIndex = 76
        ' 
        ' txtPatZip
        ' 
        txtPatZip.BackColor = Color.White
        txtPatZip.Location = New Point(265, 383)
        txtPatZip.Margin = New Padding(5, 6, 5, 6)
        txtPatZip.MaxLength = 35
        txtPatZip.Name = "txtPatZip"
        txtPatZip.Size = New Size(177, 31)
        txtPatZip.TabIndex = 75
        ' 
        ' txtPatState
        ' 
        txtPatState.BackColor = Color.White
        txtPatState.Location = New Point(18, 383)
        txtPatState.Margin = New Padding(5, 6, 5, 6)
        txtPatState.MaxLength = 35
        txtPatState.Name = "txtPatState"
        txtPatState.Size = New Size(229, 31)
        txtPatState.TabIndex = 74
        ' 
        ' Label97
        ' 
        Label97.ForeColor = Color.DarkBlue
        Label97.Location = New Point(272, 260)
        Label97.Margin = New Padding(5, 0, 5, 0)
        Label97.Name = "Label97"
        Label97.Size = New Size(145, 25)
        Label97.TabIndex = 95
        Label97.Text = "Address 2"
        ' 
        ' txtPatAdr2
        ' 
        txtPatAdr2.BackColor = Color.White
        txtPatAdr2.Location = New Point(265, 290)
        txtPatAdr2.Margin = New Padding(5, 6, 5, 6)
        txtPatAdr2.MaxLength = 35
        txtPatAdr2.Name = "txtPatAdr2"
        txtPatAdr2.Size = New Size(177, 31)
        txtPatAdr2.TabIndex = 72
        ' 
        ' btnRemDxAll
        ' 
        btnRemDxAll.ForeColor = Color.Red
        btnRemDxAll.Image = CType(resources.GetObject("btnRemDxAll.Image"), Image)
        btnRemDxAll.Location = New Point(885, 475)
        btnRemDxAll.Margin = New Padding(5, 6, 5, 6)
        btnRemDxAll.Name = "btnRemDxAll"
        btnRemDxAll.Size = New Size(167, 52)
        btnRemDxAll.TabIndex = 83
        btnRemDxAll.Text = "Remove All"
        btnRemDxAll.TextAlign = ContentAlignment.MiddleRight
        btnRemDxAll.TextImageRelation = TextImageRelation.ImageBeforeText
        btnRemDxAll.UseVisualStyleBackColor = True
        ' 
        ' btnRemPat
        ' 
        btnRemPat.Enabled = False
        btnRemPat.Image = CType(resources.GetObject("btnRemPat.Image"), Image)
        btnRemPat.Location = New Point(18, 87)
        btnRemPat.Margin = New Padding(5, 6, 5, 6)
        btnRemPat.Name = "btnRemPat"
        btnRemPat.Size = New Size(50, 50)
        btnRemPat.TabIndex = 62
        btnRemPat.UseVisualStyleBackColor = True
        ' 
        ' Label36
        ' 
        Label36.ForeColor = Color.Red
        Label36.Location = New Point(362, 163)
        Label36.Margin = New Padding(5, 0, 5, 0)
        Label36.Name = "Label36"
        Label36.Size = New Size(123, 25)
        Label36.TabIndex = 94
        Label36.Text = "DOB"
        ' 
        ' Label34
        ' 
        Label34.ForeColor = Color.DarkBlue
        Label34.Location = New Point(83, 63)
        Label34.Margin = New Padding(5, 0, 5, 0)
        Label34.Name = "Label34"
        Label34.Size = New Size(132, 25)
        Label34.TabIndex = 92
        Label34.Text = "Patient ID"
        ' 
        ' Label35
        ' 
        Label35.ForeColor = Color.Red
        Label35.Location = New Point(227, 162)
        Label35.Margin = New Padding(5, 0, 5, 0)
        Label35.Name = "Label35"
        Label35.Size = New Size(118, 25)
        Label35.TabIndex = 93
        Label35.Text = "Gender"
        ' 
        ' txtPatientID
        ' 
        txtPatientID.BackColor = Color.Ivory
        txtPatientID.Location = New Point(78, 98)
        txtPatientID.Margin = New Padding(5, 6, 5, 6)
        txtPatientID.MaxLength = 12
        txtPatientID.Name = "txtPatientID"
        txtPatientID.Size = New Size(134, 31)
        txtPatientID.TabIndex = 63
        txtPatientID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label30
        ' 
        Label30.ForeColor = Color.Red
        Label30.Location = New Point(525, 63)
        Label30.Margin = New Padding(5, 0, 5, 0)
        Label30.Name = "Label30"
        Label30.Size = New Size(145, 25)
        Label30.TabIndex = 84
        Label30.Text = "First Name"
        ' 
        ' txtDOB
        ' 
        txtDOB.Location = New Point(367, 194)
        txtDOB.Margin = New Padding(5, 6, 5, 6)
        txtDOB.Mask = "00/00/0000"
        txtDOB.Name = "txtDOB"
        txtDOB.Size = New Size(144, 31)
        txtDOB.TabIndex = 69
        txtDOB.ValidatingType = GetType(Date)
        ' 
        ' btnPatLook
        ' 
        btnPatLook.Image = CType(resources.GetObject("btnPatLook.Image"), Image)
        btnPatLook.Location = New Point(227, 90)
        btnPatLook.Margin = New Padding(5, 6, 5, 6)
        btnPatLook.Name = "btnPatLook"
        btnPatLook.Size = New Size(50, 50)
        btnPatLook.TabIndex = 64
        btnPatLook.UseVisualStyleBackColor = True
        ' 
        ' cmbSex
        ' 
        cmbSex.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSex.FormattingEnabled = True
        cmbSex.Items.AddRange(New Object() {"Female", "Male", "Indetermined", "Unreported"})
        cmbSex.Location = New Point(227, 194)
        cmbSex.Margin = New Padding(5, 6, 5, 6)
        cmbSex.Name = "cmbSex"
        cmbSex.Size = New Size(116, 33)
        cmbSex.TabIndex = 68
        ' 
        ' txtLName
        ' 
        txtLName.BackColor = Color.White
        txtLName.Location = New Point(287, 98)
        txtLName.Margin = New Padding(5, 6, 5, 6)
        txtLName.MaxLength = 35
        txtLName.Name = "txtLName"
        txtLName.Size = New Size(224, 31)
        txtLName.TabIndex = 65
        ' 
        ' lblHPhone
        ' 
        lblHPhone.ForeColor = Color.DarkBlue
        lblHPhone.Location = New Point(20, 444)
        lblHPhone.Margin = New Padding(5, 0, 5, 0)
        lblHPhone.Name = "lblHPhone"
        lblHPhone.Size = New Size(130, 25)
        lblHPhone.TabIndex = 91
        lblHPhone.Text = "Home Phone"
        ' 
        ' Label29
        ' 
        Label29.ForeColor = Color.Red
        Label29.Location = New Point(287, 63)
        Label29.Margin = New Padding(5, 0, 5, 0)
        Label29.Name = "Label29"
        Label29.Size = New Size(158, 25)
        Label29.TabIndex = 85
        Label29.Text = "Last Name"
        ' 
        ' txtPatAdr1
        ' 
        txtPatAdr1.BackColor = Color.White
        txtPatAdr1.Location = New Point(18, 290)
        txtPatAdr1.Margin = New Padding(5, 6, 5, 6)
        txtPatAdr1.MaxLength = 35
        txtPatAdr1.Name = "txtPatAdr1"
        txtPatAdr1.Size = New Size(229, 31)
        txtPatAdr1.TabIndex = 71
        ' 
        ' Label32
        ' 
        Label32.Location = New Point(595, 162)
        Label32.Margin = New Padding(5, 0, 5, 0)
        Label32.Name = "Label32"
        Label32.Size = New Size(120, 25)
        Label32.TabIndex = 90
        Label32.Text = "SSN"
        ' 
        ' lblAdd1
        ' 
        lblAdd1.ForeColor = Color.DarkBlue
        lblAdd1.Location = New Point(20, 260)
        lblAdd1.Margin = New Padding(5, 0, 5, 0)
        lblAdd1.Name = "lblAdd1"
        lblAdd1.Size = New Size(145, 25)
        lblAdd1.TabIndex = 86
        lblAdd1.Text = "Address 1"
        ' 
        ' txtSSN
        ' 
        txtSSN.Location = New Point(525, 194)
        txtSSN.Margin = New Padding(5, 6, 5, 6)
        txtSSN.Mask = "000-00-0000"
        txtSSN.Name = "txtSSN"
        txtSSN.Size = New Size(142, 31)
        txtSSN.TabIndex = 70
        ' 
        ' txtPatCity
        ' 
        txtPatCity.BackColor = Color.White
        txtPatCity.Location = New Point(462, 290)
        txtPatCity.Margin = New Padding(5, 6, 5, 6)
        txtPatCity.MaxLength = 35
        txtPatCity.Name = "txtPatCity"
        txtPatCity.Size = New Size(206, 31)
        txtPatCity.TabIndex = 73
        ' 
        ' Label31
        ' 
        Label31.Location = New Point(20, 162)
        Label31.Margin = New Padding(5, 0, 5, 0)
        Label31.Name = "Label31"
        Label31.Size = New Size(170, 25)
        Label31.TabIndex = 89
        Label31.Text = "Middle Name"
        ' 
        ' lblCity
        ' 
        lblCity.ForeColor = Color.DarkBlue
        lblCity.Location = New Point(475, 260)
        lblCity.Margin = New Padding(5, 0, 5, 0)
        lblCity.Name = "lblCity"
        lblCity.Size = New Size(117, 25)
        lblCity.TabIndex = 87
        lblCity.Text = "City"
        ' 
        ' txtMName
        ' 
        txtMName.BackColor = Color.White
        txtMName.Location = New Point(18, 196)
        txtMName.Margin = New Padding(5, 6, 5, 6)
        txtMName.MaxLength = 35
        txtMName.Name = "txtMName"
        txtMName.Size = New Size(194, 31)
        txtMName.TabIndex = 67
        ' 
        ' txtPatEmail
        ' 
        txtPatEmail.BackColor = Color.White
        txtPatEmail.Location = New Point(200, 475)
        txtPatEmail.Margin = New Padding(5, 6, 5, 6)
        txtPatEmail.MaxLength = 35
        txtPatEmail.Name = "txtPatEmail"
        txtPatEmail.Size = New Size(311, 31)
        txtPatEmail.TabIndex = 78
        ' 
        ' txtFName
        ' 
        txtFName.BackColor = Color.White
        txtFName.Location = New Point(525, 98)
        txtFName.Margin = New Padding(5, 6, 5, 6)
        txtFName.MaxLength = 35
        txtFName.Name = "txtFName"
        txtFName.Size = New Size(142, 31)
        txtFName.TabIndex = 66
        ' 
        ' Label26
        ' 
        Label26.ForeColor = Color.DarkBlue
        Label26.Location = New Point(203, 444)
        Label26.Margin = New Padding(5, 0, 5, 0)
        Label26.Name = "Label26"
        Label26.Size = New Size(198, 25)
        Label26.TabIndex = 88
        Label26.Text = "Email"
        ' 
        ' tpBilling
        ' 
        tpBilling.Controls.Add(grpPSubs)
        tpBilling.Controls.Add(grpPrimary)
        tpBilling.Controls.Add(Label49)
        tpBilling.Controls.Add(Label48)
        tpBilling.Controls.Add(cmbPrimResp)
        tpBilling.Controls.Add(chkSvcGratis)
        tpBilling.Location = New Point(4, 34)
        tpBilling.Margin = New Padding(5, 6, 5, 6)
        tpBilling.Name = "tpBilling"
        tpBilling.Size = New Size(1087, 658)
        tpBilling.TabIndex = 4
        tpBilling.Text = "Billing"
        tpBilling.UseVisualStyleBackColor = True
        ' 
        ' grpPSubs
        ' 
        grpPSubs.Controls.Add(btnPrev5)
        grpPSubs.Controls.Add(txtPSubLName)
        grpPSubs.Controls.Add(Label57)
        grpPSubs.Controls.Add(txtPSubSSN)
        grpPSubs.Controls.Add(Label59)
        grpPSubs.Controls.Add(txtPSubDOB)
        grpPSubs.Controls.Add(cmbPSubSex)
        grpPSubs.Controls.Add(btnPSubLook)
        grpPSubs.Controls.Add(Label60)
        grpPSubs.Controls.Add(txtPSubCountry)
        grpPSubs.Controls.Add(Label61)
        grpPSubs.Controls.Add(txtPSubEmail)
        grpPSubs.Controls.Add(Label62)
        grpPSubs.Controls.Add(txtPSubPhone)
        grpPSubs.Controls.Add(Label63)
        grpPSubs.Controls.Add(txtPSubZip)
        grpPSubs.Controls.Add(Label64)
        grpPSubs.Controls.Add(txtPSubState)
        grpPSubs.Controls.Add(Label65)
        grpPSubs.Controls.Add(txtPSubCity)
        grpPSubs.Controls.Add(Label66)
        grpPSubs.Controls.Add(txtPSubAdd2)
        grpPSubs.Controls.Add(Label67)
        grpPSubs.Controls.Add(txtPSubAdd1)
        grpPSubs.Controls.Add(Label68)
        grpPSubs.Controls.Add(Label69)
        grpPSubs.Controls.Add(txtPSubMName)
        grpPSubs.Controls.Add(Label70)
        grpPSubs.Controls.Add(txtPSubFName)
        grpPSubs.Controls.Add(Label71)
        grpPSubs.Controls.Add(Label72)
        grpPSubs.Controls.Add(txtPSubID)
        grpPSubs.Location = New Point(32, 338)
        grpPSubs.Margin = New Padding(5, 6, 5, 6)
        grpPSubs.Name = "grpPSubs"
        grpPSubs.Padding = New Padding(5, 6, 5, 6)
        grpPSubs.Size = New Size(1022, 302)
        grpPSubs.TabIndex = 30
        grpPSubs.TabStop = False
        grpPSubs.Text = "Primary Subscriber"
        ' 
        ' btnPrev5
        ' 
        btnPrev5.Location = New Point(887, 233)
        btnPrev5.Margin = New Padding(5, 6, 5, 6)
        btnPrev5.Name = "btnPrev5"
        btnPrev5.Size = New Size(125, 44)
        btnPrev5.TabIndex = 103
        btnPrev5.Text = "Previous"
        btnPrev5.UseVisualStyleBackColor = True
        ' 
        ' txtPSubLName
        ' 
        txtPSubLName.AcceptsReturn = True
        txtPSubLName.Location = New Point(200, 62)
        txtPSubLName.Margin = New Padding(5, 6, 5, 6)
        txtPSubLName.Name = "txtPSubLName"
        txtPSubLName.Size = New Size(149, 31)
        txtPSubLName.TabIndex = 31
        ' 
        ' Label57
        ' 
        Label57.ForeColor = Color.DarkBlue
        Label57.Location = New Point(10, 117)
        Label57.Margin = New Padding(5, 0, 5, 0)
        Label57.Name = "Label57"
        Label57.Size = New Size(100, 25)
        Label57.TabIndex = 93
        Label57.Text = "SSN"
        ' 
        ' txtPSubSSN
        ' 
        txtPSubSSN.Location = New Point(10, 146)
        txtPSubSSN.Margin = New Padding(5, 6, 5, 6)
        txtPSubSSN.Mask = "000-00-0000"
        txtPSubSSN.Name = "txtPSubSSN"
        txtPSubSSN.Size = New Size(174, 31)
        txtPSubSSN.TabIndex = 36
        ' 
        ' Label59
        ' 
        Label59.ForeColor = Color.DarkBlue
        Label59.Location = New Point(638, 31)
        Label59.Margin = New Padding(5, 0, 5, 0)
        Label59.Name = "Label59"
        Label59.Size = New Size(120, 25)
        Label59.TabIndex = 90
        Label59.Text = "Gender"
        ' 
        ' txtPSubDOB
        ' 
        txtPSubDOB.Location = New Point(805, 62)
        txtPSubDOB.Margin = New Padding(5, 6, 5, 6)
        txtPSubDOB.Mask = "00/00/0000"
        txtPSubDOB.Name = "txtPSubDOB"
        txtPSubDOB.Size = New Size(127, 31)
        txtPSubDOB.TabIndex = 35
        txtPSubDOB.ValidatingType = GetType(Date)
        ' 
        ' cmbPSubSex
        ' 
        cmbPSubSex.DropDownStyle = ComboBoxStyle.DropDownList
        cmbPSubSex.FormattingEnabled = True
        cmbPSubSex.Items.AddRange(New Object() {"Female", "Male", "Indetermined", "Unreported"})
        cmbPSubSex.Location = New Point(643, 62)
        cmbPSubSex.Margin = New Padding(5, 6, 5, 6)
        cmbPSubSex.Name = "cmbPSubSex"
        cmbPSubSex.Size = New Size(144, 33)
        cmbPSubSex.TabIndex = 34
        ' 
        ' btnPSubLook
        ' 
        btnPSubLook.Image = CType(resources.GetObject("btnPSubLook.Image"), Image)
        btnPSubLook.Location = New Point(147, 54)
        btnPSubLook.Margin = New Padding(5, 6, 5, 6)
        btnPSubLook.Name = "btnPSubLook"
        btnPSubLook.Size = New Size(43, 50)
        btnPSubLook.TabIndex = 30
        btnPSubLook.UseVisualStyleBackColor = True
        ' 
        ' Label60
        ' 
        Label60.ForeColor = Color.DarkBlue
        Label60.Location = New Point(295, 208)
        Label60.Margin = New Padding(5, 0, 5, 0)
        Label60.Name = "Label60"
        Label60.Size = New Size(117, 25)
        Label60.TabIndex = 86
        Label60.Text = "Country"
        ' 
        ' txtPSubCountry
        ' 
        txtPSubCountry.AcceptsReturn = True
        txtPSubCountry.Location = New Point(292, 238)
        txtPSubCountry.Margin = New Padding(5, 6, 5, 6)
        txtPSubCountry.MaxLength = 35
        txtPSubCountry.Name = "txtPSubCountry"
        txtPSubCountry.Size = New Size(162, 31)
        txtPSubCountry.TabIndex = 42
        ' 
        ' Label61
        ' 
        Label61.ForeColor = Color.DarkBlue
        Label61.Location = New Point(648, 208)
        Label61.Margin = New Padding(5, 0, 5, 0)
        Label61.Name = "Label61"
        Label61.Size = New Size(112, 25)
        Label61.TabIndex = 85
        Label61.Text = "Email Address"
        ' 
        ' txtPSubEmail
        ' 
        txtPSubEmail.AcceptsReturn = True
        txtPSubEmail.Location = New Point(653, 238)
        txtPSubEmail.Margin = New Padding(5, 6, 5, 6)
        txtPSubEmail.MaxLength = 25
        txtPSubEmail.Name = "txtPSubEmail"
        txtPSubEmail.Size = New Size(207, 31)
        txtPSubEmail.TabIndex = 44
        ' 
        ' Label62
        ' 
        Label62.ForeColor = Color.DarkBlue
        Label62.Location = New Point(473, 208)
        Label62.Margin = New Padding(5, 0, 5, 0)
        Label62.Name = "Label62"
        Label62.Size = New Size(160, 25)
        Label62.TabIndex = 84
        Label62.Text = "Home Phone"
        ' 
        ' txtPSubPhone
        ' 
        txtPSubPhone.AcceptsReturn = True
        txtPSubPhone.Location = New Point(467, 238)
        txtPSubPhone.Margin = New Padding(5, 6, 5, 6)
        txtPSubPhone.MaxLength = 25
        txtPSubPhone.Name = "txtPSubPhone"
        txtPSubPhone.Size = New Size(174, 31)
        txtPSubPhone.TabIndex = 43
        ' 
        ' Label63
        ' 
        Label63.ForeColor = Color.DarkBlue
        Label63.Location = New Point(173, 208)
        Label63.Margin = New Padding(5, 0, 5, 0)
        Label63.Name = "Label63"
        Label63.Size = New Size(68, 25)
        Label63.TabIndex = 83
        Label63.Text = "Zip"
        ' 
        ' txtPSubZip
        ' 
        txtPSubZip.AcceptsReturn = True
        txtPSubZip.Location = New Point(173, 238)
        txtPSubZip.Margin = New Padding(5, 6, 5, 6)
        txtPSubZip.MaxLength = 25
        txtPSubZip.Name = "txtPSubZip"
        txtPSubZip.Size = New Size(97, 31)
        txtPSubZip.TabIndex = 41
        ' 
        ' Label64
        ' 
        Label64.ForeColor = Color.DarkBlue
        Label64.Location = New Point(10, 208)
        Label64.Margin = New Padding(5, 0, 5, 0)
        Label64.Name = "Label64"
        Label64.Size = New Size(175, 25)
        Label64.TabIndex = 82
        Label64.Text = "State/Province"
        ' 
        ' txtPSubState
        ' 
        txtPSubState.AcceptsReturn = True
        txtPSubState.Location = New Point(13, 238)
        txtPSubState.Margin = New Padding(5, 6, 5, 6)
        txtPSubState.MaxLength = 35
        txtPSubState.Name = "txtPSubState"
        txtPSubState.Size = New Size(147, 31)
        txtPSubState.TabIndex = 40
        ' 
        ' Label65
        ' 
        Label65.ForeColor = Color.DarkBlue
        Label65.Location = New Point(687, 115)
        Label65.Margin = New Padding(5, 0, 5, 0)
        Label65.Name = "Label65"
        Label65.Size = New Size(103, 25)
        Label65.TabIndex = 81
        Label65.Text = "City"
        ' 
        ' txtPSubCity
        ' 
        txtPSubCity.AcceptsReturn = True
        txtPSubCity.Location = New Point(692, 146)
        txtPSubCity.Margin = New Padding(5, 6, 5, 6)
        txtPSubCity.MaxLength = 35
        txtPSubCity.Name = "txtPSubCity"
        txtPSubCity.Size = New Size(241, 31)
        txtPSubCity.TabIndex = 39
        ' 
        ' Label66
        ' 
        Label66.ForeColor = Color.DarkBlue
        Label66.Location = New Point(433, 115)
        Label66.Margin = New Padding(5, 0, 5, 0)
        Label66.Name = "Label66"
        Label66.Size = New Size(190, 25)
        Label66.TabIndex = 80
        Label66.Text = "Address Line 2"
        ' 
        ' txtPSubAdd2
        ' 
        txtPSubAdd2.AcceptsReturn = True
        txtPSubAdd2.Location = New Point(438, 146)
        txtPSubAdd2.Margin = New Padding(5, 6, 5, 6)
        txtPSubAdd2.MaxLength = 35
        txtPSubAdd2.Name = "txtPSubAdd2"
        txtPSubAdd2.Size = New Size(241, 31)
        txtPSubAdd2.TabIndex = 38
        ' 
        ' Label67
        ' 
        Label67.ForeColor = Color.DarkBlue
        Label67.Location = New Point(192, 115)
        Label67.Margin = New Padding(5, 0, 5, 0)
        Label67.Name = "Label67"
        Label67.Size = New Size(178, 25)
        Label67.TabIndex = 78
        Label67.Text = "Address Line 1"
        ' 
        ' txtPSubAdd1
        ' 
        txtPSubAdd1.AcceptsReturn = True
        txtPSubAdd1.Location = New Point(197, 146)
        txtPSubAdd1.Margin = New Padding(5, 6, 5, 6)
        txtPSubAdd1.MaxLength = 35
        txtPSubAdd1.Name = "txtPSubAdd1"
        txtPSubAdd1.Size = New Size(229, 31)
        txtPSubAdd1.TabIndex = 37
        ' 
        ' Label68
        ' 
        Label68.ForeColor = Color.DarkBlue
        Label68.Location = New Point(800, 31)
        Label68.Margin = New Padding(5, 0, 5, 0)
        Label68.Name = "Label68"
        Label68.Size = New Size(60, 25)
        Label68.TabIndex = 75
        Label68.Text = "D.O.B"
        ' 
        ' Label69
        ' 
        Label69.ForeColor = Color.DarkBlue
        Label69.Location = New Point(522, 31)
        Label69.Margin = New Padding(5, 0, 5, 0)
        Label69.Name = "Label69"
        Label69.Size = New Size(115, 25)
        Label69.TabIndex = 72
        Label69.Text = "Middle Name"
        ' 
        ' txtPSubMName
        ' 
        txtPSubMName.Location = New Point(523, 62)
        txtPSubMName.Margin = New Padding(5, 6, 5, 6)
        txtPSubMName.MaxLength = 35
        txtPSubMName.Name = "txtPSubMName"
        txtPSubMName.Size = New Size(107, 31)
        txtPSubMName.TabIndex = 33
        ' 
        ' Label70
        ' 
        Label70.ForeColor = Color.DarkBlue
        Label70.Location = New Point(357, 31)
        Label70.Margin = New Padding(5, 0, 5, 0)
        Label70.Name = "Label70"
        Label70.Size = New Size(130, 25)
        Label70.TabIndex = 69
        Label70.Text = "First Name"
        ' 
        ' txtPSubFName
        ' 
        txtPSubFName.AcceptsReturn = True
        txtPSubFName.Location = New Point(362, 62)
        txtPSubFName.Margin = New Padding(5, 6, 5, 6)
        txtPSubFName.MaxLength = 35
        txtPSubFName.Name = "txtPSubFName"
        txtPSubFName.Size = New Size(149, 31)
        txtPSubFName.TabIndex = 32
        ' 
        ' Label71
        ' 
        Label71.ForeColor = Color.DarkBlue
        Label71.Location = New Point(200, 31)
        Label71.Margin = New Padding(5, 0, 5, 0)
        Label71.Name = "Label71"
        Label71.Size = New Size(147, 25)
        Label71.TabIndex = 67
        Label71.Text = "Last Name"
        ' 
        ' Label72
        ' 
        Label72.ForeColor = Color.Fuchsia
        Label72.Location = New Point(8, 31)
        Label72.Margin = New Padding(5, 0, 5, 0)
        Label72.Name = "Label72"
        Label72.Size = New Size(133, 25)
        Label72.TabIndex = 64
        Label72.Text = "Subscriber ID"
        ' 
        ' txtPSubID
        ' 
        txtPSubID.AcceptsReturn = True
        txtPSubID.Location = New Point(10, 62)
        txtPSubID.Margin = New Padding(5, 6, 5, 6)
        txtPSubID.MaxLength = 12
        txtPSubID.Name = "txtPSubID"
        txtPSubID.ReadOnly = True
        txtPSubID.Size = New Size(124, 31)
        txtPSubID.TabIndex = 29
        txtPSubID.TabStop = False
        txtPSubID.TextAlign = HorizontalAlignment.Center
        ' 
        ' grpPrimary
        ' 
        grpPrimary.Controls.Add(txtPCopay)
        grpPrimary.Controls.Add(Label58)
        grpPrimary.Controls.Add(Label51)
        grpPrimary.Controls.Add(cmbPRelation)
        grpPrimary.Controls.Add(Label52)
        grpPrimary.Controls.Add(txtPFrom)
        grpPrimary.Controls.Add(btnPIns)
        grpPrimary.Controls.Add(Label53)
        grpPrimary.Controls.Add(cmbPIns)
        grpPrimary.Controls.Add(txtPGroup)
        grpPrimary.Controls.Add(txtPTo)
        grpPrimary.Controls.Add(Label54)
        grpPrimary.Controls.Add(Label55)
        grpPrimary.Controls.Add(txtPInsID)
        grpPrimary.Controls.Add(Label56)
        grpPrimary.Location = New Point(32, 140)
        grpPrimary.Margin = New Padding(5, 6, 5, 6)
        grpPrimary.Name = "grpPrimary"
        grpPrimary.Padding = New Padding(5, 6, 5, 6)
        grpPrimary.Size = New Size(1022, 187)
        grpPrimary.TabIndex = 21
        grpPrimary.TabStop = False
        grpPrimary.Text = "Primary Insurance"
        ' 
        ' txtPCopay
        ' 
        txtPCopay.AcceptsReturn = True
        txtPCopay.Location = New Point(805, 137)
        txtPCopay.Margin = New Padding(5, 6, 5, 6)
        txtPCopay.MaxLength = 6
        txtPCopay.Name = "txtPCopay"
        txtPCopay.Size = New Size(144, 31)
        txtPCopay.TabIndex = 27
        ' 
        ' Label58
        ' 
        Label58.ForeColor = Color.DarkBlue
        Label58.Location = New Point(800, 106)
        Label58.Margin = New Padding(5, 0, 5, 0)
        Label58.Name = "Label58"
        Label58.Size = New Size(112, 25)
        Label58.TabIndex = 69
        Label58.Text = "Copay"
        ' 
        ' Label51
        ' 
        Label51.ForeColor = Color.Fuchsia
        Label51.Location = New Point(590, 106)
        Label51.Margin = New Padding(5, 0, 5, 0)
        Label51.Name = "Label51"
        Label51.Size = New Size(112, 23)
        Label51.TabIndex = 67
        Label51.Text = "Relation"
        ' 
        ' cmbPRelation
        ' 
        cmbPRelation.DropDownStyle = ComboBoxStyle.DropDownList
        cmbPRelation.FormattingEnabled = True
        cmbPRelation.Items.AddRange(New Object() {"Self", "Spouse", "Son/Daughter", "Other Dependent"})
        cmbPRelation.Location = New Point(595, 135)
        cmbPRelation.Margin = New Padding(5, 6, 5, 6)
        cmbPRelation.Name = "cmbPRelation"
        cmbPRelation.Size = New Size(192, 33)
        cmbPRelation.TabIndex = 26
        ' 
        ' Label52
        ' 
        Label52.ForeColor = Color.Fuchsia
        Label52.Location = New Point(10, 21)
        Label52.Margin = New Padding(5, 0, 5, 0)
        Label52.Name = "Label52"
        Label52.Size = New Size(212, 25)
        Label52.TabIndex = 60
        Label52.Text = "Insurance Name"
        ' 
        ' txtPFrom
        ' 
        txtPFrom.Location = New Point(278, 137)
        txtPFrom.Margin = New Padding(5, 6, 5, 6)
        txtPFrom.Mask = "00/00/0000"
        txtPFrom.Name = "txtPFrom"
        txtPFrom.Size = New Size(147, 31)
        txtPFrom.TabIndex = 24
        txtPFrom.ValidatingType = GetType(Date)
        ' 
        ' btnPIns
        ' 
        btnPIns.Image = CType(resources.GetObject("btnPIns.Image"), Image)
        btnPIns.Location = New Point(595, 46)
        btnPIns.Margin = New Padding(5, 6, 5, 6)
        btnPIns.Name = "btnPIns"
        btnPIns.Size = New Size(43, 50)
        btnPIns.TabIndex = 21
        btnPIns.UseVisualStyleBackColor = True
        ' 
        ' Label53
        ' 
        Label53.ForeColor = Color.DarkBlue
        Label53.Location = New Point(287, 106)
        Label53.Margin = New Padding(5, 0, 5, 0)
        Label53.Name = "Label53"
        Label53.Size = New Size(125, 25)
        Label53.TabIndex = 64
        Label53.Text = "Effective From"
        ' 
        ' cmbPIns
        ' 
        cmbPIns.DropDownStyle = ComboBoxStyle.DropDownList
        cmbPIns.FormattingEnabled = True
        cmbPIns.Location = New Point(10, 54)
        cmbPIns.Margin = New Padding(5, 6, 5, 6)
        cmbPIns.Name = "cmbPIns"
        cmbPIns.Size = New Size(572, 33)
        cmbPIns.TabIndex = 20
        ' 
        ' txtPGroup
        ' 
        txtPGroup.AcceptsReturn = True
        txtPGroup.Location = New Point(10, 135)
        txtPGroup.Margin = New Padding(5, 6, 5, 6)
        txtPGroup.MaxLength = 35
        txtPGroup.Name = "txtPGroup"
        txtPGroup.Size = New Size(256, 31)
        txtPGroup.TabIndex = 22
        ' 
        ' txtPTo
        ' 
        txtPTo.Location = New Point(438, 137)
        txtPTo.Margin = New Padding(5, 6, 5, 6)
        txtPTo.Mask = "00/00/0000"
        txtPTo.Name = "txtPTo"
        txtPTo.Size = New Size(144, 31)
        txtPTo.TabIndex = 25
        txtPTo.ValidatingType = GetType(Date)
        ' 
        ' Label54
        ' 
        Label54.ForeColor = Color.DarkBlue
        Label54.Location = New Point(447, 106)
        Label54.Margin = New Padding(5, 0, 5, 0)
        Label54.Name = "Label54"
        Label54.Size = New Size(68, 25)
        Label54.TabIndex = 62
        Label54.Text = "Expires"
        ' 
        ' Label55
        ' 
        Label55.ForeColor = Color.DarkBlue
        Label55.Location = New Point(17, 106)
        Label55.Margin = New Padding(5, 0, 5, 0)
        Label55.Name = "Label55"
        Label55.Size = New Size(170, 25)
        Label55.TabIndex = 59
        Label55.Text = "Group No"
        ' 
        ' txtPInsID
        ' 
        txtPInsID.AcceptsReturn = True
        txtPInsID.Location = New Point(653, 54)
        txtPInsID.Margin = New Padding(5, 6, 5, 6)
        txtPInsID.MaxLength = 35
        txtPInsID.Name = "txtPInsID"
        txtPInsID.Size = New Size(341, 31)
        txtPInsID.TabIndex = 23
        ' 
        ' Label56
        ' 
        Label56.ForeColor = Color.Fuchsia
        Label56.Location = New Point(667, 21)
        Label56.Margin = New Padding(5, 0, 5, 0)
        Label56.Name = "Label56"
        Label56.Size = New Size(197, 25)
        Label56.TabIndex = 61
        Label56.Text = "Policy No"
        ' 
        ' Label49
        ' 
        Label49.ForeColor = Color.Fuchsia
        Label49.Location = New Point(212, 12)
        Label49.Margin = New Padding(5, 0, 5, 0)
        Label49.Name = "Label49"
        Label49.Size = New Size(250, 29)
        Label49.TabIndex = 12
        Label49.Text = "Primary Responsible Payer"
        ' 
        ' Label48
        ' 
        Label48.Location = New Point(35, 12)
        Label48.Margin = New Padding(5, 0, 5, 0)
        Label48.Name = "Label48"
        Label48.Size = New Size(122, 29)
        Label48.TabIndex = 11
        Label48.Text = "Service"
        ' 
        ' cmbPrimResp
        ' 
        cmbPrimResp.DropDownStyle = ComboBoxStyle.DropDownList
        cmbPrimResp.FormattingEnabled = True
        cmbPrimResp.Items.AddRange(New Object() {"Client Billing", "Insurance Billing", "Patient Billing"})
        cmbPrimResp.Location = New Point(205, 56)
        cmbPrimResp.Margin = New Padding(5, 6, 5, 6)
        cmbPrimResp.Name = "cmbPrimResp"
        cmbPrimResp.Size = New Size(492, 33)
        cmbPrimResp.TabIndex = 10
        ' 
        ' chkSvcGratis
        ' 
        chkSvcGratis.Appearance = Appearance.Button
        chkSvcGratis.Location = New Point(32, 48)
        chkSvcGratis.Margin = New Padding(5, 6, 5, 6)
        chkSvcGratis.Name = "chkSvcGratis"
        chkSvcGratis.Size = New Size(143, 52)
        chkSvcGratis.TabIndex = 9
        chkSvcGratis.Text = "Charge"
        chkSvcGratis.TextAlign = ContentAlignment.MiddleCenter
        chkSvcGratis.UseVisualStyleBackColor = True
        ' 
        ' tpOrders
        ' 
        tpOrders.Controls.Add(GroupBox1)
        tpOrders.Controls.Add(Label23)
        tpOrders.Controls.Add(Label22)
        tpOrders.Controls.Add(btnCompLookUp)
        tpOrders.Controls.Add(txtTGPName)
        tpOrders.Controls.Add(txtTGPID)
        tpOrders.Controls.Add(Label20)
        tpOrders.Controls.Add(dgvTGPs)
        tpOrders.Location = New Point(4, 34)
        tpOrders.Margin = New Padding(5, 6, 5, 6)
        tpOrders.Name = "tpOrders"
        tpOrders.Padding = New Padding(5, 6, 5, 6)
        tpOrders.Size = New Size(1087, 658)
        tpOrders.TabIndex = 6
        tpOrders.Text = "Components"
        tpOrders.UseVisualStyleBackColor = True
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(txtMaxCount)
        GroupBox1.Controls.Add(Label10)
        GroupBox1.Controls.Add(btnUpdate)
        GroupBox1.Controls.Add(dtpStartDate)
        GroupBox1.Controls.Add(txtEndDate)
        GroupBox1.Controls.Add(cmbInterval)
        GroupBox1.Controls.Add(Label6)
        GroupBox1.Controls.Add(Label39)
        GroupBox1.Controls.Add(Label5)
        GroupBox1.Controls.Add(txtQty)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.ForeColor = Color.DarkBlue
        GroupBox1.Location = New Point(10, 452)
        GroupBox1.Margin = New Padding(5, 6, 5, 6)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(5, 6, 5, 6)
        GroupBox1.Size = New Size(1062, 160)
        GroupBox1.TabIndex = 36
        GroupBox1.TabStop = False
        GroupBox1.Text = "Schedule Parameters"
        ' 
        ' txtMaxCount
        ' 
        txtMaxCount.Location = New Point(580, 79)
        txtMaxCount.Margin = New Padding(5, 6, 5, 6)
        txtMaxCount.MaxLength = 5
        txtMaxCount.Name = "txtMaxCount"
        txtMaxCount.ReadOnly = True
        txtMaxCount.Size = New Size(159, 31)
        txtMaxCount.TabIndex = 38
        txtMaxCount.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label10
        ' 
        Label10.ForeColor = Color.Fuchsia
        Label10.Location = New Point(752, 42)
        Label10.Margin = New Padding(5, 0, 5, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(138, 31)
        Label10.TabIndex = 37
        Label10.Text = "Expire Date"
        ' 
        ' btnUpdate
        ' 
        btnUpdate.Enabled = False
        btnUpdate.Location = New Point(900, 65)
        btnUpdate.Margin = New Padding(5, 6, 5, 6)
        btnUpdate.Name = "btnUpdate"
        btnUpdate.Size = New Size(145, 60)
        btnUpdate.TabIndex = 36
        btnUpdate.Text = "Schedule"
        btnUpdate.UseVisualStyleBackColor = True
        ' 
        ' dtpStartDate
        ' 
        dtpStartDate.Format = DateTimePickerFormat.Short
        dtpStartDate.Location = New Point(28, 81)
        dtpStartDate.Margin = New Padding(5, 6, 5, 6)
        dtpStartDate.Name = "dtpStartDate"
        dtpStartDate.Size = New Size(172, 31)
        dtpStartDate.TabIndex = 28
        ' 
        ' txtEndDate
        ' 
        txtEndDate.Location = New Point(757, 77)
        txtEndDate.Margin = New Padding(5, 6, 5, 6)
        txtEndDate.Mask = "00/00/0000"
        txtEndDate.Name = "txtEndDate"
        txtEndDate.ReadOnly = True
        txtEndDate.Size = New Size(131, 31)
        txtEndDate.TabIndex = 35
        txtEndDate.ValidatingType = GetType(Date)
        ' 
        ' cmbInterval
        ' 
        cmbInterval.DropDownStyle = ComboBoxStyle.DropDownList
        cmbInterval.FormattingEnabled = True
        cmbInterval.Items.AddRange(New Object() {"DAY", "WEEK", "BIWEEK", "MONTH", "QUARTER", "SEMI-ANNUAL", "ANNUAL"})
        cmbInterval.Location = New Point(225, 79)
        cmbInterval.Margin = New Padding(5, 6, 5, 6)
        cmbInterval.Name = "cmbInterval"
        cmbInterval.Size = New Size(209, 33)
        cmbInterval.TabIndex = 25
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.Red
        Label6.Location = New Point(418, 40)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(135, 31)
        Label6.TabIndex = 34
        Label6.Text = "Interval Count"
        Label6.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label39
        ' 
        Label39.ForeColor = Color.Red
        Label39.Location = New Point(242, 42)
        Label39.Margin = New Padding(5, 0, 5, 0)
        Label39.Name = "Label39"
        Label39.Size = New Size(182, 31)
        Label39.TabIndex = 26
        Label39.Text = "Interval "
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.Fuchsia
        Label5.Location = New Point(580, 40)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(162, 31)
        Label5.TabIndex = 33
        Label5.Text = "Max Count"
        Label5.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtQty
        ' 
        txtQty.Location = New Point(450, 81)
        txtQty.Margin = New Padding(5, 6, 5, 6)
        txtQty.Name = "txtQty"
        txtQty.Size = New Size(76, 31)
        txtQty.TabIndex = 27
        txtQty.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.Red
        Label3.Location = New Point(42, 42)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(162, 31)
        Label3.TabIndex = 31
        Label3.Text = "Start Date"
        ' 
        ' Label23
        ' 
        Label23.ForeColor = Color.DarkBlue
        Label23.Location = New Point(252, 365)
        Label23.Margin = New Padding(5, 0, 5, 0)
        Label23.Name = "Label23"
        Label23.Size = New Size(140, 31)
        Label23.TabIndex = 23
        Label23.Text = "Component Name"
        Label23.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label22
        ' 
        Label22.ForeColor = Color.Red
        Label22.Location = New Point(27, 360)
        Label22.Margin = New Padding(5, 0, 5, 0)
        Label22.Name = "Label22"
        Label22.Size = New Size(147, 31)
        Label22.TabIndex = 22
        Label22.Text = "Component ID"
        Label22.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' btnCompLookUp
        ' 
        btnCompLookUp.Image = CType(resources.GetObject("btnCompLookUp.Image"), Image)
        btnCompLookUp.Location = New Point(183, 396)
        btnCompLookUp.Margin = New Padding(5, 6, 5, 6)
        btnCompLookUp.Name = "btnCompLookUp"
        btnCompLookUp.Size = New Size(42, 44)
        btnCompLookUp.TabIndex = 21
        btnCompLookUp.UseVisualStyleBackColor = True
        ' 
        ' txtTGPName
        ' 
        txtTGPName.Location = New Point(235, 402)
        txtTGPName.Margin = New Padding(5, 6, 5, 6)
        txtTGPName.Name = "txtTGPName"
        txtTGPName.Size = New Size(817, 31)
        txtTGPName.TabIndex = 3
        ' 
        ' txtTGPID
        ' 
        txtTGPID.Location = New Point(27, 402)
        txtTGPID.Margin = New Padding(5, 6, 5, 6)
        txtTGPID.Name = "txtTGPID"
        txtTGPID.Size = New Size(144, 31)
        txtTGPID.TabIndex = 2
        txtTGPID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label20
        ' 
        Label20.ForeColor = Color.Red
        Label20.Location = New Point(40, 13)
        Label20.Margin = New Padding(5, 0, 5, 0)
        Label20.Name = "Label20"
        Label20.Size = New Size(265, 31)
        Label20.TabIndex = 1
        Label20.Text = "Scheduled Components"
        Label20.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' dgvTGPs
        ' 
        dgvTGPs.AllowUserToAddRows = False
        dgvTGPs.AllowUserToDeleteRows = False
        DataGridViewCellStyle5.BackColor = Color.MintCream
        dgvTGPs.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        dgvTGPs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvTGPs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvTGPs.Columns.AddRange(New DataGridViewColumn() {Del, Comp_ID, CompName, Starts, Frequency, QTY, MaxCount, EndDate})
        dgvTGPs.Location = New Point(27, 50)
        dgvTGPs.Margin = New Padding(5, 6, 5, 6)
        dgvTGPs.Name = "dgvTGPs"
        dgvTGPs.ReadOnly = True
        dgvTGPs.RowHeadersVisible = False
        dgvTGPs.RowHeadersWidth = 62
        dgvTGPs.Size = New Size(1028, 281)
        dgvTGPs.TabIndex = 0
        ' 
        ' Del
        ' 
        Del.FillWeight = 30F
        Del.HeaderText = ""
        Del.Image = CType(resources.GetObject("Del.Image"), Image)
        Del.MinimumWidth = 8
        Del.Name = "Del"
        Del.ReadOnly = True
        Del.Resizable = DataGridViewTriState.True
        ' 
        ' Comp_ID
        ' 
        Comp_ID.FillWeight = 80F
        Comp_ID.HeaderText = "Comp ID"
        Comp_ID.MinimumWidth = 8
        Comp_ID.Name = "Comp_ID"
        Comp_ID.ReadOnly = True
        ' 
        ' CompName
        ' 
        CompName.FillWeight = 140F
        CompName.HeaderText = "Component"
        CompName.MinimumWidth = 8
        CompName.Name = "CompName"
        CompName.ReadOnly = True
        ' 
        ' Starts
        ' 
        Starts.FillWeight = 80F
        Starts.HeaderText = "Start Date"
        Starts.MinimumWidth = 8
        Starts.Name = "Starts"
        Starts.ReadOnly = True
        Starts.Resizable = DataGridViewTriState.True
        Starts.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Frequency
        ' 
        Frequency.FillWeight = 90F
        Frequency.HeaderText = "Frequency"
        Frequency.MinimumWidth = 8
        Frequency.Name = "Frequency"
        Frequency.ReadOnly = True
        ' 
        ' QTY
        ' 
        QTY.FillWeight = 36F
        QTY.HeaderText = "QTY"
        QTY.MinimumWidth = 8
        QTY.Name = "QTY"
        QTY.ReadOnly = True
        QTY.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' MaxCount
        ' 
        MaxCount.FillWeight = 60F
        MaxCount.HeaderText = "Max"
        MaxCount.MinimumWidth = 8
        MaxCount.Name = "MaxCount"
        MaxCount.ReadOnly = True
        MaxCount.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' EndDate
        ' 
        EndDate.FillWeight = 80F
        EndDate.HeaderText = "Expiring"
        EndDate.MinimumWidth = 8
        EndDate.Name = "EndDate"
        EndDate.ReadOnly = True
        ' 
        ' lblGeneral
        ' 
        lblGeneral.BackColor = Color.PeachPuff
        lblGeneral.BorderStyle = BorderStyle.FixedSingle
        lblGeneral.Font = New Font("Microsoft Sans Serif", 7F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblGeneral.Location = New Point(185, 71)
        lblGeneral.Margin = New Padding(5, 0, 5, 0)
        lblGeneral.Name = "lblGeneral"
        lblGeneral.Size = New Size(155, 31)
        lblGeneral.TabIndex = 28
        lblGeneral.Text = "General"
        lblGeneral.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' frmOrders
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1120, 835)
        Controls.Add(lblGeneral)
        Controls.Add(TC)
        Controls.Add(lblOrders)
        Controls.Add(lblBilling)
        Controls.Add(lblPatient)
        Controls.Add(lblOrderer)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MaximumSize = New Size(1142, 891)
        MinimizeBox = False
        MinimumSize = New Size(1142, 891)
        Name = "frmOrders"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Order Management"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        TC.ResumeLayout(False)
        tpGeneral.ResumeLayout(False)
        tpGeneral.PerformLayout()
        tpProvider.ResumeLayout(False)
        tpProvider.PerformLayout()
        tpPatient.ResumeLayout(False)
        tpPatient.PerformLayout()
        CType(dgvDxs, ComponentModel.ISupportInitialize).EndInit()
        tpBilling.ResumeLayout(False)
        grpPSubs.ResumeLayout(False)
        grpPSubs.PerformLayout()
        grpPrimary.ResumeLayout(False)
        grpPrimary.PerformLayout()
        tpOrders.ResumeLayout(False)
        tpOrders.PerformLayout()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        CType(dgvTGPs, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblOrders As System.Windows.Forms.Label
    Friend WithEvents lblBilling As System.Windows.Forms.Label
    Friend WithEvents lblPatient As System.Windows.Forms.Label
    Friend WithEvents lblOrderer As System.Windows.Forms.Label
    Friend WithEvents TC As System.Windows.Forms.TabControl
    Friend WithEvents tpProvider As System.Windows.Forms.TabPage
    Friend WithEvents tpPatient As System.Windows.Forms.TabPage
    Friend WithEvents tpBilling As System.Windows.Forms.TabPage
    Friend WithEvents tpGeneral As System.Windows.Forms.TabPage
    Friend WithEvents lblGeneral As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnOrderLook As System.Windows.Forms.Button
    Friend WithEvents txtOrderID As System.Windows.Forms.TextBox
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents dtpOrderDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label96 As System.Windows.Forms.Label
    Friend WithEvents txtProvEmail As System.Windows.Forms.TextBox
    Friend WithEvents lstProviders As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label95 As System.Windows.Forms.Label
    Friend WithEvents btnRemProv As System.Windows.Forms.Button
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtCountry As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtOrdCSZ As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtOrdAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtOrdName As System.Windows.Forms.TextBox
    Friend WithEvents btnOrdLookup As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtOrdID As System.Windows.Forms.TextBox
    Friend WithEvents chkNeedFast As System.Windows.Forms.CheckBox
    Friend WithEvents Label100 As System.Windows.Forms.Label
    Friend WithEvents lblZip As System.Windows.Forms.Label
    Friend WithEvents lblState As System.Windows.Forms.Label
    Friend WithEvents txtPatCountry As System.Windows.Forms.TextBox
    Friend WithEvents txtPatZip As System.Windows.Forms.TextBox
    Friend WithEvents txtPatState As System.Windows.Forms.TextBox
    Friend WithEvents Label97 As System.Windows.Forms.Label
    Friend WithEvents txtPatAdr2 As System.Windows.Forms.TextBox
    Friend WithEvents btnRemDxAll As System.Windows.Forms.Button
    Friend WithEvents btnRemPat As System.Windows.Forms.Button
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents txtPatientID As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtDOB As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnPatLook As System.Windows.Forms.Button
    Friend WithEvents cmbSex As System.Windows.Forms.ComboBox
    Friend WithEvents txtLName As System.Windows.Forms.TextBox
    Friend WithEvents lblHPhone As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtPatAdr1 As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents lblAdd1 As System.Windows.Forms.Label
    Friend WithEvents txtSSN As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtPatCity As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents lblCity As System.Windows.Forms.Label
    Friend WithEvents txtMName As System.Windows.Forms.TextBox
    Friend WithEvents txtPatEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtFName As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents cmbPrimResp As System.Windows.Forms.ComboBox
    Friend WithEvents chkSvcGratis As System.Windows.Forms.CheckBox
    Friend WithEvents grpPrimary As System.Windows.Forms.GroupBox
    Friend WithEvents txtPCopay As System.Windows.Forms.TextBox
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents cmbPRelation As System.Windows.Forms.ComboBox
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents txtPFrom As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnPIns As System.Windows.Forms.Button
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents cmbPIns As System.Windows.Forms.ComboBox
    Friend WithEvents txtPGroup As System.Windows.Forms.TextBox
    Friend WithEvents txtPTo As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents txtPInsID As System.Windows.Forms.TextBox
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents grpPSubs As System.Windows.Forms.GroupBox
    Friend WithEvents txtPSubLName As System.Windows.Forms.TextBox
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents txtPSubSSN As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents txtPSubDOB As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cmbPSubSex As System.Windows.Forms.ComboBox
    Friend WithEvents btnPSubLook As System.Windows.Forms.Button
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents txtPSubCountry As System.Windows.Forms.TextBox
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents txtPSubEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents txtPSubPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents txtPSubZip As System.Windows.Forms.TextBox
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents txtPSubState As System.Windows.Forms.TextBox
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents txtPSubCity As System.Windows.Forms.TextBox
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents txtPSubAdd2 As System.Windows.Forms.TextBox
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents txtPSubAdd1 As System.Windows.Forms.TextBox
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents txtPSubMName As System.Windows.Forms.TextBox
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents txtPSubFName As System.Windows.Forms.TextBox
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents txtPSubID As System.Windows.Forms.TextBox
    Friend WithEvents btnNext1 As System.Windows.Forms.Button
    Friend WithEvents btnPrev2 As System.Windows.Forms.Button
    Friend WithEvents btnNext2 As System.Windows.Forms.Button
    Friend WithEvents btnPrev3 As System.Windows.Forms.Button
    Friend WithEvents btnNext3 As System.Windows.Forms.Button
    Friend WithEvents btnPrev5 As System.Windows.Forms.Button
    Friend WithEvents lblEMRReqd As System.Windows.Forms.Label
    Friend WithEvents txtEMRNo As System.Windows.Forms.TextBox
    Friend WithEvents txtOrdFax As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtOrdPhone As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtPatHPhone As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnPatUpdate As System.Windows.Forms.Button
    Friend WithEvents tpOrders As System.Windows.Forms.TabPage
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents dgvTGPs As System.Windows.Forms.DataGridView
    Friend WithEvents txtTGPID As System.Windows.Forms.TextBox
    Friend WithEvents btnCompLookUp As System.Windows.Forms.Button
    Friend WithEvents txtTGPName As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents cmbInterval As System.Windows.Forms.ComboBox
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtEndDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents txtAgencyAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtAgencyPhone As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtAgency As System.Windows.Forms.TextBox
    Friend WithEvents btnAgencyLookUp As System.Windows.Forms.Button
    Friend WithEvents txtAgencyID As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents txtAgencyContact As System.Windows.Forms.TextBox
    Friend WithEvents chkInfiniteTimed As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtWorkCmnt As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents chklstDays As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents txtMaxCount As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbPhlebLoc As System.Windows.Forms.ComboBox
    Friend WithEvents dgvDxs As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Lookup As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents Del As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents Comp_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CompName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Starts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Frequency As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MaxCount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EndDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblDxs As System.Windows.Forms.Label
    Friend WithEvents dtpDischargeDate As DateTimePicker
    Friend WithEvents lblClearDates As Label
End Class
