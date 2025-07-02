<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmResInq
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmResInq))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        chkAccHist = New ToolStripButton()
        ToolStripButton2 = New ToolStripSeparator()
        btnFax = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnEmail = New ToolStripButton()
        ToolStripButton1 = New ToolStripSeparator()
        btnPrint = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        grpSearch = New GroupBox()
        lblDOB = New Label()
        txtName = New TextBox()
        txtSex = New TextBox()
        txtPatientID = New TextBox()
        txtAddress = New MaskedTextBox()
        Label2 = New Label()
        pctSex = New PictureBox()
        txtDOB = New MaskedTextBox()
        btnPatErase = New Button()
        btnPatLook = New Button()
        Label1 = New Label()
        grpHist = New GroupBox()
        btnTestErase = New Button()
        cmbTest = New ComboBox()
        Label9 = New Label()
        Label3 = New Label()
        dgvAccResults = New DataGridView()
        TestID = New DataGridViewTextBoxColumn()
        TestName = New DataGridViewTextBoxColumn()
        Result = New DataGridViewTextBoxColumn()
        Flag = New DataGridViewTextBoxColumn()
        Range = New DataGridViewTextBoxColumn()
        UOM = New DataGridViewTextBoxColumn()
        ExtRes = New DataGridViewTextBoxColumn()
        Comment = New DataGridViewTextBoxColumn()
        TabControl1 = New TabControl()
        TPAcc = New TabPage()
        grpAcc = New GroupBox()
        chkAccReq = New CheckBox()
        lblAccDate = New Label()
        Label10 = New Label()
        txtSlideID = New TextBox()
        btnAccErase = New Button()
        cmbAccession = New ComboBox()
        txtAccDate = New MaskedTextBox()
        TPHist = New TabPage()
        GroupBox1 = New GroupBox()
        txtFax = New MaskedTextBox()
        txtProviderName = New TextBox()
        btnProvLook = New Button()
        txtProviderID = New TextBox()
        Label8 = New Label()
        chkReport = New CheckBox()
        txtEmail = New TextBox()
        chkEmail = New CheckBox()
        chkFax = New CheckBox()
        Label7 = New Label()
        dgvReports = New DataGridView()
        Provider = New DataGridViewTextBoxColumn()
        Rpt = New DataGridViewTextBoxColumn()
        RPTType = New DataGridViewTextBoxColumn()
        EntryTime = New DataGridViewTextBoxColumn()
        Src = New DataGridViewTextBoxColumn()
        Fax = New DataGridViewCheckBoxColumn()
        FaxNo = New DataGridViewTextBoxColumn()
        Critical = New DataGridViewCheckBoxColumn()
        Email = New DataGridViewCheckBoxColumn()
        EmailID = New DataGridViewTextBoxColumn()
        btnSchedule = New Button()
        btnRem = New Button()
        Timer1 = New Timer(components)
        Label11 = New Label()
        lblRPTStatus = New Label()
        txtPatProvider = New TextBox()
        dgvHistResults = New DataGridView()
        Accession = New DataGridViewTextBoxColumn()
        AccDate = New DataGridViewTextBoxColumn()
        HResult = New DataGridViewTextBoxColumn()
        HFlag = New DataGridViewTextBoxColumn()
        HRange = New DataGridViewTextBoxColumn()
        HUOM = New DataGridViewTextBoxColumn()
        HTResult = New DataGridViewImageColumn()
        HComment = New DataGridViewTextBoxColumn()
        txtAccID = New TextBox()
        Label4 = New Label()
        Label14 = New Label()
        Label15 = New Label()
        Label16 = New Label()
        txtMeds = New TextBox()
        ToolStrip1.SuspendLayout()
        grpSearch.SuspendLayout()
        CType(pctSex, ComponentModel.ISupportInitialize).BeginInit()
        grpHist.SuspendLayout()
        CType(dgvAccResults, ComponentModel.ISupportInitialize).BeginInit()
        TabControl1.SuspendLayout()
        TPAcc.SuspendLayout()
        grpAcc.SuspendLayout()
        TPHist.SuspendLayout()
        GroupBox1.SuspendLayout()
        CType(dgvReports, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvHistResults, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {chkAccHist, ToolStripButton2, btnFax, ToolStripSeparator4, btnEmail, ToolStripButton1, btnPrint, ToolStripSeparator2, btnCancel, ToolStripSeparator1, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(1428, 34)
        ToolStrip1.TabIndex = 4
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' chkAccHist
        ' 
        chkAccHist.CheckOnClick = True
        chkAccHist.ForeColor = Color.DarkBlue
        chkAccHist.Image = CType(resources.GetObject("chkAccHist.Image"), Image)
        chkAccHist.ImageTransparentColor = Color.Magenta
        chkAccHist.Name = "chkAccHist"
        chkAccHist.Size = New Size(138, 29)
        chkAccHist.Text = "Accessioned"
        ' 
        ' ToolStripButton2
        ' 
        ToolStripButton2.Name = "ToolStripButton2"
        ToolStripButton2.Size = New Size(6, 34)
        ' 
        ' btnFax
        ' 
        btnFax.Enabled = False
        btnFax.ForeColor = Color.DarkBlue
        btnFax.Image = CType(resources.GetObject("btnFax.Image"), Image)
        btnFax.ImageTransparentColor = Color.Magenta
        btnFax.Name = "btnFax"
        btnFax.Size = New Size(122, 29)
        btnFax.Text = "Fax Critical"
        ' 
        ' ToolStripSeparator4
        ' 
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New Size(6, 34)
        ' 
        ' btnEmail
        ' 
        btnEmail.Enabled = False
        btnEmail.ForeColor = Color.DarkBlue
        btnEmail.Image = CType(resources.GetObject("btnEmail.Image"), Image)
        btnEmail.ImageTransparentColor = Color.Magenta
        btnEmail.Name = "btnEmail"
        btnEmail.Size = New Size(82, 29)
        btnEmail.Text = "Email"
        ' 
        ' ToolStripButton1
        ' 
        ToolStripButton1.Name = "ToolStripButton1"
        ToolStripButton1.Size = New Size(6, 34)
        ' 
        ' btnPrint
        ' 
        btnPrint.ForeColor = Color.DarkBlue
        btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), Image)
        btnPrint.ImageTransparentColor = Color.Magenta
        btnPrint.Name = "btnPrint"
        btnPrint.Size = New Size(134, 29)
        btnPrint.Text = "Print Report"
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
        btnCancel.Size = New Size(91, 29)
        btnCancel.Text = "Cancel"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
        ' 
        ' btnHelp
        ' 
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.ImageTransparentColor = Color.Magenta
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(77, 29)
        btnHelp.Text = "Help"
        ' 
        ' grpSearch
        ' 
        grpSearch.Controls.Add(lblDOB)
        grpSearch.Controls.Add(txtName)
        grpSearch.Controls.Add(txtSex)
        grpSearch.Controls.Add(txtPatientID)
        grpSearch.Controls.Add(txtAddress)
        grpSearch.Controls.Add(Label2)
        grpSearch.Controls.Add(pctSex)
        grpSearch.Controls.Add(txtDOB)
        grpSearch.Controls.Add(btnPatErase)
        grpSearch.Controls.Add(btnPatLook)
        grpSearch.Controls.Add(Label1)
        grpSearch.Location = New Point(22, 75)
        grpSearch.Margin = New Padding(5, 6, 5, 6)
        grpSearch.Name = "grpSearch"
        grpSearch.Padding = New Padding(5, 6, 5, 6)
        grpSearch.Size = New Size(645, 212)
        grpSearch.TabIndex = 5
        grpSearch.TabStop = False
        grpSearch.Text = "Patient"
        ' 
        ' lblDOB
        ' 
        lblDOB.ForeColor = Color.DarkBlue
        lblDOB.Location = New Point(423, 23)
        lblDOB.Margin = New Padding(5, 0, 5, 0)
        lblDOB.Name = "lblDOB"
        lblDOB.Size = New Size(195, 27)
        lblDOB.TabIndex = 11
        lblDOB.Text = "DOB"
        ' 
        ' txtName
        ' 
        txtName.Location = New Point(77, 58)
        txtName.Margin = New Padding(5, 6, 5, 6)
        txtName.MaxLength = 20
        txtName.Name = "txtName"
        txtName.Size = New Size(271, 31)
        txtName.TabIndex = 1
        ' 
        ' txtSex
        ' 
        txtSex.Location = New Point(325, 106)
        txtSex.Margin = New Padding(5, 6, 5, 6)
        txtSex.MaxLength = 13
        txtSex.Name = "txtSex"
        txtSex.Size = New Size(22, 31)
        txtSex.TabIndex = 10
        txtSex.Visible = False
        ' 
        ' txtPatientID
        ' 
        txtPatientID.Location = New Point(415, 113)
        txtPatientID.Margin = New Padding(5, 6, 5, 6)
        txtPatientID.MaxLength = 13
        txtPatientID.Name = "txtPatientID"
        txtPatientID.Size = New Size(142, 31)
        txtPatientID.TabIndex = 9
        txtPatientID.Visible = False
        ' 
        ' txtAddress
        ' 
        txtAddress.BackColor = Color.White
        txtAddress.Location = New Point(15, 158)
        txtAddress.Margin = New Padding(5, 6, 5, 6)
        txtAddress.Name = "txtAddress"
        txtAddress.ReadOnly = True
        txtAddress.Size = New Size(601, 31)
        txtAddress.TabIndex = 7
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(32, 125)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(262, 27)
        Label2.TabIndex = 6
        Label2.Text = "Address"
        ' 
        ' pctSex
        ' 
        pctSex.ErrorImage = CType(resources.GetObject("pctSex.ErrorImage"), Image)
        pctSex.Image = CType(resources.GetObject("pctSex.Image"), Image)
        pctSex.Location = New Point(578, 52)
        pctSex.Margin = New Padding(5, 6, 5, 6)
        pctSex.Name = "pctSex"
        pctSex.Size = New Size(40, 44)
        pctSex.TabIndex = 5
        pctSex.TabStop = False
        ' 
        ' txtDOB
        ' 
        txtDOB.BackColor = Color.White
        txtDOB.Location = New Point(415, 58)
        txtDOB.Margin = New Padding(5, 6, 5, 6)
        txtDOB.Mask = "00/00/0000"
        txtDOB.Name = "txtDOB"
        txtDOB.ReadOnly = True
        txtDOB.Size = New Size(112, 31)
        txtDOB.TabIndex = 4
        txtDOB.ValidatingType = GetType(Date)
        ' 
        ' btnPatErase
        ' 
        btnPatErase.Image = CType(resources.GetObject("btnPatErase.Image"), Image)
        btnPatErase.Location = New Point(10, 50)
        btnPatErase.Margin = New Padding(5, 6, 5, 6)
        btnPatErase.Name = "btnPatErase"
        btnPatErase.Size = New Size(52, 50)
        btnPatErase.TabIndex = 3
        btnPatErase.UseVisualStyleBackColor = True
        btnPatErase.Visible = False
        ' 
        ' btnPatLook
        ' 
        btnPatLook.Image = CType(resources.GetObject("btnPatLook.Image"), Image)
        btnPatLook.Location = New Point(360, 50)
        btnPatLook.Margin = New Padding(5, 6, 5, 6)
        btnPatLook.Name = "btnPatLook"
        btnPatLook.Size = New Size(45, 50)
        btnPatLook.TabIndex = 2
        btnPatLook.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(72, 25)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(262, 27)
        Label1.TabIndex = 0
        Label1.Text = "Name (Last, First Middle)"
        ' 
        ' grpHist
        ' 
        grpHist.Controls.Add(btnTestErase)
        grpHist.Controls.Add(cmbTest)
        grpHist.Controls.Add(Label9)
        grpHist.Enabled = False
        grpHist.Location = New Point(8, 6)
        grpHist.Margin = New Padding(5, 6, 5, 6)
        grpHist.Name = "grpHist"
        grpHist.Padding = New Padding(5, 6, 5, 6)
        grpHist.Size = New Size(545, 108)
        grpHist.TabIndex = 9
        grpHist.TabStop = False
        ' 
        ' btnTestErase
        ' 
        btnTestErase.Image = CType(resources.GetObject("btnTestErase.Image"), Image)
        btnTestErase.Location = New Point(17, 42)
        btnTestErase.Margin = New Padding(5, 6, 5, 6)
        btnTestErase.Name = "btnTestErase"
        btnTestErase.Size = New Size(52, 50)
        btnTestErase.TabIndex = 9
        btnTestErase.UseVisualStyleBackColor = True
        btnTestErase.Visible = False
        ' 
        ' cmbTest
        ' 
        cmbTest.DropDownStyle = ComboBoxStyle.DropDownList
        cmbTest.FormattingEnabled = True
        cmbTest.Location = New Point(78, 50)
        cmbTest.Margin = New Padding(5, 6, 5, 6)
        cmbTest.Name = "cmbTest"
        cmbTest.Size = New Size(436, 33)
        cmbTest.Sorted = True
        cmbTest.TabIndex = 6
        ' 
        ' Label9
        ' 
        Label9.ForeColor = Color.DarkBlue
        Label9.Location = New Point(78, 17)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(137, 27)
        Label9.TabIndex = 0
        Label9.Text = "Analyte"
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(717, 260)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(502, 27)
        Label3.TabIndex = 6
        Label3.Text = "Provider Name and address"
        ' 
        ' dgvAccResults
        ' 
        dgvAccResults.AllowUserToAddRows = False
        dgvAccResults.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.LavenderBlush
        dgvAccResults.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvAccResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvAccResults.Columns.AddRange(New DataGridViewColumn() {TestID, TestName, Result, Flag, Range, UOM, ExtRes, Comment})
        dgvAccResults.Location = New Point(22, 425)
        dgvAccResults.Margin = New Padding(5, 6, 5, 6)
        dgvAccResults.Name = "dgvAccResults"
        dgvAccResults.ReadOnly = True
        dgvAccResults.RowHeadersVisible = False
        dgvAccResults.RowHeadersWidth = 62
        dgvAccResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvAccResults.Size = New Size(1380, 527)
        dgvAccResults.TabIndex = 8
        ' 
        ' TestID
        ' 
        TestID.HeaderText = "TestID"
        TestID.MinimumWidth = 8
        TestID.Name = "TestID"
        TestID.ReadOnly = True
        TestID.Visible = False
        TestID.Width = 150
        ' 
        ' TestName
        ' 
        TestName.FillWeight = 150F
        TestName.HeaderText = "Analyte Name"
        TestName.MinimumWidth = 8
        TestName.Name = "TestName"
        TestName.ReadOnly = True
        TestName.Width = 150
        ' 
        ' Result
        ' 
        Result.HeaderText = "Result"
        Result.MinimumWidth = 8
        Result.Name = "Result"
        Result.ReadOnly = True
        Result.Width = 150
        ' 
        ' Flag
        ' 
        Flag.FillWeight = 40F
        Flag.HeaderText = "Flag"
        Flag.MinimumWidth = 8
        Flag.Name = "Flag"
        Flag.ReadOnly = True
        Flag.Width = 40
        ' 
        ' Range
        ' 
        Range.HeaderText = "Normal Range"
        Range.MinimumWidth = 8
        Range.Name = "Range"
        Range.ReadOnly = True
        Range.Width = 150
        ' 
        ' UOM
        ' 
        UOM.HeaderText = "U O M"
        UOM.MinimumWidth = 8
        UOM.Name = "UOM"
        UOM.ReadOnly = True
        UOM.Width = 150
        ' 
        ' ExtRes
        ' 
        ExtRes.HeaderText = "Extended"
        ExtRes.MinimumWidth = 8
        ExtRes.Name = "ExtRes"
        ExtRes.ReadOnly = True
        ExtRes.Resizable = DataGridViewTriState.True
        ExtRes.SortMode = DataGridViewColumnSortMode.NotSortable
        ExtRes.Width = 150
        ' 
        ' Comment
        ' 
        Comment.FillWeight = 210F
        Comment.HeaderText = "Comment"
        Comment.MinimumWidth = 8
        Comment.Name = "Comment"
        Comment.ReadOnly = True
        Comment.Width = 210
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(TPAcc)
        TabControl1.Controls.Add(TPHist)
        TabControl1.Location = New Point(690, 81)
        TabControl1.Margin = New Padding(5, 6, 5, 6)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(718, 173)
        TabControl1.TabIndex = 10
        ' 
        ' TPAcc
        ' 
        TPAcc.Controls.Add(grpAcc)
        TPAcc.Location = New Point(4, 34)
        TPAcc.Margin = New Padding(5, 6, 5, 6)
        TPAcc.Name = "TPAcc"
        TPAcc.Padding = New Padding(5, 6, 5, 6)
        TPAcc.Size = New Size(710, 135)
        TPAcc.TabIndex = 0
        TPAcc.Text = "Accession"
        TPAcc.UseVisualStyleBackColor = True
        ' 
        ' grpAcc
        ' 
        grpAcc.Controls.Add(chkAccReq)
        grpAcc.Controls.Add(lblAccDate)
        grpAcc.Controls.Add(Label10)
        grpAcc.Controls.Add(txtSlideID)
        grpAcc.Controls.Add(btnAccErase)
        grpAcc.Controls.Add(cmbAccession)
        grpAcc.Controls.Add(txtAccDate)
        grpAcc.Location = New Point(10, 0)
        grpAcc.Margin = New Padding(5, 6, 5, 6)
        grpAcc.Name = "grpAcc"
        grpAcc.Padding = New Padding(5, 6, 5, 6)
        grpAcc.Size = New Size(685, 123)
        grpAcc.TabIndex = 7
        grpAcc.TabStop = False
        ' 
        ' chkAccReq
        ' 
        chkAccReq.Appearance = Appearance.Button
        chkAccReq.Location = New Point(103, 15)
        chkAccReq.Margin = New Padding(5, 6, 5, 6)
        chkAccReq.Name = "chkAccReq"
        chkAccReq.Size = New Size(180, 42)
        chkAccReq.TabIndex = 13
        chkAccReq.Text = "Acc ID"
        chkAccReq.TextAlign = ContentAlignment.MiddleCenter
        chkAccReq.UseVisualStyleBackColor = True
        ' 
        ' lblAccDate
        ' 
        lblAccDate.ForeColor = Color.DarkBlue
        lblAccDate.Location = New Point(482, 25)
        lblAccDate.Margin = New Padding(5, 0, 5, 0)
        lblAccDate.Name = "lblAccDate"
        lblAccDate.Size = New Size(137, 27)
        lblAccDate.TabIndex = 12
        lblAccDate.Text = "Acc Date"
        ' 
        ' Label10
        ' 
        Label10.ForeColor = Color.DarkBlue
        Label10.Location = New Point(325, 25)
        Label10.Margin = New Padding(5, 0, 5, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(123, 27)
        Label10.TabIndex = 11
        Label10.Text = "Source"
        ' 
        ' txtSlideID
        ' 
        txtSlideID.Location = New Point(330, 65)
        txtSlideID.Margin = New Padding(5, 6, 5, 6)
        txtSlideID.Name = "txtSlideID"
        txtSlideID.Size = New Size(134, 31)
        txtSlideID.TabIndex = 10
        ' 
        ' btnAccErase
        ' 
        btnAccErase.Image = CType(resources.GetObject("btnAccErase.Image"), Image)
        btnAccErase.Location = New Point(15, 58)
        btnAccErase.Margin = New Padding(5, 6, 5, 6)
        btnAccErase.Name = "btnAccErase"
        btnAccErase.Size = New Size(52, 50)
        btnAccErase.TabIndex = 9
        btnAccErase.UseVisualStyleBackColor = True
        btnAccErase.Visible = False
        ' 
        ' cmbAccession
        ' 
        cmbAccession.FormattingEnabled = True
        cmbAccession.Location = New Point(77, 65)
        cmbAccession.Margin = New Padding(5, 6, 5, 6)
        cmbAccession.Name = "cmbAccession"
        cmbAccession.Size = New Size(241, 33)
        cmbAccession.Sorted = True
        cmbAccession.TabIndex = 6
        ' 
        ' txtAccDate
        ' 
        txtAccDate.BackColor = Color.White
        txtAccDate.Location = New Point(477, 65)
        txtAccDate.Margin = New Padding(5, 6, 5, 6)
        txtAccDate.Mask = "00/00/0000"
        txtAccDate.Name = "txtAccDate"
        txtAccDate.ReadOnly = True
        txtAccDate.Size = New Size(139, 31)
        txtAccDate.TabIndex = 4
        txtAccDate.ValidatingType = GetType(Date)
        ' 
        ' TPHist
        ' 
        TPHist.Controls.Add(grpHist)
        TPHist.Location = New Point(4, 34)
        TPHist.Margin = New Padding(5, 6, 5, 6)
        TPHist.Name = "TPHist"
        TPHist.Padding = New Padding(5, 6, 5, 6)
        TPHist.Size = New Size(710, 135)
        TPHist.TabIndex = 1
        TPHist.Text = "History"
        TPHist.UseVisualStyleBackColor = True
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(txtFax)
        GroupBox1.Controls.Add(txtProviderName)
        GroupBox1.Controls.Add(btnProvLook)
        GroupBox1.Controls.Add(txtProviderID)
        GroupBox1.Controls.Add(Label8)
        GroupBox1.Controls.Add(chkReport)
        GroupBox1.Controls.Add(txtEmail)
        GroupBox1.Controls.Add(chkEmail)
        GroupBox1.Controls.Add(chkFax)
        GroupBox1.Controls.Add(Label7)
        GroupBox1.Location = New Point(20, 1019)
        GroupBox1.Margin = New Padding(5, 6, 5, 6)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(5, 6, 5, 6)
        GroupBox1.Size = New Size(595, 208)
        GroupBox1.TabIndex = 12
        GroupBox1.TabStop = False
        GroupBox1.Text = "RDM Scheduler"
        ' 
        ' txtFax
        ' 
        txtFax.Location = New Point(90, 144)
        txtFax.Margin = New Padding(5, 6, 5, 6)
        txtFax.Name = "txtFax"
        txtFax.Size = New Size(146, 31)
        txtFax.TabIndex = 4
        ' 
        ' txtProviderName
        ' 
        txtProviderName.Location = New Point(248, 38)
        txtProviderName.Margin = New Padding(5, 6, 5, 6)
        txtProviderName.Name = "txtProviderName"
        txtProviderName.ReadOnly = True
        txtProviderName.Size = New Size(331, 31)
        txtProviderName.TabIndex = 8
        ' 
        ' btnProvLook
        ' 
        btnProvLook.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnProvLook.Image = CType(resources.GetObject("btnProvLook.Image"), Image)
        btnProvLook.Location = New Point(193, 33)
        btnProvLook.Margin = New Padding(5, 6, 5, 6)
        btnProvLook.Name = "btnProvLook"
        btnProvLook.Size = New Size(45, 50)
        btnProvLook.TabIndex = 2
        btnProvLook.UseVisualStyleBackColor = True
        ' 
        ' txtProviderID
        ' 
        txtProviderID.Location = New Point(90, 40)
        txtProviderID.Margin = New Padding(5, 6, 5, 6)
        txtProviderID.Name = "txtProviderID"
        txtProviderID.Size = New Size(96, 31)
        txtProviderID.TabIndex = 1
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.ForeColor = Color.DarkBlue
        Label8.Location = New Point(15, 102)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(65, 25)
        Label8.TabIndex = 7
        Label8.Text = "Report"
        Label8.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' chkReport
        ' 
        chkReport.Appearance = Appearance.Button
        chkReport.Location = New Point(10, 135)
        chkReport.Margin = New Padding(5, 6, 5, 6)
        chkReport.Name = "chkReport"
        chkReport.Size = New Size(70, 54)
        chkReport.TabIndex = 6
        chkReport.Text = "ACC"
        chkReport.TextAlign = ContentAlignment.MiddleCenter
        chkReport.UseVisualStyleBackColor = True
        ' 
        ' txtEmail
        ' 
        txtEmail.Enabled = False
        txtEmail.Location = New Point(248, 144)
        txtEmail.Margin = New Padding(5, 6, 5, 6)
        txtEmail.MaxLength = 50
        txtEmail.Name = "txtEmail"
        txtEmail.ReadOnly = True
        txtEmail.Size = New Size(331, 31)
        txtEmail.TabIndex = 6
        ' 
        ' chkEmail
        ' 
        chkEmail.AutoSize = True
        chkEmail.Enabled = False
        chkEmail.Location = New Point(250, 100)
        chkEmail.Margin = New Padding(5, 6, 5, 6)
        chkEmail.Name = "chkEmail"
        chkEmail.Size = New Size(80, 29)
        chkEmail.TabIndex = 5
        chkEmail.Text = "Email"
        chkEmail.UseVisualStyleBackColor = True
        ' 
        ' chkFax
        ' 
        chkFax.AutoSize = True
        chkFax.Location = New Point(102, 100)
        chkFax.Margin = New Padding(5, 6, 5, 6)
        chkFax.Name = "chkFax"
        chkFax.Size = New Size(63, 29)
        chkFax.TabIndex = 3
        chkFax.Text = "Fax"
        chkFax.UseVisualStyleBackColor = True
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.ForeColor = Color.DarkBlue
        Label7.Location = New Point(0, 46)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(78, 25)
        Label7.TabIndex = 0
        Label7.Text = "Provider"
        Label7.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' dgvReports
        ' 
        dgvReports.AllowUserToAddRows = False
        dgvReports.AllowUserToDeleteRows = False
        dgvReports.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvReports.Columns.AddRange(New DataGridViewColumn() {Provider, Rpt, RPTType, EntryTime, Src, Fax, FaxNo, Critical, Email, EmailID})
        dgvReports.Location = New Point(690, 1000)
        dgvReports.Margin = New Padding(5, 6, 5, 6)
        dgvReports.Name = "dgvReports"
        dgvReports.ReadOnly = True
        dgvReports.RowHeadersVisible = False
        dgvReports.RowHeadersWidth = 62
        dgvReports.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvReports.Size = New Size(712, 227)
        dgvReports.TabIndex = 13
        ' 
        ' Provider
        ' 
        Provider.FillWeight = 40F
        Provider.HeaderText = "Provider"
        Provider.MaxInputLength = 12
        Provider.MinimumWidth = 8
        Provider.Name = "Provider"
        Provider.ReadOnly = True
        Provider.SortMode = DataGridViewColumnSortMode.NotSortable
        Provider.Width = 40
        ' 
        ' Rpt
        ' 
        Rpt.FillWeight = 30F
        Rpt.HeaderText = "BaseID"
        Rpt.MaxInputLength = 12
        Rpt.MinimumWidth = 8
        Rpt.Name = "Rpt"
        Rpt.ReadOnly = True
        Rpt.SortMode = DataGridViewColumnSortMode.NotSortable
        Rpt.ToolTipText = "Base ID"
        Rpt.Width = 30
        ' 
        ' RPTType
        ' 
        RPTType.FillWeight = 30F
        RPTType.HeaderText = "RPT"
        RPTType.MaxInputLength = 4
        RPTType.MinimumWidth = 8
        RPTType.Name = "RPTType"
        RPTType.ReadOnly = True
        RPTType.SortMode = DataGridViewColumnSortMode.NotSortable
        RPTType.Width = 30
        ' 
        ' EntryTime
        ' 
        EntryTime.HeaderText = "EntryTime"
        EntryTime.MaxInputLength = 25
        EntryTime.MinimumWidth = 8
        EntryTime.Name = "EntryTime"
        EntryTime.ReadOnly = True
        EntryTime.SortMode = DataGridViewColumnSortMode.NotSortable
        EntryTime.Visible = False
        EntryTime.Width = 150
        ' 
        ' Src
        ' 
        Src.FillWeight = 40F
        Src.HeaderText = "Source"
        Src.MaxInputLength = 18
        Src.MinimumWidth = 8
        Src.Name = "Src"
        Src.ReadOnly = True
        Src.SortMode = DataGridViewColumnSortMode.NotSortable
        Src.Visible = False
        Src.Width = 40
        ' 
        ' Fax
        ' 
        Fax.FillWeight = 30F
        Fax.HeaderText = "Fax"
        Fax.MinimumWidth = 8
        Fax.Name = "Fax"
        Fax.ReadOnly = True
        Fax.Width = 30
        ' 
        ' FaxNo
        ' 
        FaxNo.FillWeight = 40F
        FaxNo.HeaderText = "No."
        FaxNo.MaxInputLength = 13
        FaxNo.MinimumWidth = 8
        FaxNo.Name = "FaxNo"
        FaxNo.ReadOnly = True
        FaxNo.SortMode = DataGridViewColumnSortMode.NotSortable
        FaxNo.Width = 40
        ' 
        ' Critical
        ' 
        Critical.FillWeight = 30F
        Critical.HeaderText = "Critical"
        Critical.MinimumWidth = 8
        Critical.Name = "Critical"
        Critical.ReadOnly = True
        Critical.Resizable = DataGridViewTriState.True
        Critical.SortMode = DataGridViewColumnSortMode.Automatic
        Critical.Width = 30
        ' 
        ' Email
        ' 
        Email.FillWeight = 30F
        Email.HeaderText = "Email"
        Email.MinimumWidth = 8
        Email.Name = "Email"
        Email.ReadOnly = True
        Email.Width = 30
        ' 
        ' EmailID
        ' 
        EmailID.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        EmailID.HeaderText = "Address"
        EmailID.MaxInputLength = 50
        EmailID.MinimumWidth = 8
        EmailID.Name = "EmailID"
        EmailID.ReadOnly = True
        EmailID.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' btnSchedule
        ' 
        btnSchedule.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnSchedule.Location = New Point(638, 1044)
        btnSchedule.Margin = New Padding(5, 6, 5, 6)
        btnSchedule.Name = "btnSchedule"
        btnSchedule.Size = New Size(42, 46)
        btnSchedule.TabIndex = 7
        btnSchedule.Text = ">"
        btnSchedule.UseVisualStyleBackColor = True
        ' 
        ' btnRem
        ' 
        btnRem.Enabled = False
        btnRem.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnRem.Location = New Point(638, 1154)
        btnRem.Margin = New Padding(5, 6, 5, 6)
        btnRem.Name = "btnRem"
        btnRem.Size = New Size(42, 46)
        btnRem.TabIndex = 8
        btnRem.Text = "<"
        btnRem.UseVisualStyleBackColor = True
        ' 
        ' Timer1
        ' 
        ' 
        ' Label11
        ' 
        Label11.ForeColor = Color.DarkBlue
        Label11.Location = New Point(20, 306)
        Label11.Margin = New Padding(5, 0, 5, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(128, 27)
        Label11.TabIndex = 18
        Label11.Text = "Report Status:"
        ' 
        ' lblRPTStatus
        ' 
        lblRPTStatus.Font = New Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblRPTStatus.ForeColor = Color.Red
        lblRPTStatus.Location = New Point(158, 300)
        lblRPTStatus.Margin = New Padding(5, 0, 5, 0)
        lblRPTStatus.Name = "lblRPTStatus"
        lblRPTStatus.Size = New Size(268, 38)
        lblRPTStatus.TabIndex = 19
        lblRPTStatus.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' txtPatProvider
        ' 
        txtPatProvider.Location = New Point(690, 300)
        txtPatProvider.Margin = New Padding(5, 6, 5, 6)
        txtPatProvider.Name = "txtPatProvider"
        txtPatProvider.ReadOnly = True
        txtPatProvider.Size = New Size(709, 31)
        txtPatProvider.TabIndex = 20
        ' 
        ' dgvHistResults
        ' 
        dgvHistResults.AllowUserToAddRows = False
        dgvHistResults.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = Color.Azure
        dgvHistResults.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        dgvHistResults.BackgroundColor = Color.AntiqueWhite
        dgvHistResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvHistResults.Columns.AddRange(New DataGridViewColumn() {Accession, AccDate, HResult, HFlag, HRange, HUOM, HTResult, HComment})
        dgvHistResults.Location = New Point(20, 425)
        dgvHistResults.Margin = New Padding(5, 6, 5, 6)
        dgvHistResults.Name = "dgvHistResults"
        dgvHistResults.ReadOnly = True
        dgvHistResults.RowHeadersVisible = False
        dgvHistResults.RowHeadersWidth = 62
        dgvHistResults.Size = New Size(1382, 527)
        dgvHistResults.TabIndex = 9
        dgvHistResults.Visible = False
        ' 
        ' Accession
        ' 
        Accession.FillWeight = 80F
        Accession.HeaderText = "Accession"
        Accession.MinimumWidth = 8
        Accession.Name = "Accession"
        Accession.ReadOnly = True
        Accession.Width = 138
        ' 
        ' AccDate
        ' 
        AccDate.FillWeight = 80F
        AccDate.HeaderText = "Date"
        AccDate.MinimumWidth = 8
        AccDate.Name = "AccDate"
        AccDate.ReadOnly = True
        AccDate.Width = 137
        ' 
        ' HResult
        ' 
        HResult.HeaderText = "Result"
        HResult.MinimumWidth = 8
        HResult.Name = "HResult"
        HResult.ReadOnly = True
        HResult.SortMode = DataGridViewColumnSortMode.NotSortable
        HResult.Width = 172
        ' 
        ' HFlag
        ' 
        HFlag.FillWeight = 40F
        HFlag.HeaderText = "Flag"
        HFlag.MinimumWidth = 8
        HFlag.Name = "HFlag"
        HFlag.ReadOnly = True
        HFlag.SortMode = DataGridViewColumnSortMode.NotSortable
        HFlag.Width = 69
        ' 
        ' HRange
        ' 
        HRange.FillWeight = 80F
        HRange.HeaderText = "Range"
        HRange.MinimumWidth = 8
        HRange.Name = "HRange"
        HRange.ReadOnly = True
        HRange.SortMode = DataGridViewColumnSortMode.NotSortable
        HRange.Width = 137
        ' 
        ' HUOM
        ' 
        HUOM.FillWeight = 80F
        HUOM.HeaderText = "UOM"
        HUOM.MinimumWidth = 8
        HUOM.Name = "HUOM"
        HUOM.ReadOnly = True
        HUOM.SortMode = DataGridViewColumnSortMode.NotSortable
        HUOM.Width = 138
        ' 
        ' HTResult
        ' 
        HTResult.HeaderText = "RTF"
        HTResult.Image = CType(resources.GetObject("HTResult.Image"), Image)
        HTResult.MinimumWidth = 8
        HTResult.Name = "HTResult"
        HTResult.ReadOnly = True
        HTResult.Resizable = DataGridViewTriState.True
        HTResult.Width = 172
        ' 
        ' HComment
        ' 
        HComment.FillWeight = 242F
        HComment.HeaderText = "Comment"
        HComment.MinimumWidth = 8
        HComment.Name = "HComment"
        HComment.ReadOnly = True
        HComment.Resizable = DataGridViewTriState.True
        HComment.SortMode = DataGridViewColumnSortMode.NotSortable
        HComment.Width = 416
        ' 
        ' txtAccID
        ' 
        txtAccID.Location = New Point(518, 304)
        txtAccID.Margin = New Padding(5, 6, 5, 6)
        txtAccID.Name = "txtAccID"
        txtAccID.ReadOnly = True
        txtAccID.Size = New Size(146, 31)
        txtAccID.TabIndex = 21
        ' 
        ' Label4
        ' 
        Label4.Location = New Point(437, 310)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(72, 31)
        Label4.TabIndex = 22
        Label4.Text = "AccID"
        Label4.TextAlign = ContentAlignment.TopRight
        ' 
        ' Label14
        ' 
        Label14.BackColor = Color.GreenYellow
        Label14.BorderStyle = BorderStyle.FixedSingle
        Label14.ForeColor = Color.Blue
        Label14.Location = New Point(110, 962)
        Label14.Margin = New Padding(5, 0, 5, 0)
        Label14.Name = "Label14"
        Label14.Size = New Size(130, 27)
        Label14.TabIndex = 78
        Label14.Text = "CORRECTED"
        Label14.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label15
        ' 
        Label15.BackColor = SystemColors.Control
        Label15.ForeColor = SystemColors.ControlText
        Label15.Location = New Point(20, 962)
        Label15.Margin = New Padding(5, 0, 5, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(103, 27)
        Label15.TabIndex = 77
        Label15.Text = "Legend ="
        ' 
        ' Label16
        ' 
        Label16.ForeColor = Color.DarkBlue
        Label16.Location = New Point(53, 360)
        Label16.Margin = New Padding(5, 0, 5, 0)
        Label16.Name = "Label16"
        Label16.Size = New Size(110, 25)
        Label16.TabIndex = 80
        Label16.Text = "Medications:"
        Label16.TextAlign = ContentAlignment.TopRight
        ' 
        ' txtMeds
        ' 
        txtMeds.Location = New Point(178, 354)
        txtMeds.Margin = New Padding(5, 6, 5, 6)
        txtMeds.Name = "txtMeds"
        txtMeds.ReadOnly = True
        txtMeds.Size = New Size(1221, 31)
        txtMeds.TabIndex = 79
        ' 
        ' frmResInq
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1428, 1256)
        Controls.Add(lblRPTStatus)
        Controls.Add(Label16)
        Controls.Add(txtMeds)
        Controls.Add(Label14)
        Controls.Add(Label15)
        Controls.Add(Label4)
        Controls.Add(txtAccID)
        Controls.Add(txtPatProvider)
        Controls.Add(Label11)
        Controls.Add(btnRem)
        Controls.Add(btnSchedule)
        Controls.Add(dgvReports)
        Controls.Add(GroupBox1)
        Controls.Add(TabControl1)
        Controls.Add(dgvHistResults)
        Controls.Add(Label3)
        Controls.Add(dgvAccResults)
        Controls.Add(grpSearch)
        Controls.Add(ToolStrip1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmResInq"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Result Inquiry"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        grpSearch.ResumeLayout(False)
        grpSearch.PerformLayout()
        CType(pctSex, ComponentModel.ISupportInitialize).EndInit()
        grpHist.ResumeLayout(False)
        CType(dgvAccResults, ComponentModel.ISupportInitialize).EndInit()
        TabControl1.ResumeLayout(False)
        TPAcc.ResumeLayout(False)
        grpAcc.ResumeLayout(False)
        grpAcc.PerformLayout()
        TPHist.ResumeLayout(False)
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        CType(dgvReports, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvHistResults, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents chkAccHist As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnFax As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnEmail As System.Windows.Forms.ToolStripButton
    Friend WithEvents grpSearch As System.Windows.Forms.GroupBox
    Friend WithEvents btnPatLook As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnPatErase As System.Windows.Forms.Button
    Friend WithEvents pctSex As System.Windows.Forms.PictureBox
    Friend WithEvents txtDOB As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtAddress As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dgvAccResults As System.Windows.Forms.DataGridView
    Friend WithEvents grpHist As System.Windows.Forms.GroupBox
    Friend WithEvents btnTestErase As System.Windows.Forms.Button
    Friend WithEvents cmbTest As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TPAcc As System.Windows.Forms.TabPage
    Friend WithEvents TPHist As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvReports As System.Windows.Forms.DataGridView
    Friend WithEvents chkFax As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents chkReport As System.Windows.Forms.CheckBox
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents chkEmail As System.Windows.Forms.CheckBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnSchedule As System.Windows.Forms.Button
    Friend WithEvents btnRem As System.Windows.Forms.Button
    Friend WithEvents txtPatientID As System.Windows.Forms.TextBox
    Friend WithEvents txtSex As System.Windows.Forms.TextBox
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents txtProviderID As System.Windows.Forms.TextBox
    Friend WithEvents txtProviderName As System.Windows.Forms.TextBox
    Friend WithEvents btnProvLook As System.Windows.Forms.Button
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents txtFax As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Provider As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Rpt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RPTType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EntryTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Src As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Fax As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents FaxNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Critical As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Email As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents EmailID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblRPTStatus As System.Windows.Forms.Label
    Friend WithEvents lblDOB As System.Windows.Forms.Label
    Friend WithEvents txtPatProvider As System.Windows.Forms.TextBox
    Friend WithEvents dgvHistResults As System.Windows.Forms.DataGridView
    Friend WithEvents btnAccErase As System.Windows.Forms.Button
    Friend WithEvents chkAccReq As System.Windows.Forms.CheckBox
    Friend WithEvents cmbAccession As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblAccDate As System.Windows.Forms.Label
    Friend WithEvents txtSlideID As System.Windows.Forms.TextBox
    Friend WithEvents txtAccDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents grpAcc As System.Windows.Forms.GroupBox
    Friend WithEvents txtAccID As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtMeds As System.Windows.Forms.TextBox
    Friend WithEvents TestID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TestName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Result As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Flag As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Range As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UOM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExtRes As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Comment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Accession As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AccDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HResult As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HFlag As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HRange As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HUOM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HTResult As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents HComment As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
