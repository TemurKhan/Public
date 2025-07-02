<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogin
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
    Friend WithEvents LogoPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents UsernameLabel As System.Windows.Forms.Label
    Friend WithEvents PasswordLabel As System.Windows.Forms.Label
    Friend WithEvents UsernameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PasswordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents btn_OK As System.Windows.Forms.Button
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogin))
        UsernameLabel = New Label()
        PasswordLabel = New Label()
        UsernameTextBox = New TextBox()
        PasswordTextBox = New TextBox()
        btn_OK = New Button()
        btn_Cancel = New Button()
        LogoPictureBox = New PictureBox()
        CType(LogoPictureBox, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' UsernameLabel
        ' 
        UsernameLabel.Location = New Point(169, 17)
        UsernameLabel.Margin = New Padding(5, 0, 5, 0)
        UsernameLabel.Name = "UsernameLabel"
        UsernameLabel.Size = New Size(184, 33)
        UsernameLabel.TabIndex = 0
        UsernameLabel.Text = "&User name"
        UsernameLabel.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' PasswordLabel
        ' 
        PasswordLabel.Location = New Point(169, 100)
        PasswordLabel.Margin = New Padding(5, 0, 5, 0)
        PasswordLabel.Name = "PasswordLabel"
        PasswordLabel.Size = New Size(184, 33)
        PasswordLabel.TabIndex = 2
        PasswordLabel.Text = "&Password"
        PasswordLabel.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' UsernameTextBox
        ' 
        UsernameTextBox.Location = New Point(171, 56)
        UsernameTextBox.Margin = New Padding(5, 6, 5, 6)
        UsernameTextBox.Name = "UsernameTextBox"
        UsernameTextBox.Size = New Size(244, 31)
        UsernameTextBox.TabIndex = 1
        UsernameTextBox.Text = "!"
        ' 
        ' PasswordTextBox
        ' 
        PasswordTextBox.Location = New Point(169, 139)
        PasswordTextBox.Margin = New Padding(5, 6, 5, 6)
        PasswordTextBox.Name = "PasswordTextBox"
        PasswordTextBox.PasswordChar = "*"c
        PasswordTextBox.Size = New Size(248, 31)
        PasswordTextBox.TabIndex = 3
        ' 
        ' btn_OK
        ' 
        btn_OK.Image = CType(resources.GetObject("btn_OK.Image"), Image)
        btn_OK.Location = New Point(46, 234)
        btn_OK.Margin = New Padding(5, 6, 5, 6)
        btn_OK.Name = "btn_OK"
        btn_OK.Size = New Size(156, 58)
        btn_OK.TabIndex = 4
        btn_OK.Text = "&OK"
        btn_OK.TextAlign = ContentAlignment.MiddleRight
        btn_OK.TextImageRelation = TextImageRelation.ImageBeforeText
        ' 
        ' btn_Cancel
        ' 
        btn_Cancel.DialogResult = DialogResult.Cancel
        btn_Cancel.Image = CType(resources.GetObject("btn_Cancel.Image"), Image)
        btn_Cancel.Location = New Point(231, 234)
        btn_Cancel.Margin = New Padding(5, 6, 5, 6)
        btn_Cancel.Name = "btn_Cancel"
        btn_Cancel.Size = New Size(156, 58)
        btn_Cancel.TabIndex = 5
        btn_Cancel.Text = "&Cancel"
        btn_Cancel.TextAlign = ContentAlignment.MiddleRight
        btn_Cancel.TextImageRelation = TextImageRelation.ImageBeforeText
        ' 
        ' LogoPictureBox
        ' 
        LogoPictureBox.Image = CType(resources.GetObject("LogoPictureBox.Image"), Image)
        LogoPictureBox.Location = New Point(6, 23)
        LogoPictureBox.Margin = New Padding(5, 6, 5, 6)
        LogoPictureBox.Name = "LogoPictureBox"
        LogoPictureBox.Size = New Size(151, 172)
        LogoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage
        LogoPictureBox.TabIndex = 0
        LogoPictureBox.TabStop = False
        ' 
        ' frmLogin
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = btn_Cancel
        ClientSize = New Size(441, 341)
        Controls.Add(btn_Cancel)
        Controls.Add(btn_OK)
        Controls.Add(PasswordTextBox)
        Controls.Add(UsernameTextBox)
        Controls.Add(PasswordLabel)
        Controls.Add(UsernameLabel)
        Controls.Add(LogoPictureBox)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmLogin"
        SizeGripStyle = SizeGripStyle.Hide
        StartPosition = FormStartPosition.CenterParent
        Text = "Prolis Login"
        CType(LogoPictureBox, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub

End Class
