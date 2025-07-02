<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInvoiceLookUp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInvoiceLookUp))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        btnOK = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        dgv = New DataGridView()
        InvID = New DataGridViewTextBoxColumn()
        AccID = New DataGridViewTextBoxColumn()
        PatName = New DataGridViewTextBoxColumn()
        Amt = New DataGridViewTextBoxColumn()
        GroupBox1 = New GroupBox()
        Label19 = New Label()
        Label5 = New Label()
        Label4 = New Label()
        Label3 = New Label()
        txtAccID = New TextBox()
        dtpSvcDate = New DateTimePicker()
        Label2 = New Label()
        btnLook = New Button()
        Label1 = New Label()
        txtTerm = New TextBox()
        btn_Cancel = New Button()
        ToolStrip1.SuspendLayout()
        CType(dgv, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(20, 20)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnOK, ToolStripSeparator1, btnCancel, ToolStripSeparator2, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(851, 34)
        ToolStrip1.TabIndex = 3
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
        ' dgv
        ' 
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(255), CByte(224), CByte(192))
        dgv.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv.Columns.AddRange(New DataGridViewColumn() {InvID, AccID, PatName, Amt})
        dgv.Location = New Point(20, 239)
        dgv.Margin = New Padding(5, 6, 5, 6)
        dgv.Name = "dgv"
        dgv.ReadOnly = True
        dgv.RowHeadersVisible = False
        dgv.RowHeadersWidth = 62
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.Size = New Size(809, 342)
        dgv.TabIndex = 6
        ' 
        ' InvID
        ' 
        InvID.FillWeight = 90.0F
        InvID.HeaderText = "Inv ID"
        InvID.MaxInputLength = 18
        InvID.MinimumWidth = 8
        InvID.Name = "InvID"
        InvID.ReadOnly = True
        ' 
        ' AccID
        ' 
        AccID.FillWeight = 90.0F
        AccID.HeaderText = "Acc ID"
        AccID.MaxInputLength = 18
        AccID.MinimumWidth = 8
        AccID.Name = "AccID"
        AccID.ReadOnly = True
        AccID.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' PatName
        ' 
        PatName.FillWeight = 190.0F
        PatName.HeaderText = "Patient Name"
        PatName.MaxInputLength = 75
        PatName.MinimumWidth = 8
        PatName.Name = "PatName"
        PatName.ReadOnly = True
        ' 
        ' Amt
        ' 
        Amt.FillWeight = 90.0F
        Amt.HeaderText = "Amount"
        Amt.MaxInputLength = 12
        Amt.MinimumWidth = 8
        Amt.Name = "Amt"
        Amt.ReadOnly = True
        Amt.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(Label19)
        GroupBox1.Controls.Add(Label5)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(txtAccID)
        GroupBox1.Controls.Add(dtpSvcDate)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(btnLook)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Controls.Add(txtTerm)
        GroupBox1.Location = New Point(20, 72)
        GroupBox1.Margin = New Padding(5, 6, 5, 6)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(5, 6, 5, 6)
        GroupBox1.Size = New Size(809, 131)
        GroupBox1.TabIndex = 7
        GroupBox1.TabStop = False
        GroupBox1.Text = "Search"
        ' 
        ' Label19
        ' 
        Label19.BackColor = SystemColors.Control
        Label19.Image = My.Resources.Resources.paste
        Label19.Location = New Point(664, 41)
        Label19.Margin = New Padding(4, 0, 4, 0)
        Label19.Name = "Label19"
        Label19.Size = New Size(40, 28)
        Label19.TabIndex = 93
        Label19.Text = "      "
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.Fuchsia
        Label5.Location = New Point(295, 41)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(170, 25)
        Label5.TabIndex = 9
        Label5.Text = "Svc Date"
        ' 
        ' Label4
        ' 
        Label4.Location = New Point(475, 81)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(50, 25)
        Label4.TabIndex = 8
        Label4.Text = "OR"
        Label4.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(235, 81)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(50, 25)
        Label3.TabIndex = 7
        Label3.Text = "AND"
        ' 
        ' txtAccID
        ' 
        txtAccID.Location = New Point(540, 75)
        txtAccID.Margin = New Padding(5, 6, 5, 6)
        txtAccID.Name = "txtAccID"
        txtAccID.Size = New Size(164, 31)
        txtAccID.TabIndex = 6
        ' 
        ' dtpSvcDate
        ' 
        dtpSvcDate.Format = DateTimePickerFormat.Short
        dtpSvcDate.Location = New Point(295, 75)
        dtpSvcDate.Margin = New Padding(5, 6, 5, 6)
        dtpSvcDate.Name = "dtpSvcDate"
        dtpSvcDate.Size = New Size(168, 31)
        dtpSvcDate.TabIndex = 5
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.Fuchsia
        Label2.Location = New Point(549, 41)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(121, 25)
        Label2.TabIndex = 4
        Label2.Text = "Accession ID"
        ' 
        ' btnLook
        ' 
        btnLook.Image = CType(resources.GetObject("btnLook.Image"), Image)
        btnLook.Location = New Point(716, 69)
        btnLook.Margin = New Padding(5, 6, 5, 6)
        btnLook.Name = "btnLook"
        btnLook.Size = New Size(69, 47)
        btnLook.TabIndex = 3
        btnLook.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.Fuchsia
        Label1.Location = New Point(10, 41)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(215, 25)
        Label1.TabIndex = 1
        Label1.Text = "Patient Name (Last, First)"
        ' 
        ' txtTerm
        ' 
        txtTerm.Location = New Point(16, 75)
        txtTerm.Margin = New Padding(5, 6, 5, 6)
        txtTerm.Name = "txtTerm"
        txtTerm.Size = New Size(205, 31)
        txtTerm.TabIndex = 0
        ' 
        ' btn_Cancel
        ' 
        btn_Cancel.DialogResult = DialogResult.Cancel
        btn_Cancel.Location = New Point(689, 303)
        btn_Cancel.Margin = New Padding(4, 5, 4, 5)
        btn_Cancel.Name = "btn_Cancel"
        btn_Cancel.Size = New Size(116, 64)
        btn_Cancel.TabIndex = 8
        btn_Cancel.TabStop = False
        btn_Cancel.Text = "Cancel"
        btn_Cancel.UseVisualStyleBackColor = True
        ' 
        ' frmInvoiceLookUp
        ' 
        AcceptButton = btnLook
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = btn_Cancel
        ClientSize = New Size(851, 611)
        Controls.Add(GroupBox1)
        Controls.Add(dgv)
        Controls.Add(ToolStrip1)
        Controls.Add(btn_Cancel)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmInvoiceLookUp"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Invoice LookUp"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgv, ComponentModel.ISupportInitialize).EndInit()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnOK As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnLook As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTerm As System.Windows.Forms.TextBox
    Friend WithEvents dtpSvcDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtAccID As System.Windows.Forms.TextBox
    Friend WithEvents InvID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AccID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PatName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Amt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button
    Friend WithEvents Label19 As System.Windows.Forms.Label

End Class
