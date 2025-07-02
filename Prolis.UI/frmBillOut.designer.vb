<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBillOut
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBillOut))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        cmbBillingType = New ToolStripComboBox()
        ToolStripButton2 = New ToolStripSeparator()
        btnProcess = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        grpDates = New GroupBox()
        dtpDateTo = New DateTimePicker()
        chkSvcBill = New CheckBox()
        lblClearDates = New Label()
        Label2 = New Label()
        dtpDateFrom = New DateTimePicker()
        Label1 = New Label()
        btnRefresh = New Button()
        grpAccessions = New GroupBox()
        Label10 = New Label()
        txtEmailCount = New TextBox()
        Label9 = New Label()
        txtPrintCount = New TextBox()
        chkPlanned = New CheckBox()
        dgvAccessions = New DataGridView()
        Del = New DataGridViewCheckBoxColumn()
        Invoice = New DataGridViewTextBoxColumn()
        Patient = New DataGridViewTextBoxColumn()
        Sex = New DataGridViewTextBoxColumn()
        DOB = New DataGridViewTextBoxColumn()
        Address = New DataGridViewTextBoxColumn()
        Email = New DataGridViewTextBoxColumn()
        btnDeselAcc = New Button()
        btnSelAcc = New Button()
        grpProviders = New GroupBox()
        btnDeselProv = New Button()
        btnSelProv = New Button()
        lstProviders = New CheckedListBox()
        grpPayers = New GroupBox()
        btnTPACCS = New Button()
        btnDeselPayers = New Button()
        btnSelPayers = New Button()
        lstPayers = New CheckedListBox()
        grpPrice = New GroupBox()
        Label7 = New Label()
        chkINVLBL = New CheckBox()
        Label6 = New Label()
        txtCopies = New TextBox()
        chkInvs = New CheckBox()
        Label4 = New Label()
        cmbMedium = New ComboBox()
        txtAccTo = New TextBox()
        txtAccFrom = New TextBox()
        Label3 = New Label()
        Label5 = New Label()
        SaveFileDialog1 = New SaveFileDialog()
        Label8 = New Label()
        cmbCHP = New ComboBox()
        chkAccInv = New CheckBox()
        ToolStrip1.SuspendLayout()
        grpDates.SuspendLayout()
        grpAccessions.SuspendLayout()
        CType(dgvAccessions, ComponentModel.ISupportInitialize).BeginInit()
        grpProviders.SuspendLayout()
        grpPayers.SuspendLayout()
        grpPrice.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {cmbBillingType, ToolStripButton2, btnProcess, ToolStripSeparator1, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(1310, 34)
        ToolStrip1.TabIndex = 5
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' cmbBillingType
        ' 
        cmbBillingType.AutoSize = False
        cmbBillingType.DropDownStyle = ComboBoxStyle.DropDownList
        cmbBillingType.ForeColor = Color.DarkBlue
        cmbBillingType.Items.AddRange(New Object() {"Client Billing", "Third Party Billing", "Patient Billing"})
        cmbBillingType.Name = "cmbBillingType"
        cmbBillingType.Size = New Size(206, 33)
        ' 
        ' ToolStripButton2
        ' 
        ToolStripButton2.Name = "ToolStripButton2"
        ToolStripButton2.Size = New Size(6, 34)
        ' 
        ' btnProcess
        ' 
        btnProcess.Enabled = False
        btnProcess.ForeColor = Color.DarkBlue
        btnProcess.Image = CType(resources.GetObject("btnProcess.Image"), Image)
        btnProcess.ImageTransparentColor = Color.Magenta
        btnProcess.Name = "btnProcess"
        btnProcess.Size = New Size(100, 29)
        btnProcess.Text = "Process"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
        ' 
        ' btnCancel
        ' 
        btnCancel.ForeColor = Color.DarkBlue
        btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), Image)
        btnCancel.ImageTransparentColor = Color.Magenta
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(91, 29)
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
        btnHelp.ForeColor = Color.DarkBlue
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.ImageTransparentColor = Color.Magenta
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(65, 22)
        btnHelp.Text = "Help"
        ' 
        ' grpDates
        ' 
        grpDates.Controls.Add(dtpDateTo)
        grpDates.Controls.Add(chkSvcBill)
        grpDates.Controls.Add(lblClearDates)
        grpDates.Controls.Add(Label2)
        grpDates.Controls.Add(dtpDateFrom)
        grpDates.Controls.Add(Label1)
        grpDates.Location = New Point(20, 73)
        grpDates.Margin = New Padding(5, 6, 5, 6)
        grpDates.Name = "grpDates"
        grpDates.Padding = New Padding(5, 6, 5, 6)
        grpDates.Size = New Size(448, 119)
        grpDates.TabIndex = 6
        grpDates.TabStop = False
        grpDates.Text = "Billed Service Dates"
        ' 
        ' dtpDateTo
        ' 
        dtpDateTo.CustomFormat = " "
        dtpDateTo.Format = DateTimePickerFormat.Custom
        dtpDateTo.Location = New Point(303, 65)
        dtpDateTo.Margin = New Padding(3, 4, 3, 4)
        dtpDateTo.Name = "dtpDateTo"
        dtpDateTo.Size = New Size(129, 31)
        dtpDateTo.TabIndex = 95
        ' 
        ' chkSvcBill
        ' 
        chkSvcBill.Appearance = Appearance.Button
        chkSvcBill.Location = New Point(12, 56)
        chkSvcBill.Margin = New Padding(5, 6, 5, 6)
        chkSvcBill.Name = "chkSvcBill"
        chkSvcBill.Size = New Size(123, 52)
        chkSvcBill.TabIndex = 4
        chkSvcBill.Text = "SVC"
        chkSvcBill.TextAlign = ContentAlignment.MiddleCenter
        chkSvcBill.UseVisualStyleBackColor = True
        ' 
        ' lblClearDates
        ' 
        lblClearDates.BackColor = SystemColors.Control
        lblClearDates.Image = CType(resources.GetObject("lblClearDates.Image"), Image)
        lblClearDates.Location = New Point(402, 19)
        lblClearDates.Name = "lblClearDates"
        lblClearDates.Size = New Size(33, 46)
        lblClearDates.TabIndex = 96
        lblClearDates.Text = "      "
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.Magenta
        Label2.Location = New Point(298, 31)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(35, 25)
        Label2.TabIndex = 2
        Label2.Text = "To"
        ' 
        ' dtpDateFrom
        ' 
        dtpDateFrom.CustomFormat = " "
        dtpDateFrom.Format = DateTimePickerFormat.Custom
        dtpDateFrom.Location = New Point(153, 65)
        dtpDateFrom.Margin = New Padding(3, 4, 3, 4)
        dtpDateFrom.Name = "dtpDateFrom"
        dtpDateFrom.Size = New Size(129, 31)
        dtpDateFrom.TabIndex = 94
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.Red
        Label1.Location = New Point(148, 31)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(57, 25)
        Label1.TabIndex = 0
        Label1.Text = "From"
        Label1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' btnRefresh
        ' 
        btnRefresh.Location = New Point(503, 92)
        btnRefresh.Margin = New Padding(5, 6, 5, 6)
        btnRefresh.Name = "btnRefresh"
        btnRefresh.Size = New Size(182, 71)
        btnRefresh.TabIndex = 5
        btnRefresh.Text = "Detailed Selection"
        btnRefresh.UseVisualStyleBackColor = True
        ' 
        ' grpAccessions
        ' 
        grpAccessions.Controls.Add(Label10)
        grpAccessions.Controls.Add(txtEmailCount)
        grpAccessions.Controls.Add(Label9)
        grpAccessions.Controls.Add(txtPrintCount)
        grpAccessions.Controls.Add(chkPlanned)
        grpAccessions.Controls.Add(dgvAccessions)
        grpAccessions.Controls.Add(btnDeselAcc)
        grpAccessions.Controls.Add(btnSelAcc)
        grpAccessions.Location = New Point(478, 175)
        grpAccessions.Margin = New Padding(5, 6, 5, 6)
        grpAccessions.Name = "grpAccessions"
        grpAccessions.Padding = New Padding(5, 6, 5, 6)
        grpAccessions.Size = New Size(812, 712)
        grpAccessions.TabIndex = 12
        grpAccessions.TabStop = False
        grpAccessions.Text = "Billed Accession - Patient"
        ' 
        ' Label10
        ' 
        Label10.Location = New Point(437, 644)
        Label10.Margin = New Padding(5, 0, 5, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(117, 25)
        Label10.TabIndex = 12
        Label10.Text = "Email Count:"
        Label10.TextAlign = ContentAlignment.TopRight
        ' 
        ' txtEmailCount
        ' 
        txtEmailCount.Location = New Point(557, 638)
        txtEmailCount.Margin = New Padding(5, 6, 5, 6)
        txtEmailCount.Name = "txtEmailCount"
        txtEmailCount.ReadOnly = True
        txtEmailCount.Size = New Size(92, 31)
        txtEmailCount.TabIndex = 11
        txtEmailCount.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label9
        ' 
        Label9.Location = New Point(225, 642)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(103, 25)
        Label9.TabIndex = 10
        Label9.Text = "Print Count:"
        Label9.TextAlign = ContentAlignment.TopRight
        ' 
        ' txtPrintCount
        ' 
        txtPrintCount.Location = New Point(330, 638)
        txtPrintCount.Margin = New Padding(5, 6, 5, 6)
        txtPrintCount.Name = "txtPrintCount"
        txtPrintCount.ReadOnly = True
        txtPrintCount.Size = New Size(92, 31)
        txtPrintCount.TabIndex = 9
        txtPrintCount.TextAlign = HorizontalAlignment.Center
        ' 
        ' chkPlanned
        ' 
        chkPlanned.Appearance = Appearance.Button
        chkPlanned.Location = New Point(25, 631)
        chkPlanned.Margin = New Padding(5, 6, 5, 6)
        chkPlanned.Name = "chkPlanned"
        chkPlanned.Size = New Size(182, 52)
        chkPlanned.TabIndex = 8
        chkPlanned.Text = "Unplanned"
        chkPlanned.TextAlign = ContentAlignment.MiddleCenter
        chkPlanned.UseVisualStyleBackColor = True
        ' 
        ' dgvAccessions
        ' 
        dgvAccessions.AllowUserToAddRows = False
        dgvAccessions.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.Honeydew
        dgvAccessions.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvAccessions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvAccessions.Columns.AddRange(New DataGridViewColumn() {Del, Invoice, Patient, Sex, DOB, Address, Email})
        dgvAccessions.Location = New Point(25, 38)
        dgvAccessions.Margin = New Padding(5, 6, 5, 6)
        dgvAccessions.Name = "dgvAccessions"
        dgvAccessions.RowHeadersVisible = False
        dgvAccessions.RowHeadersWidth = 62
        dgvAccessions.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvAccessions.Size = New Size(762, 581)
        dgvAccessions.TabIndex = 7
        ' 
        ' Del
        ' 
        Del.FillWeight = 20.0F
        Del.HeaderText = ""
        Del.MinimumWidth = 8
        Del.Name = "Del"
        Del.Resizable = DataGridViewTriState.False
        Del.Width = 34
        ' 
        ' Invoice
        ' 
        Invoice.FillWeight = 60.0F
        Invoice.HeaderText = "Invoice"
        Invoice.MaxInputLength = 12
        Invoice.MinimumWidth = 8
        Invoice.Name = "Invoice"
        Invoice.ReadOnly = True
        Invoice.Resizable = DataGridViewTriState.True
        Invoice.Width = 102
        ' 
        ' Patient
        ' 
        Patient.FillWeight = 80.0F
        Patient.HeaderText = "Patient"
        Patient.MinimumWidth = 8
        Patient.Name = "Patient"
        Patient.ReadOnly = True
        Patient.Width = 137
        ' 
        ' Sex
        ' 
        Sex.FillWeight = 30.0F
        Sex.HeaderText = "Sex"
        Sex.MinimumWidth = 8
        Sex.Name = "Sex"
        Sex.ReadOnly = True
        Sex.SortMode = DataGridViewColumnSortMode.NotSortable
        Sex.Width = 51
        ' 
        ' DOB
        ' 
        DOB.FillWeight = 35.0F
        DOB.HeaderText = "DOB"
        DOB.MinimumWidth = 8
        DOB.Name = "DOB"
        DOB.ReadOnly = True
        DOB.SortMode = DataGridViewColumnSortMode.NotSortable
        DOB.Width = 60
        ' 
        ' Address
        ' 
        Address.FillWeight = 110.0F
        Address.HeaderText = "Address"
        Address.MinimumWidth = 8
        Address.Name = "Address"
        Address.ReadOnly = True
        Address.SortMode = DataGridViewColumnSortMode.NotSortable
        Address.Width = 187
        ' 
        ' Email
        ' 
        Email.FillWeight = 110.0F
        Email.HeaderText = "Email"
        Email.MinimumWidth = 8
        Email.Name = "Email"
        Email.ReadOnly = True
        Email.SortMode = DataGridViewColumnSortMode.NotSortable
        Email.Width = 188
        ' 
        ' btnDeselAcc
        ' 
        btnDeselAcc.Image = CType(resources.GetObject("btnDeselAcc.Image"), Image)
        btnDeselAcc.Location = New Point(742, 631)
        btnDeselAcc.Margin = New Padding(5, 6, 5, 6)
        btnDeselAcc.Name = "btnDeselAcc"
        btnDeselAcc.Size = New Size(45, 52)
        btnDeselAcc.TabIndex = 2
        btnDeselAcc.UseVisualStyleBackColor = True
        ' 
        ' btnSelAcc
        ' 
        btnSelAcc.Image = CType(resources.GetObject("btnSelAcc.Image"), Image)
        btnSelAcc.Location = New Point(683, 631)
        btnSelAcc.Margin = New Padding(5, 6, 5, 6)
        btnSelAcc.Name = "btnSelAcc"
        btnSelAcc.Size = New Size(48, 52)
        btnSelAcc.TabIndex = 1
        btnSelAcc.UseVisualStyleBackColor = True
        ' 
        ' grpProviders
        ' 
        grpProviders.Controls.Add(btnDeselProv)
        grpProviders.Controls.Add(btnSelProv)
        grpProviders.Controls.Add(lstProviders)
        grpProviders.Location = New Point(20, 329)
        grpProviders.Margin = New Padding(5, 6, 5, 6)
        grpProviders.Name = "grpProviders"
        grpProviders.Padding = New Padding(5, 6, 5, 6)
        grpProviders.Size = New Size(448, 325)
        grpProviders.TabIndex = 13
        grpProviders.TabStop = False
        grpProviders.Text = "Billed Providers"
        ' 
        ' btnDeselProv
        ' 
        btnDeselProv.Image = CType(resources.GetObject("btnDeselProv.Image"), Image)
        btnDeselProv.Location = New Point(380, 260)
        btnDeselProv.Margin = New Padding(5, 6, 5, 6)
        btnDeselProv.Name = "btnDeselProv"
        btnDeselProv.Size = New Size(43, 52)
        btnDeselProv.TabIndex = 2
        btnDeselProv.UseVisualStyleBackColor = True
        ' 
        ' btnSelProv
        ' 
        btnSelProv.Image = CType(resources.GetObject("btnSelProv.Image"), Image)
        btnSelProv.Location = New Point(13, 260)
        btnSelProv.Margin = New Padding(5, 6, 5, 6)
        btnSelProv.Name = "btnSelProv"
        btnSelProv.Size = New Size(48, 52)
        btnSelProv.TabIndex = 1
        btnSelProv.UseVisualStyleBackColor = True
        ' 
        ' lstProviders
        ' 
        lstProviders.FormattingEnabled = True
        lstProviders.Location = New Point(12, 38)
        lstProviders.Margin = New Padding(5, 6, 5, 6)
        lstProviders.Name = "lstProviders"
        lstProviders.Size = New Size(409, 200)
        lstProviders.Sorted = True
        lstProviders.TabIndex = 0
        ' 
        ' grpPayers
        ' 
        grpPayers.Controls.Add(btnTPACCS)
        grpPayers.Controls.Add(btnDeselPayers)
        grpPayers.Controls.Add(btnSelPayers)
        grpPayers.Controls.Add(lstPayers)
        grpPayers.Location = New Point(20, 694)
        grpPayers.Margin = New Padding(5, 6, 5, 6)
        grpPayers.Name = "grpPayers"
        grpPayers.Padding = New Padding(5, 6, 5, 6)
        grpPayers.Size = New Size(448, 388)
        grpPayers.TabIndex = 14
        grpPayers.TabStop = False
        grpPayers.Text = "Billed 3rd Parties"
        ' 
        ' btnTPACCS
        ' 
        btnTPACCS.Image = CType(resources.GetObject("btnTPACCS.Image"), Image)
        btnTPACCS.Location = New Point(355, 325)
        btnTPACCS.Margin = New Padding(5, 6, 5, 6)
        btnTPACCS.Name = "btnTPACCS"
        btnTPACCS.Size = New Size(68, 52)
        btnTPACCS.TabIndex = 9
        btnTPACCS.UseVisualStyleBackColor = True
        ' 
        ' btnDeselPayers
        ' 
        btnDeselPayers.Image = CType(resources.GetObject("btnDeselPayers.Image"), Image)
        btnDeselPayers.Location = New Point(193, 325)
        btnDeselPayers.Margin = New Padding(5, 6, 5, 6)
        btnDeselPayers.Name = "btnDeselPayers"
        btnDeselPayers.Size = New Size(48, 52)
        btnDeselPayers.TabIndex = 2
        btnDeselPayers.UseVisualStyleBackColor = True
        ' 
        ' btnSelPayers
        ' 
        btnSelPayers.Image = CType(resources.GetObject("btnSelPayers.Image"), Image)
        btnSelPayers.Location = New Point(13, 325)
        btnSelPayers.Margin = New Padding(5, 6, 5, 6)
        btnSelPayers.Name = "btnSelPayers"
        btnSelPayers.Size = New Size(48, 52)
        btnSelPayers.TabIndex = 1
        btnSelPayers.UseVisualStyleBackColor = True
        ' 
        ' lstPayers
        ' 
        lstPayers.FormattingEnabled = True
        lstPayers.Location = New Point(12, 38)
        lstPayers.Margin = New Padding(5, 6, 5, 6)
        lstPayers.Name = "lstPayers"
        lstPayers.Size = New Size(409, 256)
        lstPayers.Sorted = True
        lstPayers.TabIndex = 0
        ' 
        ' grpPrice
        ' 
        grpPrice.Controls.Add(Label7)
        grpPrice.Controls.Add(chkINVLBL)
        grpPrice.Controls.Add(Label6)
        grpPrice.Controls.Add(txtCopies)
        grpPrice.Controls.Add(chkInvs)
        grpPrice.Controls.Add(Label4)
        grpPrice.Controls.Add(cmbMedium)
        grpPrice.Location = New Point(478, 898)
        grpPrice.Margin = New Padding(5, 6, 5, 6)
        grpPrice.Name = "grpPrice"
        grpPrice.Padding = New Padding(5, 6, 5, 6)
        grpPrice.Size = New Size(812, 185)
        grpPrice.TabIndex = 16
        grpPrice.TabStop = False
        grpPrice.Text = "Output Selection"
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.Red
        Label7.Location = New Point(87, 106)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(607, 60)
        Label7.TabIndex = 10
        Label7.Text = "One label per patient Invoice is suggested where label quantity for Client and Insurance be selected carefully. Label format is 30 labels per sheet."
        Label7.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' chkINVLBL
        ' 
        chkINVLBL.Appearance = Appearance.Button
        chkINVLBL.Location = New Point(12, 48)
        chkINVLBL.Margin = New Padding(5, 6, 5, 6)
        chkINVLBL.Name = "chkINVLBL"
        chkINVLBL.Size = New Size(157, 46)
        chkINVLBL.TabIndex = 9
        chkINVLBL.Text = "Invoice"
        chkINVLBL.TextAlign = ContentAlignment.MiddleCenter
        chkINVLBL.UseVisualStyleBackColor = True
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.DarkBlue
        Label6.Location = New Point(498, 17)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(70, 31)
        Label6.TabIndex = 8
        Label6.Text = "Copies"
        Label6.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' txtCopies
        ' 
        txtCopies.Location = New Point(498, 54)
        txtCopies.Margin = New Padding(5, 6, 5, 6)
        txtCopies.MaxLength = 12
        txtCopies.Name = "txtCopies"
        txtCopies.Size = New Size(67, 31)
        txtCopies.TabIndex = 7
        txtCopies.TextAlign = HorizontalAlignment.Center
        ' 
        ' chkInvs
        ' 
        chkInvs.Appearance = Appearance.Button
        chkInvs.Location = New Point(593, 44)
        chkInvs.Margin = New Padding(5, 6, 5, 6)
        chkInvs.Name = "chkInvs"
        chkInvs.Size = New Size(193, 56)
        chkInvs.TabIndex = 4
        chkInvs.Text = "Non Zero Invoices"
        chkInvs.TextAlign = ContentAlignment.MiddleCenter
        chkInvs.UseVisualStyleBackColor = True
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(208, 17)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(80, 31)
        Label4.TabIndex = 3
        Label4.Text = "Medium"
        Label4.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' cmbMedium
        ' 
        cmbMedium.DropDownStyle = ComboBoxStyle.DropDownList
        cmbMedium.FormattingEnabled = True
        cmbMedium.Items.AddRange(New Object() {"Printer", "Screen", "Email"})
        cmbMedium.Location = New Point(198, 54)
        cmbMedium.Margin = New Padding(5, 6, 5, 6)
        cmbMedium.Name = "cmbMedium"
        cmbMedium.Size = New Size(259, 33)
        cmbMedium.TabIndex = 2
        ' 
        ' txtAccTo
        ' 
        txtAccTo.Location = New Point(1107, 125)
        txtAccTo.Margin = New Padding(5, 6, 5, 6)
        txtAccTo.MaxLength = 12
        txtAccTo.Name = "txtAccTo"
        txtAccTo.Size = New Size(156, 31)
        txtAccTo.TabIndex = 5
        txtAccTo.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtAccFrom
        ' 
        txtAccFrom.Location = New Point(938, 125)
        txtAccFrom.Margin = New Padding(5, 6, 5, 6)
        txtAccFrom.MaxLength = 12
        txtAccFrom.Name = "txtAccFrom"
        txtAccFrom.Size = New Size(156, 31)
        txtAccFrom.TabIndex = 4
        txtAccFrom.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.Magenta
        Label3.Location = New Point(1117, 87)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(35, 25)
        Label3.TabIndex = 2
        Label3.Text = "To"
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.Red
        Label5.Location = New Point(950, 87)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(57, 25)
        Label5.TabIndex = 0
        Label5.Text = "From"
        Label5.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label8
        ' 
        Label8.ForeColor = Color.DarkBlue
        Label8.Location = New Point(27, 231)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(292, 31)
        Label8.TabIndex = 19
        Label8.Text = "Clearing House Partner"
        Label8.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' cmbCHP
        ' 
        cmbCHP.FormattingEnabled = True
        cmbCHP.Location = New Point(33, 277)
        cmbCHP.Margin = New Padding(5, 6, 5, 6)
        cmbCHP.Name = "cmbCHP"
        cmbCHP.Size = New Size(407, 33)
        cmbCHP.Sorted = True
        cmbCHP.TabIndex = 18
        ' 
        ' chkAccInv
        ' 
        chkAccInv.Appearance = Appearance.Button
        chkAccInv.Location = New Point(755, 117)
        chkAccInv.Margin = New Padding(5, 6, 5, 6)
        chkAccInv.Name = "chkAccInv"
        chkAccInv.Size = New Size(173, 46)
        chkAccInv.TabIndex = 20
        chkAccInv.Text = "Accession Range"
        chkAccInv.TextAlign = ContentAlignment.MiddleCenter
        chkAccInv.UseVisualStyleBackColor = True
        ' 
        ' frmBillOut
        ' 
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1310, 1106)
        Controls.Add(chkAccInv)
        Controls.Add(txtAccTo)
        Controls.Add(Label5)
        Controls.Add(Label3)
        Controls.Add(Label8)
        Controls.Add(txtAccFrom)
        Controls.Add(cmbCHP)
        Controls.Add(btnRefresh)
        Controls.Add(grpPrice)
        Controls.Add(grpProviders)
        Controls.Add(grpPayers)
        Controls.Add(grpAccessions)
        Controls.Add(grpDates)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmBillOut"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Billing Output"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        grpDates.ResumeLayout(False)
        grpAccessions.ResumeLayout(False)
        grpAccessions.PerformLayout()
        CType(dgvAccessions, ComponentModel.ISupportInitialize).EndInit()
        grpProviders.ResumeLayout(False)
        grpPayers.ResumeLayout(False)
        grpPrice.ResumeLayout(False)
        grpPrice.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents cmbBillingType As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnProcess As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents grpDates As System.Windows.Forms.GroupBox
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grpAccessions As System.Windows.Forms.GroupBox
    Friend WithEvents btnDeselAcc As System.Windows.Forms.Button
    Friend WithEvents btnSelAcc As System.Windows.Forms.Button
    Friend WithEvents grpProviders As System.Windows.Forms.GroupBox
    Friend WithEvents btnDeselProv As System.Windows.Forms.Button
    Friend WithEvents btnSelProv As System.Windows.Forms.Button
    Friend WithEvents lstProviders As System.Windows.Forms.CheckedListBox
    Friend WithEvents grpPayers As System.Windows.Forms.GroupBox
    Friend WithEvents btnDeselPayers As System.Windows.Forms.Button
    Friend WithEvents btnSelPayers As System.Windows.Forms.Button
    Friend WithEvents lstPayers As System.Windows.Forms.CheckedListBox
    Friend WithEvents grpPrice As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbMedium As System.Windows.Forms.ComboBox
    Friend WithEvents chkInvs As System.Windows.Forms.CheckBox
    Friend WithEvents dgvAccessions As System.Windows.Forms.DataGridView
    Friend WithEvents txtAccTo As System.Windows.Forms.TextBox
    Friend WithEvents txtAccFrom As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtCopies As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbCHP As System.Windows.Forms.ComboBox
    Friend WithEvents btnTPACCS As System.Windows.Forms.Button
    Friend WithEvents chkSvcBill As System.Windows.Forms.CheckBox
    Friend WithEvents chkINVLBL As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents chkAccInv As System.Windows.Forms.CheckBox
    Friend WithEvents chkPlanned As System.Windows.Forms.CheckBox
    Friend WithEvents Del As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Invoice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Patient As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Sex As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DOB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Address As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Email As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtPrintCount As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtEmailCount As System.Windows.Forms.TextBox
    Friend WithEvents dtpDateTo As DateTimePicker
    Friend WithEvents lblClearDates As Label
    Friend WithEvents dtpDateFrom As DateTimePicker
End Class
