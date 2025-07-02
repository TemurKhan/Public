<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRefluxResultReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRefluxResultReport))
        grpDateRange = New GroupBox()
        Label4 = New Label()
        Label3 = New Label()
        dtpDateTo = New DateTimePicker()
        dtpDateFrom = New DateTimePicker()
        grpAccRange = New GroupBox()
        Label6 = New Label()
        Label5 = New Label()
        txtAccTo = New TextBox()
        txtAccFrom = New TextBox()
        chkDateRange = New CheckBox()
        chkAccRange = New CheckBox()
        btnProcess = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        ToolStrip1 = New ToolStrip()
        grpDateRange.SuspendLayout()
        grpAccRange.SuspendLayout()
        ToolStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' grpDateRange
        ' 
        grpDateRange.Controls.Add(Label4)
        grpDateRange.Controls.Add(Label3)
        grpDateRange.Controls.Add(dtpDateTo)
        grpDateRange.Controls.Add(dtpDateFrom)
        grpDateRange.Enabled = False
        grpDateRange.Location = New Point(55, 256)
        grpDateRange.Margin = New Padding(5, 6, 5, 6)
        grpDateRange.Name = "grpDateRange"
        grpDateRange.Padding = New Padding(5, 6, 5, 6)
        grpDateRange.Size = New Size(418, 148)
        grpDateRange.TabIndex = 51
        grpDateRange.TabStop = False
        grpDateRange.Text = "Date Range"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(302, 40)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(30, 25)
        Label4.TabIndex = 17
        Label4.Text = "To"
        Label4.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(90, 40)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(54, 25)
        Label3.TabIndex = 16
        Label3.Text = "From"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' dtpDateTo
        ' 
        dtpDateTo.Format = DateTimePickerFormat.Short
        dtpDateTo.Location = New Point(238, 81)
        dtpDateTo.Margin = New Padding(5, 6, 5, 6)
        dtpDateTo.Name = "dtpDateTo"
        dtpDateTo.Size = New Size(157, 31)
        dtpDateTo.TabIndex = 6
        ' 
        ' dtpDateFrom
        ' 
        dtpDateFrom.Format = DateTimePickerFormat.Short
        dtpDateFrom.Location = New Point(35, 81)
        dtpDateFrom.Margin = New Padding(5, 6, 5, 6)
        dtpDateFrom.Name = "dtpDateFrom"
        dtpDateFrom.Size = New Size(157, 31)
        dtpDateFrom.TabIndex = 5
        ' 
        ' grpAccRange
        ' 
        grpAccRange.Controls.Add(Label6)
        grpAccRange.Controls.Add(Label5)
        grpAccRange.Controls.Add(txtAccTo)
        grpAccRange.Controls.Add(txtAccFrom)
        grpAccRange.Location = New Point(58, 79)
        grpAccRange.Margin = New Padding(5, 6, 5, 6)
        grpAccRange.Name = "grpAccRange"
        grpAccRange.Padding = New Padding(5, 6, 5, 6)
        grpAccRange.Size = New Size(418, 148)
        grpAccRange.TabIndex = 50
        grpAccRange.TabStop = False
        grpAccRange.Text = "Accession Range"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.ForeColor = Color.DarkBlue
        Label6.Location = New Point(298, 44)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(30, 25)
        Label6.TabIndex = 17
        Label6.Text = "To"
        Label6.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.ForeColor = Color.DarkBlue
        Label5.Location = New Point(87, 44)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(54, 25)
        Label5.TabIndex = 16
        Label5.Text = "From"
        Label5.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtAccTo
        ' 
        txtAccTo.Location = New Point(235, 81)
        txtAccTo.Margin = New Padding(5, 6, 5, 6)
        txtAccTo.MaxLength = 15
        txtAccTo.Name = "txtAccTo"
        txtAccTo.Size = New Size(157, 31)
        txtAccTo.TabIndex = 2
        txtAccTo.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtAccFrom
        ' 
        txtAccFrom.Location = New Point(32, 81)
        txtAccFrom.Margin = New Padding(5, 6, 5, 6)
        txtAccFrom.MaxLength = 15
        txtAccFrom.Name = "txtAccFrom"
        txtAccFrom.Size = New Size(157, 31)
        txtAccFrom.TabIndex = 1
        txtAccFrom.TextAlign = HorizontalAlignment.Center
        ' 
        ' chkDateRange
        ' 
        chkDateRange.AutoSize = True
        chkDateRange.Location = New Point(23, 304)
        chkDateRange.Margin = New Padding(5, 6, 5, 6)
        chkDateRange.Name = "chkDateRange"
        chkDateRange.Size = New Size(22, 21)
        chkDateRange.TabIndex = 49
        chkDateRange.UseVisualStyleBackColor = True
        ' 
        ' chkAccRange
        ' 
        chkAccRange.AutoSize = True
        chkAccRange.Checked = True
        chkAccRange.CheckState = CheckState.Checked
        chkAccRange.Location = New Point(23, 125)
        chkAccRange.Margin = New Padding(5, 6, 5, 6)
        chkAccRange.Name = "chkAccRange"
        chkAccRange.Size = New Size(22, 21)
        chkAccRange.TabIndex = 46
        chkAccRange.UseVisualStyleBackColor = True
        ' 
        ' btnProcess
        ' 
        btnProcess.ForeColor = Color.DarkBlue
        btnProcess.Image = CType(resources.GetObject("btnProcess.Image"), Image)
        btnProcess.ImageTransparentColor = Color.Magenta
        btnProcess.Name = "btnProcess"
        btnProcess.Size = New Size(96, 29)
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
        btnCancel.Size = New Size(79, 29)
        btnCancel.Text = "&Close"
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
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(20, 20)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnProcess, ToolStripSeparator1, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(510, 34)
        ToolStrip1.TabIndex = 48
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' frmRefluxResultReport
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(510, 440)
        Controls.Add(grpDateRange)
        Controls.Add(grpAccRange)
        Controls.Add(ToolStrip1)
        Controls.Add(chkDateRange)
        Controls.Add(chkAccRange)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmRefluxResultReport"
        Text = "Reflex Results Report"
        grpDateRange.ResumeLayout(False)
        grpDateRange.PerformLayout()
        grpAccRange.ResumeLayout(False)
        grpAccRange.PerformLayout()
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents grpDateRange As GroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents dtpDateTo As DateTimePicker
    Friend WithEvents dtpDateFrom As DateTimePicker
    Friend WithEvents grpAccRange As GroupBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txtAccTo As TextBox
    Friend WithEvents txtAccFrom As TextBox
    Friend WithEvents chkDateRange As CheckBox
    Friend WithEvents chkAccRange As CheckBox
    Friend WithEvents btnProcess As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents btnCancel As ToolStripButton
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents btnHelp As ToolStripButton
    Friend WithEvents ToolStrip1 As ToolStrip
End Class
