<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProviderAlert
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
        Me.txtAlert = New System.Windows.Forms.RichTextBox
        Me.SuspendLayout()
        '
        'txtAlert
        '
        Me.txtAlert.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAlert.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtAlert.Location = New System.Drawing.Point(0, 0)
        Me.txtAlert.Name = "txtAlert"
        Me.txtAlert.ReadOnly = True
        Me.txtAlert.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.txtAlert.Size = New System.Drawing.Size(452, 238)
        Me.txtAlert.TabIndex = 0
        Me.txtAlert.Text = ""
        '
        'frmProviderAlert
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(452, 238)
        Me.Controls.Add(Me.txtAlert)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmProviderAlert"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Provider Alert"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtAlert As System.Windows.Forms.RichTextBox

End Class
