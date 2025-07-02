Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmAddProvider

    Private Sub txtProviderID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtProviderID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtProviderID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProviderID.LostFocus
        Dim RetVal As Integer
        If txtProviderID.Text <> "" Then
            If Not IsProviderIDUnique(Val(txtProviderID.Text)) Then
                RetVal = MsgBox("The Accession ID you typed is not unique. Either type a unique " _
                & "(unused) value or simply accept the system assigned value by clicking 'No' button." _
                & " Do you want to type the unique value yourself?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo)
                If RetVal = vbYes Then
                    txtProviderID.Text = ""
                    txtProviderID.Focus()
                Else
                    txtProviderID.Text = GetNextProviderID()
                End If
            End If
        End If
    End Sub

    Private Function IsProviderIDUnique(ByVal ProviderID As Long) As Boolean
        Dim Isit As Boolean = True
        Dim cniu As New SqlConnection(connString)
        cniu.Open()
        Dim cmdiu As New SqlCommand("Select " &
        "* from Providers where ID = " & ProviderID, cniu)
        cmdiu.CommandType = CommandType.Text
        Dim driu As SqlDataReader = cmdiu.ExecuteReader
        If driu.HasRows Then Isit = False
        cniu.Close()
        cniu = Nothing
        Return Isit
    End Function

    Private Sub chkIndGrp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIndGrp.CheckedChanged
        If chkIndGrp.Checked = True Then
            chkIndGrp.Text = "Individual"
            Label2.Text = "Last Name"
            txtFName.Enabled = True
            Label3.ForeColor = Color.Red
            txtMName.Enabled = True
            txtDegree.Enabled = True
        Else
            chkIndGrp.Text = "Entity"
            Label2.Text = "Entity Name"
            txtFName.Text = ""
            txtMName.Text = ""
            txtDegree.Text = ""
            Label3.ForeColor = Color.DarkBlue
            txtFName.Enabled = False
            txtMName.Enabled = False
            txtDegree.Enabled = False
        End If
    End Sub
    Private Sub ClearForm()
        chkIndGrp.Checked = False
        txtProviderID.Text = ""
        txtLName.Text = ""
        txtAdd1.Text = ""
        txtAdd2.Text = ""
        txtCity.Text = ""
        txtState.Text = ""
        txtZip.Text = ""
        txtCountry.Text = ""
        txtPhone.Text = ""
        txtFax.Text = ""
        txtEmail.Text = ""
    End Sub

    Private Sub btnSS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSS.Click
        If txtProviderID.Text <> "" And
        ((chkIndGrp.Checked = True And txtLName.Text <> "") Or
        (chkIndGrp.Checked = False And txtLName.Text <> "" And txtFName.Text <> "")) _
        And txtAdd1.Text <> "" And txtCity.Text <> "" _
        And txtState.Text <> "" And txtZip.Text <> "" Then
            SaveProvider(Val(txtProviderID.Text))
            ClearForm()
            Me.Close()
        Else
            MsgBox("You have not provided all the required data (labeled red)", MsgBoxStyle.Critical, "Prolis")
        End If
    End Sub

    Private Sub frmAddProvider_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CN.Open("DSN=ProlisQC;UID=sa;PWD=''")
        txtProviderID.Text = GetNextProviderID()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtProviderID.Text <> "" And
        ((chkIndGrp.Checked = True And txtLName.Text <> "") Or
        (chkIndGrp.Checked = False And txtLName.Text <> "" And txtFName.Text <> "")) _
        And txtAdd1.Text <> "" And txtCity.Text <> "" _
        And txtState.Text <> "" And txtZip.Text <> "" Then
            '
            SaveProvider(Val(txtProviderID.Text))
            ClearForm()
            txtProviderID.Text = GetNextProviderID()
            txtLName.Focus()
        Else
            MsgBox("You have not provided all the required data (labeled red)", MsgBoxStyle.Critical, "Prolis")
        End If
    End Sub

    Private Sub SaveProvider(ByVal ProviderID As Long)
        Dim cnsp As New SqlConnection(connString)
        cnsp.Open()
        Dim cmdupsert As New SqlCommand("Providers_SP", cnsp)
        cmdupsert.CommandType = CommandType.StoredProcedure
        cmdupsert.Parameters.AddWithValue("@act", "Upsert")
        cmdupsert.Parameters.AddWithValue("@ID", ProviderID)
        cmdupsert.Parameters.AddWithValue("@FacilityType_ID", 5)
        cmdupsert.Parameters.AddWithValue("@LastName_BSN", Trim(txtLName.Text))
        cmdupsert.Parameters.AddWithValue("@FirstName", Trim(txtFName.Text))
        cmdupsert.Parameters.AddWithValue("@MiddleName", Trim(txtMName.Text))
        cmdupsert.Parameters.AddWithValue("@Degree", Trim(txtDegree.Text))
        cmdupsert.Parameters.AddWithValue("@IsIndividual", chkIndGrp.Checked)
        cmdupsert.Parameters.AddWithValue("@Address_ID", GetAddressID(txtAdd1.Text, _
        txtAdd2.Text, txtCity.Text, txtState.Text, txtZip.Text, txtCountry.Text))
        cmdupsert.Parameters.AddWithValue("@SalesPerson_ID", 0)
        cmdupsert.Parameters.AddWithValue("@Route_ID", 0)
        cmdupsert.Parameters.AddWithValue("@PickUP", "")
        cmdupsert.Parameters.AddWithValue("@Report_ID", "")
        cmdupsert.Parameters.AddWithValue("@License", "")
        cmdupsert.Parameters.AddWithValue("@UPIN", "")
        cmdupsert.Parameters.AddWithValue("@CLIA", "")
        cmdupsert.Parameters.AddWithValue("@NPI", "")
        cmdupsert.Parameters.AddWithValue("@Medicare", "")
        cmdupsert.Parameters.AddWithValue("@Medicaid", "")
        cmdupsert.Parameters.AddWithValue("@BCBS", "")
        cmdupsert.Parameters.AddWithValue("@InterfaceDLL", "")
        cmdupsert.Parameters.AddWithValue("@Active", True)
        cmdupsert.Parameters.AddWithValue("@Phone", PhoneNeat(txtPhone.Text))
        cmdupsert.Parameters.AddWithValue("@Fax", PhoneNeat(txtFax.Text))
        cmdupsert.Parameters.AddWithValue("@Cell", "")
        cmdupsert.Parameters.AddWithValue("@Email", Trim(txtEmail.Text))
        cmdupsert.Parameters.AddWithValue("@Contact", "")
        cmdupsert.Parameters.AddWithValue("@UserName", "")
        cmdupsert.Parameters.AddWithValue("@Password", "")
        cmdupsert.Parameters.AddWithValue("@EMRNoRequired", 0)
        cmdupsert.Parameters.AddWithValue("@PatPhRequired", 0)
        cmdupsert.Parameters.AddWithValue("@CalendarOrdering", 0)
        cmdupsert.Parameters.AddWithValue("@AccConsolidate", 1)
        cmdupsert.Parameters.AddWithValue("@BlockDemograph", 0)
        cmdupsert.Parameters.AddWithValue("@DefaultBilling", "")
        cmdupsert.Parameters.AddWithValue("@DxSearchDefault", "")
        cmdupsert.Parameters.AddWithValue("@DOCRequired", 0)
        cmdupsert.Parameters.AddWithValue("@UseESRD", 0)
        cmdupsert.Parameters.AddWithValue("@UseMyReports", 0)
        cmdupsert.Parameters.AddWithValue("@ClientUserMgmt", 0)
        cmdupsert.Parameters.AddWithValue("@ORNecessity", 0)
        cmdupsert.Parameters.AddWithValue("@Labeler", "")
        cmdupsert.Parameters.AddWithValue("@Note", "")
        cmdupsert.Parameters.AddWithValue("@Panic_Instructions", "")
        cmdupsert.Parameters.AddWithValue("@ExtComment", "")
        cmdupsert.Parameters.AddWithValue("@Alert", "")
        cmdupsert.Parameters.AddWithValue("@Alert_CS", "")
        cmdupsert.Parameters.AddWithValue("@Alert_Acc", "")
        cmdupsert.Parameters.AddWithValue("@ResRPTFile", "")
        cmdupsert.Parameters.AddWithValue("@InvoiceRPTFile", "")
        cmdupsert.Parameters.AddWithValue("@PanicRDM_ID", 1)
        cmdupsert.Parameters.AddWithValue("@RptComplete", 0)
        cmdupsert.Parameters.AddWithValue("@RptComp_Span", 3)
        cmdupsert.Parameters.AddWithValue("@RptIncomp_Span", 7)
        cmdupsert.Parameters.AddWithValue("@RegularRDM_ID", 3)
        cmdupsert.Parameters.AddWithValue("@RptCopies", 1)
        cmdupsert.Parameters.AddWithValue("@SetExtend", "")
        cmdupsert.Parameters.AddWithValue("@ProlisOn", 0)
        cmdupsert.Parameters.AddWithValue("@Rpt_AutoPrint_Start", "")
        cmdupsert.Parameters.AddWithValue("@RDM_Auto", 0)
        cmdupsert.Parameters.AddWithValue("@RDM_Email", 0)
        cmdupsert.Parameters.AddWithValue("@RDM_Fax", 0)
        cmdupsert.Parameters.AddWithValue("@RDM_Print", 0)
        cmdupsert.Parameters.AddWithValue("@RDM_Interface", 0)
        cmdupsert.Parameters.AddWithValue("@RDM_Prolison", 0)
        cmdupsert.Parameters.AddWithValue("@ServerPDF", 0)
        cmdupsert.Parameters.AddWithValue("@PriceLevel", 0)
        cmdupsert.Parameters.AddWithValue("@ContractFrom", Nothing)
        cmdupsert.Parameters.AddWithValue("@ContractTo", Nothing)
        cmdupsert.Parameters.AddWithValue("@MonLunchStart", "OFF")
        cmdupsert.Parameters.AddWithValue("@MonLunchStop", "OFF")
        cmdupsert.Parameters.AddWithValue("@MonStart", "OFF")
        cmdupsert.Parameters.AddWithValue("@MonStop", "OFF")
        cmdupsert.Parameters.AddWithValue("@TueStart", "OFF")
        cmdupsert.Parameters.AddWithValue("@TueLunchStart", "OFF")
        cmdupsert.Parameters.AddWithValue("@TueLunchStop", "OFF")
        cmdupsert.Parameters.AddWithValue("@TueStop", "OFF")
        cmdupsert.Parameters.AddWithValue("@WedStart", "OFF")
        cmdupsert.Parameters.AddWithValue("@WedLunchStart", "OFF")
        cmdupsert.Parameters.AddWithValue("@WedLunchStop", "OFF")
        cmdupsert.Parameters.AddWithValue("@WedStop", "OFF")
        cmdupsert.Parameters.AddWithValue("@ThuStart", "OFF")
        cmdupsert.Parameters.AddWithValue("@ThuLunchStart", "OFF")
        cmdupsert.Parameters.AddWithValue("@ThuLunchStop", "OFF")
        cmdupsert.Parameters.AddWithValue("@ThuStop", "OFF")
        cmdupsert.Parameters.AddWithValue("@FriStart", "OFF")
        cmdupsert.Parameters.AddWithValue("@FriLunchStart", "OFF")
        cmdupsert.Parameters.AddWithValue("@FriLunchStop", "OFF")
        cmdupsert.Parameters.AddWithValue("@FriStop", "OFF")
        cmdupsert.Parameters.AddWithValue("@SatStart", "OFF")
        cmdupsert.Parameters.AddWithValue("@SatLunchStart", "OFF")
        cmdupsert.Parameters.AddWithValue("@SatLunchStop", "OFF")
        cmdupsert.Parameters.AddWithValue("@SatStop", "OFF")
        cmdupsert.Parameters.AddWithValue("@SunStart", "OFF")
        cmdupsert.Parameters.AddWithValue("@SunLunchStart", "OFF")
        cmdupsert.Parameters.AddWithValue("@SunLunchStop", "OFF")
        cmdupsert.Parameters.AddWithValue("@SunStop", "OFF")
        cmdupsert.Parameters.AddWithValue("@PickupNote", "OFF")
        cmdupsert.Parameters.AddWithValue("@LastEditedOn", Date.Now)
        cmdupsert.Parameters.AddWithValue("@EditedBy", ThisUser.ID)
        Try
            cmdupsert.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cnsp.Close()
            cnsp = Nothing
        End Try
        '
        ExecuteSqlProcedure("Delete from Provider_Contract where Provider_ID = " & ProviderID)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

End Class
