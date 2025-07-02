Imports System.Windows.Forms
Imports System.data

Public Class frmLabMgmt
    Private originalDataSource As DataGridView
    Private Sub frmLabMgmt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpFrom.Value = Date.Today
        dtpTo.Value = DateAdd(DateInterval.Day, 365, dtpFrom.Value)
        dgvContract.Rows.Add()
        txtPhone.Mask = SystemConfig.PhoneMask
        txtFax.Mask = SystemConfig.PhoneMask
        txtLabels.Text = "1"
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub FormClear()
        chkActive.Checked = False
        txtLabID.Text = "" : txtLabName.Text = "" : txtAccount.Text = "" : txtWebsite.Text = ""
        txtAdd1.Text = "" : txtAdd2.Text = "" : txtCity.Text = "" : txtState.Text = ""
        txtZip.Text = "" : txtCountry.Text = "" : txtContact.Text = "" : txtEmail.Text = ""
        txtUserName.Text = "" : txtPassword.Text = "" : txtPhone.Text = "" : txtFax.Text = ""
        txtNote.Text = "" : txtDocFile.Text = "" : txtLabelFile.Text = ""
        txtCLIA.Text = "" : txtNPI.Text = "" : txtSSN.Text = "" : txtDirector.Text = ""
        dtpFrom.Value = Date.Today : dtpTo.Value = DateAdd(DateInterval.Day, 365, dtpFrom.Value)
        dgvContract.Rows.Clear() : dgvContract.Rows.Add() : txtLabels.Text = "1"
        btnSave.Enabled = False : btnDelete.Enabled = False
        btnMapping.Enabled = False
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim RetVal As Integer
        If txtLabID.Text <> "" And txtLabName.Text <> "" And _
        txtAdd1.Text <> "" And txtCity.Text <> "" And txtState.Text <> "" _
        And txtZip.Text <> "" Then
            If Not IsLabUsed(Val(txtLabID.Text)) Then
                RetVal = MsgBox("Are you sure about deleting this Laboratory?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "MD PERFECT")
                If RetVal = vbYes Then
                    ExecuteSqlProcedure("Delete from Labs where ID = " & Val(txtLabID.Text))
                    ExecuteSqlProcedure("Delete from Lab_TGP where Lab_ID = " & Val(txtLabID.Text))
                    FormClear()
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                End If
            Else
                MsgBox("This laboratory has a references in Sendout records. In such a case system" _
                & " does not allow to delete the laboratory. If you must delete this laboratory, remove" _
                & " all the references of this laboratory in Sendouts and try again.", MsgBoxStyle.Information)
            End If
        Else
            MsgBox("You must display the laboratory first then delete it", MsgBoxStyle.Information)
        End If
    End Sub

    Private Function IsLabUsed(ByVal LabID As Integer) As Boolean
        Dim Used As Boolean = False
        Dim cnlu As New SqlClient.SqlConnection(connString)
        cnlu.Open()
        Dim cmdlu As New SqlClient.SqlCommand("Select " &
        "* from SendOuts where Lab_ID = " & LabID, cnlu)
        cmdlu.CommandType = CommandType.Text
        Dim drlu As SqlClient.SqlDataReader = cmdlu.ExecuteReader
        If drlu.HasRows Then Used = True
        cnlu.Close()
        cnlu = Nothing
        Return Used
    End Function

    Private Sub chkEditNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEditNew.Click
        FormClear()
        If chkEditNew.Checked = False Then
            chkEditNew.Text = "Edit"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Edit.ico")
            btnLabLook.Enabled = True
        Else
            chkEditNew.Text = "New"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\New.ico")
            txtLabID.Text = NextLabID()
            btnLabLook.Enabled = False
        End If
    End Sub

    Private Function NextLabID() As Integer
        Dim LabID As Integer = 1
        Dim cnlu As New SqlClient.SqlConnection(connString)
        cnlu.Open()
        Dim cmdlu As New SqlClient.SqlCommand(
        "Select max(ID) as LastID from Labs", cnlu)
        cmdlu.CommandType = CommandType.Text
        Dim drlu As SqlClient.SqlDataReader = cmdlu.ExecuteReader
        If drlu.HasRows Then
            While drlu.Read
                If drlu("LastID") IsNot DBNull.Value _
                Then LabID = drlu("LastID") + 1
            End While
        End If
        cnlu.Close()
        cnlu = Nothing
        Return LabID
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        search.Text = ""
        If txtLabID.Text <> "" And txtLabName.Text <> "" And txtAdd1.Text <> "" And
        txtCity.Text <> "" And txtState.Text <> "" And txtZip.Text <> "" Then
            SaveLaboratory(Val(txtLabID.Text))
            FormClear()
            If chkEditNew.Checked = True Then
                txtLabID.Text = NextLabID()
            End If
            btnSave.Enabled = False
            btnDelete.Enabled = False
        Else
            MsgBox("All required fields (Red labeled) must be filled to save the record.")
            If txtLabName.Text = "" Then
                txtLabName.Focus()
            ElseIf txtAdd1.Text = "" Then
                txtAdd1.Focus()
            ElseIf txtCity.Text = "" Then
                txtCity.Focus()
            ElseIf txtState.Text = "" Then
                txtState.Focus()
            ElseIf txtZip.Text = "" Then
                txtZip.Focus()
            End If
        End If
    End Sub

    Private Sub SaveLabTGPs(ByVal LabID As Integer)
        Dim rws As DataGridView = CompareAndSaveRows()
        Dim dd = rws.Rows.Count()
        For i As Integer = 0 To rws.RowCount - 1
            For J As Integer = 0 To rws.Rows(i).Cells.Count - 1
                Dim cc = rws.Rows(i).Cells(J).Value
            Next
        Next
        If dgvContract.Rows.Count() < originalDataSource.Rows.Count() Then
            search.Text = ""
        End If
        ExecuteSqlProcedure("Delete from Lab_TGP where Lab_ID = " & LabID)
        For i As Integer = 0 To dgvContract.RowCount - 1
            If dgvContract.Rows(i).Cells(1).Value IsNot Nothing AndAlso
            (dgvContract.Rows(i).Cells(3).Value IsNot Nothing AndAlso
            dgvContract.Rows(i).Cells(3).Value <> "") AndAlso
            ((dgvContract.Rows(i).Cells(5).Value IsNot Nothing AndAlso
            Trim(dgvContract.Rows(i).Cells(5).Value) <> "") Or
            (dgvContract.Rows(i).Cells(6).Value IsNot Nothing AndAlso
            Trim(dgvContract.Rows(i).Cells(6).Value) <> "")) Then
                ExecuteSqlProcedure("Insert into Lab_TGP (Lab_ID, TGP_ID, LabComponentID, LabResultID, " &
                "Price, Ordinal) values (" & LabID & ", " & dgvContract.Rows(i).Cells(1).Value & ", '" &
                dgvContract.Rows(i).Cells(5).Value & "', '" & dgvContract.Rows(i).Cells(6).Value & "', " &
                CDbl(dgvContract.Rows(i).Cells(7).Value) & ", " & i & ")")
            End If
        Next
    End Sub

    Private Sub SaveLaboratory(ByVal LabID As Integer)
        Dim FLName As String = ""
        Dim TargetPath As String = Application.StartupPath & "\Reports\"
        Dim cnlab As New SqlClient.SqlConnection(connString)
        cnlab.Open()
        Dim cmdupsert As New SqlClient.SqlCommand("Labs_SP", cnlab)
        cmdupsert.CommandType = CommandType.StoredProcedure
        cmdupsert.Parameters.AddWithValue("@act", "Upsert")
        cmdupsert.Parameters.AddWithValue("@ID", txtLabID.Text)
        cmdupsert.Parameters.AddWithValue("@FacilityType_ID", 4)
        cmdupsert.Parameters.AddWithValue("@LabName", Trim(txtLabName.Text))
        cmdupsert.Parameters.AddWithValue("@Active", chkActive.Checked)
        cmdupsert.Parameters.AddWithValue("@Account", Trim(txtAccount.Text))
        cmdupsert.Parameters.AddWithValue("@ComDLL", Trim(txtCommDLL.Text))
        cmdupsert.Parameters.AddWithValue("@CLIA", Trim(txtCLIA.Text))
        cmdupsert.Parameters.AddWithValue("@NPI", Trim(txtNPI.Text))
        cmdupsert.Parameters.AddWithValue("@SSN", Trim(txtSSN.Text))
        cmdupsert.Parameters.AddWithValue("@IsPrimary", chkPrimary.Checked)
        cmdupsert.Parameters.AddWithValue("@Address_ID", GetAddressID(txtAdd1.Text,
        txtAdd2.Text, txtCity.Text, txtState.Text, txtZip.Text, txtCountry.Text))
        cmdupsert.Parameters.AddWithValue("@Contact", Trim(txtContact.Text))
        cmdupsert.Parameters.AddWithValue("@Phone", PhoneNeat(txtPhone.Text))
        cmdupsert.Parameters.AddWithValue("@Fax", PhoneNeat(txtFax.Text))
        cmdupsert.Parameters.AddWithValue("@UserName", Trim(txtUserName.Text))
        cmdupsert.Parameters.AddWithValue("@Password", Trim(txtPassword.Text))
        cmdupsert.Parameters.AddWithValue("@Website", Trim(txtWebsite.Text))
        cmdupsert.Parameters.AddWithValue("@Email", Trim(txtEmail.Text))
        cmdupsert.Parameters.AddWithValue("@Note", Trim(txtNote.Text))
        If Trim(txtDocFile.Text) <> "" Then
            FLName = Microsoft.VisualBasic.Mid(Trim(txtDocFile.Text),
            InStrRev(Trim(txtDocFile.Text), "\") + 1)
            cmdupsert.Parameters.AddWithValue("@DocFile", Trim(txtDocFile.Text))
        End If
        If Trim(txtLabelFile.Text) <> "" Then
            FLName = Microsoft.VisualBasic.Mid(Trim(txtLabelFile.Text),
            InStrRev(Trim(txtLabelFile.Text), "\") + 1)
            cmdupsert.Parameters.AddWithValue("@LabelFile", Trim(txtLabelFile.Text))
        End If
        cmdupsert.Parameters.AddWithValue("@DirectorName", Trim(txtDirector.Text))
        cmdupsert.Parameters.AddWithValue("@Labels", Val(txtLabels.Text))
        cmdupsert.Parameters.AddWithValue("@ContractFrom", dtpFrom.Value)
        cmdupsert.Parameters.AddWithValue("@ContractTo", dtpTo.Value)
        cmdupsert.Parameters.AddWithValue("@LastEditedOn", Date.Now)
        cmdupsert.Parameters.AddWithValue("@EditedBy", ThisUser.ID)
        Try
            cmdupsert.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cnlab.Close()
            cnlab = Nothing
        End Try
        '
        SaveLabTGPs(LabID)
    End Sub

    Private Function CompareAndSaveRows() As DataGridView

        Dim dgvNewRows As New DataGridView



        ' Loop through each row in dgvContract
        For Each contractRow As DataGridViewRow In dgvContract.Rows

            Dim rowExists As Boolean = False

            For Each originalRow As DataGridViewRow In originalDataSource.Rows
                Dim id1 = originalRow.Cells(1).Value
                Dim id = contractRow.Cells(1).Value
                If id = 131 Then
                    id1 = originalRow.Cells(1).Value
                End If
                Dim name1 = originalRow.Cells(3).Value
                Dim name = contractRow.Cells(3).Value

                Dim odrID1 = originalRow.Cells(5).Value
                Dim OrdID = contractRow.Cells(5).Value

                Dim ResID1 = originalRow.Cells(6).Value
                Dim ResID = contractRow.Cells(6).Value
                If id1 = id Then
                    rowExists = True
                    Exit For
                End If
            Next

            ' If the row does not exist in originalDataSource, add it to dgvNewRows
            If Not rowExists Then
                Dim newRow As DataGridViewRow = CType(contractRow.Clone(), DataGridViewRow)
                For Each cell As DataGridViewCell In contractRow.Cells
                    newRow.Cells(cell.ColumnIndex).Value = cell.Value
                Next
                dgvNewRows.Rows.Add(newRow)
            End If
        Next

        Return dgvNewRows
    End Function


    Private Sub Update_Progress()
        If txtLabID.Text <> "" And txtLabName.Text <> "" And txtAdd1.Text <> "" And
        txtCity.Text <> "" And txtState.Text <> "" And txtZip.Text <> "" Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub DisplayLab(ByVal LabID As Integer)
        FormClear()
        Dim cndl As New SqlClient.SqlConnection(connString)
        cndl.Open()
        Dim cmddl As New SqlClient.SqlCommand("Select " &
        "* from Labs where ID = " & LabID, cndl)
        cmddl.CommandType = CommandType.Text
        Dim drdl As SqlClient.SqlDataReader = cmddl.ExecuteReader
        If drdl.HasRows Then
            While drdl.Read
                txtLabID.Text = drdl("ID")
                txtLabName.Text = drdl("LabName")
                chkActive.Checked = drdl("Active")
                chkPrimary.Checked = drdl("IsPrimary")
                If drdl("Account") IsNot DBNull.Value Then txtAccount.Text = drdl("Account")
                txtAdd1.Text = GetAddress1(drdl("address_ID"))
                txtAdd2.Text = GetAddress2(drdl("address_ID"))
                txtCity.Text = GetAddressCity(drdl("Address_ID"))
                txtState.Text = GetAddressState(drdl("Address_ID"))
                txtZip.Text = GetAddressZip(drdl("Address_ID"))
                txtCountry.Text = GetAddressCountry(drdl("Address_ID"))
                If drdl("DocFile") IsNot DBNull.Value Then txtDocFile.Text = drdl("DocFile")
                If drdl("LabelFile") IsNot DBNull.Value Then txtLabelFile.Text = drdl("LabelFile")
                If drdl("ComDLL") IsNot DBNull.Value Then txtCommDLL.Text = drdl("ComDLL")
                If drdl("Contact") IsNot DBNull.Value Then txtContact.Text = drdl("Contact")
                If drdl("Email") IsNot DBNull.Value Then txtEmail.Text = drdl("Email")
                If drdl("UserName") IsNot DBNull.Value Then txtUserName.Text = drdl("UserName")
                If drdl("Password") IsNot DBNull.Value Then txtPassword.Text = drdl("Password")
                If drdl("Phone") IsNot DBNull.Value Then txtPhone.Text = drdl("Phone")
                If drdl("Fax") IsNot DBNull.Value Then txtFax.Text = drdl("Fax")
                If drdl("Website") IsNot DBNull.Value Then txtWebsite.Text = drdl("Website")
                If drdl("CLIA") IsNot DBNull.Value Then txtCLIA.Text = drdl("CLIA")
                If drdl("NPI") IsNot DBNull.Value Then txtNPI.Text = drdl("NPI")
                If drdl("SSN") IsNot DBNull.Value Then txtSSN.Text = drdl("SSN")
                If drdl("Note") IsNot DBNull.Value Then txtNote.Text = drdl("Note")
                If drdl("DocFile") IsNot DBNull.Value Then txtDocFile.Text = drdl("DocFile")
                If drdl("LabelFile") IsNot DBNull.Value Then txtLabelFile.Text = drdl("LabelFile")
                If drdl("DirectorName") IsNot DBNull.Value Then txtDirector.Text = drdl("DirectorName")
                txtLabels.Text = drdl("Labels").ToString
                If drdl("ContractFrom") IsNot DBNull.Value Then _
                dtpFrom.Value = drdl("ContractFrom")
                If drdl("ContractTo") IsNot DBNull.Value Then _
                dtpTo.Value = drdl("ContractTo")
                btnDelete.Enabled = True
                btnMapping.Enabled = True
            End While
        End If
        cndl.Close()
        cndl = Nothing
        DisplayLabTGPs(LabID)
    End Sub

    Private Sub DisplayLabTGPs(ByVal LabID As Integer)
        dgvContract.Rows.Clear()
        'dgvContract.RowCount = 1
        Dim CompType As String = ""
        Dim OrdID As String = ""
        Dim ResID As String = ""
        Dim Price As String = ""
        Dim cnl1 As New SqlClient.SqlConnection(connString)
        cnl1.Open()
        Dim cmdl1 As New SqlClient.SqlCommand("Select a.TGP_ID, (Select Name from Tests where " &
        "ID = a.TGP_ID Union Select Name from Groups where ID = a.TGP_ID Union Select Name from " &
        "Profiles where ID = a.TGP_ID) as Component, (Select ComponentType from Tests where " &
        "ID = a.TGP_ID Union Select ComponentType from Groups where ID = a.TGP_ID Union Select " &
        "ComponentType from Profiles where ID = a.TGP_ID) as ComponentType, a.LabComponentID as " &
        "OrdID, a.LabResultID as ResID, a.Price from Lab_TGP a where a.Lab_ID = " & LabID &
        " Order by a.Ordinal", cnl1)
        cmdl1.CommandType = CommandType.Text
        Dim drl1 As SqlClient.SqlDataReader = cmdl1.ExecuteReader
        If drl1.HasRows Then
            While drl1.Read
                dgvContract.Rows.Add(System.Drawing.Image.FromFile(Application.StartupPath &
                "\Images\Eraser.ico"), drl1("TGP_ID"), Nothing, drl1("Component"), Nothing,
                drl1("OrdID"), drl1("ResID"), drl1("Price"))
                '
                If drl1("ComponentType") IsNot DBNull.Value AndAlso drl1("ComponentType") = "T" Then
                    dgvContract.Rows(dgvContract.RowCount - 1).Cells(4).Value =
                    System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath _
                    & "\Images\test.ico")
                ElseIf drl1("ComponentType") IsNot DBNull.Value AndAlso drl1("ComponentType") = "G" Then
                    dgvContract.Rows(dgvContract.RowCount - 1).Cells(4).Value =
                    System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath _
                    & "\Images\group.ico")
                Else
                    dgvContract.Rows(dgvContract.RowCount - 1).Cells(4).Value =
                    System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath _
                    & "\Images\profile.ico")
                End If
            End While
        End If
        cnl1.Close()
        cnl1 = Nothing
        dgvContract.Rows.Add()
        ' Clone the DataGridView
        originalDataSource = New DataGridView()
        For Each column As DataGridViewColumn In dgvContract.Columns
            originalDataSource.Columns.Add(CType(column.Clone(), DataGridViewColumn))
        Next
        For Each row As DataGridViewRow In dgvContract.Rows
            Dim newRow As DataGridViewRow = CType(row.Clone(), DataGridViewRow)
            For Each cell As DataGridViewCell In row.Cells
                newRow.Cells(cell.ColumnIndex).Value = cell.Value
            Next
            originalDataSource.Rows.Add(newRow)
        Next
    End Sub

    Private Sub txtLabID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLabID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtLabID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLabID.Validated
        If txtLabID.Text <> "" Then
            Dim LabID As Integer = Val(txtLabID.Text)
            If chkEditNew.Checked = False Then 'Edit
                If IsLabIDUnique(LabID) = True Then
                    MsgBox("Invalid lab ID", MsgBoxStyle.Critical, "Prolis")
                    LabID = -1
                    txtLabID.Text = ""
                    txtLabID.Focus()
                Else
                    FormClear()
                    DisplayLab(LabID)
                End If
            Else    'Add mode
                If IsLabIDUnique(LabID) = False Then
                    MsgBox("The provider ID has been used already.", MsgBoxStyle.Critical, "Prolis")
                    LabID = -1
                    txtLabID.Text = ""
                    txtLabID.Focus()
                Else
                    DisplayLab(LabID)
                End If
            End If
        End If
        Update_Progress()
    End Sub

    Private Function IsLabIDUnique(ByVal LabID As Integer) As Boolean
        Dim IsIt As Boolean = True
        Dim cnl As New SqlClient.SqlConnection(connString)
        cnl.Open()
        Dim cmdl As New SqlClient.SqlCommand(
        "Select * from Labs where ID = " & LabID, cnl)
        cmdl.CommandType = CommandType.Text
        Dim drl As SqlClient.SqlDataReader = cmdl.ExecuteReader
        If drl.HasRows Then IsIt = False
        cnl.Close()
        cnl = Nothing
        Return IsIt
    End Function

    Private Sub btnLabLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLabLook.Click
        Dim LabID As String = frmLabLook.ShowDialog()
        If LabID <> "" Then DisplayLab(LabID)
    End Sub

    Private Sub chkActive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkActive.CheckedChanged
        If chkActive.Checked = True Then
            chkActive.Text = "Yes"
        Else
            chkActive.Text = "No"
            If chkPrimary.Checked = True Then chkPrimary.Checked = False
        End If
    End Sub

    Private Sub chkPrimary_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPrimary.CheckedChanged
        If chkPrimary.Checked = True Then
            chkPrimary.Text = "Yes"
            ExecuteSqlProcedure("Update Labs set IsPrimary = 0 where IsPrimary <> 0")
        Else
            chkPrimary.Text = "No"
            If txtLabID.Text <> "" And txtLabName.Text <> "" Then
                ExecuteSqlProcedure("Update Labs set IsPrimary = 1 where ID in (Select Top 1 ID from Labs " &
                "where Active <> 0 and ID <> " & Val(txtLabID.Text) & ")")
            End If
        End If
    End Sub

    Private Sub dgvContract_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvContract.CellClick
        If e.ColumnIndex = 0 Then   'Eraser
            If dgvContract.Rows(e.RowIndex).Cells(3).Value <> "" Then
                Dim RetVal As Integer = MsgBox("This action deletes the record, independent of " &
                "the Reference Lab record. Are you sure you want to delete this component?",
                MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
                If RetVal = vbYes Then
                    ExecuteSqlProcedure("Delete from Lab_TGP where Lab_ID = " & Val(txtLabID.Text) _
                    & " and TGP_ID = " & dgvContract.Rows(e.RowIndex).Cells(1).Value)
                    If e.RowIndex = dgvContract.RowCount - 1 Then 'Last
                        ClearLine(e.RowIndex)
                    Else
                        dgvContract.Rows.RemoveAt(e.RowIndex)
                    End If
                End If
            End If
        ElseIf e.ColumnIndex = 2 Then
            frmTGPLookup.ShowDialog()
            If frmTGPLookup.Tag.ToString <> "" Then
                Dim cndc As New SqlClient.SqlConnection(connString)
                cndc.Open()
                Dim cmddc As New SqlClient.SqlCommand("Select ID, Name, ComponentType, ListPrice " &
                "from Tests where ID = " & frmTGPLookup.Tag & " Union Select ID, Name, ComponentType, " &
                "ListPrice from Groups where ID = " & frmTGPLookup.Tag & " Union Select ID, Name, " &
                "ComponentType, ListPrice from Profiles where ID = " & frmTGPLookup.Tag, cndc)
                cmddc.CommandType = CommandType.Text
                Dim drdc As SqlClient.SqlDataReader = cmddc.ExecuteReader
                If drdc.HasRows Then
                    While drdc.Read
                        If drdc("ID") IsNot DBNull.Value Then
                            dgvContract.Rows(e.RowIndex).Cells(0).Value =
                            System.Drawing.Image.FromFile(Application.StartupPath &
                            "\Images\Eraser.ico")
                            dgvContract.Rows(e.RowIndex).Cells(1).Value = drdc("ID")
                            dgvContract.Rows(e.RowIndex).Cells(3).Value = drdc("Name")
                            If drdc("ComponentType") = "T" Then
                                dgvContract.Rows(e.RowIndex).Cells(4).Value =
                                System.Drawing.Image.FromFile(Application.StartupPath &
                                "\Images\test.ico")
                            ElseIf drdc("ComponentType") = "G" Then
                                dgvContract.Rows(e.RowIndex).Cells(4).Value =
                                System.Drawing.Image.FromFile(Application.StartupPath &
                                "\Images\group.ico")
                            Else
                                dgvContract.Rows(e.RowIndex).Cells(4).Value =
                                System.Drawing.Image.FromFile(Application.StartupPath &
                                "\Images\profile.ico")
                            End If
                            dgvContract.Rows(e.RowIndex).Cells(5).Value = ""
                            dgvContract.Rows(e.RowIndex).Cells(7).Value =
                            Format(drdc("ListPrice"), "##,##0.00")
                            dgvContract.Rows.Insert(e.RowIndex + 1, 1)
                            'CellSetFocus(dgvContract, e.RowIndex, e.ColumnIndex)
                            Exit While
                        End If
                    End While
                End If
                cndc.Close()
                cndc = Nothing
            End If
        End If
    End Sub

    Private Sub dgvContract_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvContract.CellEndEdit
        If e.ColumnIndex = 1 Then   'ID
            If IsNumeric(dgvContract.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) = True Then
                Dim cndc As New SqlClient.SqlConnection(connString)
                cndc.Open()
                Dim cmddc As New SqlClient.SqlCommand("Select ID, Name, ComponentType, ListPrice " & _
                "from Tests where ID = " & dgvContract.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & _
                " Union Select ID, Name, ComponentType, ListPrice from Groups where ID = " & _
                dgvContract.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & " Union Select ID, Name, " & _
                "ComponentType, ListPrice from Profiles where ID = " & _
                dgvContract.Rows(e.RowIndex).Cells(e.ColumnIndex).Value, cndc)
                cmddc.CommandType = CommandType.Text
                Dim drdc As SqlClient.SqlDataReader = cmddc.ExecuteReader
                If drdc.HasRows Then
                    While drdc.Read
                        If drdc("ID") IsNot DBNull.Value Then
                            dgvContract.Rows(e.RowIndex).Cells(0).Value = _
                            System.Drawing.Image.FromFile(Application.StartupPath & _
                            "\Images\Eraser.ico")
                            dgvContract.Rows(e.RowIndex).Cells(1).Value = drdc("ID")
                            dgvContract.Rows(e.RowIndex).Cells(3).Value = drdc("Name")
                            If drdc("ComponentType") = "T" Then
                                dgvContract.Rows(e.RowIndex).Cells(4).Value = _
                                System.Drawing.Image.FromFile(Application.StartupPath & _
                                "\Images\test.ico")
                            ElseIf drdc("ComponentType") = "G" Then
                                dgvContract.Rows(e.RowIndex).Cells(4).Value = _
                                System.Drawing.Image.FromFile(Application.StartupPath & _
                                "\Images\group.ico")
                            Else
                                dgvContract.Rows(e.RowIndex).Cells(4).Value = _
                                System.Drawing.Image.FromFile(Application.StartupPath & _
                                "\Images\profile.ico")
                            End If
                            dgvContract.Rows(e.RowIndex).Cells(5).Value = ""
                            dgvContract.Rows(e.RowIndex).Cells(7).Value = _
                            Format(drdc("ListPrice"), "##,##0.00")
                            dgvContract.Rows.Insert(e.RowIndex + 1, 1)
                            'CellSetFocus(dgvContract, e.RowIndex, e.ColumnIndex)
                            Exit While
                        End If
                    End While
                    If dgvContract.Rows(e.RowIndex).Cells(3).Value = "" Then
                        MsgBox("Type a valid component ID", MsgBoxStyle.Critical, "Prolis")
                        ClearLine(e.RowIndex)
                    End If
                End If
                cndc.Close()
                cndc = Nothing
            Else
                MsgBox("Type a valid component ID", MsgBoxStyle.Critical, "Prolis")
                ClearLine(e.RowIndex)
            End If
        ElseIf e.ColumnIndex = 6 Then   'ResID
            If (dgvContract.Rows(e.RowIndex).Cells(3).Value IsNot Nothing _
            AndAlso dgvContract.Rows(e.RowIndex).Cells(3).Value <> "") _
            AndAlso (dgvContract.Rows(e.RowIndex).Cells(5).Value = "" _
            AndAlso dgvContract.Rows(e.RowIndex).Cells(6).Value = "") Then
                MsgBox("Entered component will not be saved without either the " & _
                "'Lab Ord ID' or the 'Lab Res ID'", MsgBoxStyle.Critical, "Prolis")
            End If
        End If
    End Sub

    Private Sub CellSetFocus(ByRef dgv As DataGridView, _
    ByVal RowIndex As Integer, ByVal ColumnIndex As Integer)
        Dim SelectedRow As DataGridViewRow = dgv.Rows(RowIndex)
        SelectedRow.Selected = True
        SelectedRow.Cells(ColumnIndex).Selected = True
    End Sub

    Private Sub ClearLine(ByVal RowIndex As Integer)
        dgvContract.Rows(RowIndex).Cells(0).Value = _
        System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Blank.ico")
        dgvContract.Rows(RowIndex).Cells(1).Value = ""
        dgvContract.Rows(RowIndex).Cells(3).Value = ""
        dgvContract.Rows(RowIndex).Cells(4).Value = _
        System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Blank.ico")
        dgvContract.Rows(RowIndex).Cells(5).Value = ""
        dgvContract.Rows(RowIndex).Cells(6).Value = ""
        dgvContract.Rows(RowIndex).Cells(7).Value = ""
    End Sub

    Private Sub btnDocFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDocFile.Click
        Dim DLG As New OpenFileDialog
        DLG.InitialDirectory = My.Application.Info.DirectoryPath & "\Reports\"
        DLG.Filter = "Crystal Report Files (*.RPT)|*.rpt|All files (*.*)|*.*"
        '"txt files (*.txt)|*.txt|All files (*.*)|*.*"
        If MsgBoxResult.Ok = DLG.ShowDialog Then
            Dim TargetPath As String = My.Application.Info.DirectoryPath & "\Reports\"
            Dim RPTFILE As String = Microsoft.VisualBasic.Mid(DLG.FileName, _
            InStrRev(DLG.FileName, "\") + 1)
            If TargetPath <> DLG.InitialDirectory Then _
            IO.File.Copy(DLG.FileName, TargetPath & RPTFILE, True)
            txtDocFile.Text = RPTFILE
        Else
            txtDocFile.Text = ""
        End If
        DLG.Dispose()
        DLG = Nothing
    End Sub

    Private Sub btnLabelFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLabelFile.Click
        Dim DLG As New OpenFileDialog
        DLG.InitialDirectory = My.Application.Info.DirectoryPath & "\Reports\"
        DLG.Filter = "Crystal Report Files (*.RPT)|*.rpt|All files (*.*)|*.*"
        If MsgBoxResult.Ok = DLG.ShowDialog Then
            Dim TargetPath As String = My.Application.Info.DirectoryPath & "\Reports\"
            Dim RPTFILE As String = Microsoft.VisualBasic.Mid(DLG.FileName, _
            InStrRev(DLG.FileName, "\") + 1)
            If TargetPath <> DLG.InitialDirectory Then _
            IO.File.Copy(DLG.FileName, TargetPath & RPTFILE, True)
            txtLabelFile.Text = RPTFILE
        Else
            txtLabelFile.Text = ""
        End If
        DLG.Dispose()
        DLG = Nothing
    End Sub

    Private Sub txtLabName_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLabName.Validated
        Update_Progress()
    End Sub

    Private Sub txtAdd1_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAdd1.Validated
        Update_Progress()
    End Sub

    Private Sub txtCity_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCity.Validated
        Update_Progress()
    End Sub

    Private Sub txtState_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtState.Validated
        Update_Progress()
    End Sub

    Private Sub txtZip_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtZip.Validated
        Update_Progress()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub txtLabels_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLabels.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtLabels_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLabels.Validated
        If Val(txtLabels.Text) < 0 Or Val(txtLabels.Text) > 99 Then txtLabels.Text = "1"
    End Sub

    Private Sub btnMapping_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMapping.Click
        If txtLabID.Text <> "" Then
            frmLabMapping.ShowDialog()
            DisplayLabTGPs(Val(txtLabID.Text))
        End If
    End Sub

    Private Sub dgvContract_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvContract.CellValidated
        If e.ColumnIndex = 6 Then   'ResID
            If (dgvContract.Rows(e.RowIndex).Cells(3).Value IsNot Nothing _
            AndAlso dgvContract.Rows(e.RowIndex).Cells(3).Value <> "") _
            AndAlso (dgvContract.Rows(e.RowIndex).Cells(5).Value = "" _
            AndAlso dgvContract.Rows(e.RowIndex).Cells(6).Value = "") Then
                MsgBox("Entered component will not be saved without either the " & _
                "'Lab Ord ID' or the 'Lab Res ID'", MsgBoxStyle.Critical, "Prolis")
            End If
        End If
    End Sub

    Private Sub search_Click(sender As Object, e As EventArgs) Handles search.Click
        If search.Text = "Search here ...." Then
            search.Text = ""
        End If

    End Sub

    Private Sub search_MouseLeave(sender As Object, e As EventArgs) Handles search.MouseLeave
        If search.Text = "" Then
            search.Text = "Search here ...."
        End If
    End Sub

    Private Sub search_TextChanged(sender As Object, e As EventArgs) Handles search.TextChanged
        Dim searchTerm As String = search.Text.Trim()
        If originalDataSource Is Nothing Then
            Return

        End If


        If String.IsNullOrEmpty(searchTerm) Or searchTerm.Contains("Search here") Then
            ' If search term is empty, show all rows from the original data source
            dgvContract.Rows.Clear()
            For Each row As DataGridViewRow In originalDataSource.Rows
                Dim newRow As DataGridViewRow = CType(row.Clone(), DataGridViewRow)
                For Each cell As DataGridViewCell In row.Cells
                    newRow.Cells(cell.ColumnIndex).Value = cell.Value
                Next
                dgvContract.Rows.Add(newRow)
            Next
        Else
            ' If search term is not empty, filter the original DataGridView
            Dim filteredRows As New List(Of DataGridViewRow)

            For Each row As DataGridViewRow In originalDataSource.Rows
                For Each cell As DataGridViewCell In row.Cells
                    If cell.Value IsNot Nothing AndAlso cell.Value.ToString().IndexOf(searchTerm, StringComparison.CurrentCultureIgnoreCase) >= 0 Then
                        Dim newRow As DataGridViewRow = CType(row.Clone(), DataGridViewRow)
                        For Each cellInRow As DataGridViewCell In row.Cells
                            newRow.Cells(cellInRow.ColumnIndex).Value = cellInRow.Value
                        Next
                        filteredRows.Add(newRow)
                        Exit For
                    End If
                Next
            Next

            ' Clear existing rows and add filtered rows
            dgvContract.Rows.Clear()
            dgvContract.Rows.AddRange(filteredRows.ToArray())
        End If
    End Sub

    Private Sub dgvContract_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvContract.CellValueChanged
        Try
            Dim v = dgvContract.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            Dim cIndex = e.ColumnIndex
            Dim id = dgvContract.Rows(e.RowIndex).Cells(1).Value
            Dim idw = dgvContract.Rows(e.RowIndex).Cells(6).Value
            Dim trg As Integer = 0
            For i As Integer = 0 To originalDataSource.RowCount - 1
                If originalDataSource.Rows(i).Cells(1).Value = id Then
                    trg = i
                End If

            Next
            originalDataSource.Rows(trg).Cells(e.ColumnIndex).Value = v
        Catch ex As Exception

        End Try
      
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        search.Text = ""
        txtLabID.Focus()
        SendKeys.Send("{TAB}")
        search.Text = "Search here ...."
    End Sub
End Class
