Imports System.Windows.Forms

Public Class frmPrintAccLog

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmPrintAccLog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpDateFrom.Format = DateTimePickerFormat.Custom
        dtpDateFrom.CustomFormat = SystemConfig.DateFormat
        dtpDateTo.Format = DateTimePickerFormat.Custom
        dtpDateTo.CustomFormat = SystemConfig.DateFormat
        cmbDestination.SelectedIndex = 0
        cmbAccStaged.SelectedIndex = 0
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Dim Formula As String = ""
        Dim UID As String = My.Settings.UID.ToString
        Dim PWD As String = My.Settings.PWD.ToString
        If cmbAccStaged.SelectedIndex = 0 Then
            Formula = "{Requisitions.Received} = True and {Requisitions.ReceivedTime}" _
            & " in [CDateTime (" & "Date(" & Format(dtpDateFrom.Value, _
            "yyyy,MM,dd") & "), Time(00,00,00)) To CDateTime (Date(" _
            & Format(dtpDateTo.Value, "yyyy,MM,dd") & "), Time(23,59,59))]"
        Else
            Formula = "{Requisitions.Received} = False and {Requisitions.AccessionDate}" _
             & " in [CDateTime (" & "Date(" & Format(dtpDateFrom.Value, _
             "yyyy,MM,dd") & "), Time(00,00,00)) To CDateTime (Date(" _
             & Format(dtpDateTo.Value, "yyyy,MM,dd") & "), Time(23,59,59))]"
        End If
        Try
            'TODO: Crystal Report
            '=============================================

            ''Dim gReport As CrystalDecisions.CrystalReports.Engine.ReportDocument
            'gReport.Load(Application.StartupPath & "\Reports\Accession_Log.rpt")
            ''gReport.SetDatabaseLogon(UID, PWD)
            'ApplyNewServer(gReport, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)
            'gReport.RecordSelectionFormula = Formula
            'If cmbDestination.SelectedIndex = 0 Then
            '    gReport.PrintToPrinter(1, False, 0, 0)
            '    My.Application.DoEvents()
            '    gReport.Close()
            '    gReport = Nothing
            'Else
            '    frmRV.CRRV.ReportSource = gReport
            '    frmRV.Show()
            '    frmRV.MdiParent = ProlisQC
            'End If
            '=============================================

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub cmbAccStaged_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAccStaged.SelectedIndexChanged
        If cmbAccStaged.SelectedIndex = 0 Then  'Received
            lblFrom.Text = "Rc'vd From"
            lblTo.Text = "Rc'vd To"
        Else    'Acc'd
            lblFrom.Text = "Acc'd From"
            lblTo.Text = "Acc'd To"
        End If
    End Sub
End Class
