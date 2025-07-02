<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProcessing
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProcessing))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        grpSearch = New GroupBox()
        lblClearDates = New Label()
        dtpDateTo = New DateTimePicker()
        txtAccTo = New TextBox()
        txtAccFrom = New TextBox()
        dtpDateFrom = New DateTimePicker()
        Label103 = New Label()
        Label104 = New Label()
        Label102 = New Label()
        lblTo = New Label()
        lblFrom = New Label()
        btnFirst = New Button()
        btnPrevious = New Button()
        btnNext = New Button()
        btnLast = New Button()
        txtNavStatus = New TextBox()
        btnLoad = New Button()
        txtTest = New TextBox()
        Label1 = New Label()
        dgvSlideIDs = New DataGridView()
        SlideID = New DataGridViewTextBoxColumn()
        IsNew = New DataGridViewCheckBoxColumn()
        txtAccID = New TextBox()
        Label2 = New Label()
        txtTestID = New TextBox()
        Label3 = New Label()
        ToolStrip1.SuspendLayout()
        grpSearch.SuspendLayout()
        CType(dgvSlideIDs, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(820, 34)
        ToolStrip1.TabIndex = 4
        ToolStrip1.Text = "ToolStrip1"
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
        ' grpSearch
        ' 
        grpSearch.Controls.Add(lblClearDates)
        grpSearch.Controls.Add(dtpDateTo)
        grpSearch.Controls.Add(txtAccTo)
        grpSearch.Controls.Add(txtAccFrom)
        grpSearch.Controls.Add(dtpDateFrom)
        grpSearch.Controls.Add(Label103)
        grpSearch.Controls.Add(Label104)
        grpSearch.Controls.Add(Label102)
        grpSearch.Controls.Add(lblTo)
        grpSearch.Controls.Add(lblFrom)
        grpSearch.Location = New Point(20, 83)
        grpSearch.Margin = New Padding(5, 6, 5, 6)
        grpSearch.Name = "grpSearch"
        grpSearch.Padding = New Padding(5, 6, 5, 6)
        grpSearch.Size = New Size(252, 463)
        grpSearch.TabIndex = 5
        grpSearch.TabStop = False
        ' 
        ' lblClearDates
        ' 
        lblClearDates.BackColor = SystemColors.Control
        lblClearDates.Image = My.Resources.Resources.Eraser
        lblClearDates.Location = New Point(195, 152)
        lblClearDates.Name = "lblClearDates"
        lblClearDates.Size = New Size(33, 46)
        lblClearDates.TabIndex = 96
        lblClearDates.Text = "      "
        ' 
        ' dtpDateTo
        ' 
        dtpDateTo.CustomFormat = " "
        dtpDateTo.Format = DateTimePickerFormat.Custom
        dtpDateTo.Location = New Point(22, 160)
        dtpDateTo.Margin = New Padding(3, 4, 3, 4)
        dtpDateTo.Name = "dtpDateTo"
        dtpDateTo.Size = New Size(164, 31)
        dtpDateTo.TabIndex = 94
        ' 
        ' txtAccTo
        ' 
        txtAccTo.Location = New Point(22, 383)
        txtAccTo.Margin = New Padding(5, 6, 5, 6)
        txtAccTo.MaxLength = 12
        txtAccTo.Name = "txtAccTo"
        txtAccTo.Size = New Size(164, 31)
        txtAccTo.TabIndex = 4
        txtAccTo.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtAccFrom
        ' 
        txtAccFrom.Location = New Point(22, 290)
        txtAccFrom.Margin = New Padding(5, 6, 5, 6)
        txtAccFrom.MaxLength = 12
        txtAccFrom.Name = "txtAccFrom"
        txtAccFrom.Size = New Size(164, 31)
        txtAccFrom.TabIndex = 3
        txtAccFrom.TextAlign = HorizontalAlignment.Center
        ' 
        ' dtpDateFrom
        ' 
        dtpDateFrom.CustomFormat = " "
        dtpDateFrom.Format = DateTimePickerFormat.Custom
        dtpDateFrom.Location = New Point(22, 54)
        dtpDateFrom.Margin = New Padding(3, 4, 3, 4)
        dtpDateFrom.Name = "dtpDateFrom"
        dtpDateFrom.Size = New Size(164, 31)
        dtpDateFrom.TabIndex = 95
        ' 
        ' Label103
        ' 
        Label103.ForeColor = Color.Magenta
        Label103.Location = New Point(23, 352)
        Label103.Margin = New Padding(5, 0, 5, 0)
        Label103.Name = "Label103"
        Label103.Size = New Size(108, 25)
        Label103.TabIndex = 8
        Label103.Text = "Acc To"
        ' 
        ' Label104
        ' 
        Label104.ForeColor = Color.Red
        Label104.Location = New Point(23, 260)
        Label104.Margin = New Padding(5, 0, 5, 0)
        Label104.Name = "Label104"
        Label104.Size = New Size(135, 25)
        Label104.TabIndex = 7
        Label104.Text = "Acc From"
        Label104.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label102
        ' 
        Label102.ForeColor = Color.DarkBlue
        Label102.Location = New Point(22, 206)
        Label102.Margin = New Padding(5, 0, 5, 0)
        Label102.Name = "Label102"
        Label102.Size = New Size(52, 54)
        Label102.TabIndex = 6
        Label102.Text = "OR"
        Label102.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblTo
        ' 
        lblTo.ForeColor = Color.Magenta
        lblTo.Location = New Point(23, 127)
        lblTo.Margin = New Padding(5, 0, 5, 0)
        lblTo.Name = "lblTo"
        lblTo.Size = New Size(218, 25)
        lblTo.TabIndex = 4
        lblTo.Text = "Date To"
        ' 
        ' lblFrom
        ' 
        lblFrom.ForeColor = Color.Red
        lblFrom.Location = New Point(23, 15)
        lblFrom.Margin = New Padding(5, 0, 5, 0)
        lblFrom.Name = "lblFrom"
        lblFrom.Size = New Size(225, 25)
        lblFrom.TabIndex = 3
        lblFrom.Text = "Date From"
        lblFrom.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' btnFirst
        ' 
        btnFirst.Enabled = False
        btnFirst.Image = CType(resources.GetObject("btnFirst.Image"), Image)
        btnFirst.Location = New Point(357, 125)
        btnFirst.Margin = New Padding(5, 6, 5, 6)
        btnFirst.Name = "btnFirst"
        btnFirst.Size = New Size(50, 50)
        btnFirst.TabIndex = 9
        btnFirst.TabStop = False
        btnFirst.UseVisualStyleBackColor = True
        ' 
        ' btnPrevious
        ' 
        btnPrevious.Enabled = False
        btnPrevious.Image = CType(resources.GetObject("btnPrevious.Image"), Image)
        btnPrevious.Location = New Point(417, 125)
        btnPrevious.Margin = New Padding(5, 6, 5, 6)
        btnPrevious.Name = "btnPrevious"
        btnPrevious.Size = New Size(48, 50)
        btnPrevious.TabIndex = 8
        btnPrevious.TabStop = False
        btnPrevious.UseVisualStyleBackColor = True
        ' 
        ' btnNext
        ' 
        btnNext.Enabled = False
        btnNext.Image = CType(resources.GetObject("btnNext.Image"), Image)
        btnNext.Location = New Point(672, 125)
        btnNext.Margin = New Padding(5, 6, 5, 6)
        btnNext.Name = "btnNext"
        btnNext.Size = New Size(53, 50)
        btnNext.TabIndex = 6
        btnNext.TabStop = False
        btnNext.UseVisualStyleBackColor = True
        ' 
        ' btnLast
        ' 
        btnLast.Enabled = False
        btnLast.Image = CType(resources.GetObject("btnLast.Image"), Image)
        btnLast.Location = New Point(735, 125)
        btnLast.Margin = New Padding(5, 6, 5, 6)
        btnLast.Name = "btnLast"
        btnLast.Size = New Size(55, 50)
        btnLast.TabIndex = 7
        btnLast.TabStop = False
        btnLast.UseVisualStyleBackColor = True
        ' 
        ' txtNavStatus
        ' 
        txtNavStatus.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtNavStatus.Location = New Point(475, 133)
        txtNavStatus.Margin = New Padding(5, 6, 5, 6)
        txtNavStatus.Name = "txtNavStatus"
        txtNavStatus.ReadOnly = True
        txtNavStatus.Size = New Size(184, 26)
        txtNavStatus.TabIndex = 10
        txtNavStatus.TabStop = False
        txtNavStatus.TextAlign = HorizontalAlignment.Center
        ' 
        ' btnLoad
        ' 
        btnLoad.Image = CType(resources.GetObject("btnLoad.Image"), Image)
        btnLoad.Location = New Point(282, 108)
        btnLoad.Margin = New Padding(5, 6, 5, 6)
        btnLoad.Name = "btnLoad"
        btnLoad.Size = New Size(62, 67)
        btnLoad.TabIndex = 5
        btnLoad.UseVisualStyleBackColor = True
        ' 
        ' txtTest
        ' 
        txtTest.BackColor = Color.White
        txtTest.Location = New Point(282, 248)
        txtTest.Margin = New Padding(5, 6, 5, 6)
        txtTest.Name = "txtTest"
        txtTest.ReadOnly = True
        txtTest.Size = New Size(506, 31)
        txtTest.TabIndex = 11
        ' 
        ' Label1
        ' 
        Label1.Location = New Point(282, 213)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(167, 29)
        Label1.TabIndex = 12
        Label1.Text = "Component Name"
        ' 
        ' dgvSlideIDs
        ' 
        dgvSlideIDs.AllowUserToAddRows = False
        dgvSlideIDs.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.Beige
        dgvSlideIDs.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvSlideIDs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvSlideIDs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvSlideIDs.Columns.AddRange(New DataGridViewColumn() {SlideID, IsNew})
        dgvSlideIDs.Location = New Point(480, 308)
        dgvSlideIDs.Margin = New Padding(5, 6, 5, 6)
        dgvSlideIDs.Name = "dgvSlideIDs"
        dgvSlideIDs.RowHeadersVisible = False
        dgvSlideIDs.RowHeadersWidth = 62
        dgvSlideIDs.ScrollBars = ScrollBars.Vertical
        dgvSlideIDs.Size = New Size(310, 242)
        dgvSlideIDs.TabIndex = 14
        ' 
        ' SlideID
        ' 
        SlideID.FillWeight = 130F
        SlideID.HeaderText = "Slide IDs"
        SlideID.MaxInputLength = 10
        SlideID.MinimumWidth = 8
        SlideID.Name = "SlideID"
        SlideID.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' IsNew
        ' 
        IsNew.FillWeight = 36F
        IsNew.HeaderText = "New"
        IsNew.MinimumWidth = 8
        IsNew.Name = "IsNew"
        IsNew.ReadOnly = True
        ' 
        ' txtAccID
        ' 
        txtAccID.Location = New Point(287, 352)
        txtAccID.Margin = New Padding(5, 6, 5, 6)
        txtAccID.MaxLength = 12
        txtAccID.Name = "txtAccID"
        txtAccID.Size = New Size(176, 31)
        txtAccID.TabIndex = 15
        txtAccID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label2
        ' 
        Label2.Location = New Point(287, 317)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(178, 29)
        Label2.TabIndex = 16
        Label2.Text = "Displayed Accession"
        ' 
        ' txtTestID
        ' 
        txtTestID.Location = New Point(292, 469)
        txtTestID.Margin = New Padding(5, 6, 5, 6)
        txtTestID.MaxLength = 12
        txtTestID.Name = "txtTestID"
        txtTestID.Size = New Size(176, 31)
        txtTestID.TabIndex = 17
        txtTestID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(292, 435)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(178, 29)
        Label3.TabIndex = 18
        Label3.Text = "Displayed Test ID"
        ' 
        ' frmProcessing
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        AutoSize = True
        ClientSize = New Size(820, 592)
        Controls.Add(Label3)
        Controls.Add(txtTestID)
        Controls.Add(Label2)
        Controls.Add(txtAccID)
        Controls.Add(dgvSlideIDs)
        Controls.Add(Label1)
        Controls.Add(txtTest)
        Controls.Add(btnFirst)
        Controls.Add(grpSearch)
        Controls.Add(btnPrevious)
        Controls.Add(ToolStrip1)
        Controls.Add(btnNext)
        Controls.Add(btnLast)
        Controls.Add(btnLoad)
        Controls.Add(txtNavStatus)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmProcessing"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Processing"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        grpSearch.ResumeLayout(False)
        grpSearch.PerformLayout()
        CType(dgvSlideIDs, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents grpSearch As System.Windows.Forms.GroupBox
    Friend WithEvents txtAccTo As System.Windows.Forms.TextBox
    Friend WithEvents txtAccFrom As System.Windows.Forms.TextBox
    Friend WithEvents Label103 As System.Windows.Forms.Label
    Friend WithEvents Label104 As System.Windows.Forms.Label
    Friend WithEvents Label102 As System.Windows.Forms.Label
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents btnFirst As System.Windows.Forms.Button
    Friend WithEvents btnPrevious As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnLast As System.Windows.Forms.Button
    Friend WithEvents txtNavStatus As System.Windows.Forms.TextBox
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents txtTest As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvSlideIDs As System.Windows.Forms.DataGridView
    Friend WithEvents txtAccID As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtTestID As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents SlideID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IsNew As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents lblClearDates As Label
    Friend WithEvents dtpDateTo As DateTimePicker
    Friend WithEvents dtpDateFrom As DateTimePicker
End Class
