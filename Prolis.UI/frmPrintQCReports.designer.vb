<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintQCReports
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintQCReports))
        ToolStrip1 = New ToolStrip()
        btnProcess = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        Label4 = New Label()
        Label3 = New Label()
        dtpDateTo = New DateTimePicker()
        dtpDateFrom = New DateTimePicker()
        Label1 = New Label()
        cmbDestination = New ComboBox()
        Label2 = New Label()
        cmbAnalysis = New ComboBox()
        chkTabLVJ = New CheckBox()
        cmbAnalyte = New ComboBox()
        Label5 = New Label()
        Label6 = New Label()
        Label7 = New Label()
        GroupBox1 = New GroupBox()
        Label8 = New Label()
        Label9 = New Label()
        Label10 = New Label()
        Label11 = New Label()
        Label12 = New Label()
        Label13 = New Label()
        lstLevels = New CheckedListBox()
        ToolStrip1.SuspendLayout()
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
        ToolStrip1.Size = New Size(963, 34)
        ToolStrip1.TabIndex = 6
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
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(278, 37)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(35, 35)
        Label4.TabIndex = 17
        Label4.Text = "To"
        Label4.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(10, 37)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(52, 35)
        Label3.TabIndex = 16
        Label3.Text = "From"
        Label3.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' dtpDateTo
        ' 
        dtpDateTo.Format = DateTimePickerFormat.Short
        dtpDateTo.Location = New Point(323, 37)
        dtpDateTo.Margin = New Padding(5, 6, 5, 6)
        dtpDateTo.Name = "dtpDateTo"
        dtpDateTo.Size = New Size(184, 31)
        dtpDateTo.TabIndex = 3
        ' 
        ' dtpDateFrom
        ' 
        dtpDateFrom.Format = DateTimePickerFormat.Short
        dtpDateFrom.Location = New Point(72, 37)
        dtpDateFrom.Margin = New Padding(5, 6, 5, 6)
        dtpDateFrom.Name = "dtpDateFrom"
        dtpDateFrom.Size = New Size(189, 31)
        dtpDateFrom.TabIndex = 2
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(47, 694)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(178, 35)
        Label1.TabIndex = 18
        Label1.Text = "Report Destination"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' cmbDestination
        ' 
        cmbDestination.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDestination.FormattingEnabled = True
        cmbDestination.Items.AddRange(New Object() {"Printer", "Screen"})
        cmbDestination.Location = New Point(47, 735)
        cmbDestination.Margin = New Padding(5, 6, 5, 6)
        cmbDestination.Name = "cmbDestination"
        cmbDestination.Size = New Size(256, 33)
        cmbDestination.TabIndex = 7
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(57, 263)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(177, 35)
        Label2.TabIndex = 20
        Label2.Text = "Analysis"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' cmbAnalysis
        ' 
        cmbAnalysis.DropDownStyle = ComboBoxStyle.DropDownList
        cmbAnalysis.FormattingEnabled = True
        cmbAnalysis.Items.AddRange(New Object() {"Printer", "Screen"})
        cmbAnalysis.Location = New Point(47, 304)
        cmbAnalysis.Margin = New Padding(5, 6, 5, 6)
        cmbAnalysis.Name = "cmbAnalysis"
        cmbAnalysis.Size = New Size(879, 33)
        cmbAnalysis.TabIndex = 4
        ' 
        ' chkTabLVJ
        ' 
        chkTabLVJ.Appearance = Appearance.Button
        chkTabLVJ.Location = New Point(47, 183)
        chkTabLVJ.Margin = New Padding(5, 6, 5, 6)
        chkTabLVJ.Name = "chkTabLVJ"
        chkTabLVJ.Size = New Size(160, 62)
        chkTabLVJ.TabIndex = 1
        chkTabLVJ.Text = "Tabular"
        chkTabLVJ.TextAlign = ContentAlignment.MiddleCenter
        chkTabLVJ.UseVisualStyleBackColor = True
        ' 
        ' cmbAnalyte
        ' 
        cmbAnalyte.DropDownStyle = ComboBoxStyle.DropDownList
        cmbAnalyte.Enabled = False
        cmbAnalyte.FormattingEnabled = True
        cmbAnalyte.Location = New Point(47, 413)
        cmbAnalyte.Margin = New Padding(5, 6, 5, 6)
        cmbAnalyte.Name = "cmbAnalyte"
        cmbAnalyte.Size = New Size(879, 33)
        cmbAnalyte.TabIndex = 5
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.DarkBlue
        Label5.Location = New Point(57, 373)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(160, 35)
        Label5.TabIndex = 23
        Label5.Text = "Analyte"
        Label5.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.DarkBlue
        Label6.Location = New Point(57, 487)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(160, 35)
        Label6.TabIndex = 25
        Label6.Text = "Control Levels"
        Label6.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label7.ForeColor = Color.Maroon
        Label7.Location = New Point(18, 198)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(24, 25)
        Label7.TabIndex = 26
        Label7.Text = "1"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(dtpDateFrom)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(dtpDateTo)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Location = New Point(243, 160)
        GroupBox1.Margin = New Padding(5, 6, 5, 6)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(5, 6, 5, 6)
        GroupBox1.Size = New Size(520, 98)
        GroupBox1.TabIndex = 27
        GroupBox1.TabStop = False
        GroupBox1.Text = "Date Range"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label8.ForeColor = Color.Maroon
        Label8.Location = New Point(217, 198)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(24, 25)
        Label8.TabIndex = 28
        Label8.Text = "2"
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label9.ForeColor = Color.Maroon
        Label9.Location = New Point(18, 306)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(24, 25)
        Label9.TabIndex = 29
        Label9.Text = "3"
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label10.ForeColor = Color.Maroon
        Label10.Location = New Point(18, 415)
        Label10.Margin = New Padding(5, 0, 5, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(24, 25)
        Label10.TabIndex = 30
        Label10.Text = "4"
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label11.ForeColor = Color.Maroon
        Label11.Location = New Point(18, 529)
        Label11.Margin = New Padding(5, 0, 5, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(24, 25)
        Label11.TabIndex = 31
        Label11.Text = "5"
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label12.ForeColor = Color.Maroon
        Label12.Location = New Point(10, 737)
        Label12.Margin = New Padding(5, 0, 5, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(24, 25)
        Label12.TabIndex = 32
        Label12.Text = "6"
        ' 
        ' Label13
        ' 
        Label13.ForeColor = Color.Maroon
        Label13.Location = New Point(50, 71)
        Label13.Margin = New Padding(5, 0, 5, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(680, 83)
        Label13.TabIndex = 33
        Label13.Text = "Make your selection following the numbers (1 - 6) as the higher numbered field options get populated, depending upon the selection of the previous numbered field."
        Label13.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lstLevels
        ' 
        lstLevels.FormattingEnabled = True
        lstLevels.Location = New Point(50, 527)
        lstLevels.Margin = New Padding(5, 6, 5, 6)
        lstLevels.Name = "lstLevels"
        lstLevels.Size = New Size(874, 144)
        lstLevels.TabIndex = 34
        ' 
        ' frmPrintQCReports
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(963, 798)
        Controls.Add(lstLevels)
        Controls.Add(Label13)
        Controls.Add(Label12)
        Controls.Add(Label11)
        Controls.Add(Label10)
        Controls.Add(Label9)
        Controls.Add(Label8)
        Controls.Add(GroupBox1)
        Controls.Add(Label7)
        Controls.Add(Label6)
        Controls.Add(Label5)
        Controls.Add(cmbAnalyte)
        Controls.Add(chkTabLVJ)
        Controls.Add(Label2)
        Controls.Add(cmbAnalysis)
        Controls.Add(Label1)
        Controls.Add(cmbDestination)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmPrintQCReports"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Print QC Reports"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        GroupBox1.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnProcess As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbDestination As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbAnalysis As System.Windows.Forms.ComboBox
    Friend WithEvents chkTabLVJ As System.Windows.Forms.CheckBox
    Friend WithEvents cmbAnalyte As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lstLevels As System.Windows.Forms.CheckedListBox

End Class
