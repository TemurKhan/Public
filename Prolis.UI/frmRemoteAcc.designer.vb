<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRemoteAcc
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRemoteAcc))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        chkReqOrd = New ToolStripButton()
        btnDelete = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnReport = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        Label3 = New Label()
        btnReceive = New Button()
        GroupBox2 = New GroupBox()
        dtpAccDate = New DateTimePicker()
        rl = New Label()
        Label13 = New Label()
        Panel1 = New Panel()
        Button2 = New Button()
        Button1 = New Button()
        Label11 = New Label()
        nxtbtn = New Button()
        pagtxt = New TextBox()
        prevbtn = New Button()
        Label9 = New Label()
        txtAccPageCount = New TextBox()
        Label7 = New Label()
        txtAccCount = New TextBox()
        Label10 = New Label()
        txtAccCountOnWindow = New TextBox()
        searchPanel = New Panel()
        lblTerm = New Label()
        Label19 = New Label()
        txtTerm = New TextBox()
        patientpanel = New Panel()
        Label34 = New Label()
        btnPatLook = New Button()
        txtPatientID = New TextBox()
        TextBox1 = New TextBox()
        ReqChk = New CheckBox()
        LabelChk = New CheckBox()
        SelectChk = New CheckBox()
        RecAll = New Button()
        Label8 = New Label()
        Label6 = New Label()
        cmbEntry = New ComboBox()
        dgvAccessions = New DataGridView()
        Accession = New DataGridViewTextBoxColumn()
        ReqID = New DataGridViewTextBoxColumn()
        AccDate = New DataGridViewTextBoxColumn()
        Patient = New DataGridViewTextBoxColumn()
        MRN = New DataGridViewTextBoxColumn()
        Provider = New DataGridViewTextBoxColumn()
        Req = New DataGridViewCheckBoxColumn()
        Label = New DataGridViewCheckBoxColumn()
        Selected = New DataGridViewCheckBoxColumn()
        Label1 = New Label()
        cmbTerm = New ComboBox()
        Label5 = New Label()
        Label4 = New Label()
        Label2 = New Label()
        btnBrowse = New Button()
        cmbProvider = New ComboBox()
        cmbRoute = New ComboBox()
        GroupBox3 = New GroupBox()
        Label12 = New Label()
        ChkRemotePrint = New CheckBox()
        countLaberls = New TextBox()
        cmbLabelReq = New ComboBox()
        cmbDestination = New ComboBox()
        btnSel = New Button()
        txtComment = New TextBox()
        btnPrint = New Button()
        btnDesel = New Button()
        dgvSources = New DataGridView()
        SrcID = New DataGridViewTextBoxColumn()
        SrcNo = New DataGridViewTextBoxColumn()
        SrcName = New DataGridViewTextBoxColumn()
        SrcQty = New DataGridViewTextBoxColumn()
        SrcDate = New DataGridViewTextBoxColumn()
        SrcTime = New DataGridViewTextBoxColumn()
        SrcTemp = New DataGridViewComboBoxColumn()
        chkRec = New DataGridViewCheckBoxColumn()
        ToolTip1 = New ToolTip(components)
        ToolStrip1.SuspendLayout()
        GroupBox2.SuspendLayout()
        Panel1.SuspendLayout()
        searchPanel.SuspendLayout()
        patientpanel.SuspendLayout()
        CType(dgvAccessions, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox3.SuspendLayout()
        CType(dgvSources, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(20, 20)
        ToolStrip1.Items.AddRange(New ToolStripItem() {chkReqOrd, btnDelete, ToolStripSeparator3, btnReport, ToolStripSeparator1, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(1438, 34)
        ToolStrip1.TabIndex = 1
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' chkReqOrd
        ' 
        chkReqOrd.CheckOnClick = True
        chkReqOrd.Image = CType(resources.GetObject("chkReqOrd.Image"), Image)
        chkReqOrd.ImageTransparentColor = Color.Magenta
        chkReqOrd.Name = "chkReqOrd"
        chkReqOrd.Size = New Size(114, 29)
        chkReqOrd.Text = "Accession"
        ' 
        ' btnDelete
        ' 
        btnDelete.Enabled = False
        btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), Image)
        btnDelete.ImageTransparentColor = Color.Magenta
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(86, 29)
        btnDelete.Text = "Delete"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(6, 34)
        ' 
        ' btnReport
        ' 
        btnReport.Enabled = False
        btnReport.Image = CType(resources.GetObject("btnReport.Image"), Image)
        btnReport.ImageTransparentColor = Color.Magenta
        btnReport.Name = "btnReport"
        btnReport.Size = New Size(89, 29)
        btnReport.Text = "Report"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
        ' 
        ' btnCancel
        ' 
        btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), Image)
        btnCancel.ImageTransparentColor = Color.Magenta
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(87, 29)
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
        btnHelp.Size = New Size(73, 29)
        btnHelp.Text = "Help"
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.Blue
        Label3.Location = New Point(19, 394)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(819, 73)
        Label3.TabIndex = 3
        Label3.Text = "Either scan the specimen's barcode label or enter the accession number manually in the accession number field followed by the <ENTER> key or click the 'Receive Button."
        Label3.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' btnReceive
        ' 
        btnReceive.Location = New Point(1240, 402)
        btnReceive.Margin = New Padding(5, 6, 5, 6)
        btnReceive.Name = "btnReceive"
        btnReceive.Size = New Size(141, 56)
        btnReceive.TabIndex = 2
        btnReceive.Text = "Ok"
        btnReceive.UseVisualStyleBackColor = True
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(dtpAccDate)
        GroupBox2.Controls.Add(rl)
        GroupBox2.Controls.Add(Label13)
        GroupBox2.Controls.Add(Panel1)
        GroupBox2.Controls.Add(searchPanel)
        GroupBox2.Controls.Add(patientpanel)
        GroupBox2.Controls.Add(TextBox1)
        GroupBox2.Controls.Add(ReqChk)
        GroupBox2.Controls.Add(LabelChk)
        GroupBox2.Controls.Add(SelectChk)
        GroupBox2.Controls.Add(RecAll)
        GroupBox2.Controls.Add(Label8)
        GroupBox2.Controls.Add(Label6)
        GroupBox2.Controls.Add(cmbEntry)
        GroupBox2.Controls.Add(dgvAccessions)
        GroupBox2.Controls.Add(Label1)
        GroupBox2.Controls.Add(cmbTerm)
        GroupBox2.Controls.Add(Label5)
        GroupBox2.Controls.Add(Label4)
        GroupBox2.Controls.Add(Label2)
        GroupBox2.Controls.Add(btnBrowse)
        GroupBox2.Controls.Add(cmbProvider)
        GroupBox2.Controls.Add(cmbRoute)
        GroupBox2.Location = New Point(19, 58)
        GroupBox2.Margin = New Padding(5, 6, 5, 6)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Padding = New Padding(5, 6, 5, 6)
        GroupBox2.Size = New Size(1401, 727)
        GroupBox2.TabIndex = 3
        GroupBox2.TabStop = False
        GroupBox2.Text = "Browse Remote Specimens"
        ' 
        ' dtpAccDate
        ' 
        dtpAccDate.Format = DateTimePickerFormat.Short
        dtpAccDate.Location = New Point(29, 120)
        dtpAccDate.Margin = New Padding(4, 5, 4, 5)
        dtpAccDate.Name = "dtpAccDate"
        dtpAccDate.Size = New Size(135, 31)
        dtpAccDate.TabIndex = 96
        ' 
        ' rl
        ' 
        rl.AccessibleName = ""
        rl.AutoSize = True
        rl.ForeColor = SystemColors.InfoText
        rl.Location = New Point(42, 677)
        rl.Margin = New Padding(4, 0, 4, 0)
        rl.Name = "rl"
        rl.Size = New Size(155, 25)
        rl.TabIndex = 95
        rl.Text = "Specimen missing"
        rl.Visible = False
        ' 
        ' Label13
        ' 
        Label13.AccessibleName = ""
        Label13.AutoSize = True
        Label13.ForeColor = Color.Red
        Label13.Location = New Point(21, 677)
        Label13.Margin = New Padding(4, 0, 4, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(30, 25)
        Label13.TabIndex = 94
        Label13.Text = "▬"
        Label13.Visible = False
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(Button2)
        Panel1.Controls.Add(Button1)
        Panel1.Controls.Add(Label11)
        Panel1.Controls.Add(nxtbtn)
        Panel1.Controls.Add(pagtxt)
        Panel1.Controls.Add(prevbtn)
        Panel1.Controls.Add(Label9)
        Panel1.Controls.Add(txtAccPageCount)
        Panel1.Controls.Add(Label7)
        Panel1.Controls.Add(txtAccCount)
        Panel1.Controls.Add(Label10)
        Panel1.Controls.Add(txtAccCountOnWindow)
        Panel1.Location = New Point(81, 444)
        Panel1.Margin = New Padding(4, 5, 4, 5)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1252, 95)
        Panel1.TabIndex = 49
        Panel1.Visible = False
        ' 
        ' Button2
        ' 
        Button2.Anchor = AnchorStyles.None
        Button2.Font = New Font("Microsoft Sans Serif", 14.0F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Button2.Location = New Point(714, 30)
        Button2.Margin = New Padding(5, 6, 5, 6)
        Button2.Name = "Button2"
        Button2.Size = New Size(94, 56)
        Button2.TabIndex = 57
        Button2.Text = "«"
        ToolTip1.SetToolTip(Button2, "Go to first page")
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button1
        ' 
        Button1.Anchor = AnchorStyles.None
        Button1.Font = New Font("Microsoft Sans Serif", 14.0F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Button1.Location = New Point(1106, 30)
        Button1.Margin = New Padding(5, 6, 5, 6)
        Button1.Name = "Button1"
        Button1.Size = New Size(85, 59)
        Button1.TabIndex = 56
        Button1.Text = "»"
        ToolTip1.SetToolTip(Button1, "Go to last page")
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Label11
        ' 
        Label11.ForeColor = Color.DarkBlue
        Label11.Location = New Point(921, 9)
        Label11.Margin = New Padding(5, 0, 5, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(79, 33)
        Label11.TabIndex = 55
        Label11.Text = "Page#"
        Label11.TextAlign = ContentAlignment.TopRight
        ' 
        ' nxtbtn
        ' 
        nxtbtn.Location = New Point(1018, 30)
        nxtbtn.Margin = New Padding(5, 6, 5, 6)
        nxtbtn.Name = "nxtbtn"
        nxtbtn.Size = New Size(85, 59)
        nxtbtn.TabIndex = 48
        nxtbtn.Text = "Nxt"
        nxtbtn.UseVisualStyleBackColor = True
        ' 
        ' pagtxt
        ' 
        pagtxt.Location = New Point(921, 48)
        pagtxt.Margin = New Padding(5, 6, 5, 6)
        pagtxt.Name = "pagtxt"
        pagtxt.Size = New Size(74, 31)
        pagtxt.TabIndex = 46
        pagtxt.Text = "1"
        pagtxt.TextAlign = HorizontalAlignment.Center
        ' 
        ' prevbtn
        ' 
        prevbtn.Location = New Point(818, 30)
        prevbtn.Margin = New Padding(5, 6, 5, 6)
        prevbtn.Name = "prevbtn"
        prevbtn.Size = New Size(94, 58)
        prevbtn.TabIndex = 47
        prevbtn.Text = "Prev"
        prevbtn.UseVisualStyleBackColor = True
        ' 
        ' Label9
        ' 
        Label9.ForeColor = Color.DarkBlue
        Label9.Location = New Point(461, 14)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(210, 33)
        Label9.TabIndex = 54
        Label9.Text = "Toral Accession Pages:"
        Label9.TextAlign = ContentAlignment.TopRight
        ' 
        ' txtAccPageCount
        ' 
        txtAccPageCount.Location = New Point(512, 52)
        txtAccPageCount.Margin = New Padding(5, 6, 5, 6)
        txtAccPageCount.Name = "txtAccPageCount"
        txtAccPageCount.ReadOnly = True
        txtAccPageCount.Size = New Size(94, 31)
        txtAccPageCount.TabIndex = 53
        txtAccPageCount.Text = "0"
        txtAccPageCount.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.DarkBlue
        Label7.Location = New Point(302, 16)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(160, 27)
        Label7.TabIndex = 52
        Label7.Text = "Total Accessions Count:"
        Label7.TextAlign = ContentAlignment.TopRight
        ' 
        ' txtAccCount
        ' 
        txtAccCount.Location = New Point(345, 52)
        txtAccCount.Margin = New Padding(5, 6, 5, 6)
        txtAccCount.Name = "txtAccCount"
        txtAccCount.ReadOnly = True
        txtAccCount.Size = New Size(94, 31)
        txtAccCount.TabIndex = 51
        txtAccCount.Text = "0"
        txtAccCount.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label10
        ' 
        Label10.ForeColor = Color.DarkBlue
        Label10.Location = New Point(56, 16)
        Label10.Margin = New Padding(5, 0, 5, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(236, 27)
        Label10.TabIndex = 50
        Label10.Text = "Accession Count Per Page"
        Label10.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' txtAccCountOnWindow
        ' 
        txtAccCountOnWindow.Location = New Point(85, 53)
        txtAccCountOnWindow.Margin = New Padding(5, 6, 5, 6)
        txtAccCountOnWindow.Name = "txtAccCountOnWindow"
        txtAccCountOnWindow.Size = New Size(94, 31)
        txtAccCountOnWindow.TabIndex = 49
        txtAccCountOnWindow.Text = "0"
        txtAccCountOnWindow.TextAlign = HorizontalAlignment.Center
        ' 
        ' searchPanel
        ' 
        searchPanel.Controls.Add(lblTerm)
        searchPanel.Controls.Add(Label19)
        searchPanel.Controls.Add(txtTerm)
        searchPanel.Location = New Point(965, 59)
        searchPanel.Margin = New Padding(4, 5, 4, 5)
        searchPanel.Name = "searchPanel"
        searchPanel.Size = New Size(231, 111)
        searchPanel.TabIndex = 92
        ' 
        ' lblTerm
        ' 
        lblTerm.ForeColor = Color.DarkBlue
        lblTerm.Location = New Point(5, 33)
        lblTerm.Margin = New Padding(5, 0, 5, 0)
        lblTerm.Name = "lblTerm"
        lblTerm.Size = New Size(136, 27)
        lblTerm.TabIndex = 90
        lblTerm.Text = "Accession"
        ' 
        ' Label19
        ' 
        Label19.BackColor = SystemColors.Control
        Label19.Image = My.Resources.Resources.paste
        Label19.Location = New Point(135, 14)
        Label19.Margin = New Padding(4, 0, 4, 0)
        Label19.Name = "Label19"
        Label19.Size = New Size(41, 48)
        Label19.TabIndex = 91
        Label19.Text = "      "
        ' 
        ' txtTerm
        ' 
        txtTerm.Location = New Point(5, 61)
        txtTerm.Margin = New Padding(5, 6, 5, 6)
        txtTerm.Name = "txtTerm"
        txtTerm.Size = New Size(216, 31)
        txtTerm.TabIndex = 89
        ' 
        ' patientpanel
        ' 
        patientpanel.Controls.Add(Label34)
        patientpanel.Controls.Add(btnPatLook)
        patientpanel.Controls.Add(txtPatientID)
        patientpanel.Location = New Point(965, 42)
        patientpanel.Margin = New Padding(4, 5, 4, 5)
        patientpanel.Name = "patientpanel"
        patientpanel.Size = New Size(228, 133)
        patientpanel.TabIndex = 93
        ' 
        ' Label34
        ' 
        Label34.ForeColor = Color.DarkBlue
        Label34.Location = New Point(10, 36)
        Label34.Margin = New Padding(5, 0, 5, 0)
        Label34.Name = "Label34"
        Label34.Size = New Size(131, 27)
        Label34.TabIndex = 91
        Label34.Text = "Patient ID"
        ' 
        ' btnPatLook
        ' 
        btnPatLook.Image = CType(resources.GetObject("btnPatLook.Image"), Image)
        btnPatLook.Location = New Point(155, 67)
        btnPatLook.Margin = New Padding(5, 6, 5, 6)
        btnPatLook.Name = "btnPatLook"
        btnPatLook.Size = New Size(50, 53)
        btnPatLook.TabIndex = 90
        btnPatLook.TabStop = False
        btnPatLook.UseVisualStyleBackColor = True
        ' 
        ' txtPatientID
        ' 
        txtPatientID.BackColor = Color.Ivory
        txtPatientID.Location = New Point(9, 77)
        txtPatientID.Margin = New Padding(5, 6, 5, 6)
        txtPatientID.MaxLength = 12
        txtPatientID.Name = "txtPatientID"
        txtPatientID.Size = New Size(135, 31)
        txtPatientID.TabIndex = 89
        txtPatientID.TextAlign = HorizontalAlignment.Center
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(29, 169)
        TextBox1.Margin = New Padding(5, 6, 5, 6)
        TextBox1.Name = "TextBox1"
        TextBox1.ReadOnly = True
        TextBox1.Size = New Size(746, 31)
        TextBox1.TabIndex = 46
        TextBox1.Text = "Tip: Double click on an acession id to copy to clipboard"
        TextBox1.TextAlign = HorizontalAlignment.Center
        ' 
        ' ReqChk
        ' 
        ReqChk.AutoSize = True
        ReqChk.Location = New Point(1150, 228)
        ReqChk.Margin = New Padding(5, 6, 5, 6)
        ReqChk.Name = "ReqChk"
        ReqChk.Size = New Size(22, 21)
        ReqChk.TabIndex = 42
        ReqChk.UseVisualStyleBackColor = True
        ' 
        ' LabelChk
        ' 
        LabelChk.AutoSize = True
        LabelChk.Location = New Point(1241, 227)
        LabelChk.Margin = New Padding(5, 6, 5, 6)
        LabelChk.Name = "LabelChk"
        LabelChk.Size = New Size(22, 21)
        LabelChk.TabIndex = 41
        LabelChk.UseVisualStyleBackColor = True
        ' 
        ' SelectChk
        ' 
        SelectChk.AutoSize = True
        SelectChk.Location = New Point(1331, 227)
        SelectChk.Margin = New Padding(5, 6, 5, 6)
        SelectChk.Name = "SelectChk"
        SelectChk.Size = New Size(22, 21)
        SelectChk.TabIndex = 40
        SelectChk.UseVisualStyleBackColor = True
        ' 
        ' RecAll
        ' 
        RecAll.Location = New Point(1268, 677)
        RecAll.Margin = New Padding(5, 6, 5, 6)
        RecAll.Name = "RecAll"
        RecAll.Size = New Size(129, 38)
        RecAll.TabIndex = 32
        RecAll.Text = "Pull Selected"
        RecAll.UseVisualStyleBackColor = True
        ' 
        ' Label8
        ' 
        Label8.BackColor = SystemColors.GradientActiveCaption
        Label8.BorderStyle = BorderStyle.FixedSingle
        Label8.ForeColor = Color.DarkBlue
        Label8.Location = New Point(1268, 112)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(124, 41)
        Label8.TabIndex = 27
        Label8.Text = "Uncheck All"
        Label8.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.DarkBlue
        Label6.Location = New Point(295, 30)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(120, 27)
        Label6.TabIndex = 25
        Label6.Text = "Entry Point:"
        Label6.TextAlign = ContentAlignment.TopRight
        ' 
        ' cmbEntry
        ' 
        cmbEntry.DropDownStyle = ComboBoxStyle.DropDownList
        cmbEntry.FormattingEnabled = True
        cmbEntry.Items.AddRange(New Object() {"ALL", "EMR Random", "In Lab Infinite", "In Lab Timed", "In Lab Un-Received", "Outreach Infinite", "Outreach Random", "Outreach Timed"})
        cmbEntry.Location = New Point(425, 25)
        cmbEntry.Margin = New Padding(5, 6, 5, 6)
        cmbEntry.Name = "cmbEntry"
        cmbEntry.Size = New Size(342, 33)
        cmbEntry.TabIndex = 24
        ' 
        ' dgvAccessions
        ' 
        dgvAccessions.AllowUserToAddRows = False
        dgvAccessions.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(255), CByte(192), CByte(255))
        dgvAccessions.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvAccessions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvAccessions.Columns.AddRange(New DataGridViewColumn() {Accession, ReqID, AccDate, Patient, MRN, Provider, Req, Label, Selected})
        dgvAccessions.Location = New Point(21, 216)
        dgvAccessions.Margin = New Padding(5, 6, 5, 6)
        dgvAccessions.Name = "dgvAccessions"
        dgvAccessions.RowHeadersVisible = False
        dgvAccessions.RowHeadersWidth = 56
        dgvAccessions.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvAccessions.Size = New Size(1370, 448)
        dgvAccessions.TabIndex = 2
        ' 
        ' Accession
        ' 
        Accession.FillWeight = 120.0F
        Accession.HeaderText = "Accession"
        Accession.MinimumWidth = 7
        Accession.Name = "Accession"
        Accession.ReadOnly = True
        Accession.Width = 208
        ' 
        ' ReqID
        ' 
        ReqID.FillWeight = 80.0F
        ReqID.HeaderText = "Req ID"
        ReqID.MaxInputLength = 16
        ReqID.MinimumWidth = 7
        ReqID.Name = "ReqID"
        ReqID.ReadOnly = True
        ReqID.SortMode = DataGridViewColumnSortMode.NotSortable
        ReqID.Width = 138
        ' 
        ' AccDate
        ' 
        AccDate.FillWeight = 80.0F
        AccDate.HeaderText = "Acc Date"
        AccDate.MinimumWidth = 7
        AccDate.Name = "AccDate"
        AccDate.ReadOnly = True
        AccDate.Width = 139
        ' 
        ' Patient
        ' 
        Patient.FillWeight = 130.0F
        Patient.HeaderText = "Patient"
        Patient.MinimumWidth = 7
        Patient.Name = "Patient"
        Patient.ReadOnly = True
        Patient.Width = 225
        ' 
        ' MRN
        ' 
        MRN.FillWeight = 70.0F
        MRN.HeaderText = "MRN"
        MRN.MinimumWidth = 7
        MRN.Name = "MRN"
        MRN.ReadOnly = True
        MRN.Width = 121
        ' 
        ' Provider
        ' 
        Provider.FillWeight = 175.0F
        Provider.HeaderText = "Provider"
        Provider.MinimumWidth = 7
        Provider.Name = "Provider"
        Provider.ReadOnly = True
        Provider.Width = 303
        ' 
        ' Req
        ' 
        Req.FillWeight = 40.0F
        Req.HeaderText = "Req"
        Req.MinimumWidth = 7
        Req.Name = "Req"
        Req.Width = 70
        ' 
        ' Label
        ' 
        Label.FillWeight = 40.0F
        Label.HeaderText = "Label"
        Label.MinimumWidth = 7
        Label.Name = "Label"
        Label.Width = 69
        ' 
        ' Selected
        ' 
        Selected.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        Selected.FlatStyle = FlatStyle.Popup
        Selected.HeaderText = "Select"
        Selected.MinimumWidth = 2
        Selected.Name = "Selected"
        Selected.SortMode = DataGridViewColumnSortMode.Automatic
        Selected.Width = 94
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(756, 84)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(204, 27)
        Label1.TabIndex = 12
        Label1.Text = "ACC/REQ/MRN/Patient"
        ' 
        ' cmbTerm
        ' 
        cmbTerm.DropDownStyle = ComboBoxStyle.DropDownList
        cmbTerm.FormattingEnabled = True
        cmbTerm.Items.AddRange(New Object() {"Accession", "MRN", "Requisition", "Patient"})
        cmbTerm.Location = New Point(746, 119)
        cmbTerm.Margin = New Padding(5, 6, 5, 6)
        cmbTerm.Name = "cmbTerm"
        cmbTerm.Size = New Size(209, 33)
        cmbTerm.TabIndex = 10
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.DarkBlue
        Label5.Location = New Point(409, 84)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(129, 27)
        Label5.TabIndex = 9
        Label5.Text = "Client"
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(176, 84)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(315, 27)
        Label4.TabIndex = 8
        Label4.Text = "Courier Route"
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(24, 84)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(144, 27)
        Label2.TabIndex = 7
        Label2.Text = "Accession Date"
        ' 
        ' btnBrowse
        ' 
        btnBrowse.Image = CType(resources.GetObject("btnBrowse.Image"), Image)
        btnBrowse.Location = New Point(1192, 112)
        btnBrowse.Margin = New Padding(5, 6, 5, 6)
        btnBrowse.Name = "btnBrowse"
        btnBrowse.Size = New Size(60, 48)
        btnBrowse.TabIndex = 6
        btnBrowse.UseVisualStyleBackColor = True
        ' 
        ' cmbProvider
        ' 
        cmbProvider.FormattingEnabled = True
        cmbProvider.Location = New Point(394, 119)
        cmbProvider.Margin = New Padding(5, 6, 5, 6)
        cmbProvider.Name = "cmbProvider"
        cmbProvider.Size = New Size(342, 33)
        cmbProvider.TabIndex = 5
        ' 
        ' cmbRoute
        ' 
        cmbRoute.FormattingEnabled = True
        cmbRoute.Location = New Point(176, 119)
        cmbRoute.Margin = New Padding(5, 6, 5, 6)
        cmbRoute.Name = "cmbRoute"
        cmbRoute.Size = New Size(204, 33)
        cmbRoute.TabIndex = 4
        ' 
        ' GroupBox3
        ' 
        GroupBox3.Controls.Add(Label12)
        GroupBox3.Controls.Add(ChkRemotePrint)
        GroupBox3.Controls.Add(countLaberls)
        GroupBox3.Controls.Add(cmbLabelReq)
        GroupBox3.Controls.Add(cmbDestination)
        GroupBox3.Controls.Add(Label3)
        GroupBox3.Controls.Add(btnSel)
        GroupBox3.Controls.Add(txtComment)
        GroupBox3.Controls.Add(btnPrint)
        GroupBox3.Controls.Add(btnReceive)
        GroupBox3.Controls.Add(btnDesel)
        GroupBox3.Controls.Add(dgvSources)
        GroupBox3.Location = New Point(20, 797)
        GroupBox3.Margin = New Padding(5, 6, 5, 6)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Padding = New Padding(5, 6, 5, 6)
        GroupBox3.Size = New Size(1380, 469)
        GroupBox3.TabIndex = 4
        GroupBox3.TabStop = False
        GroupBox3.Text = "Required Specimen  Content(s)"
        ' 
        ' Label12
        ' 
        Label12.ForeColor = Color.DarkBlue
        Label12.Location = New Point(19, 289)
        Label12.Margin = New Padding(5, 0, 5, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(160, 27)
        Label12.TabIndex = 48
        Label12.Text = "Comments"
        Label12.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' ChkRemotePrint
        ' 
        ChkRemotePrint.AutoSize = True
        ChkRemotePrint.Location = New Point(411, 22)
        ChkRemotePrint.Margin = New Padding(4, 5, 4, 5)
        ChkRemotePrint.Name = "ChkRemotePrint"
        ChkRemotePrint.Size = New Size(188, 29)
        ChkRemotePrint.TabIndex = 21
        ChkRemotePrint.Text = "Prolis Remote Print"
        ChkRemotePrint.UseVisualStyleBackColor = True
        ' 
        ' countLaberls
        ' 
        countLaberls.Location = New Point(918, 23)
        countLaberls.Margin = New Padding(5, 6, 5, 6)
        countLaberls.Name = "countLaberls"
        countLaberls.Size = New Size(74, 31)
        countLaberls.TabIndex = 47
        countLaberls.Text = "1"
        countLaberls.TextAlign = HorizontalAlignment.Center
        ' 
        ' cmbLabelReq
        ' 
        cmbLabelReq.DropDownStyle = ComboBoxStyle.DropDownList
        cmbLabelReq.FormattingEnabled = True
        cmbLabelReq.Items.AddRange(New Object() {"Select", "Label (Doc only)", "Label (Doc + Src)", "Requisition"})
        cmbLabelReq.Location = New Point(682, 22)
        cmbLabelReq.Margin = New Padding(5, 6, 5, 6)
        cmbLabelReq.Name = "cmbLabelReq"
        cmbLabelReq.Size = New Size(224, 33)
        cmbLabelReq.TabIndex = 9
        ' 
        ' cmbDestination
        ' 
        cmbDestination.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDestination.FormattingEnabled = True
        cmbDestination.Items.AddRange(New Object() {"Printer", "Screen"})
        cmbDestination.Location = New Point(1115, 19)
        cmbDestination.Margin = New Padding(5, 6, 5, 6)
        cmbDestination.Name = "cmbDestination"
        cmbDestination.Size = New Size(130, 33)
        cmbDestination.TabIndex = 8
        ' 
        ' btnSel
        ' 
        btnSel.Image = CType(resources.GetObject("btnSel.Image"), Image)
        btnSel.Location = New Point(1059, 16)
        btnSel.Margin = New Padding(5, 6, 5, 6)
        btnSel.Name = "btnSel"
        btnSel.Size = New Size(46, 44)
        btnSel.TabIndex = 7
        btnSel.UseVisualStyleBackColor = True
        ' 
        ' txtComment
        ' 
        txtComment.Location = New Point(21, 317)
        txtComment.Margin = New Padding(5, 6, 5, 6)
        txtComment.Multiline = True
        txtComment.Name = "txtComment"
        txtComment.ScrollBars = ScrollBars.Vertical
        txtComment.Size = New Size(1358, 70)
        txtComment.TabIndex = 21
        ' 
        ' btnPrint
        ' 
        btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), Image)
        btnPrint.ImageAlign = ContentAlignment.MiddleRight
        btnPrint.Location = New Point(1256, 17)
        btnPrint.Margin = New Padding(5, 6, 5, 6)
        btnPrint.Name = "btnPrint"
        btnPrint.Size = New Size(125, 44)
        btnPrint.TabIndex = 6
        btnPrint.Text = "Print"
        btnPrint.TextImageRelation = TextImageRelation.ImageBeforeText
        btnPrint.UseVisualStyleBackColor = True
        ' 
        ' btnDesel
        ' 
        btnDesel.Image = CType(resources.GetObject("btnDesel.Image"), Image)
        btnDesel.Location = New Point(1002, 19)
        btnDesel.Margin = New Padding(5, 6, 5, 6)
        btnDesel.Name = "btnDesel"
        btnDesel.Size = New Size(46, 44)
        btnDesel.TabIndex = 5
        btnDesel.UseVisualStyleBackColor = True
        ' 
        ' dgvSources
        ' 
        dgvSources.AllowUserToAddRows = False
        dgvSources.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = Color.FromArgb(CByte(255), CByte(192), CByte(192))
        dgvSources.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = SystemColors.Control
        DataGridViewCellStyle3.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle3.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = DataGridViewTriState.True
        dgvSources.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        dgvSources.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvSources.Columns.AddRange(New DataGridViewColumn() {SrcID, SrcNo, SrcName, SrcQty, SrcDate, SrcTime, SrcTemp, chkRec})
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = SystemColors.Window
        DataGridViewCellStyle4.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle4.ForeColor = Color.DarkBlue
        DataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = DataGridViewTriState.False
        dgvSources.DefaultCellStyle = DataGridViewCellStyle4
        dgvSources.Location = New Point(21, 69)
        dgvSources.Margin = New Padding(5, 6, 5, 6)
        dgvSources.Name = "dgvSources"
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = SystemColors.Control
        DataGridViewCellStyle5.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle5.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = DataGridViewTriState.True
        dgvSources.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
        dgvSources.RowHeadersVisible = False
        dgvSources.RowHeadersWidth = 56
        dgvSources.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvSources.Size = New Size(1355, 214)
        dgvSources.TabIndex = 20
        dgvSources.TabStop = False
        ' 
        ' SrcID
        ' 
        SrcID.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        SrcID.HeaderText = "ID"
        SrcID.MaxInputLength = 5
        SrcID.MinimumWidth = 7
        SrcID.Name = "SrcID"
        SrcID.ReadOnly = True
        SrcID.SortMode = DataGridViewColumnSortMode.NotSortable
        SrcID.Visible = False
        ' 
        ' SrcNo
        ' 
        SrcNo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        SrcNo.FillWeight = 45.0F
        SrcNo.HeaderText = "No."
        SrcNo.MaxInputLength = 5
        SrcNo.MinimumWidth = 7
        SrcNo.Name = "SrcNo"
        SrcNo.ReadOnly = True
        SrcNo.Resizable = DataGridViewTriState.False
        SrcNo.SortMode = DataGridViewColumnSortMode.Programmatic
        ' 
        ' SrcName
        ' 
        SrcName.FillWeight = 200.0F
        SrcName.HeaderText = "Source Name"
        SrcName.MinimumWidth = 7
        SrcName.Name = "SrcName"
        SrcName.ReadOnly = True
        SrcName.SortMode = DataGridViewColumnSortMode.NotSortable
        SrcName.Width = 200
        ' 
        ' SrcQty
        ' 
        SrcQty.FillWeight = 60.0F
        SrcQty.HeaderText = "Qty"
        SrcQty.MinimumWidth = 7
        SrcQty.Name = "SrcQty"
        SrcQty.SortMode = DataGridViewColumnSortMode.NotSortable
        SrcQty.Width = 60
        ' 
        ' SrcDate
        ' 
        SrcDate.FillWeight = 90.0F
        SrcDate.HeaderText = "Drawn On"
        SrcDate.MinimumWidth = 7
        SrcDate.Name = "SrcDate"
        SrcDate.SortMode = DataGridViewColumnSortMode.NotSortable
        SrcDate.Width = 90
        ' 
        ' SrcTime
        ' 
        SrcTime.FillWeight = 80.0F
        SrcTime.HeaderText = "Time"
        SrcTime.MinimumWidth = 7
        SrcTime.Name = "SrcTime"
        SrcTime.SortMode = DataGridViewColumnSortMode.NotSortable
        SrcTime.Width = 80
        ' 
        ' SrcTemp
        ' 
        SrcTemp.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
        SrcTemp.FillWeight = 120.0F
        SrcTemp.HeaderText = "Temp"
        SrcTemp.Items.AddRange(New Object() {"Refrigerated", "Room Temp", "Frozen", "Incubated"})
        SrcTemp.MinimumWidth = 7
        SrcTemp.Name = "SrcTemp"
        SrcTemp.Resizable = DataGridViewTriState.True
        SrcTemp.Width = 120
        ' 
        ' chkRec
        ' 
        chkRec.FillWeight = 70.0F
        chkRec.HeaderText = "Received?"
        chkRec.MinimumWidth = 7
        chkRec.Name = "chkRec"
        chkRec.Resizable = DataGridViewTriState.True
        chkRec.Width = 70
        ' 
        ' frmRemoteAcc
        ' 
        AcceptButton = btnReceive
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode = AutoScaleMode.Font
        AutoScroll = True
        AutoSize = True
        ClientSize = New Size(1438, 1273)
        Controls.Add(GroupBox3)
        Controls.Add(GroupBox2)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmRemoteAcc"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Remote Accessions"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        searchPanel.ResumeLayout(False)
        searchPanel.PerformLayout()
        patientpanel.ResumeLayout(False)
        patientpanel.PerformLayout()
        CType(dgvAccessions, ComponentModel.ISupportInitialize).EndInit()
        GroupBox3.ResumeLayout(False)
        GroupBox3.PerformLayout()
        CType(dgvSources, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnReceive As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvAccessions As System.Windows.Forms.DataGridView
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents cmbProvider As System.Windows.Forms.ComboBox
    Friend WithEvents cmbRoute As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvSources As System.Windows.Forms.DataGridView
    Friend WithEvents chkReqOrd As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents btnReport As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmbTerm As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnDesel As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnSel As System.Windows.Forms.Button
    Friend WithEvents cmbDestination As System.Windows.Forms.ComboBox
    'Friend WithEvents AxAcroPDF1 As AxAcroPDFLib.AxAcroPDF
    Friend WithEvents cmbLabelReq As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbEntry As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents SrcID As DataGridViewTextBoxColumn
    Friend WithEvents SrcNo As DataGridViewTextBoxColumn
    Friend WithEvents SrcName As DataGridViewTextBoxColumn
    Friend WithEvents SrcQty As DataGridViewTextBoxColumn
    Friend WithEvents SrcDate As DataGridViewTextBoxColumn
    Friend WithEvents SrcTime As DataGridViewTextBoxColumn
    Friend WithEvents SrcTemp As DataGridViewComboBoxColumn
    Friend WithEvents chkRec As DataGridViewCheckBoxColumn
    Friend WithEvents RecAll As System.Windows.Forms.Button
    Friend WithEvents ReqChk As System.Windows.Forms.CheckBox
    Friend WithEvents LabelChk As System.Windows.Forms.CheckBox
    Friend WithEvents SelectChk As System.Windows.Forms.CheckBox
    Friend WithEvents Accession As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ReqID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AccDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Patient As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MRN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Provider As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Req As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Label As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Selected As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents countLaberls As System.Windows.Forms.TextBox
    Friend WithEvents ChkRemotePrint As System.Windows.Forms.CheckBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents txtPatientID As System.Windows.Forms.TextBox
    Friend WithEvents btnPatLook As System.Windows.Forms.Button
    Friend WithEvents searchPanel As System.Windows.Forms.Panel
    Friend WithEvents patientpanel As System.Windows.Forms.Panel
    Friend WithEvents lblTerm As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtTerm As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents nxtbtn As System.Windows.Forms.Button
    Friend WithEvents pagtxt As System.Windows.Forms.TextBox
    Friend WithEvents prevbtn As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtAccPageCount As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtAccCount As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtAccCountOnWindow As System.Windows.Forms.TextBox
    Friend WithEvents rl As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents dtpAccDate As DateTimePicker
End Class
