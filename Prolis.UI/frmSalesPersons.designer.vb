<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSalesPersons
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSalesPersons))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        chkEditNew = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnSave = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnDelete = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        dgvSales = New DataGridView()
        SrcID = New DataGridViewTextBoxColumn()
        FullName = New DataGridViewTextBoxColumn()
        UoM = New DataGridViewCheckBoxColumn()
        Email = New DataGridViewTextBoxColumn()
        Address_ID = New DataGridViewTextBoxColumn()
        Address = New DataGridViewTextBoxColumn()
        chkActive = New CheckBox()
        Label1 = New Label()
        txtName = New TextBox()
        txtID = New TextBox()
        txtSSN = New MaskedTextBox()
        txtUID = New TextBox()
        txtStart = New MaskedTextBox()
        txtEnd = New MaskedTextBox()
        txtPWD = New TextBox()
        txtAdd1 = New TextBox()
        txtAdd2 = New TextBox()
        txtCity = New TextBox()
        txtState = New TextBox()
        txtZip = New TextBox()
        txtCountry = New TextBox()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        Label6 = New Label()
        Label7 = New Label()
        Label8 = New Label()
        Label9 = New Label()
        Label10 = New Label()
        Label11 = New Label()
        Label12 = New Label()
        Label13 = New Label()
        Label14 = New Label()
        Label15 = New Label()
        Label16 = New Label()
        txtHPhone = New MaskedTextBox()
        txtCell = New MaskedTextBox()
        txtEmail = New TextBox()
        lblEmail = New Label()
        dgvClients = New DataGridView()
        ClientID = New DataGridViewTextBoxColumn()
        Lookup = New DataGridViewImageColumn()
        Client_Name = New DataGridViewTextBoxColumn()
        ClientPhone = New DataGridViewTextBoxColumn()
        ClientAddress = New DataGridViewTextBoxColumn()
        lblClients = New Label()
        ToolStrip1.SuspendLayout()
        CType(dgvSales, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvClients, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {chkEditNew, ToolStripSeparator1, btnSave, ToolStripSeparator2, btnDelete, ToolStripSeparator3, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(1013, 34)
        ToolStrip1.TabIndex = 2
        ToolStrip1.Text = "ToolStrip1"
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
        ' dgvSales
        ' 
        dgvSales.AllowUserToAddRows = False
        dgvSales.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(255), CByte(224), CByte(192))
        dgvSales.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvSales.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvSales.Columns.AddRange(New DataGridViewColumn() {SrcID, FullName, UoM, Email, Address_ID, Address})
        dgvSales.Location = New Point(20, 73)
        dgvSales.Margin = New Padding(5, 6, 5, 6)
        dgvSales.Name = "dgvSales"
        dgvSales.ReadOnly = True
        dgvSales.RowHeadersVisible = False
        dgvSales.RowHeadersWidth = 62
        dgvSales.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvSales.Size = New Size(973, 233)
        dgvSales.TabIndex = 22
        ' 
        ' SrcID
        ' 
        SrcID.FillWeight = 60F
        SrcID.HeaderText = "ID"
        SrcID.MaxInputLength = 12
        SrcID.MinimumWidth = 8
        SrcID.Name = "SrcID"
        SrcID.ReadOnly = True
        SrcID.Width = 113
        ' 
        ' FullName
        ' 
        FullName.FillWeight = 125F
        FullName.HeaderText = "Name"
        FullName.MaxInputLength = 60
        FullName.MinimumWidth = 8
        FullName.Name = "FullName"
        FullName.ReadOnly = True
        FullName.Width = 235
        ' 
        ' UoM
        ' 
        UoM.FillWeight = 50F
        UoM.HeaderText = "Active?"
        UoM.MinimumWidth = 8
        UoM.Name = "UoM"
        UoM.ReadOnly = True
        UoM.Resizable = DataGridViewTriState.True
        UoM.Width = 95
        ' 
        ' Email
        ' 
        Email.FillWeight = 90F
        Email.HeaderText = "Email"
        Email.MinimumWidth = 8
        Email.Name = "Email"
        Email.ReadOnly = True
        Email.Width = 169
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
        Address.FillWeight = 190F
        Address.HeaderText = "Address"
        Address.MinimumWidth = 8
        Address.Name = "Address"
        Address.ReadOnly = True
        Address.SortMode = DataGridViewColumnSortMode.NotSortable
        Address.Width = 358
        ' 
        ' chkActive
        ' 
        chkActive.Appearance = Appearance.Button
        chkActive.Checked = True
        chkActive.CheckState = CheckState.Checked
        chkActive.Location = New Point(913, 356)
        chkActive.Margin = New Padding(5, 6, 5, 6)
        chkActive.Name = "chkActive"
        chkActive.Size = New Size(80, 46)
        chkActive.TabIndex = 6
        chkActive.Text = "Yes"
        chkActive.TextAlign = ContentAlignment.MiddleCenter
        chkActive.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(22, 331)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(122, 25)
        Label1.TabIndex = 24
        Label1.Text = "ID"
        Label1.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtName
        ' 
        txtName.Location = New Point(153, 362)
        txtName.Margin = New Padding(5, 6, 5, 6)
        txtName.MaxLength = 60
        txtName.Name = "txtName"
        txtName.Size = New Size(307, 31)
        txtName.TabIndex = 3
        ' 
        ' txtID
        ' 
        txtID.Location = New Point(18, 363)
        txtID.Margin = New Padding(5, 6, 5, 6)
        txtID.MaxLength = 12
        txtID.Name = "txtID"
        txtID.ReadOnly = True
        txtID.Size = New Size(122, 31)
        txtID.TabIndex = 2
        txtID.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtSSN
        ' 
        txtSSN.Location = New Point(473, 363)
        txtSSN.Margin = New Padding(5, 6, 5, 6)
        txtSSN.Mask = "000-00-0000"
        txtSSN.Name = "txtSSN"
        txtSSN.Size = New Size(116, 31)
        txtSSN.TabIndex = 4
        ' 
        ' txtUID
        ' 
        txtUID.Location = New Point(287, 448)
        txtUID.Margin = New Padding(5, 6, 5, 6)
        txtUID.MaxLength = 20
        txtUID.Name = "txtUID"
        txtUID.Size = New Size(174, 31)
        txtUID.TabIndex = 10
        ' 
        ' txtStart
        ' 
        txtStart.Location = New Point(20, 448)
        txtStart.Margin = New Padding(5, 6, 5, 6)
        txtStart.Mask = "00/00/0000"
        txtStart.Name = "txtStart"
        txtStart.Size = New Size(121, 31)
        txtStart.TabIndex = 8
        txtStart.ValidatingType = GetType(Date)
        ' 
        ' txtEnd
        ' 
        txtEnd.Location = New Point(153, 448)
        txtEnd.Margin = New Padding(5, 6, 5, 6)
        txtEnd.Mask = "00/00/0000"
        txtEnd.Name = "txtEnd"
        txtEnd.Size = New Size(121, 31)
        txtEnd.TabIndex = 9
        txtEnd.ValidatingType = GetType(Date)
        ' 
        ' txtPWD
        ' 
        txtPWD.Location = New Point(473, 448)
        txtPWD.Margin = New Padding(5, 6, 5, 6)
        txtPWD.MaxLength = 20
        txtPWD.Name = "txtPWD"
        txtPWD.PasswordChar = "X"c
        txtPWD.Size = New Size(116, 31)
        txtPWD.TabIndex = 11
        ' 
        ' txtAdd1
        ' 
        txtAdd1.Location = New Point(18, 535)
        txtAdd1.Margin = New Padding(5, 6, 5, 6)
        txtAdd1.MaxLength = 35
        txtAdd1.Name = "txtAdd1"
        txtAdd1.Size = New Size(176, 31)
        txtAdd1.TabIndex = 13
        ' 
        ' txtAdd2
        ' 
        txtAdd2.Location = New Point(207, 535)
        txtAdd2.Margin = New Padding(5, 6, 5, 6)
        txtAdd2.MaxLength = 35
        txtAdd2.Name = "txtAdd2"
        txtAdd2.Size = New Size(156, 31)
        txtAdd2.TabIndex = 14
        ' 
        ' txtCity
        ' 
        txtCity.Location = New Point(375, 535)
        txtCity.Margin = New Padding(5, 6, 5, 6)
        txtCity.MaxLength = 35
        txtCity.Name = "txtCity"
        txtCity.Size = New Size(167, 31)
        txtCity.TabIndex = 15
        ' 
        ' txtState
        ' 
        txtState.Location = New Point(555, 535)
        txtState.Margin = New Padding(5, 6, 5, 6)
        txtState.MaxLength = 35
        txtState.Name = "txtState"
        txtState.Size = New Size(121, 31)
        txtState.TabIndex = 16
        ' 
        ' txtZip
        ' 
        txtZip.Location = New Point(688, 535)
        txtZip.Margin = New Padding(5, 6, 5, 6)
        txtZip.MaxLength = 25
        txtZip.Name = "txtZip"
        txtZip.Size = New Size(147, 31)
        txtZip.TabIndex = 17
        ' 
        ' txtCountry
        ' 
        txtCountry.Location = New Point(848, 535)
        txtCountry.Margin = New Padding(5, 6, 5, 6)
        txtCountry.MaxLength = 35
        txtCountry.Name = "txtCountry"
        txtCountry.Size = New Size(142, 31)
        txtCountry.TabIndex = 18
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(162, 331)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(280, 25)
        Label2.TabIndex = 41
        Label2.Text = "Name (Last, First M - if individual) "
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(482, 331)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(82, 25)
        Label3.TabIndex = 42
        Label3.Text = "SSN"
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(613, 333)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(127, 25)
        Label4.TabIndex = 43
        Label4.Text = "Home Phone"
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.DarkBlue
        Label5.Location = New Point(913, 331)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(80, 25)
        Label5.TabIndex = 44
        Label5.Text = "Active?"
        Label5.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.DarkBlue
        Label6.Location = New Point(767, 333)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(107, 25)
        Label6.TabIndex = 45
        Label6.Text = "Cell Phone"
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.DarkBlue
        Label7.Location = New Point(15, 417)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(107, 25)
        Label7.TabIndex = 46
        Label7.Text = "Start Date"
        ' 
        ' Label8
        ' 
        Label8.ForeColor = Color.DarkBlue
        Label8.Location = New Point(148, 417)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(107, 25)
        Label8.TabIndex = 47
        Label8.Text = "End Date"
        ' 
        ' Label9
        ' 
        Label9.ForeColor = Color.Magenta
        Label9.Location = New Point(305, 417)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(137, 25)
        Label9.TabIndex = 48
        Label9.Text = "User Name"
        ' 
        ' Label10
        ' 
        Label10.ForeColor = Color.Magenta
        Label10.Location = New Point(482, 417)
        Label10.Margin = New Padding(5, 0, 5, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(92, 25)
        Label10.TabIndex = 49
        Label10.Text = "Password"
        ' 
        ' Label11
        ' 
        Label11.ForeColor = Color.DarkBlue
        Label11.Location = New Point(37, 504)
        Label11.Margin = New Padding(5, 0, 5, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(195, 25)
        Label11.TabIndex = 50
        Label11.Text = "Address 1"
        ' 
        ' Label12
        ' 
        Label12.ForeColor = Color.DarkBlue
        Label12.Location = New Point(220, 504)
        Label12.Margin = New Padding(5, 0, 5, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(107, 25)
        Label12.TabIndex = 51
        Label12.Text = "Address 2"
        ' 
        ' Label13
        ' 
        Label13.ForeColor = Color.DarkBlue
        Label13.Location = New Point(397, 504)
        Label13.Margin = New Padding(5, 0, 5, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(88, 25)
        Label13.TabIndex = 52
        Label13.Text = "City"
        ' 
        ' Label14
        ' 
        Label14.ForeColor = Color.DarkBlue
        Label14.Location = New Point(550, 504)
        Label14.Margin = New Padding(5, 0, 5, 0)
        Label14.Name = "Label14"
        Label14.Size = New Size(137, 25)
        Label14.TabIndex = 53
        Label14.Text = "State/Province"
        ' 
        ' Label15
        ' 
        Label15.ForeColor = Color.DarkBlue
        Label15.Location = New Point(707, 504)
        Label15.Margin = New Padding(5, 0, 5, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(132, 25)
        Label15.TabIndex = 54
        Label15.Text = "Postal Zip"
        ' 
        ' Label16
        ' 
        Label16.ForeColor = Color.DarkBlue
        Label16.Location = New Point(865, 504)
        Label16.Margin = New Padding(5, 0, 5, 0)
        Label16.Name = "Label16"
        Label16.Size = New Size(92, 25)
        Label16.TabIndex = 55
        Label16.Text = "Country"
        ' 
        ' txtHPhone
        ' 
        txtHPhone.Location = New Point(602, 363)
        txtHPhone.Margin = New Padding(5, 6, 5, 6)
        txtHPhone.Mask = "(999) 000-0000"
        txtHPhone.Name = "txtHPhone"
        txtHPhone.Size = New Size(136, 31)
        txtHPhone.TabIndex = 56
        ' 
        ' txtCell
        ' 
        txtCell.Location = New Point(753, 363)
        txtCell.Margin = New Padding(5, 6, 5, 6)
        txtCell.Mask = "(999) 000-0000"
        txtCell.Name = "txtCell"
        txtCell.Size = New Size(147, 31)
        txtCell.TabIndex = 57
        ' 
        ' txtEmail
        ' 
        txtEmail.Location = New Point(602, 448)
        txtEmail.Margin = New Padding(5, 6, 5, 6)
        txtEmail.MaxLength = 35
        txtEmail.Name = "txtEmail"
        txtEmail.Size = New Size(389, 31)
        txtEmail.TabIndex = 19
        ' 
        ' lblEmail
        ' 
        lblEmail.ForeColor = Color.DarkBlue
        lblEmail.Location = New Point(618, 417)
        lblEmail.Margin = New Padding(5, 0, 5, 0)
        lblEmail.Name = "lblEmail"
        lblEmail.Size = New Size(107, 25)
        lblEmail.TabIndex = 59
        lblEmail.Text = "Email"
        ' 
        ' dgvClients
        ' 
        dgvClients.AllowUserToAddRows = False
        dgvClients.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = Color.AliceBlue
        dgvClients.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        dgvClients.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvClients.Columns.AddRange(New DataGridViewColumn() {ClientID, Lookup, Client_Name, ClientPhone, ClientAddress})
        dgvClients.Location = New Point(18, 637)
        dgvClients.Margin = New Padding(5, 6, 5, 6)
        dgvClients.Name = "dgvClients"
        dgvClients.RowHeadersVisible = False
        dgvClients.RowHeadersWidth = 62
        dgvClients.SelectionMode = DataGridViewSelectionMode.CellSelect
        dgvClients.Size = New Size(975, 269)
        dgvClients.TabIndex = 60
        ' 
        ' ClientID
        ' 
        ClientID.FillWeight = 80F
        ClientID.HeaderText = "Client ID"
        ClientID.MaxInputLength = 12
        ClientID.MinimumWidth = 8
        ClientID.Name = "ClientID"
        ClientID.Width = 139
        ' 
        ' Lookup
        ' 
        Lookup.FillWeight = 30F
        Lookup.HeaderText = ""
        Lookup.Image = CType(resources.GetObject("Lookup.Image"), Image)
        Lookup.MinimumWidth = 8
        Lookup.Name = "Lookup"
        Lookup.Resizable = DataGridViewTriState.True
        Lookup.Width = 52
        ' 
        ' Client_Name
        ' 
        Client_Name.FillWeight = 160F
        Client_Name.HeaderText = "Client Name"
        Client_Name.MinimumWidth = 8
        Client_Name.Name = "Client_Name"
        Client_Name.ReadOnly = True
        Client_Name.Resizable = DataGridViewTriState.True
        Client_Name.Width = 278
        ' 
        ' ClientPhone
        ' 
        ClientPhone.FillWeight = 90F
        ClientPhone.HeaderText = "Phone"
        ClientPhone.MinimumWidth = 8
        ClientPhone.Name = "ClientPhone"
        ClientPhone.ReadOnly = True
        ClientPhone.Resizable = DataGridViewTriState.True
        ClientPhone.SortMode = DataGridViewColumnSortMode.NotSortable
        ClientPhone.Width = 156
        ' 
        ' ClientAddress
        ' 
        ClientAddress.FillWeight = 200F
        ClientAddress.HeaderText = "Address"
        ClientAddress.MinimumWidth = 8
        ClientAddress.Name = "ClientAddress"
        ClientAddress.ReadOnly = True
        ClientAddress.Resizable = DataGridViewTriState.True
        ClientAddress.SortMode = DataGridViewColumnSortMode.NotSortable
        ClientAddress.Width = 347
        ' 
        ' lblClients
        ' 
        lblClients.ForeColor = Color.DarkBlue
        lblClients.Location = New Point(37, 602)
        lblClients.Margin = New Padding(5, 0, 5, 0)
        lblClients.Name = "lblClients"
        lblClients.Size = New Size(122, 25)
        lblClients.TabIndex = 61
        lblClients.Text = "Clients [ 0 ]"
        ' 
        ' frmSalesPersons
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1013, 929)
        Controls.Add(lblClients)
        Controls.Add(dgvClients)
        Controls.Add(lblEmail)
        Controls.Add(txtEmail)
        Controls.Add(txtCell)
        Controls.Add(txtHPhone)
        Controls.Add(Label16)
        Controls.Add(Label15)
        Controls.Add(Label14)
        Controls.Add(Label13)
        Controls.Add(Label12)
        Controls.Add(Label11)
        Controls.Add(Label10)
        Controls.Add(Label9)
        Controls.Add(Label8)
        Controls.Add(Label7)
        Controls.Add(Label6)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(txtCountry)
        Controls.Add(txtZip)
        Controls.Add(txtState)
        Controls.Add(txtCity)
        Controls.Add(txtAdd2)
        Controls.Add(txtAdd1)
        Controls.Add(txtPWD)
        Controls.Add(txtEnd)
        Controls.Add(txtStart)
        Controls.Add(txtUID)
        Controls.Add(txtSSN)
        Controls.Add(txtID)
        Controls.Add(txtName)
        Controls.Add(Label1)
        Controls.Add(chkActive)
        Controls.Add(dgvSales)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmSalesPersons"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Sales Person Management"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgvSales, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvClients, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

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
    Friend WithEvents dgvSales As System.Windows.Forms.DataGridView
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents txtSSN As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtUID As System.Windows.Forms.TextBox
    Friend WithEvents txtStart As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtEnd As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtPWD As System.Windows.Forms.TextBox
    Friend WithEvents txtAdd1 As System.Windows.Forms.TextBox
    Friend WithEvents txtAdd2 As System.Windows.Forms.TextBox
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents txtState As System.Windows.Forms.TextBox
    Friend WithEvents txtZip As System.Windows.Forms.TextBox
    Friend WithEvents txtCountry As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtHPhone As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtCell As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents lblEmail As System.Windows.Forms.Label
    Friend WithEvents SrcID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FullName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UoM As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Email As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Address_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Address As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvClients As System.Windows.Forms.DataGridView
    Friend WithEvents lblClients As System.Windows.Forms.Label
    Friend WithEvents ClientID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Lookup As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents Client_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ClientPhone As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ClientAddress As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
