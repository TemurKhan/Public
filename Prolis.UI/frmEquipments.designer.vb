<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEquipments
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEquipments))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.chkEditNew = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnSave = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.btnDelete = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.btnCancel = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.btnHelp = New System.Windows.Forms.ToolStripButton
        Me.dgvEquips = New System.Windows.Forms.DataGridView
        Me.txtEquipName = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtEquipID = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtCommDLL = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbBaud = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmbParity = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.cmbData = New System.Windows.Forms.ComboBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.cmbStop = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.chkActive = New System.Windows.Forms.CheckBox
        Me.chkSerialIP = New System.Windows.Forms.CheckBox
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EquipName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.IsActive = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.SNet = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CommDLL = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Baurd = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Parity = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Data = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.StopBit = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ToolStrip1.SuspendLayout()
        CType(Me.dgvEquips, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.chkEditNew, Me.ToolStripSeparator1, Me.btnSave, Me.ToolStripSeparator2, Me.btnDelete, Me.ToolStripSeparator3, Me.btnCancel, Me.ToolStripSeparator4, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(604, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'chkEditNew
        '
        Me.chkEditNew.CheckOnClick = True
        Me.chkEditNew.Image = CType(resources.GetObject("chkEditNew.Image"), System.Drawing.Image)
        Me.chkEditNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.chkEditNew.Name = "chkEditNew"
        Me.chkEditNew.Size = New System.Drawing.Size(47, 22)
        Me.chkEditNew.Text = "Edit"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(51, 22)
        Me.btnSave.Text = "Save"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(60, 22)
        Me.btnDelete.Text = "Delete"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'btnCancel
        '
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(63, 22)
        Me.btnCancel.Text = "Cancel"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(52, 22)
        Me.btnHelp.Text = "Help"
        '
        'dgvEquips
        '
        Me.dgvEquips.AllowUserToAddRows = False
        Me.dgvEquips.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.dgvEquips.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvEquips.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvEquips.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID, Me.EquipName, Me.IsActive, Me.SNet, Me.CommDLL, Me.Baurd, Me.Parity, Me.Data, Me.StopBit})
        Me.dgvEquips.Location = New System.Drawing.Point(12, 43)
        Me.dgvEquips.Name = "dgvEquips"
        Me.dgvEquips.ReadOnly = True
        Me.dgvEquips.RowHeadersVisible = False
        Me.dgvEquips.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvEquips.Size = New System.Drawing.Size(580, 213)
        Me.dgvEquips.TabIndex = 2
        '
        'txtEquipName
        '
        Me.txtEquipName.Location = New System.Drawing.Point(96, 293)
        Me.txtEquipName.MaxLength = 60
        Me.txtEquipName.Name = "txtEquipName"
        Me.txtEquipName.Size = New System.Drawing.Size(215, 20)
        Me.txtEquipName.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label2.Location = New System.Drawing.Point(96, 271)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(154, 19)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Equipment Name"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtEquipID
        '
        Me.txtEquipID.Location = New System.Drawing.Point(12, 293)
        Me.txtEquipID.MaxLength = 2
        Me.txtEquipID.Name = "txtEquipID"
        Me.txtEquipID.ReadOnly = True
        Me.txtEquipID.Size = New System.Drawing.Size(78, 20)
        Me.txtEquipID.TabIndex = 7
        Me.txtEquipID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label1.Location = New System.Drawing.Point(24, 271)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 19)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "ID"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCommDLL
        '
        Me.txtCommDLL.Location = New System.Drawing.Point(326, 293)
        Me.txtCommDLL.MaxLength = 50
        Me.txtCommDLL.Name = "txtCommDLL"
        Me.txtCommDLL.Size = New System.Drawing.Size(266, 20)
        Me.txtCommDLL.TabIndex = 11
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label3.Location = New System.Drawing.Point(334, 271)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(107, 19)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Comm DLL"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbBaud
        '
        Me.cmbBaud.FormattingEnabled = True
        Me.cmbBaud.Items.AddRange(New Object() {"300", "600", "1200", "1800", "2400", "3600", "4800", "7200", "9600", "14400", "19200", "28800", "38400", "57600", "115200"})
        Me.cmbBaud.Location = New System.Drawing.Point(154, 340)
        Me.cmbBaud.Name = "cmbBaud"
        Me.cmbBaud.Size = New System.Drawing.Size(118, 21)
        Me.cmbBaud.TabIndex = 12
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label4.Location = New System.Drawing.Point(165, 318)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 19)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Baud Rate"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label5.Location = New System.Drawing.Point(286, 318)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 19)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Parity"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbParity
        '
        Me.cmbParity.FormattingEnabled = True
        Me.cmbParity.Items.AddRange(New Object() {"odd ", "even", "none "})
        Me.cmbParity.Location = New System.Drawing.Point(278, 340)
        Me.cmbParity.Name = "cmbParity"
        Me.cmbParity.Size = New System.Drawing.Size(112, 21)
        Me.cmbParity.TabIndex = 14
        '
        'Label7
        '
        Me.Label7.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label7.Location = New System.Drawing.Point(410, 318)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(63, 19)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "Data Bits"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbData
        '
        Me.cmbData.FormattingEnabled = True
        Me.cmbData.Items.AddRange(New Object() {"5", "6", "7", "8 "})
        Me.cmbData.Location = New System.Drawing.Point(404, 340)
        Me.cmbData.Name = "cmbData"
        Me.cmbData.Size = New System.Drawing.Size(92, 21)
        Me.cmbData.TabIndex = 18
        '
        'Label8
        '
        Me.Label8.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label8.Location = New System.Drawing.Point(506, 318)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(65, 19)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "Stop Bit"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbStop
        '
        Me.cmbStop.FormattingEnabled = True
        Me.cmbStop.Items.AddRange(New Object() {"1", "1.5 ", "2"})
        Me.cmbStop.Location = New System.Drawing.Point(509, 340)
        Me.cmbStop.Name = "cmbStop"
        Me.cmbStop.Size = New System.Drawing.Size(83, 21)
        Me.cmbStop.TabIndex = 20
        '
        'Label6
        '
        Me.Label6.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label6.Location = New System.Drawing.Point(25, 318)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(37, 19)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "Active"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkActive
        '
        Me.chkActive.Location = New System.Drawing.Point(37, 343)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(25, 15)
        Me.chkActive.TabIndex = 23
        Me.chkActive.UseVisualStyleBackColor = True
        '
        'chkSerialIP
        '
        Me.chkSerialIP.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkSerialIP.ForeColor = System.Drawing.Color.DarkBlue
        Me.chkSerialIP.Location = New System.Drawing.Point(68, 336)
        Me.chkSerialIP.Name = "chkSerialIP"
        Me.chkSerialIP.Size = New System.Drawing.Size(78, 26)
        Me.chkSerialIP.TabIndex = 24
        Me.chkSerialIP.Text = "Serial"
        Me.chkSerialIP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkSerialIP.UseVisualStyleBackColor = True
        '
        'ID
        '
        Me.ID.FillWeight = 40.0!
        Me.ID.HeaderText = "ID"
        Me.ID.MaxInputLength = 2
        Me.ID.Name = "ID"
        Me.ID.ReadOnly = True
        Me.ID.Width = 40
        '
        'EquipName
        '
        Me.EquipName.HeaderText = "Equipment"
        Me.EquipName.MaxInputLength = 60
        Me.EquipName.Name = "EquipName"
        Me.EquipName.ReadOnly = True
        '
        'IsActive
        '
        Me.IsActive.FillWeight = 45.0!
        Me.IsActive.HeaderText = "Active"
        Me.IsActive.Name = "IsActive"
        Me.IsActive.ReadOnly = True
        Me.IsActive.Width = 45
        '
        'SNet
        '
        Me.SNet.FillWeight = 50.0!
        Me.SNet.HeaderText = "S/Net"
        Me.SNet.Name = "SNet"
        Me.SNet.ReadOnly = True
        Me.SNet.Width = 50
        '
        'CommDLL
        '
        Me.CommDLL.HeaderText = "Comm DLL"
        Me.CommDLL.Name = "CommDLL"
        Me.CommDLL.ReadOnly = True
        '
        'Baurd
        '
        Me.Baurd.FillWeight = 70.0!
        Me.Baurd.HeaderText = "Baurd"
        Me.Baurd.Name = "Baurd"
        Me.Baurd.ReadOnly = True
        Me.Baurd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Baurd.Width = 70
        '
        'Parity
        '
        Me.Parity.FillWeight = 60.0!
        Me.Parity.HeaderText = "Parity"
        Me.Parity.Name = "Parity"
        Me.Parity.ReadOnly = True
        Me.Parity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Parity.Width = 60
        '
        'Data
        '
        Me.Data.FillWeight = 45.0!
        Me.Data.HeaderText = "Data"
        Me.Data.Name = "Data"
        Me.Data.ReadOnly = True
        Me.Data.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Data.Width = 45
        '
        'StopBit
        '
        Me.StopBit.FillWeight = 45.0!
        Me.StopBit.HeaderText = "Stop"
        Me.StopBit.MaxInputLength = 2
        Me.StopBit.Name = "StopBit"
        Me.StopBit.ReadOnly = True
        Me.StopBit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.StopBit.Width = 45
        '
        'frmEquipments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(604, 383)
        Me.Controls.Add(Me.chkSerialIP)
        Me.Controls.Add(Me.chkActive)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cmbStop)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cmbData)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmbParity)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbBaud)
        Me.Controls.Add(Me.txtCommDLL)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtEquipName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtEquipID)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgvEquips)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEquipments"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Equipments"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.dgvEquips, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents chkEditNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents dgvEquips As System.Windows.Forms.DataGridView
    Friend WithEvents txtEquipName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtEquipID As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCommDLL As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbBaud As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbParity As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbData As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbStop As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents chkSerialIP As System.Windows.Forms.CheckBox
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EquipName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IsActive As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents SNet As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CommDLL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Baurd As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Parity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Data As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StopBit As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
