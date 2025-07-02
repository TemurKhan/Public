<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGroups
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGroups))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        chkEditNew = New ToolStripButton()
        ToolStripButton2 = New ToolStripSeparator()
        btnSave = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnDelete = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnReplicate = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        btnGroupLook = New Button()
        Label11 = New Label()
        ChkMarkable = New CheckBox()
        Label8 = New Label()
        ChkActive = New CheckBox()
        txtDescription = New TextBox()
        Label6 = New Label()
        txtAbbr = New TextBox()
        Label3 = New Label()
        txtName = New TextBox()
        Label2 = New Label()
        txtGroupID = New TextBox()
        Label1 = New Label()
        TabControl1 = New TabControl()
        tpContents = New TabPage()
        chkBreakOROrder = New CheckBox()
        Label9 = New Label()
        txtTestName = New TextBox()
        btnTestLook = New Button()
        Label7 = New Label()
        txtTestID = New TextBox()
        dgvTests = New DataGridView()
        NR_ID = New DataGridViewTextBoxColumn()
        Logo = New DataGridViewImageColumn()
        NRTo = New DataGridViewTextBoxColumn()
        btnRemTst = New Button()
        btnRemTstAll = New Button()
        btnAddTest = New Button()
        tpMarking = New TabPage()
        grpMarking = New GroupBox()
        btnRemoveMod = New Button()
        Label16 = New Label()
        Label17 = New Label()
        cmbSex = New ComboBox()
        Label18 = New Label()
        btnRemoveAllMod = New Button()
        txtAgeFrom = New TextBox()
        btnAddModifier = New Button()
        Label15 = New Label()
        txtAgeTo = New TextBox()
        dgvModifiers = New DataGridView()
        Test_ID = New DataGridViewTextBoxColumn()
        Modifier = New DataGridViewTextBoxColumn()
        Gender = New DataGridViewTextBoxColumn()
        AgeFrom = New DataGridViewTextBoxColumn()
        AgeTo = New DataGridViewTextBoxColumn()
        tpBilling = New TabPage()
        TabControl3 = New TabControl()
        tpPricing = New TabPage()
        txtPOS = New TextBox()
        Label72 = New Label()
        txtMod1 = New TextBox()
        Label75 = New Label()
        txtMod2 = New TextBox()
        Label76 = New Label()
        Label77 = New Label()
        txtMod4 = New TextBox()
        txtMod3 = New TextBox()
        Label78 = New Label()
        txtCPTSPC = New TextBox()
        Label68 = New Label()
        txtCPTMCD = New TextBox()
        Label67 = New Label()
        txtCPTMCR = New TextBox()
        Label66 = New Label()
        Label65 = New Label()
        txtBillUnit = New TextBox()
        txtCPTCode = New TextBox()
        txtPrice9 = New TextBox()
        Label29 = New Label()
        btnCPTLook = New Button()
        txtPrice8 = New TextBox()
        Label30 = New Label()
        txtPrice7 = New TextBox()
        Label5 = New Label()
        Label31 = New Label()
        txtPrice6 = New TextBox()
        Label32 = New Label()
        txtPrice4 = New TextBox()
        txtPrice5 = New TextBox()
        Label24 = New Label()
        Label33 = New Label()
        txtListPrice = New TextBox()
        Label25 = New Label()
        txtPrice1 = New TextBox()
        Label28 = New Label()
        Label26 = New Label()
        txtPrice3 = New TextBox()
        txtPrice2 = New TextBox()
        Label27 = New Label()
        tpNecessity = New TabPage()
        btnNecCopy = New Button()
        btnICD9Lookup = New Button()
        btnAddNec = New Button()
        btnRemAllNec = New Button()
        txtICD9Description = New TextBox()
        Label53 = New Label()
        txtICD9Code = New TextBox()
        Label52 = New Label()
        btnRemNec = New Button()
        dgvNecessity = New DataGridView()
        NecTest_ID = New DataGridViewTextBoxColumn()
        NecNo = New DataGridViewTextBoxColumn()
        ICD9 = New DataGridViewTextBoxColumn()
        ICD9Description = New DataGridViewTextBoxColumn()
        tpLocation = New TabPage()
        GBLabAssociation = New GroupBox()
        Label36 = New Label()
        btnAddLab = New Button()
        Label37 = New Label()
        btnRemoveLabs = New Button()
        cmbLabs = New ComboBox()
        btnRemoveLab = New Button()
        txtRefLabTestID = New TextBox()
        dgvLabs = New DataGridView()
        TestID = New DataGridViewTextBoxColumn()
        RefLab = New DataGridViewTextBoxColumn()
        IsDefault = New DataGridViewCheckBoxColumn()
        RefLabTestID = New DataGridViewTextBoxColumn()
        btnRefLab = New Button()
        Label10 = New Label()
        chkCBillable = New CheckBox()
        Label4 = New Label()
        chkPBillable = New CheckBox()
        Label14 = New Label()
        ChkInHouse = New CheckBox()
        Label13 = New Label()
        ChkTBillable = New CheckBox()
        ToolTip1 = New ToolTip(components)
        txtLoinc = New TextBox()
        Label12 = New Label()
        txtSearchID = New TextBox()
        btnNote = New Button()
        txtResultNote = New TextBox()
        ToolStrip1.SuspendLayout()
        TabControl1.SuspendLayout()
        tpContents.SuspendLayout()
        CType(dgvTests, ComponentModel.ISupportInitialize).BeginInit()
        tpMarking.SuspendLayout()
        grpMarking.SuspendLayout()
        CType(dgvModifiers, ComponentModel.ISupportInitialize).BeginInit()
        tpBilling.SuspendLayout()
        TabControl3.SuspendLayout()
        tpPricing.SuspendLayout()
        tpNecessity.SuspendLayout()
        CType(dgvNecessity, ComponentModel.ISupportInitialize).BeginInit()
        tpLocation.SuspendLayout()
        GBLabAssociation.SuspendLayout()
        CType(dgvLabs, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(20, 20)
        ToolStrip1.Items.AddRange(New ToolStripItem() {chkEditNew, ToolStripButton2, btnSave, ToolStripSeparator1, btnDelete, ToolStripSeparator2, btnReplicate, ToolStripSeparator4, btnCancel, ToolStripSeparator3, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(1125, 34)
        ToolStrip1.TabIndex = 5
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' chkEditNew
        ' 
        chkEditNew.CheckOnClick = True
        chkEditNew.ForeColor = Color.DarkBlue
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
        btnSave.ForeColor = Color.DarkBlue
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
        btnDelete.ForeColor = Color.Red
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
        ' btnReplicate
        ' 
        btnReplicate.Enabled = False
        btnReplicate.Image = CType(resources.GetObject("btnReplicate.Image"), Image)
        btnReplicate.ImageTransparentColor = Color.Magenta
        btnReplicate.Name = "btnReplicate"
        btnReplicate.Size = New Size(106, 29)
        btnReplicate.Text = "Replicate"
        ' 
        ' ToolStripSeparator4
        ' 
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New Size(6, 34)
        ' 
        ' btnCancel
        ' 
        btnCancel.ForeColor = Color.DarkBlue
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
        btnHelp.ForeColor = Color.DarkBlue
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.ImageTransparentColor = Color.Magenta
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(73, 29)
        btnHelp.Text = "Help"
        ' 
        ' btnGroupLook
        ' 
        btnGroupLook.Image = CType(resources.GetObject("btnGroupLook.Image"), Image)
        btnGroupLook.Location = New Point(157, 88)
        btnGroupLook.Margin = New Padding(5, 6, 5, 6)
        btnGroupLook.Name = "btnGroupLook"
        btnGroupLook.Size = New Size(43, 48)
        btnGroupLook.TabIndex = 2
        btnGroupLook.TabStop = False
        btnGroupLook.UseVisualStyleBackColor = True
        ' 
        ' Label11
        ' 
        Label11.BackColor = Color.Transparent
        Label11.ForeColor = Color.DarkBlue
        Label11.Location = New Point(1000, 52)
        Label11.Margin = New Padding(5, 0, 5, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(92, 31)
        Label11.TabIndex = 90
        Label11.Text = "Markable"
        Label11.TextAlign = ContentAlignment.BottomCenter
        ' 
        ' ChkMarkable
        ' 
        ChkMarkable.Appearance = Appearance.Button
        ChkMarkable.CheckAlign = ContentAlignment.MiddleRight
        ChkMarkable.Checked = True
        ChkMarkable.CheckState = CheckState.Checked
        ChkMarkable.ForeColor = Color.DarkBlue
        ChkMarkable.Location = New Point(993, 88)
        ChkMarkable.Margin = New Padding(5, 6, 5, 6)
        ChkMarkable.Name = "ChkMarkable"
        ChkMarkable.Size = New Size(108, 48)
        ChkMarkable.TabIndex = 8
        ChkMarkable.Text = "YES"
        ChkMarkable.TextAlign = ContentAlignment.MiddleCenter
        ChkMarkable.TextImageRelation = TextImageRelation.ImageAboveText
        ChkMarkable.UseVisualStyleBackColor = True
        ' 
        ' Label8
        ' 
        Label8.BackColor = Color.Transparent
        Label8.ForeColor = Color.DarkBlue
        Label8.Location = New Point(882, 52)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(80, 31)
        Label8.TabIndex = 88
        Label8.Text = "Active"
        Label8.TextAlign = ContentAlignment.BottomCenter
        ' 
        ' ChkActive
        ' 
        ChkActive.Appearance = Appearance.Button
        ChkActive.CheckAlign = ContentAlignment.MiddleRight
        ChkActive.Checked = True
        ChkActive.CheckState = CheckState.Checked
        ChkActive.ForeColor = Color.DarkBlue
        ChkActive.Location = New Point(875, 88)
        ChkActive.Margin = New Padding(5, 6, 5, 6)
        ChkActive.Name = "ChkActive"
        ChkActive.Size = New Size(108, 48)
        ChkActive.TabIndex = 7
        ChkActive.Text = "YES"
        ChkActive.TextAlign = ContentAlignment.MiddleCenter
        ChkActive.TextImageRelation = TextImageRelation.ImageAboveText
        ChkActive.UseVisualStyleBackColor = True
        ' 
        ' txtDescription
        ' 
        txtDescription.Location = New Point(15, 210)
        txtDescription.Margin = New Padding(5, 6, 5, 6)
        txtDescription.MaxLength = 250
        txtDescription.Multiline = True
        txtDescription.Name = "txtDescription"
        txtDescription.ScrollBars = ScrollBars.Vertical
        txtDescription.Size = New Size(819, 102)
        txtDescription.TabIndex = 6
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.DarkBlue
        Label6.Location = New Point(17, 169)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(207, 35)
        Label6.TabIndex = 85
        Label6.Text = "Group Description"
        Label6.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' txtAbbr
        ' 
        txtAbbr.Location = New Point(688, 94)
        txtAbbr.Margin = New Padding(5, 6, 5, 6)
        txtAbbr.MaxLength = 10
        txtAbbr.Name = "txtAbbr"
        txtAbbr.Size = New Size(146, 31)
        txtAbbr.TabIndex = 4
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.Red
        Label3.Location = New Point(693, 50)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(133, 35)
        Label3.TabIndex = 82
        Label3.Text = "Group Abbr"
        Label3.TextAlign = ContentAlignment.BottomLeft
        ' 
        ' txtName
        ' 
        txtName.Location = New Point(210, 94)
        txtName.Margin = New Padding(5, 6, 5, 6)
        txtName.MaxLength = 60
        txtName.Name = "txtName"
        txtName.Size = New Size(449, 31)
        txtName.TabIndex = 3
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.Red
        Label2.Location = New Point(210, 58)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(228, 31)
        Label2.TabIndex = 80
        Label2.Text = "Group Name"
        ' 
        ' txtGroupID
        ' 
        txtGroupID.Location = New Point(473, 152)
        txtGroupID.Margin = New Padding(5, 6, 5, 6)
        txtGroupID.MaxLength = 12
        txtGroupID.Name = "txtGroupID"
        txtGroupID.Size = New Size(34, 31)
        txtGroupID.TabIndex = 127
        txtGroupID.TabStop = False
        txtGroupID.TextAlign = HorizontalAlignment.Center
        txtGroupID.Visible = False
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.Red
        Label1.Location = New Point(18, 58)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(120, 31)
        Label1.TabIndex = 78
        Label1.Text = "Group ID"
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(tpContents)
        TabControl1.Controls.Add(tpMarking)
        TabControl1.Controls.Add(tpBilling)
        TabControl1.Controls.Add(tpLocation)
        TabControl1.Location = New Point(15, 338)
        TabControl1.Margin = New Padding(5, 6, 5, 6)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(1087, 608)
        TabControl1.SizeMode = TabSizeMode.Fixed
        TabControl1.TabIndex = 7
        ' 
        ' tpContents
        ' 
        tpContents.AutoScroll = True
        tpContents.Controls.Add(chkBreakOROrder)
        tpContents.Controls.Add(Label9)
        tpContents.Controls.Add(txtTestName)
        tpContents.Controls.Add(btnTestLook)
        tpContents.Controls.Add(Label7)
        tpContents.Controls.Add(txtTestID)
        tpContents.Controls.Add(dgvTests)
        tpContents.Controls.Add(btnRemTst)
        tpContents.Controls.Add(btnRemTstAll)
        tpContents.Controls.Add(btnAddTest)
        tpContents.ForeColor = Color.Black
        tpContents.Location = New Point(4, 34)
        tpContents.Margin = New Padding(5, 6, 5, 6)
        tpContents.Name = "tpContents"
        tpContents.Padding = New Padding(5, 6, 5, 6)
        tpContents.Size = New Size(1079, 570)
        tpContents.TabIndex = 1
        tpContents.Text = "Contents"
        tpContents.UseVisualStyleBackColor = True
        ' 
        ' chkBreakOROrder
        ' 
        chkBreakOROrder.Location = New Point(440, 23)
        chkBreakOROrder.Margin = New Padding(5, 6, 5, 6)
        chkBreakOROrder.Name = "chkBreakOROrder"
        chkBreakOROrder.Size = New Size(200, 31)
        chkBreakOROrder.TabIndex = 19
        chkBreakOROrder.Text = "Break on OR Order"
        chkBreakOROrder.UseVisualStyleBackColor = True
        ' 
        ' Label9
        ' 
        Label9.ForeColor = Color.DarkBlue
        Label9.Location = New Point(243, 433)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(142, 27)
        Label9.TabIndex = 8
        Label9.Text = "Analyte Name"
        ' 
        ' txtTestName
        ' 
        txtTestName.Location = New Point(245, 473)
        txtTestName.Margin = New Padding(5, 6, 5, 6)
        txtTestName.MaxLength = 60
        txtTestName.Name = "txtTestName"
        txtTestName.ReadOnly = True
        txtTestName.Size = New Size(631, 31)
        txtTestName.TabIndex = 15
        ' 
        ' btnTestLook
        ' 
        btnTestLook.Image = CType(resources.GetObject("btnTestLook.Image"), Image)
        btnTestLook.Location = New Point(185, 463)
        btnTestLook.Margin = New Padding(5, 6, 5, 6)
        btnTestLook.Name = "btnTestLook"
        btnTestLook.Size = New Size(50, 50)
        btnTestLook.TabIndex = 14
        btnTestLook.TabStop = False
        btnTestLook.UseVisualStyleBackColor = True
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.DarkBlue
        Label7.Location = New Point(32, 433)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(142, 27)
        Label7.TabIndex = 5
        Label7.Text = "Analyte ID"
        ' 
        ' txtTestID
        ' 
        txtTestID.Location = New Point(30, 467)
        txtTestID.Margin = New Padding(5, 6, 5, 6)
        txtTestID.MaxLength = 12
        txtTestID.Name = "txtTestID"
        txtTestID.Size = New Size(139, 31)
        txtTestID.TabIndex = 13
        txtTestID.TextAlign = HorizontalAlignment.Center
        ' 
        ' dgvTests
        ' 
        dgvTests.AllowUserToAddRows = False
        dgvTests.AllowUserToDeleteRows = False
        dgvTests.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = Color.Azure
        dgvTests.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvTests.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvTests.Columns.AddRange(New DataGridViewColumn() {NR_ID, Logo, NRTo})
        dgvTests.Location = New Point(30, 73)
        dgvTests.Margin = New Padding(5, 6, 5, 6)
        dgvTests.Name = "dgvTests"
        dgvTests.ReadOnly = True
        dgvTests.RowHeadersVisible = False
        dgvTests.RowHeadersWidth = 51
        dgvTests.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvTests.Size = New Size(848, 335)
        dgvTests.TabIndex = 8
        dgvTests.TabStop = False
        ' 
        ' NR_ID
        ' 
        NR_ID.HeaderText = "Analyte ID"
        NR_ID.MaxInputLength = 12
        NR_ID.MinimumWidth = 6
        NR_ID.Name = "NR_ID"
        NR_ID.ReadOnly = True
        NR_ID.Width = 125
        ' 
        ' Logo
        ' 
        Logo.FillWeight = 30.0F
        Logo.HeaderText = ""
        Logo.MinimumWidth = 6
        Logo.Name = "Logo"
        Logo.ReadOnly = True
        Logo.Resizable = DataGridViewTriState.True
        Logo.Width = 30
        ' 
        ' NRTo
        ' 
        NRTo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        NRTo.HeaderText = "Analyte Name"
        NRTo.MaxInputLength = 60
        NRTo.MinimumWidth = 6
        NRTo.Name = "NRTo"
        NRTo.ReadOnly = True
        ' 
        ' btnRemTst
        ' 
        btnRemTst.Enabled = False
        btnRemTst.ForeColor = Color.Red
        btnRemTst.Location = New Point(907, 73)
        btnRemTst.Margin = New Padding(5, 6, 5, 6)
        btnRemTst.Name = "btnRemTst"
        btnRemTst.Size = New Size(132, 44)
        btnRemTst.TabIndex = 18
        btnRemTst.TabStop = False
        btnRemTst.Text = "Remove"
        btnRemTst.UseVisualStyleBackColor = True
        ' 
        ' btnRemTstAll
        ' 
        btnRemTstAll.Enabled = False
        btnRemTstAll.ForeColor = Color.Red
        btnRemTstAll.Location = New Point(907, 363)
        btnRemTstAll.Margin = New Padding(5, 6, 5, 6)
        btnRemTstAll.Name = "btnRemTstAll"
        btnRemTstAll.Size = New Size(132, 44)
        btnRemTstAll.TabIndex = 17
        btnRemTstAll.TabStop = False
        btnRemTstAll.Text = "Remove All"
        btnRemTstAll.UseVisualStyleBackColor = True
        ' 
        ' btnAddTest
        ' 
        btnAddTest.Enabled = False
        btnAddTest.ForeColor = Color.DarkGreen
        btnAddTest.Location = New Point(907, 467)
        btnAddTest.Margin = New Padding(5, 6, 5, 6)
        btnAddTest.Name = "btnAddTest"
        btnAddTest.Size = New Size(132, 44)
        btnAddTest.TabIndex = 16
        btnAddTest.Text = "Add to List"
        btnAddTest.UseVisualStyleBackColor = True
        ' 
        ' tpMarking
        ' 
        tpMarking.AutoScroll = True
        tpMarking.Controls.Add(grpMarking)
        tpMarking.Location = New Point(4, 34)
        tpMarking.Margin = New Padding(5, 6, 5, 6)
        tpMarking.Name = "tpMarking"
        tpMarking.Padding = New Padding(5, 6, 5, 6)
        tpMarking.Size = New Size(1079, 570)
        tpMarking.TabIndex = 0
        tpMarking.Text = "Marking"
        tpMarking.UseVisualStyleBackColor = True
        ' 
        ' grpMarking
        ' 
        grpMarking.Controls.Add(btnRemoveMod)
        grpMarking.Controls.Add(Label16)
        grpMarking.Controls.Add(Label17)
        grpMarking.Controls.Add(cmbSex)
        grpMarking.Controls.Add(Label18)
        grpMarking.Controls.Add(btnRemoveAllMod)
        grpMarking.Controls.Add(txtAgeFrom)
        grpMarking.Controls.Add(btnAddModifier)
        grpMarking.Controls.Add(Label15)
        grpMarking.Controls.Add(txtAgeTo)
        grpMarking.Controls.Add(dgvModifiers)
        grpMarking.Location = New Point(10, 12)
        grpMarking.Margin = New Padding(5, 6, 5, 6)
        grpMarking.Name = "grpMarking"
        grpMarking.Padding = New Padding(5, 6, 5, 6)
        grpMarking.Size = New Size(917, 377)
        grpMarking.TabIndex = 11
        grpMarking.TabStop = False
        ' 
        ' btnRemoveMod
        ' 
        btnRemoveMod.Enabled = False
        btnRemoveMod.ForeColor = Color.Red
        btnRemoveMod.Location = New Point(785, 50)
        btnRemoveMod.Margin = New Padding(5, 6, 5, 6)
        btnRemoveMod.Name = "btnRemoveMod"
        btnRemoveMod.Size = New Size(120, 40)
        btnRemoveMod.TabIndex = 26
        btnRemoveMod.TabStop = False
        btnRemoveMod.Text = "Remove"
        btnRemoveMod.UseVisualStyleBackColor = True
        ' 
        ' Label16
        ' 
        Label16.ForeColor = Color.MidnightBlue
        Label16.Location = New Point(13, 292)
        Label16.Margin = New Padding(5, 0, 5, 0)
        Label16.Name = "Label16"
        Label16.Size = New Size(140, 25)
        Label16.TabIndex = 8
        Label16.Text = "Gender"
        Label16.TextAlign = ContentAlignment.BottomLeft
        ' 
        ' Label17
        ' 
        Label17.ForeColor = Color.MidnightBlue
        Label17.Location = New Point(267, 288)
        Label17.Margin = New Padding(5, 0, 5, 0)
        Label17.Name = "Label17"
        Label17.Size = New Size(140, 25)
        Label17.TabIndex = 9
        Label17.Text = "Age Lower Limit"
        Label17.TextAlign = ContentAlignment.BottomLeft
        ' 
        ' cmbSex
        ' 
        cmbSex.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSex.FormattingEnabled = True
        cmbSex.Items.AddRange(New Object() {"F - Female", "I - Inbetween", "M - Male", "U - Unreported"})
        cmbSex.Location = New Point(18, 323)
        cmbSex.Margin = New Padding(5, 6, 5, 6)
        cmbSex.Name = "cmbSex"
        cmbSex.Size = New Size(204, 33)
        cmbSex.Sorted = True
        cmbSex.TabIndex = 21
        ' 
        ' Label18
        ' 
        Label18.ForeColor = Color.MidnightBlue
        Label18.Location = New Point(427, 288)
        Label18.Margin = New Padding(5, 0, 5, 0)
        Label18.Name = "Label18"
        Label18.Size = New Size(152, 25)
        Label18.TabIndex = 10
        Label18.Text = "Age Upper Limit"
        Label18.TextAlign = ContentAlignment.BottomLeft
        ' 
        ' btnRemoveAllMod
        ' 
        btnRemoveAllMod.Enabled = False
        btnRemoveAllMod.ForeColor = Color.Red
        btnRemoveAllMod.Location = New Point(785, 227)
        btnRemoveAllMod.Margin = New Padding(5, 6, 5, 6)
        btnRemoveAllMod.Name = "btnRemoveAllMod"
        btnRemoveAllMod.Size = New Size(122, 42)
        btnRemoveAllMod.TabIndex = 25
        btnRemoveAllMod.TabStop = False
        btnRemoveAllMod.Text = "Remove All"
        btnRemoveAllMod.UseVisualStyleBackColor = True
        ' 
        ' txtAgeFrom
        ' 
        txtAgeFrom.Location = New Point(272, 323)
        txtAgeFrom.Margin = New Padding(5, 6, 5, 6)
        txtAgeFrom.MaxLength = 3
        txtAgeFrom.Name = "txtAgeFrom"
        txtAgeFrom.Size = New Size(132, 31)
        txtAgeFrom.TabIndex = 22
        txtAgeFrom.TextAlign = HorizontalAlignment.Center
        ' 
        ' btnAddModifier
        ' 
        btnAddModifier.Enabled = False
        btnAddModifier.ForeColor = Color.MidnightBlue
        btnAddModifier.Location = New Point(620, 319)
        btnAddModifier.Margin = New Padding(5, 6, 5, 6)
        btnAddModifier.Name = "btnAddModifier"
        btnAddModifier.Size = New Size(155, 42)
        btnAddModifier.TabIndex = 24
        btnAddModifier.Text = "Add to List"
        btnAddModifier.UseVisualStyleBackColor = True
        ' 
        ' Label15
        ' 
        Label15.ForeColor = Color.MidnightBlue
        Label15.Location = New Point(13, 19)
        Label15.Margin = New Padding(5, 0, 5, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(762, 25)
        Label15.TabIndex = 0
        Label15.Text = "Modifiers (System restricts marking if the following conditions are not met)"
        Label15.TextAlign = ContentAlignment.BottomLeft
        ' 
        ' txtAgeTo
        ' 
        txtAgeTo.Location = New Point(432, 323)
        txtAgeTo.Margin = New Padding(5, 6, 5, 6)
        txtAgeTo.MaxLength = 3
        txtAgeTo.Name = "txtAgeTo"
        txtAgeTo.Size = New Size(144, 31)
        txtAgeTo.TabIndex = 23
        txtAgeTo.TextAlign = HorizontalAlignment.Center
        ' 
        ' dgvModifiers
        ' 
        dgvModifiers.AllowUserToAddRows = False
        dgvModifiers.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = Color.FromArgb(CByte(255), CByte(224), CByte(192))
        dgvModifiers.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        dgvModifiers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvModifiers.Columns.AddRange(New DataGridViewColumn() {Test_ID, Modifier, Gender, AgeFrom, AgeTo})
        dgvModifiers.Location = New Point(13, 50)
        dgvModifiers.Margin = New Padding(5, 6, 5, 6)
        dgvModifiers.Name = "dgvModifiers"
        dgvModifiers.ReadOnly = True
        dgvModifiers.RowHeadersVisible = False
        dgvModifiers.RowHeadersWidth = 51
        dgvModifiers.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvModifiers.Size = New Size(762, 219)
        dgvModifiers.TabIndex = 19
        dgvModifiers.TabStop = False
        ' 
        ' Test_ID
        ' 
        Test_ID.HeaderText = "Test_ID"
        Test_ID.MinimumWidth = 6
        Test_ID.Name = "Test_ID"
        Test_ID.ReadOnly = True
        Test_ID.Visible = False
        Test_ID.Width = 125
        ' 
        ' Modifier
        ' 
        Modifier.FillWeight = 60.0F
        Modifier.HeaderText = "No."
        Modifier.MinimumWidth = 6
        Modifier.Name = "Modifier"
        Modifier.ReadOnly = True
        Modifier.SortMode = DataGridViewColumnSortMode.NotSortable
        Modifier.Width = 60
        ' 
        ' Gender
        ' 
        Gender.HeaderText = "Gender"
        Gender.MinimumWidth = 6
        Gender.Name = "Gender"
        Gender.ReadOnly = True
        Gender.SortMode = DataGridViewColumnSortMode.NotSortable
        Gender.Width = 125
        ' 
        ' AgeFrom
        ' 
        AgeFrom.FillWeight = 140.0F
        AgeFrom.HeaderText = "Age (Lower Limit)"
        AgeFrom.MinimumWidth = 6
        AgeFrom.Name = "AgeFrom"
        AgeFrom.ReadOnly = True
        AgeFrom.SortMode = DataGridViewColumnSortMode.NotSortable
        AgeFrom.Width = 140
        ' 
        ' AgeTo
        ' 
        AgeTo.FillWeight = 150.0F
        AgeTo.HeaderText = "Age (Upper Limit)"
        AgeTo.MinimumWidth = 6
        AgeTo.Name = "AgeTo"
        AgeTo.ReadOnly = True
        AgeTo.SortMode = DataGridViewColumnSortMode.NotSortable
        AgeTo.Width = 150
        ' 
        ' tpBilling
        ' 
        tpBilling.Controls.Add(TabControl3)
        tpBilling.Location = New Point(4, 34)
        tpBilling.Margin = New Padding(5, 6, 5, 6)
        tpBilling.Name = "tpBilling"
        tpBilling.Size = New Size(1079, 570)
        tpBilling.TabIndex = 2
        tpBilling.Text = "Billing"
        tpBilling.UseVisualStyleBackColor = True
        ' 
        ' TabControl3
        ' 
        TabControl3.Controls.Add(tpPricing)
        TabControl3.Controls.Add(tpNecessity)
        TabControl3.Location = New Point(15, 31)
        TabControl3.Margin = New Padding(5, 6, 5, 6)
        TabControl3.Name = "TabControl3"
        TabControl3.SelectedIndex = 0
        TabControl3.Size = New Size(1040, 523)
        TabControl3.SizeMode = TabSizeMode.Fixed
        TabControl3.TabIndex = 30
        ' 
        ' tpPricing
        ' 
        tpPricing.Controls.Add(txtPOS)
        tpPricing.Controls.Add(Label72)
        tpPricing.Controls.Add(txtMod1)
        tpPricing.Controls.Add(Label75)
        tpPricing.Controls.Add(txtMod2)
        tpPricing.Controls.Add(Label76)
        tpPricing.Controls.Add(Label77)
        tpPricing.Controls.Add(txtMod4)
        tpPricing.Controls.Add(txtMod3)
        tpPricing.Controls.Add(Label78)
        tpPricing.Controls.Add(txtCPTSPC)
        tpPricing.Controls.Add(Label68)
        tpPricing.Controls.Add(txtCPTMCD)
        tpPricing.Controls.Add(Label67)
        tpPricing.Controls.Add(txtCPTMCR)
        tpPricing.Controls.Add(Label66)
        tpPricing.Controls.Add(Label65)
        tpPricing.Controls.Add(txtBillUnit)
        tpPricing.Controls.Add(txtCPTCode)
        tpPricing.Controls.Add(txtPrice9)
        tpPricing.Controls.Add(Label29)
        tpPricing.Controls.Add(btnCPTLook)
        tpPricing.Controls.Add(txtPrice8)
        tpPricing.Controls.Add(Label30)
        tpPricing.Controls.Add(txtPrice7)
        tpPricing.Controls.Add(Label5)
        tpPricing.Controls.Add(Label31)
        tpPricing.Controls.Add(txtPrice6)
        tpPricing.Controls.Add(Label32)
        tpPricing.Controls.Add(txtPrice4)
        tpPricing.Controls.Add(txtPrice5)
        tpPricing.Controls.Add(Label24)
        tpPricing.Controls.Add(Label33)
        tpPricing.Controls.Add(txtListPrice)
        tpPricing.Controls.Add(Label25)
        tpPricing.Controls.Add(txtPrice1)
        tpPricing.Controls.Add(Label28)
        tpPricing.Controls.Add(Label26)
        tpPricing.Controls.Add(txtPrice3)
        tpPricing.Controls.Add(txtPrice2)
        tpPricing.Controls.Add(Label27)
        tpPricing.Location = New Point(4, 34)
        tpPricing.Margin = New Padding(5, 6, 5, 6)
        tpPricing.Name = "tpPricing"
        tpPricing.Padding = New Padding(5, 6, 5, 6)
        tpPricing.Size = New Size(1032, 485)
        tpPricing.TabIndex = 0
        tpPricing.Text = "Pricing"
        tpPricing.UseVisualStyleBackColor = True
        ' 
        ' txtPOS
        ' 
        txtPOS.Location = New Point(855, 175)
        txtPOS.Margin = New Padding(5, 6, 5, 6)
        txtPOS.MaxLength = 12
        txtPOS.Name = "txtPOS"
        txtPOS.Size = New Size(147, 31)
        txtPOS.TabIndex = 79
        txtPOS.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label72
        ' 
        Label72.ForeColor = Color.RoyalBlue
        Label72.Location = New Point(10, 135)
        Label72.Margin = New Padding(5, 0, 5, 0)
        Label72.Name = "Label72"
        Label72.Size = New Size(152, 25)
        Label72.TabIndex = 94
        Label72.Text = "Modifier 1"
        ' 
        ' txtMod1
        ' 
        txtMod1.Location = New Point(10, 175)
        txtMod1.Margin = New Padding(5, 6, 5, 6)
        txtMod1.MaxLength = 12
        txtMod1.Name = "txtMod1"
        txtMod1.Size = New Size(147, 31)
        txtMod1.TabIndex = 75
        txtMod1.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label75
        ' 
        Label75.ForeColor = Color.RoyalBlue
        Label75.Location = New Point(235, 135)
        Label75.Margin = New Padding(5, 0, 5, 0)
        Label75.Name = "Label75"
        Label75.Size = New Size(138, 25)
        Label75.TabIndex = 95
        Label75.Text = "Modifier 2"
        ' 
        ' txtMod2
        ' 
        txtMod2.Location = New Point(223, 175)
        txtMod2.Margin = New Padding(5, 6, 5, 6)
        txtMod2.MaxLength = 12
        txtMod2.Name = "txtMod2"
        txtMod2.Size = New Size(147, 31)
        txtMod2.TabIndex = 76
        txtMod2.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label76
        ' 
        Label76.ForeColor = Color.RoyalBlue
        Label76.Location = New Point(858, 135)
        Label76.Margin = New Padding(5, 0, 5, 0)
        Label76.Name = "Label76"
        Label76.Size = New Size(137, 25)
        Label76.TabIndex = 98
        Label76.Text = "POS Code"
        ' 
        ' Label77
        ' 
        Label77.ForeColor = Color.RoyalBlue
        Label77.Location = New Point(433, 135)
        Label77.Margin = New Padding(5, 0, 5, 0)
        Label77.Name = "Label77"
        Label77.Size = New Size(138, 25)
        Label77.TabIndex = 96
        Label77.Text = "Modifier 3"
        ' 
        ' txtMod4
        ' 
        txtMod4.Location = New Point(643, 175)
        txtMod4.Margin = New Padding(5, 6, 5, 6)
        txtMod4.MaxLength = 12
        txtMod4.Name = "txtMod4"
        txtMod4.Size = New Size(147, 31)
        txtMod4.TabIndex = 78
        txtMod4.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtMod3
        ' 
        txtMod3.Location = New Point(433, 175)
        txtMod3.Margin = New Padding(5, 6, 5, 6)
        txtMod3.MaxLength = 12
        txtMod3.Name = "txtMod3"
        txtMod3.Size = New Size(147, 31)
        txtMod3.TabIndex = 77
        txtMod3.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label78
        ' 
        Label78.ForeColor = Color.RoyalBlue
        Label78.Location = New Point(645, 135)
        Label78.Margin = New Padding(5, 0, 5, 0)
        Label78.Name = "Label78"
        Label78.Size = New Size(148, 25)
        Label78.TabIndex = 97
        Label78.Text = "Modifier 4"
        ' 
        ' txtCPTSPC
        ' 
        txtCPTSPC.Location = New Point(643, 63)
        txtCPTSPC.Margin = New Padding(5, 6, 5, 6)
        txtCPTSPC.MaxLength = 10
        txtCPTSPC.Name = "txtCPTSPC"
        txtCPTSPC.Size = New Size(147, 31)
        txtCPTSPC.TabIndex = 73
        txtCPTSPC.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label68
        ' 
        Label68.ForeColor = Color.Magenta
        Label68.Location = New Point(645, 25)
        Label68.Margin = New Padding(5, 0, 5, 0)
        Label68.Name = "Label68"
        Label68.Size = New Size(148, 25)
        Label68.TabIndex = 93
        Label68.Text = "CPT SPC"
        ' 
        ' txtCPTMCD
        ' 
        txtCPTMCD.Location = New Point(433, 63)
        txtCPTMCD.Margin = New Padding(5, 6, 5, 6)
        txtCPTMCD.MaxLength = 10
        txtCPTMCD.Name = "txtCPTMCD"
        txtCPTMCD.Size = New Size(147, 31)
        txtCPTMCD.TabIndex = 71
        txtCPTMCD.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label67
        ' 
        Label67.ForeColor = Color.Magenta
        Label67.Location = New Point(433, 25)
        Label67.Margin = New Padding(5, 0, 5, 0)
        Label67.Name = "Label67"
        Label67.Size = New Size(138, 25)
        Label67.TabIndex = 91
        Label67.Text = "CPT MCD"
        ' 
        ' txtCPTMCR
        ' 
        txtCPTMCR.Location = New Point(223, 63)
        txtCPTMCR.Margin = New Padding(5, 6, 5, 6)
        txtCPTMCR.MaxLength = 10
        txtCPTMCR.Name = "txtCPTMCR"
        txtCPTMCR.Size = New Size(147, 31)
        txtCPTMCR.TabIndex = 68
        txtCPTMCR.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label66
        ' 
        Label66.ForeColor = Color.Magenta
        Label66.Location = New Point(235, 25)
        Label66.Margin = New Padding(5, 0, 5, 0)
        Label66.Name = "Label66"
        Label66.Size = New Size(138, 25)
        Label66.TabIndex = 87
        Label66.Text = "CPT MCR"
        ' 
        ' Label65
        ' 
        Label65.ForeColor = Color.DarkBlue
        Label65.Location = New Point(858, 25)
        Label65.Margin = New Padding(5, 0, 5, 0)
        Label65.Name = "Label65"
        Label65.Size = New Size(130, 25)
        Label65.TabIndex = 86
        Label65.Text = "Billing Unit(s)"
        ' 
        ' txtBillUnit
        ' 
        txtBillUnit.Location = New Point(855, 63)
        txtBillUnit.Margin = New Padding(5, 6, 5, 6)
        txtBillUnit.MaxLength = 5
        txtBillUnit.Name = "txtBillUnit"
        txtBillUnit.Size = New Size(147, 31)
        txtBillUnit.TabIndex = 74
        txtBillUnit.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtCPTCode
        ' 
        txtCPTCode.Location = New Point(10, 63)
        txtCPTCode.Margin = New Padding(5, 6, 5, 6)
        txtCPTCode.MaxLength = 10
        txtCPTCode.Name = "txtCPTCode"
        txtCPTCode.Size = New Size(147, 31)
        txtCPTCode.TabIndex = 67
        txtCPTCode.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtPrice9
        ' 
        txtPrice9.Location = New Point(855, 394)
        txtPrice9.Margin = New Padding(5, 6, 5, 6)
        txtPrice9.MaxLength = 9
        txtPrice9.Name = "txtPrice9"
        txtPrice9.Size = New Size(147, 31)
        txtPrice9.TabIndex = 92
        txtPrice9.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label29
        ' 
        Label29.ForeColor = Color.RoyalBlue
        Label29.Location = New Point(858, 358)
        Label29.Margin = New Padding(5, 0, 5, 0)
        Label29.Name = "Label29"
        Label29.Size = New Size(137, 25)
        Label29.TabIndex = 72
        Label29.Text = "Price Level 9"
        ' 
        ' btnCPTLook
        ' 
        btnCPTLook.Image = CType(resources.GetObject("btnCPTLook.Image"), Image)
        btnCPTLook.Location = New Point(170, 58)
        btnCPTLook.Margin = New Padding(5, 6, 5, 6)
        btnCPTLook.Name = "btnCPTLook"
        btnCPTLook.Size = New Size(43, 48)
        btnCPTLook.TabIndex = 70
        btnCPTLook.TabStop = False
        btnCPTLook.UseVisualStyleBackColor = True
        ' 
        ' txtPrice8
        ' 
        txtPrice8.Location = New Point(643, 394)
        txtPrice8.Margin = New Padding(5, 6, 5, 6)
        txtPrice8.MaxLength = 9
        txtPrice8.Name = "txtPrice8"
        txtPrice8.Size = New Size(147, 31)
        txtPrice8.TabIndex = 90
        txtPrice8.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label30
        ' 
        Label30.ForeColor = Color.RoyalBlue
        Label30.Location = New Point(645, 358)
        Label30.Margin = New Padding(5, 0, 5, 0)
        Label30.Name = "Label30"
        Label30.Size = New Size(148, 25)
        Label30.TabIndex = 69
        Label30.Text = "Price Level 8"
        ' 
        ' txtPrice7
        ' 
        txtPrice7.Location = New Point(433, 394)
        txtPrice7.Margin = New Padding(5, 6, 5, 6)
        txtPrice7.MaxLength = 9
        txtPrice7.Name = "txtPrice7"
        txtPrice7.Size = New Size(147, 31)
        txtPrice7.TabIndex = 89
        txtPrice7.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.Magenta
        Label5.Location = New Point(10, 25)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(152, 25)
        Label5.TabIndex = 58
        Label5.Text = "CPT Code"
        ' 
        ' Label31
        ' 
        Label31.ForeColor = Color.RoyalBlue
        Label31.Location = New Point(433, 358)
        Label31.Margin = New Padding(5, 0, 5, 0)
        Label31.Name = "Label31"
        Label31.Size = New Size(138, 25)
        Label31.TabIndex = 66
        Label31.Text = "Price Level 7"
        ' 
        ' txtPrice6
        ' 
        txtPrice6.Location = New Point(223, 394)
        txtPrice6.Margin = New Padding(5, 6, 5, 6)
        txtPrice6.MaxLength = 9
        txtPrice6.Name = "txtPrice6"
        txtPrice6.Size = New Size(147, 31)
        txtPrice6.TabIndex = 88
        txtPrice6.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label32
        ' 
        Label32.ForeColor = Color.RoyalBlue
        Label32.Location = New Point(235, 358)
        Label32.Margin = New Padding(5, 0, 5, 0)
        Label32.Name = "Label32"
        Label32.Size = New Size(138, 25)
        Label32.TabIndex = 65
        Label32.Text = "Price Level 6"
        ' 
        ' txtPrice4
        ' 
        txtPrice4.Location = New Point(855, 288)
        txtPrice4.Margin = New Padding(5, 6, 5, 6)
        txtPrice4.MaxLength = 9
        txtPrice4.Name = "txtPrice4"
        txtPrice4.Size = New Size(147, 31)
        txtPrice4.TabIndex = 84
        txtPrice4.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtPrice5
        ' 
        txtPrice5.Location = New Point(10, 394)
        txtPrice5.Margin = New Padding(5, 6, 5, 6)
        txtPrice5.MaxLength = 9
        txtPrice5.Name = "txtPrice5"
        txtPrice5.Size = New Size(147, 31)
        txtPrice5.TabIndex = 85
        txtPrice5.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label24
        ' 
        Label24.ForeColor = Color.MediumVioletRed
        Label24.Location = New Point(10, 252)
        Label24.Margin = New Padding(5, 0, 5, 0)
        Label24.Name = "Label24"
        Label24.Size = New Size(152, 25)
        Label24.TabIndex = 59
        Label24.Text = "List Price"
        ' 
        ' Label33
        ' 
        Label33.ForeColor = Color.RoyalBlue
        Label33.Location = New Point(10, 358)
        Label33.Margin = New Padding(5, 0, 5, 0)
        Label33.Name = "Label33"
        Label33.Size = New Size(152, 25)
        Label33.TabIndex = 64
        Label33.Text = "Price Level 5"
        ' 
        ' txtListPrice
        ' 
        txtListPrice.Location = New Point(10, 288)
        txtListPrice.Margin = New Padding(5, 6, 5, 6)
        txtListPrice.MaxLength = 9
        txtListPrice.Name = "txtListPrice"
        txtListPrice.Size = New Size(147, 31)
        txtListPrice.TabIndex = 80
        txtListPrice.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label25
        ' 
        Label25.ForeColor = Color.RoyalBlue
        Label25.Location = New Point(235, 252)
        Label25.Margin = New Padding(5, 0, 5, 0)
        Label25.Name = "Label25"
        Label25.Size = New Size(138, 25)
        Label25.TabIndex = 60
        Label25.Text = "Price Level 1"
        ' 
        ' txtPrice1
        ' 
        txtPrice1.Location = New Point(223, 288)
        txtPrice1.Margin = New Padding(5, 6, 5, 6)
        txtPrice1.MaxLength = 9
        txtPrice1.Name = "txtPrice1"
        txtPrice1.Size = New Size(147, 31)
        txtPrice1.TabIndex = 81
        txtPrice1.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label28
        ' 
        Label28.ForeColor = Color.RoyalBlue
        Label28.Location = New Point(858, 252)
        Label28.Margin = New Padding(5, 0, 5, 0)
        Label28.Name = "Label28"
        Label28.Size = New Size(137, 25)
        Label28.TabIndex = 63
        Label28.Text = "Price Level 4"
        ' 
        ' Label26
        ' 
        Label26.ForeColor = Color.RoyalBlue
        Label26.Location = New Point(433, 252)
        Label26.Margin = New Padding(5, 0, 5, 0)
        Label26.Name = "Label26"
        Label26.Size = New Size(138, 25)
        Label26.TabIndex = 61
        Label26.Text = "Price Level 2"
        ' 
        ' txtPrice3
        ' 
        txtPrice3.Location = New Point(643, 288)
        txtPrice3.Margin = New Padding(5, 6, 5, 6)
        txtPrice3.MaxLength = 9
        txtPrice3.Name = "txtPrice3"
        txtPrice3.Size = New Size(147, 31)
        txtPrice3.TabIndex = 83
        txtPrice3.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtPrice2
        ' 
        txtPrice2.Location = New Point(433, 288)
        txtPrice2.Margin = New Padding(5, 6, 5, 6)
        txtPrice2.MaxLength = 9
        txtPrice2.Name = "txtPrice2"
        txtPrice2.Size = New Size(147, 31)
        txtPrice2.TabIndex = 82
        txtPrice2.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label27
        ' 
        Label27.ForeColor = Color.RoyalBlue
        Label27.Location = New Point(645, 252)
        Label27.Margin = New Padding(5, 0, 5, 0)
        Label27.Name = "Label27"
        Label27.Size = New Size(148, 25)
        Label27.TabIndex = 62
        Label27.Text = "Price Level 3"
        ' 
        ' tpNecessity
        ' 
        tpNecessity.Controls.Add(btnNecCopy)
        tpNecessity.Controls.Add(btnICD9Lookup)
        tpNecessity.Controls.Add(btnAddNec)
        tpNecessity.Controls.Add(btnRemAllNec)
        tpNecessity.Controls.Add(txtICD9Description)
        tpNecessity.Controls.Add(Label53)
        tpNecessity.Controls.Add(txtICD9Code)
        tpNecessity.Controls.Add(Label52)
        tpNecessity.Controls.Add(btnRemNec)
        tpNecessity.Controls.Add(dgvNecessity)
        tpNecessity.Location = New Point(4, 34)
        tpNecessity.Margin = New Padding(5, 6, 5, 6)
        tpNecessity.Name = "tpNecessity"
        tpNecessity.Padding = New Padding(5, 6, 5, 6)
        tpNecessity.Size = New Size(1032, 485)
        tpNecessity.TabIndex = 1
        tpNecessity.Text = "Medical Necessity"
        tpNecessity.UseVisualStyleBackColor = True
        ' 
        ' btnNecCopy
        ' 
        btnNecCopy.ForeColor = Color.DarkBlue
        btnNecCopy.Location = New Point(873, 25)
        btnNecCopy.Margin = New Padding(5, 6, 5, 6)
        btnNecCopy.Name = "btnNecCopy"
        btnNecCopy.Size = New Size(132, 44)
        btnNecCopy.TabIndex = 50
        btnNecCopy.TabStop = False
        btnNecCopy.Text = "Copy From"
        btnNecCopy.UseVisualStyleBackColor = True
        ' 
        ' btnICD9Lookup
        ' 
        btnICD9Lookup.ForeColor = Color.Red
        btnICD9Lookup.Image = CType(resources.GetObject("btnICD9Lookup.Image"), Image)
        btnICD9Lookup.Location = New Point(165, 390)
        btnICD9Lookup.Margin = New Padding(5, 6, 5, 6)
        btnICD9Lookup.Name = "btnICD9Lookup"
        btnICD9Lookup.Size = New Size(45, 44)
        btnICD9Lookup.TabIndex = 45
        btnICD9Lookup.TabStop = False
        btnICD9Lookup.UseVisualStyleBackColor = True
        ' 
        ' btnAddNec
        ' 
        btnAddNec.Enabled = False
        btnAddNec.ForeColor = Color.DarkGreen
        btnAddNec.Location = New Point(873, 392)
        btnAddNec.Margin = New Padding(5, 6, 5, 6)
        btnAddNec.Name = "btnAddNec"
        btnAddNec.Size = New Size(132, 44)
        btnAddNec.TabIndex = 47
        btnAddNec.Text = "Add to List"
        btnAddNec.UseVisualStyleBackColor = True
        ' 
        ' btnRemAllNec
        ' 
        btnRemAllNec.Enabled = False
        btnRemAllNec.ForeColor = Color.Red
        btnRemAllNec.Location = New Point(873, 298)
        btnRemAllNec.Margin = New Padding(5, 6, 5, 6)
        btnRemAllNec.Name = "btnRemAllNec"
        btnRemAllNec.Size = New Size(132, 44)
        btnRemAllNec.TabIndex = 48
        btnRemAllNec.TabStop = False
        btnRemAllNec.Text = "Remove All"
        btnRemAllNec.UseVisualStyleBackColor = True
        ' 
        ' txtICD9Description
        ' 
        txtICD9Description.Location = New Point(222, 398)
        txtICD9Description.Margin = New Padding(5, 6, 5, 6)
        txtICD9Description.MaxLength = 250
        txtICD9Description.Name = "txtICD9Description"
        txtICD9Description.ReadOnly = True
        txtICD9Description.Size = New Size(616, 31)
        txtICD9Description.TabIndex = 46
        ' 
        ' Label53
        ' 
        Label53.AutoSize = True
        Label53.ForeColor = Color.DarkGreen
        Label53.Location = New Point(217, 365)
        Label53.Margin = New Padding(5, 0, 5, 0)
        Label53.Name = "Label53"
        Label53.Size = New Size(149, 25)
        Label53.TabIndex = 14
        Label53.Text = "Code Description"
        ' 
        ' txtICD9Code
        ' 
        txtICD9Code.Location = New Point(20, 398)
        txtICD9Code.Margin = New Padding(5, 6, 5, 6)
        txtICD9Code.MaxLength = 10
        txtICD9Code.Name = "txtICD9Code"
        txtICD9Code.Size = New Size(122, 31)
        txtICD9Code.TabIndex = 44
        ' 
        ' Label52
        ' 
        Label52.AutoSize = True
        Label52.ForeColor = Color.DarkGreen
        Label52.Location = New Point(25, 365)
        Label52.Margin = New Padding(5, 0, 5, 0)
        Label52.Name = "Label52"
        Label52.Size = New Size(80, 25)
        Label52.TabIndex = 12
        Label52.Text = "Dx Code"
        ' 
        ' btnRemNec
        ' 
        btnRemNec.Enabled = False
        btnRemNec.ForeColor = Color.Red
        btnRemNec.Location = New Point(873, 102)
        btnRemNec.Margin = New Padding(5, 6, 5, 6)
        btnRemNec.Name = "btnRemNec"
        btnRemNec.Size = New Size(132, 44)
        btnRemNec.TabIndex = 49
        btnRemNec.TabStop = False
        btnRemNec.Text = "Remove"
        btnRemNec.UseVisualStyleBackColor = True
        ' 
        ' dgvNecessity
        ' 
        dgvNecessity.AllowUserToAddRows = False
        dgvNecessity.AllowUserToDeleteRows = False
        DataGridViewCellStyle3.BackColor = Color.MintCream
        dgvNecessity.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        dgvNecessity.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvNecessity.Columns.AddRange(New DataGridViewColumn() {NecTest_ID, NecNo, ICD9, ICD9Description})
        dgvNecessity.Location = New Point(20, 25)
        dgvNecessity.Margin = New Padding(5, 6, 5, 6)
        dgvNecessity.Name = "dgvNecessity"
        dgvNecessity.ReadOnly = True
        dgvNecessity.RowHeadersVisible = False
        dgvNecessity.RowHeadersWidth = 51
        dgvNecessity.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvNecessity.Size = New Size(820, 315)
        dgvNecessity.TabIndex = 43
        dgvNecessity.TabStop = False
        ' 
        ' NecTest_ID
        ' 
        NecTest_ID.HeaderText = "Test ID"
        NecTest_ID.MaxInputLength = 10
        NecTest_ID.MinimumWidth = 6
        NecTest_ID.Name = "NecTest_ID"
        NecTest_ID.ReadOnly = True
        NecTest_ID.SortMode = DataGridViewColumnSortMode.NotSortable
        NecTest_ID.Visible = False
        NecTest_ID.Width = 125
        ' 
        ' NecNo
        ' 
        NecNo.FillWeight = 40.0F
        NecNo.HeaderText = "No."
        NecNo.MaxInputLength = 3
        NecNo.MinimumWidth = 6
        NecNo.Name = "NecNo"
        NecNo.ReadOnly = True
        NecNo.SortMode = DataGridViewColumnSortMode.NotSortable
        NecNo.Width = 40
        ' 
        ' ICD9
        ' 
        ICD9.FillWeight = 80.0F
        ICD9.HeaderText = "ICD9 Code"
        ICD9.MaxInputLength = 10
        ICD9.MinimumWidth = 6
        ICD9.Name = "ICD9"
        ICD9.ReadOnly = True
        ICD9.SortMode = DataGridViewColumnSortMode.NotSortable
        ICD9.Width = 80
        ' 
        ' ICD9Description
        ' 
        ICD9Description.FillWeight = 368.0F
        ICD9Description.HeaderText = "ICD9 Description"
        ICD9Description.MaxInputLength = 250
        ICD9Description.MinimumWidth = 6
        ICD9Description.Name = "ICD9Description"
        ICD9Description.ReadOnly = True
        ICD9Description.SortMode = DataGridViewColumnSortMode.NotSortable
        ICD9Description.Width = 368
        ' 
        ' tpLocation
        ' 
        tpLocation.Controls.Add(GBLabAssociation)
        tpLocation.Location = New Point(4, 34)
        tpLocation.Margin = New Padding(5, 6, 5, 6)
        tpLocation.Name = "tpLocation"
        tpLocation.Size = New Size(1079, 570)
        tpLocation.TabIndex = 3
        tpLocation.Text = "Service Location"
        tpLocation.UseVisualStyleBackColor = True
        ' 
        ' GBLabAssociation
        ' 
        GBLabAssociation.Controls.Add(Label36)
        GBLabAssociation.Controls.Add(btnAddLab)
        GBLabAssociation.Controls.Add(Label37)
        GBLabAssociation.Controls.Add(btnRemoveLabs)
        GBLabAssociation.Controls.Add(cmbLabs)
        GBLabAssociation.Controls.Add(btnRemoveLab)
        GBLabAssociation.Controls.Add(txtRefLabTestID)
        GBLabAssociation.Controls.Add(dgvLabs)
        GBLabAssociation.Controls.Add(btnRefLab)
        GBLabAssociation.Enabled = False
        GBLabAssociation.ForeColor = Color.DarkViolet
        GBLabAssociation.Location = New Point(5, 27)
        GBLabAssociation.Margin = New Padding(5, 6, 5, 6)
        GBLabAssociation.Name = "GBLabAssociation"
        GBLabAssociation.Padding = New Padding(5, 6, 5, 6)
        GBLabAssociation.Size = New Size(910, 356)
        GBLabAssociation.TabIndex = 10
        GBLabAssociation.TabStop = False
        GBLabAssociation.Text = "Reference Lab Association"
        ' 
        ' Label36
        ' 
        Label36.ForeColor = Color.MidnightBlue
        Label36.Location = New Point(5, 252)
        Label36.Margin = New Padding(5, 0, 5, 0)
        Label36.Name = "Label36"
        Label36.Size = New Size(325, 33)
        Label36.TabIndex = 5
        Label36.Text = "Reference Lab"
        ' 
        ' btnAddLab
        ' 
        btnAddLab.Enabled = False
        btnAddLab.ForeColor = Color.MidnightBlue
        btnAddLab.Location = New Point(758, 288)
        btnAddLab.Margin = New Padding(5, 6, 5, 6)
        btnAddLab.Name = "btnAddLab"
        btnAddLab.Size = New Size(137, 44)
        btnAddLab.TabIndex = 59
        btnAddLab.Text = "Add to List"
        btnAddLab.UseVisualStyleBackColor = True
        ' 
        ' Label37
        ' 
        Label37.ForeColor = Color.MidnightBlue
        Label37.Location = New Point(573, 262)
        Label37.Margin = New Padding(5, 0, 5, 0)
        Label37.Name = "Label37"
        Label37.Size = New Size(165, 25)
        Label37.TabIndex = 9
        Label37.Text = "Ref Lab Test ID"
        ' 
        ' btnRemoveLabs
        ' 
        btnRemoveLabs.Enabled = False
        btnRemoveLabs.ForeColor = Color.Red
        btnRemoveLabs.Location = New Point(758, 194)
        btnRemoveLabs.Margin = New Padding(5, 6, 5, 6)
        btnRemoveLabs.Name = "btnRemoveLabs"
        btnRemoveLabs.Size = New Size(137, 44)
        btnRemoveLabs.TabIndex = 60
        btnRemoveLabs.TabStop = False
        btnRemoveLabs.Text = "Remove All"
        btnRemoveLabs.UseVisualStyleBackColor = True
        ' 
        ' cmbLabs
        ' 
        cmbLabs.FormattingEnabled = True
        cmbLabs.Location = New Point(10, 292)
        cmbLabs.Margin = New Padding(5, 6, 5, 6)
        cmbLabs.Name = "cmbLabs"
        cmbLabs.Size = New Size(501, 33)
        cmbLabs.Sorted = True
        cmbLabs.TabIndex = 56
        ' 
        ' btnRemoveLab
        ' 
        btnRemoveLab.Enabled = False
        btnRemoveLab.ForeColor = Color.Red
        btnRemoveLab.Location = New Point(758, 40)
        btnRemoveLab.Margin = New Padding(5, 6, 5, 6)
        btnRemoveLab.Name = "btnRemoveLab"
        btnRemoveLab.Size = New Size(137, 44)
        btnRemoveLab.TabIndex = 61
        btnRemoveLab.TabStop = False
        btnRemoveLab.Text = "Remove"
        btnRemoveLab.UseVisualStyleBackColor = True
        ' 
        ' txtRefLabTestID
        ' 
        txtRefLabTestID.Location = New Point(578, 292)
        txtRefLabTestID.Margin = New Padding(5, 6, 5, 6)
        txtRefLabTestID.MaxLength = 12
        txtRefLabTestID.Name = "txtRefLabTestID"
        txtRefLabTestID.Size = New Size(162, 31)
        txtRefLabTestID.TabIndex = 58
        ' 
        ' dgvLabs
        ' 
        dgvLabs.AllowUserToAddRows = False
        dgvLabs.AllowUserToDeleteRows = False
        dgvLabs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvLabs.Columns.AddRange(New DataGridViewColumn() {TestID, RefLab, IsDefault, RefLabTestID})
        dgvLabs.Location = New Point(10, 40)
        dgvLabs.Margin = New Padding(5, 6, 5, 6)
        dgvLabs.Name = "dgvLabs"
        dgvLabs.RowHeadersVisible = False
        dgvLabs.RowHeadersWidth = 51
        dgvLabs.Size = New Size(733, 198)
        dgvLabs.TabIndex = 55
        dgvLabs.TabStop = False
        ' 
        ' TestID
        ' 
        TestID.FillWeight = 120.0F
        TestID.HeaderText = "Test ID"
        TestID.MinimumWidth = 6
        TestID.Name = "TestID"
        TestID.ReadOnly = True
        TestID.Visible = False
        TestID.Width = 120
        ' 
        ' RefLab
        ' 
        RefLab.FillWeight = 225.0F
        RefLab.HeaderText = "Reference Lab"
        RefLab.MinimumWidth = 6
        RefLab.Name = "RefLab"
        RefLab.ReadOnly = True
        RefLab.Width = 225
        ' 
        ' IsDefault
        ' 
        IsDefault.FillWeight = 60.0F
        IsDefault.HeaderText = "Default?"
        IsDefault.MinimumWidth = 6
        IsDefault.Name = "IsDefault"
        IsDefault.Resizable = DataGridViewTriState.True
        IsDefault.SortMode = DataGridViewColumnSortMode.Automatic
        IsDefault.Width = 60
        ' 
        ' RefLabTestID
        ' 
        RefLabTestID.FillWeight = 150.0F
        RefLabTestID.HeaderText = "Ref Lab Test ID"
        RefLabTestID.MinimumWidth = 6
        RefLabTestID.Name = "RefLabTestID"
        RefLabTestID.ReadOnly = True
        RefLabTestID.Width = 150
        ' 
        ' btnRefLab
        ' 
        btnRefLab.Image = CType(resources.GetObject("btnRefLab.Image"), Image)
        btnRefLab.Location = New Point(523, 288)
        btnRefLab.Margin = New Padding(5, 6, 5, 6)
        btnRefLab.Name = "btnRefLab"
        btnRefLab.Size = New Size(45, 44)
        btnRefLab.TabIndex = 57
        btnRefLab.TabStop = False
        btnRefLab.UseVisualStyleBackColor = True
        ' 
        ' Label10
        ' 
        Label10.ForeColor = Color.DarkViolet
        Label10.Location = New Point(995, 237)
        Label10.Margin = New Padding(5, 0, 5, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(110, 25)
        Label10.TabIndex = 124
        Label10.Text = "C-Billable"
        Label10.TextAlign = ContentAlignment.BottomCenter
        ' 
        ' chkCBillable
        ' 
        chkCBillable.Appearance = Appearance.Button
        chkCBillable.CheckAlign = ContentAlignment.MiddleRight
        chkCBillable.Checked = True
        chkCBillable.CheckState = CheckState.Checked
        chkCBillable.ForeColor = Color.DarkBlue
        chkCBillable.Location = New Point(993, 267)
        chkCBillable.Margin = New Padding(5, 6, 5, 6)
        chkCBillable.Name = "chkCBillable"
        chkCBillable.Size = New Size(108, 48)
        chkCBillable.TabIndex = 12
        chkCBillable.Text = "YES"
        chkCBillable.TextAlign = ContentAlignment.MiddleCenter
        chkCBillable.TextImageRelation = TextImageRelation.ImageAboveText
        ToolTip1.SetToolTip(chkCBillable, "Click to switch the status of the Group between billable in Client Billing, or Not")
        chkCBillable.UseVisualStyleBackColor = True
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkViolet
        Label4.Location = New Point(875, 237)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(110, 25)
        Label4.TabIndex = 123
        Label4.Text = "P-Billable"
        Label4.TextAlign = ContentAlignment.BottomCenter
        ' 
        ' chkPBillable
        ' 
        chkPBillable.Appearance = Appearance.Button
        chkPBillable.CheckAlign = ContentAlignment.MiddleRight
        chkPBillable.Checked = True
        chkPBillable.CheckState = CheckState.Checked
        chkPBillable.ForeColor = Color.DarkBlue
        chkPBillable.Location = New Point(875, 267)
        chkPBillable.Margin = New Padding(5, 6, 5, 6)
        chkPBillable.Name = "chkPBillable"
        chkPBillable.Size = New Size(108, 48)
        chkPBillable.TabIndex = 11
        chkPBillable.Text = "YES"
        chkPBillable.TextAlign = ContentAlignment.MiddleCenter
        chkPBillable.TextImageRelation = TextImageRelation.ImageAboveText
        ToolTip1.SetToolTip(chkPBillable, "Click to switch the status of the Group between billable in Patient Billing, or Not")
        chkPBillable.UseVisualStyleBackColor = True
        ' 
        ' Label14
        ' 
        Label14.ForeColor = Color.DarkViolet
        Label14.Location = New Point(883, 148)
        Label14.Margin = New Padding(5, 0, 5, 0)
        Label14.Name = "Label14"
        Label14.Size = New Size(90, 25)
        Label14.TabIndex = 122
        Label14.Text = "In House"
        Label14.TextAlign = ContentAlignment.BottomCenter
        ' 
        ' ChkInHouse
        ' 
        ChkInHouse.Appearance = Appearance.Button
        ChkInHouse.CheckAlign = ContentAlignment.MiddleRight
        ChkInHouse.Checked = True
        ChkInHouse.CheckState = CheckState.Checked
        ChkInHouse.ForeColor = Color.DarkBlue
        ChkInHouse.Location = New Point(875, 177)
        ChkInHouse.Margin = New Padding(5, 6, 5, 6)
        ChkInHouse.Name = "ChkInHouse"
        ChkInHouse.Size = New Size(107, 48)
        ChkInHouse.TabIndex = 9
        ChkInHouse.Text = "YES"
        ChkInHouse.TextAlign = ContentAlignment.MiddleCenter
        ChkInHouse.TextImageRelation = TextImageRelation.ImageAboveText
        ChkInHouse.UseVisualStyleBackColor = True
        ' 
        ' Label13
        ' 
        Label13.ForeColor = Color.DarkViolet
        Label13.Location = New Point(993, 148)
        Label13.Margin = New Padding(5, 0, 5, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(110, 25)
        Label13.TabIndex = 121
        Label13.Text = "T-Billable"
        Label13.TextAlign = ContentAlignment.BottomCenter
        ' 
        ' ChkTBillable
        ' 
        ChkTBillable.Appearance = Appearance.Button
        ChkTBillable.CheckAlign = ContentAlignment.MiddleRight
        ChkTBillable.Checked = True
        ChkTBillable.CheckState = CheckState.Checked
        ChkTBillable.ForeColor = Color.DarkBlue
        ChkTBillable.Location = New Point(993, 177)
        ChkTBillable.Margin = New Padding(5, 6, 5, 6)
        ChkTBillable.Name = "ChkTBillable"
        ChkTBillable.Size = New Size(108, 48)
        ChkTBillable.TabIndex = 10
        ChkTBillable.Text = "YES"
        ChkTBillable.TextAlign = ContentAlignment.MiddleCenter
        ChkTBillable.TextImageRelation = TextImageRelation.ImageAboveText
        ToolTip1.SetToolTip(ChkTBillable, "Click to switch the status of the Group between billable in Third Party Billing, or Not")
        ChkTBillable.UseVisualStyleBackColor = True
        ' 
        ' txtLoinc
        ' 
        txtLoinc.Location = New Point(688, 152)
        txtLoinc.Margin = New Padding(5, 6, 5, 6)
        txtLoinc.MaxLength = 20
        txtLoinc.Name = "txtLoinc"
        txtLoinc.Size = New Size(146, 31)
        txtLoinc.TabIndex = 5
        ' 
        ' Label12
        ' 
        Label12.ForeColor = Color.DarkBlue
        Label12.Location = New Point(542, 152)
        Label12.Margin = New Padding(5, 0, 5, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(118, 35)
        Label12.TabIndex = 126
        Label12.Text = "Loinc Code"
        Label12.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' txtSearchID
        ' 
        txtSearchID.Location = New Point(15, 94)
        txtSearchID.Margin = New Padding(5, 6, 5, 6)
        txtSearchID.MaxLength = 12
        txtSearchID.Name = "txtSearchID"
        txtSearchID.Size = New Size(129, 31)
        txtSearchID.TabIndex = 1
        txtSearchID.TextAlign = HorizontalAlignment.Center
        ' 
        ' btnNote
        ' 
        btnNote.Image = CType(resources.GetObject("btnNote.Image"), Image)
        btnNote.Location = New Point(210, 142)
        btnNote.Margin = New Padding(5, 6, 5, 6)
        btnNote.Name = "btnNote"
        btnNote.Size = New Size(153, 44)
        btnNote.TabIndex = 128
        btnNote.TabStop = False
        btnNote.Text = "Result Note"
        btnNote.TextAlign = ContentAlignment.MiddleRight
        btnNote.TextImageRelation = TextImageRelation.ImageBeforeText
        btnNote.UseVisualStyleBackColor = True
        ' 
        ' txtResultNote
        ' 
        txtResultNote.Location = New Point(382, 138)
        txtResultNote.Margin = New Padding(5, 6, 5, 6)
        txtResultNote.MaxLength = 10000
        txtResultNote.Name = "txtResultNote"
        txtResultNote.Size = New Size(56, 31)
        txtResultNote.TabIndex = 129
        txtResultNote.Visible = False
        ' 
        ' frmGroups
        ' 
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1125, 969)
        Controls.Add(txtResultNote)
        Controls.Add(btnNote)
        Controls.Add(txtSearchID)
        Controls.Add(Label12)
        Controls.Add(txtLoinc)
        Controls.Add(Label10)
        Controls.Add(chkCBillable)
        Controls.Add(Label4)
        Controls.Add(chkPBillable)
        Controls.Add(Label14)
        Controls.Add(ChkInHouse)
        Controls.Add(Label13)
        Controls.Add(ChkTBillable)
        Controls.Add(TabControl1)
        Controls.Add(btnGroupLook)
        Controls.Add(Label11)
        Controls.Add(ChkMarkable)
        Controls.Add(Label8)
        Controls.Add(ChkActive)
        Controls.Add(txtDescription)
        Controls.Add(Label6)
        Controls.Add(txtAbbr)
        Controls.Add(Label3)
        Controls.Add(txtName)
        Controls.Add(Label2)
        Controls.Add(txtGroupID)
        Controls.Add(Label1)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmGroups"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Group Management"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        TabControl1.ResumeLayout(False)
        tpContents.ResumeLayout(False)
        tpContents.PerformLayout()
        CType(dgvTests, ComponentModel.ISupportInitialize).EndInit()
        tpMarking.ResumeLayout(False)
        grpMarking.ResumeLayout(False)
        grpMarking.PerformLayout()
        CType(dgvModifiers, ComponentModel.ISupportInitialize).EndInit()
        tpBilling.ResumeLayout(False)
        TabControl3.ResumeLayout(False)
        tpPricing.ResumeLayout(False)
        tpPricing.PerformLayout()
        tpNecessity.ResumeLayout(False)
        tpNecessity.PerformLayout()
        CType(dgvNecessity, ComponentModel.ISupportInitialize).EndInit()
        tpLocation.ResumeLayout(False)
        GBLabAssociation.ResumeLayout(False)
        GBLabAssociation.PerformLayout()
        CType(dgvLabs, ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents btnGroupLook As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents ChkMarkable As System.Windows.Forms.CheckBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ChkActive As System.Windows.Forms.CheckBox
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtAbbr As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtGroupID As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tpContents As System.Windows.Forms.TabPage
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtTestName As System.Windows.Forms.TextBox
    Friend WithEvents btnTestLook As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtTestID As System.Windows.Forms.TextBox
    Friend WithEvents dgvTests As System.Windows.Forms.DataGridView
    Friend WithEvents NR_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Logo As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents NRTo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnRemTst As System.Windows.Forms.Button
    Friend WithEvents btnRemTstAll As System.Windows.Forms.Button
    Friend WithEvents btnAddTest As System.Windows.Forms.Button
    Friend WithEvents tpMarking As System.Windows.Forms.TabPage
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnAddModifier As System.Windows.Forms.Button
    Friend WithEvents btnRemoveAllMod As System.Windows.Forms.Button
    Friend WithEvents txtAgeTo As System.Windows.Forms.TextBox
    Friend WithEvents txtAgeFrom As System.Windows.Forms.TextBox
    Friend WithEvents cmbSex As System.Windows.Forms.ComboBox
    Friend WithEvents btnRemoveMod As System.Windows.Forms.Button
    Friend WithEvents dgvModifiers As System.Windows.Forms.DataGridView
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents tpBilling As System.Windows.Forms.TabPage
    Friend WithEvents TabControl3 As System.Windows.Forms.TabControl
    Friend WithEvents tpPricing As System.Windows.Forms.TabPage
    Friend WithEvents tpNecessity As System.Windows.Forms.TabPage
    Friend WithEvents tpLocation As System.Windows.Forms.TabPage
    Friend WithEvents GBLabAssociation As System.Windows.Forms.GroupBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents btnAddLab As System.Windows.Forms.Button
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents btnRemoveLabs As System.Windows.Forms.Button
    Friend WithEvents cmbLabs As System.Windows.Forms.ComboBox
    Friend WithEvents btnRemoveLab As System.Windows.Forms.Button
    Friend WithEvents txtRefLabTestID As System.Windows.Forms.TextBox
    Friend WithEvents dgvLabs As System.Windows.Forms.DataGridView
    Friend WithEvents TestID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RefLab As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IsDefault As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents RefLabTestID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnRefLab As System.Windows.Forms.Button
    Friend WithEvents grpMarking As System.Windows.Forms.GroupBox
    Friend WithEvents Test_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Modifier As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Gender As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AgeFrom As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AgeTo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnICD9Lookup As System.Windows.Forms.Button
    Friend WithEvents btnAddNec As System.Windows.Forms.Button
    Friend WithEvents btnRemAllNec As System.Windows.Forms.Button
    Friend WithEvents txtICD9Description As System.Windows.Forms.TextBox
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents txtICD9Code As System.Windows.Forms.TextBox
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents btnRemNec As System.Windows.Forms.Button
    Friend WithEvents dgvNecessity As System.Windows.Forms.DataGridView
    Friend WithEvents btnNecCopy As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents chkCBillable As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkPBillable As System.Windows.Forms.CheckBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents ChkInHouse As System.Windows.Forms.CheckBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents ChkTBillable As System.Windows.Forms.CheckBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents txtLoinc As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtSearchID As System.Windows.Forms.TextBox
    Friend WithEvents txtPOS As System.Windows.Forms.TextBox
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents txtMod1 As System.Windows.Forms.TextBox
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents txtMod2 As System.Windows.Forms.TextBox
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents txtMod4 As System.Windows.Forms.TextBox
    Friend WithEvents txtMod3 As System.Windows.Forms.TextBox
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents txtCPTSPC As System.Windows.Forms.TextBox
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents txtCPTMCD As System.Windows.Forms.TextBox
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents txtCPTMCR As System.Windows.Forms.TextBox
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents txtBillUnit As System.Windows.Forms.TextBox
    Friend WithEvents txtCPTCode As System.Windows.Forms.TextBox
    Friend WithEvents txtPrice9 As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents btnCPTLook As System.Windows.Forms.Button
    Friend WithEvents txtPrice8 As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtPrice7 As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtPrice6 As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtPrice4 As System.Windows.Forms.TextBox
    Friend WithEvents txtPrice5 As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents txtListPrice As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtPrice1 As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtPrice3 As System.Windows.Forms.TextBox
    Friend WithEvents txtPrice2 As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents NecTest_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NecNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ICD9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ICD9Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkBreakOROrder As System.Windows.Forms.CheckBox
    Friend WithEvents btnReplicate As ToolStripButton
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents btnNote As Button
    Friend WithEvents txtResultNote As TextBox
End Class
