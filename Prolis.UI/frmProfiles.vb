
Imports Microsoft.Data.SqlClient


Public Class frmProfiles
    Private SearchMode As Boolean = True

    Private Sub chkEditNew_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEditNew.CheckedChanged
        ClearForm()
        If chkEditNew.Checked = False Then
            chkEditNew.Text = "Edit"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Edit.ico")
            SearchMode = True
            btnProfLook.Enabled = True
        Else
            chkEditNew.Text = "New"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\New.ico")
            SearchMode = False
            btnProfLook.Enabled = False
            txtSearchID.Text = GetNextTGPID()
        End If
    End Sub

    Private Sub ClearForm()
        txtSearchID.Text = ""
        txtProfileID.Text = ""
        'txtProfileID.ReadOnly = False
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
        dgvTGs.Rows.Clear()
    End Sub

    Private Function GetNextTGPID() As Long
        Dim LastID As Integer = 0
        Dim DefaultID As Integer = 1
        Dim cntgp As New SqlConnection(connString)
        cntgp.Open()
        Dim cmdtgp As New SqlCommand("Select max(ID) as LastID " &
        "from Tests union Select max(ID) as LastID from Groups Union Select " _
        & "max(ID) as LastID from Profiles Union " _
        & "Select max(ID) as LastID from Departments", cntgp)
        cmdtgp.CommandType = CommandType.Text
        Dim drtgp As SqlDataReader = cmdtgp.ExecuteReader
        If drtgp.HasRows Then
            While drtgp.Read
                If drtgp("LastID") IsNot DBNull.Value Then
                    If drtgp("LastID") > LastID Then LastID = drtgp("LastID")
                End If
            End While
        End If
        cntgp.Close()
        cntgp = Nothing
        If LastID > DefaultID Then
            GetNextTGPID = LastID + 1
        Else
            GetNextTGPID = DefaultID
        End If
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
        If txtSearchID.Text = txtProfileID.Text Then
            If txtProfileID.Text <> "" And txtName.Text <> "" And txtAbbr.Text <> "" And
            ((ChkTBillable.Checked = True And Trim(txtCPTCode.Text) <> "" And txtListPrice.Text <> "") Or
            (ChkTBillable.Checked = False)) And (((chkCBillable.Checked = True Or chkPBillable.Checked = True) _
            And (txtListPrice.Text <> "")) Or (chkCBillable.Checked = False And chkPBillable.Checked = False)) Then
                SaveProfile(Val(txtProfileID.Text))
                ExecuteSqlProcedure("Delete from MarkingModifiers where Test_ID = " & Val(txtProfileID.Text))
                If ChkMarkable.Checked = True Then
                    If dgvModifiers.RowCount > 0 Then SaveModifiers(Val(txtProfileID.Text))
                End If
                '

                If Replication = True Then Return

                ClearForm()
                If chkEditNew.Checked = True Then  'New
                    txtSearchID.Text = GetNextTGPID()
                    txtProfileID.Text = txtSearchID.Text
                End If
            Else
                MsgBox("Profile name and Abbr are required to save it. If you are marking the profile as " &
                "Third Party Billable, you are required to provide CPT Code.", MsgBoxStyle.Exclamation, "PROLIS")
            End If
        Else
            MsgBox("Profile ID is different than that of the displayed record. Correct it and try again.", MsgBoxStyle.Critical, "PROLIS")
            txtSearchID.Text = txtProfileID.Text
            txtSearchID.Focus()
        End If
    End Sub

    Private Sub SaveProfile(ByVal ProfileID As Integer)
        Dim cnp As New SqlConnection(connString)
        cnp.Open()
        Dim cmdupsert As New SqlCommand("Profiles_SP", cnp)
        cmdupsert.CommandType = CommandType.StoredProcedure
        cmdupsert.Parameters.AddWithValue("@act", "Upsert")
        cmdupsert.Parameters.AddWithValue("@ID", ProfileID)
        cmdupsert.Parameters.AddWithValue("@Name", Trim(txtName.Text))
        cmdupsert.Parameters.AddWithValue("@Abbr", Trim(txtAbbr.Text))
        cmdupsert.Parameters.AddWithValue("@Description", Trim(txtDescription.Text))
        cmdupsert.Parameters.AddWithValue("@Loinc", Trim(txtLoinc.Text))
        cmdupsert.Parameters.AddWithValue("@ComponentType", "P")
        cmdupsert.Parameters.AddWithValue("@IsActive", Convert.ToInt16(ChkActive.Checked))
        cmdupsert.Parameters.AddWithValue("@BreakOROrder", Convert.ToInt16(chkBreakOROrder.Checked))
        cmdupsert.Parameters.AddWithValue("@InHouse", Convert.ToInt16(ChkInHouse.Checked))
        cmdupsert.Parameters.AddWithValue("@CPT_Code", Trim(txtCPTCode.Text))
        cmdupsert.Parameters.AddWithValue("@CPT_MCR", Trim(txtCPTMCR.Text))
        cmdupsert.Parameters.AddWithValue("@CPT_MCD", Trim(txtCPTMCD.Text))
        cmdupsert.Parameters.AddWithValue("@CPT_SPC", Trim(txtCPTSPC.Text))
        cmdupsert.Parameters.AddWithValue("@Mod1", Trim(txtMod1.Text))
        cmdupsert.Parameters.AddWithValue("@Mod2", Trim(txtMod2.Text))
        cmdupsert.Parameters.AddWithValue("@Mod3", Trim(txtMod3.Text))
        cmdupsert.Parameters.AddWithValue("@Mod4", Trim(txtMod4.Text))
        cmdupsert.Parameters.AddWithValue("@Bill_Unit", Trim(txtBillUnit.Text))
        cmdupsert.Parameters.AddWithValue("@POS_Code", Trim(txtPOS.Text))
        cmdupsert.Parameters.AddWithValue("@PBillable", Convert.ToInt16(chkPBillable.Checked))
        cmdupsert.Parameters.AddWithValue("@CBillable", Convert.ToInt16(chkCBillable.Checked))
        cmdupsert.Parameters.AddWithValue("@TBillable", Convert.ToInt16(ChkTBillable.Checked))
        cmdupsert.Parameters.AddWithValue("@IsMarkable", Convert.ToInt16(ChkMarkable.Checked))
        cmdupsert.Parameters.AddWithValue("@ListPrice", Trim(txtListPrice.Text))
        cmdupsert.Parameters.AddWithValue("@Price1", Trim(txtPrice1.Text))
        cmdupsert.Parameters.AddWithValue("@Price2", Trim(txtPrice2.Text))
        cmdupsert.Parameters.AddWithValue("@Price3", Trim(txtPrice3.Text))
        cmdupsert.Parameters.AddWithValue("@Price4", Trim(txtPrice4.Text))
        cmdupsert.Parameters.AddWithValue("@Price5", Trim(txtPrice5.Text))
        cmdupsert.Parameters.AddWithValue("@Price6", Trim(txtPrice6.Text))
        cmdupsert.Parameters.AddWithValue("@Price7", Trim(txtPrice7.Text))
        cmdupsert.Parameters.AddWithValue("@Price8", Trim(txtPrice8.Text))
        cmdupsert.Parameters.AddWithValue("@Price9", Trim(txtPrice9.Text))
        cmdupsert.Parameters.AddWithValue("@LastEditedOn", Date.Now)
        cmdupsert.Parameters.AddWithValue("@EditedBy", ThisUser.ID)
        Try
            cmdupsert.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cnp.Close()
            cnp = Nothing
        End Try
        '
        ExecuteSqlProcedure("Delete from Prof_GrpTst where Profile_ID = " & ProfileID)
        '
        For i As Integer = 0 To dgvTGs.RowCount - 1
            If dgvTGs.Rows(i).Cells(0).Value IsNot Nothing AndAlso
            Trim(dgvTGs.Rows(i).Cells(0).Value.ToString) <> "" Then
                ExecuteSqlProcedure("Insert into Prof_GrpTst (Profile_ID, GrpTst_ID, Ordinal) values (" &
                ProfileID & ", " & Trim(dgvTGs.Rows(i).Cells(0).Value.ToString) & ", " & i & ")")
            End If
        Next
    End Sub

    Private Sub SaveModifiers(ByVal GroupID As Integer)
        ExecuteSqlProcedure("Delete from MarkingModifiers where Test_ID = " & GroupID)
        '
        For i As Integer = 0 To dgvModifiers.RowCount - 1
            ExecuteSqlProcedure("Insert into MarkingModifiers (Test_ID, Sex, AgeFrom, AgeTo, " &
            "LastEditedOn, EditedBy) values (" & GroupID & ", '" & dgvModifiers.Rows(i).Cells(2).Value &
            "', " & dgvModifiers.Rows(i).Cells(3).Value & ", " & dgvModifiers.Rows(i).Cells(4).Value &
            ", '" & Date.Now & "', " & ThisUser.ID & ")")
        Next
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If txtSearchID.Text = txtProfileID.Text Then
            If txtProfileID.Text <> "" And txtName.Text <> "" And txtAbbr.Text <> "" Then
                If MarkedOnPats(Val(txtProfileID.Text)) = False Then
                    If ThisUser.Hard_Deletion = True Then
                        Dim Retval As Integer
                        Retval = MsgBox("It is recommended to make the Component Inactive instead of deleting " _
                        & "it from the system entirely. Are you firm to delete this component anyway?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo)
                        If Retval = vbYes Then
                            ExecuteSqlProcedure("Delete from MarkingModifiers where Test_ID = " & Val(txtProfileID.Text))
                            ExecuteSqlProcedure("Delete from Necessity where TGP_ID = " & Val(txtProfileID.Text))
                            ExecuteSqlProcedure("Delete from Profiles where ID = " & Val(txtProfileID.Text))
                            ExecuteSqlProcedure("Delete from Prof_GrpTst where Profile_ID = " & Val(txtProfileID.Text))
                            ClearForm()
                            SearchMode = True
                        End If
                    Else
                        MsgBox("You don't have permission to delete any component of the system", MsgBoxStyle.Critical)
                    End If
                Else
                    MsgBox("The Profile you want to delete, has been used in processing the " &
                    "requisition(s). System does not allow the deletion of such component. You may " &
                    "mark it as 'Inactive'", MsgBoxStyle.Critical)
                End If
            End If
        Else
            MsgBox("Profile ID is different than that of the displayed record. Correct it and try again.", MsgBoxStyle.Critical, "Prolis")
            txtSearchID.Text = txtProfileID.Text
            txtSearchID.Focus()
        End If
    End Sub

    Private Function MarkedOnPats(ByVal TGPID As Integer) As Boolean
        Dim Marked As Boolean = False
        Dim cnmrk As New SqlConnection(connString)
        cnmrk.Open()
        Dim cmdmrk As New SqlCommand("Select " &
        "* from Req_TGP where TGP_ID = " & TGPID, cnmrk)
        cmdmrk.CommandType = CommandType.Text
        Dim drmrk As SqlDataReader = cmdmrk.ExecuteReader
        If drmrk.HasRows Then Marked = True
        cnmrk.Close()
        cnmrk = Nothing
        Return Marked
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub txtProfileID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProfileID.GotFocus
        txtProfileID.BackColor = FCOLOR
    End Sub

    Private Sub txtProfileID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProfileID.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtGroupID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtProfileID.KeyPress
        Numerals(sender, e)
    End Sub

    Friend Sub DisplayProfile(ByVal ProfileID As Integer)
        Dim cndp As New SqlConnection(connString)
        cndp.Open()
        Dim cmddp As New SqlCommand("Select * from Profiles where ID = " & ProfileID, cndp)
        cmddp.CommandType = CommandType.Text
        Dim drdp As SqlDataReader = cmddp.ExecuteReader
        If drdp.HasRows Then
            While drdp.Read
                txtProfileID.Text = drdp("ID")
                txtName.Text = drdp("Name")
                If drdp("Abbr") IsNot DBNull.Value Then txtAbbr.Text = drdp("Abbr")
                If drdp("Loinc") IsNot DBNull.Value Then txtLoinc.Text = drdp("Loinc")
                If drdp("Description") IsNot DBNull.Value Then txtDescription.Text = drdp("Description")
                ChkActive.Checked = drdp("Isactive")
                chkBreakOROrder.Checked = drdp("BreakOROrder")
                ChkMarkable.Checked = drdp("IsMarkable")
                ChkTBillable.Checked = drdp("TBillable")
                chkCBillable.Checked = drdp("CBillable")
                chkPBillable.Checked = drdp("PBillable")
                ChkInHouse.Checked = drdp("InHouse")
                If drdp("CPT_Code") IsNot DBNull.Value Then txtCPTCode.Text = Trim(drdp("CPT_Code"))
                If drdp("CPT_MCR") IsNot DBNull.Value Then txtCPTMCR.Text = Trim(drdp("CPT_MCR"))
                If drdp("CPT_MCD") IsNot DBNull.Value Then txtCPTMCD.Text = Trim(drdp("CPT_MCD"))
                If drdp("CPT_SPC") IsNot DBNull.Value Then txtCPTSPC.Text = Trim(drdp("CPT_SPC"))
                If drdp("Bill_Unit") IsNot DBNull.Value Then
                    txtBillUnit.Text = Trim(drdp("Bill_Unit"))
                Else
                    txtBillUnit.Text = 1.0
                End If
                If drdp("Mod1") IsNot DBNull.Value Then txtMod1.Text = Trim(drdp("Mod1"))
                If drdp("Mod2") IsNot DBNull.Value Then txtMod2.Text = Trim(drdp("Mod2"))
                If drdp("Mod3") IsNot DBNull.Value Then txtMod3.Text = Trim(drdp("Mod3"))
                If drdp("Mod4") IsNot DBNull.Value Then txtMod4.Text = Trim(drdp("Mod4"))
                If drdp("POS_Code") IsNot DBNull.Value Then txtPOS.Text = Trim(drdp("POS_Code"))
                txtListPrice.Text = drdp("ListPrice")
                txtPrice1.Text = drdp("Price1")
                txtPrice2.Text = drdp("Price2")
                txtPrice3.Text = drdp("Price3")
                txtPrice4.Text = drdp("Price4")
                txtPrice5.Text = drdp("Price5")
                txtPrice6.Text = drdp("Price6")
                txtPrice7.Text = drdp("Price7")
                txtPrice8.Text = drdp("Price8")
                txtPrice9.Text = drdp("Price9")
            End While
        End If
        cndp.Close()
        cndp = Nothing
        '
        dgvTGs.Rows.Clear()
        Dim cndp1 As New SqlConnection(connString)
        cndp1.Open()
        Dim cmddp1 As New SqlCommand("Select * from Prof_GrpTst " &
        "where Profile_ID = " & ProfileID & " Order by Ordinal", cndp1)
        cmddp1.CommandType = CommandType.Text
        Dim drdp1 As SqlDataReader = cmddp1.ExecuteReader
        If drdp1.HasRows Then
            While drdp1.Read
                dgvTGs.Rows.Add(drdp1("GrpTst_ID"), IIf(GetTGPType(drdp1("GrpTst_ID")) = "T",
               System.Drawing.Image.FromFile(Application.StartupPath & "\Images\test.ico"),
               System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Group.ico")),
               GetTGPName(drdp1("GrpTst_ID")))
            End While
        End If
        cndp1.Close()
        cndp1 = Nothing
        '
        If ChkMarkable.Checked = True Then DisplayModifiers(Val(txtProfileID.Text))
        If ChkMarkable.Checked = True Then DisplayModifiers(ProfileID)
        If ChkTBillable.Checked = True Then
            If TabControl3.SelectedTab.Name = "tpNecessity" _
            AndAlso Trim(txtCPTCode.Text) <> "" Then
                DisplayNecessity(Trim(txtCPTCode.Text))
            End If
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

    Private Function ProfileExist(ByVal ProfileID As Integer) As Boolean
        Dim Exist As Boolean = False
        Dim cnpe As New SqlConnection(connString)
        cnpe.Open()
        Dim cmdpe As New SqlCommand("Select " &
        "* from Profiles where ID = " & ProfileID, cnpe)
        cmdpe.CommandType = CommandType.Text
        Dim drpe As SqlDataReader = cmdpe.ExecuteReader
        If drpe.HasRows Then Exist = True
        cnpe.Close()
        cnpe = Nothing
        Return Exist
    End Function

    Private Sub DisplayModifiers(ByVal GroupID As Integer)
        dgvModifiers.Rows.Clear()
        Dim cndm As New SqlConnection(connString)
        cndm.Open()
        Dim cmddm As New SqlCommand("Select " &
        "* from MarkingModifiers where Test_ID = " & GroupID, cndm)
        cmddm.CommandType = CommandType.Text
        Dim drdm As SqlDataReader = cmddm.ExecuteReader
        If drdm.HasRows Then
            While drdm.Read
                dgvModifiers.Rows.Add(dgvModifiers.RowCount + 1, GroupID,
                ToGender(drdm("Sex")), drdm("AgeFrom"), drdm("AgeTo"))
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

    Private Sub ChkInHouse_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkInHouse.CheckedChanged
        If ChkInHouse.Checked = False Then
            ChkInHouse.Text = "NO"
            GBLabAssociation.Enabled = True
        Else
            ChkInHouse.Text = "YES"
            dgvLabs.Rows.Clear()
            GBLabAssociation.Enabled = False
        End If
        Update_Progress()
    End Sub

    Private Function GetTGItem(ByVal TGPID As Integer) As MyList
        Dim ItemX As New MyList
        Dim cngi As New SqlConnection(connString)
        cngi.Open()
        Dim cmdgi As New SqlCommand("Select ID, Name from Tests where " &
        "ID = " & TGPID & " Union Select ID, Name from Groups where ID = " & TGPID, cngi)
        cmdgi.CommandType = CommandType.Text
        Dim drgi As SqlDataReader = cmdgi.ExecuteReader
        If drgi.HasRows Then
            While drgi.Read
                ItemX.ItemData = drgi("ID")
                ItemX.Name = drgi("Name")
            End While
        Else
            ItemX.ItemData = -1
            ItemX.Name = ""
        End If
        cngi.Close()
        cngi = Nothing
        Return ItemX
    End Function

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
        If txtSearchID.Text <> "" And txtName.Text <> "" And txtAbbr.Text <> "" _
        And dgvTGs.RowCount > 0 And ((ChkTBillable.Checked = True And Trim(txtCPTCode.Text) <> "" _
        And txtListPrice.Text <> "") Or (ChkTBillable.Checked = False)) And ((chkCBillable.Checked = True _
        Or chkPBillable.Checked = True And txtListPrice.Text <> "") Or (chkCBillable.Checked = False And
        chkPBillable.Checked = False)) Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub btnRemTst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemTst.Click
        If dgvTGs.SelectedRows(0).Index <> -1 Then
            dgvTGs.Rows.RemoveAt(dgvTGs.SelectedRows(0).Index)
            txtCompID.Focus()
            btnRemTst.Enabled = False
            If dgvTGs.RowCount = 0 Then btnRemTstAll.Enabled = False
        End If
        Update_Progress()
    End Sub

    Private Sub btnRemTstAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemTstAll.Click
        dgvTGs.Rows.Clear()
        txtCompID.Focus()
        btnRemTstAll.Enabled = False
        btnRemTst.Enabled = False
        Update_Progress()
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
            If dgvNecessity.RowCount > 6 Then dgvNecessity.Rows.RemoveAt(dgvNecessity.RowCount - 1)
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
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
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
        If txtProfileID.Text <> "" And cmbSex.SelectedIndex <> -1 And
        txtAgeFrom.Text <> "" And txtAgeTo.Text <> "" Then
            'btnAddModifier.Enabled = True
            If Not ModifierInList(Val(txtProfileID.Text), cmbSex.SelectedItem.ToString.Substring(0, 1),
            Val(txtAgeFrom.Text), Val(txtAgeTo.Text)) Then
                dgvModifiers.Rows.Add(Val(txtProfileID.Text), dgvModifiers.RowCount + 1,
                cmbSex.SelectedItem.ToString.Substring(0, 1), Val(txtAgeFrom.Text), Val(txtAgeTo.Text))
                btnRemModAll.Enabled = True
            End If
        End If
    End Sub

    Private Function ModifierInList(ByVal Test_ID As Integer, ByVal Sex As String,
    ByVal AgeFrom As Integer, ByVal AgeTo As Integer) As Boolean
        Dim InList As Boolean = False
        Dim i As Integer
        For i = 0 To dgvModifiers.RowCount - 1
            If dgvModifiers.Rows(i).Cells(0).Value = Val(txtProfileID.Text) And
            dgvModifiers.Rows(i).Cells(2).Value = cmbSex.SelectedItem.ToString.Substring(0, 1) And
            dgvModifiers.Rows(i).Cells(3).Value = Val(txtAgeFrom.Text) And
            dgvModifiers.Rows(i).Cells(4).Value = Val(txtAgeTo.Text) Then
                InList = True
                Exit For
            End If
        Next
        ModifierInList = InList
    End Function

    Private Sub btnAddComp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddComp.Click
        If txtCompID.Text <> "" And txtCompName.Text <> "" Then
            If Not TestInTheList(Val(txtCompID.Text)) Then
                dgvTGs.Rows.Add(Val(txtCompID.Text), IIf(GetTGPType(Val(txtCompID.Text)) = "T",
                System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Test.ico"),
                System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Group.ico")),
                txtCompName.Text)
                txtCompID.Text = ""
                txtCompName.Text = ""
                txtCompID.Focus()
                btnAddComp.Enabled = False
                btnRemTstAll.Enabled = True
            Else
                MsgBox("Duplicate Entry is not allowed!", MsgBoxStyle.Critical, "Prolis")
                txtCompID.Text = ""
                txtCompName.Text = ""
                txtCompID.Focus()
                btnAddComp.Enabled = False
                If dgvTGs.RowCount > 0 Then btnRemTstAll.Enabled = True
            End If
        Else
            MsgBox("You need to have an Analyte displayed to add to the list.")
            txtCompID.Text = ""
            txtCompName.Text = ""
            txtCompID.Focus()
            btnAddComp.Enabled = False
            If dgvTGs.RowCount > 0 Then btnRemTstAll.Enabled = True
        End If
        Update_Progress()
    End Sub

    Private Sub btnProfLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProfLook.Click
        Dim ProfileID As String = frmProfLookup.ShowDialog()
        If ProfileID <> "" Then
            txtSearchID.Text = ProfileID
            DisplayProfile(Val(ProfileID))
            btnSave.Enabled = True
            btnDelete.Enabled = True
            btnReplicate.Enabled = True
        ElseIf txtSearchID.Text <> "" Then
            ProfileID = txtSearchID.Text
            DisplayProfile(Val(ProfileID))
            btnSave.Enabled = True
            btnDelete.Enabled = True
        End If
    End Sub

    Private Sub btnTGLookup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTGLookup.Click
        Dim TGID As String = frmTGLookup.ShowDialog()
        If TGID <> "" Then
            Dim ItemX As MyList = GetTGItem(Val(TGID))
            If ItemX.ItemData <> -1 Then
                txtCompID.Text = TGID
                txtCompName.Text = ItemX.Name
                btnAddComp.Enabled = True
            End If
        End If
    End Sub

    Private Sub txtCompID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCompID.GotFocus
        txtCompID.BackColor = FCOLOR
    End Sub

    Private Sub txtCompID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCompID.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtCompID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCompID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtCompID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCompID.LostFocus
        txtCompID.BackColor = NFCOLOR
    End Sub

    Private Sub txtCompID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCompID.Validated
        If txtCompID.Text <> "" Then
            Dim ItemX As MyList = GetTGItem(Val(txtCompID.Text))
            If ItemX.ItemData <> -1 Then
                txtCompName.Text = ItemX.Name
                btnAddComp.Enabled = True
            Else
                MsgBox("Invalid Analyte ID. Use Look Up instead")
                txtCompID.Text = ""
                txtCompID.Focus()
            End If
        Else
            txtCompName.Text = ""
        End If
    End Sub

    Private Sub DisplayNecessity(ByVal CPT As String)
        dgvNecessity.Rows.Clear()
        Dim sSQL As String = "Select a.Dx_Code, b.Description from MedicalNecessity a " &
        "inner join DiagCodes b on a.Dx_Code = b.Code where a.CPT_Code = '" & CPT & "'"
        Dim cndn As New SqlConnection(connString)
        cndn.Open()
        Dim cmddn As New SqlCommand(sSQL, cndn)
        cmddn.CommandType = Data.CommandType.Text
        Dim drdn As SqlDataReader = cmddn.ExecuteReader
        If drdn.HasRows Then
            While drdn.Read
                If drdn("Dx_Code") IsNot DBNull.Value _
                AndAlso Trim(drdn("Dx_Code")) <> "" Then
                    dgvNecessity.Rows.Add(Val(txtProfileID.Text), dgvNecessity.RowCount + 1,
                    Trim(drdn("Dx_Code")), drdn("Description"))
                End If
            End While
            btnRemAllNec.Enabled = True
        End If
        cndn.Close()
        cndn = Nothing
    End Sub

    Private Sub txtProfileID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProfileID.LostFocus
        txtProfileID.BackColor = NFCOLOR
    End Sub

    Private Sub frmProfiles_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CN.Open("DSN=ProlisQC;UID=sa;PWD=''")
        dgvNecessity.RowCount = 6
        txtBillUnit.Text = "1.0"
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub dgvTGs_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTGs.CellClick
        If e.RowIndex <> -1 Then btnRemTst.Enabled = True
    End Sub

    Private Sub dgvModifiers_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvModifiers.CellClick
        If e.RowIndex <> -1 Then btnRemMod.Enabled = True
    End Sub

    Private Sub btnRemMod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemMod.Click
        If dgvModifiers.SelectedRows(0).Index <> -1 Then
            dgvModifiers.Rows.RemoveAt(dgvModifiers.SelectedRows(0).Index)
            btnRemMod.Enabled = False
            If dgvModifiers.RowCount = 0 Then btnRemModAll.Enabled = False
        End If
    End Sub

    Private Sub btnRemModAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemModAll.Click
        dgvModifiers.Rows.Clear()
        btnRemMod.Enabled = False
        btnRemModAll.Enabled = False
    End Sub

    Private Function NecessityUnique(ByVal GroupID As String, ByVal ICD9 As String) As Boolean
        Dim Uniq As Boolean = True
        Dim i As Integer
        For i = 0 To dgvNecessity.RowCount - 1
            If dgvNecessity.Rows(i).Cells(0).Value = CInt(GroupID) And
            Trim(dgvNecessity.Rows(i).Cells(2).Value) = ICD9 Then
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
            Dim cn1 As New SqlConnection(connString)
            cn1.Open()
            Dim cmd1 As New SqlCommand("Select * from " &
            "DiagCodes where Code = '" & Trim(txtICD9Code.Text) & "'", cn1)
            cmd1.CommandType = Data.CommandType.Text
            Dim dr1 As SqlDataReader = cmd1.ExecuteReader
            If dr1.HasRows Then
                While dr1.Read
                    txtICD9Description.Text = dr1("Description")
                End While
            Else
                MsgBox("The code does not exist in the master file.", MsgBoxStyle.Critical, "Prolis")
                txtICD9Code.Text = ""
                txtICD9Description.Text = ""
            End If
            cn1.Close()
            cn1 = Nothing
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

    Private Sub btnAddNec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNec.Click
        If txtProfileID.Text <> "" And txtICD9Code.Text <> "" Then
            If NecessityUnique(txtProfileID.Text, txtICD9Code.Text) Then
                dgvNecessity.Rows.Add(txtProfileID.Text, dgvNecessity.RowCount + 1, txtICD9Code.Text, txtICD9Description.Text)
                SaveNecessity(Trim(txtProfileID.Text), Trim(txtICD9Code.Text))
                txtICD9Code.Text = ""
                txtICD9Description.Text = ""
                btnAddNec.Enabled = False
                btnRemAllNec.Enabled = True
                'txtICD9Code.Focus()
            Else
                MsgBox("Record is already in the list")
                txtICD9Code.Text = ""
                txtICD9Code.Focus()
            End If
            If dgvNecessity.RowCount > 0 Then _
            dgvNecessity.FirstDisplayedScrollingRowIndex = dgvNecessity.RowCount - 1
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
                ExecuteSqlProcedure("Delete from MedicalNecessity where CPT_Code = '" &
                Trim(txtCPTCode.Text) & "' and Dx_Code = '" & Trim(dgvNecessity.SelectedRows(0).Cells(2).Value) & "'")
                dgvNecessity.Rows.Remove(dgvNecessity.SelectedRows(0))
                btnRemNec.Enabled = False
                If dgvNecessity.RowCount = 0 Then btnRemAllNec.Enabled = False
            End If
            If dgvNecessity.RowCount > 0 Then _
            dgvNecessity.SelectedRows(0).Selected = False
        End If
    End Sub

    Private Sub btnRemAllNec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemAllNec.Click
        If dgvNecessity.SelectedRows.Count > 0 Then
            Dim RetVal As Integer = MsgBox("Are you sure you want to delete the entire necessity " &
            "of the displayed Group component?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
            If RetVal = vbYes Then
                ExecuteSqlProcedure("Delete from MedicalNecessity where CPT_Code = '" &
                Trim(txtCPTCode.Text) & "'")
                dgvNecessity.Rows.Clear()
                btnRemAllNec.Enabled = False
                btnRemNec.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnNecCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNecCopy.Click
        Dim TGPID As String = frmNecCopy.ShowDialog()
        If TGPID <> "" Then
            Dim cnc As New SqlConnection(connString)
            cnc.Open()
            Dim cmdc As New SqlCommand("Select ICD9 from Necessity where TGP_ID = " & Val(TGPID), cnc)
            cmdc.CommandType = Data.CommandType.Text
            Dim drc As SqlDataReader = cmdc.ExecuteReader
            If drc.HasRows Then
                While drc.Read
                    dgvNecessity.Rows.Add(txtProfileID.Text, _
                    dgvNecessity.RowCount + 1, Trim(drc("ICD9")), "")
                End While
            End If
            cnc.Close()
            cnc = Nothing
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

    Private Sub ChkActive_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkActive.GotFocus
        ChkActive.BackColor = FCOLOR
    End Sub

    Private Sub ChkActive_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ChkActive.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub ChkActive_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkActive.LostFocus
        ChkActive.BackColor = NFCOLOR
    End Sub

    Private Sub chkCBillable_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCBillable.GotFocus
        chkCBillable.BackColor = FCOLOR
    End Sub

    Private Sub chkCBillable_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles chkCBillable.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub chkCBillable_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCBillable.LostFocus
        chkCBillable.BackColor = NFCOLOR
    End Sub

    Private Sub ChkInHouse_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkInHouse.GotFocus
        ChkInHouse.BackColor = FCOLOR
    End Sub

    Private Sub ChkInHouse_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ChkInHouse.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub ChkInHouse_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkInHouse.LostFocus
        ChkInHouse.BackColor = NFCOLOR
    End Sub

    Private Sub ChkMarkable_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkMarkable.GotFocus
        ChkMarkable.BackColor = FCOLOR
    End Sub

    Private Sub ChkMarkable_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ChkMarkable.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub ChkMarkable_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkMarkable.LostFocus
        ChkMarkable.BackColor = NFCOLOR
    End Sub

    Private Sub chkPBillable_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPBillable.GotFocus
        chkPBillable.BackColor = FCOLOR
    End Sub

    Private Sub chkPBillable_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles chkPBillable.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub chkPBillable_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPBillable.LostFocus
        chkPBillable.BackColor = NFCOLOR
    End Sub

    Private Sub ChkTBillable_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkTBillable.GotFocus
        ChkTBillable.BackColor = FCOLOR
    End Sub

    Private Sub ChkTBillable_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ChkTBillable.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub ChkTBillable_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkTBillable.LostFocus
        ChkTBillable.BackColor = NFCOLOR
    End Sub

    Private Sub cmbLabs_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLabs.GotFocus
        cmbLabs.BackColor = FCOLOR
    End Sub

    Private Sub cmbLabs_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbLabs.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbLabs_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLabs.LostFocus
        cmbLabs.BackColor = NFCOLOR
    End Sub

    Private Sub cmbSex_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSex.GotFocus
        cmbSex.BackColor = FCOLOR
    End Sub

    Private Sub cmbSex_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbSex.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbSex_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSex.LostFocus
        cmbSex.BackColor = NFCOLOR
    End Sub

    Private Sub txtCompName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCompName.GotFocus
        txtCompName.BackColor = FCOLOR
    End Sub

    Private Sub txtCompName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCompName.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        ElseIf e.KeyCode = Keys.Left Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtCompName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCompName.LostFocus
        txtCompName.BackColor = NFCOLOR
    End Sub

    Private Sub txtDescription_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDescription.GotFocus
        txtDescription.BackColor = FCOLOR
    End Sub

    Private Sub txtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDescription.KeyDown
        If e.KeyCode = Keys.PageDown Then
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
            If chkEditNew.Checked = True Then  'Add mode
                If IsTGPUnique(Val(txtSearchID.Text)) = True Then
                    txtProfileID.Text = txtSearchID.Text
                    Update_Progress()
                Else
                    RetVal = MsgBox("You need to provide an unused number or simply accept the system generated one." _
                    & " Do you want to provide the unique number?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo)
                    If RetVal = vbYes Then
                        txtSearchID.Text = ""
                        txtProfileID.Text = ""
                        txtSearchID.Focus()
                    Else
                        txtSearchID.Text = GetNextTGPID()
                        txtProfileID.Text = txtSearchID.Text
                    End If
                End If
            Else
                If True = ProfileExist(Val(txtSearchID.Text)) Then
                    DisplayProfile(Val(txtSearchID.Text))

                    If Not String.IsNullOrEmpty(txtSearchID.Text) Then

                        btnReplicate.Enabled = True
                    End If

                    btnDelete.Enabled = True
                    btnSave.Enabled = True
                Else
                    MsgBox("The profile code you typed, does not exist. You may use " _
                    & "the 'Lookup' function to select the profile")
                    txtSearchID.Text = ""
                    txtProfileID.Text = ""

                    ClearForm()

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

    Private Sub txtListPrice_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtListPrice.LostFocus
        txtListPrice.BackColor = NFCOLOR
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

    Private Sub txtPrice1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice1.LostFocus
        txtPrice1.BackColor = NFCOLOR
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

    Private Sub txtPrice2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice2.LostFocus
        txtPrice2.BackColor = NFCOLOR
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

    Private Sub txtPrice3_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice3.LostFocus
        txtPrice3.BackColor = NFCOLOR
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

    Private Sub txtPrice4_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice4.LostFocus
        txtPrice4.BackColor = NFCOLOR
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

    Private Sub txtPrice5_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice5.LostFocus
        txtPrice5.BackColor = NFCOLOR
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

    Private Sub txtPrice6_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice6.LostFocus
        txtPrice6.BackColor = NFCOLOR
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

    Private Sub txtPrice7_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice7.LostFocus
        txtPrice7.BackColor = NFCOLOR
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

    Private Sub txtPrice8_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice8.LostFocus
        txtPrice8.BackColor = NFCOLOR
    End Sub

    Private Sub txtPrice9_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice9.GotFocus
        txtPrice9.BackColor = FCOLOR
    End Sub

    Private Sub txtPrice9_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPrice9.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPrice9_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice9.LostFocus
        txtPrice9.BackColor = NFCOLOR
    End Sub

    Private Sub txtListPrice_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtListPrice.Validated
        Update_Progress()
    End Sub

    Dim Replication As Boolean
    Private Sub btnReplicate_Click(sender As Object, e As EventArgs) Handles btnReplicate.Click

        Dim RetVal As Integer = MsgBox("Are you sure to Replicate the selected Record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
        If RetVal = vbYes Then
            Replication = True


            txtSearchID.Text = GetNextTGPID()
            txtProfileID.Text = txtSearchID.Text

            btnSave_Click(Nothing, Nothing)
            Replication = False
            MessageBox.Show($"Record Replicated as New ID = {txtProfileID.Text} ")
        End If
    End Sub

End Class
