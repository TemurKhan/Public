<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmARInquiry
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmARInquiry))
        Me.cmbARType = New System.Windows.Forms.ComboBox()
        Me.cmbSearch = New System.Windows.Forms.ComboBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dgvDetail = New System.Windows.Forms.DataGridView()
        Me.txtAccs = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtPats = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtBilled = New System.Windows.Forms.TextBox()
        Me.txtPaid = New System.Windows.Forms.TextBox()
        Me.txtBal = New System.Windows.Forms.TextBox()
        Me.txtWO = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtAPD = New System.Windows.Forms.TextBox()
        Me.txt0_30 = New System.Windows.Forms.TextBox()
        Me.txt31_60 = New System.Windows.Forms.TextBox()
        Me.txt61_90 = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txt91Up = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtDocAmt = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtUnAppliedAmt = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtInvCount = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtPayer = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtSpeed = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.cmbDateType = New System.Windows.Forms.ComboBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.clipboardMsg = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.btnSummary = New System.Windows.Forms.Button()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.btnDetail = New System.Windows.Forms.Button()
        Me.btnGo = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.cnt = New System.Windows.Forms.TextBox()
        Me.prev = New System.Windows.Forms.Button()
        Me.nxt = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.searchID = New System.Windows.Forms.TextBox()
        Me.dtpDateTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpDateFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblClearDates = New System.Windows.Forms.Label()
        Me.Accession = New System.Windows.Forms.DataGridViewLinkColumn()
        Me.InvID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SvcDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BillTo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Patient = New System.Windows.Forms.DataGridViewLinkColumn()
        Me.BillAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DocNo = New System.Windows.Forms.DataGridViewLinkColumn()
        Me.TrnDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pmnt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Balance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Age = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbARType
        '
        Me.cmbARType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbARType.FormattingEnabled = True
        Me.cmbARType.Items.AddRange(New Object() {"Client", "Insurance", "Patient", "ALL"})
        Me.cmbARType.Location = New System.Drawing.Point(12, 36)
        Me.cmbARType.Name = "cmbARType"
        Me.cmbARType.Size = New System.Drawing.Size(101, 21)
        Me.cmbARType.TabIndex = 1
        '
        'cmbSearch
        '
        Me.cmbSearch.FormattingEnabled = True
        Me.cmbSearch.Items.AddRange(New Object() {"Provider/Entity ID", "Provider/Entity Name", "Insurance ID", "Insurance Name", "Patient ID", "Patient Name (Last, First)", "Accession Range (First, Last)", "Document ID", "Invoice ID", "CPT"})
        Me.cmbSearch.Location = New System.Drawing.Point(436, 36)
        Me.cmbSearch.Name = "cmbSearch"
        Me.cmbSearch.Size = New System.Drawing.Size(173, 21)
        Me.cmbSearch.TabIndex = 4
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(617, 36)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(136, 20)
        Me.txtSearch.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(21, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Billing Type"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(449, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Search Criteria"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(629, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(78, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Search Term"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(244, 14)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Date From"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(339, 14)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(46, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Date To"
        '
        'dgvDetail
        '
        Me.dgvDetail.AllowUserToAddRows = False
        Me.dgvDetail.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.MintCream
        Me.dgvDetail.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetail.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Accession, Me.InvID, Me.SvcDate, Me.BillTo, Me.Patient, Me.BillAmount, Me.DocNo, Me.TrnDate, Me.Pmnt, Me.WO, Me.Balance, Me.Age})
        Me.dgvDetail.Location = New System.Drawing.Point(10, 106)
        Me.dgvDetail.Name = "dgvDetail"
        Me.dgvDetail.ReadOnly = True
        Me.dgvDetail.RowHeadersVisible = False
        Me.dgvDetail.RowHeadersWidth = 51
        Me.dgvDetail.Size = New System.Drawing.Size(953, 444)
        Me.dgvDetail.TabIndex = 12
        '
        'txtAccs
        '
        Me.txtAccs.Location = New System.Drawing.Point(109, 580)
        Me.txtAccs.Name = "txtAccs"
        Me.txtAccs.ReadOnly = True
        Me.txtAccs.Size = New System.Drawing.Size(84, 20)
        Me.txtAccs.TabIndex = 15
        Me.txtAccs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(109, 561)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(84, 13)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Accessions"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtPats
        '
        Me.txtPats.Location = New System.Drawing.Point(199, 580)
        Me.txtPats.Name = "txtPats"
        Me.txtPats.ReadOnly = True
        Me.txtPats.Size = New System.Drawing.Size(84, 20)
        Me.txtPats.TabIndex = 17
        Me.txtPats.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(199, 561)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(84, 13)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Patients"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(291, 561)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(85, 13)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "Billed"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(384, 561)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(85, 13)
        Me.Label10.TabIndex = 22
        Me.Label10.Text = "Payments"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(591, 561)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(102, 13)
        Me.Label12.TabIndex = 24
        Me.Label12.Text = "Balance"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtBilled
        '
        Me.txtBilled.Location = New System.Drawing.Point(289, 580)
        Me.txtBilled.Name = "txtBilled"
        Me.txtBilled.ReadOnly = True
        Me.txtBilled.Size = New System.Drawing.Size(87, 20)
        Me.txtBilled.TabIndex = 25
        Me.txtBilled.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtPaid
        '
        Me.txtPaid.Location = New System.Drawing.Point(382, 580)
        Me.txtPaid.Name = "txtPaid"
        Me.txtPaid.ReadOnly = True
        Me.txtPaid.Size = New System.Drawing.Size(87, 20)
        Me.txtPaid.TabIndex = 26
        Me.txtPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtBal
        '
        Me.txtBal.Location = New System.Drawing.Point(591, 580)
        Me.txtBal.Name = "txtBal"
        Me.txtBal.ReadOnly = True
        Me.txtBal.Size = New System.Drawing.Size(102, 20)
        Me.txtBal.TabIndex = 28
        Me.txtBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtWO
        '
        Me.txtWO.Location = New System.Drawing.Point(475, 580)
        Me.txtWO.Name = "txtWO"
        Me.txtWO.ReadOnly = True
        Me.txtWO.Size = New System.Drawing.Size(110, 20)
        Me.txtWO.TabIndex = 31
        Me.txtWO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(491, 561)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(81, 13)
        Me.Label8.TabIndex = 32
        Me.Label8.Text = "Written Off"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtAPD
        '
        Me.txtAPD.ForeColor = System.Drawing.Color.DarkBlue
        Me.txtAPD.Location = New System.Drawing.Point(109, 627)
        Me.txtAPD.Name = "txtAPD"
        Me.txtAPD.ReadOnly = True
        Me.txtAPD.Size = New System.Drawing.Size(113, 20)
        Me.txtAPD.TabIndex = 33
        Me.txtAPD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt0_30
        '
        Me.txt0_30.ForeColor = System.Drawing.Color.DarkGreen
        Me.txt0_30.Location = New System.Drawing.Point(242, 627)
        Me.txt0_30.Name = "txt0_30"
        Me.txt0_30.ReadOnly = True
        Me.txt0_30.Size = New System.Drawing.Size(145, 20)
        Me.txt0_30.TabIndex = 34
        Me.txt0_30.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt31_60
        '
        Me.txt31_60.ForeColor = System.Drawing.Color.Maroon
        Me.txt31_60.Location = New System.Drawing.Point(402, 627)
        Me.txt31_60.Name = "txt31_60"
        Me.txt31_60.ReadOnly = True
        Me.txt31_60.Size = New System.Drawing.Size(145, 20)
        Me.txt31_60.TabIndex = 35
        Me.txt31_60.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt61_90
        '
        Me.txt61_90.ForeColor = System.Drawing.Color.Maroon
        Me.txt61_90.Location = New System.Drawing.Point(562, 627)
        Me.txt61_90.Name = "txt61_90"
        Me.txt61_90.ReadOnly = True
        Me.txt61_90.Size = New System.Drawing.Size(145, 20)
        Me.txt61_90.TabIndex = 36
        Me.txt61_90.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label11
        '
        Me.Label11.ForeColor = System.Drawing.Color.DarkGreen
        Me.Label11.Location = New System.Drawing.Point(245, 611)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(142, 13)
        Me.Label11.TabIndex = 37
        Me.Label11.Text = "0 - 30 Days"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label13
        '
        Me.Label13.ForeColor = System.Drawing.Color.Maroon
        Me.Label13.Location = New System.Drawing.Point(402, 611)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(145, 13)
        Me.Label13.TabIndex = 38
        Me.Label13.Text = "31 - 60 Days"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txt91Up
        '
        Me.txt91Up.ForeColor = System.Drawing.Color.Red
        Me.txt91Up.Location = New System.Drawing.Point(722, 627)
        Me.txt91Up.Name = "txt91Up"
        Me.txt91Up.ReadOnly = True
        Me.txt91Up.Size = New System.Drawing.Size(145, 20)
        Me.txt91Up.TabIndex = 39
        Me.txt91Up.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label14
        '
        Me.Label14.ForeColor = System.Drawing.Color.Maroon
        Me.Label14.Location = New System.Drawing.Point(562, 611)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(145, 13)
        Me.Label14.TabIndex = 40
        Me.Label14.Text = "61 - 90 Days"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label15
        '
        Me.Label15.ForeColor = System.Drawing.Color.Red
        Me.Label15.Location = New System.Drawing.Point(722, 611)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(142, 13)
        Me.Label15.TabIndex = 41
        Me.Label15.Text = "91 Days and Up"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label16
        '
        Me.Label16.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label16.Location = New System.Drawing.Point(109, 611)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(113, 13)
        Me.Label16.TabIndex = 42
        Me.Label16.Text = "Ave Pay Days"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtDocAmt
        '
        Me.txtDocAmt.Location = New System.Drawing.Point(323, 63)
        Me.txtDocAmt.Name = "txtDocAmt"
        Me.txtDocAmt.ReadOnly = True
        Me.txtDocAmt.Size = New System.Drawing.Size(82, 20)
        Me.txtDocAmt.TabIndex = 43
        Me.txtDocAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(239, 67)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(78, 13)
        Me.Label17.TabIndex = 44
        Me.Label17.Text = "Doc Amount:"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.Location = New System.Drawing.Point(457, 68)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(66, 13)
        Me.Label18.TabIndex = 46
        Me.Label18.Text = "Unapplied:"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtUnAppliedAmt
        '
        Me.txtUnAppliedAmt.ForeColor = System.Drawing.Color.Red
        Me.txtUnAppliedAmt.Location = New System.Drawing.Point(529, 63)
        Me.txtUnAppliedAmt.Name = "txtUnAppliedAmt"
        Me.txtUnAppliedAmt.ReadOnly = True
        Me.txtUnAppliedAmt.Size = New System.Drawing.Size(82, 20)
        Me.txtUnAppliedAmt.TabIndex = 45
        Me.txtUnAppliedAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(653, 68)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(99, 13)
        Me.Label19.TabIndex = 48
        Me.Label19.Text = "Invoices affected:"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtInvCount
        '
        Me.txtInvCount.Location = New System.Drawing.Point(759, 63)
        Me.txtInvCount.Name = "txtInvCount"
        Me.txtInvCount.ReadOnly = True
        Me.txtInvCount.Size = New System.Drawing.Size(82, 20)
        Me.txtInvCount.TabIndex = 47
        Me.txtInvCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label20
        '
        Me.Label20.Location = New System.Drawing.Point(12, 67)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(40, 13)
        Me.Label20.TabIndex = 50
        Me.Label20.Text = "Payer:"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtPayer
        '
        Me.txtPayer.Location = New System.Drawing.Point(58, 63)
        Me.txtPayer.Name = "txtPayer"
        Me.txtPayer.ReadOnly = True
        Me.txtPayer.Size = New System.Drawing.Size(164, 20)
        Me.txtPayer.TabIndex = 49
        '
        'Label21
        '
        Me.Label21.Location = New System.Drawing.Point(12, 561)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(91, 13)
        Me.Label21.TabIndex = 52
        Me.Label21.Text = "Speed"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtSpeed
        '
        Me.txtSpeed.Location = New System.Drawing.Point(10, 580)
        Me.txtSpeed.Name = "txtSpeed"
        Me.txtSpeed.ReadOnly = True
        Me.txtSpeed.Size = New System.Drawing.Size(93, 20)
        Me.txtSpeed.TabIndex = 51
        Me.txtSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label22
        '
        Me.Label22.Location = New System.Drawing.Point(7, 626)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(92, 13)
        Me.Label22.TabIndex = 54
        Me.Label22.Text = "AGING ->"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbDateType
        '
        Me.cmbDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDateType.FormattingEnabled = True
        Me.cmbDateType.Items.AddRange(New Object() {"Bill", "Receive", "Service"})
        Me.cmbDateType.Location = New System.Drawing.Point(121, 35)
        Me.cmbDateType.Name = "cmbDateType"
        Me.cmbDateType.Size = New System.Drawing.Size(101, 21)
        Me.cmbDateType.Sorted = True
        Me.cmbDateType.TabIndex = 55
        '
        'Label23
        '
        Me.Label23.Location = New System.Drawing.Point(127, 14)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(78, 13)
        Me.Label23.TabIndex = 56
        Me.Label23.Text = "Date Type"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(10, 659)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(859, 10)
        Me.ProgressBar1.TabIndex = 57
        '
        'clipboardMsg
        '
        Me.clipboardMsg.AutoSize = True
        Me.clipboardMsg.Location = New System.Drawing.Point(12, 86)
        Me.clipboardMsg.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.clipboardMsg.Name = "clipboardMsg"
        Me.clipboardMsg.Size = New System.Drawing.Size(0, 13)
        Me.clipboardMsg.TabIndex = 58
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Button1
        '
        Me.Button1.Image = My.Resources.Resources.excel
        Me.Button1.Location = New System.Drawing.Point(860, 573)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(40, 41)
        Me.Button1.TabIndex = 85
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.Button1, "Save the aboove data into Excel file")
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Image = My.Resources.Resources.paste
        Me.Label24.Location = New System.Drawing.Point(697, 7)
        Me.Label24.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(25, 25)
        Me.Label24.TabIndex = 84
        Me.Label24.Text = "      "
        Me.ToolTip1.SetToolTip(Me.Label24, "Click to paste text")
        '
        'btnSummary
        '
        Me.btnSummary.Image = CType(resources.GetObject("btnSummary.Image"), System.Drawing.Image)
        Me.btnSummary.Location = New System.Drawing.Point(773, 575)
        Me.btnSummary.Name = "btnSummary"
        Me.btnSummary.Size = New System.Drawing.Size(81, 28)
        Me.btnSummary.TabIndex = 29
        Me.btnSummary.Text = "Summary"
        Me.btnSummary.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSummary.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSummary.UseVisualStyleBackColor = True
        '
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.Location = New System.Drawing.Point(849, 31)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(23, 27)
        Me.btnHelp.TabIndex = 14
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnHelp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'btnDetail
        '
        Me.btnDetail.Enabled = False
        Me.btnDetail.Image = CType(resources.GetObject("btnDetail.Image"), System.Drawing.Image)
        Me.btnDetail.Location = New System.Drawing.Point(699, 575)
        Me.btnDetail.Name = "btnDetail"
        Me.btnDetail.Size = New System.Drawing.Size(68, 28)
        Me.btnDetail.TabIndex = 13
        Me.btnDetail.Text = "Detail"
        Me.btnDetail.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnDetail.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnDetail.UseVisualStyleBackColor = True
        '
        'btnGo
        '
        Me.btnGo.Image = CType(resources.GetObject("btnGo.Image"), System.Drawing.Image)
        Me.btnGo.Location = New System.Drawing.Point(759, 30)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(81, 31)
        Me.btnGo.TabIndex = 6
        Me.btnGo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnGo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnGo.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Button4)
        Me.Panel1.Controls.Add(Me.cnt)
        Me.Panel1.Controls.Add(Me.prev)
        Me.Panel1.Controls.Add(Me.nxt)
        Me.Panel1.Controls.Add(Me.Button3)
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Location = New System.Drawing.Point(111, 296)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(635, 34)
        Me.Panel1.TabIndex = 92
        Me.Panel1.Visible = False
        '
        'Button4
        '
        Me.Button4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.Image = My.Resources.Resources.icons8_delete_24
        Me.Button4.Location = New System.Drawing.Point(392, 2)
        Me.Button4.Margin = New System.Windows.Forms.Padding(2)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(36, 28)
        Me.Button4.TabIndex = 97
        Me.Button4.UseVisualStyleBackColor = True
        '
        'cnt
        '
        Me.cnt.Location = New System.Drawing.Point(207, 12)
        Me.cnt.Name = "cnt"
        Me.cnt.ReadOnly = True
        Me.cnt.Size = New System.Drawing.Size(82, 20)
        Me.cnt.TabIndex = 96
        Me.cnt.Text = "0"
        Me.cnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'prev
        '
        Me.prev.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.prev.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.prev.Location = New System.Drawing.Point(294, 2)
        Me.prev.Name = "prev"
        Me.prev.Size = New System.Drawing.Size(38, 31)
        Me.prev.TabIndex = 95
        Me.prev.Text = "«"
        Me.prev.UseVisualStyleBackColor = True
        '
        'nxt
        '
        Me.nxt.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.nxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nxt.Location = New System.Drawing.Point(338, 2)
        Me.nxt.Name = "nxt"
        Me.nxt.Size = New System.Drawing.Size(38, 31)
        Me.nxt.TabIndex = 94
        Me.nxt.Text = "»"
        Me.nxt.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Image = My.Resources.Resources.ViewHistory
        Me.Button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button3.Location = New System.Drawing.Point(104, 2)
        Me.Button3.Margin = New System.Windows.Forms.Padding(2)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(88, 28)
        Me.Button3.TabIndex = 93
        Me.Button3.Text = "Select File:"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Image = My.Resources.Resources.ViewHistory
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button2.Location = New System.Drawing.Point(449, 4)
        Me.Button2.Margin = New System.Windows.Forms.Padding(2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(81, 26)
        Me.Button2.TabIndex = 92
        Me.Button2.Text = "adjust"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'searchID
        '
        Me.searchID.Location = New System.Drawing.Point(529, 9)
        Me.searchID.Name = "searchID"
        Me.searchID.ReadOnly = True
        Me.searchID.Size = New System.Drawing.Size(82, 20)
        Me.searchID.TabIndex = 93
        Me.searchID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.searchID.Visible = False
        '
        'dtpDateTo
        '
        Me.dtpDateTo.CustomFormat = " "
        Me.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateTo.Location = New System.Drawing.Point(323, 35)
        Me.dtpDateTo.Margin = New System.Windows.Forms.Padding(2)
        Me.dtpDateTo.Name = "dtpDateTo"
        Me.dtpDateTo.Size = New System.Drawing.Size(79, 20)
        Me.dtpDateTo.TabIndex = 95
        '
        'dtpDateFrom
        '
        Me.dtpDateFrom.CustomFormat = " "
        Me.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateFrom.Location = New System.Drawing.Point(233, 35)
        Me.dtpDateFrom.Margin = New System.Windows.Forms.Padding(2)
        Me.dtpDateFrom.Name = "dtpDateFrom"
        Me.dtpDateFrom.Size = New System.Drawing.Size(79, 20)
        Me.dtpDateFrom.TabIndex = 94
        '
        'lblClearDates
        '
        Me.lblClearDates.BackColor = System.Drawing.SystemColors.Control
        Me.lblClearDates.Image = My.Resources.Resources.Eraser
        Me.lblClearDates.Location = New System.Drawing.Point(406, 33)
        Me.lblClearDates.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblClearDates.Name = "lblClearDates"
        Me.lblClearDates.Size = New System.Drawing.Size(20, 24)
        Me.lblClearDates.TabIndex = 96
        Me.lblClearDates.Text = "      "
        '
        'Accession
        '
        Me.Accession.FillWeight = 80.0!
        Me.Accession.HeaderText = "Accession"
        Me.Accession.MinimumWidth = 6
        Me.Accession.Name = "Accession"
        Me.Accession.ReadOnly = True
        Me.Accession.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Accession.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Accession.Width = 80
        '
        'InvID
        '
        Me.InvID.FillWeight = 66.0!
        Me.InvID.HeaderText = "Invoice"
        Me.InvID.MaxInputLength = 15
        Me.InvID.MinimumWidth = 6
        Me.InvID.Name = "InvID"
        Me.InvID.ReadOnly = True
        Me.InvID.Width = 66
        '
        'SvcDate
        '
        Me.SvcDate.FillWeight = 76.0!
        Me.SvcDate.HeaderText = "Svc Date"
        Me.SvcDate.MinimumWidth = 6
        Me.SvcDate.Name = "SvcDate"
        Me.SvcDate.ReadOnly = True
        Me.SvcDate.Width = 76
        '
        'BillTo
        '
        Me.BillTo.FillWeight = 94.0!
        Me.BillTo.HeaderText = "Bill To"
        Me.BillTo.MaxInputLength = 100
        Me.BillTo.MinimumWidth = 6
        Me.BillTo.Name = "BillTo"
        Me.BillTo.ReadOnly = True
        Me.BillTo.Width = 94
        '
        'Patient
        '
        Me.Patient.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Patient.FillWeight = 94.0!
        Me.Patient.HeaderText = "Patient"
        Me.Patient.MinimumWidth = 6
        Me.Patient.Name = "Patient"
        Me.Patient.ReadOnly = True
        Me.Patient.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Patient.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'BillAmount
        '
        Me.BillAmount.FillWeight = 66.0!
        Me.BillAmount.HeaderText = "Bill Amt"
        Me.BillAmount.MaxInputLength = 12
        Me.BillAmount.MinimumWidth = 6
        Me.BillAmount.Name = "BillAmount"
        Me.BillAmount.ReadOnly = True
        Me.BillAmount.Width = 66
        '
        'DocNo
        '
        Me.DocNo.FillWeight = 64.0!
        Me.DocNo.HeaderText = "Doc No"
        Me.DocNo.MinimumWidth = 6
        Me.DocNo.Name = "DocNo"
        Me.DocNo.ReadOnly = True
        Me.DocNo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DocNo.Width = 64
        '
        'TrnDate
        '
        Me.TrnDate.FillWeight = 74.0!
        Me.TrnDate.HeaderText = "Trn Date"
        Me.TrnDate.MinimumWidth = 6
        Me.TrnDate.Name = "TrnDate"
        Me.TrnDate.ReadOnly = True
        Me.TrnDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TrnDate.Width = 74
        '
        'Pmnt
        '
        Me.Pmnt.FillWeight = 64.0!
        Me.Pmnt.HeaderText = "Payment"
        Me.Pmnt.MaxInputLength = 12
        Me.Pmnt.MinimumWidth = 6
        Me.Pmnt.Name = "Pmnt"
        Me.Pmnt.ReadOnly = True
        Me.Pmnt.Width = 64
        '
        'WO
        '
        Me.WO.FillWeight = 64.0!
        Me.WO.HeaderText = "W.O."
        Me.WO.MaxInputLength = 12
        Me.WO.MinimumWidth = 6
        Me.WO.Name = "WO"
        Me.WO.ReadOnly = True
        Me.WO.Width = 64
        '
        'Balance
        '
        Me.Balance.FillWeight = 70.0!
        Me.Balance.HeaderText = "Balance"
        Me.Balance.MaxInputLength = 12
        Me.Balance.MinimumWidth = 6
        Me.Balance.Name = "Balance"
        Me.Balance.ReadOnly = True
        Me.Balance.Width = 70
        '
        'Age
        '
        Me.Age.FillWeight = 40.0!
        Me.Age.HeaderText = "Age"
        Me.Age.MaxInputLength = 5
        Me.Age.MinimumWidth = 6
        Me.Age.Name = "Age"
        Me.Age.ReadOnly = True
        Me.Age.Width = 40
        '
        'frmARInquiry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(973, 673)
        Me.Controls.Add(Me.lblClearDates)
        Me.Controls.Add(Me.dtpDateTo)
        Me.Controls.Add(Me.dtpDateFrom)
        Me.Controls.Add(Me.searchID)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.clipboardMsg)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.cmbDateType)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.txtSpeed)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.txtPayer)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.txtInvCount)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.txtUnAppliedAmt)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.txtDocAmt)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txt91Up)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txt61_90)
        Me.Controls.Add(Me.txt31_60)
        Me.Controls.Add(Me.txt0_30)
        Me.Controls.Add(Me.txtAPD)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtWO)
        Me.Controls.Add(Me.btnSummary)
        Me.Controls.Add(Me.txtBal)
        Me.Controls.Add(Me.txtPaid)
        Me.Controls.Add(Me.txtBilled)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtPats)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtAccs)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.btnDetail)
        Me.Controls.Add(Me.dgvDetail)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnGo)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.cmbSearch)
        Me.Controls.Add(Me.cmbARType)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(758, 597)
        Me.Name = "frmARInquiry"
        Me.Text = "AR Inquiry"
        CType(Me.dgvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbARType As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSearch As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnGo As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dgvDetail As System.Windows.Forms.DataGridView
    Friend WithEvents btnDetail As System.Windows.Forms.Button
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents txtAccs As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtPats As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtBilled As System.Windows.Forms.TextBox
    Friend WithEvents txtPaid As System.Windows.Forms.TextBox
    Friend WithEvents txtBal As System.Windows.Forms.TextBox
    Friend WithEvents btnSummary As System.Windows.Forms.Button
    Friend WithEvents txtWO As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtAPD As System.Windows.Forms.TextBox
    Friend WithEvents txt0_30 As System.Windows.Forms.TextBox
    Friend WithEvents txt31_60 As System.Windows.Forms.TextBox
    Friend WithEvents txt61_90 As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txt91Up As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtDocAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtUnAppliedAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtInvCount As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtPayer As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtSpeed As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents cmbDateType As System.Windows.Forms.ComboBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents clipboardMsg As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents cnt As System.Windows.Forms.TextBox
    Friend WithEvents prev As System.Windows.Forms.Button
    Friend WithEvents nxt As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents searchID As TextBox
    Friend WithEvents dtpDateTo As DateTimePicker
    Friend WithEvents dtpDateFrom As DateTimePicker
    Friend WithEvents lblClearDates As Label
    Friend WithEvents Accession As DataGridViewLinkColumn
    Friend WithEvents InvID As DataGridViewTextBoxColumn
    Friend WithEvents SvcDate As DataGridViewTextBoxColumn
    Friend WithEvents BillTo As DataGridViewTextBoxColumn
    Friend WithEvents Patient As DataGridViewLinkColumn
    Friend WithEvents BillAmount As DataGridViewTextBoxColumn
    Friend WithEvents DocNo As DataGridViewLinkColumn
    Friend WithEvents TrnDate As DataGridViewTextBoxColumn
    Friend WithEvents Pmnt As DataGridViewTextBoxColumn
    Friend WithEvents WO As DataGridViewTextBoxColumn
    Friend WithEvents Balance As DataGridViewTextBoxColumn
    Friend WithEvents Age As DataGridViewTextBoxColumn
End Class
