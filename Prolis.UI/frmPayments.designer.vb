<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPayments
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            Try
                MyBase.Dispose(disposing)

            Catch ex As Exception

            End Try
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPayments))
        Dim DataGridViewCellStyle9 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        chkEditNew = New ToolStripButton()
        ToolStripButton2 = New ToolStripSeparator()
        btnSave = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnVoidCK = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        Label1 = New Label()
        cmbARType = New ComboBox()
        txtARName = New TextBox()
        cmbPaymentType = New ComboBox()
        Label2 = New Label()
        Label3 = New Label()
        txtDoc = New TextBox()
        Label4 = New Label()
        lblDated = New Label()
        txtAmount = New TextBox()
        txtID = New TextBox()
        Label6 = New Label()
        dgvPayment = New DataGridView()
        InvNo = New DataGridViewTextBoxColumn()
        TGPID = New DataGridViewTextBoxColumn()
        TstCPT = New DataGridViewTextBoxColumn()
        Billed = New DataGridViewTextBoxColumn()
        Paid = New DataGridViewTextBoxColumn()
        WriteOff = New DataGridViewTextBoxColumn()
        Balance = New DataGridViewTextBoxColumn()
        Reas = New DataGridViewCheckBoxColumn()
        PR = New DataGridViewTextBoxColumn()
        Update1 = New DataGridViewComboBoxColumn()
        existingPR = New DataGridViewTextBoxColumn()
        Label11 = New Label()
        txtCKUnApplied = New TextBox()
        txtCKApplied = New TextBox()
        Label12 = New Label()
        txtArID = New TextBox()
        btnAutoApply = New Button()
        Label13 = New Label()
        txtInvID = New TextBox()
        Label15 = New Label()
        Label17 = New Label()
        gbAcc = New GroupBox()
        Label8 = New Label()
        Label5 = New Label()
        txtInvBal = New TextBox()
        txtInvWO = New TextBox()
        Label20 = New Label()
        txtBilled = New TextBox()
        Label19 = New Label()
        txtInvApplied = New TextBox()
        btnUnApply = New Button()
        Label18 = New Label()
        txtInvUnApplied = New TextBox()
        GroupBox1 = New GroupBox()
        OpenFileDialog1 = New OpenFileDialog()
        dtpEOBDate = New DateTimePicker()
        dgvInvoices = New DataGridView()
        InvoiceID = New DataGridViewTextBoxColumn()
        AccID = New DataGridViewTextBoxColumn()
        PatName = New DataGridViewTextBoxColumn()
        SvcDate = New DataGridViewTextBoxColumn()
        InvAmt = New DataGridViewTextBoxColumn()
        dgvComments = New DataGridView()
        Dated = New DataGridViewTextBoxColumn()
        ABP = New DataGridViewTextBoxColumn()
        Cmnt = New DataGridViewTextBoxColumn()
        User = New DataGridViewTextBoxColumn()
        ToolTip1 = New ToolTip(components)
        deleteeob = New Button()
        Label10 = New Label()
        Label9 = New Label()
        btnDelInvPmt = New Button()
        clipboardMsg = New Label()
        newInv = New CheckBox()
        ViewEra = New Button()
        AttachEOB = New Button()
        eobPath = New TextBox()
        Label14 = New Label()
        mtxtPostDate = New MaskedTextBox()
        OrgClaimNumber = New TextBox()
        VoidClaim = New CheckBox()
        Corrected = New CheckBox()
        Label7 = New Label()
        txtReason = New TextBox()
        btnInvLookUp = New Button()
        btnPayLookUp = New Button()
        btnARLookUp = New Button()
        ToolStrip1.SuspendLayout()
        CType(dgvPayment, ComponentModel.ISupportInitialize).BeginInit()
        gbAcc.SuspendLayout()
        GroupBox1.SuspendLayout()
        CType(dgvInvoices, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvComments, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(20, 20)
        ToolStrip1.Items.AddRange(New ToolStripItem() {chkEditNew, ToolStripButton2, btnSave, ToolStripSeparator1, btnVoidCK, ToolStripSeparator2, btnCancel, ToolStripSeparator3, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(1363, 34)
        ToolStrip1.TabIndex = 2
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' chkEditNew
        ' 
        chkEditNew.AutoSize = False
        chkEditNew.CheckOnClick = True
        chkEditNew.Image = CType(resources.GetObject("chkEditNew.Image"), Image)
        chkEditNew.ImageTransparentColor = Color.Magenta
        chkEditNew.Name = "chkEditNew"
        chkEditNew.Size = New Size(60, 22)
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
        btnSave.Size = New Size(73, 29)
        btnSave.Text = "Save"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
        ' 
        ' btnVoidCK
        ' 
        btnVoidCK.AutoSize = False
        btnVoidCK.Enabled = False
        btnVoidCK.Image = CType(resources.GetObject("btnVoidCK.Image"), Image)
        btnVoidCK.ImageTransparentColor = Color.Magenta
        btnVoidCK.Name = "btnVoidCK"
        btnVoidCK.Size = New Size(118, 28)
        btnVoidCK.Text = "Void Cheque"
        btnVoidCK.ToolTipText = "Void Check by deleting the entire payment"
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
        btnCancel.Size = New Size(87, 29)
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
        btnHelp.Size = New Size(73, 29)
        btnHelp.Text = "Help"
        ' 
        ' Label1
        ' 
        Label1.Location = New Point(52, 63)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(105, 31)
        Label1.TabIndex = 3
        Label1.Text = "AR Type_"
        ' 
        ' cmbARType
        ' 
        cmbARType.DropDownStyle = ComboBoxStyle.DropDownList
        cmbARType.FormattingEnabled = True
        cmbARType.Items.AddRange(New Object() {"Client", "Third Party", "Patient"})
        cmbARType.Location = New Point(27, 98)
        cmbARType.Margin = New Padding(5, 6, 5, 6)
        cmbARType.Name = "cmbARType"
        cmbARType.Size = New Size(151, 33)
        cmbARType.TabIndex = 1
        ' 
        ' txtARName
        ' 
        txtARName.Location = New Point(640, 98)
        txtARName.Margin = New Padding(5, 6, 5, 6)
        txtARName.Name = "txtARName"
        txtARName.ReadOnly = True
        txtARName.Size = New Size(304, 31)
        txtARName.TabIndex = 4
        ' 
        ' cmbPaymentType
        ' 
        cmbPaymentType.DropDownStyle = ComboBoxStyle.DropDownList
        cmbPaymentType.FormattingEnabled = True
        cmbPaymentType.Items.AddRange(New Object() {"Cash", "Cheque/EOB", "Credit Card", "835 Transaction"})
        cmbPaymentType.Location = New Point(1127, 98)
        cmbPaymentType.Margin = New Padding(5, 6, 5, 6)
        cmbPaymentType.Name = "cmbPaymentType"
        cmbPaymentType.Size = New Size(151, 33)
        cmbPaymentType.TabIndex = 7
        ' 
        ' Label2
        ' 
        Label2.Location = New Point(650, 63)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(257, 31)
        Label2.TabIndex = 8
        Label2.Text = "Payer Identification"
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(1130, 63)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(140, 31)
        Label3.TabIndex = 9
        Label3.Text = "Payment Type"
        ' 
        ' txtDoc
        ' 
        txtDoc.Location = New Point(188, 98)
        txtDoc.Margin = New Padding(5, 6, 5, 6)
        txtDoc.MaxLength = 500
        txtDoc.Name = "txtDoc"
        txtDoc.Size = New Size(182, 31)
        txtDoc.TabIndex = 8
        ' 
        ' Label4
        ' 
        Label4.Location = New Point(203, 62)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(125, 31)
        Label4.TabIndex = 11
        Label4.Text = "Document #"
        ' 
        ' lblDated
        ' 
        lblDated.Location = New Point(27, 154)
        lblDated.Margin = New Padding(5, 0, 5, 0)
        lblDated.Name = "lblDated"
        lblDated.Size = New Size(162, 31)
        lblDated.TabIndex = 13
        lblDated.Text = "Cheque/EOB Date"
        ' 
        ' txtAmount
        ' 
        txtAmount.Location = New Point(188, 190)
        txtAmount.Margin = New Padding(5, 6, 5, 6)
        txtAmount.MaxLength = 12
        txtAmount.Name = "txtAmount"
        txtAmount.Size = New Size(182, 31)
        txtAmount.TabIndex = 10
        txtAmount.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtID
        ' 
        txtID.Location = New Point(957, 100)
        txtID.Margin = New Padding(5, 6, 5, 6)
        txtID.MaxLength = 12
        txtID.Name = "txtID"
        txtID.ReadOnly = True
        txtID.Size = New Size(157, 31)
        txtID.TabIndex = 5
        txtID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label6
        ' 
        Label6.Location = New Point(203, 154)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(153, 31)
        Label6.TabIndex = 17
        Label6.Text = "Cheque Amount"
        ' 
        ' dgvPayment
        ' 
        dgvPayment.AllowUserToAddRows = False
        dgvPayment.AllowUserToDeleteRows = False
        DataGridViewCellStyle9.BackColor = Color.AliceBlue
        dgvPayment.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle9
        dgvPayment.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvPayment.Columns.AddRange(New DataGridViewColumn() {InvNo, TGPID, TstCPT, Billed, Paid, WriteOff, Balance, Reas, PR, Update1, existingPR})
        dgvPayment.Location = New Point(25, 487)
        dgvPayment.Margin = New Padding(5, 6, 5, 6)
        dgvPayment.Name = "dgvPayment"
        dgvPayment.RowHeadersVisible = False
        dgvPayment.RowHeadersWidth = 51
        dgvPayment.Size = New Size(1323, 283)
        dgvPayment.TabIndex = 30
        ' 
        ' InvNo
        ' 
        InvNo.FillWeight = 70F
        InvNo.HeaderText = "Inv No"
        InvNo.MinimumWidth = 6
        InvNo.Name = "InvNo"
        InvNo.ReadOnly = True
        InvNo.SortMode = DataGridViewColumnSortMode.NotSortable
        InvNo.Visible = False
        InvNo.Width = 70
        ' 
        ' TGPID
        ' 
        TGPID.FillWeight = 70F
        TGPID.HeaderText = "TGPID"
        TGPID.MinimumWidth = 6
        TGPID.Name = "TGPID"
        TGPID.ReadOnly = True
        TGPID.Width = 112
        ' 
        ' TstCPT
        ' 
        TstCPT.FillWeight = 115F
        TstCPT.HeaderText = "Test/CPT"
        TstCPT.MinimumWidth = 6
        TstCPT.Name = "TstCPT"
        TstCPT.ReadOnly = True
        TstCPT.Width = 184
        ' 
        ' Billed
        ' 
        Billed.FillWeight = 80F
        Billed.HeaderText = "Amount"
        Billed.MinimumWidth = 6
        Billed.Name = "Billed"
        Billed.ReadOnly = True
        Billed.SortMode = DataGridViewColumnSortMode.NotSortable
        Billed.Width = 128
        ' 
        ' Paid
        ' 
        Paid.FillWeight = 80F
        Paid.HeaderText = "Paid"
        Paid.MaxInputLength = 9
        Paid.MinimumWidth = 6
        Paid.Name = "Paid"
        Paid.SortMode = DataGridViewColumnSortMode.NotSortable
        Paid.Width = 128
        ' 
        ' WriteOff
        ' 
        WriteOff.FillWeight = 80F
        WriteOff.HeaderText = "WO"
        WriteOff.MinimumWidth = 6
        WriteOff.Name = "WriteOff"
        WriteOff.Resizable = DataGridViewTriState.True
        WriteOff.SortMode = DataGridViewColumnSortMode.NotSortable
        WriteOff.Width = 128
        ' 
        ' Balance
        ' 
        Balance.FillWeight = 80F
        Balance.HeaderText = "Bal"
        Balance.MinimumWidth = 6
        Balance.Name = "Balance"
        Balance.ReadOnly = True
        Balance.SortMode = DataGridViewColumnSortMode.NotSortable
        Balance.Width = 128
        ' 
        ' Reas
        ' 
        Reas.FillWeight = 50F
        Reas.HeaderText = "Bill PR"
        Reas.MinimumWidth = 6
        Reas.Name = "Reas"
        Reas.Resizable = DataGridViewTriState.True
        Reas.Width = 80
        ' 
        ' PR
        ' 
        PR.FillWeight = 80F
        PR.HeaderText = "PR"
        PR.MinimumWidth = 6
        PR.Name = "PR"
        PR.Resizable = DataGridViewTriState.True
        PR.SortMode = DataGridViewColumnSortMode.NotSortable
        PR.Width = 128
        ' 
        ' Update1
        ' 
        Update1.FillWeight = 90F
        Update1.HeaderText = ""
        Update1.Items.AddRange(New Object() {"Process", "Delete", "Reverse"})
        Update1.MinimumWidth = 6
        Update1.Name = "Update1"
        Update1.Resizable = DataGridViewTriState.True
        Update1.Width = 144
        ' 
        ' existingPR
        ' 
        existingPR.HeaderText = "PR"
        existingPR.MinimumWidth = 6
        existingPR.Name = "existingPR"
        existingPR.ReadOnly = True
        existingPR.Width = 160
        ' 
        ' Label11
        ' 
        Label11.Location = New Point(155, 27)
        Label11.Margin = New Padding(5, 0, 5, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(117, 31)
        Label11.TabIndex = 30
        Label11.Text = "UnApplied"
        ' 
        ' txtCKUnApplied
        ' 
        txtCKUnApplied.ForeColor = Color.Red
        txtCKUnApplied.Location = New Point(153, 63)
        txtCKUnApplied.Margin = New Padding(5, 6, 5, 6)
        txtCKUnApplied.MaxLength = 12
        txtCKUnApplied.Name = "txtCKUnApplied"
        txtCKUnApplied.ReadOnly = True
        txtCKUnApplied.Size = New Size(127, 31)
        txtCKUnApplied.TabIndex = 27
        txtCKUnApplied.TextAlign = HorizontalAlignment.Right
        ' 
        ' txtCKApplied
        ' 
        txtCKApplied.ForeColor = Color.Green
        txtCKApplied.Location = New Point(13, 63)
        txtCKApplied.Margin = New Padding(5, 6, 5, 6)
        txtCKApplied.MaxLength = 12
        txtCKApplied.Name = "txtCKApplied"
        txtCKApplied.ReadOnly = True
        txtCKApplied.Size = New Size(127, 31)
        txtCKApplied.TabIndex = 26
        txtCKApplied.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label12
        ' 
        Label12.Location = New Point(10, 29)
        Label12.Margin = New Padding(5, 0, 5, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(135, 31)
        Label12.TabIndex = 32
        Label12.Text = "Applied Amount"
        ' 
        ' txtArID
        ' 
        txtArID.Location = New Point(445, 98)
        txtArID.Margin = New Padding(5, 6, 5, 6)
        txtArID.MaxLength = 12
        txtArID.Name = "txtArID"
        txtArID.Size = New Size(127, 31)
        txtArID.TabIndex = 2
        txtArID.TextAlign = HorizontalAlignment.Center
        ' 
        ' btnAutoApply
        ' 
        btnAutoApply.Enabled = False
        btnAutoApply.ForeColor = Color.Red
        btnAutoApply.Location = New Point(497, 260)
        btnAutoApply.Margin = New Padding(5, 6, 5, 6)
        btnAutoApply.Name = "btnAutoApply"
        btnAutoApply.Size = New Size(72, 79)
        btnAutoApply.TabIndex = 14
        btnAutoApply.TabStop = False
        btnAutoApply.Text = "Auto Apply"
        btnAutoApply.TextImageRelation = TextImageRelation.ImageBeforeText
        btnAutoApply.UseVisualStyleBackColor = True
        ' 
        ' Label13
        ' 
        Label13.Location = New Point(967, 63)
        Label13.Margin = New Padding(5, 0, 5, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(132, 29)
        Label13.TabIndex = 36
        Label13.Text = "Transaction ID"
        ' 
        ' txtInvID
        ' 
        txtInvID.Location = New Point(385, 190)
        txtInvID.Margin = New Padding(5, 6, 5, 6)
        txtInvID.MaxLength = 12
        txtInvID.Name = "txtInvID"
        txtInvID.Size = New Size(132, 31)
        txtInvID.TabIndex = 12
        ' 
        ' Label15
        ' 
        Label15.Location = New Point(395, 154)
        Label15.Margin = New Padding(5, 0, 5, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(125, 31)
        Label15.TabIndex = 44
        Label15.Text = "Invoice ID"
        ' 
        ' Label17
        ' 
        Label17.Location = New Point(447, 63)
        Label17.Margin = New Padding(5, 0, 5, 0)
        Label17.Name = "Label17"
        Label17.Size = New Size(130, 31)
        Label17.TabIndex = 46
        Label17.Text = "AR ID"
        Label17.TextAlign = ContentAlignment.TopCenter
        ' 
        ' gbAcc
        ' 
        gbAcc.Controls.Add(Label8)
        gbAcc.Controls.Add(Label5)
        gbAcc.Controls.Add(txtInvBal)
        gbAcc.Controls.Add(txtInvWO)
        gbAcc.Controls.Add(Label20)
        gbAcc.Controls.Add(txtBilled)
        gbAcc.Controls.Add(Label19)
        gbAcc.Controls.Add(txtInvApplied)
        gbAcc.Controls.Add(btnUnApply)
        gbAcc.Location = New Point(25, 777)
        gbAcc.Margin = New Padding(5, 6, 5, 6)
        gbAcc.Name = "gbAcc"
        gbAcc.Padding = New Padding(5, 6, 5, 6)
        gbAcc.Size = New Size(733, 117)
        gbAcc.TabIndex = 48
        gbAcc.TabStop = False
        gbAcc.Text = "Invoice"
        ' 
        ' Label8
        ' 
        Label8.Location = New Point(467, 33)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(117, 29)
        Label8.TabIndex = 48
        Label8.Text = "Balance"
        ' 
        ' Label5
        ' 
        Label5.Location = New Point(318, 31)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(122, 29)
        Label5.TabIndex = 47
        Label5.Text = "WO Amount"
        ' 
        ' txtInvBal
        ' 
        txtInvBal.ForeColor = Color.Green
        txtInvBal.Location = New Point(450, 65)
        txtInvBal.Margin = New Padding(5, 6, 5, 6)
        txtInvBal.MaxLength = 12
        txtInvBal.Name = "txtInvBal"
        txtInvBal.ReadOnly = True
        txtInvBal.Size = New Size(131, 31)
        txtInvBal.TabIndex = 46
        txtInvBal.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtInvWO
        ' 
        txtInvWO.ForeColor = Color.Green
        txtInvWO.Location = New Point(307, 65)
        txtInvWO.Margin = New Padding(5, 6, 5, 6)
        txtInvWO.MaxLength = 12
        txtInvWO.Name = "txtInvWO"
        txtInvWO.ReadOnly = True
        txtInvWO.Size = New Size(131, 31)
        txtInvWO.TabIndex = 45
        txtInvWO.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label20
        ' 
        Label20.Location = New Point(25, 31)
        Label20.Margin = New Padding(5, 0, 5, 0)
        Label20.Name = "Label20"
        Label20.Size = New Size(128, 29)
        Label20.TabIndex = 44
        Label20.Text = "Billed Amount"
        ' 
        ' txtBilled
        ' 
        txtBilled.ForeColor = Color.Black
        txtBilled.Location = New Point(20, 65)
        txtBilled.Margin = New Padding(5, 6, 5, 6)
        txtBilled.MaxLength = 12
        txtBilled.Name = "txtBilled"
        txtBilled.ReadOnly = True
        txtBilled.Size = New Size(131, 31)
        txtBilled.TabIndex = 43
        txtBilled.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label19
        ' 
        Label19.Location = New Point(163, 33)
        Label19.Margin = New Padding(5, 0, 5, 0)
        Label19.Name = "Label19"
        Label19.Size = New Size(145, 29)
        Label19.TabIndex = 42
        Label19.Text = "Applied Amount"
        ' 
        ' txtInvApplied
        ' 
        txtInvApplied.ForeColor = Color.Green
        txtInvApplied.Location = New Point(163, 67)
        txtInvApplied.Margin = New Padding(5, 6, 5, 6)
        txtInvApplied.MaxLength = 12
        txtInvApplied.Name = "txtInvApplied"
        txtInvApplied.ReadOnly = True
        txtInvApplied.Size = New Size(131, 31)
        txtInvApplied.TabIndex = 23
        txtInvApplied.TextAlign = HorizontalAlignment.Center
        ' 
        ' btnUnApply
        ' 
        btnUnApply.Enabled = False
        btnUnApply.Image = CType(resources.GetObject("btnUnApply.Image"), Image)
        btnUnApply.Location = New Point(593, 52)
        btnUnApply.Margin = New Padding(5, 6, 5, 6)
        btnUnApply.Name = "btnUnApply"
        btnUnApply.Size = New Size(118, 54)
        btnUnApply.TabIndex = 25
        btnUnApply.Text = "UnApply"
        btnUnApply.TextImageRelation = TextImageRelation.ImageBeforeText
        btnUnApply.UseVisualStyleBackColor = True
        ' 
        ' Label18
        ' 
        Label18.Location = New Point(385, 244)
        Label18.Margin = New Padding(5, 0, 5, 0)
        Label18.Name = "Label18"
        Label18.Size = New Size(103, 31)
        Label18.TabIndex = 41
        Label18.Text = "Inv Pmt"
        Label18.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtInvUnApplied
        ' 
        txtInvUnApplied.ForeColor = Color.Red
        txtInvUnApplied.Location = New Point(385, 281)
        txtInvUnApplied.Margin = New Padding(5, 6, 5, 6)
        txtInvUnApplied.MaxLength = 12
        txtInvUnApplied.Name = "txtInvUnApplied"
        txtInvUnApplied.Size = New Size(101, 31)
        txtInvUnApplied.TabIndex = 24
        txtInvUnApplied.TextAlign = HorizontalAlignment.Center
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(txtCKUnApplied)
        GroupBox1.Controls.Add(Label11)
        GroupBox1.Controls.Add(txtCKApplied)
        GroupBox1.Controls.Add(Label12)
        GroupBox1.Location = New Point(982, 777)
        GroupBox1.Margin = New Padding(5, 6, 5, 6)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(5, 6, 5, 6)
        GroupBox1.Size = New Size(293, 115)
        GroupBox1.TabIndex = 49
        GroupBox1.TabStop = False
        GroupBox1.Text = "Entire Payment"
        ' 
        ' OpenFileDialog1
        ' 
        OpenFileDialog1.FileName = "OpenFileDialog1"
        ' 
        ' dtpEOBDate
        ' 
        dtpEOBDate.Format = DateTimePickerFormat.Short
        dtpEOBDate.Location = New Point(27, 190)
        dtpEOBDate.Margin = New Padding(5, 6, 5, 6)
        dtpEOBDate.Name = "dtpEOBDate"
        dtpEOBDate.Size = New Size(151, 31)
        dtpEOBDate.TabIndex = 51
        ' 
        ' dgvInvoices
        ' 
        dgvInvoices.AllowUserToAddRows = False
        dgvInvoices.AllowUserToDeleteRows = False
        DataGridViewCellStyle10.BackColor = Color.Honeydew
        dgvInvoices.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle10
        dgvInvoices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvInvoices.Columns.AddRange(New DataGridViewColumn() {InvoiceID, AccID, PatName, SvcDate, InvAmt})
        dgvInvoices.Location = New Point(582, 146)
        dgvInvoices.Margin = New Padding(5, 6, 5, 6)
        dgvInvoices.Name = "dgvInvoices"
        dgvInvoices.ReadOnly = True
        dgvInvoices.RowHeadersVisible = False
        dgvInvoices.RowHeadersWidth = 51
        dgvInvoices.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvInvoices.Size = New Size(767, 283)
        dgvInvoices.TabIndex = 52
        ' 
        ' InvoiceID
        ' 
        InvoiceID.FillWeight = 70F
        InvoiceID.HeaderText = "Inv ID"
        InvoiceID.MaxInputLength = 18
        InvoiceID.MinimumWidth = 6
        InvoiceID.Name = "InvoiceID"
        InvoiceID.ReadOnly = True
        InvoiceID.Width = 130
        ' 
        ' AccID
        ' 
        AccID.FillWeight = 80F
        AccID.HeaderText = "Acc ID"
        AccID.MaxInputLength = 18
        AccID.MinimumWidth = 6
        AccID.Name = "AccID"
        AccID.ReadOnly = True
        AccID.SortMode = DataGridViewColumnSortMode.NotSortable
        AccID.Width = 150
        ' 
        ' PatName
        ' 
        PatName.FillWeight = 110F
        PatName.HeaderText = "Patient Name"
        PatName.MaxInputLength = 80
        PatName.MinimumWidth = 6
        PatName.Name = "PatName"
        PatName.ReadOnly = True
        PatName.SortMode = DataGridViewColumnSortMode.NotSortable
        PatName.Width = 204
        ' 
        ' SvcDate
        ' 
        SvcDate.FillWeight = 80F
        SvcDate.HeaderText = "Svc Date"
        SvcDate.MaxInputLength = 12
        SvcDate.MinimumWidth = 6
        SvcDate.Name = "SvcDate"
        SvcDate.ReadOnly = True
        SvcDate.SortMode = DataGridViewColumnSortMode.NotSortable
        SvcDate.Width = 150
        ' 
        ' InvAmt
        ' 
        InvAmt.FillWeight = 70F
        InvAmt.HeaderText = "Amount"
        InvAmt.MinimumWidth = 6
        InvAmt.Name = "InvAmt"
        InvAmt.ReadOnly = True
        InvAmt.Width = 130
        ' 
        ' dgvComments
        ' 
        dgvComments.AllowUserToAddRows = False
        dgvComments.AllowUserToDeleteRows = False
        DataGridViewCellStyle11.BackColor = Color.LavenderBlush
        dgvComments.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle11
        dgvComments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvComments.Columns.AddRange(New DataGridViewColumn() {Dated, ABP, Cmnt, User})
        dgvComments.Location = New Point(25, 906)
        dgvComments.Margin = New Padding(5, 6, 5, 6)
        dgvComments.Name = "dgvComments"
        dgvComments.RowHeadersVisible = False
        dgvComments.RowHeadersWidth = 51
        DataGridViewCellStyle12.BackColor = Color.MistyRose
        dgvComments.RowsDefaultCellStyle = DataGridViewCellStyle12
        dgvComments.Size = New Size(1323, 150)
        dgvComments.TabIndex = 76
        ' 
        ' Dated
        ' 
        Dated.FillWeight = 80F
        Dated.HeaderText = "Dated"
        Dated.MaxInputLength = 10
        Dated.MinimumWidth = 6
        Dated.Name = "Dated"
        Dated.ReadOnly = True
        Dated.Width = 80
        ' 
        ' ABP
        ' 
        ABP.FillWeight = 30F
        ABP.HeaderText = "ABP"
        ABP.MaxInputLength = 3
        ABP.MinimumWidth = 6
        ABP.Name = "ABP"
        ABP.ReadOnly = True
        ABP.Width = 30
        ' 
        ' Cmnt
        ' 
        Cmnt.FillWeight = 506F
        Cmnt.HeaderText = "Comment"
        Cmnt.MaxInputLength = 500
        Cmnt.MinimumWidth = 6
        Cmnt.Name = "Cmnt"
        Cmnt.SortMode = DataGridViewColumnSortMode.NotSortable
        Cmnt.Width = 506
        ' 
        ' User
        ' 
        User.FillWeight = 105F
        User.HeaderText = "By"
        User.MaxInputLength = 60
        User.MinimumWidth = 6
        User.Name = "User"
        User.Width = 105
        ' 
        ' deleteeob
        ' 
        deleteeob.Image = My.Resources.Resources.icons8_attachment_32
        deleteeob.Location = New Point(332, 350)
        deleteeob.Margin = New Padding(3, 4, 3, 4)
        deleteeob.Name = "deleteeob"
        deleteeob.Size = New Size(60, 62)
        deleteeob.TabIndex = 101
        ToolTip1.SetToolTip(deleteeob, "Click to attach more files")
        deleteeob.UseVisualStyleBackColor = True
        ' 
        ' Label10
        ' 
        Label10.BackColor = SystemColors.Control
        Label10.Image = My.Resources.Resources.paste
        Label10.Location = New Point(478, 146)
        Label10.Name = "Label10"
        Label10.Size = New Size(47, 46)
        Label10.TabIndex = 84
        Label10.Text = "      "
        ToolTip1.SetToolTip(Label10, "Paste")
        ' 
        ' Label9
        ' 
        Label9.BackColor = SystemColors.Control
        Label9.Image = My.Resources.Resources.paste
        Label9.Location = New Point(332, 54)
        Label9.Name = "Label9"
        Label9.Size = New Size(38, 46)
        Label9.TabIndex = 83
        Label9.Text = "      "
        ToolTip1.SetToolTip(Label9, "Paste")
        ' 
        ' btnDelInvPmt
        ' 
        btnDelInvPmt.Enabled = False
        btnDelInvPmt.Image = CType(resources.GetObject("btnDelInvPmt.Image"), Image)
        btnDelInvPmt.Location = New Point(768, 823)
        btnDelInvPmt.Margin = New Padding(5, 6, 5, 6)
        btnDelInvPmt.Name = "btnDelInvPmt"
        btnDelInvPmt.Size = New Size(203, 54)
        btnDelInvPmt.TabIndex = 81
        btnDelInvPmt.Text = "Delete Payment"
        btnDelInvPmt.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(btnDelInvPmt, "Deletes the payment of currently selected invoice")
        btnDelInvPmt.UseVisualStyleBackColor = True
        ' 
        ' clipboardMsg
        ' 
        clipboardMsg.AutoSize = True
        clipboardMsg.Location = New Point(833, 38)
        clipboardMsg.Name = "clipboardMsg"
        clipboardMsg.Size = New Size(0, 25)
        clipboardMsg.TabIndex = 82
        ' 
        ' newInv
        ' 
        newInv.AutoSize = True
        newInv.Location = New Point(25, 450)
        newInv.Margin = New Padding(3, 4, 3, 4)
        newInv.Name = "newInv"
        newInv.Size = New Size(269, 29)
        newInv.TabIndex = 85
        newInv.Text = "Create New Invoice(Re-BILL) ?"
        newInv.UseVisualStyleBackColor = True
        ' 
        ' ViewEra
        ' 
        ViewEra.Location = New Point(25, 350)
        ViewEra.Margin = New Padding(3, 4, 3, 4)
        ViewEra.Name = "ViewEra"
        ViewEra.Size = New Size(152, 62)
        ViewEra.TabIndex = 97
        ViewEra.Text = "View ERA"
        ViewEra.UseVisualStyleBackColor = True
        ' 
        ' AttachEOB
        ' 
        AttachEOB.Location = New Point(188, 350)
        AttachEOB.Margin = New Padding(3, 4, 3, 4)
        AttachEOB.Name = "AttachEOB"
        AttachEOB.Size = New Size(137, 62)
        AttachEOB.TabIndex = 98
        AttachEOB.Text = "Attach EOB"
        AttachEOB.UseVisualStyleBackColor = True
        ' 
        ' eobPath
        ' 
        eobPath.Location = New Point(843, 38)
        eobPath.Margin = New Padding(5, 6, 5, 6)
        eobPath.MaxLength = 12
        eobPath.Name = "eobPath"
        eobPath.ReadOnly = True
        eobPath.Size = New Size(112, 31)
        eobPath.TabIndex = 99
        eobPath.TextAlign = HorizontalAlignment.Center
        eobPath.Visible = False
        ' 
        ' Label14
        ' 
        Label14.Location = New Point(25, 244)
        Label14.Margin = New Padding(5, 0, 5, 0)
        Label14.Name = "Label14"
        Label14.Size = New Size(162, 31)
        Label14.TabIndex = 13
        Label14.Text = "Post Date"
        ' 
        ' mtxtPostDate
        ' 
        mtxtPostDate.Location = New Point(25, 281)
        mtxtPostDate.Margin = New Padding(5, 6, 5, 6)
        mtxtPostDate.Mask = "00/00/0000"
        mtxtPostDate.Name = "mtxtPostDate"
        mtxtPostDate.Size = New Size(147, 31)
        mtxtPostDate.TabIndex = 102
        mtxtPostDate.ValidatingType = GetType(Date)
        ' 
        ' OrgClaimNumber
        ' 
        OrgClaimNumber.ForeColor = Color.Red
        OrgClaimNumber.Location = New Point(1038, 442)
        OrgClaimNumber.Margin = New Padding(5, 6, 5, 6)
        OrgClaimNumber.MaxLength = 12
        OrgClaimNumber.Name = "OrgClaimNumber"
        OrgClaimNumber.Size = New Size(311, 31)
        OrgClaimNumber.TabIndex = 107
        OrgClaimNumber.TextAlign = HorizontalAlignment.Center
        ' 
        ' VoidClaim
        ' 
        VoidClaim.ForeColor = Color.DarkBlue
        VoidClaim.Location = New Point(937, 446)
        VoidClaim.Margin = New Padding(5, 6, 5, 6)
        VoidClaim.Name = "VoidClaim"
        VoidClaim.Size = New Size(73, 35)
        VoidClaim.TabIndex = 106
        VoidClaim.Text = "Void"
        VoidClaim.UseVisualStyleBackColor = True
        ' 
        ' Corrected
        ' 
        Corrected.ForeColor = Color.DarkBlue
        Corrected.Location = New Point(803, 446)
        Corrected.Margin = New Padding(5, 6, 5, 6)
        Corrected.Name = "Corrected"
        Corrected.Size = New Size(127, 35)
        Corrected.TabIndex = 105
        Corrected.Text = "Corrected"
        Corrected.UseVisualStyleBackColor = True
        ' 
        ' Label7
        ' 
        Label7.Location = New Point(343, 450)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(127, 31)
        Label7.TabIndex = 104
        Label7.Text = "Rebill Reason"
        ' 
        ' txtReason
        ' 
        txtReason.Location = New Point(478, 446)
        txtReason.Margin = New Padding(5, 6, 5, 6)
        txtReason.MaxLength = 500
        txtReason.Name = "txtReason"
        txtReason.Size = New Size(304, 31)
        txtReason.TabIndex = 103
        ' 
        ' btnInvLookUp
        ' 
        btnInvLookUp.Image = CType(resources.GetObject("btnInvLookUp.Image"), Image)
        btnInvLookUp.Location = New Point(528, 185)
        btnInvLookUp.Margin = New Padding(5, 6, 5, 6)
        btnInvLookUp.Name = "btnInvLookUp"
        btnInvLookUp.Size = New Size(45, 48)
        btnInvLookUp.TabIndex = 50
        btnInvLookUp.UseVisualStyleBackColor = True
        ' 
        ' btnPayLookUp
        ' 
        btnPayLookUp.Enabled = False
        btnPayLookUp.Image = CType(resources.GetObject("btnPayLookUp.Image"), Image)
        btnPayLookUp.Location = New Point(385, 92)
        btnPayLookUp.Margin = New Padding(5, 6, 5, 6)
        btnPayLookUp.Name = "btnPayLookUp"
        btnPayLookUp.Size = New Size(52, 48)
        btnPayLookUp.TabIndex = 6
        btnPayLookUp.UseVisualStyleBackColor = True
        ' 
        ' btnARLookUp
        ' 
        btnARLookUp.Image = CType(resources.GetObject("btnARLookUp.Image"), Image)
        btnARLookUp.Location = New Point(585, 92)
        btnARLookUp.Margin = New Padding(5, 6, 5, 6)
        btnARLookUp.Name = "btnARLookUp"
        btnARLookUp.Size = New Size(45, 48)
        btnARLookUp.TabIndex = 3
        btnARLookUp.UseVisualStyleBackColor = True
        ' 
        ' frmPayments
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1363, 1069)
        Controls.Add(Label9)
        Controls.Add(Label4)
        Controls.Add(OrgClaimNumber)
        Controls.Add(VoidClaim)
        Controls.Add(Corrected)
        Controls.Add(Label7)
        Controls.Add(txtReason)
        Controls.Add(mtxtPostDate)
        Controls.Add(deleteeob)
        Controls.Add(eobPath)
        Controls.Add(AttachEOB)
        Controls.Add(ViewEra)
        Controls.Add(newInv)
        Controls.Add(Label10)
        Controls.Add(clipboardMsg)
        Controls.Add(btnDelInvPmt)
        Controls.Add(dgvComments)
        Controls.Add(dgvInvoices)
        Controls.Add(dtpEOBDate)
        Controls.Add(btnInvLookUp)
        Controls.Add(GroupBox1)
        Controls.Add(gbAcc)
        Controls.Add(Label17)
        Controls.Add(Label18)
        Controls.Add(txtInvUnApplied)
        Controls.Add(Label15)
        Controls.Add(txtInvID)
        Controls.Add(btnAutoApply)
        Controls.Add(Label13)
        Controls.Add(txtArID)
        Controls.Add(dgvPayment)
        Controls.Add(Label6)
        Controls.Add(btnPayLookUp)
        Controls.Add(txtID)
        Controls.Add(Label14)
        Controls.Add(txtAmount)
        Controls.Add(lblDated)
        Controls.Add(txtDoc)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(cmbPaymentType)
        Controls.Add(txtARName)
        Controls.Add(btnARLookUp)
        Controls.Add(cmbARType)
        Controls.Add(Label1)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        KeyPreview = True
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmPayments"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Payments"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgvPayment, ComponentModel.ISupportInitialize).EndInit()
        gbAcc.ResumeLayout(False)
        gbAcc.PerformLayout()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        CType(dgvInvoices, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvComments, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents chkEditNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnVoidCK As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbARType As System.Windows.Forms.ComboBox
    Friend WithEvents btnARLookUp As System.Windows.Forms.Button
    Friend WithEvents txtARName As System.Windows.Forms.TextBox
    Friend WithEvents cmbPaymentType As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDoc As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblDated As System.Windows.Forms.Label
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents btnPayLookUp As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dgvPayment As System.Windows.Forms.DataGridView
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtCKUnApplied As System.Windows.Forms.TextBox
    Friend WithEvents txtCKApplied As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtArID As System.Windows.Forms.TextBox
    Friend WithEvents btnAutoApply As System.Windows.Forms.Button
    Friend WithEvents btnUnApply As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtInvID As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents gbAcc As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtInvUnApplied As System.Windows.Forms.TextBox
    Friend WithEvents txtInvApplied As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtBilled As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnInvLookUp As System.Windows.Forms.Button
    Friend WithEvents dtpEOBDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dgvInvoices As System.Windows.Forms.DataGridView
    Friend WithEvents InvoiceID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AccID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PatName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SvcDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InvAmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvComments As System.Windows.Forms.DataGridView
    Friend WithEvents Dated As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ABP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cmnt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents User As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtInvBal As System.Windows.Forms.TextBox
    Friend WithEvents txtInvWO As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnDelInvPmt As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents clipboardMsg As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents newInv As System.Windows.Forms.CheckBox
    Friend WithEvents InvNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TGPID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TstCPT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Billed As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Paid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WriteOff As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Balance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Reas As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents PR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Update1 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents existingPR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ViewEra As System.Windows.Forms.Button
    Friend WithEvents AttachEOB As Button
    Friend WithEvents eobPath As TextBox
    Friend WithEvents deleteeob As Button
    Friend WithEvents Label14 As Label
    Friend WithEvents mtxtPostDate As MaskedTextBox
    Friend WithEvents OrgClaimNumber As TextBox
    Friend WithEvents VoidClaim As CheckBox
    Friend WithEvents Corrected As CheckBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txtReason As TextBox
End Class
