Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.SqlClient
Imports Microsoft.Data.SqlClient

Public Class frmGroups
    Private SearchMode As Boolean = True

    Private Sub frmGrps_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'If CN.State = 0 Then CN.Open(connstring)
        dgvNecessity.RowCount = 6
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub chkEditNew_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEditNew.CheckedChanged
        ClearForm()
        If chkEditNew.Checked = False Then
            chkEditNew.Text = "Edit"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Edit.ico")
            btnGroupLook.Enabled = True
        Else
            chkEditNew.Text = "New"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\New.ico")
            btnGroupLook.Enabled = False
            txtSearchID.Text = GetNextGroupID()
            txtGroupID.Text = txtSearchID.Text
        End If
    End Sub

    Private Sub ClearForm()
        txtSearchID.Text = ""
        rsNotes = ""
        txtResultNote.Text = ""
        txtGroupID.Text = ""
        'txtGroupID.ReadOnly = False
        txtName.Text = ""
        txtAbbr.Text = ""
        txtLoinc.Text = ""
        txtDescription.Text = ""
        ChkActive.Checked = True
        ChkMarkable.Checked = True
        ChkTBillable.Checked = True
        chkPBillable.Checked = True
        chkCBillable.Checked = True
        ChkInHouse.Checked = True
        chkBreakOROrder.Checked = False
        btnSave.Enabled = False
        btnDelete.Enabled = False
        btnReplicate.Enabled = False
        dgvModifiers.Rows.Clear()
        ClearBilling()
        dgvTests.Rows.Clear()
    End Sub

    Private Function GetNextGroupID() As Long
        Dim LastID As Integer = 0
        Dim NID As Integer = 1
        Dim cng As New SqlConnection(connString)
        cng.Open()
        Dim cmdg As New SqlCommand("Select max(ID) as LastID " &
        "from Tests union Select max(ID) as LastID from Groups Union Select " _
        & "max(ID) as LastID from Profiles Union " _
        & "Select max(ID) as LastID from Departments", cng)
        cmdg.CommandType = CommandType.Text
        Dim drg As SqlDataReader = cmdg.ExecuteReader
        If drg.HasRows Then
            While drg.Read
                If drg("LastID") IsNot DBNull.Value Then
                    If drg("LastID") > LastID Then LastID = drg("LastID")
                End If
            End While
        End If
        cng.Close()
        cng = Nothing
        If LastID > NID Then NID = LastID + 1
        Return NID
    End Function

    Private Sub ClearBilling()
        txtCPTCode.Text = "" : txtCPTMCR.Text = "" : txtCPTMCD.Text = ""
        txtCPTSPC.Text = "" : txtBillUnit.Text = "1.0" : txtPOS.Text = "81"
        txtMod1.Text = "" : txtMod2.Text = "" : txtMod3.Text = "" : txtMod4.Text = ""
        txtListPrice.Text = "" : txtPrice1.Text = "" : txtPrice2.Text = ""
        txtPrice3.Text = "" : txtPrice4.Text = "" : txtPrice5.Text = ""
        txtPrice6.Text = "" : txtPrice7.Text = "" : txtPrice8.Text = ""
        txtPrice9.Text = "" : dgvNecessity.Rows.Clear()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtSearchID.Text = txtGroupID.Text Then
            If txtGroupID.Text <> "" And txtName.Text <> "" And txtAbbr.Text <> "" Then
                SaveGroup(Val(txtGroupID.Text))
                UpdateNecessity(Val(txtSearchID.Text)) 'for portal
                'SaveGroupMaterial(Val(txtGroupID.Text))
                ExecuteSqlProcedure("Delete from MarkingModifiers where Test_ID = " & Val(txtGroupID.Text))
                If ChkMarkable.Checked = True Then
                    If dgvModifiers.RowCount > 0 Then SaveModifiers(Val(txtGroupID.Text))
                End If
                '
                ' CN.Execute("Delete from Necessity where TGP_ID = " & Val(txtGroupID.Text))
                'If ChkTBillable.Checked = True Then
                '    If dgvNecessity.RowCount > 0 Then SaveNecessity(Val(txtGroupID.Text))
                'End If

                If Replication = True Then Return

                ClearForm()
                If chkEditNew.Checked = True Then   'New
                    txtSearchID.Text = GetNextGroupID()
                    txtGroupID.Text = txtSearchID.Text
                End If
                txtSearchID.Focus()
            Else
                MsgBox("Group name, Abbr and the material are required to save it.")
            End If
        Else
            MsgBox("Group ID is different than that of the displayed record. Correct it and try again.", MsgBoxStyle.Critical, "Prolis")
            txtSearchID.Text = txtGroupID.Text
            txtSearchID.Focus()
        End If
    End Sub

    Private Sub SaveGroupMaterial(ByVal GroupID As Integer)
        ExecuteSqlProcedure("Delete from Group_Material where Group_ID = " & GroupID)
        Dim cngm As New SqlConnection(connString)
        cngm.Open()
        Dim cmdgm As New SqlCommand("Select " &
        "distinct Material_ID from Tests where ID in (Select Test_ID " &
        "from Group_Test where Group_ID = " & GroupID & ")", cngm)
        cmdgm.CommandType = CommandType.Text
        Dim drgm As SqlDataReader = cmdgm.ExecuteReader
        If drgm.HasRows Then
            While drgm.Read
                If drgm("Material_ID") IsNot DBNull.Value Then
                    ExecuteSqlProcedure("Insert into Group_Material (Group_ID, " &
                    "Material_ID) values (" & GroupID & ", " & drgm("Material_ID") & ")")
                End If
            End While
        End If
        cngm.Close()
        cngm = Nothing
    End Sub

    Private Sub SaveGroup(ByVal GroupID As Integer)

        Dim cnsg As New SqlConnection(connString)
        cnsg.Open()
        Dim cmdupsert As New SqlCommand("Groups_SP", cnsg)
        cmdupsert.CommandType = CommandType.StoredProcedure
        cmdupsert.Parameters.AddWithValue("@act", "Upsert")
        cmdupsert.Parameters.AddWithValue("@ID", GroupID)
        cmdupsert.Parameters.AddWithValue("@Name", Trim(txtName.Text))
        cmdupsert.Parameters.AddWithValue("@Abbr", Trim(txtAbbr.Text))
        cmdupsert.Parameters.AddWithValue("@Description", Trim(txtDescription.Text))
        cmdupsert.Parameters.AddWithValue("@Loinc", Trim(txtLoinc.Text))
        cmdupsert.Parameters.AddWithValue("@ComponentType", "G")
        cmdupsert.Parameters.AddWithValue("@IsActive", ChkActive.Checked)
        cmdupsert.Parameters.AddWithValue("@BreakOROrder", chkBreakOROrder.Checked)
        cmdupsert.Parameters.AddWithValue("@CPT_Code", Trim(txtCPTCode.Text))
        cmdupsert.Parameters.AddWithValue("@CPT_MCR", "")
        cmdupsert.Parameters.AddWithValue("@CPT_MCD", "")
        cmdupsert.Parameters.AddWithValue("@CPT_SPC", "")
        cmdupsert.Parameters.AddWithValue("@Mod1", Trim(txtMod1.Text))
        cmdupsert.Parameters.AddWithValue("@Mod2", Trim(txtMod2.Text))
        cmdupsert.Parameters.AddWithValue("@Mod3", Trim(txtMod3.Text))
        cmdupsert.Parameters.AddWithValue("@Mod4", Trim(txtMod4.Text))
        cmdupsert.Parameters.AddWithValue("@Bill_Unit", Trim(txtBillUnit.Text))
        cmdupsert.Parameters.AddWithValue("@POS_Code", Trim(txtPOS.Text))
        cmdupsert.Parameters.AddWithValue("@PBillable", chkPBillable.Checked)
        cmdupsert.Parameters.AddWithValue("@CBillable", chkCBillable.Checked)
        cmdupsert.Parameters.AddWithValue("@TBillable", ChkTBillable.Checked)
        cmdupsert.Parameters.AddWithValue("@IsMarkable", ChkMarkable.Checked)
        cmdupsert.Parameters.AddWithValue("@InHouse", ChkInHouse.Checked)
        cmdupsert.Parameters.AddWithValue("@ListPrice", Val(txtListPrice.Text))
        cmdupsert.Parameters.AddWithValue("@Price1", Val(txtPrice1.Text))
        cmdupsert.Parameters.AddWithValue("@Price2", Val(txtPrice2.Text))
        cmdupsert.Parameters.AddWithValue("@Price3", Val(txtPrice3.Text))
        cmdupsert.Parameters.AddWithValue("@Price4", Val(txtPrice4.Text))
        cmdupsert.Parameters.AddWithValue("@Price5", Val(txtPrice5.Text))
        cmdupsert.Parameters.AddWithValue("@Price6", Val(txtPrice6.Text))
        cmdupsert.Parameters.AddWithValue("@Price7", Val(txtPrice7.Text))
        cmdupsert.Parameters.AddWithValue("@Price8", Val(txtPrice8.Text))
        cmdupsert.Parameters.AddWithValue("@Price9", Val(txtPrice9.Text))

        cmdupsert.Parameters.AddWithValue("@ResultNotes", txtResultNote.Text.Trim)

        cmdupsert.Parameters.AddWithValue("@LastEditedOn", Date.Now)
        cmdupsert.Parameters.AddWithValue("@EditedBy", ThisUser.ID)
        Try
            cmdupsert.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cnsg.Close()
            cnsg = Nothing
        End Try
        '
        If txtResultNote.Text <> "" Then
            ExecuteSqlProcedure("update groups set ResultNotes='" & txtResultNote.Text & "' where id = " & GroupID)
        End If
        ExecuteSqlProcedure("Delete from Group_Test where Group_ID = " & GroupID)
        For i As Integer = 0 To dgvTests.RowCount - 1
            If dgvTests.Rows(i).Cells(0).Value.ToString <> "" Then
                ExecuteSqlProcedure("Insert into Group_Test (Group_ID, Test_ID, Ordinal) " &
                "values (" & GroupID & ", " & dgvTests.Rows(i).Cells(0).Value & ", " & i & ")")
            End If
        Next
        '
        SaveGroupMaterial(GroupID)
    End Sub

    Private Sub SaveModifiers(ByVal GroupID As Integer)
        ExecuteSqlProcedure("Delete from MarkingModifiers where Test_ID = " & GroupID)
        For i As Integer = 0 To dgvModifiers.RowCount - 1
            ExecuteSqlProcedure("Insert into MarkingModifiers (Test_ID, Sex, AgeFrom, " &
            "AgeTo, LastEditedOn, EditedBy) values (" & GroupID & ", '" &
            dgvModifiers.Rows(i).Cells(2).Value.ToString.Substring(0, 1) & "', " &
            dgvModifiers.Rows(i).Cells(3).Value & ", " & dgvModifiers.Rows(i).Cells(4).Value &
            ", '" & Date.Now & "', " & ThisUser.ID & ")")
        Next
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If txtSearchID.Text = txtGroupID.Text Then
            If txtGroupID.Text <> "" And txtName.Text <> "" And txtAbbr.Text <> "" Then
                If Not (GroupUsed(Val(txtGroupID.Text))) Then
                    If ThisUser.Hard_Deletion = True Then
                        Dim Retval As Integer
                        Retval = MsgBox("It is recommended to make the Group Inactive instead of deleting " _
                        & "it from the system entirely. Are you firm to delete this group anyway?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo)
                        If Retval = vbYes Then
                            ExecuteSqlProcedure("Delete from MarkingModifiers where Test_ID = " & Val(txtGroupID.Text))
                            ExecuteSqlProcedure("Delete from Necessity where TGP_ID = " & Val(txtGroupID.Text))
                            ExecuteSqlProcedure("Delete from Group_Test where Group_ID = " & Val(txtGroupID.Text))
                            ExecuteSqlProcedure("Delete from Groups where ID = " & Val(txtGroupID.Text))
                            ClearForm()
                            SearchMode = True
                        End If
                    Else
                        MsgBox("You don't have permission to delete any component of the system", MsgBoxStyle.Critical)
                    End If
                Else
                    MsgBox("The Group you want to delete, is either a part of Profile(s) and/or " _
                    & "it has been used in processing the requisition(s). System " _
                    & "does not allow the deletion of such component. You may mark it as " _
                    & "'Inactive'", MsgBoxStyle.Critical)
                End If
            End If
        Else
            MsgBox("Group ID is different than that of the displayed record. Correct it and try again.", MsgBoxStyle.Critical, "Prolis")
            txtSearchID.Text = txtGroupID.Text
            txtSearchID.Focus()
        End If
    End Sub

    Private Function GroupUsed(ByVal GroupID As Integer) As Boolean
        Dim IsPart As Boolean = False
        Dim cnpp As New SqlConnection(connString)
        cnpp.Open()
        Dim cmdpp As New SqlCommand("Select Profile_ID " &
        "as component from Prof_GrpTst where GrpTst_ID = " & GroupID &
        " Union Select TGP_ID as component from Req_TGP where TGP_ID = " & GroupID, cnpp)
        cmdpp.CommandType = CommandType.Text
        Dim drpp As SqlDataReader = cmdpp.ExecuteReader
        If drpp.HasRows Then
            While drpp.Read
                If drpp("Component") IsNot DBNull.Value Then
                    IsPart = True
                    Exit While
                End If
            End While
        End If
        cnpp.Close()
        cnpp = Nothing
        Return IsPart
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Friend Sub DisplayTheGroup(ByVal GroupID As Integer)
        ClearForm()
        Dim cndg As New SqlConnection(connString)
        cndg.Open()
        Dim cmddg As New SqlCommand("Select " &
        "* from Groups where ID = " & GroupID, cndg)
        cmddg.CommandType = CommandType.Text
        Dim drdg As SqlDataReader = cmddg.ExecuteReader
        If drdg.HasRows Then
            While drdg.Read
                txtSearchID.Text = drdg("ID").ToString
                txtGroupID.Text = drdg("ID").ToString
                txtName.Text = drdg("Name")
                txtAbbr.Text = drdg("Abbr")
                If drdg("Loinc") IsNot DBNull.Value Then _
                txtLoinc.Text = drdg("Loinc")
                If drdg("Description") IsNot DBNull.Value Then _
                txtDescription.Text = drdg("Description")
                ChkActive.Checked = drdg("Isactive")
                chkBreakOROrder.Checked = drdg("BreakOROrder")
                ChkMarkable.Checked = drdg("IsMarkable")
                ChkTBillable.Checked = drdg("TBillable")
                chkCBillable.Checked = drdg("CBillable")
                chkPBillable.Checked = drdg("PBillable")
                ChkInHouse.Checked = drdg("InHouse")
                If drdg("CPT_Code") IsNot DBNull.Value Then txtCPTCode.Text = Trim(drdg("CPT_Code"))
                If drdg("CPT_MCR") IsNot DBNull.Value Then txtCPTMCR.Text = Trim(drdg("CPT_MCR"))
                If drdg("CPT_MCD") IsNot DBNull.Value Then txtCPTMCD.Text = Trim(drdg("CPT_MCD"))
                If drdg("CPT_SPC") IsNot DBNull.Value Then txtCPTSPC.Text = Trim(drdg("CPT_SPC"))
                If drdg("Bill_Unit") IsNot DBNull.Value Then
                    txtBillUnit.Text = Trim(drdg("Bill_Unit"))
                Else
                    txtBillUnit.Text = 1.0
                End If
                If drdg("Mod1") IsNot DBNull.Value Then txtMod1.Text = Trim(drdg("Mod1"))
                If drdg("Mod2") IsNot DBNull.Value Then txtMod2.Text = Trim(drdg("Mod2"))
                If drdg("Mod3") IsNot DBNull.Value Then txtMod3.Text = Trim(drdg("Mod3"))
                If drdg("Mod4") IsNot DBNull.Value Then txtMod4.Text = Trim(drdg("Mod4"))
                If drdg("POS_Code") IsNot DBNull.Value Then txtPOS.Text = Trim(drdg("POS_Code"))
                If drdg("ListPrice") IsNot DBNull.Value Then txtListPrice.Text = drdg("ListPrice")
                If drdg("Price1") IsNot DBNull.Value Then txtPrice1.Text = drdg("Price1")
                If drdg("Price2") IsNot DBNull.Value Then txtPrice2.Text = drdg("Price2")
                If drdg("Price3") IsNot DBNull.Value Then txtPrice3.Text = drdg("Price3")
                If drdg("Price4") IsNot DBNull.Value Then txtPrice4.Text = drdg("Price4")
                If drdg("Price5") IsNot DBNull.Value Then txtPrice5.Text = drdg("Price5")
                If drdg("Price6") IsNot DBNull.Value Then txtPrice6.Text = drdg("Price6")
                If drdg("Price7") IsNot DBNull.Value Then txtPrice7.Text = drdg("Price7")
                If drdg("Price8") IsNot DBNull.Value Then txtPrice8.Text = drdg("Price8")
                If drdg("Price9") IsNot DBNull.Value Then txtPrice9.Text = drdg("Price9")
                If drdg("ResultNotes") IsNot DBNull.Value AndAlso
               drdg("ResultNotes") <> "" Then
                    txtResultNote.Text = drdg("ResultNotes")
                    If txtResultNote.Text <> "" Then
                        btnNote.Image =
                    System.Drawing.Image.FromFile(Application.StartupPath _
                    & "\Images\Note.ico")
                        rsNotes = txtResultNote.Text
                    End If
                Else
                    txtResultNote.Text = ""
                    rsNotes = ""
                    btnNote.Image =
                    System.Drawing.Image.FromFile(Application.StartupPath _
                    & "\Images\Noteblank.ico")
                End If
            End While
        End If
        cndg.Close()
        cndg = Nothing
        '
        dgvTests.Rows.Clear()
        Dim cngt As New SqlConnection(connString)
        cngt.Open()
        Dim cmdgt As New SqlCommand("Select " &
        "* from Group_Test where Group_ID = " &
        Val(txtGroupID.Text) & " Order by Ordinal", cngt)
        cmdgt.CommandType = CommandType.Text
        Dim drgt As SqlDataReader = cmdgt.ExecuteReader
        If drgt.HasRows Then
            While drgt.Read
                dgvTests.Rows.Add(drgt("Test_ID"),
               System.Drawing.Image.FromFile(Application.StartupPath &
               "\Images\test.ico"), GetTGPName(drgt("Test_ID")))
            End While
        End If
        cngt.Close()
        cngt = Nothing
        '
        If ChkMarkable.Checked = True Then DisplayModifiers(Val(txtGroupID.Text))
        If ChkTBillable.Checked = True Then
            If TabControl3.SelectedTab.Name = "tpNecessity" Then
                DisplayNecessity(Trim(txtGroupID.Text))
            End If
        End If
    End Sub

    Private Sub DisplayNecessity(ByVal GroupID As String)
        dgvNecessity.Rows.Clear()
        If Trim(txtCPTCode.Text) <> "" Then
            Dim sSQL As String = "Select a.Dx_Code, b.Description from " &
            "MedicalNecessity a inner join DiagCodes b on a.Dx_Code = " &
            "b.Code where a.CPT_Code = '" & Trim(txtCPTCode.Text) & "'"
            '
            Dim cndn As New SqlConnection(connString)
            cndn.Open()
            Dim cmddn As New SqlCommand(sSQL, cndn)
            cmddn.CommandType = Data.CommandType.Text
            Dim drdn As SqlDataReader = cmddn.ExecuteReader
            If drdn.HasRows Then
                While drdn.Read
                    dgvNecessity.Rows.Add(GroupID, dgvNecessity.RowCount + 1,
                    drdn("Dx_Code"), drdn("Description"))
                End While
                btnRemAllNec.Enabled = True
            End If
            cndn.Close()
            cndn = Nothing
        End If
    End Sub

    Private Function ToGender(ByVal Sex As String) As String
        If Sex = "M" Then
            ToGender = "M - Male"
        ElseIf Sex = "F" Then
            ToGender = "F - Female"
        ElseIf Sex = "I" Then
            ToGender = "I - Inbetween"
        Else
            ToGender = "U - Unreported"
        End If
    End Function

    Private Function CodeName(ByVal Code As String) As String
        Dim CName As String = ""
        Dim cncn As New SqlConnection(connString)
        cncn.Open()
        Dim cmdcn As New SqlCommand("Select " &
        "Description from DiagCodes where Code = '" & Code & "'", cncn)
        cmdcn.CommandType = Data.CommandType.Text
        Dim drcn As SqlDataReader = cmdcn.ExecuteReader
        If drcn.HasRows Then
            While drcn.Read
                If drcn("Description") IsNot DBNull.Value _
                Then CName = Replace(drcn("Description"), Chr(34), "")
            End While
        End If
        cncn.Close()
        cncn = Nothing
        Return CName
    End Function

    Private Sub DisplayModifiers(ByVal GroupID As Integer)
        dgvModifiers.Rows.Clear()
        Dim cndm As New SqlConnection(connString)
        cndm.Open()
        Dim cmddm As New SqlCommand("Select * " &
        "from MarkingModifiers where Test_ID = " & GroupID, cndm)
        cmddm.CommandType = Data.CommandType.Text
        Dim drdm As SqlDataReader = cmddm.ExecuteReader
        If drdm.HasRows Then
            While drdm.Read
                dgvModifiers.Rows.Add(dgvModifiers.RowCount + 1,
                GroupID, ToGender(drdm("Sex")), drdm("AgeFrom"),
                drdm("AgeTo"))
            End While
        End If
        cndm.Close()
        cndm = Nothing
    End Sub

    Private Sub txtName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.GotFocus
        txtName.BackColor = FCOLOR
    End Sub

    Private Sub txtName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.LostFocus
        txtName.BackColor = NFCOLOR
    End Sub

    Private Sub txtName_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.Validated
        Update_Progress()
    End Sub

    Private Sub txtAbbr_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAbbr.GotFocus
        txtAbbr.BackColor = FCOLOR
    End Sub

    Private Sub txtAbbr_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAbbr.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtAbbr_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAbbr.LostFocus
        txtAbbr.BackColor = NFCOLOR
    End Sub
    Private Sub txtAbbr_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAbbr.Validated
        Update_Progress()
    End Sub

    Private Sub ChkActive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkActive.CheckedChanged
        If ChkActive.Checked = False Then
            ChkActive.Text = "NO"
        Else
            ChkActive.Text = "YES"
        End If
    End Sub

    Private Sub ChkMarkable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkMarkable.CheckedChanged
        If ChkMarkable.Checked = False Then
            ChkMarkable.Text = "NO"
            dgvModifiers.Rows.Clear()
            grpMarking.Enabled = False
        Else
            ChkMarkable.Text = "YES"
            grpMarking.Enabled = True
        End If
    End Sub

    Private Sub txtTestID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTestID.GotFocus
        txtTestID.BackColor = FCOLOR
    End Sub

    Private Sub txtTestID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTestID.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtTestID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTestID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtTestID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTestID.LostFocus
        txtTestID.BackColor = NFCOLOR
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
        Dim cnti As New SqlConnection(connString)
        cnti.Open()
        Dim cmdti As New SqlCommand(
        "Select * from Tests where ID = " & TestID, cnti)
        cmdti.CommandType = Data.CommandType.Text
        Dim drti As SqlDataReader = cmdti.ExecuteReader
        If drti.HasRows Then
            While drti.Read
                ItemX.ItemData = drti("ID")
                ItemX.Name = drti("Name")
            End While
        Else
            ItemX.ItemData = -1
            ItemX.Name = ""
        End If
        cnti.Close()
        cnti = Nothing
        Return ItemX
    End Function

    Private Sub btnAddTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddTest.Click
        If txtTestID.Text <> "" And txtTestName.Text <> "" Then
            If Not TestInTheList(Val(txtTestID.Text)) Then
                dgvTests.Rows.Add(Val(txtTestID.Text), System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Test.ico"),
                txtTestName.Text)
                txtTestID.Text = ""
                txtTestName.Text = ""
                txtTestID.Focus()
                btnAddTest.Enabled = False
                btnRemTstAll.Enabled = True
            Else
                MsgBox("Duplicate Entry not allowed!", MsgBoxStyle.Critical, "Prolis")
                txtTestID.Text = ""
                txtTestName.Text = ""
                txtTestID.Focus()
                btnAddTest.Enabled = False
                If dgvTests.RowCount > 0 Then btnRemTstAll.Enabled = True
            End If
        Else
            MsgBox("You need to have an Analyte displayed to add to the list.")
            txtTestID.Text = ""
            txtTestName.Text = ""
            txtTestID.Focus()
            btnAddTest.Enabled = False
            If dgvTests.RowCount > 0 Then btnRemTstAll.Enabled = True
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
        If txtGroupID.Text <> "" And txtName.Text <> "" And txtAbbr.Text <> "" _
        And dgvTests.RowCount > 0 And ((ChkTBillable.Checked = True And Trim(txtCPTCode.Text) <> "" _
        And txtListPrice.Text <> "") Or (ChkTBillable.Checked = False)) And ((chkCBillable.Checked = True _
        Or chkPBillable.Checked = True And txtListPrice.Text <> "") Or (chkCBillable.Checked = False And
        chkPBillable.Checked = False)) Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub btnRemTst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemTst.Click
        If dgvTests.SelectedRows(0).Index <> -1 Then
            dgvTests.Rows.RemoveAt(dgvTests.SelectedRows(0).Index)
            If dgvTests.RowCount = 0 Then btnRemTstAll.Enabled = False
            btnRemTst.Enabled = False
            txtTestID.Focus()
        End If
        Update_Progress()
    End Sub

    Private Sub btnRemTstAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemTstAll.Click
        dgvTests.Rows.Clear()
        btnRemTstAll.Enabled = False
        btnRemTst.Enabled = False
        Update_Progress()
        txtTestID.Focus()
    End Sub

    Private Sub btnAddLab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddLab.Click
        Update_Progress()
    End Sub

    Private Sub btnRemoveLabs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveLabs.Click
        Update_Progress()
    End Sub

    Private Sub btnRemoveLab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveLab.Click
        Update_Progress()
    End Sub

    Private Sub dgvNecessity_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvNecessity.CellEndEdit
        If dgvNecessity.Rows(e.RowIndex).Cells(e.ColumnIndex).Value <> "" Then
            dgvNecessity.RowCount = dgvNecessity.RowCount + 1
        Else
            If dgvNecessity.RowCount > 6 Then dgvNecessity.Rows.RemoveAt(dgvNecessity.RowCount - 2)
        End If
    End Sub

    Private Sub txtAgeFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgeFrom.GotFocus
        txtAgeFrom.BackColor = FCOLOR
    End Sub

    Private Sub txtAgeFrom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAgeFrom.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtAgeFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAgeFrom.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAgeFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgeFrom.LostFocus
        txtAgeFrom.BackColor = NFCOLOR
    End Sub

    Private Sub txtAgeFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgeFrom.Validated
        If cmbSex.SelectedIndex <> -1 And txtAgeFrom.Text <> "" And txtAgeTo.Text <> "" Then
            btnAddModifier.Enabled = True
        Else
            btnAddModifier.Enabled = False
        End If
    End Sub

    Private Sub txtAgeTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgeTo.GotFocus
        txtAgeTo.BackColor = FCOLOR
    End Sub

    Private Sub txtAgeTo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAgeTo.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtAgeTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAgeTo.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAgeTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgeTo.LostFocus
        txtAgeTo.BackColor = NFCOLOR
    End Sub

    Private Sub txtAgeTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgeTo.Validated
        If cmbSex.SelectedIndex <> -1 And txtAgeFrom.Text <> "" And txtAgeTo.Text <> "" Then
            btnAddModifier.Enabled = True
        Else
            btnAddModifier.Enabled = False
        End If
    End Sub

    Private Sub cmbSex_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSex.Click
        If cmbSex.SelectedIndex <> -1 And txtAgeFrom.Text <> "" And txtAgeTo.Text <> "" Then
            btnAddModifier.Enabled = True
        Else
            btnAddModifier.Enabled = False
        End If
    End Sub

    Private Sub btnAddModifier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddModifier.Click
        If txtGroupID.Text <> "" And cmbSex.SelectedIndex <> -1 And
        txtAgeFrom.Text <> "" And txtAgeTo.Text <> "" Then
            'btnAddModifier.Enabled = True
            If Not ModifierInList(Val(txtGroupID.Text), cmbSex.SelectedItem.ToString.Substring(0, 1),
            Val(txtAgeFrom.Text), Val(txtAgeTo.Text)) Then
                dgvModifiers.Rows.Add(Val(txtGroupID.Text), dgvModifiers.RowCount + 1,
                cmbSex.SelectedItem.ToString.Substring(0, 1), Val(txtAgeFrom.Text), Val(txtAgeTo.Text))
            End If
        End If
    End Sub

    Private Function ModifierInList(ByVal Test_ID As Integer, ByVal Sex As String,
    ByVal AgeFrom As Integer, ByVal AgeTo As Integer) As Boolean
        Dim InList As Boolean = False
        Dim i As Integer
        For i = 0 To dgvModifiers.RowCount - 1
            If dgvModifiers.Rows(i).Cells(0).Value = Val(txtGroupID.Text) And
            dgvModifiers.Rows(i).Cells(2).Value = cmbSex.SelectedItem.ToString.Substring(0, 1) And
            dgvModifiers.Rows(i).Cells(3).Value = Val(txtAgeFrom.Text) And
            dgvModifiers.Rows(i).Cells(4).Value = Val(txtAgeTo.Text) Then
                InList = True
                Exit For
            End If
        Next
        ModifierInList = InList
    End Function

    Private Sub btnTestLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestLook.Click
        Dim TestID As String = frmTestLookup.ShowDialog()
        If TestID <> "" Then
            txtTestID.Text = TestID
            txtTestName.Text = GetTGPName(Val(TestID))
            btnAddTest.Enabled = True
            txtTestName.Focus()
        End If
    End Sub

    Private Sub btnGroupLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGroupLook.Click
        Dim GroupID As String = frmGroupLookup.ShowDialog()
        If GroupID <> "" Then
            DisplayTheGroup(Val(GroupID))
            btnDelete.Enabled = True
            btnSave.Enabled = True
            btnReplicate.Enabled = True
        End If
    End Sub

    Private Sub dgvTests_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTests.CellClick
        If e.RowIndex <> -1 Then
            btnRemTst.Enabled = True
            Clipboard.SetText(dgvTests.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString())
        End If

    End Sub

    Private Sub dgvNecessity_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvNecessity.CellClick
        If e.RowIndex <> -1 Then btnRemNec.Enabled = True
    End Sub

    Private Sub btnRemNec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemNec.Click
        If dgvNecessity.SelectedRows.Count > 0 Then
            Dim RetVal As Integer = MsgBox("Are you sure you want to delete the selected " &
            "Code from Necessity?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
            If RetVal = vbYes Then
                ExecuteSqlProcedure("Delete from MedicalNecessity where CPT_Code = '" & Trim(txtCPTCode.Text) &
                "' and Dx_Code = '" & Trim(dgvNecessity.SelectedRows(0).Cells(2).Value.ToString) & "'")
                dgvNecessity.Rows.Remove(dgvNecessity.SelectedRows(0))
                btnRemNec.Enabled = False
                If dgvNecessity.RowCount = 0 Then btnRemAllNec.Enabled = False
            End If
            dgvNecessity.SelectedRows(0).Selected = False
        End If
    End Sub

    Private Sub btnRemAllNec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemAllNec.Click
        If dgvNecessity.SelectedRows.Count > 0 Then
            Dim RetVal As Integer = MsgBox("Are you sure you want to delete the entire necessity " &
            "of the displayed Group component?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
            If RetVal = vbYes Then
                ExecuteSqlProcedure("Delete from MedicalNecessity where CPT_Code = '" & Trim(txtCPTCode.Text) & "'")
                dgvNecessity.Rows.Clear()
                btnRemAllNec.Enabled = False
                btnRemNec.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnAddNec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNec.Click
        If txtCPTCode.Text <> "" And txtICD9Code.Text <> "" Then
            If NecessityUnique(txtGroupID.Text, txtICD9Code.Text) Then
                dgvNecessity.Rows.Add(txtGroupID.Text, dgvNecessity.RowCount + 1, txtICD9Code.Text, txtICD9Description.Text)
                SaveNecessity(Trim(txtCPTCode.Text), Trim(txtICD9Code.Text))

                txtICD9Code.Text = ""
                txtICD9Description.Text = ""
                btnAddNec.Enabled = False
                btnRemAllNec.Enabled = True
                txtICD9Code.Focus()
            Else
                MsgBox("Record is already in the list")
                txtICD9Code.Text = ""
                txtICD9Code.Focus()
            End If
            If dgvNecessity.RowCount > 0 Then _
            dgvNecessity.FirstDisplayedScrollingRowIndex = dgvNecessity.RowCount - 1
        End If
    End Sub
    Private Sub UpdateNecessity(ByVal TestID)
        If ChkTBillable.Checked = True Then
            If dgvNecessity.RowCount > 0 Then
                Dim i As Integer
                Dim sSQL As String = ""
                Try
                    For i = 0 To dgvNecessity.RowCount - 1
                        If dgvNecessity.Rows(i).Cells(2).Value IsNot Nothing _
                        AndAlso Trim(dgvNecessity.Rows(i).Cells(2).Value) <> "" Then
                            sSQL = "If Exists (Select * from Necessity where TGP_ID = " & TestID &
                            " and Dx_Code = '" & Trim(dgvNecessity.Rows(i).Cells(2).Value) & "') " &
                            "Update Necessity Set Ordinal = 0, ICD9 = null where TGP_ID = " & TestID &
                            " and Dx_Code = '" & Trim(dgvNecessity.Rows(i).Cells(2).Value) & "' Else " &
                            "Insert into Necessity (TGP_ID, Dx_Code, ICD9, Ordinal) values (" & TestID &
                            ", '" & Trim(dgvNecessity.Rows(i).Cells(2).Value) & "', Null, 0)"
                            ExecuteSqlProcedure(sSQL)
                        End If
                    Next
                Catch ex As Exception

                End Try

            End If
        End If
    End Sub
    Private Function NecessityUnique(ByVal GroupID As String, ByVal ICD9 As String) As Boolean
        Dim Uniq As Boolean = True
        Dim i As Integer
        For i = 0 To dgvNecessity.RowCount - 1
            If dgvNecessity.Rows(i).Cells(0).Value = CInt(GroupID) And Trim(dgvNecessity.Rows(i).Cells(2).Value) = ICD9 Then
                Uniq = False
                Exit For
            End If
        Next
        NecessityUnique = Uniq
    End Function

    Private Sub txtICD9Code_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtICD9Code.GotFocus
        txtICD9Code.BackColor = FCOLOR
    End Sub

    Private Sub txtICD9Code_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtICD9Code.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtICD9Code_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtICD9Code.LostFocus
        txtICD9Code.BackColor = NFCOLOR
        If txtICD9Code.Text <> "" Then
            Dim cndx As New SqlConnection(connString)
            cndx.Open()
            Dim cmddx As New SqlCommand("Select * from " &
            "DiagCodes where Code = '" & Trim(txtICD9Code.Text) & "'", cndx)
            cmddx.CommandType = CommandType.Text
            Dim drdx As SqlDataReader = cmddx.ExecuteReader
            If drdx.HasRows Then
                While drdx.Read
                    txtICD9Description.Text = drdx("Description")
                End While
            Else
                MsgBox("The code does not exist in the master file.", MsgBoxStyle.Critical, "Prolis")
                txtICD9Code.Text = ""
                txtICD9Description.Text = ""
            End If
            cndx.Close()
            cndx = Nothing
        Else
            txtICD9Description.Text = ""
        End If
    End Sub

    Private Sub txtICD9Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtICD9Code.TextChanged
        If txtICD9Code.Text <> "" Then
            btnAddNec.Enabled = True
        Else
            btnAddNec.Enabled = False
        End If
    End Sub

    Private Sub btnNecCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNecCopy.Click
        Dim TGPID As String = frmNecCopy.ShowDialog()
        If TGPID <> "" Then
            Dim cndx As New SqlConnection(connString)
            cndx.Open()
            Dim cmddx As New SqlCommand("Select Dx_Code " &
            "from Necessity where TGP_ID = " & Val(TGPID), cndx)
            cmddx.CommandType = CommandType.Text
            Dim drdx As SqlDataReader = cmddx.ExecuteReader
            If drdx.HasRows Then
                While drdx.Read
                    dgvNecessity.Rows.Add(txtGroupID.Text, _
                    dgvNecessity.RowCount + 1, Trim(drdx("Dx_Code")), "")
                End While
            End If
            cndx.Close()
            cndx = Nothing
            If dgvNecessity.RowCount > 0 Then btnRemAllNec.Enabled = True
        End If
    End Sub

    Private Sub ChkTBillable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkTBillable.CheckedChanged
        If ChkTBillable.Checked = False Then
            ChkTBillable.Text = "NO"
        Else
            ChkTBillable.Text = "YES"
        End If
        If chkPBillable.Checked Or ChkTBillable.Checked Or chkCBillable.Checked Then
            TabControl3.Enabled = True
        Else
            ClearBilling()
            TabControl3.Enabled = False
        End If
        Update_Progress()
    End Sub

    Private Sub chkPBillable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPBillable.CheckedChanged
        If chkPBillable.Checked = False Then
            chkPBillable.Text = "NO"
        Else
            chkPBillable.Text = "YES"
        End If
        If chkPBillable.Checked Or ChkTBillable.Checked Or chkCBillable.Checked Then
            TabControl3.Enabled = True
        Else
            ClearBilling()
            TabControl3.Enabled = False
        End If
        Update_Progress()
    End Sub

    Private Sub chkCBillable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCBillable.CheckedChanged
        If chkCBillable.Checked = False Then
            chkCBillable.Text = "NO"
        Else
            chkCBillable.Text = "YES"
        End If
        If chkPBillable.Checked Or ChkTBillable.Checked Or chkCBillable.Checked Then
            TabControl3.Enabled = True
        Else
            ClearBilling()
            TabControl3.Enabled = False
        End If
        Update_Progress()
    End Sub

    Private Sub txtCPTCode_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
        Update_Progress()
    End Sub

    Private Sub txtGroupID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGroupID.LostFocus
        txtGroupID.BackColor = NFCOLOR
    End Sub

    Private Sub txtDescription_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDescription.GotFocus
        txtDescription.BackColor = FCOLOR
    End Sub

    Private Sub txtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDescription.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.PageUp Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtDescription_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDescription.LostFocus
        txtDescription.BackColor = NFCOLOR
    End Sub

    Private Sub txtICD9Description_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtICD9Description.GotFocus
        txtICD9Description.BackColor = FCOLOR
    End Sub

    Private Sub txtICD9Description_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtICD9Description.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.Left Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtICD9Description_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtICD9Description.LostFocus
        txtICD9Description.BackColor = NFCOLOR
    End Sub

    Private Sub txtLoinc_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLoinc.GotFocus
        txtLoinc.BackColor = FCOLOR
    End Sub

    Private Sub txtLoinc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLoinc.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtLoinc_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLoinc.LostFocus
        txtLoinc.BackColor = NFCOLOR
    End Sub

    Private Sub txtRefLabTestID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRefLabTestID.GotFocus
        txtRefLabTestID.BackColor = FCOLOR
    End Sub

    Private Sub txtRefLabTestID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRefLabTestID.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtRefLabTestID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRefLabTestID.LostFocus
        txtRefLabTestID.BackColor = NFCOLOR
    End Sub

    Private Sub txtTestName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTestName.GotFocus
        txtTestName.BackColor = FCOLOR
    End Sub

    Private Sub txtTestName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTestName.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtTestName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTestName.LostFocus
        txtTestName.BackColor = NFCOLOR
    End Sub

    Private Sub txtSearchID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchID.GotFocus
        txtSearchID.BackColor = FCOLOR
    End Sub

    Private Sub txtSearchID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearchID.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtSearchID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtSearchID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchID.LostFocus
        txtSearchID.BackColor = NFCOLOR
    End Sub

    Private Sub txtSearchID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchID.Validated
        Dim RetVal As Integer
        If txtSearchID.Text <> "" Then
            If chkEditNew.Checked = True Then     'New
                If IsTGPUnique(Val(txtSearchID.Text)) = True Then
                    txtGroupID.Text = txtSearchID.Text
                    If txtName.Text <> "" And txtAbbr.Text <> "" Then btnSave.Enabled = True
                Else
                    RetVal = MsgBox("You need to provide an unused number or simply accept the system generated one." _
                    & " Do you want to provide the unique number?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo)
                    If RetVal = vbYes Then
                        txtSearchID.Text = ""
                        txtSearchID.Focus()
                    Else
                        txtSearchID.Text = GetNextGroupID()
                        txtGroupID.Text = txtSearchID.Text
                    End If
                End If
            Else    'Edit
                DisplayTheGroup(Val(txtSearchID.Text))

                'If Not String.IsNullOrEmpty(txtSearchID.Text) Then

                '    btnReplicate.Enabled = True
                'End If

                If txtName.Text <> "" Then
                    btnDelete.Enabled = True
                    btnSave.Enabled = True
                    btnReplicate.Enabled = True
                Else
                    MsgBox("The group code you typed, does not exit. You may use " _
                    & "the 'Lookup' function to select the group")
                    txtSearchID.Text = ""
                    txtSearchID.Focus()
                End If
            End If
        Else
            ClearForm()
        End If
        Update_Progress()
    End Sub

    Private Sub txtMod1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMod1.GotFocus
        txtMod1.BackColor = FCOLOR
    End Sub

    Private Sub txtMod1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMod1.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtMod1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMod1.LostFocus
        txtMod1.BackColor = NFCOLOR
    End Sub

    Private Sub txtMod2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMod2.GotFocus
        txtMod2.BackColor = FCOLOR
    End Sub

    Private Sub txtMod2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMod2.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtMod2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMod2.LostFocus
        txtMod2.BackColor = NFCOLOR
    End Sub

    Private Sub txtMod3_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMod3.GotFocus
        txtMod3.BackColor = FCOLOR
    End Sub

    Private Sub txtMod3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMod3.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtMod3_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMod3.LostFocus
        txtMod3.BackColor = NFCOLOR
    End Sub

    Private Sub txtMod4_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMod4.GotFocus
        txtMod4.BackColor = FCOLOR
    End Sub

    Private Sub txtMod4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMod4.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtMod4_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMod4.LostFocus
        txtMod4.BackColor = NFCOLOR
    End Sub

    Private Sub txtCPTMCD_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCPTMCD.GotFocus
        txtCPTMCD.BackColor = FCOLOR
    End Sub

    Private Sub txtCPTMCD_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCPTMCD.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtCPTMCD_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCPTMCD.LostFocus
        txtCPTMCD.BackColor = NFCOLOR
    End Sub

    Private Sub txtCPTMCR_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCPTMCR.GotFocus
        txtCPTMCR.BackColor = FCOLOR
    End Sub

    Private Sub txtCPTMCR_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCPTMCR.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtCPTMCR_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCPTMCR.LostFocus
        txtCPTMCR.BackColor = NFCOLOR
    End Sub

    Private Sub txtCPTSPC_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCPTSPC.GotFocus
        txtCPTSPC.BackColor = FCOLOR
    End Sub

    Private Sub txtCPTSPC_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCPTSPC.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtCPTSPC_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCPTSPC.LostFocus
        txtCPTSPC.BackColor = NFCOLOR
    End Sub

    Private Sub txtPOS_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPOS.GotFocus
        txtPOS.BackColor = FCOLOR
    End Sub

    Private Sub txtPOS_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPOS.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPOS_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPOS.LostFocus
        txtPOS.BackColor = NFCOLOR
    End Sub

    Private Sub txtPOS_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPOS.Validated
        If txtPOS.Text = "" Then txtPOS.Text = "81"
    End Sub

    Private Sub txtBillUnit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBillUnit.GotFocus
        txtBillUnit.BackColor = FCOLOR
    End Sub

    Private Sub txtBillUnit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBillUnit.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtBillUnit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBillUnit.KeyPress
        Prices(txtBillUnit, e)
    End Sub

    Private Sub txtBillUnit_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBillUnit.LostFocus
        txtBillUnit.BackColor = NFCOLOR
    End Sub

    Private Sub txtBillUnit_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBillUnit.Validated
        If txtBillUnit.Text = "" Then txtBillUnit.Text = "1.0"
    End Sub

    Private Sub txtCPTCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCPTCode.GotFocus
        txtCPTCode.BackColor = FCOLOR
    End Sub

    Private Sub txtCPTCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCPTCode.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtCPTCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCPTCode.LostFocus
        txtCPTCode.BackColor = NFCOLOR
    End Sub

    Private Sub txtListPrice_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtListPrice.GotFocus
        txtListPrice.BackColor = FCOLOR
    End Sub

    Private Sub txtListPrice_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtListPrice.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub
    Private Sub txtListPrice_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtListPrice.KeyPress
        Prices(txtListPrice, e)
    End Sub

    Private Sub txtPrice1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice1.GotFocus
        txtPrice1.BackColor = FCOLOR
    End Sub

    Private Sub txtPrice1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPrice1.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub
    Private Sub txtPrice1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrice1.KeyPress
        Prices(txtPrice1, e)
    End Sub

    Private Sub txtPrice2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice2.GotFocus
        txtPrice2.BackColor = FCOLOR
    End Sub

    Private Sub txtPrice2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPrice2.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPrice2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrice2.KeyPress
        Prices(txtPrice2, e)
    End Sub

    Private Sub txtPrice3_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice3.GotFocus
        txtPrice3.BackColor = FCOLOR
    End Sub

    Private Sub txtPrice3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPrice3.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPrice3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrice3.KeyPress
        Prices(txtPrice3, e)
    End Sub

    Private Sub txtPrice4_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice4.GotFocus
        txtPrice4.BackColor = FCOLOR
    End Sub

    Private Sub txtPrice4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPrice4.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPrice4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrice4.KeyPress
        Prices(txtPrice4, e)
    End Sub

    Private Sub txtPrice5_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice5.GotFocus
        txtPrice5.BackColor = FCOLOR
    End Sub

    Private Sub txtPrice5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPrice5.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPrice5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrice5.KeyPress
        Prices(txtPrice5, e)
    End Sub

    Private Sub txtPrice6_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice6.GotFocus
        txtPrice6.BackColor = FCOLOR
    End Sub

    Private Sub txtPrice6_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPrice6.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPrice6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrice6.KeyPress
        Prices(txtPrice6, e)
    End Sub

    Private Sub txtPrice7_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice7.GotFocus
        txtPrice7.BackColor = FCOLOR
    End Sub

    Private Sub txtPrice7_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPrice7.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPrice7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrice7.KeyPress
        Prices(txtPrice7, e)
    End Sub

    Private Sub txtPrice8_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice8.GotFocus
        txtPrice8.BackColor = FCOLOR
    End Sub

    Private Sub txtPrice8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPrice8.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPrice8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrice8.KeyPress
        Prices(txtPrice8, e)
    End Sub

    Private Sub txtPrice9_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice9.GotFocus
        txtPrice9.BackColor = FCOLOR
    End Sub

    Private Sub txtPrice9_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPrice9.KeyDown
        If e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPrice9_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrice9.KeyPress
        Prices(txtPrice9, e)
    End Sub

    Private Sub txtListPrice_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtListPrice.LostFocus
        txtListPrice.BackColor = NFCOLOR
    End Sub

    Private Sub txtPrice1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice1.LostFocus
        txtPrice1.BackColor = NFCOLOR
    End Sub

    Private Sub txtPrice2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice2.LostFocus
        txtPrice2.BackColor = NFCOLOR
    End Sub

    Private Sub txtPrice3_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice3.LostFocus
        txtPrice3.BackColor = NFCOLOR
    End Sub

    Private Sub txtPrice4_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice4.LostFocus
        txtPrice4.BackColor = NFCOLOR
    End Sub

    Private Sub txtPrice5_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice5.LostFocus
        txtPrice5.BackColor = NFCOLOR
    End Sub

    Private Sub txtPrice6_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice6.LostFocus
        txtPrice6.BackColor = NFCOLOR
    End Sub

    Private Sub txtPrice7_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice7.LostFocus
        txtPrice7.BackColor = NFCOLOR
    End Sub

    Private Sub txtPrice8_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice8.LostFocus
        txtPrice8.BackColor = NFCOLOR
    End Sub

    Private Sub txtPrice9_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice9.LostFocus
        txtPrice9.BackColor = NFCOLOR
    End Sub

    Private Sub ChkInHouse_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkInHouse.CheckedChanged
        If ChkInHouse.Checked = False Then
            ChkInHouse.Text = "NO"
            'GBLabAssociation.Enabled = True
        Else
            ChkInHouse.Text = "YES"
            'dgvLabs.Rows.Clear()
            'GBLabAssociation.Enabled = False
        End If
        Update_Progress()
    End Sub

    Private Sub TabControl3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl3.SelectedIndexChanged
        If TabControl3.SelectedTab Is TabControl3.TabPages(1) Then
            DisplayNecessity(txtGroupID.Text)
        End If
    End Sub

    Dim Replication As Boolean
    Friend Shared rsNotes As String

    Private Sub btnReplicate_Click(sender As Object, e As EventArgs) Handles btnReplicate.Click

        Dim RetVal As Integer = MsgBox("Are you sure to Replicate the selected Record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
        If RetVal = vbYes Then
            Replication = True

            txtSearchID.Text = GetNextGroupID()
            txtGroupID.Text = txtSearchID.Text

            btnSave_Click(Nothing, Nothing)
            Replication = False
            MessageBox.Show($"Record Replicated as New ID = {txtGroupID.Text} ")
        End If
    End Sub

    Private Sub btnNote_Click(sender As Object, e As EventArgs) Handles btnNote.Click
        'txtResultNote.Text = frmGroupNote.ShowDialog()
        'If txtResultNote.Text <> "" Then
        '    btnNote.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Note.ico")
        'Else
        '    btnNote.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\NoteBlank.ico")
        'End If

        Dim f As New frmNotes

        If IsRtf(txtResultNote.Text) = False Then
            txtResultNote.Text = ConvertPlainTextToRtf(txtResultNote.Text)
        End If

        f.ProlisText2.RichTextBox1.Rtf = txtResultNote.Text
        f.ShowDialog()

        If f.DialogResult = DialogResult.OK Then

            If String.IsNullOrWhiteSpace(f.ProlisText2.RichTextBox1.Text) Then
                txtResultNote.Clear()
                btnNote.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\NoteBlank.ico")

            Else
                txtResultNote.Text = f.ProlisText2.RichTextBox1.Rtf
                btnNote.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Note.ico")
            End If

        End If

    End Sub
End Class
