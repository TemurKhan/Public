<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintPickUp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintPickUp))
        ToolStrip1 = New ToolStrip()
        btnProcess = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        dtpDate = New DateTimePicker()
        Label1 = New Label()
        Label2 = New Label()
        lstCouriers = New CheckedListBox()
        btnSelAll = New Button()
        btnDeSelAll = New Button()
        cmbDestination = New ComboBox()
        Label3 = New Label()
        ToolStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnProcess, ToolStripSeparator1, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(595, 34)
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
        ' dtpDate
        ' 
        dtpDate.Format = DateTimePickerFormat.Short
        dtpDate.Location = New Point(20, 115)
        dtpDate.Margin = New Padding(5, 6, 5, 6)
        dtpDate.Name = "dtpDate"
        dtpDate.Size = New Size(189, 31)
        dtpDate.TabIndex = 7
        ' 
        ' Label1
        ' 
        Label1.Location = New Point(20, 79)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(168, 31)
        Label1.TabIndex = 8
        Label1.Text = "Dated"
        ' 
        ' Label2
        ' 
        Label2.Location = New Point(217, 79)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(163, 31)
        Label2.TabIndex = 9
        Label2.Text = "Courier"
        ' 
        ' lstCouriers
        ' 
        lstCouriers.FormattingEnabled = True
        lstCouriers.Location = New Point(222, 115)
        lstCouriers.Margin = New Padding(5, 6, 5, 6)
        lstCouriers.Name = "lstCouriers"
        lstCouriers.Size = New Size(351, 200)
        lstCouriers.TabIndex = 10
        ' 
        ' btnSelAll
        ' 
        btnSelAll.Location = New Point(22, 188)
        btnSelAll.Margin = New Padding(5, 6, 5, 6)
        btnSelAll.Name = "btnSelAll"
        btnSelAll.Size = New Size(190, 46)
        btnSelAll.TabIndex = 11
        btnSelAll.Text = "Select All"
        btnSelAll.UseVisualStyleBackColor = True
        ' 
        ' btnDeSelAll
        ' 
        btnDeSelAll.Location = New Point(20, 279)
        btnDeSelAll.Margin = New Padding(5, 6, 5, 6)
        btnDeSelAll.Name = "btnDeSelAll"
        btnDeSelAll.Size = New Size(190, 46)
        btnDeSelAll.TabIndex = 12
        btnDeSelAll.Text = "Deselect All"
        btnDeSelAll.UseVisualStyleBackColor = True
        ' 
        ' cmbDestination
        ' 
        cmbDestination.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDestination.FormattingEnabled = True
        cmbDestination.Items.AddRange(New Object() {"Printer", "Screen"})
        cmbDestination.Location = New Point(222, 354)
        cmbDestination.Margin = New Padding(5, 6, 5, 6)
        cmbDestination.Name = "cmbDestination"
        cmbDestination.Size = New Size(349, 33)
        cmbDestination.TabIndex = 13
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(47, 360)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(163, 31)
        Label3.TabIndex = 14
        Label3.Text = "Destination"
        Label3.TextAlign = ContentAlignment.TopRight
        ' 
        ' frmPrintPickUp
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(595, 431)
        Controls.Add(Label3)
        Controls.Add(cmbDestination)
        Controls.Add(btnDeSelAll)
        Controls.Add(btnSelAll)
        Controls.Add(lstCouriers)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(dtpDate)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmPrintPickUp"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Print Pickup Report"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnProcess As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lstCouriers As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnSelAll As System.Windows.Forms.Button
    Friend WithEvents btnDeSelAll As System.Windows.Forms.Button
    Friend WithEvents cmbDestination As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label

End Class
