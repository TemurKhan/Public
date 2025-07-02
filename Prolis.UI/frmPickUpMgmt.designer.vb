<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPickUpMgmt
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPickUpMgmt))
        ToolStrip1 = New ToolStrip()
        cmbMode = New ToolStripComboBox()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnSave = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        Label1 = New Label()
        txtID = New TextBox()
        btnPickUpLook = New Button()
        txtClient = New TextBox()
        btnProviderLook = New Button()
        Label2 = New Label()
        Label3 = New Label()
        txtContact = New TextBox()
        txtAddress = New TextBox()
        Label4 = New Label()
        Label5 = New Label()
        Label6 = New Label()
        Label7 = New Label()
        txtEmail = New TextBox()
        txtCourier = New TextBox()
        txtRep = New TextBox()
        txtNote = New TextBox()
        chkVoid = New CheckBox()
        Label8 = New Label()
        Label9 = New Label()
        Label10 = New Label()
        lblSchDate = New Label()
        Label12 = New Label()
        Label13 = New Label()
        txtTime = New MaskedTextBox()
        txtProviderID = New TextBox()
        Label14 = New Label()
        txtRouteID = New TextBox()
        txtPhone = New MaskedTextBox()
        txtCell = New MaskedTextBox()
        dtpDate = New DateTimePicker()
        ToolStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(22, 22)
        ToolStrip1.Items.AddRange(New ToolStripItem() {cmbMode, ToolStripSeparator1, btnSave, ToolStripSeparator2, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(933, 34)
        ToolStrip1.TabIndex = 4
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' cmbMode
        ' 
        cmbMode.AutoSize = False
        cmbMode.DropDownStyle = ComboBoxStyle.DropDownList
        cmbMode.Items.AddRange(New Object() {"New", "Edit", "View"})
        cmbMode.Name = "cmbMode"
        cmbMode.Size = New Size(131, 33)
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
        ' 
        ' btnSave
        ' 
        btnSave.Image = CType(resources.GetObject("btnSave.Image"), Image)
        btnSave.ImageTransparentColor = Color.Magenta
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(75, 29)
        btnSave.Text = "Save"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 34)
        ' 
        ' btnCancel
        ' 
        btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), Image)
        btnCancel.ImageTransparentColor = Color.Magenta
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(89, 29)
        btnCancel.Text = "Cancel"
        ' 
        ' ToolStripSeparator4
        ' 
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New Size(6, 34)
        ' 
        ' btnHelp
        ' 
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.ImageTransparentColor = Color.Magenta
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(75, 29)
        btnHelp.Text = "Help"
        ' 
        ' Label1
        ' 
        Label1.Location = New Point(32, 73)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(168, 29)
        Label1.TabIndex = 5
        Label1.Text = "Reservation ID"
        ' 
        ' txtID
        ' 
        txtID.Font = New Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtID.ForeColor = Color.FromArgb(CByte(128), CByte(64), CByte(0))
        txtID.Location = New Point(28, 108)
        txtID.Margin = New Padding(5, 6, 5, 6)
        txtID.Name = "txtID"
        txtID.Size = New Size(169, 30)
        txtID.TabIndex = 1
        txtID.TextAlign = HorizontalAlignment.Center
        ' 
        ' btnPickUpLook
        ' 
        btnPickUpLook.Enabled = False
        btnPickUpLook.Image = CType(resources.GetObject("btnPickUpLook.Image"), Image)
        btnPickUpLook.Location = New Point(210, 106)
        btnPickUpLook.Margin = New Padding(5, 6, 5, 6)
        btnPickUpLook.Name = "btnPickUpLook"
        btnPickUpLook.Size = New Size(47, 46)
        btnPickUpLook.TabIndex = 2
        btnPickUpLook.TabStop = False
        btnPickUpLook.UseVisualStyleBackColor = True
        ' 
        ' txtClient
        ' 
        txtClient.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtClient.ForeColor = SystemColors.ControlText
        txtClient.Location = New Point(495, 113)
        txtClient.Margin = New Padding(5, 6, 5, 6)
        txtClient.Name = "txtClient"
        txtClient.ReadOnly = True
        txtClient.Size = New Size(416, 26)
        txtClient.TabIndex = 5
        ' 
        ' btnProviderLook
        ' 
        btnProviderLook.Image = CType(resources.GetObject("btnProviderLook.Image"), Image)
        btnProviderLook.Location = New Point(438, 106)
        btnProviderLook.Margin = New Padding(5, 6, 5, 6)
        btnProviderLook.Name = "btnProviderLook"
        btnProviderLook.Size = New Size(47, 46)
        btnProviderLook.TabIndex = 4
        btnProviderLook.TabStop = False
        btnProviderLook.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.Location = New Point(285, 73)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(143, 29)
        Label2.TabIndex = 10
        Label2.Text = "Client ID"
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(32, 177)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(168, 29)
        Label3.TabIndex = 11
        Label3.Text = "Contact"
        ' 
        ' txtContact
        ' 
        txtContact.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtContact.ForeColor = SystemColors.ControlText
        txtContact.Location = New Point(28, 212)
        txtContact.Margin = New Padding(5, 6, 5, 6)
        txtContact.MaxLength = 60
        txtContact.Name = "txtContact"
        txtContact.Size = New Size(232, 26)
        txtContact.TabIndex = 6
        ' 
        ' txtAddress
        ' 
        txtAddress.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtAddress.ForeColor = SystemColors.ControlText
        txtAddress.Location = New Point(290, 212)
        txtAddress.Margin = New Padding(5, 6, 5, 6)
        txtAddress.Name = "txtAddress"
        txtAddress.ReadOnly = True
        txtAddress.Size = New Size(621, 26)
        txtAddress.TabIndex = 7
        ' 
        ' Label4
        ' 
        Label4.Location = New Point(290, 177)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(163, 29)
        Label4.TabIndex = 14
        Label4.Text = "Address"
        ' 
        ' Label5
        ' 
        Label5.Location = New Point(32, 275)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(168, 29)
        Label5.TabIndex = 15
        Label5.Text = "Office Phone"
        ' 
        ' Label6
        ' 
        Label6.Location = New Point(285, 275)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(168, 29)
        Label6.TabIndex = 16
        Label6.Text = "Cell"
        ' 
        ' Label7
        ' 
        Label7.Location = New Point(543, 275)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(158, 29)
        Label7.TabIndex = 17
        Label7.Text = "Email"
        ' 
        ' txtEmail
        ' 
        txtEmail.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtEmail.ForeColor = SystemColors.ControlText
        txtEmail.Location = New Point(538, 310)
        txtEmail.Margin = New Padding(5, 6, 5, 6)
        txtEmail.Name = "txtEmail"
        txtEmail.ReadOnly = True
        txtEmail.Size = New Size(372, 26)
        txtEmail.TabIndex = 10
        ' 
        ' txtCourier
        ' 
        txtCourier.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtCourier.ForeColor = SystemColors.ControlText
        txtCourier.Location = New Point(470, 410)
        txtCourier.Margin = New Padding(5, 6, 5, 6)
        txtCourier.Name = "txtCourier"
        txtCourier.ReadOnly = True
        txtCourier.Size = New Size(441, 26)
        txtCourier.TabIndex = 13
        ' 
        ' txtRep
        ' 
        txtRep.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtRep.ForeColor = Color.FromArgb(CByte(128), CByte(64), CByte(0))
        txtRep.Location = New Point(28, 504)
        txtRep.Margin = New Padding(5, 6, 5, 6)
        txtRep.Name = "txtRep"
        txtRep.ReadOnly = True
        txtRep.Size = New Size(232, 26)
        txtRep.TabIndex = 14
        ' 
        ' txtNote
        ' 
        txtNote.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtNote.ForeColor = SystemColors.ControlText
        txtNote.Location = New Point(290, 504)
        txtNote.Margin = New Padding(5, 6, 5, 6)
        txtNote.MaxLength = 400
        txtNote.Multiline = True
        txtNote.Name = "txtNote"
        txtNote.ScrollBars = ScrollBars.Vertical
        txtNote.Size = New Size(621, 212)
        txtNote.TabIndex = 15
        ' 
        ' chkVoid
        ' 
        chkVoid.Appearance = Appearance.Button
        chkVoid.Location = New Point(65, 656)
        chkVoid.Margin = New Padding(5, 6, 5, 6)
        chkVoid.Name = "chkVoid"
        chkVoid.Size = New Size(135, 63)
        chkVoid.TabIndex = 16
        chkVoid.Text = "No"
        chkVoid.TextAlign = ContentAlignment.MiddleCenter
        chkVoid.UseVisualStyleBackColor = True
        ' 
        ' Label8
        ' 
        Label8.Location = New Point(65, 621)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(130, 29)
        Label8.TabIndex = 27
        Label8.Text = "Voided"
        Label8.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label9
        ' 
        Label9.Location = New Point(32, 469)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(168, 29)
        Label9.TabIndex = 28
        Label9.Text = "Representative"
        ' 
        ' Label10
        ' 
        Label10.Location = New Point(300, 469)
        Label10.Margin = New Padding(5, 0, 5, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(168, 29)
        Label10.TabIndex = 29
        Label10.Text = "Instructions"
        ' 
        ' lblSchDate
        ' 
        lblSchDate.Location = New Point(32, 375)
        lblSchDate.Margin = New Padding(5, 0, 5, 0)
        lblSchDate.Name = "lblSchDate"
        lblSchDate.Size = New Size(225, 29)
        lblSchDate.TabIndex = 30
        lblSchDate.Text = "Sched Date"
        ' 
        ' Label12
        ' 
        Label12.Location = New Point(290, 375)
        Label12.Margin = New Padding(5, 0, 5, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(168, 29)
        Label12.TabIndex = 31
        Label12.Text = "Scheduled Time"
        ' 
        ' Label13
        ' 
        Label13.Location = New Point(468, 375)
        Label13.Margin = New Padding(5, 0, 5, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(163, 29)
        Label13.TabIndex = 32
        Label13.Text = "Courier"
        ' 
        ' txtTime
        ' 
        txtTime.Location = New Point(290, 410)
        txtTime.Margin = New Padding(5, 6, 5, 6)
        txtTime.Mask = "00:00"
        txtTime.Name = "txtTime"
        txtTime.Size = New Size(136, 31)
        txtTime.TabIndex = 12
        txtTime.ValidatingType = GetType(Date)
        ' 
        ' txtProviderID
        ' 
        txtProviderID.Location = New Point(290, 113)
        txtProviderID.Margin = New Padding(5, 6, 5, 6)
        txtProviderID.MaxLength = 12
        txtProviderID.Name = "txtProviderID"
        txtProviderID.Size = New Size(136, 31)
        txtProviderID.TabIndex = 3
        txtProviderID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label14
        ' 
        Label14.Location = New Point(495, 73)
        Label14.Margin = New Padding(5, 0, 5, 0)
        Label14.Name = "Label14"
        Label14.Size = New Size(338, 29)
        Label14.TabIndex = 36
        Label14.Text = "Client Name"
        ' 
        ' txtRouteID
        ' 
        txtRouteID.Location = New Point(742, 463)
        txtRouteID.Margin = New Padding(5, 6, 5, 6)
        txtRouteID.MaxLength = 5
        txtRouteID.Name = "txtRouteID"
        txtRouteID.Size = New Size(136, 31)
        txtRouteID.TabIndex = 37
        txtRouteID.TextAlign = HorizontalAlignment.Center
        txtRouteID.Visible = False
        ' 
        ' txtPhone
        ' 
        txtPhone.Location = New Point(28, 310)
        txtPhone.Margin = New Padding(5, 6, 5, 6)
        txtPhone.Name = "txtPhone"
        txtPhone.Size = New Size(232, 31)
        txtPhone.TabIndex = 8
        ' 
        ' txtCell
        ' 
        txtCell.Location = New Point(290, 310)
        txtCell.Margin = New Padding(5, 6, 5, 6)
        txtCell.Name = "txtCell"
        txtCell.Size = New Size(232, 31)
        txtCell.TabIndex = 9
        ' 
        ' dtpDate
        ' 
        dtpDate.CustomFormat = " "
        dtpDate.Format = DateTimePickerFormat.Custom
        dtpDate.Location = New Point(28, 410)
        dtpDate.Margin = New Padding(3, 4, 3, 4)
        dtpDate.Name = "dtpDate"
        dtpDate.Size = New Size(232, 31)
        dtpDate.TabIndex = 86
        ' 
        ' frmPickUpMgmt
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(933, 756)
        Controls.Add(dtpDate)
        Controls.Add(txtCell)
        Controls.Add(txtPhone)
        Controls.Add(txtRouteID)
        Controls.Add(Label14)
        Controls.Add(txtProviderID)
        Controls.Add(txtTime)
        Controls.Add(Label13)
        Controls.Add(Label12)
        Controls.Add(lblSchDate)
        Controls.Add(Label10)
        Controls.Add(Label9)
        Controls.Add(Label8)
        Controls.Add(chkVoid)
        Controls.Add(txtNote)
        Controls.Add(txtRep)
        Controls.Add(txtCourier)
        Controls.Add(txtEmail)
        Controls.Add(Label7)
        Controls.Add(Label6)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(txtAddress)
        Controls.Add(txtContact)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(btnProviderLook)
        Controls.Add(txtClient)
        Controls.Add(btnPickUpLook)
        Controls.Add(txtID)
        Controls.Add(Label1)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmPickUpMgmt"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Pick Up Management"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbMode As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents btnPickUpLook As System.Windows.Forms.Button
    Friend WithEvents txtClient As System.Windows.Forms.TextBox
    Friend WithEvents btnProviderLook As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtContact As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtCourier As System.Windows.Forms.TextBox
    Friend WithEvents txtRep As System.Windows.Forms.TextBox
    Friend WithEvents txtNote As System.Windows.Forms.TextBox
    Friend WithEvents chkVoid As System.Windows.Forms.CheckBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblSchDate As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtTime As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtProviderID As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtRouteID As System.Windows.Forms.TextBox
    Friend WithEvents txtPhone As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtCell As System.Windows.Forms.MaskedTextBox
    Friend WithEvents dtpDate As DateTimePicker
End Class
