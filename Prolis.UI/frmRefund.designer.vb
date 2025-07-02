<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRefund
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRefund))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        chkEditNew = New ToolStripButton()
        ToolStripButton2 = New ToolStripSeparator()
        btnSave = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnDelete = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        Label13 = New Label()
        btnRefLookUp = New Button()
        txtID = New TextBox()
        Label4 = New Label()
        txtDoc = New TextBox()
        Label2 = New Label()
        txtARName = New TextBox()
        Label1 = New Label()
        dtpRefundDate = New DateTimePicker()
        Label6 = New Label()
        txtAmount = New TextBox()
        lblDated = New Label()
        dgvInvoices = New DataGridView()
        InvoiceID = New DataGridViewTextBoxColumn()
        AccID = New DataGridViewTextBoxColumn()
        PatName = New DataGridViewTextBoxColumn()
        SvcDate = New DataGridViewTextBoxColumn()
        PmtAmt = New DataGridViewTextBoxColumn()
        TotalRefund = New DataGridViewTextBoxColumn()
        dgvPayment = New DataGridView()
        detSel = New DataGridViewCheckBoxColumn()
        InvNo = New DataGridViewTextBoxColumn()
        TGPID = New DataGridViewTextBoxColumn()
        TstCPT = New DataGridViewTextBoxColumn()
        Paid = New DataGridViewTextBoxColumn()
        Refund = New DataGridViewTextBoxColumn()
        Label3 = New Label()
        txtCheckNo = New TextBox()
        StatusStrip1 = New StatusStrip()
        txtPaymentID = New TextBox()
        PmtLookUp = New Button()
        btnRefAll = New Button()
        ClearAll = New Button()
        txtReason = New TextBox()
        Label5 = New Label()
        ToolStrip1.SuspendLayout()
        CType(dgvInvoices, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvPayment, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {chkEditNew, ToolStripButton2, btnSave, ToolStripSeparator1, btnDelete, ToolStripSeparator2, btnCancel, ToolStripSeparator3, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(1033, 34)
        ToolStrip1.TabIndex = 3
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' chkEditNew
        ' 
        chkEditNew.CheckOnClick = True
        chkEditNew.Image = CType(resources.GetObject("chkEditNew.Image"), Image)
        chkEditNew.ImageTransparentColor = Color.Magenta
        chkEditNew.Name = "chkEditNew"
        chkEditNew.Size = New Size(75, 29)
        chkEditNew.Text = "New"
        ' 
        ' ToolStripButton2
        ' 
        ToolStripButton2.Name = "ToolStripButton2"
        ToolStripButton2.Size = New Size(6, 34)
        ' 
        ' btnSave
        ' 
        btnSave.Enabled = False
        btnSave.Image = CType(resources.GetObject("btnSave.Image"), Image)
        btnSave.ImageTransparentColor = Color.Magenta
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(77, 29)
        btnSave.Text = "Save"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
        ' 
        ' btnDelete
        ' 
        btnDelete.Enabled = False
        btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), Image)
        btnDelete.ImageTransparentColor = Color.Magenta
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(90, 29)
        btnDelete.Text = "Delete"
        btnDelete.ToolTipText = "A click of this button will delete the debit transaction"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 34)
        ' 
        ' btnCancel
        ' 
        btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), Image)
        btnCancel.ImageTransparentColor = Color.Magenta
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(91, 29)
        btnCancel.Text = "Cancel"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(6, 34)
        ' 
        ' btnHelp
        ' 
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.ImageTransparentColor = Color.Magenta
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(77, 29)
        btnHelp.Text = "Help"
        ' 
        ' Label13
        ' 
        Label13.Location = New Point(20, 79)
        Label13.Margin = New Padding(5, 0, 5, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(142, 29)
        Label13.TabIndex = 57
        Label13.Text = "ID"
        Label13.TextAlign = ContentAlignment.TopCenter
        ' 
        ' btnRefLookUp
        ' 
        btnRefLookUp.Enabled = False
        btnRefLookUp.Image = CType(resources.GetObject("btnRefLookUp.Image"), Image)
        btnRefLookUp.Location = New Point(172, 110)
        btnRefLookUp.Margin = New Padding(5, 6, 5, 6)
        btnRefLookUp.Name = "btnRefLookUp"
        btnRefLookUp.Size = New Size(52, 48)
        btnRefLookUp.TabIndex = 53
        btnRefLookUp.UseVisualStyleBackColor = True
        ' 
        ' txtID
        ' 
        txtID.Location = New Point(20, 115)
        txtID.Margin = New Padding(5, 6, 5, 6)
        txtID.MaxLength = 12
        txtID.Name = "txtID"
        txtID.ReadOnly = True
        txtID.Size = New Size(139, 31)
        txtID.TabIndex = 52
        txtID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label4
        ' 
        Label4.Location = New Point(490, 79)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(113, 29)
        Label4.TabIndex = 56
        Label4.Text = "Document #"
        ' 
        ' txtDoc
        ' 
        txtDoc.Location = New Point(465, 115)
        txtDoc.Margin = New Padding(5, 6, 5, 6)
        txtDoc.MaxLength = 100
        txtDoc.Name = "txtDoc"
        txtDoc.ReadOnly = True
        txtDoc.Size = New Size(344, 31)
        txtDoc.TabIndex = 54
        ' 
        ' Label2
        ' 
        Label2.Location = New Point(40, 181)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(235, 31)
        Label2.TabIndex = 55
        Label2.Text = "Payer Identification"
        ' 
        ' txtARName
        ' 
        txtARName.Location = New Point(20, 215)
        txtARName.Margin = New Padding(5, 6, 5, 6)
        txtARName.Name = "txtARName"
        txtARName.ReadOnly = True
        txtARName.Size = New Size(364, 31)
        txtARName.TabIndex = 51
        ' 
        ' Label1
        ' 
        Label1.Location = New Point(233, 79)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(153, 29)
        Label1.TabIndex = 50
        Label1.Text = "Payment ID"
        ' 
        ' dtpRefundDate
        ' 
        dtpRefundDate.Format = DateTimePickerFormat.Short
        dtpRefundDate.Location = New Point(822, 115)
        dtpRefundDate.Margin = New Padding(5, 6, 5, 6)
        dtpRefundDate.Name = "dtpRefundDate"
        dtpRefundDate.Size = New Size(199, 31)
        dtpRefundDate.TabIndex = 62
        ' 
        ' Label6
        ' 
        Label6.Location = New Point(737, 181)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(137, 31)
        Label6.TabIndex = 61
        Label6.Text = "Refund Amount"
        ' 
        ' txtAmount
        ' 
        txtAmount.Location = New Point(737, 215)
        txtAmount.Margin = New Padding(5, 6, 5, 6)
        txtAmount.MaxLength = 12
        txtAmount.Name = "txtAmount"
        txtAmount.ReadOnly = True
        txtAmount.Size = New Size(134, 31)
        txtAmount.TabIndex = 59
        txtAmount.TextAlign = HorizontalAlignment.Center
        ' 
        ' lblDated
        ' 
        lblDated.Location = New Point(842, 79)
        lblDated.Margin = New Padding(5, 0, 5, 0)
        lblDated.Name = "lblDated"
        lblDated.Size = New Size(160, 29)
        lblDated.TabIndex = 60
        lblDated.Text = "Debit Date"
        ' 
        ' dgvInvoices
        ' 
        dgvInvoices.AllowUserToAddRows = False
        dgvInvoices.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.Honeydew
        dgvInvoices.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvInvoices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvInvoices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvInvoices.Columns.AddRange(New DataGridViewColumn() {InvoiceID, AccID, PatName, SvcDate, PmtAmt, TotalRefund})
        dgvInvoices.Location = New Point(20, 287)
        dgvInvoices.Margin = New Padding(5, 6, 5, 6)
        dgvInvoices.Name = "dgvInvoices"
        dgvInvoices.RowHeadersVisible = False
        dgvInvoices.RowHeadersWidth = 62
        dgvInvoices.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvInvoices.Size = New Size(1003, 260)
        dgvInvoices.TabIndex = 63
        ' 
        ' InvoiceID
        ' 
        InvoiceID.FillWeight = 70F
        InvoiceID.HeaderText = "Inv ID"
        InvoiceID.MaxInputLength = 18
        InvoiceID.MinimumWidth = 8
        InvoiceID.Name = "InvoiceID"
        ' 
        ' AccID
        ' 
        AccID.FillWeight = 80F
        AccID.HeaderText = "Acc ID"
        AccID.MaxInputLength = 18
        AccID.MinimumWidth = 8
        AccID.Name = "AccID"
        AccID.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' PatName
        ' 
        PatName.FillWeight = 148F
        PatName.HeaderText = "Patient Name"
        PatName.MaxInputLength = 80
        PatName.MinimumWidth = 8
        PatName.Name = "PatName"
        PatName.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' SvcDate
        ' 
        SvcDate.FillWeight = 80F
        SvcDate.HeaderText = "Svc Date"
        SvcDate.MaxInputLength = 12
        SvcDate.MinimumWidth = 8
        SvcDate.Name = "SvcDate"
        SvcDate.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' PmtAmt
        ' 
        PmtAmt.FillWeight = 96F
        PmtAmt.HeaderText = "Paid Amount"
        PmtAmt.MinimumWidth = 8
        PmtAmt.Name = "PmtAmt"
        PmtAmt.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' TotalRefund
        ' 
        TotalRefund.HeaderText = "Refund"
        TotalRefund.MinimumWidth = 8
        TotalRefund.Name = "TotalRefund"
        TotalRefund.ReadOnly = True
        TotalRefund.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' dgvPayment
        ' 
        dgvPayment.AllowUserToAddRows = False
        dgvPayment.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = Color.FloralWhite
        dgvPayment.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        dgvPayment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvPayment.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvPayment.Columns.AddRange(New DataGridViewColumn() {detSel, InvNo, TGPID, TstCPT, Paid, Refund})
        dgvPayment.Location = New Point(20, 583)
        dgvPayment.Margin = New Padding(5, 6, 5, 6)
        dgvPayment.Name = "dgvPayment"
        dgvPayment.RowHeadersVisible = False
        dgvPayment.RowHeadersWidth = 62
        dgvPayment.Size = New Size(952, 321)
        dgvPayment.TabIndex = 64
        ' 
        ' detSel
        ' 
        detSel.FillWeight = 40F
        detSel.HeaderText = ""
        detSel.MinimumWidth = 8
        detSel.Name = "detSel"
        ' 
        ' InvNo
        ' 
        InvNo.FillWeight = 70F
        InvNo.HeaderText = "Inv No"
        InvNo.MinimumWidth = 8
        InvNo.Name = "InvNo"
        InvNo.ReadOnly = True
        InvNo.SortMode = DataGridViewColumnSortMode.NotSortable
        InvNo.Visible = False
        ' 
        ' TGPID
        ' 
        TGPID.FillWeight = 90F
        TGPID.HeaderText = "TGPID"
        TGPID.MinimumWidth = 8
        TGPID.Name = "TGPID"
        TGPID.ReadOnly = True
        TGPID.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' TstCPT
        ' 
        TstCPT.FillWeight = 260F
        TstCPT.HeaderText = "Test/CPT"
        TstCPT.MinimumWidth = 8
        TstCPT.Name = "TstCPT"
        TstCPT.ReadOnly = True
        TstCPT.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Paid
        ' 
        Paid.FillWeight = 90F
        Paid.HeaderText = "Paid"
        Paid.MinimumWidth = 8
        Paid.Name = "Paid"
        Paid.ReadOnly = True
        Paid.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Refund
        ' 
        Refund.FillWeight = 90F
        Refund.HeaderText = "Refund"
        Refund.MinimumWidth = 8
        Refund.Name = "Refund"
        Refund.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(883, 181)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(140, 31)
        Label3.TabIndex = 65
        Label3.Text = "My Check #"
        ' 
        ' txtCheckNo
        ' 
        txtCheckNo.Location = New Point(883, 215)
        txtCheckNo.Margin = New Padding(5, 6, 5, 6)
        txtCheckNo.MaxLength = 50
        txtCheckNo.Name = "txtCheckNo"
        txtCheckNo.Size = New Size(137, 31)
        txtCheckNo.TabIndex = 66
        txtCheckNo.TextAlign = HorizontalAlignment.Center
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.ImageScalingSize = New Size(24, 24)
        StatusStrip1.Location = New Point(0, 920)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Padding = New Padding(2, 0, 23, 0)
        StatusStrip1.Size = New Size(1033, 22)
        StatusStrip1.TabIndex = 67
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' txtPaymentID
        ' 
        txtPaymentID.Location = New Point(233, 115)
        txtPaymentID.Margin = New Padding(5, 6, 5, 6)
        txtPaymentID.Name = "txtPaymentID"
        txtPaymentID.ReadOnly = True
        txtPaymentID.Size = New Size(151, 31)
        txtPaymentID.TabIndex = 68
        ' 
        ' PmtLookUp
        ' 
        PmtLookUp.Image = CType(resources.GetObject("PmtLookUp.Image"), Image)
        PmtLookUp.Location = New Point(398, 110)
        PmtLookUp.Margin = New Padding(5, 6, 5, 6)
        PmtLookUp.Name = "PmtLookUp"
        PmtLookUp.Size = New Size(52, 48)
        PmtLookUp.TabIndex = 69
        PmtLookUp.UseVisualStyleBackColor = True
        ' 
        ' btnRefAll
        ' 
        btnRefAll.Enabled = False
        btnRefAll.Image = CType(resources.GetObject("btnRefAll.Image"), Image)
        btnRefAll.Location = New Point(982, 621)
        btnRefAll.Margin = New Padding(5, 6, 5, 6)
        btnRefAll.Name = "btnRefAll"
        btnRefAll.Size = New Size(42, 42)
        btnRefAll.TabIndex = 70
        btnRefAll.UseVisualStyleBackColor = True
        ' 
        ' ClearAll
        ' 
        ClearAll.Enabled = False
        ClearAll.Image = CType(resources.GetObject("ClearAll.Image"), Image)
        ClearAll.Location = New Point(982, 813)
        ClearAll.Margin = New Padding(5, 6, 5, 6)
        ClearAll.Name = "ClearAll"
        ClearAll.Size = New Size(42, 42)
        ClearAll.TabIndex = 71
        ClearAll.UseVisualStyleBackColor = True
        ' 
        ' txtReason
        ' 
        txtReason.Location = New Point(398, 215)
        txtReason.Margin = New Padding(5, 6, 5, 6)
        txtReason.MaxLength = 400
        txtReason.Name = "txtReason"
        txtReason.Size = New Size(326, 31)
        txtReason.TabIndex = 72
        ' 
        ' Label5
        ' 
        Label5.Location = New Point(413, 181)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(235, 31)
        Label5.TabIndex = 73
        Label5.Text = "Reason"
        ' 
        ' frmRefund
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1033, 942)
        Controls.Add(Label5)
        Controls.Add(txtReason)
        Controls.Add(ClearAll)
        Controls.Add(btnRefAll)
        Controls.Add(PmtLookUp)
        Controls.Add(txtPaymentID)
        Controls.Add(StatusStrip1)
        Controls.Add(txtCheckNo)
        Controls.Add(Label3)
        Controls.Add(dgvPayment)
        Controls.Add(dgvInvoices)
        Controls.Add(dtpRefundDate)
        Controls.Add(Label6)
        Controls.Add(txtAmount)
        Controls.Add(lblDated)
        Controls.Add(Label13)
        Controls.Add(btnRefLookUp)
        Controls.Add(txtID)
        Controls.Add(Label4)
        Controls.Add(txtDoc)
        Controls.Add(Label2)
        Controls.Add(txtARName)
        Controls.Add(Label1)
        Controls.Add(ToolStrip1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MaximumSize = New Size(1055, 998)
        MinimumSize = New Size(1055, 998)
        Name = "frmRefund"
        Text = "Refund Management"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgvInvoices, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvPayment, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents chkEditNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnRefLookUp As System.Windows.Forms.Button
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDoc As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtARName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpRefundDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents lblDated As System.Windows.Forms.Label
    Friend WithEvents dgvInvoices As System.Windows.Forms.DataGridView
    Friend WithEvents dgvPayment As System.Windows.Forms.DataGridView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCheckNo As System.Windows.Forms.TextBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents txtPaymentID As System.Windows.Forms.TextBox
    Friend WithEvents PmtLookUp As System.Windows.Forms.Button
    Friend WithEvents detSel As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents InvNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TGPID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TstCPT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Paid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Refund As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnRefAll As System.Windows.Forms.Button
    Friend WithEvents ClearAll As System.Windows.Forms.Button
    Friend WithEvents InvoiceID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AccID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PatName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SvcDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PmtAmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TotalRefund As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtReason As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
