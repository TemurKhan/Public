<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExportSuperBills
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExportSuperBills))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        btnProcess = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        dgvDiscrete = New DataGridView()
        AccID = New DataGridViewTextBoxColumn()
        Label5 = New Label()
        Label4 = New Label()
        txtAccTo = New TextBox()
        txtAccFrom = New TextBox()
        lblTo = New Label()
        lblFrom = New Label()
        Label1 = New Label()
        cmbFormat = New ComboBox()
        Label6 = New Label()
        chkClaims = New CheckBox()
        FolderBrowserDialog1 = New FolderBrowserDialog()
        btnDeSel = New Button()
        btnSelAll = New Button()
        lstTargets = New CheckedListBox()
        Label7 = New Label()
        cmbBillType = New ComboBox()
        Label8 = New Label()
        cmbDateType = New ComboBox()
        StatusStrip1 = New StatusStrip()
        lblStatus = New ToolStripStatusLabel()
        PB = New ToolStripProgressBar()
        BW = New ComponentModel.BackgroundWorker()
        dtpDateTo = New DateTimePicker()
        lblClearDates = New Label()
        dtpDateFrom = New DateTimePicker()
        ToolStrip1.SuspendLayout()
        CType(dgvDiscrete, ComponentModel.ISupportInitialize).BeginInit()
        StatusStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnProcess, ToolStripSeparator2, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(870, 34)
        ToolStrip1.TabIndex = 15
        ToolStrip1.Text = "ToolStrip1"
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
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 34)
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
        btnHelp.ForeColor = Color.DarkBlue
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.ImageTransparentColor = Color.Magenta
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(77, 29)
        btnHelp.Text = "Help"
        ' 
        ' dgvDiscrete
        ' 
        dgvDiscrete.AllowUserToAddRows = False
        dgvDiscrete.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.AliceBlue
        dgvDiscrete.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvDiscrete.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvDiscrete.Columns.AddRange(New DataGridViewColumn() {AccID})
        dgvDiscrete.Location = New Point(20, 367)
        dgvDiscrete.Margin = New Padding(5, 6, 5, 6)
        dgvDiscrete.Name = "dgvDiscrete"
        dgvDiscrete.RowHeadersVisible = False
        dgvDiscrete.RowHeadersWidth = 62
        dgvDiscrete.Size = New Size(330, 328)
        dgvDiscrete.TabIndex = 5
        ' 
        ' AccID
        ' 
        AccID.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        AccID.FillWeight = 174F
        AccID.HeaderText = "Discrete Accessions"
        AccID.MaxInputLength = 12
        AccID.MinimumWidth = 8
        AccID.Name = "AccID"
        AccID.Resizable = DataGridViewTriState.False
        AccID.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.DarkBlue
        Label5.Location = New Point(192, 287)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(127, 25)
        Label5.TabIndex = 33
        Label5.Text = "To Accession"
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(23, 287)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(140, 25)
        Label4.TabIndex = 32
        Label4.Text = "From Accession"
        ' 
        ' txtAccTo
        ' 
        txtAccTo.Location = New Point(192, 317)
        txtAccTo.Margin = New Padding(5, 6, 5, 6)
        txtAccTo.Name = "txtAccTo"
        txtAccTo.Size = New Size(136, 31)
        txtAccTo.TabIndex = 4
        ' 
        ' txtAccFrom
        ' 
        txtAccFrom.Location = New Point(23, 317)
        txtAccFrom.Margin = New Padding(5, 6, 5, 6)
        txtAccFrom.Name = "txtAccFrom"
        txtAccFrom.Size = New Size(136, 31)
        txtAccFrom.TabIndex = 3
        ' 
        ' lblTo
        ' 
        lblTo.ForeColor = Color.DarkBlue
        lblTo.Location = New Point(192, 196)
        lblTo.Margin = New Padding(5, 0, 5, 0)
        lblTo.Name = "lblTo"
        lblTo.Size = New Size(182, 25)
        lblTo.TabIndex = 31
        lblTo.Text = "To"
        ' 
        ' lblFrom
        ' 
        lblFrom.ForeColor = Color.DarkBlue
        lblFrom.Location = New Point(20, 196)
        lblFrom.Margin = New Padding(5, 0, 5, 0)
        lblFrom.Name = "lblFrom"
        lblFrom.Size = New Size(177, 25)
        lblFrom.TabIndex = 30
        lblFrom.Text = "From"
        ' 
        ' Label1
        ' 
        Label1.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.Red
        Label1.Location = New Point(223, 73)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(470, 37)
        Label1.TabIndex = 35
        Label1.Text = "Synchrinization must be done prior to running this routine"
        Label1.TextAlign = ContentAlignment.TopCenter
        ' 
        ' cmbFormat
        ' 
        cmbFormat.DropDownStyle = ComboBoxStyle.DropDownList
        cmbFormat.FormattingEnabled = True
        cmbFormat.Location = New Point(380, 662)
        cmbFormat.Margin = New Padding(5, 6, 5, 6)
        cmbFormat.Name = "cmbFormat"
        cmbFormat.Size = New Size(272, 33)
        cmbFormat.TabIndex = 6
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.DarkBlue
        Label6.Location = New Point(400, 631)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(83, 25)
        Label6.TabIndex = 38
        Label6.Text = "Export Format"
        ' 
        ' chkClaims
        ' 
        chkClaims.Appearance = Appearance.Button
        chkClaims.Location = New Point(697, 656)
        chkClaims.Margin = New Padding(5, 6, 5, 6)
        chkClaims.Name = "chkClaims"
        chkClaims.Size = New Size(173, 46)
        chkClaims.TabIndex = 39
        chkClaims.Text = "Unprocessed"
        chkClaims.TextAlign = ContentAlignment.MiddleCenter
        chkClaims.UseVisualStyleBackColor = True
        ' 
        ' btnDeSel
        ' 
        btnDeSel.Image = CType(resources.GetObject("btnDeSel.Image"), Image)
        btnDeSel.Location = New Point(808, 131)
        btnDeSel.Margin = New Padding(5, 6, 5, 6)
        btnDeSel.Name = "btnDeSel"
        btnDeSel.Size = New Size(62, 56)
        btnDeSel.TabIndex = 44
        btnDeSel.UseVisualStyleBackColor = True
        ' 
        ' btnSelAll
        ' 
        btnSelAll.Image = CType(resources.GetObject("btnSelAll.Image"), Image)
        btnSelAll.Location = New Point(723, 131)
        btnSelAll.Margin = New Padding(5, 6, 5, 6)
        btnSelAll.Name = "btnSelAll"
        btnSelAll.Size = New Size(62, 56)
        btnSelAll.TabIndex = 43
        btnSelAll.UseVisualStyleBackColor = True
        ' 
        ' lstTargets
        ' 
        lstTargets.CheckOnClick = True
        lstTargets.FormattingEnabled = True
        lstTargets.Items.AddRange(New Object() {"All (Clients, Third Parties, Patients)"})
        lstTargets.Location = New Point(380, 229)
        lstTargets.Margin = New Padding(5, 6, 5, 6)
        lstTargets.Name = "lstTargets"
        lstTargets.Size = New Size(487, 396)
        lstTargets.Sorted = True
        lstTargets.TabIndex = 42
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.DarkBlue
        Label7.Location = New Point(400, 110)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(117, 25)
        Label7.TabIndex = 41
        Label7.Text = "Billing Type"
        ' 
        ' cmbBillType
        ' 
        cmbBillType.DropDownStyle = ComboBoxStyle.DropDownList
        cmbBillType.FormattingEnabled = True
        cmbBillType.Items.AddRange(New Object() {"Client", "Third Party", "Patient", "All"})
        cmbBillType.Location = New Point(380, 140)
        cmbBillType.Margin = New Padding(5, 6, 5, 6)
        cmbBillType.Name = "cmbBillType"
        cmbBillType.Size = New Size(306, 33)
        cmbBillType.TabIndex = 40
        ' 
        ' Label8
        ' 
        Label8.ForeColor = Color.DarkBlue
        Label8.Location = New Point(38, 110)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(117, 25)
        Label8.TabIndex = 45
        Label8.Text = "Date Type"
        ' 
        ' cmbDateType
        ' 
        cmbDateType.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDateType.FormattingEnabled = True
        cmbDateType.Items.AddRange(New Object() {"Accession Date", "Collection Date", "Receive Date", "Report Finalize Date", "Service Date"})
        cmbDateType.Location = New Point(20, 140)
        cmbDateType.Margin = New Padding(5, 6, 5, 6)
        cmbDateType.Name = "cmbDateType"
        cmbDateType.Size = New Size(327, 33)
        cmbDateType.TabIndex = 46
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.ImageScalingSize = New Size(24, 24)
        StatusStrip1.Items.AddRange(New ToolStripItem() {lblStatus, PB})
        StatusStrip1.Location = New Point(0, 721)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Padding = New Padding(2, 0, 23, 0)
        StatusStrip1.Size = New Size(870, 35)
        StatusStrip1.TabIndex = 47
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = False
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(120, 28)
        ' 
        ' PB
        ' 
        PB.AutoSize = False
        PB.Name = "PB"
        PB.Size = New Size(667, 27)
        PB.Style = ProgressBarStyle.Continuous
        ' 
        ' BW
        ' 
        BW.WorkerReportsProgress = True
        BW.WorkerSupportsCancellation = True
        ' 
        ' dtpDateTo
        ' 
        dtpDateTo.CustomFormat = " "
        dtpDateTo.Format = DateTimePickerFormat.Custom
        dtpDateTo.Location = New Point(192, 229)
        dtpDateTo.Margin = New Padding(3, 4, 3, 4)
        dtpDateTo.Name = "dtpDateTo"
        dtpDateTo.Size = New Size(136, 31)
        dtpDateTo.TabIndex = 2
        ' 
        ' lblClearDates
        ' 
        lblClearDates.BackColor = SystemColors.Control
        lblClearDates.Image = My.Resources.Resources.Eraser
        lblClearDates.Location = New Point(337, 221)
        lblClearDates.Name = "lblClearDates"
        lblClearDates.Size = New Size(38, 46)
        lblClearDates.TabIndex = 99
        lblClearDates.Text = "      "
        ' 
        ' dtpDateFrom
        ' 
        dtpDateFrom.CustomFormat = " "
        dtpDateFrom.Format = DateTimePickerFormat.Custom
        dtpDateFrom.Location = New Point(23, 229)
        dtpDateFrom.Margin = New Padding(3, 4, 3, 4)
        dtpDateFrom.Name = "dtpDateFrom"
        dtpDateFrom.Size = New Size(136, 31)
        dtpDateFrom.TabIndex = 1
        ' 
        ' frmExportSuperBills
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(870, 756)
        Controls.Add(dtpDateTo)
        Controls.Add(lblClearDates)
        Controls.Add(dtpDateFrom)
        Controls.Add(StatusStrip1)
        Controls.Add(cmbDateType)
        Controls.Add(Label8)
        Controls.Add(btnDeSel)
        Controls.Add(btnSelAll)
        Controls.Add(lstTargets)
        Controls.Add(Label7)
        Controls.Add(cmbBillType)
        Controls.Add(chkClaims)
        Controls.Add(Label6)
        Controls.Add(cmbFormat)
        Controls.Add(Label1)
        Controls.Add(dgvDiscrete)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(txtAccTo)
        Controls.Add(txtAccFrom)
        Controls.Add(lblTo)
        Controls.Add(lblFrom)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MaximumSize = New Size(892, 812)
        MinimumSize = New Size(892, 812)
        Name = "frmExportSuperBills"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Export Super Bills"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgvDiscrete, ComponentModel.ISupportInitialize).EndInit()
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnProcess As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents dgvDiscrete As System.Windows.Forms.DataGridView
    Friend WithEvents AccID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtAccTo As System.Windows.Forms.TextBox
    Friend WithEvents txtAccFrom As System.Windows.Forms.TextBox
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbFormat As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents chkClaims As System.Windows.Forms.CheckBox
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents btnDeSel As System.Windows.Forms.Button
    Friend WithEvents btnSelAll As System.Windows.Forms.Button
    Friend WithEvents lstTargets As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbBillType As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbDateType As System.Windows.Forms.ComboBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PB As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents BW As System.ComponentModel.BackgroundWorker
    Friend WithEvents dtpDateTo As DateTimePicker
    Friend WithEvents lblClearDates As Label
    Friend WithEvents dtpDateFrom As DateTimePicker
End Class
