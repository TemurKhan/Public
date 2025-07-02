<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintWorksheets
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintWorksheets))
        ToolStrip1 = New ToolStrip()
        btnPrint = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        grpDates = New GroupBox()
        lblClearDates = New Label()
        dtpDateTo = New DateTimePicker()
        Label2 = New Label()
        Label1 = New Label()
        dtpDateFrom = New DateTimePicker()
        Label8 = New Label()
        Label4 = New Label()
        txtCopies = New TextBox()
        cmbDestination = New ComboBox()
        GroupBox1 = New GroupBox()
        txtAccIDTo = New TextBox()
        txtAccIDFrom = New TextBox()
        Label3 = New Label()
        Label5 = New Label()
        lstWorksheets = New CheckedListBox()
        btnSelAll = New Button()
        btnDeselAll = New Button()
        cmbDept = New ComboBox()
        Label6 = New Label()
        cmbSheets = New ComboBox()
        Label7 = New Label()
        ToolStrip1.SuspendLayout()
        grpDates.SuspendLayout()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnPrint, ToolStripSeparator1, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(803, 34)
        ToolStrip1.TabIndex = 8
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnPrint
        ' 
        btnPrint.Enabled = False
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
        ' grpDates
        ' 
        grpDates.Controls.Add(lblClearDates)
        grpDates.Controls.Add(dtpDateTo)
        grpDates.Controls.Add(Label2)
        grpDates.Controls.Add(Label1)
        grpDates.Controls.Add(dtpDateFrom)
        grpDates.Location = New Point(20, 71)
        grpDates.Margin = New Padding(5, 6, 5, 6)
        grpDates.Name = "grpDates"
        grpDates.Padding = New Padding(5, 6, 5, 6)
        grpDates.Size = New Size(282, 150)
        grpDates.TabIndex = 9
        grpDates.TabStop = False
        grpDates.Text = "Acc Dates"
        ' 
        ' lblClearDates
        ' 
        lblClearDates.BackColor = SystemColors.Control
        lblClearDates.Image = My.Resources.Resources.Eraser
        lblClearDates.Location = New Point(228, 85)
        lblClearDates.Name = "lblClearDates"
        lblClearDates.Size = New Size(33, 46)
        lblClearDates.TabIndex = 96
        lblClearDates.Text = "      "
        ' 
        ' dtpDateTo
        ' 
        dtpDateTo.CustomFormat = " "
        dtpDateTo.Format = DateTimePickerFormat.Custom
        dtpDateTo.Location = New Point(80, 88)
        dtpDateTo.Margin = New Padding(3, 4, 3, 4)
        dtpDateTo.Name = "dtpDateTo"
        dtpDateTo.Size = New Size(129, 31)
        dtpDateTo.TabIndex = 2
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.Magenta
        Label2.Location = New Point(35, 96)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(35, 25)
        Label2.TabIndex = 2
        Label2.Text = "To"
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.Red
        Label1.Location = New Point(13, 42)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(57, 25)
        Label1.TabIndex = 0
        Label1.Text = "From"
        Label1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' dtpDateFrom
        ' 
        dtpDateFrom.CustomFormat = " "
        dtpDateFrom.Format = DateTimePickerFormat.Custom
        dtpDateFrom.Location = New Point(80, 35)
        dtpDateFrom.Margin = New Padding(3, 4, 3, 4)
        dtpDateFrom.Name = "dtpDateFrom"
        dtpDateFrom.Size = New Size(129, 31)
        dtpDateFrom.TabIndex = 1
        ' 
        ' Label8
        ' 
        Label8.ForeColor = Color.Red
        Label8.Location = New Point(20, 552)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(108, 25)
        Label8.TabIndex = 21
        Label8.Text = "Destination"
        Label8.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.Red
        Label4.Location = New Point(248, 552)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(93, 25)
        Label4.TabIndex = 20
        Label4.Text = "Copies"
        Label4.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtCopies
        ' 
        txtCopies.Location = New Point(248, 583)
        txtCopies.Margin = New Padding(5, 6, 5, 6)
        txtCopies.MaxLength = 1
        txtCopies.Name = "txtCopies"
        txtCopies.Size = New Size(91, 31)
        txtCopies.TabIndex = 8
        txtCopies.Text = "1"
        txtCopies.TextAlign = HorizontalAlignment.Center
        ' 
        ' cmbDestination
        ' 
        cmbDestination.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDestination.FormattingEnabled = True
        cmbDestination.Items.AddRange(New Object() {"Printer", "Screen"})
        cmbDestination.Location = New Point(20, 583)
        cmbDestination.Margin = New Padding(5, 6, 5, 6)
        cmbDestination.Name = "cmbDestination"
        cmbDestination.Size = New Size(214, 33)
        cmbDestination.TabIndex = 9
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(txtAccIDTo)
        GroupBox1.Controls.Add(txtAccIDFrom)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(Label5)
        GroupBox1.Location = New Point(20, 233)
        GroupBox1.Margin = New Padding(5, 6, 5, 6)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(5, 6, 5, 6)
        GroupBox1.Size = New Size(282, 150)
        GroupBox1.TabIndex = 23
        GroupBox1.TabStop = False
        GroupBox1.Text = "Accession Numbers"
        ' 
        ' txtAccIDTo
        ' 
        txtAccIDTo.Location = New Point(80, 90)
        txtAccIDTo.Margin = New Padding(5, 6, 5, 6)
        txtAccIDTo.MaxLength = 12
        txtAccIDTo.Name = "txtAccIDTo"
        txtAccIDTo.Size = New Size(134, 31)
        txtAccIDTo.TabIndex = 4
        txtAccIDTo.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtAccIDFrom
        ' 
        txtAccIDFrom.Location = New Point(80, 37)
        txtAccIDFrom.Margin = New Padding(5, 6, 5, 6)
        txtAccIDFrom.MaxLength = 12
        txtAccIDFrom.Name = "txtAccIDFrom"
        txtAccIDFrom.Size = New Size(134, 31)
        txtAccIDFrom.TabIndex = 3
        txtAccIDFrom.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.Magenta
        Label3.Location = New Point(35, 96)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(35, 25)
        Label3.TabIndex = 2
        Label3.Text = "To"
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.Red
        Label5.Location = New Point(13, 42)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(57, 25)
        Label5.TabIndex = 0
        Label5.Text = "From"
        Label5.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lstWorksheets
        ' 
        lstWorksheets.FormattingEnabled = True
        lstWorksheets.Location = New Point(330, 160)
        lstWorksheets.Margin = New Padding(5, 6, 5, 6)
        lstWorksheets.Name = "lstWorksheets"
        lstWorksheets.Size = New Size(451, 368)
        lstWorksheets.TabIndex = 7
        ' 
        ' btnSelAll
        ' 
        btnSelAll.Image = CType(resources.GetObject("btnSelAll.Image"), Image)
        btnSelAll.Location = New Point(368, 569)
        btnSelAll.Margin = New Padding(5, 6, 5, 6)
        btnSelAll.Name = "btnSelAll"
        btnSelAll.Size = New Size(158, 63)
        btnSelAll.TabIndex = 10
        btnSelAll.TabStop = False
        btnSelAll.Text = "Select All"
        btnSelAll.TextAlign = ContentAlignment.MiddleRight
        btnSelAll.TextImageRelation = TextImageRelation.ImageBeforeText
        btnSelAll.UseVisualStyleBackColor = True
        ' 
        ' btnDeselAll
        ' 
        btnDeselAll.Image = CType(resources.GetObject("btnDeselAll.Image"), Image)
        btnDeselAll.Location = New Point(577, 569)
        btnDeselAll.Margin = New Padding(5, 6, 5, 6)
        btnDeselAll.Name = "btnDeselAll"
        btnDeselAll.Size = New Size(158, 63)
        btnDeselAll.TabIndex = 11
        btnDeselAll.TabStop = False
        btnDeselAll.Text = "Deselect All"
        btnDeselAll.TextAlign = ContentAlignment.MiddleRight
        btnDeselAll.TextImageRelation = TextImageRelation.ImageBeforeText
        btnDeselAll.UseVisualStyleBackColor = True
        ' 
        ' cmbDept
        ' 
        cmbDept.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDept.FormattingEnabled = True
        cmbDept.Location = New Point(20, 450)
        cmbDept.Margin = New Padding(5, 6, 5, 6)
        cmbDept.Name = "cmbDept"
        cmbDept.Size = New Size(279, 33)
        cmbDept.TabIndex = 25
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.DarkBlue
        Label6.Location = New Point(33, 419)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(132, 25)
        Label6.TabIndex = 26
        Label6.Text = "Department"
        ' 
        ' cmbSheets
        ' 
        cmbSheets.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSheets.FormattingEnabled = True
        cmbSheets.Items.AddRange(New Object() {"Equipment Worksheet", "Test Worksheet Inhouse", "Test Worksheet Outsourced", "Pending Sheet", "Result Sheet"})
        cmbSheets.Location = New Point(330, 108)
        cmbSheets.Margin = New Padding(5, 6, 5, 6)
        cmbSheets.Name = "cmbSheets"
        cmbSheets.Size = New Size(449, 33)
        cmbSheets.TabIndex = 27
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.DarkBlue
        Label7.Location = New Point(342, 71)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(132, 25)
        Label7.TabIndex = 28
        Label7.Text = "Sheet Type"
        ' 
        ' frmPrintWorksheets
        ' 
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(803, 658)
        Controls.Add(Label7)
        Controls.Add(cmbSheets)
        Controls.Add(Label6)
        Controls.Add(cmbDept)
        Controls.Add(btnDeselAll)
        Controls.Add(btnSelAll)
        Controls.Add(lstWorksheets)
        Controls.Add(GroupBox1)
        Controls.Add(Label8)
        Controls.Add(Label4)
        Controls.Add(txtCopies)
        Controls.Add(cmbDestination)
        Controls.Add(grpDates)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmPrintWorksheets"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Print Worksheets"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        grpDates.ResumeLayout(False)
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
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
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCopies As System.Windows.Forms.TextBox
    Friend WithEvents cmbDestination As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lstWorksheets As System.Windows.Forms.CheckedListBox
    Friend WithEvents txtAccIDTo As System.Windows.Forms.TextBox
    Friend WithEvents txtAccIDFrom As System.Windows.Forms.TextBox
    Friend WithEvents btnSelAll As System.Windows.Forms.Button
    Friend WithEvents btnDeselAll As System.Windows.Forms.Button
    Friend WithEvents cmbDept As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbSheets As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblClearDates As Label
    Friend WithEvents dtpDateTo As DateTimePicker
    Friend WithEvents dtpDateFrom As DateTimePicker
End Class
