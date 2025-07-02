<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddProvider
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddProvider))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnSS = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.btnSave = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.btnCancel = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnHelp = New System.Windows.Forms.ToolStripButton
        Me.txtProviderID = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkIndGrp = New System.Windows.Forms.CheckBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtLName = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtFName = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtMName = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtDegree = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtAdd1 = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtAdd2 = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtCity = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtState = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtZip = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtPhone = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtFax = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtCountry = New System.Windows.Forms.TextBox
        Me.txtEmail = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnSS, Me.ToolStripSeparator2, Me.btnSave, Me.ToolStripSeparator4, Me.btnCancel, Me.ToolStripSeparator1, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(435, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnSS
        '
        Me.btnSS.Image = CType(resources.GetObject("btnSS.Image"), System.Drawing.Image)
        Me.btnSS.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSS.Name = "btnSS"
        Me.btnSS.Size = New System.Drawing.Size(90, 22)
        Me.btnSS.Text = "Save / Select"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(51, 22)
        Me.btnSave.Text = "Save"
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
        Me.btnCancel.Size = New System.Drawing.Size(59, 22)
        Me.btnCancel.Text = "Cancel"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(48, 22)
        Me.btnHelp.Text = "Help"
        '
        'txtProviderID
        '
        Me.txtProviderID.AcceptsReturn = True
        Me.txtProviderID.Location = New System.Drawing.Point(12, 76)
        Me.txtProviderID.MaxLength = 12
        Me.txtProviderID.Name = "txtProviderID"
        Me.txtProviderID.Size = New System.Drawing.Size(132, 20)
        Me.txtProviderID.TabIndex = 3
        Me.txtProviderID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(12, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Provider ID"
        '
        'chkIndGrp
        '
        Me.chkIndGrp.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkIndGrp.Checked = True
        Me.chkIndGrp.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIndGrp.Location = New System.Drawing.Point(170, 28)
        Me.chkIndGrp.Name = "chkIndGrp"
        Me.chkIndGrp.Size = New System.Drawing.Size(94, 25)
        Me.chkIndGrp.TabIndex = 2
        Me.chkIndGrp.Text = "Individual"
        Me.chkIndGrp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkIndGrp.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(150, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Last Name"
        '
        'txtLName
        '
        Me.txtLName.AcceptsReturn = True
        Me.txtLName.Location = New System.Drawing.Point(153, 76)
        Me.txtLName.MaxLength = 60
        Me.txtLName.Name = "txtLName"
        Me.txtLName.Size = New System.Drawing.Size(270, 20)
        Me.txtLName.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(12, 109)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "First Name"
        '
        'txtFName
        '
        Me.txtFName.AcceptsReturn = True
        Me.txtFName.Location = New System.Drawing.Point(12, 125)
        Me.txtFName.MaxLength = 35
        Me.txtFName.Name = "txtFName"
        Me.txtFName.Size = New System.Drawing.Size(132, 20)
        Me.txtFName.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label4.Location = New System.Drawing.Point(150, 109)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(113, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Middle Name"
        '
        'txtMName
        '
        Me.txtMName.AcceptsReturn = True
        Me.txtMName.Location = New System.Drawing.Point(153, 125)
        Me.txtMName.MaxLength = 35
        Me.txtMName.Name = "txtMName"
        Me.txtMName.Size = New System.Drawing.Size(132, 20)
        Me.txtMName.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label5.Location = New System.Drawing.Point(288, 105)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(42, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Degree"
        '
        'txtDegree
        '
        Me.txtDegree.AcceptsReturn = True
        Me.txtDegree.Location = New System.Drawing.Point(291, 125)
        Me.txtDegree.MaxLength = 25
        Me.txtDegree.Name = "txtDegree"
        Me.txtDegree.Size = New System.Drawing.Size(132, 20)
        Me.txtDegree.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(9, 158)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(107, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Address Line 1"
        '
        'txtAdd1
        '
        Me.txtAdd1.AcceptsReturn = True
        Me.txtAdd1.Location = New System.Drawing.Point(12, 174)
        Me.txtAdd1.MaxLength = 35
        Me.txtAdd1.Name = "txtAdd1"
        Me.txtAdd1.Size = New System.Drawing.Size(132, 20)
        Me.txtAdd1.TabIndex = 8
        '
        'Label7
        '
        Me.Label7.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label7.Location = New System.Drawing.Point(150, 158)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(99, 13)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "Address Line 2"
        '
        'txtAdd2
        '
        Me.txtAdd2.AcceptsReturn = True
        Me.txtAdd2.Location = New System.Drawing.Point(153, 174)
        Me.txtAdd2.MaxLength = 35
        Me.txtAdd2.Name = "txtAdd2"
        Me.txtAdd2.Size = New System.Drawing.Size(132, 20)
        Me.txtAdd2.TabIndex = 9
        '
        'Label8
        '
        Me.Label8.ForeColor = System.Drawing.Color.Red
        Me.Label8.Location = New System.Drawing.Point(291, 158)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(71, 13)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "City"
        '
        'txtCity
        '
        Me.txtCity.AcceptsReturn = True
        Me.txtCity.Location = New System.Drawing.Point(291, 174)
        Me.txtCity.MaxLength = 35
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(132, 20)
        Me.txtCity.TabIndex = 10
        '
        'Label9
        '
        Me.Label9.ForeColor = System.Drawing.Color.Red
        Me.Label9.Location = New System.Drawing.Point(9, 208)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(107, 13)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "State/Province"
        '
        'txtState
        '
        Me.txtState.AcceptsReturn = True
        Me.txtState.Location = New System.Drawing.Point(12, 224)
        Me.txtState.MaxLength = 35
        Me.txtState.Name = "txtState"
        Me.txtState.Size = New System.Drawing.Size(132, 20)
        Me.txtState.TabIndex = 11
        '
        'Label10
        '
        Me.Label10.ForeColor = System.Drawing.Color.Red
        Me.Label10.Location = New System.Drawing.Point(150, 208)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(61, 13)
        Me.Label10.TabIndex = 23
        Me.Label10.Text = "Zip"
        '
        'txtZip
        '
        Me.txtZip.AcceptsReturn = True
        Me.txtZip.Location = New System.Drawing.Point(153, 224)
        Me.txtZip.MaxLength = 25
        Me.txtZip.Name = "txtZip"
        Me.txtZip.Size = New System.Drawing.Size(132, 20)
        Me.txtZip.TabIndex = 12
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label11.Location = New System.Drawing.Point(12, 256)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(69, 13)
        Me.Label11.TabIndex = 25
        Me.Label11.Text = "Office Phone"
        '
        'txtPhone
        '
        Me.txtPhone.AcceptsReturn = True
        Me.txtPhone.Location = New System.Drawing.Point(12, 272)
        Me.txtPhone.MaxLength = 25
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New System.Drawing.Size(132, 20)
        Me.txtPhone.TabIndex = 14
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label12.Location = New System.Drawing.Point(150, 256)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(24, 13)
        Me.Label12.TabIndex = 27
        Me.Label12.Text = "Fax"
        '
        'txtFax
        '
        Me.txtFax.AcceptsReturn = True
        Me.txtFax.Location = New System.Drawing.Point(153, 272)
        Me.txtFax.MaxLength = 25
        Me.txtFax.Name = "txtFax"
        Me.txtFax.Size = New System.Drawing.Size(132, 20)
        Me.txtFax.TabIndex = 15
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label13.Location = New System.Drawing.Point(288, 208)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(43, 13)
        Me.Label13.TabIndex = 29
        Me.Label13.Text = "Country"
        '
        'txtCountry
        '
        Me.txtCountry.AcceptsReturn = True
        Me.txtCountry.Location = New System.Drawing.Point(291, 224)
        Me.txtCountry.MaxLength = 35
        Me.txtCountry.Name = "txtCountry"
        Me.txtCountry.Size = New System.Drawing.Size(132, 20)
        Me.txtCountry.TabIndex = 13
        '
        'txtEmail
        '
        Me.txtEmail.AcceptsReturn = True
        Me.txtEmail.Location = New System.Drawing.Point(291, 272)
        Me.txtEmail.MaxLength = 25
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(132, 20)
        Me.txtEmail.TabIndex = 30
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label14.Location = New System.Drawing.Point(291, 256)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(32, 13)
        Me.Label14.TabIndex = 31
        Me.Label14.Text = "Email"
        '
        'frmAddProvider
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(435, 304)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtCountry)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtFax)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtPhone)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtZip)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtState)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtCity)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtAdd2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtAdd1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtDegree)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtMName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtFName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtLName)
        Me.Controls.Add(Me.chkIndGrp)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtProviderID)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddProvider"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add Provider"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnSS As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtProviderID As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkIndGrp As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtLName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtFName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtMName As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtDegree As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtAdd1 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtAdd2 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtState As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtZip As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtFax As System.Windows.Forms.TextBox
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtCountry As System.Windows.Forms.TextBox
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label

End Class
