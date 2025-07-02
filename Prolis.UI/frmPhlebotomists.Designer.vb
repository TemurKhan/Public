<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPhlebotomists
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPhlebotomists))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        btnAccept = New ToolStripButton()
        chkEditNew = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnSave = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnDelete = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        dgvPhlebotomists = New DataGridView()
        RoutID = New DataGridViewTextBoxColumn()
        LName = New DataGridViewTextBoxColumn()
        FName = New DataGridViewTextBoxColumn()
        MName = New DataGridViewTextBoxColumn()
        FullName = New DataGridViewTextBoxColumn()
        Email = New DataGridViewTextBoxColumn()
        Password = New DataGridViewTextBoxColumn()
        Active = New DataGridViewCheckBoxColumn()
        Cell = New DataGridViewTextBoxColumn()
        HPhone = New DataGridViewTextBoxColumn()
        Address_ID = New DataGridViewTextBoxColumn()
        Address = New DataGridViewTextBoxColumn()
        StartDate = New DataGridViewTextBoxColumn()
        EndDate = New DataGridViewTextBoxColumn()
        Label2 = New Label()
        Label1 = New Label()
        txtID = New TextBox()
        txtLName = New TextBox()
        Label3 = New Label()
        txtFName = New TextBox()
        Label4 = New Label()
        txtMName = New TextBox()
        Label5 = New Label()
        txtEmail = New TextBox()
        Label6 = New Label()
        txtPassword = New TextBox()
        chkActive = New CheckBox()
        Label7 = New Label()
        txtCell = New MaskedTextBox()
        txtHPhone = New MaskedTextBox()
        Label8 = New Label()
        Label9 = New Label()
        txtAddress = New TextBox()
        txtCountry = New TextBox()
        txtCity = New TextBox()
        txtState = New TextBox()
        txtZip = New TextBox()
        Label10 = New Label()
        Label11 = New Label()
        Label12 = New Label()
        Label13 = New Label()
        Label14 = New Label()
        txtStartDate = New MaskedTextBox()
        txtEndDate = New MaskedTextBox()
        Label15 = New Label()
        Label16 = New Label()
        ToolStrip1.SuspendLayout()
        CType(dgvPhlebotomists, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnAccept, chkEditNew, ToolStripSeparator1, btnSave, ToolStripSeparator2, btnDelete, ToolStripSeparator3, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(822, 34)
        ToolStrip1.TabIndex = 4
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnAccept
        ' 
        btnAccept.Enabled = False
        btnAccept.Image = CType(resources.GetObject("btnAccept.Image"), Image)
        btnAccept.ImageTransparentColor = Color.Magenta
        btnAccept.Name = "btnAccept"
        btnAccept.Size = New Size(94, 29)
        btnAccept.Text = "Accept"
        ' 
        ' chkEditNew
        ' 
        chkEditNew.CheckOnClick = True
        chkEditNew.Image = CType(resources.GetObject("chkEditNew.Image"), Image)
        chkEditNew.ImageTransparentColor = Color.Magenta
        chkEditNew.Name = "chkEditNew"
        chkEditNew.Size = New Size(70, 29)
        chkEditNew.Text = "Edit"
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
        btnSave.Size = New Size(77, 29)
        btnSave.Text = "Save"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 34)
        ' 
        ' btnDelete
        ' 
        btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), Image)
        btnDelete.ImageTransparentColor = Color.Magenta
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(90, 29)
        btnDelete.Text = "Delete"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(6, 34)
        ' 
        ' btnCancel
        ' 
        btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), Image)
        btnCancel.ImageTransparentColor = Color.Magenta
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(91, 29)
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
        btnHelp.Size = New Size(77, 29)
        btnHelp.Text = "Help"
        ' 
        ' dgvPhlebotomists
        ' 
        dgvPhlebotomists.AllowUserToAddRows = False
        dgvPhlebotomists.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(255), CByte(224), CByte(192))
        dgvPhlebotomists.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvPhlebotomists.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvPhlebotomists.Columns.AddRange(New DataGridViewColumn() {RoutID, LName, FName, MName, FullName, Email, Password, Active, Cell, HPhone, Address_ID, Address, StartDate, EndDate})
        dgvPhlebotomists.Location = New Point(20, 71)
        dgvPhlebotomists.Margin = New Padding(5, 6, 5, 6)
        dgvPhlebotomists.Name = "dgvPhlebotomists"
        dgvPhlebotomists.ReadOnly = True
        dgvPhlebotomists.RowHeadersVisible = False
        dgvPhlebotomists.RowHeadersWidth = 62
        dgvPhlebotomists.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvPhlebotomists.Size = New Size(792, 262)
        dgvPhlebotomists.TabIndex = 24
        ' 
        ' RoutID
        ' 
        RoutID.FillWeight = 50F
        RoutID.HeaderText = "ID"
        RoutID.MaxInputLength = 12
        RoutID.MinimumWidth = 8
        RoutID.Name = "RoutID"
        RoutID.ReadOnly = True
        RoutID.Width = 84
        ' 
        ' LName
        ' 
        LName.HeaderText = "LName"
        LName.MinimumWidth = 8
        LName.Name = "LName"
        LName.ReadOnly = True
        LName.Visible = False
        LName.Width = 150
        ' 
        ' FName
        ' 
        FName.HeaderText = "FName"
        FName.MinimumWidth = 8
        FName.Name = "FName"
        FName.ReadOnly = True
        FName.Visible = False
        FName.Width = 150
        ' 
        ' MName
        ' 
        MName.HeaderText = "MName"
        MName.MinimumWidth = 8
        MName.Name = "MName"
        MName.ReadOnly = True
        MName.Visible = False
        MName.Width = 150
        ' 
        ' FullName
        ' 
        FullName.FillWeight = 115F
        FullName.HeaderText = "Phlebotomist"
        FullName.MaxInputLength = 60
        FullName.MinimumWidth = 8
        FullName.Name = "FullName"
        FullName.ReadOnly = True
        FullName.Width = 193
        ' 
        ' Email
        ' 
        Email.FillWeight = 130F
        Email.HeaderText = "Email"
        Email.MinimumWidth = 8
        Email.Name = "Email"
        Email.ReadOnly = True
        Email.Width = 218
        ' 
        ' Password
        ' 
        Password.HeaderText = "Password"
        Password.MinimumWidth = 8
        Password.Name = "Password"
        Password.ReadOnly = True
        Password.Visible = False
        Password.Width = 150
        ' 
        ' Active
        ' 
        Active.FillWeight = 45F
        Active.HeaderText = "Active"
        Active.MinimumWidth = 8
        Active.Name = "Active"
        Active.ReadOnly = True
        Active.Width = 76
        ' 
        ' Cell
        ' 
        Cell.HeaderText = "Cell"
        Cell.MinimumWidth = 8
        Cell.Name = "Cell"
        Cell.ReadOnly = True
        Cell.Visible = False
        Cell.Width = 150
        ' 
        ' HPhone
        ' 
        HPhone.HeaderText = "HPhone"
        HPhone.MinimumWidth = 8
        HPhone.Name = "HPhone"
        HPhone.ReadOnly = True
        HPhone.Visible = False
        HPhone.Width = 150
        ' 
        ' Address_ID
        ' 
        Address_ID.HeaderText = "Address_ID"
        Address_ID.MinimumWidth = 8
        Address_ID.Name = "Address_ID"
        Address_ID.ReadOnly = True
        Address_ID.Visible = False
        Address_ID.Width = 150
        ' 
        ' Address
        ' 
        Address.FillWeight = 130F
        Address.HeaderText = "Address"
        Address.MinimumWidth = 8
        Address.Name = "Address"
        Address.ReadOnly = True
        Address.Width = 218
        ' 
        ' StartDate
        ' 
        StartDate.HeaderText = "StartDate"
        StartDate.MinimumWidth = 8
        StartDate.Name = "StartDate"
        StartDate.ReadOnly = True
        StartDate.Visible = False
        StartDate.Width = 150
        ' 
        ' EndDate
        ' 
        EndDate.HeaderText = "EndDate"
        EndDate.MinimumWidth = 8
        EndDate.Name = "EndDate"
        EndDate.ReadOnly = True
        EndDate.Visible = False
        EndDate.Width = 150
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.Red
        Label2.Location = New Point(170, 350)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(140, 31)
        Label2.TabIndex = 32
        Label2.Text = "Last Name"
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.Red
        Label1.Location = New Point(38, 352)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(87, 31)
        Label1.TabIndex = 31
        Label1.Text = "Pleb ID"
        ' 
        ' txtID
        ' 
        txtID.Location = New Point(20, 388)
        txtID.Margin = New Padding(5, 6, 5, 6)
        txtID.MaxLength = 12
        txtID.Name = "txtID"
        txtID.ReadOnly = True
        txtID.Size = New Size(119, 31)
        txtID.TabIndex = 29
        txtID.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtLName
        ' 
        txtLName.Location = New Point(157, 387)
        txtLName.Margin = New Padding(5, 6, 5, 6)
        txtLName.MaxLength = 60
        txtLName.Name = "txtLName"
        txtLName.Size = New Size(172, 31)
        txtLName.TabIndex = 30
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.Red
        Label3.Location = New Point(357, 352)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(140, 31)
        Label3.TabIndex = 34
        Label3.Text = "First Name"
        ' 
        ' txtFName
        ' 
        txtFName.Location = New Point(342, 387)
        txtFName.Margin = New Padding(5, 6, 5, 6)
        txtFName.MaxLength = 60
        txtFName.Name = "txtFName"
        txtFName.Size = New Size(172, 31)
        txtFName.TabIndex = 33
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.Navy
        Label4.Location = New Point(540, 352)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(140, 31)
        Label4.TabIndex = 36
        Label4.Text = "Middle Name"
        ' 
        ' txtMName
        ' 
        txtMName.Location = New Point(527, 388)
        txtMName.Margin = New Padding(5, 6, 5, 6)
        txtMName.MaxLength = 60
        txtMName.Name = "txtMName"
        txtMName.Size = New Size(172, 31)
        txtMName.TabIndex = 35
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.Red
        Label5.Location = New Point(33, 446)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(140, 31)
        Label5.TabIndex = 38
        Label5.Text = "Email / User ID"
        ' 
        ' txtEmail
        ' 
        txtEmail.Location = New Point(20, 483)
        txtEmail.Margin = New Padding(5, 6, 5, 6)
        txtEmail.MaxLength = 60
        txtEmail.Name = "txtEmail"
        txtEmail.Size = New Size(224, 31)
        txtEmail.TabIndex = 37
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.Fuchsia
        Label6.Location = New Point(270, 446)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(110, 31)
        Label6.TabIndex = 40
        Label6.Text = "Password"
        ' 
        ' txtPassword
        ' 
        txtPassword.Location = New Point(257, 483)
        txtPassword.Margin = New Padding(5, 6, 5, 6)
        txtPassword.MaxLength = 60
        txtPassword.Name = "txtPassword"
        txtPassword.PasswordChar = "*"c
        txtPassword.Size = New Size(151, 31)
        txtPassword.TabIndex = 39
        ' 
        ' chkActive
        ' 
        chkActive.Appearance = Appearance.Button
        chkActive.Checked = True
        chkActive.CheckState = CheckState.Checked
        chkActive.Location = New Point(712, 385)
        chkActive.Margin = New Padding(5, 6, 5, 6)
        chkActive.Name = "chkActive"
        chkActive.Size = New Size(85, 46)
        chkActive.TabIndex = 42
        chkActive.Text = "Yes"
        chkActive.TextAlign = ContentAlignment.MiddleCenter
        chkActive.UseVisualStyleBackColor = True
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.Navy
        Label7.Location = New Point(720, 352)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(77, 31)
        Label7.TabIndex = 43
        Label7.Text = "Active?"
        ' 
        ' txtCell
        ' 
        txtCell.Location = New Point(420, 483)
        txtCell.Margin = New Padding(5, 6, 5, 6)
        txtCell.Name = "txtCell"
        txtCell.Size = New Size(176, 31)
        txtCell.TabIndex = 44
        ' 
        ' txtHPhone
        ' 
        txtHPhone.Location = New Point(608, 483)
        txtHPhone.Margin = New Padding(5, 6, 5, 6)
        txtHPhone.Name = "txtHPhone"
        txtHPhone.Size = New Size(176, 31)
        txtHPhone.TabIndex = 45
        ' 
        ' Label8
        ' 
        Label8.ForeColor = Color.Fuchsia
        Label8.Location = New Point(435, 446)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(110, 31)
        Label8.TabIndex = 46
        Label8.Text = "Cell Phone"
        ' 
        ' Label9
        ' 
        Label9.ForeColor = Color.Navy
        Label9.Location = New Point(625, 446)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(140, 31)
        Label9.TabIndex = 47
        Label9.Text = "Home Phone"
        ' 
        ' txtAddress
        ' 
        txtAddress.Location = New Point(20, 579)
        txtAddress.Margin = New Padding(5, 6, 5, 6)
        txtAddress.Name = "txtAddress"
        txtAddress.Size = New Size(177, 31)
        txtAddress.TabIndex = 48
        ' 
        ' txtCountry
        ' 
        txtCountry.Location = New Point(667, 579)
        txtCountry.Margin = New Padding(5, 6, 5, 6)
        txtCountry.Name = "txtCountry"
        txtCountry.Size = New Size(142, 31)
        txtCountry.TabIndex = 49
        ' 
        ' txtCity
        ' 
        txtCity.Location = New Point(210, 579)
        txtCity.Margin = New Padding(5, 6, 5, 6)
        txtCity.Name = "txtCity"
        txtCity.Size = New Size(177, 31)
        txtCity.TabIndex = 50
        ' 
        ' txtState
        ' 
        txtState.Location = New Point(400, 579)
        txtState.Margin = New Padding(5, 6, 5, 6)
        txtState.Name = "txtState"
        txtState.Size = New Size(122, 31)
        txtState.TabIndex = 51
        ' 
        ' txtZip
        ' 
        txtZip.Location = New Point(535, 579)
        txtZip.Margin = New Padding(5, 6, 5, 6)
        txtZip.Name = "txtZip"
        txtZip.Size = New Size(119, 31)
        txtZip.TabIndex = 52
        ' 
        ' Label10
        ' 
        Label10.ForeColor = Color.Fuchsia
        Label10.Location = New Point(38, 542)
        Label10.Margin = New Padding(5, 0, 5, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(110, 31)
        Label10.TabIndex = 53
        Label10.Text = "Address"
        ' 
        ' Label11
        ' 
        Label11.ForeColor = Color.Fuchsia
        Label11.Location = New Point(540, 542)
        Label11.Margin = New Padding(5, 0, 5, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(110, 31)
        Label11.TabIndex = 54
        Label11.Text = "Zip"
        ' 
        ' Label12
        ' 
        Label12.ForeColor = Color.Fuchsia
        Label12.Location = New Point(217, 542)
        Label12.Margin = New Padding(5, 0, 5, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(115, 31)
        Label12.TabIndex = 55
        Label12.Text = "City"
        ' 
        ' Label13
        ' 
        Label13.ForeColor = Color.Fuchsia
        Label13.Location = New Point(407, 542)
        Label13.Margin = New Padding(5, 0, 5, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(110, 31)
        Label13.TabIndex = 56
        Label13.Text = "State"
        ' 
        ' Label14
        ' 
        Label14.ForeColor = Color.Navy
        Label14.Location = New Point(677, 542)
        Label14.Margin = New Padding(5, 0, 5, 0)
        Label14.Name = "Label14"
        Label14.Size = New Size(110, 31)
        Label14.TabIndex = 57
        Label14.Text = "Country"
        ' 
        ' txtStartDate
        ' 
        txtStartDate.Location = New Point(175, 640)
        txtStartDate.Margin = New Padding(5, 6, 5, 6)
        txtStartDate.Name = "txtStartDate"
        txtStartDate.Size = New Size(176, 31)
        txtStartDate.TabIndex = 58
        ' 
        ' txtEndDate
        ' 
        txtEndDate.Location = New Point(502, 640)
        txtEndDate.Margin = New Padding(5, 6, 5, 6)
        txtEndDate.Name = "txtEndDate"
        txtEndDate.Size = New Size(176, 31)
        txtEndDate.TabIndex = 59
        ' 
        ' Label15
        ' 
        Label15.ForeColor = Color.Navy
        Label15.Location = New Point(65, 646)
        Label15.Margin = New Padding(5, 0, 5, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(100, 31)
        Label15.TabIndex = 60
        Label15.Text = "Start Date:"
        Label15.TextAlign = ContentAlignment.TopRight
        ' 
        ' Label16
        ' 
        Label16.ForeColor = Color.Navy
        Label16.Location = New Point(397, 646)
        Label16.Margin = New Padding(5, 0, 5, 0)
        Label16.Name = "Label16"
        Label16.Size = New Size(100, 31)
        Label16.TabIndex = 61
        Label16.Text = "Quit Date:"
        Label16.TextAlign = ContentAlignment.TopRight
        ' 
        ' frmPhlebotomists
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(822, 669)
        Controls.Add(Label16)
        Controls.Add(Label15)
        Controls.Add(txtEndDate)
        Controls.Add(txtStartDate)
        Controls.Add(Label14)
        Controls.Add(Label13)
        Controls.Add(Label12)
        Controls.Add(Label11)
        Controls.Add(Label10)
        Controls.Add(txtZip)
        Controls.Add(txtState)
        Controls.Add(txtCity)
        Controls.Add(txtCountry)
        Controls.Add(txtAddress)
        Controls.Add(Label9)
        Controls.Add(Label8)
        Controls.Add(txtHPhone)
        Controls.Add(txtCell)
        Controls.Add(Label7)
        Controls.Add(chkActive)
        Controls.Add(Label6)
        Controls.Add(txtPassword)
        Controls.Add(Label5)
        Controls.Add(txtEmail)
        Controls.Add(Label4)
        Controls.Add(txtMName)
        Controls.Add(Label3)
        Controls.Add(txtFName)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(txtID)
        Controls.Add(txtLName)
        Controls.Add(dgvPhlebotomists)
        Controls.Add(ToolStrip1)
        Margin = New Padding(5, 6, 5, 6)
        MaximumSize = New Size(844, 725)
        MinimumSize = New Size(844, 725)
        Name = "frmPhlebotomists"
        Text = "Phlebotomist Management"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgvPhlebotomists, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnAccept As System.Windows.Forms.ToolStripButton
    Friend WithEvents chkEditNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents dgvPhlebotomists As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents txtLName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtFName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtMName As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCell As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtHPhone As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtCountry As System.Windows.Forms.TextBox
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents txtState As System.Windows.Forms.TextBox
    Friend WithEvents txtZip As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtStartDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtEndDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents RoutID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FullName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Email As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Password As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Active As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Cell As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HPhone As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Address_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Address As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StartDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EndDate As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
