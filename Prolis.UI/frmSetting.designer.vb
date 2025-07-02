<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetting
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetting))
        txtUserID = New TextBox()
        txtPassword = New TextBox()
        Label12 = New Label()
        Label13 = New Label()
        txtDatabase = New TextBox()
        Label1 = New Label()
        txtProlisServer = New TextBox()
        Label2 = New Label()
        Label3 = New Label()
        SuspendLayout()
        ' 
        ' txtUserID
        ' 
        txtUserID.Location = New Point(29, 142)
        txtUserID.Margin = New Padding(5, 6, 5, 6)
        txtUserID.MaxLength = 200
        txtUserID.Name = "txtUserID"
        txtUserID.Size = New Size(193, 31)
        txtUserID.TabIndex = 3
        ' 
        ' txtPassword
        ' 
        txtPassword.Location = New Point(250, 142)
        txtPassword.Margin = New Padding(5, 6, 5, 6)
        txtPassword.MaxLength = 200
        txtPassword.Name = "txtPassword"
        txtPassword.PasswordChar = "X"c
        txtPassword.Size = New Size(193, 31)
        txtPassword.TabIndex = 4
        ' 
        ' Label12
        ' 
        Label12.ForeColor = Color.Red
        Label12.Location = New Point(29, 108)
        Label12.Margin = New Padding(5, 0, 5, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(140, 28)
        Label12.TabIndex = 34
        Label12.Text = "Log in Name"
        ' 
        ' Label13
        ' 
        Label13.ForeColor = Color.Red
        Label13.Location = New Point(260, 108)
        Label13.Margin = New Padding(5, 0, 5, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(159, 28)
        Label13.TabIndex = 35
        Label13.Text = "Log in Password"
        ' 
        ' txtDatabase
        ' 
        txtDatabase.Location = New Point(29, 53)
        txtDatabase.Margin = New Padding(5, 6, 5, 6)
        txtDatabase.MaxLength = 200
        txtDatabase.Name = "txtDatabase"
        txtDatabase.Size = New Size(193, 31)
        txtDatabase.TabIndex = 2
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.Red
        Label1.Location = New Point(29, 19)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(165, 28)
        Label1.TabIndex = 37
        Label1.Text = "Database"
        ' 
        ' txtProlisServer
        ' 
        txtProlisServer.Location = New Point(29, 234)
        txtProlisServer.Margin = New Padding(5, 6, 5, 6)
        txtProlisServer.MaxLength = 200
        txtProlisServer.Name = "txtProlisServer"
        txtProlisServer.Size = New Size(414, 31)
        txtProlisServer.TabIndex = 5
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.Red
        Label2.Location = New Point(34, 200)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(165, 28)
        Label2.TabIndex = 39
        Label2.Text = "Prolis Server"
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.Navy
        Label3.Location = New Point(24, 302)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(416, 28)
        Label3.TabIndex = 40
        Label3.Text = "All above 5 fields are required"
        Label3.TextAlign = ContentAlignment.TopCenter
        ' 
        ' frmSetting
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(474, 336)
        Controls.Add(Label3)
        Controls.Add(txtProlisServer)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(txtDatabase)
        Controls.Add(Label13)
        Controls.Add(Label12)
        Controls.Add(txtPassword)
        Controls.Add(txtUserID)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MaximumSize = New Size(496, 392)
        MinimizeBox = False
        MinimumSize = New Size(496, 392)
        Name = "frmSetting"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterScreen
        Text = "Prolis Data Setting"
        TopMost = True
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents txtDSN As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtUserID As System.Windows.Forms.TextBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtDatabase As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtProlisServer As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label

End Class
