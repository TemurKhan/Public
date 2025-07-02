<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBARAdhoc
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBARAdhoc))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        btnExecute = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnPrint = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnExport = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        dgvResult = New DataGridView()
        StatusStrip1 = New StatusStrip()
        lblStatus = New ToolStripStatusLabel()
        PB = New ToolStripProgressBar()
        dgvFields = New DataGridView()
        Sel = New DataGridViewCheckBoxColumn()
        Field = New DataGridViewTextBoxColumn()
        FLD = New DataGridViewTextBoxColumn()
        Table = New DataGridViewTextBoxColumn()
        Tbl = New DataGridViewTextBoxColumn()
        TC = New TabControl()
        tpParams = New TabPage()
        btnReset = New Button()
        Label8 = New Label()
        Label7 = New Label()
        txtIDTo = New TextBox()
        txtIDFrom = New TextBox()
        Label6 = New Label()
        cmbIdentype = New ComboBox()
        txtDateTo = New MaskedTextBox()
        txtDateFrom = New MaskedTextBox()
        Label5 = New Label()
        Label4 = New Label()
        Label3 = New Label()
        cmbDateType = New ComboBox()
        Label2 = New Label()
        Label1 = New Label()
        tpSQL = New TabPage()
        txtSQL = New TextBox()
        ToolStrip1.SuspendLayout()
        CType(dgvResult, ComponentModel.ISupportInitialize).BeginInit()
        StatusStrip1.SuspendLayout()
        CType(dgvFields, ComponentModel.ISupportInitialize).BeginInit()
        TC.SuspendLayout()
        tpParams.SuspendLayout()
        tpSQL.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnExecute, ToolStripSeparator2, btnPrint, ToolStripSeparator3, btnExport, ToolStripSeparator1, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(1295, 34)
        ToolStrip1.TabIndex = 2
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnExecute
        ' 
        btnExecute.Enabled = False
        btnExecute.Image = CType(resources.GetObject("btnExecute.Image"), Image)
        btnExecute.ImageTransparentColor = Color.Magenta
        btnExecute.Name = "btnExecute"
        btnExecute.Size = New Size(99, 29)
        btnExecute.Text = "Execute"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 34)
        ' 
        ' btnPrint
        ' 
        btnPrint.Enabled = False
        btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), Image)
        btnPrint.ImageTransparentColor = Color.Magenta
        btnPrint.Name = "btnPrint"
        btnPrint.Size = New Size(76, 29)
        btnPrint.Text = "Print"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(6, 34)
        ' 
        ' btnExport
        ' 
        btnExport.Enabled = False
        btnExport.Image = CType(resources.GetObject("btnExport.Image"), Image)
        btnExport.ImageTransparentColor = Color.Magenta
        btnExport.Name = "btnExport"
        btnExport.Size = New Size(126, 29)
        btnExport.Text = "Export Text"
        btnExport.ToolTipText = "Export Tab delimited Text"
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
        ' dgvResult
        ' 
        dgvResult.AllowUserToAddRows = False
        dgvResult.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.FloralWhite
        dgvResult.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvResult.Location = New Point(20, 635)
        dgvResult.Margin = New Padding(5, 6, 5, 6)
        dgvResult.Name = "dgvResult"
        dgvResult.ReadOnly = True
        dgvResult.RowHeadersVisible = False
        dgvResult.RowHeadersWidth = 62
        dgvResult.Size = New Size(1255, 533)
        dgvResult.TabIndex = 3
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.ImageScalingSize = New Size(24, 24)
        StatusStrip1.Items.AddRange(New ToolStripItem() {lblStatus, PB})
        StatusStrip1.Location = New Point(0, 1200)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Padding = New Padding(2, 0, 23, 0)
        StatusStrip1.Size = New Size(1295, 35)
        StatusStrip1.TabIndex = 8
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = False
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(650, 28)
        ' 
        ' PB
        ' 
        PB.Name = "PB"
        PB.Size = New Size(167, 27)
        ' 
        ' dgvFields
        ' 
        dgvFields.AllowUserToAddRows = False
        dgvFields.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = Color.LavenderBlush
        dgvFields.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        dgvFields.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvFields.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvFields.Columns.AddRange(New DataGridViewColumn() {Sel, Field, FLD, Table, Tbl})
        dgvFields.Location = New Point(10, 60)
        dgvFields.Margin = New Padding(5, 6, 5, 6)
        dgvFields.Name = "dgvFields"
        dgvFields.RowHeadersVisible = False
        dgvFields.RowHeadersWidth = 62
        dgvFields.Size = New Size(772, 402)
        dgvFields.TabIndex = 9
        ' 
        ' Sel
        ' 
        Sel.FillWeight = 40F
        Sel.HeaderText = ""
        Sel.MinimumWidth = 8
        Sel.Name = "Sel"
        Sel.Resizable = DataGridViewTriState.True
        ' 
        ' Field
        ' 
        Field.HeaderText = "Field"
        Field.MinimumWidth = 8
        Field.Name = "Field"
        Field.Visible = False
        ' 
        ' FLD
        ' 
        FLD.FillWeight = 200F
        FLD.HeaderText = "FIELD NNAME"
        FLD.MinimumWidth = 8
        FLD.Name = "FLD"
        FLD.ReadOnly = True
        FLD.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Table
        ' 
        Table.FillWeight = 200F
        Table.HeaderText = "Table"
        Table.MinimumWidth = 8
        Table.Name = "Table"
        Table.ReadOnly = True
        Table.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Tbl
        ' 
        Tbl.HeaderText = "Tbl"
        Tbl.MinimumWidth = 8
        Tbl.Name = "Tbl"
        Tbl.Visible = False
        ' 
        ' TC
        ' 
        TC.Controls.Add(tpParams)
        TC.Controls.Add(tpSQL)
        TC.Location = New Point(20, 79)
        TC.Margin = New Padding(5, 6, 5, 6)
        TC.Name = "TC"
        TC.SelectedIndex = 0
        TC.Size = New Size(1255, 544)
        TC.TabIndex = 1
        ' 
        ' tpParams
        ' 
        tpParams.Controls.Add(btnReset)
        tpParams.Controls.Add(Label8)
        tpParams.Controls.Add(Label7)
        tpParams.Controls.Add(txtIDTo)
        tpParams.Controls.Add(txtIDFrom)
        tpParams.Controls.Add(Label6)
        tpParams.Controls.Add(cmbIdentype)
        tpParams.Controls.Add(txtDateTo)
        tpParams.Controls.Add(txtDateFrom)
        tpParams.Controls.Add(Label5)
        tpParams.Controls.Add(Label4)
        tpParams.Controls.Add(Label3)
        tpParams.Controls.Add(cmbDateType)
        tpParams.Controls.Add(Label2)
        tpParams.Controls.Add(Label1)
        tpParams.Controls.Add(dgvFields)
        tpParams.Location = New Point(4, 34)
        tpParams.Margin = New Padding(5, 6, 5, 6)
        tpParams.Name = "tpParams"
        tpParams.Padding = New Padding(5, 6, 5, 6)
        tpParams.Size = New Size(1247, 506)
        tpParams.TabIndex = 0
        tpParams.Text = "Parameters"
        tpParams.UseVisualStyleBackColor = True
        ' 
        ' btnReset
        ' 
        btnReset.Image = CType(resources.GetObject("btnReset.Image"), Image)
        btnReset.ImageAlign = ContentAlignment.MiddleRight
        btnReset.Location = New Point(937, 417)
        btnReset.Margin = New Padding(5, 6, 5, 6)
        btnReset.Name = "btnReset"
        btnReset.Size = New Size(128, 65)
        btnReset.TabIndex = 24
        btnReset.Text = "Reset"
        btnReset.TextImageRelation = TextImageRelation.ImageBeforeText
        btnReset.UseVisualStyleBackColor = True
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.ForeColor = SystemColors.ActiveCaptionText
        Label8.Location = New Point(1027, 337)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(105, 25)
        Label8.TabIndex = 23
        Label8.Text = "Identifier To"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.ForeColor = Color.Red
        Label7.Location = New Point(832, 337)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(129, 25)
        Label7.TabIndex = 22
        Label7.Text = "Identifier From"
        ' 
        ' txtIDTo
        ' 
        txtIDTo.Location = New Point(1015, 367)
        txtIDTo.Margin = New Padding(5, 6, 5, 6)
        txtIDTo.Name = "txtIDTo"
        txtIDTo.Size = New Size(174, 31)
        txtIDTo.TabIndex = 21
        ' 
        ' txtIDFrom
        ' 
        txtIDFrom.Location = New Point(820, 367)
        txtIDFrom.Margin = New Padding(5, 6, 5, 6)
        txtIDFrom.Name = "txtIDFrom"
        txtIDFrom.Size = New Size(174, 31)
        txtIDFrom.TabIndex = 20
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(900, 250)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(124, 25)
        Label6.TabIndex = 19
        Label6.Text = "Identifier Type"
        ' 
        ' cmbIdentype
        ' 
        cmbIdentype.DropDownStyle = ComboBoxStyle.DropDownList
        cmbIdentype.FormattingEnabled = True
        cmbIdentype.Items.AddRange(New Object() {"Accession", "Invoice", "Document"})
        cmbIdentype.Location = New Point(883, 281)
        cmbIdentype.Margin = New Padding(5, 6, 5, 6)
        cmbIdentype.Name = "cmbIdentype"
        cmbIdentype.Size = New Size(246, 33)
        cmbIdentype.TabIndex = 18
        ' 
        ' txtDateTo
        ' 
        txtDateTo.Location = New Point(1050, 188)
        txtDateTo.Margin = New Padding(5, 6, 5, 6)
        txtDateTo.Mask = "00/00/0000"
        txtDateTo.Name = "txtDateTo"
        txtDateTo.Size = New Size(139, 31)
        txtDateTo.TabIndex = 17
        txtDateTo.ValidatingType = GetType(Date)
        ' 
        ' txtDateFrom
        ' 
        txtDateFrom.Location = New Point(820, 188)
        txtDateFrom.Margin = New Padding(5, 6, 5, 6)
        txtDateFrom.Mask = "00/00/0000"
        txtDateFrom.Name = "txtDateFrom"
        txtDateFrom.Size = New Size(139, 31)
        txtDateFrom.TabIndex = 16
        txtDateFrom.ValidatingType = GetType(Date)
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.ForeColor = Color.Red
        Label5.Location = New Point(1055, 158)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(72, 25)
        Label5.TabIndex = 15
        Label5.Text = "Date To"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.ForeColor = Color.Red
        Label4.Location = New Point(832, 158)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(96, 25)
        Label4.TabIndex = 14
        Label4.Text = "Date From"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(900, 60)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(91, 25)
        Label3.TabIndex = 13
        Label3.Text = "Date Type"
        ' 
        ' cmbDateType
        ' 
        cmbDateType.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDateType.FormattingEnabled = True
        cmbDateType.Items.AddRange(New Object() {"Accession", "Receive", "Service", "Reported", "Bill", "Payment"})
        cmbDateType.Location = New Point(883, 90)
        cmbDateType.Margin = New Padding(5, 6, 5, 6)
        cmbDateType.Name = "cmbDateType"
        cmbDateType.Size = New Size(246, 33)
        cmbDateType.TabIndex = 12
        ' 
        ' Label2
        ' 
        Label2.Font = New Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.Red
        Label2.Location = New Point(787, 19)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(445, 29)
        Label2.TabIndex = 11
        Label2.Text = "Select Data filters"
        Label2.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label1
        ' 
        Label1.Font = New Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.Red
        Label1.Location = New Point(10, 19)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(758, 29)
        Label1.TabIndex = 10
        Label1.Text = "Select desired fields below"
        Label1.TextAlign = ContentAlignment.TopCenter
        ' 
        ' tpSQL
        ' 
        tpSQL.Controls.Add(txtSQL)
        tpSQL.Location = New Point(4, 34)
        tpSQL.Margin = New Padding(5, 6, 5, 6)
        tpSQL.Name = "tpSQL"
        tpSQL.Padding = New Padding(5, 6, 5, 6)
        tpSQL.Size = New Size(1247, 506)
        tpSQL.TabIndex = 1
        tpSQL.Text = "SQL"
        tpSQL.UseVisualStyleBackColor = True
        ' 
        ' txtSQL
        ' 
        txtSQL.Location = New Point(5, 6)
        txtSQL.Margin = New Padding(5, 6, 5, 6)
        txtSQL.Multiline = True
        txtSQL.Name = "txtSQL"
        txtSQL.ReadOnly = True
        txtSQL.ScrollBars = ScrollBars.Vertical
        txtSQL.Size = New Size(1224, 452)
        txtSQL.TabIndex = 0
        ' 
        ' frmBARAdhoc
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1295, 1235)
        Controls.Add(TC)
        Controls.Add(StatusStrip1)
        Controls.Add(dgvResult)
        Controls.Add(ToolStrip1)
        Margin = New Padding(5, 6, 5, 6)
        Name = "frmBARAdhoc"
        Text = "BAR ADHOC Report"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgvResult, ComponentModel.ISupportInitialize).EndInit()
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        CType(dgvFields, ComponentModel.ISupportInitialize).EndInit()
        TC.ResumeLayout(False)
        tpParams.ResumeLayout(False)
        tpParams.PerformLayout()
        tpSQL.ResumeLayout(False)
        tpSQL.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnExecute As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents dgvResult As System.Windows.Forms.DataGridView
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PB As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents dgvFields As System.Windows.Forms.DataGridView
    Friend WithEvents TC As System.Windows.Forms.TabControl
    Friend WithEvents tpParams As System.Windows.Forms.TabPage
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtIDTo As System.Windows.Forms.TextBox
    Friend WithEvents txtIDFrom As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbIdentype As System.Windows.Forms.ComboBox
    Friend WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbDateType As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tpSQL As System.Windows.Forms.TabPage
    Friend WithEvents txtSQL As System.Windows.Forms.TextBox
    Friend WithEvents Sel As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Field As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FLD As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Table As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tbl As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
