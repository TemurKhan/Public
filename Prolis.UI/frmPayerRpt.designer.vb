<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPayerRpt
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPayerRpt))
        ToolStrip1 = New ToolStrip()
        btnPrint = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        Label1 = New Label()
        cmbReports = New ComboBox()
        cmbDestination = New ComboBox()
        Label2 = New Label()
        ToolStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnPrint, ToolStripSeparator1, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(480, 34)
        ToolStrip1.TabIndex = 12
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnPrint
        ' 
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
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(43, 87)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(142, 25)
        Label1.TabIndex = 16
        Label1.Text = "Select Report"
        ' 
        ' cmbReports
        ' 
        cmbReports.DropDownStyle = ComboBoxStyle.DropDownList
        cmbReports.FormattingEnabled = True
        cmbReports.Items.AddRange(New Object() {"Payers Report"})
        cmbReports.Location = New Point(20, 117)
        cmbReports.Margin = New Padding(5, 6, 5, 6)
        cmbReports.Name = "cmbReports"
        cmbReports.Size = New Size(419, 33)
        cmbReports.TabIndex = 15
        ' 
        ' cmbDestination
        ' 
        cmbDestination.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDestination.FormattingEnabled = True
        cmbDestination.Items.AddRange(New Object() {"Printer", "Screen"})
        cmbDestination.Location = New Point(20, 225)
        cmbDestination.Margin = New Padding(5, 6, 5, 6)
        cmbDestination.Name = "cmbDestination"
        cmbDestination.Size = New Size(197, 33)
        cmbDestination.TabIndex = 17
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(43, 194)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(167, 25)
        Label2.TabIndex = 18
        Label2.Text = "Output Destination"
        ' 
        ' frmPayerRpt
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(480, 288)
        Controls.Add(Label2)
        Controls.Add(cmbDestination)
        Controls.Add(Label1)
        Controls.Add(cmbReports)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmPayerRpt"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Payer Report"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbReports As System.Windows.Forms.ComboBox
    Friend WithEvents cmbDestination As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class
