Option Compare Text
Imports System.Windows.Forms
Imports System.Data

Public Class frmReportOrder
    Private CompsDirty As Boolean = False

    Private Sub frmReportOrder_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SaveSetting()
    End Sub

    Private Sub frmReportOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadDepartments()
        'LoadComponents()
        cmbParameter.SelectedIndex = 0
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub LoadDepartments()
        dgvDepartments.Rows.Clear()
        Dim sSQL As String = "Select * from Departments order by Ordinal"
        Dim cnd As New SqlClient.SqlConnection(connString)
        cnd.Open()
        Dim cmdd As New SqlClient.SqlCommand(sSQL, cnd)
        cmdd.CommandType = CommandType.Text
        Dim drd As SqlClient.SqlDataReader = cmdd.ExecuteReader
        If drd.HasRows Then
            While drd.Read
                dgvDepartments.Rows.Add(drd("Dept_Name"), drd("ID"))
            End While
        End If
        cnd.Close()
        cnd = Nothing
        '
    End Sub

    Private Sub LoadComponents(ByVal DeptID As Integer)
        If CompsDirty = True Then

            'ExecuteSqlProcedure("Delete from Department_Test where Department_ID = " & dgvComponents.Rows(0).Cells(0).Value)
            'For i As Integer = 0 To dgvComponents.RowCount - 1
            '    ExecuteSqlProcedure("If Exists (Select * from Department_Test where Department_ID = " &
            '    dgvComponents.Rows(i).Cells(0).Value & " and Test_ID = " & dgvComponents.Rows(i).Cells(2).Value &
            '    ") Update Department_Test set Ordinal = " & i & " where Department_ID = " &
            '    dgvComponents.Rows(i).Cells(0).Value & " and Test_ID = " & dgvComponents.Rows(i).Cells(2).Value &
            '    " Else Insert into Department_Test (Department_ID, Test_ID, Ordinal) values (" &
            '    dgvComponents.Rows(i).Cells(0).Value & ", " & dgvComponents.Rows(i).Cells(2).Value & ", " & i & ")")
            'Next
            '

            UpdateComponents(dgvComponents.Rows(0).Cells(0).Value)
            CompsDirty = False
        End If
        '
        dgvComponents.Rows.Clear()
        '
        Dim sSQL As String = "Select b.ID as CompID, b.Name as CompName from Tests b Left outer join " &
        "Department_Test a on a.Test_ID = b.ID where b.Department_ID = " & DeptID & " order by a.Ordinal"
        '
        Dim cnro As New SqlClient.SqlConnection(connString)
        cnro.Open()
        Dim cmdro As New SqlClient.SqlCommand(sSQL, cnro)
        cmdro.CommandType = CommandType.Text
        Dim drro As SqlClient.SqlDataReader = cmdro.ExecuteReader
        If drro.HasRows Then
            While drro.Read
                dgvComponents.Rows.Add(DeptID, drro("CompName"), drro("CompID"))
            End While
        End If
        cnro.Close()
        cnro = Nothing
        '
        'sSQL = "Select ID as CompID, Name as CompName from Tests where Department_ID = " & DeptID & _
        '" and Not ID in (Select Test_ID from Department_Test where Department_ID = " & DeptID & ")"
        'Dim cnro1 As New SqlClient.SqlConnection(connstring)
        'cnro1.Open()
        'Dim cmdro1 As New SqlClient.SqlCommand(sSQL, cnro1)
        'cmdro1.CommandType = CommandType.Text
        'Dim drro1 As SqlClient.SqlDataReader = cmdro1.ExecuteReader
        'If drro1.HasRows Then
        '    While drro1.Read
        '        dgvComponents.Rows.Add(DeptID, drro1("CompName"), drro1("CompID"))
        '    End While
        'End If
        'cnro1.Close()
        'cnro1 = Nothing
    End Sub

    Private Sub SaveSetting()
        Dim DeptID As String = ""
        If dgvDepartments.SelectedRows(0).Index <> -1 Then
            DeptID = dgvDepartments.SelectedRows(0).Cells(1).Value.ToString
        End If
        For i As Integer = 0 To dgvDepartments.RowCount - 1
            ExecuteSqlProcedure("Update Departments set Ordinal = " & i & _
            " where ID = " & dgvDepartments.Rows(i).Cells(1).Value)
        Next
        '
        If CompsDirty = True AndAlso DeptID <> "" Then
            'ExecuteSqlProcedure($"delete from Department_Test where Department_ID = {DeptID}")
            'For i As Integer = 0 To dgvComponents.RowCount - 1
            '    SaveComponentSetting(DeptID, i)
            'Next
            UpdateComponents(DeptID)
            CompsDirty = False
        End If
    End Sub

    Private Sub UpdateComponents(ByVal DeptID As Integer)
        ExecuteSqlProcedure($"delete from Department_Test where Department_ID = {DeptID}")
        For i As Integer = 0 To dgvComponents.RowCount - 1
            'SaveComponentSetting(DeptID, i)
            'Delete existing test IDs 
            ExecuteSqlProcedure($"delete from Department_Test where Test_ID = {dgvComponents.Rows(i).Cells(2).Value}")

            ExecuteSqlProcedure($"Insert into Department_Test (Department_ID, Test_ID, Ordinal) values (
               {dgvComponents.Rows(i).Cells(0).Value}, {dgvComponents.Rows(i).Cells(2).Value}, {i} )")


            'ExecuteSqlProcedure("If Exists (Select * from Department_Test where Department_ID = " &
            '   dgvComponents.Rows(i).Cells(0).Value & " and Test_ID = " & dgvComponents.Rows(i).Cells(2).Value &
            '   ") Update Department_Test set Ordinal = " & i & " where Department_ID = " &
            '   dgvComponents.Rows(i).Cells(0).Value & " and Test_ID = " & dgvComponents.Rows(i).Cells(2).Value &
            '   " Else Insert into Department_Test (Department_ID, Test_ID, Ordinal) values (" &
            '   dgvComponents.Rows(i).Cells(0).Value & ", " & dgvComponents.Rows(i).Cells(2).Value & ", " & i & ")")
        Next
    End Sub

    Private Sub SaveComponentSetting(ByVal DeptID As Integer, ByVal RowID As Integer)
        ExecuteSqlProcedure("If Exists (Select * from Department_Test where Department_ID = " & DeptID & " and Test_ID = " & _
        dgvComponents.Rows(RowID).Cells(2).Value & ") Update Department_Test set Ordinal = " & RowID & " where Department_ID = " & _
        DeptID & " and Test_ID = " & dgvComponents.Rows(RowID).Cells(2).Value & " Else Insert into Department_Test (Department_ID, " & _
        "Test_ID, Ordinal) values (" & DeptID & ", " & dgvComponents.Rows(RowID).Cells(2).Value & ", " & RowID & ")")
    End Sub

    Private Sub btnFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirst.Click
        If dgvComponents.SelectedRows.Count > 0 Then
            Dim SelRow As Integer = dgvComponents.SelectedRows(0).Index
            Row_Move(dgvComponents, SelRow, 0)
            dgvComponents.Rows(SelRow).Selected = False
            dgvComponents.Rows(0).Selected = True
            dgvComponents.FirstDisplayedScrollingRowIndex = 0
            CompsDirty = True
        End If
    End Sub

    Private Sub Row_Move(ByRef dgv As DataGridView, ByVal Selrow As Integer, ByVal RowID As Integer)
        Dim RowS As DataGridViewRow = dgv.Rows(Selrow)
        dgv.Rows.RemoveAt(Selrow)
        dgv.Rows.Insert(RowID, RowS)
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        If dgvComponents.SelectedRows.Count > 0 Then
            Dim SelRow As Integer = dgvComponents.SelectedRows(0).Index
            If SelRow > 0 Then
                Row_Move(dgvComponents, SelRow, SelRow - 1)
                dgvComponents.Rows(SelRow).Selected = False
                dgvComponents.Rows(SelRow - 1).Selected = True
                dgvComponents.FirstDisplayedScrollingRowIndex = SelRow - 1
                CompsDirty = True
            End If
        End If
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        If dgvComponents.SelectedRows.Count > 0 Then
            Dim SelRow As Integer = dgvComponents.SelectedRows(0).Index
            If SelRow < dgvComponents.Rows.Count - 1 Then
                Row_Move(dgvComponents, SelRow, SelRow + 1)
                dgvComponents.Rows(SelRow).Selected = False
                dgvComponents.Rows(SelRow + 1).Selected = True
                dgvComponents.FirstDisplayedScrollingRowIndex = SelRow + 1
                CompsDirty = True
            End If
        End If
    End Sub

    Private Sub btnLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLast.Click
        If dgvComponents.SelectedRows.Count > 0 Then
            Dim SelRow As Integer = dgvComponents.SelectedRows(0).Index
            If SelRow < dgvComponents.Rows.Count - 1 Then
                Row_Move(dgvComponents, SelRow, dgvComponents.Rows.Count - 1)
                dgvComponents.Rows(SelRow).Selected = False
                dgvComponents.Rows(dgvComponents.Rows.Count - 1).Selected = True
                dgvComponents.FirstDisplayedScrollingRowIndex = dgvComponents.Rows.Count - 1
                CompsDirty = True
            End If
        End If
    End Sub

    Private Sub btnInsertBlank_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim SelRow As Integer = dgvComponents.SelectedRows(0).Index
        Row_Move(dgvComponents, SelRow, SelRow)
        dgvComponents.Rows(SelRow).Selected = True
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim SelRow As Integer = dgvComponents.SelectedRows(0).Index
        dgvComponents.Rows.RemoveAt(SelRow)
    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        Try
            If Trim(txtValue.Text) <> "" Then
                If cmbParameter.SelectedIndex = 0 Then  'Name
                    For i As Integer = 0 To dgvComponents.RowCount - 1
                        If dgvComponents.Rows(i).Cells(1).Value.ToString.ToUpper.StartsWith(Trim(txtValue.Text).ToUpper) Then
                            dgvComponents.Rows(i).Selected = True
                            dgvComponents.FirstDisplayedScrollingRowIndex = i
                            Exit For
                        End If
                    Next
                Else    'ID
                    For i As Integer = 0 To dgvComponents.RowCount - 1
                        If dgvComponents.Rows(i).Cells(2).Value = Trim(txtValue.Text) Then
                            dgvComponents.Rows(i).Selected = True
                            dgvComponents.FirstDisplayedScrollingRowIndex = i
                            Exit For
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            MsgBox("An error '" & ex.Message & "' occured because of your unthoughtful " & _
            "selection." & vbCrLf & "Focus Dude!", MsgBoxStyle.Critical, "Prolis")
        End Try
    End Sub

    Private Sub btnDFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDFirst.Click
        If dgvDepartments.SelectedRows.Count > 0 Then
            Dim SelRow As Integer = dgvDepartments.SelectedRows(0).Index
            Row_Move(dgvDepartments, SelRow, 0)
            dgvDepartments.Rows(SelRow).Selected = False
            dgvDepartments.Rows(0).Selected = True
            dgvDepartments.FirstDisplayedScrollingRowIndex = 0
        End If
    End Sub

    Private Sub btnDPrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDPrev.Click
        If dgvDepartments.SelectedRows.Count > 0 Then
            Dim SelRow As Integer = dgvDepartments.SelectedRows(0).Index
            If SelRow > 0 Then
                Row_Move(dgvDepartments, SelRow, SelRow - 1)
                dgvDepartments.Rows(SelRow).Selected = False
                dgvDepartments.Rows(SelRow - 1).Selected = True
            End If
        End If
    End Sub

    Private Sub btnDNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDNext.Click
        If dgvDepartments.SelectedRows.Count > 0 Then
            Dim SelRow As Integer = dgvDepartments.SelectedRows(0).Index
            If SelRow < dgvDepartments.Rows.Count - 1 Then
                Row_Move(dgvDepartments, SelRow, SelRow + 1)
                dgvDepartments.Rows(SelRow).Selected = False
                dgvDepartments.Rows(SelRow + 1).Selected = True
            End If
        End If
    End Sub

    Private Sub btnDLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDLast.Click
        If dgvDepartments.SelectedRows.Count > 0 Then
            Dim SelRow As Integer = dgvDepartments.SelectedRows(0).Index
            If SelRow < dgvDepartments.Rows.Count - 1 Then
                Row_Move(dgvDepartments, SelRow, dgvDepartments.Rows.Count - 1)
                dgvDepartments.Rows(SelRow).Selected = False
                dgvDepartments.Rows(dgvDepartments.Rows.Count - 1).Selected = True
                dgvDepartments.FirstDisplayedScrollingRowIndex = dgvDepartments.Rows.Count - 1
            End If
        End If
    End Sub

    Private Sub dgvDepartments_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDepartments.CellDoubleClick
        LoadComponents(dgvDepartments.Rows(e.RowIndex).Cells(1).Value)
    End Sub
End Class
