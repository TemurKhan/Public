<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmESig
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmESig))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnAccept = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnDelete = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.btnCancel = New System.Windows.Forms.ToolStripButton
        Me.btnHelp = New System.Windows.Forms.ToolStripButton
        Me.dgvSignatories = New System.Windows.Forms.DataGridView
        Me.UID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Signatory = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Designation = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.txtPWD1 = New System.Windows.Forms.TextBox
        Me.txtPWD2 = New System.Windows.Forms.TextBox
        Me.btnValidate = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtSignee = New System.Windows.Forms.TextBox
        Me.ToolStrip1.SuspendLayout()
        CType(Me.dgvSignatories, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnAccept, Me.ToolStripSeparator1, Me.btnDelete, Me.ToolStripSeparator4, Me.btnCancel, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(435, 25)
        Me.ToolStrip1.TabIndex = 3
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnAccept
        '
        Me.btnAccept.Enabled = False
        Me.btnAccept.Image = CType(resources.GetObject("btnAccept.Image"), System.Drawing.Image)
        Me.btnAccept.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(64, 22)
        Me.btnAccept.Text = "Accept"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnDelete
        '
        Me.btnDelete.AutoSize = False
        Me.btnDelete.Enabled = False
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(90, 22)
        Me.btnDelete.Text = "Remove Sig"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'btnCancel
        '
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(63, 22)
        Me.btnCancel.Text = "Cancel"
        '
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(52, 22)
        Me.btnHelp.Text = "Help"
        '
        'dgvSignatories
        '
        Me.dgvSignatories.AllowUserToAddRows = False
        Me.dgvSignatories.AllowUserToDeleteRows = False
        Me.dgvSignatories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSignatories.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.UID, Me.Signatory, Me.Designation})
        Me.dgvSignatories.Location = New System.Drawing.Point(12, 76)
        Me.dgvSignatories.Name = "dgvSignatories"
        Me.dgvSignatories.ReadOnly = True
        Me.dgvSignatories.RowHeadersVisible = False
        Me.dgvSignatories.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSignatories.Size = New System.Drawing.Size(411, 171)
        Me.dgvSignatories.TabIndex = 4
        '
        'UID
        '
        Me.UID.HeaderText = "UID"
        Me.UID.Name = "UID"
        Me.UID.ReadOnly = True
        Me.UID.Visible = False
        '
        'Signatory
        '
        Me.Signatory.FillWeight = 250.0!
        Me.Signatory.HeaderText = "Signatory Name"
        Me.Signatory.Name = "Signatory"
        Me.Signatory.ReadOnly = True
        Me.Signatory.Width = 250
        '
        'Designation
        '
        Me.Designation.FillWeight = 136.0!
        Me.Designation.HeaderText = "Designation"
        Me.Designation.Name = "Designation"
        Me.Designation.ReadOnly = True
        Me.Designation.Width = 136
        '
        'txtPWD1
        '
        Me.txtPWD1.Location = New System.Drawing.Point(12, 280)
        Me.txtPWD1.MaxLength = 20
        Me.txtPWD1.Name = "txtPWD1"
        Me.txtPWD1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(88)
        Me.txtPWD1.ReadOnly = True
        Me.txtPWD1.Size = New System.Drawing.Size(115, 20)
        Me.txtPWD1.TabIndex = 5
        '
        'txtPWD2
        '
        Me.txtPWD2.Location = New System.Drawing.Point(146, 280)
        Me.txtPWD2.MaxLength = 20
        Me.txtPWD2.Name = "txtPWD2"
        Me.txtPWD2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(88)
        Me.txtPWD2.ReadOnly = True
        Me.txtPWD2.Size = New System.Drawing.Size(115, 20)
        Me.txtPWD2.TabIndex = 6
        '
        'btnValidate
        '
        Me.btnValidate.Location = New System.Drawing.Point(287, 276)
        Me.btnValidate.Name = "btnValidate"
        Me.btnValidate.Size = New System.Drawing.Size(136, 27)
        Me.btnValidate.TabIndex = 7
        Me.btnValidate.Text = "Validate"
        Me.btnValidate.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(12, 260)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(115, 17)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Enter Password"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(146, 260)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(115, 17)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Re-Enter Password"
        '
        'txtSignee
        '
        Me.txtSignee.ForeColor = System.Drawing.Color.Red
        Me.txtSignee.Location = New System.Drawing.Point(12, 41)
        Me.txtSignee.MaxLength = 20
        Me.txtSignee.Name = "txtSignee"
        Me.txtSignee.ReadOnly = True
        Me.txtSignee.Size = New System.Drawing.Size(411, 20)
        Me.txtSignee.TabIndex = 10
        Me.txtSignee.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'frmESig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(435, 315)
        Me.Controls.Add(Me.txtSignee)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnValidate)
        Me.Controls.Add(Me.txtPWD2)
        Me.Controls.Add(Me.txtPWD1)
        Me.Controls.Add(Me.dgvSignatories)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmESig"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "eSignature"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.dgvSignatories, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnAccept As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents dgvSignatories As System.Windows.Forms.DataGridView
    Friend WithEvents txtPWD1 As System.Windows.Forms.TextBox
    Friend WithEvents txtPWD2 As System.Windows.Forms.TextBox
    Friend WithEvents btnValidate As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtSignee As System.Windows.Forms.TextBox
    Friend WithEvents UID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Signatory As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Designation As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
