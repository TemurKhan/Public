<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSendOuts
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSendOuts))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        chkNewEdit = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnSave = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnDelete = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        dgvCandidates = New DataGridView()
        AccID = New DataGridViewTextBoxColumn()
        AccDate = New DataGridViewTextBoxColumn()
        Provider = New DataGridViewTextBoxColumn()
        Patient = New DataGridViewTextBoxColumn()
        Billee = New DataGridViewTextBoxColumn()
        lblCandidates = New Label()
        Label2 = New Label()
        txtAccID = New TextBox()
        dtpDate = New DateTimePicker()
        Label3 = New Label()
        cmbLab = New ComboBox()
        Label4 = New Label()
        cmbBillType = New ComboBox()
        Label5 = New Label()
        dgvOrders = New DataGridView()
        CompID = New DataGridViewTextBoxColumn()
        CompLook = New DataGridViewImageColumn()
        TGP = New DataGridViewTextBoxColumn()
        CompType = New DataGridViewTextBoxColumn()
        Stat = New DataGridViewCheckBoxColumn()
        btnSendoutLook = New Button()
        chkPrintLabels = New CheckBox()
        txtLabelQTY = New TextBox()
        txtReqQTY = New TextBox()
        chkPrintReq = New CheckBox()
        txtNote = New TextBox()
        Label6 = New Label()
        txtID = New TextBox()
        StatusStrip1 = New StatusStrip()
        lblStatus = New ToolStripStatusLabel()
        PB = New ToolStripProgressBar()
        cmbSendID = New ComboBox()
        Label7 = New Label()
        ToolStrip1.SuspendLayout()
        CType(dgvCandidates, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvOrders, ComponentModel.ISupportInitialize).BeginInit()
        StatusStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {chkNewEdit, ToolStripSeparator1, btnSave, ToolStripSeparator2, btnDelete, ToolStripSeparator3, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(1197, 34)
        ToolStrip1.TabIndex = 1
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' chkNewEdit
        ' 
        chkNewEdit.CheckOnClick = True
        chkNewEdit.Image = CType(resources.GetObject("chkNewEdit.Image"), Image)
        chkNewEdit.ImageTransparentColor = Color.Magenta
        chkNewEdit.Name = "chkNewEdit"
        chkNewEdit.Size = New Size(75, 29)
        chkNewEdit.Text = "New"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
        ' 
        ' btnSave
        ' 
        btnSave.Enabled = False
        btnSave.Image = CType(resources.GetObject("btnSave.Image"), Image)
        btnSave.ImageTransparentColor = Color.Magenta
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(77, 29)
        btnSave.Text = "Save"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 34)
        ' 
        ' btnDelete
        ' 
        btnDelete.Enabled = False
        btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), Image)
        btnDelete.ImageTransparentColor = Color.Magenta
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(90, 29)
        btnDelete.Text = "Delete"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(6, 34)
        ' 
        ' btnCancel
        ' 
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
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.ImageTransparentColor = Color.Magenta
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(77, 29)
        btnHelp.Text = "Help"
        ' 
        ' dgvCandidates
        ' 
        dgvCandidates.AllowUserToAddRows = False
        dgvCandidates.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.Linen
        dgvCandidates.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvCandidates.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvCandidates.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvCandidates.Columns.AddRange(New DataGridViewColumn() {AccID, AccDate, Provider, Patient, Billee})
        dgvCandidates.Location = New Point(25, 104)
        dgvCandidates.Margin = New Padding(5, 6, 5, 6)
        dgvCandidates.MultiSelect = False
        dgvCandidates.Name = "dgvCandidates"
        dgvCandidates.ReadOnly = True
        dgvCandidates.RowHeadersVisible = False
        dgvCandidates.RowHeadersWidth = 62
        dgvCandidates.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvCandidates.Size = New Size(1153, 252)
        dgvCandidates.TabIndex = 2
        ' 
        ' AccID
        ' 
        AccID.FillWeight = 90F
        AccID.HeaderText = "Accession"
        AccID.MaxInputLength = 12
        AccID.MinimumWidth = 8
        AccID.Name = "AccID"
        AccID.ReadOnly = True
        ' 
        ' AccDate
        ' 
        AccDate.FillWeight = 80F
        AccDate.HeaderText = "Dated"
        AccDate.MaxInputLength = 10
        AccDate.MinimumWidth = 8
        AccDate.Name = "AccDate"
        AccDate.ReadOnly = True
        ' 
        ' Provider
        ' 
        Provider.FillWeight = 180F
        Provider.HeaderText = "Provider"
        Provider.MaxInputLength = 100
        Provider.MinimumWidth = 8
        Provider.Name = "Provider"
        Provider.ReadOnly = True
        ' 
        ' Patient
        ' 
        Patient.FillWeight = 150F
        Patient.HeaderText = "Patient"
        Patient.MaxInputLength = 100
        Patient.MinimumWidth = 8
        Patient.Name = "Patient"
        Patient.ReadOnly = True
        ' 
        ' Billee
        ' 
        Billee.FillWeight = 170F
        Billee.HeaderText = "Billee"
        Billee.MaxInputLength = 100
        Billee.MinimumWidth = 8
        Billee.Name = "Billee"
        Billee.ReadOnly = True
        ' 
        ' lblCandidates
        ' 
        lblCandidates.Location = New Point(25, 65)
        lblCandidates.Margin = New Padding(5, 0, 5, 0)
        lblCandidates.Name = "lblCandidates"
        lblCandidates.Size = New Size(325, 33)
        lblCandidates.TabIndex = 3
        lblCandidates.Text = "Candidates"
        ' 
        ' Label2
        ' 
        Label2.Location = New Point(25, 381)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(162, 33)
        Label2.TabIndex = 4
        Label2.Text = "Accession ID"
        Label2.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtAccID
        ' 
        txtAccID.Location = New Point(25, 415)
        txtAccID.Margin = New Padding(5, 6, 5, 6)
        txtAccID.Name = "txtAccID"
        txtAccID.Size = New Size(159, 31)
        txtAccID.TabIndex = 5
        txtAccID.TextAlign = HorizontalAlignment.Center
        ' 
        ' dtpDate
        ' 
        dtpDate.Format = DateTimePickerFormat.Short
        dtpDate.Location = New Point(248, 410)
        dtpDate.Margin = New Padding(5, 6, 5, 6)
        dtpDate.Name = "dtpDate"
        dtpDate.Size = New Size(159, 31)
        dtpDate.TabIndex = 6
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(248, 381)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(162, 33)
        Label3.TabIndex = 7
        Label3.Text = "Sendout Date"
        ' 
        ' cmbLab
        ' 
        cmbLab.DropDownStyle = ComboBoxStyle.DropDownList
        cmbLab.FormattingEnabled = True
        cmbLab.Location = New Point(430, 415)
        cmbLab.Margin = New Padding(5, 6, 5, 6)
        cmbLab.Name = "cmbLab"
        cmbLab.Size = New Size(496, 33)
        cmbLab.TabIndex = 8
        ' 
        ' Label4
        ' 
        Label4.Location = New Point(442, 381)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(192, 31)
        Label4.TabIndex = 9
        Label4.Text = "Performing Lab"
        ' 
        ' cmbBillType
        ' 
        cmbBillType.DropDownStyle = ComboBoxStyle.DropDownList
        cmbBillType.FormattingEnabled = True
        cmbBillType.Items.AddRange(New Object() {"Me", "Insurance", "Patient", "Gratis"})
        cmbBillType.Location = New Point(958, 415)
        cmbBillType.Margin = New Padding(5, 6, 5, 6)
        cmbBillType.Name = "cmbBillType"
        cmbBillType.Size = New Size(211, 33)
        cmbBillType.TabIndex = 10
        ' 
        ' Label5
        ' 
        Label5.Location = New Point(953, 381)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(150, 29)
        Label5.TabIndex = 11
        Label5.Text = "Bill To"
        ' 
        ' dgvOrders
        ' 
        dgvOrders.AllowUserToAddRows = False
        dgvOrders.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = Color.FromArgb(CByte(192), CByte(255), CByte(192))
        dgvOrders.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = SystemColors.Control
        DataGridViewCellStyle3.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle3.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = DataGridViewTriState.True
        dgvOrders.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        dgvOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvOrders.Columns.AddRange(New DataGridViewColumn() {CompID, CompLook, TGP, CompType, Stat})
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = SystemColors.Window
        DataGridViewCellStyle4.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle4.ForeColor = Color.DarkBlue
        DataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = DataGridViewTriState.False
        dgvOrders.DefaultCellStyle = DataGridViewCellStyle4
        dgvOrders.Location = New Point(20, 692)
        dgvOrders.Margin = New Padding(5, 6, 5, 6)
        dgvOrders.MultiSelect = False
        dgvOrders.Name = "dgvOrders"
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = SystemColors.Control
        DataGridViewCellStyle5.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle5.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = DataGridViewTriState.True
        dgvOrders.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
        dgvOrders.RowHeadersVisible = False
        dgvOrders.RowHeadersWidth = 62
        dgvOrders.SelectionMode = DataGridViewSelectionMode.CellSelect
        dgvOrders.Size = New Size(1150, 250)
        dgvOrders.TabIndex = 12
        ' 
        ' CompID
        ' 
        CompID.FillWeight = 90F
        CompID.HeaderText = "ID"
        CompID.MaxInputLength = 12
        CompID.MinimumWidth = 8
        CompID.Name = "CompID"
        CompID.SortMode = DataGridViewColumnSortMode.NotSortable
        CompID.ToolTipText = "Component numeric ID"
        ' 
        ' CompLook
        ' 
        CompLook.FillWeight = 60F
        CompLook.HeaderText = "Look up"
        CompLook.Image = CType(resources.GetObject("CompLook.Image"), Image)
        CompLook.MinimumWidth = 8
        CompLook.Name = "CompLook"
        ' 
        ' TGP
        ' 
        TGP.FillWeight = 330F
        TGP.HeaderText = "T. G. P."
        TGP.MinimumWidth = 8
        TGP.Name = "TGP"
        TGP.ReadOnly = True
        TGP.SortMode = DataGridViewColumnSortMode.NotSortable
        TGP.ToolTipText = "Analyte, Group or Profile"
        ' 
        ' CompType
        ' 
        CompType.FillWeight = 120F
        CompType.HeaderText = "Ref Lab No"
        CompType.MaxInputLength = 60
        CompType.MinimumWidth = 8
        CompType.Name = "CompType"
        CompType.Resizable = DataGridViewTriState.True
        CompType.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Stat
        ' 
        Stat.FillWeight = 60F
        Stat.HeaderText = "Stat?"
        Stat.MinimumWidth = 8
        Stat.Name = "Stat"
        ' 
        ' btnSendoutLook
        ' 
        btnSendoutLook.Enabled = False
        btnSendoutLook.Image = CType(resources.GetObject("btnSendoutLook.Image"), Image)
        btnSendoutLook.Location = New Point(197, 410)
        btnSendoutLook.Margin = New Padding(5, 6, 5, 6)
        btnSendoutLook.Name = "btnSendoutLook"
        btnSendoutLook.Size = New Size(42, 46)
        btnSendoutLook.TabIndex = 13
        btnSendoutLook.UseVisualStyleBackColor = True
        ' 
        ' chkPrintLabels
        ' 
        chkPrintLabels.Checked = True
        chkPrintLabels.CheckState = CheckState.Checked
        chkPrintLabels.Location = New Point(23, 971)
        chkPrintLabels.Margin = New Padding(5, 6, 5, 6)
        chkPrintLabels.Name = "chkPrintLabels"
        chkPrintLabels.Size = New Size(137, 40)
        chkPrintLabels.TabIndex = 14
        chkPrintLabels.Text = "Print Labels"
        chkPrintLabels.UseVisualStyleBackColor = True
        ' 
        ' txtLabelQTY
        ' 
        txtLabelQTY.Location = New Point(180, 971)
        txtLabelQTY.Margin = New Padding(5, 6, 5, 6)
        txtLabelQTY.Name = "txtLabelQTY"
        txtLabelQTY.Size = New Size(74, 31)
        txtLabelQTY.TabIndex = 15
        txtLabelQTY.Text = "1"
        txtLabelQTY.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtReqQTY
        ' 
        txtReqQTY.Location = New Point(1090, 971)
        txtReqQTY.Margin = New Padding(5, 6, 5, 6)
        txtReqQTY.Name = "txtReqQTY"
        txtReqQTY.Size = New Size(77, 31)
        txtReqQTY.TabIndex = 17
        txtReqQTY.Text = "1"
        txtReqQTY.TextAlign = HorizontalAlignment.Center
        ' 
        ' chkPrintReq
        ' 
        chkPrintReq.Location = New Point(910, 971)
        chkPrintReq.Margin = New Padding(5, 6, 5, 6)
        chkPrintReq.Name = "chkPrintReq"
        chkPrintReq.Size = New Size(170, 40)
        chkPrintReq.TabIndex = 16
        chkPrintReq.Text = "Print Requisition"
        chkPrintReq.UseVisualStyleBackColor = True
        ' 
        ' txtNote
        ' 
        txtNote.Location = New Point(20, 552)
        txtNote.Margin = New Padding(5, 6, 5, 6)
        txtNote.MaxLength = 960
        txtNote.Multiline = True
        txtNote.Name = "txtNote"
        txtNote.ScrollBars = ScrollBars.Vertical
        txtNote.Size = New Size(1144, 108)
        txtNote.TabIndex = 18
        ' 
        ' Label6
        ' 
        Label6.Location = New Point(23, 517)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(150, 29)
        Label6.TabIndex = 19
        Label6.Text = "Note"
        ' 
        ' txtID
        ' 
        txtID.Location = New Point(958, 469)
        txtID.Margin = New Padding(5, 6, 5, 6)
        txtID.Name = "txtID"
        txtID.Size = New Size(94, 31)
        txtID.TabIndex = 20
        txtID.TextAlign = HorizontalAlignment.Center
        txtID.Visible = False
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.ImageScalingSize = New Size(24, 24)
        StatusStrip1.Items.AddRange(New ToolStripItem() {lblStatus, PB})
        StatusStrip1.Location = New Point(0, 1053)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Padding = New Padding(2, 0, 23, 0)
        StatusStrip1.Size = New Size(1197, 35)
        StatusStrip1.TabIndex = 21
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = False
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(220, 28)
        ' 
        ' PB
        ' 
        PB.AutoSize = False
        PB.Name = "PB"
        PB.Size = New Size(767, 27)
        ' 
        ' cmbSendID
        ' 
        cmbSendID.FormattingEnabled = True
        cmbSendID.Location = New Point(197, 467)
        cmbSendID.Margin = New Padding(5, 6, 5, 6)
        cmbSendID.Name = "cmbSendID"
        cmbSendID.Size = New Size(211, 33)
        cmbSendID.TabIndex = 22
        ' 
        ' Label7
        ' 
        Label7.Location = New Point(57, 473)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(130, 33)
        Label7.TabIndex = 23
        Label7.Text = "Sendout ID"
        Label7.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' frmSendOuts
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1197, 1088)
        Controls.Add(Label7)
        Controls.Add(cmbSendID)
        Controls.Add(StatusStrip1)
        Controls.Add(txtID)
        Controls.Add(Label6)
        Controls.Add(txtNote)
        Controls.Add(txtReqQTY)
        Controls.Add(chkPrintReq)
        Controls.Add(txtLabelQTY)
        Controls.Add(chkPrintLabels)
        Controls.Add(btnSendoutLook)
        Controls.Add(dgvOrders)
        Controls.Add(Label5)
        Controls.Add(cmbBillType)
        Controls.Add(Label4)
        Controls.Add(cmbLab)
        Controls.Add(Label3)
        Controls.Add(dtpDate)
        Controls.Add(txtAccID)
        Controls.Add(Label2)
        Controls.Add(lblCandidates)
        Controls.Add(dgvCandidates)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmSendOuts"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Send Outs"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgvCandidates, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvOrders, ComponentModel.ISupportInitialize).EndInit()
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents chkNewEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents dgvCandidates As System.Windows.Forms.DataGridView
    Friend WithEvents lblCandidates As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtAccID As System.Windows.Forms.TextBox
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbLab As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbBillType As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dgvOrders As System.Windows.Forms.DataGridView
    Friend WithEvents btnSendoutLook As System.Windows.Forms.Button
    Friend WithEvents chkPrintLabels As System.Windows.Forms.CheckBox
    Friend WithEvents txtLabelQTY As System.Windows.Forms.TextBox
    Friend WithEvents txtReqQTY As System.Windows.Forms.TextBox
    Friend WithEvents chkPrintReq As System.Windows.Forms.CheckBox
    Friend WithEvents txtNote As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents AccID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AccDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Provider As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Patient As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Billee As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PB As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents cmbSendID As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents CompID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CompLook As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents TGP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CompType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Stat As System.Windows.Forms.DataGridViewCheckBoxColumn

End Class
