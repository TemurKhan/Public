Public Class frmRV

    Private Sub frmRV_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'TODO: CRRV.RefreshReport()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

End Class