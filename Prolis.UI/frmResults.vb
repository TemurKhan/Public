Option Compare Text
Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.AccessControl
Imports System.Security.Policy
Imports System.Threading
Imports MSScriptControl
Imports System.Management
Imports System.Diagnostics
Imports DataTable = System.Data.DataTable
Imports Microsoft.Data.Sql

Public Class frmResults
    Private origWidth As Integer
    Private origHeight As Integer
    Private ReleaserInfo(2, 0) As String
    Private Override As Integer
    Private Refluxes As String
    Public Comment As String
    Public TestID As Integer
    Private origRes As String
    Private ResDirty As Boolean = False
    Private CorrectedStyle As New DataGridViewCellStyle
    Private NormalStyle As New DataGridViewCellStyle
    Private AbnormalStyle As New DataGridViewCellStyle
    Private PanicStyle As New DataGridViewCellStyle
    'Private RelStatus As Boolean
    'Private ChangeDate As Boolean = False
    Private ExtPDF As Byte()
    Private Decs As String
    Public TriggerID As Integer
    Private ResultsAT(,) As String
    'Private OldResults(0, 8) '0=TestID, 1=Res, 2=Flag, 3=Note, 4=RTF, 5=Cause0, 6=Cause1, 7=Cause2, 8=Rel
    'Private NewResults(0, 8) '0=TestID, 1=Res, 2=Flag, 3=Note, 4=RTF, 5=Cause0, 6=Cause1, 7=Cause2, 8=Rel
    Private AccDirty As Boolean = False
    Private QCDirty As Boolean = False

    '========================================
    Private contextMenuStrip As New ContextMenuStrip()
    Private openTestsMenuItem As New ToolStripMenuItem("Open Test Window")
    Private selectedTestID As String ' Define a variable to hold the TestID
    '========================================


    Public Property ResultComment()
        Get
            Return Comment
        End Get
        Set(ByVal value)
            Comment = value
        End Set
    End Property

    Private Sub SaveQCChanges()
        SaveQCWithRelease()
        'IsDirty = False
    End Sub

    Private Sub SaveAccChanges(ByVal AccID As Long)
        'Report Status
        'If cmbDirector.SelectedIndex <> -1 Then
        '    Dim Itemx As MyList = cmbDirector.SelectedItem
        '    CN.Execute("Update Requisitions set Director_ID = " & Itemx.ItemData & _
        '    " where ID = " & AccID)
        'End If
        '
        Dim Itemx As MyList = Nothing
        If cmbDirector.SelectedIndex <> -1 _
        Then Itemx = cmbDirector.SelectedItem
        If Itemx IsNot Nothing Then
            ExecuteSqlProcedure("Update Requisitions set Director_ID = " & Itemx.ItemData &
            " where ID = " & AccID & " and Director_ID is NULL")
        Else
            Dim DirID As String = ""
            DirID = GetDefaultDirectorID()
            If DirID <> "" Then _
            ExecuteSqlProcedure("Update Requisitions set Director_ID = " & DirID &
            " where ID = " & AccID & " and Director_ID is NULL")
        End If
        '
        LogEvent(AccID, 22, GetOrdProvIDFromAccID(AccID),
        "Result Edit", False, ThisUser.Name, "Result Edit Manual")
        'Audit Trail
        If SystemConfig.AuditTrail = True Then
            Dim i As Integer
            For i = 0 To dgvResults.RowCount - 1
                ResultsAT(3, i) = Trim(dgvResults.Rows(i).Cells(2).Value)
                ResultsAT(4, i) = IIf(dgvResults.Rows(i).Cells(10).Value =
                True, "1", "0")
                ResultsAT(7, i) = Trim(dgvResults.Rows(i).Cells(16).Value)
                ResultsAT(8, i) = Trim(dgvResults.Rows(i).Cells(17).Value)
            Next
            Dim ResOld As String = ""
            Dim ResNew As String = ""
            Dim NoteOld As String = ""
            Dim RTFOld As String = ""
            Dim NoteNew As String = ""
            Dim RTFNew As String = ""
            For i = LBound(ResultsAT, 2) To UBound(ResultsAT, 2)
                If ResultsAT(1, i) <> ResultsAT(3, i) Or
                ResultsAT(2, i) <> ResultsAT(4, i) Then
                    If ResultsAT(1, i) = "" Then    'new
                        ResNew += ResultsAT(0, i) & "=" &
                        ResultsAT(3, i) & "^" & ResultsAT(4, i) & "|"
                    Else
                        ResOld += ResultsAT(0, i) & "=" &
                        ResultsAT(1, i) & "^" & ResultsAT(2, i) & "|"
                        ResNew += ResultsAT(0, i) & "=" &
                        ResultsAT(3, i) & "^" & ResultsAT(4, i) & "|"
                    End If
                End If
                If ResultsAT(5, i) <> ResultsAT(7, i) Then  'note change
                    If ResultsAT(5, i) <> "" Then _
                    NoteOld += ResultsAT(0, i) & "=" & ResultsAT(5, i) & "|"
                    NoteNew += ResultsAT(0, i) & "=" & ResultsAT(7, i) & "|"
                End If
                If ResultsAT(6, i) <> ResultsAT(8, i) Then  'RTF change
                    If ResultsAT(6, i) <> "" Then _
                    RTFOld += ResultsAT(0, i) & "=" & RTF_To_Text(ResultsAT(6, i)) & "|"
                    RTFNew += ResultsAT(0, i) & "=" & RTF_To_Text(ResultsAT(8, i)) & "|"
                End If
            Next
            If ResOld.Length > 1 Then ResOld.Remove(ResOld.Length - 1, 1)
            If ResNew.Length > 1 Then ResNew.Remove(ResNew.Length - 1, 1)
            If NoteNew.Length > 1 Then NoteNew.Remove(NoteNew.Length - 1, 1)
            If NoteOld.Length > 1 Then NoteOld.Remove(NoteOld.Length - 1, 1)
            If RTFNew.Length > 1 Then RTFNew.Remove(RTFNew.Length - 1, 1)
            If RTFOld.Length > 1 Then RTFOld.Remove(RTFOld.Length - 1, 1)
            '
            If ResOld <> "" Or ResNew <> "" Then LogUserEvent(ThisUser.ID,
            22, Date.Now, "Accession", AccID, Trim(Microsoft.VisualBasic.Mid(ResOld,
            1, 4000)), Trim(Microsoft.VisualBasic.Mid(ResNew, 1, 4000)))
            '
            If NoteOld <> "" Or NoteNew <> "" Then LogUserEvent(ThisUser.ID,
            24, Date.Now, "Accession", AccID, Trim(Microsoft.VisualBasic.Mid(NoteOld,
            1, 4000)), Trim(Microsoft.VisualBasic.Mid(NoteNew, 1, 4000)))
            '
            If RTFOld <> "" Or RTFNew <> "" Then LogUserEvent(ThisUser.ID,
            26, Date.Now, "Accession", AccID, Trim(Microsoft.VisualBasic.Mid(RTFOld,
            1, 4000)), Trim(Microsoft.VisualBasic.Mid(RTFNew, 1, 4000)))
        End If
        '********** End Audit Trail ****************
        If SystemConfig.HL7AutoPub = True Then
            ExecuteSqlProcedure("Update Req_RPT Set EntryDate = '" & Date.Now &
            "' where RPT_Interface <> 0 and Base_ID = " & AccID)
            UpdateAccDisbursement(AccID)
        End If
        '
        If IsDate(txtRptDate.Text & " " & txtRptTime.Text) Then _
        UpdateReportTime(AccID, CDate(txtRptDate.Text & " " & txtRptTime.Text))
        'ReDim UserInfo(2, 0)
    End Sub


    Private Sub SaveChanges()
        If btnAccQC.Checked = False Then 'Accession
            SaveAccWithRelease()
        Else
            If cmbRun.SelectedIndex <> -1 Then
                SaveQCWithRelease()
                If txtValidated.Text = "Yes" Then SaveAccWithRelease()
            End If
        End If
        If Refluxes <> "" And cmbAccCtl.Text <> "" Then _
        ProcessRefluxes(Refluxes)
        Refluxes = ""
        'dgvResults.Rows.Clear()
        txtRelStatus.Text = ""
        txtEditedOn.Text = ""
        'IsDirty = False
        'cmbAccCtl.Text = ""
        'cmbAccCtl.SelectedIndex = -1
    End Sub

    Private Sub ProcessRefluxes(ByVal Refluxes As String)
        Dim i As Integer
        Dim AccID As Long
        Dim ParentID As Integer
        Dim TGPID As Integer
        Dim CMD As String
        Dim cSQL As String = ""
        Dim Accs As String = ""
        Dim Parents As String = ""
        Dim Orphans As String = ""
        Dim TGPType As String = ""
        Dim Data() As String
        Dim Fields() As String
        Data = Split(Refluxes, "|")
        For i = 0 To Data.Length - 1
            Fields = Split(Data(i), "^")
            If Fields.Length >= 3 Then
                AccID = Val(Fields(0))
                ParentID = Val(Fields(1))
                TGPID = Val(Fields(2))
                CMD = Fields(3)
                If Fields(0) <> "" And Fields(1) <> "" And Fields(2) <> "" And Fields(3) <> "" Then
                    If CMD = "Add" Then     'Mark
                        'MarkComponent(AccID, ParentID, TGPID)
                    Else
                        Accs = Fields(0)
                        If InStr(Parents, Fields(1)) = 0 Then Parents += Fields(1) & ", "
                        If InStr(Orphans, Fields(2)) = 0 Then Orphans += Fields(2) & ", "
                    End If
                    Fields(0) = "" : Fields(1) = "" : Fields(2) = "" : Fields(3) = ""
                ElseIf Fields(0) <> "" And Fields(1) <> "" And Fields(2) = "" And Fields(3) = "Remove" Then
                    Accs = Fields(0)
                    If InStr(Parents, Fields(1)) = 0 Then Parents += Fields(1) & ", "
                    Orphans = "*, "
                End If
            End If
        Next
        If Accs <> "" And Parents.Length > 0 And Orphans.Length > 0 Then
            Orphans = Orphans.Substring(0, Len(Orphans) - 2)
            Parents = Parents.Substring(0, Len(Parents) - 2)
            If Orphans <> "*" Then
                ExecuteSqlProcedure("Delete from Acc_Results2 where Accession_ID = " &
                Accs & " and Parent_ID in (" & Parents & ") and Child_ID in (" &
                Orphans & ")")
                ExecuteSqlProcedure("Delete from Req_TGP2 where Accession_ID = " &
                Accs & " and Parent_ID in (" & Parents & ") and ChildTGP_ID in (" &
                Orphans & ")")
            Else
                ExecuteSqlProcedure("Delete from Acc_Results2 where Accession_ID = " &
                Accs & " and Parent_ID in (Select Child_ID from Acc_Results2 " &
                "where Accession_ID = " & Accs & " and Parent_ID in (" & Parents &
                "))")
                ExecuteSqlProcedure("Delete from Acc_Results2 where Accession_ID = " &
                Accs & " and Parent_ID in (" & Parents & ")")
                ExecuteSqlProcedure("Delete from Req_TGP2 where Accession_ID = " &
                Accs & " and Parent_ID in (Select ChildTGP_ID from Req_TGP2 " &
                "where Accession_ID = " & Accs & " and Parent_ID in (" & Parents &
                "))")
                ExecuteSqlProcedure("Delete from Req_TGP2 where Accession_ID = " &
                Accs & " and Parent_ID in (" & Parents & ")")
            End If
        End If
    End Sub

    Private Sub SaveQCWithRelease()
        If cmbRun.SelectedIndex <> -1 And cmbAccCtl.SelectedIndex <> -1 Then
            'Try
            Dim ItemR As MyList = cmbRun.SelectedItem
            Dim ItemC As MyList = cmbAccCtl.SelectedItem
            Dim i As Integer
            For i = 0 To dgvResults.RowCount - 1
                ExecuteSqlProcedure("Update QC_Results set Result = '" & Trim(dgvResults.Rows(i).Cells(2).Value) _
                & "', Comment = '" & Trim(dgvResults.Rows(i).Cells(16).Value) & "', T_Result = '" &
                Trim(dgvResults.Rows(i).Cells(17).Value) & "', Released = " &
                IIf(dgvResults.Rows(i).Cells(10).Value = True, 1, 0) & ", Released_By = " &
                IIf(dgvResults.Rows(i).Cells(10).Value = True, ThisUser.ID, Nothing) & ", Release_Time = " &
                IIf(dgvResults.Rows(i).Cells(10).Value = True, "'" & Date.Now & "'", Nothing) & " where " &
                "Run_ID = " & ItemR.ItemData & " and Control_ID = " & ItemC.ItemData & " and Test_ID = " &
                dgvResults.Rows(i).Cells(0).Value)
            Next
            UpdateRunValidation(ItemR.ItemData, ItemC.ItemData)
            ' Catch Ex As Exception
            '   MsgBox(Ex)
            'End Try
        End If
    End Sub

    Public Sub UpdateRunValidation(ByVal RunID As Long, ByVal ControlID As Long)
        'Try
        Dim Validaters As Byte = 0
        Dim Controls As Byte = 0
        Dim Validating As Boolean = False
        Override = CInt(cmbOverride.SelectedValue)
        Dim cnurv As New Data.SqlClient.SqlConnection(connString)
        cnurv.Open()
        Dim cmdurv As New Data.SqlClient.SqlCommand("Select Controls, Validaters from " &
        "Anas where ID in (Select Analysis_ID from Runs where ID = " & RunID & ")", cnurv)
        cmdurv.CommandType = Data.CommandType.Text
        Dim drurv As Data.SqlClient.SqlDataReader = cmdurv.ExecuteReader
        If drurv.HasRows Then
            While drurv.Read
                If drurv("Validaters") IsNot DBNull.Value Then Validaters = drurv("Validaters")
                If drurv("Controls") IsNot DBNull.Value Then Controls = drurv("Controls")
            End While
        End If
        cnurv.Close()
        cnurv = Nothing
        If Validaters = 0 Then  'No validation required
            ExecuteSqlProcedure("Update Runs set Validated = 1 where ID = " & RunID)
            'txtValid.Text = "Yes"
        Else        'Validation required
            If Controls = 0 Then    'Patients as controls
                Dim RanControls As Byte = 0
                Dim cnur1 As New Data.SqlClient.SqlConnection(connString)
                cnur1.Open()
                Dim cmdur1 As New Data.SqlClient.SqlCommand("Select DistinctCount(Control_ID) " &
                "as RanControls from QC_Results where Run_ID = " & RunID, cnur1)
                cmdur1.CommandType = Data.CommandType.Text
                Dim drur1 As Data.SqlClient.SqlDataReader = cmdur1.ExecuteReader
                If drur1.HasRows Then
                    While drur1.Read
                        If drur1("RanControls") IsNot DBNull.Value _
                        Then RanControls = drur1("RanControls")
                    End While
                End If
                cnur1.Close()
                cnur1 = Nothing
                If RanControls = Validaters Then 'Required controls selected
                    ExecuteSqlProcedure("Update Runs set Validated = 1 where ID = " & RunID)
                    'txtValid.Text = "Yes"
                Else
                    ExecuteSqlProcedure("Update Runs set Validated = 0 where ID = " & RunID)
                    'txtValid.Text = "No"
                End If
            Else        'QC material-Compare ranges
                Dim OverridePercent As Integer = cmbOverride.SelectedItem
                If OverridePercent = 0 Then
                    Validating = True
                Else
                    Dim i As Integer = 0
                    Dim OutOfRange As Integer = 0
                    Dim InRange As Integer = 0
                    Dim cnur1 As New Data.SqlClient.SqlConnection(connString)
                    cnur1.Open()
                    Dim cmdur1 As New Data.SqlClient.SqlCommand("Select * from QC_Results " &
                    "where Run_ID = " & RunID & " and Control_ID = " & ControlID, cnur1)
                    cmdur1.CommandType = Data.CommandType.Text
                    Dim drur1 As Data.SqlClient.SqlDataReader = cmdur1.ExecuteReader
                    If drur1.HasRows Then
                        While drur1.Read
                            If drur1("Result") Is DBNull.Value Then
                                OutOfRange = OutOfRange + 1
                            Else
                                If ControlInRange(RunID, drur1("Control_ID"),
                                drur1("Test_ID"), drur1("Result")) = True Then
                                    InRange = InRange + 1
                                Else
                                    OutOfRange = OutOfRange + 1
                                End If
                            End If
                            i = i + 1
                        End While
                    End If
                    cnur1.Close()
                    cnur1 = Nothing
                    '
                    If i > 0 Then
                        If CInt(InRange / i * 100) >= OverridePercent Then
                            Validating = True
                        End If
                    Else
                        Validating = False
                    End If
                End If
            End If
            '
        End If
        ExecuteSqlProcedure("Update Runs set Validaters = Validaters + 1 where ID = " & RunID)
        Dim RunValer As Integer = 0
        Dim RunValid As Boolean = False
        Dim cnur2 As New Data.SqlClient.SqlConnection(connString)
        cnur2.Open()
        Dim cmdur2 As New Data.SqlClient.SqlCommand("Select * from Runs where ID = " & RunID, cnur2)
        cmdur2.CommandType = Data.CommandType.Text
        Dim drur2 As Data.SqlClient.SqlDataReader = cmdur2.ExecuteReader
        If drur2.HasRows Then
            While drur2.Read
                RunValer = drur2("Validater")
            End While
        End If
        cnur2.Close()
        cnur2 = Nothing
        '
        If RunValer >= Validaters Then
            'txtValid.Text = "Yes"
            ExecuteSqlProcedure("Update Runs set Validated = 1 where ID = " & RunID)
        Else
            'txtValid.Text = "No"
            ExecuteSqlProcedure("Update Runs set Validated = 0 where ID = " & RunID)
        End If
        'Catch Ex As Exception
        '   MsgBox(Ex)
        'End Try
    End Sub

    Private Sub SaveQCWithoutRelease()
        If cmbRun.SelectedIndex <> -1 And cmbAccCtl.SelectedIndex <> -1 Then
            'Try
            Dim ItemR As MyList = cmbRun.SelectedItem
            Dim ItemC As MyList = cmbAccCtl.SelectedItem
            Dim i As Integer
            For i = 0 To dgvResults.RowCount - 1
                ExecuteSqlProcedure("Update QC_Results set Result = '" & Trim(dgvResults.Rows(i).Cells(2).Value) _
                & "', Comment = '" & Trim(dgvResults.Rows(i).Cells(16).Value) & "', T_Result = '" &
                Trim(dgvResults.Rows(i).Cells(17).Value) & "', Released = " &
                IIf(dgvResults.Rows(i).Cells(10).Value = True, 1, 0) & ", Released_By = " &
                IIf(dgvResults.Rows(i).Cells(10).Value = True, ThisUser.ID, Nothing) & ", Release_Time = " &
                IIf(dgvResults.Rows(i).Cells(10).Value = True, "'" & Date.Now & "'", Nothing) & " where " &
                "Run_ID = " & ItemR.ItemData & " and Control_ID = " & ItemC.ItemData & " and Test_ID = " &
                dgvResults.Rows(i).Cells(0).Value)
            Next
            UpdateRunValidation(ItemR.ItemData, ItemC.ItemData)
            ' Catch Ex As Exception
            '   MsgBox(Ex)
            'End Try
        End If
    End Sub

    Private Function ControlInRange(ByVal Run_ID As Long, ByVal Control_ID As Long,
    ByVal Test_ID As Integer, ByVal Result As String) As Boolean
        Dim InRange As Boolean = False
        Dim cncir As New SqlClient.SqlConnection(connString)
        cncir.Open()
        Dim cmdcir As New SqlClient.SqlCommand("Select * from Ana_Ranges where " &
        "Ana_ID in (Select Analysis_ID from Runs where ID = " & Run_ID & ") and " &
        "Control_ID = " & Control_ID & " and Test_ID = " & Test_ID, cncir)
        cmdcir.CommandType = CommandType.Text
        Dim drcir As SqlClient.SqlDataReader = cmdcir.ExecuteReader
        If drcir.HasRows Then
            While drcir.Read
                If drcir("Quantitative") = 0 Then     'Qualitative
                    If drcir("MeanNormal") IsNot DBNull.Value Then
                        If Result = drcir("MeanNormal") Then InRange = True
                    Else
                        InRange = True
                    End If
                Else        'Quantitative
                    If Val(Result) >= drcir("Low") And
                    Val(Result) <= drcir("High") Then InRange = True
                End If
            End While
        End If
        cncir.Close()
        cncir = Nothing
        Return InRange
        'Catch Ex As Exception
        '   MsgBox(Ex)
        'End Try
    End Function

    Private Sub SaveAccWithRelease()
        Try
            If cmbAccCtl.Text <> "" AndAlso IsNumeric(cmbAccCtl.Text) = True Then
                Dim ItemX As MyList = cmbRun.SelectedItem
                Dim ItemA As MyList = cmbAccCtl.SelectedItem
                Dim AccID As Long = Val(cmbAccCtl.Text)
                'Dim ResHistory As Boolean = GetResultHistory(AccID)
                Dim i As Integer
                Dim n As Integer
                '
                For i = 0 To dgvResults.RowCount - 1
                    If dgvResults.Rows(i).Cells(13).Value.ToString.StartsWith("ACC") Then   'Original
                        Dim cnrpt As New SqlClient.SqlConnection(connString)
                        cnrpt.Open()
                        Dim cmdrpt As New SqlClient.SqlCommand("Select a.*, b.Reported_Final " &
                        "from Acc_Results a inner join Requisitions b on b.ID = a.Accession_ID " &
                        "where b.ID = " & Val(dgvResults.Rows(i).Cells(12).Value) & " and " &
                        "a.Test_ID = " & Val(dgvResults.Rows(i).Cells(0).Value), cnrpt)
                        cmdrpt.CommandType = CommandType.Text
                        Dim drrpt As SqlClient.SqlDataReader = cmdrpt.ExecuteReader
                        If drrpt.HasRows Then
                            While drrpt.Read
                                If drrpt("Reported_Final") IsNot DBNull.Value _
                                AndAlso drrpt("Reported_Final") <> "#12:00:00AM#" Then
                                    If IsNumeric(Trim(drrpt("Result"))) And
                                    IsNumeric(Trim(dgvResults.Rows(i).Cells(2).Value)) Then
                                        If Val(Trim(drrpt("Result"))) <> Val(Trim(dgvResults.Rows(i).Cells(2).Value)) Then
                                            StoreResultInHistory(drrpt("Accession_ID"), -1,
                                            drrpt("Test_ID"), drrpt("Release_Time"),
                                            Trim(drrpt("Result")), ThisUser.ID)
                                        End If
                                    Else
                                        If Trim(drrpt("Result")) <> Trim(dgvResults.Rows(i).Cells(2).Value) Then
                                            StoreResultInHistory(drrpt("Accession_ID"), -1,
                                            drrpt("Test_ID"), drrpt("Release_Time"),
                                            Trim(drrpt("Result")), ThisUser.ID)
                                        End If
                                    End If
                                End If
                            End While
                        End If
                        cnrpt.Close()
                        cnrpt = Nothing
                        '
                        Dim cnacc As New SqlClient.SqlConnection(connString)
                        cnacc.Open()
                        Dim cmdupsert As New SqlClient.SqlCommand("Acc_Results_SP", cnacc)
                        cmdupsert.CommandType = CommandType.StoredProcedure
                        cmdupsert.Parameters.AddWithValue("@act", "Upsert")
                        cmdupsert.Parameters.AddWithValue("@Accession_ID", Val(dgvResults.Rows(i).Cells(12).Value))
                        cmdupsert.Parameters.AddWithValue("@Test_ID", Val(dgvResults.Rows(i).Cells(0).Value))
                        cmdupsert.Parameters.AddWithValue("@Result", Trim(dgvResults.Rows(i).Cells(2).Value))
                        cmdupsert.Parameters.AddWithValue("@Flag", dgvResults.Rows(i).Cells(4).Value)
                        If Trim(dgvResults.Rows(i).Cells(4).Value) = "" OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value).StartsWith("Pos") OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value).StartsWith("Reac") OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value).StartsWith("L") AndAlso
                        Trim(dgvResults.Rows(i).Cells(4).Value).Contains("H") OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value).StartsWith("R") OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value).StartsWith("INCONSIST") OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value).StartsWith("A") OrElse
                        (Trim(dgvResults.Rows(i).Cells(4).Value).StartsWith("Non") AndAlso
                        Trim(dgvResults.Rows(i).Cells(4).Value).StartsWith("Neg")) Then
                            cmdupsert.Parameters.AddWithValue("@Behavior", "Caution")
                        ElseIf Trim(dgvResults.Rows(i).Cells(4).Value) = "LP" OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value) = "HP" OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value) = "AP" OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value).Contains("Critical") OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value).Contains("Panic") Then
                            cmdupsert.Parameters.AddWithValue("@Behavior", "Panic")
                        Else
                            cmdupsert.Parameters.AddWithValue("@Behavior", "Ignore")
                        End If
                        cmdupsert.Parameters.AddWithValue("@NormalRange", dgvResults.Rows(i).Cells(5).Value)
                        cmdupsert.Parameters.AddWithValue("@T_Result", dgvResults.Rows(i).Cells(17).Value)
                        cmdupsert.Parameters.AddWithValue("@I_Result", "")
                        cmdupsert.Parameters.AddWithValue("@Comment", dgvResults.Rows(i).Cells(16).Value)
                        If ThisUser.Result_Release Then 'has permission
                            cmdupsert.Parameters.AddWithValue("@Released", CType(dgvResults.Rows(i).Cells(10).Value, Integer))
                            For n = 0 To UBound(ReleaserInfo, 2)
                                If ReleaserInfo(0, n) = dgvResults.Rows(i).Cells(0).Value Then
                                    cmdupsert.Parameters.AddWithValue("@Released_By", Val(ReleaserInfo(1, n)))
                                    cmdupsert.Parameters.AddWithValue("@Release_Time", CDate(ReleaserInfo(2, n)))
                                    Exit For
                                End If
                            Next
                        End If
                        Try
                            cmdupsert.ExecuteNonQuery()
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        Finally
                            cnacc.Close()
                            cnacc = Nothing
                        End Try
                    ElseIf dgvResults.Rows(i).Cells(13).Value.ToString.StartsWith("INF") Then   'Info
                        Dim cnrpt As New SqlClient.SqlConnection(connString)
                        cnrpt.Open()
                        Dim cmdrpt As New SqlClient.SqlCommand("Select a.*, b.Reported_Final " &
                        "from Acc_Info_Results a inner join Requisitions b on b.ID = a.Accession_ID " &
                        "where b.ID = " & Val(dgvResults.Rows(i).Cells(12).Value) & " and " &
                        "a.Test_ID = " & Val(dgvResults.Rows(i).Cells(14).Value) & " and " &
                        "a.Info_ID = " & Val(dgvResults.Rows(i).Cells(0).Value), cnrpt)
                        cmdrpt.CommandType = CommandType.Text
                        Dim drrpt As SqlClient.SqlDataReader = cmdrpt.ExecuteReader
                        If drrpt.HasRows Then
                            While drrpt.Read
                                If drrpt("Reported_Final") IsNot DBNull.Value _
                                AndAlso drrpt("Reported_Final") <> "#12:00:00AM#" Then
                                    If IsNumeric(Trim(drrpt("Result"))) And
                                    IsNumeric(Trim(dgvResults.Rows(i).Cells(2).Value)) Then
                                        If Val(Trim(drrpt("Result"))) <> Val(Trim(dgvResults.Rows(i).Cells(2).Value)) Then
                                            StoreResultInHistory(drrpt("Accession_ID"), -1,
                                            drrpt("Test_ID"), drrpt("Release_Time"),
                                            Trim(drrpt("Result")), ThisUser.ID)
                                        End If
                                    Else
                                        If Trim(drrpt("Result")) <> Trim(dgvResults.Rows(i).Cells(2).Value) Then
                                            StoreResultInHistory(drrpt("Accession_ID"), -1,
                                            drrpt("Test_ID"), drrpt("Release_Time"),
                                            Trim(drrpt("Result")), ThisUser.ID)
                                        End If
                                    End If
                                End If
                            End While
                        End If
                        cnrpt.Close()
                        cnrpt = Nothing
                        '
                        Dim cnacc As New SqlClient.SqlConnection(connString)
                        cnacc.Open()
                        Dim cmdupsert As New SqlClient.SqlCommand("Acc_Info_Results_SP", cnacc)
                        cmdupsert.CommandType = CommandType.StoredProcedure
                        cmdupsert.Parameters.AddWithValue("@act", "Upsert")
                        cmdupsert.Parameters.AddWithValue("@Accession_ID", Val(dgvResults.Rows(i).Cells(12).Value))
                        cmdupsert.Parameters.AddWithValue("@Test_ID", Val(dgvResults.Rows(i).Cells(14).Value))
                        cmdupsert.Parameters.AddWithValue("@Info_ID", Val(dgvResults.Rows(i).Cells(0).Value))
                        cmdupsert.Parameters.AddWithValue("@Result", Trim(dgvResults.Rows(i).Cells(2).Value))
                        cmdupsert.Parameters.AddWithValue("@Flag", dgvResults.Rows(i).Cells(4).Value)
                        If Trim(dgvResults.Rows(i).Cells(4).Value) = "" OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value).StartsWith("Pos") OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value).StartsWith("Reac") OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value).StartsWith("L") AndAlso
                        Trim(dgvResults.Rows(i).Cells(4).Value).Contains("H") OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value).StartsWith("R") OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value).StartsWith("INCONSIST") OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value).StartsWith("A") OrElse
                        (Trim(dgvResults.Rows(i).Cells(4).Value).StartsWith("Non") AndAlso
                        Trim(dgvResults.Rows(i).Cells(4).Value).StartsWith("Neg")) Then
                            cmdupsert.Parameters.AddWithValue("@Behavior", "Caution")
                        ElseIf Trim(dgvResults.Rows(i).Cells(4).Value) = "LP" OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value) = "HP" OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value) = "AP" OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value).Contains("Critical") OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value).Contains("Panic") Then
                            cmdupsert.Parameters.AddWithValue("@Behavior", "Panic")
                        Else
                            cmdupsert.Parameters.AddWithValue("@Behavior", "Ignore")
                        End If
                        cmdupsert.Parameters.AddWithValue("@NormalRange", dgvResults.Rows(i).Cells(5).Value)
                        cmdupsert.Parameters.AddWithValue("@T_Result", dgvResults.Rows(i).Cells(17).Value)
                        cmdupsert.Parameters.AddWithValue("@I_Result", "")
                        cmdupsert.Parameters.AddWithValue("@Comment", dgvResults.Rows(i).Cells(16).Value)
                        If ThisUser.Result_Release Then 'has permission
                            cmdupsert.Parameters.AddWithValue("@Released", CType(dgvResults.Rows(i).Cells(10).Value, Integer))
                            For n = 0 To UBound(ReleaserInfo, 2)
                                If ReleaserInfo(0, n) = dgvResults.Rows(i).Cells(0).Value Then
                                    cmdupsert.Parameters.AddWithValue("@Released_By", Val(ReleaserInfo(1, n)))
                                    cmdupsert.Parameters.AddWithValue("@Release_Time", CDate(ReleaserInfo(2, n)))
                                    Exit For
                                End If
                            Next
                        End If
                        Try
                            cmdupsert.ExecuteNonQuery()
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        Finally
                            cnacc.Close()
                            cnacc = Nothing
                        End Try
                    ElseIf dgvResults.Rows(i).Cells(13).Value.ToString.StartsWith("REF") Then 'Reflex
                        Dim cnrpt As New SqlClient.SqlConnection(connString)
                        cnrpt.Open()
                        Dim cmdrpt As New SqlClient.SqlCommand("Select a.*, b.Reported_Final " &
                        "from Ref_Results a inner join Requisitions b on b.ID = a.Accession_ID " &
                        "where b.ID = " & Val(dgvResults.Rows(i).Cells(12).Value) & " and " &
                        "a.Reflexer_ID = " & Val(dgvResults.Rows(i).Cells(14).Value) & " and " &
                        "a.Reflexed_ID = " & Val(dgvResults.Rows(i).Cells(15).Value) & " and " &
                        "a.Test_ID = " & Val(dgvResults.Rows(i).Cells(0).Value), cnrpt)
                        cmdrpt.CommandType = CommandType.Text
                        Dim drrpt As SqlClient.SqlDataReader = cmdrpt.ExecuteReader
                        If drrpt.HasRows Then
                            While drrpt.Read
                                If drrpt("Reported_Final") IsNot DBNull.Value _
                                AndAlso drrpt("Reported_Final") <> "#12:00:00AM#" Then
                                    If IsNumeric(Trim(drrpt("Result"))) And
                                    IsNumeric(Trim(dgvResults.Rows(i).Cells(2).Value)) Then
                                        If Val(Trim(drrpt("Result"))) <> Val(Trim(dgvResults.Rows(i).Cells(2).Value)) Then
                                            StoreResultInHistory(drrpt("Accession_ID"), -1,
                                            drrpt("Test_ID"), drrpt("Release_Time"),
                                            Trim(drrpt("Result")), ThisUser.ID)
                                        End If
                                    Else
                                        If Trim(drrpt("Result")) <> Trim(dgvResults.Rows(i).Cells(2).Value) Then
                                            StoreResultInHistory(drrpt("Accession_ID"), -1,
                                            drrpt("Test_ID"), drrpt("Release_Time"),
                                            Trim(drrpt("Result")), ThisUser.ID)
                                        End If
                                    End If
                                End If
                            End While
                        End If
                        cnrpt.Close()
                        cnrpt = Nothing
                        '
                        Dim cnacc As New SqlClient.SqlConnection(connString)
                        cnacc.Open()
                        Dim cmdupsert As New SqlClient.SqlCommand("Ref_Results_SP", cnacc)
                        cmdupsert.CommandType = CommandType.StoredProcedure
                        cmdupsert.Parameters.AddWithValue("@act", "Upsert")
                        cmdupsert.Parameters.AddWithValue("@Accession_ID", Val(dgvResults.Rows(i).Cells(12).Value))
                        cmdupsert.Parameters.AddWithValue("@Reflexer_ID", Val(dgvResults.Rows(i).Cells(14).Value))
                        cmdupsert.Parameters.AddWithValue("@Reflexed_ID", Val(dgvResults.Rows(i).Cells(15).Value))
                        cmdupsert.Parameters.AddWithValue("@Test_ID", Val(dgvResults.Rows(i).Cells(0).Value))
                        cmdupsert.Parameters.AddWithValue("@Result", Trim(dgvResults.Rows(i).Cells(2).Value))
                        cmdupsert.Parameters.AddWithValue("@Flag", dgvResults.Rows(i).Cells(4).Value)
                        If Trim(dgvResults.Rows(i).Cells(4).Value) = "" OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value).StartsWith("Pos") OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value).StartsWith("Reac") OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value).StartsWith("L") AndAlso
                        Trim(dgvResults.Rows(i).Cells(4).Value).Contains("H") OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value).StartsWith("R") OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value).StartsWith("INCONSIST") OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value).StartsWith("A") OrElse
                        (Trim(dgvResults.Rows(i).Cells(4).Value).StartsWith("Non") AndAlso
                        Trim(dgvResults.Rows(i).Cells(4).Value).StartsWith("Neg")) Then
                            cmdupsert.Parameters.AddWithValue("@Behavior", "Caution")
                        ElseIf Trim(dgvResults.Rows(i).Cells(4).Value) = "LP" OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value) = "HP" OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value) = "AP" OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value).Contains("Critical") OrElse
                        Trim(dgvResults.Rows(i).Cells(4).Value).Contains("Panic") Then
                            cmdupsert.Parameters.AddWithValue("@Behavior", "Panic")
                        Else
                            cmdupsert.Parameters.AddWithValue("@Behavior", "Ignore")
                        End If
                        cmdupsert.Parameters.AddWithValue("@NormalRange", dgvResults.Rows(i).Cells(5).Value)
                        cmdupsert.Parameters.AddWithValue("@T_Result", dgvResults.Rows(i).Cells(17).Value)
                        cmdupsert.Parameters.AddWithValue("@I_Result", "")
                        cmdupsert.Parameters.AddWithValue("@Comment", dgvResults.Rows(i).Cells(16).Value)
                        If ThisUser.Result_Release Then 'has permission
                            cmdupsert.Parameters.AddWithValue("@Released", CType(dgvResults.Rows(i).Cells(10).Value, Integer))
                            For n = 0 To UBound(ReleaserInfo, 2)
                                If ReleaserInfo(0, n) = dgvResults.Rows(i).Cells(0).Value Then
                                    cmdupsert.Parameters.AddWithValue("@Released_By", Val(ReleaserInfo(1, n)))
                                    cmdupsert.Parameters.AddWithValue("@Release_Time", CDate(ReleaserInfo(2, n)))
                                    Exit For
                                End If
                            Next
                        End If
                        Try
                            cmdupsert.ExecuteNonQuery()
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        Finally
                            cnacc.Close()
                            cnacc = Nothing
                        End Try
                        '
                        SaveAccessionComment(AccID, txtNote.Text)
                        '
                        If ExtPDF IsNot Nothing Then
                            SaveExtendedResult(cmbAccCtl.Text, ExtPDF)
                        Else
                            ExecuteSqlProcedure("Delete from Extend_Results where Accession_ID = " & cmbAccCtl.Text)
                        End If
                        'If IsDirty = True Then
                        '    If IsDate(txtRptDate.Text & " " & txtRptTime.Text) Then _
                        '    UpdateReportTime(AccID, CDate(txtRptDate.Text & " " & txtRptTime.Text))
                        '    IsDirty = False
                        'End If
                        'CN.Execute("Delete from Event_Capture where Accession_ID = " & _
                        'AccID & " and Event_ID in (Select ID from Events " & _
                        '"where Event_Type in ('Report', 'Panic', 'Result'))")
                        LogEvent(AccID, 22, GetOrdProvIDFromAccID(AccID),
                        "Result Edit", False, ThisUser.Name, "Result Edit Manual")
                        '********** Audit Trail Code ***************
                        '0=TestID, 1=SavedRes, 2=SavedRel, 3=ChangedRes, 4=ChangedRel, 
                        '5=SavedNote, 6=SavedRTF, 7=newNote, 8=newRTF
                        If SystemConfig.AuditTrail = True Then
                            For c As Integer = 0 To dgvResults.RowCount - 1
                                ResultsAT(3, c) = Trim(dgvResults.Rows(i).Cells(2).Value)
                                ResultsAT(4, c) = IIf(dgvResults.Rows(i).Cells(10).Value =
                                True, "1", "0")
                                ResultsAT(7, c) = Trim(dgvResults.Rows(i).Cells(16).Value)
                                ResultsAT(8, c) = Trim(dgvResults.Rows(i).Cells(17).Value)
                            Next
                            Dim ResOld As String = ""
                            Dim ResNew As String = ""
                            Dim NoteOld As String = ""
                            Dim RTFOld As String = ""
                            Dim NoteNew As String = ""
                            Dim RTFNew As String = ""
                            For m As Integer = LBound(ResultsAT, 2) To UBound(ResultsAT, 2)
                                If ResultsAT(1, m) <> ResultsAT(3, m) Or
                                ResultsAT(2, m) <> ResultsAT(4, m) Then
                                    If ResultsAT(1, i) = "" Then    'new
                                        ResNew += ResultsAT(0, m) & "=" &
                                        ResultsAT(3, m) & "^" & ResultsAT(4, m) & "|"
                                    Else
                                        ResOld += ResultsAT(0, m) & "=" &
                                        ResultsAT(1, m) & "^" & ResultsAT(2, m) & "|"
                                        ResNew += ResultsAT(0, i) & "=" &
                                        ResultsAT(3, m) & "^" & ResultsAT(4, m) & "|"
                                    End If
                                End If
                                If ResultsAT(5, m) <> ResultsAT(7, m) Then  'note change
                                    If ResultsAT(5, m) <> "" Then _
                                    NoteOld += ResultsAT(0, m) & "=" & ResultsAT(5, m) & "|"
                                    NoteNew += ResultsAT(0, m) & "=" & ResultsAT(7, m) & "|"
                                End If
                                If ResultsAT(6, m) <> ResultsAT(8, m) Then  'RTF change
                                    If ResultsAT(6, m) <> "" Then _
                                    RTFOld += ResultsAT(0, m) & "=" & RTF_To_Text(ResultsAT(6, m)) & "|"
                                    RTFNew += ResultsAT(0, m) & "=" & RTF_To_Text(ResultsAT(8, m)) & "|"
                                End If
                            Next
                            If ResOld.Length > 1 Then ResOld.Remove(ResOld.Length - 1, 1)
                            If ResNew.Length > 1 Then ResNew.Remove(ResNew.Length - 1, 1)
                            If NoteNew.Length > 1 Then NoteNew.Remove(NoteNew.Length - 1, 1)
                            If NoteOld.Length > 1 Then NoteOld.Remove(NoteOld.Length - 1, 1)
                            If RTFNew.Length > 1 Then RTFNew.Remove(RTFNew.Length - 1, 1)
                            If RTFOld.Length > 1 Then RTFOld.Remove(RTFOld.Length - 1, 1)
                            '
                            If ResOld <> "" Or ResNew <> "" Then LogUserEvent(ThisUser.ID,
                            22, Date.Now, "Accession", AccID, Trim(Microsoft.VisualBasic.Mid(ResOld,
                            1, 4000)), Trim(Microsoft.VisualBasic.Mid(ResNew, 1, 4000)))
                            '
                            If NoteOld <> "" Or NoteNew <> "" Then LogUserEvent(ThisUser.ID,
                            24, Date.Now, "Accession", AccID, Trim(Microsoft.VisualBasic.Mid(NoteOld,
                            1, 4000)), Trim(Microsoft.VisualBasic.Mid(NoteNew, 1, 4000)))
                            '
                            If RTFOld <> "" Or RTFNew <> "" Then LogUserEvent(ThisUser.ID,
                            26, Date.Now, "Accession", AccID, Trim(Microsoft.VisualBasic.Mid(RTFOld,
                            1, 4000)), Trim(Microsoft.VisualBasic.Mid(RTFNew, 1, 4000)))
                        End If
                    End If
                    '********** End Audit Trail ****************
                    If SystemConfig.HL7AutoPub = True Then _
                    UpdateAccDisbursement(Val(cmbAccCtl.Text))
                    '
                    ReDim ReleaserInfo(2, 0)
                Next
            End If
        Catch Ex As Exception
            MsgBox(Ex)
        End Try
    End Sub

    Private Sub SaveExtendedResult_OLD(ByVal AccID As Long, ByVal ExtPDF As Byte())
        'Dim CNE As New ADODB.Connection
        'CNE.Open(connstring)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select * from Extend_Results where Accession_ID = " & AccID,
        'CNE, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        'If Rs.BOF Then Rs.AddNew()
        'Rs.Fields("Accession_ID").Value = AccID
        'If Rs.Fields("Result").Value Is DBNull.Value _
        'OrElse Rs.Fields("Result").Value Is Nothing OrElse
        'Rs.Fields("Result").Value IsNot ExtPDF Then
        '    Rs.Fields("Result").Value = CompressByte(ExtPDF)
        '    If Rs.Fields("Released").Value Is DBNull.Value Then
        '        Rs.Fields("Released").Value = chkExtRelease.Checked
        '        Rs.Fields("Release_Time").Value = Date.Now
        '        Rs.Fields("Released_By").Value = ThisUser.ID
        '    End If
        'End If
        'Rs.Update()
        'Rs.Close()
        'Rs = Nothing
        'CNE.Close()
        'CNE = Nothing
    End Sub

    Private Sub SaveExtendedResult(ByVal AccID As Long, ByVal ExtPDF As Byte())

        Using connection As New SqlConnection(connString)
            connection.Open()

            Using transaction As SqlTransaction = connection.BeginTransaction()
                Try
                    ' Check if the record exists
                    Dim selectQuery As String = "SELECT * FROM Extend_Results WHERE Accession_ID = @AccID"
                    Using selectCommand As New SqlCommand(selectQuery, connection, transaction)
                        selectCommand.Parameters.AddWithValue("@AccID", AccID)

                        Using reader As SqlDataReader = selectCommand.ExecuteReader()
                            If reader.HasRows Then
                                ' If the record exists, update it
                                reader.Read()
                                Dim updateQuery As String = "UPDATE Extend_Results SET Result = @Result, Released = @Released, Release_Time = @ReleaseTime, Released_By = @ReleasedBy WHERE Accession_ID = @AccID"
                                Using updateCommand As New SqlCommand(updateQuery, connection, transaction)
                                    updateCommand.Parameters.AddWithValue("@AccID", AccID)
                                    updateCommand.Parameters.AddWithValue("@Result", ExtPDF) '' CompressByte(ExtPDF)
                                    updateCommand.Parameters.AddWithValue("@Released", If(reader("Released") Is DBNull.Value, chkExtRelease.Checked, reader("Released")))
                                    updateCommand.Parameters.AddWithValue("@ReleaseTime", If(reader("Released") Is DBNull.Value, DateTime.Now, reader("Release_Time")))
                                    updateCommand.Parameters.AddWithValue("@ReleasedBy", If(reader("Released") Is DBNull.Value, ThisUser.ID, reader("Released_By")))

                                    updateCommand.ExecuteNonQuery()
                                End Using
                            Else
                                ' If the record does not exist, insert it
                                Dim insertQuery As String = "INSERT INTO Extend_Results (Accession_ID, Result, Released, Release_Time, Released_By) VALUES (@AccID, @Result, @Released, @ReleaseTime, @ReleasedBy)"
                                Using insertCommand As New SqlCommand(insertQuery, connection, transaction)
                                    insertCommand.Parameters.AddWithValue("@AccID", AccID)
                                    insertCommand.Parameters.AddWithValue("@Result", ExtPDF) ' CompressByte(ExtPDF))
                                    insertCommand.Parameters.AddWithValue("@Released", chkExtRelease.Checked)
                                    insertCommand.Parameters.AddWithValue("@ReleaseTime", DateTime.Now)
                                    insertCommand.Parameters.AddWithValue("@ReleasedBy", ThisUser.ID)

                                    insertCommand.ExecuteNonQuery()
                                End Using
                            End If
                        End Using
                    End Using

                    ' Commit the transaction
                    transaction.Commit()
                Catch ex As Exception
                    ' Rollback the transaction if an error occurs
                    transaction.Rollback()
                    Throw
                End Try
            End Using
        End Using
    End Sub

    Private Function GetReportConfigs(ByVal AccID As Long) As String()
        Dim Configs() As String = {"", "", "", "", "", "", "", "", "", ""}
        '0=ProviderID, 1=Complete, 2=Print, 3=Prolison, 4=Interface, 5=RPTFax
        '6=Fax#, 7=RPTEmail, 8=Email, 9=RPTFile
        Dim cnrc As New SqlClient.SqlConnection(connString)
        cnrc.Open()
        Dim cmdrc As New SqlClient.SqlCommand("Select * from " &
        "Providers where ID in (Select OrderingProvider_ID " &
        "from Requisitions where ID = " & AccID & ")", cnrc)
        cmdrc.CommandType = CommandType.Text
        Dim drrc As SqlClient.SqlDataReader = cmdrc.ExecuteReader
        If drrc.HasRows Then
            While drrc.Read
                Configs(0) = drrc("ID").ToString
                If drrc("RPTComplete") IsNot DBNull.Value Then
                    Configs(1) = drrc("RPTComplete").ToString
                Else
                    Configs(1) = ""
                End If
                If drrc("RDM_Print") IsNot DBNull.Value Then
                    Configs(2) = drrc("RDM_Print").ToString
                Else
                    Configs(2) = ""
                End If
                If drrc("RDM_Prolison") IsNot DBNull.Value Then
                    Configs(3) = drrc("RDM_Prolison").ToString
                Else
                    Configs(3) = ""
                End If
                If drrc("RDM_Interface") IsNot DBNull.Value Then
                    Configs(4) = drrc("RDM_Interface").ToString
                Else
                    Configs(4) = ""
                End If
                If drrc("Fax") IsNot DBNull.Value _
                AndAlso Trim(drrc("Fax").ToString) <> "" Then
                    If drrc("RDM_Fax") IsNot DBNull.Value Then
                        Configs(5) = drrc("RDM_Fax").ToString
                        Configs(6) = Trim(drrc("Fax"))
                    Else
                        Configs(5) = "False"
                        Configs(6) = Trim(drrc("Fax"))
                    End If
                Else
                    Configs(5) = "False"
                    Configs(6) = ""
                End If
                If drrc("Email") IsNot DBNull.Value _
                AndAlso Trim(drrc("Email").ToString) <> "" Then
                    If drrc("RDM_Email") IsNot DBNull.Value Then
                        Configs(7) = drrc("RDM_Email").ToString
                        Configs(8) = Trim(drrc("Email"))
                    Else
                        Configs(7) = "False"
                        Configs(9) = Trim(drrc("Email"))
                    End If
                Else
                    Configs(7) = "False"
                    Configs(8) = ""
                End If
                If drrc("ResRPTFile") IsNot DBNull.Value _
                AndAlso Trim(drrc("ResRPTFile").ToString) <> "" Then
                    Configs(9) = Trim(drrc("ResRPTFile"))
                Else
                    Configs(9) = ""
                End If
            End While
        End If
        cnrc.Close()
        cnrc = Nothing
        Return Configs
    End Function

    Private Sub SaveAccessionComment(ByVal AccID As Long, ByVal Note As String)
        If cmbDirector.SelectedIndex <> -1 Then
            Dim ItemX As MyList = cmbDirector.SelectedItem
            ExecuteSqlProcedure("Update Requisitions set Comment = '" & Note &
            "', Director_ID = " & ItemX.ItemData & " where ID = " & AccID)
        Else
            ExecuteSqlProcedure("Update Requisitions set " &
            "Comment = '" & Note & "' where ID = " & AccID)
        End If
    End Sub

    Private Function Releasing() As Boolean
        Dim i As Integer
        Dim Rel As Boolean = False
        For i = 0 To dgvResults.RowCount - 1
            If dgvResults.Rows(i).Cells("Release").Value <> 0 Then
                Rel = True
                Exit For
            End If
        Next
        Releasing = Rel
    End Function

    Private Sub frmResults_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If dgvResults.RowCount > 0 Then _
            If AccDirty Then SaveAccChanges(dgvResults.Rows(0).Cells(12).Value)
            If IO.Directory.Exists(My.Application.Info.DirectoryPath & "\Temp\" & ThisUser.ID.ToString & "\") Then
                Dim directoryName As String = My.Application.Info.DirectoryPath & "\Temp\" & ThisUser.ID.ToString & "\"
                Dim F As String
                For Each F In IO.Directory.GetFiles(directoryName, "*.*", IO.SearchOption.TopDirectoryOnly)
                    IO.File.Delete(F)
                Next
                IO.Directory.Delete(My.Application.Info.DirectoryPath & "\Temp\" & ThisUser.ID.ToString & "\")
            End If

            Dim folderPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName)

            ' Create the directory if it does not exist
            If Directory.Exists(folderPath) Then
                For Each F In IO.Directory.GetFiles(folderPath, "*.*", IO.SearchOption.TopDirectoryOnly)
                    IO.File.Delete(F)
                Next
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub frmResults_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If frmATRResults.IsHandleCreated Then frmATRResults.Close()
        Me.MaximumSize = MaxSize
        CorrectedStyle.ForeColor = Color.Blue
        CorrectedStyle.BackColor = Color.GreenYellow
        PanicStyle.BackColor = Color.Red
        PanicStyle.ForeColor = Color.White
        txtRptDate.Mask = Replace(Replace(Replace(SystemConfig.DateFormat, "y", "0"), "M", "0"), "d", "0")
        AbnormalStyle.BackColor = Color.Yellow
        lblAccRun.Text += " (" & SystemConfig.DateFormat & ")"
        dtpFromDate.Format = DateTimePickerFormat.Custom
        dtpFromDate.CustomFormat = SystemConfig.DateFormat

        dtpToDate.Format = DateTimePickerFormat.Custom
        dtpToDate.CustomFormat = SystemConfig.DateFormat

        PopulateWorksheets()
        PopulateDirectors()
        AccDirty = False
        ' AxAcroPDF1.Controls.Clear()
        ' PdfViewer1.Controls.Clear()
        If ThisUser.Result_Release = True Then
            btnRelease.Enabled = True
            btnBlock.Enabled = True
        Else
            btnRelease.Enabled = False
            btnBlock.Enabled = False
        End If
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
        loadD()
        If Not MyLab.LabName.ToLower().Contains("ayas") Then
            Wildcard.Hide()
            Label20.Hide()
        End If


        If ThisUser.Test_Mgmt = True Then
            ' Initialize the context menu and add the "Open Tests" menu item
            contextMenuStrip.Items.Add(openTestsMenuItem)
            AddHandler openTestsMenuItem.Click, AddressOf OpenTestsMenuItem_Click

            ' Subscribe to the CellMouseClick event of the DataGridView
            AddHandler dgvResults.CellMouseClick, AddressOf dgvResults_CellMouseClick
        End If
    End Sub

    Private pdfItems As New List(Of PdfItem)()
    Private pdfPaths As New List(Of String)()
    Private currentIndex As Integer = 0

    Private Async Sub DisplayExtendResult(ByVal AccID As Long)
        Try
            LoggerHelper.LogInfo("Starting DisplayExtendResult.")

            ''********************

            ' Initialize WebView2 control
            'Await WebView.EnsureCoreWebView2Async(Nothing)

            ' Retrieve all PDFs and their Released status from the database
            pdfItems = GetAllPdfsFromDatabase(AccID)

            ClearWebView()
            If pdfItems.Count = 0 Then
                chkExtRelease.Checked = False
                btnBrowse.Enabled = True
                btnDelPDF.Enabled = False

                '    ClearWebView()

                'btnPrevious.Enabled = False
                'btnNext.Enabled = False

            Else


                '=i used this line to clear list because it saves all previous loaded pdf files=========================
                pdfPaths.Clear()


                For i As Integer = 0 To pdfItems.Count - 1
                    pdfPaths.Add(SavePdfToTempFile(AccID, pdfItems(i).PdfData, i))
                Next

                '==========================
                ' NEED TO SEE THIS LATER, WHAT IS THE PURPOSE OF THIS?

                ' Load the first PDF and its status
                If pdfPaths.Count > 0 Then
                    LoadPdfAndStatus(pdfPaths(currentIndex), pdfItems(currentIndex).Released)
                End If

                '==========================
                btnDelPDF.Enabled = True

            End If

        Catch ex As Exception
            LoggerHelper.LogError(ex, "Error DisplayExtendResult.")
        End Try
    End Sub

    Private Sub PopulateDirectors()
        cmbDirector.Items.Clear()
        Dim Degree As String = ""
        Dim sSQL As String = "Select * from Lab_Directors where Company_ID = " & MyLab.ID &
        " and (EffectiveTo is NULL or EffectiveTo > '" & Date.Today & "') order by IsDefault DESC"
        '
        Dim cnpd As New SqlClient.SqlConnection(connString)
        cnpd.Open()
        Dim cmdpd As New SqlClient.SqlCommand(sSQL, cnpd)
        cmdpd.CommandType = CommandType.Text
        Dim drpd As SqlClient.SqlDataReader = cmdpd.ExecuteReader
        If drpd.HasRows Then
            While drpd.Read
                If drpd("Degree") IsNot DBNull.Value _
                AndAlso drpd("Degree") <> "" Then
                    Degree = drpd("Degree")
                Else
                    Degree = ""
                End If
                cmbDirector.Items.Add(New MyList(drpd("LastName") &
                ", " & drpd("FirstName") & IIf(Degree = "", "",
                " " & Degree), drpd("ID")))
            End While
        End If
        cnpd.Close()
        cnpd = Nothing
        '
        If cmbDirector.Items.Count > 0 Then cmbDirector.SelectedIndex = 0
    End Sub

    Private Sub PopulateWorksheets()
        cmbRun.Items.Clear()
        Dim sSQL As String = "Select * from Worksheets"
        Dim cnpw As New SqlClient.SqlConnection(connString)
        cnpw.Open()
        Dim cmdpw As New SqlClient.SqlCommand(sSQL, cnpw)
        cmdpw.CommandType = CommandType.Text
        Dim drpw As SqlClient.SqlDataReader = cmdpw.ExecuteReader
        If drpw.HasRows Then
            While drpw.Read
                cmbRun.Items.Add(New MyList(drpw("Name"), drpw("ID")))
            End While
        End If
        cnpw.Close()
        cnpw = Nothing
    End Sub

    Private Sub PopulateRuns(ByVal StartDate As Date, ByVal EndDate As Date)
        'cmbRun.SelectedIndex = -1
        cmbAccCtl.SelectedIndex = -1
        cmbRun.Items.Clear()
        cmbAccCtl.Items.Clear()
        Dim ItemX As MyList = cmbRun.SelectedItem
        Dim cnpr As New SqlClient.SqlConnection(connString)
        cnpr.Open()

        Dim cmdpr As New SqlClient.SqlCommand("Select * from Runs where RunDate between @StartDate and @EndDate", cnpr)
        cmdpr.CommandType = CommandType.Text

        ' Add parameters to prevent SQL injection
        cmdpr.Parameters.AddWithValue("@StartDate", StartDate.Date)
        cmdpr.Parameters.AddWithValue("@EndDate", EndDate)

        'Dim cmdpr As New SqlClient.SqlCommand("Select * from " &
        '"Runs where RunDate between '" & Format(RunDate,
        'SystemConfig.DateFormat) & "' and '" & Format(RunDate,
        ''SystemConfig.DateFormat) & " 23:59:00'", cnpr)
        cmdpr.CommandType = CommandType.Text
        Dim drpr As SqlClient.SqlDataReader = cmdpr.ExecuteReader
        If drpr.HasRows Then
            While drpr.Read
                cmbRun.Items.Add(New MyList(drpr("Name"), drpr("ID")))
            End While
        End If
        cnpr.Close()
        cnpr = Nothing
    End Sub

    Private Sub LoadAccessions(ByVal RunID As Long)
        cmbAccCtl.Items.Clear()
        Dim cnla As New SqlClient.SqlConnection(connString)
        cnla.Open()
        Dim cmdla As New SqlClient.SqlCommand("Select distinct " &
        "Accession_ID from Acc_Results where Run_ID = " & RunID, cnla)
        cmdla.CommandType = CommandType.Text
        Dim drla As SqlClient.SqlDataReader = cmdla.ExecuteReader
        If drla.HasRows Then
            While drla.Read
                cmbAccCtl.Items.Add(New MyList(drla("Accession_ID").ToString, drla("Accession_ID")))
            End While
        End If
        cnla.Close()
        cnla = Nothing
    End Sub

    Private Sub LoadControls(ByVal RunID As Long)
        cmbAccCtl.Items.Clear()
        Dim ControlName As String = ""
        Dim cnlc As New SqlClient.SqlConnection(connString)
        cnlc.Open()
        Dim cmdlc As New SqlClient.SqlCommand("Select distinct " &
        "Control_ID from QC_Results where Run_ID = " & RunID, cnlc)
        cmdlc.CommandType = CommandType.Text
        Dim drlc As SqlClient.SqlDataReader = cmdlc.ExecuteReader
        If drlc.HasRows Then
            While drlc.Read
                ControlName = GetControlName(drlc("Control_ID"), RunID)
                If ControlName <> "" Then
                    cmbAccCtl.Items.Add(New MyList(ControlName,
                    drlc("Control_ID")))
                Else
                    cmbAccCtl.Items.Add(New MyList(drlc("Control_ID").ToString,
                    drlc("Control_ID")))
                End If
            End While
        End If
        cnlc.Close()
        cnlc = Nothing
    End Sub

    Private Function GetControlName(ByVal ControlID As Long, ByVal RunID As Long) As String
        Dim ControlName As String = ""
        Dim cncn As New SqlClient.SqlConnection(connString)
        cncn.Open()
        Dim cmdcn As New SqlClient.SqlCommand("Select ControlName " &
        "from Ana_Control where Ana_ID in (Select Analysis_ID from " &
        "Runs where ID = " & RunID & ") and Control_ID = " & ControlID, cncn)
        cmdcn.CommandType = CommandType.Text
        Dim drcn As SqlClient.SqlDataReader = cmdcn.ExecuteReader
        If drcn.HasRows Then
            While drcn.Read
                ControlName = drcn("ControlName")
            End While
        End If
        cncn.Close()
        cncn = Nothing
        Return ControlName
    End Function

    Private Sub btnAccQC_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAccQC.CheckedChanged
        If btnAccQC.Checked = False Then    'Accession
            btnAccQC.Text = "Accessioned"
            lblAccCtl.Text = "Accession"
            btnAccQC.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Acc.ico")
            cmbAccCtl.DropDownStyle = ComboBoxStyle.DropDown
            PopulateWorksheets()
            btnPrev.Enabled = True
            cmbOverride.Enabled = False
            lblAccRun.Text = "Acc Date"
            lblWorkBatch.Text = "Worksheet"
        Else
            btnAccQC.Text = "Quality Control"
            lblAccCtl.Text = "Control"
            btnAccQC.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\chart.ico")
            cmbAccCtl.DropDownStyle = ComboBoxStyle.DropDownList
            PopulateRuns(dtpFromDate.Value, GetEndDate(dtpToDate.Value))
            btnPrev.Enabled = False
            cmbOverride.Enabled = True
            lblAccRun.Text = "Run Date"
            lblWorkBatch.Text = "Batch Run"
        End If
        cmbRun.Text = ""
        cmbAccCtl.Text = ""
        cmbRun.SelectedIndex = -1
        cmbAccCtl.SelectedIndex = -1
        dgvResults.Rows.Clear()
    End Sub

    Private Sub UpdateValStatus(ByVal RunID As Long)
        Dim i As Integer
        Dim cn1 As New SqlClient.SqlConnection(connString)
        cn1.Open()
        Dim cmd1 As New SqlClient.SqlCommand("Select " &
        "Validated from Runs where ID = " & RunID, cn1)
        cmd1.CommandType = CommandType.Text
        Dim dr1 As SqlClient.SqlDataReader = cmd1.ExecuteReader
        If dr1.HasRows Then
            While dr1.Read
                If dr1("Validated") IsNot DBNull.Value Then
                    If dr1("Validated") <> 0 Then
                        txtValidated.Text = "Yes"
                    Else
                        txtValidated.Text = "No"
                    End If
                Else
                    txtValidated.Text = "No"
                End If
            End While
        End If
        cn1.Close()
        cn1 = Nothing
        Dim cn2 As New SqlClient.SqlConnection(connString)
        cn2.Open()
        Dim cmd2 As New SqlClient.SqlCommand("Select Distinct " &
        "Test_ID from QC_Results where Run_ID = " & RunID, cn2)
        cmd2.CommandType = CommandType.Text
        Dim dr2 As SqlClient.SqlDataReader = cmd2.ExecuteReader
        If dr2.HasRows Then
            While dr2.Read
                i += 1
            End While
        End If
        cn2.Close()
        cn2 = Nothing
        UpdateInRangePercent(i)
        Dim cn3 As New SqlClient.SqlConnection(connString)
        cn3.Open()
        Dim cmd3 As New SqlClient.SqlCommand("Select " &
        "InRangePercent from Anas where ID in (Select " &
        "Analysis_ID from Runs where ID = " & RunID & ")", cn3)
        cmd3.CommandType = CommandType.Text
        Dim dr3 As SqlClient.SqlDataReader = cmd3.ExecuteReader
        If dr3.HasRows Then
            While dr3.Read
                For i = 0 To cmbOverride.Items.Count - 1
                    If i > 0 And i < cmbOverride.Items.Count - 1 Then   'between
                        If dr3("InRangePercent") > cmbOverride.Items(i - 1) And
                        dr3("InRangePercent") < cmbOverride.Items(i + 1) Then
                            cmbOverride.SelectedIndex = i
                            Exit For
                        End If
                    Else    'Lbound or UBound
                        If dr3("InRangePercent") = cmbOverride.Items(i) Then
                            cmbOverride.SelectedIndex = i
                            Exit For
                        End If
                    End If
                Next
            End While
        End If
        cn3.Close()
        cn3 = Nothing
    End Sub

    Private Sub UpdateInRangePercent(ByVal TestCount As Integer)
        cmbOverride.Items.Clear()
        Dim i As Integer
        If TestCount = 1 Then
            cmbOverride.Items.Add("0")
            cmbOverride.Items.Add("100")
        Else
            For i = 0 To TestCount
                cmbOverride.Items.Add(CInt(i / TestCount * 100))
            Next
        End If
    End Sub

    Private Function GetAccessionDate(ByVal AccID As Long) As Date
        Dim AccDate As Date = Date.Now
        Dim cn2 As New SqlClient.SqlConnection(connString)
        cn2.Open()
        Dim cmd2 As New SqlClient.SqlCommand("Select AccessionDate " &
        "from Requisitions where ID = " & AccID, cn2)
        cmd2.CommandType = CommandType.Text
        Dim dr2 As SqlClient.SqlDataReader = cmd2.ExecuteReader
        If dr2.HasRows Then
            While dr2.Read
                AccDate = dr2("AccessionDate")
            End While
        End If
        cn2.Close()
        cn2 = Nothing
        Return AccDate
    End Function

    Private Function GetFirstAccID(ByVal AccDate As Date, ByVal WID As Integer) As Long
        Dim AccID As Long = -1
        Dim sSQL As String = ""
        If WID = -1 Then
            sSQL = "Select Min(ID) as AccID from Requisitions where AccessionDate " &
            "between '" & Format(AccDate, SystemConfig.DateFormat) &
            "' and '" & Format(AccDate, SystemConfig.DateFormat) & " 23:59:00'"
        Else
            sSQL = "Select Min(ID) as AccID from Requisitions where AccessionDate between '" &
            Format(AccDate, SystemConfig.DateFormat) & "' and '" & Format(AccDate,
            SystemConfig.DateFormat) & " 23:59:00' and ID in (Select distinct Accession_ID " &
            "from Acc_Results where Test_ID in (Select Test_ID from Worksheet_Test where " &
            "Worksheet_ID = " & WID & "))"
        End If
        Dim cna1 As New SqlClient.SqlConnection(connString)
        cna1.Open()
        Dim cmda1 As New SqlClient.SqlCommand("", cna1)
        cmda1.CommandType = CommandType.Text
        Dim dra1 As SqlClient.SqlDataReader = cmda1.ExecuteReader
        If dra1.HasRows Then
            While dra1.Read
                If dra1("AccID") IsNot DBNull.Value Then AccID = Val(dra1("AccID"))
            End While
        End If
        cna1.Close()
        cna1 = Nothing
        Return AccID
    End Function

    Private Function GetLastAccID(ByVal AccDate As Date, ByVal WID As Integer) As Long
        Dim AccID As Long = -1
        Dim sSQL As String = ""
        If WID = -1 Then
            sSQL = "Select Max(ID) as AccID from Requisitions where AccessionDate " &
            "between '" & Format(AccDate, SystemConfig.DateFormat) &
            "' and '" & Format(AccDate, SystemConfig.DateFormat) & " 23:59:00'"
        Else
            sSQL = "Select Max(ID) as AccID from Requisitions where AccessionDate between '" &
            Format(AccDate, SystemConfig.DateFormat) & "' and '" & Format(AccDate,
            SystemConfig.DateFormat) & " 23:59:00' and ID in (Select distinct Accession_ID " &
            "from Acc_Results where Test_ID in (Select Test_ID from Worksheet_Test where " &
            "Worksheet_ID = " & WID & "))"
        End If
        Dim cna1 As New SqlClient.SqlConnection(connString)
        cna1.Open()
        Dim cmda1 As New SqlClient.SqlCommand("", cna1)
        cmda1.CommandType = CommandType.Text
        Dim dra1 As SqlClient.SqlDataReader = cmda1.ExecuteReader
        If dra1.HasRows Then
            While dra1.Read
                If dra1("AccID") IsNot DBNull.Value Then AccID = Val(dra1("AccID"))
            End While
        End If
        cna1.Close()
        cna1 = Nothing
        Return AccID
    End Function

    Private Function GetNextAccID(ByVal AccID As String, ByVal WID As String, ByVal Incomplete As Boolean, ByVal AccFromDate As Date, ByVal AccToDate As Date) As String
        Dim NewAccID As String = ""
        Dim sSQL As String = ""
        '
        If AccID <> "" AndAlso IsNumeric(AccID) Then
            If WID = "" Then
                If Incomplete = False Then  'All
                    sSQL = "Select ID as AccID from Requisitions where " &
                    "Received <> 0 and ID > " & Val(AccID) & " order by ID"
                Else    'Incomplete
                    sSQL = "Select ID as AccID from Requisitions where Received <> 0 and " &
                    "IsDate(Reported_Final) = 0 and ID > " & Val(AccID) & " order by ID"
                End If
            Else    'Worksheet filter
                If Incomplete = False Then  'All
                    sSQL = "Select ID as AccID from Requisitions where Received <> 0 and ID in (Select " &
                    "a.Accession_ID from Acc_Results a inner join Worksheet_Test b on a.Test_ID = b.Test_ID " &
                    "where b.Worksheet_ID = " & Val(WID) & ") and ID > " & Val(AccID) & " order by ID"
                Else    'Incomplete
                    sSQL = "Select ID as AccID from Requisitions where Received <> 0 and IsDate(Reported_Final) = 0 " &
                    "and ID in (Select a.Accession_ID from Acc_Results a inner join Worksheet_Test b on a.Test_ID = " &
                    "b.Test_ID where b.Worksheet_ID = " & Val(WID) & ") and ID > " & Val(AccID) & " order by ID"
                End If
            End If
        Else
            If WID = "" Then
                If Incomplete = False Then  'All
                    sSQL = $"Select min(ID) as AccID from Requisitions where Received <> 0 and AccessionDate between '{AccFromDate}' and '{AccToDate}'"
                Else    'Incomplete
                    sSQL = $"Select min(ID) as AccID from Requisitions where Received <> 0 and IsDate(Reported_Final) = 0 and AccessionDate 
                             between '{AccFromDate}' and '{AccToDate}'"
                End If
            Else
                If Incomplete = False Then  'All
                    'sSQL = "Select min(ID) as AccID from Requisitions where Received <> 0 and AccessionDate between '" &
                    'Format(AccFromDate, SystemConfig.DateFormat) & "' and '" & Format(AccFromDate, SystemConfig.DateFormat) &
                    '" 23:59:00' and ID in (Select distinct Accession_ID from Acc_Results where Test_ID in " &
                    '"(Select Test_ID from Worksheet_Test where Worksheet_ID = " & Val(WID) & "))"

                    sSQL = $"Select min(ID) as AccID from Requisitions where Received <> 0 and AccessionDate between '{AccFromDate}' and '{AccToDate}'
                            and ID in (Select distinct Accession_ID from Acc_Results where Test_ID in 
                            (Select Test_ID from Worksheet_Test where Worksheet_ID = " & Val(WID) & "))"

                Else    'Incomplete
                    'sSQL = "Select min(ID) as AccID from Requisitions where Received <> 0 and IsDate(Reported_Final) = 0 " &
                    '"and AccessionDate between '" & Format(AccFromDate, SystemConfig.DateFormat) & "' and '" & Format(AccFromDate,
                    'SystemConfig.DateFormat) & " 23:59:00' and ID in (Select distinct Accession_ID from Acc_Results where " &
                    '"Test_ID in (Select Test_ID from Worksheet_Test where Worksheet_ID = " & Val(WID) & "))"

                    sSQL = $"Select min(ID) as AccID from Requisitions where Received <> 0 and IsDate(Reported_Final) = 0  
                             and AccessionDate between '{AccFromDate}' and '{AccToDate}' and ID in (Select distinct Accession_ID from Acc_Results where  
                             Test_ID in (Select Test_ID from Worksheet_Test where Worksheet_ID = " & Val(WID) & "))"
                End If
            End If
        End If
        Dim cnnaid As New SqlClient.SqlConnection(connString)
        cnnaid.Open()
        Dim cmdnaid As New SqlClient.SqlCommand(sSQL, cnnaid)
        cmdnaid.CommandType = CommandType.Text
        Dim drnaid As SqlClient.SqlDataReader = cmdnaid.ExecuteReader
        If drnaid.HasRows Then
            While drnaid.Read
                NewAccID = drnaid("AccID").ToString
                Exit While
            End While
        Else
            NewAccID = ""
        End If
        cnnaid.Close()
        cnnaid = Nothing
        Return NewAccID
    End Function

    Private Sub UpdateCtlReleaseStatus(ByVal RunID As Long, ByVal ControlID As Long)
        'Try
        If dgvResults.RowCount > 0 Then
            Dim i As Integer
            Dim Released As Integer = 0
            For i = 0 To dgvResults.RowCount - 1
                If dgvResults.Rows(i).Cells(10).Value IsNot Nothing _
                AndAlso dgvResults.Rows(i).Cells(10).Value = True Then
                    Released += 1
                End If
            Next
            txtRelStatus.Text = CStr(Released) & " of " & CStr(dgvResults.RowCount)
            '
            Dim cnrt As New SqlClient.SqlConnection(connString)
            cnrt.Open()
            Dim cmdrt As New SqlClient.SqlCommand("Select " &
            "Max(Release_Time) as RelTime from QC_Results where Run_ID = " _
            & RunID & " and Control_ID = " & ControlID, cnrt)
            cmdrt.CommandType = CommandType.Text
            Dim drrt As SqlClient.SqlDataReader = cmdrt.ExecuteReader
            If drrt.HasRows Then
                While drrt.Read
                    If drrt("RelTime") IsNot DBNull.Value Then
                        txtEditedOn.Text = drrt("RelTime")
                    Else
                        txtEditedOn.Text = ""
                    End If
                End While
            End If
            cnrt.Close()
            cnrt = Nothing
            '
            Dim cn1 As New SqlClient.SqlConnection(connString)
            cn1.Open()
            Dim cmd1 As New SqlClient.SqlCommand("Select Released_By as " &
            "RelBy from QC_Results where Release_Time in (Select Max(Release_Time) " &
            "from QC_Results where Run_ID = " & RunID & " and " &
            "Control_ID = " & ControlID & ")", cn1)
            cmd1.CommandType = CommandType.Text
            Dim dr1 As SqlClient.SqlDataReader = cmd1.ExecuteReader
            If dr1.HasRows Then
                While dr1.Read
                    If dr1("RelBy") IsNot DBNull.Value Then
                        txtEditedBy.Text = GetUserName(dr1("RelBy"))
                    Else
                        txtEditedBy.Text = ""
                    End If
                End While
            Else
                txtEditedBy.Text = ""
            End If
            cn1.Close()
            cn1 = Nothing
        End If
        'Catch Ex As Exception
        '   MsgBox(Ex)
        'End Try
    End Sub

    Private Sub UpdateAccReleaseStatus(ByVal AccID As Long)
        'Try
        If dgvResults.RowCount > 0 Then
            Dim i As Integer
            Dim Hval As Date = Nothing
            Dim Released As Integer = 0
            For i = 0 To dgvResults.RowCount - 1
                If Not dgvResults.Rows(i).Cells(10).Value Is System.DBNull.Value AndAlso
                dgvResults.Rows(i).Cells(10).Value = True Then Released = Released + 1
            Next
            txtRelStatus.Text = CStr(Released) & " of " & CStr(dgvResults.RowCount)
            Dim sSQL As String = "Select Max(Release_Time) as RelTime from Acc_Results where Accession_ID = " & AccID &
            " Union Select Max(Release_Time) as RelTime from Ref_Results where Accession_ID = " & AccID &
            " Union Select Max(Release_Time) as RelTime from Acc_Info_Results where Accession_ID = " & AccID
            Dim cnars As New SqlClient.SqlConnection(connString)
            cnars.Open()
            Dim cmdars As New SqlClient.SqlCommand(sSQL, cnars)
            cmdars.CommandType = CommandType.Text
            Dim drars As SqlClient.SqlDataReader = cmdars.ExecuteReader
            If drars.HasRows Then
                While drars.Read
                    If drars("RelTime") IsNot DBNull.Value Then
                        If Hval = Nothing OrElse Hval < drars("RelTime") Then
                            Hval = drars("RelTime")
                            Exit While
                        End If
                    End If
                End While
            End If
            cnars.Close()
            cnars = Nothing
            '
            If Hval <> Nothing Then
                txtEditedOn.Text = Format(Hval, SystemConfig.DateFormat & " HH:mm")
            Else
                txtEditedOn.Text = ""
            End If
            '
            If Hval <> Nothing AndAlso IsDate(Hval) Then
                sSQL = "Select Released_By as RelBy from Acc_Results where Release_Time = '" &
                Format(Hval, "MM/dd/yyyy HH:mm") & "' Union Select Released_By as RelBy " &
                "from Ref_Results where Release_Time = '" & Format(Hval, "MM/dd/yyyy HH:mm") &
                "' Union Select Released_By as RelBy from Acc_Info_Results where Release_Time " &
                "= '" & Format(Hval, "MM/dd/yyyy HH:mm") & "'"
                Dim cnrlr As New SqlClient.SqlConnection(connString)
                cnrlr.Open()
                Dim cmdrlr As New SqlClient.SqlCommand(sSQL, cnrlr)
                cmdrlr.CommandType = CommandType.Text
                Dim drrlr As SqlClient.SqlDataReader = cmdrlr.ExecuteReader
                If drrlr.HasRows Then
                    While drrlr.Read
                        If drrlr("RelBy") IsNot DBNull.Value Then _
                         txtEditedBy.Text = Trim(GetUserName(drrlr("RelBy")))
                    End While
                End If
                cnrlr.Close()
                cnrlr = Nothing
            End If
        End If
    End Sub

    Private Sub PopulateOverride(ByVal Analytes As Integer)
        cmbOverride.Items.Clear()
        Dim i As Integer
        For i = 0 To Analytes
            cmbOverride.Items.Add(CInt(i / Analytes * 100))
        Next
    End Sub

    Private Sub UpdateRunSection(ByVal AccID As Long)
        cmbRun.Items.Clear()
        Dim sSQL As String = "Select * from Runs where ID in (Select Run_ID " &
        "from Acc_Results where Accession_ID = " & AccID & ")"
        Dim cnurs As New SqlClient.SqlConnection(connString)
        cnurs.Open()
        Dim cmdurs As New SqlClient.SqlCommand(sSQL, cnurs)
        cmdurs.CommandType = CommandType.Text
        Dim drurs As SqlClient.SqlDataReader = cmdurs.ExecuteReader
        If drurs.HasRows Then
            While drurs.Read
                cmbRun.Items.Add(New MyList(drurs("Name"), drurs("ID")))
            End While
        End If
        cnurs.Close()
        cnurs = Nothing
    End Sub

    Private Function GetCause(ByVal AccID As Long, ByVal TrigID As Integer) As String()
        Dim Cause() As String = {"ACC", "-1", "-1"}
        Dim sSQL As String = "Select * from Ref_Results where Accession_ID = " & AccID & " and Reflexer_ID = " & TrigID
        Dim cngc As New SqlClient.SqlConnection(connString)
        cngc.Open()
        Dim cmdgc As New SqlClient.SqlCommand(sSQL, cngc)
        cmdgc.CommandType = CommandType.Text
        Dim drgc As SqlClient.SqlDataReader = cmdgc.ExecuteReader
        If drgc.HasRows Then
            While drgc.Read
                Cause(0) = "REF" : Cause(1) = TrigID.ToString : Cause(2) = drgc("Reflexed_ID").ToString
            End While
        End If
        cngc.Close()
        cngc = Nothing
        Return Cause
    End Function

    Private Sub CreateResultGridRow(ByVal RowInfo() As String)
        Dim NormalStyle As New DataGridViewCellStyle
        Dim CorrectedStyle As New DataGridViewCellStyle
        CorrectedStyle.ForeColor = Color.Blue
        CorrectedStyle.BackColor = Color.GreenYellow
        Dim RPTComp As Boolean = ReportFullResulted(RowInfo(12))
        'Result
        If RowInfo(2) Is System.DBNull.Value OrElse RowInfo(2) Is Nothing Then RowInfo(2) = ""
        'Flag
        If RowInfo(4) Is System.DBNull.Value OrElse RowInfo(4) Is Nothing Then RowInfo(4) = ""
        'Normal Range
        If RowInfo(5) Is System.DBNull.Value OrElse RowInfo(5) Is Nothing Then RowInfo(5) = ""
        'Note
        If RowInfo(16) Is System.DBNull.Value OrElse RowInfo(16) Is Nothing Then RowInfo(16) = ""
        'RTFNote
        If RowInfo(17) Is System.DBNull.Value OrElse RowInfo(17) Is Nothing Then RowInfo(17) = ""
        'Create row
        dgvResults.Rows.Add(RowInfo(0), RowInfo(1), RowInfo(2), RowInfo(3), RowInfo(4), RowInfo(5),
        RowInfo(6), RowInfo(7), RowInfo(8), RowInfo(9), IIf(RowInfo(10) = "0", False, True), RowInfo(11), RowInfo(12),
        RowInfo(13), RowInfo(14), RowInfo(15), RowInfo(16), RowInfo(17), RowInfo(18), RowInfo(19))
        dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).Style.BackColor = Color.White
        NormalStyle.ForeColor = dgvResults.Rows(dgvResults.RowCount - 1).Cells(0).Style.ForeColor
        NormalStyle.BackColor = dgvResults.Rows(dgvResults.RowCount - 1).Cells(0).Style.ForeColor
        '0=TID, 1=TName, 2=Result, 3=nothing, 4=Flag, 5=NormalRange, 6=nothing, 7=nothing, 8=nothing, 9=nothing, 
        '10=Release, 11=Qualitative, 12=AccID, 13=Cause0, 14=Cause1, 15=Cause2, 16=Cmnt, 17=RTF, 18=IMG, 19=Sig
        '
        If RPTComp = True AndAlso (ThisUser.Supervisor = True Or ThisUser.Director = True) _
        OrElse (ThisUser.Result_Entry = True AndAlso ThisUser.Result_Release = True) _
        OrElse dgvResults.Rows(dgvResults.RowCount - 1).Cells(19).Value = False Then
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).ReadOnly = False
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(6).ReadOnly = False
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).ReadOnly = False
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(9).ReadOnly = False
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(10).ReadOnly = False
        Else
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).ReadOnly = True
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(6).ReadOnly = True
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).ReadOnly = True
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(9).ReadOnly = True
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(10).ReadOnly = True
        End If
        ' *** Decor ***
        If DeltaCheck(dgvResults.Rows(dgvResults.RowCount - 1).Cells(12).Value,
        dgvResults.Rows(dgvResults.RowCount - 1).Cells(0).Value,
        dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).Value) = False Then
            If Not dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Value.ToString.StartsWith(ChrW(8710)) _
            Then dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Value = ChrW(8710) & " " &
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Value
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(4).Style = PanicStyle
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Style.ForeColor = Color.Red
            If InStr(dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value,
            ChrW(8710) & " Delta Check failed" & vbCrLf) = 0 Then
                dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value =
                           ChrW(8710) & " Delta Check failed" & vbCrLf &
                           dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value
            End If

        Else
            If InStr(dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Value.ToString,
            ChrW(8710)) > 0 Then dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Value =
            Replace(dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Value, ChrW(8710) & " ", "")
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(4).Style.BackColor =
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(5).Style.BackColor
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Style.ForeColor =
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(5).Style.ForeColor
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value =
            Replace(dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value,
            ChrW(8710) & " Delta Check failed" & vbCrLf, "")
        End If
        ' *** End Delta Check ****
        If dgvResults.Rows(dgvResults.RowCount - 1).Cells(13).Value = "REF" Then
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(3).Value =
            System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Reflux.ico")
        ElseIf dgvResults.Rows(dgvResults.RowCount - 1).Cells(11).Value = True Then 'QL
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(3).Value =
            System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Looks.ico")
        End If
        '
        If dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value <> "" Then
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).Value =
            System.Drawing.Image.FromFile(Application.StartupPath &
            "\Images\Note.ico")


        Else
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).Value =
            System.Drawing.Image.FromFile(Application.StartupPath &
            "\Images\NoteBlank.ico")
        End If
        If dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value <> Nothing Then
            If dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value.ToString().Contains("Delta Check failed") Then
                Alert.Text = "∆ Delta Check failed"
            Else

            End If
        Else


        End If


        If dgvResults.Rows(dgvResults.RowCount - 1).Cells(17).Value <> "" Then
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(9).Value =
            System.Drawing.Image.FromFile(Application.StartupPath &
            "\Images\rtf.ico")
        Else
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(9).Value =
            System.Drawing.Image.FromFile(Application.StartupPath _
            & "\Images\rtfBlank.ico")
        End If
        ' *** Coloring Flag ************
        If RowInfo(6) = "Panic" Then
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(4).Style = PanicStyle
        ElseIf RowInfo(6) = "Caution" Then
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(4).Style = HLAStyle
        Else
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(4).Style = NormalStyle
        End If
        If ResultHistoryExists(dgvResults.Rows(dgvResults.RowCount - 1).Cells(12).Value,
        dgvResults.Rows(dgvResults.RowCount - 1).Cells(0).Value) Then
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Style = CorrectedStyle
        Else
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Style = NormalStyle
        End If
        ' *** Decor End *************
        TestCount.Text = dgvResults.RowCount()
    End Sub

    Private Sub CreateAccTestRow(ByVal AccID As Long, ByVal TestID As Integer)
        Dim RowInfo() As String = {"", "", "", Nothing, "", "", Nothing,
        Nothing, Nothing, Nothing, "", "", "", "", "", "", "", "", "", ""}
        Dim sSQL As String = "Select a.Test_ID, a.Result, a.T_Result, a.I_Result, a.Flag, " &
        "a.NormalRange, a.Released,  b.Name, b.Esig, b.Qualitative from Acc_Results a inner " &
        "join Tests b on b.ID = a.Test_ID where a.Accession_ID = " & AccID & " and a.Test_ID = " & TestID
        '
        Dim cnsql As New SqlClient.SqlConnection(connString)
        cnsql.Open()
        Dim selcmd As New SqlClient.SqlCommand(sSQL, cnsql)
        selcmd.CommandType = CommandType.Text
        Dim selDR As SqlClient.SqlDataReader = selcmd.ExecuteReader
        If selDR.HasRows Then
            While selDR.Read
                '0=TID, 1=TName, 2=Result, 3=nothing, 4=Flag, 5=NormalRange, 6=nothing, 7=nothing, 8=nothing, 9=nothing, 
                '10=Release, 11=Qualitative, 12=AccID, 13=Cause0, 14=Cause1, 15=Cause2, 16=Cmnt, 17=RTF, 18=IMG, 19=Sig
                RowInfo(0) = selDR("Test_ID").ToString
                RowInfo(1) = selDR("Name")
                If selDR("Result") IsNot DBNull.Value Then
                    RowInfo(2) = selDR("Result")
                Else
                    RowInfo(2) = ""
                End If
                If selDR("Flag") IsNot DBNull.Value Then
                    RowInfo(4) = selDR("Flag")
                Else
                    RowInfo(4) = ""
                End If
                If selDR("NormalRange") IsNot DBNull.Value Then
                    RowInfo(5) = selDR("NormalRange")
                Else
                    RowInfo(5) = ""
                End If
                If selDR("Released") Is DBNull.Value Then
                    RowInfo(10) = "0"
                Else
                    RowInfo(10) = CStr(CInt(selDR("Released")))
                End If
                RowInfo(11) = selDR("Qualitative").ToString
                RowInfo(12) = AccID
                RowInfo(13) = "ACC"
                If selDR("ESig") <> 0 Then
                    RowInfo(14) = "1"
                Else
                    RowInfo(14) = ""
                End If
                RowInfo(15) = ""
                If selDR("Comment") IsNot DBNull.Value Then
                    RowInfo(16) = selDR("Comment")
                Else
                    RowInfo(16) = ""
                End If
                If selDR("T_Result") IsNot DBNull.Value Then
                    RowInfo(17) = selDR("T_Result")
                Else
                    RowInfo(17) = ""
                End If
                If selDR("I_Result") IsNot DBNull.Value _
                AndAlso CType(selDR("I_Result"), Byte()).Length > 0 Then
                    RowInfo(18) = Convert.ToBase64String(selDR("I_Result"))
                Else
                    RowInfo(18) = Nothing
                End If
                RowInfo(19) = selDR("ESig")
                '
                If (RowInfo(0) IsNot Nothing AndAlso RowInfo(0) <> "") AndAlso
                (RowInfo(12) IsNot Nothing AndAlso RowInfo(12) <> "") Then _
                CreateResultGridRow(RowInfo)
                '
                If RowInfo(10) <> "0" Then
                    If ReleaserInfo(0, UBound(ReleaserInfo, 2)) IsNot Nothing _
                    AndAlso ReleaserInfo(0, UBound(ReleaserInfo, 2)) <> "" _
                    Then ReDim Preserve ReleaserInfo(UBound(ReleaserInfo, 1),
                    UBound(ReleaserInfo, 2) + 1)
                    ReleaserInfo(0, UBound(ReleaserInfo, 2)) = selDR("Test_ID").ToString
                    ReleaserInfo(1, UBound(ReleaserInfo, 2)) = selDR("Released_By").ToString
                    ReleaserInfo(2, UBound(ReleaserInfo, 2)) = selDR("Release_Time").ToString
                End If
            End While
        End If
        cnsql.Close()
        cnsql = Nothing
    End Sub

    Private Sub CreateReferTestRow(ByVal AccID As Long, ByVal ReflexerID As Integer, ByVal ReflexedID As Integer)
        Dim RowInfo() As String = {"", "", "", Nothing, "", "", Nothing,
        Nothing, Nothing, Nothing, "", "", "", "", "", "", "", "", "", ""}
        Dim sSQL As String = "Select a.Accession_ID, a.Reflexer_ID, Reflexed_ID, a.Test_ID, a.Result, a.Flag, " &
        "a.NormalRange, a.Comment, a.T_Result, a.I_Result, a.Released, a.Released_By, a.Release_Time, b.* from " &
        "Ref_Results a inner join Tests b on a.Test_ID = b.ID where b.IsActive <> 0 and b.HasResult <> 0 and " &
        "a.Accession_ID = " & AccID & " and a.Reflexer_ID = " & ReflexerID & " and a.Reflexed_ID = " & ReflexedID
        Dim cnsql As New SqlClient.SqlConnection(connString)
        cnsql.Open()
        Dim selcmd As New SqlClient.SqlCommand(sSQL, cnsql)
        selcmd.CommandType = CommandType.Text
        Dim selDR As SqlClient.SqlDataReader = selcmd.ExecuteReader
        If selDR.HasRows Then
            While selDR.Read
                '0=TID, 1=TName, 2=Result, 3=nothing, 4=Flag, 5=NormalRange, 6=nothing, 7=nothing, 8=nothing, 9=nothing, 
                '10=Release, 11=Qualitative, 12=AccID, 13=Cause0, 14=Cause1, 15=Cause2, 16=Cmnt, 17=RTF, 18=IMG, 19=Sig
                RowInfo(0) = selDR("Test_ID").ToString
                RowInfo(1) = selDR("Name")
                If selDR("Result") IsNot DBNull.Value Then
                    RowInfo(2) = selDR("Result")
                Else
                    RowInfo(2) = ""
                End If
                If selDR("Flag") IsNot DBNull.Value Then
                    RowInfo(4) = selDR("Flag")
                Else
                    RowInfo(4) = ""
                End If
                If selDR("NormalRange") IsNot DBNull.Value Then
                    RowInfo(5) = selDR("NormalRange")
                Else
                    RowInfo(5) = ""
                End If
                If selDR("Released") Is DBNull.Value Then
                    RowInfo(10) = "0"
                Else
                    RowInfo(10) = CStr(CInt(selDR("Released")))
                End If
                RowInfo(11) = selDR("Qualitative")
                RowInfo(12) = AccID
                RowInfo(13) = "REF"
                If selDR("ESig") <> 0 Then
                    RowInfo(14) = "1"
                Else
                    RowInfo(14) = ReflexerID
                End If
                RowInfo(15) = ReflexedID
                If selDR("Comment") IsNot DBNull.Value Then
                    RowInfo(16) = selDR("Comment")
                Else
                    RowInfo(16) = ""
                End If
                If selDR("T_Result") IsNot DBNull.Value Then
                    RowInfo(17) = selDR("T_Result")
                Else
                    RowInfo(17) = ""
                End If
                If selDR("I_Result") IsNot DBNull.Value _
                AndAlso CType(selDR("I_Result"), Byte()).Length > 0 Then
                    RowInfo(18) = Convert.ToBase64String(selDR("I_Result"))
                Else
                    RowInfo(18) = Nothing
                End If
                RowInfo(19) = selDR("ESig")
                '
                If (RowInfo(0) IsNot Nothing AndAlso RowInfo(0) <> "") AndAlso
                (RowInfo(12) IsNot Nothing AndAlso RowInfo(12) <> "") Then _
                CreateResultGridRow(RowInfo)
                '
                If RowInfo(10) <> "0" Then
                    If ReleaserInfo(0, UBound(ReleaserInfo, 2)) IsNot Nothing _
                    AndAlso ReleaserInfo(0, UBound(ReleaserInfo, 2)) <> "" _
                    Then ReDim Preserve ReleaserInfo(UBound(ReleaserInfo, 1),
                    UBound(ReleaserInfo, 2) + 1)
                    ReleaserInfo(0, UBound(ReleaserInfo, 2)) = selDR("Test_ID").ToString
                    ReleaserInfo(1, UBound(ReleaserInfo, 2)) = selDR("Released_By").ToString
                    ReleaserInfo(2, UBound(ReleaserInfo, 2)) = selDR("Release_Time").ToString
                End If
                ReDim RowInfo(19)
            End While
        End If
        cnsql.Close()
        cnsql = Nothing
    End Sub

    Private Sub CreateRef3TestRow(ByVal AccID As Long, ByVal ReflexerID As Integer)
        Dim RowInfo() As String = {"", "", "", Nothing, "", "", Nothing,
        Nothing, Nothing, Nothing, "", "", "", "", "", "", "", "", "", ""}
        Dim sSQL As String = "Select a.Accession_ID, a.Reflexer_ID, Reflexed_ID, a.Test_ID, a.Result, a.Flag, " &
        "a.NormalRange, a.Comment, a.T_Result, a.I_Result, a.Released,  a.Released_By, a.Release_Time, b.* from " &
        "Ref_Results a inner join Tests b on a.Test_ID = b.ID where b.IsActive <> 0 and b.HasResult <> 0 and " &
        "a.Accession_ID = " & AccID & " and a.Reflexer_ID <> " & ReflexerID & " and a.Reflexer_ID in (Select " &
        "Distinct Reflexed_ID from Ref_Results where Accession_ID = " & AccID & ")"
        Dim cnsql As New SqlClient.SqlConnection(connString)
        cnsql.Open()
        Dim selcmd As New SqlClient.SqlCommand(sSQL, cnsql)
        selcmd.CommandType = CommandType.Text
        Dim selDR As SqlClient.SqlDataReader = selcmd.ExecuteReader
        If selDR.HasRows Then
            While selDR.Read
                '0=TID, 1=TName, 2=Result, 3=nothing, 4=Flag, 5=NormalRange, 6=nothing, 7=nothing, 8=nothing, 9=nothing, 
                '10=Release, 11=Qualitative, 12=AccID, 13=Cause0, 14=Cause1, 15=Cause2, 16=Cmnt, 17=RTF, 18=IMG, 19=Sig
                RowInfo(0) = selDR("Test_ID").ToString
                RowInfo(1) = selDR("Name")
                If selDR("Result") IsNot DBNull.Value Then
                    RowInfo(2) = selDR("Result")
                Else
                    RowInfo(2) = ""
                End If
                If selDR("Flag") IsNot DBNull.Value Then
                    RowInfo(4) = selDR("Flag")
                Else
                    RowInfo(4) = ""
                End If
                If selDR("NormalRange") IsNot DBNull.Value Then
                    RowInfo(5) = selDR("NormalRange")
                Else
                    RowInfo(5) = ""
                End If
                If selDR("Released") Is DBNull.Value Then
                    RowInfo(10) = "0"
                Else
                    RowInfo(10) = CStr(CInt(selDR("Released")))
                End If
                RowInfo(11) = selDR("Qualitative")
                RowInfo(12) = AccID
                RowInfo(13) = "REF"
                If selDR("ESig") <> 0 Then
                    RowInfo(14) = "1"
                Else
                    RowInfo(14) = selDR("Reflexer_ID").ToString
                End If
                RowInfo(15) = selDR("Reflexed_ID").ToString
                If selDR("Comment") IsNot DBNull.Value Then
                    RowInfo(16) = selDR("Comment")
                Else
                    RowInfo(16) = ""
                End If
                If selDR("T_Result") IsNot DBNull.Value Then
                    RowInfo(17) = selDR("T_Result")
                Else
                    RowInfo(17) = ""
                End If
                If selDR("I_Result") IsNot DBNull.Value _
                AndAlso CType(selDR("I_Result"), Byte()).Length > 0 Then
                    RowInfo(18) = Convert.ToBase64String(selDR("I_Result"))
                Else
                    RowInfo(18) = Nothing
                End If
                RowInfo(19) = selDR("ESig")
                '
                If (RowInfo(0) IsNot Nothing AndAlso RowInfo(0) <> "") AndAlso
                (RowInfo(12) IsNot Nothing AndAlso RowInfo(12) <> "") Then _
                CreateResultGridRow(RowInfo)
                '
                If RowInfo(10) <> "0" Then
                    If ReleaserInfo(0, UBound(ReleaserInfo, 2)) IsNot Nothing _
                    AndAlso ReleaserInfo(0, UBound(ReleaserInfo, 2)) <> "" _
                    Then ReDim Preserve ReleaserInfo(UBound(ReleaserInfo, 1),
                    UBound(ReleaserInfo, 2) + 1)
                    ReleaserInfo(0, UBound(ReleaserInfo, 2)) = selDR("Test_ID").ToString
                    ReleaserInfo(1, UBound(ReleaserInfo, 2)) = selDR("Released_By").ToString
                    ReleaserInfo(2, UBound(ReleaserInfo, 2)) = selDR("Release_Time").ToString
                End If
                ReDim RowInfo(19)
            End While
        End If
        cnsql.Close()
        cnsql = Nothing
    End Sub

    Private Sub CreateInfoTestRow(ByVal AccID As Long, ByVal TestID As Integer, ByVal InfoID As Integer)
        Dim RowInfo() As String = {"", "", "", Nothing, "", "", Nothing,
        Nothing, Nothing, Nothing, "", "", "", "", "", "", "", "", "", ""}
        Dim sSQL As String = "Select a.Accession_ID, a.Test_ID, a.Info_ID, a.Result, a.Flag, a.NormalRange, " &
        "a.Comment, a.T_Result, a.I_Result, a.Released,  a.Released_By, a.Release_Time, b.* from Acc_Info_Results " &
        "a inner join Tests b on a.Info_ID = b.ID where b.IsActive <> 0 and b.HasResult <> 0 and b.PreAnalytical = 0 " &
        "and a.Accession_ID = " & AccID & " and a.Test_ID = " & TestID & " and a.Info_ID = " & InfoID
        Dim cnsql As New SqlClient.SqlConnection(connString)
        cnsql.Open()
        Dim selcmd As New SqlClient.SqlCommand(sSQL, cnsql)
        selcmd.CommandType = CommandType.Text
        Dim selDR As SqlClient.SqlDataReader = selcmd.ExecuteReader
        If selDR.HasRows Then
            While selDR.Read
                '0=TID, 1=TName, 2=Result, 3=nothing, 4=Flag, 5=NormalRange, 6=nothing, 7=nothing, 8=nothing, 9=nothing, 
                '10=Release, 11=Qualitative, 12=AccID, 13=Cause0, 14=Cause1, 15=Cause2, 16=Cmnt, 17=RTF, 18=IMG, 19=Sig
                RowInfo(0) = selDR("Info_ID").ToString
                RowInfo(1) = selDR("Name")
                If selDR("Result") IsNot DBNull.Value Then
                    RowInfo(2) = selDR("Result")
                Else
                    RowInfo(2) = ""
                End If
                If selDR("Flag") IsNot DBNull.Value Then
                    RowInfo(4) = selDR("Flag")
                Else
                    RowInfo(4) = ""
                End If
                If selDR("NormalRange") IsNot DBNull.Value Then
                    RowInfo(5) = selDR("NormalRange")
                Else
                    RowInfo(5) = ""
                End If
                If selDR("Released") Is DBNull.Value Then
                    RowInfo(10) = "0"
                Else
                    RowInfo(10) = CStr(CInt(selDR("Released")))
                End If
                RowInfo(11) = selDR("Qualitative")
                RowInfo(12) = AccID
                RowInfo(13) = "INFO"
                If selDR("ESig") <> 0 Then
                    RowInfo(14) = "1"
                Else
                    RowInfo(14) = selDR("Test_ID").ToString
                End If
                RowInfo(15) = selDR("Info_ID").ToString
                If selDR("Comment") IsNot DBNull.Value Then
                    RowInfo(16) = selDR("Comment")
                Else
                    RowInfo(16) = ""
                End If
                If selDR("T_Result") IsNot DBNull.Value Then
                    RowInfo(17) = selDR("T_Result")
                Else
                    RowInfo(17) = ""
                End If
                If selDR("I_Result") IsNot DBNull.Value _
                AndAlso CType(selDR("I_Result"), Byte()).Length > 0 Then
                    RowInfo(18) = Convert.ToBase64String(selDR("I_Result"))
                Else
                    RowInfo(18) = Nothing
                End If
                RowInfo(19) = selDR("ESig")
                '
                If (RowInfo(0) IsNot Nothing AndAlso RowInfo(0) <> "") AndAlso
                (RowInfo(12) IsNot Nothing AndAlso RowInfo(12) <> "") Then _
                CreateResultGridRow(RowInfo)
                '
                If RowInfo(10) <> "0" Then
                    If ReleaserInfo(0, UBound(ReleaserInfo, 2)) IsNot Nothing _
                    AndAlso ReleaserInfo(0, UBound(ReleaserInfo, 2)) <> "" _
                    Then ReDim Preserve ReleaserInfo(UBound(ReleaserInfo, 1),
                    UBound(ReleaserInfo, 2) + 1)
                    ReleaserInfo(0, UBound(ReleaserInfo, 2)) = selDR("Test_ID").ToString
                    ReleaserInfo(1, UBound(ReleaserInfo, 2)) = selDR("Released_By").ToString
                    ReleaserInfo(2, UBound(ReleaserInfo, 2)) = selDR("Release_Time").ToString
                End If
                ReDim RowInfo(19)
            End While
        End If
        cnsql.Close()
        cnsql = Nothing
    End Sub

    'Public Sub DisplayAccession(ByVal AccID As String, ByVal WID As String)
    '    'Try
    '    Dim stopWatch As New Stopwatch()
    '    stopWatch.Start()
    '    '''''''
    '    dgvResults.Rows.Clear()
    '    Dim o As Integer
    '    Dim d As Integer
    '    Dim i As Integer
    '    Dim Cause0 As String = ""   'Res Type
    '    Dim Cause1 As String = ""   'Reflexer
    '    Dim Cause2 As String = ""   'Reflexed
    '    '0=TID, 1=TName, 2=Result, 3=nothing, 4=Flag, 5=NormalRange, 6=nothing, 7=nothing, 8=nothing, 9=nothing, 
    '    '10=Release, 11=Qualitative, 12=AccID, 13=Cause0, 14=Cause1, 15=Cause2, 16=Cmnt, 17=RTF, 18=IMG, 19=Sig
    '    Dim RowInfo() As String = {"", "", "", Nothing, "", "", Nothing, _
    '    Nothing, Nothing, Nothing, "", "", "", "", "", "", "", "", "", ""}
    '    Dim RptComp As Boolean = ReportFullResulted(AccID)
    '    Dim Drawn As Date = GetDrawnDate(Val(AccID))
    '    If Drawn <> "#12:00:00#" Then _
    '    txtdrawn.Text = Drawn
    '    Dim Client() As String = GetClient(Val(AccID))
    '    txtClient.Text = Client(0)
    '    txtAttProv.Text = Client(1)
    '    txtPatient.Text = GetPatient(Val(AccID))
    '    txtMeds.Text = GetMedications(AccID)
    '    Dim Reported As String = GetReported(Val(AccID))
    '    If Reported <> "" Then
    '        txtRptDate.Text = Format(CDate(Reported), SystemConfig.DateFormat)
    '        txtRptTime.Text = Format(CDate(Reported), "HH:mm")
    '    Else
    '        txtRptDate.Text = ""
    '        txtRptTime.Text = ""
    '    End If
    '    '
    '    PopulateDirectors()
    '    cmbDirector.Enabled = True
    '    'dgvResults.Rows.Clear()
    '    My.Application.DoEvents()
    '    '
    '    Dim Rs As New ADODB.Recordset
    '    Dim Result As String = ""
    '    Dim NormalRange As String = ""
    '    Dim Flag As String = ""
    '    Dim Release As Boolean = False
    '    Dim Cmnt As String = ""
    '    Dim RTF As String = ""
    '    Dim IMG As Image
    '    If connString <> "" Then
    '        Dim cnn As New SqlClient.SqlConnection(connString)
    '        cnn.Open()
    '        Dim cmdacc As New SqlClient.SqlCommand("ReadAccResults_SP", cnn)
    '        cmdacc.CommandType = CommandType.StoredProcedure
    '        cmdacc.Parameters.AddWithValue("@AccID", AccID)
    '        cmdacc.Parameters.AddWithValue("@WID", WID)
    '        Dim DTacc As New DataTable
    '        Dim DAacc As New SqlClient.SqlDataAdapter(cmdacc)
    '        DAacc.Fill(DTacc)
    '        cnn.Close()
    '        cnn = Nothing
    '        For i = 0 To DTacc.Rows.Count - 1
    '            RowInfo(0) = DTacc.Rows(i).Item("Test_ID").ToString
    '            RowInfo(1) = DTacc.Rows(i).Item("Name")
    '            If DTacc.Rows(i).Item("Result") IsNot DBNull.Value Then
    '                RowInfo(2) = DTacc.Rows(i).Item("Result")
    '            Else
    '                RowInfo(2) = ""
    '            End If
    '            If DTacc.Rows(i).Item("Flag") IsNot DBNull.Value Then
    '                RowInfo(4) = DTacc.Rows(i).Item("Flag")
    '            Else
    '                RowInfo(4) = ""
    '            End If
    '            If DTacc.Rows(i).Item("NormalRange") IsNot DBNull.Value Then
    '                RowInfo(5) = DTacc.Rows(i).Item("NormalRange")
    '            Else
    '                RowInfo(5) = ""
    '            End If
    '            If DTacc.Rows(i).Item("Released") Is DBNull.Value Then
    '                RowInfo(10) = "0"
    '            Else
    '                RowInfo(10) = CStr(CInt(DTacc.Rows(i).Item("Released")))
    '            End If
    '            RowInfo(11) = DTacc.Rows(i).Item("Qualitative")
    '            RowInfo(12) = AccID
    '            RowInfo(13) = "ACC"
    '            If DTacc.Rows(i).Item("ESig") <> 0 Then
    '                RowInfo(14) = "1"
    '            Else
    '                RowInfo(14) = ""
    '            End If
    '            RowInfo(15) = ""
    '            If DTacc.Rows(i).Item("Comment") IsNot DBNull.Value Then
    '                RowInfo(16) = DTacc.Rows(i).Item("Comment")
    '            Else
    '                RowInfo(16) = ""
    '            End If
    '            If DTacc.Rows(i).Item("T_Result") IsNot DBNull.Value Then
    '                RowInfo(17) = DTacc.Rows(i).Item("T_Result")
    '            Else
    '                RowInfo(17) = ""
    '            End If

    '            If DTacc.Rows(i).Item("I_Result") IsNot DBNull.Value _
    '            AndAlso CType(DTacc.Rows(i).Item("I_Result"), Array).Length > 0 Then
    '                RowInfo(18) = Convert.ToBase64String(DTacc.Rows(i).Item("I_Result"))
    '            Else
    '                RowInfo(18) = Nothing
    '            End If
    '            RowInfo(19) = DTacc.Rows(i).Item("ESig")
    '            '
    '            If (RowInfo(0) IsNot Nothing AndAlso RowInfo(0) <> "") AndAlso _
    '            (RowInfo(12) IsNot Nothing AndAlso RowInfo(12) <> "") Then _
    '            CreateResultGridRow(RowInfo)
    '            '
    '            If RowInfo(10) <> "0" Then
    '                If ReleaserInfo(0, UBound(ReleaserInfo, 2)) IsNot Nothing _
    '                AndAlso ReleaserInfo(0, UBound(ReleaserInfo, 2)) <> "" _
    '                Then ReDim Preserve ReleaserInfo(UBound(ReleaserInfo, 1), _
    '                UBound(ReleaserInfo, 2) + 1)
    '                ReleaserInfo(0, UBound(ReleaserInfo, 2)) = DTacc.Rows(i).Item("Test_ID").ToString
    '                ReleaserInfo(1, UBound(ReleaserInfo, 2)) = DTacc.Rows(i).Item("Released_By").ToString
    '                ReleaserInfo(2, UBound(ReleaserInfo, 2)) = DTacc.Rows(i).Item("Release_Time").ToString
    '            End If
    '            ReDim RowInfo(19)
    '            '
    '            Dim AccReflexeds() As String = GetAccReflexedIDs(AccID, DTacc.Rows(i).Item("Test_ID"))
    '            For o = 0 To AccReflexeds.Length - 1
    '                If AccReflexeds(o) <> "" Then   'Organism or Reflexed1
    '                    Dim cnOrg As New SqlClient.SqlConnection(connString)
    '                    cnOrg.Open()
    '                    Dim cmdOrg As New SqlClient.SqlCommand("Select a.Accession_ID, a.Reflexer_ID, Reflexed_ID, a.Test_ID, a.Result, " & _
    '                    "a.Flag, a.NormalRange, a.Comment, a.T_Result, a.I_Result, a.Released, a.Released_By, a.Release_Time, b.Name, " & _
    '                    "b.Qualitative, b.eSig from Ref_Results a inner join Tests b on a.Test_ID = b.ID where b.IsActive <> 0 and " & _
    '                    "b.HasResult <> 0 and a.Accession_ID = " & AccID & " and a.Reflexer_ID = " & DTacc.Rows(i).Item("Test_ID") & _
    '                    " and a.Reflexed_ID = " & Val(AccReflexeds(o)), cnOrg)
    '                    cmdOrg.CommandType = CommandType.Text
    '                    Dim DROrg As SqlClient.SqlDataReader = cmdOrg.ExecuteReader
    '                    If DROrg.HasRows Then
    '                        While DROrg.Read
    '                            RowInfo(0) = DROrg("Test_ID").ToString
    '                            RowInfo(1) = DROrg("Name")
    '                            If DROrg("Result") IsNot DBNull.Value Then
    '                                RowInfo(2) = DROrg("Result")
    '                            Else
    '                                RowInfo(2) = ""
    '                            End If
    '                            If DROrg("Flag") IsNot DBNull.Value Then
    '                                RowInfo(4) = DROrg("Flag")
    '                            Else
    '                                RowInfo(4) = ""
    '                            End If
    '                            If DROrg("NormalRange") IsNot DBNull.Value Then
    '                                RowInfo(5) = DROrg("NormalRange")
    '                            Else
    '                                RowInfo(5) = ""
    '                            End If
    '                            If DROrg("Released") Is DBNull.Value Then
    '                                RowInfo(10) = "0"
    '                            Else
    '                                RowInfo(10) = CStr(CInt(DROrg("Released")))
    '                            End If
    '                            RowInfo(11) = DROrg("Qualitative")
    '                            RowInfo(12) = AccID
    '                            RowInfo(13) = "REF"
    '                            RowInfo(14) = DTacc.Rows(i).Item("Test_ID").ToString    'Reflexer
    '                            RowInfo(15) = DROrg("Reflexed_ID").ToString
    '                            If DROrg("Comment") IsNot DBNull.Value Then
    '                                RowInfo(16) = DROrg("Comment")
    '                            Else
    '                                RowInfo(16) = ""
    '                            End If
    '                            If DROrg("T_Result") IsNot DBNull.Value Then
    '                                RowInfo(17) = DROrg("T_Result")
    '                            Else
    '                                RowInfo(17) = ""
    '                            End If
    '                            If DROrg("I_Result") IsNot DBNull.Value _
    '                            AndAlso CType(DROrg("I_Result"), Byte()).Length > 0 Then
    '                                RowInfo(18) = Convert.ToBase64String(DROrg("I_Result"))
    '                            Else
    '                                RowInfo(18) = Nothing
    '                            End If
    '                            RowInfo(19) = DROrg("ESig")
    '                            '
    '                            If (RowInfo(0) IsNot Nothing AndAlso RowInfo(0) <> "") AndAlso _
    '                            (RowInfo(12) IsNot Nothing AndAlso RowInfo(12) <> "") Then _
    '                            CreateResultGridRow(RowInfo)
    '                            '
    '                            If CType(RowInfo(10), Boolean) = True Then
    '                                If ReleaserInfo(0, UBound(ReleaserInfo, 2)) IsNot Nothing _
    '                                AndAlso ReleaserInfo(0, UBound(ReleaserInfo, 2)) <> "" _
    '                                Then ReDim Preserve ReleaserInfo(UBound(ReleaserInfo, 1), _
    '                                UBound(ReleaserInfo, 2) + 1)
    '                                ReleaserInfo(0, UBound(ReleaserInfo, 2)) = DROrg("Test_ID").ToString
    '                                ReleaserInfo(1, UBound(ReleaserInfo, 2)) = DROrg("Released_By").ToString
    '                                ReleaserInfo(2, UBound(ReleaserInfo, 2)) = DROrg("Release_Time").ToString
    '                            End If
    '                            ReDim RowInfo(19)
    '                            ''''''''''''''''''''2nd level Reflex
    '                            Dim DRUGS() As String = GetAccReflexedIDs(AccID, Val(AccReflexeds(o)))
    '                            For d = 0 To DRUGS.Length - 1
    '                                If DRUGS(d) <> "" Then
    '                                    Dim cnDrg As New SqlClient.SqlConnection(connString)
    '                                    cnDrg.Open()
    '                                    Dim cmdDrg As New SqlClient.SqlCommand("Select a.Accession_ID, a.Reflexer_ID, Reflexed_ID, a.Test_ID, " & _
    '                                    "a.Result, a.Flag, a.NormalRange, a.Comment, a.T_Result, a.I_Result, a.Released, a.Released_By, " & _
    '                                    "a.Release_Time, b.Name, b.Qualitative, b.eSig from Ref_Results a inner join Tests b on a.Test_ID = b.ID " & _
    '                                    "where b.IsActive <> 0 and b.HasResult <> 0 and a.Accession_ID = " & AccID & " and a.Reflexer_ID = " & _
    '                                    Val(AccReflexeds(o)) & " and a.Reflexed_ID = " & Val(DRUGS(d)), cnDrg)
    '                                    cmdDrg.CommandType = CommandType.Text
    '                                    Dim DRDrg As SqlClient.SqlDataReader = cmdDrg.ExecuteReader
    '                                    If DRDrg.HasRows Then
    '                                        While DRDrg.Read
    '                                            RowInfo(0) = DRDrg("Test_ID").ToString
    '                                            RowInfo(1) = DRDrg("Name")
    '                                            If DRDrg("Result") IsNot DBNull.Value Then
    '                                                RowInfo(2) = DRDrg("Result")
    '                                            Else
    '                                                RowInfo(2) = ""
    '                                            End If
    '                                            If DRDrg("Flag") IsNot DBNull.Value Then
    '                                                RowInfo(4) = DRDrg("Flag")
    '                                            Else
    '                                                RowInfo(4) = ""
    '                                            End If
    '                                            If DRDrg("NormalRange") IsNot DBNull.Value Then
    '                                                RowInfo(5) = DRDrg("NormalRange")
    '                                            Else
    '                                                RowInfo(5) = ""
    '                                            End If
    '                                            If DRDrg("Released") Is DBNull.Value Then
    '                                                RowInfo(10) = "0"
    '                                            Else
    '                                                RowInfo(10) = CStr(CInt(DRDrg("Released")))
    '                                            End If
    '                                            RowInfo(11) = DRDrg("Qualitative")
    '                                            RowInfo(12) = AccID
    '                                            RowInfo(13) = "REF"
    '                                            RowInfo(14) = AccReflexeds(o)   'Reflexer
    '                                            RowInfo(15) = DRDrg("Reflexed_ID").ToString
    '                                            If DRDrg("Comment") IsNot DBNull.Value Then
    '                                                RowInfo(16) = DRDrg("Comment")
    '                                            Else
    '                                                RowInfo(16) = ""
    '                                            End If
    '                                            If DRDrg("T_Result") IsNot DBNull.Value Then
    '                                                RowInfo(17) = DRDrg("T_Result")
    '                                            Else
    '                                                RowInfo(17) = ""
    '                                            End If
    '                                            If DRDrg("I_Result") IsNot DBNull.Value _
    '                                            AndAlso CType(DRDrg("I_Result"), Byte()).Length > 0 Then
    '                                                RowInfo(18) = Convert.ToBase64String(DRDrg("I_Result"))
    '                                            Else
    '                                                RowInfo(18) = Nothing
    '                                            End If
    '                                            RowInfo(19) = DRDrg("ESig")
    '                                            '
    '                                            If (RowInfo(0) IsNot Nothing AndAlso RowInfo(0) <> "") AndAlso _
    '                                            (RowInfo(12) IsNot Nothing AndAlso RowInfo(12) <> "") Then _
    '                                            CreateResultGridRow(RowInfo)
    '                                            '
    '                                            If RowInfo(10) <> "0" Then
    '                                                If ReleaserInfo(0, UBound(ReleaserInfo, 2)) IsNot Nothing _
    '                                                AndAlso ReleaserInfo(0, UBound(ReleaserInfo, 2)) <> "" _
    '                                                Then ReDim Preserve ReleaserInfo(UBound(ReleaserInfo, 1), _
    '                                                UBound(ReleaserInfo, 2) + 1)
    '                                                ReleaserInfo(0, UBound(ReleaserInfo, 2)) = DRDrg("Test_ID").ToString
    '                                                ReleaserInfo(1, UBound(ReleaserInfo, 2)) = DRDrg("Released_By").ToString
    '                                                ReleaserInfo(2, UBound(ReleaserInfo, 2)) = DRDrg("Release_Time").ToString
    '                                            End If
    '                                            ReDim RowInfo(19)
    '                                        End While
    '                                    End If
    '                                    cnDrg.Close()
    '                                    cnDrg = Nothing
    '                                End If   'end of Drugs
    '                            Next d
    '                        End While
    '                    End If
    '                    cnOrg.Close()
    '                    cnOrg = Nothing
    '                End If
    '            Next o
    '            ' Fetch Info
    '            Dim Infos() As String = GetInfos(Val(AccID), DTacc.Rows(i).Item("Test_ID"))
    '            For f As Integer = 0 To Infos.Length - 1
    '                If Infos(f) <> "" Then   'Infos exist
    '                    Dim cnInf As New SqlClient.SqlConnection(connString)
    '                    cnInf.Open()
    '                    Dim cmdInf As New SqlClient.SqlCommand("Select a.Accession_ID, a.Test_ID, a.Info_ID, a.Result, a.Flag, " & _
    '                    "a.NormalRange, a.Comment, a.T_Result, a.I_Result, a.Released,  a.Released_By, a.Release_Time, b.Name, " & _
    '                    "b.Qualitative, b.eSig from Acc_Info_Results a inner join Tests b on a.Info_ID = b.ID where b.IsActive <> 0 " & _
    '                    "and b.HasResult <> 0 and b.PreAnalytical = 0 and a.Accession_ID = " & AccID & " and a.Test_ID = " & _
    '                    DTacc.Rows(i).Item("Test_ID") & " and a.Info_ID = " & Val(Infos(f)), cnInf)
    '                    cmdInf.CommandType = CommandType.Text
    '                    Dim DRInf As SqlClient.SqlDataReader = cmdInf.ExecuteReader
    '                    If DRInf.HasRows Then
    '                        While DRInf.Read
    '                            RowInfo(0) = DRInf("Info_ID").ToString
    '                            RowInfo(1) = DRInf("Name")
    '                            If DRInf("Result") IsNot DBNull.Value Then
    '                                RowInfo(2) = DRInf("Result")
    '                            Else
    '                                RowInfo(2) = ""
    '                            End If
    '                            If DRInf("Flag") IsNot DBNull.Value Then
    '                                RowInfo(4) = DRInf("Flag")
    '                            Else
    '                                RowInfo(4) = ""
    '                            End If
    '                            If DRInf("NormalRange") IsNot DBNull.Value Then
    '                                RowInfo(5) = DRInf("NormalRange")
    '                            Else
    '                                RowInfo(5) = ""
    '                            End If
    '                            If DRInf("Released") Is DBNull.Value Then
    '                                RowInfo(10) = "0"
    '                            Else
    '                                RowInfo(10) = CStr(CInt(DRInf("Released")))
    '                            End If
    '                            RowInfo(11) = DRInf("Qualitative")
    '                            RowInfo(12) = AccID
    '                            RowInfo(13) = "info"
    '                            RowInfo(14) = DTacc.Rows(i).Item("Test_ID")
    '                            RowInfo(15) = DRInf("Info_ID").ToString
    '                            If DRInf("Comment") IsNot DBNull.Value Then
    '                                RowInfo(16) = DRInf("Comment")
    '                            Else
    '                                RowInfo(16) = ""
    '                            End If
    '                            If DRInf("T_Result") IsNot DBNull.Value Then
    '                                RowInfo(17) = DRInf("T_Result")
    '                            Else
    '                                RowInfo(17) = ""
    '                            End If
    '                            If DRInf("I_Result") IsNot DBNull.Value _
    '                            AndAlso CType(DRInf("I_Result"), Byte()).Length > 0 Then
    '                                RowInfo(18) = Convert.ToBase64String(DRInf("I_Result"))
    '                            Else
    '                                RowInfo(18) = Nothing
    '                            End If
    '                            RowInfo(19) = DRInf("ESig")
    '                            '
    '                            If (RowInfo(0) IsNot Nothing AndAlso RowInfo(0) <> "") AndAlso _
    '                            (RowInfo(12) IsNot Nothing AndAlso RowInfo(12) <> "") Then _
    '                            CreateResultGridRow(RowInfo)
    '                            '
    '                            If RowInfo(10) <> "0" Then
    '                                If ReleaserInfo(0, UBound(ReleaserInfo, 2)) IsNot Nothing _
    '                                AndAlso ReleaserInfo(0, UBound(ReleaserInfo, 2)) <> "" _
    '                                Then ReDim Preserve ReleaserInfo(UBound(ReleaserInfo, 1), _
    '                                UBound(ReleaserInfo, 2) + 1)
    '                                ReleaserInfo(0, UBound(ReleaserInfo, 2)) = DRInf("Test_ID").ToString
    '                                ReleaserInfo(1, UBound(ReleaserInfo, 2)) = DRInf("Released_By").ToString
    '                                ReleaserInfo(2, UBound(ReleaserInfo, 2)) = DRInf("Release_Time").ToString
    '                            End If
    '                        End While
    '                    End If
    '                    cnInf.Close()
    '                    cnInf = Nothing
    '                End If
    '            Next f
    '            '  End of all rows related to the test id of AccResults
    '        Next i

    '    Else    'ADODB
    '        Dim sSQL As String = ""
    '        If WID <> "" Then   'WS filtered
    '            sSQL = "Select a.Accession_ID, a.Test_ID, a.Result, a.Flag, a.NormalRange, a.Comment, a.T_Result, a.I_Result, a.Released, " & _
    '            "a.Released_By, a.Release_Time, b.Name, b.Qualitative, b.eSig from Acc_Results a, Tests b, Worksheet_Test c, Requisitions " & _
    '            "d where a.Test_ID = b.ID and b.IsActive <> 0 and b.HasResult <> 0 and a.Accession_ID = d.ID and d.Received <> 0 and " & _
    '            "a.Accession_ID = " & Val(AccID) & " and a.Test_ID = c.Test_ID and c.Worksheet_ID = " & Val(WID) & " Order by c.Ordinal"
    '        Else
    '            sSQL = "Select a.Accession_ID, a.Test_ID, a.Result, a.Flag, a.NormalRange, a.Comment, a.T_Result, a.I_Result, " & _
    '            "a.Released, a.Released_By, a.Release_Time, b.Name, b.Qualitative, b.eSig from Acc_Results a, Tests b, Requisitions c " & _
    '            "where a.Test_ID = b.ID and a.Accession_ID = c.ID and c.Received <> 0 and b.IsActive <> 0 and b.HasResult <> 0 and " & _
    '            "a.Accession_ID = " & Val(AccID) & " Order by a.Ordinal"
    '        End If
    '        '

    '        Rs.Open(sSQL, CN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '        If Not Rs.BOF Then
    '            Rs.MoveLast()
    '            Rs.MoveFirst()
    '            ReDim ReleaserInfo(2, 0)
    '            'ReDim Extras(4, Rs.RecordCount - 1)
    '            Do Until Rs.EOF
    '                '0=TID, 1=TName, 2=Result, 3=nothing, 4=Flag, 5=NormalRange, 6=nothing, 7=nothing, 8=nothing, 9=nothing, 
    '                '10=Release, 11=Qualitative, 12=AccID, 13=Cause0, 14=Cause1, 15=Cause2, 16=Cmnt, 17=RTF, 18=IMG, 19=Sig
    '                RowInfo(0) = Rs.Fields("Test_ID").Value.ToString
    '                RowInfo(1) = Rs.Fields("Name").Value
    '                If Rs.Fields("Result").Value IsNot DBNull.Value Then
    '                    RowInfo(2) = Rs.Fields("Result").Value
    '                Else
    '                    RowInfo(2) = ""
    '                End If
    '                If Rs.Fields("Flag").Value IsNot DBNull.Value Then
    '                    RowInfo(4) = Rs.Fields("Flag").Value
    '                Else
    '                    RowInfo(4) = ""
    '                End If
    '                If Rs.Fields("NormalRange").Value IsNot DBNull.Value Then
    '                    RowInfo(5) = Rs.Fields("NormalRange").Value
    '                Else
    '                    RowInfo(5) = ""
    '                End If
    '                If Rs.Fields("Released").Value Is DBNull.Value Then
    '                    RowInfo(10) = "0"
    '                Else
    '                    RowInfo(10) = CStr(CInt(Rs.Fields("Released").Value))
    '                End If
    '                RowInfo(11) = Rs.Fields("Qualitative").Value
    '                RowInfo(12) = AccID
    '                RowInfo(13) = "ACC"
    '                If Rs.Fields("ESig").Value <> 0 Then
    '                    RowInfo(14) = "1"
    '                Else
    '                    RowInfo(14) = ""
    '                End If
    '                RowInfo(15) = ""
    '                If Rs.Fields("Comment").Value IsNot DBNull.Value Then
    '                    RowInfo(16) = Rs.Fields("Comment").Value
    '                Else
    '                    RowInfo(16) = ""
    '                End If
    '                If Rs.Fields("T_Result").Value IsNot DBNull.Value Then
    '                    RowInfo(17) = Rs.Fields("T_Result").Value
    '                Else
    '                    RowInfo(17) = ""
    '                End If
    '                If Rs.Fields("I_Result").Value IsNot DBNull.Value _
    '                AndAlso CType(Rs.Fields("I_Result").Value, Byte()).Length > 0 Then
    '                    RowInfo(18) = Convert.ToBase64String(Rs.Fields("I_Result").Value)
    '                Else
    '                    RowInfo(18) = Nothing
    '                End If
    '                RowInfo(19) = Rs.Fields("ESig").Value
    '                '
    '                If (RowInfo(0) IsNot Nothing AndAlso RowInfo(0) <> "") AndAlso _
    '                (RowInfo(12) IsNot Nothing AndAlso RowInfo(12) <> "") Then _
    '                CreateResultGridRow(RowInfo)
    '                '
    '                If RowInfo(10) <> "0" Then
    '                    If ReleaserInfo(0, UBound(ReleaserInfo, 2)) IsNot Nothing _
    '                    AndAlso ReleaserInfo(0, UBound(ReleaserInfo, 2)) <> "" _
    '                    Then ReDim Preserve ReleaserInfo(UBound(ReleaserInfo, 1), _
    '                    UBound(ReleaserInfo, 2) + 1)
    '                    ReleaserInfo(0, UBound(ReleaserInfo, 2)) = Rs.Fields("Test_ID").Value.ToString
    '                    ReleaserInfo(1, UBound(ReleaserInfo, 2)) = Rs.Fields("Released_By").Value.ToString
    '                    ReleaserInfo(2, UBound(ReleaserInfo, 2)) = Rs.Fields("Release_Time").Value.ToString
    '                End If
    '                ReDim RowInfo(19)
    '                ' Fetch the first level reflexes 
    '                Dim AccReflexeds() As String = GetAccReflexedIDs(AccID, Rs.Fields("Test_ID").Value)
    '                If AccReflexeds(0) <> "" Then   'Organism or Reflexed1
    '                    For o = 0 To AccReflexeds.Length - 1
    '                        Dim RsA As New ADODB.Recordset
    '                        RsA.Open("Select a.Accession_ID, a.Reflexer_ID, Reflexed_ID, a.Test_ID, a.Result, a.Flag, a.NormalRange, " & _
    '                        "a.Comment, a.T_Result, a.I_Result, a.Released, a.Released_By, a.Release_Time, b.* from Ref_Results a " & _
    '                        "inner join Tests b on a.Test_ID = b.ID where b.IsActive <> 0 and b.HasResult <> 0 and a.Accession_ID = " & _
    '                        AccID & " and a.Reflexer_ID = " & Rs.Fields("Test_ID").Value & " and a.Reflexed_ID = " & Val(AccReflexeds(o)), _
    '                        CN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '                        If Not RsA.BOF Then
    '                            Do Until RsA.EOF
    '                                RowInfo(0) = RsA.Fields("Test_ID").Value.ToString
    '                                RowInfo(1) = RsA.Fields("Name").Value
    '                                If RsA.Fields("Result").Value IsNot DBNull.Value Then
    '                                    RowInfo(2) = RsA.Fields("Result").Value
    '                                Else
    '                                    RowInfo(2) = ""
    '                                End If
    '                                If RsA.Fields("Flag").Value IsNot DBNull.Value Then
    '                                    RowInfo(4) = RsA.Fields("Flag").Value
    '                                Else
    '                                    RowInfo(4) = ""
    '                                End If
    '                                If RsA.Fields("NormalRange").Value IsNot DBNull.Value Then
    '                                    RowInfo(5) = RsA.Fields("NormalRange").Value
    '                                Else
    '                                    RowInfo(5) = ""
    '                                End If
    '                                If RsA.Fields("Released").Value Is DBNull.Value Then
    '                                    RowInfo(10) = "0"
    '                                Else
    '                                    RowInfo(10) = CStr(CInt(RsA.Fields("Released").Value))
    '                                End If
    '                                RowInfo(11) = RsA.Fields("Qualitative").Value
    '                                RowInfo(12) = AccID
    '                                RowInfo(13) = "REF"
    '                                RowInfo(14) = Rs.Fields("Test_ID").Value    'Reflexer
    '                                RowInfo(15) = RsA.Fields("Reflexed_ID").Value.ToString
    '                                If RsA.Fields("Comment").Value IsNot DBNull.Value Then
    '                                    RowInfo(16) = RsA.Fields("Comment").Value
    '                                Else
    '                                    RowInfo(16) = ""
    '                                End If
    '                                If RsA.Fields("T_Result").Value IsNot DBNull.Value Then
    '                                    RowInfo(17) = RsA.Fields("T_Result").Value
    '                                Else
    '                                    RowInfo(17) = ""
    '                                End If
    '                                If RsA.Fields("I_Result").Value IsNot DBNull.Value _
    '                                AndAlso CType(RsA.Fields("I_Result").Value, Byte()).Length > 0 Then
    '                                    RowInfo(18) = Convert.ToBase64String(RsA.Fields("I_Result").Value)
    '                                Else
    '                                    RowInfo(18) = Nothing
    '                                End If
    '                                RowInfo(19) = RsA.Fields("ESig").Value
    '                                '
    '                                If (RowInfo(0) IsNot Nothing AndAlso RowInfo(0) <> "") AndAlso _
    '                                (RowInfo(12) IsNot Nothing AndAlso RowInfo(12) <> "") Then _
    '                                CreateResultGridRow(RowInfo)
    '                                '
    '                                If CType(RowInfo(10), Boolean) = True Then
    '                                    If ReleaserInfo(0, UBound(ReleaserInfo, 2)) IsNot Nothing _
    '                                    AndAlso ReleaserInfo(0, UBound(ReleaserInfo, 2)) <> "" _
    '                                    Then ReDim Preserve ReleaserInfo(UBound(ReleaserInfo, 1), _
    '                                    UBound(ReleaserInfo, 2) + 1)
    '                                    ReleaserInfo(0, UBound(ReleaserInfo, 2)) = RsA.Fields("Test_ID").Value.ToString
    '                                    ReleaserInfo(1, UBound(ReleaserInfo, 2)) = RsA.Fields("Released_By").Value.ToString
    '                                    ReleaserInfo(2, UBound(ReleaserInfo, 2)) = RsA.Fields("Release_Time").Value.ToString
    '                                End If
    '                                ReDim RowInfo(19)
    '                                ''''''''''''''''''''2nd level Reflex
    '                                Dim DRUGS() As String = GetAccReflexedIDs(AccID, Val(AccReflexeds(o)))
    '                                If DRUGS(0) <> "" Then
    '                                    For d = 0 To DRUGS.Length - 1
    '                                        Dim Rsd As New ADODB.Recordset
    '                                        Rsd.Open("Select a.Accession_ID, a.Reflexer_ID, Reflexed_ID, a.Test_ID, a.Result, a.Flag, " & _
    '                                        "a.NormalRange, a.Comment, a.T_Result, a.I_Result, a.Released, a.Released_By, a.Release_Time, " & _
    '                                        "b.* from Ref_Results a inner join Tests b on a.Test_ID = b.ID where b.IsActive <> 0 and " & _
    '                                        "b.HasResult <> 0 and a.Accession_ID = " & AccID & " and a.Reflexer_ID = " & _
    '                                        Val(AccReflexeds(o)) & " and a.Reflexed_ID = " & Val(DRUGS(d)), _
    '                                        CN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '                                        If Not Rsd.BOF Then
    '                                            Do Until Rsd.EOF
    '                                                RowInfo(0) = Rsd.Fields("Test_ID").Value.ToString
    '                                                RowInfo(1) = Rsd.Fields("Name").Value
    '                                                If Rsd.Fields("Result").Value IsNot DBNull.Value Then
    '                                                    RowInfo(2) = Rsd.Fields("Result").Value
    '                                                Else
    '                                                    RowInfo(2) = ""
    '                                                End If
    '                                                If Rsd.Fields("Flag").Value IsNot DBNull.Value Then
    '                                                    RowInfo(4) = Rsd.Fields("Flag").Value
    '                                                Else
    '                                                    RowInfo(4) = ""
    '                                                End If
    '                                                If Rsd.Fields("NormalRange").Value IsNot DBNull.Value Then
    '                                                    RowInfo(5) = Rsd.Fields("NormalRange").Value
    '                                                Else
    '                                                    RowInfo(5) = ""
    '                                                End If
    '                                                If Rsd.Fields("Released").Value Is DBNull.Value Then
    '                                                    RowInfo(10) = "0"
    '                                                Else
    '                                                    RowInfo(10) = CStr(CInt(Rsd.Fields("Released").Value))
    '                                                End If
    '                                                RowInfo(11) = Rsd.Fields("Qualitative").Value
    '                                                RowInfo(12) = AccID
    '                                                RowInfo(13) = "REF"
    '                                                RowInfo(14) = AccReflexeds(o)   'Reflexer
    '                                                RowInfo(15) = Rsd.Fields("Reflexed_ID").Value.ToString
    '                                                If Rsd.Fields("Comment").Value IsNot DBNull.Value Then
    '                                                    RowInfo(16) = Rsd.Fields("Comment").Value
    '                                                Else
    '                                                    RowInfo(16) = ""
    '                                                End If
    '                                                If Rsd.Fields("T_Result").Value IsNot DBNull.Value Then
    '                                                    RowInfo(17) = Rsd.Fields("T_Result").Value
    '                                                Else
    '                                                    RowInfo(17) = ""
    '                                                End If
    '                                                If Rsd.Fields("I_Result").Value IsNot DBNull.Value _
    '                                                AndAlso CType(Rsd.Fields("I_Result").Value, Byte()).Length > 0 Then
    '                                                    RowInfo(18) = Convert.ToBase64String(Rsd.Fields("I_Result").Value)
    '                                                Else
    '                                                    RowInfo(18) = Nothing
    '                                                End If
    '                                                RowInfo(19) = Rsd.Fields("ESig").Value
    '                                                '
    '                                                If (RowInfo(0) IsNot Nothing AndAlso RowInfo(0) <> "") AndAlso _
    '                                                (RowInfo(12) IsNot Nothing AndAlso RowInfo(12) <> "") Then _
    '                                                CreateResultGridRow(RowInfo)
    '                                                '
    '                                                If RowInfo(10) <> "0" Then
    '                                                    If ReleaserInfo(0, UBound(ReleaserInfo, 2)) IsNot Nothing _
    '                                                    AndAlso ReleaserInfo(0, UBound(ReleaserInfo, 2)) <> "" _
    '                                                    Then ReDim Preserve ReleaserInfo(UBound(ReleaserInfo, 1), _
    '                                                    UBound(ReleaserInfo, 2) + 1)
    '                                                    ReleaserInfo(0, UBound(ReleaserInfo, 2)) = Rsd.Fields("Test_ID").Value.ToString
    '                                                    ReleaserInfo(1, UBound(ReleaserInfo, 2)) = Rsd.Fields("Released_By").Value.ToString
    '                                                    ReleaserInfo(2, UBound(ReleaserInfo, 2)) = Rsd.Fields("Release_Time").Value.ToString
    '                                                End If
    '                                                ReDim RowInfo(19)
    '                                                Rsd.MoveNext()
    '                                            Loop
    '                                        End If
    '                                        Rsd.Close()
    '                                        Rsd = Nothing
    '                                    Next d
    '                                    '    3rd level drugs display
    '                                    'Dim Rs2 As New ADODB.Recordset
    '                                    'Rs2.Open("Select a.Accession_ID, a.Reflexer_ID, Reflexed_ID, a.Test_ID, a.Result, a.Flag, a.NormalRange, " & _
    '                                    '"a.Comment, a.T_Result, a.I_Result, a.Released,  a.Released_By, a.Release_Time, b.* from Ref_Results a " & _
    '                                    '"inner join Tests b on a.Test_ID = b.ID where b.IsActive <> 0 and b.HasResult <> 0 and a.Accession_ID = " & _
    '                                    'AccID & " and a.Reflexer_ID <> " & Val(AccReflexeds(o)) & " and a.Reflexer_ID in (Select Distinct " & _
    '                                    '"Reflexed_ID from Ref_Results where Accession_ID = " & AccID & ")", CN, ADODB.CursorTypeEnum.adOpenStatic, _
    '                                    'ADODB.LockTypeEnum.adLockReadOnly)
    '                                    'If Not Rs2.BOF Then
    '                                    '    Do Until Rs2.EOF
    '                                    '        RowInfo(0) = Rs2.Fields("Test_ID").Value.ToString
    '                                    '        RowInfo(1) = Rs2.Fields("Name").Value
    '                                    '        If Rs2.Fields("Result").Value IsNot DBNull.Value Then
    '                                    '            RowInfo(2) = Rs2.Fields("Result").Value
    '                                    '        Else
    '                                    '            RowInfo(2) = ""
    '                                    '        End If
    '                                    '        If Rs2.Fields("Flag").Value IsNot DBNull.Value Then
    '                                    '            RowInfo(4) = Rs2.Fields("Flag").Value
    '                                    '        Else
    '                                    '            RowInfo(4) = ""
    '                                    '        End If
    '                                    '        If Rs2.Fields("NormalRange").Value IsNot DBNull.Value Then
    '                                    '            RowInfo(5) = Rs2.Fields("NormalRange").Value
    '                                    '        Else
    '                                    '            RowInfo(5) = ""
    '                                    '        End If
    '                                    '        If Rs2.Fields("Released").Value Is DBNull.Value Then
    '                                    '            RowInfo(10) = "0"
    '                                    '        Else
    '                                    '            RowInfo(10) = CStr(CInt(Rs2.Fields("Released").Value))
    '                                    '        End If
    '                                    '        RowInfo(11) = Rs2.Fields("Qualitative").Value
    '                                    '        RowInfo(12) = AccID
    '                                    '        RowInfo(13) = "REF"
    '                                    '        RowInfo(14) = Rs2.Fields("Reflexer_ID").Value.ToString
    '                                    '        RowInfo(15) = Rs2.Fields("Reflexed_ID").Value.ToString
    '                                    '        If Rs2.Fields("Comment").Value IsNot DBNull.Value Then
    '                                    '            RowInfo(16) = Rs2.Fields("Comment").Value
    '                                    '        Else
    '                                    '            RowInfo(16) = ""
    '                                    '        End If
    '                                    '        If Rs2.Fields("T_Result").Value IsNot DBNull.Value Then
    '                                    '            RowInfo(17) = Rs2.Fields("T_Result").Value
    '                                    '        Else
    '                                    '            RowInfo(17) = ""
    '                                    '        End If
    '                                    '        If Rs2.Fields("I_Result").Value IsNot DBNull.Value _
    '                                    '        AndAlso CType(Rs2.Fields("I_Result").Value, Byte()).Length > 0 Then
    '                                    '            RowInfo(18) = Convert.ToBase64String(Rs2.Fields("I_Result").Value)
    '                                    '        Else
    '                                    '            RowInfo(18) = Nothing
    '                                    '        End If
    '                                    '        RowInfo(19) = Rs2.Fields("ESig").Value
    '                                    '        '
    '                                    '        If (RowInfo(0) IsNot Nothing AndAlso RowInfo(0) <> "") AndAlso _
    '                                    '        (RowInfo(12) IsNot Nothing AndAlso RowInfo(12) <> "") Then _
    '                                    '        CreateResultGridRow(RowInfo)
    '                                    '        '
    '                                    '        If RowInfo(10) <> "0" Then
    '                                    '            If ReleaserInfo(0, UBound(ReleaserInfo, 2)) IsNot Nothing _
    '                                    '            AndAlso ReleaserInfo(0, UBound(ReleaserInfo, 2)) <> "" _
    '                                    '            Then ReDim Preserve ReleaserInfo(UBound(ReleaserInfo, 1), _
    '                                    '            UBound(ReleaserInfo, 2) + 1)
    '                                    '            ReleaserInfo(0, UBound(ReleaserInfo, 2)) = Rs2.Fields("Test_ID").Value.ToString
    '                                    '            ReleaserInfo(1, UBound(ReleaserInfo, 2)) = Rs2.Fields("Released_By").Value.ToString
    '                                    '            ReleaserInfo(2, UBound(ReleaserInfo, 2)) = Rs2.Fields("Release_Time").Value.ToString
    '                                    '        End If
    '                                    '        Rs2.MoveNext()
    '                                    '    Loop
    '                                    'End If
    '                                    'Rs2.Close()
    '                                    'Rs2 = Nothing
    '                                End If   'end of Drugs
    '                                RsA.MoveNext()
    '                            Loop
    '                        End If   'end if orgs
    '                        RsA.Close()
    '                        RsA = Nothing
    '                    Next o
    '                End If
    '                ' Fetch Info
    '                Dim Infos() As String = GetInfos(Val(AccID), Rs.Fields("Test_ID").Value)
    '                If Infos(0) <> "" Then   'Infos exist
    '                    For i = 0 To Infos.Length - 1
    '                        Dim RsI As New ADODB.Recordset
    '                        RsI.Open("Select a.Accession_ID, a.Test_ID, a.Info_ID, a.Result, a.Flag, a.NormalRange, a.Comment, " & _
    '                        "a.T_Result, a.I_Result, a.Released,  a.Released_By, a.Release_Time, b.* from Acc_Info_Results " & _
    '                        "a inner join Tests b on a.Info_ID = b.ID where b.IsActive <> 0 and b.HasResult <> 0 and b.PreAnalytical = 0 " & _
    '                        "and a.Accession_ID = " & AccID & " and a.Test_ID = " & Rs.Fields("Test_ID").Value & " and a.Info_ID = " & _
    '                        Val(Infos(i)), CN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '                        If Not RsI.BOF Then
    '                            Do Until RsI.EOF
    '                                RowInfo(0) = RsI.Fields("Info_ID").Value.ToString
    '                                RowInfo(1) = RsI.Fields("Name").Value
    '                                If RsI.Fields("Result").Value IsNot DBNull.Value Then
    '                                    RowInfo(2) = RsI.Fields("Result").Value
    '                                Else
    '                                    RowInfo(2) = ""
    '                                End If
    '                                If RsI.Fields("Flag").Value IsNot DBNull.Value Then
    '                                    RowInfo(4) = RsI.Fields("Flag").Value
    '                                Else
    '                                    RowInfo(4) = ""
    '                                End If
    '                                If RsI.Fields("NormalRange").Value IsNot DBNull.Value Then
    '                                    RowInfo(5) = RsI.Fields("NormalRange").Value
    '                                Else
    '                                    RowInfo(5) = ""
    '                                End If
    '                                If RsI.Fields("Released").Value Is DBNull.Value Then
    '                                    RowInfo(10) = "0"
    '                                Else
    '                                    RowInfo(10) = CStr(CInt(RsI.Fields("Released").Value))
    '                                End If
    '                                RowInfo(11) = RsI.Fields("Qualitative").Value
    '                                RowInfo(12) = AccID
    '                                RowInfo(13) = "info"
    '                                If RsI.Fields("ESig").Value = 0 Then
    '                                    RowInfo(14) = RsI.Fields("Test_ID").Value.ToString
    '                                    RowInfo(15) = RsI.Fields("Info_ID").Value.ToString
    '                                Else
    '                                    RowInfo(14) = "SIG"
    '                                    RowInfo(15) = ""
    '                                End If
    '                                If RsI.Fields("Comment").Value IsNot DBNull.Value Then
    '                                    RowInfo(16) = RsI.Fields("Comment").Value
    '                                Else
    '                                    RowInfo(16) = ""
    '                                End If
    '                                If RsI.Fields("T_Result").Value IsNot DBNull.Value Then
    '                                    RowInfo(17) = RsI.Fields("T_Result").Value
    '                                Else
    '                                    RowInfo(17) = ""
    '                                End If
    '                                If RsI.Fields("I_Result").Value IsNot DBNull.Value _
    '                                AndAlso CType(RsI.Fields("I_Result").Value, Byte()).Length > 0 Then
    '                                    RowInfo(18) = Convert.ToBase64String(RsI.Fields("I_Result").Value)
    '                                Else
    '                                    RowInfo(18) = Nothing
    '                                End If
    '                                RowInfo(19) = RsI.Fields("ESig").Value
    '                                '
    '                                If (RowInfo(0) IsNot Nothing AndAlso RowInfo(0) <> "") AndAlso _
    '                                (RowInfo(12) IsNot Nothing AndAlso RowInfo(12) <> "") Then _
    '                                CreateResultGridRow(RowInfo)
    '                                '
    '                                If RowInfo(10) <> "0" Then
    '                                    If ReleaserInfo(0, UBound(ReleaserInfo, 2)) IsNot Nothing _
    '                                    AndAlso ReleaserInfo(0, UBound(ReleaserInfo, 2)) <> "" _
    '                                    Then ReDim Preserve ReleaserInfo(UBound(ReleaserInfo, 1), _
    '                                    UBound(ReleaserInfo, 2) + 1)
    '                                    ReleaserInfo(0, UBound(ReleaserInfo, 2)) = RsI.Fields("Test_ID").Value.ToString
    '                                    ReleaserInfo(1, UBound(ReleaserInfo, 2)) = RsI.Fields("Released_By").Value.ToString
    '                                    ReleaserInfo(2, UBound(ReleaserInfo, 2)) = RsI.Fields("Release_Time").Value.ToString
    '                                End If
    '                                RsI.MoveNext()
    '                            Loop
    '                        End If
    '                        RsI.Close()
    '                        RsI = Nothing
    '                    Next i
    '                End If
    '                '  End of all rows related to the test id of AccResults
    '                Rs.MoveNext()
    '            Loop
    '        End If
    '        Rs.Close()
    '        Rs = Nothing
    '    End If
    '    '
    '    txtNote.Text = ""
    '    Dim COMDIRS() As String = GetAccComDir(AccID)
    '    txtNote.Text = COMDIRS(0)
    '    Dim ItemX As MyList
    '    cmbDirector.SelectedIndex = -1
    '    If COMDIRS(1) = "" Then
    '        If cmbDirector.Items.Count > 0 Then cmbDirector.SelectedIndex = 0
    '    Else
    '        If cmbDirector.Items.Count > 0 Then
    '            Dim dr As Integer
    '            For dr = 0 To cmbDirector.Items.Count - 1
    '                ItemX = cmbDirector.Items(dr)
    '                If ItemX.ItemData = COMDIRS(1) Then
    '                    cmbDirector.SelectedIndex = dr
    '                    Exit For
    '                End If
    '            Next
    '        End If
    '        If cmbDirector.SelectedIndex = -1 Then  'Old
    '            'If RptComp = True Then
    '            ItemX = New MyList(getlabdirectorname(COMDIRS(1)), COMDIRS(1))
    '            cmbDirector.Items.Add(ItemX)
    '            cmbDirector.SelectedIndex = cmbDirector.Items.Count - 1
    '            cmbDirector.Enabled = False
    '            'End If
    '        End If
    '    End If
    '    COMDIRS = Nothing
    '    '
    '    'If RptComp Then     'Report Complete
    '    '    CN.Execute("Update Requisitions Set Reported_Final = '" & _
    '    '    Date.Now & "' where Reported_Final is NULL and ID = " & AccID)
    '    '    If ThisUser.Supervisor Or ThisUser.Director Then
    '    '        txtNote.ReadOnly = False
    '    '        txtRptDate.ReadOnly = False
    '    '        txtRptTime.ReadOnly = False
    '    '        btnBlock.Enabled = True
    '    '        btnRelease.Enabled = True
    '    '    Else
    '    '        txtNote.ReadOnly = True
    '    '        txtRptDate.ReadOnly = True
    '    '        txtRptTime.ReadOnly = True
    '    '        btnBlock.Enabled = False
    '    '        btnRelease.Enabled = False
    '    '    End If
    '    'Else        'Initial report
    '    '    CN.Execute("Update Requisitions Set Reported_Final = " & _
    '    '    "NULL where ID = " & AccID)
    '    '    If ThisUser.Result_Release Then
    '    '        'txtNote.ReadOnly = False
    '    '        txtRptDate.ReadOnly = False
    '    '        txtRptTime.ReadOnly = False
    '    '        btnBlock.Enabled = True
    '    '        btnRelease.Enabled = True
    '    '    Else    'Only entry - no release
    '    '        'txtNote.ReadOnly = True
    '    '        txtRptDate.ReadOnly = True
    '    '        txtRptTime.ReadOnly = True
    '    '        btnBlock.Enabled = False
    '    '        btnRelease.Enabled = False
    '    '    End If
    '    'End If
    '    '********** Audit Trail Code ***************
    '    '0=TestID, 1=SavedRes, 2=SavedRel, 3=ChangedRes, 4=ChangedRel, 
    '    '5=SavedNote, 6=SavedRTF, 7=newNote, 8=newRTF
    '    If SystemConfig.AuditTrail = True Then
    '        If dgvResults.RowCount > 0 Then
    '            'ReDim OldResults(dgvResults.RowCount - 1, 8)
    '            ''0=TestID, 1=Res, 2=Flag, 3=Note, 4=RTF, 5=Cause0, 6=Cause1, 7=Cause2, 8=Rel
    '            'For i = 0 To dgvResults.RowCount - 1
    '            '    OldResults(i, 0) = dgvResults.Rows(i).Cells(0).Value.ToString
    '            '    OldResults(i, 1) = Trim(dgvResults.Rows(i).Cells(2).Value)
    '            '    OldResults(i, 2) = dgvResults.Rows(i).Cells(4).Value
    '            '    OldResults(i, 3) = dgvResults.Rows(i).Cells(16).Value
    '            '    OldResults(i, 4) = dgvResults.Rows(i).Cells(17).Value
    '            '    OldResults(i, 5) = dgvResults.Rows(i).Cells(13).Value
    '            '    OldResults(i, 6) = dgvResults.Rows(i).Cells(14).Value
    '            '    OldResults(i, 7) = dgvResults.Rows(i).Cells(15).Value
    '            '    OldResults(i, 8) = dgvResults.Rows(i).Cells(10).Value
    '            'Next
    '            ReDim ResultsAT(8, dgvResults.RowCount - 1)
    '            For i = 0 To dgvResults.RowCount - 1
    '                If dgvResults.Rows(i).Cells(13).Value = "ACC" Then
    '                    ResultsAT(0, i) = dgvResults.Rows(i).Cells(0).Value.ToString
    '                Else
    '                    ResultsAT(0, i) = dgvResults.Rows(i).Cells(15).Value.ToString _
    '                    & "-" & dgvResults.Rows(i).Cells(0).Value.ToString
    '                End If
    '                ResultsAT(1, i) = Trim(dgvResults.Rows(i).Cells(2).Value)
    '                ResultsAT(2, i) = IIf(dgvResults.Rows(i).Cells(10).Value IsNot _
    '                System.DBNull.Value AndAlso dgvResults.Rows(i).Cells(10).Value = _
    '                True, "1", "0")
    '                ResultsAT(3, i) = ""
    '                ResultsAT(4, i) = ""
    '                ResultsAT(5, i) = Trim(dgvResults.Rows(i).Cells(16).Value)
    '                ResultsAT(6, i) = Trim(dgvResults.Rows(i).Cells(17).Value)
    '                ResultsAT(7, i) = ""
    '                ResultsAT(8, i) = ""
    '            Next
    '        End If
    '    End If
    '    '********** End Audit Trail ****************
    '    'Catch Ex As Exception
    '    '   MsgBox(Ex)
    '    'End Try
    '    stopWatch.Stop()
    '    lblStatus.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", _
    '    stopWatch.Elapsed.Hours, stopWatch.Elapsed.Minutes, _
    '    stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds / 10)
    'End Sub

    Public Sub DisplayAccession(ByVal AccID As String, ByVal WID As String)
        If AccessionRejected(AccID) Then
            MsgBox("The accession '" & AccID & "' is rejected. " &
            "Rejected accession can not be resulted.", MsgBoxStyle.Critical, "Prolis")
        Else
            Dim stopWatch As New Stopwatch()
            stopWatch.Start()
            '''''''
            dgvResults.Rows.Clear()
            Dim o As Integer
            Dim d As Integer
            Dim i As Integer
            Dim Cause0 As String = ""   'Res Type
            Dim Cause1 As String = ""   'Reflexer
            Dim Cause2 As String = ""   'Reflexed
            Dim RowInfo() As String = {"", "", "", Nothing, "", "", Nothing,
            Nothing, Nothing, Nothing, "", "", "", "", "", "", "", "", "", ""}
            Dim RptComp As Boolean = ReportFullResulted(AccID)
            Dim Drawn As Date = GetDrawnDate(Val(AccID))
            If Drawn <> "#12:00:00#" Then _
            txtdrawn.Text = Drawn
            Dim Client() As String = GetClient(Val(AccID))
            txtClient.Text = Client(0)
            txtAttProv.Text = Client(1)
            txtPatient.Text = GetPatient(Val(AccID))
            txtMeds.Text = GetMedications(AccID)
            Dim Reported As String = GetReported(Val(AccID))
            If Reported <> "" Then
                txtRptDate.Text = Format(CDate(Reported), SystemConfig.DateFormat)
                txtRptTime.Text = Format(CDate(Reported), "HH:mm")
            Else
                txtRptDate.Text = ""
                txtRptTime.Text = ""
            End If
            '
            PopulateDirectors()
            cmbDirector.Enabled = True
            'dgvResults.Rows.Clear()
            My.Application.DoEvents()
            '
            'Dim Rs As New ADODB.Recordset
            Dim Result As String = ""
            Dim NormalRange As String = ""
            Dim Flag As String = ""
            Dim Release As Boolean = False
            Dim Cmnt As String = ""
            Dim RTF As String = ""
            Dim IMG As Image = Nothing
            Dim Behavior As String = ""
            '
            Dim cnacc As New SqlClient.SqlConnection(connString)
            cnacc.Open()
            Dim cmdacc As New SqlClient.SqlCommand("ReadAccResults_SP", cnacc)
            cmdacc.CommandType = CommandType.StoredProcedure
            cmdacc.Parameters.AddWithValue("@AccID", AccID)
            cmdacc.Parameters.AddWithValue("@WID", WID)
            Dim dracc As SqlClient.SqlDataReader = cmdacc.ExecuteReader
            If dracc.HasRows Then
                While dracc.Read
                    If dracc("Result") IsNot DBNull.Value Then
                        Result = dracc("Result")
                    Else
                        Result = ""
                    End If
                    If dracc("Flag") IsNot DBNull.Value Then
                        Flag = Trim(dracc("Flag"))
                        If dracc("Behavior") IsNot DBNull.Value Then
                            Behavior = Trim(dracc("Behavior"))
                        Else
                            Behavior = "Ignore"
                        End If
                    Else
                        Flag = ""
                        Behavior = "Ignore"
                    End If
                    If dracc("NormalRange") IsNot DBNull.Value Then
                        NormalRange = dracc("NormalRange")
                    Else
                        NormalRange = ""
                    End If
                    If dracc("Released") Is DBNull.Value Then
                        Release = False
                    Else
                        Release = dracc("Released")
                    End If
                    If dracc("Comment") IsNot DBNull.Value Then
                        Cmnt = dracc("Comment")
                    Else
                        Cmnt = ""
                    End If
                    If dracc("T_Result") IsNot DBNull.Value Then
                        RTF = dracc("T_Result")
                    Else
                        RTF = ""
                    End If
                    If dracc("I_Result") IsNot DBNull.Value _
                    AndAlso CType(dracc("I_Result"), Array).Length > 0 Then
                        Try
                            IMG = CType(dracc("I_Result"), Image)

                        Catch ex As Exception
                            IMG = Nothing
                            Try
                                IMG = BytesToImage(dracc("I_Result"))

                            Catch exd As Exception
                                IMG = Nothing
                            End Try
                        End Try
                    Else
                        IMG = Nothing
                    End If
                    If RTF.ToString().Contains(Wildcard.Text) Then
                        Flag = ""
                        Behavior = "Ignore"
                    End If
                    dgvResults.Rows.Add(dracc("Test_ID"), dracc("Name"), Result, Nothing, Flag,
                    NormalRange, Nothing, Nothing, Nothing, Nothing, Release, dracc("Qualitative"),
                    AccID, "ACC", "", "", Cmnt, RTF, IMG, dracc("eSig"), Behavior)
                    '0=TID, 1=TName, 2=Result, 3=nothing, 4=Flag, 5=NormalRange, 6=nothing, 7=nothing, 8=nothing, 9=nothing, 
                    '10=Release, 11=Qualitative, 12=AccID, 13=Cause0, 14=Cause1, 15=Cause2, 16=Cmnt, 17=RTF, 18=IMG, 19=Sig, 20=Behavior
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).Style.BackColor = Color.White
                    If dracc("Qualitative") <> 0 Then
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(3).Value =
                        Image.FromFile(My.Application.Info.DirectoryPath & "\Images\Looks.ico")
                    End If
                    If Behavior = "Caution" Then
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(4).Style = AbnormalStyle
                    ElseIf Behavior = "Panic" Then
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(4).Style = PanicStyle
                    End If
                    If Cmnt <> "" Then dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).Value =
                    Image.FromFile(My.Application.Info.DirectoryPath & "\Images\Note.ico")
                    If RTF <> "" Then
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(9).Value = IMG
                    End If
                    '
                    If dgvResults.Rows(dgvResults.RowCount - 1).Cells(10).Value = True Then
                        If ReleaserInfo(0, UBound(ReleaserInfo, 2)) IsNot Nothing _
                        AndAlso ReleaserInfo(0, UBound(ReleaserInfo, 2)) <> "" _
                        Then ReDim Preserve ReleaserInfo(UBound(ReleaserInfo, 1),
                        UBound(ReleaserInfo, 2) + 1)
                        ReleaserInfo(0, UBound(ReleaserInfo, 2)) = dracc("Test_ID").ToString
                        ReleaserInfo(1, UBound(ReleaserInfo, 2)) = dracc("Released_By").ToString
                        ReleaserInfo(2, UBound(ReleaserInfo, 2)) = dracc("Release_Time").ToString
                    End If
                    '
                    Dim AccReflexeds() As String = GetAccReflexedIDs(AccID, dracc("Test_ID"))

                    For o = 0 To AccReflexeds.Length - 1
                        If AccReflexeds(o) <> "" Then   'Organism or Reflexed1
                            Dim cnOrg As New SqlClient.SqlConnection(connString)
                            cnOrg.Open()
                            Dim cmdOrg As New SqlClient.SqlCommand("Select a.Accession_ID, a.Reflexer_ID, Reflexed_ID, a.Test_ID, a.Result, " &
                            "a.Flag, a.Behavior, a.NormalRange, a.Comment, a.T_Result, a.I_Result, a.Released, a.Released_By, a.Release_Time, " &
                            "b.Name, b.Qualitative, b.eSig from Ref_Results a inner join Tests b on a.Test_ID = b.ID where b.IsActive <> 0 and " &
                            "b.HasResult <> 0 and a.Accession_ID = " & AccID & " and a.Reflexer_ID = " & dracc("Test_ID") &
                            " and a.Reflexed_ID = " & Val(AccReflexeds(o)), cnOrg)
                            cmdOrg.CommandType = CommandType.Text
                            Dim DROrg As SqlClient.SqlDataReader = cmdOrg.ExecuteReader
                            If DROrg.HasRows Then
                                While DROrg.Read
                                    If DROrg("Result") IsNot DBNull.Value Then
                                        Result = DROrg("Result")
                                    Else
                                        Result = ""
                                    End If
                                    If DROrg("Flag") IsNot DBNull.Value Then
                                        Flag = Trim(DROrg("Flag"))
                                        If DROrg("Behavior") IsNot DBNull.Value Then
                                            Behavior = Trim(DROrg("Behavior"))
                                        Else
                                            Behavior = "Ignore"
                                        End If
                                    Else
                                        Flag = ""
                                        Behavior = "Ignore"
                                    End If
                                    If DROrg("NormalRange") IsNot DBNull.Value Then
                                        NormalRange = Trim(DROrg("NormalRange"))
                                    Else
                                        NormalRange = ""
                                    End If
                                    If DROrg("Released") Is DBNull.Value Then
                                        Release = False
                                    Else
                                        Release = CStr(CInt(DROrg("Released")))
                                    End If
                                    RowInfo(12) = AccID
                                    Cause0 = "REF"
                                    Cause1 = dracc("Test_ID")    'Reflexer
                                    Cause2 = DROrg("Reflexed_ID")
                                    If DROrg("Comment") IsNot DBNull.Value Then
                                        Cmnt = DROrg("Comment")
                                    Else
                                        Cmnt = ""
                                    End If
                                    If DROrg("T_Result") IsNot DBNull.Value AndAlso DROrg("T_Result") <> "" Then
                                        RTF = DROrg("T_Result")
                                    Else
                                        RTF = ""
                                    End If
                                    If DROrg("I_Result") IsNot DBNull.Value _
                                    AndAlso CType(DROrg("I_Result"), Byte()).Length > 0 Then
                                        Try
                                            IMG = BytesToImage(DROrg("I_Result"))

                                        Catch ex As Exception

                                        End Try
                                    Else
                                        IMG = Nothing
                                    End If
                                    '
                                    dgvResults.Rows.Add(DROrg("Test_ID"), DROrg("Name"), Result, Nothing, Flag,
                                    NormalRange, Nothing, Nothing, Nothing, Nothing, Release, DROrg("Qualitative"),
                                    AccID, Cause0, Cause1, Cause2, Cmnt, RTF, IMG, DROrg("eSig"), Behavior)
                                    '0=TID, 1=TName, 2=Result, 3=nothing, 4=Flag, 5=NormalRange, 6=nothing, 7=nothing, 8=nothing, 9=nothing, 
                                    '10=Release, 11=Qualitative, 12=AccID, 13=Cause0, 14=Cause1, 15=Cause2, 16=Cmnt, 17=RTF, 18=IMG, 19=Sig, 20=Behavior
                                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).Style.BackColor = Color.White
                                    If Cause0 = "REF" Then
                                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(3).Value =
                                        Image.FromFile(My.Application.Info.DirectoryPath & "\Images\Reflux.ico")
                                    End If
                                    If Behavior = "Caution" Then
                                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(4).Style = AbnormalStyle
                                    ElseIf Behavior = "Panic" Then
                                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(4).Style = PanicStyle
                                    End If
                                    '
                                    If Cmnt <> "" Then dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).Value =
                                    Image.FromFile(My.Application.Info.DirectoryPath & "\Images\Note.ico")
                                    If RTF <> "" Then dgvResults.Rows(dgvResults.RowCount - 1).Cells(8).Value =
                                    Image.FromFile(My.Application.Info.DirectoryPath & "\Images\RTF.ico")
                                    '
                                    If dgvResults.Rows(dgvResults.RowCount - 1).Cells(10).Value = True Then
                                        If ReleaserInfo(0, UBound(ReleaserInfo, 2)) IsNot Nothing _
                                        AndAlso ReleaserInfo(0, UBound(ReleaserInfo, 2)) <> "" _
                                        Then ReDim Preserve ReleaserInfo(UBound(ReleaserInfo, 1),
                                        UBound(ReleaserInfo, 2) + 1)
                                        ReleaserInfo(0, UBound(ReleaserInfo, 2)) = DROrg("Test_ID").ToString
                                        ReleaserInfo(1, UBound(ReleaserInfo, 2)) = DROrg("Released_By").ToString
                                        ReleaserInfo(2, UBound(ReleaserInfo, 2)) = DROrg("Release_Time").ToString
                                    End If
                                    ''''''''''''''''''''2nd level Reflex
                                    Dim DRUGS() As String = GetAccReflexedIDs(AccID, Val(AccReflexeds(o)))
                                    For d = 0 To DRUGS.Length - 1
                                        If DRUGS(d) <> "" Then
                                            Dim cnDrg As New SqlClient.SqlConnection(connString)
                                            cnDrg.Open()
                                            Dim cmdDrg As New SqlClient.SqlCommand("Select a.Accession_ID, a.Reflexer_ID, Reflexed_ID, a.Test_ID, " &
                                            "a.Result, a.Flag, a.Behavior, a.NormalRange, a.Comment, a.T_Result, a.I_Result, a.Released, a.Released_By, " &
                                            "a.Release_Time, b.Name, b.Qualitative, b.eSig from Ref_Results a inner join Tests b on a.Test_ID = b.ID " &
                                            "where b.IsActive <> 0 and b.HasResult <> 0 and a.Accession_ID = " & AccID & " and a.Reflexer_ID = " &
                                            Val(AccReflexeds(o)) & " and a.Reflexed_ID = " & Val(DRUGS(d)), cnDrg)
                                            cmdDrg.CommandType = CommandType.Text
                                            Dim DRDrg As SqlClient.SqlDataReader = cmdDrg.ExecuteReader
                                            If DRDrg.HasRows Then
                                                While DRDrg.Read
                                                    If DRDrg("Result") IsNot DBNull.Value Then
                                                        Result = DRDrg("Result")
                                                    Else
                                                        Result = ""
                                                    End If
                                                    If DRDrg("Flag") IsNot DBNull.Value Then
                                                        Flag = Trim(DRDrg("Flag"))
                                                        If DRDrg("Behavior") IsNot DBNull.Value Then
                                                            Behavior = Trim(DRDrg("Behavior"))
                                                        Else
                                                            Behavior = "Ignore"
                                                        End If
                                                    Else
                                                        Flag = ""
                                                        Behavior = "Ignore"
                                                    End If
                                                    If DRDrg("NormalRange") IsNot DBNull.Value Then
                                                        NormalRange = Trim(DRDrg("NormalRange"))
                                                    Else
                                                        NormalRange = ""
                                                    End If
                                                    If DRDrg("Released") Is DBNull.Value Then
                                                        Release = "0"
                                                    Else
                                                        Release = CStr(CInt(DRDrg("Released")))
                                                    End If
                                                    Cause0 = "REF"
                                                    Cause1 = AccReflexeds(o)   'Reflexer
                                                    Cause2 = DRDrg("Reflexed_ID").ToString
                                                    If DRDrg("Comment") IsNot DBNull.Value Then
                                                        Cmnt = DRDrg("Comment")
                                                    Else
                                                        Cmnt = ""
                                                    End If
                                                    If DRDrg("T_Result") IsNot DBNull.Value Then
                                                        RTF = DRDrg("T_Result")
                                                    Else
                                                        RTF = ""
                                                    End If
                                                    If DRDrg("I_Result") IsNot DBNull.Value _
                                                    AndAlso CType(DRDrg("I_Result"), Byte()).Length > 0 Then
                                                        IMG = BytesToImage(DROrg("I_Result"))
                                                    Else
                                                        IMG = Nothing
                                                    End If
                                                    '
                                                    dgvResults.Rows.Add(DRDrg("Test_ID"), DRDrg("Name"), Result, Nothing, Flag,
                                                    NormalRange, Nothing, Nothing, Nothing, Nothing, Release, DRDrg("Qualitative"),
                                                    AccID, Cause0, Cause1, Cause2, Cmnt, RTF, IMG, DRDrg("eSig"), Behavior)
                                                    '0=TID, 1=TName, 2=Result, 3=nothing, 4=Flag, 5=NormalRange, 6=nothing, 7=nothing, 
                                                    '8=nothing, 9=nothing, 10=Release, 11=Qualitative, 12=AccID, 13=Cause0, 14=Cause1, 
                                                    '15=Cause2, 16=Cmnt, 17=RTF, 18=IMG, 19=Sig, 20=Behavior
                                                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).Style.BackColor = Color.White
                                                    If Cause0 = "REF" Then
                                                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(3).Value =
                                                        Image.FromFile(My.Application.Info.DirectoryPath & "\Images\Reflux.ico")
                                                    End If
                                                    '
                                                    If Behavior = "Caution" Then
                                                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(4).Style = AbnormalStyle
                                                    ElseIf Behavior = "Panic" Then
                                                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(4).Style = PanicStyle
                                                    End If
                                                    '
                                                    If Cmnt <> "" Then dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).Value =
                                                    Image.FromFile(My.Application.Info.DirectoryPath & "\Images\Note.ico")
                                                    If RTF <> "" Then dgvResults.Rows(dgvResults.RowCount - 1).Cells(8).Value =
                                                    Image.FromFile(My.Application.Info.DirectoryPath & "\Images\RTF.ico")
                                                    '
                                                    If dgvResults.Rows(dgvResults.RowCount - 1).Cells(10).Value = True Then
                                                        If ReleaserInfo(0, UBound(ReleaserInfo, 2)) IsNot Nothing _
                                                        AndAlso ReleaserInfo(0, UBound(ReleaserInfo, 2)) <> "" _
                                                        Then ReDim Preserve ReleaserInfo(UBound(ReleaserInfo, 1),
                                                        UBound(ReleaserInfo, 2) + 1)
                                                        ReleaserInfo(0, UBound(ReleaserInfo, 2)) = DRDrg("Test_ID").ToString
                                                        ReleaserInfo(1, UBound(ReleaserInfo, 2)) = DRDrg("Released_By").ToString
                                                        ReleaserInfo(2, UBound(ReleaserInfo, 2)) = DRDrg("Release_Time").ToString
                                                    End If
                                                End While
                                            End If
                                            cnDrg.Close()
                                            cnDrg = Nothing
                                        End If   'end of Drugs
                                    Next d
                                End While
                            End If
                            cnOrg.Close()
                            cnOrg = Nothing
                        End If
                    Next o
                    ' Fetch Info
                    Dim Infos() As String = GetInfos(Val(AccID), dracc("Test_ID"))
                    For f As Integer = 0 To Infos.Length - 1
                        If Infos(f) <> "" Then   'Infos exist
                            Dim cnInf As New SqlClient.SqlConnection(connString)
                            cnInf.Open()
                            Dim cmdInf As New SqlClient.SqlCommand("Select a.Accession_ID, a.Test_ID, a.Info_ID, a.Result, a.Flag, " &
                            "a.Behavior, a.NormalRange, a.Comment, a.T_Result, a.I_Result, a.Released,  a.Released_By, a.Release_Time, " &
                            "b.Name, b.Qualitative, b.eSig from Acc_Info_Results a inner join Tests b on a.Info_ID = b.ID where " &
                            "b.IsActive <> 0 and b.HasResult <> 0 and b.PreAnalytical = 0 and a.Accession_ID = " & AccID & " and " &
                            "a.Test_ID = " & dracc("Test_ID") & " and a.Info_ID = " & Val(Infos(f)), cnInf)
                            cmdInf.CommandType = CommandType.Text
                            Dim DRInf As SqlClient.SqlDataReader = cmdInf.ExecuteReader
                            If DRInf.HasRows Then
                                While DRInf.Read
                                    If DRInf("Result") IsNot DBNull.Value Then
                                        Result = DRInf("Result")
                                    Else
                                        Result = ""
                                    End If
                                    If DRInf("Flag") IsNot DBNull.Value Then
                                        Flag = Trim(DRInf("Flag"))
                                        If DRInf("Behavior") IsNot DBNull.Value Then
                                            Behavior = Trim(DRInf("Behavior"))
                                        Else
                                            Behavior = "Ignore"
                                        End If
                                    Else
                                        Flag = ""
                                        Behavior = "Ignore"
                                    End If
                                    If DRInf("NormalRange") IsNot DBNull.Value Then
                                        NormalRange = Trim(DRInf("NormalRange"))
                                    Else
                                        NormalRange = ""
                                    End If
                                    If DRInf("Released") Is DBNull.Value Then
                                        Release = "0"
                                    Else
                                        Release = CStr(CInt(DRInf("Released")))
                                    End If
                                    Cause0 = "info"
                                    Cause1 = dracc("Test_ID")
                                    Cause2 = DRInf("Info_ID").ToString
                                    If DRInf("Comment") IsNot DBNull.Value Then
                                        Cmnt = DRInf("Comment")
                                    Else
                                        Cmnt = ""
                                    End If
                                    If DRInf("T_Result") IsNot DBNull.Value Then
                                        RTF = DRInf("T_Result")
                                    Else
                                        RTF = ""
                                    End If
                                    If DRInf("I_Result") IsNot DBNull.Value _
                                    AndAlso CType(DRInf("I_Result"), Byte()).Length > 0 Then

                                        IMG = BytesToImage(DRInf("I_Result"))
                                    Else
                                        IMG = Nothing
                                    End If
                                    '
                                    dgvResults.Rows.Add(DRInf("Info_ID"), DRInf("Name"), Result, Nothing, Flag,
                                    NormalRange, Nothing, Nothing, Nothing, Nothing, Release, DRInf("Qualitative"),
                                    AccID, Cause0, Cause1, Cause2, Cmnt, RTF, IMG, DRInf("eSig"), Behavior)
                                    '0=TID, 1=TName, 2=Result, 3=nothing, 4=Flag, 5=NormalRange, 6=nothing, 7=nothing, 8=nothing, 9=nothing, 
                                    '10=Release, 11=Qualitative, 12=AccID, 13=Cause0, 14=Cause1, 15=Cause2, 16=Cmnt, 17=RTF, 18=IMG, 19=Sig, 20=Behavior
                                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).Style.BackColor = Color.White
                                    If Behavior = "Caution" Then
                                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(4).Style = AbnormalStyle
                                    ElseIf Behavior = "Panic" Then
                                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(4).Style = PanicStyle
                                    End If
                                    '
                                    If Cmnt <> "" Then dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).Value =
                                    Image.FromFile(My.Application.Info.DirectoryPath & "\Images\Note.ico")
                                    If RTF <> "" Then dgvResults.Rows(dgvResults.RowCount - 1).Cells(8).Value =
                                    Image.FromFile(My.Application.Info.DirectoryPath & "\Images\RTF.ico")
                                    '
                                    If dgvResults.Rows(dgvResults.RowCount - 1).Cells(10).Value = True Then
                                        If ReleaserInfo(0, UBound(ReleaserInfo, 2)) IsNot Nothing _
                                        AndAlso ReleaserInfo(0, UBound(ReleaserInfo, 2)) <> "" _
                                        Then ReDim Preserve ReleaserInfo(UBound(ReleaserInfo, 1),
                                        UBound(ReleaserInfo, 2) + 1)
                                        ReleaserInfo(0, UBound(ReleaserInfo, 2)) = DRInf("Info_ID").ToString
                                        ReleaserInfo(1, UBound(ReleaserInfo, 2)) = DRInf("Released_By").ToString
                                        ReleaserInfo(2, UBound(ReleaserInfo, 2)) = DRInf("Release_Time").ToString
                                    End If
                                End While
                            End If
                            cnInf.Close()
                            cnInf = Nothing
                        End If
                    Next f
                    '  End of all rows related to the test id of AccResults
                End While
            End If
            cnacc.Close()
            cnacc = Nothing
            '
            txtNote.Text = ""
            Dim COMDIRS() As String = GetAccComDir(AccID)
            txtNote.Text = COMDIRS(0)
            Dim ItemX As MyList
            cmbDirector.SelectedIndex = -1
            If COMDIRS(1) = "" Then
                If cmbDirector.Items.Count > 0 Then cmbDirector.SelectedIndex = 0
            Else
                If cmbDirector.Items.Count > 0 Then
                    Dim dr As Integer
                    For dr = 0 To cmbDirector.Items.Count - 1
                        ItemX = cmbDirector.Items(dr)
                        If ItemX.ItemData = COMDIRS(1) Then
                            cmbDirector.SelectedIndex = dr
                            Exit For
                        End If
                    Next
                End If
                If cmbDirector.SelectedIndex = -1 Then  'Old
                    'If RptComp = True Then
                    ItemX = New MyList(getlabdirectorname(COMDIRS(1)), COMDIRS(1))
                    cmbDirector.Items.Add(ItemX)
                    cmbDirector.SelectedIndex = cmbDirector.Items.Count - 1
                    cmbDirector.Enabled = False
                    'End If
                End If
            End If
            COMDIRS = Nothing
            '
            '********** Audit Trail Code ***************
            '0=TestID, 1=SavedRes, 2=SavedRel, 3=ChangedRes, 4=ChangedRel, 
            '5=SavedNote, 6=SavedRTF, 7=newNote, 8=newRTF
            If SystemConfig.AuditTrail = True Then
                If dgvResults.RowCount > 0 Then
                    ReDim ResultsAT(8, dgvResults.RowCount - 1)
                    For i = 0 To dgvResults.RowCount - 1
                        If dgvResults.Rows(i).Cells(13).Value = "ACC" Then
                            ResultsAT(0, i) = dgvResults.Rows(i).Cells(0).Value.ToString
                        Else
                            ResultsAT(0, i) = dgvResults.Rows(i).Cells(15).Value.ToString _
                            & "-" & dgvResults.Rows(i).Cells(0).Value.ToString
                        End If
                        ResultsAT(1, i) = Trim(dgvResults.Rows(i).Cells(2).Value)
                        ResultsAT(2, i) = IIf(dgvResults.Rows(i).Cells(10).Value IsNot
                        System.DBNull.Value AndAlso dgvResults.Rows(i).Cells(10).Value =
                        True, "1", "0")
                        ResultsAT(3, i) = ""
                        ResultsAT(4, i) = ""
                        ResultsAT(5, i) = Trim(dgvResults.Rows(i).Cells(16).Value)
                        ResultsAT(6, i) = Trim(dgvResults.Rows(i).Cells(17).Value)
                        ResultsAT(7, i) = ""
                        ResultsAT(8, i) = ""
                    Next
                End If
            End If
            '********** End Audit Trail ****************
            'Catch Ex As Exception
            '   MsgBox(Ex)
            'End Try
            stopWatch.Stop()
            lblStatus.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            stopWatch.Elapsed.Hours, stopWatch.Elapsed.Minutes,
            stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds / 10)
        End If
    End Sub
    Public Function BytesToImage(byteArray As Byte()) As Image
        Using ms As New MemoryStream(byteArray)
            Return Image.FromStream(ms)
        End Using
    End Function
    Public Sub DisplayResults(ByVal AccID As String, ByVal WID As String)

        Try
            LoggerHelper.LogInfo("Loading DisplayResults")

            If AccessionRejected(AccID) Then
                MsgBox("The accession '" & AccID & "' is rejected. " &
                "Rejected accession can not be resulted.", MsgBoxStyle.Critical, "Prolis")
            Else
                If AccessionReceived(AccID) Then
                    Dim stopWatch As New Stopwatch()
                    stopWatch.Start()
                    '''''''
                    dgvResults.Rows.Clear()
                    Dim CorrectedStyle As New DataGridViewCellStyle
                    CorrectedStyle.ForeColor = Color.Blue
                    CorrectedStyle.BackColor = Color.GreenYellow
                    Dim NormalStyle As New DataGridViewCellStyle
                    Dim Cause0 As String = ""   'Res Type
                    Dim Cause1 As String = ""   'Reflexer
                    Dim Cause2 As String = ""   'Reflexed
                    Dim InfoCause As String = ""
                    '0=TID, 1=TName, 2=Result, 3=nothing, 4=Flag, 5=NormalRange, 6=Behavior, 7=nothing, 8=nothing, 9=nothing, 
                    '10=Release, 11=Qualitative, 12=AccID, 13=Cause0, 14=Cause1, 15=Cause2, 16=Cmnt, 17=RTF, 18=IMG, 19=Sig
                    Dim RowInfo() As String = {"", "", "", Nothing, "", "", Nothing,
                    Nothing, Nothing, Nothing, "", "", "", "", "", "", "", "", "", ""}
                    Dim RptComp As Boolean = ReportFullResulted(AccID)
                    Dim Drawn As Date = GetDrawnDate(Val(AccID))
                    If Drawn <> "#12:00:00 AM#" Then
                        txtdrawn.Text = Drawn
                    Else
                        txtdrawn.Text = ""
                    End If
                    Dim Client() As String = GetClient(Val(AccID))
                    txtClient.Text = Client(0)
                    txtAttProv.Text = Client(1)
                    txtPatient.Text = GetPatient(Val(AccID))
                    txtMeds.Text = GetMedications(AccID)
                    Dim Reported As String = GetReported(Val(AccID))
                    If Reported <> "" Then
                        txtRptDate.Text = Format(CDate(Reported), SystemConfig.DateFormat)
                        txtRptTime.Text = Format(CDate(Reported), "HH:mm")
                    Else
                        txtRptDate.Text = ""
                        txtRptTime.Text = ""
                    End If
                    '
                    PopulateDirectors()
                    cmbDirector.Enabled = True
                    'dgvResults.Rows.Clear()
                    My.Application.DoEvents()
                    '
                    'Dim Rs As New ADODB.Recordset
                    Dim Result As String = ""
                    Dim NormalRange As String = ""
                    Dim Flag As String = ""
                    Dim Release As Boolean = False
                    Dim Cmnt As String = ""
                    Dim RTF As String = ""
                    'Dim IMG As Image
                    '
                    Dim cnn As New SqlClient.SqlConnection(connString)
                    cnn.Open()
                    Dim cmdacc As New SqlClient.SqlCommand("DisplayUResults_SP", cnn)
                    cmdacc.CommandType = CommandType.StoredProcedure
                    cmdacc.Parameters.AddWithValue("@AccID", AccID)
                    cmdacc.Parameters.AddWithValue("@wid", WID)
                    Dim DTRes As New DataTable
                    Dim DARes As New SqlClient.SqlDataAdapter(cmdacc)
                    DARes.Fill(DTRes)
                    cnn.Close()
                    cnn = Nothing
                    For i As Integer = 0 To DTRes.Rows.Count - 1
                        RowInfo(0) = DTRes.Rows(i).Item("Test_ID").ToString
                        Cause1 = DTRes.Rows(i).Item("Test_ID").ToString
                        InfoCause = DTRes.Rows(i).Item("Test_ID").ToString
                        RowInfo(1) = DTRes.Rows(i).Item("Name")
                        If DTRes.Rows(i).Item("Result") IsNot DBNull.Value Then
                            RowInfo(2) = DTRes.Rows(i).Item("Result")
                        Else
                            RowInfo(2) = ""
                        End If
                        If DTRes.Rows(i).Item("Flag") IsNot DBNull.Value Then
                            RowInfo(4) = DTRes.Rows(i).Item("Flag")
                        Else
                            RowInfo(4) = ""
                        End If
                        If DTRes.Rows(i).Item("NormalRange") IsNot DBNull.Value Then
                            RowInfo(5) = DTRes.Rows(i).Item("NormalRange")
                        Else
                            RowInfo(5) = ""
                        End If
                        If DTRes.Rows(i).Item("Behavior") IsNot DBNull.Value Then
                            RowInfo(6) = DTRes.Rows(i).Item("Behavior")
                        Else
                            If RowInfo(4) = "" OrElse RowInfo(4).StartsWith("N") OrElse
                            RowInfo(4).StartsWith("Acc") OrElse RowInfo(4).StartsWith("Non") Then
                                RowInfo(6) = "Ignore"
                            ElseIf RowInfo(4) = "L" OrElse RowInfo(4) = "H" OrElse RowInfo(4) = "A" _
                            OrElse RowInfo(4).StartsWith("Incon") OrElse RowInfo(4).StartsWith("Pos") _
                            OrElse RowInfo(4).StartsWith("Reac") OrElse RowInfo(4).StartsWith("Res") _
                            OrElse RowInfo(4).StartsWith("Found") OrElse RowInfo(4).StartsWith("Detect") Then
                                RowInfo(6) = "Caution"
                            ElseIf RowInfo(4) = "LP" OrElse RowInfo(4) = "HP" OrElse
                            RowInfo(4) = "AP" OrElse RowInfo(4).Contains("Panic") Then
                                RowInfo(6) = "Panic"
                            Else
                                RowInfo(6) = "Ignore"
                            End If
                        End If
                        If DTRes.Rows(i).Item("Released") Is DBNull.Value Then
                            RowInfo(10) = "0"
                        Else
                            RowInfo(10) = Convert.ToInt16(DTRes.Rows(i).Item("Released")).ToString
                        End If
                        RowInfo(11) = DTRes.Rows(i).Item("Qualitative")
                        RowInfo(12) = AccID
                        RowInfo(13) = DTRes.Rows(i).Item("Cause0")
                        RowInfo(14) = DTRes.Rows(i).Item("Cause1")
                        RowInfo(15) = DTRes.Rows(i).Item("Cause2")
                        If DTRes.Rows(i).Item("Comment") IsNot DBNull.Value Then
                            RowInfo(16) = DTRes.Rows(i).Item("Comment")
                        Else
                            RowInfo(16) = ""
                        End If
                        If DTRes.Rows(i).Item("T_Result") IsNot DBNull.Value Then
                            RowInfo(17) = DTRes.Rows(i).Item("T_Result")
                        Else
                            RowInfo(17) = ""
                        End If
                        If DTRes.Rows(i).Item("I_Result") IsNot DBNull.Value _
                        AndAlso CType(DTRes.Rows(i).Item("I_Result"), Byte()).Length > 0 Then
                            RowInfo(18) = Convert.ToBase64String(DTRes.Rows(i).Item("I_Result"))
                        Else
                            RowInfo(18) = Nothing
                        End If
                        RowInfo(19) = DTRes.Rows(i).Item("ESig")
                        '
                        If (RowInfo(0) IsNot Nothing AndAlso RowInfo(0) <> "") AndAlso
                        (RowInfo(12) IsNot Nothing AndAlso RowInfo(12) <> "") Then _
                        CreateResultGridRow(RowInfo)
                        '
                        If RowInfo(10) <> "0" Then
                            If ReleaserInfo(0, UBound(ReleaserInfo, 2)) IsNot Nothing _
                            AndAlso ReleaserInfo(0, UBound(ReleaserInfo, 2)) <> "" _
                            Then ReDim Preserve ReleaserInfo(UBound(ReleaserInfo, 1),
                            UBound(ReleaserInfo, 2) + 1)
                            ReleaserInfo(0, UBound(ReleaserInfo, 2)) = DTRes.Rows(i).Item("Test_ID").ToString
                            ReleaserInfo(1, UBound(ReleaserInfo, 2)) = DTRes.Rows(i).Item("Released_By").ToString
                            ReleaserInfo(2, UBound(ReleaserInfo, 2)) = DTRes.Rows(i).Item("Release_Time").ToString
                        End If
                        ReDim RowInfo(19)
                        'Display User Reflexeds **************************
                        Dim cnrfr As New SqlClient.SqlConnection(connString)
                        cnrfr.Open()
                        Dim cmdrfr As New SqlClient.SqlCommand("DisplayREFResults_SP", cnrfr)
                        cmdrfr.CommandType = CommandType.StoredProcedure
                        cmdrfr.Parameters.AddWithValue("@AccID", AccID)
                        cmdrfr.Parameters.AddWithValue("@Cause1", Cause1)
                        Dim drrfr As SqlClient.SqlDataReader = cmdrfr.ExecuteReader
                        If drrfr.HasRows Then
                            While drrfr.Read
                                RowInfo(0) = drrfr("Test_ID").ToString
                                Cause1 = drrfr("Test_ID").ToString
                                RowInfo(1) = drrfr("Name")
                                If drrfr("Result") IsNot DBNull.Value Then
                                    RowInfo(2) = drrfr("Result")
                                Else
                                    RowInfo(2) = ""
                                End If
                                If drrfr("Flag") IsNot DBNull.Value Then
                                    RowInfo(4) = drrfr("Flag")
                                Else
                                    RowInfo(4) = ""
                                End If
                                If drrfr("NormalRange") IsNot DBNull.Value Then
                                    RowInfo(5) = drrfr("NormalRange")
                                Else
                                    RowInfo(5) = ""
                                End If
                                If drrfr("Released") Is DBNull.Value Then
                                    RowInfo(10) = "0"
                                Else
                                    RowInfo(10) = Convert.ToInt16(drrfr("Released")).ToString
                                End If
                                RowInfo(11) = drrfr("Qualitative")
                                RowInfo(12) = AccID
                                RowInfo(13) = drrfr("Cause0")
                                RowInfo(14) = drrfr("Cause1")
                                RowInfo(15) = drrfr("Cause2")
                                If drrfr("Comment") IsNot DBNull.Value Then
                                    RowInfo(16) = drrfr("Comment")
                                Else
                                    RowInfo(16) = ""
                                End If
                                If drrfr("T_Result") IsNot DBNull.Value Then
                                    RowInfo(17) = drrfr("T_Result")
                                Else
                                    RowInfo(17) = ""
                                End If
                                If drrfr("I_Result") IsNot DBNull.Value _
                                AndAlso CType(drrfr("I_Result"), Byte()).Length > 0 Then
                                    RowInfo(18) = Convert.ToBase64String(drrfr("I_Result"))
                                Else
                                    RowInfo(18) = Nothing
                                End If
                                RowInfo(19) = drrfr("ESig")
                                '
                                If (RowInfo(0) IsNot Nothing AndAlso RowInfo(0) <> "") AndAlso
                                (RowInfo(12) IsNot Nothing AndAlso RowInfo(12) <> "") Then _
                                CreateResultGridRow(RowInfo)
                                '
                                If RowInfo(10) <> "0" Then
                                    If ReleaserInfo(0, UBound(ReleaserInfo, 2)) IsNot Nothing _
                                    AndAlso ReleaserInfo(0, UBound(ReleaserInfo, 2)) <> "" _
                                    Then ReDim Preserve ReleaserInfo(UBound(ReleaserInfo, 1),
                                    UBound(ReleaserInfo, 2) + 1)
                                    ReleaserInfo(0, UBound(ReleaserInfo, 2)) = drrfr("Test_ID").ToString
                                    ReleaserInfo(1, UBound(ReleaserInfo, 2)) = drrfr("Released_By").ToString
                                    ReleaserInfo(2, UBound(ReleaserInfo, 2)) = drrfr("Release_Time").ToString
                                End If
                                ReDim RowInfo(19)
                                'Display User Reflexed Infos **************************
                                Dim cnuri As New SqlClient.SqlConnection(connString)
                                cnuri.Open()
                                Dim cmduri As New SqlClient.SqlCommand("DisplayInfoResults_SP", cnuri)
                                cmduri.CommandType = CommandType.StoredProcedure
                                cmduri.Parameters.AddWithValue("@AccID", AccID)
                                cmduri.Parameters.AddWithValue("@Cause1", Cause1)
                                Dim druri As SqlClient.SqlDataReader = cmduri.ExecuteReader
                                If druri.HasRows Then
                                    While druri.Read
                                        RowInfo(0) = druri("Info_ID").ToString
                                        RowInfo(1) = druri("Name")
                                        If druri("Result") IsNot DBNull.Value Then
                                            RowInfo(2) = druri("Result")
                                        Else
                                            RowInfo(2) = ""
                                        End If
                                        If druri("Flag") IsNot DBNull.Value Then
                                            RowInfo(4) = druri("Flag")
                                        Else
                                            RowInfo(4) = ""
                                        End If
                                        If druri("NormalRange") IsNot DBNull.Value Then
                                            RowInfo(5) = druri("NormalRange")
                                        Else
                                            RowInfo(5) = ""
                                        End If
                                        If druri("Released") Is DBNull.Value Then
                                            RowInfo(10) = "0"
                                        Else
                                            RowInfo(10) = Convert.ToInt16(druri("Released")).ToString
                                        End If
                                        RowInfo(11) = druri("Qualitative")
                                        RowInfo(12) = AccID
                                        RowInfo(13) = druri("Cause0")
                                        RowInfo(14) = druri("Cause1")
                                        RowInfo(15) = druri("Cause2")
                                        If druri("Comment") IsNot DBNull.Value Then
                                            RowInfo(16) = druri("Comment")
                                        Else
                                            RowInfo(16) = ""
                                        End If
                                        If druri("T_Result") IsNot DBNull.Value Then
                                            RowInfo(17) = druri("T_Result")
                                        Else
                                            RowInfo(17) = ""
                                        End If
                                        If druri("I_Result") IsNot DBNull.Value _
                                        AndAlso CType(druri("I_Result"), Byte()).Length > 0 Then
                                            RowInfo(18) = Convert.ToBase64String(druri("I_Result"))
                                        Else
                                            RowInfo(18) = Nothing
                                        End If
                                        RowInfo(19) = druri("ESig")
                                        '
                                        If (RowInfo(0) IsNot Nothing AndAlso RowInfo(0) <> "") AndAlso
                                        (RowInfo(12) IsNot Nothing AndAlso RowInfo(12) <> "") Then _
                                        CreateResultGridRow(RowInfo)
                                        '
                                        If RowInfo(10) <> "0" Then
                                            If ReleaserInfo(0, UBound(ReleaserInfo, 2)) IsNot Nothing _
                                            AndAlso ReleaserInfo(0, UBound(ReleaserInfo, 2)) <> "" _
                                            Then ReDim Preserve ReleaserInfo(UBound(ReleaserInfo, 1),
                                            UBound(ReleaserInfo, 2) + 1)
                                            ReleaserInfo(0, UBound(ReleaserInfo, 2)) = druri("Test_ID").ToString
                                            ReleaserInfo(1, UBound(ReleaserInfo, 2)) = druri("Released_By").ToString
                                            ReleaserInfo(2, UBound(ReleaserInfo, 2)) = druri("Release_Time").ToString
                                        End If
                                        ReDim RowInfo(19)
                                    End While
                                End If
                                cnuri.Close()
                                cnuri = Nothing
                                'Display Ref Reflexeds **************************
                                Dim cnrfd As New SqlClient.SqlConnection(connString)
                                cnrfd.Open()
                                Dim cmdrfd As New SqlClient.SqlCommand("DisplayREFResults_SP", cnrfd)
                                cmdrfd.CommandType = CommandType.StoredProcedure
                                cmdrfd.Parameters.AddWithValue("@AccID", AccID)
                                cmdrfd.Parameters.AddWithValue("@Cause1", Cause1)
                                Dim drrfd As SqlClient.SqlDataReader = cmdrfd.ExecuteReader
                                If drrfd.HasRows Then
                                    While drrfd.Read
                                        RowInfo(0) = drrfd("Test_ID").ToString
                                        Cause1 = drrfd("Test_ID").ToString
                                        RowInfo(1) = drrfd("Name")
                                        If drrfd("Result") IsNot DBNull.Value Then
                                            RowInfo(2) = drrfd("Result")
                                        Else
                                            RowInfo(2) = ""
                                        End If
                                        If drrfd("Flag") IsNot DBNull.Value Then
                                            RowInfo(4) = drrfd("Flag")
                                        Else
                                            RowInfo(4) = ""
                                        End If
                                        If drrfd("NormalRange") IsNot DBNull.Value Then
                                            RowInfo(5) = drrfd("NormalRange")
                                        Else
                                            RowInfo(5) = ""
                                        End If
                                        If drrfd("Released") Is DBNull.Value Then
                                            RowInfo(10) = "0"
                                        Else
                                            RowInfo(10) = Convert.ToInt16(drrfd("Released")).ToString
                                        End If
                                        RowInfo(11) = drrfd("Qualitative")
                                        RowInfo(12) = AccID
                                        RowInfo(13) = drrfd("Cause0")
                                        RowInfo(14) = drrfd("Cause1")
                                        RowInfo(15) = drrfd("Cause2")
                                        If drrfd("Comment") IsNot DBNull.Value Then
                                            RowInfo(16) = drrfd("Comment")
                                        Else
                                            RowInfo(16) = ""
                                        End If
                                        If drrfd("T_Result") IsNot DBNull.Value Then
                                            RowInfo(17) = drrfd("T_Result")
                                        Else
                                            RowInfo(17) = ""
                                        End If
                                        If drrfd("I_Result") IsNot DBNull.Value _
                                        AndAlso CType(drrfd("I_Result"), Byte()).Length > 0 Then
                                            RowInfo(18) = Convert.ToBase64String(drrfd("I_Result"))
                                        Else
                                            RowInfo(18) = Nothing
                                        End If
                                        RowInfo(19) = drrfd("ESig")
                                        '
                                        If (RowInfo(0) IsNot Nothing AndAlso RowInfo(0) <> "") AndAlso
                                        (RowInfo(12) IsNot Nothing AndAlso RowInfo(12) <> "") Then _
                                        CreateResultGridRow(RowInfo)
                                        '
                                        If RowInfo(10) <> "0" Then
                                            If ReleaserInfo(0, UBound(ReleaserInfo, 2)) IsNot Nothing _
                                            AndAlso ReleaserInfo(0, UBound(ReleaserInfo, 2)) <> "" _
                                            Then ReDim Preserve ReleaserInfo(UBound(ReleaserInfo, 1),
                                            UBound(ReleaserInfo, 2) + 1)
                                            ReleaserInfo(0, UBound(ReleaserInfo, 2)) = drrfd("Test_ID").ToString
                                            ReleaserInfo(1, UBound(ReleaserInfo, 2)) = drrfd("Released_By").ToString
                                            ReleaserInfo(2, UBound(ReleaserInfo, 2)) = drrfd("Release_Time").ToString
                                        End If
                                        ReDim RowInfo(19)
                                        'Display Ref Reflexed Infos **************************
                                        Dim cnrri As New SqlClient.SqlConnection(connString)
                                        cnrri.Open()
                                        Dim cmdrri As New SqlClient.SqlCommand("DisplayInfoResults_SP", cnrri)
                                        cmdrri.CommandType = CommandType.StoredProcedure
                                        cmdrri.Parameters.AddWithValue("@AccID", AccID)
                                        cmdrri.Parameters.AddWithValue("@Cause1", Cause1)
                                        Dim drrri As SqlClient.SqlDataReader = cmdrri.ExecuteReader
                                        If drrri.HasRows Then
                                            While drrri.Read
                                                RowInfo(0) = drrri("Info_ID").ToString
                                                RowInfo(1) = drrri("Name")
                                                If drrri("Result") IsNot DBNull.Value Then
                                                    RowInfo(2) = drrri("Result")
                                                Else
                                                    RowInfo(2) = ""
                                                End If
                                                If drrri("Flag") IsNot DBNull.Value Then
                                                    RowInfo(4) = drrri("Flag")
                                                Else
                                                    RowInfo(4) = ""
                                                End If
                                                If drrri("NormalRange") IsNot DBNull.Value Then
                                                    RowInfo(5) = drrri("NormalRange")
                                                Else
                                                    RowInfo(5) = ""
                                                End If
                                                If drrri("Released") Is DBNull.Value Then
                                                    RowInfo(10) = "0"
                                                Else
                                                    RowInfo(10) = Convert.ToInt16(drrri("Released")).ToString
                                                End If
                                                RowInfo(11) = drrri("Qualitative")
                                                RowInfo(12) = AccID
                                                RowInfo(13) = drrri("Cause0")
                                                RowInfo(14) = drrri("Cause1")
                                                RowInfo(15) = drrri("Cause2")
                                                If drrri("Comment") IsNot DBNull.Value Then
                                                    RowInfo(16) = drrri("Comment")
                                                Else
                                                    RowInfo(16) = ""
                                                End If
                                                If drrri("T_Result") IsNot DBNull.Value Then
                                                    RowInfo(17) = drrri("T_Result")
                                                Else
                                                    RowInfo(17) = ""
                                                End If
                                                If drrri("I_Result") IsNot DBNull.Value _
                                                AndAlso CType(drrri("I_Result"), Byte()).Length > 0 Then
                                                    RowInfo(18) = Convert.ToBase64String(drrri("I_Result"))
                                                Else
                                                    RowInfo(18) = Nothing
                                                End If
                                                RowInfo(19) = drrri("ESig")
                                                '
                                                If (RowInfo(0) IsNot Nothing AndAlso RowInfo(0) <> "") AndAlso
                                                (RowInfo(12) IsNot Nothing AndAlso RowInfo(12) <> "") Then _
                                                CreateResultGridRow(RowInfo)
                                                '
                                                If RowInfo(10) <> "0" Then
                                                    If ReleaserInfo(0, UBound(ReleaserInfo, 2)) IsNot Nothing _
                                                    AndAlso ReleaserInfo(0, UBound(ReleaserInfo, 2)) <> "" _
                                                    Then ReDim Preserve ReleaserInfo(UBound(ReleaserInfo, 1),
                                                    UBound(ReleaserInfo, 2) + 1)
                                                    ReleaserInfo(0, UBound(ReleaserInfo, 2)) = drrri("Test_ID").ToString
                                                    ReleaserInfo(1, UBound(ReleaserInfo, 2)) = drrri("Released_By").ToString
                                                    ReleaserInfo(2, UBound(ReleaserInfo, 2)) = drrri("Release_Time").ToString
                                                End If
                                                ReDim RowInfo(19)
                                            End While
                                        End If
                                        cnrri.Close()
                                        cnrri = Nothing
                                    End While
                                End If
                                cnrfd.Close()
                                cnrfd = Nothing
                            End While
                        End If
                        cnrfr.Close()
                        cnrfr = Nothing
                        'Display User Test Infos **************************
                        Dim cninf As New SqlClient.SqlConnection(connString)
                        cninf.Open()
                        Dim cmdinf As New SqlClient.SqlCommand("DisplayInfoResults_SP", cninf)
                        cmdinf.CommandType = CommandType.StoredProcedure
                        cmdinf.Parameters.AddWithValue("@AccID", AccID)
                        cmdinf.Parameters.AddWithValue("@Cause1", InfoCause)
                        Dim drinf As SqlClient.SqlDataReader = cmdinf.ExecuteReader
                        If drinf.HasRows Then
                            While drinf.Read
                                RowInfo(0) = drinf("Info_ID").ToString
                                RowInfo(1) = drinf("Name")
                                If drinf("Result") IsNot DBNull.Value Then
                                    RowInfo(2) = drinf("Result")
                                Else
                                    RowInfo(2) = ""
                                End If
                                If drinf("Flag") IsNot DBNull.Value Then
                                    RowInfo(4) = drinf("Flag")
                                Else
                                    RowInfo(4) = ""
                                End If
                                If drinf("NormalRange") IsNot DBNull.Value Then
                                    RowInfo(5) = drinf("NormalRange")
                                Else
                                    RowInfo(5) = ""
                                End If
                                If drinf("Released") Is DBNull.Value Then
                                    RowInfo(10) = "0"
                                Else
                                    RowInfo(10) = Convert.ToInt16(drinf("Released")).ToString
                                End If
                                RowInfo(11) = drinf("Qualitative")
                                RowInfo(12) = AccID
                                RowInfo(13) = drinf("Cause0")
                                RowInfo(14) = drinf("Cause1")
                                RowInfo(15) = drinf("Cause2")
                                If drinf("Comment") IsNot DBNull.Value Then
                                    RowInfo(16) = drinf("Comment")
                                Else
                                    RowInfo(16) = ""
                                End If
                                If drinf("T_Result") IsNot DBNull.Value Then
                                    RowInfo(17) = drinf("T_Result")
                                Else
                                    RowInfo(17) = ""
                                End If
                                If drinf("I_Result") IsNot DBNull.Value _
                                AndAlso CType(drinf("I_Result"), Byte()).Length > 0 Then
                                    RowInfo(18) = Convert.ToBase64String(drinf("I_Result"))
                                Else
                                    RowInfo(18) = Nothing
                                End If
                                RowInfo(19) = drinf("ESig")
                                '
                                If (RowInfo(0) IsNot Nothing AndAlso RowInfo(0) <> "") AndAlso
                                (RowInfo(12) IsNot Nothing AndAlso RowInfo(12) <> "") Then _
                                CreateResultGridRow(RowInfo)
                                '
                                If RowInfo(10) <> "0" Then
                                    If ReleaserInfo(0, UBound(ReleaserInfo, 2)) IsNot Nothing _
                                    AndAlso ReleaserInfo(0, UBound(ReleaserInfo, 2)) <> "" _
                                    Then ReDim Preserve ReleaserInfo(UBound(ReleaserInfo, 1),
                                    UBound(ReleaserInfo, 2) + 1)
                                    ReleaserInfo(0, UBound(ReleaserInfo, 2)) = drinf("Test_ID").ToString
                                    ReleaserInfo(1, UBound(ReleaserInfo, 2)) = drinf("Released_By").ToString
                                    ReleaserInfo(2, UBound(ReleaserInfo, 2)) = drinf("Release_Time").ToString
                                End If
                                ReDim RowInfo(19)
                            End While
                        End If
                        cninf.Close()
                        cninf = Nothing
                    Next i

                    '
                    txtNote.Text = ""
                    Dim COMDIRS() As String = GetAccComDir(AccID)
                    txtNote.Text = COMDIRS(0)
                    Dim ItemX As MyList
                    cmbDirector.SelectedIndex = -1
                    If COMDIRS(1) = "" Then
                        If cmbDirector.Items.Count > 0 Then cmbDirector.SelectedIndex = 0
                    Else
                        If cmbDirector.Items.Count > 0 Then
                            Dim dr As Integer
                            For dr = 0 To cmbDirector.Items.Count - 1
                                ItemX = cmbDirector.Items(dr)
                                If ItemX.ItemData = COMDIRS(1) Then
                                    cmbDirector.SelectedIndex = dr
                                    Exit For
                                End If
                            Next
                        End If
                        If cmbDirector.SelectedIndex = -1 Then  'Old
                            'If RptComp = True Then
                            ItemX = New MyList(getlabdirectorname(COMDIRS(1)), COMDIRS(1))
                            cmbDirector.Items.Add(ItemX)
                            cmbDirector.SelectedIndex = cmbDirector.Items.Count - 1
                            cmbDirector.Enabled = False
                            'End If
                        End If
                    End If
                    COMDIRS = Nothing
                    '
                    'If (RptComp = True AndAlso (ThisUser.Supervisor = True _
                    'Or ThisUser.Director = True)) OrElse (RptComp = False AndAlso _
                    '(ThisUser.Result_Entry = True And ThisUser.Result_Release = True)) Then 'Complete Report but power user

                    'End If

                    If SystemConfig.AuditTrail = True Then
                        If dgvResults.RowCount > 0 Then
                            ReDim ResultsAT(8, dgvResults.RowCount - 1)
                            For i = 0 To dgvResults.RowCount - 1
                                If dgvResults.Rows(i).Cells(13).Value = "ACC" Then
                                    ResultsAT(0, i) = dgvResults.Rows(i).Cells(0).Value.ToString
                                Else
                                    ResultsAT(0, i) = dgvResults.Rows(i).Cells(15).Value.ToString _
                                    & "-" & dgvResults.Rows(i).Cells(0).Value.ToString
                                End If
                                ResultsAT(1, i) = Trim(dgvResults.Rows(i).Cells(2).Value)
                                ResultsAT(2, i) = IIf(dgvResults.Rows(i).Cells(10).Value IsNot
                                System.DBNull.Value AndAlso dgvResults.Rows(i).Cells(10).Value =
                                True, "1", "0")
                                ResultsAT(3, i) = ""
                                ResultsAT(4, i) = ""
                                ResultsAT(5, i) = Trim(dgvResults.Rows(i).Cells(16).Value)
                                ResultsAT(6, i) = Trim(dgvResults.Rows(i).Cells(17).Value)
                                ResultsAT(7, i) = ""
                                ResultsAT(8, i) = ""
                            Next
                        End If
                    End If
                    '********** End Audit Trail ****************
                    'Catch Ex As Exception
                    '   MsgBox(Ex)
                    'End Try
                    stopWatch.Stop()
                    lblStatus.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    stopWatch.Elapsed.Hours, stopWatch.Elapsed.Minutes,
                    stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds / 10)
                End If
            End If


        Catch ex As Exception
            LoggerHelper.LogError(ex, "Error DisplayResults")
        End Try
    End Sub


    Private Function GetAccTestIDs(ByVal sSQL As String) As String()
        Dim AccTestIDS() As String = {""}
        Dim cnsql As New SqlClient.SqlConnection(connString)
        cnsql.Open()
        Dim cmdsql As New SqlClient.SqlCommand(sSQL, cnsql)
        cmdsql.CommandType = CommandType.Text
        Dim drsql As SqlClient.SqlDataReader = cmdsql.ExecuteReader
        If drsql.HasRows Then
            While drsql.Read
                If AccTestIDS(UBound(AccTestIDS)) <> "" Then _
                ReDim Preserve AccTestIDS(UBound(AccTestIDS) + 1)
                AccTestIDS(UBound(AccTestIDS)) = drsql("Test_ID").ToString
            End While
        End If
        cnsql.Close()
        cnsql = Nothing
        Return AccTestIDS
    End Function

    Private Function getlabdirectorname(ByVal DirID As Long) As String
        Dim DirName As String = ""
        Dim cnld As New SqlClient.SqlConnection(connString)
        cnld.Open()
        Dim cmdld As New SqlClient.SqlCommand("Select " &
        "* from Lab_Directors where ID = " & DirID, cnld)
        cmdld.CommandType = CommandType.Text
        Dim drld As SqlClient.SqlDataReader = cmdld.ExecuteReader
        If drld.HasRows Then
            While drld.Read
                If drld("Degree") Is DBNull.Value Then
                    DirName = drld("LastName") & ", " & drld("FirstName")
                Else
                    DirName = drld("LastName") & ", " & drld("FirstName") & " " & drld("Degree")
                End If
            End While
        End If
        cnld.Close()
        cnld = Nothing
        Return DirName
    End Function

    Private Function GetInfos(ByVal AccID As Long, ByVal TESTID As Integer) As String()
        Dim Infos() As String = {""}
        Dim sSQL As String = "Select Info_ID from Acc_Info_Results where " &
        "Accession_ID = " & AccID & " and Test_ID = " & TESTID
        Dim cngis As New SqlClient.SqlConnection(connString)
        cngis.Open()
        Dim cmdgis As New SqlClient.SqlCommand(sSQL, cngis)
        cmdgis.CommandType = CommandType.Text
        Dim drgis As SqlClient.SqlDataReader = cmdgis.ExecuteReader
        If drgis.HasRows Then
            While drgis.Read
                If Infos(UBound(Infos)) <> "" Then ReDim Preserve Infos(UBound(Infos) + 1)
                Infos(UBound(Infos)) = drgis("Info_ID").ToString
            End While
        End If
        cngis.Close()
        cngis = Nothing
        Return Infos
    End Function

    Private Function GetAccComDir(ByVal AccID As Long) As String()
        Dim Notes() As String = {"", ""}
        Dim sSQL As String = "Select * from Requisitions where ID = " & AccID
        Dim cnacd As New SqlClient.SqlConnection(connString)
        cnacd.Open()
        Dim cmdacd As New SqlClient.SqlCommand(sSQL, cnacd)
        cmdacd.CommandType = CommandType.Text
        Dim dracd As SqlClient.SqlDataReader = cmdacd.ExecuteReader
        If dracd.HasRows Then
            While dracd.Read
                If dracd("Comment") IsNot DBNull.Value Then
                    Notes(0) = dracd("Comment")
                End If
                If dracd("Director_ID") IsNot DBNull.Value Then
                    Notes(1) = dracd("Director_ID").ToString
                End If
            End While
        End If
        cnacd.Close()
        cnacd = Nothing
        Return Notes
    End Function

    Private Function HasReflux(ByVal AccID As Long, ByVal TrigID As Integer) As Boolean
        Dim Reflux As Boolean = False
        Dim sSQL As String = "Select * from Ref_Results where " &
        "Accession_ID = " & AccID & " and Reflexer_ID = " & TrigID
        Dim cnhr As New SqlClient.SqlConnection(connString)
        cnhr.Open()
        Dim cmdhr As New SqlClient.SqlCommand(sSQL, cnhr)
        cmdhr.CommandType = CommandType.Text
        Dim drhr As SqlClient.SqlDataReader = cmdhr.ExecuteReader
        If drhr.HasRows Then Reflux = True
        cnhr.Close()
        cnhr = Nothing
        Return Reflux
    End Function

    Private Sub DisplayControl(ByVal RunID As Long, ByVal ControlID As Integer)
        dgvResults.Rows.Clear()
        Dim Result As String = ""
        Dim Note As String = ""
        Dim RTFNote As String = ""
        Dim Flag As String = ""
        Dim UOM As String = ""
        Dim Released As Boolean = False
        Dim QCRange As String = ""
        '
        Dim cncc As New SqlClient.SqlConnection(connString)
        cncc.Open()
        Dim cmdcc As New SqlClient.SqlCommand("Select a.*, b.Name, b.Qualitative, c.Analysis_ID " &
        "from Runs c inner join (QC_Results a inner join Tests b on a.Test_ID = b.ID) on a.Run_ID = c.ID " &
        "where b.IsActive <> 0 and b.HasResult <> 0 and a.Run_ID = " &
        RunID & " and a.Control_ID = " & ControlID, cncc)
        cmdcc.CommandType = CommandType.Text
        Dim drcc As SqlClient.SqlDataReader = cmdcc.ExecuteReader
        If drcc.HasRows Then
            While drcc.Read
                If drcc("Qualitative") = 0 Then  'QN
                    QCRange = GetNQCRange(drcc("Analysis_ID"), ControlID, drcc("Test_ID"))
                Else
                    QCRange = GetLQCRange(drcc("Analysis_ID"), ControlID, drcc("Test_ID"))
                End If
                If drcc("Result") Is DBNull.Value Then
                    Result = ""
                Else
                    Result = drcc("Result")
                End If
                If drcc("Comment") Is DBNull.Value Then
                    Note = ""
                Else
                    Note = drcc("Comment")
                End If
                If drcc("Flag") Is DBNull.Value Then
                    Flag = ""
                Else
                    Flag = drcc("Flag")
                End If
                If drcc("UOM") Is DBNull.Value Then
                    UOM = ""
                Else
                    UOM = drcc("UOM")
                End If
                If drcc("T_Result") Is DBNull.Value Then
                    RTFNote = ""
                Else
                    RTFNote = drcc("T_Result")
                End If
                If drcc("Released") Is DBNull.Value Then
                    Released = False
                Else
                    Released = drcc("Released")
                End If
                'Note = "" : RTFNote = ""

                dgvResults.Rows.Add(drcc("Test_ID"), drcc("Name"),
                Result, Nothing, Flag, QCRange, Nothing, Nothing, Nothing, Nothing,
                Released, drcc("Qualitative"), ControlID, "QC", RunID, drcc("Analysis_ID"),
                Note, RTFNote)
                dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).Style.BackColor = Color.White
            End While
        End If
        cncc.Close()
        cncc = Nothing
        If dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value IsNot System.DBNull.Value _
        AndAlso dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value <> "" Then
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).Value =
            System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Note.ico")
        End If
        If dgvResults.Rows(dgvResults.RowCount - 1).Cells(17).Value IsNot System.DBNull.Value _
        AndAlso dgvResults.Rows(dgvResults.RowCount - 1).Cells(17).Value <> "" Then
            dgvResults.Rows(dgvResults.RowCount - 1).Cells(9).Value =
            System.Drawing.Image.FromFile(Application.StartupPath & "\Images\rtf.ico")
        End If
        'Catch Ex As Exception
        'MsgBox(Ex)
        'End Try
    End Sub

    Private Function AddComboChoices(ByVal cmbCell As DataGridViewComboBoxCell, ByVal TestID As Integer) As DataGridViewComboBoxCell
        cmbCell.Items.Clear()
        Dim cncc As New SqlClient.SqlConnection(connString)
        cncc.Open()
        Dim cmdcc As New SqlClient.SqlCommand("Select * from C_Ranges where Test_ID = " & TestID, cncc)
        cmdcc.CommandType = CommandType.Text
        Dim drcc As SqlClient.SqlDataReader = cmdcc.ExecuteReader
        If drcc.HasRows Then
            While drcc.Read
                cmbCell.Items.Add(drcc("Choice"))
            End While
        End If
        cncc.Close()
        cncc = Nothing
        AddComboChoices = cmbCell
    End Function

    Private Function GetNQCRange(ByVal AnaID As Integer, ByVal ControlID As Long,
    ByVal TestID As Integer) As String
        Dim NRange As String = ""
        Dim Pnts As String = "0"
        Dim cnrg As New SqlClient.SqlConnection(connString)
        cnrg.Open()
        Dim cmdrg As New SqlClient.SqlCommand("Select a.*, b.DecimalPlaces " &
        "from Ana_Ranges a inner join Tests b on a.Test_ID = b.ID where " &
        "a.Ana_ID = " & AnaID & " and a.Control_ID = " & ControlID &
        " and a.Test_ID = " & TestID, cnrg)
        cmdrg.CommandType = CommandType.Text
        Dim drrg As SqlClient.SqlDataReader = cmdrg.ExecuteReader
        If drrg.HasRows Then
            While drrg.Read
                If drrg("DecimalPlaces") = 1 Then
                    Pnts = "0.0"
                ElseIf drrg("DecimalPlaces") = 3 Then
                    Pnts = "0.000"
                ElseIf drrg("DecimalPlaces") = 4 Then
                    Pnts = "0.0000"
                Else
                    Pnts = "0.00"
                End If
                NRange = Format(drrg("Low"), Pnts) & " - " &
                Format(drrg("High"), Pnts)
            End While
        End If
        cnrg.Close()
        cnrg = Nothing
        Return NRange
    End Function

    Private Function GetLQCRange(ByVal AnaID As Integer, ByVal ControlID As Long,
    ByVal TestID As Integer) As String
        Dim LRange As String = ""
        Dim cnrg As New SqlClient.SqlConnection(connString)
        cnrg.Open()
        Dim cmdrg As New SqlClient.SqlCommand("Select * from " &
        "Ana_Ranges where Ana_ID = " & AnaID & " and Control_ID = " _
        & ControlID & " and Test_ID = " & TestID, cnrg)
        cmdrg.CommandType = CommandType.Text
        Dim drrg As SqlClient.SqlDataReader = cmdrg.ExecuteReader
        If drrg.HasRows Then
            While drrg.Read
                If Not drrg("MeanNormal") Is DBNull.Value Then _
                    LRange = CStr(drrg("MeanNormal"))
            End While
        End If
        cnrg.Close()
        cnrg = Nothing
        Return LRange
    End Function

    Private Function GetLQCFlag(ByVal AnaID As Integer, ByVal ControlID As Long,
    ByVal TestID As Integer, ByVal Result As String) As String
        Dim LFlag As String = ""
        Dim cnlf As New SqlClient.SqlConnection(connString)
        cnlf.Open()
        Dim cmdlf As New SqlClient.SqlCommand("Select * from " &
        "Ana_Ranges where Ana_ID = " & AnaID & " and Control_ID = " _
        & ControlID & " and Test_ID = " & TestID, cnlf)
        cmdlf.CommandType = CommandType.Text
        Dim drlf As SqlClient.SqlDataReader = cmdlf.ExecuteReader
        If drlf.HasRows Then
            While drlf.Read
                If drlf("MeanNormal") IsNot DBNull.Value Then
                    If Result = CStr(drlf("MeanNormal")) Then
                        LFlag = "IN"
                    Else
                        LFlag = "OUT"
                    End If
                Else
                    LFlag = ""
                End If
            End While
        End If
        cnlf.Close()
        cnlf = Nothing
        Return LFlag
    End Function

    Private Function GetNQCFlag(ByVal AnaID As Integer, ByVal ControlID As Long,
    ByVal TestID As Integer, ByVal Result As String) As String
        Dim NFlag As String = ""
        Dim cnnf As New SqlClient.SqlConnection(connString)
        cnnf.Open()
        Dim cmdnf As New SqlClient.SqlCommand("Select * from " &
        "Ana_Ranges where Ana_ID = " & AnaID & " and Control_ID = " _
        & ControlID & " and Test_ID = " & TestID, cnnf)
        cmdnf.CommandType = CommandType.Text
        Dim drnf As SqlClient.SqlDataReader = cmdnf.ExecuteReader
        If drnf.HasRows Then
            While drnf.Read
                If IsNumeric(Result) Then
                    If Result >= drnf("Low") And Result <= drnf("High") Then
                        NFlag = "IN"
                    Else
                        NFlag = "OUT"
                    End If
                Else
                    NFlag = ""
                End If
            End While
        End If
        cnnf.Close()
        cnnf = Nothing
        Return NFlag
    End Function

    Private Function GetPatient(ByVal AccID As Long) As String
        Dim PAT As String = ""
        If btnAccQC.Checked = False Then    'Accession
            Dim sSQL As String = "Select * from Patients where ID in (Select Patient_ID " &
            "from Requisitions where Received <> 0 and ID = " & AccID & ")"
            '
            Dim cngp As New SqlClient.SqlConnection(connString)
            cngp.Open()
            Dim cmdgp As New SqlClient.SqlCommand(sSQL, cngp)
            cmdgp.CommandType = CommandType.Text
            Dim drgp As SqlClient.SqlDataReader = cmdgp.ExecuteReader
            If drgp.HasRows Then
                While drgp.Read
                    PAT = drgp("LastName") & ", " & drgp("FirstName") _
                   & " ; DOB: " & Format(drgp("DOB"), SystemConfig.DateFormat) &
                   " ; Sex: " & Microsoft.VisualBasic.Left(Trim(drgp("Sex")), 1)
                End While
            End If
            cngp.Close()
            cngp = Nothing
        End If
        Return PAT
    End Function

    Private Function GetPatientAgeSex() As String()
        Dim AgeSex() As String = {"-1", ""}
        If btnAccQC.Checked = False Then    'Accession
            If cmbAccCtl.Text <> "" Then
                Dim sSQL As String = "Select a.DOB, a.Sex, b.AccessionDate from Patients a inner " &
                "join Requisitions b on a.Patient_ID = b.ID where b.ID = " & Val(cmbAccCtl.Text)
                If connString <> "" Then
                    Dim cnpas As New SqlClient.SqlConnection(connString)
                    cnpas.Open()
                    Dim cmdpas As New SqlClient.SqlCommand(sSQL, cnpas)
                    cmdpas.CommandType = CommandType.Text
                    Dim drpas As SqlClient.SqlDataReader = cmdpas.ExecuteReader
                    If drpas.HasRows Then
                        While drpas.Read
                            AgeSex(0) = DateDiff(DateInterval.Year, drpas("DOB"), drpas("AccessionDate"))
                            AgeSex(1) = Trim(drpas("Sex"))
                        End While
                    End If
                    cnpas.Close()
                    cnpas = Nothing
                End If
            End If
        End If
        Return AgeSex
    End Function

    Private Function CalculateResult(ByVal Test_ID As Integer, ByVal AccID As Long) As String
        Dim FinalRes As String = ""
        Try
            ' Use fully qualified name and late binding to avoid BC30002 error
            Dim SC As Object = CreateObject("MSScriptControl.ScriptControl")
            SC.Language = "VBScript"
            'SC.AllowUI = True
            Dim i As Integer
            Dim P1 As Integer
            Dim P2 As Integer
            Dim MyScript As String = ""
            Dim NoRes As Boolean = False
            Dim CompRes As String = ""
            Dim TestId As String
            Dim TPRMS() As String = GetTestParams(Test_ID)  '0 = formula, 1= decimalpoints
            Dim Formula As String = TPRMS(0)
            Dim Decs As String = "#0.00"
            If TPRMS(1) <> "" Then
                If Val(TPRMS(1)) = 0 Then
                    Decs = "#0"
                ElseIf Val(TPRMS(1)) = 1 Then
                    Decs = "#0.0"
                ElseIf Val(TPRMS(1)) = 3 Then
                    Decs = "#0.000"
                ElseIf Val(TPRMS(1)) = 4 Then
                    Decs = "#0.0000"
                Else
                    Decs = "#0.00"
                End If
            End If
            If Formula <> "" Then
                Do Until InStr(Formula, "}") = 0
                    P1 = InStr(Formula, "{")
                    P2 = InStr(Formula, "}")
                    TestId = Formula.Substring(P1, P2 - (P1 + 1))
                    For i = 0 To dgvResults.RowCount - 1
                        If dgvResults.Rows(i).Cells(0).Value = Val(TestId) Then
                            CompRes = dgvResults.Rows(i).Cells(2).Value
                            Exit For
                        End If
                    Next
                    If Not CompRes Is Nothing AndAlso CompRes <> "" Then
                        CompRes = Replace(CompRes, ">", "")
                        CompRes = Replace(CompRes, "<", "")
                        CompRes = Replace(CompRes, ">=", "")
                        CompRes = Replace(CompRes, "<=", "")
                        Formula = Replace(Formula, "{" & TestId & "}", CompRes)
                    Else
                        NoRes = True
                        FinalRes = ""
                        Exit Do
                    End If
                Loop
                '
                If NoRes = False Then
                    If InStr(Formula, "@Age@") > 0 Then     'Age is used
                        Dim Age() As String = GetPatientAge(AccID)
                        If Age(0) = "0" Then Age(0) = "1"
                        Formula = Replace(Formula, "@Age@", Val(Age(0)))
                        If InStr(Formula, "@Sex@") > 0 Then Formula = Replace(Formula, "@Sex@", Val(Age(1)))
                        If InStr(Formula, "@Ethnicity@") > 0 Then Formula = Replace(Formula, "@Ethnicity@", Val(Age(2)))
                    End If
                    If InStr(Formula, "}") = 0 And InStr(Formula, "@") = 0 Then     'Formula resolved
                        Do Until InStr(Formula, "IIF(") = 0
                            If InStr(Formula, "IIF(") > 0 Then
                                Dim sTEMP = Microsoft.VisualBasic.Mid(Formula,
                                InStr(Formula, "IIF("))
                                sTEMP = Microsoft.VisualBasic.Mid(sTEMP, 1, InStr(sTEMP, ")"))
                                Dim Expr As String = Microsoft.VisualBasic.Mid(sTEMP,
                                InStr(sTEMP, "IIF(") + 4)
                                Expr = Microsoft.VisualBasic.Mid(Expr, 1, InStr(Expr, ",") - 1)
                                Dim truePart As String = Trim(Microsoft.VisualBasic.Mid(sTEMP,
                                InStr(sTEMP, ",") + 1))
                                Dim falsePart As String = Microsoft.VisualBasic.Mid(truePart,
                                InStr(truePart, ",") + 1)
                                falsePart = Trim(Microsoft.VisualBasic.Mid(falsePart, 1,
                                InStr(falsePart, ")") - 1))
                                truePart = Microsoft.VisualBasic.Mid(truePart, 1,
                                InStr(truePart, ",") - 1)
                                '
                                Dim Myval As String = IIf(Expr = 0, truePart, falsePart)
                                Formula = Replace(Formula, sTEMP, Myval)
                            End If
                        Loop
                        '
                        Do Until InStr(Formula, "Min(") = 0
                            If InStr(Formula, "Min(") > 0 Then
                                Dim sTEMP = Microsoft.VisualBasic.Mid(Formula,
                                InStr(Formula, "Min("))
                                sTEMP = Microsoft.VisualBasic.Mid(sTEMP, 1, InStr(sTEMP, ")"))
                                Dim Expr As String = Microsoft.VisualBasic.Mid(sTEMP,
                                InStr(sTEMP, "Min(") + 4)
                                Expr = Microsoft.VisualBasic.Mid(Expr, 1, InStr(Expr, ")") - 1)
                                Dim Params() As String = Split(Expr, ",")
                                Dim MyVal As String = Math.Min(Val(Params(0)), Val(Params(1)))
                                Formula = Replace(Formula, sTEMP, MyVal)
                                sTEMP = "" : MyVal = ""
                            End If
                        Loop
                        '
                        Do Until InStr(Formula, "Max(") = 0
                            If InStr(Formula, "Max(") > 0 Then
                                Dim sTEMP = Microsoft.VisualBasic.Mid(Formula,
                                InStr(Formula, "Max("))
                                sTEMP = Microsoft.VisualBasic.Mid(sTEMP, 1, InStr(sTEMP, ")"))
                                Dim Expr As String = Microsoft.VisualBasic.Mid(sTEMP,
                                InStr(sTEMP, "Max(") + 4)
                                Expr = Microsoft.VisualBasic.Mid(Expr, 1, InStr(Expr, ")") - 1)
                                Dim Params() As String = Split(Expr, ",")
                                Dim MyVal As String = Math.Max(Val(Params(0)), Val(Params(1)))
                                Formula = Replace(Formula, sTEMP, MyVal)
                                sTEMP = "" : MyVal = ""
                            End If
                        Loop
                        '
                        Try
                            FinalRes = Format(SC.Eval(Formula), Decs)
                        Catch ex As Exception
                        End Try
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return FinalRes
    End Function

    Private Function GetPatientAge(ByVal AccID As Long) As String()
        Dim Age() As String = {"", "", ""}
        Dim cnasr As New SqlClient.SqlConnection(connString)
        cnasr.Open()
        Dim cmdasr As New SqlClient.SqlCommand("Select a.AccessionDate " &
        "as AccDate, b.DOB as DOB, b.Sex as Sex, b.Ethnicity as Ehnicity " &
        "from Requisitions a inner join Patients b on a.Patient_ID = b.ID " &
        "where a.ID = " & AccID, cnasr)
        cmdasr.CommandType = CommandType.Text
        Dim drasr As SqlClient.SqlDataReader = cmdasr.ExecuteReader
        If drasr.HasRows Then
            While drasr.Read
                If drasr("DOB") IsNot DBNull.Value Then
                    Age(0) = DateDiff(DateInterval.Year, drasr("DOB"), drasr("AccDate"))
                    If drasr("Sex") = "F" Then
                        Age(1) = "0"
                    Else
                        Age(1) = "1"
                    End If
                    If drasr("Ehnicity") IsNot DBNull.Value Then
                        If ("Ehnicity") = "Black" Then
                            Age(2) = "1"
                        Else
                            Age(2) = "0"
                        End If
                    End If
                End If
            End While
        Else
            Age(0) = "0" : Age(1) = "0" : Age(2) = "0"
        End If
        cnasr.Close()
        cnasr = Nothing
        Return Age
    End Function

    Private Function GetRefCleaner(ByVal AccID As Long, ByVal TGPID As Integer) As String
        Dim RefCleaner As String = ""
        Dim cnrc As New SqlClient.SqlConnection(connString)
        cnrc.Open()
        Dim cmdrc As New SqlClient.SqlCommand("Select * " &
        "from Ref_Results where Reflexer_ID = " & TGPID &
        " and Accession_ID = " & AccID, cnrc)
        cmdrc.CommandType = CommandType.Text
        Dim drrc As SqlClient.SqlDataReader = cmdrc.ExecuteReader
        If drrc.HasRows Then
            While drrc.Read
                RefCleaner += AccID.ToString & "^" & TGPID.ToString & "^" &
                drrc("Reflexed_ID").ToString & "^Remove" & "|"
            End While
        End If
        cnrc.Close()
        cnrc = Nothing
        If RefCleaner.EndsWith("|") Then RefCleaner = RefCleaner.Substring(0, Len(RefCleaner) - 1)
        Return RefCleaner
    End Function


    Private Sub ProcessCMDTrigger(ByVal NewVals As String, ByVal AccRef As String)
        Dim CMDS() As String = Split(NewVals, "|")
        Dim Comps() As String
        Dim AccID As Long
        Dim TriggerID As Integer
        Dim TGPType As String = ""
        Dim Ordinal As Integer
        Dim AccOrd As Integer
        Dim TGPID As Integer
        Dim ACT As String = ""
        Dim BadTests As String = ""
        Dim BadTriggers As String = ""
        Dim FB() As String = {"", ""}
        Dim sSQL As String = ""
        For i As Integer = 0 To CMDS.Length - 1
            If CMDS(i) <> "" Then
                Comps = Split(CMDS(i), "^")
                AccID = Comps(0)
                TriggerID = Comps(1)
                TGPID = IIf(Comps(2) = "", -1, Comps(2))
                ACT = Comps(3)
                AccOrd = GetTriggerOrdinal(AccID, TriggerID)
                If Ordinal < AccOrd Then Ordinal = AccOrd
                If ACT = "Add" Then
                    If Reflexable(AccID, TGPID, TriggerID) Then
                        TGPType = GetTGPType(TGPID)
                        If TGPType = "T" Then
                            sSQL = "If not exists (Select * from Ref_Results where Accession_ID = " & AccID & " and " &
                            "Reflexer_ID = " & TriggerID & " and Reflexed_ID = " & TGPID & " and Test_ID = " & TGPID &
                            ") and not exists (Select * from Acc_Results where Accession_ID = " & AccID & " and " &
                            "Test_ID = " & TGPID & ") Insert into Ref_Results(Accession_ID, Reflexer_ID, Reflexed_ID, " &
                            "Test_ID, Ordinal, Result, Flag, NormalRange) values (" & AccID & ", " & TriggerID & ", " &
                            TGPID & ", " & TGPID & ", " & Ordinal + 1 & ", '', '', '" & GetNormalRange(AccID, TGPID) & "')"
                            ExecuteSqlProcedure(sSQL)
                            '
                            Dim cnch1 As New SqlClient.SqlConnection(connString)
                            cnch1.Open()
                            Dim cmdch1 As New SqlClient.SqlCommand("Select * from TGP_Info " &
                            "where TGP_ID = " & TGPID & " order by Ordinal", cnch1)
                            cmdch1.CommandType = CommandType.Text
                            Dim drch1 As SqlClient.SqlDataReader = cmdch1.ExecuteReader
                            If drch1.HasRows Then
                                While drch1.Read
                                    sSQL = "If not exists (Select * from Acc_Info_Results where Accession_ID = " & AccID & " and " &
                                    "Test_ID = " & TGPID & " and Info_ID = " & drch1("Info_ID") & ") Insert into Acc_Info_Results(" &
                                    "Accession_ID, Test_ID, Info_ID, Ordinal, Result, Flag, NormalRange) values (" & AccID & ", " & TGPID &
                                    ", " & drch1("Info_ID") & ", " & drch1("Ordinal") & ", '', '', '" & GetNormalRange(AccID, TGPID) & "')"
                                    ExecuteSqlProcedure(sSQL)
                                End While
                            End If
                            cnch1.Close()
                            cnch1 = Nothing
                            '
                        Else
                            Dim cng As New SqlClient.SqlConnection(connString)
                            cng.Open()
                            Dim cmdg As New SqlClient.SqlCommand("Select * From Group_Test " &
                            "where Group_ID = " & TGPID & " order by Ordinal", cng)
                            cmdg.CommandType = CommandType.Text
                            Dim drg As SqlClient.SqlDataReader = cmdg.ExecuteReader
                            If drg.HasRows Then
                                Ordinal = 1
                                While drg.Read
                                    sSQL = "If not exists (Select * from Ref_Results where Accession_ID = " & AccID & " and " &
                                    "Reflexer_ID = " & TriggerID & " and Reflexed_ID = " & TGPID & " and Test_ID = " & drg("Test_ID") &
                                    ") and not exists (Select * from Acc_Results where Accession_ID = " & AccID & " and Test_ID = " &
                                    TGPID & ")Insert into Ref_Results(" & "Accession_ID, Reflexer_ID, Reflexed_ID, Test_ID, Ordinal, " &
                                    "Result, Flag, NormalRange) values (" & AccID & ", " & TriggerID & ", " & TGPID & ", " &
                                    drg("Test_ID") & ", " & Ordinal & ", '', '', '" & GetNormalRange(AccID, drg("Test_ID")) & "')"
                                    ExecuteSqlProcedure(sSQL)
                                    Ordinal += 1
                                    '
                                    Dim cnch2 As New SqlClient.SqlConnection(connString)
                                    cnch2.Open()
                                    Dim cmdch2 As New SqlClient.SqlCommand("Select * from TGP_Info " &
                                    "where TGP_ID = " & drg("Test_ID") & " order by Ordinal", cnch2)
                                    cmdch2.CommandType = CommandType.Text
                                    Dim drch2 As SqlClient.SqlDataReader = cmdch2.ExecuteReader
                                    If drch2.HasRows Then
                                        While drch2.Read
                                            sSQL = "If not exists (Select * from Acc_Info_Results where Accession_ID = " & AccID &
                                            " and Test_ID = " & drg("Test_ID") & " and Info_ID = " & drch2("Info_ID") & ") Insert " &
                                            "into Acc_Info_Results(Accession_ID, Test_ID, Info_ID, Ordinal, Result, Flag, NormalRange) " &
                                            "values (" & AccID & ", " & drg("Test_ID") & ", " & drch2("Info_ID") & ", " &
                                            drch2("Ordinal") & ", '', '', '" & GetNormalRange(AccID, drg("Test_ID")) & "')"
                                            ExecuteSqlProcedure(sSQL)
                                        End While
                                    End If
                                    cnch2.Close()
                                    cnch2 = Nothing
                                    '
                                End While
                            End If
                            cng.Close()
                            cng = Nothing
                        End If
                    End If
                ElseIf ACT = "Remove" Then
                    ExecuteSqlProcedure("Delete from Acc_Info_Results where Accession_ID = " & AccID & " and Test_ID = " & TGPID)
                    ExecuteSqlProcedure("Delete from Acc_Info_Results where Accession_ID = " & AccID & " and Test_ID in (" &
                    "Select Test_ID from Group_Test where Group_ID = " & TGPID & ")")
                    '
                    ExecuteSqlProcedure("Delete from Ref_Results where Accession_ID = " & AccID &
                    " and Reflexer_ID = " & TGPID)
                    ExecuteSqlProcedure("Delete from Ref_Results where Accession_ID = " & AccID &
                    " and Reflexer_ID = " & TriggerID & " and Reflexed_ID = " & TGPID)
                End If
            End If
        Next
    End Sub

    Private Function GetTriggerOrdinal(ByVal AccID As Long, ByVal TrigID As Integer) As Integer
        Dim TRID As Integer = 0
        Dim cno As New SqlClient.SqlConnection(connString)
        cno.Open()
        Dim cmdo As New SqlClient.SqlCommand("If Exists (Select * from " &
        "Ref_Results where Accession_ID = " & AccID & " and Reflexer_ID = " &
        TrigID & ") Select Max(Ordinal) as MaxOrd from Ref_Results where " &
        "Accession_ID = " & AccID & " and Reflexer_ID = " & TrigID &
        " Else Select Max(Ordinal) as MaxOrd from Acc_Results where " &
        "Accession_ID = " & AccID & " and Test_ID = " & TrigID, cno)
        cmdo.CommandType = CommandType.Text
        Dim dro As SqlClient.SqlDataReader = cmdo.ExecuteReader
        If dro.HasRows Then
            While dro.Read
                If dro("MaxOrd") IsNot DBNull.Value Then
                    If dro("MaxOrd") > TRID Then TRID = dro("MaxOrd")
                End If
            End While
        End If
        cno.Close()
        cno = Nothing
        Return TRID
    End Function

    Private Sub btnRelease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRelease.Click
        If dgvResults.RowCount > 0 Then
            Dim IMG As Byte()
            Dim FB() As String = {"", ""}
            txtRptDate.Text = Format(Date.Today, SystemConfig.DateFormat)
            txtRptTime.Text = Format(Date.Now, "HH:mm")
            For i As Integer = 0 To dgvResults.RowCount - 1
                If (dgvResults.Rows(i).Cells(2).Value IsNot Nothing AndAlso
                Trim(dgvResults.Rows(i).Cells(2).Value.ToString) <> "") AndAlso
                ((dgvResults.Rows(i).Cells(20).Value <> "Panic") _
                OrElse (dgvResults.Rows(i).Cells(20).Value = "Panic" _
                And dgvResults.Rows(i).Cells(16).Value <> "")) Then
                    dgvResults.Rows(i).Cells(10).Value = True

                    'If dgvResults.Rows(i).Cells(17).Value.ToString().Contains(Wildcard.Text) Then
                    '    dgvResults.Rows(i).Cells(4).Value = ""
                    'End If

                    If btnAccQC.Checked = False Then    'ACC
                        If dgvResults.Rows(i).Cells(18).Value IsNot Nothing Then
                            Try
                                IMG = Img_To_Array(dgvResults.Rows(i).Cells(18).Value,
                                                           CType(dgvResults.Rows(i).Cells(18).Value, Image).RawFormat)
                            Catch ex As Exception
                                Try
                                    IMG = BitmapToBytes(dgvResults.Rows(i).Cells(18).Value)

                                Catch exw As Exception

                                End Try
                            End Try

                        Else
                            IMG = Nothing
                        End If
                        FB(0) = dgvResults.Rows(i).Cells(4).Value
                        FB(1) = dgvResults.Rows(i).Cells(20).Value

                        If dgvResults.Rows(i).Cells(17).Value.ToString().Contains(Wildcard.Text) Then
                            dgvResults.Rows(i).Cells(4).Value = ""
                            FB(1) = "Ignore"

                        End If

                        SaveResultChange(Val(dgvResults.Rows(i).Cells(12).Value),
                        dgvResults.Rows(i).Cells(13).Value, dgvResults.Rows(i).Cells(14).Value,
                        dgvResults.Rows(i).Cells(15).Value, dgvResults.Rows(i).Cells(0).Value,
                        origRes, dgvResults.Rows(i).Cells(2).Value, FB(0), FB(1),
                        dgvResults.Rows(i).Cells(16).Value, dgvResults.Rows(i).Cells(17).Value,
                        IMG, dgvResults.Rows(i).Cells(10).Value, CDate(txtRptDate.Text & " " & txtRptTime.Text), i)
                        AccDirty = True
                    Else    'QC
                        SaveQCResultChange(Val(dgvResults.Rows(i).Cells(14).Value),
                        dgvResults.Rows(i).Cells(12).Value, dgvResults.Rows(i).Cells(0).Value,
                        dgvResults.Rows(i).Cells(2).Value, dgvResults.Rows(i).Cells(4).Value,
                        dgvResults.Rows(i).Cells(16).Value, dgvResults.Rows(i).Cells(17).Value,
                        dgvResults.Rows(i).Cells(10).Value, CDate(txtRptDate.Text & " " & txtRptTime.Text))
                    End If
                Else
                    dgvResults.Rows(i).Cells(10).Value = False
                End If
            Next
            If ExtPDF IsNot Nothing And btnAccQC.Checked = False Then
                SaveExtResultChange(dgvResults.Rows(0).Cells(12).Value,
                ExtPDF, True, CDate(txtRptDate.Text & " " & txtRptTime.Text))
                chkExtRelease.Checked = True
                AccDirty = True
            End If
            '
            If AccDirty Then
                If Not IsDate(txtRptDate.Text & " " & txtRptTime.Text) Then
                    txtRptDate.Text = Format(Date.Today, SystemConfig.DateFormat)
                    txtRptTime.Text = Format(Date.Now, "HH:mm")
                End If
                'UpdateReportTime(dgvResults.Rows(0).Cells(12).Value, CDate(txtRptDate.Text & " " & txtRptTime.Text))
                'lblRPTStatus.Text = GetReportStatus(Val(dgvResults.Rows(0).Cells(12).Value))
                'AccDirty = False
            End If
        End If
    End Sub

    Private Sub btnBlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBlock.Click
        If dgvResults.RowCount > 0 Then
            Dim i As Integer
            Dim IMG As Byte()
            Dim FB() As String = {"", ""}
            txtRptDate.Text = Format(Date.Today, SystemConfig.DateFormat)
            txtRptTime.Text = Format(Date.Now, "HH:mm")
            For i = 0 To dgvResults.RowCount - 1
                If dgvResults.Rows(i).Cells(10).Value IsNot System.DBNull.Value _
                AndAlso dgvResults.Rows(i).Cells(10).Value = True Then
                    dgvResults.Rows(i).Cells(10).Value = False
                    If btnAccQC.Checked = False Then    'ACC
                        If dgvResults.Rows(i).Cells(18).Value IsNot Nothing Then
                            Try
                                IMG = Img_To_Array(dgvResults.Rows(i).Cells(18).Value,
                                                           CType(dgvResults.Rows(i).Cells(18).Value, Image).RawFormat)
                            Catch ex As Exception
                                Try
                                    IMG = BitmapToBytes(dgvResults.Rows(i).Cells(18).Value)
                                Catch exw As Exception

                                End Try
                            End Try

                        Else
                            IMG = Nothing
                        End If
                        FB(0) = dgvResults.Rows(i).Cells(4).Value




                        If dgvResults.Rows(i).Cells(4).Style.BackColor = Color.Yellow Then
                            FB(1) = "Caution"
                        ElseIf dgvResults.Rows(i).Cells(4).Style.BackColor = Color.Red Then
                            FB(1) = "Panic"
                        Else
                            FB(1) = "Ignore"
                        End If
                        If dgvResults.Rows(i).Cells(17).Value.ToString().Contains(Wildcard.Text) Then
                            dgvResults.Rows(i).Cells(4).Value = ""
                            FB(1) = "Ignore"

                        End If

                        SaveResultChange(Val(dgvResults.Rows(i).Cells(12).Value),
                        dgvResults.Rows(i).Cells(13).Value, dgvResults.Rows(i).Cells(14).Value,
                        dgvResults.Rows(i).Cells(15).Value, dgvResults.Rows(i).Cells(0).Value,
                        origRes, dgvResults.Rows(i).Cells(2).Value, FB(0), FB(1),
                        dgvResults.Rows(i).Cells(16).Value, dgvResults.Rows(i).Cells(17).Value,
                        IMG, dgvResults.Rows(i).Cells(10).Value, CDate(txtRptDate.Text & " " & txtRptTime.Text), i)
                        AccDirty = True
                    Else    'QC
                        SaveQCResultChange(Val(dgvResults.Rows(i).Cells(14).Value),
                        dgvResults.Rows(i).Cells(12).Value, dgvResults.Rows(i).Cells(0).Value,
                        dgvResults.Rows(i).Cells(2).Value, dgvResults.Rows(i).Cells(4).Value,
                        dgvResults.Rows(i).Cells(16).Value, dgvResults.Rows(i).Cells(17).Value,
                        dgvResults.Rows(i).Cells(10).Value, CDate(txtRptDate.Text & " " & txtRptTime.Text))
                    End If
                End If
            Next
            chkExtRelease.Checked = False
            'If AccDirty Then
            '    UpdateReportTime(dgvResults.Rows(0).Cells(12).Value, CDate(txtRptDate.Text & " " & txtRptTime.Text))
            '    lblRPTStatus.Text = GetReportStatus(Val(dgvResults.Rows(0).Cells(12).Value))
            '    AccDirty = False
            'End If
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Function GetMarked(ByVal TestID As Integer) As Integer
        Dim TID As Integer = -1
        Dim sSQL As String = ""
        If IsQualitative(TestID) Then
            sSQL = "Select Marked_ID from C_Triggers where Test_ID = " & TestID
        Else
            sSQL = "Select Marked_ID from N_Triggers where Test_ID = " & TestID
        End If
        Dim cngpr As New SqlClient.SqlConnection(connString)
        cngpr.Open()
        Dim cmdgpr As New SqlClient.SqlCommand(sSQL, cngpr)
        cmdgpr.CommandType = CommandType.Text
        Dim drgpr As SqlClient.SqlDataReader = cmdgpr.ExecuteReader
        If drgpr.HasRows Then
            While drgpr.Read
                TID = drgpr("Marked_ID")
            End While
        End If
        cngpr.Close()
        cngpr = Nothing
        Return TID
    End Function

    Private Function GetPreviousAccID(ByVal AccID As String, ByVal WID As _
    String, ByVal Incomplete As Boolean, ByVal AccFromDate As Date, ByVal AccToDate As Date) As String
        Dim NewAccID As String = ""
        Dim sSQL As String = ""
        If AccID <> "" AndAlso IsNumeric(AccID) Then
            If WID = "" Then
                If Incomplete = False Then  'All
                    sSQL = "Select ID as AccID from Requisitions where " &
                    "Received <> 0 and ID < " & Val(AccID) & " order by ID DESC"
                Else    'Incomplete
                    sSQL = "Select ID as AccID from Requisitions where Received <> 0 and " &
                    "IsDate(Reported_Final) = 0 and ID < " & Val(AccID) & " order by ID DESC"
                End If
            Else    'Worksheet filter
                If Incomplete = False Then  'All
                    sSQL = "Select ID as AccID from Requisitions where Received <> 0 and ID in (Select " &
                    "a.Accession_ID from Acc_Results a inner join Worksheet_Test b on a.Test_ID = b.Test_ID " &
                    "where b.Worksheet_ID = " & Val(WID) & ") and ID < " & Val(AccID) & " order by ID DESC"
                Else    'Incomplete
                    sSQL = "Select ID as AccID from Requisitions where Received <> 0 and IsDate(Reported_Final) = 0 " &
                    "and ID in (Select a.Accession_ID from Acc_Results a inner join Worksheet_Test b on a.Test_ID = " &
                    "b.Test_ID where b.Worksheet_ID = " & Val(WID) & ") and ID < " & Val(AccID) & " order by ID DESC"
                End If
            End If
        Else
            If WID = "" Then
                If Incomplete = False Then  'All
                    'sSQL = "Select min(ID) as AccID from Requisitions where Received <> 0 and " &
                    '"AccessionDate between '" & Format(AccDate, SystemConfig.DateFormat) & "' " &
                    '"and '" & Format(AccDate, SystemConfig.DateFormat) & " 23:59:00'"

                    sSQL = $"Select min(ID) as AccID from Requisitions where Received <> 0 and AccessionDate between '{AccFromDate}' and '{AccToDate}'"

                Else    'Incomplete
                    'sSQL = "Select min(ID) as AccID from Requisitions where Received <> 0 and IsDate(Reported_Final) = 0 and AccessionDate " &
                    '"between '" & Format(AccFromDate, SystemConfig.DateFormat) & "' and '" & Format(AccFromDate, SystemConfig.DateFormat) & " 23:59:00'"
                    sSQL = $"Select min(ID) as AccID from Requisitions where Received <> 0 and IsDate(Reported_Final) = 0 and AccessionDate 
                             between '{AccFromDate}' and '{AccToDate}'"
                End If
            Else
                If Incomplete = False Then  'All
                    'sSQL = "Select min(ID) as AccID from Requisitions where Received <> 0 and AccessionDate between '" &
                    'Format(AccFromDate, SystemConfig.DateFormat) & "' and '" & Format(AccFromDate, SystemConfig.DateFormat) &
                    '" 23:59:00' and ID in (Select distinct Accession_ID from Acc_Results where Test_ID in " &
                    '"(Select Test_ID from Worksheet_Test where Worksheet_ID = " & Val(WID) & "))"

                    sSQL = $"Select min(ID) as AccID from Requisitions where Received <> 0 and AccessionDate between '{AccFromDate}' and '{AccToDate}'
                            and ID in (Select distinct Accession_ID from Acc_Results where Test_ID in 
                            (Select Test_ID from Worksheet_Test where Worksheet_ID = " & Val(WID) & "))"

                Else    'Incomplete
                    'sSQL = "Select min(ID) as AccID from Requisitions where Received <> 0 and IsDate(Reported_Final) = 0 " &
                    '"and AccessionDate between '" & Format(AccFromDate, SystemConfig.DateFormat) & "' and '" & Format(AccFromDate,
                    'SystemConfig.DateFormat) & " 23:59:00' and ID in (Select distinct Accession_ID from Acc_Results where " &
                    '"Test_ID in (Select Test_ID from Worksheet_Test where Worksheet_ID = " & Val(WID) & "))"

                    sSQL = $"Select min(ID) as AccID from Requisitions where Received <> 0 and IsDate(Reported_Final) = 0  
                             and AccessionDate between '{AccFromDate}' and '{AccToDate}' and ID in (Select distinct Accession_ID from Acc_Results where  
                             Test_ID in (Select Test_ID from Worksheet_Test where Worksheet_ID = " & Val(WID) & "))"
                End If
            End If
        End If
        Dim cnpa As New SqlClient.SqlConnection(connString)
        cnpa.Open()
        Dim cmdpa As New SqlClient.SqlCommand(sSQL, cnpa)
        cmdpa.CommandType = CommandType.Text
        Dim drpa As SqlClient.SqlDataReader = cmdpa.ExecuteReader
        If drpa.HasRows Then
            While drpa.Read
                If drpa("AccID") IsNot DBNull.Value Then
                    NewAccID = drpa("AccID").ToString
                    Exit While
                End If
            End While
        End If
        cnpa.Close()
        cnpa = Nothing
        Return NewAccID
    End Function

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Alert.Text = ""
        If btnAccQC.Checked = False Then    'Acc
            DisableCommands()
            Try
                If cmbAccCtl.Text <> "" Then
                    Dim Accid As String = Trim(cmbAccCtl.Text)
                    If AccDirty = True And dgvResults.RowCount > 0 Then _
                    SaveAccChanges(dgvResults.Rows(0).Cells(12).Value)
                    Dim ItemX As MyList
                    Dim WID As String = ""
                    If cmbRun.SelectedIndex <> -1 Then
                        ItemX = cmbRun.SelectedItem
                        WID = ItemX.ItemData.ToString
                    End If
                    ClearForm()
                    lblStatus.Text = ""
                    Dim stopWatch As New Stopwatch()
                    stopWatch.Start()
                    '
                    DisplayAccession(Val(Accid), WID)
                    'DisplayResults(Val(Accid), WID)
                    DisplayExtendResult(Accid)
                    UpdateAccReleaseStatus(Val(Accid))
                    lblRPTStatus.Text = GetReportStatus(Val(Accid))
                    '
                    stopWatch.Stop()
                    lblStatus.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    stopWatch.Elapsed.Hours, stopWatch.Elapsed.Minutes,
                    stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds / 10)
                    My.Application.DoEvents()
                    '
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            EnableCommands()
        Else                                'QC
            If cmbRun.SelectedIndex <> -1 And cmbAccCtl.SelectedIndex <> -1 Then
                If dgvResults.RowCount > 0 Then
                    If QCDirty = True Then SaveQCChanges()
                    Dim ItemR As MyList = cmbRun.SelectedItem
                    Dim ItemQ As MyList = cmbAccCtl.SelectedItem
                    DisplayControl(ItemR.ItemData, ItemQ.ItemData)
                    UpdateCtlReleaseStatus(ItemR.ItemData, ItemQ.ItemData)
                End If
            End If
        End If
    End Sub

    Private Sub DisableCommands()
        btnLoad.Enabled = False
        btnPrev.Enabled = False
        btnRefresh.Enabled = False
        txtNote.Enabled = False
        txtRptDate.Enabled = False
        txtRptTime.Enabled = False
        My.Application.DoEvents()
    End Sub

    Private Sub EnableCommands()
        btnLoad.Enabled = True
        btnPrev.Enabled = True
        btnRefresh.Enabled = True
        txtNote.Enabled = True
        txtRptDate.Enabled = True
        txtRptTime.Enabled = True
        My.Application.DoEvents()
    End Sub

    Public Shared dataList As New List(Of String)

    Public Shared Sub loadD()
        Dim filePath As String = "star.csv"
        If Not System.IO.File.Exists(filePath) Then
            Return
        End If
        Using reader As New StreamReader(filePath)
            ' Read each line from the file
            While Not reader.EndOfStream
                ' Read a line
                Dim line As String = reader.ReadLine()

                ' Split the line into values
                Dim values As String() = line.Split(",")

                ' Add the values to the list
                dataList.AddRange(values)
            End While
        End Using


        ' Loop through each line and split it into values

    End Sub

    Private Sub LoadResults(line As String, actual As Boolean)
        Try
            LoggerHelper.LogInfo("Starting LoadResults.")

            cmbAccCtl.Text = line
            If btnAccQC.Checked = True Then         'Control
                If QCDirty Then SaveQCChanges()
                If cmbRun.SelectedIndex <> -1 And cmbAccCtl.SelectedIndex <> -1 Then
                    Dim ItemR As MyList = cmbRun.SelectedItem
                    Dim ItemC As MyList = cmbAccCtl.SelectedItem
                    DisplayControl(ItemR.ItemData, ItemC.ItemData)
                    'PopulateOverride(dgvResults.RowCount)
                    UpdateCtlReleaseStatus(ItemR.ItemData, ItemC.ItemData)
                Else
                    MsgBox("In order to display the Quality Control record, both Batched " _
                    & "Run and the Control must be selected")
                    If cmbRun.SelectedIndex = -1 Then cmbRun.Focus()
                    If cmbAccCtl.SelectedIndex = -1 Then cmbAccCtl.Focus()
                End If
            Else            'Accession
                DisableCommands()
                Try
                    Dim AccID As String = ""
                    Dim WID As String = ""
                    If cmbRun.SelectedIndex <> -1 Then
                        Dim ItemX As MyList = cmbRun.SelectedItem
                        WID = ItemX.ItemData.ToString
                    End If
                    If cmbAccCtl.Text <> "" Then
                        If dgvResults.RowCount = 0 Then 'nothing displayed
                            AccID = cmbAccCtl.Text
                        Else    'get next
                            If AccDirty = True Then
                                SaveAccChanges(dgvResults.Rows(0).Cells(12).Value)
                                AccDirty = False
                            End If
                            If cmbAccCtl.Text = dgvResults.Rows(0).Cells(12).Value Then
                                AccID = GetNextAccID(Val(cmbAccCtl.Text), WID, chkIncomplete.Checked, dtpFromDate.Value.Date, GetEndDate(dtpToDate.Value))
                            Else
                                AccID = cmbAccCtl.Text
                            End If
                        End If
                        If AccID <> "" Then
                            ClearForm()
                            My.Application.DoEvents()
                            lblStatus.Text = ""
                            Dim stopWatch As New Stopwatch()
                            stopWatch.Start()
                            '
                            'DisplayAccession(Val(AccID), WID)
                            DisplayResults(Val(AccID), WID)
                            DisplayExtendResult(AccID)
                            UpdateAccReleaseStatus(Val(AccID))
                            lblRPTStatus.Text = GetReportStatus(Val(AccID))
                            '
                            stopWatch.Stop()
                            lblStatus.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                            stopWatch.Elapsed.Hours, stopWatch.Elapsed.Minutes,
                            stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds / 10)
                            My.Application.DoEvents()
                            '
                            If dgvResults.RowCount > 0 Then
                                cmbAccCtl.Text = dgvResults.Rows(0).Cells(12).Value
                            Else
                                MsgBox("Invalid Accession or End of File", MsgBoxStyle.Information, "Prolis")
                                ClearForm()
                                cmbAccCtl.Focus()
                            End If
                        Else
                            MsgBox("Invalid Accession or End of File", MsgBoxStyle.Information, "Prolis")
                            ClearForm()
                            cmbAccCtl.Focus()
                        End If
                    Else
                        AccID = GetNextAccID("", WID, chkIncomplete.Checked, dtpFromDate.Value.Date, GetEndDate(dtpToDate.Value))
                        If AccID <> "" Then
                            cmbAccCtl.Text = AccID
                            dgvResults.Rows.Clear()
                            txtNote.Text = ""
                            lblStatus.Text = ""
                            Dim stopWatch As New Stopwatch()
                            stopWatch.Start()
                            '
                            'DisplayAccession(Val(AccID), WID)
                            DisplayResults(Val(AccID), WID)
                            DisplayExtendResult(AccID)
                            UpdateAccReleaseStatus(Val(AccID))
                            lblRPTStatus.Text = GetReportStatus(Val(AccID))
                            '
                            stopWatch.Stop()
                            lblStatus.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                            stopWatch.Elapsed.Hours, stopWatch.Elapsed.Minutes,
                            stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds / 10)
                            My.Application.DoEvents()
                        Else
                            MsgBox("Invalid Accession or End of File", MsgBoxStyle.Information, "Prolis")
                            ClearForm()
                        End If
                    End If
                Catch ex As Exception
                    If actual Then
                        MsgBox(ex.InnerException.Message)
                    Else
                        AppendStringToCsv("OutPut.csv", line)
                    End If
                    MsgBox("Stack Trace: " & ex.StackTrace)
                    ' 
                End Try
                EnableCommands()

                If (lblRPTStatus.Text.ToLower().Contains("partial") Or lblRPTStatus.Text.ToLower().Contains("initial")) And actual = False Then
                    '' txtFiller.Text = "."
                    Dim skip = True
                    For Each r As System.Windows.Forms.DataGridViewRow In dgvResults.Rows
                        If Not r.Cells(10).Value = True Then
                            skip = False
                            Exit For
                        End If
                    Next
                    If skip Then
                        ''btnFill.PerformClick()
                        btnBlock.PerformClick()
                        btnRefresh.PerformClick()
                        btnRelease.PerformClick()
                        btnRefresh.PerformClick()
                    End If

                End If
            End If

        Catch ex As Exception
            LoggerHelper.LogError(ex, "Error LoadResults.")
        End Try
    End Sub
    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Alert.Text = ""
        If cmbAccCtl.Text.Contains("-") Then
            Dim accid = cmbAccCtl.Text.Split("-")(0)
            cmbAccCtl.Text = accid
            LoadResults(accid, True)
        Else
            LoadResults(cmbAccCtl.Text, True)

        End If

    End Sub
    Sub AppendStringToCsv(filePath As String, stringValue As String)
        ' Check if the file exists, create it if not
        If Not System.IO.File.Exists(filePath) Then
            System.IO.File.Create(filePath).Close()
        End If

        ' Open the CSV file with StreamWriter in append mode
        Using writer As New StreamWriter(filePath, True)
            ' Write the string to the file
            writer.Write(stringValue)

            ' Add a comma if it's not the last value
            If Not String.IsNullOrEmpty(stringValue) Then
                writer.Write(",")
            End If

            ' Write a new line
            writer.WriteLine()
        End Using
    End Sub
    'Private Function GetReportStatus(ByVal AccID As Long) As String
    '    Dim Status As String = ""
    '    Dim Rs As New ADODB.Recordset
    '    Rs.Open("Select * from Requisitions where Received <> 0 and ID = " & AccID, _
    '    CN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Not Rs.BOF Then
    '        If Rs.Fields("Reported_Final").Value IsNot Nothing AndAlso _
    '        Rs.Fields("Reported_Final").Value IsNot System.DBNull.Value _
    '        AndAlso Rs.Fields("Reported_Final").Value <> "#12:00:00 AM#" Then
    '            Status = "FINAL"
    '        ElseIf Rs.Fields("ReportedOn").Value IsNot Nothing AndAlso _
    '        Rs.Fields("ReportedOn").Value IsNot System.DBNull.Value _
    '        AndAlso Rs.Fields("ReportedOn").Value <> "#12:00:00 AM#" Then
    '            Status = "PARTIAL"
    '        ElseIf Rs.Fields("Reported_Initial").Value IsNot Nothing AndAlso _
    '        Rs.Fields("Reported_Initial").Value IsNot System.DBNull.Value _
    '        AndAlso Rs.Fields("Reported_Initial").Value <> "#12:00:00 AM#" Then
    '            Status = "INITIAL"
    '        Else
    '            Status = "UNRELEASED"
    '        End If
    '    End If
    '    Rs.Close()
    '    Rs = Nothing
    '    Return Status
    'End Function

    Private Sub ClearForm()
        'cmbAccCtl.Text = ""
        txtMeds.Text = ""
        dgvResults.Rows.Clear()
        txtComment.Text = ""
        txtRTF.Text = ""
        txtNote.Text = ""
        txtClient.Text = ""
        txtAttProv.Text = ""
        txtPatient.Text = ""
        txtRelStatus.Text = ""
        txtdrawn.Text = ""
        txtEditedBy.Text = ""
        txtEditedOn.Text = ""
        txtRptDate.Text = ""
        txtRptTime.Text = ""
        If cmbDirector.Items.Count > 0 Then
            cmbDirector.SelectedIndex = 0
        Else
            cmbDirector.SelectedIndex = -1
        End If
        lblRPTStatus.Text = ""
        ClearExtendedResult()
    End Sub

    Private Sub ClearExtendedResult()
        ExtPDF = Nothing
        chkExtRelease.Checked = False
        btnDelPDF.Enabled = False
        btnBrowse.Enabled = True
        'AxAcroPDF1.src = "RemoveExtPDFContent.PDF"
        'PdfViewer1.Open("RemoveExtPDFContent.PDF")
        ' PdfViewer1.Close()
    End Sub

    Private Sub btnPrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrev.Click
        Alert.Text = ""
        'PdfViewer1.Close()
        'PdfViewer1.Refresh()
        If btnAccQC.Checked = False Then    'ACC
            DisableCommands()
            Try
                Dim AccID As String = ""
                Dim WID As String = ""
                If cmbRun.SelectedIndex <> -1 Then
                    Dim ItemX As MyList = cmbRun.SelectedItem
                    WID = ItemX.ItemData.ToString
                End If
                If cmbAccCtl.Text <> "" Then
                    If dgvResults.RowCount = 0 Then 'nothing displayed
                        AccID = cmbAccCtl.Text
                    Else
                        If cmbAccCtl.Text = dgvResults.Rows(0).Cells(12).Value Then
                            If AccDirty = True Then
                                SaveAccChanges(dgvResults.Rows(0).Cells(12).Value)
                                AccDirty = False
                            End If
                            AccID = GetPreviousAccID(Val(cmbAccCtl.Text), WID, chkIncomplete.Checked, dtpFromDate.Value, GetEndDate(dtpToDate.Value))
                        Else
                            AccID = cmbAccCtl.Text
                        End If
                    End If
                    If AccID <> "" Then
                        ClearForm()
                        My.Application.DoEvents()
                        lblStatus.Text = ""
                        Dim stopWatch As New Stopwatch()
                        stopWatch.Start()
                        '
                        'DisplayAccession(Val(AccID), WID)
                        DisplayResults(Val(AccID), WID)
                        DisplayExtendResult(AccID)
                        UpdateAccReleaseStatus(Val(AccID))
                        lblRPTStatus.Text = GetReportStatus(Val(AccID))
                        '
                        stopWatch.Stop()
                        lblStatus.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        stopWatch.Elapsed.Hours, stopWatch.Elapsed.Minutes,
                        stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds / 10)
                        My.Application.DoEvents()
                        '
                        If dgvResults.RowCount > 0 Then
                            cmbAccCtl.Text = dgvResults.Rows(0).Cells(12).Value
                        Else
                            MsgBox("Invalid Accession or Beginning of File", MsgBoxStyle.Information, "Prolis")
                            ClearForm()
                        End If
                    Else
                        MsgBox("Invalid Accession or Beginning of File", MsgBoxStyle.Information, "Prolis")
                        ClearForm()
                    End If
                Else
                    AccID = GetPreviousAccID("", WID, chkIncomplete.Checked, dtpFromDate.Value, GetEndDate(dtpToDate.Value))
                    If AccID <> "" Then
                        cmbAccCtl.Text = AccID
                        dgvResults.Rows.Clear()
                        txtNote.Text = ""
                        lblStatus.Text = ""
                        Dim stopWatch As New Stopwatch()
                        stopWatch.Start()
                        '
                        'DisplayAccession(Val(AccID), WID)
                        DisplayResults(Val(AccID), WID)
                        DisplayExtendResult(AccID)
                        UpdateAccReleaseStatus(Val(AccID))
                        lblRPTStatus.Text = GetReportStatus(Val(AccID))
                        '
                        stopWatch.Stop()
                        lblStatus.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        stopWatch.Elapsed.Hours, stopWatch.Elapsed.Minutes,
                        stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds / 10)
                        My.Application.DoEvents()
                    Else
                        MsgBox("Invalid Accession or Beginning of File", MsgBoxStyle.Information, "Prolis")
                        ClearForm()
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            EnableCommands()
        End If
    End Sub

    Private Sub cmbRun_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbRun.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub cmbRun_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbRun.LostFocus
        txtValidated.Text = ""
        cmbOverride.SelectedIndex = -1
        cmbOverride.Items.Clear()
        txtRelStatus.Text = ""
        txtEditedBy.Text = ""
        txtEditedOn.Text = ""
        If cmbRun.SelectedIndex = -1 Then
            cmbAccCtl.Items.Clear()
        End If
    End Sub

    Private Sub cmbRun_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbRun.SelectedIndexChanged
        txtValidated.Text = ""
        cmbOverride.SelectedIndex = -1
        cmbOverride.Items.Clear()
        txtRelStatus.Text = ""
        txtEditedBy.Text = ""
        txtEditedOn.Text = ""
        'cmbAccCtl.Text = ""
        cmbAccCtl.SelectedIndex = -1
        dgvResults.Rows.Clear()
        If cmbRun.SelectedIndex <> -1 Then
            If btnAccQC.Checked = True Then 'QC
                Dim ItemX As MyList = cmbRun.SelectedItem
                cmbAccCtl.DropDownStyle = ComboBoxStyle.DropDownList
                UpdateValStatus(ItemX.ItemData)
                LoadControls(ItemX.ItemData)
                cmbOverride.Enabled = True
            Else
                cmbAccCtl.DropDownStyle = ComboBoxStyle.DropDown
                'LoadAccessions(ItemX.ItemData)
                cmbOverride.Enabled = False
            End If
        Else
            cmbAccCtl.Text = ""
            cmbAccCtl.SelectedIndex = -1
        End If
    End Sub

    Private Function DeltaCheck(ByVal AccID As Long, ByVal TestID As Integer,
    ByVal Result As String) As Boolean
        Dim DC As Boolean = True
        Dim cndc As New SqlClient.SqlConnection(connString)
        cndc.Open()
        Dim cmddc As New SqlClient.SqlCommand("Select a.Result as Result from Tests c inner join (Requisitions b " &
        "inner join Acc_Results a on b.ID = a.Accession_ID) on a.Test_ID = c.ID where c.DeltaCheck <> 0 and c.ID = " &
        TestID & " and b.Patient_ID in (Select Patient_ID from Requisitions where ID = " & AccID & ") and b.ID < " &
        AccID & " order by b.AccessionDate DESC", cndc)
        cmddc.CommandType = CommandType.Text
        Dim drdc As SqlClient.SqlDataReader = cmddc.ExecuteReader
        If drdc.HasRows Then
            While drdc.Read
                Try
                    If drdc("Result") IsNot DBNull.Value And drdc("Result") IsNot Nothing Then
                        If Result <> "" Then
                            If Trim(drdc("Result")) <> Trim(Result) Then DC = False
                        End If
                    End If
                Catch ex As Exception

                End Try


            End While
        End If
        cndc.Close()
        cndc = Nothing
        Return DC
    End Function

    Private Sub dgvResults_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvResults.CellClick
        Dim NormalStyle As New DataGridViewCellStyle
        Dim IsDirty As Boolean = False
        Dim FB() As String = {"", ""}
        Dim IMG As Byte()
        If e.RowIndex <> -1 Then
            TestID = dgvResults.Rows(e.RowIndex).Cells(0).Value
            Dim RptComp As Boolean = False
            If btnAccQC.Checked = False Then RptComp = ReportFullResulted(Val(cmbAccCtl.Text)) 'ACC
            If e.ColumnIndex = 3 Then   'Reflux
                '*** QL Reflexing *************************************************** 
                If dgvResults.Rows(e.RowIndex).Cells(19).Value = False Then  'non-esig
                    If dgvResults.Rows(e.RowIndex).Cells(11).Value = True Then  'QL
                        ChoiceTestID = dgvResults.Rows(e.RowIndex).Cells(0).Value
                        Dim Choice As String = frmChoiceLook.ShowDialog()
                        If Choice <> "" Then
                            dgvResults.Rows(e.RowIndex).Cells(2).Value = Choice
                            If ThisUser.Result_Release = True AndAlso
                            SystemConfig.ReleaseWithEntry Then _
                            dgvResults.Rows(e.RowIndex).Cells(10).Value = True 'Rel if required
                            IsDirty = True
                            txtRptDate.Text = Format(Date.Today, SystemConfig.DateFormat)
                            txtRptTime.Text = Format(Date.Now, "HH:mm")
                            My.Application.DoEvents()
                            If btnAccQC.Checked = False AndAlso
                            dgvResults.Rows(e.RowIndex).Cells(11).Value = True Then    'QL Test of Accession
                                If IsAutomarker(dgvResults.Rows(e.RowIndex).Cells(0).Value) Then
                                    If ResultTriggering(dgvResults.Rows(e.RowIndex).Cells(12).Value,
                                    dgvResults.Rows(e.RowIndex).Cells(0).Value,
                                    dgvResults.Rows(e.RowIndex).Cells(2).Value) Then
                                        TriggerID = dgvResults.Rows(e.RowIndex).Cells(0).Value
                                        frmReflux.Tag = TriggerID
                                        frmReflux.txtAccID.Text = Trim(cmbAccCtl.Text)
                                        Dim NewVals As String = frmReflux.ShowDialog()
                                        If NewVals <> "" Then
                                            ProcessCMDTrigger(NewVals, dgvResults.Rows(e.RowIndex).Cells(13).Value)
                                            TriggerID = Nothing
                                            NewVals = ""
                                            IsDirty = True
                                            txtRptDate.Text = Format(Date.Now, SystemConfig.DateFormat)
                                            txtRptTime.Text = Format(Date.Now, "HH:mm")
                                            If SystemConfig.CumRef = False Then btnRefresh_Click(Nothing, Nothing)
                                        End If
                                    Else
                                        'CleanAutomarked
                                        Dim RefCleaner As String = GetRefCleaner(dgvResults.Rows(e.RowIndex).Cells(12).Value,
                                        dgvResults.Rows(e.RowIndex).Cells(0).Value)
                                        If RefCleaner <> "" Then
                                            ProcessCMDTrigger(RefCleaner, dgvResults.Rows(e.RowIndex).Cells(13).Value.ToString)
                                            IsDirty = True
                                            RefCleaner = ""
                                            txtRptDate.Text = Format(Date.Now, SystemConfig.DateFormat)
                                            txtRptTime.Text = Format(Date.Now, "HH:mm")
                                        End If
                                        If SystemConfig.CumRef = False Then btnRefresh_Click(Nothing, Nothing)
                                    End If
                                    'If IsDirty Then DecorateGridRow(e.RowIndex)
                                End If

                                FB = GetFlag(dgvResults.Rows(e.RowIndex).Cells(12).Value,
                                dgvResults.Rows(e.RowIndex).Cells(2).Value,
                                dgvResults.Rows(e.RowIndex).Cells(0).Value)
                                dgvResults.Rows(e.RowIndex).Cells(4).Value = FB(0)
                                dgvResults.Rows(e.RowIndex).Cells(20).Value = FB(1)
                                If FB(1) = "Caution" Then
                                    dgvResults.Rows(e.RowIndex).Cells(4).Style = AbnormalStyle
                                ElseIf FB(1) = "Panic" Then
                                    dgvResults.Rows(e.RowIndex).Cells(4).Style = PanicStyle
                                End If

                                'If dgvResults.Rows(e.RowIndex).Cells(4).Value = "" Then 'Unrecognized
                                '    RetVal = MsgBox("Your entry is outside of the prdefined scope and appears to " & _
                                '    "be a typographical error. Are you certain of the correctness of your entry?", _
                                '    MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
                                '    If RetVal = vbNo Then dgvResults.Rows(e.RowIndex).Cells(2).Value = ""
                                'End If
                            End If
                        End If
                    End If
                Else    'eSig
                    txtRTF.Rtf = dgvResults.Rows(e.RowIndex).Cells(17).Value
                    frmRTF.rtt = dgvResults.Rows(e.RowIndex).Cells(17).Value
                    Dim eSigName As String = frmESig.ShowDialog()
                    If eSigName <> "" Then
                        Dim Segs() As String = Split(eSigName, "|")
                        dgvResults.Rows(e.RowIndex).Cells(2).Value = Segs(1)
                        txtRTF.Text = "*** Electronically Signed By ***" &
                        vbCrLf & vbCrLf & Segs(2) & vbCrLf & vbCrLf & Segs(1)
                        dgvResults.Rows(e.RowIndex).Cells(17).Value = txtRTF.Rtf
                        txtRTF.Text = ""
                        dgvResults.Rows(e.RowIndex).Cells(10).Value = True
                        dgvResults.Rows(e.RowIndex).Cells(9).Value =
                        System.Drawing.Image.FromFile(Application.StartupPath &
                        "\Images\rtf.ico")
                    Else
                        dgvResults.Rows(e.RowIndex).Cells(2).Value = ""
                        txtRTF.Text = ""
                        dgvResults.Rows(e.RowIndex).Cells(17).Value = ""
                        dgvResults.Rows(e.RowIndex).Cells(10).Value = False
                        dgvResults.Rows(e.RowIndex).Cells(9).Value =
                        System.Drawing.Image.FromFile(Application.StartupPath &
                        "\Images\rtfblank.ico")
                    End If
                    txtRptDate.Text = Format(Date.Today, SystemConfig.DateFormat)
                    txtRptTime.Text = Format(Date.Now, "HH:mm")
                    FB(0) = dgvResults.Rows(e.RowIndex).Cells(4).Value
                    If dgvResults.Rows(e.RowIndex).Cells(4).Style.BackColor = Color.Yellow Then
                        FB(1) = "Caution"
                    ElseIf dgvResults.Rows(e.RowIndex).Cells(4).Style.BackColor = Color.Red Then
                        FB(1) = "Panic"
                    Else
                        FB(1) = "Ignore"
                    End If
                    SaveResultChange(dgvResults.Rows(e.RowIndex).Cells(12).Value, dgvResults.Rows(e.RowIndex).Cells(13).Value,
                    dgvResults.Rows(e.RowIndex).Cells(14).Value, dgvResults.Rows(e.RowIndex).Cells(15).Value,
                    dgvResults.Rows(e.RowIndex).Cells(0).Value, origRes, dgvResults.Rows(e.RowIndex).Cells(2).Value, FB(0),
                    FB(1), "", dgvResults.Rows(e.RowIndex).Cells(17).Value, Nothing, True, Format(Date.Today, SystemConfig.DateFormat) _
                    & " " & Format(Date.Now, "HH:mm:ss"), e.RowIndex)
                    AccDirty = True
                End If
            ElseIf e.ColumnIndex = 6 Then       'History
                If dgvResults.Rows(e.RowIndex).Cells(19).Value = False Then
                    TestID = dgvResults.Rows(e.RowIndex).Cells(0).Value
                    'Dim frm As New frmHistory(cmbAccCtl.Text, TestID)
                    'frm.ShowDialog()
                    frmHistory.TestID = TestID
                    frmHistory.AccessionID = cmbAccCtl.Text
                    frmHistory.ShowDialog()
                End If
            ElseIf e.ColumnIndex = 7 Then       'Note
                If dgvResults.Rows(e.RowIndex).Cells(19).Value = False Then
                    If (RptComp = True AndAlso (ThisUser.Supervisor = True _
                    Or ThisUser.Director = True)) OrElse (RptComp = False AndAlso
                    (ThisUser.Result_Entry = True And ThisUser.Result_Release = True)) Then 'Complete Report but power user
                        txtComment.Text = Trim(dgvResults.Rows(e.RowIndex).Cells(16).Value)

                        'Dim Note As String = frmResultNote.ShowDialog()
                        Dim Note As String = Trim(dgvResults.Rows(e.RowIndex).Cells(16).Value)

                        Using frm As New frmResultNote()
                            frm.SavedNote = Trim(dgvResults.Rows(e.RowIndex).Cells(16).Value)
                            frm.ShowDialog()

                            If frm.DialogResult = DialogResult.OK Then
                                Note = frm.txtNote.Text.Trim
                            End If
                        End Using

                        dgvResults.Rows(e.RowIndex).Cells(16).Value = Trim(Note)
                        If Trim(txtComment.Text) <> Trim(Note) Then
                            IsDirty = True
                            txtRptDate.Text = Format(Date.Now, SystemConfig.DateFormat)
                            txtRptTime.Text = Format(Date.Now, "HH:mm")
                        End If
                    End If
                    If dgvResults.Rows(e.RowIndex).Cells(16).Value <> "" Then
                        dgvResults.Rows(e.RowIndex).Cells(7).Value =
                        Image.FromFile(My.Application.Info.DirectoryPath & "\Images\Note.ico")
                    Else
                        dgvResults.Rows(e.RowIndex).Cells(7).Value =
                        Image.FromFile(My.Application.Info.DirectoryPath & "\Images\NoteBlank.ico")
                    End If
                    If dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value <> Nothing Then
                        If dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value.ToString().Contains("Delta Check failed") Then
                            Alert.Text = "∆ Delta Check failed"
                        Else

                        End If
                    Else


                    End If
                End If
            ElseIf e.ColumnIndex = 9 Then       'Extended Result
                If dgvResults.Rows(e.RowIndex).Cells(19).Value = False Then
                    If (RptComp = True AndAlso (ThisUser.Supervisor = True _
                    Or ThisUser.Director = True)) OrElse (RptComp = False AndAlso
                    (ThisUser.Result_Entry = True And ThisUser.Result_Release = True)) Then 'Complete Report but power user
                        If dgvResults.Rows(e.RowIndex).Cells(18).Value IsNot DBNull.Value AndAlso
                        dgvResults.Rows(e.RowIndex).Cells(18).Value IsNot Nothing Then
                            If TypeOf (dgvResults.Rows(e.RowIndex).Cells(18).Value) Is String Then
                                pctImg.Image = ConvertBase64ToImage(dgvResults.Rows(e.RowIndex).Cells(18).Value)
                            Else
                                pctImg.Image = dgvResults.Rows(e.RowIndex).Cells(18).Value

                            End If
                            frmRTF.img = pctImg.Image
                        Else
                            pctImg.Image = Nothing
                            frmRTF.img = pctImg.Image
                        End If

                        txtRTF.Rtf = dgvResults.Rows(e.RowIndex).Cells(17).Value
                        frmRTF.rtt = txtRTF.Rtf
                        txtRTF.Rtf = frmRTF.ShowDialog()
                        frmRTF.rtt = txtRTF.Rtf
                        If txtRTF.Text <> "" Then   'changed
                            dgvResults.Rows(e.RowIndex).Cells(17).Value = txtRTF.Rtf
                            dgvResults.Rows(e.RowIndex).Cells(9).Value =
                            System.Drawing.Image.FromFile(Application.StartupPath &
                            "\Images\rtf.ico")

                            pctImg.Image = System.Drawing.Image.FromFile(Application.StartupPath &
                            "\Images\rtf.ico")
                            frmRTF.img = pctImg.Image

                        Else
                            dgvResults.Rows(e.RowIndex).Cells(17).Value = ""
                            dgvResults.Rows(e.RowIndex).Cells(9).Value =
                            System.Drawing.Image.FromFile(Application.StartupPath &
                            "\Images\rtfblank.ico")


                            pctImg.Image = System.Drawing.Image.FromFile(Application.StartupPath &
                            "\Images\rtfblank.ico")
                            frmRTF.img = pctImg.Image

                        End If
                        dgvResults.Rows(e.RowIndex).Cells(18).Value = pctImg.Image
                        IsDirty = True
                        txtRptDate.Text = Format(Date.Now, SystemConfig.DateFormat)
                        txtRptTime.Text = Format(Date.Now, "HH:mm")
                    End If
                End If
            ElseIf e.ColumnIndex = 10 Then       'Release Result
                If dgvResults.Rows(e.RowIndex).Cells(10).Value = False Then
                    dgvResults.Rows(e.RowIndex).Cells(10).Value = True
                Else
                    dgvResults.Rows(e.RowIndex).Cells(10).Value = False
                End If
                If btnAccQC.Checked = False Then    'ACC
                    txtRptDate.Text = Format(Date.Now, SystemConfig.DateFormat)
                    txtRptTime.Text = Format(Date.Now, "HH:mm")
                    If (RptComp = True AndAlso (ThisUser.Supervisor = True _
                    Or ThisUser.Director = True)) OrElse (RptComp = False AndAlso
                    (ThisUser.Result_Entry = True And ThisUser.Result_Release = True)) _
                    OrElse dgvResults.Rows(e.RowIndex).Cells(19).Value = False Then 'Complete Report but power user
                        If Trim(dgvResults.Rows(e.RowIndex).Cells(2).Value.ToString) <> "" _
                        And ((dgvResults.Rows(e.RowIndex).Cells(20).Value <> "Panic") Or
                        (dgvResults.Rows(e.RowIndex).Cells(20).Value = "Panic" And
                         dgvResults.Rows(e.RowIndex).Cells(16).Value <> "")) Then
                            dgvResults.Rows(e.RowIndex).Cells(10).Value = True
                            Exit Sub
                        Else
                            If dgvResults.Rows(e.RowIndex).Cells(16).Value = "" AndAlso
                            dgvResults.Rows(e.RowIndex).Cells(20).Value = "Panic" Then
                                MsgBox("The result will not get released, as Prolis requires verification", MsgBoxStyle.Critical, "Prolis")
                                dgvResults.Rows(e.RowIndex).Cells(10).Value = False
                                Exit Sub
                            End If
                        End If
                        IsDirty = True
                        dgvResults.Rows(e.RowIndex).Cells(10).ReadOnly = False
                    Else
                        dgvResults.Rows(e.RowIndex).Cells(10).ReadOnly = True
                    End If
                Else
                    If (CType(dgvResults.Rows(e.RowIndex).Cells(10).Value, Boolean) = False _
                    AndAlso Trim(dgvResults.Rows(e.RowIndex).Cells(2).Value.ToString) <> "") Then
                        dgvResults.Rows(e.RowIndex).Cells(10).Value = True
                    Else
                        dgvResults.Rows(e.RowIndex).Cells(10).Value = False
                    End If
                    IsDirty = True
                End If
            End If
            If IsDirty = True Then
                If dgvResults.Rows(e.RowIndex).Cells(18).Value IsNot Nothing Then


                    IMG = BitmapToBytes(dgvResults.Rows(e.RowIndex).Cells(18).Value)
                Else
                    IMG = Nothing
                End If
                '
                FB(0) = dgvResults.Rows(e.RowIndex).Cells(4).Value
                FB(1) = dgvResults.Rows(e.RowIndex).Cells(20).Value
                '
                SaveResultChange(Val(dgvResults.Rows(e.RowIndex).Cells(12).Value),
                dgvResults.Rows(e.RowIndex).Cells(13).Value, dgvResults.Rows(e.RowIndex).Cells(14).Value,
                dgvResults.Rows(e.RowIndex).Cells(15).Value, dgvResults.Rows(e.RowIndex).Cells(0).Value,
                origRes, dgvResults.Rows(e.RowIndex).Cells(2).Value, FB(0), FB(1),
                dgvResults.Rows(e.RowIndex).Cells(16).Value, dgvResults.Rows(e.RowIndex).Cells(17).Value,
                IMG, dgvResults.Rows(e.RowIndex).Cells(10).Value, CDate(txtRptDate.Text & " " &
                txtRptTime.Text), e.RowIndex)
                IsDirty = False
                AccDirty = True
                ' *** Coloring Flag ************
                If dgvResults.Rows(e.RowIndex).Cells(19).Value = False Then _
                dgvResults.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.White
                If UCase(dgvResults.Rows(e.RowIndex).Cells(20).Value) = "PANIC" Then
                    dgvResults.Rows(e.RowIndex).Cells(4).Style = PanicStyle
                ElseIf (UCase(dgvResults.Rows(e.RowIndex).Cells(4).Value).ToString.StartsWith("H") Or
                    UCase(dgvResults.Rows(e.RowIndex).Cells(4).Value).ToString.StartsWith("L") Or
                    UCase(dgvResults.Rows(e.RowIndex).Cells(4).Value).ToString.StartsWith("A") Or
                    UCase(dgvResults.Rows(e.RowIndex).Cells(4).Value).ToString.StartsWith("REAC") Or
                    UCase(dgvResults.Rows(e.RowIndex).Cells(4).Value).ToString.StartsWith("POS") Or
                    UCase(dgvResults.Rows(e.RowIndex).Cells(4).Value).ToString.StartsWith("EQ") Or
                    UCase(dgvResults.Rows(e.RowIndex).Cells(4).Value).ToString.StartsWith("NONNEG")) Then
                    dgvResults.Rows(e.RowIndex).Cells(4).Style = HLAStyle
                Else
                    NormalStyle.ForeColor = dgvResults.Rows(e.RowIndex).Cells(0).Style.ForeColor
                    NormalStyle.BackColor = dgvResults.Rows(e.RowIndex).Cells(0).Style.BackColor
                    dgvResults.Rows(e.RowIndex).Cells(4).Style = NormalStyle
                End If
            End If
        End If
    End Sub

    Private Function Img_To_Array(ByVal Img As Image, ByVal ImgFmt As Imaging.ImageFormat) As Byte()
        Dim Ret As Byte() = Nothing
        Dim ms As New IO.MemoryStream
        Try
            Img.Save(ms, ImgFmt)
            Ret = ms.ToArray()
        Catch ex As Exception
            Dim dd = ex.Message
        End Try

        Return Ret
    End Function

    Private Function Array_To_Img(ByVal Arr As Byte()) As System.Drawing.Image
        Dim Img As System.Drawing.Image
        Dim ms As New IO.MemoryStream(Arr, 0, Arr.Length)
        ms.Write(Arr, 0, Arr.Length)
        Img = Image.FromStream(ms)
        Return Img
    End Function

    Private Function IsResultPanic(ByVal Flag As String) As Boolean
        Dim IsPanic As Boolean = False
        If Flag = "LP" Or Flag = "HP" Or Flag = "TBC" Or
        Flag = "AP" Or InStr(Flag, "Panic") > 0 Or
        InStr(Flag, "Critical") > 0 Then IsPanic = True
        Return IsPanic
    End Function

    Private Sub dgvResults_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvResults.CellDoubleClick
        Dim PanicStyle As New DataGridViewCellStyle
        PanicStyle.ForeColor = Color.White
        PanicStyle.BackColor = Color.Red
        Dim AbnormalStyle As New DataGridViewCellStyle
        AbnormalStyle.BackColor = Color.Yellow
        Dim FB() As String = {"", ""}
        Dim IMG As Byte()
        Dim IsDirty As Boolean = False
        If e.RowIndex <> -1 Then
            If e.ColumnIndex = 2 Then
                If dgvResults.Rows(e.RowIndex).Cells(19).Value = False Then
                    If dgvResults.Rows(e.RowIndex).Cells(11).Value = True Then  'QL 
                        ChoiceTestID = dgvResults.Rows(e.RowIndex).Cells(0).Value
                        Dim Choice As String = frmChoiceLook.ShowDialog()
                        If Choice <> "" Then
                            dgvResults.Rows(e.RowIndex).Cells(2).Value = Choice
                            If ThisUser.Result_Release = True AndAlso
                            SystemConfig.ReleaseWithEntry Then _
                            dgvResults.Rows(e.RowIndex).Cells(10).Value = True 'Rel if required
                            IsDirty = True
                            txtRptDate.Text = Format(Date.Today, SystemConfig.DateFormat)
                            txtRptTime.Text = Format(Date.Now, "HH:mm")
                            '=================================================================
                            'dgvResults.Rows(e.RowIndex).Cells(4).Value = GetFlag(dgvResults.Rows(e.RowIndex).Cells(12).Value, dgvResults.Rows(e.RowIndex).Cells(2).Value, dgvResults.Rows(e.RowIndex).Cells(0).Value)

                            FB = GetFlag(dgvResults.Rows(e.RowIndex).Cells(12).Value, dgvResults.Rows(e.RowIndex).Cells(2).Value, dgvResults.Rows(e.RowIndex).Cells(0).Value)
                            dgvResults.Rows(e.RowIndex).Cells(4).Value = FB(0)
                            dgvResults.Rows(e.RowIndex).Cells(20).Value = FB(1)
                            If FB(1) = "Caution" Then
                                dgvResults.Rows(e.RowIndex).Cells(4).Style = AbnormalStyle
                            ElseIf FB(1) = "Panic" Then
                                dgvResults.Rows(e.RowIndex).Cells(4).Style = PanicStyle
                            Else
                                dgvResults.Rows(e.RowIndex).Cells(4).Style = NormalStyle
                            End If
                            '=================================================================

                            'If dgvResults.Rows(e.RowIndex).Cells(4).Value = "" Then 'Unrecognized
                            '    RetVal = MsgBox("Your entry is outside of the prdefined scope and appears to " & _
                            '    "be a typographical error. Are you certain of the correctness of your entry?", _
                            '    MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
                            '    If RetVal = vbNo Then dgvResults.Rows(e.RowIndex).Cells(2).Value = ""
                            'End If
                            My.Application.DoEvents()
                            If btnAccQC.Checked = False Then    'Accession  
                                If IsAutomarker(dgvResults.Rows(e.RowIndex).Cells(0).Value) Then
                                    If ResultTriggering(dgvResults.Rows(e.RowIndex).Cells(12).Value,
                                    dgvResults.Rows(e.RowIndex).Cells(0).Value,
                                    dgvResults.Rows(e.RowIndex).Cells(2).Value) Then
                                        TriggerID = dgvResults.Rows(e.RowIndex).Cells(0).Value
                                        Dim NewVals As String = frmReflux.ShowDialog()
                                        If NewVals <> "" Then
                                            ProcessCMDTrigger(NewVals, dgvResults.Rows(e.RowIndex).Cells(13).Value)
                                            TriggerID = Nothing
                                            IsDirty = True
                                            If SystemConfig.CumRef = False Then btnRefresh_Click(Nothing, Nothing)
                                        End If
                                    Else
                                        'CleanAutomarked
                                        Dim RefCleaner As String = GetRefCleaner(dgvResults.Rows(e.RowIndex).Cells(12).Value, dgvResults.Rows(e.RowIndex).Cells(0).Value)
                                        ProcessCMDTrigger(RefCleaner, dgvResults.Rows(e.RowIndex).Cells(13).Value.ToString)
                                        RefCleaner = ""
                                        IsDirty = True
                                        If SystemConfig.CumRef = False Then btnRefresh_Click(Nothing, Nothing)
                                    End If
                                End If
                            End If
                            SendKeys.Send("{TAB}")
                            'If IsDirty Then DecorateGridRow(e.RowIndex)
                        End If
                    End If
                End If
            End If
            If IsDirty Then
                If dgvResults.Rows(e.RowIndex).Cells(18).Value IsNot Nothing Then
                    IMG = BitmapToBytes(dgvResults.Rows(e.RowIndex).Cells(18).Value)

                Else
                    IMG = Nothing
                End If
                ' FB = dgvResults.Rows(e.RowIndex).Cells(4).Value
                If dgvResults.Rows(e.RowIndex).Cells(4).Style.BackColor = Color.Yellow Then
                    FB(1) = "Caution"
                ElseIf dgvResults.Rows(e.RowIndex).Cells(4).Style.BackColor = Color.Red Then
                    FB(1) = "Panic"
                Else
                    FB(1) = "Ignore"
                End If
                SaveResultChange(Val(dgvResults.Rows(e.RowIndex).Cells(12).Value),
                dgvResults.Rows(e.RowIndex).Cells(13).Value, dgvResults.Rows(e.RowIndex).Cells(14).Value,
                dgvResults.Rows(e.RowIndex).Cells(15).Value, dgvResults.Rows(e.RowIndex).Cells(0).Value,
                origRes, dgvResults.Rows(e.RowIndex).Cells(2).Value, FB(0), FB(1),
                dgvResults.Rows(e.RowIndex).Cells(16).Value, dgvResults.Rows(e.RowIndex).Cells(17).Value,
                IMG, dgvResults.Rows(e.RowIndex).Cells(10).Value, CDate(txtRptDate.Text & " " &
                txtRptTime.Text), e.RowIndex)
                IsDirty = False
                AccDirty = True
            End If
        End If
    End Sub

    Private Sub dgvResults_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvResults.CellEndEdit
        Dim isDirty As Boolean = False
        If e.ColumnIndex = 2 Then   'Result
            Dim RefDirty As Boolean = False
            Dim RetVal As Integer
            Dim IMG As Byte()
            Dim FB() As String = {"", ""}
            'Dim NormalStyle As New DataGridViewCellStyle
            'NormalStyle.ForeColor = dgvResults.Rows(e.RowIndex).Cells(0).Style.ForeColor   
            'NormalStyle.BackColor = dgvResults.Rows(e.RowIndex).Cells(0).Style.BackColor
            txtRptDate.Text = Format(Date.Today, SystemConfig.DateFormat)
            txtRptTime.Text = Format(Date.Now, "HH:mm")
            isDirty = True
            If btnAccQC.Checked = False Then    'ACC
                'Reflex Code '''''
                If Trim(dgvResults.Rows(e.RowIndex).Cells(2).Value) <> "" Then  'changed result
                    'Dim ReflexedIDs() As String = GetConfigReflexedIDs( _
                    'dgvResults.Rows(e.RowIndex).Cells(0).Value, _
                    'Trim(dgvResults.Rows(e.RowIndex).Cells(2).Value))
                    'If ReflexedIDs(0) <> "" Then    'has at least one trigger
                    '    RefDirty = ProcessTriggers(dgvResults.Rows(e.RowIndex).Cells(12).Value, _
                    '    dgvResults.Rows(e.RowIndex).Cells(0).Value, ReflexedIDs)
                    'Else    'Clean Reflexing
                    '    ExecuteSqlProcedure("Delete from Ref_Results where Accession_ID = " & _
                    '    dgvResults.Rows(e.RowIndex).Cells(12).Value & " and Reflexer_ID in " & _
                    '    "(Select Reflexed_ID from Ref_Results where Accession_ID = " & _
                    '    dgvResults.Rows(e.RowIndex).Cells(12).Value & " and Reflexer_ID = " & _
                    '    dgvResults.Rows(e.RowIndex).Cells(0).Value & ")")
                    '    ExecuteSqlProcedure("Delete from Ref_Results where Accession_ID = " & _
                    '    dgvResults.Rows(e.RowIndex).Cells(12).Value & " and Reflexer_ID = " & _
                    '    dgvResults.Rows(e.RowIndex).Cells(0).Value)
                    '    RefDirty = True
                    'End If
                    'If RefDirty Then
                    If IsAutomarker(dgvResults.Rows(e.RowIndex).Cells(0).Value) Then
                        If ResultTriggering(dgvResults.Rows(e.RowIndex).Cells(12).Value,
                        dgvResults.Rows(e.RowIndex).Cells(0).Value,
                        dgvResults.Rows(e.RowIndex).Cells(2).Value) Then
                            TriggerID = dgvResults.Rows(e.RowIndex).Cells(0).Value
                            Dim NewVals As String = frmReflux.ShowDialog()
                            If NewVals <> "" Then
                                ProcessCMDTrigger(NewVals, dgvResults.Rows(e.RowIndex).Cells(13).Value)
                                TriggerID = Nothing
                                NewVals = ""
                                isDirty = True
                                txtRptDate.Text = Format(Date.Now, SystemConfig.DateFormat)
                                txtRptTime.Text = Format(Date.Now, "HH:mm")
                                If SystemConfig.CumRef = False Then btnRefresh_Click(Nothing, Nothing)
                            End If
                        Else
                            'CleanAutomarked
                            Dim RefCleaner As String = GetRefCleaner(dgvResults.Rows(e.RowIndex).Cells(12).Value,
                            dgvResults.Rows(e.RowIndex).Cells(0).Value)
                            If RefCleaner <> "" Then
                                ProcessCMDTrigger(RefCleaner, dgvResults.Rows(e.RowIndex).Cells(13).Value.ToString)
                                isDirty = True
                                RefCleaner = ""
                                txtRptDate.Text = Format(Date.Now, SystemConfig.DateFormat)
                                txtRptTime.Text = Format(Date.Now, "HH:mm")
                            End If
                            If SystemConfig.CumRef = False Then btnRefresh_Click(Nothing, Nothing)
                        End If
                        'If IsDirty Then DecorateGridRow(e.RowIndex)
                    End If
                    If DeltaCheck(dgvResults.Rows(e.RowIndex).Cells(12).Value,
                    dgvResults.Rows(e.RowIndex).Cells(0).Value,
                    dgvResults.Rows(e.RowIndex).Cells(2).Value) = False Then
                        If Not dgvResults.Rows(e.RowIndex).Cells(1).Value.ToString.StartsWith(ChrW(8710)) _
                        Then dgvResults.Rows(e.RowIndex).Cells(1).Value = ChrW(8710) & " " &
                        dgvResults.Rows(e.RowIndex).Cells(1).Value
                        FB(1) = "Panic"
                        If InStr(dgvResults.Rows(e.RowIndex).Cells(16).Value,
                        ChrW(8710) & " Delta Check failed" & vbCrLf) = 0 Then _
                        dgvResults.Rows(e.RowIndex).Cells(16).Value =
                        ChrW(8710) & " Delta Check failed" & vbCrLf &
                        dgvResults.Rows(e.RowIndex).Cells(16).Value
                    Else
                        If InStr(dgvResults.Rows(e.RowIndex).Cells(1).Value.ToString,
                        ChrW(8710)) > 0 Then dgvResults.Rows(e.RowIndex).Cells(1).Value =
                        Replace(dgvResults.Rows(e.RowIndex).Cells(1).Value, ChrW(8710) & " ", "")
                        FB(1) = "Ignore"
                        dgvResults.Rows(e.RowIndex).Cells(16).Value =
                        Replace(dgvResults.Rows(e.RowIndex).Cells(16).Value,
                        ChrW(8710) & " Delta Check failed" & vbCrLf, "")
                    End If
                    '
                    If dgvResults.Rows(e.RowIndex).Cells(11).Value = False Then   'QN
                        If (IsNumeric(dgvResults.Rows(e.RowIndex).Cells(2).Value) OrElse
                        IsNumeric(CleanNumericResult(dgvResults.Rows(e.RowIndex).Cells(2).Value))) Then
                            If ReportableFormatRequired(dgvResults.Rows(e.RowIndex).Cells(0).Value) _
                            Then dgvResults.Rows(e.RowIndex).Cells(2).Value =
                            FormatToReportable(dgvResults.Rows(e.RowIndex).Cells(2).Value,
                            dgvResults.Rows(e.RowIndex).Cells(0).Value)
                            FB = GetFlag(dgvResults.Rows(e.RowIndex).Cells(12).Value,
                            dgvResults.Rows(e.RowIndex).Cells(2).Value,
                            dgvResults.Rows(e.RowIndex).Cells(0).Value)
                            dgvResults.Rows(e.RowIndex).Cells(4).Value = FB(0)
                            dgvResults.Rows(e.RowIndex).Cells(20).Value = FB(1)
                            If FB(1) = "Caution" Then
                                dgvResults.Rows(e.RowIndex).Cells(4).Style = AbnormalStyle
                            ElseIf FB(1) = "Panic" Then
                                dgvResults.Rows(e.RowIndex).Cells(4).Style = PanicStyle
                            End If
                        Else
                            If Trim(dgvResults.Rows(e.RowIndex).Cells(2).Value) <> "." Then
                                RetVal = MsgBox("You entered a non numeric result for a numeric analyte which " _
                                & "seems to be unusual. Would you like to push this result (Click 'Yes' to confirm" _
                                & " or 'No' to re-enter).",
                                MsgBoxStyle.Critical + MsgBoxStyle.YesNo, "Prolis")
                                If RetVal = vbNo Then
                                    dgvResults.Rows(e.RowIndex).Cells(2).Value = ""
                                Else
                                    FB = GetFlag(dgvResults.Rows(e.RowIndex).Cells(12).Value,
                                    dgvResults.Rows(e.RowIndex).Cells(2).Value, dgvResults.Rows(e.RowIndex).Cells(0).Value)
                                    dgvResults.Rows(e.RowIndex).Cells(4).Value = FB(0)
                                    dgvResults.Rows(e.RowIndex).Cells(20).Value = FB(1)
                                    If FB(1) = "Caution" Then
                                        dgvResults.Rows(e.RowIndex).Cells(4).Style = AbnormalStyle
                                    ElseIf FB(1) = "Panic" Then
                                        dgvResults.Rows(e.RowIndex).Cells(4).Style = PanicStyle
                                    End If
                                End If
                            End If
                        End If
                    Else    'QL
                        FB = GetFlag(dgvResults.Rows(e.RowIndex).Cells(12).Value,
                        dgvResults.Rows(e.RowIndex).Cells(2).Value, dgvResults.Rows(e.RowIndex).Cells(0).Value)
                        dgvResults.Rows(e.RowIndex).Cells(4).Value = FB(0)
                        dgvResults.Rows(e.RowIndex).Cells(20).Value = FB(1)
                        If dgvResults.Rows(e.RowIndex).Cells(4).Value = "" _
                        AndAlso dgvResults.Rows(e.RowIndex).Cells(2).Value <> "" Then 'Unrecognized
                            RetVal = MsgBox("Your entry is outside of the prdefined scope and appears to " &
                            "be a typographical error. Are you certain of the correctness of your entry?",
                            MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
                            If RetVal = vbNo Then dgvResults.Rows(e.RowIndex).Cells(2).Value = ""
                        Else
                            If FB(1) = "Caution" Then
                                dgvResults.Rows(e.RowIndex).Cells(4).Style = AbnormalStyle
                            ElseIf FB(1) = "Panic" Then
                                dgvResults.Rows(e.RowIndex).Cells(4).Style = PanicStyle
                            Else
                                dgvResults.Rows(e.RowIndex).Cells(4).Style = NormalStyle
                            End If
                        End If
                    End If
                    If ThisUser.Result_Release = True AndAlso
                    SystemConfig.ReleaseWithEntry AndAlso
                    dgvResults.Rows(e.RowIndex).Cells(2).Value <> "" Then _
                    dgvResults.Rows(e.RowIndex).Cells(10).Value = True 'Rel if required
                    isDirty = True
                    txtRptDate.Text = Format(Date.Today, SystemConfig.DateFormat)
                    txtRptTime.Text = Format(Date.Now, "HH:mm")
                Else    'Result removed
                    dgvResults.Rows(e.RowIndex).Cells(4).Value = ""
                    dgvResults.Rows(e.RowIndex).Cells(4).Style = NormalStyle
                    dgvResults.Rows(e.RowIndex).Cells(20).Value = "Ignore"
                    If dgvResults.Rows(e.RowIndex).Cells(17).Value Is Nothing OrElse
                    Trim(dgvResults.Rows(e.RowIndex).Cells(17).Value.ToString) = "" Then _
                    dgvResults.Rows(e.RowIndex).Cells(10).Value = False
                    ExecuteSqlProcedure("Delete from Ref_Results where Accession_ID = " &
                    dgvResults.Rows(e.RowIndex).Cells(12).Value & " and Reflexer_ID in " &
                    "(Select Reflexed_ID from Ref_Results where Accession_ID = " &
                    dgvResults.Rows(e.RowIndex).Cells(12).Value & " and Reflexer_ID = " &
                    dgvResults.Rows(e.RowIndex).Cells(0).Value & ")")
                    ExecuteSqlProcedure("Delete from Ref_Results where Accession_ID = " &
                    dgvResults.Rows(e.RowIndex).Cells(12).Value & " and Reflexer_ID = " &
                    dgvResults.Rows(e.RowIndex).Cells(0).Value)
                End If
                ProcessConditionalTriggers(dgvResults.Rows(e.RowIndex).Cells(12).Value,
                dgvResults.Rows(e.RowIndex).Cells(0).Value,
                dgvResults.Rows(e.RowIndex).Cells(4).Value)
                If isDirty Then
                    If dgvResults.Rows(e.RowIndex).Cells(18).Value IsNot Nothing Then
                        IMG = BitmapToBytes(dgvResults.Rows(e.RowIndex).Cells(18).Value)
                    Else
                        IMG = Nothing
                    End If
                    If FB(0) = "" Then
                        FB(0) = dgvResults.Rows(e.RowIndex).Cells(4).Value
                        If dgvResults.Rows(e.RowIndex).Cells(4).Style.BackColor = Color.Yellow Then
                            FB(1) = "Caution"
                        ElseIf dgvResults.Rows(e.RowIndex).Cells(4).Style.BackColor = Color.Red Then
                            FB(1) = "Panic"
                        Else
                            FB(1) = "Ignore"
                        End If
                    End If
                    SaveResultChange(Val(dgvResults.Rows(e.RowIndex).Cells(12).Value),
                    dgvResults.Rows(e.RowIndex).Cells(13).Value, dgvResults.Rows(e.RowIndex).Cells(14).Value,
                    dgvResults.Rows(e.RowIndex).Cells(15).Value, dgvResults.Rows(e.RowIndex).Cells(0).Value,
                    origRes, dgvResults.Rows(e.RowIndex).Cells(2).Value, FB(0), FB(1),
                    dgvResults.Rows(e.RowIndex).Cells(16).Value, dgvResults.Rows(e.RowIndex).Cells(17).Value,
                    IMG, dgvResults.Rows(e.RowIndex).Cells(10).Value, CDate(txtRptDate.Text & " " &
                    txtRptTime.Text), e.RowIndex)
                    '============================================= add following in 22jan25
                    origRes = ""    '======================
                    '=====================================================
                    isDirty = False
                    AccDirty = True
                End If
                '
            Else    'QC
                If Trim(dgvResults.Rows(e.RowIndex).Cells(2).Value) <> "" Then  'changed result
                    If dgvResults.Rows(e.RowIndex).Cells(11).Value = False Then   'QN
                        If Not (IsNumeric(dgvResults.Rows(e.RowIndex).Cells(2).Value) OrElse
                        Not IsNumeric(CleanNumericResult(dgvResults.Rows(e.RowIndex).Cells(2).Value))) Then
                            If Trim(dgvResults.Rows(e.RowIndex).Cells(2).Value) <> "." Then
                                RetVal = MsgBox("You entered a non numeric result for a numeric analyte which " _
                                & "seems to be unusual. Would you like to push this result (Click 'Yes' to confirm" _
                                & " or 'No' to re-enter).",
                                MsgBoxStyle.Critical + MsgBoxStyle.YesNo, "Prolis")
                                If RetVal = vbNo Then
                                    dgvResults.Rows(e.RowIndex).Cells(2).Value = ""
                                End If
                            End If
                        End If
                        dgvResults.Rows(e.RowIndex).Cells(4).Value =
                        GetNQCFlag(dgvResults.Rows(e.RowIndex).Cells(15).Value,
                        dgvResults.Rows(e.RowIndex).Cells(12).Value,
                        dgvResults.Rows(e.RowIndex).Cells(0).Value,
                        dgvResults.Rows(e.RowIndex).Cells(2).Value)
                        If SystemConfig.ReleaseWithEntry Then _
                        dgvResults.Rows(e.RowIndex).Cells(10).Value = True
                    Else    'QL
                        dgvResults.Rows(e.RowIndex).Cells(4).Value =
                        GetLQCFlag(dgvResults.Rows(e.RowIndex).Cells(15).Value,
                        dgvResults.Rows(e.RowIndex).Cells(12).Value,
                        dgvResults.Rows(e.RowIndex).Cells(0).Value,
                        dgvResults.Rows(e.RowIndex).Cells(2).Value)
                    End If
                Else    'Result removed
                    dgvResults.Rows(e.RowIndex).Cells(4).Value = ""
                    dgvResults.Rows(e.RowIndex).Cells(10).Value = False
                    'ExecuteSqlProcedure("Delete from QC_Results where Run_ID = " & _
                    'dgvResults.Rows(e.RowIndex).Cells(14).Value & " and Control_ID = " & _
                    'dgvResults.Rows(e.RowIndex).Cells(12).Value & " and Test_ID = " & _
                    'dgvResults.Rows(e.RowIndex).Cells(0).Value)
                End If
                If isDirty Then
                    SaveQCResultChange(dgvResults.Rows(e.RowIndex).Cells(14).Value,
                    dgvResults.Rows(e.RowIndex).Cells(12).Value,
                    dgvResults.Rows(e.RowIndex).Cells(0).Value,
                    IIf(dgvResults.Rows(e.RowIndex).Cells(2).Value Is Nothing,
                    "", dgvResults.Rows(e.RowIndex).Cells(2).Value),
                    dgvResults.Rows(e.RowIndex).Cells(4).Value,
                    dgvResults.Rows(e.RowIndex).Cells(16).Value,
                    dgvResults.Rows(e.RowIndex).Cells(17).Value,
                    dgvResults.Rows(e.RowIndex).Cells(10).Value,
                    CDate(txtRptDate.Text & " " & txtRptTime.Text))
                    isDirty = False
                End If
            End If
        ElseIf e.ColumnIndex = 10 Then  'Release
            If dgvResults.Rows(e.RowIndex).Cells(10).Value = True Then
                If Trim(dgvResults.Rows(e.RowIndex).Cells(2).Value.ToString) <> "" _
                And ((dgvResults.Rows(e.RowIndex).Cells(20).Value <> "Panic") Or
                (dgvResults.Rows(e.RowIndex).Cells(20).Value = "Panic" And
                dgvResults.Rows(e.RowIndex).Cells(16).Value <> "")) Then
                    dgvResults.Rows(e.RowIndex).Cells(10).Value = True
                    'Exit Sub
                Else
                    If dgvResults.Rows(e.RowIndex).Cells(16).Value = "" AndAlso
                    dgvResults.Rows(e.RowIndex).Cells(20).Value = "Panic" Then
                        MsgBox("The result will not get released, as Prolis requires verification", MsgBoxStyle.Critical, "Prolis")
                        dgvResults.Rows(e.RowIndex).Cells(10).Value = False
                        'Exit Sub
                    End If
                End If
            End If
        End If
        'If AccDirty Then
        '    UpdateReportTime(Val(dgvResults.Rows(e.RowIndex).Cells(12).Value), _
        '    CDate(txtRptDate.Text & " " & txtRptTime.Text))
        '    lblRPTStatus.Text = GetReportStatus(Val(dgvResults.Rows(0).Cells(12).Value))
        '    AccDirty = False
        'End If
        'If SystemConfig.CumRef = False Then
        '    If IsDirty Then btnRefresh_Click(Nothing, Nothing)
        'End If
    End Sub

    Private Sub SaveQCResultChange(ByVal RunID As Long, ByVal ControlID As Long, ByVal TestID _
    As Integer, ByVal Result As String, ByVal Flag As String, ByVal Note As String,
    ByVal RTF As String, ByVal Released As Boolean, ByVal RelDate As Date)
        Dim cnqc As New SqlClient.SqlConnection(connString)
        cnqc.Open()
        Dim cmdupsert As New SqlClient.SqlCommand("QC_Results_SP", cnqc)
        cmdupsert.CommandType = CommandType.StoredProcedure
        cmdupsert.Parameters.AddWithValue("@act", "Upsert")
        cmdupsert.Parameters.AddWithValue("@Run_ID", RunID)
        cmdupsert.Parameters.AddWithValue("@Control_ID", ControlID)
        cmdupsert.Parameters.AddWithValue("@Test_ID", TestID)
        cmdupsert.Parameters.AddWithValue("@Ordinal", 0)
        cmdupsert.Parameters.AddWithValue("@Result", Result)
        cmdupsert.Parameters.AddWithValue("@Flag", Flag)
        cmdupsert.Parameters.AddWithValue("@T_Result", RTF)
        cmdupsert.Parameters.AddWithValue("@Comment", Note)
        cmdupsert.Parameters.AddWithValue("@Released", Released)
        If Released = True Then
            cmdupsert.Parameters.AddWithValue("@Released_By", ThisUser.ID)
            cmdupsert.Parameters.AddWithValue("@Release_Time", Date.Now)
        End If
        Try
            cmdupsert.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cnqc.Close()
            cnqc = Nothing
        End Try
    End Sub

    Private Sub SaveExtResultChange0(ByVal AccID As Long, ByVal Result _
    As Byte(), ByVal Released As Boolean, ByVal RelDate As Date)
        'Dim CNE As New ADODB.Connection
        'CNE.Open(connstring)
        'Dim Rs As New ADODB.Recordset
        'Dim sSQL As String = "Select * from Extend_Results where Accession_ID = " & AccID
        'Rs.Open(sSQL, CNE, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        'If Rs.BOF Then Rs.AddNew()
        'Rs.Fields("Accession_ID").Value = AccID
        'If Rs.Fields("Result").Value Is System.DBNull.Value _
        'OrElse Rs.Fields("Result").Value IsNot Result Then
        '    Rs.Fields("Result").Value = Result
        '    Rs.Fields("Released").Value = Released
        '    Rs.Fields("Release_Time").Value = RelDate
        '    Rs.Fields("Released_By").Value = ThisUser.ID
        'End If
        'Rs.Update()
        'Rs.Close()
        'Rs = Nothing
        'CNE.Close()
        'CNE = Nothing
    End Sub

    Private Sub SaveExtResultChange(ByVal AccID As Long, ByVal Result As Byte(), ByVal Released As Boolean, ByVal RelDate As Date)
        Using connection As New SqlConnection(connString)
            connection.Open()

            ' Check if record exists
            Dim recordExists As Boolean
            Dim checkQuery As String = "SELECT COUNT(*) FROM Extend_Results WHERE Accession_ID = @AccID"

            Using checkCmd As New SqlCommand(checkQuery, connection)
                checkCmd.Parameters.AddWithValue("@AccID", AccID)
                recordExists = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0
            End Using

            ' Insert or update logic
            Dim query As String
            If recordExists Then
                query = "UPDATE Extend_Results SET Result = @Result, Released = @Released, Release_Time = @RelDate, Released_By = @ReleasedBy WHERE Accession_ID = @AccID"
            Else
                query = "INSERT INTO Extend_Results (Accession_ID, Result, Released, Release_Time, Released_By) VALUES (@AccID, @Result, @Released, @RelDate, @ReleasedBy)"
            End If

            Using cmd As New SqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@AccID", AccID)
                cmd.Parameters.AddWithValue("@Result", Result)
                cmd.Parameters.AddWithValue("@Released", Released)
                cmd.Parameters.AddWithValue("@RelDate", RelDate)
                cmd.Parameters.AddWithValue("@ReleasedBy", ThisUser.ID)

                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub SaveResultChange(ByVal AccID As Long, ByVal Cause0 As String, ByVal Cause1 As String, ByVal Cause2 As String, ByVal _
    TestID As Integer, ByVal OldRes As String, ByVal NewRes As String, ByVal Flag As String, ByVal Behavior As String, ByVal Note As _
    String, ByVal RTF As String, ByVal IMG As Byte(), ByVal Released As Boolean, ByVal RelDate As Date, ByVal RowIndex As Integer)
        Dim RPTStatus As String = GetReportStatus(AccID)
        Dim sSQLPDF As String = ""
        '
        If NewRes Is Nothing Then
            NewRes = ""
            Flag = ""
        End If
        If Note Is Nothing OrElse Note Is DBNull.Value Then Note = ""
        '
        If InStr(RPTStatus, "FINAL") > 0 Then
            If IsNumeric(NewRes) AndAlso IsNumeric(OldRes) Then
                If Val(NewRes) <> Val(OldRes) Then
                    UpdateResultHistory(AccID, TestID, OldRes, Date.Now, ThisUser.ID)
                    Released = False
                End If
            Else    'non-numeric
                If Trim(OldRes) <> "" AndAlso Trim(NewRes) <> Trim(OldRes) Then
                    UpdateResultHistory(AccID, TestID, OldRes, Date.Now, ThisUser.ID)
                    Released = False
                End If
            End If
        End If
        '
        If Cause0.ToUpper.StartsWith("ACC") Then  'Acc
            Dim cnn As New Data.SqlClient.SqlConnection(connString)
            cnn.Open()
            Dim cmdUpsert As New Data.SqlClient.SqlCommand("Acc_Results_SP", cnn)
            cmdUpsert.CommandType = Data.CommandType.StoredProcedure
            cmdUpsert.Parameters.AddWithValue("@act", "upsert")
            cmdUpsert.Parameters.AddWithValue("@Accession_ID", AccID)
            cmdUpsert.Parameters.AddWithValue("@Test_ID", TestID)
            cmdUpsert.Parameters.AddWithValue("@Result", NewRes)
            cmdUpsert.Parameters.AddWithValue("@Flag", Flag)
            cmdUpsert.Parameters.AddWithValue("@Behavior", Behavior)
            '            cmdUpsert.Parameters.AddWithValue("@Ordinal", 0)
            cmdUpsert.Parameters.AddWithValue("@Comment", Note)
            cmdUpsert.Parameters.AddWithValue("@T_Result", RTF)
            cmdUpsert.Parameters.AddWithValue("@I_Result", IMG)
            If Released = True AndAlso (Behavior <> "PANIC" Or Behavior = "Panic" And Note <> "") Then
                cmdUpsert.Parameters.AddWithValue("@Released", True)
                cmdUpsert.Parameters.AddWithValue("@Release_Time", RelDate)
                cmdUpsert.Parameters.AddWithValue("@Released_By", ThisUser.ID)
            Else
                cmdUpsert.Parameters.AddWithValue("@Released", False)
                cmdUpsert.Parameters.AddWithValue("@Release_Time", DBNull.Value)
                cmdUpsert.Parameters.AddWithValue("@Released_By", DBNull.Value)
            End If
            cmdUpsert.ExecuteNonQuery()
            cnn.Close()
            cnn = Nothing
        ElseIf Cause0.ToUpper.StartsWith("REF") Then  'Ref
            Dim cnn As New Data.SqlClient.SqlConnection(connString)
            cnn.Open()
            Dim cmdUpsert As New Data.SqlClient.SqlCommand("Ref_Results_SP", cnn)
            cmdUpsert.CommandType = Data.CommandType.StoredProcedure
            cmdUpsert.Parameters.AddWithValue("@act", "upsert")
            cmdUpsert.Parameters.AddWithValue("@Accession_ID", AccID)
            cmdUpsert.Parameters.AddWithValue("@Reflexer_ID", Cause1)
            cmdUpsert.Parameters.AddWithValue("@Reflexed_ID", Cause2)
            cmdUpsert.Parameters.AddWithValue("@Test_ID", TestID)
            cmdUpsert.Parameters.AddWithValue("@Result", NewRes)
            cmdUpsert.Parameters.AddWithValue("@Flag", Flag)
            cmdUpsert.Parameters.AddWithValue("@Behavior", Behavior)
            cmdUpsert.Parameters.AddWithValue("@Ordinal", RowIndex)
            cmdUpsert.Parameters.AddWithValue("@Comment", Note)
            cmdUpsert.Parameters.AddWithValue("@T_Result", RTF)
            cmdUpsert.Parameters.AddWithValue("@I_Result", IMG)
            If Released = True AndAlso (Behavior <> "PANIC" Or Behavior = "Panic" And Note <> "") Then
                cmdUpsert.Parameters.AddWithValue("@Released", True)
                cmdUpsert.Parameters.AddWithValue("@Release_Time", RelDate)
                cmdUpsert.Parameters.AddWithValue("@Released_By", ThisUser.ID)
            Else
                cmdUpsert.Parameters.AddWithValue("@Released", False)
                cmdUpsert.Parameters.AddWithValue("@Release_Time", DBNull.Value)
                cmdUpsert.Parameters.AddWithValue("@Released_By", DBNull.Value)
            End If
            cmdUpsert.ExecuteNonQuery()
            cnn.Close()
            cnn = Nothing
        ElseIf Cause0.ToUpper.StartsWith("INF") Then  'Info
            Dim cnn As New Data.SqlClient.SqlConnection(connString)
            cnn.Open()
            Dim cmdUpsert As New Data.SqlClient.SqlCommand("Acc_Info_Results_SP", cnn)
            cmdUpsert.CommandType = Data.CommandType.StoredProcedure
            cmdUpsert.Parameters.AddWithValue("@act", "upsert")
            cmdUpsert.Parameters.AddWithValue("@Accession_ID", AccID)
            cmdUpsert.Parameters.AddWithValue("@Test_ID", Cause1)
            cmdUpsert.Parameters.AddWithValue("@Info_ID", TestID)
            cmdUpsert.Parameters.AddWithValue("@Result", NewRes)
            cmdUpsert.Parameters.AddWithValue("@Flag", Flag)
            cmdUpsert.Parameters.AddWithValue("@Behavior", Behavior)
            cmdUpsert.Parameters.AddWithValue("@Comment", Note)
            cmdUpsert.Parameters.AddWithValue("@T_Result", RTF)
            cmdUpsert.Parameters.AddWithValue("@I_Result", IMG)
            If Released = True AndAlso (Behavior <> "PANIC" Or Behavior = "Panic" And Note <> "") Then
                cmdUpsert.Parameters.AddWithValue("@Released", True)
                cmdUpsert.Parameters.AddWithValue("@Release_Time", RelDate)
                cmdUpsert.Parameters.AddWithValue("@Released_By", ThisUser.ID)
            Else
                cmdUpsert.Parameters.AddWithValue("@Released", False)
                cmdUpsert.Parameters.AddWithValue("@Release_Time", DBNull.Value)
                cmdUpsert.Parameters.AddWithValue("@Released_By", DBNull.Value)
            End If
            cmdUpsert.ExecuteNonQuery()
            cnn.Close()
            cnn = Nothing
        End If
    End Sub

    Private Sub dgvResults_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvResults.CellEnter
        If e.RowIndex <> -1 Then
            If e.ColumnIndex = 2 Then   'Results
                If dgvResults.Rows(e.RowIndex).Cells(2).Value IsNot Nothing AndAlso
                Trim(dgvResults.Rows(e.RowIndex).Cells(2).Value.ToString) <> "" Then
                    origRes = Trim(dgvResults.Rows(e.RowIndex).Cells(2).Value.ToString)
                Else
                    origRes = ""
                    If IsCalculated(dgvResults.Rows(e.RowIndex).Cells(0).Value) Then
                        Dim Result As String = CalculateResult(dgvResults.Rows(e.RowIndex).Cells(0).Value,
                        dgvResults.Rows(e.RowIndex).Cells(12).Value)
                        '
                        If ReportableFormatRequired(dgvResults.Rows(e.RowIndex).Cells(0).Value) _
                        Then Result = FormatToReportable(Result, dgvResults.Rows(e.RowIndex).Cells(0).Value)
                        '
                        dgvResults.Rows(e.RowIndex).Cells(2).Value = Result
                        '
                        If dgvResults.Rows(e.RowIndex).Cells(10).Value Is Nothing OrElse
                        dgvResults.Rows(e.RowIndex).Cells(10).Value Is System.DBNull.Value _
                        Then dgvResults.Rows(e.RowIndex).Cells(10).Value = False
                        ResDirty = True
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub dgvResults_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvResults.CellValidated
        Dim NormalStyle As New DataGridViewCellStyle
        NormalStyle.ForeColor = dgvResults.Rows(e.RowIndex).Cells(1).Style.ForeColor
        NormalStyle.BackColor = dgvResults.Rows(e.RowIndex).Cells(1).Style.BackColor
        Dim FB() As String = {"", ""}
        'Dim RptComp As Boolean = False
        'Dim ItemR As MyList = cmbRun.SelectedItem
        'Dim ItemC As MyList
        'Dim RetVal As Integer
        'Dim TempRes As String = ""
        'Dim Result As String = ""
        Dim isdirty As Boolean = False
        If btnAccQC.Checked = False Then 'Acc
            If e.ColumnIndex = 2 Then       'Result
                If ResDirty Then
                    If dgvResults.Rows(e.RowIndex).Cells(2).Value IsNot Nothing AndAlso
                    Trim(dgvResults.Rows(e.RowIndex).Cells(2).Value.ToString) <> "" Then
                        FB = GetFlag(dgvResults.Rows(e.RowIndex).Cells(12).Value,
                        dgvResults.Rows(e.RowIndex).Cells(2).Value,
                        dgvResults.Rows(e.RowIndex).Cells(0).Value)
                        dgvResults.Rows(e.RowIndex).Cells(4).Value = FB(0)
                        If ThisUser.Result_Release = True AndAlso
                        SystemConfig.ReleaseWithEntry Then _
                        dgvResults.Rows(e.RowIndex).Cells(10).Value = True 'Rel if required
                        isdirty = True
                        txtRptDate.Text = Format(Date.Today, SystemConfig.DateFormat)
                        txtRptTime.Text = Format(Date.Now, "HH:mm")
                    Else  'Null or ""
                        dgvResults.Rows(e.RowIndex).Cells(2).Value = ""
                        dgvResults.Rows(e.RowIndex).Cells(4).Value = ""
                        dgvResults.Rows(e.RowIndex).Cells(10).Value = False
                    End If
                    If FB(1) = "Panic" Then
                        dgvResults.Rows(e.RowIndex).Cells(4).Style = PanicStyle
                    ElseIf FB(1) = "Caution" Then
                        dgvResults.Rows(e.RowIndex).Cells(4).Style = AbnormalStyle
                    End If
                    ResDirty = False
                End If
            ElseIf e.ColumnIndex = 9 Then  'Image
                dgvResults.Rows(e.RowIndex).Cells(18).Value = pctImg.Image
                pctImg.Image = Nothing
                frmRTF.img = pctImg.Image
                isdirty = True
                '    If ThisUser.Result_Release And SystemConfig.ReleaseWithEntry AndAlso _
                '    (CType(dgvResults.Rows(e.RowIndex).Cells(10).Value, Boolean) = False _
                '    AndAlso Trim(dgvResults.Rows(e.RowIndex).Cells(2).Value.ToString) <> "") Then
                '        dgvResults.Rows(e.RowIndex).Cells(10).Value = True
                '    Else
                '        dgvResults.Rows(e.RowIndex).Cells(10).Value = False
                '    End If
            End If
        End If
        If isdirty Then
            Dim IMG As Byte()
            If dgvResults.Rows(e.RowIndex).Cells(18).Value IsNot Nothing Then


                IMG = BitmapToBytes(dgvResults.Rows(e.RowIndex).Cells(18).Value)
            Else
                IMG = Nothing
            End If
            FB(0) = dgvResults.Rows(e.RowIndex).Cells(4).Value
            If dgvResults.Rows(e.RowIndex).Cells(4).Style.BackColor = Color.Yellow Then
                FB(1) = "Caution"
            ElseIf dgvResults.Rows(e.RowIndex).Cells(4).Style.BackColor = Color.Red Then
                FB(1) = "Panic"
            Else
                FB(1) = "Ignore"
            End If


            SaveResultChange(Val(dgvResults.Rows(e.RowIndex).Cells(12).Value),
            dgvResults.Rows(e.RowIndex).Cells(13).Value, dgvResults.Rows(e.RowIndex).Cells(14).Value,
            dgvResults.Rows(e.RowIndex).Cells(15).Value, dgvResults.Rows(e.RowIndex).Cells(0).Value,
            origRes, dgvResults.Rows(e.RowIndex).Cells(2).Value, FB(0), FB(1),
            dgvResults.Rows(e.RowIndex).Cells(16).Value, dgvResults.Rows(e.RowIndex).Cells(17).Value,
            IMG, dgvResults.Rows(e.RowIndex).Cells(10).Value, CDate(txtRptDate.Text & " " & txtRptTime.Text), e.RowIndex)
            isdirty = False
            AccDirty = True
        End If
    End Sub

    Private Sub DecorateGridRow(ByVal RowIndex As Integer)
        ' *** Deta Check ****
        If DeltaCheck(dgvResults.Rows(RowIndex).Cells(12).Value,
        dgvResults.Rows(RowIndex).Cells(0).Value,
        dgvResults.Rows(RowIndex).Cells(2).Value) = False Then
            If Not dgvResults.Rows(RowIndex).Cells(1).Value.ToString.StartsWith(ChrW(8710)) _
            Then dgvResults.Rows(RowIndex).Cells(1).Value = ChrW(8710) & " " &
            dgvResults.Rows(RowIndex).Cells(1).Value
            dgvResults.Rows(RowIndex).Cells(4).Style = PanicStyle
            dgvResults.Rows(RowIndex).Cells(1).Style.ForeColor = Color.Red
            If InStr(dgvResults.Rows(RowIndex).Cells(16).Value,
            ChrW(8710) & " Delta Check failed" & vbCrLf) = 0 Then _
            dgvResults.Rows(RowIndex).Cells(16).Value =
            ChrW(8710) & " Delta Check failed" & vbCrLf &
            dgvResults.Rows(RowIndex).Cells(16).Value
        Else
            If InStr(dgvResults.Rows(RowIndex).Cells(1).Value.ToString,
            ChrW(8710)) > 0 Then dgvResults.Rows(RowIndex).Cells(1).Value =
            Replace(dgvResults.Rows(RowIndex).Cells(1).Value, ChrW(8710) & " ", "")
            dgvResults.Rows(RowIndex).Cells(4).Style.BackColor =
            dgvResults.Rows(RowIndex).Cells(5).Style.BackColor
            dgvResults.Rows(RowIndex).Cells(1).Style.ForeColor =
            dgvResults.Rows(RowIndex).Cells(5).Style.ForeColor
            dgvResults.Rows(RowIndex).Cells(16).Value =
            Replace(dgvResults.Rows(RowIndex).Cells(16).Value,
            ChrW(8710) & " Delta Check failed" & vbCrLf, "")
        End If
        ' *** End Delta Check ****
        If dgvResults.Rows(RowIndex).Cells(13).Value = "REF" Then
            dgvResults.Rows(RowIndex).Cells(3).Value =
            System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Reflux.ico")
        ElseIf dgvResults.Rows(RowIndex).Cells(11).Value = True Then 'QL
            dgvResults.Rows(RowIndex).Cells(3).Value =
            System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Looks.ico")
        End If
        '
        If dgvResults.Rows(RowIndex).Cells(16).Value <> "" Then
            dgvResults.Rows(RowIndex).Cells(7).Value =
            System.Drawing.Image.FromFile(Application.StartupPath &
            "\Images\Note.ico")
        Else
            dgvResults.Rows(RowIndex).Cells(7).Value =
            System.Drawing.Image.FromFile(Application.StartupPath &
            "\Images\NoteBlank.ico")
        End If
        If dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value <> Nothing Then
            If dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value.ToString().Contains("Delta Check failed") Then
                Alert.Text = "∆ Delta Check failed"
            Else

            End If
        Else


        End If
        If dgvResults.Rows(RowIndex).Cells(17).Value <> "" Then
            dgvResults.Rows(RowIndex).Cells(9).Value =
            System.Drawing.Image.FromFile(Application.StartupPath &
            "\Images\rtf.ico")
        Else
            dgvResults.Rows(RowIndex).Cells(9).Value =
            System.Drawing.Image.FromFile(Application.StartupPath _
            & "\Images\rtfBlank.ico")
        End If
    End Sub

    Public Function BitmapToBytes(bitmap As Bitmap) As Byte()
        Using ms As New MemoryStream()
            ' Save the bitmap to the memory stream in a specific image format
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
            ' Return the byte array
            Return ms.ToArray()
        End Using
    End Function
    Private Sub dgvResults_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvResults.DataError
        On Error Resume Next
    End Sub

    Private Sub txtRptDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRptDate.KeyPress
        AccDirty = True
    End Sub

    Private Sub txtRptDate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRptDate.Validated
        ValidateReported()
    End Sub

    Private Sub ValidateReported()
        If UserEnteredText(txtRptDate) <> "" Then
            If Not IsDate(txtRptDate.Text & " " & txtRptTime.Text) Then
                Dim RetVal As Integer = MsgBox("Oops! the date you entered is not valid. Either enter a valid " &
                "date (and time) by pressing the 'No' button or accept the Prolis assigned date and time by " &
                "pressing the 'Yes' button. What is your option?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo, "Prolis")
                If RetVal = vbYes Then
                    If Not IsDate(txtRptDate.Text) Then
                        txtRptDate.Focus()
                    ElseIf Not IsDate(txtRptTime.Text) Then
                        txtRptTime.Focus()
                    End If
                Else
                    Dim Reported As Date = GetReported(Val(cmbAccCtl.Text))
                    txtRptDate.Text = Reported
                    txtRptTime.Text = Reported
                End If
            Else
                Dim RecDate As Date = GetRecDate(Val(cmbAccCtl.Text))
                If CDate(txtRptDate.Text & " " & txtRptTime.Text) <= RecDate Then
                    MsgBox("The Report Date must be greater than the Accession Receive Date, '" &
                    RecDate & "'. Please change the Report Date", MsgBoxStyle.Critical, "Prolis")
                    txtRptDate.Text = ""
                    txtRptTime.Text = Format(Date.Now, "HH:mm")
                End If
            End If
        End If
    End Sub

    Private Sub txtRptTime_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRptTime.Validated
        ValidateReported()
    End Sub

    Private Function GetRecDate(ByVal AccID As Long) As Date
        Dim RecDate As Date = Nothing
        Dim cnr As New SqlClient.SqlConnection(connString)
        cnr.Open()
        Dim cmdr As New SqlClient.SqlCommand("Select ReceivedTime from Requisitions where ID = " & AccID, cnr)
        cmdr.CommandType = CommandType.Text
        Dim drr As SqlClient.SqlDataReader = cmdr.ExecuteReader
        If drr.HasRows Then
            While drr.Read
                If drr("ReceivedTime") IsNot DBNull.Value _
                AndAlso drr("ReceivedTime") <> "#12:00:00 AM#" _
                Then RecDate = drr("ReceivedTime")
            End While
        End If
        cnr.Close()
        cnr = Nothing
        Return RecDate
    End Function

    Private Sub cmbAccCtl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbAccCtl.KeyPress
        If e.KeyChar = Chr(13) Then
            btnLoad.Focus()
        End If
    End Sub

    Private Sub frmResults_ResizeBegin(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeBegin
        origWidth = Me.Width
        origHeight = Me.Height
    End Sub

    Private Sub frmResults_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        MeResize(Me, origWidth, origHeight)
    End Sub

    Private Sub dtpFromDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpFromDate.ValueChanged, dtpToDate.ValueChanged
        cmbRun.Text = ""
        cmbAccCtl.Text = ""
        cmbRun.SelectedIndex = -1
        cmbAccCtl.SelectedIndex = -1
        dgvResults.Rows.Clear()
        If btnAccQC.Checked = True Then 'QC
            PopulateRuns(dtpFromDate.Value, GetEndDate(dtpToDate.Value))
        End If
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        'InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.


        InitializeComponent()
        InitializeBrowser()
        'InitializeBrowser(url).GetAwaiter().GetResult()
    End Sub

    Private Sub txtNote_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNote.Validated
        If btnAccQC.Checked = False Then    'ACC
            If dgvResults.RowCount > 0 Then _
            SaveAccessionComment(Val(dgvResults.Rows(0).Cells(12).Value), Trim(txtNote.Text))
            AccDirty = True
        End If
    End Sub
    Private Sub btnDelPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelPDF.Click
        ExtPDF = Nothing
        If dgvResults.RowCount > 0 Then _
        ExecuteSqlProcedure("Delete from Extend_Results where Accession_ID = " & dgvResults.Rows(0).Cells(12).Value)
        'AxAcroPDF1.src = "ClearContent.PDF"
        'PdfViewer1.Close()
        'PdfViewer1.Refresh()

        ClearWebView()

        chkExtRelease.Checked = False
        btnBrowse.Enabled = True
        btnDelPDF.Enabled = False
    End Sub
    Private Async Function btnBrowse_ClickAsync(ByVal sender As Object, ByVal e As System.EventArgs) As Task Handles btnBrowse.Click
        If dgvResults.RowCount > 0 Then
            OpenFileDialog1.Filter = "PDF Files (*.PDF)|*.PDF"
            If OpenFileDialog1.ShowDialog = DialogResult.OK Then
                'Dim filePath As String = Replace(AxAcroPDF1.src, "File://", "")
                Dim filename As String = IO.Path.GetFileName(OpenFileDialog1.FileName)
                Dim fs As IO.FileStream = New IO.FileStream(OpenFileDialog1.FileName,
                IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Dim br As IO.BinaryReader = New IO.BinaryReader(fs)
                ExtPDF = br.ReadBytes(Convert.ToInt32(fs.Length))
                br.Close()
                fs.Close()
                'File.WriteAllBytes("C:/Users/hp/AppData/Roaming/Prolis/tempPdfFile_2311210056_0_2025021322440881212.pdf", ExtPDF)
                'If AxAcroPDF1.LoadFile(OpenFileDialog1.FileName) Then
                'AxAcroPDF1.src = OpenFileDialog1.FileName

                'PdfViewer1.Open(OpenFileDialog1.FileName)

                '************************************************
                '' Initialize WebView2 control
                'Await WebView.EnsureCoreWebView2Async(Nothing)

                '' Specify the path to the PDF file
                Dim pdfPath As String = OpenFileDialog1.FileName

                '' Load the PDF file into the WebView2 control
                'WebView.CoreWebView2.Navigate(pdfPath)

                WebBrowser1.Navigate(pdfPath)
                '************************************************
                chkExtRelease.Checked = SystemConfig.ReleaseWithEntry
                btnDelPDF.Enabled = True
                txtRptDate.Text = Format(Date.Today, SystemConfig.DateFormat)
                txtRptTime.Text = Format(Date.Now, "HH:mm")
                SaveExtendedResult(dgvResults.Rows(0).Cells(12).Value, ExtPDF)
                AccDirty = True
            End If
        End If
    End Function

    Private Sub chkExtRelease_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkExtRelease.CheckedChanged
        If ExtPDF IsNot Nothing And btnAccQC.Checked = False Then
            txtRptDate.Text = Format(Date.Today, SystemConfig.DateFormat)
            txtRptTime.Text = Format(Date.Now, "HH:mm")
            '
            SaveExtResultChange(dgvResults.Rows(0).Cells(12).Value,
            ExtPDF, chkExtRelease.Checked, CDate(txtRptDate.Text & " " & txtRptTime.Text))
            AccDirty = True
        End If
    End Sub

    Private Sub txtFiller_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFiller.TextChanged
        If txtFiller.Text <> "" Then
            btnFill.Enabled = True
        Else
            btnFill.Enabled = False
        End If
    End Sub

    Private Sub btnFill_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFill.Click
        If Trim(txtFiller.Text) <> "" Then
            Dim FB() As String = {"", ""}
            For i As Integer = 0 To dgvResults.RowCount - 1
                If dgvResults.Rows(i).Cells(2).Value Is Nothing OrElse
                Trim(dgvResults.Rows(i).Cells(2).Value) = "" Then
                    dgvResults.Rows(i).Cells(2).Value = Trim(txtFiller.Text)
                    FB = GetFlag(Val(dgvResults.Rows(i).Cells(12).Value),
                    dgvResults.Rows(i).Cells(2).Value, dgvResults.Rows(i).Cells(0).Value)
                    dgvResults.Rows(i).Cells(4).Value = FB(0)
                    If FB(1) = "Caution" Then
                        dgvResults.Rows(i).Cells(4).Style = AbnormalStyle
                    ElseIf FB(1) = "Panic" Then
                        dgvResults.Rows(i).Cells(4).Style = PanicStyle
                    End If
                    Dim IMG As Byte()
                    If dgvResults.Rows(i).Cells(18).Value IsNot Nothing Then
                        IMG = BitmapToBytes(dgvResults.Rows(i).Cells(18).Value)
                    Else
                        IMG = Nothing
                    End If
                    txtRptDate.Text = Format(Date.Now, SystemConfig.DateFormat)
                    txtRptTime.Text = Format(Date.Now, "HH:mm")
                    SaveResultChange(Val(dgvResults.Rows(i).Cells(12).Value),
                    dgvResults.Rows(i).Cells(13).Value, dgvResults.Rows(i).Cells(14).Value,
                    dgvResults.Rows(i).Cells(15).Value, dgvResults.Rows(i).Cells(0).Value,
                    origRes, dgvResults.Rows(i).Cells(2).Value, FB(0), FB(1),
                    dgvResults.Rows(i).Cells(16).Value, dgvResults.Rows(i).Cells(17).Value,
                    IMG, dgvResults.Rows(i).Cells(10).Value, CDate(txtRptDate.Text & " " & txtRptTime.Text), i)
                    AccDirty = True
                End If
            Next
            txtFiller.Text = ""
            AccDirty = True
        End If
    End Sub

    Private Sub chkIncomplete_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIncomplete.CheckedChanged
        If chkIncomplete.Checked = False Then   'All
            chkIncomplete.Text = "All"
        Else
            chkIncomplete.Text = "Incomplete"
        End If
    End Sub

    Private Sub btnDeleteHistory_Click(sender As Object, e As EventArgs) Handles btnDeleteHistory.Click
        If Trim(cmbAccCtl.Text) <> "" Then
            Dim RetVal As Integer = MsgBox("This action will delete the resulting history (a CLIA requirement) " &
            "of the accession. Only delete the history if it got built accidently. Click 'Yes' to proceed.",
            MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Prolis")
            If RetVal = vbYes Then
                ExecuteSqlProcedure("Delete from Acc_Results_History where Accession_ID = " & Trim(cmbAccCtl.Text))
            End If
        End If
    End Sub

    Private Sub cmbAccCtl_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles cmbAccCtl.MouseDoubleClick
        cmbAccCtl.Text = Clipboard.GetText()
    End Sub

    'Private Sub PdfViewer1_FileOpened(sender As Object, e As Foxit.PDF.Viewer.FileOpenedEventArgs) Handles PdfViewer1.FileOpened
    '    If dgvResults.RowCount > 0 Then
    '        'Dim filePath As String = Replace(AxAcroPDF1.src, "File://", "")   Dim filename As String = IO.Path.GetFileName(OpenFileDialog1.FileName)
    '        Dim fs As IO.FileStream = New IO.FileStream(PdfViewer1.FilePath,
    '            IO.FileMode.Open, IO.FileAccess.ReadWrite)
    '        Dim br As IO.BinaryReader = New IO.BinaryReader(fs)
    '        ExtPDF = br.ReadBytes(Convert.ToInt32(fs.Length))
    '        br.Close()
    '        fs.Close()
    '        chkExtRelease.Checked = SystemConfig.ReleaseWithEntry
    '        btnDelPDF.Enabled = True
    '        txtRptDate.Text = Format(Date.Today, SystemConfig.DateFormat)
    '        txtRptTime.Text = Format(Date.Now, "HH:mm")
    '        SaveExtendedResult(dgvResults.Rows(0).Cells(12).Value, ExtPDF)
    '        AccDirty = True
    '    End If
    'End Sub

    Private Sub Label19_Click(sender As Object, e As EventArgs) Handles Label19.Click
        cmbAccCtl.Text = Clipboard.GetText()
    End Sub

    Private Sub btnPrintReport_Click(sender As Object, e As EventArgs) Handles btnPrintReport.Click
        Me.Cursor = Cursors.WaitCursor
        If cmbAccCtl.Text = "" Then
            Return
        End If
        Dim RPTFile = ValidateReportFile(cmbAccCtl.Text, False)

        ' Convert single AccID value to an array
        Dim accIDsArray() As String = {cmbAccCtl.Text}
        GenerateReports(accIDsArray, 1, False)
        Me.Cursor = Cursors.Default
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

            '*************************** FOR RPT OPTIMIZATION TASK BY AQEEL 14 MAY 2024
            ExecuteSqlProcedure("Exec usp_Acc_Result '" & AccID & "' ")
            '***************************
            ''
            'Todo: Crystal Report Code
            '============================================================
            'Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            'oRpt.Load(RPTFile)
            'ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database,
            'My.Settings.UID, My.Settings.PWD)
            'oRpt.RecordSelectionFormula = "{Requisitions.ID} = " & AccID & " AND {Acc_Results.Result} <> '.'"
            ''Dim S As New MemoryStream ''= oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat)
            ''SPDFS.Add(S.ToArray)

            '' Assuming oRpt is your Crystal Report object
            ''Dim S As New MemoryStream()
            ''oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat).CopyTo(S)
            ''S.Position = 0 ' Reset the position to the beginning of the stream


            'Try
            '    Dim S As New MemoryStream()
            '    oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat).CopyTo(S)
            '    S.Position = 0


            '    'S = oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat)
            '    SPDFS.Add(S.ToArray)
            '    S.Close()
            '    S = Nothing
            'Catch ex As Exception
            '    Dim err As String = ex.Message()
            'End Try


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
            '============================================================
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

            Dim filePath As String = FolderPath & AccID & ".PDF"
            If Device = 0 Then  'printer
                'Dim PS As New System.Drawing.Printing.PrinterSettings
                'PDFDOC.PrintSettings.PrinterName = PS.PrinterName
                'PDFDOC.Print()
                'My.Application.DoEvents()
            ElseIf Device = 1 Then  'screen

                Dim f As New frmWebView()
                f.LoadPdfData(FPDF)
                f.MdiParent = frmDashboard
                f.Show()

            End If
        End If
    End Sub




    Private Sub LoadPdfAndStatus(pdfPath As String, released As Boolean)
        'WebView.CoreWebView2.Navigate(pdfPath)
        WebBrowser1.Navigate(pdfPath)
        chkExtRelease.Checked = released
    End Sub

    'Private Sub UpdateNavigationButtons()
    '    btnPrevious.Enabled = currentIndex > 0
    '    btnNext.Enabled = currentIndex < pdfPaths.Count - 1
    'End Sub

    Private Function SavePdfToTempFile(AccID As String, pdfData As Byte(), index As Integer) As String

        ' Determine the directory path using Application Data folder and product name
        Dim folderPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName)

        ' Create the directory if it does not exist
        If Not Directory.Exists(folderPath) Then
            Directory.CreateDirectory(folderPath)
        End If

        ' Define the full file path with AccID and index
        Dim tempFilePath As String = Path.Combine(folderPath, $"tempPdfFile_{AccID}_{index}_{DateTime.UtcNow.ToString("yyyyMMddHHmmssfff")}.pdf")

        ' Write the PDF data to the specified path
        File.WriteAllBytes(tempFilePath, pdfData)

        ' Return the full file path
        Return tempFilePath

    End Function
    Private Function SavePdfToTempFile1(pdfData As Byte(), index As Integer) As String
        ' Define the file path for the temporary PDF file
        Dim tempFilePath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName, "tempPdfFile" & index & ".pdf")
        KillProcessHoldingFile(tempFilePath)
        Dim retries As Integer = 5
        Dim success As Boolean = False

        ' Retry loop to handle file locking issues
        While retries > 0 AndAlso Not success
            Try
                ' Check if the file is locked by another process
                If Not IsFileLocked(tempFilePath) Then
                    ' Write the PDF data to the file
                    File.WriteAllBytes(tempFilePath, pdfData)
                    success = True ' Exit loop on successful write
                Else
                    ' File is locked, wait and retry
                    retries -= 1
                    '   MessageBox.Show("The file is currently in use by another process. Retrying...")
                    Thread.Sleep(500) ' Wait for 500 ms before retrying
                End If
            Catch ex As IOException
                ' Handle any IO exceptions, such as access being denied
                MessageBox.Show("An error occurred while writing to the file: " & ex.Message)
                retries -= 1
                Thread.Sleep(500) ' Wait before retrying
            End Try
        End While

        ' If retries reach 0 and the file is still locked
        If Not success Then
            MessageBox.Show("Failed to save the PDF file after multiple attempts. It might be locked by another process.")
        End If

        Return tempFilePath
    End Function

    Public Sub KillProcessHoldingFile(filePath As String)
        Try
            Dim knownProcesses As String() = {"AcroRd32", "explorer"} ' Add known processes that may lock the file

            ' Attempt to open the file with exclusive access to check if it's locked
            If IsFileLocked(filePath) Then
                ' Iterate through all running processes
                For Each proc As Process In Process.GetProcesses()
                    If knownProcesses.Contains(proc.ProcessName) Then
                        ' Check if this process is likely using the file
                        If IsProcessUsingFile(proc, filePath) Then
                            ' Terminate the process
                            proc.Kill()
                            proc.WaitForExit() ' Ensure the process has exited
                            '   MessageBox.Show($"Terminated process {proc.ProcessName} holding the file.")
                            Return
                        End If
                    End If
                Next
            Else
                '  MessageBox.Show("The file is not locked by any known process.")
            End If
        Catch ex As Exception
            MessageBox.Show("Error while attempting to terminate process: " & ex.Message)
        End Try
    End Sub

    Private Function IsFileLocked(filePath As String) As Boolean
        Try
            ' Try to open the file with exclusive access
            Using fileStream As New FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None)
                ' If the code reaches here, the file is not locked
            End Using
            Return False
        Catch ex As IOException
            ' The file is locked by another process
            Return True
        End Try
    End Function

    ' Helper function to check if the process is using the file
    Private Function IsProcessUsingFile(proc As Process, filePath As String) As Boolean
        Try
            ' Check if the process has the file locked (based on known process names)
            ' Here, you can extend it to match more specific criteria as needed
            Return proc.MainModule.FileName.Contains(filePath)
        Catch ex As Exception
            ' Some processes might throw an exception if accessed (e.g., system processes)
            Return False
        End Try
    End Function


    Private Function GetAllPdfsFromDatabase(ByVal AccID As Long) As List(Of PdfItem)
        LoggerHelper.LogInfo("Starting GetAllPdfsFromDatabase")

        Dim pdfList As New List(Of PdfItem)()

        Using connection As New SqlConnection(connString)
            connection.Open()
            Dim query As String = "Select * from Extend_Results where Accession_ID = " & AccID
            Using command As New SqlCommand(query, connection)
                Dim reader As SqlDataReader = command.ExecuteReader()
                While reader.Read()
                    Dim pdfItem As New PdfItem() With {
                    .PdfData = CType(reader("Result"), Byte()),
                    .Released = CType(reader("Released"), Boolean)
                }
                    pdfList.Add(pdfItem)
                End While
            End Using
        End Using
        'File.WriteAllBytes("C:/Users/hp/AppData/Roaming/Prolis/tempPdfFile_2311210056_0_202502132244088122.pdf", pdfList.First().PdfData)

        Return pdfList
    End Function

    Public Class PdfItem
        Public Property PdfData As Byte()
        Public Property Released As Boolean
    End Class

    Private ReadOnly blankHtml As String = "<html><body></body></html>"

    Private Sub ClearWebView()
        'If WebView.CoreWebView2 IsNot Nothing Then
        '    WebView.NavigateToString(blankHtml)
        'End If

        Try
            LoggerHelper.LogInfo("Starting ClearWebView")

            If WebBrowser1 IsNot Nothing Then
                WebBrowser1.Navigate("")
            End If
        Catch ex As Exception
            LoggerHelper.LogError(ex, "Error ClearWebView")
        End Try

    End Sub

    Private Sub dgvResults_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs)

        If e.Button = MouseButtons.Right AndAlso e.ColumnIndex = 1 Then

            If dgvResults.Rows.Count > 0 AndAlso e.RowIndex >= 0 AndAlso e.RowIndex < dgvResults.Rows.Count Then

                dgvResults.CurrentCell = dgvResults.Rows(e.RowIndex).Cells(e.ColumnIndex)
                dgvResults.Rows(e.RowIndex).Selected = True
                selectedTestID = dgvResults.Rows(e.RowIndex).Cells(0).Value.ToString()
                contextMenuStrip.Show(Cursor.Position)
            Else
                MessageBox.Show("No data available to select.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub OpenTestsMenuItem_Click(sender As Object, e As EventArgs)
        If Not String.IsNullOrEmpty(selectedTestID) Then

            Dim openTestsForm As New frmTests()
            openTestsForm.TestID = selectedTestID
            openTestsForm.ShowDialog()
        Else
            MessageBox.Show("Please select a valid TestID.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub



    'Public Sub New(Optional url As String = Nothing)
    '    InitializeComponent()
    '    InitializeBrowser(url)
    '    'InitializeBrowser(url).GetAwaiter().GetResult()

    'End Sub

    Private Async Sub InitializeBrowser(Optional url As String = Nothing)
        Try
            'Dim userDataFolder As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName)
            'Dim env As CoreWebView2Environment = Await CoreWebView2Environment.CreateAsync(Nothing, userDataFolder)
            'Await WebView.EnsureCoreWebView2Async(env)
            'If url IsNot Nothing Then
            '    WebView.Source = New UriBuilder(If(url, url)).Uri
            'End If

        Catch ex As Exception
            MessageBox.Show("Error initializing browser: " & ex.Message)
        End Try
    End Sub

    Private Function GetEndDate(endOfDay As DateTime) As Date
        Return endOfDay.Date.AddHours(23).AddMinutes(59).AddSeconds(59)
    End Function

    Private Sub dgvResults_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvResults.CellContentClick

    End Sub
End Class