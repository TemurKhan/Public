<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScrubber
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmScrubber))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        GroupBox1 = New GroupBox()
        lblClearDates = New Label()
        dtpDateTo = New DateTimePicker()
        Label6 = New Label()
        dtpDateFrom = New DateTimePicker()
        txtClientID = New TextBox()
        chkClientAllSpec = New CheckBox()
        btnTarget = New Button()
        btnSel = New Button()
        btnDesel = New Button()
        lstTargets = New CheckedListBox()
        Label43 = New Label()
        cmbABU = New ComboBox()
        Label5 = New Label()
        txtAccTo = New TextBox()
        Label4 = New Label()
        lblTo = New Label()
        btnLoad = New Button()
        txtAccFrom = New TextBox()
        lblFrom = New Label()
        GroupBox2 = New GroupBox()
        Label9 = New Label()
        txtMissingRecs = New TextBox()
        txtAccDate = New MaskedTextBox()
        Label7 = New Label()
        Label3 = New Label()
        txtAccID = New TextBox()
        btnFirst = New Button()
        btnPrevious = New Button()
        btnNext = New Button()
        btnLast = New Button()
        txtNavStatus = New TextBox()
        Label10 = New Label()
        dgvDXs = New DataGridView()
        SNo = New DataGridViewTextBoxColumn()
        ICD9 = New DataGridViewTextBoxColumn()
        LookUp = New DataGridViewImageColumn()
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
        Label11 = New Label()
        txtPayerName = New TextBox()
        txtPayerID = New TextBox()
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
        btnPayerLookUp = New Button()
        Label16 = New Label()
        chkTPBill = New CheckBox()
        Label14 = New Label()
        tpPatient = New TabPage()
        Label52 = New Label()
        Label51 = New Label()
        Label50 = New Label()
        Label49 = New Label()
        Label48 = New Label()
        Label47 = New Label()
        Label8 = New Label()
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
        txtPatDOB = New MaskedTextBox()
        Label25 = New Label()
        Label26 = New Label()
        txtPatAdd1 = New TextBox()
        Label27 = New Label()
        txtPatLName = New TextBox()
        chkPatientBill = New CheckBox()
        Label15 = New Label()
        btnPatLook = New Button()
        pctPatSex = New PictureBox()
        tpCharges = New TabPage()
        dgvCharges = New DataGridView()
        DEL = New DataGridViewImageColumn()
        TGPID = New DataGridViewTextBoxColumn()
        TGPName = New DataGridViewTextBoxColumn()
        CPT = New DataGridViewTextBoxColumn()
        Dx = New DataGridViewTextBoxColumn()
        Price = New DataGridViewTextBoxColumn()
        Label55 = New Label()
        txtWorkCmnt = New TextBox()
        ToolStrip1 = New ToolStrip()
        btnSave = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnReport = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        btnHelp = New ToolStripButton()
        gbIndicator = New GroupBox()
        lblCodes = New Label()
        lblPatient = New Label()
        lbl3P = New Label()
        lblClient = New Label()
        btnEECC = New Button()
        lblEligibility = New Button()
        DataGridViewImageColumn1 = New DataGridViewImageColumn()
        DataGridViewImageColumn2 = New DataGridViewImageColumn()
        btnDxSync = New Button()
        loading = New Label()
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
        CType(dgvDXs, ComponentModel.ISupportInitialize).BeginInit()
        TabControl1.SuspendLayout()
        tpClient.SuspendLayout()
        tpThirdParty.SuspendLayout()
        gbTP.SuspendLayout()
        CType(pctInsSex, ComponentModel.ISupportInitialize).BeginInit()
        tpPatient.SuspendLayout()
        CType(pctPatSex, ComponentModel.ISupportInitialize).BeginInit()
        tpCharges.SuspendLayout()
        CType(dgvCharges, ComponentModel.ISupportInitialize).BeginInit()
        ToolStrip1.SuspendLayout()
        gbIndicator.SuspendLayout()
        SuspendLayout()
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(lblClearDates)
        GroupBox1.Controls.Add(dtpDateTo)
        GroupBox1.Controls.Add(Label6)
        GroupBox1.Controls.Add(dtpDateFrom)
        GroupBox1.Controls.Add(txtClientID)
        GroupBox1.Controls.Add(chkClientAllSpec)
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
        GroupBox1.Location = New Point(20, 79)
        GroupBox1.Margin = New Padding(5, 6, 5, 6)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(5, 6, 5, 6)
        GroupBox1.Size = New Size(1298, 227)
        GroupBox1.TabIndex = 5
        GroupBox1.TabStop = False
        GroupBox1.Text = "Selection"
        ' 
        ' lblClearDates
        ' 
        lblClearDates.BackColor = SystemColors.Control
        lblClearDates.Image = My.Resources.Resources.Eraser
        lblClearDates.Location = New Point(325, 54)
        lblClearDates.Name = "lblClearDates"
        lblClearDates.Size = New Size(47, 46)
        lblClearDates.TabIndex = 87
        lblClearDates.Text = "      "
        ' 
        ' dtpDateTo
        ' 
        dtpDateTo.CustomFormat = " "
        dtpDateTo.Format = DateTimePickerFormat.Custom
        dtpDateTo.Location = New Point(173, 62)
        dtpDateTo.Margin = New Padding(3, 4, 3, 4)
        dtpDateTo.Name = "dtpDateTo"
        dtpDateTo.Size = New Size(136, 31)
        dtpDateTo.TabIndex = 85
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.DarkBlue
        Label6.Location = New Point(557, 31)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(100, 25)
        Label6.TabIndex = 18
        Label6.Text = "Client ID"
        ' 
        ' dtpDateFrom
        ' 
        dtpDateFrom.CustomFormat = " "
        dtpDateFrom.Format = DateTimePickerFormat.Custom
        dtpDateFrom.Location = New Point(22, 62)
        dtpDateFrom.Margin = New Padding(3, 4, 3, 4)
        dtpDateFrom.Name = "dtpDateFrom"
        dtpDateFrom.Size = New Size(139, 31)
        dtpDateFrom.TabIndex = 85
        ' 
        ' txtClientID
        ' 
        txtClientID.Enabled = False
        txtClientID.Location = New Point(550, 62)
        txtClientID.Margin = New Padding(5, 6, 5, 6)
        txtClientID.MaxLength = 10
        txtClientID.Name = "txtClientID"
        txtClientID.Size = New Size(126, 31)
        txtClientID.TabIndex = 6
        txtClientID.TextAlign = HorizontalAlignment.Center
        ' 
        ' chkClientAllSpec
        ' 
        chkClientAllSpec.Location = New Point(417, 52)
        chkClientAllSpec.Margin = New Padding(5, 6, 5, 6)
        chkClientAllSpec.Name = "chkClientAllSpec"
        chkClientAllSpec.Size = New Size(123, 58)
        chkClientAllSpec.TabIndex = 5
        chkClientAllSpec.Text = "All Clients"
        chkClientAllSpec.UseVisualStyleBackColor = True
        ' 
        ' btnTarget
        ' 
        btnTarget.ForeColor = Color.DarkBlue
        btnTarget.Image = CType(resources.GetObject("btnTarget.Image"), Image)
        btnTarget.Location = New Point(557, 152)
        btnTarget.Margin = New Padding(5, 6, 5, 6)
        btnTarget.Name = "btnTarget"
        btnTarget.Size = New Size(85, 58)
        btnTarget.TabIndex = 8
        btnTarget.TextAlign = ContentAlignment.MiddleRight
        btnTarget.TextImageRelation = TextImageRelation.ImageBeforeText
        btnTarget.UseVisualStyleBackColor = True
        ' 
        ' btnSel
        ' 
        btnSel.ForeColor = Color.DarkBlue
        btnSel.Image = CType(resources.GetObject("btnSel.Image"), Image)
        btnSel.Location = New Point(1230, 94)
        btnSel.Margin = New Padding(5, 6, 5, 6)
        btnSel.Name = "btnSel"
        btnSel.Size = New Size(58, 58)
        btnSel.TabIndex = 11
        btnSel.TextAlign = ContentAlignment.MiddleRight
        btnSel.TextImageRelation = TextImageRelation.ImageBeforeText
        btnSel.UseVisualStyleBackColor = True
        ' 
        ' btnDesel
        ' 
        btnDesel.ForeColor = Color.DarkBlue
        btnDesel.Image = CType(resources.GetObject("btnDesel.Image"), Image)
        btnDesel.Location = New Point(1230, 31)
        btnDesel.Margin = New Padding(5, 6, 5, 6)
        btnDesel.Name = "btnDesel"
        btnDesel.Size = New Size(58, 58)
        btnDesel.TabIndex = 10
        btnDesel.TextAlign = ContentAlignment.MiddleRight
        btnDesel.TextImageRelation = TextImageRelation.ImageBeforeText
        btnDesel.UseVisualStyleBackColor = True
        ' 
        ' lstTargets
        ' 
        lstTargets.FormattingEnabled = True
        lstTargets.Location = New Point(690, 31)
        lstTargets.Margin = New Padding(5, 6, 5, 6)
        lstTargets.Name = "lstTargets"
        lstTargets.Size = New Size(527, 116)
        lstTargets.TabIndex = 9
        ' 
        ' Label43
        ' 
        Label43.ForeColor = Color.DarkBlue
        Label43.Location = New Point(327, 127)
        Label43.Margin = New Padding(5, 0, 5, 0)
        Label43.Name = "Label43"
        Label43.Size = New Size(123, 25)
        Label43.TabIndex = 14
        Label43.Text = "Status"
        ' 
        ' cmbABU
        ' 
        cmbABU.DropDownStyle = ComboBoxStyle.DropDownList
        cmbABU.FormattingEnabled = True
        cmbABU.Items.AddRange(New Object() {"ALL", "SCRUBBED", "UNSCRUBBED"})
        cmbABU.Location = New Point(327, 163)
        cmbABU.Margin = New Padding(5, 6, 5, 6)
        cmbABU.Name = "cmbABU"
        cmbABU.Size = New Size(217, 33)
        cmbABU.TabIndex = 7
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.DarkBlue
        Label5.Location = New Point(178, 127)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(138, 25)
        Label5.TabIndex = 9
        Label5.Text = "Accession To"
        ' 
        ' txtAccTo
        ' 
        txtAccTo.Location = New Point(173, 163)
        txtAccTo.Margin = New Padding(5, 6, 5, 6)
        txtAccTo.MaxLength = 12
        txtAccTo.Name = "txtAccTo"
        txtAccTo.Size = New Size(141, 31)
        txtAccTo.TabIndex = 4
        txtAccTo.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(22, 127)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(138, 25)
        Label4.TabIndex = 7
        Label4.Text = "Accession From"
        ' 
        ' lblTo
        ' 
        lblTo.ForeColor = Color.DarkBlue
        lblTo.Location = New Point(173, 31)
        lblTo.Margin = New Padding(5, 0, 5, 0)
        lblTo.Name = "lblTo"
        lblTo.Size = New Size(158, 25)
        lblTo.TabIndex = 4
        lblTo.Text = "To"
        ' 
        ' btnLoad
        ' 
        btnLoad.ForeColor = Color.DarkBlue
        btnLoad.Image = CType(resources.GetObject("btnLoad.Image"), Image)
        btnLoad.Location = New Point(1230, 158)
        btnLoad.Margin = New Padding(5, 6, 5, 6)
        btnLoad.Name = "btnLoad"
        btnLoad.Size = New Size(58, 58)
        btnLoad.TabIndex = 12
        btnLoad.TextAlign = ContentAlignment.MiddleRight
        btnLoad.TextImageRelation = TextImageRelation.ImageBeforeText
        btnLoad.UseVisualStyleBackColor = True
        ' 
        ' txtAccFrom
        ' 
        txtAccFrom.Location = New Point(20, 163)
        txtAccFrom.Margin = New Padding(5, 6, 5, 6)
        txtAccFrom.MaxLength = 12
        txtAccFrom.Name = "txtAccFrom"
        txtAccFrom.Size = New Size(141, 31)
        txtAccFrom.TabIndex = 3
        txtAccFrom.TextAlign = HorizontalAlignment.Center
        ' 
        ' lblFrom
        ' 
        lblFrom.ForeColor = Color.DarkBlue
        lblFrom.Location = New Point(22, 31)
        lblFrom.Margin = New Padding(5, 0, 5, 0)
        lblFrom.Name = "lblFrom"
        lblFrom.Size = New Size(180, 25)
        lblFrom.TabIndex = 0
        lblFrom.Text = "From"
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(Label9)
        GroupBox2.Controls.Add(txtMissingRecs)
        GroupBox2.Controls.Add(txtAccDate)
        GroupBox2.Controls.Add(Label7)
        GroupBox2.Controls.Add(Label3)
        GroupBox2.Controls.Add(txtAccID)
        GroupBox2.Controls.Add(btnFirst)
        GroupBox2.Controls.Add(btnPrevious)
        GroupBox2.Controls.Add(btnNext)
        GroupBox2.Controls.Add(btnLast)
        GroupBox2.Controls.Add(txtNavStatus)
        GroupBox2.Location = New Point(20, 317)
        GroupBox2.Margin = New Padding(5, 6, 5, 6)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Padding = New Padding(5, 6, 5, 6)
        GroupBox2.Size = New Size(847, 127)
        GroupBox2.TabIndex = 6
        GroupBox2.TabStop = False
        GroupBox2.Text = "Navigation"
        ' 
        ' Label9
        ' 
        Label9.ForeColor = Color.DarkBlue
        Label9.Location = New Point(705, 25)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(120, 25)
        Label9.TabIndex = 23
        Label9.Text = "Missing Recs"
        ' 
        ' txtMissingRecs
        ' 
        txtMissingRecs.Location = New Point(688, 56)
        txtMissingRecs.Margin = New Padding(5, 6, 5, 6)
        txtMissingRecs.MaxLength = 10
        txtMissingRecs.Name = "txtMissingRecs"
        txtMissingRecs.ReadOnly = True
        txtMissingRecs.Size = New Size(134, 31)
        txtMissingRecs.TabIndex = 22
        txtMissingRecs.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtAccDate
        ' 
        txtAccDate.Location = New Point(557, 56)
        txtAccDate.Margin = New Padding(5, 6, 5, 6)
        txtAccDate.Mask = "00/00/0000"
        txtAccDate.Name = "txtAccDate"
        txtAccDate.ReadOnly = True
        txtAccDate.Size = New Size(119, 31)
        txtAccDate.TabIndex = 21
        txtAccDate.ValidatingType = GetType(Date)
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.DarkBlue
        Label7.Location = New Point(567, 25)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(97, 25)
        Label7.TabIndex = 20
        Label7.Text = "Dated"
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(402, 25)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(133, 25)
        Label3.TabIndex = 19
        Label3.Text = "Accession"
        ' 
        ' txtAccID
        ' 
        txtAccID.Location = New Point(395, 56)
        txtAccID.Margin = New Padding(5, 6, 5, 6)
        txtAccID.MaxLength = 12
        txtAccID.Name = "txtAccID"
        txtAccID.ReadOnly = True
        txtAccID.Size = New Size(149, 31)
        txtAccID.TabIndex = 18
        txtAccID.TextAlign = HorizontalAlignment.Center
        ' 
        ' btnFirst
        ' 
        btnFirst.Enabled = False
        btnFirst.Image = CType(resources.GetObject("btnFirst.Image"), Image)
        btnFirst.Location = New Point(10, 48)
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
        btnPrevious.Location = New Point(70, 48)
        btnPrevious.Margin = New Padding(5, 6, 5, 6)
        btnPrevious.Name = "btnPrevious"
        btnPrevious.Size = New Size(48, 50)
        btnPrevious.TabIndex = 14
        btnPrevious.UseVisualStyleBackColor = True
        ' 
        ' btnNext
        ' 
        btnNext.Enabled = False
        btnNext.Image = CType(resources.GetObject("btnNext.Image"), Image)
        btnNext.Location = New Point(267, 48)
        btnNext.Margin = New Padding(5, 6, 5, 6)
        btnNext.Name = "btnNext"
        btnNext.Size = New Size(53, 50)
        btnNext.TabIndex = 12
        btnNext.UseVisualStyleBackColor = True
        ' 
        ' btnLast
        ' 
        btnLast.Enabled = False
        btnLast.Image = CType(resources.GetObject("btnLast.Image"), Image)
        btnLast.Location = New Point(330, 48)
        btnLast.Margin = New Padding(5, 6, 5, 6)
        btnLast.Name = "btnLast"
        btnLast.Size = New Size(55, 50)
        btnLast.TabIndex = 13
        btnLast.UseVisualStyleBackColor = True
        ' 
        ' txtNavStatus
        ' 
        txtNavStatus.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtNavStatus.Location = New Point(128, 56)
        txtNavStatus.Margin = New Padding(5, 6, 5, 6)
        txtNavStatus.Name = "txtNavStatus"
        txtNavStatus.ReadOnly = True
        txtNavStatus.Size = New Size(126, 26)
        txtNavStatus.TabIndex = 8
        txtNavStatus.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label10
        ' 
        Label10.ForeColor = Color.DarkBlue
        Label10.Location = New Point(20, 481)
        Label10.Margin = New Padding(5, 0, 5, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(128, 25)
        Label10.TabIndex = 22
        Label10.Text = "Dx Codes"
        ' 
        ' dgvDXs
        ' 
        dgvDXs.AllowUserToAddRows = False
        dgvDXs.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(255), CByte(192), CByte(255))
        dgvDXs.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvDXs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvDXs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvDXs.Columns.AddRange(New DataGridViewColumn() {SNo, ICD9, LookUp})
        dgvDXs.Location = New Point(20, 517)
        dgvDXs.Margin = New Padding(5, 6, 5, 6)
        dgvDXs.Name = "dgvDXs"
        dgvDXs.RowHeadersVisible = False
        dgvDXs.RowHeadersWidth = 51
        dgvDXs.ScrollBars = ScrollBars.Vertical
        dgvDXs.Size = New Size(230, 446)
        dgvDXs.TabIndex = 21
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
        ' 
        ' ICD9
        ' 
        ICD9.FillWeight = 66F
        ICD9.HeaderText = "Dx Code"
        ICD9.MaxInputLength = 12
        ICD9.MinimumWidth = 6
        ICD9.Name = "ICD9"
        ICD9.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' LookUp
        ' 
        LookUp.FillWeight = 30F
        LookUp.HeaderText = ""
        LookUp.Image = CType(resources.GetObject("LookUp.Image"), Image)
        LookUp.MinimumWidth = 6
        LookUp.Name = "LookUp"
        LookUp.Resizable = DataGridViewTriState.True
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(tpClient)
        TabControl1.Controls.Add(tpThirdParty)
        TabControl1.Controls.Add(tpPatient)
        TabControl1.Controls.Add(tpCharges)
        TabControl1.Location = New Point(268, 475)
        TabControl1.Margin = New Padding(5, 6, 5, 6)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(1040, 502)
        TabControl1.SizeMode = TabSizeMode.Fixed
        TabControl1.TabIndex = 25
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
        tpClient.Size = New Size(1032, 464)
        tpClient.TabIndex = 1
        tpClient.Text = "Client"
        tpClient.UseVisualStyleBackColor = True
        ' 
        ' Label57
        ' 
        Label57.ForeColor = Color.DarkBlue
        Label57.Location = New Point(352, 198)
        Label57.Margin = New Padding(5, 0, 5, 0)
        Label57.Name = "Label57"
        Label57.Size = New Size(75, 25)
        Label57.TabIndex = 66
        Label57.Text = "NPI"
        Label57.TextAlign = ContentAlignment.TopRight
        ' 
        ' txtNPI
        ' 
        txtNPI.Location = New Point(437, 192)
        txtNPI.Margin = New Padding(5, 6, 5, 6)
        txtNPI.Name = "txtNPI"
        txtNPI.ReadOnly = True
        txtNPI.Size = New Size(159, 31)
        txtNPI.TabIndex = 65
        ' 
        ' txtProvFax
        ' 
        txtProvFax.Location = New Point(405, 346)
        txtProvFax.Margin = New Padding(5, 6, 5, 6)
        txtProvFax.Name = "txtProvFax"
        txtProvFax.ReadOnly = True
        txtProvFax.Size = New Size(191, 31)
        txtProvFax.TabIndex = 64
        ' 
        ' txtProvPhone
        ' 
        txtProvPhone.Location = New Point(40, 346)
        txtProvPhone.Margin = New Padding(5, 6, 5, 6)
        txtProvPhone.Name = "txtProvPhone"
        txtProvPhone.ReadOnly = True
        txtProvPhone.Size = New Size(191, 31)
        txtProvPhone.TabIndex = 63
        ' 
        ' lstAttending
        ' 
        lstAttending.FormattingEnabled = True
        lstAttending.Location = New Point(623, 135)
        lstAttending.Margin = New Padding(5, 6, 5, 6)
        lstAttending.Name = "lstAttending"
        lstAttending.Size = New Size(387, 200)
        lstAttending.TabIndex = 62
        ' 
        ' chkProvContract
        ' 
        chkProvContract.ForeColor = Color.DarkBlue
        chkProvContract.Location = New Point(352, 37)
        chkProvContract.Margin = New Padding(5, 6, 5, 6)
        chkProvContract.Name = "chkProvContract"
        chkProvContract.Size = New Size(112, 40)
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
        Label38.Size = New Size(107, 25)
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
        cmbProvPrice.Size = New Size(226, 33)
        cmbProvPrice.TabIndex = 18
        ' 
        ' Label37
        ' 
        Label37.ForeColor = Color.DarkBlue
        Label37.Location = New Point(618, 96)
        Label37.Margin = New Padding(5, 0, 5, 0)
        Label37.Name = "Label37"
        Label37.Size = New Size(115, 25)
        Label37.TabIndex = 42
        Label37.Text = "Attending"
        ' 
        ' Label36
        ' 
        Label36.ForeColor = Color.DarkBlue
        Label36.Location = New Point(253, 96)
        Label36.Margin = New Padding(5, 0, 5, 0)
        Label36.Name = "Label36"
        Label36.Size = New Size(187, 25)
        Label36.TabIndex = 40
        Label36.Text = "Ordering Provider"
        ' 
        ' Label35
        ' 
        Label35.ForeColor = Color.DarkBlue
        Label35.Location = New Point(402, 315)
        Label35.Margin = New Padding(5, 0, 5, 0)
        Label35.Name = "Label35"
        Label35.Size = New Size(115, 25)
        Label35.TabIndex = 38
        Label35.Text = "Fax"
        ' 
        ' Label34
        ' 
        Label34.ForeColor = Color.DarkBlue
        Label34.Location = New Point(42, 315)
        Label34.Margin = New Padding(5, 0, 5, 0)
        Label34.Name = "Label34"
        Label34.Size = New Size(120, 25)
        Label34.TabIndex = 36
        Label34.Text = "Phone"
        ' 
        ' Label33
        ' 
        Label33.ForeColor = Color.DarkBlue
        Label33.Location = New Point(40, 212)
        Label33.Margin = New Padding(5, 0, 5, 0)
        Label33.Name = "Label33"
        Label33.Size = New Size(133, 25)
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
        txtProviderAddress.Size = New Size(556, 31)
        txtProviderAddress.TabIndex = 22
        ' 
        ' txtProviderName
        ' 
        txtProviderName.Location = New Point(243, 135)
        txtProviderName.Margin = New Padding(5, 6, 5, 6)
        txtProviderName.MaxLength = 105
        txtProviderName.Name = "txtProviderName"
        txtProviderName.ReadOnly = True
        txtProviderName.Size = New Size(352, 31)
        txtProviderName.TabIndex = 21
        ' 
        ' btnProviderLook
        ' 
        btnProviderLook.Image = CType(resources.GetObject("btnProviderLook.Image"), Image)
        btnProviderLook.Location = New Point(183, 127)
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
        chkClientBill.Location = New Point(183, 29)
        chkClientBill.Margin = New Padding(5, 6, 5, 6)
        chkClientBill.Name = "chkClientBill"
        chkClientBill.Size = New Size(112, 52)
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
        Label13.Size = New Size(148, 25)
        Label13.TabIndex = 10
        Label13.Text = "Party to be billed"
        Label13.TextAlign = ContentAlignment.TopRight
        ' 
        ' Label12
        ' 
        Label12.ForeColor = Color.DarkBlue
        Label12.Location = New Point(40, 96)
        Label12.Margin = New Padding(5, 0, 5, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(133, 25)
        Label12.TabIndex = 9
        Label12.Text = "Provider ID"
        ' 
        ' txtProviderID
        ' 
        txtProviderID.Location = New Point(40, 135)
        txtProviderID.Margin = New Padding(5, 6, 5, 6)
        txtProviderID.MaxLength = 10
        txtProviderID.Name = "txtProviderID"
        txtProviderID.ReadOnly = True
        txtProviderID.Size = New Size(131, 31)
        txtProviderID.TabIndex = 19
        txtProviderID.TextAlign = HorizontalAlignment.Center
        ' 
        ' tpThirdParty
        ' 
        tpThirdParty.Controls.Add(gbTP)
        tpThirdParty.Location = New Point(4, 34)
        tpThirdParty.Margin = New Padding(5, 6, 5, 6)
        tpThirdParty.Name = "tpThirdParty"
        tpThirdParty.Size = New Size(1032, 464)
        tpThirdParty.TabIndex = 2
        tpThirdParty.Text = "Third Party"
        tpThirdParty.UseVisualStyleBackColor = True
        ' 
        ' gbTP
        ' 
        gbTP.Controls.Add(Label11)
        gbTP.Controls.Add(txtPayerName)
        gbTP.Controls.Add(txtPayerID)
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
        gbTP.Controls.Add(btnPayerLookUp)
        gbTP.Controls.Add(Label16)
        gbTP.Controls.Add(chkTPBill)
        gbTP.Controls.Add(Label14)
        gbTP.Location = New Point(12, 6)
        gbTP.Margin = New Padding(5, 6, 5, 6)
        gbTP.Name = "gbTP"
        gbTP.Padding = New Padding(5, 6, 5, 6)
        gbTP.Size = New Size(1025, 421)
        gbTP.TabIndex = 63
        gbTP.TabStop = False
        ' 
        ' Label11
        ' 
        Label11.ForeColor = Color.Red
        Label11.Location = New Point(33, 108)
        Label11.Margin = New Padding(5, 0, 5, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(110, 25)
        Label11.TabIndex = 65
        Label11.Text = "Payer ID"
        ' 
        ' txtPayerName
        ' 
        txtPayerName.Location = New Point(250, 144)
        txtPayerName.Margin = New Padding(5, 6, 5, 6)
        txtPayerName.MaxLength = 25
        txtPayerName.Name = "txtPayerName"
        txtPayerName.ReadOnly = True
        txtPayerName.Size = New Size(546, 31)
        txtPayerName.TabIndex = 64
        ' 
        ' txtPayerID
        ' 
        txtPayerID.Location = New Point(30, 144)
        txtPayerID.Margin = New Padding(5, 6, 5, 6)
        txtPayerID.MaxLength = 25
        txtPayerID.Name = "txtPayerID"
        txtPayerID.ReadOnly = True
        txtPayerID.Size = New Size(147, 31)
        txtPayerID.TabIndex = 63
        ' 
        ' chkTPContract
        ' 
        chkTPContract.Checked = True
        chkTPContract.CheckState = CheckState.Checked
        chkTPContract.ForeColor = Color.DarkBlue
        chkTPContract.Location = New Point(390, 44)
        chkTPContract.Margin = New Padding(5, 6, 5, 6)
        chkTPContract.Name = "chkTPContract"
        chkTPContract.Size = New Size(125, 40)
        chkTPContract.TabIndex = 28
        chkTPContract.Text = "Contract"
        chkTPContract.UseVisualStyleBackColor = True
        ' 
        ' Label39
        ' 
        Label39.ForeColor = Color.DarkBlue
        Label39.Location = New Point(692, 50)
        Label39.Margin = New Padding(5, 0, 5, 0)
        Label39.Name = "Label39"
        Label39.Size = New Size(107, 25)
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
        cmbTPPrice.Size = New Size(182, 33)
        cmbTPPrice.TabIndex = 29
        ' 
        ' txtInsDOB
        ' 
        txtInsDOB.Location = New Point(278, 358)
        txtInsDOB.Margin = New Padding(5, 6, 5, 6)
        txtInsDOB.Mask = "00/00/0000"
        txtInsDOB.Name = "txtInsDOB"
        txtInsDOB.Size = New Size(131, 31)
        txtInsDOB.TabIndex = 39
        txtInsDOB.ValidatingType = GetType(Date)
        ' 
        ' pctInsSex
        ' 
        pctInsSex.ErrorImage = CType(resources.GetObject("pctInsSex.ErrorImage"), Image)
        pctInsSex.InitialImage = CType(resources.GetObject("pctInsSex.InitialImage"), Image)
        pctInsSex.Location = New Point(237, 358)
        pctInsSex.Margin = New Padding(5, 6, 5, 6)
        pctInsSex.Name = "pctInsSex"
        pctInsSex.Size = New Size(32, 37)
        pctInsSex.TabIndex = 38
        pctInsSex.TabStop = False
        ' 
        ' Label24
        ' 
        Label24.ForeColor = Color.Fuchsia
        Label24.Location = New Point(273, 327)
        Label24.Margin = New Padding(5, 0, 5, 0)
        Label24.Name = "Label24"
        Label24.Size = New Size(72, 25)
        Label24.TabIndex = 37
        Label24.Text = "DOB"
        ' 
        ' txtInsSex
        ' 
        txtInsSex.Location = New Point(268, 217)
        txtInsSex.Margin = New Padding(5, 6, 5, 6)
        txtInsSex.MaxLength = 6
        txtInsSex.Name = "txtInsSex"
        txtInsSex.Size = New Size(26, 31)
        txtInsSex.TabIndex = 36
        txtInsSex.Visible = False
        ' 
        ' Label23
        ' 
        Label23.ForeColor = Color.Fuchsia
        Label23.Location = New Point(417, 327)
        Label23.Margin = New Padding(5, 0, 5, 0)
        Label23.Name = "Label23"
        Label23.Size = New Size(190, 25)
        Label23.TabIndex = 35
        Label23.Text = "Insured Address"
        ' 
        ' txtInsAddress
        ' 
        txtInsAddress.Location = New Point(422, 358)
        txtInsAddress.Margin = New Padding(5, 6, 5, 6)
        txtInsAddress.MaxLength = 6
        txtInsAddress.Name = "txtInsAddress"
        txtInsAddress.Size = New Size(571, 31)
        txtInsAddress.TabIndex = 40
        ' 
        ' Label22
        ' 
        Label22.ForeColor = Color.Fuchsia
        Label22.Location = New Point(25, 327)
        Label22.Margin = New Padding(5, 0, 5, 0)
        Label22.Name = "Label22"
        Label22.Size = New Size(177, 25)
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
        Label21.ForeColor = Color.Red
        Label21.Location = New Point(560, 223)
        Label21.Margin = New Padding(5, 0, 5, 0)
        Label21.Name = "Label21"
        Label21.Size = New Size(180, 25)
        Label21.TabIndex = 31
        Label21.Text = "Relation to Patient"
        ' 
        ' btnInsLookup
        ' 
        btnInsLookup.Image = CType(resources.GetObject("btnInsLookup.Image"), Image)
        btnInsLookup.Location = New Point(945, 254)
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
        Label20.Size = New Size(117, 25)
        Label20.TabIndex = 29
        Label20.Text = "Insured ID"
        ' 
        ' txtInsuredID
        ' 
        txtInsuredID.Location = New Point(777, 262)
        txtInsuredID.Margin = New Padding(5, 6, 5, 6)
        txtInsuredID.MaxLength = 6
        txtInsuredID.Name = "txtInsuredID"
        txtInsuredID.Size = New Size(137, 31)
        txtInsuredID.TabIndex = 36
        ' 
        ' cmbRelation
        ' 
        cmbRelation.FormattingEnabled = True
        cmbRelation.Items.AddRange(New Object() {"Self", "Spouse", "Son/Daughter", "Other Dependent"})
        cmbRelation.Location = New Point(560, 260)
        cmbRelation.Margin = New Padding(5, 6, 5, 6)
        cmbRelation.Name = "cmbRelation"
        cmbRelation.Size = New Size(187, 33)
        cmbRelation.TabIndex = 35
        ' 
        ' Label19
        ' 
        Label19.ForeColor = Color.DarkBlue
        Label19.Location = New Point(403, 223)
        Label19.Margin = New Padding(5, 0, 5, 0)
        Label19.Name = "Label19"
        Label19.Size = New Size(122, 25)
        Label19.TabIndex = 26
        Label19.Text = "Copayment"
        ' 
        ' txtCopay
        ' 
        txtCopay.Location = New Point(408, 260)
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
        Label18.Location = New Point(32, 231)
        Label18.Margin = New Padding(5, 0, 5, 0)
        Label18.Name = "Label18"
        Label18.Size = New Size(138, 25)
        Label18.TabIndex = 24
        Label18.Text = "Group No"
        ' 
        ' txtGroup
        ' 
        txtGroup.Location = New Point(32, 262)
        txtGroup.Margin = New Padding(5, 6, 5, 6)
        txtGroup.MaxLength = 25
        txtGroup.Name = "txtGroup"
        txtGroup.Size = New Size(342, 31)
        txtGroup.TabIndex = 33
        ' 
        ' Label17
        ' 
        Label17.ForeColor = Color.Red
        Label17.Location = New Point(817, 108)
        Label17.Margin = New Padding(5, 0, 5, 0)
        Label17.Name = "Label17"
        Label17.Size = New Size(152, 25)
        Label17.TabIndex = 22
        Label17.Text = "Policy No"
        ' 
        ' txtPolicy
        ' 
        txtPolicy.Location = New Point(810, 144)
        txtPolicy.Margin = New Padding(5, 6, 5, 6)
        txtPolicy.MaxLength = 25
        txtPolicy.Name = "txtPolicy"
        txtPolicy.Size = New Size(182, 31)
        txtPolicy.TabIndex = 32
        ' 
        ' btnPayerLookUp
        ' 
        btnPayerLookUp.Image = CType(resources.GetObject("btnPayerLookUp.Image"), Image)
        btnPayerLookUp.Location = New Point(190, 137)
        btnPayerLookUp.Margin = New Padding(5, 6, 5, 6)
        btnPayerLookUp.Name = "btnPayerLookUp"
        btnPayerLookUp.Size = New Size(50, 52)
        btnPayerLookUp.TabIndex = 31
        btnPayerLookUp.UseVisualStyleBackColor = True
        ' 
        ' Label16
        ' 
        Label16.ForeColor = Color.DarkBlue
        Label16.Location = New Point(253, 108)
        Label16.Margin = New Padding(5, 0, 5, 0)
        Label16.Name = "Label16"
        Label16.Size = New Size(168, 25)
        Label16.TabIndex = 19
        Label16.Text = "Insurance Company"
        ' 
        ' chkTPBill
        ' 
        chkTPBill.Appearance = Appearance.Button
        chkTPBill.Enabled = False
        chkTPBill.Location = New Point(183, 37)
        chkTPBill.Margin = New Padding(5, 6, 5, 6)
        chkTPBill.Name = "chkTPBill"
        chkTPBill.Size = New Size(112, 52)
        chkTPBill.TabIndex = 27
        chkTPBill.Text = "Yes"
        chkTPBill.TextAlign = ContentAlignment.MiddleCenter
        chkTPBill.UseVisualStyleBackColor = True
        ' 
        ' Label14
        ' 
        Label14.ForeColor = Color.DarkBlue
        Label14.Location = New Point(25, 48)
        Label14.Margin = New Padding(5, 0, 5, 0)
        Label14.Name = "Label14"
        Label14.Size = New Size(148, 25)
        Label14.TabIndex = 16
        Label14.Text = "Party to be billed"
        Label14.TextAlign = ContentAlignment.TopRight
        ' 
        ' tpPatient
        ' 
        tpPatient.Controls.Add(Label52)
        tpPatient.Controls.Add(Label51)
        tpPatient.Controls.Add(Label50)
        tpPatient.Controls.Add(Label49)
        tpPatient.Controls.Add(Label48)
        tpPatient.Controls.Add(Label47)
        tpPatient.Controls.Add(Label8)
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
        tpPatient.Controls.Add(txtPatDOB)
        tpPatient.Controls.Add(Label25)
        tpPatient.Controls.Add(Label26)
        tpPatient.Controls.Add(txtPatAdd1)
        tpPatient.Controls.Add(Label27)
        tpPatient.Controls.Add(txtPatLName)
        tpPatient.Controls.Add(chkPatientBill)
        tpPatient.Controls.Add(Label15)
        tpPatient.Controls.Add(btnPatLook)
        tpPatient.Controls.Add(pctPatSex)
        tpPatient.Location = New Point(4, 34)
        tpPatient.Margin = New Padding(5, 6, 5, 6)
        tpPatient.Name = "tpPatient"
        tpPatient.Size = New Size(1032, 464)
        tpPatient.TabIndex = 3
        tpPatient.Text = "Patient"
        tpPatient.UseVisualStyleBackColor = True
        ' 
        ' Label52
        ' 
        Label52.ForeColor = Color.DarkBlue
        Label52.Location = New Point(692, 102)
        Label52.Margin = New Padding(5, 0, 5, 0)
        Label52.Name = "Label52"
        Label52.Size = New Size(52, 25)
        Label52.TabIndex = 70
        Label52.Text = "Sex"
        ' 
        ' Label51
        ' 
        Label51.ForeColor = Color.DarkBlue
        Label51.Location = New Point(562, 102)
        Label51.Margin = New Padding(5, 0, 5, 0)
        Label51.Name = "Label51"
        Label51.Size = New Size(120, 25)
        Label51.TabIndex = 69
        Label51.Text = "Middle Name"
        ' 
        ' Label50
        ' 
        Label50.ForeColor = Color.DarkBlue
        Label50.Location = New Point(410, 102)
        Label50.Margin = New Padding(5, 0, 5, 0)
        Label50.Name = "Label50"
        Label50.Size = New Size(115, 25)
        Label50.TabIndex = 68
        Label50.Text = "First Name"
        ' 
        ' Label49
        ' 
        Label49.ForeColor = Color.Fuchsia
        Label49.Location = New Point(48, 321)
        Label49.Margin = New Padding(5, 0, 5, 0)
        Label49.Name = "Label49"
        Label49.Size = New Size(130, 25)
        Label49.TabIndex = 67
        Label49.Text = "Zip"
        ' 
        ' Label48
        ' 
        Label48.ForeColor = Color.Fuchsia
        Label48.Location = New Point(750, 212)
        Label48.Margin = New Padding(5, 0, 5, 0)
        Label48.Name = "Label48"
        Label48.Size = New Size(65, 25)
        Label48.TabIndex = 66
        Label48.Text = "ST"
        ' 
        ' Label47
        ' 
        Label47.ForeColor = Color.Fuchsia
        Label47.Location = New Point(570, 212)
        Label47.Margin = New Padding(5, 0, 5, 0)
        Label47.Name = "Label47"
        Label47.Size = New Size(130, 25)
        Label47.TabIndex = 65
        Label47.Text = "City"
        ' 
        ' Label8
        ' 
        Label8.ForeColor = Color.DarkBlue
        Label8.Location = New Point(390, 212)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(130, 25)
        Label8.TabIndex = 64
        Label8.Text = "Address 2"
        ' 
        ' txtPatState
        ' 
        txtPatState.Location = New Point(752, 242)
        txtPatState.Margin = New Padding(5, 6, 5, 6)
        txtPatState.MaxLength = 35
        txtPatState.Name = "txtPatState"
        txtPatState.Size = New Size(117, 31)
        txtPatState.TabIndex = 12
        ' 
        ' txtPatHPhone
        ' 
        txtPatHPhone.Location = New Point(595, 352)
        txtPatHPhone.Margin = New Padding(5, 6, 5, 6)
        txtPatHPhone.Name = "txtPatHPhone"
        txtPatHPhone.Size = New Size(192, 31)
        txtPatHPhone.TabIndex = 15
        ' 
        ' txtPatCell
        ' 
        txtPatCell.Location = New Point(813, 352)
        txtPatCell.Margin = New Padding(5, 6, 5, 6)
        txtPatCell.Name = "txtPatCell"
        txtPatCell.Size = New Size(191, 31)
        txtPatCell.TabIndex = 16
        ' 
        ' txtPatZip
        ' 
        txtPatZip.Location = New Point(47, 352)
        txtPatZip.Margin = New Padding(5, 6, 5, 6)
        txtPatZip.MaxLength = 10
        txtPatZip.Name = "txtPatZip"
        txtPatZip.Size = New Size(127, 31)
        txtPatZip.TabIndex = 13
        ' 
        ' txtPatCity
        ' 
        txtPatCity.Location = New Point(567, 242)
        txtPatCity.Margin = New Padding(5, 6, 5, 6)
        txtPatCity.MaxLength = 35
        txtPatCity.Name = "txtPatCity"
        txtPatCity.Size = New Size(167, 31)
        txtPatCity.TabIndex = 11
        ' 
        ' txtPatAdd2
        ' 
        txtPatAdd2.Location = New Point(395, 242)
        txtPatAdd2.Margin = New Padding(5, 6, 5, 6)
        txtPatAdd2.MaxLength = 35
        txtPatAdd2.Name = "txtPatAdd2"
        txtPatAdd2.Size = New Size(159, 31)
        txtPatAdd2.TabIndex = 10
        ' 
        ' txtPatMName
        ' 
        txtPatMName.Location = New Point(567, 133)
        txtPatMName.Margin = New Padding(5, 6, 5, 6)
        txtPatMName.MaxLength = 35
        txtPatMName.Name = "txtPatMName"
        txtPatMName.Size = New Size(107, 31)
        txtPatMName.TabIndex = 5
        ' 
        ' txtPatFName
        ' 
        txtPatFName.Location = New Point(415, 133)
        txtPatFName.Margin = New Padding(5, 6, 5, 6)
        txtPatFName.MaxLength = 35
        txtPatFName.Name = "txtPatFName"
        txtPatFName.Size = New Size(139, 31)
        txtPatFName.TabIndex = 4
        ' 
        ' Label40
        ' 
        Label40.ForeColor = Color.DarkBlue
        Label40.Location = New Point(683, 44)
        Label40.Margin = New Padding(5, 0, 5, 0)
        Label40.Name = "Label40"
        Label40.Size = New Size(107, 25)
        Label40.TabIndex = 62
        Label40.Text = "Price Level"
        Label40.TextAlign = ContentAlignment.TopRight
        ' 
        ' cmbPatPrice
        ' 
        cmbPatPrice.DropDownStyle = ComboBoxStyle.DropDownList
        cmbPatPrice.FormattingEnabled = True
        cmbPatPrice.Items.AddRange(New Object() {"List Price", "Level 1", "Level 2", "Level 3", "Level 4", "Level 5", "Level 6", "Level 7", "Level 8", "Level 9"})
        cmbPatPrice.Location = New Point(813, 38)
        cmbPatPrice.Margin = New Padding(5, 6, 5, 6)
        cmbPatPrice.Name = "cmbPatPrice"
        cmbPatPrice.Size = New Size(182, 33)
        cmbPatPrice.TabIndex = 42
        ' 
        ' Label32
        ' 
        Label32.ForeColor = Color.Fuchsia
        Label32.Location = New Point(48, 102)
        Label32.Margin = New Padding(5, 0, 5, 0)
        Label32.Name = "Label32"
        Label32.Size = New Size(128, 25)
        Label32.TabIndex = 58
        Label32.Text = "Patient ID"
        ' 
        ' txtPatientID
        ' 
        txtPatientID.Location = New Point(47, 133)
        txtPatientID.Margin = New Padding(5, 6, 5, 6)
        txtPatientID.MaxLength = 6
        txtPatientID.Name = "txtPatientID"
        txtPatientID.Size = New Size(127, 31)
        txtPatientID.TabIndex = 1
        txtPatientID.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtPatSex
        ' 
        txtPatSex.Location = New Point(697, 133)
        txtPatSex.Margin = New Padding(5, 6, 5, 6)
        txtPatSex.MaxLength = 6
        txtPatSex.Name = "txtPatSex"
        txtPatSex.Size = New Size(62, 31)
        txtPatSex.TabIndex = 6
        ' 
        ' Label31
        ' 
        Label31.ForeColor = Color.DarkBlue
        Label31.Location = New Point(808, 321)
        Label31.Margin = New Padding(5, 0, 5, 0)
        Label31.Name = "Label31"
        Label31.Size = New Size(170, 25)
        Label31.TabIndex = 55
        Label31.Text = "Cell Phone"
        ' 
        ' Label30
        ' 
        Label30.ForeColor = Color.DarkBlue
        Label30.Location = New Point(590, 321)
        Label30.Margin = New Padding(5, 0, 5, 0)
        Label30.Name = "Label30"
        Label30.Size = New Size(128, 25)
        Label30.TabIndex = 54
        Label30.Text = "Home Phone"
        ' 
        ' txtPatEmail
        ' 
        txtPatEmail.Location = New Point(187, 352)
        txtPatEmail.Margin = New Padding(5, 6, 5, 6)
        txtPatEmail.MaxLength = 50
        txtPatEmail.Name = "txtPatEmail"
        txtPatEmail.Size = New Size(367, 31)
        txtPatEmail.TabIndex = 14
        ' 
        ' Label29
        ' 
        Label29.ForeColor = Color.DarkBlue
        Label29.Location = New Point(182, 317)
        Label29.Margin = New Padding(5, 0, 5, 0)
        Label29.Name = "Label29"
        Label29.Size = New Size(185, 25)
        Label29.TabIndex = 50
        Label29.Text = "Email Address"
        ' 
        ' Label28
        ' 
        Label28.ForeColor = Color.DarkBlue
        Label28.Location = New Point(48, 212)
        Label28.Margin = New Padding(5, 0, 5, 0)
        Label28.Name = "Label28"
        Label28.Size = New Size(138, 25)
        Label28.TabIndex = 49
        Label28.Text = "Social Security No"
        ' 
        ' txtPatSSN
        ' 
        txtPatSSN.Location = New Point(47, 242)
        txtPatSSN.Margin = New Padding(5, 6, 5, 6)
        txtPatSSN.Mask = "000-00-0000"
        txtPatSSN.Name = "txtPatSSN"
        txtPatSSN.Size = New Size(127, 31)
        txtPatSSN.TabIndex = 8
        ' 
        ' txtPatDOB
        ' 
        txtPatDOB.Location = New Point(838, 133)
        txtPatDOB.Margin = New Padding(5, 6, 5, 6)
        txtPatDOB.Name = "txtPatDOB"
        txtPatDOB.Size = New Size(157, 31)
        txtPatDOB.TabIndex = 7
        txtPatDOB.ValidatingType = GetType(Date)
        ' 
        ' Label25
        ' 
        Label25.ForeColor = Color.Fuchsia
        Label25.Location = New Point(833, 102)
        Label25.Margin = New Padding(5, 0, 5, 0)
        Label25.Name = "Label25"
        Label25.Size = New Size(95, 25)
        Label25.TabIndex = 44
        Label25.Text = "DOB"
        ' 
        ' Label26
        ' 
        Label26.ForeColor = Color.Fuchsia
        Label26.Location = New Point(188, 212)
        Label26.Margin = New Padding(5, 0, 5, 0)
        Label26.Name = "Label26"
        Label26.Size = New Size(130, 25)
        Label26.TabIndex = 43
        Label26.Text = "Address 1"
        ' 
        ' txtPatAdd1
        ' 
        txtPatAdd1.Location = New Point(187, 242)
        txtPatAdd1.Margin = New Padding(5, 6, 5, 6)
        txtPatAdd1.MaxLength = 35
        txtPatAdd1.Name = "txtPatAdd1"
        txtPatAdd1.Size = New Size(196, 31)
        txtPatAdd1.TabIndex = 9
        ' 
        ' Label27
        ' 
        Label27.ForeColor = Color.Fuchsia
        Label27.Location = New Point(252, 102)
        Label27.Margin = New Padding(5, 0, 5, 0)
        Label27.Name = "Label27"
        Label27.Size = New Size(122, 25)
        Label27.TabIndex = 41
        Label27.Text = "Last Name"
        ' 
        ' txtPatLName
        ' 
        txtPatLName.Location = New Point(258, 133)
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
        chkPatientBill.Location = New Point(207, 31)
        chkPatientBill.Margin = New Padding(5, 6, 5, 6)
        chkPatientBill.Name = "chkPatientBill"
        chkPatientBill.Size = New Size(112, 52)
        chkPatientBill.TabIndex = 41
        chkPatientBill.Text = "No"
        chkPatientBill.TextAlign = ContentAlignment.MiddleCenter
        chkPatientBill.UseVisualStyleBackColor = True
        ' 
        ' Label15
        ' 
        Label15.ForeColor = Color.DarkBlue
        Label15.Location = New Point(48, 44)
        Label15.Margin = New Padding(5, 0, 5, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(148, 25)
        Label15.TabIndex = 16
        Label15.Text = "Party to be billed"
        Label15.TextAlign = ContentAlignment.TopRight
        ' 
        ' btnPatLook
        ' 
        btnPatLook.Image = CType(resources.GetObject("btnPatLook.Image"), Image)
        btnPatLook.Location = New Point(192, 125)
        btnPatLook.Margin = New Padding(5, 6, 5, 6)
        btnPatLook.Name = "btnPatLook"
        btnPatLook.Size = New Size(50, 52)
        btnPatLook.TabIndex = 2
        btnPatLook.TabStop = False
        btnPatLook.UseVisualStyleBackColor = True
        ' 
        ' pctPatSex
        ' 
        pctPatSex.ErrorImage = CType(resources.GetObject("pctPatSex.ErrorImage"), Image)
        pctPatSex.InitialImage = CType(resources.GetObject("pctPatSex.InitialImage"), Image)
        pctPatSex.Location = New Point(787, 135)
        pctPatSex.Margin = New Padding(5, 6, 5, 6)
        pctPatSex.Name = "pctPatSex"
        pctPatSex.Size = New Size(32, 37)
        pctPatSex.TabIndex = 45
        pctPatSex.TabStop = False
        ' 
        ' tpCharges
        ' 
        tpCharges.Controls.Add(dgvCharges)
        tpCharges.Location = New Point(4, 34)
        tpCharges.Margin = New Padding(5, 6, 5, 6)
        tpCharges.Name = "tpCharges"
        tpCharges.Size = New Size(1032, 464)
        tpCharges.TabIndex = 4
        tpCharges.Text = "Charge Detail"
        tpCharges.UseVisualStyleBackColor = True
        ' 
        ' dgvCharges
        ' 
        dgvCharges.AllowUserToAddRows = False
        dgvCharges.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = Color.AliceBlue
        dgvCharges.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        dgvCharges.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvCharges.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvCharges.Columns.AddRange(New DataGridViewColumn() {DEL, TGPID, TGPName, CPT, Dx, Price})
        dgvCharges.Dock = DockStyle.Top
        dgvCharges.Location = New Point(0, 0)
        dgvCharges.Margin = New Padding(5, 6, 5, 6)
        dgvCharges.Name = "dgvCharges"
        dgvCharges.RowHeadersVisible = False
        dgvCharges.RowHeadersWidth = 51
        DataGridViewCellStyle3.BackColor = Color.Linen
        dgvCharges.RowsDefaultCellStyle = DataGridViewCellStyle3
        dgvCharges.Size = New Size(1032, 446)
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
        ' 
        ' TGPID
        ' 
        TGPID.FillWeight = 90F
        TGPID.HeaderText = "ID"
        TGPID.MaxInputLength = 5
        TGPID.MinimumWidth = 6
        TGPID.Name = "TGPID"
        TGPID.ReadOnly = True
        TGPID.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' TGPName
        ' 
        TGPName.FillWeight = 280F
        TGPName.HeaderText = "Name"
        TGPName.MaxInputLength = 35
        TGPName.MinimumWidth = 6
        TGPName.Name = "TGPName"
        TGPName.ReadOnly = True
        TGPName.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' CPT
        ' 
        CPT.FillWeight = 70F
        CPT.HeaderText = "CPT"
        CPT.MinimumWidth = 6
        CPT.Name = "CPT"
        CPT.ReadOnly = True
        CPT.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Dx
        ' 
        Dx.FillWeight = 60F
        Dx.HeaderText = "Dx"
        Dx.MaxInputLength = 16
        Dx.MinimumWidth = 6
        Dx.Name = "Dx"
        Dx.ReadOnly = True
        Dx.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Price
        ' 
        Price.FillWeight = 70F
        Price.HeaderText = "Price"
        Price.MaxInputLength = 8
        Price.MinimumWidth = 6
        Price.Name = "Price"
        Price.ReadOnly = True
        Price.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Label55
        ' 
        Label55.ForeColor = Color.DarkBlue
        Label55.Location = New Point(152, 988)
        Label55.Margin = New Padding(5, 0, 5, 0)
        Label55.Name = "Label55"
        Label55.Size = New Size(98, 25)
        Label55.TabIndex = 77
        Label55.Text = "Comments"
        Label55.TextAlign = ContentAlignment.TopRight
        ' 
        ' txtWorkCmnt
        ' 
        txtWorkCmnt.AcceptsReturn = True
        txtWorkCmnt.Location = New Point(268, 988)
        txtWorkCmnt.Margin = New Padding(5, 6, 5, 6)
        txtWorkCmnt.MaxLength = 960
        txtWorkCmnt.Multiline = True
        txtWorkCmnt.Name = "txtWorkCmnt"
        txtWorkCmnt.ScrollBars = ScrollBars.Vertical
        txtWorkCmnt.Size = New Size(1031, 171)
        txtWorkCmnt.TabIndex = 78
        txtWorkCmnt.TabStop = False
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(20, 20)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnSave, ToolStripSeparator1, btnReport, ToolStripSeparator3, btnCancel, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(1338, 34)
        ToolStrip1.TabIndex = 79
        ToolStrip1.Text = "ToolStrip1"
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
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
        ' 
        ' btnReport
        ' 
        btnReport.Enabled = False
        btnReport.Image = CType(resources.GetObject("btnReport.Image"), Image)
        btnReport.ImageTransparentColor = Color.Magenta
        btnReport.Name = "btnReport"
        btnReport.Size = New Size(154, 29)
        btnReport.Text = "Process Report"
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
        ' gbIndicator
        ' 
        gbIndicator.Controls.Add(lblCodes)
        gbIndicator.Controls.Add(lblPatient)
        gbIndicator.Controls.Add(lbl3P)
        gbIndicator.Controls.Add(lblClient)
        gbIndicator.Location = New Point(877, 317)
        gbIndicator.Margin = New Padding(5, 6, 5, 6)
        gbIndicator.Name = "gbIndicator"
        gbIndicator.Padding = New Padding(5, 6, 5, 6)
        gbIndicator.Size = New Size(253, 127)
        gbIndicator.TabIndex = 80
        gbIndicator.TabStop = False
        gbIndicator.Text = "Requisits"
        ' 
        ' lblCodes
        ' 
        lblCodes.BorderStyle = BorderStyle.FixedSingle
        lblCodes.ForeColor = Color.Black
        lblCodes.Location = New Point(118, 73)
        lblCodes.Margin = New Padding(5, 0, 5, 0)
        lblCodes.Name = "lblCodes"
        lblCodes.Size = New Size(119, 37)
        lblCodes.TabIndex = 81
        lblCodes.Text = "CODES"
        lblCodes.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblPatient
        ' 
        lblPatient.BorderStyle = BorderStyle.FixedSingle
        lblPatient.ForeColor = Color.Black
        lblPatient.Location = New Point(15, 73)
        lblPatient.Margin = New Padding(5, 0, 5, 0)
        lblPatient.Name = "lblPatient"
        lblPatient.Size = New Size(104, 37)
        lblPatient.TabIndex = 81
        lblPatient.Text = "PATIENT"
        lblPatient.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lbl3P
        ' 
        lbl3P.BorderStyle = BorderStyle.FixedSingle
        lbl3P.ForeColor = Color.Black
        lbl3P.Location = New Point(118, 37)
        lbl3P.Margin = New Padding(5, 0, 5, 0)
        lbl3P.Name = "lbl3P"
        lbl3P.Size = New Size(119, 37)
        lbl3P.TabIndex = 21
        lbl3P.Text = "INSURANCE"
        lbl3P.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblClient
        ' 
        lblClient.BorderStyle = BorderStyle.FixedSingle
        lblClient.ForeColor = Color.Black
        lblClient.Location = New Point(15, 37)
        lblClient.Margin = New Padding(5, 0, 5, 0)
        lblClient.Name = "lblClient"
        lblClient.Size = New Size(104, 37)
        lblClient.TabIndex = 20
        lblClient.Text = "CLIENT"
        lblClient.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' btnEECC
        ' 
        btnEECC.Location = New Point(1140, 404)
        btnEECC.Margin = New Padding(5, 6, 5, 6)
        btnEECC.Name = "btnEECC"
        btnEECC.Size = New Size(168, 52)
        btnEECC.TabIndex = 81
        btnEECC.Text = "Check Eligibility"
        btnEECC.UseVisualStyleBackColor = True
        ' 
        ' lblEligibility
        ' 
        lblEligibility.Location = New Point(1140, 342)
        lblEligibility.Margin = New Padding(5, 6, 5, 6)
        lblEligibility.Name = "lblEligibility"
        lblEligibility.Size = New Size(160, 54)
        lblEligibility.TabIndex = 84
        lblEligibility.Text = "Eligibility"
        lblEligibility.UseVisualStyleBackColor = True
        ' 
        ' DataGridViewImageColumn1
        ' 
        DataGridViewImageColumn1.FillWeight = 30F
        DataGridViewImageColumn1.HeaderText = ""
        DataGridViewImageColumn1.Image = CType(resources.GetObject("DataGridViewImageColumn1.Image"), Image)
        DataGridViewImageColumn1.MinimumWidth = 6
        DataGridViewImageColumn1.Name = "DataGridViewImageColumn1"
        DataGridViewImageColumn1.Resizable = DataGridViewTriState.True
        DataGridViewImageColumn1.Width = 30
        ' 
        ' DataGridViewImageColumn2
        ' 
        DataGridViewImageColumn2.FillWeight = 25F
        DataGridViewImageColumn2.HeaderText = ""
        DataGridViewImageColumn2.Image = CType(resources.GetObject("DataGridViewImageColumn2.Image"), Image)
        DataGridViewImageColumn2.MinimumWidth = 6
        DataGridViewImageColumn2.Name = "DataGridViewImageColumn2"
        DataGridViewImageColumn2.ReadOnly = True
        DataGridViewImageColumn2.Width = 25
        ' 
        ' btnDxSync
        ' 
        btnDxSync.Image = CType(resources.GetObject("btnDxSync.Image"), Image)
        btnDxSync.Location = New Point(193, 454)
        btnDxSync.Margin = New Padding(5, 6, 5, 6)
        btnDxSync.Name = "btnDxSync"
        btnDxSync.Size = New Size(50, 52)
        btnDxSync.TabIndex = 60
        btnDxSync.UseVisualStyleBackColor = True
        ' 
        ' loading
        ' 
        loading.Image = My.Resources.Resources.icons8_waiting_24
        loading.Location = New Point(1195, 402)
        loading.Margin = New Padding(5, 0, 5, 0)
        loading.Name = "loading"
        loading.Size = New Size(45, 63)
        loading.TabIndex = 83
        loading.Visible = False
        ' 
        ' frmScrubber
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1338, 1208)
        Controls.Add(lblEligibility)
        Controls.Add(btnEECC)
        Controls.Add(gbIndicator)
        Controls.Add(ToolStrip1)
        Controls.Add(txtWorkCmnt)
        Controls.Add(Label55)
        Controls.Add(btnDxSync)
        Controls.Add(TabControl1)
        Controls.Add(Label10)
        Controls.Add(dgvDXs)
        Controls.Add(GroupBox2)
        Controls.Add(GroupBox1)
        Controls.Add(loading)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmScrubber"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Third Party Billing Scrubber"
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        CType(dgvDXs, ComponentModel.ISupportInitialize).EndInit()
        TabControl1.ResumeLayout(False)
        tpClient.ResumeLayout(False)
        tpClient.PerformLayout()
        tpThirdParty.ResumeLayout(False)
        gbTP.ResumeLayout(False)
        gbTP.PerformLayout()
        CType(pctInsSex, ComponentModel.ISupportInitialize).EndInit()
        tpPatient.ResumeLayout(False)
        tpPatient.PerformLayout()
        CType(pctPatSex, ComponentModel.ISupportInitialize).EndInit()
        tpCharges.ResumeLayout(False)
        CType(dgvCharges, ComponentModel.ISupportInitialize).EndInit()
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        gbIndicator.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnTarget As System.Windows.Forms.Button
    Friend WithEvents btnSel As System.Windows.Forms.Button
    Friend WithEvents btnDesel As System.Windows.Forms.Button
    Friend WithEvents lstTargets As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents cmbABU As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtAccTo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents txtAccFrom As System.Windows.Forms.TextBox
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnFirst As System.Windows.Forms.Button
    Friend WithEvents btnPrevious As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnLast As System.Windows.Forms.Button
    Friend WithEvents txtNavStatus As System.Windows.Forms.TextBox
    Friend WithEvents txtClientID As System.Windows.Forms.TextBox
    Friend WithEvents chkClientAllSpec As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtAccDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtAccID As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents dgvDXs As System.Windows.Forms.DataGridView
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tpClient As System.Windows.Forms.TabPage
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents txtNPI As System.Windows.Forms.TextBox
    Friend WithEvents txtProvFax As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtProvPhone As System.Windows.Forms.MaskedTextBox
    Friend WithEvents lstAttending As System.Windows.Forms.CheckedListBox
    Friend WithEvents chkProvContract As System.Windows.Forms.CheckBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents cmbProvPrice As System.Windows.Forms.ComboBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents txtProviderAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtProviderName As System.Windows.Forms.TextBox
    Friend WithEvents btnProviderLook As System.Windows.Forms.Button
    Friend WithEvents chkClientBill As System.Windows.Forms.CheckBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtProviderID As System.Windows.Forms.TextBox
    Friend WithEvents tpThirdParty As System.Windows.Forms.TabPage
    Friend WithEvents gbTP As System.Windows.Forms.GroupBox
    Friend WithEvents chkTPContract As System.Windows.Forms.CheckBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents cmbTPPrice As System.Windows.Forms.ComboBox
    Friend WithEvents txtInsDOB As System.Windows.Forms.MaskedTextBox
    Friend WithEvents pctInsSex As System.Windows.Forms.PictureBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtInsSex As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtInsAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtInsName As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents btnInsLookup As System.Windows.Forms.Button
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtInsuredID As System.Windows.Forms.TextBox
    Friend WithEvents cmbRelation As System.Windows.Forms.ComboBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtCopay As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtGroup As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtPolicy As System.Windows.Forms.TextBox
    Friend WithEvents btnPayerLookUp As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents chkTPBill As System.Windows.Forms.CheckBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents tpPatient As System.Windows.Forms.TabPage
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtPatState As System.Windows.Forms.TextBox
    Friend WithEvents txtPatHPhone As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtPatCell As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtPatZip As System.Windows.Forms.TextBox
    Friend WithEvents txtPatCity As System.Windows.Forms.TextBox
    Friend WithEvents txtPatAdd2 As System.Windows.Forms.TextBox
    Friend WithEvents txtPatMName As System.Windows.Forms.TextBox
    Friend WithEvents txtPatFName As System.Windows.Forms.TextBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents cmbPatPrice As System.Windows.Forms.ComboBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtPatientID As System.Windows.Forms.TextBox
    Friend WithEvents txtPatSex As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtPatEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtPatSSN As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnPatLook As System.Windows.Forms.Button
    Friend WithEvents txtPatDOB As System.Windows.Forms.MaskedTextBox
    Friend WithEvents pctPatSex As System.Windows.Forms.PictureBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtPatAdd1 As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtPatLName As System.Windows.Forms.TextBox
    Friend WithEvents chkPatientBill As System.Windows.Forms.CheckBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents tpCharges As System.Windows.Forms.TabPage
    Friend WithEvents dgvCharges As System.Windows.Forms.DataGridView
    Friend WithEvents btnDxSync As System.Windows.Forms.Button
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents txtWorkCmnt As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtMissingRecs As System.Windows.Forms.TextBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnReport As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtPayerName As System.Windows.Forms.TextBox
    Friend WithEvents txtPayerID As System.Windows.Forms.TextBox
    Friend WithEvents gbIndicator As System.Windows.Forms.GroupBox
    Friend WithEvents lblClient As System.Windows.Forms.Label
    Friend WithEvents lbl3P As System.Windows.Forms.Label
    Friend WithEvents lblCodes As System.Windows.Forms.Label
    Friend WithEvents lblPatient As System.Windows.Forms.Label
    Friend WithEvents DEL As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents TGPID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TGPName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CPT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Dx As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Price As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ICD9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LookUp As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents btnEECC As System.Windows.Forms.Button
    Friend WithEvents DataGridViewImageColumn1 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents DataGridViewImageColumn2 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents loading As Label
    Friend WithEvents lblEligibility As Button
    Friend WithEvents dtpDateFrom As DateTimePicker
    Friend WithEvents dtpDateTo As DateTimePicker
    Friend WithEvents lblClearDates As Label
End Class
