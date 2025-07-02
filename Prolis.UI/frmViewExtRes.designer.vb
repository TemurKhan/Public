<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmViewExtRes
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
        Me.rtbResult = New System.Windows.Forms.RichTextBox
        Me.SuspendLayout()
        '
        'rtbResult
        '
        Me.rtbResult.Location = New System.Drawing.Point(12, 16)
        Me.rtbResult.Name = "rtbResult"
        Me.rtbResult.ReadOnly = True
        Me.rtbResult.Size = New System.Drawing.Size(678, 473)
        Me.rtbResult.TabIndex = 0
        Me.rtbResult.Text = ""
        '
        'frmViewExtRes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(702, 501)
        Me.Controls.Add(Me.rtbResult)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmViewExtRes"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "View Exteded Result"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rtbResult As System.Windows.Forms.RichTextBox

End Class
