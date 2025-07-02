Option Compare Text
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmApplyLabResults
    Dim IsRunning As Boolean = False

    Private Sub frmApplyLabResults_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If IsRunning Then
            e.Cancel = True
            IsRunning = False
        End If
    End Sub

    Private Sub frmApplyLabResults_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PopulateLabs()
        ConfigureDateTimePicker(dtpDateFrom)
        ConfigureDateTimePicker(dtpDateTo)
        lblFrom.Text += " (" & SystemConfig.DateFormat & ")"
        lblTo.Text += " (" & SystemConfig.DateFormat & ")"
        'txtDateFrom.Text = Format(DateAdd(DateInterval.Day, -1, Date.Today), SystemConfig.DateFormat)
        'txtDateTo.Text = Format(DateAdd(DateInterval.Day, -1, Date.Today), SystemConfig.DateFormat)
        cmbStatus.SelectedIndex = 1
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub PopulateLabs()
        cmbLabs.Items.Clear()
        Dim sSQL As String = "Select * from Labs where ID in (Select distinct Lab_ID from Lab_Results)"
        If connString <> "" Then
            Dim cnpl As New SqlClient.SqlConnection(connString)
            cnpl.Open()
            Dim cmdpl As New SqlClient.SqlCommand(sSQL, cnpl)
            cmdpl.CommandType = CommandType.Text
            Dim drpl As SqlClient.SqlDataReader = cmdpl.ExecuteReader
            If drpl.HasRows Then
                While drpl.Read
                    cmbLabs.Items.Add(New MyList(drpl("LabName"), drpl("ID")))
                End While
            End If
            cnpl.Close()
            cnpl = Nothing
            'Else
            '    Dim cnpl As New Odbc.OdbcConnection(connstring)
            '    cnpl.Open()
            '    Dim cmdpl As New Odbc.OdbcCommand(sSQL, cnpl)
            '    cmdpl.CommandType = CommandType.Text
            '    Dim drpl As Odbc.OdbcDataReader = cmdpl.ExecuteReader
            '    If drpl.HasRows Then
            '        While drpl.Read
            '            cmbLabs.Items.Add(New MyList(drpl("LabName"), drpl("ID")))
            '        End While
            '    End If
            '    cnpl.Close()
            '    cnpl = Nothing
        End If
    End Sub

    Private Sub LoadResults(ByVal LabID As Integer)
        Dim sSQL As String = ""
        Dim BadAccs As String = ""
        Dim BadTGPs As String = ""
        Dim Accs() As String = {""}
        Dim TGPINFO() As String
        Dim TESTID As String = ""
        Dim TGPType As String = ""
        Dim TESTNAME As String = ""
        Dim Result As String = ""
        Dim NormalRange As String = ""
        Dim UOM As String = ""
        Dim Comment As String = ""
        Dim TResult As String = ""
        lblStatus.Text = ""
        sSQL = "Select * from Lab_Results where Lab_ID = " & LabID
        If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
            sSQL += " and Accession_ID in (Select Accession_ID from Sendouts where Lab_ID = " & LabID &
            " and SentDate between '" & dtpDateFrom.Text & "' and '" & dtpDateFrom.Text & " 23:59:00')"
            sSQL += " and IsNumeric(Accession_ID) <> 0"
        ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
            sSQL += " and Accession_ID in (Select Accession_ID from Sendouts where Lab_ID = " & LabID &
           " and SentDate between '" & dtpDateTo.Text & "' and '" & dtpDateTo.Text & " 23:59:00')"
            sSQL += " and IsNumeric(Accession_ID) <> 0"
        ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
            sSQL += " and Accession_ID in (Select Accession_ID from Sendouts where Lab_ID = " & LabID &
            " and SentDate between '" & dtpDateFrom.Text & "' and '" & dtpDateTo.Text & " 23:59:00')"
            sSQL += " and IsNumeric(Accession_ID) <> 0"
        ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
            sSQL += " and Accession_ID = '" & Trim(txtAccFrom.Text) & "'"
        ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
            sSQL += " and Accession_ID = '" & Trim(txtAccTo.Text) & "'"
        ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
            sSQL += " and Accession_ID between '" & Trim(txtAccFrom.Text) & "' and '" & Trim(txtAccTo.Text) & "'"
        End If
        '
        If cmbStatus.SelectedIndex = 1 Then     'FINAL
            sSQL += " and (Status like 'F%' OR Status like 'C%')"
        ElseIf cmbStatus.SelectedIndex = 2 Then     'Preliminary
            sSQL += " and Status like 'P%'"
        End If
        dgvResults.Rows.Clear()
        btnProcess.Enabled = False
        IsRunning = True
        Dim i As Integer = 1
        Dim Recs As Integer = 0
        '
        Dim cnsql As New SqlClient.SqlConnection(connString)
        cnsql.Open()
        Dim selcmd As New SqlClient.SqlCommand(sSQL, cnsql)
        selcmd.CommandType = CommandType.Text
        Dim selDR As SqlClient.SqlDataReader = selcmd.ExecuteReader
        If selDR.HasRows Then
            While selDR.Read
                If IsRunning Then
                    If IsNumeric(selDR("Accession_ID")) Then
                        If Accs(UBound(Accs)) <> "" Then    'subsequent entries
                            If InStr(Accs(UBound(Accs)), selDR("Accession_ID")) = 0 Then
                                Accs(UBound(Accs)) = selDR("Accession_ID")
                            Else
                                ReDim Preserve Accs(UBound(Accs) + 1)
                                If InStr(Accs(UBound(Accs)), selDR("Accession_ID")) _
                                = 0 Then Accs(UBound(Accs)) = selDR("Accession_ID")
                            End If
                        Else    'First time
                            Accs(UBound(Accs)) = selDR("Accession_ID")
                        End If
                        TGPINFO = GetProlisTestID(LabID, selDR("Test_ID"))
                        If TGPINFO(0) <> "" AndAlso IsNumeric(TGPINFO(0)) Then
                            TESTID = TGPINFO(0)
                            TESTNAME = TGPINFO(1)
                            TGPType = TGPINFO(2)
                        Else
                            TESTNAME = "" : TGPType = ""
                        End If
                        If selDR("Result") IsNot DBNull.Value Then
                            Result = selDR("Result")
                        Else
                            Result = ""
                        End If
                        NormalRange = Trim(selDR("NormalRange"))
                        UOM = Trim(selDR("Unit"))
                        If selDR("Comment") IsNot DBNull.Value _
                        AndAlso selDR("Comment") <> "" Then
                            Comment = selDR("Comment")
                        Else
                            Comment = ""
                        End If
                        If selDR("T_Result") IsNot DBNull.Value _
                        AndAlso selDR("T_Result") <> "" Then
                            TResult = selDR("T_Result")
                        Else
                            TResult = ""
                        End If
                        'If TGPType = "T" Or TGPType = "" Then   'good or unmapped
                        If TESTNAME <> "" AndAlso (Result <> "" _
                        Or Comment <> "" Or TResult <> "") Then  'good TGP
                            dgvResults.Rows.Add(IIf(Result <> "" Or Comment <> "" Or TResult <> "", True, False),
                            selDR("Accession_ID"), TESTID, TESTNAME, Trim(Result), Trim(selDR("Flag")), NormalRange,
                            selDR("Status"), Comment, TResult, UOM, selDR("LabID"))
                            '
                            dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).Style.BackColor =
                            dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Style.BackColor
                        Else    'unmapped
                            dgvResults.Rows.Add(False, selDR("Accession_ID"), selDR("Test_ID"), TESTNAME, Result,
                            Trim(selDR("Flag")), NormalRange, selDR("Status"), Comment, TResult, UOM, selDR("LabID"))
                            '
                            dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).Style.BackColor = Color.Yellow
                            If InStr(BadTGPs, TESTID & ", ") = 0 Then BadTGPs += "'" & TESTID & "', "
                        End If
                        My.Application.DoEvents()
                        If InStr(BadAccs, Trim(selDR("Accession_ID")) & ", ") = 0 Then BadAccs += "'" & Trim(selDR("Accession_ID")) & "', "
                    End If
                Else
                    Exit While
                End If
                lblStatus.Text = CStr(CInt(Val(lblStatus.Text) + 1))
                My.Application.DoEvents()
            End While
        End If
        cnsql.Close()
        cnsql = Nothing
        '
        'Accs.Length
        'If BadTGPs.Length > 2 AndAlso Microsoft.VisualBasic.Right(BadTGPs, 2) = _
        '", " Then BadTGPs = Microsoft.VisualBasic.Mid(BadTGPs, 1, Len(BadTGPs) - 2)
        'If BadAccs.Length > 2 AndAlso Microsoft.VisualBasic.Right(BadAccs, 2) = _
        '", " Then BadAccs = Microsoft.VisualBasic.Mid(BadAccs, 1, Len(BadAccs) - 2)
        'For i = 0 To Accs.Length - 1
        '    If Accs(i) <> "" AndAlso BadTGPs <> "" Then
        '        ExecuteSqlProcedure("Delete from Lab_Results where Accession_ID = '" & _
        '        Accs(i) & "' and Test_ID in (" & BadTGPs & ")")
        '    End If
        'Next
        'If BadAccs <> "" Then
        '    ExecuteSqlProcedure("Delete from Lab_Results where Accession_ID in (" _
        '    & BadAccs & ")")
        'End If
        '
        If dgvResults.RowCount > 0 Then btnProcess.Enabled = True
    End Sub

    Private Function GetProlisTestID(ByVal LabID As Integer, ByVal RLTestID As String) As String()
        Dim TGPINFO() As String = {"", "", ""}
        Dim sSQL As String = "Select a.ID as TGPID, a.Name as TGPName, a.ComponentType as TGPType from Tests a inner join Lab_TGP b " &
        "on a.ID = b.TGP_ID where b.Lab_ID = " & LabID & " and b.LabResultID = '" & RLTestID & "' Union Select c.ID as TGPID, c.Name " &
        "as TGPName, c.ComponentType as TGPType from Groups c inner join Lab_TGP d on c.ID = d.TGP_ID where d.Lab_ID = " & LabID &
        " and d.LabResultID = '" & RLTestID & "' Union Select e.ID as TGPID, e.Name as TGPName, e.ComponentType as TGPType from Tests " &
        "e inner join Lab_TGP f on e.ID = f.TGP_ID where f.Lab_ID = " & LabID & " and f.LabResultID = '" & RLTestID & "'"
        Dim cntid As New SqlClient.SqlConnection(connString)
        cntid.Open()
        Dim cmdtid As New SqlClient.SqlCommand(sSQL, cntid)
        cmdtid.CommandType = CommandType.Text
        Dim drtid As SqlClient.SqlDataReader = cmdtid.ExecuteReader
        If drtid.HasRows Then
            While drtid.Read
                TGPINFO(0) = drtid("TGPID")
                TGPINFO(1) = drtid("TGPName")
                TGPINFO(2) = drtid("TGPType")
            End While
        End If
        cntid.Close()
        cntid = Nothing
        Return TGPINFO
    End Function

    Private Sub UpdateApplyProcess()
        Dim i As Integer
        Dim HasData As Boolean = False
        For i = 0 To dgvResults.RowCount - 1
            If Not dgvResults.Rows(i).Cells(0).Value = True Then
                HasData = True
                Exit For
            End If
        Next
        If HasData = True Or chkClear.Checked = True Then
            btnProcess.Enabled = True
        Else
            btnProcess.Enabled = False
        End If
    End Sub

    Private Sub dgvResults_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvResults.CellEndEdit
        If e.ColumnIndex = 0 Then
            UpdateApplyProcess()
        End If
    End Sub

    Private Sub UpdateReflexerIDs(ByRef ReflexerIDs() As String, ByVal AccID As Long)
        Dim Inserted As Boolean
        Dim cntid As New SqlClient.SqlConnection(connString)
        cntid.Open()
        Dim cmdtid As New SqlClient.SqlCommand("Select distinct Reflexer_ID from Ref_Results where Accession_ID = " & AccID, cntid)
        cmdtid.CommandType = CommandType.Text
        Dim drtid As SqlClient.SqlDataReader = cmdtid.ExecuteReader
        If drtid.HasRows Then
            While drtid.Read
                Inserted = False
                For i As Integer = 0 To ReflexerIDs.Length - 1
                    If ReflexerIDs(i) = "" Then
                        If Not ReflexerInReflexers(ReflexerIDs, drtid("Reflexer_ID").ToString) Then
                            ReflexerIDs(i) = drtid("Reflexer_ID").ToString
                            Inserted = True
                            Exit For
                        End If
                    End If
                Next
                If Inserted = False Then
                    ReDim Preserve ReflexerIDs(UBound(ReflexerIDs) + 1)
                    If Not ReflexerInReflexers(ReflexerIDs, drtid("Reflexer_ID").ToString) Then _
                    ReflexerIDs(UBound(ReflexerIDs)) = drtid("Reflexer_ID").ToString
                End If
            End While
        End If
        cntid.Close()
        cntid = Nothing
    End Sub

    Private Function IsValidAccID(ByVal AccID As String) As Boolean
        Dim ValidAcc As Boolean = False
        Dim cnacc As New SqlClient.SqlConnection(connString)
        cnacc.Open()
        Dim cmdacc As New SqlClient.SqlCommand("Select * from Requisitions where ID = " & Val(AccID), cnacc)
        cmdacc.CommandType = CommandType.Text
        Dim dracc As SqlClient.SqlDataReader = cmdacc.ExecuteReader
        If dracc.HasRows Then ValidAcc = True
        cnacc.Close()
        cnacc = Nothing
        Return ValidAcc
    End Function

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        'On Error Resume Next
        If cmbLabs.SelectedIndex <> -1 Then
            Dim IsDirty As Boolean = False
            Dim i As Integer
            Dim n As Integer
            Dim ItemX As MyList = cmbLabs.SelectedItem
            lblStatus.Text = ""
            Dim ValidAccs() As String = {""}
            Dim InvalidAccs() As String = {""}
            Dim GoodAccs As String = ""
            Dim BadAccs As String = ""
            Dim BadTGPs As String = ""
            Dim Conds() As String = {"", ""}
            Dim StatusFrom As String = ""
            Dim StatusTo As String = ""
            'pbProcess.Minimum = 0
            'pbProcess.Maximum = 100
            For i = 0 To dgvResults.RowCount - 1
                If InStr(GoodAccs, dgvResults.Rows(i).Cells(1).Value & "|") = 0 _
                AndAlso IsValidAccID(dgvResults.Rows(i).Cells(1).Value) Then
                    GoodAccs += dgvResults.Rows(i).Cells(1).Value & "|"
                    If ValidAccs(UBound(ValidAccs)) <> "" Then _
                    ReDim Preserve ValidAccs(UBound(ValidAccs) + 1)
                    ValidAccs(UBound(ValidAccs)) = dgvResults.Rows(i).Cells(1).Value
                Else
                    If InStr(BadAccs, dgvResults.Rows(i).Cells(1).Value & "|") = 0 _
                    AndAlso InStr(GoodAccs, dgvResults.Rows(i).Cells(1).Value & "|") = 0 _
                    Then BadAccs += dgvResults.Rows(i).Cells(1).Value & "|"
                End If
                If dgvResults.Rows(i).Cells(3).Value = "" AndAlso
                dgvResults.Rows(i).Cells(4).Value = "" Then     'bad TGP
                    If InStr(BadTGPs, dgvResults.Rows(i).Cells(2).Value & "|") = 0 _
                    Then BadTGPs += dgvResults.Rows(i).Cells(2).Value & "|"
                End If
                pbProcess.Value = (i + 1) * 100 / dgvResults.RowCount
                lblStatus.Text = (i + 1).ToString & " of " & dgvResults.RowCount.ToString
            Next
            If BadAccs.EndsWith("|") Then BadAccs = BadAccs.Substring(0, Len(BadAccs) - 1)
            '
            If BadTGPs.EndsWith("|") Then BadTGPs = BadTGPs.Substring(0, Len(BadTGPs) - 1)
            '
            Dim ResultID As String = ""
            Dim ResHistory As Boolean
            Dim ProviderID As Long
            Dim ReflexerIDs() As String = {""}
            Dim DirID As String = GetDefaultDirectorID()
            pbProcess.Minimum = 0
            pbProcess.Maximum = 100
            '
            If chkApply.Checked = True Then
                For i = 0 To ValidAccs.Length - 1
                    For n = i To dgvResults.RowCount - 1
                        If dgvResults.Rows(n).Cells(0).Value = True AndAlso
                            ValidAccs(i) = dgvResults.Rows(n).Cells(1).Value Then
                            ResultID = GetRefLabResultID(ItemX.ItemData,
                                dgvResults.Rows(n).Cells(2).Value.ToString)
                            If Not ReportFullResulted(ValidAccs(i)) AndAlso
                                (ResultID <> "" And IsNumeric(dgvResults.Rows(n).Cells(2).Value) _
                                And dgvResults.Rows(n).Cells(3).Value <> "") Then  'ApplyThen
                                ResHistory = GetResultHistory(Val(ValidAccs(i)))
                                ProviderID = GetOrdProvIDFromAccID(Val(ValidAccs(i)))
                                '
                                Conds = UpdateAccResults(ValidAccs(i), dgvResults.Rows(n).Cells(2).Value,
                                    dgvResults.Rows(n).Cells(4).Value, dgvResults.Rows(n).Cells(5).Value,
                                    dgvResults.Rows(n).Cells(6).Value, dgvResults.Rows(n).Cells(8).Value,
                                    dgvResults.Rows(n).Cells(10).Value, dgvResults.Rows(n).Cells(11).Value,
                                    dgvResults.Rows(n).Cells(7).Value, Not chkHoldRes.Checked)
                                '
                                UpdateReflexerIDs(ReflexerIDs, Val(ValidAccs(i)))
                                If ReflexerIDs(0) <> "" Then
                                    If UpdateRefResults(ValidAccs(i),
                                        ReflexerIDs, dgvResults.Rows(n).Cells(2).Value,
                                        dgvResults.Rows(n).Cells(4).Value,
                                        dgvResults.Rows(n).Cells(5).Value,
                                        dgvResults.Rows(n).Cells(6).Value,
                                        dgvResults.Rows(n).Cells(8).Value,
                                        dgvResults.Rows(n).Cells(9).Value,
                                        dgvResults.Rows(n).Cells(7).Value,
                                        dgvResults.Rows(n).Cells(10).Value,
                                        dgvResults.Rows(n).Cells(11).Value,
                                        Not chkHoldRes.Checked) Then
                                        LogEvent(ValidAccs(i), 22,
                                            ProviderID, "Autoapply", True, ThisUser.Name,
                                            "Result autoapplied")
                                        dgvResults.Rows(i).Cells(0).Value = False
                                        ProcessReflex(ValidAccs(i),
                                            dgvResults.Rows(n).Cells(2).Value,
                                            dgvResults.Rows(n).Cells(4).Value)
                                    Else    'Note attached to orphan test
                                        If dgvResults.Rows(n).Cells(8).Value IsNot DBNull.Value _
                                            AndAlso Trim(dgvResults.Rows(n).Cells(8).Value) <> "" Then
                                            ExecuteSqlProcedure("Update Sendouts Set RefLabNote = '" &
                                                Trim(dgvResults.Rows(n).Cells(8).Value) & "' where " &
                                                "Lab_ID = " & ItemX.ItemData & " and Accession_ID = " &
                                                ValidAccs(i))
                                        End If
                                    End If
                                    My.Application.DoEvents()
                                End If      'of apply
                                My.Application.DoEvents()
                                IsDirty = True
                                'of non finalized
                                '
                                If Conds(0) <> "" Then StatusFrom += Conds(0)
                                If Conds(1) <> "" Then
                                    StatusTo += Conds(1)
                                    IsDirty = True
                                    dgvResults.Rows(n).Cells(0).Value = False
                                    If ProcessReflex(ValidAccs(i), dgvResults.Rows(n).Cells(2).Value,
                                        dgvResults.Rows(n).Cells(4).Value) Then
                                        If ReflexerIDs(UBound(ReflexerIDs)) <> "" Then _
                                            ReDim Preserve ReflexerIDs(UBound(ReflexerIDs) + 1)
                                        If Not ReflexerInReflexers(ReflexerIDs,
                                            dgvResults.Rows(n).Cells(2).Value) Then _
                                            ReflexerIDs(UBound(ReflexerIDs)) =
                                            dgvResults.Rows(n).Cells(2).Value
                                    End If
                                    '
                                End If
                                '
                                Conds = UpdateAccInfoResults(ValidAccs(i), dgvResults.Rows(n).Cells(2).Value,
                                    dgvResults.Rows(n).Cells(4).Value, dgvResults.Rows(n).Cells(5).Value,
                                    dgvResults.Rows(n).Cells(6).Value, dgvResults.Rows(n).Cells(8).Value,
                                    dgvResults.Rows(n).Cells(10).Value, dgvResults.Rows(n).Cells(11).Value,
                                    dgvResults.Rows(n).Cells(7).Value, Not chkHoldRes.Checked)
                                If Conds(0) <> "" Then StatusFrom += Conds(0)
                                If Conds(1) <> "" Then
                                    StatusTo += Conds(1)
                                    IsDirty = True
                                    dgvResults.Rows(n).Cells(0).Value = False
                                    If ProcessReflex(ValidAccs(i), dgvResults.Rows(n).Cells(2).Value,
                                        dgvResults.Rows(n).Cells(4).Value) Then
                                        If ReflexerIDs(UBound(ReflexerIDs)) <> "" Then _
                                            ReDim Preserve ReflexerIDs(UBound(ReflexerIDs) + 1)
                                        If Not ReflexerInReflexers(ReflexerIDs,
                                            dgvResults.Rows(n).Cells(2).Value) Then _
                                            ReflexerIDs(UBound(ReflexerIDs)) =
                                            dgvResults.Rows(n).Cells(2).Value
                                    End If
                                End If
                                '
                                If ReflexerIDs(0) <> "" Then
                                    If UpdateRefResults(ValidAccs(i), ReflexerIDs, dgvResults.Rows(n).Cells(2).Value,
                                        dgvResults.Rows(n).Cells(4).Value, dgvResults.Rows(n).Cells(5).Value, dgvResults.Rows(n).Cells(6).Value,
                                        dgvResults.Rows(n).Cells(8).Value, dgvResults.Rows(n).Cells(9).Value, dgvResults.Rows(n).Cells(7).Value,
                                        dgvResults.Rows(n).Cells(10).Value, dgvResults.Rows(n).Cells(11).Value, Not chkHoldRes.Checked) Then
                                        ProcessReflex(ValidAccs(i), dgvResults.Rows(n).Cells(2).Value, dgvResults.Rows(n).Cells(4).Value)
                                    Else    'Note attached to orphan test
                                        If dgvResults.Rows(n).Cells(8).Value IsNot DBNull.Value _
                                            AndAlso Trim(dgvResults.Rows(n).Cells(8).Value) <> "" Then
                                            ExecuteSqlProcedure("Update Sendouts Set RefLabNote = '" &
                                                Trim(dgvResults.Rows(n).Cells(8).Value) & "' where " &
                                                "Lab_ID = " & ItemX.ItemData & " and Accession_ID = " &
                                                ValidAccs(i))
                                        End If
                                    End If
                                    IsDirty = True
                                End If
                                '
                            End If
                        End If
                    Next    'n
                    '
                    For n = i To dgvResults.RowCount - 1
                        If dgvResults.Rows(n).Cells(0).Value = True AndAlso
                        ValidAccs(i) = dgvResults.Rows(n).Cells(1).Value Then
                            ResultID = GetRefLabResultID(ItemX.ItemData,
                            dgvResults.Rows(n).Cells(2).Value.ToString)
                            If Not ReportFullResulted(ValidAccs(i)) Then
                                If (ResultID <> "" And IsNumeric(dgvResults.Rows(n).Cells(2).Value) _
                                AndAlso dgvResults.Rows(i).Cells(3).Value <> "") Then  'Apply
                                    UpdateReflexerIDs(ReflexerIDs, Val(ValidAccs(i)))
                                    If ReflexerIDs(0) <> "" Then
                                        If UpdateRefResults(ValidAccs(i),
                                        ReflexerIDs, dgvResults.Rows(n).Cells(2).Value,
                                        dgvResults.Rows(n).Cells(4).Value,
                                        dgvResults.Rows(n).Cells(5).Value,
                                        dgvResults.Rows(n).Cells(6).Value,
                                        dgvResults.Rows(n).Cells(8).Value,
                                        dgvResults.Rows(n).Cells(9).Value,
                                        dgvResults.Rows(n).Cells(7).Value,
                                        dgvResults.Rows(n).Cells(10).Value,
                                        dgvResults.Rows(n).Cells(11).Value,
                                        Not chkHoldRes.Checked) Then
                                            LogEvent(ValidAccs(i), 22,
                                            ProviderID, "Autoapply", True, ThisUser.Name,
                                            "Result autoapplied")
                                            dgvResults.Rows(i).Cells(0).Value = False
                                            ProcessReflex(ValidAccs(i),
                                            dgvResults.Rows(n).Cells(2).Value,
                                            dgvResults.Rows(n).Cells(4).Value)
                                        Else    'Note attached to orphan test
                                            If dgvResults.Rows(n).Cells(8).Value IsNot DBNull.Value _
                                            AndAlso Trim(dgvResults.Rows(n).Cells(8).Value) <> "" Then
                                                ExecuteSqlProcedure("Update Sendouts Set RefLabNote = '" &
                                                Trim(dgvResults.Rows(n).Cells(8).Value) & "' where " &
                                                "Lab_ID = " & ItemX.ItemData & " and Accession_ID = " &
                                                ValidAccs(i))
                                            End If
                                        End If
                                        My.Application.DoEvents()
                                    End If      'of apply
                                    My.Application.DoEvents()
                                    IsDirty = True
                                End If          'of non finalized
                            End If
                        Else
                            Exit For 'n
                        End If
                    Next    'n
                    '
                    'Following code to apply reflex result 2nd phase
                    For n = i To dgvResults.RowCount - 1
                        If dgvResults.Rows(n).Cells(0).Value = True AndAlso
                        ValidAccs(i) = dgvResults.Rows(n).Cells(1).Value Then
                            ResultID = GetRefLabResultID(ItemX.ItemData,
                            dgvResults.Rows(n).Cells(2).Value.ToString)
                            If Not ReportFullResulted(ValidAccs(i)) Then
                                If (ResultID <> "" And IsNumeric(dgvResults.Rows(i).Cells(2).Value) _
                                And dgvResults.Rows(i).Cells(3).Value <> "") Then  'Apply
                                    If ReflexerIDs(0) <> "" Then
                                        If UpdateRefResults(ValidAccs(i),
                                        ReflexerIDs, dgvResults.Rows(n).Cells(2).Value,
                                        dgvResults.Rows(n).Cells(4).Value,
                                        dgvResults.Rows(n).Cells(5).Value,
                                        dgvResults.Rows(n).Cells(6).Value,
                                        dgvResults.Rows(n).Cells(8).Value,
                                        dgvResults.Rows(n).Cells(9).Value,
                                        dgvResults.Rows(n).Cells(7).Value,
                                        dgvResults.Rows(n).Cells(10).Value,
                                        dgvResults.Rows(n).Cells(11).Value,
                                        Not chkHoldRes.Checked) Then
                                            LogEvent(ValidAccs(i), 22,
                                            ProviderID, "Autoapply", True, ThisUser.Name,
                                            "Result autoapplied")
                                            dgvResults.Rows(i).Cells(0).Value = False
                                            ProcessReflex(ValidAccs(i),
                                            dgvResults.Rows(n).Cells(2).Value,
                                            dgvResults.Rows(n).Cells(4).Value)
                                        Else    'Note attached to orphan test
                                            If dgvResults.Rows(n).Cells(8).Value IsNot DBNull.Value _
                                            AndAlso Trim(dgvResults.Rows(n).Cells(8).Value) <> "" Then
                                                ExecuteSqlProcedure("Update Sendouts Set RefLabNote = '" &
                                                Trim(dgvResults.Rows(n).Cells(8).Value) & "' where " &
                                                "Lab_ID = " & ItemX.ItemData & " and Accession_ID = " &
                                                ValidAccs(i))
                                            End If
                                        End If
                                        My.Application.DoEvents()
                                    End If          'of non finalized
                                    IsDirty = True
                                End If
                            End If
                        End If
                        'pbProcess.Value = (n + 1) * 100 / ValidAccs.Length
                        'lblStatus.Text = (n + 1).ToString & " of " & ValidAccs.Length.ToString
                    Next    'n
                    '
                    Dim sSQL As String = ""
                    If ExtendedResultExists(ItemX.ItemData, ValidAccs(i)) Then  'has extended result
                        sSQL = "Update t set t.Result = s.Result, t.Released = 1, t.Release_Time = IsNull(t.Release_Time, " &
                        "GetDate()), t.Released_By = IsNull(t.Released_By, " & ThisUser.ID & ") from Extend_Results t " &
                        "inner join Lab_Results_Extended s on t.Accession_ID = s.Accession_ID where s.Lab_ID = " &
                        ItemX.ItemData & " and s.Accession_ID = '" & ValidAccs(i) & "'"
                        ExecuteSqlProcedure(sSQL)
                    End If

                    If IsDirty = True Then
                        LogEvent(ValidAccs(i), 22, ProviderID, "Autoapply", True, ThisUser.Name, "Result autoapplied")
                        If StatusTo <> "" Then
                            LogUserEvent(ThisUser.ID, 22, Date.Now, "Accession", ValidAccs(i), StatusFrom, StatusTo)
                            StatusFrom = "" : StatusTo = ""
                        End If
                        '
                        UpdateReportTime(ValidAccs(i), Date.Now)
                        My.Application.DoEvents()
                        If DirID <> "" Then ExecuteSqlProcedure("Update Requisitions set " &
                        "Director_ID = " & Val(DirID) & " where ID = " &
                        ValidAccs(i) & " and Director_ID is NULL")
                        '
                        If SystemConfig.HL7AutoPub = True Then _
                        UpdateAccDisbursement(Val(ValidAccs(i)))
                        '
                        IsDirty = False
                    End If
                    '
                    If chkClear.Checked Then _
                    ExecuteSqlProcedure("Delete from Lab_Results where Lab_ID = " &
                    ItemX.ItemData & " and Accession_ID = '" & ValidAccs(i) & "'")
                    '
                    pbProcess.Value = (i + 1) * 100 / ValidAccs.Length
                    lblStatus.Text = (i + 1).ToString & " of " & ValidAccs.Length.ToString
                    My.Application.DoEvents()
                Next 'i
            Else    'do not apply results
                If chkClear.Checked Then
                    For i = 0 To ValidAccs.Length - 1
                        If ValidAccs(i) <> "" Then
                            ExecuteSqlProcedure("Delete from Lab_Results where Lab_ID = " &
                            ItemX.ItemData & " and Accession_ID = '" & ValidAccs(i) & "'")
                        End If
                    Next
                    '
                    For i = 0 To InvalidAccs.Length - 1
                        If InvalidAccs(i) <> "" Then
                            ExecuteSqlProcedure("Delete from Lab_Results where Lab_ID = " &
                            ItemX.ItemData & " and Accession_ID = '" & InvalidAccs(i) & "'")
                        End If
                    Next
                End If
            End If
            dgvResults.Rows.Clear()
        End If
        '
        PopulateLabs()
        'txtDateFrom.Text = Format(DateAdd(DateInterval.Day, -1, Date.Today), SystemConfig.DateFormat)
        'txtDateTo.Text = Format(DateAdd(DateInterval.Day, -1, Date.Today), SystemConfig.DateFormat)
        cmbStatus.SelectedIndex = 1
    End Sub

    Private Function ExtendedResultExists0(ByVal LabID As String, ByVal AccID As String) As Boolean
        'Dim Has As Boolean = False
        'Dim CNE As New ADODB.Connection
        'CNE.Open(connstring)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select Result from Lab_Results_Extended where Lab_ID = " & LabID & " and Accession_ID = " _
        '& AccID, CNE, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Rs.BOF Then
        '    Has = False
        'Else
        '    If Rs.Fields("Result").Value Is DBNull.Value Then
        '        Has = False
        '    Else
        '        Has = True
        '    End If
        'End If
        'Rs.Close()
        'Rs = Nothing
        'CNE.Close()
        'CNE = Nothing
        'Return Has
    End Function

    Private Function ExtendedResultExists(ByVal LabID As String, ByVal AccID As String) As Boolean
        Dim HasResult As Boolean = False

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT Result FROM Lab_Results_Extended WHERE Lab_ID = @LabID AND Accession_ID = @AccID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@LabID", LabID)
                command.Parameters.AddWithValue("@AccID", AccID)

                Dim result As Object = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    HasResult = True
                End If
            End Using
        End Using

        Return HasResult
    End Function

    Private Function UpdateAccInfoResults(ByVal AccID As Long, ByVal InfoID As Integer, ByVal Result As String,
    ByVal Flag As String, ByVal Range As String, ByVal Cmnt As String, ByVal UOM As String, ByVal LabID As _
    String, ByVal Status As String, ByVal Rel As Boolean) As String()
        Dim Conds() As String = {"", ""}
        Dim sSQL As String = ""
        If AccID > 0 And InfoID > 0 And Result <> "" Then
            Conds(0) = GetExistingAccInfoResult(AccID, InfoID)
            If Status = "F" Then
                If Rel = False Then
                    sSQL = "Update Acc_Info_Results Set Result = '" & Replace(Result, "'", "''") & "', Flag = '" & Trim(Replace(Flag, "'", "''")) &
                    "', NormalRange = '" & Replace(Range, "'", "''") & "', Comment = '" & Replace(Cmnt, "'", "''") & "', UOM = '" & Replace(UOM,
                    "'", "''") & "', LabID = '" & LabID & "', Released = 0, Released_By = NULL, Release_Time = NULL where Accession_ID = " &
                    AccID & " and Info_ID = " & InfoID & " and (Result is Null or Result = '')"
                Else
                    sSQL = "Update Acc_Info_Results Set Result = '" & Replace(Result, "'", "''") & "', Flag = '" & Trim(Replace(Flag, "'", "''")) &
                    "', NormalRange = '" & Replace(Range, "'", "''") & "', Comment = '" & Replace(Cmnt, "'", "''") & "', UOM = '" & Replace(UOM,
                    "'", "''") & "', LabID = '" & LabID & "', Released = " & Convert.ToInt16(Rel) & ", Released_By = " & ThisUser.ID &
                    ", Release_Time = '" & Date.Now & "' where Accession_ID = " & AccID & " and Info_ID = " & InfoID & " and (Result is Null or Result = '')"
                End If
            ElseIf Status = "C" Then
                If Rel = False Then
                    sSQL = "Update Acc_Info_Results Set Result = '" & Replace(Result, "'", "''") & "', Flag = '" & Trim(Replace(Flag, "'", "''")) &
                    "', NormalRange = '" & Replace(Range, "'", "''") & "', Comment = '" & Replace(Cmnt, "'", "''") & "', UOM = '" & Replace(UOM,
                    "'", "''") & "', LabID = '" & LabID & "', Released = 0, Released_By = NULL, Release_Time = NULL where Accession_ID = " &
                    AccID & " and Info_ID = " & InfoID
                Else
                    sSQL = "Update Acc_Info_Results Set Result = '" & Replace(Result, "'", "''") & "', Flag = '" & Trim(Replace(Flag, "'", "''")) &
                    "', NormalRange = '" & Replace(Range, "'", "''") & "', Comment = '" & Replace(Cmnt, "'", "''") & "', UOM = '" & Replace(UOM,
                    "'", "''") & "', LabID = '" & LabID & "', Released = " & Convert.ToInt16(Rel) & ", Released_By = " & ThisUser.ID &
                    ", Release_Time = '" & Date.Now & "' where Accession_ID = " & AccID & " and Info_ID = " & InfoID
                End If
            ElseIf Status = "X" Then
                sSQL = "Update Acc_Info_Results Set Result = '.', Flag = '', NormalRange = '" & Replace(Range, "'", "''") & "', Comment = '', UOM = '" &
                Replace(UOM, "'", "''") & "', LabID = '" & LabID & "', Released = " & Convert.ToInt16(Rel) & ", Released_By = " & ThisUser.ID &
                ", Release_Time = '" & Date.Now & "' where Accession_ID = " & AccID & " and Info_ID = " & InfoID
            End If
            '
            Try
                If sSQL <> "" Then
                    ExecuteSqlProcedure(sSQL)
                    Conds(1) = Result
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
                Conds(1) = ""
            End Try
            '
            '*** Audit Trail ***
            If SystemConfig.AuditTrail = True Then
                Conds(1) = TestID.ToString & "=" & Result &
                "," & Convert.ToInt16(Rel).ToString & "|"
            End If
        End If
        '
        Return Conds
    End Function

    Private Function UpdateAccResults(ByVal AccID As Long, ByVal TestID As Integer, ByVal Result As String,
    ByVal Flag As String, ByVal Range As String, ByVal Cmnt As String, ByVal UOM As String, ByVal LabID As _
    String, ByVal Status As String, ByVal Rel As Boolean) As String()
        Dim Conds() As String = {"", ""}
        Dim sSQL As String = ""
        If AccID > 0 And TestID > 0 And Result <> "" Then
            Conds(0) = GetExistingAccResult(AccID, TestID)
            If Status = "F" Then
                If Rel = False Then
                    sSQL = "Update Acc_Results Set Result = '" & Replace(Result, "'", "''") & "', Flag = '" & Trim(Replace(Flag, "'", "''")) &
                    "', NormalRange = '" & Replace(Range, "'", "''") & "', Comment = '" & Replace(Cmnt, "'", "''") & "', UOM = '" & Replace(UOM,
                    "'", "''") & "', LabID = '" & LabID & "', Released = 0, Released_By = NULL, Release_Time = NULL where Accession_ID = " &
                    AccID & " and Test_ID = " & TestID & " " 'and (Result is Null or Result = '')
                Else
                    sSQL = "Update Acc_Results Set Result = '" & Replace(Result, "'", "''") & "', Flag = '" & Trim(Replace(Flag, "'", "''")) &
                    "', NormalRange = '" & Replace(Range, "'", "''") & "', Comment = '" & Replace(Cmnt, "'", "''") & "', UOM = '" & Replace(UOM,
                    "'", "''") & "', LabID = '" & LabID & "', Released = " & Convert.ToInt16(Rel) & ", Released_By = " & ThisUser.ID &
                    ", Release_Time = '" & Date.Now & "' where Accession_ID = " & AccID & " and Test_ID = " & TestID & " " 'and (Result is Null or Result = '')
                End If
            ElseIf Status = "C" Then
                If Rel = False Then
                    sSQL = "Update Acc_Results Set Result = '" & Replace(Result, "'", "''") & "', Flag = '" & Trim(Replace(Flag, "'", "''")) &
                    "', NormalRange = '" & Replace(Range, "'", "''") & "', Comment = '" & Replace(Cmnt, "'", "''") & "', UOM = '" & Replace(UOM,
                    "'", "''") & "', LabID = '" & LabID & "', Released = 0, Released_By = NULL, Release_Time = NULL where Accession_ID = " &
                    AccID & " and Test_ID = " & TestID
                Else
                    sSQL = "Update Acc_Results Set Result = '" & Replace(Result, "'", "''") & "', Flag = '" & Trim(Replace(Flag, "'", "''")) &
                    "', NormalRange = '" & Replace(Range, "'", "''") & "', Comment = '" & Replace(Cmnt, "'", "''") & "', UOM = '" & Replace(UOM,
                    "'", "''") & "', LabID = '" & LabID & "', Released = " & Convert.ToInt16(Rel) & ", Released_By = " & ThisUser.ID &
                    ", Release_Time = '" & Date.Now & "' where Accession_ID = " & AccID & " and Test_ID = " & TestID
                End If
            ElseIf Status = "X" Then
                sSQL = "Update Acc_Results Set Result = '.', Flag = '', NormalRange = '" & Replace(Range, "'", "''") & "', Comment = '', UOM = '" &
                Replace(UOM, "'", "''") & "', LabID = '" & LabID & "', Released = " & Convert.ToInt16(Rel) & ", Released_By = " & ThisUser.ID &
                ", Release_Time = '" & Date.Now & "' where Accession_ID = " & AccID & " and Test_ID = " & TestID
            End If
            '
            Try
                If sSQL <> "" Then
                    ExecuteSqlProcedure(sSQL)
                    Conds(1) = Result
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
                Conds(1) = ""
            End Try
            '
            '*** Audit Trail ***
            If SystemConfig.AuditTrail = True Then
                Conds(1) = TestID.ToString & "=" & Result &
                "," & Convert.ToInt16(Rel).ToString & "|"
            End If
        End If
        '
        Return Conds
    End Function

    Private Function GetExistingAccInfoResult(ByVal AccID As Long, ByVal InfoID As Integer) As String
        Dim Res As String = ""
        Dim sSQL As String = "Select Result from Acc_Info_Results where Accession_ID = " & AccID & " and Info_ID = " & InfoID
        '
        Dim cnn As New SqlClient.SqlConnection(connString)
        cnn.Open()
        Dim cmdsel As New SqlClient.SqlCommand(sSQL, cnn)
        Dim DRsel As SqlClient.SqlDataReader = cmdsel.ExecuteReader
        If DRsel.HasRows Then
            While DRsel.Read
                If DRsel("Result") IsNot DBNull.Value Then Res = DRsel("Result")
            End While
        End If
        DRsel.Close()
        cmdsel.Dispose()
        cnn.Close()
        cnn = Nothing
        Return Res
    End Function

    Private Function GetExistingAccResult(ByVal AccID As Long, ByVal TestID As Integer) As String
        Dim Res As String = ""
        Dim sSQL As String = "Select Result from Acc_Results where Accession_ID = " & AccID & " and Test_ID = " & TestID
        '
        Dim cnn As New SqlClient.SqlConnection(connString)
        cnn.Open()
        Dim cmdsel As New SqlClient.SqlCommand(sSQL, cnn)
        Dim DRsel As SqlClient.SqlDataReader = cmdsel.ExecuteReader
        If DRsel.HasRows Then
            While DRsel.Read
                If DRsel("Result") IsNot DBNull.Value Then Res = DRsel("Result")
            End While
        End If
        DRsel.Close()
        cmdsel.Dispose()
        cnn.Close()
        cnn = Nothing
        Return Res
    End Function

    Private Function ReflexerInReflexers(ByVal Reflexers() As String, ByVal Reflexer As String) As Boolean
        Dim RInRs As Boolean = False
        Dim i As Integer
        For i = 0 To Reflexers.Length - 1
            If Reflexers(i) = Reflexer Then
                RInRs = True
                Exit For
            End If
        Next
        Return RInRs
    End Function


    Private Function GetRefLabResultID(ByVal LabID As Integer, ByVal ProlisTestID As String) As String
        Dim ResID As String = ""
        If IsNumeric(ProlisTestID) Then
            Dim cnrt As New SqlClient.SqlConnection(connString)
            cnrt.Open()
            Dim cmdrt As New SqlClient.SqlCommand("Select LabResultID from " &
            "Lab_TGP where Lab_ID = " & LabID & " and TGP_ID = " & ProlisTestID, cnrt)
            cmdrt.CommandType = CommandType.Text
            Dim drrt As SqlClient.SqlDataReader = cmdrt.ExecuteReader
            If drrt.HasRows Then
                While drrt.Read
                    ResID = drrt("LabResultID")
                End While
            End If
            cnrt.Close()
            cnrt = Nothing
        End If
        Return ResID
    End Function

    Private Function ProcessReflex(ByVal AccID As Long, ByVal TestID As Integer, ByVal Result As String) As Boolean
        Dim Processed As Boolean = False
        Dim ReflexedIDs() As String = GetConfigReflexedIDs(TestID, Trim(Result))
        Dim TGPType As String = ""
        For i As Integer = 0 To ReflexedIDs.Length - 1
            If ReflexedIDs(i) <> "" Then
                TGPType = GetTGPType(ReflexedIDs(i))
                If TGPType = "T" Then
                    ExecuteSqlProcedure("If Not Exists (Select * from Ref_Results where " &
                    "Accession_ID = " & AccID & " and Reflexer_ID = " & TestID & " and " &
                    "Reflexed_ID = " & ReflexedIDs(i) & " and Test_ID = " & ReflexedIDs(i) &
                    ") Insert into Ref_Results (Accession_ID, Reflexer_ID, Reflexed_ID, " &
                    "Test_ID) values (" & AccID & ", " & TestID & ", " & ReflexedIDs(i) &
                    ", " & ReflexedIDs(i) & ")")
                ElseIf TGPType = "G" Then
                    Dim cnrf As New SqlClient.SqlConnection(connString)
                    cnrf.Open()
                    Dim cmdrf As New SqlClient.SqlCommand("Select Test_ID from " & _
                    "Group_Test where Group_ID = " & ReflexedIDs(i), cnrf)
                    cmdrf.CommandType = CommandType.Text
                    Dim drrf As SqlClient.SqlDataReader = cmdrf.ExecuteReader
                    If drrf.HasRows Then
                        While drrf.Read
                            ExecuteSqlProcedure("If Not Exists (Select * from Ref_Results where " & _
                            "Accession_ID = " & AccID & " and Reflexer_ID = " & TestID & " and " & _
                            "Reflexed_ID = " & ReflexedIDs(i) & " and Test_ID = " & drrf("Test_ID") & _
                            ") Insert into Ref_Results (Accession_ID, Reflexer_ID, Reflexed_ID, " & _
                            "Test_ID) values (" & AccID & ", " & TestID & ", " & ReflexedIDs(i) & _
                            ", " & drrf("Test_ID") & ")")
                        End While
                    End If
                    cnrf.Close()
                    cnrf = Nothing
                End If
                Processed = True
            End If
        Next
        Return Processed
    End Function

    'Private Function ProcessReflexes(ByVal Accessions As String) As Boolean
    '    Dim IsDirty As Boolean = False
    '    Dim i As Integer
    '    'Dim n As Integer
    '    Dim AccID As Long
    '    Dim RefedIDs As String = ""
    '    Dim AccIDs(0) As String
    '    If InStr(Accessions, ",") > 0 Then
    '        AccIDs = Split(Accessions, ",")
    '    Else
    '        AccIDs(0) = Accessions
    '    End If
    '    For i = 0 To AccIDs.Length - 1
    '        AccIDs(i) = Replace(AccIDs(i), "'", "")
    '        If Trim(AccIDs(i)) <> "" Then
    '            AccID = Val(Trim(AccIDs(i)))
    '            Dim RsA As New ADODB.Recordset
    '            RsA.Open("Select * from Acc_Results where Accession_ID = " & _
    '            AccID & " and Test_ID in (Select ID from Tests where " & _
    '            "IsActive <> 0 and HasResult <> 0 and Automarker <> 0)", CN, _
    '            ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '            If Not RsA.BOF Then
    '                Dim ReflexedIDs() As String = {""}
    '                Do Until RsA.EOF
    '                    ReDim ReflexedIDs(0)
    '                    ReflexedIDs(0) = ""
    '                    If RsA.Fields("Result").Value IsNot System.DBNull.Value _
    '                    AndAlso Trim(RsA.Fields("Result").Value) <> "" Then
    '                        ReflexedIDs = GetConfigReflexedIDs(RsA.Fields("Test_ID").Value, _
    '                        Trim(RsA.Fields("Result").Value))
    '                    End If
    '                    If ReflexedIDs(0) <> "" Then
    '                        IsDirty = ProcessTriggers(AccID, RsA.Fields("Test_ID").Value, ReflexedIDs)
    '                        RefedIDs = Join(ReflexedIDs, ", ")
    '                        CN.Execute("Delete from Ref_Results where Accession_ID = " & _
    '                        AccID & " and Reflexer_ID = " & RsA.Fields("Test_ID").Value & _
    '                        " and Not Reflexed_ID in (" & RefedIDs & ")")
    '                        RefedIDs = ""
    '                    Else
    '                        CN.Execute("Delete from Ref_Results where Accession_ID = " & _
    '                        AccID & " and Reflexer_ID = " & RsA.Fields("Test_ID").Value)
    '                        'IsDirty = False
    '                    End If
    '                    'IsDirty = True
    '                    RsA.MoveNext()
    '                Loop
    '            End If
    '            RsA.Close()
    '            RsA = Nothing
    '            Dim RsR As New ADODB.Recordset
    '            RsR.Open("Select * from Ref_Results where Accession_ID = " & _
    '            AccID & " and Test_ID in (Select ID from Tests where " & _
    '            "IsActive <> 0 and HasResult <> 0 and Automarker <> 0)", CN, _
    '            ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '            If Not RsR.BOF Then
    '                Dim ReflexedIDs() As String = {""}
    '                Do Until RsR.EOF
    '                    ReDim ReflexedIDs(0)
    '                    ReflexedIDs(0) = ""
    '                    If RsR.Fields("Result").Value IsNot System.DBNull.Value _
    '                    AndAlso Trim(RsR.Fields("Result").Value) <> "" Then
    '                        ReflexedIDs = GetConfigReflexedIDs(RsR.Fields("Test_ID").Value, _
    '                        Trim(RsR.Fields("Result").Value))
    '                    End If
    '                    If ReflexedIDs(0) <> "" Then
    '                        IsDirty = ProcessTriggers(AccID, RsR.Fields("Test_ID").Value, ReflexedIDs)
    '                        RefedIDs = Join(ReflexedIDs, ", ")
    '                        CN.Execute("Delete from Ref_Results where Accession_ID = " & _
    '                        AccID & " and Reflexer_ID = " & RsR.Fields("Reflexer_ID").Value & _
    '                        " and Not Reflexed_ID in (" & RefedIDs & ")")
    '                        RefedIDs = ""
    '                    Else
    '                        CN.Execute("Delete from Ref_Results where Accession_ID = " & _
    '                        AccID & " and Reflexer_ID = " & RsR.Fields("Test_ID").Value)
    '                        'IsDirty = False
    '                    End If
    '                    'IsDirty = True
    '                    RsR.MoveNext()
    '                Loop
    '            End If
    '            RsR.Close()
    '            RsR = Nothing
    '        End If
    '        'If InStr(Accessions, AccID & ", ") > 0 Then _
    '        'Accessions = Replace(Accessions, AccID & ", ", "")
    '        'If InStr(Accessions, AccID) > 0 Then _
    '        'Accessions = Replace(Accessions, AccID, "")
    '    Next
    '    Return IsDirty
    'End Function

    Private Sub btnSelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelAll.Click
        Dim i As Integer
        For i = 0 To dgvResults.RowCount - 1
            dgvResults.Rows(i).Cells(0).Value = True
        Next
    End Sub

    Private Sub btnDeselAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeselAll.Click
        Dim i As Integer
        For i = 0 To dgvResults.RowCount - 1
            dgvResults.Rows(i).Cells(0).Value = False
        Next
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        If cmbLabs.SelectedIndex <> -1 Then     'Lab selected
            Dim ItemX As MyList = cmbLabs.SelectedItem
            LoadResults(ItemX.ItemData)
        End If
    End Sub

    Private Sub chkApply_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkApply.CheckedChanged
        If chkApply.Checked = True Then
            chkApply.Text = "Apply Results"
        Else
            chkApply.Text = "Do Not Result"
        End If
    End Sub

    Private Sub chkClear_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkClear.CheckedChanged
        If chkClear.Checked = True Then
            chkApply.Checked = True
            chkApply.Enabled = True
        Else
            chkApply.Checked = True
            chkApply.Enabled = False
        End If
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
                Else    'valid entry
                    'txtDateFrom.Text = ""
                    'txtDateTo.Text = ""
                    ClearDateTimePicker(dtpDateFrom)
                    ClearDateTimePicker(dtpDateTo)

                    txtAccFrom.Text = ""
                    txtAccTo.Text = ""
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

    Private Sub dgvDiscrete_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvDiscrete.CellMouseUp
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
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
                If HasDiscretes() Then
                    'txtDateFrom.Text = ""
                    'txtDateTo.Text = ""

                    ClearDateTimePicker(dtpDateFrom)
                    ClearDateTimePicker(dtpDateTo)
                    txtAccFrom.Text = ""
                    txtAccTo.Text = ""
                End If
            End If
        End If
    End Sub

    Private Function HasDiscretes() As Boolean
        Dim Hasit As Boolean = False
        For i As Integer = 0 To dgvDiscrete.RowCount - 1
            If dgvDiscrete.Rows(i).Cells(0).Value IsNot Nothing AndAlso _
            IsNumeric(Trim(dgvDiscrete.Rows(i).Cells(0).Value)) Then
                Hasit = True
                Exit For
            End If
        Next
        Return Hasit
    End Function

    Private Sub txtDateFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
        'If IsDate(txtDateFrom.Text) Then
        '    dgvDiscrete.Rows.Clear()
        '    dgvDiscrete.RowCount = 1
        '    txtAccFrom.Text = ""
        '    txtAccTo.Text = ""
        'End If
    End Sub

    Private Sub txtDateTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
        'If IsDate(txtDateTo.Text) Then
        '    dgvDiscrete.Rows.Clear()
        '    dgvDiscrete.RowCount = 1
        '    txtAccFrom.Text = ""
        '    txtAccTo.Text = ""
        'End If
    End Sub

    Private Sub txtAccFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccFrom.Validated
        If txtAccFrom.Text <> "" AndAlso _
        IsNumeric(txtAccFrom.Text) Then
            dgvDiscrete.Rows.Clear()
            dgvDiscrete.RowCount = 1
            'txtDateFrom.Text = ""
            'txtDateTo.Text = ""
            ClearDateTimePicker(dtpDateFrom)
            ClearDateTimePicker(dtpDateTo)
        End If
    End Sub

    Private Sub txtAccTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccTo.Validated
        If txtAccTo.Text <> "" AndAlso _
        IsNumeric(txtAccTo.Text) Then
            dgvDiscrete.Rows.Clear()
            dgvDiscrete.RowCount = 1
            'txtDateFrom.Text = ""
            'txtDateTo.Text = ""

            ClearDateTimePicker(dtpDateFrom)
            ClearDateTimePicker(dtpDateTo)
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If IsRunning Then
            IsRunning = False
        Else
            Me.Close()
        End If
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
End Class