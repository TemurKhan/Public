<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBatchResults
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBatchResults))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        btnSave = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnRelease = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnBlock = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        Label1 = New Label()
        cmbTests = New ComboBox()
        dgvResults = New DataGridView()
        AccID = New DataGridViewTextBoxColumn()
        Patient = New DataGridViewTextBoxColumn()
        Result = New DataGridViewTextBoxColumn()
        Release = New DataGridViewCheckBoxColumn()
        btnLoad = New Button()
        txtAccFrom = New TextBox()
        txtAccTo = New TextBox()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        Label6 = New Label()
        Label7 = New Label()
        btnClear = New Button()
        cmbDefault = New ComboBox()
        dtpDateTo = New DateTimePicker()
        lblClearDates = New Label()
        dtpDateFrom = New DateTimePicker()
        ToolStrip1.SuspendLayout()
        CType(dgvResults, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnSave, ToolStripSeparator1, btnRelease, ToolStripSeparator2, btnBlock, ToolStripSeparator3, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(1050, 34)
        ToolStrip1.TabIndex = 4
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnSave
        ' 
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
        btnRelease.Size = New Size(123, 29)
        btnRelease.Text = "Release All"
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
        btnBlock.Size = New Size(104, 29)
        btnBlock.Text = "Hold All"
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
        ' Label1
        ' 
        Label1.Location = New Point(27, 75)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(142, 27)
        Label1.TabIndex = 5
        Label1.Text = "Date From"
        ' 
        ' cmbTests
        ' 
        cmbTests.DropDownStyle = ComboBoxStyle.DropDownList
        cmbTests.FormattingEnabled = True
        cmbTests.Location = New Point(27, 196)
        cmbTests.Margin = New Padding(5, 6, 5, 6)
        cmbTests.Name = "cmbTests"
        cmbTests.Size = New Size(547, 33)
        cmbTests.TabIndex = 5
        ' 
        ' dgvResults
        ' 
        dgvResults.AllowUserToAddRows = False
        dgvResults.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.AliceBlue
        dgvResults.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvResults.Columns.AddRange(New DataGridViewColumn() {AccID, Patient, Result, Release})
        dgvResults.Location = New Point(27, 277)
        dgvResults.Margin = New Padding(5, 6, 5, 6)
        dgvResults.Name = "dgvResults"
        dgvResults.RowHeadersVisible = False
        dgvResults.RowHeadersWidth = 62
        DataGridViewCellStyle2.BackColor = Color.FloralWhite
        dgvResults.RowsDefaultCellStyle = DataGridViewCellStyle2
        dgvResults.Size = New Size(985, 606)
        dgvResults.TabIndex = 9
        ' 
        ' AccID
        ' 
        AccID.HeaderText = "Acc ID"
        AccID.MaxInputLength = 12
        AccID.MinimumWidth = 8
        AccID.Name = "AccID"
        AccID.ReadOnly = True
        ' 
        ' Patient
        ' 
        Patient.FillWeight = 160F
        Patient.HeaderText = "Patient"
        Patient.MaxInputLength = 65
        Patient.MinimumWidth = 8
        Patient.Name = "Patient"
        Patient.ReadOnly = True
        ' 
        ' Result
        ' 
        Result.FillWeight = 240F
        Result.HeaderText = "Result"
        Result.MaxInputLength = 100
        Result.MinimumWidth = 8
        Result.Name = "Result"
        Result.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Release
        ' 
        Release.FillWeight = 70F
        Release.HeaderText = "Release"
        Release.MinimumWidth = 8
        Release.Name = "Release"
        Release.Resizable = DataGridViewTriState.True
        ' 
        ' btnLoad
        ' 
        btnLoad.Image = CType(resources.GetObject("btnLoad.Image"), Image)
        btnLoad.Location = New Point(892, 187)
        btnLoad.Margin = New Padding(5, 6, 5, 6)
        btnLoad.Name = "btnLoad"
        btnLoad.Size = New Size(55, 56)
        btnLoad.TabIndex = 7
        btnLoad.UseVisualStyleBackColor = True
        ' 
        ' txtAccFrom
        ' 
        txtAccFrom.Location = New Point(655, 108)
        txtAccFrom.Margin = New Padding(5, 6, 5, 6)
        txtAccFrom.MaxLength = 12
        txtAccFrom.Name = "txtAccFrom"
        txtAccFrom.Size = New Size(164, 31)
        txtAccFrom.TabIndex = 3
        txtAccFrom.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtAccTo
        ' 
        txtAccTo.Location = New Point(845, 108)
        txtAccTo.Margin = New Padding(5, 6, 5, 6)
        txtAccTo.MaxLength = 12
        txtAccTo.Name = "txtAccTo"
        txtAccTo.Size = New Size(164, 31)
        txtAccTo.TabIndex = 4
        txtAccTo.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label2
        ' 
        Label2.Location = New Point(203, 75)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(137, 27)
        Label2.TabIndex = 14
        Label2.Text = "Date To"
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(655, 75)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(137, 27)
        Label3.TabIndex = 15
        Label3.Text = "Accession From"
        ' 
        ' Label4
        ' 
        Label4.Location = New Point(845, 75)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(137, 27)
        Label4.TabIndex = 16
        Label4.Text = "Accession To"
        ' 
        ' Label5
        ' 
        Label5.Location = New Point(462, 113)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(102, 27)
        Label5.TabIndex = 17
        Label5.Text = "OR"
        Label5.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label6
        ' 
        Label6.Location = New Point(32, 163)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(208, 27)
        Label6.TabIndex = 18
        Label6.Text = "Test Name"
        ' 
        ' Label7
        ' 
        Label7.Location = New Point(600, 163)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(162, 27)
        Label7.TabIndex = 19
        Label7.Text = "Default Result"
        ' 
        ' btnClear
        ' 
        btnClear.Image = CType(resources.GetObject("btnClear.Image"), Image)
        btnClear.Location = New Point(957, 187)
        btnClear.Margin = New Padding(5, 6, 5, 6)
        btnClear.Name = "btnClear"
        btnClear.Size = New Size(55, 56)
        btnClear.TabIndex = 8
        btnClear.UseVisualStyleBackColor = True
        ' 
        ' cmbDefault
        ' 
        cmbDefault.FormattingEnabled = True
        cmbDefault.Items.AddRange(New Object() {"NEGATIVE", "POSITIVE", "NON REACTIVE", "REACTIVE", "RESISTANT", "SENSITIVE", "NORMAL", "ABNORMAL"})
        cmbDefault.Location = New Point(605, 196)
        cmbDefault.Margin = New Padding(5, 6, 5, 6)
        cmbDefault.Name = "cmbDefault"
        cmbDefault.Size = New Size(266, 33)
        cmbDefault.TabIndex = 6
        ' 
        ' dtpDateTo
        ' 
        dtpDateTo.CustomFormat = " "
        dtpDateTo.Format = DateTimePickerFormat.Custom
        dtpDateTo.Location = New Point(208, 108)
        dtpDateTo.Margin = New Padding(3, 4, 3, 4)
        dtpDateTo.Name = "dtpDateTo"
        dtpDateTo.Size = New Size(129, 31)
        dtpDateTo.TabIndex = 94
        ' 
        ' lblClearDates
        ' 
        lblClearDates.BackColor = SystemColors.Control
        lblClearDates.Image = My.Resources.Resources.Eraser
        lblClearDates.Location = New Point(368, 104)
        lblClearDates.Name = "lblClearDates"
        lblClearDates.Size = New Size(33, 46)
        lblClearDates.TabIndex = 96
        lblClearDates.Text = "      "
        ' 
        ' dtpDateFrom
        ' 
        dtpDateFrom.CustomFormat = " "
        dtpDateFrom.Format = DateTimePickerFormat.Custom
        dtpDateFrom.Location = New Point(32, 108)
        dtpDateFrom.Margin = New Padding(3, 4, 3, 4)
        dtpDateFrom.Name = "dtpDateFrom"
        dtpDateFrom.Size = New Size(129, 31)
        dtpDateFrom.TabIndex = 95
        ' 
        ' frmBatchResults
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1050, 925)
        Controls.Add(dtpDateTo)
        Controls.Add(lblClearDates)
        Controls.Add(dtpDateFrom)
        Controls.Add(cmbDefault)
        Controls.Add(btnClear)
        Controls.Add(Label7)
        Controls.Add(Label6)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(txtAccTo)
        Controls.Add(txtAccFrom)
        Controls.Add(btnLoad)
        Controls.Add(dgvResults)
        Controls.Add(cmbTests)
        Controls.Add(Label1)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmBatchResults"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Result Entry by Batch"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgvResults, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnRelease As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnBlock As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbTests As System.Windows.Forms.ComboBox
    Friend WithEvents dgvResults As System.Windows.Forms.DataGridView
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents txtAccFrom As System.Windows.Forms.TextBox
    Friend WithEvents txtAccTo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents cmbDefault As System.Windows.Forms.ComboBox
    Friend WithEvents AccID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Patient As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Result As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Release As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents dtpDateTo As DateTimePicker
    Friend WithEvents lblClearDates As Label
    Friend WithEvents dtpDateFrom As DateTimePicker
End Class
