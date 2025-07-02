<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmApplyLabResults
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmApplyLabResults))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        btnProcess = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        lblTo = New Label()
        Label5 = New Label()
        cmbStatus = New ComboBox()
        chkHoldRes = New CheckBox()
        chkApply = New CheckBox()
        btnSelAll = New Button()
        lblFrom = New Label()
        chkClear = New CheckBox()
        dgvResults = New DataGridView()
        Sel = New DataGridViewCheckBoxColumn()
        AccID = New DataGridViewTextBoxColumn()
        TestID = New DataGridViewTextBoxColumn()
        TestName = New DataGridViewTextBoxColumn()
        Result = New DataGridViewTextBoxColumn()
        Flag = New DataGridViewTextBoxColumn()
        Range = New DataGridViewTextBoxColumn()
        Status = New DataGridViewTextBoxColumn()
        Cmnt = New DataGridViewTextBoxColumn()
        TResult = New DataGridViewTextBoxColumn()
        UOM = New DataGridViewTextBoxColumn()
        LabID = New DataGridViewTextBoxColumn()
        btnLoad = New Button()
        cmbLabs = New ComboBox()
        Label1 = New Label()
        StatusStrip1 = New StatusStrip()
        lblStatus = New ToolStripStatusLabel()
        pbProcess = New ToolStripProgressBar()
        txtAccFrom = New TextBox()
        txtAccTo = New TextBox()
        btnDeselAll = New Button()
        Label3 = New Label()
        Label4 = New Label()
        dgvDiscrete = New DataGridView()
        Discretes = New DataGridViewTextBoxColumn()
        dtpDateTo = New DateTimePicker()
        lblClearDates = New Label()
        dtpDateFrom = New DateTimePicker()
        ToolStrip1.SuspendLayout()
        CType(dgvResults, ComponentModel.ISupportInitialize).BeginInit()
        StatusStrip1.SuspendLayout()
        CType(dgvDiscrete, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(20, 20)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnProcess, ToolStripSeparator2, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(1337, 34)
        ToolStrip1.TabIndex = 14
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnProcess
        ' 
        btnProcess.Enabled = False
        btnProcess.ForeColor = Color.DarkBlue
        btnProcess.Image = CType(resources.GetObject("btnProcess.Image"), Image)
        btnProcess.ImageTransparentColor = Color.Magenta
        btnProcess.Name = "btnProcess"
        btnProcess.Size = New Size(96, 29)
        btnProcess.Text = "Process"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 34)
        ' 
        ' btnCancel
        ' 
        btnCancel.ForeColor = Color.DarkBlue
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
        btnHelp.ForeColor = Color.DarkBlue
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.ImageTransparentColor = Color.Magenta
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(73, 29)
        btnHelp.Text = "Help"
        ' 
        ' lblTo
        ' 
        lblTo.ForeColor = Color.DarkBlue
        lblTo.Location = New Point(200, 171)
        lblTo.Margin = New Padding(5, 0, 5, 0)
        lblTo.Name = "lblTo"
        lblTo.Size = New Size(183, 25)
        lblTo.TabIndex = 41
        lblTo.Text = "To"
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.DarkBlue
        Label5.Location = New Point(1158, 77)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(137, 25)
        Label5.TabIndex = 39
        Label5.Text = "Status"
        Label5.TextAlign = ContentAlignment.TopCenter
        ' 
        ' cmbStatus
        ' 
        cmbStatus.FormattingEnabled = True
        cmbStatus.Items.AddRange(New Object() {"ALL", "FINAL", "PRELIMINARY"})
        cmbStatus.Location = New Point(1123, 108)
        cmbStatus.Margin = New Padding(5, 6, 5, 6)
        cmbStatus.Name = "cmbStatus"
        cmbStatus.Size = New Size(201, 33)
        cmbStatus.TabIndex = 7
        ' 
        ' chkHoldRes
        ' 
        chkHoldRes.ForeColor = Color.DarkBlue
        chkHoldRes.Location = New Point(648, 1022)
        chkHoldRes.Margin = New Padding(5, 6, 5, 6)
        chkHoldRes.Name = "chkHoldRes"
        chkHoldRes.Size = New Size(218, 42)
        chkHoldRes.TabIndex = 31
        chkHoldRes.Text = "Hold Results"
        chkHoldRes.UseVisualStyleBackColor = True
        ' 
        ' chkApply
        ' 
        chkApply.Appearance = Appearance.Button
        chkApply.Checked = True
        chkApply.CheckState = CheckState.Checked
        chkApply.ForeColor = Color.DarkBlue
        chkApply.Location = New Point(319, 1016)
        chkApply.Margin = New Padding(5, 6, 5, 6)
        chkApply.Name = "chkApply"
        chkApply.Size = New Size(215, 48)
        chkApply.TabIndex = 30
        chkApply.Text = "Apply Results"
        chkApply.TextAlign = ContentAlignment.MiddleCenter
        chkApply.UseVisualStyleBackColor = True
        ' 
        ' btnSelAll
        ' 
        btnSelAll.ForeColor = Color.DarkBlue
        btnSelAll.Image = CType(resources.GetObject("btnSelAll.Image"), Image)
        btnSelAll.Location = New Point(1086, 1016)
        btnSelAll.Margin = New Padding(5, 6, 5, 6)
        btnSelAll.Name = "btnSelAll"
        btnSelAll.Size = New Size(182, 52)
        btnSelAll.TabIndex = 29
        btnSelAll.Text = "Select All"
        btnSelAll.TextAlign = ContentAlignment.MiddleRight
        btnSelAll.TextImageRelation = TextImageRelation.ImageBeforeText
        btnSelAll.UseVisualStyleBackColor = True
        ' 
        ' lblFrom
        ' 
        lblFrom.ForeColor = Color.DarkBlue
        lblFrom.Location = New Point(15, 171)
        lblFrom.Margin = New Padding(5, 0, 5, 0)
        lblFrom.Name = "lblFrom"
        lblFrom.Size = New Size(210, 25)
        lblFrom.TabIndex = 28
        lblFrom.Text = "From"
        ' 
        ' chkClear
        ' 
        chkClear.ForeColor = Color.DarkBlue
        chkClear.Location = New Point(24, 1022)
        chkClear.Margin = New Padding(5, 6, 5, 6)
        chkClear.Name = "chkClear"
        chkClear.Size = New Size(152, 42)
        chkClear.TabIndex = 27
        chkClear.Text = "Clear Buffer"
        chkClear.UseVisualStyleBackColor = True
        ' 
        ' dgvResults
        ' 
        dgvResults.AllowUserToAddRows = False
        dgvResults.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.MintCream
        dgvResults.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvResults.Columns.AddRange(New DataGridViewColumn() {Sel, AccID, TestID, TestName, Result, Flag, Range, Status, Cmnt, TResult, UOM, LabID})
        dgvResults.Location = New Point(20, 298)
        dgvResults.Margin = New Padding(5, 6, 5, 6)
        dgvResults.Name = "dgvResults"
        dgvResults.RowHeadersVisible = False
        dgvResults.RowHeadersWidth = 62
        dgvResults.Size = New Size(1307, 698)
        dgvResults.TabIndex = 26
        ' 
        ' Sel
        ' 
        Sel.FillWeight = 30F
        Sel.HeaderText = "Sel"
        Sel.MinimumWidth = 8
        Sel.Name = "Sel"
        ' 
        ' AccID
        ' 
        AccID.FillWeight = 70F
        AccID.HeaderText = "Acc ID"
        AccID.MaxInputLength = 12
        AccID.MinimumWidth = 8
        AccID.Name = "AccID"
        AccID.ReadOnly = True
        AccID.Resizable = DataGridViewTriState.True
        ' 
        ' TestID
        ' 
        TestID.FillWeight = 50F
        TestID.HeaderText = "Test ID"
        TestID.MaxInputLength = 8
        TestID.MinimumWidth = 8
        TestID.Name = "TestID"
        TestID.ReadOnly = True
        TestID.Resizable = DataGridViewTriState.True
        TestID.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' TestName
        ' 
        TestName.FillWeight = 105F
        TestName.HeaderText = "Test Name"
        TestName.MaxInputLength = 60
        TestName.MinimumWidth = 8
        TestName.Name = "TestName"
        TestName.ReadOnly = True
        TestName.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Result
        ' 
        Result.FillWeight = 80F
        Result.HeaderText = "Result"
        Result.MaxInputLength = 100
        Result.MinimumWidth = 8
        Result.Name = "Result"
        Result.ReadOnly = True
        Result.Resizable = DataGridViewTriState.True
        Result.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Flag
        ' 
        Flag.FillWeight = 30F
        Flag.HeaderText = "Flag"
        Flag.MinimumWidth = 8
        Flag.Name = "Flag"
        Flag.ReadOnly = True
        ' 
        ' Range
        ' 
        Range.FillWeight = 60F
        Range.HeaderText = "Range"
        Range.MinimumWidth = 8
        Range.Name = "Range"
        Range.ReadOnly = True
        ' 
        ' Status
        ' 
        Status.FillWeight = 30F
        Status.HeaderText = "Status"
        Status.MinimumWidth = 8
        Status.Name = "Status"
        Status.ReadOnly = True
        ' 
        ' Cmnt
        ' 
        Cmnt.FillWeight = 110F
        Cmnt.HeaderText = "Comment"
        Cmnt.MinimumWidth = 8
        Cmnt.Name = "Cmnt"
        Cmnt.ReadOnly = True
        Cmnt.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' TResult
        ' 
        TResult.FillWeight = 180F
        TResult.HeaderText = "RTF Result"
        TResult.MinimumWidth = 8
        TResult.Name = "TResult"
        TResult.ReadOnly = True
        ' 
        ' UOM
        ' 
        UOM.HeaderText = "UOM"
        UOM.MinimumWidth = 8
        UOM.Name = "UOM"
        UOM.Visible = False
        ' 
        ' LabID
        ' 
        LabID.FillWeight = 40F
        LabID.HeaderText = "Lab ID"
        LabID.MinimumWidth = 8
        LabID.Name = "LabID"
        LabID.SortMode = DataGridViewColumnSortMode.NotSortable
        LabID.Visible = False
        ' 
        ' btnLoad
        ' 
        btnLoad.Location = New Point(1123, 190)
        btnLoad.Margin = New Padding(5, 6, 5, 6)
        btnLoad.Name = "btnLoad"
        btnLoad.Size = New Size(203, 71)
        btnLoad.TabIndex = 8
        btnLoad.Text = "Load"
        btnLoad.UseVisualStyleBackColor = True
        ' 
        ' cmbLabs
        ' 
        cmbLabs.DropDownStyle = ComboBoxStyle.DropDownList
        cmbLabs.FormattingEnabled = True
        cmbLabs.Location = New Point(20, 108)
        cmbLabs.Margin = New Padding(5, 6, 5, 6)
        cmbLabs.Name = "cmbLabs"
        cmbLabs.Size = New Size(782, 33)
        cmbLabs.TabIndex = 1
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(20, 77)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(232, 25)
        Label1.TabIndex = 22
        Label1.Text = "Reference Lab"
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.ImageScalingSize = New Size(20, 20)
        StatusStrip1.Items.AddRange(New ToolStripItem() {lblStatus, pbProcess})
        StatusStrip1.Location = New Point(0, 1070)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Padding = New Padding(2, 0, 23, 0)
        StatusStrip1.Size = New Size(1337, 43)
        StatusStrip1.TabIndex = 42
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = False
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(100, 36)
        ' 
        ' pbProcess
        ' 
        pbProcess.AutoSize = False
        pbProcess.Name = "pbProcess"
        pbProcess.Size = New Size(1093, 35)
        ' 
        ' txtAccFrom
        ' 
        txtAccFrom.Location = New Point(458, 204)
        txtAccFrom.Margin = New Padding(5, 6, 5, 6)
        txtAccFrom.Name = "txtAccFrom"
        txtAccFrom.Size = New Size(156, 31)
        txtAccFrom.TabIndex = 4
        txtAccFrom.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtAccTo
        ' 
        txtAccTo.Location = New Point(647, 204)
        txtAccTo.Margin = New Padding(5, 6, 5, 6)
        txtAccTo.Name = "txtAccTo"
        txtAccTo.Size = New Size(156, 31)
        txtAccTo.TabIndex = 5
        txtAccTo.TextAlign = HorizontalAlignment.Center
        ' 
        ' btnDeselAll
        ' 
        btnDeselAll.ForeColor = Color.DarkBlue
        btnDeselAll.Image = CType(resources.GetObject("btnDeselAll.Image"), Image)
        btnDeselAll.Location = New Point(894, 1016)
        btnDeselAll.Margin = New Padding(5, 6, 5, 6)
        btnDeselAll.Name = "btnDeselAll"
        btnDeselAll.Size = New Size(182, 52)
        btnDeselAll.TabIndex = 45
        btnDeselAll.Text = "Deselect All"
        btnDeselAll.TextAlign = ContentAlignment.MiddleRight
        btnDeselAll.TextImageRelation = TextImageRelation.ImageBeforeText
        btnDeselAll.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(458, 169)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(143, 25)
        Label3.TabIndex = 46
        Label3.Text = "Accession From"
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(642, 171)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(138, 25)
        Label4.TabIndex = 47
        Label4.Text = "Accession To"
        ' 
        ' dgvDiscrete
        ' 
        dgvDiscrete.AllowUserToAddRows = False
        dgvDiscrete.AllowUserToDeleteRows = False
        dgvDiscrete.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvDiscrete.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvDiscrete.Columns.AddRange(New DataGridViewColumn() {Discretes})
        dgvDiscrete.Location = New Point(847, 54)
        dgvDiscrete.Margin = New Padding(5, 6, 5, 6)
        dgvDiscrete.Name = "dgvDiscrete"
        dgvDiscrete.RowHeadersVisible = False
        dgvDiscrete.RowHeadersWidth = 62
        dgvDiscrete.Size = New Size(243, 208)
        dgvDiscrete.TabIndex = 6
        ' 
        ' Discretes
        ' 
        Discretes.FillWeight = 120F
        Discretes.HeaderText = "Accession ID"
        Discretes.MaxInputLength = 15
        Discretes.MinimumWidth = 8
        Discretes.Name = "Discretes"
        Discretes.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' dtpDateTo
        ' 
        dtpDateTo.CustomFormat = " "
        dtpDateTo.Format = DateTimePickerFormat.Custom
        dtpDateTo.Location = New Point(205, 204)
        dtpDateTo.Margin = New Padding(3, 4, 3, 4)
        dtpDateTo.Name = "dtpDateTo"
        dtpDateTo.Size = New Size(156, 31)
        dtpDateTo.TabIndex = 94
        ' 
        ' lblClearDates
        ' 
        lblClearDates.BackColor = SystemColors.Control
        lblClearDates.Image = My.Resources.Resources.Eraser
        lblClearDates.Location = New Point(385, 198)
        lblClearDates.Name = "lblClearDates"
        lblClearDates.Size = New Size(33, 46)
        lblClearDates.TabIndex = 96
        lblClearDates.Text = "      "
        ' 
        ' dtpDateFrom
        ' 
        dtpDateFrom.CustomFormat = " "
        dtpDateFrom.Format = DateTimePickerFormat.Custom
        dtpDateFrom.Location = New Point(20, 204)
        dtpDateFrom.Margin = New Padding(3, 4, 3, 4)
        dtpDateFrom.Name = "dtpDateFrom"
        dtpDateFrom.Size = New Size(156, 31)
        dtpDateFrom.TabIndex = 95
        ' 
        ' frmApplyLabResults
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1337, 1113)
        Controls.Add(dtpDateTo)
        Controls.Add(lblClearDates)
        Controls.Add(dtpDateFrom)
        Controls.Add(dgvDiscrete)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(btnDeselAll)
        Controls.Add(txtAccTo)
        Controls.Add(txtAccFrom)
        Controls.Add(StatusStrip1)
        Controls.Add(lblTo)
        Controls.Add(Label5)
        Controls.Add(cmbStatus)
        Controls.Add(chkHoldRes)
        Controls.Add(chkApply)
        Controls.Add(btnSelAll)
        Controls.Add(lblFrom)
        Controls.Add(chkClear)
        Controls.Add(dgvResults)
        Controls.Add(btnLoad)
        Controls.Add(cmbLabs)
        Controls.Add(Label1)
        Controls.Add(ToolStrip1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MaximumSize = New Size(1359, 1169)
        MinimumSize = New Size(1359, 1169)
        Name = "frmApplyLabResults"
        Text = "Apply Ref Lab Results"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgvResults, ComponentModel.ISupportInitialize).EndInit()
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        CType(dgvDiscrete, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnProcess As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents chkHoldRes As System.Windows.Forms.CheckBox
    Friend WithEvents chkApply As System.Windows.Forms.CheckBox
    Friend WithEvents btnSelAll As System.Windows.Forms.Button
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents chkClear As System.Windows.Forms.CheckBox
    Friend WithEvents dgvResults As System.Windows.Forms.DataGridView
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents cmbLabs As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pbProcess As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents txtAccFrom As System.Windows.Forms.TextBox
    Friend WithEvents txtAccTo As System.Windows.Forms.TextBox
    Friend WithEvents btnDeselAll As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dgvDiscrete As System.Windows.Forms.DataGridView
    Friend WithEvents Discretes As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Sel As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents AccID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TestID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TestName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Result As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Flag As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Range As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cmnt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TResult As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UOM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LabID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dtpDateTo As DateTimePicker
    Friend WithEvents lblClearDates As Label
    Friend WithEvents dtpDateFrom As DateTimePicker
End Class
