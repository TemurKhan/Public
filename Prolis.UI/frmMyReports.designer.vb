<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMyReports
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMyReports))
        ToolStrip1 = New ToolStrip()
        btnPrint = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        dgvReports = New DataGridView()
        ID = New DataGridViewTextBoxColumn()
        RptName = New DataGridViewTextBoxColumn()
        RptFile = New DataGridViewTextBoxColumn()
        PrintDialog1 = New PrintDialog()
        Label1 = New Label()
        txtSearchRptName = New TextBox()
        btnSearch = New Button()
        cbxReportType = New ComboBox()
        Label2 = New Label()
        ToolStrip1.SuspendLayout()
        CType(dgvReports, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnPrint, ToolStripSeparator1, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(1010, 34)
        ToolStrip1.TabIndex = 9
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnPrint
        ' 
        btnPrint.Enabled = False
        btnPrint.ForeColor = Color.DarkBlue
        btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), Image)
        btnPrint.ImageTransparentColor = Color.Magenta
        btnPrint.Name = "btnPrint"
        btnPrint.Size = New Size(76, 29)
        btnPrint.Text = "Print"
        btnPrint.TextAlign = ContentAlignment.MiddleRight
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
        ' dgvReports
        ' 
        dgvReports.AllowUserToAddRows = False
        dgvReports.AllowUserToDeleteRows = False
        dgvReports.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvReports.Columns.AddRange(New DataGridViewColumn() {ID, RptName, RptFile})
        dgvReports.Location = New Point(20, 160)
        dgvReports.Margin = New Padding(5, 6, 5, 6)
        dgvReports.MultiSelect = False
        dgvReports.Name = "dgvReports"
        dgvReports.ReadOnly = True
        dgvReports.RowHeadersVisible = False
        dgvReports.RowHeadersWidth = 62
        dgvReports.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvReports.Size = New Size(970, 579)
        dgvReports.TabIndex = 3
        ' 
        ' ID
        ' 
        ID.FillWeight = 80.0F
        ID.HeaderText = "No"
        ID.MaxInputLength = 4
        ID.MinimumWidth = 8
        ID.Name = "ID"
        ID.ReadOnly = True
        ID.SortMode = DataGridViewColumnSortMode.NotSortable
        ID.Width = 80
        ' 
        ' RptName
        ' 
        RptName.FillWeight = 150.0F
        RptName.HeaderText = "Report"
        RptName.MinimumWidth = 8
        RptName.Name = "RptName"
        RptName.ReadOnly = True
        RptName.Width = 150
        ' 
        ' RptFile
        ' 
        RptFile.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        RptFile.FillWeight = 30.0F
        RptFile.HeaderText = "File"
        RptFile.MinimumWidth = 8
        RptFile.Name = "RptFile"
        RptFile.ReadOnly = True
        RptFile.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' PrintDialog1
        ' 
        PrintDialog1.UseEXDialog = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(482, 96)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(174, 25)
        Label1.TabIndex = 11
        Label1.Text = "Search Report Name"
        ' 
        ' txtSearchRptName
        ' 
        txtSearchRptName.Location = New Point(670, 88)
        txtSearchRptName.Margin = New Padding(5, 6, 5, 6)
        txtSearchRptName.Name = "txtSearchRptName"
        txtSearchRptName.Size = New Size(164, 31)
        txtSearchRptName.TabIndex = 1
        ' 
        ' btnSearch
        ' 
        btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), Image)
        btnSearch.Location = New Point(847, 87)
        btnSearch.Margin = New Padding(5, 6, 5, 6)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(38, 44)
        btnSearch.TabIndex = 2
        btnSearch.UseVisualStyleBackColor = True
        ' 
        ' cbxReportType
        ' 
        cbxReportType.DropDownStyle = ComboBoxStyle.DropDownList
        cbxReportType.FormattingEnabled = True
        cbxReportType.Location = New Point(247, 88)
        cbxReportType.Margin = New Padding(5, 6, 5, 6)
        cbxReportType.Name = "cbxReportType"
        cbxReportType.Size = New Size(199, 33)
        cbxReportType.TabIndex = 0
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(127, 96)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(107, 25)
        Label2.TabIndex = 11
        Label2.Text = "Report Type"
        ' 
        ' frmMyReports
        ' 
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1010, 762)
        Controls.Add(cbxReportType)
        Controls.Add(btnSearch)
        Controls.Add(txtSearchRptName)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(dgvReports)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmMyReports"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "My Reports"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgvReports, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents dgvReports As System.Windows.Forms.DataGridView
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RptName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RptFile As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSearchRptName As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents cbxReportType As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class
