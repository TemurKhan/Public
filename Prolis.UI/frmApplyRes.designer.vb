<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmApplyRes
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmApplyRes))
        Label1 = New Label()
        cmbEquips = New ComboBox()
        btnLoad = New Button()
        dgvAccs = New DataGridView()
        SampleID = New DataGridViewTextBoxColumn()
        RunDate = New DataGridViewTextBoxColumn()
        AccID = New DataGridViewTextBoxColumn()
        IsQC = New DataGridViewCheckBoxColumn()
        chkClear = New CheckBox()
        btnClearTarget = New Button()
        chkOverwrite = New CheckBox()
        chkRelVal = New CheckBox()
        ToolStrip1 = New ToolStrip()
        btnAccQC = New ToolStripButton()
        ToolStripButton2 = New ToolStripSeparator()
        btnProcess = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        cmbBatches = New ComboBox()
        Label4 = New Label()
        txtAccQC = New TextBox()
        lblAccQC = New Label()
        Label6 = New Label()
        txtBatchDate = New MaskedTextBox()
        ProlisHelp = New HelpProvider()
        StatusStrip1 = New StatusStrip()
        lblStatus = New ToolStripStatusLabel()
        pbProcess = New ToolStripProgressBar()
        BW = New ComponentModel.BackgroundWorker()
        dgvControls = New DataGridView()
        QCSampleID = New DataGridViewTextBoxColumn()
        QCRunDate = New DataGridViewTextBoxColumn()
        ControlID = New DataGridViewComboBoxColumn()
        Label2 = New Label()
        SpecificAccessions = New TextBox()
        automap = New Button()
        ProcceeStatus = New ComboBox()
        Label8 = New Label()
        Label5 = New Label()
        cmbOverride = New ComboBox()
        txtValid = New TextBox()
        Label3 = New Label()
        lblDate = New Label()
        txtRunDate = New MaskedTextBox()
        Label7 = New Label()
        Todate = New MaskedTextBox()
        CType(dgvAccs, ComponentModel.ISupportInitialize).BeginInit()
        ToolStrip1.SuspendLayout()
        StatusStrip1.SuspendLayout()
        CType(dgvControls, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(18, 56)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(114, 25)
        Label1.TabIndex = 0
        Label1.Text = "Equipments"
        ' 
        ' cmbEquips
        ' 
        cmbEquips.DropDownStyle = ComboBoxStyle.DropDownList
        cmbEquips.FormattingEnabled = True
        cmbEquips.Location = New Point(21, 88)
        cmbEquips.Margin = New Padding(5, 6, 5, 6)
        cmbEquips.Name = "cmbEquips"
        cmbEquips.Size = New Size(933, 33)
        cmbEquips.TabIndex = 1
        ' 
        ' btnLoad
        ' 
        btnLoad.Location = New Point(801, 250)
        btnLoad.Margin = New Padding(5, 6, 5, 6)
        btnLoad.Name = "btnLoad"
        btnLoad.Size = New Size(154, 52)
        btnLoad.TabIndex = 3
        btnLoad.Text = "Load"
        btnLoad.UseVisualStyleBackColor = True
        ' 
        ' dgvAccs
        ' 
        dgvAccs.AllowUserToAddRows = False
        dgvAccs.AllowUserToDeleteRows = False
        dgvAccs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvAccs.Columns.AddRange(New DataGridViewColumn() {SampleID, RunDate, AccID, IsQC})
        dgvAccs.Location = New Point(21, 398)
        dgvAccs.Margin = New Padding(5, 6, 5, 6)
        dgvAccs.Name = "dgvAccs"
        dgvAccs.RowHeadersVisible = False
        dgvAccs.RowHeadersWidth = 51
        dgvAccs.Size = New Size(934, 486)
        dgvAccs.TabIndex = 4
        ' 
        ' SampleID
        ' 
        SampleID.HeaderText = "Sample ID"
        SampleID.MaxInputLength = 15
        SampleID.MinimumWidth = 6
        SampleID.Name = "SampleID"
        SampleID.ReadOnly = True
        SampleID.Resizable = DataGridViewTriState.True
        SampleID.Width = 125
        ' 
        ' RunDate
        ' 
        RunDate.HeaderText = "Run Date"
        RunDate.MaxInputLength = 25
        RunDate.MinimumWidth = 6
        RunDate.Name = "RunDate"
        RunDate.ReadOnly = True
        RunDate.Width = 125
        ' 
        ' AccID
        ' 
        AccID.FillWeight = 180F
        AccID.HeaderText = "Target ID"
        AccID.MaxInputLength = 12
        AccID.MinimumWidth = 6
        AccID.Name = "AccID"
        AccID.Resizable = DataGridViewTriState.False
        AccID.SortMode = DataGridViewColumnSortMode.NotSortable
        AccID.Width = 180
        ' 
        ' IsQC
        ' 
        IsQC.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        IsQC.HeaderText = "QC ?"
        IsQC.MinimumWidth = 6
        IsQC.Name = "IsQC"
        ' 
        ' chkClear
        ' 
        chkClear.ForeColor = Color.DarkBlue
        chkClear.Location = New Point(19, 902)
        chkClear.Margin = New Padding(5, 6, 5, 6)
        chkClear.Name = "chkClear"
        chkClear.Size = New Size(151, 42)
        chkClear.TabIndex = 5
        chkClear.Text = "Clear Buffer"
        chkClear.UseVisualStyleBackColor = True
        ' 
        ' btnClearTarget
        ' 
        btnClearTarget.ForeColor = Color.Red
        btnClearTarget.Location = New Point(774, 898)
        btnClearTarget.Margin = New Padding(5, 6, 5, 6)
        btnClearTarget.Name = "btnClearTarget"
        btnClearTarget.Size = New Size(181, 52)
        btnClearTarget.TabIndex = 8
        btnClearTarget.Text = "Clear Target List"
        btnClearTarget.UseVisualStyleBackColor = True
        ' 
        ' chkOverwrite
        ' 
        chkOverwrite.ForeColor = Color.DarkBlue
        chkOverwrite.Location = New Point(262, 902)
        chkOverwrite.Margin = New Padding(5, 6, 5, 6)
        chkOverwrite.Name = "chkOverwrite"
        chkOverwrite.Size = New Size(215, 42)
        chkOverwrite.TabIndex = 9
        chkOverwrite.Text = "Overwrite Results"
        chkOverwrite.UseVisualStyleBackColor = True
        ' 
        ' chkRelVal
        ' 
        chkRelVal.ForeColor = Color.DarkBlue
        chkRelVal.Location = New Point(538, 898)
        chkRelVal.Margin = New Padding(5, 6, 5, 6)
        chkRelVal.Name = "chkRelVal"
        chkRelVal.Size = New Size(219, 42)
        chkRelVal.TabIndex = 10
        chkRelVal.Text = "Release Non Panic"
        chkRelVal.UseVisualStyleBackColor = True
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(20, 20)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnAccQC, ToolStripButton2, btnProcess, ToolStripSeparator2, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(984, 34)
        ToolStrip1.TabIndex = 13
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnAccQC
        ' 
        btnAccQC.CheckOnClick = True
        btnAccQC.ForeColor = Color.DarkBlue
        btnAccQC.Image = CType(resources.GetObject("btnAccQC.Image"), Image)
        btnAccQC.ImageTransparentColor = Color.Magenta
        btnAccQC.Name = "btnAccQC"
        btnAccQC.Size = New Size(134, 29)
        btnAccQC.Text = "Accessioned"
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
        btnProcess.Size = New Size(96, 29)
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
        ' cmbBatches
        ' 
        cmbBatches.DropDownStyle = ComboBoxStyle.DropDownList
        cmbBatches.FormattingEnabled = True
        cmbBatches.Location = New Point(264, 267)
        cmbBatches.Margin = New Padding(5, 6, 5, 6)
        cmbBatches.Name = "cmbBatches"
        cmbBatches.Size = New Size(222, 33)
        cmbBatches.TabIndex = 15
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(260, 236)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(161, 25)
        Label4.TabIndex = 14
        Label4.Text = "Batches"
        ' 
        ' txtAccQC
        ' 
        txtAccQC.Location = New Point(496, 267)
        txtAccQC.Margin = New Padding(5, 6, 5, 6)
        txtAccQC.Name = "txtAccQC"
        txtAccQC.ReadOnly = True
        txtAccQC.Size = New Size(294, 31)
        txtAccQC.TabIndex = 17
        txtAccQC.TextAlign = HorizontalAlignment.Center
        ' 
        ' lblAccQC
        ' 
        lblAccQC.ForeColor = Color.DarkBlue
        lblAccQC.Location = New Point(494, 236)
        lblAccQC.Margin = New Padding(5, 0, 5, 0)
        lblAccQC.Name = "lblAccQC"
        lblAccQC.Size = New Size(114, 25)
        lblAccQC.TabIndex = 16
        lblAccQC.Text = "Accessions"
        lblAccQC.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.DarkBlue
        Label6.Location = New Point(19, 239)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(135, 25)
        Label6.TabIndex = 21
        Label6.Text = "Batch Date"
        ' 
        ' txtBatchDate
        ' 
        txtBatchDate.Location = New Point(21, 267)
        txtBatchDate.Margin = New Padding(5, 6, 5, 6)
        txtBatchDate.Mask = "00/00/0000"
        txtBatchDate.Name = "txtBatchDate"
        txtBatchDate.Size = New Size(232, 31)
        txtBatchDate.TabIndex = 20
        txtBatchDate.ValidatingType = GetType(Date)
        ' 
        ' ProlisHelp
        ' 
        ProlisHelp.HelpNamespace = "prolishelp.chm"
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.ImageScalingSize = New Size(20, 20)
        StatusStrip1.Items.AddRange(New ToolStripItem() {lblStatus, pbProcess})
        StatusStrip1.Location = New Point(0, 973)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Padding = New Padding(1, 0, 24, 0)
        StatusStrip1.Size = New Size(984, 36)
        StatusStrip1.TabIndex = 22
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = False
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(100, 29)
        ' 
        ' pbProcess
        ' 
        pbProcess.AutoSize = False
        pbProcess.Name = "pbProcess"
        pbProcess.Size = New Size(650, 28)
        ' 
        ' BW
        ' 
        BW.WorkerReportsProgress = True
        BW.WorkerSupportsCancellation = True
        ' 
        ' dgvControls
        ' 
        dgvControls.AllowUserToAddRows = False
        dgvControls.AllowUserToDeleteRows = False
        dgvControls.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvControls.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvControls.Columns.AddRange(New DataGridViewColumn() {QCSampleID, QCRunDate, ControlID})
        dgvControls.Location = New Point(21, 398)
        dgvControls.Margin = New Padding(5, 6, 5, 6)
        dgvControls.Name = "dgvControls"
        dgvControls.RowHeadersVisible = False
        dgvControls.RowHeadersWidth = 51
        dgvControls.Size = New Size(934, 485)
        dgvControls.TabIndex = 23
        ' 
        ' QCSampleID
        ' 
        QCSampleID.HeaderText = "Sample ID"
        QCSampleID.MaxInputLength = 15
        QCSampleID.MinimumWidth = 6
        QCSampleID.Name = "QCSampleID"
        QCSampleID.ReadOnly = True
        QCSampleID.Resizable = DataGridViewTriState.True
        ' 
        ' QCRunDate
        ' 
        QCRunDate.HeaderText = "Run Date"
        QCRunDate.MaxInputLength = 25
        QCRunDate.MinimumWidth = 6
        QCRunDate.Name = "QCRunDate"
        QCRunDate.ReadOnly = True
        ' 
        ' ControlID
        ' 
        ControlID.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
        ControlID.FillWeight = 252F
        ControlID.HeaderText = "Control ID"
        ControlID.Items.AddRange(New Object() {"Control 1", "Control 2", "Control 3", "Control 4", "Control 5", "Control 6"})
        ControlID.MinimumWidth = 6
        ControlID.Name = "ControlID"
        ControlID.Resizable = DataGridViewTriState.False
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(18, 320)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(274, 28)
        Label2.TabIndex = 24
        Label2.Text = "Specific Accession(comma sep)"
        ' 
        ' SpecificAccessions
        ' 
        SpecificAccessions.Location = New Point(21, 353)
        SpecificAccessions.Margin = New Padding(4, 5, 4, 5)
        SpecificAccessions.MaxLength = 99999999
        SpecificAccessions.Name = "SpecificAccessions"
        SpecificAccessions.Size = New Size(933, 31)
        SpecificAccessions.TabIndex = 25
        ' 
        ' automap
        ' 
        automap.Location = New Point(801, 309)
        automap.Margin = New Padding(4, 5, 4, 5)
        automap.Name = "automap"
        automap.Size = New Size(154, 34)
        automap.TabIndex = 26
        automap.Text = "Load &&Auto Map"
        automap.UseVisualStyleBackColor = True
        ' 
        ' ProcceeStatus
        ' 
        ProcceeStatus.DropDownStyle = ComboBoxStyle.DropDownList
        ProcceeStatus.FormattingEnabled = True
        ProcceeStatus.Items.AddRange(New Object() {"All", "Processed", "Non-Processed"})
        ProcceeStatus.Location = New Point(21, 183)
        ProcceeStatus.Margin = New Padding(5, 6, 5, 6)
        ProcceeStatus.Name = "ProcceeStatus"
        ProcceeStatus.Size = New Size(232, 33)
        ProcceeStatus.TabIndex = 30
        ' 
        ' Label8
        ' 
        Label8.ForeColor = Color.DarkBlue
        Label8.Location = New Point(18, 147)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(152, 25)
        Label8.TabIndex = 29
        Label8.Text = "Process State"
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.DarkBlue
        Label5.Location = New Point(664, 141)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(119, 25)
        Label5.TabIndex = 36
        Label5.Text = "Override %"
        Label5.TextAlign = ContentAlignment.TopCenter
        ' 
        ' cmbOverride
        ' 
        cmbOverride.FormattingEnabled = True
        cmbOverride.Location = New Point(668, 181)
        cmbOverride.Margin = New Padding(5, 6, 5, 6)
        cmbOverride.Name = "cmbOverride"
        cmbOverride.Size = New Size(184, 33)
        cmbOverride.TabIndex = 35
        ' 
        ' txtValid
        ' 
        txtValid.Location = New Point(862, 181)
        txtValid.Margin = New Padding(5, 6, 5, 6)
        txtValid.Name = "txtValid"
        txtValid.ReadOnly = True
        txtValid.Size = New Size(92, 31)
        txtValid.TabIndex = 34
        txtValid.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(862, 141)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(92, 25)
        Label3.TabIndex = 33
        Label3.Text = "Validated"
        Label3.TextAlign = ContentAlignment.TopCenter
        ' 
        ' lblDate
        ' 
        lblDate.ForeColor = Color.DarkBlue
        lblDate.Location = New Point(259, 141)
        lblDate.Margin = New Padding(5, 0, 5, 0)
        lblDate.Name = "lblDate"
        lblDate.Size = New Size(116, 25)
        lblDate.TabIndex = 32
        lblDate.Text = "From Date"
        ' 
        ' txtRunDate
        ' 
        txtRunDate.Location = New Point(262, 183)
        txtRunDate.Margin = New Padding(5, 6, 5, 6)
        txtRunDate.Mask = "00/00/0000"
        txtRunDate.Name = "txtRunDate"
        txtRunDate.Size = New Size(223, 31)
        txtRunDate.TabIndex = 31
        txtRunDate.ValidatingType = GetType(Date)
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.DarkBlue
        Label7.Location = New Point(491, 141)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(116, 25)
        Label7.TabIndex = 38
        Label7.Text = "To Date"
        ' 
        ' Todate
        ' 
        Todate.Location = New Point(495, 183)
        Todate.Margin = New Padding(5, 6, 5, 6)
        Todate.Mask = "00/00/0000"
        Todate.Name = "Todate"
        Todate.Size = New Size(156, 31)
        Todate.TabIndex = 37
        Todate.ValidatingType = GetType(Date)
        ' 
        ' frmApplyRes
        ' 
        AcceptButton = btnLoad
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(984, 1009)
        Controls.Add(Label7)
        Controls.Add(Todate)
        Controls.Add(Label5)
        Controls.Add(cmbOverride)
        Controls.Add(txtValid)
        Controls.Add(Label3)
        Controls.Add(lblDate)
        Controls.Add(txtRunDate)
        Controls.Add(ProcceeStatus)
        Controls.Add(Label8)
        Controls.Add(automap)
        Controls.Add(SpecificAccessions)
        Controls.Add(Label2)
        Controls.Add(dgvControls)
        Controls.Add(StatusStrip1)
        Controls.Add(Label6)
        Controls.Add(txtBatchDate)
        Controls.Add(txtAccQC)
        Controls.Add(lblAccQC)
        Controls.Add(cmbBatches)
        Controls.Add(Label4)
        Controls.Add(ToolStrip1)
        Controls.Add(chkRelVal)
        Controls.Add(chkOverwrite)
        Controls.Add(btnClearTarget)
        Controls.Add(chkClear)
        Controls.Add(dgvAccs)
        Controls.Add(btnLoad)
        Controls.Add(cmbEquips)
        Controls.Add(Label1)
        ProlisHelp.SetHelpKeyword(Me, "html\hs1054.htm")
        ProlisHelp.SetHelpNavigator(Me, HelpNavigator.Topic)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximumSize = New Size(1120, 1375)
        Name = "frmApplyRes"
        ProlisHelp.SetShowHelp(Me, True)
        Text = "Apply Results"
        CType(dgvAccs, ComponentModel.ISupportInitialize).EndInit()
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        CType(dgvControls, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbEquips As System.Windows.Forms.ComboBox
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents dgvAccs As System.Windows.Forms.DataGridView
    Friend WithEvents chkClear As System.Windows.Forms.CheckBox
    Friend WithEvents btnClearTarget As System.Windows.Forms.Button
    Friend WithEvents chkOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents chkRelVal As System.Windows.Forms.CheckBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnAccQC As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnProcess As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbBatches As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents SampleID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RunDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AccID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IsQC As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents txtAccQC As System.Windows.Forms.TextBox
    Friend WithEvents lblAccQC As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtBatchDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents ProlisHelp As System.Windows.Forms.HelpProvider
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pbProcess As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents BW As System.ComponentModel.BackgroundWorker
    Friend WithEvents dgvControls As System.Windows.Forms.DataGridView
    Friend WithEvents QCSampleID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QCRunDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ControlID As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents SpecificAccessions As System.Windows.Forms.TextBox
    Friend WithEvents automap As System.Windows.Forms.Button
    Friend WithEvents ProcceeStatus As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents cmbOverride As ComboBox
    Friend WithEvents txtValid As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents lblDate As Label
    Friend WithEvents txtRunDate As MaskedTextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Todate As MaskedTextBox
End Class
