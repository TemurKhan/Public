<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintAccLog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintAccLog))
        ToolStrip1 = New ToolStrip()
        btnProcess = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        grpDateRange = New GroupBox()
        lblTo = New Label()
        lblFrom = New Label()
        dtpDateTo = New DateTimePicker()
        dtpDateFrom = New DateTimePicker()
        Label1 = New Label()
        cmbDestination = New ComboBox()
        Label2 = New Label()
        cmbAccStaged = New ComboBox()
        ToolStrip1.SuspendLayout()
        grpDateRange.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnProcess, ToolStripSeparator1, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(598, 34)
        ToolStrip1.TabIndex = 5
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnProcess
        ' 
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
        ' grpDateRange
        ' 
        grpDateRange.Controls.Add(lblTo)
        grpDateRange.Controls.Add(lblFrom)
        grpDateRange.Controls.Add(dtpDateTo)
        grpDateRange.Controls.Add(dtpDateFrom)
        grpDateRange.Location = New Point(20, 79)
        grpDateRange.Margin = New Padding(5, 6, 5, 6)
        grpDateRange.Name = "grpDateRange"
        grpDateRange.Padding = New Padding(5, 6, 5, 6)
        grpDateRange.Size = New Size(558, 158)
        grpDateRange.TabIndex = 7
        grpDateRange.TabStop = False
        grpDateRange.Text = "Date Range"
        ' 
        ' lblTo
        ' 
        lblTo.ForeColor = Color.DarkBlue
        lblTo.Location = New Point(333, 40)
        lblTo.Margin = New Padding(5, 0, 5, 0)
        lblTo.Name = "lblTo"
        lblTo.Size = New Size(192, 35)
        lblTo.TabIndex = 17
        lblTo.Text = "Rc'vd To"
        lblTo.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblFrom
        ' 
        lblFrom.ForeColor = Color.DarkBlue
        lblFrom.Location = New Point(30, 40)
        lblFrom.Margin = New Padding(5, 0, 5, 0)
        lblFrom.Name = "lblFrom"
        lblFrom.Size = New Size(192, 35)
        lblFrom.TabIndex = 16
        lblFrom.Text = "Rc'vd From"
        lblFrom.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' dtpDateTo
        ' 
        dtpDateTo.Format = DateTimePickerFormat.Short
        dtpDateTo.Location = New Point(338, 81)
        dtpDateTo.Margin = New Padding(5, 6, 5, 6)
        dtpDateTo.Name = "dtpDateTo"
        dtpDateTo.Size = New Size(184, 31)
        dtpDateTo.TabIndex = 5
        ' 
        ' dtpDateFrom
        ' 
        dtpDateFrom.Format = DateTimePickerFormat.Short
        dtpDateFrom.Location = New Point(35, 81)
        dtpDateFrom.Margin = New Padding(5, 6, 5, 6)
        dtpDateFrom.Name = "dtpDateFrom"
        dtpDateFrom.Size = New Size(189, 31)
        dtpDateFrom.TabIndex = 4
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(390, 287)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(160, 35)
        Label1.TabIndex = 16
        Label1.Text = "Report Destination"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' cmbDestination
        ' 
        cmbDestination.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDestination.FormattingEnabled = True
        cmbDestination.Items.AddRange(New Object() {"Printer", "Screen"})
        cmbDestination.Location = New Point(390, 327)
        cmbDestination.Margin = New Padding(5, 6, 5, 6)
        cmbDestination.Name = "cmbDestination"
        cmbDestination.Size = New Size(186, 33)
        cmbDestination.TabIndex = 15
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(15, 287)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(258, 35)
        Label2.TabIndex = 18
        Label2.Text = "Accession staged"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' cmbAccStaged
        ' 
        cmbAccStaged.DropDownStyle = ComboBoxStyle.DropDownList
        cmbAccStaged.FormattingEnabled = True
        cmbAccStaged.Items.AddRange(New Object() {"Lab Acc'd + Rec'd Rem Acc'd", "Remote Acc'd but not Rec'd"})
        cmbAccStaged.Location = New Point(20, 327)
        cmbAccStaged.Margin = New Padding(5, 6, 5, 6)
        cmbAccStaged.Name = "cmbAccStaged"
        cmbAccStaged.Size = New Size(357, 33)
        cmbAccStaged.TabIndex = 17
        ' 
        ' frmPrintAccLog
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(598, 410)
        Controls.Add(Label2)
        Controls.Add(cmbAccStaged)
        Controls.Add(Label1)
        Controls.Add(cmbDestination)
        Controls.Add(grpDateRange)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmPrintAccLog"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Print Accession Log"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        grpDateRange.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnProcess As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents grpDateRange As System.Windows.Forms.GroupBox
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents dtpDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbDestination As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbAccStaged As System.Windows.Forms.ComboBox
    'Friend WithEvents gReport As CrystalDecisions.CrystalReports.Engine.ReportDocument

End Class
