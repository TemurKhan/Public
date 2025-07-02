<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRequisitions
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRequisitions))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As DataGridViewCellStyle = New DataGridViewCellStyle()
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
        txtAccID = New TextBox()
        Label1 = New Label()
        Label2 = New Label()
        txtRequisition = New TextBox()
        dtpDate = New DateTimePicker()
        lblAccDate = New Label()
        Label4 = New Label()
        cmbSpecimenType = New ComboBox()
        Label5 = New Label()
        TabControl1 = New TabControl()
        tpSpecimen = New TabPage()
        printLabel = New Button()
        txtRecDate = New MaskedTextBox()
        Label117 = New Label()
        Label108 = New Label()
        txtRecTime = New MaskedTextBox()
        Label3 = New Label()
        chkPostPrePhleb = New CheckBox()
        txtDrawnTime = New MaskedTextBox()
        txtLabels = New TextBox()
        Label37 = New Label()
        Label13 = New Label()
        txtQty = New TextBox()
        btnSources = New Button()
        cmbSource = New ComboBox()
        Label12 = New Label()
        txtSrcComment = New TextBox()
        Label11 = New Label()
        chkIsReady = New CheckBox()
        cmbTemp = New ComboBox()
        btnAddSrc = New Button()
        btnRemAllSrc = New Button()
        Label10 = New Label()
        Label9 = New Label()
        btnRemSrc = New Button()
        dgvSources = New DataGridView()
        SrcID = New DataGridViewTextBoxColumn()
        SrcNo = New DataGridViewTextBoxColumn()
        SrcName = New DataGridViewTextBoxColumn()
        SrcQty = New DataGridViewTextBoxColumn()
        SrcDate = New DataGridViewTextBoxColumn()
        SrcTime = New DataGridViewTextBoxColumn()
        SrcTemp = New DataGridViewTextBoxColumn()
        SrcComment = New DataGridViewTextBoxColumn()
        Label8 = New Label()
        dtpDateDrawn = New DateTimePicker()
        Label7 = New Label()
        Label6 = New Label()
        tpOrderer = New TabPage()
        txtProvEmail = New TextBox()
        grpClient = New GroupBox()
        btn120 = New Button()
        btn122 = New Button()
        lstProviders = New CheckedListBox()
        txtOrdFax = New MaskedTextBox()
        txtOrdPhone = New MaskedTextBox()
        Label96 = New Label()
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
        grpPatient = New GroupBox()
        tcDxMeds = New TabControl()
        tpCodes = New TabPage()
        PanelDX = New Panel()
        LoadDx = New Button()
        btnRemDxAll = New Button()
        dgvDxs = New DataGridView()
        Dx_Code = New DataGridViewTextBoxColumn()
        Lookup = New DataGridViewImageColumn()
        remd = New DataGridViewImageColumn()
        tpMeds = New TabPage()
        PanelMedi = New Panel()
        Button2 = New Button()
        RemMedAll = New Button()
        dgvMeds = New DataGridView()
        MedName = New DataGridViewTextBoxColumn()
        MedLook = New DataGridViewImageColumn()
        remm = New DataGridViewImageColumn()
        cmbRace = New ComboBox()
        Label130 = New Label()
        Label127 = New Label()
        txtTage = New TextBox()
        txtFax = New MaskedTextBox()
        Label126 = New Label()
        txtCell = New MaskedTextBox()
        Label125 = New Label()
        txtWPhone = New MaskedTextBox()
        Label124 = New Label()
        Label119 = New Label()
        cmbEthnicity = New ComboBox()
        Label118 = New Label()
        txtRoom = New TextBox()
        txtPatHPhone = New MaskedTextBox()
        lblChart = New Label()
        txtEMRNo = New TextBox()
        chkFasting = New CheckBox()
        btnPatUpdate = New Button()
        Label100 = New Label()
        Label99 = New Label()
        Label98 = New Label()
        txtPatCountry = New TextBox()
        txtPatZip = New TextBox()
        txtPatState = New TextBox()
        Label97 = New Label()
        txtPatAdr2 = New TextBox()
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
        Label33 = New Label()
        Label29 = New Label()
        txtPatAdr1 = New TextBox()
        Label32 = New Label()
        Label28 = New Label()
        txtSSN = New MaskedTextBox()
        txtPatCity = New TextBox()
        Label31 = New Label()
        Label27 = New Label()
        txtMName = New TextBox()
        txtPatEmail = New TextBox()
        txtFName = New TextBox()
        Label26 = New Label()
        gbVeterinary = New GroupBox()
        Label129 = New Label()
        cmbBreed = New ComboBox()
        lblSpecies = New Label()
        cmbSpecies = New ComboBox()
        tpOrders = New TabPage()
        chkCare = New CheckBox()
        chkHomeBound = New CheckBox()
        chkPhlebotomy = New CheckBox()
        chkVerbal = New CheckBox()
        chkProfile = New CheckBox()
        dgvTGPMarked = New DataGridView()
        CompID = New DataGridViewTextBoxColumn()
        CompLook = New DataGridViewImageColumn()
        TGP = New DataGridViewTextBoxColumn()
        CompType = New DataGridViewImageColumn()
        Stat = New DataGridViewCheckBoxColumn()
        Verbal = New DataGridViewCheckBoxColumn()
        Outsource = New DataGridViewCheckBoxColumn()
        Label38 = New Label()
        tpReports = New TabPage()
        gbReports = New GroupBox()
        txtRPTFax = New MaskedTextBox()
        chkInterface = New CheckBox()
        chkProlison = New CheckBox()
        Label42 = New Label()
        Label41 = New Label()
        chkRDMAuto = New CheckBox()
        Label47 = New Label()
        Label46 = New Label()
        Label45 = New Label()
        btnRptAdd = New Button()
        txtRptEmail = New TextBox()
        Label44 = New Label()
        chkRptFax = New CheckBox()
        Label43 = New Label()
        chkrptEmail = New CheckBox()
        chkPrint = New CheckBox()
        btnRefProf = New Button()
        txtRptRcptName = New TextBox()
        Label40 = New Label()
        btnRPTLookUp = New Button()
        chkRptComplete = New CheckBox()
        txtRptRcptID = New TextBox()
        Label39 = New Label()
        btnRemRpt = New Button()
        btnRemRptAll = New Button()
        dgvRptProviders = New DataGridView()
        Prov_ID = New DataGridViewTextBoxColumn()
        Prov_Name = New DataGridViewTextBoxColumn()
        RDMAuto = New DataGridViewCheckBoxColumn()
        RCO = New DataGridViewCheckBoxColumn()
        Print = New DataGridViewCheckBoxColumn()
        Prolison = New DataGridViewCheckBoxColumn()
        RDMInterface = New DataGridViewCheckBoxColumn()
        chkFax = New DataGridViewCheckBoxColumn()
        Fax = New DataGridViewTextBoxColumn()
        chkEmail = New DataGridViewCheckBoxColumn()
        Email = New DataGridViewTextBoxColumn()
        tpBilling = New TabPage()
        rbP = New RadioButton()
        rbT = New RadioButton()
        rbC = New RadioButton()
        btnSwitchCarriers = New Button()
        Label101 = New Label()
        txtPayment = New TextBox()
        btnCalculate = New Button()
        TabControl2 = New TabControl()
        tpPrimary = New TabPage()
        grpPSubs = New GroupBox()
        txtPsubWPhone = New MaskedTextBox()
        Label120 = New Label()
        txtPsubHPhone = New MaskedTextBox()
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
        Label123 = New Label()
        Label122 = New Label()
        txtCovCmnt = New TextBox()
        txtDOI = New MaskedTextBox()
        chkWorkman = New CheckBox()
        Label105 = New Label()
        txtPInsName = New TextBox()
        txtPInsID = New TextBox()
        txtPCopay = New TextBox()
        Label58 = New Label()
        Label51 = New Label()
        cmbPRelation = New ComboBox()
        Label52 = New Label()
        txtPFrom = New MaskedTextBox()
        btnPIns = New Button()
        Label53 = New Label()
        txtPGroup = New TextBox()
        txtPTo = New MaskedTextBox()
        Label54 = New Label()
        Label55 = New Label()
        txtPPolicy = New TextBox()
        Label56 = New Label()
        tpSecondary = New TabPage()
        grpSSubs = New GroupBox()
        txtSSubPhone = New MaskedTextBox()
        Label80 = New Label()
        txtSSubSSN = New MaskedTextBox()
        Label81 = New Label()
        txtSSubDOB = New MaskedTextBox()
        cmbSSubSex = New ComboBox()
        btnSSubLook = New Button()
        Label82 = New Label()
        txtSSubCountry = New TextBox()
        Label83 = New Label()
        txtSSubEmail = New TextBox()
        Label84 = New Label()
        Label85 = New Label()
        txtSSubZip = New TextBox()
        Label86 = New Label()
        txtSSubState = New TextBox()
        Label87 = New Label()
        txtSSubCity = New TextBox()
        Label88 = New Label()
        txtSSubAdd2 = New TextBox()
        Label89 = New Label()
        txtSSubAdd1 = New TextBox()
        Label90 = New Label()
        Label91 = New Label()
        txtSSubMName = New TextBox()
        Label92 = New Label()
        txtSSubFName = New TextBox()
        Label93 = New Label()
        txtSSubLName = New TextBox()
        Label94 = New Label()
        txtSSubID = New TextBox()
        grpSecondary = New GroupBox()
        Label106 = New Label()
        txtSInsName = New TextBox()
        txtSPolicy = New TextBox()
        txtSCopay = New TextBox()
        Label73 = New Label()
        Label74 = New Label()
        cmbSRelation = New ComboBox()
        Label75 = New Label()
        txtSFrom = New MaskedTextBox()
        btnSIns = New Button()
        Label76 = New Label()
        txtSGroup = New TextBox()
        txtSTo = New MaskedTextBox()
        Label77 = New Label()
        Label78 = New Label()
        txtSInsID = New TextBox()
        Label79 = New Label()
        tpGuarantor = New TabPage()
        Label128 = New Label()
        txtGTage = New TextBox()
        Label107 = New Label()
        cmbGRelation = New ComboBox()
        txtGCountry = New TextBox()
        Label109 = New Label()
        txtGPhone = New MaskedTextBox()
        chkGIsIndividual = New CheckBox()
        txtGZip = New TextBox()
        Label112 = New Label()
        txtGEmail = New TextBox()
        Label110 = New Label()
        txtGState = New TextBox()
        Label113 = New Label()
        txtGSSN = New MaskedTextBox()
        lblGSSN = New Label()
        txtGLName_BSN = New TextBox()
        Label111 = New Label()
        txtGDOB = New MaskedTextBox()
        lblGSex = New Label()
        cmbGSex = New ComboBox()
        Label121 = New Label()
        txtGID = New TextBox()
        txtGCity = New TextBox()
        Label114 = New Label()
        btnGLookUp = New Button()
        lblGLName = New Label()
        txtGAdd2 = New TextBox()
        Label115 = New Label()
        lblGFName = New Label()
        txtGFName = New TextBox()
        txtGAdd1 = New TextBox()
        Label116 = New Label()
        lblGMName = New Label()
        txtGMName = New TextBox()
        lblGDOB = New Label()
        Label50 = New Label()
        Label49 = New Label()
        Label48 = New Label()
        btnPmnt = New Button()
        txtCopay = New TextBox()
        chkSvcGratis = New CheckBox()
        TabPage1 = New TabPage()
        barcode = New TextBox()
        SendTocolorBtn = New Button()
        QrChk = New CheckBox()
        lblSpecimen = New Label()
        lblOrderer = New Label()
        lblPatient = New Label()
        lblBilling = New Label()
        lblOrders = New Label()
        lblReports = New Label()
        lblRequisition = New Label()
        ToolTip1 = New ToolTip(components)
        btnSupportive = New Button()
        chkReject = New CheckBox()
        btnAccLook = New Button()
        txtAnalysisStage = New TextBox()
        txtComment = New TextBox()
        Label21 = New Label()
        txtWorkCmnt = New TextBox()
        Label20 = New Label()
        ProlisHelp = New HelpProvider()
        grpSearch = New GroupBox()
        btnFirst = New Button()
        btnPrevious = New Button()
        btnNext = New Button()
        btnLast = New Button()
        txtNavStatus = New TextBox()
        btnLoad = New Button()
        txtAccTo = New TextBox()
        txtAccFrom = New TextBox()
        Label103 = New Label()
        Label104 = New Label()
        Label102 = New Label()
        txtDateTo = New MaskedTextBox()
        Label23 = New Label()
        Label22 = New Label()
        txtDateFrom = New MaskedTextBox()
        txtTime = New MaskedTextBox()
        chkInHouse = New CheckBox()
        Label24 = New Label()
        txtInEditReason = New TextBox()
        StatusStrip1 = New StatusStrip()
        lblStatus = New ToolStripStatusLabel()
        PB = New ToolStripProgressBar()
        txtRejectReason = New TextBox()
        lblRejectReason = New Label()
        ToolStrip1.SuspendLayout()
        TabControl1.SuspendLayout()
        tpSpecimen.SuspendLayout()
        CType(dgvSources, ComponentModel.ISupportInitialize).BeginInit()
        tpOrderer.SuspendLayout()
        grpClient.SuspendLayout()
        tpPatient.SuspendLayout()
        grpPatient.SuspendLayout()
        tcDxMeds.SuspendLayout()
        tpCodes.SuspendLayout()
        PanelDX.SuspendLayout()
        CType(dgvDxs, ComponentModel.ISupportInitialize).BeginInit()
        tpMeds.SuspendLayout()
        PanelMedi.SuspendLayout()
        CType(dgvMeds, ComponentModel.ISupportInitialize).BeginInit()
        gbVeterinary.SuspendLayout()
        tpOrders.SuspendLayout()
        CType(dgvTGPMarked, ComponentModel.ISupportInitialize).BeginInit()
        tpReports.SuspendLayout()
        gbReports.SuspendLayout()
        CType(dgvRptProviders, ComponentModel.ISupportInitialize).BeginInit()
        tpBilling.SuspendLayout()
        TabControl2.SuspendLayout()
        tpPrimary.SuspendLayout()
        grpPSubs.SuspendLayout()
        grpPrimary.SuspendLayout()
        tpSecondary.SuspendLayout()
        grpSSubs.SuspendLayout()
        grpSecondary.SuspendLayout()
        tpGuarantor.SuspendLayout()
        TabPage1.SuspendLayout()
        grpSearch.SuspendLayout()
        StatusStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.AutoSize = False
        ToolStrip1.Font = New Font("Segoe UI", 7.753846F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        ToolStrip1.ImageScalingSize = New Size(20, 20)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnNew, ToolStripSeparator1, btnSave, ToolStripSeparator2, btnDelete, ToolStripSeparator3, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 4, 0)
        ToolStrip1.Size = New Size(1386, 53)
        ToolStrip1.TabIndex = 0
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnNew
        ' 
        btnNew.AutoSize = False
        btnNew.CheckOnClick = True
        btnNew.Image = CType(resources.GetObject("btnNew.Image"), Image)
        btnNew.ImageTransparentColor = Color.Magenta
        btnNew.Name = "btnNew"
        btnNew.Size = New Size(80, 22)
        btnNew.Text = "New"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 53)
        ' 
        ' btnSave
        ' 
        btnSave.AutoSize = False
        btnSave.Enabled = False
        btnSave.Image = CType(resources.GetObject("btnSave.Image"), Image)
        btnSave.ImageTransparentColor = Color.Magenta
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(80, 22)
        btnSave.Text = "Save"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 53)
        ' 
        ' btnDelete
        ' 
        btnDelete.AutoSize = False
        btnDelete.Enabled = False
        btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), Image)
        btnDelete.ImageTransparentColor = Color.Magenta
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(80, 22)
        btnDelete.Text = "Delete"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(6, 53)
        ' 
        ' btnCancel
        ' 
        btnCancel.AutoSize = False
        btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), Image)
        btnCancel.ImageTransparentColor = Color.Magenta
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(80, 22)
        btnCancel.Text = "Cancel"
        ' 
        ' ToolStripSeparator4
        ' 
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New Size(6, 53)
        ' 
        ' btnHelp
        ' 
        btnHelp.AutoSize = False
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.ImageTransparentColor = Color.Magenta
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(80, 22)
        btnHelp.Text = "Help"
        ' 
        ' txtAccID
        ' 
        txtAccID.Location = New Point(38, 286)
        txtAccID.Margin = New Padding(5, 6, 5, 6)
        txtAccID.MaxLength = 12
        txtAccID.Name = "txtAccID"
        txtAccID.Size = New Size(173, 31)
        txtAccID.TabIndex = 11
        txtAccID.TabStop = False
        txtAccID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(54, 247)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(140, 25)
        Label1.TabIndex = 2
        Label1.Text = "Accession ID"
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(296, 247)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(156, 25)
        Label2.TabIndex = 4
        Label2.Text = "Requisition"
        ' 
        ' txtRequisition
        ' 
        txtRequisition.AcceptsReturn = True
        txtRequisition.Location = New Point(284, 286)
        txtRequisition.Margin = New Padding(5, 6, 5, 6)
        txtRequisition.MaxLength = 25
        txtRequisition.Name = "txtRequisition"
        txtRequisition.Size = New Size(168, 31)
        txtRequisition.TabIndex = 13
        ' 
        ' dtpDate
        ' 
        dtpDate.Format = DateTimePickerFormat.Short
        dtpDate.Location = New Point(464, 286)
        dtpDate.Margin = New Padding(5, 6, 5, 6)
        dtpDate.Name = "dtpDate"
        dtpDate.Size = New Size(169, 31)
        dtpDate.TabIndex = 14
        ' 
        ' lblAccDate
        ' 
        lblAccDate.ForeColor = Color.DarkBlue
        lblAccDate.Location = New Point(464, 244)
        lblAccDate.Margin = New Padding(5, 0, 5, 0)
        lblAccDate.Name = "lblAccDate"
        lblAccDate.Size = New Size(122, 25)
        lblAccDate.TabIndex = 7
        lblAccDate.Text = "Acc Date"
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(635, 247)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(118, 25)
        Label4.TabIndex = 9
        Label4.Text = "Acc. Time"
        Label4.TextAlign = ContentAlignment.TopCenter
        ' 
        ' cmbSpecimenType
        ' 
        cmbSpecimenType.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSpecimenType.FormattingEnabled = True
        cmbSpecimenType.Items.AddRange(New Object() {"Clinical", "Industrial", "Regulatory"})
        cmbSpecimenType.Location = New Point(754, 284)
        cmbSpecimenType.Margin = New Padding(5, 6, 5, 6)
        cmbSpecimenType.Name = "cmbSpecimenType"
        cmbSpecimenType.Size = New Size(218, 33)
        cmbSpecimenType.TabIndex = 16
        cmbSpecimenType.TabStop = False
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.DarkBlue
        Label5.Location = New Point(764, 244)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(174, 28)
        Label5.TabIndex = 12
        Label5.Text = "Specimen Type"
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(tpSpecimen)
        TabControl1.Controls.Add(tpOrderer)
        TabControl1.Controls.Add(tpPatient)
        TabControl1.Controls.Add(tpOrders)
        TabControl1.Controls.Add(tpReports)
        TabControl1.Controls.Add(tpBilling)
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Location = New Point(16, 619)
        TabControl1.Margin = New Padding(5, 6, 5, 6)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(1350, 609)
        TabControl1.SizeMode = TabSizeMode.Fixed
        TabControl1.TabIndex = 19
        ' 
        ' tpSpecimen
        ' 
        tpSpecimen.Controls.Add(printLabel)
        tpSpecimen.Controls.Add(txtRecDate)
        tpSpecimen.Controls.Add(Label117)
        tpSpecimen.Controls.Add(Label108)
        tpSpecimen.Controls.Add(txtRecTime)
        tpSpecimen.Controls.Add(Label3)
        tpSpecimen.Controls.Add(chkPostPrePhleb)
        tpSpecimen.Controls.Add(txtDrawnTime)
        tpSpecimen.Controls.Add(txtLabels)
        tpSpecimen.Controls.Add(Label37)
        tpSpecimen.Controls.Add(Label13)
        tpSpecimen.Controls.Add(txtQty)
        tpSpecimen.Controls.Add(btnSources)
        tpSpecimen.Controls.Add(cmbSource)
        tpSpecimen.Controls.Add(Label12)
        tpSpecimen.Controls.Add(txtSrcComment)
        tpSpecimen.Controls.Add(Label11)
        tpSpecimen.Controls.Add(chkIsReady)
        tpSpecimen.Controls.Add(cmbTemp)
        tpSpecimen.Controls.Add(btnAddSrc)
        tpSpecimen.Controls.Add(btnRemAllSrc)
        tpSpecimen.Controls.Add(Label10)
        tpSpecimen.Controls.Add(Label9)
        tpSpecimen.Controls.Add(btnRemSrc)
        tpSpecimen.Controls.Add(dgvSources)
        tpSpecimen.Controls.Add(Label8)
        tpSpecimen.Controls.Add(dtpDateDrawn)
        tpSpecimen.Controls.Add(Label7)
        tpSpecimen.Controls.Add(Label6)
        tpSpecimen.ForeColor = Color.DarkBlue
        tpSpecimen.Location = New Point(4, 34)
        tpSpecimen.Margin = New Padding(5, 6, 5, 6)
        tpSpecimen.Name = "tpSpecimen"
        tpSpecimen.Padding = New Padding(5, 6, 5, 6)
        tpSpecimen.Size = New Size(1342, 571)
        tpSpecimen.TabIndex = 0
        tpSpecimen.Text = "Specimen"
        tpSpecimen.UseVisualStyleBackColor = True
        ' 
        ' printLabel
        ' 
        printLabel.Image = My.Resources.Resources.icons8_print_30
        printLabel.Location = New Point(1114, 453)
        printLabel.Margin = New Padding(4, 3, 4, 3)
        printLabel.Name = "printLabel"
        printLabel.Size = New Size(69, 72)
        printLabel.TabIndex = 96
        ToolTip1.SetToolTip(printLabel, "Print Labels")
        printLabel.UseVisualStyleBackColor = True
        ' 
        ' txtRecDate
        ' 
        txtRecDate.Location = New Point(1184, 141)
        txtRecDate.Margin = New Padding(5, 6, 5, 6)
        txtRecDate.Mask = "00/00/0000"
        txtRecDate.Name = "txtRecDate"
        txtRecDate.Size = New Size(138, 31)
        txtRecDate.TabIndex = 40
        txtRecDate.ValidatingType = GetType(Date)
        ' 
        ' Label117
        ' 
        Label117.Location = New Point(1186, 184)
        Label117.Margin = New Padding(5, 0, 5, 0)
        Label117.Name = "Label117"
        Label117.Size = New Size(135, 34)
        Label117.TabIndex = 39
        Label117.Text = "Receive Time"
        Label117.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label108
        ' 
        Label108.Location = New Point(1184, 100)
        Label108.Margin = New Padding(5, 0, 5, 0)
        Label108.Name = "Label108"
        Label108.Size = New Size(140, 34)
        Label108.TabIndex = 38
        Label108.Text = "Receive Date"
        Label108.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' txtRecTime
        ' 
        txtRecTime.Location = New Point(1184, 228)
        txtRecTime.Margin = New Padding(5, 6, 5, 6)
        txtRecTime.Mask = "00:00"
        txtRecTime.Name = "txtRecTime"
        txtRecTime.Size = New Size(138, 31)
        txtRecTime.TabIndex = 37
        txtRecTime.ValidatingType = GetType(Date)
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(1186, 9)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(129, 34)
        Label3.TabIndex = 35
        Label3.Text = "Phlebotomy"
        Label3.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' chkPostPrePhleb
        ' 
        chkPostPrePhleb.Appearance = Appearance.Button
        chkPostPrePhleb.Location = New Point(1184, 47)
        chkPostPrePhleb.Margin = New Padding(5, 6, 5, 6)
        chkPostPrePhleb.Name = "chkPostPrePhleb"
        chkPostPrePhleb.Size = New Size(140, 47)
        chkPostPrePhleb.TabIndex = 34
        chkPostPrePhleb.TabStop = False
        chkPostPrePhleb.Text = "POST"
        chkPostPrePhleb.TextAlign = ContentAlignment.MiddleCenter
        chkPostPrePhleb.UseVisualStyleBackColor = True
        ' 
        ' txtDrawnTime
        ' 
        txtDrawnTime.Location = New Point(706, 311)
        txtDrawnTime.Margin = New Padding(5, 6, 5, 6)
        txtDrawnTime.Mask = "00:00"
        txtDrawnTime.Name = "txtDrawnTime"
        txtDrawnTime.Size = New Size(140, 31)
        txtDrawnTime.TabIndex = 24
        txtDrawnTime.ValidatingType = GetType(Date)
        ' 
        ' txtLabels
        ' 
        txtLabels.AcceptsReturn = True
        txtLabels.Location = New Point(1190, 483)
        txtLabels.Margin = New Padding(5, 6, 5, 6)
        txtLabels.MaxLength = 2
        txtLabels.Name = "txtLabels"
        txtLabels.Size = New Size(130, 31)
        txtLabels.TabIndex = 31
        txtLabels.TabStop = False
        txtLabels.Text = "0"
        txtLabels.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label37
        ' 
        Label37.Location = New Point(1186, 453)
        Label37.Margin = New Padding(5, 0, 5, 0)
        Label37.Name = "Label37"
        Label37.Size = New Size(135, 25)
        Label37.TabIndex = 22
        Label37.Text = "Labels To Print"
        Label37.TextAlign = ContentAlignment.TopRight
        ' 
        ' Label13
        ' 
        Label13.Location = New Point(402, 272)
        Label13.Margin = New Padding(5, 0, 5, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(62, 25)
        Label13.TabIndex = 21
        Label13.Text = "Qty"
        Label13.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' txtQty
        ' 
        txtQty.AcceptsReturn = True
        txtQty.Location = New Point(406, 311)
        txtQty.Margin = New Padding(5, 6, 5, 6)
        txtQty.MaxLength = 2
        txtQty.Name = "txtQty"
        txtQty.Size = New Size(62, 31)
        txtQty.TabIndex = 22
        txtQty.TextAlign = HorizontalAlignment.Center
        ' 
        ' btnSources
        ' 
        btnSources.Image = CType(resources.GetObject("btnSources.Image"), Image)
        btnSources.Location = New Point(311, 258)
        btnSources.Margin = New Padding(5, 6, 5, 6)
        btnSources.Name = "btnSources"
        btnSources.Size = New Size(49, 53)
        btnSources.TabIndex = 21
        btnSources.TabStop = False
        ToolTip1.SetToolTip(btnSources, "Source Management")
        btnSources.UseVisualStyleBackColor = True
        ' 
        ' cmbSource
        ' 
        cmbSource.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSource.FormattingEnabled = True
        cmbSource.Location = New Point(25, 311)
        cmbSource.Margin = New Padding(5, 6, 5, 6)
        cmbSource.Name = "cmbSource"
        cmbSource.Size = New Size(340, 33)
        cmbSource.TabIndex = 20
        ' 
        ' Label12
        ' 
        Label12.Location = New Point(18, 359)
        Label12.Margin = New Padding(5, 0, 5, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(256, 25)
        Label12.TabIndex = 16
        Label12.Text = "Source Comment"
        Label12.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' txtSrcComment
        ' 
        txtSrcComment.Location = New Point(24, 394)
        txtSrcComment.Margin = New Padding(5, 6, 5, 6)
        txtSrcComment.MaxLength = 250
        txtSrcComment.Multiline = True
        txtSrcComment.Name = "txtSrcComment"
        txtSrcComment.ScrollBars = ScrollBars.Vertical
        txtSrcComment.Size = New Size(1074, 121)
        txtSrcComment.TabIndex = 27
        txtSrcComment.TabStop = False
        ' 
        ' Label11
        ' 
        Label11.Location = New Point(1194, 361)
        Label11.Margin = New Padding(5, 0, 5, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(130, 25)
        Label11.TabIndex = 14
        Label11.Text = "Ready to use"
        ' 
        ' chkIsReady
        ' 
        chkIsReady.Appearance = Appearance.Button
        chkIsReady.Checked = True
        chkIsReady.CheckState = CheckState.Checked
        chkIsReady.Location = New Point(1189, 397)
        chkIsReady.Margin = New Padding(5, 6, 5, 6)
        chkIsReady.Name = "chkIsReady"
        chkIsReady.Size = New Size(134, 42)
        chkIsReady.TabIndex = 28
        chkIsReady.TabStop = False
        chkIsReady.Text = "Yes"
        chkIsReady.TextAlign = ContentAlignment.MiddleCenter
        chkIsReady.UseVisualStyleBackColor = True
        ' 
        ' cmbTemp
        ' 
        cmbTemp.DropDownStyle = ComboBoxStyle.DropDownList
        cmbTemp.FormattingEnabled = True
        cmbTemp.ItemHeight = 25
        cmbTemp.Items.AddRange(New Object() {"Refrigerated", "Room Temp", "Frozen", "Incubated"})
        cmbTemp.Location = New Point(882, 311)
        cmbTemp.Margin = New Padding(5, 6, 5, 6)
        cmbTemp.Name = "cmbTemp"
        cmbTemp.Size = New Size(215, 33)
        cmbTemp.TabIndex = 25
        ' 
        ' btnAddSrc
        ' 
        btnAddSrc.Enabled = False
        btnAddSrc.Location = New Point(1184, 303)
        btnAddSrc.Margin = New Padding(5, 6, 5, 6)
        btnAddSrc.Name = "btnAddSrc"
        btnAddSrc.Size = New Size(140, 53)
        btnAddSrc.TabIndex = 26
        btnAddSrc.Text = "Add to List"
        btnAddSrc.UseVisualStyleBackColor = True
        ' 
        ' btnRemAllSrc
        ' 
        btnRemAllSrc.Enabled = False
        btnRemAllSrc.ForeColor = Color.Red
        btnRemAllSrc.Image = CType(resources.GetObject("btnRemAllSrc.Image"), Image)
        btnRemAllSrc.Location = New Point(1071, 203)
        btnRemAllSrc.Margin = New Padding(5, 6, 5, 6)
        btnRemAllSrc.Name = "btnRemAllSrc"
        btnRemAllSrc.Size = New Size(60, 44)
        btnRemAllSrc.TabIndex = 29
        btnRemAllSrc.TabStop = False
        btnRemAllSrc.UseVisualStyleBackColor = True
        ' 
        ' Label10
        ' 
        Label10.Location = New Point(710, 269)
        Label10.Margin = New Padding(5, 0, 5, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(155, 28)
        Label10.TabIndex = 9
        Label10.Text = "Drawn Time"
        Label10.TextAlign = ContentAlignment.BottomLeft
        ' 
        ' Label9
        ' 
        Label9.Location = New Point(22, 6)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(544, 34)
        Label9.TabIndex = 8
        Label9.Text = "Specimen Contents  Note: Double click to edit added specimin."
        Label9.TextAlign = ContentAlignment.BottomLeft
        ' 
        ' btnRemSrc
        ' 
        btnRemSrc.Enabled = False
        btnRemSrc.ForeColor = Color.Red
        btnRemSrc.Image = CType(resources.GetObject("btnRemSrc.Image"), Image)
        btnRemSrc.Location = New Point(1071, 47)
        btnRemSrc.Margin = New Padding(5, 6, 5, 6)
        btnRemSrc.Name = "btnRemSrc"
        btnRemSrc.Size = New Size(60, 44)
        btnRemSrc.TabIndex = 30
        btnRemSrc.TabStop = False
        btnRemSrc.UseVisualStyleBackColor = True
        ' 
        ' dgvSources
        ' 
        dgvSources.AllowUserToAddRows = False
        dgvSources.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.SeaShell
        dgvSources.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = SystemColors.Control
        DataGridViewCellStyle2.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle2.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.True
        dgvSources.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        dgvSources.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvSources.Columns.AddRange(New DataGridViewColumn() {SrcID, SrcNo, SrcName, SrcQty, SrcDate, SrcTime, SrcTemp, SrcComment})
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = SystemColors.Window
        DataGridViewCellStyle3.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle3.ForeColor = Color.DarkBlue
        DataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False
        dgvSources.DefaultCellStyle = DataGridViewCellStyle3
        dgvSources.Location = New Point(26, 47)
        dgvSources.Margin = New Padding(5, 6, 5, 6)
        dgvSources.Name = "dgvSources"
        dgvSources.ReadOnly = True
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = SystemColors.Control
        DataGridViewCellStyle4.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle4.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = DataGridViewTriState.True
        dgvSources.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        dgvSources.RowHeadersVisible = False
        dgvSources.RowHeadersWidth = 56
        dgvSources.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvSources.Size = New Size(1030, 200)
        dgvSources.TabIndex = 19
        dgvSources.TabStop = False
        ' 
        ' SrcID
        ' 
        SrcID.HeaderText = "ID"
        SrcID.MinimumWidth = 7
        SrcID.Name = "SrcID"
        SrcID.ReadOnly = True
        SrcID.Visible = False
        SrcID.Width = 135
        ' 
        ' SrcNo
        ' 
        SrcNo.FillWeight = 60F
        SrcNo.HeaderText = "No."
        SrcNo.MinimumWidth = 7
        SrcNo.Name = "SrcNo"
        SrcNo.ReadOnly = True
        SrcNo.SortMode = DataGridViewColumnSortMode.NotSortable
        SrcNo.Width = 60
        ' 
        ' SrcName
        ' 
        SrcName.HeaderText = "Source Name"
        SrcName.MinimumWidth = 7
        SrcName.Name = "SrcName"
        SrcName.ReadOnly = True
        SrcName.SortMode = DataGridViewColumnSortMode.NotSortable
        SrcName.Width = 135
        ' 
        ' SrcQty
        ' 
        SrcQty.FillWeight = 40F
        SrcQty.HeaderText = "Qty"
        SrcQty.MinimumWidth = 7
        SrcQty.Name = "SrcQty"
        SrcQty.ReadOnly = True
        SrcQty.SortMode = DataGridViewColumnSortMode.NotSortable
        SrcQty.Width = 40
        ' 
        ' SrcDate
        ' 
        SrcDate.FillWeight = 80F
        SrcDate.HeaderText = "Drawn On"
        SrcDate.MinimumWidth = 7
        SrcDate.Name = "SrcDate"
        SrcDate.ReadOnly = True
        SrcDate.SortMode = DataGridViewColumnSortMode.NotSortable
        SrcDate.Width = 80
        ' 
        ' SrcTime
        ' 
        SrcTime.FillWeight = 60F
        SrcTime.HeaderText = "Time"
        SrcTime.MinimumWidth = 7
        SrcTime.Name = "SrcTime"
        SrcTime.ReadOnly = True
        SrcTime.SortMode = DataGridViewColumnSortMode.NotSortable
        SrcTime.Width = 60
        ' 
        ' SrcTemp
        ' 
        SrcTemp.FillWeight = 60F
        SrcTemp.HeaderText = "Temp"
        SrcTemp.MinimumWidth = 7
        SrcTemp.Name = "SrcTemp"
        SrcTemp.ReadOnly = True
        SrcTemp.SortMode = DataGridViewColumnSortMode.NotSortable
        SrcTemp.Width = 60
        ' 
        ' SrcComment
        ' 
        SrcComment.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        SrcComment.FillWeight = 165F
        SrcComment.HeaderText = "Comment"
        SrcComment.MinimumWidth = 7
        SrcComment.Name = "SrcComment"
        SrcComment.ReadOnly = True
        SrcComment.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Label8
        ' 
        Label8.Location = New Point(898, 272)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(196, 25)
        Label8.TabIndex = 4
        Label8.Text = "Temp Environment"
        ' 
        ' dtpDateDrawn
        ' 
        dtpDateDrawn.Format = DateTimePickerFormat.Short
        dtpDateDrawn.Location = New Point(500, 311)
        dtpDateDrawn.Margin = New Padding(5, 6, 5, 6)
        dtpDateDrawn.Name = "dtpDateDrawn"
        dtpDateDrawn.Size = New Size(180, 31)
        dtpDateDrawn.TabIndex = 23
        ' 
        ' Label7
        ' 
        Label7.Location = New Point(505, 269)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(182, 28)
        Label7.TabIndex = 2
        Label7.Text = "Drawn Date"
        Label7.TextAlign = ContentAlignment.BottomLeft
        ' 
        ' Label6
        ' 
        Label6.Location = New Point(18, 278)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(202, 25)
        Label6.TabIndex = 0
        Label6.Text = "Specimen Source"
        Label6.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tpOrderer
        ' 
        tpOrderer.Controls.Add(txtProvEmail)
        tpOrderer.Controls.Add(grpClient)
        tpOrderer.ForeColor = Color.DarkBlue
        tpOrderer.Location = New Point(4, 34)
        tpOrderer.Margin = New Padding(5, 6, 5, 6)
        tpOrderer.Name = "tpOrderer"
        tpOrderer.Padding = New Padding(5, 6, 5, 6)
        tpOrderer.Size = New Size(1342, 571)
        tpOrderer.TabIndex = 1
        tpOrderer.Text = "Client"
        tpOrderer.UseVisualStyleBackColor = True
        ' 
        ' txtProvEmail
        ' 
        txtProvEmail.BackColor = Color.White
        txtProvEmail.Location = New Point(470, 467)
        txtProvEmail.Margin = New Padding(5, 6, 5, 6)
        txtProvEmail.MaxLength = 25
        txtProvEmail.Name = "txtProvEmail"
        txtProvEmail.ReadOnly = True
        txtProvEmail.Size = New Size(494, 31)
        txtProvEmail.TabIndex = 9
        txtProvEmail.TabStop = False
        ' 
        ' grpClient
        ' 
        grpClient.Controls.Add(btn120)
        grpClient.Controls.Add(btn122)
        grpClient.Controls.Add(lstProviders)
        grpClient.Controls.Add(txtOrdFax)
        grpClient.Controls.Add(txtOrdPhone)
        grpClient.Controls.Add(Label96)
        grpClient.Controls.Add(Label95)
        grpClient.Controls.Add(btnRemProv)
        grpClient.Controls.Add(Label25)
        grpClient.Controls.Add(txtCountry)
        grpClient.Controls.Add(Label19)
        grpClient.Controls.Add(Label18)
        grpClient.Controls.Add(Label17)
        grpClient.Controls.Add(txtOrdCSZ)
        grpClient.Controls.Add(Label16)
        grpClient.Controls.Add(txtOrdAddress)
        grpClient.Controls.Add(Label15)
        grpClient.Controls.Add(txtOrdName)
        grpClient.Controls.Add(btnOrdLookup)
        grpClient.Controls.Add(Label14)
        grpClient.Controls.Add(txtOrdID)
        grpClient.Location = New Point(14, 8)
        grpClient.Margin = New Padding(5, 6, 5, 6)
        grpClient.Name = "grpClient"
        grpClient.Padding = New Padding(5, 6, 5, 6)
        grpClient.Size = New Size(1275, 522)
        grpClient.TabIndex = 45
        grpClient.TabStop = False
        ' 
        ' btn120
        ' 
        btn120.Image = CType(resources.GetObject("btn120.Image"), Image)
        btn120.Location = New Point(1076, 428)
        btn120.Margin = New Padding(5, 6, 5, 6)
        btn120.Name = "btn120"
        btn120.Size = New Size(75, 69)
        btn120.TabIndex = 12
        btn120.TabStop = False
        btn120.UseVisualStyleBackColor = True
        ' 
        ' btn122
        ' 
        btn122.Image = CType(resources.GetObject("btn122.Image"), Image)
        btn122.Location = New Point(1162, 428)
        btn122.Margin = New Padding(5, 6, 5, 6)
        btn122.Name = "btn122"
        btn122.Size = New Size(75, 69)
        btn122.TabIndex = 11
        btn122.TabStop = False
        btn122.UseVisualStyleBackColor = True
        ' 
        ' lstProviders
        ' 
        lstProviders.FormattingEnabled = True
        lstProviders.Location = New Point(624, 58)
        lstProviders.Margin = New Padding(5, 6, 5, 6)
        lstProviders.Name = "lstProviders"
        lstProviders.Size = New Size(610, 340)
        lstProviders.TabIndex = 45
        ' 
        ' txtOrdFax
        ' 
        txtOrdFax.BackColor = SystemColors.ButtonHighlight
        txtOrdFax.Location = New Point(260, 459)
        txtOrdFax.Margin = New Padding(5, 6, 5, 6)
        txtOrdFax.Name = "txtOrdFax"
        txtOrdFax.ReadOnly = True
        txtOrdFax.Size = New Size(184, 31)
        txtOrdFax.TabIndex = 8
        txtOrdFax.TabStop = False
        txtOrdFax.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
        ' 
        ' txtOrdPhone
        ' 
        txtOrdPhone.BackColor = SystemColors.ButtonHighlight
        txtOrdPhone.Location = New Point(64, 459)
        txtOrdPhone.Margin = New Padding(5, 6, 5, 6)
        txtOrdPhone.Name = "txtOrdPhone"
        txtOrdPhone.ReadOnly = True
        txtOrdPhone.Size = New Size(184, 31)
        txtOrdPhone.TabIndex = 7
        txtOrdPhone.TabStop = False
        txtOrdPhone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
        ' 
        ' Label96
        ' 
        Label96.Location = New Point(458, 428)
        Label96.Margin = New Padding(5, 0, 5, 0)
        Label96.Name = "Label96"
        Label96.Size = New Size(156, 25)
        Label96.TabIndex = 44
        Label96.Text = "Email"
        ' 
        ' Label95
        ' 
        Label95.Location = New Point(618, 28)
        Label95.Margin = New Padding(5, 0, 5, 0)
        Label95.Name = "Label95"
        Label95.Size = New Size(198, 25)
        Label95.TabIndex = 41
        Label95.Text = "Attending Provider"
        ' 
        ' btnRemProv
        ' 
        btnRemProv.Enabled = False
        btnRemProv.Image = CType(resources.GetObject("btnRemProv.Image"), Image)
        btnRemProv.Location = New Point(4, 33)
        btnRemProv.Margin = New Padding(5, 6, 5, 6)
        btnRemProv.Name = "btnRemProv"
        btnRemProv.Size = New Size(50, 50)
        btnRemProv.TabIndex = 11
        btnRemProv.TabStop = False
        btnRemProv.UseVisualStyleBackColor = True
        ' 
        ' Label25
        ' 
        Label25.Location = New Point(64, 342)
        Label25.Margin = New Padding(5, 0, 5, 0)
        Label25.Name = "Label25"
        Label25.Size = New Size(189, 25)
        Label25.TabIndex = 20
        Label25.Text = "Country"
        ' 
        ' txtCountry
        ' 
        txtCountry.BackColor = Color.White
        txtCountry.Location = New Point(64, 372)
        txtCountry.Margin = New Padding(5, 6, 5, 6)
        txtCountry.MaxLength = 35
        txtCountry.Name = "txtCountry"
        txtCountry.ReadOnly = True
        txtCountry.Size = New Size(186, 31)
        txtCountry.TabIndex = 6
        txtCountry.TabStop = False
        ' 
        ' Label19
        ' 
        Label19.Location = New Point(269, 428)
        Label19.Margin = New Padding(5, 0, 5, 0)
        Label19.Name = "Label19"
        Label19.Size = New Size(156, 25)
        Label19.TabIndex = 16
        Label19.Text = "Fax"
        ' 
        ' Label18
        ' 
        Label18.Location = New Point(64, 428)
        Label18.Margin = New Padding(5, 0, 5, 0)
        Label18.Name = "Label18"
        Label18.Size = New Size(214, 25)
        Label18.TabIndex = 14
        Label18.Text = "Telephone"
        ' 
        ' Label17
        ' 
        Label17.Location = New Point(64, 256)
        Label17.Margin = New Padding(5, 0, 5, 0)
        Label17.Name = "Label17"
        Label17.Size = New Size(274, 25)
        Label17.TabIndex = 12
        Label17.Text = "City, State and Zip"
        ' 
        ' txtOrdCSZ
        ' 
        txtOrdCSZ.BackColor = Color.White
        txtOrdCSZ.Location = New Point(64, 286)
        txtOrdCSZ.Margin = New Padding(5, 6, 5, 6)
        txtOrdCSZ.MaxLength = 112
        txtOrdCSZ.Name = "txtOrdCSZ"
        txtOrdCSZ.ReadOnly = True
        txtOrdCSZ.Size = New Size(506, 31)
        txtOrdCSZ.TabIndex = 5
        txtOrdCSZ.TabStop = False
        ' 
        ' Label16
        ' 
        Label16.Location = New Point(64, 175)
        Label16.Margin = New Padding(5, 0, 5, 0)
        Label16.Name = "Label16"
        Label16.Size = New Size(186, 25)
        Label16.TabIndex = 10
        Label16.Text = "Address"
        ' 
        ' txtOrdAddress
        ' 
        txtOrdAddress.BackColor = Color.White
        txtOrdAddress.Location = New Point(64, 206)
        txtOrdAddress.Margin = New Padding(5, 6, 5, 6)
        txtOrdAddress.MaxLength = 75
        txtOrdAddress.Name = "txtOrdAddress"
        txtOrdAddress.ReadOnly = True
        txtOrdAddress.Size = New Size(506, 31)
        txtOrdAddress.TabIndex = 4
        txtOrdAddress.TabStop = False
        ' 
        ' Label15
        ' 
        Label15.Location = New Point(58, 97)
        Label15.Margin = New Padding(5, 0, 5, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(238, 25)
        Label15.TabIndex = 8
        Label15.Text = "Ordering Provider Name"
        ' 
        ' txtOrdName
        ' 
        txtOrdName.BackColor = Color.White
        txtOrdName.Location = New Point(64, 128)
        txtOrdName.Margin = New Padding(5, 6, 5, 6)
        txtOrdName.MaxLength = 95
        txtOrdName.Name = "txtOrdName"
        txtOrdName.ReadOnly = True
        txtOrdName.Size = New Size(506, 31)
        txtOrdName.TabIndex = 3
        txtOrdName.TabStop = False
        ' 
        ' btnOrdLookup
        ' 
        btnOrdLookup.Image = CType(resources.GetObject("btnOrdLookup.Image"), Image)
        btnOrdLookup.Location = New Point(260, 33)
        btnOrdLookup.Margin = New Padding(5, 6, 5, 6)
        btnOrdLookup.Name = "btnOrdLookup"
        btnOrdLookup.Size = New Size(50, 50)
        btnOrdLookup.TabIndex = 2
        btnOrdLookup.TabStop = False
        btnOrdLookup.UseVisualStyleBackColor = True
        ' 
        ' Label14
        ' 
        Label14.Location = New Point(58, 9)
        Label14.Margin = New Padding(5, 0, 5, 0)
        Label14.Name = "Label14"
        Label14.Size = New Size(210, 25)
        Label14.TabIndex = 1
        Label14.Text = "Orderer ID"
        ' 
        ' txtOrdID
        ' 
        txtOrdID.Location = New Point(64, 41)
        txtOrdID.Margin = New Padding(5, 6, 5, 6)
        txtOrdID.MaxLength = 12
        txtOrdID.Name = "txtOrdID"
        txtOrdID.Size = New Size(184, 31)
        txtOrdID.TabIndex = 1
        txtOrdID.TextAlign = HorizontalAlignment.Center
        ' 
        ' tpPatient
        ' 
        tpPatient.Controls.Add(grpPatient)
        tpPatient.ForeColor = Color.DarkBlue
        tpPatient.Location = New Point(4, 34)
        tpPatient.Margin = New Padding(5, 6, 5, 6)
        tpPatient.Name = "tpPatient"
        tpPatient.Size = New Size(1342, 571)
        tpPatient.TabIndex = 2
        tpPatient.Text = "Patient"
        tpPatient.UseVisualStyleBackColor = True
        ' 
        ' grpPatient
        ' 
        grpPatient.Controls.Add(tcDxMeds)
        grpPatient.Controls.Add(cmbRace)
        grpPatient.Controls.Add(Label130)
        grpPatient.Controls.Add(Label127)
        grpPatient.Controls.Add(txtTage)
        grpPatient.Controls.Add(txtFax)
        grpPatient.Controls.Add(Label126)
        grpPatient.Controls.Add(txtCell)
        grpPatient.Controls.Add(Label125)
        grpPatient.Controls.Add(txtWPhone)
        grpPatient.Controls.Add(Label124)
        grpPatient.Controls.Add(Label119)
        grpPatient.Controls.Add(cmbEthnicity)
        grpPatient.Controls.Add(Label118)
        grpPatient.Controls.Add(txtRoom)
        grpPatient.Controls.Add(txtPatHPhone)
        grpPatient.Controls.Add(lblChart)
        grpPatient.Controls.Add(txtEMRNo)
        grpPatient.Controls.Add(chkFasting)
        grpPatient.Controls.Add(btnPatUpdate)
        grpPatient.Controls.Add(Label100)
        grpPatient.Controls.Add(Label99)
        grpPatient.Controls.Add(Label98)
        grpPatient.Controls.Add(txtPatCountry)
        grpPatient.Controls.Add(txtPatZip)
        grpPatient.Controls.Add(txtPatState)
        grpPatient.Controls.Add(Label97)
        grpPatient.Controls.Add(txtPatAdr2)
        grpPatient.Controls.Add(btnRemPat)
        grpPatient.Controls.Add(Label36)
        grpPatient.Controls.Add(Label34)
        grpPatient.Controls.Add(Label35)
        grpPatient.Controls.Add(txtPatientID)
        grpPatient.Controls.Add(Label30)
        grpPatient.Controls.Add(txtDOB)
        grpPatient.Controls.Add(btnPatLook)
        grpPatient.Controls.Add(cmbSex)
        grpPatient.Controls.Add(txtLName)
        grpPatient.Controls.Add(Label33)
        grpPatient.Controls.Add(Label29)
        grpPatient.Controls.Add(txtPatAdr1)
        grpPatient.Controls.Add(Label32)
        grpPatient.Controls.Add(Label28)
        grpPatient.Controls.Add(txtSSN)
        grpPatient.Controls.Add(txtPatCity)
        grpPatient.Controls.Add(Label31)
        grpPatient.Controls.Add(Label27)
        grpPatient.Controls.Add(txtMName)
        grpPatient.Controls.Add(txtPatEmail)
        grpPatient.Controls.Add(txtFName)
        grpPatient.Controls.Add(Label26)
        grpPatient.Controls.Add(gbVeterinary)
        grpPatient.Location = New Point(11, 9)
        grpPatient.Margin = New Padding(5, 6, 5, 6)
        grpPatient.Name = "grpPatient"
        grpPatient.Padding = New Padding(5, 6, 5, 6)
        grpPatient.Size = New Size(1304, 517)
        grpPatient.TabIndex = 45
        grpPatient.TabStop = False
        ' 
        ' tcDxMeds
        ' 
        tcDxMeds.Controls.Add(tpCodes)
        tcDxMeds.Controls.Add(tpMeds)
        tcDxMeds.Location = New Point(874, 39)
        tcDxMeds.Margin = New Padding(5, 6, 5, 6)
        tcDxMeds.Name = "tcDxMeds"
        tcDxMeds.SelectedIndex = 0
        tcDxMeds.Size = New Size(434, 397)
        tcDxMeds.TabIndex = 65
        ' 
        ' tpCodes
        ' 
        tpCodes.Controls.Add(PanelDX)
        tpCodes.Controls.Add(dgvDxs)
        tpCodes.Location = New Point(4, 34)
        tpCodes.Margin = New Padding(5, 6, 5, 6)
        tpCodes.Name = "tpCodes"
        tpCodes.Padding = New Padding(5, 6, 5, 6)
        tpCodes.Size = New Size(426, 359)
        tpCodes.TabIndex = 0
        tpCodes.Text = "Dx Codes"
        tpCodes.UseVisualStyleBackColor = True
        ' 
        ' PanelDX
        ' 
        PanelDX.Controls.Add(LoadDx)
        PanelDX.Controls.Add(btnRemDxAll)
        PanelDX.Location = New Point(55, 294)
        PanelDX.Margin = New Padding(4, 3, 4, 3)
        PanelDX.Name = "PanelDX"
        PanelDX.Size = New Size(324, 56)
        PanelDX.TabIndex = 81
        ' 
        ' LoadDx
        ' 
        LoadDx.ForeColor = Color.Green
        LoadDx.ImageAlign = ContentAlignment.MiddleLeft
        LoadDx.Location = New Point(9, 6)
        LoadDx.Margin = New Padding(5, 6, 5, 6)
        LoadDx.Name = "LoadDx"
        LoadDx.Size = New Size(144, 44)
        LoadDx.TabIndex = 25
        LoadDx.Text = "Auto Load"
        LoadDx.TextImageRelation = TextImageRelation.ImageBeforeText
        LoadDx.UseVisualStyleBackColor = True
        ' 
        ' btnRemDxAll
        ' 
        btnRemDxAll.ForeColor = Color.Red
        btnRemDxAll.Image = CType(resources.GetObject("btnRemDxAll.Image"), Image)
        btnRemDxAll.Location = New Point(165, 6)
        btnRemDxAll.Margin = New Padding(5, 6, 5, 6)
        btnRemDxAll.Name = "btnRemDxAll"
        btnRemDxAll.Size = New Size(154, 44)
        btnRemDxAll.TabIndex = 23
        btnRemDxAll.Text = "Remove All"
        btnRemDxAll.TextAlign = ContentAlignment.MiddleRight
        btnRemDxAll.TextImageRelation = TextImageRelation.ImageBeforeText
        btnRemDxAll.UseVisualStyleBackColor = True
        ' 
        ' dgvDxs
        ' 
        dgvDxs.AllowUserToAddRows = False
        dgvDxs.AllowUserToDeleteRows = False
        DataGridViewCellStyle5.BackColor = Color.Linen
        dgvDxs.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        dgvDxs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvDxs.Columns.AddRange(New DataGridViewColumn() {Dx_Code, Lookup, remd})
        dgvDxs.Location = New Point(10, 11)
        dgvDxs.Margin = New Padding(5, 6, 5, 6)
        dgvDxs.Name = "dgvDxs"
        dgvDxs.RowHeadersVisible = False
        dgvDxs.RowHeadersWidth = 56
        dgvDxs.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvDxs.Size = New Size(405, 278)
        dgvDxs.TabIndex = 19
        ' 
        ' Dx_Code
        ' 
        Dx_Code.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Dx_Code.FillWeight = 136F
        Dx_Code.HeaderText = "Dx Code"
        Dx_Code.MaxInputLength = 12
        Dx_Code.MinimumWidth = 7
        Dx_Code.Name = "Dx_Code"
        ' 
        ' Lookup
        ' 
        Lookup.FillWeight = 52F
        Lookup.HeaderText = "LookUp"
        Lookup.Image = CType(resources.GetObject("Lookup.Image"), Image)
        Lookup.MinimumWidth = 70
        Lookup.Name = "Lookup"
        Lookup.Width = 70
        ' 
        ' remd
        ' 
        remd.FillWeight = 30F
        remd.HeaderText = ""
        remd.Image = My.Resources.Resources.icons8_delete_16
        remd.MinimumWidth = 7
        remd.Name = "remd"
        remd.Width = 30
        ' 
        ' tpMeds
        ' 
        tpMeds.Controls.Add(PanelMedi)
        tpMeds.Controls.Add(dgvMeds)
        tpMeds.Location = New Point(4, 34)
        tpMeds.Margin = New Padding(5, 6, 5, 6)
        tpMeds.Name = "tpMeds"
        tpMeds.Padding = New Padding(5, 6, 5, 6)
        tpMeds.Size = New Size(426, 359)
        tpMeds.TabIndex = 1
        tpMeds.Text = "Medications"
        tpMeds.UseVisualStyleBackColor = True
        ' 
        ' PanelMedi
        ' 
        PanelMedi.Controls.Add(Button2)
        PanelMedi.Controls.Add(RemMedAll)
        PanelMedi.Location = New Point(38, 297)
        PanelMedi.Margin = New Padding(4, 3, 4, 3)
        PanelMedi.Name = "PanelMedi"
        PanelMedi.Size = New Size(336, 53)
        PanelMedi.TabIndex = 82
        ' 
        ' Button2
        ' 
        Button2.ForeColor = Color.Green
        Button2.ImageAlign = ContentAlignment.MiddleLeft
        Button2.Location = New Point(10, 3)
        Button2.Margin = New Padding(5, 6, 5, 6)
        Button2.Name = "Button2"
        Button2.Size = New Size(154, 42)
        Button2.TabIndex = 24
        Button2.Text = "Auto Load"
        Button2.TextImageRelation = TextImageRelation.ImageBeforeText
        Button2.UseVisualStyleBackColor = True
        ' 
        ' RemMedAll
        ' 
        RemMedAll.ForeColor = Color.Red
        RemMedAll.Image = CType(resources.GetObject("RemMedAll.Image"), Image)
        RemMedAll.Location = New Point(174, 6)
        RemMedAll.Margin = New Padding(5, 6, 5, 6)
        RemMedAll.Name = "RemMedAll"
        RemMedAll.Size = New Size(154, 41)
        RemMedAll.TabIndex = 23
        RemMedAll.Text = "Remove All"
        RemMedAll.TextAlign = ContentAlignment.MiddleRight
        RemMedAll.TextImageRelation = TextImageRelation.ImageBeforeText
        RemMedAll.UseVisualStyleBackColor = True
        ' 
        ' dgvMeds
        ' 
        dgvMeds.AllowUserToAddRows = False
        dgvMeds.AllowUserToDeleteRows = False
        DataGridViewCellStyle6.BackColor = Color.Azure
        dgvMeds.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle6
        dgvMeds.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvMeds.Columns.AddRange(New DataGridViewColumn() {MedName, MedLook, remm})
        dgvMeds.Location = New Point(6, 11)
        dgvMeds.Margin = New Padding(5, 6, 5, 6)
        dgvMeds.Name = "dgvMeds"
        dgvMeds.RowHeadersVisible = False
        dgvMeds.RowHeadersWidth = 56
        dgvMeds.Size = New Size(406, 275)
        dgvMeds.TabIndex = 0
        ' 
        ' MedName
        ' 
        MedName.FillWeight = 160F
        MedName.HeaderText = "Medication"
        MedName.MaxInputLength = 100
        MedName.MinimumWidth = 7
        MedName.Name = "MedName"
        MedName.SortMode = DataGridViewColumnSortMode.NotSortable
        MedName.Width = 160
        ' 
        ' MedLook
        ' 
        MedLook.FillWeight = 30F
        MedLook.HeaderText = ""
        MedLook.Image = CType(resources.GetObject("MedLook.Image"), Image)
        MedLook.MinimumWidth = 7
        MedLook.Name = "MedLook"
        MedLook.Width = 30
        ' 
        ' remm
        ' 
        remm.FillWeight = 30F
        remm.HeaderText = ""
        remm.Image = My.Resources.Resources.icons8_delete_161
        remm.MinimumWidth = 7
        remm.Name = "remm"
        remm.Width = 30
        ' 
        ' cmbRace
        ' 
        cmbRace.DropDownStyle = ComboBoxStyle.DropDownList
        cmbRace.FormattingEnabled = True
        cmbRace.Location = New Point(196, 350)
        cmbRace.Margin = New Padding(5, 6, 5, 6)
        cmbRace.Name = "cmbRace"
        cmbRace.Size = New Size(273, 33)
        cmbRace.TabIndex = 79
        ' 
        ' Label130
        ' 
        Label130.ForeColor = Color.DarkBlue
        Label130.Location = New Point(489, 317)
        Label130.Margin = New Padding(5, 0, 5, 0)
        Label130.Name = "Label130"
        Label130.Size = New Size(140, 25)
        Label130.TabIndex = 78
        Label130.Text = "Ethnicity"
        ' 
        ' Label127
        ' 
        Label127.Location = New Point(238, 125)
        Label127.Margin = New Padding(5, 0, 5, 0)
        Label127.Name = "Label127"
        Label127.Size = New Size(115, 25)
        Label127.TabIndex = 76
        Label127.Text = "Tage (Years)"
        ' 
        ' txtTage
        ' 
        txtTage.BackColor = Color.White
        txtTage.Location = New Point(238, 156)
        txtTage.Margin = New Padding(5, 6, 5, 6)
        txtTage.MaxLength = 35
        txtTage.Name = "txtTage"
        txtTage.ReadOnly = True
        txtTage.Size = New Size(113, 31)
        txtTage.TabIndex = 75
        txtTage.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtFax
        ' 
        txtFax.Location = New Point(310, 439)
        txtFax.Margin = New Padding(5, 6, 5, 6)
        txtFax.Mask = "(999) 000-0000"
        txtFax.Name = "txtFax"
        txtFax.Size = New Size(130, 31)
        txtFax.TabIndex = 20
        ' 
        ' Label126
        ' 
        Label126.Location = New Point(320, 408)
        Label126.Margin = New Padding(5, 0, 5, 0)
        Label126.Name = "Label126"
        Label126.Size = New Size(74, 25)
        Label126.TabIndex = 74
        Label126.Text = "Fax"
        ' 
        ' txtCell
        ' 
        txtCell.Location = New Point(166, 439)
        txtCell.Margin = New Padding(5, 6, 5, 6)
        txtCell.Name = "txtCell"
        txtCell.Size = New Size(130, 31)
        txtCell.TabIndex = 19
        ' 
        ' Label125
        ' 
        Label125.Location = New Point(170, 409)
        Label125.Margin = New Padding(5, 0, 5, 0)
        Label125.Name = "Label125"
        Label125.Size = New Size(110, 25)
        Label125.TabIndex = 72
        Label125.Text = "Cell Phone"
        ' 
        ' txtWPhone
        ' 
        txtWPhone.Location = New Point(24, 439)
        txtWPhone.Margin = New Padding(5, 6, 5, 6)
        txtWPhone.Name = "txtWPhone"
        txtWPhone.Size = New Size(130, 31)
        txtWPhone.TabIndex = 18
        ' 
        ' Label124
        ' 
        Label124.Location = New Point(35, 408)
        Label124.Margin = New Padding(5, 0, 5, 0)
        Label124.Name = "Label124"
        Label124.Size = New Size(122, 25)
        Label124.TabIndex = 70
        Label124.Text = "Work Phone"
        ' 
        ' Label119
        ' 
        Label119.ForeColor = Color.DarkBlue
        Label119.Location = New Point(202, 319)
        Label119.Margin = New Padding(5, 0, 5, 0)
        Label119.Name = "Label119"
        Label119.Size = New Size(140, 25)
        Label119.TabIndex = 68
        Label119.Text = "Race"
        ' 
        ' cmbEthnicity
        ' 
        cmbEthnicity.DropDownStyle = ComboBoxStyle.DropDownList
        cmbEthnicity.FormattingEnabled = True
        cmbEthnicity.Items.AddRange(New Object() {"Hispanic or Latino", "Not Hispanic or Latino", "Unknown"})
        cmbEthnicity.Location = New Point(482, 347)
        cmbEthnicity.Margin = New Padding(5, 6, 5, 6)
        cmbEthnicity.Name = "cmbEthnicity"
        cmbEthnicity.Size = New Size(213, 33)
        cmbEthnicity.TabIndex = 16
        ' 
        ' Label118
        ' 
        Label118.Location = New Point(615, 408)
        Label118.Margin = New Padding(5, 0, 5, 0)
        Label118.Name = "Label118"
        Label118.Size = New Size(80, 25)
        Label118.TabIndex = 66
        Label118.Text = "Room#"
        ' 
        ' txtRoom
        ' 
        txtRoom.BackColor = Color.Ivory
        txtRoom.Location = New Point(595, 439)
        txtRoom.Margin = New Padding(5, 6, 5, 6)
        txtRoom.MaxLength = 25
        txtRoom.Name = "txtRoom"
        txtRoom.Size = New Size(128, 31)
        txtRoom.TabIndex = 22
        ' 
        ' txtPatHPhone
        ' 
        txtPatHPhone.Location = New Point(709, 347)
        txtPatHPhone.Margin = New Padding(5, 6, 5, 6)
        txtPatHPhone.Name = "txtPatHPhone"
        txtPatHPhone.Size = New Size(144, 31)
        txtPatHPhone.TabIndex = 17
        ' 
        ' lblChart
        ' 
        lblChart.Location = New Point(454, 409)
        lblChart.Margin = New Padding(5, 0, 5, 0)
        lblChart.Name = "lblChart"
        lblChart.Size = New Size(131, 25)
        lblChart.TabIndex = 62
        lblChart.Text = "Chart/EMR No"
        ' 
        ' txtEMRNo
        ' 
        txtEMRNo.BackColor = Color.Ivory
        txtEMRNo.Location = New Point(454, 441)
        txtEMRNo.Margin = New Padding(5, 6, 5, 6)
        txtEMRNo.MaxLength = 25
        txtEMRNo.Name = "txtEMRNo"
        txtEMRNo.Size = New Size(129, 31)
        txtEMRNo.TabIndex = 21
        ToolTip1.SetToolTip(txtEMRNo, "Patient Chart or the patient Identification in the EMR in use")
        ' 
        ' chkFasting
        ' 
        chkFasting.CheckAlign = ContentAlignment.MiddleRight
        chkFasting.Location = New Point(735, 439)
        chkFasting.Margin = New Padding(5, 6, 5, 6)
        chkFasting.Name = "chkFasting"
        chkFasting.Size = New Size(114, 42)
        chkFasting.TabIndex = 23
        chkFasting.Text = "Fasting?"
        chkFasting.UseVisualStyleBackColor = True
        ' 
        ' btnPatUpdate
        ' 
        btnPatUpdate.ForeColor = Color.DarkBlue
        btnPatUpdate.Location = New Point(889, 442)
        btnPatUpdate.Margin = New Padding(5, 6, 5, 6)
        btnPatUpdate.Name = "btnPatUpdate"
        btnPatUpdate.Size = New Size(158, 53)
        btnPatUpdate.TabIndex = 20
        btnPatUpdate.Text = "Validate Patient"
        btnPatUpdate.TextAlign = ContentAlignment.MiddleRight
        btnPatUpdate.TextImageRelation = TextImageRelation.ImageBeforeText
        btnPatUpdate.UseVisualStyleBackColor = True
        ' 
        ' Label100
        ' 
        Label100.ForeColor = Color.DarkBlue
        Label100.Location = New Point(36, 319)
        Label100.Margin = New Padding(5, 0, 5, 0)
        Label100.Name = "Label100"
        Label100.Size = New Size(140, 25)
        Label100.TabIndex = 60
        Label100.Text = "Country"
        ' 
        ' Label99
        ' 
        Label99.ForeColor = Color.Fuchsia
        Label99.Location = New Point(711, 228)
        Label99.Margin = New Padding(5, 0, 5, 0)
        Label99.Name = "Label99"
        Label99.Size = New Size(130, 25)
        Label99.TabIndex = 59
        Label99.Text = "Zip Code"
        ' 
        ' Label98
        ' 
        Label98.ForeColor = Color.Fuchsia
        Label98.Location = New Point(556, 228)
        Label98.Margin = New Padding(5, 0, 5, 0)
        Label98.Name = "Label98"
        Label98.Size = New Size(138, 25)
        Label98.TabIndex = 58
        Label98.Text = "State/Province"
        ' 
        ' txtPatCountry
        ' 
        txtPatCountry.BackColor = Color.White
        txtPatCountry.Location = New Point(24, 350)
        txtPatCountry.Margin = New Padding(5, 6, 5, 6)
        txtPatCountry.MaxLength = 35
        txtPatCountry.Name = "txtPatCountry"
        txtPatCountry.Size = New Size(154, 31)
        txtPatCountry.TabIndex = 15
        ' 
        ' txtPatZip
        ' 
        txtPatZip.BackColor = Color.Ivory
        txtPatZip.Location = New Point(709, 258)
        txtPatZip.Margin = New Padding(5, 6, 5, 6)
        txtPatZip.MaxLength = 35
        txtPatZip.Name = "txtPatZip"
        txtPatZip.Size = New Size(144, 31)
        txtPatZip.TabIndex = 14
        ' 
        ' txtPatState
        ' 
        txtPatState.BackColor = Color.Ivory
        txtPatState.Location = New Point(540, 258)
        txtPatState.Margin = New Padding(5, 6, 5, 6)
        txtPatState.MaxLength = 35
        txtPatState.Name = "txtPatState"
        txtPatState.Size = New Size(154, 31)
        txtPatState.TabIndex = 13
        ' 
        ' Label97
        ' 
        Label97.ForeColor = Color.DarkBlue
        Label97.Location = New Point(196, 228)
        Label97.Margin = New Padding(5, 0, 5, 0)
        Label97.Name = "Label97"
        Label97.Size = New Size(115, 25)
        Label97.TabIndex = 54
        Label97.Text = "Address 2"
        ' 
        ' txtPatAdr2
        ' 
        txtPatAdr2.BackColor = Color.White
        txtPatAdr2.Location = New Point(196, 258)
        txtPatAdr2.Margin = New Padding(5, 6, 5, 6)
        txtPatAdr2.MaxLength = 35
        txtPatAdr2.Name = "txtPatAdr2"
        txtPatAdr2.Size = New Size(154, 31)
        txtPatAdr2.TabIndex = 11
        ' 
        ' btnRemPat
        ' 
        btnRemPat.Enabled = False
        btnRemPat.Image = CType(resources.GetObject("btnRemPat.Image"), Image)
        btnRemPat.Location = New Point(24, 53)
        btnRemPat.Margin = New Padding(5, 6, 5, 6)
        btnRemPat.Name = "btnRemPat"
        btnRemPat.Size = New Size(50, 50)
        btnRemPat.TabIndex = 63
        btnRemPat.TabStop = False
        btnRemPat.UseVisualStyleBackColor = True
        ' 
        ' Label36
        ' 
        Label36.ForeColor = Color.Red
        Label36.Location = New Point(369, 125)
        Label36.Margin = New Padding(5, 0, 5, 0)
        Label36.Name = "Label36"
        Label36.Size = New Size(114, 25)
        Label36.TabIndex = 44
        Label36.Text = "DOB"
        ' 
        ' Label34
        ' 
        Label34.ForeColor = Color.DarkBlue
        Label34.Location = New Point(78, 31)
        Label34.Margin = New Padding(5, 0, 5, 0)
        Label34.Name = "Label34"
        Label34.Size = New Size(131, 25)
        Label34.TabIndex = 42
        Label34.Text = "Patient ID"
        ' 
        ' Label35
        ' 
        Label35.ForeColor = Color.Red
        Label35.Location = New Point(26, 125)
        Label35.Margin = New Padding(5, 0, 5, 0)
        Label35.Name = "Label35"
        Label35.Size = New Size(102, 25)
        Label35.TabIndex = 43
        Label35.Text = "Gender"
        ' 
        ' txtPatientID
        ' 
        txtPatientID.BackColor = Color.Ivory
        txtPatientID.Location = New Point(84, 61)
        txtPatientID.Margin = New Padding(5, 6, 5, 6)
        txtPatientID.MaxLength = 12
        txtPatientID.Name = "txtPatientID"
        txtPatientID.ReadOnly = True
        txtPatientID.Size = New Size(135, 31)
        txtPatientID.TabIndex = 1
        txtPatientID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label30
        ' 
        Label30.ForeColor = Color.Red
        Label30.Location = New Point(518, 31)
        Label30.Margin = New Padding(5, 0, 5, 0)
        Label30.Name = "Label30"
        Label30.Size = New Size(145, 25)
        Label30.TabIndex = 22
        Label30.Text = "First Name"
        ' 
        ' txtDOB
        ' 
        txtDOB.BackColor = Color.Ivory
        txtDOB.Location = New Point(364, 156)
        txtDOB.Margin = New Padding(5, 6, 5, 6)
        txtDOB.Mask = "00/00/0000"
        txtDOB.Name = "txtDOB"
        txtDOB.Size = New Size(120, 31)
        txtDOB.TabIndex = 7
        txtDOB.ValidatingType = GetType(Date)
        ' 
        ' btnPatLook
        ' 
        btnPatLook.Image = CType(resources.GetObject("btnPatLook.Image"), Image)
        btnPatLook.Location = New Point(238, 53)
        btnPatLook.Margin = New Padding(5, 6, 5, 6)
        btnPatLook.Name = "btnPatLook"
        btnPatLook.Size = New Size(50, 50)
        btnPatLook.TabIndex = 2
        btnPatLook.TabStop = False
        btnPatLook.UseVisualStyleBackColor = True
        ' 
        ' cmbSex
        ' 
        cmbSex.BackColor = Color.Ivory
        cmbSex.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSex.FormattingEnabled = True
        cmbSex.Items.AddRange(New Object() {"F - Female", "M - Male", "G - Transgender Female", "N - Transgender Male ", "I - Indetermined", "U - Unreported"})
        cmbSex.Location = New Point(24, 156)
        cmbSex.Margin = New Padding(5, 6, 5, 6)
        cmbSex.Name = "cmbSex"
        cmbSex.Size = New Size(195, 33)
        cmbSex.TabIndex = 6
        ' 
        ' txtLName
        ' 
        txtLName.BackColor = Color.Ivory
        txtLName.Location = New Point(298, 61)
        txtLName.Margin = New Padding(5, 6, 5, 6)
        txtLName.MaxLength = 35
        txtLName.Name = "txtLName"
        txtLName.Size = New Size(186, 31)
        txtLName.TabIndex = 3
        ' 
        ' Label33
        ' 
        Label33.Location = New Point(714, 317)
        Label33.Margin = New Padding(5, 0, 5, 0)
        Label33.Name = "Label33"
        Label33.Size = New Size(130, 25)
        Label33.TabIndex = 38
        Label33.Text = "Home Phone"
        ' 
        ' Label29
        ' 
        Label29.ForeColor = Color.Red
        Label29.Location = New Point(320, 31)
        Label29.Margin = New Padding(5, 0, 5, 0)
        Label29.Name = "Label29"
        Label29.Size = New Size(158, 25)
        Label29.TabIndex = 25
        Label29.Text = "Last Name"
        ' 
        ' txtPatAdr1
        ' 
        txtPatAdr1.BackColor = Color.Ivory
        txtPatAdr1.Location = New Point(24, 258)
        txtPatAdr1.Margin = New Padding(5, 6, 5, 6)
        txtPatAdr1.MaxLength = 35
        txtPatAdr1.Name = "txtPatAdr1"
        txtPatAdr1.Size = New Size(154, 31)
        txtPatAdr1.TabIndex = 10
        ' 
        ' Label32
        ' 
        Label32.Location = New Point(540, 125)
        Label32.Margin = New Padding(5, 0, 5, 0)
        Label32.Name = "Label32"
        Label32.Size = New Size(102, 25)
        Label32.TabIndex = 36
        Label32.Text = "SSN"
        ' 
        ' Label28
        ' 
        Label28.ForeColor = Color.Fuchsia
        Label28.Location = New Point(25, 228)
        Label28.Margin = New Padding(5, 0, 5, 0)
        Label28.Name = "Label28"
        Label28.Size = New Size(145, 25)
        Label28.TabIndex = 27
        Label28.Text = "Address 1"
        ' 
        ' txtSSN
        ' 
        txtSSN.Location = New Point(524, 156)
        txtSSN.Margin = New Padding(5, 6, 5, 6)
        txtSSN.Mask = "000-00-0000"
        txtSSN.Name = "txtSSN"
        txtSSN.Size = New Size(120, 31)
        txtSSN.TabIndex = 8
        ' 
        ' txtPatCity
        ' 
        txtPatCity.BackColor = Color.Ivory
        txtPatCity.Location = New Point(369, 258)
        txtPatCity.Margin = New Padding(5, 6, 5, 6)
        txtPatCity.MaxLength = 35
        txtPatCity.Name = "txtPatCity"
        txtPatCity.Size = New Size(154, 31)
        txtPatCity.TabIndex = 12
        ' 
        ' Label31
        ' 
        Label31.Location = New Point(716, 28)
        Label31.Margin = New Padding(5, 0, 5, 0)
        Label31.Name = "Label31"
        Label31.Size = New Size(120, 25)
        Label31.TabIndex = 34
        Label31.Text = "Middle Name"
        ' 
        ' Label27
        ' 
        Label27.ForeColor = Color.Fuchsia
        Label27.Location = New Point(375, 228)
        Label27.Margin = New Padding(5, 0, 5, 0)
        Label27.Name = "Label27"
        Label27.Size = New Size(116, 25)
        Label27.TabIndex = 29
        Label27.Text = "City"
        ' 
        ' txtMName
        ' 
        txtMName.BackColor = Color.White
        txtMName.Location = New Point(715, 61)
        txtMName.Margin = New Padding(5, 6, 5, 6)
        txtMName.MaxLength = 35
        txtMName.Name = "txtMName"
        txtMName.Size = New Size(138, 31)
        txtMName.TabIndex = 5
        ' 
        ' txtPatEmail
        ' 
        txtPatEmail.BackColor = Color.White
        txtPatEmail.Location = New Point(656, 156)
        txtPatEmail.Margin = New Padding(5, 6, 5, 6)
        txtPatEmail.MaxLength = 50
        txtPatEmail.Name = "txtPatEmail"
        txtPatEmail.Size = New Size(195, 31)
        txtPatEmail.TabIndex = 9
        ' 
        ' txtFName
        ' 
        txtFName.BackColor = Color.Ivory
        txtFName.Location = New Point(504, 61)
        txtFName.Margin = New Padding(5, 6, 5, 6)
        txtFName.MaxLength = 35
        txtFName.Name = "txtFName"
        txtFName.Size = New Size(190, 31)
        txtFName.TabIndex = 4
        ' 
        ' Label26
        ' 
        Label26.Location = New Point(651, 125)
        Label26.Margin = New Padding(5, 0, 5, 0)
        Label26.Name = "Label26"
        Label26.Size = New Size(190, 25)
        Label26.TabIndex = 31
        Label26.Text = "Email"
        ' 
        ' gbVeterinary
        ' 
        gbVeterinary.Controls.Add(Label129)
        gbVeterinary.Controls.Add(cmbBreed)
        gbVeterinary.Controls.Add(lblSpecies)
        gbVeterinary.Controls.Add(cmbSpecies)
        gbVeterinary.Location = New Point(882, 31)
        gbVeterinary.Margin = New Padding(5, 6, 5, 6)
        gbVeterinary.Name = "gbVeterinary"
        gbVeterinary.Padding = New Padding(5, 6, 5, 6)
        gbVeterinary.Size = New Size(411, 394)
        gbVeterinary.TabIndex = 77
        gbVeterinary.TabStop = False
        gbVeterinary.Text = "For Veterinary Use"
        ' 
        ' Label129
        ' 
        Label129.Location = New Point(30, 286)
        Label129.Margin = New Padding(5, 0, 5, 0)
        Label129.Name = "Label129"
        Label129.Size = New Size(130, 25)
        Label129.TabIndex = 39
        Label129.Text = "Breed"
        ' 
        ' cmbBreed
        ' 
        cmbBreed.FormattingEnabled = True
        cmbBreed.Location = New Point(18, 319)
        cmbBreed.Margin = New Padding(5, 6, 5, 6)
        cmbBreed.Name = "cmbBreed"
        cmbBreed.Size = New Size(373, 33)
        cmbBreed.TabIndex = 24
        ' 
        ' lblSpecies
        ' 
        lblSpecies.ForeColor = Color.Red
        lblSpecies.Location = New Point(24, 36)
        lblSpecies.Margin = New Padding(5, 0, 5, 0)
        lblSpecies.Name = "lblSpecies"
        lblSpecies.Size = New Size(145, 25)
        lblSpecies.TabIndex = 23
        lblSpecies.Text = "Species"
        ' 
        ' cmbSpecies
        ' 
        cmbSpecies.FormattingEnabled = True
        cmbSpecies.Location = New Point(16, 72)
        cmbSpecies.Margin = New Padding(5, 6, 5, 6)
        cmbSpecies.Name = "cmbSpecies"
        cmbSpecies.Size = New Size(380, 33)
        cmbSpecies.TabIndex = 0
        ' 
        ' tpOrders
        ' 
        tpOrders.Controls.Add(chkCare)
        tpOrders.Controls.Add(chkHomeBound)
        tpOrders.Controls.Add(chkPhlebotomy)
        tpOrders.Controls.Add(chkVerbal)
        tpOrders.Controls.Add(chkProfile)
        tpOrders.Controls.Add(dgvTGPMarked)
        tpOrders.Controls.Add(Label38)
        tpOrders.ForeColor = Color.DarkBlue
        tpOrders.Location = New Point(4, 34)
        tpOrders.Margin = New Padding(5, 6, 5, 6)
        tpOrders.Name = "tpOrders"
        tpOrders.Padding = New Padding(5, 6, 5, 6)
        tpOrders.Size = New Size(1342, 571)
        tpOrders.TabIndex = 4
        tpOrders.Text = "Orders"
        tpOrders.UseVisualStyleBackColor = True
        ' 
        ' chkCare
        ' 
        chkCare.AutoSize = True
        chkCare.CheckAlign = ContentAlignment.MiddleRight
        chkCare.Location = New Point(725, 17)
        chkCare.Margin = New Padding(5, 6, 5, 6)
        chkCare.Name = "chkCare"
        chkCare.Size = New Size(155, 29)
        chkCare.TabIndex = 14
        chkCare.Text = "Care Entity Call"
        chkCare.UseVisualStyleBackColor = True
        ' 
        ' chkHomeBound
        ' 
        chkHomeBound.AutoSize = True
        chkHomeBound.CheckAlign = ContentAlignment.MiddleRight
        chkHomeBound.Location = New Point(538, 17)
        chkHomeBound.Margin = New Padding(5, 6, 5, 6)
        chkHomeBound.Name = "chkHomeBound"
        chkHomeBound.Size = New Size(122, 29)
        chkHomeBound.TabIndex = 13
        chkHomeBound.Text = "House Call"
        chkHomeBound.UseVisualStyleBackColor = True
        ' 
        ' chkPhlebotomy
        ' 
        chkPhlebotomy.AutoSize = True
        chkPhlebotomy.CheckAlign = ContentAlignment.MiddleRight
        chkPhlebotomy.Location = New Point(360, 17)
        chkPhlebotomy.Margin = New Padding(5, 6, 5, 6)
        chkPhlebotomy.Name = "chkPhlebotomy"
        chkPhlebotomy.Size = New Size(135, 29)
        chkPhlebotomy.TabIndex = 12
        chkPhlebotomy.Text = "Phlebotomy"
        chkPhlebotomy.UseVisualStyleBackColor = True
        ' 
        ' chkVerbal
        ' 
        chkVerbal.AutoSize = True
        chkVerbal.CheckAlign = ContentAlignment.MiddleRight
        chkVerbal.Location = New Point(1102, 17)
        chkVerbal.Margin = New Padding(5, 6, 5, 6)
        chkVerbal.Name = "chkVerbal"
        chkVerbal.Size = New Size(95, 29)
        chkVerbal.TabIndex = 11
        chkVerbal.Text = "Verbal?"
        chkVerbal.UseVisualStyleBackColor = True
        ' 
        ' chkProfile
        ' 
        chkProfile.Appearance = Appearance.Button
        chkProfile.Location = New Point(918, 9)
        chkProfile.Margin = New Padding(5, 6, 5, 6)
        chkProfile.Name = "chkProfile"
        chkProfile.Size = New Size(174, 44)
        chkProfile.TabIndex = 10
        chkProfile.Text = "Profile Integral"
        chkProfile.TextAlign = ContentAlignment.MiddleCenter
        chkProfile.UseVisualStyleBackColor = True
        ' 
        ' dgvTGPMarked
        ' 
        dgvTGPMarked.AllowUserToAddRows = False
        dgvTGPMarked.AllowUserToDeleteRows = False
        DataGridViewCellStyle7.BackColor = Color.Honeydew
        dgvTGPMarked.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle7
        DataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = SystemColors.Control
        DataGridViewCellStyle8.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle8.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = DataGridViewTriState.True
        dgvTGPMarked.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        dgvTGPMarked.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvTGPMarked.Columns.AddRange(New DataGridViewColumn() {CompID, CompLook, TGP, CompType, Stat, Verbal, Outsource})
        DataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = SystemColors.Window
        DataGridViewCellStyle9.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle9.ForeColor = Color.DarkBlue
        DataGridViewCellStyle9.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = DataGridViewTriState.False
        dgvTGPMarked.DefaultCellStyle = DataGridViewCellStyle9
        dgvTGPMarked.Location = New Point(26, 61)
        dgvTGPMarked.Margin = New Padding(5, 6, 5, 6)
        dgvTGPMarked.MultiSelect = False
        dgvTGPMarked.Name = "dgvTGPMarked"
        DataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = SystemColors.Control
        DataGridViewCellStyle10.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle10.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = DataGridViewTriState.True
        dgvTGPMarked.RowHeadersDefaultCellStyle = DataGridViewCellStyle10
        dgvTGPMarked.RowHeadersVisible = False
        dgvTGPMarked.RowHeadersWidth = 56
        dgvTGPMarked.SelectionMode = DataGridViewSelectionMode.CellSelect
        dgvTGPMarked.Size = New Size(1262, 456)
        dgvTGPMarked.TabIndex = 9
        ' 
        ' CompID
        ' 
        CompID.FillWeight = 90F
        CompID.HeaderText = "ID"
        CompID.MaxInputLength = 8
        CompID.MinimumWidth = 7
        CompID.Name = "CompID"
        CompID.SortMode = DataGridViewColumnSortMode.NotSortable
        CompID.ToolTipText = "Component numeric ID"
        CompID.Width = 90
        ' 
        ' CompLook
        ' 
        CompLook.FillWeight = 70F
        CompLook.HeaderText = "Look up"
        CompLook.Image = CType(resources.GetObject("CompLook.Image"), Image)
        CompLook.MinimumWidth = 7
        CompLook.Name = "CompLook"
        CompLook.Width = 70
        ' 
        ' TGP
        ' 
        TGP.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        TGP.FillWeight = 360F
        TGP.HeaderText = "T. G. P."
        TGP.MinimumWidth = 7
        TGP.Name = "TGP"
        TGP.ReadOnly = True
        TGP.SortMode = DataGridViewColumnSortMode.NotSortable
        TGP.ToolTipText = "Analyte, Group or Profile"
        ' 
        ' CompType
        ' 
        CompType.FillWeight = 55F
        CompType.HeaderText = "Type"
        CompType.Image = CType(resources.GetObject("CompType.Image"), Image)
        CompType.MinimumWidth = 7
        CompType.Name = "CompType"
        CompType.ReadOnly = True
        CompType.Width = 55
        ' 
        ' Stat
        ' 
        Stat.FalseValue = "0"
        Stat.FillWeight = 50F
        Stat.HeaderText = "Stat?"
        Stat.MinimumWidth = 7
        Stat.Name = "Stat"
        Stat.Resizable = DataGridViewTriState.False
        Stat.TrueValue = "1"
        Stat.Width = 50
        ' 
        ' Verbal
        ' 
        Verbal.FillWeight = 50F
        Verbal.HeaderText = "Verbal?"
        Verbal.MinimumWidth = 7
        Verbal.Name = "Verbal"
        Verbal.Resizable = DataGridViewTriState.False
        Verbal.Width = 50
        ' 
        ' Outsource
        ' 
        Outsource.FillWeight = 50F
        Outsource.HeaderText = "OS?"
        Outsource.MinimumWidth = 7
        Outsource.Name = "Outsource"
        Outsource.Width = 50
        ' 
        ' Label38
        ' 
        Label38.Location = New Point(26, 19)
        Label38.Margin = New Padding(5, 0, 5, 0)
        Label38.Name = "Label38"
        Label38.Size = New Size(285, 25)
        Label38.TabIndex = 8
        Label38.Text = "Marked Components"
        ' 
        ' tpReports
        ' 
        tpReports.Controls.Add(gbReports)
        tpReports.ForeColor = Color.DarkBlue
        tpReports.Location = New Point(4, 34)
        tpReports.Margin = New Padding(5, 6, 5, 6)
        tpReports.Name = "tpReports"
        tpReports.Padding = New Padding(5, 6, 5, 6)
        tpReports.Size = New Size(1342, 571)
        tpReports.TabIndex = 5
        tpReports.Text = "Reports"
        tpReports.UseVisualStyleBackColor = True
        ' 
        ' gbReports
        ' 
        gbReports.Controls.Add(txtRPTFax)
        gbReports.Controls.Add(chkInterface)
        gbReports.Controls.Add(chkProlison)
        gbReports.Controls.Add(Label42)
        gbReports.Controls.Add(Label41)
        gbReports.Controls.Add(chkRDMAuto)
        gbReports.Controls.Add(Label47)
        gbReports.Controls.Add(Label46)
        gbReports.Controls.Add(Label45)
        gbReports.Controls.Add(btnRptAdd)
        gbReports.Controls.Add(txtRptEmail)
        gbReports.Controls.Add(Label44)
        gbReports.Controls.Add(chkRptFax)
        gbReports.Controls.Add(Label43)
        gbReports.Controls.Add(chkrptEmail)
        gbReports.Controls.Add(chkPrint)
        gbReports.Controls.Add(btnRefProf)
        gbReports.Controls.Add(txtRptRcptName)
        gbReports.Controls.Add(Label40)
        gbReports.Controls.Add(btnRPTLookUp)
        gbReports.Controls.Add(chkRptComplete)
        gbReports.Controls.Add(txtRptRcptID)
        gbReports.Controls.Add(Label39)
        gbReports.Controls.Add(btnRemRpt)
        gbReports.Controls.Add(btnRemRptAll)
        gbReports.Controls.Add(dgvRptProviders)
        gbReports.Location = New Point(15, 11)
        gbReports.Margin = New Padding(5, 6, 5, 6)
        gbReports.Name = "gbReports"
        gbReports.Padding = New Padding(5, 6, 5, 6)
        gbReports.Size = New Size(1300, 536)
        gbReports.TabIndex = 0
        gbReports.TabStop = False
        ' 
        ' txtRPTFax
        ' 
        txtRPTFax.Location = New Point(902, 469)
        txtRPTFax.Margin = New Padding(5, 6, 5, 6)
        txtRPTFax.Name = "txtRPTFax"
        txtRPTFax.Size = New Size(204, 31)
        txtRPTFax.TabIndex = 40
        ' 
        ' chkInterface
        ' 
        chkInterface.CheckAlign = ContentAlignment.MiddleRight
        chkInterface.Location = New Point(29, 464)
        chkInterface.Margin = New Padding(5, 6, 5, 6)
        chkInterface.Name = "chkInterface"
        chkInterface.Size = New Size(206, 41)
        chkInterface.TabIndex = 53
        chkInterface.Text = "HL7/PDF Interface"
        chkInterface.UseVisualStyleBackColor = True
        ' 
        ' chkProlison
        ' 
        chkProlison.CheckAlign = ContentAlignment.MiddleRight
        chkProlison.Location = New Point(264, 411)
        chkProlison.Margin = New Padding(5, 6, 5, 6)
        chkProlison.Name = "chkProlison"
        chkProlison.Size = New Size(155, 41)
        chkProlison.TabIndex = 52
        chkProlison.Text = "Prolison Print"
        chkProlison.UseVisualStyleBackColor = True
        ' 
        ' Label42
        ' 
        Label42.Location = New Point(916, 278)
        Label42.Margin = New Padding(5, 0, 5, 0)
        Label42.Name = "Label42"
        Label42.Size = New Size(129, 25)
        Label42.TabIndex = 51
        Label42.Text = "Report Status"
        ' 
        ' Label41
        ' 
        Label41.Location = New Point(702, 278)
        Label41.Margin = New Padding(5, 0, 5, 0)
        Label41.Name = "Label41"
        Label41.Size = New Size(142, 25)
        Label41.TabIndex = 50
        Label41.Text = "Report Delivery"
        Label41.TextAlign = ContentAlignment.TopCenter
        ' 
        ' chkRDMAuto
        ' 
        chkRDMAuto.Appearance = Appearance.Button
        chkRDMAuto.CheckAlign = ContentAlignment.MiddleCenter
        chkRDMAuto.Location = New Point(691, 308)
        chkRDMAuto.Margin = New Padding(5, 6, 5, 6)
        chkRDMAuto.Name = "chkRDMAuto"
        chkRDMAuto.Size = New Size(186, 53)
        chkRDMAuto.TabIndex = 49
        chkRDMAuto.Text = "Batch"
        chkRDMAuto.TextAlign = ContentAlignment.MiddleCenter
        chkRDMAuto.UseVisualStyleBackColor = True
        ' 
        ' Label47
        ' 
        Label47.Location = New Point(18, 9)
        Label47.Margin = New Padding(5, 0, 5, 0)
        Label47.Name = "Label47"
        Label47.Size = New Size(238, 25)
        Label47.TabIndex = 48
        Label47.Text = "Report Recipients"
        ' 
        ' Label46
        ' 
        Label46.Location = New Point(775, 475)
        Label46.Margin = New Padding(5, 0, 5, 0)
        Label46.Name = "Label46"
        Label46.Size = New Size(124, 25)
        Label46.TabIndex = 47
        Label46.Text = "Fax Number"
        Label46.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label45
        ' 
        Label45.Location = New Point(316, 475)
        Label45.Margin = New Padding(5, 0, 5, 0)
        Label45.Name = "Label45"
        Label45.Size = New Size(134, 25)
        Label45.TabIndex = 46
        Label45.Text = "Email Address"
        Label45.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' btnRptAdd
        ' 
        btnRptAdd.Location = New Point(1124, 433)
        btnRptAdd.Margin = New Padding(5, 6, 5, 6)
        btnRptAdd.Name = "btnRptAdd"
        btnRptAdd.Size = New Size(150, 75)
        btnRptAdd.TabIndex = 41
        btnRptAdd.Text = "Add to List"
        btnRptAdd.UseVisualStyleBackColor = True
        ' 
        ' txtRptEmail
        ' 
        txtRptEmail.Enabled = False
        txtRptEmail.Location = New Point(458, 469)
        txtRptEmail.Margin = New Padding(5, 6, 5, 6)
        txtRptEmail.MaxLength = 50
        txtRptEmail.Name = "txtRptEmail"
        txtRptEmail.Size = New Size(304, 31)
        txtRptEmail.TabIndex = 36
        ' 
        ' Label44
        ' 
        Label44.Location = New Point(896, 386)
        Label44.Margin = New Padding(5, 0, 5, 0)
        Label44.Name = "Label44"
        Label44.Size = New Size(114, 25)
        Label44.TabIndex = 45
        Label44.Text = "Fax Report"
        Label44.TextAlign = ContentAlignment.TopCenter
        ' 
        ' chkRptFax
        ' 
        chkRptFax.Appearance = Appearance.Button
        chkRptFax.Location = New Point(898, 417)
        chkRptFax.Margin = New Padding(5, 6, 5, 6)
        chkRptFax.Name = "chkRptFax"
        chkRptFax.Size = New Size(109, 47)
        chkRptFax.TabIndex = 38
        chkRptFax.Text = "No"
        chkRptFax.TextAlign = ContentAlignment.MiddleCenter
        chkRptFax.UseVisualStyleBackColor = True
        ' 
        ' Label43
        ' 
        Label43.Location = New Point(454, 386)
        Label43.Margin = New Padding(5, 0, 5, 0)
        Label43.Name = "Label43"
        Label43.Size = New Size(120, 25)
        Label43.TabIndex = 44
        Label43.Text = "Email Report"
        Label43.TextAlign = ContentAlignment.TopCenter
        ' 
        ' chkrptEmail
        ' 
        chkrptEmail.Appearance = Appearance.Button
        chkrptEmail.Location = New Point(458, 417)
        chkrptEmail.Margin = New Padding(5, 6, 5, 6)
        chkrptEmail.Name = "chkrptEmail"
        chkrptEmail.Size = New Size(109, 47)
        chkrptEmail.TabIndex = 35
        chkrptEmail.Text = "No"
        chkrptEmail.TextAlign = ContentAlignment.MiddleCenter
        chkrptEmail.UseVisualStyleBackColor = True
        ' 
        ' chkPrint
        ' 
        chkPrint.CheckAlign = ContentAlignment.MiddleRight
        chkPrint.Checked = True
        chkPrint.CheckState = CheckState.Checked
        chkPrint.Location = New Point(20, 411)
        chkPrint.Margin = New Padding(5, 6, 5, 6)
        chkPrint.Name = "chkPrint"
        chkPrint.Size = New Size(215, 41)
        chkPrint.TabIndex = 33
        chkPrint.Text = "In Lab Print Report"
        chkPrint.TextAlign = ContentAlignment.MiddleRight
        chkPrint.UseVisualStyleBackColor = True
        ' 
        ' btnRefProf
        ' 
        btnRefProf.Location = New Point(1086, 308)
        btnRefProf.Margin = New Padding(5, 6, 5, 6)
        btnRefProf.Name = "btnRefProf"
        btnRefProf.Size = New Size(186, 53)
        btnRefProf.TabIndex = 34
        btnRefProf.TabStop = False
        btnRefProf.Text = "Refresh Profile"
        btnRefProf.UseVisualStyleBackColor = True
        ' 
        ' txtRptRcptName
        ' 
        txtRptRcptName.Location = New Point(234, 316)
        txtRptRcptName.Margin = New Padding(5, 6, 5, 6)
        txtRptRcptName.MaxLength = 12
        txtRptRcptName.Name = "txtRptRcptName"
        txtRptRcptName.ReadOnly = True
        txtRptRcptName.Size = New Size(435, 31)
        txtRptRcptName.TabIndex = 39
        ' 
        ' Label40
        ' 
        Label40.Location = New Point(240, 278)
        Label40.Margin = New Padding(5, 0, 5, 0)
        Label40.Name = "Label40"
        Label40.Size = New Size(138, 25)
        Label40.TabIndex = 37
        Label40.Text = "Provider Name"
        ' 
        ' btnRPTLookUp
        ' 
        btnRPTLookUp.Image = CType(resources.GetObject("btnRPTLookUp.Image"), Image)
        btnRPTLookUp.Location = New Point(171, 306)
        btnRPTLookUp.Margin = New Padding(5, 6, 5, 6)
        btnRPTLookUp.Name = "btnRPTLookUp"
        btnRPTLookUp.Size = New Size(51, 53)
        btnRPTLookUp.TabIndex = 30
        btnRPTLookUp.TabStop = False
        btnRPTLookUp.UseVisualStyleBackColor = True
        ' 
        ' chkRptComplete
        ' 
        chkRptComplete.Appearance = Appearance.Button
        chkRptComplete.CheckAlign = ContentAlignment.MiddleRight
        chkRptComplete.Checked = True
        chkRptComplete.CheckState = CheckState.Checked
        chkRptComplete.Location = New Point(889, 308)
        chkRptComplete.Margin = New Padding(5, 6, 5, 6)
        chkRptComplete.Name = "chkRptComplete"
        chkRptComplete.Size = New Size(186, 53)
        chkRptComplete.TabIndex = 32
        chkRptComplete.Text = "Complete"
        chkRptComplete.TextAlign = ContentAlignment.MiddleCenter
        chkRptComplete.UseVisualStyleBackColor = True
        ' 
        ' txtRptRcptID
        ' 
        txtRptRcptID.Location = New Point(24, 316)
        txtRptRcptID.Margin = New Padding(5, 6, 5, 6)
        txtRptRcptID.MaxLength = 12
        txtRptRcptID.Name = "txtRptRcptID"
        txtRptRcptID.Size = New Size(135, 31)
        txtRptRcptID.TabIndex = 29
        txtRptRcptID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label39
        ' 
        Label39.Location = New Point(24, 278)
        Label39.Margin = New Padding(5, 0, 5, 0)
        Label39.Name = "Label39"
        Label39.Size = New Size(111, 25)
        Label39.TabIndex = 31
        Label39.Text = "Provider ID"
        ' 
        ' btnRemRpt
        ' 
        btnRemRpt.Enabled = False
        btnRemRpt.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnRemRpt.ForeColor = Color.Red
        btnRemRpt.Location = New Point(1194, 41)
        btnRemRpt.Margin = New Padding(5, 6, 5, 6)
        btnRemRpt.Name = "btnRemRpt"
        btnRemRpt.Size = New Size(80, 58)
        btnRemRpt.TabIndex = 43
        btnRemRpt.TabStop = False
        btnRemRpt.Text = "X"
        btnRemRpt.UseVisualStyleBackColor = True
        ' 
        ' btnRemRptAll
        ' 
        btnRemRptAll.Enabled = False
        btnRemRptAll.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnRemRptAll.ForeColor = Color.Red
        btnRemRptAll.Location = New Point(1194, 203)
        btnRemRptAll.Margin = New Padding(5, 6, 5, 6)
        btnRemRptAll.Name = "btnRemRptAll"
        btnRemRptAll.Size = New Size(80, 58)
        btnRemRptAll.TabIndex = 42
        btnRemRptAll.TabStop = False
        btnRemRptAll.Text = "XXX"
        btnRemRptAll.UseVisualStyleBackColor = True
        ' 
        ' dgvRptProviders
        ' 
        dgvRptProviders.AllowUserToAddRows = False
        dgvRptProviders.AllowUserToDeleteRows = False
        DataGridViewCellStyle11.BackColor = Color.LavenderBlush
        dgvRptProviders.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle11
        DataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = SystemColors.Control
        DataGridViewCellStyle12.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle12.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle12.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = DataGridViewTriState.True
        dgvRptProviders.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle12
        dgvRptProviders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvRptProviders.Columns.AddRange(New DataGridViewColumn() {Prov_ID, Prov_Name, RDMAuto, RCO, Print, Prolison, RDMInterface, chkFax, Fax, chkEmail, Email})
        DataGridViewCellStyle13.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle13.BackColor = SystemColors.Window
        DataGridViewCellStyle13.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle13.ForeColor = Color.DarkBlue
        DataGridViewCellStyle13.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle13.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle13.WrapMode = DataGridViewTriState.False
        dgvRptProviders.DefaultCellStyle = DataGridViewCellStyle13
        dgvRptProviders.Location = New Point(20, 41)
        dgvRptProviders.Margin = New Padding(5, 6, 5, 6)
        dgvRptProviders.Name = "dgvRptProviders"
        dgvRptProviders.ReadOnly = True
        DataGridViewCellStyle14.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle14.BackColor = SystemColors.Control
        DataGridViewCellStyle14.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle14.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle14.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle14.WrapMode = DataGridViewTriState.True
        dgvRptProviders.RowHeadersDefaultCellStyle = DataGridViewCellStyle14
        dgvRptProviders.RowHeadersVisible = False
        dgvRptProviders.RowHeadersWidth = 56
        dgvRptProviders.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvRptProviders.Size = New Size(1151, 222)
        dgvRptProviders.TabIndex = 28
        ' 
        ' Prov_ID
        ' 
        Prov_ID.FillWeight = 60F
        Prov_ID.HeaderText = "ProvID"
        Prov_ID.MinimumWidth = 7
        Prov_ID.Name = "Prov_ID"
        Prov_ID.ReadOnly = True
        Prov_ID.Width = 60
        ' 
        ' Prov_Name
        ' 
        Prov_Name.FillWeight = 96F
        Prov_Name.HeaderText = "Prov Name"
        Prov_Name.MinimumWidth = 7
        Prov_Name.Name = "Prov_Name"
        Prov_Name.ReadOnly = True
        Prov_Name.Width = 96
        ' 
        ' RDMAuto
        ' 
        RDMAuto.FillWeight = 36F
        RDMAuto.HeaderText = "Auto"
        RDMAuto.MinimumWidth = 7
        RDMAuto.Name = "RDMAuto"
        RDMAuto.ReadOnly = True
        RDMAuto.Width = 36
        ' 
        ' RCO
        ' 
        RCO.FillWeight = 36F
        RCO.HeaderText = "RCO"
        RCO.MinimumWidth = 7
        RCO.Name = "RCO"
        RCO.ReadOnly = True
        RCO.Width = 36
        ' 
        ' Print
        ' 
        Print.FillWeight = 36F
        Print.HeaderText = "Print"
        Print.MinimumWidth = 7
        Print.Name = "Print"
        Print.ReadOnly = True
        Print.Width = 36
        ' 
        ' Prolison
        ' 
        Prolison.FillWeight = 45F
        Prolison.HeaderText = "Prolison"
        Prolison.MinimumWidth = 7
        Prolison.Name = "Prolison"
        Prolison.ReadOnly = True
        Prolison.Width = 45
        ' 
        ' RDMInterface
        ' 
        RDMInterface.FillWeight = 45F
        RDMInterface.HeaderText = "Interface"
        RDMInterface.MinimumWidth = 7
        RDMInterface.Name = "RDMInterface"
        RDMInterface.ReadOnly = True
        RDMInterface.Width = 45
        ' 
        ' chkFax
        ' 
        chkFax.FillWeight = 36F
        chkFax.HeaderText = "Fax"
        chkFax.MinimumWidth = 7
        chkFax.Name = "chkFax"
        chkFax.ReadOnly = True
        chkFax.Width = 36
        ' 
        ' Fax
        ' 
        Fax.FillWeight = 110F
        Fax.HeaderText = "Fax No"
        Fax.MinimumWidth = 7
        Fax.Name = "Fax"
        Fax.ReadOnly = True
        Fax.Width = 110
        ' 
        ' chkEmail
        ' 
        chkEmail.FillWeight = 36F
        chkEmail.HeaderText = "Email?"
        chkEmail.MinimumWidth = 7
        chkEmail.Name = "chkEmail"
        chkEmail.ReadOnly = True
        chkEmail.Width = 36
        ' 
        ' Email
        ' 
        Email.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Email.HeaderText = "Email Address"
        Email.MinimumWidth = 7
        Email.Name = "Email"
        Email.ReadOnly = True
        ' 
        ' tpBilling
        ' 
        tpBilling.AutoScroll = True
        tpBilling.Controls.Add(rbP)
        tpBilling.Controls.Add(rbT)
        tpBilling.Controls.Add(rbC)
        tpBilling.Controls.Add(btnSwitchCarriers)
        tpBilling.Controls.Add(Label101)
        tpBilling.Controls.Add(txtPayment)
        tpBilling.Controls.Add(btnCalculate)
        tpBilling.Controls.Add(TabControl2)
        tpBilling.Controls.Add(Label50)
        tpBilling.Controls.Add(Label49)
        tpBilling.Controls.Add(Label48)
        tpBilling.Controls.Add(btnPmnt)
        tpBilling.Controls.Add(txtCopay)
        tpBilling.Controls.Add(chkSvcGratis)
        tpBilling.ForeColor = Color.DarkBlue
        tpBilling.Location = New Point(4, 34)
        tpBilling.Margin = New Padding(5, 6, 5, 6)
        tpBilling.Name = "tpBilling"
        tpBilling.Padding = New Padding(5, 6, 5, 6)
        tpBilling.Size = New Size(1342, 571)
        tpBilling.TabIndex = 3
        tpBilling.Text = "Billing/Payment"
        tpBilling.UseVisualStyleBackColor = True
        ' 
        ' rbP
        ' 
        rbP.Location = New Point(360, 47)
        rbP.Margin = New Padding(5, 6, 5, 6)
        rbP.Name = "rbP"
        rbP.Size = New Size(98, 33)
        rbP.TabIndex = 4
        rbP.Text = "Patient"
        rbP.UseVisualStyleBackColor = True
        ' 
        ' rbT
        ' 
        rbT.Location = New Point(270, 47)
        rbT.Margin = New Padding(5, 6, 5, 6)
        rbT.Name = "rbT"
        rbT.Size = New Size(75, 33)
        rbT.TabIndex = 3
        rbT.Text = "TP"
        rbT.UseVisualStyleBackColor = True
        ' 
        ' rbC
        ' 
        rbC.Checked = True
        rbC.Location = New Point(158, 47)
        rbC.Margin = New Padding(5, 6, 5, 6)
        rbC.Name = "rbC"
        rbC.Size = New Size(102, 33)
        rbC.TabIndex = 2
        rbC.TabStop = True
        rbC.Text = "Client"
        rbC.UseVisualStyleBackColor = True
        ' 
        ' btnSwitchCarriers
        ' 
        btnSwitchCarriers.Location = New Point(1105, 33)
        btnSwitchCarriers.Margin = New Padding(5, 6, 5, 6)
        btnSwitchCarriers.Name = "btnSwitchCarriers"
        btnSwitchCarriers.Size = New Size(160, 64)
        btnSwitchCarriers.TabIndex = 7
        btnSwitchCarriers.Text = "Switch Carriers"
        btnSwitchCarriers.UseVisualStyleBackColor = True
        ' 
        ' Label101
        ' 
        Label101.Location = New Point(898, 11)
        Label101.Margin = New Padding(5, 0, 5, 0)
        Label101.Name = "Label101"
        Label101.Size = New Size(138, 28)
        Label101.TabIndex = 10
        Label101.Text = "Paid Amount"
        Label101.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtPayment
        ' 
        txtPayment.Location = New Point(904, 47)
        txtPayment.Margin = New Padding(5, 6, 5, 6)
        txtPayment.MaxLength = 9
        txtPayment.Name = "txtPayment"
        txtPayment.ReadOnly = True
        txtPayment.Size = New Size(146, 31)
        txtPayment.TabIndex = 6
        txtPayment.TextAlign = HorizontalAlignment.Center
        ' 
        ' btnCalculate
        ' 
        btnCalculate.Location = New Point(469, 41)
        btnCalculate.Margin = New Padding(5, 6, 5, 6)
        btnCalculate.Name = "btnCalculate"
        btnCalculate.Size = New Size(114, 47)
        btnCalculate.TabIndex = 5
        btnCalculate.TabStop = False
        btnCalculate.Text = "Calculate"
        btnCalculate.UseVisualStyleBackColor = True
        ' 
        ' TabControl2
        ' 
        TabControl2.Controls.Add(tpPrimary)
        TabControl2.Controls.Add(tpSecondary)
        TabControl2.Controls.Add(tpGuarantor)
        TabControl2.Location = New Point(14, 133)
        TabControl2.Margin = New Padding(5, 6, 5, 6)
        TabControl2.Name = "TabControl2"
        TabControl2.SelectedIndex = 0
        TabControl2.Size = New Size(1258, 584)
        TabControl2.SizeMode = TabSizeMode.FillToRight
        TabControl2.TabIndex = 7
        ' 
        ' tpPrimary
        ' 
        tpPrimary.AutoScroll = True
        tpPrimary.Controls.Add(grpPSubs)
        tpPrimary.Controls.Add(grpPrimary)
        tpPrimary.Location = New Point(4, 34)
        tpPrimary.Margin = New Padding(5, 6, 5, 6)
        tpPrimary.Name = "tpPrimary"
        tpPrimary.Padding = New Padding(5, 6, 5, 6)
        tpPrimary.Size = New Size(1250, 546)
        tpPrimary.TabIndex = 0
        tpPrimary.Text = "Primary Insurance"
        tpPrimary.UseVisualStyleBackColor = True
        ' 
        ' grpPSubs
        ' 
        grpPSubs.Controls.Add(txtPsubWPhone)
        grpPSubs.Controls.Add(Label120)
        grpPSubs.Controls.Add(txtPsubHPhone)
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
        grpPSubs.Location = New Point(15, 283)
        grpPSubs.Margin = New Padding(5, 6, 5, 6)
        grpPSubs.Name = "grpPSubs"
        grpPSubs.Padding = New Padding(5, 6, 5, 6)
        grpPSubs.Size = New Size(1220, 217)
        grpPSubs.TabIndex = 29
        grpPSubs.TabStop = False
        grpPSubs.Text = "Primary Subscriber"
        ' 
        ' txtPsubWPhone
        ' 
        txtPsubWPhone.Location = New Point(1065, 147)
        txtPsubWPhone.Margin = New Padding(5, 6, 5, 6)
        txtPsubWPhone.Name = "txtPsubWPhone"
        txtPsubWPhone.Size = New Size(133, 31)
        txtPsubWPhone.TabIndex = 94
        ' 
        ' Label120
        ' 
        Label120.ForeColor = Color.DarkBlue
        Label120.Location = New Point(1062, 116)
        Label120.Margin = New Padding(5, 0, 5, 0)
        Label120.Name = "Label120"
        Label120.Size = New Size(142, 25)
        Label120.TabIndex = 95
        Label120.Text = "Work Phone"
        ' 
        ' txtPsubHPhone
        ' 
        txtPsubHPhone.Location = New Point(916, 147)
        txtPsubHPhone.Margin = New Padding(5, 6, 5, 6)
        txtPsubHPhone.Name = "txtPsubHPhone"
        txtPsubHPhone.Size = New Size(135, 31)
        txtPsubHPhone.TabIndex = 82
        ' 
        ' txtPSubLName
        ' 
        txtPSubLName.AcceptsReturn = True
        txtPSubLName.Location = New Point(200, 61)
        txtPSubLName.Margin = New Padding(5, 6, 5, 6)
        txtPSubLName.Name = "txtPSubLName"
        txtPSubLName.Size = New Size(142, 31)
        txtPSubLName.TabIndex = 80
        ' 
        ' Label57
        ' 
        Label57.ForeColor = Color.DarkBlue
        Label57.Location = New Point(916, 33)
        Label57.Margin = New Padding(5, 0, 5, 0)
        Label57.Name = "Label57"
        Label57.Size = New Size(100, 25)
        Label57.TabIndex = 93
        Label57.Text = "SSN"
        ' 
        ' txtPSubSSN
        ' 
        txtPSubSSN.Location = New Point(916, 61)
        txtPSubSSN.Margin = New Padding(5, 6, 5, 6)
        txtPSubSSN.Mask = "000-00-0000"
        txtPSubSSN.Name = "txtPSubSSN"
        txtPSubSSN.Size = New Size(135, 31)
        txtPSubSSN.TabIndex = 85
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
        txtPSubDOB.Location = New Point(770, 61)
        txtPSubDOB.Margin = New Padding(5, 6, 5, 6)
        txtPSubDOB.Mask = "00/00/0000"
        txtPSubDOB.Name = "txtPSubDOB"
        txtPSubDOB.Size = New Size(128, 31)
        txtPSubDOB.TabIndex = 84
        txtPSubDOB.ValidatingType = GetType(Date)
        ' 
        ' cmbPSubSex
        ' 
        cmbPSubSex.DropDownStyle = ComboBoxStyle.DropDownList
        cmbPSubSex.FormattingEnabled = True
        cmbPSubSex.Items.AddRange(New Object() {"Female", "Male", "Indetermined", "Unreported"})
        cmbPSubSex.Location = New Point(644, 61)
        cmbPSubSex.Margin = New Padding(5, 6, 5, 6)
        cmbPSubSex.Name = "cmbPSubSex"
        cmbPSubSex.Size = New Size(109, 33)
        cmbPSubSex.TabIndex = 83
        ' 
        ' btnPSubLook
        ' 
        btnPSubLook.Enabled = False
        btnPSubLook.Image = CType(resources.GetObject("btnPSubLook.Image"), Image)
        btnPSubLook.Location = New Point(146, 53)
        btnPSubLook.Margin = New Padding(5, 6, 5, 6)
        btnPSubLook.Name = "btnPSubLook"
        btnPSubLook.Size = New Size(44, 50)
        btnPSubLook.TabIndex = 79
        btnPSubLook.TabStop = False
        btnPSubLook.UseVisualStyleBackColor = True
        ' 
        ' Label60
        ' 
        Label60.ForeColor = Color.DarkBlue
        Label60.Location = New Point(778, 116)
        Label60.Margin = New Padding(5, 0, 5, 0)
        Label60.Name = "Label60"
        Label60.Size = New Size(116, 25)
        Label60.TabIndex = 86
        Label60.Text = "Country"
        ' 
        ' txtPSubCountry
        ' 
        txtPSubCountry.AcceptsReturn = True
        txtPSubCountry.Location = New Point(770, 147)
        txtPSubCountry.Margin = New Padding(5, 6, 5, 6)
        txtPSubCountry.MaxLength = 35
        txtPSubCountry.Name = "txtPSubCountry"
        txtPSubCountry.Size = New Size(128, 31)
        txtPSubCountry.TabIndex = 91
        ' 
        ' Label61
        ' 
        Label61.ForeColor = Color.DarkBlue
        Label61.Location = New Point(1060, 31)
        Label61.Margin = New Padding(5, 0, 5, 0)
        Label61.Name = "Label61"
        Label61.Size = New Size(111, 25)
        Label61.TabIndex = 85
        Label61.Text = "Email Address"
        ' 
        ' txtPSubEmail
        ' 
        txtPSubEmail.AcceptsReturn = True
        txtPSubEmail.Location = New Point(1065, 61)
        txtPSubEmail.Margin = New Padding(5, 6, 5, 6)
        txtPSubEmail.MaxLength = 25
        txtPSubEmail.Name = "txtPSubEmail"
        txtPSubEmail.Size = New Size(133, 31)
        txtPSubEmail.TabIndex = 93
        ' 
        ' Label62
        ' 
        Label62.ForeColor = Color.DarkBlue
        Label62.Location = New Point(914, 116)
        Label62.Margin = New Padding(5, 0, 5, 0)
        Label62.Name = "Label62"
        Label62.Size = New Size(142, 25)
        Label62.TabIndex = 84
        Label62.Text = "Home Phone"
        ' 
        ' Label63
        ' 
        Label63.ForeColor = Color.Fuchsia
        Label63.Location = New Point(666, 116)
        Label63.Margin = New Padding(5, 0, 5, 0)
        Label63.Name = "Label63"
        Label63.Size = New Size(66, 25)
        Label63.TabIndex = 83
        Label63.Text = "Zip"
        ' 
        ' txtPSubZip
        ' 
        txtPSubZip.AcceptsReturn = True
        txtPSubZip.Location = New Point(649, 147)
        txtPSubZip.Margin = New Padding(5, 6, 5, 6)
        txtPSubZip.MaxLength = 25
        txtPSubZip.Name = "txtPSubZip"
        txtPSubZip.Size = New Size(104, 31)
        txtPSubZip.TabIndex = 90
        ' 
        ' Label64
        ' 
        Label64.ForeColor = Color.Fuchsia
        Label64.Location = New Point(520, 116)
        Label64.Margin = New Padding(5, 0, 5, 0)
        Label64.Name = "Label64"
        Label64.Size = New Size(136, 25)
        Label64.TabIndex = 82
        Label64.Text = "State/Province"
        ' 
        ' txtPSubState
        ' 
        txtPSubState.AcceptsReturn = True
        txtPSubState.Location = New Point(524, 147)
        txtPSubState.Margin = New Padding(5, 6, 5, 6)
        txtPSubState.MaxLength = 35
        txtPSubState.Name = "txtPSubState"
        txtPSubState.Size = New Size(110, 31)
        txtPSubState.TabIndex = 89
        ' 
        ' Label65
        ' 
        Label65.ForeColor = Color.Fuchsia
        Label65.Location = New Point(375, 116)
        Label65.Margin = New Padding(5, 0, 5, 0)
        Label65.Name = "Label65"
        Label65.Size = New Size(85, 25)
        Label65.TabIndex = 81
        Label65.Text = "City"
        ' 
        ' txtPSubCity
        ' 
        txtPSubCity.AcceptsReturn = True
        txtPSubCity.Location = New Point(362, 147)
        txtPSubCity.Margin = New Padding(5, 6, 5, 6)
        txtPSubCity.MaxLength = 35
        txtPSubCity.Name = "txtPSubCity"
        txtPSubCity.Size = New Size(149, 31)
        txtPSubCity.TabIndex = 88
        ' 
        ' Label66
        ' 
        Label66.ForeColor = Color.DarkBlue
        Label66.Location = New Point(215, 116)
        Label66.Margin = New Padding(5, 0, 5, 0)
        Label66.Name = "Label66"
        Label66.Size = New Size(136, 25)
        Label66.TabIndex = 80
        Label66.Text = "Address Line 2"
        ' 
        ' txtPSubAdd2
        ' 
        txtPSubAdd2.AcceptsReturn = True
        txtPSubAdd2.Location = New Point(200, 147)
        txtPSubAdd2.Margin = New Padding(5, 6, 5, 6)
        txtPSubAdd2.MaxLength = 35
        txtPSubAdd2.Name = "txtPSubAdd2"
        txtPSubAdd2.Size = New Size(149, 31)
        txtPSubAdd2.TabIndex = 87
        ' 
        ' Label67
        ' 
        Label67.ForeColor = Color.Fuchsia
        Label67.Location = New Point(16, 116)
        Label67.Margin = New Padding(5, 0, 5, 0)
        Label67.Name = "Label67"
        Label67.Size = New Size(144, 25)
        Label67.TabIndex = 78
        Label67.Text = "Address Line 1"
        ' 
        ' txtPSubAdd1
        ' 
        txtPSubAdd1.AcceptsReturn = True
        txtPSubAdd1.Location = New Point(10, 147)
        txtPSubAdd1.Margin = New Padding(5, 6, 5, 6)
        txtPSubAdd1.MaxLength = 35
        txtPSubAdd1.Name = "txtPSubAdd1"
        txtPSubAdd1.Size = New Size(178, 31)
        txtPSubAdd1.TabIndex = 86
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
        txtPSubMName.Location = New Point(524, 61)
        txtPSubMName.Margin = New Padding(5, 6, 5, 6)
        txtPSubMName.MaxLength = 35
        txtPSubMName.Name = "txtPSubMName"
        txtPSubMName.Size = New Size(108, 31)
        txtPSubMName.TabIndex = 82
        ' 
        ' Label70
        ' 
        Label70.ForeColor = Color.DarkBlue
        Label70.Location = New Point(356, 31)
        Label70.Margin = New Padding(5, 0, 5, 0)
        Label70.Name = "Label70"
        Label70.Size = New Size(130, 25)
        Label70.TabIndex = 69
        Label70.Text = "First Name"
        ' 
        ' txtPSubFName
        ' 
        txtPSubFName.AcceptsReturn = True
        txtPSubFName.Location = New Point(362, 61)
        txtPSubFName.Margin = New Padding(5, 6, 5, 6)
        txtPSubFName.MaxLength = 35
        txtPSubFName.Name = "txtPSubFName"
        txtPSubFName.Size = New Size(149, 31)
        txtPSubFName.TabIndex = 81
        ' 
        ' Label71
        ' 
        Label71.ForeColor = Color.DarkBlue
        Label71.Location = New Point(200, 31)
        Label71.Margin = New Padding(5, 0, 5, 0)
        Label71.Name = "Label71"
        Label71.Size = New Size(146, 25)
        Label71.TabIndex = 67
        Label71.Text = "Last Name"
        ' 
        ' Label72
        ' 
        Label72.ForeColor = Color.Fuchsia
        Label72.Location = New Point(9, 31)
        Label72.Margin = New Padding(5, 0, 5, 0)
        Label72.Name = "Label72"
        Label72.Size = New Size(134, 25)
        Label72.TabIndex = 64
        Label72.Text = "Subscriber ID"
        ' 
        ' txtPSubID
        ' 
        txtPSubID.AcceptsReturn = True
        txtPSubID.Location = New Point(10, 61)
        txtPSubID.Margin = New Padding(5, 6, 5, 6)
        txtPSubID.MaxLength = 12
        txtPSubID.Name = "txtPSubID"
        txtPSubID.ReadOnly = True
        txtPSubID.Size = New Size(124, 31)
        txtPSubID.TabIndex = 78
        txtPSubID.TabStop = False
        txtPSubID.TextAlign = HorizontalAlignment.Center
        ToolTip1.SetToolTip(txtPSubID, "Patient ID of the subsvriber")
        ' 
        ' grpPrimary
        ' 
        grpPrimary.Controls.Add(Label123)
        grpPrimary.Controls.Add(Label122)
        grpPrimary.Controls.Add(txtCovCmnt)
        grpPrimary.Controls.Add(txtDOI)
        grpPrimary.Controls.Add(chkWorkman)
        grpPrimary.Controls.Add(Label105)
        grpPrimary.Controls.Add(txtPInsName)
        grpPrimary.Controls.Add(txtPInsID)
        grpPrimary.Controls.Add(txtPCopay)
        grpPrimary.Controls.Add(Label58)
        grpPrimary.Controls.Add(Label51)
        grpPrimary.Controls.Add(cmbPRelation)
        grpPrimary.Controls.Add(Label52)
        grpPrimary.Controls.Add(txtPFrom)
        grpPrimary.Controls.Add(btnPIns)
        grpPrimary.Controls.Add(Label53)
        grpPrimary.Controls.Add(txtPGroup)
        grpPrimary.Controls.Add(txtPTo)
        grpPrimary.Controls.Add(Label54)
        grpPrimary.Controls.Add(Label55)
        grpPrimary.Controls.Add(txtPPolicy)
        grpPrimary.Controls.Add(Label56)
        grpPrimary.Location = New Point(10, 11)
        grpPrimary.Margin = New Padding(5, 6, 5, 6)
        grpPrimary.Name = "grpPrimary"
        grpPrimary.Padding = New Padding(5, 6, 5, 6)
        grpPrimary.Size = New Size(1220, 259)
        grpPrimary.TabIndex = 20
        grpPrimary.TabStop = False
        grpPrimary.Text = "Primary Insurance"
        ' 
        ' Label123
        ' 
        Label123.ForeColor = Color.DarkBlue
        Label123.Location = New Point(244, 206)
        Label123.Margin = New Padding(5, 0, 5, 0)
        Label123.Name = "Label123"
        Label123.Size = New Size(136, 25)
        Label123.TabIndex = 82
        Label123.Text = "Instance Date:"
        Label123.TextAlign = ContentAlignment.TopRight
        ' 
        ' Label122
        ' 
        Label122.ForeColor = Color.DarkBlue
        Label122.Location = New Point(576, 108)
        Label122.Margin = New Padding(5, 0, 5, 0)
        Label122.Name = "Label122"
        Label122.Size = New Size(206, 25)
        Label122.TabIndex = 81
        Label122.Text = "Coverage Note"
        ' 
        ' txtCovCmnt
        ' 
        txtCovCmnt.Location = New Point(558, 139)
        txtCovCmnt.Margin = New Padding(5, 6, 5, 6)
        txtCovCmnt.MaxLength = 400
        txtCovCmnt.Multiline = True
        txtCovCmnt.Name = "txtCovCmnt"
        txtCovCmnt.ScrollBars = ScrollBars.Vertical
        txtCovCmnt.Size = New Size(629, 96)
        txtCovCmnt.TabIndex = 80
        ' 
        ' txtDOI
        ' 
        txtDOI.Location = New Point(385, 200)
        txtDOI.Margin = New Padding(5, 6, 5, 6)
        txtDOI.Mask = "00/00/0000"
        txtDOI.Name = "txtDOI"
        txtDOI.ReadOnly = True
        txtDOI.Size = New Size(148, 31)
        txtDOI.TabIndex = 79
        txtDOI.ValidatingType = GetType(Date)
        ' 
        ' chkWorkman
        ' 
        chkWorkman.CheckAlign = ContentAlignment.MiddleRight
        chkWorkman.Location = New Point(16, 200)
        chkWorkman.Margin = New Padding(5, 6, 5, 6)
        chkWorkman.Name = "chkWorkman"
        chkWorkman.Size = New Size(176, 42)
        chkWorkman.TabIndex = 78
        chkWorkman.Text = "Workman Comp"
        chkWorkman.UseVisualStyleBackColor = True
        ' 
        ' Label105
        ' 
        Label105.ForeColor = Color.Fuchsia
        Label105.Location = New Point(29, 22)
        Label105.Margin = New Padding(5, 0, 5, 0)
        Label105.Name = "Label105"
        Label105.Size = New Size(124, 25)
        Label105.TabIndex = 72
        Label105.Text = "Insurance ID"
        ' 
        ' txtPInsName
        ' 
        txtPInsName.AcceptsReturn = True
        txtPInsName.Location = New Point(215, 53)
        txtPInsName.Margin = New Padding(5, 6, 5, 6)
        txtPInsName.MaxLength = 100
        txtPInsName.Name = "txtPInsName"
        txtPInsName.ReadOnly = True
        txtPInsName.Size = New Size(406, 31)
        txtPInsName.TabIndex = 71
        ' 
        ' txtPInsID
        ' 
        txtPInsID.AcceptsReturn = True
        txtPInsID.Location = New Point(15, 53)
        txtPInsID.Margin = New Padding(5, 6, 5, 6)
        txtPInsID.MaxLength = 12
        txtPInsID.Name = "txtPInsID"
        txtPInsID.Size = New Size(134, 31)
        txtPInsID.TabIndex = 70
        txtPInsID.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtPCopay
        ' 
        txtPCopay.AcceptsReturn = True
        txtPCopay.Location = New Point(385, 136)
        txtPCopay.Margin = New Padding(5, 6, 5, 6)
        txtPCopay.MaxLength = 6
        txtPCopay.Name = "txtPCopay"
        txtPCopay.Size = New Size(148, 31)
        txtPCopay.TabIndex = 77
        ' 
        ' Label58
        ' 
        Label58.ForeColor = Color.DarkBlue
        Label58.Location = New Point(380, 106)
        Label58.Margin = New Padding(5, 0, 5, 0)
        Label58.Name = "Label58"
        Label58.Size = New Size(111, 25)
        Label58.TabIndex = 69
        Label58.Text = "Copay"
        ' 
        ' Label51
        ' 
        Label51.ForeColor = Color.Fuchsia
        Label51.Location = New Point(1014, 22)
        Label51.Margin = New Padding(5, 0, 5, 0)
        Label51.Name = "Label51"
        Label51.Size = New Size(144, 22)
        Label51.TabIndex = 67
        Label51.Text = "Relation"
        ' 
        ' cmbPRelation
        ' 
        cmbPRelation.DropDownStyle = ComboBoxStyle.DropDownList
        cmbPRelation.FormattingEnabled = True
        cmbPRelation.Items.AddRange(New Object() {"Self", "Spouse", "Son/Daughter", "Other Dependent"})
        cmbPRelation.Location = New Point(1005, 53)
        cmbPRelation.Margin = New Padding(5, 6, 5, 6)
        cmbPRelation.Name = "cmbPRelation"
        cmbPRelation.Size = New Size(182, 33)
        cmbPRelation.TabIndex = 74
        ' 
        ' Label52
        ' 
        Label52.ForeColor = Color.Fuchsia
        Label52.Location = New Point(235, 22)
        Label52.Margin = New Padding(5, 0, 5, 0)
        Label52.Name = "Label52"
        Label52.Size = New Size(266, 25)
        Label52.TabIndex = 60
        Label52.Text = "Insurance Name"
        ' 
        ' txtPFrom
        ' 
        txtPFrom.Location = New Point(15, 136)
        txtPFrom.Margin = New Padding(5, 6, 5, 6)
        txtPFrom.Mask = "00/00/0000"
        txtPFrom.Name = "txtPFrom"
        txtPFrom.Size = New Size(178, 31)
        txtPFrom.TabIndex = 75
        txtPFrom.ValidatingType = GetType(Date)
        ' 
        ' btnPIns
        ' 
        btnPIns.Image = CType(resources.GetObject("btnPIns.Image"), Image)
        btnPIns.Location = New Point(162, 47)
        btnPIns.Margin = New Padding(5, 6, 5, 6)
        btnPIns.Name = "btnPIns"
        btnPIns.Size = New Size(44, 50)
        btnPIns.TabIndex = 8
        btnPIns.TabStop = False
        btnPIns.UseVisualStyleBackColor = True
        ' 
        ' Label53
        ' 
        Label53.ForeColor = Color.DarkBlue
        Label53.Location = New Point(11, 106)
        Label53.Margin = New Padding(5, 0, 5, 0)
        Label53.Name = "Label53"
        Label53.Size = New Size(125, 25)
        Label53.TabIndex = 64
        Label53.Text = "Effective From"
        ' 
        ' txtPGroup
        ' 
        txtPGroup.AcceptsReturn = True
        txtPGroup.Location = New Point(834, 53)
        txtPGroup.Margin = New Padding(5, 6, 5, 6)
        txtPGroup.MaxLength = 35
        txtPGroup.Name = "txtPGroup"
        txtPGroup.Size = New Size(159, 31)
        txtPGroup.TabIndex = 73
        ' 
        ' txtPTo
        ' 
        txtPTo.Location = New Point(215, 136)
        txtPTo.Margin = New Padding(5, 6, 5, 6)
        txtPTo.Mask = "00/00/0000"
        txtPTo.Name = "txtPTo"
        txtPTo.Size = New Size(148, 31)
        txtPTo.TabIndex = 76
        txtPTo.ValidatingType = GetType(Date)
        ' 
        ' Label54
        ' 
        Label54.ForeColor = Color.DarkBlue
        Label54.Location = New Point(185, 108)
        Label54.Margin = New Padding(5, 0, 5, 0)
        Label54.Name = "Label54"
        Label54.Size = New Size(69, 25)
        Label54.TabIndex = 62
        Label54.Text = "Expires"
        ' 
        ' Label55
        ' 
        Label55.ForeColor = Color.DarkBlue
        Label55.Location = New Point(844, 22)
        Label55.Margin = New Padding(5, 0, 5, 0)
        Label55.Name = "Label55"
        Label55.Size = New Size(98, 25)
        Label55.TabIndex = 59
        Label55.Text = "Group No"
        ' 
        ' txtPPolicy
        ' 
        txtPPolicy.AcceptsReturn = True
        txtPPolicy.Location = New Point(644, 53)
        txtPPolicy.Margin = New Padding(5, 6, 5, 6)
        txtPPolicy.MaxLength = 35
        txtPPolicy.Name = "txtPPolicy"
        txtPPolicy.Size = New Size(178, 31)
        txtPPolicy.TabIndex = 72
        ' 
        ' Label56
        ' 
        Label56.ForeColor = Color.Fuchsia
        Label56.Location = New Point(644, 22)
        Label56.Margin = New Padding(5, 0, 5, 0)
        Label56.Name = "Label56"
        Label56.Size = New Size(146, 25)
        Label56.TabIndex = 61
        Label56.Text = "Policy No"
        ' 
        ' tpSecondary
        ' 
        tpSecondary.Controls.Add(grpSSubs)
        tpSecondary.Controls.Add(grpSecondary)
        tpSecondary.Location = New Point(4, 34)
        tpSecondary.Margin = New Padding(5, 6, 5, 6)
        tpSecondary.Name = "tpSecondary"
        tpSecondary.Padding = New Padding(5, 6, 5, 6)
        tpSecondary.Size = New Size(1250, 546)
        tpSecondary.TabIndex = 1
        tpSecondary.Text = "Secondary Insurance"
        tpSecondary.UseVisualStyleBackColor = True
        ' 
        ' grpSSubs
        ' 
        grpSSubs.Controls.Add(txtSSubPhone)
        grpSSubs.Controls.Add(Label80)
        grpSSubs.Controls.Add(txtSSubSSN)
        grpSSubs.Controls.Add(Label81)
        grpSSubs.Controls.Add(txtSSubDOB)
        grpSSubs.Controls.Add(cmbSSubSex)
        grpSSubs.Controls.Add(btnSSubLook)
        grpSSubs.Controls.Add(Label82)
        grpSSubs.Controls.Add(txtSSubCountry)
        grpSSubs.Controls.Add(Label83)
        grpSSubs.Controls.Add(txtSSubEmail)
        grpSSubs.Controls.Add(Label84)
        grpSSubs.Controls.Add(Label85)
        grpSSubs.Controls.Add(txtSSubZip)
        grpSSubs.Controls.Add(Label86)
        grpSSubs.Controls.Add(txtSSubState)
        grpSSubs.Controls.Add(Label87)
        grpSSubs.Controls.Add(txtSSubCity)
        grpSSubs.Controls.Add(Label88)
        grpSSubs.Controls.Add(txtSSubAdd2)
        grpSSubs.Controls.Add(Label89)
        grpSSubs.Controls.Add(txtSSubAdd1)
        grpSSubs.Controls.Add(Label90)
        grpSSubs.Controls.Add(Label91)
        grpSSubs.Controls.Add(txtSSubMName)
        grpSSubs.Controls.Add(Label92)
        grpSSubs.Controls.Add(txtSSubFName)
        grpSSubs.Controls.Add(Label93)
        grpSSubs.Controls.Add(txtSSubLName)
        grpSSubs.Controls.Add(Label94)
        grpSSubs.Controls.Add(txtSSubID)
        grpSSubs.Location = New Point(10, 209)
        grpSSubs.Margin = New Padding(5, 6, 5, 6)
        grpSSubs.Name = "grpSSubs"
        grpSSubs.Padding = New Padding(5, 6, 5, 6)
        grpSSubs.Size = New Size(1020, 300)
        grpSSubs.TabIndex = 55
        grpSSubs.TabStop = False
        grpSSubs.Text = "Secondary Subscriber"
        ' 
        ' txtSSubPhone
        ' 
        txtSSubPhone.Location = New Point(536, 231)
        txtSSubPhone.Margin = New Padding(5, 6, 5, 6)
        txtSSubPhone.Name = "txtSSubPhone"
        txtSSubPhone.Size = New Size(174, 31)
        txtSSubPhone.TabIndex = 54
        ' 
        ' Label80
        ' 
        Label80.ForeColor = Color.DarkBlue
        Label80.Location = New Point(10, 116)
        Label80.Margin = New Padding(5, 0, 5, 0)
        Label80.Name = "Label80"
        Label80.Size = New Size(114, 25)
        Label80.TabIndex = 94
        Label80.Text = "SSN"
        ' 
        ' txtSSubSSN
        ' 
        txtSSubSSN.Location = New Point(10, 147)
        txtSSubSSN.Margin = New Padding(5, 6, 5, 6)
        txtSSubSSN.Mask = "000-00-0000"
        txtSSubSSN.Name = "txtSSubSSN"
        txtSSubSSN.Size = New Size(174, 31)
        txtSSubSSN.TabIndex = 47
        ' 
        ' Label81
        ' 
        Label81.ForeColor = Color.DarkBlue
        Label81.Location = New Point(654, 31)
        Label81.Margin = New Padding(5, 0, 5, 0)
        Label81.Name = "Label81"
        Label81.Size = New Size(70, 25)
        Label81.TabIndex = 90
        Label81.Text = "Gender"
        ' 
        ' txtSSubDOB
        ' 
        txtSSubDOB.Location = New Point(825, 59)
        txtSSubDOB.Margin = New Padding(5, 6, 5, 6)
        txtSSubDOB.Mask = "00/00/0000"
        txtSSubDOB.Name = "txtSSubDOB"
        txtSSubDOB.Size = New Size(124, 31)
        txtSSubDOB.TabIndex = 46
        txtSSubDOB.ValidatingType = GetType(Date)
        ' 
        ' cmbSSubSex
        ' 
        cmbSSubSex.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSSubSex.FormattingEnabled = True
        cmbSSubSex.Items.AddRange(New Object() {"Female", "Male", "Indetermined", "Unreported"})
        cmbSSubSex.Location = New Point(649, 59)
        cmbSSubSex.Margin = New Padding(5, 6, 5, 6)
        cmbSSubSex.Name = "cmbSSubSex"
        cmbSSubSex.Size = New Size(164, 33)
        cmbSSubSex.TabIndex = 45
        ' 
        ' btnSSubLook
        ' 
        btnSSubLook.Enabled = False
        btnSSubLook.Image = CType(resources.GetObject("btnSSubLook.Image"), Image)
        btnSSubLook.Location = New Point(144, 53)
        btnSSubLook.Margin = New Padding(5, 6, 5, 6)
        btnSSubLook.Name = "btnSSubLook"
        btnSSubLook.Size = New Size(44, 50)
        btnSSubLook.TabIndex = 41
        btnSSubLook.TabStop = False
        btnSSubLook.UseVisualStyleBackColor = True
        ' 
        ' Label82
        ' 
        Label82.ForeColor = Color.DarkBlue
        Label82.Location = New Point(326, 200)
        Label82.Margin = New Padding(5, 0, 5, 0)
        Label82.Name = "Label82"
        Label82.Size = New Size(130, 25)
        Label82.TabIndex = 86
        Label82.Text = "Country"
        ' 
        ' txtSSubCountry
        ' 
        txtSSubCountry.AcceptsReturn = True
        txtSSubCountry.Location = New Point(331, 231)
        txtSSubCountry.Margin = New Padding(5, 6, 5, 6)
        txtSSubCountry.MaxLength = 35
        txtSSubCountry.Name = "txtSSubCountry"
        txtSSubCountry.Size = New Size(193, 31)
        txtSSubCountry.TabIndex = 53
        ' 
        ' Label83
        ' 
        Label83.ForeColor = Color.DarkBlue
        Label83.Location = New Point(720, 200)
        Label83.Margin = New Padding(5, 0, 5, 0)
        Label83.Name = "Label83"
        Label83.Size = New Size(175, 25)
        Label83.TabIndex = 85
        Label83.Text = "Email Address"
        ' 
        ' txtSSubEmail
        ' 
        txtSSubEmail.AcceptsReturn = True
        txtSSubEmail.Location = New Point(725, 231)
        txtSSubEmail.Margin = New Padding(5, 6, 5, 6)
        txtSSubEmail.MaxLength = 25
        txtSSubEmail.Name = "txtSSubEmail"
        txtSSubEmail.Size = New Size(224, 31)
        txtSSubEmail.TabIndex = 55
        ' 
        ' Label84
        ' 
        Label84.ForeColor = Color.DarkBlue
        Label84.Location = New Point(538, 200)
        Label84.Margin = New Padding(5, 0, 5, 0)
        Label84.Name = "Label84"
        Label84.Size = New Size(155, 25)
        Label84.TabIndex = 84
        Label84.Text = "Home Phone"
        ' 
        ' Label85
        ' 
        Label85.ForeColor = Color.Fuchsia
        Label85.Location = New Point(206, 200)
        Label85.Margin = New Padding(5, 0, 5, 0)
        Label85.Name = "Label85"
        Label85.Size = New Size(75, 25)
        Label85.TabIndex = 83
        Label85.Text = "Zip"
        ' 
        ' txtSSubZip
        ' 
        txtSSubZip.AcceptsReturn = True
        txtSSubZip.Location = New Point(196, 231)
        txtSSubZip.Margin = New Padding(5, 6, 5, 6)
        txtSSubZip.MaxLength = 25
        txtSSubZip.Name = "txtSSubZip"
        txtSSubZip.Size = New Size(120, 31)
        txtSSubZip.TabIndex = 52
        ' 
        ' Label86
        ' 
        Label86.ForeColor = Color.Fuchsia
        Label86.Location = New Point(15, 200)
        Label86.Margin = New Padding(5, 0, 5, 0)
        Label86.Name = "Label86"
        Label86.Size = New Size(145, 25)
        Label86.TabIndex = 82
        Label86.Text = "State/Province"
        ' 
        ' txtSSubState
        ' 
        txtSSubState.AcceptsReturn = True
        txtSSubState.Location = New Point(10, 231)
        txtSSubState.Margin = New Padding(5, 6, 5, 6)
        txtSSubState.MaxLength = 35
        txtSSubState.Name = "txtSSubState"
        txtSSubState.Size = New Size(174, 31)
        txtSSubState.TabIndex = 51
        ' 
        ' Label87
        ' 
        Label87.ForeColor = Color.Fuchsia
        Label87.Location = New Point(725, 116)
        Label87.Margin = New Padding(5, 0, 5, 0)
        Label87.Name = "Label87"
        Label87.Size = New Size(70, 25)
        Label87.TabIndex = 81
        Label87.Text = "City"
        ' 
        ' txtSSubCity
        ' 
        txtSSubCity.AcceptsReturn = True
        txtSSubCity.Location = New Point(710, 147)
        txtSSubCity.Margin = New Padding(5, 6, 5, 6)
        txtSSubCity.MaxLength = 35
        txtSSubCity.Name = "txtSSubCity"
        txtSSubCity.Size = New Size(239, 31)
        txtSSubCity.TabIndex = 50
        ' 
        ' Label88
        ' 
        Label88.ForeColor = Color.DarkBlue
        Label88.Location = New Point(444, 116)
        Label88.Margin = New Padding(5, 0, 5, 0)
        Label88.Name = "Label88"
        Label88.Size = New Size(195, 25)
        Label88.TabIndex = 80
        Label88.Text = "Address Line 2"
        ' 
        ' txtSSubAdd2
        ' 
        txtSSubAdd2.AcceptsReturn = True
        txtSSubAdd2.Location = New Point(449, 147)
        txtSSubAdd2.Margin = New Padding(5, 6, 5, 6)
        txtSSubAdd2.MaxLength = 35
        txtSSubAdd2.Name = "txtSSubAdd2"
        txtSSubAdd2.Size = New Size(239, 31)
        txtSSubAdd2.TabIndex = 49
        ' 
        ' Label89
        ' 
        Label89.ForeColor = Color.Fuchsia
        Label89.Location = New Point(202, 116)
        Label89.Margin = New Padding(5, 0, 5, 0)
        Label89.Name = "Label89"
        Label89.Size = New Size(169, 25)
        Label89.TabIndex = 78
        Label89.Text = "Address Line 1"
        ' 
        ' txtSSubAdd1
        ' 
        txtSSubAdd1.AcceptsReturn = True
        txtSSubAdd1.Location = New Point(196, 147)
        txtSSubAdd1.Margin = New Padding(5, 6, 5, 6)
        txtSSubAdd1.MaxLength = 35
        txtSSubAdd1.Name = "txtSSubAdd1"
        txtSSubAdd1.Size = New Size(239, 31)
        txtSSubAdd1.TabIndex = 48
        ' 
        ' Label90
        ' 
        Label90.ForeColor = Color.DarkBlue
        Label90.Location = New Point(820, 28)
        Label90.Margin = New Padding(5, 0, 5, 0)
        Label90.Name = "Label90"
        Label90.Size = New Size(60, 25)
        Label90.TabIndex = 75
        Label90.Text = "D.O.B"
        ' 
        ' Label91
        ' 
        Label91.ForeColor = Color.DarkBlue
        Label91.Location = New Point(516, 31)
        Label91.Margin = New Padding(5, 0, 5, 0)
        Label91.Name = "Label91"
        Label91.Size = New Size(115, 25)
        Label91.TabIndex = 72
        Label91.Text = "Middle Name"
        ' 
        ' txtSSubMName
        ' 
        txtSSubMName.AcceptsReturn = True
        txtSSubMName.Location = New Point(514, 61)
        txtSSubMName.Margin = New Padding(5, 6, 5, 6)
        txtSSubMName.MaxLength = 35
        txtSSubMName.Name = "txtSSubMName"
        txtSSubMName.Size = New Size(122, 31)
        txtSSubMName.TabIndex = 44
        ' 
        ' Label92
        ' 
        Label92.ForeColor = Color.DarkBlue
        Label92.Location = New Point(358, 31)
        Label92.Margin = New Padding(5, 0, 5, 0)
        Label92.Name = "Label92"
        Label92.Size = New Size(149, 25)
        Label92.TabIndex = 69
        Label92.Text = "First Name"
        ' 
        ' txtSSubFName
        ' 
        txtSSubFName.AcceptsReturn = True
        txtSSubFName.Location = New Point(355, 61)
        txtSSubFName.Margin = New Padding(5, 6, 5, 6)
        txtSSubFName.MaxLength = 35
        txtSSubFName.Name = "txtSSubFName"
        txtSSubFName.Size = New Size(146, 31)
        txtSSubFName.TabIndex = 43
        ' 
        ' Label93
        ' 
        Label93.ForeColor = Color.DarkBlue
        Label93.Location = New Point(191, 28)
        Label93.Margin = New Padding(5, 0, 5, 0)
        Label93.Name = "Label93"
        Label93.Size = New Size(154, 25)
        Label93.TabIndex = 67
        Label93.Text = "Last Name"
        ' 
        ' txtSSubLName
        ' 
        txtSSubLName.AcceptsReturn = True
        txtSSubLName.Location = New Point(196, 61)
        txtSSubLName.Margin = New Padding(5, 6, 5, 6)
        txtSSubLName.MaxLength = 35
        txtSSubLName.Name = "txtSSubLName"
        txtSSubLName.Size = New Size(146, 31)
        txtSSubLName.TabIndex = 42
        ' 
        ' Label94
        ' 
        Label94.ForeColor = Color.Fuchsia
        Label94.Location = New Point(10, 31)
        Label94.Margin = New Padding(5, 0, 5, 0)
        Label94.Name = "Label94"
        Label94.Size = New Size(124, 25)
        Label94.TabIndex = 64
        Label94.Text = "Subscriber ID"
        ' 
        ' txtSSubID
        ' 
        txtSSubID.AcceptsReturn = True
        txtSSubID.Location = New Point(10, 61)
        txtSSubID.Margin = New Padding(5, 6, 5, 6)
        txtSSubID.MaxLength = 12
        txtSSubID.Name = "txtSSubID"
        txtSSubID.ReadOnly = True
        txtSSubID.Size = New Size(124, 31)
        txtSSubID.TabIndex = 40
        txtSSubID.TabStop = False
        txtSSubID.TextAlign = HorizontalAlignment.Center
        ToolTip1.SetToolTip(txtSSubID, "Patient ID of subscriber")
        ' 
        ' grpSecondary
        ' 
        grpSecondary.Controls.Add(Label106)
        grpSecondary.Controls.Add(txtSInsName)
        grpSecondary.Controls.Add(txtSPolicy)
        grpSecondary.Controls.Add(txtSCopay)
        grpSecondary.Controls.Add(Label73)
        grpSecondary.Controls.Add(Label74)
        grpSecondary.Controls.Add(cmbSRelation)
        grpSecondary.Controls.Add(Label75)
        grpSecondary.Controls.Add(txtSFrom)
        grpSecondary.Controls.Add(btnSIns)
        grpSecondary.Controls.Add(Label76)
        grpSecondary.Controls.Add(txtSGroup)
        grpSecondary.Controls.Add(txtSTo)
        grpSecondary.Controls.Add(Label77)
        grpSecondary.Controls.Add(Label78)
        grpSecondary.Controls.Add(txtSInsID)
        grpSecondary.Controls.Add(Label79)
        grpSecondary.Location = New Point(10, 11)
        grpSecondary.Margin = New Padding(5, 6, 5, 6)
        grpSecondary.Name = "grpSecondary"
        grpSecondary.Padding = New Padding(5, 6, 5, 6)
        grpSecondary.Size = New Size(1020, 186)
        grpSecondary.TabIndex = 46
        grpSecondary.TabStop = False
        grpSecondary.Text = "Secondary Insurance"
        ' 
        ' Label106
        ' 
        Label106.ForeColor = Color.DarkBlue
        Label106.Location = New Point(238, 22)
        Label106.Margin = New Padding(5, 0, 5, 0)
        Label106.Name = "Label106"
        Label106.Size = New Size(150, 25)
        Label106.TabIndex = 72
        Label106.Text = "Insurance Name"
        ' 
        ' txtSInsName
        ' 
        txtSInsName.AcceptsReturn = True
        txtSInsName.Location = New Point(216, 53)
        txtSInsName.Margin = New Padding(5, 6, 5, 6)
        txtSInsName.MaxLength = 100
        txtSInsName.Name = "txtSInsName"
        txtSInsName.ReadOnly = True
        txtSInsName.Size = New Size(562, 31)
        txtSInsName.TabIndex = 33
        ' 
        ' txtSPolicy
        ' 
        txtSPolicy.AcceptsReturn = True
        txtSPolicy.Location = New Point(795, 53)
        txtSPolicy.Margin = New Padding(5, 6, 5, 6)
        txtSPolicy.MaxLength = 35
        txtSPolicy.Name = "txtSPolicy"
        txtSPolicy.Size = New Size(198, 31)
        txtSPolicy.TabIndex = 34
        ' 
        ' txtSCopay
        ' 
        txtSCopay.AcceptsReturn = True
        txtSCopay.Location = New Point(825, 136)
        txtSCopay.Margin = New Padding(5, 6, 5, 6)
        txtSCopay.MaxLength = 6
        txtSCopay.Name = "txtSCopay"
        txtSCopay.Size = New Size(168, 31)
        txtSCopay.TabIndex = 39
        ' 
        ' Label73
        ' 
        Label73.ForeColor = Color.DarkBlue
        Label73.Location = New Point(820, 106)
        Label73.Margin = New Padding(5, 0, 5, 0)
        Label73.Name = "Label73"
        Label73.Size = New Size(111, 25)
        Label73.TabIndex = 71
        Label73.Text = "Copay"
        ' 
        ' Label74
        ' 
        Label74.ForeColor = Color.Fuchsia
        Label74.Location = New Point(280, 106)
        Label74.Margin = New Padding(5, 0, 5, 0)
        Label74.Name = "Label74"
        Label74.Size = New Size(76, 25)
        Label74.TabIndex = 67
        Label74.Text = "Relation"
        ' 
        ' cmbSRelation
        ' 
        cmbSRelation.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSRelation.FormattingEnabled = True
        cmbSRelation.Items.AddRange(New Object() {"Self", "Spouse", "Son/Daughter", "Other Dependent"})
        cmbSRelation.Location = New Point(276, 136)
        cmbSRelation.Margin = New Padding(5, 6, 5, 6)
        cmbSRelation.Name = "cmbSRelation"
        cmbSRelation.Size = New Size(193, 33)
        cmbSRelation.TabIndex = 36
        ' 
        ' Label75
        ' 
        Label75.ForeColor = Color.Fuchsia
        Label75.Location = New Point(15, 22)
        Label75.Margin = New Padding(5, 0, 5, 0)
        Label75.Name = "Label75"
        Label75.Size = New Size(136, 25)
        Label75.TabIndex = 60
        Label75.Text = "Insurance ID"
        ' 
        ' txtSFrom
        ' 
        txtSFrom.Location = New Point(482, 136)
        txtSFrom.Margin = New Padding(5, 6, 5, 6)
        txtSFrom.Mask = "00/00/0000"
        txtSFrom.Name = "txtSFrom"
        txtSFrom.Size = New Size(159, 31)
        txtSFrom.TabIndex = 37
        txtSFrom.ValidatingType = GetType(Date)
        ' 
        ' btnSIns
        ' 
        btnSIns.Image = CType(resources.GetObject("btnSIns.Image"), Image)
        btnSIns.Location = New Point(164, 47)
        btnSIns.Margin = New Padding(5, 6, 5, 6)
        btnSIns.Name = "btnSIns"
        btnSIns.Size = New Size(44, 50)
        btnSIns.TabIndex = 32
        btnSIns.TabStop = False
        btnSIns.UseVisualStyleBackColor = True
        ' 
        ' Label76
        ' 
        Label76.ForeColor = Color.DarkBlue
        Label76.Location = New Point(476, 106)
        Label76.Margin = New Padding(5, 0, 5, 0)
        Label76.Name = "Label76"
        Label76.Size = New Size(125, 25)
        Label76.TabIndex = 64
        Label76.Text = "Effective From"
        ' 
        ' txtSGroup
        ' 
        txtSGroup.AcceptsReturn = True
        txtSGroup.Location = New Point(15, 136)
        txtSGroup.Margin = New Padding(5, 6, 5, 6)
        txtSGroup.MaxLength = 35
        txtSGroup.Name = "txtSGroup"
        txtSGroup.Size = New Size(239, 31)
        txtSGroup.TabIndex = 35
        ' 
        ' txtSTo
        ' 
        txtSTo.Location = New Point(658, 136)
        txtSTo.Margin = New Padding(5, 6, 5, 6)
        txtSTo.Mask = "00/00/0000"
        txtSTo.Name = "txtSTo"
        txtSTo.Size = New Size(148, 31)
        txtSTo.TabIndex = 38
        txtSTo.ValidatingType = GetType(Date)
        ' 
        ' Label77
        ' 
        Label77.ForeColor = Color.DarkBlue
        Label77.Location = New Point(662, 106)
        Label77.Margin = New Padding(5, 0, 5, 0)
        Label77.Name = "Label77"
        Label77.Size = New Size(69, 25)
        Label77.TabIndex = 62
        Label77.Text = "Expires"
        ' 
        ' Label78
        ' 
        Label78.ForeColor = Color.DarkBlue
        Label78.Location = New Point(10, 106)
        Label78.Margin = New Padding(5, 0, 5, 0)
        Label78.Name = "Label78"
        Label78.Size = New Size(150, 25)
        Label78.TabIndex = 59
        Label78.Text = "Group No"
        ' 
        ' txtSInsID
        ' 
        txtSInsID.AcceptsReturn = True
        txtSInsID.Location = New Point(15, 53)
        txtSInsID.Margin = New Padding(5, 6, 5, 6)
        txtSInsID.MaxLength = 12
        txtSInsID.Name = "txtSInsID"
        txtSInsID.Size = New Size(134, 31)
        txtSInsID.TabIndex = 31
        txtSInsID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label79
        ' 
        Label79.ForeColor = Color.Fuchsia
        Label79.Location = New Point(814, 22)
        Label79.Margin = New Padding(5, 0, 5, 0)
        Label79.Name = "Label79"
        Label79.Size = New Size(176, 25)
        Label79.TabIndex = 61
        Label79.Text = "Policy No"
        ' 
        ' tpGuarantor
        ' 
        tpGuarantor.Controls.Add(Label128)
        tpGuarantor.Controls.Add(txtGTage)
        tpGuarantor.Controls.Add(Label107)
        tpGuarantor.Controls.Add(cmbGRelation)
        tpGuarantor.Controls.Add(txtGCountry)
        tpGuarantor.Controls.Add(Label109)
        tpGuarantor.Controls.Add(txtGPhone)
        tpGuarantor.Controls.Add(chkGIsIndividual)
        tpGuarantor.Controls.Add(txtGZip)
        tpGuarantor.Controls.Add(Label112)
        tpGuarantor.Controls.Add(txtGEmail)
        tpGuarantor.Controls.Add(Label110)
        tpGuarantor.Controls.Add(txtGState)
        tpGuarantor.Controls.Add(Label113)
        tpGuarantor.Controls.Add(txtGSSN)
        tpGuarantor.Controls.Add(lblGSSN)
        tpGuarantor.Controls.Add(txtGLName_BSN)
        tpGuarantor.Controls.Add(Label111)
        tpGuarantor.Controls.Add(txtGDOB)
        tpGuarantor.Controls.Add(lblGSex)
        tpGuarantor.Controls.Add(cmbGSex)
        tpGuarantor.Controls.Add(Label121)
        tpGuarantor.Controls.Add(txtGID)
        tpGuarantor.Controls.Add(txtGCity)
        tpGuarantor.Controls.Add(Label114)
        tpGuarantor.Controls.Add(btnGLookUp)
        tpGuarantor.Controls.Add(lblGLName)
        tpGuarantor.Controls.Add(txtGAdd2)
        tpGuarantor.Controls.Add(Label115)
        tpGuarantor.Controls.Add(lblGFName)
        tpGuarantor.Controls.Add(txtGFName)
        tpGuarantor.Controls.Add(txtGAdd1)
        tpGuarantor.Controls.Add(Label116)
        tpGuarantor.Controls.Add(lblGMName)
        tpGuarantor.Controls.Add(txtGMName)
        tpGuarantor.Controls.Add(lblGDOB)
        tpGuarantor.Location = New Point(4, 34)
        tpGuarantor.Margin = New Padding(5, 6, 5, 6)
        tpGuarantor.Name = "tpGuarantor"
        tpGuarantor.Padding = New Padding(5, 6, 5, 6)
        tpGuarantor.Size = New Size(1250, 546)
        tpGuarantor.TabIndex = 2
        tpGuarantor.Text = "Guarantor"
        tpGuarantor.UseVisualStyleBackColor = True
        ' 
        ' Label128
        ' 
        Label128.Location = New Point(334, 122)
        Label128.Margin = New Padding(5, 0, 5, 0)
        Label128.Name = "Label128"
        Label128.Size = New Size(115, 25)
        Label128.TabIndex = 97
        Label128.Text = "Tage (Years)"
        ' 
        ' txtGTage
        ' 
        txtGTage.BackColor = Color.White
        txtGTage.Location = New Point(334, 153)
        txtGTage.Margin = New Padding(5, 6, 5, 6)
        txtGTage.MaxLength = 35
        txtGTage.Name = "txtGTage"
        txtGTage.ReadOnly = True
        txtGTage.Size = New Size(113, 31)
        txtGTage.TabIndex = 38
        txtGTage.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label107
        ' 
        Label107.ForeColor = Color.DarkBlue
        Label107.Location = New Point(878, 22)
        Label107.Margin = New Padding(5, 0, 5, 0)
        Label107.Name = "Label107"
        Label107.Size = New Size(184, 25)
        Label107.TabIndex = 95
        Label107.Text = "Relation To Patient"
        ' 
        ' cmbGRelation
        ' 
        cmbGRelation.DropDownStyle = ComboBoxStyle.DropDownList
        cmbGRelation.FormattingEnabled = True
        cmbGRelation.Location = New Point(866, 58)
        cmbGRelation.Margin = New Padding(5, 6, 5, 6)
        cmbGRelation.Name = "cmbGRelation"
        cmbGRelation.Size = New Size(193, 33)
        cmbGRelation.TabIndex = 94
        ' 
        ' txtGCountry
        ' 
        txtGCountry.AcceptsReturn = True
        txtGCountry.Location = New Point(529, 344)
        txtGCountry.Margin = New Padding(5, 6, 5, 6)
        txtGCountry.MaxLength = 35
        txtGCountry.Name = "txtGCountry"
        txtGCountry.Size = New Size(240, 31)
        txtGCountry.TabIndex = 48
        ' 
        ' Label109
        ' 
        Label109.ForeColor = Color.DarkBlue
        Label109.Location = New Point(544, 314)
        Label109.Margin = New Padding(5, 0, 5, 0)
        Label109.Name = "Label109"
        Label109.Size = New Size(116, 25)
        Label109.TabIndex = 86
        Label109.Text = "Country"
        ' 
        ' txtGPhone
        ' 
        txtGPhone.Location = New Point(780, 153)
        txtGPhone.Margin = New Padding(5, 6, 5, 6)
        txtGPhone.Name = "txtGPhone"
        txtGPhone.Size = New Size(174, 31)
        txtGPhone.TabIndex = 41
        ' 
        ' chkGIsIndividual
        ' 
        chkGIsIndividual.Appearance = Appearance.Button
        chkGIsIndividual.Checked = True
        chkGIsIndividual.CheckState = CheckState.Checked
        chkGIsIndividual.Location = New Point(31, 53)
        chkGIsIndividual.Margin = New Padding(5, 6, 5, 6)
        chkGIsIndividual.Name = "chkGIsIndividual"
        chkGIsIndividual.Size = New Size(149, 47)
        chkGIsIndividual.TabIndex = 31
        chkGIsIndividual.Text = "Individual"
        chkGIsIndividual.TextAlign = ContentAlignment.MiddleCenter
        chkGIsIndividual.UseVisualStyleBackColor = True
        ' 
        ' txtGZip
        ' 
        txtGZip.AcceptsReturn = True
        txtGZip.Location = New Point(271, 344)
        txtGZip.Margin = New Padding(5, 6, 5, 6)
        txtGZip.MaxLength = 25
        txtGZip.Name = "txtGZip"
        txtGZip.Size = New Size(240, 31)
        txtGZip.TabIndex = 47
        ' 
        ' Label112
        ' 
        Label112.ForeColor = Color.Fuchsia
        Label112.Location = New Point(274, 314)
        Label112.Margin = New Padding(5, 0, 5, 0)
        Label112.Name = "Label112"
        Label112.Size = New Size(74, 25)
        Label112.TabIndex = 83
        Label112.Text = "Zip"
        ' 
        ' txtGEmail
        ' 
        txtGEmail.AcceptsReturn = True
        txtGEmail.Location = New Point(966, 153)
        txtGEmail.Margin = New Padding(5, 6, 5, 6)
        txtGEmail.MaxLength = 25
        txtGEmail.Name = "txtGEmail"
        txtGEmail.Size = New Size(244, 31)
        txtGEmail.TabIndex = 42
        ' 
        ' Label110
        ' 
        Label110.ForeColor = Color.DarkBlue
        Label110.Location = New Point(975, 122)
        Label110.Margin = New Padding(5, 0, 5, 0)
        Label110.Name = "Label110"
        Label110.Size = New Size(111, 25)
        Label110.TabIndex = 85
        Label110.Text = "Email Address"
        ' 
        ' txtGState
        ' 
        txtGState.AcceptsReturn = True
        txtGState.Location = New Point(31, 344)
        txtGState.Margin = New Padding(5, 6, 5, 6)
        txtGState.MaxLength = 35
        txtGState.Name = "txtGState"
        txtGState.Size = New Size(228, 31)
        txtGState.TabIndex = 46
        ' 
        ' Label113
        ' 
        Label113.ForeColor = Color.Fuchsia
        Label113.Location = New Point(44, 314)
        Label113.Margin = New Padding(5, 0, 5, 0)
        Label113.Name = "Label113"
        Label113.Size = New Size(145, 25)
        Label113.TabIndex = 82
        Label113.Text = "State/Province"
        ' 
        ' txtGSSN
        ' 
        txtGSSN.Location = New Point(604, 153)
        txtGSSN.Margin = New Padding(5, 6, 5, 6)
        txtGSSN.Mask = "000-00-0000"
        txtGSSN.Name = "txtGSSN"
        txtGSSN.Size = New Size(159, 31)
        txtGSSN.TabIndex = 40
        ' 
        ' lblGSSN
        ' 
        lblGSSN.ForeColor = Color.DarkBlue
        lblGSSN.Location = New Point(610, 122)
        lblGSSN.Margin = New Padding(5, 0, 5, 0)
        lblGSSN.Name = "lblGSSN"
        lblGSSN.Size = New Size(100, 25)
        lblGSSN.TabIndex = 93
        lblGSSN.Text = "SSN"
        ' 
        ' txtGLName_BSN
        ' 
        txtGLName_BSN.AcceptsReturn = True
        txtGLName_BSN.Location = New Point(380, 58)
        txtGLName_BSN.Margin = New Padding(5, 6, 5, 6)
        txtGLName_BSN.Name = "txtGLName_BSN"
        txtGLName_BSN.Size = New Size(206, 31)
        txtGLName_BSN.TabIndex = 34
        ' 
        ' Label111
        ' 
        Label111.ForeColor = Color.DarkBlue
        Label111.Location = New Point(786, 122)
        Label111.Margin = New Padding(5, 0, 5, 0)
        Label111.Name = "Label111"
        Label111.Size = New Size(160, 25)
        Label111.TabIndex = 84
        Label111.Text = "Phone"
        ' 
        ' txtGDOB
        ' 
        txtGDOB.Location = New Point(458, 153)
        txtGDOB.Margin = New Padding(5, 6, 5, 6)
        txtGDOB.Mask = "00/00/0000"
        txtGDOB.Name = "txtGDOB"
        txtGDOB.Size = New Size(128, 31)
        txtGDOB.TabIndex = 39
        txtGDOB.ValidatingType = GetType(Date)
        ' 
        ' lblGSex
        ' 
        lblGSex.ForeColor = Color.Fuchsia
        lblGSex.Location = New Point(40, 122)
        lblGSex.Margin = New Padding(5, 0, 5, 0)
        lblGSex.Name = "lblGSex"
        lblGSex.Size = New Size(120, 25)
        lblGSex.TabIndex = 90
        lblGSex.Text = "Gender"
        ' 
        ' cmbGSex
        ' 
        cmbGSex.DropDownStyle = ComboBoxStyle.DropDownList
        cmbGSex.FormattingEnabled = True
        cmbGSex.Items.AddRange(New Object() {"F - Female", "M - Male", "G - Transgender Female", "N - Transgender Male ", "I - Indetermined", "U - Unreported"})
        cmbGSex.Location = New Point(31, 153)
        cmbGSex.Margin = New Padding(5, 6, 5, 6)
        cmbGSex.Name = "cmbGSex"
        cmbGSex.Size = New Size(282, 33)
        cmbGSex.TabIndex = 37
        ' 
        ' Label121
        ' 
        Label121.ForeColor = Color.Fuchsia
        Label121.Location = New Point(191, 22)
        Label121.Margin = New Padding(5, 0, 5, 0)
        Label121.Name = "Label121"
        Label121.Size = New Size(134, 25)
        Label121.TabIndex = 64
        Label121.Text = "Guarantor ID"
        ' 
        ' txtGID
        ' 
        txtGID.AcceptsReturn = True
        txtGID.Location = New Point(190, 58)
        txtGID.Margin = New Padding(5, 6, 5, 6)
        txtGID.MaxLength = 12
        txtGID.Name = "txtGID"
        txtGID.ReadOnly = True
        txtGID.Size = New Size(124, 31)
        txtGID.TabIndex = 32
        txtGID.TabStop = False
        txtGID.TextAlign = HorizontalAlignment.Center
        ToolTip1.SetToolTip(txtGID, "Patient ID of the subsvriber")
        ' 
        ' txtGCity
        ' 
        txtGCity.AcceptsReturn = True
        txtGCity.Location = New Point(531, 250)
        txtGCity.Margin = New Padding(5, 6, 5, 6)
        txtGCity.MaxLength = 35
        txtGCity.Name = "txtGCity"
        txtGCity.Size = New Size(240, 31)
        txtGCity.TabIndex = 45
        ' 
        ' Label114
        ' 
        Label114.ForeColor = Color.Fuchsia
        Label114.Location = New Point(544, 219)
        Label114.Margin = New Padding(5, 0, 5, 0)
        Label114.Name = "Label114"
        Label114.Size = New Size(104, 25)
        Label114.TabIndex = 81
        Label114.Text = "City"
        ' 
        ' btnGLookUp
        ' 
        btnGLookUp.Image = CType(resources.GetObject("btnGLookUp.Image"), Image)
        btnGLookUp.Location = New Point(326, 50)
        btnGLookUp.Margin = New Padding(5, 6, 5, 6)
        btnGLookUp.Name = "btnGLookUp"
        btnGLookUp.Size = New Size(44, 50)
        btnGLookUp.TabIndex = 33
        btnGLookUp.TabStop = False
        btnGLookUp.UseVisualStyleBackColor = True
        ' 
        ' lblGLName
        ' 
        lblGLName.ForeColor = Color.Fuchsia
        lblGLName.Location = New Point(386, 22)
        lblGLName.Margin = New Padding(5, 0, 5, 0)
        lblGLName.Name = "lblGLName"
        lblGLName.Size = New Size(134, 25)
        lblGLName.TabIndex = 67
        lblGLName.Text = "Last Name"
        ' 
        ' txtGAdd2
        ' 
        txtGAdd2.AcceptsReturn = True
        txtGAdd2.Location = New Point(278, 250)
        txtGAdd2.Margin = New Padding(5, 6, 5, 6)
        txtGAdd2.MaxLength = 35
        txtGAdd2.Name = "txtGAdd2"
        txtGAdd2.Size = New Size(240, 31)
        txtGAdd2.TabIndex = 44
        ' 
        ' Label115
        ' 
        Label115.ForeColor = Color.DarkBlue
        Label115.Location = New Point(274, 219)
        Label115.Margin = New Padding(5, 0, 5, 0)
        Label115.Name = "Label115"
        Label115.Size = New Size(190, 25)
        Label115.TabIndex = 80
        Label115.Text = "Address Line 2"
        ' 
        ' lblGFName
        ' 
        lblGFName.ForeColor = Color.Fuchsia
        lblGFName.Location = New Point(604, 22)
        lblGFName.Margin = New Padding(5, 0, 5, 0)
        lblGFName.Name = "lblGFName"
        lblGFName.Size = New Size(120, 25)
        lblGFName.TabIndex = 69
        lblGFName.Text = "First Name"
        ' 
        ' txtGFName
        ' 
        txtGFName.AcceptsReturn = True
        txtGFName.Location = New Point(604, 58)
        txtGFName.Margin = New Padding(5, 6, 5, 6)
        txtGFName.MaxLength = 35
        txtGFName.Name = "txtGFName"
        txtGFName.Size = New Size(130, 31)
        txtGFName.TabIndex = 35
        ' 
        ' txtGAdd1
        ' 
        txtGAdd1.AcceptsReturn = True
        txtGAdd1.Location = New Point(31, 250)
        txtGAdd1.Margin = New Padding(5, 6, 5, 6)
        txtGAdd1.MaxLength = 35
        txtGAdd1.Name = "txtGAdd1"
        txtGAdd1.Size = New Size(229, 31)
        txtGAdd1.TabIndex = 43
        ' 
        ' Label116
        ' 
        Label116.ForeColor = Color.Fuchsia
        Label116.Location = New Point(40, 219)
        Label116.Margin = New Padding(5, 0, 5, 0)
        Label116.Name = "Label116"
        Label116.Size = New Size(178, 25)
        Label116.TabIndex = 78
        Label116.Text = "Address Line 1"
        ' 
        ' lblGMName
        ' 
        lblGMName.ForeColor = Color.DarkBlue
        lblGMName.Location = New Point(742, 22)
        lblGMName.Margin = New Padding(5, 0, 5, 0)
        lblGMName.Name = "lblGMName"
        lblGMName.Size = New Size(115, 25)
        lblGMName.TabIndex = 72
        lblGMName.Text = "Middle Name"
        ' 
        ' txtGMName
        ' 
        txtGMName.Location = New Point(746, 58)
        txtGMName.Margin = New Padding(5, 6, 5, 6)
        txtGMName.MaxLength = 35
        txtGMName.Name = "txtGMName"
        txtGMName.Size = New Size(108, 31)
        txtGMName.TabIndex = 36
        ' 
        ' lblGDOB
        ' 
        lblGDOB.ForeColor = Color.Fuchsia
        lblGDOB.Location = New Point(469, 122)
        lblGDOB.Margin = New Padding(5, 0, 5, 0)
        lblGDOB.Name = "lblGDOB"
        lblGDOB.Size = New Size(60, 25)
        lblGDOB.TabIndex = 75
        lblGDOB.Text = "D.O.B"
        ' 
        ' Label50
        ' 
        Label50.Location = New Point(611, 11)
        Label50.Margin = New Padding(5, 0, 5, 0)
        Label50.Name = "Label50"
        Label50.Size = New Size(131, 28)
        Label50.TabIndex = 6
        Label50.Text = "Due Amount"
        Label50.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label49
        ' 
        Label49.ForeColor = Color.Fuchsia
        Label49.Location = New Point(171, 11)
        Label49.Margin = New Padding(5, 0, 5, 0)
        Label49.Name = "Label49"
        Label49.Size = New Size(286, 28)
        Label49.TabIndex = 5
        Label49.Text = "Primary Responsible Payer"
        ' 
        ' Label48
        ' 
        Label48.Location = New Point(16, 8)
        Label48.Margin = New Padding(5, 0, 5, 0)
        Label48.Name = "Label48"
        Label48.Size = New Size(122, 28)
        Label48.TabIndex = 4
        Label48.Text = "Service"
        ' 
        ' btnPmnt
        ' 
        btnPmnt.Location = New Point(764, 41)
        btnPmnt.Margin = New Padding(5, 6, 5, 6)
        btnPmnt.Name = "btnPmnt"
        btnPmnt.Size = New Size(125, 47)
        btnPmnt.TabIndex = 6
        btnPmnt.TabStop = False
        btnPmnt.Text = "Payment"
        btnPmnt.UseVisualStyleBackColor = True
        ' 
        ' txtCopay
        ' 
        txtCopay.Location = New Point(602, 47)
        txtCopay.Margin = New Padding(5, 6, 5, 6)
        txtCopay.Name = "txtCopay"
        txtCopay.ReadOnly = True
        txtCopay.Size = New Size(139, 31)
        txtCopay.TabIndex = 4
        txtCopay.TextAlign = HorizontalAlignment.Center
        ' 
        ' chkSvcGratis
        ' 
        chkSvcGratis.Appearance = Appearance.Button
        chkSvcGratis.Location = New Point(14, 41)
        chkSvcGratis.Margin = New Padding(5, 6, 5, 6)
        chkSvcGratis.Name = "chkSvcGratis"
        chkSvcGratis.Size = New Size(118, 64)
        chkSvcGratis.TabIndex = 1
        chkSvcGratis.TabStop = False
        chkSvcGratis.Text = "Charge"
        chkSvcGratis.TextAlign = ContentAlignment.MiddleCenter
        chkSvcGratis.UseVisualStyleBackColor = True
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(barcode)
        TabPage1.Controls.Add(SendTocolorBtn)
        TabPage1.Controls.Add(QrChk)
        TabPage1.Location = New Point(4, 34)
        TabPage1.Margin = New Padding(4, 3, 4, 3)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(4, 3, 4, 3)
        TabPage1.Size = New Size(1342, 571)
        TabPage1.TabIndex = 6
        TabPage1.Text = "Misc"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' barcode
        ' 
        barcode.AcceptsReturn = True
        barcode.Font = New Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        barcode.Location = New Point(15, 28)
        barcode.Margin = New Padding(5, 6, 5, 6)
        barcode.MaxLength = 25
        barcode.Multiline = True
        barcode.Name = "barcode"
        barcode.Size = New Size(320, 48)
        barcode.TabIndex = 48
        ' 
        ' SendTocolorBtn
        ' 
        SendTocolorBtn.Location = New Point(346, 28)
        SendTocolorBtn.Margin = New Padding(5, 6, 5, 6)
        SendTocolorBtn.Name = "SendTocolorBtn"
        SendTocolorBtn.Size = New Size(191, 47)
        SendTocolorBtn.TabIndex = 46
        SendTocolorBtn.Text = "Send to  Color"
        SendTocolorBtn.UseVisualStyleBackColor = True
        ' 
        ' QrChk
        ' 
        QrChk.ForeColor = Color.DarkBlue
        QrChk.Location = New Point(558, 33)
        QrChk.Margin = New Padding(5, 6, 5, 6)
        QrChk.Name = "QrChk"
        QrChk.Size = New Size(190, 53)
        QrChk.TabIndex = 47
        QrChk.Text = "QR Required?"
        QrChk.TextImageRelation = TextImageRelation.TextAboveImage
        QrChk.UseVisualStyleBackColor = True
        ' 
        ' lblSpecimen
        ' 
        lblSpecimen.BackColor = Color.PeachPuff
        lblSpecimen.BorderStyle = BorderStyle.FixedSingle
        lblSpecimen.Font = New Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblSpecimen.Location = New Point(398, 72)
        lblSpecimen.Margin = New Padding(5, 0, 5, 0)
        lblSpecimen.Name = "lblSpecimen"
        lblSpecimen.Size = New Size(117, 35)
        lblSpecimen.TabIndex = 14
        lblSpecimen.Text = "Specimen"
        lblSpecimen.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblOrderer
        ' 
        lblOrderer.BackColor = Color.PeachPuff
        lblOrderer.BorderStyle = BorderStyle.FixedSingle
        lblOrderer.Font = New Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblOrderer.Location = New Point(516, 72)
        lblOrderer.Margin = New Padding(5, 0, 5, 0)
        lblOrderer.Name = "lblOrderer"
        lblOrderer.Size = New Size(117, 35)
        lblOrderer.TabIndex = 15
        lblOrderer.Text = "Client"
        lblOrderer.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblPatient
        ' 
        lblPatient.BackColor = Color.PeachPuff
        lblPatient.BorderStyle = BorderStyle.FixedSingle
        lblPatient.Font = New Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblPatient.Location = New Point(635, 72)
        lblPatient.Margin = New Padding(5, 0, 5, 0)
        lblPatient.Name = "lblPatient"
        lblPatient.Size = New Size(117, 35)
        lblPatient.TabIndex = 16
        lblPatient.Text = "Patient"
        lblPatient.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblBilling
        ' 
        lblBilling.BackColor = Color.PeachPuff
        lblBilling.BorderStyle = BorderStyle.FixedSingle
        lblBilling.Font = New Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblBilling.Location = New Point(990, 72)
        lblBilling.Margin = New Padding(5, 0, 5, 0)
        lblBilling.Name = "lblBilling"
        lblBilling.Size = New Size(117, 35)
        lblBilling.TabIndex = 17
        lblBilling.Text = "Billing"
        lblBilling.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblOrders
        ' 
        lblOrders.BackColor = Color.PeachPuff
        lblOrders.BorderStyle = BorderStyle.FixedSingle
        lblOrders.Font = New Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblOrders.Location = New Point(754, 72)
        lblOrders.Margin = New Padding(5, 0, 5, 0)
        lblOrders.Name = "lblOrders"
        lblOrders.Size = New Size(117, 35)
        lblOrders.TabIndex = 18
        lblOrders.Text = "Orders"
        lblOrders.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblReports
        ' 
        lblReports.BackColor = Color.PeachPuff
        lblReports.BorderStyle = BorderStyle.FixedSingle
        lblReports.Font = New Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblReports.Location = New Point(871, 72)
        lblReports.Margin = New Padding(5, 0, 5, 0)
        lblReports.Name = "lblReports"
        lblReports.Size = New Size(117, 35)
        lblReports.TabIndex = 19
        lblReports.Text = "Reports"
        lblReports.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblRequisition
        ' 
        lblRequisition.BackColor = Color.PeachPuff
        lblRequisition.BorderStyle = BorderStyle.FixedSingle
        lblRequisition.Font = New Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblRequisition.Location = New Point(280, 72)
        lblRequisition.Margin = New Padding(5, 0, 5, 0)
        lblRequisition.Name = "lblRequisition"
        lblRequisition.Size = New Size(117, 35)
        lblRequisition.TabIndex = 20
        lblRequisition.Text = "Requisition"
        lblRequisition.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' btnSupportive
        ' 
        btnSupportive.Image = CType(resources.GetObject("btnSupportive.Image"), Image)
        btnSupportive.ImageAlign = ContentAlignment.MiddleRight
        btnSupportive.Location = New Point(1171, 275)
        btnSupportive.Margin = New Padding(5, 6, 5, 6)
        btnSupportive.Name = "btnSupportive"
        btnSupportive.Size = New Size(166, 50)
        btnSupportive.TabIndex = 37
        btnSupportive.Text = "Supportive"
        btnSupportive.TextAlign = ContentAlignment.MiddleLeft
        btnSupportive.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(btnSupportive, "Associates the paper Requisitions")
        btnSupportive.UseVisualStyleBackColor = True
        ' 
        ' chkReject
        ' 
        chkReject.Appearance = Appearance.Button
        chkReject.Location = New Point(38, 347)
        chkReject.Margin = New Padding(5, 6, 5, 6)
        chkReject.Name = "chkReject"
        chkReject.Size = New Size(170, 44)
        chkReject.TabIndex = 38
        chkReject.Text = "Accepted"
        chkReject.TextAlign = ContentAlignment.MiddleCenter
        ToolTip1.SetToolTip(chkReject, "Click to reject the sample. and enter the reason in the next appearing field.")
        chkReject.UseVisualStyleBackColor = True
        ' 
        ' btnAccLook
        ' 
        btnAccLook.Enabled = False
        btnAccLook.Image = CType(resources.GetObject("btnAccLook.Image"), Image)
        btnAccLook.Location = New Point(224, 278)
        btnAccLook.Margin = New Padding(5, 6, 5, 6)
        btnAccLook.Name = "btnAccLook"
        btnAccLook.Size = New Size(50, 50)
        btnAccLook.TabIndex = 12
        btnAccLook.TabStop = False
        btnAccLook.UseVisualStyleBackColor = True
        ' 
        ' txtAnalysisStage
        ' 
        txtAnalysisStage.AcceptsReturn = True
        txtAnalysisStage.ForeColor = Color.DarkRed
        txtAnalysisStage.Location = New Point(1322, 231)
        txtAnalysisStage.Margin = New Padding(5, 6, 5, 6)
        txtAnalysisStage.MaxLength = 60
        txtAnalysisStage.Name = "txtAnalysisStage"
        txtAnalysisStage.ReadOnly = True
        txtAnalysisStage.Size = New Size(14, 31)
        txtAnalysisStage.TabIndex = 23
        txtAnalysisStage.Visible = False
        ' 
        ' txtComment
        ' 
        txtComment.AcceptsReturn = True
        txtComment.Location = New Point(58, 434)
        txtComment.Margin = New Padding(5, 6, 5, 6)
        txtComment.MaxLength = 960
        txtComment.Multiline = True
        txtComment.Name = "txtComment"
        txtComment.ScrollBars = ScrollBars.Vertical
        txtComment.Size = New Size(669, 151)
        txtComment.TabIndex = 17
        txtComment.TabStop = False
        ' 
        ' Label21
        ' 
        Label21.ForeColor = Color.DarkBlue
        Label21.Location = New Point(78, 397)
        Label21.Margin = New Padding(5, 0, 5, 0)
        Label21.Name = "Label21"
        Label21.Size = New Size(215, 33)
        Label21.TabIndex = 30
        Label21.Text = "Accession Comment"
        ' 
        ' txtWorkCmnt
        ' 
        txtWorkCmnt.AcceptsReturn = True
        txtWorkCmnt.Location = New Point(754, 431)
        txtWorkCmnt.Margin = New Padding(5, 6, 5, 6)
        txtWorkCmnt.MaxLength = 960
        txtWorkCmnt.Multiline = True
        txtWorkCmnt.Name = "txtWorkCmnt"
        txtWorkCmnt.ScrollBars = ScrollBars.Vertical
        txtWorkCmnt.Size = New Size(602, 157)
        txtWorkCmnt.TabIndex = 18
        txtWorkCmnt.TabStop = False
        ' 
        ' Label20
        ' 
        Label20.ForeColor = Color.DarkBlue
        Label20.Location = New Point(765, 397)
        Label20.Margin = New Padding(5, 0, 5, 0)
        Label20.Name = "Label20"
        Label20.Size = New Size(191, 33)
        Label20.TabIndex = 32
        Label20.Text = "Worksheet Comment"
        ' 
        ' ProlisHelp
        ' 
        ProlisHelp.HelpNamespace = "prolishelp.chm"
        ' 
        ' grpSearch
        ' 
        grpSearch.Controls.Add(btnFirst)
        grpSearch.Controls.Add(btnPrevious)
        grpSearch.Controls.Add(btnNext)
        grpSearch.Controls.Add(btnLast)
        grpSearch.Controls.Add(txtNavStatus)
        grpSearch.Controls.Add(btnLoad)
        grpSearch.Controls.Add(txtAccTo)
        grpSearch.Controls.Add(txtAccFrom)
        grpSearch.Controls.Add(Label103)
        grpSearch.Controls.Add(Label104)
        grpSearch.Controls.Add(Label102)
        grpSearch.Controls.Add(txtDateTo)
        grpSearch.Controls.Add(Label23)
        grpSearch.Controls.Add(Label22)
        grpSearch.Controls.Add(txtDateFrom)
        grpSearch.Enabled = False
        grpSearch.Location = New Point(16, 114)
        grpSearch.Margin = New Padding(5, 6, 5, 6)
        grpSearch.Name = "grpSearch"
        grpSearch.Padding = New Padding(5, 6, 5, 6)
        grpSearch.Size = New Size(1350, 103)
        grpSearch.TabIndex = 0
        grpSearch.TabStop = False
        ' 
        ' btnFirst
        ' 
        btnFirst.Enabled = False
        btnFirst.Image = CType(resources.GetObject("btnFirst.Image"), Image)
        btnFirst.Location = New Point(806, 39)
        btnFirst.Margin = New Padding(5, 6, 5, 6)
        btnFirst.Name = "btnFirst"
        btnFirst.Size = New Size(50, 50)
        btnFirst.TabIndex = 9
        btnFirst.TabStop = False
        btnFirst.UseVisualStyleBackColor = True
        ' 
        ' btnPrevious
        ' 
        btnPrevious.Enabled = False
        btnPrevious.Image = CType(resources.GetObject("btnPrevious.Image"), Image)
        btnPrevious.Location = New Point(865, 39)
        btnPrevious.Margin = New Padding(5, 6, 5, 6)
        btnPrevious.Name = "btnPrevious"
        btnPrevious.Size = New Size(49, 50)
        btnPrevious.TabIndex = 8
        btnPrevious.TabStop = False
        btnPrevious.UseVisualStyleBackColor = True
        ' 
        ' btnNext
        ' 
        btnNext.Enabled = False
        btnNext.Image = CType(resources.GetObject("btnNext.Image"), Image)
        btnNext.Location = New Point(1204, 39)
        btnNext.Margin = New Padding(5, 6, 5, 6)
        btnNext.Name = "btnNext"
        btnNext.Size = New Size(54, 50)
        btnNext.TabIndex = 6
        btnNext.TabStop = False
        btnNext.UseVisualStyleBackColor = True
        ' 
        ' btnLast
        ' 
        btnLast.Enabled = False
        btnLast.Image = CType(resources.GetObject("btnLast.Image"), Image)
        btnLast.Location = New Point(1266, 39)
        btnLast.Margin = New Padding(5, 6, 5, 6)
        btnLast.Name = "btnLast"
        btnLast.Size = New Size(55, 50)
        btnLast.TabIndex = 7
        btnLast.TabStop = False
        btnLast.UseVisualStyleBackColor = True
        ' 
        ' txtNavStatus
        ' 
        txtNavStatus.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtNavStatus.Location = New Point(924, 47)
        txtNavStatus.Margin = New Padding(5, 6, 5, 6)
        txtNavStatus.Name = "txtNavStatus"
        txtNavStatus.ReadOnly = True
        txtNavStatus.Size = New Size(268, 26)
        txtNavStatus.TabIndex = 10
        txtNavStatus.TabStop = False
        txtNavStatus.TextAlign = HorizontalAlignment.Center
        ' 
        ' btnLoad
        ' 
        btnLoad.Image = CType(resources.GetObject("btnLoad.Image"), Image)
        btnLoad.Location = New Point(665, 39)
        btnLoad.Margin = New Padding(5, 6, 5, 6)
        btnLoad.Name = "btnLoad"
        btnLoad.Size = New Size(122, 50)
        btnLoad.TabIndex = 5
        btnLoad.UseVisualStyleBackColor = True
        ' 
        ' txtAccTo
        ' 
        txtAccTo.Location = New Point(506, 47)
        txtAccTo.Margin = New Padding(5, 6, 5, 6)
        txtAccTo.MaxLength = 12
        txtAccTo.Name = "txtAccTo"
        txtAccTo.Size = New Size(133, 31)
        txtAccTo.TabIndex = 4
        txtAccTo.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtAccFrom
        ' 
        txtAccFrom.Location = New Point(362, 47)
        txtAccFrom.Margin = New Padding(5, 6, 5, 6)
        txtAccFrom.MaxLength = 12
        txtAccFrom.Name = "txtAccFrom"
        txtAccFrom.Size = New Size(133, 31)
        txtAccFrom.TabIndex = 3
        txtAccFrom.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label103
        ' 
        Label103.ForeColor = Color.Magenta
        Label103.Location = New Point(506, 16)
        Label103.Margin = New Padding(5, 0, 5, 0)
        Label103.Name = "Label103"
        Label103.Size = New Size(109, 25)
        Label103.TabIndex = 8
        Label103.Text = "Acc To"
        ' 
        ' Label104
        ' 
        Label104.ForeColor = Color.Red
        Label104.Location = New Point(362, 16)
        Label104.Margin = New Padding(5, 0, 5, 0)
        Label104.Name = "Label104"
        Label104.Size = New Size(135, 25)
        Label104.TabIndex = 7
        Label104.Text = "Acc From"
        Label104.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label102
        ' 
        Label102.ForeColor = Color.DarkBlue
        Label102.Location = New Point(300, 36)
        Label102.Margin = New Padding(5, 0, 5, 0)
        Label102.Name = "Label102"
        Label102.Size = New Size(51, 53)
        Label102.TabIndex = 6
        Label102.Text = "OR"
        Label102.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' txtDateTo
        ' 
        txtDateTo.Location = New Point(165, 47)
        txtDateTo.Margin = New Padding(5, 6, 5, 6)
        txtDateTo.Mask = "00/00/0000"
        txtDateTo.Name = "txtDateTo"
        txtDateTo.Size = New Size(130, 31)
        txtDateTo.TabIndex = 2
        txtDateTo.ValidatingType = GetType(Date)
        ' 
        ' Label23
        ' 
        Label23.ForeColor = Color.Magenta
        Label23.Location = New Point(180, 16)
        Label23.Margin = New Padding(5, 0, 5, 0)
        Label23.Name = "Label23"
        Label23.Size = New Size(110, 25)
        Label23.TabIndex = 4
        Label23.Text = "Date To"
        ' 
        ' Label22
        ' 
        Label22.ForeColor = Color.Red
        Label22.Location = New Point(16, 16)
        Label22.Margin = New Padding(5, 0, 5, 0)
        Label22.Name = "Label22"
        Label22.Size = New Size(122, 25)
        Label22.TabIndex = 3
        Label22.Text = "Date From"
        Label22.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' txtDateFrom
        ' 
        txtDateFrom.Location = New Point(22, 47)
        txtDateFrom.Margin = New Padding(5, 6, 5, 6)
        txtDateFrom.Mask = "00/00/0000"
        txtDateFrom.Name = "txtDateFrom"
        txtDateFrom.Size = New Size(130, 31)
        txtDateFrom.TabIndex = 1
        txtDateFrom.ValidatingType = GetType(Date)
        ' 
        ' txtTime
        ' 
        txtTime.Location = New Point(645, 286)
        txtTime.Margin = New Padding(5, 6, 5, 6)
        txtTime.Mask = "00:00"
        txtTime.Name = "txtTime"
        txtTime.Size = New Size(82, 31)
        txtTime.TabIndex = 15
        txtTime.ValidatingType = GetType(Date)
        ' 
        ' chkInHouse
        ' 
        chkInHouse.Appearance = Appearance.Button
        chkInHouse.Checked = True
        chkInHouse.CheckState = CheckState.Checked
        chkInHouse.Location = New Point(995, 278)
        chkInHouse.Margin = New Padding(5, 6, 5, 6)
        chkInHouse.Name = "chkInHouse"
        chkInHouse.Size = New Size(140, 47)
        chkInHouse.TabIndex = 33
        chkInHouse.TabStop = False
        chkInHouse.Text = "Yes"
        chkInHouse.TextAlign = ContentAlignment.MiddleCenter
        chkInHouse.UseVisualStyleBackColor = True
        ' 
        ' Label24
        ' 
        Label24.ForeColor = Color.DarkBlue
        Label24.Location = New Point(995, 247)
        Label24.Margin = New Padding(5, 0, 5, 0)
        Label24.Name = "Label24"
        Label24.Size = New Size(135, 22)
        Label24.TabIndex = 34
        Label24.Text = "In House ?"
        Label24.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtInEditReason
        ' 
        txtInEditReason.Location = New Point(1010, 391)
        txtInEditReason.Margin = New Padding(5, 6, 5, 6)
        txtInEditReason.MaxLength = 250
        txtInEditReason.Name = "txtInEditReason"
        txtInEditReason.Size = New Size(346, 31)
        txtInEditReason.TabIndex = 35
        txtInEditReason.Visible = False
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.ImageScalingSize = New Size(20, 20)
        StatusStrip1.Items.AddRange(New ToolStripItem() {lblStatus, PB})
        StatusStrip1.Location = New Point(0, 1245)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Padding = New Padding(2, 0, 24, 0)
        StatusStrip1.Size = New Size(1386, 52)
        StatusStrip1.TabIndex = 36
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = False
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(120, 45)
        ' 
        ' PB
        ' 
        PB.AutoSize = False
        PB.Name = "PB"
        PB.Size = New Size(1134, 44)
        ' 
        ' txtRejectReason
        ' 
        txtRejectReason.BackColor = Color.Ivory
        txtRejectReason.Location = New Point(322, 347)
        txtRejectReason.Margin = New Padding(5, 6, 5, 6)
        txtRejectReason.Name = "txtRejectReason"
        txtRejectReason.Size = New Size(1034, 31)
        txtRejectReason.TabIndex = 39
        txtRejectReason.Visible = False
        ' 
        ' lblRejectReason
        ' 
        lblRejectReason.ForeColor = Color.Red
        lblRejectReason.Location = New Point(224, 356)
        lblRejectReason.Margin = New Padding(5, 0, 5, 0)
        lblRejectReason.Name = "lblRejectReason"
        lblRejectReason.Size = New Size(91, 25)
        lblRejectReason.TabIndex = 40
        lblRejectReason.Text = "Reason:"
        lblRejectReason.TextAlign = ContentAlignment.TopRight
        lblRejectReason.Visible = False
        ' 
        ' frmRequisitions
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        AutoScroll = True
        ClientSize = New Size(1386, 1297)
        Controls.Add(lblRejectReason)
        Controls.Add(txtRejectReason)
        Controls.Add(chkReject)
        Controls.Add(btnSupportive)
        Controls.Add(StatusStrip1)
        Controls.Add(txtInEditReason)
        Controls.Add(Label24)
        Controls.Add(chkInHouse)
        Controls.Add(txtTime)
        Controls.Add(grpSearch)
        Controls.Add(Label20)
        Controls.Add(txtWorkCmnt)
        Controls.Add(Label21)
        Controls.Add(txtComment)
        Controls.Add(txtAnalysisStage)
        Controls.Add(lblRequisition)
        Controls.Add(lblReports)
        Controls.Add(lblOrders)
        Controls.Add(lblBilling)
        Controls.Add(lblPatient)
        Controls.Add(lblOrderer)
        Controls.Add(lblSpecimen)
        Controls.Add(txtAccID)
        Controls.Add(Label1)
        Controls.Add(Label5)
        Controls.Add(TabControl1)
        Controls.Add(btnAccLook)
        Controls.Add(ToolStrip1)
        Controls.Add(cmbSpecimenType)
        Controls.Add(lblAccDate)
        Controls.Add(txtRequisition)
        Controls.Add(dtpDate)
        Controls.Add(Label2)
        Controls.Add(Label4)
        ProlisHelp.SetHelpKeyword(Me, "html\hs1000.htm")
        ProlisHelp.SetHelpNavigator(Me, HelpNavigator.Topic)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimumSize = New Size(1203, 1031)
        Name = "frmRequisitions"
        ProlisHelp.SetShowHelp(Me, True)
        StartPosition = FormStartPosition.CenterParent
        Text = "Requisition Management"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        TabControl1.ResumeLayout(False)
        tpSpecimen.ResumeLayout(False)
        tpSpecimen.PerformLayout()
        CType(dgvSources, ComponentModel.ISupportInitialize).EndInit()
        tpOrderer.ResumeLayout(False)
        tpOrderer.PerformLayout()
        grpClient.ResumeLayout(False)
        grpClient.PerformLayout()
        tpPatient.ResumeLayout(False)
        grpPatient.ResumeLayout(False)
        grpPatient.PerformLayout()
        tcDxMeds.ResumeLayout(False)
        tpCodes.ResumeLayout(False)
        PanelDX.ResumeLayout(False)
        CType(dgvDxs, ComponentModel.ISupportInitialize).EndInit()
        tpMeds.ResumeLayout(False)
        PanelMedi.ResumeLayout(False)
        CType(dgvMeds, ComponentModel.ISupportInitialize).EndInit()
        gbVeterinary.ResumeLayout(False)
        tpOrders.ResumeLayout(False)
        tpOrders.PerformLayout()
        CType(dgvTGPMarked, ComponentModel.ISupportInitialize).EndInit()
        tpReports.ResumeLayout(False)
        gbReports.ResumeLayout(False)
        gbReports.PerformLayout()
        CType(dgvRptProviders, ComponentModel.ISupportInitialize).EndInit()
        tpBilling.ResumeLayout(False)
        tpBilling.PerformLayout()
        TabControl2.ResumeLayout(False)
        tpPrimary.ResumeLayout(False)
        grpPSubs.ResumeLayout(False)
        grpPSubs.PerformLayout()
        grpPrimary.ResumeLayout(False)
        grpPrimary.PerformLayout()
        tpSecondary.ResumeLayout(False)
        grpSSubs.ResumeLayout(False)
        grpSSubs.PerformLayout()
        grpSecondary.ResumeLayout(False)
        grpSecondary.PerformLayout()
        tpGuarantor.ResumeLayout(False)
        tpGuarantor.PerformLayout()
        TabPage1.ResumeLayout(False)
        TabPage1.PerformLayout()
        grpSearch.ResumeLayout(False)
        grpSearch.PerformLayout()
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
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
    Friend WithEvents txtAccID As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtRequisition As System.Windows.Forms.TextBox
    Friend WithEvents btnAccLook As System.Windows.Forms.Button
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblAccDate As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbSpecimenType As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tpSpecimen As System.Windows.Forms.TabPage
    Friend WithEvents tpOrderer As System.Windows.Forms.TabPage
    Friend WithEvents lblSpecimen As System.Windows.Forms.Label
    Friend WithEvents lblOrderer As System.Windows.Forms.Label
    Friend WithEvents lblPatient As System.Windows.Forms.Label
    Friend WithEvents lblBilling As System.Windows.Forms.Label
    Friend WithEvents tpPatient As System.Windows.Forms.TabPage
    Friend WithEvents tpBilling As System.Windows.Forms.TabPage
    Friend WithEvents tpOrders As System.Windows.Forms.TabPage
    Friend WithEvents tpReports As System.Windows.Forms.TabPage
    Friend WithEvents lblOrders As System.Windows.Forms.Label
    Friend WithEvents lblReports As System.Windows.Forms.Label
    Friend WithEvents lblRequisition As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtpDateDrawn As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnRemSrc As System.Windows.Forms.Button
    Friend WithEvents dgvSources As System.Windows.Forms.DataGridView
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnAddSrc As System.Windows.Forms.Button
    Friend WithEvents btnRemAllSrc As System.Windows.Forms.Button
    Friend WithEvents cmbTemp As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtSrcComment As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents chkIsReady As System.Windows.Forms.CheckBox
    Friend WithEvents btnSources As System.Windows.Forms.Button
    Friend WithEvents cmbSource As System.Windows.Forms.ComboBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtAnalysisStage As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtPatEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtPatCity As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtPatAdr1 As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtLName As System.Windows.Forms.TextBox
    Friend WithEvents btnPatLook As System.Windows.Forms.Button
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtPatientID As System.Windows.Forms.TextBox
    Friend WithEvents txtFName As System.Windows.Forms.TextBox
    Friend WithEvents txtMName As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtSSN As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents btnRemPat As System.Windows.Forms.Button
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents txtDOB As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cmbSex As System.Windows.Forms.ComboBox
    Friend WithEvents grpPatient As System.Windows.Forms.GroupBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents dgvTGPMarked As System.Windows.Forms.DataGridView
    Friend WithEvents chkSvcGratis As System.Windows.Forms.CheckBox
    Friend WithEvents btnPmnt As System.Windows.Forms.Button
    Friend WithEvents txtCopay As System.Windows.Forms.TextBox
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
    Friend WithEvents tpPrimary As System.Windows.Forms.TabPage
    Friend WithEvents tpSecondary As System.Windows.Forms.TabPage
    Friend WithEvents grpPrimary As System.Windows.Forms.GroupBox
    Friend WithEvents txtPCopay As System.Windows.Forms.TextBox
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents cmbPRelation As System.Windows.Forms.ComboBox
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents txtPFrom As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnPIns As System.Windows.Forms.Button
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents txtPGroup As System.Windows.Forms.TextBox
    Friend WithEvents txtPTo As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents txtPPolicy As System.Windows.Forms.TextBox
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
    Friend WithEvents grpSecondary As System.Windows.Forms.GroupBox
    Friend WithEvents txtSCopay As System.Windows.Forms.TextBox
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents cmbSRelation As System.Windows.Forms.ComboBox
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents txtSFrom As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnSIns As System.Windows.Forms.Button
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents txtSGroup As System.Windows.Forms.TextBox
    Friend WithEvents txtSTo As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents txtSInsID As System.Windows.Forms.TextBox
    Friend WithEvents Label79 As System.Windows.Forms.Label
    Friend WithEvents grpSSubs As System.Windows.Forms.GroupBox
    Friend WithEvents Label80 As System.Windows.Forms.Label
    Friend WithEvents txtSSubSSN As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents txtSSubDOB As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cmbSSubSex As System.Windows.Forms.ComboBox
    Friend WithEvents btnSSubLook As System.Windows.Forms.Button
    Friend WithEvents Label82 As System.Windows.Forms.Label
    Friend WithEvents txtSSubCountry As System.Windows.Forms.TextBox
    Friend WithEvents Label83 As System.Windows.Forms.Label
    Friend WithEvents txtSSubEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents Label85 As System.Windows.Forms.Label
    Friend WithEvents txtSSubZip As System.Windows.Forms.TextBox
    Friend WithEvents Label86 As System.Windows.Forms.Label
    Friend WithEvents txtSSubState As System.Windows.Forms.TextBox
    Friend WithEvents Label87 As System.Windows.Forms.Label
    Friend WithEvents txtSSubCity As System.Windows.Forms.TextBox
    Friend WithEvents Label88 As System.Windows.Forms.Label
    Friend WithEvents txtSSubAdd2 As System.Windows.Forms.TextBox
    Friend WithEvents Label89 As System.Windows.Forms.Label
    Friend WithEvents txtSSubAdd1 As System.Windows.Forms.TextBox
    Friend WithEvents Label90 As System.Windows.Forms.Label
    Friend WithEvents Label91 As System.Windows.Forms.Label
    Friend WithEvents txtSSubMName As System.Windows.Forms.TextBox
    Friend WithEvents Label92 As System.Windows.Forms.Label
    Friend WithEvents txtSSubFName As System.Windows.Forms.TextBox
    Friend WithEvents Label93 As System.Windows.Forms.Label
    Friend WithEvents txtSSubLName As System.Windows.Forms.TextBox
    Friend WithEvents Label94 As System.Windows.Forms.Label
    Friend WithEvents txtSSubID As System.Windows.Forms.TextBox
    Friend WithEvents btnCalculate As System.Windows.Forms.Button
    Friend WithEvents txtLabels As System.Windows.Forms.TextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents txtProvEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label97 As System.Windows.Forms.Label
    Friend WithEvents txtPatAdr2 As System.Windows.Forms.TextBox
    Friend WithEvents txtPatCountry As System.Windows.Forms.TextBox
    Friend WithEvents txtPatZip As System.Windows.Forms.TextBox
    Friend WithEvents txtPatState As System.Windows.Forms.TextBox
    Friend WithEvents Label100 As System.Windows.Forms.Label
    Friend WithEvents Label99 As System.Windows.Forms.Label
    Friend WithEvents Label98 As System.Windows.Forms.Label
    Friend WithEvents btnPatUpdate As System.Windows.Forms.Button
    Friend WithEvents chkFasting As System.Windows.Forms.CheckBox
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label101 As System.Windows.Forms.Label
    Friend WithEvents txtPayment As System.Windows.Forms.TextBox
    Friend WithEvents txtWorkCmnt As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents ProlisHelp As System.Windows.Forms.HelpProvider
    Friend WithEvents chkProfile As System.Windows.Forms.CheckBox
    Friend WithEvents lblChart As System.Windows.Forms.Label
    Friend WithEvents txtEMRNo As System.Windows.Forms.TextBox
    Friend WithEvents txtPatHPhone As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtPsubHPhone As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtSSubPhone As System.Windows.Forms.MaskedTextBox
    Friend WithEvents grpSearch As System.Windows.Forms.GroupBox
    Friend WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label102 As System.Windows.Forms.Label
    Friend WithEvents txtAccTo As System.Windows.Forms.TextBox
    Friend WithEvents txtAccFrom As System.Windows.Forms.TextBox
    Friend WithEvents Label103 As System.Windows.Forms.Label
    Friend WithEvents Label104 As System.Windows.Forms.Label
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents btnFirst As System.Windows.Forms.Button
    Friend WithEvents btnPrevious As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnLast As System.Windows.Forms.Button
    Friend WithEvents txtNavStatus As System.Windows.Forms.TextBox
    Friend WithEvents btn122 As System.Windows.Forms.Button
    Friend WithEvents btn120 As System.Windows.Forms.Button
    Friend WithEvents chkVerbal As System.Windows.Forms.CheckBox
    Friend WithEvents txtTime As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtDrawnTime As System.Windows.Forms.MaskedTextBox
    Friend WithEvents grpClient As System.Windows.Forms.GroupBox
    Friend WithEvents txtOrdFax As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtOrdPhone As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label96 As System.Windows.Forms.Label
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
    Friend WithEvents lstProviders As System.Windows.Forms.CheckedListBox
    Friend WithEvents chkHomeBound As System.Windows.Forms.CheckBox
    Friend WithEvents chkPhlebotomy As System.Windows.Forms.CheckBox
    Friend WithEvents SrcID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SrcNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SrcName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SrcQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SrcDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SrcTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SrcTemp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SrcComment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkCare As System.Windows.Forms.CheckBox
    Friend WithEvents btnSwitchCarriers As System.Windows.Forms.Button
    Friend WithEvents txtSInsName As System.Windows.Forms.TextBox
    Friend WithEvents txtSPolicy As System.Windows.Forms.TextBox
    Friend WithEvents txtPInsName As System.Windows.Forms.TextBox
    Friend WithEvents txtPInsID As System.Windows.Forms.TextBox
    Friend WithEvents Label105 As System.Windows.Forms.Label
    Friend WithEvents Label106 As System.Windows.Forms.Label
    Friend WithEvents chkInHouse As System.Windows.Forms.CheckBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtInEditReason As System.Windows.Forms.TextBox
    Friend WithEvents tpGuarantor As System.Windows.Forms.TabPage
    Friend WithEvents txtGPhone As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtGLName_BSN As System.Windows.Forms.TextBox
    Friend WithEvents lblGSSN As System.Windows.Forms.Label
    Friend WithEvents txtGSSN As System.Windows.Forms.MaskedTextBox
    Friend WithEvents lblGSex As System.Windows.Forms.Label
    Friend WithEvents txtGDOB As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cmbGSex As System.Windows.Forms.ComboBox
    Friend WithEvents btnGLookUp As System.Windows.Forms.Button
    Friend WithEvents Label109 As System.Windows.Forms.Label
    Friend WithEvents txtGCountry As System.Windows.Forms.TextBox
    Friend WithEvents Label110 As System.Windows.Forms.Label
    Friend WithEvents txtGEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label111 As System.Windows.Forms.Label
    Friend WithEvents Label112 As System.Windows.Forms.Label
    Friend WithEvents txtGZip As System.Windows.Forms.TextBox
    Friend WithEvents Label113 As System.Windows.Forms.Label
    Friend WithEvents txtGState As System.Windows.Forms.TextBox
    Friend WithEvents Label114 As System.Windows.Forms.Label
    Friend WithEvents txtGCity As System.Windows.Forms.TextBox
    Friend WithEvents Label115 As System.Windows.Forms.Label
    Friend WithEvents txtGAdd2 As System.Windows.Forms.TextBox
    Friend WithEvents Label116 As System.Windows.Forms.Label
    Friend WithEvents txtGAdd1 As System.Windows.Forms.TextBox
    Friend WithEvents lblGDOB As System.Windows.Forms.Label
    Friend WithEvents lblGMName As System.Windows.Forms.Label
    Friend WithEvents txtGMName As System.Windows.Forms.TextBox
    Friend WithEvents lblGFName As System.Windows.Forms.Label
    Friend WithEvents txtGFName As System.Windows.Forms.TextBox
    Friend WithEvents lblGLName As System.Windows.Forms.Label
    Friend WithEvents Label121 As System.Windows.Forms.Label
    Friend WithEvents txtGID As System.Windows.Forms.TextBox
    Friend WithEvents chkGIsIndividual As System.Windows.Forms.CheckBox
    Friend WithEvents cmbGRelation As System.Windows.Forms.ComboBox
    Friend WithEvents Label107 As System.Windows.Forms.Label
    Friend WithEvents rbP As System.Windows.Forms.RadioButton
    Friend WithEvents rbT As System.Windows.Forms.RadioButton
    Friend WithEvents rbC As System.Windows.Forms.RadioButton
    Friend WithEvents gbReports As System.Windows.Forms.GroupBox
    Friend WithEvents txtRPTFax As System.Windows.Forms.MaskedTextBox
    Friend WithEvents chkInterface As System.Windows.Forms.CheckBox
    Friend WithEvents chkProlison As System.Windows.Forms.CheckBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents chkRDMAuto As System.Windows.Forms.CheckBox
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents btnRptAdd As System.Windows.Forms.Button
    Friend WithEvents txtRptEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents chkRptFax As System.Windows.Forms.CheckBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents chkrptEmail As System.Windows.Forms.CheckBox
    Friend WithEvents chkPrint As System.Windows.Forms.CheckBox
    Friend WithEvents btnRefProf As System.Windows.Forms.Button
    Friend WithEvents txtRptRcptName As System.Windows.Forms.TextBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents btnRPTLookUp As System.Windows.Forms.Button
    Friend WithEvents chkRptComplete As System.Windows.Forms.CheckBox
    Friend WithEvents txtRptRcptID As System.Windows.Forms.TextBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents btnRemRpt As System.Windows.Forms.Button
    Friend WithEvents btnRemRptAll As System.Windows.Forms.Button
    Friend WithEvents dgvRptProviders As System.Windows.Forms.DataGridView
    Friend WithEvents Prov_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Prov_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RDMAuto As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents RCO As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Print As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Prolison As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents RDMInterface As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents chkFax As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Fax As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkEmail As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Email As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkPostPrePhleb As System.Windows.Forms.CheckBox
    Friend WithEvents txtRecTime As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label117 As System.Windows.Forms.Label
    Friend WithEvents Label108 As System.Windows.Forms.Label
    Friend WithEvents txtRecDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PB As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents txtRoom As System.Windows.Forms.TextBox
    Friend WithEvents Label118 As System.Windows.Forms.Label
    Friend WithEvents Label119 As System.Windows.Forms.Label
    Friend WithEvents cmbEthnicity As System.Windows.Forms.ComboBox
    Friend WithEvents txtPsubWPhone As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label120 As System.Windows.Forms.Label
    Friend WithEvents Label122 As System.Windows.Forms.Label
    Friend WithEvents txtCovCmnt As System.Windows.Forms.TextBox
    Friend WithEvents txtDOI As System.Windows.Forms.MaskedTextBox
    Friend WithEvents chkWorkman As System.Windows.Forms.CheckBox
    Friend WithEvents Label123 As System.Windows.Forms.Label
    Friend WithEvents txtCell As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label125 As System.Windows.Forms.Label
    Friend WithEvents txtWPhone As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label124 As System.Windows.Forms.Label
    Friend WithEvents txtFax As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label126 As System.Windows.Forms.Label
    Friend WithEvents btnSupportive As System.Windows.Forms.Button
    Friend WithEvents chkReject As System.Windows.Forms.CheckBox
    Friend WithEvents txtRejectReason As System.Windows.Forms.TextBox
    Friend WithEvents lblRejectReason As System.Windows.Forms.Label
    Friend WithEvents Label127 As System.Windows.Forms.Label
    Friend WithEvents txtTage As System.Windows.Forms.TextBox
    Friend WithEvents Label128 As System.Windows.Forms.Label
    Friend WithEvents txtGTage As System.Windows.Forms.TextBox
    Friend WithEvents cmbRace As System.Windows.Forms.ComboBox
    Friend WithEvents Label130 As System.Windows.Forms.Label
    Friend WithEvents gbVeterinary As System.Windows.Forms.GroupBox
    Friend WithEvents Label129 As System.Windows.Forms.Label
    Friend WithEvents cmbBreed As System.Windows.Forms.ComboBox
    Friend WithEvents lblSpecies As System.Windows.Forms.Label
    Friend WithEvents cmbSpecies As System.Windows.Forms.ComboBox
    Friend WithEvents tcDxMeds As System.Windows.Forms.TabControl
    Friend WithEvents tpCodes As System.Windows.Forms.TabPage
    Friend WithEvents PanelDX As System.Windows.Forms.Panel
    Friend WithEvents LoadDx As System.Windows.Forms.Button
    Friend WithEvents btnRemDxAll As System.Windows.Forms.Button
    Friend WithEvents dgvDxs As System.Windows.Forms.DataGridView
    Friend WithEvents tpMeds As System.Windows.Forms.TabPage
    Friend WithEvents PanelMedi As System.Windows.Forms.Panel
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents RemMedAll As System.Windows.Forms.Button
    Friend WithEvents dgvMeds As System.Windows.Forms.DataGridView
    Friend WithEvents MedName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MedLook As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents remm As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents printLabel As System.Windows.Forms.Button
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents barcode As System.Windows.Forms.TextBox
    Friend WithEvents SendTocolorBtn As System.Windows.Forms.Button
    Friend WithEvents QrChk As System.Windows.Forms.CheckBox
    Friend WithEvents Dx_Code As DataGridViewTextBoxColumn
    Friend WithEvents Lookup As DataGridViewImageColumn
    Friend WithEvents remd As DataGridViewImageColumn
    Friend WithEvents CompID As DataGridViewTextBoxColumn
    Friend WithEvents CompLook As DataGridViewImageColumn
    Friend WithEvents TGP As DataGridViewTextBoxColumn
    Friend WithEvents CompType As DataGridViewImageColumn
    Friend WithEvents Stat As DataGridViewCheckBoxColumn
    Friend WithEvents Verbal As DataGridViewCheckBoxColumn
    Friend WithEvents Outsource As DataGridViewCheckBoxColumn
End Class
