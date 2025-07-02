<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPartLookUp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPartLookUp))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        btnAccept = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        GroupBox1 = New GroupBox()
        lbl_TotRec = New Label()
        txtSearch = New MaskedTextBox()
        btnSearch = New Button()
        Label1 = New Label()
        dgv = New DataGridView()
        ID = New DataGridViewTextBoxColumn()
        Partner = New DataGridViewTextBoxColumn()
        Address = New DataGridViewTextBoxColumn()
        btn_Cancel = New Button()
        ToolStrip1.SuspendLayout()
        GroupBox1.SuspendLayout()
        CType(dgv, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(20, 20)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnAccept, ToolStripSeparator1, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(760, 34)
        ToolStrip1.TabIndex = 5
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
        GroupBox1.Controls.Add(lbl_TotRec)
        GroupBox1.Controls.Add(txtSearch)
        GroupBox1.Controls.Add(btnSearch)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Location = New Point(20, 53)
        GroupBox1.Margin = New Padding(5, 6, 5, 6)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(5, 6, 5, 6)
        GroupBox1.Size = New Size(715, 131)
        GroupBox1.TabIndex = 27
        GroupBox1.TabStop = False
        GroupBox1.Text = "Search"
        ' 
        ' lbl_TotRec
        ' 
        lbl_TotRec.AutoSize = True
        lbl_TotRec.ForeColor = Color.Blue
        lbl_TotRec.Location = New Point(511, 77)
        lbl_TotRec.Margin = New Padding(5, 0, 5, 0)
        lbl_TotRec.Name = "lbl_TotRec"
        lbl_TotRec.Size = New Size(106, 25)
        lbl_TotRec.TabIndex = 7
        lbl_TotRec.Text = "Total Rec     "
        ' 
        ' txtSearch
        ' 
        txtSearch.Location = New Point(24, 72)
        txtSearch.Margin = New Padding(5, 6, 5, 6)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(400, 31)
        txtSearch.TabIndex = 5
        ' 
        ' btnSearch
        ' 
        btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), Image)
        btnSearch.Location = New Point(435, 62)
        btnSearch.Margin = New Padding(5, 6, 5, 6)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(51, 53)
        btnSearch.TabIndex = 2
        btnSearch.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.MidnightBlue
        Label1.Location = New Point(24, 34)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(240, 25)
        Label1.TabIndex = 0
        Label1.Text = "Name (Even Partial)"
        ' 
        ' dgv
        ' 
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(255), CByte(224), CByte(192))
        dgv.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv.Columns.AddRange(New DataGridViewColumn() {ID, Partner, Address})
        dgv.Location = New Point(20, 197)
        dgv.Margin = New Padding(5, 6, 5, 6)
        dgv.Name = "dgv"
        dgv.ReadOnly = True
        dgv.RowHeadersVisible = False
        dgv.RowHeadersWidth = 62
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.Size = New Size(715, 391)
        dgv.TabIndex = 28
        ' 
        ' ID
        ' 
        ID.FillWeight = 60F
        ID.HeaderText = "ID"
        ID.MaxInputLength = 5
        ID.MinimumWidth = 8
        ID.Name = "ID"
        ID.ReadOnly = True
        ' 
        ' Partner
        ' 
        Partner.FillWeight = 120F
        Partner.HeaderText = "Name"
        Partner.MaxInputLength = 60
        Partner.MinimumWidth = 8
        Partner.Name = "Partner"
        Partner.ReadOnly = True
        ' 
        ' Address
        ' 
        Address.FillWeight = 246F
        Address.HeaderText = "Address"
        Address.MaxInputLength = 200
        Address.MinimumWidth = 8
        Address.Name = "Address"
        Address.ReadOnly = True
        Address.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' btn_Cancel
        ' 
        btn_Cancel.DialogResult = DialogResult.Cancel
        btn_Cancel.Location = New Point(578, 325)
        btn_Cancel.Margin = New Padding(4, 5, 4, 5)
        btn_Cancel.Name = "btn_Cancel"
        btn_Cancel.Size = New Size(116, 64)
        btn_Cancel.TabIndex = 29
        btn_Cancel.TabStop = False
        btn_Cancel.Text = "Cancel"
        btn_Cancel.UseVisualStyleBackColor = True
        ' 
        ' frmPartLookUp
        ' 
        AcceptButton = btnSearch
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = btn_Cancel
        ClientSize = New Size(760, 619)
        Controls.Add(dgv)
        Controls.Add(GroupBox1)
        Controls.Add(ToolStrip1)
        Controls.Add(btn_Cancel)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmPartLookUp"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Partner Look Up"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        CType(dgv, ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents txtSearch As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Partner As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Address As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button
    Friend WithEvents lbl_TotRec As System.Windows.Forms.Label

End Class
