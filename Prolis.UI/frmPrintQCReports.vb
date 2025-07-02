Imports System.Windows.Forms
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmPrintQCReports
    Private Sub frmPrintQCReports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CN.Open("DSN=ProlisQC")
        chkTabLVJ.Checked = False
        PopulateAnalyses()
        dtpDateFrom.Value = Date.Today.AddMonths(-1)
        dtpDateTo.Value = Date.Today
        cmbDestination.SelectedIndex = 0
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub PopulateAnalyses()
        cmbAnalysis.Items.Clear()
        Dim cnpa As New SqlConnection(connString)
        cnpa.Open()
        Dim cmdpa As New SqlCommand("Select ID, Name from Anas where ID in (Select " &
        "distinct Analysis_ID from Runs where ID in (Select distinct Run_ID from " &
        "QC_Results where not (Result is NULL or Result = ''))) order by Name", cnpa)
        cmdpa.CommandType = CommandType.Text
        Dim drpa As SqlDataReader = cmdpa.ExecuteReader
        If drpa.HasRows Then
            While drpa.Read
                cmbAnalysis.Items.Add(New MyList(drpa("Name") &
                " [" & drpa("ID") & "]", drpa("ID")))
            End While
        End If
        cnpa.Close()
        cnpa = Nothing
    End Sub

    Private Sub chkTabLVJ_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTabLVJ.CheckedChanged
        If chkTabLVJ.Checked = False Then
            chkTabLVJ.Text = "Tabular"
            cmbAnalyte.SelectedIndex = -1
            cmbAnalyte.Enabled = False
            lstLevels.Enabled = False
        Else
            chkTabLVJ.Text = "Levy Jennings"
            cmbAnalyte.Enabled = True
            lstLevels.Enabled = True
        End If
    End Sub

    Private Sub cmbAnalysis_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAnalysis.SelectedIndexChanged
        If cmbAnalysis.SelectedIndex = -1 Then
            cmbAnalyte.Items.Clear()
        Else
            Dim ItemX As MyList = cmbAnalysis.SelectedItem
            PopulateAnalytes(ItemX.ItemData)
        End If
    End Sub

    Private Sub PopulateAnalytes(ByVal AnaID As Integer)
        cmbAnalyte.Items.Clear()
        Dim sSQL As String = "Select Distinct a.ID, a.Name from Tests a inner join " &
        "(QC_Results b inner join Runs c on c.ID = b.Run_ID) on a.ID = b.Test_ID " &
        "where b.Released <> 0 and c.Analysis_ID = " & AnaID & " order by a.Name"
        sSQL = "SELECT DISTINCT a.ID, a.Name FROM Tests a where  a.ID in (select Test_ID from QC_Results b JOIN Runs c ON b.Run_ID = c.ID WHERE b.Released <> 0 AND c.Analysis_ID =  " & AnaID & " ) ORDER BY a.Name;"
        Dim cnpa As New SqlConnection(connString)
        cnpa.Open()
        Dim cmdpa As New SqlCommand(sSQL, cnpa)
        cmdpa.CommandType = CommandType.Text
        cmdpa.CommandTimeout = 180
        Dim drpa As SqlDataReader = cmdpa.ExecuteReader
        If drpa.HasRows Then
            cmbAnalyte.Items.Add(New MyList("ALL ...", 0))
            While drpa.Read
                cmbAnalyte.Items.Add(New MyList(drpa("Name") &
                " [" & drpa("ID") & "]", drpa("ID")))
            End While
        End If
        cnpa.Close()
        cnpa = Nothing
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        If cmbAnalysis.SelectedIndex <> -1 Then
            Try
                Dim ItemX As MyList = cmbAnalysis.SelectedItem
                'TODO: Dim gReport As New ReportDocument
                Dim UID As String = My.Settings.UID.ToString
                Dim PWD As String = My.Settings.PWD.ToString
                '{Runs.RunDate} in Date(2014, 03, 01) To Date(2014, 03, 31) and
                '{Anas.ID} = 14 and {Tests.ID} = 326 and {Ana_Control.Control_ID} = 1
                Dim Formula As String = "{Anas.ID} = " & ItemX.ItemData
                If dtpDateFrom.Value <= dtpDateTo.Value Then
                    Formula += " and {Runs.RunDate} in [Date(" &
                    CStr(dtpDateFrom.Value.Year) & ", " &
                    CStr(dtpDateFrom.Value.Month) & ", " &
                    CStr(dtpDateFrom.Value.Day) & ") To Date(" &
                    CStr(dtpDateTo.Value.Year) & ", " &
                    CStr(dtpDateTo.Value.Month) & ", " &
                    CStr(dtpDateTo.Value.Day) & ")]"
                Else
                    Formula += " and {Runs.RunDate} in [Date(" &
                    CStr(dtpDateTo.Value.Year) & ", " &
                    CStr(dtpDateTo.Value.Month) & ", " &
                    CStr(dtpDateTo.Value.Day) & ") To Date(" &
                    CStr(dtpDateFrom.Value.Year) & ", " &
                    CStr(dtpDateFrom.Value.Month) & ", " &
                    CStr(dtpDateFrom.Value.Day) & ")]"
                End If
                '
                If cmbAnalyte.SelectedIndex > 0 Then
                    Dim ItemT As MyList = cmbAnalyte.SelectedItem
                    Formula += " and {Tests.ID} = " & ItemT.ItemData
                End If
                '
                If lstLevels.CheckedItems.Count > 0 Then
                    Formula += " and {Ana_Control.Control_ID} in ["
                    Dim ItemU As MyList
                    For i As Integer = 0 To lstLevels.Items.Count - 1
                        If lstLevels.GetItemChecked(i) = True Then
                            ItemU = lstLevels.Items(i)
                            Formula += ItemU.ItemData & ", "
                        End If
                    Next
                    If Formula.EndsWith(", ") Then Formula = Formula.Substring(0, Len(Formula) - 2) & "]"
                End If
                '
                'TODO: Crystal Reports
                '==================================

                'If chkTabLVJ.Checked = False Then   'Tabular
                '    gReport.Load(Application.StartupPath & "\Reports\QC_Tab.rpt")
                '    ApplyNewServer(gReport, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)
                '    'gReport.SetDatabaseLogon(UID, PWD)
                '    gReport.RecordSelectionFormula = Formula
                'Else
                '    gReport.Load(Application.StartupPath & "\Reports\QC_LJ.rpt")
                '    ApplyNewServer(gReport, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)
                '    'gReport.SetDatabaseLogon(UID, PWD)

                '    gReport.RecordSelectionFormula = Formula
                'End If
                'If cmbDestination.SelectedIndex = 0 Then
                '    gReport.PrintToPrinter(1, False, 0, 0)
                'Else
                '    frmRV.CRRV.ReportSource = gReport
                '    frmRV.Show()
                '    frmRV.MdiParent = ProlisQC
                'End If
                '==================================
                'gReport.Close()
                'gReport = Nothing
            Catch Ex As Exception
                MsgBox(Ex.Message)
            End Try
        End If
    End Sub

    Private Sub cmbAnalyte_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAnalyte.SelectedIndexChanged
        lstLevels.Items.Clear()
        My.Application.DoEvents()
        Dim ItemA As MyList = cmbAnalyte.SelectedItem
        Dim ItemN As MyList = cmbAnalysis.SelectedItem
        Dim sSQL As String = ""
        If cmbAnalyte.SelectedIndex = 0 Then
            sSQL = "Select distinct Control_ID, ControlName from Ana_Control where Ana_ID = " &
            ItemN.ItemData & " and Control_ID in (Select distinct a.Control_ID from Qc_Results " &
            "a inner join Runs b on b.ID = a.Run_ID where not (a.Result is null or a.Result = '') " &
            "and a.Test_ID in (Select Test_ID from QC_Results where Run_ID = b.ID) and b.RunDate " &
            "between '" & Format(dtpDateFrom.Value, SystemConfig.DateFormat) & "' and '" &
            Format(dtpDateTo.Value, SystemConfig.DateFormat) & " 23:59:00') order by ControlName"
        Else
            If ItemA Is Nothing Then
                Return
            End If
            sSQL = "Select distinct Control_ID, ControlName from Ana_Control where Ana_ID = " &
            ItemN.ItemData & " and Control_ID in (Select distinct a.Control_ID from Qc_Results a " &
            "inner join Runs b on b.ID = a.Run_ID where not (a.Result is null or a.Result = '') and " &
            "a.Test_ID = " & ItemA.ItemData & " and b.RunDate between '" & Format(dtpDateFrom.Value,
            SystemConfig.DateFormat) & "' and '" & Format(dtpDateTo.Value, SystemConfig.DateFormat) &
            " 23:59:00') order by ControlName"
        End If
        Dim cnqc As New SqlConnection(connString)
        cnqc.Open()
        Dim cmdqc As New SqlCommand(sSQL, cnqc)
        cmdqc.CommandType = CommandType.Text
        Dim drqc As SqlDataReader = cmdqc.ExecuteReader
        If drqc.HasRows Then
            While drqc.Read
                lstLevels.Items.Add(New MyList(drqc("ControlName") & _
                " (" & drqc("Control_ID") & ")", drqc("Control_ID")))
                lstLevels.SetItemChecked(lstLevels.Items.Count - 1, True)
            End While
        End If
        cnqc.Close()
        cnqc = Nothing
    End Sub
End Class
