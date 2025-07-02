Imports System.Windows.Forms

Public Class frmClientReports

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim RPT As String
        Try
            If cmbReports.SelectedIndex <> -1 Then
                If cmbReports.SelectedIndex = 0 Then
                    RPT = Application.StartupPath & "\Reports\ProvidersList.RPT"
                ElseIf cmbReports.SelectedIndex = 1 Then
                    RPT = Application.StartupPath & "\Reports\Clients_By_Sales_Rep.RPT"
                Else
                    RPT = Application.StartupPath & "\Reports\Clients_By_Route.RPT"
                End If
                If RPT <> "" Then
                    Dim UID As String = My.Settings.UID.ToString
                    Dim PWD As String = My.Settings.PWD
                    'TODO: Crystal Reports Code
                    '======================================
                    'Dim gRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'gRpt.Load(RPT)
                    'gRpt.SetDatabaseLogon(UID, PWD)
                    'frmRV.CRRV.ReportSource = gRpt
                    '======================================
                    frmRV.Show()
                End If
            Else
                MsgBox("You need to select the report first and then print")
            End If
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            
        End Try
    End Sub

    Private Sub frmClientReports_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub
End Class
