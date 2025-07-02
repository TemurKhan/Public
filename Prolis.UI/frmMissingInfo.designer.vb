<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMissingInfo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMissingInfo))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        btnPrint = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        GroupBox1 = New GroupBox()
        dtpDateTo = New DateTimePicker()
        lblClearDates = New Label()
        dtpDateFrom = New DateTimePicker()
        chkTPAll = New CheckBox()
        btnLoad = New Button()
        Label5 = New Label()
        Label4 = New Label()
        Label3 = New Label()
        txtAccTo = New TextBox()
        txtAccFrom = New TextBox()
        Label2 = New Label()
        Label1 = New Label()
        dgvInfo = New DataGridView()
        Patient = New DataGridViewTextBoxColumn()
        DOB = New DataGridViewTextBoxColumn()
        Sex = New DataGridViewTextBoxColumn()
        Chart = New DataGridViewTextBoxColumn()
        Accession = New DataGridViewTextBoxColumn()
        Dated = New DataGridViewTextBoxColumn()
        ICD9s = New DataGridViewTextBoxColumn()
        dgvMIS = New DataGridView()
        ProviderID = New DataGridViewTextBoxColumn()
        PName = New DataGridViewTextBoxColumn()
        Print = New DataGridViewCheckBoxColumn()
        Fax = New DataGridViewCheckBoxColumn()
        FaxNo = New DataGridViewTextBoxColumn()
        Email = New DataGridViewCheckBoxColumn()
        EmailID = New DataGridViewTextBoxColumn()
        Report = New DataGridViewTextBoxColumn()
        Formula = New DataGridViewTextBoxColumn()
        btnFax = New Button()
        btnDeFax = New Button()
        btnEmail = New Button()
        btnDemail = New Button()
        btnSchedule = New Button()
        btnPSel = New Button()
        btnPDSel = New Button()
        ToolStrip1.SuspendLayout()
        GroupBox1.SuspendLayout()
        CType(dgvInfo, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvMIS, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(22, 22)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnPrint, ToolStripSeparator2, btnCancel, ToolStripSeparator1, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(1175, 34)
        ToolStrip1.TabIndex = 5
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnPrint
        ' 
        btnPrint.Enabled = False
        btnPrint.ForeColor = Color.DarkBlue
        btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), Image)
        btnPrint.ImageTransparentColor = Color.Magenta
        btnPrint.Name = "btnPrint"
        btnPrint.Size = New Size(132, 29)
        btnPrint.Text = "Print Report"
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
        btnCancel.Size = New Size(89, 29)
        btnCancel.Text = "Cancel"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
        ' 
        ' btnHelp
        ' 
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.ImageTransparentColor = Color.Magenta
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(75, 29)
        btnHelp.Text = "Help"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(dtpDateTo)
        GroupBox1.Controls.Add(lblClearDates)
        GroupBox1.Controls.Add(dtpDateFrom)
        GroupBox1.Controls.Add(chkTPAll)
        GroupBox1.Controls.Add(btnLoad)
        GroupBox1.Controls.Add(Label5)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(txtAccTo)
        GroupBox1.Controls.Add(txtAccFrom)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Location = New Point(23, 73)
        GroupBox1.Margin = New Padding(5, 6, 5, 6)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(5, 6, 5, 6)
        GroupBox1.Size = New Size(338, 331)
        GroupBox1.TabIndex = 6
        GroupBox1.TabStop = False
        GroupBox1.Text = "Inquiry Criteria"
        ' 
        ' dtpDateTo
        ' 
        dtpDateTo.CustomFormat = " "
        dtpDateTo.Format = DateTimePickerFormat.Custom
        dtpDateTo.Location = New Point(167, 67)
        dtpDateTo.Margin = New Padding(3, 4, 3, 4)
        dtpDateTo.Name = "dtpDateTo"
        dtpDateTo.Size = New Size(126, 31)
        dtpDateTo.TabIndex = 88
        ' 
        ' lblClearDates
        ' 
        lblClearDates.BackColor = SystemColors.Control
        lblClearDates.Image = My.Resources.Resources.Eraser
        lblClearDates.Location = New Point(297, 62)
        lblClearDates.Name = "lblClearDates"
        lblClearDates.Size = New Size(33, 46)
        lblClearDates.TabIndex = 90
        lblClearDates.Text = "      "
        ' 
        ' dtpDateFrom
        ' 
        dtpDateFrom.CustomFormat = " "
        dtpDateFrom.Format = DateTimePickerFormat.Custom
        dtpDateFrom.Location = New Point(10, 67)
        dtpDateFrom.Margin = New Padding(3, 4, 3, 4)
        dtpDateFrom.Name = "dtpDateFrom"
        dtpDateFrom.Size = New Size(126, 31)
        dtpDateFrom.TabIndex = 89
        ' 
        ' chkTPAll
        ' 
        chkTPAll.Appearance = Appearance.Button
        chkTPAll.Location = New Point(27, 269)
        chkTPAll.Margin = New Padding(5, 6, 5, 6)
        chkTPAll.Name = "chkTPAll"
        chkTPAll.Size = New Size(128, 46)
        chkTPAll.TabIndex = 13
        chkTPAll.Text = "3rd Parties"
        chkTPAll.TextAlign = ContentAlignment.MiddleCenter
        chkTPAll.UseVisualStyleBackColor = True
        ' 
        ' btnLoad
        ' 
        btnLoad.Image = CType(resources.GetObject("btnLoad.Image"), Image)
        btnLoad.Location = New Point(167, 265)
        btnLoad.Margin = New Padding(5, 6, 5, 6)
        btnLoad.Name = "btnLoad"
        btnLoad.Size = New Size(128, 54)
        btnLoad.TabIndex = 14
        btnLoad.UseVisualStyleBackColor = True
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.Magenta
        Label5.Location = New Point(172, 173)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(130, 25)
        Label5.TabIndex = 11
        Label5.Text = "To Accession"
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.Red
        Label4.Location = New Point(10, 173)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(152, 25)
        Label4.TabIndex = 10
        Label4.Text = "From Accession"
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.MediumBlue
        Label3.Location = New Point(27, 129)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(50, 25)
        Label3.TabIndex = 9
        Label3.Text = "OR"
        Label3.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtAccTo
        ' 
        txtAccTo.Location = New Point(167, 204)
        txtAccTo.Margin = New Padding(5, 6, 5, 6)
        txtAccTo.MaxLength = 12
        txtAccTo.Name = "txtAccTo"
        txtAccTo.Size = New Size(126, 31)
        txtAccTo.TabIndex = 4
        ' 
        ' txtAccFrom
        ' 
        txtAccFrom.Location = New Point(15, 204)
        txtAccFrom.Margin = New Padding(5, 6, 5, 6)
        txtAccFrom.MaxLength = 12
        txtAccFrom.Name = "txtAccFrom"
        txtAccFrom.Size = New Size(126, 31)
        txtAccFrom.TabIndex = 3
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.Magenta
        Label2.Location = New Point(162, 37)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(103, 25)
        Label2.TabIndex = 2
        Label2.Text = "To Date:"
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.Red
        Label1.Location = New Point(10, 37)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(98, 25)
        Label1.TabIndex = 0
        Label1.Text = "From Date:"
        Label1.TextAlign = ContentAlignment.TopRight
        ' 
        ' dgvInfo
        ' 
        dgvInfo.AllowUserToAddRows = False
        dgvInfo.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.Honeydew
        dgvInfo.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvInfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvInfo.Columns.AddRange(New DataGridViewColumn() {Patient, DOB, Sex, Chart, Accession, Dated, ICD9s})
        dgvInfo.Location = New Point(23, 435)
        dgvInfo.Margin = New Padding(5, 6, 5, 6)
        dgvInfo.Name = "dgvInfo"
        dgvInfo.RowHeadersVisible = False
        dgvInfo.RowHeadersWidth = 56
        dgvInfo.RowTemplate.DefaultCellStyle.BackColor = Color.Beige
        dgvInfo.Size = New Size(1132, 469)
        dgvInfo.TabIndex = 0
        ' 
        ' Patient
        ' 
        Patient.FillWeight = 120F
        Patient.HeaderText = "Patient"
        Patient.MinimumWidth = 7
        Patient.Name = "Patient"
        Patient.ReadOnly = True
        Patient.Width = 120
        ' 
        ' DOB
        ' 
        DOB.FillWeight = 80F
        DOB.HeaderText = "DOB"
        DOB.MinimumWidth = 7
        DOB.Name = "DOB"
        DOB.ReadOnly = True
        DOB.SortMode = DataGridViewColumnSortMode.NotSortable
        DOB.Width = 80
        ' 
        ' Sex
        ' 
        Sex.FillWeight = 40F
        Sex.HeaderText = "Sex"
        Sex.MaxInputLength = 1
        Sex.MinimumWidth = 7
        Sex.Name = "Sex"
        Sex.ReadOnly = True
        Sex.SortMode = DataGridViewColumnSortMode.NotSortable
        Sex.Width = 40
        ' 
        ' Chart
        ' 
        Chart.FillWeight = 80F
        Chart.HeaderText = "Chart"
        Chart.MinimumWidth = 7
        Chart.Name = "Chart"
        Chart.ReadOnly = True
        Chart.SortMode = DataGridViewColumnSortMode.NotSortable
        Chart.Width = 80
        ' 
        ' Accession
        ' 
        Accession.FillWeight = 80F
        Accession.HeaderText = "Accession"
        Accession.MinimumWidth = 7
        Accession.Name = "Accession"
        Accession.ReadOnly = True
        Accession.Width = 80
        ' 
        ' Dated
        ' 
        Dated.FillWeight = 60F
        Dated.HeaderText = "Dated"
        Dated.MinimumWidth = 7
        Dated.Name = "Dated"
        Dated.ReadOnly = True
        Dated.Width = 60
        ' 
        ' ICD9s
        ' 
        ICD9s.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        ICD9s.HeaderText = "ICD9 (... , ...)"
        ICD9s.MinimumWidth = 7
        ICD9s.Name = "ICD9s"
        ICD9s.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' dgvMIS
        ' 
        dgvMIS.AllowUserToAddRows = False
        dgvMIS.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = Color.LavenderBlush
        dgvMIS.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        dgvMIS.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvMIS.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvMIS.Columns.AddRange(New DataGridViewColumn() {ProviderID, PName, Print, Fax, FaxNo, Email, EmailID, Report, Formula})
        dgvMIS.Location = New Point(373, 73)
        dgvMIS.Margin = New Padding(5, 6, 5, 6)
        dgvMIS.Name = "dgvMIS"
        dgvMIS.RowHeadersVisible = False
        dgvMIS.RowHeadersWidth = 56
        dgvMIS.Size = New Size(783, 288)
        dgvMIS.TabIndex = 5
        ' 
        ' ProviderID
        ' 
        ProviderID.FillWeight = 70F
        ProviderID.HeaderText = "Prov ID"
        ProviderID.MinimumWidth = 7
        ProviderID.Name = "ProviderID"
        ProviderID.ReadOnly = True
        ' 
        ' PName
        ' 
        PName.FillWeight = 130F
        PName.HeaderText = "Name"
        PName.MinimumWidth = 7
        PName.Name = "PName"
        PName.ReadOnly = True
        ' 
        ' Print
        ' 
        Print.FillWeight = 40F
        Print.HeaderText = "Print"
        Print.MinimumWidth = 7
        Print.Name = "Print"
        ' 
        ' Fax
        ' 
        Fax.FillWeight = 30F
        Fax.HeaderText = "Fax"
        Fax.MinimumWidth = 7
        Fax.Name = "Fax"
        ' 
        ' FaxNo
        ' 
        FaxNo.FillWeight = 80F
        FaxNo.HeaderText = "No"
        FaxNo.MinimumWidth = 7
        FaxNo.Name = "FaxNo"
        FaxNo.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Email
        ' 
        Email.FillWeight = 40F
        Email.HeaderText = "Email"
        Email.MinimumWidth = 7
        Email.Name = "Email"
        ' 
        ' EmailID
        ' 
        EmailID.FillWeight = 84F
        EmailID.HeaderText = "ID"
        EmailID.MinimumWidth = 7
        EmailID.Name = "EmailID"
        EmailID.Resizable = DataGridViewTriState.True
        EmailID.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Report
        ' 
        Report.FillWeight = 20F
        Report.HeaderText = "Report"
        Report.MinimumWidth = 7
        Report.Name = "Report"
        Report.ReadOnly = True
        Report.SortMode = DataGridViewColumnSortMode.NotSortable
        Report.Visible = False
        ' 
        ' Formula
        ' 
        Formula.HeaderText = "Formula"
        Formula.MinimumWidth = 7
        Formula.Name = "Formula"
        Formula.ReadOnly = True
        Formula.SortMode = DataGridViewColumnSortMode.NotSortable
        Formula.Visible = False
        ' 
        ' btnFax
        ' 
        btnFax.Image = CType(resources.GetObject("btnFax.Image"), Image)
        btnFax.Location = New Point(593, 369)
        btnFax.Margin = New Padding(5, 6, 5, 6)
        btnFax.Name = "btnFax"
        btnFax.Size = New Size(102, 54)
        btnFax.TabIndex = 8
        btnFax.Text = "Fax"
        btnFax.TextAlign = ContentAlignment.MiddleRight
        btnFax.TextImageRelation = TextImageRelation.ImageBeforeText
        btnFax.UseVisualStyleBackColor = True
        ' 
        ' btnDeFax
        ' 
        btnDeFax.Image = CType(resources.GetObject("btnDeFax.Image"), Image)
        btnDeFax.Location = New Point(703, 369)
        btnDeFax.Margin = New Padding(5, 6, 5, 6)
        btnDeFax.Name = "btnDeFax"
        btnDeFax.Size = New Size(102, 54)
        btnDeFax.TabIndex = 9
        btnDeFax.Text = "Fax"
        btnDeFax.TextAlign = ContentAlignment.MiddleRight
        btnDeFax.TextImageRelation = TextImageRelation.ImageBeforeText
        btnDeFax.UseVisualStyleBackColor = True
        ' 
        ' btnEmail
        ' 
        btnEmail.Image = CType(resources.GetObject("btnEmail.Image"), Image)
        btnEmail.Location = New Point(813, 369)
        btnEmail.Margin = New Padding(5, 6, 5, 6)
        btnEmail.Name = "btnEmail"
        btnEmail.Size = New Size(102, 54)
        btnEmail.TabIndex = 10
        btnEmail.Text = "Email"
        btnEmail.TextAlign = ContentAlignment.MiddleRight
        btnEmail.TextImageRelation = TextImageRelation.ImageBeforeText
        btnEmail.UseVisualStyleBackColor = True
        ' 
        ' btnDemail
        ' 
        btnDemail.Image = CType(resources.GetObject("btnDemail.Image"), Image)
        btnDemail.Location = New Point(923, 369)
        btnDemail.Margin = New Padding(5, 6, 5, 6)
        btnDemail.Name = "btnDemail"
        btnDemail.Size = New Size(102, 54)
        btnDemail.TabIndex = 11
        btnDemail.Text = "Email"
        btnDemail.TextAlign = ContentAlignment.MiddleRight
        btnDemail.TextImageRelation = TextImageRelation.ImageBeforeText
        btnDemail.UseVisualStyleBackColor = True
        ' 
        ' btnSchedule
        ' 
        btnSchedule.Location = New Point(1033, 369)
        btnSchedule.Margin = New Padding(5, 6, 5, 6)
        btnSchedule.Name = "btnSchedule"
        btnSchedule.Size = New Size(125, 54)
        btnSchedule.TabIndex = 12
        btnSchedule.Text = "Schedule"
        btnSchedule.UseVisualStyleBackColor = True
        ' 
        ' btnPSel
        ' 
        btnPSel.Image = CType(resources.GetObject("btnPSel.Image"), Image)
        btnPSel.Location = New Point(373, 369)
        btnPSel.Margin = New Padding(5, 6, 5, 6)
        btnPSel.Name = "btnPSel"
        btnPSel.Size = New Size(102, 54)
        btnPSel.TabIndex = 6
        btnPSel.Text = "Print"
        btnPSel.TextAlign = ContentAlignment.MiddleRight
        btnPSel.TextImageRelation = TextImageRelation.ImageBeforeText
        btnPSel.UseVisualStyleBackColor = True
        ' 
        ' btnPDSel
        ' 
        btnPDSel.Image = CType(resources.GetObject("btnPDSel.Image"), Image)
        btnPDSel.Location = New Point(483, 369)
        btnPDSel.Margin = New Padding(5, 6, 5, 6)
        btnPDSel.Name = "btnPDSel"
        btnPDSel.Size = New Size(102, 54)
        btnPDSel.TabIndex = 7
        btnPDSel.Text = "Print"
        btnPDSel.TextAlign = ContentAlignment.MiddleRight
        btnPDSel.TextImageRelation = TextImageRelation.ImageBeforeText
        btnPDSel.UseVisualStyleBackColor = True
        ' 
        ' frmMissingInfo
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1175, 927)
        Controls.Add(btnPDSel)
        Controls.Add(btnPSel)
        Controls.Add(btnSchedule)
        Controls.Add(btnDemail)
        Controls.Add(btnEmail)
        Controls.Add(btnDeFax)
        Controls.Add(btnFax)
        Controls.Add(dgvMIS)
        Controls.Add(dgvInfo)
        Controls.Add(GroupBox1)
        Controls.Add(ToolStrip1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmMissingInfo"
        Text = "Missing Info Inquiry"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        CType(dgvInfo, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvMIS, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvInfo As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtAccFrom As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtAccTo As System.Windows.Forms.TextBox
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkTPAll As System.Windows.Forms.CheckBox
    Friend WithEvents Patient As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DOB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Sex As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Chart As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Accession As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Dated As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ICD9s As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvMIS As System.Windows.Forms.DataGridView
    Friend WithEvents btnFax As System.Windows.Forms.Button
    Friend WithEvents btnDeFax As System.Windows.Forms.Button
    Friend WithEvents btnEmail As System.Windows.Forms.Button
    Friend WithEvents btnDemail As System.Windows.Forms.Button
    Friend WithEvents btnSchedule As System.Windows.Forms.Button
    Friend WithEvents btnPSel As System.Windows.Forms.Button
    Friend WithEvents btnPDSel As System.Windows.Forms.Button
    Friend WithEvents ProviderID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Print As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Fax As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents FaxNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Email As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents EmailID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Report As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Formula As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dtpDateTo As DateTimePicker
    Friend WithEvents lblClearDates As Label
    Friend WithEvents dtpDateFrom As DateTimePicker
End Class
