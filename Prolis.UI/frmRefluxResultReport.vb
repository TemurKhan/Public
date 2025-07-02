Imports System.ComponentModel
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmRefluxResultReport
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()

    End Sub

    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click

        Dim rsf As String = String.Empty
        Dim repTitle As String = String.Empty

        If chkAccRange.Checked Then

            If String.IsNullOrWhiteSpace(txtAccFrom.Text) Then
                MessageBox.Show("Please Enter From Accession No.")
                Return
            ElseIf Not String.IsNullOrWhiteSpace(txtAccFrom.Text) AndAlso String.IsNullOrWhiteSpace(txtAccTo.Text) Then
                rsf = "{Ref_Results.Accession_ID} =" + txtAccFrom.Text
                repTitle = "Accession ID: " + txtAccFrom.Text
            Else
                rsf = "{Ref_Results.Accession_ID} in " + $"{txtAccFrom.Text} To {txtAccTo.Text}"
                repTitle = "Accession ID: " + $"{txtAccFrom.Text} To {txtAccTo.Text}"
            End If
        ElseIf chkDateRange.Checked Then

            If dtpDateFrom.Value > dtpDateTo.Value Then
                MessageBox.Show("Please Choose ""From Date"" Greater than ""To Date""")
                Return
            Else
                rsf = "{vw_Patient_Provider_Info_by_ReqID.AccessionDate} in DateTime" + $" ({dtpDateFrom.Value.ToString("yyyy,MM,dd")}) to DateTime ({dtpDateTo.Value.AddDays(1).ToString("yyyy,MM,dd")})"
                repTitle = "From: " + $"{dtpDateFrom.Value.ToString(SystemConfig.DateFormat)} To: {dtpDateTo.Value.ToString(SystemConfig.DateFormat)}"
            End If

        End If

        Try
            Cursor.Current = Cursors.WaitCursor

            'TODO: Crystal Report generation code should be here.
            '====================================
            'Dim oRPT As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            'oRPT.Load(GetReportPath("Reflex Results.RPT"))
            'oRPT.RecordSelectionFormula = rsf
            'oRPT.DataDefinition.FormulaFields("Lab Name").Text = "'" & MyLab.LabName & "'"
            'oRPT.SummaryInfo.ReportTitle = repTitle

            'ApplyNewServer(oRPT, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)

            'Dim frmRptView As frmRV = New frmRV
            'frmRptView.CRRV.ReportSource = oRPT
            'frmRptView.MdiParent = ProlisQC
            'frmRptView.Show()
            '====================================
            Cursor.Current = Cursors.Default

        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try

    End Sub

    Private Sub txtAccFrom_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAccFrom.KeyPress, txtAccTo.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAccFrom_Validating(sender As Object, e As CancelEventArgs) Handles txtAccFrom.Validating
        If Not IsNumeric(Me.txtAccFrom.Text) Then
            Me.txtAccFrom.Clear()
        End If
    End Sub
    Private Sub txtAccTo_Validating(sender As Object, e As CancelEventArgs) Handles txtAccTo.Validating
        If Not IsNumeric(Me.txtAccTo.Text) Then
            Me.txtAccTo.Clear()
        End If
    End Sub

    Private Sub frmRefluxResultReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.dtpDateFrom.Value = Now
        Me.dtpDateTo.Value = Now

    End Sub

    Private Sub chkAccRange_CheckedChanged(sender As Object, e As EventArgs) Handles chkAccRange.CheckedChanged
        grpAccRange.Enabled = chkAccRange.Checked
        chkDateRange.Checked = Not chkAccRange.Checked
    End Sub

    Private Sub chkDateRange_CheckedChanged(sender As Object, e As EventArgs) Handles chkDateRange.CheckedChanged
        grpDateRange.Enabled = chkDateRange.Checked
        chkAccRange.Checked = Not chkDateRange.Checked
    End Sub
End Class