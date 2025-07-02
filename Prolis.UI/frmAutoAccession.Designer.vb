<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAutoAccession
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAutoAccession))
        Me.chkIntExt = New System.Windows.Forms.CheckBox()
        Me.dgvDiscrete = New System.Windows.Forms.DataGridView()
        Me.Clients = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.txtOutput = New System.Windows.Forms.TextBox()
        Me.gbExt = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbDelimiter = New System.Windows.Forms.ComboBox()
        Me.dgvFieldMap = New System.Windows.Forms.DataGridView()
        Me.btnSel = New System.Windows.Forms.Button()
        Me.btnDesel = New System.Windows.Forms.Button()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.txtFile = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.BW = New System.ComponentModel.BackgroundWorker()
        Me.btnFetchOrders = New System.Windows.Forms.Button()
        Me.dgvOrders = New System.Windows.Forms.DataGridView()
        Me.Sel = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.CID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Component = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CompType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.btnCompLook = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.PB = New System.Windows.Forms.ToolStripProgressBar()
        Me.Include = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.FileField = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Map = New System.Windows.Forms.DataGridViewComboBoxColumn()
        CType(Me.dgvDiscrete, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbExt.SuspendLayout()
        CType(Me.dgvFieldMap, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvOrders, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkIntExt
        '
        Me.chkIntExt.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkIntExt.ForeColor = System.Drawing.Color.Navy
        Me.chkIntExt.Location = New System.Drawing.Point(257, 12)
        Me.chkIntExt.Name = "chkIntExt"
        Me.chkIntExt.Size = New System.Drawing.Size(149, 32)
        Me.chkIntExt.TabIndex = 0
        Me.chkIntExt.Text = "Internal Patients"
        Me.chkIntExt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkIntExt.UseVisualStyleBackColor = True
        '
        'dgvDiscrete
        '
        Me.dgvDiscrete.AllowUserToAddRows = False
        Me.dgvDiscrete.AllowUserToDeleteRows = False
        Me.dgvDiscrete.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDiscrete.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Clients})
        Me.dgvDiscrete.Location = New System.Drawing.Point(484, 54)
        Me.dgvDiscrete.Name = "dgvDiscrete"
        Me.dgvDiscrete.RowHeadersVisible = False
        Me.dgvDiscrete.Size = New System.Drawing.Size(167, 231)
        Me.dgvDiscrete.TabIndex = 50
        '
        'Clients
        '
        Me.Clients.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Clients.HeaderText = "Client ID (one per line)"
        Me.Clients.Name = "Clients"
        '
        'btnGenerate
        '
        Me.btnGenerate.Enabled = False
        Me.btnGenerate.ForeColor = System.Drawing.Color.Navy
        Me.btnGenerate.Location = New System.Drawing.Point(257, 332)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(149, 31)
        Me.btnGenerate.TabIndex = 51
        Me.btnGenerate.Text = "Generate Accessions"
        Me.btnGenerate.UseVisualStyleBackColor = True
        '
        'txtOutput
        '
        Me.txtOutput.BackColor = System.Drawing.Color.Snow
        Me.txtOutput.Location = New System.Drawing.Point(12, 374)
        Me.txtOutput.Multiline = True
        Me.txtOutput.Name = "txtOutput"
        Me.txtOutput.ReadOnly = True
        Me.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOutput.Size = New System.Drawing.Size(407, 115)
        Me.txtOutput.TabIndex = 52
        '
        'gbExt
        '
        Me.gbExt.Controls.Add(Me.Label3)
        Me.gbExt.Controls.Add(Me.cmbDelimiter)
        Me.gbExt.Controls.Add(Me.dgvFieldMap)
        Me.gbExt.Controls.Add(Me.btnSel)
        Me.gbExt.Controls.Add(Me.btnDesel)
        Me.gbExt.Controls.Add(Me.btnLoad)
        Me.gbExt.Controls.Add(Me.btnOpen)
        Me.gbExt.Controls.Add(Me.txtFile)
        Me.gbExt.Controls.Add(Me.Label1)
        Me.gbExt.Enabled = False
        Me.gbExt.Location = New System.Drawing.Point(12, 50)
        Me.gbExt.Name = "gbExt"
        Me.gbExt.Size = New System.Drawing.Size(456, 278)
        Me.gbExt.TabIndex = 53
        Me.gbExt.TabStop = False
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(0, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 14)
        Me.Label3.TabIndex = 58
        Me.Label3.Text = "File Type"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbDelimiter
        '
        Me.cmbDelimiter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDelimiter.FormattingEnabled = True
        Me.cmbDelimiter.Items.AddRange(New Object() {"COMA SEPARATED VALUES (*.CSV)", "PIPE SEPARATED VALUES (*.*)", "TAB SEPARATED VALUES (*.*)"})
        Me.cmbDelimiter.Location = New System.Drawing.Point(60, 5)
        Me.cmbDelimiter.Name = "cmbDelimiter"
        Me.cmbDelimiter.Size = New System.Drawing.Size(347, 21)
        Me.cmbDelimiter.TabIndex = 57
        '
        'dgvFieldMap
        '
        Me.dgvFieldMap.AllowUserToAddRows = False
        Me.dgvFieldMap.AllowUserToDeleteRows = False
        Me.dgvFieldMap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFieldMap.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Include, Me.FileField, Me.Map})
        Me.dgvFieldMap.Location = New System.Drawing.Point(0, 82)
        Me.dgvFieldMap.Name = "dgvFieldMap"
        Me.dgvFieldMap.RowHeadersVisible = False
        Me.dgvFieldMap.Size = New System.Drawing.Size(407, 190)
        Me.dgvFieldMap.TabIndex = 56
        '
        'btnSel
        '
        Me.btnSel.Image = CType(resources.GetObject("btnSel.Image"), System.Drawing.Image)
        Me.btnSel.Location = New System.Drawing.Point(415, 246)
        Me.btnSel.Name = "btnSel"
        Me.btnSel.Size = New System.Drawing.Size(32, 26)
        Me.btnSel.TabIndex = 55
        Me.btnSel.UseVisualStyleBackColor = True
        '
        'btnDesel
        '
        Me.btnDesel.Image = CType(resources.GetObject("btnDesel.Image"), System.Drawing.Image)
        Me.btnDesel.Location = New System.Drawing.Point(415, 214)
        Me.btnDesel.Name = "btnDesel"
        Me.btnDesel.Size = New System.Drawing.Size(32, 26)
        Me.btnDesel.TabIndex = 54
        Me.btnDesel.UseVisualStyleBackColor = True
        '
        'btnLoad
        '
        Me.btnLoad.Image = CType(resources.GetObject("btnLoad.Image"), System.Drawing.Image)
        Me.btnLoad.Location = New System.Drawing.Point(415, 82)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(32, 26)
        Me.btnLoad.TabIndex = 53
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'btnOpen
        '
        Me.btnOpen.Image = CType(resources.GetObject("btnOpen.Image"), System.Drawing.Image)
        Me.btnOpen.Location = New System.Drawing.Point(415, 44)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(32, 26)
        Me.btnOpen.TabIndex = 52
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'txtFile
        '
        Me.txtFile.Location = New System.Drawing.Point(0, 48)
        Me.txtFile.Name = "txtFile"
        Me.txtFile.ReadOnly = True
        Me.txtFile.Size = New System.Drawing.Size(407, 20)
        Me.txtFile.TabIndex = 51
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(3, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 16)
        Me.Label1.TabIndex = 50
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
        'btnFetchOrders
        '
        Me.btnFetchOrders.Enabled = False
        Me.btnFetchOrders.ForeColor = System.Drawing.Color.Navy
        Me.btnFetchOrders.Location = New System.Drawing.Point(501, 291)
        Me.btnFetchOrders.Name = "btnFetchOrders"
        Me.btnFetchOrders.Size = New System.Drawing.Size(128, 31)
        Me.btnFetchOrders.TabIndex = 54
        Me.btnFetchOrders.Text = "Fetch Orders"
        Me.btnFetchOrders.UseVisualStyleBackColor = True
        '
        'dgvOrders
        '
        Me.dgvOrders.AllowUserToAddRows = False
        Me.dgvOrders.AllowUserToDeleteRows = False
        Me.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOrders.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Sel, Me.CID, Me.Component, Me.CompType})
        Me.dgvOrders.Location = New System.Drawing.Point(427, 334)
        Me.dgvOrders.Name = "dgvOrders"
        Me.dgvOrders.RowHeadersVisible = False
        Me.dgvOrders.Size = New System.Drawing.Size(224, 128)
        Me.dgvOrders.TabIndex = 55
        '
        'Sel
        '
        Me.Sel.FillWeight = 30.0!
        Me.Sel.HeaderText = ""
        Me.Sel.Name = "Sel"
        Me.Sel.Width = 30
        '
        'CID
        '
        Me.CID.FillWeight = 80.0!
        Me.CID.HeaderText = "ID"
        Me.CID.Name = "CID"
        Me.CID.Width = 80
        '
        'Component
        '
        Me.Component.HeaderText = "Name"
        Me.Component.Name = "Component"
        '
        'CompType
        '
        Me.CompType.HeaderText = "CompType"
        Me.CompType.Name = "CompType"
        Me.CompType.Visible = False
        '
        'txtID
        '
        Me.txtID.Location = New System.Drawing.Point(427, 468)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(55, 20)
        Me.txtID.TabIndex = 56
        '
        'btnCompLook
        '
        Me.btnCompLook.Image = CType(resources.GetObject("btnCompLook.Image"), System.Drawing.Image)
        Me.btnCompLook.Location = New System.Drawing.Point(488, 468)
        Me.btnCompLook.Name = "btnCompLook"
        Me.btnCompLook.Size = New System.Drawing.Size(21, 21)
        Me.btnCompLook.TabIndex = 57
        Me.btnCompLook.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Enabled = False
        Me.btnAdd.Image = CType(resources.GetObject("btnAdd.Image"), System.Drawing.Image)
        Me.btnAdd.Location = New System.Drawing.Point(628, 468)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(21, 20)
        Me.btnAdd.TabIndex = 58
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(515, 468)
        Me.txtName.Name = "txtName"
        Me.txtName.ReadOnly = True
        Me.txtName.Size = New System.Drawing.Size(107, 20)
        Me.txtName.TabIndex = 59
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus, Me.PB})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 495)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(663, 22)
        Me.StatusStrip1.TabIndex = 60
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
        Me.Map.Items.AddRange(New Object() {"Last Name", "First Name", "Middle Name", "DOB", "Gender", "Email", "Phone", "Address1", "Address2", "City", "State", "Zip", "Country", "ReqNo", "Acc Date", "Client ID", "Att. Provider ID", "Component IDs", "Dxs"})
        Me.Map.Name = "Map"
        Me.Map.Width = 210
        '
        'frmAutoAccession
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(663, 517)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnCompLook)
        Me.Controls.Add(Me.txtID)
        Me.Controls.Add(Me.dgvOrders)
        Me.Controls.Add(Me.btnFetchOrders)
        Me.Controls.Add(Me.gbExt)
        Me.Controls.Add(Me.txtOutput)
        Me.Controls.Add(Me.btnGenerate)
        Me.Controls.Add(Me.dgvDiscrete)
        Me.Controls.Add(Me.chkIntExt)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmAutoAccession"
        Me.Text = "Auto Accessions"
        CType(Me.dgvDiscrete, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbExt.ResumeLayout(False)
        Me.gbExt.PerformLayout()
        CType(Me.dgvFieldMap, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvOrders, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkIntExt As System.Windows.Forms.CheckBox
    Friend WithEvents dgvDiscrete As System.Windows.Forms.DataGridView
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents txtOutput As System.Windows.Forms.TextBox
    Friend WithEvents gbExt As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbDelimiter As System.Windows.Forms.ComboBox
    Friend WithEvents dgvFieldMap As System.Windows.Forms.DataGridView
    Friend WithEvents btnSel As System.Windows.Forms.Button
    Friend WithEvents btnDesel As System.Windows.Forms.Button
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents btnOpen As System.Windows.Forms.Button
    Friend WithEvents txtFile As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Clients As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents BW As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnFetchOrders As System.Windows.Forms.Button
    Friend WithEvents dgvOrders As System.Windows.Forms.DataGridView
    Friend WithEvents Sel As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents CID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Component As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CompType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents btnCompLook As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PB As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents Include As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents FileField As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Map As System.Windows.Forms.DataGridViewComboBoxColumn
End Class
