<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmResultDash
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmResultDash))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Label10 = New Label()
        txtTerm = New TextBox()
        txtTotal = New TextBox()
        lblTerm = New Label()
        Label9 = New Label()
        dtpTo = New DateTimePicker()
        txtOutSrcd = New TextBox()
        cmbCriteria = New ComboBox()
        Label8 = New Label()
        dtpFrom = New DateTimePicker()
        txtFinals = New TextBox()
        Label7 = New Label()
        btnGo = New Button()
        txtPartials = New TextBox()
        Label6 = New Label()
        Label3 = New Label()
        txtInitials = New TextBox()
        LabelP = New Label()
        lblTo = New Label()
        txtAttReqs = New TextBox()
        lblFrom = New Label()
        txtFalseFinals = New TextBox()
        Label1 = New Label()
        dgvAccessions = New DataGridView()
        AccID = New DataGridViewTextBoxColumn()
        Received = New DataGridViewTextBoxColumn()
        Status = New DataGridViewTextBoxColumn()
        Age = New DataGridViewTextBoxColumn()
        dgvTests = New DataGridView()
        TestID = New DataGridViewTextBoxColumn()
        TestName = New DataGridViewTextBoxColumn()
        Result = New DataGridViewTextBoxColumn()
        Flag = New DataGridViewTextBoxColumn()
        TestType = New DataGridViewTextBoxColumn()
        AnalyteAge = New DataGridViewTextBoxColumn()
        Label2 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        txtComment = New TextBox()
        Label11 = New Label()
        StatusStrip1 = New StatusStrip()
        lblStatus = New ToolStripStatusLabel()
        txtOverAged = New TextBox()
        Label12 = New Label()
        btnPrint = New Button()
        clipmsg = New Label()
        Panel1 = New Panel()
        CType(dgvAccessions, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvTests, ComponentModel.ISupportInitialize).BeginInit()
        StatusStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label10
        ' 
        Label10.ForeColor = Color.DarkBlue
        Label10.Location = New Point(1066, 125)
        Label10.Margin = New Padding(5, 0, 5, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(241, 28)
        Label10.TabIndex = 61
        Label10.Text = "Total"
        Label10.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtTerm
        ' 
        txtTerm.Location = New Point(796, 56)
        txtTerm.Margin = New Padding(5, 6, 5, 6)
        txtTerm.Name = "txtTerm"
        txtTerm.Size = New Size(398, 31)
        txtTerm.TabIndex = 47
        ' 
        ' txtTotal
        ' 
        txtTotal.Location = New Point(1066, 159)
        txtTotal.Margin = New Padding(5, 6, 5, 6)
        txtTotal.Name = "txtTotal"
        txtTotal.ReadOnly = True
        txtTotal.Size = New Size(239, 31)
        txtTotal.TabIndex = 60
        txtTotal.TextAlign = HorizontalAlignment.Center
        ' 
        ' lblTerm
        ' 
        lblTerm.ForeColor = Color.DarkBlue
        lblTerm.Location = New Point(804, 22)
        lblTerm.Margin = New Padding(5, 0, 5, 0)
        lblTerm.Name = "lblTerm"
        lblTerm.Size = New Size(175, 28)
        lblTerm.TabIndex = 48
        lblTerm.Text = "Search Term"
        ' 
        ' Label9
        ' 
        Label9.ForeColor = Color.DarkBlue
        Label9.Location = New Point(921, 125)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(135, 28)
        Label9.TabIndex = 59
        Label9.Text = "Outsourced"
        Label9.TextAlign = ContentAlignment.TopCenter
        ' 
        ' dtpTo
        ' 
        dtpTo.Format = DateTimePickerFormat.Short
        dtpTo.Location = New Point(260, 53)
        dtpTo.Margin = New Padding(5, 6, 5, 6)
        dtpTo.Name = "dtpTo"
        dtpTo.Size = New Size(194, 31)
        dtpTo.TabIndex = 41
        ' 
        ' txtOutSrcd
        ' 
        txtOutSrcd.Location = New Point(921, 159)
        txtOutSrcd.Margin = New Padding(5, 6, 5, 6)
        txtOutSrcd.Name = "txtOutSrcd"
        txtOutSrcd.ReadOnly = True
        txtOutSrcd.Size = New Size(133, 31)
        txtOutSrcd.TabIndex = 58
        txtOutSrcd.TextAlign = HorizontalAlignment.Center
        ' 
        ' cmbCriteria
        ' 
        cmbCriteria.FormattingEnabled = True
        cmbCriteria.Items.AddRange(New Object() {"Accession ID", "Att Requiring", "False Finals", "Finals", "Partials", "Outsourced", "Over Aged", "Un-Resulted"})
        cmbCriteria.Location = New Point(484, 53)
        cmbCriteria.Margin = New Padding(5, 6, 5, 6)
        cmbCriteria.Name = "cmbCriteria"
        cmbCriteria.Size = New Size(280, 33)
        cmbCriteria.TabIndex = 45
        ' 
        ' Label8
        ' 
        Label8.ForeColor = Color.DarkBlue
        Label8.Location = New Point(631, 125)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(135, 28)
        Label8.TabIndex = 57
        Label8.Text = "Finals"
        Label8.TextAlign = ContentAlignment.TopCenter
        ' 
        ' dtpFrom
        ' 
        dtpFrom.Format = DateTimePickerFormat.Short
        dtpFrom.Location = New Point(21, 56)
        dtpFrom.Margin = New Padding(5, 6, 5, 6)
        dtpFrom.Name = "dtpFrom"
        dtpFrom.Size = New Size(205, 31)
        dtpFrom.TabIndex = 40
        ' 
        ' txtFinals
        ' 
        txtFinals.Location = New Point(631, 159)
        txtFinals.Margin = New Padding(5, 6, 5, 6)
        txtFinals.Name = "txtFinals"
        txtFinals.ReadOnly = True
        txtFinals.Size = New Size(133, 31)
        txtFinals.TabIndex = 56
        txtFinals.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.DarkBlue
        Label7.Location = New Point(339, 125)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(135, 28)
        Label7.TabIndex = 55
        Label7.Text = "Partials"
        Label7.TextAlign = ContentAlignment.TopCenter
        ' 
        ' btnGo
        ' 
        btnGo.Image = CType(resources.GetObject("btnGo.Image"), Image)
        btnGo.Location = New Point(1206, 48)
        btnGo.Margin = New Padding(5, 6, 5, 6)
        btnGo.Name = "btnGo"
        btnGo.Size = New Size(101, 48)
        btnGo.TabIndex = 43
        btnGo.UseVisualStyleBackColor = True
        ' 
        ' txtPartials
        ' 
        txtPartials.Location = New Point(339, 159)
        txtPartials.Margin = New Padding(5, 6, 5, 6)
        txtPartials.Name = "txtPartials"
        txtPartials.ReadOnly = True
        txtPartials.Size = New Size(133, 31)
        txtPartials.TabIndex = 54
        txtPartials.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.DarkBlue
        Label6.Location = New Point(21, 125)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(156, 28)
        Label6.TabIndex = 53
        Label6.Text = "Unresulted"
        Label6.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(500, 22)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(206, 28)
        Label3.TabIndex = 46
        Label3.Text = "Criteria"
        ' 
        ' txtInitials
        ' 
        txtInitials.Location = New Point(21, 159)
        txtInitials.Margin = New Padding(5, 6, 5, 6)
        txtInitials.Name = "txtInitials"
        txtInitials.ReadOnly = True
        txtInitials.Size = New Size(154, 31)
        txtInitials.TabIndex = 52
        txtInitials.TextAlign = HorizontalAlignment.Center
        ' 
        ' LabelP
        ' 
        LabelP.ForeColor = Color.DarkBlue
        LabelP.Location = New Point(484, 125)
        LabelP.Margin = New Padding(5, 0, 5, 0)
        LabelP.Name = "LabelP"
        LabelP.Size = New Size(135, 28)
        LabelP.TabIndex = 51
        LabelP.Text = "Att Requiring"
        LabelP.TextAlign = ContentAlignment.TopCenter
        ' 
        ' lblTo
        ' 
        lblTo.ForeColor = Color.Magenta
        lblTo.Location = New Point(275, 22)
        lblTo.Margin = New Padding(5, 0, 5, 0)
        lblTo.Name = "lblTo"
        lblTo.Size = New Size(200, 28)
        lblTo.TabIndex = 42
        lblTo.Text = "To"
        ' 
        ' txtAttReqs
        ' 
        txtAttReqs.Location = New Point(484, 159)
        txtAttReqs.Margin = New Padding(5, 6, 5, 6)
        txtAttReqs.Name = "txtAttReqs"
        txtAttReqs.ReadOnly = True
        txtAttReqs.Size = New Size(133, 31)
        txtAttReqs.TabIndex = 50
        txtAttReqs.TextAlign = HorizontalAlignment.Center
        ' 
        ' lblFrom
        ' 
        lblFrom.ForeColor = Color.Red
        lblFrom.Location = New Point(35, 22)
        lblFrom.Margin = New Padding(5, 0, 5, 0)
        lblFrom.Name = "lblFrom"
        lblFrom.Size = New Size(195, 28)
        lblFrom.TabIndex = 39
        lblFrom.Text = "From"
        ' 
        ' txtFalseFinals
        ' 
        txtFalseFinals.Location = New Point(776, 159)
        txtFalseFinals.Margin = New Padding(5, 6, 5, 6)
        txtFalseFinals.Name = "txtFalseFinals"
        txtFalseFinals.ReadOnly = True
        txtFalseFinals.Size = New Size(133, 31)
        txtFalseFinals.TabIndex = 62
        txtFalseFinals.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(776, 125)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(135, 28)
        Label1.TabIndex = 63
        Label1.Text = "False Finals"
        Label1.TextAlign = ContentAlignment.TopCenter
        ' 
        ' dgvAccessions
        ' 
        dgvAccessions.AllowUserToAddRows = False
        dgvAccessions.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.SeaShell
        dgvAccessions.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvAccessions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvAccessions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvAccessions.Columns.AddRange(New DataGridViewColumn() {AccID, Received, Status, Age})
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = Color.Lavender
        DataGridViewCellStyle2.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle2.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False
        dgvAccessions.DefaultCellStyle = DataGridViewCellStyle2
        dgvAccessions.Location = New Point(25, 258)
        dgvAccessions.Margin = New Padding(5, 6, 5, 6)
        dgvAccessions.MultiSelect = False
        dgvAccessions.Name = "dgvAccessions"
        dgvAccessions.ReadOnly = True
        dgvAccessions.RowHeadersVisible = False
        dgvAccessions.RowHeadersWidth = 62
        dgvAccessions.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvAccessions.Size = New Size(560, 722)
        dgvAccessions.TabIndex = 64
        ' 
        ' AccID
        ' 
        AccID.FillWeight = 90F
        AccID.HeaderText = "Acc ID"
        AccID.MinimumWidth = 8
        AccID.Name = "AccID"
        AccID.ReadOnly = True
        ' 
        ' Received
        ' 
        Received.FillWeight = 80F
        Received.HeaderText = "Received"
        Received.MinimumWidth = 8
        Received.Name = "Received"
        Received.ReadOnly = True
        ' 
        ' Status
        ' 
        Status.FillWeight = 80F
        Status.HeaderText = "Status"
        Status.MinimumWidth = 8
        Status.Name = "Status"
        Status.ReadOnly = True
        ' 
        ' Age
        ' 
        Age.FillWeight = 60F
        Age.HeaderText = "Age"
        Age.MinimumWidth = 8
        Age.Name = "Age"
        Age.ReadOnly = True
        ' 
        ' dgvTests
        ' 
        dgvTests.AllowUserToAddRows = False
        dgvTests.AllowUserToDeleteRows = False
        DataGridViewCellStyle3.BackColor = Color.LightCyan
        dgvTests.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        dgvTests.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvTests.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvTests.Columns.AddRange(New DataGridViewColumn() {TestID, TestName, Result, Flag, TestType, AnalyteAge})
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = Color.Lavender
        DataGridViewCellStyle4.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle4.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = DataGridViewTriState.False
        dgvTests.DefaultCellStyle = DataGridViewCellStyle4
        dgvTests.Location = New Point(616, 258)
        dgvTests.Margin = New Padding(5, 6, 5, 6)
        dgvTests.MultiSelect = False
        dgvTests.Name = "dgvTests"
        dgvTests.ReadOnly = True
        dgvTests.RowHeadersVisible = False
        dgvTests.RowHeadersWidth = 62
        dgvTests.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvTests.Size = New Size(691, 581)
        dgvTests.TabIndex = 65
        ' 
        ' TestID
        ' 
        TestID.FillWeight = 70F
        TestID.HeaderText = "Test ID"
        TestID.MinimumWidth = 8
        TestID.Name = "TestID"
        TestID.ReadOnly = True
        ' 
        ' TestName
        ' 
        TestName.FillWeight = 120F
        TestName.HeaderText = "Name"
        TestName.MinimumWidth = 8
        TestName.Name = "TestName"
        TestName.ReadOnly = True
        ' 
        ' Result
        ' 
        Result.FillWeight = 50F
        Result.HeaderText = "Result"
        Result.MinimumWidth = 8
        Result.Name = "Result"
        Result.ReadOnly = True
        Result.Resizable = DataGridViewTriState.True
        ' 
        ' Flag
        ' 
        Flag.FillWeight = 30F
        Flag.HeaderText = "Flag"
        Flag.MinimumWidth = 8
        Flag.Name = "Flag"
        Flag.ReadOnly = True
        Flag.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' TestType
        ' 
        TestType.FillWeight = 60F
        TestType.HeaderText = "Type"
        TestType.MinimumWidth = 8
        TestType.Name = "TestType"
        TestType.ReadOnly = True
        TestType.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' AnalyteAge
        ' 
        AnalyteAge.FillWeight = 60F
        AnalyteAge.HeaderText = "Age"
        AnalyteAge.MinimumWidth = 8
        AnalyteAge.Name = "AnalyteAge"
        AnalyteAge.ReadOnly = True
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(35, 223)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(156, 28)
        Label2.TabIndex = 66
        Label2.Text = "Accessions"
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(631, 223)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(156, 28)
        Label4.TabIndex = 67
        Label4.Text = "Analytes"
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.Brown
        Label5.Location = New Point(616, 964)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(691, 77)
        Label5.TabIndex = 68
        Label5.Text = "Age represents the resulting duration period in D = days, H = Hours and M = Minutes" & vbCrLf & "Over Aged represent the unresulted over the average " & vbCrLf & "resulting time of the same test (takes up to 3 minutes)"
        Label5.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtComment
        ' 
        txtComment.Location = New Point(616, 875)
        txtComment.Margin = New Padding(5, 6, 5, 6)
        txtComment.Multiline = True
        txtComment.Name = "txtComment"
        txtComment.ReadOnly = True
        txtComment.Size = New Size(689, 79)
        txtComment.TabIndex = 69
        txtComment.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label11
        ' 
        Label11.Font = New Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label11.ForeColor = Color.Crimson
        Label11.Location = New Point(729, 200)
        Label11.Margin = New Padding(5, 0, 5, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(421, 48)
        Label11.TabIndex = 70
        Label11.Text = "Under Development ! Do not use"
        Label11.Visible = False
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.ImageScalingSize = New Size(20, 20)
        StatusStrip1.Items.AddRange(New ToolStripItem() {lblStatus})
        StatusStrip1.Location = New Point(0, 1176)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Padding = New Padding(1, 0, 24, 0)
        StatusStrip1.Size = New Size(1339, 22)
        StatusStrip1.TabIndex = 71
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = False
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(120, 15)
        ' 
        ' txtOverAged
        ' 
        txtOverAged.Location = New Point(189, 159)
        txtOverAged.Margin = New Padding(5, 6, 5, 6)
        txtOverAged.Name = "txtOverAged"
        txtOverAged.ReadOnly = True
        txtOverAged.Size = New Size(133, 31)
        txtOverAged.TabIndex = 72
        txtOverAged.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label12
        ' 
        Label12.ForeColor = Color.Red
        Label12.Location = New Point(189, 125)
        Label12.Margin = New Padding(5, 0, 5, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(135, 28)
        Label12.TabIndex = 73
        Label12.Text = "Over Aged"
        Label12.TextAlign = ContentAlignment.TopCenter
        ' 
        ' btnPrint
        ' 
        btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), Image)
        btnPrint.Location = New Point(868, 1089)
        btnPrint.Margin = New Padding(5, 6, 5, 6)
        btnPrint.Name = "btnPrint"
        btnPrint.Size = New Size(169, 58)
        btnPrint.TabIndex = 74
        btnPrint.Text = "Print"
        btnPrint.TextAlign = ContentAlignment.MiddleRight
        btnPrint.TextImageRelation = TextImageRelation.ImageBeforeText
        btnPrint.UseVisualStyleBackColor = True
        ' 
        ' clipmsg
        ' 
        clipmsg.AutoSize = True
        clipmsg.Location = New Point(154, 225)
        clipmsg.Margin = New Padding(4, 0, 4, 0)
        clipmsg.Name = "clipmsg"
        clipmsg.Size = New Size(0, 25)
        clipmsg.TabIndex = 75
        ' 
        ' Panel1
        ' 
        Panel1.Location = New Point(21, 991)
        Panel1.Margin = New Padding(4, 5, 4, 5)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(564, 156)
        Panel1.TabIndex = 76
        ' 
        ' frmResultDash
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1339, 1198)
        Controls.Add(Panel1)
        Controls.Add(clipmsg)
        Controls.Add(btnPrint)
        Controls.Add(Label12)
        Controls.Add(txtOverAged)
        Controls.Add(StatusStrip1)
        Controls.Add(Label11)
        Controls.Add(txtComment)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(Label2)
        Controls.Add(dgvTests)
        Controls.Add(dgvAccessions)
        Controls.Add(Label1)
        Controls.Add(txtFalseFinals)
        Controls.Add(Label10)
        Controls.Add(txtTerm)
        Controls.Add(txtTotal)
        Controls.Add(lblTerm)
        Controls.Add(Label9)
        Controls.Add(dtpTo)
        Controls.Add(txtOutSrcd)
        Controls.Add(cmbCriteria)
        Controls.Add(Label8)
        Controls.Add(dtpFrom)
        Controls.Add(txtFinals)
        Controls.Add(Label7)
        Controls.Add(btnGo)
        Controls.Add(txtPartials)
        Controls.Add(Label6)
        Controls.Add(Label3)
        Controls.Add(txtInitials)
        Controls.Add(LabelP)
        Controls.Add(lblTo)
        Controls.Add(txtAttReqs)
        Controls.Add(lblFrom)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmResultDash"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Resulting Dashboard"
        CType(dgvAccessions, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvTests, ComponentModel.ISupportInitialize).EndInit()
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtTerm As System.Windows.Forms.TextBox
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents lblTerm As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtOutSrcd As System.Windows.Forms.TextBox
    Friend WithEvents cmbCriteria As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtFinals As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnGo As System.Windows.Forms.Button
    Friend WithEvents txtPartials As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtInitials As System.Windows.Forms.TextBox
    Friend WithEvents LabelP As System.Windows.Forms.Label
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents txtAttReqs As System.Windows.Forms.TextBox
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents txtFalseFinals As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvAccessions As System.Windows.Forms.DataGridView
    Friend WithEvents dgvTests As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents txtOverAged As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents AccID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Received As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Age As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TestID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TestName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Result As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Flag As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TestType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AnalyteAge As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents clipmsg As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel

End Class
