<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBillingDash
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBillingDash))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        cmbBillType = New ToolStripComboBox()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnSave = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        btnHelp = New ToolStripButton()
        TB = New TabControl()
        tpUnsynched = New TabPage()
        Button9 = New Button()
        chkLAE = New CheckBox()
        chkTNP = New CheckBox()
        chkQNS = New CheckBox()
        chkDot = New CheckBox()
        chkFinals = New CheckBox()
        txtUnsynchCount = New TextBox()
        Label3 = New Label()
        btnSynchronize = New Button()
        btnSelUs = New Button()
        btnDeselUS = New Button()
        dgvUnsynchAccs = New DataGridView()
        chkStS = New DataGridViewCheckBoxColumn()
        AccID = New DataGridViewTextBoxColumn()
        sPatient = New DataGridViewTextBoxColumn()
        RecDate = New DataGridViewTextBoxColumn()
        RepDate = New DataGridViewTextBoxColumn()
        Client = New DataGridViewTextBoxColumn()
        tpUnbilled = New TabPage()
        Button10 = New Button()
        chkValidate = New CheckBox()
        txtSynchCount = New TextBox()
        Label9 = New Label()
        dtpBillDate = New DateTimePicker()
        Label7 = New Label()
        dgvSynchAccs = New DataGridView()
        DataGridViewCheckBoxColumn1 = New DataGridViewCheckBoxColumn()
        AccIDS = New DataGridViewTextBoxColumn()
        PatientS = New DataGridViewTextBoxColumn()
        SvcDateS = New DataGridViewTextBoxColumn()
        AmountS = New DataGridViewTextBoxColumn()
        Billee = New DataGridViewTextBoxColumn()
        btnBill = New Button()
        btnUnsynch = New Button()
        btnSelS = New Button()
        btnDeselS = New Button()
        tpBilled = New TabPage()
        Button11 = New Button()
        chkFileless = New CheckBox()
        Label12 = New Label()
        cmbClearingHouse = New ComboBox()
        Label11 = New Label()
        chkNZero = New CheckBox()
        cmbDestination = New ComboBox()
        txtBCount = New TextBox()
        Label10 = New Label()
        dgvBilled = New DataGridView()
        DataGridViewCheckBoxColumn2 = New DataGridViewCheckBoxColumn()
        InvID = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn5 = New DataGridViewTextBoxColumn()
        PatientsB = New DataGridViewTextBoxColumn()
        SvcDateB = New DataGridViewTextBoxColumn()
        BillDateB = New DataGridViewTextBoxColumn()
        AmountB = New DataGridViewTextBoxColumn()
        Billees = New DataGridViewTextBoxColumn()
        Email = New DataGridViewTextBoxColumn()
        btnReverse = New Button()
        btnProcess = New Button()
        btnSelB = New Button()
        btnDeselB = New Button()
        tpProcessed = New TabPage()
        dgvProcPats = New DataGridView()
        PatCheck = New DataGridViewCheckBoxColumn()
        PatInVID = New DataGridViewTextBoxColumn()
        PatAccID = New DataGridViewTextBoxColumn()
        PatPatient = New DataGridViewTextBoxColumn()
        PatSDate = New DataGridViewTextBoxColumn()
        PatBDate = New DataGridViewTextBoxColumn()
        PatAmount = New DataGridViewTextBoxColumn()
        PatPrints = New DataGridViewTextBoxColumn()
        PatEmail = New DataGridViewTextBoxColumn()
        txtProcessed = New TextBox()
        Label16 = New Label()
        dgvProcTPs = New DataGridView()
        TPCheck = New DataGridViewCheckBoxColumn()
        FileNo = New DataGridViewTextBoxColumn()
        ClearingHouse = New DataGridViewTextBoxColumn()
        Created = New DataGridViewTextBoxColumn()
        ChCount = New DataGridViewTextBoxColumn()
        FileAmount = New DataGridViewTextBoxColumn()
        btnFileOutput = New Button()
        btnUnprocess = New Button()
        btnSelP = New Button()
        btnDeselP = New Button()
        Label5 = New Label()
        txtAccTo = New TextBox()
        Label4 = New Label()
        lblTo = New Label()
        txtAccFrom = New TextBox()
        lblFrom = New Label()
        txtOutput = New TextBox()
        StatusStrip1 = New StatusStrip()
        lblStatus = New ToolStripStatusLabel()
        PB = New ToolStripProgressBar()
        Label2 = New Label()
        lstTargets = New CheckedListBox()
        dgvDiscrete = New DataGridView()
        Discrete = New DataGridViewTextBoxColumn()
        Label1 = New Label()
        ToolTip1 = New ToolTip(components)
        ComboBox1 = New ComboBox()
        CheckBox1 = New CheckBox()
        ComboBox2 = New ComboBox()
        txt837File = New TextBox()
        btn837Lookup = New Button()
        btnSellT = New Button()
        btnDeselT = New Button()
        btnTarget = New Button()
        btnLoad = New Button()
        Button1 = New Button()
        Button2 = New Button()
        Button5 = New Button()
        Button6 = New Button()
        claimpath = New TextBox()
        BW = New ComponentModel.BackgroundWorker()
        Label8 = New Label()
        SaveFileDialog1 = New SaveFileDialog()
        PrintDialog1 = New PrintDialog()
        Label13 = New Label()
        Label14 = New Label()
        TextBox1 = New TextBox()
        Label15 = New Label()
        DataGridView1 = New DataGridView()
        DataGridViewCheckBoxColumn3 = New DataGridViewCheckBoxColumn()
        DataGridViewTextBoxColumn1 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn2 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn3 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn4 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn6 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn7 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn8 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn9 = New DataGridViewTextBoxColumn()
        Label17 = New Label()
        Label18 = New Label()
        OpenFileDialog1 = New OpenFileDialog()
        TextBox2 = New TextBox()
        Label19 = New Label()
        DataGridView2 = New DataGridView()
        DataGridViewCheckBoxColumn5 = New DataGridViewCheckBoxColumn()
        DataGridViewTextBoxColumn10 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn11 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn12 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn13 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn14 = New DataGridViewTextBoxColumn()
        clipboardMsg = New Label()
        Label34 = New Label()
        txtPatientID = New TextBox()
        btnPatLook = New Button()
        Button3 = New Button()
        Button4 = New Button()
        Button7 = New Button()
        Button8 = New Button()
        viewClaim = New Button()
        dtpDateTo = New DateTimePicker()
        lblClearDates = New Label()
        dtpDateFrom = New DateTimePicker()
        ToolStrip1.SuspendLayout()
        TB.SuspendLayout()
        tpUnsynched.SuspendLayout()
        CType(dgvUnsynchAccs, ComponentModel.ISupportInitialize).BeginInit()
        tpUnbilled.SuspendLayout()
        CType(dgvSynchAccs, ComponentModel.ISupportInitialize).BeginInit()
        tpBilled.SuspendLayout()
        CType(dgvBilled, ComponentModel.ISupportInitialize).BeginInit()
        tpProcessed.SuspendLayout()
        CType(dgvProcPats, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvProcTPs, ComponentModel.ISupportInitialize).BeginInit()
        StatusStrip1.SuspendLayout()
        CType(dgvDiscrete, ComponentModel.ISupportInitialize).BeginInit()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        CType(DataGridView2, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(20, 20)
        ToolStrip1.Items.AddRange(New ToolStripItem() {cmbBillType, ToolStripSeparator1, btnSave, ToolStripSeparator3, btnCancel, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(1502, 34)
        ToolStrip1.TabIndex = 4
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
        btnSave.Size = New Size(135, 29)
        btnSave.Text = "Save Output"
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
        ' TB
        ' 
        TB.Controls.Add(tpUnsynched)
        TB.Controls.Add(tpUnbilled)
        TB.Controls.Add(tpBilled)
        TB.Controls.Add(tpProcessed)
        TB.ItemSize = New Size(80, 18)
        TB.Location = New Point(17, 375)
        TB.Margin = New Padding(5, 6, 5, 6)
        TB.MinimumSize = New Size(1012, 879)
        TB.Name = "TB"
        TB.SelectedIndex = 0
        TB.Size = New Size(1012, 879)
        TB.TabIndex = 6
        ' 
        ' tpUnsynched
        ' 
        tpUnsynched.Controls.Add(Button9)
        tpUnsynched.Controls.Add(chkLAE)
        tpUnsynched.Controls.Add(chkTNP)
        tpUnsynched.Controls.Add(chkQNS)
        tpUnsynched.Controls.Add(chkDot)
        tpUnsynched.Controls.Add(chkFinals)
        tpUnsynched.Controls.Add(txtUnsynchCount)
        tpUnsynched.Controls.Add(Label3)
        tpUnsynched.Controls.Add(btnSynchronize)
        tpUnsynched.Controls.Add(btnSelUs)
        tpUnsynched.Controls.Add(btnDeselUS)
        tpUnsynched.Controls.Add(dgvUnsynchAccs)
        tpUnsynched.Location = New Point(4, 22)
        tpUnsynched.Margin = New Padding(5, 6, 5, 6)
        tpUnsynched.Name = "tpUnsynched"
        tpUnsynched.Padding = New Padding(5, 6, 5, 6)
        tpUnsynched.Size = New Size(1004, 853)
        tpUnsynched.TabIndex = 0
        tpUnsynched.Text = "Unsynched "
        tpUnsynched.UseVisualStyleBackColor = True
        ' 
        ' Button9
        ' 
        Button9.Location = New Point(10, 612)
        Button9.Margin = New Padding(3, 4, 3, 4)
        Button9.Name = "Button9"
        Button9.Size = New Size(127, 48)
        Button9.TabIndex = 31
        Button9.Text = "↓↓↓ CSV"
        Button9.UseVisualStyleBackColor = True
        ' 
        ' chkLAE
        ' 
        chkLAE.AutoSize = True
        chkLAE.Checked = True
        chkLAE.CheckState = CheckState.Checked
        chkLAE.Location = New Point(495, 731)
        chkLAE.Margin = New Padding(5, 6, 5, 6)
        chkLAE.Name = "chkLAE"
        chkLAE.Size = New Size(68, 29)
        chkLAE.TabIndex = 30
        chkLAE.Text = "LAE"
        ToolTip1.SetToolTip(chkLAE, "Click me to toggle between including or excluding the Lab Accidents while synchronizing.")
        chkLAE.UseVisualStyleBackColor = True
        ' 
        ' chkTNP
        ' 
        chkTNP.AutoSize = True
        chkTNP.Checked = True
        chkTNP.CheckState = CheckState.Checked
        chkTNP.Location = New Point(670, 731)
        chkTNP.Margin = New Padding(5, 6, 5, 6)
        chkTNP.Name = "chkTNP"
        chkTNP.Size = New Size(70, 29)
        chkTNP.TabIndex = 29
        chkTNP.Text = "TNP"
        ToolTip1.SetToolTip(chkTNP, "Click me to toggle between including or excluding the TNP (Test not performed) while synchronizing.")
        chkTNP.UseVisualStyleBackColor = True
        ' 
        ' chkQNS
        ' 
        chkQNS.AutoSize = True
        chkQNS.Checked = True
        chkQNS.CheckState = CheckState.Checked
        chkQNS.Location = New Point(670, 688)
        chkQNS.Margin = New Padding(5, 6, 5, 6)
        chkQNS.Name = "chkQNS"
        chkQNS.Size = New Size(75, 29)
        chkQNS.TabIndex = 28
        chkQNS.Text = "QNS"
        ToolTip1.SetToolTip(chkQNS, "Click me to toggle between including or excluding the QNS while synchronizing.")
        chkQNS.UseVisualStyleBackColor = True
        ' 
        ' chkDot
        ' 
        chkDot.AutoSize = True
        chkDot.Checked = True
        chkDot.CheckState = CheckState.Checked
        chkDot.Location = New Point(495, 688)
        chkDot.Margin = New Padding(5, 6, 5, 6)
        chkDot.Name = "chkDot"
        chkDot.Size = New Size(73, 29)
        chkDot.TabIndex = 27
        chkDot.Text = "DOT"
        ToolTip1.SetToolTip(chkDot, "Click me to toggle between including or excluding the dot suppress while synchronizing.")
        chkDot.UseVisualStyleBackColor = True
        ' 
        ' chkFinals
        ' 
        chkFinals.Appearance = Appearance.Button
        chkFinals.CheckAlign = ContentAlignment.MiddleRight
        chkFinals.Checked = True
        chkFinals.CheckState = CheckState.Checked
        chkFinals.Location = New Point(302, 702)
        chkFinals.Margin = New Padding(5, 6, 5, 6)
        chkFinals.Name = "chkFinals"
        chkFinals.Size = New Size(167, 48)
        chkFinals.TabIndex = 26
        chkFinals.Text = "FINALS"
        chkFinals.TextAlign = ContentAlignment.MiddleCenter
        ToolTip1.SetToolTip(chkFinals, "Click me to toggle between 'FINALS' and 'Ignore Results'")
        chkFinals.UseVisualStyleBackColor = True
        ' 
        ' txtUnsynchCount
        ' 
        txtUnsynchCount.Location = New Point(168, 706)
        txtUnsynchCount.Margin = New Padding(5, 6, 5, 6)
        txtUnsynchCount.Name = "txtUnsynchCount"
        txtUnsynchCount.ReadOnly = True
        txtUnsynchCount.Size = New Size(102, 31)
        txtUnsynchCount.TabIndex = 25
        txtUnsynchCount.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(168, 677)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(105, 25)
        Label3.TabIndex = 24
        Label3.Text = "Count"
        Label3.TextAlign = ContentAlignment.TopCenter
        ' 
        ' btnSynchronize
        ' 
        btnSynchronize.Enabled = False
        btnSynchronize.Image = CType(resources.GetObject("btnSynchronize.Image"), Image)
        btnSynchronize.Location = New Point(833, 687)
        btnSynchronize.Margin = New Padding(5, 6, 5, 6)
        btnSynchronize.Name = "btnSynchronize"
        btnSynchronize.Size = New Size(153, 77)
        btnSynchronize.TabIndex = 13
        btnSynchronize.Text = "Synchronize"
        btnSynchronize.TextAlign = ContentAlignment.MiddleRight
        btnSynchronize.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(btnSynchronize, "Click me to invoke the synchronization.")
        btnSynchronize.UseVisualStyleBackColor = True
        ' 
        ' btnSelUs
        ' 
        btnSelUs.ForeColor = Color.DarkBlue
        btnSelUs.Image = CType(resources.GetObject("btnSelUs.Image"), Image)
        btnSelUs.Location = New Point(30, 696)
        btnSelUs.Margin = New Padding(5, 6, 5, 6)
        btnSelUs.Name = "btnSelUs"
        btnSelUs.Size = New Size(50, 56)
        btnSelUs.TabIndex = 12
        btnSelUs.TextAlign = ContentAlignment.MiddleRight
        btnSelUs.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(btnSelUs, "Click me to select all of the displayed accessions.")
        btnSelUs.UseVisualStyleBackColor = True
        ' 
        ' btnDeselUS
        ' 
        btnDeselUS.ForeColor = Color.DarkBlue
        btnDeselUS.Image = CType(resources.GetObject("btnDeselUS.Image"), Image)
        btnDeselUS.Location = New Point(90, 696)
        btnDeselUS.Margin = New Padding(5, 6, 5, 6)
        btnDeselUS.Name = "btnDeselUS"
        btnDeselUS.Size = New Size(50, 56)
        btnDeselUS.TabIndex = 11
        btnDeselUS.TextAlign = ContentAlignment.MiddleRight
        btnDeselUS.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(btnDeselUS, "Click me to deselect all of the displayed accessions")
        btnDeselUS.UseVisualStyleBackColor = True
        ' 
        ' dgvUnsynchAccs
        ' 
        dgvUnsynchAccs.AllowUserToAddRows = False
        dgvUnsynchAccs.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.MintCream
        dgvUnsynchAccs.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvUnsynchAccs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvUnsynchAccs.Columns.AddRange(New DataGridViewColumn() {chkStS, AccID, sPatient, RecDate, RepDate, Client})
        dgvUnsynchAccs.Location = New Point(10, 6)
        dgvUnsynchAccs.Margin = New Padding(5, 6, 5, 6)
        dgvUnsynchAccs.Name = "dgvUnsynchAccs"
        dgvUnsynchAccs.RowHeadersVisible = False
        dgvUnsynchAccs.RowHeadersWidth = 62
        dgvUnsynchAccs.Size = New Size(983, 596)
        dgvUnsynchAccs.TabIndex = 0
        ' 
        ' chkStS
        ' 
        chkStS.FillWeight = 40F
        chkStS.HeaderText = ""
        chkStS.MinimumWidth = 8
        chkStS.Name = "chkStS"
        chkStS.Width = 70
        ' 
        ' AccID
        ' 
        AccID.FillWeight = 80F
        AccID.HeaderText = "Accession"
        AccID.MinimumWidth = 8
        AccID.Name = "AccID"
        AccID.ReadOnly = True
        AccID.Width = 140
        ' 
        ' sPatient
        ' 
        sPatient.FillWeight = 124F
        sPatient.HeaderText = "Patient (L, F)"
        sPatient.MinimumWidth = 8
        sPatient.Name = "sPatient"
        sPatient.ReadOnly = True
        sPatient.Width = 217
        ' 
        ' RecDate
        ' 
        RecDate.FillWeight = 80F
        RecDate.HeaderText = "Rec Date"
        RecDate.MinimumWidth = 8
        RecDate.Name = "RecDate"
        RecDate.ReadOnly = True
        RecDate.Width = 140
        ' 
        ' RepDate
        ' 
        RepDate.FillWeight = 80F
        RepDate.HeaderText = "Rep Date"
        RepDate.MinimumWidth = 8
        RepDate.Name = "RepDate"
        RepDate.ReadOnly = True
        RepDate.Width = 140
        ' 
        ' Client
        ' 
        Client.FillWeight = 156F
        Client.HeaderText = "Client"
        Client.MinimumWidth = 8
        Client.Name = "Client"
        Client.ReadOnly = True
        Client.Width = 273
        ' 
        ' tpUnbilled
        ' 
        tpUnbilled.Controls.Add(Button10)
        tpUnbilled.Controls.Add(chkValidate)
        tpUnbilled.Controls.Add(txtSynchCount)
        tpUnbilled.Controls.Add(Label9)
        tpUnbilled.Controls.Add(dtpBillDate)
        tpUnbilled.Controls.Add(Label7)
        tpUnbilled.Controls.Add(dgvSynchAccs)
        tpUnbilled.Controls.Add(btnBill)
        tpUnbilled.Controls.Add(btnUnsynch)
        tpUnbilled.Controls.Add(btnSelS)
        tpUnbilled.Controls.Add(btnDeselS)
        tpUnbilled.Location = New Point(4, 22)
        tpUnbilled.Margin = New Padding(5, 6, 5, 6)
        tpUnbilled.Name = "tpUnbilled"
        tpUnbilled.Padding = New Padding(5, 6, 5, 6)
        tpUnbilled.Size = New Size(1004, 853)
        tpUnbilled.TabIndex = 1
        tpUnbilled.Text = "Unbilled     "
        tpUnbilled.UseVisualStyleBackColor = True
        ' 
        ' Button10
        ' 
        Button10.Location = New Point(8, 610)
        Button10.Margin = New Padding(3, 4, 3, 4)
        Button10.Name = "Button10"
        Button10.Size = New Size(127, 48)
        Button10.TabIndex = 33
        Button10.Text = "↓↓↓ CSV"
        Button10.UseVisualStyleBackColor = True
        ' 
        ' chkValidate
        ' 
        chkValidate.CheckAlign = ContentAlignment.MiddleRight
        chkValidate.Checked = True
        chkValidate.CheckState = CheckState.Checked
        chkValidate.Location = New Point(470, 708)
        chkValidate.Margin = New Padding(5, 6, 5, 6)
        chkValidate.Name = "chkValidate"
        chkValidate.Size = New Size(125, 35)
        chkValidate.TabIndex = 32
        chkValidate.Text = "Validate ?"
        chkValidate.UseVisualStyleBackColor = True
        ' 
        ' txtSynchCount
        ' 
        txtSynchCount.Location = New Point(168, 706)
        txtSynchCount.Margin = New Padding(5, 6, 5, 6)
        txtSynchCount.Name = "txtSynchCount"
        txtSynchCount.ReadOnly = True
        txtSynchCount.Size = New Size(102, 31)
        txtSynchCount.TabIndex = 31
        txtSynchCount.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label9
        ' 
        Label9.ForeColor = Color.DarkBlue
        Label9.Location = New Point(168, 677)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(105, 25)
        Label9.TabIndex = 30
        Label9.Text = "Count"
        Label9.TextAlign = ContentAlignment.TopCenter
        ' 
        ' dtpBillDate
        ' 
        dtpBillDate.Format = DateTimePickerFormat.Custom
        dtpBillDate.Location = New Point(630, 708)
        dtpBillDate.Margin = New Padding(5, 6, 5, 6)
        dtpBillDate.Name = "dtpBillDate"
        dtpBillDate.Size = New Size(187, 31)
        dtpBillDate.TabIndex = 29
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.DarkBlue
        Label7.Location = New Point(643, 679)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(143, 25)
        Label7.TabIndex = 28
        Label7.Text = "Bill Date"
        ' 
        ' dgvSynchAccs
        ' 
        dgvSynchAccs.AllowUserToAddRows = False
        dgvSynchAccs.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = Color.Honeydew
        dgvSynchAccs.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        dgvSynchAccs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvSynchAccs.Columns.AddRange(New DataGridViewColumn() {DataGridViewCheckBoxColumn1, AccIDS, PatientS, SvcDateS, AmountS, Billee})
        dgvSynchAccs.Location = New Point(10, 6)
        dgvSynchAccs.Margin = New Padding(5, 6, 5, 6)
        dgvSynchAccs.Name = "dgvSynchAccs"
        dgvSynchAccs.RowHeadersVisible = False
        dgvSynchAccs.RowHeadersWidth = 62
        dgvSynchAccs.Size = New Size(983, 592)
        dgvSynchAccs.TabIndex = 17
        ' 
        ' DataGridViewCheckBoxColumn1
        ' 
        DataGridViewCheckBoxColumn1.FillWeight = 40F
        DataGridViewCheckBoxColumn1.HeaderText = ""
        DataGridViewCheckBoxColumn1.MinimumWidth = 8
        DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
        DataGridViewCheckBoxColumn1.Width = 40
        ' 
        ' AccIDS
        ' 
        AccIDS.FillWeight = 90F
        AccIDS.HeaderText = "Accession"
        AccIDS.MinimumWidth = 8
        AccIDS.Name = "AccIDS"
        AccIDS.ReadOnly = True
        AccIDS.Width = 90
        ' 
        ' PatientS
        ' 
        PatientS.FillWeight = 136F
        PatientS.HeaderText = "Patient Name (L, S)"
        PatientS.MinimumWidth = 8
        PatientS.Name = "PatientS"
        PatientS.ReadOnly = True
        PatientS.Width = 136
        ' 
        ' SvcDateS
        ' 
        SvcDateS.FillWeight = 80F
        SvcDateS.HeaderText = "Svc Date"
        SvcDateS.MinimumWidth = 8
        SvcDateS.Name = "SvcDateS"
        SvcDateS.ReadOnly = True
        SvcDateS.Width = 80
        ' 
        ' AmountS
        ' 
        AmountS.FillWeight = 80F
        AmountS.HeaderText = "Amount"
        AmountS.MinimumWidth = 8
        AmountS.Name = "AmountS"
        AmountS.ReadOnly = True
        AmountS.Width = 80
        ' 
        ' Billee
        ' 
        Billee.FillWeight = 144F
        Billee.HeaderText = "Billed To"
        Billee.MinimumWidth = 8
        Billee.Name = "Billee"
        Billee.Width = 144
        ' 
        ' btnBill
        ' 
        btnBill.Enabled = False
        btnBill.Image = CType(resources.GetObject("btnBill.Image"), Image)
        btnBill.Location = New Point(845, 696)
        btnBill.Margin = New Padding(5, 6, 5, 6)
        btnBill.Name = "btnBill"
        btnBill.Size = New Size(140, 56)
        btnBill.TabIndex = 23
        btnBill.Text = "Bill"
        btnBill.TextAlign = ContentAlignment.MiddleRight
        btnBill.TextImageRelation = TextImageRelation.ImageBeforeText
        btnBill.UseVisualStyleBackColor = True
        ' 
        ' btnUnsynch
        ' 
        btnUnsynch.Enabled = False
        btnUnsynch.Image = CType(resources.GetObject("btnUnsynch.Image"), Image)
        btnUnsynch.Location = New Point(300, 696)
        btnUnsynch.Margin = New Padding(5, 6, 5, 6)
        btnUnsynch.Name = "btnUnsynch"
        btnUnsynch.Size = New Size(137, 56)
        btnUnsynch.TabIndex = 21
        btnUnsynch.Text = "Unsynch"
        btnUnsynch.TextAlign = ContentAlignment.MiddleRight
        btnUnsynch.TextImageRelation = TextImageRelation.ImageBeforeText
        btnUnsynch.UseVisualStyleBackColor = True
        ' 
        ' btnSelS
        ' 
        btnSelS.ForeColor = Color.DarkBlue
        btnSelS.Image = CType(resources.GetObject("btnSelS.Image"), Image)
        btnSelS.Location = New Point(27, 696)
        btnSelS.Margin = New Padding(5, 6, 5, 6)
        btnSelS.Name = "btnSelS"
        btnSelS.Size = New Size(50, 56)
        btnSelS.TabIndex = 20
        btnSelS.TextAlign = ContentAlignment.MiddleRight
        btnSelS.TextImageRelation = TextImageRelation.ImageBeforeText
        btnSelS.UseVisualStyleBackColor = True
        ' 
        ' btnDeselS
        ' 
        btnDeselS.ForeColor = Color.DarkBlue
        btnDeselS.Image = CType(resources.GetObject("btnDeselS.Image"), Image)
        btnDeselS.Location = New Point(87, 696)
        btnDeselS.Margin = New Padding(5, 6, 5, 6)
        btnDeselS.Name = "btnDeselS"
        btnDeselS.Size = New Size(50, 56)
        btnDeselS.TabIndex = 19
        btnDeselS.TextAlign = ContentAlignment.MiddleRight
        btnDeselS.TextImageRelation = TextImageRelation.ImageBeforeText
        btnDeselS.UseVisualStyleBackColor = True
        ' 
        ' tpBilled
        ' 
        tpBilled.Controls.Add(Button11)
        tpBilled.Controls.Add(chkFileless)
        tpBilled.Controls.Add(Label12)
        tpBilled.Controls.Add(cmbClearingHouse)
        tpBilled.Controls.Add(Label11)
        tpBilled.Controls.Add(chkNZero)
        tpBilled.Controls.Add(cmbDestination)
        tpBilled.Controls.Add(txtBCount)
        tpBilled.Controls.Add(Label10)
        tpBilled.Controls.Add(dgvBilled)
        tpBilled.Controls.Add(btnReverse)
        tpBilled.Controls.Add(btnProcess)
        tpBilled.Controls.Add(btnSelB)
        tpBilled.Controls.Add(btnDeselB)
        tpBilled.Location = New Point(4, 22)
        tpBilled.Margin = New Padding(5, 6, 5, 6)
        tpBilled.Name = "tpBilled"
        tpBilled.Size = New Size(1004, 853)
        tpBilled.TabIndex = 2
        tpBilled.Text = "Billed          "
        tpBilled.UseVisualStyleBackColor = True
        ' 
        ' Button11
        ' 
        Button11.Location = New Point(8, 631)
        Button11.Margin = New Padding(3, 4, 3, 4)
        Button11.Name = "Button11"
        Button11.Size = New Size(127, 48)
        Button11.TabIndex = 41
        Button11.Text = "↓↓↓ CSV"
        Button11.UseVisualStyleBackColor = True
        ' 
        ' chkFileless
        ' 
        chkFileless.Appearance = Appearance.Button
        chkFileless.Checked = True
        chkFileless.CheckState = CheckState.Checked
        chkFileless.Location = New Point(383, 688)
        chkFileless.Margin = New Padding(5, 6, 5, 6)
        chkFileless.Name = "chkFileless"
        chkFileless.Size = New Size(138, 50)
        chkFileless.TabIndex = 40
        chkFileless.Text = "Unprocessed"
        chkFileless.TextAlign = ContentAlignment.MiddleCenter
        ToolTip1.SetToolTip(chkFileless, "Click me to toggle between 'Unprocessed' and 'All'.")
        chkFileless.UseVisualStyleBackColor = True
        ' 
        ' Label12
        ' 
        Label12.ForeColor = Color.DarkBlue
        Label12.Location = New Point(540, 756)
        Label12.Margin = New Padding(5, 0, 5, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(100, 25)
        Label12.TabIndex = 39
        Label12.Text = "Clearing H"
        Label12.TextAlign = ContentAlignment.TopRight
        ' 
        ' cmbClearingHouse
        ' 
        cmbClearingHouse.DropDownStyle = ComboBoxStyle.DropDownList
        cmbClearingHouse.FormattingEnabled = True
        cmbClearingHouse.Location = New Point(650, 752)
        cmbClearingHouse.Margin = New Padding(5, 6, 5, 6)
        cmbClearingHouse.Name = "cmbClearingHouse"
        cmbClearingHouse.Size = New Size(206, 33)
        cmbClearingHouse.TabIndex = 38
        ToolTip1.SetToolTip(cmbClearingHouse, "Select the desired Clearing House.")
        ' 
        ' Label11
        ' 
        Label11.ForeColor = Color.DarkBlue
        Label11.Location = New Point(532, 704)
        Label11.Margin = New Padding(5, 0, 5, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(108, 25)
        Label11.TabIndex = 37
        Label11.Text = "Destination"
        Label11.TextAlign = ContentAlignment.TopRight
        ' 
        ' chkNZero
        ' 
        chkNZero.Appearance = Appearance.Button
        chkNZero.Checked = True
        chkNZero.CheckState = CheckState.Checked
        chkNZero.Location = New Point(380, 744)
        chkNZero.Margin = New Padding(5, 6, 5, 6)
        chkNZero.Name = "chkNZero"
        chkNZero.Size = New Size(142, 50)
        chkNZero.TabIndex = 35
        chkNZero.Text = "Non Zero"
        chkNZero.TextAlign = ContentAlignment.MiddleCenter
        ToolTip1.SetToolTip(chkNZero, "Click me to toggle between 'Non Zero' and 'All'.")
        chkNZero.UseVisualStyleBackColor = True
        ' 
        ' cmbDestination
        ' 
        cmbDestination.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDestination.FormattingEnabled = True
        cmbDestination.Location = New Point(650, 698)
        cmbDestination.Margin = New Padding(5, 6, 5, 6)
        cmbDestination.Name = "cmbDestination"
        cmbDestination.Size = New Size(206, 33)
        cmbDestination.TabIndex = 34
        ToolTip1.SetToolTip(cmbDestination, "Select among 'Printer', 'Screen' or '837 File'.")
        ' 
        ' txtBCount
        ' 
        txtBCount.Location = New Point(128, 727)
        txtBCount.Margin = New Padding(5, 6, 5, 6)
        txtBCount.Name = "txtBCount"
        txtBCount.ReadOnly = True
        txtBCount.Size = New Size(102, 31)
        txtBCount.TabIndex = 33
        txtBCount.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label10
        ' 
        Label10.ForeColor = Color.DarkBlue
        Label10.Location = New Point(128, 696)
        Label10.Margin = New Padding(5, 0, 5, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(105, 25)
        Label10.TabIndex = 32
        Label10.Text = "Count"
        Label10.TextAlign = ContentAlignment.TopCenter
        ' 
        ' dgvBilled
        ' 
        dgvBilled.AllowUserToAddRows = False
        dgvBilled.AllowUserToDeleteRows = False
        DataGridViewCellStyle3.BackColor = Color.MistyRose
        dgvBilled.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        dgvBilled.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvBilled.Columns.AddRange(New DataGridViewColumn() {DataGridViewCheckBoxColumn2, InvID, DataGridViewTextBoxColumn5, PatientsB, SvcDateB, BillDateB, AmountB, Billees, Email})
        dgvBilled.Location = New Point(8, 4)
        dgvBilled.Margin = New Padding(5, 6, 5, 6)
        dgvBilled.Name = "dgvBilled"
        dgvBilled.RowHeadersVisible = False
        dgvBilled.RowHeadersWidth = 62
        dgvBilled.ScrollBars = ScrollBars.Vertical
        dgvBilled.Size = New Size(985, 617)
        dgvBilled.TabIndex = 17
        ' 
        ' DataGridViewCheckBoxColumn2
        ' 
        DataGridViewCheckBoxColumn2.FillWeight = 40F
        DataGridViewCheckBoxColumn2.HeaderText = ""
        DataGridViewCheckBoxColumn2.MinimumWidth = 8
        DataGridViewCheckBoxColumn2.Name = "DataGridViewCheckBoxColumn2"
        DataGridViewCheckBoxColumn2.Width = 40
        ' 
        ' InvID
        ' 
        InvID.FillWeight = 70F
        InvID.HeaderText = "Invoice"
        InvID.MinimumWidth = 8
        InvID.Name = "InvID"
        InvID.ReadOnly = True
        InvID.Width = 70
        ' 
        ' DataGridViewTextBoxColumn5
        ' 
        DataGridViewTextBoxColumn5.FillWeight = 70F
        DataGridViewTextBoxColumn5.HeaderText = "Accession"
        DataGridViewTextBoxColumn5.MinimumWidth = 8
        DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        DataGridViewTextBoxColumn5.ReadOnly = True
        DataGridViewTextBoxColumn5.Width = 70
        ' 
        ' PatientsB
        ' 
        PatientsB.HeaderText = "Patient (L, S)"
        PatientsB.MinimumWidth = 8
        PatientsB.Name = "PatientsB"
        PatientsB.ReadOnly = True
        PatientsB.Width = 150
        ' 
        ' SvcDateB
        ' 
        SvcDateB.FillWeight = 70F
        SvcDateB.HeaderText = "Svc'd"
        SvcDateB.MinimumWidth = 8
        SvcDateB.Name = "SvcDateB"
        SvcDateB.ReadOnly = True
        SvcDateB.SortMode = DataGridViewColumnSortMode.NotSortable
        SvcDateB.Width = 70
        ' 
        ' BillDateB
        ' 
        BillDateB.FillWeight = 70F
        BillDateB.HeaderText = "Bill'd"
        BillDateB.MinimumWidth = 8
        BillDateB.Name = "BillDateB"
        BillDateB.ReadOnly = True
        BillDateB.SortMode = DataGridViewColumnSortMode.NotSortable
        BillDateB.Width = 70
        ' 
        ' AmountB
        ' 
        AmountB.FillWeight = 70F
        AmountB.HeaderText = "Amount"
        AmountB.MinimumWidth = 8
        AmountB.Name = "AmountB"
        AmountB.ReadOnly = True
        AmountB.Width = 70
        ' 
        ' Billees
        ' 
        Billees.FillWeight = 110F
        Billees.HeaderText = "Billees"
        Billees.MinimumWidth = 8
        Billees.Name = "Billees"
        Billees.ReadOnly = True
        Billees.Width = 110
        ' 
        ' Email
        ' 
        Email.HeaderText = "Email"
        Email.MinimumWidth = 8
        Email.Name = "Email"
        Email.ReadOnly = True
        Email.SortMode = DataGridViewColumnSortMode.NotSortable
        Email.Width = 150
        ' 
        ' btnReverse
        ' 
        btnReverse.Enabled = False
        btnReverse.Image = CType(resources.GetObject("btnReverse.Image"), Image)
        btnReverse.Location = New Point(243, 715)
        btnReverse.Margin = New Padding(5, 6, 5, 6)
        btnReverse.Name = "btnReverse"
        btnReverse.Size = New Size(127, 56)
        btnReverse.TabIndex = 36
        btnReverse.Text = "Reverse"
        btnReverse.TextAlign = ContentAlignment.MiddleRight
        btnReverse.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(btnReverse, "Click me to reverse the billing.")
        btnReverse.UseVisualStyleBackColor = True
        ' 
        ' btnProcess
        ' 
        btnProcess.Enabled = False
        btnProcess.Image = CType(resources.GetObject("btnProcess.Image"), Image)
        btnProcess.Location = New Point(868, 704)
        btnProcess.Margin = New Padding(5, 6, 5, 6)
        btnProcess.Name = "btnProcess"
        btnProcess.Size = New Size(123, 77)
        btnProcess.TabIndex = 21
        btnProcess.Text = "Process"
        btnProcess.TextAlign = ContentAlignment.MiddleRight
        btnProcess.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(btnProcess, "Click me to start the process of screen display, print or the 837 file generation.")
        btnProcess.UseVisualStyleBackColor = True
        ' 
        ' btnSelB
        ' 
        btnSelB.ForeColor = Color.DarkBlue
        btnSelB.Image = CType(resources.GetObject("btnSelB.Image"), Image)
        btnSelB.Location = New Point(8, 715)
        btnSelB.Margin = New Padding(5, 6, 5, 6)
        btnSelB.Name = "btnSelB"
        btnSelB.Size = New Size(50, 56)
        btnSelB.TabIndex = 20
        btnSelB.TextAlign = ContentAlignment.MiddleRight
        btnSelB.TextImageRelation = TextImageRelation.ImageBeforeText
        btnSelB.UseVisualStyleBackColor = True
        ' 
        ' btnDeselB
        ' 
        btnDeselB.ForeColor = Color.DarkBlue
        btnDeselB.Image = CType(resources.GetObject("btnDeselB.Image"), Image)
        btnDeselB.Location = New Point(68, 715)
        btnDeselB.Margin = New Padding(5, 6, 5, 6)
        btnDeselB.Name = "btnDeselB"
        btnDeselB.Size = New Size(50, 56)
        btnDeselB.TabIndex = 19
        btnDeselB.TextAlign = ContentAlignment.MiddleRight
        btnDeselB.TextImageRelation = TextImageRelation.ImageBeforeText
        btnDeselB.UseVisualStyleBackColor = True
        ' 
        ' tpProcessed
        ' 
        tpProcessed.Controls.Add(dgvProcPats)
        tpProcessed.Controls.Add(txtProcessed)
        tpProcessed.Controls.Add(Label16)
        tpProcessed.Controls.Add(dgvProcTPs)
        tpProcessed.Controls.Add(btnFileOutput)
        tpProcessed.Controls.Add(btnUnprocess)
        tpProcessed.Controls.Add(btnSelP)
        tpProcessed.Controls.Add(btnDeselP)
        tpProcessed.Location = New Point(4, 22)
        tpProcessed.Margin = New Padding(5, 6, 5, 6)
        tpProcessed.Name = "tpProcessed"
        tpProcessed.Padding = New Padding(5, 6, 5, 6)
        tpProcessed.Size = New Size(1004, 853)
        tpProcessed.TabIndex = 3
        tpProcessed.Text = "Processed"
        tpProcessed.UseVisualStyleBackColor = True
        ' 
        ' dgvProcPats
        ' 
        dgvProcPats.AllowUserToAddRows = False
        dgvProcPats.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.BackColor = Color.AliceBlue
        dgvProcPats.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        dgvProcPats.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvProcPats.Columns.AddRange(New DataGridViewColumn() {PatCheck, PatInVID, PatAccID, PatPatient, PatSDate, PatBDate, PatAmount, PatPrints, PatEmail})
        dgvProcPats.Location = New Point(7, 12)
        dgvProcPats.Margin = New Padding(5, 6, 5, 6)
        dgvProcPats.Name = "dgvProcPats"
        dgvProcPats.RowHeadersVisible = False
        dgvProcPats.RowHeadersWidth = 62
        dgvProcPats.ScrollBars = ScrollBars.Vertical
        dgvProcPats.Size = New Size(983, 646)
        dgvProcPats.TabIndex = 43
        ' 
        ' PatCheck
        ' 
        PatCheck.FillWeight = 40F
        PatCheck.HeaderText = ""
        PatCheck.MinimumWidth = 8
        PatCheck.Name = "PatCheck"
        PatCheck.Width = 40
        ' 
        ' PatInVID
        ' 
        PatInVID.FillWeight = 70F
        PatInVID.HeaderText = "Invoice"
        PatInVID.MinimumWidth = 8
        PatInVID.Name = "PatInVID"
        PatInVID.ReadOnly = True
        PatInVID.Width = 70
        ' 
        ' PatAccID
        ' 
        PatAccID.FillWeight = 70F
        PatAccID.HeaderText = "Accession"
        PatAccID.MinimumWidth = 8
        PatAccID.Name = "PatAccID"
        PatAccID.ReadOnly = True
        PatAccID.Width = 70
        ' 
        ' PatPatient
        ' 
        PatPatient.HeaderText = "Patient (L, S)"
        PatPatient.MinimumWidth = 8
        PatPatient.Name = "PatPatient"
        PatPatient.ReadOnly = True
        PatPatient.Width = 150
        ' 
        ' PatSDate
        ' 
        PatSDate.FillWeight = 70F
        PatSDate.HeaderText = "Svc'd"
        PatSDate.MinimumWidth = 8
        PatSDate.Name = "PatSDate"
        PatSDate.ReadOnly = True
        PatSDate.SortMode = DataGridViewColumnSortMode.NotSortable
        PatSDate.Width = 70
        ' 
        ' PatBDate
        ' 
        PatBDate.FillWeight = 70F
        PatBDate.HeaderText = "Bill'd"
        PatBDate.MinimumWidth = 8
        PatBDate.Name = "PatBDate"
        PatBDate.ReadOnly = True
        PatBDate.SortMode = DataGridViewColumnSortMode.NotSortable
        PatBDate.Width = 70
        ' 
        ' PatAmount
        ' 
        PatAmount.FillWeight = 70F
        PatAmount.HeaderText = "Amount"
        PatAmount.MinimumWidth = 8
        PatAmount.Name = "PatAmount"
        PatAmount.ReadOnly = True
        PatAmount.Width = 70
        ' 
        ' PatPrints
        ' 
        PatPrints.FillWeight = 110F
        PatPrints.HeaderText = "Prints"
        PatPrints.MinimumWidth = 8
        PatPrints.Name = "PatPrints"
        PatPrints.ReadOnly = True
        PatPrints.Width = 110
        ' 
        ' PatEmail
        ' 
        PatEmail.HeaderText = "Email"
        PatEmail.MinimumWidth = 8
        PatEmail.Name = "PatEmail"
        PatEmail.ReadOnly = True
        PatEmail.SortMode = DataGridViewColumnSortMode.NotSortable
        PatEmail.Width = 150
        ' 
        ' txtProcessed
        ' 
        txtProcessed.Location = New Point(152, 715)
        txtProcessed.Margin = New Padding(5, 6, 5, 6)
        txtProcessed.Name = "txtProcessed"
        txtProcessed.ReadOnly = True
        txtProcessed.Size = New Size(102, 31)
        txtProcessed.TabIndex = 40
        txtProcessed.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label16
        ' 
        Label16.ForeColor = Color.DarkBlue
        Label16.Location = New Point(152, 685)
        Label16.Margin = New Padding(5, 0, 5, 0)
        Label16.Name = "Label16"
        Label16.Size = New Size(105, 25)
        Label16.TabIndex = 39
        Label16.Text = "Count"
        Label16.TextAlign = ContentAlignment.TopCenter
        ' 
        ' dgvProcTPs
        ' 
        dgvProcTPs.AllowUserToAddRows = False
        dgvProcTPs.AllowUserToDeleteRows = False
        DataGridViewCellStyle5.BackColor = Color.Azure
        dgvProcTPs.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        dgvProcTPs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvProcTPs.Columns.AddRange(New DataGridViewColumn() {TPCheck, FileNo, ClearingHouse, Created, ChCount, FileAmount})
        dgvProcTPs.Location = New Point(7, 12)
        dgvProcTPs.Margin = New Padding(5, 6, 5, 6)
        dgvProcTPs.Name = "dgvProcTPs"
        dgvProcTPs.RowHeadersVisible = False
        dgvProcTPs.RowHeadersWidth = 62
        dgvProcTPs.ScrollBars = ScrollBars.Vertical
        dgvProcTPs.Size = New Size(983, 646)
        dgvProcTPs.TabIndex = 18
        ' 
        ' TPCheck
        ' 
        TPCheck.FillWeight = 40F
        TPCheck.HeaderText = ""
        TPCheck.MinimumWidth = 8
        TPCheck.Name = "TPCheck"
        TPCheck.Width = 40
        ' 
        ' FileNo
        ' 
        FileNo.FillWeight = 70F
        FileNo.HeaderText = "File No"
        FileNo.MinimumWidth = 8
        FileNo.Name = "FileNo"
        FileNo.ReadOnly = True
        FileNo.Width = 70
        ' 
        ' ClearingHouse
        ' 
        ClearingHouse.FillWeight = 200F
        ClearingHouse.HeaderText = "Clearing House"
        ClearingHouse.MinimumWidth = 8
        ClearingHouse.Name = "ClearingHouse"
        ClearingHouse.ReadOnly = True
        ClearingHouse.Width = 200
        ' 
        ' Created
        ' 
        Created.FillWeight = 90F
        Created.HeaderText = "Created on"
        Created.MinimumWidth = 8
        Created.Name = "Created"
        Created.ReadOnly = True
        Created.SortMode = DataGridViewColumnSortMode.NotSortable
        Created.Width = 90
        ' 
        ' ChCount
        ' 
        ChCount.FillWeight = 80F
        ChCount.HeaderText = "Invoices"
        ChCount.MinimumWidth = 8
        ChCount.Name = "ChCount"
        ChCount.ReadOnly = True
        ChCount.Width = 80
        ' 
        ' FileAmount
        ' 
        FileAmount.FillWeight = 90F
        FileAmount.HeaderText = "File Amount"
        FileAmount.MinimumWidth = 8
        FileAmount.Name = "FileAmount"
        FileAmount.ReadOnly = True
        FileAmount.SortMode = DataGridViewColumnSortMode.NotSortable
        FileAmount.Width = 90
        ' 
        ' btnFileOutput
        ' 
        btnFileOutput.Enabled = False
        btnFileOutput.Image = CType(resources.GetObject("btnFileOutput.Image"), Image)
        btnFileOutput.Location = New Point(820, 706)
        btnFileOutput.Margin = New Padding(5, 6, 5, 6)
        btnFileOutput.Name = "btnFileOutput"
        btnFileOutput.Size = New Size(153, 56)
        btnFileOutput.TabIndex = 42
        btnFileOutput.Text = "File Output"
        btnFileOutput.TextAlign = ContentAlignment.MiddleRight
        btnFileOutput.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(btnFileOutput, "Click me to reverse the billing.")
        btnFileOutput.UseVisualStyleBackColor = True
        ' 
        ' btnUnprocess
        ' 
        btnUnprocess.Enabled = False
        btnUnprocess.Image = CType(resources.GetObject("btnUnprocess.Image"), Image)
        btnUnprocess.Location = New Point(267, 706)
        btnUnprocess.Margin = New Padding(5, 6, 5, 6)
        btnUnprocess.Name = "btnUnprocess"
        btnUnprocess.Size = New Size(127, 56)
        btnUnprocess.TabIndex = 41
        btnUnprocess.Text = "Reverse"
        btnUnprocess.TextAlign = ContentAlignment.MiddleRight
        btnUnprocess.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(btnUnprocess, "Click me to reverse the billing.")
        btnUnprocess.UseVisualStyleBackColor = True
        ' 
        ' btnSelP
        ' 
        btnSelP.ForeColor = Color.DarkBlue
        btnSelP.Image = CType(resources.GetObject("btnSelP.Image"), Image)
        btnSelP.Location = New Point(32, 706)
        btnSelP.Margin = New Padding(5, 6, 5, 6)
        btnSelP.Name = "btnSelP"
        btnSelP.Size = New Size(50, 56)
        btnSelP.TabIndex = 38
        btnSelP.TextAlign = ContentAlignment.MiddleRight
        btnSelP.TextImageRelation = TextImageRelation.ImageBeforeText
        btnSelP.UseVisualStyleBackColor = True
        ' 
        ' btnDeselP
        ' 
        btnDeselP.ForeColor = Color.DarkBlue
        btnDeselP.Image = CType(resources.GetObject("btnDeselP.Image"), Image)
        btnDeselP.Location = New Point(92, 706)
        btnDeselP.Margin = New Padding(5, 6, 5, 6)
        btnDeselP.Name = "btnDeselP"
        btnDeselP.Size = New Size(50, 56)
        btnDeselP.TabIndex = 37
        btnDeselP.TextAlign = ContentAlignment.MiddleRight
        btnDeselP.TextImageRelation = TextImageRelation.ImageBeforeText
        btnDeselP.UseVisualStyleBackColor = True
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.DarkBlue
        Label5.Location = New Point(237, 223)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(143, 25)
        Label5.TabIndex = 27
        Label5.Text = "Accession To"
        ' 
        ' txtAccTo
        ' 
        txtAccTo.Location = New Point(232, 256)
        txtAccTo.Margin = New Padding(5, 6, 5, 6)
        txtAccTo.MaxLength = 11
        txtAccTo.Name = "txtAccTo"
        txtAccTo.Size = New Size(191, 31)
        txtAccTo.TabIndex = 25
        txtAccTo.TextAlign = HorizontalAlignment.Center
        ToolTip1.SetToolTip(txtAccTo, "Enter the maximum of the accession ID range")
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(27, 223)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(147, 25)
        Label4.TabIndex = 26
        Label4.Text = "Accession From"
        ' 
        ' lblTo
        ' 
        lblTo.ForeColor = Color.Navy
        lblTo.Location = New Point(233, 131)
        lblTo.Margin = New Padding(5, 0, 5, 0)
        lblTo.Name = "lblTo"
        lblTo.Size = New Size(190, 25)
        lblTo.TabIndex = 23
        lblTo.Text = "Date To"
        ' 
        ' txtAccFrom
        ' 
        txtAccFrom.Location = New Point(25, 256)
        txtAccFrom.Margin = New Padding(5, 6, 5, 6)
        txtAccFrom.MaxLength = 11
        txtAccFrom.Name = "txtAccFrom"
        txtAccFrom.Size = New Size(196, 31)
        txtAccFrom.TabIndex = 24
        txtAccFrom.TextAlign = HorizontalAlignment.Center
        ToolTip1.SetToolTip(txtAccFrom, "Enter the minimum of the accession ID range")
        ' 
        ' lblFrom
        ' 
        lblFrom.ForeColor = Color.Navy
        lblFrom.Location = New Point(23, 131)
        lblFrom.Margin = New Padding(5, 0, 5, 0)
        lblFrom.Name = "lblFrom"
        lblFrom.Size = New Size(200, 25)
        lblFrom.TabIndex = 20
        lblFrom.Text = "Date From"
        ' 
        ' txtOutput
        ' 
        txtOutput.Location = New Point(1047, 435)
        txtOutput.Margin = New Padding(5, 6, 5, 6)
        txtOutput.Multiline = True
        txtOutput.Name = "txtOutput"
        txtOutput.ReadOnly = True
        txtOutput.ScrollBars = ScrollBars.Vertical
        txtOutput.Size = New Size(414, 806)
        txtOutput.TabIndex = 29
        ToolTip1.SetToolTip(txtOutput, "Area to display the running process result.")
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.ImageScalingSize = New Size(20, 20)
        StatusStrip1.Items.AddRange(New ToolStripItem() {lblStatus, PB})
        StatusStrip1.Location = New Point(0, 1257)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Padding = New Padding(2, 0, 23, 0)
        StatusStrip1.Size = New Size(1502, 43)
        StatusStrip1.TabIndex = 30
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = False
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(120, 36)
        ' 
        ' PB
        ' 
        PB.AutoSize = False
        PB.Name = "PB"
        PB.Size = New Size(1050, 35)
        PB.Style = ProgressBarStyle.Continuous
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(1047, 392)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(417, 25)
        Label2.TabIndex = 31
        Label2.Text = "Process output (Tab Delimited)"
        Label2.TextAlign = ContentAlignment.TopCenter
        ' 
        ' lstTargets
        ' 
        lstTargets.FormattingEnabled = True
        lstTargets.Location = New Point(810, 67)
        lstTargets.Margin = New Padding(5, 6, 5, 6)
        lstTargets.Name = "lstTargets"
        lstTargets.Size = New Size(577, 200)
        lstTargets.Sorted = True
        lstTargets.TabIndex = 32
        ToolTip1.SetToolTip(lstTargets, "Parties to be billed.")
        ' 
        ' dgvDiscrete
        ' 
        dgvDiscrete.AllowUserToAddRows = False
        dgvDiscrete.AllowUserToDeleteRows = False
        DataGridViewCellStyle6.BackColor = Color.FromArgb(CByte(255), CByte(224), CByte(192))
        dgvDiscrete.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle6
        dgvDiscrete.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvDiscrete.Columns.AddRange(New DataGridViewColumn() {Discrete})
        dgvDiscrete.Location = New Point(453, 67)
        dgvDiscrete.Margin = New Padding(5, 6, 5, 6)
        dgvDiscrete.Name = "dgvDiscrete"
        dgvDiscrete.RowHeadersVisible = False
        dgvDiscrete.RowHeadersWidth = 62
        dgvDiscrete.ScrollBars = ScrollBars.Vertical
        dgvDiscrete.Size = New Size(248, 238)
        dgvDiscrete.TabIndex = 34
        ToolTip1.SetToolTip(dgvDiscrete, "Either enter one discrete accession per line" & vbCrLf & "or" & vbCrLf & "Right click and paste the discrete accessions copied from the external source.")
        ' 
        ' Discrete
        ' 
        Discrete.FillWeight = 125F
        Discrete.HeaderText = "Discrete"
        Discrete.MaxInputLength = 16
        Discrete.MinimumWidth = 8
        Discrete.Name = "Discrete"
        Discrete.SortMode = DataGridViewColumnSortMode.NotSortable
        Discrete.Width = 125
        ' 
        ' Label1
        ' 
        Label1.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.Magenta
        Label1.Location = New Point(387, 213)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(57, 37)
        Label1.TabIndex = 35
        Label1.Text = "OR"
        Label1.TextAlign = ContentAlignment.TopCenter
        ' 
        ' ComboBox1
        ' 
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(369, 467)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(147, 33)
        ComboBox1.TabIndex = 38
        ToolTip1.SetToolTip(ComboBox1, "Select the desired Clearing House.")
        ' 
        ' CheckBox1
        ' 
        CheckBox1.Appearance = Appearance.Button
        CheckBox1.Checked = True
        CheckBox1.CheckState = CheckState.Checked
        CheckBox1.Location = New Point(229, 463)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(61, 26)
        CheckBox1.TabIndex = 35
        CheckBox1.Text = "Non Zero"
        CheckBox1.TextAlign = ContentAlignment.MiddleCenter
        ToolTip1.SetToolTip(CheckBox1, "Click me to toggle between 'Non Zero' and 'All'.")
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' ComboBox2
        ' 
        ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox2.FormattingEnabled = True
        ComboBox2.Location = New Point(369, 440)
        ComboBox2.Name = "ComboBox2"
        ComboBox2.Size = New Size(147, 33)
        ComboBox2.TabIndex = 34
        ToolTip1.SetToolTip(ComboBox2, "Select among 'Printer', 'Screen' or '837 File'.")
        ' 
        ' txt837File
        ' 
        txt837File.Location = New Point(173, 333)
        txt837File.Margin = New Padding(5, 6, 5, 6)
        txt837File.MaxLength = 400
        txt837File.Name = "txt837File"
        txt837File.ReadOnly = True
        txt837File.Size = New Size(1022, 31)
        txt837File.TabIndex = 41
        txt837File.TextAlign = HorizontalAlignment.Center
        ToolTip1.SetToolTip(txt837File, "Enter the 837 file with its path using the lookup button")
        ' 
        ' btn837Lookup
        ' 
        btn837Lookup.ForeColor = Color.DarkBlue
        btn837Lookup.Image = CType(resources.GetObject("btn837Lookup.Image"), Image)
        btn837Lookup.Location = New Point(1208, 323)
        btn837Lookup.Margin = New Padding(5, 6, 5, 6)
        btn837Lookup.Name = "btn837Lookup"
        btn837Lookup.Size = New Size(48, 52)
        btn837Lookup.TabIndex = 43
        btn837Lookup.TextAlign = ContentAlignment.MiddleRight
        btn837Lookup.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(btn837Lookup, "Click me to display the parties to bill, depending on the Billing Type" & vbCrLf & "selected in the top left Billing Type drop down.")
        btn837Lookup.UseVisualStyleBackColor = True
        ' 
        ' btnSellT
        ' 
        btnSellT.ForeColor = Color.DarkBlue
        btnSellT.Image = CType(resources.GetObject("btnSellT.Image"), Image)
        btnSellT.Location = New Point(1400, 67)
        btnSellT.Margin = New Padding(5, 6, 5, 6)
        btnSellT.Name = "btnSellT"
        btnSellT.Size = New Size(50, 56)
        btnSellT.TabIndex = 38
        btnSellT.TextAlign = ContentAlignment.MiddleRight
        btnSellT.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(btnSellT, "Click me to select all the parties to bill.")
        btnSellT.UseVisualStyleBackColor = True
        ' 
        ' btnDeselT
        ' 
        btnDeselT.ForeColor = Color.DarkBlue
        btnDeselT.Image = CType(resources.GetObject("btnDeselT.Image"), Image)
        btnDeselT.Location = New Point(1400, 250)
        btnDeselT.Margin = New Padding(5, 6, 5, 6)
        btnDeselT.Name = "btnDeselT"
        btnDeselT.Size = New Size(50, 56)
        btnDeselT.TabIndex = 37
        btnDeselT.TextAlign = ContentAlignment.MiddleRight
        btnDeselT.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(btnDeselT, "Click me to deselect all the parties to bill.")
        btnDeselT.UseVisualStyleBackColor = True
        ' 
        ' btnTarget
        ' 
        btnTarget.ForeColor = Color.DarkBlue
        btnTarget.Image = CType(resources.GetObject("btnTarget.Image"), Image)
        btnTarget.Location = New Point(728, 144)
        btnTarget.Margin = New Padding(5, 6, 5, 6)
        btnTarget.Name = "btnTarget"
        btnTarget.Size = New Size(58, 62)
        btnTarget.TabIndex = 33
        btnTarget.TextAlign = ContentAlignment.MiddleRight
        btnTarget.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(btnTarget, "Click me to display the parties to bill, depending on the Billing Type" & vbCrLf & "selected in the top left Billing Type drop down.")
        btnTarget.UseVisualStyleBackColor = True
        ' 
        ' btnLoad
        ' 
        btnLoad.ForeColor = Color.DarkBlue
        btnLoad.Image = CType(resources.GetObject("btnLoad.Image"), Image)
        btnLoad.Location = New Point(1303, 315)
        btnLoad.Margin = New Padding(5, 6, 5, 6)
        btnLoad.Name = "btnLoad"
        btnLoad.Size = New Size(147, 60)
        btnLoad.TabIndex = 12
        btnLoad.TextAlign = ContentAlignment.MiddleRight
        btnLoad.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(btnLoad, "Click me to display the appropriate accessions within the selected Tab.")
        btnLoad.UseVisualStyleBackColor = True
        ' 
        ' Button1
        ' 
        Button1.Enabled = False
        Button1.Image = CType(resources.GetObject("Button1.Image"), Image)
        Button1.Location = New Point(147, 454)
        Button1.Name = "Button1"
        Button1.Size = New Size(76, 29)
        Button1.TabIndex = 36
        Button1.Text = "Reverse"
        Button1.TextAlign = ContentAlignment.MiddleRight
        Button1.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(Button1, "Click me to reverse the billing.")
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Enabled = False
        Button2.Image = CType(resources.GetObject("Button2.Image"), Image)
        Button2.Location = New Point(522, 443)
        Button2.Name = "Button2"
        Button2.Size = New Size(74, 40)
        Button2.TabIndex = 21
        Button2.Text = "Process"
        Button2.TextAlign = ContentAlignment.MiddleRight
        Button2.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(Button2, "Click me to start the process of screen display, print or the 837 file generation.")
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button5
        ' 
        Button5.Enabled = False
        Button5.Image = CType(resources.GetObject("Button5.Image"), Image)
        Button5.Location = New Point(486, 457)
        Button5.Name = "Button5"
        Button5.Size = New Size(92, 29)
        Button5.TabIndex = 42
        Button5.Text = "File Output"
        Button5.TextAlign = ContentAlignment.MiddleRight
        Button5.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(Button5, "Click me to reverse the billing.")
        Button5.UseVisualStyleBackColor = True
        ' 
        ' Button6
        ' 
        Button6.Enabled = False
        Button6.Image = CType(resources.GetObject("Button6.Image"), Image)
        Button6.Location = New Point(154, 457)
        Button6.Name = "Button6"
        Button6.Size = New Size(76, 29)
        Button6.TabIndex = 41
        Button6.Text = "Reverse"
        Button6.TextAlign = ContentAlignment.MiddleRight
        Button6.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(Button6, "Click me to reverse the billing.")
        Button6.UseVisualStyleBackColor = True
        ' 
        ' claimpath
        ' 
        claimpath.Location = New Point(1113, 829)
        claimpath.Margin = New Padding(5, 6, 5, 6)
        claimpath.MaxLength = 200
        claimpath.Name = "claimpath"
        claimpath.Size = New Size(191, 31)
        claimpath.TabIndex = 49
        claimpath.TextAlign = HorizontalAlignment.Center
        ToolTip1.SetToolTip(claimpath, "Enter the maximum of the accession ID range")
        claimpath.Visible = False
        ' 
        ' BW
        ' 
        BW.WorkerReportsProgress = True
        BW.WorkerSupportsCancellation = True
        ' 
        ' Label8
        ' 
        Label8.Font = New Font("Microsoft Sans Serif", 36F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label8.ForeColor = Color.Red
        Label8.Location = New Point(23, 204)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(70, 19)
        Label8.TabIndex = 39
        Label8.Text = "BEING CODED ...      DO NOT USE !"
        Label8.TextAlign = ContentAlignment.TopCenter
        ' 
        ' PrintDialog1
        ' 
        PrintDialog1.UseEXDialog = True
        ' 
        ' Label13
        ' 
        Label13.ForeColor = Color.DarkBlue
        Label13.Location = New Point(298, 470)
        Label13.Name = "Label13"
        Label13.Size = New Size(65, 13)
        Label13.TabIndex = 39
        Label13.Text = "Clearing H"
        Label13.TextAlign = ContentAlignment.TopRight
        ' 
        ' Label14
        ' 
        Label14.ForeColor = Color.DarkBlue
        Label14.Location = New Point(298, 443)
        Label14.Name = "Label14"
        Label14.Size = New Size(65, 13)
        Label14.TabIndex = 37
        Label14.Text = "Destination"
        Label14.TextAlign = ContentAlignment.TopRight
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(78, 459)
        TextBox1.Name = "TextBox1"
        TextBox1.ReadOnly = True
        TextBox1.Size = New Size(63, 31)
        TextBox1.TabIndex = 33
        TextBox1.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label15
        ' 
        Label15.ForeColor = Color.DarkBlue
        Label15.Location = New Point(78, 443)
        Label15.Name = "Label15"
        Label15.Size = New Size(63, 13)
        Label15.TabIndex = 32
        Label15.Text = "Count"
        Label15.TextAlign = ContentAlignment.TopCenter
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False
        DataGridViewCellStyle7.BackColor = Color.MistyRose
        DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle7
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {DataGridViewCheckBoxColumn3, DataGridViewTextBoxColumn1, DataGridViewTextBoxColumn2, DataGridViewTextBoxColumn3, DataGridViewTextBoxColumn4, DataGridViewTextBoxColumn6, DataGridViewTextBoxColumn7, DataGridViewTextBoxColumn8, DataGridViewTextBoxColumn9})
        DataGridView1.Location = New Point(6, 2)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersVisible = False
        DataGridView1.RowHeadersWidth = 62
        DataGridView1.ScrollBars = ScrollBars.Vertical
        DataGridView1.Size = New Size(590, 409)
        DataGridView1.TabIndex = 17
        ' 
        ' DataGridViewCheckBoxColumn3
        ' 
        DataGridViewCheckBoxColumn3.FillWeight = 40F
        DataGridViewCheckBoxColumn3.HeaderText = ""
        DataGridViewCheckBoxColumn3.MinimumWidth = 8
        DataGridViewCheckBoxColumn3.Name = "DataGridViewCheckBoxColumn3"
        DataGridViewCheckBoxColumn3.Width = 40
        ' 
        ' DataGridViewTextBoxColumn1
        ' 
        DataGridViewTextBoxColumn1.FillWeight = 70F
        DataGridViewTextBoxColumn1.HeaderText = "Invoice"
        DataGridViewTextBoxColumn1.MinimumWidth = 8
        DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        DataGridViewTextBoxColumn1.ReadOnly = True
        DataGridViewTextBoxColumn1.Width = 70
        ' 
        ' DataGridViewTextBoxColumn2
        ' 
        DataGridViewTextBoxColumn2.FillWeight = 70F
        DataGridViewTextBoxColumn2.HeaderText = "Accession"
        DataGridViewTextBoxColumn2.MinimumWidth = 8
        DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        DataGridViewTextBoxColumn2.ReadOnly = True
        DataGridViewTextBoxColumn2.Width = 70
        ' 
        ' DataGridViewTextBoxColumn3
        ' 
        DataGridViewTextBoxColumn3.HeaderText = "Patient (L, S)"
        DataGridViewTextBoxColumn3.MinimumWidth = 8
        DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        DataGridViewTextBoxColumn3.ReadOnly = True
        DataGridViewTextBoxColumn3.Width = 150
        ' 
        ' DataGridViewTextBoxColumn4
        ' 
        DataGridViewTextBoxColumn4.FillWeight = 70F
        DataGridViewTextBoxColumn4.HeaderText = "Svc'd"
        DataGridViewTextBoxColumn4.MinimumWidth = 8
        DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        DataGridViewTextBoxColumn4.ReadOnly = True
        DataGridViewTextBoxColumn4.SortMode = DataGridViewColumnSortMode.NotSortable
        DataGridViewTextBoxColumn4.Width = 70
        ' 
        ' DataGridViewTextBoxColumn6
        ' 
        DataGridViewTextBoxColumn6.FillWeight = 70F
        DataGridViewTextBoxColumn6.HeaderText = "Bill'd"
        DataGridViewTextBoxColumn6.MinimumWidth = 8
        DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        DataGridViewTextBoxColumn6.ReadOnly = True
        DataGridViewTextBoxColumn6.SortMode = DataGridViewColumnSortMode.NotSortable
        DataGridViewTextBoxColumn6.Width = 70
        ' 
        ' DataGridViewTextBoxColumn7
        ' 
        DataGridViewTextBoxColumn7.FillWeight = 70F
        DataGridViewTextBoxColumn7.HeaderText = "Amount"
        DataGridViewTextBoxColumn7.MinimumWidth = 8
        DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        DataGridViewTextBoxColumn7.ReadOnly = True
        DataGridViewTextBoxColumn7.SortMode = DataGridViewColumnSortMode.NotSortable
        DataGridViewTextBoxColumn7.Width = 70
        ' 
        ' DataGridViewTextBoxColumn8
        ' 
        DataGridViewTextBoxColumn8.FillWeight = 110F
        DataGridViewTextBoxColumn8.HeaderText = "Billees"
        DataGridViewTextBoxColumn8.MinimumWidth = 8
        DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        DataGridViewTextBoxColumn8.ReadOnly = True
        DataGridViewTextBoxColumn8.Width = 110
        ' 
        ' DataGridViewTextBoxColumn9
        ' 
        DataGridViewTextBoxColumn9.HeaderText = "Email"
        DataGridViewTextBoxColumn9.MinimumWidth = 8
        DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        DataGridViewTextBoxColumn9.ReadOnly = True
        DataGridViewTextBoxColumn9.SortMode = DataGridViewColumnSortMode.NotSortable
        DataGridViewTextBoxColumn9.Width = 150
        ' 
        ' Label17
        ' 
        Label17.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label17.ForeColor = Color.Magenta
        Label17.Location = New Point(20, 333)
        Label17.Margin = New Padding(5, 0, 5, 0)
        Label17.Name = "Label17"
        Label17.Size = New Size(57, 37)
        Label17.TabIndex = 40
        Label17.Text = "OR"
        Label17.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label18
        ' 
        Label18.ForeColor = Color.DarkBlue
        Label18.Location = New Point(95, 337)
        Label18.Margin = New Padding(5, 0, 5, 0)
        Label18.Name = "Label18"
        Label18.Size = New Size(77, 25)
        Label18.TabIndex = 42
        Label18.Text = "837 File"
        Label18.TextAlign = ContentAlignment.TopRight
        ' 
        ' OpenFileDialog1
        ' 
        OpenFileDialog1.FileName = "OpenFileDialog1"
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(85, 462)
        TextBox2.Name = "TextBox2"
        TextBox2.ReadOnly = True
        TextBox2.Size = New Size(63, 31)
        TextBox2.TabIndex = 40
        TextBox2.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label19
        ' 
        Label19.ForeColor = Color.DarkBlue
        Label19.Location = New Point(85, 446)
        Label19.Name = "Label19"
        Label19.Size = New Size(63, 13)
        Label19.TabIndex = 39
        Label19.Text = "Count"
        Label19.TextAlign = ContentAlignment.TopCenter
        ' 
        ' DataGridView2
        ' 
        DataGridView2.AllowUserToAddRows = False
        DataGridView2.AllowUserToDeleteRows = False
        DataGridViewCellStyle8.BackColor = Color.Azure
        DataGridView2.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle8
        DataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView2.Columns.AddRange(New DataGridViewColumn() {DataGridViewCheckBoxColumn5, DataGridViewTextBoxColumn10, DataGridViewTextBoxColumn11, DataGridViewTextBoxColumn12, DataGridViewTextBoxColumn13, DataGridViewTextBoxColumn14})
        DataGridView2.Location = New Point(4, 3)
        DataGridView2.Name = "DataGridView2"
        DataGridView2.RowHeadersVisible = False
        DataGridView2.RowHeadersWidth = 62
        DataGridView2.ScrollBars = ScrollBars.Vertical
        DataGridView2.Size = New Size(590, 60)
        DataGridView2.TabIndex = 18
        ' 
        ' DataGridViewCheckBoxColumn5
        ' 
        DataGridViewCheckBoxColumn5.FillWeight = 40F
        DataGridViewCheckBoxColumn5.HeaderText = ""
        DataGridViewCheckBoxColumn5.MinimumWidth = 8
        DataGridViewCheckBoxColumn5.Name = "DataGridViewCheckBoxColumn5"
        DataGridViewCheckBoxColumn5.Width = 40
        ' 
        ' DataGridViewTextBoxColumn10
        ' 
        DataGridViewTextBoxColumn10.FillWeight = 70F
        DataGridViewTextBoxColumn10.HeaderText = "File No"
        DataGridViewTextBoxColumn10.MinimumWidth = 8
        DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        DataGridViewTextBoxColumn10.ReadOnly = True
        DataGridViewTextBoxColumn10.Width = 70
        ' 
        ' DataGridViewTextBoxColumn11
        ' 
        DataGridViewTextBoxColumn11.FillWeight = 200F
        DataGridViewTextBoxColumn11.HeaderText = "Clearing House"
        DataGridViewTextBoxColumn11.MinimumWidth = 8
        DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        DataGridViewTextBoxColumn11.ReadOnly = True
        DataGridViewTextBoxColumn11.Width = 200
        ' 
        ' DataGridViewTextBoxColumn12
        ' 
        DataGridViewTextBoxColumn12.FillWeight = 90F
        DataGridViewTextBoxColumn12.HeaderText = "Created on"
        DataGridViewTextBoxColumn12.MinimumWidth = 8
        DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        DataGridViewTextBoxColumn12.ReadOnly = True
        DataGridViewTextBoxColumn12.SortMode = DataGridViewColumnSortMode.NotSortable
        DataGridViewTextBoxColumn12.Width = 90
        ' 
        ' DataGridViewTextBoxColumn13
        ' 
        DataGridViewTextBoxColumn13.FillWeight = 80F
        DataGridViewTextBoxColumn13.HeaderText = "Invoices"
        DataGridViewTextBoxColumn13.MinimumWidth = 8
        DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        DataGridViewTextBoxColumn13.ReadOnly = True
        DataGridViewTextBoxColumn13.Width = 80
        ' 
        ' DataGridViewTextBoxColumn14
        ' 
        DataGridViewTextBoxColumn14.FillWeight = 90F
        DataGridViewTextBoxColumn14.HeaderText = "File Amount"
        DataGridViewTextBoxColumn14.MinimumWidth = 8
        DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        DataGridViewTextBoxColumn14.ReadOnly = True
        DataGridViewTextBoxColumn14.SortMode = DataGridViewColumnSortMode.NotSortable
        DataGridViewTextBoxColumn14.Width = 90
        ' 
        ' clipboardMsg
        ' 
        clipboardMsg.AutoSize = True
        clipboardMsg.Location = New Point(503, 379)
        clipboardMsg.Name = "clipboardMsg"
        clipboardMsg.Size = New Size(82, 25)
        clipboardMsg.TabIndex = 44
        clipboardMsg.Text = "message"
        ' 
        ' Label34
        ' 
        Label34.ForeColor = Color.DarkBlue
        Label34.Location = New Point(27, 58)
        Label34.Margin = New Padding(5, 0, 5, 0)
        Label34.Name = "Label34"
        Label34.Size = New Size(132, 27)
        Label34.TabIndex = 47
        Label34.Text = "Patient ID"
        ' 
        ' txtPatientID
        ' 
        txtPatientID.BackColor = Color.Ivory
        txtPatientID.Location = New Point(25, 90)
        txtPatientID.Margin = New Padding(5, 6, 5, 6)
        txtPatientID.MaxLength = 12
        txtPatientID.Name = "txtPatientID"
        txtPatientID.Size = New Size(136, 31)
        txtPatientID.TabIndex = 45
        txtPatientID.TextAlign = HorizontalAlignment.Center
        ' 
        ' btnPatLook
        ' 
        btnPatLook.Image = CType(resources.GetObject("btnPatLook.Image"), Image)
        btnPatLook.Location = New Point(172, 81)
        btnPatLook.Margin = New Padding(5, 6, 5, 6)
        btnPatLook.Name = "btnPatLook"
        btnPatLook.Size = New Size(50, 54)
        btnPatLook.TabIndex = 46
        btnPatLook.TabStop = False
        btnPatLook.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.ForeColor = Color.DarkBlue
        Button3.Image = CType(resources.GetObject("Button3.Image"), Image)
        Button3.Location = New Point(6, 454)
        Button3.Name = "Button3"
        Button3.Size = New Size(30, 29)
        Button3.TabIndex = 20
        Button3.TextAlign = ContentAlignment.MiddleRight
        Button3.TextImageRelation = TextImageRelation.ImageBeforeText
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.ForeColor = Color.DarkBlue
        Button4.Image = CType(resources.GetObject("Button4.Image"), Image)
        Button4.Location = New Point(42, 454)
        Button4.Name = "Button4"
        Button4.Size = New Size(30, 29)
        Button4.TabIndex = 19
        Button4.TextAlign = ContentAlignment.MiddleRight
        Button4.TextImageRelation = TextImageRelation.ImageBeforeText
        Button4.UseVisualStyleBackColor = True
        ' 
        ' Button7
        ' 
        Button7.ForeColor = Color.DarkBlue
        Button7.Image = CType(resources.GetObject("Button7.Image"), Image)
        Button7.Location = New Point(13, 457)
        Button7.Name = "Button7"
        Button7.Size = New Size(30, 29)
        Button7.TabIndex = 38
        Button7.TextAlign = ContentAlignment.MiddleRight
        Button7.TextImageRelation = TextImageRelation.ImageBeforeText
        Button7.UseVisualStyleBackColor = True
        ' 
        ' Button8
        ' 
        Button8.ForeColor = Color.DarkBlue
        Button8.Image = CType(resources.GetObject("Button8.Image"), Image)
        Button8.Location = New Point(49, 457)
        Button8.Name = "Button8"
        Button8.Size = New Size(30, 29)
        Button8.TabIndex = 37
        Button8.TextAlign = ContentAlignment.MiddleRight
        Button8.TextImageRelation = TextImageRelation.ImageBeforeText
        Button8.UseVisualStyleBackColor = True
        ' 
        ' viewClaim
        ' 
        viewClaim.Location = New Point(1193, 1137)
        viewClaim.Margin = New Padding(3, 4, 3, 4)
        viewClaim.Name = "viewClaim"
        viewClaim.Size = New Size(113, 46)
        viewClaim.TabIndex = 48
        viewClaim.Text = "View Claim"
        viewClaim.UseVisualStyleBackColor = True
        ' 
        ' dtpDateTo
        ' 
        dtpDateTo.CustomFormat = " "
        dtpDateTo.Format = DateTimePickerFormat.Custom
        dtpDateTo.Location = New Point(232, 167)
        dtpDateTo.Margin = New Padding(3, 4, 3, 4)
        dtpDateTo.Name = "dtpDateTo"
        dtpDateTo.Size = New Size(136, 31)
        dtpDateTo.TabIndex = 95
        ' 
        ' lblClearDates
        ' 
        lblClearDates.BackColor = SystemColors.Control
        lblClearDates.Image = CType(resources.GetObject("lblClearDates.Image"), Image)
        lblClearDates.Location = New Point(390, 163)
        lblClearDates.Name = "lblClearDates"
        lblClearDates.Size = New Size(33, 46)
        lblClearDates.TabIndex = 96
        lblClearDates.Text = "      "
        ' 
        ' dtpDateFrom
        ' 
        dtpDateFrom.CustomFormat = " "
        dtpDateFrom.Format = DateTimePickerFormat.Custom
        dtpDateFrom.Location = New Point(25, 167)
        dtpDateFrom.Margin = New Padding(3, 4, 3, 4)
        dtpDateFrom.Name = "dtpDateFrom"
        dtpDateFrom.Size = New Size(136, 31)
        dtpDateFrom.TabIndex = 94
        ' 
        ' frmBillingDash
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        AutoSize = True
        AutoSizeMode = AutoSizeMode.GrowAndShrink
        ClientSize = New Size(1502, 1300)
        Controls.Add(dtpDateTo)
        Controls.Add(lblClearDates)
        Controls.Add(dtpDateFrom)
        Controls.Add(StatusStrip1)
        Controls.Add(claimpath)
        Controls.Add(viewClaim)
        Controls.Add(Label34)
        Controls.Add(txtPatientID)
        Controls.Add(btnPatLook)
        Controls.Add(clipboardMsg)
        Controls.Add(btn837Lookup)
        Controls.Add(Label18)
        Controls.Add(txt837File)
        Controls.Add(Label17)
        Controls.Add(Label8)
        Controls.Add(btnSellT)
        Controls.Add(btnDeselT)
        Controls.Add(Label1)
        Controls.Add(dgvDiscrete)
        Controls.Add(btnTarget)
        Controls.Add(lstTargets)
        Controls.Add(Label2)
        Controls.Add(Label5)
        Controls.Add(txtAccTo)
        Controls.Add(Label4)
        Controls.Add(lblTo)
        Controls.Add(txtAccFrom)
        Controls.Add(lblFrom)
        Controls.Add(btnLoad)
        Controls.Add(TB)
        Controls.Add(ToolStrip1)
        Controls.Add(txtOutput)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        Name = "frmBillingDash"
        Text = "Billing Dashboard"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        TB.ResumeLayout(False)
        tpUnsynched.ResumeLayout(False)
        tpUnsynched.PerformLayout()
        CType(dgvUnsynchAccs, ComponentModel.ISupportInitialize).EndInit()
        tpUnbilled.ResumeLayout(False)
        tpUnbilled.PerformLayout()
        CType(dgvSynchAccs, ComponentModel.ISupportInitialize).EndInit()
        tpBilled.ResumeLayout(False)
        tpBilled.PerformLayout()
        CType(dgvBilled, ComponentModel.ISupportInitialize).EndInit()
        tpProcessed.ResumeLayout(False)
        tpProcessed.PerformLayout()
        CType(dgvProcPats, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvProcTPs, ComponentModel.ISupportInitialize).EndInit()
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        CType(dgvDiscrete, ComponentModel.ISupportInitialize).EndInit()
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        CType(DataGridView2, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents cmbBillType As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents TB As System.Windows.Forms.TabControl
    Friend WithEvents tpUnsynched As System.Windows.Forms.TabPage
    Friend WithEvents tpUnbilled As System.Windows.Forms.TabPage
    Friend WithEvents tpBilled As System.Windows.Forms.TabPage
    Friend WithEvents dgvUnsynchAccs As System.Windows.Forms.DataGridView
    Friend WithEvents btnSynchronize As System.Windows.Forms.Button
    Friend WithEvents btnSelUs As System.Windows.Forms.Button
    Friend WithEvents btnDeselUS As System.Windows.Forms.Button
    Friend WithEvents btnBill As System.Windows.Forms.Button
    Friend WithEvents btnUnsynch As System.Windows.Forms.Button
    Friend WithEvents btnSelS As System.Windows.Forms.Button
    Friend WithEvents btnDeselS As System.Windows.Forms.Button
    Friend WithEvents dgvSynchAccs As System.Windows.Forms.DataGridView
    Friend WithEvents btnProcess As System.Windows.Forms.Button
    Friend WithEvents btnSelB As System.Windows.Forms.Button
    Friend WithEvents btnDeselB As System.Windows.Forms.Button
    Friend WithEvents dgvBilled As System.Windows.Forms.DataGridView
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtAccTo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents txtAccFrom As System.Windows.Forms.TextBox
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents txtOutput As System.Windows.Forms.TextBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PB As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtUnsynchCount As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lstTargets As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnTarget As System.Windows.Forms.Button
    Friend WithEvents chkFinals As System.Windows.Forms.CheckBox
    Friend WithEvents chkDot As System.Windows.Forms.CheckBox
    Friend WithEvents chkLAE As System.Windows.Forms.CheckBox
    Friend WithEvents chkTNP As System.Windows.Forms.CheckBox
    Friend WithEvents chkQNS As System.Windows.Forms.CheckBox
    Friend WithEvents chkStS As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents AccID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents sPatient As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RecDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RepDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Client As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvDiscrete As System.Windows.Forms.DataGridView
    Friend WithEvents Discrete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnSellT As System.Windows.Forms.Button
    Friend WithEvents btnDeselT As System.Windows.Forms.Button
    Friend WithEvents BW As System.ComponentModel.BackgroundWorker
    Friend WithEvents DataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents AccIDS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PatientS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SvcDateS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AmountS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Billee As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dtpBillDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtSynchCount As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtBCount As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbDestination As System.Windows.Forms.ComboBox
    Friend WithEvents chkNZero As System.Windows.Forms.CheckBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnReverse As System.Windows.Forms.Button
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents chkValidate As System.Windows.Forms.CheckBox
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cmbClearingHouse As System.Windows.Forms.ComboBox
    Friend WithEvents chkFileless As System.Windows.Forms.CheckBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewCheckBoxColumn3 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tpProcessed As System.Windows.Forms.TabPage
    Friend WithEvents dgvProcTPs As System.Windows.Forms.DataGridView
    Friend WithEvents btnUnprocess As System.Windows.Forms.Button
    Friend WithEvents txtProcessed As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnSelP As System.Windows.Forms.Button
    Friend WithEvents btnDeselP As System.Windows.Forms.Button
    Friend WithEvents btnFileOutput As System.Windows.Forms.Button
    Friend WithEvents DataGridViewCheckBoxColumn2 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents InvID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PatientsB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SvcDateB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BillDateB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AmountB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Billees As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Email As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txt837File As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents btn837Lookup As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents dgvProcPats As System.Windows.Forms.DataGridView
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewCheckBoxColumn5 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PatCheck As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents PatInVID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PatAccID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PatPatient As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PatSDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PatBDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PatAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PatPrints As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PatEmail As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TPCheck As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents FileNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ClearingHouse As System.Windows.Forms.DataGridViewTextBoxColumn
    Shadows WithEvents Created As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChCount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FileAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents clipboardMsg As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents txtPatientID As System.Windows.Forms.TextBox
    Friend WithEvents btnPatLook As System.Windows.Forms.Button
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents Button10 As System.Windows.Forms.Button
    Friend WithEvents Button11 As System.Windows.Forms.Button
    Friend WithEvents viewClaim As System.Windows.Forms.Button
    Friend WithEvents claimpath As System.Windows.Forms.TextBox
    Friend WithEvents dtpDateTo As DateTimePicker
    Friend WithEvents lblClearDates As Label
    Friend WithEvents dtpDateFrom As DateTimePicker
End Class
