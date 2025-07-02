Imports System.Windows.Forms
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmComponentReports

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim Rpt As String
        Dim UID As String = My.Settings.UID.ToString
        Dim PWD As String = My.Settings.PWD.ToString
        If cmbReports.SelectedIndex <> -1 Then
            If cmbReports.SelectedIndex = 0 Then
                Rpt = Application.StartupPath & "\Reports\TestList.RPT"
            ElseIf cmbReports.SelectedIndex = 1 Then
                Rpt = Application.StartupPath & "\Reports\GroupList.RPT"
            ElseIf cmbReports.SelectedIndex = 2 Then
                Rpt = Application.StartupPath & "\Reports\ProfileList.RPT"
            Else
                Rpt = Application.StartupPath & "\Reports\AnalysisList.RPT"
            End If
            '============================
            'TODO: Crystal Reports Code
            'Dim gRpt As New ReportDocument
            'gRpt.Load(Rpt)
            'gRpt.SetDatabaseLogon(UID, PWD)
            'frmRV.CRRV.ReportSource = gRpt
            'frmRV.Show()
            'frmRV.MdiParent = ProlisQC
            '============================
        Else
            MsgBox("You need to select the report first and then print")
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub cmbReports_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbReports.SelectedIndexChanged
        If cmbReports.SelectedIndex <> -1 Then
            btnPrint.Enabled = True
        Else
            btnPrint.Enabled = False
        End If
    End Sub

    Private Sub frmComponentReports_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub
End Class
