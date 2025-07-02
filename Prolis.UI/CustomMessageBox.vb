Public Class CustomMessageBox

    Friend Shared reply As String

    Public Sub New(message As String)
        InitializeComponent()
        TextBoxMessage.Text = message
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        reply = "OK"
        Me.Close()
    End Sub
    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function
    Public Shared Sub ShowMe(message As String, Optional title As String = "Message")
        Dim messageBox As New CustomMessageBox(message)
        messageBox.Text = title
        messageBox.ShowDialog()
    End Sub

    Private Sub NO_Click(sender As Object, e As EventArgs) Handles NO.Click
        reply = "NO"
        Me.Close()
    End Sub
End Class
