<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRoutes
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRoutes))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnAccept = New System.Windows.Forms.ToolStripButton()
        Me.chkEditNew = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.dgvRoutes = New System.Windows.Forms.DataGridView()
        Me.RoutID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FullName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Courier = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Phleb_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Phleb = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Active = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCourier = New System.Windows.Forms.TextBox()
        Me.txtPhlebotomist = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnPhelbLook = New System.Windows.Forms.Button()
        Me.txtPhlebID = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.dgvRoutes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.CanOverflow = False
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnAccept, Me.chkEditNew, Me.ToolStripSeparator1, Me.btnSave, Me.ToolStripSeparator2, Me.btnDelete, Me.ToolStripSeparator3, Me.btnCancel, Me.ToolStripSeparator4, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(409, 25)
        Me.ToolStrip1.Stretch = True
        Me.ToolStrip1.TabIndex = 3
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnAccept
        '
        Me.btnAccept.AutoSize = False
        Me.btnAccept.Enabled = False
        Me.btnAccept.Image = CType(resources.GetObject("btnAccept.Image"), System.Drawing.Image)
        Me.btnAccept.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(70, 22)
        Me.btnAccept.Text = "Accept"
        '
        'chkEditNew
        '
        Me.chkEditNew.AutoSize = False
        Me.chkEditNew.CheckOnClick = True
        Me.chkEditNew.Image = CType(resources.GetObject("chkEditNew.Image"), System.Drawing.Image)
        Me.chkEditNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.chkEditNew.Name = "chkEditNew"
        Me.chkEditNew.Size = New System.Drawing.Size(50, 22)
        Me.chkEditNew.Text = "Edit"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnSave
        '
        Me.btnSave.AutoSize = False
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
        Me.btnDelete.AutoSize = False
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(58, 22)
        Me.btnDelete.Text = "Delete"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'btnCancel
        '
        Me.btnCancel.AutoSize = False
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
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(48, 22)
        Me.btnHelp.Text = "Help"
        '
        'dgvRoutes
        '
        Me.dgvRoutes.AllowUserToAddRows = False
        Me.dgvRoutes.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.dgvRoutes.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvRoutes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRoutes.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RoutID, Me.FullName, Me.Courier, Me.Phleb_ID, Me.Phleb, Me.Active})
        Me.dgvRoutes.Location = New System.Drawing.Point(12, 41)
        Me.dgvRoutes.Name = "dgvRoutes"
        Me.dgvRoutes.ReadOnly = True
        Me.dgvRoutes.RowHeadersVisible = False
        Me.dgvRoutes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvRoutes.Size = New System.Drawing.Size(475, 136)
        Me.dgvRoutes.TabIndex = 23
        '
        'RoutID
        '
        Me.RoutID.FillWeight = 50.0!
        Me.RoutID.HeaderText = "ID"
        Me.RoutID.MaxInputLength = 12
        Me.RoutID.Name = "RoutID"
        Me.RoutID.ReadOnly = True
        Me.RoutID.Width = 50
        '
        'FullName
        '
        Me.FullName.FillWeight = 115.0!
        Me.FullName.HeaderText = "Name"
        Me.FullName.MaxInputLength = 60
        Me.FullName.Name = "FullName"
        Me.FullName.ReadOnly = True
        Me.FullName.Width = 115
        '
        'Courier
        '
        Me.Courier.FillWeight = 130.0!
        Me.Courier.HeaderText = "Courier"
        Me.Courier.Name = "Courier"
        Me.Courier.ReadOnly = True
        Me.Courier.Width = 130
        '
        'Phleb_ID
        '
        Me.Phleb_ID.HeaderText = "PhlebID"
        Me.Phleb_ID.Name = "Phleb_ID"
        Me.Phleb_ID.ReadOnly = True
        Me.Phleb_ID.Visible = False
        '
        'Phleb
        '
        Me.Phleb.FillWeight = 130.0!
        Me.Phleb.HeaderText = "Phlebotomist"
        Me.Phleb.Name = "Phleb"
        Me.Phleb.ReadOnly = True
        Me.Phleb.Width = 130
        '
        'Active
        '
        Me.Active.FillWeight = 45.0!
        Me.Active.HeaderText = "Active"
        Me.Active.Name = "Active"
        Me.Active.ReadOnly = True
        Me.Active.Width = 45
        '
        'txtID
        '
        Me.txtID.Location = New System.Drawing.Point(12, 211)
        Me.txtID.MaxLength = 12
        Me.txtID.Name = "txtID"
        Me.txtID.ReadOnly = True
        Me.txtID.Size = New System.Drawing.Size(73, 20)
        Me.txtID.TabIndex = 24
        Me.txtID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(94, 210)
        Me.txtName.MaxLength = 60
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(393, 20)
        Me.txtName.TabIndex = 25
        '
        'chkActive
        '
        Me.chkActive.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkActive.Checked = True
        Me.chkActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkActive.Location = New System.Drawing.Point(12, 261)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(73, 24)
        Me.chkActive.TabIndex = 26
        Me.chkActive.Text = "Yes"
        Me.chkActive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkActive.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(13, 192)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 16)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Rout ID"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(91, 191)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(118, 16)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "Route Name"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(95, 245)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(277, 16)
        Me.Label3.TabIndex = 29
        Me.Label3.Text = "Courier Name (Last, First)"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(13, 242)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 16)
        Me.Label4.TabIndex = 30
        Me.Label4.Text = "Active?"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCourier
        '
        Me.txtCourier.Location = New System.Drawing.Point(94, 264)
        Me.txtCourier.MaxLength = 60
        Me.txtCourier.Name = "txtCourier"
        Me.txtCourier.Size = New System.Drawing.Size(393, 20)
        Me.txtCourier.TabIndex = 31
        '
        'txtPhlebotomist
        '
        Me.txtPhlebotomist.Location = New System.Drawing.Point(125, 318)
        Me.txtPhlebotomist.MaxLength = 60
        Me.txtPhlebotomist.Name = "txtPhlebotomist"
        Me.txtPhlebotomist.ReadOnly = True
        Me.txtPhlebotomist.Size = New System.Drawing.Size(362, 20)
        Me.txtPhlebotomist.TabIndex = 32
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(129, 300)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(233, 16)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "Phlebotomist Name (Last, First)"
        '
        'btnPhelbLook
        '
        Me.btnPhelbLook.Image = CType(resources.GetObject("btnPhelbLook.Image"), System.Drawing.Image)
        Me.btnPhelbLook.Location = New System.Drawing.Point(94, 315)
        Me.btnPhelbLook.Name = "btnPhelbLook"
        Me.btnPhelbLook.Size = New System.Drawing.Size(25, 24)
        Me.btnPhelbLook.TabIndex = 34
        Me.btnPhelbLook.UseVisualStyleBackColor = True
        '
        'txtPhlebID
        '
        Me.txtPhlebID.Location = New System.Drawing.Point(12, 318)
        Me.txtPhlebID.MaxLength = 12
        Me.txtPhlebID.Name = "txtPhlebID"
        Me.txtPhlebID.ReadOnly = True
        Me.txtPhlebID.Size = New System.Drawing.Size(73, 20)
        Me.txtPhlebID.TabIndex = 35
        Me.txtPhlebID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(13, 300)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 16)
        Me.Label6.TabIndex = 36
        Me.Label6.Text = "Phelb ID"
        '
        'frmRoutes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(499, 365)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtPhlebID)
        Me.Controls.Add(Me.btnPhelbLook)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtPhlebotomist)
        Me.Controls.Add(Me.txtCourier)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtID)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.chkActive)
        Me.Controls.Add(Me.dgvRoutes)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(515, 404)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(515, 404)
        Me.Name = "frmRoutes"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Route Management"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.dgvRoutes, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents dgvRoutes As System.Windows.Forms.DataGridView
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCourier As System.Windows.Forms.TextBox
    Friend WithEvents btnAccept As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtPhlebotomist As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents RoutID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FullName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Courier As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Phleb_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Phleb As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Active As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents btnPhelbLook As System.Windows.Forms.Button
    Friend WithEvents txtPhlebID As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label

End Class
