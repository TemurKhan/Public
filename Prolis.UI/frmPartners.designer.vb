<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPartners
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPartners))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.chkEditNew = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tpPartner = New System.Windows.Forms.TabPage()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtOutgoing = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtIncoming = New System.Windows.Forms.TextBox()
        Me.txtFax = New System.Windows.Forms.MaskedTextBox()
        Me.txtPhone = New System.Windows.Forms.MaskedTextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtCommDLL = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtFileNo = New System.Windows.Forms.TextBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtReceiver = New System.Windows.Forms.TextBox()
        Me.txtSubmitter = New System.Windows.Forms.TextBox()
        Me.txtAccountNo = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtNote = New System.Windows.Forms.TextBox()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.txtWebsite = New System.Windows.Forms.TextBox()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.txtContact = New System.Windows.Forms.TextBox()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.btnPartLook = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtCountry = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtZip = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtState = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtAdd2 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtAdd1 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtFedID = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPartnerID = New System.Windows.Forms.TextBox()
        Me.tpPayers = New System.Windows.Forms.TabPage()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtPayers = New System.Windows.Forms.TextBox()
        Me.btnRemAll = New System.Windows.Forms.Button()
        Me.btnRem = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.txtPayer = New System.Windows.Forms.TextBox()
        Me.txtPayerID = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btnPayerLook = New System.Windows.Forms.Button()
        Me.dgvPayers = New System.Windows.Forms.DataGridView()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Payer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PayerID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Eligibility = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Address = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToolStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tpPartner.SuspendLayout()
        Me.tpPayers.SuspendLayout()
        CType(Me.dgvPayers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.chkEditNew, Me.ToolStripButton2, Me.btnSave, Me.ToolStripSeparator1, Me.btnDelete, Me.ToolStripSeparator2, Me.btnCancel, Me.ToolStripSeparator3, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(601, 25)
        Me.ToolStrip1.TabIndex = 3
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'chkEditNew
        '
        Me.chkEditNew.AutoSize = False
        Me.chkEditNew.CheckOnClick = True
        Me.chkEditNew.Image = CType(resources.GetObject("chkEditNew.Image"), System.Drawing.Image)
        Me.chkEditNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.chkEditNew.Name = "chkEditNew"
        Me.chkEditNew.Size = New System.Drawing.Size(60, 22)
        Me.chkEditNew.Text = "Edit"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(6, 25)
        '
        'btnSave
        '
        Me.btnSave.AutoSize = False
        Me.btnSave.Enabled = False
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(60, 22)
        Me.btnSave.Text = "Save"
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
        Me.btnDelete.Size = New System.Drawing.Size(60, 22)
        Me.btnDelete.Text = "Delete"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnCancel
        '
        Me.btnCancel.AutoSize = False
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(60, 22)
        Me.btnCancel.Text = "Cancel"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'btnHelp
        '
        Me.btnHelp.AutoSize = False
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(60, 22)
        Me.btnHelp.Text = "Help"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tpPartner)
        Me.TabControl1.Controls.Add(Me.tpPayers)
        Me.TabControl1.Location = New System.Drawing.Point(12, 37)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(577, 437)
        Me.TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.TabControl1.TabIndex = 4
        '
        'tpPartner
        '
        Me.tpPartner.Controls.Add(Me.Label17)
        Me.tpPartner.Controls.Add(Me.txtOutgoing)
        Me.tpPartner.Controls.Add(Me.Label16)
        Me.tpPartner.Controls.Add(Me.txtIncoming)
        Me.tpPartner.Controls.Add(Me.txtFax)
        Me.tpPartner.Controls.Add(Me.txtPhone)
        Me.tpPartner.Controls.Add(Me.Label22)
        Me.tpPartner.Controls.Add(Me.txtCommDLL)
        Me.tpPartner.Controls.Add(Me.Label21)
        Me.tpPartner.Controls.Add(Me.Label20)
        Me.tpPartner.Controls.Add(Me.txtFileNo)
        Me.tpPartner.Controls.Add(Me.txtPassword)
        Me.tpPartner.Controls.Add(Me.Label5)
        Me.tpPartner.Controls.Add(Me.Label3)
        Me.tpPartner.Controls.Add(Me.txtReceiver)
        Me.tpPartner.Controls.Add(Me.txtSubmitter)
        Me.tpPartner.Controls.Add(Me.txtAccountNo)
        Me.tpPartner.Controls.Add(Me.Label11)
        Me.tpPartner.Controls.Add(Me.txtNote)
        Me.tpPartner.Controls.Add(Me.Label67)
        Me.tpPartner.Controls.Add(Me.txtWebsite)
        Me.tpPartner.Controls.Add(Me.Label65)
        Me.tpPartner.Controls.Add(Me.txtContact)
        Me.tpPartner.Controls.Add(Me.Label63)
        Me.tpPartner.Controls.Add(Me.chkActive)
        Me.tpPartner.Controls.Add(Me.Label62)
        Me.tpPartner.Controls.Add(Me.txtUserName)
        Me.tpPartner.Controls.Add(Me.Label61)
        Me.tpPartner.Controls.Add(Me.Label60)
        Me.tpPartner.Controls.Add(Me.Label14)
        Me.tpPartner.Controls.Add(Me.btnPartLook)
        Me.tpPartner.Controls.Add(Me.Label13)
        Me.tpPartner.Controls.Add(Me.txtCountry)
        Me.tpPartner.Controls.Add(Me.Label12)
        Me.tpPartner.Controls.Add(Me.txtEmail)
        Me.tpPartner.Controls.Add(Me.Label10)
        Me.tpPartner.Controls.Add(Me.txtZip)
        Me.tpPartner.Controls.Add(Me.Label9)
        Me.tpPartner.Controls.Add(Me.txtState)
        Me.tpPartner.Controls.Add(Me.Label8)
        Me.tpPartner.Controls.Add(Me.txtCity)
        Me.tpPartner.Controls.Add(Me.Label7)
        Me.tpPartner.Controls.Add(Me.txtAdd2)
        Me.tpPartner.Controls.Add(Me.Label6)
        Me.tpPartner.Controls.Add(Me.txtAdd1)
        Me.tpPartner.Controls.Add(Me.Label4)
        Me.tpPartner.Controls.Add(Me.txtFedID)
        Me.tpPartner.Controls.Add(Me.Label2)
        Me.tpPartner.Controls.Add(Me.txtName)
        Me.tpPartner.Controls.Add(Me.Label1)
        Me.tpPartner.Controls.Add(Me.txtPartnerID)
        Me.tpPartner.Location = New System.Drawing.Point(4, 22)
        Me.tpPartner.Name = "tpPartner"
        Me.tpPartner.Padding = New System.Windows.Forms.Padding(3)
        Me.tpPartner.Size = New System.Drawing.Size(569, 411)
        Me.tpPartner.TabIndex = 0
        Me.tpPartner.Text = "Partner"
        Me.tpPartner.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label17.Location = New System.Drawing.Point(303, 195)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(119, 13)
        Me.Label17.TabIndex = 98
        Me.Label17.Text = "Outgoing Folder"
        '
        'txtOutgoing
        '
        Me.txtOutgoing.AcceptsReturn = True
        Me.txtOutgoing.Location = New System.Drawing.Point(296, 211)
        Me.txtOutgoing.MaxLength = 35
        Me.txtOutgoing.Name = "txtOutgoing"
        Me.txtOutgoing.Size = New System.Drawing.Size(257, 20)
        Me.txtOutgoing.TabIndex = 19
        '
        'Label16
        '
        Me.Label16.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label16.Location = New System.Drawing.Point(24, 195)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(119, 13)
        Me.Label16.TabIndex = 96
        Me.Label16.Text = "Incoming Folder"
        '
        'txtIncoming
        '
        Me.txtIncoming.AcceptsReturn = True
        Me.txtIncoming.Location = New System.Drawing.Point(17, 211)
        Me.txtIncoming.MaxLength = 35
        Me.txtIncoming.Name = "txtIncoming"
        Me.txtIncoming.Size = New System.Drawing.Size(257, 20)
        Me.txtIncoming.TabIndex = 18
        '
        'txtFax
        '
        Me.txtFax.Location = New System.Drawing.Point(228, 117)
        Me.txtFax.Name = "txtFax"
        Me.txtFax.Size = New System.Drawing.Size(90, 20)
        Me.txtFax.TabIndex = 12
        '
        'txtPhone
        '
        Me.txtPhone.Location = New System.Drawing.Point(132, 117)
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New System.Drawing.Size(90, 20)
        Me.txtPhone.TabIndex = 11
        '
        'Label22
        '
        Me.Label22.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label22.Location = New System.Drawing.Point(386, 147)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(91, 13)
        Me.Label22.TabIndex = 94
        Me.Label22.Text = "Comm DLL"
        '
        'txtCommDLL
        '
        Me.txtCommDLL.AcceptsReturn = True
        Me.txtCommDLL.Location = New System.Drawing.Point(377, 163)
        Me.txtCommDLL.MaxLength = 100
        Me.txtCommDLL.Name = "txtCommDLL"
        Me.txtCommDLL.Size = New System.Drawing.Size(176, 20)
        Me.txtCommDLL.TabIndex = 17
        '
        'Label21
        '
        Me.Label21.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label21.Location = New System.Drawing.Point(462, 101)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(73, 13)
        Me.Label21.TabIndex = 92
        Me.Label21.Text = "Password"
        '
        'Label20
        '
        Me.Label20.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label20.Location = New System.Drawing.Point(491, 55)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(57, 13)
        Me.Label20.TabIndex = 91
        Me.Label20.Text = "File No"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtFileNo
        '
        Me.txtFileNo.AcceptsReturn = True
        Me.txtFileNo.Location = New System.Drawing.Point(484, 71)
        Me.txtFileNo.MaxLength = 13
        Me.txtFileNo.Name = "txtFileNo"
        Me.txtFileNo.ReadOnly = True
        Me.txtFileNo.Size = New System.Drawing.Size(69, 20)
        Me.txtFileNo.TabIndex = 9
        Me.txtFileNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtPassword
        '
        Me.txtPassword.AcceptsReturn = True
        Me.txtPassword.Location = New System.Drawing.Point(452, 117)
        Me.txtPassword.MaxLength = 20
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(88)
        Me.txtPassword.Size = New System.Drawing.Size(101, 20)
        Me.txtPassword.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(366, 54)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(98, 13)
        Me.Label5.TabIndex = 88
        Me.Label5.Text = "Receiver ID"
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(260, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 13)
        Me.Label3.TabIndex = 87
        Me.Label3.Text = "Submitter ID"
        '
        'txtReceiver
        '
        Me.txtReceiver.AcceptsReturn = True
        Me.txtReceiver.Location = New System.Drawing.Point(354, 71)
        Me.txtReceiver.MaxLength = 35
        Me.txtReceiver.Name = "txtReceiver"
        Me.txtReceiver.Size = New System.Drawing.Size(117, 20)
        Me.txtReceiver.TabIndex = 8
        '
        'txtSubmitter
        '
        Me.txtSubmitter.AcceptsReturn = True
        Me.txtSubmitter.Location = New System.Drawing.Point(228, 71)
        Me.txtSubmitter.MaxLength = 35
        Me.txtSubmitter.Name = "txtSubmitter"
        Me.txtSubmitter.Size = New System.Drawing.Size(117, 20)
        Me.txtSubmitter.TabIndex = 7
        '
        'txtAccountNo
        '
        Me.txtAccountNo.AcceptsReturn = True
        Me.txtAccountNo.Location = New System.Drawing.Point(113, 71)
        Me.txtAccountNo.MaxLength = 35
        Me.txtAccountNo.Name = "txtAccountNo"
        Me.txtAccountNo.Size = New System.Drawing.Size(109, 20)
        Me.txtAccountNo.TabIndex = 6
        '
        'Label11
        '
        Me.Label11.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label11.Location = New System.Drawing.Point(26, 328)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(64, 13)
        Me.Label11.TabIndex = 80
        Me.Label11.Text = "Note"
        '
        'txtNote
        '
        Me.txtNote.AcceptsReturn = True
        Me.txtNote.Location = New System.Drawing.Point(20, 344)
        Me.txtNote.MaxLength = 250
        Me.txtNote.Multiline = True
        Me.txtNote.Name = "txtNote"
        Me.txtNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNote.Size = New System.Drawing.Size(533, 50)
        Me.txtNote.TabIndex = 26
        '
        'Label67
        '
        Me.Label67.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label67.Location = New System.Drawing.Point(156, 147)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(91, 13)
        Me.Label67.TabIndex = 78
        Me.Label67.Text = "Website"
        '
        'txtWebsite
        '
        Me.txtWebsite.AcceptsReturn = True
        Me.txtWebsite.Location = New System.Drawing.Point(149, 163)
        Me.txtWebsite.MaxLength = 100
        Me.txtWebsite.Name = "txtWebsite"
        Me.txtWebsite.Size = New System.Drawing.Size(213, 20)
        Me.txtWebsite.TabIndex = 16
        '
        'Label65
        '
        Me.Label65.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label65.Location = New System.Drawing.Point(334, 101)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(64, 13)
        Me.Label65.TabIndex = 74
        Me.Label65.Text = "User Name"
        '
        'txtContact
        '
        Me.txtContact.AcceptsReturn = True
        Me.txtContact.Location = New System.Drawing.Point(17, 117)
        Me.txtContact.MaxLength = 60
        Me.txtContact.Name = "txtContact"
        Me.txtContact.Size = New System.Drawing.Size(109, 20)
        Me.txtContact.TabIndex = 10
        '
        'Label63
        '
        Me.Label63.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label63.Location = New System.Drawing.Point(491, 8)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(57, 13)
        Me.Label63.TabIndex = 70
        Me.Label63.Text = "Active?"
        Me.Label63.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkActive
        '
        Me.chkActive.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkActive.Checked = True
        Me.chkActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkActive.Location = New System.Drawing.Point(484, 22)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(69, 24)
        Me.chkActive.TabIndex = 4
        Me.chkActive.Text = "Yes"
        Me.chkActive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkActive.UseVisualStyleBackColor = True
        '
        'Label62
        '
        Me.Label62.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label62.Location = New System.Drawing.Point(20, 101)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(87, 13)
        Me.Label62.TabIndex = 68
        Me.Label62.Text = "Contact"
        '
        'txtUserName
        '
        Me.txtUserName.AcceptsReturn = True
        Me.txtUserName.Location = New System.Drawing.Point(328, 117)
        Me.txtUserName.MaxLength = 20
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(108, 20)
        Me.txtUserName.TabIndex = 13
        '
        'Label61
        '
        Me.Label61.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label61.Location = New System.Drawing.Point(134, 101)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(73, 13)
        Me.Label61.TabIndex = 66
        Me.Label61.Text = "Phone"
        '
        'Label60
        '
        Me.Label60.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label60.Location = New System.Drawing.Point(238, 101)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(61, 13)
        Me.Label60.TabIndex = 64
        Me.Label60.Text = "Fax"
        '
        'Label14
        '
        Me.Label14.ForeColor = System.Drawing.Color.Red
        Me.Label14.Location = New System.Drawing.Point(126, 54)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(81, 13)
        Me.Label14.TabIndex = 60
        Me.Label14.Text = "Account No"
        '
        'btnPartLook
        '
        Me.btnPartLook.Image = CType(resources.GetObject("btnPartLook.Image"), System.Drawing.Image)
        Me.btnPartLook.Location = New System.Drawing.Point(100, 21)
        Me.btnPartLook.Name = "btnPartLook"
        Me.btnPartLook.Size = New System.Drawing.Size(26, 26)
        Me.btnPartLook.TabIndex = 2
        Me.btnPartLook.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label13.Location = New System.Drawing.Point(374, 282)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(93, 13)
        Me.Label13.TabIndex = 55
        Me.Label13.Text = "Country"
        '
        'txtCountry
        '
        Me.txtCountry.AcceptsReturn = True
        Me.txtCountry.Location = New System.Drawing.Point(369, 298)
        Me.txtCountry.MaxLength = 35
        Me.txtCountry.Name = "txtCountry"
        Me.txtCountry.Size = New System.Drawing.Size(184, 20)
        Me.txtCountry.TabIndex = 25
        '
        'Label12
        '
        Me.Label12.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label12.Location = New System.Drawing.Point(23, 147)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(103, 13)
        Me.Label12.TabIndex = 54
        Me.Label12.Text = "Email Address"
        '
        'txtEmail
        '
        Me.txtEmail.AcceptsReturn = True
        Me.txtEmail.Location = New System.Drawing.Point(17, 163)
        Me.txtEmail.MaxLength = 50
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(122, 20)
        Me.txtEmail.TabIndex = 15
        '
        'Label10
        '
        Me.Label10.ForeColor = System.Drawing.Color.Fuchsia
        Me.Label10.Location = New System.Drawing.Point(199, 282)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(57, 13)
        Me.Label10.TabIndex = 52
        Me.Label10.Text = "Zip"
        '
        'txtZip
        '
        Me.txtZip.AcceptsReturn = True
        Me.txtZip.Location = New System.Drawing.Point(192, 298)
        Me.txtZip.MaxLength = 25
        Me.txtZip.Name = "txtZip"
        Me.txtZip.Size = New System.Drawing.Size(153, 20)
        Me.txtZip.TabIndex = 24
        '
        'Label9
        '
        Me.Label9.ForeColor = System.Drawing.Color.Fuchsia
        Me.Label9.Location = New System.Drawing.Point(23, 282)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(109, 13)
        Me.Label9.TabIndex = 51
        Me.Label9.Text = "State/Province"
        '
        'txtState
        '
        Me.txtState.AcceptsReturn = True
        Me.txtState.Location = New System.Drawing.Point(17, 298)
        Me.txtState.MaxLength = 35
        Me.txtState.Name = "txtState"
        Me.txtState.Size = New System.Drawing.Size(157, 20)
        Me.txtState.TabIndex = 23
        '
        'Label8
        '
        Me.Label8.ForeColor = System.Drawing.Color.Fuchsia
        Me.Label8.Location = New System.Drawing.Point(374, 238)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(66, 13)
        Me.Label8.TabIndex = 50
        Me.Label8.Text = "City"
        '
        'txtCity
        '
        Me.txtCity.AcceptsReturn = True
        Me.txtCity.Location = New System.Drawing.Point(368, 254)
        Me.txtCity.MaxLength = 35
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(185, 20)
        Me.txtCity.TabIndex = 22
        '
        'Label7
        '
        Me.Label7.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label7.Location = New System.Drawing.Point(199, 238)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(119, 13)
        Me.Label7.TabIndex = 49
        Me.Label7.Text = "Address Line 2"
        '
        'txtAdd2
        '
        Me.txtAdd2.AcceptsReturn = True
        Me.txtAdd2.Location = New System.Drawing.Point(192, 254)
        Me.txtAdd2.MaxLength = 35
        Me.txtAdd2.Name = "txtAdd2"
        Me.txtAdd2.Size = New System.Drawing.Size(153, 20)
        Me.txtAdd2.TabIndex = 21
        '
        'Label6
        '
        Me.Label6.ForeColor = System.Drawing.Color.Fuchsia
        Me.Label6.Location = New System.Drawing.Point(26, 238)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(125, 13)
        Me.Label6.TabIndex = 47
        Me.Label6.Text = "Address Line 1"
        '
        'txtAdd1
        '
        Me.txtAdd1.AcceptsReturn = True
        Me.txtAdd1.Location = New System.Drawing.Point(17, 254)
        Me.txtAdd1.MaxLength = 35
        Me.txtAdd1.Name = "txtAdd1"
        Me.txtAdd1.Size = New System.Drawing.Size(157, 20)
        Me.txtAdd1.TabIndex = 20
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label4.Location = New System.Drawing.Point(26, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 13)
        Me.Label4.TabIndex = 41
        Me.Label4.Text = "Federal ID"
        '
        'txtFedID
        '
        Me.txtFedID.AcceptsReturn = True
        Me.txtFedID.Location = New System.Drawing.Point(17, 71)
        Me.txtFedID.MaxLength = 35
        Me.txtFedID.Name = "txtFedID"
        Me.txtFedID.Size = New System.Drawing.Size(90, 20)
        Me.txtFedID.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(135, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(150, 13)
        Me.Label2.TabIndex = 35
        Me.Label2.Text = "Trading Partner Name"
        '
        'txtName
        '
        Me.txtName.AcceptsReturn = True
        Me.txtName.Location = New System.Drawing.Point(133, 25)
        Me.txtName.MaxLength = 60
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(338, 20)
        Me.txtName.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(17, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "ID"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtPartnerID
        '
        Me.txtPartnerID.AcceptsReturn = True
        Me.txtPartnerID.Location = New System.Drawing.Point(17, 25)
        Me.txtPartnerID.MaxLength = 12
        Me.txtPartnerID.Name = "txtPartnerID"
        Me.txtPartnerID.Size = New System.Drawing.Size(77, 20)
        Me.txtPartnerID.TabIndex = 1
        Me.txtPartnerID.TabStop = False
        Me.txtPartnerID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tpPayers
        '
        Me.tpPayers.AutoScroll = True
        Me.tpPayers.Controls.Add(Me.Label18)
        Me.tpPayers.Controls.Add(Me.txtPayers)
        Me.tpPayers.Controls.Add(Me.btnRemAll)
        Me.tpPayers.Controls.Add(Me.btnRem)
        Me.tpPayers.Controls.Add(Me.btnAdd)
        Me.tpPayers.Controls.Add(Me.txtPayer)
        Me.tpPayers.Controls.Add(Me.txtPayerID)
        Me.tpPayers.Controls.Add(Me.Label15)
        Me.tpPayers.Controls.Add(Me.btnPayerLook)
        Me.tpPayers.Controls.Add(Me.dgvPayers)
        Me.tpPayers.Location = New System.Drawing.Point(4, 22)
        Me.tpPayers.Name = "tpPayers"
        Me.tpPayers.Padding = New System.Windows.Forms.Padding(3)
        Me.tpPayers.Size = New System.Drawing.Size(569, 411)
        Me.tpPayers.TabIndex = 1
        Me.tpPayers.Text = "Payers"
        Me.tpPayers.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label18.Location = New System.Drawing.Point(437, 346)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(44, 13)
        Me.Label18.TabIndex = 9
        Me.Label18.Text = "Count:"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPayers
        '
        Me.txtPayers.Location = New System.Drawing.Point(487, 343)
        Me.txtPayers.MaxLength = 9
        Me.txtPayers.Name = "txtPayers"
        Me.txtPayers.ReadOnly = True
        Me.txtPayers.Size = New System.Drawing.Size(66, 20)
        Me.txtPayers.TabIndex = 8
        Me.txtPayers.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnRemAll
        '
        Me.btnRemAll.Enabled = False
        Me.btnRemAll.Image = CType(resources.GetObject("btnRemAll.Image"), System.Drawing.Image)
        Me.btnRemAll.Location = New System.Drawing.Point(521, 369)
        Me.btnRemAll.Name = "btnRemAll"
        Me.btnRemAll.Size = New System.Drawing.Size(28, 27)
        Me.btnRemAll.TabIndex = 7
        Me.btnRemAll.UseVisualStyleBackColor = True
        '
        'btnRem
        '
        Me.btnRem.Enabled = False
        Me.btnRem.Image = CType(resources.GetObject("btnRem.Image"), System.Drawing.Image)
        Me.btnRem.Location = New System.Drawing.Point(487, 369)
        Me.btnRem.Name = "btnRem"
        Me.btnRem.Size = New System.Drawing.Size(28, 27)
        Me.btnRem.TabIndex = 6
        Me.btnRem.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Enabled = False
        Me.btnAdd.Image = CType(resources.GetObject("btnAdd.Image"), System.Drawing.Image)
        Me.btnAdd.Location = New System.Drawing.Point(453, 369)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(28, 27)
        Me.btnAdd.TabIndex = 5
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'txtPayer
        '
        Me.txtPayer.Location = New System.Drawing.Point(127, 373)
        Me.txtPayer.Name = "txtPayer"
        Me.txtPayer.ReadOnly = True
        Me.txtPayer.Size = New System.Drawing.Size(308, 20)
        Me.txtPayer.TabIndex = 4
        '
        'txtPayerID
        '
        Me.txtPayerID.Location = New System.Drawing.Point(15, 373)
        Me.txtPayerID.MaxLength = 9
        Me.txtPayerID.Name = "txtPayerID"
        Me.txtPayerID.Size = New System.Drawing.Size(72, 20)
        Me.txtPayerID.TabIndex = 3
        Me.txtPayerID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label15
        '
        Me.Label15.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label15.Location = New System.Drawing.Point(15, 357)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(72, 13)
        Me.Label15.TabIndex = 2
        Me.Label15.Text = "Payer ID"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnPayerLook
        '
        Me.btnPayerLook.Image = CType(resources.GetObject("btnPayerLook.Image"), System.Drawing.Image)
        Me.btnPayerLook.Location = New System.Drawing.Point(93, 369)
        Me.btnPayerLook.Name = "btnPayerLook"
        Me.btnPayerLook.Size = New System.Drawing.Size(28, 27)
        Me.btnPayerLook.TabIndex = 1
        Me.btnPayerLook.UseVisualStyleBackColor = True
        '
        'dgvPayers
        '
        Me.dgvPayers.AllowUserToAddRows = False
        Me.dgvPayers.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Honeydew
        Me.dgvPayers.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvPayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPayers.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID, Me.Payer, Me.PayerID, Me.Eligibility, Me.Address})
        Me.dgvPayers.Location = New System.Drawing.Point(14, 15)
        Me.dgvPayers.MultiSelect = False
        Me.dgvPayers.Name = "dgvPayers"
        Me.dgvPayers.ReadOnly = True
        Me.dgvPayers.RowHeadersVisible = False
        Me.dgvPayers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPayers.Size = New System.Drawing.Size(539, 319)
        Me.dgvPayers.TabIndex = 0
        '
        'ID
        '
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.Honeydew
        Me.ID.DefaultCellStyle = DataGridViewCellStyle2
        Me.ID.FillWeight = 60.0!
        Me.ID.HeaderText = "ID"
        Me.ID.Name = "ID"
        Me.ID.ReadOnly = True
        Me.ID.Width = 60
        '
        'Payer
        '
        Me.Payer.FillWeight = 120.0!
        Me.Payer.HeaderText = "Payer"
        Me.Payer.Name = "Payer"
        Me.Payer.ReadOnly = True
        Me.Payer.Width = 120
        '
        'PayerID
        '
        Me.PayerID.FillWeight = 60.0!
        Me.PayerID.HeaderText = "Code"
        Me.PayerID.Name = "PayerID"
        Me.PayerID.ReadOnly = True
        Me.PayerID.Width = 60
        '
        'Eligibility
        '
        Me.Eligibility.FillWeight = 60.0!
        Me.Eligibility.HeaderText = "Eligibility"
        Me.Eligibility.Name = "Eligibility"
        Me.Eligibility.ReadOnly = True
        Me.Eligibility.Width = 60
        '
        'Address
        '
        Me.Address.FillWeight = 220.0!
        Me.Address.HeaderText = "Address"
        Me.Address.Name = "Address"
        Me.Address.ReadOnly = True
        Me.Address.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Address.Width = 220
        '
        'frmPartners
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(601, 486)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPartners"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Partner Management"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.tpPartner.ResumeLayout(False)
        Me.tpPartner.PerformLayout()
        Me.tpPayers.ResumeLayout(False)
        Me.tpPayers.PerformLayout()
        CType(Me.dgvPayers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tpPartner As System.Windows.Forms.TabPage
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtNote As System.Windows.Forms.TextBox
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents txtWebsite As System.Windows.Forms.TextBox
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents txtContact As System.Windows.Forms.TextBox
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnPartLook As System.Windows.Forms.Button
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
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtFedID As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPartnerID As System.Windows.Forms.TextBox
    Friend WithEvents tpPayers As System.Windows.Forms.TabPage
    Friend WithEvents txtAccountNo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtReceiver As System.Windows.Forms.TextBox
    Friend WithEvents txtSubmitter As System.Windows.Forms.TextBox
    Friend WithEvents txtFileNo As System.Windows.Forms.TextBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtCommDLL As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents dgvPayers As System.Windows.Forms.DataGridView
    Friend WithEvents btnRemAll As System.Windows.Forms.Button
    Friend WithEvents btnRem As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents txtPayer As System.Windows.Forms.TextBox
    Friend WithEvents txtPayerID As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents btnPayerLook As System.Windows.Forms.Button
    Friend WithEvents txtFax As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtPhone As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtIncoming As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtOutgoing As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtPayers As System.Windows.Forms.TextBox
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Payer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PayerID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Eligibility As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Address As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
