<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPayers
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPayers))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.chkEditNew = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.tcPayers = New System.Windows.Forms.TabControl()
        Me.tpInsurance = New System.Windows.Forms.TabPage()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.cmbPayerType = New System.Windows.Forms.ComboBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.btnPreAuthLook = New System.Windows.Forms.Button()
        Me.txtPreAuth = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtEligibilityCode = New System.Windows.Forms.TextBox()
        Me.btnRPTLookup = New System.Windows.Forms.Button()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtDocFile = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lstPartners = New System.Windows.Forms.CheckedListBox()
        Me.txtFax = New System.Windows.Forms.MaskedTextBox()
        Me.txtPhone = New System.Windows.Forms.MaskedTextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtNote = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtCountry = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtZip = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtState = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtAdd2 = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtAdd1 = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtWebsite = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtPartNo = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtContact = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtCommDLL = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.chkECC = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtNPI = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtFedID = New System.Windows.Forms.MaskedTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtPayerCode = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtAccount = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPayerName = New System.Windows.Forms.TextBox()
        Me.chkPar = New System.Windows.Forms.CheckBox()
        Me.btnInsLook = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPayerID = New System.Windows.Forms.TextBox()
        Me.tpContract = New System.Windows.Forms.TabPage()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.cmbPriceLevel = New System.Windows.Forms.ComboBox()
        Me.dgvContract = New System.Windows.Forms.DataGridView()
        Me.tpBillReqs = New System.Windows.Forms.TabPage()
        Me.dgvReqs = New System.Windows.Forms.DataGridView()
        Me.ReqID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BillType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ReqName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ReqValue = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.tpBillRules = New System.Windows.Forms.TabPage()
        Me.dgvBillRules = New System.Windows.Forms.DataGridView()
        Me.Errase = New System.Windows.Forms.DataGridViewImageColumn()
        Me.Element = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Action = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Origin = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Target = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RuleActive = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.DEL = New System.Windows.Forms.DataGridViewImageColumn()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Look = New System.Windows.Forms.DataGridViewImageColumn()
        Me.Component = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Logo = New System.Windows.Forms.DataGridViewImageColumn()
        Me.Price = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Modifier = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToolStrip1.SuspendLayout()
        Me.tcPayers.SuspendLayout()
        Me.tpInsurance.SuspendLayout()
        Me.tpContract.SuspendLayout()
        CType(Me.dgvContract, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpBillReqs.SuspendLayout()
        CType(Me.dgvReqs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpBillRules.SuspendLayout()
        CType(Me.dgvBillRules, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.chkEditNew, Me.ToolStripSeparator1, Me.btnSave, Me.ToolStripSeparator2, Me.btnDelete, Me.ToolStripSeparator3, Me.btnCancel, Me.ToolStripSeparator4, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(753, 25)
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
        Me.btnSave.Enabled = False
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
        Me.btnDelete.Enabled = False
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
        'tcPayers
        '
        Me.tcPayers.Controls.Add(Me.tpInsurance)
        Me.tcPayers.Controls.Add(Me.tpContract)
        Me.tcPayers.Controls.Add(Me.tpBillReqs)
        Me.tcPayers.Controls.Add(Me.tpBillRules)
        Me.tcPayers.Location = New System.Drawing.Point(12, 36)
        Me.tcPayers.Name = "tcPayers"
        Me.tcPayers.SelectedIndex = 0
        Me.tcPayers.Size = New System.Drawing.Size(730, 495)
        Me.tcPayers.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight
        Me.tcPayers.TabIndex = 2
        '
        'tpInsurance
        '
        Me.tpInsurance.Controls.Add(Me.Label33)
        Me.tpInsurance.Controls.Add(Me.cmbPayerType)
        Me.tpInsurance.Controls.Add(Me.Label32)
        Me.tpInsurance.Controls.Add(Me.btnPreAuthLook)
        Me.tpInsurance.Controls.Add(Me.txtPreAuth)
        Me.tpInsurance.Controls.Add(Me.Label29)
        Me.tpInsurance.Controls.Add(Me.txtEligibilityCode)
        Me.tpInsurance.Controls.Add(Me.btnRPTLookup)
        Me.tpInsurance.Controls.Add(Me.Label28)
        Me.tpInsurance.Controls.Add(Me.txtDocFile)
        Me.tpInsurance.Controls.Add(Me.Label27)
        Me.tpInsurance.Controls.Add(Me.chkActive)
        Me.tpInsurance.Controls.Add(Me.Label26)
        Me.tpInsurance.Controls.Add(Me.lstPartners)
        Me.tpInsurance.Controls.Add(Me.txtFax)
        Me.tpInsurance.Controls.Add(Me.txtPhone)
        Me.tpInsurance.Controls.Add(Me.Label25)
        Me.tpInsurance.Controls.Add(Me.txtNote)
        Me.tpInsurance.Controls.Add(Me.Label23)
        Me.tpInsurance.Controls.Add(Me.txtCountry)
        Me.tpInsurance.Controls.Add(Me.Label22)
        Me.tpInsurance.Controls.Add(Me.txtZip)
        Me.tpInsurance.Controls.Add(Me.Label18)
        Me.tpInsurance.Controls.Add(Me.txtState)
        Me.tpInsurance.Controls.Add(Me.Label19)
        Me.tpInsurance.Controls.Add(Me.txtCity)
        Me.tpInsurance.Controls.Add(Me.Label20)
        Me.tpInsurance.Controls.Add(Me.txtAdd2)
        Me.tpInsurance.Controls.Add(Me.Label21)
        Me.tpInsurance.Controls.Add(Me.txtAdd1)
        Me.tpInsurance.Controls.Add(Me.Label14)
        Me.tpInsurance.Controls.Add(Me.txtPassword)
        Me.tpInsurance.Controls.Add(Me.Label15)
        Me.tpInsurance.Controls.Add(Me.txtUserName)
        Me.tpInsurance.Controls.Add(Me.Label16)
        Me.tpInsurance.Controls.Add(Me.txtWebsite)
        Me.tpInsurance.Controls.Add(Me.Label17)
        Me.tpInsurance.Controls.Add(Me.txtEmail)
        Me.tpInsurance.Controls.Add(Me.Label13)
        Me.tpInsurance.Controls.Add(Me.txtPartNo)
        Me.tpInsurance.Controls.Add(Me.Label12)
        Me.tpInsurance.Controls.Add(Me.Label11)
        Me.tpInsurance.Controls.Add(Me.Label10)
        Me.tpInsurance.Controls.Add(Me.txtContact)
        Me.tpInsurance.Controls.Add(Me.Label9)
        Me.tpInsurance.Controls.Add(Me.txtCommDLL)
        Me.tpInsurance.Controls.Add(Me.Label8)
        Me.tpInsurance.Controls.Add(Me.chkECC)
        Me.tpInsurance.Controls.Add(Me.Label7)
        Me.tpInsurance.Controls.Add(Me.txtNPI)
        Me.tpInsurance.Controls.Add(Me.Label6)
        Me.tpInsurance.Controls.Add(Me.txtFedID)
        Me.tpInsurance.Controls.Add(Me.Label5)
        Me.tpInsurance.Controls.Add(Me.txtPayerCode)
        Me.tpInsurance.Controls.Add(Me.Label4)
        Me.tpInsurance.Controls.Add(Me.Label3)
        Me.tpInsurance.Controls.Add(Me.txtAccount)
        Me.tpInsurance.Controls.Add(Me.Label2)
        Me.tpInsurance.Controls.Add(Me.txtPayerName)
        Me.tpInsurance.Controls.Add(Me.chkPar)
        Me.tpInsurance.Controls.Add(Me.btnInsLook)
        Me.tpInsurance.Controls.Add(Me.Label1)
        Me.tpInsurance.Controls.Add(Me.txtPayerID)
        Me.tpInsurance.Location = New System.Drawing.Point(4, 22)
        Me.tpInsurance.Name = "tpInsurance"
        Me.tpInsurance.Padding = New System.Windows.Forms.Padding(3)
        Me.tpInsurance.Size = New System.Drawing.Size(722, 469)
        Me.tpInsurance.TabIndex = 0
        Me.tpInsurance.Text = "Insurance Information"
        Me.tpInsurance.UseVisualStyleBackColor = True
        '
        'Label33
        '
        Me.Label33.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label33.Location = New System.Drawing.Point(420, 10)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(83, 15)
        Me.Label33.TabIndex = 62
        Me.Label33.Text = "Payer Type"
        '
        'cmbPayerType
        '
        Me.cmbPayerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPayerType.FormattingEnabled = True
        Me.cmbPayerType.Location = New System.Drawing.Point(414, 30)
        Me.cmbPayerType.Name = "cmbPayerType"
        Me.cmbPayerType.Size = New System.Drawing.Size(283, 21)
        Me.cmbPayerType.TabIndex = 3
        '
        'Label32
        '
        Me.Label32.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label32.Location = New System.Drawing.Point(367, 224)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(85, 13)
        Me.Label32.TabIndex = 60
        Me.Label32.Text = "PreAuth File"
        '
        'btnPreAuthLook
        '
        Me.btnPreAuthLook.Image = CType(resources.GetObject("btnPreAuthLook.Image"), System.Drawing.Image)
        Me.btnPreAuthLook.Location = New System.Drawing.Point(674, 238)
        Me.btnPreAuthLook.Name = "btnPreAuthLook"
        Me.btnPreAuthLook.Size = New System.Drawing.Size(23, 22)
        Me.btnPreAuthLook.TabIndex = 59
        Me.btnPreAuthLook.UseVisualStyleBackColor = True
        '
        'txtPreAuth
        '
        Me.txtPreAuth.Location = New System.Drawing.Point(360, 240)
        Me.txtPreAuth.MaxLength = 100
        Me.txtPreAuth.Name = "txtPreAuth"
        Me.txtPreAuth.Size = New System.Drawing.Size(308, 20)
        Me.txtPreAuth.TabIndex = 23
        '
        'Label29
        '
        Me.Label29.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label29.Location = New System.Drawing.Point(545, 66)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(90, 13)
        Me.Label29.TabIndex = 57
        Me.Label29.Text = "Eligibility Code"
        '
        'txtEligibilityCode
        '
        Me.txtEligibilityCode.Location = New System.Drawing.Point(536, 83)
        Me.txtEligibilityCode.MaxLength = 20
        Me.txtEligibilityCode.Name = "txtEligibilityCode"
        Me.txtEligibilityCode.Size = New System.Drawing.Size(99, 20)
        Me.txtEligibilityCode.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.txtEligibilityCode, "Enter the Payer ID of the Insurance company")
        '
        'btnRPTLookup
        '
        Me.btnRPTLookup.Image = CType(resources.GetObject("btnRPTLookup.Image"), System.Drawing.Image)
        Me.btnRPTLookup.Location = New System.Drawing.Point(317, 240)
        Me.btnRPTLookup.Name = "btnRPTLookup"
        Me.btnRPTLookup.Size = New System.Drawing.Size(23, 22)
        Me.btnRPTLookup.TabIndex = 55
        Me.btnRPTLookup.UseVisualStyleBackColor = True
        '
        'Label28
        '
        Me.Label28.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label28.Location = New System.Drawing.Point(23, 224)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(85, 13)
        Me.Label28.TabIndex = 54
        Me.Label28.Text = "Document File"
        '
        'txtDocFile
        '
        Me.txtDocFile.Location = New System.Drawing.Point(20, 240)
        Me.txtDocFile.MaxLength = 100
        Me.txtDocFile.Name = "txtDocFile"
        Me.txtDocFile.Size = New System.Drawing.Size(291, 20)
        Me.txtDocFile.TabIndex = 22
        '
        'Label27
        '
        Me.Label27.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label27.Location = New System.Drawing.Point(168, 63)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(47, 15)
        Me.Label27.TabIndex = 52
        Me.Label27.Text = "Active?"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'chkActive
        '
        Me.chkActive.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkActive.Location = New System.Drawing.Point(168, 82)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(47, 22)
        Me.chkActive.TabIndex = 6
        Me.chkActive.Text = "No"
        Me.chkActive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.chkActive, "Non participating labs should leave it as 'No' and the participating labs should " & _
        "click to make it 'Yes'")
        Me.chkActive.UseVisualStyleBackColor = True
        '
        'Label26
        '
        Me.Label26.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label26.Location = New System.Drawing.Point(442, 337)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(114, 13)
        Me.Label26.TabIndex = 50
        Me.Label26.Text = "Partner Association"
        '
        'lstPartners
        '
        Me.lstPartners.FormattingEnabled = True
        Me.lstPartners.Location = New System.Drawing.Point(428, 353)
        Me.lstPartners.Name = "lstPartners"
        Me.lstPartners.Size = New System.Drawing.Size(269, 94)
        Me.lstPartners.TabIndex = 31
        '
        'txtFax
        '
        Me.txtFax.Location = New System.Drawing.Point(468, 135)
        Me.txtFax.Name = "txtFax"
        Me.txtFax.Size = New System.Drawing.Size(88, 20)
        Me.txtFax.TabIndex = 16
        '
        'txtPhone
        '
        Me.txtPhone.Location = New System.Drawing.Point(360, 135)
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New System.Drawing.Size(96, 20)
        Me.txtPhone.TabIndex = 15
        '
        'Label25
        '
        Me.Label25.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label25.Location = New System.Drawing.Point(23, 337)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(36, 13)
        Me.Label25.TabIndex = 48
        Me.Label25.Text = "Note"
        '
        'txtNote
        '
        Me.txtNote.Location = New System.Drawing.Point(20, 353)
        Me.txtNote.MaxLength = 500
        Me.txtNote.Multiline = True
        Me.txtNote.Name = "txtNote"
        Me.txtNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNote.Size = New System.Drawing.Size(385, 91)
        Me.txtNote.TabIndex = 30
        '
        'Label23
        '
        Me.Label23.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label23.Location = New System.Drawing.Point(575, 274)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(93, 13)
        Me.Label23.TabIndex = 46
        Me.Label23.Text = "Country"
        '
        'txtCountry
        '
        Me.txtCountry.Location = New System.Drawing.Point(569, 290)
        Me.txtCountry.MaxLength = 35
        Me.txtCountry.Name = "txtCountry"
        Me.txtCountry.Size = New System.Drawing.Size(128, 20)
        Me.txtCountry.TabIndex = 29
        '
        'Label22
        '
        Me.Label22.ForeColor = System.Drawing.Color.Firebrick
        Me.Label22.Location = New System.Drawing.Point(474, 274)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(66, 13)
        Me.Label22.TabIndex = 44
        Me.Label22.Text = "Zip Code"
        '
        'txtZip
        '
        Me.txtZip.Location = New System.Drawing.Point(468, 290)
        Me.txtZip.MaxLength = 25
        Me.txtZip.Name = "txtZip"
        Me.txtZip.Size = New System.Drawing.Size(88, 20)
        Me.txtZip.TabIndex = 28
        '
        'Label18
        '
        Me.Label18.ForeColor = System.Drawing.Color.Firebrick
        Me.Label18.Location = New System.Drawing.Point(367, 274)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(77, 13)
        Me.Label18.TabIndex = 42
        Me.Label18.Text = "State/Province"
        '
        'txtState
        '
        Me.txtState.Location = New System.Drawing.Point(360, 290)
        Me.txtState.MaxLength = 35
        Me.txtState.Name = "txtState"
        Me.txtState.Size = New System.Drawing.Size(96, 20)
        Me.txtState.TabIndex = 27
        '
        'Label19
        '
        Me.Label19.ForeColor = System.Drawing.Color.Firebrick
        Me.Label19.Location = New System.Drawing.Point(254, 274)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(43, 13)
        Me.Label19.TabIndex = 40
        Me.Label19.Text = "City"
        '
        'txtCity
        '
        Me.txtCity.Location = New System.Drawing.Point(244, 290)
        Me.txtCity.MaxLength = 35
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(103, 20)
        Me.txtCity.TabIndex = 26
        '
        'Label20
        '
        Me.Label20.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label20.Location = New System.Drawing.Point(165, 274)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(69, 13)
        Me.Label20.TabIndex = 38
        Me.Label20.Text = "Address 2"
        '
        'txtAdd2
        '
        Me.txtAdd2.Location = New System.Drawing.Point(146, 290)
        Me.txtAdd2.MaxLength = 35
        Me.txtAdd2.Name = "txtAdd2"
        Me.txtAdd2.Size = New System.Drawing.Size(92, 20)
        Me.txtAdd2.TabIndex = 25
        '
        'Label21
        '
        Me.Label21.ForeColor = System.Drawing.Color.Firebrick
        Me.Label21.Location = New System.Drawing.Point(23, 274)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(85, 13)
        Me.Label21.TabIndex = 36
        Me.Label21.Text = "Address 1"
        '
        'txtAdd1
        '
        Me.txtAdd1.Location = New System.Drawing.Point(20, 290)
        Me.txtAdd1.MaxLength = 35
        Me.txtAdd1.Name = "txtAdd1"
        Me.txtAdd1.Size = New System.Drawing.Size(120, 20)
        Me.txtAdd1.TabIndex = 24
        '
        'Label14
        '
        Me.Label14.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label14.Location = New System.Drawing.Point(474, 172)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(85, 13)
        Me.Label14.TabIndex = 34
        Me.Label14.Text = "Password"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(468, 188)
        Me.txtPassword.MaxLength = 12
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(124)
        Me.txtPassword.Size = New System.Drawing.Size(229, 20)
        Me.txtPassword.TabIndex = 21
        '
        'Label15
        '
        Me.Label15.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label15.Location = New System.Drawing.Point(360, 172)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(90, 13)
        Me.Label15.TabIndex = 32
        Me.Label15.Text = "User Name"
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(360, 188)
        Me.txtUserName.MaxLength = 20
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(96, 20)
        Me.txtUserName.TabIndex = 20
        '
        'Label16
        '
        Me.Label16.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label16.Location = New System.Drawing.Point(218, 172)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(93, 13)
        Me.Label16.TabIndex = 30
        Me.Label16.Text = "Website"
        '
        'txtWebsite
        '
        Me.txtWebsite.Location = New System.Drawing.Point(207, 188)
        Me.txtWebsite.MaxLength = 50
        Me.txtWebsite.Name = "txtWebsite"
        Me.txtWebsite.Size = New System.Drawing.Size(132, 20)
        Me.txtWebsite.TabIndex = 19
        '
        'Label17
        '
        Me.Label17.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label17.Location = New System.Drawing.Point(23, 172)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(106, 13)
        Me.Label17.TabIndex = 28
        Me.Label17.Text = "Email"
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(20, 188)
        Me.txtEmail.MaxLength = 50
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(167, 20)
        Me.txtEmail.TabIndex = 18
        '
        'Label13
        '
        Me.Label13.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label13.Location = New System.Drawing.Point(575, 119)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(106, 13)
        Me.Label13.TabIndex = 26
        Me.Label13.Text = "Part No"
        '
        'txtPartNo
        '
        Me.txtPartNo.Location = New System.Drawing.Point(569, 135)
        Me.txtPartNo.MaxLength = 50
        Me.txtPartNo.Name = "txtPartNo"
        Me.txtPartNo.Size = New System.Drawing.Size(128, 20)
        Me.txtPartNo.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.txtPartNo, "Contact the Prolis agent for the value of this field")
        '
        'Label12
        '
        Me.Label12.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label12.Location = New System.Drawing.Point(477, 119)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(48, 13)
        Me.Label12.TabIndex = 24
        Me.Label12.Text = "Fax"
        '
        'Label11
        '
        Me.Label11.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label11.Location = New System.Drawing.Point(367, 119)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(83, 13)
        Me.Label11.TabIndex = 22
        Me.Label11.Text = "Phone"
        '
        'Label10
        '
        Me.Label10.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label10.Location = New System.Drawing.Point(218, 119)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(65, 13)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "Contact"
        '
        'txtContact
        '
        Me.txtContact.Location = New System.Drawing.Point(207, 135)
        Me.txtContact.MaxLength = 60
        Me.txtContact.Name = "txtContact"
        Me.txtContact.Size = New System.Drawing.Size(133, 20)
        Me.txtContact.TabIndex = 14
        '
        'Label9
        '
        Me.Label9.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label9.Location = New System.Drawing.Point(29, 117)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(137, 13)
        Me.Label9.TabIndex = 18
        Me.Label9.Text = "Comm DLL"
        '
        'txtCommDLL
        '
        Me.txtCommDLL.Enabled = False
        Me.txtCommDLL.Location = New System.Drawing.Point(20, 135)
        Me.txtCommDLL.MaxLength = 50
        Me.txtCommDLL.Name = "txtCommDLL"
        Me.txtCommDLL.Size = New System.Drawing.Size(167, 20)
        Me.txtCommDLL.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.txtCommDLL, "Contact the Prolis agent for the value of this field")
        '
        'Label8
        '
        Me.Label8.ForeColor = System.Drawing.Color.Fuchsia
        Me.Label8.Location = New System.Drawing.Point(647, 67)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(50, 12)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "ECC?"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'chkECC
        '
        Me.chkECC.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkECC.Location = New System.Drawing.Point(647, 82)
        Me.chkECC.Name = "chkECC"
        Me.chkECC.Size = New System.Drawing.Size(50, 21)
        Me.chkECC.TabIndex = 12
        Me.chkECC.Text = "No"
        Me.chkECC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.chkECC, "The field should have a 'Yes' value if billing electronically with no partner rec" & _
        "ord")
        Me.chkECC.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.ForeColor = System.Drawing.Color.Fuchsia
        Me.Label7.Location = New System.Drawing.Point(333, 65)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 13)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Receiver ID"
        '
        'txtNPI
        '
        Me.txtNPI.Location = New System.Drawing.Point(329, 83)
        Me.txtNPI.MaxLength = 20
        Me.txtNPI.Name = "txtNPI"
        Me.txtNPI.Size = New System.Drawing.Size(90, 20)
        Me.txtNPI.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.txtNPI, "Enter the Receiver ID of the Insurance company")
        '
        'Label6
        '
        Me.Label6.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label6.Location = New System.Drawing.Point(232, 66)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Federal ID"
        '
        'txtFedID
        '
        Me.txtFedID.Location = New System.Drawing.Point(223, 83)
        Me.txtFedID.Mask = "00-0000000"
        Me.txtFedID.Name = "txtFedID"
        Me.txtFedID.Size = New System.Drawing.Size(88, 20)
        Me.txtFedID.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtFedID, "This field is specific to the American laboratories")
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.Color.Fuchsia
        Me.Label5.Location = New System.Drawing.Point(442, 65)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Payer Code"
        '
        'txtPayerCode
        '
        Me.txtPayerCode.Location = New System.Drawing.Point(435, 83)
        Me.txtPayerCode.MaxLength = 20
        Me.txtPayerCode.Name = "txtPayerCode"
        Me.txtPayerCode.Size = New System.Drawing.Size(90, 20)
        Me.txtPayerCode.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.txtPayerCode, "Enter the Payer ID of the Insurance company")
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.Color.Firebrick
        Me.Label4.Location = New System.Drawing.Point(73, 66)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(70, 16)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Account No"
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label3.Location = New System.Drawing.Point(23, 65)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 15)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Par?"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'txtAccount
        '
        Me.txtAccount.Location = New System.Drawing.Point(69, 83)
        Me.txtAccount.MaxLength = 20
        Me.txtAccount.Name = "txtAccount"
        Me.txtAccount.Size = New System.Drawing.Size(93, 20)
        Me.txtAccount.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtAccount, "Enter the account number with which the Payer recognizes your laboratory")
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.Color.Firebrick
        Me.Label2.Location = New System.Drawing.Point(156, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(141, 16)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Payer Name"
        '
        'txtPayerName
        '
        Me.txtPayerName.Location = New System.Drawing.Point(149, 30)
        Me.txtPayerName.MaxLength = 60
        Me.txtPayerName.Name = "txtPayerName"
        Me.txtPayerName.Size = New System.Drawing.Size(256, 20)
        Me.txtPayerName.TabIndex = 2
        '
        'chkPar
        '
        Me.chkPar.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkPar.Location = New System.Drawing.Point(20, 82)
        Me.chkPar.Name = "chkPar"
        Me.chkPar.Size = New System.Drawing.Size(43, 22)
        Me.chkPar.TabIndex = 4
        Me.chkPar.Text = "No"
        Me.chkPar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.chkPar, "Non participating labs should leave it as 'No' and the participating labs should " & _
        "click to make it 'Yes'")
        Me.chkPar.UseVisualStyleBackColor = True
        '
        'btnInsLook
        '
        Me.btnInsLook.Image = CType(resources.GetObject("btnInsLook.Image"), System.Drawing.Image)
        Me.btnInsLook.Location = New System.Drawing.Point(114, 26)
        Me.btnInsLook.Name = "btnInsLook"
        Me.btnInsLook.Size = New System.Drawing.Size(29, 27)
        Me.btnInsLook.TabIndex = 1
        Me.btnInsLook.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.Firebrick
        Me.Label1.Location = New System.Drawing.Point(23, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Payer ID"
        '
        'txtPayerID
        '
        Me.txtPayerID.Location = New System.Drawing.Point(20, 30)
        Me.txtPayerID.MaxLength = 12
        Me.txtPayerID.Name = "txtPayerID"
        Me.txtPayerID.Size = New System.Drawing.Size(88, 20)
        Me.txtPayerID.TabIndex = 0
        Me.txtPayerID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tpContract
        '
        Me.tpContract.Controls.Add(Me.Label31)
        Me.tpContract.Controls.Add(Me.Label30)
        Me.tpContract.Controls.Add(Me.dtpTo)
        Me.tpContract.Controls.Add(Me.dtpFrom)
        Me.tpContract.Controls.Add(Me.Label24)
        Me.tpContract.Controls.Add(Me.cmbPriceLevel)
        Me.tpContract.Controls.Add(Me.dgvContract)
        Me.tpContract.Location = New System.Drawing.Point(4, 22)
        Me.tpContract.Name = "tpContract"
        Me.tpContract.Padding = New System.Windows.Forms.Padding(3)
        Me.tpContract.Size = New System.Drawing.Size(722, 469)
        Me.tpContract.TabIndex = 1
        Me.tpContract.Text = "Contract Management"
        Me.tpContract.UseVisualStyleBackColor = True
        '
        'Label31
        '
        Me.Label31.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label31.Location = New System.Drawing.Point(533, 12)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(120, 15)
        Me.Label31.TabIndex = 15
        Me.Label31.Text = "Expires"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label30
        '
        Me.Label30.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label30.Location = New System.Drawing.Point(9, 12)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(120, 15)
        Me.Label30.TabIndex = 14
        Me.Label30.Text = "Effective From"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpTo
        '
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTo.Location = New System.Drawing.Point(525, 30)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(141, 20)
        Me.dtpTo.TabIndex = 13
        '
        'dtpFrom
        '
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFrom.Location = New System.Drawing.Point(12, 30)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(141, 20)
        Me.dtpFrom.TabIndex = 12
        '
        'Label24
        '
        Me.Label24.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label24.Location = New System.Drawing.Point(39, 422)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(254, 20)
        Me.Label24.TabIndex = 11
        Me.Label24.Text = "Billing Price Level for Non-Contract components"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbPriceLevel
        '
        Me.cmbPriceLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPriceLevel.FormattingEnabled = True
        Me.cmbPriceLevel.Items.AddRange(New Object() {"List Price", "Level 1", "Level 2", "Level 3", "Level 4", "Level 5", "Level 6", "Level 7", "Level 8", "Level 9"})
        Me.cmbPriceLevel.Location = New System.Drawing.Point(299, 421)
        Me.cmbPriceLevel.Name = "cmbPriceLevel"
        Me.cmbPriceLevel.Size = New System.Drawing.Size(145, 21)
        Me.cmbPriceLevel.TabIndex = 10
        '
        'dgvContract
        '
        Me.dgvContract.AllowUserToAddRows = False
        Me.dgvContract.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AntiqueWhite
        Me.dgvContract.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvContract.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvContract.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DEL, Me.ID, Me.Look, Me.Component, Me.Logo, Me.Price, Me.Modifier})
        Me.dgvContract.Location = New System.Drawing.Point(12, 56)
        Me.dgvContract.Name = "dgvContract"
        Me.dgvContract.RowHeadersVisible = False
        Me.dgvContract.Size = New System.Drawing.Size(668, 346)
        Me.dgvContract.TabIndex = 9
        '
        'tpBillReqs
        '
        Me.tpBillReqs.Controls.Add(Me.dgvReqs)
        Me.tpBillReqs.Location = New System.Drawing.Point(4, 22)
        Me.tpBillReqs.Name = "tpBillReqs"
        Me.tpBillReqs.Padding = New System.Windows.Forms.Padding(3)
        Me.tpBillReqs.Size = New System.Drawing.Size(722, 469)
        Me.tpBillReqs.TabIndex = 2
        Me.tpBillReqs.Text = "Billing Requisits"
        Me.tpBillReqs.UseVisualStyleBackColor = True
        '
        'dgvReqs
        '
        Me.dgvReqs.AllowUserToAddRows = False
        Me.dgvReqs.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.Lavender
        Me.dgvReqs.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvReqs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvReqs.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ReqID, Me.BillType, Me.Cat, Me.ReqName, Me.ReqValue})
        Me.dgvReqs.Location = New System.Drawing.Point(15, 20)
        Me.dgvReqs.Name = "dgvReqs"
        Me.dgvReqs.RowHeadersVisible = False
        Me.dgvReqs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvReqs.Size = New System.Drawing.Size(675, 429)
        Me.dgvReqs.TabIndex = 3
        '
        'ReqID
        '
        Me.ReqID.FillWeight = 90.0!
        Me.ReqID.HeaderText = "BR ID"
        Me.ReqID.Name = "ReqID"
        Me.ReqID.ReadOnly = True
        Me.ReqID.Width = 90
        '
        'BillType
        '
        Me.BillType.FillWeight = 90.0!
        Me.BillType.HeaderText = "Billing Type"
        Me.BillType.Name = "BillType"
        Me.BillType.Visible = False
        Me.BillType.Width = 90
        '
        'Cat
        '
        Me.Cat.FillWeight = 180.0!
        Me.Cat.HeaderText = "Category"
        Me.Cat.Name = "Cat"
        Me.Cat.ReadOnly = True
        Me.Cat.Width = 180
        '
        'ReqName
        '
        Me.ReqName.FillWeight = 290.0!
        Me.ReqName.HeaderText = "Name"
        Me.ReqName.Name = "ReqName"
        Me.ReqName.ReadOnly = True
        Me.ReqName.Width = 290
        '
        'ReqValue
        '
        Me.ReqValue.FillWeight = 62.0!
        Me.ReqValue.HeaderText = "Required?"
        Me.ReqValue.Name = "ReqValue"
        Me.ReqValue.Width = 62
        '
        'tpBillRules
        '
        Me.tpBillRules.Controls.Add(Me.dgvBillRules)
        Me.tpBillRules.Location = New System.Drawing.Point(4, 22)
        Me.tpBillRules.Name = "tpBillRules"
        Me.tpBillRules.Padding = New System.Windows.Forms.Padding(3)
        Me.tpBillRules.Size = New System.Drawing.Size(722, 469)
        Me.tpBillRules.TabIndex = 3
        Me.tpBillRules.Text = "Billing Rules"
        Me.tpBillRules.UseVisualStyleBackColor = True
        '
        'dgvBillRules
        '
        Me.dgvBillRules.AllowUserToAddRows = False
        Me.dgvBillRules.AllowUserToDeleteRows = False
        Me.dgvBillRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBillRules.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Errase, Me.Element, Me.Action, Me.Origin, Me.Target, Me.RuleActive})
        Me.dgvBillRules.Location = New System.Drawing.Point(18, 20)
        Me.dgvBillRules.Name = "dgvBillRules"
        Me.dgvBillRules.RowHeadersVisible = False
        Me.dgvBillRules.Size = New System.Drawing.Size(675, 424)
        Me.dgvBillRules.TabIndex = 0
        '
        'Errase
        '
        Me.Errase.FillWeight = 40.0!
        Me.Errase.HeaderText = ""
        Me.Errase.Image = CType(resources.GetObject("Errase.Image"), System.Drawing.Image)
        Me.Errase.Name = "Errase"
        Me.Errase.ReadOnly = True
        Me.Errase.Width = 40
        '
        'Element
        '
        Me.Element.HeaderText = "Element"
        Me.Element.Items.AddRange(New Object() {"TGP_ID", "CPT_Code", "POS_Code"})
        Me.Element.Name = "Element"
        Me.Element.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'Action
        '
        Me.Action.FillWeight = 90.0!
        Me.Action.HeaderText = "ACTION"
        Me.Action.Items.AddRange(New Object() {"BREAK", "REPLACE"})
        Me.Action.Name = "Action"
        Me.Action.Width = 90
        '
        'Origin
        '
        Me.Origin.FillWeight = 236.0!
        Me.Origin.HeaderText = "Original (Single or Composit)"
        Me.Origin.Name = "Origin"
        Me.Origin.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Origin.Width = 236
        '
        'Target
        '
        Me.Target.FillWeight = 120.0!
        Me.Target.HeaderText = "Replacing Element"
        Me.Target.Name = "Target"
        Me.Target.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Target.Width = 120
        '
        'RuleActive
        '
        Me.RuleActive.FillWeight = 66.0!
        Me.RuleActive.HeaderText = "ACTIVE"
        Me.RuleActive.Name = "RuleActive"
        Me.RuleActive.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.RuleActive.Width = 66
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'DEL
        '
        Me.DEL.FillWeight = 30.0!
        Me.DEL.HeaderText = ""
        Me.DEL.Image = CType(resources.GetObject("DEL.Image"), System.Drawing.Image)
        Me.DEL.Name = "DEL"
        Me.DEL.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DEL.Width = 30
        '
        'ID
        '
        Me.ID.FillWeight = 80.0!
        Me.ID.HeaderText = "ID"
        Me.ID.Name = "ID"
        Me.ID.Width = 80
        '
        'Look
        '
        Me.Look.FillWeight = 30.0!
        Me.Look.HeaderText = ""
        Me.Look.Image = CType(resources.GetObject("Look.Image"), System.Drawing.Image)
        Me.Look.Name = "Look"
        Me.Look.ReadOnly = True
        Me.Look.Width = 30
        '
        'Component
        '
        Me.Component.FillWeight = 250.0!
        Me.Component.HeaderText = "Component Name"
        Me.Component.Name = "Component"
        Me.Component.ReadOnly = True
        Me.Component.Width = 250
        '
        'Logo
        '
        Me.Logo.FillWeight = 30.0!
        Me.Logo.HeaderText = ""
        Me.Logo.Image = CType(resources.GetObject("Logo.Image"), System.Drawing.Image)
        Me.Logo.Name = "Logo"
        Me.Logo.ReadOnly = True
        Me.Logo.Width = 30
        '
        'Price
        '
        Me.Price.HeaderText = "Price"
        Me.Price.Name = "Price"
        '
        'Modifier
        '
        Me.Modifier.FillWeight = 96.0!
        Me.Modifier.HeaderText = "Modifier"
        Me.Modifier.Name = "Modifier"
        Me.Modifier.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Modifier.Width = 96
        '
        'frmPayers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(753, 543)
        Me.Controls.Add(Me.tcPayers)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPayers"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Payer Management"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.tcPayers.ResumeLayout(False)
        Me.tpInsurance.ResumeLayout(False)
        Me.tpInsurance.PerformLayout()
        Me.tpContract.ResumeLayout(False)
        CType(Me.dgvContract, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpBillReqs.ResumeLayout(False)
        CType(Me.dgvReqs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpBillRules.ResumeLayout(False)
        CType(Me.dgvBillRules, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents tcPayers As System.Windows.Forms.TabControl
    Friend WithEvents tpInsurance As System.Windows.Forms.TabPage
    Friend WithEvents tpContract As System.Windows.Forms.TabPage
    Friend WithEvents txtPayerID As System.Windows.Forms.TextBox
    Friend WithEvents chkPar As System.Windows.Forms.CheckBox
    Friend WithEvents btnInsLook As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPayerName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtAccount As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtFedID As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtPayerCode As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtNPI As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtCommDLL As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents chkECC As System.Windows.Forms.CheckBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtContact As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtPartNo As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtWebsite As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtState As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtAdd2 As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtAdd1 As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtCountry As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtZip As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents cmbPriceLevel As System.Windows.Forms.ComboBox
    Friend WithEvents dgvContract As System.Windows.Forms.DataGridView
    Friend WithEvents txtNote As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents txtFax As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtPhone As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents lstPartners As System.Windows.Forms.CheckedListBox
    Friend WithEvents tpBillReqs As System.Windows.Forms.TabPage
    Friend WithEvents dgvReqs As System.Windows.Forms.DataGridView
    Friend WithEvents tpBillRules As System.Windows.Forms.TabPage
    Friend WithEvents dgvBillRules As System.Windows.Forms.DataGridView
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents txtDocFile As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents btnRPTLookup As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtEligibilityCode As System.Windows.Forms.TextBox
    Friend WithEvents ReqID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BillType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cat As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ReqName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ReqValue As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Errase As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents Element As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents Action As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents Origin As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Target As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RuleActive As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents btnPreAuthLook As System.Windows.Forms.Button
    Friend WithEvents txtPreAuth As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents cmbPayerType As System.Windows.Forms.ComboBox
    Friend WithEvents DEL As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Look As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents Component As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Logo As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents Price As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Modifier As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
