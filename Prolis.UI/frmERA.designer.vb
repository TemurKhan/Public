<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmERA
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmERA))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        btnERALookUp = New Button()
        Label1 = New Label()
        dgvClaims = New DataGridView()
        Claim = New DataGridViewLinkColumn()
        PayerCLMID = New DataGridViewTextBoxColumn()
        PatientName = New DataGridViewTextBoxColumn()
        CBilled = New DataGridViewTextBoxColumn()
        PAuth = New DataGridViewTextBoxColumn()
        CPaid = New DataGridViewTextBoxColumn()
        PR = New DataGridViewTextBoxColumn()
        WO = New DataGridViewTextBoxColumn()
        Bal = New DataGridViewTextBoxColumn()
        CLMSusp = New DataGridViewTextBoxColumn()
        ChargeAction = New DataGridViewComboBoxColumn()
        CLMDist = New DataGridViewTextBoxColumn()
        btnApply = New DataGridViewImageColumn()
        Reason = New DataGridViewTextBoxColumn()
        SvcDate = New DataGridViewTextBoxColumn()
        Sel = New DataGridViewCheckBoxColumn()
        dgvClaimDetail = New DataGridView()
        ChargeID = New DataGridViewTextBoxColumn()
        TGPID = New DataGridViewTextBoxColumn()
        TGPName = New DataGridViewTextBoxColumn()
        CPT = New DataGridViewTextBoxColumn()
        Billed = New DataGridViewTextBoxColumn()
        Allowed = New DataGridViewTextBoxColumn()
        Paid = New DataGridViewTextBoxColumn()
        ItemPR = New DataGridViewTextBoxColumn()
        ItemWO = New DataGridViewTextBoxColumn()
        ItemBal = New DataGridViewTextBoxColumn()
        ItemSusp = New DataGridViewTextBoxColumn()
        ReasCode = New DataGridViewTextBoxColumn()
        Action = New DataGridViewComboBoxColumn()
        ItemUpdate = New DataGridViewImageColumn()
        Label4 = New Label()
        Label5 = New Label()
        Label6 = New Label()
        txtERAType = New TextBox()
        btnClear = New Button()
        OpenFileDialog1 = New OpenFileDialog()
        dgvEOBs = New DataGridView()
        Payer_Code = New DataGridViewTextBoxColumn()
        Payer = New DataGridViewTextBoxColumn()
        DocID = New DataGridViewTextBoxColumn()
        DocDate = New DataGridViewTextBoxColumn()
        BillAmt = New DataGridViewTextBoxColumn()
        ChkPmt = New DataGridViewTextBoxColumn()
        Label2 = New Label()
        txtERAFile = New TextBox()
        cmbBillLevel = New ComboBox()
        txtDisc = New TextBox()
        Label3 = New Label()
        Label7 = New Label()
        dgvComments = New DataGridView()
        Dated = New DataGridViewTextBoxColumn()
        ABP = New DataGridViewTextBoxColumn()
        Cmnt = New DataGridViewTextBoxColumn()
        User = New DataGridViewTextBoxColumn()
        txtBillReason = New TextBox()
        Label8 = New Label()
        cmbAction = New ComboBox()
        btnAction = New Button()
        gbCumulative = New GroupBox()
        Label9 = New Label()
        Label10 = New Label()
        Label11 = New Label()
        txtClaims = New TextBox()
        txtUnprocessed = New TextBox()
        txtProcessed = New TextBox()
        PB = New ProgressBar()
        StatusStrip1 = New StatusStrip()
        lblStatus = New ToolStripStatusLabel()
        Label12 = New Label()
        dtpPost = New DateTimePicker()
        Label13 = New Label()
        clipboardMsg = New Label()
        ViewERAbtn = New Button()
        proceesedbtn = New Button()
        documentID = New TextBox()
        lblProgress = New Label()
        CType(dgvClaims, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvClaimDetail, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvEOBs, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvComments, ComponentModel.ISupportInitialize).BeginInit()
        gbCumulative.SuspendLayout()
        StatusStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' btnERALookUp
        ' 
        btnERALookUp.Image = CType(resources.GetObject("btnERALookUp.Image"), Image)
        btnERALookUp.Location = New Point(506, 61)
        btnERALookUp.Margin = New Padding(5, 6, 5, 6)
        btnERALookUp.Name = "btnERALookUp"
        btnERALookUp.Size = New Size(45, 44)
        btnERALookUp.TabIndex = 1
        btnERALookUp.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.Location = New Point(35, 31)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(110, 31)
        Label1.TabIndex = 2
        Label1.Text = "ERA File"
        ' 
        ' dgvClaims
        ' 
        dgvClaims.AllowUserToAddRows = False
        dgvClaims.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(235), CByte(255), CByte(235))
        dgvClaims.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvClaims.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvClaims.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvClaims.Columns.AddRange(New DataGridViewColumn() {Claim, PayerCLMID, PatientName, CBilled, PAuth, CPaid, PR, WO, Bal, CLMSusp, ChargeAction, CLMDist, btnApply, Reason, SvcDate, Sel})
        dgvClaims.Location = New Point(20, 302)
        dgvClaims.Margin = New Padding(5, 6, 5, 6)
        dgvClaims.Name = "dgvClaims"
        dgvClaims.RowHeadersVisible = False
        dgvClaims.RowHeadersWidth = 51
        dgvClaims.Size = New Size(1494, 294)
        dgvClaims.TabIndex = 7
        ' 
        ' Claim
        ' 
        Claim.FillWeight = 80F
        Claim.HeaderText = "Lab Claim"
        Claim.MinimumWidth = 6
        Claim.Name = "Claim"
        Claim.ReadOnly = True
        Claim.Resizable = DataGridViewTriState.True
        Claim.SortMode = DataGridViewColumnSortMode.Automatic
        ' 
        ' PayerCLMID
        ' 
        PayerCLMID.FillWeight = 70F
        PayerCLMID.HeaderText = "Payer Claim"
        PayerCLMID.MinimumWidth = 6
        PayerCLMID.Name = "PayerCLMID"
        PayerCLMID.ReadOnly = True
        PayerCLMID.SortMode = DataGridViewColumnSortMode.NotSortable
        PayerCLMID.Visible = False
        ' 
        ' PatientName
        ' 
        PatientName.FillWeight = 98F
        PatientName.HeaderText = "Patient"
        PatientName.MaxInputLength = 80
        PatientName.MinimumWidth = 6
        PatientName.Name = "PatientName"
        PatientName.ReadOnly = True
        PatientName.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' CBilled
        ' 
        CBilled.FillWeight = 66F
        CBilled.HeaderText = "Billed"
        CBilled.MinimumWidth = 6
        CBilled.Name = "CBilled"
        CBilled.ReadOnly = True
        CBilled.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' PAuth
        ' 
        PAuth.FillWeight = 66F
        PAuth.HeaderText = "Auth"
        PAuth.MinimumWidth = 6
        PAuth.Name = "PAuth"
        PAuth.ReadOnly = True
        PAuth.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' CPaid
        ' 
        CPaid.FillWeight = 66F
        CPaid.HeaderText = "Paid"
        CPaid.MinimumWidth = 6
        CPaid.Name = "CPaid"
        CPaid.ReadOnly = True
        CPaid.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' PR
        ' 
        PR.FillWeight = 66F
        PR.HeaderText = "PR"
        PR.MinimumWidth = 6
        PR.Name = "PR"
        PR.ReadOnly = True
        PR.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' WO
        ' 
        WO.FillWeight = 66F
        WO.HeaderText = "WO"
        WO.MinimumWidth = 6
        WO.Name = "WO"
        WO.ReadOnly = True
        WO.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Bal
        ' 
        Bal.FillWeight = 66F
        Bal.HeaderText = "Bal"
        Bal.MinimumWidth = 6
        Bal.Name = "Bal"
        Bal.ReadOnly = True
        Bal.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' CLMSusp
        ' 
        CLMSusp.FillWeight = 66F
        CLMSusp.HeaderText = "Susp"
        CLMSusp.MinimumWidth = 6
        CLMSusp.Name = "CLMSusp"
        CLMSusp.ReadOnly = True
        CLMSusp.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' ChargeAction
        ' 
        ChargeAction.FillWeight = 90F
        ChargeAction.HeaderText = "Action"
        ChargeAction.Items.AddRange(New Object() {"None", "Bill Patient", "Bill Insurance", "Correction", "Supportives"})
        ChargeAction.MinimumWidth = 6
        ChargeAction.Name = "ChargeAction"
        ChargeAction.Resizable = DataGridViewTriState.True
        ' 
        ' CLMDist
        ' 
        CLMDist.FillWeight = 60F
        CLMDist.HeaderText = "Payer ID"
        CLMDist.MaxInputLength = 12
        CLMDist.MinimumWidth = 6
        CLMDist.Name = "CLMDist"
        CLMDist.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' btnApply
        ' 
        btnApply.FillWeight = 48F
        btnApply.HeaderText = ""
        btnApply.Image = CType(resources.GetObject("btnApply.Image"), Image)
        btnApply.MinimumWidth = 6
        btnApply.Name = "btnApply"
        btnApply.Resizable = DataGridViewTriState.True
        ' 
        ' Reason
        ' 
        Reason.FillWeight = 60F
        Reason.HeaderText = "Reason"
        Reason.MinimumWidth = 6
        Reason.Name = "Reason"
        Reason.ReadOnly = True
        Reason.SortMode = DataGridViewColumnSortMode.NotSortable
        Reason.Visible = False
        ' 
        ' SvcDate
        ' 
        SvcDate.HeaderText = "SvcDate"
        SvcDate.MinimumWidth = 6
        SvcDate.Name = "SvcDate"
        SvcDate.Visible = False
        ' 
        ' Sel
        ' 
        Sel.FillWeight = 30F
        Sel.HeaderText = ""
        Sel.MinimumWidth = 6
        Sel.Name = "Sel"
        ' 
        ' dgvClaimDetail
        ' 
        dgvClaimDetail.AllowUserToAddRows = False
        dgvClaimDetail.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = Color.FromArgb(CByte(218), CByte(255), CByte(255))
        dgvClaimDetail.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        dgvClaimDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvClaimDetail.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvClaimDetail.Columns.AddRange(New DataGridViewColumn() {ChargeID, TGPID, TGPName, CPT, Billed, Allowed, Paid, ItemPR, ItemWO, ItemBal, ItemSusp, ReasCode, Action, ItemUpdate})
        dgvClaimDetail.Location = New Point(20, 747)
        dgvClaimDetail.Margin = New Padding(5, 6, 5, 6)
        dgvClaimDetail.Name = "dgvClaimDetail"
        dgvClaimDetail.RowHeadersVisible = False
        dgvClaimDetail.RowHeadersWidth = 51
        dgvClaimDetail.Size = New Size(1494, 302)
        dgvClaimDetail.TabIndex = 8
        ' 
        ' ChargeID
        ' 
        ChargeID.FillWeight = 80F
        ChargeID.HeaderText = "ChargeID"
        ChargeID.MinimumWidth = 6
        ChargeID.Name = "ChargeID"
        ChargeID.Visible = False
        ' 
        ' TGPID
        ' 
        TGPID.FillWeight = 80F
        TGPID.HeaderText = "TGPID"
        TGPID.MinimumWidth = 6
        TGPID.Name = "TGPID"
        TGPID.ReadOnly = True
        TGPID.SortMode = DataGridViewColumnSortMode.NotSortable
        TGPID.Visible = False
        ' 
        ' TGPName
        ' 
        TGPName.FillWeight = 124F
        TGPName.HeaderText = "Component"
        TGPName.MinimumWidth = 6
        TGPName.Name = "TGPName"
        TGPName.ReadOnly = True
        TGPName.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' CPT
        ' 
        CPT.FillWeight = 66F
        CPT.HeaderText = "CPT"
        CPT.MinimumWidth = 6
        CPT.Name = "CPT"
        CPT.ReadOnly = True
        ' 
        ' Billed
        ' 
        Billed.FillWeight = 70F
        Billed.HeaderText = "Billed"
        Billed.MinimumWidth = 6
        Billed.Name = "Billed"
        Billed.ReadOnly = True
        Billed.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Allowed
        ' 
        Allowed.FillWeight = 70F
        Allowed.HeaderText = "Allowed"
        Allowed.MinimumWidth = 6
        Allowed.Name = "Allowed"
        Allowed.ReadOnly = True
        ' 
        ' Paid
        ' 
        Paid.FillWeight = 70F
        Paid.HeaderText = "Paid"
        Paid.MinimumWidth = 6
        Paid.Name = "Paid"
        Paid.ReadOnly = True
        ' 
        ' ItemPR
        ' 
        ItemPR.FillWeight = 70F
        ItemPR.HeaderText = "PR"
        ItemPR.MinimumWidth = 6
        ItemPR.Name = "ItemPR"
        ItemPR.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' ItemWO
        ' 
        ItemWO.FillWeight = 70F
        ItemWO.HeaderText = "WO"
        ItemWO.MinimumWidth = 6
        ItemWO.Name = "ItemWO"
        ItemWO.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' ItemBal
        ' 
        ItemBal.FillWeight = 70F
        ItemBal.HeaderText = "Balance"
        ItemBal.MinimumWidth = 6
        ItemBal.Name = "ItemBal"
        ItemBal.ReadOnly = True
        ItemBal.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' ItemSusp
        ' 
        ItemSusp.FillWeight = 70F
        ItemSusp.HeaderText = "Susp"
        ItemSusp.MinimumWidth = 6
        ItemSusp.Name = "ItemSusp"
        ItemSusp.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' ReasCode
        ' 
        ReasCode.FillWeight = 60F
        ReasCode.HeaderText = "Code"
        ReasCode.MinimumWidth = 6
        ReasCode.Name = "ReasCode"
        ReasCode.ReadOnly = True
        ReasCode.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Action
        ' 
        Action.FillWeight = 80F
        Action.HeaderText = "Action"
        Action.Items.AddRange(New Object() {"Balance", "Bill Patient", "Bill Insurance", "Correction", "Supportives", "Write Off"})
        Action.MinimumWidth = 6
        Action.Name = "Action"
        ' 
        ' ItemUpdate
        ' 
        ItemUpdate.FillWeight = 40F
        ItemUpdate.HeaderText = ""
        ItemUpdate.Image = CType(resources.GetObject("ItemUpdate.Image"), Image)
        ItemUpdate.MinimumWidth = 6
        ItemUpdate.Name = "ItemUpdate"
        ' 
        ' Label4
        ' 
        Label4.Location = New Point(16, 267)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(160, 31)
        Label4.TabIndex = 9
        Label4.Text = "Claims"
        ' 
        ' Label5
        ' 
        Label5.Location = New Point(25, 709)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(130, 31)
        Label5.TabIndex = 10
        Label5.Text = "Claim Detail"
        ' 
        ' Label6
        ' 
        Label6.Location = New Point(79, 167)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(109, 33)
        Label6.TabIndex = 11
        Label6.Text = "ERA Type"
        Label6.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtERAType
        ' 
        txtERAType.Location = New Point(80, 212)
        txtERAType.Margin = New Padding(5, 6, 5, 6)
        txtERAType.MaxLength = 100
        txtERAType.Name = "txtERAType"
        txtERAType.ReadOnly = True
        txtERAType.Size = New Size(254, 31)
        txtERAType.TabIndex = 12
        txtERAType.TextAlign = HorizontalAlignment.Center
        ' 
        ' btnClear
        ' 
        btnClear.Image = CType(resources.GetObject("btnClear.Image"), Image)
        btnClear.Location = New Point(20, 195)
        btnClear.Margin = New Padding(5, 6, 5, 6)
        btnClear.Name = "btnClear"
        btnClear.Size = New Size(50, 56)
        btnClear.TabIndex = 15
        btnClear.UseVisualStyleBackColor = True
        ' 
        ' OpenFileDialog1
        ' 
        OpenFileDialog1.FileName = "OpenFileDialog1"
        ' 
        ' dgvEOBs
        ' 
        dgvEOBs.AllowUserToAddRows = False
        dgvEOBs.AllowUserToDeleteRows = False
        DataGridViewCellStyle3.BackColor = Color.FromArgb(CByte(255), CByte(230), CByte(230))
        dgvEOBs.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        dgvEOBs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvEOBs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvEOBs.Columns.AddRange(New DataGridViewColumn() {Payer_Code, Payer, DocID, DocDate, BillAmt, ChkPmt})
        dgvEOBs.Location = New Point(561, 67)
        dgvEOBs.Margin = New Padding(5, 6, 5, 6)
        dgvEOBs.Name = "dgvEOBs"
        dgvEOBs.ReadOnly = True
        dgvEOBs.RowHeadersVisible = False
        dgvEOBs.RowHeadersWidth = 51
        dgvEOBs.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvEOBs.Size = New Size(951, 194)
        dgvEOBs.TabIndex = 16
        ' 
        ' Payer_Code
        ' 
        Payer_Code.FillWeight = 66F
        Payer_Code.HeaderText = "Code"
        Payer_Code.MinimumWidth = 6
        Payer_Code.Name = "Payer_Code"
        Payer_Code.ReadOnly = True
        Payer_Code.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Payer
        ' 
        Payer.FillWeight = 125F
        Payer.HeaderText = "Payer"
        Payer.MinimumWidth = 6
        Payer.Name = "Payer"
        Payer.ReadOnly = True
        Payer.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' DocID
        ' 
        DocID.FillWeight = 120F
        DocID.HeaderText = "Document ID"
        DocID.MinimumWidth = 6
        DocID.Name = "DocID"
        DocID.ReadOnly = True
        DocID.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' DocDate
        ' 
        DocDate.FillWeight = 80F
        DocDate.HeaderText = "Dated"
        DocDate.MinimumWidth = 6
        DocDate.Name = "DocDate"
        DocDate.ReadOnly = True
        DocDate.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' BillAmt
        ' 
        BillAmt.FillWeight = 75F
        BillAmt.HeaderText = "Billed"
        BillAmt.MinimumWidth = 6
        BillAmt.Name = "BillAmt"
        BillAmt.ReadOnly = True
        BillAmt.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' ChkPmt
        ' 
        ChkPmt.FillWeight = 75F
        ChkPmt.HeaderText = "Payment"
        ChkPmt.MinimumWidth = 6
        ChkPmt.Name = "ChkPmt"
        ChkPmt.ReadOnly = True
        ChkPmt.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Label2
        ' 
        Label2.Location = New Point(556, 31)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(184, 31)
        Label2.TabIndex = 17
        Label2.Text = "EOB Contents"
        ' 
        ' txtERAFile
        ' 
        txtERAFile.BackColor = SystemColors.ButtonHighlight
        txtERAFile.Location = New Point(20, 67)
        txtERAFile.Margin = New Padding(5, 6, 5, 6)
        txtERAFile.Name = "txtERAFile"
        txtERAFile.ReadOnly = True
        txtERAFile.Size = New Size(474, 31)
        txtERAFile.TabIndex = 19
        ' 
        ' cmbBillLevel
        ' 
        cmbBillLevel.DropDownStyle = ComboBoxStyle.DropDownList
        cmbBillLevel.FormattingEnabled = True
        cmbBillLevel.Items.AddRange(New Object() {"List Price", "Level 1", "Level 2", "Level 3", "Level 4", "Level 5", "Level 6", "Level 7", "Level 8", "Level 9", "Inherited"})
        cmbBillLevel.Location = New Point(536, 630)
        cmbBillLevel.Margin = New Padding(5, 6, 5, 6)
        cmbBillLevel.Name = "cmbBillLevel"
        cmbBillLevel.Size = New Size(159, 33)
        cmbBillLevel.TabIndex = 20
        ' 
        ' txtDisc
        ' 
        txtDisc.Location = New Point(876, 630)
        txtDisc.Margin = New Padding(5, 6, 5, 6)
        txtDisc.MaxLength = 5
        txtDisc.Name = "txtDisc"
        txtDisc.Size = New Size(100, 31)
        txtDisc.TabIndex = 21
        txtDisc.Text = "0.00"
        txtDisc.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(409, 636)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(119, 31)
        Label3.TabIndex = 22
        Label3.Text = "Rebill Level:"
        Label3.TextAlign = ContentAlignment.TopRight
        ' 
        ' Label7
        ' 
        Label7.Location = New Point(709, 636)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(159, 31)
        Label7.TabIndex = 23
        Label7.Text = "Rebill Discount %:"
        Label7.TextAlign = ContentAlignment.TopRight
        ' 
        ' dgvComments
        ' 
        dgvComments.AllowUserToAddRows = False
        dgvComments.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.BackColor = Color.LightCyan
        dgvComments.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        dgvComments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvComments.Columns.AddRange(New DataGridViewColumn() {Dated, ABP, Cmnt, User})
        dgvComments.Location = New Point(20, 1077)
        dgvComments.Margin = New Padding(5, 6, 5, 6)
        dgvComments.Name = "dgvComments"
        dgvComments.RowHeadersVisible = False
        dgvComments.RowHeadersWidth = 51
        DataGridViewCellStyle5.BackColor = Color.MistyRose
        dgvComments.RowsDefaultCellStyle = DataGridViewCellStyle5
        dgvComments.Size = New Size(1494, 167)
        dgvComments.TabIndex = 77
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
        Cmnt.FillWeight = 644F
        Cmnt.HeaderText = "Comment"
        Cmnt.MaxInputLength = 500
        Cmnt.MinimumWidth = 6
        Cmnt.Name = "Cmnt"
        Cmnt.SortMode = DataGridViewColumnSortMode.NotSortable
        Cmnt.Width = 644
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
        ' txtBillReason
        ' 
        txtBillReason.Location = New Point(536, 681)
        txtBillReason.Margin = New Padding(5, 6, 5, 6)
        txtBillReason.MaxLength = 250
        txtBillReason.Name = "txtBillReason"
        txtBillReason.Size = New Size(440, 31)
        txtBillReason.TabIndex = 78
        ' 
        ' Label8
        ' 
        Label8.Location = New Point(386, 688)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(140, 31)
        Label8.TabIndex = 79
        Label8.Text = "Reason:"
        Label8.TextAlign = ContentAlignment.TopRight
        ' 
        ' cmbAction
        ' 
        cmbAction.DropDownStyle = ComboBoxStyle.DropDownList
        cmbAction.Enabled = False
        cmbAction.FormattingEnabled = True
        cmbAction.Items.AddRange(New Object() {"Balance", "Bill Patient", "Bill Insurance", "Refund", "Write Off"})
        cmbAction.Location = New Point(10, 42)
        cmbAction.Margin = New Padding(5, 6, 5, 6)
        cmbAction.Name = "cmbAction"
        cmbAction.Size = New Size(183, 33)
        cmbAction.TabIndex = 80
        ' 
        ' btnAction
        ' 
        btnAction.BackColor = SystemColors.ControlLight
        btnAction.Enabled = False
        btnAction.Image = CType(resources.GetObject("btnAction.Image"), Image)
        btnAction.Location = New Point(286, 36)
        btnAction.Margin = New Padding(5, 6, 5, 6)
        btnAction.Name = "btnAction"
        btnAction.Size = New Size(166, 52)
        btnAction.TabIndex = 81
        btnAction.UseVisualStyleBackColor = False
        ' 
        ' gbCumulative
        ' 
        gbCumulative.Controls.Add(btnAction)
        gbCumulative.Controls.Add(cmbAction)
        gbCumulative.Location = New Point(1015, 608)
        gbCumulative.Margin = New Padding(5, 6, 5, 6)
        gbCumulative.Name = "gbCumulative"
        gbCumulative.Padding = New Padding(5, 6, 5, 6)
        gbCumulative.Size = New Size(499, 111)
        gbCumulative.TabIndex = 82
        gbCumulative.TabStop = False
        gbCumulative.Text = "Cumulative command"
        ' 
        ' Label9
        ' 
        Label9.Location = New Point(29, 608)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(99, 31)
        Label9.TabIndex = 83
        Label9.Text = "All Claims"
        Label9.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label10
        ' 
        Label10.Location = New Point(136, 608)
        Label10.Margin = New Padding(5, 0, 5, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(119, 31)
        Label10.TabIndex = 84
        Label10.Text = "Unprocessed"
        Label10.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label11
        ' 
        Label11.Location = New Point(260, 608)
        Label11.Margin = New Padding(5, 0, 5, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(99, 31)
        Label11.TabIndex = 85
        Label11.Text = "Processed"
        Label11.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtClaims
        ' 
        txtClaims.Location = New Point(29, 644)
        txtClaims.Margin = New Padding(5, 6, 5, 6)
        txtClaims.MaxLength = 5
        txtClaims.Name = "txtClaims"
        txtClaims.ReadOnly = True
        txtClaims.Size = New Size(95, 31)
        txtClaims.TabIndex = 86
        txtClaims.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtUnprocessed
        ' 
        txtUnprocessed.Location = New Point(141, 644)
        txtUnprocessed.Margin = New Padding(5, 6, 5, 6)
        txtUnprocessed.MaxLength = 5
        txtUnprocessed.Name = "txtUnprocessed"
        txtUnprocessed.ReadOnly = True
        txtUnprocessed.Size = New Size(95, 31)
        txtUnprocessed.TabIndex = 87
        txtUnprocessed.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtProcessed
        ' 
        txtProcessed.Location = New Point(260, 644)
        txtProcessed.Margin = New Padding(5, 6, 5, 6)
        txtProcessed.MaxLength = 5
        txtProcessed.Name = "txtProcessed"
        txtProcessed.ReadOnly = True
        txtProcessed.Size = New Size(95, 31)
        txtProcessed.TabIndex = 88
        txtProcessed.TextAlign = HorizontalAlignment.Center
        ' 
        ' PB
        ' 
        PB.BackColor = SystemColors.GradientActiveCaption
        PB.Location = New Point(788, 270)
        PB.Margin = New Padding(5, 6, 5, 6)
        PB.Name = "PB"
        PB.Size = New Size(281, 19)
        PB.TabIndex = 90
        PB.Visible = False
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.ImageScalingSize = New Size(20, 20)
        StatusStrip1.Items.AddRange(New ToolStripItem() {lblStatus})
        StatusStrip1.Location = New Point(0, 1258)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Padding = New Padding(1, 0, 24, 0)
        StatusStrip1.Size = New Size(1529, 22)
        StatusStrip1.TabIndex = 92
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = False
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(120, 15)
        ' 
        ' Label12
        ' 
        Label12.Location = New Point(365, 177)
        Label12.Margin = New Padding(5, 0, 5, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(161, 31)
        Label12.TabIndex = 93
        Label12.Text = "Post Date"
        Label12.TextAlign = ContentAlignment.TopCenter
        ' 
        ' dtpPost
        ' 
        dtpPost.Format = DateTimePickerFormat.Custom
        dtpPost.Location = New Point(346, 212)
        dtpPost.Margin = New Padding(5, 6, 5, 6)
        dtpPost.Name = "dtpPost"
        dtpPost.Size = New Size(203, 31)
        dtpPost.TabIndex = 94
        ' 
        ' Label13
        ' 
        Label13.Location = New Point(1021, 31)
        Label13.Margin = New Padding(5, 0, 5, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(234, 31)
        Label13.TabIndex = 95
        Label13.Text = "Tip: Double click to copy text."
        ' 
        ' clipboardMsg
        ' 
        clipboardMsg.Location = New Point(1289, 31)
        clipboardMsg.Margin = New Padding(5, 0, 5, 0)
        clipboardMsg.Name = "clipboardMsg"
        clipboardMsg.Size = New Size(225, 31)
        clipboardMsg.TabIndex = 96
        ' 
        ' ViewERAbtn
        ' 
        ViewERAbtn.Location = New Point(20, 108)
        ViewERAbtn.Margin = New Padding(4, 5, 4, 5)
        ViewERAbtn.Name = "ViewERAbtn"
        ViewERAbtn.Size = New Size(139, 48)
        ViewERAbtn.TabIndex = 97
        ViewERAbtn.Text = "View ERA"
        ViewERAbtn.UseVisualStyleBackColor = True
        ' 
        ' proceesedbtn
        ' 
        proceesedbtn.Location = New Point(285, 14)
        proceesedbtn.Margin = New Padding(4, 5, 4, 5)
        proceesedbtn.Name = "proceesedbtn"
        proceesedbtn.Size = New Size(210, 42)
        proceesedbtn.TabIndex = 98
        proceesedbtn.Text = "Show Processed"
        proceesedbtn.UseVisualStyleBackColor = True
        ' 
        ' documentID
        ' 
        documentID.BackColor = SystemColors.ButtonHighlight
        documentID.Location = New Point(290, 108)
        documentID.Margin = New Padding(5, 6, 5, 6)
        documentID.Name = "documentID"
        documentID.ReadOnly = True
        documentID.Size = New Size(204, 31)
        documentID.TabIndex = 99
        ' 
        ' lblProgress
        ' 
        lblProgress.Font = New Font("Microsoft Sans Serif", 7.5F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblProgress.Location = New Point(558, 269)
        lblProgress.Margin = New Padding(5, 0, 5, 0)
        lblProgress.Name = "lblProgress"
        lblProgress.Size = New Size(184, 27)
        lblProgress.TabIndex = 100
        lblProgress.Text = "Loading"
        ' 
        ' frmERA
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1529, 1280)
        Controls.Add(lblProgress)
        Controls.Add(documentID)
        Controls.Add(proceesedbtn)
        Controls.Add(ViewERAbtn)
        Controls.Add(clipboardMsg)
        Controls.Add(Label13)
        Controls.Add(dtpPost)
        Controls.Add(Label12)
        Controls.Add(StatusStrip1)
        Controls.Add(PB)
        Controls.Add(txtProcessed)
        Controls.Add(txtUnprocessed)
        Controls.Add(txtClaims)
        Controls.Add(Label11)
        Controls.Add(Label10)
        Controls.Add(Label9)
        Controls.Add(gbCumulative)
        Controls.Add(Label8)
        Controls.Add(txtBillReason)
        Controls.Add(dgvComments)
        Controls.Add(Label7)
        Controls.Add(Label3)
        Controls.Add(txtDisc)
        Controls.Add(cmbBillLevel)
        Controls.Add(txtERAFile)
        Controls.Add(Label2)
        Controls.Add(dgvEOBs)
        Controls.Add(btnClear)
        Controls.Add(txtERAType)
        Controls.Add(Label6)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(dgvClaimDetail)
        Controls.Add(dgvClaims)
        Controls.Add(Label1)
        Controls.Add(btnERALookUp)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximumSize = New Size(1551, 1336)
        MinimumSize = New Size(1508, 1294)
        Name = "frmERA"
        Text = "ERA Processing"
        CType(dgvClaims, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvClaimDetail, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvEOBs, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvComments, ComponentModel.ISupportInitialize).EndInit()
        gbCumulative.ResumeLayout(False)
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents btnERALookUp As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvClaims As System.Windows.Forms.DataGridView
    Friend WithEvents dgvClaimDetail As System.Windows.Forms.DataGridView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtERAType As System.Windows.Forms.TextBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents dgvEOBs As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtERAFile As System.Windows.Forms.TextBox
    Friend WithEvents cmbBillLevel As System.Windows.Forms.ComboBox
    Friend WithEvents txtDisc As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dgvComments As System.Windows.Forms.DataGridView
    Friend WithEvents txtBillReason As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbAction As System.Windows.Forms.ComboBox
    Friend WithEvents btnAction As System.Windows.Forms.Button
    Friend WithEvents gbCumulative As System.Windows.Forms.GroupBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtClaims As System.Windows.Forms.TextBox
    Friend WithEvents txtUnprocessed As System.Windows.Forms.TextBox
    Friend WithEvents txtProcessed As System.Windows.Forms.TextBox
    Friend WithEvents PB As System.Windows.Forms.ProgressBar
    Friend WithEvents Dated As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ABP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cmnt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents User As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Payer_Code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Payer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DocID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DocDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BillAmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChkPmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents dtpPost As System.Windows.Forms.DateTimePicker
    Friend WithEvents Claim As System.Windows.Forms.DataGridViewLinkColumn
    Friend WithEvents PayerCLMID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PatientName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CBilled As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PAuth As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CPaid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Bal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CLMSusp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChargeAction As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents CLMDist As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnApply As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents Reason As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SvcDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Sel As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ChargeID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TGPID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TGPName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CPT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Billed As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Allowed As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Paid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ItemPR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ItemWO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ItemBal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ItemSusp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ReasCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Action As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents ItemUpdate As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents clipboardMsg As System.Windows.Forms.Label
    Friend WithEvents ViewERAbtn As System.Windows.Forms.Button
    Friend WithEvents proceesedbtn As System.Windows.Forms.Button
    Friend WithEvents documentID As TextBox
    Friend WithEvents lblProgress As Label
End Class
