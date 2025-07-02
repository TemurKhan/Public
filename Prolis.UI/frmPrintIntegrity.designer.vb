<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintIntegrity
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintIntegrity))
        ToolStrip1 = New ToolStrip()
        btnPrint = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        grpDates = New GroupBox()
        dtpDateTo = New DateTimePicker()
        lblClearDates = New Label()
        dtpDateFrom = New DateTimePicker()
        Label2 = New Label()
        Label1 = New Label()
        grpAccessions = New GroupBox()
        txtAccTo = New TextBox()
        txtAccFrom = New TextBox()
        Label7 = New Label()
        Label6 = New Label()
        dgvAccessions = New DataGridView()
        Del = New DataGridViewImageColumn()
        Acc = New DataGridViewTextBoxColumn()
        Label3 = New Label()
        Label5 = New Label()
        cmbDestination = New ComboBox()
        txtCopies = New TextBox()
        Label4 = New Label()
        Label8 = New Label()
        ToolStrip1.SuspendLayout()
        grpDates.SuspendLayout()
        grpAccessions.SuspendLayout()
        CType(dgvAccessions, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(20, 20)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnPrint, ToolStripSeparator1, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(725, 34)
        ToolStrip1.TabIndex = 7
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnPrint
        ' 
        btnPrint.Enabled = False
        btnPrint.ForeColor = Color.DarkBlue
        btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), Image)
        btnPrint.ImageTransparentColor = Color.Magenta
        btnPrint.Name = "btnPrint"
        btnPrint.Size = New Size(72, 29)
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
        btnCancel.Size = New Size(87, 29)
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
        btnHelp.Size = New Size(73, 29)
        btnHelp.Text = "Help"
        ' 
        ' grpDates
        ' 
        grpDates.Controls.Add(dtpDateTo)
        grpDates.Controls.Add(lblClearDates)
        grpDates.Controls.Add(dtpDateFrom)
        grpDates.Controls.Add(Label2)
        grpDates.Controls.Add(Label1)
        grpDates.Location = New Point(20, 54)
        grpDates.Margin = New Padding(5, 6, 5, 6)
        grpDates.Name = "grpDates"
        grpDates.Padding = New Padding(5, 6, 5, 6)
        grpDates.Size = New Size(685, 106)
        grpDates.TabIndex = 8
        grpDates.TabStop = False
        grpDates.Text = "Accession Dates"
        ' 
        ' dtpDateTo
        ' 
        dtpDateTo.CustomFormat = " "
        dtpDateTo.Format = DateTimePickerFormat.Custom
        dtpDateTo.Location = New Point(390, 48)
        dtpDateTo.Margin = New Padding(3, 4, 3, 4)
        dtpDateTo.Name = "dtpDateTo"
        dtpDateTo.Size = New Size(129, 31)
        dtpDateTo.TabIndex = 95
        ' 
        ' lblClearDates
        ' 
        lblClearDates.BackColor = SystemColors.Control
        lblClearDates.Image = CType(resources.GetObject("lblClearDates.Image"), Image)
        lblClearDates.Location = New Point(538, 37)
        lblClearDates.Name = "lblClearDates"
        lblClearDates.Size = New Size(33, 46)
        lblClearDates.TabIndex = 96
        lblClearDates.Text = "      "
        ' 
        ' dtpDateFrom
        ' 
        dtpDateFrom.CustomFormat = " "
        dtpDateFrom.Format = DateTimePickerFormat.Custom
        dtpDateFrom.Location = New Point(100, 48)
        dtpDateFrom.Margin = New Padding(3, 4, 3, 4)
        dtpDateFrom.Name = "dtpDateFrom"
        dtpDateFrom.Size = New Size(129, 31)
        dtpDateFrom.TabIndex = 94
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.Magenta
        Label2.Location = New Point(347, 48)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(35, 25)
        Label2.TabIndex = 2
        Label2.Text = "To"
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.Red
        Label1.Location = New Point(17, 48)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(57, 25)
        Label1.TabIndex = 0
        Label1.Text = "From"
        Label1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' grpAccessions
        ' 
        grpAccessions.Controls.Add(txtAccTo)
        grpAccessions.Controls.Add(txtAccFrom)
        grpAccessions.Controls.Add(Label7)
        grpAccessions.Controls.Add(Label6)
        grpAccessions.Controls.Add(dgvAccessions)
        grpAccessions.Controls.Add(Label3)
        grpAccessions.Controls.Add(Label5)
        grpAccessions.Location = New Point(20, 171)
        grpAccessions.Margin = New Padding(5, 6, 5, 6)
        grpAccessions.Name = "grpAccessions"
        grpAccessions.Padding = New Padding(5, 6, 5, 6)
        grpAccessions.Size = New Size(685, 317)
        grpAccessions.TabIndex = 13
        grpAccessions.TabStop = False
        grpAccessions.Text = "Accession"
        ' 
        ' txtAccTo
        ' 
        txtAccTo.Location = New Point(22, 227)
        txtAccTo.Margin = New Padding(5, 6, 5, 6)
        txtAccTo.MaxLength = 12
        txtAccTo.Name = "txtAccTo"
        txtAccTo.Size = New Size(207, 31)
        txtAccTo.TabIndex = 10
        ' 
        ' txtAccFrom
        ' 
        txtAccFrom.Location = New Point(22, 119)
        txtAccFrom.Margin = New Padding(5, 6, 5, 6)
        txtAccFrom.MaxLength = 12
        txtAccFrom.Name = "txtAccFrom"
        txtAccFrom.Size = New Size(207, 31)
        txtAccFrom.TabIndex = 9
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.DarkBlue
        Label7.Location = New Point(27, 31)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(172, 25)
        Label7.TabIndex = 8
        Label7.Text = "Accessions in range"
        Label7.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.Magenta
        Label6.Location = New Point(385, 31)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(203, 25)
        Label6.TabIndex = 7
        Label6.Text = "Random Accessions"
        ' 
        ' dgvAccessions
        ' 
        dgvAccessions.AllowUserToAddRows = False
        dgvAccessions.AllowUserToDeleteRows = False
        dgvAccessions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvAccessions.Columns.AddRange(New DataGridViewColumn() {Del, Acc})
        dgvAccessions.Location = New Point(390, 69)
        dgvAccessions.Margin = New Padding(5, 6, 5, 6)
        dgvAccessions.Name = "dgvAccessions"
        dgvAccessions.RowHeadersVisible = False
        dgvAccessions.RowHeadersWidth = 51
        dgvAccessions.Size = New Size(265, 223)
        dgvAccessions.TabIndex = 6
        ' 
        ' Del
        ' 
        Del.FillWeight = 20F
        Del.HeaderText = "X"
        Del.Image = CType(resources.GetObject("Del.Image"), Image)
        Del.MinimumWidth = 6
        Del.Name = "Del"
        Del.Width = 20
        ' 
        ' Acc
        ' 
        Acc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Acc.HeaderText = "Accession"
        Acc.MaxInputLength = 12
        Acc.MinimumWidth = 6
        Acc.Name = "Acc"
        Acc.Resizable = DataGridViewTriState.True
        Acc.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.Magenta
        Label3.Location = New Point(17, 196)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(120, 25)
        Label3.TabIndex = 2
        Label3.Text = "To"
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.Fuchsia
        Label5.Location = New Point(17, 88)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(120, 25)
        Label5.TabIndex = 0
        Label5.Text = "From"
        ' 
        ' cmbDestination
        ' 
        cmbDestination.FormattingEnabled = True
        cmbDestination.Items.AddRange(New Object() {"Printer", "Screen"})
        cmbDestination.Location = New Point(410, 525)
        cmbDestination.Margin = New Padding(5, 6, 5, 6)
        cmbDestination.Name = "cmbDestination"
        cmbDestination.Size = New Size(262, 33)
        cmbDestination.TabIndex = 14
        ' 
        ' txtCopies
        ' 
        txtCopies.Location = New Point(158, 525)
        txtCopies.Margin = New Padding(5, 6, 5, 6)
        txtCopies.MaxLength = 1
        txtCopies.Name = "txtCopies"
        txtCopies.Size = New Size(91, 31)
        txtCopies.TabIndex = 15
        txtCopies.Text = "1"
        txtCopies.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.Red
        Label4.Location = New Point(42, 531)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(107, 25)
        Label4.TabIndex = 16
        Label4.Text = "Copies"
        Label4.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label8
        ' 
        Label8.ForeColor = Color.Red
        Label8.Location = New Point(292, 531)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(108, 25)
        Label8.TabIndex = 17
        Label8.Text = "Destination"
        Label8.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' frmPrintIntegrity
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(725, 606)
        Controls.Add(Label8)
        Controls.Add(Label4)
        Controls.Add(txtCopies)
        Controls.Add(cmbDestination)
        Controls.Add(grpAccessions)
        Controls.Add(grpDates)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmPrintIntegrity"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Print Integrity Report"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        grpDates.ResumeLayout(False)
        grpAccessions.ResumeLayout(False)
        grpAccessions.PerformLayout()
        CType(dgvAccessions, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents grpDates As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grpAccessions As System.Windows.Forms.GroupBox
    Friend WithEvents txtAccTo As System.Windows.Forms.TextBox
    Friend WithEvents txtAccFrom As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dgvAccessions As System.Windows.Forms.DataGridView
    Friend WithEvents Del As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents Acc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbDestination As System.Windows.Forms.ComboBox
    Friend WithEvents txtCopies As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dtpDateTo As DateTimePicker
    Friend WithEvents lblClearDates As Label
    Friend WithEvents dtpDateFrom As DateTimePicker
End Class
