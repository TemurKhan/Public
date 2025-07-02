<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmQuickQuote
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmQuickQuote))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        txtPatHPhone = New MaskedTextBox()
        Label100 = New Label()
        Label99 = New Label()
        Label98 = New Label()
        txtPatCountry = New TextBox()
        txtPatZip = New TextBox()
        txtPatState = New TextBox()
        Label97 = New Label()
        txtPatAdr2 = New TextBox()
        btnRemPat = New Button()
        Label36 = New Label()
        Label34 = New Label()
        Label35 = New Label()
        txtPatientID = New TextBox()
        Label30 = New Label()
        txtDOB = New MaskedTextBox()
        btnPatLook = New Button()
        cmbSex = New ComboBox()
        txtLName = New TextBox()
        Label33 = New Label()
        Label29 = New Label()
        txtPatAdr1 = New TextBox()
        Label32 = New Label()
        Label28 = New Label()
        txtSSN = New MaskedTextBox()
        txtPatCity = New TextBox()
        Label31 = New Label()
        Label27 = New Label()
        txtMName = New TextBox()
        txtPatEmail = New TextBox()
        txtFName = New TextBox()
        Label26 = New Label()
        ToolStrip1 = New ToolStrip()
        chkNewEdit = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnSave = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnPrint = New ToolStripButton()
        ToolStripSeparator5 = New ToolStripSeparator()
        btnDelete = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        gbPatient = New GroupBox()
        txtQuoteID = New TextBox()
        Label1 = New Label()
        dtpDate = New DateTimePicker()
        Label2 = New Label()
        btnQuoteLookUp = New Button()
        btnProvLookUp = New Button()
        Label3 = New Label()
        txtProviderID = New TextBox()
        txtNPI = New TextBox()
        Label4 = New Label()
        txtProvLName = New TextBox()
        txtProvFName = New TextBox()
        Label5 = New Label()
        Label6 = New Label()
        dgvCharges = New DataGridView()
        DEL = New DataGridViewImageColumn()
        TGPID = New DataGridViewTextBoxColumn()
        tgpLook = New DataGridViewImageColumn()
        TGPName = New DataGridViewTextBoxColumn()
        CPT = New DataGridViewTextBoxColumn()
        Price = New DataGridViewTextBoxColumn()
        Unit = New DataGridViewTextBoxColumn()
        Extend = New DataGridViewTextBoxColumn()
        txtTotal = New TextBox()
        Label7 = New Label()
        txtPayment = New TextBox()
        btnPament = New Button()
        txtBalance = New TextBox()
        Label8 = New Label()
        txtDiscount = New TextBox()
        Label9 = New Label()
        txtPaymentID = New TextBox()
        txtPaymentType = New TextBox()
        ToolStrip1.SuspendLayout()
        gbPatient.SuspendLayout()
        CType(dgvCharges, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' txtPatHPhone
        ' 
        txtPatHPhone.Location = New Point(430, 162)
        txtPatHPhone.Margin = New Padding(5, 6, 5, 6)
        txtPatHPhone.Name = "txtPatHPhone"
        txtPatHPhone.Size = New Size(171, 31)
        txtPatHPhone.TabIndex = 79
        ' 
        ' Label100
        ' 
        Label100.ForeColor = Color.DarkBlue
        Label100.Location = New Point(780, 227)
        Label100.Margin = New Padding(5, 0, 5, 0)
        Label100.Name = "Label100"
        Label100.Size = New Size(120, 25)
        Label100.TabIndex = 96
        Label100.Text = "Country"
        ' 
        ' Label99
        ' 
        Label99.ForeColor = Color.Fuchsia
        Label99.Location = New Point(652, 227)
        Label99.Margin = New Padding(5, 0, 5, 0)
        Label99.Name = "Label99"
        Label99.Size = New Size(95, 25)
        Label99.TabIndex = 95
        Label99.Text = "Zip Code"
        ' 
        ' Label98
        ' 
        Label98.ForeColor = Color.Fuchsia
        Label98.Location = New Point(490, 227)
        Label98.Margin = New Padding(5, 0, 5, 0)
        Label98.Name = "Label98"
        Label98.Size = New Size(163, 25)
        Label98.TabIndex = 94
        Label98.Text = "State/Province"
        ' 
        ' txtPatCountry
        ' 
        txtPatCountry.BackColor = Color.White
        txtPatCountry.Location = New Point(773, 258)
        txtPatCountry.Margin = New Padding(5, 6, 5, 6)
        txtPatCountry.MaxLength = 35
        txtPatCountry.Name = "txtPatCountry"
        txtPatCountry.Size = New Size(144, 31)
        txtPatCountry.TabIndex = 78
        ' 
        ' txtPatZip
        ' 
        txtPatZip.BackColor = Color.White
        txtPatZip.Location = New Point(645, 258)
        txtPatZip.Margin = New Padding(5, 6, 5, 6)
        txtPatZip.MaxLength = 35
        txtPatZip.Name = "txtPatZip"
        txtPatZip.Size = New Size(116, 31)
        txtPatZip.TabIndex = 77
        ' 
        ' txtPatState
        ' 
        txtPatState.BackColor = Color.White
        txtPatState.Location = New Point(487, 258)
        txtPatState.Margin = New Padding(5, 6, 5, 6)
        txtPatState.MaxLength = 35
        txtPatState.Name = "txtPatState"
        txtPatState.Size = New Size(146, 31)
        txtPatState.TabIndex = 76
        ' 
        ' Label97
        ' 
        Label97.ForeColor = Color.DarkBlue
        Label97.Location = New Point(188, 227)
        Label97.Margin = New Padding(5, 0, 5, 0)
        Label97.Name = "Label97"
        Label97.Size = New Size(110, 25)
        Label97.TabIndex = 93
        Label97.Text = "Address 2"
        ' 
        ' txtPatAdr2
        ' 
        txtPatAdr2.BackColor = Color.White
        txtPatAdr2.Location = New Point(173, 258)
        txtPatAdr2.Margin = New Padding(5, 6, 5, 6)
        txtPatAdr2.MaxLength = 35
        txtPatAdr2.Name = "txtPatAdr2"
        txtPatAdr2.Size = New Size(144, 31)
        txtPatAdr2.TabIndex = 74
        ' 
        ' btnRemPat
        ' 
        btnRemPat.Enabled = False
        btnRemPat.Image = CType(resources.GetObject("btnRemPat.Image"), Image)
        btnRemPat.Location = New Point(17, 54)
        btnRemPat.Margin = New Padding(5, 6, 5, 6)
        btnRemPat.Name = "btnRemPat"
        btnRemPat.Size = New Size(50, 50)
        btnRemPat.TabIndex = 98
        btnRemPat.TabStop = False
        btnRemPat.UseVisualStyleBackColor = True
        ' 
        ' Label36
        ' 
        Label36.ForeColor = Color.Red
        Label36.Location = New Point(160, 129)
        Label36.Margin = New Padding(5, 0, 5, 0)
        Label36.Name = "Label36"
        Label36.Size = New Size(123, 25)
        Label36.TabIndex = 92
        Label36.Text = "DOB"
        ' 
        ' Label34
        ' 
        Label34.ForeColor = Color.DarkBlue
        Label34.Location = New Point(72, 31)
        Label34.Margin = New Padding(5, 0, 5, 0)
        Label34.Name = "Label34"
        Label34.Size = New Size(132, 25)
        Label34.TabIndex = 90
        Label34.Text = "Patient ID"
        ' 
        ' Label35
        ' 
        Label35.ForeColor = Color.Red
        Label35.Location = New Point(25, 129)
        Label35.Margin = New Padding(5, 0, 5, 0)
        Label35.Name = "Label35"
        Label35.Size = New Size(118, 25)
        Label35.TabIndex = 91
        Label35.Text = "Gender"
        ' 
        ' txtPatientID
        ' 
        txtPatientID.BackColor = Color.Ivory
        txtPatientID.Location = New Point(77, 62)
        txtPatientID.Margin = New Padding(5, 6, 5, 6)
        txtPatientID.MaxLength = 12
        txtPatientID.Name = "txtPatientID"
        txtPatientID.ReadOnly = True
        txtPatientID.Size = New Size(136, 31)
        txtPatientID.TabIndex = 64
        txtPatientID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label30
        ' 
        Label30.ForeColor = Color.Red
        Label30.Location = New Point(545, 29)
        Label30.Margin = New Padding(5, 0, 5, 0)
        Label30.Name = "Label30"
        Label30.Size = New Size(145, 25)
        Label30.TabIndex = 82
        Label30.Text = "First Name"
        ' 
        ' txtDOB
        ' 
        txtDOB.Location = New Point(153, 162)
        txtDOB.Margin = New Padding(5, 6, 5, 6)
        txtDOB.Mask = "00/00/0000"
        txtDOB.Name = "txtDOB"
        txtDOB.Size = New Size(127, 31)
        txtDOB.TabIndex = 70
        txtDOB.ValidatingType = GetType(Date)
        ' 
        ' btnPatLook
        ' 
        btnPatLook.Image = CType(resources.GetObject("btnPatLook.Image"), Image)
        btnPatLook.Location = New Point(232, 54)
        btnPatLook.Margin = New Padding(5, 6, 5, 6)
        btnPatLook.Name = "btnPatLook"
        btnPatLook.Size = New Size(50, 50)
        btnPatLook.TabIndex = 65
        btnPatLook.TabStop = False
        btnPatLook.UseVisualStyleBackColor = True
        ' 
        ' cmbSex
        ' 
        cmbSex.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSex.FormattingEnabled = True
        cmbSex.Items.AddRange(New Object() {"Female", "Male", "Indetermined", "Unreported"})
        cmbSex.Location = New Point(17, 162)
        cmbSex.Margin = New Padding(5, 6, 5, 6)
        cmbSex.Name = "cmbSex"
        cmbSex.Size = New Size(124, 33)
        cmbSex.TabIndex = 69
        ' 
        ' txtLName
        ' 
        txtLName.BackColor = Color.White
        txtLName.Location = New Point(292, 62)
        txtLName.Margin = New Padding(5, 6, 5, 6)
        txtLName.MaxLength = 35
        txtLName.Name = "txtLName"
        txtLName.Size = New Size(224, 31)
        txtLName.TabIndex = 66
        ' 
        ' Label33
        ' 
        Label33.Location = New Point(432, 131)
        Label33.Margin = New Padding(5, 0, 5, 0)
        Label33.Name = "Label33"
        Label33.Size = New Size(130, 25)
        Label33.TabIndex = 89
        Label33.Text = "Home Phone"
        ' 
        ' Label29
        ' 
        Label29.ForeColor = Color.Red
        Label29.Location = New Point(320, 31)
        Label29.Margin = New Padding(5, 0, 5, 0)
        Label29.Name = "Label29"
        Label29.Size = New Size(158, 25)
        Label29.TabIndex = 83
        Label29.Text = "Last Name"
        ' 
        ' txtPatAdr1
        ' 
        txtPatAdr1.BackColor = Color.White
        txtPatAdr1.Location = New Point(17, 258)
        txtPatAdr1.Margin = New Padding(5, 6, 5, 6)
        txtPatAdr1.MaxLength = 35
        txtPatAdr1.Name = "txtPatAdr1"
        txtPatAdr1.Size = New Size(144, 31)
        txtPatAdr1.TabIndex = 73
        ' 
        ' Label32
        ' 
        Label32.Location = New Point(300, 129)
        Label32.Margin = New Padding(5, 0, 5, 0)
        Label32.Name = "Label32"
        Label32.Size = New Size(115, 25)
        Label32.TabIndex = 88
        Label32.Text = "SSN"
        ' 
        ' Label28
        ' 
        Label28.ForeColor = Color.Fuchsia
        Label28.Location = New Point(18, 227)
        Label28.Margin = New Padding(5, 0, 5, 0)
        Label28.Name = "Label28"
        Label28.Size = New Size(145, 25)
        Label28.TabIndex = 84
        Label28.Text = "Address 1"
        ' 
        ' txtSSN
        ' 
        txtSSN.Location = New Point(292, 162)
        txtSSN.Margin = New Padding(5, 6, 5, 6)
        txtSSN.Mask = "000-00-0000"
        txtSSN.Name = "txtSSN"
        txtSSN.Size = New Size(126, 31)
        txtSSN.TabIndex = 71
        ' 
        ' txtPatCity
        ' 
        txtPatCity.BackColor = Color.White
        txtPatCity.Location = New Point(330, 258)
        txtPatCity.Margin = New Padding(5, 6, 5, 6)
        txtPatCity.MaxLength = 35
        txtPatCity.Name = "txtPatCity"
        txtPatCity.Size = New Size(144, 31)
        txtPatCity.TabIndex = 75
        ' 
        ' Label31
        ' 
        Label31.Location = New Point(757, 29)
        Label31.Margin = New Padding(5, 0, 5, 0)
        Label31.Name = "Label31"
        Label31.Size = New Size(120, 25)
        Label31.TabIndex = 87
        Label31.Text = "Middle Name"
        ' 
        ' Label27
        ' 
        Label27.ForeColor = Color.Fuchsia
        Label27.Location = New Point(343, 227)
        Label27.Margin = New Padding(5, 0, 5, 0)
        Label27.Name = "Label27"
        Label27.Size = New Size(98, 25)
        Label27.TabIndex = 85
        Label27.Text = "City"
        ' 
        ' txtMName
        ' 
        txtMName.BackColor = Color.White
        txtMName.Location = New Point(755, 62)
        txtMName.Margin = New Padding(5, 6, 5, 6)
        txtMName.MaxLength = 35
        txtMName.Name = "txtMName"
        txtMName.Size = New Size(162, 31)
        txtMName.TabIndex = 68
        ' 
        ' txtPatEmail
        ' 
        txtPatEmail.BackColor = Color.White
        txtPatEmail.Location = New Point(613, 162)
        txtPatEmail.Margin = New Padding(5, 6, 5, 6)
        txtPatEmail.MaxLength = 35
        txtPatEmail.Name = "txtPatEmail"
        txtPatEmail.Size = New Size(304, 31)
        txtPatEmail.TabIndex = 80
        ' 
        ' txtFName
        ' 
        txtFName.BackColor = Color.White
        txtFName.Location = New Point(528, 62)
        txtFName.Margin = New Padding(5, 6, 5, 6)
        txtFName.MaxLength = 35
        txtFName.Name = "txtFName"
        txtFName.Size = New Size(214, 31)
        txtFName.TabIndex = 67
        ' 
        ' Label26
        ' 
        Label26.Location = New Point(613, 131)
        Label26.Margin = New Padding(5, 0, 5, 0)
        Label26.Name = "Label26"
        Label26.Size = New Size(198, 25)
        Label26.TabIndex = 86
        Label26.Text = "Email"
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {chkNewEdit, ToolStripSeparator1, btnSave, ToolStripSeparator2, btnPrint, ToolStripSeparator5, btnDelete, ToolStripSeparator3, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(1355, 34)
        ToolStrip1.TabIndex = 99
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' chkNewEdit
        ' 
        chkNewEdit.CheckOnClick = True
        chkNewEdit.Image = CType(resources.GetObject("chkNewEdit.Image"), Image)
        chkNewEdit.ImageTransparentColor = Color.Magenta
        chkNewEdit.Name = "chkNewEdit"
        chkNewEdit.Size = New Size(75, 29)
        chkNewEdit.Text = "New"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
        ' 
        ' btnSave
        ' 
        btnSave.Enabled = False
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
        ' btnPrint
        ' 
        btnPrint.Enabled = False
        btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), Image)
        btnPrint.ImageTransparentColor = Color.Magenta
        btnPrint.Name = "btnPrint"
        btnPrint.Size = New Size(76, 29)
        btnPrint.Text = "Print"
        ' 
        ' ToolStripSeparator5
        ' 
        ToolStripSeparator5.Name = "ToolStripSeparator5"
        ToolStripSeparator5.Size = New Size(6, 34)
        ' 
        ' btnDelete
        ' 
        btnDelete.Enabled = False
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
        ' gbPatient
        ' 
        gbPatient.BackColor = Color.MintCream
        gbPatient.Controls.Add(txtPatientID)
        gbPatient.Controls.Add(Label26)
        gbPatient.Controls.Add(txtPatHPhone)
        gbPatient.Controls.Add(txtFName)
        gbPatient.Controls.Add(txtPatEmail)
        gbPatient.Controls.Add(txtMName)
        gbPatient.Controls.Add(Label27)
        gbPatient.Controls.Add(Label100)
        gbPatient.Controls.Add(Label31)
        gbPatient.Controls.Add(Label99)
        gbPatient.Controls.Add(txtPatCity)
        gbPatient.Controls.Add(Label98)
        gbPatient.Controls.Add(txtSSN)
        gbPatient.Controls.Add(txtPatCountry)
        gbPatient.Controls.Add(Label28)
        gbPatient.Controls.Add(txtPatZip)
        gbPatient.Controls.Add(Label32)
        gbPatient.Controls.Add(txtPatState)
        gbPatient.Controls.Add(txtPatAdr1)
        gbPatient.Controls.Add(Label97)
        gbPatient.Controls.Add(Label29)
        gbPatient.Controls.Add(txtPatAdr2)
        gbPatient.Controls.Add(Label33)
        gbPatient.Controls.Add(btnRemPat)
        gbPatient.Controls.Add(txtLName)
        gbPatient.Controls.Add(Label36)
        gbPatient.Controls.Add(cmbSex)
        gbPatient.Controls.Add(Label34)
        gbPatient.Controls.Add(btnPatLook)
        gbPatient.Controls.Add(Label35)
        gbPatient.Controls.Add(txtDOB)
        gbPatient.Controls.Add(Label30)
        gbPatient.Location = New Point(388, 92)
        gbPatient.Margin = New Padding(5, 6, 5, 6)
        gbPatient.Name = "gbPatient"
        gbPatient.Padding = New Padding(5, 6, 5, 6)
        gbPatient.Size = New Size(947, 327)
        gbPatient.TabIndex = 100
        gbPatient.TabStop = False
        ' 
        ' txtQuoteID
        ' 
        txtQuoteID.BackColor = Color.Ivory
        txtQuoteID.Location = New Point(20, 156)
        txtQuoteID.Margin = New Padding(5, 6, 5, 6)
        txtQuoteID.MaxLength = 12
        txtQuoteID.Name = "txtQuoteID"
        txtQuoteID.Size = New Size(136, 31)
        txtQuoteID.TabIndex = 102
        txtQuoteID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(27, 123)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(113, 25)
        Label1.TabIndex = 103
        Label1.Text = "Quote ID"
        ' 
        ' dtpDate
        ' 
        dtpDate.Format = DateTimePickerFormat.Short
        dtpDate.Location = New Point(225, 156)
        dtpDate.Margin = New Padding(5, 6, 5, 6)
        dtpDate.Name = "dtpDate"
        dtpDate.Size = New Size(149, 31)
        dtpDate.TabIndex = 104
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(225, 123)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(113, 25)
        Label2.TabIndex = 105
        Label2.Text = "Dated"
        ' 
        ' btnQuoteLookUp
        ' 
        btnQuoteLookUp.Image = CType(resources.GetObject("btnQuoteLookUp.Image"), Image)
        btnQuoteLookUp.Location = New Point(165, 148)
        btnQuoteLookUp.Margin = New Padding(5, 6, 5, 6)
        btnQuoteLookUp.Name = "btnQuoteLookUp"
        btnQuoteLookUp.Size = New Size(50, 50)
        btnQuoteLookUp.TabIndex = 106
        btnQuoteLookUp.TabStop = False
        btnQuoteLookUp.UseVisualStyleBackColor = True
        ' 
        ' btnProvLookUp
        ' 
        btnProvLookUp.Image = CType(resources.GetObject("btnProvLookUp.Image"), Image)
        btnProvLookUp.Location = New Point(165, 246)
        btnProvLookUp.Margin = New Padding(5, 6, 5, 6)
        btnProvLookUp.Name = "btnProvLookUp"
        btnProvLookUp.Size = New Size(50, 50)
        btnProvLookUp.TabIndex = 109
        btnProvLookUp.TabStop = False
        btnProvLookUp.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(27, 221)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(113, 25)
        Label3.TabIndex = 108
        Label3.Text = "Provider ID"
        ' 
        ' txtProviderID
        ' 
        txtProviderID.BackColor = Color.Ivory
        txtProviderID.Location = New Point(20, 254)
        txtProviderID.Margin = New Padding(5, 6, 5, 6)
        txtProviderID.MaxLength = 12
        txtProviderID.Name = "txtProviderID"
        txtProviderID.ReadOnly = True
        txtProviderID.Size = New Size(136, 31)
        txtProviderID.TabIndex = 107
        txtProviderID.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtNPI
        ' 
        txtNPI.BackColor = Color.Ivory
        txtNPI.Location = New Point(225, 254)
        txtNPI.Margin = New Padding(5, 6, 5, 6)
        txtNPI.MaxLength = 12
        txtNPI.Name = "txtNPI"
        txtNPI.ReadOnly = True
        txtNPI.Size = New Size(149, 31)
        txtNPI.TabIndex = 110
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.Fuchsia
        Label4.Location = New Point(240, 221)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(113, 25)
        Label4.TabIndex = 111
        Label4.Text = "NPI"
        ' 
        ' txtProvLName
        ' 
        txtProvLName.BackColor = Color.Ivory
        txtProvLName.Location = New Point(20, 350)
        txtProvLName.Margin = New Padding(5, 6, 5, 6)
        txtProvLName.MaxLength = 12
        txtProvLName.Name = "txtProvLName"
        txtProvLName.ReadOnly = True
        txtProvLName.Size = New Size(171, 31)
        txtProvLName.TabIndex = 112
        ' 
        ' txtProvFName
        ' 
        txtProvFName.BackColor = Color.Ivory
        txtProvFName.Location = New Point(203, 350)
        txtProvFName.Margin = New Padding(5, 6, 5, 6)
        txtProvFName.MaxLength = 12
        txtProvFName.Name = "txtProvFName"
        txtProvFName.ReadOnly = True
        txtProvFName.Size = New Size(171, 31)
        txtProvFName.TabIndex = 113
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.Fuchsia
        Label5.Location = New Point(27, 319)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(167, 25)
        Label5.TabIndex = 114
        Label5.Text = "Last Name (Prov)"
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.Fuchsia
        Label6.Location = New Point(220, 319)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(157, 25)
        Label6.TabIndex = 115
        Label6.Text = "First Name (Prov)"
        ' 
        ' dgvCharges
        ' 
        dgvCharges.AllowUserToAddRows = False
        dgvCharges.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.AliceBlue
        dgvCharges.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvCharges.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvCharges.Columns.AddRange(New DataGridViewColumn() {DEL, TGPID, tgpLook, TGPName, CPT, Price, Unit, Extend})
        dgvCharges.Location = New Point(20, 452)
        dgvCharges.Margin = New Padding(5, 6, 5, 6)
        dgvCharges.Name = "dgvCharges"
        dgvCharges.RowHeadersVisible = False
        dgvCharges.RowHeadersWidth = 62
        DataGridViewCellStyle2.BackColor = Color.Linen
        dgvCharges.RowsDefaultCellStyle = DataGridViewCellStyle2
        dgvCharges.Size = New Size(1315, 431)
        dgvCharges.TabIndex = 116
        ' 
        ' DEL
        ' 
        DEL.FillWeight = 40F
        DEL.HeaderText = ""
        DEL.Image = CType(resources.GetObject("DEL.Image"), Image)
        DEL.MinimumWidth = 8
        DEL.Name = "DEL"
        DEL.ReadOnly = True
        DEL.Width = 69
        ' 
        ' TGPID
        ' 
        TGPID.FillWeight = 90F
        TGPID.HeaderText = "ID"
        TGPID.MaxInputLength = 5
        TGPID.MinimumWidth = 8
        TGPID.Name = "TGPID"
        TGPID.SortMode = DataGridViewColumnSortMode.NotSortable
        TGPID.Width = 155
        ' 
        ' tgpLook
        ' 
        tgpLook.FillWeight = 40F
        tgpLook.HeaderText = ""
        tgpLook.Image = CType(resources.GetObject("tgpLook.Image"), Image)
        tgpLook.MinimumWidth = 8
        tgpLook.Name = "tgpLook"
        tgpLook.ReadOnly = True
        tgpLook.Width = 69
        ' 
        ' TGPName
        ' 
        TGPName.FillWeight = 260F
        TGPName.HeaderText = "Name"
        TGPName.MaxInputLength = 100
        TGPName.MinimumWidth = 8
        TGPName.Name = "TGPName"
        TGPName.ReadOnly = True
        TGPName.SortMode = DataGridViewColumnSortMode.NotSortable
        TGPName.Width = 449
        ' 
        ' CPT
        ' 
        CPT.FillWeight = 90F
        CPT.HeaderText = "CPT"
        CPT.MinimumWidth = 8
        CPT.Name = "CPT"
        CPT.SortMode = DataGridViewColumnSortMode.NotSortable
        CPT.Width = 156
        ' 
        ' Price
        ' 
        Price.FillWeight = 90F
        Price.HeaderText = "Price"
        Price.MaxInputLength = 8
        Price.MinimumWidth = 8
        Price.Name = "Price"
        Price.SortMode = DataGridViewColumnSortMode.NotSortable
        Price.Width = 155
        ' 
        ' Unit
        ' 
        Unit.FillWeight = 60F
        Unit.HeaderText = "Unit"
        Unit.MaxInputLength = 4
        Unit.MinimumWidth = 8
        Unit.Name = "Unit"
        Unit.SortMode = DataGridViewColumnSortMode.NotSortable
        Unit.Width = 104
        ' 
        ' Extend
        ' 
        Extend.FillWeight = 90F
        Extend.HeaderText = "Extend"
        Extend.MinimumWidth = 8
        Extend.Name = "Extend"
        Extend.ReadOnly = True
        Extend.SortMode = DataGridViewColumnSortMode.NotSortable
        Extend.Width = 155
        ' 
        ' txtTotal
        ' 
        txtTotal.BackColor = Color.Ivory
        txtTotal.Location = New Point(1143, 917)
        txtTotal.Margin = New Padding(5, 6, 5, 6)
        txtTotal.MaxLength = 12
        txtTotal.Name = "txtTotal"
        txtTotal.ReadOnly = True
        txtTotal.Size = New Size(142, 31)
        txtTotal.TabIndex = 117
        txtTotal.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.DarkBlue
        Label7.Location = New Point(1002, 919)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(133, 33)
        Label7.TabIndex = 118
        Label7.Text = "Total: "
        Label7.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' txtPayment
        ' 
        txtPayment.BackColor = Color.Ivory
        txtPayment.Location = New Point(1143, 967)
        txtPayment.Margin = New Padding(5, 6, 5, 6)
        txtPayment.MaxLength = 12
        txtPayment.Name = "txtPayment"
        txtPayment.ReadOnly = True
        txtPayment.Size = New Size(142, 31)
        txtPayment.TabIndex = 119
        txtPayment.TextAlign = HorizontalAlignment.Right
        ' 
        ' btnPament
        ' 
        btnPament.Location = New Point(1007, 962)
        btnPament.Margin = New Padding(5, 6, 5, 6)
        btnPament.Name = "btnPament"
        btnPament.Size = New Size(128, 48)
        btnPament.TabIndex = 120
        btnPament.Text = "Payment"
        btnPament.TextAlign = ContentAlignment.MiddleRight
        btnPament.UseVisualStyleBackColor = True
        ' 
        ' txtBalance
        ' 
        txtBalance.BackColor = Color.Ivory
        txtBalance.Location = New Point(1143, 1017)
        txtBalance.Margin = New Padding(5, 6, 5, 6)
        txtBalance.MaxLength = 12
        txtBalance.Name = "txtBalance"
        txtBalance.ReadOnly = True
        txtBalance.Size = New Size(142, 31)
        txtBalance.TabIndex = 121
        txtBalance.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label8
        ' 
        Label8.ForeColor = Color.DarkBlue
        Label8.Location = New Point(1002, 1019)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(133, 33)
        Label8.TabIndex = 122
        Label8.Text = "Total: "
        Label8.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' txtDiscount
        ' 
        txtDiscount.BackColor = Color.Ivory
        txtDiscount.Location = New Point(20, 967)
        txtDiscount.Margin = New Padding(5, 6, 5, 6)
        txtDiscount.MaxLength = 5
        txtDiscount.Name = "txtDiscount"
        txtDiscount.Size = New Size(117, 31)
        txtDiscount.TabIndex = 123
        txtDiscount.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label9
        ' 
        Label9.ForeColor = Color.Red
        Label9.Location = New Point(27, 929)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(133, 33)
        Label9.TabIndex = 124
        Label9.Text = "Discount (%)"
        Label9.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' txtPaymentID
        ' 
        txtPaymentID.BackColor = Color.White
        txtPaymentID.Location = New Point(225, 917)
        txtPaymentID.Margin = New Padding(5, 6, 5, 6)
        txtPaymentID.MaxLength = 35
        txtPaymentID.Name = "txtPaymentID"
        txtPaymentID.Size = New Size(126, 31)
        txtPaymentID.TabIndex = 125
        txtPaymentID.Visible = False
        ' 
        ' txtPaymentType
        ' 
        txtPaymentType.BackColor = Color.White
        txtPaymentType.Location = New Point(367, 917)
        txtPaymentType.Margin = New Padding(5, 6, 5, 6)
        txtPaymentType.MaxLength = 35
        txtPaymentType.Name = "txtPaymentType"
        txtPaymentType.Size = New Size(122, 31)
        txtPaymentType.TabIndex = 126
        txtPaymentType.Visible = False
        ' 
        ' frmQuickQuote
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1355, 1088)
        Controls.Add(txtPaymentType)
        Controls.Add(txtPaymentID)
        Controls.Add(Label9)
        Controls.Add(txtDiscount)
        Controls.Add(Label8)
        Controls.Add(txtBalance)
        Controls.Add(btnPament)
        Controls.Add(txtPayment)
        Controls.Add(Label7)
        Controls.Add(txtTotal)
        Controls.Add(dgvCharges)
        Controls.Add(Label6)
        Controls.Add(Label5)
        Controls.Add(txtProvFName)
        Controls.Add(txtProvLName)
        Controls.Add(Label4)
        Controls.Add(txtNPI)
        Controls.Add(btnProvLookUp)
        Controls.Add(Label3)
        Controls.Add(txtProviderID)
        Controls.Add(btnQuoteLookUp)
        Controls.Add(Label2)
        Controls.Add(dtpDate)
        Controls.Add(Label1)
        Controls.Add(txtQuoteID)
        Controls.Add(gbPatient)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmQuickQuote"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Quick Quote"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        gbPatient.ResumeLayout(False)
        gbPatient.PerformLayout()
        CType(dgvCharges, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents txtPatHPhone As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label100 As System.Windows.Forms.Label
    Friend WithEvents Label99 As System.Windows.Forms.Label
    Friend WithEvents Label98 As System.Windows.Forms.Label
    Friend WithEvents txtPatCountry As System.Windows.Forms.TextBox
    Friend WithEvents txtPatZip As System.Windows.Forms.TextBox
    Friend WithEvents txtPatState As System.Windows.Forms.TextBox
    Friend WithEvents Label97 As System.Windows.Forms.Label
    Friend WithEvents txtPatAdr2 As System.Windows.Forms.TextBox
    Friend WithEvents btnRemPat As System.Windows.Forms.Button
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents txtPatientID As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtDOB As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnPatLook As System.Windows.Forms.Button
    Friend WithEvents cmbSex As System.Windows.Forms.ComboBox
    Friend WithEvents txtLName As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtPatAdr1 As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtSSN As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtPatCity As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtMName As System.Windows.Forms.TextBox
    Friend WithEvents txtPatEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtFName As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents chkNewEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents gbPatient As System.Windows.Forms.GroupBox
    Friend WithEvents txtQuoteID As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnQuoteLookUp As System.Windows.Forms.Button
    Friend WithEvents btnProvLookUp As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtProviderID As System.Windows.Forms.TextBox
    Friend WithEvents txtNPI As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtProvLName As System.Windows.Forms.TextBox
    Friend WithEvents txtProvFName As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents dgvCharges As System.Windows.Forms.DataGridView
    Friend WithEvents DEL As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents TGPID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tgpLook As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents TGPName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CPT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Price As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Extend As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtPayment As System.Windows.Forms.TextBox
    Friend WithEvents btnPament As System.Windows.Forms.Button
    Friend WithEvents txtBalance As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtDiscount As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtPaymentID As System.Windows.Forms.TextBox
    Friend WithEvents txtPaymentType As System.Windows.Forms.TextBox

End Class
