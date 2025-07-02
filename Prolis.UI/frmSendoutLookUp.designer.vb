<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSendoutLookUp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSendoutLookUp))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        btnOK = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        GroupBox1 = New GroupBox()
        dtpDate = New DateTimePicker()
        Label3 = New Label()
        Label2 = New Label()
        btnLook = New Button()
        cmbLab = New ComboBox()
        Label1 = New Label()
        txtAccID = New TextBox()
        dgv = New DataGridView()
        SendoutID = New DataGridViewTextBoxColumn()
        AccID = New DataGridViewTextBoxColumn()
        Dated = New DataGridViewTextBoxColumn()
        Lab = New DataGridViewTextBoxColumn()
        Tests = New DataGridViewTextBoxColumn()
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
        ToolStrip1.Size = New Size(826, 34)
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
        GroupBox1.Controls.Add(dtpDate)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(btnLook)
        GroupBox1.Controls.Add(cmbLab)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Controls.Add(txtAccID)
        GroupBox1.Location = New Point(20, 53)
        GroupBox1.Margin = New Padding(5, 6, 5, 6)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(5, 6, 5, 6)
        GroupBox1.Size = New Size(785, 128)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        GroupBox1.Text = "Search"
        ' 
        ' dtpDate
        ' 
        dtpDate.Format = DateTimePickerFormat.Short
        dtpDate.Location = New Point(151, 66)
        dtpDate.Margin = New Padding(5, 6, 5, 6)
        dtpDate.Name = "dtpDate"
        dtpDate.Size = New Size(155, 31)
        dtpDate.TabIndex = 1
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(151, 34)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(126, 25)
        Label3.TabIndex = 6
        Label3.Text = "Date + Span"
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.Magenta
        Label2.Location = New Point(334, 34)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(151, 25)
        Label2.TabIndex = 4
        Label2.Text = "Reference Lab"
        ' 
        ' btnLook
        ' 
        btnLook.Image = CType(resources.GetObject("btnLook.Image"), Image)
        btnLook.Location = New Point(726, 58)
        btnLook.Margin = New Padding(5, 6, 5, 6)
        btnLook.Name = "btnLook"
        btnLook.Size = New Size(49, 47)
        btnLook.TabIndex = 3
        btnLook.UseVisualStyleBackColor = True
        ' 
        ' cmbLab
        ' 
        cmbLab.DropDownStyle = ComboBoxStyle.DropDownList
        cmbLab.FormattingEnabled = True
        cmbLab.Location = New Point(320, 66)
        cmbLab.Margin = New Padding(5, 6, 5, 6)
        cmbLab.Name = "cmbLab"
        cmbLab.Size = New Size(394, 33)
        cmbLab.TabIndex = 2
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.Magenta
        Label1.Location = New Point(16, 34)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(125, 25)
        Label1.TabIndex = 1
        Label1.Text = "Accession ID"
        ' 
        ' txtAccID
        ' 
        txtAccID.Location = New Point(16, 66)
        txtAccID.Margin = New Padding(5, 6, 5, 6)
        txtAccID.Name = "txtAccID"
        txtAccID.Size = New Size(123, 31)
        txtAccID.TabIndex = 0
        ' 
        ' dgv
        ' 
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(255), CByte(224), CByte(192))
        dgv.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv.Columns.AddRange(New DataGridViewColumn() {SendoutID, AccID, Dated, Lab, Tests})
        dgv.Location = New Point(21, 211)
        dgv.Margin = New Padding(5, 6, 5, 6)
        dgv.Name = "dgv"
        dgv.ReadOnly = True
        dgv.RowHeadersVisible = False
        dgv.RowHeadersWidth = 62
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.Size = New Size(785, 402)
        dgv.TabIndex = 1
        ' 
        ' SendoutID
        ' 
        SendoutID.FillWeight = 50F
        SendoutID.HeaderText = "ID"
        SendoutID.MaxInputLength = 10
        SendoutID.MinimumWidth = 8
        SendoutID.Name = "SendoutID"
        SendoutID.ReadOnly = True
        SendoutID.Visible = False
        ' 
        ' AccID
        ' 
        AccID.FillWeight = 80F
        AccID.HeaderText = "Accession"
        AccID.MinimumWidth = 8
        AccID.Name = "AccID"
        AccID.ReadOnly = True
        ' 
        ' Dated
        ' 
        Dated.FillWeight = 60F
        Dated.HeaderText = "Dated"
        Dated.MaxInputLength = 10
        Dated.MinimumWidth = 8
        Dated.Name = "Dated"
        Dated.ReadOnly = True
        Dated.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Lab
        ' 
        Lab.FillWeight = 125F
        Lab.HeaderText = "Ref Lab"
        Lab.MinimumWidth = 8
        Lab.Name = "Lab"
        Lab.ReadOnly = True
        ' 
        ' Tests
        ' 
        Tests.FillWeight = 203F
        Tests.HeaderText = "Tests"
        Tests.MinimumWidth = 8
        Tests.Name = "Tests"
        Tests.ReadOnly = True
        Tests.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' btn_Cancel
        ' 
        btn_Cancel.DialogResult = DialogResult.Cancel
        btn_Cancel.Location = New Point(661, 288)
        btn_Cancel.Margin = New Padding(4, 5, 4, 5)
        btn_Cancel.Name = "btn_Cancel"
        btn_Cancel.Size = New Size(116, 64)
        btn_Cancel.TabIndex = 6
        btn_Cancel.TabStop = False
        btn_Cancel.Text = "Cancel"
        btn_Cancel.UseVisualStyleBackColor = True
        ' 
        ' frmSendoutLookUp
        ' 
        AcceptButton = btnLook
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = btn_Cancel
        ClientSize = New Size(826, 642)
        Controls.Add(dgv)
        Controls.Add(GroupBox1)
        Controls.Add(ToolStrip1)
        Controls.Add(btn_Cancel)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmSendoutLookUp"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Sendout Look Up"
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
    Friend WithEvents cmbLab As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtAccID As System.Windows.Forms.TextBox
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents SendoutID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AccID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Dated As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Lab As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tests As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button

End Class
