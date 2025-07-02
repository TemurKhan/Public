<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAccDash
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
        components = New ComponentModel.Container()
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAccDash))
        lblTerm = New Label()
        txtTerm = New TextBox()
        Label3 = New Label()
        cmbCriteria = New ComboBox()
        lblTo = New Label()
        dtpTo = New DateTimePicker()
        dtpFrom = New DateTimePicker()
        lblFrom = New Label()
        txtProviders = New TextBox()
        Label10 = New Label()
        txtTotal = New TextBox()
        Label9 = New Label()
        txt3PBilled = New TextBox()
        Label8 = New Label()
        txtPatBilled = New TextBox()
        Label7 = New Label()
        txtClientBilled = New TextBox()
        Label6 = New Label()
        txtGratis = New TextBox()
        LabelP = New Label()
        txtPatients = New TextBox()
        Label4 = New Label()
        dgvAccessions = New DataGridView()
        AccID = New DataGridViewTextBoxColumn()
        Dated = New DataGridViewTextBoxColumn()
        Patient = New DataGridViewTextBoxColumn()
        Provider = New DataGridViewTextBoxColumn()
        BillType = New DataGridViewTextBoxColumn()
        dgvOrders = New DataGridView()
        TGPID = New DataGridViewTextBoxColumn()
        TGPName = New DataGridViewTextBoxColumn()
        Stat = New DataGridViewCheckBoxColumn()
        txtBillee = New TextBox()
        Label5 = New Label()
        lblResp = New Label()
        Label13 = New Label()
        txtPatient = New TextBox()
        Label11 = New Label()
        txtDxs = New TextBox()
        txtSpecimen = New TextBox()
        Label14 = New Label()
        txtAttendingProvider = New TextBox()
        Label15 = New Label()
        btnPrintDet = New Button()
        btnPrintSum = New Button()
        cmbDateType = New ComboBox()
        Label16 = New Label()
        cmbStatus = New ComboBox()
        Label1 = New Label()
        lblUser = New Label()
        txtUser = New TextBox()
        StatusStrip1 = New StatusStrip()
        lblStatus = New ToolStripStatusLabel()
        PB = New ToolStripProgressBar()
        btnPmtReceipt = New Button()
        Label2 = New Label()
        btnGo = New Button()
        ToolTip1 = New ToolTip(components)
        openInRequi = New Button()
        Label19 = New Label()
        CType(dgvAccessions, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvOrders, ComponentModel.ISupportInitialize).BeginInit()
        StatusStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' lblTerm
        ' 
        lblTerm.ForeColor = Color.DarkBlue
        lblTerm.Location = New Point(975, 22)
        lblTerm.Margin = New Padding(5, 0, 5, 0)
        lblTerm.Name = "lblTerm"
        lblTerm.Size = New Size(175, 28)
        lblTerm.TabIndex = 8
        lblTerm.Text = "Search Term"
        ' 
        ' txtTerm
        ' 
        txtTerm.Location = New Point(979, 56)
        txtTerm.Margin = New Padding(5, 6, 5, 6)
        txtTerm.Name = "txtTerm"
        txtTerm.Size = New Size(238, 31)
        txtTerm.TabIndex = 7
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(695, 22)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(206, 28)
        Label3.TabIndex = 6
        Label3.Text = "Criteria"
        ' 
        ' cmbCriteria
        ' 
        cmbCriteria.FormattingEnabled = True
        cmbCriteria.Items.AddRange(New Object() {"Accession ID", "Provider ID", "Provider Name (Last, First)", "Patient ID", "Patient Name (Last, First)", "Client Billed", "Patient Billed", "Insurance Billed", "Payer ID", "Payer Name", "Gratis", "Requisition Number"})
        cmbCriteria.Location = New Point(699, 53)
        cmbCriteria.Margin = New Padding(5, 6, 5, 6)
        cmbCriteria.Name = "cmbCriteria"
        cmbCriteria.Size = New Size(250, 33)
        cmbCriteria.TabIndex = 5
        ' 
        ' lblTo
        ' 
        lblTo.ForeColor = Color.Magenta
        lblTo.Location = New Point(524, 17)
        lblTo.Margin = New Padding(5, 0, 5, 0)
        lblTo.Name = "lblTo"
        lblTo.Size = New Size(140, 28)
        lblTo.TabIndex = 3
        lblTo.Text = "To"
        ' 
        ' dtpTo
        ' 
        dtpTo.Format = DateTimePickerFormat.Short
        dtpTo.Location = New Point(521, 53)
        dtpTo.Margin = New Padding(5, 6, 5, 6)
        dtpTo.Name = "dtpTo"
        dtpTo.Size = New Size(168, 31)
        dtpTo.TabIndex = 2
        ' 
        ' dtpFrom
        ' 
        dtpFrom.Format = DateTimePickerFormat.Short
        dtpFrom.Location = New Point(349, 56)
        dtpFrom.Margin = New Padding(5, 6, 5, 6)
        dtpFrom.Name = "dtpFrom"
        dtpFrom.Size = New Size(148, 31)
        dtpFrom.TabIndex = 1
        ' 
        ' lblFrom
        ' 
        lblFrom.ForeColor = Color.Red
        lblFrom.Location = New Point(349, 22)
        lblFrom.Margin = New Padding(5, 0, 5, 0)
        lblFrom.Name = "lblFrom"
        lblFrom.Size = New Size(84, 28)
        lblFrom.TabIndex = 0
        lblFrom.Text = "From"
        ' 
        ' txtProviders
        ' 
        txtProviders.Location = New Point(26, 139)
        txtProviders.Margin = New Padding(5, 6, 5, 6)
        txtProviders.Name = "txtProviders"
        txtProviders.ReadOnly = True
        txtProviders.Size = New Size(155, 31)
        txtProviders.TabIndex = 5
        txtProviders.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label10
        ' 
        Label10.ForeColor = Color.DarkBlue
        Label10.Location = New Point(1134, 103)
        Label10.Margin = New Padding(5, 0, 5, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(156, 28)
        Label10.TabIndex = 23
        Label10.Text = "Total"
        Label10.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtTotal
        ' 
        txtTotal.Location = New Point(1116, 139)
        txtTotal.Margin = New Padding(5, 6, 5, 6)
        txtTotal.Name = "txtTotal"
        txtTotal.ReadOnly = True
        txtTotal.Size = New Size(173, 31)
        txtTotal.TabIndex = 22
        txtTotal.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label9
        ' 
        Label9.ForeColor = Color.DarkBlue
        Label9.Location = New Point(951, 103)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(156, 28)
        Label9.TabIndex = 21
        Label9.Text = "Insurance Billed"
        Label9.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txt3PBilled
        ' 
        txt3PBilled.Location = New Point(955, 139)
        txt3PBilled.Margin = New Padding(5, 6, 5, 6)
        txt3PBilled.Name = "txt3PBilled"
        txt3PBilled.ReadOnly = True
        txt3PBilled.Size = New Size(150, 31)
        txt3PBilled.TabIndex = 20
        txt3PBilled.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label8
        ' 
        Label8.ForeColor = Color.DarkBlue
        Label8.Location = New Point(699, 103)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(156, 28)
        Label8.TabIndex = 19
        Label8.Text = "Patient Billed"
        Label8.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtPatBilled
        ' 
        txtPatBilled.Location = New Point(699, 139)
        txtPatBilled.Margin = New Padding(5, 6, 5, 6)
        txtPatBilled.Name = "txtPatBilled"
        txtPatBilled.ReadOnly = True
        txtPatBilled.Size = New Size(154, 31)
        txtPatBilled.TabIndex = 18
        txtPatBilled.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.DarkBlue
        Label7.Location = New Point(521, 103)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(156, 28)
        Label7.TabIndex = 17
        Label7.Text = "Client Billed"
        Label7.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtClientBilled
        ' 
        txtClientBilled.Location = New Point(521, 139)
        txtClientBilled.Margin = New Padding(5, 6, 5, 6)
        txtClientBilled.Name = "txtClientBilled"
        txtClientBilled.ReadOnly = True
        txtClientBilled.Size = New Size(154, 31)
        txtClientBilled.TabIndex = 16
        txtClientBilled.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.DarkBlue
        Label6.Location = New Point(349, 103)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(156, 28)
        Label6.TabIndex = 15
        Label6.Text = "Gratis"
        Label6.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtGratis
        ' 
        txtGratis.Location = New Point(349, 139)
        txtGratis.Margin = New Padding(5, 6, 5, 6)
        txtGratis.Name = "txtGratis"
        txtGratis.ReadOnly = True
        txtGratis.Size = New Size(154, 31)
        txtGratis.TabIndex = 14
        txtGratis.TextAlign = HorizontalAlignment.Center
        ' 
        ' LabelP
        ' 
        LabelP.ForeColor = Color.DarkBlue
        LabelP.Location = New Point(201, 103)
        LabelP.Margin = New Padding(5, 0, 5, 0)
        LabelP.Name = "LabelP"
        LabelP.Size = New Size(136, 28)
        LabelP.TabIndex = 13
        LabelP.Text = "Patients"
        LabelP.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtPatients
        ' 
        txtPatients.Location = New Point(194, 139)
        txtPatients.Margin = New Padding(5, 6, 5, 6)
        txtPatients.Name = "txtPatients"
        txtPatients.ReadOnly = True
        txtPatients.Size = New Size(135, 31)
        txtPatients.TabIndex = 12
        txtPatients.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(26, 103)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(151, 28)
        Label4.TabIndex = 11
        Label4.Text = "Providers"
        Label4.TextAlign = ContentAlignment.TopCenter
        ' 
        ' dgvAccessions
        ' 
        dgvAccessions.AllowUserToAddRows = False
        dgvAccessions.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.SeaShell
        dgvAccessions.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvAccessions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvAccessions.Columns.AddRange(New DataGridViewColumn() {AccID, Dated, Patient, Provider, BillType})
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = Color.Lavender
        DataGridViewCellStyle2.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle2.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False
        dgvAccessions.DefaultCellStyle = DataGridViewCellStyle2
        dgvAccessions.Location = New Point(25, 484)
        dgvAccessions.Margin = New Padding(5, 6, 5, 6)
        dgvAccessions.MultiSelect = False
        dgvAccessions.Name = "dgvAccessions"
        dgvAccessions.ReadOnly = True
        dgvAccessions.RowHeadersVisible = False
        dgvAccessions.RowHeadersWidth = 51
        dgvAccessions.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvAccessions.Size = New Size(735, 611)
        dgvAccessions.TabIndex = 2
        ' 
        ' AccID
        ' 
        AccID.FillWeight = 75F
        AccID.HeaderText = "Acc ID"
        AccID.MinimumWidth = 6
        AccID.Name = "AccID"
        AccID.ReadOnly = True
        AccID.Width = 117
        ' 
        ' Dated
        ' 
        Dated.FillWeight = 80F
        Dated.HeaderText = "Dated"
        Dated.MinimumWidth = 6
        Dated.Name = "Dated"
        Dated.ReadOnly = True
        Dated.Width = 124
        ' 
        ' Patient
        ' 
        Patient.FillWeight = 136F
        Patient.HeaderText = "Patient"
        Patient.MinimumWidth = 6
        Patient.Name = "Patient"
        Patient.ReadOnly = True
        Patient.Width = 211
        ' 
        ' Provider
        ' 
        Provider.FillWeight = 180F
        Provider.HeaderText = "Provider"
        Provider.MinimumWidth = 6
        Provider.Name = "Provider"
        Provider.ReadOnly = True
        Provider.Width = 280
        ' 
        ' BillType
        ' 
        BillType.HeaderText = "Billing"
        BillType.MinimumWidth = 6
        BillType.Name = "BillType"
        BillType.ReadOnly = True
        BillType.SortMode = DataGridViewColumnSortMode.NotSortable
        BillType.Visible = False
        BillType.Width = 125
        ' 
        ' dgvOrders
        ' 
        dgvOrders.AllowUserToAddRows = False
        dgvOrders.AllowUserToDeleteRows = False
        DataGridViewCellStyle3.BackColor = Color.LavenderBlush
        dgvOrders.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        dgvOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvOrders.Columns.AddRange(New DataGridViewColumn() {TGPID, TGPName, Stat})
        dgvOrders.Location = New Point(770, 773)
        dgvOrders.Margin = New Padding(5, 6, 5, 6)
        dgvOrders.Name = "dgvOrders"
        dgvOrders.ReadOnly = True
        dgvOrders.RowHeadersVisible = False
        dgvOrders.RowHeadersWidth = 51
        DataGridViewCellStyle4.BackColor = Color.Azure
        dgvOrders.RowsDefaultCellStyle = DataGridViewCellStyle4
        dgvOrders.Size = New Size(521, 236)
        dgvOrders.TabIndex = 3
        ' 
        ' TGPID
        ' 
        TGPID.FillWeight = 80F
        TGPID.HeaderText = "ID"
        TGPID.MinimumWidth = 6
        TGPID.Name = "TGPID"
        TGPID.ReadOnly = True
        TGPID.Width = 135
        ' 
        ' TGPName
        ' 
        TGPName.FillWeight = 180F
        TGPName.HeaderText = "Component"
        TGPName.MinimumWidth = 6
        TGPName.Name = "TGPName"
        TGPName.ReadOnly = True
        TGPName.Width = 302
        ' 
        ' Stat
        ' 
        Stat.FillWeight = 48F
        Stat.HeaderText = "Stat"
        Stat.MinimumWidth = 6
        Stat.Name = "Stat"
        Stat.ReadOnly = True
        Stat.Width = 81
        ' 
        ' txtBillee
        ' 
        txtBillee.BackColor = Color.Azure
        txtBillee.Location = New Point(770, 1103)
        txtBillee.Margin = New Padding(5, 6, 5, 6)
        txtBillee.Multiline = True
        txtBillee.Name = "txtBillee"
        txtBillee.ReadOnly = True
        txtBillee.ScrollBars = ScrollBars.Vertical
        txtBillee.Size = New Size(519, 104)
        txtBillee.TabIndex = 4
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.DarkBlue
        Label5.Location = New Point(795, 739)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(191, 28)
        Label5.TabIndex = 22
        Label5.Text = "Components Ordered"
        ' 
        ' lblResp
        ' 
        lblResp.ForeColor = Color.DarkBlue
        lblResp.Location = New Point(795, 1069)
        lblResp.Margin = New Padding(5, 0, 5, 0)
        lblResp.Name = "lblResp"
        lblResp.Size = New Size(246, 28)
        lblResp.TabIndex = 23
        lblResp.Text = "Responsible Party"
        ' 
        ' Label13
        ' 
        Label13.ForeColor = Color.DarkBlue
        Label13.Location = New Point(790, 300)
        Label13.Margin = New Padding(5, 0, 5, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(149, 28)
        Label13.TabIndex = 26
        Label13.Text = "Patient"
        ' 
        ' txtPatient
        ' 
        txtPatient.BackColor = Color.LavenderBlush
        txtPatient.Location = New Point(770, 334)
        txtPatient.Margin = New Padding(5, 6, 5, 6)
        txtPatient.Multiline = True
        txtPatient.Name = "txtPatient"
        txtPatient.ReadOnly = True
        txtPatient.ScrollBars = ScrollBars.Vertical
        txtPatient.Size = New Size(519, 82)
        txtPatient.TabIndex = 25
        ' 
        ' Label11
        ' 
        Label11.ForeColor = Color.DarkBlue
        Label11.Location = New Point(795, 461)
        Label11.Margin = New Padding(5, 0, 5, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(144, 28)
        Label11.TabIndex = 27
        Label11.Text = "Dx Code(s)"
        ' 
        ' txtDxs
        ' 
        txtDxs.BackColor = Color.Azure
        txtDxs.Location = New Point(770, 497)
        txtDxs.Margin = New Padding(5, 6, 5, 6)
        txtDxs.Multiline = True
        txtDxs.Name = "txtDxs"
        txtDxs.ReadOnly = True
        txtDxs.ScrollBars = ScrollBars.Vertical
        txtDxs.Size = New Size(519, 54)
        txtDxs.TabIndex = 28
        ' 
        ' txtSpecimen
        ' 
        txtSpecimen.BackColor = Color.LavenderBlush
        txtSpecimen.Location = New Point(770, 611)
        txtSpecimen.Margin = New Padding(5, 6, 5, 6)
        txtSpecimen.Multiline = True
        txtSpecimen.Name = "txtSpecimen"
        txtSpecimen.ReadOnly = True
        txtSpecimen.ScrollBars = ScrollBars.Vertical
        txtSpecimen.Size = New Size(519, 93)
        txtSpecimen.TabIndex = 29
        ' 
        ' Label14
        ' 
        Label14.ForeColor = Color.DarkBlue
        Label14.Location = New Point(795, 577)
        Label14.Margin = New Padding(5, 0, 5, 0)
        Label14.Name = "Label14"
        Label14.Size = New Size(191, 28)
        Label14.TabIndex = 30
        Label14.Text = "Specimen Contents"
        ' 
        ' txtAttendingProvider
        ' 
        txtAttendingProvider.BackColor = Color.Azure
        txtAttendingProvider.Location = New Point(770, 217)
        txtAttendingProvider.Margin = New Padding(5, 6, 5, 6)
        txtAttendingProvider.Multiline = True
        txtAttendingProvider.Name = "txtAttendingProvider"
        txtAttendingProvider.ReadOnly = True
        txtAttendingProvider.ScrollBars = ScrollBars.Vertical
        txtAttendingProvider.Size = New Size(519, 76)
        txtAttendingProvider.TabIndex = 31
        ' 
        ' Label15
        ' 
        Label15.ForeColor = Color.DarkBlue
        Label15.Location = New Point(951, 183)
        Label15.Margin = New Padding(5, 0, 5, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(176, 28)
        Label15.TabIndex = 32
        Label15.Text = "Attending Provider"
        ' 
        ' btnPrintDet
        ' 
        btnPrintDet.Enabled = False
        btnPrintDet.Location = New Point(555, 1117)
        btnPrintDet.Margin = New Padding(5, 6, 5, 6)
        btnPrintDet.Name = "btnPrintDet"
        btnPrintDet.Size = New Size(205, 59)
        btnPrintDet.TabIndex = 33
        btnPrintDet.Text = "Print Detail"
        btnPrintDet.UseVisualStyleBackColor = True
        ' 
        ' btnPrintSum
        ' 
        btnPrintSum.Enabled = False
        btnPrintSum.Location = New Point(25, 1122)
        btnPrintSum.Margin = New Padding(5, 6, 5, 6)
        btnPrintSum.Name = "btnPrintSum"
        btnPrintSum.Size = New Size(205, 59)
        btnPrintSum.TabIndex = 34
        btnPrintSum.Text = "Print Summary"
        btnPrintSum.UseVisualStyleBackColor = True
        ' 
        ' cmbDateType
        ' 
        cmbDateType.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDateType.FormattingEnabled = True
        cmbDateType.Items.AddRange(New Object() {"Accession Date", "Collection Date", "Receive Date"})
        cmbDateType.Location = New Point(194, 53)
        cmbDateType.Margin = New Padding(5, 6, 5, 6)
        cmbDateType.Name = "cmbDateType"
        cmbDateType.Size = New Size(145, 33)
        cmbDateType.TabIndex = 35
        ' 
        ' Label16
        ' 
        Label16.ForeColor = Color.DarkBlue
        Label16.Location = New Point(189, 17)
        Label16.Margin = New Padding(5, 0, 5, 0)
        Label16.Name = "Label16"
        Label16.Size = New Size(150, 28)
        Label16.TabIndex = 36
        Label16.Text = "Date Type"
        ' 
        ' cmbStatus
        ' 
        cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList
        cmbStatus.FormattingEnabled = True
        cmbStatus.Items.AddRange(New Object() {"Live", "Deleted", "Not Received"})
        cmbStatus.Location = New Point(26, 53)
        cmbStatus.Margin = New Padding(5, 6, 5, 6)
        cmbStatus.Name = "cmbStatus"
        cmbStatus.Size = New Size(155, 33)
        cmbStatus.TabIndex = 37
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(31, 17)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(126, 28)
        Label1.TabIndex = 38
        Label1.Text = "Status"
        ' 
        ' lblUser
        ' 
        lblUser.ForeColor = Color.Red
        lblUser.Location = New Point(345, 1108)
        lblUser.Margin = New Padding(5, 0, 5, 0)
        lblUser.Name = "lblUser"
        lblUser.Size = New Size(166, 28)
        lblUser.TabIndex = 39
        lblUser.Text = "Deleted by"
        lblUser.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtUser
        ' 
        txtUser.Location = New Point(331, 1142)
        txtUser.Margin = New Padding(5, 6, 5, 6)
        txtUser.Name = "txtUser"
        txtUser.ReadOnly = True
        txtUser.Size = New Size(194, 31)
        txtUser.TabIndex = 40
        txtUser.TextAlign = HorizontalAlignment.Center
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.ImageScalingSize = New Size(20, 20)
        StatusStrip1.Items.AddRange(New ToolStripItem() {lblStatus, PB})
        StatusStrip1.Location = New Point(0, 1232)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Padding = New Padding(1, 0, 24, 0)
        StatusStrip1.Size = New Size(1314, 52)
        StatusStrip1.TabIndex = 41
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = False
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(120, 45)
        ' 
        ' PB
        ' 
        PB.AutoSize = False
        PB.Name = "PB"
        PB.Size = New Size(1134, 44)
        PB.Style = ProgressBarStyle.Continuous
        ' 
        ' btnPmtReceipt
        ' 
        btnPmtReceipt.Location = New Point(1116, 1050)
        btnPmtReceipt.Margin = New Padding(5, 6, 5, 6)
        btnPmtReceipt.Name = "btnPmtReceipt"
        btnPmtReceipt.Size = New Size(174, 41)
        btnPmtReceipt.TabIndex = 42
        btnPmtReceipt.Text = "Payment Receipt"
        btnPmtReceipt.UseVisualStyleBackColor = True
        btnPmtReceipt.Visible = False
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(21, 447)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(476, 25)
        Label2.TabIndex = 44
        Label2.Text = "Tip:Double click on a column text to copy text to clipboard"
        ' 
        ' btnGo
        ' 
        btnGo.Image = CType(resources.GetObject("btnGo.Image"), Image)
        btnGo.Location = New Point(1226, 48)
        btnGo.Margin = New Padding(5, 6, 5, 6)
        btnGo.Name = "btnGo"
        btnGo.Size = New Size(64, 48)
        btnGo.TabIndex = 4
        btnGo.UseVisualStyleBackColor = True
        ' 
        ' openInRequi
        ' 
        openInRequi.Enabled = False
        openInRequi.Image = CType(resources.GetObject("openInRequi.Image"), Image)
        openInRequi.Location = New Point(600, 433)
        openInRequi.Margin = New Padding(5, 6, 5, 6)
        openInRequi.Name = "openInRequi"
        openInRequi.Size = New Size(89, 48)
        openInRequi.TabIndex = 46
        ToolTip1.SetToolTip(openInRequi, "Open in  Requisitions  Window")
        openInRequi.UseVisualStyleBackColor = True
        openInRequi.Visible = False
        ' 
        ' Label19
        ' 
        Label19.BackColor = SystemColors.Control
        Label19.Location = New Point(1086, 2)
        Label19.Margin = New Padding(4, 0, 4, 0)
        Label19.Name = "Label19"
        Label19.Size = New Size(41, 48)
        Label19.TabIndex = 88
        Label19.Text = "      "
        ' 
        ' frmAccDash
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1314, 1284)
        Controls.Add(Label19)
        Controls.Add(openInRequi)
        Controls.Add(Label2)
        Controls.Add(btnPmtReceipt)
        Controls.Add(StatusStrip1)
        Controls.Add(txtUser)
        Controls.Add(lblUser)
        Controls.Add(Label1)
        Controls.Add(cmbStatus)
        Controls.Add(Label16)
        Controls.Add(cmbDateType)
        Controls.Add(btnPrintSum)
        Controls.Add(btnPrintDet)
        Controls.Add(Label15)
        Controls.Add(txtAttendingProvider)
        Controls.Add(Label14)
        Controls.Add(txtSpecimen)
        Controls.Add(txtDxs)
        Controls.Add(Label11)
        Controls.Add(Label13)
        Controls.Add(txtPatient)
        Controls.Add(Label10)
        Controls.Add(txtTerm)
        Controls.Add(txtTotal)
        Controls.Add(lblTerm)
        Controls.Add(Label9)
        Controls.Add(dtpTo)
        Controls.Add(txt3PBilled)
        Controls.Add(cmbCriteria)
        Controls.Add(Label8)
        Controls.Add(dtpFrom)
        Controls.Add(txtPatBilled)
        Controls.Add(Label7)
        Controls.Add(btnGo)
        Controls.Add(txtClientBilled)
        Controls.Add(lblResp)
        Controls.Add(Label6)
        Controls.Add(Label3)
        Controls.Add(txtGratis)
        Controls.Add(Label5)
        Controls.Add(LabelP)
        Controls.Add(lblTo)
        Controls.Add(txtPatients)
        Controls.Add(txtBillee)
        Controls.Add(Label4)
        Controls.Add(txtProviders)
        Controls.Add(dgvOrders)
        Controls.Add(dgvAccessions)
        Controls.Add(lblFrom)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(1327, 1123)
        Name = "frmAccDash"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Accession Dashboard"
        CType(dgvAccessions, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvOrders, ComponentModel.ISupportInitialize).EndInit()
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents btnGo As System.Windows.Forms.Button
    Friend WithEvents txtProviders As System.Windows.Forms.TextBox
    Friend WithEvents cmbCriteria As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblTerm As System.Windows.Forms.Label
    Friend WithEvents txtTerm As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtClientBilled As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtGratis As System.Windows.Forms.TextBox
    Friend WithEvents LabelP As System.Windows.Forms.Label
    Friend WithEvents txtPatients As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txt3PBilled As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtPatBilled As System.Windows.Forms.TextBox
    Friend WithEvents dgvAccessions As System.Windows.Forms.DataGridView
    Friend WithEvents dgvOrders As System.Windows.Forms.DataGridView
    Friend WithEvents txtBillee As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblResp As System.Windows.Forms.Label
    Friend WithEvents TGPID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TGPName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Stat As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtPatient As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtDxs As System.Windows.Forms.TextBox
    Friend WithEvents txtSpecimen As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtAttendingProvider As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents btnPrintDet As System.Windows.Forms.Button
    Friend WithEvents btnPrintSum As System.Windows.Forms.Button
    Friend WithEvents cmbDateType As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents AccID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Dated As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Patient As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Provider As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BillType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblUser As System.Windows.Forms.Label
    Friend WithEvents txtUser As System.Windows.Forms.TextBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PB As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents btnPmtReceipt As System.Windows.Forms.Button
    'Friend WithEvents Chart1 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents openInRequi As System.Windows.Forms.Button
    Friend WithEvents Label19 As System.Windows.Forms.Label

End Class
