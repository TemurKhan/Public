<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptBuild
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRptBuild))
        ToolStrip1 = New ToolStrip()
        chkEditNew = New ToolStripButton()
        ToolStripButton2 = New ToolStripSeparator()
        btnSave = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnDelete = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        btnRptLook = New Button()
        txtFile = New TextBox()
        Label3 = New Label()
        txtName = New TextBox()
        Label2 = New Label()
        txtID = New TextBox()
        Label1 = New Label()
        btnFile = New Button()
        txtDesc = New TextBox()
        lstUsers = New CheckedListBox()
        Label4 = New Label()
        Label5 = New Label()
        btnAuthAll = New Button()
        btnDeAuthAll = New Button()
        ToolStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {chkEditNew, ToolStripButton2, btnSave, ToolStripSeparator1, btnDelete, ToolStripSeparator2, btnCancel, ToolStripSeparator3, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(803, 34)
        ToolStrip1.TabIndex = 6
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' chkEditNew
        ' 
        chkEditNew.CheckOnClick = True
        chkEditNew.ForeColor = Color.DarkBlue
        chkEditNew.Image = CType(resources.GetObject("chkEditNew.Image"), Image)
        chkEditNew.ImageTransparentColor = Color.Magenta
        chkEditNew.Name = "chkEditNew"
        chkEditNew.Size = New Size(70, 29)
        chkEditNew.Text = "Edit"
        ' 
        ' ToolStripButton2
        ' 
        ToolStripButton2.Name = "ToolStripButton2"
        ToolStripButton2.Size = New Size(6, 34)
        ' 
        ' btnSave
        ' 
        btnSave.Enabled = False
        btnSave.ForeColor = Color.DarkBlue
        btnSave.Image = CType(resources.GetObject("btnSave.Image"), Image)
        btnSave.ImageTransparentColor = Color.Magenta
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(103, 29)
        btnSave.Text = "Register"
        btnSave.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
        ' 
        ' btnDelete
        ' 
        btnDelete.Enabled = False
        btnDelete.ForeColor = Color.Red
        btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), Image)
        btnDelete.ImageTransparentColor = Color.Magenta
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(125, 29)
        btnDelete.Text = "UnRegister"
        btnDelete.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 34)
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
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(6, 34)
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
        ' btnRptLook
        ' 
        btnRptLook.Image = CType(resources.GetObject("btnRptLook.Image"), Image)
        btnRptLook.Location = New Point(158, 94)
        btnRptLook.Margin = New Padding(5, 6, 5, 6)
        btnRptLook.Name = "btnRptLook"
        btnRptLook.Size = New Size(43, 46)
        btnRptLook.TabIndex = 2
        btnRptLook.UseVisualStyleBackColor = True
        ' 
        ' txtFile
        ' 
        txtFile.Location = New Point(17, 192)
        txtFile.Margin = New Padding(5, 6, 5, 6)
        txtFile.MaxLength = 60
        txtFile.Name = "txtFile"
        txtFile.ReadOnly = True
        txtFile.Size = New Size(711, 31)
        txtFile.TabIndex = 4
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.Red
        Label3.Location = New Point(22, 152)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(143, 35)
        Label3.TabIndex = 89
        Label3.Text = "Report File"
        Label3.TextAlign = ContentAlignment.BottomLeft
        ' 
        ' txtName
        ' 
        txtName.Location = New Point(212, 100)
        txtName.Margin = New Padding(5, 6, 5, 6)
        txtName.MaxLength = 60
        txtName.Name = "txtName"
        txtName.Size = New Size(569, 31)
        txtName.TabIndex = 3
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.Red
        Label2.Location = New Point(212, 63)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(228, 31)
        Label2.TabIndex = 87
        Label2.Text = "Report Name"
        ' 
        ' txtID
        ' 
        txtID.Location = New Point(17, 100)
        txtID.Margin = New Padding(5, 6, 5, 6)
        txtID.MaxLength = 4
        txtID.Name = "txtID"
        txtID.Size = New Size(129, 31)
        txtID.TabIndex = 1
        txtID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.Red
        Label1.Location = New Point(20, 63)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(120, 31)
        Label1.TabIndex = 85
        Label1.Text = "Report ID"
        ' 
        ' btnFile
        ' 
        btnFile.Image = CType(resources.GetObject("btnFile.Image"), Image)
        btnFile.Location = New Point(740, 185)
        btnFile.Margin = New Padding(5, 6, 5, 6)
        btnFile.Name = "btnFile"
        btnFile.Size = New Size(43, 46)
        btnFile.TabIndex = 5
        btnFile.UseVisualStyleBackColor = True
        ' 
        ' txtDesc
        ' 
        txtDesc.Location = New Point(17, 283)
        txtDesc.Margin = New Padding(5, 6, 5, 6)
        txtDesc.MaxLength = 200
        txtDesc.Multiline = True
        txtDesc.Name = "txtDesc"
        txtDesc.ScrollBars = ScrollBars.Vertical
        txtDesc.Size = New Size(764, 112)
        txtDesc.TabIndex = 6
        ' 
        ' lstUsers
        ' 
        lstUsers.FormattingEnabled = True
        lstUsers.Location = New Point(20, 465)
        lstUsers.Margin = New Padding(5, 6, 5, 6)
        lstUsers.Name = "lstUsers"
        lstUsers.Size = New Size(761, 228)
        lstUsers.TabIndex = 9
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(22, 237)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(143, 40)
        Label4.TabIndex = 95
        Label4.Text = "Description"
        Label4.TextAlign = ContentAlignment.BottomLeft
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.Maroon
        Label5.Location = New Point(22, 425)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(197, 35)
        Label5.TabIndex = 96
        Label5.Text = "Authorized Users"
        Label5.TextAlign = ContentAlignment.BottomLeft
        ' 
        ' btnAuthAll
        ' 
        btnAuthAll.ForeColor = Color.DarkGreen
        btnAuthAll.Location = New Point(463, 410)
        btnAuthAll.Margin = New Padding(5, 6, 5, 6)
        btnAuthAll.Name = "btnAuthAll"
        btnAuthAll.Size = New Size(155, 46)
        btnAuthAll.TabIndex = 7
        btnAuthAll.Text = "Authorize All"
        btnAuthAll.UseVisualStyleBackColor = True
        ' 
        ' btnDeAuthAll
        ' 
        btnDeAuthAll.ForeColor = Color.Red
        btnDeAuthAll.Location = New Point(628, 410)
        btnDeAuthAll.Margin = New Padding(5, 6, 5, 6)
        btnDeAuthAll.Name = "btnDeAuthAll"
        btnDeAuthAll.Size = New Size(155, 46)
        btnDeAuthAll.TabIndex = 8
        btnDeAuthAll.Text = "DeAuthorize All"
        btnDeAuthAll.UseVisualStyleBackColor = True
        ' 
        ' frmRptBuild
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(803, 748)
        Controls.Add(btnDeAuthAll)
        Controls.Add(btnAuthAll)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(lstUsers)
        Controls.Add(txtDesc)
        Controls.Add(btnFile)
        Controls.Add(btnRptLook)
        Controls.Add(txtFile)
        Controls.Add(Label3)
        Controls.Add(txtName)
        Controls.Add(Label2)
        Controls.Add(txtID)
        Controls.Add(Label1)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmRptBuild"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Report Build"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents chkEditNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRptLook As System.Windows.Forms.Button
    Friend WithEvents txtFile As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnFile As System.Windows.Forms.Button
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents lstUsers As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnAuthAll As System.Windows.Forms.Button
    Friend WithEvents btnDeAuthAll As System.Windows.Forms.Button

End Class
