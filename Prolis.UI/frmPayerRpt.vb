Imports System.Windows.Forms
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmPayerRpt

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        'TODO: Crystal Report
        '======================================================
        'Dim gReport As New ReportDocument
        'Dim PrtDlg As New PrintDialog
        'Try
        '    If cmbReports.SelectedIndex <> -1 Then
        '        If cmbReports.SelectedIndex = 0 Then
        '            gReport.Load(Application.StartupPath & "\Reports\PayersList.RPT")
        '            ApplyNewServer(gReport, My.Settings.DSN,
        '            My.Settings.Database, My.Settings.UID, My.Settings.PWD)
        '            If cmbDestination.SelectedIndex = 0 Then    'Printer
        '                If MsgBoxResult.Ok = PrtDlg.ShowDialog Then
        '                    gReport.PrintOptions.PrinterName = PrtDlg.PrinterSettings.PrinterName
        '                    gReport.PrintToPrinter(PrtDlg.PrinterSettings.Copies,
        '                    PrtDlg.PrinterSettings.Collate, 0, 0)
        '                End If
        '            Else    'Screen
        '                frmRV.CRRV.ReportSource = gReport
        '                frmRV.Show()
        '            End If
        '        End If
        '    Else
        '        MsgBox("You need to select the report first and then print")
        '    End If
        'Catch Ex As Exception
        '    MsgBox(Ex.Message)
        'Finally
        '    PrtDlg.Dispose()
        '    PrtDlg = Nothing
        'End Try
        '======================================================
    End Sub

    Private Sub frmPayerRpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbDestination.SelectedIndex = 0
    End Sub
End Class
