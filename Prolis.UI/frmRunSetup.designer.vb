<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRunSetup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRunSetup))
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
        btnCancel = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        Label1 = New Label()
        txtAnaID = New TextBox()
        btnAnaLookup = New Button()
        txtanaName = New TextBox()
        Label2 = New Label()
        cmbDepts = New ComboBox()
        cmbEquipments = New ComboBox()
        btnEquipEdit = New Button()
        btnDeptEdit = New Button()
        Label4 = New Label()
        Label5 = New Label()
        dgvEquips = New DataGridView()
        ProlisID = New DataGridViewTextBoxColumn()
        Ord_ID = New DataGridViewTextBoxColumn()
        Res_ID = New DataGridViewTextBoxColumn()
        TestName = New DataGridViewTextBoxColumn()
        txtTestID = New TextBox()
        Label8 = New Label()
        btnTestLook = New Button()
        txtTestName = New TextBox()
        Label9 = New Label()
        btnAddTst = New Button()
        btnRemTst = New Button()
        btnRemTstAll = New Button()
        txtOrdID = New TextBox()
        Label10 = New Label()
        TabControl1 = New TabControl()
        tpTests = New TabPage()
        Label12 = New Label()
        txtResID = New TextBox()
        tpControls = New TabPage()
        cmbInRange = New ComboBox()
        Label11 = New Label()
        Label7 = New Label()
        txtValidaters = New TextBox()
        dtpExpire = New DateTimePicker()
        dgvControls = New DataGridView()
        CntID = New DataGridViewTextBoxColumn()
        Control = New DataGridViewTextBoxColumn()
        Lot = New DataGridViewTextBoxColumn()
        Expires = New DataGridViewTextBoxColumn()
        RackPos = New DataGridViewTextBoxColumn()
        Label3 = New Label()
        txtControls = New TextBox()
        tpRanges = New TabPage()
        dgvRanges = New DataGridView()
        ControlID = New DataGridViewTextBoxColumn()
        CntName = New DataGridViewTextBoxColumn()
        tstID = New DataGridViewTextBoxColumn()
        tstname = New DataGridViewTextBoxColumn()
        Mean = New DataGridViewTextBoxColumn()
        Factor = New DataGridViewTextBoxColumn()
        Low = New DataGridViewTextBoxColumn()
        High = New DataGridViewTextBoxColumn()
        ToolStrip1.SuspendLayout()
        CType(dgvEquips, ComponentModel.ISupportInitialize).BeginInit()
        TabControl1.SuspendLayout()
        tpTests.SuspendLayout()
        tpControls.SuspendLayout()
        CType(dgvControls, ComponentModel.ISupportInitialize).BeginInit()
        tpRanges.SuspendLayout()
        CType(dgvRanges, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(20, 20)
        ToolStrip1.Items.AddRange(New ToolStripItem() {chkEditNew, ToolStripButton2, btnSave, ToolStripSeparator1, btnDelete, ToolStripSeparator2, btnCancel, ToolStripSeparator3, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(1054, 34)
        ToolStrip1.TabIndex = 7
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
        btnHelp.AutoSize = False
        btnHelp.ForeColor = Color.DarkBlue
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.ImageTransparentColor = Color.Magenta
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(80, 22)
        btnHelp.Text = "Help"
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(20, 64)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(116, 27)
        Label1.TabIndex = 8
        Label1.Text = "Analysis ID"
        ' 
        ' txtAnaID
        ' 
        txtAnaID.Location = New Point(20, 97)
        txtAnaID.Margin = New Padding(5, 6, 5, 6)
        txtAnaID.MaxLength = 5
        txtAnaID.Name = "txtAnaID"
        txtAnaID.Size = New Size(144, 31)
        txtAnaID.TabIndex = 9
        txtAnaID.TextAlign = HorizontalAlignment.Center
        ' 
        ' btnAnaLookup
        ' 
        btnAnaLookup.Image = CType(resources.GetObject("btnAnaLookup.Image"), Image)
        btnAnaLookup.Location = New Point(176, 91)
        btnAnaLookup.Margin = New Padding(5, 6, 5, 6)
        btnAnaLookup.Name = "btnAnaLookup"
        btnAnaLookup.Size = New Size(45, 48)
        btnAnaLookup.TabIndex = 10
        btnAnaLookup.UseVisualStyleBackColor = True
        ' 
        ' txtanaName
        ' 
        txtanaName.Location = New Point(231, 97)
        txtanaName.Margin = New Padding(5, 6, 5, 6)
        txtanaName.MaxLength = 60
        txtanaName.Name = "txtanaName"
        txtanaName.Size = New Size(793, 31)
        txtanaName.TabIndex = 12
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(244, 64)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(241, 27)
        Label2.TabIndex = 11
        Label2.Text = "Analysis Name"
        ' 
        ' cmbDepts
        ' 
        cmbDepts.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDepts.FormattingEnabled = True
        cmbDepts.Location = New Point(20, 177)
        cmbDepts.Margin = New Padding(5, 6, 5, 6)
        cmbDepts.Name = "cmbDepts"
        cmbDepts.Size = New Size(429, 33)
        cmbDepts.Sorted = True
        cmbDepts.TabIndex = 15
        ' 
        ' cmbEquipments
        ' 
        cmbEquipments.DropDownStyle = ComboBoxStyle.DropDownList
        cmbEquipments.FormattingEnabled = True
        cmbEquipments.Location = New Point(551, 177)
        cmbEquipments.Margin = New Padding(5, 6, 5, 6)
        cmbEquipments.Name = "cmbEquipments"
        cmbEquipments.Size = New Size(418, 33)
        cmbEquipments.Sorted = True
        cmbEquipments.TabIndex = 16
        ' 
        ' btnEquipEdit
        ' 
        btnEquipEdit.Image = CType(resources.GetObject("btnEquipEdit.Image"), Image)
        btnEquipEdit.Location = New Point(981, 172)
        btnEquipEdit.Margin = New Padding(5, 6, 5, 6)
        btnEquipEdit.Name = "btnEquipEdit"
        btnEquipEdit.Size = New Size(45, 48)
        btnEquipEdit.TabIndex = 17
        btnEquipEdit.UseVisualStyleBackColor = True
        ' 
        ' btnDeptEdit
        ' 
        btnDeptEdit.Image = CType(resources.GetObject("btnDeptEdit.Image"), Image)
        btnDeptEdit.Location = New Point(461, 172)
        btnDeptEdit.Margin = New Padding(5, 6, 5, 6)
        btnDeptEdit.Name = "btnDeptEdit"
        btnDeptEdit.Size = New Size(45, 48)
        btnDeptEdit.TabIndex = 18
        btnDeptEdit.UseVisualStyleBackColor = True
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(15, 144)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(116, 27)
        Label4.TabIndex = 19
        Label4.Text = "Department"
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.DarkBlue
        Label5.Location = New Point(561, 144)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(116, 27)
        Label5.TabIndex = 20
        Label5.Text = "Instrument"
        ' 
        ' dgvEquips
        ' 
        dgvEquips.AllowUserToAddRows = False
        dgvEquips.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(255), CByte(224), CByte(192))
        dgvEquips.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvEquips.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvEquips.Columns.AddRange(New DataGridViewColumn() {ProlisID, Ord_ID, Res_ID, TestName})
        dgvEquips.Location = New Point(15, 11)
        dgvEquips.Margin = New Padding(5, 6, 5, 6)
        dgvEquips.Name = "dgvEquips"
        dgvEquips.RowHeadersVisible = False
        dgvEquips.RowHeadersWidth = 62
        dgvEquips.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvEquips.Size = New Size(960, 425)
        dgvEquips.TabIndex = 23
        ' 
        ' ProlisID
        ' 
        ProlisID.FillWeight = 80F
        ProlisID.HeaderText = "Prolis ID"
        ProlisID.MinimumWidth = 8
        ProlisID.Name = "ProlisID"
        ProlisID.ReadOnly = True
        ProlisID.Width = 139
        ' 
        ' Ord_ID
        ' 
        Ord_ID.FillWeight = 110F
        Ord_ID.HeaderText = "Ord ID"
        Ord_ID.MaxInputLength = 100
        Ord_ID.MinimumWidth = 8
        Ord_ID.Name = "Ord_ID"
        Ord_ID.SortMode = DataGridViewColumnSortMode.NotSortable
        Ord_ID.Width = 192
        ' 
        ' Res_ID
        ' 
        Res_ID.FillWeight = 110F
        Res_ID.HeaderText = "Res ID"
        Res_ID.MaxInputLength = 100
        Res_ID.MinimumWidth = 8
        Res_ID.Name = "Res_ID"
        Res_ID.SortMode = DataGridViewColumnSortMode.NotSortable
        Res_ID.Width = 191
        ' 
        ' TestName
        ' 
        TestName.FillWeight = 250F
        TestName.HeaderText = "Analyte Name"
        TestName.MinimumWidth = 8
        TestName.Name = "TestName"
        TestName.ReadOnly = True
        TestName.Width = 435
        ' 
        ' txtTestID
        ' 
        txtTestID.Location = New Point(15, 494)
        txtTestID.Margin = New Padding(5, 6, 5, 6)
        txtTestID.MaxLength = 12
        txtTestID.Name = "txtTestID"
        txtTestID.Size = New Size(139, 31)
        txtTestID.TabIndex = 8
        txtTestID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label8
        ' 
        Label8.ForeColor = Color.DarkBlue
        Label8.Location = New Point(9, 464)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(116, 27)
        Label8.TabIndex = 25
        Label8.Text = "Analyte ID"
        ' 
        ' btnTestLook
        ' 
        btnTestLook.Image = CType(resources.GetObject("btnTestLook.Image"), Image)
        btnTestLook.Location = New Point(166, 489)
        btnTestLook.Margin = New Padding(5, 6, 5, 6)
        btnTestLook.Name = "btnTestLook"
        btnTestLook.Size = New Size(45, 48)
        btnTestLook.TabIndex = 9
        btnTestLook.UseVisualStyleBackColor = True
        ' 
        ' txtTestName
        ' 
        txtTestName.Location = New Point(221, 494)
        txtTestName.Margin = New Padding(5, 6, 5, 6)
        txtTestName.MaxLength = 60
        txtTestName.Name = "txtTestName"
        txtTestName.ReadOnly = True
        txtTestName.Size = New Size(624, 31)
        txtTestName.TabIndex = 10
        ' 
        ' Label9
        ' 
        Label9.ForeColor = Color.DarkBlue
        Label9.Location = New Point(230, 464)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(229, 27)
        Label9.TabIndex = 28
        Label9.Text = "Analyte Name"
        ' 
        ' btnAddTst
        ' 
        btnAddTst.Enabled = False
        btnAddTst.Image = CType(resources.GetObject("btnAddTst.Image"), Image)
        btnAddTst.Location = New Point(875, 564)
        btnAddTst.Margin = New Padding(5, 6, 5, 6)
        btnAddTst.Name = "btnAddTst"
        btnAddTst.Size = New Size(100, 69)
        btnAddTst.TabIndex = 13
        btnAddTst.UseVisualStyleBackColor = True
        ' 
        ' btnRemTst
        ' 
        btnRemTst.Enabled = False
        btnRemTst.Image = CType(resources.GetObject("btnRemTst.Image"), Image)
        btnRemTst.Location = New Point(875, 492)
        btnRemTst.Margin = New Padding(5, 6, 5, 6)
        btnRemTst.Name = "btnRemTst"
        btnRemTst.Size = New Size(45, 48)
        btnRemTst.TabIndex = 14
        btnRemTst.UseVisualStyleBackColor = True
        ' 
        ' btnRemTstAll
        ' 
        btnRemTstAll.Enabled = False
        btnRemTstAll.Image = CType(resources.GetObject("btnRemTstAll.Image"), Image)
        btnRemTstAll.Location = New Point(930, 492)
        btnRemTstAll.Margin = New Padding(5, 6, 5, 6)
        btnRemTstAll.Name = "btnRemTstAll"
        btnRemTstAll.Size = New Size(45, 48)
        btnRemTstAll.TabIndex = 15
        btnRemTstAll.UseVisualStyleBackColor = True
        ' 
        ' txtOrdID
        ' 
        txtOrdID.Location = New Point(15, 594)
        txtOrdID.Margin = New Padding(5, 6, 5, 6)
        txtOrdID.MaxLength = 100
        txtOrdID.Name = "txtOrdID"
        txtOrdID.Size = New Size(399, 31)
        txtOrdID.TabIndex = 11
        ' 
        ' Label10
        ' 
        Label10.ForeColor = Color.DarkBlue
        Label10.Location = New Point(19, 564)
        Label10.Margin = New Padding(5, 0, 5, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(126, 27)
        Label10.TabIndex = 33
        Label10.Text = "Order ID"
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(tpTests)
        TabControl1.Controls.Add(tpControls)
        TabControl1.Controls.Add(tpRanges)
        TabControl1.Location = New Point(20, 231)
        TabControl1.Margin = New Padding(5, 6, 5, 6)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(1014, 714)
        TabControl1.SizeMode = TabSizeMode.Fixed
        TabControl1.TabIndex = 35
        ' 
        ' tpTests
        ' 
        tpTests.Controls.Add(Label12)
        tpTests.Controls.Add(txtResID)
        tpTests.Controls.Add(dgvEquips)
        tpTests.Controls.Add(txtTestName)
        tpTests.Controls.Add(Label9)
        tpTests.Controls.Add(txtOrdID)
        tpTests.Controls.Add(Label10)
        tpTests.Controls.Add(btnRemTstAll)
        tpTests.Controls.Add(btnAddTst)
        tpTests.Controls.Add(btnRemTst)
        tpTests.Controls.Add(btnTestLook)
        tpTests.Controls.Add(txtTestID)
        tpTests.Controls.Add(Label8)
        tpTests.Location = New Point(4, 34)
        tpTests.Margin = New Padding(5, 6, 5, 6)
        tpTests.Name = "tpTests"
        tpTests.Padding = New Padding(5, 6, 5, 6)
        tpTests.Size = New Size(1006, 676)
        tpTests.TabIndex = 0
        tpTests.Text = "Analytes"
        tpTests.UseVisualStyleBackColor = True
        ' 
        ' Label12
        ' 
        Label12.ForeColor = Color.DarkBlue
        Label12.Location = New Point(460, 564)
        Label12.Margin = New Padding(5, 0, 5, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(126, 27)
        Label12.TabIndex = 34
        Label12.Text = "Result ID"
        ' 
        ' txtResID
        ' 
        txtResID.Location = New Point(446, 594)
        txtResID.Margin = New Padding(5, 6, 5, 6)
        txtResID.MaxLength = 100
        txtResID.Name = "txtResID"
        txtResID.Size = New Size(399, 31)
        txtResID.TabIndex = 12
        ' 
        ' tpControls
        ' 
        tpControls.Controls.Add(cmbInRange)
        tpControls.Controls.Add(Label11)
        tpControls.Controls.Add(Label7)
        tpControls.Controls.Add(txtValidaters)
        tpControls.Controls.Add(dtpExpire)
        tpControls.Controls.Add(dgvControls)
        tpControls.Controls.Add(Label3)
        tpControls.Controls.Add(txtControls)
        tpControls.Location = New Point(4, 34)
        tpControls.Margin = New Padding(5, 6, 5, 6)
        tpControls.Name = "tpControls"
        tpControls.Padding = New Padding(5, 6, 5, 6)
        tpControls.Size = New Size(1006, 676)
        tpControls.TabIndex = 1
        tpControls.Text = "Controls"
        tpControls.UseVisualStyleBackColor = True
        ' 
        ' cmbInRange
        ' 
        cmbInRange.DropDownStyle = ComboBoxStyle.DropDownList
        cmbInRange.Enabled = False
        cmbInRange.FormattingEnabled = True
        cmbInRange.Location = New Point(820, 47)
        cmbInRange.Margin = New Padding(5, 6, 5, 6)
        cmbInRange.Name = "cmbInRange"
        cmbInRange.Size = New Size(154, 33)
        cmbInRange.TabIndex = 31
        ' 
        ' Label11
        ' 
        Label11.ForeColor = Color.DarkBlue
        Label11.Location = New Point(815, 11)
        Label11.Margin = New Padding(5, 0, 5, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(161, 27)
        Label11.TabIndex = 30
        Label11.Text = "In Range %"
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.DarkBlue
        Label7.Location = New Point(400, 11)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(171, 27)
        Label7.TabIndex = 28
        Label7.Text = "Validating Levels"
        ' 
        ' txtValidaters
        ' 
        txtValidaters.Location = New Point(405, 47)
        txtValidaters.Margin = New Padding(5, 6, 5, 6)
        txtValidaters.MaxLength = 2
        txtValidaters.Name = "txtValidaters"
        txtValidaters.Size = New Size(114, 31)
        txtValidaters.TabIndex = 29
        txtValidaters.TextAlign = HorizontalAlignment.Center
        ' 
        ' dtpExpire
        ' 
        dtpExpire.Location = New Point(585, 67)
        dtpExpire.Margin = New Padding(5, 6, 5, 6)
        dtpExpire.Name = "dtpExpire"
        dtpExpire.Size = New Size(189, 31)
        dtpExpire.TabIndex = 27
        dtpExpire.Visible = False
        ' 
        ' dgvControls
        ' 
        dgvControls.AllowUserToAddRows = False
        dgvControls.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = Color.FromArgb(CByte(255), CByte(255), CByte(192))
        dgvControls.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        dgvControls.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvControls.Columns.AddRange(New DataGridViewColumn() {CntID, Control, Lot, Expires, RackPos})
        dgvControls.Location = New Point(29, 117)
        dgvControls.Margin = New Padding(5, 6, 5, 6)
        dgvControls.Name = "dgvControls"
        dgvControls.RowHeadersVisible = False
        dgvControls.RowHeadersWidth = 62
        dgvControls.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvControls.Size = New Size(949, 514)
        dgvControls.TabIndex = 25
        ' 
        ' CntID
        ' 
        CntID.FillWeight = 40F
        CntID.HeaderText = "ID"
        CntID.MaxInputLength = 2
        CntID.MinimumWidth = 8
        CntID.Name = "CntID"
        CntID.ReadOnly = True
        CntID.SortMode = DataGridViewColumnSortMode.NotSortable
        CntID.Width = 40
        ' 
        ' Control
        ' 
        Control.FillWeight = 130F
        Control.HeaderText = "Control Label"
        Control.MaxInputLength = 35
        Control.MinimumWidth = 8
        Control.Name = "Control"
        Control.SortMode = DataGridViewColumnSortMode.NotSortable
        Control.Width = 130
        ' 
        ' Lot
        ' 
        Lot.FillWeight = 120F
        Lot.HeaderText = "Control Lot"
        Lot.MaxInputLength = 35
        Lot.MinimumWidth = 8
        Lot.Name = "Lot"
        Lot.SortMode = DataGridViewColumnSortMode.NotSortable
        Lot.Width = 120
        ' 
        ' Expires
        ' 
        Expires.FillWeight = 160F
        Expires.HeaderText = "Expires"
        Expires.MinimumWidth = 8
        Expires.Name = "Expires"
        Expires.ReadOnly = True
        Expires.SortMode = DataGridViewColumnSortMode.NotSortable
        Expires.Width = 160
        ' 
        ' RackPos
        ' 
        RackPos.HeaderText = "Rack Position"
        RackPos.MinimumWidth = 8
        RackPos.Name = "RackPos"
        RackPos.Width = 150
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(24, 11)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(156, 27)
        Label3.TabIndex = 23
        Label3.Text = "Control Levels"
        ' 
        ' txtControls
        ' 
        txtControls.Location = New Point(29, 47)
        txtControls.Margin = New Padding(5, 6, 5, 6)
        txtControls.MaxLength = 2
        txtControls.Name = "txtControls"
        txtControls.Size = New Size(114, 31)
        txtControls.TabIndex = 24
        txtControls.TextAlign = HorizontalAlignment.Center
        ' 
        ' tpRanges
        ' 
        tpRanges.Controls.Add(dgvRanges)
        tpRanges.Location = New Point(4, 34)
        tpRanges.Margin = New Padding(5, 6, 5, 6)
        tpRanges.Name = "tpRanges"
        tpRanges.Padding = New Padding(5, 6, 5, 6)
        tpRanges.Size = New Size(1006, 676)
        tpRanges.TabIndex = 2
        tpRanges.Text = "Ranges"
        tpRanges.UseVisualStyleBackColor = True
        ' 
        ' dgvRanges
        ' 
        dgvRanges.AllowUserToAddRows = False
        dgvRanges.AllowUserToDeleteRows = False
        DataGridViewCellStyle3.BackColor = Color.FromArgb(CByte(255), CByte(224), CByte(192))
        dgvRanges.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        dgvRanges.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvRanges.Columns.AddRange(New DataGridViewColumn() {ControlID, CntName, tstID, tstname, Mean, Factor, Low, High})
        dgvRanges.Location = New Point(10, 11)
        dgvRanges.Margin = New Padding(5, 6, 5, 6)
        dgvRanges.Name = "dgvRanges"
        dgvRanges.RowHeadersVisible = False
        dgvRanges.RowHeadersWidth = 62
        dgvRanges.Size = New Size(935, 617)
        dgvRanges.TabIndex = 1
        ' 
        ' ControlID
        ' 
        ControlID.HeaderText = "Control"
        ControlID.MinimumWidth = 8
        ControlID.Name = "ControlID"
        ControlID.ReadOnly = True
        ControlID.Visible = False
        ControlID.Width = 150
        ' 
        ' CntName
        ' 
        CntName.FillWeight = 80F
        CntName.HeaderText = "Control"
        CntName.MinimumWidth = 8
        CntName.Name = "CntName"
        CntName.ReadOnly = True
        CntName.Width = 80
        ' 
        ' tstID
        ' 
        tstID.HeaderText = "tstID"
        tstID.MinimumWidth = 8
        tstID.Name = "tstID"
        tstID.ReadOnly = True
        tstID.Visible = False
        tstID.Width = 150
        ' 
        ' tstname
        ' 
        tstname.FillWeight = 120F
        tstname.HeaderText = "Test Name"
        tstname.MinimumWidth = 8
        tstname.Name = "tstname"
        tstname.ReadOnly = True
        tstname.Width = 120
        ' 
        ' Mean
        ' 
        Mean.HeaderText = "Mean Normal"
        Mean.MaxInputLength = 8
        Mean.MinimumWidth = 8
        Mean.Name = "Mean"
        Mean.SortMode = DataGridViewColumnSortMode.NotSortable
        Mean.Width = 150
        ' 
        ' Factor
        ' 
        Factor.HeaderText = "(+ -) Abnormal"
        Factor.MaxInputLength = 8
        Factor.MinimumWidth = 8
        Factor.Name = "Factor"
        Factor.Width = 150
        ' 
        ' Low
        ' 
        Low.FillWeight = 60F
        Low.HeaderText = "Low"
        Low.MaxInputLength = 8
        Low.MinimumWidth = 8
        Low.Name = "Low"
        Low.Width = 60
        ' 
        ' High
        ' 
        High.FillWeight = 60F
        High.HeaderText = "High"
        High.MaxInputLength = 8
        High.MinimumWidth = 8
        High.Name = "High"
        High.Width = 60
        ' 
        ' frmRunSetup
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1054, 967)
        Controls.Add(TabControl1)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(btnDeptEdit)
        Controls.Add(btnEquipEdit)
        Controls.Add(cmbEquipments)
        Controls.Add(cmbDepts)
        Controls.Add(txtanaName)
        Controls.Add(Label2)
        Controls.Add(btnAnaLookup)
        Controls.Add(txtAnaID)
        Controls.Add(Label1)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmRunSetup"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Analysis Setup"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgvEquips, ComponentModel.ISupportInitialize).EndInit()
        TabControl1.ResumeLayout(False)
        tpTests.ResumeLayout(False)
        tpTests.PerformLayout()
        tpControls.ResumeLayout(False)
        tpControls.PerformLayout()
        CType(dgvControls, ComponentModel.ISupportInitialize).EndInit()
        tpRanges.ResumeLayout(False)
        CType(dgvRanges, ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtAnaID As System.Windows.Forms.TextBox
    Friend WithEvents btnAnaLookup As System.Windows.Forms.Button
    Friend WithEvents txtanaName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbDepts As System.Windows.Forms.ComboBox
    Friend WithEvents cmbEquipments As System.Windows.Forms.ComboBox
    Friend WithEvents btnEquipEdit As System.Windows.Forms.Button
    Friend WithEvents btnDeptEdit As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dgvEquips As System.Windows.Forms.DataGridView
    Friend WithEvents txtTestID As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnTestLook As System.Windows.Forms.Button
    Friend WithEvents txtTestName As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnAddTst As System.Windows.Forms.Button
    Friend WithEvents btnRemTst As System.Windows.Forms.Button
    Friend WithEvents btnRemTstAll As System.Windows.Forms.Button
    Friend WithEvents txtOrdID As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tpTests As System.Windows.Forms.TabPage
    Friend WithEvents tpControls As System.Windows.Forms.TabPage
    Friend WithEvents tpRanges As System.Windows.Forms.TabPage
    Friend WithEvents dgvControls As System.Windows.Forms.DataGridView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtControls As System.Windows.Forms.TextBox
    Friend WithEvents dgvRanges As System.Windows.Forms.DataGridView
    Friend WithEvents ControlID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CntName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tstID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tstname As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Mean As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Factor As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Low As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents High As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dtpExpire As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtValidaters As System.Windows.Forms.TextBox
    Friend WithEvents cmbInRange As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtResID As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents ProlisID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Ord_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Res_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TestName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CntID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Control As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Lot As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Expires As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RackPos As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
