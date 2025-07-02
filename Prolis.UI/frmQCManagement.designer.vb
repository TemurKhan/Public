<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmQCManagement
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmQCManagement))
        ToolStrip1 = New ToolStrip()
        btnNew = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnSave = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnDelete = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        txtTime = New MaskedTextBox()
        txtRunID = New TextBox()
        Label1 = New Label()
        btnQCRunLook = New Button()
        lbldate = New Label()
        dtpDate = New DateTimePicker()
        Label2 = New Label()
        Label4 = New Label()
        cmbShift = New ComboBox()
        cmbAnalysis = New ComboBox()
        Label5 = New Label()
        txtRunName = New TextBox()
        Label6 = New Label()
        txtControls = New TextBox()
        Label7 = New Label()
        Label8 = New Label()
        txtValidators = New TextBox()
        txtValidated = New TextBox()
        Label9 = New Label()
        txtCreatedBy = New TextBox()
        Label10 = New Label()
        txtEditedBy = New TextBox()
        Label11 = New Label()
        txtEditedOn = New TextBox()
        Label12 = New Label()
        Label3 = New Label()
        cmbEquipment = New ComboBox()
        chkAllInd = New CheckBox()
        ToolStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnNew, ToolStripSeparator1, btnSave, ToolStripSeparator2, btnDelete, ToolStripSeparator3, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(788, 34)
        ToolStrip1.TabIndex = 1
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnNew
        ' 
        btnNew.CheckOnClick = True
        btnNew.Image = CType(resources.GetObject("btnNew.Image"), Image)
        btnNew.ImageTransparentColor = Color.Magenta
        btnNew.Name = "btnNew"
        btnNew.Size = New Size(75, 29)
        btnNew.Text = "New"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
        ' 
        ' btnSave
        ' 
        btnSave.Enabled = False
        btnSave.Image = CType(resources.GetObject("btnSave.Image"), Image)
        btnSave.ImageTransparentColor = Color.Magenta
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(77, 29)
        btnSave.Text = "Save"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 34)
        ' 
        ' btnDelete
        ' 
        btnDelete.Enabled = False
        btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), Image)
        btnDelete.ImageTransparentColor = Color.Magenta
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(90, 29)
        btnDelete.Text = "Delete"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(6, 34)
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
        ' txtTime
        ' 
        txtTime.Location = New Point(458, 219)
        txtTime.Margin = New Padding(5, 6, 5, 6)
        txtTime.Mask = "00:00"
        txtTime.Name = "txtTime"
        txtTime.Size = New Size(107, 31)
        txtTime.TabIndex = 24
        txtTime.ValidatingType = GetType(Date)
        ' 
        ' txtRunID
        ' 
        txtRunID.Location = New Point(20, 219)
        txtRunID.Margin = New Padding(5, 6, 5, 6)
        txtRunID.MaxLength = 12
        txtRunID.Name = "txtRunID"
        txtRunID.Size = New Size(157, 31)
        txtRunID.TabIndex = 20
        txtRunID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(38, 187)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(125, 25)
        Label1.TabIndex = 16
        Label1.Text = "QC Run ID"
        ' 
        ' btnQCRunLook
        ' 
        btnQCRunLook.Enabled = False
        btnQCRunLook.Image = CType(resources.GetObject("btnQCRunLook.Image"), Image)
        btnQCRunLook.Location = New Point(190, 213)
        btnQCRunLook.Margin = New Padding(5, 6, 5, 6)
        btnQCRunLook.Name = "btnQCRunLook"
        btnQCRunLook.Size = New Size(50, 50)
        btnQCRunLook.TabIndex = 21
        btnQCRunLook.TabStop = False
        btnQCRunLook.UseVisualStyleBackColor = True
        ' 
        ' lbldate
        ' 
        lbldate.ForeColor = Color.DarkBlue
        lbldate.Location = New Point(250, 187)
        lbldate.Margin = New Padding(5, 0, 5, 0)
        lbldate.Name = "lbldate"
        lbldate.Size = New Size(198, 25)
        lbldate.TabIndex = 18
        lbldate.Text = "Dated"
        ' 
        ' dtpDate
        ' 
        dtpDate.Format = DateTimePickerFormat.Short
        dtpDate.Location = New Point(255, 219)
        dtpDate.Margin = New Padding(5, 6, 5, 6)
        dtpDate.Name = "dtpDate"
        dtpDate.Size = New Size(186, 31)
        dtpDate.TabIndex = 23
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(600, 187)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(132, 25)
        Label2.TabIndex = 17
        Label2.Text = "Shift"
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(458, 187)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(110, 25)
        Label4.TabIndex = 19
        Label4.Text = "Run Time"
        ' 
        ' cmbShift
        ' 
        cmbShift.DropDownStyle = ComboBoxStyle.DropDownList
        cmbShift.FormattingEnabled = True
        cmbShift.Items.AddRange(New Object() {"Morning (All Day)", "Afternoon", "Night"})
        cmbShift.Location = New Point(585, 219)
        cmbShift.Margin = New Padding(5, 6, 5, 6)
        cmbShift.Name = "cmbShift"
        cmbShift.Size = New Size(177, 33)
        cmbShift.TabIndex = 25
        ' 
        ' cmbAnalysis
        ' 
        cmbAnalysis.DropDownStyle = ComboBoxStyle.DropDownList
        cmbAnalysis.FormattingEnabled = True
        cmbAnalysis.Location = New Point(20, 329)
        cmbAnalysis.Margin = New Padding(5, 6, 5, 6)
        cmbAnalysis.Name = "cmbAnalysis"
        cmbAnalysis.Size = New Size(342, 33)
        cmbAnalysis.TabIndex = 26
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.DarkBlue
        Label5.Location = New Point(32, 294)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(132, 25)
        Label5.TabIndex = 27
        Label5.Text = "Analysis"
        ' 
        ' txtRunName
        ' 
        txtRunName.Location = New Point(375, 329)
        txtRunName.Margin = New Padding(5, 6, 5, 6)
        txtRunName.MaxLength = 100
        txtRunName.Name = "txtRunName"
        txtRunName.Size = New Size(387, 31)
        txtRunName.TabIndex = 28
        txtRunName.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.DarkBlue
        Label6.Location = New Point(390, 294)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(132, 25)
        Label6.TabIndex = 29
        Label6.Text = "QC Run Name"
        ' 
        ' txtControls
        ' 
        txtControls.Location = New Point(38, 446)
        txtControls.Margin = New Padding(5, 6, 5, 6)
        txtControls.MaxLength = 2
        txtControls.Name = "txtControls"
        txtControls.ReadOnly = True
        txtControls.Size = New Size(86, 31)
        txtControls.TabIndex = 30
        txtControls.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.DarkBlue
        Label7.Location = New Point(38, 415)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(88, 25)
        Label7.TabIndex = 31
        Label7.Text = "Controls"
        Label7.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label8
        ' 
        Label8.ForeColor = Color.DarkBlue
        Label8.Location = New Point(137, 415)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(92, 25)
        Label8.TabIndex = 32
        Label8.Text = "Validators"
        Label8.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtValidators
        ' 
        txtValidators.Location = New Point(137, 446)
        txtValidators.Margin = New Padding(5, 6, 5, 6)
        txtValidators.MaxLength = 2
        txtValidators.Name = "txtValidators"
        txtValidators.ReadOnly = True
        txtValidators.Size = New Size(89, 31)
        txtValidators.TabIndex = 33
        txtValidators.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtValidated
        ' 
        txtValidated.Location = New Point(238, 446)
        txtValidated.Margin = New Padding(5, 6, 5, 6)
        txtValidated.MaxLength = 2
        txtValidated.Name = "txtValidated"
        txtValidated.ReadOnly = True
        txtValidated.Size = New Size(89, 31)
        txtValidated.TabIndex = 35
        txtValidated.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label9
        ' 
        Label9.ForeColor = Color.DarkBlue
        Label9.Location = New Point(233, 415)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(107, 25)
        Label9.TabIndex = 34
        Label9.Text = "Validated?"
        Label9.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtCreatedBy
        ' 
        txtCreatedBy.Location = New Point(340, 446)
        txtCreatedBy.Margin = New Padding(5, 6, 5, 6)
        txtCreatedBy.MaxLength = 2
        txtCreatedBy.Name = "txtCreatedBy"
        txtCreatedBy.ReadOnly = True
        txtCreatedBy.Size = New Size(119, 31)
        txtCreatedBy.TabIndex = 37
        txtCreatedBy.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label10
        ' 
        Label10.ForeColor = Color.DarkBlue
        Label10.Location = New Point(340, 415)
        Label10.Margin = New Padding(5, 0, 5, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(122, 25)
        Label10.TabIndex = 36
        Label10.Text = "Created By"
        Label10.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtEditedBy
        ' 
        txtEditedBy.Location = New Point(472, 446)
        txtEditedBy.Margin = New Padding(5, 6, 5, 6)
        txtEditedBy.MaxLength = 2
        txtEditedBy.Name = "txtEditedBy"
        txtEditedBy.ReadOnly = True
        txtEditedBy.Size = New Size(119, 31)
        txtEditedBy.TabIndex = 39
        txtEditedBy.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label11
        ' 
        Label11.ForeColor = Color.DarkBlue
        Label11.Location = New Point(472, 415)
        Label11.Margin = New Padding(5, 0, 5, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(122, 25)
        Label11.TabIndex = 38
        Label11.Text = "Edited By?"
        Label11.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtEditedOn
        ' 
        txtEditedOn.Location = New Point(603, 446)
        txtEditedOn.Margin = New Padding(5, 6, 5, 6)
        txtEditedOn.MaxLength = 2
        txtEditedOn.Name = "txtEditedOn"
        txtEditedOn.ReadOnly = True
        txtEditedOn.Size = New Size(144, 31)
        txtEditedOn.TabIndex = 41
        txtEditedOn.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label12
        ' 
        Label12.ForeColor = Color.DarkBlue
        Label12.Location = New Point(597, 415)
        Label12.Margin = New Padding(5, 0, 5, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(153, 25)
        Label12.TabIndex = 40
        Label12.Text = "Edited On"
        Label12.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(32, 83)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(132, 25)
        Label3.TabIndex = 43
        Label3.Text = "Equipment"
        ' 
        ' cmbEquipment
        ' 
        cmbEquipment.DropDownStyle = ComboBoxStyle.DropDownList
        cmbEquipment.FormattingEnabled = True
        cmbEquipment.Location = New Point(20, 117)
        cmbEquipment.Margin = New Padding(5, 6, 5, 6)
        cmbEquipment.Name = "cmbEquipment"
        cmbEquipment.Size = New Size(546, 33)
        cmbEquipment.TabIndex = 42
        ' 
        ' chkAllInd
        ' 
        chkAllInd.Appearance = Appearance.Button
        chkAllInd.CheckAlign = ContentAlignment.MiddleCenter
        chkAllInd.Location = New Point(610, 113)
        chkAllInd.Margin = New Padding(5, 6, 5, 6)
        chkAllInd.Name = "chkAllInd"
        chkAllInd.Size = New Size(155, 42)
        chkAllInd.TabIndex = 44
        chkAllInd.Text = "All"
        chkAllInd.TextAlign = ContentAlignment.MiddleCenter
        chkAllInd.UseVisualStyleBackColor = True
        ' 
        ' frmQCManagement
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(788, 529)
        Controls.Add(chkAllInd)
        Controls.Add(Label3)
        Controls.Add(cmbEquipment)
        Controls.Add(txtEditedOn)
        Controls.Add(Label12)
        Controls.Add(txtEditedBy)
        Controls.Add(Label11)
        Controls.Add(txtCreatedBy)
        Controls.Add(Label10)
        Controls.Add(txtValidated)
        Controls.Add(Label9)
        Controls.Add(txtValidators)
        Controls.Add(Label8)
        Controls.Add(Label7)
        Controls.Add(txtControls)
        Controls.Add(Label6)
        Controls.Add(txtRunName)
        Controls.Add(Label5)
        Controls.Add(cmbAnalysis)
        Controls.Add(cmbShift)
        Controls.Add(txtTime)
        Controls.Add(txtRunID)
        Controls.Add(Label1)
        Controls.Add(btnQCRunLook)
        Controls.Add(lbldate)
        Controls.Add(dtpDate)
        Controls.Add(Label2)
        Controls.Add(Label4)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmQCManagement"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "QC Management"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtTime As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtRunID As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnQCRunLook As System.Windows.Forms.Button
    Friend WithEvents lbldate As System.Windows.Forms.Label
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbShift As System.Windows.Forms.ComboBox
    Friend WithEvents cmbAnalysis As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtRunName As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtControls As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtValidators As System.Windows.Forms.TextBox
    Friend WithEvents txtValidated As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtCreatedBy As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtEditedBy As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtEditedOn As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbEquipment As System.Windows.Forms.ComboBox
    Friend WithEvents chkAllInd As System.Windows.Forms.CheckBox

End Class
