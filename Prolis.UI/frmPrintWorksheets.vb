Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmPrintWorksheets

    Private Sub frmPrintWorksheets_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbDestination.SelectedIndex = 0
        grpDates.Text += " (" & SystemConfig.DateFormat & ")"
        PopulateWorksheets()
        PopulateDepartments()
        'txtDateFrom.Focus()

        ConfigureDateTimePicker(dtpDateFrom)
        ConfigureDateTimePicker(dtpDateTo)

        dtpDateFrom.Focus()

        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub PopulateDepartments()
        cmbDept.Items.Clear()
        cmbDept.Items.Add("*ALL*")
        Dim cnpd As New SqlConnection(connString)
        cnpd.Open()
        Dim cmdpd As New SqlCommand("Select * from Departments order by Dept_Name", cnpd)
        cmdpd.CommandType = CommandType.Text
        Dim drpd As SqlDataReader = cmdpd.ExecuteReader
        If drpd.HasRows Then
            While drpd.Read
                cmbDept.Items.Add(New MyList(drpd("Dept_Name"), drpd("ID")))
            End While
        End If
        cnpd.Close()
        cnpd = Nothing
    End Sub

    Private Sub PopulateWorksheets()
        lstWorksheets.Items.Clear()
        Dim cnpw As New SqlConnection(connString)
        cnpw.Open()
        Dim cmdpw As New SqlCommand("Select * from Worksheets order by Name", cnpw)
        cmdpw.CommandType = CommandType.Text
        Dim drpw As SqlDataReader = cmdpw.ExecuteReader
        If drpw.HasRows Then
            While drpw.Read
                lstWorksheets.Items.Add(New MyList(drpw("Name"), drpw("ID")))
            End While
        End If
        cnpw.Close()
        cnpw = Nothing
    End Sub

    Private Sub txtCopies_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCopies.GotFocus
        txtCopies.BackColor = FCOLOR
    End Sub

    Private Sub txtCopies_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCopies.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtCopies_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCopies.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtCopies_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCopies.LostFocus
        txtCopies.BackColor = NFCOLOR
    End Sub

    Private Sub txtCopies_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCopies.Validated
        If txtCopies.Text = "" Then txtCopies.Text = "1"
    End Sub

    'Private Sub txtDateFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    txtDateFrom.BackColor = FCOLOR
    'End Sub

    'Private Sub txtDateFrom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    If e.KeyCode = Keys.Enter Then
    '        SendKeys.Send("{TAB}")
    '    End If
    'End Sub

    'Private Sub txtDateFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    txtDateFrom.BackColor = NFCOLOR
    '    If UserEnteredText(txtDateFrom) <> "" Then
    '        If IsDate(txtDateFrom.Text) = False Then
    '            MsgBox("Invalid date")
    '            txtDateFrom.Text = ""
    '        Else
    '            txtAccIDFrom.Text = ""
    '            txtAccIDTo.Text = ""
    '        End If
    '    End If
    '    PrintProgress()
    'End Sub

    Private Sub PrintProgress()
        If cmbSheets.SelectedIndex = 0 Then 'Equipment Worksheet
            If (IsDate(dtpDateFrom.Text) Or IsDate(dtpDateTo.Text) Or
            Trim(txtAccIDFrom.Text) <> "" Or Trim(txtAccIDTo.Text) <> "") Then
                btnPrint.Enabled = True
            Else
                btnPrint.Enabled = False
            End If
        ElseIf cmbSheets.SelectedIndex = 1 Or cmbSheets.SelectedIndex = 2 Then 'Worksheets In/Out
            If (IsDate(dtpDateFrom.Text) Or IsDate(dtpDateTo.Text) Or
            Trim(txtAccIDFrom.Text) <> "" Or Trim(txtAccIDTo.Text) <> "") _
            AndAlso lstWorksheets.CheckedItems.Count > 0 Then
                btnPrint.Enabled = True
            Else
                btnPrint.Enabled = False
            End If
        ElseIf cmbSheets.SelectedIndex = 3 Then 'Pending Sheet
            If (IsDate(dtpDateFrom.Text) Or IsDate(dtpDateTo.Text) Or
             Trim(txtAccIDFrom.Text) <> "" Or Trim(txtAccIDTo.Text) <> "") Then
                btnPrint.Enabled = True
            Else
                btnPrint.Enabled = False
            End If
        Else    'Result Sheet
            If (IsDate(dtpDateFrom.Text) Or IsDate(dtpDateTo.Text) Or
            Trim(txtAccIDFrom.Text) <> "" Or Trim(txtAccIDTo.Text) <> "") _
            AndAlso lstWorksheets.CheckedItems.Count > 0 Then
                btnPrint.Enabled = True
            Else
                btnPrint.Enabled = False
            End If
        End If
    End Sub

    'Private Sub txtDateTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    txtDateTo.BackColor = FCOLOR
    'End Sub

    'Private Sub txtDateTo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    If e.KeyCode = Keys.Enter Then
    '        SendKeys.Send("{TAB}")
    '    ElseIf e.KeyCode = Keys.Up Then
    '        SendKeys.Send("+{TAB}")
    '    End If
    'End Sub

    'Private Sub txtDateTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    txtDateTo.BackColor = NFCOLOR
    '    If UserEnteredText(txtDateTo) <> "" Then
    '        If IsDate(txtDateTo.Text) = False Then
    '            MsgBox("Invalid date")
    '            txtDateTo.Text = ""
    '        Else
    '            txtAccIDFrom.Text = ""
    '            txtAccIDTo.Text = ""
    '        End If
    '    End If
    '    PrintProgress()
    'End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If IsDate(dtpDateFrom.Text) Or IsDate(dtpDateTo.Text) Or
        Trim(txtAccIDFrom.Text) <> "" Or Trim(txtAccIDTo.Text) <> "" Then
            'TODO: Crystal reports
            '=====================================================
            'Dim ItemX As MyList
            'Dim i As Integer
            'Dim Rpt As String = ""
            'Dim Formula As String = "{Requisitions.Received} = True"
            'If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
            '    Formula += " and {Requisitions.ReceivedTime} in DateTime(" &
            '    CDate(dtpDateFrom.Text).Year & "," & CDate(dtpDateFrom.Text).Month _
            '    & "," & CDate(dtpDateFrom.Text).Day & ",00,00,00) To DateTime(" &
            '    CDate(dtpDateFrom.Text).Year & "," & CDate(dtpDateFrom.Text).Month _
            '    & "," & CDate(dtpDateFrom.Text).Day & ",23,59,00)"
            'ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
            '    Formula += " and {Requisitions.ReceivedTime} in DateTime(" &
            '    CDate(dtpDateTo.Text).Year & "," & CDate(dtpDateTo.Text).Month _
            '    & "," & CDate(dtpDateTo.Text).Day & ",00,00,00) To DateTime(" &
            '    CDate(dtpDateTo.Text).Year & "," & CDate(dtpDateTo.Text).Month _
            '    & "," & CDate(dtpDateTo.Text).Day & ",23,59,00)"
            'ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
            '    Formula += " and {Requisitions.ReceivedTime} in DateTime(" &
            '    CDate(dtpDateFrom.Text).Year & "," & CDate(dtpDateFrom.Text).Month _
            '    & "," & CDate(dtpDateFrom.Text).Day & ",00,00,00) To DateTime(" &
            '    CDate(dtpDateTo.Text).Year & "," & CDate(dtpDateTo.Text).Month _
            '    & "," & CDate(dtpDateTo.Text).Day & ",23,59,00)"
            'ElseIf Trim(txtAccIDFrom.Text) <> "" And Trim(txtAccIDTo.Text) = "" Then
            '    Formula += " and {Requisitions.ID} = " & Val(txtAccIDFrom.Text)
            'ElseIf Trim(txtAccIDFrom.Text) = "" And Trim(txtAccIDTo.Text) <> "" Then
            '    Formula += " and {Requisitions.ID} = " & Val(txtAccIDTo.Text)
            'ElseIf Trim(txtAccIDFrom.Text) <> "" And Trim(txtAccIDTo.Text) <> "" Then
            '    Formula += " and {Requisitions.ID} in [" & Val(txtAccIDFrom.Text) _
            '    & " To " & Val(txtAccIDTo.Text) & "]"
            'End If
            'If cmbSheets.SelectedIndex = 0 Then     'Equipment
            '    Rpt = GetReportPath("EquipWorksheet.RPT")
            '    Formula += " and {Requisitions.InHouse} = True and {Tests.InHouse} = True"
            '    Dim gRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            '    gRpt.Load(Rpt)
            '    ApplyNewServer(gRpt, My.Settings.DSN, My.Settings.Database,
            '    My.Settings.UID, My.Settings.PWD)
            '    gRpt.RecordSelectionFormula = Formula
            '    If cmbDestination.SelectedIndex = 0 Then    'Printer
            '        gRpt.PrintToPrinter(Val(txtCopies.Text), False, 0, 0)
            '        My.Application.DoEvents()
            '        gRpt.Close()
            '        gRpt = Nothing
            '    Else    'Screen
            '        Dim frmRptView As frmRV = New frmRV
            '        frmRptView.CRRV.ReportSource = gRpt
            '        frmRptView.Show()
            '    End If
            'ElseIf cmbSheets.SelectedIndex = 1 Then     'Tests inhouse
            '    If System.IO.File.Exists(My.Application.Info.DirectoryPath &
            '    "\Reports\Pend_Work_Local.RPT") Then
            '        Rpt = GetReportPath("Pend_Work_Local.RPT")
            '        Formula += " and (({Requisitions.InHouse} = True and " &
            '        "{Tests.InHouse} = True) or ({Requisitions.InHouse} = False and " &
            '        "(Not IsNull({TimeSensitiveTests.TGP_ID}) or Not " &
            '        "IsNull({TimeSensitiveTests_G.TGP_ID}))))"
            '    Else
            '        Rpt = GetReportPath("Pend_Work.RPT")
            '        Formula += " and {Requisitions.InHouse} = True and " &
            '        "{Tests.InHouse} = True"
            '    End If

            '    For i = 0 To lstWorksheets.Items.Count - 1
            '        If lstWorksheets.GetItemChecked(i) = True Then      'To print
            '            ItemX = lstWorksheets.Items(i)
            '            '
            '            Dim gRptAcc As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            '            gRptAcc.Load(Rpt)
            '            ApplyNewServer(gRptAcc, My.Settings.DSN, My.Settings.Database,
            '            My.Settings.UID, My.Settings.PWD)
            '            gRptAcc.RecordSelectionFormula = Formula & " and {Worksheets.ID} = " &
            '            ItemX.ItemData
            '            If cmbDestination.SelectedIndex = 0 Then    'Printer
            '                gRptAcc.PrintToPrinter(Val(txtCopies.Text), False, 0, 0)
            '                My.Application.DoEvents()
            '                gRptAcc.Close()
            '                gRptAcc = Nothing
            '            Else    'Screen
            '                Dim frmRptView As frmRV = New frmRV
            '                frmRptView.CRRV.ReportSource = gRptAcc
            '                frmRptView.Show()
            '            End If
            '        End If
            '    Next
            'ElseIf cmbSheets.SelectedIndex = 2 Then     'Tests outsource
            '    Rpt = GetReportPath("Pend_Work.RPT")
            '    Formula += " and {Requisitions.InHouse} = False or {Tests.InHouse} = False"
            '    For i = 0 To lstWorksheets.Items.Count - 1
            '        If lstWorksheets.GetItemChecked(i) = True Then      'To print
            '            ItemX = lstWorksheets.Items(i)
            '            '
            '            Dim gRptAcc As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            '            gRptAcc.Load(Rpt)
            '            ApplyNewServer(gRptAcc, My.Settings.DSN, My.Settings.Database,
            '            My.Settings.UID, My.Settings.PWD)
            '            gRptAcc.RecordSelectionFormula = Formula & " and {Worksheets.ID} = " &
            '            ItemX.ItemData
            '            If cmbDestination.SelectedIndex = 0 Then    'Printer
            '                gRptAcc.PrintToPrinter(Val(txtCopies.Text), False, 0, 0)
            '                My.Application.DoEvents()
            '                gRptAcc.Close()
            '                gRptAcc = Nothing
            '            Else    'Screen
            '                Dim frmRptView As frmRV = New frmRV
            '                frmRptView.CRRV.ReportSource = gRptAcc
            '                frmRptView.Show()
            '            End If
            '        End If
            '    Next
            'ElseIf cmbSheets.SelectedIndex = 3 Then     'Pending
            '    Rpt = GetReportPath("PendingSheet.RPT")
            '    If cmbDept.SelectedIndex > 0 Then   'Dept selected
            '        ItemX = cmbDept.SelectedItem
            '        Formula += " and {Tests.Department_ID} = " & ItemX.ItemData
            '    End If
            '    Dim gRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            '    gRpt.Load(Rpt)
            '    ApplyNewServer(gRpt, My.Settings.DSN, My.Settings.Database,
            '    My.Settings.UID, My.Settings.PWD)
            '    gRpt.RecordSelectionFormula = Formula
            '    If cmbDestination.SelectedIndex = 0 Then    'Printer
            '        gRpt.PrintToPrinter(Val(txtCopies.Text), False, 0, 0)
            '        My.Application.DoEvents()
            '        gRpt.Close()
            '        gRpt = Nothing
            '    Else    'Screen
            '        Dim frmRptView As frmRV = New frmRV
            '        frmRptView.CRRV.ReportSource = gRpt
            '        frmRptView.Show()
            '    End If
            'Else    'Results
            '    Rpt = GetReportPath("Result_Work.RPT")
            '    For i = 0 To lstWorksheets.Items.Count - 1
            '        If lstWorksheets.GetItemChecked(i) = True Then      'To print
            '            ItemX = lstWorksheets.Items(i)
            '            '
            '            Dim gRptAcc As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            '            gRptAcc.Load(Rpt)
            '            ApplyNewServer(gRptAcc, My.Settings.DSN, My.Settings.Database,
            '            My.Settings.UID, My.Settings.PWD)
            '            gRptAcc.RecordSelectionFormula = Formula & " and {Worksheets.ID} = " &
            '            ItemX.ItemData
            '            If cmbDestination.SelectedIndex = 0 Then    'Printer
            '                gRptAcc.PrintToPrinter(Val(txtCopies.Text), False, 0, 0)
            '                My.Application.DoEvents()
            '                gRptAcc.Close()
            '                gRptAcc = Nothing
            '            Else    'Screen
            '                Dim frmRptView As frmRV = New frmRV
            '                frmRptView.CRRV.ReportSource = gRptAcc
            '                frmRptView.Show()
            '            End If
            '        End If
            '    Next
            'End If
            '==============================================================================

            '
            'dtpDateFrom.Text = ""
            'dtpDateTo.Text = ""

            ClearDateTimePicker(dtpDateFrom)
            ClearDateTimePicker(dtpDateTo)

            txtAccIDFrom.Text = ""
            txtAccIDTo.Text = ""
            For i = 0 To lstWorksheets.Items.Count - 1
                lstWorksheets.SetItemChecked(i, False)
            Next
            btnPrint.Enabled = False
            'dtpDateFrom.Focus()

            dtpDateFrom.Focus()
        Else    'Minimum filling not done
            MsgBox("In order to process the report, specify " _
            & "a date, a range of dates or accessions", MsgBoxStyle.Critical, "Prolis")
            'dtpDateFrom.Focus()
            dtpDateFrom.Focus()
        End If
    End Sub

    Private Sub lstWorksheets_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstWorksheets.GotFocus
        lstWorksheets.BackColor = FCOLOR
    End Sub

    Private Sub lstWorksheets_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstWorksheets.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub lstWorksheets_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstWorksheets.LostFocus
        lstWorksheets.BackColor = NFCOLOR
    End Sub

    Private Sub lstWorksheets_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstWorksheets.SelectedIndexChanged
        PrintProgress()
    End Sub

    Private Sub txtAccIDFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccIDFrom.GotFocus
        txtAccIDFrom.BackColor = FCOLOR
    End Sub

    Private Sub txtAccIDFrom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAccIDFrom.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtAccIDFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccIDFrom.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAccIDTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccIDTo.GotFocus
        txtAccIDTo.BackColor = FCOLOR
    End Sub

    Private Sub txtAccIDTo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAccIDTo.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtAccIDTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccIDTo.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAccIDFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccIDFrom.LostFocus
        txtAccIDFrom.BackColor = NFCOLOR
        If txtAccIDFrom.Text <> "" Then
            'txtDateFrom.Text = ""
            'txtDateTo.Text = ""
            ClearDateTimePicker(dtpDateFrom)
            ClearDateTimePicker(dtpDateTo)
        End If
        PrintProgress()
    End Sub

    Private Sub txtAccIDTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccIDTo.LostFocus
        txtAccIDTo.BackColor = NFCOLOR
        If txtAccIDTo.Text <> "" Then
            'txtDateFrom.Text = ""
            'txtDateTo.Text = ""
            ClearDateTimePicker(dtpDateFrom)
            ClearDateTimePicker(dtpDateTo)
        End If
        PrintProgress()
    End Sub

    Private Sub btnSelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelAll.Click
        Dim i As Integer
        For i = 0 To lstWorksheets.Items.Count - 1
            lstWorksheets.SetItemChecked(i, True)
        Next
        PrintProgress()
    End Sub

    Private Sub btnDeselAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeselAll.Click
        Dim i As Integer
        For i = 0 To lstWorksheets.Items.Count - 1
            lstWorksheets.SetItemChecked(i, False)
        Next
        PrintProgress()
    End Sub

    Private Sub cmbSheets_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSheets.SelectedIndexChanged
        Dim i As Integer
        If cmbSheets.SelectedIndex = 0 Then 'Equipment
            For i = 0 To lstWorksheets.Items.Count - 1
                lstWorksheets.SetItemChecked(i, False)
            Next
            lstWorksheets.Enabled = False
        ElseIf cmbSheets.SelectedIndex = 1 _
        Or cmbSheets.SelectedIndex = 2 Then 'worksheets
            lstWorksheets.Enabled = True
        ElseIf cmbSheets.SelectedIndex = 3 Then 'Pending
            For i = 0 To lstWorksheets.Items.Count - 1
                lstWorksheets.SetItemChecked(i, False)
            Next
            lstWorksheets.Enabled = False
        Else    'Result
            lstWorksheets.Enabled = True
        End If
        PrintProgress()
    End Sub
    Private Sub dtpDateFrom_CloseUp(sender As Object, e As EventArgs) Handles dtpDateFrom.CloseUp
        ' After selecting a valid date, revert to the standard date format
        CloseUpDateTimePicker(dtpDateFrom)
    End Sub
    Private Sub dtpDateTo_CloseUp(sender As Object, e As EventArgs) Handles dtpDateTo.CloseUp
        CloseUpDateTimePicker(dtpDateTo)
    End Sub
    Private Sub lblClearDates_Click(sender As Object, e As EventArgs) Handles lblClearDates.Click
        ClearDateTimePicker(dtpDateFrom)
        ClearDateTimePicker(dtpDateTo)
    End Sub

    Private Sub dtpDateFrom_LostFocus(sender As Object, e As EventArgs) Handles dtpDateFrom.LostFocus, dtpDateTo.LostFocus
        PrintProgress()
    End Sub

End Class
