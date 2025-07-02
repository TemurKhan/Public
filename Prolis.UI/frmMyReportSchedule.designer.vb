<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMyReportSchedule
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
        components = New ComponentModel.Container()
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMyReportSchedule))
        dgvSchedule = New DataGridView()
        ScheduleID = New DataGridViewTextBoxColumn()
        StartDate = New DataGridViewTextBoxColumn()
        Source = New DataGridViewTextBoxColumn()
        Interval = New DataGridViewTextBoxColumn()
        Frequency = New DataGridViewTextBoxColumn()
        Disburse = New DataGridViewTextBoxColumn()
        Address = New DataGridViewTextBoxColumn()
        LastRan = New DataGridViewTextBoxColumn()
        Remaining = New DataGridViewTextBoxColumn()
        btnNew = New Button()
        txtID = New TextBox()
        Label1 = New Label()
        Label2 = New Label()
        txtSource = New TextBox()
        btnRPTLookup = New Button()
        Label3 = New Label()
        txtFrequency = New TextBox()
        cmbInterval = New ComboBox()
        Label4 = New Label()
        lblAddress = New Label()
        txtAddress = New TextBox()
        btnSave = New Button()
        btnDelete = New Button()
        ToolTip1 = New ToolTip(components)
        cmbSourceType = New ComboBox()
        cmbAddressType = New ComboBox()
        cmbOutput = New ComboBox()
        dtpStartDate = New DateTimePicker()
        Label6 = New Label()
        HelpProvider1 = New HelpProvider()
        btnHelp = New Button()
        Label7 = New Label()
        Label8 = New Label()
        Label9 = New Label()
        CType(dgvSchedule, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' dgvSchedule
        ' 
        dgvSchedule.AllowUserToAddRows = False
        dgvSchedule.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.MintCream
        dgvSchedule.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvSchedule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvSchedule.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvSchedule.Columns.AddRange(New DataGridViewColumn() {ScheduleID, StartDate, Source, Interval, Frequency, Disburse, Address, LastRan, Remaining})
        dgvSchedule.Location = New Point(18, 21)
        dgvSchedule.Margin = New Padding(5, 6, 5, 6)
        dgvSchedule.Name = "dgvSchedule"
        dgvSchedule.ReadOnly = True
        dgvSchedule.RowHeadersVisible = False
        dgvSchedule.RowHeadersWidth = 62
        dgvSchedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvSchedule.Size = New Size(1073, 348)
        dgvSchedule.TabIndex = 0
        ToolTip1.SetToolTip(dgvSchedule, "Double click the displayed schedule, to edit.")
        ' 
        ' ScheduleID
        ' 
        ScheduleID.FillWeight = 60F
        ScheduleID.HeaderText = "ID"
        ScheduleID.MaxInputLength = 6
        ScheduleID.MinimumWidth = 8
        ScheduleID.Name = "ScheduleID"
        ScheduleID.ReadOnly = True
        ' 
        ' StartDate
        ' 
        StartDate.FillWeight = 80F
        StartDate.HeaderText = "Start Date"
        StartDate.MinimumWidth = 8
        StartDate.Name = "StartDate"
        StartDate.ReadOnly = True
        ' 
        ' Source
        ' 
        Source.FillWeight = 80F
        Source.HeaderText = "Report/Script"
        Source.MinimumWidth = 8
        Source.Name = "Source"
        Source.ReadOnly = True
        ' 
        ' Interval
        ' 
        Interval.FillWeight = 70F
        Interval.HeaderText = "Interval"
        Interval.MinimumWidth = 8
        Interval.Name = "Interval"
        Interval.ReadOnly = True
        ' 
        ' Frequency
        ' 
        Frequency.FillWeight = 60F
        Frequency.HeaderText = "Frequency"
        Frequency.MinimumWidth = 8
        Frequency.Name = "Frequency"
        Frequency.ReadOnly = True
        ' 
        ' Disburse
        ' 
        Disburse.FillWeight = 60F
        Disburse.HeaderText = "Disburse"
        Disburse.MinimumWidth = 8
        Disburse.Name = "Disburse"
        Disburse.ReadOnly = True
        Disburse.Resizable = DataGridViewTriState.True
        ' 
        ' Address
        ' 
        Address.FillWeight = 80F
        Address.HeaderText = "Address"
        Address.MinimumWidth = 8
        Address.Name = "Address"
        Address.ReadOnly = True
        ' 
        ' LastRan
        ' 
        LastRan.FillWeight = 75F
        LastRan.HeaderText = "Last Ran"
        LastRan.MinimumWidth = 8
        LastRan.Name = "LastRan"
        LastRan.ReadOnly = True
        LastRan.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Remaining
        ' 
        Remaining.FillWeight = 60F
        Remaining.HeaderText = "Remaining"
        Remaining.MinimumWidth = 8
        Remaining.Name = "Remaining"
        Remaining.ReadOnly = True
        ' 
        ' btnNew
        ' 
        btnNew.Image = CType(resources.GetObject("btnNew.Image"), Image)
        btnNew.Location = New Point(18, 392)
        btnNew.Margin = New Padding(5, 6, 5, 6)
        btnNew.Name = "btnNew"
        btnNew.Size = New Size(208, 71)
        btnNew.TabIndex = 1
        btnNew.Text = "New Schedule"
        btnNew.TextAlign = ContentAlignment.MiddleRight
        btnNew.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(btnNew, "Click me to enter a new schedule.")
        btnNew.UseVisualStyleBackColor = True
        ' 
        ' txtID
        ' 
        txtID.Location = New Point(265, 425)
        txtID.Margin = New Padding(5, 6, 5, 6)
        txtID.Name = "txtID"
        txtID.ReadOnly = True
        txtID.Size = New Size(151, 31)
        txtID.TabIndex = 2
        txtID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.Red
        Label1.Location = New Point(265, 390)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(153, 29)
        Label1.TabIndex = 3
        Label1.Text = "Schedule ID"
        Label1.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.Red
        Label2.Location = New Point(282, 502)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(173, 29)
        Label2.TabIndex = 5
        Label2.Text = "Report or Script"
        ' 
        ' txtSource
        ' 
        txtSource.Location = New Point(265, 538)
        txtSource.Margin = New Padding(5, 6, 5, 6)
        txtSource.Multiline = True
        txtSource.Name = "txtSource"
        txtSource.Size = New Size(771, 175)
        txtSource.TabIndex = 4
        ' 
        ' btnRPTLookup
        ' 
        btnRPTLookup.Image = CType(resources.GetObject("btnRPTLookup.Image"), Image)
        btnRPTLookup.Location = New Point(1048, 537)
        btnRPTLookup.Margin = New Padding(5, 6, 5, 6)
        btnRPTLookup.Name = "btnRPTLookup"
        btnRPTLookup.Size = New Size(43, 40)
        btnRPTLookup.TabIndex = 6
        ToolTip1.SetToolTip(btnRPTLookup, "Click me to select the report to run on a schedule.")
        btnRPTLookup.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.Red
        Label3.Location = New Point(677, 390)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(138, 29)
        Label3.TabIndex = 8
        Label3.Text = "Interval"
        ' 
        ' txtFrequency
        ' 
        txtFrequency.Location = New Point(867, 425)
        txtFrequency.Margin = New Padding(5, 6, 5, 6)
        txtFrequency.Name = "txtFrequency"
        txtFrequency.Size = New Size(104, 31)
        txtFrequency.TabIndex = 7
        txtFrequency.Text = "Infinite"
        txtFrequency.TextAlign = HorizontalAlignment.Center
        ToolTip1.SetToolTip(txtFrequency, "Leave blank to run schedule indefinitely or type a non zero number")
        ' 
        ' cmbInterval
        ' 
        cmbInterval.DropDownStyle = ComboBoxStyle.DropDownList
        cmbInterval.FormattingEnabled = True
        cmbInterval.Items.AddRange(New Object() {"Day", "Week", "Bi-Week", "Month", "Quarter", "Semi-Annual", "Annual"})
        cmbInterval.Location = New Point(662, 423)
        cmbInterval.Margin = New Padding(5, 6, 5, 6)
        cmbInterval.Name = "cmbInterval"
        cmbInterval.Size = New Size(177, 33)
        cmbInterval.TabIndex = 9
        ToolTip1.SetToolTip(cmbInterval, "Select the interval you want the report to run at.")
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.Red
        Label4.Location = New Point(867, 390)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(107, 29)
        Label4.TabIndex = 10
        Label4.Text = "Frequency"
        Label4.TextAlign = ContentAlignment.TopCenter
        ' 
        ' lblAddress
        ' 
        lblAddress.ForeColor = Color.Red
        lblAddress.Location = New Point(282, 756)
        lblAddress.Margin = New Padding(5, 0, 5, 0)
        lblAddress.Name = "lblAddress"
        lblAddress.Size = New Size(377, 29)
        lblAddress.TabIndex = 11
        lblAddress.Text = "Valid Email"
        ' 
        ' txtAddress
        ' 
        txtAddress.Location = New Point(265, 790)
        txtAddress.Margin = New Padding(5, 6, 5, 6)
        txtAddress.Name = "txtAddress"
        txtAddress.Size = New Size(771, 31)
        txtAddress.TabIndex = 12
        ToolTip1.SetToolTip(txtAddress, "Enter a valid email address where to send the report to.")
        ' 
        ' btnSave
        ' 
        btnSave.Enabled = False
        btnSave.Image = CType(resources.GetObject("btnSave.Image"), Image)
        btnSave.Location = New Point(333, 843)
        btnSave.Margin = New Padding(5, 6, 5, 6)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(208, 42)
        btnSave.TabIndex = 13
        btnSave.Text = "Save"
        btnSave.TextAlign = ContentAlignment.MiddleRight
        btnSave.TextImageRelation = TextImageRelation.ImageBeforeText
        btnSave.UseVisualStyleBackColor = True
        ' 
        ' btnDelete
        ' 
        btnDelete.Enabled = False
        btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), Image)
        btnDelete.Location = New Point(570, 843)
        btnDelete.Margin = New Padding(5, 6, 5, 6)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(208, 42)
        btnDelete.TabIndex = 14
        btnDelete.Text = "Delete"
        btnDelete.TextAlign = ContentAlignment.MiddleRight
        btnDelete.TextImageRelation = TextImageRelation.ImageBeforeText
        btnDelete.UseVisualStyleBackColor = True
        ' 
        ' cmbSourceType
        ' 
        cmbSourceType.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSourceType.FormattingEnabled = True
        cmbSourceType.Items.AddRange(New Object() {"Report", "Script"})
        cmbSourceType.Location = New Point(18, 538)
        cmbSourceType.Margin = New Padding(5, 6, 5, 6)
        cmbSourceType.Name = "cmbSourceType"
        cmbSourceType.Size = New Size(206, 33)
        cmbSourceType.TabIndex = 18
        ToolTip1.SetToolTip(cmbSourceType, "Select the interval you want the report to run at.")
        ' 
        ' cmbAddressType
        ' 
        cmbAddressType.DropDownStyle = ComboBoxStyle.DropDownList
        cmbAddressType.FormattingEnabled = True
        cmbAddressType.Items.AddRange(New Object() {"Email", "Folder"})
        cmbAddressType.Location = New Point(18, 788)
        cmbAddressType.Margin = New Padding(5, 6, 5, 6)
        cmbAddressType.Name = "cmbAddressType"
        cmbAddressType.Size = New Size(206, 33)
        cmbAddressType.TabIndex = 21
        ToolTip1.SetToolTip(cmbAddressType, "Select the interval you want the report to run at.")
        ' 
        ' cmbOutput
        ' 
        cmbOutput.DropDownStyle = ComboBoxStyle.DropDownList
        cmbOutput.FormattingEnabled = True
        cmbOutput.Items.AddRange(New Object() {"PDF", "Excel", "CSV", "TAB"})
        cmbOutput.Location = New Point(18, 677)
        cmbOutput.Margin = New Padding(5, 6, 5, 6)
        cmbOutput.Name = "cmbOutput"
        cmbOutput.Size = New Size(206, 33)
        cmbOutput.TabIndex = 23
        ToolTip1.SetToolTip(cmbOutput, "Select the interval you want the report to run at.")
        ' 
        ' dtpStartDate
        ' 
        dtpStartDate.Format = DateTimePickerFormat.Short
        dtpStartDate.Location = New Point(457, 425)
        dtpStartDate.Margin = New Padding(5, 6, 5, 6)
        dtpStartDate.Name = "dtpStartDate"
        dtpStartDate.Size = New Size(177, 31)
        dtpStartDate.TabIndex = 15
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.Red
        Label6.Location = New Point(455, 390)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(138, 29)
        Label6.TabIndex = 16
        Label6.Text = "Start Date"
        Label6.TextAlign = ContentAlignment.TopCenter
        ' 
        ' HelpProvider1
        ' 
        HelpProvider1.HelpNamespace = "ProlisHelp.chm"
        HelpProvider1.Tag = "35"
        ' 
        ' btnHelp
        ' 
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.Location = New Point(1023, 394)
        btnHelp.Margin = New Padding(5, 6, 5, 6)
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(68, 69)
        btnHelp.TabIndex = 17
        btnHelp.UseVisualStyleBackColor = True
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.MediumBlue
        Label7.Location = New Point(20, 502)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(138, 29)
        Label7.TabIndex = 19
        Label7.Text = "Source Type"
        Label7.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label8
        ' 
        Label8.ForeColor = Color.MediumBlue
        Label8.Location = New Point(38, 754)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(145, 29)
        Label8.TabIndex = 22
        Label8.Text = "Destination Type"
        Label8.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label9
        ' 
        Label9.ForeColor = Color.Red
        Label9.Location = New Point(38, 642)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(173, 29)
        Label9.TabIndex = 24
        Label9.Text = "Output Format"
        ' 
        ' frmMyReportSchedule
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1102, 900)
        Controls.Add(Label9)
        Controls.Add(cmbOutput)
        Controls.Add(Label8)
        Controls.Add(cmbAddressType)
        Controls.Add(Label7)
        Controls.Add(cmbSourceType)
        Controls.Add(btnHelp)
        Controls.Add(Label6)
        Controls.Add(dtpStartDate)
        Controls.Add(btnDelete)
        Controls.Add(btnSave)
        Controls.Add(txtAddress)
        Controls.Add(lblAddress)
        Controls.Add(Label4)
        Controls.Add(cmbInterval)
        Controls.Add(Label3)
        Controls.Add(txtFrequency)
        Controls.Add(btnRPTLookup)
        Controls.Add(Label2)
        Controls.Add(txtSource)
        Controls.Add(Label1)
        Controls.Add(txtID)
        Controls.Add(btnNew)
        Controls.Add(dgvSchedule)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MaximumSize = New Size(1124, 956)
        MinimumSize = New Size(1124, 956)
        Name = "frmMyReportSchedule"
        Text = "My Report Schedule"
        CType(dgvSchedule, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents dgvSchedule As System.Windows.Forms.DataGridView
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtSource As System.Windows.Forms.TextBox
    Friend WithEvents btnRPTLookup As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtFrequency As System.Windows.Forms.TextBox
    Friend WithEvents cmbInterval As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblAddress As System.Windows.Forms.Label
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents dtpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents cmbSourceType As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbAddressType As System.Windows.Forms.ComboBox
    Friend WithEvents cmbOutput As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ScheduleID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StartDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Source As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Interval As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Frequency As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Disburse As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Address As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LastRan As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Remaining As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
