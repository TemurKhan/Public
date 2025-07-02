Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmPartners

    Private Sub chkEditNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEditNew.Click
        If chkEditNew.Checked = True Then
            chkEditNew.Text = "New"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\New.ico")
            ClearForm()
            txtPartnerID.Text = NextPartnerID()
            btnPartLook.Enabled = False
        Else
            chkEditNew.Text = "Edit"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Edit.ico")
            ClearForm()
            btnPartLook.Enabled = True
        End If
    End Sub

    Private Sub ClearForm()
        txtPartnerID.Text = ""
        txtName.Text = ""
        txtContact.Text = ""
        txtFileNo.Text = ""
        chkActive.Checked = True
        txtCommDLL.Text = ""
        txtSubmitter.Text = ""
        txtReceiver.Text = ""
        txtAccountNo.Text = ""
        txtFedID.Text = ""
        txtPhone.Text = ""
        txtFax.Text = ""
        txtEmail.Text = ""
        txtWebsite.Text = ""
        txtUserName.Text = ""
        txtPassword.Text = ""
        txtIncoming.Text = ""
        txtOutgoing.Text = ""
        txtAdd1.Text = ""
        txtAdd2.Text = ""
        txtCity.Text = ""
        txtState.Text = ""
        txtZip.Text = ""
        txtCountry.Text = ""
        txtNote.Text = ""
        dgvPayers.Rows.Clear()
    End Sub

    Private Sub DisplayPartner(ByVal PartnerID As Long)
        ClearForm()
        Dim cndp As New SqlConnection(connString)
        cndp.Open()
        Dim cmddp As New SqlCommand("Select * from Partners where ID = " & PartnerID, cndp)
        cmddp.CommandType = CommandType.Text
        Dim drdp As SqlDataReader = cmddp.ExecuteReader
        If drdp.HasRows Then
            While drdp.Read
                txtPartnerID.Text = drdp("ID")
                txtName.Text = drdp("Name")
                chkActive.Checked = drdp("Active")
                txtFileNo.Text = drdp("FileNo")
                txtSubmitter.Text = drdp("Submitter")
                txtReceiver.Text = drdp("Receiver")
                txtAccountNo.Text = drdp("AccountNo")
                If drdp("CommDLL") IsNot DBNull.Value Then txtCommDLL.Text = drdp("CommDLL")
                If drdp("Contact") IsNot DBNull.Value Then txtContact.Text = drdp("Contact")
                If drdp("FedID") IsNot DBNull.Value Then txtFedID.Text = SSNNeat(drdp("FedID"))
                If drdp("Email") IsNot DBNull.Value Then txtEmail.Text = drdp("Email")
                If drdp("Website") IsNot DBNull.Value Then txtWebsite.Text = drdp("Website")
                If drdp("UserName") IsNot DBNull.Value Then txtUserName.Text = Trim(drdp("UserName"))
                If drdp("Password") IsNot DBNull.Value Then txtPassword.Text = LIC.decryptString(drdp("Password"))
                If drdp("IncomingFolder") IsNot DBNull.Value Then txtIncoming.Text = drdp("IncomingFolder")
                If drdp("OutgoingFolder") IsNot DBNull.Value Then txtOutgoing.Text = drdp("OutgoingFolder")
                If drdp("Phone") IsNot DBNull.Value Then txtPhone.Text = drdp("Phone")
                If drdp("Fax") IsNot DBNull.Value Then txtFax.Text = drdp("Fax")
                If drdp("Note") IsNot DBNull.Value Then txtNote.Text = Trim(drdp("Note"))
                If drdp("Address_ID") IsNot DBNull.Value Then
                    txtAdd1.Text = GetAddress1(drdp("Address_ID"))
                    txtAdd2.Text = GetAddress2(drdp("Address_ID"))
                    txtCity.Text = GetAddressCity(drdp("Address_ID"))
                    txtState.Text = GetAddressState(drdp("Address_ID"))
                    txtZip.Text = GetAddressZip(drdp("Address_ID"))
                    txtCountry.Text = GetAddressCountry(drdp("Address_ID"))
                End If
            End While
        End If
        cndp.Close()
        cndp = Nothing
        '
        Dim cngpi As New SqlConnection(connString)
        cngpi.Open()
        Dim cmdgpi As New SqlCommand("Select * from Payers where ID in (Select " &
        "Payer_ID from Partner_Payer where Partner_ID = " & PartnerID & ")", cngpi)
        cmdgpi.CommandType = CommandType.Text
        Dim drgpi As SqlDataReader = cmdgpi.ExecuteReader
        If drgpi.HasRows Then
            While drgpi.Read
                If drgpi("Address_ID") Is DBNull.Value Then
                    dgvPayers.Rows.Add(drgpi("ID"), drgpi("PayerName"),
                    drgpi("PayerCode"), drgpi("EligibilityCode"), "")
                Else
                    dgvPayers.Rows.Add(drgpi("ID"), drgpi("PayerName"),
                    drgpi("PayerCode"), drgpi("EligibilityCode"), GetAddress(drgpi("Address_ID")))
                End If
            End While
        End If
        cngpi.Close()
        cngpi = Nothing
        txtPayers.Text = dgvPayers.RowCount.ToString
    End Sub

    Private Function GetPayerInfo(ByVal PayerID As Long) As String()
        Dim Payer() As String = {"", "", "", "", ""}
        Dim cnpi As New SqlConnection(connString)
        cnpi.Open()
        Dim cmdpi As New SqlCommand("Select * from Payers where ID = " & PayerID, cnpi)
        cmdpi.CommandType = CommandType.Text
        Dim drpi As SqlDataReader = cmdpi.ExecuteReader
        If drpi.HasRows Then
            While drpi.Read
                Payer(0) = drpi("ID").ToString
                Payer(1) = Trim(drpi("PayerName"))
                If drpi("PayerCode") IsNot DBNull.Value Then Payer(2) = Trim(drpi("PayerCode"))
                If drpi("EligibilityCode") IsNot DBNull.Value Then Payer(3) = Trim(drpi("EligibilityCode"))
                If drpi("Address_ID") IsNot DBNull.Value Then Payer(4) = GetAddress(drpi("Address_ID"))
            End While
        End If
        cnpi.Close()
        cnpi = Nothing
        Return Payer
    End Function

    Private Sub btnPartLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPartLook.Click
        Dim PartnerID As String = frmPartLookUp.ShowDialog()
        If PartnerID <> "" Then
            DisplayPartner(Val(PartnerID))
            btnDelete.Enabled = True
            btnSave.Enabled = True
        End If
    End Sub

    Private Function NextPartnerID() As Long
        Dim pid As Long = 1
        Dim cnpid As New SqlConnection(connString)
        cnpid.Open()
        Dim cmdpid As New SqlCommand("Select max(ID) as LastID from Partners", cnpid)
        cmdpid.CommandType = CommandType.Text
        Dim drpid As SqlDataReader = cmdpid.ExecuteReader
        If drpid.HasRows Then
            While drpid.Read
                If drpid("LastID") IsNot DBNull.Value _
                Then pid = drpid("LastID") + 1
            End While
        End If
        cnpid.Close()
        cnpid = Nothing
        Return pid
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtPartnerID.Text <> "" And txtName.Text <> "" And txtSubmitter.Text _
        <> "" And txtReceiver.Text <> "" And txtAccountNo.Text <> "" Then
            SavePartner(Val(txtPartnerID.Text))
            ClearForm()
            If chkEditNew.Checked = True Then txtPartnerID.Text = NextPartnerID()
            btnDelete.Enabled = False
            btnSave.Enabled = False
        Else
            MsgBox("Some required data is missing")
        End If
    End Sub

    Private Sub SavePartner(ByVal PartnerID As Long)
        Dim AddressID As Long = Nothing
        If Trim(txtAdd1.Text) <> "" And Trim(txtCity.Text) <> "" And
        Trim(txtState.Text) <> "" And Trim(txtZip.Text) <> "" Then
            AddressID = GetAddressID(Trim(txtAdd1.Text), Trim(txtAdd2.Text),
            Trim(txtCity.Text), Trim(txtState.Text), Trim(txtZip.Text), Trim(txtCountry.Text))
        End If
        Dim sSQL As String = "If Exists (Select * from Partners where ID = " & PartnerID & ") Update Partners " &
        "Set Name = '" & Trim(txtName.Text) & "', Active = " & Convert.ToInt16(chkActive.Checked) & ", FileNo = '" &
        txtFileNo.Text & "', Submitter = '" & Trim(txtSubmitter.Text) & "', Receiver = '" & Trim(txtReceiver.Text) &
        "', AccountNo = '" & Trim(txtAccountNo.Text) & "', CommDLL = '" & Trim(txtCommDLL.Text) & "', Contact = '" &
        Trim(txtContact.Text) & "', FedID = '" & SSNNeat(txtFedID.Text) & "', Email = '" & Trim(txtEmail.Text) &
        "', Website = '" & Trim(txtWebsite.Text) & "', UserName = '" & Trim(txtUserName.Text) & "', Password = '" &
        LIC.encryptString(Trim(txtPassword.Text)) & "', IncomingFolder = '" & Trim(txtIncoming.Text) & "', " &
        "OutgoingFolder = '" & Trim(txtOutgoing.Text) & "', Phone = '" & PhoneNeat(txtPhone.Text) & "', Fax = '" &
        PhoneNeat(txtFax.Text) & "', Note = '" & txtNote.Text & "', Address_ID = " & AddressID & " where ID = " &
        PartnerID & " Else Insert into Partners (ID, Name, Active, FileNo, Submitter, Receiver, AccountNo, " &
        "CommDLL, Contact, FedID, Email, Website, UserName, Password, IncomingFolder, OutgoingFolder, Phone, " &
        "Fax, Note, Address_ID) Values (" & Val(txtPartnerID.Text) & ", '" & Trim(txtName.Text) & "', " &
        Convert.ToInt16(chkActive.Checked) & ", '" & txtFileNo.Text & "', '" & Trim(txtSubmitter.Text) & "', '" &
        Trim(txtReceiver.Text) & "', '" & Trim(txtAccountNo.Text) & "', '" & Trim(txtCommDLL.Text) & "', '" &
        Trim(txtContact.Text) & "', '" & SSNNeat(txtFedID.Text) & "', '" & Trim(txtEmail.Text) & "', '" &
        Trim(txtWebsite.Text) & "', '" & Trim(txtUserName.Text) & "', '" & LIC.encryptString(Trim(txtPassword.Text)) &
        "', '" & Trim(txtIncoming.Text) & "', '" & Trim(txtOutgoing.Text) & "', '" & PhoneNeat(txtPhone.Text) &
        "', '" & PhoneNeat(txtFax.Text) & "', '" & txtNote.Text & "', " & AddressID & ")"
        ExecuteSqlProcedure(sSQL)
        '
        ExecuteSqlProcedure("Delete from Partner_Payer where Partner_ID = " & PartnerID)
        For i As Integer = 0 To dgvPayers.RowCount - 1
            ExecuteSqlProcedure("Insert into Partner_Payer (Partner_ID, Payer_ID, Ordinal) " &
            "values (" & PartnerID & ", " & dgvPayers.Rows(i).Cells(0).Value & ", " & i & ")")
        Next
    End Sub

    Private Sub btnPayerLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPayerLook.Click
        Dim PayerInfo As String = frmActivePayersLookUp.ShowDialog()
        If PayerInfo <> "" Then
            Dim PRS() As String = Split(PayerInfo, "|")
            txtPayerID.Text = PRS(0)
            If PRS.Length >= 2 Then txtPayer.Text = PRS(1)
            btnAdd.Enabled = True
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If PayerInList(Val(txtPayerID.Text)) = False Then
            Dim Payer() As String = GetPayerInfo(Val(txtPayerID.Text))
            dgvPayers.Rows.Add(Payer(0), Payer(1), Payer(2), Payer(3), Payer(4))
            btnRemAll.Enabled = True
            txtPayerID.Text = ""
            txtPayer.Text = ""
            btnAdd.Enabled = False
        End If
        txtPayers.Text = dgvPayers.RowCount.ToString
    End Sub

    Private Function PayerInList(ByVal PayerID As Long) As Boolean
        Dim InList As Boolean = False
        Dim i As Integer
        For i = 0 To dgvPayers.RowCount - 1
            If dgvPayers.Rows(i).Cells(0).Value = PayerID Then
                InList = True
                Exit For
            End If
        Next
        Return InList
    End Function

    Private Sub dgvPayers_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPayers.CellClick
        If e.RowIndex <> -1 Then btnRem.Enabled = True
    End Sub

    Private Sub btnRemAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemAll.Click
        dgvPayers.Rows.Clear()
        btnRemAll.Enabled = False
        btnRem.Enabled = False
        txtPayers.Text = dgvPayers.RowCount.ToString
    End Sub

    Private Sub txtPayerID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPayerID.Validated
        If txtPayerID.Text <> "" Then
            Dim Payer() As String = GetPayerInfo(Val(txtPayerID.Text))
            If Payer(0) <> "" Then
                txtPayerID.Text = Payer(0)
                txtPayer.Text = Payer(1)
                btnAdd.Enabled = True
            Else
                MsgBox("Invalid Insurance ID")
                txtPayerID.Focus()
            End If
        Else
            txtPayer.Text = ""
        End If
    End Sub

    Private Sub chkActive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkActive.CheckedChanged
        If chkActive.Checked = True Then
            chkActive.Text = "Yes"
        Else
            chkActive.Text = "No"
        End If
    End Sub

    Private Sub txtPartnerID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPartnerID.Validated
        Dim RetVal As Integer
        If txtPartnerID.Text <> "" Then
            If chkEditNew.Checked = False Then      'Edit
                If PartnerIDUnique(Val(txtPartnerID.Text)) = False Then
                    DisplayPartner(Val(txtPartnerID.Text))
                    btnDelete.Enabled = True
                    btnSave.Enabled = True
                Else
                    MsgBox("No record found")
                    txtPartnerID.Focus()
                End If
            Else
                If PartnerIDUnique(Val(txtPartnerID.Text)) = False Then
                    RetVal = MsgBox("The ID you typed, is not unique. Either type a unique" _
                    & " ID or accept the system assigned ID. Do you want Prolis " &
                    "to generate the ID?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
                    If RetVal = vbYes Then
                        txtPartnerID.Text = NextPartnerID()
                        txtName.Focus()
                    Else
                        txtPartnerID.Text = ""
                        txtPartnerID.Focus()
                    End If
                End If
            End If
        End If
        Update_Status()
    End Sub

    Private Sub Update_Status()
        If txtPartnerID.Text <> "" And txtName.Text <> "" And txtAccountNo.Text <> "" _
        And txtSubmitter.Text <> "" And txtReceiver.Text <> "" Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If
    End Sub

    Private Function PartnerIDUnique(ByVal PartnerID As Long) As Boolean
        Dim Unique As Boolean = False
        Dim cnidu As New SqlConnection(connString)
        cnidu.Open()
        Dim cmdidu As New SqlCommand("Select * from Partners where ID = " & PartnerID, cnidu)
        cmdidu.CommandType = CommandType.Text
        Dim dridu As SqlDataReader = cmdidu.ExecuteReader
        If Not dridu.HasRows Then Unique = True
        cnidu.Close()
        cnidu = Nothing
        Return Unique
    End Function

    Private Sub btnRem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRem.Click
        If dgvPayers.SelectedRows.Count > 0 Then
            dgvPayers.Rows.RemoveAt(dgvPayers.SelectedRows(0).Index)
            btnRem.Enabled = False
            If dgvPayers.RowCount = 0 Then btnRemAll.Enabled = False
        End If
        txtPayers.Text = dgvPayers.RowCount.ToString
    End Sub

    Private Sub txtName_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.Validated
        Update_Status()
    End Sub

    Private Sub txtAccountNo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccountNo.Validated
        Update_Status()
    End Sub

    Private Sub txtReceiver_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtReceiver.Validated
        Update_Status()
    End Sub

    Private Sub txtSubmitter_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSubmitter.Validated
        Update_Status()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmPartners_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtPhone.Mask = SystemConfig.PhoneMask
        txtFax.Mask = SystemConfig.PhoneMask
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If txtPartnerID.Text <> "" AndAlso CLng(txtPartnerID.Text) > 0 Then
            Dim RetVal As Integer = MsgBox("Are you certain you want to delete this " & _
            "Clearing House record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
            If RetVal = vbYes Then
                ExecuteSqlProcedure("Delete from Partner_Payer where Partner_ID = " & _
                CLng(txtPartnerID.Text))
                ExecuteSqlProcedure("Delete from Partners where ID = " & CLng(txtPartnerID.Text))
                '
                ClearForm()
            End If
        End If
    End Sub
End Class
