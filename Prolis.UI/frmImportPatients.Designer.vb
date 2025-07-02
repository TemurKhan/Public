<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImportPatients
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImportPatients))
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbDelimiter = New System.Windows.Forms.ComboBox()
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.txtFile = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvFieldMap = New System.Windows.Forms.DataGridView()
        Me.btnSel = New System.Windows.Forms.Button()
        Me.btnDesel = New System.Windows.Forms.Button()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.PB = New System.Windows.Forms.ToolStripProgressBar()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Include = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.FileField = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Map = New System.Windows.Forms.DataGridViewComboBoxColumn()
        CType(Me.dgvFieldMap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(12, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 14)
        Me.Label3.TabIndex = 60
        Me.Label3.Text = "File Type"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbDelimiter
        '
        Me.cmbDelimiter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDelimiter.FormattingEnabled = True
        Me.cmbDelimiter.Items.AddRange(New Object() {"COMA SEPARATED VALUES (*.CSV)", "PIPE SEPARATED VALUES (*.*)", "TAB SEPARATED VALUES (*.*)"})
        Me.cmbDelimiter.Location = New System.Drawing.Point(72, 12)
        Me.cmbDelimiter.Name = "cmbDelimiter"
        Me.cmbDelimiter.Size = New System.Drawing.Size(390, 21)
        Me.cmbDelimiter.TabIndex = 59
        '
        'btnOpen
        '
        Me.btnOpen.Image = CType(resources.GetObject("btnOpen.Image"), System.Drawing.Image)
        Me.btnOpen.Location = New System.Drawing.Point(430, 57)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(32, 26)
        Me.btnOpen.TabIndex = 63
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'txtFile
        '
        Me.txtFile.Location = New System.Drawing.Point(15, 61)
        Me.txtFile.Name = "txtFile"
        Me.txtFile.ReadOnly = True
        Me.txtFile.Size = New System.Drawing.Size(407, 20)
        Me.txtFile.TabIndex = 62
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(18, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 16)
        Me.Label1.TabIndex = 61
        Me.Label1.Text = "Source File"
        '
        'dgvFieldMap
        '
        Me.dgvFieldMap.AllowUserToAddRows = False
        Me.dgvFieldMap.AllowUserToDeleteRows = False
        Me.dgvFieldMap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFieldMap.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Include, Me.FileField, Me.Map})
        Me.dgvFieldMap.Location = New System.Drawing.Point(15, 94)
        Me.dgvFieldMap.Name = "dgvFieldMap"
        Me.dgvFieldMap.RowHeadersVisible = False
        Me.dgvFieldMap.Size = New System.Drawing.Size(407, 190)
        Me.dgvFieldMap.TabIndex = 67
        '
        'btnSel
        '
        Me.btnSel.Image = CType(resources.GetObject("btnSel.Image"), System.Drawing.Image)
        Me.btnSel.Location = New System.Drawing.Point(430, 258)
        Me.btnSel.Name = "btnSel"
        Me.btnSel.Size = New System.Drawing.Size(32, 26)
        Me.btnSel.TabIndex = 66
        Me.btnSel.UseVisualStyleBackColor = True
        '
        'btnDesel
        '
        Me.btnDesel.Image = CType(resources.GetObject("btnDesel.Image"), System.Drawing.Image)
        Me.btnDesel.Location = New System.Drawing.Point(430, 226)
        Me.btnDesel.Name = "btnDesel"
        Me.btnDesel.Size = New System.Drawing.Size(32, 26)
        Me.btnDesel.TabIndex = 65
        Me.btnDesel.UseVisualStyleBackColor = True
        '
        'btnLoad
        '
        Me.btnLoad.Image = CType(resources.GetObject("btnLoad.Image"), System.Drawing.Image)
        Me.btnLoad.Location = New System.Drawing.Point(430, 94)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(32, 26)
        Me.btnLoad.TabIndex = 64
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'btnImport
        '
        Me.btnImport.Enabled = False
        Me.btnImport.ForeColor = System.Drawing.Color.Navy
        Me.btnImport.Image = CType(resources.GetObject("btnImport.Image"), System.Drawing.Image)
        Me.btnImport.Location = New System.Drawing.Point(164, 299)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(149, 31)
        Me.btnImport.TabIndex = 68
        Me.btnImport.Text = "Import"
        Me.btnImport.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus, Me.PB})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 344)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(477, 22)
        Me.StatusStrip1.TabIndex = 69
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
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
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
        Me.Map.Items.AddRange(New Object() {"Last Name", "First Name", "Middle Name", "DOB", "Gender", "Address1", "Address2", "City", "State", "Zip", "Country", "SSN", "Phone", "Bill Type", "Prime Payer", "Prime Policy", "Prime Group ", "Prime Relation", "Second Payer", "Second Policy", "Second Group", "Second Relation", "Client ID", "Chart"})
        Me.Map.Name = "Map"
        Me.Map.Width = 210
        '
        'frmImportPatients
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(477, 366)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.btnImport)
        Me.Controls.Add(Me.dgvFieldMap)
        Me.Controls.Add(Me.btnSel)
        Me.Controls.Add(Me.btnDesel)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.txtFile)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbDelimiter)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximumSize = New System.Drawing.Size(493, 405)
        Me.MinimumSize = New System.Drawing.Size(493, 405)
        Me.Name = "frmImportPatients"
        Me.Text = "Import Patients"
        CType(Me.dgvFieldMap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbDelimiter As System.Windows.Forms.ComboBox
    Friend WithEvents btnOpen As System.Windows.Forms.Button
    Friend WithEvents txtFile As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvFieldMap As System.Windows.Forms.DataGridView
    Friend WithEvents btnSel As System.Windows.Forms.Button
    Friend WithEvents btnDesel As System.Windows.Forms.Button
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents btnImport As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PB As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Include As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents FileField As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Map As System.Windows.Forms.DataGridViewComboBoxColumn
End Class
