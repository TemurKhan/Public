<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBillReqs
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBillReqs))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
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
        dgvReqs = New DataGridView()
        ReqID = New DataGridViewTextBoxColumn()
        BillType = New DataGridViewTextBoxColumn()
        Cat = New DataGridViewTextBoxColumn()
        ReqName = New DataGridViewTextBoxColumn()
        ReqValue = New DataGridViewCheckBoxColumn()
        cmbBillType = New ComboBox()
        Label1 = New Label()
        txtName = New TextBox()
        chkRequired = New CheckBox()
        cmbCategory = New ComboBox()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        txtReqID = New TextBox()
        Label5 = New Label()
        ToolStrip1.SuspendLayout()
        CType(dgvReqs, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {chkEditNew, ToolStripSeparator1, btnSave, ToolStripSeparator2, btnDelete, ToolStripSeparator3, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(1048, 34)
        ToolStrip1.TabIndex = 1
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
        ' dgvReqs
        ' 
        dgvReqs.AllowUserToAddRows = False
        dgvReqs.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.Lavender
        dgvReqs.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvReqs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvReqs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvReqs.Columns.AddRange(New DataGridViewColumn() {ReqID, BillType, Cat, ReqName, ReqValue})
        dgvReqs.Location = New Point(20, 83)
        dgvReqs.Margin = New Padding(5, 6, 5, 6)
        dgvReqs.Name = "dgvReqs"
        dgvReqs.ReadOnly = True
        dgvReqs.RowHeadersVisible = False
        dgvReqs.RowHeadersWidth = 62
        dgvReqs.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvReqs.Size = New Size(1008, 579)
        dgvReqs.TabIndex = 2
        ' 
        ' ReqID
        ' 
        ReqID.FillWeight = 80F
        ReqID.HeaderText = "BR ID"
        ReqID.MinimumWidth = 8
        ReqID.Name = "ReqID"
        ReqID.ReadOnly = True
        ReqID.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' BillType
        ' 
        BillType.FillWeight = 90F
        BillType.HeaderText = "Billing Type"
        BillType.MinimumWidth = 8
        BillType.Name = "BillType"
        BillType.ReadOnly = True
        ' 
        ' Cat
        ' 
        Cat.FillWeight = 110F
        Cat.HeaderText = "Category"
        Cat.MinimumWidth = 8
        Cat.Name = "Cat"
        Cat.ReadOnly = True
        ' 
        ' ReqName
        ' 
        ReqName.FillWeight = 260F
        ReqName.HeaderText = "Name"
        ReqName.MinimumWidth = 8
        ReqName.Name = "ReqName"
        ReqName.ReadOnly = True
        ' 
        ' ReqValue
        ' 
        ReqValue.FillWeight = 62F
        ReqValue.HeaderText = "Required?"
        ReqValue.MinimumWidth = 8
        ReqValue.Name = "ReqValue"
        ReqValue.ReadOnly = True
        ' 
        ' cmbBillType
        ' 
        cmbBillType.DropDownStyle = ComboBoxStyle.DropDownList
        cmbBillType.FormattingEnabled = True
        cmbBillType.Items.AddRange(New Object() {"Client", "Third Party", "Patient"})
        cmbBillType.Location = New Point(143, 748)
        cmbBillType.Margin = New Padding(5, 6, 5, 6)
        cmbBillType.Name = "cmbBillType"
        cmbBillType.Size = New Size(182, 33)
        cmbBillType.TabIndex = 3
        ' 
        ' Label1
        ' 
        Label1.Location = New Point(162, 712)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(160, 31)
        Label1.TabIndex = 4
        Label1.Text = "Billing Type"
        ' 
        ' txtName
        ' 
        txtName.Location = New Point(552, 748)
        txtName.Margin = New Padding(5, 6, 5, 6)
        txtName.Name = "txtName"
        txtName.Size = New Size(374, 31)
        txtName.TabIndex = 5
        ' 
        ' chkRequired
        ' 
        chkRequired.Appearance = Appearance.Button
        chkRequired.Location = New Point(938, 740)
        chkRequired.Margin = New Padding(5, 6, 5, 6)
        chkRequired.Name = "chkRequired"
        chkRequired.Size = New Size(90, 54)
        chkRequired.TabIndex = 6
        chkRequired.Text = "No"
        chkRequired.TextAlign = ContentAlignment.MiddleCenter
        chkRequired.UseVisualStyleBackColor = True
        ' 
        ' cmbCategory
        ' 
        cmbCategory.FormattingEnabled = True
        cmbCategory.Items.AddRange(New Object() {"Payer", "Lab", "Provider", "Patient", "Coverage", "Charge", "Charge_Detail"})
        cmbCategory.Location = New Point(338, 748)
        cmbCategory.Margin = New Padding(5, 6, 5, 6)
        cmbCategory.Name = "cmbCategory"
        cmbCategory.Size = New Size(201, 33)
        cmbCategory.TabIndex = 7
        ' 
        ' Label2
        ' 
        Label2.Location = New Point(355, 712)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(162, 31)
        Label2.TabIndex = 8
        Label2.Text = "Category"
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(563, 712)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(178, 31)
        Label3.TabIndex = 9
        Label3.Text = "Requisit Name"
        ' 
        ' Label4
        ' 
        Label4.Location = New Point(933, 712)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(95, 31)
        Label4.TabIndex = 10
        Label4.Text = "Required?"
        ' 
        ' txtReqID
        ' 
        txtReqID.Location = New Point(20, 748)
        txtReqID.Margin = New Padding(5, 6, 5, 6)
        txtReqID.Name = "txtReqID"
        txtReqID.Size = New Size(111, 31)
        txtReqID.TabIndex = 11
        txtReqID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label5
        ' 
        Label5.Location = New Point(20, 712)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(113, 31)
        Label5.TabIndex = 12
        Label5.Text = "ID"
        Label5.TextAlign = ContentAlignment.TopCenter
        ' 
        ' frmBillReqs
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1048, 858)
        Controls.Add(Label5)
        Controls.Add(txtReqID)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(cmbCategory)
        Controls.Add(chkRequired)
        Controls.Add(txtName)
        Controls.Add(Label1)
        Controls.Add(cmbBillType)
        Controls.Add(dgvReqs)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmBillReqs"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Billing Requisits"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgvReqs, ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents dgvReqs As System.Windows.Forms.DataGridView
    Friend WithEvents cmbBillType As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents chkRequired As System.Windows.Forms.CheckBox
    Friend WithEvents cmbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtReqID As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ReqID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BillType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cat As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ReqName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ReqValue As System.Windows.Forms.DataGridViewCheckBoxColumn

End Class
