<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintSendouts
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintSendouts))
        ToolStrip1 = New ToolStrip()
        btnProcess = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        chkOrderLabel = New CheckBox()
        Label1 = New Label()
        txtQty = New TextBox()
        btnClear = New Button()
        dgvDiscrete = New DataGridView()
        Accessions = New DataGridViewTextBoxColumn()
        grpAccRange = New GroupBox()
        Label6 = New Label()
        Label5 = New Label()
        txtAccTo = New TextBox()
        txtAccFrom = New TextBox()
        GroupBox1 = New GroupBox()
        txtDateTo = New MaskedTextBox()
        txtDateFrom = New MaskedTextBox()
        Label2 = New Label()
        Label3 = New Label()
        chkPT = New CheckBox()
        cmbRefLab = New ComboBox()
        Label4 = New Label()
        ToolStrip1.SuspendLayout()
        CType(dgvDiscrete, ComponentModel.ISupportInitialize).BeginInit()
        grpAccRange.SuspendLayout()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnProcess, ToolStripSeparator1, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(693, 34)
        ToolStrip1.TabIndex = 6
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
        ' chkOrderLabel
        ' 
        chkOrderLabel.Appearance = Appearance.Button
        chkOrderLabel.Image = CType(resources.GetObject("chkOrderLabel.Image"), Image)
        chkOrderLabel.Location = New Point(20, 133)
        chkOrderLabel.Margin = New Padding(5, 6, 5, 6)
        chkOrderLabel.Name = "chkOrderLabel"
        chkOrderLabel.Size = New Size(292, 48)
        chkOrderLabel.TabIndex = 3
        chkOrderLabel.Text = "Lab Orders"
        chkOrderLabel.TextAlign = ContentAlignment.MiddleRight
        chkOrderLabel.TextImageRelation = TextImageRelation.ImageBeforeText
        chkOrderLabel.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(88, 519)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(197, 35)
        Label1.TabIndex = 23
        Label1.Text = "Quantity to print"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' txtQty
        ' 
        txtQty.Location = New Point(93, 558)
        txtQty.Margin = New Padding(5, 6, 5, 6)
        txtQty.MaxLength = 2
        txtQty.Name = "txtQty"
        txtQty.Size = New Size(189, 31)
        txtQty.TabIndex = 9
        txtQty.TextAlign = HorizontalAlignment.Center
        ' 
        ' btnClear
        ' 
        btnClear.Enabled = False
        btnClear.ForeColor = Color.Red
        btnClear.Image = CType(resources.GetObject("btnClear.Image"), Image)
        btnClear.Location = New Point(372, 554)
        btnClear.Margin = New Padding(5, 6, 5, 6)
        btnClear.Name = "btnClear"
        btnClear.Size = New Size(270, 44)
        btnClear.TabIndex = 21
        btnClear.TabStop = False
        btnClear.Text = "Clear List"
        btnClear.TextAlign = ContentAlignment.MiddleRight
        btnClear.TextImageRelation = TextImageRelation.ImageBeforeText
        btnClear.UseVisualStyleBackColor = True
        ' 
        ' dgvDiscrete
        ' 
        dgvDiscrete.AllowUserToAddRows = False
        dgvDiscrete.AllowUserToDeleteRows = False
        dgvDiscrete.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvDiscrete.Columns.AddRange(New DataGridViewColumn() {Accessions})
        dgvDiscrete.Location = New Point(342, 133)
        dgvDiscrete.Margin = New Padding(5, 6, 5, 6)
        dgvDiscrete.Name = "dgvDiscrete"
        dgvDiscrete.RowHeadersVisible = False
        dgvDiscrete.RowHeadersWidth = 62
        dgvDiscrete.Size = New Size(332, 392)
        dgvDiscrete.TabIndex = 8
        ' 
        ' Accessions
        ' 
        Accessions.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Accessions.HeaderText = "Accessions"
        Accessions.MinimumWidth = 8
        Accessions.Name = "Accessions"
        ' 
        ' grpAccRange
        ' 
        grpAccRange.Controls.Add(Label6)
        grpAccRange.Controls.Add(Label5)
        grpAccRange.Controls.Add(txtAccTo)
        grpAccRange.Controls.Add(txtAccFrom)
        grpAccRange.Location = New Point(20, 192)
        grpAccRange.Margin = New Padding(5, 6, 5, 6)
        grpAccRange.Name = "grpAccRange"
        grpAccRange.Padding = New Padding(5, 6, 5, 6)
        grpAccRange.Size = New Size(292, 156)
        grpAccRange.TabIndex = 19
        grpAccRange.TabStop = False
        grpAccRange.Text = "Accession Range"
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.DarkBlue
        Label6.Location = New Point(10, 94)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(70, 35)
        Label6.TabIndex = 17
        Label6.Text = "To"
        Label6.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.DarkBlue
        Label5.Location = New Point(10, 38)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(70, 35)
        Label5.TabIndex = 16
        Label5.Text = "From"
        Label5.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' txtAccTo
        ' 
        txtAccTo.Location = New Point(90, 94)
        txtAccTo.Margin = New Padding(5, 6, 5, 6)
        txtAccTo.Name = "txtAccTo"
        txtAccTo.Size = New Size(172, 31)
        txtAccTo.TabIndex = 5
        txtAccTo.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtAccFrom
        ' 
        txtAccFrom.Location = New Point(90, 38)
        txtAccFrom.Margin = New Padding(5, 6, 5, 6)
        txtAccFrom.Name = "txtAccFrom"
        txtAccFrom.Size = New Size(172, 31)
        txtAccFrom.TabIndex = 4
        txtAccFrom.TextAlign = HorizontalAlignment.Center
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(txtDateTo)
        GroupBox1.Controls.Add(txtDateFrom)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Location = New Point(20, 360)
        GroupBox1.Margin = New Padding(5, 6, 5, 6)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(5, 6, 5, 6)
        GroupBox1.Size = New Size(292, 154)
        GroupBox1.TabIndex = 24
        GroupBox1.TabStop = False
        GroupBox1.Text = "Date Range"
        ' 
        ' txtDateTo
        ' 
        txtDateTo.Location = New Point(90, 87)
        txtDateTo.Margin = New Padding(5, 6, 5, 6)
        txtDateTo.Mask = "00/00/0000"
        txtDateTo.Name = "txtDateTo"
        txtDateTo.Size = New Size(172, 31)
        txtDateTo.TabIndex = 7
        txtDateTo.ValidatingType = GetType(Date)
        ' 
        ' txtDateFrom
        ' 
        txtDateFrom.Location = New Point(90, 37)
        txtDateFrom.Margin = New Padding(5, 6, 5, 6)
        txtDateFrom.Mask = "00/00/0000"
        txtDateFrom.Name = "txtDateFrom"
        txtDateFrom.Size = New Size(172, 31)
        txtDateFrom.TabIndex = 6
        txtDateFrom.ValidatingType = GetType(Date)
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(10, 87)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(70, 35)
        Label2.TabIndex = 17
        Label2.Text = "To"
        Label2.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(13, 29)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(67, 35)
        Label3.TabIndex = 16
        Label3.Text = "From"
        Label3.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' chkPT
        ' 
        chkPT.Appearance = Appearance.Button
        chkPT.Location = New Point(20, 73)
        chkPT.Margin = New Padding(5, 6, 5, 6)
        chkPT.Name = "chkPT"
        chkPT.Size = New Size(113, 48)
        chkPT.TabIndex = 1
        chkPT.Text = "Print"
        chkPT.TextAlign = ContentAlignment.MiddleCenter
        chkPT.TextImageRelation = TextImageRelation.ImageBeforeText
        chkPT.UseVisualStyleBackColor = True
        ' 
        ' cmbRefLab
        ' 
        cmbRefLab.FormattingEnabled = True
        cmbRefLab.Location = New Point(238, 79)
        cmbRefLab.Margin = New Padding(5, 6, 5, 6)
        cmbRefLab.Name = "cmbRefLab"
        cmbRefLab.Size = New Size(431, 33)
        cmbRefLab.TabIndex = 2
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(153, 85)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(70, 25)
        Label4.TabIndex = 27
        Label4.Text = "Ref Lab"
        ' 
        ' frmPrintSendouts
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(693, 627)
        Controls.Add(Label4)
        Controls.Add(cmbRefLab)
        Controls.Add(chkPT)
        Controls.Add(GroupBox1)
        Controls.Add(Label1)
        Controls.Add(txtQty)
        Controls.Add(btnClear)
        Controls.Add(dgvDiscrete)
        Controls.Add(grpAccRange)
        Controls.Add(chkOrderLabel)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmPrintSendouts"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Print Sendouts"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgvDiscrete, ComponentModel.ISupportInitialize).EndInit()
        grpAccRange.ResumeLayout(False)
        grpAccRange.PerformLayout()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnProcess As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents chkOrderLabel As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents dgvDiscrete As System.Windows.Forms.DataGridView
    Friend WithEvents Accessions As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grpAccRange As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtAccTo As System.Windows.Forms.TextBox
    Friend WithEvents txtAccFrom As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Friend WithEvents chkPT As System.Windows.Forms.CheckBox
    Friend WithEvents cmbRefLab As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label

End Class
