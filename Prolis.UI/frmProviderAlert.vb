Imports System.Windows.Forms

Public Class frmProviderAlert

    Private Sub frmProviderAlert_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtAlert.BackColor = txtAlert.SelectionBackColor
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub
End Class
