<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEBillOut
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEBillOut))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
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
        chkAccInv = New CheckBox()
        chk8371500 = New CheckBox()
        cmbPartner = New ComboBox()
        Label1 = New Label()
        grpDateRange = New GroupBox()
        dtpDateTo = New DateTimePicker()
        lblClearDates = New Label()
        dtpDateFrom = New DateTimePicker()
        chkSvcBill = New CheckBox()
        Label4 = New Label()
        Label3 = New Label()
        chkPT = New CheckBox()
        lstPayers = New CheckedListBox()
        Label2 = New Label()
        btnDeSel = New Button()
        btnSelAll = New Button()
        txtProcessDate = New MaskedTextBox()
        cmbStatus = New ComboBox()
        Label7 = New Label()
        Label8 = New Label()
        dgvDiscrete = New DataGridView()
        AccInv = New DataGridViewTextBoxColumn()
        ToolStrip1.SuspendLayout()
        grpAccRange.SuspendLayout()
        grpDateRange.SuspendLayout()
        CType(dgvDiscrete, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnProcess, ToolStripSeparator1, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(843, 34)
        ToolStrip1.TabIndex = 5
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
        btnHelp.ForeColor = Color.DarkBlue
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.ImageTransparentColor = Color.Magenta
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(77, 29)
        btnHelp.Text = "Help"
        ' 
        ' grpAccRange
        ' 
        grpAccRange.Controls.Add(Label6)
        grpAccRange.Controls.Add(Label5)
        grpAccRange.Controls.Add(txtAccTo)
        grpAccRange.Controls.Add(txtAccFrom)
        grpAccRange.Location = New Point(238, 179)
        grpAccRange.Margin = New Padding(5, 6, 5, 6)
        grpAccRange.Name = "grpAccRange"
        grpAccRange.Padding = New Padding(5, 6, 5, 6)
        grpAccRange.Size = New Size(580, 102)
        grpAccRange.TabIndex = 6
        grpAccRange.TabStop = False
        grpAccRange.Text = "Accession Range"
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.DarkBlue
        Label6.Location = New Point(328, 44)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(40, 35)
        Label6.TabIndex = 17
        Label6.Text = "To"
        Label6.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.DarkBlue
        Label5.Location = New Point(48, 44)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(58, 35)
        Label5.TabIndex = 16
        Label5.Text = "From"
        Label5.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' txtAccTo
        ' 
        txtAccTo.Location = New Point(378, 40)
        txtAccTo.Margin = New Padding(5, 6, 5, 6)
        txtAccTo.MaxLength = 10
        txtAccTo.Name = "txtAccTo"
        txtAccTo.Size = New Size(184, 31)
        txtAccTo.TabIndex = 4
        txtAccTo.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtAccFrom
        ' 
        txtAccFrom.Location = New Point(117, 40)
        txtAccFrom.Margin = New Padding(5, 6, 5, 6)
        txtAccFrom.MaxLength = 10
        txtAccFrom.Name = "txtAccFrom"
        txtAccFrom.Size = New Size(184, 31)
        txtAccFrom.TabIndex = 3
        txtAccFrom.TextAlign = HorizontalAlignment.Center
        ' 
        ' chkAccInv
        ' 
        chkAccInv.Appearance = Appearance.Button
        chkAccInv.Location = New Point(22, 215)
        chkAccInv.Margin = New Padding(5, 6, 5, 6)
        chkAccInv.Name = "chkAccInv"
        chkAccInv.Size = New Size(173, 42)
        chkAccInv.TabIndex = 18
        chkAccInv.Text = "ACCESSION"
        chkAccInv.TextAlign = ContentAlignment.MiddleCenter
        chkAccInv.UseVisualStyleBackColor = True
        ' 
        ' chk8371500
        ' 
        chk8371500.Appearance = Appearance.Button
        chk8371500.Location = New Point(22, 104)
        chk8371500.Margin = New Padding(5, 6, 5, 6)
        chk8371500.Name = "chk8371500"
        chk8371500.Size = New Size(173, 42)
        chk8371500.TabIndex = 1
        chk8371500.TabStop = False
        chk8371500.Text = "837"
        chk8371500.TextAlign = ContentAlignment.MiddleCenter
        chk8371500.UseVisualStyleBackColor = True
        ' 
        ' cmbPartner
        ' 
        cmbPartner.FormattingEnabled = True
        cmbPartner.Location = New Point(270, 331)
        cmbPartner.Margin = New Padding(5, 6, 5, 6)
        cmbPartner.Name = "cmbPartner"
        cmbPartner.Size = New Size(546, 33)
        cmbPartner.TabIndex = 2
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(273, 292)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(187, 33)
        Label1.TabIndex = 18
        Label1.Text = "Clearing House"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' grpDateRange
        ' 
        grpDateRange.Controls.Add(dtpDateTo)
        grpDateRange.Controls.Add(lblClearDates)
        grpDateRange.Controls.Add(dtpDateFrom)
        grpDateRange.Controls.Add(chkSvcBill)
        grpDateRange.Controls.Add(Label4)
        grpDateRange.Controls.Add(Label3)
        grpDateRange.Location = New Point(238, 71)
        grpDateRange.Margin = New Padding(5, 6, 5, 6)
        grpDateRange.Name = "grpDateRange"
        grpDateRange.Padding = New Padding(5, 6, 5, 6)
        grpDateRange.Size = New Size(580, 96)
        grpDateRange.TabIndex = 19
        grpDateRange.TabStop = False
        grpDateRange.Text = "Date Range"
        ' 
        ' dtpDateTo
        ' 
        dtpDateTo.CustomFormat = " "
        dtpDateTo.Format = DateTimePickerFormat.Custom
        dtpDateTo.Location = New Point(390, 35)
        dtpDateTo.Margin = New Padding(3, 4, 3, 4)
        dtpDateTo.Name = "dtpDateTo"
        dtpDateTo.Size = New Size(129, 31)
        dtpDateTo.TabIndex = 95
        ' 
        ' lblClearDates
        ' 
        lblClearDates.BackColor = SystemColors.Control
        lblClearDates.Image = My.Resources.Resources.Eraser
        lblClearDates.Location = New Point(528, 31)
        lblClearDates.Name = "lblClearDates"
        lblClearDates.Size = New Size(33, 46)
        lblClearDates.TabIndex = 96
        lblClearDates.Text = "      "
        ' 
        ' dtpDateFrom
        ' 
        dtpDateFrom.CustomFormat = " "
        dtpDateFrom.Format = DateTimePickerFormat.Custom
        dtpDateFrom.Location = New Point(203, 35)
        dtpDateFrom.Margin = New Padding(3, 4, 3, 4)
        dtpDateFrom.Name = "dtpDateFrom"
        dtpDateFrom.Size = New Size(129, 31)
        dtpDateFrom.TabIndex = 94
        ' 
        ' chkSvcBill
        ' 
        chkSvcBill.Appearance = Appearance.Button
        chkSvcBill.Location = New Point(10, 33)
        chkSvcBill.Margin = New Padding(5, 6, 5, 6)
        chkSvcBill.Name = "chkSvcBill"
        chkSvcBill.Size = New Size(123, 42)
        chkSvcBill.TabIndex = 19
        chkSvcBill.Text = "SERVICE"
        chkSvcBill.TextAlign = ContentAlignment.MiddleCenter
        chkSvcBill.UseVisualStyleBackColor = True
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(343, 37)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(38, 35)
        Label4.TabIndex = 17
        Label4.Text = "To"
        Label4.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(143, 37)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(57, 35)
        Label3.TabIndex = 16
        Label3.Text = "From"
        Label3.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' chkPT
        ' 
        chkPT.Appearance = Appearance.Button
        chkPT.Checked = True
        chkPT.CheckState = CheckState.Checked
        chkPT.Location = New Point(485, 756)
        chkPT.Margin = New Padding(5, 6, 5, 6)
        chkPT.Name = "chkPT"
        chkPT.Size = New Size(150, 50)
        chkPT.TabIndex = 8
        chkPT.TabStop = False
        chkPT.Text = "Production"
        chkPT.TextAlign = ContentAlignment.MiddleCenter
        chkPT.UseVisualStyleBackColor = True
        ' 
        ' lstPayers
        ' 
        lstPayers.FormattingEnabled = True
        lstPayers.Location = New Point(270, 421)
        lstPayers.Margin = New Padding(5, 6, 5, 6)
        lstPayers.Name = "lstPayers"
        lstPayers.Size = New Size(546, 284)
        lstPayers.Sorted = True
        lstPayers.TabIndex = 20
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(287, 383)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(182, 33)
        Label2.TabIndex = 21
        Label2.Text = "Payers"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' btnDeSel
        ' 
        btnDeSel.Image = CType(resources.GetObject("btnDeSel.Image"), Image)
        btnDeSel.Location = New Point(772, 758)
        btnDeSel.Margin = New Padding(5, 6, 5, 6)
        btnDeSel.Name = "btnDeSel"
        btnDeSel.Size = New Size(47, 48)
        btnDeSel.TabIndex = 39
        btnDeSel.UseVisualStyleBackColor = True
        ' 
        ' btnSelAll
        ' 
        btnSelAll.Image = CType(resources.GetObject("btnSelAll.Image"), Image)
        btnSelAll.Location = New Point(715, 758)
        btnSelAll.Margin = New Padding(5, 6, 5, 6)
        btnSelAll.Name = "btnSelAll"
        btnSelAll.Size = New Size(47, 48)
        btnSelAll.TabIndex = 38
        btnSelAll.UseVisualStyleBackColor = True
        ' 
        ' txtProcessDate
        ' 
        txtProcessDate.Location = New Point(270, 765)
        txtProcessDate.Margin = New Padding(5, 6, 5, 6)
        txtProcessDate.Mask = "00/00/0000"
        txtProcessDate.Name = "txtProcessDate"
        txtProcessDate.Size = New Size(144, 31)
        txtProcessDate.TabIndex = 40
        txtProcessDate.ValidatingType = GetType(Date)
        ' 
        ' cmbStatus
        ' 
        cmbStatus.FormattingEnabled = True
        cmbStatus.Items.AddRange(New Object() {"All Claims", "Processed", "Unprocessed"})
        cmbStatus.Location = New Point(22, 763)
        cmbStatus.Margin = New Padding(5, 6, 5, 6)
        cmbStatus.Name = "cmbStatus"
        cmbStatus.Size = New Size(219, 33)
        cmbStatus.TabIndex = 41
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.DarkBlue
        Label7.Location = New Point(32, 723)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(212, 35)
        Label7.TabIndex = 42
        Label7.Text = "Claim Status"
        Label7.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label8
        ' 
        Label8.ForeColor = Color.DarkBlue
        Label8.Location = New Point(273, 725)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(152, 35)
        Label8.TabIndex = 43
        Label8.Text = "Date Processed"
        Label8.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' dgvDiscrete
        ' 
        dgvDiscrete.AllowUserToAddRows = False
        dgvDiscrete.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.WhiteSmoke
        dgvDiscrete.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvDiscrete.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvDiscrete.Columns.AddRange(New DataGridViewColumn() {AccInv})
        dgvDiscrete.Location = New Point(22, 292)
        dgvDiscrete.Margin = New Padding(5, 6, 5, 6)
        dgvDiscrete.Name = "dgvDiscrete"
        dgvDiscrete.RowHeadersVisible = False
        dgvDiscrete.RowHeadersWidth = 62
        dgvDiscrete.Size = New Size(222, 425)
        dgvDiscrete.TabIndex = 44
        ' 
        ' AccInv
        ' 
        AccInv.FillWeight = 105F
        AccInv.HeaderText = "Discrete"
        AccInv.MaxInputLength = 12
        AccInv.MinimumWidth = 8
        AccInv.Name = "AccInv"
        AccInv.Width = 105
        ' 
        ' frmEBillOut
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(843, 833)
        Controls.Add(chkAccInv)
        Controls.Add(dgvDiscrete)
        Controls.Add(Label8)
        Controls.Add(Label7)
        Controls.Add(cmbStatus)
        Controls.Add(txtProcessDate)
        Controls.Add(btnDeSel)
        Controls.Add(btnSelAll)
        Controls.Add(Label2)
        Controls.Add(lstPayers)
        Controls.Add(chkPT)
        Controls.Add(grpDateRange)
        Controls.Add(Label1)
        Controls.Add(cmbPartner)
        Controls.Add(chk8371500)
        Controls.Add(grpAccRange)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmEBillOut"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Electronic Billing Output"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        grpAccRange.ResumeLayout(False)
        grpAccRange.PerformLayout()
        grpDateRange.ResumeLayout(False)
        CType(dgvDiscrete, ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtAccTo As System.Windows.Forms.TextBox
    Friend WithEvents txtAccFrom As System.Windows.Forms.TextBox
    Friend WithEvents chk8371500 As System.Windows.Forms.CheckBox
    Friend WithEvents cmbPartner As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grpDateRange As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkPT As System.Windows.Forms.CheckBox
    Friend WithEvents lstPayers As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnDeSel As System.Windows.Forms.Button
    Friend WithEvents btnSelAll As System.Windows.Forms.Button
    Friend WithEvents txtProcessDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents chkAccInv As System.Windows.Forms.CheckBox
    Friend WithEvents chkSvcBill As System.Windows.Forms.CheckBox
    Friend WithEvents dgvDiscrete As System.Windows.Forms.DataGridView
    Friend WithEvents AccInv As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dtpDateTo As DateTimePicker
    Friend WithEvents lblClearDates As Label
    Friend WithEvents dtpDateFrom As DateTimePicker
End Class
