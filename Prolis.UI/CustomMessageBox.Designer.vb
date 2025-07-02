Partial Class CustomMessageBox
    Inherits Form

    ' Required designer variable.
    Private components As System.ComponentModel.IContainer

    ' Note: The following procedure is required by the Windows Form Designer
    ' It can be modified using the Windows Form Designer.  
    ' Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TextBoxMessage = New System.Windows.Forms.TextBox()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.NO = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TextBoxMessage
        '
        Me.TextBoxMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxMessage.Location = New System.Drawing.Point(31, 15)
        Me.TextBoxMessage.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextBoxMessage.Multiline = True
        Me.TextBoxMessage.Name = "TextBoxMessage"
        Me.TextBoxMessage.ReadOnly = True
        Me.TextBoxMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBoxMessage.Size = New System.Drawing.Size(464, 184)
        Me.TextBoxMessage.TabIndex = 0
        '
        'ButtonOK
        '
        Me.ButtonOK.Location = New System.Drawing.Point(270, 207)
        Me.ButtonOK.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(100, 28)
        Me.ButtonOK.TabIndex = 1
        Me.ButtonOK.Text = "YES"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'NO
        '
        Me.NO.Location = New System.Drawing.Point(395, 207)
        Me.NO.Margin = New System.Windows.Forms.Padding(4)
        Me.NO.Name = "NO"
        Me.NO.Size = New System.Drawing.Size(100, 28)
        Me.NO.TabIndex = 2
        Me.NO.Text = "NO"
        Me.NO.UseVisualStyleBackColor = True
        '
        'CustomMessageBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(512, 247)
        Me.Controls.Add(Me.NO)
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.TextBoxMessage)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "CustomMessageBox"
        Me.Text = "CustomMessageBox"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBoxMessage As TextBox
    Friend WithEvents ButtonOK As Button
    Friend WithEvents NO As System.Windows.Forms.Button
End Class
