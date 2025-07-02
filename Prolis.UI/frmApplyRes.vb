Option Compare Text
Imports System.Data

Public Class frmApplyRes
    Private Override As Integer
    Private EquipID As Integer
    Private RefRDs() As String = {""}

    Private Sub frmApplyRes_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If BW.IsBusy Then
            e.Cancel = True
        End If
    End Sub

    Private Sub frmApplyRes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CN.Open("DSN=ProlisQC")
        PopulateEquipments()
        lblDate.Text += " (" & SystemConfig.DateFormat & ")"
        If ThisUser.Result_Release Then
            chkRelVal.Enabled = True
        Else
            chkRelVal.Enabled = False
        End If
        chkOverwrite.Checked = False
        If ThisUser.Supervisor Then
            chkOverwrite.Enabled = True
        Else
            chkOverwrite.Enabled = False
        End If
        dgvControls.Visible = False
        automap.Hide()
        txtBatchDate.Text = Format(Date.Today, SystemConfig.DateFormat)
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
        txtRunDate.Text = Format(Date.Now.AddMonths(-1), SystemConfig.DateFormat)
        Todate.Text = Format(Date.Now, SystemConfig.DateFormat)
        chkClear.Checked = SystemConfig.EQ_Results_BufferChk
    End Sub

    Private Sub PopulateEquipments()
        ProcceeStatus.SelectedIndex = 0
        cmbEquips.Items.Clear()
        Dim sSQL As String = "Select distinct * from Equipments where ID in (Select Equipment_ID from Mac_Results)"
        '
        Dim cnPE As New SqlClient.SqlConnection(connString)
        cnPE.Open()
        Dim cmdPE As New SqlClient.SqlCommand(sSQL, cnPE)
        cmdPE.CommandType = CommandType.Text
        Dim drPE As SqlClient.SqlDataReader = cmdPE.ExecuteReader
        If drPE.HasRows Then
            While drPE.Read
                cmbEquips.Items.Add(New MyList(drPE("Name"), drPE("ID")))
            End While
        End If
        cnPE.Close()
        cnPE = Nothing
    End Sub

    Private Sub btnClearTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearTarget.Click
        For i As Integer = 0 To dgvAccs.RowCount - 1
            dgvAccs.Rows(i).Cells(2).Value = ""
        Next
        UpdateApplyProcess()
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        dgvAccs.Rows.Clear()
        If cmbEquips.SelectedIndex <> -1 Then
            Dim ItemX As MyList = cmbEquips.SelectedItem
            If btnAccQC.Checked = False Then
                LoadAccessions(ItemX.ItemData)
            Else
                LoadControls(ItemX.ItemData)
            End If
        End If
        UpdateApplyProcess()
    End Sub

    Private Sub LoadAccessions(ByVal EquipID As Integer)
        dgvAccs.Columns(2).HeaderText = "Accession ID"
        Dim sSQL As String = ""
        Dim AccID As String = ""
        Dim i As Integer = 0
        txtAccQC.Text = i.ToString
        'sSQL = "Select distinct a.Accession_ID as SampleID, convert(nvarchar, a.Run_Date, 101) as RunDate, b.ID as " & _
        '"AccID, a.QP as QP from Mac_Results a left outer join Requisitions b on b.ID = substring(a.Accession_ID, 1, " & _
        '"IIF(CHARINDEX('-', a.Accession_ID) = 0, Len(a.Accession_ID), CHARINDEX('-', a.Accession_ID) - 1)) where " & _
        '"a.QP = 'P' and a.Equipment_ID = " & EquipID

        If SpecificAccessions.Text = "" Then
            sSQL = "Select distinct Accession_ID as SampleID, Accession_ID as AccID, " &
       "Run_Date as RunDate, QP as QP from Mac_Results where  Equipment_ID = " & EquipID
            If IsDate(txtRunDate.Text) = True Then sSQL += " and Run_Date between '" &
        txtRunDate.Text & "' and '" & Todate.Text & " 23:59:00'"
        Else
            If SpecificAccessions.Text.Contains(",") Then
                Dim accessionsArray As String() = SpecificAccessions.Text.Split(","c)

                ' Construct a new string with single-quoted values
                Dim result As String = String.Join(",", accessionsArray.Select(Function(s) "'" & s.Trim() & "'"))

                sSQL = "Select distinct Accession_ID as SampleID, Accession_ID as AccID, " &
                        "Run_Date as RunDate, QP as QP from Mac_Results where Accession_ID in (" & result & ") and  Equipment_ID = " & EquipID
                If IsDate(txtRunDate.Text) = True Then sSQL += " and Run_Date between '" &
                txtRunDate.Text & "' and '" & Todate.Text & " 23:59:00'"
            Else
                sSQL = "Select distinct Accession_ID as SampleID, Accession_ID as AccID, " &
                        "Run_Date as RunDate, QP as QP from Mac_Results where Accession_ID like '%" & SpecificAccessions.Text & "%' and  Equipment_ID = " & EquipID
                If IsDate(txtRunDate.Text) = True Then sSQL += " and Run_Date between '" &
                txtRunDate.Text & "' and '" & Todate.Text & " 23:59:00'"
            End If
        End If
        '
        Dim cnla As New Data.SqlClient.SqlConnection(connString)
        cnla.Open()
        Dim cmdla As New Data.SqlClient.SqlCommand(sSQL, cnla)
        cmdla.CommandType = Data.CommandType.Text
        Dim dala As New Data.SqlClient.SqlDataAdapter(cmdla)
        Dim TBL As New Data.DataTable
        dala.Fill(TBL)
        cnla.Close()
        cnla = Nothing
        For i = 0 To TBL.Rows.Count - 1
            TBL.Rows(i).Item("AccID") = ValidateAccID(TBL.Rows(i).Item("AccID"))
            If TBL.Rows(i).Item("RunDate") IsNot DBNull.Value Then
                dgvAccs.Rows.Add(TBL.Rows(i).Item("SampleID"),
                Format(TBL.Rows(i).Item("RunDate"), SystemConfig.DateFormat),
                TBL.Rows(i).Item("AccID"), IIf(TBL.Rows(i).Item("QP") = "P", False, True))
            Else
                dgvAccs.Rows.Add(TBL.Rows(i).Item("SampleID"), "", TBL.Rows(i).Item("AccID"),
                IIf(TBL.Rows(i).Item("QP") = "P", False, True))
            End If
            'If Not IsDate(txtRunDate.Text) Then txtRunDate.Text = _
            'Format(TBL.Rows(i).Item("RunDate"), SystemConfig.DateFormat)
        Next
        '
        txtAccQC.Text = i.ToString
    End Sub

    Private Function ValidateAccID(ByVal SampleID As String) As String
        Dim AccID As String = ""
        If InStr(SampleID, "-") > 0 Then SampleID =
        Microsoft.VisualBasic.Mid(SampleID, 1, InStr(SampleID, "-") - 1)
        If IsNumeric(SampleID) Then
            Dim sSQL As String = "Select ID from Requisitions where Received <> 0 and ID = " & Val(SampleID)
            '
            Dim cnVA As New Data.SqlClient.SqlConnection(connString)
            cnVA.Open()
            Dim cmdVA As New Data.SqlClient.SqlCommand(sSQL, cnVA)
            cmdVA.CommandType = Data.CommandType.Text
            Dim drVA As Data.SqlClient.SqlDataReader = cmdVA.ExecuteReader
            If drVA.HasRows Then AccID = SampleID
            cnVA.Close()
            cnVA = Nothing
        End If
        Return AccID
    End Function

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
            txtValid.Text = "Yes"
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
                    txtValid.Text = "Yes"
                Else
                    ExecuteSqlProcedure("Update Runs set Validated = 0 where ID = " & RunID)
                    txtValid.Text = "No"
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
                RunValer = drur2("Validaters")
            End While
        End If
        cnur2.Close()
        cnur2 = Nothing
        '
        If RunValer >= Validaters Then
            txtValid.Text = "Yes"
            ExecuteSqlProcedure("Update Runs set Validated = 1 where ID = " & RunID)
        Else
            txtValid.Text = "No"
            ExecuteSqlProcedure("Update Runs set Validated = 0 where ID = " & RunID)
        End If
        'Catch Ex As Exception
        '   MsgBox(Ex)
        'End Try
    End Sub

    Private Function GetControls(ByVal EquipID As Integer) As String()
        Dim Controls(1) As String
        Controls(0) = ""
        Dim sSQL As String = "Select distinct Control_ID, ControlName from Ana_Control " &
        "where Ana_ID in (Select ID from Anas where Equipment_ID = " & EquipID & ")"
        Dim cnGC As New SqlClient.SqlConnection(connString)
        cnGC.Open()
        Dim cmdGC As New SqlClient.SqlCommand(sSQL, cnGC)
        cmdGC.CommandType = CommandType.Text
        Dim drGC As SqlClient.SqlDataReader = cmdGC.ExecuteReader
        If drGC.HasRows Then
            While drGC.Read
                If Controls(UBound(Controls)) <> "" Then _
                ReDim Preserve Controls(UBound(Controls) + 1)
                Controls(UBound(Controls)) = drGC("ControlName") _
                & " [" & drGC("Control_ID") & "]"
            End While
        End If
        cnGC.Close()
        cnGC = Nothing
        Return Controls
    End Function

    Private Sub LoadControlsAndMap(ByVal EquipID As Integer)
        dgvControls.Rows.Clear()
        Dim Controls() As String = GetControls(EquipID)
        Dim RowIndex As Integer = 0
        Dim cmbcell As DataGridViewComboBoxCell
        Dim sSQL As String = "Select distinct Accession_ID as SampleID, Run_Date " &
        "as RunDate, QP from Mac_Results where QP <> 'P' and Equipment_ID = " & EquipID

        If IsDate(txtRunDate.Text) = True Then sSQL += $" and Run_Date between 
        '{txtRunDate.Text}'  and '{Todate.Text} 23:59:00'"

        Dim cnLC As New SqlClient.SqlConnection(connString)
        cnLC.Open()
        Dim cmdLC As New SqlClient.SqlCommand(sSQL, cnLC)
        cmdLC.CommandType = CommandType.Text
        Dim drLC As SqlClient.SqlDataReader = cmdLC.ExecuteReader
        If drLC.HasRows Then
            While drLC.Read
                dgvControls.Rows.Add(drLC("SampleID"), Format(drLC("RunDate"), SystemConfig.DateFormat), Nothing)
                cmbcell = dgvControls.Rows(dgvControls.RowCount - 1).Cells(2)
                cmbcell.Items.Clear()
                For i As Integer = 0 To Controls.Length - 1
                    cmbcell.Items.Add(Controls(i))
                    cmbcell.Items.IndexOf(i)
                Next
                Dim fk = drLC("SampleID").ToString().Split("_")(0)
                For j As Integer = 0 To cmbcell.Items.Count - 1
                    Dim fff = cmbcell.Items(j)

                    If fff.ToString().Contains(fk) Then
                        cmbcell.Value = fff
                        Exit For
                    End If
                Next

                'dgvControls.Rows(dgvControls.RowCount - 1).Cells(2) = cmbcell
            End While
        End If
        cnLC.Close()
        cnLC = Nothing
    End Sub

    Private Sub LoadControls(ByVal EquipID As Integer)
        dgvControls.Rows.Clear()
        Dim Controls() As String = GetControls(EquipID)
        Dim RowIndex As Integer = 0
        Dim cmbcell As DataGridViewComboBoxCell
        Dim sSQL As String = "Select distinct Accession_ID as SampleID, Run_Date " &
        "as RunDate, QP from Mac_Results where QP <> 'P' and Equipment_ID = " & EquipID
        If IsDate(txtRunDate.Text) = True Then sSQL += " and Run_Date between '" &
        txtRunDate.Text & "' and '" & Todate.Text & " 23:59:00'"
        Dim cnLC As New SqlClient.SqlConnection(connString)
        cnLC.Open()
        Dim cmdLC As New SqlClient.SqlCommand(sSQL, cnLC)
        cmdLC.CommandType = CommandType.Text
        Dim drLC As SqlClient.SqlDataReader = cmdLC.ExecuteReader
        If drLC.HasRows Then
            While drLC.Read
                dgvControls.Rows.Add(drLC("SampleID"), Format(drLC("RunDate"), SystemConfig.DateFormat), Nothing)
                cmbcell = dgvControls.Rows(dgvControls.RowCount - 1).Cells(2)
                cmbcell.Items.Clear()
                For i As Integer = 0 To Controls.Length - 1
                    cmbcell.Items.Add(Controls(i))
                    cmbcell.Items.IndexOf(i)
                Next

                cmbcell.Value = ""

                'dgvControls.Rows(dgvControls.RowCount - 1).Cells(2) = cmbcell
            End While
        End If
        cnLC.Close()
        cnLC = Nothing
    End Sub
    Private Function setCellComboBoxItems(ByVal rowIndex As Integer, ByVal colIndex As Integer, ByVal itemsToAdd() As Object) As DataGridViewComboBoxCell
        Dim dgvcbc As DataGridViewComboBoxCell = New DataGridViewComboBoxCell
        dgvcbc.Items.Clear()
        Dim i As Integer
        For Each itemToAdd As Object In itemsToAdd
            i = dgvcbc.Items.Add(itemToAdd)
        Next
        Return dgvcbc
    End Function

    Private Sub dgvAccs_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAccs.CellEndEdit
        If e.ColumnIndex = 2 Then
            If btnAccQC.Checked = True Then 'Control
                Dim MyVal As String = CType(dgvAccs.Rows(e.RowIndex).Cells(2).Value, String)
                dgvAccs.Rows(e.RowIndex).Cells(2).Value = MyVal
            End If
            UpdateApplyProcess()
        End If
    End Sub

    Private Sub UpdateApplyProcess()
        Dim i As Integer
        Dim HasData As Boolean = False
        If btnAccQC.Checked = False Then
            For i = 0 To dgvAccs.RowCount - 1
                If dgvAccs.Rows(i).Cells(2).Value IsNot Nothing AndAlso
                dgvAccs.Rows(i).Cells(2).Value IsNot System.DBNull.Value AndAlso
                Trim(dgvAccs.Rows(i).Cells(2).Value.ToString) <> "" Then
                    HasData = True
                    Exit For
                End If
            Next
        Else
            For i = 0 To dgvControls.RowCount - 1
                'Dim CmbCell As DataGridViewComboBoxCell = CType(dgvControls.Rows(i).Cells(2).Value, DataGridViewComboBoxCell)
                'Dim itemX As MyList = CmbCell.Items.Item(se)
                If dgvControls.Rows(i).Cells(2).Value IsNot Nothing _
                AndAlso dgvControls.Rows(i).Cells(2).Value <> "" Then
                    HasData = True
                    Exit For
                End If
            Next
        End If
        If HasData = True Or chkClear.Checked = True Then
            If ThisUser.Result_Entry Then btnProcess.Enabled = True
        Else
            btnProcess.Enabled = False
        End If
    End Sub

    Private Function ResultIsPanic(ByVal AccID As Long, ByVal TestID As Integer, ByVal Res As String) As Boolean
        Dim IsPanic As Boolean = False
        Dim Flag() As String = GetFlag(AccID, Res, TestID)
        If Flag(1) = "Panic" Then IsPanic = True
        Return IsPanic
    End Function

    Private Function GetPatientAgeSex(ByVal AccID As Long) As String()
        Dim AgeSex() As String = {"-1", ""}
        If btnAccQC.Checked = False Then    'Accession
            Dim sSQL As String = "Select DOB, Sex from Patients where ID in " &
            "(Select Patient_ID from Requisitions where ID = " & AccID & ")"
            '
            Dim cnPAS As New SqlClient.SqlConnection(connString)
            cnPAS.Open()
            Dim cmdPAS As New SqlClient.SqlCommand(sSQL, cnPAS)
            cmdPAS.CommandType = CommandType.Text
            Dim drPAS As SqlClient.SqlDataReader = cmdPAS.ExecuteReader
            If drPAS.HasRows Then
                While drPAS.Read
                    AgeSex(0) = DateDiff(DateInterval.Year, drPAS("DOB"), Date.Today)
                    AgeSex(1) = Trim(drPAS("Sex"))
                End While
            End If
            cnPAS.Close()
            cnPAS = Nothing
        End If
        Return AgeSex
    End Function

    Private Function GetEquipmentResult(ByVal EquipID As Integer,
     ByVal SMPLID As String, ByVal ResultID As String) As String
        Dim Result As String = ""
        Dim sSQL As String = "Select * from Mac_Results where Accession_ID = '" & SMPLID &
        "' and Equipment_ID = " & EquipID & " and Test_ID = '" & ResultID & "'"
        '
        Dim cnGER As New SqlClient.SqlConnection(connString)
        cnGER.Open()
        Dim cmdGER As New SqlClient.SqlCommand(sSQL, cnGER)
        cmdGER.CommandType = CommandType.Text
        Dim drGER As SqlClient.SqlDataReader = cmdGER.ExecuteReader
        If drGER.HasRows Then
            While drGER.Read
                If drGER("Result") IsNot DBNull.Value _
                AndAlso Trim(drGER("Result")) <> "" _
                Then Result = Trim(drGER("Result"))
            End While
        End If
        cnGER.Close()
        cnGER = Nothing
        Return Result
    End Function

    Private Function GetEquipmentResultID(ByVal EquipID As Integer, ByVal TestID As Integer) As String
        Dim ResID As String = ""
        Dim sSQL As String = "Select EqResult_ID from Ana_Test where Test_ID = " & TestID &
        " and Ana_ID in (Select ID from Anas where Equipment_ID = " & EquipID & ")"
        Dim cnGERI As New SqlClient.SqlConnection(connString)
        cnGERI.Open()
        Dim cmdGERI As New SqlClient.SqlCommand(sSQL, cnGERI)
        cmdGERI.CommandType = CommandType.Text
        Dim drGERI As SqlClient.SqlDataReader = cmdGERI.ExecuteReader
        If drGERI.HasRows Then
            While drGERI.Read
                If drGERI("EqResult_ID") IsNot DBNull.Value _
                AndAlso Trim(drGERI("EqResult_ID")) <> "" _
                Then ResID = Trim(drGERI("EqResult_ID"))
            End While
        End If
        cnGERI.Close()
        cnGERI = Nothing
        Return ResID
    End Function

    Private Function GetEquipmentResultIDs(ByVal EquipID As Integer) As String
        Dim ResultIDs As String = ""
        Dim sSQL As String = "Select distinct EqResult_ID from Ana_Test where " &
        "Ana_ID in (Select ID from Anas where Equipment_ID = " & EquipID & ")"
        Dim cnER As New SqlClient.SqlConnection(connString)
        cnER.Open()
        Dim cmdER As New SqlClient.SqlCommand(sSQL, cnER)
        cmdER.CommandType = CommandType.Text
        Dim drER As SqlClient.SqlDataReader = cmdER.ExecuteReader
        If drER.HasRows Then
            While drER.Read
                If drER("EqResult_ID") IsNot DBNull.Value _
                AndAlso Trim(drER("EqResult_ID")) <> "" Then
                    ResultIDs += "'" & Trim(drER("EqResult_ID")) & "', "
                End If
            End While
            If ResultIDs.EndsWith(", ") Then ResultIDs = ResultIDs.Substring(0, Len(ResultIDs) - 2)
        End If
        cnER.Close()
        cnER = Nothing
        Return ResultIDs
    End Function

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        'On Error Resume Next
        If cmbEquips.SelectedIndex <> -1 Then
            Dim ItemX As MyList = cmbEquips.SelectedItem
            EquipID = ItemX.ItemData
            Dim i As Integer
            Dim n As Integer
            Dim IsDirty As Boolean = False
            Dim PTestID() As String
            Dim Result As String = ""
            Dim Flag As String = ""
            Dim ControlID As String = ""
            Dim Samples As String = ""
            Dim SMPLID As String = ""
            Dim SrcID As String = ""
            Dim sSQL As String = ""
            If btnAccQC.Checked = True Then    'QC
                If cmbBatches.SelectedIndex <> -1 Then
                    Dim ItemR As MyList = cmbBatches.SelectedItem
                    For i = 0 To dgvControls.RowCount - 1
                        Samples += "'" & dgvControls.Rows(i).Cells(0).Value & "', "
                        'IsDirty = False
                        If dgvControls.Rows(i).Cells(2).Value IsNot Nothing AndAlso
                        dgvControls.Rows(i).Cells(2).Value.ToString <> "" Then    'Control assigned
                            'Dim cmbcell As DataGridViewComboBoxCell = CType(dgvControls.Rows(i).Cells(2), DataGridViewComboBoxCell)
                            ControlID = Trim(dgvControls.Rows(i).Cells(2).Value.ToString.Substring(InStr(dgvControls.Rows(i).Cells(2).Value, "[")))
                            ControlID = Trim(ControlID.Substring(0, InStr(ControlID, "]") - 1))
                            Dim cnmac As New SqlClient.SqlConnection(connString)
                            cnmac.Open()
                            Dim cmdmac As New SqlClient.SqlCommand("Select * from Mac_Results " &
                            "where QP = 'Q' and Equipment_ID = " & EquipID & " and Accession_ID = '" &
                            dgvControls.Rows(i).Cells(0).Value & "'", cnmac)
                            cmdmac.CommandType = CommandType.Text
                            Dim drmac As SqlClient.SqlDataReader = cmdmac.ExecuteReader
                            If drmac.HasRows Then
                                While drmac.Read
                                    PTestID = GetPTestIDs(ItemX.ItemData,
                                    dgvControls.Rows(i).Cells(2).Value, drmac("Test_ID"), SrcID)
                                    If PTestID(0) <> "" Then
                                        For n = 0 To PTestID.Length - 1
                                            If IsQCInRange(ControlID, Val(PTestID(n)), Trim(drmac("Result"))) = False Then
                                                Flag = "OUT"
                                            Else
                                                Flag = "IN"
                                            End If
                                            If chkOverwrite.Checked = False And chkRelVal.Checked = False Then 'no ow and no release
                                                sSQL = "Update QC_Results set Result = '" & Trim(drmac("Result")) & "', Flag = '" &
                                                Flag & "', Released = 0 where (Result is null or Result = '') and Run_ID = " &
                                                ItemR.ItemData & " and Control_ID = " & ControlID & " and " & "Test_ID = " & Val(PTestID(n))
                                            ElseIf chkOverwrite.Checked = False And chkRelVal.Checked = True Then 'no ow and release
                                                sSQL = "Update QC_Results set Result = '" & Trim(drmac("Result")) & "', Flag = '" &
                                                Flag & "', Released = 1, Released_By = " & ThisUser.ID & ", Release_Time = '" &
                                                Date.Now & "' where (Result is null or Result = '') and Run_ID = " &
                                                ItemR.ItemData & " and Control_ID = " & ControlID & " and " & "Test_ID = " & Val(PTestID(n))
                                            ElseIf chkOverwrite.Checked = True And chkRelVal.Checked = False Then 'ow and no release
                                                sSQL = "Update QC_Results set Result = '" & Trim(drmac("Result")) &
                                                "', Flag = '" & Flag & "', Released = 0 where Run_ID = " & ItemR.ItemData &
                                                " and Control_ID = " & ControlID & " and " & "Test_ID = " & Val(PTestID(n))
                                            Else    'ow and release
                                                sSQL = "Update QC_Results set Result = '" & Trim(drmac("Result")) & "', Flag = '" &
                                                Flag & "', Released = 1, Released_By = " & ThisUser.ID & ", Release_Time = '" &
                                                Date.Now & "' where Run_ID = " & ItemR.ItemData & " and Control_ID = " & ControlID &
                                                " and " & "Test_ID = " & Val(PTestID(n))
                                            End If
                                            ExecuteSqlProcedure(sSQL)
                                        Next
                                    End If
                                End While
                            End If
                            cnmac.Close()
                            cnmac = Nothing
                            UpdateRunValidation(ItemR.ItemData, ControlID)
                            'Validate Control
                        End If
                    Next
                    'Validate Run
                End If
                If chkClear.Checked = True Then
                    If Samples.EndsWith(", ") Then Samples = Samples.Substring(0, Len(Samples) - 2)
                    If Samples <> "" Then
                        ExecuteSqlProcedure("Delete from Mac_Results where Equipment_ID = " &
                        ItemX.ItemData & " and Accession_ID in (" & Samples & ")")
                        Samples = ""
                    End If
                End If
                dgvControls.Rows.Clear()
                cmbBatches.Items.Clear()
                cmbBatches.Text = ""
                txtBatchDate.Text = Format(Date.Today, SystemConfig.DateFormat)
                cmbEquips.SelectedIndex = -1
                txtRunDate.Text = ""
            Else    'Accession
                Dim SAMPACCS(1, 0) As String
                For i = 0 To dgvAccs.RowCount - 1
                    Samples += "'" & dgvAccs.Rows(i).Cells(0).Value & "'" & ", "
                    If (dgvAccs.Rows(i).Cells(0).Value IsNot Nothing AndAlso
                    dgvAccs.Rows(i).Cells(0).Value.ToString <> "") AndAlso
                    (dgvAccs.Rows(i).Cells(2).Value IsNot Nothing AndAlso
                    Trim(dgvAccs.Rows(i).Cells(2).Value.ToString) <> "") Then    'accession assigned
                        If SAMPACCS(0, UBound(SAMPACCS, 2)) <> "" Then _
                        ReDim Preserve SAMPACCS(1, UBound(SAMPACCS, 2) + 1)
                        SAMPACCS(0, UBound(SAMPACCS, 2)) = dgvAccs.Rows(i).Cells(0).Value    'Sample
                        SAMPACCS(1, UBound(SAMPACCS, 2)) = dgvAccs.Rows(i).Cells(2).Value    'Accession
                    End If
                Next
                If Samples.EndsWith(", ") Then Samples = Microsoft.VisualBasic.Mid(Samples, 1, Len(Samples) - 2)
                If SAMPACCS(0, 0) <> "" Then
                    DisableActions()
                    BW.RunWorkerAsync(SAMPACCS)
                Else
                    If chkClear.Checked Then
                        If Samples.Length > 0 Then _
                        ExecuteSqlProcedure("Delete from Mac_Results where Equipment_ID = " &
                        ItemX.ItemData & " and Accession_ID in (" & Samples & ")")
                        Samples = ""
                    End If
                    If Not Me.InvokeRequired Then
                        btnAccQC.Checked = False
                        PopulateEquipments()
                        txtRunDate.Text = ""
                        cmbBatches.Items.Clear()
                        dgvAccs.Rows.Clear()
                        txtBatchDate.Text = Format(Date.Today, SystemConfig.DateFormat)
                        txtAccQC.Text = ""
                        txtValid.Text = ""
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub DisableActions()
        btnAccQC.Enabled = False
        btnProcess.Enabled = False
        cmbEquips.Enabled = False
        btnLoad.Enabled = False
        dgvAccs.Enabled = False
        chkRelVal.Enabled = False
        chkOverwrite.Enabled = False
        chkClear.Enabled = False
        btnClearTarget.Enabled = False
    End Sub

    Private Sub EnableActions()
        btnAccQC.Enabled = True
        btnProcess.Enabled = True
        cmbEquips.Enabled = True
        btnLoad.Enabled = True
        dgvAccs.Enabled = True
        chkRelVal.Enabled = True
        chkOverwrite.Enabled = True
        chkClear.Enabled = True
        btnClearTarget.Enabled = True
    End Sub

    Private Function UpdateAccResults(ByVal AccID As Long, ByVal TestID As Integer, ByVal Result As String, ByVal TResult _
    As String, ByVal Flag() As String, ByVal Status As String, ByVal OW As Boolean, ByVal Rel As Boolean) As String()
        Dim Conds() As String = {"", ""}
        If AccID > 0 And TestID > 0 And Result <> "" Then
            Dim sSQL As String = ""
            Dim RPTStatus As String = GetReportStatus(AccID)
            sSQL = "Select * from Acc_Results where Accession_ID = " & AccID & " and Test_ID = " & TestID
            '
            Dim cnUAR As New SqlClient.SqlConnection(connString)
            cnUAR.Open()
            Dim cmdUAR As New SqlClient.SqlCommand(sSQL, cnUAR)
            cmdUAR.CommandType = CommandType.Text
            Dim drUAR As SqlClient.SqlDataReader = cmdUAR.ExecuteReader
            If drUAR.HasRows Then
                While drUAR.Read
                    Dim ddd = drUAR("Result")
                    If InStr(RPTStatus, "FINAL") > 0 Or RPTStatus = "PARTIAL" Then

                        If (drUAR("Result") IsNot DBNull.Value AndAlso Trim(drUAR("Result")) <> "" _
                        AndAlso Trim(drUAR("Result")) <> Trim(Result)) AndAlso drUAR("Released") = True Then
                            UpdateResultHistory(AccID, TestID, Trim(drUAR("Result")), Date.Now, ThisUser.ID)
                        End If

                    End If
                    '*** Audit Trail ***
                    If SystemConfig.AuditTrail = True Then
                        If drUAR("Result") IsNot DBNull.Value _
                        AndAlso Trim(drUAR("Result")) <> "" Then
                            Conds(0) = TestID.ToString & "=" & Trim(drUAR("Result"))
                        Else
                            Conds(0) = TestID.ToString & "=" & String.Empty
                        End If
                        '
                        If drUAR("Released") IsNot DBNull.Value Then
                            Conds(0) += "," & Convert.ToInt16(drUAR("Released")) & "|"
                        Else
                            Conds(0) += ",0|"
                        End If
                        '*** End Audit Trail ****
                    End If
                    '
                    Result = Replace(Result, "[", "")
                    Result = Replace(Result, "]", "")
                    'If Trim(Flag(0)) = "" Then _
                    If TestID = 131 Then
                        Dim dddd = ""
                    End If
                    'Temur
                    Dim results1 = Result
                    If SystemConfig.ReportableFormat Then
                        If ReportableFormatRequired(TestID) Then
                            Result = FormatToReportable(Result, TestID)
                        End If

                    End If
                    If Not results1 = Result Then
                        Dim dddd = ""
                    End If
                    Flag = GetFlag(AccID, Result, TestID)
                    '
                    If TResult <> "" Then
                        TResult = Replace(TResult, Chr(34), "")
                        Dim RTB As New RichTextBox
                        RTB.Text = TResult
                        TResult = RTB.Rtf

                    End If
                    '
                    If ReportableFormatRequired(TestID) AndAlso
                    InStr(Result, "<") = 0 AndAlso InStr(Result, ">") = 0 _
                    Then Result = FormatToReportable(Result, TestID)
                    '
                    If OW = False And Rel = False Then
                        sSQL = "Update Acc_Results Set Result = '" & Result & "', T_Result = '" & Replace(TResult, "'", "''") & "', Flag = '" &
                        Trim(Flag(0)) & "', Behavior = '" & Flag(1) & "', Released = 0, Released_By = NULL, Release_Time = NULL " &
                        "where Accession_ID = " & AccID & " and Test_ID = " & TestID & " and (Result is Null or Result = '')"
                    ElseIf OW = False And Rel = True Then
                        If Flag(1) <> "Panic" Then
                            sSQL = "Update Acc_Results Set Result = '" & Result & "', T_Result = '" & Replace(TResult, "'", "''") & "', Flag = '" &
                            Trim(Flag(0)) & "', Behavior = '" & Flag(1) & "', Released = " & Convert.ToInt16(Rel) & ", Released_By = " &
                            ThisUser.ID & ", Release_Time = '" & Date.Now & "' where Accession_ID = " & AccID & " and Test_ID = " &
                            TestID & " and (Result is Null or Result = '')"
                        Else
                            sSQL = "Update Acc_Results Set Result = '" & Result & "', T_Result = '" & Replace(TResult, "'", "''") & "', Flag = '" &
                            Trim(Flag(0)) & "', Behavior = '" & Flag(1) & "', Released = 0, Released_By = NULL, Release_Time = NULL " &
                            "where Accession_ID = " & AccID & " and Test_ID = " & TestID & " and (Result is Null or Result = '')"
                        End If
                    ElseIf OW = True And Rel = True Then
                        If Flag(1) <> "Panic" Then
                            sSQL = "Update Acc_Results Set Result = '" & Result & "', T_Result = '" & Replace(TResult, "'", "''") & "', Flag = '" &
                            Trim(Flag(0)) & "', Behavior = '" & Flag(1) & "', Released = " & Convert.ToInt16(Rel) & ", Released_By = " &
                            ThisUser.ID & ", " & "Release_Time = '" & Date.Now & "' where Accession_ID = " & AccID & " and Test_ID = " & TestID
                        Else
                            sSQL = "Update Acc_Results Set Result = '" & Result & "', T_Result = '" & Replace(TResult, "'", "''") & "', Flag = '" &
                            Trim(Flag(0)) & "', Behavior = '" & Flag(1) & "', Released = 0, Released_By = NULL, Release_Time = NULL " &
                            "where Accession_ID = " & AccID & " and Test_ID = " & TestID
                        End If
                    ElseIf OW = True And Rel = False Then
                        sSQL = "Update Acc_Results Set Result = '" & Result & "', T_Result = '" & Replace(TResult, "'", "''") & "', Flag = '" &
                        Trim(Flag(0)) & "', Behavior = '" & Flag(1) & "', Released = 0, Released_By = NULL, Release_Time = NULL " &
                        "where Accession_ID = " & AccID & " and Test_ID = " & TestID
                    End If
                    '
                    Try
                        ExecuteSqlProcedure(sSQL)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                    '
                    '*** Audit Trail ***
                    If SystemConfig.AuditTrail = True Then
                        Conds(1) = TestID.ToString & "=" & Result & "," & Convert.ToInt16(Rel).ToString & "|"
                    End If
                    '*** End Audit Trail ****
                    If ResultTriggering(AccID, TestID, Result) Then _
                    PerformReflexing(EquipID, AccID, TestID, Result)
                End While
            End If
            cnUAR.Close()
            cnUAR = Nothing
        End If
        Return Conds
    End Function

    Private Function GetTriggerOrdinal(ByVal AccID As Long, ByVal TrigID As Integer) As Integer
        Dim TRID As Integer = 0
        Dim sSQL As String = "Select Max(Ordinal) as MaxOrd from Ref_Results " &
        "where Accession_ID = " & AccID & " and Reflexer_ID = " & TrigID
        If connString <> "" Then
            Dim cnTO1 As New SqlClient.SqlConnection(connString)
            cnTO1.Open()
            Dim cmdTO1 As New SqlClient.SqlCommand(sSQL, cnTO1)
            cmdTO1.CommandType = CommandType.Text
            Dim drTO1 As SqlClient.SqlDataReader = cmdTO1.ExecuteReader
            If drTO1.HasRows Then
                While drTO1.Read
                    If drTO1("MaxOrd") IsNot DBNull.Value Then
                        TRID = drTO1("MaxOrd")
                    End If
                End While
            End If
            cnTO1.Close()
            cnTO1 = Nothing
            '
            If TRID = 0 Then
                sSQL = "Select Ordinal as MaxOrd from Acc_Results where Accession_ID = " & AccID & " and Test_ID = " & TrigID
                Dim cnTO2 As New SqlClient.SqlConnection(connString)
                cnTO2.Open()
                Dim cmdTO2 As New SqlClient.SqlCommand(sSQL, cnTO2)
                cmdTO2.CommandType = CommandType.Text
                Dim drTO2 As SqlClient.SqlDataReader = cmdTO2.ExecuteReader
                If drTO2.HasRows Then
                    While drTO2.Read
                        If drTO2("MaxOrd") IsNot DBNull.Value Then
                            TRID = drTO2("MaxOrd")
                        End If
                    End While
                End If
                cnTO2.Close()
                cnTO2 = Nothing
            End If
            'Else
            '    Dim cnTO1 As New Odbc.OdbcConnection(connstring)
            '    cnTO1.Open()
            '    Dim cmdTO1 As New Odbc.OdbcCommand(sSQL, cnTO1)
            '    cmdTO1.CommandType = CommandType.Text
            '    Dim drTO1 As Odbc.OdbcDataReader = cmdTO1.ExecuteReader
            '    If drTO1.HasRows Then
            '        While drTO1.Read
            '            If drTO1("MaxOrd") IsNot DBNull.Value Then
            '                TRID = drTO1("MaxOrd")
            '            End If
            '        End While
            '    End If
            '    cnTO1.Close()
            '    cnTO1 = Nothing
            '    '
            '    If TRID = 0 Then
            '        sSQL = "Select Ordinal as MaxOrd from Acc_Results where Accession_ID = " & AccID & " and Test_ID = " & TrigID
            '        Dim cnTO2 As New Odbc.OdbcConnection(connstring)
            '        cnTO2.Open()
            '        Dim cmdTO2 As New Odbc.OdbcCommand(sSQL, cnTO2)
            '        cmdTO2.CommandType = CommandType.Text
            '        Dim drTO2 As Odbc.OdbcDataReader = cmdTO2.ExecuteReader
            '        If drTO2.HasRows Then
            '            While drTO2.Read
            '                If drTO2("MaxOrd") IsNot DBNull.Value Then
            '                    TRID = drTO2("MaxOrd")
            '                End If
            '            End While
            '        End If
            '        cnTO2.Close()
            '        cnTO2 = Nothing
            '    End If
        End If
        Return TRID
    End Function

    Private Sub ProcessTriggers(ByVal AccID As Long, ByVal ReflexerID As Integer, ByVal ReflexedIDs() As String)
        Dim i As Integer
        Dim TGPID As Integer
        Dim TGPType As String = ""
        Dim Ordinal As Integer = GetTriggerOrdinal(AccID, ReflexerID)
        '
        If connString <> "" Then
            For i = 0 To ReflexedIDs.Length - 1
                If ReflexedIDs(i) <> "" AndAlso Reflexable(AccID, ReflexedIDs(i), ReflexerID) Then
                    TGPID = Val(ReflexedIDs(i))
                    TGPType = GetTGPType(TGPID)
                    If TGPType = "T" Then
                        Dim cnn As New Data.SqlClient.SqlConnection(connString)
                        cnn.Open()
                        Dim cmdUpsert As New Data.SqlClient.SqlCommand("Ref_Results_SP", cnn)
                        cmdUpsert.CommandType = Data.CommandType.StoredProcedure
                        cmdUpsert.Parameters.AddWithValue("@act", "upsert")
                        cmdUpsert.Parameters.AddWithValue("@Accession_ID", AccID)
                        cmdUpsert.Parameters.AddWithValue("@Reflexer_ID", ReflexerID)
                        cmdUpsert.Parameters.AddWithValue("@Reflexed_ID", TGPID)
                        cmdUpsert.Parameters.AddWithValue("@Test_ID", TGPID)
                        cmdUpsert.Parameters.AddWithValue("@Ordinal", Ordinal)
                        'cmdUpsert.Parameters.AddWithValue("@Result", "")
                        'cmdUpsert.Parameters.AddWithValue("@Flag", "")
                        cmdUpsert.Parameters.AddWithValue("@NormalRange", GetNormalRange(AccID, TGPID))
                        cmdUpsert.Parameters.AddWithValue("@Released", 0)
                        cmdUpsert.ExecuteNonQuery()
                        cnn.Close()
                        cnn = Nothing
                    ElseIf TGPType = "G" Then
                        Dim TestIDs() As String = GetTestIDsfromTGPS(TGPID)
                        For n As Integer = 0 To TestIDs.Length - 1
                            Dim cnn As New Data.SqlClient.SqlConnection(connString)
                            cnn.Open()
                            Dim cmdUpsert As New Data.SqlClient.SqlCommand("Ref_Results_SP", cnn)
                            cmdUpsert.CommandType = Data.CommandType.StoredProcedure
                            cmdUpsert.Parameters.AddWithValue("@act", "upsert")
                            cmdUpsert.Parameters.AddWithValue("@Accession_ID", AccID)
                            cmdUpsert.Parameters.AddWithValue("@Reflexer_ID", ReflexerID)
                            cmdUpsert.Parameters.AddWithValue("@Reflexed_ID", TGPID)
                            cmdUpsert.Parameters.AddWithValue("@Test_ID", TestIDs(n))
                            cmdUpsert.Parameters.AddWithValue("@Ordinal", Ordinal)
                            'cmdUpsert.Parameters.AddWithValue("@Result", "")
                            'cmdUpsert.Parameters.AddWithValue("@Flag", "")
                            cmdUpsert.Parameters.AddWithValue("@NormalRange", GetNormalRange(AccID, TestIDs(n)))
                            cmdUpsert.Parameters.AddWithValue("@Released", 0)
                            cmdUpsert.ExecuteNonQuery()
                            cnn.Close()
                            cnn = Nothing
                        Next
                    End If
                End If
            Next
            'Else
            '    For i = 0 To ReflexedIDs.Length - 1
            '        If ReflexedIDs(i) <> "" Then
            '            TGPID = Val(ReflexedIDs(i))
            '            TGPType = GetTGPType(TGPID)
            '            If TGPType = "T" Then
            '                Dim cnn As New Data.Odbc.OdbcConnection(connstring)
            '                cnn.Open()
            '                Dim cmdUpsert As New Data.Odbc.OdbcCommand("Ref_Results_SP", cnn)
            '                cmdUpsert.CommandType = Data.CommandType.StoredProcedure
            '                cmdUpsert.Parameters.AddWithValue("@act", "upsert")
            '                cmdUpsert.Parameters.AddWithValue("@AccID", AccID)
            '                cmdUpsert.Parameters.AddWithValue("@ReflexerID", ReflexerID)
            '                cmdUpsert.Parameters.AddWithValue("@ReflexedID", TGPID)
            '                cmdUpsert.Parameters.AddWithValue("@TestID", TGPID)
            '                cmdUpsert.Parameters.AddWithValue("@Ordinal", Ordinal)
            '                'cmdUpsert.Parameters.AddWithValue("@Result", "")
            '                'cmdUpsert.Parameters.AddWithValue("@Flag", "")
            '                cmdUpsert.Parameters.AddWithValue("@NormalRange", GetNormalRange(AccID, TGPID))
            '                cmdUpsert.Parameters.AddWithValue("@Released", 0)
            '                cmdUpsert.ExecuteNonQuery()
            '                cnn.Close()
            '                cnn = Nothing
            '            ElseIf TGPType = "G" Then
            '                Dim TestIDs() As String = GetTestIDsfromTGPS(TGPID)
            '                For n As Integer = 0 To TestIDs.Length - 1
            '                    Dim cnn As New Data.Odbc.OdbcConnection(connstring)
            '                    cnn.Open()
            '                    Dim cmdUpsert As New Data.Odbc.OdbcCommand("Ref_Results_SP", cnn)
            '                    cmdUpsert.CommandType = Data.CommandType.StoredProcedure
            '                    cmdUpsert.Parameters.AddWithValue("@act", "upsert")
            '                    cmdUpsert.Parameters.AddWithValue("@AccID", AccID)
            '                    cmdUpsert.Parameters.AddWithValue("@ReflexerID", ReflexerID)
            '                    cmdUpsert.Parameters.AddWithValue("@ReflexedID", TGPID)
            '                    cmdUpsert.Parameters.AddWithValue("@TestID", TestIDs(n))
            '                    cmdUpsert.Parameters.AddWithValue("@Ordinal", Ordinal)
            '                    'cmdUpsert.Parameters.AddWithValue("@Result", "")
            '                    'cmdUpsert.Parameters.AddWithValue("@Flag", "")
            '                    cmdUpsert.Parameters.AddWithValue("@NormalRange", GetNormalRange(AccID, TestIDs(n)))
            '                    cmdUpsert.Parameters.AddWithValue("@Released", 0)
            '                    cmdUpsert.ExecuteNonQuery()
            '                    cnn.Close()
            '                    cnn = Nothing
            '                Next
            '            End If
            '        End If
            '    Next
        End If
    End Sub

    Private Function GetTestIDsfromTGPS(ByVal TGPID As String) As String()
        Dim TestIDs() As String = {""}
        If TGPID <> "" Then
            Dim TGPType As String = GetTGPType(Val(TGPID))
            Dim sSQL As String = ""
            If connString <> "" Then
                If TGPType = "P" Then   'P
                    sSQL = "Select c.Test_ID as TestID from (Group_Test c inner join Tests d on c.Test_ID = d.ID) " &
                    "inner join Prof_GrpTst e on e.GrpTst_ID = c.Group_ID where d.IsActive <> 0 and d.HasResult <> 0 " &
                    "and e.GrpTst_ID in (Select ID from Groups where IsActive <> 0) and e.Profile_ID = " & Val(TGPID) &
                    " order by e.Ordinal, c.Ordinal"
                    Dim cnpgt As New Data.SqlClient.SqlConnection(connString)
                    cnpgt.Open()
                    Dim pgtcmd As New Data.SqlClient.SqlCommand(sSQL, cnpgt)
                    pgtcmd.CommandType = Data.CommandType.Text
                    Dim pgtDR As Data.SqlClient.SqlDataReader = pgtcmd.ExecuteReader
                    If pgtDR.HasRows Then
                        While pgtDR.Read
                            TestIDs = AddTESTinTESTS(pgtDR("TestID"), TestIDs)
                        End While
                    End If
                    pgtDR.Close()
                    pgtcmd.Dispose()
                    cnpgt.Close()
                    'Tests in Profile
                    sSQL = "Select a.GrpTst_ID as TestID from Prof_GrpTst a inner join Tests b on b.ID = a.GrpTst_ID where " &
                    "b.IsActive <> 0 and b.HasResult <> 0 and a.Profile_ID = " & Val(TGPID) & " order by a.Ordinal"
                    Dim cnpt As New Data.SqlClient.SqlConnection(connString)
                    cnpt.Open()
                    Dim ptcmd As New Data.SqlClient.SqlCommand(sSQL, cnpt)
                    ptcmd.CommandType = Data.CommandType.Text
                    Dim ptDR As Data.SqlClient.SqlDataReader = ptcmd.ExecuteReader
                    If ptDR.HasRows Then
                        While ptDR.Read
                            TestIDs = AddTESTinTESTS(ptDR("TestID"), TestIDs)
                        End While
                    End If
                    ptDR.Close()
                    ptcmd.Dispose()
                    cnpt.Close()
                ElseIf TGPType = "G" Then   'G
                    sSQL = "Select a.Test_ID as TestID from Group_Test a inner join Tests b on b.ID = a.Test_ID " &
                    "where b.IsActive <> 0 and b.HasResult <> 0 and a.Group_ID = " & Val(TGPID) & " order by a.ordinal"
                    Dim cngt As New Data.SqlClient.SqlConnection(connString)
                    cngt.Open()
                    Dim gtcmd As New Data.SqlClient.SqlCommand(sSQL, cngt)
                    gtcmd.CommandType = Data.CommandType.Text
                    Dim gtDR As Data.SqlClient.SqlDataReader = gtcmd.ExecuteReader
                    If gtDR.HasRows Then
                        While gtDR.Read
                            TestIDs = AddTESTinTESTS(gtDR("TestID"), TestIDs)
                        End While
                    End If
                    gtDR.Close()
                    gtcmd.Dispose()
                    cngt.Close()
                    cngt = Nothing
                End If
                'Else
                'If TGPType = "P" Then   'P
                '    sSQL = "Select c.Test_ID as TestID from (Group_Test c inner join Tests d on c.Test_ID = d.ID) " &
                '    "inner join Prof_GrpTst e on e.GrpTst_ID = c.Group_ID where d.IsActive <> 0 and d.HasResult <> 0 " &
                '    "and e.GrpTst_ID in (Select ID from Groups where IsActive <> 0) and e.Profile_ID = " & Val(TGPID) &
                '    " order by e.Ordinal, c.Ordinal"
                '    Dim cnpgt As New Data.Odbc.OdbcConnection(connstring)
                '    cnpgt.Open()
                '    Dim pgtcmd As New Data.Odbc.OdbcCommand(sSQL, cnpgt)
                '    pgtcmd.CommandType = Data.CommandType.Text
                '    Dim pgtDR As Data.Odbc.OdbcDataReader = pgtcmd.ExecuteReader
                '    If pgtDR.HasRows Then
                '        While pgtDR.Read
                '            TestIDs = AddTESTinTESTS(pgtDR("TestID"), TestIDs)
                '        End While
                '    End If
                '    pgtDR.Close()
                '    pgtcmd.Dispose()
                '    cnpgt.Close()
                '    'Tests in Profile
                '    sSQL = "Select a.GrpTst_ID as TestID from Prof_GrpTst a inner join Tests b on b.ID = a.GrpTst_ID where " &
                '    "b.IsActive <> 0 and b.HasResult <> 0 and a.Profile_ID = " & Val(TGPID) & " order by a.Ordinal"
                '    Dim cnpt As New Data.Odbc.OdbcConnection(connstring)
                '    cnpt.Open()
                '    Dim ptcmd As New Data.Odbc.OdbcCommand(sSQL, cnpt)
                '    ptcmd.CommandType = Data.CommandType.Text
                '    Dim ptDR As Data.Odbc.OdbcDataReader = ptcmd.ExecuteReader
                '    If ptDR.HasRows Then
                '        While ptDR.Read
                '            TestIDs = AddTESTinTESTS(ptDR("TestID"), TestIDs)
                '        End While
                '    End If
                '    ptDR.Close()
                '    ptcmd.Dispose()
                '    cnpt.Close()
                'ElseIf TGPType = "G" Then   'G
                '    sSQL = "Select a.Test_ID as TestID from Group_Test a inner join Tests b on b.ID = a.Test_ID " &
                '    "where b.IsActive <> 0 and b.HasResult <> 0 and a.Group_ID = " & Val(TGPID) & " order by a.ordinal"
                '    Dim cngt As New Data.Odbc.OdbcConnection(connstring)
                '    cngt.Open()
                '    Dim gtcmd As New Data.Odbc.OdbcCommand(sSQL, cngt)
                '    gtcmd.CommandType = Data.CommandType.Text
                '    Dim gtDR As Data.Odbc.OdbcDataReader = gtcmd.ExecuteReader
                '    If gtDR.HasRows Then
                '        While gtDR.Read
                '            TestIDs = AddTESTinTESTS(gtDR("TestID"), TestIDs)
                '        End While
                '    End If
                '    gtDR.Close()
                '    gtcmd.Dispose()
                '    cngt.Close()
                '    cngt = Nothing
                'End If
            End If
        End If
        Return TestIDs
    End Function

    Private Sub PerformReflexing(ByVal EquipID As Integer, ByVal AccID As Long, ByVal TestID As Integer, ByVal Result As String)
        '********* Following is Automarking code ****************
        Dim RefedIDs() As String = GetConfigReflexedIDs(TestID, Trim(Result))
        ProcessTriggers(AccID, TestID, RefedIDs)
        ' ****** End of automarking code **************************
    End Sub

    Private Function UpdateRefResults(ByVal AccID As Long, ByVal ReflexerID As Integer, ByVal ReflexedID As Integer,
    ByVal TestID As Integer, ByVal Result As String, ByVal TResult As String, ByVal Flag() As String, ByVal Status _
    As String, ByVal OW As Boolean, ByVal Rel As Boolean) As String()
        Dim Conds() As String = {"", ""}
        Result = Replace(Result, "[", "")
        Result = Replace(Result, "]", "")
        '
        If TResult <> "" Then
            TResult = Replace(TResult, Chr(34), "")
            Dim RTB As New RichTextBox
            RTB.Text = TResult
            TResult = RTB.Rtf
        End If
        '
        If Trim(Flag(0)) = "" Then _
        Flag = GetFlag(AccID, Result, TestID)
        '
        If ReportableFormatRequired(TestID) Then _
        Result = FormatToReportable(Result, TestID)
        If AccID > 0 And ReflexerID > 0 And TestID > 0 And Result <> "" Then
            Dim sSQL As String = "Select * from Ref_Results where Accession_ID = " &
            AccID & " and Reflexer_ID = " & ReflexerID & " And Test_ID = " & TestID
            '
            Dim cnn As New Data.SqlClient.SqlConnection(connString)
            cnn.Open()
            Dim cmdsel As New Data.SqlClient.SqlCommand(sSQL, cnn)
            cmdsel.CommandType = Data.CommandType.Text
            Dim drsel As Data.SqlClient.SqlDataReader = cmdsel.ExecuteReader
            If drsel.HasRows Then
                While drsel.Read
                    '*** Audit Trail ***
                    If SystemConfig.AuditTrail = True Then
                        If drsel("Result") IsNot DBNull.Value _
                        AndAlso Trim(drsel("Result")) <> "" Then
                            Conds(0) = TestID.ToString & "=" & Trim(drsel("Result"))
                        Else
                            Conds(0) = TestID.ToString & "="
                        End If
                        '
                        If drsel("Released") IsNot DBNull.Value Then
                            Conds(0) += "," & Convert.ToInt16(drsel("Released")) & "|"
                        Else
                            Conds(0) += ",0|"
                        End If
                    End If
                    '*** End Audit Trail ****
                End While
            End If
            cnn.Close()
            cnn = Nothing
            '
            If Conds(0) <> "" Then
                If OW = False And Rel = False Then
                    sSQL = "Update Ref_Results Set Result = '" & Result & "', T_Result = '" & Replace(TResult, "'", "''") & "', Flag = '" &
                    Flag(0) & "', Behavior = '" & Flag(1) & "', Released = 0, Released_By = NULL, Release_Time = NULL where Accession_ID = " &
                    AccID & " and Reflexer_ID = " & ReflexerID & " and Test_ID = " & TestID & " and (Result is Null or Result = '')"
                ElseIf OW = False And Rel = True Then
                    If Flag(1) <> "Panic" Then
                        sSQL = "Update Ref_Results Set Result = '" & Result & "', T_Result = '" & Replace(TResult, "'", "''") & "', Flag = '" &
                        Flag(0) & "', Behavior = '" & Flag(1) & "', Released = " & Convert.ToInt16(Rel) & ", Released_By = " &
                        ThisUser.ID & ", " & "Release_Time = '" & Date.Now & "' where Accession_ID = " & AccID & " and " &
                        "Reflexer_ID = " & ReflexerID & " and Test_ID = " & TestID & " and (Result is Null or Result = '')"
                    Else
                        sSQL = "Update Ref_Results Set Result = '" & Result & "', T_Result = '" & Replace(TResult, "'", "''") & "', Flag = '" &
                        Flag(0) & "', Behavior = '" & Flag(1) & "', Released = 0, Released_By = NULL, Release_Time = NULL where Accession_ID = " &
                        AccID & " and Reflexer_ID = " & ReflexerID & " and Test_ID = " & TestID & " and (Result is Null or Result = '')"
                    End If
                ElseIf OW = True And Rel = True Then
                    If Flag(1) <> "Panic" Then
                        sSQL = "Update Ref_Results Set Result = '" & Result & "', T_Result = '" & Replace(TResult, "'", "''") & "', Flag = '" &
                        Flag(0) & "', Behavior = '" & Flag(1) & "', Released = " & Convert.ToInt16(Rel) & ", Released_By = " & ThisUser.ID &
                        ", Release_Time = '" & Date.Now & "' where Accession_ID = " & AccID & " and Reflexer_ID = " & ReflexerID &
                        " and Test_ID = " & TestID
                    Else
                        sSQL = "Update Ref_Results Set Result = '" & Result & "', T_Result = '" & Replace(TResult, "'", "''") & "', Flag = '" &
                        Flag(0) & "', Behavior = '" & Flag(1) & "', Released = 0, Released_By = NULL, Release_Time = NULL where Accession_ID = " &
                        AccID & " and Reflexer_ID = " & ReflexerID & " and Test_ID = " & TestID
                    End If
                ElseIf OW = True And Rel = False Then
                    sSQL = "Update Ref_Results Set Result = '" & Result & "', T_Result = '" & Replace(TResult, "'", "''") & "', Flag = '" &
                    Flag(0) & "', Behavior = '" & Flag(1) & "', Released = 0, Released_By = NULL, Release_Time = NULL " &
                    "where Accession_ID = " & AccID & " and Reflexer_ID = " & ReflexerID & " and Test_ID = " & TestID
                End If
                '
                Try
                    ExecuteSqlProcedure(sSQL)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            Else    'Not applies - change Reflexed to Reflexer
                sSQL = "Select * from Ref_Results where Accession_ID = " & AccID &
                " and Reflexer_ID = " & ReflexedID & " And Test_ID = " & TestID
                '
                Dim cnd As New Data.SqlClient.SqlConnection(connString)
                cnd.Open()
                Dim cmdd As New Data.SqlClient.SqlCommand(sSQL, cnd)
                cmdd.CommandType = Data.CommandType.Text
                Dim drd As Data.SqlClient.SqlDataReader = cmdd.ExecuteReader
                If drd.HasRows Then
                    While drd.Read
                        '*** Audit Trail ***
                        If SystemConfig.AuditTrail = True Then
                            If drd("Result") IsNot DBNull.Value _
                            AndAlso Trim(drd("Result")) <> "" Then
                                Conds(0) = TestID.ToString & "=" & Trim(drd("Result"))
                            Else
                                Conds(0) = TestID.ToString & "="
                            End If
                            '
                            If drd("Released") IsNot DBNull.Value Then
                                Conds(0) += "," & Convert.ToInt16(drd("Released")) & "|"
                            Else
                                Conds(0) += ",0|"
                            End If
                        End If
                        '*** End Audit Trail ****
                    End While
                End If
                cnd.Close()
                cnd = Nothing
            End If
            '
            If OW = False And Rel = False Then
                sSQL = "Update Ref_Results Set Result = '" & Result & "', T_Result = '" & Replace(TResult, "'", "''") & "', Flag = '" & Flag(0) &
                "', Behavior = '" & Flag(1) & "', Released = 0, Released_By = NULL, Release_Time = NULL where Accession_ID = " &
                AccID & " and Reflexer_ID = " & ReflexedID & " and Test_ID = " & TestID & " and (Result is Null or Result = '')"
            ElseIf OW = False And Rel = True Then
                If Flag(1) <> "Panic" Then
                    sSQL = "Update Ref_Results Set Result = '" & Result & "', T_Result = '" & Replace(TResult, "'", "''") & "', Flag = '" &
                    Flag(0) & "', Behavior = '" & Flag(1) & "', Released = " & Convert.ToInt16(Rel) & ", Released_By = " &
                    ThisUser.ID & ", " & "Release_Time = '" & Date.Now & "' where Accession_ID = " & AccID & " and " &
                    "Reflexer_ID = " & ReflexedID & " and Test_ID = " & TestID & " and (Result is Null or Result = '')"
                Else
                    sSQL = "Update Ref_Results Set Result = '" & Result & "', T_Result = '" & Replace(TResult, "'", "''") & "', Flag = '" &
                    Flag(0) & "', Behavior = '" & Flag(1) & "', Released = 0, Released_By = NULL, Release_Time = NULL where Accession_ID = " &
                    AccID & " and Reflexer_ID = " & ReflexedID & " and Test_ID = " & TestID & " and (Result is Null or Result = '')"
                End If
            ElseIf OW = True And Rel = True Then
                If Flag(1) <> "Panic" Then
                    sSQL = "Update Ref_Results Set Result = '" & Result & "', T_Result = '" & Replace(TResult, "'", "''") & "', Flag = '" &
                    Flag(0) & "', Behavior = '" & Flag(1) & "', Released = " & Convert.ToInt16(Rel) & ", Released_By = " &
                    ThisUser.ID & ", " & "Release_Time = '" & Date.Now & "' where Accession_ID = " & AccID & " and " &
                    "Reflexer_ID = " & ReflexedID & " and Test_ID = " & TestID
                Else
                    sSQL = "Update Ref_Results Set Result = '" & Result & "', T_Result = '" & Replace(TResult, "'", "''") & "', Flag = '" &
                    Flag(0) & "', Behavior = '" & Flag(1) & "', Released = 0, Released_By = NULL, Release_Time = NULL " &
                    "where Accession_ID = " & AccID & " and Reflexer_ID = " & ReflexedID & " and Test_ID = " & TestID
                End If
            ElseIf OW = True And Rel = False Then
                sSQL = "Update Ref_Results Set Result = '" & Result & "', T_Result = '" & Replace(TResult, "'", "''") & "', Flag = '" &
                Flag(0) & "', Behavior = '" & Flag(1) & "', Released = 0, Released_By = NULL, Release_Time = NULL " &
                "where Accession_ID = " & AccID & " and Reflexer_ID = " & ReflexedID & " and Test_ID = " & TestID
            End If
            '
            Try
                ExecuteSqlProcedure(sSQL)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
        '
        '*** Audit Trail ***
        If SystemConfig.AuditTrail = True Then
            Conds(1) = TestID.ToString & "=" & Result &
            "," & Convert.ToInt16(Rel).ToString & "|"
        End If
        '*** End Audit Trail ****
        If ResultTriggering(AccID, TestID, Result) _
        Then PerformReflexing(EquipID, AccID, TestID, Result)
        '
        Return Conds
    End Function

    Private Function UpdateInfoResults(ByVal AccID As Long, ByVal TestID As Integer, ByVal InfoID As Integer,
    ByVal Result As String, ByVal TResult As String, ByVal Flag() As String, ByVal Status As String,
    ByVal OW As Boolean, ByVal Rel As Boolean) As String()
        Dim Conds() As String = {"", ""}
        Dim sSql As String = ""
        If AccID > 0 And InfoID > 0 And TestID > 0 And Result <> "" Then
            Dim cnir As New Data.SqlClient.SqlConnection(connString)
            cnir.Open()
            Dim cmdir As New Data.SqlClient.SqlCommand("Select * from Acc_Info_Results where " &
            "Accession_ID = " & AccID & " and Info_ID = " & InfoID & " and Test_ID = " & TestID, cnir)
            cmdir.CommandType = Data.CommandType.Text
            Dim drir As Data.SqlClient.SqlDataReader = cmdir.ExecuteReader
            If drir.HasRows Then
                While drir.Read
                    '*** Audit Trail ***
                    If SystemConfig.AuditTrail = True Then
                        If drir("Result") IsNot DBNull.Value _
                        AndAlso Trim(drir("Result")) <> "" Then
                            Conds(0) = TestID.ToString & "=" & Trim(drir("Result"))
                        Else
                            Conds(0) = TestID.ToString & "="
                        End If
                        '
                        If drir("Released") IsNot DBNull.Value Then
                            Conds(0) += "," & Convert.ToInt16(drir("Released")) & "|"
                        Else
                            Conds(0) += ",0|"
                        End If
                        '*** End Audit Trail ****
                    End If
                    Result = Replace(Result, "[", "")
                    Result = Replace(Result, "]", "")
                    '
                    If TResult <> "" Then
                        TResult = Replace(TResult, Chr(34), "")
                        Dim RTB As New RichTextBox
                        RTB.Text = TResult
                        TResult = RTB.Rtf
                    End If
                    '
                    If Flag(0) = "" Then _
                    Flag = GetFlag(AccID, Result, InfoID)
                    '
                    If ReportableFormatRequired(TestID) Then _
                    Result = FormatToReportable(Result, TestID)
                    '
                End While
            End If
            cnir.Close()
            cnir = Nothing
            '
            If OW = False And Rel = False Then
                sSql = "Update Acc_Info_Results Set Result = '" & Result & "', T_Result = '" & Replace(TResult, "'", "''") & "', Flag = '" &
                Flag(0) & "', Behavior = '" & Flag(1) & "', Released = 0, Released_By = NULL, Release_Time = NULL where Accession_ID = " &
                AccID & " and Info_ID = " & InfoID & " and Test_ID = " & TestID & " and (Result is Null or Result = '')"
            ElseIf OW = False And Rel = True Then
                If Flag(1) <> "Panic" Then
                    sSql = "Update Acc_Info_Results Set Result = '" & Result & "', T_Result = '" & Replace(TResult, "'", "''") &
                    "', Flag = '" & Flag(0) & "', Behavior = '" & Flag(1) & "', Released = " & Convert.ToInt16(Rel) & ", Released_By = " &
                    ThisUser.ID & ", Release_Time = '" & Date.Now & "' where Accession_ID = " & AccID & " and Info_ID = " & InfoID & " and " &
                    "Test_ID = " & TestID & " and (Result is Null or Result = '')"
                Else
                    sSql = "Update Acc_Info_Results Set Result = '" & Result & "', T_Result = '" & Replace(TResult, "'", "''") & "', Flag = '" _
                    & Flag(0) & "', Behavior = '" & Flag(1) & "', Released = 0, Released_By = NULL, Release_Time = NULL where Accession_ID = " &
                    AccID & " and Info_ID = " & InfoID & " and Test_ID = " & TestID & " and (Result is Null or Result = '')"
                End If
            ElseIf OW = True And Rel = True Then
                If Flag(1) <> "Panic" Then
                    sSql = "Update Acc_Info_Results Set Result = '" & Result & "', T_Result = '" & Replace(TResult, "'", "''") & "', Flag = '" &
                    Flag(0) & "', Behavior = '" & Flag(1) & "', Released = " & Convert.ToInt16(Rel) & ", Released_By = " & ThisUser.ID & ", " &
                    "Release_Time = '" & Date.Now & "' where Accession_ID = " & AccID & " and Info_ID = " & InfoID & " and Test_ID = " & TestID
                Else
                    sSql = "Update Acc_Info_Results Set Result = '" & Result & "', T_Result = '" & Replace(TResult, "'", "''") &
                    "', Flag = '" & Flag(0) & "', Behavior = '" & Flag(1) & "', Released = 0, Released_By = NULL, Release_Time = NULL " &
                    "where Accession_ID = " & AccID & " and Info_ID = " & InfoID & " and Test_ID = " & TestID
                End If
            End If
            '
            ExecuteSqlProcedure(sSql)
            '
            '*** Audit Trail ***
            If SystemConfig.AuditTrail = True Then
                Conds(1) = TestID.ToString & "=" & Result &
                "," & Convert.ToInt16(Rel).ToString & "|"
            End If
            '*** End Audit Trail ****
            'PerformReflexing(EquipID, AccID, TestID, Result)
        End If
        '
        Return Conds
    End Function

    Private Function GetInfoTID(ByVal AccID As Long, ByVal InfoID As Integer) As String
        Dim TID As String = ""
        Dim cniid As New Data.SqlClient.SqlConnection(connString)
        cniid.Open()
        Dim cmdiid As New Data.SqlClient.SqlCommand("Select * from Acc_Info_Results " &
        "where Accession_ID = " & AccID & " and Info_ID = " & InfoID, cniid)
        cmdiid.CommandType = Data.CommandType.Text
        Dim driid As Data.SqlClient.SqlDataReader = cmdiid.ExecuteReader
        If driid.HasRows Then
            While driid.Read
                TID = driid("Test_ID").ToString
            End While
        End If
        cniid.Close()
        cniid = Nothing
        Return TID
    End Function

    Private Function GetRefRecords(ByVal AccID As String) As String()
        Dim Refs() As String = {""}
        Dim sSQL As String = "Select * from Ref_Results where Accession_ID = " & Val(AccID)
        '
        Dim cnn As New Data.SqlClient.SqlConnection(connString)
        cnn.Open()
        Dim cmdsel As New Data.SqlClient.SqlCommand(sSQL, cnn)
        cmdsel.CommandType = Data.CommandType.Text
        Dim DRsel As Data.SqlClient.SqlDataReader = cmdsel.ExecuteReader
        If DRsel.HasRows Then
            While DRsel.Read
                If Refs(UBound(Refs)) <> "" Then ReDim Preserve Refs(UBound(Refs) + 1)
                Refs(UBound(Refs)) = DRsel("Reflexer_ID").ToString & "|" &
                DRsel("Reflexed_ID").ToString & "|" & DRsel("Test_ID").ToString
            End While
        End If
        cnn.Close()
        cnn = Nothing
        Return Refs
    End Function

    Private Function GetMacResult(ByVal EquipID As Integer, ByVal AccID As String, ByVal _
    ReflexerID As Integer, ByVal TestID As Integer) As String()
        Dim ResFlag() As String = {"", "", ""}  '0=Res, 1=Flag, 2=Behavior
        Dim sSQL As String = "Select Result, Flag from Mac_Results where Equipment_ID = " & EquipID & " and Accession_ID = '" &
        AccID & "' and Reflexer_ID = " & ReflexerID & " and Test_ID in (Select a.eqResult_ID from Ana_Test a inner join Anas " &
        "b on b.ID = a.Ana_ID where b.Equipment_ID = " & EquipID & " and a.Test_ID = " & TestID & ")"
        'If connstring <> "" Then
        '    If connstring <> "" Then
        Dim cnn As New Data.SqlClient.SqlConnection(connString)
        cnn.Open()
        Dim cmdsel As New Data.SqlClient.SqlCommand(sSQL, cnn)
        cmdsel.CommandType = Data.CommandType.Text
        Dim DRsel As Data.SqlClient.SqlDataReader = cmdsel.ExecuteReader
        If DRsel.HasRows Then
            While DRsel.Read
                If DRsel("Result") IsNot DBNull.Value AndAlso
                Trim(DRsel("Result")) <> "" Then ResFlag(0) = Trim(DRsel("Result"))
                '
                If DRsel("Flag") IsNot DBNull.Value Then ResFlag(1) = Trim(DRsel("Flag"))
            End While
        End If
        cnn.Close()
        cnn = Nothing
        '
        If ResFlag(0) = "" Then
            sSQL = "Select Result, Flag from Mac_Results where Equipment_ID = " & EquipID & " and Accession_ID = '" &
            AccID & "' and Reflexer_ID = -1 and Test_ID in (Select a.eqResult_ID from Ana_Test a inner join Anas " &
            "b on b.ID = a.Ana_ID where b.Equipment_ID = " & EquipID & " and a.Test_ID = " & TestID & ")"
            Dim cn1 As New Data.SqlClient.SqlConnection(connString)
            cn1.Open()
            Dim cmd1 As New Data.SqlClient.SqlCommand(sSQL, cn1)
            cmd1.CommandType = Data.CommandType.Text
            Dim DR1 As Data.SqlClient.SqlDataReader = cmd1.ExecuteReader
            If DR1.HasRows Then
                While DR1.Read
                    If DR1("Result") IsNot DBNull.Value AndAlso
                    Trim(DR1("Result")) <> "" Then ResFlag(0) = Trim(DR1("Result"))
                    '
                    If DR1("Flag") IsNot DBNull.Value Then ResFlag(1) = Trim(DR1("Flag"))
                End While
            End If
            cn1.Close()
            cn1 = Nothing
        End If
        Return ResFlag
    End Function

    Private Function UpdateMacRefResults(ByVal EquipID As Integer, ByVal AccID As Long, ByVal ReflexerID As Integer, ByVal _
    ReflexedID As Integer, ByVal TestID As Integer, ByVal RPTStatus As String, ByVal OW As Boolean, ByVal Rel As Boolean) As String()
        Dim Conds() As String = {"", ""}
        If InStr(RPTStatus, "FINAL") = 0 Or OW = True Then
            Dim PrevRes As String = ""
            Dim sSQL As String = "Select * from Ref_Results where Accession_ID = " & AccID & " and " &
            "Reflexer_ID = " & ReflexerID & " and Reflexed_ID = " & ReflexedID & " and Test_ID = " & TestID
            If connString <> "" Then
                If connString <> "" Then
                    Dim cnn As New Data.SqlClient.SqlConnection(connString)
                    cnn.Open()
                    Dim cmdsel As New Data.SqlClient.SqlCommand(sSQL, cnn)
                    cmdsel.CommandType = Data.CommandType.Text
                    Dim DRsel As Data.SqlClient.SqlDataReader = cmdsel.ExecuteReader
                    If DRsel.HasRows Then
                        While DRsel.Read
                            '*** Audit Trail ***
                            If SystemConfig.AuditTrail = True Then
                                If DRsel("Result") IsNot DBNull.Value _
                                AndAlso Trim(DRsel("Result")) <> "" Then
                                    Conds(0) = TestID.ToString & "=" & Trim(DRsel("Result"))
                                    PrevRes = Trim(DRsel("Result"))
                                Else
                                    Conds(0) = TestID.ToString & "="
                                End If
                                '
                                If DRsel("Released") IsNot DBNull.Value Then
                                    Conds(0) += "," & Convert.ToInt16(DRsel("Released")) & "|"
                                Else
                                    Conds(0) += ",0|"
                                End If
                                '*** End Audit Trail ****
                            End If
                        End While
                    End If
                    cnn.Close()
                    cnn = Nothing
                    'Else
                    '    Dim cnn As New Data.Odbc.OdbcConnection(connstring)
                    '    cnn.Open()
                    '    Dim cmdsel As New Data.Odbc.OdbcCommand(sSQL, cnn)
                    '    cmdsel.CommandType = Data.CommandType.Text
                    '    Dim DRsel As Data.Odbc.OdbcDataReader = cmdsel.ExecuteReader
                    '    If DRsel.HasRows Then
                    '        While DRsel.Read
                    '            '*** Audit Trail ***
                    '            If SystemConfig.AuditTrail = True Then
                    '                If DRsel("Result") IsNot DBNull.Value _
                    '                AndAlso Trim(DRsel("Result")) <> "" Then
                    '                    Conds(0) = TestID.ToString & "=" & Trim(DRsel("Result"))
                    '                Else
                    '                    Conds(0) = TestID.ToString & "="
                    '                End If
                    '                '
                    '                If DRsel("Released") IsNot DBNull.Value Then
                    '                    Conds(0) += "," & Convert.ToInt16(DRsel("Released")) & "|"
                    '                Else
                    '                    Conds(0) += ",0|"
                    '                End If
                    '                '*** End Audit Trail ****
                    '            End If
                    '        End While
                    '    End If
                    '    cnn.Close()
                    '    cnn = Nothing
                End If
            End If
            '
            Dim ResFlag() As String = GetMacResult(EquipID, AccID.ToString, ReflexerID, TestID)
            If ResFlag(0) <> "" Then
                If Trim(ResFlag(1)) = "" Then
                    Dim FB() As String = GetFlag(AccID, ResFlag(0), TestID)
                    ResFlag(1) = FB(0)
                    ResFlag(2) = FB(1)
                End If
                '
                If ReportableFormatRequired(TestID) Then _
                ResFlag(0) = FormatToReportable(ResFlag(0), TestID)
                '
                Dim cnn As New Data.SqlClient.SqlConnection(connString)
                cnn.Open()
                Dim cmdUpsert As New Data.SqlClient.SqlCommand("Ref_Results_SP", cnn)
                cmdUpsert.CommandType = Data.CommandType.StoredProcedure
                cmdUpsert.Parameters.AddWithValue("@act", "upsert")
                cmdUpsert.Parameters.AddWithValue("@Accession_ID", AccID)
                cmdUpsert.Parameters.AddWithValue("@Reflexer_ID", ReflexerID)
                cmdUpsert.Parameters.AddWithValue("@Reflexed_ID", ReflexedID)
                cmdUpsert.Parameters.AddWithValue("@Test_ID", TestID)
                If OW = False Or PrevRes = "" Then
                    cmdUpsert.Parameters.AddWithValue("@Result", ResFlag(0))
                    cmdUpsert.Parameters.AddWithValue("@Flag", ResFlag(1))
                End If
                cmdUpsert.Parameters.AddWithValue("@Comment", ResFlag(2))
                If OW = False And Rel = False Then
                    cmdUpsert.Parameters.AddWithValue("@Released", 0)
                ElseIf OW = False And Rel = True Then
                    If Not (ResFlag(1) = "LP" Or ResFlag(1) = "HP" Or ResFlag(1) = "AP" Or InStr(ResFlag(1), "PANIC") > 0) Then
                        cmdUpsert.Parameters.AddWithValue("@Released", 1)
                        cmdUpsert.Parameters.AddWithValue("@Release_Time", Date.Now)
                        cmdUpsert.Parameters.AddWithValue("@Released_By", ThisUser.ID)
                    Else
                        cmdUpsert.Parameters.AddWithValue("@Released", 0)
                        cmdUpsert.Parameters.AddWithValue("@Release_Time", DBNull.Value)
                        cmdUpsert.Parameters.AddWithValue("@Released_By", DBNull.Value)
                    End If
                ElseIf OW = True And Rel = True Then
                    If Not (ResFlag(1) = "LP" Or ResFlag(1) = "HP" Or ResFlag(1) = "AP" Or InStr(ResFlag(1), "PANIC") > 0) Then
                        cmdUpsert.Parameters.AddWithValue("@Released", 1)
                        cmdUpsert.Parameters.AddWithValue("@Release_Time", Date.Now)
                        cmdUpsert.Parameters.AddWithValue("@Released_By", ThisUser.ID)
                    Else
                        cmdUpsert.Parameters.AddWithValue("@Released", 0)
                        cmdUpsert.Parameters.AddWithValue("@Release_Time", DBNull.Value)
                        cmdUpsert.Parameters.AddWithValue("@Released_By", DBNull.Value)
                    End If
                End If
                cmdUpsert.ExecuteNonQuery()
                cnn.Close()
                cnn = Nothing
                '*** Audit Trail ***
                If SystemConfig.AuditTrail = True Then
                    Conds(1) = TestID.ToString & "=" & ResFlag(0) &
                    "," & Convert.ToInt16(Rel).ToString & "|"
                End If
                '*** End Audit Trail ****
                If ResultTriggering(AccID, TestID, ResFlag(0)) _
                Then PerformReflexing(EquipID, AccID, TestID, ResFlag(0))
            End If
        End If
        '
        Return Conds
    End Function

    Private Function GetInfoMacResults(ByVal EquipID As Integer, ByVal SampleID As String, ByVal AccID As String) As Data.DataTable
        Dim TBL As New Data.DataTable
        Dim sSQL As String = "Select a.Equipment_ID, a.Accession_ID, a.Reflexer_ID, b.Test_ID , a.QP, a.Result, a.T_Result, " &
        "a.Flag, a.Run_Date, a.Opr from Mac_Results a inner join (Ana_Test b inner join Anas c on b.Ana_ID = c.ID and " &
        "c.Equipment_ID = " & EquipID & ") on b.eqResult_ID = a.Test_ID where c.Equipment_ID = " & EquipID & " and a.Accession_ID " &
        "= '" & SampleID & "' and b.Test_ID in (Select Info_ID from Acc_Info_Results where Accession_ID = " & Val(AccID) & ")"
        '
        Dim cnn As New Data.SqlClient.SqlConnection(connString)
        cnn.Open()
        Dim cmdMac As New Data.SqlClient.SqlCommand(sSQL, cnn)
        cmdMac.CommandType = Data.CommandType.Text
        Dim daMac As New Data.SqlClient.SqlDataAdapter(cmdMac)
        daMac.Fill(TBL)
        cnn.Close()
        cnn = Nothing
        Return TBL
    End Function

    Private Function GetRefMacResults(ByVal EquipID As Integer, ByVal SampleID As String, ByVal AccID As String) As Data.DataTable
        Dim TBL As New Data.DataTable
        Dim sSQL As String = "Select a.Equipment_ID, a.Accession_ID, a.Reflexer_ID, b.Test_ID , a.QP, a.Result, a.T_Result, " &
        "a.Flag, a.Run_Date, a.Opr from Mac_Results a inner join (Ana_Test b inner join Anas c on b.Ana_ID = c.ID and " &
        "c.Equipment_ID = " & EquipID & ") on b.eqResult_ID = a.Test_ID where c.Equipment_ID = " & EquipID & " and a.Accession_ID " &
        "= '" & SampleID & "' and b.Test_ID in (Select Test_ID from Ref_Results where Accession_ID = " & Val(AccID) & ")"
        '
        Dim cnn As New Data.SqlClient.SqlConnection(connString)
        cnn.Open()
        Dim cmdMac As New Data.SqlClient.SqlCommand(sSQL, cnn)
        cmdMac.CommandType = Data.CommandType.Text
        Dim daMac As New Data.SqlClient.SqlDataAdapter(cmdMac)
        daMac.Fill(TBL)
        cnn.Close()
        cnn = Nothing
        Return TBL
    End Function

    Private Function GetAccMacResults(ByVal EquipID As Integer, ByVal SampleID As String, ByVal AccID As String) As Data.DataTable
        Dim TBL As New Data.DataTable
        Dim sSQL As String = "Select a.Equipment_ID, a.Accession_ID, a.Reflexer_ID, b.Test_ID , a.QP, a.Result, a.T_Result, " &
        "a.Flag, a.Run_Date, a.Opr from Mac_Results a inner join (Ana_Test b inner join Anas c on b.Ana_ID = c.ID) on " &
        "c.Equipment_ID = a.Equipment_ID where b.eqResult_ID = a.Test_ID and a.Equipment_ID = " & EquipID & " and a.Accession_ID " &
        "= '" & SampleID & "' and b.Test_ID in (Select Test_ID from Acc_Results where Accession_ID = " & Val(AccID) & ")"
        Dim cnn As New Data.SqlClient.SqlConnection(connString)
        cnn.Open()
        Dim cmdMac As New Data.SqlClient.SqlCommand(sSQL, cnn)
        cmdMac.CommandType = Data.CommandType.Text
        Dim daMac As New Data.SqlClient.SqlDataAdapter(cmdMac)
        daMac.Fill(TBL)
        cnn.Close()
        cnn = Nothing
        Return TBL
    End Function

    Private Function GetInfoIDs(ByVal AccID As Long) As String()
        Dim Infos() As String = {""}
        Dim sSQL As String = "Select * from Acc_Info_Results where Accession_ID = " & AccID
        If connString <> "" Then
            Dim cnn As New Data.SqlClient.SqlConnection(connString)
            cnn.Open()
            Dim cmdsel As New Data.SqlClient.SqlCommand(sSQL, cnn)
            cmdsel.CommandType = Data.CommandType.Text
            Dim drsel As Data.SqlClient.SqlDataReader = cmdsel.ExecuteReader
            If drsel.HasRows Then
                While drsel.Read
                    If Infos(UBound(Infos)) <> "" Then ReDim Preserve Infos(UBound(Infos) + 1)
                    Infos(UBound(Infos)) = drsel("Test_ID").ToString & "|" & drsel("Info_ID").ToString
                End While
            End If
            cnn.Close()
            cnn = Nothing
            'Else
            '    Dim cnn As New Data.Odbc.OdbcConnection(connstring)
            '    cnn.Open()
            '    Dim cmdsel As New Data.Odbc.OdbcCommand(sSQL, cnn)
            '    cmdsel.CommandType = Data.CommandType.Text
            '    Dim drsel As Data.Odbc.OdbcDataReader = cmdsel.ExecuteReader
            '    If drsel.HasRows Then
            '        While drsel.Read
            '            If Infos(UBound(Infos)) <> "" Then ReDim Preserve Infos(UBound(Infos) + 1)
            '            Infos(UBound(Infos)) = drsel("Test_ID").ToString & "|" & drsel("Info_ID").ToString
            '        End While
            '    End If
            '    cnn.Close()
            '    cnn = Nothing
        End If
        Return Infos
    End Function

    Private Sub ApplyPatientResults(ByVal SAMPACCS(,) As String)
        If EquipID <> -1 Then
            Dim i As Integer
            'Dim n As Integer
            Dim IsDirty As Boolean = False
            'Dim PTestID() As String
            Dim Result As String = ""
            Dim Flag() As String = {"", ""}
            Dim Samples As String = ""
            Dim SMPLID As String = ""
            Dim SrcID As String = ""
            Dim Conds() As String = {"", ""}
            Dim RefRDs() As String = {""}
            Dim Calculator() As String
            If btnAccQC.Checked = False Then    'Patient
                Dim StatusFrom As String = ""
                Dim StatusTo As String = ""
                Dim RPTStatus As String = ""
                For i = 0 To UBound(SAMPACCS, 2)
                    If Not BW.CancellationPending Then
                        If SAMPACCS(0, i) <> "" And SAMPACCS(1, i) <> "" Then
                            BW.ReportProgress((i + 1) * 100 / (UBound(SAMPACCS, 2) + 1),
                            (i + 1).ToString & " of " & (UBound(SAMPACCS, 2) + 1).ToString)
                            RPTStatus = GetReportStatus(SAMPACCS(1, i))
                            ReDim RefRDs(0) : RefRDs(0) = ""
                            Try
                                If InStr(RPTStatus, "FINAL") = 0 OrElse chkOverwrite.Checked = True Then
                                    SMPLID = SAMPACCS(0, i)
                                    If InStrRev(SMPLID, "-") > 0 Then
                                        Dim Comps() As String = Split(SMPLID, "-")
                                        For s As Integer = 0 To Comps.Length - 1
                                            If Comps(s) = SAMPACCS(1, i) Then
                                                'SMPLID = Comps(s)
                                                If s < Comps.Length - 1 Then
                                                    SrcID = Comps(s + 1)
                                                Else
                                                    SrcID = ""
                                                End If
                                            End If
                                        Next
                                    Else
                                        SrcID = ""
                                    End If
                                    StatusFrom = "" : StatusTo = ""
                                    '******* Fetch Mac_Results *****************************
                                    Dim MacResTbl As Data.DataTable = GetAccMacResults(EquipID, SAMPACCS(0, i), SAMPACCS(1, i))
                                    'Update Acc_Results
                                    For tr As Integer = 0 To MacResTbl.Rows.Count - 1
                                        If MacResTbl.Rows(tr).Item("Flag") IsNot DBNull.Value Then
                                            Flag(0) = Trim(MacResTbl.Rows(tr).Item("Flag"))
                                        Else
                                            Flag(0) = ""
                                        End If
                                        If MacResTbl.Rows(tr).Item("T_Result") Is DBNull.Value _
                                        Then MacResTbl.Rows(tr).Item("T_Result") = ""
                                        Conds = UpdateAccResults(Val(SAMPACCS(1, i)), Val(MacResTbl.Rows(tr).Item("Test_ID")),
                                        MacResTbl.Rows(tr).Item("Result"), MacResTbl.Rows(tr).Item("T_Result"), Flag, RPTStatus,
                                        chkOverwrite.Checked, chkRelVal.Checked)
                                        If Conds(0) <> "" Then StatusFrom += Conds(0)
                                        If Conds(1) <> "" Then
                                            StatusTo += Conds(1)
                                            IsDirty = True
                                        End If
                                    Next
                                    'Update Acc_Info_Results
                                    MacResTbl = GetInfoMacResults(EquipID, SAMPACCS(0, i), SAMPACCS(1, i))
                                    For tr As Integer = 0 To MacResTbl.Rows.Count - 1
                                        Dim InfoIDs() As String = GetInfoIDs(Val(SAMPACCS(1, i)))
                                        For inf As Integer = 0 To InfoIDs.Length - 1
                                            If InfoIDs(inf) <> "" Then
                                                Dim Data() As String = Split(InfoIDs(inf), "|")
                                                If MacResTbl.Rows(tr).Item("Flag") IsNot DBNull.Value Then
                                                    Flag(0) = Trim(MacResTbl.Rows(tr).Item("Flag"))
                                                Else
                                                    Flag(0) = ""
                                                End If
                                                If MacResTbl.Rows(tr).Item("T_Result") Is DBNull.Value _
                                                Then MacResTbl.Rows(tr).Item("T_Result") = ""
                                                Conds = UpdateInfoResults(Val(SAMPACCS(1, i)), Val(Data(0)), Val(Data(1)),
                                                MacResTbl.Rows(tr).Item("Result"), MacResTbl.Rows(tr).Item("T_Result"), Flag,
                                                RPTStatus, chkOverwrite.Checked, chkRelVal.Checked)
                                                If Conds(1) <> "" Then
                                                    If Conds(0) <> "" Then StatusFrom += Conds(0)
                                                    StatusTo += Conds(1)
                                                    IsDirty = True
                                                End If
                                            End If
                                        Next
                                    Next
                                    'Update Ref_Results for AccRef
                                    MacResTbl = GetRefMacResults(EquipID, SAMPACCS(0, i), SAMPACCS(1, i))
                                    RefRDs = GetRefRecords(Val(SAMPACCS(1, i)))
                                    For RL As Integer = 0 To RefRDs.Length - 1
                                        Dim LCLS() As String = Split(RefRDs(RL), "|")   '0=reflexer, 1=reflexed, 2=Test
                                        For tr As Integer = 0 To MacResTbl.Rows.Count - 1
                                            If Val(LCLS(2)) = Val(MacResTbl.Rows(tr).Item("Test_ID")) Then
                                                If MacResTbl.Rows(tr).Item("Flag") IsNot DBNull.Value Then
                                                    Flag(0) = Trim(MacResTbl.Rows(tr).Item("Flag"))
                                                Else
                                                    Flag(0) = ""
                                                End If
                                                If MacResTbl.Rows(tr).Item("T_Result") Is DBNull.Value _
                                                Then MacResTbl.Rows(tr).Item("T_Result") = ""
                                                Conds = UpdateRefResults(Val(SAMPACCS(1, i)), Val(LCLS(0)), Val(LCLS(1)),
                                                Val(LCLS(2)), MacResTbl.Rows(tr).Item("Result"), MacResTbl.Rows(tr).Item("T_Result"),
                                                Flag, RPTStatus, chkOverwrite.Checked, chkRelVal.Checked)
                                                If Conds(1) <> "" Then
                                                    If Conds(0) <> "" Then StatusFrom += Conds(0)
                                                    If Conds(1) <> "" Then
                                                        StatusTo += Conds(1)
                                                        IsDirty = True
                                                    End If
                                                End If
                                            End If
                                        Next
                                    Next
                                    '*** Audit Trail ***
                                    If SystemConfig.AuditTrail = True Then
                                        If StatusFrom.Length > 1 Then StatusFrom =
                                        Microsoft.VisualBasic.Mid(StatusFrom, 1, Len(StatusFrom) - 1)
                                        If StatusTo.Length > 1 Then StatusTo =
                                        Microsoft.VisualBasic.Mid(StatusTo, 1, Len(StatusTo) - 1)
                                        LogUserEvent(ThisUser.ID, 21, Date.Now, "Accession",
                                        SAMPACCS(1, i), StatusFrom, StatusTo)
                                        StatusFrom = "" : StatusTo = ""
                                    End If
                                    '*** End Audit Trail ****
                                    '*****  End of Acc_Result + first level reflex Update ***************************
                                    My.Application.DoEvents()
                                    StatusFrom = "" : StatusTo = ""
                                    ' ****** Start of Calculation *****************************
                                    Dim sSQL As String = "Select a.*, b.Formula from Acc_Results a inner join Tests b on b.ID = " &
                                    "a.Test_ID where a.Accession_ID in (Select ID from Requisitions where Received <> 0) and " &
                                    "b.IsCalculated <> 0 and b.Formula <> '' and a.Accession_ID = " & Val(SAMPACCS(1, i))
                                    '
                                    Dim cncalc As New Data.SqlClient.SqlConnection(connString)
                                    cncalc.Open()
                                    Dim cmdcalc As New Data.SqlClient.SqlCommand(sSQL, cncalc)
                                    Dim drcalc As Data.SqlClient.SqlDataReader = cmdcalc.ExecuteReader
                                    If drcalc.HasRows Then
                                        While drcalc.Read
                                            If TestInAnalysis(EquipID, drcalc("Formula")) Then
                                                '*** Audit Trail ***
                                                If SystemConfig.AuditTrail = True Then
                                                    If (drcalc("Result") IsNot DBNull.Value AndAlso drcalc("Result") <> "") _
                                                    AndAlso drcalc("Released") IsNot DBNull.Value Then
                                                        StatusFrom += drcalc("Test_ID").ToString &
                                                        "=" & drcalc("Result") & "," & drcalc("Released") & "|"
                                                    Else
                                                        StatusFrom += drcalc("Test_ID").ToString & "=, 0|"
                                                    End If
                                                End If
                                                '*** End Audit Trail ****
                                                Calculator = UpdateCalculator(drcalc("Test_ID"))
                                                If Calculator(0) <> "" And Calculator(1) <> "" Then
                                                    Result = CalculateResult(Calculator(0), Calculator(1), Val(SAMPACCS(1, i)))
                                                    If ReportableFormatRequired(drcalc("Test_ID")) Then _
                                                    Result = FormatToReportable(Result, drcalc("Test_ID"))
                                                    Dim TResult As String
                                                    If drcalc("T_Result") Is DBNull.Value Then
                                                        TResult = ""
                                                    Else
                                                        TResult = drcalc("T_Result")
                                                    End If
                                                    UpdateAccResults(Val(SAMPACCS(1, i)), drcalc("Test_ID"), Result,
                                                    TResult, GetFlag(Val(SAMPACCS(1, i)), Result, drcalc("Test_ID")),
                                                    RPTStatus, chkOverwrite.Checked, chkRelVal.Checked)
                                                    IsDirty = True
                                                End If
                                            End If
                                        End While
                                    End If
                                    cncalc.Close()
                                    cncalc = Nothing
                                    '*** Audit Trail ***
                                    If SystemConfig.AuditTrail = True Then
                                        If StatusFrom.Length > 1 Then StatusFrom =
                                        Microsoft.VisualBasic.Mid(StatusFrom, 1, Len(StatusFrom) - 1)
                                        If StatusTo.Length > 1 Then StatusTo =
                                        Microsoft.VisualBasic.Mid(StatusTo, 1, Len(StatusTo) - 1)
                                        If StatusFrom <> StatusTo Then _
                                        LogUserEvent(ThisUser.ID, 21, Date.Now, "Accession",
                                        SAMPACCS(1, i), StatusFrom, StatusTo)
                                    End If
                                    '*** End Audit Trail ****
                                    ' ****** End of Calculation *****************************
                                    '*********************************Temur***********************

                                    If IsDirty = True Then
                                        If IsDate(txtBatchDate.Text) Then
                                            UpdateReportTime(Val(SAMPACCS(1, i)),
                                            CDate(txtBatchDate.Text & " " & Format(Date.Now, "HH:mm:ss")))
                                        Else
                                            UpdateReportTime(Val(SAMPACCS(1, i)), Date.Now)
                                        End If
                                        IsDirty = False
                                        Dim DirID As String = GetDefaultDirectorID()
                                        If DirID <> "" Then ExecuteSqlProcedure("Update Requisitions set " &
                                        "Director_ID = " & Val(DirID) & " where ID = " &
                                        Val(SAMPACCS(1, i)) & " and Director_ID is NULL")
                                        '
                                        LogEvent(Val(SAMPACCS(1, i)), 21,
                                        GetOrdProvIDFromAccID(Val(SAMPACCS(1, i))),
                                        "Autoapply", True, ThisUser.Name, "Result autoapplied")
                                        '
                                        If SystemConfig.HL7AutoPub = True Then _
                                        UpdateAccDisbursement(Val(SAMPACCS(1, i)))
                                    End If
                                    Samples += "'" & SAMPACCS(0, i) & "'" & ", "
                                End If  'of non-complete or overwrite
                            Catch ex As Exception
                                MsgBox(ex.Message)
                            End Try
                        Else
                            Exit For
                        End If
                    End If
                Next
                If Samples.EndsWith(", ") Then Samples = Microsoft.VisualBasic.Mid(Samples, 1, Len(Samples) - 2)
            End If
            If chkClear.Checked = True Then
                If Samples.Length > 0 Then
                    ExecuteSqlProcedure("Delete from Mac_Results where Equipment_ID = " &
                    EquipID & " and Accession_ID in (" & Samples & ")")
                    Samples = ""
                End If
            End If
        End If
        'EnableActions()
    End Sub

    Private Function TestInAnalysis(ByVal EquipID As Integer, ByVal Formula As String) As Boolean
        Dim TinA As Boolean = False
        Dim Data As String = Formula.Substring(InStr(Formula, "{"))
        Dim TID As String = Data.Substring(0, InStr(Data, "}") - 1)
        Dim sSQL As String = "Select a.* from Ana_Test a inner join Anas b on b.ID = " &
        "a.Ana_ID where a.Test_ID = " & Val(TID) & " and b.Equipment_ID = " & EquipID
        '
        Dim cntia As New Data.SqlClient.SqlConnection(connString)
        cntia.Open()
        Dim cmdtia As New Data.SqlClient.SqlCommand(sSQL, cntia)
        cmdtia.CommandType = CommandType.Text
        Dim drtia As Data.SqlClient.SqlDataReader = cmdtia.ExecuteReader
        If drtia.HasRows Then TinA = True
        cntia.Close()
        cntia = Nothing
        Return TinA
    End Function

    Private Function IsResultPanic(ByVal Flag As String) As Boolean
        Dim IsPanic As Boolean = False
        If Flag = "LP" Or Flag = "HP" Or
        InStr(Flag, "Panic") > 0 Or
        InStr(Flag, "Critical") > 0 Then IsPanic = True
        Return IsPanic
    End Function

    Private Function ValidateSourceID(ByVal SrcID As String, ByVal AccID As String) As String
        Dim SID As String = ""
        If IsNumeric(AccID) And IsNumeric(SrcID) Then
            Dim cntia As New Data.SqlClient.SqlConnection(connString)
            cntia.Open()
            Dim cmdtia As New Data.SqlClient.SqlCommand("Select Material_ID " &
            "from Test_Material where Test_ID in (Select Test_ID from Acc_Results " &
            "where Accession_ID = " & AccID & ")", cntia)
            cmdtia.CommandType = CommandType.Text
            Dim drtia As Data.SqlClient.SqlDataReader = cmdtia.ExecuteReader
            If drtia.HasRows Then
                While drtia.Read
                    If drtia("Material_ID") = Val(SrcID) Then
                        SID = drtia("Material_ID").ToString
                        Exit While
                    End If
                End While
            End If
            cntia.Close()
            cntia = Nothing
        End If
        Return SID
    End Function

    Private Function HasReflux(ByVal AccID As Long, ByVal ReflexerID As Integer,
    ByVal ReflexedID As Integer) As Boolean
        Dim Reflux As Boolean = False
        Dim cnhr As New Data.SqlClient.SqlConnection(connString)
        cnhr.Open()
        Dim cmdhr As New Data.SqlClient.SqlCommand("Select * from Ref_Results " &
        "where Accession_ID = " & AccID & " and Reflexer_ID = " & ReflexerID &
        " and Reflexed_ID = " & ReflexedID, cnhr)
        cmdhr.CommandType = CommandType.Text
        Dim drhr As Data.SqlClient.SqlDataReader = cmdhr.ExecuteReader
        If drhr.HasRows Then Reflux = True
        cnhr.Close()
        cnhr = Nothing
        Return Reflux
    End Function

    Private Function CalculateResult(ByVal CalcTestID As Integer, ByVal formula As String, ByVal AccID As Long) As String
        Dim NoRes As Boolean = False
        'Dim SC As New MSScriptControl.ScriptControl
        Dim SC As Object = CreateObject("MSScriptControl.ScriptControl")
        SC.Language = "VBSCript"
        'Dim i As Integer
        Dim P1 As Integer
        Dim P2 As Integer
        Dim FinalRes As String = ""
        Dim Decs As String = UpdateDecs(CalcTestID)
        Dim CompRes As String = ""
        Dim TestId As String
        If formula <> "" Then
            formula = Replace(formula, " ", "")
            Do Until InStr(formula, "}") = 0
                P1 = InStr(formula, "{")
                P2 = InStr(formula, "}")
                TestId = formula.Substring(P1, P2 - (P1 + 1))
                CompRes = GetTestResult(TestId, AccID)
                If CompRes Is Nothing Or CompRes = "" Then
                    NoRes = True
                    Exit Do
                Else
                    CompRes = Replace(CompRes, ">", "")
                    CompRes = Replace(CompRes, "<", "")
                    CompRes = Replace(CompRes, ">=", "")
                    CompRes = Replace(CompRes, "<=", "")
                    formula = Replace(formula, "{" & TestId & "}", CompRes)
                End If
            Loop
            If NoRes = False Then
                If InStr(formula, "@Age@") > 0 Or InStr(formula, "@Sex@") > 0 Or
                InStr(formula, "@Ethnicity@") > 0 Then     'Age is used
                    Dim Age() As String = GetPatientAge(AccID)
                    If Age(0) = "0" Then Age(0) = "1"
                    formula = Replace(formula, "@Age@", Val(Age(0)))
                    If InStr(formula, "@Sex@") > 0 Then formula = Replace(formula, "@Sex@", Val(Age(1)))
                    If InStr(formula, "@Ethnicity@") > 0 Then formula = Replace(formula, "@Ethnicity@", Val(Age(2)))
                End If
                If InStr(formula, "}") = 0 And InStr(formula, "@") = 0 Then     'Formula resolved
                    'SC = New MSScriptControl.ScriptControl
                    'SC.Language = "VBScript"
                    Do Until InStr(formula, "IIF(") = 0
                        If InStr(formula, "IIF(") > 0 Then
                            Dim sTEMP = Microsoft.VisualBasic.Mid(formula,
                            InStr(formula, "IIF("))
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
                            formula = Replace(formula, sTEMP, Myval)
                        End If
                    Loop
                    '
                    Do Until InStr(formula, "Min(") = 0
                        If InStr(formula, "Min(") > 0 Then
                            Dim sTEMP = Microsoft.VisualBasic.Mid(formula,
                            InStr(formula, "Min("))
                            sTEMP = Microsoft.VisualBasic.Mid(sTEMP, 1, InStr(sTEMP, ")"))
                            Dim Expr As String = Microsoft.VisualBasic.Mid(sTEMP,
                            InStr(sTEMP, "Min(") + 4)
                            Expr = Microsoft.VisualBasic.Mid(Expr, 1, InStr(Expr, ")") - 1)
                            Dim Params() As String = Split(Expr, ",")
                            Dim MyVal As String = Math.Min(Val(Params(0)), Val(Params(1)))
                            formula = Replace(formula, sTEMP, MyVal)
                            sTEMP = "" : MyVal = ""
                        End If
                    Loop
                    '
                    Do Until InStr(formula, "Max(") = 0
                        If InStr(formula, "Max(") > 0 Then
                            Dim sTEMP = Microsoft.VisualBasic.Mid(formula,
                            InStr(formula, "Max("))
                            sTEMP = Microsoft.VisualBasic.Mid(sTEMP, 1, InStr(sTEMP, ")"))
                            Dim Expr As String = Microsoft.VisualBasic.Mid(sTEMP,
                            InStr(sTEMP, "Max(") + 4)
                            Expr = Microsoft.VisualBasic.Mid(Expr, 1, InStr(Expr, ")") - 1)
                            Dim Params() As String = Split(Expr, ",")
                            Dim MyVal As String = Math.Max(Val(Params(0)), Val(Params(1)))
                            formula = Replace(formula, sTEMP, MyVal)
                            sTEMP = "" : MyVal = ""
                        End If
                    Loop
                    '
                    Try
                        FinalRes = Format(SC.Eval(formula), Decs)
                    Catch ex As Exception
                    End Try
                End If
            Else
                FinalRes = ""
            End If
        End If
        If FinalRes <> "" Then
            CalculateResult = Format(Val(FinalRes), Decs)
        Else
            CalculateResult = FinalRes
        End If
    End Function

    Private Function GetPatientAge(ByVal AccID As Long) As String()
        Dim Age() As String = {"", "", ""}
        Dim sSQL As String = "Select a.AccessionDate as AccDate, b.DOB as DOB, b.Sex as Sex, b.Ethnicity as " &
        "Ehnicity from Requisitions a inner join Patients b on a.Patient_ID = b.ID where a.ID = " & AccID
        '
        Dim cnGPA As New SqlClient.SqlConnection(connString)
        cnGPA.Open()
        Dim cmdGPA As New SqlClient.SqlCommand(sSQL, cnGPA)
        cmdGPA.CommandType = CommandType.Text
        Dim drGPA As SqlClient.SqlDataReader = cmdGPA.ExecuteReader
        If drGPA.HasRows Then
            While drGPA.Read
                If drGPA("DOB") IsNot DBNull.Value Then
                    Age(0) = DateDiff(DateInterval.Year, drGPA("DOB"), drGPA("AccDate"))
                    If drGPA("Sex") = "F" Then
                        Age(1) = "0"
                    Else
                        Age(1) = "1"
                    End If
                    If drGPA("Ehnicity") IsNot DBNull.Value Then
                        If drGPA("Ehnicity") = "Black" Then
                            Age(2) = "1"
                        Else
                            Age(2) = "0"
                        End If
                    Else
                        Age(2) = "0"
                    End If
                End If
            End While
        Else
            Age(0) = "0" : Age(1) = "0" : Age(2) = "0"
        End If
        cnGPA.Close()
        cnGPA = Nothing
        Return Age
    End Function

    Private Function GetTestResult(ByVal TestID As Integer, ByVal AccID As Long) As String
        Dim Result As String = ""
        Dim sSQL As String = "Select * from Acc_Results where Accession_ID = " & AccID & " and Test_ID = " & TestID
        '
        Dim cnGTR As New SqlClient.SqlConnection(connString)
        cnGTR.Open()
        Dim cmdGTR As New SqlClient.SqlCommand(sSQL, cnGTR)
        cmdGTR.CommandType = CommandType.Text
        Dim drGTR As SqlClient.SqlDataReader = cmdGTR.ExecuteReader
        If drGTR.HasRows Then
            While drGTR.Read
                If drGTR("Result") IsNot DBNull.Value _
                AndAlso Trim(drGTR("Result")) <> "" Then
                    Result = Trim(drGTR("Result"))
                End If
            End While
        End If
        cnGTR.Close()
        cnGTR = Nothing
        Return Result
    End Function

    Private Function UpdateCalculator(ByVal TestID As Integer) As String()
        Dim Upd() As String = {"", ""}
        Dim sSQL As String = "Select * from Tests where IsCalculated <> 0 and Formula <> '' and ID = " & TestID
        '
        Dim cnuc As New Data.SqlClient.SqlConnection(connString)
        cnuc.Open()
        Dim cmduc As New Data.SqlClient.SqlCommand(sSQL, cnuc)
        cmduc.CommandType = Data.CommandType.Text
        Dim druc As Data.SqlClient.SqlDataReader = cmduc.ExecuteReader
        If druc.HasRows Then
            While druc.Read
                Upd(0) = TestID.ToString
                Upd(1) = Trim(druc("Formula"))
            End While
        End If
        cnuc.Close()
        cnuc = Nothing
        Return Upd
    End Function

    Private Function GetPTestIDs(ByVal EquipID As Integer, ByVal AccID As String, ByVal MTestID As String, ByVal SrcID As String) As String()
        Dim PTests() As String = {""}
        Dim sSQL As String = ""
        Dim Found As Boolean = False
        If SrcID <> "" Then
            sSQL = "Select a.Test_ID as PTestID from Ana_Test a inner join (" &
            "Test_Material b inner join Sources c on c.Material_ID = b.Material_ID) " &
            "on b.Test_ID = a.Test_ID where a.EqResult_ID = '" & MTestID & "' and " &
            "a.Ana_ID in (Select ID from Anas where Equipment_ID = " & EquipID &
            ") and c.ID = " & Val(SrcID)
        Else
            sSQL = "Select Test_ID as PTestID from Ana_Test where EqResult_ID = '" &
            MTestID & "' and Ana_ID in (Select ID from Anas where Equipment_ID = " _
            & EquipID & ")"
        End If
        '
        Dim cnn As New Data.SqlClient.SqlConnection(connString)
        cnn.Open()
        Dim cmdSel As New Data.SqlClient.SqlCommand(sSQL, cnn)
        cmdSel.CommandType = Data.CommandType.Text
        Dim DRsel As Data.SqlClient.SqlDataReader = cmdSel.ExecuteReader
        If DRsel.HasRows Then
            While DRsel.Read
                If DRsel("PTestID") IsNot DBNull.Value AndAlso
                Trim(DRsel("PTestID").ToString) <> "" Then
                    Found = False
                    For i As Integer = 0 To PTests.Length - 1
                        If PTests(i) = DRsel("PTestID").ToString Then Found = True
                    Next
                    If Not Found Then
                        If PTests(UBound(PTests)) <> "" Then ReDim Preserve PTests(UBound(PTests) + 1)
                        PTests(UBound(PTests)) = Trim(DRsel("PTestID").ToString)
                    End If
                End If
            End While
        End If
        cnn.Close()
        cnn = Nothing
        Return PTests
    End Function

    Private Function IsQCInRange(ByVal ControlID As Long, ByVal TestID As Integer, ByVal Result As String) As Boolean
        Dim InRange As Boolean = False
        If cmbBatches.SelectedIndex <> -1 Then
            Dim ItemX As MyList = cmbBatches.SelectedItem
            Dim sSQL As String = "Select * from Ana_Ranges where Control_ID = " & ControlID & " and Test_ID = " &
            TestID & " and Ana_ID in (Select Analysis_ID from Runs where ID = " & ItemX.ItemData & ")"
            '
            Dim cnQCIR As New SqlClient.SqlConnection(connString)
            cnQCIR.Open()
            Dim cmdQCIR As New SqlClient.SqlCommand(sSQL, cnQCIR)
            cmdQCIR.CommandType = CommandType.Text
            Dim drQCIR As SqlClient.SqlDataReader = cmdQCIR.ExecuteReader
            If drQCIR.HasRows Then
                While drQCIR.Read
                    If drQCIR("Quantitative") = 0 Then     'Qualitative
                        If Trim(drQCIR("MeanNormal")) = Trim(Result) Then
                            InRange = True
                        End If
                    Else
                        If Val(Result) >= drQCIR("Low") And Val(Result) <= drQCIR("High") Then
                            InRange = True
                        End If
                    End If
                End While
            End If
            cnQCIR.Close()
            cnQCIR = Nothing
        End If
        Return InRange
    End Function

    Private Function IsQualitative(ByVal TestID As Integer) As Boolean
        Dim Qualitative As Boolean = False
        Dim sSQL As String = "Select Qualitative from Tests where ID = " & TestID
        If connString <> "" Then
            Dim cnIQ As New SqlClient.SqlConnection(connString)
            cnIQ.Open()
            Dim cmdIQ As New SqlClient.SqlCommand(sSQL, cnIQ)
            cmdIQ.CommandType = CommandType.Text
            Dim drIQ As SqlClient.SqlDataReader = cmdIQ.ExecuteReader
            If drIQ.HasRows Then
                While drIQ.Read
                    Qualitative = drIQ("Qualitative")
                End While
            End If
            cnIQ.Close()
            cnIQ = Nothing
            'Else
            '    Dim cnIQ As New Odbc.OdbcConnection(connstring)
            '    cnIQ.Open()
            '    Dim cmdIQ As New Odbc.OdbcCommand(sSQL, cnIQ)
            '    cmdIQ.CommandType = CommandType.Text
            '    Dim drIQ As Odbc.OdbcDataReader = cmdIQ.ExecuteReader
            '    If drIQ.HasRows Then
            '        While drIQ.Read
            '            Qualitative = drIQ("Qualitative")
            '        End While
            '    End If
            '    cnIQ.Close()
            '    cnIQ = Nothing
        End If
        Return Qualitative
    End Function

    Private Sub btnAccQC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccQC.Click
        If btnAccQC.Checked = False Then    'Acc
            btnAccQC.Text = "Accessioned"
            lblAccQC.Text = "Accessions"
            btnAccQC.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Acc.ico")
            chkRelVal.Text = "Release Non Panic"
            cmbOverride.Enabled = False
            dgvAccs.Visible = True
            dgvControls.Visible = False
            SpecificAccessions.Show()
            Label2.Show()
            automap.Hide()
        Else
            btnAccQC.Text = "Quality Control"
            lblAccQC.Text = "Controls"
            btnAccQC.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\chart.ico")
            chkRelVal.Text = "Force Validate"
            cmbOverride.Enabled = True
            dgvAccs.Visible = False
            dgvControls.Visible = True
            SpecificAccessions.Hide()
            Label2.Hide()
            automap.Show()
        End If
        cmbEquips.SelectedIndex = -1
        cmbBatches.SelectedIndex = -1
        txtRunDate.Text = ""
        txtAccQC.Text = ""
        txtValid.Text = ""
        dgvAccs.Rows.Clear()
        dgvControls.Rows.Clear()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If BW.IsBusy Then
            BW.CancelAsync()
        Else
            Me.Close()
        End If
    End Sub

    Private Sub PopulateOverride(ByVal Analytes As Integer)
        cmbOverride.Items.Clear()
        Dim i As Integer
        For i = 0 To Analytes
            cmbOverride.Items.Add(CInt(i / Analytes * 100))
        Next
    End Sub

    Private Sub UpdateValStatus(ByVal RunID As Long)
        Dim i As Integer
        Dim sSQL As String = "Select Validated from Runs where ID = " & RunID
        Dim cnUVS1 As New SqlClient.SqlConnection(connString)
        cnUVS1.Open()
        Dim cmdUVS1 As New SqlClient.SqlCommand(sSQL, cnUVS1)
        cmdUVS1.CommandType = CommandType.Text
        Dim drUVS1 As SqlClient.SqlDataReader = cmdUVS1.ExecuteReader
        If drUVS1.HasRows Then
            While drUVS1.Read
                If drUVS1("Validated") IsNot DBNull.Value Then
                    If drUVS1("Validated") <> 0 Then
                        txtValid.Text = "Yes"
                    Else
                        txtValid.Text = "No"
                    End If
                Else
                    txtValid.Text = "No"
                End If
            End While
        End If
        cnUVS1.Close()
        cnUVS1 = Nothing
        '
        sSQL = "Select Distinct Test_ID from QC_Results where Run_ID = " & RunID
        Dim cnUVS2 As New SqlClient.SqlConnection(connString)
        cnUVS2.Open()
        Dim cmdUVS2 As New SqlClient.SqlCommand(sSQL, cnUVS2)
        cmdUVS2.CommandType = CommandType.Text
        Dim cnt As Integer = 0
        Dim drUVs2 As SqlClient.SqlDataReader = cmdUVS2.ExecuteReader
        If drUVs2.HasRows Then
            While drUVs2.Read
                cnt += 1

            End While
            UpdateInRangePercent(cnt)
        End If
        cnUVS2.Close()
        cnUVS2 = Nothing
        '
        sSQL = "Select InRangePercent from Anas where ID in (Select Analysis_ID from Runs where ID = " & RunID & ")"
        Dim cnUVS3 As New SqlClient.SqlConnection(connString)
        cnUVS3.Open()
        Dim cmdUVS3 As New SqlClient.SqlCommand(sSQL, cnUVS3)
        cmdUVS3.CommandType = CommandType.Text
        Dim drUVS3 As SqlClient.SqlDataReader = cmdUVS3.ExecuteReader
        If drUVS3.HasRows Then
            While drUVS3.Read
                For i = 0 To cmbOverride.Items.Count - 1
                    If i > 0 And i < cmbOverride.Items.Count - 1 Then   'between
                        If drUVS3("InRangePercent") > cmbOverride.Items(i - 1) And
                        drUVS3("InRangePercent") < cmbOverride.Items(i + 1) Then
                            cmbOverride.SelectedIndex = i
                            Exit For
                        End If
                    Else    'Lbound or UBound
                        If drUVS3("InRangePercent") = cmbOverride.Items(i) Then
                            cmbOverride.SelectedIndex = i
                            Exit For
                        End If
                    End If
                Next
            End While
        End If
        cnUVS3.Close()
        cnUVS3 = Nothing
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

    Private Sub cmbEquips_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEquips.SelectedIndexChanged
        If cmbEquips.SelectedIndex <> -1 Then
            Dim ItemX As MyList = cmbEquips.SelectedItem
            If IsDate(txtBatchDate.Text) = True Then _
            PopulateBatches(CDate(txtBatchDate.Text), ItemX.ItemData)
        End If
    End Sub

    Private Sub PopulateBatches(ByVal BatchDate As Date, ByVal EquipID As Integer)
        cmbBatches.Items.Clear()
        Dim MinDate As Date = DateAdd(DateInterval.Day, -7, BatchDate)
        'Dim MaxDate As Date = GetMaximumDate(EquipID)
        Dim cnpb As New SqlClient.SqlConnection(connString)
        cnpb.Open()
        Dim cmdpb As New SqlClient.SqlCommand("Select * from Runs where " &
        "RunDate >= '" & Format(MinDate, SystemConfig.DateFormat) & "' and Analysis_ID " &
        "in (Select distinct ID from Anas where Equipment_ID = " & EquipID & ")", cnpb)
        cmdpb.CommandType = CommandType.Text
        Dim drpb As SqlClient.SqlDataReader = cmdpb.ExecuteReader
        If drpb.HasRows Then
            While drpb.Read
                cmbBatches.Items.Add(New MyList(drpb("Name"), drpb("ID")))
            End While
        End If
        cnpb.Close()
        cnpb = Nothing
    End Sub

    Private Function GetMinimumDate(ByVal EquipID As Integer) As Date
        Dim MinDate As Date
        Dim cnpb As New SqlClient.SqlConnection(connString)
        cnpb.Open()
        Dim cmdpb As New SqlClient.SqlCommand("Select Min(Run_Date) " &
        "as MinDate from Mac_Results where Equipment_ID = " & EquipID, cnpb)
        cmdpb.CommandType = CommandType.Text
        Dim drpb As SqlClient.SqlDataReader = cmdpb.ExecuteReader
        If drpb.HasRows Then
            While drpb.Read
                MinDate = drpb("MinDate")
            End While
        End If
        cnpb.Close()
        cnpb = Nothing
        Return MinDate
    End Function

    Private Function GetMaximumDate(ByVal EquipID As Integer) As Date
        Dim MaxDate As Date
        Dim cnpb As New SqlClient.SqlConnection(connString)
        cnpb.Open()
        Dim cmdpb As New SqlClient.SqlCommand("Select Max(Run_Date) " &
        "as MaxDate from Mac_Results where Equipment_ID = " & EquipID, cnpb)
        cmdpb.CommandType = CommandType.Text
        Dim drpb As SqlClient.SqlDataReader = cmdpb.ExecuteReader
        If drpb.HasRows Then
            While drpb.Read
                MaxDate = drpb("MinDate")
            End While
        End If
        cnpb.Close()
        cnpb = Nothing
        Return MaxDate
    End Function

    Private Sub chkClear_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkClear.CheckedChanged
        UpdateApplyProcess()
    End Sub

    Private Sub dgvAccs_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvAccs.DataError
        On Error Resume Next
    End Sub

    Private Sub cmbBatches_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBatches.SelectedIndexChanged
        If cmbBatches.SelectedIndex <> -1 Then
            Dim ItemX As MyList = cmbBatches.SelectedItem
            If btnAccQC.Checked = False Then
                txtAccQC.Text = GetAccessionsQty(ItemX.ItemData)
            Else
                txtAccQC.Text = GetControlsQty(ItemX.ItemData)
                PopulateOverride(GetTestCount(ItemX.ItemData))
            End If
            Dim BatchValid As Boolean = GetBatchValidity(ItemX.ItemData)
            If BatchValid = True Then
                txtValid.Text = "Yes"
                chkRelVal.Enabled = True
            Else
                txtValid.Text = "No"
                If btnAccQC.Checked = False Then
                    chkRelVal.Enabled = False
                Else
                    chkRelVal.Enabled = True
                End If
            End If
            UpdateValStatus(ItemX.ItemData)
        End If
    End Sub

    Private Function GetTestCount(ByVal RunID As Long) As Integer
        Dim Tests As Integer = 0
        Dim cntc As New SqlClient.SqlConnection(connString)
        cntc.Open()
        Dim cmdtc As New SqlClient.SqlCommand("Select Count(ID) as Tests " &
        "from Tests where ID in (Select Test_ID from QC_Results where Run_ID = " &
        RunID & " and Control_ID in (Select Min(Control_ID) as Control_ID " &
        "from QC_Results where Run_ID = " & RunID & "))", cntc)
        cmdtc.CommandType = CommandType.Text
        Dim drtc As SqlClient.SqlDataReader = cmdtc.ExecuteReader
        If drtc.HasRows Then
            While drtc.Read
                Tests = drtc("Tests")
            End While
        End If
        cntc.Close()
        cntc = Nothing
        Return Tests
    End Function

    Private Function GetBatchValidity(ByVal RunID As Long) As Boolean
        Dim Valid As Boolean = False
        Dim cnbv As New SqlClient.SqlConnection(connString)
        cnbv.Open()
        Dim cmdbv As New SqlClient.SqlCommand("Select " &
        "Validated from Runs where ID = " & RunID, cnbv)
        cmdbv.CommandType = CommandType.Text
        Dim drbv As SqlClient.SqlDataReader = cmdbv.ExecuteReader
        If drbv.HasRows Then
            While drbv.Read
                If drbv("Validated") <> 0 Then Valid = True
            End While
        End If
        cnbv.Close()
        cnbv = Nothing
        Return Valid
    End Function

    Private Function GetControlsQty(ByVal RunID As Long) As Integer
        Dim Controls As Integer = 0
        Dim cncc As New SqlClient.SqlConnection(connString)
        cncc.Open()
        Dim cmdcc As New SqlClient.SqlCommand("Select Controls " &
        "from Anas where ID in (Select Analysis_ID from " &
        "Runs where ID = " & RunID & ")", cncc)
        cmdcc.CommandType = CommandType.Text
        Dim drcc As SqlClient.SqlDataReader = cmdcc.ExecuteReader
        If drcc.HasRows Then
            While drcc.Read
                Controls = drcc("Controls")
            End While
        End If
        cncc.Close()
        cncc = Nothing
        Return Controls
    End Function

    Private Function GetAccessionsQty(ByVal RunID As Long) As Integer
        Dim Accs As Integer = 0
        Dim cnac As New SqlClient.SqlConnection(connString)
        cnac.Open()
        Dim cmdac As New SqlClient.SqlCommand("Select Distinct Count(a.Accession_ID) as " &
        "Accs from Acc_Results a inner join (Ana_Test b inner join Runs c on c.Analysis_ID = " &
        "b.Ana_ID) on b.Test_ID = a.Test_ID and a.Run_ID = c.ID where c.ID = " & RunID, cnac)
        cmdac.CommandType = CommandType.Text
        Dim drac As SqlClient.SqlDataReader = cmdac.ExecuteReader
        If drac.HasRows Then
            While drac.Read
                Accs = drac("Accs")
            End While
        End If
        cnac.Close()
        cnac = Nothing
        Return Accs
    End Function

    Private Function ControlInRange(ByVal RunID As Long, ByVal ControlID As Long,
    ByVal TestID As Integer, ByVal Result As String) As Boolean
        Dim InR As Boolean = False
        Dim cncir As New SqlClient.SqlConnection(connString)
        cncir.Open()
        Dim cmdcir As New SqlClient.SqlCommand("Select * from Ana_Ranges " &
        "where Ana_ID in (Select Analysis_ID from Runs where ID = " & RunID &
        ") and Control_ID = " & ControlID & " and Test_ID = " & TestID, cncir)
        cmdcir.CommandType = CommandType.Text
        Dim drcir As SqlClient.SqlDataReader = cmdcir.ExecuteReader
        If drcir.HasRows Then
            While drcir.Read
                If drcir("Quantitative") = 0 Then     'Qualitative
                    If Result = drcir("MeanNormal") Then
                        InR = True
                        Exit While
                    End If
                Else        'Quantitative
                    If Val(Result) >= drcir("Low") And
                    Val(Result) <= drcir("High") Then
                        InR = True
                        Exit While
                    End If
                End If
            End While
        End If
        cncir.Close()
        cncir = Nothing
        Return InR
    End Function

    Private Sub txtBatchDate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBatchDate.Validated
        If txtBatchDate.Text <> "" Then
            If IsDate(txtBatchDate.Text) = False Then
                MsgBox("Invalid Date", MsgBoxStyle.Critical, "Prolis")
                txtBatchDate.Text = ""
                cmbBatches.Items.Clear()
                txtBatchDate.Focus()
            Else
                If cmbEquips.SelectedIndex <> -1 Then
                    Dim ItemX As MyList = cmbEquips.SelectedItem
                    PopulateBatches(CDate(txtBatchDate.Text), ItemX.ItemData)
                End If
            End If
        End If
    End Sub

    Private Sub BW_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BW.DoWork
        ApplyPatientResults(e.Argument)
    End Sub

    Private Sub BW_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BW.ProgressChanged
        pbProcess.Value = e.ProgressPercentage
        lblStatus.Text = e.UserState
    End Sub

    Private Sub BW_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BW.RunWorkerCompleted
        btnAccQC.Checked = False
        PopulateEquipments()
        txtRunDate.Text = ""
        cmbBatches.Items.Clear()
        dgvAccs.Rows.Clear()
        txtBatchDate.Text = Format(Date.Today, SystemConfig.DateFormat)
        txtAccQC.Text = ""
        txtValid.Text = ""
        EnableActions()
    End Sub

    Private Sub dgvControls_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvControls.CellEndEdit
        If e.ColumnIndex = 2 Then
            UpdateApplyProcess()
        End If
    End Sub

    Private Sub dgvControls_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvControls.DataError
        On Error Resume Next
    End Sub

    Private Sub automap_Click(sender As Object, e As EventArgs) Handles automap.Click
        dgvAccs.Rows.Clear()
        If cmbEquips.SelectedIndex <> -1 Then
            Dim ItemX As MyList = cmbEquips.SelectedItem

            LoadControlsAndMap(ItemX.ItemData)
        End If
        UpdateApplyProcess()
    End Sub
End Class