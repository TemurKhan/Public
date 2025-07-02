<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmChatWithList
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnSendMsg = New System.Windows.Forms.Button()
        Me.txtMsg = New System.Windows.Forms.RichTextBox()
        Me.rtbChat = New System.Windows.Forms.RichTextBox()
        Me.megType = New System.Windows.Forms.TextBox()
        Me.rtbBgChat = New System.Windows.Forms.RichTextBox()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.txtUserID = New System.Windows.Forms.TextBox()
        Me.lblUserID = New System.Windows.Forms.Label()
        Me.lblUserName = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.searchuser = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tbProviders = New System.Windows.Forms.TabPage()
        Me.dgvClients = New System.Windows.Forms.DataGridView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tbProviders.SuspendLayout()
        CType(Me.dgvClients, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.btnSendMsg)
        Me.GroupBox1.Controls.Add(Me.txtMsg)
        Me.GroupBox1.Controls.Add(Me.rtbChat)
        Me.GroupBox1.Controls.Add(Me.megType)
        Me.GroupBox1.Location = New System.Drawing.Point(306, 58)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.MaximumSize = New System.Drawing.Size(783, 583)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(783, 575)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        '
        'btnSendMsg
        '
        Me.btnSendMsg.Location = New System.Drawing.Point(693, 466)
        Me.btnSendMsg.Name = "btnSendMsg"
        Me.btnSendMsg.Size = New System.Drawing.Size(71, 85)
        Me.btnSendMsg.TabIndex = 10
        Me.btnSendMsg.Text = "Send"
        Me.btnSendMsg.UseVisualStyleBackColor = True
        '
        'txtMsg
        '
        Me.txtMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMsg.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMsg.Location = New System.Drawing.Point(26, 466)
        Me.txtMsg.Name = "txtMsg"
        Me.txtMsg.Size = New System.Drawing.Size(669, 85)
        Me.txtMsg.TabIndex = 9
        Me.txtMsg.Text = ""
        '
        'rtbChat
        '
        Me.rtbChat.BackColor = System.Drawing.SystemColors.Control
        Me.rtbChat.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbChat.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.rtbChat.HideSelection = False
        Me.rtbChat.Location = New System.Drawing.Point(26, 4)
        Me.rtbChat.Margin = New System.Windows.Forms.Padding(4)
        Me.rtbChat.Name = "rtbChat"
        Me.rtbChat.ReadOnly = True
        Me.rtbChat.Size = New System.Drawing.Size(738, 455)
        Me.rtbChat.TabIndex = 1
        Me.rtbChat.TabStop = False
        Me.rtbChat.Text = ""
        '
        'megType
        '
        Me.megType.Location = New System.Drawing.Point(212, 103)
        Me.megType.Name = "megType"
        Me.megType.Size = New System.Drawing.Size(100, 22)
        Me.megType.TabIndex = 10
        Me.megType.Visible = False
        '
        'rtbBgChat
        '
        Me.rtbBgChat.BackColor = System.Drawing.Color.White
        Me.rtbBgChat.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbBgChat.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbBgChat.HideSelection = False
        Me.rtbBgChat.Location = New System.Drawing.Point(83, 473)
        Me.rtbBgChat.Margin = New System.Windows.Forms.Padding(4)
        Me.rtbBgChat.Name = "rtbBgChat"
        Me.rtbBgChat.ReadOnly = True
        Me.rtbBgChat.Size = New System.Drawing.Size(150, 88)
        Me.rtbBgChat.TabIndex = 6
        Me.rtbBgChat.TabStop = False
        Me.rtbBgChat.Text = ""
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(83, 93)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(229, 22)
        Me.txtUserName.TabIndex = 5
        Me.txtUserName.Visible = False
        '
        'txtUserID
        '
        Me.txtUserID.Location = New System.Drawing.Point(83, 61)
        Me.txtUserID.Name = "txtUserID"
        Me.txtUserID.Size = New System.Drawing.Size(229, 22)
        Me.txtUserID.TabIndex = 5
        Me.txtUserID.Visible = False
        '
        'lblUserID
        '
        Me.lblUserID.AutoSize = True
        Me.lblUserID.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUserID.ForeColor = System.Drawing.Color.Black
        Me.lblUserID.Location = New System.Drawing.Point(496, 19)
        Me.lblUserID.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblUserID.Name = "lblUserID"
        Me.lblUserID.Size = New System.Drawing.Size(83, 31)
        Me.lblUserID.TabIndex = 16
        Me.lblUserID.Text = "Name"
        Me.lblUserID.Visible = False
        '
        'lblUserName
        '
        Me.lblUserName.AutoSize = True
        Me.lblUserName.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUserName.ForeColor = System.Drawing.Color.Black
        Me.lblUserName.Location = New System.Drawing.Point(326, 19)
        Me.lblUserName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(83, 31)
        Me.lblUserName.TabIndex = 17
        Me.lblUserName.Text = "Name"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 5000
        '
        'searchuser
        '
        Me.searchuser.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.searchuser.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.searchuser.Location = New System.Drawing.Point(12, 19)
        Me.searchuser.Name = "searchuser"
        Me.searchuser.Size = New System.Drawing.Size(300, 21)
        Me.searchuser.TabIndex = 21
        Me.searchuser.Text = "Search here...."
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.lblUserName)
        Me.Panel1.Controls.Add(Me.lblUserID)
        Me.Panel1.Location = New System.Drawing.Point(313, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(780, 55)
        Me.Panel1.TabIndex = 18
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.TabControl1)
        Me.Panel2.Location = New System.Drawing.Point(-3, 58)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(315, 584)
        Me.Panel2.TabIndex = 19
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tbProviders)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(3, 4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(309, 571)
        Me.TabControl1.TabIndex = 11
        '
        'tbProviders
        '
        Me.tbProviders.BackColor = System.Drawing.Color.Lavender
        Me.tbProviders.Controls.Add(Me.dgvClients)
        Me.tbProviders.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbProviders.Location = New System.Drawing.Point(4, 25)
        Me.tbProviders.Name = "tbProviders"
        Me.tbProviders.Padding = New System.Windows.Forms.Padding(3)
        Me.tbProviders.Size = New System.Drawing.Size(301, 542)
        Me.tbProviders.TabIndex = 0
        Me.tbProviders.Text = "Client Users"
        '
        'dgvClients
        '
        Me.dgvClients.AllowUserToAddRows = False
        Me.dgvClients.AllowUserToDeleteRows = False
        Me.dgvClients.AllowUserToResizeColumns = False
        Me.dgvClients.AllowUserToResizeRows = False
        Me.dgvClients.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvClients.BackgroundColor = System.Drawing.Color.White
        Me.dgvClients.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvClients.ColumnHeadersVisible = False
        Me.dgvClients.Location = New System.Drawing.Point(0, 1)
        Me.dgvClients.MultiSelect = False
        Me.dgvClients.Name = "dgvClients"
        Me.dgvClients.ReadOnly = True
        Me.dgvClients.RowHeadersWidth = 51
        Me.dgvClients.RowTemplate.Height = 24
        Me.dgvClients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvClients.Size = New System.Drawing.Size(301, 540)
        Me.dgvClients.TabIndex = 23
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.TabPage2.Controls.Add(Me.dgv)
        Me.TabPage2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(301, 542)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Internal Users"
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.AllowUserToResizeColumns = False
        Me.dgv.AllowUserToResizeRows = False
        Me.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgv.BackgroundColor = System.Drawing.Color.White
        Me.dgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.ColumnHeadersVisible = False
        Me.dgv.Location = New System.Drawing.Point(0, 1)
        Me.dgv.MultiSelect = False
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowHeadersWidth = 51
        Me.dgv.RowTemplate.Height = 24
        Me.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv.Size = New System.Drawing.Size(301, 540)
        Me.dgv.TabIndex = 22
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.Controls.Add(Me.searchuser)
        Me.Panel3.Location = New System.Drawing.Point(-3, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(315, 55)
        Me.Panel3.TabIndex = 20
        '
        'frmChatWithList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1092, 640)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.rtbBgChat)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtUserName)
        Me.Controls.Add(Me.txtUserID)
        Me.Name = "frmChatWithList"
        Me.Text = "Chat Window"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.tbProviders.ResumeLayout(False)
        CType(Me.dgvClients, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtUserName As TextBox
    Friend WithEvents txtUserID As TextBox
    Friend WithEvents rtbChat As RichTextBox
    Friend WithEvents lblUserID As Label
    Friend WithEvents lblUserName As Label
    Friend WithEvents rtbBgChat As RichTextBox
    Friend WithEvents Timer1 As Timer
    Friend WithEvents searchuser As System.Windows.Forms.TextBox
    Friend WithEvents btnSendMsg As System.Windows.Forms.Button
    Friend WithEvents txtMsg As System.Windows.Forms.RichTextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents megType As System.Windows.Forms.TextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tbProviders As System.Windows.Forms.TabPage
    Friend WithEvents dgvClients As System.Windows.Forms.DataGridView
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
End Class
