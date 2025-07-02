<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInstantiateOrders
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInstantiateOrders))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnProcess = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnCancel = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.btnHelp = New System.Windows.Forms.ToolStripButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtpDate = New System.Windows.Forms.DateTimePicker
        Me.cmbOrderType = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.dgvClients = New System.Windows.Forms.DataGridView
        Me.SelClient = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.ClientID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ClientName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.dgvOrders = New System.Windows.Forms.DataGridView
        Me.SelOrder = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.OrderID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.OrderType = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Patient = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Start = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TestDays = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Expire = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnDeselAll = New System.Windows.Forms.Button
        Me.btnSelAll = New System.Windows.Forms.Button
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.btnSellClients = New System.Windows.Forms.Button
        Me.btnDesellClients = New System.Windows.Forms.Button
        Me.ToolStrip1.SuspendLayout()
        CType(Me.dgvClients, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvOrders, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnProcess, Me.ToolStripSeparator1, Me.btnCancel, Me.ToolStripSeparator4, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(686, 25)
        Me.ToolStrip1.TabIndex = 5
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnProcess
        '
        Me.btnProcess.AutoSize = False
        Me.btnProcess.Enabled = False
        Me.btnProcess.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnProcess.Image = CType(resources.GetObject("btnProcess.Image"), System.Drawing.Image)
        Me.btnProcess.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(80, 22)
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
        Me.btnCancel.Size = New System.Drawing.Size(80, 22)
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
        Me.btnHelp.Size = New System.Drawing.Size(80, 22)
        Me.btnHelp.Text = "Help"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(21, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 17)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Target Date"
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = SystemConfig.DateFormat
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(12, 65)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(115, 20)
        Me.dtpDate.TabIndex = 8
        '
        'cmbOrderType
        '
        Me.cmbOrderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOrderType.FormattingEnabled = True
        Me.cmbOrderType.Items.AddRange(New Object() {"INFINITE", "TIMED"})
        Me.cmbOrderType.Location = New System.Drawing.Point(145, 64)
        Me.cmbOrderType.Name = "cmbOrderType"
        Me.cmbOrderType.Size = New System.Drawing.Size(130, 21)
        Me.cmbOrderType.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(155, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 17)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Order Type"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(24, 100)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(94, 17)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Clients"
        '
        'dgvClients
        '
        Me.dgvClients.AllowUserToAddRows = False
        Me.dgvClients.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Azure
        Me.dgvClients.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvClients.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SelClient, Me.ClientID, Me.ClientName})
        Me.dgvClients.Location = New System.Drawing.Point(12, 120)
        Me.dgvClients.Name = "dgvClients"
        Me.dgvClients.RowHeadersVisible = False
        Me.dgvClients.Size = New System.Drawing.Size(263, 254)
        Me.dgvClients.TabIndex = 13
        '
        'SelClient
        '
        Me.SelClient.FillWeight = 30.0!
        Me.SelClient.HeaderText = ""
        Me.SelClient.Name = "SelClient"
        Me.SelClient.Width = 30
        '
        'ClientID
        '
        Me.ClientID.FillWeight = 68.0!
        Me.ClientID.HeaderText = "ID"
        Me.ClientID.Name = "ClientID"
        Me.ClientID.ReadOnly = True
        Me.ClientID.Width = 68
        '
        'ClientName
        '
        Me.ClientName.FillWeight = 140.0!
        Me.ClientName.HeaderText = "Client Name"
        Me.ClientName.Name = "ClientName"
        Me.ClientName.ReadOnly = True
        Me.ClientName.Width = 140
        '
        'dgvOrders
        '
        Me.dgvOrders.AllowUserToAddRows = False
        Me.dgvOrders.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.LavenderBlush
        Me.dgvOrders.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOrders.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SelOrder, Me.OrderID, Me.OrderType, Me.Patient, Me.Start, Me.TestDays, Me.Expire})
        Me.dgvOrders.Location = New System.Drawing.Point(281, 65)
        Me.dgvOrders.Name = "dgvOrders"
        Me.dgvOrders.RowHeadersVisible = False
        Me.dgvOrders.Size = New System.Drawing.Size(393, 341)
        Me.dgvOrders.TabIndex = 14
        '
        'SelOrder
        '
        Me.SelOrder.FillWeight = 30.0!
        Me.SelOrder.HeaderText = ""
        Me.SelOrder.Name = "SelOrder"
        Me.SelOrder.Width = 30
        '
        'OrderID
        '
        Me.OrderID.FillWeight = 40.0!
        Me.OrderID.HeaderText = "ID"
        Me.OrderID.Name = "OrderID"
        Me.OrderID.ReadOnly = True
        Me.OrderID.Width = 40
        '
        'OrderType
        '
        Me.OrderType.FillWeight = 32.0!
        Me.OrderType.HeaderText = "Type"
        Me.OrderType.Name = "OrderType"
        Me.OrderType.ReadOnly = True
        Me.OrderType.Width = 32
        '
        'Patient
        '
        Me.Patient.FillWeight = 110.0!
        Me.Patient.HeaderText = "Patient"
        Me.Patient.Name = "Patient"
        Me.Patient.ReadOnly = True
        Me.Patient.Width = 110
        '
        'Start
        '
        Me.Start.FillWeight = 80.0!
        Me.Start.HeaderText = "Start"
        Me.Start.Name = "Start"
        Me.Start.ReadOnly = True
        Me.Start.Width = 80
        '
        'TestDays
        '
        Me.TestDays.FillWeight = 80.0!
        Me.TestDays.HeaderText = "Test Days"
        Me.TestDays.Name = "TestDays"
        Me.TestDays.ReadOnly = True
        Me.TestDays.Width = 80
        '
        'Expire
        '
        Me.Expire.FillWeight = 80.0!
        Me.Expire.HeaderText = "Expire"
        Me.Expire.Name = "Expire"
        Me.Expire.ReadOnly = True
        Me.Expire.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Expire.Width = 80
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 422)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(662, 24)
        Me.ProgressBar1.TabIndex = 15
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(293, 45)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(94, 17)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Orders"
        '
        'btnDeselAll
        '
        Me.btnDeselAll.Image = CType(resources.GetObject("btnDeselAll.Image"), System.Drawing.Image)
        Me.btnDeselAll.Location = New System.Drawing.Point(649, 37)
        Me.btnDeselAll.Name = "btnDeselAll"
        Me.btnDeselAll.Size = New System.Drawing.Size(25, 22)
        Me.btnDeselAll.TabIndex = 17
        Me.btnDeselAll.UseVisualStyleBackColor = True
        '
        'btnSelAll
        '
        Me.btnSelAll.Image = CType(resources.GetObject("btnSelAll.Image"), System.Drawing.Image)
        Me.btnSelAll.Location = New System.Drawing.Point(616, 37)
        Me.btnSelAll.Name = "btnSelAll"
        Me.btnSelAll.Size = New System.Drawing.Size(25, 22)
        Me.btnSelAll.TabIndex = 18
        Me.btnSelAll.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(188, 380)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(87, 26)
        Me.btnUpdate.TabIndex = 19
        Me.btnUpdate.Text = "Update Orders"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnSellClients
        '
        Me.btnSellClients.Image = CType(resources.GetObject("btnSellClients.Image"), System.Drawing.Image)
        Me.btnSellClients.Location = New System.Drawing.Point(12, 382)
        Me.btnSellClients.Name = "btnSellClients"
        Me.btnSellClients.Size = New System.Drawing.Size(25, 22)
        Me.btnSellClients.TabIndex = 21
        Me.btnSellClients.UseVisualStyleBackColor = True
        '
        'btnDesellClients
        '
        Me.btnDesellClients.Image = CType(resources.GetObject("btnDesellClients.Image"), System.Drawing.Image)
        Me.btnDesellClients.Location = New System.Drawing.Point(45, 382)
        Me.btnDesellClients.Name = "btnDesellClients"
        Me.btnDesellClients.Size = New System.Drawing.Size(25, 22)
        Me.btnDesellClients.TabIndex = 20
        Me.btnDesellClients.UseVisualStyleBackColor = True
        '
        'frmInstantiateOrders
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(686, 458)
        Me.Controls.Add(Me.btnSellClients)
        Me.Controls.Add(Me.btnDesellClients)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.btnSelAll)
        Me.Controls.Add(Me.btnDeselAll)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.dgvOrders)
        Me.Controls.Add(Me.dgvClients)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbOrderType)
        Me.Controls.Add(Me.dtpDate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmInstantiateOrders"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Instantiate Orders"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.dgvClients, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvOrders, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnProcess As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbOrderType As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dgvClients As System.Windows.Forms.DataGridView
    Friend WithEvents dgvOrders As System.Windows.Forms.DataGridView
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents SelClient As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ClientID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ClientName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SelOrder As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents OrderID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrderType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Patient As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Start As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TestDays As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Expire As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnDeselAll As System.Windows.Forms.Button
    Friend WithEvents btnSelAll As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnSellClients As System.Windows.Forms.Button
    Friend WithEvents btnDesellClients As System.Windows.Forms.Button

End Class
