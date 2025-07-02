<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReportOrder
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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReportOrder))
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        dgvComponents = New DataGridView()
        Dept_ID = New DataGridViewTextBoxColumn()
        CompName = New DataGridViewTextBoxColumn()
        CompID = New DataGridViewTextBoxColumn()
        Label2 = New Label()
        btnFirst = New Button()
        btnUp = New Button()
        btnDown = New Button()
        btnLast = New Button()
        txtValue = New TextBox()
        cmbParameter = New ComboBox()
        btnFind = New Button()
        Label1 = New Label()
        Label3 = New Label()
        dgvDepartments = New DataGridView()
        DeptName = New DataGridViewTextBoxColumn()
        DeptID = New DataGridViewTextBoxColumn()
        Label4 = New Label()
        btnDFirst = New Button()
        btnDPrev = New Button()
        btnDNext = New Button()
        btnDLast = New Button()
        CType(dgvComponents, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvDepartments, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' dgvComponents
        ' 
        dgvComponents.AllowUserToAddRows = False
        dgvComponents.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.FloralWhite
        dgvComponents.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvComponents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvComponents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvComponents.Columns.AddRange(New DataGridViewColumn() {Dept_ID, CompName, CompID})
        dgvComponents.Location = New Point(23, 581)
        dgvComponents.Margin = New Padding(5, 6, 5, 6)
        dgvComponents.MultiSelect = False
        dgvComponents.Name = "dgvComponents"
        dgvComponents.RowHeadersVisible = False
        dgvComponents.RowHeadersWidth = 62
        dgvComponents.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvComponents.Size = New Size(1027, 458)
        dgvComponents.TabIndex = 1
        ' 
        ' Dept_ID
        ' 
        Dept_ID.HeaderText = "Dept ID"
        Dept_ID.MinimumWidth = 8
        Dept_ID.Name = "Dept_ID"
        Dept_ID.SortMode = DataGridViewColumnSortMode.NotSortable
        Dept_ID.Visible = False
        ' 
        ' CompName
        ' 
        CompName.FillWeight = 490F
        CompName.HeaderText = "Component Name"
        CompName.MinimumWidth = 8
        CompName.Name = "CompName"
        CompName.ReadOnly = True
        CompName.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' CompID
        ' 
        CompID.HeaderText = "Component ID"
        CompID.MinimumWidth = 8
        CompID.Name = "CompID"
        CompID.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Label2
        ' 
        Label2.Location = New Point(37, 17)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(248, 25)
        Label2.TabIndex = 3
        Label2.Text = "Departments"
        ' 
        ' btnFirst
        ' 
        btnFirst.Image = CType(resources.GetObject("btnFirst.Image"), Image)
        btnFirst.Location = New Point(1085, 581)
        btnFirst.Margin = New Padding(5, 6, 5, 6)
        btnFirst.Name = "btnFirst"
        btnFirst.Size = New Size(65, 65)
        btnFirst.TabIndex = 6
        btnFirst.UseVisualStyleBackColor = True
        ' 
        ' btnUp
        ' 
        btnUp.Image = CType(resources.GetObject("btnUp.Image"), Image)
        btnUp.Location = New Point(1085, 677)
        btnUp.Margin = New Padding(5, 6, 5, 6)
        btnUp.Name = "btnUp"
        btnUp.Size = New Size(65, 65)
        btnUp.TabIndex = 7
        btnUp.UseVisualStyleBackColor = True
        ' 
        ' btnDown
        ' 
        btnDown.Image = CType(resources.GetObject("btnDown.Image"), Image)
        btnDown.Location = New Point(1085, 879)
        btnDown.Margin = New Padding(5, 6, 5, 6)
        btnDown.Name = "btnDown"
        btnDown.Size = New Size(65, 65)
        btnDown.TabIndex = 8
        btnDown.UseVisualStyleBackColor = True
        ' 
        ' btnLast
        ' 
        btnLast.Image = CType(resources.GetObject("btnLast.Image"), Image)
        btnLast.Location = New Point(1085, 973)
        btnLast.Margin = New Padding(5, 6, 5, 6)
        btnLast.Name = "btnLast"
        btnLast.Size = New Size(65, 65)
        btnLast.TabIndex = 9
        btnLast.UseVisualStyleBackColor = True
        ' 
        ' txtValue
        ' 
        txtValue.Location = New Point(732, 506)
        txtValue.Margin = New Padding(5, 6, 5, 6)
        txtValue.Name = "txtValue"
        txtValue.Size = New Size(242, 31)
        txtValue.TabIndex = 12
        ' 
        ' cmbParameter
        ' 
        cmbParameter.DropDownStyle = ComboBoxStyle.DropDownList
        cmbParameter.FormattingEnabled = True
        cmbParameter.Items.AddRange(New Object() {"Component Name", "Component ID"})
        cmbParameter.Location = New Point(193, 504)
        cmbParameter.Margin = New Padding(5, 6, 5, 6)
        cmbParameter.Name = "cmbParameter"
        cmbParameter.Size = New Size(224, 33)
        cmbParameter.TabIndex = 13
        ' 
        ' btnFind
        ' 
        btnFind.Image = CType(resources.GetObject("btnFind.Image"), Image)
        btnFind.Location = New Point(987, 490)
        btnFind.Margin = New Padding(5, 6, 5, 6)
        btnFind.Name = "btnFind"
        btnFind.Size = New Size(65, 62)
        btnFind.TabIndex = 14
        btnFind.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(37, 510)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(142, 25)
        Label1.TabIndex = 15
        Label1.Text = "Select Parameter"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(448, 512)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(269, 25)
        Label3.TabIndex = 16
        Label3.Text = "Type in selected parameter value"
        Label3.TextAlign = ContentAlignment.TopRight
        ' 
        ' dgvDepartments
        ' 
        dgvDepartments.AllowUserToAddRows = False
        dgvDepartments.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = Color.Honeydew
        dgvDepartments.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        dgvDepartments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvDepartments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvDepartments.Columns.AddRange(New DataGridViewColumn() {DeptName, DeptID})
        dgvDepartments.Location = New Point(23, 48)
        dgvDepartments.Margin = New Padding(5, 6, 5, 6)
        dgvDepartments.MultiSelect = False
        dgvDepartments.Name = "dgvDepartments"
        dgvDepartments.RowHeadersVisible = False
        dgvDepartments.RowHeadersWidth = 62
        dgvDepartments.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvDepartments.Size = New Size(1027, 358)
        dgvDepartments.TabIndex = 17
        ' 
        ' DeptName
        ' 
        DeptName.FillWeight = 490F
        DeptName.HeaderText = "Department Name"
        DeptName.MinimumWidth = 8
        DeptName.Name = "DeptName"
        DeptName.ReadOnly = True
        DeptName.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' DeptID
        ' 
        DeptID.HeaderText = "Dept ID"
        DeptID.MinimumWidth = 8
        DeptID.Name = "DeptID"
        DeptID.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Label4
        ' 
        Label4.Location = New Point(37, 550)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(248, 25)
        Label4.TabIndex = 18
        Label4.Text = "Analytes"
        ' 
        ' btnDFirst
        ' 
        btnDFirst.Image = CType(resources.GetObject("btnDFirst.Image"), Image)
        btnDFirst.Location = New Point(1085, 48)
        btnDFirst.Margin = New Padding(5, 6, 5, 6)
        btnDFirst.Name = "btnDFirst"
        btnDFirst.Size = New Size(65, 67)
        btnDFirst.TabIndex = 19
        btnDFirst.UseVisualStyleBackColor = True
        ' 
        ' btnDPrev
        ' 
        btnDPrev.Image = CType(resources.GetObject("btnDPrev.Image"), Image)
        btnDPrev.Location = New Point(1085, 140)
        btnDPrev.Margin = New Padding(5, 6, 5, 6)
        btnDPrev.Name = "btnDPrev"
        btnDPrev.Size = New Size(65, 67)
        btnDPrev.TabIndex = 20
        btnDPrev.UseVisualStyleBackColor = True
        ' 
        ' btnDNext
        ' 
        btnDNext.Image = CType(resources.GetObject("btnDNext.Image"), Image)
        btnDNext.Location = New Point(1085, 260)
        btnDNext.Margin = New Padding(5, 6, 5, 6)
        btnDNext.Name = "btnDNext"
        btnDNext.Size = New Size(65, 67)
        btnDNext.TabIndex = 21
        btnDNext.UseVisualStyleBackColor = True
        ' 
        ' btnDLast
        ' 
        btnDLast.Image = CType(resources.GetObject("btnDLast.Image"), Image)
        btnDLast.Location = New Point(1085, 338)
        btnDLast.Margin = New Padding(5, 6, 5, 6)
        btnDLast.Name = "btnDLast"
        btnDLast.Size = New Size(65, 67)
        btnDLast.TabIndex = 22
        btnDLast.UseVisualStyleBackColor = True
        ' 
        ' frmReportOrder
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1148, 1010)
        Controls.Add(btnDLast)
        Controls.Add(btnDNext)
        Controls.Add(btnDPrev)
        Controls.Add(btnDFirst)
        Controls.Add(Label4)
        Controls.Add(dgvDepartments)
        Controls.Add(Label3)
        Controls.Add(Label1)
        Controls.Add(btnFind)
        Controls.Add(cmbParameter)
        Controls.Add(txtValue)
        Controls.Add(btnLast)
        Controls.Add(btnDown)
        Controls.Add(btnUp)
        Controls.Add(btnFirst)
        Controls.Add(Label2)
        Controls.Add(dgvComponents)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MaximumSize = New Size(1170, 1066)
        MinimizeBox = False
        MinimumSize = New Size(1170, 1066)
        Name = "frmReportOrder"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Report Order"
        CType(dgvComponents, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvDepartments, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents dgvComponents As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnFirst As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnLast As System.Windows.Forms.Button
    Friend WithEvents txtValue As System.Windows.Forms.TextBox
    Friend WithEvents cmbParameter As System.Windows.Forms.ComboBox
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dgvDepartments As System.Windows.Forms.DataGridView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnDFirst As System.Windows.Forms.Button
    Friend WithEvents btnDPrev As System.Windows.Forms.Button
    Friend WithEvents btnDNext As System.Windows.Forms.Button
    Friend WithEvents btnDLast As System.Windows.Forms.Button
    Friend WithEvents DeptName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DeptID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Dept_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CompName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CompID As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
