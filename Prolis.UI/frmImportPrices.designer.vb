<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImportPrices
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImportPrices))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnProcess = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnCancel = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.btnHelp = New System.Windows.Forms.ToolStripButton
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.PB = New System.Windows.Forms.ToolStripProgressBar
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbDelimiter = New System.Windows.Forms.ComboBox
        Me.dgvFieldMap = New System.Windows.Forms.DataGridView
        Me.btnSel = New System.Windows.Forms.Button
        Me.btnDesel = New System.Windows.Forms.Button
        Me.btnLoad = New System.Windows.Forms.Button
        Me.btnOpen = New System.Windows.Forms.Button
        Me.txtFile = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.BW = New System.ComponentModel.BackgroundWorker
        Me.Include = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.FileField = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Map = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.ToolStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.dgvFieldMap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnProcess, Me.ToolStripSeparator1, Me.btnCancel, Me.ToolStripSeparator4, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(482, 25)
        Me.ToolStrip1.TabIndex = 8
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnProcess
        '
        Me.btnProcess.Enabled = False
        Me.btnProcess.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnProcess.Image = CType(resources.GetObject("btnProcess.Image"), System.Drawing.Image)
        Me.btnProcess.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(67, 22)
        Me.btnProcess.Text = "Process"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnCancel
        '
        Me.btnCancel.AutoSize = False
        Me.btnCancel.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(65, 22)
        Me.btnCancel.Text = "Cancel"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'btnHelp
        '
        Me.btnHelp.AutoSize = False
        Me.btnHelp.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(65, 22)
        Me.btnHelp.Text = "Help"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus, Me.PB})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 342)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(482, 22)
        Me.StatusStrip1.TabIndex = 31
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = False
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(90, 17)
        '
        'PB
        '
        Me.PB.AutoSize = False
        Me.PB.Name = "PB"
        Me.PB.Size = New System.Drawing.Size(330, 16)
        Me.PB.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(55, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 14)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "File Type"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbDelimiter
        '
        Me.cmbDelimiter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDelimiter.FormattingEnabled = True
        Me.cmbDelimiter.Items.AddRange(New Object() {"COMA SEPARATED VALUES (*.CSV)", "PIPE SEPARATED VALUES (*.*)", "TAB SEPARATED VALUES (*.*)"})
        Me.cmbDelimiter.Location = New System.Drawing.Point(115, 38)
        Me.cmbDelimiter.Name = "cmbDelimiter"
        Me.cmbDelimiter.Size = New System.Drawing.Size(245, 21)
        Me.cmbDelimiter.TabIndex = 29
        '
        'dgvFieldMap
        '
        Me.dgvFieldMap.AllowUserToAddRows = False
        Me.dgvFieldMap.AllowUserToDeleteRows = False
        Me.dgvFieldMap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFieldMap.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Include, Me.FileField, Me.Map})
        Me.dgvFieldMap.Location = New System.Drawing.Point(15, 121)
        Me.dgvFieldMap.Name = "dgvFieldMap"
        Me.dgvFieldMap.RowHeadersVisible = False
        Me.dgvFieldMap.Size = New System.Drawing.Size(416, 204)
        Me.dgvFieldMap.TabIndex = 28
        '
        'btnSel
        '
        Me.btnSel.Image = CType(resources.GetObject("btnSel.Image"), System.Drawing.Image)
        Me.btnSel.Location = New System.Drawing.Point(437, 299)
        Me.btnSel.Name = "btnSel"
        Me.btnSel.Size = New System.Drawing.Size(32, 26)
        Me.btnSel.TabIndex = 27
        Me.btnSel.UseVisualStyleBackColor = True
        '
        'btnDesel
        '
        Me.btnDesel.Image = CType(resources.GetObject("btnDesel.Image"), System.Drawing.Image)
        Me.btnDesel.Location = New System.Drawing.Point(437, 267)
        Me.btnDesel.Name = "btnDesel"
        Me.btnDesel.Size = New System.Drawing.Size(32, 26)
        Me.btnDesel.TabIndex = 26
        Me.btnDesel.UseVisualStyleBackColor = True
        '
        'btnLoad
        '
        Me.btnLoad.Image = CType(resources.GetObject("btnLoad.Image"), System.Drawing.Image)
        Me.btnLoad.Location = New System.Drawing.Point(437, 121)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(32, 26)
        Me.btnLoad.TabIndex = 25
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'btnOpen
        '
        Me.btnOpen.Image = CType(resources.GetObject("btnOpen.Image"), System.Drawing.Image)
        Me.btnOpen.Location = New System.Drawing.Point(437, 83)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(32, 26)
        Me.btnOpen.TabIndex = 24
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'txtFile
        '
        Me.txtFile.Location = New System.Drawing.Point(15, 87)
        Me.txtFile.Name = "txtFile"
        Me.txtFile.ReadOnly = True
        Me.txtFile.Size = New System.Drawing.Size(416, 20)
        Me.txtFile.TabIndex = 23
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(12, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 16)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Source File"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'BW
        '
        Me.BW.WorkerReportsProgress = True
        Me.BW.WorkerSupportsCancellation = True
        '
        'Include
        '
        Me.Include.FillWeight = 30.0!
        Me.Include.HeaderText = ""
        Me.Include.Name = "Include"
        Me.Include.Width = 30
        '
        'FileField
        '
        Me.FileField.FillWeight = 150.0!
        Me.FileField.HeaderText = "File Field"
        Me.FileField.Name = "FileField"
        Me.FileField.ReadOnly = True
        Me.FileField.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.FileField.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.FileField.Width = 150
        '
        'Map
        '
        Me.Map.FillWeight = 210.0!
        Me.Map.HeaderText = "Mapping"
        Me.Map.Items.AddRange(New Object() {"CPT Code", "List Price", "Level 1"})
        Me.Map.Name = "Map"
        Me.Map.Width = 210
        '
        'frmImportPrices
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(482, 364)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbDelimiter)
        Me.Controls.Add(Me.dgvFieldMap)
        Me.Controls.Add(Me.btnSel)
        Me.Controls.Add(Me.btnDesel)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.txtFile)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmImportPrices"
        Me.Text = "Import Prices"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.dgvFieldMap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnProcess As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PB As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbDelimiter As System.Windows.Forms.ComboBox
    Friend WithEvents dgvFieldMap As System.Windows.Forms.DataGridView
    Friend WithEvents btnSel As System.Windows.Forms.Button
    Friend WithEvents btnDesel As System.Windows.Forms.Button
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents btnOpen As System.Windows.Forms.Button
    Friend WithEvents txtFile As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents BW As System.ComponentModel.BackgroundWorker
    Friend WithEvents Include As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents FileField As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Map As System.Windows.Forms.DataGridViewComboBoxColumn
End Class
