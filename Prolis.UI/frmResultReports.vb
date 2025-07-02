Imports System.IO
Imports System.Security.AccessControl
Imports DataTable = System.Data.DataTable

Public Class frmResultReports
    Private IsBusy As Boolean
    'Private Cancelled As Boolean
    Private sSQL As String
    Private IsCustomRPT As Boolean
    Private IsRPTFinal As Boolean
    Private IsConfigControl As Boolean
    Private Medium As Integer
    Public Shared ReportsFolder As String
    Private Sub frmResReports_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If IsBusy Then
            e.Cancel = True
        End If

    End Sub

    Private Sub frmResReports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ReportsFolder = ""
        InitializeConfiguration(MyLab.ID)
        waitingLabel.Hide()
        IsBusy = False
        grpDateRange.Text += " (" & SystemConfig.DateFormat & ")"
        dtpDateFrom.Format = DateTimePickerFormat.Custom
        dtpDateFrom.CustomFormat = SystemConfig.DateFormat
        dtpDateTo.Format = DateTimePickerFormat.Custom
        dtpDateTo.CustomFormat = SystemConfig.DateFormat
        cmbDestination.SelectedIndex = 0
        dgvDiscrete.RowCount = 1
        chkRPT.Checked = SystemConfig.CustomRPT
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub txtAccFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccFrom.GotFocus
        txtAccFrom.BackColor = FCOLOR
    End Sub

    Private Sub txtAccFrom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAccFrom.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub
    Private Sub txtAccFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccFrom.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAccTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccTo.GotFocus
        txtAccTo.BackColor = FCOLOR
    End Sub

    Private Sub txtAccTo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAccTo.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtAccTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccTo.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Me.Cursor = Cursors.WaitCursor
        'Try
        waitingLabel.Text = ""
        If cmbDestination.SelectedIndex = 6 AndAlso ReportsFolder = "" Then
            MessageBox.Show("Please Click on Destination Combo again to Select Directory to Save Reports.")
            Return
        End If

        IsBusy = True
        waitingLabel.Show()
        Dim sSQL As String = ""
        If chkConfig.Checked Then   'config control
            If chkDateRange.Checked = True Then 'Date Range used
                If chkAccRes.Checked = False Then   'RecDate
                    sSQL = "Select a.ID as AccID, c.Provider_Id as ProvID, b.Route_ID as RouteID from Requisitions a " &
                    "inner join (Req_RPT c inner join Providers b on c.Provider_ID = b.ID) on c.Base_ID = a.ID where " &
                    "c.RPT_Print <> 0 and ((c.RPT_Complete = 0 and (not (a.Reported_Final is null) or Not (ReportedOn is " &
                    "null))) or (c.RPT_Complete <> 0 and (not (a.Reported_Final is null)))) and a.ReceivedTime between '" &
                    Format(dtpDateFrom.Value, SystemConfig.DateFormat) & "' and '" & Format(dtpDateTo.Value,
                    SystemConfig.DateFormat & " 23:59:00") & "'"
                Else
                    sSQL = "Select a.ID as AccID, c.Provider_Id as ProvID, b.Route_ID as RouteID from Requisitions a " &
                    "inner join (Req_RPT c inner join Providers b on c.Provider_ID = b.ID) on c.Base_ID = a.ID where " &
                    "c.RPT_Print <> 0 and ((c.RPT_Complete <> 0 and a.Reported_Final between '" & Format(dtpDateFrom.Value,
                    SystemConfig.DateFormat) & "' and '" & Format(dtpDateTo.Value, SystemConfig.DateFormat & " 23:59:00") &
                    "') or (c.RPT_Complete = 0 and a.ReportedOn between '" & Format(dtpDateFrom.Value, SystemConfig.DateFormat) &
                    "' and '" & Format(dtpDateTo.Value, SystemConfig.DateFormat & " 23:59:00") & "'))"
                End If
                If chkProvider.Checked AndAlso Val(txtProviderID.Text) > 0 Then sSQL += " and b.ID = " & Val(txtProviderID.Text)
                If chkUnPrinted.Checked Then sSQL += " and not a.ID in (Select Accession_ID from Event_Capture where Event_ID = 11)"
            End If
        Else    'Manual
            If chkAccRange.Checked = True Then  'Accession Range
                If txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                    sSQL = "Select a.ID as AccID, b.Id as ProvID, b.Route_ID as RouteID from Requisitions a inner join Providers " &
                    "b on a.OrderingProvider_ID = b.ID where a.ID between " & Val(txtAccFrom.Text) & " and " & Val(txtAccTo.Text)
                    'Formula = "{Requisitions.ID} in [" & Val(txtAccFrom.Text) & " To " & _
                    'Val(txtAccTo.Text) & "]"
                ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                    sSQL = "Select a.ID as AccID, b.Id as ProvID, b.Route_ID as RouteID from Requisitions a " &
                    "inner join Providers b on a.OrderingProvider_ID = b.ID where a.ID = " & txtAccFrom.Text
                    'Formula = "{Requisitions.ID} = " & Val(txtAccFrom.Text)
                ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                    sSQL = "Select a.ID as AccID, b.Id as ProvID, b.Route_ID as RouteID from Requisitions a " &
                    "inner join Providers b on a.OrderingProvider_ID = b.ID where a.ID = " & Val(txtAccTo.Text)
                    'Formula = "{Requisitions.ID} = " & Val(txtAccTo.Text)
                End If
            Else    'Discrete Accessions
                If HasDiscreteValues() = True Then
                    sSQL = "Select a.ID as AccID, b.Id as ProvID, b.Route_ID as RouteID from Requisitions a " &
                    "inner join Providers b on a.OrderingProvider_ID = b.ID and a.ID in ("
                    'Formula = "{Requisitions.ID} in ["
                    Dim i As Integer
                    For i = 0 To dgvDiscrete.RowCount - 1
                        If Trim(dgvDiscrete.Rows(i).Cells(0).Value) <> "" Then _
                        sSQL = sSQL & Trim(dgvDiscrete.Rows(i).Cells(0).Value) & ", "
                        'Formula = Formula & dgvDiscrete.Rows(i).Cells(0).Value & ", "
                    Next
                    sSQL = sSQL.Substring(0, Len(sSQL) - 2) & ")"
                    'Formula = Formula.Substring(0, Len(Formula) - 2) & "]"
                End If
            End If
            '
            If chkDateRange.Checked = True Then
                If sSQL <> "" Then
                    If chkAccRes.Checked = False Then   'Accession
                        sSQL += " and a.ReceivedTime between '" & Format(dtpDateFrom.Value,
                        SystemConfig.DateFormat) & "' and '" & Format(dtpDateTo.Value, SystemConfig.DateFormat & " 23:59:00") & "'"
                    Else    'Result
                        sSQL += " and a.Reported_Final between '" & Format(dtpDateFrom.Value,
                        SystemConfig.DateFormat) & "' and '" & Format(dtpDateTo.Value, SystemConfig.DateFormat & " 23:59:00") & "'"
                    End If
                Else
                    If chkAccRes.Checked = False Then   'Received
                        sSQL = "Select a.ID as AccID, b.Id as ProvID, b.Route_ID as RouteID from Requisitions a inner join " &
                        "Providers b on a.OrderingProvider_ID = b.ID where a.ReceivedTime between '" & Format(dtpDateFrom.Value,
                        SystemConfig.DateFormat) & "' and '" & Format(dtpDateTo.Value, SystemConfig.DateFormat) & " 23:59:00'"
                    Else
                        sSQL = "Select a.ID as AccID, b.Id as ProvID, b.Route_ID as RouteID from Requisitions a inner join " &
                        "Providers b on a.OrderingProvider_ID = b.ID where a.Reported_Final between '" & Format(dtpDateFrom.Value,
                        SystemConfig.DateFormat) & "' and '" & Format(dtpDateTo.Value, SystemConfig.DateFormat) & " 23:59:00'"
                    End If
                End If
            End If
            '
            If chkProvider.Checked = True Then
                If txtProviderID.Text <> "" Then
                    If sSQL <> "" Then
                        sSQL = sSQL & " and b.ID = " & Val(txtProviderID.Text)
                        'Formula = Formula & " and {Providers.ID} = " & ItemX.ItemData
                    Else
                        sSQL = "Select a.ID as AccID, b.Id as ProvID, b.Route_ID as RouteID from Requisitions a inner join " &
                        "Providers b on a.OrderingProvider_ID = b.ID b.ID = " & Val(txtProviderID.Text)
                        'Formula = "{Providers.ID} = " & ItemX.ItemData
                    End If
                End If
            End If
            '
            If sSQL <> "" Then
                If chkComplete.Checked = True Then _
                sSQL = sSQL & " and Not (a.Reported_Final is NULL)"
            End If
            '
            If chkUnPrinted.Checked = True Then 'unprinted
                sSQL = sSQL & " and not a.ID in (Select Accession_ID from " &
                "Event_Capture where Event_ID = 11)"
            End If
        End If
        '
        If chkOrigCorr.Checked Then 'corrected only
            sSQL += " and Charindex('CORRECT', a.RPT_Status) > 0)"
        End If
        '
        If cmbSort.SelectedIndex = 1 Then   'Accession
            sSQL = sSQL & " order by a.ID"
        ElseIf cmbSort.SelectedIndex = 2 Then
            sSQL = sSQL & " order by b.ID"
        ElseIf cmbSort.SelectedIndex = 3 Then
            sSQL = sSQL & " order by b.Route_ID"
        Else
            sSQL = sSQL & " order by a.ID"
        End If
        '
        IsCustomRPT = chkRPT.Checked
        IsRPTFinal = chkComplete.Checked
        IsConfigControl = chkConfig.Checked
        Medium = cmbDestination.SelectedIndex
        '
        btnProcess.Enabled = False
        If Medium = 0 Then
            If BW.IsBusy Then BW.CancelAsync()
            Do Until Not BW.IsBusy
                My.Application.DoEvents()
            Loop
            BW.RunWorkerAsync(sSQL)
        Else
            If IsBusy Then ProcessSQL(sSQL)
            IsBusy = False
            'If Me.InvokeRequired = True Then
            '    Me.Invoke(New ClearForm(AddressOf FormClear))
            'Else
            FormClear()
            'End If
        End If
        'Else
        '    MsgBox("You must make a selection, to process the report", MsgBoxStyle.Critical, "Prolis")

        'Catch Ex As Exception
        '    'MsgBox(Ex.Message, MsgBoxStyle.Critical, "Prolis")
        'End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ProcessSQL(ByVal sSQL As String)
        Dim Recs As Integer
        Dim Accs() As String = {""}
        'Dim UID As String = My.Settings.UID.ToString
        'Dim PWD As String = My.Settings.PWD.ToString
        Dim RPTFile As String = ""
        Dim INSTRUCTS() As String
        Dim Configs() As String = {""}
        Dim RPTStatus As String = ""
        Dim dt As New DataTable

        Using cnrpts As New SqlClient.SqlConnection(connString)
            Try
                cnrpts.Open()
                Using cmdrpts As New SqlClient.SqlCommand(sSQL, cnrpts)
                    cmdrpts.CommandType = CommandType.Text
                    Using da As New SqlClient.SqlDataAdapter(cmdrpts)
                        da.Fill(dt)
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End Using
        '
        Recs = dt.Rows.Count
        If Recs > 0 Then
            If Medium = 0 Then  'Printer
                For i As Integer = 0 To Recs - 1
                    If Not BW.CancellationPending Then
                        Try
                            Dim AccID As String = dt.Rows(i).Item("AccID").ToString
                            'RPTFile = ValidateReportFile(dt.Rows(i).Item("AccID"), IsCustomRPT)
                            If IsConfigControl = False Then   'independent
                                'LogEvent(dt.Rows(i).Item("AccID"), 11, GetOrdProvIDFromAccID(dt.Rows(i).Item("AccID")),
                                'IIf(ReportFullResulted(dt.Rows(i).Item("AccID")) = True, "FINAL", "PARTIAL"), False,
                                'ThisUser.UserName, "Result Reports")
                                'If SystemConfig.AuditTrail = True Then _
                                'LogUserEvent(ThisUser.ID, 11, Date.Now, "REPORT", dt.Rows(i).Item("AccID"), "", "")

                                'PrintPDFReport(RPTFile, dt.Rows(i).Item("AccID").ToString)
                                RPTStatus = IIf(ReportFullResulted(AccID) = True, "FINAL", "PARTIAL")
                                LogEventAndAuditTrail(AccID, 11, RPTStatus)

                                Accs = {AccID}
                                GenerateReports(Accs, Medium, IsCustomRPT, "", txtQty.Text)

                            Else    'under control
                                Configs = GetReportConfigs(AccID)

                                For n As Integer = 0 To Configs.Length - 1
                                    If Configs(n) <> "" Then
                                        INSTRUCTS = Split(Configs(n), "|")
                                        RPTStatus = GetReportStatus(AccID)
                                        If CType(INSTRUCTS(2), Boolean) = True Then

                                            'WHAT IS THE PURPOSE OF THESE LINES?
                                            If Accs(UBound(Accs)) <> "" Then ReDim Preserve Accs(UBound(Accs) + 1)
                                            Accs(UBound(Accs)) = AccID

                                            'LogEvent(dt.Rows(i).Item("AccID"), 11, INSTRUCTS(0),
                                            'RPTStatus, False, ThisUser.UserName, "Result Reports")
                                            'If SystemConfig.AuditTrail = True Then _
                                            'LogUserEvent(ThisUser.ID, 11, Date.Now, "REPORT",
                                            'dt.Rows(i).Item("AccID"), "", "")
                                            'PrintPDFReport(RPTFile, dt.Rows(i).Item("AccID").ToString)

                                            LogEventAndAuditTrail(AccID, 11, RPTStatus, INSTRUCTS(0))

                                            GenerateReports({AccID}, Medium, IsCustomRPT, "", txtQty.Text)
                                        End If
                                    End If
                                Next n
                            End If

                            If BW.IsBusy Then
                                BW.ReportProgress((i + 1) * 100 / Recs, (i + 1).ToString & " of " & Recs.ToString)
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                        End Try
                    End If
                Next
            ElseIf Medium = 6 Then  'save Folder

                For i As Integer = 0 To Recs - 1
                    If Not BW.CancellationPending Then
                        Try
                            Dim AccID As String = dt.Rows(i).Item("AccID").ToString
                            'RPTFile = ValidateReportFile(dt.Rows(i).Item("AccID"), IsCustomRPT)
                            If IsConfigControl = False Then   'independent

                                'LogEvent(dt.Rows(i).Item("AccID"), 11, GetOrdProvIDFromAccID(dt.Rows(i).Item("AccID")),
                                'IIf(ReportFullResulted(dt.Rows(i).Item("AccID")) = True, "FINAL", "PARTIAL"), False,
                                'ThisUser.UserName, "Result Reports")
                                'If SystemConfig.AuditTrail = True Then _
                                'LogUserEvent(ThisUser.ID, 11, Date.Now, "REPORT", dt.Rows(i).Item("AccID"), "", "")

                                'PrintPDFReport(RPTFile, dt.Rows(i).Item("AccID").ToString, ReportsFolder & "\" & dt.Rows(i).Item("AccID").ToString)

                                RPTStatus = IIf(ReportFullResulted(AccID) = True, "FINAL", "PARTIAL")
                                LogEventAndAuditTrail(AccID, 11, RPTStatus)
                                Accs = {AccID}
                                GenerateReports(Accs, Medium, IsCustomRPT, ReportsFolder & "\" & AccID.ToString)

                            Else    'under control
                                Configs = GetReportConfigs(AccID)
                                For n As Integer = 0 To Configs.Length - 1
                                    If Configs(n) <> "" Then
                                        INSTRUCTS = Split(Configs(n), "|")
                                        RPTStatus = GetReportStatus(AccID)
                                        If CType(INSTRUCTS(2), Boolean) = True Then

                                            If Accs(UBound(Accs)) <> "" Then ReDim Preserve Accs(UBound(Accs) + 1)
                                            Accs(UBound(Accs)) = AccID

                                            'LogEvent(dt.Rows(i).Item("AccID"), 11, INSTRUCTS(0),
                                            'RPTStatus, False, ThisUser.UserName, "Result Reports")
                                            'If SystemConfig.AuditTrail = True Then _
                                            'LogUserEvent(ThisUser.ID, 11, Date.Now, "REPORT",
                                            'dt.Rows(i).Item("AccID"), "", "")

                                            'PrintPDFReport(RPTFile, dt.Rows(i).Item("AccID").ToString, ReportsFolder & "\" & dt.Rows(i).Item("AccID").ToString)

                                            LogEventAndAuditTrail(AccID, 11, RPTStatus, INSTRUCTS(0))

                                            GenerateReports({AccID}, Medium, IsCustomRPT, ReportsFolder & "\" & dt.Rows(i).Item("AccID").ToString)

                                        End If
                                    End If
                                Next n
                            End If
                            If BW.IsBusy Then
                                BW.ReportProgress((i + 1) * 100 / Recs, (i + 1).ToString & " of " & Recs.ToString)
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                        End Try
                    End If
                Next
                Dim inp = MessageBox.Show("Report(s) has been saved to folder, do you want to open folder", "INFO", MessageBoxButtons.OKCancel)
                If inp = DialogResult.OK Then

                    ' Open the folder containing the saved file
                    Dim folderPath As String = ReportsFolder
                    Process.Start(folderPath)

                End If
                waitingLabel.Text = "Report(s) has beed saved to folder."
            ElseIf Medium = 1 Then  'screen
                'waitingLabel.Text = "Loading..."
                'RPTFile = ValidateReportFile(dt.Rows(0).Item("AccID"), IsCustomRPT)
                PB.Value = 0
                For i As Integer = 0 To Recs - 1
                    If IsBusy Then
                        Try
                            Dim AccID As String = dt.Rows(i).Item("AccID").ToString

                            If Accs(UBound(Accs)) <> "" Then ReDim Preserve Accs(UBound(Accs) + 1)
                            Accs(UBound(Accs)) = AccID
                            'LogEvent(dt.Rows(i).Item("AccID"), 10, GetOrdProvIDFromAccID(dt.Rows(i).Item("AccID")),
                            'IIf(ReportFullResulted(dt.Rows(i).Item("AccID")) = True, "FINAL", "PARTIAL"), False,
                            'ThisUser.UserName, "Result Reports")
                            'If SystemConfig.AuditTrail = True Then _
                            'LogUserEvent(ThisUser.ID, 10, Date.Now, "REPORT",
                            'dt.Rows(i).Item("AccID"), "", "")
                            RPTStatus = IIf(ReportFullResulted(AccID) = True, "FINAL", "PARTIAL")
                            LogEventAndAuditTrail(AccID, 10, RPTStatus)

                            PB.Value = (i + 1) * 100 / Recs
                            lblStatus.Text = (i + 1).ToString & " of " & Recs.ToString
                            My.Application.DoEvents()
                            'BW.ReportProgress((i + 1) * 100 / Recs, (i + 1).ToString & " of " & Recs.ToString)
                            If BW.IsBusy Then
                                BW.ReportProgress((i + 1) * 100 / Recs, (i + 1).ToString & " of " & Recs.ToString)
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                        End Try
                    End If
                Next
                'PrintMultiReports(RPTFile, Accs, 1)

                GenerateReports(Accs, Medium, IsCustomRPT)

            ElseIf Medium = 2 Then  'Fax
                If IsConfigControl = False Then   'independent
                    'For i As Integer = 0 To Recs - 1
                    '    If IsBusy Then
                    '        ExecuteSqlProcedure("Delete from Event_Capture where Event_ID = 12 " &
                    '        "and Accession_ID = " & dt.Rows(i).Item("AccID"))
                    '        ExecuteSqlProcedure("Delete from Fax_Log where Status = 'Failed' " &
                    '        "and Accession_ID = " & dt.Rows(i).Item("AccID"))
                    '        PB.Value = (i + 1) * 100 / Recs
                    '        lblStatus.Text = (i + 1).ToString & " of " & Recs.ToString
                    '        My.Application.DoEvents()
                    '        'BW.ReportProgress((i + 1) * 100 / Recs, (i + 1).ToString & " of " & Recs.ToString)
                    '    End If
                    'Next

                    ProcessFax(dt, Recs, BW, PB, lblStatus)
                End If
            ElseIf Medium = 3 Then  'Interface
                If IsConfigControl = False Then   'independent
                    'For i As Integer = 0 To Recs - 1
                    '    If IsBusy Then
                    '        ExecuteSqlProcedure("Delete from Event_Capture where Event_ID in (" &
                    '         "14, 15) and Accession_ID = " & dt.Rows(i).Item("AccID"))
                    '        '
                    '        PB.Value = (i + 1) * 100 / Recs
                    '        lblStatus.Text = (i + 1).ToString & " of " & Recs.ToString
                    '        My.Application.DoEvents()
                    '        'BW.ReportProgress((i + 1) * 100 / Recs, (i + 1).ToString & " of " & Recs.ToString)
                    '    End If
                    'Next
                    ProcessInterface(dt, Recs, BW, PB, lblStatus)
                End If
            ElseIf Medium = 4 Then    'Prolison
                If IsConfigControl = False Then   'independent
                    'For i As Integer = 0 To Recs - 1
                    '    If IsBusy Then
                    '        ExecuteSqlProcedure("Delete from Event_Capture where Event_ID = 16 " &
                    '        "and Accession_ID = " & dt.Rows(i).Item("AccID"))
                    '        '
                    '        PB.Value = (i + 1) * 100 / Recs
                    '        lblStatus.Text = (i + 1).ToString & " of " & Recs.ToString
                    '        My.Application.DoEvents()
                    '        'BW.ReportProgress((i + 1) * 100 / Recs, (i + 1).ToString & " of " & Recs.ToString)
                    '    End If
                    'Next
                    ProcessProlison(dt, Recs, BW, PB, lblStatus)
                End If
            Else    'Email
                If IsConfigControl = False Then   'independent
                    'For i As Integer = 0 To Recs - 1
                    '    If IsBusy Then
                    '        ExecuteSqlProcedure("Delete from Event_Capture where Event_ID = 13 " &
                    '        "and Accession_ID = " & dt.Rows(i).Item("AccID"))
                    '        '
                    '        PB.Value = (i + 1) * 100 / Recs
                    '        lblStatus.Text = (i + 1).ToString & " of " & Recs.ToString
                    '        My.Application.DoEvents()
                    '        'BW.ReportProgress((i + 1) * 100 / Recs, (i + 1).ToString & " of " & Recs.ToString)
                    '    End If
                    'Next
                    ProcessEmail(dt, Recs, BW, PB, lblStatus)
                End If
            End If
        End If
        'If Me.InvokeRequired = True Then
        '    Me.Invoke(New ClearForm(AddressOf FormClear))
        'Else
        '    FormClear()
        'End If
    End Sub

    Private Sub PrintPDFReport(ByVal RPTFile As String, ByVal AccID As String, Optional ByVal SaveTo As String = "")
        'Try
        '    Dim SPDFS As New List(Of Byte())
        '    Dim FPDF As Byte() = Nothing
        '    Dim ExtCount As Integer = 0
        '    If AccID <> "" Then
        '        Dim FolderPath As String = My.Application.Info.DirectoryPath & "\Temp\" & ThisUser.ID.ToString & "\"
        '        If Not Directory.Exists(FolderPath) Then
        '            Directory.CreateDirectory(FolderPath)
        '            Dim UserAccount As String = "everyone" 'Specify the user here
        '            Dim FolderInfo As DirectoryInfo = New DirectoryInfo(FolderPath)
        '            Dim FolderAcl As New DirectorySecurity
        '            FolderAcl.AddAccessRule(New FileSystemAccessRule(UserAccount,
        '            FileSystemRights.Modify, InheritanceFlags.ContainerInherit Or
        '            InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow))
        '            FolderAcl.SetAccessRuleProtection(True, False) 'uncomment to remove existing permissions
        '            FolderInfo.SetAccessControl(FolderAcl)
        '        End If
        '        '
        '        Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        '        oRpt.Load(RPTFile)
        '        ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database,
        '        My.Settings.UID, My.Settings.PWD)
        '        oRpt.RecordSelectionFormula = "{Requisitions.ID} = " & AccID & " AND {Acc_Results.Result} <> '.'"
        '        Dim S As MemoryStream = oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat)
        '        SPDFS.Add(S.ToArray)
        '        Dim cnex As New SqlClient.SqlConnection(connstring)
        '        cnex.Open()
        '        Dim cmdex As New SqlClient.SqlCommand("Select Result from Extend_Results where Accession_ID = " & AccID, cnex)
        '        cmdex.CommandType = CommandType.Text
        '        Dim drex As SqlClient.SqlDataReader = cmdex.ExecuteReader
        '        If drex.HasRows Then
        '            While drex.Read
        '                SPDFS.Add(drex("Result"))
        '                ExtCount += 1
        '            End While
        '        End If
        '        cnex.Close()
        '        cnex = Nothing
        '        oRpt.Close()
        '        oRpt = Nothing
        '        '
        '        FPDF = PDFMerger.MergePDFStreams(SPDFS)
        '        For Each FlToDel As String In Directory.GetFiles(FolderPath, "*.*", SearchOption.AllDirectories)
        '            File.Delete(FlToDel)
        '        Next
        '        '
        '        Dim ms As New FileStream(FolderPath & AccID & ".PDF", FileMode.Create, FileAccess.ReadWrite, FileShare.Read)
        '        ms.Write(FPDF, 0, FPDF.Length)
        '        ms.Close()
        '        ms = Nothing

        '        If SaveTo = "" Then
        '            Dim PDFDOC As New Spire.Pdf.PdfDocument
        '            PDFDOC.LoadFromFile(FolderPath & AccID & ".PDF")
        '            PDFDOC.PrintSettings.PrinterName = GetDefaultPrinter()
        '            PDFDOC.Print()
        '        Else
        '            Try
        '                FileCopy(FolderPath & AccID & ".PDF", SaveTo & ".PDF")

        '            Catch ex As Exception

        '            End Try

        '        End If

        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub

    Private Sub PrintSingleReport(ByVal RPTFile As String, ByVal AccID As String, ByVal Device As Integer)
        Dim SPDFS As New List(Of Byte())
        Dim FPDF As Byte() = Nothing
        Dim ExtCount As Integer = 0
        If AccID <> "" Then
            Dim FolderPath As String = My.Application.Info.DirectoryPath & "\Temp\" & ThisUser.ID.ToString & "\"
            If Not Directory.Exists(FolderPath) Then
                Directory.CreateDirectory(FolderPath)
                Dim UserAccount As String = "everyone" 'Specify the user here
                Dim FolderInfo As DirectoryInfo = New DirectoryInfo(FolderPath)
                Dim FolderAcl As New DirectorySecurity
                FolderAcl.AddAccessRule(New FileSystemAccessRule(UserAccount,
                FileSystemRights.Modify, InheritanceFlags.ContainerInherit Or
                InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow))
                FolderAcl.SetAccessRuleProtection(True, False) 'uncomment to remove existing permissions
                FolderInfo.SetAccessControl(FolderAcl)
            End If
            '
            '==================================================
            'TODO: Crystal reports code
            'Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            'oRpt.Load(RPTFile)
            'ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database,
            'My.Settings.UID, My.Settings.PWD)
            'oRpt.RecordSelectionFormula = "{Requisitions.ID} = " & AccID & " AND {Acc_Results.Result} <> '.'"
            'Dim S As MemoryStream = oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat)
            'SPDFS.Add(S.ToArray)
            'Dim cner As New SqlClient.SqlConnection(connString)
            'cner.Open()
            'Dim cmder As New SqlClient.SqlCommand("Select Result from Extend_Results where Accession_ID = " & AccID, cner)
            'cmder.CommandType = CommandType.Text
            'Dim drer As SqlClient.SqlDataReader = cmder.ExecuteReader
            'If drer.HasRows Then
            '    While drer.Read
            '        SPDFS.Add(drer("Result"))
            '        ExtCount += 1
            '    End While
            'End If
            'cner.Close()
            'cner = Nothing
            'oRpt.Close()
            'oRpt = Nothing
            '
            '================================================================
            FPDF = PdfHelper.MergePDFStreams(SPDFS)
            '
            For Each FlToDel As String In Directory.GetFiles(FolderPath, "*.*", SearchOption.TopDirectoryOnly)
                File.Delete(FlToDel)
            Next
            '
            Dim ms As New FileStream(FolderPath & AccID & ".PDF", FileMode.Create, FileAccess.ReadWrite, FileShare.Delete)
            ms.Write(FPDF, 0, FPDF.Length)
            ms.Close()
            ms = Nothing
            '
            'Dim PDFDOC As New Spire.Pdf.PdfDocument
            'PDFDOC.LoadFromFile(FolderPath & AccID & ".PDF")
            If Device = 0 Then  'printer
                'Dim PS As New System.Drawing.Printing.PrinterSettings
                'PDFDOC.PrintSettings.PrinterName = PS.PrinterName
                'PDFDOC.Print()
                'My.Application.DoEvents()
            ElseIf Device = 1 Then  'screen
                'If PDFRV_foxit.IsHandleCreated Then PDFRV_foxit.Close()
                'PDFRV_foxit.PdfViewer1.Refresh()
                'PDFRV_foxit.PdfViewer1.Open(New Foxit.PDF.Viewer.PdfDocument(FPDF)
                '   )


                ''If PDFRV.IsHandleCreated Then PDFRV.Close()

                'PDFRV_foxit.FilePath = FolderPath & AccID & ".PDF"

                'PDFRV_foxit.Show()
            End If
        End If
    End Sub

    Private Sub PrintMultiReports(ByVal RPTFile As String, ByVal AccIDs() As String, ByVal Device As Integer)
        Dim S As New MemoryStream
        'Try
        Dim AccID As String = ""
        Dim SPDFS As New List(Of Byte())
        Dim FPDF As Byte() = Nothing
        Dim ExtCount As Integer = 0
        If AccIDs(0) <> "" Then
            Dim FolderPath As String = My.Application.Info.DirectoryPath & "\Temp\" & ThisUser.ID.ToString & "\"
            If Not Directory.Exists(FolderPath) Then
                Directory.CreateDirectory(FolderPath)
                Dim UserAccount As String = "everyone" 'Specify the user here
                Dim FolderInfo As DirectoryInfo = New DirectoryInfo(FolderPath)
                Dim FolderAcl As New DirectorySecurity
                FolderAcl.AddAccessRule(New FileSystemAccessRule(UserAccount,
                FileSystemRights.Modify, InheritanceFlags.ContainerInherit Or
                InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow))
                FolderAcl.SetAccessRuleProtection(True, False) 'uncomment to remove existing permissions
                FolderInfo.SetAccessControl(FolderAcl)
            End If


            For i As Integer = 0 To AccIDs.Length - 1
                If AccIDs(i) <> "" Then
                    AccID = AccIDs(i)
                    '==================================================
                    'TODO: Crystal reports code
                    'Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'oRpt.Load(RPTFile, CrystalDecisions.Shared.OpenReportMethod.OpenReportByTempCopy)
                    'ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database,
                    'My.Settings.UID, My.Settings.PWD)
                    'oRpt.RecordSelectionFormula = "{Requisitions.ID} = " & AccIDs(i) & " AND {Acc_Results.Result} <> '.'"
                    'Try
                    '    oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat).CopyTo(S)
                    '    S.Position = 0

                    '    'S = oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                    '    ')
                    'SPDFS.Add(S.ToArray)
                    '    S.Close()
                    '    S = Nothing
                    'Catch ex As Exception
                    '    Dim err As String = ex.Message()
                    'End Try

                    ''
                    'Dim cner As New SqlClient.SqlConnection(connString)
                    'cner.Open()
                    'Dim cmder As New SqlClient.SqlCommand("Select Result from Extend_Results where Accession_ID = " & AccID, cner)
                    'cmder.CommandType = CommandType.Text
                    'Dim drer As SqlClient.SqlDataReader = cmder.ExecuteReader
                    'If drer.HasRows Then
                    '    While drer.Read
                    '        SPDFS.Add(drer("Result"))
                    '        ExtCount += 1
                    '    End While
                    'End If
                    'cner.Close()
                    'cner = Nothing
                    ''
                    'oRpt.Close()
                    'oRpt = Nothing
                    ''===========================================================
                End If
            Next
            FPDF = PdfHelper.MergePDFStreams(SPDFS)
            '
            For Each FlToDel As String In Directory.GetFiles(FolderPath, "*.*", SearchOption.TopDirectoryOnly)
                File.Delete(FlToDel)
            Next
            '
            Dim ms As New FileStream(FolderPath & AccID & ".PDF", FileMode.Create, FileAccess.ReadWrite, FileShare.Delete)
            ms.Write(FPDF, 0, FPDF.Length)
            ms.Close()
            ms = Nothing
            '
            If Device = 0 Then  'printer
                'Dim PDFDOC As New Spire.Pdf.PdfDocument
                'PDFDOC.LoadFromFile(FolderPath & AccID & ".PDF")
                'Dim PS As New System.Drawing.Printing.PrinterSettings
                'PDFDOC.PrintSettings.PrinterName = PS.PrinterName
                'PDFDOC.Print()
                'My.Application.DoEvents()
            ElseIf Device = 1 Then  'screen

                'If PDFRV_foxit.IsHandleCreated Then PDFRV_foxit.Close()
                'PDFRV_foxit.PdfViewer1.Refresh()
                'PDFRV_foxit.PdfViewer1.Open(New Foxit.PDF.Viewer.PdfDocument(FPDF)
                '   )

                ''Temuree
                ''If PDFRV.IsHandleCreated Then PDFRV.Close()
                ''PDFRV.AxAcroPDF1.src = FolderPath & AccID & ".PDF"
                'PDFRV_foxit.FilePath = FolderPath & AccID & ".PDF"

                'PDFRV_foxit.Show()
            End If
        End If
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical, "Prolis")
        'Finally
        '    If oRpt IsNot Nothing Then
        '        oRpt.Close()
        '        oRpt = Nothing
        '    End If
        '    If S IsNot Nothing Then
        '        S.Close()
        '        S = Nothing
        '    End If
        'End Try
    End Sub

    Private Sub ForceAccDisbursement(ByVal AccID As Long)
        Try
            Dim FacilityID As Integer = -1
            Dim cnad As New SqlClient.SqlConnection(connString)
            cnad.Open()
            Dim cmdad As New SqlClient.SqlCommand("Select * from External_Interfaces " &
            "where IsActive <> 0 and FacilityType_ID = 5 and Facility_ID in (Select " &
            "OrderingProvider_ID from Requisitions where ID = " & AccID & ")", cnad)
            cmdad.CommandType = CommandType.Text
            Dim drad As SqlClient.SqlDataReader = cmdad.ExecuteReader
            If drad.HasRows Then
                While drad.Read
                    FacilityID = drad("Facility_ID")
                End While
            End If
            cnad.Close()
            cnad = Nothing
            Dim RptStatus As String = GetReportStatus(AccID)
            If FacilityID <> -1 And RptStatus <> "" Then
                ExecuteSqlProcedure("If Exists (Select * from Result_Disbursement where Provider_ID = " &
                FacilityID & " and Accession_ID = " & AccID & ") Update Result_Disbursement set " &
                "DisburseDate = '" & Date.Now & "', Routed = 0, RptStatus = '" & RptStatus & "' where " &
                "Provider_ID = " & FacilityID & " and Accession_ID = " & AccID & " Else Insert into " &
                "Result_Disbursement (Provider_ID, Accession_ID, DisburseDate, Routed, RptStatus) " &
                "values (" & FacilityID & ", " & AccID & ", '" & Date.Now & "', 0, '" & RptStatus & "')")
            Else
                ExecuteSqlProcedure("Delete from Result_Disbursement where " &
                "Provider_ID = " & FacilityID & " and Accession_ID = " & AccID)
            End If
        Catch Ex As Exception
        End Try
    End Sub

    Private Function GetProviderFax(ByVal AccID As Long) As String
        Dim Fax As String = ""
        Dim cnpf As New SqlClient.SqlConnection(connString)
        cnpf.Open()
        Dim cmdpf As New SqlClient.SqlCommand("Select * from " &
        "Providers where ID in (Select OrderingProvider_ID " &
        "from Requisitions where ID = " & AccID & ")", cnpf)
        cmdpf.CommandType = CommandType.Text
        Dim drpf As SqlClient.SqlDataReader = cmdpf.ExecuteReader
        If drpf.HasRows Then
            While drpf.Read
                If drpf("Fax") IsNot DBNull.Value _
                Then Fax = PhoneNeat(drpf("Fax"))
            End While
        End If
        cnpf.Close()
        cnpf = Nothing
        Return Fax
    End Function

    Private Function GetPatientID(ByVal AccID As Long) As Long
        Dim PatientID As Long = -1
        Dim cnpi As New SqlClient.SqlConnection(connString)
        cnpi.Open()
        Dim cmdpi As New SqlClient.SqlCommand("Select " &
        "Patient_ID from Requisitions where ID = " & AccID, cnpi)
        cmdpi.CommandType = CommandType.Text
        Dim drpi As SqlClient.SqlDataReader = cmdpi.ExecuteReader
        If drpi.HasRows Then
            While drpi.Read
                PatientID = drpi("Patient_ID")
            End While
        End If
        cnpi.Close()
        cnpi = Nothing
        Return PatientID
    End Function

    'Private Function GetReportConfigs(ByVal AccID As Long) As String()
    '    Dim Configs() As String = {""}
    '    '"", "", "", "", "", "", "", "", "", ""
    '    '0=ProviderID, 1=Complete, 2=Print, 3=Prolison, 4=Interface, 5=RPTFax
    '    '6=Fax#, 7=RPTEmail, 8=Email, 9=RPTFile
    '    Dim cnrc As New SqlClient.SqlConnection(connstring)
    '    cnrc.Open()
    '    Dim cmdrc As New SqlClient.SqlCommand("Select a.*, b.ResRPTFile from Req_RPT a " &
    '    "inner join Providers b on a.Provider_ID = b.ID where a.Base_ID = " & AccID, cnrc)
    '    cmdrc.CommandType = CommandType.Text
    '    Dim drrc As SqlClient.SqlDataReader = cmdrc.ExecuteReader
    '    If drrc.HasRows Then
    '        While drrc.Read
    '            If Configs(UBound(Configs)) <> "" Then ReDim Preserve Configs(UBound(Configs) + 1)
    '            Configs(UBound(Configs)) = drrc("Provider_ID").ToString &
    '            "|" & Convert.ToInt32(drrc("RPT_Complete")) & "|" &
    '            Convert.ToInt32(drrc("RPT_Print")) & "|" &
    '            Convert.ToInt32(drrc("RPT_ProlisOn")) & "|" &
    '            Convert.ToInt32(drrc("RPT_Interface")) & "|" &
    '            Convert.ToInt32(drrc("RPT_Fax")) & "|"
    '            If drrc("Fax") IsNot DBNull.Value AndAlso Trim(drrc("Fax")) <> "" Then
    '                Configs(UBound(Configs)) += drrc("Fax") & "|"
    '            Else
    '                Configs(UBound(Configs)) += "|"
    '            End If
    '            Configs(UBound(Configs)) += Convert.ToInt32(drrc("RPT_Email")) & "|"
    '            If drrc("Email") IsNot DBNull.Value AndAlso Trim(drrc("Email")) <> "" Then
    '                Configs(UBound(Configs)) += drrc("Email") & "|"
    '            Else
    '                Configs(UBound(Configs)) += "|"
    '            End If
    '            If drrc("ResRPTFile") IsNot DBNull.Value AndAlso Trim(drrc("ResRPTFile")) <> "" Then
    '                Configs(UBound(Configs)) += Trim(drrc("ResRPTFile")) & vbCr
    '            Else
    '                Configs(UBound(Configs)) += vbCr
    '            End If
    '        End While
    '    End If
    '    cnrc.Close()
    '    cnrc = Nothing
    '    Return Configs
    'End Function

    'Public Delegate Sub ClearForm()

    Private Sub FormClear()
        chkAccRange.Checked = True
        txtAccFrom.Text = ""
        txtAccTo.Text = ""
        chkDateRange.Checked = False
        chkProvider.Checked = False
        If Not cmbDestination.SelectedIndex = 6 Then
            cmbDestination.SelectedIndex = 0
        End If

        chkComplete.Checked = False
        chkUnPrinted.Checked = False
        btnProcess.Enabled = False
        waitingLabel.Hide()
    End Sub

    Private Sub txtAccFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccFrom.LostFocus
        txtAccFrom.BackColor = NFCOLOR
    End Sub
    Private Sub txtAccFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccFrom.Validated
        Update_Progress()
    End Sub

    Private Sub chkAccRange_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAccRange.CheckedChanged
        If chkAccRange.Checked = True Then
            grpAccRange.Enabled = True
            DiscreteClear()
            dgvDiscrete.Enabled = False
            'cmbSort.Enabled = True
        Else
            txtAccFrom.Text = ""
            txtAccTo.Text = ""
            grpAccRange.Enabled = False
            dgvDiscrete.Enabled = True
            'cmbSort.SelectedIndex = 0
            'cmbSort.Enabled = False
        End If
        Update_Progress()
    End Sub

    Private Sub txtAccTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccTo.LostFocus
        txtAccTo.BackColor = NFCOLOR
    End Sub
    Private Sub txtAccTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccTo.Validated
        Update_Progress()
    End Sub
    Private Sub Update_Progress()
        If txtAccFrom.Text <> "" Or txtAccFrom.Text <> "" Or txtProviderID.Text <> "" _
        Or HasDiscreteValues() = True Or chkDateRange.Checked = True Then
            btnProcess.Enabled = True
        Else
            btnProcess.Enabled = False
        End If
    End Sub
    Private Function HasDiscreteValues() As Boolean
        Dim HasVal As Boolean = False
        Dim i As Integer
        For i = 0 To dgvDiscrete.RowCount - 1
            If dgvDiscrete.Rows(i).Cells(0).Value <> "" Then
                HasVal = True
                Exit For
            End If
        Next
        HasDiscreteValues = HasVal
    End Function
    Private Sub dtpDateFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDateFrom.ValueChanged
        Update_Progress()
    End Sub

    Private Sub dtpDateTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDateTo.ValueChanged
        Update_Progress()
    End Sub

    Private Sub cmbProvider_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Update_Progress()
    End Sub

    Private Sub dgvDiscrete_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDiscrete.CellEndEdit
        If Trim(dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) <> "" Then
            If IsNumeric(Trim(dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)) = False Then
                MsgBox("Only digits are allowed.")
                dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
            Else
                If IsDuplicate(Trim(dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)) Then
                    MsgBox("Duplicate Entry is not allowed.")
                    dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
                End If
            End If
            If e.RowIndex = dgvDiscrete.RowCount - 1 Then
                dgvDiscrete.Rows.Add()
                SendKeys.Send("{ENTER}")
            End If
        Else
            If e.RowIndex < dgvDiscrete.RowCount - 1 Then
                dgvDiscrete.Rows.RemoveAt(e.RowIndex)
            End If
        End If
        Update_Progress()
    End Sub

    Private Function IsDuplicate(ByVal AccID As Long) As Boolean
        Dim i As Integer
        Dim CT As Integer = 0
        For i = 0 To dgvDiscrete.RowCount - 1
            If dgvDiscrete.Rows(i).Cells(0).Value = AccID.ToString Then CT += 1
        Next
        If CT > 1 Then
            IsDuplicate = True
        Else
            IsDuplicate = False
        End If
    End Function

    Private Sub DiscreteClear()
        Dim i As Integer
        For i = dgvDiscrete.RowCount - 1 To 0 Step -1
            dgvDiscrete.Rows(i).Cells(0).Value = ""
            If i > 0 Then dgvDiscrete.Rows.RemoveAt(i)
        Next
    End Sub
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        DiscreteClear()
        Update_Progress()
    End Sub

    Private Sub chkProvider_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkProvider.CheckedChanged
        If chkProvider.Checked = True Then
            grpProvider.Enabled = True
        Else
            txtProviderID.Text = ""
            txtProviderName.Text = ""
            grpProvider.Enabled = False
        End If
    End Sub

    Private Sub chkDateRange_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDateRange.CheckedChanged
        If chkDateRange.Checked = True Then
            grpDateRange.Enabled = True
        Else
            grpDateRange.Enabled = False
        End If
        Update_Progress()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If IsBusy Then
            IsBusy = False
        Else
            Me.Close()
        End If
    End Sub

    Private Sub txtQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtQty_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.Validated
        If Val(txtQty.Text) < 1 Or Val(txtQty.Text) > 20 Then txtQty.Text = "1"
    End Sub

    Private Sub cmbDestination_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDestination.SelectedIndexChanged
        If cmbDestination.SelectedIndex = 0 Then
            txtQty.Enabled = True
        Else
            txtQty.Enabled = False
        End If
        If cmbDestination.SelectedIndex = 6 Then
            Dim folderBrowserDialog1 As New FolderBrowserDialog()

            ' Set the initial directory (optional)
            folderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)

            ' Show the dialog and check if the user clicked OK
            If folderBrowserDialog1.ShowDialog() = DialogResult.OK Then
                ' Get the selected folder path
                Dim selectedFolderPath As String = folderBrowserDialog1.SelectedPath
                ReportsFolder = selectedFolderPath
            End If
        End If

    End Sub

    Private Sub chkRPT_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRPT.CheckedChanged
        If chkRPT.Checked = False Then  'Generic
            chkRPT.Text = "GENERIC"
        Else
            chkRPT.Text = "CUSTOM"
        End If
    End Sub

    Private Sub chkConfig_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkConfig.CheckedChanged
        If chkConfig.Checked = False Then
            EnableCustomeries()
        Else
            DisableCustomeries()
        End If
    End Sub

    Private Sub EnableCustomeries()
        chkAccRange.Enabled = True
        If Not chkAccRange.Checked Then dgvDiscrete.Enabled = True
        chkComplete.Enabled = True
        cmbDestination.Enabled = True
    End Sub

    Private Sub DisableCustomeries()
        chkAccRange.Checked = False
        dgvDiscrete.RowCount = 1
        dgvDiscrete.Rows(0).Cells(0).Value = ""
        dgvDiscrete.Enabled = False
        chkAccRange.Enabled = False
        chkComplete.Checked = False
        chkComplete.Enabled = False
        If Not cmbDestination.SelectedIndex = 6 Then
            cmbDestination.SelectedIndex = 0
        End If

        cmbDestination.Enabled = False
    End Sub

    Private Sub chkAccRes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAccRes.CheckedChanged
        If chkAccRes.Checked = False Then
            chkAccRes.Text = "Received"
        Else
            chkAccRes.Text = "Released"
        End If
    End Sub

    Private Sub txtProviderID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtProviderID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtProviderID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProviderID.Validated
        If txtProviderID.Text = "" Then
            txtProviderName.Text = ""
        Else
            Dim cnpv As New SqlClient.SqlConnection(connString)
            cnpv.Open()
            Dim cmdpv As New SqlClient.SqlCommand("Select * from " &
            "Providers where ID = " & Val(txtProviderID.Text), cnpv)
            cmdpv.CommandType = CommandType.Text
            Dim drpv As SqlClient.SqlDataReader = cmdpv.ExecuteReader
            If drpv.HasRows Then
                While drpv.Read
                    If drpv("IsIndividual") = False Then 'Entity
                        txtProviderName.Text = drpv("LastNAme_BSN")
                    Else
                        If drpv("Degree") IsNot DBNull.Value _
                        AndAlso drpv("Degree") <> "" Then
                            txtProviderName.Text = drpv("LastNAme_BSN") & ", " &
                            drpv("FirstNAme") & " " & drpv("Degree")
                        Else
                            txtProviderName.Text = drpv("LastNAme_BSN") & ", " &
                            drpv("FirstNAme")
                        End If
                    End If
                End While
            End If
            cnpv.Close()
            cnpv = Nothing
        End If
    End Sub

    Private Sub btnProvLookUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProvLookUp.Click
        Dim ProviderID As String = frmProviderLookup.ShowDialog
        If ProviderID <> "" Then
            txtProviderID.Text = ProviderID
        End If
    End Sub

    Private Sub dgvDiscrete_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvDiscrete.CellMouseUp
        If e.Button = MouseButtons.Right Then
            If Clipboard.ContainsText Then
                dgvDiscrete.Rows.Clear()
                Dim Accs() As String = Split(Clipboard.GetText, vbCrLf)
                For i As Integer = 0 To Accs.Length - 1
                    If Trim(Accs(i)) <> "" Then
                        If dgvDiscrete.RowCount = 0 Then dgvDiscrete.Rows.Add()
                        If dgvDiscrete.Rows(dgvDiscrete.RowCount - 1).Cells(0).Value _
                        = "" Then
                            dgvDiscrete.Rows(dgvDiscrete.RowCount - 1).Cells(0).Value = Trim(Accs(i))
                        Else
                            dgvDiscrete.Rows.Add()
                            dgvDiscrete.Rows(dgvDiscrete.RowCount - 1).Cells(0).Value = Trim(Accs(i))
                        End If
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub BW_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BW.DoWork
        ProcessSQL(e.Argument)
    End Sub

    Private Sub BW_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BW.ProgressChanged
        PB.Value = e.ProgressPercentage
        lblStatus.Text = e.UserState
    End Sub

    Private Sub BW_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BW.RunWorkerCompleted
        'If Me.InvokeRequired = True Then
        '    Me.Invoke(New ClearForm(AddressOf FormClear))
        'Else

        If e.Error IsNot Nothing Then
            MessageBox.Show("An error occurred: " & e.Error.Message)
        ElseIf e.Cancelled Then
            MessageBox.Show("Operation was cancelled.")
            'Else
            '    waitingLabel.Text = "Operation completed successfully."
        End If

        FormClear()
        'End If
        IsBusy = False
        If BW.CancellationPending = True Then Me.Close()
    End Sub

    Private Sub chkOrigCorr_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOrigCorr.CheckedChanged
        If chkOrigCorr.Checked = False Then
            chkOrigCorr.Text = "Original"
        Else
            chkOrigCorr.Text = "Corrected"
        End If
    End Sub

    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.Click
        HelpProvider1.HelpNamespace = My.Application.Info.DirectoryPath & "\ProlisHelp.chm"
        HelpProvider1.SetHelpKeyword(Me, 4)
        HelpProvider1.SetHelpNavigator(Me, HelpNavigator.TopicId)
        Help.ShowHelp(Me, My.Application.Info.DirectoryPath & "\ProlisHelp.chm")
    End Sub

    Private Sub frmResultReports_MouseClick(sender As Object, e As MouseEventArgs) Handles MyBase.MouseClick
        Update_Progress()
    End Sub
End Class
