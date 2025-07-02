<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImportNec
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImportNec))
        ToolStrip1 = New ToolStrip()
        btnProcess = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        Label1 = New Label()
        txtFile = New TextBox()
        btnOpen = New Button()
        btnLoad = New Button()
        btnDesel = New Button()
        btnSel = New Button()
        OpenFileDialog1 = New OpenFileDialog()
        dgvFieldMap = New DataGridView()
        Include = New DataGridViewCheckBoxColumn()
        FileField = New DataGridViewTextBoxColumn()
        Map = New DataGridViewComboBoxColumn()
        cmbDelimiter = New ComboBox()
        Label3 = New Label()
        BW = New ComponentModel.BackgroundWorker()
        StatusStrip1 = New StatusStrip()
        lblStatus = New ToolStripStatusLabel()
        PB = New ToolStripProgressBar()
        ToolStrip1.SuspendLayout()
        CType(dgvFieldMap, ComponentModel.ISupportInitialize).BeginInit()
        StatusStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnProcess, ToolStripSeparator1, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(815, 34)
        ToolStrip1.TabIndex = 7
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnProcess
        ' 
        btnProcess.Enabled = False
        btnProcess.ForeColor = Color.DarkBlue
        btnProcess.Image = CType(resources.GetObject("btnProcess.Image"), Image)
        btnProcess.ImageTransparentColor = Color.Magenta
        btnProcess.Name = "btnProcess"
        btnProcess.Size = New Size(100, 29)
        btnProcess.Text = "Process"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
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
        Label1.Location = New Point(20, 133)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(168, 31)
        Label1.TabIndex = 9
        Label1.Text = "Source File"
        ' 
        ' txtFile
        ' 
        txtFile.Location = New Point(25, 169)
        txtFile.Margin = New Padding(5, 6, 5, 6)
        txtFile.Name = "txtFile"
        txtFile.ReadOnly = True
        txtFile.Size = New Size(676, 31)
        txtFile.TabIndex = 10
        ' 
        ' btnOpen
        ' 
        btnOpen.Image = CType(resources.GetObject("btnOpen.Image"), Image)
        btnOpen.Location = New Point(717, 162)
        btnOpen.Margin = New Padding(5, 6, 5, 6)
        btnOpen.Name = "btnOpen"
        btnOpen.Size = New Size(53, 50)
        btnOpen.TabIndex = 11
        btnOpen.UseVisualStyleBackColor = True
        ' 
        ' btnLoad
        ' 
        btnLoad.Image = CType(resources.GetObject("btnLoad.Image"), Image)
        btnLoad.Location = New Point(717, 235)
        btnLoad.Margin = New Padding(5, 6, 5, 6)
        btnLoad.Name = "btnLoad"
        btnLoad.Size = New Size(53, 50)
        btnLoad.TabIndex = 13
        btnLoad.UseVisualStyleBackColor = True
        ' 
        ' btnDesel
        ' 
        btnDesel.Image = CType(resources.GetObject("btnDesel.Image"), Image)
        btnDesel.Location = New Point(717, 488)
        btnDesel.Margin = New Padding(5, 6, 5, 6)
        btnDesel.Name = "btnDesel"
        btnDesel.Size = New Size(53, 50)
        btnDesel.TabIndex = 14
        btnDesel.UseVisualStyleBackColor = True
        ' 
        ' btnSel
        ' 
        btnSel.Image = CType(resources.GetObject("btnSel.Image"), Image)
        btnSel.Location = New Point(717, 550)
        btnSel.Margin = New Padding(5, 6, 5, 6)
        btnSel.Name = "btnSel"
        btnSel.Size = New Size(53, 50)
        btnSel.TabIndex = 15
        btnSel.UseVisualStyleBackColor = True
        ' 
        ' OpenFileDialog1
        ' 
        OpenFileDialog1.FileName = "OpenFileDialog1"
        ' 
        ' dgvFieldMap
        ' 
        dgvFieldMap.AllowUserToAddRows = False
        dgvFieldMap.AllowUserToDeleteRows = False
        dgvFieldMap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvFieldMap.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvFieldMap.Columns.AddRange(New DataGridViewColumn() {Include, FileField, Map})
        dgvFieldMap.Location = New Point(25, 235)
        dgvFieldMap.Margin = New Padding(5, 6, 5, 6)
        dgvFieldMap.Name = "dgvFieldMap"
        dgvFieldMap.RowHeadersVisible = False
        dgvFieldMap.RowHeadersWidth = 62
        dgvFieldMap.Size = New Size(678, 365)
        dgvFieldMap.TabIndex = 18
        ' 
        ' Include
        ' 
        Include.FillWeight = 30F
        Include.HeaderText = ""
        Include.MinimumWidth = 8
        Include.Name = "Include"
        ' 
        ' FileField
        ' 
        FileField.FillWeight = 150F
        FileField.HeaderText = "File Field"
        FileField.MinimumWidth = 8
        FileField.Name = "FileField"
        FileField.ReadOnly = True
        FileField.Resizable = DataGridViewTriState.True
        FileField.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Map
        ' 
        Map.FillWeight = 210F
        Map.HeaderText = "Mapping"
        Map.Items.AddRange(New Object() {"CPT_Code", "Dx_Code"})
        Map.MinimumWidth = 8
        Map.Name = "Map"
        ' 
        ' cmbDelimiter
        ' 
        cmbDelimiter.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDelimiter.FormattingEnabled = True
        cmbDelimiter.Items.AddRange(New Object() {"COMA SEPARATED VALUES (*.CSV)", "PIPE SEPARATED VALUES (*.*)", "TAB SEPARATED VALUES (*.*)"})
        cmbDelimiter.Location = New Point(192, 75)
        cmbDelimiter.Margin = New Padding(5, 6, 5, 6)
        cmbDelimiter.Name = "cmbDelimiter"
        cmbDelimiter.Size = New Size(406, 33)
        cmbDelimiter.TabIndex = 19
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(92, 81)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(90, 27)
        Label3.TabIndex = 20
        Label3.Text = "File Type"
        Label3.TextAlign = ContentAlignment.TopRight
        ' 
        ' BW
        ' 
        BW.WorkerReportsProgress = True
        BW.WorkerSupportsCancellation = True
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.ImageScalingSize = New Size(24, 24)
        StatusStrip1.Items.AddRange(New ToolStripItem() {lblStatus, PB})
        StatusStrip1.Location = New Point(0, 611)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Padding = New Padding(2, 0, 23, 0)
        StatusStrip1.Size = New Size(815, 35)
        StatusStrip1.TabIndex = 21
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = False
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(90, 28)
        ' 
        ' PB
        ' 
        PB.AutoSize = False
        PB.Name = "PB"
        PB.Size = New Size(550, 27)
        PB.Style = ProgressBarStyle.Continuous
        ' 
        ' frmImportNec
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(815, 646)
        Controls.Add(StatusStrip1)
        Controls.Add(Label3)
        Controls.Add(cmbDelimiter)
        Controls.Add(dgvFieldMap)
        Controls.Add(btnSel)
        Controls.Add(btnDesel)
        Controls.Add(btnLoad)
        Controls.Add(btnOpen)
        Controls.Add(txtFile)
        Controls.Add(Label1)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        Name = "frmImportNec"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Import Necessity"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgvFieldMap, ComponentModel.ISupportInitialize).EndInit()
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnProcess As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFile As System.Windows.Forms.TextBox
    Friend WithEvents btnOpen As System.Windows.Forms.Button
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents btnDesel As System.Windows.Forms.Button
    Friend WithEvents btnSel As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents dgvFieldMap As System.Windows.Forms.DataGridView
    Friend WithEvents cmbDelimiter As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents BW As System.ComponentModel.BackgroundWorker
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PB As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents Include As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents FileField As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Map As System.Windows.Forms.DataGridViewComboBoxColumn

End Class
