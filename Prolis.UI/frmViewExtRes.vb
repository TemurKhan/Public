Imports System.Windows.Forms

Public Class frmViewExtRes

    Private Sub frmViewExtRes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub
End Class
