Imports System.Windows.Forms
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmPartnerRpt

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If cmbReports.SelectedIndex = 0 Then
                Dim UID As String = My.Settings.UID.ToString
                Dim PWD As String = My.Settings.PWD.ToString
                'TODO : Crystal reports
                'Dim gRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                'gRpt.Load(Application.StartupPath & "\Reports\PartnersList.RPT")
                'gRpt.SetDatabaseLogon(UID, PWD)
                'frmRV.CRRV.ReportSource = gRpt
                'frmRV.Show()
                'frmRV.MdiParent = frmDashboard

            Else
                MsgBox("You need to select the report first and then print")
            End If
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
        End Try
    End Sub

    Private Sub frmPartnerRpt_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub
End Class
