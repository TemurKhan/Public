<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOrderLookUp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOrderLookUp))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        btnAccept = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        GroupBox1 = New GroupBox()
        Label3 = New Label()
        chkNonProvider = New CheckBox()
        txtDate = New MaskedTextBox()
        txtPatient = New MaskedTextBox()
        Label2 = New Label()
        cmbProvider = New ComboBox()
        btnSearch = New Button()
        Label1 = New Label()
        dgvOrders = New DataGridView()
        OrderID = New DataGridViewTextBoxColumn()
        OrdDate = New DataGridViewTextBoxColumn()
        Inst = New DataGridViewTextBoxColumn()
        ExecInst = New DataGridViewTextBoxColumn()
        Provider = New DataGridViewTextBoxColumn()
        Patient = New DataGridViewTextBoxColumn()
        Sex = New DataGridViewTextBoxColumn()
        DOB = New DataGridViewTextBoxColumn()
        btn_Cancel = New Button()
        Button1 = New Button()
        ToolStrip1.SuspendLayout()
        GroupBox1.SuspendLayout()
        CType(dgvOrders, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(20, 20)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnAccept, ToolStripSeparator1, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(1035, 34)
        ToolStrip1.TabIndex = 3
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnAccept
        ' 
        btnAccept.Enabled = False
        btnAccept.Image = CType(resources.GetObject("btnAccept.Image"), Image)
        btnAccept.ImageTransparentColor = Color.Magenta
        btnAccept.Name = "btnAccept"
        btnAccept.Size = New Size(90, 29)
        btnAccept.Text = "Accept"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
        ' 
        ' btnCancel
        ' 
        btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), Image)
        btnCancel.ImageTransparentColor = Color.Magenta
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(87, 29)
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
        btnHelp.Size = New Size(73, 29)
        btnHelp.Text = "Help"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(chkNonProvider)
        GroupBox1.Controls.Add(txtDate)
        GroupBox1.Controls.Add(txtPatient)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(cmbProvider)
        GroupBox1.Controls.Add(btnSearch)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Location = New Point(20, 77)
        GroupBox1.Margin = New Padding(5, 6, 5, 6)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(5, 6, 5, 6)
        GroupBox1.Size = New Size(995, 186)
        GroupBox1.TabIndex = 1
        GroupBox1.TabStop = False
        GroupBox1.Text = "Search"
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(555, 136)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(164, 25)
        Label3.TabIndex = 8
        Label3.Text = "Order Date"
        Label3.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' chkNonProvider
        ' 
        chkNonProvider.CheckAlign = ContentAlignment.MiddleRight
        chkNonProvider.ForeColor = Color.DarkBlue
        chkNonProvider.Location = New Point(24, 131)
        chkNonProvider.Margin = New Padding(5, 6, 5, 6)
        chkNonProvider.Name = "chkNonProvider"
        chkNonProvider.Size = New Size(279, 36)
        chkNonProvider.TabIndex = 2
        chkNonProvider.Text = "Providerless Order(s)"
        chkNonProvider.UseVisualStyleBackColor = True
        ' 
        ' txtDate
        ' 
        txtDate.Location = New Point(744, 130)
        txtDate.Margin = New Padding(5, 6, 5, 6)
        txtDate.Mask = "00/00/0000"
        txtDate.Name = "txtDate"
        txtDate.Size = New Size(138, 31)
        txtDate.TabIndex = 3
        txtDate.ValidatingType = GetType(Date)
        ' 
        ' txtPatient
        ' 
        txtPatient.Location = New Point(24, 70)
        txtPatient.Margin = New Padding(5, 6, 5, 6)
        txtPatient.Name = "txtPatient"
        txtPatient.Size = New Size(385, 31)
        txtPatient.TabIndex = 1
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(439, 31)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(255, 33)
        Label2.TabIndex = 4
        Label2.Text = "Ordering Provider"
        ' 
        ' cmbProvider
        ' 
        cmbProvider.FormattingEnabled = True
        cmbProvider.Items.AddRange(New Object() {"Name (Last, First) even partial", "Social Security No"})
        cmbProvider.Location = New Point(439, 70)
        cmbProvider.Margin = New Padding(5, 6, 5, 6)
        cmbProvider.Name = "cmbProvider"
        cmbProvider.Size = New Size(524, 33)
        cmbProvider.Sorted = True
        cmbProvider.TabIndex = 0
        ' 
        ' btnSearch
        ' 
        btnSearch.Enabled = False
        btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), Image)
        btnSearch.Location = New Point(914, 120)
        btnSearch.Margin = New Padding(5, 6, 5, 6)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(51, 53)
        btnSearch.TabIndex = 3
        btnSearch.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.MidnightBlue
        Label1.Location = New Point(24, 31)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(335, 25)
        Label1.TabIndex = 0
        Label1.Text = "Patient Name (Last, First) even partial"
        ' 
        ' dgvOrders
        ' 
        dgvOrders.AllowUserToAddRows = False
        dgvOrders.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.LemonChiffon
        dgvOrders.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvOrders.Columns.AddRange(New DataGridViewColumn() {OrderID, OrdDate, Inst, ExecInst, Provider, Patient, Sex, DOB})
        dgvOrders.Location = New Point(20, 275)
        dgvOrders.Margin = New Padding(5, 6, 5, 6)
        dgvOrders.MultiSelect = False
        dgvOrders.Name = "dgvOrders"
        dgvOrders.ReadOnly = True
        dgvOrders.RowHeadersVisible = False
        dgvOrders.RowHeadersWidth = 62
        dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvOrders.Size = New Size(995, 484)
        dgvOrders.TabIndex = 2
        ' 
        ' OrderID
        ' 
        OrderID.FillWeight = 80F
        OrderID.HeaderText = "Order ID"
        OrderID.MaxInputLength = 12
        OrderID.MinimumWidth = 8
        OrderID.Name = "OrderID"
        OrderID.ReadOnly = True
        ' 
        ' OrdDate
        ' 
        OrdDate.FillWeight = 80F
        OrdDate.HeaderText = "Dated"
        OrdDate.MaxInputLength = 12
        OrdDate.MinimumWidth = 8
        OrdDate.Name = "OrdDate"
        OrdDate.ReadOnly = True
        ' 
        ' Inst
        ' 
        Inst.FillWeight = 40F
        Inst.HeaderText = "Inst"
        Inst.MinimumWidth = 8
        Inst.Name = "Inst"
        Inst.ReadOnly = True
        Inst.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' ExecInst
        ' 
        ExecInst.FillWeight = 40F
        ExecInst.HeaderText = "Exec"
        ExecInst.MaxInputLength = 3
        ExecInst.MinimumWidth = 8
        ExecInst.Name = "ExecInst"
        ExecInst.ReadOnly = True
        ExecInst.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Provider
        ' 
        Provider.FillWeight = 115F
        Provider.HeaderText = "Ordering Provider"
        Provider.MinimumWidth = 8
        Provider.Name = "Provider"
        Provider.ReadOnly = True
        ' 
        ' Patient
        ' 
        Patient.FillWeight = 120F
        Patient.HeaderText = "Patient Name(L, F)"
        Patient.MinimumWidth = 8
        Patient.Name = "Patient"
        Patient.ReadOnly = True
        ' 
        ' Sex
        ' 
        Sex.FillWeight = 50F
        Sex.HeaderText = "Gender"
        Sex.MinimumWidth = 8
        Sex.Name = "Sex"
        Sex.ReadOnly = True
        Sex.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' DOB
        ' 
        DOB.FillWeight = 60F
        DOB.HeaderText = "D.O.B."
        DOB.MinimumWidth = 8
        DOB.Name = "DOB"
        DOB.ReadOnly = True
        DOB.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' btn_Cancel
        ' 
        btn_Cancel.DialogResult = DialogResult.Cancel
        btn_Cancel.Location = New Point(852, 461)
        btn_Cancel.Margin = New Padding(4, 5, 4, 5)
        btn_Cancel.Name = "btn_Cancel"
        btn_Cancel.Size = New Size(116, 64)
        btn_Cancel.TabIndex = 29
        btn_Cancel.TabStop = False
        btn_Cancel.Text = "Cancel"
        btn_Cancel.UseVisualStyleBackColor = True
        ' 
        ' Button1
        ' 
        Button1.DialogResult = DialogResult.Cancel
        Button1.Location = New Point(852, 436)
        Button1.Margin = New Padding(4, 5, 4, 5)
        Button1.Name = "Button1"
        Button1.Size = New Size(116, 64)
        Button1.TabIndex = 30
        Button1.TabStop = False
        Button1.Text = "Cancel"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' frmOrderLookUp
        ' 
        AcceptButton = btnSearch
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = btn_Cancel
        ClientSize = New Size(1035, 802)
        Controls.Add(dgvOrders)
        Controls.Add(GroupBox1)
        Controls.Add(ToolStrip1)
        Controls.Add(btn_Cancel)
        Controls.Add(Button1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmOrderLookUp"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Order Look Up"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        CType(dgvOrders, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnAccept As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkNonProvider As System.Windows.Forms.CheckBox
    Friend WithEvents txtDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtPatient As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbProvider As System.Windows.Forms.ComboBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvOrders As System.Windows.Forms.DataGridView
    Friend WithEvents OrderID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrdDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Inst As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExecInst As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Provider As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Patient As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Sex As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DOB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button

End Class
