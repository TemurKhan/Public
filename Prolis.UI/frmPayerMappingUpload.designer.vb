<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPayerMappingUpload
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPayerMappingUpload))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnProcess = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnCancel = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.btnHelp = New System.Windows.Forms.ToolStripButton
        Me.cmbDelim = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.dgvFieldMap = New System.Windows.Forms.DataGridView
        Me.pb = New System.Windows.Forms.ProgressBar
        Me.btnSel = New System.Windows.Forms.Button
        Me.btnDesel = New System.Windows.Forms.Button
        Me.btnLoad = New System.Windows.Forms.Button
        Me.btnOpen = New System.Windows.Forms.Button
        Me.txtFile = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Include = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.FileField = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Map = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.ToolStrip1.SuspendLayout()
        CType(Me.dgvFieldMap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnProcess, Me.ToolStripSeparator1, Me.btnCancel, Me.ToolStripSeparator4, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(475, 25)
        Me.ToolStrip1.TabIndex = 9
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnProcess
        '
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
        'cmbDelim
        '
        Me.cmbDelim.FormattingEnabled = True
        Me.cmbDelim.Items.AddRange(New Object() {"Comma ( , )", "Pipe ( | )", "Tab (" & Global.Microsoft.VisualBasic.ChrW(9) & ")"})
        Me.cmbDelim.Location = New System.Drawing.Point(71, 41)
        Me.cmbDelim.Name = "cmbDelim"
        Me.cmbDelim.Size = New System.Drawing.Size(128, 21)
        Me.cmbDelim.TabIndex = 42
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(11, 43)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 15)
        Me.Label3.TabIndex = 41
        Me.Label3.Text = "Delimiter:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'dgvFieldMap
        '
        Me.dgvFieldMap.AllowUserToAddRows = False
        Me.dgvFieldMap.AllowUserToDeleteRows = False
        Me.dgvFieldMap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFieldMap.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Include, Me.FileField, Me.Map})
        Me.dgvFieldMap.Location = New System.Drawing.Point(14, 115)
        Me.dgvFieldMap.Name = "dgvFieldMap"
        Me.dgvFieldMap.RowHeadersVisible = False
        Me.dgvFieldMap.Size = New System.Drawing.Size(407, 178)
        Me.dgvFieldMap.TabIndex = 39
        '
        'pb
        '
        Me.pb.Location = New System.Drawing.Point(11, 309)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(448, 20)
        Me.pb.TabIndex = 37
        '
        'btnSel
        '
        Me.btnSel.Image = CType(resources.GetObject("btnSel.Image"), System.Drawing.Image)
        Me.btnSel.Location = New System.Drawing.Point(430, 267)
        Me.btnSel.Name = "btnSel"
        Me.btnSel.Size = New System.Drawing.Size(32, 26)
        Me.btnSel.TabIndex = 36
        Me.btnSel.UseVisualStyleBackColor = True
        '
        'btnDesel
        '
        Me.btnDesel.Image = CType(resources.GetObject("btnDesel.Image"), System.Drawing.Image)
        Me.btnDesel.Location = New System.Drawing.Point(430, 235)
        Me.btnDesel.Name = "btnDesel"
        Me.btnDesel.Size = New System.Drawing.Size(32, 26)
        Me.btnDesel.TabIndex = 35
        Me.btnDesel.UseVisualStyleBackColor = True
        '
        'btnLoad
        '
        Me.btnLoad.Image = CType(resources.GetObject("btnLoad.Image"), System.Drawing.Image)
        Me.btnLoad.Location = New System.Drawing.Point(430, 115)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(32, 26)
        Me.btnLoad.TabIndex = 34
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'btnOpen
        '
        Me.btnOpen.Image = CType(resources.GetObject("btnOpen.Image"), System.Drawing.Image)
        Me.btnOpen.Location = New System.Drawing.Point(430, 74)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(32, 26)
        Me.btnOpen.TabIndex = 33
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'txtFile
        '
        Me.txtFile.Location = New System.Drawing.Point(14, 78)
        Me.txtFile.Name = "txtFile"
        Me.txtFile.ReadOnly = True
        Me.txtFile.Size = New System.Drawing.Size(410, 20)
        Me.txtFile.TabIndex = 32
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(213, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(249, 15)
        Me.Label1.TabIndex = 31
        Me.Label1.Text = "CSV, Pipe or TAB DELIMITED (CSV/TXT File only"
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
        Me.Map.FillWeight = 206.0!
        Me.Map.HeaderText = "Mapping"
        Me.Map.Items.AddRange(New Object() {"Payer ID", "External System", "External ID"})
        Me.Map.Name = "Map"
        Me.Map.Width = 206
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'frmPayerMappingUpload
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(475, 343)
        Me.Controls.Add(Me.cmbDelim)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dgvFieldMap)
        Me.Controls.Add(Me.pb)
        Me.Controls.Add(Me.btnSel)
        Me.Controls.Add(Me.btnDesel)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.txtFile)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPayerMappingUpload"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Payer Mapping Upload"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
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
    Friend WithEvents cmbDelim As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dgvFieldMap As System.Windows.Forms.DataGridView
    Friend WithEvents pb As System.Windows.Forms.ProgressBar
    Friend WithEvents btnSel As System.Windows.Forms.Button
    Friend WithEvents btnDesel As System.Windows.Forms.Button
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents btnOpen As System.Windows.Forms.Button
    Friend WithEvents txtFile As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Include As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents FileField As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Map As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog

End Class
