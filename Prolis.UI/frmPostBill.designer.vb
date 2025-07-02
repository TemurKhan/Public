<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPostBill
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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPostBill))
        ToolStrip1 = New ToolStrip()
        btnPrint = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        btnTarget = New Button()
        btnSel = New Button()
        btnDesel = New Button()
        lstTargets = New CheckedListBox()
        lblTo = New Label()
        txtAccTo = New TextBox()
        lblFrom = New Label()
        Label2 = New Label()
        txtAccFrom = New TextBox()
        Label1 = New Label()
        cmbBillType = New ComboBox()
        Label9 = New Label()
        ToolTip1 = New ToolTip(components)
        cmbDateType = New ComboBox()
        Label3 = New Label()
        chkAccInv = New CheckBox()
        dtpDateTo = New DateTimePicker()
        lblClearDates = New Label()
        dtpDateFrom = New DateTimePicker()
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
        ToolStrip1.Size = New Size(690, 34)
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
        ' btnTarget
        ' 
        btnTarget.ForeColor = Color.DarkBlue
        btnTarget.Image = CType(resources.GetObject("btnTarget.Image"), Image)
        btnTarget.Location = New Point(522, 181)
        btnTarget.Margin = New Padding(5, 6, 5, 6)
        btnTarget.Name = "btnTarget"
        btnTarget.Size = New Size(107, 58)
        btnTarget.TabIndex = 7
        btnTarget.TextAlign = ContentAlignment.MiddleRight
        btnTarget.TextImageRelation = TextImageRelation.ImageBeforeText
        btnTarget.UseVisualStyleBackColor = True
        ' 
        ' btnSel
        ' 
        btnSel.ForeColor = Color.DarkBlue
        btnSel.Image = CType(resources.GetObject("btnSel.Image"), Image)
        btnSel.Location = New Point(570, 500)
        btnSel.Margin = New Padding(5, 6, 5, 6)
        btnSel.Name = "btnSel"
        btnSel.Size = New Size(58, 58)
        btnSel.TabIndex = 10
        btnSel.TextAlign = ContentAlignment.MiddleRight
        btnSel.TextImageRelation = TextImageRelation.ImageBeforeText
        btnSel.UseVisualStyleBackColor = True
        ' 
        ' btnDesel
        ' 
        btnDesel.ForeColor = Color.DarkBlue
        btnDesel.Image = CType(resources.GetObject("btnDesel.Image"), Image)
        btnDesel.Location = New Point(570, 431)
        btnDesel.Margin = New Padding(5, 6, 5, 6)
        btnDesel.Name = "btnDesel"
        btnDesel.Size = New Size(58, 58)
        btnDesel.TabIndex = 9
        btnDesel.TextAlign = ContentAlignment.MiddleRight
        btnDesel.TextImageRelation = TextImageRelation.ImageBeforeText
        btnDesel.UseVisualStyleBackColor = True
        ' 
        ' lstTargets
        ' 
        lstTargets.FormattingEnabled = True
        lstTargets.Location = New Point(15, 262)
        lstTargets.Margin = New Padding(5, 6, 5, 6)
        lstTargets.Name = "lstTargets"
        lstTargets.Size = New Size(542, 284)
        lstTargets.TabIndex = 8
        ' 
        ' lblTo
        ' 
        lblTo.ForeColor = Color.DarkBlue
        lblTo.Location = New Point(355, 163)
        lblTo.Margin = New Padding(5, 0, 5, 0)
        lblTo.Name = "lblTo"
        lblTo.Size = New Size(138, 25)
        lblTo.TabIndex = 9
        lblTo.Text = "ACC To"
        ' 
        ' txtAccTo
        ' 
        txtAccTo.Location = New Point(343, 200)
        txtAccTo.Margin = New Padding(5, 6, 5, 6)
        txtAccTo.MaxLength = 10
        txtAccTo.Name = "txtAccTo"
        txtAccTo.Size = New Size(166, 31)
        txtAccTo.TabIndex = 5
        txtAccTo.TextAlign = HorizontalAlignment.Center
        ' 
        ' lblFrom
        ' 
        lblFrom.ForeColor = Color.DarkBlue
        lblFrom.Location = New Point(180, 163)
        lblFrom.Margin = New Padding(5, 0, 5, 0)
        lblFrom.Name = "lblFrom"
        lblFrom.Size = New Size(138, 25)
        lblFrom.TabIndex = 7
        lblFrom.Text = "ACC From"
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(498, 69)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(113, 25)
        Label2.TabIndex = 4
        Label2.Text = "Date To"
        ' 
        ' txtAccFrom
        ' 
        txtAccFrom.Location = New Point(165, 200)
        txtAccFrom.Margin = New Padding(5, 6, 5, 6)
        txtAccFrom.MaxLength = 10
        txtAccFrom.Name = "txtAccFrom"
        txtAccFrom.Size = New Size(166, 31)
        txtAccFrom.TabIndex = 4
        txtAccFrom.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(357, 69)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(103, 25)
        Label1.TabIndex = 0
        Label1.Text = "Date From"
        ' 
        ' cmbBillType
        ' 
        cmbBillType.DropDownStyle = ComboBoxStyle.DropDownList
        cmbBillType.FormattingEnabled = True
        cmbBillType.Items.AddRange(New Object() {"Client", "Insurance", "Patient"})
        cmbBillType.Location = New Point(20, 100)
        cmbBillType.Margin = New Padding(5, 6, 5, 6)
        cmbBillType.Name = "cmbBillType"
        cmbBillType.Size = New Size(136, 33)
        cmbBillType.TabIndex = 1
        ' 
        ' Label9
        ' 
        Label9.ForeColor = Color.DarkBlue
        Label9.Location = New Point(20, 69)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(130, 25)
        Label9.TabIndex = 17
        Label9.Text = "Billing Type"
        ' 
        ' cmbDateType
        ' 
        cmbDateType.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDateType.FormattingEnabled = True
        cmbDateType.Location = New Point(168, 100)
        cmbDateType.Margin = New Padding(5, 6, 5, 6)
        cmbDateType.Name = "cmbDateType"
        cmbDateType.Size = New Size(179, 33)
        cmbDateType.TabIndex = 18
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(180, 69)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(103, 25)
        Label3.TabIndex = 19
        Label3.Text = "Date Type"
        ' 
        ' chkAccInv
        ' 
        chkAccInv.Appearance = Appearance.Button
        chkAccInv.Location = New Point(20, 194)
        chkAccInv.Margin = New Padding(5, 6, 5, 6)
        chkAccInv.Name = "chkAccInv"
        chkAccInv.Size = New Size(138, 46)
        chkAccInv.TabIndex = 20
        chkAccInv.Text = "Accession"
        chkAccInv.TextAlign = ContentAlignment.MiddleCenter
        chkAccInv.UseVisualStyleBackColor = True
        ' 
        ' dtpDateTo
        ' 
        dtpDateTo.CustomFormat = " "
        dtpDateTo.Format = DateTimePickerFormat.Custom
        dtpDateTo.Location = New Point(503, 100)
        dtpDateTo.Margin = New Padding(3, 4, 3, 4)
        dtpDateTo.Name = "dtpDateTo"
        dtpDateTo.Size = New Size(129, 31)
        dtpDateTo.TabIndex = 95
        ' 
        ' lblClearDates
        ' 
        lblClearDates.BackColor = SystemColors.Control
        lblClearDates.Image = My.Resources.Resources.Eraser
        lblClearDates.Location = New Point(642, 94)
        lblClearDates.Name = "lblClearDates"
        lblClearDates.Size = New Size(33, 46)
        lblClearDates.TabIndex = 96
        lblClearDates.Text = "      "
        ' 
        ' dtpDateFrom
        ' 
        dtpDateFrom.CustomFormat = " "
        dtpDateFrom.Format = DateTimePickerFormat.Custom
        dtpDateFrom.Location = New Point(362, 100)
        dtpDateFrom.Margin = New Padding(3, 4, 3, 4)
        dtpDateFrom.Name = "dtpDateFrom"
        dtpDateFrom.Size = New Size(129, 31)
        dtpDateFrom.TabIndex = 94
        ' 
        ' frmPostBill
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(690, 579)
        Controls.Add(dtpDateTo)
        Controls.Add(lblClearDates)
        Controls.Add(dtpDateFrom)
        Controls.Add(chkAccInv)
        Controls.Add(Label3)
        Controls.Add(cmbDateType)
        Controls.Add(btnTarget)
        Controls.Add(btnSel)
        Controls.Add(ToolStrip1)
        Controls.Add(btnDesel)
        Controls.Add(lstTargets)
        Controls.Add(Label9)
        Controls.Add(lblTo)
        Controls.Add(cmbBillType)
        Controls.Add(txtAccTo)
        Controls.Add(Label1)
        Controls.Add(lblFrom)
        Controls.Add(txtAccFrom)
        Controls.Add(Label2)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmPostBill"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Post Billing Report"
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
    Friend WithEvents btnTarget As System.Windows.Forms.Button
    Friend WithEvents btnSel As System.Windows.Forms.Button
    Friend WithEvents btnDesel As System.Windows.Forms.Button
    Friend WithEvents lstTargets As System.Windows.Forms.CheckedListBox
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents txtAccTo As System.Windows.Forms.TextBox
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtAccFrom As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbBillType As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cmbDateType As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkAccInv As System.Windows.Forms.CheckBox
    Friend WithEvents dtpDateTo As DateTimePicker
    Friend WithEvents lblClearDates As Label
    Friend WithEvents dtpDateFrom As DateTimePicker
End Class
