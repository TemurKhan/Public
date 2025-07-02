<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProviders
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProviders))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        chkEditNew = New ToolStripButton()
        ToolStripButton2 = New ToolStripSeparator()
        btnSave = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnDelete = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        tcProvider = New TabControl()
        tpProvider = New TabPage()
        Label54 = New Label()
        txtPOS = New TextBox()
        btnExternalFeed = New Button()
        btnAssAdd = New Button()
        btnAssDel = New Button()
        txtAssAddress = New TextBox()
        Label50 = New Label()
        chkAssActive = New CheckBox()
        Label49 = New Label()
        Label48 = New Label()
        Label47 = New Label()
        btnAssLookUp = New Button()
        txtAssName = New TextBox()
        txtAssID = New TextBox()
        chkUserMgmt = New CheckBox()
        btnPIColor = New Button()
        btnPIFont = New Button()
        Label46 = New Label()
        txtPanicInstructions = New RichTextBox()
        txtCell = New MaskedTextBox()
        txtFax = New MaskedTextBox()
        txtPhone = New MaskedTextBox()
        dgvProviders = New DataGridView()
        ProvID = New DataGridViewTextBoxColumn()
        ProvName = New DataGridViewTextBoxColumn()
        Active = New DataGridViewCheckBoxColumn()
        Label35 = New Label()
        txtNote = New TextBox()
        Label29 = New Label()
        txtContact = New TextBox()
        Label28 = New Label()
        txtLicense = New TextBox()
        Label27 = New Label()
        txtPassword = New TextBox()
        Label26 = New Label()
        txtUserName = New TextBox()
        Label25 = New Label()
        chkActive = New CheckBox()
        txtCountry = New TextBox()
        Label21 = New Label()
        txtZip = New TextBox()
        Label20 = New Label()
        txtState = New TextBox()
        Label19 = New Label()
        txtCity = New TextBox()
        Label18 = New Label()
        txtAdd2 = New TextBox()
        Label17 = New Label()
        txtAdd1 = New TextBox()
        Label16 = New Label()
        txtCommDLL = New TextBox()
        Label15 = New Label()
        txtEmail = New TextBox()
        Label14 = New Label()
        btnProvLook = New Button()
        txtProviderID = New TextBox()
        Label13 = New Label()
        Label12 = New Label()
        Label11 = New Label()
        Label10 = New Label()
        txtBCBS = New TextBox()
        Label9 = New Label()
        txtMCD = New TextBox()
        Label8 = New Label()
        txtMCR = New TextBox()
        Label7 = New Label()
        txtCLIA = New TextBox()
        Label6 = New Label()
        txtUPIN = New TextBox()
        Label5 = New Label()
        lblSSN = New Label()
        txtSSN = New MaskedTextBox()
        txtNPI = New TextBox()
        Label4 = New Label()
        txtDegree = New TextBox()
        Label3 = New Label()
        txtMName = New TextBox()
        Label2 = New Label()
        txtFName = New TextBox()
        lblFName = New Label()
        txtLName = New TextBox()
        lblLName = New Label()
        chkIndividual = New CheckBox()
        tpRDM = New TabPage()
        Label37 = New Label()
        Label44 = New Label()
        cmbPickUp = New ComboBox()
        txtPickupNote = New TextBox()
        btnRoutes = New Button()
        Label33 = New Label()
        GrpHours = New GroupBox()
        dgvHours = New DataGridView()
        WeekDay = New DataGridViewTextBoxColumn()
        DayStart = New DataGridViewComboBoxColumn()
        LunchStart = New DataGridViewComboBoxColumn()
        LunchStop = New DataGridViewComboBoxColumn()
        DayStop = New DataGridViewComboBoxColumn()
        cmbRoutes = New ComboBox()
        btnSales = New Button()
        grpRDM = New GroupBox()
        btnInvFileLook = New Button()
        txtInvoiceRPTFile = New TextBox()
        Label53 = New Label()
        chkORNecessity = New CheckBox()
        chkBlockDemograph = New CheckBox()
        chkUseMyReports = New CheckBox()
        chkDOCRequired = New CheckBox()
        chkPatPhRequired = New CheckBox()
        chkServerPDF = New CheckBox()
        chkAccConsolidate = New CheckBox()
        chkUseESRD = New CheckBox()
        chkChartRequired = New CheckBox()
        chkAuto = New CheckBox()
        chkProlison = New CheckBox()
        chkInterface = New CheckBox()
        chkEmail = New CheckBox()
        chkPrint = New CheckBox()
        chkFax = New CheckBox()
        btnRptLook = New Button()
        txtResRPTFile = New TextBox()
        Label45 = New Label()
        chkSetExt = New CheckBox()
        Label43 = New Label()
        Label42 = New Label()
        Label41 = New Label()
        txtIncSpan = New TextBox()
        txtCompSpan = New TextBox()
        Label40 = New Label()
        txtAutoTime = New MaskedTextBox()
        Label38 = New Label()
        txtExtCmnt = New TextBox()
        Label36 = New Label()
        txtCopies = New TextBox()
        cmbReport = New ComboBox()
        Label34 = New Label()
        cmbRegular = New ComboBox()
        Label24 = New Label()
        Label23 = New Label()
        chkComplete = New CheckBox()
        cmbPanic = New ComboBox()
        Label22 = New Label()
        Label32 = New Label()
        cmbSales = New ComboBox()
        tpContract = New TabPage()
        cmbDxSearch = New ComboBox()
        Label52 = New Label()
        Label51 = New Label()
        cmbDefaultBilling = New ComboBox()
        btnCopyContract = New Button()
        Label31 = New Label()
        Label30 = New Label()
        dtpTo = New DateTimePicker()
        dtpFrom = New DateTimePicker()
        Label1 = New Label()
        cmbPriceLevel = New ComboBox()
        dgvContract = New DataGridView()
        DEL = New DataGridViewImageColumn()
        ID = New DataGridViewTextBoxColumn()
        Look = New DataGridViewImageColumn()
        Component = New DataGridViewTextBoxColumn()
        Logo = New DataGridViewImageColumn()
        CPT = New DataGridViewTextBoxColumn()
        DPA = New DataGridViewCheckBoxColumn()
        Price = New DataGridViewTextBoxColumn()
        tpAlert = New TabPage()
        btnDefault = New Button()
        btnBG = New Button()
        btnColor = New Button()
        Button1 = New Button()
        txtAlert = New RichTextBox()
        chkAcc = New CheckBox()
        chkCS = New CheckBox()
        Label39 = New Label()
        tpRanges = New TabPage()
        dgvAGRanges = New DataGridView()
        btnDel = New DataGridViewImageColumn()
        TestID = New DataGridViewTextBoxColumn()
        btnLookUp = New DataGridViewImageColumn()
        AGAgFrom = New DataGridViewTextBoxColumn()
        AGAgeTo = New DataGridViewTextBoxColumn()
        Sex = New DataGridViewComboBoxColumn()
        AGValueGrom = New DataGridViewTextBoxColumn()
        AGValueTo = New DataGridViewTextBoxColumn()
        AGFlag = New DataGridViewTextBoxColumn()
        Behavior = New DataGridViewComboBoxColumn()
        OpenFileDialog1 = New OpenFileDialog()
        ToolTip1 = New ToolTip(components)
        ToolStrip1.SuspendLayout()
        tcProvider.SuspendLayout()
        tpProvider.SuspendLayout()
        CType(dgvProviders, ComponentModel.ISupportInitialize).BeginInit()
        tpRDM.SuspendLayout()
        GrpHours.SuspendLayout()
        CType(dgvHours, ComponentModel.ISupportInitialize).BeginInit()
        grpRDM.SuspendLayout()
        tpContract.SuspendLayout()
        CType(dgvContract, ComponentModel.ISupportInitialize).BeginInit()
        tpAlert.SuspendLayout()
        tpRanges.SuspendLayout()
        CType(dgvAGRanges, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {chkEditNew, ToolStripButton2, btnSave, ToolStripSeparator1, btnDelete, ToolStripSeparator2, btnCancel, ToolStripSeparator3, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(1375, 34)
        ToolStrip1.TabIndex = 3
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' chkEditNew
        ' 
        chkEditNew.CheckOnClick = True
        chkEditNew.Image = CType(resources.GetObject("chkEditNew.Image"), Image)
        chkEditNew.ImageTransparentColor = Color.Magenta
        chkEditNew.Name = "chkEditNew"
        chkEditNew.Size = New Size(70, 29)
        chkEditNew.Text = "Edit"
        ' 
        ' ToolStripButton2
        ' 
        ToolStripButton2.Name = "ToolStripButton2"
        ToolStripButton2.Size = New Size(6, 34)
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
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
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
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 34)
        ' 
        ' btnCancel
        ' 
        btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), Image)
        btnCancel.ImageTransparentColor = Color.Magenta
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(91, 29)
        btnCancel.Text = "Cancel"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(6, 34)
        ' 
        ' btnHelp
        ' 
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.ImageTransparentColor = Color.Magenta
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(77, 29)
        btnHelp.Text = "Help"
        ' 
        ' tcProvider
        ' 
        tcProvider.Controls.Add(tpProvider)
        tcProvider.Controls.Add(tpRDM)
        tcProvider.Controls.Add(tpContract)
        tcProvider.Controls.Add(tpAlert)
        tcProvider.Controls.Add(tpRanges)
        tcProvider.Location = New Point(18, 67)
        tcProvider.Margin = New Padding(5, 6, 5, 6)
        tcProvider.Name = "tcProvider"
        tcProvider.SelectedIndex = 0
        tcProvider.Size = New Size(1337, 1092)
        tcProvider.TabIndex = 4
        ' 
        ' tpProvider
        ' 
        tpProvider.Controls.Add(Label54)
        tpProvider.Controls.Add(txtPOS)
        tpProvider.Controls.Add(btnExternalFeed)
        tpProvider.Controls.Add(btnAssAdd)
        tpProvider.Controls.Add(btnAssDel)
        tpProvider.Controls.Add(txtAssAddress)
        tpProvider.Controls.Add(Label50)
        tpProvider.Controls.Add(chkAssActive)
        tpProvider.Controls.Add(Label49)
        tpProvider.Controls.Add(Label48)
        tpProvider.Controls.Add(Label47)
        tpProvider.Controls.Add(btnAssLookUp)
        tpProvider.Controls.Add(txtAssName)
        tpProvider.Controls.Add(txtAssID)
        tpProvider.Controls.Add(chkUserMgmt)
        tpProvider.Controls.Add(btnPIColor)
        tpProvider.Controls.Add(btnPIFont)
        tpProvider.Controls.Add(Label46)
        tpProvider.Controls.Add(txtPanicInstructions)
        tpProvider.Controls.Add(txtCell)
        tpProvider.Controls.Add(txtFax)
        tpProvider.Controls.Add(txtPhone)
        tpProvider.Controls.Add(dgvProviders)
        tpProvider.Controls.Add(Label35)
        tpProvider.Controls.Add(txtNote)
        tpProvider.Controls.Add(Label29)
        tpProvider.Controls.Add(txtContact)
        tpProvider.Controls.Add(Label28)
        tpProvider.Controls.Add(txtLicense)
        tpProvider.Controls.Add(Label27)
        tpProvider.Controls.Add(txtPassword)
        tpProvider.Controls.Add(Label26)
        tpProvider.Controls.Add(txtUserName)
        tpProvider.Controls.Add(Label25)
        tpProvider.Controls.Add(chkActive)
        tpProvider.Controls.Add(txtCountry)
        tpProvider.Controls.Add(Label21)
        tpProvider.Controls.Add(txtZip)
        tpProvider.Controls.Add(Label20)
        tpProvider.Controls.Add(txtState)
        tpProvider.Controls.Add(Label19)
        tpProvider.Controls.Add(txtCity)
        tpProvider.Controls.Add(Label18)
        tpProvider.Controls.Add(txtAdd2)
        tpProvider.Controls.Add(Label17)
        tpProvider.Controls.Add(txtAdd1)
        tpProvider.Controls.Add(Label16)
        tpProvider.Controls.Add(txtCommDLL)
        tpProvider.Controls.Add(Label15)
        tpProvider.Controls.Add(txtEmail)
        tpProvider.Controls.Add(Label14)
        tpProvider.Controls.Add(btnProvLook)
        tpProvider.Controls.Add(txtProviderID)
        tpProvider.Controls.Add(Label13)
        tpProvider.Controls.Add(Label12)
        tpProvider.Controls.Add(Label11)
        tpProvider.Controls.Add(Label10)
        tpProvider.Controls.Add(txtBCBS)
        tpProvider.Controls.Add(Label9)
        tpProvider.Controls.Add(txtMCD)
        tpProvider.Controls.Add(Label8)
        tpProvider.Controls.Add(txtMCR)
        tpProvider.Controls.Add(Label7)
        tpProvider.Controls.Add(txtCLIA)
        tpProvider.Controls.Add(Label6)
        tpProvider.Controls.Add(txtUPIN)
        tpProvider.Controls.Add(Label5)
        tpProvider.Controls.Add(lblSSN)
        tpProvider.Controls.Add(txtSSN)
        tpProvider.Controls.Add(txtNPI)
        tpProvider.Controls.Add(Label4)
        tpProvider.Controls.Add(txtDegree)
        tpProvider.Controls.Add(Label3)
        tpProvider.Controls.Add(txtMName)
        tpProvider.Controls.Add(Label2)
        tpProvider.Controls.Add(txtFName)
        tpProvider.Controls.Add(lblFName)
        tpProvider.Controls.Add(txtLName)
        tpProvider.Controls.Add(lblLName)
        tpProvider.Controls.Add(chkIndividual)
        tpProvider.Location = New Point(4, 34)
        tpProvider.Margin = New Padding(5, 6, 5, 6)
        tpProvider.Name = "tpProvider"
        tpProvider.Padding = New Padding(5, 6, 5, 6)
        tpProvider.Size = New Size(1329, 1054)
        tpProvider.TabIndex = 0
        tpProvider.Text = "Provider"
        tpProvider.UseVisualStyleBackColor = True
        ' 
        ' Label54
        ' 
        Label54.ForeColor = Color.DarkBlue
        Label54.Location = New Point(1055, 117)
        Label54.Margin = New Padding(5, 0, 5, 0)
        Label54.Name = "Label54"
        Label54.Size = New Size(147, 27)
        Label54.TabIndex = 78
        Label54.Text = "Sp. POS Code"
        ' 
        ' txtPOS
        ' 
        txtPOS.Location = New Point(1045, 150)
        txtPOS.Margin = New Padding(5, 6, 5, 6)
        txtPOS.MaxLength = 3
        txtPOS.Name = "txtPOS"
        txtPOS.Size = New Size(156, 31)
        txtPOS.TabIndex = 12
        txtPOS.TextAlign = HorizontalAlignment.Center
        ToolTip1.SetToolTip(txtPOS, resources.GetString("txtPOS.ToolTip"))
        ' 
        ' btnExternalFeed
        ' 
        btnExternalFeed.Location = New Point(1097, 367)
        btnExternalFeed.Margin = New Padding(5, 6, 5, 6)
        btnExternalFeed.Name = "btnExternalFeed"
        btnExternalFeed.Size = New Size(105, 75)
        btnExternalFeed.TabIndex = 76
        btnExternalFeed.Text = "External Feed"
        btnExternalFeed.UseVisualStyleBackColor = True
        ' 
        ' btnAssAdd
        ' 
        btnAssAdd.Enabled = False
        btnAssAdd.Image = CType(resources.GetObject("btnAssAdd.Image"), Image)
        btnAssAdd.Location = New Point(547, 746)
        btnAssAdd.Margin = New Padding(5, 6, 5, 6)
        btnAssAdd.Name = "btnAssAdd"
        btnAssAdd.Size = New Size(43, 48)
        btnAssAdd.TabIndex = 75
        btnAssAdd.UseVisualStyleBackColor = True
        ' 
        ' btnAssDel
        ' 
        btnAssDel.Enabled = False
        btnAssDel.Image = CType(resources.GetObject("btnAssDel.Image"), Image)
        btnAssDel.Location = New Point(493, 746)
        btnAssDel.Margin = New Padding(5, 6, 5, 6)
        btnAssDel.Name = "btnAssDel"
        btnAssDel.Size = New Size(43, 48)
        btnAssDel.TabIndex = 74
        btnAssDel.UseVisualStyleBackColor = True
        ' 
        ' txtAssAddress
        ' 
        txtAssAddress.Location = New Point(158, 894)
        txtAssAddress.Margin = New Padding(5, 6, 5, 6)
        txtAssAddress.MaxLength = 150
        txtAssAddress.Name = "txtAssAddress"
        txtAssAddress.ReadOnly = True
        txtAssAddress.Size = New Size(429, 31)
        txtAssAddress.TabIndex = 73
        ' 
        ' Label50
        ' 
        Label50.ForeColor = Color.DarkBlue
        Label50.Location = New Point(162, 858)
        Label50.Margin = New Padding(5, 0, 5, 0)
        Label50.Name = "Label50"
        Label50.Size = New Size(225, 27)
        Label50.TabIndex = 72
        Label50.Text = "Provider/Entity Address"
        Label50.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' chkAssActive
        ' 
        chkAssActive.Appearance = Appearance.Button
        chkAssActive.Checked = True
        chkAssActive.CheckState = CheckState.Checked
        chkAssActive.ForeColor = Color.DarkBlue
        chkAssActive.Location = New Point(20, 890)
        chkAssActive.Margin = New Padding(5, 6, 5, 6)
        chkAssActive.Name = "chkAssActive"
        chkAssActive.Size = New Size(127, 44)
        chkAssActive.TabIndex = 71
        chkAssActive.Text = "Active"
        chkAssActive.TextAlign = ContentAlignment.MiddleCenter
        chkAssActive.UseVisualStyleBackColor = True
        ' 
        ' Label49
        ' 
        Label49.ForeColor = Color.DarkBlue
        Label49.Location = New Point(27, 858)
        Label49.Margin = New Padding(5, 0, 5, 0)
        Label49.Name = "Label49"
        Label49.Size = New Size(120, 27)
        Label49.TabIndex = 70
        Label49.Text = "Association"
        Label49.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label48
        ' 
        Label48.ForeColor = Color.DarkBlue
        Label48.Location = New Point(220, 767)
        Label48.Margin = New Padding(5, 0, 5, 0)
        Label48.Name = "Label48"
        Label48.Size = New Size(225, 27)
        Label48.TabIndex = 69
        Label48.Text = "Provider/Entity Name"
        Label48.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label47
        ' 
        Label47.ForeColor = Color.DarkBlue
        Label47.Location = New Point(28, 767)
        Label47.Margin = New Padding(5, 0, 5, 0)
        Label47.Name = "Label47"
        Label47.Size = New Size(135, 27)
        Label47.TabIndex = 68
        Label47.Text = "Prov/Entity ID"
        Label47.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' btnAssLookUp
        ' 
        btnAssLookUp.Image = CType(resources.GetObject("btnAssLookUp.Image"), Image)
        btnAssLookUp.Location = New Point(157, 794)
        btnAssLookUp.Margin = New Padding(5, 6, 5, 6)
        btnAssLookUp.Name = "btnAssLookUp"
        btnAssLookUp.Size = New Size(43, 48)
        btnAssLookUp.TabIndex = 67
        btnAssLookUp.UseVisualStyleBackColor = True
        ' 
        ' txtAssName
        ' 
        txtAssName.Location = New Point(210, 800)
        txtAssName.Margin = New Padding(5, 6, 5, 6)
        txtAssName.MaxLength = 60
        txtAssName.Name = "txtAssName"
        txtAssName.ReadOnly = True
        txtAssName.Size = New Size(377, 31)
        txtAssName.TabIndex = 66
        ' 
        ' txtAssID
        ' 
        txtAssID.Location = New Point(20, 800)
        txtAssID.Margin = New Padding(5, 6, 5, 6)
        txtAssID.MaxLength = 12
        txtAssID.Name = "txtAssID"
        txtAssID.Size = New Size(124, 31)
        txtAssID.TabIndex = 65
        txtAssID.TextAlign = HorizontalAlignment.Center
        ' 
        ' chkUserMgmt
        ' 
        chkUserMgmt.BackColor = Color.Transparent
        chkUserMgmt.CheckAlign = ContentAlignment.MiddleRight
        chkUserMgmt.ForeColor = Color.Brown
        chkUserMgmt.Location = New Point(770, 313)
        chkUserMgmt.Margin = New Padding(5, 6, 5, 6)
        chkUserMgmt.Name = "chkUserMgmt"
        chkUserMgmt.Size = New Size(133, 35)
        chkUserMgmt.TabIndex = 23
        chkUserMgmt.Text = "User Mgmt"
        chkUserMgmt.UseVisualStyleBackColor = False
        ' 
        ' btnPIColor
        ' 
        btnPIColor.Image = CType(resources.GetObject("btnPIColor.Image"), Image)
        btnPIColor.Location = New Point(1045, 685)
        btnPIColor.Margin = New Padding(5, 6, 5, 6)
        btnPIColor.Name = "btnPIColor"
        btnPIColor.Size = New Size(48, 44)
        btnPIColor.TabIndex = 64
        btnPIColor.UseVisualStyleBackColor = True
        ' 
        ' btnPIFont
        ' 
        btnPIFont.Image = CType(resources.GetObject("btnPIFont.Image"), Image)
        btnPIFont.Location = New Point(1103, 685)
        btnPIFont.Margin = New Padding(5, 6, 5, 6)
        btnPIFont.Name = "btnPIFont"
        btnPIFont.Size = New Size(48, 44)
        btnPIFont.TabIndex = 63
        btnPIFont.UseVisualStyleBackColor = True
        ' 
        ' Label46
        ' 
        Label46.ForeColor = Color.DarkBlue
        Label46.Location = New Point(612, 702)
        Label46.Margin = New Padding(5, 0, 5, 0)
        Label46.Name = "Label46"
        Label46.Size = New Size(178, 27)
        Label46.TabIndex = 62
        Label46.Text = "Panic Instructions"
        Label46.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' txtPanicInstructions
        ' 
        txtPanicInstructions.Location = New Point(603, 735)
        txtPanicInstructions.Margin = New Padding(5, 6, 5, 6)
        txtPanicInstructions.MaxLength = 4000
        txtPanicInstructions.Name = "txtPanicInstructions"
        txtPanicInstructions.ScrollBars = RichTextBoxScrollBars.Vertical
        txtPanicInstructions.Size = New Size(597, 196)
        txtPanicInstructions.TabIndex = 32
        txtPanicInstructions.Text = ""
        ' 
        ' txtCell
        ' 
        txtCell.Location = New Point(933, 233)
        txtCell.Margin = New Padding(5, 6, 5, 6)
        txtCell.Name = "txtCell"
        txtCell.Size = New Size(267, 31)
        txtCell.TabIndex = 18
        txtCell.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
        ' 
        ' txtFax
        ' 
        txtFax.Location = New Point(702, 233)
        txtFax.Margin = New Padding(5, 6, 5, 6)
        txtFax.Name = "txtFax"
        txtFax.Size = New Size(199, 31)
        txtFax.TabIndex = 17
        txtFax.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
        ' 
        ' txtPhone
        ' 
        txtPhone.Location = New Point(502, 233)
        txtPhone.Margin = New Padding(5, 6, 5, 6)
        txtPhone.Name = "txtPhone"
        txtPhone.Size = New Size(184, 31)
        txtPhone.TabIndex = 16
        txtPhone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
        ' 
        ' dgvProviders
        ' 
        dgvProviders.AllowUserToAddRows = False
        dgvProviders.AllowUserToDeleteRows = False
        dgvProviders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvProviders.Columns.AddRange(New DataGridViewColumn() {ProvID, ProvName, Active})
        dgvProviders.Location = New Point(20, 475)
        dgvProviders.Margin = New Padding(5, 6, 5, 6)
        dgvProviders.Name = "dgvProviders"
        dgvProviders.ReadOnly = True
        dgvProviders.RowHeadersVisible = False
        dgvProviders.RowHeadersWidth = 62
        dgvProviders.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvProviders.Size = New Size(570, 254)
        dgvProviders.TabIndex = 32
        ' 
        ' ProvID
        ' 
        ProvID.FillWeight = 70F
        ProvID.HeaderText = "ID"
        ProvID.MinimumWidth = 8
        ProvID.Name = "ProvID"
        ProvID.ReadOnly = True
        ProvID.Width = 126
        ' 
        ' ProvName
        ' 
        ProvName.FillWeight = 206F
        ProvName.HeaderText = "Name"
        ProvName.MinimumWidth = 8
        ProvName.Name = "ProvName"
        ProvName.ReadOnly = True
        ProvName.Width = 369
        ' 
        ' Active
        ' 
        Active.FillWeight = 40F
        Active.HeaderText = "Active"
        Active.MinimumWidth = 8
        Active.Name = "Active"
        Active.ReadOnly = True
        Active.Width = 72
        ' 
        ' Label35
        ' 
        Label35.ForeColor = Color.DarkBlue
        Label35.Location = New Point(28, 442)
        Label35.Margin = New Padding(5, 0, 5, 0)
        Label35.Name = "Label35"
        Label35.Size = New Size(225, 27)
        Label35.TabIndex = 59
        Label35.Text = "Provider Association"
        Label35.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' txtNote
        ' 
        txtNote.Location = New Point(603, 475)
        txtNote.Margin = New Padding(5, 6, 5, 6)
        txtNote.MaxLength = 4000
        txtNote.Multiline = True
        txtNote.Name = "txtNote"
        txtNote.ScrollBars = ScrollBars.Vertical
        txtNote.Size = New Size(597, 192)
        txtNote.TabIndex = 31
        ' 
        ' Label29
        ' 
        Label29.ForeColor = Color.DarkBlue
        Label29.Location = New Point(612, 442)
        Label29.Margin = New Padding(5, 0, 5, 0)
        Label29.Name = "Label29"
        Label29.Size = New Size(65, 27)
        Label29.TabIndex = 57
        Label29.Text = "Note:"
        Label29.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' txtContact
        ' 
        txtContact.Location = New Point(933, 310)
        txtContact.Margin = New Padding(5, 6, 5, 6)
        txtContact.MaxLength = 60
        txtContact.Name = "txtContact"
        txtContact.Size = New Size(267, 31)
        txtContact.TabIndex = 24
        ' 
        ' Label28
        ' 
        Label28.ForeColor = Color.DarkBlue
        Label28.Location = New Point(942, 277)
        Label28.Margin = New Padding(5, 0, 5, 0)
        Label28.Name = "Label28"
        Label28.Size = New Size(152, 27)
        Label28.TabIndex = 55
        Label28.Text = "Office Contact"
        ' 
        ' txtLicense
        ' 
        txtLicense.Location = New Point(315, 150)
        txtLicense.Margin = New Padding(5, 6, 5, 6)
        txtLicense.MaxLength = 25
        txtLicense.Name = "txtLicense"
        txtLicense.Size = New Size(136, 31)
        txtLicense.TabIndex = 8
        ToolTip1.SetToolTip(txtLicense, "Conditionally required" & vbCrLf & "License or NPI or UPIN")
        ' 
        ' Label27
        ' 
        Label27.ForeColor = Color.Fuchsia
        Label27.Location = New Point(328, 117)
        Label27.Margin = New Padding(5, 0, 5, 0)
        Label27.Name = "Label27"
        Label27.Size = New Size(117, 27)
        Label27.TabIndex = 53
        Label27.Text = "License"
        ' 
        ' txtPassword
        ' 
        txtPassword.BackColor = Color.Honeydew
        txtPassword.Location = New Point(603, 310)
        txtPassword.Margin = New Padding(5, 6, 5, 6)
        txtPassword.MaxLength = 20
        txtPassword.Name = "txtPassword"
        txtPassword.Size = New Size(139, 31)
        txtPassword.TabIndex = 22
        ' 
        ' Label26
        ' 
        Label26.ForeColor = Color.Brown
        Label26.Location = New Point(632, 277)
        Label26.Margin = New Padding(5, 0, 5, 0)
        Label26.Name = "Label26"
        Label26.Size = New Size(93, 27)
        Label26.TabIndex = 51
        Label26.Text = "Password"
        ' 
        ' txtUserName
        ' 
        txtUserName.BackColor = Color.Honeydew
        txtUserName.Location = New Point(423, 310)
        txtUserName.Margin = New Padding(5, 6, 5, 6)
        txtUserName.MaxLength = 20
        txtUserName.Name = "txtUserName"
        txtUserName.Size = New Size(164, 31)
        txtUserName.TabIndex = 21
        ' 
        ' Label25
        ' 
        Label25.ForeColor = Color.Brown
        Label25.Location = New Point(438, 277)
        Label25.Margin = New Padding(5, 0, 5, 0)
        Label25.Name = "Label25"
        Label25.Size = New Size(108, 27)
        Label25.TabIndex = 49
        Label25.Text = "User Name"
        ' 
        ' chkActive
        ' 
        chkActive.Appearance = Appearance.Button
        chkActive.Checked = True
        chkActive.CheckState = CheckState.Checked
        chkActive.ForeColor = Color.DarkBlue
        chkActive.Location = New Point(1103, 10)
        chkActive.Margin = New Padding(5, 6, 5, 6)
        chkActive.Name = "chkActive"
        chkActive.Size = New Size(100, 44)
        chkActive.TabIndex = 29
        chkActive.Text = "Active"
        chkActive.TextAlign = ContentAlignment.MiddleCenter
        chkActive.UseVisualStyleBackColor = True
        ' 
        ' txtCountry
        ' 
        txtCountry.Location = New Point(933, 390)
        txtCountry.Margin = New Padding(5, 6, 5, 6)
        txtCountry.MaxLength = 35
        txtCountry.Name = "txtCountry"
        txtCountry.Size = New Size(139, 31)
        txtCountry.TabIndex = 30
        ' 
        ' Label21
        ' 
        Label21.ForeColor = Color.DarkBlue
        Label21.Location = New Point(933, 358)
        Label21.Margin = New Padding(5, 0, 5, 0)
        Label21.Name = "Label21"
        Label21.Size = New Size(142, 27)
        Label21.TabIndex = 46
        Label21.Text = "Country"
        ' 
        ' txtZip
        ' 
        txtZip.Location = New Point(770, 390)
        txtZip.Margin = New Padding(5, 6, 5, 6)
        txtZip.MaxLength = 25
        txtZip.Name = "txtZip"
        txtZip.Size = New Size(131, 31)
        txtZip.TabIndex = 29
        ' 
        ' Label20
        ' 
        Label20.ForeColor = Color.DarkBlue
        Label20.Location = New Point(770, 358)
        Label20.Margin = New Padding(5, 0, 5, 0)
        Label20.Name = "Label20"
        Label20.Size = New Size(113, 27)
        Label20.TabIndex = 44
        Label20.Text = "Zip Code"
        ' 
        ' txtState
        ' 
        txtState.Location = New Point(603, 390)
        txtState.Margin = New Padding(5, 6, 5, 6)
        txtState.MaxLength = 35
        txtState.Name = "txtState"
        txtState.Size = New Size(139, 31)
        txtState.TabIndex = 28
        ' 
        ' Label19
        ' 
        Label19.ForeColor = Color.DarkBlue
        Label19.Location = New Point(612, 358)
        Label19.Margin = New Padding(5, 0, 5, 0)
        Label19.Name = "Label19"
        Label19.Size = New Size(133, 27)
        Label19.TabIndex = 42
        Label19.Text = "State/Province"
        ' 
        ' txtCity
        ' 
        txtCity.Location = New Point(388, 390)
        txtCity.Margin = New Padding(5, 6, 5, 6)
        txtCity.MaxLength = 35
        txtCity.Name = "txtCity"
        txtCity.Size = New Size(199, 31)
        txtCity.TabIndex = 27
        ' 
        ' Label18
        ' 
        Label18.ForeColor = Color.DarkBlue
        Label18.Location = New Point(400, 358)
        Label18.Margin = New Padding(5, 0, 5, 0)
        Label18.Name = "Label18"
        Label18.Size = New Size(90, 27)
        Label18.TabIndex = 40
        Label18.Text = "City"
        ' 
        ' txtAdd2
        ' 
        txtAdd2.Location = New Point(225, 390)
        txtAdd2.Margin = New Padding(5, 6, 5, 6)
        txtAdd2.MaxLength = 35
        txtAdd2.Name = "txtAdd2"
        txtAdd2.Size = New Size(142, 31)
        txtAdd2.TabIndex = 26
        ' 
        ' Label17
        ' 
        Label17.ForeColor = Color.DarkBlue
        Label17.Location = New Point(225, 358)
        Label17.Margin = New Padding(5, 0, 5, 0)
        Label17.Name = "Label17"
        Label17.Size = New Size(127, 27)
        Label17.TabIndex = 38
        Label17.Text = "Address 2"
        ' 
        ' txtAdd1
        ' 
        txtAdd1.Location = New Point(20, 390)
        txtAdd1.Margin = New Padding(5, 6, 5, 6)
        txtAdd1.MaxLength = 35
        txtAdd1.Name = "txtAdd1"
        txtAdd1.Size = New Size(192, 31)
        txtAdd1.TabIndex = 24
        ' 
        ' Label16
        ' 
        Label16.ForeColor = Color.DarkBlue
        Label16.Location = New Point(33, 358)
        Label16.Margin = New Padding(5, 0, 5, 0)
        Label16.Name = "Label16"
        Label16.Size = New Size(113, 27)
        Label16.TabIndex = 36
        Label16.Text = "Address 1"
        ' 
        ' txtCommDLL
        ' 
        txtCommDLL.Location = New Point(225, 310)
        txtCommDLL.Margin = New Padding(5, 6, 5, 6)
        txtCommDLL.MaxLength = 50
        txtCommDLL.Name = "txtCommDLL"
        txtCommDLL.Size = New Size(179, 31)
        txtCommDLL.TabIndex = 20
        ' 
        ' Label15
        ' 
        Label15.ForeColor = Color.DarkBlue
        Label15.Location = New Point(225, 277)
        Label15.Margin = New Padding(5, 0, 5, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(182, 27)
        Label15.TabIndex = 34
        Label15.Text = "Interface DLL"
        ' 
        ' txtEmail
        ' 
        txtEmail.Location = New Point(20, 310)
        txtEmail.Margin = New Padding(5, 6, 5, 6)
        txtEmail.MaxLength = 50
        txtEmail.Name = "txtEmail"
        txtEmail.Size = New Size(192, 31)
        txtEmail.TabIndex = 19
        ' 
        ' Label14
        ' 
        Label14.ForeColor = Color.DarkBlue
        Label14.Location = New Point(28, 277)
        Label14.Margin = New Padding(5, 0, 5, 0)
        Label14.Name = "Label14"
        Label14.Size = New Size(118, 27)
        Label14.TabIndex = 32
        Label14.Text = "Email"
        ' 
        ' btnProvLook
        ' 
        btnProvLook.Image = CType(resources.GetObject("btnProvLook.Image"), Image)
        btnProvLook.Location = New Point(150, 62)
        btnProvLook.Margin = New Padding(5, 6, 5, 6)
        btnProvLook.Name = "btnProvLook"
        btnProvLook.Size = New Size(43, 48)
        btnProvLook.TabIndex = 2
        btnProvLook.UseVisualStyleBackColor = True
        ' 
        ' txtProviderID
        ' 
        txtProviderID.Location = New Point(20, 67)
        txtProviderID.Margin = New Padding(5, 6, 5, 6)
        txtProviderID.MaxLength = 12
        txtProviderID.Name = "txtProviderID"
        txtProviderID.Size = New Size(124, 31)
        txtProviderID.TabIndex = 1
        txtProviderID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label13
        ' 
        Label13.ForeColor = Color.Red
        Label13.Location = New Point(28, 35)
        Label13.Margin = New Padding(5, 0, 5, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(118, 27)
        Label13.TabIndex = 29
        Label13.Text = "Provider ID"
        ' 
        ' Label12
        ' 
        Label12.ForeColor = Color.DarkBlue
        Label12.Location = New Point(933, 200)
        Label12.Margin = New Padding(5, 0, 5, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(118, 27)
        Label12.TabIndex = 27
        Label12.Text = "Cell"
        ' 
        ' Label11
        ' 
        Label11.ForeColor = Color.DarkBlue
        Label11.Location = New Point(702, 200)
        Label11.Margin = New Padding(5, 0, 5, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(113, 27)
        Label11.TabIndex = 25
        Label11.Text = "Fax"
        ' 
        ' Label10
        ' 
        Label10.ForeColor = Color.DarkBlue
        Label10.Location = New Point(513, 200)
        Label10.Margin = New Padding(5, 0, 5, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(113, 27)
        Label10.TabIndex = 23
        Label10.Text = "Phone"
        ' 
        ' txtBCBS
        ' 
        txtBCBS.Location = New Point(302, 233)
        txtBCBS.Margin = New Padding(5, 6, 5, 6)
        txtBCBS.MaxLength = 25
        txtBCBS.Name = "txtBCBS"
        txtBCBS.Size = New Size(166, 31)
        txtBCBS.TabIndex = 15
        ' 
        ' Label9
        ' 
        Label9.ForeColor = Color.DarkBlue
        Label9.Location = New Point(307, 200)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(118, 27)
        Label9.TabIndex = 21
        Label9.Text = "BCBS"
        ' 
        ' txtMCD
        ' 
        txtMCD.Location = New Point(158, 233)
        txtMCD.Margin = New Padding(5, 6, 5, 6)
        txtMCD.MaxLength = 25
        txtMCD.Name = "txtMCD"
        txtMCD.Size = New Size(131, 31)
        txtMCD.TabIndex = 14
        ' 
        ' Label8
        ' 
        Label8.ForeColor = Color.DarkBlue
        Label8.Location = New Point(162, 200)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(118, 27)
        Label8.TabIndex = 19
        Label8.Text = "Medicaid"
        ' 
        ' txtMCR
        ' 
        txtMCR.Location = New Point(20, 233)
        txtMCR.Margin = New Padding(5, 6, 5, 6)
        txtMCR.MaxLength = 25
        txtMCR.Name = "txtMCR"
        txtMCR.Size = New Size(126, 31)
        txtMCR.TabIndex = 13
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.DarkBlue
        Label7.Location = New Point(28, 200)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(118, 27)
        Label7.TabIndex = 17
        Label7.Text = "Medicare"
        ' 
        ' txtCLIA
        ' 
        txtCLIA.Location = New Point(838, 150)
        txtCLIA.Margin = New Padding(5, 6, 5, 6)
        txtCLIA.MaxLength = 25
        txtCLIA.Name = "txtCLIA"
        txtCLIA.Size = New Size(176, 31)
        txtCLIA.TabIndex = 11
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.DarkBlue
        Label6.Location = New Point(853, 117)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(113, 27)
        Label6.TabIndex = 15
        Label6.Text = "CLIA"
        ' 
        ' txtUPIN
        ' 
        txtUPIN.Location = New Point(657, 150)
        txtUPIN.Margin = New Padding(5, 6, 5, 6)
        txtUPIN.MaxLength = 25
        txtUPIN.Name = "txtUPIN"
        txtUPIN.Size = New Size(169, 31)
        txtUPIN.TabIndex = 10
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.Fuchsia
        Label5.Location = New Point(672, 117)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(118, 27)
        Label5.TabIndex = 13
        Label5.Text = "UPIN"
        ' 
        ' lblSSN
        ' 
        lblSSN.ForeColor = Color.DarkBlue
        lblSSN.Location = New Point(162, 117)
        lblSSN.Margin = New Padding(5, 0, 5, 0)
        lblSSN.Name = "lblSSN"
        lblSSN.Size = New Size(118, 27)
        lblSSN.TabIndex = 12
        lblSSN.Text = "SSN"
        ' 
        ' txtSSN
        ' 
        txtSSN.Location = New Point(167, 150)
        txtSSN.Margin = New Padding(5, 6, 5, 6)
        txtSSN.Mask = "000-00-0000"
        txtSSN.Name = "txtSSN"
        txtSSN.Size = New Size(136, 31)
        txtSSN.TabIndex = 7
        ' 
        ' txtNPI
        ' 
        txtNPI.Location = New Point(463, 150)
        txtNPI.Margin = New Padding(5, 6, 5, 6)
        txtNPI.MaxLength = 10
        txtNPI.Name = "txtNPI"
        txtNPI.Size = New Size(181, 31)
        txtNPI.TabIndex = 9
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.Fuchsia
        Label4.Location = New Point(488, 117)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(98, 27)
        Label4.TabIndex = 9
        Label4.Text = "NPI"
        ' 
        ' txtDegree
        ' 
        txtDegree.Location = New Point(20, 150)
        txtDegree.Margin = New Padding(5, 6, 5, 6)
        txtDegree.MaxLength = 25
        txtDegree.Name = "txtDegree"
        txtDegree.Size = New Size(126, 31)
        txtDegree.TabIndex = 6
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(28, 117)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(118, 27)
        Label3.TabIndex = 7
        Label3.Text = "Degree"
        ' 
        ' txtMName
        ' 
        txtMName.Location = New Point(933, 67)
        txtMName.Margin = New Padding(5, 6, 5, 6)
        txtMName.MaxLength = 35
        txtMName.Name = "txtMName"
        txtMName.Size = New Size(267, 31)
        txtMName.TabIndex = 5
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(928, 35)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(118, 27)
        Label2.TabIndex = 5
        Label2.Text = "Middle Name"
        ' 
        ' txtFName
        ' 
        txtFName.Location = New Point(673, 67)
        txtFName.Margin = New Padding(5, 6, 5, 6)
        txtFName.MaxLength = 35
        txtFName.Name = "txtFName"
        txtFName.Size = New Size(227, 31)
        txtFName.TabIndex = 4
        ' 
        ' lblFName
        ' 
        lblFName.ForeColor = Color.Red
        lblFName.Location = New Point(685, 35)
        lblFName.Margin = New Padding(5, 0, 5, 0)
        lblFName.Name = "lblFName"
        lblFName.Size = New Size(105, 27)
        lblFName.TabIndex = 3
        lblFName.Text = "First Name"
        ' 
        ' txtLName
        ' 
        txtLName.Location = New Point(203, 67)
        txtLName.Margin = New Padding(5, 6, 5, 6)
        txtLName.MaxLength = 60
        txtLName.Name = "txtLName"
        txtLName.Size = New Size(441, 31)
        txtLName.TabIndex = 3
        ' 
        ' lblLName
        ' 
        lblLName.ForeColor = Color.Red
        lblLName.Location = New Point(203, 35)
        lblLName.Margin = New Padding(5, 0, 5, 0)
        lblLName.Name = "lblLName"
        lblLName.Size = New Size(167, 27)
        lblLName.TabIndex = 1
        lblLName.Text = "Last Name"
        ' 
        ' chkIndividual
        ' 
        chkIndividual.Appearance = Appearance.Button
        chkIndividual.Checked = True
        chkIndividual.CheckState = CheckState.Checked
        chkIndividual.ForeColor = Color.DarkBlue
        chkIndividual.Location = New Point(388, 6)
        chkIndividual.Margin = New Padding(5, 6, 5, 6)
        chkIndividual.Name = "chkIndividual"
        chkIndividual.Size = New Size(158, 50)
        chkIndividual.TabIndex = 0
        chkIndividual.Text = "Individual"
        chkIndividual.TextAlign = ContentAlignment.MiddleCenter
        chkIndividual.UseVisualStyleBackColor = True
        ' 
        ' tpRDM
        ' 
        tpRDM.Controls.Add(Label37)
        tpRDM.Controls.Add(Label44)
        tpRDM.Controls.Add(cmbPickUp)
        tpRDM.Controls.Add(txtPickupNote)
        tpRDM.Controls.Add(btnRoutes)
        tpRDM.Controls.Add(Label33)
        tpRDM.Controls.Add(GrpHours)
        tpRDM.Controls.Add(cmbRoutes)
        tpRDM.Controls.Add(btnSales)
        tpRDM.Controls.Add(grpRDM)
        tpRDM.Controls.Add(Label32)
        tpRDM.Controls.Add(cmbSales)
        tpRDM.Location = New Point(4, 34)
        tpRDM.Margin = New Padding(5, 6, 5, 6)
        tpRDM.Name = "tpRDM"
        tpRDM.Padding = New Padding(5, 6, 5, 6)
        tpRDM.Size = New Size(1329, 1054)
        tpRDM.TabIndex = 1
        tpRDM.Text = "Configuration"
        tpRDM.UseVisualStyleBackColor = True
        ' 
        ' Label37
        ' 
        Label37.ForeColor = Color.DarkBlue
        Label37.Location = New Point(937, 617)
        Label37.Margin = New Padding(5, 0, 5, 0)
        Label37.Name = "Label37"
        Label37.Size = New Size(172, 25)
        Label37.TabIndex = 7
        Label37.Text = "Pick up Option"
        ' 
        ' Label44
        ' 
        Label44.ForeColor = Color.DarkBlue
        Label44.Location = New Point(13, 925)
        Label44.Margin = New Padding(5, 0, 5, 0)
        Label44.Name = "Label44"
        Label44.Size = New Size(125, 33)
        Label44.TabIndex = 11
        Label44.Text = "Pick Up Note"
        Label44.TextAlign = ContentAlignment.TopRight
        ' 
        ' cmbPickUp
        ' 
        cmbPickUp.DropDownStyle = ComboBoxStyle.DropDownList
        cmbPickUp.FormattingEnabled = True
        cmbPickUp.Items.AddRange(New Object() {"On Call", "Regular"})
        cmbPickUp.Location = New Point(932, 648)
        cmbPickUp.Margin = New Padding(5, 6, 5, 6)
        cmbPickUp.Name = "cmbPickUp"
        cmbPickUp.Size = New Size(186, 33)
        cmbPickUp.TabIndex = 32
        ' 
        ' txtPickupNote
        ' 
        txtPickupNote.Location = New Point(148, 919)
        txtPickupNote.Margin = New Padding(5, 6, 5, 6)
        txtPickupNote.MaxLength = 960
        txtPickupNote.Multiline = True
        txtPickupNote.Name = "txtPickupNote"
        txtPickupNote.ScrollBars = ScrollBars.Vertical
        txtPickupNote.Size = New Size(1059, 108)
        txtPickupNote.TabIndex = 34
        ' 
        ' btnRoutes
        ' 
        btnRoutes.Image = CType(resources.GetObject("btnRoutes.Image"), Image)
        btnRoutes.Location = New Point(860, 640)
        btnRoutes.Margin = New Padding(5, 6, 5, 6)
        btnRoutes.Name = "btnRoutes"
        btnRoutes.Size = New Size(43, 50)
        btnRoutes.TabIndex = 31
        btnRoutes.UseVisualStyleBackColor = True
        ' 
        ' Label33
        ' 
        Label33.ForeColor = Color.DarkBlue
        Label33.Location = New Point(453, 617)
        Label33.Margin = New Padding(5, 0, 5, 0)
        Label33.Name = "Label33"
        Label33.Size = New Size(192, 25)
        Label33.TabIndex = 5
        Label33.Text = "Courier Route"
        ' 
        ' GrpHours
        ' 
        GrpHours.Controls.Add(dgvHours)
        GrpHours.Location = New Point(18, 700)
        GrpHours.Margin = New Padding(5, 6, 5, 6)
        GrpHours.Name = "GrpHours"
        GrpHours.Padding = New Padding(5, 6, 5, 6)
        GrpHours.Size = New Size(1192, 208)
        GrpHours.TabIndex = 4
        GrpHours.TabStop = False
        GrpHours.Text = "Business Hours"
        ' 
        ' dgvHours
        ' 
        dgvHours.AllowUserToAddRows = False
        dgvHours.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.LemonChiffon
        dgvHours.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvHours.ColumnHeadersHeight = 34
        dgvHours.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvHours.Columns.AddRange(New DataGridViewColumn() {WeekDay, DayStart, LunchStart, LunchStop, DayStop})
        dgvHours.Location = New Point(25, 37)
        dgvHours.Margin = New Padding(5, 6, 5, 6)
        dgvHours.Name = "dgvHours"
        dgvHours.RowHeadersVisible = False
        dgvHours.RowHeadersWidth = 62
        dgvHours.Size = New Size(1070, 146)
        dgvHours.TabIndex = 33
        ' 
        ' WeekDay
        ' 
        WeekDay.FillWeight = 99F
        WeekDay.HeaderText = "Week Day"
        WeekDay.MinimumWidth = 8
        WeekDay.Name = "WeekDay"
        WeekDay.ReadOnly = True
        WeekDay.Width = 99
        ' 
        ' DayStart
        ' 
        DayStart.FillWeight = 98F
        DayStart.HeaderText = "Day Start"
        DayStart.Items.AddRange(New Object() {"OFF", "00:00 AM", "01:00 AM", "02:00 AM", "03:00 AM", "04:00 AM", "05:00 AM", "06:00 AM", "07:00 AM", "08:00 AM", "09:00 AM", "10:00 AM", "11:00 AM", "12:00 PM", "01:00 PM", "02:00 PM", "03:00 PM", "04:00 PM", "05:00 PM", "06:00 PM", "07:00 PM", "08:00 PM", "09:00 PM", "10:00 PM", "11:00 PM"})
        DayStart.MinimumWidth = 8
        DayStart.Name = "DayStart"
        DayStart.Width = 98
        ' 
        ' LunchStart
        ' 
        LunchStart.FillWeight = 98F
        LunchStart.HeaderText = "Lunch Start"
        LunchStart.Items.AddRange(New Object() {"OFF", "00:00 AM", "01:00 AM", "02:00 AM", "03:00 AM", "04:00 AM", "05:00 AM", "06:00 AM", "07:00 AM", "08:00 AM", "09:00 AM", "10:00 AM", "11:00 AM", "12:00 PM", "01:00 PM", "02:00 PM", "03:00 PM", "04:00 PM", "05:00 PM", "06:00 PM", "07:00 PM", "08:00 PM", "09:00 PM", "10:00 PM", "11:00 PM"})
        LunchStart.MinimumWidth = 8
        LunchStart.Name = "LunchStart"
        LunchStart.Width = 98
        ' 
        ' LunchStop
        ' 
        LunchStop.FillWeight = 98F
        LunchStop.HeaderText = "Lunch Stop"
        LunchStop.Items.AddRange(New Object() {"OFF", "00:00 AM", "01:00 AM", "02:00 AM", "03:00 AM", "04:00 AM", "05:00 AM", "06:00 AM", "07:00 AM", "08:00 AM", "09:00 AM", "10:00 AM", "11:00 AM", "12:00 PM", "01:00 PM", "02:00 PM", "03:00 PM", "04:00 PM", "05:00 PM", "06:00 PM", "07:00 PM", "08:00 PM", "09:00 PM", "10:00 PM", "11:00 PM"})
        LunchStop.MinimumWidth = 8
        LunchStop.Name = "LunchStop"
        LunchStop.Width = 98
        ' 
        ' DayStop
        ' 
        DayStop.FillWeight = 98F
        DayStop.HeaderText = "Day Stop"
        DayStop.Items.AddRange(New Object() {"OFF", "00:00 AM", "01:00 AM", "02:00 AM", "03:00 AM", "04:00 AM", "05:00 AM", "06:00 AM", "07:00 AM", "08:00 AM", "09:00 AM", "10:00 AM", "11:00 AM", "12:00 PM", "01:00 PM", "02:00 PM", "03:00 PM", "04:00 PM", "05:00 PM", "06:00 PM", "07:00 PM", "08:00 PM", "09:00 PM", "10:00 PM", "11:00 PM"})
        DayStop.MinimumWidth = 8
        DayStop.Name = "DayStop"
        DayStop.Width = 98
        ' 
        ' cmbRoutes
        ' 
        cmbRoutes.DropDownStyle = ComboBoxStyle.DropDownList
        cmbRoutes.FormattingEnabled = True
        cmbRoutes.Location = New Point(440, 648)
        cmbRoutes.Margin = New Padding(5, 6, 5, 6)
        cmbRoutes.Name = "cmbRoutes"
        cmbRoutes.Size = New Size(407, 33)
        cmbRoutes.TabIndex = 30
        ' 
        ' btnSales
        ' 
        btnSales.Image = CType(resources.GetObject("btnSales.Image"), Image)
        btnSales.Location = New Point(357, 640)
        btnSales.Margin = New Padding(5, 6, 5, 6)
        btnSales.Name = "btnSales"
        btnSales.Size = New Size(43, 50)
        btnSales.TabIndex = 29
        btnSales.UseVisualStyleBackColor = True
        ' 
        ' grpRDM
        ' 
        grpRDM.Controls.Add(btnInvFileLook)
        grpRDM.Controls.Add(txtInvoiceRPTFile)
        grpRDM.Controls.Add(Label53)
        grpRDM.Controls.Add(chkORNecessity)
        grpRDM.Controls.Add(chkBlockDemograph)
        grpRDM.Controls.Add(chkUseMyReports)
        grpRDM.Controls.Add(chkDOCRequired)
        grpRDM.Controls.Add(chkPatPhRequired)
        grpRDM.Controls.Add(chkServerPDF)
        grpRDM.Controls.Add(chkAccConsolidate)
        grpRDM.Controls.Add(chkUseESRD)
        grpRDM.Controls.Add(chkChartRequired)
        grpRDM.Controls.Add(chkAuto)
        grpRDM.Controls.Add(chkProlison)
        grpRDM.Controls.Add(chkInterface)
        grpRDM.Controls.Add(chkEmail)
        grpRDM.Controls.Add(chkPrint)
        grpRDM.Controls.Add(chkFax)
        grpRDM.Controls.Add(btnRptLook)
        grpRDM.Controls.Add(txtResRPTFile)
        grpRDM.Controls.Add(Label45)
        grpRDM.Controls.Add(chkSetExt)
        grpRDM.Controls.Add(Label43)
        grpRDM.Controls.Add(Label42)
        grpRDM.Controls.Add(Label41)
        grpRDM.Controls.Add(txtIncSpan)
        grpRDM.Controls.Add(txtCompSpan)
        grpRDM.Controls.Add(Label40)
        grpRDM.Controls.Add(txtAutoTime)
        grpRDM.Controls.Add(Label38)
        grpRDM.Controls.Add(txtExtCmnt)
        grpRDM.Controls.Add(Label36)
        grpRDM.Controls.Add(txtCopies)
        grpRDM.Controls.Add(cmbReport)
        grpRDM.Controls.Add(Label34)
        grpRDM.Controls.Add(cmbRegular)
        grpRDM.Controls.Add(Label24)
        grpRDM.Controls.Add(Label23)
        grpRDM.Controls.Add(chkComplete)
        grpRDM.Controls.Add(cmbPanic)
        grpRDM.Controls.Add(Label22)
        grpRDM.Location = New Point(18, 12)
        grpRDM.Margin = New Padding(5, 6, 5, 6)
        grpRDM.Name = "grpRDM"
        grpRDM.Padding = New Padding(5, 6, 5, 6)
        grpRDM.Size = New Size(1192, 600)
        grpRDM.TabIndex = 0
        grpRDM.TabStop = False
        grpRDM.Text = "Report Delivery Manager"
        ' 
        ' btnInvFileLook
        ' 
        btnInvFileLook.Image = CType(resources.GetObject("btnInvFileLook.Image"), Image)
        btnInvFileLook.Location = New Point(1093, 92)
        btnInvFileLook.Margin = New Padding(5, 6, 5, 6)
        btnInvFileLook.Name = "btnInvFileLook"
        btnInvFileLook.Size = New Size(55, 44)
        btnInvFileLook.TabIndex = 29
        btnInvFileLook.UseVisualStyleBackColor = True
        ' 
        ' txtInvoiceRPTFile
        ' 
        txtInvoiceRPTFile.Location = New Point(163, 96)
        txtInvoiceRPTFile.Margin = New Padding(5, 6, 5, 6)
        txtInvoiceRPTFile.MaxLength = 60
        txtInvoiceRPTFile.Name = "txtInvoiceRPTFile"
        txtInvoiceRPTFile.Size = New Size(902, 31)
        txtInvoiceRPTFile.TabIndex = 28
        ' 
        ' Label53
        ' 
        Label53.ForeColor = Color.DarkBlue
        Label53.Location = New Point(22, 102)
        Label53.Margin = New Padding(5, 0, 5, 0)
        Label53.Name = "Label53"
        Label53.Size = New Size(132, 27)
        Label53.TabIndex = 30
        Label53.Text = "Invoice Report"
        Label53.TextAlign = ContentAlignment.TopRight
        ' 
        ' chkORNecessity
        ' 
        chkORNecessity.CheckAlign = ContentAlignment.MiddleRight
        chkORNecessity.ForeColor = Color.DarkBlue
        chkORNecessity.Location = New Point(10, 408)
        chkORNecessity.Margin = New Padding(5, 6, 5, 6)
        chkORNecessity.Name = "chkORNecessity"
        chkORNecessity.Size = New Size(210, 35)
        chkORNecessity.TabIndex = 26
        chkORNecessity.Text = "Force OR Necessity"
        chkORNecessity.UseVisualStyleBackColor = True
        ' 
        ' chkBlockDemograph
        ' 
        chkBlockDemograph.CheckAlign = ContentAlignment.MiddleRight
        chkBlockDemograph.ForeColor = Color.DarkBlue
        chkBlockDemograph.Location = New Point(827, 362)
        chkBlockDemograph.Margin = New Padding(5, 6, 5, 6)
        chkBlockDemograph.Name = "chkBlockDemograph"
        chkBlockDemograph.Size = New Size(275, 35)
        chkBlockDemograph.TabIndex = 25
        chkBlockDemograph.Text = "Block Patient Demographics ?"
        chkBlockDemograph.UseVisualStyleBackColor = True
        ' 
        ' chkUseMyReports
        ' 
        chkUseMyReports.CheckAlign = ContentAlignment.MiddleRight
        chkUseMyReports.ForeColor = Color.DarkBlue
        chkUseMyReports.Location = New Point(407, 362)
        chkUseMyReports.Margin = New Padding(5, 6, 5, 6)
        chkUseMyReports.Name = "chkUseMyReports"
        chkUseMyReports.Size = New Size(158, 35)
        chkUseMyReports.TabIndex = 23
        chkUseMyReports.Text = "Use My Rpts"
        chkUseMyReports.UseVisualStyleBackColor = True
        ' 
        ' chkDOCRequired
        ' 
        chkDOCRequired.CheckAlign = ContentAlignment.MiddleRight
        chkDOCRequired.ForeColor = Color.DarkBlue
        chkDOCRequired.Location = New Point(230, 362)
        chkDOCRequired.Margin = New Padding(5, 6, 5, 6)
        chkDOCRequired.Name = "chkDOCRequired"
        chkDOCRequired.Size = New Size(158, 35)
        chkDOCRequired.TabIndex = 22
        chkDOCRequired.Text = "DOC Req'd ?"
        chkDOCRequired.UseVisualStyleBackColor = True
        ' 
        ' chkPatPhRequired
        ' 
        chkPatPhRequired.CheckAlign = ContentAlignment.MiddleRight
        chkPatPhRequired.ForeColor = Color.DarkBlue
        chkPatPhRequired.Location = New Point(12, 362)
        chkPatPhRequired.Margin = New Padding(5, 6, 5, 6)
        chkPatPhRequired.Name = "chkPatPhRequired"
        chkPatPhRequired.Size = New Size(208, 35)
        chkPatPhRequired.TabIndex = 21
        chkPatPhRequired.Text = "Patient Phone Req'd ?"
        chkPatPhRequired.UseVisualStyleBackColor = True
        ' 
        ' chkServerPDF
        ' 
        chkServerPDF.CheckAlign = ContentAlignment.MiddleRight
        chkServerPDF.ForeColor = Color.DarkBlue
        chkServerPDF.Location = New Point(627, 362)
        chkServerPDF.Margin = New Padding(5, 6, 5, 6)
        chkServerPDF.Name = "chkServerPDF"
        chkServerPDF.Size = New Size(187, 35)
        chkServerPDF.TabIndex = 24
        chkServerPDF.Text = "PDF To Server ?"
        chkServerPDF.UseVisualStyleBackColor = True
        ' 
        ' chkAccConsolidate
        ' 
        chkAccConsolidate.CheckAlign = ContentAlignment.MiddleRight
        chkAccConsolidate.ForeColor = Color.DarkBlue
        chkAccConsolidate.Location = New Point(25, 312)
        chkAccConsolidate.Margin = New Padding(5, 6, 5, 6)
        chkAccConsolidate.Name = "chkAccConsolidate"
        chkAccConsolidate.Size = New Size(195, 42)
        chkAccConsolidate.TabIndex = 15
        chkAccConsolidate.Text = "Consolidate Acc?"
        chkAccConsolidate.UseVisualStyleBackColor = True
        ' 
        ' chkUseESRD
        ' 
        chkUseESRD.CheckAlign = ContentAlignment.MiddleRight
        chkUseESRD.ForeColor = Color.DarkBlue
        chkUseESRD.Location = New Point(407, 315)
        chkUseESRD.Margin = New Padding(5, 6, 5, 6)
        chkUseESRD.Name = "chkUseESRD"
        chkUseESRD.Size = New Size(158, 35)
        chkUseESRD.TabIndex = 17
        chkUseESRD.Text = "Use ESRD ?"
        chkUseESRD.UseVisualStyleBackColor = True
        ' 
        ' chkChartRequired
        ' 
        chkChartRequired.CheckAlign = ContentAlignment.MiddleRight
        chkChartRequired.ForeColor = Color.DarkBlue
        chkChartRequired.Location = New Point(230, 315)
        chkChartRequired.Margin = New Padding(5, 6, 5, 6)
        chkChartRequired.Name = "chkChartRequired"
        chkChartRequired.Size = New Size(158, 35)
        chkChartRequired.TabIndex = 16
        chkChartRequired.Text = "Chart Req'd ?"
        chkChartRequired.UseVisualStyleBackColor = True
        ' 
        ' chkAuto
        ' 
        chkAuto.CheckAlign = ContentAlignment.MiddleRight
        chkAuto.ForeColor = Color.DarkBlue
        chkAuto.Location = New Point(627, 315)
        chkAuto.Margin = New Padding(5, 6, 5, 6)
        chkAuto.Name = "chkAuto"
        chkAuto.Size = New Size(187, 35)
        chkAuto.TabIndex = 18
        chkAuto.Text = "Auto Delivery ?"
        chkAuto.UseVisualStyleBackColor = True
        ' 
        ' chkProlison
        ' 
        chkProlison.CheckAlign = ContentAlignment.MiddleRight
        chkProlison.ForeColor = Color.DarkBlue
        chkProlison.Location = New Point(970, 315)
        chkProlison.Margin = New Padding(5, 6, 5, 6)
        chkProlison.Name = "chkProlison"
        chkProlison.Size = New Size(128, 35)
        chkProlison.TabIndex = 20
        chkProlison.Text = "Prolison ?"
        chkProlison.UseVisualStyleBackColor = True
        ' 
        ' chkInterface
        ' 
        chkInterface.CheckAlign = ContentAlignment.MiddleRight
        chkInterface.ForeColor = Color.DarkBlue
        chkInterface.Location = New Point(827, 315)
        chkInterface.Margin = New Padding(5, 6, 5, 6)
        chkInterface.Name = "chkInterface"
        chkInterface.Size = New Size(133, 35)
        chkInterface.TabIndex = 19
        chkInterface.Text = "Interface ?"
        chkInterface.UseVisualStyleBackColor = True
        ' 
        ' chkEmail
        ' 
        chkEmail.CheckAlign = ContentAlignment.MiddleRight
        chkEmail.ForeColor = Color.DarkBlue
        chkEmail.Location = New Point(990, 267)
        chkEmail.Margin = New Padding(5, 6, 5, 6)
        chkEmail.Name = "chkEmail"
        chkEmail.Size = New Size(108, 35)
        chkEmail.TabIndex = 14
        chkEmail.Text = "Email ?"
        chkEmail.UseVisualStyleBackColor = True
        ' 
        ' chkPrint
        ' 
        chkPrint.CheckAlign = ContentAlignment.MiddleRight
        chkPrint.ForeColor = Color.DarkBlue
        chkPrint.Location = New Point(862, 267)
        chkPrint.Margin = New Padding(5, 6, 5, 6)
        chkPrint.Name = "chkPrint"
        chkPrint.Size = New Size(98, 35)
        chkPrint.TabIndex = 13
        chkPrint.Text = "Print ?"
        chkPrint.UseVisualStyleBackColor = True
        ' 
        ' chkFax
        ' 
        chkFax.CheckAlign = ContentAlignment.MiddleRight
        chkFax.ForeColor = Color.DarkBlue
        chkFax.Location = New Point(705, 269)
        chkFax.Margin = New Padding(5, 6, 5, 6)
        chkFax.Name = "chkFax"
        chkFax.Size = New Size(108, 35)
        chkFax.TabIndex = 12
        chkFax.Text = "Fax ?"
        chkFax.UseVisualStyleBackColor = True
        ' 
        ' btnRptLook
        ' 
        btnRptLook.Image = CType(resources.GetObject("btnRptLook.Image"), Image)
        btnRptLook.Location = New Point(1093, 38)
        btnRptLook.Margin = New Padding(5, 6, 5, 6)
        btnRptLook.Name = "btnRptLook"
        btnRptLook.Size = New Size(55, 44)
        btnRptLook.TabIndex = 2
        btnRptLook.UseVisualStyleBackColor = True
        ' 
        ' txtResRPTFile
        ' 
        txtResRPTFile.Location = New Point(163, 44)
        txtResRPTFile.Margin = New Padding(5, 6, 5, 6)
        txtResRPTFile.MaxLength = 60
        txtResRPTFile.Name = "txtResRPTFile"
        txtResRPTFile.Size = New Size(902, 31)
        txtResRPTFile.TabIndex = 1
        ' 
        ' Label45
        ' 
        Label45.ForeColor = Color.DarkBlue
        Label45.Location = New Point(22, 50)
        Label45.Margin = New Padding(5, 0, 5, 0)
        Label45.Name = "Label45"
        Label45.Size = New Size(132, 27)
        Label45.TabIndex = 19
        Label45.Text = "Result Report"
        Label45.TextAlign = ContentAlignment.TopRight
        ' 
        ' chkSetExt
        ' 
        chkSetExt.Appearance = Appearance.Button
        chkSetExt.Location = New Point(862, 204)
        chkSetExt.Margin = New Padding(5, 6, 5, 6)
        chkSetExt.Name = "chkSetExt"
        chkSetExt.Size = New Size(105, 48)
        chkSetExt.TabIndex = 8
        chkSetExt.Text = "No"
        chkSetExt.TextAlign = ContentAlignment.MiddleCenter
        chkSetExt.UseVisualStyleBackColor = True
        ' 
        ' Label43
        ' 
        Label43.ForeColor = Color.DarkBlue
        Label43.Location = New Point(862, 177)
        Label43.Margin = New Padding(5, 0, 5, 0)
        Label43.Name = "Label43"
        Label43.Size = New Size(105, 27)
        Label43.TabIndex = 17
        Label43.Text = "Set Ext"
        Label43.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label42
        ' 
        Label42.ForeColor = Color.DarkBlue
        Label42.Location = New Point(23, 271)
        Label42.Margin = New Padding(5, 0, 5, 0)
        Label42.Name = "Label42"
        Label42.Size = New Size(210, 27)
        Label42.TabIndex = 10
        Label42.Text = "Incomplete Report Span"
        Label42.TextAlign = ContentAlignment.TopRight
        ' 
        ' Label41
        ' 
        Label41.ForeColor = Color.DarkBlue
        Label41.Location = New Point(368, 273)
        Label41.Margin = New Padding(5, 0, 5, 0)
        Label41.Name = "Label41"
        Label41.Size = New Size(197, 27)
        Label41.TabIndex = 15
        Label41.Text = "Complete Report Span"
        Label41.TextAlign = ContentAlignment.TopRight
        ' 
        ' txtIncSpan
        ' 
        txtIncSpan.Location = New Point(262, 265)
        txtIncSpan.Margin = New Padding(5, 6, 5, 6)
        txtIncSpan.MaxLength = 2
        txtIncSpan.Name = "txtIncSpan"
        txtIncSpan.Size = New Size(77, 31)
        txtIncSpan.TabIndex = 10
        txtIncSpan.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtCompSpan
        ' 
        txtCompSpan.Location = New Point(593, 267)
        txtCompSpan.Margin = New Padding(5, 6, 5, 6)
        txtCompSpan.MaxLength = 2
        txtCompSpan.Name = "txtCompSpan"
        txtCompSpan.Size = New Size(79, 31)
        txtCompSpan.TabIndex = 11
        txtCompSpan.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label40
        ' 
        Label40.ForeColor = Color.DarkBlue
        Label40.Location = New Point(992, 177)
        Label40.Margin = New Padding(5, 0, 5, 0)
        Label40.Name = "Label40"
        Label40.Size = New Size(107, 27)
        Label40.TabIndex = 12
        Label40.Text = "Print Time"
        ' 
        ' txtAutoTime
        ' 
        txtAutoTime.Location = New Point(992, 210)
        txtAutoTime.Margin = New Padding(5, 6, 5, 6)
        txtAutoTime.Mask = "90:00 ??"
        txtAutoTime.Name = "txtAutoTime"
        txtAutoTime.Size = New Size(104, 31)
        txtAutoTime.TabIndex = 9
        txtAutoTime.ValidatingType = GetType(Date)
        ' 
        ' Label38
        ' 
        Label38.ForeColor = Color.DarkBlue
        Label38.Location = New Point(42, 450)
        Label38.Margin = New Padding(5, 0, 5, 0)
        Label38.Name = "Label38"
        Label38.Size = New Size(163, 27)
        Label38.TabIndex = 10
        Label38.Text = "External Comment"
        ' 
        ' txtExtCmnt
        ' 
        txtExtCmnt.Location = New Point(27, 483)
        txtExtCmnt.Margin = New Padding(5, 6, 5, 6)
        txtExtCmnt.MaxLength = 4000
        txtExtCmnt.Multiline = True
        txtExtCmnt.Name = "txtExtCmnt"
        txtExtCmnt.ScrollBars = ScrollBars.Vertical
        txtExtCmnt.Size = New Size(1134, 102)
        txtExtCmnt.TabIndex = 27
        ' 
        ' Label36
        ' 
        Label36.ForeColor = Color.DarkBlue
        Label36.Location = New Point(593, 179)
        Label36.Margin = New Padding(5, 0, 5, 0)
        Label36.Name = "Label36"
        Label36.Size = New Size(80, 27)
        Label36.TabIndex = 8
        Label36.Text = "Copies"
        ' 
        ' txtCopies
        ' 
        txtCopies.Location = New Point(593, 213)
        txtCopies.Margin = New Padding(5, 6, 5, 6)
        txtCopies.MaxLength = 2
        txtCopies.Name = "txtCopies"
        txtCopies.Size = New Size(77, 31)
        txtCopies.TabIndex = 6
        txtCopies.TextAlign = HorizontalAlignment.Center
        ' 
        ' cmbReport
        ' 
        cmbReport.DropDownStyle = ComboBoxStyle.DropDownList
        cmbReport.FormattingEnabled = True
        cmbReport.Items.AddRange(New Object() {"History", "Accession"})
        cmbReport.Location = New Point(28, 213)
        cmbReport.Margin = New Padding(5, 6, 5, 6)
        cmbReport.Name = "cmbReport"
        cmbReport.Size = New Size(199, 33)
        cmbReport.TabIndex = 3
        ' 
        ' Label34
        ' 
        Label34.ForeColor = Color.DarkBlue
        Label34.Location = New Point(23, 177)
        Label34.Margin = New Padding(5, 0, 5, 0)
        Label34.Name = "Label34"
        Label34.Size = New Size(187, 27)
        Label34.TabIndex = 5
        Label34.Text = "Default Result Report"
        ' 
        ' cmbRegular
        ' 
        cmbRegular.DropDownStyle = ComboBoxStyle.DropDownList
        cmbRegular.FormattingEnabled = True
        cmbRegular.Items.AddRange(New Object() {"Email", "Fax", "Interface", "Print", "ProlisOn", "Phone"})
        cmbRegular.Location = New Point(423, 213)
        cmbRegular.Margin = New Padding(5, 6, 5, 6)
        cmbRegular.Name = "cmbRegular"
        cmbRegular.Size = New Size(139, 33)
        cmbRegular.TabIndex = 5
        ' 
        ' Label24
        ' 
        Label24.ForeColor = Color.DarkBlue
        Label24.Location = New Point(418, 177)
        Label24.Margin = New Padding(5, 0, 5, 0)
        Label24.Name = "Label24"
        Label24.Size = New Size(147, 27)
        Label24.TabIndex = 4
        Label24.Text = "Regular Delivery"
        ' 
        ' Label23
        ' 
        Label23.ForeColor = Color.DarkBlue
        Label23.Location = New Point(700, 177)
        Label23.Margin = New Padding(5, 0, 5, 0)
        Label23.Name = "Label23"
        Label23.Size = New Size(133, 27)
        Label23.TabIndex = 3
        Label23.Text = "Report Status"
        Label23.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' chkComplete
        ' 
        chkComplete.Appearance = Appearance.Button
        chkComplete.Checked = True
        chkComplete.CheckState = CheckState.Checked
        chkComplete.Location = New Point(700, 210)
        chkComplete.Margin = New Padding(5, 6, 5, 6)
        chkComplete.Name = "chkComplete"
        chkComplete.Size = New Size(133, 42)
        chkComplete.TabIndex = 7
        chkComplete.Text = "Complete"
        chkComplete.TextAlign = ContentAlignment.MiddleCenter
        chkComplete.UseVisualStyleBackColor = True
        ' 
        ' cmbPanic
        ' 
        cmbPanic.DropDownStyle = ComboBoxStyle.DropDownList
        cmbPanic.FormattingEnabled = True
        cmbPanic.Items.AddRange(New Object() {"Email", "Fax", "Interface", "Print", "ProlisOn", "Phone"})
        cmbPanic.Location = New Point(262, 213)
        cmbPanic.Margin = New Padding(5, 6, 5, 6)
        cmbPanic.Name = "cmbPanic"
        cmbPanic.Size = New Size(134, 33)
        cmbPanic.TabIndex = 4
        ' 
        ' Label22
        ' 
        Label22.ForeColor = Color.DarkBlue
        Label22.Location = New Point(270, 177)
        Label22.Margin = New Padding(5, 0, 5, 0)
        Label22.Name = "Label22"
        Label22.Size = New Size(128, 27)
        Label22.TabIndex = 0
        Label22.Text = "Panic Delivery"
        Label22.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label32
        ' 
        Label32.ForeColor = Color.DarkBlue
        Label32.Location = New Point(45, 617)
        Label32.Margin = New Padding(5, 0, 5, 0)
        Label32.Name = "Label32"
        Label32.Size = New Size(177, 25)
        Label32.TabIndex = 2
        Label32.Text = "Sales Person"
        ' 
        ' cmbSales
        ' 
        cmbSales.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSales.FormattingEnabled = True
        cmbSales.Location = New Point(38, 648)
        cmbSales.Margin = New Padding(5, 6, 5, 6)
        cmbSales.Name = "cmbSales"
        cmbSales.Size = New Size(306, 33)
        cmbSales.TabIndex = 28
        ' 
        ' tpContract
        ' 
        tpContract.Controls.Add(cmbDxSearch)
        tpContract.Controls.Add(Label52)
        tpContract.Controls.Add(Label51)
        tpContract.Controls.Add(cmbDefaultBilling)
        tpContract.Controls.Add(btnCopyContract)
        tpContract.Controls.Add(Label31)
        tpContract.Controls.Add(Label30)
        tpContract.Controls.Add(dtpTo)
        tpContract.Controls.Add(dtpFrom)
        tpContract.Controls.Add(Label1)
        tpContract.Controls.Add(cmbPriceLevel)
        tpContract.Controls.Add(dgvContract)
        tpContract.Location = New Point(4, 34)
        tpContract.Margin = New Padding(5, 6, 5, 6)
        tpContract.Name = "tpContract"
        tpContract.Padding = New Padding(5, 6, 5, 6)
        tpContract.Size = New Size(1329, 1054)
        tpContract.TabIndex = 2
        tpContract.Text = "Contract Management"
        tpContract.UseVisualStyleBackColor = True
        ' 
        ' cmbDxSearch
        ' 
        cmbDxSearch.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDxSearch.FormattingEnabled = True
        cmbDxSearch.Items.AddRange(New Object() {"Component ID", "Component Name", "Disease-Condition"})
        cmbDxSearch.Location = New Point(208, 950)
        cmbDxSearch.Margin = New Padding(5, 6, 5, 6)
        cmbDxSearch.Name = "cmbDxSearch"
        cmbDxSearch.Size = New Size(236, 33)
        cmbDxSearch.TabIndex = 13
        ' 
        ' Label52
        ' 
        Label52.ForeColor = Color.DarkBlue
        Label52.Location = New Point(28, 948)
        Label52.Margin = New Padding(5, 0, 5, 0)
        Label52.Name = "Label52"
        Label52.Size = New Size(165, 38)
        Label52.TabIndex = 12
        Label52.Text = "Dx Search Default"
        Label52.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label51
        ' 
        Label51.ForeColor = Color.DarkBlue
        Label51.Location = New Point(47, 883)
        Label51.Margin = New Padding(5, 0, 5, 0)
        Label51.Name = "Label51"
        Label51.Size = New Size(128, 38)
        Label51.TabIndex = 11
        Label51.Text = "Default Billing"
        Label51.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' cmbDefaultBilling
        ' 
        cmbDefaultBilling.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDefaultBilling.FormattingEnabled = True
        cmbDefaultBilling.Items.AddRange(New Object() {"Client", "Insurance", "Patient"})
        cmbDefaultBilling.Location = New Point(208, 885)
        cmbDefaultBilling.Margin = New Padding(5, 6, 5, 6)
        cmbDefaultBilling.Name = "cmbDefaultBilling"
        cmbDefaultBilling.Size = New Size(172, 33)
        cmbDefaultBilling.TabIndex = 10
        ' 
        ' btnCopyContract
        ' 
        btnCopyContract.Location = New Point(1107, 52)
        btnCopyContract.Margin = New Padding(5, 6, 5, 6)
        btnCopyContract.Name = "btnCopyContract"
        btnCopyContract.Size = New Size(183, 48)
        btnCopyContract.TabIndex = 9
        btnCopyContract.Text = "Copy Contract"
        btnCopyContract.UseVisualStyleBackColor = True
        ' 
        ' Label31
        ' 
        Label31.ForeColor = Color.DarkBlue
        Label31.Location = New Point(235, 6)
        Label31.Margin = New Padding(5, 0, 5, 0)
        Label31.Name = "Label31"
        Label31.Size = New Size(150, 40)
        Label31.TabIndex = 8
        Label31.Text = "Expires"
        Label31.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label30
        ' 
        Label30.ForeColor = Color.DarkBlue
        Label30.Location = New Point(25, 6)
        Label30.Margin = New Padding(5, 0, 5, 0)
        Label30.Name = "Label30"
        Label30.Size = New Size(200, 40)
        Label30.TabIndex = 7
        Label30.Text = "Effective From"
        Label30.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' dtpTo
        ' 
        dtpTo.Format = DateTimePickerFormat.Short
        dtpTo.Location = New Point(235, 52)
        dtpTo.Margin = New Padding(5, 6, 5, 6)
        dtpTo.Name = "dtpTo"
        dtpTo.Size = New Size(192, 31)
        dtpTo.TabIndex = 6
        ' 
        ' dtpFrom
        ' 
        dtpFrom.Format = DateTimePickerFormat.Short
        dtpFrom.Location = New Point(30, 52)
        dtpFrom.Margin = New Padding(5, 6, 5, 6)
        dtpFrom.Name = "dtpFrom"
        dtpFrom.Size = New Size(192, 31)
        dtpFrom.TabIndex = 5
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(605, 887)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(398, 38)
        Label1.TabIndex = 2
        Label1.Text = "Billing Price Level for Non-Contract components"
        Label1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' cmbPriceLevel
        ' 
        cmbPriceLevel.DropDownStyle = ComboBoxStyle.DropDownList
        cmbPriceLevel.FormattingEnabled = True
        cmbPriceLevel.Items.AddRange(New Object() {"List Price", "Level 1", "Level 2", "Level 3", "Level 4", "Level 5", "Level 6", "Level 7", "Level 8", "Level 9"})
        cmbPriceLevel.Location = New Point(1013, 888)
        cmbPriceLevel.Margin = New Padding(5, 6, 5, 6)
        cmbPriceLevel.Name = "cmbPriceLevel"
        cmbPriceLevel.Size = New Size(274, 33)
        cmbPriceLevel.TabIndex = 1
        ' 
        ' dgvContract
        ' 
        dgvContract.AllowUserToAddRows = False
        dgvContract.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = Color.AntiqueWhite
        dgvContract.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        dgvContract.ColumnHeadersHeight = 34
        dgvContract.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvContract.Columns.AddRange(New DataGridViewColumn() {DEL, ID, Look, Component, Logo, CPT, DPA, Price})
        dgvContract.Location = New Point(30, 125)
        dgvContract.Margin = New Padding(5, 6, 5, 6)
        dgvContract.Name = "dgvContract"
        dgvContract.RowHeadersVisible = False
        dgvContract.RowHeadersWidth = 62
        dgvContract.Size = New Size(1260, 712)
        dgvContract.TabIndex = 0
        ' 
        ' DEL
        ' 
        DEL.FillWeight = 30F
        DEL.HeaderText = ""
        DEL.Image = CType(resources.GetObject("DEL.Image"), Image)
        DEL.MinimumWidth = 8
        DEL.Name = "DEL"
        DEL.Resizable = DataGridViewTriState.True
        DEL.Width = 30
        ' 
        ' ID
        ' 
        ID.HeaderText = "ID"
        ID.MinimumWidth = 8
        ID.Name = "ID"
        ID.Width = 150
        ' 
        ' Look
        ' 
        Look.FillWeight = 30F
        Look.HeaderText = ""
        Look.Image = CType(resources.GetObject("Look.Image"), Image)
        Look.MinimumWidth = 8
        Look.Name = "Look"
        Look.ReadOnly = True
        Look.Width = 30
        ' 
        ' Component
        ' 
        Component.FillWeight = 300F
        Component.HeaderText = "Component Name"
        Component.MinimumWidth = 8
        Component.Name = "Component"
        Component.ReadOnly = True
        Component.Width = 300
        ' 
        ' Logo
        ' 
        Logo.FillWeight = 30F
        Logo.HeaderText = ""
        Logo.Image = CType(resources.GetObject("Logo.Image"), Image)
        Logo.MinimumWidth = 8
        Logo.Name = "Logo"
        Logo.ReadOnly = True
        Logo.Width = 30
        ' 
        ' CPT
        ' 
        CPT.FillWeight = 90F
        CPT.HeaderText = "CPT"
        CPT.MinimumWidth = 8
        CPT.Name = "CPT"
        CPT.ReadOnly = True
        CPT.Width = 90
        ' 
        ' DPA
        ' 
        DPA.FillWeight = 60F
        DPA.HeaderText = "Distinct?"
        DPA.MinimumWidth = 8
        DPA.Name = "DPA"
        DPA.Width = 60
        ' 
        ' Price
        ' 
        Price.FillWeight = 90F
        Price.HeaderText = "Price"
        Price.MinimumWidth = 8
        Price.Name = "Price"
        Price.Width = 90
        ' 
        ' tpAlert
        ' 
        tpAlert.Controls.Add(btnDefault)
        tpAlert.Controls.Add(btnBG)
        tpAlert.Controls.Add(btnColor)
        tpAlert.Controls.Add(Button1)
        tpAlert.Controls.Add(txtAlert)
        tpAlert.Controls.Add(chkAcc)
        tpAlert.Controls.Add(chkCS)
        tpAlert.Controls.Add(Label39)
        tpAlert.Location = New Point(4, 34)
        tpAlert.Margin = New Padding(5, 6, 5, 6)
        tpAlert.Name = "tpAlert"
        tpAlert.Padding = New Padding(5, 6, 5, 6)
        tpAlert.Size = New Size(1329, 1054)
        tpAlert.TabIndex = 3
        tpAlert.Text = "Alert Set Up"
        tpAlert.UseVisualStyleBackColor = True
        ' 
        ' btnDefault
        ' 
        btnDefault.Location = New Point(962, 54)
        btnDefault.Margin = New Padding(5, 6, 5, 6)
        btnDefault.Name = "btnDefault"
        btnDefault.Size = New Size(183, 44)
        btnDefault.TabIndex = 8
        btnDefault.Text = "Default Setting"
        btnDefault.UseVisualStyleBackColor = True
        ' 
        ' btnBG
        ' 
        btnBG.Location = New Point(722, 781)
        btnBG.Margin = New Padding(5, 6, 5, 6)
        btnBG.Name = "btnBG"
        btnBG.Size = New Size(122, 44)
        btnBG.TabIndex = 7
        btnBG.Text = "Background"
        btnBG.UseVisualStyleBackColor = True
        ' 
        ' btnColor
        ' 
        btnColor.Location = New Point(873, 781)
        btnColor.Margin = New Padding(5, 6, 5, 6)
        btnColor.Name = "btnColor"
        btnColor.Size = New Size(122, 44)
        btnColor.TabIndex = 6
        btnColor.Text = "Color"
        btnColor.UseVisualStyleBackColor = True
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(1023, 781)
        Button1.Margin = New Padding(5, 6, 5, 6)
        Button1.Name = "Button1"
        Button1.Size = New Size(122, 44)
        Button1.TabIndex = 5
        Button1.Text = "Font"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' txtAlert
        ' 
        txtAlert.Location = New Point(38, 104)
        txtAlert.Margin = New Padding(5, 6, 5, 6)
        txtAlert.MaxLength = 4000
        txtAlert.Name = "txtAlert"
        txtAlert.Size = New Size(1104, 633)
        txtAlert.TabIndex = 4
        txtAlert.Text = ""
        ' 
        ' chkAcc
        ' 
        chkAcc.AutoSize = True
        chkAcc.Location = New Point(307, 792)
        chkAcc.Margin = New Padding(5, 6, 5, 6)
        chkAcc.Name = "chkAcc"
        chkAcc.Size = New Size(198, 29)
        chkAcc.TabIndex = 3
        chkAcc.Text = "Appear in Accession"
        chkAcc.UseVisualStyleBackColor = True
        ' 
        ' chkCS
        ' 
        chkCS.AutoSize = True
        chkCS.Location = New Point(38, 792)
        chkCS.Margin = New Padding(5, 6, 5, 6)
        chkCS.Name = "chkCS"
        chkCS.Size = New Size(224, 29)
        chkCS.TabIndex = 2
        chkCS.Text = "Appear in Client Service"
        chkCS.UseVisualStyleBackColor = True
        ' 
        ' Label39
        ' 
        Label39.Location = New Point(33, 71)
        Label39.Margin = New Padding(5, 0, 5, 0)
        Label39.Name = "Label39"
        Label39.Size = New Size(162, 27)
        Label39.TabIndex = 0
        Label39.Text = "Alert Content"
        ' 
        ' tpRanges
        ' 
        tpRanges.Controls.Add(dgvAGRanges)
        tpRanges.Location = New Point(4, 34)
        tpRanges.Margin = New Padding(5, 6, 5, 6)
        tpRanges.Name = "tpRanges"
        tpRanges.Padding = New Padding(5, 6, 5, 6)
        tpRanges.Size = New Size(1329, 1054)
        tpRanges.TabIndex = 4
        tpRanges.Text = "Custom Ranges"
        tpRanges.UseVisualStyleBackColor = True
        ' 
        ' dgvAGRanges
        ' 
        dgvAGRanges.AllowUserToAddRows = False
        dgvAGRanges.AllowUserToDeleteRows = False
        dgvAGRanges.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvAGRanges.Columns.AddRange(New DataGridViewColumn() {btnDel, TestID, btnLookUp, AGAgFrom, AGAgeTo, Sex, AGValueGrom, AGValueTo, AGFlag, Behavior})
        dgvAGRanges.Location = New Point(28, 38)
        dgvAGRanges.Margin = New Padding(5, 6, 5, 6)
        dgvAGRanges.Name = "dgvAGRanges"
        dgvAGRanges.RowHeadersVisible = False
        dgvAGRanges.RowHeadersWidth = 62
        dgvAGRanges.SelectionMode = DataGridViewSelectionMode.CellSelect
        dgvAGRanges.Size = New Size(1222, 967)
        dgvAGRanges.TabIndex = 1
        ' 
        ' btnDel
        ' 
        btnDel.FillWeight = 40F
        btnDel.HeaderText = ""
        btnDel.Image = CType(resources.GetObject("btnDel.Image"), Image)
        btnDel.MinimumWidth = 8
        btnDel.Name = "btnDel"
        btnDel.Width = 40
        ' 
        ' TestID
        ' 
        TestID.FillWeight = 90F
        TestID.HeaderText = "Test ID"
        TestID.MaxInputLength = 12
        TestID.MinimumWidth = 8
        TestID.Name = "TestID"
        TestID.Width = 90
        ' 
        ' btnLookUp
        ' 
        btnLookUp.FillWeight = 40F
        btnLookUp.HeaderText = ""
        btnLookUp.Image = CType(resources.GetObject("btnLookUp.Image"), Image)
        btnLookUp.MinimumWidth = 8
        btnLookUp.Name = "btnLookUp"
        btnLookUp.ReadOnly = True
        btnLookUp.Width = 40
        ' 
        ' AGAgFrom
        ' 
        AGAgFrom.FillWeight = 60F
        AGAgFrom.HeaderText = "Age From"
        AGAgFrom.MaxInputLength = 3
        AGAgFrom.MinimumWidth = 8
        AGAgFrom.Name = "AGAgFrom"
        AGAgFrom.SortMode = DataGridViewColumnSortMode.NotSortable
        AGAgFrom.Width = 60
        ' 
        ' AGAgeTo
        ' 
        AGAgeTo.FillWeight = 60F
        AGAgeTo.HeaderText = "Age To"
        AGAgeTo.MaxInputLength = 3
        AGAgeTo.MinimumWidth = 8
        AGAgeTo.Name = "AGAgeTo"
        AGAgeTo.SortMode = DataGridViewColumnSortMode.NotSortable
        AGAgeTo.Width = 60
        ' 
        ' Sex
        ' 
        Sex.FillWeight = 50F
        Sex.HeaderText = "Sex"
        Sex.Items.AddRange(New Object() {"F", "M", "U"})
        Sex.MinimumWidth = 8
        Sex.Name = "Sex"
        Sex.Resizable = DataGridViewTriState.True
        Sex.Width = 50
        ' 
        ' AGValueGrom
        ' 
        AGValueGrom.FillWeight = 80F
        AGValueGrom.HeaderText = "Value From"
        AGValueGrom.MaxInputLength = 100
        AGValueGrom.MinimumWidth = 8
        AGValueGrom.Name = "AGValueGrom"
        AGValueGrom.SortMode = DataGridViewColumnSortMode.NotSortable
        AGValueGrom.Width = 80
        ' 
        ' AGValueTo
        ' 
        AGValueTo.FillWeight = 80F
        AGValueTo.HeaderText = "Value To"
        AGValueTo.MaxInputLength = 100
        AGValueTo.MinimumWidth = 8
        AGValueTo.Name = "AGValueTo"
        AGValueTo.SortMode = DataGridViewColumnSortMode.NotSortable
        AGValueTo.Width = 80
        ' 
        ' AGFlag
        ' 
        AGFlag.FillWeight = 130F
        AGFlag.HeaderText = "Flag"
        AGFlag.MaxInputLength = 25
        AGFlag.MinimumWidth = 8
        AGFlag.Name = "AGFlag"
        AGFlag.Resizable = DataGridViewTriState.True
        AGFlag.SortMode = DataGridViewColumnSortMode.NotSortable
        AGFlag.Width = 130
        ' 
        ' Behavior
        ' 
        Behavior.FillWeight = 80F
        Behavior.HeaderText = "Behavior"
        Behavior.Items.AddRange(New Object() {"Ignore", "Caution", "Panic"})
        Behavior.MinimumWidth = 8
        Behavior.Name = "Behavior"
        Behavior.Resizable = DataGridViewTriState.True
        Behavior.Width = 80
        ' 
        ' OpenFileDialog1
        ' 
        OpenFileDialog1.FileName = "OpenFileDialog1"
        ' 
        ' frmProviders
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        AutoSize = True
        ClientSize = New Size(1375, 1183)
        Controls.Add(tcProvider)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(1234, 1019)
        Name = "frmProviders"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Provider Management"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        tcProvider.ResumeLayout(False)
        tpProvider.ResumeLayout(False)
        tpProvider.PerformLayout()
        CType(dgvProviders, ComponentModel.ISupportInitialize).EndInit()
        tpRDM.ResumeLayout(False)
        tpRDM.PerformLayout()
        GrpHours.ResumeLayout(False)
        CType(dgvHours, ComponentModel.ISupportInitialize).EndInit()
        grpRDM.ResumeLayout(False)
        grpRDM.PerformLayout()
        tpContract.ResumeLayout(False)
        CType(dgvContract, ComponentModel.ISupportInitialize).EndInit()
        tpAlert.ResumeLayout(False)
        tpAlert.PerformLayout()
        tpRanges.ResumeLayout(False)
        CType(dgvAGRanges, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents chkEditNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents tcProvider As System.Windows.Forms.TabControl
    Friend WithEvents tpProvider As System.Windows.Forms.TabPage
    Friend WithEvents tpRDM As System.Windows.Forms.TabPage
    Friend WithEvents tpContract As System.Windows.Forms.TabPage
    Friend WithEvents chkIndividual As System.Windows.Forms.CheckBox
    Friend WithEvents txtLName As System.Windows.Forms.TextBox
    Friend WithEvents lblLName As System.Windows.Forms.Label
    Friend WithEvents txtNPI As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDegree As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtMName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtFName As System.Windows.Forms.TextBox
    Friend WithEvents lblFName As System.Windows.Forms.Label
    Friend WithEvents lblSSN As System.Windows.Forms.Label
    Friend WithEvents txtSSN As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtMCD As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtMCR As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCLIA As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtUPIN As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtBCBS As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtProviderID As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnProvLook As System.Windows.Forms.Button
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtCommDLL As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtCountry As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtZip As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtState As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtAdd2 As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtAdd1 As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents GrpHours As System.Windows.Forms.GroupBox
    Friend WithEvents grpRDM As System.Windows.Forms.GroupBox
    Friend WithEvents cmbPanic As System.Windows.Forms.ComboBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents chkComplete As System.Windows.Forms.CheckBox
    Friend WithEvents cmbRegular As System.Windows.Forms.ComboBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents dgvHours As System.Windows.Forms.DataGridView
    Friend WithEvents WeekDay As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DayStart As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents LunchStart As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents LunchStop As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DayStop As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtLicense As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtContact As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtNote As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents cmbPriceLevel As System.Windows.Forms.ComboBox
    Friend WithEvents dgvContract As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents cmbRoutes As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSales As System.Windows.Forms.ComboBox
    Friend WithEvents btnSales As System.Windows.Forms.Button
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents btnRoutes As System.Windows.Forms.Button
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents cmbReport As System.Windows.Forms.ComboBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents txtCopies As System.Windows.Forms.TextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents cmbPickUp As System.Windows.Forms.ComboBox
    Friend WithEvents dgvProviders As System.Windows.Forms.DataGridView
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents txtExtCmnt As System.Windows.Forms.TextBox
    Friend WithEvents tpAlert As System.Windows.Forms.TabPage
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents chkAcc As System.Windows.Forms.CheckBox
    Friend WithEvents chkCS As System.Windows.Forms.CheckBox
    Friend WithEvents txtAlert As System.Windows.Forms.RichTextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnColor As System.Windows.Forms.Button
    Friend WithEvents btnBG As System.Windows.Forms.Button
    Friend WithEvents btnDefault As System.Windows.Forms.Button
    Friend WithEvents txtAutoTime As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents txtIncSpan As System.Windows.Forms.TextBox
    Friend WithEvents txtCompSpan As System.Windows.Forms.TextBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents chkSetExt As System.Windows.Forms.CheckBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents txtPickupNote As System.Windows.Forms.TextBox
    Friend WithEvents txtFax As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtPhone As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnCopyContract As System.Windows.Forms.Button
    Friend WithEvents txtResRPTFile As System.Windows.Forms.TextBox
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents btnRptLook As System.Windows.Forms.Button
    Friend WithEvents chkFax As System.Windows.Forms.CheckBox
    Friend WithEvents chkEmail As System.Windows.Forms.CheckBox
    Friend WithEvents chkPrint As System.Windows.Forms.CheckBox
    Friend WithEvents chkInterface As System.Windows.Forms.CheckBox
    Friend WithEvents chkAuto As System.Windows.Forms.CheckBox
    Friend WithEvents chkProlison As System.Windows.Forms.CheckBox
    Friend WithEvents txtCell As System.Windows.Forms.MaskedTextBox
    Friend WithEvents chkChartRequired As System.Windows.Forms.CheckBox
    Friend WithEvents tpRanges As System.Windows.Forms.TabPage
    Friend WithEvents dgvAGRanges As System.Windows.Forms.DataGridView
    Friend WithEvents chkUseESRD As System.Windows.Forms.CheckBox
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents txtPanicInstructions As System.Windows.Forms.RichTextBox
    Friend WithEvents btnPIColor As System.Windows.Forms.Button
    Friend WithEvents btnPIFont As System.Windows.Forms.Button
    Friend WithEvents chkUserMgmt As System.Windows.Forms.CheckBox
    Friend WithEvents txtAssID As System.Windows.Forms.TextBox
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents btnAssLookUp As System.Windows.Forms.Button
    Friend WithEvents txtAssName As System.Windows.Forms.TextBox
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents chkAssActive As System.Windows.Forms.CheckBox
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents btnAssAdd As System.Windows.Forms.Button
    Friend WithEvents btnAssDel As System.Windows.Forms.Button
    Friend WithEvents txtAssAddress As System.Windows.Forms.TextBox
    Friend WithEvents ProvID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ProvName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Active As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents chkAccConsolidate As System.Windows.Forms.CheckBox
    Friend WithEvents chkServerPDF As System.Windows.Forms.CheckBox
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents cmbDefaultBilling As System.Windows.Forms.ComboBox
    Friend WithEvents chkPatPhRequired As System.Windows.Forms.CheckBox
    Friend WithEvents cmbDxSearch As System.Windows.Forms.ComboBox
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents chkDOCRequired As System.Windows.Forms.CheckBox
    Friend WithEvents chkUseMyReports As System.Windows.Forms.CheckBox
    Friend WithEvents chkBlockDemograph As System.Windows.Forms.CheckBox
    Friend WithEvents chkORNecessity As System.Windows.Forms.CheckBox
    Friend WithEvents btnDel As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents TestID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnLookUp As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents AGAgFrom As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AGAgeTo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Sex As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents AGValueGrom As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AGValueTo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AGFlag As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Behavior As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents btnExternalFeed As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnInvFileLook As System.Windows.Forms.Button
    Friend WithEvents txtInvoiceRPTFile As System.Windows.Forms.TextBox
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents DEL As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Look As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents Component As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Logo As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents CPT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DPA As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Price As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents txtPOS As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

End Class
