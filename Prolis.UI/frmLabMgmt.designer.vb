<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLabMgmt
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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLabMgmt))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        chkEditNew = New ToolStripButton()
        ToolStripButton2 = New ToolStripSeparator()
        btnSave = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnDelete = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        tcProvider = New TabControl()
        tpLab = New TabPage()
        Label22 = New Label()
        txtDirector = New TextBox()
        Label8 = New Label()
        Label7 = New Label()
        Label6 = New Label()
        txtSSN = New MaskedTextBox()
        txtNPI = New TextBox()
        txtCLIA = New TextBox()
        txtLabels = New TextBox()
        Label5 = New Label()
        txtFax = New MaskedTextBox()
        txtPhone = New MaskedTextBox()
        Label4 = New Label()
        btnLabelFile = New Button()
        btnDocFile = New Button()
        Label2 = New Label()
        Label1 = New Label()
        txtLabelFile = New TextBox()
        txtDocFile = New TextBox()
        chkPrimary = New CheckBox()
        txtNote = New TextBox()
        Label29 = New Label()
        txtContact = New TextBox()
        Label28 = New Label()
        Label27 = New Label()
        txtPassword = New TextBox()
        Label26 = New Label()
        txtUserName = New TextBox()
        Label25 = New Label()
        chkActive = New CheckBox()
        txtCountry = New TextBox()
        Label21 = New Label()
        txtZip = New TextBox()
        Label20 = New Label()
        txtState = New TextBox()
        Label19 = New Label()
        txtCity = New TextBox()
        Label18 = New Label()
        txtAdd2 = New TextBox()
        Label17 = New Label()
        txtAdd1 = New TextBox()
        Label16 = New Label()
        txtCommDLL = New TextBox()
        Label15 = New Label()
        txtEmail = New TextBox()
        Label14 = New Label()
        btnLabLook = New Button()
        txtLabID = New TextBox()
        Label13 = New Label()
        txtWebsite = New TextBox()
        Label12 = New Label()
        Label11 = New Label()
        Label10 = New Label()
        txtAccount = New TextBox()
        Label3 = New Label()
        txtLabName = New TextBox()
        lblLName = New Label()
        tpContract = New TabPage()
        Button1 = New Button()
        search = New TextBox()
        Label9 = New Label()
        btnMapping = New Button()
        Label31 = New Label()
        Label30 = New Label()
        dtpTo = New DateTimePicker()
        dtpFrom = New DateTimePicker()
        dgvContract = New DataGridView()
        DEL = New DataGridViewImageColumn()
        ID = New DataGridViewTextBoxColumn()
        Look = New DataGridViewImageColumn()
        Component = New DataGridViewTextBoxColumn()
        Logo = New DataGridViewImageColumn()
        LabComp = New DataGridViewTextBoxColumn()
        LabResID = New DataGridViewTextBoxColumn()
        Price = New DataGridViewTextBoxColumn()
        ToolTip1 = New ToolTip(components)
        ToolStrip1.SuspendLayout()
        tcProvider.SuspendLayout()
        tpLab.SuspendLayout()
        tpContract.SuspendLayout()
        CType(dgvContract, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(20, 20)
        ToolStrip1.Items.AddRange(New ToolStripItem() {chkEditNew, ToolStripButton2, btnSave, ToolStripSeparator1, btnDelete, ToolStripSeparator2, btnCancel, ToolStripSeparator3, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(1241, 34)
        ToolStrip1.TabIndex = 4
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' chkEditNew
        ' 
        chkEditNew.CheckOnClick = True
        chkEditNew.Image = CType(resources.GetObject("chkEditNew.Image"), Image)
        chkEditNew.ImageTransparentColor = Color.Magenta
        chkEditNew.Name = "chkEditNew"
        chkEditNew.Size = New Size(66, 29)
        chkEditNew.Text = "Edit"
        ' 
        ' ToolStripButton2
        ' 
        ToolStripButton2.Name = "ToolStripButton2"
        ToolStripButton2.Size = New Size(6, 34)
        ' 
        ' btnSave
        ' 
        btnSave.Enabled = False
        btnSave.Image = CType(resources.GetObject("btnSave.Image"), Image)
        btnSave.ImageTransparentColor = Color.Magenta
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(73, 29)
        btnSave.Text = "Save"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
        ' 
        ' btnDelete
        ' 
        btnDelete.Enabled = False
        btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), Image)
        btnDelete.ImageTransparentColor = Color.Magenta
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(86, 29)
        btnDelete.Text = "Delete"
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
        btnCancel.Size = New Size(87, 29)
        btnCancel.Text = "Cancel"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(6, 34)
        ' 
        ' btnHelp
        ' 
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.ImageTransparentColor = Color.Magenta
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(73, 29)
        btnHelp.Text = "Help"
        ' 
        ' tcProvider
        ' 
        tcProvider.Controls.Add(tpLab)
        tcProvider.Controls.Add(tpContract)
        tcProvider.Location = New Point(20, 72)
        tcProvider.Margin = New Padding(5, 6, 5, 6)
        tcProvider.Name = "tcProvider"
        tcProvider.SelectedIndex = 0
        tcProvider.Size = New Size(1201, 989)
        tcProvider.TabIndex = 5
        ' 
        ' tpLab
        ' 
        tpLab.Controls.Add(Label22)
        tpLab.Controls.Add(txtDirector)
        tpLab.Controls.Add(Label8)
        tpLab.Controls.Add(Label7)
        tpLab.Controls.Add(Label6)
        tpLab.Controls.Add(txtSSN)
        tpLab.Controls.Add(txtNPI)
        tpLab.Controls.Add(txtCLIA)
        tpLab.Controls.Add(txtLabels)
        tpLab.Controls.Add(Label5)
        tpLab.Controls.Add(txtFax)
        tpLab.Controls.Add(txtPhone)
        tpLab.Controls.Add(Label4)
        tpLab.Controls.Add(btnLabelFile)
        tpLab.Controls.Add(btnDocFile)
        tpLab.Controls.Add(Label2)
        tpLab.Controls.Add(Label1)
        tpLab.Controls.Add(txtLabelFile)
        tpLab.Controls.Add(txtDocFile)
        tpLab.Controls.Add(chkPrimary)
        tpLab.Controls.Add(txtNote)
        tpLab.Controls.Add(Label29)
        tpLab.Controls.Add(txtContact)
        tpLab.Controls.Add(Label28)
        tpLab.Controls.Add(Label27)
        tpLab.Controls.Add(txtPassword)
        tpLab.Controls.Add(Label26)
        tpLab.Controls.Add(txtUserName)
        tpLab.Controls.Add(Label25)
        tpLab.Controls.Add(chkActive)
        tpLab.Controls.Add(txtCountry)
        tpLab.Controls.Add(Label21)
        tpLab.Controls.Add(txtZip)
        tpLab.Controls.Add(Label20)
        tpLab.Controls.Add(txtState)
        tpLab.Controls.Add(Label19)
        tpLab.Controls.Add(txtCity)
        tpLab.Controls.Add(Label18)
        tpLab.Controls.Add(txtAdd2)
        tpLab.Controls.Add(Label17)
        tpLab.Controls.Add(txtAdd1)
        tpLab.Controls.Add(Label16)
        tpLab.Controls.Add(txtCommDLL)
        tpLab.Controls.Add(Label15)
        tpLab.Controls.Add(txtEmail)
        tpLab.Controls.Add(Label14)
        tpLab.Controls.Add(btnLabLook)
        tpLab.Controls.Add(txtLabID)
        tpLab.Controls.Add(Label13)
        tpLab.Controls.Add(txtWebsite)
        tpLab.Controls.Add(Label12)
        tpLab.Controls.Add(Label11)
        tpLab.Controls.Add(Label10)
        tpLab.Controls.Add(txtAccount)
        tpLab.Controls.Add(Label3)
        tpLab.Controls.Add(txtLabName)
        tpLab.Controls.Add(lblLName)
        tpLab.Location = New Point(4, 34)
        tpLab.Margin = New Padding(5, 6, 5, 6)
        tpLab.Name = "tpLab"
        tpLab.Padding = New Padding(5, 6, 5, 6)
        tpLab.Size = New Size(1193, 951)
        tpLab.TabIndex = 0
        tpLab.Text = "Laboratory"
        tpLab.UseVisualStyleBackColor = True
        ' 
        ' Label22
        ' 
        Label22.ForeColor = Color.Fuchsia
        Label22.Location = New Point(694, 581)
        Label22.Margin = New Padding(5, 0, 5, 0)
        Label22.Name = "Label22"
        Label22.Size = New Size(325, 27)
        Label22.TabIndex = 76
        Label22.Text = "Director Name (First Last, Degree)"
        ' 
        ' txtDirector
        ' 
        txtDirector.Location = New Point(684, 614)
        txtDirector.Margin = New Padding(5, 6, 5, 6)
        txtDirector.MaxLength = 60
        txtDirector.Name = "txtDirector"
        txtDirector.Size = New Size(453, 31)
        txtDirector.TabIndex = 27
        ToolTip1.SetToolTip(txtDirector, "Required for CLIA compliance")
        ' 
        ' Label8
        ' 
        Label8.ForeColor = Color.Crimson
        Label8.Location = New Point(34, 314)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(99, 27)
        Label8.TabIndex = 74
        Label8.Text = "CLIA"
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.Fuchsia
        Label7.Location = New Point(226, 314)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(114, 27)
        Label7.TabIndex = 73
        Label7.Text = "NPI"
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.Fuchsia
        Label6.Location = New Point(416, 314)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(135, 27)
        Label6.TabIndex = 72
        Label6.Text = "Fed Tax ID"
        ' 
        ' txtSSN
        ' 
        txtSSN.Location = New Point(405, 347)
        txtSSN.Margin = New Padding(5, 6, 5, 6)
        txtSSN.Mask = "00-0000000"
        txtSSN.Name = "txtSSN"
        txtSSN.Size = New Size(198, 31)
        txtSSN.TabIndex = 71
        txtSSN.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
        ToolTip1.SetToolTip(txtSSN, "Either NPI or the Fed Tax ID required for Billing")
        ' 
        ' txtNPI
        ' 
        txtNPI.Location = New Point(226, 347)
        txtNPI.Margin = New Padding(5, 6, 5, 6)
        txtNPI.MaxLength = 60
        txtNPI.Name = "txtNPI"
        txtNPI.Size = New Size(165, 31)
        txtNPI.TabIndex = 14
        ToolTip1.SetToolTip(txtNPI, "Either NPI or the Fed Tax ID required for Billing")
        ' 
        ' txtCLIA
        ' 
        txtCLIA.Location = New Point(20, 347)
        txtCLIA.Margin = New Padding(5, 6, 5, 6)
        txtCLIA.MaxLength = 60
        txtCLIA.Name = "txtCLIA"
        txtCLIA.Size = New Size(180, 31)
        txtCLIA.TabIndex = 13
        ToolTip1.SetToolTip(txtCLIA, "Required for billing")
        ' 
        ' txtLabels
        ' 
        txtLabels.Location = New Point(20, 614)
        txtLabels.Margin = New Padding(5, 6, 5, 6)
        txtLabels.MaxLength = 25
        txtLabels.Name = "txtLabels"
        txtLabels.Size = New Size(108, 31)
        txtLabels.TabIndex = 25
        txtLabels.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.DarkBlue
        Label5.Location = New Point(20, 581)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(110, 27)
        Label5.TabIndex = 70
        Label5.Text = "Labels"
        Label5.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtFax
        ' 
        txtFax.Location = New Point(544, 150)
        txtFax.Margin = New Padding(5, 6, 5, 6)
        txtFax.Name = "txtFax"
        txtFax.Size = New Size(164, 31)
        txtFax.TabIndex = 9
        ' 
        ' txtPhone
        ' 
        txtPhone.Location = New Point(365, 150)
        txtPhone.Margin = New Padding(5, 6, 5, 6)
        txtPhone.Name = "txtPhone"
        txtPhone.Size = New Size(165, 31)
        txtPhone.TabIndex = 8
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(856, 34)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(99, 27)
        Label4.TabIndex = 68
        Label4.Text = "Active"
        Label4.TextAlign = ContentAlignment.TopCenter
        ' 
        ' btnLabelFile
        ' 
        btnLabelFile.Image = CType(resources.GetObject("btnLabelFile.Image"), Image)
        btnLabelFile.Location = New Point(616, 608)
        btnLabelFile.Margin = New Padding(5, 6, 5, 6)
        btnLabelFile.Name = "btnLabelFile"
        btnLabelFile.Size = New Size(44, 48)
        btnLabelFile.TabIndex = 25
        btnLabelFile.UseVisualStyleBackColor = True
        ' 
        ' btnDocFile
        ' 
        btnDocFile.Image = CType(resources.GetObject("btnDocFile.Image"), Image)
        btnDocFile.Location = New Point(1095, 514)
        btnDocFile.Margin = New Padding(5, 6, 5, 6)
        btnDocFile.Name = "btnDocFile"
        btnDocFile.Size = New Size(44, 48)
        btnDocFile.TabIndex = 22
        btnDocFile.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(171, 581)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(136, 27)
        Label2.TabIndex = 65
        Label2.Text = "Label File"
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(316, 486)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(141, 27)
        Label1.TabIndex = 64
        Label1.Text = "Document File"
        ' 
        ' txtLabelFile
        ' 
        txtLabelFile.Location = New Point(150, 614)
        txtLabelFile.Margin = New Padding(5, 6, 5, 6)
        txtLabelFile.MaxLength = 100
        txtLabelFile.Name = "txtLabelFile"
        txtLabelFile.Size = New Size(453, 31)
        txtLabelFile.TabIndex = 26
        ' 
        ' txtDocFile
        ' 
        txtDocFile.Location = New Point(321, 519)
        txtDocFile.Margin = New Padding(5, 6, 5, 6)
        txtDocFile.MaxLength = 100
        txtDocFile.Name = "txtDocFile"
        txtDocFile.Size = New Size(760, 31)
        txtDocFile.TabIndex = 24
        ' 
        ' chkPrimary
        ' 
        chkPrimary.Appearance = Appearance.Button
        chkPrimary.ForeColor = Color.DarkBlue
        chkPrimary.Location = New Point(20, 142)
        chkPrimary.Margin = New Padding(5, 6, 5, 6)
        chkPrimary.Name = "chkPrimary"
        chkPrimary.Size = New Size(89, 50)
        chkPrimary.TabIndex = 6
        chkPrimary.Text = "No"
        chkPrimary.TextAlign = ContentAlignment.MiddleCenter
        chkPrimary.UseVisualStyleBackColor = True
        ' 
        ' txtNote
        ' 
        txtNote.Location = New Point(20, 711)
        txtNote.Margin = New Padding(5, 6, 5, 6)
        txtNote.MaxLength = 960
        txtNote.Multiline = True
        txtNote.Name = "txtNote"
        txtNote.ScrollBars = ScrollBars.Vertical
        txtNote.Size = New Size(1115, 181)
        txtNote.TabIndex = 28
        ' 
        ' Label29
        ' 
        Label29.ForeColor = Color.DarkBlue
        Label29.Location = New Point(34, 678)
        Label29.Margin = New Padding(5, 0, 5, 0)
        Label29.Name = "Label29"
        Label29.Size = New Size(221, 27)
        Label29.TabIndex = 57
        Label29.Text = "Note:"
        Label29.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' txtContact
        ' 
        txtContact.Location = New Point(121, 150)
        txtContact.Margin = New Padding(5, 6, 5, 6)
        txtContact.MaxLength = 60
        txtContact.Name = "txtContact"
        txtContact.Size = New Size(215, 31)
        txtContact.TabIndex = 7
        ' 
        ' Label28
        ' 
        Label28.ForeColor = Color.DarkBlue
        Label28.Location = New Point(121, 117)
        Label28.Margin = New Padding(5, 0, 5, 0)
        Label28.Name = "Label28"
        Label28.Size = New Size(151, 27)
        Label28.TabIndex = 55
        Label28.Text = "Office Contact"
        ' 
        ' Label27
        ' 
        Label27.ForeColor = Color.DarkBlue
        Label27.Location = New Point(29, 117)
        Label27.Margin = New Padding(5, 0, 5, 0)
        Label27.Name = "Label27"
        Label27.Size = New Size(80, 27)
        Label27.TabIndex = 53
        Label27.Text = "Primary"
        ' 
        ' txtPassword
        ' 
        txtPassword.Location = New Point(789, 347)
        txtPassword.Margin = New Padding(5, 6, 5, 6)
        txtPassword.MaxLength = 60
        txtPassword.Name = "txtPassword"
        txtPassword.Size = New Size(164, 31)
        txtPassword.TabIndex = 17
        ' 
        ' Label26
        ' 
        Label26.ForeColor = Color.DarkBlue
        Label26.Location = New Point(789, 314)
        Label26.Margin = New Padding(5, 0, 5, 0)
        Label26.Name = "Label26"
        Label26.Size = New Size(89, 27)
        Label26.TabIndex = 51
        Label26.Text = "Password"
        ' 
        ' txtUserName
        ' 
        txtUserName.Location = New Point(616, 347)
        txtUserName.Margin = New Padding(5, 6, 5, 6)
        txtUserName.MaxLength = 60
        txtUserName.Name = "txtUserName"
        txtUserName.Size = New Size(154, 31)
        txtUserName.TabIndex = 16
        ' 
        ' Label25
        ' 
        Label25.ForeColor = Color.DarkBlue
        Label25.Location = New Point(611, 314)
        Label25.Margin = New Padding(5, 0, 5, 0)
        Label25.Name = "Label25"
        Label25.Size = New Size(99, 27)
        Label25.TabIndex = 49
        Label25.Text = "User Name"
        ' 
        ' chkActive
        ' 
        chkActive.Appearance = Appearance.Button
        chkActive.Checked = True
        chkActive.CheckState = CheckState.Checked
        chkActive.ForeColor = Color.DarkBlue
        chkActive.Location = New Point(855, 64)
        chkActive.Margin = New Padding(5, 6, 5, 6)
        chkActive.Name = "chkActive"
        chkActive.Size = New Size(100, 42)
        chkActive.TabIndex = 4
        chkActive.Text = "Yes"
        chkActive.TextAlign = ContentAlignment.MiddleCenter
        chkActive.UseVisualStyleBackColor = True
        ' 
        ' txtCountry
        ' 
        txtCountry.Location = New Point(20, 519)
        txtCountry.Margin = New Padding(5, 6, 5, 6)
        txtCountry.MaxLength = 35
        txtCountry.Name = "txtCountry"
        txtCountry.Size = New Size(285, 31)
        txtCountry.TabIndex = 23
        ' 
        ' Label21
        ' 
        Label21.ForeColor = Color.DarkBlue
        Label21.Location = New Point(34, 486)
        Label21.Margin = New Padding(5, 0, 5, 0)
        Label21.Name = "Label21"
        Label21.Size = New Size(141, 27)
        Label21.TabIndex = 46
        Label21.Text = "Country"
        ' 
        ' txtZip
        ' 
        txtZip.Location = New Point(971, 433)
        txtZip.Margin = New Padding(5, 6, 5, 6)
        txtZip.MaxLength = 25
        txtZip.Name = "txtZip"
        txtZip.Size = New Size(164, 31)
        txtZip.TabIndex = 22
        ' 
        ' Label20
        ' 
        Label20.ForeColor = Color.Red
        Label20.Location = New Point(975, 400)
        Label20.Margin = New Padding(5, 0, 5, 0)
        Label20.Name = "Label20"
        Label20.Size = New Size(114, 27)
        Label20.TabIndex = 44
        Label20.Text = "Zip Code"
        ' 
        ' txtState
        ' 
        txtState.Location = New Point(771, 433)
        txtState.Margin = New Padding(5, 6, 5, 6)
        txtState.MaxLength = 35
        txtState.Name = "txtState"
        txtState.Size = New Size(180, 31)
        txtState.TabIndex = 21
        ' 
        ' Label19
        ' 
        Label19.ForeColor = Color.Red
        Label19.Location = New Point(784, 400)
        Label19.Margin = New Padding(5, 0, 5, 0)
        Label19.Name = "Label19"
        Label19.Size = New Size(134, 27)
        Label19.TabIndex = 42
        Label19.Text = "State/Province"
        ' 
        ' txtCity
        ' 
        txtCity.Location = New Point(531, 433)
        txtCity.Margin = New Padding(5, 6, 5, 6)
        txtCity.MaxLength = 35
        txtCity.Name = "txtCity"
        txtCity.Size = New Size(228, 31)
        txtCity.TabIndex = 20
        ' 
        ' Label18
        ' 
        Label18.ForeColor = Color.Red
        Label18.Location = New Point(539, 400)
        Label18.Margin = New Padding(5, 0, 5, 0)
        Label18.Name = "Label18"
        Label18.Size = New Size(90, 27)
        Label18.TabIndex = 40
        Label18.Text = "City"
        ' 
        ' txtAdd2
        ' 
        txtAdd2.Location = New Point(319, 433)
        txtAdd2.Margin = New Padding(5, 6, 5, 6)
        txtAdd2.MaxLength = 35
        txtAdd2.Name = "txtAdd2"
        txtAdd2.Size = New Size(200, 31)
        txtAdd2.TabIndex = 19
        ' 
        ' Label17
        ' 
        Label17.ForeColor = Color.DarkBlue
        Label17.Location = New Point(331, 400)
        Label17.Margin = New Padding(5, 0, 5, 0)
        Label17.Name = "Label17"
        Label17.Size = New Size(126, 27)
        Label17.TabIndex = 38
        Label17.Text = "Address 2"
        ' 
        ' txtAdd1
        ' 
        txtAdd1.Location = New Point(20, 433)
        txtAdd1.Margin = New Padding(5, 6, 5, 6)
        txtAdd1.MaxLength = 35
        txtAdd1.Name = "txtAdd1"
        txtAdd1.Size = New Size(285, 31)
        txtAdd1.TabIndex = 18
        ' 
        ' Label16
        ' 
        Label16.ForeColor = Color.Red
        Label16.Location = New Point(34, 400)
        Label16.Margin = New Padding(5, 0, 5, 0)
        Label16.Name = "Label16"
        Label16.Size = New Size(114, 27)
        Label16.TabIndex = 36
        Label16.Text = "Address 1"
        ' 
        ' txtCommDLL
        ' 
        txtCommDLL.Location = New Point(616, 244)
        txtCommDLL.Margin = New Padding(5, 6, 5, 6)
        txtCommDLL.MaxLength = 60
        txtCommDLL.Name = "txtCommDLL"
        txtCommDLL.Size = New Size(519, 31)
        txtCommDLL.TabIndex = 12
        ' 
        ' Label15
        ' 
        Label15.ForeColor = Color.DarkBlue
        Label15.Location = New Point(611, 208)
        Label15.Margin = New Padding(5, 0, 5, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(176, 27)
        Label15.TabIndex = 34
        Label15.Text = "Interface DLL"
        ' 
        ' txtEmail
        ' 
        txtEmail.Location = New Point(725, 150)
        txtEmail.Margin = New Padding(5, 6, 5, 6)
        txtEmail.MaxLength = 60
        txtEmail.Name = "txtEmail"
        txtEmail.Size = New Size(410, 31)
        txtEmail.TabIndex = 10
        ' 
        ' Label14
        ' 
        Label14.ForeColor = Color.DarkBlue
        Label14.Location = New Point(736, 117)
        Label14.Margin = New Padding(5, 0, 5, 0)
        Label14.Name = "Label14"
        Label14.Size = New Size(119, 27)
        Label14.TabIndex = 32
        Label14.Text = "Email"
        ' 
        ' btnLabLook
        ' 
        btnLabLook.Image = CType(resources.GetObject("btnLabLook.Image"), Image)
        btnLabLook.Location = New Point(150, 61)
        btnLabLook.Margin = New Padding(5, 6, 5, 6)
        btnLabLook.Name = "btnLabLook"
        btnLabLook.Size = New Size(44, 48)
        btnLabLook.TabIndex = 2
        btnLabLook.UseVisualStyleBackColor = True
        ' 
        ' txtLabID
        ' 
        txtLabID.Location = New Point(20, 67)
        txtLabID.Margin = New Padding(5, 6, 5, 6)
        txtLabID.MaxLength = 5
        txtLabID.Name = "txtLabID"
        txtLabID.Size = New Size(124, 31)
        txtLabID.TabIndex = 1
        txtLabID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label13
        ' 
        Label13.ForeColor = Color.Red
        Label13.Location = New Point(29, 34)
        Label13.Margin = New Padding(5, 0, 5, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(119, 27)
        Label13.TabIndex = 29
        Label13.Text = "Laboratory ID"
        ' 
        ' txtWebsite
        ' 
        txtWebsite.Location = New Point(20, 244)
        txtWebsite.Margin = New Padding(5, 6, 5, 6)
        txtWebsite.MaxLength = 60
        txtWebsite.Name = "txtWebsite"
        txtWebsite.Size = New Size(568, 31)
        txtWebsite.TabIndex = 11
        ' 
        ' Label12
        ' 
        Label12.ForeColor = Color.DarkBlue
        Label12.Location = New Point(29, 208)
        Label12.Margin = New Padding(5, 0, 5, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(119, 27)
        Label12.TabIndex = 27
        Label12.Text = "Website"
        ' 
        ' Label11
        ' 
        Label11.ForeColor = Color.DarkBlue
        Label11.Location = New Point(556, 117)
        Label11.Margin = New Padding(5, 0, 5, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(114, 27)
        Label11.TabIndex = 25
        Label11.Text = "Fax"
        ' 
        ' Label10
        ' 
        Label10.ForeColor = Color.DarkBlue
        Label10.Location = New Point(376, 117)
        Label10.Margin = New Padding(5, 0, 5, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(131, 27)
        Label10.TabIndex = 23
        Label10.Text = "Phone"
        ' 
        ' txtAccount
        ' 
        txtAccount.Location = New Point(965, 67)
        txtAccount.Margin = New Padding(5, 6, 5, 6)
        txtAccount.MaxLength = 60
        txtAccount.Name = "txtAccount"
        txtAccount.Size = New Size(170, 31)
        txtAccount.TabIndex = 5
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(975, 34)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(114, 27)
        Label3.TabIndex = 7
        Label3.Text = "Account No"
        ' 
        ' txtLabName
        ' 
        txtLabName.Location = New Point(204, 67)
        txtLabName.Margin = New Padding(5, 6, 5, 6)
        txtLabName.MaxLength = 60
        txtLabName.Name = "txtLabName"
        txtLabName.Size = New Size(639, 31)
        txtLabName.TabIndex = 3
        ' 
        ' lblLName
        ' 
        lblLName.ForeColor = Color.Red
        lblLName.Location = New Point(204, 34)
        lblLName.Margin = New Padding(5, 0, 5, 0)
        lblLName.Name = "lblLName"
        lblLName.Size = New Size(166, 27)
        lblLName.TabIndex = 1
        lblLName.Text = "Laboratory Name"
        ' 
        ' tpContract
        ' 
        tpContract.Controls.Add(Button1)
        tpContract.Controls.Add(search)
        tpContract.Controls.Add(Label9)
        tpContract.Controls.Add(btnMapping)
        tpContract.Controls.Add(Label31)
        tpContract.Controls.Add(Label30)
        tpContract.Controls.Add(dtpTo)
        tpContract.Controls.Add(dtpFrom)
        tpContract.Controls.Add(dgvContract)
        tpContract.Location = New Point(4, 34)
        tpContract.Margin = New Padding(5, 6, 5, 6)
        tpContract.Name = "tpContract"
        tpContract.Padding = New Padding(5, 6, 5, 6)
        tpContract.Size = New Size(1193, 951)
        tpContract.TabIndex = 2
        tpContract.Text = "Contract Management"
        tpContract.UseVisualStyleBackColor = True
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(956, 95)
        Button1.Margin = New Padding(5, 6, 5, 6)
        Button1.Name = "Button1"
        Button1.Size = New Size(205, 42)
        Button1.TabIndex = 31
        Button1.Text = "Reset"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' search
        ' 
        search.Location = New Point(30, 103)
        search.Margin = New Padding(4, 5, 4, 5)
        search.Name = "search"
        search.Size = New Size(398, 31)
        search.TabIndex = 30
        search.Text = "Search here ...."
        ' 
        ' Label9
        ' 
        Label9.ForeColor = Color.Red
        Label9.Location = New Point(524, 33)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(356, 58)
        Label9.TabIndex = 29
        Label9.Text = "Components without 'LabOrdID' and/or ''Lab Res ID', will not be saved."
        Label9.TextAlign = ContentAlignment.TopCenter
        ' 
        ' btnMapping
        ' 
        btnMapping.Enabled = False
        btnMapping.Location = New Point(956, 36)
        btnMapping.Margin = New Padding(5, 6, 5, 6)
        btnMapping.Name = "btnMapping"
        btnMapping.Size = New Size(205, 47)
        btnMapping.TabIndex = 28
        btnMapping.Text = "Upload Mapping"
        btnMapping.UseVisualStyleBackColor = True
        ' 
        ' Label31
        ' 
        Label31.ForeColor = Color.DarkBlue
        Label31.Location = New Point(250, 6)
        Label31.Margin = New Padding(5, 0, 5, 0)
        Label31.Name = "Label31"
        Label31.Size = New Size(135, 41)
        Label31.TabIndex = 8
        Label31.Text = "Expires"
        Label31.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label30
        ' 
        Label30.ForeColor = Color.DarkBlue
        Label30.Location = New Point(25, 6)
        Label30.Margin = New Padding(5, 0, 5, 0)
        Label30.Name = "Label30"
        Label30.Size = New Size(170, 41)
        Label30.TabIndex = 7
        Label30.Text = "Effective From"
        Label30.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' dtpTo
        ' 
        dtpTo.Format = DateTimePickerFormat.Short
        dtpTo.Location = New Point(235, 52)
        dtpTo.Margin = New Padding(5, 6, 5, 6)
        dtpTo.Name = "dtpTo"
        dtpTo.Size = New Size(193, 31)
        dtpTo.TabIndex = 26
        ' 
        ' dtpFrom
        ' 
        dtpFrom.Format = DateTimePickerFormat.Short
        dtpFrom.Location = New Point(30, 52)
        dtpFrom.Margin = New Padding(5, 6, 5, 6)
        dtpFrom.Name = "dtpFrom"
        dtpFrom.Size = New Size(193, 31)
        dtpFrom.TabIndex = 25
        ' 
        ' dgvContract
        ' 
        dgvContract.AllowUserToAddRows = False
        dgvContract.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.AntiqueWhite
        dgvContract.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvContract.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvContract.ColumnHeadersHeight = 34
        dgvContract.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvContract.Columns.AddRange(New DataGridViewColumn() {DEL, ID, Look, Component, Logo, LabComp, LabResID, Price})
        dgvContract.Location = New Point(30, 148)
        dgvContract.Margin = New Padding(5, 6, 5, 6)
        dgvContract.Name = "dgvContract"
        dgvContract.RowHeadersVisible = False
        dgvContract.RowHeadersWidth = 62
        dgvContract.Size = New Size(1131, 759)
        dgvContract.TabIndex = 27
        ' 
        ' DEL
        ' 
        DEL.FillWeight = 30F
        DEL.HeaderText = ""
        DEL.Image = CType(resources.GetObject("DEL.Image"), Image)
        DEL.MinimumWidth = 8
        DEL.Name = "DEL"
        DEL.Resizable = DataGridViewTriState.True
        ' 
        ' ID
        ' 
        ID.FillWeight = 80F
        ID.HeaderText = "Prolis ID"
        ID.MaxInputLength = 12
        ID.MinimumWidth = 8
        ID.Name = "ID"
        ' 
        ' Look
        ' 
        Look.FillWeight = 30F
        Look.HeaderText = ""
        Look.Image = CType(resources.GetObject("Look.Image"), Image)
        Look.MinimumWidth = 8
        Look.Name = "Look"
        Look.ReadOnly = True
        ' 
        ' Component
        ' 
        Component.FillWeight = 221F
        Component.HeaderText = "Component Name"
        Component.MinimumWidth = 8
        Component.Name = "Component"
        Component.ReadOnly = True
        ' 
        ' Logo
        ' 
        Logo.FillWeight = 30F
        Logo.HeaderText = ""
        Logo.Image = CType(resources.GetObject("Logo.Image"), Image)
        Logo.MinimumWidth = 8
        Logo.Name = "Logo"
        Logo.ReadOnly = True
        ' 
        ' LabComp
        ' 
        LabComp.FillWeight = 90F
        LabComp.HeaderText = "Lab Ord ID"
        LabComp.MaxInputLength = 25
        LabComp.MinimumWidth = 8
        LabComp.Name = "LabComp"
        ' 
        ' LabResID
        ' 
        LabResID.FillWeight = 96F
        LabResID.HeaderText = "Lab Res ID"
        LabResID.MinimumWidth = 8
        LabResID.Name = "LabResID"
        ' 
        ' Price
        ' 
        Price.FillWeight = 78F
        Price.HeaderText = "Price"
        Price.MinimumWidth = 8
        Price.Name = "Price"
        Price.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' frmLabMgmt
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1241, 1083)
        Controls.Add(tcProvider)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmLabMgmt"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Reference Lab Management"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        tcProvider.ResumeLayout(False)
        tpLab.ResumeLayout(False)
        tpLab.PerformLayout()
        tpContract.ResumeLayout(False)
        tpContract.PerformLayout()
        CType(dgvContract, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents chkEditNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents tcProvider As System.Windows.Forms.TabControl
    Friend WithEvents tpLab As System.Windows.Forms.TabPage
    Friend WithEvents txtNote As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtContact As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents txtCountry As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtZip As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtState As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtAdd2 As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtAdd1 As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtCommDLL As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnLabLook As System.Windows.Forms.Button
    Friend WithEvents txtLabID As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtWebsite As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtAccount As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtLabName As System.Windows.Forms.TextBox
    Friend WithEvents lblLName As System.Windows.Forms.Label
    Friend WithEvents tpContract As System.Windows.Forms.TabPage
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dgvContract As System.Windows.Forms.DataGridView
    Friend WithEvents chkPrimary As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtLabelFile As System.Windows.Forms.TextBox
    Friend WithEvents txtDocFile As System.Windows.Forms.TextBox
    Friend WithEvents btnLabelFile As System.Windows.Forms.Button
    Friend WithEvents btnDocFile As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtFax As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtPhone As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtLabels As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtNPI As System.Windows.Forms.TextBox
    Friend WithEvents txtCLIA As System.Windows.Forms.TextBox
    Friend WithEvents txtSSN As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnMapping As System.Windows.Forms.Button
    Friend WithEvents DEL As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Look As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents Component As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Logo As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents LabComp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LabResID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Price As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtDirector As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents search As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button

End Class
