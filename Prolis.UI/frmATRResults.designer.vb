<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmATRResults
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmATRResults))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        btnAccQC = New ToolStripButton()
        ToolStripButton2 = New ToolStripSeparator()
        btnSave = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnRelease = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnBlock = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        dtpTo = New DateTimePicker()
        dtpFrom = New DateTimePicker()
        lblTo = New Label()
        lblFrom = New Label()
        btnLoad = New Button()
        lblPos = New Label()
        btnFirst = New Button()
        btnPrevious = New Button()
        btnNext = New Button()
        btnLast = New Button()
        Label3 = New Label()
        txtAccID = New TextBox()
        txtAccDate = New TextBox()
        Label4 = New Label()
        txtProvider = New TextBox()
        txtPatient = New TextBox()
        Label5 = New Label()
        Label6 = New Label()
        txtRTF = New RichTextBox()
        txtComment = New TextBox()
        cmbDirector = New ComboBox()
        Label13 = New Label()
        Label9 = New Label()
        txtNote = New TextBox()
        Label8 = New Label()
        Label7 = New Label()
        txtRptTime = New MaskedTextBox()
        txtRptDate = New MaskedTextBox()
        dgvExceptions = New DataGridView()
        Acc = New DataGridViewTextBoxColumn()
        chkRelease = New CheckBox()
        lblExceptions = New Label()
        Label16 = New Label()
        txtMeds = New TextBox()
        cmbWID = New ComboBox()
        Label1 = New Label()
        dgvResults = New DataGridView()
        Test_ID = New DataGridViewTextBoxColumn()
        Analyte = New DataGridViewTextBoxColumn()
        Result = New DataGridViewTextBoxColumn()
        Reflux = New DataGridViewImageColumn()
        Flag = New DataGridViewTextBoxColumn()
        Normal = New DataGridViewTextBoxColumn()
        History = New DataGridViewImageColumn()
        Note = New DataGridViewImageColumn()
        IResult = New DataGridViewImageColumn()
        TResult = New DataGridViewImageColumn()
        Release = New DataGridViewCheckBoxColumn()
        LorN = New DataGridViewTextBoxColumn()
        AccID = New DataGridViewTextBoxColumn()
        Cause = New DataGridViewTextBoxColumn()
        Reflexer = New DataGridViewTextBoxColumn()
        Reflexed = New DataGridViewTextBoxColumn()
        Cmnt = New DataGridViewTextBoxColumn()
        RTFRes = New DataGridViewTextBoxColumn()
        ImgRes = New DataGridViewImageColumn()
        ESig = New DataGridViewCheckBoxColumn()
        Behavior = New DataGridViewTextBoxColumn()
        ToolStrip1.SuspendLayout()
        CType(dgvExceptions, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvResults, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnAccQC, ToolStripButton2, btnSave, ToolStripSeparator1, btnRelease, ToolStripSeparator2, btnBlock, ToolStripSeparator3, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(1367, 34)
        ToolStrip1.TabIndex = 4
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnAccQC
        ' 
        btnAccQC.CheckOnClick = True
        btnAccQC.ForeColor = Color.DarkBlue
        btnAccQC.Image = CType(resources.GetObject("btnAccQC.Image"), Image)
        btnAccQC.ImageTransparentColor = Color.Magenta
        btnAccQC.Name = "btnAccQC"
        btnAccQC.Size = New Size(138, 29)
        btnAccQC.Text = "Accessioned"
        ' 
        ' ToolStripButton2
        ' 
        ToolStripButton2.Name = "ToolStripButton2"
        ToolStripButton2.Size = New Size(6, 34)
        ' 
        ' btnSave
        ' 
        btnSave.Enabled = False
        btnSave.ForeColor = Color.DarkBlue
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
        ' btnRelease
        ' 
        btnRelease.DoubleClickEnabled = True
        btnRelease.ForeColor = Color.DarkBlue
        btnRelease.Image = CType(resources.GetObject("btnRelease.Image"), Image)
        btnRelease.ImageTransparentColor = Color.Magenta
        btnRelease.Name = "btnRelease"
        btnRelease.Size = New Size(98, 29)
        btnRelease.Text = "Release"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 34)
        ' 
        ' btnBlock
        ' 
        btnBlock.ForeColor = Color.DarkBlue
        btnBlock.Image = CType(resources.GetObject("btnBlock.Image"), Image)
        btnBlock.ImageTransparentColor = Color.Magenta
        btnBlock.Name = "btnBlock"
        btnBlock.Size = New Size(79, 29)
        btnBlock.Text = "Hold"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(6, 34)
        ' 
        ' btnCancel
        ' 
        btnCancel.ForeColor = Color.DarkBlue
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
        btnHelp.ForeColor = Color.DarkBlue
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.ImageTransparentColor = Color.Magenta
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(77, 29)
        btnHelp.Text = "Help"
        ' 
        ' dtpTo
        ' 
        dtpTo.Format = DateTimePickerFormat.Short
        dtpTo.Location = New Point(242, 112)
        dtpTo.Margin = New Padding(5, 6, 5, 6)
        dtpTo.Name = "dtpTo"
        dtpTo.Size = New Size(186, 31)
        dtpTo.TabIndex = 38
        ' 
        ' dtpFrom
        ' 
        dtpFrom.Format = DateTimePickerFormat.Short
        dtpFrom.Location = New Point(20, 112)
        dtpFrom.Margin = New Padding(5, 6, 5, 6)
        dtpFrom.Name = "dtpFrom"
        dtpFrom.Size = New Size(186, 31)
        dtpFrom.TabIndex = 37
        ' 
        ' lblTo
        ' 
        lblTo.ForeColor = Color.Magenta
        lblTo.Location = New Point(242, 77)
        lblTo.Margin = New Padding(5, 0, 5, 0)
        lblTo.Name = "lblTo"
        lblTo.Size = New Size(207, 29)
        lblTo.TabIndex = 39
        lblTo.Text = "To"
        ' 
        ' lblFrom
        ' 
        lblFrom.ForeColor = Color.Red
        lblFrom.Location = New Point(20, 75)
        lblFrom.Margin = New Padding(5, 0, 5, 0)
        lblFrom.Name = "lblFrom"
        lblFrom.Size = New Size(212, 29)
        lblFrom.TabIndex = 36
        lblFrom.Text = "From"
        ' 
        ' btnLoad
        ' 
        btnLoad.Image = CType(resources.GetObject("btnLoad.Image"), Image)
        btnLoad.Location = New Point(925, 100)
        btnLoad.Margin = New Padding(5, 6, 5, 6)
        btnLoad.Name = "btnLoad"
        btnLoad.Size = New Size(123, 69)
        btnLoad.TabIndex = 40
        btnLoad.Text = "Refresh"
        btnLoad.TextAlign = ContentAlignment.MiddleRight
        btnLoad.TextImageRelation = TextImageRelation.ImageBeforeText
        btnLoad.UseVisualStyleBackColor = True
        ' 
        ' lblPos
        ' 
        lblPos.BorderStyle = BorderStyle.FixedSingle
        lblPos.Location = New Point(157, 329)
        lblPos.Margin = New Padding(5, 0, 5, 0)
        lblPos.Name = "lblPos"
        lblPos.Size = New Size(442, 39)
        lblPos.TabIndex = 41
        lblPos.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' btnFirst
        ' 
        btnFirst.Image = CType(resources.GetObject("btnFirst.Image"), Image)
        btnFirst.Location = New Point(33, 321)
        btnFirst.Margin = New Padding(5, 6, 5, 6)
        btnFirst.Name = "btnFirst"
        btnFirst.Size = New Size(52, 56)
        btnFirst.TabIndex = 42
        btnFirst.UseVisualStyleBackColor = True
        ' 
        ' btnPrevious
        ' 
        btnPrevious.Image = CType(resources.GetObject("btnPrevious.Image"), Image)
        btnPrevious.Location = New Point(95, 321)
        btnPrevious.Margin = New Padding(5, 6, 5, 6)
        btnPrevious.Name = "btnPrevious"
        btnPrevious.Size = New Size(52, 56)
        btnPrevious.TabIndex = 43
        btnPrevious.UseVisualStyleBackColor = True
        ' 
        ' btnNext
        ' 
        btnNext.Image = CType(resources.GetObject("btnNext.Image"), Image)
        btnNext.Location = New Point(610, 323)
        btnNext.Margin = New Padding(5, 6, 5, 6)
        btnNext.Name = "btnNext"
        btnNext.Size = New Size(52, 56)
        btnNext.TabIndex = 44
        btnNext.UseVisualStyleBackColor = True
        ' 
        ' btnLast
        ' 
        btnLast.Image = CType(resources.GetObject("btnLast.Image"), Image)
        btnLast.Location = New Point(668, 321)
        btnLast.Margin = New Padding(5, 6, 5, 6)
        btnLast.Name = "btnLast"
        btnLast.Size = New Size(52, 56)
        btnLast.TabIndex = 45
        btnLast.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(28, 163)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(135, 29)
        Label3.TabIndex = 46
        Label3.Text = "Accession ID"
        ' 
        ' txtAccID
        ' 
        txtAccID.Location = New Point(20, 198)
        txtAccID.Margin = New Padding(5, 6, 5, 6)
        txtAccID.Name = "txtAccID"
        txtAccID.ReadOnly = True
        txtAccID.Size = New Size(156, 31)
        txtAccID.TabIndex = 47
        ' 
        ' txtAccDate
        ' 
        txtAccDate.Location = New Point(188, 198)
        txtAccDate.Margin = New Padding(5, 6, 5, 6)
        txtAccDate.Name = "txtAccDate"
        txtAccDate.ReadOnly = True
        txtAccDate.Size = New Size(184, 31)
        txtAccDate.TabIndex = 48
        ' 
        ' Label4
        ' 
        Label4.Location = New Point(208, 163)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(167, 29)
        Label4.TabIndex = 49
        Label4.Text = "Accession Date"
        ' 
        ' txtProvider
        ' 
        txtProvider.Location = New Point(405, 198)
        txtProvider.Margin = New Padding(5, 6, 5, 6)
        txtProvider.Name = "txtProvider"
        txtProvider.ReadOnly = True
        txtProvider.Size = New Size(312, 31)
        txtProvider.TabIndex = 50
        ' 
        ' txtPatient
        ' 
        txtPatient.Location = New Point(733, 198)
        txtPatient.Margin = New Padding(5, 6, 5, 6)
        txtPatient.Name = "txtPatient"
        txtPatient.ReadOnly = True
        txtPatient.Size = New Size(312, 31)
        txtPatient.TabIndex = 51
        ' 
        ' Label5
        ' 
        Label5.Location = New Point(420, 163)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(242, 29)
        Label5.TabIndex = 52
        Label5.Text = "Ordering Provider"
        ' 
        ' Label6
        ' 
        Label6.Location = New Point(753, 163)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(87, 29)
        Label6.TabIndex = 53
        Label6.Text = "Patient"
        ' 
        ' txtRTF
        ' 
        txtRTF.Location = New Point(1262, 81)
        txtRTF.Margin = New Padding(5, 6, 5, 6)
        txtRTF.Name = "txtRTF"
        txtRTF.Size = New Size(21, 16)
        txtRTF.TabIndex = 55
        txtRTF.Text = ""
        txtRTF.Visible = False
        ' 
        ' txtComment
        ' 
        txtComment.Location = New Point(1295, 62)
        txtComment.Margin = New Padding(5, 6, 5, 6)
        txtComment.Name = "txtComment"
        txtComment.Size = New Size(21, 31)
        txtComment.TabIndex = 54
        txtComment.Visible = False
        ' 
        ' cmbDirector
        ' 
        cmbDirector.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDirector.FormattingEnabled = True
        cmbDirector.Location = New Point(975, 1119)
        cmbDirector.Margin = New Padding(5, 6, 5, 6)
        cmbDirector.Name = "cmbDirector"
        cmbDirector.Size = New Size(341, 33)
        cmbDirector.TabIndex = 78
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.Location = New Point(892, 1125)
        Label13.Margin = New Padding(5, 0, 5, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(75, 25)
        Label13.TabIndex = 77
        Label13.Text = "Director"
        Label13.TextAlign = ContentAlignment.TopRight
        ' 
        ' Label9
        ' 
        Label9.Location = New Point(20, 1073)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(58, 25)
        Label9.TabIndex = 76
        Label9.Text = "Note"
        Label9.TextAlign = ContentAlignment.TopRight
        ' 
        ' txtNote
        ' 
        txtNote.Location = New Point(88, 1073)
        txtNote.Margin = New Padding(5, 6, 5, 6)
        txtNote.MaxLength = 960
        txtNote.Multiline = True
        txtNote.Name = "txtNote"
        txtNote.ScrollBars = ScrollBars.Vertical
        txtNote.Size = New Size(791, 85)
        txtNote.TabIndex = 75
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(920, 1079)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(107, 25)
        Label8.TabIndex = 74
        Label8.Text = "Report Date"
        Label8.TextAlign = ContentAlignment.TopRight
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(1172, 1079)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(50, 25)
        Label7.TabIndex = 73
        Label7.Text = "Time"
        Label7.TextAlign = ContentAlignment.TopRight
        ' 
        ' txtRptTime
        ' 
        txtRptTime.Location = New Point(1232, 1073)
        txtRptTime.Margin = New Padding(5, 6, 5, 6)
        txtRptTime.Mask = "90:00"
        txtRptTime.Name = "txtRptTime"
        txtRptTime.Size = New Size(84, 31)
        txtRptTime.TabIndex = 72
        txtRptTime.ValidatingType = GetType(Date)
        ' 
        ' txtRptDate
        ' 
        txtRptDate.Location = New Point(1038, 1073)
        txtRptDate.Margin = New Padding(5, 6, 5, 6)
        txtRptDate.Mask = "00/00/0000"
        txtRptDate.Name = "txtRptDate"
        txtRptDate.Size = New Size(121, 31)
        txtRptDate.TabIndex = 71
        txtRptDate.ValidatingType = GetType(Date)
        ' 
        ' dgvExceptions
        ' 
        dgvExceptions.AllowUserToAddRows = False
        dgvExceptions.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.Linen
        dgvExceptions.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvExceptions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvExceptions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvExceptions.Columns.AddRange(New DataGridViewColumn() {Acc})
        dgvExceptions.Location = New Point(1075, 112)
        dgvExceptions.Margin = New Padding(5, 6, 5, 6)
        dgvExceptions.Name = "dgvExceptions"
        dgvExceptions.RowHeadersVisible = False
        dgvExceptions.RowHeadersWidth = 62
        dgvExceptions.Size = New Size(243, 233)
        dgvExceptions.TabIndex = 79
        ' 
        ' Acc
        ' 
        Acc.FillWeight = 128F
        Acc.HeaderText = "AccID"
        Acc.MaxInputLength = 12
        Acc.MinimumWidth = 8
        Acc.Name = "Acc"
        Acc.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' chkRelease
        ' 
        chkRelease.Appearance = Appearance.Button
        chkRelease.Location = New Point(862, 321)
        chkRelease.Margin = New Padding(5, 6, 5, 6)
        chkRelease.Name = "chkRelease"
        chkRelease.Size = New Size(187, 52)
        chkRelease.TabIndex = 80
        chkRelease.Text = "Release Individual"
        chkRelease.TextAlign = ContentAlignment.MiddleCenter
        chkRelease.UseVisualStyleBackColor = True
        ' 
        ' lblExceptions
        ' 
        lblExceptions.Location = New Point(1070, 75)
        lblExceptions.Margin = New Padding(5, 0, 5, 0)
        lblExceptions.Name = "lblExceptions"
        lblExceptions.Size = New Size(135, 29)
        lblExceptions.TabIndex = 81
        lblExceptions.Text = "Exceptions"
        ' 
        ' Label16
        ' 
        Label16.ForeColor = Color.DarkBlue
        Label16.Location = New Point(28, 242)
        Label16.Margin = New Padding(5, 0, 5, 0)
        Label16.Name = "Label16"
        Label16.Size = New Size(110, 25)
        Label16.TabIndex = 83
        Label16.Text = "Medications:"
        Label16.TextAlign = ContentAlignment.TopRight
        ' 
        ' txtMeds
        ' 
        txtMeds.Location = New Point(20, 273)
        txtMeds.Margin = New Padding(5, 6, 5, 6)
        txtMeds.Name = "txtMeds"
        txtMeds.ReadOnly = True
        txtMeds.Size = New Size(1026, 31)
        txtMeds.TabIndex = 82
        ' 
        ' cmbWID
        ' 
        cmbWID.FormattingEnabled = True
        cmbWID.Location = New Point(468, 110)
        cmbWID.Margin = New Padding(5, 6, 5, 6)
        cmbWID.Name = "cmbWID"
        cmbWID.Size = New Size(434, 33)
        cmbWID.Sorted = True
        cmbWID.TabIndex = 84
        ' 
        ' Label1
        ' 
        Label1.Location = New Point(487, 75)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(135, 29)
        Label1.TabIndex = 85
        Label1.Text = "Worksheet"
        ' 
        ' dgvResults
        ' 
        dgvResults.AllowUserToAddRows = False
        dgvResults.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = Color.FromArgb(CByte(192), CByte(255), CByte(255))
        dgvResults.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvResults.Columns.AddRange(New DataGridViewColumn() {Test_ID, Analyte, Result, Reflux, Flag, Normal, History, Note, IResult, TResult, Release, LorN, AccID, Cause, Reflexer, Reflexed, Cmnt, RTFRes, ImgRes, ESig, Behavior})
        dgvResults.Location = New Point(20, 390)
        dgvResults.Margin = New Padding(5, 6, 5, 6)
        dgvResults.Name = "dgvResults"
        dgvResults.RowHeadersVisible = False
        dgvResults.RowHeadersWidth = 62
        DataGridViewCellStyle3.BackColor = Color.Wheat
        dgvResults.RowsDefaultCellStyle = DataGridViewCellStyle3
        dgvResults.Size = New Size(1318, 662)
        dgvResults.TabIndex = 86
        ' 
        ' Test_ID
        ' 
        Test_ID.HeaderText = "Test_ID"
        Test_ID.MinimumWidth = 8
        Test_ID.Name = "Test_ID"
        Test_ID.ReadOnly = True
        Test_ID.SortMode = DataGridViewColumnSortMode.NotSortable
        Test_ID.Visible = False
        ' 
        ' Analyte
        ' 
        Analyte.FillWeight = 156F
        Analyte.HeaderText = "Analyte Name"
        Analyte.MinimumWidth = 8
        Analyte.Name = "Analyte"
        Analyte.ReadOnly = True
        ' 
        ' Result
        ' 
        Result.FillWeight = 200F
        Result.HeaderText = "Result"
        Result.MaxInputLength = 100
        Result.MinimumWidth = 8
        Result.Name = "Result"
        Result.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Reflux
        ' 
        Reflux.FillWeight = 30F
        Reflux.HeaderText = ""
        Reflux.Image = CType(resources.GetObject("Reflux.Image"), Image)
        Reflux.MinimumWidth = 8
        Reflux.Name = "Reflux"
        Reflux.ReadOnly = True
        Reflux.Resizable = DataGridViewTriState.False
        ' 
        ' Flag
        ' 
        Flag.FillWeight = 69F
        Flag.HeaderText = "Flag/Interp"
        Flag.MinimumWidth = 8
        Flag.Name = "Flag"
        Flag.ReadOnly = True
        ' 
        ' Normal
        ' 
        Normal.FillWeight = 95F
        Normal.HeaderText = "Range/Cutoff"
        Normal.MinimumWidth = 8
        Normal.Name = "Normal"
        Normal.ReadOnly = True
        Normal.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' History
        ' 
        History.FillWeight = 50F
        History.HeaderText = "History"
        History.Image = CType(resources.GetObject("History.Image"), Image)
        History.MinimumWidth = 8
        History.Name = "History"
        History.ReadOnly = True
        ' 
        ' Note
        ' 
        Note.FillWeight = 50F
        Note.HeaderText = "Note"
        Note.Image = CType(resources.GetObject("Note.Image"), Image)
        Note.MinimumWidth = 8
        Note.Name = "Note"
        Note.ReadOnly = True
        ' 
        ' IResult
        ' 
        IResult.FillWeight = 40F
        IResult.HeaderText = "Image"
        IResult.Image = CType(resources.GetObject("IResult.Image"), Image)
        IResult.MinimumWidth = 8
        IResult.Name = "IResult"
        IResult.ReadOnly = True
        IResult.Visible = False
        ' 
        ' TResult
        ' 
        TResult.FillWeight = 50F
        TResult.HeaderText = "RTFI"
        TResult.Image = CType(resources.GetObject("TResult.Image"), Image)
        TResult.MinimumWidth = 8
        TResult.Name = "TResult"
        TResult.ReadOnly = True
        ' 
        ' Release
        ' 
        Release.FillWeight = 60F
        Release.HeaderText = "Release"
        Release.MinimumWidth = 8
        Release.Name = "Release"
        ' 
        ' LorN
        ' 
        LorN.HeaderText = "L/N"
        LorN.MinimumWidth = 8
        LorN.Name = "LorN"
        LorN.ReadOnly = True
        LorN.Visible = False
        ' 
        ' AccID
        ' 
        AccID.HeaderText = "AccID"
        AccID.MinimumWidth = 8
        AccID.Name = "AccID"
        AccID.ReadOnly = True
        AccID.Visible = False
        ' 
        ' Cause
        ' 
        Cause.HeaderText = "Cause"
        Cause.MinimumWidth = 8
        Cause.Name = "Cause"
        Cause.Visible = False
        ' 
        ' Reflexer
        ' 
        Reflexer.HeaderText = "Reflexer"
        Reflexer.MinimumWidth = 8
        Reflexer.Name = "Reflexer"
        Reflexer.Visible = False
        ' 
        ' Reflexed
        ' 
        Reflexed.HeaderText = "Reflexed"
        Reflexed.MinimumWidth = 8
        Reflexed.Name = "Reflexed"
        Reflexed.Visible = False
        ' 
        ' Cmnt
        ' 
        Cmnt.HeaderText = "Cmnt"
        Cmnt.MinimumWidth = 8
        Cmnt.Name = "Cmnt"
        Cmnt.ReadOnly = True
        Cmnt.SortMode = DataGridViewColumnSortMode.NotSortable
        Cmnt.Visible = False
        ' 
        ' RTFRes
        ' 
        RTFRes.HeaderText = "RTFRes"
        RTFRes.MinimumWidth = 8
        RTFRes.Name = "RTFRes"
        RTFRes.ReadOnly = True
        RTFRes.SortMode = DataGridViewColumnSortMode.NotSortable
        RTFRes.Visible = False
        ' 
        ' ImgRes
        ' 
        ImgRes.HeaderText = "ImgRes"
        ImgRes.MinimumWidth = 8
        ImgRes.Name = "ImgRes"
        ImgRes.Visible = False
        ' 
        ' ESig
        ' 
        ESig.HeaderText = "ESig"
        ESig.MinimumWidth = 8
        ESig.Name = "ESig"
        ESig.Visible = False
        ' 
        ' Behavior
        ' 
        Behavior.HeaderText = "Behavior"
        Behavior.MinimumWidth = 8
        Behavior.Name = "Behavior"
        Behavior.SortMode = DataGridViewColumnSortMode.NotSortable
        Behavior.Visible = False
        ' 
        ' frmATRResults
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1367, 1192)
        Controls.Add(dgvResults)
        Controls.Add(Label1)
        Controls.Add(cmbWID)
        Controls.Add(Label16)
        Controls.Add(txtMeds)
        Controls.Add(lblExceptions)
        Controls.Add(chkRelease)
        Controls.Add(dgvExceptions)
        Controls.Add(cmbDirector)
        Controls.Add(Label13)
        Controls.Add(Label9)
        Controls.Add(txtNote)
        Controls.Add(Label8)
        Controls.Add(Label7)
        Controls.Add(txtRptTime)
        Controls.Add(txtRptDate)
        Controls.Add(txtRTF)
        Controls.Add(txtComment)
        Controls.Add(Label6)
        Controls.Add(Label5)
        Controls.Add(txtPatient)
        Controls.Add(txtProvider)
        Controls.Add(Label4)
        Controls.Add(txtAccDate)
        Controls.Add(txtAccID)
        Controls.Add(Label3)
        Controls.Add(btnLast)
        Controls.Add(btnNext)
        Controls.Add(btnPrevious)
        Controls.Add(btnFirst)
        Controls.Add(lblPos)
        Controls.Add(btnLoad)
        Controls.Add(dtpTo)
        Controls.Add(dtpFrom)
        Controls.Add(lblTo)
        Controls.Add(lblFrom)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmATRResults"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Results Requiring Attention"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgvExceptions, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvResults, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnAccQC As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnRelease As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnBlock As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents lblPos As System.Windows.Forms.Label
    Friend WithEvents btnFirst As System.Windows.Forms.Button
    Friend WithEvents btnPrevious As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnLast As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtAccID As System.Windows.Forms.TextBox
    Friend WithEvents txtAccDate As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtProvider As System.Windows.Forms.TextBox
    Friend WithEvents txtPatient As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtRTF As System.Windows.Forms.RichTextBox
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents cmbDirector As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtNote As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtRptTime As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtRptDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents dgvExceptions As System.Windows.Forms.DataGridView
    Friend WithEvents chkRelease As System.Windows.Forms.CheckBox
    Friend WithEvents lblExceptions As System.Windows.Forms.Label
    Friend WithEvents Acc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtMeds As System.Windows.Forms.TextBox
    Friend WithEvents cmbWID As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvResults As System.Windows.Forms.DataGridView
    Friend WithEvents Test_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Analyte As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Result As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Reflux As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents Flag As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Normal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents History As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents Note As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents IResult As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents TResult As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents Release As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents LorN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AccID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cause As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Reflexer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Reflexed As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cmnt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RTFRes As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ImgRes As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents ESig As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Behavior As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
