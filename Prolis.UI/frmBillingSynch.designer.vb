<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBillingSynch
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBillingSynch))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        btnProcess = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        lblFrom = New Label()
        lblTo = New Label()
        txtAccFrom = New TextBox()
        txtAccTo = New TextBox()
        Label4 = New Label()
        Label5 = New Label()
        dgvDiscrete = New DataGridView()
        AccID = New DataGridViewTextBoxColumn()
        chkResults = New CheckBox()
        chkComplete = New CheckBox()
        chkDotSuppress = New CheckBox()
        chkQNS = New CheckBox()
        chkLAE = New CheckBox()
        grpExceptions = New GroupBox()
        chkSkip = New CheckBox()
        cmbBillType = New ComboBox()
        Label1 = New Label()
        lstTargets = New CheckedListBox()
        btnSelAll = New Button()
        btnDeSel = New Button()
        chkSpecAll = New CheckBox()
        BW = New ComponentModel.BackgroundWorker()
        StatusStrip1 = New StatusStrip()
        lblStatus = New ToolStripStatusLabel()
        PB = New ToolStripProgressBar()
        HelpProvider1 = New HelpProvider()
        dtpDateTo = New DateTimePicker()
        lblClearDates = New Label()
        dtpDateFrom = New DateTimePicker()
        ToolStrip1.SuspendLayout()
        CType(dgvDiscrete, ComponentModel.ISupportInitialize).BeginInit()
        grpExceptions.SuspendLayout()
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
        ToolStrip1.Size = New Size(1007, 34)
        ToolStrip1.TabIndex = 14
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
        ' lblFrom
        ' 
        lblFrom.ForeColor = Color.DarkBlue
        lblFrom.Location = New Point(15, 163)
        lblFrom.Margin = New Padding(5, 0, 5, 0)
        lblFrom.Name = "lblFrom"
        lblFrom.Size = New Size(143, 29)
        lblFrom.TabIndex = 16
        lblFrom.Text = "From"
        ' 
        ' lblTo
        ' 
        lblTo.ForeColor = Color.DarkBlue
        lblTo.Location = New Point(193, 163)
        lblTo.Margin = New Padding(5, 0, 5, 0)
        lblTo.Name = "lblTo"
        lblTo.Size = New Size(173, 25)
        lblTo.TabIndex = 19
        lblTo.Text = "To"
        ' 
        ' txtAccFrom
        ' 
        txtAccFrom.Location = New Point(20, 292)
        txtAccFrom.Margin = New Padding(5, 6, 5, 6)
        txtAccFrom.Name = "txtAccFrom"
        txtAccFrom.Size = New Size(136, 31)
        txtAccFrom.TabIndex = 3
        ' 
        ' txtAccTo
        ' 
        txtAccTo.Location = New Point(188, 292)
        txtAccTo.Margin = New Padding(5, 6, 5, 6)
        txtAccTo.Name = "txtAccTo"
        txtAccTo.Size = New Size(136, 31)
        txtAccTo.TabIndex = 4
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(15, 262)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(140, 25)
        Label4.TabIndex = 22
        Label4.Text = "From Accession"
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.DarkBlue
        Label5.Location = New Point(188, 262)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(162, 25)
        Label5.TabIndex = 23
        Label5.Text = "To Accession"
        ' 
        ' dgvDiscrete
        ' 
        dgvDiscrete.AllowUserToAddRows = False
        dgvDiscrete.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.AliceBlue
        dgvDiscrete.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvDiscrete.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvDiscrete.Columns.AddRange(New DataGridViewColumn() {AccID})
        dgvDiscrete.Location = New Point(20, 358)
        dgvDiscrete.Margin = New Padding(5, 6, 5, 6)
        dgvDiscrete.Name = "dgvDiscrete"
        dgvDiscrete.RowHeadersVisible = False
        dgvDiscrete.RowHeadersWidth = 62
        dgvDiscrete.Size = New Size(342, 356)
        dgvDiscrete.TabIndex = 25
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
        ' chkResults
        ' 
        chkResults.Appearance = Appearance.Button
        chkResults.Checked = True
        chkResults.CheckState = CheckState.Checked
        chkResults.Location = New Point(395, 492)
        chkResults.Margin = New Padding(5, 6, 5, 6)
        chkResults.Name = "chkResults"
        chkResults.Size = New Size(190, 48)
        chkResults.TabIndex = 26
        chkResults.Text = "With Results Only"
        chkResults.TextAlign = ContentAlignment.MiddleCenter
        chkResults.UseVisualStyleBackColor = True
        ' 
        ' chkComplete
        ' 
        chkComplete.Appearance = Appearance.Button
        chkComplete.ForeColor = Color.DarkBlue
        chkComplete.Location = New Point(650, 494)
        chkComplete.Margin = New Padding(5, 6, 5, 6)
        chkComplete.Name = "chkComplete"
        chkComplete.Size = New Size(295, 46)
        chkComplete.TabIndex = 27
        chkComplete.Text = "Final Accessions Only"
        chkComplete.TextAlign = ContentAlignment.MiddleCenter
        chkComplete.UseVisualStyleBackColor = True
        ' 
        ' chkDotSuppress
        ' 
        chkDotSuppress.Checked = True
        chkDotSuppress.CheckState = CheckState.Checked
        chkDotSuppress.ForeColor = Color.Red
        chkDotSuppress.Location = New Point(10, 44)
        chkDotSuppress.Margin = New Padding(5, 6, 5, 6)
        chkDotSuppress.Name = "chkDotSuppress"
        chkDotSuppress.Size = New Size(237, 31)
        chkDotSuppress.TabIndex = 29
        chkDotSuppress.Text = "Dot (.) Suppress"
        chkDotSuppress.UseVisualStyleBackColor = True
        ' 
        ' chkQNS
        ' 
        chkQNS.Checked = True
        chkQNS.CheckState = CheckState.Checked
        chkQNS.ForeColor = Color.Red
        chkQNS.Location = New Point(317, 44)
        chkQNS.Margin = New Padding(5, 6, 5, 6)
        chkQNS.Name = "chkQNS"
        chkQNS.Size = New Size(137, 31)
        chkQNS.TabIndex = 30
        chkQNS.Text = "QNS"
        chkQNS.UseVisualStyleBackColor = True
        ' 
        ' chkLAE
        ' 
        chkLAE.Checked = True
        chkLAE.CheckState = CheckState.Checked
        chkLAE.Location = New Point(10, 87)
        chkLAE.Margin = New Padding(5, 6, 5, 6)
        chkLAE.Name = "chkLAE"
        chkLAE.Size = New Size(258, 38)
        chkLAE.TabIndex = 31
        chkLAE.Text = "Lab Accident (LAE)"
        chkLAE.UseVisualStyleBackColor = True
        ' 
        ' grpExceptions
        ' 
        grpExceptions.Controls.Add(chkSkip)
        grpExceptions.Controls.Add(chkDotSuppress)
        grpExceptions.Controls.Add(chkLAE)
        grpExceptions.Controls.Add(chkQNS)
        grpExceptions.ForeColor = Color.DarkBlue
        grpExceptions.Location = New Point(395, 554)
        grpExceptions.Margin = New Padding(5, 6, 5, 6)
        grpExceptions.Name = "grpExceptions"
        grpExceptions.Padding = New Padding(5, 6, 5, 6)
        grpExceptions.Size = New Size(577, 148)
        grpExceptions.TabIndex = 32
        grpExceptions.TabStop = False
        grpExceptions.Text = "Result Exceptions"
        ' 
        ' chkSkip
        ' 
        chkSkip.Checked = True
        chkSkip.CheckState = CheckState.Checked
        chkSkip.Location = New Point(317, 87)
        chkSkip.Margin = New Padding(5, 6, 5, 6)
        chkSkip.Name = "chkSkip"
        chkSkip.Size = New Size(250, 38)
        chkSkip.TabIndex = 32
        chkSkip.Text = "TNP (Test Not Performed)"
        chkSkip.UseVisualStyleBackColor = True
        ' 
        ' cmbBillType
        ' 
        cmbBillType.DropDownStyle = ComboBoxStyle.DropDownList
        cmbBillType.FormattingEnabled = True
        cmbBillType.Items.AddRange(New Object() {"Client", "Third Party", "Patient", "All"})
        cmbBillType.Location = New Point(395, 108)
        cmbBillType.Margin = New Padding(5, 6, 5, 6)
        cmbBillType.Name = "cmbBillType"
        cmbBillType.Size = New Size(416, 33)
        cmbBillType.TabIndex = 33
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(400, 77)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(117, 25)
        Label1.TabIndex = 34
        Label1.Text = "Billing Type"
        ' 
        ' lstTargets
        ' 
        lstTargets.CheckOnClick = True
        lstTargets.FormattingEnabled = True
        lstTargets.Items.AddRange(New Object() {"All (Clients, Third Parties, Patients)"})
        lstTargets.Location = New Point(395, 196)
        lstTargets.Margin = New Padding(5, 6, 5, 6)
        lstTargets.Name = "lstTargets"
        lstTargets.Size = New Size(574, 256)
        lstTargets.Sorted = True
        lstTargets.TabIndex = 35
        ' 
        ' btnSelAll
        ' 
        btnSelAll.Image = CType(resources.GetObject("btnSelAll.Image"), Image)
        btnSelAll.Location = New Point(838, 98)
        btnSelAll.Margin = New Padding(5, 6, 5, 6)
        btnSelAll.Name = "btnSelAll"
        btnSelAll.Size = New Size(62, 56)
        btnSelAll.TabIndex = 36
        btnSelAll.UseVisualStyleBackColor = True
        ' 
        ' btnDeSel
        ' 
        btnDeSel.Image = CType(resources.GetObject("btnDeSel.Image"), Image)
        btnDeSel.Location = New Point(910, 98)
        btnDeSel.Margin = New Padding(5, 6, 5, 6)
        btnDeSel.Name = "btnDeSel"
        btnDeSel.Size = New Size(62, 56)
        btnDeSel.TabIndex = 37
        btnDeSel.UseVisualStyleBackColor = True
        ' 
        ' chkSpecAll
        ' 
        chkSpecAll.Appearance = Appearance.Button
        chkSpecAll.Location = New Point(20, 100)
        chkSpecAll.Margin = New Padding(5, 6, 5, 6)
        chkSpecAll.Name = "chkSpecAll"
        chkSpecAll.Size = New Size(342, 50)
        chkSpecAll.TabIndex = 38
        chkSpecAll.Text = "Specific Unbilled"
        chkSpecAll.TextAlign = ContentAlignment.MiddleCenter
        chkSpecAll.UseVisualStyleBackColor = True
        ' 
        ' BW
        ' 
        BW.WorkerReportsProgress = True
        BW.WorkerSupportsCancellation = True
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.ImageScalingSize = New Size(24, 24)
        StatusStrip1.Items.AddRange(New ToolStripItem() {lblStatus, PB})
        StatusStrip1.Location = New Point(0, 759)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Padding = New Padding(2, 0, 23, 0)
        StatusStrip1.Size = New Size(1007, 35)
        StatusStrip1.TabIndex = 39
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
        PB.Size = New Size(700, 27)
        PB.Style = ProgressBarStyle.Continuous
        ' 
        ' dtpDateTo
        ' 
        dtpDateTo.CustomFormat = " "
        dtpDateTo.Format = DateTimePickerFormat.Custom
        dtpDateTo.Location = New Point(188, 196)
        dtpDateTo.Margin = New Padding(3, 4, 3, 4)
        dtpDateTo.Name = "dtpDateTo"
        dtpDateTo.Size = New Size(136, 31)
        dtpDateTo.TabIndex = 2
        ' 
        ' lblClearDates
        ' 
        lblClearDates.BackColor = SystemColors.Control
        lblClearDates.Image = My.Resources.Resources.Eraser
        lblClearDates.Location = New Point(333, 188)
        lblClearDates.Name = "lblClearDates"
        lblClearDates.Size = New Size(38, 46)
        lblClearDates.TabIndex = 96
        lblClearDates.Text = "      "
        ' 
        ' dtpDateFrom
        ' 
        dtpDateFrom.CustomFormat = " "
        dtpDateFrom.Format = DateTimePickerFormat.Custom
        dtpDateFrom.Location = New Point(20, 196)
        dtpDateFrom.Margin = New Padding(3, 4, 3, 4)
        dtpDateFrom.Name = "dtpDateFrom"
        dtpDateFrom.Size = New Size(136, 31)
        dtpDateFrom.TabIndex = 1
        ' 
        ' frmBillingSynch
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1007, 794)
        Controls.Add(chkSpecAll)
        Controls.Add(dtpDateTo)
        Controls.Add(lblClearDates)
        Controls.Add(dtpDateFrom)
        Controls.Add(StatusStrip1)
        Controls.Add(btnDeSel)
        Controls.Add(btnSelAll)
        Controls.Add(lstTargets)
        Controls.Add(Label1)
        Controls.Add(cmbBillType)
        Controls.Add(grpExceptions)
        Controls.Add(chkComplete)
        Controls.Add(chkResults)
        Controls.Add(dgvDiscrete)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(txtAccTo)
        Controls.Add(txtAccFrom)
        Controls.Add(lblTo)
        Controls.Add(lblFrom)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        Name = "frmBillingSynch"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Billing Synchronization"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgvDiscrete, ComponentModel.ISupportInitialize).EndInit()
        grpExceptions.ResumeLayout(False)
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
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents txtAccFrom As System.Windows.Forms.TextBox
    Friend WithEvents txtAccTo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dgvDiscrete As System.Windows.Forms.DataGridView
    Friend WithEvents chkResults As System.Windows.Forms.CheckBox
    Friend WithEvents chkComplete As System.Windows.Forms.CheckBox
    Friend WithEvents AccID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkDotSuppress As System.Windows.Forms.CheckBox
    Friend WithEvents chkQNS As System.Windows.Forms.CheckBox
    Friend WithEvents chkLAE As System.Windows.Forms.CheckBox
    Friend WithEvents grpExceptions As System.Windows.Forms.GroupBox
    Friend WithEvents cmbBillType As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lstTargets As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnSelAll As System.Windows.Forms.Button
    Friend WithEvents btnDeSel As System.Windows.Forms.Button
    Friend WithEvents chkSkip As System.Windows.Forms.CheckBox
    Friend WithEvents chkSpecAll As System.Windows.Forms.CheckBox
    Friend WithEvents BW As System.ComponentModel.BackgroundWorker
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PB As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents dtpDateTo As DateTimePicker
    Friend WithEvents lblClearDates As Label
    Friend WithEvents dtpDateFrom As DateTimePicker
End Class
