<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUserLook
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUserLook))
        txtTerm = New TextBox()
        Label1 = New Label()
        cmbTermType = New ComboBox()
        dgvUsers = New DataGridView()
        User_ID = New DataGridViewTextBoxColumn()
        UserName = New DataGridViewTextBoxColumn()
        FullName = New DataGridViewTextBoxColumn()
        ToolStrip1 = New ToolStrip()
        btnAccept = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        GroupBox1 = New GroupBox()
        Label3 = New Label()
        btnSearch = New Button()
        CType(dgvUsers, ComponentModel.ISupportInitialize).BeginInit()
        ToolStrip1.SuspendLayout()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' txtTerm
        ' 
        txtTerm.Location = New Point(23, 69)
        txtTerm.Margin = New Padding(5, 6, 5, 6)
        txtTerm.MaxLength = 60
        txtTerm.Name = "txtTerm"
        txtTerm.Size = New Size(262, 31)
        txtTerm.TabIndex = 1
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(18, 31)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(228, 25)
        Label1.TabIndex = 2
        Label1.Text = "Search Term"
        ' 
        ' cmbTermType
        ' 
        cmbTermType.DropDownStyle = ComboBoxStyle.DropDownList
        cmbTermType.FormattingEnabled = True
        cmbTermType.Items.AddRange(New Object() {"User Name ", "Full Name or part"})
        cmbTermType.Location = New Point(360, 67)
        cmbTermType.Margin = New Padding(5, 6, 5, 6)
        cmbTermType.Name = "cmbTermType"
        cmbTermType.Size = New Size(321, 33)
        cmbTermType.TabIndex = 3
        ' 
        ' dgvUsers
        ' 
        dgvUsers.AllowUserToAddRows = False
        dgvUsers.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.LavenderBlush
        dgvUsers.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvUsers.Columns.AddRange(New DataGridViewColumn() {User_ID, UserName, FullName})
        dgvUsers.Location = New Point(20, 196)
        dgvUsers.Margin = New Padding(5, 6, 5, 6)
        dgvUsers.Name = "dgvUsers"
        dgvUsers.ReadOnly = True
        dgvUsers.RowHeadersVisible = False
        dgvUsers.RowHeadersWidth = 62
        dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvUsers.Size = New Size(713, 329)
        dgvUsers.TabIndex = 4
        ' 
        ' User_ID
        ' 
        User_ID.FillWeight = 60F
        User_ID.HeaderText = "User_ID"
        User_ID.MaxInputLength = 20
        User_ID.MinimumWidth = 8
        User_ID.Name = "User_ID"
        User_ID.ReadOnly = True
        User_ID.SortMode = DataGridViewColumnSortMode.NotSortable
        User_ID.Width = 60
        ' 
        ' UserName
        ' 
        UserName.FillWeight = 150F
        UserName.HeaderText = "User Name"
        UserName.MaxInputLength = 20
        UserName.MinimumWidth = 8
        UserName.Name = "UserName"
        UserName.ReadOnly = True
        UserName.SortMode = DataGridViewColumnSortMode.NotSortable
        UserName.Width = 150
        ' 
        ' FullName
        ' 
        FullName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        FullName.HeaderText = "Full Name"
        FullName.MaxInputLength = 60
        FullName.MinimumWidth = 8
        FullName.Name = "FullName"
        FullName.ReadOnly = True
        FullName.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnAccept, ToolStripSeparator1, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(757, 34)
        ToolStrip1.TabIndex = 5
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnAccept
        ' 
        btnAccept.Enabled = False
        btnAccept.Image = CType(resources.GetObject("btnAccept.Image"), Image)
        btnAccept.ImageTransparentColor = Color.Magenta
        btnAccept.Name = "btnAccept"
        btnAccept.Size = New Size(94, 29)
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
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(btnSearch)
        GroupBox1.Controls.Add(cmbTermType)
        GroupBox1.Controls.Add(txtTerm)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Location = New Point(20, 54)
        GroupBox1.Margin = New Padding(5, 6, 5, 6)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(5, 6, 5, 6)
        GroupBox1.Size = New Size(713, 131)
        GroupBox1.TabIndex = 26
        GroupBox1.TabStop = False
        GroupBox1.Text = "Search"
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(355, 31)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(165, 25)
        Label3.TabIndex = 4
        Label3.Text = "Term Type"
        ' 
        ' btnSearch
        ' 
        btnSearch.Enabled = False
        btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), Image)
        btnSearch.Location = New Point(298, 62)
        btnSearch.Margin = New Padding(5, 6, 5, 6)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(52, 50)
        btnSearch.TabIndex = 2
        btnSearch.UseVisualStyleBackColor = True
        ' 
        ' frmUserLook
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(757, 556)
        Controls.Add(GroupBox1)
        Controls.Add(ToolStrip1)
        Controls.Add(dgvUsers)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmUserLook"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "User Search"
        CType(dgvUsers, ComponentModel.ISupportInitialize).EndInit()
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents txtTerm As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbTermType As System.Windows.Forms.ComboBox
    Friend WithEvents dgvUsers As System.Windows.Forms.DataGridView
    Friend WithEvents User_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UserName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FullName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnAccept As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button

End Class
