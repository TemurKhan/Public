<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPmntLookUp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPmntLookUp))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        btnAccept = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        dgvPayments = New DataGridView()
        ARID = New DataGridViewTextBoxColumn()
        DocNo = New DataGridViewTextBoxColumn()
        Dated = New DataGridViewTextBoxColumn()
        Amount = New DataGridViewTextBoxColumn()
        ArType = New DataGridViewTextBoxColumn()
        ARName = New DataGridViewTextBoxColumn()
        btnPmtSearch = New Button()
        Label2 = New Label()
        txtSearch = New MaskedTextBox()
        cmbARType = New ComboBox()
        Label3 = New Label()
        txtFrom = New MaskedTextBox()
        Label5 = New Label()
        Label6 = New Label()
        txtDocID = New TextBox()
        txtTo = New MaskedTextBox()
        Label1 = New Label()
        txtInvID = New TextBox()
        Label4 = New Label()
        Label7 = New Label()
        GroupBox1 = New GroupBox()
        txtSvcDate = New MaskedTextBox()
        btn_Cancel = New Button()
        ToolStrip1.SuspendLayout()
        CType(dgvPayments, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(20, 20)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnAccept, ToolStripSeparator1, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(899, 34)
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
        ' dgvPayments
        ' 
        dgvPayments.AllowUserToAddRows = False
        dgvPayments.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(255), CByte(224), CByte(192))
        dgvPayments.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvPayments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvPayments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvPayments.Columns.AddRange(New DataGridViewColumn() {ARID, DocNo, Dated, Amount, ArType, ARName})
        dgvPayments.Location = New Point(20, 314)
        dgvPayments.Margin = New Padding(5, 6, 5, 6)
        dgvPayments.Name = "dgvPayments"
        dgvPayments.ReadOnly = True
        dgvPayments.RowHeadersVisible = False
        dgvPayments.RowHeadersWidth = 62
        dgvPayments.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvPayments.Size = New Size(851, 436)
        dgvPayments.TabIndex = 1
        ' 
        ' ARID
        ' 
        ARID.FillWeight = 60F
        ARID.HeaderText = "ID"
        ARID.MaxInputLength = 12
        ARID.MinimumWidth = 8
        ARID.Name = "ARID"
        ARID.ReadOnly = True
        ' 
        ' DocNo
        ' 
        DocNo.FillWeight = 90F
        DocNo.HeaderText = "Document"
        DocNo.MinimumWidth = 8
        DocNo.Name = "DocNo"
        DocNo.ReadOnly = True
        ' 
        ' Dated
        ' 
        Dated.FillWeight = 60F
        Dated.HeaderText = "Dated"
        Dated.MinimumWidth = 8
        Dated.Name = "Dated"
        Dated.ReadOnly = True
        ' 
        ' Amount
        ' 
        Amount.FillWeight = 90F
        Amount.HeaderText = "Amount"
        Amount.MaxInputLength = 12
        Amount.MinimumWidth = 8
        Amount.Name = "Amount"
        Amount.ReadOnly = True
        Amount.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' ArType
        ' 
        ArType.FillWeight = 25F
        ArType.HeaderText = "AR"
        ArType.MinimumWidth = 8
        ArType.Name = "ArType"
        ArType.ReadOnly = True
        ArType.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' ARName
        ' 
        ARName.FillWeight = 160F
        ARName.HeaderText = "AR Name"
        ARName.MaxInputLength = 150
        ARName.MinimumWidth = 8
        ARName.Name = "ARName"
        ARName.ReadOnly = True
        ' 
        ' btnPmtSearch
        ' 
        btnPmtSearch.Image = CType(resources.GetObject("btnPmtSearch.Image"), Image)
        btnPmtSearch.Location = New Point(684, 142)
        btnPmtSearch.Margin = New Padding(5, 6, 5, 6)
        btnPmtSearch.Name = "btnPmtSearch"
        btnPmtSearch.Size = New Size(131, 53)
        btnPmtSearch.TabIndex = 7
        btnPmtSearch.UseVisualStyleBackColor = True
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
        ' txtSearch
        ' 
        txtSearch.Location = New Point(26, 75)
        txtSearch.Margin = New Padding(5, 6, 5, 6)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(379, 31)
        txtSearch.TabIndex = 0
        ' 
        ' cmbARType
        ' 
        cmbARType.DropDownStyle = ComboBoxStyle.DropDownList
        cmbARType.FormattingEnabled = True
        cmbARType.Items.AddRange(New Object() {"Client", "Third Party", "Patient"})
        cmbARType.Location = New Point(419, 73)
        cmbARType.Margin = New Padding(5, 6, 5, 6)
        cmbARType.Name = "cmbARType"
        cmbARType.Size = New Size(173, 33)
        cmbARType.TabIndex = 1
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.MidnightBlue
        Label3.Location = New Point(439, 41)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(131, 33)
        Label3.TabIndex = 9
        Label3.Text = "AR Type"
        ' 
        ' txtFrom
        ' 
        txtFrom.Location = New Point(26, 152)
        txtFrom.Margin = New Padding(5, 6, 5, 6)
        txtFrom.Mask = "00/00/0000"
        txtFrom.Name = "txtFrom"
        txtFrom.Size = New Size(134, 31)
        txtFrom.TabIndex = 3
        txtFrom.ValidatingType = GetType(Date)
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.MidnightBlue
        Label5.Location = New Point(34, 122)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(130, 25)
        Label5.TabIndex = 15
        Label5.Text = "Payment From"
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.MidnightBlue
        Label6.Location = New Point(174, 122)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(136, 25)
        Label6.TabIndex = 16
        Label6.Text = "Payment To"
        ' 
        ' txtDocID
        ' 
        txtDocID.Location = New Point(604, 75)
        txtDocID.Margin = New Padding(5, 6, 5, 6)
        txtDocID.MaxLength = 12
        txtDocID.Name = "txtDocID"
        txtDocID.Size = New Size(209, 31)
        txtDocID.TabIndex = 2
        txtDocID.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtTo
        ' 
        txtTo.Location = New Point(174, 152)
        txtTo.Margin = New Padding(5, 6, 5, 6)
        txtTo.Mask = "00/00/0000"
        txtTo.Name = "txtTo"
        txtTo.Size = New Size(134, 31)
        txtTo.TabIndex = 4
        txtTo.ValidatingType = GetType(Date)
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.MidnightBlue
        Label1.Location = New Point(614, 41)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(160, 25)
        Label1.TabIndex = 18
        Label1.Text = "Document #"
        ' 
        ' txtInvID
        ' 
        txtInvID.Location = New Point(339, 152)
        txtInvID.Margin = New Padding(5, 6, 5, 6)
        txtInvID.MaxLength = 12
        txtInvID.Name = "txtInvID"
        txtInvID.Size = New Size(153, 31)
        txtInvID.TabIndex = 5
        txtInvID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.MidnightBlue
        Label4.Location = New Point(360, 123)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(106, 25)
        Label4.TabIndex = 20
        Label4.Text = "Invoice ID"
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.MidnightBlue
        Label7.Location = New Point(525, 123)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(134, 25)
        Label7.TabIndex = 22
        Label7.Text = "Service Date"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(txtSvcDate)
        GroupBox1.Controls.Add(Label7)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(txtInvID)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Controls.Add(txtTo)
        GroupBox1.Controls.Add(txtDocID)
        GroupBox1.Controls.Add(Label6)
        GroupBox1.Controls.Add(Label5)
        GroupBox1.Controls.Add(txtFrom)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(cmbARType)
        GroupBox1.Controls.Add(txtSearch)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(btnPmtSearch)
        GroupBox1.Location = New Point(20, 72)
        GroupBox1.Margin = New Padding(5, 6, 5, 6)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(5, 6, 5, 6)
        GroupBox1.Size = New Size(851, 214)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        GroupBox1.Text = "Search"
        ' 
        ' txtSvcDate
        ' 
        txtSvcDate.Location = New Point(521, 152)
        txtSvcDate.Margin = New Padding(5, 6, 5, 6)
        txtSvcDate.Mask = "00/00/0000"
        txtSvcDate.Name = "txtSvcDate"
        txtSvcDate.Size = New Size(134, 31)
        txtSvcDate.TabIndex = 6
        txtSvcDate.ValidatingType = GetType(Date)
        ' 
        ' btn_Cancel
        ' 
        btn_Cancel.DialogResult = DialogResult.Cancel
        btn_Cancel.Location = New Point(739, 434)
        btn_Cancel.Margin = New Padding(4, 5, 4, 5)
        btn_Cancel.Name = "btn_Cancel"
        btn_Cancel.Size = New Size(116, 64)
        btn_Cancel.TabIndex = 30
        btn_Cancel.TabStop = False
        btn_Cancel.Text = "Cancel"
        btn_Cancel.UseVisualStyleBackColor = True
        ' 
        ' frmPmntLookUp
        ' 
        AcceptButton = btnPmtSearch
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = btn_Cancel
        ClientSize = New Size(899, 773)
        Controls.Add(dgvPayments)
        Controls.Add(GroupBox1)
        Controls.Add(ToolStrip1)
        Controls.Add(btn_Cancel)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmPmntLookUp"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Payment Look Up"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgvPayments, ComponentModel.ISupportInitialize).EndInit()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnAccept As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents dgvPayments As System.Windows.Forms.DataGridView
    Friend WithEvents btnPmtSearch As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cmbARType As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtFrom As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtDocID As System.Windows.Forms.TextBox
    Friend WithEvents txtTo As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtInvID As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSvcDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents ARID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DocNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Dated As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Amount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ArType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ARName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button

End Class
