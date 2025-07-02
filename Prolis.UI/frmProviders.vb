Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmProviders
    Private AlertFont As Font

    Private Sub chkEditNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEditNew.Click
        FormClear()
        If chkEditNew.Checked = False Then
            chkEditNew.Text = "Edit"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Edit.ico")
            btnProvLook.Enabled = True
        Else
            chkEditNew.Text = "New"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\New.ico")
            txtProviderID.Text = NextProviderID()
            btnProvLook.Enabled = False
        End If
    End Sub

    Private Function IsProviderUsed(ByVal ProviderID As Long) As Boolean
        Dim used As Boolean = False
        Dim cnpu As New SqlConnection(connString)
        cnpu.Open()
        Dim cmdpu As New SqlCommand("Select * from Requisitions where OrderingProvider_ID = " & ProviderID, cnpu)
        cmdpu.CommandType = CommandType.Text
        Dim drpu As SqlDataReader = cmdpu.ExecuteReader
        If drpu.HasRows Then used = True
        cnpu.Close()
        cnpu = Nothing
        Return used
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim RetVal As Integer
        If (chkIndividual.Checked = True And (txtProviderID.Text <> "" And
        txtLName.Text <> "" And txtFName.Text <> "")) Or
        (chkIndividual.Checked = False And (txtProviderID.Text <> "" And
        txtLName.Text <> "")) Then
            If Not IsProviderUsed(Val(txtProviderID.Text)) Then
                RetVal = MsgBox("Are you sure about deleting this Provider?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "MD PERFECT")
                If RetVal = vbYes Then
                    ExecuteSqlProcedure("Delete from Providers where ID = " & Val(txtProviderID.Text))
                    ExecuteSqlProcedure("Delete from Clinic_Provider where Provider_ID = " & Val(txtProviderID.Text))
                    FormClear()
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                End If
            Else
                MsgBox("This provider has a references in Requisitions. In such a case system" _
                & " does not allow to delete the provider. If you must delete this provider, remove" _
                & " all the references of this provider in Requisitions and try again.", MsgBoxStyle.Information)
            End If
        Else
            MsgBox("You must display the provider first then delete it", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub FormClear()
        chkIndividual.Checked = False
        chkPatPhRequired.Checked = False
        chkDOCRequired.Checked = False
        chkUseMyReports.Checked = False
        cmbDxSearch.SelectedIndex = 0
        txtProviderID.Text = ""
        txtLName.Text = ""
        txtFName.Text = ""
        txtMName.Text = ""
        txtDegree.Text = ""
        txtSSN.Text = ""
        txtNPI.Text = ""
        txtUPIN.Text = ""
        txtCLIA.Text = ""
        txtPOS.Text = ""
        txtMCR.Text = ""
        txtMCD.Text = ""
        txtBCBS.Text = ""
        txtLicense.Text = ""
        txtAdd1.Text = ""
        txtAdd2.Text = ""
        txtCity.Text = ""
        txtState.Text = ""
        txtZip.Text = ""
        txtContact.Text = ""
        txtEmail.Text = ""
        txtUserName.Text = ""
        txtPassword.Text = ""
        chkUserMgmt.Checked = False
        txtPhone.Text = ""
        txtFax.Text = ""
        txtCell.Text = ""
        txtNote.Text = ""
        txtExtCmnt.Text = ""
        txtAlert.Text = ""
        chkServerPDF.Checked = False
        txtAlert.Font = AlertFont
        txtAlert.BackColor = Color.White
        txtAlert.ForeColor = Color.Black
        chkCS.Checked = False
        chkAcc.Checked = False
        chkAssActive.Checked = True
        For r As Integer = 0 To dgvHours.RowCount - 1
            For c As Integer = 1 To dgvHours.ColumnCount - 1
                dgvHours.Rows(r).Cells(c).Value = "OFF"
            Next
        Next
        txtResRPTFile.Text = ""
        txtInvoiceRPTFile.Text = ""
        txtPanicInstructions.Text = ""
        cmbPanic.SelectedIndex = 1
        cmbRegular.SelectedIndex = 3
        chkComplete.Checked = True
        txtCopies.Text = "1"
        chkSetExt.Checked = False
        txtAutoTime.Text = "06:00 AM"
        txtIncSpan.Text = "2"
        txtCompSpan.Text = "7"
        chkAuto.Checked = False : chkFax.Checked = False : chkFax.Enabled = True
        chkPrint.Checked = False : chkEmail.Checked = False : chkEmail.Enabled = True
        chkInterface.Checked = False : chkInterface.Enabled = True
        chkProlison.Checked = False
        chkChartRequired.Checked = False
        chkUseESRD.Checked = False
        chkAccConsolidate.Checked = False
        chkBlockDemograph.Checked = False
        chkORNecessity.Checked = True
        txtPickupNote.Text = ""
        cmbPickUp.SelectedIndex = 0
        '
        dgvProviders.Rows.Clear()
        dtpFrom.Value = Date.Today
        dtpTo.Value = DateAdd(DateInterval.Day, 365, dtpFrom.Value)
        dgvContract.Rows.Clear()
        dgvContract.RowCount = 1
        dgvAGRanges.Rows.Clear()
        dgvAGRanges.Rows.Add()
        btnSave.Enabled = False
        btnDelete.Enabled = False
    End Sub

    Private Function NextProviderID() As Long
        Dim ProvID As Long = 1001
        Dim cnpid As New SqlConnection(connString)
        cnpid.Open()
        Dim cmdpid As New SqlCommand("Select max(ID) as LastID from Providers", cnpid)
        cmdpid.CommandType = CommandType.Text
        Dim drpid As SqlDataReader = cmdpid.ExecuteReader
        If drpid.HasRows Then
            While drpid.Read
                If drpid("LastID") IsNot DBNull.Value _
                Then ProvID = drpid("LastID") + 1
            End While
        End If
        cnpid.Close()
        cnpid = Nothing
        Return ProvID
    End Function

    Private Sub chkComplete_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkComplete.CheckedChanged
        If chkComplete.Checked = False Then
            chkComplete.Text = "Partial"
        Else
            chkComplete.Text = "Complete"   'Default
        End If
    End Sub

    Private Sub frmProviders_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If LIC Is Nothing Then _
        LIC = New LicenseManager.ProlisLicense(connString, My.Application.Info.AssemblyName)
        If LIC.AppRun = True Then
            chkServerPDF.Enabled = True
        Else
            chkServerPDF.Enabled = False
        End If
        '
        cmbDefaultBilling.SelectedIndex = 0
        chkChartRequired.Checked = True
        chkDOCRequired.Checked = True
        cmbDxSearch.SelectedIndex = 2   'Disease-Condition
        chkAccConsolidate.Checked = True
        cmbPanic.SelectedIndex = 1
        cmbRegular.SelectedIndex = 3
        'PopulateReports()
        txtPhone.Mask = SystemConfig.PhoneMask
        txtFax.Mask = SystemConfig.PhoneMask
        txtCell.Mask = SystemConfig.PhoneMask
        AlertFont = txtAlert.Font
        cmbReport.SelectedIndex = 1
        If LIC.Fax = True Then
            chkFax.Enabled = True
        Else
            chkFax.Enabled = False
        End If
        dgvAGRanges.RowCount = 1
        PopulateSales()
        PopulateRoutes(0)
        PopulateHours()
        'PopulateIndProviders()
        dtpFrom.Value = Date.Today
        dtpTo.Value = DateAdd(DateInterval.Day, 365, dtpFrom.Value)
        dgvContract.RowCount = 1
        cmbPriceLevel.SelectedIndex = 0
        txtCopies.Text = "1"
        cmbPickUp.SelectedIndex = 0
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub PopulateIndProviders()
        dgvProviders.Rows.Clear()
        Dim Provider As String = ""
        Dim cnpr As New SqlConnection(connString)
        cnpr.Open()
        Dim cmdpr As New SqlCommand("Select * from Providers " &
        "where IsIndividual <> 0 order by LastName_BSN, FirstName", cnpr)
        cmdpr.CommandType = CommandType.Text
        Dim drpr As SqlDataReader = cmdpr.ExecuteReader
        If drpr.HasRows Then
            While drpr.Read
                If drpr("Degree") IsNot DBNull.Value AndAlso Trim(drpr("Degree")) <> "" Then
                    Provider = Trim(drpr("LastName_BSN")) & ", " &
                    Trim(drpr("FirstName")) & " " & Trim(drpr("Degree"))
                Else
                    Provider = Trim(drpr("LastName_BSN")) & ", " & Trim(drpr("FirstName"))
                End If
                dgvProviders.Rows.Add(False, drpr("ID"), Provider)
                If drpr("Active") IsNot DBNull.Value OrElse drpr("Active") = False Then
                    dgvProviders.Rows(dgvProviders.RowCount - 1).Cells(0).ReadOnly = True
                    dgvProviders.Rows(dgvProviders.RowCount - 1).Cells(0).Style.BackColor = Color.LightGray
                Else
                    dgvProviders.Rows(dgvProviders.RowCount - 1).Cells(0).ReadOnly = False
                    dgvProviders.Rows(dgvProviders.RowCount - 1).Cells(0).Style.BackColor =
                    dgvProviders.Rows(dgvProviders.RowCount - 1).Cells(1).Style.BackColor
                End If
            End While
        End If
        cnpr.Close()
        cnpr = Nothing
    End Sub

    Private Sub PopulateReports()
        cmbReport.Items.Clear()
        Dim cnpr As New SqlConnection(connString)
        cnpr.Open()
        Dim cmdpr As New SqlCommand("Select * from Reports where Report_Type_ID = 1", cnpr)
        cmdpr.CommandType = CommandType.Text
        Dim drpr As SqlDataReader = cmdpr.ExecuteReader
        If drpr.HasRows Then
            While drpr.Read
                cmbReport.Items.Add(New MyList(drpr("Name"), drpr("ID")))
            End While
            If cmbReport.Items.Count > 0 Then _
            cmbReport.SelectedIndex = 0
        End If
        cnpr.Close()
        cnpr = Nothing
    End Sub

    Private Sub PopulateSales()
        cmbSales.Items.Clear()
        Dim cnsp As New SqlConnection(connString)
        cnsp.Open()
        Dim cmdsp As New SqlCommand("Select * from SalesPersons", cnsp)
        cmdsp.CommandType = CommandType.Text
        Dim drsp As SqlDataReader = cmdsp.ExecuteReader
        If drsp.HasRows Then
            While drsp.Read
                cmbSales.Items.Add(New MyList(drsp("FullName"), drsp("ID")))
            End While
            If cmbSales.Items.Count > 0 Then _
            cmbSales.SelectedIndex = 0
        End If
        cnsp.Close()
        cnsp = Nothing
    End Sub

    Private Sub PopulateRoutes(ByVal SelRouteID As Integer)
        cmbRoutes.Items.Clear()
        Dim ItemX As MyList
        Dim cnsp As New SqlConnection(connString)
        cnsp.Open()
        Dim cmdsp As New SqlCommand("Select * from Routes", cnsp)
        cmdsp.CommandType = CommandType.Text
        Dim drsp As SqlDataReader = cmdsp.ExecuteReader
        If drsp.HasRows Then
            While drsp.Read
                cmbRoutes.Items.Add(New MyList(drsp("Name"), drsp("ID")))
            End While
            For i As Integer = 0 To cmbRoutes.Items.Count - 1
                ItemX = cmbRoutes.Items(i)
                If ItemX.ItemData = SelRouteID Then
                    cmbRoutes.SelectedIndex = i
                    Exit For
                End If
            Next
        End If
        cnsp.Close()
        cnsp = Nothing
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtProviderID.Text <> "" And txtLName.Text <> "" And
        ((chkIndividual.Checked = True And txtFName.Text <> "") Or
        (chkIndividual.Checked = False)) And (txtLicense.Text <> "" Or
        txtNPI.Text <> "" Or txtUPIN.Text <> "") Then
            SaveProvider(Val(txtProviderID.Text))
            SaveAssociation(Val(txtProviderID.Text))
            SaveProviderRanges()
            'PopulateIndProviders()
            FormClear()
            If chkEditNew.Checked = True Then
                txtProviderID.Text = NextProviderID()
            End If
            btnSave.Enabled = False
            btnDelete.Enabled = False
        Else
            MsgBox("All required fields (Red labeled) and one of the conditionally " &
            "required (PINK) must be filled to save the record.")
            If txtLName.Text = "" Then
                txtLName.Focus()
            ElseIf txtFName.Text = "" Then
                txtFName.Focus()
            ElseIf txtLicense.Text = "" Then
                txtLicense.Focus()
            ElseIf txtNPI.Text = "" Then
                txtNPI.Focus()
            ElseIf txtUPIN.Text = "" Then
                txtUPIN.Focus()
            End If
        End If
    End Sub

    Private Sub SaveProviderRanges()
        If txtProviderID.Text <> "" Then
            ExecuteSqlProcedure("Delete from Provider_Ranges where Provider_ID = " & Val(txtProviderID.Text))
            For i As Integer = 0 To dgvAGRanges.RowCount - 1
                If (dgvAGRanges.Rows(i).Cells(1).Value IsNot Nothing _
                AndAlso Trim(dgvAGRanges.Rows(i).Cells(1).Value) <> "") _
                And (dgvAGRanges.Rows(i).Cells(3).Value IsNot Nothing _
                AndAlso Trim(dgvAGRanges.Rows(i).Cells(3).Value) <> "") _
                And (dgvAGRanges.Rows(i).Cells(4).Value IsNot Nothing _
                AndAlso Trim(dgvAGRanges.Rows(i).Cells(4).Value) <> "") _
                And (dgvAGRanges.Rows(i).Cells(5).Value IsNot Nothing _
                AndAlso Trim(dgvAGRanges.Rows(i).Cells(5).Value) <> "") _
                And (dgvAGRanges.Rows(i).Cells(6).Value IsNot Nothing _
                AndAlso Trim(dgvAGRanges.Rows(i).Cells(6).Value) <> "") _
                And (dgvAGRanges.Rows(i).Cells(7).Value IsNot Nothing _
                AndAlso Trim(dgvAGRanges.Rows(i).Cells(7).Value) <> "") _
                And (dgvAGRanges.Rows(i).Cells(8).Value IsNot Nothing _
                AndAlso Trim(dgvAGRanges.Rows(i).Cells(8).Value) <> "") _
                And (dgvAGRanges.Rows(i).Cells(9).Value IsNot Nothing _
                AndAlso Trim(dgvAGRanges.Rows(i).Cells(9).Value) <> "") Then
                    ExecuteSqlProcedure("If Exists (Select * from Provider_Ranges " &
                    "where Provider_ID = " & Val(txtProviderID.Text) & " and Test_ID = " &
                    Val(Trim(dgvAGRanges.Rows(i).Cells(1).Value)) & " and AgeFrom = " &
                    Val(Trim(dgvAGRanges.Rows(i).Cells(3).Value)) & " and AgeTo = " &
                    Val(Trim(dgvAGRanges.Rows(i).Cells(4).Value)) & " and Sex = '" &
                    Trim(dgvAGRanges.Rows(i).Cells(5).Value) & "' and ValueFrom = '" &
                    Trim(dgvAGRanges.Rows(i).Cells(6).Value) & "' and ValueTo = '" &
                    Trim(dgvAGRanges.Rows(i).Cells(7).Value) & "') Update Provider_Ranges " &
                    "set Flag = '" & Trim(dgvAGRanges.Rows(i).Cells(8).Value) & "', " &
                    "Behavior = '" & Trim(dgvAGRanges.Rows(i).Cells(9).Value) & "', " &
                    "LastEditedOn = '" & Date.Now & "', EditedBy = " & ThisUser.ID &
                    " where Provider_ID = " & Val(txtProviderID.Text) & " and Test_ID = " &
                    Val(Trim(dgvAGRanges.Rows(i).Cells(1).Value)) & " and AgeFrom = " &
                    Val(Trim(dgvAGRanges.Rows(i).Cells(3).Value)) & " and AgeTo = " &
                    Val(Trim(dgvAGRanges.Rows(i).Cells(4).Value)) & " and Sex = '" &
                    Trim(dgvAGRanges.Rows(i).Cells(5).Value) & "' and ValueFrom = '" &
                    Trim(dgvAGRanges.Rows(i).Cells(6).Value) & "' and ValueTo = '" &
                    Trim(dgvAGRanges.Rows(i).Cells(7).Value) & "' Else Insert into Provider_Ranges " &
                    "(Provider_ID, Test_ID, AgeFrom, AgeTo, Sex, ValueFrom, ValueTo, Flag, Behavior, " &
                    "LastEditedOn, EditedBy) values (" & Val(txtProviderID.Text) & ", " &
                    Val(Trim(dgvAGRanges.Rows(i).Cells(1).Value)) & ", " &
                    Val(Trim(dgvAGRanges.Rows(i).Cells(3).Value)) & ", " &
                    Val(Trim(dgvAGRanges.Rows(i).Cells(4).Value)) & ", '" &
                    Trim(dgvAGRanges.Rows(i).Cells(5).Value) & "', '" &
                    Trim(dgvAGRanges.Rows(i).Cells(6).Value) & "', '" &
                    Trim(dgvAGRanges.Rows(i).Cells(7).Value) & "', '" &
                    Trim(dgvAGRanges.Rows(i).Cells(8).Value) & "', '" &
                    Trim(dgvAGRanges.Rows(i).Cells(9).Value) & "', '" &
                    Date.Now & "', " & ThisUser.ID & ")")
                End If
            Next
        End If
    End Sub

    Private Sub SaveAssociation(ByVal ClinicID As Long)
        Dim AssIDs As String = ""
        For i As Integer = 0 To dgvProviders.RowCount - 1
            If chkIndividual.Checked = True Then    'ind
                ExecuteSqlProcedure("If Exists (Select * from Clinic_Provider where Provider_ID = " &
                ClinicID & " and Clinic_ID = " & dgvProviders.Rows(i).Cells(0).Value & ") Update " &
                "Clinic_Provider set Active = " & Convert.ToInt16(dgvProviders.Rows(i).Cells(2).Value) &
                " where Provider_ID = " & ClinicID & " and Clinic_ID = " & dgvProviders.Rows(i).Cells(0).Value &
                " Else Insert into Clinic_Provider (Provider_ID, Clinic_ID, Active) values (" &
                ClinicID & ", " & dgvProviders.Rows(i).Cells(0).Value & ", " &
                Convert.ToInt16(dgvProviders.Rows(i).Cells(2).Value) & ")")
                '
                AssIDs += dgvProviders.Rows(i).Cells(0).Value.ToString & ", "
            Else    'Entity
                ExecuteSqlProcedure("If Exists (Select * from Clinic_Provider where Clinic_ID = " &
                ClinicID & " and Provider_ID = " & dgvProviders.Rows(i).Cells(0).Value & ") Update " &
                "Clinic_Provider set Active = " & Convert.ToInt16(dgvProviders.Rows(i).Cells(2).Value) &
                " where Clinic_ID = " & ClinicID & " and Provider_ID = " & dgvProviders.Rows(i).Cells(0).Value &
                " Else Insert into Clinic_Provider (Clinic_ID, Provider_ID, Active) values (" &
                ClinicID & ", " & dgvProviders.Rows(i).Cells(0).Value & ", " &
                Convert.ToInt16(dgvProviders.Rows(i).Cells(2).Value) & ")")
                '
                AssIDs += dgvProviders.Rows(i).Cells(0).Value.ToString & ", "
            End If
        Next
        If AssIDs.EndsWith(", ") Then AssIDs = AssIDs.Substring(0, Len(AssIDs) - 2)
        If AssIDs <> "" Then
            If chkIndividual.Checked = True Then    'ind
                ExecuteSqlProcedure("Delete from Clinic_Provider where Provider_ID = " & ClinicID &
                " and Clinic_ID <> Provider_ID and not Clinic_ID in (" & AssIDs & ")")
            Else    'Entity
                ExecuteSqlProcedure("Delete from Clinic_Provider where Clinic_ID = " & ClinicID &
                " and Clinic_ID <> Provider_ID and not Provider_ID in (" & AssIDs & ")")
            End If
        End If
    End Sub

    Private Sub SaveProvider(ByVal ProviderID As Long)
        Dim ItemX As MyList
        Dim cnsv As New SqlConnection(connString)
        cnsv.Open()
        Dim cmdupsert As New SqlCommand("Providers_SP", cnsv)
        cmdupsert.CommandType = CommandType.StoredProcedure
        cmdupsert.Parameters.AddWithValue("@act", "Upsert")
        cmdupsert.Parameters.AddWithValue("@ID", ProviderID)
        cmdupsert.Parameters.AddWithValue("@FacilityType_ID", 5)
        cmdupsert.Parameters.AddWithValue("@LastName_BSN", Trim(txtLName.Text))
        cmdupsert.Parameters.AddWithValue("@FirstName", Trim(txtFName.Text))
        cmdupsert.Parameters.AddWithValue("@MiddleName", Trim(txtMName.Text))
        cmdupsert.Parameters.AddWithValue("@Degree", Trim(txtDegree.Text))
        cmdupsert.Parameters.AddWithValue("@IsIndividual", chkIndividual.Checked)
        cmdupsert.Parameters.AddWithValue("@Abbr", "")
        cmdupsert.Parameters.AddWithValue("@SSN", SSNNeat(txtSSN.Text))
        cmdupsert.Parameters.AddWithValue("@Address_ID", GetAddressID(Trim(txtAdd1.Text),
        Trim(txtAdd2.Text), Trim(txtCity.Text), Trim(txtState.Text), Trim(txtZip.Text), Trim(txtCountry.Text)))
        ItemX = cmbSales.SelectedItem
        Try
            If ItemX IsNot Nothing Then
                cmdupsert.Parameters.AddWithValue("@SalesPerson_ID", ItemX.ItemData)
                If cmbRoutes.SelectedIndex = -1 Then cmbRoutes.SelectedIndex = 0
                ItemX = cmbRoutes.SelectedItem
                cmdupsert.Parameters.AddWithValue("@Route_ID", ItemX.ItemData)
            End If

        Catch ex As Exception

        End Try

        cmdupsert.Parameters.AddWithValue("@PickUP", cmbPickUp.SelectedIndex)
        cmdupsert.Parameters.AddWithValue("@Report_ID", cmbReport.SelectedIndex)
        cmdupsert.Parameters.AddWithValue("@License", Trim(txtLicense.Text))
        cmdupsert.Parameters.AddWithValue("@UPIN", Trim(txtUPIN.Text))
        cmdupsert.Parameters.AddWithValue("@CLIA", Trim(txtCLIA.Text))
        cmdupsert.Parameters.AddWithValue("@POS_Code", Trim(txtPOS.Text))
        cmdupsert.Parameters.AddWithValue("@NPI", Trim(txtNPI.Text))
        cmdupsert.Parameters.AddWithValue("@Medicare", Trim(txtMCR.Text))
        cmdupsert.Parameters.AddWithValue("@Medicaid", Trim(txtMCD.Text))
        cmdupsert.Parameters.AddWithValue("@BCBS", Trim(txtBCBS.Text))
        cmdupsert.Parameters.AddWithValue("@InterfaceDLL", Trim(txtCommDLL.Text))
        cmdupsert.Parameters.AddWithValue("@Active", chkActive.Checked)
        cmdupsert.Parameters.AddWithValue("@Phone", PhoneNeat(txtPhone.Text))
        cmdupsert.Parameters.AddWithValue("@Fax", PhoneNeat(txtFax.Text))
        cmdupsert.Parameters.AddWithValue("@Cell", PhoneNeat(txtCell.Text))
        cmdupsert.Parameters.AddWithValue("@Email", Trim(txtEmail.Text))
        cmdupsert.Parameters.AddWithValue("@Contact", Trim(txtContact.Text))
        cmdupsert.Parameters.AddWithValue("@UserName", Trim(txtUserName.Text))
        cmdupsert.Parameters.AddWithValue("@Password", Trim(txtPassword.Text))
        UpdateOutreachAccess(ProviderID, Trim(txtUserName.Text),
        Trim(txtPassword.Text), chkUserMgmt.Checked)
        cmdupsert.Parameters.AddWithValue("@EMRNoRequired", chkChartRequired.Checked)
        cmdupsert.Parameters.AddWithValue("@PatPhRequired", chkPatPhRequired.Checked)
        cmdupsert.Parameters.AddWithValue("@CalendarOrdering", 0)
        cmdupsert.Parameters.AddWithValue("@AccConsolidate", chkAccConsolidate.Checked)
        cmdupsert.Parameters.AddWithValue("@BlockDemograph", chkBlockDemograph.Checked)
        cmdupsert.Parameters.AddWithValue("@DefaultBilling", cmbDefaultBilling.SelectedIndex)
        cmdupsert.Parameters.AddWithValue("@DxSearchDefault", cmbDxSearch.SelectedIndex)
        cmdupsert.Parameters.AddWithValue("@DOCRequired", chkDOCRequired.Checked)
        cmdupsert.Parameters.AddWithValue("@UseESRD", chkUseESRD.Checked)
        cmdupsert.Parameters.AddWithValue("@UseMyReports", chkUseMyReports.Checked)
        cmdupsert.Parameters.AddWithValue("@ClientUserMgmt", chkUserMgmt.Checked)
        cmdupsert.Parameters.AddWithValue("@ORNecessity", chkORNecessity.Checked)
        cmdupsert.Parameters.AddWithValue("@Labeler", "")
        cmdupsert.Parameters.AddWithValue("@Note", Trim(txtNote.Text))
        cmdupsert.Parameters.AddWithValue("@Panic_Instructions", txtPanicInstructions.Rtf)
        cmdupsert.Parameters.AddWithValue("@ExtComment", Trim(txtExtCmnt.Text))
        If txtAlert.Text <> "" Then cmdupsert.Parameters.AddWithValue("@Alert", txtAlert.Rtf)
        cmdupsert.Parameters.AddWithValue("@Alert_CS", chkCS.Checked)
        cmdupsert.Parameters.AddWithValue("@Alert_Acc", chkAcc.Checked)
        cmdupsert.Parameters.AddWithValue("@ResRPTFile", Trim(txtResRPTFile.Text))
        cmdupsert.Parameters.AddWithValue("@InvoiceRPTFile", Trim(txtInvoiceRPTFile.Text))
        cmdupsert.Parameters.AddWithValue("@PanicRDM_ID", cmbPanic.SelectedIndex)
        cmdupsert.Parameters.AddWithValue("@RptComplete", chkComplete.Checked)
        cmdupsert.Parameters.AddWithValue("@RptComp_Span", Trim(txtCompSpan.Text))
        cmdupsert.Parameters.AddWithValue("@RptIncomp_Span", Trim(txtIncSpan.Text))
        cmdupsert.Parameters.AddWithValue("@RegularRDM_ID", cmbRegular.SelectedIndex)
        cmdupsert.Parameters.AddWithValue("@RptCopies", Val(txtCopies.Text))
        cmdupsert.Parameters.AddWithValue("@SetExtend", chkSetExt.Checked)
        cmdupsert.Parameters.AddWithValue("@ProlisOn", "")
        cmdupsert.Parameters.AddWithValue("@Rpt_AutoPrint_Start", txtAutoTime.Text)
        cmdupsert.Parameters.AddWithValue("@RDM_Auto", chkAuto.Checked)
        cmdupsert.Parameters.AddWithValue("@RDM_Email", chkEmail.Checked)
        cmdupsert.Parameters.AddWithValue("@RDM_Fax", chkFax.Checked)
        cmdupsert.Parameters.AddWithValue("@RDM_Print", chkPrint.Checked)
        cmdupsert.Parameters.AddWithValue("@RDM_Interface", chkInterface.Checked)
        cmdupsert.Parameters.AddWithValue("@RDM_Prolison", chkProlison.Checked)
        cmdupsert.Parameters.AddWithValue("@ServerPDF", chkServerPDF.Checked)
        cmdupsert.Parameters.AddWithValue("@PriceLevel", cmbPriceLevel.SelectedIndex)
        cmdupsert.Parameters.AddWithValue("@ContractFrom", dtpFrom.Value)
        cmdupsert.Parameters.AddWithValue("@ContractTo", dtpTo.Value)
        cmdupsert.Parameters.AddWithValue("@MonLunchStart", dgvHours.Rows(0).Cells(2).Value)
        cmdupsert.Parameters.AddWithValue("@MonLunchStop", dgvHours.Rows(0).Cells(3).Value)
        cmdupsert.Parameters.AddWithValue("@MonStart", dgvHours.Rows(0).Cells(1).Value)
        cmdupsert.Parameters.AddWithValue("@MonStop", dgvHours.Rows(0).Cells(4).Value)
        cmdupsert.Parameters.AddWithValue("@TueStart", dgvHours.Rows(1).Cells(1).Value)
        cmdupsert.Parameters.AddWithValue("@TueLunchStart", dgvHours.Rows(1).Cells(2).Value)
        cmdupsert.Parameters.AddWithValue("@TueLunchStop", dgvHours.Rows(1).Cells(3).Value)
        cmdupsert.Parameters.AddWithValue("@TueStop", dgvHours.Rows(1).Cells(4).Value)
        cmdupsert.Parameters.AddWithValue("@WedStart", dgvHours.Rows(2).Cells(1).Value)
        cmdupsert.Parameters.AddWithValue("@WedLunchStart", dgvHours.Rows(2).Cells(2).Value)
        cmdupsert.Parameters.AddWithValue("@WedLunchStop", dgvHours.Rows(2).Cells(3).Value)
        cmdupsert.Parameters.AddWithValue("@WedStop", dgvHours.Rows(2).Cells(4).Value)
        cmdupsert.Parameters.AddWithValue("@ThuStart", dgvHours.Rows(3).Cells(1).Value)
        cmdupsert.Parameters.AddWithValue("@ThuLunchStart", dgvHours.Rows(3).Cells(2).Value)
        cmdupsert.Parameters.AddWithValue("@ThuLunchStop", dgvHours.Rows(3).Cells(3).Value)
        cmdupsert.Parameters.AddWithValue("@ThuStop", dgvHours.Rows(3).Cells(4).Value)
        cmdupsert.Parameters.AddWithValue("@FriStart", dgvHours.Rows(4).Cells(1).Value)
        cmdupsert.Parameters.AddWithValue("@FriLunchStart", dgvHours.Rows(4).Cells(2).Value)
        cmdupsert.Parameters.AddWithValue("@FriLunchStop", dgvHours.Rows(4).Cells(3).Value)
        cmdupsert.Parameters.AddWithValue("@FriStop", dgvHours.Rows(4).Cells(4).Value)
        cmdupsert.Parameters.AddWithValue("@SatStart", dgvHours.Rows(5).Cells(1).Value)
        cmdupsert.Parameters.AddWithValue("@SatLunchStart", dgvHours.Rows(5).Cells(2).Value)
        cmdupsert.Parameters.AddWithValue("@SatLunchStop", dgvHours.Rows(5).Cells(3).Value)
        cmdupsert.Parameters.AddWithValue("@SatStop", dgvHours.Rows(5).Cells(4).Value)
        cmdupsert.Parameters.AddWithValue("@SunStart", dgvHours.Rows(6).Cells(1).Value)
        cmdupsert.Parameters.AddWithValue("@SunLunchStart", dgvHours.Rows(6).Cells(2).Value)
        cmdupsert.Parameters.AddWithValue("@SunLunchStop", dgvHours.Rows(6).Cells(3).Value)
        cmdupsert.Parameters.AddWithValue("@SunStop", dgvHours.Rows(6).Cells(4).Value)
        cmdupsert.Parameters.AddWithValue("@PickupNote", Trim(txtPickupNote.Text))
        cmdupsert.Parameters.AddWithValue("@LastEditedOn", Date.Now)
        cmdupsert.Parameters.AddWithValue("@EditedBy", ThisUser.ID)
        Try
            cmdupsert.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cnsv.Close()
            cnsv = Nothing
        End Try
        '
        Dim CPT As String = ""
        ExecuteSqlProcedure("Delete from Provider_TGP where Provider_ID = " & ProviderID)
        For i As Integer = 0 To dgvContract.RowCount - 1
            If Not dgvContract.Rows(i).Cells(1).Value Is Nothing AndAlso
            dgvContract.Rows(i).Cells(1).Value.ToString <> "" Then
                If dgvContract.Rows(i).Cells(5).Value IsNot Nothing AndAlso
                dgvContract.Rows(i).Cells(5).Value IsNot DBNull.Value Then
                    CPT = Trim(dgvContract.Rows(i).Cells(5).Value)
                Else
                    CPT = ""
                End If
                ExecuteSqlProcedure("Insert into Provider_TGP (Provider_ID, TGP_ID, CPT_Code, IsDistinct, " &
                "Price, Ordinal) values (" & ProviderID & ", " & Val(dgvContract.Rows(i).Cells(1).Value) &
                ", '" & CPT & "', " & Convert.ToInt16(dgvContract.Rows(i).Cells(6).Value) _
                & ", " & Val(dgvContract.Rows(i).Cells(7).Value) & ", " & i & ")")
            End If
        Next
    End Sub

    Private Sub UpdateOutreachAccess(ByVal ProviderID As Long, ByVal _
    UserName As String, ByVal Password As String, ByVal UserMgmt As Boolean)
        If UserName <> "" And Password <> "" Then
            ' Dim LM As New LicenseManager.ProlisLicense(connString, My.Application.Info.AssemblyName)
            Dim ClientUserID As Long = Nothing
            ExecuteSqlProcedure("If Exists (Select * from Client_Users where Provider_ID = " &
            ProviderID & " and UserName = '" & UserName & "') Update Client_Users set " &
            "Password = '" & encryptString(Password) & "', Active = 1, LastName = '" &
            "Administrator', FirstName = 'Lab', Email = '', Cell = '', UserMgmt = " &
            Convert.ToInt16(UserMgmt) & ", Accession = 1, Schedule = 1, ScheduleExec = 1, " &
            "ResultAcc = 1, ResultCum = 1 where Provider_ID = " & ProviderID & " and " &
            "UserName = '" & UserName & "' Else Insert into Client_Users (Provider_ID, " &
            "ClientUser_ID, UserName, Password, Active, LastName, FirstName, Email, Cell, " &
            "UserMgmt, Accession, Schedule, ScheduleExec, ResultAcc, ResultCum) values (" &
            ProviderID & ", " & NextClientUserID(ProviderID, UserName) & ", '" & UserName &
            "', '" & encryptString(Password) & "', 1, 'Administrator', 'Lab', '', '', " &
            Convert.ToInt16(UserMgmt) & ", 1, 1, 1, 1, 1)")
            'LM = Nothing
        Else    'delete user access
            ExecuteSqlProcedure("Update Client_Users Set UserMgmt = 0 where Provider_ID = " & Val(txtProviderID.Text))
            ExecuteSqlProcedure("Delete from Client_Users where Provider_ID = " & Val(txtProviderID.Text) _
            & " and Not ClientUser_ID in(Select Edited_By from Orders where EntrySource_ID = 1 " _
            & "and OrderingProvider_ID = " & Val(txtProviderID.Text) & " Union Select " &
            "AccessionedBy from Requisitions where AccessionLoc_ID = 1 and OrderingProvider_ID " _
            & "= " & Val(txtProviderID.Text) & ")")
            ExecuteSqlProcedure("Delete from ClientUser_Event where Provider_ID = " & Val(txtProviderID.Text) _
            & " and Not ClientUser_ID in(Select ClientUser_ID from ClientUser_Event where " &
            "Provider_ID = " & Val(txtProviderID.Text) & ")")
        End If
    End Sub

    Private Function NextClientUserID(ByVal ProviderID As Long, ByVal User As String) As Long
        Dim uid As Long = 1
        Dim cnuid As New SqlConnection(connString)
        cnuid.Open()
        Dim cmduid As New SqlCommand("Select Max(ClientUser_ID) as UID from " &
        "Client_Users where Provider_ID = " & ProviderID & " and UserName = '" & User & "'", cnuid)
        cmduid.CommandType = CommandType.Text
        Dim druid As SqlDataReader = cmduid.ExecuteReader
        If druid.HasRows Then
            While druid.Read
                If druid("UID") IsNot DBNull.Value Then
                    uid = druid("UID") + 1
                Else
                    uid = 1
                End If
            End While
        Else
            uid = 1
        End If
        cnuid.Close()
        cnuid = Nothing
        Return uid
    End Function

    Private Sub chkIndividual_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIndividual.CheckedChanged
        txtFName.Text = ""
        txtMName.Text = ""
        txtDegree.Text = ""
        txtSSN.Text = ""
        If chkIndividual.Checked = True Then
            chkIndividual.Text = "Individual"
            lblLName.Text = "Last Name"
            lblFName.ForeColor = Color.Red
            txtFName.Enabled = True
            txtMName.Enabled = True
            txtDegree.Enabled = True
            txtSSN.Mask = "000-00-0000"
        Else
            chkIndividual.Text = "Entity"
            lblLName.Text = "Entity Name"
            lblFName.ForeColor = Color.DarkBlue
            txtFName.Enabled = False
            txtMName.Enabled = False
            txtDegree.Enabled = False
            txtSSN.Mask = "00-0000000"
        End If
        Update_Progress()
    End Sub

    Private Sub Update_Progress()
        If txtProviderID.Text <> "" And txtLName.Text <> "" And
        ((chkIndividual.Checked = True And txtFName.Text <> "") Or
        (chkIndividual.Checked = False)) And (txtLicense.Text <> "" Or
        txtNPI.Text <> "" Or txtUPIN.Text <> "") Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub DisplayProvider(ByVal ProviderID As Long)
        Dim ItemX As MyList
        Dim cndp As New SqlConnection(connString)
        cndp.Open()
        Dim cmddp As New SqlCommand("Select * from Providers where ID = " & ProviderID, cndp)
        cmddp.CommandType = CommandType.Text
        Dim drdp As SqlDataReader = cmddp.ExecuteReader
        If drdp.HasRows Then
            While drdp.Read
                If drdp("IsIndividual") Is DBNull.Value Then
                    If drdp("FirstName") IsNot DBNull.Value Then
                        chkIndividual.Checked = True
                    Else
                        chkIndividual.Checked = False
                    End If
                Else
                    chkIndividual.Checked = drdp("IsIndividual")
                End If
                txtProviderID.Text = drdp("ID")
                If drdp("Active") IsNot DBNull.Value Then
                    chkActive.Checked = drdp("Active")
                Else
                    chkActive.Checked = True
                End If
                txtLName.Text = drdp("LastName_BSN")
                If drdp("FirstName") IsNot DBNull.Value Then txtFName.Text = drdp("FirstName")
                If drdp("MiddleName") IsNot DBNull.Value Then txtMName.Text = drdp("MiddleName")
                If drdp("Degree") IsNot DBNull.Value Then txtDegree.Text = drdp("Degree")
                If drdp("SSN") IsNot DBNull.Value Then txtSSN.Text = drdp("SSN")
                If drdp("NPI") IsNot DBNull.Value Then txtNPI.Text = Trim(drdp("Npi"))
                If drdp("UPIN") IsNot DBNull.Value Then txtUPIN.Text = Trim(drdp("UPIN"))
                If drdp("CLIA") IsNot DBNull.Value Then txtCLIA.Text = Trim(drdp("CLIA"))
                If drdp("POS_Code") IsNot DBNull.Value Then txtPOS.Text = Trim(drdp("POS_Code"))
                If drdp("Medicare") IsNot DBNull.Value Then txtMCR.Text = drdp("Medicare")
                If drdp("Medicaid") IsNot DBNull.Value Then txtMCD.Text = drdp("Medicaid")
                If drdp("BCBS") IsNot DBNull.Value Then txtBCBS.Text = drdp("BCBS")
                If drdp("License") IsNot DBNull.Value Then txtLicense.Text = drdp("License")
                If drdp("address_ID") IsNot DBNull.Value Then
                    txtAdd1.Text = GetAddress1(drdp("address_ID"))
                    txtAdd2.Text = GetAddress2(drdp("address_ID"))
                    txtCity.Text = GetAddressCity(drdp("Address_ID"))
                    txtState.Text = GetAddressState(drdp("Address_ID"))
                    txtZip.Text = GetAddressZip(drdp("Address_ID"))
                    txtCountry.Text = GetAddressCountry(drdp("Address_ID"))
                End If
                If drdp("Contact") IsNot DBNull.Value Then txtContact.Text = drdp("Contact")
                If drdp("Email") IsNot DBNull.Value _
                AndAlso drdp("Email") <> "" Then
                    txtEmail.Text = drdp("Email")
                    chkEmail.Enabled = True
                Else
                    chkEmail.Checked = False
                    chkEmail.Enabled = False
                End If
                If drdp("UserName") IsNot DBNull.Value Then txtUserName.Text = drdp("UserName")
                If drdp("Password") IsNot DBNull.Value Then txtPassword.Text = drdp("Password")
                If txtUserName.Text <> "" And txtPassword.Text <> "" Then
                    chkUserMgmt.Enabled = True
                    chkUserMgmt.Checked = drdp("ClientUserMgmt")
                Else
                    chkUserMgmt.Checked = False
                End If
                chkUseMyReports.Checked = drdp("UseMyReports")
                If drdp("Phone") IsNot DBNull.Value Then txtPhone.Text = drdp("Phone")
                If drdp("Fax") IsNot DBNull.Value _
                AndAlso drdp("Fax") <> "" Then
                    txtFax.Text = drdp("Fax")
                    chkFax.Enabled = True
                Else
                    chkFax.Checked = False
                    chkFax.Enabled = False
                End If
                If HasInterface(Val(txtProviderID.Text)) Then
                    chkInterface.Enabled = True
                Else
                    chkInterface.Enabled = False
                End If
                If drdp("Cell") IsNot DBNull.Value Then txtCell.Text = drdp("Cell")
                If drdp("Note") IsNot DBNull.Value Then txtNote.Text = drdp("Note")
                If drdp("Panic_Instructions") Is DBNull.Value Then
                    txtPanicInstructions.Text = ""
                Else
                    txtPanicInstructions.Rtf = drdp("Panic_Instructions")
                End If
                If drdp("ExtComment") IsNot DBNull.Value Then txtExtCmnt.Text = drdp("ExtComment")
                If drdp("ResRPTFile") IsNot DBNull.Value Then
                    txtResRPTFile.Text = drdp("ResRPTFile")
                Else
                    txtResRPTFile.Text = ""
                End If
                If drdp("InvoiceRPTFile") IsNot DBNull.Value _
                AndAlso Trim(drdp("InvoiceRPTFile")) <> "" Then
                    txtInvoiceRPTFile.Text = Trim(drdp("InvoiceRPTFile"))
                Else
                    txtInvoiceRPTFile.Text = "Provider Invoice.rpt"
                End If
                If drdp("PanicRDM_ID") IsNot DBNull.Value Then cmbPanic.SelectedIndex = drdp("PanicRDM_ID")
                If drdp("RegularRDM_ID") IsNot DBNull.Value Then cmbRegular.SelectedIndex = drdp("RegularRDM_ID")
                If drdp("RptComplete") IsNot DBNull.Value Then chkComplete.Checked = drdp("RptComplete")
                If drdp("Alert") IsNot DBNull.Value Then txtAlert.Rtf = drdp("Alert")
                chkCS.Checked = drdp("Alert_CS")
                chkAcc.Checked = drdp("Alert_Acc")
                If drdp("Report_ID") IsNot DBNull.Value Then
                    'For i = 0 To cmbReport.Items.Count - 1
                    '    ItemX = cmbReport.Items(i)
                    '    If Rs.Fields("Report_ID").Value = ItemX.ItemData Then
                    cmbReport.SelectedIndex = drdp("Report_ID")
                    '        Exit For
                    '    End If
                    'Next
                End If
                If drdp("RptCopies") IsNot DBNull.Value Then txtCopies.Text = drdp("RptCopies")
                If drdp("PickUp") IsNot DBNull.Value Then cmbPickUp.SelectedIndex = drdp("Pickup")
                If drdp("SetExtend") IsNot DBNull.Value _
                Then chkSetExt.Checked = drdp("SetExtend")
                If drdp("Rpt_Autoprint_Start") IsNot DBNull.Value AndAlso drdp("Rpt_Autoprint_Start") <> "" Then
                    txtAutoTime.Text = drdp("Rpt_Autoprint_Start")
                Else
                    txtAutoTime.Text = "06:00 PM"
                End If
                If drdp("RptComp_Span") IsNot DBNull.Value Then
                    txtCompSpan.Text = drdp("RptComp_Span").ToString
                Else
                    txtCompSpan.Text = "7"
                End If
                If drdp("RptIncomp_Span") IsNot DBNull.Value Then
                    txtIncSpan.Text = drdp("RptIncomp_Span").ToString
                Else
                    txtIncSpan.Text = "2"
                End If
                If drdp("SalesPerson_ID") IsNot DBNull.Value Then
                    For i As Integer = 0 To cmbSales.Items.Count - 1
                        ItemX = cmbSales.Items(i)
                        If drdp("SalesPerson_ID") = ItemX.ItemData Then
                            cmbSales.SelectedIndex = i
                            Exit For
                        End If
                    Next
                End If
                If drdp("Route_ID") IsNot DBNull.Value Then
                    For i As Integer = 0 To cmbRoutes.Items.Count - 1
                        ItemX = cmbRoutes.Items(i)
                        If drdp("Route_ID") = ItemX.ItemData Then
                            cmbRoutes.SelectedIndex = i
                            Exit For
                        End If
                    Next
                End If
                '
                chkAuto.Checked = drdp("RDM_Auto")
                If LIC.Fax = True Then
                    chkFax.Checked = drdp("RDM_Fax")
                    chkFax.Enabled = True
                Else
                    chkFax.Checked = False
                    chkFax.Enabled = False
                End If
                chkServerPDF.Checked = drdp("ServerPDF")
                chkPrint.Checked = drdp("RDM_Print")
                chkEmail.Checked = drdp("RDM_Email")
                chkInterface.Checked = drdp("RDM_Interface")
                chkProlison.Checked = drdp("RDM_Prolison")
                chkChartRequired.Checked = drdp("EMRNoRequired")
                chkPatPhRequired.Checked = drdp("PatPhRequired")
                chkAccConsolidate.Checked = drdp("AccConsolidate")
                chkUseESRD.Checked = drdp("UseESRD")
                cmbDefaultBilling.SelectedIndex = drdp("DefaultBilling")
                cmbDxSearch.SelectedIndex = drdp("DxSearchDefault")
                chkDOCRequired.Checked = drdp("DOCRequired")
                chkBlockDemograph.Checked = drdp("BlockDemograph")
                chkORNecessity.Checked = drdp("ORNecessity")
                '
                If drdp("MonStart") IsNot DBNull.Value Then dgvHours.Rows(0).Cells(1).Value = Trim(drdp("MonStart"))
                If drdp("MonLunchStart") IsNot DBNull.Value Then dgvHours.Rows(0).Cells(2).Value = Trim(drdp("MonLunchStart"))
                If drdp("MonLunchStop") IsNot DBNull.Value Then dgvHours.Rows(0).Cells(3).Value = Trim(drdp("MonLunchStop"))
                If drdp("MonStop") IsNot DBNull.Value Then dgvHours.Rows(0).Cells(4).Value = Trim(drdp("MonStop"))
                If drdp("TueStart") IsNot DBNull.Value Then dgvHours.Rows(1).Cells(1).Value = Trim(drdp("TueStart"))
                If drdp("TueLunchStart") IsNot DBNull.Value Then dgvHours.Rows(1).Cells(2).Value = Trim(drdp("TueLunchStart"))
                If drdp("TueLunchStop") IsNot DBNull.Value Then dgvHours.Rows(1).Cells(3).Value = Trim(drdp("TueLunchStop"))
                If drdp("TueStop") IsNot DBNull.Value Then dgvHours.Rows(1).Cells(4).Value = Trim(drdp("TueStop"))
                If drdp("WedStart") IsNot DBNull.Value Then dgvHours.Rows(2).Cells(1).Value = Trim(drdp("WedStart"))
                If drdp("WedLunchStart") IsNot DBNull.Value Then dgvHours.Rows(2).Cells(2).Value = Trim(drdp("WedLunchStart"))
                If drdp("WedLunchStop") IsNot DBNull.Value Then dgvHours.Rows(2).Cells(3).Value = Trim(drdp("WedLunchStop"))
                If drdp("WedStop") IsNot DBNull.Value Then dgvHours.Rows(2).Cells(4).Value = Trim(drdp("WedStop"))
                If drdp("ThuStart") IsNot DBNull.Value Then dgvHours.Rows(3).Cells(1).Value = Trim(drdp("ThuStart"))
                If drdp("ThuLunchStart") IsNot DBNull.Value Then dgvHours.Rows(3).Cells(2).Value = Trim(drdp("ThuLunchStart"))
                If drdp("ThuLunchStop") IsNot DBNull.Value Then dgvHours.Rows(3).Cells(3).Value = Trim(drdp("ThuLunchStop"))
                If drdp("ThuStop") IsNot DBNull.Value Then dgvHours.Rows(3).Cells(4).Value = Trim(drdp("ThuStop"))
                If drdp("FriStart") IsNot DBNull.Value Then dgvHours.Rows(4).Cells(1).Value = Trim(drdp("FriStart"))
                If drdp("FriLunchStart") IsNot DBNull.Value Then dgvHours.Rows(4).Cells(2).Value = Trim(drdp("FriLunchStart"))
                If drdp("FriLunchStop") IsNot DBNull.Value Then dgvHours.Rows(4).Cells(3).Value = Trim(drdp("FriLunchStop"))
                If drdp("FriStop") IsNot DBNull.Value Then dgvHours.Rows(4).Cells(4).Value = Trim(drdp("FriStop"))
                If drdp("SatStart") IsNot DBNull.Value Then dgvHours.Rows(5).Cells(1).Value = Trim(drdp("SatStart"))
                If drdp("SatLunchStart") IsNot DBNull.Value Then dgvHours.Rows(5).Cells(2).Value = Trim(drdp("SatLunchStart"))
                If drdp("SatLunchStop") IsNot DBNull.Value Then dgvHours.Rows(5).Cells(3).Value = Trim(drdp("SatLunchStop"))
                If drdp("SatStop") IsNot DBNull.Value Then dgvHours.Rows(5).Cells(4).Value = Trim(drdp("SatStop"))
                If drdp("SunStart") IsNot DBNull.Value Then dgvHours.Rows(6).Cells(1).Value = Trim(drdp("SunStart"))
                If drdp("SunLunchStart") IsNot DBNull.Value Then dgvHours.Rows(6).Cells(2).Value = Trim(drdp("SunLunchStart"))
                If drdp("SunLunchStop") IsNot DBNull.Value Then dgvHours.Rows(6).Cells(3).Value = Trim(drdp("SunLunchStop"))
                If drdp("SunStop") IsNot DBNull.Value Then dgvHours.Rows(6).Cells(4).Value = Trim(drdp("SunStop"))
                If drdp("ContractFrom") IsNot DBNull.Value Then dtpFrom.Value = drdp("ContractFrom")
                If drdp("ContractTo") IsNot DBNull.Value Then dtpTo.Value = drdp("ContractTo")
                If drdp("PriceLevel") IsNot DBNull.Value Then cmbPriceLevel.SelectedIndex = drdp("PriceLevel")
                If drdp("PickupNote") IsNot DBNull.Value Then txtPickupNote.Text = drdp("PickupNote")
                btnDelete.Enabled = True
            End While
        End If
        cndp.Close()
        cndp = Nothing
        DisplayAssociation(ProviderID)
        DisplayContract(ProviderID)
        DisplayProviderRanges(ProviderID)
    End Sub

    Private Sub DisplayProviderRanges(ByVal ProviderID As Long)
        dgvAGRanges.Rows.Clear()
        Dim cndpr As New SqlConnection(connString)
        cndpr.Open()
        Dim cmddpr As New SqlCommand("Select * from " &
        "Provider_Ranges where Provider_ID = " & ProviderID, cndpr)
        cmddpr.CommandType = CommandType.Text
        Dim drdpr As SqlDataReader = cmddpr.ExecuteReader
        If drdpr.HasRows Then
            While drdpr.Read
                dgvAGRanges.Rows.Add(System.Drawing.Image.FromFile(Application.StartupPath &
                "\Images\Eraser.ico"), drdpr("Test_ID"), Nothing, drdpr("AgeFrom"),
                drdpr("AgeTo"), drdpr("Sex"), drdpr("ValueFrom"),
                drdpr("ValueTo"), drdpr("Flag"), IIf(drdpr("Behavior") _
                Is DBNull.Value, "Ignore", drdpr("Behavior")))
            End While
        End If
        cndpr.Close()
        cndpr = Nothing
        dgvAGRanges.Rows.Add()
    End Sub

    Private Function HasInterface(ByVal ProviderID As Long) As Boolean
        Dim HL7PDF As Boolean = False
        Dim cnhi As New SqlConnection(connString)
        cnhi.Open()
        Dim cmdhi As New SqlCommand("Select * from " &
        "External_Interfaces where Facility_ID = " & ProviderID, cnhi)
        cmdhi.CommandType = CommandType.Text
        Dim drhi As SqlDataReader = cmdhi.ExecuteReader
        If drhi.HasRows Then HL7PDF = True
        cnhi.Close()
        cnhi = Nothing
        Return HL7PDF
    End Function

    Private Sub DisplayAssociation(ByVal ProviderID As Long)
        'Dim i As Integer
        dgvProviders.Rows.Clear()
        If chkIndividual.Checked = True Then    'individual
            Dim cnda As New SqlConnection(connString)
            cnda.Open()
            Dim cmdda As New SqlCommand("Select * from Clinic_Provider " &
            "where Clinic_ID <> " & ProviderID & " and Provider_ID = " & ProviderID, cnda)
            cmdda.CommandType = CommandType.Text
            Dim drda As SqlDataReader = cmdda.ExecuteReader
            If drda.HasRows Then
                While drda.Read
                    dgvProviders.Rows.Add(drda("Clinic_ID"),
                    GetProviderName(drda("Clinic_ID")), drda("Active"))
                End While
            End If
            cnda.Close()
            cnda = Nothing
        Else    'Entity
            Dim cnda As New SqlConnection(connString)
            cnda.Open()
            Dim cmdda As New SqlCommand("Select * from Clinic_Provider " &
            "where Provider_ID <> " & ProviderID & " and Clinic_ID = " & ProviderID, cnda)
            cmdda.CommandType = CommandType.Text
            Dim drda As SqlDataReader = cmdda.ExecuteReader
            If drda.HasRows Then
                While drda.Read
                    dgvProviders.Rows.Add(drda("Provider_ID"),
                    GetProviderName(drda("Provider_ID")), drda("Active"))
                End While
            End If
            cnda.Close()
            cnda = Nothing
        End If
    End Sub

    Private Sub DisplayContract(ByVal ProviderID As Long)
        Dim Img1 As Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Eraser.ico")
        Dim Img2 As Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Looks.ico")
        Dim Img3 As Image = Nothing
        dgvContract.Rows.Clear()
        'dgvContract.RowCount = 1
        Dim cndc1 As New SqlConnection(connString)
        cndc1.Open()
        Dim cmddc1 As New SqlCommand("Select a.TGP_ID as TGPID, (Select Name from Tests " &
        "where ID = a.TGP_ID Union Select Name from Groups where ID = a.TGP_ID Union Select Name " &
        "from Profiles where ID = a.TGP_ID) as Component, (Select ComponentType from Tests where " &
        "ID = a.TGP_ID Union Select ComponentType from Groups where ID = a.TGP_ID Union Select " &
        "ComponentType from Profiles where ID = a.TGP_ID) as ComponentType, a.CPT_Code, " &
        "a.IsDistinct, a.Price from Provider_TGP a where a.Provider_ID = " & ProviderID &
        " order by a.Ordinal", cndc1)
        cmddc1.CommandType = CommandType.Text
        Dim drdc1 As SqlDataReader = cmddc1.ExecuteReader
        If drdc1.HasRows Then
            While drdc1.Read
                If drdc1("ComponentType") IsNot DBNull.Value _
                AndAlso drdc1("ComponentType") = "T" Then
                    Img3 = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\test.ico")
                ElseIf drdc1("ComponentType") IsNot DBNull.Value _
                AndAlso drdc1("ComponentType") = "G" Then
                    Img3 = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\group.ico")
                Else
                    Img3 = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\profile.ico")
                End If
                dgvContract.Rows.Add(Img1, drdc1("TGPID"), Img2,
                drdc1("Component"), Img3, drdc1("CPT_Code"), drdc1("IsDistinct"), drdc1("Price"))
            End While
            dgvContract.Rows.Add()
        Else
            dgvContract.Rows.Add()
        End If
        cndc1.Close()
        cndc1 = Nothing
    End Sub

    Private Sub PopulateHours()
        dgvHours.Rows.Add("Monday", "OFF", "OFF", "OFF", "OFF")
        dgvHours.Rows.Add("Tuesday", "OFF", "OFF", "OFF", "OFF")
        dgvHours.Rows.Add("Wednesday", "OFF", "OFF", "OFF", "OFF")
        dgvHours.Rows.Add("Thursday", "OFF", "OFF", "OFF", "OFF")
        dgvHours.Rows.Add("Friday", "OFF", "OFF", "OFF", "OFF")
        dgvHours.Rows.Add("Saturday", "OFF", "OFF", "OFF", "OFF")
        dgvHours.Rows.Add("Sunday", "OFF", "OFF", "OFF", "OFF")
    End Sub

    Private Sub chkActive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkActive.CheckedChanged
        If chkActive.Checked = True Then
            chkActive.Text = "Active"
        Else
            chkActive.Text = "Inactive"
        End If
        Update_Progress()
    End Sub

    Private Sub txtProviderID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtProviderID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtProviderID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProviderID.Validated
        If txtProviderID.Text <> "" Then
            Dim ProvID As Long = Val(txtProviderID.Text)
            If chkEditNew.Checked = False Then 'Edit
                If IsProviderIDUnique(ProvID) = True Then
                    MsgBox("Invalid provider ID", MsgBoxStyle.Critical, "Prolis")
                    ProvID = -1
                    txtProviderID.Text = ""
                    txtProviderID.Focus()
                Else
                    FormClear()
                    DisplayProvider(ProvID)
                End If
            Else    'Add mode
                If IsProviderIDUnique(ProvID) = False Then
                    MsgBox("The provider ID has been used already.", MsgBoxStyle.Critical, "Prolis")
                    ProvID = -1
                    txtProviderID.Text = ""
                    txtProviderID.Focus()
                Else
                    DisplayProvider(ProvID)
                End If
            End If
        End If
        Update_Progress()
    End Sub

    Private Function IsProviderIDUnique(ByVal ProviderID As Long) As Boolean
        Dim unique As Boolean = True
        Dim cnda As New SqlConnection(connString)
        cnda.Open()
        Dim cmdda As New SqlCommand("Select * " &
        "from Providers where ID = " & ProviderID, cnda)
        cmdda.CommandType = CommandType.Text
        Dim drda As SqlDataReader = cmdda.ExecuteReader
        If drda.HasRows Then unique = False
        cnda.Close()
        cnda = Nothing
        Return unique
    End Function

    Private Sub txtLName_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLName.Validated
        Update_Progress()
    End Sub

    Private Sub btnProvLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProvLook.Click
        Dim ProviderID As String = frmProviderLookup.ShowDialog()
        If ProviderID <> "" Then
            FormClear()
            DisplayProvider(Val(ProviderID))
            Update_Progress()
        End If
    End Sub

    Private Sub dgvContract_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvContract.CellClick
        If e.RowIndex <> -1 Then
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
                End If
            ElseIf e.ColumnIndex = 2 Then
                frmTGPLookup.ShowDialog()
                If frmTGPLookup.Tag.ToString <> "" Then
                    Dim cndc As New SqlConnection(connString)
                    cndc.Open()
                    Dim cmddc As New SqlCommand("Select ID, Name, ComponentType, CPT_Code, ListPrice " &
                    "from Tests where ID = " & frmTGPLookup.Tag & " Union Select ID, Name, ComponentType, CPT_Code, " &
                    "ListPrice from Groups where ID = " & frmTGPLookup.Tag & " Union Select ID, Name, " &
                    "ComponentType, CPT_Code, ListPrice from Profiles where ID = " & frmTGPLookup.Tag, cndc)
                    cmddc.CommandType = CommandType.Text
                    Dim drdc As SqlDataReader = cmddc.ExecuteReader
                    If drdc.HasRows Then
                        While drdc.Read
                            If drdc("ID") IsNot DBNull.Value Then
                                dgvContract.Rows(e.RowIndex).Cells(0).Value =
                                System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Eraser.ico")
                                dgvContract.Rows(e.RowIndex).Cells(1).Value = drdc("ID")
                                dgvContract.Rows(e.RowIndex).Cells(3).Value = drdc("Name")
                                If drdc("ComponentType") = "T" Then
                                    dgvContract.Rows(e.RowIndex).Cells(4).Value =
                                    System.Drawing.Image.FromFile(Application.StartupPath & "\Images\test.ico")
                                ElseIf drdc("ComponentType") = "G" Then
                                    dgvContract.Rows(e.RowIndex).Cells(4).Value =
                                    System.Drawing.Image.FromFile(Application.StartupPath & "\Images\group.ico")
                                Else
                                    dgvContract.Rows(e.RowIndex).Cells(4).Value =
                                    System.Drawing.Image.FromFile(Application.StartupPath & "\Images\profile.ico")
                                End If
                                If drdc("CPT_Code") IsNot DBNull.Value Then _
                                dgvContract.Rows(e.RowIndex).Cells(5).Value = Trim(drdc("CPT_Code"))
                                dgvContract.Rows(e.RowIndex).Cells(7).Value = Format(drdc("ListPrice"), "##,##0.00")
                                If e.RowIndex = dgvContract.RowCount - 1 Then dgvContract.Rows.Add()
                                Exit While
                            End If
                        End While
                    End If
                    cndc.Close()
                    cndc = Nothing
                End If
            End If
        End If
    End Sub

    Private Sub dgvContract_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvContract.CellEndEdit
        If e.ColumnIndex = 1 Then   'ID
            If IsNumeric(dgvContract.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) = True Then
                Dim cndc As New SqlConnection(connString)
                cndc.Open()
                Dim cmddc As New SqlCommand("Select ID, Name, ComponentType, CPT_Code, ListPrice " &
                "from Tests where ID = " & dgvContract.Rows(e.RowIndex).Cells(e.ColumnIndex).Value &
                " Union Select ID, Name, ComponentType, CPT_Code, ListPrice from Groups where ID = " &
                dgvContract.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & " Union Select ID, Name, " &
                "ComponentType, CPT_Code, ListPrice from Profiles where ID = " &
                dgvContract.Rows(e.RowIndex).Cells(e.ColumnIndex).Value, cndc)
                cmddc.CommandType = CommandType.Text
                Dim drdc As SqlDataReader = cmddc.ExecuteReader
                If drdc.HasRows Then
                    While drdc.Read
                        If drdc("ID") IsNot DBNull.Value Then
                            dgvContract.Rows(e.RowIndex).Cells(0).Value =
                            System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Eraser.ico")
                            dgvContract.Rows(e.RowIndex).Cells(3).Value = drdc("Name")
                            If drdc("ComponentType") = "T" Then
                                dgvContract.Rows(e.RowIndex).Cells(4).Value =
                                System.Drawing.Image.FromFile(Application.StartupPath & "\Images\test.ico")
                            ElseIf drdc("ComponentType") = "G" Then
                                dgvContract.Rows(e.RowIndex).Cells(4).Value =
                                System.Drawing.Image.FromFile(Application.StartupPath & "\Images\group.ico")
                            Else
                                dgvContract.Rows(e.RowIndex).Cells(4).Value =
                                System.Drawing.Image.FromFile(Application.StartupPath & "\Images\profile.ico")
                            End If
                            If drdc("CPT_Code") IsNot DBNull.Value Then _
                            dgvContract.Rows(e.RowIndex).Cells(5).Value = Trim(drdc("CPT_Code"))
                            dgvContract.Rows(e.RowIndex).Cells(7).Value = Format(drdc("ListPrice"), "##,##0.00")
                            If e.RowIndex = dgvContract.RowCount - 1 Then dgvContract.Rows.Add()
                            Exit While
                        End If
                    End While
                Else
                    If dgvContract.Rows(e.RowIndex).Cells(3).Value = "" Then
                        MsgBox("Type a valid component ID", MsgBoxStyle.Critical, "Prolis")
                        dgvContract.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
                    End If
                End If
                cndc.Close()
                cndc = Nothing
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnSales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSales.Click
        frmSalesPersons.ShowDialog()
        PopulateSales()
    End Sub

    Private Sub btnRoutes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRoutes.Click
        Dim RouteID As String = frmRoutes.ShowDialog()
        If RouteID <> "" Then PopulateRoutes(Val(RouteID))
    End Sub

    Private Sub txtCopies_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCopies.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtCopies_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCopies.Validated
        If txtCopies.Text = "" Then txtCopies.Text = "1"
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim MyFont As New FontDialog
        MyFont.Font = txtAlert.SelectionFont
        Dim RetVal As Integer = MyFont.ShowDialog
        If RetVal = System.Windows.Forms.DialogResult.OK Then
            txtAlert.SelectionFont = MyFont.Font
        End If
        MyFont = Nothing
    End Sub

    Private Sub btnBG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBG.Click
        Dim MyBG As New ColorDialog
        MyBG.Color = txtAlert.SelectionBackColor
        Dim RetVal As Integer = MyBG.ShowDialog
        If RetVal = System.Windows.Forms.DialogResult.OK Then
            txtAlert.SelectionBackColor = MyBG.Color
        End If
        MyBG = Nothing
    End Sub

    Private Sub btnColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnColor.Click
        Dim MyFC As New ColorDialog
        MyFC.Color = txtAlert.SelectionColor
        Dim RetVal As Integer = MyFC.ShowDialog
        If RetVal = System.Windows.Forms.DialogResult.OK Then
            txtAlert.SelectionColor = MyFC.Color
        End If
        MyFC = Nothing
    End Sub

    Private Sub btnDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDefault.Click
        txtAlert.SelectionFont = AlertFont
        txtAlert.SelectionBackColor = Color.White
        txtAlert.SelectionColor = Color.Black
    End Sub


    Private Sub chkSetExt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSetExt.CheckedChanged
        If chkSetExt.Checked = False Then
            chkSetExt.Text = "No"
        Else
            chkSetExt.Text = "Yes"
        End If
    End Sub

    Private Sub txtNPI_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNPI.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtNPI_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNPI.Validated
        If txtNPI.Text <> "" Then
            If Len(txtNPI.Text) <> 10 Then
                MsgBox("The field requires 10 digits NPI. Your input is not valid", MsgBoxStyle.Critical, "Prolis")
                txtNPI.Focus()
            End If
        End If
        Update_Progress()
    End Sub

    Private Sub btnCopyContract_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyContract.Click
        Dim ProvId As String = frmProviderLookup.ShowDialog
        If ProvId <> "" Then
            DisplayContract(Val(ProvId))
        End If
    End Sub

    Private Sub btnRptLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRptLook.Click
        Dim DLG As New OpenFileDialog
        DLG.InitialDirectory = My.Application.Info.DirectoryPath & "\Reports\"
        DLG.Filter = "Crystal Report File (*.RPT)|*.rpt"
        If MsgBoxResult.Ok = DLG.ShowDialog Then
            Dim TargetPath As String = My.Application.Info.DirectoryPath & "\Reports\"
            Dim RPTFILE As String = Microsoft.VisualBasic.Mid(DLG.FileName,
            InStrRev(DLG.FileName, "\") + 1)
            If TargetPath <> DLG.InitialDirectory Then _
            IO.File.Copy(DLG.FileName, TargetPath & RPTFILE, True)
            txtResRPTFile.Text = RPTFILE
        Else
            txtResRPTFile.Text = ""
        End If
        DLG.Dispose()
        DLG = Nothing
    End Sub

    Private Sub txtEmail_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmail.Validated
        If Trim(txtEmail.Text) <> "" Then
            chkEmail.Enabled = True
        Else
            chkEmail.Checked = False
            chkEmail.Enabled = False
        End If
    End Sub

    Private Sub txtFax_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFax.Validated
        If Trim(txtFax.Text) <> "" Then
            chkFax.Enabled = True
        Else
            chkFax.Checked = False
            chkFax.Enabled = False
        End If
    End Sub

    Private Sub dgvAGRanges_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAGRanges.CellContentClick
        If e.ColumnIndex = 0 Then   'Eraser
            If (dgvAGRanges.Rows(e.RowIndex).Cells(1).Value IsNot Nothing _
            AndAlso dgvAGRanges.Rows(e.RowIndex).Cells(1).Value.ToString <> "") Then
                If e.RowIndex < dgvAGRanges.RowCount - 1 Then
                    dgvAGRanges.Rows.RemoveAt(e.RowIndex)
                Else
                    dgvAGRanges.Rows(e.RowIndex).Cells(0).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath &
                    "\Images\Blank.ico")
                    dgvAGRanges.Rows(e.RowIndex).Cells(1).Value = ""
                    dgvAGRanges.Rows(e.RowIndex).Cells(3).Value = ""
                    dgvAGRanges.Rows(e.RowIndex).Cells(4).Value = ""
                    dgvAGRanges.Rows(e.RowIndex).Cells(6).Value = ""
                    dgvAGRanges.Rows(e.RowIndex).Cells(7).Value = ""
                End If
                btnSave.Enabled = True
            End If
        ElseIf e.ColumnIndex = 2 Then   'Lookup
            Dim TestID As String = frmTestLookup.ShowDialog
            If TestID <> "" Then
                dgvAGRanges.Rows(e.RowIndex).Cells(0).Value =
                System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Eraser.ico")
                dgvAGRanges.Rows(e.RowIndex).Cells(1).Value = TestID
                dgvAGRanges.Rows.Add()
            End If
        End If
    End Sub

    Private Sub dgvAGRanges_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAGRanges.CellEndEdit
        If e.ColumnIndex = 1 Then
            If Trim(dgvAGRanges.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) <> "" AndAlso
            IsNumeric(Trim(dgvAGRanges.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)) Then
                If Not IsLineDuplicate(e.RowIndex) Then
                    Dim cnag As New SqlConnection(connString)
                    cnag.Open()
                    Dim cmdag As New SqlCommand("Select ID from Tests where IsActive <> 0 " &
                    "and ID = " & Val(Trim(dgvAGRanges.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)), cnag)
                    cmdag.CommandType = CommandType.Text
                    Dim drag As SqlDataReader = cmdag.ExecuteReader
                    If drag.HasRows Then
                        While drag.Read
                            dgvAGRanges.Rows(e.RowIndex).Cells(0).Value =
                            System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Eraser.ico")
                            dgvAGRanges.Rows(e.RowIndex).Cells(1).Value = drag("ID").ToString
                            If e.RowIndex = dgvAGRanges.RowCount - 1 Then dgvAGRanges.Rows.Add()
                        End While
                    Else
                        MsgBox("Invalid Test ID", MsgBoxStyle.Critical, "Prolis")
                        dgvAGRanges.Rows(e.RowIndex).Cells(1).Value = ""
                    End If
                    cnag.Close()
                    cnag = Nothing
                Else
                    MsgBox("Duplicate ranges can not be inserted.", MsgBoxStyle.Critical, "Prolis")
                    dgvAGRanges.Rows(e.RowIndex).Cells(1).Value = ""
                    dgvAGRanges.Rows(e.RowIndex).Cells(3).Value = ""
                    dgvAGRanges.Rows(e.RowIndex).Cells(4).Value = ""
                    dgvAGRanges.Rows(e.RowIndex).Cells(6).Value = ""
                    dgvAGRanges.Rows(e.RowIndex).Cells(7).Value = ""
                End If
            Else
                dgvAGRanges.Rows(e.RowIndex).Cells(0).Value =
                System.Drawing.Image.FromFile(Application.StartupPath &
                "\Images\Blank.ico")
                'If e.RowIndex < dgvAGRanges.RowCount - 1 _
                'Then dgvAGRanges.Rows.RemoveAt(e.RowIndex)
            End If
            If e.ColumnIndex = 3 Or e.ColumnIndex = 4 Or e.ColumnIndex = 6 Or e.ColumnIndex = 7 Then
                If IsNumeric(dgvAGRanges.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) = False Then
                    MsgBox("Allowed characters for this field, are '0123456789'",
                    MsgBoxStyle.Critical, "Prolis")
                    dgvAGRanges.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
                End If
            End If
        End If
    End Sub

    Private Function IsLineDuplicate(ByVal RowIndex As Integer) As Boolean
        Dim IsDup As Boolean = False
        Dim i As Integer
        For i = 0 To dgvAGRanges.RowCount - 1
            If i <> RowIndex Then
                If (dgvAGRanges.Rows(i).Cells(1).Value IsNot Nothing _
                AndAlso dgvAGRanges.Rows(i).Cells(1).Value.ToString <> "") And
                (dgvAGRanges.Rows(i).Cells(3).Value IsNot Nothing _
                AndAlso dgvAGRanges.Rows(i).Cells(3).Value.ToString <> "") And
                (dgvAGRanges.Rows(i).Cells(4).Value IsNot Nothing _
                AndAlso dgvAGRanges.Rows(i).Cells(4).Value.ToString <> "") And
                (dgvAGRanges.Rows(i).Cells(5).Value IsNot Nothing _
                AndAlso dgvAGRanges.Rows(i).Cells(5).Value <> "") And
                (dgvAGRanges.Rows(i).Cells(6).Value IsNot Nothing _
                AndAlso dgvAGRanges.Rows(i).Cells(6).Value.ToString <> "") And
                (dgvAGRanges.Rows(i).Cells(7).Value IsNot Nothing _
                AndAlso dgvAGRanges.Rows(i).Cells(7).Value.ToString <> "") Then
                    If dgvAGRanges.Rows(i).Cells(1).Value = dgvAGRanges.Rows(RowIndex).Cells(1).Value _
                    And dgvAGRanges.Rows(i).Cells(3).Value = dgvAGRanges.Rows(RowIndex).Cells(3).Value _
                    And dgvAGRanges.Rows(i).Cells(4).Value = dgvAGRanges.Rows(RowIndex).Cells(4).Value _
                    And dgvAGRanges.Rows(i).Cells(5).Value = dgvAGRanges.Rows(RowIndex).Cells(5).Value _
                    And dgvAGRanges.Rows(i).Cells(6).Value = dgvAGRanges.Rows(RowIndex).Cells(6).Value _
                    And dgvAGRanges.Rows(i).Cells(7).Value = dgvAGRanges.Rows(RowIndex).Cells(7).Value Then
                        IsDup = True
                        Exit For
                    End If
                End If
            End If
        Next
        Return IsDup
    End Function

    Private Sub dgvAGRanges_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvAGRanges.DataError
        On Error Resume Next
    End Sub

    Private Sub txtLicense_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLicense.Validated
        Update_Progress()
    End Sub

    Private Sub txtUPIN_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUPIN.Validated
        Update_Progress()
    End Sub

    Private Sub btnPIColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPIColor.Click
        Dim MyFC As New ColorDialog
        MyFC.Color = txtPanicInstructions.SelectionColor
        Dim RetVal As Integer = MyFC.ShowDialog
        If RetVal = System.Windows.Forms.DialogResult.OK Then
            txtPanicInstructions.SelectionColor = MyFC.Color
        End If
        MyFC = Nothing
    End Sub

    Private Sub btnPIFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPIFont.Click
        Dim MyFont As New FontDialog
        MyFont.Font = txtPanicInstructions.SelectionFont
        Dim RetVal As Integer = MyFont.ShowDialog
        If RetVal = System.Windows.Forms.DialogResult.OK Then
            txtPanicInstructions.SelectionFont = MyFont.Font
        End If
        MyFont = Nothing
    End Sub

    Private Sub txtUserName_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserName.Validated
        If txtProviderID.Text <> "" Then
            If Trim(txtUserName.Text) = "" Then
                MsgBox("Leaving this field blank, would invalidate the client access to the Outreach")
                chkUserMgmt.Checked = False
                chkUserMgmt.Enabled = False
            Else
                If Trim(txtPassword.Text) <> "" Then chkUserMgmt.Enabled = True
            End If
        End If
    End Sub

    Private Sub txtPassword_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPassword.Validated
        If txtProviderID.Text <> "" Then
            If Trim(txtPassword.Text) = "" Then
                MsgBox("Leaving this field blank, would invalidate the client access to the Outreach")
                chkUserMgmt.Checked = False
                chkUserMgmt.Enabled = False
            Else
                If Trim(txtUserName.Text) <> "" Then chkUserMgmt.Enabled = True
            End If
        End If
    End Sub

    Private Sub btnDelORAccess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If txtProviderID.Text <> "" And txtUserName.Text <> "" And txtPassword.Text <> "" Then
            Dim RetVal As Integer = MsgBox("This operation will delete all the Outreach users " &
            "of this client. This procedure is not reversible, so please think before clicking" &
            " the 'Yes' button.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
            If RetVal = vbYes Then
                txtUserName.Text = ""
                txtPassword.Text = ""
                ExecuteSqlProcedure("Delete from Client_Users where Provider_ID = " & Val(txtProviderID.Text) _
                & " and Not ClientUser_ID in(Select Edited_By from Orders where EntrySource_ID = 1 " _
                & "and OrderingProvider_ID = " & Val(txtProviderID.Text) & " Union Select " &
                "AccessionedBy from Requisitions where AccessionLoc_ID = 1 and OrderingProvider_ID " _
                & "= " & Val(txtProviderID.Text) & ")")
            End If
        End If
    End Sub

    Private Sub dgvProviders_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProviders.CellDoubleClick
        If e.RowIndex <> -1 Then
            txtAssID.Text = dgvProviders.Rows(e.RowIndex).Cells(0).Value
            txtAssName.Text = dgvProviders.Rows(e.RowIndex).Cells(1).Value
            chkAssActive.Checked = dgvProviders.Rows(e.RowIndex).Cells(2).Value
            btnAssDel.Enabled = True
            btnAssAdd.Enabled = True
        End If
    End Sub

    Private Sub btnAssDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAssDel.Click
        If txtAssID.Text = dgvProviders.SelectedRows(0).Cells(0).Value Then
            Dim RetVal As Integer = MsgBox("You are about to delete the association " &
            "record along with its Requisition and the label printing settings. This " &
            "action may affect adversely the accession records created earlier using " &
            "this association. A safer way is to inactivate it. Are you sure to " &
            "delete it?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo, "Prolis")
            If RetVal = vbYes Then
                If chkIndividual.Checked = True Then    'Individual
                    ExecuteSqlProcedure("Delete from Clinic_Provider where Provider_ID = " &
                    txtProviderID.Text & " and Clinic_ID = " & txtAssID.Text)
                Else    'Entity
                    ExecuteSqlProcedure("Delete from Clinic_Provider where Clinic_ID = " &
                    txtProviderID.Text & " and Provider_ID = " & txtAssID.Text)
                End If
                dgvProviders.Rows.RemoveAt(dgvProviders.SelectedRows(0).Index)
                txtAssID.Text = "" : txtAssName.Text = ""
                txtAssAddress.Text = "" : chkAssActive.Checked = True
                btnAssDel.Enabled = False : btnAssAdd.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnAssAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAssAdd.Click
        If txtAssID.Text <> "" And txtAssName.Text <> "" Then
            UpdateAssGrid(txtAssID.Text, txtAssName.Text, chkAssActive.Checked)
            txtAssID.Text = "" : txtAssName.Text = ""
            txtAssAddress.Text = "" : chkAssActive.Checked = True
            btnAssDel.Enabled = False : btnAssAdd.Enabled = False
        End If
    End Sub

    Private Sub UpdateAssGrid(ByVal AssID As Long,
    ByVal AssName As String, ByVal Active As Boolean)
        Dim Done As Boolean = False
        For i As Integer = 0 To dgvProviders.RowCount - 1
            If AssID = dgvProviders.Rows(i).Cells(0).Value Then
                dgvProviders.Rows(i).Cells(1).Value = AssName
                dgvProviders.Rows(i).Cells(2).Value = Active
                Done = True
                Exit For
            End If
        Next
        If Done = False Then
            dgvProviders.Rows.Add(AssID, AssName, Active)
        End If
    End Sub

    Private Sub chkAssActive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAssActive.CheckedChanged
        If chkAssActive.Checked = True Then
            chkAssActive.Text = "Active"
        Else
            chkAssActive.Text = "Inactive"
        End If
        AssProgress()
    End Sub

    Private Sub AssProgress()
        If txtAssID.Text <> "" And txtAssName.Text <> "" Then
            btnAssAdd.Enabled = True
            btnAssDel.Enabled = True
        Else
            btnAssAdd.Enabled = False
            btnAssDel.Enabled = False
        End If
    End Sub

    Private Sub txtAssID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAssID.Validated
        If txtAssID.Text <> "" AndAlso txtAssID.Text <> txtProviderID.Text Then
            txtAssName.Text = GetProviderName(txtAssID.Text)
            txtAssAddress.Text = ""
        Else
            If txtAssID.Text = txtProviderID.Text Then _
            MsgBox(" a provider can't be associated with itself")
            txtAssName.Text = ""
            txtAssAddress.Text = ""
        End If
        AssProgress()
    End Sub

    Private Sub btnExternalFeed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExternalFeed.Click
        Dim Ln As String ' Going to hold one line at a time
        Dim Delim As String = Chr(9)    'HT
        Dim AddressID As Long = -1
        Dim providerID As String = ""
        DialogResult = OpenFileDialog1.ShowDialog()
        If DialogResult = DialogResult.OK Then
            Dim SR As New IO.StreamReader(OpenFileDialog1.FileName)
            Do Until SR.EndOfStream
                Ln = SR.ReadLine
                If Ln IsNot Nothing AndAlso Ln <> "" Then
                    If InStr(Ln, Chr(9)) > 0 Then
                        Delim = Chr(9)
                    ElseIf InStr(Ln, ",") > 0 Then
                        Delim = ","
                    Else
                        MsgBox("Routine supports either the TAB or the coma, only. The file has an invalid format")
                        Exit Do
                    End If
                    Dim Fields() As String = Split(Ln, Delim)
                    If Fields.Length > 8 AndAlso (Fields(0) <> "" And Fields(1) <> "" And Fields(3) <> "") AndAlso
                    (InStr(Replace(Fields(0), " ", ""), "Last") = 0 And InStr(Replace(Fields(1), " ", ""), "First") = 0) Then
                        If Fields.Length > 15 AndAlso (Fields(9) <> "" And Fields(11) <> "" And Fields(12) <> "" And Fields(13) <> "") Then
                            AddressID = GetAddressID(Trim(Fields(9)), Trim(Fields(10)), Trim(Fields(11)), Trim(Fields(12)), Trim(Fields(13)), Trim(Fields(14)))
                        Else
                            AddressID = -1
                        End If
                        UpdateProvider(Trim(Fields(0)), Trim(Fields(1)), "", Trim(Fields(2)), Trim(Fields(3)), AddressID,
                        Trim(Fields(4)), Trim(Fields(5)), Trim(Fields(6)), Trim(Fields(7)), Trim(Fields(8)))
                        '
                        If Fields(15) <> "" AndAlso IsNumeric(Fields(15)) Then
                            providerID = GetProviderID(Fields(3))
                            If providerID <> "" Then
                                ExecuteSqlProcedure("If not Exists (Select * from Clinic_Provider where Clinic_ID = " &
                                Fields(15) & " and Provider_ID = " & providerID & ") Insert into Clinic_Provider " &
                                "(Clinic_ID, Provider_ID) values (" & Fields(15) & ", " & providerID & ")")
                            End If
                        End If
                    End If
                End If
            Loop
            SR.Close()
            SR = Nothing
        End If
    End Sub

    Private Function GetProviderID(ByVal NPI As String) As String
        Dim PID As String = ""
        Dim cnpid As New SqlConnection(connString)
        cnpid.Open()
        Dim cmdpid As New SqlCommand("Select ID from Providers where NPI = '" & NPI & "'", cnpid)
        cmdpid.CommandType = CommandType.Text
        Dim drpid As SqlDataReader = cmdpid.ExecuteReader
        If drpid.HasRows Then
            While drpid.Read
                PID = drpid("ID").ToString
            End While
        End If
        cnpid.Close()
        cnpid = Nothing
        Return PID
    End Function

    Private Sub UpdateProvider(ByVal LastName_BSN As String, ByVal FirstName As String, ByVal MiddleName _
    As String, ByVal Degree As String, ByVal NPI As String, ByVal AddressID As Long, ByVal Phone As String,
    ByVal Fax As String, ByVal Cell As String, ByVal Email As String, ByVal Contact As String)
        Dim sSQL As String = "If Exists (Select * from Providers where LastName_BSN = '" & LastName_BSN & "' and " &
        "FirstName = '" & FirstName & "' and NPI = '" & NPI & "') Update Providers set MiddleName = '" & MiddleName &
        "', Degree = '" & Degree & "', Address_ID = " & AddressID & ", Phone = '" & Phone & "', Fax = '" & Fax & "', " &
        "Cell = '" & Cell & "', Email = '" & Email & "', Contact = '" & Contact & "' where LastName_BSN = '" & LastName_BSN &
        "' and FirstName = '" & FirstName & "' and NPI = '" & NPI & "' Else Insert into Providers (ID, LastName_BSN, " &
        "FirstName, MiddleName, Degree, IsIndividual, Address_ID, NPI, Phone, Fax, Cell, Email, Contact) values (" &
        NextProviderID() & ", '" & LastName_BSN & "', '" & FirstName & "', '" & MiddleName & "', '" & Degree & "', 1, " &
        AddressID & ", '" & NPI & "', '" & Phone & "', '" & Fax & "', '" & Cell & "', '" & Email & "', '" & Contact & "')"
        ExecuteSqlProcedure(sSQL)
    End Sub

    Private Sub btnInvFileLook_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInvFileLook.Click
        Dim DLG As New OpenFileDialog
        DLG.InitialDirectory = My.Application.Info.DirectoryPath & "\Reports\"
        DLG.Filter = "Crystal Report File (*.RPT)|*.rpt"
        If MsgBoxResult.Ok = DLG.ShowDialog Then
            Dim TargetPath As String = My.Application.Info.DirectoryPath & "\Reports\"
            Dim RPTFILE As String = Microsoft.VisualBasic.Mid(DLG.FileName,
            InStrRev(DLG.FileName, "\") + 1)
            If TargetPath <> DLG.InitialDirectory Then _
            IO.File.Copy(DLG.FileName, TargetPath & RPTFILE, True)
            txtInvoiceRPTFile.Text = RPTFILE
        Else
            txtInvoiceRPTFile.Text = ""
        End If
        DLG.Dispose()
        DLG = Nothing
    End Sub

    Private Sub dgvContract_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvContract.CellMouseUp
        If e.ColumnIndex = 1 Then
            If e.Button = MouseButtons.Right Then
                If Clipboard.ContainsText Then
                    dgvContract.Rows.Clear()
                    dgvContract.Rows.Add()
                    Dim Comps() As String = Split(Clipboard.GetText, vbCrLf)
                    For i As Integer = 0 To Comps.Length - 1
                        If Trim(Comps(i)) <> "" AndAlso IsNumeric(Trim(Comps(i))) Then
                            Dim cndc As New SqlConnection(connString)
                            cndc.Open()
                            Dim cmddc As New SqlCommand("Select ID, Name, ComponentType, CPT_Code, " &
                            "ListPrice from Tests where ID = " & Trim(Comps(i)) & " Union Select ID, Name, " &
                            "ComponentType, CPT_Code, ListPrice from Groups where ID = " & Trim(Comps(i)) &
                            " Union Select ID, Name, ComponentType, CPT_Code, ListPrice from Profiles " &
                            "where ID = " & Trim(Comps(i)), cndc)
                            cmddc.CommandType = CommandType.Text
                            Dim drdc As SqlDataReader = cmddc.ExecuteReader
                            If drdc.HasRows Then
                                While drdc.Read
                                    dgvContract.Rows(i).Cells(0).Value =
                                    System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Eraser.ico")
                                    dgvContract.Rows(i).Cells(1).Value = drdc("ID")
                                    dgvContract.Rows(i).Cells(3).Value = drdc("Name")
                                    If drdc("ComponentType") = "T" Then
                                        dgvContract.Rows(i).Cells(4).Value =
                                        System.Drawing.Image.FromFile(Application.StartupPath & "\Images\test.ico")
                                    ElseIf drdc("ComponentType") = "G" Then
                                        dgvContract.Rows(i).Cells(4).Value =
                                        System.Drawing.Image.FromFile(Application.StartupPath & "\Images\group.ico")
                                    Else
                                        dgvContract.Rows(i).Cells(4).Value =
                                        System.Drawing.Image.FromFile(Application.StartupPath & "\Images\profile.ico")
                                    End If
                                    If drdc("CPT_Code") IsNot DBNull.Value Then _
                                    dgvContract.Rows(i).Cells(5).Value = Trim(drdc("CPT_Code"))
                                    dgvContract.Rows(i).Cells(7).Value = Format(drdc("ListPrice"), "##,##0.00")
                                    If i = dgvContract.RowCount - 1 Then dgvContract.Rows.Add()
                                    Exit While
                                End While
                            End If
                            cndc.Close()
                            cndc = Nothing
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Private Sub btnAssLookUp_Click(sender As Object, e As EventArgs) Handles btnAssLookUp.Click
        If txtProviderID.Text <> "" Then
            Dim ProvID As String = frmProviderLookup.ShowDialog
            If ProvID <> "" AndAlso ProvID <> txtProviderID.Text Then
                txtAssID.Text = ProvID
                txtAssName.Text = GetProviderName(Val(ProvID))
            Else
                If ProvID = txtProviderID.Text Then _
                MsgBox(" a provider can't be associated with itself")
                txtAssName.Text = ""
            End If
        End If
        AssProgress()
    End Sub
End Class
