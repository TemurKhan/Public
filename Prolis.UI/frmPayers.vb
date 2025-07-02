Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmPayers
    Private SearchMode As Boolean = True
    Private Sub txtInsID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPayerID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub frmPayers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CN.Open("DSN=ProlisQC;UID=sa;PWD=''")
        dtpFrom.Value = Date.Today
        dtpTo.Value = DateAdd(DateInterval.Day, 365, dtpFrom.Value)
        dgvContract.RowCount = 1
        dgvBillRules.RowCount = 1
        cmbPriceLevel.SelectedIndex = 0
        txtPhone.Mask = SystemConfig.PhoneMask
        txtFax.Mask = SystemConfig.PhoneMask
        LoadPayerTypes()
        LoadPartners()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub LoadPayerTypes()
        cmbPayerType.Items.Clear()
        Dim cnlpt As New SqlConnection(connString)
        cnlpt.Open()
        Dim cmdlpt As New SqlCommand("Select * from " &
        "Payer_Types", cnlpt)
        cmdlpt.CommandType = CommandType.Text
        Dim drlpt As SqlDataReader = cmdlpt.ExecuteReader
        If drlpt.HasRows Then
            While drlpt.Read
                cmbPayerType.Items.Add(drlpt("Code") & "-" & drlpt("TypeName"))
            End While
        End If
        cnlpt.Close()
        cnlpt = Nothing
        If cmbPayerType.Items.Count > 0 Then _
        cmbPayerType.SelectedIndex = cmbPayerType.Items.Count - 1
    End Sub
    Private Sub LoadPartners()
        lstPartners.Items.Clear()
        Dim cnlp As New SqlConnection(connString)
        cnlp.Open()
        Dim cmdlp As New SqlCommand("Select * from " &
        "Partners where Active <> 0 order by Name", cnlp)
        cmdlp.CommandType = CommandType.Text
        Dim drlp As SqlDataReader = cmdlp.ExecuteReader
        If drlp.HasRows Then
            While drlp.Read
                lstPartners.Items.Add(New MyList(drlp("Name"), drlp("ID")))
            End While
        End If
        cnlp.Close()
        cnlp = Nothing
    End Sub

    Private Sub chkEditNew_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEditNew.CheckedChanged
        If chkEditNew.Checked = False Then
            chkEditNew.Text = "Edit"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Edit.ico")
            SearchMode = True
            btnInsLook.Enabled = True
            FormClear()
        Else
            chkEditNew.Text = "New"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\New.ico")
            SearchMode = False
            btnInsLook.Enabled = False
            FormClear()
            txtPayerID.Text = NextPayerID()
        End If
    End Sub

    Private Sub FormClear()
        txtPayerID.Text = ""
        txtPayerName.Text = ""
        If cmbPayerType.Items.Count > 0 Then _
        cmbPayerType.SelectedIndex = cmbPayerType.Items.Count - 1
        chkPar.Checked = False
        txtAccount.Text = ""
        chkActive.Checked = False
        txtFedID.Text = ""
        txtPayerCode.Text = ""
        txtEligibilityCode.Text = ""
        txtNPI.Text = ""
        chkECC.Checked = False
        txtContact.Text = ""
        txtPhone.Text = ""
        txtFax.Text = ""
        txtPartNo.Text = ""
        txtDocFile.Text = ""
        txtPreAuth.Text = ""
        txtEmail.Text = ""
        txtUserName.Text = ""
        txtPassword.Text = ""
        txtWebsite.Text = ""
        txtAdd1.Text = ""
        txtAdd2.Text = ""
        txtCity.Text = ""
        txtState.Text = ""
        txtZip.Text = ""
        txtCountry.Text = ""
        txtNote.Text = ""
        cmbPriceLevel.SelectedIndex = 0
        '
        dtpFrom.Value = Date.Today
        dtpTo.Value = DateAdd(DateInterval.Day, 365, dtpFrom.Value)
        dgvContract.Rows.Clear()
        dgvContract.RowCount = 1
        dgvBillRules.Rows.Clear()
        dgvBillRules.RowCount = 1
        '
        dgvReqs.Rows.Clear()
        Dim i As Integer
        For i = 0 To lstPartners.Items.Count - 1
            lstPartners.SetItemChecked(i, False)
        Next
    End Sub

    Private Function NextPayerID() As Long
        Dim NID As Long = 1
        Dim cnpid As New SqlConnection(connString)
        cnpid.Open()
        Dim cmdpid As New SqlCommand(
        "Select max(ID) as LastID from Payers", cnpid)
        cmdpid.CommandType = CommandType.Text
        Dim drpid As SqlDataReader = cmdpid.ExecuteReader
        If drpid.HasRows Then
            While drpid.Read
                If drpid("LastID") IsNot DBNull.Value _
                Then NID = drpid("LastID") + 1
            End While
        End If
        cnpid.Close()
        cnpid = Nothing
        Return NID
    End Function

    Private Sub chkECC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkECC.CheckedChanged
        If chkECC.Checked = False Then
            chkECC.Text = "No"
            txtCommDLL.Enabled = False
        Else
            chkECC.Text = "Yes"
            txtCommDLL.Enabled = True
        End If
        txtCommDLL.Text = ""
    End Sub

    Private Sub chkPar_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPar.CheckedChanged
        If chkPar.Checked = False Then
            chkPar.Text = "No"
        Else
            chkPar.Text = "Yes"
        End If
    End Sub
    Private Sub Update_Status()
        If txtPayerID.Text <> "" And txtPayerName.Text <> "" And txtAccount.Text <> "" _
        And txtAdd1.Text <> "" And txtCity.Text <> "" And txtState.Text <> "" And
        txtZip.Text <> "" Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub DisplayPayer(ByVal PayerID As Long)
        FormClear()
        Dim cndp As New SqlConnection(connString)
        cndp.Open()
        Dim cmddp As New SqlCommand("Select * from Payers where ID = " & PayerID, cndp)
        cmddp.CommandType = CommandType.Text
        Dim drdp As SqlDataReader = cmddp.ExecuteReader
        If drdp.HasRows Then
            While drdp.Read
                txtPayerID.Text = CStr(PayerID)
                txtPayerName.Text = drdp("PayerName")
                For i As Integer = 0 To cmbPayerType.Items.Count - 1
                    If drdp("PayerTypeCode") = cmbPayerType.Items(i).ToString.Substring(0, 2) Then
                        cmbPayerType.SelectedIndex = i
                        Exit For
                    End If
                Next
                chkPar.Checked = drdp("IsPar")
                txtAccount.Text = drdp("AccountNo")
                chkActive.Checked = drdp("Active")
                If drdp("FederalId") IsNot DBNull.Value Then txtFedID.Text = drdp("FederalId")
                If drdp("PayerCode") IsNot DBNull.Value Then txtPayerCode.Text = drdp("PayerCode")
                If drdp("EligibilityCode") IsNot DBNull.Value Then txtEligibilityCode.Text = drdp("EligibilityCode")
                If drdp("NPI") IsNot DBNull.Value Then txtNPI.Text = drdp("NPI")
                chkECC.Checked = drdp("IsECC")
                If drdp("Contact") IsNot DBNull.Value Then txtContact.Text = drdp("Contact")
                If drdp("Phone") IsNot DBNull.Value Then txtPhone.Text = drdp("Phone")
                If drdp("Fax") IsNot DBNull.Value Then txtFax.Text = drdp("Fax")
                If drdp("PartNo") IsNot DBNull.Value Then txtPartNo.Text = drdp("PartNo")
                If drdp("DocFile") IsNot DBNull.Value Then txtDocFile.Text = drdp("DocFile")
                If drdp("PreAuthFile") IsNot DBNull.Value Then txtPreAuth.Text = drdp("PreAuthFile")
                If drdp("Email") IsNot DBNull.Value Then txtEmail.Text = drdp("Email")
                If drdp("UserName") IsNot DBNull.Value Then txtUserName.Text = drdp("UserName")
                If drdp("Password") IsNot DBNull.Value Then txtPassword.Text = LIC.decryptString(drdp("Password"))
                If drdp("Website") IsNot DBNull.Value Then txtWebsite.Text = drdp("Website")
                If drdp("Note") IsNot DBNull.Value Then txtNote.Text = drdp("Note")
                If drdp("Address_ID") IsNot DBNull.Value Then
                    txtAdd1.Text = GetAddress1(drdp("Address_ID"))
                    txtAdd2.Text = GetAddress2(drdp("Address_ID"))
                    txtCity.Text = GetAddressCity(drdp("Address_ID"))
                    txtState.Text = GetAddressState(drdp("Address_ID"))
                    txtZip.Text = GetAddressZip(drdp("Address_ID"))
                    txtCountry.Text = GetAddressCountry(drdp("Address_ID"))
                End If
                If drdp("ContractFrom") IsNot DBNull.Value Then dtpFrom.Value = drdp("ContractFrom")
                If drdp("ContractTo") IsNot DBNull.Value Then dtpTo.Value = drdp("ContractTo")
                If drdp("PriceLevel") IsNot DBNull.Value Then cmbPriceLevel.SelectedIndex = drdp("PriceLevel")
                btnDelete.Enabled = True
                btnSave.Enabled = True
            End While
        End If
        cndp.Close()
        cndp = Nothing
        DisplayContract(PayerID)
        DisplayPartnersAssociation(PayerID)
        DisplayBillingReqs(PayerID)
        DisplayBillRules(PayerID)
    End Sub

    Private Sub DisplayBillRules(ByVal PayerID As Long)
        dgvBillRules.Rows.Clear()
        Dim cndbr As New SqlConnection(connString)
        cndbr.Open()
        Dim cmddbr As New SqlCommand("Select * from Payer_BillRules where Payer_ID = " & PayerID & " Order by Ordinal", cndbr)
        cmddbr.CommandType = CommandType.Text
        Dim drdbr As SqlDataReader = cmddbr.ExecuteReader
        If drdbr.HasRows Then
            While drdbr.Read
                dgvBillRules.Rows.Add(System.Drawing.Image.FromFile(Application.StartupPath &
                "\Images\Eraser.ico"), drdbr("Element"), drdbr("Action"),
                drdbr("Origin"), drdbr("Target"), drdbr("Active"))
            End While
        End If
        cndbr.Close()
        cndbr = Nothing
        dgvBillRules.Rows.Add()
    End Sub

    Private Sub DisplayBillingReqs(ByVal PayerID As Long)
        dgvReqs.Rows.Clear()
        Dim cndbr As New SqlConnection(connString)
        cndbr.Open()
        Dim cmddbr As New SqlCommand("Select * from BillingRequisits", cndbr)
        cmddbr.CommandType = CommandType.Text
        Dim drdbr As SqlDataReader = cmddbr.ExecuteReader
        If drdbr.HasRows Then
            While drdbr.Read
                dgvReqs.Rows.Add(drdbr("ID"), drdbr("BillingType"),
                drdbr("Category"), drdbr("BillingRequisit"), drdbr("Required"))
            End While
        End If
        cndbr.Close()
        cndbr = Nothing
        '
        For i As Integer = 0 To dgvReqs.RowCount - 1
            Dim cndr As New SqlConnection(connString)
            cndr.Open()
            Dim cmddr As New SqlCommand("Select Required from Payer_Requisit where " &
            "Payer_ID = " & PayerID & " and Requisit_ID = " & dgvReqs.Rows(i).Cells(0).Value, cndr)
            cmddr.CommandType = CommandType.Text
            Dim drdr As SqlDataReader = cmddr.ExecuteReader
            If drdr.HasRows Then
                While drdr.Read
                    dgvReqs.Rows(i).Cells(4).Value = drdr("Required")
                End While
            End If
            cndr.Close()
            cndr = Nothing
        Next
    End Sub

    Private Sub DisplayPartnersAssociation(ByVal PayerID As Long)
        For i As Integer = 0 To lstPartners.Items.Count - 1
            lstPartners.SetItemChecked(i, False)
        Next
        Dim ItemX As MyList
        Dim cnpa As New SqlConnection(connString)
        cnpa.Open()
        Dim cmdpa As New SqlCommand("Select * from Partner_Payer where Payer_ID = " & PayerID, cnpa)
        cmdpa.CommandType = CommandType.Text
        Dim drpa As SqlDataReader = cmdpa.ExecuteReader
        If drpa.HasRows Then
            While drpa.Read
                For i As Integer = 0 To lstPartners.Items.Count - 1
                    ItemX = lstPartners.Items(i)
                    If ItemX.ItemData = drpa("Partner_ID") Then
                        lstPartners.SetItemChecked(i, True)
                    End If
                Next
            End While
        End If
        cnpa.Close()
        cnpa = Nothing
    End Sub

    Private Function PayerExists(ByVal PayerID As Long) As Boolean
        Dim Exist As Boolean = False
        Dim cnpe As New SqlConnection(connString)
        cnpe.Open()
        Dim cmdpe As New SqlCommand("Select * from Payers where ID = " & PayerID, cnpe)
        cmdpe.CommandType = CommandType.Text
        Dim drpe As SqlDataReader = cmdpe.ExecuteReader
        If drpe.HasRows Then Exist = True
        cnpe.Close()
        cnpe = Nothing
        Return Exist
    End Function

    Private Sub txtPayerID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPayerID.LostFocus
        Dim RetVal As Integer
        If txtPayerID.Text <> "" Then
            If SearchMode = True Then
                If PayerExists(Val(txtPayerID.Text)) Then
                    DisplayPayer(Val(txtPayerID.Text))
                    btnDelete.Enabled = True
                Else
                    MsgBox("The Insurance ID you typed, is not valid. You may use Look Up")
                    txtPayerID.Text = ""
                    txtPayerID.Focus()
                End If
            Else        'Adding new record
                If PayerExists(Val(txtPayerID.Text)) Then
                    RetVal = MsgBox("Duplicate ID. Either type a unique ID or accept the system assigned ID." _
                    & " Click the button 'No' to accept the system assigned ID or the button 'Yes' to type " _
                    & "an unused ID. Do you want to type the ID your self?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo)
                    If RetVal = vbYes Then
                        txtPayerID.Text = ""
                        txtPayerID.Focus()
                    Else
                        txtPayerID.Text = NextPayerID()
                    End If
                End If
            End If
        End If
        Update_Status()
    End Sub

    Private Sub txtPayerName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPayerName.LostFocus
        Update_Status()
    End Sub

    Private Sub txtAccount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccount.LostFocus
        Update_Status()
    End Sub

    Private Sub txtAdd1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAdd1.LostFocus
        Update_Status()
    End Sub

    Private Sub txtCity_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCity.LostFocus
        Update_Status()
    End Sub

    Private Sub txtState_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtState.LostFocus
        Update_Status()
    End Sub

    Private Sub txtZip_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtZip.LostFocus
        Update_Status()
    End Sub

    Private Sub SavePayer()
        If cmbPayerType Is Nothing Or cmbPayerType.SelectedItem Is Nothing Then
            Return
        End If
        Dim sSQL As String = "If Exists (Select * from Payers where ID = " & Val(txtPayerID.Text) &
        ") Update Payers set PayerName = '" & Trim(txtPayerName.Text) & "', PayerTypeCode = '" &
        cmbPayerType.SelectedItem.ToString.Substring(0, 2) & "', IsPar = '" & chkPar.Checked &
        "', AccountNo = '" & Trim(txtAccount.Text) & "', Active = '" & chkActive.Checked & "', FederalID = '" &
        SSNNeat(txtFedID.Text) & "', PayerCode = '" & Trim(txtPayerCode.Text) & "', EligibilityCode = '" &
        Trim(txtEligibilityCode.Text) & "', NPI = '" & Trim(txtNPI.Text) & "', IsECC = '" & chkECC.Checked &
        "', Contact = '" & Trim(txtContact.Text) & "', Phone = '" & PhoneNeat(txtPhone.Text) & "', Fax = '" &
        PhoneNeat(txtFax.Text) & "', PartNo = '" & Trim(txtPartNo.Text) & "', DocFile = '" & Trim(txtDocFile.Text) &
        "', PreAuthFile = '" & Trim(txtPreAuth.Text) & "', Email = '" & Trim(txtEmail.Text) & "', UserName = '" &
        Trim(txtUserName.Text) & "', Password = '" & IIf(Trim(txtPassword.Text) = "", "", LIC.encryptString(Trim(txtPassword.Text))) &
        "', Website = '" & Trim(txtWebsite.Text) & "', PriceLevel = " & cmbPriceLevel.SelectedIndex & ", Address_ID = " &
        GetAddressID(Trim(txtAdd1.Text), Trim(txtAdd2.Text), Trim(txtCity.Text), Trim(txtState.Text), Trim(txtZip.Text),
        Trim(txtCountry.Text)) & ", ContractFrom = '" & dtpFrom.Value & "', ContractTo = '" & dtpTo.Value & "', Note = '" &
        Trim(txtNote.Text) & "' where ID = " & Val(txtPayerID.Text) & " Else Insert into Payers (ID, PayerName, PayerTypeCode, IsPar, " &
        "AccountNo, Active, FederalId, PayerCode, EligibilityCode, NPI, IsECC, Contact, Phone, Fax, PartNo, DocFile, " &
        "PreAuthFile, Email, UserName, Password, Website, PriceLevel, Address_ID, ContractFrom, ContractTo, Note) Values (" &
        Trim(txtPayerID.Text) & ", '" & Trim(txtPayerName.Text) & "', '" & cmbPayerType.SelectedItem.ToString.Substring(0, 2) & "', '" &
        chkPar.Checked & "', '" & Trim(txtAccount.Text) & "', '" & chkActive.Checked & "', '" & SSNNeat(txtFedID.Text) & "', '" &
        Trim(txtPayerCode.Text) & "', '" & Trim(txtEligibilityCode.Text) & "', '" & Trim(txtNPI.Text) & "', '" & chkECC.Checked &
        "', '" & Trim(txtContact.Text) & "', '" & PhoneNeat(txtPhone.Text) & "', '" & PhoneNeat(txtFax.Text) & "', '" &
        Trim(txtPartNo.Text) & "', '" & Trim(txtDocFile.Text) & "', '" & Trim(txtPreAuth.Text) & "', '" & Trim(txtEmail.Text) & "', '" &
        Trim(txtUserName.Text) & "', '" & IIf(Trim(txtPassword.Text) <> "", LIC.encryptString(Trim(txtPassword.Text)), "") & "', '" &
        Trim(txtWebsite.Text) & "', " & cmbPriceLevel.SelectedIndex & ", " & GetAddressID(Trim(txtAdd1.Text), Trim(txtAdd2.Text),
        Trim(txtCity.Text), Trim(txtState.Text), Trim(txtZip.Text), Trim(txtCountry.Text)) & ", '" & dtpFrom.Value & "', '" &
        dtpTo.Value & "', '" & Trim(txtNote.Text) & "')"
        ExecuteSqlProcedure(sSQL)
        '
        ExecuteSqlProcedure("Delete from Payer_TGP where Payer_ID = " & Val(txtPayerID.Text))
        For i As Integer = 0 To dgvContract.RowCount - 1
            If Not dgvContract.Rows(i).Cells(1).Value Is Nothing AndAlso
            dgvContract.Rows(i).Cells(1).Value.ToString <> "" Then
                ExecuteSqlProcedure("Insert into Payer_TGP (Payer_ID, TGP_ID, Ordinal, Price, Modifier) Values (" &
                Val(txtPayerID.Text) & ", " & Val(dgvContract.Rows(i).Cells(1).Value) & ", " & i & ", " &
                Val(dgvContract.Rows(i).Cells(5).Value) & ", '" & Trim(dgvContract.Rows(i).Cells(6).Value) & "')")
            End If
        Next
        '
        SavePartnerAssociation(Val(txtPayerID.Text))
        SavePayerRequisits(Val(txtPayerID.Text))
        SaveBillRules(Val(txtPayerID.Text))
    End Sub

    Private Sub SaveBillRules(ByVal PayerID As Long)
        ExecuteSqlProcedure("Delete from Payer_BillRules where Payer_ID = " & PayerID)
        For i As Integer = 0 To dgvBillRules.RowCount - 1
            If (dgvBillRules.Rows(i).Cells(1).Value IsNot Nothing _
            AndAlso dgvBillRules.Rows(i).Cells(1).Value <> "") _
            AndAlso (dgvBillRules.Rows(i).Cells(2).Value IsNot Nothing _
            AndAlso dgvBillRules.Rows(i).Cells(2).Value <> "") _
            AndAlso dgvBillRules.Rows(i).Cells(3).Value <> "" _
            AndAlso dgvBillRules.Rows(i).Cells(4).Value <> "" Then
                ExecuteSqlProcedure("Insert into Payer_BillRules (Payer_ID, Element, Action, Origin, Target, " &
                "Active, Ordinal, Added_On, Added_By, LastEdited_On, Edited_By) Values (" & PayerID & ", '" &
                Trim(dgvBillRules.Rows(i).Cells(1).Value) & "', '" & Trim(dgvBillRules.Rows(i).Cells(2).Value) &
                "', '" & Trim(dgvBillRules.Rows(i).Cells(3).Value) & "', '" & Trim(dgvBillRules.Rows(i).Cells(4).Value) &
                "', '" & Trim(dgvBillRules.Rows(i).Cells(5).Value) & "', " & i & ", '" & Date.Now & "', " &
                ThisUser.ID & ", '" & Date.Now & "', " & ThisUser.ID & ")")
            End If
        Next
    End Sub

    Private Sub SavePayerRequisits(ByVal PayerID As Long)
        ExecuteSqlProcedure("Delete from Payer_Requisit where Payer_ID = " & PayerID)
        For i As Integer = 0 To dgvReqs.RowCount - 1
            ExecuteSqlProcedure("Insert into Payer_Requisit (Payer_ID, Requisit_ID, Required) values (" &
            PayerID & ", " & dgvReqs.Rows(i).Cells(0).Value & ", '" & dgvReqs.Rows(i).Cells(4).Value & "')")
        Next
    End Sub

    Private Sub SavePartnerAssociation(ByVal PayerID As Long)
        ExecuteSqlProcedure("Delete from Partner_Payer where Payer_ID = " & PayerID)
        If lstPartners.CheckedItems.Count > 0 Then
            Dim ItemX As MyList
            For i As Integer = 0 To lstPartners.Items.Count - 1
                If lstPartners.GetItemChecked(i) = True Then
                    ItemX = lstPartners.Items(i)
                    ExecuteSqlProcedure("Insert into Partner_Payer (Payer_ID, Partner_ID, " &
                    "Ordinal) values (" & PayerID & ", " & ItemX.ItemData & ", " & i & ")")
                End If
            Next
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtPayerID.Text <> "" And txtPayerName.Text <> "" And txtAccount.Text <> "" _
        And txtAdd1.Text <> "" And txtCity.Text <> "" And txtState.Text <> "" And
        txtZip.Text <> "" Then
            SavePayer()
            FormClear()
            If chkEditNew.Checked = True Then txtPayerID.Text = NextPayerID()
            tcPayers.SelectTab("tpInsurance")
            btnSave.Enabled = False
            btnDelete.Enabled = False
        End If
    End Sub

    Private Sub DisplayContract(ByVal PayerID As Long)
        dgvContract.Rows.Clear()
        dgvContract.RowCount = 1
        Dim cnpr As New SqlConnection(connString)
        cnpr.Open()
        Dim cmdpr As New SqlCommand("Select a.TGP_ID as ID, (Select Name from Tests where ID = a.TGP_ID " &
        "Union Select Name from Groups where ID = a.TGP_ID Union Select Name from Profiles where ID = a.TGP_ID) " &
        "as Name, (Select ComponentType from Tests where ID = a.TGP_ID Union Select ComponentType from Groups " &
        "where ID = a.TGP_ID Union Select ComponentType from Profiles where ID = a.TGP_ID) as ComponentType, " &
        "a.Modifier, a.Price from Payer_TGP a where a.Payer_ID = " & PayerID, cnpr)
        cmdpr.CommandType = CommandType.Text
        Dim drpr As SqlDataReader = cmdpr.ExecuteReader
        If drpr.HasRows Then
            While drpr.Read
                If drpr("ID") IsNot DBNull.Value Then
                    dgvContract.Rows(dgvContract.RowCount - 1).Cells(0).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath &
                    "\Images\Eraser.ico")
                    dgvContract.Rows(dgvContract.RowCount - 1).Cells(1).Value = drpr("ID")
                    dgvContract.Rows(dgvContract.RowCount - 1).Cells(3).Value = drpr("Name")
                    If drpr("ComponentType") = "T" Then
                        dgvContract.Rows(dgvContract.RowCount - 1).Cells(4).Value =
                        System.Drawing.Image.FromFile(Application.StartupPath & "\Images\test.ico")
                    ElseIf drpr("ComponentType") = "G" Then
                        dgvContract.Rows(dgvContract.RowCount - 1).Cells(4).Value =
                        System.Drawing.Image.FromFile(Application.StartupPath & "\Images\group.ico")
                    Else
                        dgvContract.Rows(dgvContract.RowCount - 1).Cells(4).Value =
                        System.Drawing.Image.FromFile(Application.StartupPath & "\Images\profile.ico")
                    End If
                    dgvContract.Rows(dgvContract.RowCount - 1).Cells(5).Value = Format(drpr("Price"), "##,##0.00")
                    If drpr("Modifier") IsNot DBNull.Value Then
                        dgvContract.Rows(dgvContract.RowCount - 1).Cells(6).Value = Trim(drpr("Modifier"))
                    Else
                        dgvContract.Rows(dgvContract.RowCount - 1).Cells(6).Value = ""
                    End If
                End If
            End While
            dgvContract.Rows.Add()
        End If
        cnpr.Close()
        cnpr = Nothing
    End Sub

    Private Sub dgvContract_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvContract.CellClick
        If e.ColumnIndex = 0 Then   'Eraser
            If dgvContract.Rows(e.RowIndex).Cells(3).Value <> "" Then
                If dgvContract.RowCount > 1 Then
                    dgvContract.Rows.RemoveAt(e.RowIndex)
                Else
                    dgvContract.Rows(e.RowIndex).Cells(0).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath &
                    "\Images\Blank.ico")
                    dgvContract.Rows(e.RowIndex).Cells(3).Value = ""
                    dgvContract.Rows(e.RowIndex).Cells(4).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath &
                    "\Images\Blank.ico")
                    dgvContract.Rows(e.RowIndex).Cells(5).Value = ""
                End If
                btnSave.Enabled = True
            End If
        ElseIf e.ColumnIndex = 2 Then
            Dim TGPID As String = frmTGPLookup.ShowDialog()
            If TGPID <> "" Then
                If ComponentInGrid(Val(TGPID)) < 1 Then
                    Dim Comps() = GetComponent(Val(TGPID))
                    '0=ID, 1=Name, 2=Type, 3=Listprice
                    If Comps(0) <> "" Then
                        dgvContract.Rows(e.RowIndex).Cells(0).Value =
                        System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Eraser.ico")
                        dgvContract.Rows(e.RowIndex).Cells(1).Value = Comps(0)
                        dgvContract.Rows(e.RowIndex).Cells(3).Value = Comps(1)
                        If Comps(2) = "T" Then
                            dgvContract.Rows(e.RowIndex).Cells(4).Value =
                            System.Drawing.Image.FromFile(Application.StartupPath &
                            "\Images\test.ico")
                        ElseIf Comps(2) = "G" Then
                            dgvContract.Rows(e.RowIndex).Cells(4).Value =
                            System.Drawing.Image.FromFile(Application.StartupPath &
                            "\Images\group.ico")
                        Else
                            dgvContract.Rows(e.RowIndex).Cells(4).Value =
                            System.Drawing.Image.FromFile(Application.StartupPath &
                            "\Images\profile.ico")
                        End If
                        dgvContract.Rows(e.RowIndex).Cells(5).Value =
                        Format(Val(Comps(2)), "##,##0.00")
                        dgvContract.Rows.Add()
                        btnSave.Enabled = True
                    End If
                Else
                    MsgBox("You are trying to enter a duplicate component. " _
                    & "Try a unused component", MsgBoxStyle.Critical, "Prolis")
                End If
            End If
        End If
    End Sub

    Private Function ComponentInGrid(ByVal TGPID As Integer) As Integer
        Dim i As Integer
        Dim TGPNo As Integer = 0
        For i = 0 To dgvContract.RowCount - 1
            If dgvContract.Rows(i).Cells(1).Value = TGPID.ToString Then TGPNo = TGPNo + 1
        Next
        ComponentInGrid = TGPNo
    End Function

    Private Sub dgvContract_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvContract.CellEndEdit
        If e.ColumnIndex = 1 Then   'ID
            If IsNumeric(dgvContract.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) = True Then
                Dim Comp() As String = GetComponent(Val(dgvContract.Rows(e.RowIndex).Cells(e.ColumnIndex).Value))
                If Comp(0) <> "" Then
                    If ComponentInGrid(Comp(0)) < 2 Then
                        dgvContract.Rows(e.RowIndex).Cells(0).Value =
                        System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Eraser.ico")
                        dgvContract.Rows(e.RowIndex).Cells(3).Value = Comp(1)
                        If Comp(2) = "T" Then
                            dgvContract.Rows(e.RowIndex).Cells(4).Value =
                            System.Drawing.Image.FromFile(Application.StartupPath & "\Images\test.ico")
                        ElseIf Comp(2) = "G" Then
                            dgvContract.Rows(e.RowIndex).Cells(4).Value =
                            System.Drawing.Image.FromFile(Application.StartupPath & "\Images\group.ico")
                        Else
                            dgvContract.Rows(e.RowIndex).Cells(4).Value =
                            System.Drawing.Image.FromFile(Application.StartupPath & "\Images\profile.ico")
                        End If
                        dgvContract.Rows(e.RowIndex).Cells(5).Value = Format(Val(Comp(3)), "##,##0.00")
                        dgvContract.Rows.Add()
                    Else
                        MsgBox("Duplicate entry. Type a valid component ID", MsgBoxStyle.Critical, "Prolis")
                        dgvContract.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
                    End If
                End If
            Else
                MsgBox("Type a valid component ID", MsgBoxStyle.Critical, "Prolis")
                dgvContract.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
            End If
        End If
    End Sub

    Private Function GetComponent(ByVal TGPID As Integer) As String()
        Dim Comp() As String = {"", "", "", ""}
        '0=ID, 1=Name, 2=Type, 3=Listprice
        Dim cngc As New SqlConnection(connString)
        cngc.Open()
        Dim cmdgc As New SqlCommand("Select ID, Name, ComponentType, " &
        "ListPrice from Tests where ID = " & TGPID & " Union Select ID, Name, " &
        "ComponentType, ListPrice from Groups where ID = " & TGPID & " Union Select " &
        "ID, Name, ComponentType, ListPrice from Profiles where ID = " & TGPID, cngc)
        cmdgc.CommandType = CommandType.Text
        Dim drgc As SqlDataReader = cmdgc.ExecuteReader
        If drgc.HasRows Then
            While drgc.Read
                Comp(0) = drgc("ID").ToString
                Comp(1) = drgc("Name")
                Comp(2) = drgc("ComponentType")
                Comp(3) = drgc("ListPrice").ToString
            End While
        End If
        cngc.Close()
        cngc = Nothing
        Return Comp
    End Function

    Private Sub btnInsLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsLook.Click
        Dim PayerInfo As String = frmPayerLookup.ShowDialog()
        If PayerInfo <> "" Then
            Dim PRS() As String = Split(PayerInfo, "|")
            DisplayPayer(Val(PRS(0)))
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If txtPayerID.Text <> "" And txtPayerName.Text <> "" Then
            If IsPayerUsed(Val(txtPayerID.Text)) Then
                MsgBox("The Payer record has been used in Accessioning. Used records " &
                "are not allowed to be deleted. If you must delete this record, remove " &
                "all references in Accessions and try again.", MsgBoxStyle.Critical, "Prolis")
            Else
                Dim RetVal As Integer = MsgBox("Are you certain to delete this record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
                If RetVal = vbYes Then
                    ExecuteSqlProcedure("Delete from Partner_Payer where Payer_ID = " & Val(txtPayerID.Text))
                    ExecuteSqlProcedure("Delete from Payer_TGP where Payer_ID = " & Val(txtPayerID.Text))
                    ExecuteSqlProcedure("Delete from Payers where ID = " & Val(txtPayerID.Text))
                    FormClear()
                    btnDelete.Enabled = False
                End If
            End If
        End If
    End Sub

    Private Function IsPayerUsed(ByVal PayerID As Long) As Boolean
        Dim Used As Boolean = False
        Dim cnpu As New SqlConnection(connString)
        cnpu.Open()
        Dim cmdpu As New SqlCommand("Select * from Req_Coverage where Payer_ID = " & PayerID, cnpu)
        cmdpu.CommandType = CommandType.Text
        Dim drpu As SqlDataReader = cmdpu.ExecuteReader
        If drpu.HasRows Then Used = True
        cnpu.Close()
        cnpu = Nothing
        Return Used
    End Function

    Private Sub dgvBillRules_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvBillRules.CellContentClick
        If e.ColumnIndex = 0 Then   'Eraser
            If dgvBillRules.Rows(e.RowIndex).Cells(1).Value <> "" And _
            dgvBillRules.Rows(e.RowIndex).Cells(2).Value <> "" Then
                If e.RowIndex < dgvBillRules.RowCount - 1 Then
                    dgvBillRules.Rows.RemoveAt(e.RowIndex)
                Else
                    dgvBillRules.Rows(e.RowIndex).Cells(0).Value = _
                    System.Drawing.Image.FromFile(Application.StartupPath & _
                    "\Images\Blank.ico")
                    dgvBillRules.Rows(e.RowIndex).Cells(1).Value = ""
                    dgvContract.Rows(e.RowIndex).Cells(2).Value = ""
                End If
                btnSave.Enabled = True
            End If
        End If
    End Sub

    Private Sub dgvBillRules_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvBillRules.CellEndEdit
        If dgvBillRules.Rows(e.RowIndex).Cells(2).Value = "BREAK" Then   'Action
            dgvBillRules.Rows(e.RowIndex).Cells(4).Value = "DEFAULT"
            'ElseIf dgvBillRules.Rows(e.RowIndex).Cells(2).Value = "REPLACE" _
            'AndAlso dgvBillRules.Rows(e.RowIndex).Cells(4).Value = "" Then  'Action
            '    MsgBox("The replacing field canot be left blank")
            'Else
            '    dgvBillRules.Rows(e.RowIndex).Cells(4).Value = ""
        End If
        If Trim(dgvBillRules.Rows(e.RowIndex).Cells(1).Value) <> "" And _
        Trim(dgvBillRules.Rows(e.RowIndex).Cells(2).Value) <> "" And _
        Trim(dgvBillRules.Rows(e.RowIndex).Cells(3).Value) <> "" And _
        Trim(dgvBillRules.Rows(e.RowIndex).Cells(4).Value) <> "" Then
            If Trim(dgvBillRules.Rows(e.RowIndex).Cells(3).Value) <> _
            Trim(dgvBillRules.Rows(e.RowIndex).Cells(4).Value) Then
                If Not IsLineDuplicate(e.RowIndex) Then
                    dgvBillRules.Rows(e.RowIndex).Cells(0).Value = _
                    System.Drawing.Image.FromFile(Application.StartupPath & _
                    "\Images\Eraser.ico")
                    '
                    If e.RowIndex = dgvBillRules.RowCount - 1 Then dgvBillRules.RowCount += 1
                Else
                    MsgBox("Duplicate rules can not be inserted.", MsgBoxStyle.Critical, "Prolis")
                    dgvBillRules.Rows(e.RowIndex).Cells(3).Value = ""
                    dgvBillRules.Rows(e.RowIndex).Cells(4).Value = ""
                End If
            Else
                MsgBox("The value of replacing component, must be different than " & _
                "that of the component to be replaced.", MsgBoxStyle.Critical, "Prolis")
                dgvBillRules.Rows(e.RowIndex).Cells(3).Value = ""
                dgvBillRules.Rows(e.RowIndex).Cells(4).Value = ""
            End If
        Else
            dgvBillRules.Rows(e.RowIndex).Cells(0).Value = _
            System.Drawing.Image.FromFile(Application.StartupPath & _
            "\Images\Blank.ico")
            'If e.RowIndex < dgvBillRules.RowCount - 1 _
            'Then dgvBillRules.Rows.RemoveAt(e.RowIndex)
        End If
        '
        If e.ColumnIndex = 3 Then
            If ValidateCompositField(dgvBillRules.Rows(e.RowIndex).Cells(3).Value) = False Then
                MsgBox("Invalid syntax for a composit field value. " & vbCrLf & _
                "Sub field value can not be null and should only " & vbCrLf & _
                "contain '0123456789' digits. The value of a " & vbCrLf & _
                "composit field can not start or end with a '|' and " & vbCrLf & _
                "two adjacent '|' characters, without a component value" & vbCrLf & _
                " in between, are not allowed.", MsgBoxStyle.Critical, "Prolis")
                dgvBillRules.Rows(e.RowIndex).Cells(3).Value = ""
                dgvBillRules.Rows(e.RowIndex).Cells(4).Value = ""
                'If e.RowIndex < dgvBillRules.RowCount - 1 _
                'Then dgvBillRules.Rows.RemoveAt(e.RowIndex)
            End If
        End If
        If e.ColumnIndex = 4 Then
            If Not (Trim(dgvBillRules.Rows(e.RowIndex).Cells(4).Value) = "DEFAULT" _
            OrElse IsNumeric(dgvBillRules.Rows(e.RowIndex).Cells(4).Value)) Then
                MsgBox("Allowed characters for this field, are '0123456789' " & _
                "and 'DEFAULT'", MsgBoxStyle.Critical, "Prolis")
                dgvBillRules.Rows(e.RowIndex).Cells(4).Value = ""
            End If
        End If
    End Sub

    Private Function ValidateCompositField(ByVal STR As String) As Boolean
        Dim Valid As Boolean = True
        If InStr(STR, "|") > 0 Then
            Dim i As Integer
            Dim VALS() As String = Split(STR, "|")
            For i = 0 To VALS.Length - 1
                If Trim(VALS(i)) = "" Or IsNumeric(Trim(VALS(i))) = False Then
                    Valid = False
                    Exit For
                End If
            Next
        Else
            If IsNumeric(Trim(STR)) = False Then
                Valid = False
            End If
        End If
        Return Valid
    End Function

    Private Function IsLineDuplicate(ByVal RowIndex As Integer) As Boolean
        Dim IsDup As Boolean = False
        For i As Integer = 0 To dgvBillRules.RowCount - 1
            If i <> RowIndex Then
                If dgvBillRules.Rows(i).Cells(1).Value <> "" AndAlso _
                dgvBillRules.Rows(i).Cells(2).Value <> "" AndAlso _
                dgvBillRules.Rows(i).Cells(3).Value <> "" AndAlso _
                dgvBillRules.Rows(i).Cells(4).Value <> "" Then
                    If dgvBillRules.Rows(i).Cells(1).Value = dgvBillRules.Rows(RowIndex).Cells(1).Value AndAlso _
                    dgvBillRules.Rows(i).Cells(2).Value = dgvBillRules.Rows(RowIndex).Cells(2).Value AndAlso _
                    FieldsEqualByContents(dgvBillRules.Rows(i).Cells(3).Value, dgvBillRules.Rows(RowIndex).Cells(3).Value) = True _
                    AndAlso dgvBillRules.Rows(i).Cells(4).Value = dgvBillRules.Rows(RowIndex).Cells(4).Value Then
                        IsDup = True
                        Exit For
                    End If
                End If
            End If
        Next
        Return IsDup
    End Function

    Private Function FieldsEqualByContents(ByVal NSField1 As String, ByVal NSField2 As String) As Boolean
        Dim FLDEQ As Boolean = True
        Dim Existings() As Integer
        Dim i As Integer
        Dim n As Integer
        Dim NS1() As String = Split(NSField1, "|")
        Dim NS2() As String = Split(NSField2, "|")
        ReDim Existings(NS1.Length - 1)
        For i = 0 To NS1.Length - 1
            For n = 0 To NS2.Length - 1
                If Trim(NS1(i)) = Trim(NS2(n)) Then
                    Existings(i) = 1
                    Exit For
                End If
            Next
        Next
        For i = 0 To Existings.Length - 1
            If Existings(i) = 0 Then
                FLDEQ = False
                Exit For
            End If
        Next
        Return FLDEQ
    End Function

    Private Sub chkActive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkActive.CheckedChanged
        If chkActive.Checked = False Then
            chkActive.Text = "No"
        Else
            chkActive.Text = "Yes"
        End If
    End Sub

    Private Sub btnRPTLookup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRPTLookup.Click
        OpenFileDialog1.FileName = txtDocFile.Text
        If System.Windows.Forms.DialogResult.OK = OpenFileDialog1.ShowDialog Then
            If OpenFileDialog1.FileName.Contains("\") Then
                txtDocFile.Text = Microsoft.VisualBasic.Mid(OpenFileDialog1.FileName, _
                InStrRev(OpenFileDialog1.FileName, "\") + 1)
            Else
                txtDocFile.Text = OpenFileDialog1.FileName
            End If
        End If
    End Sub

    Private Sub btnPreAuthLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreAuthLook.Click
        OpenFileDialog1.FileName = txtPreAuth.Text
        If System.Windows.Forms.DialogResult.OK = OpenFileDialog1.ShowDialog Then
            If OpenFileDialog1.FileName.Contains("\") Then
                txtPreAuth.Text = Microsoft.VisualBasic.Mid(OpenFileDialog1.FileName, _
                InStrRev(OpenFileDialog1.FileName, "\") + 1)
            Else
                txtPreAuth.Text = OpenFileDialog1.FileName
            End If
        End If
    End Sub
End Class
