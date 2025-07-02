<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBillingEdit
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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBillingEdit))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        cmbBillType = New ToolStripComboBox()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnSave = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        btnHelp = New ToolStripButton()
        GroupBox2 = New GroupBox()
        btnFirst = New Button()
        btnPrevious = New Button()
        btnNext = New Button()
        btnLast = New Button()
        txtNavStatus = New TextBox()
        Label6 = New Label()
        txtAccessionID = New TextBox()
        Label7 = New Label()
        txtSvcDate = New MaskedTextBox()
        Label8 = New Label()
        chkIsGratis = New CheckBox()
        chkBillNow = New CheckBox()
        TabControl1 = New TabControl()
        tpClient = New TabPage()
        Label57 = New Label()
        txtNPI = New TextBox()
        txtProvFax = New MaskedTextBox()
        txtProvPhone = New MaskedTextBox()
        lstAttending = New CheckedListBox()
        chkProvContract = New CheckBox()
        Label38 = New Label()
        cmbProvPrice = New ComboBox()
        Label37 = New Label()
        Label36 = New Label()
        Label35 = New Label()
        Label34 = New Label()
        Label33 = New Label()
        txtProviderAddress = New TextBox()
        txtProviderName = New TextBox()
        btnProviderLook = New Button()
        chkClientBill = New CheckBox()
        Label13 = New Label()
        Label12 = New Label()
        txtProviderID = New TextBox()
        tpThirdParty = New TabPage()
        gbTP = New GroupBox()
        chkTPContract = New CheckBox()
        Label39 = New Label()
        cmbTPPrice = New ComboBox()
        txtInsDOB = New MaskedTextBox()
        pctInsSex = New PictureBox()
        Label24 = New Label()
        txtInsSex = New TextBox()
        Label23 = New Label()
        txtInsAddress = New TextBox()
        Label22 = New Label()
        txtInsName = New TextBox()
        Label21 = New Label()
        btnInsLookup = New Button()
        Label20 = New Label()
        txtInsuredID = New TextBox()
        cmbRelation = New ComboBox()
        Label19 = New Label()
        txtCopay = New TextBox()
        Label18 = New Label()
        txtGroup = New TextBox()
        Label17 = New Label()
        txtPolicy = New TextBox()
        btnPayers = New Button()
        Label16 = New Label()
        cmbPayer = New ComboBox()
        chkTPBill = New CheckBox()
        Label14 = New Label()
        SecondaryIns = New TabPage()
        grpSSubs = New GroupBox()
        Label55 = New Label()
        Label58 = New Label()
        txtSSubEmployer = New TextBox()
        Label59 = New Label()
        Label60 = New Label()
        txtSSubCell = New TextBox()
        txtSSubSSN = New MaskedTextBox()
        Label61 = New Label()
        txtSSubDOB = New MaskedTextBox()
        cmbSSubSex = New ComboBox()
        btnSSubLook = New Button()
        Label62 = New Label()
        txtSSubCountry = New TextBox()
        Label63 = New Label()
        txtSSubEmail = New TextBox()
        Label64 = New Label()
        txtSSubWPhone = New TextBox()
        txtSSubHPhone = New TextBox()
        Label65 = New Label()
        Label67 = New Label()
        txtSSubZip = New TextBox()
        Label74 = New Label()
        txtSSubState = New TextBox()
        Label75 = New Label()
        txtSSubCity = New TextBox()
        Label76 = New Label()
        txtSSubAdd2 = New TextBox()
        Label77 = New Label()
        txtSSubAdd1 = New TextBox()
        Label78 = New Label()
        txtSSubMName = New TextBox()
        Label79 = New Label()
        txtSSubFName = New TextBox()
        Label80 = New Label()
        Label81 = New Label()
        txtSSubLName = New TextBox()
        Label83 = New Label()
        txtSSubID = New TextBox()
        GroupBox3 = New GroupBox()
        conractS = New CheckBox()
        txtSCopay = New TextBox()
        Label66 = New Label()
        Label68 = New Label()
        cmbSRelation = New ComboBox()
        Label69 = New Label()
        txtSFrom = New MaskedTextBox()
        txtSTo = New MaskedTextBox()
        txtSGroup = New TextBox()
        Label70 = New Label()
        cmbSIns = New ComboBox()
        txtSPolicy = New TextBox()
        Label71 = New Label()
        Label72 = New Label()
        Label73 = New Label()
        tpPatient = New TabPage()
        Button1 = New Button()
        Label52 = New Label()
        Label51 = New Label()
        Label50 = New Label()
        Label49 = New Label()
        Label48 = New Label()
        Label47 = New Label()
        Label3 = New Label()
        txtPatState = New TextBox()
        txtPatHPhone = New MaskedTextBox()
        txtPatCell = New MaskedTextBox()
        txtPatZip = New TextBox()
        txtPatCity = New TextBox()
        txtPatAdd2 = New TextBox()
        txtPatMName = New TextBox()
        txtPatFName = New TextBox()
        Label40 = New Label()
        cmbPatPrice = New ComboBox()
        Label32 = New Label()
        txtPatientID = New TextBox()
        txtPatSex = New TextBox()
        Label31 = New Label()
        Label30 = New Label()
        txtPatEmail = New TextBox()
        Label29 = New Label()
        Label28 = New Label()
        txtPatSSN = New MaskedTextBox()
        btnPatLook = New Button()
        txtPatDOB = New MaskedTextBox()
        pctPatSex = New PictureBox()
        Label25 = New Label()
        Label26 = New Label()
        txtPatAdd1 = New TextBox()
        Label27 = New Label()
        txtPatLName = New TextBox()
        chkPatientBill = New CheckBox()
        Label15 = New Label()
        tpCharges = New TabPage()
        dgvCharges = New DataGridView()
        DEL = New DataGridViewImageColumn()
        TGPID = New DataGridViewTextBoxColumn()
        tgpLook = New DataGridViewImageColumn()
        TGPName = New DataGridViewTextBoxColumn()
        CPT = New DataGridViewTextBoxColumn()
        Dx = New DataGridViewTextBoxColumn()
        M1 = New DataGridViewTextBoxColumn()
        M2 = New DataGridViewTextBoxColumn()
        M3 = New DataGridViewTextBoxColumn()
        M4 = New DataGridViewTextBoxColumn()
        Price = New DataGridViewTextBoxColumn()
        Unit = New DataGridViewTextBoxColumn()
        Extend = New DataGridViewTextBoxColumn()
        POS = New DataGridViewTextBoxColumn()
        STATUS = New DataGridViewTextBoxColumn()
        FN = New DataGridViewTextBoxColumn()
        BillDate = New DataGridViewTextBoxColumn()
        Biller = New DataGridViewTextBoxColumn()
        DGVICD9s = New DataGridView()
        SNo = New DataGridViewTextBoxColumn()
        ICD9 = New DataGridViewTextBoxColumn()
        LookUp = New DataGridViewImageColumn()
        Label10 = New Label()
        txtBCharges = New TextBox()
        Label11 = New Label()
        chkBillDate = New CheckBox()
        Label41 = New Label()
        txtDueDays = New TextBox()
        Label42 = New Label()
        txtDueDate = New MaskedTextBox()
        btnDxSync = New Button()
        ToolTip1 = New ToolTip(components)
        orgChargID = New TextBox()
        chkIsBilled = New CheckBox()
        Label44 = New Label()
        txtStatus = New TextBox()
        txtTax = New TextBox()
        txtTerm = New TextBox()
        Label45 = New Label()
        Label46 = New Label()
        btnHistory = New Button()
        txtBillDate = New MaskedTextBox()
        Label53 = New Label()
        txtUCharges = New TextBox()
        Label54 = New Label()
        chkRev = New CheckBox()
        Label56 = New Label()
        cmbInvoices = New ComboBox()
        Label1 = New Label()
        txtBox19 = New TextBox()
        chkECC = New CheckBox()
        dgvComments = New DataGridView()
        Dated = New DataGridViewTextBoxColumn()
        ABP = New DataGridViewTextBoxColumn()
        Cmnt = New DataGridViewTextBoxColumn()
        User = New DataGridViewTextBoxColumn()
        lblFrom = New Label()
        txtAccFrom = New TextBox()
        btnLoad = New Button()
        lblTo = New Label()
        Label4 = New Label()
        txtAccTo = New TextBox()
        Label5 = New Label()
        cmbABU = New ComboBox()
        Label43 = New Label()
        lstTargets = New CheckedListBox()
        btnDesel = New Button()
        btnSel = New Button()
        btnTarget = New Button()
        chkAI = New CheckBox()
        GroupBox1 = New GroupBox()
        dtpDateTo = New DateTimePicker()
        lblClearDates = New Label()
        dtpDateFrom = New DateTimePicker()
        lblEligibility = New Button()
        btnEECC = New Button()
        dgvDiscrete = New DataGridView()
        Discrete = New DataGridViewTextBoxColumn()
        loading = New Label()
        btnBillRev = New Button()
        txtPreAuth = New TextBox()
        Label2 = New Label()
        VoidClaim = New CheckBox()
        Corrected = New CheckBox()
        Label9 = New Label()
        ToolStrip1.SuspendLayout()
        GroupBox2.SuspendLayout()
        TabControl1.SuspendLayout()
        tpClient.SuspendLayout()
        tpThirdParty.SuspendLayout()
        gbTP.SuspendLayout()
        CType(pctInsSex, ComponentModel.ISupportInitialize).BeginInit()
        SecondaryIns.SuspendLayout()
        grpSSubs.SuspendLayout()
        GroupBox3.SuspendLayout()
        tpPatient.SuspendLayout()
        CType(pctPatSex, ComponentModel.ISupportInitialize).BeginInit()
        tpCharges.SuspendLayout()
        CType(dgvCharges, ComponentModel.ISupportInitialize).BeginInit()
        CType(DGVICD9s, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvComments, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox1.SuspendLayout()
        CType(dgvDiscrete, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(20, 20)
        ToolStrip1.Items.AddRange(New ToolStripItem() {cmbBillType, ToolStripSeparator1, btnSave, ToolStripSeparator3, btnCancel, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(1365, 34)
        ToolStrip1.TabIndex = 3
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' cmbBillType
        ' 
        cmbBillType.AutoSize = False
        cmbBillType.DropDownStyle = ComboBoxStyle.DropDownList
        cmbBillType.Items.AddRange(New Object() {"Client Billing", "Insurance Billing", "Patient Billing"})
        cmbBillType.Name = "cmbBillType"
        cmbBillType.Size = New Size(199, 33)
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
        btnSave.Size = New Size(146, 29)
        btnSave.Text = "Save Changes"
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
        btnCancel.Size = New Size(87, 29)
        btnCancel.Text = "Cancel"
        ' 
        ' btnHelp
        ' 
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.ImageTransparentColor = Color.Magenta
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(73, 29)
        btnHelp.Text = "Help"
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(btnFirst)
        GroupBox2.Controls.Add(btnPrevious)
        GroupBox2.Controls.Add(btnNext)
        GroupBox2.Controls.Add(btnLast)
        GroupBox2.Controls.Add(txtNavStatus)
        GroupBox2.Location = New Point(924, 309)
        GroupBox2.Margin = New Padding(5, 6, 5, 6)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Padding = New Padding(5, 6, 5, 6)
        GroupBox2.Size = New Size(421, 103)
        GroupBox2.TabIndex = 5
        GroupBox2.TabStop = False
        GroupBox2.Text = "Navigation"
        ' 
        ' btnFirst
        ' 
        btnFirst.Enabled = False
        btnFirst.Image = CType(resources.GetObject("btnFirst.Image"), Image)
        btnFirst.Location = New Point(10, 34)
        btnFirst.Margin = New Padding(5, 6, 5, 6)
        btnFirst.Name = "btnFirst"
        btnFirst.Size = New Size(50, 50)
        btnFirst.TabIndex = 15
        btnFirst.UseVisualStyleBackColor = True
        ' 
        ' btnPrevious
        ' 
        btnPrevious.Enabled = False
        btnPrevious.Image = CType(resources.GetObject("btnPrevious.Image"), Image)
        btnPrevious.Location = New Point(70, 36)
        btnPrevious.Margin = New Padding(5, 6, 5, 6)
        btnPrevious.Name = "btnPrevious"
        btnPrevious.Size = New Size(49, 50)
        btnPrevious.TabIndex = 14
        btnPrevious.UseVisualStyleBackColor = True
        ' 
        ' btnNext
        ' 
        btnNext.Enabled = False
        btnNext.Image = CType(resources.GetObject("btnNext.Image"), Image)
        btnNext.Location = New Point(291, 33)
        btnNext.Margin = New Padding(5, 6, 5, 6)
        btnNext.Name = "btnNext"
        btnNext.Size = New Size(54, 50)
        btnNext.TabIndex = 12
        btnNext.UseVisualStyleBackColor = True
        ' 
        ' btnLast
        ' 
        btnLast.Enabled = False
        btnLast.Image = CType(resources.GetObject("btnLast.Image"), Image)
        btnLast.Location = New Point(355, 31)
        btnLast.Margin = New Padding(5, 6, 5, 6)
        btnLast.Name = "btnLast"
        btnLast.Size = New Size(55, 50)
        btnLast.TabIndex = 13
        btnLast.UseVisualStyleBackColor = True
        ' 
        ' txtNavStatus
        ' 
        txtNavStatus.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtNavStatus.Location = New Point(129, 44)
        txtNavStatus.Margin = New Padding(5, 6, 5, 6)
        txtNavStatus.Name = "txtNavStatus"
        txtNavStatus.ReadOnly = True
        txtNavStatus.Size = New Size(145, 26)
        txtNavStatus.TabIndex = 8
        txtNavStatus.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.DarkBlue
        Label6.Location = New Point(25, 323)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(134, 25)
        Label6.TabIndex = 9
        Label6.Text = "Accession From"
        ' 
        ' txtAccessionID
        ' 
        txtAccessionID.Location = New Point(21, 361)
        txtAccessionID.Margin = New Padding(5, 6, 5, 6)
        txtAccessionID.Name = "txtAccessionID"
        txtAccessionID.ReadOnly = True
        txtAccessionID.Size = New Size(134, 31)
        txtAccessionID.TabIndex = 16
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.DarkBlue
        Label7.Location = New Point(36, 428)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(134, 25)
        Label7.TabIndex = 11
        Label7.Text = "Billing Date"
        ' 
        ' txtSvcDate
        ' 
        txtSvcDate.Location = New Point(179, 361)
        txtSvcDate.Margin = New Padding(5, 6, 5, 6)
        txtSvcDate.Mask = "00/00/0000"
        txtSvcDate.Name = "txtSvcDate"
        txtSvcDate.ReadOnly = True
        txtSvcDate.Size = New Size(110, 31)
        txtSvcDate.TabIndex = 13
        txtSvcDate.ValidatingType = GetType(Date)
        ' 
        ' Label8
        ' 
        Label8.ForeColor = Color.DarkBlue
        Label8.Location = New Point(174, 323)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(124, 25)
        Label8.TabIndex = 12
        Label8.Text = "Svc Dated"
        ' 
        ' chkIsGratis
        ' 
        chkIsGratis.Appearance = Appearance.Button
        chkIsGratis.Location = New Point(475, 358)
        chkIsGratis.Margin = New Padding(5, 6, 5, 6)
        chkIsGratis.Name = "chkIsGratis"
        chkIsGratis.Size = New Size(101, 42)
        chkIsGratis.TabIndex = 17
        chkIsGratis.Text = "Charge"
        chkIsGratis.TextAlign = ContentAlignment.MiddleCenter
        chkIsGratis.UseVisualStyleBackColor = True
        ' 
        ' chkBillNow
        ' 
        chkBillNow.Location = New Point(1104, 425)
        chkBillNow.Margin = New Padding(5, 6, 5, 6)
        chkBillNow.Name = "chkBillNow"
        chkBillNow.Size = New Size(121, 41)
        chkBillNow.TabIndex = 30
        chkBillNow.Text = "Bill Now"
        chkBillNow.UseVisualStyleBackColor = True
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(tpClient)
        TabControl1.Controls.Add(tpThirdParty)
        TabControl1.Controls.Add(SecondaryIns)
        TabControl1.Controls.Add(tpPatient)
        TabControl1.Controls.Add(tpCharges)
        TabControl1.Location = New Point(275, 609)
        TabControl1.Margin = New Padding(5, 6, 5, 6)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(1070, 503)
        TabControl1.SizeMode = TabSizeMode.Fixed
        TabControl1.TabIndex = 24
        ' 
        ' tpClient
        ' 
        tpClient.Controls.Add(Label57)
        tpClient.Controls.Add(txtNPI)
        tpClient.Controls.Add(txtProvFax)
        tpClient.Controls.Add(txtProvPhone)
        tpClient.Controls.Add(lstAttending)
        tpClient.Controls.Add(chkProvContract)
        tpClient.Controls.Add(Label38)
        tpClient.Controls.Add(cmbProvPrice)
        tpClient.Controls.Add(Label37)
        tpClient.Controls.Add(Label36)
        tpClient.Controls.Add(Label35)
        tpClient.Controls.Add(Label34)
        tpClient.Controls.Add(Label33)
        tpClient.Controls.Add(txtProviderAddress)
        tpClient.Controls.Add(txtProviderName)
        tpClient.Controls.Add(btnProviderLook)
        tpClient.Controls.Add(chkClientBill)
        tpClient.Controls.Add(Label13)
        tpClient.Controls.Add(Label12)
        tpClient.Controls.Add(txtProviderID)
        tpClient.Location = New Point(4, 34)
        tpClient.Margin = New Padding(5, 6, 5, 6)
        tpClient.Name = "tpClient"
        tpClient.Padding = New Padding(5, 6, 5, 6)
        tpClient.Size = New Size(1062, 465)
        tpClient.TabIndex = 1
        tpClient.Text = "Client"
        tpClient.UseVisualStyleBackColor = True
        ' 
        ' Label57
        ' 
        Label57.ForeColor = Color.DarkBlue
        Label57.Location = New Point(351, 198)
        Label57.Margin = New Padding(5, 0, 5, 0)
        Label57.Name = "Label57"
        Label57.Size = New Size(75, 25)
        Label57.TabIndex = 66
        Label57.Text = "NPI"
        Label57.TextAlign = ContentAlignment.TopRight
        ' 
        ' txtNPI
        ' 
        txtNPI.Location = New Point(436, 192)
        txtNPI.Margin = New Padding(5, 6, 5, 6)
        txtNPI.Name = "txtNPI"
        txtNPI.ReadOnly = True
        txtNPI.Size = New Size(159, 31)
        txtNPI.TabIndex = 65
        ' 
        ' txtProvFax
        ' 
        txtProvFax.Location = New Point(405, 347)
        txtProvFax.Margin = New Padding(5, 6, 5, 6)
        txtProvFax.Name = "txtProvFax"
        txtProvFax.ReadOnly = True
        txtProvFax.Size = New Size(190, 31)
        txtProvFax.TabIndex = 64
        ' 
        ' txtProvPhone
        ' 
        txtProvPhone.Location = New Point(40, 347)
        txtProvPhone.Margin = New Padding(5, 6, 5, 6)
        txtProvPhone.Name = "txtProvPhone"
        txtProvPhone.ReadOnly = True
        txtProvPhone.Size = New Size(190, 31)
        txtProvPhone.TabIndex = 63
        ' 
        ' lstAttending
        ' 
        lstAttending.FormattingEnabled = True
        lstAttending.Location = New Point(624, 134)
        lstAttending.Margin = New Padding(5, 6, 5, 6)
        lstAttending.Name = "lstAttending"
        lstAttending.Size = New Size(388, 200)
        lstAttending.TabIndex = 62
        ' 
        ' chkProvContract
        ' 
        chkProvContract.ForeColor = Color.DarkBlue
        chkProvContract.Location = New Point(351, 36)
        chkProvContract.Margin = New Padding(5, 6, 5, 6)
        chkProvContract.Name = "chkProvContract"
        chkProvContract.Size = New Size(111, 41)
        chkProvContract.TabIndex = 17
        chkProvContract.Text = "Contract"
        chkProvContract.UseVisualStyleBackColor = True
        ' 
        ' Label38
        ' 
        Label38.ForeColor = Color.DarkBlue
        Label38.Location = New Point(655, 56)
        Label38.Margin = New Padding(5, 0, 5, 0)
        Label38.Name = "Label38"
        Label38.Size = New Size(106, 25)
        Label38.TabIndex = 61
        Label38.Text = "Price Level"
        Label38.TextAlign = ContentAlignment.TopRight
        ' 
        ' cmbProvPrice
        ' 
        cmbProvPrice.DropDownStyle = ComboBoxStyle.DropDownList
        cmbProvPrice.FormattingEnabled = True
        cmbProvPrice.Items.AddRange(New Object() {"List Price", "Level 1", "Level 2", "Level 3", "Level 4", "Level 5", "Level 6", "Level 7", "Level 8", "Level 9"})
        cmbProvPrice.Location = New Point(785, 50)
        cmbProvPrice.Margin = New Padding(5, 6, 5, 6)
        cmbProvPrice.Name = "cmbProvPrice"
        cmbProvPrice.Size = New Size(225, 33)
        cmbProvPrice.TabIndex = 18
        ' 
        ' Label37
        ' 
        Label37.ForeColor = Color.DarkBlue
        Label37.Location = New Point(619, 97)
        Label37.Margin = New Padding(5, 0, 5, 0)
        Label37.Name = "Label37"
        Label37.Size = New Size(115, 25)
        Label37.TabIndex = 42
        Label37.Text = "Attending"
        ' 
        ' Label36
        ' 
        Label36.ForeColor = Color.DarkBlue
        Label36.Location = New Point(254, 97)
        Label36.Margin = New Padding(5, 0, 5, 0)
        Label36.Name = "Label36"
        Label36.Size = New Size(186, 25)
        Label36.TabIndex = 40
        Label36.Text = "Ordering Provider"
        ' 
        ' Label35
        ' 
        Label35.ForeColor = Color.DarkBlue
        Label35.Location = New Point(401, 316)
        Label35.Margin = New Padding(5, 0, 5, 0)
        Label35.Name = "Label35"
        Label35.Size = New Size(115, 25)
        Label35.TabIndex = 38
        Label35.Text = "Fax"
        ' 
        ' Label34
        ' 
        Label34.ForeColor = Color.DarkBlue
        Label34.Location = New Point(41, 316)
        Label34.Margin = New Padding(5, 0, 5, 0)
        Label34.Name = "Label34"
        Label34.Size = New Size(120, 25)
        Label34.TabIndex = 36
        Label34.Text = "Phone"
        ' 
        ' Label33
        ' 
        Label33.ForeColor = Color.DarkBlue
        Label33.Location = New Point(40, 211)
        Label33.Margin = New Padding(5, 0, 5, 0)
        Label33.Name = "Label33"
        Label33.Size = New Size(134, 25)
        Label33.TabIndex = 34
        Label33.Text = "Address"
        ' 
        ' txtProviderAddress
        ' 
        txtProviderAddress.Location = New Point(40, 242)
        txtProviderAddress.Margin = New Padding(5, 6, 5, 6)
        txtProviderAddress.MaxLength = 150
        txtProviderAddress.Name = "txtProviderAddress"
        txtProviderAddress.ReadOnly = True
        txtProviderAddress.Size = New Size(555, 31)
        txtProviderAddress.TabIndex = 22
        ' 
        ' txtProviderName
        ' 
        txtProviderName.Location = New Point(244, 134)
        txtProviderName.Margin = New Padding(5, 6, 5, 6)
        txtProviderName.MaxLength = 105
        txtProviderName.Name = "txtProviderName"
        txtProviderName.ReadOnly = True
        txtProviderName.Size = New Size(353, 31)
        txtProviderName.TabIndex = 21
        ' 
        ' btnProviderLook
        ' 
        btnProviderLook.Image = CType(resources.GetObject("btnProviderLook.Image"), Image)
        btnProviderLook.Location = New Point(184, 127)
        btnProviderLook.Margin = New Padding(5, 6, 5, 6)
        btnProviderLook.Name = "btnProviderLook"
        btnProviderLook.Size = New Size(50, 52)
        btnProviderLook.TabIndex = 20
        btnProviderLook.UseVisualStyleBackColor = True
        ' 
        ' chkClientBill
        ' 
        chkClientBill.Appearance = Appearance.Button
        chkClientBill.Enabled = False
        chkClientBill.Location = New Point(184, 28)
        chkClientBill.Margin = New Padding(5, 6, 5, 6)
        chkClientBill.Name = "chkClientBill"
        chkClientBill.Size = New Size(111, 52)
        chkClientBill.TabIndex = 16
        chkClientBill.Text = "No"
        chkClientBill.TextAlign = ContentAlignment.MiddleCenter
        chkClientBill.UseVisualStyleBackColor = True
        ' 
        ' Label13
        ' 
        Label13.ForeColor = Color.DarkBlue
        Label13.Location = New Point(25, 42)
        Label13.Margin = New Padding(5, 0, 5, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(149, 25)
        Label13.TabIndex = 10
        Label13.Text = "Party to be billed"
        Label13.TextAlign = ContentAlignment.TopRight
        ' 
        ' Label12
        ' 
        Label12.ForeColor = Color.DarkBlue
        Label12.Location = New Point(40, 97)
        Label12.Margin = New Padding(5, 0, 5, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(134, 25)
        Label12.TabIndex = 9
        Label12.Text = "Provider ID"
        ' 
        ' txtProviderID
        ' 
        txtProviderID.Location = New Point(40, 134)
        txtProviderID.Margin = New Padding(5, 6, 5, 6)
        txtProviderID.MaxLength = 12
        txtProviderID.Name = "txtProviderID"
        txtProviderID.Size = New Size(130, 31)
        txtProviderID.TabIndex = 19
        txtProviderID.TextAlign = HorizontalAlignment.Center
        ' 
        ' tpThirdParty
        ' 
        tpThirdParty.Controls.Add(gbTP)
        tpThirdParty.Location = New Point(4, 34)
        tpThirdParty.Margin = New Padding(5, 6, 5, 6)
        tpThirdParty.Name = "tpThirdParty"
        tpThirdParty.Size = New Size(1062, 465)
        tpThirdParty.TabIndex = 2
        tpThirdParty.Text = "Third Party"
        tpThirdParty.UseVisualStyleBackColor = True
        ' 
        ' gbTP
        ' 
        gbTP.Controls.Add(chkTPContract)
        gbTP.Controls.Add(Label39)
        gbTP.Controls.Add(cmbTPPrice)
        gbTP.Controls.Add(txtInsDOB)
        gbTP.Controls.Add(pctInsSex)
        gbTP.Controls.Add(Label24)
        gbTP.Controls.Add(txtInsSex)
        gbTP.Controls.Add(Label23)
        gbTP.Controls.Add(txtInsAddress)
        gbTP.Controls.Add(Label22)
        gbTP.Controls.Add(txtInsName)
        gbTP.Controls.Add(Label21)
        gbTP.Controls.Add(btnInsLookup)
        gbTP.Controls.Add(Label20)
        gbTP.Controls.Add(txtInsuredID)
        gbTP.Controls.Add(cmbRelation)
        gbTP.Controls.Add(Label19)
        gbTP.Controls.Add(txtCopay)
        gbTP.Controls.Add(Label18)
        gbTP.Controls.Add(txtGroup)
        gbTP.Controls.Add(Label17)
        gbTP.Controls.Add(txtPolicy)
        gbTP.Controls.Add(btnPayers)
        gbTP.Controls.Add(Label16)
        gbTP.Controls.Add(cmbPayer)
        gbTP.Controls.Add(chkTPBill)
        gbTP.Controls.Add(Label14)
        gbTP.Location = New Point(11, 6)
        gbTP.Margin = New Padding(5, 6, 5, 6)
        gbTP.Name = "gbTP"
        gbTP.Padding = New Padding(5, 6, 5, 6)
        gbTP.Size = New Size(1025, 422)
        gbTP.TabIndex = 63
        gbTP.TabStop = False
        ' 
        ' chkTPContract
        ' 
        chkTPContract.Checked = True
        chkTPContract.CheckState = CheckState.Checked
        chkTPContract.ForeColor = Color.DarkBlue
        chkTPContract.Location = New Point(390, 44)
        chkTPContract.Margin = New Padding(5, 6, 5, 6)
        chkTPContract.Name = "chkTPContract"
        chkTPContract.Size = New Size(125, 41)
        chkTPContract.TabIndex = 28
        chkTPContract.Text = "Contract"
        chkTPContract.UseVisualStyleBackColor = True
        ' 
        ' Label39
        ' 
        Label39.ForeColor = Color.DarkBlue
        Label39.Location = New Point(691, 50)
        Label39.Margin = New Padding(5, 0, 5, 0)
        Label39.Name = "Label39"
        Label39.Size = New Size(106, 25)
        Label39.TabIndex = 62
        Label39.Text = "Price Level"
        Label39.TextAlign = ContentAlignment.TopRight
        ' 
        ' cmbTPPrice
        ' 
        cmbTPPrice.DropDownStyle = ComboBoxStyle.DropDownList
        cmbTPPrice.FormattingEnabled = True
        cmbTPPrice.Items.AddRange(New Object() {"List Price", "Level 1", "Level 2", "Level 3", "Level 4", "Level 5", "Level 6", "Level 7", "Level 8", "Level 9"})
        cmbTPPrice.Location = New Point(810, 42)
        cmbTPPrice.Margin = New Padding(5, 6, 5, 6)
        cmbTPPrice.Name = "cmbTPPrice"
        cmbTPPrice.Size = New Size(183, 33)
        cmbTPPrice.TabIndex = 29
        ' 
        ' txtInsDOB
        ' 
        txtInsDOB.Location = New Point(279, 358)
        txtInsDOB.Margin = New Padding(5, 6, 5, 6)
        txtInsDOB.Mask = "00/00/0000"
        txtInsDOB.Name = "txtInsDOB"
        txtInsDOB.Size = New Size(130, 31)
        txtInsDOB.TabIndex = 39
        txtInsDOB.ValidatingType = GetType(Date)
        ' 
        ' pctInsSex
        ' 
        pctInsSex.ErrorImage = CType(resources.GetObject("pctInsSex.ErrorImage"), Image)
        pctInsSex.InitialImage = CType(resources.GetObject("pctInsSex.InitialImage"), Image)
        pctInsSex.Location = New Point(236, 358)
        pctInsSex.Margin = New Padding(5, 6, 5, 6)
        pctInsSex.Name = "pctInsSex"
        pctInsSex.Size = New Size(31, 36)
        pctInsSex.TabIndex = 38
        pctInsSex.TabStop = False
        ' 
        ' Label24
        ' 
        Label24.ForeColor = Color.Fuchsia
        Label24.Location = New Point(274, 327)
        Label24.Margin = New Padding(5, 0, 5, 0)
        Label24.Name = "Label24"
        Label24.Size = New Size(71, 25)
        Label24.TabIndex = 37
        Label24.Text = "DOB"
        ' 
        ' txtInsSex
        ' 
        txtInsSex.Location = New Point(269, 217)
        txtInsSex.Margin = New Padding(5, 6, 5, 6)
        txtInsSex.MaxLength = 6
        txtInsSex.Name = "txtInsSex"
        txtInsSex.Size = New Size(25, 31)
        txtInsSex.TabIndex = 36
        txtInsSex.Visible = False
        ' 
        ' Label23
        ' 
        Label23.ForeColor = Color.Fuchsia
        Label23.Location = New Point(416, 327)
        Label23.Margin = New Padding(5, 0, 5, 0)
        Label23.Name = "Label23"
        Label23.Size = New Size(190, 25)
        Label23.TabIndex = 35
        Label23.Text = "Insured Address"
        ' 
        ' txtInsAddress
        ' 
        txtInsAddress.Location = New Point(421, 358)
        txtInsAddress.Margin = New Padding(5, 6, 5, 6)
        txtInsAddress.MaxLength = 6
        txtInsAddress.Name = "txtInsAddress"
        txtInsAddress.Size = New Size(570, 31)
        txtInsAddress.TabIndex = 40
        ' 
        ' Label22
        ' 
        Label22.ForeColor = Color.Fuchsia
        Label22.Location = New Point(25, 327)
        Label22.Margin = New Padding(5, 0, 5, 0)
        Label22.Name = "Label22"
        Label22.Size = New Size(176, 25)
        Label22.TabIndex = 33
        Label22.Text = "Insured Name"
        ' 
        ' txtInsName
        ' 
        txtInsName.Location = New Point(30, 358)
        txtInsName.Margin = New Padding(5, 6, 5, 6)
        txtInsName.MaxLength = 72
        txtInsName.Name = "txtInsName"
        txtInsName.Size = New Size(194, 31)
        txtInsName.TabIndex = 38
        ' 
        ' Label21
        ' 
        Label21.ForeColor = Color.Fuchsia
        Label21.Location = New Point(555, 223)
        Label21.Margin = New Padding(5, 0, 5, 0)
        Label21.Name = "Label21"
        Label21.Size = New Size(185, 25)
        Label21.TabIndex = 31
        Label21.Text = "Relation to Patient"
        ' 
        ' btnInsLookup
        ' 
        btnInsLookup.Image = CType(resources.GetObject("btnInsLookup.Image"), Image)
        btnInsLookup.Location = New Point(945, 253)
        btnInsLookup.Margin = New Padding(5, 6, 5, 6)
        btnInsLookup.Name = "btnInsLookup"
        btnInsLookup.Size = New Size(50, 52)
        btnInsLookup.TabIndex = 37
        btnInsLookup.UseVisualStyleBackColor = True
        ' 
        ' Label20
        ' 
        Label20.ForeColor = Color.Fuchsia
        Label20.Location = New Point(770, 223)
        Label20.Margin = New Padding(5, 0, 5, 0)
        Label20.Name = "Label20"
        Label20.Size = New Size(116, 25)
        Label20.TabIndex = 29
        Label20.Text = "Insured ID"
        ' 
        ' txtInsuredID
        ' 
        txtInsuredID.Location = New Point(776, 261)
        txtInsuredID.Margin = New Padding(5, 6, 5, 6)
        txtInsuredID.MaxLength = 6
        txtInsuredID.Name = "txtInsuredID"
        txtInsuredID.Size = New Size(138, 31)
        txtInsuredID.TabIndex = 36
        ' 
        ' cmbRelation
        ' 
        cmbRelation.FormattingEnabled = True
        cmbRelation.Items.AddRange(New Object() {"Self", "Spouse", "Son/Daughter", "Other Dependent"})
        cmbRelation.Location = New Point(560, 259)
        cmbRelation.Margin = New Padding(5, 6, 5, 6)
        cmbRelation.Name = "cmbRelation"
        cmbRelation.Size = New Size(188, 33)
        cmbRelation.TabIndex = 35
        ' 
        ' Label19
        ' 
        Label19.ForeColor = Color.DarkBlue
        Label19.Location = New Point(404, 223)
        Label19.Margin = New Padding(5, 0, 5, 0)
        Label19.Name = "Label19"
        Label19.Size = New Size(121, 25)
        Label19.TabIndex = 26
        Label19.Text = "Copayment"
        ' 
        ' txtCopay
        ' 
        txtCopay.Location = New Point(409, 259)
        txtCopay.Margin = New Padding(5, 6, 5, 6)
        txtCopay.MaxLength = 6
        txtCopay.Name = "txtCopay"
        txtCopay.Size = New Size(114, 31)
        txtCopay.TabIndex = 34
        txtCopay.Text = "0.00"
        txtCopay.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label18
        ' 
        Label18.ForeColor = Color.DarkBlue
        Label18.Location = New Point(31, 231)
        Label18.Margin = New Padding(5, 0, 5, 0)
        Label18.Name = "Label18"
        Label18.Size = New Size(139, 25)
        Label18.TabIndex = 24
        Label18.Text = "Group No"
        ' 
        ' txtGroup
        ' 
        txtGroup.Location = New Point(31, 261)
        txtGroup.Margin = New Padding(5, 6, 5, 6)
        txtGroup.MaxLength = 25
        txtGroup.Name = "txtGroup"
        txtGroup.Size = New Size(343, 31)
        txtGroup.TabIndex = 33
        ' 
        ' Label17
        ' 
        Label17.ForeColor = Color.Fuchsia
        Label17.Location = New Point(739, 108)
        Label17.Margin = New Padding(5, 0, 5, 0)
        Label17.Name = "Label17"
        Label17.Size = New Size(179, 25)
        Label17.TabIndex = 22
        Label17.Text = "Policy No"
        ' 
        ' txtPolicy
        ' 
        txtPolicy.Location = New Point(744, 139)
        txtPolicy.Margin = New Padding(5, 6, 5, 6)
        txtPolicy.MaxLength = 25
        txtPolicy.Name = "txtPolicy"
        txtPolicy.Size = New Size(249, 31)
        txtPolicy.TabIndex = 32
        ' 
        ' btnPayers
        ' 
        btnPayers.Image = CType(resources.GetObject("btnPayers.Image"), Image)
        btnPayers.Location = New Point(676, 131)
        btnPayers.Margin = New Padding(5, 6, 5, 6)
        btnPayers.Name = "btnPayers"
        btnPayers.Size = New Size(50, 52)
        btnPayers.TabIndex = 31
        btnPayers.UseVisualStyleBackColor = True
        ' 
        ' Label16
        ' 
        Label16.ForeColor = Color.Fuchsia
        Label16.Location = New Point(26, 108)
        Label16.Margin = New Padding(5, 0, 5, 0)
        Label16.Name = "Label16"
        Label16.Size = New Size(169, 25)
        Label16.TabIndex = 19
        Label16.Text = "Insurance Company"
        ' 
        ' cmbPayer
        ' 
        cmbPayer.FormattingEnabled = True
        cmbPayer.Location = New Point(31, 141)
        cmbPayer.Margin = New Padding(5, 6, 5, 6)
        cmbPayer.Name = "cmbPayer"
        cmbPayer.Size = New Size(618, 33)
        cmbPayer.Sorted = True
        cmbPayer.TabIndex = 30
        ' 
        ' chkTPBill
        ' 
        chkTPBill.Appearance = Appearance.Button
        chkTPBill.Enabled = False
        chkTPBill.Location = New Point(184, 36)
        chkTPBill.Margin = New Padding(5, 6, 5, 6)
        chkTPBill.Name = "chkTPBill"
        chkTPBill.Size = New Size(111, 52)
        chkTPBill.TabIndex = 27
        chkTPBill.Text = "No"
        chkTPBill.TextAlign = ContentAlignment.MiddleCenter
        chkTPBill.UseVisualStyleBackColor = True
        ' 
        ' Label14
        ' 
        Label14.ForeColor = Color.DarkBlue
        Label14.Location = New Point(25, 48)
        Label14.Margin = New Padding(5, 0, 5, 0)
        Label14.Name = "Label14"
        Label14.Size = New Size(149, 25)
        Label14.TabIndex = 16
        Label14.Text = "Party to be billed"
        Label14.TextAlign = ContentAlignment.TopRight
        ' 
        ' SecondaryIns
        ' 
        SecondaryIns.AutoScroll = True
        SecondaryIns.Controls.Add(grpSSubs)
        SecondaryIns.Controls.Add(GroupBox3)
        SecondaryIns.Location = New Point(4, 34)
        SecondaryIns.Margin = New Padding(4, 3, 4, 3)
        SecondaryIns.Name = "SecondaryIns"
        SecondaryIns.Padding = New Padding(4, 3, 4, 3)
        SecondaryIns.Size = New Size(1062, 465)
        SecondaryIns.TabIndex = 5
        SecondaryIns.Text = "Secondary Ins"
        SecondaryIns.UseVisualStyleBackColor = True
        ' 
        ' grpSSubs
        ' 
        grpSSubs.Controls.Add(Label55)
        grpSSubs.Controls.Add(Label58)
        grpSSubs.Controls.Add(txtSSubEmployer)
        grpSSubs.Controls.Add(Label59)
        grpSSubs.Controls.Add(Label60)
        grpSSubs.Controls.Add(txtSSubCell)
        grpSSubs.Controls.Add(txtSSubSSN)
        grpSSubs.Controls.Add(Label61)
        grpSSubs.Controls.Add(txtSSubDOB)
        grpSSubs.Controls.Add(cmbSSubSex)
        grpSSubs.Controls.Add(btnSSubLook)
        grpSSubs.Controls.Add(Label62)
        grpSSubs.Controls.Add(txtSSubCountry)
        grpSSubs.Controls.Add(Label63)
        grpSSubs.Controls.Add(txtSSubEmail)
        grpSSubs.Controls.Add(Label64)
        grpSSubs.Controls.Add(txtSSubWPhone)
        grpSSubs.Controls.Add(txtSSubHPhone)
        grpSSubs.Controls.Add(Label65)
        grpSSubs.Controls.Add(Label67)
        grpSSubs.Controls.Add(txtSSubZip)
        grpSSubs.Controls.Add(Label74)
        grpSSubs.Controls.Add(txtSSubState)
        grpSSubs.Controls.Add(Label75)
        grpSSubs.Controls.Add(txtSSubCity)
        grpSSubs.Controls.Add(Label76)
        grpSSubs.Controls.Add(txtSSubAdd2)
        grpSSubs.Controls.Add(Label77)
        grpSSubs.Controls.Add(txtSSubAdd1)
        grpSSubs.Controls.Add(Label78)
        grpSSubs.Controls.Add(txtSSubMName)
        grpSSubs.Controls.Add(Label79)
        grpSSubs.Controls.Add(txtSSubFName)
        grpSSubs.Controls.Add(Label80)
        grpSSubs.Controls.Add(Label81)
        grpSSubs.Controls.Add(txtSSubLName)
        grpSSubs.Controls.Add(Label83)
        grpSSubs.Controls.Add(txtSSubID)
        grpSSubs.Location = New Point(10, 203)
        grpSSubs.Margin = New Padding(5, 6, 5, 6)
        grpSSubs.Name = "grpSSubs"
        grpSSubs.Padding = New Padding(5, 6, 5, 6)
        grpSSubs.Size = New Size(935, 459)
        grpSSubs.TabIndex = 57
        grpSSubs.TabStop = False
        grpSSubs.Text = "Secondary Subscriber"
        ' 
        ' Label55
        ' 
        Label55.ForeColor = Color.DarkBlue
        Label55.Location = New Point(10, 202)
        Label55.Margin = New Padding(5, 0, 5, 0)
        Label55.Name = "Label55"
        Label55.Size = New Size(140, 25)
        Label55.TabIndex = 99
        Label55.Text = "Work Phone"
        ' 
        ' Label58
        ' 
        Label58.ForeColor = Color.DarkBlue
        Label58.Location = New Point(485, 202)
        Label58.Margin = New Padding(5, 0, 5, 0)
        Label58.Name = "Label58"
        Label58.Size = New Size(191, 25)
        Label58.TabIndex = 98
        Label58.Text = "Employer"
        ' 
        ' txtSSubEmployer
        ' 
        txtSSubEmployer.AcceptsReturn = True
        txtSSubEmployer.Location = New Point(490, 233)
        txtSSubEmployer.Margin = New Padding(5, 6, 5, 6)
        txtSSubEmployer.MaxLength = 60
        txtSSubEmployer.Name = "txtSSubEmployer"
        txtSSubEmployer.Size = New Size(219, 31)
        txtSSubEmployer.TabIndex = 75
        ' 
        ' Label59
        ' 
        Label59.ForeColor = Color.DarkBlue
        Label59.Location = New Point(751, 202)
        Label59.Margin = New Padding(5, 0, 5, 0)
        Label59.Name = "Label59"
        Label59.Size = New Size(130, 25)
        Label59.TabIndex = 95
        Label59.Text = "Cell"
        ' 
        ' Label60
        ' 
        Label60.ForeColor = Color.DarkBlue
        Label60.Location = New Point(489, 117)
        Label60.Margin = New Padding(5, 0, 5, 0)
        Label60.Name = "Label60"
        Label60.Size = New Size(114, 25)
        Label60.TabIndex = 94
        Label60.Text = "SSN"
        ' 
        ' txtSSubCell
        ' 
        txtSSubCell.AcceptsReturn = True
        txtSSubCell.Location = New Point(730, 233)
        txtSSubCell.Margin = New Padding(5, 6, 5, 6)
        txtSSubCell.MaxLength = 13
        txtSSubCell.Name = "txtSSubCell"
        txtSSubCell.Size = New Size(173, 31)
        txtSSubCell.TabIndex = 76
        ' 
        ' txtSSubSSN
        ' 
        txtSSubSSN.Location = New Point(490, 148)
        txtSSubSSN.Margin = New Padding(5, 6, 5, 6)
        txtSSubSSN.Mask = "000-00-0000"
        txtSSubSSN.Name = "txtSSubSSN"
        txtSSubSSN.Size = New Size(219, 31)
        txtSSubSSN.TabIndex = 70
        ' 
        ' Label61
        ' 
        Label61.ForeColor = Color.DarkBlue
        Label61.Location = New Point(25, 117)
        Label61.Margin = New Padding(5, 0, 5, 0)
        Label61.Name = "Label61"
        Label61.Size = New Size(125, 25)
        Label61.TabIndex = 90
        Label61.Text = "Gender"
        ' 
        ' txtSSubDOB
        ' 
        txtSSubDOB.Location = New Point(260, 152)
        txtSSubDOB.Margin = New Padding(5, 6, 5, 6)
        txtSSubDOB.Mask = "00/00/0000"
        txtSSubDOB.Name = "txtSSubDOB"
        txtSSubDOB.Size = New Size(203, 31)
        txtSSubDOB.TabIndex = 69
        txtSSubDOB.ValidatingType = GetType(Date)
        ' 
        ' cmbSSubSex
        ' 
        cmbSSubSex.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSSubSex.FormattingEnabled = True
        cmbSSubSex.Items.AddRange(New Object() {"Female", "Male", "Indetermined", "Unreported"})
        cmbSSubSex.Location = New Point(15, 148)
        cmbSSubSex.Margin = New Padding(5, 6, 5, 6)
        cmbSSubSex.Name = "cmbSSubSex"
        cmbSSubSex.Size = New Size(229, 33)
        cmbSSubSex.TabIndex = 67
        ' 
        ' btnSSubLook
        ' 
        btnSSubLook.Image = CType(resources.GetObject("btnSSubLook.Image"), Image)
        btnSSubLook.Location = New Point(201, 53)
        btnSSubLook.Margin = New Padding(5, 6, 5, 6)
        btnSSubLook.Name = "btnSSubLook"
        btnSSubLook.Size = New Size(44, 50)
        btnSSubLook.TabIndex = 63
        btnSSubLook.UseVisualStyleBackColor = True
        ' 
        ' Label62
        ' 
        Label62.ForeColor = Color.DarkBlue
        Label62.Location = New Point(759, 289)
        Label62.Margin = New Padding(5, 0, 5, 0)
        Label62.Name = "Label62"
        Label62.Size = New Size(150, 25)
        Label62.TabIndex = 86
        Label62.Text = "Country"
        ' 
        ' txtSSubCountry
        ' 
        txtSSubCountry.AcceptsReturn = True
        txtSSubCountry.Location = New Point(754, 319)
        txtSSubCountry.Margin = New Padding(5, 6, 5, 6)
        txtSSubCountry.MaxLength = 35
        txtSSubCountry.Name = "txtSSubCountry"
        txtSSubCountry.Size = New Size(148, 31)
        txtSSubCountry.TabIndex = 82
        ' 
        ' Label63
        ' 
        Label63.ForeColor = Color.DarkBlue
        Label63.Location = New Point(265, 202)
        Label63.Margin = New Padding(5, 0, 5, 0)
        Label63.Name = "Label63"
        Label63.Size = New Size(191, 25)
        Label63.TabIndex = 85
        Label63.Text = "Email Address"
        ' 
        ' txtSSubEmail
        ' 
        txtSSubEmail.AcceptsReturn = True
        txtSSubEmail.Location = New Point(260, 233)
        txtSSubEmail.Margin = New Padding(5, 6, 5, 6)
        txtSSubEmail.MaxLength = 50
        txtSSubEmail.Name = "txtSSubEmail"
        txtSSubEmail.Size = New Size(204, 31)
        txtSSubEmail.TabIndex = 74
        ' 
        ' Label64
        ' 
        Label64.ForeColor = Color.DarkBlue
        Label64.Location = New Point(256, 122)
        Label64.Margin = New Padding(5, 0, 5, 0)
        Label64.Name = "Label64"
        Label64.Size = New Size(91, 25)
        Label64.TabIndex = 84
        Label64.Text = "DOB"
        ' 
        ' txtSSubWPhone
        ' 
        txtSSubWPhone.AcceptsReturn = True
        txtSSubWPhone.Location = New Point(15, 233)
        txtSSubWPhone.Margin = New Padding(5, 6, 5, 6)
        txtSSubWPhone.MaxLength = 13
        txtSSubWPhone.Name = "txtSSubWPhone"
        txtSSubWPhone.Size = New Size(229, 31)
        txtSSubWPhone.TabIndex = 72
        ' 
        ' txtSSubHPhone
        ' 
        txtSSubHPhone.AcceptsReturn = True
        txtSSubHPhone.Location = New Point(730, 148)
        txtSSubHPhone.Margin = New Padding(5, 6, 5, 6)
        txtSSubHPhone.MaxLength = 13
        txtSSubHPhone.Name = "txtSSubHPhone"
        txtSSubHPhone.Size = New Size(170, 31)
        txtSSubHPhone.TabIndex = 71
        ' 
        ' Label65
        ' 
        Label65.ForeColor = Color.DarkBlue
        Label65.Location = New Point(746, 117)
        Label65.Margin = New Padding(5, 0, 5, 0)
        Label65.Name = "Label65"
        Label65.Size = New Size(130, 25)
        Label65.TabIndex = 84
        Label65.Text = "Home Phone"
        ' 
        ' Label67
        ' 
        Label67.ForeColor = Color.DarkBlue
        Label67.Location = New Point(261, 364)
        Label67.Margin = New Padding(5, 0, 5, 0)
        Label67.Name = "Label67"
        Label67.Size = New Size(104, 25)
        Label67.TabIndex = 83
        Label67.Text = "Zip"
        ' 
        ' txtSSubZip
        ' 
        txtSSubZip.AcceptsReturn = True
        txtSSubZip.Location = New Point(265, 394)
        txtSSubZip.Margin = New Padding(5, 6, 5, 6)
        txtSSubZip.MaxLength = 25
        txtSSubZip.Name = "txtSSubZip"
        txtSSubZip.Size = New Size(168, 31)
        txtSSubZip.TabIndex = 81
        ' 
        ' Label74
        ' 
        Label74.ForeColor = Color.DarkBlue
        Label74.Location = New Point(36, 364)
        Label74.Margin = New Padding(5, 0, 5, 0)
        Label74.Name = "Label74"
        Label74.Size = New Size(180, 25)
        Label74.TabIndex = 82
        Label74.Text = "State/Province"
        ' 
        ' txtSSubState
        ' 
        txtSSubState.AcceptsReturn = True
        txtSSubState.Location = New Point(16, 394)
        txtSSubState.Margin = New Padding(5, 6, 5, 6)
        txtSSubState.MaxLength = 35
        txtSSubState.Name = "txtSSubState"
        txtSSubState.Size = New Size(228, 31)
        txtSSubState.TabIndex = 80
        ' 
        ' Label75
        ' 
        Label75.ForeColor = Color.DarkBlue
        Label75.Location = New Point(554, 289)
        Label75.Margin = New Padding(5, 0, 5, 0)
        Label75.Name = "Label75"
        Label75.Size = New Size(121, 25)
        Label75.TabIndex = 81
        Label75.Text = "City"
        ' 
        ' txtSSubCity
        ' 
        txtSSubCity.AcceptsReturn = True
        txtSSubCity.Location = New Point(549, 319)
        txtSSubCity.Margin = New Padding(5, 6, 5, 6)
        txtSSubCity.MaxLength = 35
        txtSSubCity.Name = "txtSSubCity"
        txtSSubCity.Size = New Size(190, 31)
        txtSSubCity.TabIndex = 79
        ' 
        ' Label76
        ' 
        Label76.ForeColor = Color.DarkBlue
        Label76.Location = New Point(285, 289)
        Label76.Margin = New Padding(5, 0, 5, 0)
        Label76.Name = "Label76"
        Label76.Size = New Size(179, 25)
        Label76.TabIndex = 80
        Label76.Text = "Address Line 2"
        ' 
        ' txtSSubAdd2
        ' 
        txtSSubAdd2.AcceptsReturn = True
        txtSSubAdd2.Location = New Point(265, 319)
        txtSSubAdd2.Margin = New Padding(5, 6, 5, 6)
        txtSSubAdd2.MaxLength = 35
        txtSSubAdd2.Name = "txtSSubAdd2"
        txtSSubAdd2.Size = New Size(250, 31)
        txtSSubAdd2.TabIndex = 78
        ' 
        ' Label77
        ' 
        Label77.ForeColor = Color.DarkBlue
        Label77.Location = New Point(10, 289)
        Label77.Margin = New Padding(5, 0, 5, 0)
        Label77.Name = "Label77"
        Label77.Size = New Size(184, 25)
        Label77.TabIndex = 78
        Label77.Text = "Address Line 1"
        ' 
        ' txtSSubAdd1
        ' 
        txtSSubAdd1.AcceptsReturn = True
        txtSSubAdd1.Location = New Point(15, 319)
        txtSSubAdd1.Margin = New Padding(5, 6, 5, 6)
        txtSSubAdd1.MaxLength = 35
        txtSSubAdd1.Name = "txtSSubAdd1"
        txtSSubAdd1.Size = New Size(229, 31)
        txtSSubAdd1.TabIndex = 77
        ' 
        ' Label78
        ' 
        Label78.ForeColor = Color.DarkBlue
        Label78.Location = New Point(256, 117)
        Label78.Margin = New Padding(5, 0, 5, 0)
        Label78.Name = "Label78"
        Label78.Size = New Size(124, 25)
        Label78.TabIndex = 75
        Label78.Text = "D.O.B"
        ' 
        ' txtSSubMName
        ' 
        txtSSubMName.AcceptsReturn = True
        txtSSubMName.Location = New Point(730, 61)
        txtSSubMName.Margin = New Padding(5, 6, 5, 6)
        txtSSubMName.MaxLength = 35
        txtSSubMName.Name = "txtSSubMName"
        txtSSubMName.Size = New Size(170, 31)
        txtSSubMName.TabIndex = 66
        ' 
        ' Label79
        ' 
        Label79.ForeColor = Color.DarkBlue
        Label79.Location = New Point(509, 31)
        Label79.Margin = New Padding(5, 0, 5, 0)
        Label79.Name = "Label79"
        Label79.Size = New Size(116, 25)
        Label79.TabIndex = 69
        Label79.Text = "First Name"
        ' 
        ' txtSSubFName
        ' 
        txtSSubFName.AcceptsReturn = True
        txtSSubFName.Location = New Point(490, 61)
        txtSSubFName.Margin = New Padding(5, 6, 5, 6)
        txtSSubFName.MaxLength = 35
        txtSSubFName.Name = "txtSSubFName"
        txtSSubFName.Size = New Size(219, 31)
        txtSSubFName.TabIndex = 65
        ' 
        ' Label80
        ' 
        Label80.ForeColor = Color.DarkBlue
        Label80.Location = New Point(751, 31)
        Label80.Margin = New Padding(5, 0, 5, 0)
        Label80.Name = "Label80"
        Label80.Size = New Size(131, 25)
        Label80.TabIndex = 72
        Label80.Text = "Middle Name"
        ' 
        ' Label81
        ' 
        Label81.ForeColor = Color.DarkBlue
        Label81.Location = New Point(265, 31)
        Label81.Margin = New Padding(5, 0, 5, 0)
        Label81.Name = "Label81"
        Label81.Size = New Size(124, 25)
        Label81.TabIndex = 67
        Label81.Text = "Last Name"
        ' 
        ' txtSSubLName
        ' 
        txtSSubLName.AcceptsReturn = True
        txtSSubLName.Location = New Point(260, 61)
        txtSSubLName.Margin = New Padding(5, 6, 5, 6)
        txtSSubLName.MaxLength = 35
        txtSSubLName.Name = "txtSSubLName"
        txtSSubLName.Size = New Size(204, 31)
        txtSSubLName.TabIndex = 64
        ' 
        ' Label83
        ' 
        Label83.ForeColor = Color.Fuchsia
        Label83.Location = New Point(25, 31)
        Label83.Margin = New Padding(5, 0, 5, 0)
        Label83.Name = "Label83"
        Label83.Size = New Size(150, 25)
        Label83.TabIndex = 64
        Label83.Text = "Subscriber ID"
        ' 
        ' txtSSubID
        ' 
        txtSSubID.AcceptsReturn = True
        txtSSubID.Location = New Point(15, 61)
        txtSSubID.Margin = New Padding(5, 6, 5, 6)
        txtSSubID.MaxLength = 12
        txtSSubID.Name = "txtSSubID"
        txtSSubID.ReadOnly = True
        txtSSubID.Size = New Size(175, 31)
        txtSSubID.TabIndex = 62
        txtSSubID.TabStop = False
        txtSSubID.TextAlign = HorizontalAlignment.Center
        ' 
        ' GroupBox3
        ' 
        GroupBox3.Controls.Add(conractS)
        GroupBox3.Controls.Add(txtSCopay)
        GroupBox3.Controls.Add(Label66)
        GroupBox3.Controls.Add(Label68)
        GroupBox3.Controls.Add(cmbSRelation)
        GroupBox3.Controls.Add(Label69)
        GroupBox3.Controls.Add(txtSFrom)
        GroupBox3.Controls.Add(txtSTo)
        GroupBox3.Controls.Add(txtSGroup)
        GroupBox3.Controls.Add(Label70)
        GroupBox3.Controls.Add(cmbSIns)
        GroupBox3.Controls.Add(txtSPolicy)
        GroupBox3.Controls.Add(Label71)
        GroupBox3.Controls.Add(Label72)
        GroupBox3.Controls.Add(Label73)
        GroupBox3.Location = New Point(10, 17)
        GroupBox3.Margin = New Padding(5, 6, 5, 6)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Padding = New Padding(5, 6, 5, 6)
        GroupBox3.Size = New Size(935, 186)
        GroupBox3.TabIndex = 56
        GroupBox3.TabStop = False
        GroupBox3.Text = "Secondary Insurance"
        ' 
        ' conractS
        ' 
        conractS.ForeColor = Color.DarkBlue
        conractS.Location = New Point(316, 11)
        conractS.Margin = New Padding(5, 6, 5, 6)
        conractS.Name = "conractS"
        conractS.Size = New Size(139, 41)
        conractS.TabIndex = 70
        conractS.Text = "Use For Bill"
        conractS.UseVisualStyleBackColor = True
        ' 
        ' txtSCopay
        ' 
        txtSCopay.AcceptsReturn = True
        txtSCopay.Location = New Point(709, 136)
        txtSCopay.Margin = New Padding(5, 6, 5, 6)
        txtSCopay.MaxLength = 6
        txtSCopay.Name = "txtSCopay"
        txtSCopay.Size = New Size(194, 31)
        txtSCopay.TabIndex = 61
        ' 
        ' Label66
        ' 
        Label66.ForeColor = Color.DarkBlue
        Label66.Location = New Point(726, 106)
        Label66.Margin = New Padding(5, 0, 5, 0)
        Label66.Name = "Label66"
        Label66.Size = New Size(99, 25)
        Label66.TabIndex = 69
        Label66.Text = "Copay"
        ' 
        ' Label68
        ' 
        Label68.ForeColor = Color.Fuchsia
        Label68.Location = New Point(504, 106)
        Label68.Margin = New Padding(5, 0, 5, 0)
        Label68.Name = "Label68"
        Label68.Size = New Size(121, 25)
        Label68.TabIndex = 67
        Label68.Text = "Relation"
        ' 
        ' cmbSRelation
        ' 
        cmbSRelation.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSRelation.FormattingEnabled = True
        cmbSRelation.Items.AddRange(New Object() {"Self", "Spouse", "Son/Daughter", "Other Dependent"})
        cmbSRelation.Location = New Point(490, 136)
        cmbSRelation.Margin = New Padding(5, 6, 5, 6)
        cmbSRelation.Name = "cmbSRelation"
        cmbSRelation.Size = New Size(205, 33)
        cmbSRelation.TabIndex = 60
        ' 
        ' Label69
        ' 
        Label69.ForeColor = Color.Fuchsia
        Label69.Location = New Point(10, 22)
        Label69.Margin = New Padding(5, 0, 5, 0)
        Label69.Name = "Label69"
        Label69.Size = New Size(235, 25)
        Label69.TabIndex = 60
        Label69.Text = "Insurance Name"
        ' 
        ' txtSFrom
        ' 
        txtSFrom.Location = New Point(209, 136)
        txtSFrom.Margin = New Padding(5, 6, 5, 6)
        txtSFrom.Mask = "00/00/0000"
        txtSFrom.Name = "txtSFrom"
        txtSFrom.Size = New Size(128, 31)
        txtSFrom.TabIndex = 58
        txtSFrom.ValidatingType = GetType(Date)
        ' 
        ' txtSTo
        ' 
        txtSTo.Location = New Point(349, 136)
        txtSTo.Margin = New Padding(5, 6, 5, 6)
        txtSTo.Mask = "00/00/0000"
        txtSTo.Name = "txtSTo"
        txtSTo.Size = New Size(129, 31)
        txtSTo.TabIndex = 59
        txtSTo.ValidatingType = GetType(Date)
        ' 
        ' txtSGroup
        ' 
        txtSGroup.AcceptsReturn = True
        txtSGroup.Location = New Point(10, 136)
        txtSGroup.Margin = New Padding(5, 6, 5, 6)
        txtSGroup.MaxLength = 35
        txtSGroup.Name = "txtSGroup"
        txtSGroup.Size = New Size(180, 31)
        txtSGroup.TabIndex = 57
        ' 
        ' Label70
        ' 
        Label70.ForeColor = Color.DarkBlue
        Label70.Location = New Point(204, 106)
        Label70.Margin = New Padding(5, 0, 5, 0)
        Label70.Name = "Label70"
        Label70.Size = New Size(121, 25)
        Label70.TabIndex = 64
        Label70.Text = "Effective "
        ' 
        ' cmbSIns
        ' 
        cmbSIns.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSIns.FormattingEnabled = True
        cmbSIns.Location = New Point(10, 58)
        cmbSIns.Margin = New Padding(5, 6, 5, 6)
        cmbSIns.Name = "cmbSIns"
        cmbSIns.Size = New Size(480, 33)
        cmbSIns.Sorted = True
        cmbSIns.TabIndex = 53
        ' 
        ' txtSPolicy
        ' 
        txtSPolicy.AcceptsReturn = True
        txtSPolicy.Location = New Point(556, 61)
        txtSPolicy.Margin = New Padding(5, 6, 5, 6)
        txtSPolicy.MaxLength = 35
        txtSPolicy.Name = "txtSPolicy"
        txtSPolicy.Size = New Size(265, 31)
        txtSPolicy.TabIndex = 55
        ' 
        ' Label71
        ' 
        Label71.ForeColor = Color.DarkBlue
        Label71.Location = New Point(356, 106)
        Label71.Margin = New Padding(5, 0, 5, 0)
        Label71.Name = "Label71"
        Label71.Size = New Size(109, 25)
        Label71.TabIndex = 62
        Label71.Text = "Expires"
        ' 
        ' Label72
        ' 
        Label72.ForeColor = Color.Fuchsia
        Label72.Location = New Point(579, 22)
        Label72.Margin = New Padding(5, 0, 5, 0)
        Label72.Name = "Label72"
        Label72.Size = New Size(125, 25)
        Label72.TabIndex = 59
        Label72.Text = "Policy"
        ' 
        ' Label73
        ' 
        Label73.ForeColor = Color.DarkBlue
        Label73.Location = New Point(24, 106)
        Label73.Margin = New Padding(5, 0, 5, 0)
        Label73.Name = "Label73"
        Label73.Size = New Size(146, 25)
        Label73.TabIndex = 61
        Label73.Text = "Group"
        ' 
        ' tpPatient
        ' 
        tpPatient.Controls.Add(Button1)
        tpPatient.Controls.Add(Label52)
        tpPatient.Controls.Add(Label51)
        tpPatient.Controls.Add(Label50)
        tpPatient.Controls.Add(Label49)
        tpPatient.Controls.Add(Label48)
        tpPatient.Controls.Add(Label47)
        tpPatient.Controls.Add(Label3)
        tpPatient.Controls.Add(txtPatState)
        tpPatient.Controls.Add(txtPatHPhone)
        tpPatient.Controls.Add(txtPatCell)
        tpPatient.Controls.Add(txtPatZip)
        tpPatient.Controls.Add(txtPatCity)
        tpPatient.Controls.Add(txtPatAdd2)
        tpPatient.Controls.Add(txtPatMName)
        tpPatient.Controls.Add(txtPatFName)
        tpPatient.Controls.Add(Label40)
        tpPatient.Controls.Add(cmbPatPrice)
        tpPatient.Controls.Add(Label32)
        tpPatient.Controls.Add(txtPatientID)
        tpPatient.Controls.Add(txtPatSex)
        tpPatient.Controls.Add(Label31)
        tpPatient.Controls.Add(Label30)
        tpPatient.Controls.Add(txtPatEmail)
        tpPatient.Controls.Add(Label29)
        tpPatient.Controls.Add(Label28)
        tpPatient.Controls.Add(txtPatSSN)
        tpPatient.Controls.Add(btnPatLook)
        tpPatient.Controls.Add(txtPatDOB)
        tpPatient.Controls.Add(pctPatSex)
        tpPatient.Controls.Add(Label25)
        tpPatient.Controls.Add(Label26)
        tpPatient.Controls.Add(txtPatAdd1)
        tpPatient.Controls.Add(Label27)
        tpPatient.Controls.Add(txtPatLName)
        tpPatient.Controls.Add(chkPatientBill)
        tpPatient.Controls.Add(Label15)
        tpPatient.Location = New Point(4, 34)
        tpPatient.Margin = New Padding(5, 6, 5, 6)
        tpPatient.Name = "tpPatient"
        tpPatient.Size = New Size(1062, 465)
        tpPatient.TabIndex = 3
        tpPatient.Text = "Patient"
        tpPatient.UseVisualStyleBackColor = True
        ' 
        ' Button1
        ' 
        Button1.ForeColor = Color.DodgerBlue
        Button1.Location = New Point(874, 31)
        Button1.Margin = New Padding(5, 6, 5, 6)
        Button1.Name = "Button1"
        Button1.Size = New Size(115, 52)
        Button1.TabIndex = 88
        Button1.Text = "View more"
        Button1.TextImageRelation = TextImageRelation.ImageBeforeText
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Label52
        ' 
        Label52.ForeColor = Color.DarkBlue
        Label52.Location = New Point(691, 148)
        Label52.Margin = New Padding(5, 0, 5, 0)
        Label52.Name = "Label52"
        Label52.Size = New Size(51, 25)
        Label52.TabIndex = 70
        Label52.Text = "Sex"
        ' 
        ' Label51
        ' 
        Label51.ForeColor = Color.DarkBlue
        Label51.Location = New Point(561, 148)
        Label51.Margin = New Padding(5, 0, 5, 0)
        Label51.Name = "Label51"
        Label51.Size = New Size(120, 25)
        Label51.TabIndex = 69
        Label51.Text = "Middle Name"
        ' 
        ' Label50
        ' 
        Label50.ForeColor = Color.DarkBlue
        Label50.Location = New Point(410, 148)
        Label50.Margin = New Padding(5, 0, 5, 0)
        Label50.Name = "Label50"
        Label50.Size = New Size(115, 25)
        Label50.TabIndex = 68
        Label50.Text = "First Name"
        ' 
        ' Label49
        ' 
        Label49.ForeColor = Color.Fuchsia
        Label49.Location = New Point(896, 236)
        Label49.Margin = New Padding(5, 0, 5, 0)
        Label49.Name = "Label49"
        Label49.Size = New Size(81, 25)
        Label49.TabIndex = 67
        Label49.Text = "Zip"
        ' 
        ' Label48
        ' 
        Label48.ForeColor = Color.Fuchsia
        Label48.Location = New Point(750, 236)
        Label48.Margin = New Padding(5, 0, 5, 0)
        Label48.Name = "Label48"
        Label48.Size = New Size(65, 25)
        Label48.TabIndex = 66
        Label48.Text = "ST"
        ' 
        ' Label47
        ' 
        Label47.ForeColor = Color.Fuchsia
        Label47.Location = New Point(570, 236)
        Label47.Margin = New Padding(5, 0, 5, 0)
        Label47.Name = "Label47"
        Label47.Size = New Size(130, 25)
        Label47.TabIndex = 65
        Label47.Text = "City"
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(390, 236)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(130, 25)
        Label3.TabIndex = 64
        Label3.Text = "Address 2"
        ' 
        ' txtPatState
        ' 
        txtPatState.Location = New Point(751, 267)
        txtPatState.Margin = New Padding(5, 6, 5, 6)
        txtPatState.MaxLength = 35
        txtPatState.Name = "txtPatState"
        txtPatState.Size = New Size(118, 31)
        txtPatState.TabIndex = 12
        ' 
        ' txtPatHPhone
        ' 
        txtPatHPhone.Location = New Point(566, 352)
        txtPatHPhone.Margin = New Padding(5, 6, 5, 6)
        txtPatHPhone.Name = "txtPatHPhone"
        txtPatHPhone.Size = New Size(193, 31)
        txtPatHPhone.TabIndex = 15
        ' 
        ' txtPatCell
        ' 
        txtPatCell.Location = New Point(805, 352)
        txtPatCell.Margin = New Padding(5, 6, 5, 6)
        txtPatCell.Name = "txtPatCell"
        txtPatCell.Size = New Size(190, 31)
        txtPatCell.TabIndex = 16
        ' 
        ' txtPatZip
        ' 
        txtPatZip.Location = New Point(881, 267)
        txtPatZip.Margin = New Padding(5, 6, 5, 6)
        txtPatZip.MaxLength = 10
        txtPatZip.Name = "txtPatZip"
        txtPatZip.Size = New Size(114, 31)
        txtPatZip.TabIndex = 13
        ' 
        ' txtPatCity
        ' 
        txtPatCity.Location = New Point(566, 267)
        txtPatCity.Margin = New Padding(5, 6, 5, 6)
        txtPatCity.MaxLength = 35
        txtPatCity.Name = "txtPatCity"
        txtPatCity.Size = New Size(168, 31)
        txtPatCity.TabIndex = 11
        ' 
        ' txtPatAdd2
        ' 
        txtPatAdd2.Location = New Point(395, 267)
        txtPatAdd2.Margin = New Padding(5, 6, 5, 6)
        txtPatAdd2.MaxLength = 35
        txtPatAdd2.Name = "txtPatAdd2"
        txtPatAdd2.Size = New Size(159, 31)
        txtPatAdd2.TabIndex = 10
        ' 
        ' txtPatMName
        ' 
        txtPatMName.Location = New Point(566, 180)
        txtPatMName.Margin = New Padding(5, 6, 5, 6)
        txtPatMName.MaxLength = 35
        txtPatMName.Name = "txtPatMName"
        txtPatMName.Size = New Size(108, 31)
        txtPatMName.TabIndex = 5
        ' 
        ' txtPatFName
        ' 
        txtPatFName.Location = New Point(415, 180)
        txtPatFName.Margin = New Padding(5, 6, 5, 6)
        txtPatFName.MaxLength = 35
        txtPatFName.Name = "txtPatFName"
        txtPatFName.Size = New Size(139, 31)
        txtPatFName.TabIndex = 4
        ' 
        ' Label40
        ' 
        Label40.ForeColor = Color.DarkBlue
        Label40.Location = New Point(436, 36)
        Label40.Margin = New Padding(5, 0, 5, 0)
        Label40.Name = "Label40"
        Label40.Size = New Size(106, 25)
        Label40.TabIndex = 62
        Label40.Text = "Price Level"
        Label40.TextAlign = ContentAlignment.TopRight
        ' 
        ' cmbPatPrice
        ' 
        cmbPatPrice.DropDownStyle = ComboBoxStyle.DropDownList
        cmbPatPrice.FormattingEnabled = True
        cmbPatPrice.Items.AddRange(New Object() {"List Price", "Level 1", "Level 2", "Level 3", "Level 4", "Level 5", "Level 6", "Level 7", "Level 8", "Level 9"})
        cmbPatPrice.Location = New Point(566, 31)
        cmbPatPrice.Margin = New Padding(5, 6, 5, 6)
        cmbPatPrice.Name = "cmbPatPrice"
        cmbPatPrice.Size = New Size(183, 33)
        cmbPatPrice.TabIndex = 42
        ' 
        ' Label32
        ' 
        Label32.ForeColor = Color.Fuchsia
        Label32.Location = New Point(49, 148)
        Label32.Margin = New Padding(5, 0, 5, 0)
        Label32.Name = "Label32"
        Label32.Size = New Size(129, 25)
        Label32.TabIndex = 58
        Label32.Text = "Patient ID"
        ' 
        ' txtPatientID
        ' 
        txtPatientID.Location = New Point(46, 180)
        txtPatientID.Margin = New Padding(5, 6, 5, 6)
        txtPatientID.MaxLength = 6
        txtPatientID.Name = "txtPatientID"
        txtPatientID.Size = New Size(128, 31)
        txtPatientID.TabIndex = 1
        txtPatientID.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtPatSex
        ' 
        txtPatSex.Location = New Point(696, 180)
        txtPatSex.Margin = New Padding(5, 6, 5, 6)
        txtPatSex.MaxLength = 6
        txtPatSex.Name = "txtPatSex"
        txtPatSex.Size = New Size(63, 31)
        txtPatSex.TabIndex = 6
        ' 
        ' Label31
        ' 
        Label31.ForeColor = Color.DarkBlue
        Label31.Location = New Point(809, 322)
        Label31.Margin = New Padding(5, 0, 5, 0)
        Label31.Name = "Label31"
        Label31.Size = New Size(170, 25)
        Label31.TabIndex = 55
        Label31.Text = "Cell Phone"
        ' 
        ' Label30
        ' 
        Label30.ForeColor = Color.DarkBlue
        Label30.Location = New Point(571, 322)
        Label30.Margin = New Padding(5, 0, 5, 0)
        Label30.Name = "Label30"
        Label30.Size = New Size(129, 25)
        Label30.TabIndex = 54
        Label30.Text = "Home Phone"
        ' 
        ' txtPatEmail
        ' 
        txtPatEmail.Location = New Point(46, 352)
        txtPatEmail.Margin = New Padding(5, 6, 5, 6)
        txtPatEmail.MaxLength = 50
        txtPatEmail.Name = "txtPatEmail"
        txtPatEmail.Size = New Size(508, 31)
        txtPatEmail.TabIndex = 14
        ' 
        ' Label29
        ' 
        Label29.ForeColor = Color.DarkBlue
        Label29.Location = New Point(56, 322)
        Label29.Margin = New Padding(5, 0, 5, 0)
        Label29.Name = "Label29"
        Label29.Size = New Size(185, 25)
        Label29.TabIndex = 50
        Label29.Text = "Email Address"
        ' 
        ' Label28
        ' 
        Label28.ForeColor = Color.DarkBlue
        Label28.Location = New Point(49, 236)
        Label28.Margin = New Padding(5, 0, 5, 0)
        Label28.Name = "Label28"
        Label28.Size = New Size(139, 25)
        Label28.TabIndex = 49
        Label28.Text = "Social Security No"
        ' 
        ' txtPatSSN
        ' 
        txtPatSSN.Location = New Point(46, 267)
        txtPatSSN.Margin = New Padding(5, 6, 5, 6)
        txtPatSSN.Mask = "000-00-0000"
        txtPatSSN.Name = "txtPatSSN"
        txtPatSSN.Size = New Size(128, 31)
        txtPatSSN.TabIndex = 8
        ' 
        ' btnPatLook
        ' 
        btnPatLook.Image = CType(resources.GetObject("btnPatLook.Image"), Image)
        btnPatLook.Location = New Point(191, 172)
        btnPatLook.Margin = New Padding(5, 6, 5, 6)
        btnPatLook.Name = "btnPatLook"
        btnPatLook.Size = New Size(50, 52)
        btnPatLook.TabIndex = 2
        btnPatLook.TabStop = False
        btnPatLook.UseVisualStyleBackColor = True
        ' 
        ' txtPatDOB
        ' 
        txtPatDOB.Location = New Point(839, 180)
        txtPatDOB.Margin = New Padding(5, 6, 5, 6)
        txtPatDOB.Mask = "00/00/0000"
        txtPatDOB.Name = "txtPatDOB"
        txtPatDOB.Size = New Size(158, 31)
        txtPatDOB.TabIndex = 7
        txtPatDOB.ValidatingType = GetType(Date)
        ' 
        ' pctPatSex
        ' 
        pctPatSex.ErrorImage = CType(resources.GetObject("pctPatSex.ErrorImage"), Image)
        pctPatSex.InitialImage = CType(resources.GetObject("pctPatSex.InitialImage"), Image)
        pctPatSex.Location = New Point(786, 181)
        pctPatSex.Margin = New Padding(5, 6, 5, 6)
        pctPatSex.Name = "pctPatSex"
        pctPatSex.Size = New Size(31, 36)
        pctPatSex.TabIndex = 45
        pctPatSex.TabStop = False
        ' 
        ' Label25
        ' 
        Label25.ForeColor = Color.Fuchsia
        Label25.Location = New Point(834, 148)
        Label25.Margin = New Padding(5, 0, 5, 0)
        Label25.Name = "Label25"
        Label25.Size = New Size(95, 25)
        Label25.TabIndex = 44
        Label25.Text = "DOB"
        ' 
        ' Label26
        ' 
        Label26.ForeColor = Color.Fuchsia
        Label26.Location = New Point(189, 236)
        Label26.Margin = New Padding(5, 0, 5, 0)
        Label26.Name = "Label26"
        Label26.Size = New Size(130, 25)
        Label26.TabIndex = 43
        Label26.Text = "Address 1"
        ' 
        ' txtPatAdd1
        ' 
        txtPatAdd1.Location = New Point(186, 267)
        txtPatAdd1.Margin = New Padding(5, 6, 5, 6)
        txtPatAdd1.MaxLength = 35
        txtPatAdd1.Name = "txtPatAdd1"
        txtPatAdd1.Size = New Size(195, 31)
        txtPatAdd1.TabIndex = 9
        ' 
        ' Label27
        ' 
        Label27.ForeColor = Color.Fuchsia
        Label27.Location = New Point(251, 148)
        Label27.Margin = New Padding(5, 0, 5, 0)
        Label27.Name = "Label27"
        Label27.Size = New Size(121, 25)
        Label27.TabIndex = 41
        Label27.Text = "Last Name"
        ' 
        ' txtPatLName
        ' 
        txtPatLName.Location = New Point(259, 180)
        txtPatLName.Margin = New Padding(5, 6, 5, 6)
        txtPatLName.MaxLength = 35
        txtPatLName.Name = "txtPatLName"
        txtPatLName.Size = New Size(144, 31)
        txtPatLName.TabIndex = 3
        ' 
        ' chkPatientBill
        ' 
        chkPatientBill.Appearance = Appearance.Button
        chkPatientBill.Enabled = False
        chkPatientBill.Location = New Point(185, 31)
        chkPatientBill.Margin = New Padding(5, 6, 5, 6)
        chkPatientBill.Name = "chkPatientBill"
        chkPatientBill.Size = New Size(111, 52)
        chkPatientBill.TabIndex = 41
        chkPatientBill.Text = "No"
        chkPatientBill.TextAlign = ContentAlignment.MiddleCenter
        chkPatientBill.UseVisualStyleBackColor = True
        ' 
        ' Label15
        ' 
        Label15.ForeColor = Color.DarkBlue
        Label15.Location = New Point(26, 44)
        Label15.Margin = New Padding(5, 0, 5, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(149, 25)
        Label15.TabIndex = 16
        Label15.Text = "Party to be billed"
        Label15.TextAlign = ContentAlignment.TopRight
        ' 
        ' tpCharges
        ' 
        tpCharges.Controls.Add(dgvCharges)
        tpCharges.Location = New Point(4, 34)
        tpCharges.Margin = New Padding(5, 6, 5, 6)
        tpCharges.Name = "tpCharges"
        tpCharges.Size = New Size(1062, 465)
        tpCharges.TabIndex = 4
        tpCharges.Text = "Charge Detail"
        tpCharges.UseVisualStyleBackColor = True
        ' 
        ' dgvCharges
        ' 
        dgvCharges.AllowUserToAddRows = False
        dgvCharges.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.AliceBlue
        dgvCharges.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvCharges.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvCharges.Columns.AddRange(New DataGridViewColumn() {DEL, TGPID, tgpLook, TGPName, CPT, Dx, M1, M2, M3, M4, Price, Unit, Extend, POS, STATUS, FN, BillDate, Biller})
        dgvCharges.Dock = DockStyle.Top
        dgvCharges.Location = New Point(0, 0)
        dgvCharges.Margin = New Padding(5, 6, 5, 6)
        dgvCharges.Name = "dgvCharges"
        dgvCharges.RowHeadersVisible = False
        dgvCharges.RowHeadersWidth = 51
        DataGridViewCellStyle2.BackColor = Color.Linen
        dgvCharges.RowsDefaultCellStyle = DataGridViewCellStyle2
        dgvCharges.Size = New Size(1062, 447)
        dgvCharges.TabIndex = 52
        ' 
        ' DEL
        ' 
        DEL.FillWeight = 25F
        DEL.HeaderText = ""
        DEL.Image = CType(resources.GetObject("DEL.Image"), Image)
        DEL.MinimumWidth = 6
        DEL.Name = "DEL"
        DEL.ReadOnly = True
        DEL.Width = 36
        ' 
        ' TGPID
        ' 
        TGPID.FillWeight = 50F
        TGPID.HeaderText = "ID"
        TGPID.MaxInputLength = 5
        TGPID.MinimumWidth = 6
        TGPID.Name = "TGPID"
        TGPID.SortMode = DataGridViewColumnSortMode.NotSortable
        TGPID.Width = 71
        ' 
        ' tgpLook
        ' 
        tgpLook.FillWeight = 25F
        tgpLook.HeaderText = ""
        tgpLook.Image = CType(resources.GetObject("tgpLook.Image"), Image)
        tgpLook.MinimumWidth = 6
        tgpLook.Name = "tgpLook"
        tgpLook.ReadOnly = True
        tgpLook.Width = 36
        ' 
        ' TGPName
        ' 
        TGPName.FillWeight = 90F
        TGPName.HeaderText = "Name"
        TGPName.MaxInputLength = 35
        TGPName.MinimumWidth = 6
        TGPName.Name = "TGPName"
        TGPName.ReadOnly = True
        TGPName.SortMode = DataGridViewColumnSortMode.NotSortable
        TGPName.Width = 129
        ' 
        ' CPT
        ' 
        CPT.FillWeight = 55F
        CPT.HeaderText = "CPT"
        CPT.MinimumWidth = 6
        CPT.Name = "CPT"
        CPT.SortMode = DataGridViewColumnSortMode.NotSortable
        CPT.Width = 79
        ' 
        ' Dx
        ' 
        Dx.FillWeight = 45F
        Dx.HeaderText = "Dx"
        Dx.MaxInputLength = 16
        Dx.MinimumWidth = 6
        Dx.Name = "Dx"
        Dx.SortMode = DataGridViewColumnSortMode.NotSortable
        Dx.Width = 64
        ' 
        ' M1
        ' 
        M1.FillWeight = 25F
        M1.HeaderText = "M1"
        M1.MaxInputLength = 10
        M1.MinimumWidth = 6
        M1.Name = "M1"
        M1.SortMode = DataGridViewColumnSortMode.NotSortable
        M1.Width = 36
        ' 
        ' M2
        ' 
        M2.FillWeight = 25F
        M2.HeaderText = "M2"
        M2.MaxInputLength = 10
        M2.MinimumWidth = 6
        M2.Name = "M2"
        M2.SortMode = DataGridViewColumnSortMode.NotSortable
        M2.Width = 36
        ' 
        ' M3
        ' 
        M3.FillWeight = 25F
        M3.HeaderText = "M3"
        M3.MaxInputLength = 10
        M3.MinimumWidth = 6
        M3.Name = "M3"
        M3.SortMode = DataGridViewColumnSortMode.NotSortable
        M3.Width = 35
        ' 
        ' M4
        ' 
        M4.FillWeight = 25F
        M4.HeaderText = "M4"
        M4.MaxInputLength = 10
        M4.MinimumWidth = 6
        M4.Name = "M4"
        M4.SortMode = DataGridViewColumnSortMode.NotSortable
        M4.Width = 36
        ' 
        ' Price
        ' 
        Price.FillWeight = 50F
        Price.HeaderText = "Price"
        Price.MaxInputLength = 8
        Price.MinimumWidth = 6
        Price.Name = "Price"
        Price.SortMode = DataGridViewColumnSortMode.NotSortable
        Price.Width = 72
        ' 
        ' Unit
        ' 
        Unit.FillWeight = 30F
        Unit.HeaderText = "Unit"
        Unit.MaxInputLength = 4
        Unit.MinimumWidth = 6
        Unit.Name = "Unit"
        Unit.SortMode = DataGridViewColumnSortMode.NotSortable
        Unit.Width = 43
        ' 
        ' Extend
        ' 
        Extend.FillWeight = 60F
        Extend.HeaderText = "Extend"
        Extend.MinimumWidth = 6
        Extend.Name = "Extend"
        Extend.ReadOnly = True
        Extend.SortMode = DataGridViewColumnSortMode.NotSortable
        Extend.Width = 85
        ' 
        ' POS
        ' 
        POS.FillWeight = 30F
        POS.HeaderText = "POS"
        POS.MaxInputLength = 12
        POS.MinimumWidth = 6
        POS.Name = "POS"
        POS.SortMode = DataGridViewColumnSortMode.NotSortable
        POS.Width = 43
        ' 
        ' STATUS
        ' 
        STATUS.FillWeight = 25F
        STATUS.HeaderText = "ST"
        STATUS.MinimumWidth = 6
        STATUS.Name = "STATUS"
        STATUS.Width = 36
        ' 
        ' FN
        ' 
        FN.FillWeight = 25F
        FN.HeaderText = "FN"
        FN.MaxInputLength = 1
        FN.MinimumWidth = 6
        FN.Name = "FN"
        FN.ReadOnly = True
        FN.Resizable = DataGridViewTriState.True
        FN.SortMode = DataGridViewColumnSortMode.NotSortable
        FN.Width = 36
        ' 
        ' BillDate
        ' 
        BillDate.FillWeight = 70F
        BillDate.HeaderText = "Bill Date"
        BillDate.MaxInputLength = 10
        BillDate.MinimumWidth = 6
        BillDate.Name = "BillDate"
        BillDate.ReadOnly = True
        BillDate.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Biller
        ' 
        Biller.FillWeight = 60F
        Biller.HeaderText = "Biller"
        Biller.MinimumWidth = 6
        Biller.Name = "Biller"
        Biller.ReadOnly = True
        Biller.Width = 86
        ' 
        ' DGVICD9s
        ' 
        DGVICD9s.AllowUserToAddRows = False
        DGVICD9s.AllowUserToDeleteRows = False
        DataGridViewCellStyle3.BackColor = Color.LavenderBlush
        DGVICD9s.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        DGVICD9s.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGVICD9s.Columns.AddRange(New DataGridViewColumn() {SNo, ICD9, LookUp})
        DGVICD9s.Location = New Point(20, 648)
        DGVICD9s.Margin = New Padding(5, 6, 5, 6)
        DGVICD9s.Name = "DGVICD9s"
        DGVICD9s.RowHeadersVisible = False
        DGVICD9s.RowHeadersWidth = 51
        DGVICD9s.ScrollBars = ScrollBars.Vertical
        DGVICD9s.Size = New Size(245, 466)
        DGVICD9s.TabIndex = 14
        ' 
        ' SNo
        ' 
        SNo.FillWeight = 25F
        SNo.HeaderText = "No"
        SNo.MaxInputLength = 2
        SNo.MinimumWidth = 6
        SNo.Name = "SNo"
        SNo.ReadOnly = True
        SNo.SortMode = DataGridViewColumnSortMode.NotSortable
        SNo.Width = 50
        ' 
        ' ICD9
        ' 
        ICD9.FillWeight = 66F
        ICD9.HeaderText = "Dx Code"
        ICD9.MaxInputLength = 12
        ICD9.MinimumWidth = 6
        ICD9.Name = "ICD9"
        ICD9.SortMode = DataGridViewColumnSortMode.NotSortable
        ICD9.Width = 132
        ' 
        ' LookUp
        ' 
        LookUp.FillWeight = 30F
        LookUp.HeaderText = ""
        LookUp.Image = CType(resources.GetObject("LookUp.Image"), Image)
        LookUp.MinimumWidth = 6
        LookUp.Name = "LookUp"
        LookUp.Resizable = DataGridViewTriState.True
        LookUp.Width = 60
        ' 
        ' Label10
        ' 
        Label10.ForeColor = Color.DarkBlue
        Label10.Location = New Point(16, 602)
        Label10.Margin = New Padding(5, 0, 5, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(129, 28)
        Label10.TabIndex = 20
        Label10.Text = "Dx Codes"
        ' 
        ' txtBCharges
        ' 
        txtBCharges.Location = New Point(820, 466)
        txtBCharges.Margin = New Padding(5, 6, 5, 6)
        txtBCharges.Name = "txtBCharges"
        txtBCharges.ReadOnly = True
        txtBCharges.Size = New Size(123, 31)
        txtBCharges.TabIndex = 53
        txtBCharges.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label11
        ' 
        Label11.ForeColor = Color.DarkBlue
        Label11.Location = New Point(820, 427)
        Label11.Margin = New Padding(5, 0, 5, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(125, 25)
        Label11.TabIndex = 22
        Label11.Text = "Billed Amt"
        Label11.TextAlign = ContentAlignment.TopCenter
        ' 
        ' chkBillDate
        ' 
        chkBillDate.Appearance = Appearance.Button
        chkBillDate.Location = New Point(21, 459)
        chkBillDate.Margin = New Padding(5, 6, 5, 6)
        chkBillDate.Name = "chkBillDate"
        chkBillDate.Size = New Size(150, 47)
        chkBillDate.TabIndex = 19
        chkBillDate.Text = "Service Date"
        chkBillDate.TextAlign = ContentAlignment.MiddleCenter
        chkBillDate.UseVisualStyleBackColor = True
        ' 
        ' Label41
        ' 
        Label41.ForeColor = Color.DarkBlue
        Label41.Location = New Point(426, 431)
        Label41.Margin = New Padding(5, 0, 5, 0)
        Label41.Name = "Label41"
        Label41.Size = New Size(101, 27)
        Label41.TabIndex = 58
        Label41.Text = "Due Date"
        ' 
        ' txtDueDays
        ' 
        txtDueDays.Location = New Point(329, 466)
        txtDueDays.Margin = New Padding(5, 6, 5, 6)
        txtDueDays.MaxLength = 3
        txtDueDays.Name = "txtDueDays"
        txtDueDays.Size = New Size(65, 31)
        txtDueDays.TabIndex = 20
        txtDueDays.Text = "15"
        txtDueDays.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label42
        ' 
        Label42.ForeColor = Color.DarkBlue
        Label42.Location = New Point(319, 427)
        Label42.Margin = New Padding(5, 0, 5, 0)
        Label42.Name = "Label42"
        Label42.Size = New Size(99, 27)
        Label42.TabIndex = 56
        Label42.Text = "Due Days"
        ' 
        ' txtDueDate
        ' 
        txtDueDate.Location = New Point(419, 464)
        txtDueDate.Margin = New Padding(5, 6, 5, 6)
        txtDueDate.Mask = "00/00/0000"
        txtDueDate.Name = "txtDueDate"
        txtDueDate.Size = New Size(119, 31)
        txtDueDate.TabIndex = 21
        txtDueDate.ValidatingType = GetType(Date)
        ' 
        ' btnDxSync
        ' 
        btnDxSync.Image = CType(resources.GetObject("btnDxSync.Image"), Image)
        btnDxSync.Location = New Point(215, 589)
        btnDxSync.Margin = New Padding(5, 6, 5, 6)
        btnDxSync.Name = "btnDxSync"
        btnDxSync.Size = New Size(50, 52)
        btnDxSync.TabIndex = 59
        ToolTip1.SetToolTip(btnDxSync, "Syncronize Accession's Dx with Components")
        btnDxSync.UseVisualStyleBackColor = True
        ' 
        ' orgChargID
        ' 
        orgChargID.Location = New Point(934, 517)
        orgChargID.Margin = New Padding(5, 6, 5, 6)
        orgChargID.MaxLength = 20
        orgChargID.Name = "orgChargID"
        orgChargID.Size = New Size(164, 31)
        orgChargID.TabIndex = 96
        orgChargID.TextAlign = HorizontalAlignment.Center
        ToolTip1.SetToolTip(orgChargID, "Orignal Claim ID")
        ' 
        ' chkIsBilled
        ' 
        chkIsBilled.Appearance = Appearance.Button
        chkIsBilled.Location = New Point(586, 341)
        chkIsBilled.Margin = New Padding(5, 6, 5, 6)
        chkIsBilled.Name = "chkIsBilled"
        chkIsBilled.Size = New Size(160, 61)
        chkIsBilled.TabIndex = 62
        chkIsBilled.Text = "No"
        chkIsBilled.TextAlign = ContentAlignment.MiddleCenter
        chkIsBilled.UseVisualStyleBackColor = True
        chkIsBilled.Visible = False
        ' 
        ' Label44
        ' 
        Label44.ForeColor = Color.DarkBlue
        Label44.Location = New Point(606, 309)
        Label44.Margin = New Padding(5, 0, 5, 0)
        Label44.Name = "Label44"
        Label44.Size = New Size(124, 25)
        Label44.TabIndex = 63
        Label44.Text = "Acc Status"
        Label44.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtStatus
        ' 
        txtStatus.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtStatus.ForeColor = Color.Red
        txtStatus.Location = New Point(610, 298)
        txtStatus.Margin = New Padding(5, 6, 5, 6)
        txtStatus.Name = "txtStatus"
        txtStatus.ReadOnly = True
        txtStatus.Size = New Size(120, 26)
        txtStatus.TabIndex = 18
        txtStatus.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtTax
        ' 
        txtTax.Location = New Point(550, 464)
        txtTax.Margin = New Padding(5, 6, 5, 6)
        txtTax.MaxLength = 5
        txtTax.Name = "txtTax"
        txtTax.Size = New Size(79, 31)
        txtTax.TabIndex = 22
        txtTax.Text = "0.00"
        txtTax.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtTerm
        ' 
        txtTerm.Location = New Point(644, 466)
        txtTerm.Margin = New Padding(5, 6, 5, 6)
        txtTerm.MaxLength = 60
        txtTerm.Name = "txtTerm"
        txtTerm.Size = New Size(164, 31)
        txtTerm.TabIndex = 23
        txtTerm.Text = "Net 15 Days"
        ' 
        ' Label45
        ' 
        Label45.ForeColor = Color.DarkBlue
        Label45.Location = New Point(550, 428)
        Label45.Margin = New Padding(5, 0, 5, 0)
        Label45.Name = "Label45"
        Label45.Size = New Size(81, 27)
        Label45.TabIndex = 67
        Label45.Text = "Tax %"
        ' 
        ' Label46
        ' 
        Label46.ForeColor = Color.DarkBlue
        Label46.Location = New Point(639, 428)
        Label46.Margin = New Padding(5, 0, 5, 0)
        Label46.Name = "Label46"
        Label46.Size = New Size(171, 27)
        Label46.TabIndex = 68
        Label46.Text = "Invoice Term"
        ' 
        ' btnHistory
        ' 
        btnHistory.Enabled = False
        btnHistory.ForeColor = Color.DarkBlue
        btnHistory.Image = CType(resources.GetObject("btnHistory.Image"), Image)
        btnHistory.Location = New Point(780, 341)
        btnHistory.Margin = New Padding(5, 6, 5, 6)
        btnHistory.Name = "btnHistory"
        btnHistory.Size = New Size(100, 58)
        btnHistory.TabIndex = 69
        btnHistory.TextAlign = ContentAlignment.MiddleRight
        btnHistory.TextImageRelation = TextImageRelation.ImageBeforeText
        btnHistory.UseVisualStyleBackColor = True
        ' 
        ' txtBillDate
        ' 
        txtBillDate.Location = New Point(191, 464)
        txtBillDate.Margin = New Padding(5, 6, 5, 6)
        txtBillDate.Mask = "00/00/0000"
        txtBillDate.Name = "txtBillDate"
        txtBillDate.Size = New Size(119, 31)
        txtBillDate.TabIndex = 70
        txtBillDate.ValidatingType = GetType(Date)
        ' 
        ' Label53
        ' 
        Label53.ForeColor = Color.DarkBlue
        Label53.Location = New Point(195, 431)
        Label53.Margin = New Padding(5, 0, 5, 0)
        Label53.Name = "Label53"
        Label53.Size = New Size(101, 27)
        Label53.TabIndex = 71
        Label53.Text = "Bill Date"
        ' 
        ' txtUCharges
        ' 
        txtUCharges.Location = New Point(961, 466)
        txtUCharges.Margin = New Padding(5, 6, 5, 6)
        txtUCharges.Name = "txtUCharges"
        txtUCharges.ReadOnly = True
        txtUCharges.Size = New Size(123, 31)
        txtUCharges.TabIndex = 72
        txtUCharges.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label54
        ' 
        Label54.ForeColor = Color.DarkBlue
        Label54.Location = New Point(961, 427)
        Label54.Margin = New Padding(5, 0, 5, 0)
        Label54.Name = "Label54"
        Label54.Size = New Size(125, 31)
        Label54.TabIndex = 73
        Label54.Text = "Unbilled Amt"
        Label54.TextAlign = ContentAlignment.TopCenter
        ' 
        ' chkRev
        ' 
        chkRev.Location = New Point(1104, 466)
        chkRev.Margin = New Padding(5, 6, 5, 6)
        chkRev.Name = "chkRev"
        chkRev.Size = New Size(121, 41)
        chkRev.TabIndex = 76
        chkRev.Text = "Reverse"
        chkRev.UseVisualStyleBackColor = True
        ' 
        ' Label56
        ' 
        Label56.ForeColor = Color.DarkBlue
        Label56.Location = New Point(316, 323)
        Label56.Margin = New Padding(5, 0, 5, 0)
        Label56.Name = "Label56"
        Label56.Size = New Size(109, 25)
        Label56.TabIndex = 79
        Label56.Text = "Invoice(s)"
        ' 
        ' cmbInvoices
        ' 
        cmbInvoices.DropDownStyle = ComboBoxStyle.DropDownList
        cmbInvoices.FormattingEnabled = True
        cmbInvoices.Location = New Point(305, 361)
        cmbInvoices.Margin = New Padding(5, 6, 5, 6)
        cmbInvoices.Name = "cmbInvoices"
        cmbInvoices.Size = New Size(158, 33)
        cmbInvoices.TabIndex = 80
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(271, 517)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(220, 36)
        Label1.TabIndex = 83
        Label1.Text = "Additional Info (Box 19):"
        ' 
        ' txtBox19
        ' 
        txtBox19.Location = New Point(280, 561)
        txtBox19.Margin = New Padding(5, 6, 5, 6)
        txtBox19.MaxLength = 90
        txtBox19.Name = "txtBox19"
        txtBox19.Size = New Size(818, 31)
        txtBox19.TabIndex = 84
        ' 
        ' chkECC
        ' 
        chkECC.Appearance = Appearance.Button
        chkECC.Checked = True
        chkECC.CheckState = CheckState.Checked
        chkECC.Location = New Point(24, 514)
        chkECC.Margin = New Padding(5, 6, 5, 6)
        chkECC.Name = "chkECC"
        chkECC.Size = New Size(149, 47)
        chkECC.TabIndex = 85
        chkECC.Text = "837"
        chkECC.TextAlign = ContentAlignment.MiddleCenter
        chkECC.UseVisualStyleBackColor = True
        ' 
        ' dgvComments
        ' 
        dgvComments.AllowUserToAddRows = False
        dgvComments.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.BackColor = Color.AliceBlue
        dgvComments.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        dgvComments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvComments.Columns.AddRange(New DataGridViewColumn() {Dated, ABP, Cmnt, User})
        dgvComments.Location = New Point(20, 1133)
        dgvComments.Margin = New Padding(5, 6, 5, 6)
        dgvComments.Name = "dgvComments"
        dgvComments.RowHeadersVisible = False
        dgvComments.RowHeadersWidth = 51
        DataGridViewCellStyle5.BackColor = Color.MistyRose
        dgvComments.RowsDefaultCellStyle = DataGridViewCellStyle5
        dgvComments.Size = New Size(1319, 156)
        dgvComments.TabIndex = 86
        ' 
        ' Dated
        ' 
        Dated.FillWeight = 80F
        Dated.HeaderText = "Dated"
        Dated.MaxInputLength = 10
        Dated.MinimumWidth = 6
        Dated.Name = "Dated"
        Dated.ReadOnly = True
        Dated.Width = 80
        ' 
        ' ABP
        ' 
        ABP.FillWeight = 30F
        ABP.HeaderText = "ABP"
        ABP.MaxInputLength = 3
        ABP.MinimumWidth = 6
        ABP.Name = "ABP"
        ABP.ReadOnly = True
        ABP.Width = 30
        ' 
        ' Cmnt
        ' 
        Cmnt.FillWeight = 560F
        Cmnt.HeaderText = "Comment"
        Cmnt.MaxInputLength = 500
        Cmnt.MinimumWidth = 6
        Cmnt.Name = "Cmnt"
        Cmnt.SortMode = DataGridViewColumnSortMode.NotSortable
        Cmnt.Width = 560
        ' 
        ' User
        ' 
        User.FillWeight = 105F
        User.HeaderText = "By"
        User.MaxInputLength = 60
        User.MinimumWidth = 6
        User.Name = "User"
        User.Width = 105
        ' 
        ' lblFrom
        ' 
        lblFrom.ForeColor = Color.DarkBlue
        lblFrom.Location = New Point(21, 31)
        lblFrom.Margin = New Padding(5, 0, 5, 0)
        lblFrom.Name = "lblFrom"
        lblFrom.Size = New Size(151, 25)
        lblFrom.TabIndex = 0
        lblFrom.Text = "From"
        ' 
        ' txtAccFrom
        ' 
        txtAccFrom.Location = New Point(11, 183)
        txtAccFrom.Margin = New Padding(5, 6, 5, 6)
        txtAccFrom.MaxLength = 14
        txtAccFrom.Name = "txtAccFrom"
        txtAccFrom.Size = New Size(129, 31)
        txtAccFrom.TabIndex = 4
        txtAccFrom.TextAlign = HorizontalAlignment.Center
        ' 
        ' btnLoad
        ' 
        btnLoad.ForeColor = Color.DarkBlue
        btnLoad.Image = CType(resources.GetObject("btnLoad.Image"), Image)
        btnLoad.Location = New Point(1256, 164)
        btnLoad.Margin = New Padding(5, 6, 5, 6)
        btnLoad.Name = "btnLoad"
        btnLoad.Size = New Size(59, 58)
        btnLoad.TabIndex = 11
        btnLoad.TextAlign = ContentAlignment.MiddleRight
        btnLoad.TextImageRelation = TextImageRelation.ImageBeforeText
        btnLoad.UseVisualStyleBackColor = True
        ' 
        ' lblTo
        ' 
        lblTo.ForeColor = Color.DarkBlue
        lblTo.Location = New Point(184, 31)
        lblTo.Margin = New Padding(5, 0, 5, 0)
        lblTo.Name = "lblTo"
        lblTo.Size = New Size(149, 25)
        lblTo.TabIndex = 4
        lblTo.Text = "To"
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(10, 152)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(146, 25)
        Label4.TabIndex = 7
        Label4.Text = "Accession From"
        ' 
        ' txtAccTo
        ' 
        txtAccTo.Location = New Point(159, 183)
        txtAccTo.Margin = New Padding(5, 6, 5, 6)
        txtAccTo.MaxLength = 14
        txtAccTo.Name = "txtAccTo"
        txtAccTo.Size = New Size(129, 31)
        txtAccTo.TabIndex = 5
        txtAccTo.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.DarkBlue
        Label5.Location = New Point(162, 152)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(129, 25)
        Label5.TabIndex = 9
        Label5.Text = "Accession To"
        ' 
        ' cmbABU
        ' 
        cmbABU.DropDownStyle = ComboBoxStyle.DropDownList
        cmbABU.FormattingEnabled = True
        cmbABU.Items.AddRange(New Object() {"ALL", "BILLED", "PART BILLED", "UNBILLED"})
        cmbABU.Location = New Point(600, 172)
        cmbABU.Margin = New Padding(5, 6, 5, 6)
        cmbABU.Name = "cmbABU"
        cmbABU.Size = New Size(175, 33)
        cmbABU.TabIndex = 6
        ' 
        ' Label43
        ' 
        Label43.ForeColor = Color.DarkBlue
        Label43.Location = New Point(601, 141)
        Label43.Margin = New Padding(5, 0, 5, 0)
        Label43.Name = "Label43"
        Label43.Size = New Size(124, 25)
        Label43.TabIndex = 14
        Label43.Text = "Bill Status"
        ' 
        ' lstTargets
        ' 
        lstTargets.FormattingEnabled = True
        lstTargets.Location = New Point(786, 31)
        lstTargets.Margin = New Padding(5, 6, 5, 6)
        lstTargets.Name = "lstTargets"
        lstTargets.Size = New Size(458, 116)
        lstTargets.TabIndex = 8
        ' 
        ' btnDesel
        ' 
        btnDesel.ForeColor = Color.DarkBlue
        btnDesel.Image = CType(resources.GetObject("btnDesel.Image"), Image)
        btnDesel.Location = New Point(1256, 25)
        btnDesel.Margin = New Padding(5, 6, 5, 6)
        btnDesel.Name = "btnDesel"
        btnDesel.Size = New Size(59, 58)
        btnDesel.TabIndex = 9
        btnDesel.TextAlign = ContentAlignment.MiddleRight
        btnDesel.TextImageRelation = TextImageRelation.ImageBeforeText
        btnDesel.UseVisualStyleBackColor = True
        ' 
        ' btnSel
        ' 
        btnSel.ForeColor = Color.DarkBlue
        btnSel.Image = CType(resources.GetObject("btnSel.Image"), Image)
        btnSel.Location = New Point(1256, 94)
        btnSel.Margin = New Padding(5, 6, 5, 6)
        btnSel.Name = "btnSel"
        btnSel.Size = New Size(59, 58)
        btnSel.TabIndex = 10
        btnSel.TextAlign = ContentAlignment.MiddleRight
        btnSel.TextImageRelation = TextImageRelation.ImageBeforeText
        btnSel.UseVisualStyleBackColor = True
        ' 
        ' btnTarget
        ' 
        btnTarget.ForeColor = Color.DarkBlue
        btnTarget.Image = CType(resources.GetObject("btnTarget.Image"), Image)
        btnTarget.Location = New Point(719, 106)
        btnTarget.Margin = New Padding(5, 6, 5, 6)
        btnTarget.Name = "btnTarget"
        btnTarget.Size = New Size(59, 47)
        btnTarget.TabIndex = 7
        btnTarget.TextAlign = ContentAlignment.MiddleRight
        btnTarget.TextImageRelation = TextImageRelation.ImageBeforeText
        btnTarget.UseVisualStyleBackColor = True
        ' 
        ' chkAI
        ' 
        chkAI.Appearance = Appearance.Button
        chkAI.Location = New Point(600, 36)
        chkAI.Margin = New Padding(5, 6, 5, 6)
        chkAI.Name = "chkAI"
        chkAI.Size = New Size(176, 52)
        chkAI.TabIndex = 18
        chkAI.Text = "ACCESSION"
        chkAI.TextAlign = ContentAlignment.MiddleCenter
        chkAI.UseVisualStyleBackColor = True
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(dtpDateTo)
        GroupBox1.Controls.Add(lblClearDates)
        GroupBox1.Controls.Add(dtpDateFrom)
        GroupBox1.Controls.Add(lblEligibility)
        GroupBox1.Controls.Add(btnEECC)
        GroupBox1.Controls.Add(dgvDiscrete)
        GroupBox1.Controls.Add(chkAI)
        GroupBox1.Controls.Add(btnTarget)
        GroupBox1.Controls.Add(btnSel)
        GroupBox1.Controls.Add(btnDesel)
        GroupBox1.Controls.Add(lstTargets)
        GroupBox1.Controls.Add(Label43)
        GroupBox1.Controls.Add(cmbABU)
        GroupBox1.Controls.Add(Label5)
        GroupBox1.Controls.Add(txtAccTo)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(lblTo)
        GroupBox1.Controls.Add(btnLoad)
        GroupBox1.Controls.Add(txtAccFrom)
        GroupBox1.Controls.Add(lblFrom)
        GroupBox1.Controls.Add(loading)
        GroupBox1.Location = New Point(20, 53)
        GroupBox1.Margin = New Padding(5, 6, 5, 6)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(5, 6, 5, 6)
        GroupBox1.Size = New Size(1325, 242)
        GroupBox1.TabIndex = 4
        GroupBox1.TabStop = False
        GroupBox1.Text = "Selection"
        ' 
        ' dtpDateTo
        ' 
        dtpDateTo.CustomFormat = " "
        dtpDateTo.Format = DateTimePickerFormat.Custom
        dtpDateTo.Location = New Point(159, 59)
        dtpDateTo.Margin = New Padding(4, 3, 4, 3)
        dtpDateTo.Name = "dtpDateTo"
        dtpDateTo.Size = New Size(129, 31)
        dtpDateTo.TabIndex = 102
        ' 
        ' lblClearDates
        ' 
        lblClearDates.BackColor = SystemColors.Control
        lblClearDates.Image = CType(resources.GetObject("lblClearDates.Image"), Image)
        lblClearDates.Location = New Point(296, 53)
        lblClearDates.Margin = New Padding(4, 0, 4, 0)
        lblClearDates.Name = "lblClearDates"
        lblClearDates.Size = New Size(34, 47)
        lblClearDates.TabIndex = 103
        lblClearDates.Text = "      "
        ' 
        ' dtpDateFrom
        ' 
        dtpDateFrom.CustomFormat = " "
        dtpDateFrom.Format = DateTimePickerFormat.Custom
        dtpDateFrom.Location = New Point(11, 59)
        dtpDateFrom.Margin = New Padding(4, 3, 4, 3)
        dtpDateFrom.Name = "dtpDateFrom"
        dtpDateFrom.Size = New Size(129, 31)
        dtpDateFrom.TabIndex = 101
        ' 
        ' lblEligibility
        ' 
        lblEligibility.Location = New Point(1084, 177)
        lblEligibility.Margin = New Padding(5, 6, 5, 6)
        lblEligibility.Name = "lblEligibility"
        lblEligibility.Size = New Size(160, 53)
        lblEligibility.TabIndex = 100
        lblEligibility.Text = "Eligibility"
        lblEligibility.UseVisualStyleBackColor = True
        ' 
        ' btnEECC
        ' 
        btnEECC.Location = New Point(789, 177)
        btnEECC.Margin = New Padding(5, 6, 5, 6)
        btnEECC.Name = "btnEECC"
        btnEECC.Size = New Size(169, 58)
        btnEECC.TabIndex = 98
        btnEECC.Text = "Check Eligibility"
        btnEECC.UseVisualStyleBackColor = True
        ' 
        ' dgvDiscrete
        ' 
        dgvDiscrete.AllowUserToAddRows = False
        dgvDiscrete.AllowUserToDeleteRows = False
        DataGridViewCellStyle6.BackColor = Color.FromArgb(CByte(255), CByte(224), CByte(192))
        dgvDiscrete.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle6
        dgvDiscrete.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvDiscrete.Columns.AddRange(New DataGridViewColumn() {Discrete})
        dgvDiscrete.Location = New Point(341, 25)
        dgvDiscrete.Margin = New Padding(5, 6, 5, 6)
        dgvDiscrete.Name = "dgvDiscrete"
        dgvDiscrete.RowHeadersVisible = False
        dgvDiscrete.RowHeadersWidth = 51
        dgvDiscrete.ScrollBars = ScrollBars.Vertical
        dgvDiscrete.Size = New Size(249, 197)
        dgvDiscrete.TabIndex = 19
        ' 
        ' Discrete
        ' 
        Discrete.FillWeight = 125F
        Discrete.HeaderText = "Discrete"
        Discrete.MaxInputLength = 16
        Discrete.MinimumWidth = 6
        Discrete.Name = "Discrete"
        Discrete.SortMode = DataGridViewColumnSortMode.NotSortable
        Discrete.Width = 125
        ' 
        ' loading
        ' 
        loading.Image = CType(resources.GetObject("loading.Image"), Image)
        loading.Location = New Point(849, 183)
        loading.Margin = New Padding(5, 0, 5, 0)
        loading.Name = "loading"
        loading.Size = New Size(45, 64)
        loading.TabIndex = 99
        loading.Visible = False
        ' 
        ' btnBillRev
        ' 
        btnBillRev.ForeColor = Color.DarkBlue
        btnBillRev.Location = New Point(1215, 425)
        btnBillRev.Margin = New Padding(5, 6, 5, 6)
        btnBillRev.Name = "btnBillRev"
        btnBillRev.Size = New Size(130, 81)
        btnBillRev.TabIndex = 87
        btnBillRev.Text = "Commit"
        btnBillRev.TextImageRelation = TextImageRelation.ImageBeforeText
        btnBillRev.UseVisualStyleBackColor = True
        ' 
        ' txtPreAuth
        ' 
        txtPreAuth.Location = New Point(1106, 561)
        txtPreAuth.Margin = New Padding(5, 6, 5, 6)
        txtPreAuth.MaxLength = 90
        txtPreAuth.Name = "txtPreAuth"
        txtPreAuth.Size = New Size(238, 31)
        txtPreAuth.TabIndex = 88
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(1104, 525)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(176, 25)
        Label2.TabIndex = 89
        Label2.Text = "Pre-Authorization"
        ' 
        ' VoidClaim
        ' 
        VoidClaim.ForeColor = Color.DarkBlue
        VoidClaim.Location = New Point(695, 519)
        VoidClaim.Margin = New Padding(5, 6, 5, 6)
        VoidClaim.Name = "VoidClaim"
        VoidClaim.Size = New Size(111, 41)
        VoidClaim.TabIndex = 95
        VoidClaim.Text = "Void"
        VoidClaim.UseVisualStyleBackColor = True
        ' 
        ' Corrected
        ' 
        Corrected.ForeColor = Color.DarkBlue
        Corrected.Location = New Point(550, 519)
        Corrected.Margin = New Padding(5, 6, 5, 6)
        Corrected.Name = "Corrected"
        Corrected.Size = New Size(126, 41)
        Corrected.TabIndex = 94
        Corrected.Text = "Corrected"
        Corrected.UseVisualStyleBackColor = True
        ' 
        ' Label9
        ' 
        Label9.ForeColor = Color.DarkBlue
        Label9.Location = New Point(816, 522)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(96, 25)
        Label9.TabIndex = 97
        Label9.Text = "Claim No"
        Label9.TextAlign = ContentAlignment.TopCenter
        ' 
        ' frmBillingEdit
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1365, 1311)
        Controls.Add(Label9)
        Controls.Add(orgChargID)
        Controls.Add(VoidClaim)
        Controls.Add(Corrected)
        Controls.Add(Label2)
        Controls.Add(txtPreAuth)
        Controls.Add(btnBillRev)
        Controls.Add(dgvComments)
        Controls.Add(chkECC)
        Controls.Add(txtBox19)
        Controls.Add(Label1)
        Controls.Add(cmbInvoices)
        Controls.Add(Label56)
        Controls.Add(chkRev)
        Controls.Add(Label54)
        Controls.Add(txtUCharges)
        Controls.Add(Label53)
        Controls.Add(txtBillDate)
        Controls.Add(btnHistory)
        Controls.Add(Label46)
        Controls.Add(Label45)
        Controls.Add(txtTerm)
        Controls.Add(txtTax)
        Controls.Add(txtStatus)
        Controls.Add(Label44)
        Controls.Add(chkIsBilled)
        Controls.Add(btnDxSync)
        Controls.Add(Label41)
        Controls.Add(txtDueDays)
        Controls.Add(Label42)
        Controls.Add(txtDueDate)
        Controls.Add(chkBillDate)
        Controls.Add(Label11)
        Controls.Add(txtBCharges)
        Controls.Add(Label10)
        Controls.Add(DGVICD9s)
        Controls.Add(TabControl1)
        Controls.Add(chkBillNow)
        Controls.Add(chkIsGratis)
        Controls.Add(txtSvcDate)
        Controls.Add(Label8)
        Controls.Add(Label7)
        Controls.Add(Label6)
        Controls.Add(txtAccessionID)
        Controls.Add(GroupBox2)
        Controls.Add(GroupBox1)
        Controls.Add(ToolStrip1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimumSize = New Size(1366, 1223)
        Name = "frmBillingEdit"
        Text = "Billing Primary"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        TabControl1.ResumeLayout(False)
        tpClient.ResumeLayout(False)
        tpClient.PerformLayout()
        tpThirdParty.ResumeLayout(False)
        gbTP.ResumeLayout(False)
        gbTP.PerformLayout()
        CType(pctInsSex, ComponentModel.ISupportInitialize).EndInit()
        SecondaryIns.ResumeLayout(False)
        grpSSubs.ResumeLayout(False)
        grpSSubs.PerformLayout()
        GroupBox3.ResumeLayout(False)
        GroupBox3.PerformLayout()
        tpPatient.ResumeLayout(False)
        tpPatient.PerformLayout()
        CType(pctPatSex, ComponentModel.ISupportInitialize).EndInit()
        tpCharges.ResumeLayout(False)
        CType(dgvCharges, ComponentModel.ISupportInitialize).EndInit()
        CType(DGVICD9s, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvComments, ComponentModel.ISupportInitialize).EndInit()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        CType(dgvDiscrete, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnLast As System.Windows.Forms.Button
    Friend WithEvents txtNavStatus As System.Windows.Forms.TextBox
    Friend WithEvents btnFirst As System.Windows.Forms.Button
    Friend WithEvents btnPrevious As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtAccessionID As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtSvcDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents chkIsGratis As System.Windows.Forms.CheckBox
    Friend WithEvents chkBillNow As System.Windows.Forms.CheckBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tpClient As System.Windows.Forms.TabPage
    Friend WithEvents DGVICD9s As System.Windows.Forms.DataGridView
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtBCharges As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents tpThirdParty As System.Windows.Forms.TabPage
    Friend WithEvents tpPatient As System.Windows.Forms.TabPage
    Friend WithEvents tpCharges As System.Windows.Forms.TabPage
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtProviderID As System.Windows.Forms.TextBox
    Friend WithEvents chkClientBill As System.Windows.Forms.CheckBox
    Friend WithEvents chkTPBill As System.Windows.Forms.CheckBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents chkPatientBill As System.Windows.Forms.CheckBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cmbPayer As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtPolicy As System.Windows.Forms.TextBox
    Friend WithEvents btnPayers As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtCopay As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtGroup As System.Windows.Forms.TextBox
    Friend WithEvents cmbRelation As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtInsuredID As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents btnInsLookup As System.Windows.Forms.Button
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtInsName As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtInsAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtInsSex As System.Windows.Forms.TextBox
    Friend WithEvents pctInsSex As System.Windows.Forms.PictureBox
    Friend WithEvents txtInsDOB As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtPatDOB As System.Windows.Forms.MaskedTextBox
    Friend WithEvents pctPatSex As System.Windows.Forms.PictureBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtPatAdd1 As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtPatLName As System.Windows.Forms.TextBox
    Friend WithEvents txtPatEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtPatSSN As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnPatLook As System.Windows.Forms.Button
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtPatSex As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtPatientID As System.Windows.Forms.TextBox
    Friend WithEvents btnProviderLook As System.Windows.Forms.Button
    Friend WithEvents txtProviderName As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents txtProviderAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents cmbPatPrice As System.Windows.Forms.ComboBox
    Friend WithEvents cmbTPPrice As System.Windows.Forms.ComboBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents cmbProvPrice As System.Windows.Forms.ComboBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents chkTPContract As System.Windows.Forms.CheckBox
    Friend WithEvents chkProvContract As System.Windows.Forms.CheckBox
    Friend WithEvents dgvCharges As System.Windows.Forms.DataGridView
    Friend WithEvents chkBillDate As System.Windows.Forms.CheckBox
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents txtDueDays As System.Windows.Forms.TextBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents txtDueDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnDxSync As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents chkIsBilled As System.Windows.Forms.CheckBox
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents lstAttending As System.Windows.Forms.CheckedListBox
    Friend WithEvents txtTax As System.Windows.Forms.TextBox
    Friend WithEvents txtTerm As System.Windows.Forms.TextBox
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents btnHistory As System.Windows.Forms.Button
    Friend WithEvents txtPatMName As System.Windows.Forms.TextBox
    Friend WithEvents txtPatFName As System.Windows.Forms.TextBox
    Friend WithEvents txtPatZip As System.Windows.Forms.TextBox
    Friend WithEvents txtPatCity As System.Windows.Forms.TextBox
    Friend WithEvents txtPatAdd2 As System.Windows.Forms.TextBox
    Friend WithEvents txtPatHPhone As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtPatCell As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPatState As System.Windows.Forms.TextBox
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents txtProvFax As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtProvPhone As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents txtBillDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents txtUCharges As System.Windows.Forms.TextBox
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents chkRev As System.Windows.Forms.CheckBox
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents cmbInvoices As System.Windows.Forms.ComboBox
    Friend WithEvents gbTP As System.Windows.Forms.GroupBox
    Friend WithEvents txtNPI As System.Windows.Forms.TextBox
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents SNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ICD9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LookUp As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtBox19 As System.Windows.Forms.TextBox
    Friend WithEvents chkECC As System.Windows.Forms.CheckBox
    Friend WithEvents dgvComments As System.Windows.Forms.DataGridView
    Friend WithEvents cmbBillType As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents txtAccFrom As System.Windows.Forms.TextBox
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtAccTo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbABU As System.Windows.Forms.ComboBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents lstTargets As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnDesel As System.Windows.Forms.Button
    Friend WithEvents btnSel As System.Windows.Forms.Button
    Friend WithEvents btnTarget As System.Windows.Forms.Button
    Friend WithEvents chkAI As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvDiscrete As System.Windows.Forms.DataGridView
    Friend WithEvents Discrete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnBillRev As System.Windows.Forms.Button
    Friend WithEvents Dated As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ABP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cmnt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents User As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DEL As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents TGPID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tgpLook As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents TGPName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CPT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Dx As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents M1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents M2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents M3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents M4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Price As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Extend As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents POS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents STATUS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BillDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Biller As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtPreAuth As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents SecondaryIns As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSCopay As System.Windows.Forms.TextBox
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents cmbSRelation As System.Windows.Forms.ComboBox
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents txtSFrom As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtSTo As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtSGroup As System.Windows.Forms.TextBox
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents cmbSIns As System.Windows.Forms.ComboBox
    Friend WithEvents txtSPolicy As System.Windows.Forms.TextBox
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents grpSSubs As System.Windows.Forms.GroupBox
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents txtSSubEmployer As System.Windows.Forms.TextBox
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents txtSSubCell As System.Windows.Forms.TextBox
    Friend WithEvents txtSSubSSN As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents txtSSubDOB As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cmbSSubSex As System.Windows.Forms.ComboBox
    Friend WithEvents btnSSubLook As System.Windows.Forms.Button
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents txtSSubCountry As System.Windows.Forms.TextBox
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents txtSSubEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents txtSSubWPhone As System.Windows.Forms.TextBox
    Friend WithEvents txtSSubHPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents txtSSubZip As System.Windows.Forms.TextBox
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents txtSSubState As System.Windows.Forms.TextBox
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents txtSSubCity As System.Windows.Forms.TextBox
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents txtSSubAdd2 As System.Windows.Forms.TextBox
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents txtSSubAdd1 As System.Windows.Forms.TextBox
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents txtSSubMName As System.Windows.Forms.TextBox
    Friend WithEvents Label79 As System.Windows.Forms.Label
    Friend WithEvents txtSSubFName As System.Windows.Forms.TextBox
    Friend WithEvents Label80 As System.Windows.Forms.Label
    Friend WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents txtSSubLName As System.Windows.Forms.TextBox
    Friend WithEvents Label83 As System.Windows.Forms.Label
    Friend WithEvents txtSSubID As System.Windows.Forms.TextBox
    Friend WithEvents conractS As System.Windows.Forms.CheckBox
    Friend WithEvents VoidClaim As System.Windows.Forms.CheckBox
    Friend WithEvents Corrected As System.Windows.Forms.CheckBox
    Friend WithEvents orgChargID As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnEECC As Button
    Friend WithEvents loading As Label
    Friend WithEvents lblEligibility As Button
    Friend WithEvents dtpDateTo As DateTimePicker
    Friend WithEvents lblClearDates As Label
    Friend WithEvents dtpDateFrom As DateTimePicker
    Friend WithEvents Button1 As Button
End Class
