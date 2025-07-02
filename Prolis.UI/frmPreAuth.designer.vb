<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPreAuth
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPreAuth))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Label8 = New Label()
        btnSellT = New Button()
        btnDeselT = New Button()
        Label6 = New Label()
        Label1 = New Label()
        dgvDiscrete = New DataGridView()
        Discrete = New DataGridViewTextBoxColumn()
        btnTarget = New Button()
        lstTargets = New CheckedListBox()
        Label5 = New Label()
        txtAccTo = New TextBox()
        Label4 = New Label()
        txtDateTo = New MaskedTextBox()
        lblTo = New Label()
        txtAccFrom = New TextBox()
        txtDateFrom = New MaskedTextBox()
        lblFrom = New Label()
        btnLoad = New Button()
        Email = New DataGridViewTextBoxColumn()
        Billees = New DataGridViewTextBoxColumn()
        AmountB = New DataGridViewTextBoxColumn()
        BillDateB = New DataGridViewTextBoxColumn()
        SvcDateB = New DataGridViewTextBoxColumn()
        PatientsB = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn5 = New DataGridViewTextBoxColumn()
        InvID = New DataGridViewTextBoxColumn()
        DataGridViewCheckBoxColumn2 = New DataGridViewCheckBoxColumn()
        FileAmount = New DataGridViewTextBoxColumn()
        ChCount = New DataGridViewTextBoxColumn()
        Created = New DataGridViewTextBoxColumn()
        ClearingHouse = New DataGridViewTextBoxColumn()
        FileNo = New DataGridViewTextBoxColumn()
        TPCheck = New DataGridViewCheckBoxColumn()
        PatEmail = New DataGridViewTextBoxColumn()
        PatPrints = New DataGridViewTextBoxColumn()
        PatAmount = New DataGridViewTextBoxColumn()
        PatBDate = New DataGridViewTextBoxColumn()
        PatSDate = New DataGridViewTextBoxColumn()
        PatPatient = New DataGridViewTextBoxColumn()
        PatAccID = New DataGridViewTextBoxColumn()
        PatInVID = New DataGridViewTextBoxColumn()
        PatCheck = New DataGridViewCheckBoxColumn()
        tpUnbilled = New TabPage()
        txtResCount = New TextBox()
        Label9 = New Label()
        btnSave = New Button()
        btnSelS = New Button()
        btnDeselS = New Button()
        dgvResponses = New DataGridView()
        DataGridViewCheckBoxColumn1 = New DataGridViewCheckBoxColumn()
        AccIDS = New DataGridViewTextBoxColumn()
        PatientS = New DataGridViewTextBoxColumn()
        RecDateR = New DataGridViewTextBoxColumn()
        SvcDateR = New DataGridViewTextBoxColumn()
        Billee = New DataGridViewTextBoxColumn()
        Response = New DataGridViewTextBoxColumn()
        tpRequests = New TabPage()
        Label11 = New Label()
        cmbDestination = New ComboBox()
        chkUnprocessed = New CheckBox()
        txtReqCount = New TextBox()
        Label3 = New Label()
        btnPrint = New Button()
        btnSelQs = New Button()
        btnDeselQS = New Button()
        dgvRequests = New DataGridView()
        chkStS = New DataGridViewCheckBoxColumn()
        AccID = New DataGridViewTextBoxColumn()
        sPatient = New DataGridViewTextBoxColumn()
        RecDate = New DataGridViewTextBoxColumn()
        ReqSvcDate = New DataGridViewTextBoxColumn()
        Client = New DataGridViewTextBoxColumn()
        ReqBillee = New DataGridViewTextBoxColumn()
        TB = New TabControl()
        PrintDialog1 = New PrintDialog()
        CType(dgvDiscrete, ComponentModel.ISupportInitialize).BeginInit()
        tpUnbilled.SuspendLayout()
        CType(dgvResponses, ComponentModel.ISupportInitialize).BeginInit()
        tpRequests.SuspendLayout()
        CType(dgvRequests, ComponentModel.ISupportInitialize).BeginInit()
        TB.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label8
        ' 
        Label8.Font = New Font("Microsoft Sans Serif", 36F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label8.ForeColor = Color.Red
        Label8.Location = New Point(17, 113)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(70, 19)
        Label8.TabIndex = 55
        Label8.Text = "BEING CODED ...      DO NOT USE !"
        Label8.TextAlign = ContentAlignment.TopCenter
        ' 
        ' btnSellT
        ' 
        btnSellT.ForeColor = Color.DarkBlue
        btnSellT.Image = CType(resources.GetObject("btnSellT.Image"), Image)
        btnSellT.Location = New Point(1187, 40)
        btnSellT.Margin = New Padding(5, 6, 5, 6)
        btnSellT.Name = "btnSellT"
        btnSellT.Size = New Size(50, 56)
        btnSellT.TabIndex = 54
        btnSellT.TextAlign = ContentAlignment.MiddleRight
        btnSellT.TextImageRelation = TextImageRelation.ImageBeforeText
        btnSellT.UseVisualStyleBackColor = True
        ' 
        ' btnDeselT
        ' 
        btnDeselT.ForeColor = Color.DarkBlue
        btnDeselT.Image = CType(resources.GetObject("btnDeselT.Image"), Image)
        btnDeselT.Location = New Point(1187, 121)
        btnDeselT.Margin = New Padding(5, 6, 5, 6)
        btnDeselT.Name = "btnDeselT"
        btnDeselT.Size = New Size(50, 56)
        btnDeselT.TabIndex = 53
        btnDeselT.TextAlign = ContentAlignment.MiddleRight
        btnDeselT.TextImageRelation = TextImageRelation.ImageBeforeText
        btnDeselT.UseVisualStyleBackColor = True
        ' 
        ' Label6
        ' 
        Label6.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label6.ForeColor = Color.Magenta
        Label6.Location = New Point(327, 133)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(32, 62)
        Label6.TabIndex = 52
        Label6.Text = "OR"
        Label6.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label1
        ' 
        Label1.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.Magenta
        Label1.Location = New Point(160, 142)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(57, 37)
        Label1.TabIndex = 51
        Label1.Text = "OR"
        Label1.TextAlign = ContentAlignment.TopCenter
        ' 
        ' dgvDiscrete
        ' 
        dgvDiscrete.AllowUserToAddRows = False
        dgvDiscrete.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(255), CByte(224), CByte(192))
        dgvDiscrete.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvDiscrete.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvDiscrete.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvDiscrete.Columns.AddRange(New DataGridViewColumn() {Discrete})
        dgvDiscrete.Location = New Point(368, 40)
        dgvDiscrete.Margin = New Padding(5, 6, 5, 6)
        dgvDiscrete.Name = "dgvDiscrete"
        dgvDiscrete.RowHeadersVisible = False
        dgvDiscrete.RowHeadersWidth = 62
        dgvDiscrete.ScrollBars = ScrollBars.Vertical
        dgvDiscrete.Size = New Size(213, 238)
        dgvDiscrete.TabIndex = 50
        ' 
        ' Discrete
        ' 
        Discrete.HeaderText = "Discrete"
        Discrete.MaxInputLength = 16
        Discrete.MinimumWidth = 8
        Discrete.Name = "Discrete"
        Discrete.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' btnTarget
        ' 
        btnTarget.ForeColor = Color.DarkBlue
        btnTarget.Image = CType(resources.GetObject("btnTarget.Image"), Image)
        btnTarget.Location = New Point(608, 117)
        btnTarget.Margin = New Padding(5, 6, 5, 6)
        btnTarget.Name = "btnTarget"
        btnTarget.Size = New Size(58, 62)
        btnTarget.TabIndex = 49
        btnTarget.TextAlign = ContentAlignment.MiddleRight
        btnTarget.TextImageRelation = TextImageRelation.ImageBeforeText
        btnTarget.UseVisualStyleBackColor = True
        ' 
        ' lstTargets
        ' 
        lstTargets.FormattingEnabled = True
        lstTargets.Location = New Point(695, 40)
        lstTargets.Margin = New Padding(5, 6, 5, 6)
        lstTargets.Name = "lstTargets"
        lstTargets.Size = New Size(474, 228)
        lstTargets.Sorted = True
        lstTargets.TabIndex = 48
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.DarkBlue
        Label5.Location = New Point(183, 198)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(143, 25)
        Label5.TabIndex = 47
        Label5.Text = "Accession To"
        ' 
        ' txtAccTo
        ' 
        txtAccTo.Location = New Point(178, 231)
        txtAccTo.Margin = New Padding(5, 6, 5, 6)
        txtAccTo.MaxLength = 10
        txtAccTo.Name = "txtAccTo"
        txtAccTo.Size = New Size(146, 31)
        txtAccTo.TabIndex = 45
        txtAccTo.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(22, 198)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(147, 25)
        Label4.TabIndex = 46
        Label4.Text = "Accession From"
        ' 
        ' txtDateTo
        ' 
        txtDateTo.Location = New Point(178, 71)
        txtDateTo.Margin = New Padding(5, 6, 5, 6)
        txtDateTo.Mask = "00/00/0000"
        txtDateTo.Name = "txtDateTo"
        txtDateTo.Size = New Size(146, 31)
        txtDateTo.TabIndex = 42
        txtDateTo.ValidatingType = GetType(Date)
        ' 
        ' lblTo
        ' 
        lblTo.ForeColor = Color.Navy
        lblTo.Location = New Point(178, 40)
        lblTo.Margin = New Padding(5, 0, 5, 0)
        lblTo.Name = "lblTo"
        lblTo.Size = New Size(112, 25)
        lblTo.TabIndex = 43
        lblTo.Text = "Date To"
        ' 
        ' txtAccFrom
        ' 
        txtAccFrom.Location = New Point(20, 231)
        txtAccFrom.Margin = New Padding(5, 6, 5, 6)
        txtAccFrom.MaxLength = 10
        txtAccFrom.Name = "txtAccFrom"
        txtAccFrom.Size = New Size(146, 31)
        txtAccFrom.TabIndex = 44
        txtAccFrom.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtDateFrom
        ' 
        txtDateFrom.Location = New Point(17, 71)
        txtDateFrom.Margin = New Padding(5, 6, 5, 6)
        txtDateFrom.Mask = "00/00/0000"
        txtDateFrom.Name = "txtDateFrom"
        txtDateFrom.Size = New Size(146, 31)
        txtDateFrom.TabIndex = 41
        txtDateFrom.ValidatingType = GetType(Date)
        ' 
        ' lblFrom
        ' 
        lblFrom.ForeColor = Color.Navy
        lblFrom.Location = New Point(17, 40)
        lblFrom.Margin = New Padding(5, 0, 5, 0)
        lblFrom.Name = "lblFrom"
        lblFrom.Size = New Size(200, 25)
        lblFrom.TabIndex = 40
        lblFrom.Text = "Date From"
        ' 
        ' btnLoad
        ' 
        btnLoad.ForeColor = Color.DarkBlue
        btnLoad.Image = CType(resources.GetObject("btnLoad.Image"), Image)
        btnLoad.Location = New Point(1187, 198)
        btnLoad.Margin = New Padding(5, 6, 5, 6)
        btnLoad.Name = "btnLoad"
        btnLoad.Size = New Size(68, 81)
        btnLoad.TabIndex = 56
        btnLoad.TextAlign = ContentAlignment.MiddleRight
        btnLoad.TextImageRelation = TextImageRelation.ImageBeforeText
        btnLoad.UseVisualStyleBackColor = True
        ' 
        ' Email
        ' 
        Email.HeaderText = "Email"
        Email.MinimumWidth = 8
        Email.Name = "Email"
        Email.ReadOnly = True
        Email.SortMode = DataGridViewColumnSortMode.NotSortable
        Email.Width = 150
        ' 
        ' Billees
        ' 
        Billees.FillWeight = 110F
        Billees.HeaderText = "Billees"
        Billees.MinimumWidth = 8
        Billees.Name = "Billees"
        Billees.ReadOnly = True
        Billees.Width = 110
        ' 
        ' AmountB
        ' 
        AmountB.FillWeight = 70F
        AmountB.HeaderText = "Amount"
        AmountB.MinimumWidth = 8
        AmountB.Name = "AmountB"
        AmountB.ReadOnly = True
        AmountB.Width = 70
        ' 
        ' BillDateB
        ' 
        BillDateB.FillWeight = 70F
        BillDateB.HeaderText = "Bill'd"
        BillDateB.MinimumWidth = 8
        BillDateB.Name = "BillDateB"
        BillDateB.ReadOnly = True
        BillDateB.SortMode = DataGridViewColumnSortMode.NotSortable
        BillDateB.Width = 70
        ' 
        ' SvcDateB
        ' 
        SvcDateB.FillWeight = 70F
        SvcDateB.HeaderText = "Svc'd"
        SvcDateB.MinimumWidth = 8
        SvcDateB.Name = "SvcDateB"
        SvcDateB.ReadOnly = True
        SvcDateB.SortMode = DataGridViewColumnSortMode.NotSortable
        SvcDateB.Width = 70
        ' 
        ' PatientsB
        ' 
        PatientsB.HeaderText = "Patient (L, S)"
        PatientsB.MinimumWidth = 8
        PatientsB.Name = "PatientsB"
        PatientsB.ReadOnly = True
        PatientsB.Width = 150
        ' 
        ' DataGridViewTextBoxColumn5
        ' 
        DataGridViewTextBoxColumn5.FillWeight = 70F
        DataGridViewTextBoxColumn5.HeaderText = "Accession"
        DataGridViewTextBoxColumn5.MinimumWidth = 8
        DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        DataGridViewTextBoxColumn5.ReadOnly = True
        DataGridViewTextBoxColumn5.Width = 70
        ' 
        ' InvID
        ' 
        InvID.FillWeight = 70F
        InvID.HeaderText = "Invoice"
        InvID.MinimumWidth = 8
        InvID.Name = "InvID"
        InvID.ReadOnly = True
        InvID.Width = 70
        ' 
        ' DataGridViewCheckBoxColumn2
        ' 
        DataGridViewCheckBoxColumn2.FillWeight = 40F
        DataGridViewCheckBoxColumn2.HeaderText = ""
        DataGridViewCheckBoxColumn2.MinimumWidth = 8
        DataGridViewCheckBoxColumn2.Name = "DataGridViewCheckBoxColumn2"
        DataGridViewCheckBoxColumn2.Width = 40
        ' 
        ' FileAmount
        ' 
        FileAmount.FillWeight = 90F
        FileAmount.HeaderText = "File Amount"
        FileAmount.MinimumWidth = 8
        FileAmount.Name = "FileAmount"
        FileAmount.ReadOnly = True
        FileAmount.SortMode = DataGridViewColumnSortMode.NotSortable
        FileAmount.Width = 90
        ' 
        ' ChCount
        ' 
        ChCount.FillWeight = 80F
        ChCount.HeaderText = "Invoices"
        ChCount.MinimumWidth = 8
        ChCount.Name = "ChCount"
        ChCount.ReadOnly = True
        ChCount.Width = 80
        ' 
        ' Created
        ' 
        Created.FillWeight = 90F
        Created.HeaderText = "Created on"
        Created.MinimumWidth = 8
        Created.Name = "Created"
        Created.ReadOnly = True
        Created.SortMode = DataGridViewColumnSortMode.NotSortable
        Created.Width = 90
        ' 
        ' ClearingHouse
        ' 
        ClearingHouse.FillWeight = 200F
        ClearingHouse.HeaderText = "Clearing House"
        ClearingHouse.MinimumWidth = 8
        ClearingHouse.Name = "ClearingHouse"
        ClearingHouse.ReadOnly = True
        ClearingHouse.Width = 200
        ' 
        ' FileNo
        ' 
        FileNo.FillWeight = 70F
        FileNo.HeaderText = "File No"
        FileNo.MinimumWidth = 8
        FileNo.Name = "FileNo"
        FileNo.ReadOnly = True
        FileNo.Width = 70
        ' 
        ' TPCheck
        ' 
        TPCheck.FillWeight = 40F
        TPCheck.HeaderText = ""
        TPCheck.MinimumWidth = 8
        TPCheck.Name = "TPCheck"
        TPCheck.Width = 40
        ' 
        ' PatEmail
        ' 
        PatEmail.HeaderText = "Email"
        PatEmail.MinimumWidth = 8
        PatEmail.Name = "PatEmail"
        PatEmail.ReadOnly = True
        PatEmail.SortMode = DataGridViewColumnSortMode.NotSortable
        PatEmail.Width = 150
        ' 
        ' PatPrints
        ' 
        PatPrints.FillWeight = 110F
        PatPrints.HeaderText = "Prints"
        PatPrints.MinimumWidth = 8
        PatPrints.Name = "PatPrints"
        PatPrints.ReadOnly = True
        PatPrints.Width = 110
        ' 
        ' PatAmount
        ' 
        PatAmount.FillWeight = 70F
        PatAmount.HeaderText = "Amount"
        PatAmount.MinimumWidth = 8
        PatAmount.Name = "PatAmount"
        PatAmount.ReadOnly = True
        PatAmount.Width = 70
        ' 
        ' PatBDate
        ' 
        PatBDate.FillWeight = 70F
        PatBDate.HeaderText = "Bill'd"
        PatBDate.MinimumWidth = 8
        PatBDate.Name = "PatBDate"
        PatBDate.ReadOnly = True
        PatBDate.SortMode = DataGridViewColumnSortMode.NotSortable
        PatBDate.Width = 70
        ' 
        ' PatSDate
        ' 
        PatSDate.FillWeight = 70F
        PatSDate.HeaderText = "Svc'd"
        PatSDate.MinimumWidth = 8
        PatSDate.Name = "PatSDate"
        PatSDate.ReadOnly = True
        PatSDate.SortMode = DataGridViewColumnSortMode.NotSortable
        PatSDate.Width = 70
        ' 
        ' PatPatient
        ' 
        PatPatient.HeaderText = "Patient (L, S)"
        PatPatient.MinimumWidth = 8
        PatPatient.Name = "PatPatient"
        PatPatient.ReadOnly = True
        PatPatient.Width = 150
        ' 
        ' PatAccID
        ' 
        PatAccID.FillWeight = 70F
        PatAccID.HeaderText = "Accession"
        PatAccID.MinimumWidth = 8
        PatAccID.Name = "PatAccID"
        PatAccID.ReadOnly = True
        PatAccID.Width = 70
        ' 
        ' PatInVID
        ' 
        PatInVID.FillWeight = 70F
        PatInVID.HeaderText = "Invoice"
        PatInVID.MinimumWidth = 8
        PatInVID.Name = "PatInVID"
        PatInVID.ReadOnly = True
        PatInVID.Width = 70
        ' 
        ' PatCheck
        ' 
        PatCheck.FillWeight = 40F
        PatCheck.HeaderText = ""
        PatCheck.MinimumWidth = 8
        PatCheck.Name = "PatCheck"
        PatCheck.Width = 40
        ' 
        ' tpUnbilled
        ' 
        tpUnbilled.Controls.Add(txtResCount)
        tpUnbilled.Controls.Add(Label9)
        tpUnbilled.Controls.Add(btnSave)
        tpUnbilled.Controls.Add(btnSelS)
        tpUnbilled.Controls.Add(btnDeselS)
        tpUnbilled.Controls.Add(dgvResponses)
        tpUnbilled.Location = New Point(4, 22)
        tpUnbilled.Margin = New Padding(5, 6, 5, 6)
        tpUnbilled.Name = "tpUnbilled"
        tpUnbilled.Padding = New Padding(5, 6, 5, 6)
        tpUnbilled.Size = New Size(1225, 807)
        tpUnbilled.TabIndex = 1
        tpUnbilled.Text = "Responses          "
        tpUnbilled.UseVisualStyleBackColor = True
        ' 
        ' txtResCount
        ' 
        txtResCount.Location = New Point(172, 687)
        txtResCount.Margin = New Padding(5, 6, 5, 6)
        txtResCount.Name = "txtResCount"
        txtResCount.ReadOnly = True
        txtResCount.Size = New Size(102, 31)
        txtResCount.TabIndex = 31
        txtResCount.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label9
        ' 
        Label9.ForeColor = Color.DarkBlue
        Label9.Location = New Point(172, 656)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(105, 25)
        Label9.TabIndex = 30
        Label9.Text = "Count"
        Label9.TextAlign = ContentAlignment.TopCenter
        ' 
        ' btnSave
        ' 
        btnSave.Enabled = False
        btnSave.Image = CType(resources.GetObject("btnSave.Image"), Image)
        btnSave.Location = New Point(1038, 677)
        btnSave.Margin = New Padding(5, 6, 5, 6)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(140, 56)
        btnSave.TabIndex = 23
        btnSave.Text = "Bill"
        btnSave.TextAlign = ContentAlignment.MiddleRight
        btnSave.TextImageRelation = TextImageRelation.ImageBeforeText
        btnSave.UseVisualStyleBackColor = True
        ' 
        ' btnSelS
        ' 
        btnSelS.ForeColor = Color.DarkBlue
        btnSelS.Image = CType(resources.GetObject("btnSelS.Image"), Image)
        btnSelS.Location = New Point(30, 677)
        btnSelS.Margin = New Padding(5, 6, 5, 6)
        btnSelS.Name = "btnSelS"
        btnSelS.Size = New Size(50, 56)
        btnSelS.TabIndex = 20
        btnSelS.TextAlign = ContentAlignment.MiddleRight
        btnSelS.TextImageRelation = TextImageRelation.ImageBeforeText
        btnSelS.UseVisualStyleBackColor = True
        ' 
        ' btnDeselS
        ' 
        btnDeselS.ForeColor = Color.DarkBlue
        btnDeselS.Image = CType(resources.GetObject("btnDeselS.Image"), Image)
        btnDeselS.Location = New Point(90, 677)
        btnDeselS.Margin = New Padding(5, 6, 5, 6)
        btnDeselS.Name = "btnDeselS"
        btnDeselS.Size = New Size(50, 56)
        btnDeselS.TabIndex = 19
        btnDeselS.TextAlign = ContentAlignment.MiddleRight
        btnDeselS.TextImageRelation = TextImageRelation.ImageBeforeText
        btnDeselS.UseVisualStyleBackColor = True
        ' 
        ' dgvResponses
        ' 
        dgvResponses.AllowUserToAddRows = False
        dgvResponses.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = Color.Honeydew
        dgvResponses.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        dgvResponses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvResponses.Columns.AddRange(New DataGridViewColumn() {DataGridViewCheckBoxColumn1, AccIDS, PatientS, RecDateR, SvcDateR, Billee, Response})
        dgvResponses.Location = New Point(10, 6)
        dgvResponses.Margin = New Padding(5, 6, 5, 6)
        dgvResponses.Name = "dgvResponses"
        dgvResponses.RowHeadersVisible = False
        dgvResponses.RowHeadersWidth = 62
        dgvResponses.Size = New Size(1198, 631)
        dgvResponses.TabIndex = 17
        ' 
        ' DataGridViewCheckBoxColumn1
        ' 
        DataGridViewCheckBoxColumn1.FillWeight = 40F
        DataGridViewCheckBoxColumn1.HeaderText = ""
        DataGridViewCheckBoxColumn1.MinimumWidth = 8
        DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
        DataGridViewCheckBoxColumn1.Width = 40
        ' 
        ' AccIDS
        ' 
        AccIDS.FillWeight = 90F
        AccIDS.HeaderText = "Accession"
        AccIDS.MinimumWidth = 8
        AccIDS.Name = "AccIDS"
        AccIDS.ReadOnly = True
        AccIDS.Width = 90
        ' 
        ' PatientS
        ' 
        PatientS.FillWeight = 136F
        PatientS.HeaderText = "Patient Name (L, S)"
        PatientS.MinimumWidth = 8
        PatientS.Name = "PatientS"
        PatientS.ReadOnly = True
        PatientS.Width = 136
        ' 
        ' RecDateR
        ' 
        RecDateR.FillWeight = 80F
        RecDateR.HeaderText = "Rec Date"
        RecDateR.MinimumWidth = 8
        RecDateR.Name = "RecDateR"
        RecDateR.ReadOnly = True
        RecDateR.Width = 80
        ' 
        ' SvcDateR
        ' 
        SvcDateR.FillWeight = 80F
        SvcDateR.HeaderText = "Svc Date"
        SvcDateR.MinimumWidth = 8
        SvcDateR.Name = "SvcDateR"
        SvcDateR.ReadOnly = True
        SvcDateR.Width = 80
        ' 
        ' Billee
        ' 
        Billee.FillWeight = 144F
        Billee.HeaderText = "Payer"
        Billee.MinimumWidth = 8
        Billee.Name = "Billee"
        Billee.Width = 144
        ' 
        ' Response
        ' 
        Response.FillWeight = 124F
        Response.HeaderText = "Response"
        Response.MaxInputLength = 100
        Response.MinimumWidth = 8
        Response.Name = "Response"
        Response.SortMode = DataGridViewColumnSortMode.NotSortable
        Response.Width = 124
        ' 
        ' tpRequests
        ' 
        tpRequests.Controls.Add(Label11)
        tpRequests.Controls.Add(cmbDestination)
        tpRequests.Controls.Add(chkUnprocessed)
        tpRequests.Controls.Add(txtReqCount)
        tpRequests.Controls.Add(Label3)
        tpRequests.Controls.Add(btnPrint)
        tpRequests.Controls.Add(btnSelQs)
        tpRequests.Controls.Add(btnDeselQS)
        tpRequests.Controls.Add(dgvRequests)
        tpRequests.Location = New Point(4, 22)
        tpRequests.Margin = New Padding(5, 6, 5, 6)
        tpRequests.Name = "tpRequests"
        tpRequests.Padding = New Padding(5, 6, 5, 6)
        tpRequests.Size = New Size(1225, 807)
        tpRequests.TabIndex = 0
        tpRequests.Text = "Requests          "
        tpRequests.UseVisualStyleBackColor = True
        ' 
        ' Label11
        ' 
        Label11.ForeColor = Color.DarkBlue
        Label11.Location = New Point(807, 656)
        Label11.Margin = New Padding(5, 0, 5, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(108, 25)
        Label11.TabIndex = 39
        Label11.Text = "Destination"
        ' 
        ' cmbDestination
        ' 
        cmbDestination.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDestination.FormattingEnabled = True
        cmbDestination.Location = New Point(797, 687)
        cmbDestination.Margin = New Padding(5, 6, 5, 6)
        cmbDestination.Name = "cmbDestination"
        cmbDestination.Size = New Size(206, 33)
        cmbDestination.TabIndex = 38
        ' 
        ' chkUnprocessed
        ' 
        chkUnprocessed.Appearance = Appearance.Button
        chkUnprocessed.Checked = True
        chkUnprocessed.CheckState = CheckState.Checked
        chkUnprocessed.Location = New Point(512, 673)
        chkUnprocessed.Margin = New Padding(5, 6, 5, 6)
        chkUnprocessed.Name = "chkUnprocessed"
        chkUnprocessed.Size = New Size(180, 62)
        chkUnprocessed.TabIndex = 26
        chkUnprocessed.Text = "Unprocessed"
        chkUnprocessed.TextAlign = ContentAlignment.MiddleCenter
        chkUnprocessed.UseVisualStyleBackColor = True
        ' 
        ' txtReqCount
        ' 
        txtReqCount.Location = New Point(172, 694)
        txtReqCount.Margin = New Padding(5, 6, 5, 6)
        txtReqCount.Name = "txtReqCount"
        txtReqCount.ReadOnly = True
        txtReqCount.Size = New Size(102, 31)
        txtReqCount.TabIndex = 25
        txtReqCount.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(172, 663)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(105, 25)
        Label3.TabIndex = 24
        Label3.Text = "Count"
        Label3.TextAlign = ContentAlignment.TopCenter
        ' 
        ' btnPrint
        ' 
        btnPrint.Enabled = False
        btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), Image)
        btnPrint.Location = New Point(1030, 671)
        btnPrint.Margin = New Padding(5, 6, 5, 6)
        btnPrint.Name = "btnPrint"
        btnPrint.Size = New Size(153, 67)
        btnPrint.TabIndex = 13
        btnPrint.Text = "Print"
        btnPrint.TextAlign = ContentAlignment.MiddleRight
        btnPrint.TextImageRelation = TextImageRelation.ImageBeforeText
        btnPrint.UseVisualStyleBackColor = True
        ' 
        ' btnSelQs
        ' 
        btnSelQs.ForeColor = Color.DarkBlue
        btnSelQs.Image = CType(resources.GetObject("btnSelQs.Image"), Image)
        btnSelQs.Location = New Point(33, 677)
        btnSelQs.Margin = New Padding(5, 6, 5, 6)
        btnSelQs.Name = "btnSelQs"
        btnSelQs.Size = New Size(50, 56)
        btnSelQs.TabIndex = 12
        btnSelQs.TextAlign = ContentAlignment.MiddleRight
        btnSelQs.TextImageRelation = TextImageRelation.ImageBeforeText
        btnSelQs.UseVisualStyleBackColor = True
        ' 
        ' btnDeselQS
        ' 
        btnDeselQS.ForeColor = Color.DarkBlue
        btnDeselQS.Image = CType(resources.GetObject("btnDeselQS.Image"), Image)
        btnDeselQS.Location = New Point(93, 677)
        btnDeselQS.Margin = New Padding(5, 6, 5, 6)
        btnDeselQS.Name = "btnDeselQS"
        btnDeselQS.Size = New Size(50, 56)
        btnDeselQS.TabIndex = 11
        btnDeselQS.TextAlign = ContentAlignment.MiddleRight
        btnDeselQS.TextImageRelation = TextImageRelation.ImageBeforeText
        btnDeselQS.UseVisualStyleBackColor = True
        ' 
        ' dgvRequests
        ' 
        dgvRequests.AllowUserToAddRows = False
        dgvRequests.AllowUserToDeleteRows = False
        DataGridViewCellStyle3.BackColor = Color.MintCream
        dgvRequests.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        dgvRequests.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvRequests.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvRequests.Columns.AddRange(New DataGridViewColumn() {chkStS, AccID, sPatient, RecDate, ReqSvcDate, Client, ReqBillee})
        dgvRequests.Location = New Point(10, 6)
        dgvRequests.Margin = New Padding(5, 6, 5, 6)
        dgvRequests.Name = "dgvRequests"
        dgvRequests.RowHeadersVisible = False
        dgvRequests.RowHeadersWidth = 62
        dgvRequests.Size = New Size(1200, 631)
        dgvRequests.TabIndex = 0
        ' 
        ' chkStS
        ' 
        chkStS.FillWeight = 40F
        chkStS.HeaderText = ""
        chkStS.MinimumWidth = 8
        chkStS.Name = "chkStS"
        ' 
        ' AccID
        ' 
        AccID.FillWeight = 80F
        AccID.HeaderText = "Accession"
        AccID.MinimumWidth = 8
        AccID.Name = "AccID"
        AccID.ReadOnly = True
        ' 
        ' sPatient
        ' 
        sPatient.FillWeight = 124F
        sPatient.HeaderText = "Patient (L, F)"
        sPatient.MinimumWidth = 8
        sPatient.Name = "sPatient"
        sPatient.ReadOnly = True
        ' 
        ' RecDate
        ' 
        RecDate.FillWeight = 80F
        RecDate.HeaderText = "Rec Date"
        RecDate.MinimumWidth = 8
        RecDate.Name = "RecDate"
        RecDate.ReadOnly = True
        ' 
        ' ReqSvcDate
        ' 
        ReqSvcDate.FillWeight = 80F
        ReqSvcDate.HeaderText = "Svc Date"
        ReqSvcDate.MinimumWidth = 8
        ReqSvcDate.Name = "ReqSvcDate"
        ReqSvcDate.ReadOnly = True
        ' 
        ' Client
        ' 
        Client.FillWeight = 152F
        Client.HeaderText = "Client"
        Client.MinimumWidth = 8
        Client.Name = "Client"
        Client.ReadOnly = True
        ' 
        ' ReqBillee
        ' 
        ReqBillee.FillWeight = 142F
        ReqBillee.HeaderText = "Payer"
        ReqBillee.MinimumWidth = 8
        ReqBillee.Name = "ReqBillee"
        ReqBillee.ReadOnly = True
        ' 
        ' TB
        ' 
        TB.Controls.Add(tpRequests)
        TB.Controls.Add(tpUnbilled)
        TB.ItemSize = New Size(80, 18)
        TB.Location = New Point(22, 325)
        TB.Margin = New Padding(5, 6, 5, 6)
        TB.Name = "TB"
        TB.SelectedIndex = 0
        TB.Size = New Size(1233, 833)
        TB.TabIndex = 57
        ' 
        ' PrintDialog1
        ' 
        PrintDialog1.UseEXDialog = True
        ' 
        ' frmPreAuth
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1277, 1185)
        Controls.Add(TB)
        Controls.Add(btnLoad)
        Controls.Add(Label8)
        Controls.Add(btnSellT)
        Controls.Add(btnDeselT)
        Controls.Add(Label6)
        Controls.Add(Label1)
        Controls.Add(dgvDiscrete)
        Controls.Add(btnTarget)
        Controls.Add(lstTargets)
        Controls.Add(Label5)
        Controls.Add(txtAccTo)
        Controls.Add(Label4)
        Controls.Add(txtDateTo)
        Controls.Add(lblTo)
        Controls.Add(txtAccFrom)
        Controls.Add(txtDateFrom)
        Controls.Add(lblFrom)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        Name = "frmPreAuth"
        Text = "Insurance Pre Authorizations"
        CType(dgvDiscrete, ComponentModel.ISupportInitialize).EndInit()
        tpUnbilled.ResumeLayout(False)
        tpUnbilled.PerformLayout()
        CType(dgvResponses, ComponentModel.ISupportInitialize).EndInit()
        tpRequests.ResumeLayout(False)
        tpRequests.PerformLayout()
        CType(dgvRequests, ComponentModel.ISupportInitialize).EndInit()
        TB.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnSellT As System.Windows.Forms.Button
    Friend WithEvents btnDeselT As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvDiscrete As System.Windows.Forms.DataGridView
    Friend WithEvents btnTarget As System.Windows.Forms.Button
    Friend WithEvents lstTargets As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtAccTo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents txtAccFrom As System.Windows.Forms.TextBox
    Friend WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents Discrete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents Email As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Billees As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AmountB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BillDateB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SvcDateB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PatientsB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InvID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn2 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents FileAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChCount As System.Windows.Forms.DataGridViewTextBoxColumn
    Shadows WithEvents Created As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ClearingHouse As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FileNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TPCheck As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents PatEmail As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PatPrints As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PatAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PatBDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PatSDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PatPatient As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PatAccID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PatInVID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PatCheck As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents tpUnbilled As System.Windows.Forms.TabPage
    Friend WithEvents txtResCount As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnSelS As System.Windows.Forms.Button
    Friend WithEvents btnDeselS As System.Windows.Forms.Button
    Friend WithEvents dgvResponses As System.Windows.Forms.DataGridView
    Friend WithEvents tpRequests As System.Windows.Forms.TabPage
    Friend WithEvents txtReqCount As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnSelQs As System.Windows.Forms.Button
    Friend WithEvents btnDeselQS As System.Windows.Forms.Button
    Friend WithEvents dgvRequests As System.Windows.Forms.DataGridView
    Friend WithEvents TB As System.Windows.Forms.TabControl
    Friend WithEvents chkUnprocessed As System.Windows.Forms.CheckBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmbDestination As System.Windows.Forms.ComboBox
    Friend WithEvents chkStS As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents AccID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents sPatient As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RecDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ReqSvcDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Client As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ReqBillee As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents AccIDS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PatientS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RecDateR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SvcDateR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Billee As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Response As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
End Class
