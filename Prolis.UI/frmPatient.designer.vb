<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPatient
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatient))
        ToolStrip1 = New ToolStrip()
        chkNewEdit = New ToolStripButton()
        ToolStripButton2 = New ToolStripSeparator()
        btnSave = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnDelete = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnPrint = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        TabControl1 = New TabControl()
        tpPatient = New TabPage()
        Label84 = New Label()
        Label83 = New Label()
        cmbBreed = New ComboBox()
        cmbSpecies = New ComboBox()
        Label76 = New Label()
        cmbRace = New ComboBox()
        Label127 = New Label()
        txtTage = New TextBox()
        Label14 = New Label()
        cmbSex = New ComboBox()
        Label80 = New Label()
        txtSecretA = New TextBox()
        Label79 = New Label()
        cmbSecretQ = New ComboBox()
        Label77 = New Label()
        txtPassword = New TextBox()
        Label75 = New Label()
        cmbEthnicity = New ComboBox()
        Label11 = New Label()
        txtNote = New TextBox()
        Label67 = New Label()
        txtEmployer = New TextBox()
        Label65 = New Label()
        txtWPhone = New TextBox()
        Label64 = New Label()
        txtDeathDate = New MaskedTextBox()
        Label63 = New Label()
        chkAlive = New CheckBox()
        Label62 = New Label()
        txtCell = New TextBox()
        Label61 = New Label()
        txtHPhone = New TextBox()
        Label60 = New Label()
        txtFax = New TextBox()
        Label40 = New Label()
        txtSSN = New MaskedTextBox()
        txtDOB = New MaskedTextBox()
        btnPatLook = New Button()
        Label13 = New Label()
        txtCountry = New TextBox()
        Label12 = New Label()
        txtEmail = New TextBox()
        Label10 = New Label()
        txtZip = New TextBox()
        Label9 = New Label()
        txtState = New TextBox()
        Label8 = New Label()
        txtCity = New TextBox()
        Label7 = New Label()
        txtAdd2 = New TextBox()
        Label6 = New Label()
        txtAdd1 = New TextBox()
        Label5 = New Label()
        Label4 = New Label()
        txtMName = New TextBox()
        Label3 = New Label()
        txtFName = New TextBox()
        Label2 = New Label()
        txtLName = New TextBox()
        Label1 = New Label()
        txtPatientID = New TextBox()
        tpPrimary = New TabPage()
        grpPSubs = New GroupBox()
        Label81 = New Label()
        txtPSubTage = New TextBox()
        Label74 = New Label()
        Label59 = New Label()
        Label55 = New Label()
        Label53 = New Label()
        txtPSubEmployer = New TextBox()
        txtPSubFax = New TextBox()
        txtPSubCell = New TextBox()
        txtPSubWPhone = New TextBox()
        txtPSubLName = New TextBox()
        Label56 = New Label()
        txtPSubSSN = New MaskedTextBox()
        Label25 = New Label()
        txtPSubDOB = New MaskedTextBox()
        cmbPSubSex = New ComboBox()
        btnPSubLook = New Button()
        Label26 = New Label()
        txtPSubCountry = New TextBox()
        Label27 = New Label()
        txtPSubEmail = New TextBox()
        Label28 = New Label()
        txtPSubHPhone = New TextBox()
        Label29 = New Label()
        txtPSubZip = New TextBox()
        Label30 = New Label()
        txtPSubState = New TextBox()
        Label31 = New Label()
        txtPSubCity = New TextBox()
        Label32 = New Label()
        txtPSubAdd2 = New TextBox()
        Label33 = New Label()
        txtPSubAdd1 = New TextBox()
        Label34 = New Label()
        Label35 = New Label()
        txtPSubMName = New TextBox()
        Label36 = New Label()
        txtPSubFName = New TextBox()
        Label37 = New Label()
        Label38 = New Label()
        txtPSubID = New TextBox()
        grpPrimary = New GroupBox()
        btnDelPrime = New Button()
        txtPCopay = New TextBox()
        Label58 = New Label()
        Label39 = New Label()
        cmbPRelation = New ComboBox()
        Label19 = New Label()
        txtPFrom = New MaskedTextBox()
        btnPIns = New Button()
        Label18 = New Label()
        cmbPIns = New ComboBox()
        txtPGroup = New TextBox()
        txtPTo = New MaskedTextBox()
        Label17 = New Label()
        Label15 = New Label()
        txtPPolicy = New TextBox()
        Label16 = New Label()
        tpSecondary = New TabPage()
        GroupBox1 = New GroupBox()
        btnDelSecond = New Button()
        txtSCopay = New TextBox()
        Label66 = New Label()
        Label68 = New Label()
        cmbSRelation = New ComboBox()
        Label69 = New Label()
        txtSFrom = New MaskedTextBox()
        txtSTo = New MaskedTextBox()
        btnSIns = New Button()
        txtSGroup = New TextBox()
        Label70 = New Label()
        cmbSIns = New ComboBox()
        txtSPolicy = New TextBox()
        Label71 = New Label()
        Label72 = New Label()
        Label73 = New Label()
        grpSSubs = New GroupBox()
        Label82 = New Label()
        txtSSubTage = New TextBox()
        Label52 = New Label()
        Label51 = New Label()
        Label50 = New Label()
        txtSSubEmployer = New TextBox()
        txtSSubFax = New TextBox()
        Label46 = New Label()
        Label57 = New Label()
        txtSSubCell = New TextBox()
        txtSSubSSN = New MaskedTextBox()
        Label20 = New Label()
        txtSSubDOB = New MaskedTextBox()
        cmbSSubSex = New ComboBox()
        btnSSubLook = New Button()
        Label21 = New Label()
        txtSSubCountry = New TextBox()
        Label22 = New Label()
        txtSSubEmail = New TextBox()
        Label23 = New Label()
        txtSSubWPhone = New TextBox()
        txtSSubHPhone = New TextBox()
        Label54 = New Label()
        Label24 = New Label()
        txtSSubZip = New TextBox()
        Label41 = New Label()
        txtSSubState = New TextBox()
        Label42 = New Label()
        txtSSubCity = New TextBox()
        Label43 = New Label()
        txtSSubAdd2 = New TextBox()
        Label44 = New Label()
        txtSSubAdd1 = New TextBox()
        Label45 = New Label()
        txtSSubMName = New TextBox()
        Label47 = New Label()
        txtSSubFName = New TextBox()
        Label78 = New Label()
        Label48 = New Label()
        txtSSubLName = New TextBox()
        Label49 = New Label()
        txtSSubID = New TextBox()
        TabPage1 = New TabPage()
        dgvFiles = New DataGridView()
        ID = New DataGridViewTextBoxColumn()
        Title = New DataGridViewTextBoxColumn()
        View = New DataGridViewButtonColumn()
        Column1 = New DataGridViewImageColumn()
        Panel1 = New Panel()
        Label85 = New Label()
        btnBrowse = New Button()
        txtTitle = New TextBox()
        TabPage3 = New TabPage()
        btnDefault = New Button()
        btnBG = New Button()
        btnColor = New Button()
        cmdFont = New Button()
        txtAlert = New RichTextBox()
        chkAcc = New CheckBox()
        chkCS = New CheckBox()
        Label87 = New Label()
        TabPage2 = New TabPage()
        endmail = New Button()
        Label86 = New Label()
        ToolTip1 = New ToolTip(components)
        StatusStrip1 = New StatusStrip()
        lblStatus = New ToolStripStatusLabel()
        PB = New ToolStripProgressBar()
        BW = New ComponentModel.BackgroundWorker()
        btnImport = New Button()
        OpenFileDialog1 = New OpenFileDialog()
        ToolStrip1.SuspendLayout()
        TabControl1.SuspendLayout()
        tpPatient.SuspendLayout()
        tpPrimary.SuspendLayout()
        grpPSubs.SuspendLayout()
        grpPrimary.SuspendLayout()
        tpSecondary.SuspendLayout()
        GroupBox1.SuspendLayout()
        grpSSubs.SuspendLayout()
        TabPage1.SuspendLayout()
        CType(dgvFiles, ComponentModel.ISupportInitialize).BeginInit()
        Panel1.SuspendLayout()
        TabPage3.SuspendLayout()
        TabPage2.SuspendLayout()
        StatusStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(20, 20)
        ToolStrip1.Items.AddRange(New ToolStripItem() {chkNewEdit, ToolStripButton2, btnSave, ToolStripSeparator1, btnDelete, ToolStripSeparator2, btnPrint, ToolStripSeparator4, btnCancel, ToolStripSeparator3, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(1015, 34)
        ToolStrip1.TabIndex = 2
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' chkNewEdit
        ' 
        chkNewEdit.CheckOnClick = True
        chkNewEdit.Image = CType(resources.GetObject("chkNewEdit.Image"), Image)
        chkNewEdit.ImageTransparentColor = Color.Magenta
        chkNewEdit.Name = "chkNewEdit"
        chkNewEdit.Size = New Size(71, 29)
        chkNewEdit.Text = "New"
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
        ' btnPrint
        ' 
        btnPrint.Enabled = False
        btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), Image)
        btnPrint.ImageTransparentColor = Color.Magenta
        btnPrint.Name = "btnPrint"
        btnPrint.Size = New Size(138, 29)
        btnPrint.Text = "Print Patients"
        ' 
        ' ToolStripSeparator4
        ' 
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New Size(6, 34)
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
        ' TabControl1
        ' 
        TabControl1.Controls.Add(tpPatient)
        TabControl1.Controls.Add(tpPrimary)
        TabControl1.Controls.Add(tpSecondary)
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage3)
        TabControl1.Controls.Add(TabPage2)
        TabControl1.Location = New Point(16, 108)
        TabControl1.Margin = New Padding(5, 6, 5, 6)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(995, 794)
        TabControl1.SizeMode = TabSizeMode.FillToRight
        TabControl1.TabIndex = 3
        ' 
        ' tpPatient
        ' 
        tpPatient.Controls.Add(Label84)
        tpPatient.Controls.Add(Label83)
        tpPatient.Controls.Add(cmbBreed)
        tpPatient.Controls.Add(cmbSpecies)
        tpPatient.Controls.Add(Label76)
        tpPatient.Controls.Add(cmbRace)
        tpPatient.Controls.Add(Label127)
        tpPatient.Controls.Add(txtTage)
        tpPatient.Controls.Add(Label14)
        tpPatient.Controls.Add(cmbSex)
        tpPatient.Controls.Add(Label80)
        tpPatient.Controls.Add(txtSecretA)
        tpPatient.Controls.Add(Label79)
        tpPatient.Controls.Add(cmbSecretQ)
        tpPatient.Controls.Add(Label77)
        tpPatient.Controls.Add(txtPassword)
        tpPatient.Controls.Add(Label75)
        tpPatient.Controls.Add(cmbEthnicity)
        tpPatient.Controls.Add(Label11)
        tpPatient.Controls.Add(txtNote)
        tpPatient.Controls.Add(Label67)
        tpPatient.Controls.Add(txtEmployer)
        tpPatient.Controls.Add(Label65)
        tpPatient.Controls.Add(txtWPhone)
        tpPatient.Controls.Add(Label64)
        tpPatient.Controls.Add(txtDeathDate)
        tpPatient.Controls.Add(Label63)
        tpPatient.Controls.Add(chkAlive)
        tpPatient.Controls.Add(Label62)
        tpPatient.Controls.Add(txtCell)
        tpPatient.Controls.Add(Label61)
        tpPatient.Controls.Add(txtHPhone)
        tpPatient.Controls.Add(Label60)
        tpPatient.Controls.Add(txtFax)
        tpPatient.Controls.Add(Label40)
        tpPatient.Controls.Add(txtSSN)
        tpPatient.Controls.Add(txtDOB)
        tpPatient.Controls.Add(btnPatLook)
        tpPatient.Controls.Add(Label13)
        tpPatient.Controls.Add(txtCountry)
        tpPatient.Controls.Add(Label12)
        tpPatient.Controls.Add(txtEmail)
        tpPatient.Controls.Add(Label10)
        tpPatient.Controls.Add(txtZip)
        tpPatient.Controls.Add(Label9)
        tpPatient.Controls.Add(txtState)
        tpPatient.Controls.Add(Label8)
        tpPatient.Controls.Add(txtCity)
        tpPatient.Controls.Add(Label7)
        tpPatient.Controls.Add(txtAdd2)
        tpPatient.Controls.Add(Label6)
        tpPatient.Controls.Add(txtAdd1)
        tpPatient.Controls.Add(Label5)
        tpPatient.Controls.Add(Label4)
        tpPatient.Controls.Add(txtMName)
        tpPatient.Controls.Add(Label3)
        tpPatient.Controls.Add(txtFName)
        tpPatient.Controls.Add(Label2)
        tpPatient.Controls.Add(txtLName)
        tpPatient.Controls.Add(Label1)
        tpPatient.Controls.Add(txtPatientID)
        tpPatient.Location = New Point(4, 34)
        tpPatient.Margin = New Padding(5, 6, 5, 6)
        tpPatient.Name = "tpPatient"
        tpPatient.Padding = New Padding(5, 6, 5, 6)
        tpPatient.Size = New Size(987, 756)
        tpPatient.TabIndex = 0
        tpPatient.Text = "Patient Information"
        tpPatient.UseVisualStyleBackColor = True
        ' 
        ' Label84
        ' 
        Label84.ForeColor = Color.DarkBlue
        Label84.Location = New Point(731, 106)
        Label84.Margin = New Padding(5, 0, 5, 0)
        Label84.Name = "Label84"
        Label84.Size = New Size(94, 25)
        Label84.TabIndex = 99
        Label84.Text = "Breed"
        ' 
        ' Label83
        ' 
        Label83.ForeColor = Color.DarkBlue
        Label83.Location = New Point(481, 106)
        Label83.Margin = New Padding(5, 0, 5, 0)
        Label83.Name = "Label83"
        Label83.Size = New Size(94, 25)
        Label83.TabIndex = 98
        Label83.Text = "Species"
        ' 
        ' cmbBreed
        ' 
        cmbBreed.DropDownStyle = ComboBoxStyle.DropDownList
        cmbBreed.FormattingEnabled = True
        cmbBreed.Location = New Point(715, 133)
        cmbBreed.Margin = New Padding(5, 6, 5, 6)
        cmbBreed.Name = "cmbBreed"
        cmbBreed.Size = New Size(225, 33)
        cmbBreed.TabIndex = 97
        ' 
        ' cmbSpecies
        ' 
        cmbSpecies.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSpecies.FormattingEnabled = True
        cmbSpecies.Location = New Point(470, 133)
        cmbSpecies.Margin = New Padding(5, 6, 5, 6)
        cmbSpecies.Name = "cmbSpecies"
        cmbSpecies.Size = New Size(225, 33)
        cmbSpecies.TabIndex = 96
        ' 
        ' Label76
        ' 
        Label76.ForeColor = Color.Fuchsia
        Label76.Location = New Point(630, 552)
        Label76.Margin = New Padding(5, 0, 5, 0)
        Label76.Name = "Label76"
        Label76.Size = New Size(254, 25)
        Label76.TabIndex = 95
        Label76.Text = "Race"
        ' 
        ' cmbRace
        ' 
        cmbRace.DropDownStyle = ComboBoxStyle.DropDownList
        cmbRace.FormattingEnabled = True
        cmbRace.Items.AddRange(New Object() {"White", "Black or African American", "Asian", "Mixed Race", "American Indian or Alaska Native", "Native Hawaiian or other Pacific Islander", "Middle Eastren or North African", "Other"})
        cmbRace.Location = New Point(625, 583)
        cmbRace.Margin = New Padding(5, 6, 5, 6)
        cmbRace.Name = "cmbRace"
        cmbRace.Size = New Size(315, 33)
        cmbRace.TabIndex = 94
        ToolTip1.SetToolTip(cmbRace, "Conditionally required field to be used in certain calculations")
        ' 
        ' Label127
        ' 
        Label127.Location = New Point(216, 106)
        Label127.Margin = New Padding(5, 0, 5, 0)
        Label127.Name = "Label127"
        Label127.Size = New Size(114, 25)
        Label127.TabIndex = 93
        Label127.Text = "Tr age (Yrs)"
        ' 
        ' txtTage
        ' 
        txtTage.BackColor = Color.White
        txtTage.Location = New Point(221, 136)
        txtTage.Margin = New Padding(5, 6, 5, 6)
        txtTage.MaxLength = 35
        txtTage.Name = "txtTage"
        txtTage.ReadOnly = True
        txtTage.Size = New Size(90, 31)
        txtTage.TabIndex = 92
        txtTage.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label14
        ' 
        Label14.ForeColor = Color.Red
        Label14.Location = New Point(31, 106)
        Label14.Margin = New Padding(5, 0, 5, 0)
        Label14.Name = "Label14"
        Label14.Size = New Size(101, 25)
        Label14.TabIndex = 91
        Label14.Text = "Gender"
        ' 
        ' cmbSex
        ' 
        cmbSex.BackColor = Color.Ivory
        cmbSex.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSex.FormattingEnabled = True
        cmbSex.Items.AddRange(New Object() {"F - Female", "M - Male", "G - Transgender Female", "N - Transgender Male ", "I - Indetermined", "U - Unreported"})
        cmbSex.Location = New Point(29, 136)
        cmbSex.Margin = New Padding(5, 6, 5, 6)
        cmbSex.Name = "cmbSex"
        cmbSex.Size = New Size(179, 33)
        cmbSex.TabIndex = 90
        ' 
        ' Label80
        ' 
        Label80.ForeColor = Color.DarkBlue
        Label80.Location = New Point(31, 377)
        Label80.Margin = New Padding(5, 0, 5, 0)
        Label80.Name = "Label80"
        Label80.Size = New Size(179, 25)
        Label80.TabIndex = 89
        Label80.Text = "Secret Answer"
        ' 
        ' txtSecretA
        ' 
        txtSecretA.AcceptsReturn = True
        txtSecretA.Location = New Point(29, 408)
        txtSecretA.Margin = New Padding(5, 6, 5, 6)
        txtSecretA.MaxLength = 50
        txtSecretA.Name = "txtSecretA"
        txtSecretA.Size = New Size(214, 31)
        txtSecretA.TabIndex = 18
        ToolTip1.SetToolTip(txtSecretA, "Optional field")
        ' 
        ' Label79
        ' 
        Label79.ForeColor = Color.DarkBlue
        Label79.Location = New Point(550, 284)
        Label79.Margin = New Padding(5, 0, 5, 0)
        Label79.Name = "Label79"
        Label79.Size = New Size(219, 25)
        Label79.TabIndex = 87
        Label79.Text = "Secret Question"
        ' 
        ' cmbSecretQ
        ' 
        cmbSecretQ.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSecretQ.FormattingEnabled = True
        cmbSecretQ.Items.AddRange(New Object() {"Name of your elementry school?", "Name of your favorite teacher?", "Name of your favorite food?", "Maiden name of your mother?"})
        cmbSecretQ.Location = New Point(535, 316)
        cmbSecretQ.Margin = New Padding(5, 6, 5, 6)
        cmbSecretQ.Name = "cmbSecretQ"
        cmbSecretQ.Size = New Size(405, 33)
        cmbSecretQ.TabIndex = 17
        ToolTip1.SetToolTip(cmbSecretQ, "Required field")
        ' 
        ' Label77
        ' 
        Label77.ForeColor = Color.DarkBlue
        Label77.Location = New Point(341, 284)
        Label77.Margin = New Padding(5, 0, 5, 0)
        Label77.Name = "Label77"
        Label77.Size = New Size(160, 25)
        Label77.TabIndex = 85
        Label77.Text = "Password"
        ' 
        ' txtPassword
        ' 
        txtPassword.AcceptsReturn = True
        txtPassword.Location = New Point(325, 317)
        txtPassword.Margin = New Padding(5, 6, 5, 6)
        txtPassword.MaxLength = 12
        txtPassword.Name = "txtPassword"
        txtPassword.Size = New Size(185, 31)
        txtPassword.TabIndex = 16
        ToolTip1.SetToolTip(txtPassword, "Optional field")
        ' 
        ' Label75
        ' 
        Label75.ForeColor = Color.Fuchsia
        Label75.Location = New Point(630, 642)
        Label75.Margin = New Padding(5, 0, 5, 0)
        Label75.Name = "Label75"
        Label75.Size = New Size(254, 25)
        Label75.TabIndex = 82
        Label75.Text = "Ethnicity"
        ' 
        ' cmbEthnicity
        ' 
        cmbEthnicity.DropDownStyle = ComboBoxStyle.DropDownList
        cmbEthnicity.FormattingEnabled = True
        cmbEthnicity.Items.AddRange(New Object() {"Unknown", "Hispanic or Latino", "Not Hispanic or Latino"})
        cmbEthnicity.Location = New Point(625, 673)
        cmbEthnicity.Margin = New Padding(5, 6, 5, 6)
        cmbEthnicity.Name = "cmbEthnicity"
        cmbEthnicity.Size = New Size(315, 33)
        cmbEthnicity.TabIndex = 27
        ToolTip1.SetToolTip(cmbEthnicity, "Conditionally required field to be used in certain calculations")
        ' 
        ' Label11
        ' 
        Label11.ForeColor = Color.DarkBlue
        Label11.Location = New Point(25, 552)
        Label11.Margin = New Padding(5, 0, 5, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(106, 25)
        Label11.TabIndex = 80
        Label11.Text = "Note"
        ' 
        ' txtNote
        ' 
        txtNote.AcceptsReturn = True
        txtNote.Location = New Point(29, 583)
        txtNote.Margin = New Padding(5, 6, 5, 6)
        txtNote.MaxLength = 250
        txtNote.Multiline = True
        txtNote.Name = "txtNote"
        txtNote.ScrollBars = ScrollBars.Vertical
        txtNote.Size = New Size(575, 127)
        txtNote.TabIndex = 26
        ToolTip1.SetToolTip(txtNote, "Optional field")
        ' 
        ' Label67
        ' 
        Label67.ForeColor = Color.DarkBlue
        Label67.Location = New Point(275, 377)
        Label67.Margin = New Padding(5, 0, 5, 0)
        Label67.Name = "Label67"
        Label67.Size = New Size(145, 25)
        Label67.TabIndex = 78
        Label67.Text = "Employer"
        ' 
        ' txtEmployer
        ' 
        txtEmployer.AcceptsReturn = True
        txtEmployer.Location = New Point(255, 408)
        txtEmployer.Margin = New Padding(5, 6, 5, 6)
        txtEmployer.MaxLength = 60
        txtEmployer.Name = "txtEmployer"
        txtEmployer.Size = New Size(213, 31)
        txtEmployer.TabIndex = 19
        ToolTip1.SetToolTip(txtEmployer, "Optional field")
        ' 
        ' Label65
        ' 
        Label65.ForeColor = Color.DarkBlue
        Label65.Location = New Point(594, 194)
        Label65.Margin = New Padding(5, 0, 5, 0)
        Label65.Name = "Label65"
        Label65.Size = New Size(106, 25)
        Label65.TabIndex = 74
        Label65.Text = "Work Phone"
        ' 
        ' txtWPhone
        ' 
        txtWPhone.AcceptsReturn = True
        txtWPhone.Location = New Point(569, 225)
        txtWPhone.Margin = New Padding(5, 6, 5, 6)
        txtWPhone.MaxLength = 13
        txtWPhone.Name = "txtWPhone"
        txtWPhone.Size = New Size(183, 31)
        txtWPhone.TabIndex = 13
        ToolTip1.SetToolTip(txtWPhone, "Optional field")
        ' 
        ' Label64
        ' 
        Label64.ForeColor = Color.DarkBlue
        Label64.Location = New Point(815, 17)
        Label64.Margin = New Padding(5, 0, 5, 0)
        Label64.Name = "Label64"
        Label64.Size = New Size(115, 25)
        Label64.TabIndex = 72
        Label64.Text = "Death Date"
        Label64.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' txtDeathDate
        ' 
        txtDeathDate.Enabled = False
        txtDeathDate.Location = New Point(820, 48)
        txtDeathDate.Margin = New Padding(5, 6, 5, 6)
        txtDeathDate.Mask = "00/00/0000"
        txtDeathDate.Name = "txtDeathDate"
        txtDeathDate.Size = New Size(120, 31)
        txtDeathDate.TabIndex = 6
        txtDeathDate.ValidatingType = GetType(Date)
        ' 
        ' Label63
        ' 
        Label63.ForeColor = Color.DarkBlue
        Label63.Location = New Point(739, 17)
        Label63.Margin = New Padding(5, 0, 5, 0)
        Label63.Name = "Label63"
        Label63.Size = New Size(69, 25)
        Label63.TabIndex = 70
        Label63.Text = "Alive?"
        Label63.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' chkAlive
        ' 
        chkAlive.Appearance = Appearance.Button
        chkAlive.Checked = True
        chkAlive.CheckState = CheckState.Checked
        chkAlive.Location = New Point(736, 42)
        chkAlive.Margin = New Padding(5, 6, 5, 6)
        chkAlive.Name = "chkAlive"
        chkAlive.Size = New Size(69, 48)
        chkAlive.TabIndex = 5
        chkAlive.Text = "Yes"
        chkAlive.TextAlign = ContentAlignment.MiddleCenter
        chkAlive.UseVisualStyleBackColor = True
        ' 
        ' Label62
        ' 
        Label62.ForeColor = Color.DarkBlue
        Label62.Location = New Point(390, 194)
        Label62.Margin = New Padding(5, 0, 5, 0)
        Label62.Name = "Label62"
        Label62.Size = New Size(121, 25)
        Label62.TabIndex = 68
        Label62.Text = "Cell"
        ' 
        ' txtCell
        ' 
        txtCell.AcceptsReturn = True
        txtCell.Location = New Point(374, 225)
        txtCell.Margin = New Padding(5, 6, 5, 6)
        txtCell.MaxLength = 13
        txtCell.Name = "txtCell"
        txtCell.Size = New Size(183, 31)
        txtCell.TabIndex = 12
        ToolTip1.SetToolTip(txtCell, "Optional field")
        ' 
        ' Label61
        ' 
        Label61.ForeColor = Color.DarkBlue
        Label61.Location = New Point(199, 194)
        Label61.Margin = New Padding(5, 0, 5, 0)
        Label61.Name = "Label61"
        Label61.Size = New Size(131, 25)
        Label61.TabIndex = 66
        Label61.Text = "Home Phone"
        ' 
        ' txtHPhone
        ' 
        txtHPhone.AcceptsReturn = True
        txtHPhone.Location = New Point(176, 225)
        txtHPhone.Margin = New Padding(5, 6, 5, 6)
        txtHPhone.MaxLength = 13
        txtHPhone.Name = "txtHPhone"
        txtHPhone.Size = New Size(183, 31)
        txtHPhone.TabIndex = 11
        ToolTip1.SetToolTip(txtHPhone, "Optional field")
        ' 
        ' Label60
        ' 
        Label60.ForeColor = Color.DarkBlue
        Label60.Location = New Point(770, 194)
        Label60.Margin = New Padding(5, 0, 5, 0)
        Label60.Name = "Label60"
        Label60.Size = New Size(145, 25)
        Label60.TabIndex = 64
        Label60.Text = "Fax"
        ' 
        ' txtFax
        ' 
        txtFax.AcceptsReturn = True
        txtFax.Location = New Point(759, 225)
        txtFax.Margin = New Padding(5, 6, 5, 6)
        txtFax.MaxLength = 13
        txtFax.Name = "txtFax"
        txtFax.Size = New Size(183, 31)
        txtFax.TabIndex = 14
        ToolTip1.SetToolTip(txtFax, "Optional field")
        ' 
        ' Label40
        ' 
        Label40.ForeColor = Color.DarkBlue
        Label40.Location = New Point(40, 194)
        Label40.Margin = New Padding(5, 0, 5, 0)
        Label40.Name = "Label40"
        Label40.Size = New Size(94, 25)
        Label40.TabIndex = 62
        Label40.Text = "SSN"
        ' 
        ' txtSSN
        ' 
        txtSSN.Location = New Point(29, 225)
        txtSSN.Margin = New Padding(5, 6, 5, 6)
        txtSSN.Mask = "000-00-0000"
        txtSSN.Name = "txtSSN"
        txtSSN.Size = New Size(135, 31)
        txtSSN.TabIndex = 10
        ToolTip1.SetToolTip(txtSSN, "Optional field")
        ' 
        ' txtDOB
        ' 
        txtDOB.Location = New Point(325, 134)
        txtDOB.Margin = New Padding(5, 6, 5, 6)
        txtDOB.Mask = "00/00/0000"
        txtDOB.Name = "txtDOB"
        txtDOB.Size = New Size(124, 31)
        txtDOB.TabIndex = 9
        ToolTip1.SetToolTip(txtDOB, "Required field")
        txtDOB.ValidatingType = GetType(Date)
        ' 
        ' btnPatLook
        ' 
        btnPatLook.Enabled = False
        btnPatLook.Image = CType(resources.GetObject("btnPatLook.Image"), Image)
        btnPatLook.Location = New Point(166, 41)
        btnPatLook.Margin = New Padding(5, 6, 5, 6)
        btnPatLook.Name = "btnPatLook"
        btnPatLook.Size = New Size(44, 50)
        btnPatLook.TabIndex = 2
        btnPatLook.UseVisualStyleBackColor = True
        ' 
        ' Label13
        ' 
        Label13.ForeColor = Color.DarkBlue
        Label13.Location = New Point(739, 467)
        Label13.Margin = New Padding(5, 0, 5, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(140, 25)
        Label13.TabIndex = 55
        Label13.Text = "Country"
        ' 
        ' txtCountry
        ' 
        txtCountry.AcceptsReturn = True
        txtCountry.Location = New Point(720, 498)
        txtCountry.Margin = New Padding(5, 6, 5, 6)
        txtCountry.MaxLength = 35
        txtCountry.Name = "txtCountry"
        txtCountry.Size = New Size(220, 31)
        txtCountry.TabIndex = 25
        ToolTip1.SetToolTip(txtCountry, "Optional field")
        ' 
        ' Label12
        ' 
        Label12.ForeColor = Color.DarkBlue
        Label12.Location = New Point(31, 284)
        Label12.Margin = New Padding(5, 0, 5, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(186, 25)
        Label12.TabIndex = 54
        Label12.Text = "Email Address"
        ' 
        ' txtEmail
        ' 
        txtEmail.AcceptsReturn = True
        txtEmail.Location = New Point(29, 317)
        txtEmail.Margin = New Padding(5, 6, 5, 6)
        txtEmail.MaxLength = 50
        txtEmail.Name = "txtEmail"
        txtEmail.Size = New Size(269, 31)
        txtEmail.TabIndex = 15
        ToolTip1.SetToolTip(txtEmail, "Optional field")
        ' 
        ' Label10
        ' 
        Label10.ForeColor = Color.Fuchsia
        Label10.Location = New Point(499, 467)
        Label10.Margin = New Padding(5, 0, 5, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(109, 25)
        Label10.TabIndex = 52
        Label10.Text = "Zip"
        ' 
        ' txtZip
        ' 
        txtZip.AcceptsReturn = True
        txtZip.Location = New Point(486, 498)
        txtZip.Margin = New Padding(5, 6, 5, 6)
        txtZip.MaxLength = 25
        txtZip.Name = "txtZip"
        txtZip.Size = New Size(220, 31)
        txtZip.TabIndex = 24
        ToolTip1.SetToolTip(txtZip, "Conditionally required address field")
        ' 
        ' Label9
        ' 
        Label9.ForeColor = Color.Fuchsia
        Label9.Location = New Point(280, 467)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(140, 25)
        Label9.TabIndex = 51
        Label9.Text = "State/Province"
        ' 
        ' txtState
        ' 
        txtState.AcceptsReturn = True
        txtState.Location = New Point(255, 498)
        txtState.Margin = New Padding(5, 6, 5, 6)
        txtState.MaxLength = 35
        txtState.Name = "txtState"
        txtState.Size = New Size(213, 31)
        txtState.TabIndex = 23
        ToolTip1.SetToolTip(txtState, "Conditionally required address field")
        ' 
        ' Label8
        ' 
        Label8.ForeColor = Color.Fuchsia
        Label8.Location = New Point(31, 467)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(100, 25)
        Label8.TabIndex = 50
        Label8.Text = "City"
        ' 
        ' txtCity
        ' 
        txtCity.AcceptsReturn = True
        txtCity.Location = New Point(34, 498)
        txtCity.Margin = New Padding(5, 6, 5, 6)
        txtCity.MaxLength = 35
        txtCity.Name = "txtCity"
        txtCity.Size = New Size(209, 31)
        txtCity.TabIndex = 22
        ToolTip1.SetToolTip(txtCity, "Conditionally required address field")
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.DarkBlue
        Label7.Location = New Point(734, 377)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(145, 25)
        Label7.TabIndex = 49
        Label7.Text = "Address Line 2"
        ' 
        ' txtAdd2
        ' 
        txtAdd2.AcceptsReturn = True
        txtAdd2.Location = New Point(720, 408)
        txtAdd2.Margin = New Padding(5, 6, 5, 6)
        txtAdd2.MaxLength = 35
        txtAdd2.Name = "txtAdd2"
        txtAdd2.Size = New Size(220, 31)
        txtAdd2.TabIndex = 21
        ToolTip1.SetToolTip(txtAdd2, "Optional field")
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.Fuchsia
        Label6.Location = New Point(499, 377)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(169, 25)
        Label6.TabIndex = 47
        Label6.Text = "Address Line 1"
        ' 
        ' txtAdd1
        ' 
        txtAdd1.AcceptsReturn = True
        txtAdd1.Location = New Point(486, 408)
        txtAdd1.Margin = New Padding(5, 6, 5, 6)
        txtAdd1.MaxLength = 35
        txtAdd1.Name = "txtAdd1"
        txtAdd1.Size = New Size(220, 31)
        txtAdd1.TabIndex = 20
        ToolTip1.SetToolTip(txtAdd1, "Conditionally required address field")
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.Red
        Label5.Location = New Point(341, 106)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(79, 25)
        Label5.TabIndex = 44
        Label5.Text = "D.O.B"
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(569, 17)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(131, 25)
        Label4.TabIndex = 41
        Label4.Text = "Middle Name"
        ' 
        ' txtMName
        ' 
        txtMName.AcceptsReturn = True
        txtMName.Location = New Point(569, 48)
        txtMName.Margin = New Padding(5, 6, 5, 6)
        txtMName.MaxLength = 35
        txtMName.Name = "txtMName"
        txtMName.Size = New Size(155, 31)
        txtMName.TabIndex = 7
        ToolTip1.SetToolTip(txtMName, "Optional field")
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.Red
        Label3.Location = New Point(400, 17)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(114, 25)
        Label3.TabIndex = 38
        Label3.Text = "First Name"
        ' 
        ' txtFName
        ' 
        txtFName.AcceptsReturn = True
        txtFName.BackColor = Color.LavenderBlush
        txtFName.Location = New Point(395, 48)
        txtFName.Margin = New Padding(5, 6, 5, 6)
        txtFName.MaxLength = 35
        txtFName.Name = "txtFName"
        txtFName.Size = New Size(160, 31)
        txtFName.TabIndex = 4
        ToolTip1.SetToolTip(txtFName, "Required and Searchable field")
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.Red
        Label2.Location = New Point(216, 17)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(121, 25)
        Label2.TabIndex = 35
        Label2.Text = "Last Name"
        ' 
        ' txtLName
        ' 
        txtLName.AcceptsReturn = True
        txtLName.BackColor = Color.LavenderBlush
        txtLName.Location = New Point(221, 48)
        txtLName.Margin = New Padding(5, 6, 5, 6)
        txtLName.MaxLength = 35
        txtLName.Name = "txtLName"
        txtLName.Size = New Size(160, 31)
        txtLName.TabIndex = 3
        ToolTip1.SetToolTip(txtLName, "Required and Searchable field")
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.Red
        Label1.Location = New Point(29, 17)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(104, 25)
        Label1.TabIndex = 32
        Label1.Text = "Patient ID"
        ' 
        ' txtPatientID
        ' 
        txtPatientID.AcceptsReturn = True
        txtPatientID.BackColor = Color.LavenderBlush
        txtPatientID.Location = New Point(29, 48)
        txtPatientID.Margin = New Padding(5, 6, 5, 6)
        txtPatientID.MaxLength = 12
        txtPatientID.Name = "txtPatientID"
        txtPatientID.ReadOnly = True
        txtPatientID.Size = New Size(124, 31)
        txtPatientID.TabIndex = 1
        txtPatientID.TabStop = False
        txtPatientID.TextAlign = HorizontalAlignment.Center
        ToolTip1.SetToolTip(txtPatientID, "Required and Searchable numerical field")
        ' 
        ' tpPrimary
        ' 
        tpPrimary.AutoScroll = True
        tpPrimary.Controls.Add(grpPSubs)
        tpPrimary.Controls.Add(grpPrimary)
        tpPrimary.Location = New Point(4, 34)
        tpPrimary.Margin = New Padding(5, 6, 5, 6)
        tpPrimary.Name = "tpPrimary"
        tpPrimary.Padding = New Padding(5, 6, 5, 6)
        tpPrimary.Size = New Size(987, 756)
        tpPrimary.TabIndex = 1
        tpPrimary.Text = "Primary Coverage"
        tpPrimary.UseVisualStyleBackColor = True
        ' 
        ' grpPSubs
        ' 
        grpPSubs.Controls.Add(Label81)
        grpPSubs.Controls.Add(txtPSubTage)
        grpPSubs.Controls.Add(Label74)
        grpPSubs.Controls.Add(Label59)
        grpPSubs.Controls.Add(Label55)
        grpPSubs.Controls.Add(Label53)
        grpPSubs.Controls.Add(txtPSubEmployer)
        grpPSubs.Controls.Add(txtPSubFax)
        grpPSubs.Controls.Add(txtPSubCell)
        grpPSubs.Controls.Add(txtPSubWPhone)
        grpPSubs.Controls.Add(txtPSubLName)
        grpPSubs.Controls.Add(Label56)
        grpPSubs.Controls.Add(txtPSubSSN)
        grpPSubs.Controls.Add(Label25)
        grpPSubs.Controls.Add(txtPSubDOB)
        grpPSubs.Controls.Add(cmbPSubSex)
        grpPSubs.Controls.Add(btnPSubLook)
        grpPSubs.Controls.Add(Label26)
        grpPSubs.Controls.Add(txtPSubCountry)
        grpPSubs.Controls.Add(Label27)
        grpPSubs.Controls.Add(txtPSubEmail)
        grpPSubs.Controls.Add(Label28)
        grpPSubs.Controls.Add(txtPSubHPhone)
        grpPSubs.Controls.Add(Label29)
        grpPSubs.Controls.Add(txtPSubZip)
        grpPSubs.Controls.Add(Label30)
        grpPSubs.Controls.Add(txtPSubState)
        grpPSubs.Controls.Add(Label31)
        grpPSubs.Controls.Add(txtPSubCity)
        grpPSubs.Controls.Add(Label32)
        grpPSubs.Controls.Add(txtPSubAdd2)
        grpPSubs.Controls.Add(Label33)
        grpPSubs.Controls.Add(txtPSubAdd1)
        grpPSubs.Controls.Add(Label34)
        grpPSubs.Controls.Add(Label35)
        grpPSubs.Controls.Add(txtPSubMName)
        grpPSubs.Controls.Add(Label36)
        grpPSubs.Controls.Add(txtPSubFName)
        grpPSubs.Controls.Add(Label37)
        grpPSubs.Controls.Add(Label38)
        grpPSubs.Controls.Add(txtPSubID)
        grpPSubs.Location = New Point(10, 202)
        grpPSubs.Margin = New Padding(5, 6, 5, 6)
        grpPSubs.Name = "grpPSubs"
        grpPSubs.Padding = New Padding(5, 6, 5, 6)
        grpPSubs.Size = New Size(931, 492)
        grpPSubs.TabIndex = 28
        grpPSubs.TabStop = False
        grpPSubs.Text = "Primary Subscriber"
        ' 
        ' Label81
        ' 
        Label81.Location = New Point(364, 116)
        Label81.Margin = New Padding(5, 0, 5, 0)
        Label81.Name = "Label81"
        Label81.Size = New Size(115, 25)
        Label81.TabIndex = 99
        Label81.Text = "Tage (Years)"
        ' 
        ' txtPSubTage
        ' 
        txtPSubTage.AcceptsReturn = True
        txtPSubTage.Location = New Point(364, 144)
        txtPSubTage.Margin = New Padding(5, 6, 5, 6)
        txtPSubTage.MaxLength = 13
        txtPSubTage.Name = "txtPSubTage"
        txtPSubTage.ReadOnly = True
        txtPSubTage.Size = New Size(99, 31)
        txtPSubTage.TabIndex = 38
        txtPSubTage.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label74
        ' 
        Label74.ForeColor = Color.DarkBlue
        Label74.Location = New Point(729, 216)
        Label74.Margin = New Padding(5, 0, 5, 0)
        Label74.Name = "Label74"
        Label74.Size = New Size(130, 25)
        Label74.TabIndex = 97
        Label74.Text = "Employer"
        ' 
        ' Label59
        ' 
        Label59.ForeColor = Color.DarkBlue
        Label59.Location = New Point(379, 216)
        Label59.Margin = New Padding(5, 0, 5, 0)
        Label59.Name = "Label59"
        Label59.Size = New Size(100, 25)
        Label59.TabIndex = 96
        Label59.Text = "Fax"
        ' 
        ' Label55
        ' 
        Label55.ForeColor = Color.DarkBlue
        Label55.Location = New Point(239, 216)
        Label55.Margin = New Padding(5, 0, 5, 0)
        Label55.Name = "Label55"
        Label55.Size = New Size(100, 25)
        Label55.TabIndex = 95
        Label55.Text = "Work Phone"
        ' 
        ' Label53
        ' 
        Label53.ForeColor = Color.DarkBlue
        Label53.Location = New Point(40, 216)
        Label53.Margin = New Padding(5, 0, 5, 0)
        Label53.Name = "Label53"
        Label53.Size = New Size(130, 25)
        Label53.TabIndex = 94
        Label53.Text = "Cell"
        ' 
        ' txtPSubEmployer
        ' 
        txtPSubEmployer.AcceptsReturn = True
        txtPSubEmployer.Location = New Point(706, 248)
        txtPSubEmployer.Margin = New Padding(5, 6, 5, 6)
        txtPSubEmployer.MaxLength = 60
        txtPSubEmployer.Name = "txtPSubEmployer"
        txtPSubEmployer.Size = New Size(205, 31)
        txtPSubEmployer.TabIndex = 46
        ' 
        ' txtPSubFax
        ' 
        txtPSubFax.AcceptsReturn = True
        txtPSubFax.Location = New Point(361, 248)
        txtPSubFax.Margin = New Padding(5, 6, 5, 6)
        txtPSubFax.MaxLength = 13
        txtPSubFax.Name = "txtPSubFax"
        txtPSubFax.Size = New Size(140, 31)
        txtPSubFax.TabIndex = 44
        ' 
        ' txtPSubCell
        ' 
        txtPSubCell.AcceptsReturn = True
        txtPSubCell.Location = New Point(25, 248)
        txtPSubCell.Margin = New Padding(5, 6, 5, 6)
        txtPSubCell.MaxLength = 13
        txtPSubCell.Name = "txtPSubCell"
        txtPSubCell.Size = New Size(164, 31)
        txtPSubCell.TabIndex = 42
        ' 
        ' txtPSubWPhone
        ' 
        txtPSubWPhone.AcceptsReturn = True
        txtPSubWPhone.Location = New Point(210, 248)
        txtPSubWPhone.Margin = New Padding(5, 6, 5, 6)
        txtPSubWPhone.MaxLength = 13
        txtPSubWPhone.Name = "txtPSubWPhone"
        txtPSubWPhone.Size = New Size(139, 31)
        txtPSubWPhone.TabIndex = 43
        ' 
        ' txtPSubLName
        ' 
        txtPSubLName.AcceptsReturn = True
        txtPSubLName.Location = New Point(239, 61)
        txtPSubLName.Margin = New Padding(5, 6, 5, 6)
        txtPSubLName.Name = "txtPSubLName"
        txtPSubLName.Size = New Size(224, 31)
        txtPSubLName.TabIndex = 34
        ' 
        ' Label56
        ' 
        Label56.ForeColor = Color.DarkBlue
        Label56.Location = New Point(640, 119)
        Label56.Margin = New Padding(5, 0, 5, 0)
        Label56.Name = "Label56"
        Label56.Size = New Size(89, 23)
        Label56.TabIndex = 93
        Label56.Text = "SSN"
        ' 
        ' txtPSubSSN
        ' 
        txtPSubSSN.Location = New Point(629, 148)
        txtPSubSSN.Margin = New Padding(5, 6, 5, 6)
        txtPSubSSN.Mask = "000-00-0000"
        txtPSubSSN.Name = "txtPSubSSN"
        txtPSubSSN.Size = New Size(123, 31)
        txtPSubSSN.TabIndex = 40
        ' 
        ' Label25
        ' 
        Label25.ForeColor = Color.DarkBlue
        Label25.Location = New Point(20, 116)
        Label25.Margin = New Padding(5, 0, 5, 0)
        Label25.Name = "Label25"
        Label25.Size = New Size(121, 25)
        Label25.TabIndex = 90
        Label25.Text = "Gender"
        ' 
        ' txtPSubDOB
        ' 
        txtPSubDOB.Location = New Point(491, 148)
        txtPSubDOB.Margin = New Padding(5, 6, 5, 6)
        txtPSubDOB.Mask = "00/00/0000"
        txtPSubDOB.Name = "txtPSubDOB"
        txtPSubDOB.Size = New Size(124, 31)
        txtPSubDOB.TabIndex = 39
        txtPSubDOB.ValidatingType = GetType(Date)
        ' 
        ' cmbPSubSex
        ' 
        cmbPSubSex.DropDownStyle = ComboBoxStyle.DropDownList
        cmbPSubSex.FormattingEnabled = True
        cmbPSubSex.Items.AddRange(New Object() {"Female", "Male", "Indetermined", "Unreported"})
        cmbPSubSex.Location = New Point(25, 144)
        cmbPSubSex.Margin = New Padding(5, 6, 5, 6)
        cmbPSubSex.Name = "cmbPSubSex"
        cmbPSubSex.Size = New Size(298, 33)
        cmbPSubSex.TabIndex = 37
        ' 
        ' btnPSubLook
        ' 
        btnPSubLook.Image = CType(resources.GetObject("btnPSubLook.Image"), Image)
        btnPSubLook.Location = New Point(185, 52)
        btnPSubLook.Margin = New Padding(5, 6, 5, 6)
        btnPSubLook.Name = "btnPSubLook"
        btnPSubLook.Size = New Size(44, 50)
        btnPSubLook.TabIndex = 33
        btnPSubLook.UseVisualStyleBackColor = True
        ' 
        ' Label26
        ' 
        Label26.ForeColor = Color.Fuchsia
        Label26.Location = New Point(729, 392)
        Label26.Margin = New Padding(5, 0, 5, 0)
        Label26.Name = "Label26"
        Label26.Size = New Size(146, 25)
        Label26.TabIndex = 86
        Label26.Text = "Country"
        ' 
        ' txtPSubCountry
        ' 
        txtPSubCountry.AcceptsReturn = True
        txtPSubCountry.Location = New Point(706, 423)
        txtPSubCountry.Margin = New Padding(5, 6, 5, 6)
        txtPSubCountry.MaxLength = 35
        txtPSubCountry.Name = "txtPSubCountry"
        txtPSubCountry.Size = New Size(205, 31)
        txtPSubCountry.TabIndex = 52
        ' 
        ' Label27
        ' 
        Label27.ForeColor = Color.DarkBlue
        Label27.Location = New Point(534, 216)
        Label27.Margin = New Padding(5, 0, 5, 0)
        Label27.Name = "Label27"
        Label27.Size = New Size(139, 25)
        Label27.TabIndex = 85
        Label27.Text = "Email Address"
        ' 
        ' txtPSubEmail
        ' 
        txtPSubEmail.AcceptsReturn = True
        txtPSubEmail.Location = New Point(515, 248)
        txtPSubEmail.Margin = New Padding(5, 6, 5, 6)
        txtPSubEmail.MaxLength = 25
        txtPSubEmail.Name = "txtPSubEmail"
        txtPSubEmail.Size = New Size(175, 31)
        txtPSubEmail.TabIndex = 45
        ' 
        ' Label28
        ' 
        Label28.ForeColor = Color.DarkBlue
        Label28.Location = New Point(764, 116)
        Label28.Margin = New Padding(5, 0, 5, 0)
        Label28.Name = "Label28"
        Label28.Size = New Size(136, 25)
        Label28.TabIndex = 84
        Label28.Text = "Home Phone"
        ' 
        ' txtPSubHPhone
        ' 
        txtPSubHPhone.AcceptsReturn = True
        txtPSubHPhone.Location = New Point(764, 148)
        txtPSubHPhone.Margin = New Padding(5, 6, 5, 6)
        txtPSubHPhone.MaxLength = 25
        txtPSubHPhone.Name = "txtPSubHPhone"
        txtPSubHPhone.Size = New Size(149, 31)
        txtPSubHPhone.TabIndex = 41
        ' 
        ' Label29
        ' 
        Label29.ForeColor = Color.Fuchsia
        Label29.Location = New Point(551, 392)
        Label29.Margin = New Padding(5, 0, 5, 0)
        Label29.Name = "Label29"
        Label29.Size = New Size(120, 25)
        Label29.TabIndex = 83
        Label29.Text = "Zip"
        ' 
        ' txtPSubZip
        ' 
        txtPSubZip.AcceptsReturn = True
        txtPSubZip.Location = New Point(534, 423)
        txtPSubZip.Margin = New Padding(5, 6, 5, 6)
        txtPSubZip.MaxLength = 25
        txtPSubZip.Name = "txtPSubZip"
        txtPSubZip.Size = New Size(158, 31)
        txtPSubZip.TabIndex = 51
        ' 
        ' Label30
        ' 
        Label30.ForeColor = Color.Fuchsia
        Label30.Location = New Point(290, 392)
        Label30.Margin = New Padding(5, 0, 5, 0)
        Label30.Name = "Label30"
        Label30.Size = New Size(215, 25)
        Label30.TabIndex = 82
        Label30.Text = "State/Province"
        ' 
        ' txtPSubState
        ' 
        txtPSubState.AcceptsReturn = True
        txtPSubState.Location = New Point(270, 423)
        txtPSubState.Margin = New Padding(5, 6, 5, 6)
        txtPSubState.MaxLength = 35
        txtPSubState.Name = "txtPSubState"
        txtPSubState.Size = New Size(250, 31)
        txtPSubState.TabIndex = 50
        ' 
        ' Label31
        ' 
        Label31.ForeColor = Color.Fuchsia
        Label31.Location = New Point(49, 392)
        Label31.Margin = New Padding(5, 0, 5, 0)
        Label31.Name = "Label31"
        Label31.Size = New Size(75, 25)
        Label31.TabIndex = 81
        Label31.Text = "City"
        ' 
        ' txtPSubCity
        ' 
        txtPSubCity.AcceptsReturn = True
        txtPSubCity.Location = New Point(25, 423)
        txtPSubCity.Margin = New Padding(5, 6, 5, 6)
        txtPSubCity.MaxLength = 35
        txtPSubCity.Name = "txtPSubCity"
        txtPSubCity.Size = New Size(233, 31)
        txtPSubCity.TabIndex = 49
        ' 
        ' Label32
        ' 
        Label32.ForeColor = Color.DarkBlue
        Label32.Location = New Point(295, 309)
        Label32.Margin = New Padding(5, 0, 5, 0)
        Label32.Name = "Label32"
        Label32.Size = New Size(184, 25)
        Label32.TabIndex = 80
        Label32.Text = "Address Line 2"
        ' 
        ' txtPSubAdd2
        ' 
        txtPSubAdd2.AcceptsReturn = True
        txtPSubAdd2.Location = New Point(270, 341)
        txtPSubAdd2.Margin = New Padding(5, 6, 5, 6)
        txtPSubAdd2.MaxLength = 35
        txtPSubAdd2.Name = "txtPSubAdd2"
        txtPSubAdd2.Size = New Size(250, 31)
        txtPSubAdd2.TabIndex = 48
        ' 
        ' Label33
        ' 
        Label33.ForeColor = Color.Fuchsia
        Label33.Location = New Point(20, 309)
        Label33.Margin = New Padding(5, 0, 5, 0)
        Label33.Name = "Label33"
        Label33.Size = New Size(189, 25)
        Label33.TabIndex = 78
        Label33.Text = "Address Line 1"
        ' 
        ' txtPSubAdd1
        ' 
        txtPSubAdd1.AcceptsReturn = True
        txtPSubAdd1.Location = New Point(25, 341)
        txtPSubAdd1.Margin = New Padding(5, 6, 5, 6)
        txtPSubAdd1.MaxLength = 35
        txtPSubAdd1.Name = "txtPSubAdd1"
        txtPSubAdd1.Size = New Size(233, 31)
        txtPSubAdd1.TabIndex = 47
        ' 
        ' Label34
        ' 
        Label34.ForeColor = Color.DarkBlue
        Label34.Location = New Point(504, 119)
        Label34.Margin = New Padding(5, 0, 5, 0)
        Label34.Name = "Label34"
        Label34.Size = New Size(115, 25)
        Label34.TabIndex = 75
        Label34.Text = "D.O.B"
        ' 
        ' Label35
        ' 
        Label35.ForeColor = Color.DarkBlue
        Label35.Location = New Point(750, 31)
        Label35.Margin = New Padding(5, 0, 5, 0)
        Label35.Name = "Label35"
        Label35.Size = New Size(150, 25)
        Label35.TabIndex = 72
        Label35.Text = "Middle Name"
        ' 
        ' txtPSubMName
        ' 
        txtPSubMName.Location = New Point(740, 61)
        txtPSubMName.Margin = New Padding(5, 6, 5, 6)
        txtPSubMName.MaxLength = 35
        txtPSubMName.Name = "txtPSubMName"
        txtPSubMName.Size = New Size(173, 31)
        txtPSubMName.TabIndex = 36
        ' 
        ' Label36
        ' 
        Label36.ForeColor = Color.DarkBlue
        Label36.Location = New Point(490, 31)
        Label36.Margin = New Padding(5, 0, 5, 0)
        Label36.Name = "Label36"
        Label36.Size = New Size(154, 25)
        Label36.TabIndex = 69
        Label36.Text = "First Name"
        ' 
        ' txtPSubFName
        ' 
        txtPSubFName.AcceptsReturn = True
        txtPSubFName.Location = New Point(490, 61)
        txtPSubFName.Margin = New Padding(5, 6, 5, 6)
        txtPSubFName.MaxLength = 35
        txtPSubFName.Name = "txtPSubFName"
        txtPSubFName.Size = New Size(219, 31)
        txtPSubFName.TabIndex = 35
        ' 
        ' Label37
        ' 
        Label37.ForeColor = Color.DarkBlue
        Label37.Location = New Point(239, 31)
        Label37.Margin = New Padding(5, 0, 5, 0)
        Label37.Name = "Label37"
        Label37.Size = New Size(179, 25)
        Label37.TabIndex = 67
        Label37.Text = "Last Name"
        ' 
        ' Label38
        ' 
        Label38.ForeColor = Color.Fuchsia
        Label38.Location = New Point(25, 31)
        Label38.Margin = New Padding(5, 0, 5, 0)
        Label38.Name = "Label38"
        Label38.Size = New Size(145, 25)
        Label38.TabIndex = 64
        Label38.Text = "Subscriber ID"
        ' 
        ' txtPSubID
        ' 
        txtPSubID.AcceptsReturn = True
        txtPSubID.Location = New Point(25, 61)
        txtPSubID.Margin = New Padding(5, 6, 5, 6)
        txtPSubID.MaxLength = 12
        txtPSubID.Name = "txtPSubID"
        txtPSubID.ReadOnly = True
        txtPSubID.Size = New Size(148, 31)
        txtPSubID.TabIndex = 32
        txtPSubID.TabStop = False
        txtPSubID.TextAlign = HorizontalAlignment.Center
        ToolTip1.SetToolTip(txtPSubID, "Conditionally required field")
        ' 
        ' grpPrimary
        ' 
        grpPrimary.Controls.Add(btnDelPrime)
        grpPrimary.Controls.Add(txtPCopay)
        grpPrimary.Controls.Add(Label58)
        grpPrimary.Controls.Add(Label39)
        grpPrimary.Controls.Add(cmbPRelation)
        grpPrimary.Controls.Add(Label19)
        grpPrimary.Controls.Add(txtPFrom)
        grpPrimary.Controls.Add(btnPIns)
        grpPrimary.Controls.Add(Label18)
        grpPrimary.Controls.Add(cmbPIns)
        grpPrimary.Controls.Add(txtPGroup)
        grpPrimary.Controls.Add(txtPTo)
        grpPrimary.Controls.Add(Label17)
        grpPrimary.Controls.Add(Label15)
        grpPrimary.Controls.Add(txtPPolicy)
        grpPrimary.Controls.Add(Label16)
        grpPrimary.Location = New Point(10, 11)
        grpPrimary.Margin = New Padding(5, 6, 5, 6)
        grpPrimary.Name = "grpPrimary"
        grpPrimary.Padding = New Padding(5, 6, 5, 6)
        grpPrimary.Size = New Size(931, 186)
        grpPrimary.TabIndex = 19
        grpPrimary.TabStop = False
        grpPrimary.Text = "Primary Insurance"
        ' 
        ' btnDelPrime
        ' 
        btnDelPrime.Image = CType(resources.GetObject("btnDelPrime.Image"), Image)
        btnDelPrime.Location = New Point(864, 44)
        btnDelPrime.Margin = New Padding(5, 6, 5, 6)
        btnDelPrime.Name = "btnDelPrime"
        btnDelPrime.Size = New Size(51, 52)
        btnDelPrime.TabIndex = 70
        btnDelPrime.TabStop = False
        btnDelPrime.UseVisualStyleBackColor = True
        ' 
        ' txtPCopay
        ' 
        txtPCopay.AcceptsReturn = True
        txtPCopay.Location = New Point(706, 136)
        txtPCopay.Margin = New Padding(5, 6, 5, 6)
        txtPCopay.MaxLength = 6
        txtPCopay.Name = "txtPCopay"
        txtPCopay.Size = New Size(190, 31)
        txtPCopay.TabIndex = 31
        ' 
        ' Label58
        ' 
        Label58.ForeColor = Color.DarkBlue
        Label58.Location = New Point(729, 106)
        Label58.Margin = New Padding(5, 0, 5, 0)
        Label58.Name = "Label58"
        Label58.Size = New Size(99, 25)
        Label58.TabIndex = 69
        Label58.Text = "Copay"
        ' 
        ' Label39
        ' 
        Label39.ForeColor = Color.Fuchsia
        Label39.Location = New Point(490, 106)
        Label39.Margin = New Padding(5, 0, 5, 0)
        Label39.Name = "Label39"
        Label39.Size = New Size(135, 25)
        Label39.TabIndex = 67
        Label39.Text = "Relation"
        ' 
        ' cmbPRelation
        ' 
        cmbPRelation.DropDownStyle = ComboBoxStyle.DropDownList
        cmbPRelation.FormattingEnabled = True
        cmbPRelation.Items.AddRange(New Object() {"Self", "Spouse", "Son/Daughter", "Other Dependent"})
        cmbPRelation.Location = New Point(490, 136)
        cmbPRelation.Margin = New Padding(5, 6, 5, 6)
        cmbPRelation.Name = "cmbPRelation"
        cmbPRelation.Size = New Size(200, 33)
        cmbPRelation.TabIndex = 30
        ' 
        ' Label19
        ' 
        Label19.ForeColor = Color.Fuchsia
        Label19.Location = New Point(25, 23)
        Label19.Margin = New Padding(5, 0, 5, 0)
        Label19.Name = "Label19"
        Label19.Size = New Size(220, 25)
        Label19.TabIndex = 60
        Label19.Text = "Insurance Name"
        ' 
        ' txtPFrom
        ' 
        txtPFrom.Location = New Point(209, 136)
        txtPFrom.Margin = New Padding(5, 6, 5, 6)
        txtPFrom.Mask = "00/00/0000"
        txtPFrom.Name = "txtPFrom"
        txtPFrom.Size = New Size(128, 31)
        txtPFrom.TabIndex = 28
        txtPFrom.ValidatingType = GetType(Date)
        ' 
        ' btnPIns
        ' 
        btnPIns.Image = CType(resources.GetObject("btnPIns.Image"), Image)
        btnPIns.Location = New Point(515, 48)
        btnPIns.Margin = New Padding(5, 6, 5, 6)
        btnPIns.Name = "btnPIns"
        btnPIns.Size = New Size(44, 50)
        btnPIns.TabIndex = 25
        btnPIns.UseVisualStyleBackColor = True
        ' 
        ' Label18
        ' 
        Label18.ForeColor = Color.DarkBlue
        Label18.Location = New Point(204, 106)
        Label18.Margin = New Padding(5, 0, 5, 0)
        Label18.Name = "Label18"
        Label18.Size = New Size(121, 25)
        Label18.TabIndex = 64
        Label18.Text = "Effective "
        ' 
        ' cmbPIns
        ' 
        cmbPIns.DropDownStyle = ComboBoxStyle.DropDownList
        cmbPIns.FormattingEnabled = True
        cmbPIns.Location = New Point(10, 52)
        cmbPIns.Margin = New Padding(5, 6, 5, 6)
        cmbPIns.Name = "cmbPIns"
        cmbPIns.Size = New Size(493, 33)
        cmbPIns.Sorted = True
        cmbPIns.TabIndex = 24
        ' 
        ' txtPGroup
        ' 
        txtPGroup.AcceptsReturn = True
        txtPGroup.Location = New Point(10, 136)
        txtPGroup.Margin = New Padding(5, 6, 5, 6)
        txtPGroup.MaxLength = 35
        txtPGroup.Name = "txtPGroup"
        txtPGroup.Size = New Size(179, 31)
        txtPGroup.TabIndex = 27
        ' 
        ' txtPTo
        ' 
        txtPTo.Location = New Point(349, 136)
        txtPTo.Margin = New Padding(5, 6, 5, 6)
        txtPTo.Mask = "00/00/0000"
        txtPTo.Name = "txtPTo"
        txtPTo.Size = New Size(128, 31)
        txtPTo.TabIndex = 29
        txtPTo.ValidatingType = GetType(Date)
        ' 
        ' Label17
        ' 
        Label17.ForeColor = Color.DarkBlue
        Label17.Location = New Point(356, 106)
        Label17.Margin = New Padding(5, 0, 5, 0)
        Label17.Name = "Label17"
        Label17.Size = New Size(109, 25)
        Label17.TabIndex = 62
        Label17.Text = "Expires"
        ' 
        ' Label15
        ' 
        Label15.ForeColor = Color.Fuchsia
        Label15.Location = New Point(589, 23)
        Label15.Margin = New Padding(5, 0, 5, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(121, 25)
        Label15.TabIndex = 59
        Label15.Text = "Policy"
        ' 
        ' txtPPolicy
        ' 
        txtPPolicy.AcceptsReturn = True
        txtPPolicy.Location = New Point(569, 52)
        txtPPolicy.Margin = New Padding(5, 6, 5, 6)
        txtPPolicy.MaxLength = 35
        txtPPolicy.Name = "txtPPolicy"
        txtPPolicy.Size = New Size(258, 31)
        txtPPolicy.TabIndex = 26
        ' 
        ' Label16
        ' 
        Label16.ForeColor = Color.DarkBlue
        Label16.Location = New Point(25, 106)
        Label16.Margin = New Padding(5, 0, 5, 0)
        Label16.Name = "Label16"
        Label16.Size = New Size(145, 25)
        Label16.TabIndex = 61
        Label16.Text = "Group"
        ' 
        ' tpSecondary
        ' 
        tpSecondary.AutoScroll = True
        tpSecondary.Controls.Add(GroupBox1)
        tpSecondary.Controls.Add(grpSSubs)
        tpSecondary.Location = New Point(4, 34)
        tpSecondary.Margin = New Padding(5, 6, 5, 6)
        tpSecondary.Name = "tpSecondary"
        tpSecondary.Size = New Size(987, 756)
        tpSecondary.TabIndex = 2
        tpSecondary.Text = "Secondary Coverage"
        tpSecondary.UseVisualStyleBackColor = True
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(btnDelSecond)
        GroupBox1.Controls.Add(txtSCopay)
        GroupBox1.Controls.Add(Label66)
        GroupBox1.Controls.Add(Label68)
        GroupBox1.Controls.Add(cmbSRelation)
        GroupBox1.Controls.Add(Label69)
        GroupBox1.Controls.Add(txtSFrom)
        GroupBox1.Controls.Add(txtSTo)
        GroupBox1.Controls.Add(btnSIns)
        GroupBox1.Controls.Add(txtSGroup)
        GroupBox1.Controls.Add(Label70)
        GroupBox1.Controls.Add(cmbSIns)
        GroupBox1.Controls.Add(txtSPolicy)
        GroupBox1.Controls.Add(Label71)
        GroupBox1.Controls.Add(Label72)
        GroupBox1.Controls.Add(Label73)
        GroupBox1.Location = New Point(11, 6)
        GroupBox1.Margin = New Padding(5, 6, 5, 6)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(5, 6, 5, 6)
        GroupBox1.Size = New Size(935, 186)
        GroupBox1.TabIndex = 55
        GroupBox1.TabStop = False
        GroupBox1.Text = "Secondary Insurance"
        ' 
        ' btnDelSecond
        ' 
        btnDelSecond.Image = CType(resources.GetObject("btnDelSecond.Image"), Image)
        btnDelSecond.Location = New Point(854, 42)
        btnDelSecond.Margin = New Padding(5, 6, 5, 6)
        btnDelSecond.Name = "btnDelSecond"
        btnDelSecond.Size = New Size(51, 52)
        btnDelSecond.TabIndex = 56
        btnDelSecond.TabStop = False
        btnDelSecond.UseVisualStyleBackColor = True
        ' 
        ' txtSCopay
        ' 
        txtSCopay.AcceptsReturn = True
        txtSCopay.Location = New Point(709, 136)
        txtSCopay.Margin = New Padding(5, 6, 5, 6)
        txtSCopay.MaxLength = 6
        txtSCopay.Name = "txtSCopay"
        txtSCopay.Size = New Size(194, 31)
        txtSCopay.TabIndex = 61
        ' 
        ' Label66
        ' 
        Label66.ForeColor = Color.DarkBlue
        Label66.Location = New Point(726, 106)
        Label66.Margin = New Padding(5, 0, 5, 0)
        Label66.Name = "Label66"
        Label66.Size = New Size(99, 25)
        Label66.TabIndex = 69
        Label66.Text = "Copay"
        ' 
        ' Label68
        ' 
        Label68.ForeColor = Color.Fuchsia
        Label68.Location = New Point(504, 106)
        Label68.Margin = New Padding(5, 0, 5, 0)
        Label68.Name = "Label68"
        Label68.Size = New Size(121, 25)
        Label68.TabIndex = 67
        Label68.Text = "Relation"
        ' 
        ' cmbSRelation
        ' 
        cmbSRelation.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSRelation.FormattingEnabled = True
        cmbSRelation.Items.AddRange(New Object() {"Self", "Spouse", "Son/Daughter", "Other Dependent"})
        cmbSRelation.Location = New Point(490, 136)
        cmbSRelation.Margin = New Padding(5, 6, 5, 6)
        cmbSRelation.Name = "cmbSRelation"
        cmbSRelation.Size = New Size(205, 33)
        cmbSRelation.TabIndex = 60
        ' 
        ' Label69
        ' 
        Label69.ForeColor = Color.Fuchsia
        Label69.Location = New Point(10, 23)
        Label69.Margin = New Padding(5, 0, 5, 0)
        Label69.Name = "Label69"
        Label69.Size = New Size(235, 25)
        Label69.TabIndex = 60
        Label69.Text = "Insurance Name"
        ' 
        ' txtSFrom
        ' 
        txtSFrom.Location = New Point(209, 136)
        txtSFrom.Margin = New Padding(5, 6, 5, 6)
        txtSFrom.Mask = "00/00/0000"
        txtSFrom.Name = "txtSFrom"
        txtSFrom.Size = New Size(128, 31)
        txtSFrom.TabIndex = 58
        txtSFrom.ValidatingType = GetType(Date)
        ' 
        ' txtSTo
        ' 
        txtSTo.Location = New Point(349, 136)
        txtSTo.Margin = New Padding(5, 6, 5, 6)
        txtSTo.Mask = "00/00/0000"
        txtSTo.Name = "txtSTo"
        txtSTo.Size = New Size(129, 31)
        txtSTo.TabIndex = 59
        txtSTo.ValidatingType = GetType(Date)
        ' 
        ' btnSIns
        ' 
        btnSIns.Image = CType(resources.GetObject("btnSIns.Image"), Image)
        btnSIns.Location = New Point(504, 44)
        btnSIns.Margin = New Padding(5, 6, 5, 6)
        btnSIns.Name = "btnSIns"
        btnSIns.Size = New Size(44, 50)
        btnSIns.TabIndex = 54
        btnSIns.UseVisualStyleBackColor = True
        ' 
        ' txtSGroup
        ' 
        txtSGroup.AcceptsReturn = True
        txtSGroup.Location = New Point(10, 136)
        txtSGroup.Margin = New Padding(5, 6, 5, 6)
        txtSGroup.MaxLength = 35
        txtSGroup.Name = "txtSGroup"
        txtSGroup.Size = New Size(180, 31)
        txtSGroup.TabIndex = 57
        ' 
        ' Label70
        ' 
        Label70.ForeColor = Color.DarkBlue
        Label70.Location = New Point(204, 106)
        Label70.Margin = New Padding(5, 0, 5, 0)
        Label70.Name = "Label70"
        Label70.Size = New Size(121, 25)
        Label70.TabIndex = 64
        Label70.Text = "Effective "
        ' 
        ' cmbSIns
        ' 
        cmbSIns.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSIns.FormattingEnabled = True
        cmbSIns.Location = New Point(10, 52)
        cmbSIns.Margin = New Padding(5, 6, 5, 6)
        cmbSIns.Name = "cmbSIns"
        cmbSIns.Size = New Size(480, 33)
        cmbSIns.Sorted = True
        cmbSIns.TabIndex = 53
        ' 
        ' txtSPolicy
        ' 
        txtSPolicy.AcceptsReturn = True
        txtSPolicy.Location = New Point(556, 52)
        txtSPolicy.Margin = New Padding(5, 6, 5, 6)
        txtSPolicy.MaxLength = 35
        txtSPolicy.Name = "txtSPolicy"
        txtSPolicy.Size = New Size(265, 31)
        txtSPolicy.TabIndex = 55
        ' 
        ' Label71
        ' 
        Label71.ForeColor = Color.DarkBlue
        Label71.Location = New Point(356, 106)
        Label71.Margin = New Padding(5, 0, 5, 0)
        Label71.Name = "Label71"
        Label71.Size = New Size(109, 25)
        Label71.TabIndex = 62
        Label71.Text = "Expires"
        ' 
        ' Label72
        ' 
        Label72.ForeColor = Color.Fuchsia
        Label72.Location = New Point(579, 23)
        Label72.Margin = New Padding(5, 0, 5, 0)
        Label72.Name = "Label72"
        Label72.Size = New Size(125, 25)
        Label72.TabIndex = 59
        Label72.Text = "Policy"
        ' 
        ' Label73
        ' 
        Label73.ForeColor = Color.DarkBlue
        Label73.Location = New Point(24, 106)
        Label73.Margin = New Padding(5, 0, 5, 0)
        Label73.Name = "Label73"
        Label73.Size = New Size(146, 25)
        Label73.TabIndex = 61
        Label73.Text = "Group"
        ' 
        ' grpSSubs
        ' 
        grpSSubs.Controls.Add(Label82)
        grpSSubs.Controls.Add(txtSSubTage)
        grpSSubs.Controls.Add(Label52)
        grpSSubs.Controls.Add(Label51)
        grpSSubs.Controls.Add(Label50)
        grpSSubs.Controls.Add(txtSSubEmployer)
        grpSSubs.Controls.Add(txtSSubFax)
        grpSSubs.Controls.Add(Label46)
        grpSSubs.Controls.Add(Label57)
        grpSSubs.Controls.Add(txtSSubCell)
        grpSSubs.Controls.Add(txtSSubSSN)
        grpSSubs.Controls.Add(Label20)
        grpSSubs.Controls.Add(txtSSubDOB)
        grpSSubs.Controls.Add(cmbSSubSex)
        grpSSubs.Controls.Add(btnSSubLook)
        grpSSubs.Controls.Add(Label21)
        grpSSubs.Controls.Add(txtSSubCountry)
        grpSSubs.Controls.Add(Label22)
        grpSSubs.Controls.Add(txtSSubEmail)
        grpSSubs.Controls.Add(Label23)
        grpSSubs.Controls.Add(txtSSubWPhone)
        grpSSubs.Controls.Add(txtSSubHPhone)
        grpSSubs.Controls.Add(Label54)
        grpSSubs.Controls.Add(Label24)
        grpSSubs.Controls.Add(txtSSubZip)
        grpSSubs.Controls.Add(Label41)
        grpSSubs.Controls.Add(txtSSubState)
        grpSSubs.Controls.Add(Label42)
        grpSSubs.Controls.Add(txtSSubCity)
        grpSSubs.Controls.Add(Label43)
        grpSSubs.Controls.Add(txtSSubAdd2)
        grpSSubs.Controls.Add(Label44)
        grpSSubs.Controls.Add(txtSSubAdd1)
        grpSSubs.Controls.Add(Label45)
        grpSSubs.Controls.Add(txtSSubMName)
        grpSSubs.Controls.Add(Label47)
        grpSSubs.Controls.Add(txtSSubFName)
        grpSSubs.Controls.Add(Label78)
        grpSSubs.Controls.Add(Label48)
        grpSSubs.Controls.Add(txtSSubLName)
        grpSSubs.Controls.Add(Label49)
        grpSSubs.Controls.Add(txtSSubID)
        grpSSubs.Location = New Point(11, 202)
        grpSSubs.Margin = New Padding(5, 6, 5, 6)
        grpSSubs.Name = "grpSSubs"
        grpSSubs.Padding = New Padding(5, 6, 5, 6)
        grpSSubs.Size = New Size(935, 459)
        grpSSubs.TabIndex = 54
        grpSSubs.TabStop = False
        grpSSubs.Text = "Secondary Subscriber"
        ' 
        ' Label82
        ' 
        Label82.Location = New Point(344, 119)
        Label82.Margin = New Padding(5, 0, 5, 0)
        Label82.Name = "Label82"
        Label82.Size = New Size(115, 25)
        Label82.TabIndex = 102
        Label82.Text = "Tage (Years)"
        ' 
        ' txtSSubTage
        ' 
        txtSSubTage.AcceptsReturn = True
        txtSSubTage.Location = New Point(349, 150)
        txtSSubTage.Margin = New Padding(5, 6, 5, 6)
        txtSSubTage.MaxLength = 13
        txtSSubTage.Name = "txtSSubTage"
        txtSSubTage.ReadOnly = True
        txtSSubTage.Size = New Size(99, 31)
        txtSSubTage.TabIndex = 68
        txtSSubTage.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label52
        ' 
        Label52.ForeColor = Color.DarkBlue
        Label52.Location = New Point(175, 202)
        Label52.Margin = New Padding(5, 0, 5, 0)
        Label52.Name = "Label52"
        Label52.Size = New Size(144, 25)
        Label52.TabIndex = 100
        Label52.Text = "Fax"
        ' 
        ' Label51
        ' 
        Label51.ForeColor = Color.DarkBlue
        Label51.Location = New Point(10, 202)
        Label51.Margin = New Padding(5, 0, 5, 0)
        Label51.Name = "Label51"
        Label51.Size = New Size(140, 25)
        Label51.TabIndex = 99
        Label51.Text = "Work Phone"
        ' 
        ' Label50
        ' 
        Label50.ForeColor = Color.DarkBlue
        Label50.Location = New Point(524, 202)
        Label50.Margin = New Padding(5, 0, 5, 0)
        Label50.Name = "Label50"
        Label50.Size = New Size(191, 25)
        Label50.TabIndex = 98
        Label50.Text = "Employer"
        ' 
        ' txtSSubEmployer
        ' 
        txtSSubEmployer.AcceptsReturn = True
        txtSSubEmployer.Location = New Point(529, 233)
        txtSSubEmployer.Margin = New Padding(5, 6, 5, 6)
        txtSSubEmployer.MaxLength = 60
        txtSSubEmployer.Name = "txtSSubEmployer"
        txtSSubEmployer.Size = New Size(210, 31)
        txtSSubEmployer.TabIndex = 75
        ' 
        ' txtSSubFax
        ' 
        txtSSubFax.AcceptsReturn = True
        txtSSubFax.Location = New Point(180, 233)
        txtSSubFax.Margin = New Padding(5, 6, 5, 6)
        txtSSubFax.MaxLength = 13
        txtSSubFax.Name = "txtSSubFax"
        txtSSubFax.Size = New Size(140, 31)
        txtSSubFax.TabIndex = 73
        ' 
        ' Label46
        ' 
        Label46.ForeColor = Color.DarkBlue
        Label46.Location = New Point(751, 202)
        Label46.Margin = New Padding(5, 0, 5, 0)
        Label46.Name = "Label46"
        Label46.Size = New Size(130, 25)
        Label46.TabIndex = 95
        Label46.Text = "Cell"
        ' 
        ' Label57
        ' 
        Label57.ForeColor = Color.DarkBlue
        Label57.Location = New Point(610, 117)
        Label57.Margin = New Padding(5, 0, 5, 0)
        Label57.Name = "Label57"
        Label57.Size = New Size(114, 25)
        Label57.TabIndex = 94
        Label57.Text = "SSN"
        ' 
        ' txtSSubCell
        ' 
        txtSSubCell.AcceptsReturn = True
        txtSSubCell.Location = New Point(754, 233)
        txtSSubCell.Margin = New Padding(5, 6, 5, 6)
        txtSSubCell.MaxLength = 13
        txtSSubCell.Name = "txtSSubCell"
        txtSSubCell.Size = New Size(149, 31)
        txtSSubCell.TabIndex = 76
        ' 
        ' txtSSubSSN
        ' 
        txtSSubSSN.Location = New Point(611, 148)
        txtSSubSSN.Margin = New Padding(5, 6, 5, 6)
        txtSSubSSN.Mask = "000-00-0000"
        txtSSubSSN.Name = "txtSSubSSN"
        txtSSubSSN.Size = New Size(128, 31)
        txtSSubSSN.TabIndex = 70
        ' 
        ' Label20
        ' 
        Label20.ForeColor = Color.DarkBlue
        Label20.Location = New Point(25, 117)
        Label20.Margin = New Padding(5, 0, 5, 0)
        Label20.Name = "Label20"
        Label20.Size = New Size(125, 25)
        Label20.TabIndex = 90
        Label20.Text = "Gender"
        ' 
        ' txtSSubDOB
        ' 
        txtSSubDOB.Location = New Point(479, 148)
        txtSSubDOB.Margin = New Padding(5, 6, 5, 6)
        txtSSubDOB.Mask = "00/00/0000"
        txtSSubDOB.Name = "txtSSubDOB"
        txtSSubDOB.Size = New Size(120, 31)
        txtSSubDOB.TabIndex = 69
        txtSSubDOB.ValidatingType = GetType(Date)
        ' 
        ' cmbSSubSex
        ' 
        cmbSSubSex.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSSubSex.FormattingEnabled = True
        cmbSSubSex.Items.AddRange(New Object() {"Female", "Male", "Indetermined", "Unreported"})
        cmbSSubSex.Location = New Point(15, 148)
        cmbSSubSex.Margin = New Padding(5, 6, 5, 6)
        cmbSSubSex.Name = "cmbSSubSex"
        cmbSSubSex.Size = New Size(305, 33)
        cmbSSubSex.TabIndex = 67
        ' 
        ' btnSSubLook
        ' 
        btnSSubLook.Image = CType(resources.GetObject("btnSSubLook.Image"), Image)
        btnSSubLook.Location = New Point(185, 52)
        btnSSubLook.Margin = New Padding(5, 6, 5, 6)
        btnSSubLook.Name = "btnSSubLook"
        btnSSubLook.Size = New Size(44, 50)
        btnSSubLook.TabIndex = 63
        btnSSubLook.UseVisualStyleBackColor = True
        ' 
        ' Label21
        ' 
        Label21.ForeColor = Color.DarkBlue
        Label21.Location = New Point(726, 364)
        Label21.Margin = New Padding(5, 0, 5, 0)
        Label21.Name = "Label21"
        Label21.Size = New Size(150, 25)
        Label21.TabIndex = 86
        Label21.Text = "Country"
        ' 
        ' txtSSubCountry
        ' 
        txtSSubCountry.AcceptsReturn = True
        txtSSubCountry.Location = New Point(709, 394)
        txtSSubCountry.Margin = New Padding(5, 6, 5, 6)
        txtSSubCountry.MaxLength = 35
        txtSSubCountry.Name = "txtSSubCountry"
        txtSSubCountry.Size = New Size(190, 31)
        txtSSubCountry.TabIndex = 82
        ' 
        ' Label22
        ' 
        Label22.ForeColor = Color.DarkBlue
        Label22.Location = New Point(329, 202)
        Label22.Margin = New Padding(5, 0, 5, 0)
        Label22.Name = "Label22"
        Label22.Size = New Size(191, 25)
        Label22.TabIndex = 85
        Label22.Text = "Email Address"
        ' 
        ' txtSSubEmail
        ' 
        txtSSubEmail.AcceptsReturn = True
        txtSSubEmail.Location = New Point(334, 233)
        txtSSubEmail.Margin = New Padding(5, 6, 5, 6)
        txtSSubEmail.MaxLength = 50
        txtSSubEmail.Name = "txtSSubEmail"
        txtSSubEmail.Size = New Size(183, 31)
        txtSSubEmail.TabIndex = 74
        ' 
        ' Label23
        ' 
        Label23.ForeColor = Color.DarkBlue
        Label23.Location = New Point(474, 117)
        Label23.Margin = New Padding(5, 0, 5, 0)
        Label23.Name = "Label23"
        Label23.Size = New Size(91, 25)
        Label23.TabIndex = 84
        Label23.Text = "DOB"
        ' 
        ' txtSSubWPhone
        ' 
        txtSSubWPhone.AcceptsReturn = True
        txtSSubWPhone.Location = New Point(15, 233)
        txtSSubWPhone.Margin = New Padding(5, 6, 5, 6)
        txtSSubWPhone.MaxLength = 13
        txtSSubWPhone.Name = "txtSSubWPhone"
        txtSSubWPhone.Size = New Size(149, 31)
        txtSSubWPhone.TabIndex = 72
        ' 
        ' txtSSubHPhone
        ' 
        txtSSubHPhone.AcceptsReturn = True
        txtSSubHPhone.Location = New Point(751, 148)
        txtSSubHPhone.Margin = New Padding(5, 6, 5, 6)
        txtSSubHPhone.MaxLength = 13
        txtSSubHPhone.Name = "txtSSubHPhone"
        txtSSubHPhone.Size = New Size(150, 31)
        txtSSubHPhone.TabIndex = 71
        ' 
        ' Label54
        ' 
        Label54.ForeColor = Color.DarkBlue
        Label54.Location = New Point(746, 117)
        Label54.Margin = New Padding(5, 0, 5, 0)
        Label54.Name = "Label54"
        Label54.Size = New Size(130, 25)
        Label54.TabIndex = 84
        Label54.Text = "Home Phone"
        ' 
        ' Label24
        ' 
        Label24.ForeColor = Color.DarkBlue
        Label24.Location = New Point(545, 364)
        Label24.Margin = New Padding(5, 0, 5, 0)
        Label24.Name = "Label24"
        Label24.Size = New Size(104, 25)
        Label24.TabIndex = 83
        Label24.Text = "Zip"
        ' 
        ' txtSSubZip
        ' 
        txtSSubZip.AcceptsReturn = True
        txtSSubZip.Location = New Point(529, 394)
        txtSSubZip.Margin = New Padding(5, 6, 5, 6)
        txtSSubZip.MaxLength = 25
        txtSSubZip.Name = "txtSSubZip"
        txtSSubZip.Size = New Size(168, 31)
        txtSSubZip.TabIndex = 81
        ' 
        ' Label41
        ' 
        Label41.ForeColor = Color.DarkBlue
        Label41.Location = New Point(285, 364)
        Label41.Margin = New Padding(5, 0, 5, 0)
        Label41.Name = "Label41"
        Label41.Size = New Size(180, 25)
        Label41.TabIndex = 82
        Label41.Text = "State/Province"
        ' 
        ' txtSSubState
        ' 
        txtSSubState.AcceptsReturn = True
        txtSSubState.Location = New Point(265, 394)
        txtSSubState.Margin = New Padding(5, 6, 5, 6)
        txtSSubState.MaxLength = 35
        txtSSubState.Name = "txtSSubState"
        txtSSubState.Size = New Size(250, 31)
        txtSSubState.TabIndex = 80
        ' 
        ' Label42
        ' 
        Label42.ForeColor = Color.DarkBlue
        Label42.Location = New Point(14, 364)
        Label42.Margin = New Padding(5, 0, 5, 0)
        Label42.Name = "Label42"
        Label42.Size = New Size(121, 25)
        Label42.TabIndex = 81
        Label42.Text = "City"
        ' 
        ' txtSSubCity
        ' 
        txtSSubCity.AcceptsReturn = True
        txtSSubCity.Location = New Point(15, 394)
        txtSSubCity.Margin = New Padding(5, 6, 5, 6)
        txtSSubCity.MaxLength = 35
        txtSSubCity.Name = "txtSSubCity"
        txtSSubCity.Size = New Size(238, 31)
        txtSSubCity.TabIndex = 79
        ' 
        ' Label43
        ' 
        Label43.ForeColor = Color.DarkBlue
        Label43.Location = New Point(285, 289)
        Label43.Margin = New Padding(5, 0, 5, 0)
        Label43.Name = "Label43"
        Label43.Size = New Size(179, 25)
        Label43.TabIndex = 80
        Label43.Text = "Address Line 2"
        ' 
        ' txtSSubAdd2
        ' 
        txtSSubAdd2.AcceptsReturn = True
        txtSSubAdd2.Location = New Point(265, 319)
        txtSSubAdd2.Margin = New Padding(5, 6, 5, 6)
        txtSSubAdd2.MaxLength = 35
        txtSSubAdd2.Name = "txtSSubAdd2"
        txtSSubAdd2.Size = New Size(250, 31)
        txtSSubAdd2.TabIndex = 78
        ' 
        ' Label44
        ' 
        Label44.ForeColor = Color.DarkBlue
        Label44.Location = New Point(10, 289)
        Label44.Margin = New Padding(5, 0, 5, 0)
        Label44.Name = "Label44"
        Label44.Size = New Size(184, 25)
        Label44.TabIndex = 78
        Label44.Text = "Address Line 1"
        ' 
        ' txtSSubAdd1
        ' 
        txtSSubAdd1.AcceptsReturn = True
        txtSSubAdd1.Location = New Point(15, 319)
        txtSSubAdd1.Margin = New Padding(5, 6, 5, 6)
        txtSSubAdd1.MaxLength = 35
        txtSSubAdd1.Name = "txtSSubAdd1"
        txtSSubAdd1.Size = New Size(238, 31)
        txtSSubAdd1.TabIndex = 77
        ' 
        ' Label45
        ' 
        Label45.ForeColor = Color.DarkBlue
        Label45.Location = New Point(474, 117)
        Label45.Margin = New Padding(5, 0, 5, 0)
        Label45.Name = "Label45"
        Label45.Size = New Size(124, 25)
        Label45.TabIndex = 75
        Label45.Text = "D.O.B"
        ' 
        ' txtSSubMName
        ' 
        txtSSubMName.AcceptsReturn = True
        txtSSubMName.Location = New Point(731, 61)
        txtSSubMName.Margin = New Padding(5, 6, 5, 6)
        txtSSubMName.MaxLength = 35
        txtSSubMName.Name = "txtSSubMName"
        txtSSubMName.Size = New Size(170, 31)
        txtSSubMName.TabIndex = 66
        ' 
        ' Label47
        ' 
        Label47.ForeColor = Color.DarkBlue
        Label47.Location = New Point(509, 31)
        Label47.Margin = New Padding(5, 0, 5, 0)
        Label47.Name = "Label47"
        Label47.Size = New Size(116, 25)
        Label47.TabIndex = 69
        Label47.Text = "First Name"
        ' 
        ' txtSSubFName
        ' 
        txtSSubFName.AcceptsReturn = True
        txtSSubFName.Location = New Point(490, 61)
        txtSSubFName.Margin = New Padding(5, 6, 5, 6)
        txtSSubFName.MaxLength = 35
        txtSSubFName.Name = "txtSSubFName"
        txtSSubFName.Size = New Size(219, 31)
        txtSSubFName.TabIndex = 65
        ' 
        ' Label78
        ' 
        Label78.ForeColor = Color.DarkBlue
        Label78.Location = New Point(751, 31)
        Label78.Margin = New Padding(5, 0, 5, 0)
        Label78.Name = "Label78"
        Label78.Size = New Size(131, 25)
        Label78.TabIndex = 72
        Label78.Text = "Middle Name"
        ' 
        ' Label48
        ' 
        Label48.ForeColor = Color.DarkBlue
        Label48.Location = New Point(265, 31)
        Label48.Margin = New Padding(5, 0, 5, 0)
        Label48.Name = "Label48"
        Label48.Size = New Size(124, 25)
        Label48.TabIndex = 67
        Label48.Text = "Last Name"
        ' 
        ' txtSSubLName
        ' 
        txtSSubLName.AcceptsReturn = True
        txtSSubLName.Location = New Point(239, 61)
        txtSSubLName.Margin = New Padding(5, 6, 5, 6)
        txtSSubLName.MaxLength = 35
        txtSSubLName.Name = "txtSSubLName"
        txtSSubLName.Size = New Size(224, 31)
        txtSSubLName.TabIndex = 64
        ' 
        ' Label49
        ' 
        Label49.ForeColor = Color.Fuchsia
        Label49.Location = New Point(25, 31)
        Label49.Margin = New Padding(5, 0, 5, 0)
        Label49.Name = "Label49"
        Label49.Size = New Size(150, 25)
        Label49.TabIndex = 64
        Label49.Text = "Subscriber ID"
        ' 
        ' txtSSubID
        ' 
        txtSSubID.AcceptsReturn = True
        txtSSubID.Location = New Point(15, 61)
        txtSSubID.Margin = New Padding(5, 6, 5, 6)
        txtSSubID.MaxLength = 12
        txtSSubID.Name = "txtSSubID"
        txtSSubID.ReadOnly = True
        txtSSubID.Size = New Size(158, 31)
        txtSSubID.TabIndex = 62
        txtSSubID.TabStop = False
        txtSSubID.TextAlign = HorizontalAlignment.Center
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(dgvFiles)
        TabPage1.Controls.Add(Panel1)
        TabPage1.Location = New Point(4, 34)
        TabPage1.Margin = New Padding(4, 6, 4, 6)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(4, 6, 4, 6)
        TabPage1.Size = New Size(987, 756)
        TabPage1.TabIndex = 3
        TabPage1.Text = "Documents"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' dgvFiles
        ' 
        dgvFiles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvFiles.Columns.AddRange(New DataGridViewColumn() {ID, Title, View, Column1})
        dgvFiles.Location = New Point(9, 144)
        dgvFiles.Margin = New Padding(4, 6, 4, 6)
        dgvFiles.Name = "dgvFiles"
        dgvFiles.RowHeadersWidth = 51
        dgvFiles.RowTemplate.Height = 24
        dgvFiles.Size = New Size(966, 594)
        dgvFiles.TabIndex = 0
        ' 
        ' ID
        ' 
        ID.HeaderText = ""
        ID.MinimumWidth = 6
        ID.Name = "ID"
        ID.ReadOnly = True
        ID.Width = 125
        ' 
        ' Title
        ' 
        Title.HeaderText = "Title"
        Title.MinimumWidth = 20
        Title.Name = "Title"
        Title.Width = 125
        ' 
        ' View
        ' 
        View.HeaderText = "View"
        View.MinimumWidth = 6
        View.Name = "View"
        View.ReadOnly = True
        View.Resizable = DataGridViewTriState.False
        View.Text = "View"
        View.Width = 125
        ' 
        ' Column1
        ' 
        Column1.HeaderText = ""
        Column1.Image = My.Resources.Resources.icons8_delete_32
        Column1.MinimumWidth = 6
        Column1.Name = "Column1"
        Column1.ReadOnly = True
        Column1.Width = 125
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(Label85)
        Panel1.Controls.Add(btnBrowse)
        Panel1.Controls.Add(txtTitle)
        Panel1.Location = New Point(9, 9)
        Panel1.Margin = New Padding(4, 6, 4, 6)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(966, 127)
        Panel1.TabIndex = 0
        ' 
        ' Label85
        ' 
        Label85.AutoSize = True
        Label85.Location = New Point(216, 23)
        Label85.Margin = New Padding(4, 0, 4, 0)
        Label85.Name = "Label85"
        Label85.Size = New Size(258, 25)
        Label85.TabIndex = 3
        Label85.Text = "Write Document Title (Optional"
        ' 
        ' btnBrowse
        ' 
        btnBrowse.Image = My.Resources.Resources.icons8_open_24
        btnBrowse.ImageAlign = ContentAlignment.MiddleLeft
        btnBrowse.Location = New Point(565, 52)
        btnBrowse.Margin = New Padding(4, 6, 4, 6)
        btnBrowse.Name = "btnBrowse"
        btnBrowse.Size = New Size(135, 44)
        btnBrowse.TabIndex = 1
        btnBrowse.Text = "Browse"
        btnBrowse.UseVisualStyleBackColor = True
        ' 
        ' txtTitle
        ' 
        txtTitle.Location = New Point(220, 52)
        txtTitle.Margin = New Padding(4, 6, 4, 6)
        txtTitle.Multiline = True
        txtTitle.Name = "txtTitle"
        txtTitle.Size = New Size(294, 43)
        txtTitle.TabIndex = 0
        ' 
        ' TabPage3
        ' 
        TabPage3.Controls.Add(btnDefault)
        TabPage3.Controls.Add(btnBG)
        TabPage3.Controls.Add(btnColor)
        TabPage3.Controls.Add(cmdFont)
        TabPage3.Controls.Add(txtAlert)
        TabPage3.Controls.Add(chkAcc)
        TabPage3.Controls.Add(chkCS)
        TabPage3.Controls.Add(Label87)
        TabPage3.Location = New Point(4, 34)
        TabPage3.Margin = New Padding(4, 3, 4, 3)
        TabPage3.Name = "TabPage3"
        TabPage3.Padding = New Padding(4, 3, 4, 3)
        TabPage3.Size = New Size(987, 756)
        TabPage3.TabIndex = 5
        TabPage3.Text = "Alert Set Up"
        TabPage3.UseVisualStyleBackColor = True
        ' 
        ' btnDefault
        ' 
        btnDefault.Location = New Point(775, 11)
        btnDefault.Margin = New Padding(5, 6, 5, 6)
        btnDefault.Name = "btnDefault"
        btnDefault.Size = New Size(186, 44)
        btnDefault.TabIndex = 16
        btnDefault.Text = "Default Setting"
        btnDefault.UseVisualStyleBackColor = True
        ' 
        ' btnBG
        ' 
        btnBG.Location = New Point(521, 673)
        btnBG.Margin = New Padding(5, 6, 5, 6)
        btnBG.Name = "btnBG"
        btnBG.Size = New Size(121, 44)
        btnBG.TabIndex = 15
        btnBG.Text = "Background"
        btnBG.UseVisualStyleBackColor = True
        ' 
        ' btnColor
        ' 
        btnColor.Location = New Point(680, 673)
        btnColor.Margin = New Padding(5, 6, 5, 6)
        btnColor.Name = "btnColor"
        btnColor.Size = New Size(121, 44)
        btnColor.TabIndex = 14
        btnColor.Text = "Color"
        btnColor.UseVisualStyleBackColor = True
        ' 
        ' cmdFont
        ' 
        cmdFont.Location = New Point(839, 673)
        cmdFont.Margin = New Padding(5, 6, 5, 6)
        cmdFont.Name = "cmdFont"
        cmdFont.Size = New Size(121, 44)
        cmdFont.TabIndex = 13
        cmdFont.Text = "Font"
        cmdFont.UseVisualStyleBackColor = True
        ' 
        ' txtAlert
        ' 
        txtAlert.Location = New Point(20, 73)
        txtAlert.Margin = New Padding(5, 6, 5, 6)
        txtAlert.MaxLength = 4000
        txtAlert.Name = "txtAlert"
        txtAlert.Size = New Size(940, 588)
        txtAlert.TabIndex = 12
        txtAlert.Text = ""
        ' 
        ' chkAcc
        ' 
        chkAcc.AutoSize = True
        chkAcc.Location = New Point(275, 681)
        chkAcc.Margin = New Padding(5, 6, 5, 6)
        chkAcc.Name = "chkAcc"
        chkAcc.Size = New Size(198, 29)
        chkAcc.TabIndex = 11
        chkAcc.Text = "Appear in Accession"
        chkAcc.UseVisualStyleBackColor = True
        ' 
        ' chkCS
        ' 
        chkCS.AutoSize = True
        chkCS.Location = New Point(20, 681)
        chkCS.Margin = New Padding(5, 6, 5, 6)
        chkCS.Name = "chkCS"
        chkCS.Size = New Size(224, 29)
        chkCS.TabIndex = 10
        chkCS.Text = "Appear in Client Service"
        chkCS.UseVisualStyleBackColor = True
        ' 
        ' Label87
        ' 
        Label87.Location = New Point(15, 19)
        Label87.Margin = New Padding(5, 0, 5, 0)
        Label87.Name = "Label87"
        Label87.Size = New Size(184, 27)
        Label87.TabIndex = 9
        Label87.Text = "Alert Content"
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(endmail)
        TabPage2.Controls.Add(Label86)
        TabPage2.Location = New Point(4, 34)
        TabPage2.Margin = New Padding(4, 6, 4, 6)
        TabPage2.Name = "TabPage2"
        TabPage2.Size = New Size(987, 756)
        TabPage2.TabIndex = 4
        TabPage2.Text = "misc"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' endmail
        ' 
        endmail.Location = New Point(20, 81)
        endmail.Margin = New Padding(4, 6, 4, 6)
        endmail.Name = "endmail"
        endmail.Size = New Size(94, 36)
        endmail.TabIndex = 1
        endmail.Text = "Send"
        endmail.UseVisualStyleBackColor = True
        ' 
        ' Label86
        ' 
        Label86.AutoSize = True
        Label86.Location = New Point(16, 31)
        Label86.Margin = New Padding(4, 0, 4, 0)
        Label86.Name = "Label86"
        Label86.Size = New Size(657, 25)
        Label86.TabIndex = 0
        Label86.Text = "Click the button below to send a signup email to the patient for the Patient Portal."
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.ImageScalingSize = New Size(20, 20)
        StatusStrip1.Items.AddRange(New ToolStripItem() {lblStatus, PB})
        StatusStrip1.Location = New Point(0, 885)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Padding = New Padding(1, 0, 24, 0)
        StatusStrip1.Size = New Size(1015, 49)
        StatusStrip1.TabIndex = 4
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = False
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(120, 42)
        ' 
        ' PB
        ' 
        PB.Name = "PB"
        PB.Size = New Size(666, 41)
        ' 
        ' BW
        ' 
        BW.WorkerReportsProgress = True
        BW.WorkerSupportsCancellation = True
        ' 
        ' btnImport
        ' 
        btnImport.Enabled = False
        btnImport.Location = New Point(829, 52)
        btnImport.Margin = New Padding(5, 6, 5, 6)
        btnImport.Name = "btnImport"
        btnImport.Size = New Size(166, 52)
        btnImport.TabIndex = 5
        btnImport.Text = "Import Patients"
        btnImport.UseVisualStyleBackColor = True
        ' 
        ' OpenFileDialog1
        ' 
        OpenFileDialog1.FileName = "OpenFileDialog1"
        ' 
        ' frmPatient
        ' 
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1015, 934)
        Controls.Add(btnImport)
        Controls.Add(StatusStrip1)
        Controls.Add(TabControl1)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmPatient"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Patient Management"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        TabControl1.ResumeLayout(False)
        tpPatient.ResumeLayout(False)
        tpPatient.PerformLayout()
        tpPrimary.ResumeLayout(False)
        grpPSubs.ResumeLayout(False)
        grpPSubs.PerformLayout()
        grpPrimary.ResumeLayout(False)
        grpPrimary.PerformLayout()
        tpSecondary.ResumeLayout(False)
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        grpSSubs.ResumeLayout(False)
        grpSSubs.PerformLayout()
        TabPage1.ResumeLayout(False)
        CType(dgvFiles, ComponentModel.ISupportInitialize).EndInit()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        TabPage3.ResumeLayout(False)
        TabPage3.PerformLayout()
        TabPage2.ResumeLayout(False)
        TabPage2.PerformLayout()
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents chkNewEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tpPatient As System.Windows.Forms.TabPage
    Friend WithEvents tpPrimary As System.Windows.Forms.TabPage
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtCountry As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtZip As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtState As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtAdd2 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtAdd1 As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtMName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtFName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtLName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPatientID As System.Windows.Forms.TextBox
    Friend WithEvents btnPatLook As System.Windows.Forms.Button
    Friend WithEvents txtDOB As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtSSN As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnPIns As System.Windows.Forms.Button
    Friend WithEvents cmbPIns As System.Windows.Forms.ComboBox
    Friend WithEvents grpPrimary As System.Windows.Forms.GroupBox
    Friend WithEvents txtPFrom As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtPTo As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtPPolicy As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtPGroup As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents tpSecondary As System.Windows.Forms.TabPage
    Friend WithEvents grpPSubs As System.Windows.Forms.GroupBox
    Friend WithEvents txtPSubSSN As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtPSubDOB As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cmbPSubSex As System.Windows.Forms.ComboBox
    Friend WithEvents btnPSubLook As System.Windows.Forms.Button
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtPSubCountry As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtPSubEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtPSubHPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtPSubZip As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtPSubState As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtPSubCity As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtPSubAdd2 As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents txtPSubAdd1 As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents txtPSubMName As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents txtPSubFName As System.Windows.Forms.TextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents s As System.Windows.Forms.TextBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents txtPSubID As System.Windows.Forms.TextBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents cmbPRelation As System.Windows.Forms.ComboBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents cmbSRelation As System.Windows.Forms.ComboBox
    Friend WithEvents txtSFrom As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnSIns As System.Windows.Forms.Button
    Friend WithEvents cmbSIns As System.Windows.Forms.ComboBox
    Friend WithEvents txtSGroup As System.Windows.Forms.TextBox
    Friend WithEvents txtSTo As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtSPolicy As System.Windows.Forms.TextBox
    Friend WithEvents grpSSubs As System.Windows.Forms.GroupBox
    Friend WithEvents txtSSubSSN As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtSSubDOB As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cmbSSubSex As System.Windows.Forms.ComboBox
    Friend WithEvents btnSSubLook As System.Windows.Forms.Button
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtSSubCountry As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtSSubEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtSSubWPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtSSubZip As System.Windows.Forms.TextBox
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents txtSSubState As System.Windows.Forms.TextBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents txtSSubCity As System.Windows.Forms.TextBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents txtSSubAdd2 As System.Windows.Forms.TextBox
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents txtSSubAdd1 As System.Windows.Forms.TextBox
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents txtSSubMName As System.Windows.Forms.TextBox
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents txtSSubFName As System.Windows.Forms.TextBox
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents txtSSubLName As System.Windows.Forms.TextBox
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents txtSSubID As System.Windows.Forms.TextBox
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents txtPSubLName As System.Windows.Forms.TextBox
    Friend WithEvents txtPCopay As System.Windows.Forms.TextBox
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents txtSCopay As System.Windows.Forms.TextBox
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents txtCell As System.Windows.Forms.TextBox
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents txtHPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents txtFax As System.Windows.Forms.TextBox
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents chkAlive As System.Windows.Forms.CheckBox
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents txtDeathDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtWPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents txtEmployer As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtNote As System.Windows.Forms.TextBox
    Friend WithEvents txtPSubCell As System.Windows.Forms.TextBox
    Friend WithEvents txtPSubWPhone As System.Windows.Forms.TextBox
    Friend WithEvents txtPSubEmployer As System.Windows.Forms.TextBox
    Friend WithEvents txtPSubFax As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents txtSSubCell As System.Windows.Forms.TextBox
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents txtSSubHPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents txtSSubEmployer As System.Windows.Forms.TextBox
    Friend WithEvents txtSSubFax As System.Windows.Forms.TextBox
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents cmbEthnicity As System.Windows.Forms.ComboBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label79 As System.Windows.Forms.Label
    Friend WithEvents cmbSecretQ As System.Windows.Forms.ComboBox
    Friend WithEvents Label80 As System.Windows.Forms.Label
    Friend WithEvents txtSecretA As System.Windows.Forms.TextBox
    Friend WithEvents btnDelPrime As System.Windows.Forms.Button
    Friend WithEvents btnDelSecond As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents BW As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnImport As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Label127 As System.Windows.Forms.Label
    Friend WithEvents txtTage As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cmbSex As System.Windows.Forms.ComboBox
    Friend WithEvents txtPSubTage As System.Windows.Forms.TextBox
    Friend WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents Label82 As System.Windows.Forms.Label
    Friend WithEvents txtSSubTage As System.Windows.Forms.TextBox
    Friend WithEvents PB As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents cmbRace As System.Windows.Forms.ComboBox
    Friend WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents Label83 As System.Windows.Forms.Label
    Friend WithEvents cmbBreed As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSpecies As System.Windows.Forms.ComboBox
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Panel1 As Panel
    Friend WithEvents txtTitle As TextBox
    Friend WithEvents btnBrowse As Button
    Friend WithEvents dgvFiles As DataGridView
    Friend WithEvents Label85 As Label
    Friend WithEvents ID As DataGridViewTextBoxColumn
    Friend WithEvents Title As DataGridViewTextBoxColumn
    Friend WithEvents View As DataGridViewButtonColumn
    Friend WithEvents Column1 As DataGridViewImageColumn
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Label86 As Label
    Friend WithEvents endmail As Button
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents btnDefault As Button
    Friend WithEvents btnBG As Button
    Friend WithEvents btnColor As Button
    Friend WithEvents cmdFont As Button
    Friend WithEvents txtAlert As RichTextBox
    Friend WithEvents chkAcc As CheckBox
    Friend WithEvents chkCS As CheckBox
    Friend WithEvents Label87 As Label
End Class
