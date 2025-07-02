Imports System.Windows.Forms
Imports System.data

Public Class frmOrders
    Dim Instances(SystemConfig.Max_Instances, 0) As String
    Dim InstPhlebNote(SystemConfig.Max_Instances, 2) As String
    Private Curow As Integer
    Private CurTGPRow As Integer
    Private CurrAcc As Integer
    Private PatPhRequired As Boolean = False
    Public TCode As String

    Private Sub btnNew_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.CheckedChanged
        If btnNew.Checked = False Then
            btnNew.Text = "New"
            btnNew.Image = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Images\New.ico")
            ClearForm()
            txtOrderID.Text = NextOrderID()
            btnOrderLook.Enabled = False
        Else
            btnNew.Text = "Edit"
            btnNew.Image = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Images\Edit.ico")
            ClearForm()
            btnOrderLook.Enabled = True
        End If
    End Sub

    Private Function NextOrderID() As Long
        Dim OID As Long = 1
        Dim cnts As New SqlClient.SqlConnection(connString)
        cnts.Open()
        Dim cmdts As New SqlClient.SqlCommand("Select max(ID) as LastID from Orders", cnts)
        cmdts.CommandType = CommandType.Text
        Dim drts As SqlClient.SqlDataReader = cmdts.ExecuteReader
        If drts.HasRows Then
            While drts.Read
                If drts("LastID") IsNot DBNull.Value Then _
                OID = drts("LastID") + 1
            End While
        End If
        cnts.Close()
        cnts = Nothing
        Return OID
    End Function

    Private Sub ClearForm()
        txtOrderID.Text = ""
        dtpOrderDate.Value = Date.Today
        'txtDischargeDate.Text = ""
        ConfigureDateTimePicker(dtpDischargeDate)
        chkActive.Checked = True
        dtpStartDate.Value = Date.Today
        For i As Integer = 0 To chklstDays.Items.Count - 1
            chklstDays.SetItemChecked(i, False)
        Next
        txtWorkCmnt.Text = ""
        lblGeneral.BackColor = Color.PeachPuff
        '
        ClearOrderer()
        '
        ClearPatient()
        '
        ClearPrimary()
        '
        Update_Billing_Status()
        '
        txtMaxCount.Text = ""
        cmbInterval.SelectedIndex = 1
        lblEMRReqd.ForeColor = Color.DarkBlue
        dgvTGPs.Rows.Clear()
        lblOrders.BackColor = Color.PeachPuff
        UpdateOrderProgress()
    End Sub

    Private Sub ClearOrderer()
        txtOrdID.Text = ""
        PatPhRequired = False
        cmbPrimResp.SelectedIndex = 0
        lblOrderer.BackColor = Color.PeachPuff
        btnRemProv.Enabled = False
        txtOrdName.Text = ""
        txtOrdAddress.Text = ""
        txtOrdCSZ.Text = ""
        txtCountry.Text = ""
        'txtEMRNo.Text = ""
        txtOrdPhone.Text = ""
        txtOrdFax.Text = ""
        txtProvEmail.Text = ""
        lstProviders.Items.Clear()
        If Not frmProviderAlert Is Nothing Then frmProviderAlert.Close()
    End Sub

    Private Function UpdatePatient(ByVal PatientID As String, ByVal LName As String,
        ByVal FName As String, ByVal MName As String, ByVal Sex As String,
        ByVal DOB As Date, ByVal HPhone As String, ByVal Email As String) As String
        Dim sSQL As String = ""
        If PatientID = "" Then
            sSQL = "Select * from Patients where LastName = '" & Trim(LName) &
            "' and FirstName = '" & Trim(FName) & "' and Sex = '" & Trim(Sex) &
            "' and DOB = '" & DOB & "'"
        Else
            sSQL = "Select * from Patients where ID = " & PatientID
        End If
        Dim cnts As New SqlClient.SqlConnection(connString)
        cnts.Open()
        Dim cmdts As New SqlClient.SqlCommand(sSQL, cnts)
        cmdts.CommandType = Data.CommandType.Text
        Dim drts As SqlClient.SqlDataReader = cmdts.ExecuteReader
        If drts.HasRows Then
            While drts.Read
                PatientID = drts("ID")
            End While
        Else
            PatientID = NextPatientID()
        End If
        cnts.Close()
        cnts = Nothing
        '
        Dim AddressID As Long = -1
        If Trim(txtPatAdr1.Text) <> "" And Trim(txtPatCity.Text) <> "" _
        And Trim(txtPatState.Text) <> "" And Trim(txtPatZip.Text) <> "" Then
            AddressID = GetAddressID(Trim(txtPatAdr1.Text), Trim(txtPatAdr2.Text),
            Trim(txtPatCity.Text), Trim(txtPatState.Text), Trim(txtPatZip.Text), Trim(txtPatCountry.Text))
        End If
        ExecuteSqlProcedure("If Exists (Select * from Patients where ID = " & PatientID &
        ") Update Patients set LastName = '" & Trim(LName) & "', FirstName = '" &
        Trim(FName) & "', MiddleName = '" & Trim(MName) & "', Sex = '" & Trim(Sex) &
        "', DOB = '" & DOB & "', SSN = '" & SSNNeat(txtSSN.Text) & "', Address_ID = " &
        AddressID & ", HomePhone = '" & PhoneNeat(txtPatHPhone.Text) & "', Email = '" &
        Trim(txtPatEmail.Text) & "' where ID = " & PatientID & " Else insert into " &
        "Patients (ID, LastName, FirstName, MiddleName, Sex, DOB, SSN, Address_ID, " &
        "HomePhone, Email) values (" & PatientID & ", '" & Trim(LName) & "', '" &
        Trim(FName) & "', '" & Trim(MName) & "', '" & Trim(Sex) & "', '" & DOB &
        "', '" & SSNNeat(txtSSN.Text) & "', " & AddressID & ", '" &
        PhoneNeat(txtPatHPhone.Text) & "', '" & Trim(txtPatEmail.Text) & "')")
        '
        If cmbPIns.SelectedIndex <> -1 And txtPInsID.Text <> "" _
        And cmbPRelation.SelectedIndex <> -1 Then
            Dim ItemX As MyList = cmbPIns.SelectedItem
            UpdatePatPCoverage(PatientID, ItemX.ItemData, Trim(txtPInsID.Text),
            Trim(txtPGroup.Text), cmbPRelation.SelectedIndex, txtPSubID.Text, Nothing, Nothing)
        End If
        Return PatientID
    End Function

    Private Sub Update_Provider_Status()
        Dim Seld As Boolean = False
        For i As Integer = 0 To lstProviders.Items.Count - 1
            If lstProviders.GetItemChecked(i) = True Then
                Seld = True
                Exit For
            End If
        Next
        If txtOrdID.Text <> "" And Seld = True Then
            lblOrderer.BackColor = Color.PaleGreen
        Else
            lblOrderer.BackColor = Color.PeachPuff
        End If
    End Sub

    Private Sub Update_Billing_Status()
        If chkSvcGratis.Checked = False Then
            If (cmbPrimResp.SelectedIndex = 0 And txtOrdID.Text <> "") _
            Or (cmbPrimResp.SelectedIndex = 2 And txtPatientID.Text <> "") _
            Or (cmbPrimResp.SelectedIndex = 1 And cmbPIns.SelectedIndex <> -1 _
            And txtPInsID.Text <> "" And cmbPRelation.SelectedIndex = 0) _
            Or (cmbPrimResp.SelectedIndex = 1 And cmbPIns.SelectedIndex <> -1 _
            And txtPInsID.Text <> "" And cmbPRelation.SelectedIndex > 0 And
            txtPSubID.Text <> "") Then 'Orderer or patient
                lblBilling.BackColor = Color.PaleGreen
            Else
                lblBilling.BackColor = Color.PeachPuff
            End If
        Else
            lblBilling.BackColor = Color.PaleGreen
        End If
    End Sub

    Private Sub ClearPatient()
        txtPatientID.Text = ""
        btnRemPat.Enabled = False
        lblPatient.BackColor = Color.PeachPuff
        txtLName.Text = ""
        txtFName.Text = ""
        cmbSex.SelectedIndex = -1
        txtDOB.Text = ""
        txtSSN.Text = ""
        txtMName.Text = ""
        txtPatAdr1.Text = ""
        txtPatAdr2.Text = ""
        txtPatCity.Text = ""
        txtPatState.Text = ""
        txtPatZip.Text = ""
        txtPatEmail.Text = ""
        txtPatHPhone.Text = ""
        txtEMRNo.Text = ""
        lblHPhone.ForeColor = Color.DarkBlue
        chkNeedFast.Checked = False
        ClearDxs()
        lblDxs.ForeColor = Color.DarkBlue
    End Sub

    Private Sub UpdatePrimResp(ByVal BillType As Integer)
        cmbPrimResp.SelectedIndex = BillType
        Update_Billing_Status()
    End Sub

    Private Sub PopulateInsurances()
        cmbPIns.Items.Clear()
        Dim cnts As New SqlClient.SqlConnection(connString)
        cnts.Open()
        Dim cmdts As New SqlClient.SqlCommand("Select * from Payers order by PayerName", cnts)
        cmdts.CommandType = Data.CommandType.Text
        Dim drts As SqlClient.SqlDataReader = cmdts.ExecuteReader
        If drts.HasRows Then
            While drts.Read
                cmbPIns.Items.Add(New MyList(drts("PayerName"), drts("ID")))
            End While
        End If
        cnts.Close()
        cnts = Nothing
    End Sub

    Private Sub UpdateOrderProgress()
        If lblGeneral.BackColor = Color.PaleGreen _
        And lblOrderer.BackColor = Color.PaleGreen And lblPatient.BackColor = Color.PaleGreen _
        And lblOrders.BackColor = Color.PaleGreen And lblBilling.BackColor = Color.PaleGreen Then
            btnSave.Enabled = True
            If btnNew.Checked = True Then
                btnDelete.Enabled = True
            Else
                btnDelete.Enabled = False
            End If
        Else
            btnSave.Enabled = False
            btnDelete.Enabled = False
        End If
    End Sub

    Private Sub txtOrderID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtOrderID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtOrdID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtOrdID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtPatientID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPatientID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtRptRcptID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Numerals(sender, e)
    End Sub

    Friend Sub DisplayProvider(ByVal ID As Long)
        Dim Provider As String = ""
        Dim ItemX As MyList
        Dim cndp As New SqlClient.SqlConnection(connString)
        cndp.Open()
        Dim cmddp As New SqlClient.SqlCommand("Select " &
        "* from Providers where ID = " & ID, cndp)
        cmddp.CommandType = CommandType.Text
        Dim drdp As SqlClient.SqlDataReader = cmddp.ExecuteReader
        If drdp.HasRows Then
            While drdp.Read
                txtOrdID.Text = ID
                btnRemProv.Enabled = True
                If drdp("IsIndividual") <> 0 Then
                    If drdp("Degree") IsNot DBNull.Value Then
                        Provider = Trim(drdp("LastName_BSN")) & ", " &
                        Trim(drdp("FirstName")) & " " & Trim(drdp("Degree"))
                    Else
                        Provider = Trim(drdp("LastName_BSN")) &
                        ", " & Trim(drdp("FirstName"))
                    End If
                Else
                    Provider = drdp("LastName_BSN")
                End If
                txtOrdName.Text = Provider
                If drdp("Address_ID") IsNot DBNull.Value Then
                    txtOrdAddress.Text = GetAddressLines(drdp("Address_ID"))
                    txtOrdCSZ.Text = GetAddressCSZ(drdp("Address_ID"))
                    txtCountry.Text = GetAddressCountry(drdp("Address_ID"))
                End If
                If drdp("Phone") IsNot DBNull.Value Then txtOrdPhone.Text = drdp("Phone")
                If drdp("Fax") IsNot DBNull.Value Then txtOrdFax.Text = drdp("Fax")
                If drdp("Email") IsNot DBNull.Value Then txtProvEmail.Text = drdp("Email")
                lblOrderer.BackColor = Color.PaleGreen
                If drdp("EMRNoRequired") IsNot DBNull.Value _
                AndAlso drdp("EMRNoRequired") = True Then
                    lblEMRReqd.ForeColor = Color.Red
                Else
                    lblEMRReqd.ForeColor = Color.DarkBlue
                End If
                cmbPrimResp.SelectedIndex = drdp("DefaultBilling")
                PatPhRequired = drdp("PatPhRequired")
                '
            End While
        End If
        cndp.Close()
        cndp = Nothing
        '
        lstProviders.Items.Clear()
        Dim cndp1 As New SqlClient.SqlConnection(connString)
        cndp1.Open()
        Dim cmddp1 As New SqlClient.SqlCommand("Select * from " &
        "Providers where ID in (Select Provider_ID from Clinic_Provider " &
        "where Clinic_ID = " & ID & ") order by LastName_BSN", cndp1)
        cmddp1.CommandType = CommandType.Text
        Dim drdp1 As SqlClient.SqlDataReader = cmddp1.ExecuteReader
        If drdp1.HasRows Then
            While drdp1.Read
                If drdp1("IsIndividual") <> 0 Then
                    If drdp1("Degree") IsNot DBNull.Value Then
                        Provider = Trim(drdp1("LastName_BSN")) & ", " &
                        Trim(drdp1("FirstName")) & " " & Trim(drdp1("Degree"))
                    Else
                        Provider = Trim(drdp1("LastName_BSN")) & ", " &
                        Trim(drdp1("FirstName"))
                    End If
                Else
                    Provider = drdp1("LastName_BSN")
                End If
                lstProviders.Items.Add(New MyList(Provider, drdp1("ID")))
            End While
        Else
            lstProviders.Items.Add(New MyList(txtOrdName.Text, ID))
        End If
        cndp1.Close()
        cndp1 = Nothing
        '
        If btnNew.Checked = False Then  'New Accession
            lstProviders.SetItemChecked(0, True)
            ItemX = lstProviders.CheckedItems(0)
            Dim Alert As String = GetProviderAccAlert(ItemX.ItemData)
            If Alert <> "" Then
                DisplayProviderAlert(Alert)
            Else
                If Not frmProviderAlert Is Nothing Then frmProviderAlert.Close()
            End If
            lblOrderer.BackColor = Color.PaleGreen
        Else
            Dim AttenderID As String = GetAttenderID(Val(txtOrderID.Text))
            If AttenderID <> "" Then
                For i As Integer = 0 To lstProviders.Items.Count - 1
                    ItemX = lstProviders.Items(i)
                    If ItemX.ItemData = AttenderID Then
                        lstProviders.SetItemChecked(i, True)
                        lblOrderer.BackColor = Color.PaleGreen
                        Exit For
                    End If
                Next
            Else
                lblOrderer.BackColor = Color.PeachPuff
            End If
        End If
        Update_Billing_Status()
    End Sub

    Private Function GetPhlebotomist(ByVal ProvID As Long) As String
        Dim Phleb As String = ""
        Dim cndp As New SqlClient.SqlConnection(connString)
        cndp.Open()
        Dim cmddp As New SqlClient.SqlCommand("Select Phlebotomist " &
        "from Routes where ID in (Select Route_ID from Providers where " &
        "ID = " & ProvID & ")", cndp)
        cmddp.CommandType = CommandType.Text
        Dim drdp As SqlClient.SqlDataReader = cmddp.ExecuteReader
        If drdp.HasRows Then
            While drdp.Read
                Phleb = drdp("Phlebotomist")
            End While
        End If
        cndp.Close()
        cndp = Nothing
        Return Trim(Phleb)
    End Function

    Private Sub DisplayProviderAlert(ByVal Alert As String)
        frmProviderAlert.txtAlert.Rtf = Alert
        frmProviderAlert.Show()
        frmProviderAlert.MdiParent = frmDashboard
        frmProviderAlert.TopMost = True
    End Sub

    Private Function GetProviderAccAlert(ByVal ProviderID As Long) As String
        Dim Alert As String = ""
        Dim cndp As New SqlClient.SqlConnection(connString)
        cndp.Open()
        Dim cmddp As New SqlClient.SqlCommand("Select Alert, " &
        "Alert_Acc from Providers where ID = " & ProviderID, cndp)
        cmddp.CommandType = CommandType.Text
        Dim drdp As SqlClient.SqlDataReader = cmddp.ExecuteReader
        If drdp.HasRows Then
            While drdp.Read
                If drdp("Alert") IsNot DBNull.Value _
                AndAlso drdp("Alert_Acc") <> 0 Then _
                Alert = drdp("Alert")
            End While
        End If
        cndp.Close()
        cndp = Nothing
        Return Alert
    End Function

    Private Function GetAttenderID(ByVal OrderID As Long) As String
        Dim AttenderID As String = ""
        Dim cnaid As New SqlClient.SqlConnection(connString)
        cnaid.Open()
        Dim cmdaid As New SqlClient.SqlCommand("Select " &
        "AttendingProvider_ID from Orders where ID = " & OrderID, cnaid)
        cmdaid.CommandType = CommandType.Text
        Dim draid As SqlClient.SqlDataReader = cmdaid.ExecuteReader
        If draid.HasRows Then
            While draid.Read
                AttenderID = draid("AttendingProvider_ID").ToString
            End While
        End If
        cnaid.Close()
        cnaid = Nothing
        Return AttenderID
    End Function

    Private Sub txtOrdID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOrdID.Validated
        If txtOrdID.Text = "" Then
            ClearOrderer()
            If txtPatientID.Text = "" Then
                cmbPrimResp.SelectedIndex = -1
            Else
                cmbPrimResp.SelectedIndex = 1
            End If
        Else
            'ClearOrderer()
            Dim cndp As New SqlClient.SqlConnection(connString)
            cndp.Open()
            Dim cmddp As New SqlClient.SqlCommand("Select * from " &
            "Providers where ID = " & Val(txtOrdID.Text), cndp)
            cmddp.CommandType = CommandType.Text
            Dim drdp As SqlClient.SqlDataReader = cmddp.ExecuteReader
            If drdp.HasRows Then
                DisplayProvider(Val(txtOrdID.Text))
                cmbPrimResp.SelectedIndex = 0
            Else
                MsgBox("Invalid ID")
                txtOrdID.Text = ""
                'ClearOrderer()
                txtOrdID.Focus()
            End If
            cndp.Close()
            cndp = Nothing
        End If
        'Update_Provider_Status()
        Update_Provider_Status()
        UpdateOrderProgress()
    End Sub

    Private Sub btnOrdLookup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOrdLookup.Click
        Dim ProvID As String = frmProviderLookup.ShowDialog()
        If ProvID <> "" Then
            ClearOrderer()
            DisplayProvider(Val(ProvID))
            cmbPrimResp.SelectedIndex = 0
        End If
        'Update_Provider_Status()
        Update_Billing_Status()
        UpdateOrderProgress()
    End Sub

    Private Sub txtOrderID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOrderID.Validated
        Dim RetVal As Integer
        If txtOrderID.Text <> "" Then
            If btnNew.Checked = False Then              'Add new mode
                If Not IsUniqueOrder(Val(txtOrderID.Text)) Then
                    RetVal = MsgBox("The Order ID you typed is not unique. Either type a unique " _
                    & "(unused) value or simply accept the system assigned value by clicking 'No' button." _
                    & " Do you want to type the unique value yourself?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo)
                    If RetVal = vbYes Then
                        txtOrderID.Text = ""
                        txtOrderID.Focus()
                    Else
                        txtOrderID.Text = NextOrderID()
                    End If
                End If
            Else    'Edit mode
                If Not IsUniqueOrder(Val(txtOrderID.Text)) Then
                    DisplayOrderRecord(Val(txtOrderID.Text))
                Else
                    MsgBox("The Order ID you typed, does not exit in the system. Try again with a valid ID or use search", MsgBoxStyle.Critical)
                    txtOrderID.Text = ""
                    txtOrderID.Focus()
                End If
            End If
        End If
        UpdateGeneralStatus()
        UpdateOrderProgress()
    End Sub

    Private Function IsUniqueOrder(ByVal OrderID As Long) As Boolean
        Dim IsU As Boolean = False
        Try
            Dim cndp As New SqlClient.SqlConnection(connString)
            cndp.Open()
            Dim cmddp As New SqlClient.SqlCommand("Select " &
            "ID from Orders where ID = " & OrderID, cndp)
            cmddp.CommandType = CommandType.Text
            Dim drdp As SqlClient.SqlDataReader = cmddp.ExecuteReader
            If drdp.HasRows Then
                IsU = False
            Else
                IsU = True
            End If
            cndp.Close()
            cndp = Nothing
        Catch Ex As Exception
        End Try
        Return IsU
    End Function

    Private Sub UpdateGeneralStatus()
        If txtOrderID.Text <> "" And chklstDays.CheckedItems.Count > 0 Then
            lblGeneral.BackColor = Color.PaleGreen
        Else
            lblGeneral.BackColor = Color.PeachPuff
        End If
    End Sub

    Friend Sub DisplayOrderRecord(ByVal OrderID As Long)
        ClearForm()
        Dim AgencyInfo() As String
        Dim cndp As New SqlClient.SqlConnection(connString)
        cndp.Open()
        Dim cmddp As New SqlClient.SqlCommand("Select * " &
        "from Orders where ID = " & OrderID, cndp)
        cmddp.CommandType = CommandType.Text
        Dim drdp As SqlClient.SqlDataReader = cmddp.ExecuteReader
        If drdp.HasRows Then
            While drdp.Read
                txtOrderID.Text = drdp("ID")
                dtpOrderDate.Value = drdp("OrderDate")
                If drdp("CompleteDate") IsNot DBNull.Value Then
                    'txtDischargeDate.Text = Format(drdp("CompleteDate"), SystemConfig.DateFormat)
                    dtpDischargeDate.CustomFormat = SystemConfig.DateFormat
                    dtpDischargeDate.Value = Format(drdp("CompleteDate"), SystemConfig.DateFormat)
                Else
                    ClearDateTimePicker(dtpDischargeDate)
                End If
                chkInfiniteTimed.Checked = drdp("InfiniteTimed")
                chkActive.Checked = drdp("Active")
                If drdp("Agency_ID") IsNot DBNull.Value Then
                    AgencyInfo = GetAgencyInfo(drdp("Agency_ID"))
                    txtAgencyID.Text = AgencyInfo(0)
                    txtAgency.Text = AgencyInfo(1)
                    txtAgencyContact.Text = AgencyInfo(2)
                    txtAgencyPhone.Text = AgencyInfo(3)
                    txtAgencyAddress.Text = AgencyInfo(4)
                End If
                If drdp("TestDays") IsNot DBNull.Value Then
                    For i As Integer = 0 To chklstDays.Items.Count - 1
                        If InStr(drdp("TestDays"), chklstDays.Items(i).ToString.Substring(0, 3)) > 0 Then
                            chklstDays.SetItemChecked(i, True)
                            lblGeneral.BackColor = Color.PaleGreen
                        End If
                    Next
                End If
                If drdp("WorkCmnt") IsNot DBNull.Value Then _
                txtWorkCmnt.Text = drdp("WorkCmnt")
                '
                DisplayProvider(drdp("OrderingProvider_ID"))
                If drdp("AttendingProvider_ID") IsNot DBNull.Value Then _
                DisplayAssociation(drdp("AttendingProvider_ID"))
                If txtOrdID.Text <> "" And (lstProviders.Items.Count > 0 AndAlso
                lstProviders.CheckedItems.Count > 0) Then
                    lblOrderer.BackColor = Color.PaleGreen
                Else
                    lblOrderer.BackColor = Color.PeachPuff
                End If
                '
                DisplayPatient(drdp("Patient_ID"))
                'lblPatient.BackColor = Color.PaleGreen
                chkNeedFast.Checked = drdp("Fasting")
                txtEMRNo.Text = GetEMRNo(drdp("OrderingProvider_ID"), drdp("Patient_ID"))
                '
                DisplayDxs(drdp("ID"))
                DisplayOrders(drdp("ID"))
                '
                If drdp("BillingType_ID") = 1 Then _
                DisplayPrimeIns(drdp("Patient_ID"))
                UpdatePrimResp(drdp("BillingType_ID"))
            End While
        End If
        cndp.Close()
        cndp = Nothing
    End Sub

    Private Function GetEMRNo(ByVal ProviderID As Long, ByVal PatientID As Long) As String
        Dim EMRNo As String = ""
        Dim cndp As New SqlClient.SqlConnection(connString)
        cndp.Open()
        Dim cmddp As New SqlClient.SqlCommand("Select * from Client_Patient " &
        "where Provider_ID = " & ProviderID & " and Patient_ID = " & PatientID, cndp)
        cmddp.CommandType = CommandType.Text
        Dim drdp As SqlClient.SqlDataReader = cmddp.ExecuteReader
        If drdp.HasRows Then
            While drdp.Read
                If drdp("EMRNo") IsNot DBNull.Value _
                Then EMRNo = Trim(drdp("EMRNo"))
            End While
        End If
        cndp.Close()
        cndp = Nothing
        Return EMRNo
    End Function

    Private Function GetAgencyInfo(ByVal AgencyID As Long) As String()
        Dim AgencyInfo() As String = {"", "", "", "", ""}
        Dim cndp As New SqlClient.SqlConnection(connString)
        cndp.Open()
        Dim cmddp As New SqlClient.SqlCommand("Select * from Agencies where ID = " & AgencyID, cndp)
        cmddp.CommandType = CommandType.Text
        Dim drdp As SqlClient.SqlDataReader = cmddp.ExecuteReader
        If drdp.HasRows Then
            While drdp.Read
                AgencyInfo(0) = drdp("ID").ToString
                AgencyInfo(1) = Trim(drdp("AgencyName"))
                If drdp("Contact") IsNot DBNull.Value _
                Then AgencyInfo(2) = Trim(drdp("Contact"))
                If drdp("Phone") IsNot DBNull.Value _
                Then AgencyInfo(3) = drdp("Phone")
                If drdp("Address_ID") IsNot DBNull.Value _
                Then AgencyInfo(4) = GetAddress(drdp("Address_ID"))
            End While
        End If
        cndp.Close()
        cndp = Nothing
        Return AgencyInfo
    End Function

    Private Sub DisplayOrders(ByVal OrderID As Long)
        dgvTGPs.Rows.Clear()
        Dim EndDate As String = ""
        Dim cndp As New SqlClient.SqlConnection(connString)
        cndp.Open()
        Dim cmddp As New SqlClient.SqlCommand("Select * " &
        "from Order_TGP where Order_ID = " & OrderID, cndp)
        cmddp.CommandType = CommandType.Text
        Dim drdp As SqlClient.SqlDataReader = cmddp.ExecuteReader
        If drdp.HasRows Then
            While drdp.Read
                If drdp("EndDate") IsNot DBNull.Value _
                AndAlso IsDate(drdp("EndDate")) Then
                    EndDate = Format(drdp("EndDate"), SystemConfig.DateFormat)
                Else
                    EndDate = ""
                End If
                dgvTGPs.Rows.Add(Nothing, drdp("TGP_ID"),
                GetTGPName(drdp("TGP_ID")), Format(drdp("StartDate"),
                SystemConfig.DateFormat), drdp("Interval"),
                drdp("QTY"), drdp("MaxCount"), EndDate)
            End While
            lblOrders.BackColor = Color.PaleGreen
        End If
        cndp.Close()
        cndp = Nothing
    End Sub

    Private Sub DisplayPrimeIns(ByVal PatientID As Long)
        Dim ItemX As MyList
        Dim cndp As New SqlClient.SqlConnection(connString)
        cndp.Open()
        Dim cmddp As New SqlClient.SqlCommand("Select * from " &
        "Coverages where Preference = 'P' and Patient_ID = " _
        & PatientID, cndp)
        cmddp.CommandType = CommandType.Text
        Dim drdp As SqlClient.SqlDataReader = cmddp.ExecuteReader
        If drdp.HasRows Then
            While drdp.Read
                For i As Integer = 0 To cmbPIns.Items.Count - 1
                    ItemX = cmbPIns.Items(i)
                    If ItemX.ItemData = drdp("Insurance_ID") Then
                        cmbPIns.SelectedIndex = i
                        Exit For
                    End If
                Next
                txtPGroup.Text = drdp("GroupNo")
                txtPInsID.Text = drdp("PolicyNo")
                cmbPRelation.SelectedIndex = drdp("Relation")
                If drdp("Copayment") IsNot DBNull.Value Then
                    txtPCopay.Text = Format(drdp("Copayment"), "##,##0.00")
                Else
                    txtPCopay.Text = ""
                End If
                If drdp("Relation") <> 0 Then DisplayPrimInsured(Val(txtPatientID.Text))
            End While
        End If
        cndp.Close()
        cndp = Nothing
    End Sub

    Friend Sub DisplayPatient(ByVal PatientID As Long)
        ClearPatient()
        Dim cndp As New SqlClient.SqlConnection(connString)
        cndp.Open()
        Dim cmddp As New SqlClient.SqlCommand("Select * from Patients where ID = " & PatientID, cndp)
        cmddp.CommandType = CommandType.Text
        Dim drdp As SqlClient.SqlDataReader = cmddp.ExecuteReader
        If drdp.HasRows Then
            While drdp.Read
                txtPatientID.Text = drdp("ID")
                btnRemPat.Enabled = True
                txtLName.Text = drdp("LastName")
                txtFName.Text = drdp("FirstName")
                txtMName.Text = drdp("MiddleName")
                For i As Integer = 0 To cmbSex.Items.Count - 1
                    If cmbSex.Items(i).ToString.Substring(0, 1) = drdp("Sex") Then
                        cmbSex.SelectedIndex = i
                        Exit For
                    End If
                Next
                txtDOB.Text = Format(drdp("DOB"), SystemConfig.DateFormat)
                If drdp("SSN") IsNot DBNull.Value Then txtSSN.Text = drdp("SSN")
                lblPatient.BackColor = Color.PaleGreen
                If drdp("Address_ID") IsNot DBNull.Value Then
                    txtPatAdr1.Text = GetAddress1(drdp("Address_ID"))
                    txtPatAdr2.Text = GetAddress2(drdp("Address_ID"))
                    txtPatCity.Text = GetAddressCity(drdp("Address_ID"))
                    txtPatState.Text = GetAddressState(drdp("Address_ID"))
                    txtPatZip.Text = GetAddressZip(drdp("Address_ID"))
                    txtPatCountry.Text = GetAddressCountry(drdp("Address_ID"))
                End If
                If drdp("Email") IsNot DBNull.Value Then txtPatEmail.Text = drdp("Email")
                If drdp("HomePhone") IsNot DBNull.Value Then txtPatHPhone.Text = drdp("HomePhone")
            End While
        End If
        cndp.Close()
        cndp = Nothing
    End Sub

    Private Sub DisplayDxs(ByVal OrderID As Long)
        Dim i As Integer = 0
        Dim cndp As New SqlClient.SqlConnection(connString)
        cndp.Open()
        Dim cmddp As New SqlClient.SqlCommand("Select Dx_Code from " &
        "Order_Dx where Order_ID = " & OrderID & " order by Ordinal", cndp)
        cmddp.CommandType = CommandType.Text
        Dim drdp As SqlClient.SqlDataReader = cmddp.ExecuteReader
        If drdp.HasRows Then
            While drdp.Read
                If i = dgvDxs.RowCount - 1 Then dgvDxs.RowCount += 1
                dgvDxs.Rows(i).Cells(0).Value = Trim(drdp("Dx_Code"))
                i += 1
            End While
        End If
        cndp.Close()
        cndp = Nothing
    End Sub

    Private Sub chkActive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkActive.CheckedChanged
        If chkActive.Checked = True Then
            chkActive.Text = "Yes"
        Else
            chkActive.Text = "No"
        End If
    End Sub

    Private Sub DisplayAssociation(ByVal AttendID As Long)
        Dim i As Integer
        Dim ItemX As MyList
        For i = 0 To lstProviders.Items.Count - 1
            lstProviders.SetItemChecked(i, False)
            ItemX = lstProviders.Items(i)
            If AttendID = ItemX.ItemData Then
                lstProviders.SetItemChecked(i, True)
            End If
        Next
    End Sub

    Private Sub txtPatientID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatientID.Validated
        If txtPatientID.Text <> "" Then
            Dim PatientID As Long = Val(txtPatientID.Text)
            Dim cndp As New SqlClient.SqlConnection(connString)
            cndp.Open()
            Dim cmddp As New SqlClient.SqlCommand("Select * from Patients where ID = " & PatientID, cndp)
            cmddp.CommandType = CommandType.Text
            Dim drdp As SqlClient.SqlDataReader = cmddp.ExecuteReader
            If drdp.HasRows Then
                DisplayPatient(PatientID)
                'PopulatePatientDxs(PatientID)
                dgvTGPs.Rows.Clear()
                If PatientCovered(PatientID) = True Then
                    DisplayCoverage(PatientID)
                    UpdatePrimResp(1)
                Else
                    UpdatePrimResp(0)
                End If
            Else
                MsgBox("The Patient ID provided is not valid. Use Search")
                txtPatientID.Text = ""
                cmbPrimResp.SelectedIndex = 0
                txtPatientID.Focus()
            End If
            cndp.Close()
            cndp = Nothing
        Else
            ClearPatient()
            ClearDxs()
            dgvTGPs.Rows.Clear()
            If txtOrdID.Text <> "" Then
                cmbPrimResp.SelectedIndex = 0
            Else
                cmbPrimResp.SelectedIndex = -1
            End If
        End If
        Update_Patient_Status()
        Update_Billing_Status()
        UpdateOrderProgress()
    End Sub

    Private Sub Update_Order_Status()
        If dgvTGPs.RowCount > 0 Then
            lblOrders.BackColor = Color.PaleGreen
        Else
            lblOrders.BackColor = Color.PeachPuff
        End If
    End Sub

    Private Sub Update_Patient_Status()
        If (txtPatientID.Text <> "" And lblEMRReqd.ForeColor = Color.DarkBlue) _
        Or (lblEMRReqd.ForeColor = Color.Red And txtEMRNo.Text <> "") Then
            lblPatient.BackColor = Color.PaleGreen
        Else
            lblPatient.BackColor = Color.PeachPuff
        End If
    End Sub

    Private Function PatientCovered(ByVal PatientID As Long) As Boolean
        Dim Cover As Boolean = False
        Dim cndp As New SqlClient.SqlConnection(connString)
        cndp.Open()
        Dim cmddp As New SqlClient.SqlCommand("Select * from Coverages where Patient_ID = " & PatientID, cndp)
        cmddp.CommandType = CommandType.Text
        Dim drdp As SqlClient.SqlDataReader = cmddp.ExecuteReader
        If drdp.HasRows Then Cover = True
        cndp.Close()
        cndp = Nothing
        Return Cover
    End Function

    Private Sub ClearPrimary()
        cmbPIns.SelectedIndex = -1
        txtPGroup.Text = ""
        txtPInsID.Text = ""
        txtPFrom.Text = ""
        txtPTo.Text = ""
        txtPCopay.Text = ""
        cmbPRelation.SelectedIndex = -1
        ClearPSubscriber()
    End Sub

    Private Sub ClearPSubscriber()
        txtPSubID.Text = ""
        txtPSubLName.Text = ""
        txtPSubFName.Text = ""
        txtPSubMName.Text = ""
        cmbPSubSex.SelectedIndex = -1
        txtPSubDOB.Text = ""
        txtPSubSSN.Text = ""
        txtPSubAdd1.Text = ""
        txtPSubAdd2.Text = ""
        txtPSubCity.Text = ""
        txtPSubState.Text = ""
        txtPSubZip.Text = ""
        txtPSubCountry.Text = ""
        txtPSubPhone.Text = ""
        txtPSubEmail.Text = ""
    End Sub

    Private Sub DisplayPrimInsured(ByVal InsuredID As Long)
        ClearPSubscriber()
        Dim cndp As New SqlClient.SqlConnection(connString)
        cndp.Open()
        Dim cmddp As New SqlClient.SqlCommand("Select * from Patients where ID = " & InsuredID, cndp)
        cmddp.CommandType = CommandType.Text
        Dim drdp As SqlClient.SqlDataReader = cmddp.ExecuteReader
        If drdp.HasRows Then
            While drdp.Read
                txtPSubID.Text = drdp("ID")
                txtPSubLName.Text = drdp("LastName")
                txtPSubFName.Text = drdp("FirstName")
                txtPSubMName.Text = drdp("MiddleName")
                For i As Integer = 0 To cmbPSubSex.Items.Count - 1
                    If cmbPSubSex.Items(i).ToString.Substring(0, 1) = drdp("Sex") Then
                        cmbPSubSex.SelectedIndex = i
                        Exit For
                    End If
                Next
                txtPSubDOB.Text = Format(drdp("DOB"), SystemConfig.DateFormat)
                If drdp("SSN") IsNot DBNull.Value Then txtPSubSSN.Text = drdp("SSN")
                If drdp("Address_ID") IsNot DBNull.Value Then
                    txtPSubAdd1.Text = GetAddress1(drdp("Address_ID"))
                    txtPSubAdd2.Text = GetAddress2(drdp("Address_ID"))
                    txtPSubCity.Text = GetAddressCity(drdp("Address_ID"))
                    txtPSubState.Text = GetAddressState(drdp("Address_ID"))
                    txtPSubZip.Text = GetAddressZip(drdp("Address_ID"))
                    txtPSubCountry.Text = GetAddressCountry(drdp("Address_ID"))
                End If
                If drdp("HomePhone") IsNot DBNull.Value Then _
                txtPSubPhone.Text = drdp("HomePhone")
                If drdp("Email") IsNot DBNull.Value Then _
                txtPSubEmail.Text = drdp("Email")
            End While
        End If
        cndp.Close()
        cndp = Nothing
    End Sub

    Private Sub DisplayCoverage(ByVal PatientID As Long)
        ClearPrimary()
        Dim ItemX As MyList
        Dim cndp As New SqlClient.SqlConnection(connString)
        cndp.Open()
        Dim cmddp As New SqlClient.SqlCommand("Select * from Coverages where Patient_ID = " & PatientID, cndp)
        cmddp.CommandType = CommandType.Text
        Dim drdp As SqlClient.SqlDataReader = cmddp.ExecuteReader
        If drdp.HasRows Then
            While drdp.Read
                If drdp("Preference") = "P" Then     'Primary
                    For i As Integer = 0 To cmbPIns.Items.Count - 1
                        ItemX = cmbPIns.Items(i)
                        If ItemX.ItemData = drdp("Insurance_ID") Then
                            cmbPIns.SelectedIndex = i
                            Exit For
                        End If
                    Next
                    If drdp("GroupNo") IsNot DBNull.Value Then txtPGroup.Text = drdp("GroupNo")
                    If drdp("PolicyNo") IsNot DBNull.Value Then txtPInsID.Text = drdp("PolicyNo")
                    If drdp("StartDate") IsNot DBNull.Value Then txtPFrom.Text = drdp("StartDate")
                    If drdp("ExpireDate") IsNot DBNull.Value Then txtPTo.Text = drdp("ExpireDate")
                    cmbPRelation.SelectedIndex = drdp("Relation")
                    If drdp("Copayment") IsNot DBNull.Value Then txtPCopay.Text = drdp("Copayment")
                    If drdp("Insured_ID") IsNot DBNull.Value Then DisplayPrimInsured(drdp("Insured_ID"))
                End If
            End While
        End If
        cndp.Close()
        cndp = Nothing
    End Sub

    Private Sub btnPatLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatLook.Click
        Dim PatientID As String = frmPatLookUp.ShowDialog()
        If PatientID <> "" Then
            DisplayPatient(Val(PatientID))
            'PopulatePatientDxs(PatientID)
            dgvTGPs.Rows.Clear()
            'UpdatePrimResp()
            PopulateInsurances()
            If PatientCovered(Val(PatientID)) = True Then
                DisplayCoverage(Val(PatientID))
                UpdatePrimResp(1)
            Else
                UpdatePrimResp(0)
            End If
        End If
        Update_Patient_Status()
        Update_Billing_Status()
        UpdateOrderProgress()
    End Sub

    Private Function GetNextInstanceID() As Long
        Dim NextID As Long = 1
        Dim cndp As New SqlClient.SqlConnection(connString)
        cndp.Open()
        Dim cmddp As New SqlClient.SqlCommand("Select Max(ID) as LastID from Order_Instance", cndp)
        cmddp.CommandType = CommandType.Text
        Dim drdp As SqlClient.SqlDataReader = cmddp.ExecuteReader
        If drdp.HasRows Then
            While drdp.Read
                If drdp("LastID") IsNot DBNull.Value _
                Then NextID = drdp("LastID") + 1
            End While
        End If
        cndp.Close()
        cndp = Nothing
        Return NextID
    End Function

    Private Sub btnPrev2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrev2.Click
        TC.SelectTab(0)
    End Sub

    Private Sub btnNext2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext2.Click
        TC.SelectTab(2)
    End Sub

    Private Sub btnNext3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext3.Click
        TC.SelectTab(3)
    End Sub

    Private Sub btnPrev3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrev3.Click
        TC.SelectTab(1)
    End Sub

    Private Sub btnPrev4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        TC.SelectTab(2)
    End Sub

    Private Sub btnNext4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        TC.SelectTab(4)
    End Sub

    Private Sub btnPrev5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrev5.Click
        TC.SelectTab(3)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If lblGeneral.BackColor = Color.PaleGreen And lblOrderer.BackColor = Color.PaleGreen And
        lblPatient.BackColor = Color.PaleGreen And lblOrders.BackColor = Color.PaleGreen And
        lblBilling.BackColor = Color.PaleGreen Then
            Dim OrderID As Long = SaveOrder()
            SaveBillingInfo()
            SaveComponents(OrderID)
            'SaveInstanceTGPs()
            MsgBox("The order has been saved under " & OrderID.ToString, MsgBoxStyle.Information, "Prolis")
            ClearForm()
            If btnNew.Checked = False Then txtOrderID.Text = NextOrderID()
        End If
    End Sub

    Private Sub SaveBillingInfo()
        Dim sSQL As String = ""
        Dim ItemX As MyList
        Dim PayerID As Long = 0
        If Trim(txtPatientID.Text) <> "" And cmbPIns.SelectedIndex <> -1 And
        Trim(txtPInsID.Text) <> "" And cmbPRelation.SelectedIndex = 0 Then   'self
            ItemX = cmbPIns.SelectedItem
            PayerID = ItemX.ItemData
            ExecuteSqlProcedure("If Exists (Select * from Coverages where Patient_ID = " &
            Val(txtPatientID.Text) & " and Insurance_ID = " & PayerID & " and Preference = 'P') " &
            "Update Coverages set Relation = 0, Insured_ID = " & Trim(txtPatientID.Text) &
            ", GroupNo = '" & Trim(txtPGroup.Text) & "', PolicyNo = '" & Trim(txtPInsID.Text) &
            "', CoPayment = " & Val(txtPCopay.Text) & ", LastEditedOn = '" & Date.Now & "', " &
            "EditedBy = " & ThisUser.ID & " where Patient_ID = " & Val(txtPatientID.Text) &
            " and Insurance_ID = " & PayerID & " and Preference = 'P' Else Insert into " &
            "Coverages (Patient_ID, Insurance_ID, Preference, Ordinal, Relation, Insured_ID, " &
            "GroupNo, PolicyNo, CoPayment, LastEditedOn, EditedBy) values (" & Trim(txtPatientID.Text) &
            ", " & PayerID & ", 'P', 0, 0, " & Trim(txtPatientID.Text) & ", '" & Trim(txtPGroup.Text) &
            "', '" & Trim(txtPInsID.Text) & "', " & Val(txtPCopay.Text) & ", '" & Date.Now &
            "', " & ThisUser.ID & ")")
        ElseIf Trim(txtPatientID.Text) <> "" And cmbPIns.SelectedIndex <> -1 And
                Trim(txtPInsID.Text) <> "" And cmbPRelation.SelectedIndex <> 0 And
                Trim(txtPSubLName.Text) <> "" And Trim(txtPSubFName.Text) <> "" And
                cmbPSubSex.SelectedIndex <> -1 And IsDate(txtPSubDOB.Text) Then   'other
            txtPSubID.Text = UpdatePatient(Trim(txtPSubID.Text), Trim(txtPSubLName.Text),
            Trim(txtPSubFName.Text), Trim(txtPSubMName.Text), Microsoft.VisualBasic.Left(
            cmbPSubSex.SelectedItem.ToString, 1), CDate(txtPSubDOB.Text),
            Trim(txtPSubPhone.Text), Trim(txtPSubEmail.Text))
            ItemX = cmbPIns.SelectedItem
            PayerID = ItemX.ItemData
            ExecuteSqlProcedure("If Exists (Select * from Coverages where Patient_ID = " &
            Val(txtPatientID.Text) & " and Insurance_ID = " & PayerID & " and Preference = 'P') " &
            "Update Coverages set Relation = " & cmbPRelation.SelectedIndex & ", Insured_ID = " &
            Trim(txtPSubID.Text) & ", GroupNo = '" & Trim(txtPGroup.Text) & "', PolicyNo = '" &
            Trim(txtPInsID.Text) & "', CoPayment = " & Val(txtPCopay.Text) & ", LastEditedOn = '" &
            Date.Now & "', EditedBy = " & ThisUser.ID & " where Patient_ID = " & Val(txtPatientID.Text) &
            " and Insurance_ID = " & PayerID & " and Preference = 'P' Else Insert into Coverages " &
            "(Patient_ID, Insurance_ID, Preference, Ordinal, Relation, Insured_ID, GroupNo, PolicyNo, " &
            "CoPayment, LastEditedOn, EditedBy) values (" & Trim(txtPatientID.Text) & ", " & PayerID &
            ", 'P', 0, " & cmbPRelation.SelectedIndex & ", " & Trim(txtPSubID.Text) & ", '" &
            Trim(txtPGroup.Text) & "', '" & Trim(txtPInsID.Text) & "', " & Val(txtPCopay.Text) & ", '" &
            Date.Now & "', " & ThisUser.ID & ")")
        End If
        '
        ExecuteSqlProcedure("Delete from Coverages where Preference = 'P' and Patient_ID = " &
        Trim(txtPatientID.Text) & " and Insurance_ID <> " & PayerID)
    End Sub

    Private Sub SaveComponents(ByVal OrderID As Long)
        Dim i As Integer
        Dim TDate As Date
        Dim MaxCount As Integer = Nothing
        Dim sSQL As String = "Delete from Order_TGP where Order_ID = " & OrderID _
        & " and Not TGP_ID in ("
        For i = 0 To dgvTGPs.RowCount - 1
            If dgvTGPs.Rows(i).Cells(1).Value IsNot Nothing AndAlso
            Trim(dgvTGPs.Rows(i).Cells(1).Value) <> "" Then
                sSQL += Trim(dgvTGPs.Rows(i).Cells(1).Value) & ", "
                If chkInfiniteTimed.Checked = False Then    'Infinite
                    TDate = Nothing
                    MaxCount = Nothing
                Else
                    If dgvTGPs.Rows(i).Cells(7).Value IsNot Nothing _
                    AndAlso IsDate(dgvTGPs.Rows(i).Cells(7).Value) Then
                        TDate = CDate(dgvTGPs.Rows(i).Cells(7).Value)
                    ElseIf dgvTGPs.Rows(i).Cells(6).Value IsNot Nothing _
                    AndAlso CInt(dgvTGPs.Rows(i).Cells(7).Value) > 0 Then
                        MaxCount = CInt(dgvTGPs.Rows(i).Cells(7).Value)
                    Else
                        MsgBox("For timed order, either Max Count needs to be greater than zero " &
                               "or the 'End Date' must have the value. The order could not be saved.")
                        DeleteOrder(OrderID)
                        Exit Sub
                    End If
                End If
                ExecuteSqlProcedure("If Exists (Select * from Order_TGP where Order_ID = " & OrderID & " and TGP_ID = " &
                dgvTGPs.Rows(i).Cells(1).Value & ") Update Order_TGP set Interval = '" & dgvTGPs.Rows(i).Cells(4).Value &
                "', Qty = " & dgvTGPs.Rows(i).Cells(5).Value & ", StartDate = '" & dgvTGPs.Rows(i).Cells(3).Value &
                "', EndDate = " & IIf(IsDate(TDate) = False, "Null", "'" & TDate & "'") & ", MaxCount = " &
                IIf(MaxCount > 0, MaxCount, "Null") & ", IsESRD = 0, LastEdited_On = '" & Date.Now & "', Edited_By = " &
                ThisUser.ID & " where Order_ID = " & OrderID & " and TGP_ID = " & dgvTGPs.Rows(i).Cells(1).Value &
                " Else Insert into Order_TGP (Order_ID, TGP_ID, Interval, Qty, StartDate, EndDate, MaxCount, IsESRD, " &
                "LastEdited_On, Edited_By) values (" & OrderID & ", " & dgvTGPs.Rows(i).Cells(1).Value & ", '" &
                dgvTGPs.Rows(i).Cells(4).Value & "', " & dgvTGPs.Rows(i).Cells(5).Value & ", '" & dgvTGPs.Rows(i).Cells(3).Value &
                "', " & IIf(IsDate(TDate) = False, "Null", "'" & TDate & "'") & ", " & IIf(MaxCount > 0, MaxCount, "Null") &
                ", 0, '" & Date.Now & "', " & ThisUser.ID & ")")
            End If
        Next
        If sSQL.EndsWith(", ") Then sSQL = sSQL.Substring(0, Len(sSQL) - 2) & ")"
        ExecuteSqlProcedure(sSQL)
    End Sub

    Private Function SaveOrder() As Long
        Dim OrderID As Long = Val(txtOrderID.Text)
        If btnNew.Checked = False Then  'New
            If Not IsUniqueOrder(OrderID) Then OrderID = NextOrderID()
        End If
        '
        Dim Days As String = ""
        For i As Integer = 0 To chklstDays.Items.Count - 1
            If chklstDays.GetItemChecked(i) = True Then
                Days += chklstDays.Items(i).ToString.Substring(0, 3) & ", "
            End If
        Next
        If Days.EndsWith(", ") Then Days = Days.Substring(0, Len(Days) - 2)
        '
        Dim conn As New SqlClient.SqlConnection(connString)
        conn.Open()
        Dim cmdupsert As New SqlClient.SqlCommand("Orders_SP", conn)
        cmdupsert.CommandType = CommandType.StoredProcedure
        cmdupsert.Parameters.AddWithValue("@act", "Upsert")
        cmdupsert.Parameters.AddWithValue("@ID", OrderID)
        cmdupsert.Parameters.AddWithValue("@OrderDate", dtpOrderDate.Value)
        cmdupsert.Parameters.AddWithValue("@Active", chkActive.Checked)
        cmdupsert.Parameters.AddWithValue("@InfiniteTimed", chkInfiniteTimed.Checked)
        cmdupsert.Parameters.AddWithValue("@TestDays", Days)
        cmdupsert.Parameters.AddWithValue("@Agency_ID", txtAgencyID.Text)
        cmdupsert.Parameters.AddWithValue("@OrderingProvider_ID", Val(txtOrdID.Text))
        Dim ItemX As MyList = Nothing
        For i As Integer = 0 To lstProviders.Items.Count - 1
            If lstProviders.GetItemChecked(i) = True Then
                ItemX = lstProviders.Items(i)
                Exit For
            End If
        Next
        cmdupsert.Parameters.AddWithValue("@AttendingProvider_ID", ItemX.ItemData)
        cmdupsert.Parameters.AddWithValue("@BillingType_ID", cmbPrimResp.SelectedIndex)
        cmdupsert.Parameters.AddWithValue("@Patient_ID", Val(txtPatientID.Text))
        cmdupsert.Parameters.AddWithValue("@EntrySource_ID", 0)
        cmdupsert.Parameters.AddWithValue("@Phleb_Loc", cmbPhlebLoc.SelectedIndex)
        cmdupsert.Parameters.AddWithValue("@WorkCmnt", txtWorkCmnt.Text)
        cmdupsert.Parameters.AddWithValue("@Fasting", chkNeedFast.Checked)
        'If IsDate(txtDischargeDate.Text) Then
        If IsDate(dtpDischargeDate.Text) Then
            cmdupsert.Parameters.AddWithValue("@CompleteDate", CDate(dtpDischargeDate.Text))
        End If
        If btnNew.Checked = False Then  'New
            cmdupsert.Parameters.AddWithValue("@Created_By", ThisUser.ID)
        End If
        cmdupsert.Parameters.AddWithValue("@LastEdited_On", Date.Now)
        cmdupsert.Parameters.AddWithValue("@Edited_By", ThisUser.ID)
        '
        If Trim(txtEMRNo.Text) <> "" Then SaveEMRNo(Val(txtOrdID.Text),
        Val(txtPatientID.Text), Trim(txtEMRNo.Text))
        '
        SaveOrderDxs(OrderID)
        '
        Try
            cmdupsert.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
            conn = Nothing
        End Try
        Return OrderID
    End Function

    Private Sub DeleteOrder(ByVal OrderID As Long)
        ExecuteSqlProcedure("Delete from Orders where ID = " & OrderID)
        ExecuteSqlProcedure("Delete from Order_Dx where Order_ID = " & OrderID)
    End Sub

    Private Sub SaveOrderDxs(ByVal OrderID As Long)
        For i As Integer = 0 To dgvDxs.RowCount - 1
            If dgvDxs.Rows(i).Cells(0).Value IsNot Nothing _
            AndAlso Trim(dgvDxs.Rows(i).Cells(0).Value) <> "" Then
                Dim conn As New SqlClient.SqlConnection(connString)
                conn.Open()
                Dim cmdupsert As New SqlClient.SqlCommand("Order_Dx_SP", conn)
                cmdupsert.CommandType = CommandType.StoredProcedure
                cmdupsert.Parameters.AddWithValue("@act", "Upsert")
                cmdupsert.Parameters.AddWithValue("@Order_ID", OrderID)
                cmdupsert.Parameters.AddWithValue("@Dx_Code", Trim(dgvDxs.Rows(i).Cells(0).Value))
                cmdupsert.Parameters.AddWithValue("@Ordinal", i)
                Try
                    cmdupsert.ExecuteNonQuery()
                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    conn.Close()
                End Try
            End If
        Next
    End Sub

    Private Sub SaveEMRNo(ByVal ProviderID As Long, ByVal PatientID As Long, ByVal EMRNo As String)
        ExecuteSqlProcedure("If Exists (Select * from Client_Patient where Provider_ID = " &
        ProviderID & " and Patient_ID = " & PatientID & ") Update Client_Patient set " &
        "EMRNo = '" & EMRNo & "' where Provider_ID = " & ProviderID & " and Patient_ID = " &
        PatientID & " Else Insert into Client_Patient (Provider_ID, Patient_ID, EMRNo) " &
        "values (" & ProviderID & ", " & PatientID & ", '" & EMRNo & "')")
    End Sub

    Private Function ExtMarkable(ByVal TGPID As Integer) As Boolean
        Dim Sex As String = ""
        Dim Age As Integer = 1
        Dim Markable As Boolean = False
        If txtPatientID.Text <> "" Then
            Dim cndp As New SqlClient.SqlConnection(connString)
            cndp.Open()
            Dim cmddp As New SqlClient.SqlCommand("Select * from " &
            "Patients where ID = " & Val(txtPatientID.Text), cndp)
            cmddp.CommandType = CommandType.Text
            Dim drdp As SqlClient.SqlDataReader = cmddp.ExecuteReader
            If drdp.HasRows Then
                While drdp.Read
                    Sex = drdp("Sex")
                    Age = DateDiff(DateInterval.Year, drdp("DOB"), System.DateTime.Today)
                End While
            End If
            cndp.Close()
            cndp = Nothing
            '
            Dim cnmm As New SqlClient.SqlConnection(connString)
            cnmm.Open()
            Dim cmdmm As New SqlClient.SqlCommand("Select * from " &
            "MarkingModifiers where Test_ID = " & TGPID, cnmm)
            cmdmm.CommandType = CommandType.Text
            Dim drmm As SqlClient.SqlDataReader = cmdmm.ExecuteReader
            If drmm.HasRows Then
                While drmm.Read
                    If drmm("Sex") = Sex And drmm("AgeFrom") <= Age _
                    And drmm("AgeTo") >= Age Then Markable = True
                End While
            End If
            cnmm.Close()
            cnmm = Nothing
        Else
            Markable = True
        End If
        Return Markable
    End Function

    Private Function TGPMarkable(ByVal TGPID As Integer) As Boolean
        Dim Markable As Boolean = False
        Dim cnmm As New SqlClient.SqlConnection(connString)
        cnmm.Open()
        Dim cmdmm As New SqlClient.SqlCommand("Select IsMarkable from Tests " &
        "where ID = " & TGPID & " Union Select IsMarkable from Groups where ID = " &
        TGPID & " Union Select IsMarkable from Profiles where ID = " & TGPID, cnmm)
        cmdmm.CommandType = CommandType.Text
        Dim drmm As SqlClient.SqlDataReader = cmdmm.ExecuteReader
        If drmm.HasRows Then
            While drmm.Read
                Markable = drmm("IsMarkable")
            End While
        End If
        cnmm.Close()
        cnmm = Nothing
        Return Markable
    End Function

    Private Function InstTGPStored(ByVal InstID As Long, ByVal TGPID As Integer) As Boolean
        Dim TGPStored As Boolean = False
        Dim i As Integer
        Dim t As Integer
        For i = 0 To UBound(Instances, 1)
            For t = 0 To UBound(Instances, 2)
                If Instances(i, 0) = InstID.ToString And Instances(i, t) = TGPID.ToString Then
                    TGPStored = True
                    Exit For
                End If
            Next
            If TGPStored = True Then Exit For
        Next
        Return TGPStored
    End Function

    Private Function InstanceStored(ByVal InstanceID As Long) As Boolean
        Dim Ins As Boolean = False
        Dim i As Integer
        For i = 0 To UBound(Instances, 2)
            If Instances(0, i) = InstanceID Then
                Ins = True
                Exit For
            End If
        Next
        Return Ins
    End Function

    Private Sub btnNext1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext1.Click
        TC.SelectTab(1)
    End Sub

    Private Sub btnOrderLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOrderLook.Click
        Dim OrderID As String = frmOrderLookUp.ShowDialog()
        If OrderID <> "" Then DisplayOrderRecord(Val(OrderID))
        OrderID = ""
        Update_Provider_Status()
        UpdateOrderProgress()
    End Sub

    Private Sub tpPatient_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpPatient.Validated
        btnPatUpdate_Click(Nothing, Nothing)
        UpdateOrderProgress()
    End Sub

    Private Sub btnPatUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPatUpdate.Click
        If txtLName.Text <> "" And txtFName.Text <> "" And cmbSex.SelectedIndex _
        <> -1 And IsDate(txtDOB.Text) = True Then
            txtPatientID.Text = UpdatePatient(txtPatientID.Text, txtLName.Text,
            txtFName.Text, txtMName.Text, cmbSex.SelectedItem.ToString.Substring(0, 1),
            CDate(txtDOB.Text), txtPatHPhone.Text, txtPatEmail.Text)
            lblPatient.BackColor = Color.PaleGreen
            btnRemPat.Enabled = True
        End If
        'UpdatePrimResp()
        UpdateOrderProgress()
    End Sub

    Private Sub tpGeneral_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpGeneral.Validated
        UpdateOrderProgress()
    End Sub

    Private Sub tpProvider_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpProvider.Validated
        UpdateOrderProgress()
    End Sub

    Private Sub tpOrders_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
        UpdateOrderProgress()
    End Sub

    Private Sub tpBilling_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpBilling.Validated
        UpdateOrderProgress()
    End Sub

    Private Sub txtQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtQty_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.Validated
        If txtQty.Text = "" Then txtQty.Text = "1"
        UpdateProgress()
    End Sub

    Private Sub txtRepeat_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Numerals(sender, e)
    End Sub

    Private Sub UpdateProgress()
        If txtTGPID.Text <> "" And cmbInterval.SelectedIndex <> -1 _
        And txtQty.Text <> "" And (chkInfiniteTimed.Checked = False Or
        (chkInfiniteTimed.Checked = True And (Val(txtMaxCount.Text) > 0 Or
        IsDate(txtEndDate.Text)))) Then
            btnUpdate.Enabled = True
        Else
            btnUpdate.Enabled = False
        End If
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If txtOrderID.Text <> "" And txtTGPID.Text <> "" And
        cmbInterval.SelectedIndex <> -1 And txtQty.Text <> "" And
        (Not chkInfiniteTimed.Checked Or (chkInfiniteTimed.Checked And (
        Val(txtMaxCount.Text) > 0 Or IsDate(txtEndDate.Text)))) Then
            If Not TGPinList(Val(txtTGPID.Text)) Then
                dgvTGPs.Rows.Add(Nothing, txtTGPID.Text,
                GetTGPName(Val(txtTGPID.Text)), Format(dtpStartDate.Value,
                SystemConfig.DateFormat), cmbInterval.SelectedItem.ToString,
                txtQty.Text, txtMaxCount.Text, IIf(IsDate(txtEndDate.Text),
                txtEndDate.Text, ""))
                txtTGPID.Text = ""
                txtTGPName.Text = ""
                txtQty.Text = "1"
                txtEndDate.Text = ""
                btnUpdate.Enabled = False
            Else
                MsgBox("Duplicate components not allowed", MsgBoxStyle.Critical, "Prolis")
                txtTGPID.Focus()
            End If
        End If
        Update_Order_Status()
        UpdateOrderProgress()
    End Sub

    Private Function TGPinList(ByVal TGPID As Integer) As Boolean
        Dim InList As Boolean = False
        For i As Integer = 0 To dgvTGPs.RowCount - 1
            If dgvTGPs.Rows(i).Cells(1).Value = TGPID.ToString Then
                InList = True
                Exit For
            End If
        Next
        Return InList
    End Function

    Private Sub cmbInterval_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbInterval.SelectedIndexChanged
        UpdateProgress()
    End Sub

    Private Sub frmOrders_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ConfigureDateTimePicker(dtpDischargeDate)
        txtOrderID.Text = NextOrderID()
        cmbPhlebLoc.SelectedIndex = 0
        cmbInterval.SelectedIndex = 0
        txtQty.Text = "1"
        txtAgencyPhone.Mask = SystemConfig.PhoneMask
        txtOrdPhone.Mask = SystemConfig.PhoneMask
        txtOrdFax.Mask = SystemConfig.PhoneMask
        txtPatHPhone.Mask = SystemConfig.PhoneMask
        UpdateGeneralStatus()
        PopulatePayers()
    End Sub

    Private Sub PopulatePayers()
        cmbPIns.Items.Clear()
        Dim cnpp As New SqlClient.SqlConnection(connString)
        cnpp.Open()
        Dim cmdpp As New SqlClient.SqlCommand("Select * from " & _
        "Payers where Active <> 0 order by PayerName", cnpp)
        cmdpp.CommandType = CommandType.Text
        Dim drpp As SqlClient.SqlDataReader = cmdpp.ExecuteReader
        If drpp.HasRows Then
            While drpp.Read
                If Not drpp("PayerName").ToString.StartsWith("ZZ") Then
                    cmbPIns.Items.Add(New MyList(drpp("PayerName"), drpp("ID")))
                End If
            End While
        End If
        cnpp.Close()
        cnpp = Nothing
    End Sub

    Private Sub dgvTGPs_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTGPs.CellContentClick
        If e.ColumnIndex = 0 Then   'delete row
            Dim RetVal As Integer
            RetVal = MsgBox("Are you sure you want to delete this component?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
            If RetVal = vbYes Then
                dgvTGPs.Rows.RemoveAt(e.RowIndex)
            End If
        End If
        Update_Order_Status()
    End Sub

    Private Sub dgvTGPs_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTGPs.CellDoubleClick
        If e.ColumnIndex <> 0 AndAlso e.RowIndex >= 0 Then
            txtTGPID.Text = dgvTGPs.Rows(e.RowIndex).Cells(1).Value
            txtTGPName.Text = dgvTGPs.Rows(e.RowIndex).Cells(2).Value
            dtpStartDate.Value = CDate(dgvTGPs.Rows(e.RowIndex).Cells(3).Value)
            For i As Integer = 0 To cmbInterval.Items.Count - 1
                If dgvTGPs.Rows(e.RowIndex).Cells(4).Value = cmbInterval.Items(i).ToString Then
                    cmbInterval.SelectedIndex = i
                    Exit For
                End If
            Next
            txtQty.Text = dgvTGPs.Rows(e.RowIndex).Cells(5).Value
            If dgvTGPs.Rows(e.RowIndex).Cells(6).Value IsNot System.DBNull.Value _
            Then txtMaxCount.Text = dgvTGPs.Rows(e.RowIndex).Cells(6).Value
            If IsDate(dgvTGPs.Rows(e.RowIndex).Cells(7).Value) Then _
            txtEndDate.Text = dgvTGPs.Rows(e.RowIndex).Cells(7).Value
        End If
    End Sub

    Private Sub txtTGPID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTGPID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtTGPID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTGPID.Validated
        If txtTGPID.Text <> "" Then
            txtTGPName.Text = GetTGPName(Val(txtTGPID.Text))
            If txtTGPName.Text = "" Then
                MsgBox("Invalid ID", MsgBoxStyle.Critical, "Prolis")
                txtTGPID.Text = ""
            End If
        Else
            txtTGPName.Text = ""
        End If
        SchedulingProgress()
    End Sub

    Private Sub SchedulingProgress()
        If Trim(txtTGPID.Text) <> "" And (chkInfiniteTimed.Checked = False And _
        cmbInterval.SelectedIndex <> -1) Or (chkInfiniteTimed.Checked = True And _
        cmbInterval.SelectedIndex <> -1 AndAlso (txtMaxCount.Text <> "" Or _
        IsDate(txtEndDate.Text))) Then
            btnUpdate.Enabled = True
        Else
            btnUpdate.Enabled = False
        End If
    End Sub

    Private Sub txtTime_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
        UpdateGeneralStatus()
        UpdateOrderProgress()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If lblGeneral.BackColor = Color.PaleGreen And lblOrderer.BackColor = Color.PaleGreen _
        And lblPatient.BackColor = Color.PaleGreen And lblBilling.BackColor = Color.PaleGreen _
        And lblOrders.BackColor = Color.PaleGreen Then
            Dim RetVal As Integer = MsgBox("Are you sure to delete this order?", _
            MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
            If RetVal = vbYes Then
                ExecuteSqlProcedure("Delete from Order_Dx where Order_ID = " & txtOrderID.Text)
                ExecuteSqlProcedure("Delete from Order_TGP where Order_ID = " & txtOrderID.Text)
                ExecuteSqlProcedure("Delete from Orders where ID = " & txtOrderID.Text)
                ClearForm()
            End If
        End If
    End Sub

    Private Sub btnRemProv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemProv.Click
        txtOrdID.Text = ""
        ClearOrderer()
        Update_Provider_Status()
        UpdateOrderProgress()
    End Sub

    Private Sub lstProviders_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstProviders.SelectedIndexChanged
        Dim i As Integer
        Dim ItemX As MyList = lstProviders.Items(lstProviders.SelectedIndex)
        lstProviders.SetItemChecked(lstProviders.SelectedIndex, True)
        For i = 0 To lstProviders.Items.Count - 1
            If lstProviders.SelectedIndex <> i Then _
            lstProviders.SetItemChecked(i, False)
        Next
        Update_Provider_Status()
        UpdateOrderProgress()
    End Sub

    Private Sub chkInfiniteTimed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkInfiniteTimed.CheckedChanged
        If chkInfiniteTimed.Checked = False Then    'Infinite
            chkInfiniteTimed.Text = "Infinite"
            txtEndDate.Text = ""
            txtMaxCount.Text = ""
            txtEndDate.ReadOnly = True
            txtMaxCount.ReadOnly = True
        Else
            chkInfiniteTimed.Text = "Timed"
            txtEndDate.ReadOnly = False
            txtMaxCount.ReadOnly = False
        End If
    End Sub

    Private Sub chklstDays_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chklstDays.SelectedValueChanged
        UpdateGeneralStatus()
        UpdateOrderProgress()
    End Sub

    'Private Sub txtEndDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEndDate.TextChanged
    '    If IsDate(txtEndDate.Text) Then SchedulingProgress()
    'End Sub

    Private Sub txtMaxCount_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMaxCount.Validated
        If txtMaxCount.Text <> "" AndAlso _
        Val(txtMaxCount.Text) > 0 Then SchedulingProgress()
    End Sub

    Private Sub btnCompLookUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCompLookUp.Click
        Dim Component As String = frmTGPLookup.ShowDialog
        If Component <> "" Then
            txtTGPID.Text = Component
            txtTGPName.Text = GetTGPName(Component)
        Else
            txtTGPID.Text = ""
            txtTGPName.Text = ""
        End If
        SchedulingProgress()
    End Sub

    Private Sub cmbPrimResp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPrimResp.SelectedIndexChanged
        If cmbPrimResp.SelectedIndex = 0 Then   'client
            lblBilling.BackColor = Color.PaleGreen
            lblAdd1.ForeColor = Color.DarkBlue
            lblCity.ForeColor = Color.DarkBlue
            lblState.ForeColor = Color.DarkBlue
            lblZip.ForeColor = Color.DarkBlue
            lblHPhone.ForeColor = Color.DarkBlue
            lblDxs.ForeColor = Color.DarkBlue
        ElseIf cmbPrimResp.SelectedIndex = 1 Then   'TP
            lblAdd1.ForeColor = Color.Red
            lblCity.ForeColor = Color.Red
            lblState.ForeColor = Color.Red
            lblZip.ForeColor = Color.Red
            lblDxs.ForeColor = Color.Red
            lblHPhone.ForeColor = IIf(PatPhRequired, Color.Red, Color.DarkBlue)
            If cmbPIns.SelectedIndex <> -1 And Trim(txtPInsID.Text) _
            <> "" And (cmbPRelation.SelectedIndex = 0 Or _
            (cmbPRelation.SelectedIndex > 0 And txtPSubLName.Text <> "" And _
            txtPSubFName.Text <> "" And IsDate(txtPSubDOB.Text) And _
            cmbPSubSex.SelectedIndex <> -1)) Then
                lblBilling.BackColor = Color.PaleGreen
            Else
                lblBilling.BackColor = Color.PeachPuff
            End If
        Else
            lblAdd1.ForeColor = Color.Red
            lblCity.ForeColor = Color.Red
            lblState.ForeColor = Color.Red
            lblZip.ForeColor = Color.Red
            lblDxs.ForeColor = Color.DarkBlue
            lblHPhone.ForeColor = IIf(PatPhRequired, Color.Red, Color.DarkBlue)
            lblBilling.BackColor = Color.PaleGreen
        End If
    End Sub

    Private Sub txtEndDate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEndDate.Validated
        SchedulingProgress()
    End Sub

    Private Sub dgvDxs_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDxs.CellContentClick
        If e.ColumnIndex = 1 Then
            If e.RowIndex <> -1 Then
                TCode = dgvDxs.Rows(e.RowIndex).Cells(0).Value
                TCode = frmDiagnosis.ShowDialog
                If TCode <> "" Then
                    dgvDxs.Rows(e.RowIndex).Cells(0).Value = TCode
                    TCode = ""
                End If
            End If
        End If
    End Sub

    Private Sub dgvDxs_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDxs.CellEndEdit
        If e.ColumnIndex = 0 Then
            If Trim(dgvDxs.Rows(e.RowIndex).Cells(0).Value) <> "" Then
                If IsDuplicateDx(dgvDxs.Rows(e.RowIndex).Cells(0).Value) Then
                    MsgBox("Grid already contains the code you just typed")
                    dgvDxs.Rows(e.RowIndex).Cells(0).Value = ""
                Else
                    If IsCodeComplete(dgvDxs.Rows(e.RowIndex).Cells(0).Value) Then
                        If e.RowIndex = dgvDxs.RowCount - 1 Then
                            dgvDxs.RowCount += 1
                            dgvDxs.CurrentCell = dgvDxs.Rows(dgvDxs.RowCount - 1).Cells(0)
                        End If
                    Else
                        TCode = dgvDxs.Rows(e.RowIndex).Cells(0).Value
                        If TCode.Length >= 3 Then
                            TCode = frmDiagnosis.ShowDialog
                            If TCode <> "" Then
                                dgvDxs.RowCount += 1
                                dgvDxs.CurrentCell.Value = TCode
                                TCode = ""
                            Else
                                dgvDxs.Rows(e.RowIndex).Cells(0).Value = TCode
                            End If
                        Else
                            MsgBox("Minimum 3 characters required", MsgBoxStyle.Critical, "Prolis")
                            dgvDxs.Rows(e.RowIndex).Cells(0).Value = ""
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Function IsDuplicateDx(ByVal Dx As String) As Boolean
        Dim i As Integer
        Dim DxCount As Integer = 0
        For i = 0 To dgvDxs.RowCount - 1
            If Trim(dgvDxs.Rows(i).Cells(0).Value) = Dx Then
                DxCount = DxCount + 1
            End If
        Next
        If DxCount > 1 Then
            IsDuplicateDx = True
        Else
            IsDuplicateDx = False
        End If
    End Function

    Private Sub ClearDxs()
        dgvDxs.Rows.Clear()
        dgvDxs.RowCount = 20
    End Sub

    Private Sub dgvDxs_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDxs.CellEnter
        Curow = e.RowIndex
    End Sub

    Private Sub cmbPIns_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPIns.SelectedIndexChanged
        Update_Billing_Status()
    End Sub

    Private Sub txtPInsID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPInsID.Validated
        Update_Billing_Status()
    End Sub

    Private Sub cmbPRelation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPRelation.SelectedIndexChanged
        Update_Billing_Status()
    End Sub
    Private Sub dtpDischargeDate_CloseUp(sender As Object, e As EventArgs) Handles dtpDischargeDate.CloseUp
        CloseUpDateTimePicker(dtpDischargeDate)
    End Sub

    Private Sub lblClearDates_Click(sender As Object, e As EventArgs) Handles lblClearDates.Click
        ClearDateTimePicker(dtpDischargeDate)
    End Sub

    Private Sub txtOrderID_TextChanged(sender As Object, e As EventArgs) Handles txtOrderID.TextChanged

    End Sub
End Class
