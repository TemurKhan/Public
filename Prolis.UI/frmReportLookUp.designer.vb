<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReportLookUp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReportLookUp))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        btnAccept = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        GroupBox1 = New GroupBox()
        txtTerm = New TextBox()
        Label2 = New Label()
        cmbTerm = New ComboBox()
        btnSearch = New Button()
        Label1 = New Label()
        dgvReports = New DataGridView()
        ReportID = New DataGridViewTextBoxColumn()
        RptName = New DataGridViewTextBoxColumn()
        ToolStrip1.SuspendLayout()
        GroupBox1.SuspendLayout()
        CType(dgvReports, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnAccept, ToolStripSeparator1, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(880, 34)
        ToolStrip1.TabIndex = 5
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnAccept
        ' 
        btnAccept.Enabled = False
        btnAccept.Image = CType(resources.GetObject("btnAccept.Image"), Image)
        btnAccept.ImageTransparentColor = Color.Magenta
        btnAccept.Name = "btnAccept"
        btnAccept.Size = New Size(94, 29)
        btnAccept.Text = "Accept"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
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
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(txtTerm)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(cmbTerm)
        GroupBox1.Controls.Add(btnSearch)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Location = New Point(20, 54)
        GroupBox1.Margin = New Padding(5, 6, 5, 6)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(5, 6, 5, 6)
        GroupBox1.Size = New Size(838, 131)
        GroupBox1.TabIndex = 27
        GroupBox1.TabStop = False
        GroupBox1.Text = "Search"
        ' 
        ' txtTerm
        ' 
        txtTerm.Location = New Point(23, 63)
        txtTerm.Margin = New Padding(5, 6, 5, 6)
        txtTerm.MaxLength = 100
        txtTerm.Name = "txtTerm"
        txtTerm.Size = New Size(446, 31)
        txtTerm.TabIndex = 6
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(482, 31)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(242, 25)
        Label2.TabIndex = 4
        Label2.Text = "Term Type"
        ' 
        ' cmbTerm
        ' 
        cmbTerm.FormattingEnabled = True
        cmbTerm.Items.AddRange(New Object() {"Report ID (Complete)", "Report Name (Even Partial)"})
        cmbTerm.Location = New Point(482, 63)
        cmbTerm.Margin = New Padding(5, 6, 5, 6)
        cmbTerm.Name = "cmbTerm"
        cmbTerm.Size = New Size(262, 33)
        cmbTerm.TabIndex = 2
        ' 
        ' btnSearch
        ' 
        btnSearch.Enabled = False
        btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), Image)
        btnSearch.Location = New Point(762, 54)
        btnSearch.Margin = New Padding(5, 6, 5, 6)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(52, 54)
        btnSearch.TabIndex = 5
        btnSearch.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.MidnightBlue
        Label1.Location = New Point(23, 31)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(335, 25)
        Label1.TabIndex = 0
        Label1.Text = "Report Search Term"
        ' 
        ' dgvReports
        ' 
        dgvReports.AllowUserToAddRows = False
        dgvReports.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.LemonChiffon
        dgvReports.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvReports.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvReports.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvReports.Columns.AddRange(New DataGridViewColumn() {ReportID, RptName})
        dgvReports.Location = New Point(20, 212)
        dgvReports.Margin = New Padding(5, 6, 5, 6)
        dgvReports.Name = "dgvReports"
        dgvReports.ReadOnly = True
        dgvReports.RowHeadersVisible = False
        dgvReports.RowHeadersWidth = 62
        dgvReports.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvReports.Size = New Size(838, 440)
        dgvReports.TabIndex = 28
        ' 
        ' ReportID
        ' 
        ReportID.FillWeight = 80F
        ReportID.HeaderText = "Report ID"
        ReportID.MaxInputLength = 6
        ReportID.MinimumWidth = 8
        ReportID.Name = "ReportID"
        ReportID.ReadOnly = True
        ' 
        ' RptName
        ' 
        RptName.FillWeight = 420F
        RptName.HeaderText = "Report Name"
        RptName.MaxInputLength = 60
        RptName.MinimumWidth = 8
        RptName.Name = "RptName"
        RptName.ReadOnly = True
        ' 
        ' frmReportLookUp
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(880, 675)
        Controls.Add(dgvReports)
        Controls.Add(GroupBox1)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmReportLookUp"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "frmReportLookUp"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        CType(dgvReports, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnAccept As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbTerm As System.Windows.Forms.ComboBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvReports As System.Windows.Forms.DataGridView
    Friend WithEvents txtTerm As System.Windows.Forms.TextBox
    Friend WithEvents ReportID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RptName As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
