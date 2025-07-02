Imports System.Windows.Forms
Imports System.data

Public Class frmWorkSetUp

    Private Sub frmWorkSetUp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub btnTGLookup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTGPLookup.Click
        Dim TGID As String = frmTGLookup.ShowDialog()
        If TGID <> "" Then
            Dim ItemX As MyList = GetTGItem(Val(TGID))
            If ItemX.ItemData <> -1 Then
                txtCompID.Text = TGID
                txtCompName.Text = ItemX.Name
                btnAdd.Enabled = True
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub chkEditNew_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEditNew.CheckedChanged
        ClearForm()
        If chkEditNew.Checked = False Then
            chkEditNew.Text = "Edit"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Edit.ico")
            btnWorkLook.Enabled = True
        Else
            chkEditNew.Text = "New"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\New.ico")
            btnWorkLook.Enabled = False
            txtWorkID.Text = GetNextWorkID()
        End If
    End Sub

    Private Sub ClearForm()
        txtWorkID.Text = ""
        txtControls.Text = ""
        txtName.Text = ""
        btnSave.Enabled = False
        btnDelete.Enabled = False
        dgvTGs.Rows.Clear()
    End Sub

    Private Function GetNextWorkID() As Integer
        Dim WorkID As Integer = 1
        Dim cnw As New SqlClient.SqlConnection(connString)
        cnw.Open()
        Dim cmdw As New SqlClient.SqlCommand("Select max(ID) as LastID from Worksheets", cnw)
        cmdw.CommandType = CommandType.Text
        Dim drw As SqlClient.SqlDataReader = cmdw.ExecuteReader
        If drw.HasRows Then
            While drw.Read
                If drw("LastID") IsNot DBNull.Value _
                Then WorkID = drw("LastID") + 1
            End While
        End If
        cnw.Close()
        cnw = Nothing
        Return WorkID
    End Function

    Private Sub txtWorkID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWorkID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtCompID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCompID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Function GetTGItem(ByVal TGID As Integer) As MyList
        Dim ItemX As New MyList
        Dim cnw As New SqlClient.SqlConnection(connString)
        cnw.Open()
        Dim cmdw As New SqlClient.SqlCommand("Select ID, Name from Tests where " &
        "ID = " & TGID & " Union Select ID, Name from Groups where ID = " & TGID, cnw)
        cmdw.CommandType = CommandType.Text
        Dim drw As SqlClient.SqlDataReader = cmdw.ExecuteReader
        If drw.HasRows Then
            While drw.Read
                ItemX.ItemData = drw("ID")
                ItemX.Name = drw("Name")
            End While
        Else
            ItemX.ItemData = -1
            ItemX.Name = ""
        End If
        cnw.Close()
        cnw = Nothing
        Return ItemX
    End Function

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If txtCompID.Text <> "" And txtCompName.Text <> "" Then
            If Not TestInTheList(Val(txtCompID.Text)) Then
                dgvTGs.Rows.Add(Val(txtCompID.Text), False, txtCompName.Text)
                Dim TGPType As String = GetTGPType(Val(txtCompID.Text))
                If TGPType = "T" Then
                    dgvTGs.Rows(dgvTGs.RowCount - 1).Cells(1).ReadOnly = True
                Else
                    dgvTGs.Rows(dgvTGs.RowCount - 1).Cells(1).ReadOnly = False
                End If
                txtCompID.Text = ""
                txtCompName.Text = ""
                btnAdd.Enabled = False
                btnRemAll.Enabled = True
                txtCompID.Focus()
            End If
        Else
            MsgBox("You need to have an Component displayed to add to the list.")
            txtCompID.Text = ""
            txtCompName.Text = ""
            btnAdd.Enabled = False
        End If
        Update_Progress()
    End Sub

    Private Function TestInTheList(ByVal Test_ID As Integer) As Boolean
        Dim i As Integer
        Dim InList As Boolean = False
        For i = 0 To dgvTGs.RowCount - 1
            If dgvTGs.Rows(i).Cells(0).Value = Test_ID Then
                InList = True
                Exit For
            End If
        Next
        TestInTheList = InList
    End Function

    Private Sub Update_Progress()
        If txtWorkID.Text <> "" And txtName.Text <> "" And dgvTGs.RowCount > 0 Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub txtCompID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCompID.Validated
        If txtCompID.Text <> "" Then
            Dim ItemX As MyList = GetTGItem(Val(txtCompID.Text))
            If ItemX.ItemData <> -1 Then
                txtCompName.Text = ItemX.Name
                btnAdd.Enabled = True
            Else
                MsgBox("Invalid Comonent ID. Use Look Up instead")
                txtCompID.Text = ""
                txtCompID.Focus()
            End If
        Else
            txtCompName.Text = ""
        End If
    End Sub

    Private Sub txtWorkID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWorkID.Validated
        Dim RetVal As Integer
        If txtWorkID.Text <> "" Then
            If chkEditNew.Checked = True Then  'Add mode
                If IsWorkIDUnique(Val(txtWorkID.Text)) = False Then
                    RetVal = MsgBox("You need to provide an unused number or simply accept the system generated one." _
                    & " Do you want to provide the unique number?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo)
                    If RetVal = vbYes Then
                        txtWorkID.Text = ""
                        txtName.Text = "" : txtControls.Text = "" : dgvTGs.Rows.Clear()
                        txtWorkID.Focus()
                    Else
                        txtWorkID.Text = GetNextWorkID()
                    End If
                End If
            Else
                If False = IsWorkIDUnique(Val(txtWorkID.Text)) Then
                    DisplayWorksheet(Val(txtWorkID.Text))
                    btnSave.Enabled = True
                    btnDelete.Enabled = True
                Else
                    MsgBox("The Worksheet ID you typed, does not exist. You may use " _
                    & "the 'Lookup' function to select the worksheet")
                    txtWorkID.Text = ""
                    txtName.Text = "" : txtControls.Text = "" : dgvTGs.Rows.Clear()
                    txtWorkID.Focus()
                End If
            End If
        End If
        Update_Progress()
    End Sub

    Private Sub DisplayWorksheet(ByVal WorkID As Integer)
        Me.Cursor = Cursors.WaitCursor

        dgvTGs.Rows.Clear()
        Dim cnw As New SqlClient.SqlConnection(connString)
        cnw.Open()
        Dim cmdw As New SqlClient.SqlCommand("Select * from Worksheets where ID = " & WorkID, cnw)
        cmdw.CommandType = CommandType.Text
        Dim drw As SqlClient.SqlDataReader = cmdw.ExecuteReader
        If drw.HasRows Then
            While drw.Read
                txtWorkID.Text = drw("ID")
                txtName.Text = drw("Name")
                txtControls.Text = drw("DefaultControls")
            End While
        End If
        cnw.Close()
        cnw = Nothing
        '
        Dim cn1 As New SqlClient.SqlConnection(connString)
        cn1.Open()
        Dim cmd1 As New SqlClient.SqlCommand("Select * from Worksheet_TG " &
        "where Worksheet_ID = " & WorkID & " order by ordinal", cn1)
        cmd1.CommandType = CommandType.Text
        Dim dr1 As SqlClient.SqlDataReader = cmd1.ExecuteReader
        If dr1.HasRows Then
            While dr1.Read
                dgvTGs.Rows.Add(dr1("TG_ID"), dr1("Drill"), GetTGPName(dr1("TG_ID")))
                If "T" = GetTGPType(dr1("TG_ID")) Then
                    dgvTGs.Rows(dgvTGs.RowCount - 1).Cells(1).ReadOnly = True
                Else
                    dgvTGs.Rows(dgvTGs.RowCount - 1).Cells(1).ReadOnly = False
                End If
            End While
        End If
        cn1.Close()
        cn1 = Nothing

        Me.Cursor = Cursors.Default
    End Sub

    Private Function IsWorkIDUnique(ByVal WorkID As Integer) As Boolean
        Dim Unique As Boolean = True
        Dim cnw As New SqlClient.SqlConnection(connString)
        cnw.Open()
        Dim cmdw As New SqlClient.SqlCommand("Select * from Worksheets where ID = " & WorkID, cnw)
        cmdw.CommandType = CommandType.Text
        Dim drw As SqlClient.SqlDataReader = cmdw.ExecuteReader
        If drw.HasRows Then Unique = False
        cnw.Close()
        cnw = Nothing
        Return Unique
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtWorkID.Text <> "" And txtName.Text <> "" And dgvTGs.RowCount > 0 Then
            SaveWorksheet()
            ClearForm()
            If chkEditNew.Checked = True Then txtWorkID.Text = GetNextWorkID()
        Else
            MsgBox("Required elements to save a worksheet, are ID, Name and at least one component", MsgBoxStyle.Information, "PROLIS")
        End If
    End Sub

    Private Sub SaveWorksheet()
        ExecuteSqlProcedure("If Exists (Select * from Worksheets where ID = " &
        Val(txtWorkID.Text) & ") Update Worksheets set Name = '" & Trim(txtName.Text) _
        & "', DefaultControls = " & Val(txtControls.Text) & " where ID = " &
        Val(txtWorkID.Text) & " Else Insert into Worksheets (ID, Name, " &
        "DefaultControls) values (" & Val(txtWorkID.Text) & ", '" & Trim(txtName.Text) _
        & "', " & Val(txtControls.Text) & ")")
        '
        ExecuteSqlProcedure("Delete from Worksheet_TG where Worksheet_ID = " & Val(txtWorkID.Text))
        For i As Integer = 0 To dgvTGs.RowCount - 1
            ExecuteSqlProcedure("Insert into Worksheet_TG (Worksheet_ID, Drill, TG_ID, Ordinal) " &
            "values (" & Val(txtWorkID.Text) & ", " & Convert.ToInt16(dgvTGs.Rows(i).Cells(1).Value) &
            ", " & dgvTGs.Rows(i).Cells(0).Value & ", " & i & ")")
        Next
        '
        ExecuteSqlProcedure("Delete from Worksheet_Test where Worksheet_ID = " & Val(txtWorkID.Text))
        Dim Ordinal As Integer = 0
        For i As Integer = 0 To dgvTGs.RowCount - 1
            If dgvTGs.Rows(i).Cells(1).Value = 0 Then   'Test
                ExecuteSqlProcedure("Insert into Worksheet_Test (Worksheet_ID, Test_ID, Ordinal) " &
                "values (" & Val(txtWorkID.Text) & ", " & dgvTGs.Rows(i).Cells(0).Value & ", " &
                Ordinal & ")")
                Ordinal += 1
            Else
                Dim cnw As New SqlClient.SqlConnection(connString)
                cnw.Open()
                Dim cmdw As New SqlClient.SqlCommand("Select Test_ID from " & _
                "Group_Test where Group_ID = " & dgvTGs.Rows(i).Cells(0).Value, cnw)
                cmdw.CommandType = CommandType.Text
                Dim drw As SqlClient.SqlDataReader = cmdw.ExecuteReader
                If drw.HasRows Then
                    While drw.Read
                        ExecuteSqlProcedure("Insert into Worksheet_Test (Worksheet_ID, Test_ID, Ordinal) " & _
                        "values (" & Val(txtWorkID.Text) & ", " & drw("Test_ID") & ", " & Ordinal & ")")
                        Ordinal += 1
                    End While
                End If
                cnw.Close()
                cnw = Nothing
            End If
        Next
    End Sub

    Private Sub txtControls_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtControls.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtControls_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtControls.Validated
        If txtControls.Text <> "" Then
            If Val(txtControls.Text) < 0 Or Val(txtControls.Text) > 24 Then
                MsgBox(" Controls must be between 0 and 24")
                txtControls.Text = "0"
            End If
        End If
    End Sub

    Private Sub dgvTGs_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTGs.CellClick
        If e.RowIndex <> -1 Then btnRem.Enabled = True
    End Sub

    Private Sub btnRem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRem.Click
        dgvTGs.Rows.Remove(dgvTGs.SelectedRows(0))
        btnRem.Enabled = False
        If dgvTGs.RowCount = 0 Then btnRemAll.Enabled = False
    End Sub

    Private Sub btnRemAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemAll.Click
        dgvTGs.Rows.Clear()
        btnRemAll.Enabled = False
        btnRem.Enabled = False
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If txtWorkID.Text <> "" And txtName.Text <> "" And dgvTGs.RowCount > 0 Then
            Dim RetVal As Integer = MsgBox("Are you certain to delete this record", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "PROLIS")
            If RetVal = vbYes Then
                ExecuteSqlProcedure("Delete from Worksheets where ID = " & Val(txtWorkID.Text))
                ExecuteSqlProcedure("Delete from Worksheet_TG where Worksheet_ID = " & Val(txtWorkID.Text))
                ExecuteSqlProcedure("Delete from Worksheet_Test where Worksheet_ID = " & Val(txtWorkID.Text))
                ClearForm()
            End If
        End If
    End Sub

    Private Sub btnWorkLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWorkLook.Click
        Dim WorksheetID As String = frmWorkLookUp.ShowDialog()
        If WorksheetID <> "" Then DisplayWorksheet(Val(WorksheetID))
    End Sub
End Class
