<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDiagnosis
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDiagnosis))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        btnAccept = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        cmbMode = New ToolStripComboBox()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnSave = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnDelete = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        btnHelp = New ToolStripButton()
        dgvDxs = New DataGridView()
        DxCode = New DataGridViewTextBoxColumn()
        Status = New DataGridViewTextBoxColumn()
        DxName = New DataGridViewTextBoxColumn()
        Label3 = New Label()
        txtDxName = New TextBox()
        Label2 = New Label()
        txtDxCode = New TextBox()
        txtSearch = New TextBox()
        Label4 = New Label()
        btnImport = New Button()
        chkOS = New CheckBox()
        btnSearch = New Button()
        OpenFileDialog1 = New OpenFileDialog()
        cmbStatus = New ComboBox()
        Label5 = New Label()
        Label1 = New Label()
        cmbOutput = New ComboBox()
        Label6 = New Label()
        ToolStrip1.SuspendLayout()
        CType(dgvDxs, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnAccept, ToolStripSeparator1, cmbMode, ToolStripSeparator2, btnSave, ToolStripSeparator3, btnDelete, ToolStripSeparator4, btnCancel, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(1055, 34)
        ToolStrip1.TabIndex = 2
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
        ' cmbMode
        ' 
        cmbMode.DropDownStyle = ComboBoxStyle.DropDownList
        cmbMode.Items.AddRange(New Object() {"View", "Edit", "New"})
        cmbMode.Name = "cmbMode"
        cmbMode.Size = New Size(164, 34)
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 34)
        ' 
        ' btnSave
        ' 
        btnSave.AutoSize = False
        btnSave.Image = CType(resources.GetObject("btnSave.Image"), Image)
        btnSave.ImageTransparentColor = Color.Magenta
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(58, 22)
        btnSave.Text = "Save"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(6, 34)
        ' 
        ' btnDelete
        ' 
        btnDelete.AutoSize = False
        btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), Image)
        btnDelete.ImageTransparentColor = Color.Magenta
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(59, 22)
        btnDelete.Text = "Delete"
        ' 
        ' ToolStripSeparator4
        ' 
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New Size(6, 34)
        ' 
        ' btnCancel
        ' 
        btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), Image)
        btnCancel.ImageTransparentColor = Color.Magenta
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(91, 29)
        btnCancel.Text = "Cancel"
        ' 
        ' btnHelp
        ' 
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.ImageTransparentColor = Color.Magenta
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(77, 29)
        btnHelp.Text = "Help"
        ' 
        ' dgvDxs
        ' 
        dgvDxs.AllowUserToAddRows = False
        dgvDxs.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(255), CByte(224), CByte(192))
        dgvDxs.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvDxs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvDxs.Columns.AddRange(New DataGridViewColumn() {DxCode, Status, DxName})
        dgvDxs.Location = New Point(20, 163)
        dgvDxs.Margin = New Padding(5, 6, 5, 6)
        dgvDxs.Name = "dgvDxs"
        dgvDxs.ReadOnly = True
        dgvDxs.RowHeadersVisible = False
        dgvDxs.RowHeadersWidth = 62
        dgvDxs.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvDxs.Size = New Size(1015, 362)
        dgvDxs.TabIndex = 28
        ' 
        ' DxCode
        ' 
        DxCode.FillWeight = 80F
        DxCode.HeaderText = "Dx Code"
        DxCode.MaxInputLength = 12
        DxCode.MinimumWidth = 8
        DxCode.Name = "DxCode"
        DxCode.ReadOnly = True
        DxCode.Width = 134
        ' 
        ' Status
        ' 
        Status.FillWeight = 45F
        Status.HeaderText = "Status"
        Status.MaxInputLength = 1
        Status.MinimumWidth = 8
        Status.Name = "Status"
        Status.ReadOnly = True
        Status.SortMode = DataGridViewColumnSortMode.NotSortable
        Status.Width = 75
        ' 
        ' DxName
        ' 
        DxName.FillWeight = 480F
        DxName.HeaderText = "Description"
        DxName.MaxInputLength = 255
        DxName.MinimumWidth = 8
        DxName.Name = "DxName"
        DxName.ReadOnly = True
        DxName.Width = 803
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.MidnightBlue
        Label3.Location = New Point(20, 623)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(192, 25)
        Label3.TabIndex = 27
        Label3.Text = "Dx Description"
        ' 
        ' txtDxName
        ' 
        txtDxName.Location = New Point(20, 654)
        txtDxName.Margin = New Padding(5, 6, 5, 6)
        txtDxName.MaxLength = 500
        txtDxName.Multiline = True
        txtDxName.Name = "txtDxName"
        txtDxName.ScrollBars = ScrollBars.Vertical
        txtDxName.Size = New Size(1012, 210)
        txtDxName.TabIndex = 26
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.MidnightBlue
        Label2.Location = New Point(20, 546)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(172, 25)
        Label2.TabIndex = 25
        Label2.Text = "Dx Code"
        ' 
        ' txtDxCode
        ' 
        txtDxCode.Location = New Point(25, 577)
        txtDxCode.Margin = New Padding(5, 6, 5, 6)
        txtDxCode.MaxLength = 12
        txtDxCode.Name = "txtDxCode"
        txtDxCode.Size = New Size(194, 31)
        txtDxCode.TabIndex = 24
        ' 
        ' txtSearch
        ' 
        txtSearch.Location = New Point(20, 96)
        txtSearch.Margin = New Padding(5, 6, 5, 6)
        txtSearch.MaxLength = 500
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(666, 31)
        txtSearch.TabIndex = 2
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.MidnightBlue
        Label4.Location = New Point(20, 65)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(490, 25)
        Label4.TabIndex = 30
        Label4.Text = "Search Code (Complete) or Description (Part)"
        ' 
        ' btnImport
        ' 
        btnImport.Enabled = False
        btnImport.ForeColor = Color.DarkBlue
        btnImport.Location = New Point(738, 571)
        btnImport.Margin = New Padding(5, 6, 5, 6)
        btnImport.Name = "btnImport"
        btnImport.Size = New Size(158, 46)
        btnImport.TabIndex = 31
        btnImport.Text = "External Feed"
        btnImport.UseVisualStyleBackColor = True
        ' 
        ' chkOS
        ' 
        chkOS.Appearance = Appearance.Button
        chkOS.Checked = True
        chkOS.CheckState = CheckState.Checked
        chkOS.Enabled = False
        chkOS.ForeColor = Color.DarkBlue
        chkOS.Location = New Point(907, 571)
        chkOS.Margin = New Padding(5, 6, 5, 6)
        chkOS.Name = "chkOS"
        chkOS.Size = New Size(128, 46)
        chkOS.TabIndex = 32
        chkOS.Text = "Yes"
        chkOS.TextAlign = ContentAlignment.MiddleCenter
        chkOS.UseVisualStyleBackColor = True
        ' 
        ' btnSearch
        ' 
        btnSearch.Enabled = False
        btnSearch.ForeColor = Color.DarkBlue
        btnSearch.Location = New Point(698, 90)
        btnSearch.Margin = New Padding(5, 6, 5, 6)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(122, 46)
        btnSearch.TabIndex = 3
        btnSearch.Text = "Search"
        btnSearch.UseVisualStyleBackColor = True
        ' 
        ' OpenFileDialog1
        ' 
        OpenFileDialog1.FileName = "OpenFileDialog1"
        ' 
        ' cmbStatus
        ' 
        cmbStatus.FormattingEnabled = True
        cmbStatus.Items.AddRange(New Object() {"Incomplete", "Complete"})
        cmbStatus.Location = New Point(232, 577)
        cmbStatus.Margin = New Padding(5, 6, 5, 6)
        cmbStatus.Name = "cmbStatus"
        cmbStatus.Size = New Size(201, 33)
        cmbStatus.TabIndex = 34
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.MidnightBlue
        Label5.Location = New Point(227, 546)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(172, 25)
        Label5.TabIndex = 35
        Label5.Text = "Code Status"
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.MidnightBlue
        Label1.Location = New Point(907, 546)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(127, 25)
        Label1.TabIndex = 36
        Label1.Text = "Overwrite"
        Label1.TextAlign = ContentAlignment.TopCenter
        ' 
        ' cmbOutput
        ' 
        cmbOutput.DropDownStyle = ComboBoxStyle.DropDownList
        cmbOutput.FormattingEnabled = True
        cmbOutput.Items.AddRange(New Object() {"ICD10", "ICD9", "BOTH"})
        cmbOutput.Location = New Point(830, 96)
        cmbOutput.Margin = New Padding(5, 6, 5, 6)
        cmbOutput.Name = "cmbOutput"
        cmbOutput.Size = New Size(201, 33)
        cmbOutput.TabIndex = 4
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.MidnightBlue
        Label6.Location = New Point(840, 65)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(172, 25)
        Label6.TabIndex = 38
        Label6.Text = "Output Type"
        ' 
        ' frmDiagnosis
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1055, 890)
        Controls.Add(Label6)
        Controls.Add(cmbOutput)
        Controls.Add(Label1)
        Controls.Add(Label5)
        Controls.Add(cmbStatus)
        Controls.Add(btnSearch)
        Controls.Add(chkOS)
        Controls.Add(btnImport)
        Controls.Add(Label4)
        Controls.Add(txtSearch)
        Controls.Add(dgvDxs)
        Controls.Add(Label3)
        Controls.Add(txtDxName)
        Controls.Add(Label2)
        Controls.Add(txtDxCode)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmDiagnosis"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Diagnosis Codes"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgvDxs, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnAccept As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents dgvDxs As System.Windows.Forms.DataGridView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDxName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDxCode As System.Windows.Forms.TextBox
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbMode As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnImport As System.Windows.Forms.Button
    Friend WithEvents chkOS As System.Windows.Forms.CheckBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DxCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DxName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbOutput As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label

End Class
