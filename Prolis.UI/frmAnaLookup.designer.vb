<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAnaLookup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAnaLookup))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        btnOK = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        GroupBox1 = New GroupBox()
        lbl_TotRec = New Label()
        Label2 = New Label()
        btnLook = New Button()
        cmbPosition = New ComboBox()
        Label1 = New Label()
        txtSearch = New TextBox()
        dgv = New DataGridView()
        TestID = New DataGridViewTextBoxColumn()
        TestName = New DataGridViewTextBoxColumn()
        Equip = New DataGridViewTextBoxColumn()
        Ctl = New DataGridViewTextBoxColumn()
        Valids = New DataGridViewTextBoxColumn()
        InRange = New DataGridViewTextBoxColumn()
        btn_Cancel = New Button()
        ToolStrip1.SuspendLayout()
        GroupBox1.SuspendLayout()
        CType(dgv, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(20, 20)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnOK, ToolStripSeparator1, btnCancel, ToolStripSeparator2, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(830, 34)
        ToolStrip1.TabIndex = 2
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnOK
        ' 
        btnOK.Enabled = False
        btnOK.Image = CType(resources.GetObject("btnOK.Image"), Image)
        btnOK.ImageTransparentColor = Color.Magenta
        btnOK.Name = "btnOK"
        btnOK.Size = New Size(90, 29)
        btnOK.Text = "Accept"
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
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 34)
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
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(btnLook)
        GroupBox1.Controls.Add(cmbPosition)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Controls.Add(txtSearch)
        GroupBox1.Location = New Point(20, 72)
        GroupBox1.Margin = New Padding(5, 6, 5, 6)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(5, 6, 5, 6)
        GroupBox1.Size = New Size(785, 116)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        GroupBox1.Text = "Search"
        ' 
        ' lbl_TotRec
        ' 
        lbl_TotRec.AutoSize = True
        lbl_TotRec.ForeColor = Color.Blue
        lbl_TotRec.Location = New Point(519, 70)
        lbl_TotRec.Margin = New Padding(5, 0, 5, 0)
        lbl_TotRec.Name = "lbl_TotRec"
        lbl_TotRec.Size = New Size(81, 25)
        lbl_TotRec.TabIndex = 6
        lbl_TotRec.Text = "Total Rec"
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.SaddleBrown
        Label2.Location = New Point(514, 31)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(200, 25)
        Label2.TabIndex = 4
        Label2.Text = "Term position"
        Label2.Visible = False
        ' 
        ' btnLook
        ' 
        btnLook.Image = CType(resources.GetObject("btnLook.Image"), Image)
        btnLook.Location = New Point(460, 58)
        btnLook.Margin = New Padding(5, 6, 5, 6)
        btnLook.Name = "btnLook"
        btnLook.Size = New Size(49, 47)
        btnLook.TabIndex = 1
        btnLook.UseVisualStyleBackColor = True
        ' 
        ' cmbPosition
        ' 
        cmbPosition.DropDownStyle = ComboBoxStyle.DropDownList
        cmbPosition.FormattingEnabled = True
        cmbPosition.Items.AddRange(New Object() {"Contains", "Starts with"})
        cmbPosition.Location = New Point(656, 27)
        cmbPosition.Margin = New Padding(5, 6, 5, 6)
        cmbPosition.Name = "cmbPosition"
        cmbPosition.Size = New Size(103, 33)
        cmbPosition.TabIndex = 2
        cmbPosition.Visible = False
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.SaddleBrown
        Label1.Location = New Point(10, 31)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(440, 25)
        Label1.TabIndex = 1
        Label1.Text = "A part of Name of Equipment"
        ' 
        ' txtSearch
        ' 
        txtSearch.Location = New Point(16, 66)
        txtSearch.Margin = New Padding(5, 6, 5, 6)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(430, 31)
        txtSearch.TabIndex = 0
        ' 
        ' dgv
        ' 
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(255), CByte(224), CByte(192))
        dgv.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv.Columns.AddRange(New DataGridViewColumn() {TestID, TestName, Equip, Ctl, Valids, InRange})
        dgv.Location = New Point(20, 198)
        dgv.Margin = New Padding(5, 6, 5, 6)
        dgv.Name = "dgv"
        dgv.ReadOnly = True
        dgv.RowHeadersVisible = False
        dgv.RowHeadersWidth = 62
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.Size = New Size(785, 402)
        dgv.TabIndex = 1
        ' 
        ' TestID
        ' 
        TestID.FillWeight = 50F
        TestID.HeaderText = "ID"
        TestID.MaxInputLength = 10
        TestID.MinimumWidth = 8
        TestID.Name = "TestID"
        TestID.ReadOnly = True
        TestID.Width = 84
        ' 
        ' TestName
        ' 
        TestName.FillWeight = 140F
        TestName.HeaderText = "Name"
        TestName.MaxInputLength = 60
        TestName.MinimumWidth = 8
        TestName.Name = "TestName"
        TestName.ReadOnly = True
        TestName.Width = 233
        ' 
        ' Equip
        ' 
        Equip.FillWeight = 138F
        Equip.HeaderText = "Equipment"
        Equip.MaxInputLength = 60
        Equip.MinimumWidth = 8
        Equip.Name = "Equip"
        Equip.ReadOnly = True
        Equip.Width = 231
        ' 
        ' Ctl
        ' 
        Ctl.FillWeight = 40F
        Ctl.HeaderText = "Ctl"
        Ctl.MaxInputLength = 2
        Ctl.MinimumWidth = 8
        Ctl.Name = "Ctl"
        Ctl.ReadOnly = True
        Ctl.SortMode = DataGridViewColumnSortMode.NotSortable
        Ctl.Width = 67
        ' 
        ' Valids
        ' 
        Valids.FillWeight = 40F
        Valids.HeaderText = "Valid"
        Valids.MinimumWidth = 8
        Valids.Name = "Valids"
        Valids.ReadOnly = True
        Valids.Width = 67
        ' 
        ' InRange
        ' 
        InRange.FillWeight = 60F
        InRange.HeaderText = "InRange"
        InRange.MaxInputLength = 6
        InRange.MinimumWidth = 8
        InRange.Name = "InRange"
        InRange.ReadOnly = True
        InRange.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' btn_Cancel
        ' 
        btn_Cancel.DialogResult = DialogResult.Cancel
        btn_Cancel.Location = New Point(641, 273)
        btn_Cancel.Margin = New Padding(4, 5, 4, 5)
        btn_Cancel.Name = "btn_Cancel"
        btn_Cancel.Size = New Size(116, 64)
        btn_Cancel.TabIndex = 6
        btn_Cancel.TabStop = False
        btn_Cancel.Text = "Cancel"
        btn_Cancel.UseVisualStyleBackColor = True
        ' 
        ' frmAnaLookup
        ' 
        AcceptButton = btnLook
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = btn_Cancel
        ClientSize = New Size(830, 627)
        Controls.Add(dgv)
        Controls.Add(GroupBox1)
        Controls.Add(ToolStrip1)
        Controls.Add(btn_Cancel)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmAnaLookup"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Analysis Lookup"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        CType(dgv, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnOK As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnLook As System.Windows.Forms.Button
    Friend WithEvents cmbPosition As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents TestID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TestName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Equip As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Ctl As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Valids As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InRange As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button
    Friend WithEvents lbl_TotRec As System.Windows.Forms.Label

End Class
