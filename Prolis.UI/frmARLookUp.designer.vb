<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmARLookUp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmARLookUp))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        btnAccept = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        GroupBox1 = New GroupBox()
        Label19 = New Label()
        Label4 = New Label()
        txtControl = New TextBox()
        Label1 = New Label()
        txtAccID = New TextBox()
        Label6 = New Label()
        Label5 = New Label()
        txtFrom = New MaskedTextBox()
        txtTo = New MaskedTextBox()
        Label3 = New Label()
        cmbARType = New ComboBox()
        txtSearch = New MaskedTextBox()
        Label2 = New Label()
        btnPatSearch = New Button()
        dgvARs = New DataGridView()
        ARID = New DataGridViewTextBoxColumn()
        ARName = New DataGridViewTextBoxColumn()
        Balance = New DataGridViewTextBoxColumn()
        btn_Cancel = New Button()
        ToolStrip1.SuspendLayout()
        GroupBox1.SuspendLayout()
        CType(dgvARs, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(20, 20)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnAccept, ToolStripSeparator1, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(855, 34)
        ToolStrip1.TabIndex = 2
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
        btnCancel.AutoSize = False
        btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), Image)
        btnCancel.ImageTransparentColor = Color.Magenta
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(59, 22)
        btnCancel.Text = "Cancel"
        ' 
        ' ToolStripSeparator4
        ' 
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New Size(6, 34)
        ' 
        ' btnHelp
        ' 
        btnHelp.AutoSize = False
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.ImageTransparentColor = Color.Magenta
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(48, 22)
        btnHelp.Text = "Help"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(Label19)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(txtControl)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Controls.Add(txtAccID)
        GroupBox1.Controls.Add(Label6)
        GroupBox1.Controls.Add(Label5)
        GroupBox1.Controls.Add(txtFrom)
        GroupBox1.Controls.Add(txtTo)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(cmbARType)
        GroupBox1.Controls.Add(txtSearch)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(btnPatSearch)
        GroupBox1.Location = New Point(20, 73)
        GroupBox1.Margin = New Padding(5, 6, 5, 6)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(5, 6, 5, 6)
        GroupBox1.Size = New Size(805, 225)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        GroupBox1.Text = "Search"
        ' 
        ' Label19
        ' 
        Label19.BackColor = SystemColors.Control
        Label19.Image = My.Resources.Resources.paste
        Label19.Location = New Point(432, 116)
        Label19.Margin = New Padding(4, 0, 4, 0)
        Label19.Name = "Label19"
        Label19.Size = New Size(44, 41)
        Label19.TabIndex = 92
        Label19.Text = "      "
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.MidnightBlue
        Label4.Location = New Point(486, 123)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(160, 25)
        Label4.TabIndex = 20
        Label4.Text = "Control No"
        ' 
        ' txtControl
        ' 
        txtControl.Location = New Point(486, 167)
        txtControl.Margin = New Padding(5, 6, 5, 6)
        txtControl.MaxLength = 12
        txtControl.Name = "txtControl"
        txtControl.Size = New Size(158, 31)
        txtControl.TabIndex = 6
        txtControl.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.MidnightBlue
        Label1.Location = New Point(316, 123)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(116, 25)
        Label1.TabIndex = 18
        Label1.Text = "Accession ID"
        ' 
        ' txtAccID
        ' 
        txtAccID.Location = New Point(316, 167)
        txtAccID.Margin = New Padding(5, 6, 5, 6)
        txtAccID.MaxLength = 12
        txtAccID.Name = "txtAccID"
        txtAccID.Size = New Size(158, 31)
        txtAccID.TabIndex = 5
        txtAccID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.MidnightBlue
        Label6.Location = New Point(185, 122)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(160, 25)
        Label6.TabIndex = 16
        Label6.Text = "Billing To"
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.MidnightBlue
        Label5.Location = New Point(34, 122)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(160, 25)
        Label5.TabIndex = 15
        Label5.Text = "Billing From"
        ' 
        ' txtFrom
        ' 
        txtFrom.Location = New Point(26, 167)
        txtFrom.Margin = New Padding(5, 6, 5, 6)
        txtFrom.Mask = "00/00/0000"
        txtFrom.Name = "txtFrom"
        txtFrom.Size = New Size(133, 31)
        txtFrom.TabIndex = 3
        txtFrom.ValidatingType = GetType(Date)
        ' 
        ' txtTo
        ' 
        txtTo.Location = New Point(171, 167)
        txtTo.Margin = New Padding(5, 6, 5, 6)
        txtTo.Mask = "00/00/0000"
        txtTo.Name = "txtTo"
        txtTo.Size = New Size(133, 31)
        txtTo.TabIndex = 4
        txtTo.ValidatingType = GetType(Date)
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.MidnightBlue
        Label3.Location = New Point(564, 39)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(190, 25)
        Label3.TabIndex = 9
        Label3.Text = "AR Type"
        ' 
        ' cmbARType
        ' 
        cmbARType.DropDownStyle = ComboBoxStyle.DropDownList
        cmbARType.FormattingEnabled = True
        cmbARType.Items.AddRange(New Object() {"Client", "Third Party", "Patient"})
        cmbARType.Location = New Point(564, 69)
        cmbARType.Margin = New Padding(5, 6, 5, 6)
        cmbARType.Name = "cmbARType"
        cmbARType.Size = New Size(203, 33)
        cmbARType.TabIndex = 2
        ' 
        ' txtSearch
        ' 
        txtSearch.Location = New Point(26, 72)
        txtSearch.Margin = New Padding(5, 6, 5, 6)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(504, 31)
        txtSearch.TabIndex = 1
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(34, 41)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(375, 25)
        Label2.TabIndex = 4
        Label2.Text = "Search Term (AR Name even Partial)"
        ' 
        ' btnPatSearch
        ' 
        btnPatSearch.Image = CType(resources.GetObject("btnPatSearch.Image"), Image)
        btnPatSearch.Location = New Point(679, 156)
        btnPatSearch.Margin = New Padding(5, 6, 5, 6)
        btnPatSearch.Name = "btnPatSearch"
        btnPatSearch.Size = New Size(90, 59)
        btnPatSearch.TabIndex = 7
        btnPatSearch.UseVisualStyleBackColor = True
        ' 
        ' dgvARs
        ' 
        dgvARs.AllowUserToAddRows = False
        dgvARs.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(255), CByte(224), CByte(192))
        dgvARs.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvARs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvARs.Columns.AddRange(New DataGridViewColumn() {ARID, ARName, Balance})
        dgvARs.Location = New Point(20, 323)
        dgvARs.Margin = New Padding(5, 6, 5, 6)
        dgvARs.Name = "dgvARs"
        dgvARs.ReadOnly = True
        dgvARs.RowHeadersVisible = False
        dgvARs.RowHeadersWidth = 62
        dgvARs.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvARs.Size = New Size(805, 456)
        dgvARs.TabIndex = 1
        ' 
        ' ARID
        ' 
        ARID.FillWeight = 70F
        ARID.HeaderText = "ID"
        ARID.MaxInputLength = 12
        ARID.MinimumWidth = 8
        ARID.Name = "ARID"
        ARID.ReadOnly = True
        ARID.Width = 70
        ' 
        ' ARName
        ' 
        ARName.FillWeight = 320F
        ARName.HeaderText = "AR Name"
        ARName.MaxInputLength = 150
        ARName.MinimumWidth = 8
        ARName.Name = "ARName"
        ARName.ReadOnly = True
        ARName.Width = 320
        ' 
        ' Balance
        ' 
        Balance.FillWeight = 90F
        Balance.HeaderText = "Balance"
        Balance.MaxInputLength = 12
        Balance.MinimumWidth = 8
        Balance.Name = "Balance"
        Balance.ReadOnly = True
        Balance.SortMode = DataGridViewColumnSortMode.NotSortable
        Balance.Width = 90
        ' 
        ' btn_Cancel
        ' 
        btn_Cancel.DialogResult = DialogResult.Cancel
        btn_Cancel.Location = New Point(671, 453)
        btn_Cancel.Margin = New Padding(4, 5, 4, 5)
        btn_Cancel.Name = "btn_Cancel"
        btn_Cancel.Size = New Size(116, 64)
        btn_Cancel.TabIndex = 29
        btn_Cancel.TabStop = False
        btn_Cancel.Text = "Cancel"
        btn_Cancel.UseVisualStyleBackColor = True
        ' 
        ' frmARLookUp
        ' 
        AcceptButton = btnPatSearch
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = btn_Cancel
        ClientSize = New Size(855, 802)
        Controls.Add(dgvARs)
        Controls.Add(GroupBox1)
        Controls.Add(ToolStrip1)
        Controls.Add(btn_Cancel)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmARLookUp"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "AR Look Up"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        CType(dgvARs, ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents cmbARType As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearch As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnPatSearch As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtFrom As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtTo As System.Windows.Forms.MaskedTextBox
    Friend WithEvents dgvARs As System.Windows.Forms.DataGridView
    Friend WithEvents ARID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ARName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Balance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtAccID As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtControl As System.Windows.Forms.TextBox
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button
    Friend WithEvents Label19 As System.Windows.Forms.Label

End Class
