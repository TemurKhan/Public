Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmDepts
    Private Sub frmDepts_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CN.Open("DSN=ProlisQC;UID=sa;PWD=''")
        Populate_Depts()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub Populate_Depts()
        dgvDepts.Rows.Clear()
        Dim cndep As New SqlConnection(connString)
        cndep.Open()
        Dim cmddep As New SqlCommand("Select " &
        "* from Departments order by Dept_Name", cndep)
        cmddep.CommandType = CommandType.Text
        Dim drdep As SqlDataReader = cmddep.ExecuteReader
        If drdep.HasRows Then
            While drdep.Read
                dgvDepts.Rows.Add(drdep("ID"), drdep("Dept_Name"),
                IIf(drdep("Note") Is DBNull.Value, "", drdep("Note")))
            End While
        End If
        cndep.Close()
        cndep = Nothing
    End Sub

    Private Sub chkEditNew_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEditNew.CheckedChanged
        If chkEditNew.Checked = False Then
            chkEditNew.Text = "Edit"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Edit.ico")
            dgvDepts.Enabled = True
            txtDeptID.Text = ""
        Else
            chkEditNew.Text = "New"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\New.ico")
            dgvDepts.Enabled = False
            txtDeptID.Text = NextDeptID()
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub dgvDepts_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDepts.CellDoubleClick
        If e.RowIndex <> -1 Then
            txtDeptID.Text = ""
            txtDeptName.Text = ""
            txtDeptID.Text = dgvDepts.Rows(e.RowIndex).Cells(0).Value
            txtDeptName.Text = dgvDepts.Rows(e.RowIndex).Cells(1).Value
            txtNote.Text = dgvDepts.Rows(e.RowIndex).Cells(2).Value
            DisplayAnalytes(dgvDepts.Rows(e.RowIndex).Cells(0).Value)
        End If
    End Sub

    Private Sub DisplayAnalytes(ByVal DeptID As Integer)
        dgvTests.Rows.Clear()
        Dim cnt As New SqlConnection(connString)
        cnt.Open()
        Dim cmdt As New SqlCommand("Select * " &
        "from Tests where Department_ID = " & DeptID, cnt)
        cmdt.CommandType = CommandType.Text
        Dim drt As SqlDataReader = cmdt.ExecuteReader
        If drt.HasRows Then
            While drt.Read
                dgvTests.Rows.Add(drt("ID"), System.Drawing.Image.FromFile(Application.StartupPath _
                & "\Images\Test.ico"), drt("Name"))
            End While
            btnRemTstAll.Enabled = True
        End If
        cnt.Close()
        cnt = Nothing
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim RetVal As Integer
        If dgvDepts.SelectedRows(0).Index <> -1 Then
            If Dept_Used(dgvDepts.SelectedRows(0).Cells(0).Value) Then
                MsgBox("The department you are trying to delete, has been " _
                & "used in either analysis set up or contains tests. To avoid the record left " _
                & "orphan, PROLIS won't let you delete this record")
            Else
                RetVal = MsgBox("Generally you should not delete any department. " _
                & "Are you sure to delete this record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo)
                If RetVal = vbYes Then
                    ExecuteSqlProcedure("Delete from Departments where ID = " & dgvDepts.SelectedRows(0).Cells(0).Value)
                    ExecuteSqlProcedure("Update Tests Set Department_ID = 0 where Department_ID = " & Val(txtDeptID.Text))
                    txtDeptID.Text = ""
                    txtDeptName.Text = ""
                    txtNote.Text = ""
                    Populate_Depts()
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                End If
            End If
        End If
    End Sub

    Private Function Dept_Used(ByVal DeptID As Integer) As Boolean
        Dim Used As Boolean = False
        Dim cndu As New SqlConnection(connString)
        cndu.Open()
        Dim cmddu As New SqlCommand("Select ID " &
        "from Anas where Department_ID = " & DeptID & " Union " &
        "Select ID from Tests where Department_ID = " & DeptID, cndu)
        cmddu.CommandType = CommandType.Text
        Dim drdu As SqlDataReader = cmddu.ExecuteReader
        If drdu.HasRows Then Used = True
        cndu.Close()
        cndu = Nothing
        Return Used
    End Function

    Private Function NextDeptID() As Integer
        Dim NextID As Integer = 1
        Dim LastID As Integer = 0
        Dim cndi As New SqlConnection(connString)
        cndi.Open()
        Dim cmddi As New SqlCommand("Select max(ID) as " &
        "LastID from Tests union Select max(ID) as LastID from " &
        "Groups Union Select max(ID) as LastID from Profiles " &
        "Union Select max(ID) as LastID from Departments", cndi)
        cmddi.CommandType = CommandType.Text
        Dim drdi As SqlDataReader = cmddi.ExecuteReader
        If drdi.HasRows Then
            While drdi.Read
                If drdi("LastID") IsNot DBNull.Value Then
                    If drdi("LastID") > LastID Then LastID = drdi("LastID")
                End If
            End While
        End If
        cndi.Close()
        cndi = Nothing
        If LastID > NextID Then NextID = LastID + 1
        Return NextID
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtDeptID.Text <> "" And txtDeptName.Text <> "" Then
            ExecuteSqlProcedure("If Exists (Select * from Departments where ID = " &
            Val(txtDeptID.Text) & ") Update Departments set Dept_Name = '" &
            Trim(txtDeptName.Text) & "', Note = '" & Trim(txtNote.Text) & "' where " &
            "ID = " & Val(txtDeptID.Text) & " Else Insert into Departments (ID, " &
            "Dept_Name, Note) values (" & Val(txtDeptID.Text) & ", '" &
            Trim(txtDeptName.Text) & "', '" & Trim(txtNote.Text) & "')")
            '
            UpdateDeptAnalytes(Val(txtDeptID.Text))
            If chkEditNew.Checked = True Then
                txtDeptID.Text = NextDeptID()
            Else
                txtDeptID.Text = ""
            End If
            txtDeptName.Text = ""
            txtNote.Text = ""
            dgvTests.Rows.Clear()
            Populate_Depts()
        Else
            MsgBox("You need to enter the Department's name, to save it.")
            txtDeptName.Focus()
        End If
    End Sub

    Private Sub UpdateDeptAnalytes(ByVal DeptID As Integer)
        If dgvTests.RowCount > 0 Then
            Dim i As Integer
            Dim TestIDs As String = ""
            For i = 0 To dgvTests.Rows.Count - 1
                TestIDs += dgvTests.Rows(i).Cells(0).Value.ToString & ", "
            Next
            If Len(TestIDs) > 2 Then _
            TestIDs = Microsoft.VisualBasic.Mid(TestIDs, 1, Len(TestIDs) - 2)
            ExecuteSqlProcedure("Update Tests set Department_ID = " & DeptID &
            " where ID in (" & TestIDs & ")")
        End If
    End Sub

    Private Sub btnTestLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestLook.Click
        Dim TestID As String = frmTestLookup.ShowDialog()
        If TestID <> "" Then
            txtTestID.Text = TestID
            txtTestName.Text = GetTGPName(Val(TestID))
            btnAddTest.Enabled = True
            txtTestName.Focus()
        End If
    End Sub

    Private Sub txtTestID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTestID.Validated
        If txtTestID.Text <> "" Then
            Dim ItemX As MyList = GetTestItem(Val(txtTestID.Text))
            If ItemX.ItemData <> -1 Then
                txtTestName.Text = ItemX.Name
                btnAddTest.Enabled = True
            Else
                MsgBox("Invalid Analyte ID. Use Look Up instead")
                txtTestID.Text = ""
                txtTestID.Focus()
            End If
        Else
            txtTestName.Text = ""
        End If
    End Sub

    Private Function GetTestItem(ByVal TestID As Integer) As MyList
        Dim ItemX As New MyList
        Dim cndi As New SqlConnection(connString)
        cndi.Open()
        Dim cmddi As New SqlCommand("Select * from Tests where ID = " & TestID, cndi)
        cmddi.CommandType = CommandType.Text
        Dim drdi As SqlDataReader = cmddi.ExecuteReader
        If drdi.HasRows Then
            While drdi.Read
                ItemX.ItemData = drdi("ID")
                ItemX.Name = drdi("Name")
            End While
        Else
            ItemX.ItemData = -1
            ItemX.Name = ""
        End If
        cndi.Close()
        cndi = Nothing
        Return ItemX
    End Function

    Private Sub btnAddTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddTest.Click
        If txtTestID.Text <> "" And txtTestName.Text <> "" Then
            If Not TestInTheList(Val(txtTestID.Text)) Then
                dgvTests.Rows.Add(Val(txtTestID.Text), System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Test.ico"),
                txtTestName.Text)
                txtTestID.Text = ""
                txtTestName.Text = ""
                btnAddTest.Enabled = False
                btnRemTstAll.Enabled = True
            End If
        Else
            MsgBox("You need to have an Analyte displayed to add to the list.")
            txtTestID.Text = ""
            txtTestName.Text = ""
            btnAddTest.Enabled = False
        End If
        Update_Progress()
    End Sub

    Private Function TestInTheList(ByVal Test_ID As Integer) As Boolean
        Dim i As Integer
        Dim InList As Boolean = False
        For i = 0 To dgvTests.RowCount - 1
            If dgvTests.Rows(i).Cells(0).Value = Test_ID Then
                InList = True
                Exit For
            End If
        Next
        TestInTheList = InList
    End Function

    Private Sub Update_Progress()
        If txtDeptID.Text <> "" And txtDeptName.Text <> "" Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub btnRemTst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemTst.Click
        If dgvTests.SelectedRows(0).Index <> -1 Then
            dgvTests.Rows.RemoveAt(dgvTests.SelectedRows(0).Index)
            btnRemTst.Enabled = False
            If dgvTests.RowCount = 0 Then btnRemTstAll.Enabled = False
        End If
        Update_Progress()
    End Sub

    Private Sub btnRemTstAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemTstAll.Click
        dgvTests.Rows.Clear()
        btnRemTstAll.Enabled = False
        btnRemTst.Enabled = False
        Update_Progress()
    End Sub

    Private Sub dgvTests_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTests.CellClick
        If e.RowIndex <> -1 Then btnRemTst.Enabled = True
    End Sub

    Private Sub txtDeptID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDeptID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtDeptID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDeptID.Validated
        If txtDeptID.Text <> "" Then
            If chkEditNew.Checked = True Then   'Add
                If Not IsTGPUnique(Val(txtDeptID.Text)) Then
                    MsgBox("The Department ID is not unique. Use an unused ID.")
                End If
            Else    'Edit
                If IsDeptIDValid(Val(txtDeptID.Text)) = True Then
                    DisplayDepartment(Val(txtDeptID.Text))
                Else
                    MsgBox("Invalid ID", MsgBoxStyle.Critical, "Prolis")
                    txtDeptID.Text = ""
                    txtDeptID.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub DisplayDepartment(ByVal DeptID As Integer)
        txtDeptID.Text = ""
        txtDeptName.Text = ""
        txtNote.Text = ""
        dgvTests.Rows.Clear()
        Dim cndi As New SqlConnection(connString)
        cndi.Open()
        Dim cmddi As New SqlCommand("Select " &
        "* from Departments where ID = " & DeptID, cndi)
        cmddi.CommandType = CommandType.Text
        Dim drdi As SqlDataReader = cmddi.ExecuteReader
        If drdi.HasRows Then
            While drdi.Read
                txtDeptID.Text = drdi("ID")
                txtDeptName.Text = drdi("Dept_Name")
                If drdi("Note") IsNot DBNull.Value _
                Then txtNote.Text = drdi("Note")
            End While
        End If
        cndi.Close()
        cndi = Nothing
        DisplayAnalytes(DeptID)
    End Sub

    Private Function IsDeptIDValid(ByVal DeptID As Integer) As Boolean
        Dim DidValid As Boolean = False
        Dim cnv As New SqlConnection(connString)
        cnv.Open()
        Dim cmdv As New SqlCommand("Select " &
        "* from Departments where ID = " & DeptID, cnv)
        cmdv.CommandType = CommandType.Text
        Dim drv As SqlDataReader = cmdv.ExecuteReader
        If drv.HasRows Then DidValid = True
        cnv.Close()
        cnv = Nothing
        Return DidValid
    End Function

End Class
