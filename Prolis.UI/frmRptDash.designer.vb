<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptDash
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
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRptDash))
        'Dim ChartArea5 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        'Dim Legend5 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        'Dim Series5 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtPatient = New System.Windows.Forms.TextBox()
        Me.txtTerm = New System.Windows.Forms.TextBox()
        Me.lblTerm = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.txtSuccessFax = New System.Windows.Forms.TextBox()
        Me.cmbCriteria = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.txtFailFax = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtProlisOnPrinted = New System.Windows.Forms.TextBox()
        Me.lblResp = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtEmailed = New System.Windows.Forms.TextBox()
        Me.LabelP = New System.Windows.Forms.Label()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.txtProvider = New System.Windows.Forms.TextBox()
        Me.txtView = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtLabPrinted = New System.Windows.Forms.TextBox()
        Me.dgvAccessions = New System.Windows.Forms.DataGridView()
        Me.AccID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Dated = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Source = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ReDo = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtInitial = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtPartial = New System.Windows.Forms.TextBox()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.txtProcessDetail = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.chkPUnP = New System.Windows.Forms.CheckBox()
        Me.btnRedo = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnSellAll = New System.Windows.Forms.Button()
        Me.btnDeselAll = New System.Windows.Forms.Button()
        Me.txtHL7 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtPDF = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtFinal = New System.Windows.Forms.TextBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.PB = New System.Windows.Forms.ToolStripProgressBar()
        'Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.AccIds = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbDestination = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnGo = New System.Windows.Forms.Button()
        Me.BW = New System.ComponentModel.BackgroundWorker()
        Me.waitingLabel = New System.Windows.Forms.Label()
        CType(Me.dgvAccessions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        'CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label13
        '
        Me.Label13.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label13.Location = New System.Drawing.Point(596, 214)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(112, 15)
        Me.Label13.TabIndex = 61
        Me.Label13.Text = "Patient"
        '
        'txtPatient
        '
        Me.txtPatient.BackColor = System.Drawing.Color.LavenderBlush
        Me.txtPatient.Location = New System.Drawing.Point(514, 232)
        Me.txtPatient.Multiline = True
        Me.txtPatient.Name = "txtPatient"
        Me.txtPatient.ReadOnly = True
        Me.txtPatient.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPatient.Size = New System.Drawing.Size(307, 64)
        Me.txtPatient.TabIndex = 60
        '
        'txtTerm
        '
        Me.txtTerm.Location = New System.Drawing.Point(590, 26)
        Me.txtTerm.Name = "txtTerm"
        Me.txtTerm.Size = New System.Drawing.Size(188, 20)
        Me.txtTerm.TabIndex = 42
        '
        'lblTerm
        '
        Me.lblTerm.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblTerm.Location = New System.Drawing.Point(596, 9)
        Me.lblTerm.Name = "lblTerm"
        Me.lblTerm.Size = New System.Drawing.Size(108, 15)
        Me.lblTerm.TabIndex = 43
        Me.lblTerm.Text = "Search Term"
        '
        'Label9
        '
        Me.Label9.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label9.Location = New System.Drawing.Point(510, 112)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(74, 15)
        Me.Label9.TabIndex = 54
        Me.Label9.Text = "Fax-Success"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'dtpTo
        '
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTo.Location = New System.Drawing.Point(144, 27)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(116, 20)
        Me.dtpTo.TabIndex = 33
        '
        'txtSuccessFax
        '
        Me.txtSuccessFax.Location = New System.Drawing.Point(510, 130)
        Me.txtSuccessFax.Name = "txtSuccessFax"
        Me.txtSuccessFax.ReadOnly = True
        Me.txtSuccessFax.Size = New System.Drawing.Size(74, 20)
        Me.txtSuccessFax.TabIndex = 53
        Me.txtSuccessFax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmbCriteria
        '
        Me.cmbCriteria.FormattingEnabled = True
        Me.cmbCriteria.Items.AddRange(New Object() {"Accession ID", "Provider ID", "Provider Name (Last, First)", "Patient ID", "Patient Name (Last, First)"})
        Me.cmbCriteria.Location = New System.Drawing.Point(400, 26)
        Me.cmbCriteria.Name = "cmbCriteria"
        Me.cmbCriteria.Size = New System.Drawing.Size(175, 21)
        Me.cmbCriteria.TabIndex = 40
        '
        'Label8
        '
        Me.Label8.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label8.Location = New System.Drawing.Point(750, 161)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(74, 15)
        Me.Label8.TabIndex = 52
        Me.Label8.Text = "Faxed-Fail"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'dtpFrom
        '
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFrom.Location = New System.Drawing.Point(16, 27)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(122, 20)
        Me.dtpFrom.TabIndex = 32
        '
        'txtFailFax
        '
        Me.txtFailFax.Location = New System.Drawing.Point(750, 179)
        Me.txtFailFax.Name = "txtFailFax"
        Me.txtFailFax.ReadOnly = True
        Me.txtFailFax.Size = New System.Drawing.Size(74, 20)
        Me.txtFailFax.TabIndex = 51
        Me.txtFailFax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label7.Location = New System.Drawing.Point(670, 161)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 15)
        Me.Label7.TabIndex = 50
        Me.Label7.Text = "ProlisOn"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtProlisOnPrinted
        '
        Me.txtProlisOnPrinted.Location = New System.Drawing.Point(670, 179)
        Me.txtProlisOnPrinted.Name = "txtProlisOnPrinted"
        Me.txtProlisOnPrinted.ReadOnly = True
        Me.txtProlisOnPrinted.Size = New System.Drawing.Size(74, 20)
        Me.txtProlisOnPrinted.TabIndex = 49
        Me.txtProlisOnPrinted.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblResp
        '
        Me.lblResp.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblResp.Location = New System.Drawing.Point(596, 312)
        Me.lblResp.Name = "lblResp"
        Me.lblResp.Size = New System.Drawing.Size(91, 15)
        Me.lblResp.TabIndex = 57
        Me.lblResp.Text = "Provider"
        '
        'Label6
        '
        Me.Label6.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label6.Location = New System.Drawing.Point(750, 112)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(74, 15)
        Me.Label6.TabIndex = 48
        Me.Label6.Text = "Email"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label3.Location = New System.Drawing.Point(407, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(121, 15)
        Me.Label3.TabIndex = 41
        Me.Label3.Text = "Search Criteria"
        '
        'txtEmailed
        '
        Me.txtEmailed.Location = New System.Drawing.Point(750, 130)
        Me.txtEmailed.Name = "txtEmailed"
        Me.txtEmailed.ReadOnly = True
        Me.txtEmailed.Size = New System.Drawing.Size(74, 20)
        Me.txtEmailed.TabIndex = 47
        Me.txtEmailed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LabelP
        '
        Me.LabelP.ForeColor = System.Drawing.Color.DarkBlue
        Me.LabelP.Location = New System.Drawing.Point(590, 112)
        Me.LabelP.Name = "LabelP"
        Me.LabelP.Size = New System.Drawing.Size(74, 15)
        Me.LabelP.TabIndex = 46
        Me.LabelP.Text = "View"
        Me.LabelP.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblTo
        '
        Me.lblTo.ForeColor = System.Drawing.Color.Magenta
        Me.lblTo.Location = New System.Drawing.Point(144, 8)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(116, 15)
        Me.lblTo.TabIndex = 35
        Me.lblTo.Text = "To"
        '
        'txtProvider
        '
        Me.txtProvider.BackColor = System.Drawing.Color.Azure
        Me.txtProvider.Location = New System.Drawing.Point(513, 330)
        Me.txtProvider.Multiline = True
        Me.txtProvider.Name = "txtProvider"
        Me.txtProvider.ReadOnly = True
        Me.txtProvider.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtProvider.Size = New System.Drawing.Size(307, 64)
        Me.txtProvider.TabIndex = 37
        '
        'txtView
        '
        Me.txtView.Location = New System.Drawing.Point(590, 130)
        Me.txtView.Name = "txtView"
        Me.txtView.ReadOnly = True
        Me.txtView.Size = New System.Drawing.Size(74, 20)
        Me.txtView.TabIndex = 45
        Me.txtView.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label4.Location = New System.Drawing.Point(670, 112)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 15)
        Me.Label4.TabIndex = 44
        Me.Label4.Text = "In Lab Print"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtLabPrinted
        '
        Me.txtLabPrinted.Location = New System.Drawing.Point(670, 130)
        Me.txtLabPrinted.Name = "txtLabPrinted"
        Me.txtLabPrinted.ReadOnly = True
        Me.txtLabPrinted.Size = New System.Drawing.Size(74, 20)
        Me.txtLabPrinted.TabIndex = 39
        Me.txtLabPrinted.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'dgvAccessions
        '
        Me.dgvAccessions.AllowUserToAddRows = False
        Me.dgvAccessions.AllowUserToDeleteRows = False
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.SeaShell
        Me.dgvAccessions.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle9
        Me.dgvAccessions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAccessions.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.AccID, Me.Dated, Me.Status, Me.Source, Me.ReDo})
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.Lavender
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvAccessions.DefaultCellStyle = DataGridViewCellStyle10
        Me.dgvAccessions.Location = New System.Drawing.Point(16, 198)
        Me.dgvAccessions.MultiSelect = False
        Me.dgvAccessions.Name = "dgvAccessions"
        Me.dgvAccessions.RowHeadersVisible = False
        Me.dgvAccessions.RowHeadersWidth = 51
        Me.dgvAccessions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvAccessions.Size = New System.Drawing.Size(485, 359)
        Me.dgvAccessions.TabIndex = 34
        '
        'AccID
        '
        Me.AccID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.AccID.FillWeight = 96.0!
        Me.AccID.HeaderText = "Accession ID"
        Me.AccID.MinimumWidth = 6
        Me.AccID.Name = "AccID"
        Me.AccID.ReadOnly = True
        Me.AccID.Width = 95
        '
        'Dated
        '
        Me.Dated.FillWeight = 86.0!
        Me.Dated.HeaderText = "Dated"
        Me.Dated.MinimumWidth = 6
        Me.Dated.Name = "Dated"
        Me.Dated.ReadOnly = True
        Me.Dated.Width = 86
        '
        'Status
        '
        Me.Status.FillWeight = 90.0!
        Me.Status.HeaderText = "Status"
        Me.Status.MinimumWidth = 6
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        Me.Status.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Status.Width = 90
        '
        'Source
        '
        Me.Source.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Source.HeaderText = "Output"
        Me.Source.MinimumWidth = 6
        Me.Source.Name = "Source"
        Me.Source.ReadOnly = True
        '
        'ReDo
        '
        Me.ReDo.FillWeight = 60.0!
        Me.ReDo.HeaderText = "ReDo"
        Me.ReDo.MinimumWidth = 6
        Me.ReDo.Name = "ReDo"
        Me.ReDo.Width = 60
        '
        'lblFrom
        '
        Me.lblFrom.ForeColor = System.Drawing.Color.Red
        Me.lblFrom.Location = New System.Drawing.Point(16, 8)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(122, 15)
        Me.lblFrom.TabIndex = 31
        Me.lblFrom.Text = "From"
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label5.Location = New System.Drawing.Point(511, 62)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 15)
        Me.Label5.TabIndex = 63
        Me.Label5.Text = "Initial"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtInitial
        '
        Me.txtInitial.Location = New System.Drawing.Point(510, 80)
        Me.txtInitial.Name = "txtInitial"
        Me.txtInitial.ReadOnly = True
        Me.txtInitial.Size = New System.Drawing.Size(74, 20)
        Me.txtInitial.TabIndex = 62
        Me.txtInitial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label11
        '
        Me.Label11.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label11.Location = New System.Drawing.Point(590, 62)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(74, 15)
        Me.Label11.TabIndex = 65
        Me.Label11.Text = "Partial"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtPartial
        '
        Me.txtPartial.Location = New System.Drawing.Point(590, 80)
        Me.txtPartial.Name = "txtPartial"
        Me.txtPartial.ReadOnly = True
        Me.txtPartial.Size = New System.Drawing.Size(74, 20)
        Me.txtPartial.TabIndex = 64
        Me.txtPartial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblTotal
        '
        Me.lblTotal.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblTotal.Location = New System.Drawing.Point(750, 62)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(77, 15)
        Me.lblTotal.TabIndex = 67
        Me.lblTotal.Text = "Total"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtTotal
        '
        Me.txtTotal.Location = New System.Drawing.Point(750, 80)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(74, 20)
        Me.txtTotal.TabIndex = 66
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtProcessDetail
        '
        Me.txtProcessDetail.BackColor = System.Drawing.Color.LavenderBlush
        Me.txtProcessDetail.Location = New System.Drawing.Point(514, 425)
        Me.txtProcessDetail.Multiline = True
        Me.txtProcessDetail.Name = "txtProcessDetail"
        Me.txtProcessDetail.ReadOnly = True
        Me.txtProcessDetail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtProcessDetail.Size = New System.Drawing.Size(307, 69)
        Me.txtProcessDetail.TabIndex = 70
        '
        'Label15
        '
        Me.Label15.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label15.Location = New System.Drawing.Point(593, 407)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(119, 15)
        Me.Label15.TabIndex = 71
        Me.Label15.Text = "Process Detail"
        '
        'chkPUnP
        '
        Me.chkPUnP.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkPUnP.Location = New System.Drawing.Point(278, 25)
        Me.chkPUnP.Name = "chkPUnP"
        Me.chkPUnP.Size = New System.Drawing.Size(107, 23)
        Me.chkPUnP.TabIndex = 72
        Me.chkPUnP.Text = "Processed"
        Me.chkPUnP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkPUnP.UseVisualStyleBackColor = True
        '
        'btnRedo
        '
        Me.btnRedo.Enabled = False
        Me.btnRedo.Location = New System.Drawing.Point(18, 564)
        Me.btnRedo.Name = "btnRedo"
        Me.btnRedo.Size = New System.Drawing.Size(68, 27)
        Me.btnRedo.TabIndex = 73
        Me.btnRedo.Text = "Schedule"
        Me.ToolTip1.SetToolTip(Me.btnRedo, "Schedule Fax")
        Me.btnRedo.UseVisualStyleBackColor = True
        '
        'btnSellAll
        '
        Me.btnSellAll.Image = CType(resources.GetObject("btnSellAll.Image"), System.Drawing.Image)
        Me.btnSellAll.Location = New System.Drawing.Point(413, 564)
        Me.btnSellAll.Name = "btnSellAll"
        Me.btnSellAll.Size = New System.Drawing.Size(30, 27)
        Me.btnSellAll.TabIndex = 76
        Me.btnSellAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnSellAll, "Click to execute the Redo selection")
        Me.btnSellAll.UseVisualStyleBackColor = True
        '
        'btnDeselAll
        '
        Me.btnDeselAll.Image = CType(resources.GetObject("btnDeselAll.Image"), System.Drawing.Image)
        Me.btnDeselAll.Location = New System.Drawing.Point(376, 564)
        Me.btnDeselAll.Name = "btnDeselAll"
        Me.btnDeselAll.Size = New System.Drawing.Size(30, 27)
        Me.btnDeselAll.TabIndex = 74
        Me.btnDeselAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnDeselAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnDeselAll, "Deselect All")
        Me.btnDeselAll.UseVisualStyleBackColor = True
        '
        'txtHL7
        '
        Me.txtHL7.Location = New System.Drawing.Point(510, 179)
        Me.txtHL7.Name = "txtHL7"
        Me.txtHL7.ReadOnly = True
        Me.txtHL7.Size = New System.Drawing.Size(74, 20)
        Me.txtHL7.TabIndex = 77
        Me.txtHL7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label10
        '
        Me.Label10.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label10.Location = New System.Drawing.Point(507, 161)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(74, 15)
        Me.Label10.TabIndex = 78
        Me.Label10.Text = "HL7"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtPDF
        '
        Me.txtPDF.Location = New System.Drawing.Point(590, 179)
        Me.txtPDF.Name = "txtPDF"
        Me.txtPDF.ReadOnly = True
        Me.txtPDF.Size = New System.Drawing.Size(74, 20)
        Me.txtPDF.TabIndex = 79
        Me.txtPDF.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label16
        '
        Me.Label16.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label16.Location = New System.Drawing.Point(590, 161)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(74, 15)
        Me.Label16.TabIndex = 80
        Me.Label16.Text = "PDF"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label17
        '
        Me.Label17.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label17.Location = New System.Drawing.Point(670, 62)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(74, 15)
        Me.Label17.TabIndex = 81
        Me.Label17.Text = "Final"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtFinal
        '
        Me.txtFinal.Location = New System.Drawing.Point(670, 80)
        Me.txtFinal.Name = "txtFinal"
        Me.txtFinal.ReadOnly = True
        Me.txtFinal.Size = New System.Drawing.Size(74, 20)
        Me.txtFinal.TabIndex = 82
        Me.txtFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus, Me.PB})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 600)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(833, 26)
        Me.StatusStrip1.TabIndex = 83
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = False
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(120, 21)
        '
        'PB
        '
        Me.PB.AutoSize = False
        Me.PB.Name = "PB"
        Me.PB.Size = New System.Drawing.Size(680, 20)
        Me.PB.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'Chart1
        '
        'Me.Chart1.BackColor = System.Drawing.Color.Transparent
        'ChartArea5.Name = "ChartArea1"
        'Me.Chart1.ChartAreas.Add(ChartArea5)
        'Legend5.Name = "Legend1"
        'Me.Chart1.Legends.Add(Legend5)
        'Me.Chart1.Location = New System.Drawing.Point(16, 53)
        'Me.Chart1.Name = "Chart1"
        'Series5.BorderColor = System.Drawing.Color.Red
        'Series5.ChartArea = "ChartArea1"
        'Series5.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        'Series5.IsXValueIndexed = True
        'Series5.LabelAngle = -90
        'Series5.Legend = "Legend1"
        'Series5.Name = "Count"
        'Me.Chart1.Series.Add(Series5)
        'Me.Chart1.Size = New System.Drawing.Size(434, 123)
        'Me.Chart1.TabIndex = 84
        'Me.Chart1.Text = "Chart1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 179)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 13)
        Me.Label1.TabIndex = 85
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.btnPrint)
        Me.Panel1.Controls.Add(Me.AccIds)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.txtQty)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.cmbDestination)
        Me.Panel1.Location = New System.Drawing.Point(522, 510)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(305, 81)
        Me.Panel1.TabIndex = 89
        '
        'btnPrint
        '
        'Me.btnPrint.Image = Global.Prolis.My.Resources.Resources.icons8_print_30
        Me.btnPrint.Location = New System.Drawing.Point(260, 30)
        Me.btnPrint.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(41, 46)
        Me.btnPrint.TabIndex = 94
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'AccIds
        '
        Me.AccIds.BackColor = System.Drawing.SystemColors.HighlightText
        Me.AccIds.Location = New System.Drawing.Point(101, 25)
        Me.AccIds.Name = "AccIds"
        Me.AccIds.ReadOnly = True
        Me.AccIds.Size = New System.Drawing.Size(104, 20)
        Me.AccIds.TabIndex = 90
        Me.AccIds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label14
        '
        Me.Label14.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label14.Location = New System.Drawing.Point(3, 24)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(109, 14)
        Me.Label14.TabIndex = 93
        Me.Label14.Text = "Accession ID"
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(211, 56)
        Me.txtQty.MaxLength = 2
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(44, 20)
        Me.txtQty.TabIndex = 91
        Me.txtQty.Text = "1"
        Me.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label2.Location = New System.Drawing.Point(3, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 14)
        Me.Label2.TabIndex = 90
        Me.Label2.Text = "Report Destination"
        '
        'cmbDestination
        '
        Me.cmbDestination.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDestination.FormattingEnabled = True
        Me.cmbDestination.Items.AddRange(New Object() {"Printer", "Screen", "Fax", "Interface", "Prolison", "Email", "Save Pdf to Folder"})
        Me.cmbDestination.Location = New System.Drawing.Point(101, 55)
        Me.cmbDestination.Name = "cmbDestination"
        Me.cmbDestination.Size = New System.Drawing.Size(104, 21)
        Me.cmbDestination.TabIndex = 89
        '
        'Label12
        '
        Me.Label12.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label12.Location = New System.Drawing.Point(71, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(106, 20)
        Me.Label12.TabIndex = 92
        Me.Label12.Text = "Report Printing"
        '
        'btnGo
        '
        Me.btnGo.Image = CType(resources.GetObject("btnGo.Image"), System.Drawing.Image)
        Me.btnGo.Location = New System.Drawing.Point(789, 23)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(32, 25)
        Me.btnGo.TabIndex = 38
        Me.btnGo.UseVisualStyleBackColor = True
        '
        'BW
        '
        '
        'waitingLabel
        '
        Me.waitingLabel.AutoSize = True
        Me.waitingLabel.Location = New System.Drawing.Point(92, 588)
        Me.waitingLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.waitingLabel.Name = "waitingLabel"
        Me.waitingLabel.Size = New System.Drawing.Size(0, 13)
        Me.waitingLabel.TabIndex = 93
        Me.waitingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmRptDash
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(833, 626)
        Me.Controls.Add(Me.waitingLabel)
        Me.Controls.Add(Me.Label1)
        'Me.Controls.Add(Me.Chart1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.txtFinal)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.txtPDF)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtHL7)
        Me.Controls.Add(Me.btnSellAll)
        Me.Controls.Add(Me.btnDeselAll)
        Me.Controls.Add(Me.btnRedo)
        Me.Controls.Add(Me.chkPUnP)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtProcessDetail)
        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.txtTotal)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtPartial)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtInitial)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtPatient)
        Me.Controls.Add(Me.txtTerm)
        Me.Controls.Add(Me.lblTerm)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.dtpTo)
        Me.Controls.Add(Me.txtSuccessFax)
        Me.Controls.Add(Me.cmbCriteria)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.dtpFrom)
        Me.Controls.Add(Me.txtFailFax)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btnGo)
        Me.Controls.Add(Me.txtProlisOnPrinted)
        Me.Controls.Add(Me.lblResp)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtEmailed)
        Me.Controls.Add(Me.LabelP)
        Me.Controls.Add(Me.lblTo)
        Me.Controls.Add(Me.txtProvider)
        Me.Controls.Add(Me.txtView)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtLabPrinted)
        Me.Controls.Add(Me.dgvAccessions)
        Me.Controls.Add(Me.lblFrom)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRptDash"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Reporting Dashboard"
        CType(Me.dgvAccessions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        'CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtPatient As System.Windows.Forms.TextBox
    Friend WithEvents txtTerm As System.Windows.Forms.TextBox
    Friend WithEvents lblTerm As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtSuccessFax As System.Windows.Forms.TextBox
    Friend WithEvents cmbCriteria As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtFailFax As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnGo As System.Windows.Forms.Button
    Friend WithEvents txtProlisOnPrinted As System.Windows.Forms.TextBox
    Friend WithEvents lblResp As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtEmailed As System.Windows.Forms.TextBox
    Friend WithEvents LabelP As System.Windows.Forms.Label
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents txtProvider As System.Windows.Forms.TextBox
    Friend WithEvents txtView As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtLabPrinted As System.Windows.Forms.TextBox
    Friend WithEvents dgvAccessions As System.Windows.Forms.DataGridView
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtInitial As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtPartial As System.Windows.Forms.TextBox
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents txtProcessDetail As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents chkPUnP As System.Windows.Forms.CheckBox
    Friend WithEvents btnRedo As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnDeselAll As System.Windows.Forms.Button
    Friend WithEvents btnSellAll As System.Windows.Forms.Button
    Friend WithEvents txtHL7 As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtPDF As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtFinal As System.Windows.Forms.TextBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PB As System.Windows.Forms.ToolStripProgressBar
    'Friend WithEvents Chart1 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbDestination As System.Windows.Forms.ComboBox
    Friend WithEvents AccIds As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents BW As System.ComponentModel.BackgroundWorker
    Friend WithEvents waitingLabel As System.Windows.Forms.Label
    Friend WithEvents AccID As DataGridViewTextBoxColumn
    Friend WithEvents Dated As DataGridViewTextBoxColumn
    Friend WithEvents Status As DataGridViewTextBoxColumn
    Friend WithEvents Source As DataGridViewTextBoxColumn
    Friend WithEvents ReDo As DataGridViewCheckBoxColumn
End Class
