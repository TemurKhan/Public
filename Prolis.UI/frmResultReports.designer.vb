<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmResultReports
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmResultReports))
        ToolStrip1 = New ToolStrip()
        btnProcess = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        grpAccRange = New GroupBox()
        Label6 = New Label()
        Label5 = New Label()
        txtAccTo = New TextBox()
        txtAccFrom = New TextBox()
        grpDateRange = New GroupBox()
        chkAccRes = New CheckBox()
        Label4 = New Label()
        Label3 = New Label()
        dtpDateTo = New DateTimePicker()
        dtpDateFrom = New DateTimePicker()
        grpProvider = New GroupBox()
        txtProviderName = New TextBox()
        btnProvLookUp = New Button()
        txtProviderID = New TextBox()
        Label2 = New Label()
        dgvDiscrete = New DataGridView()
        Accessions = New DataGridViewTextBoxColumn()
        chkComplete = New CheckBox()
        chkUnPrinted = New CheckBox()
        chkAccRange = New CheckBox()
        chkDateRange = New CheckBox()
        chkProvider = New CheckBox()
        cmbDestination = New ComboBox()
        Label1 = New Label()
        Label7 = New Label()
        cmbSort = New ComboBox()
        txtQty = New TextBox()
        chkRPT = New CheckBox()
        chkConfig = New CheckBox()
        BW = New ComponentModel.BackgroundWorker()
        StatusStrip1 = New StatusStrip()
        lblStatus = New ToolStripStatusLabel()
        PB = New ToolStripProgressBar()
        chkOrigCorr = New CheckBox()
        HelpProvider1 = New HelpProvider()
        waitingLabel = New Label()
        btnClear = New Button()
        ToolStrip1.SuspendLayout()
        grpAccRange.SuspendLayout()
        grpDateRange.SuspendLayout()
        grpProvider.SuspendLayout()
        CType(dgvDiscrete, ComponentModel.ISupportInitialize).BeginInit()
        StatusStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(20, 20)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnProcess, ToolStripSeparator1, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(814, 34)
        ToolStrip1.TabIndex = 4
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnProcess
        ' 
        btnProcess.Enabled = False
        btnProcess.ForeColor = Color.DarkBlue
        btnProcess.Image = CType(resources.GetObject("btnProcess.Image"), Image)
        btnProcess.ImageTransparentColor = Color.Magenta
        btnProcess.Name = "btnProcess"
        btnProcess.Size = New Size(96, 29)
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
        btnHelp.ForeColor = Color.DarkBlue
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.ImageTransparentColor = Color.Magenta
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(73, 29)
        btnHelp.Text = "Help"
        ' 
        ' grpAccRange
        ' 
        grpAccRange.Controls.Add(Label6)
        grpAccRange.Controls.Add(Label5)
        grpAccRange.Controls.Add(txtAccTo)
        grpAccRange.Controls.Add(txtAccFrom)
        grpAccRange.Location = New Point(55, 109)
        grpAccRange.Margin = New Padding(5, 6, 5, 6)
        grpAccRange.Name = "grpAccRange"
        grpAccRange.Padding = New Padding(5, 6, 5, 6)
        grpAccRange.Size = New Size(365, 131)
        grpAccRange.TabIndex = 5
        grpAccRange.TabStop = False
        grpAccRange.Text = "Accession Range"
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.DarkBlue
        Label6.Location = New Point(170, 31)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(150, 34)
        Label6.TabIndex = 17
        Label6.Text = "To"
        Label6.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.DarkBlue
        Label5.Location = New Point(10, 36)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(150, 33)
        Label5.TabIndex = 16
        Label5.Text = "From"
        Label5.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtAccTo
        ' 
        txtAccTo.Location = New Point(195, 73)
        txtAccTo.Margin = New Padding(5, 6, 5, 6)
        txtAccTo.MaxLength = 15
        txtAccTo.Name = "txtAccTo"
        txtAccTo.Size = New Size(148, 31)
        txtAccTo.TabIndex = 2
        txtAccTo.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtAccFrom
        ' 
        txtAccFrom.Location = New Point(35, 73)
        txtAccFrom.Margin = New Padding(5, 6, 5, 6)
        txtAccFrom.MaxLength = 15
        txtAccFrom.Name = "txtAccFrom"
        txtAccFrom.Size = New Size(148, 31)
        txtAccFrom.TabIndex = 1
        txtAccFrom.TextAlign = HorizontalAlignment.Center
        ' 
        ' grpDateRange
        ' 
        grpDateRange.Controls.Add(chkAccRes)
        grpDateRange.Controls.Add(Label4)
        grpDateRange.Controls.Add(Label3)
        grpDateRange.Controls.Add(dtpDateTo)
        grpDateRange.Controls.Add(dtpDateFrom)
        grpDateRange.Enabled = False
        grpDateRange.Location = New Point(55, 261)
        grpDateRange.Margin = New Padding(5, 6, 5, 6)
        grpDateRange.Name = "grpDateRange"
        grpDateRange.Padding = New Padding(5, 6, 5, 6)
        grpDateRange.Size = New Size(526, 131)
        grpDateRange.TabIndex = 6
        grpDateRange.TabStop = False
        grpDateRange.Text = "Date Range"
        ' 
        ' chkAccRes
        ' 
        chkAccRes.Appearance = Appearance.Button
        chkAccRes.ForeColor = Color.DarkBlue
        chkAccRes.Location = New Point(25, 59)
        chkAccRes.Margin = New Padding(5, 6, 5, 6)
        chkAccRes.Name = "chkAccRes"
        chkAccRes.Size = New Size(124, 50)
        chkAccRes.TabIndex = 19
        chkAccRes.Text = "Received"
        chkAccRes.TextAlign = ContentAlignment.MiddleCenter
        chkAccRes.UseVisualStyleBackColor = True
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(346, 31)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(75, 34)
        Label4.TabIndex = 17
        Label4.Text = "To"
        Label4.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(170, 31)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(76, 34)
        Label3.TabIndex = 16
        Label3.Text = "From"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' dtpDateTo
        ' 
        dtpDateTo.Format = DateTimePickerFormat.Short
        dtpDateTo.Location = New Point(330, 72)
        dtpDateTo.Margin = New Padding(5, 6, 5, 6)
        dtpDateTo.Name = "dtpDateTo"
        dtpDateTo.Size = New Size(158, 31)
        dtpDateTo.TabIndex = 6
        ' 
        ' dtpDateFrom
        ' 
        dtpDateFrom.Format = DateTimePickerFormat.Short
        dtpDateFrom.Location = New Point(159, 72)
        dtpDateFrom.Margin = New Padding(5, 6, 5, 6)
        dtpDateFrom.Name = "dtpDateFrom"
        dtpDateFrom.Size = New Size(158, 31)
        dtpDateFrom.TabIndex = 5
        ' 
        ' grpProvider
        ' 
        grpProvider.Controls.Add(txtProviderName)
        grpProvider.Controls.Add(btnProvLookUp)
        grpProvider.Controls.Add(txtProviderID)
        grpProvider.Controls.Add(Label2)
        grpProvider.Enabled = False
        grpProvider.Location = New Point(55, 403)
        grpProvider.Margin = New Padding(5, 6, 5, 6)
        grpProvider.Name = "grpProvider"
        grpProvider.Padding = New Padding(5, 6, 5, 6)
        grpProvider.Size = New Size(526, 144)
        grpProvider.TabIndex = 7
        grpProvider.TabStop = False
        grpProvider.Text = "By Provider"
        ' 
        ' txtProviderName
        ' 
        txtProviderName.Location = New Point(15, 78)
        txtProviderName.Margin = New Padding(5, 6, 5, 6)
        txtProviderName.MaxLength = 10
        txtProviderName.Name = "txtProviderName"
        txtProviderName.ReadOnly = True
        txtProviderName.Size = New Size(489, 31)
        txtProviderName.TabIndex = 18
        ' 
        ' btnProvLookUp
        ' 
        btnProvLookUp.Image = CType(resources.GetObject("btnProvLookUp.Image"), Image)
        btnProvLookUp.Location = New Point(301, 27)
        btnProvLookUp.Margin = New Padding(5, 6, 5, 6)
        btnProvLookUp.Name = "btnProvLookUp"
        btnProvLookUp.Size = New Size(44, 44)
        btnProvLookUp.TabIndex = 17
        btnProvLookUp.UseVisualStyleBackColor = True
        ' 
        ' txtProviderID
        ' 
        txtProviderID.Location = New Point(140, 28)
        txtProviderID.Margin = New Padding(5, 6, 5, 6)
        txtProviderID.MaxLength = 10
        txtProviderID.Name = "txtProviderID"
        txtProviderID.Size = New Size(148, 31)
        txtProviderID.TabIndex = 16
        txtProviderID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(30, 31)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(101, 34)
        Label2.TabIndex = 15
        Label2.Text = "Provider ID"
        Label2.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' dgvDiscrete
        ' 
        dgvDiscrete.AllowUserToAddRows = False
        dgvDiscrete.AllowUserToDeleteRows = False
        dgvDiscrete.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvDiscrete.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvDiscrete.Columns.AddRange(New DataGridViewColumn() {Accessions})
        dgvDiscrete.Enabled = False
        dgvDiscrete.Location = New Point(609, 128)
        dgvDiscrete.Margin = New Padding(5, 6, 5, 6)
        dgvDiscrete.Name = "dgvDiscrete"
        dgvDiscrete.RowHeadersVisible = False
        dgvDiscrete.RowHeadersWidth = 62
        dgvDiscrete.Size = New Size(205, 483)
        dgvDiscrete.TabIndex = 3
        ' 
        ' Accessions
        ' 
        Accessions.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Accessions.HeaderText = "Accessions"
        Accessions.MinimumWidth = 8
        Accessions.Name = "Accessions"
        ' 
        ' chkComplete
        ' 
        chkComplete.AutoSize = True
        chkComplete.ForeColor = Color.DarkBlue
        chkComplete.Location = New Point(55, 559)
        chkComplete.Margin = New Padding(5, 6, 5, 6)
        chkComplete.Name = "chkComplete"
        chkComplete.Size = New Size(220, 29)
        chkComplete.TabIndex = 9
        chkComplete.Text = "Complete Reports only"
        chkComplete.UseVisualStyleBackColor = True
        ' 
        ' chkUnPrinted
        ' 
        chkUnPrinted.AutoSize = True
        chkUnPrinted.ForeColor = Color.DarkBlue
        chkUnPrinted.Location = New Point(424, 559)
        chkUnPrinted.Margin = New Padding(5, 6, 5, 6)
        chkUnPrinted.Name = "chkUnPrinted"
        chkUnPrinted.Size = New Size(159, 29)
        chkUnPrinted.TabIndex = 10
        chkUnPrinted.Text = "Not yet printed"
        chkUnPrinted.UseVisualStyleBackColor = True
        ' 
        ' chkAccRange
        ' 
        chkAccRange.AutoSize = True
        chkAccRange.Checked = True
        chkAccRange.CheckState = CheckState.Checked
        chkAccRange.Location = New Point(20, 156)
        chkAccRange.Margin = New Padding(5, 6, 5, 6)
        chkAccRange.Name = "chkAccRange"
        chkAccRange.Size = New Size(22, 21)
        chkAccRange.TabIndex = 0
        chkAccRange.UseVisualStyleBackColor = True
        ' 
        ' chkDateRange
        ' 
        chkDateRange.AutoSize = True
        chkDateRange.Location = New Point(20, 309)
        chkDateRange.Margin = New Padding(5, 6, 5, 6)
        chkDateRange.Name = "chkDateRange"
        chkDateRange.Size = New Size(22, 21)
        chkDateRange.TabIndex = 4
        chkDateRange.UseVisualStyleBackColor = True
        ' 
        ' chkProvider
        ' 
        chkProvider.AutoSize = True
        chkProvider.Location = New Point(20, 448)
        chkProvider.Margin = New Padding(5, 6, 5, 6)
        chkProvider.Name = "chkProvider"
        chkProvider.Size = New Size(22, 21)
        chkProvider.TabIndex = 7
        chkProvider.UseVisualStyleBackColor = True
        ' 
        ' cmbDestination
        ' 
        cmbDestination.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDestination.FormattingEnabled = True
        cmbDestination.Items.AddRange(New Object() {"Printer", "Screen", "Fax", "Interface", "Prolison", "Email", "Save Pdf to Folder"})
        cmbDestination.Location = New Point(244, 647)
        cmbDestination.Margin = New Padding(5, 6, 5, 6)
        cmbDestination.Name = "cmbDestination"
        cmbDestination.Size = New Size(214, 33)
        cmbDestination.TabIndex = 12
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(245, 614)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(181, 27)
        Label1.TabIndex = 14
        Label1.Text = "Report Destination"
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.DarkBlue
        Label7.Location = New Point(20, 614)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(186, 27)
        Label7.TabIndex = 16
        Label7.Text = "Sort/Group By"
        ' 
        ' cmbSort
        ' 
        cmbSort.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSort.FormattingEnabled = True
        cmbSort.Items.AddRange(New Object() {"Default", "Accession", "Provider", "Route"})
        cmbSort.Location = New Point(20, 647)
        cmbSort.Margin = New Padding(5, 6, 5, 6)
        cmbSort.Name = "cmbSort"
        cmbSort.Size = New Size(210, 33)
        cmbSort.TabIndex = 11
        ' 
        ' txtQty
        ' 
        txtQty.Location = New Point(470, 647)
        txtQty.Margin = New Padding(5, 6, 5, 6)
        txtQty.MaxLength = 2
        txtQty.Name = "txtQty"
        txtQty.Size = New Size(70, 31)
        txtQty.TabIndex = 17
        txtQty.Text = "1"
        txtQty.TextAlign = HorizontalAlignment.Center
        ' 
        ' chkRPT
        ' 
        chkRPT.Appearance = Appearance.Button
        chkRPT.ForeColor = Color.DarkBlue
        chkRPT.Location = New Point(430, 128)
        chkRPT.Margin = New Padding(5, 6, 5, 6)
        chkRPT.Name = "chkRPT"
        chkRPT.Size = New Size(151, 47)
        chkRPT.TabIndex = 18
        chkRPT.Text = "GENERIC"
        chkRPT.TextAlign = ContentAlignment.MiddleCenter
        chkRPT.UseVisualStyleBackColor = True
        ' 
        ' chkConfig
        ' 
        chkConfig.CheckAlign = ContentAlignment.MiddleRight
        chkConfig.Font = New Font("Microsoft Sans Serif", 9.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        chkConfig.ForeColor = Color.Red
        chkConfig.Location = New Point(230, 64)
        chkConfig.Margin = New Padding(5, 6, 5, 6)
        chkConfig.Name = "chkConfig"
        chkConfig.Size = New Size(301, 47)
        chkConfig.TabIndex = 20
        chkConfig.Text = "Configuration Control"
        chkConfig.TextAlign = ContentAlignment.MiddleCenter
        chkConfig.UseVisualStyleBackColor = True
        ' 
        ' BW
        ' 
        BW.WorkerReportsProgress = True
        BW.WorkerSupportsCancellation = True
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.ImageScalingSize = New Size(20, 20)
        StatusStrip1.Items.AddRange(New ToolStripItem() {lblStatus, PB})
        StatusStrip1.Location = New Point(0, 736)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Padding = New Padding(1, 0, 24, 0)
        StatusStrip1.Size = New Size(814, 36)
        StatusStrip1.TabIndex = 43
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = False
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(100, 29)
        ' 
        ' PB
        ' 
        PB.AutoSize = False
        PB.Name = "PB"
        PB.Size = New Size(600, 28)
        ' 
        ' chkOrigCorr
        ' 
        chkOrigCorr.Appearance = Appearance.Button
        chkOrigCorr.ForeColor = Color.DarkBlue
        chkOrigCorr.Location = New Point(430, 194)
        chkOrigCorr.Margin = New Padding(5, 6, 5, 6)
        chkOrigCorr.Name = "chkOrigCorr"
        chkOrigCorr.Size = New Size(151, 47)
        chkOrigCorr.TabIndex = 44
        chkOrigCorr.Text = "Original"
        chkOrigCorr.TextAlign = ContentAlignment.MiddleCenter
        chkOrigCorr.UseVisualStyleBackColor = True
        ' 
        ' waitingLabel
        ' 
        waitingLabel.AutoSize = True
        waitingLabel.Font = New Font("Microsoft Sans Serif", 7.8F, FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        waitingLabel.Location = New Point(15, 719)
        waitingLabel.Margin = New Padding(4, 0, 4, 0)
        waitingLabel.Name = "waitingLabel"
        waitingLabel.Size = New Size(89, 20)
        waitingLabel.TabIndex = 45
        waitingLabel.Text = "Please wait"
        ' 
        ' btnClear
        ' 
        btnClear.ForeColor = Color.Red
        btnClear.Image = CType(resources.GetObject("btnClear.Image"), Image)
        btnClear.Location = New Point(585, 642)
        btnClear.Margin = New Padding(5, 6, 5, 6)
        btnClear.Name = "btnClear"
        btnClear.Size = New Size(229, 44)
        btnClear.TabIndex = 13
        btnClear.Text = "Clear List"
        btnClear.TextAlign = ContentAlignment.MiddleRight
        btnClear.TextImageRelation = TextImageRelation.ImageBeforeText
        btnClear.UseVisualStyleBackColor = True
        ' 
        ' frmResultReports
        ' 
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(814, 772)
        Controls.Add(waitingLabel)
        Controls.Add(chkOrigCorr)
        Controls.Add(StatusStrip1)
        Controls.Add(dgvDiscrete)
        Controls.Add(chkConfig)
        Controls.Add(chkRPT)
        Controls.Add(txtQty)
        Controls.Add(Label7)
        Controls.Add(cmbSort)
        Controls.Add(Label1)
        Controls.Add(cmbDestination)
        Controls.Add(chkDateRange)
        Controls.Add(chkProvider)
        Controls.Add(btnClear)
        Controls.Add(chkAccRange)
        Controls.Add(chkUnPrinted)
        Controls.Add(chkComplete)
        Controls.Add(grpProvider)
        Controls.Add(grpDateRange)
        Controls.Add(grpAccRange)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MaximumSize = New Size(836, 828)
        MinimumSize = New Size(836, 828)
        Name = "frmResultReports"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Result Reports"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        grpAccRange.ResumeLayout(False)
        grpAccRange.PerformLayout()
        grpDateRange.ResumeLayout(False)
        grpProvider.ResumeLayout(False)
        grpProvider.PerformLayout()
        CType(dgvDiscrete, ComponentModel.ISupportInitialize).EndInit()
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnProcess As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents grpAccRange As System.Windows.Forms.GroupBox
    Friend WithEvents grpDateRange As System.Windows.Forms.GroupBox
    Friend WithEvents grpProvider As System.Windows.Forms.GroupBox
    Friend WithEvents dgvDiscrete As System.Windows.Forms.DataGridView
    Friend WithEvents chkComplete As System.Windows.Forms.CheckBox
    Friend WithEvents chkUnPrinted As System.Windows.Forms.CheckBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents chkAccRange As System.Windows.Forms.CheckBox
    Friend WithEvents chkDateRange As System.Windows.Forms.CheckBox
    Friend WithEvents chkProvider As System.Windows.Forms.CheckBox
    Friend WithEvents txtAccTo As System.Windows.Forms.TextBox
    Friend WithEvents txtAccFrom As System.Windows.Forms.TextBox
    Friend WithEvents dtpDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Accessions As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbDestination As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    'Friend WithEvents gReport As CrystalDecisions.CrystalReports.Engine.ReportDocument
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbSort As System.Windows.Forms.ComboBox
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents chkRPT As System.Windows.Forms.CheckBox
    Friend WithEvents chkConfig As System.Windows.Forms.CheckBox
    Friend WithEvents chkAccRes As System.Windows.Forms.CheckBox
    Friend WithEvents txtProviderName As System.Windows.Forms.TextBox
    Friend WithEvents btnProvLookUp As System.Windows.Forms.Button
    Friend WithEvents txtProviderID As System.Windows.Forms.TextBox
    Friend WithEvents BW As System.ComponentModel.BackgroundWorker
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PB As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents chkOrigCorr As System.Windows.Forms.CheckBox
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents waitingLabel As System.Windows.Forms.Label

End Class
