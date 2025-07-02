Imports System.Windows.Forms

Public Class frmNotes

    Private Sub frmTestNote_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '' txtNote.Text = frmTests.txtResultNote.Text
        'frmTests.txtResultNote.Text = frmTests.rsNotes
        'ProlisText2.RichTextBox1.Text = frmTests.txtResultNote.Text

        'If frmTests.txtResultNote.Text = "" Then
        '    ProlisText2.RichTextBox1.Text = frmTests.rsNotes
        'End If
        'If frmTests.txtResultNote.Text.Contains("{\rtf") Then
        '    Me.ProlisText2.RichTextBox1.Rtf = frmTests.txtResultNote.Text
        'End If

        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    'Public Shadows Function ShowDialog() As String
    '    MyBase.ShowDialog()
    '    Dim rtf As String = Me.ProlisText2.RichTextBox1.Rtf
    '    If rtf.Contains("\i") Or rtf.Contains("\c") Or rtf.Contains("\*\") Or rtf.Contains("\b") Or rtf.Contains("\ul") Then
    '        Return Me.ProlisText2.RichTextBox1.Rtf
    '    End If
    '    Return Me.ProlisText2.RichTextBox1.Text  'CType(TextBox1.Text, Integer)
    'End Function

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        Me.DialogResult = DialogResult.OK
        'Me.Close()
    End Sub
End Class
