<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmManagePanics
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmManagePanics))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Label13 = New Label()
        txtPatient = New TextBox()
        Label10 = New Label()
        txtTerm = New TextBox()
        txtTotal = New TextBox()
        lblTerm = New Label()
        Label9 = New Label()
        dtpTo = New DateTimePicker()
        txtPanics = New TextBox()
        cmbCriteria = New ComboBox()
        dtpFrom = New DateTimePicker()
        Label12 = New Label()
        btnGo = New Button()
        lblProvider = New Label()
        Label3 = New Label()
        Label5 = New Label()
        LabelP = New Label()
        lblTo = New Label()
        txtPatients = New TextBox()
        txtProvider = New TextBox()
        Label4 = New Label()
        txtProviders = New TextBox()
        dgvResults = New DataGridView()
        TGPID = New DataGridViewTextBoxColumn()
        Test = New DataGridViewTextBoxColumn()
        Stat = New DataGridViewTextBoxColumn()
        Flag = New DataGridViewTextBoxColumn()
        Range = New DataGridViewTextBoxColumn()
        Unit = New DataGridViewTextBoxColumn()
        dgvAccessions = New DataGridView()
        AccID = New DataGridViewTextBoxColumn()
        Dated = New DataGridViewTextBoxColumn()
        Provider = New DataGridViewTextBoxColumn()
        lblFrom = New Label()
        GroupBox1 = New GroupBox()
        txtComment = New TextBox()
        Label6 = New Label()
        btnLog = New Button()
        btnReport = New Button()
        btnFax = New Button()
        txtOrdPhone = New TextBox()
        Label7 = New Label()
        btnDismis = New Button()
        Label8 = New Label()
        dgvFaxNos = New DataGridView()
        Fax = New DataGridViewCheckBoxColumn()
        FaxNo = New DataGridViewTextBoxColumn()
        txtAccID = New TextBox()
        txtAttPhone = New TextBox()
        Label11 = New Label()
        Label14 = New Label()
        txtInstructions = New RichTextBox()
        Label15 = New Label()
        btnFailure = New Button()
        ToolTip1 = New ToolTip(components)
        txtReason = New TextBox()
        Label1 = New Label()
        CType(dgvResults, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvAccessions, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox1.SuspendLayout()
        CType(dgvFaxNos, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label13
        ' 
        Label13.ForeColor = Color.DarkBlue
        Label13.Location = New Point(785, 113)
        Label13.Margin = New Padding(5, 0, 5, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(200, 29)
        Label13.TabIndex = 61
        Label13.Text = "Patient"
        ' 
        ' txtPatient
        ' 
        txtPatient.BackColor = Color.LavenderBlush
        txtPatient.Location = New Point(785, 148)
        txtPatient.Margin = New Padding(5, 6, 5, 6)
        txtPatient.Multiline = True
        txtPatient.Name = "txtPatient"
        txtPatient.ReadOnly = True
        txtPatient.ScrollBars = ScrollBars.Vertical
        txtPatient.Size = New Size(516, 112)
        txtPatient.TabIndex = 60
        ' 
        ' Label10
        ' 
        Label10.ForeColor = Color.DarkBlue
        Label10.Location = New Point(568, 40)
        Label10.Margin = New Padding(5, 0, 5, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(157, 29)
        Label10.TabIndex = 57
        Label10.Text = "Total"
        Label10.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtTerm
        ' 
        txtTerm.Location = New Point(790, 46)
        txtTerm.Margin = New Padding(5, 6, 5, 6)
        txtTerm.Name = "txtTerm"
        txtTerm.Size = New Size(402, 31)
        txtTerm.TabIndex = 42
        ' 
        ' txtTotal
        ' 
        txtTotal.Location = New Point(568, 75)
        txtTotal.Margin = New Padding(5, 6, 5, 6)
        txtTotal.Name = "txtTotal"
        txtTotal.ReadOnly = True
        txtTotal.Size = New Size(154, 31)
        txtTotal.TabIndex = 56
        txtTotal.TextAlign = HorizontalAlignment.Center
        ' 
        ' lblTerm
        ' 
        lblTerm.ForeColor = Color.DarkBlue
        lblTerm.Location = New Point(800, 10)
        lblTerm.Margin = New Padding(5, 0, 5, 0)
        lblTerm.Name = "lblTerm"
        lblTerm.Size = New Size(292, 29)
        lblTerm.TabIndex = 43
        lblTerm.Text = "Search Term"
        ' 
        ' Label9
        ' 
        Label9.ForeColor = Color.DarkBlue
        Label9.Location = New Point(390, 40)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(157, 29)
        Label9.TabIndex = 54
        Label9.Text = "Critical Results"
        Label9.TextAlign = ContentAlignment.TopCenter
        ' 
        ' dtpTo
        ' 
        dtpTo.Format = DateTimePickerFormat.Short
        dtpTo.Location = New Point(575, 44)
        dtpTo.Margin = New Padding(5, 6, 5, 6)
        dtpTo.Name = "dtpTo"
        dtpTo.Size = New Size(182, 31)
        dtpTo.TabIndex = 33
        ' 
        ' txtPanics
        ' 
        txtPanics.Location = New Point(390, 75)
        txtPanics.Margin = New Padding(5, 6, 5, 6)
        txtPanics.Name = "txtPanics"
        txtPanics.ReadOnly = True
        txtPanics.Size = New Size(154, 31)
        txtPanics.TabIndex = 53
        txtPanics.TextAlign = HorizontalAlignment.Center
        ' 
        ' cmbCriteria
        ' 
        cmbCriteria.FormattingEnabled = True
        cmbCriteria.Items.AddRange(New Object() {"Accession ID", "Provider ID", "Provider Name (Last, First)", "Patient Name (Last, First)"})
        cmbCriteria.Location = New Point(20, 44)
        cmbCriteria.Margin = New Padding(5, 6, 5, 6)
        cmbCriteria.Name = "cmbCriteria"
        cmbCriteria.Size = New Size(312, 33)
        cmbCriteria.TabIndex = 40
        ' 
        ' dtpFrom
        ' 
        dtpFrom.Format = DateTimePickerFormat.Short
        dtpFrom.Location = New Point(367, 44)
        dtpFrom.Margin = New Padding(5, 6, 5, 6)
        dtpFrom.Name = "dtpFrom"
        dtpFrom.Size = New Size(182, 31)
        dtpFrom.TabIndex = 32
        ' 
        ' Label12
        ' 
        Label12.ForeColor = Color.DarkBlue
        Label12.Location = New Point(20, 298)
        Label12.Margin = New Padding(5, 0, 5, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(355, 29)
        Label12.TabIndex = 59
        Label12.Text = "Accessions with Critical Results"
        ' 
        ' btnGo
        ' 
        btnGo.Image = CType(resources.GetObject("btnGo.Image"), Image)
        btnGo.Location = New Point(1220, 40)
        btnGo.Margin = New Padding(5, 6, 5, 6)
        btnGo.Name = "btnGo"
        btnGo.Size = New Size(53, 48)
        btnGo.TabIndex = 38
        btnGo.UseVisualStyleBackColor = True
        ' 
        ' lblProvider
        ' 
        lblProvider.ForeColor = Color.DarkBlue
        lblProvider.Location = New Point(547, 573)
        lblProvider.Margin = New Padding(5, 0, 5, 0)
        lblProvider.Name = "lblProvider"
        lblProvider.Size = New Size(177, 29)
        lblProvider.TabIndex = 58
        lblProvider.Text = "Provider"
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(15, 10)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(207, 29)
        Label3.TabIndex = 41
        Label3.Text = "Criteria"
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.DarkBlue
        Label5.Location = New Point(540, 298)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(205, 29)
        Label5.TabIndex = 55
        Label5.Text = "Results"
        ' 
        ' LabelP
        ' 
        LabelP.ForeColor = Color.DarkBlue
        LabelP.Location = New Point(207, 40)
        LabelP.Margin = New Padding(5, 0, 5, 0)
        LabelP.Name = "LabelP"
        LabelP.Size = New Size(157, 29)
        LabelP.TabIndex = 46
        LabelP.Text = "Patients"
        LabelP.TextAlign = ContentAlignment.TopCenter
        ' 
        ' lblTo
        ' 
        lblTo.ForeColor = Color.Magenta
        lblTo.Location = New Point(575, 10)
        lblTo.Margin = New Padding(5, 0, 5, 0)
        lblTo.Name = "lblTo"
        lblTo.Size = New Size(197, 29)
        lblTo.TabIndex = 35
        lblTo.Text = "To"
        ' 
        ' txtPatients
        ' 
        txtPatients.Location = New Point(207, 75)
        txtPatients.Margin = New Padding(5, 6, 5, 6)
        txtPatients.Name = "txtPatients"
        txtPatients.ReadOnly = True
        txtPatients.Size = New Size(154, 31)
        txtPatients.TabIndex = 45
        txtPatients.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtProvider
        ' 
        txtProvider.BackColor = Color.Azure
        txtProvider.Location = New Point(545, 608)
        txtProvider.Margin = New Padding(5, 6, 5, 6)
        txtProvider.Name = "txtProvider"
        txtProvider.ReadOnly = True
        txtProvider.Size = New Size(756, 31)
        txtProvider.TabIndex = 37
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(27, 40)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(157, 29)
        Label4.TabIndex = 44
        Label4.Text = "Providers"
        Label4.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtProviders
        ' 
        txtProviders.Location = New Point(27, 75)
        txtProviders.Margin = New Padding(5, 6, 5, 6)
        txtProviders.Name = "txtProviders"
        txtProviders.ReadOnly = True
        txtProviders.Size = New Size(154, 31)
        txtProviders.TabIndex = 39
        txtProviders.TextAlign = HorizontalAlignment.Center
        ' 
        ' dgvResults
        ' 
        dgvResults.AllowUserToAddRows = False
        dgvResults.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.LavenderBlush
        dgvResults.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvResults.Columns.AddRange(New DataGridViewColumn() {TGPID, Test, Stat, Flag, Range, Unit})
        dgvResults.Location = New Point(545, 333)
        dgvResults.Margin = New Padding(5, 6, 5, 6)
        dgvResults.Name = "dgvResults"
        dgvResults.ReadOnly = True
        dgvResults.RowHeadersVisible = False
        dgvResults.RowHeadersWidth = 62
        DataGridViewCellStyle2.BackColor = Color.Azure
        dgvResults.RowsDefaultCellStyle = DataGridViewCellStyle2
        dgvResults.Size = New Size(758, 212)
        dgvResults.TabIndex = 36
        ' 
        ' TGPID
        ' 
        TGPID.FillWeight = 80F
        TGPID.HeaderText = "ID"
        TGPID.MinimumWidth = 8
        TGPID.Name = "TGPID"
        TGPID.ReadOnly = True
        TGPID.Visible = False
        ' 
        ' Test
        ' 
        Test.FillWeight = 180F
        Test.HeaderText = "Test Name"
        Test.MinimumWidth = 8
        Test.Name = "Test"
        Test.ReadOnly = True
        ' 
        ' Stat
        ' 
        Stat.FillWeight = 90F
        Stat.HeaderText = "Result"
        Stat.MinimumWidth = 8
        Stat.Name = "Stat"
        Stat.ReadOnly = True
        Stat.Resizable = DataGridViewTriState.True
        Stat.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Flag
        ' 
        Flag.FillWeight = 40F
        Flag.HeaderText = "Flag"
        Flag.MinimumWidth = 8
        Flag.Name = "Flag"
        Flag.ReadOnly = True
        Flag.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Range
        ' 
        Range.FillWeight = 90F
        Range.HeaderText = "Range"
        Range.MinimumWidth = 8
        Range.Name = "Range"
        Range.ReadOnly = True
        Range.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Unit
        ' 
        Unit.FillWeight = 52F
        Unit.HeaderText = "Unit"
        Unit.MinimumWidth = 8
        Unit.Name = "Unit"
        Unit.ReadOnly = True
        Unit.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' dgvAccessions
        ' 
        dgvAccessions.AllowUserToAddRows = False
        dgvAccessions.AllowUserToDeleteRows = False
        DataGridViewCellStyle3.BackColor = Color.SeaShell
        dgvAccessions.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        dgvAccessions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvAccessions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvAccessions.Columns.AddRange(New DataGridViewColumn() {AccID, Dated, Provider})
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = Color.Lavender
        DataGridViewCellStyle4.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle4.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = DataGridViewTriState.False
        dgvAccessions.DefaultCellStyle = DataGridViewCellStyle4
        dgvAccessions.Location = New Point(20, 333)
        dgvAccessions.Margin = New Padding(5, 6, 5, 6)
        dgvAccessions.MultiSelect = False
        dgvAccessions.Name = "dgvAccessions"
        dgvAccessions.ReadOnly = True
        dgvAccessions.RowHeadersVisible = False
        dgvAccessions.RowHeadersWidth = 62
        dgvAccessions.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvAccessions.Size = New Size(488, 806)
        dgvAccessions.TabIndex = 34
        ' 
        ' AccID
        ' 
        AccID.FillWeight = 90F
        AccID.HeaderText = "Acc ID"
        AccID.MinimumWidth = 8
        AccID.Name = "AccID"
        AccID.ReadOnly = True
        ' 
        ' Dated
        ' 
        Dated.FillWeight = 80F
        Dated.HeaderText = "Dated"
        Dated.MinimumWidth = 8
        Dated.Name = "Dated"
        Dated.ReadOnly = True
        ' 
        ' Provider
        ' 
        Provider.FillWeight = 120F
        Provider.HeaderText = "Provider"
        Provider.MinimumWidth = 8
        Provider.Name = "Provider"
        Provider.ReadOnly = True
        ' 
        ' lblFrom
        ' 
        lblFrom.ForeColor = Color.Red
        lblFrom.Location = New Point(367, 10)
        lblFrom.Margin = New Padding(5, 0, 5, 0)
        lblFrom.Name = "lblFrom"
        lblFrom.Size = New Size(198, 29)
        lblFrom.TabIndex = 31
        lblFrom.Text = "From"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(txtPanics)
        GroupBox1.Controls.Add(txtProviders)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(txtPatients)
        GroupBox1.Controls.Add(LabelP)
        GroupBox1.Controls.Add(Label10)
        GroupBox1.Controls.Add(Label9)
        GroupBox1.Controls.Add(txtTotal)
        GroupBox1.Location = New Point(20, 113)
        GroupBox1.Margin = New Padding(5, 6, 5, 6)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(5, 6, 5, 6)
        GroupBox1.Size = New Size(740, 150)
        GroupBox1.TabIndex = 66
        GroupBox1.TabStop = False
        GroupBox1.Text = "Summary"
        ' 
        ' txtComment
        ' 
        txtComment.Location = New Point(1147, 1013)
        txtComment.Margin = New Padding(5, 6, 5, 6)
        txtComment.MaxLength = 20
        txtComment.Name = "txtComment"
        txtComment.Size = New Size(154, 31)
        txtComment.TabIndex = 67
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.DarkBlue
        Label6.Location = New Point(1147, 979)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(155, 29)
        Label6.TabIndex = 68
        Label6.Text = "Receipient Name"
        ' 
        ' btnLog
        ' 
        btnLog.Enabled = False
        btnLog.Location = New Point(1172, 1081)
        btnLog.Margin = New Padding(5, 6, 5, 6)
        btnLog.Name = "btnLog"
        btnLog.Size = New Size(132, 58)
        btnLog.TabIndex = 69
        btnLog.Text = "Log Success"
        btnLog.UseVisualStyleBackColor = True
        ' 
        ' btnReport
        ' 
        btnReport.Enabled = False
        btnReport.Location = New Point(685, 1081)
        btnReport.Margin = New Padding(5, 6, 5, 6)
        btnReport.Name = "btnReport"
        btnReport.Size = New Size(130, 58)
        btnReport.TabIndex = 70
        btnReport.Text = "Print Report"
        btnReport.UseVisualStyleBackColor = True
        ' 
        ' btnFax
        ' 
        btnFax.Enabled = False
        btnFax.Location = New Point(545, 1081)
        btnFax.Margin = New Padding(5, 6, 5, 6)
        btnFax.Name = "btnFax"
        btnFax.Size = New Size(130, 58)
        btnFax.TabIndex = 71
        btnFax.Text = "Fax Report"
        btnFax.UseVisualStyleBackColor = True
        ' 
        ' txtOrdPhone
        ' 
        txtOrdPhone.BackColor = Color.White
        txtOrdPhone.Location = New Point(897, 865)
        txtOrdPhone.Margin = New Padding(5, 6, 5, 6)
        txtOrdPhone.MaxLength = 20
        txtOrdPhone.Name = "txtOrdPhone"
        txtOrdPhone.ReadOnly = True
        txtOrdPhone.Size = New Size(291, 31)
        txtOrdPhone.TabIndex = 72
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.DarkBlue
        Label7.Location = New Point(542, 831)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(205, 29)
        Label7.TabIndex = 73
        Label7.Text = "Fax Number(s)"
        ' 
        ' btnDismis
        ' 
        btnDismis.Enabled = False
        btnDismis.Location = New Point(825, 1081)
        btnDismis.Margin = New Padding(5, 6, 5, 6)
        btnDismis.Name = "btnDismis"
        btnDismis.Size = New Size(130, 58)
        btnDismis.TabIndex = 74
        btnDismis.Text = "Dismiss"
        ToolTip1.SetToolTip(btnDismis, "This button removes the Accession from the Panic que and its use is discouraged.")
        btnDismis.UseVisualStyleBackColor = True
        ' 
        ' Label8
        ' 
        Label8.ForeColor = Color.DarkBlue
        Label8.Location = New Point(902, 831)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(257, 29)
        Label8.TabIndex = 75
        Label8.Text = "Phone Number(s)"
        ' 
        ' dgvFaxNos
        ' 
        dgvFaxNos.AllowUserToDeleteRows = False
        DataGridViewCellStyle5.BackColor = Color.AliceBlue
        dgvFaxNos.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        dgvFaxNos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvFaxNos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvFaxNos.Columns.AddRange(New DataGridViewColumn() {Fax, FaxNo})
        dgvFaxNos.Location = New Point(545, 865)
        dgvFaxNos.Margin = New Padding(5, 6, 5, 6)
        dgvFaxNos.Name = "dgvFaxNos"
        dgvFaxNos.RowHeadersVisible = False
        dgvFaxNos.RowHeadersWidth = 62
        dgvFaxNos.Size = New Size(325, 187)
        dgvFaxNos.TabIndex = 76
        ' 
        ' Fax
        ' 
        Fax.FillWeight = 40F
        Fax.HeaderText = "Fax?"
        Fax.MinimumWidth = 8
        Fax.Name = "Fax"
        Fax.Resizable = DataGridViewTriState.True
        ' 
        ' FaxNo
        ' 
        FaxNo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        FaxNo.HeaderText = "Number"
        FaxNo.MaxInputLength = 15
        FaxNo.MinimumWidth = 8
        FaxNo.Name = "FaxNo"
        FaxNo.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' txtAccID
        ' 
        txtAccID.Location = New Point(857, 560)
        txtAccID.Margin = New Padding(5, 6, 5, 6)
        txtAccID.MaxLength = 20
        txtAccID.Name = "txtAccID"
        txtAccID.Size = New Size(167, 31)
        txtAccID.TabIndex = 77
        txtAccID.Visible = False
        ' 
        ' txtAttPhone
        ' 
        txtAttPhone.BackColor = Color.White
        txtAttPhone.Location = New Point(897, 915)
        txtAttPhone.Margin = New Padding(5, 6, 5, 6)
        txtAttPhone.MaxLength = 20
        txtAttPhone.Name = "txtAttPhone"
        txtAttPhone.ReadOnly = True
        txtAttPhone.Size = New Size(291, 31)
        txtAttPhone.TabIndex = 78
        ' 
        ' Label11
        ' 
        Label11.ForeColor = Color.DarkBlue
        Label11.Location = New Point(1200, 871)
        Label11.Margin = New Padding(5, 0, 5, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(105, 29)
        Label11.TabIndex = 79
        Label11.Text = "Ordering"
        ' 
        ' Label14
        ' 
        Label14.ForeColor = Color.DarkBlue
        Label14.Location = New Point(1200, 921)
        Label14.Margin = New Padding(5, 0, 5, 0)
        Label14.Name = "Label14"
        Label14.Size = New Size(98, 29)
        Label14.TabIndex = 80
        Label14.Text = "Attending"
        ' 
        ' txtInstructions
        ' 
        txtInstructions.Location = New Point(660, 669)
        txtInstructions.Margin = New Padding(5, 6, 5, 6)
        txtInstructions.MaxLength = 800
        txtInstructions.Name = "txtInstructions"
        txtInstructions.ReadOnly = True
        txtInstructions.ScrollBars = RichTextBoxScrollBars.Vertical
        txtInstructions.Size = New Size(639, 129)
        txtInstructions.TabIndex = 81
        txtInstructions.Text = ""
        ' 
        ' Label15
        ' 
        Label15.ForeColor = Color.DarkBlue
        Label15.Location = New Point(545, 669)
        Label15.Margin = New Padding(5, 0, 5, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(105, 29)
        Label15.TabIndex = 82
        Label15.Text = "Instructions"
        Label15.TextAlign = ContentAlignment.TopRight
        ' 
        ' btnFailure
        ' 
        btnFailure.Enabled = False
        btnFailure.Location = New Point(1007, 1081)
        btnFailure.Margin = New Padding(5, 6, 5, 6)
        btnFailure.Name = "btnFailure"
        btnFailure.Size = New Size(130, 58)
        btnFailure.TabIndex = 83
        btnFailure.Text = "Log Failure"
        ToolTip1.SetToolTip(btnFailure, "This button writes failed attempt log only in 10 minutes intervals")
        btnFailure.UseVisualStyleBackColor = True
        ' 
        ' txtReason
        ' 
        txtReason.Location = New Point(897, 1013)
        txtReason.Margin = New Padding(5, 6, 5, 6)
        txtReason.MaxLength = 20
        txtReason.Name = "txtReason"
        txtReason.Size = New Size(237, 31)
        txtReason.TabIndex = 84
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(897, 979)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(240, 29)
        Label1.TabIndex = 85
        Label1.Text = "Reason"
        ' 
        ' frmManagePanics
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1330, 1162)
        Controls.Add(Label1)
        Controls.Add(txtReason)
        Controls.Add(btnFailure)
        Controls.Add(Label15)
        Controls.Add(txtInstructions)
        Controls.Add(Label14)
        Controls.Add(Label11)
        Controls.Add(txtAttPhone)
        Controls.Add(txtAccID)
        Controls.Add(dgvFaxNos)
        Controls.Add(Label8)
        Controls.Add(btnDismis)
        Controls.Add(Label7)
        Controls.Add(txtOrdPhone)
        Controls.Add(btnFax)
        Controls.Add(btnReport)
        Controls.Add(btnLog)
        Controls.Add(Label6)
        Controls.Add(txtComment)
        Controls.Add(GroupBox1)
        Controls.Add(Label13)
        Controls.Add(txtPatient)
        Controls.Add(txtTerm)
        Controls.Add(lblTerm)
        Controls.Add(dtpTo)
        Controls.Add(cmbCriteria)
        Controls.Add(dtpFrom)
        Controls.Add(Label12)
        Controls.Add(btnGo)
        Controls.Add(lblProvider)
        Controls.Add(Label3)
        Controls.Add(Label5)
        Controls.Add(lblTo)
        Controls.Add(txtProvider)
        Controls.Add(dgvResults)
        Controls.Add(dgvAccessions)
        Controls.Add(lblFrom)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmManagePanics"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Manage Critical Results"
        CType(dgvResults, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvAccessions, ComponentModel.ISupportInitialize).EndInit()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        CType(dgvFaxNos, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtPatient As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtTerm As System.Windows.Forms.TextBox
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents lblTerm As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtPanics As System.Windows.Forms.TextBox
    Friend WithEvents cmbCriteria As System.Windows.Forms.ComboBox
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnGo As System.Windows.Forms.Button
    Friend WithEvents lblProvider As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents LabelP As System.Windows.Forms.Label
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents txtPatients As System.Windows.Forms.TextBox
    Friend WithEvents txtProvider As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtProviders As System.Windows.Forms.TextBox
    Friend WithEvents dgvResults As System.Windows.Forms.DataGridView
    Friend WithEvents dgvAccessions As System.Windows.Forms.DataGridView
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnLog As System.Windows.Forms.Button
    Friend WithEvents btnReport As System.Windows.Forms.Button
    Friend WithEvents btnFax As System.Windows.Forms.Button
    Friend WithEvents txtOrdPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents AccID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Dated As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Provider As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TGPID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Test As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Stat As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Flag As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Range As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnDismis As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dgvFaxNos As System.Windows.Forms.DataGridView
    Friend WithEvents txtAccID As System.Windows.Forms.TextBox
    Friend WithEvents txtAttPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Fax As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents FaxNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtInstructions As System.Windows.Forms.RichTextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents btnFailure As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents txtReason As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
