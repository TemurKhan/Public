Imports System.Windows.Forms
Imports System.data

Public Class frmPickUpMgmt

    Private Sub frmPickUpMgmt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblSchDate.Text += " (" & SystemConfig.DateFormat & ")"
        txtPhone.Mask = SystemConfig.PhoneMask
        txtCell.Mask = SystemConfig.PhoneMask
        cmbMode.SelectedIndex = 0
        ConfigureDateTimePicker(dtpDate)
        ConvertNew()
        txtRep.Text = ThisUser.Name
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub ConvertNew()
        txtID.Enabled = False : btnPickUpLook.Enabled = False : btnProviderLook.Enabled = True
        txtID.Text = GetNextPickUpID() : txtProviderID.Text = "" : txtProviderID.Enabled = True
        txtContact.Text = "" : txtClient.Text = "" : txtContact.Enabled = True : txtAddress.Text = ""
        txtPhone.Text = "" : txtPhone.Enabled = True : txtCell.Text = "" : txtCell.Enabled = True
        txtEmail.Text = "" : dtpDate.Enabled = True : txtTime.Text = "" : btnSave.Enabled = True
        txtTime.Enabled = True : txtCourier.Text = "" : txtCourier.Enabled = True : txtNote.Text = ""
        txtNote.Enabled = True : chkVoid.Checked = False : chkVoid.Enabled = False : txtRouteID.Text = ""

        ClearDateTimePicker(dtpDate)
        'txtDate.Text = ""
    End Sub

    Private Function GetNextPickUpID() As Long
        Dim Pid As Long = 1
        Dim cnpu As New SqlClient.SqlConnection(connString)
        cnpu.Open()
        Dim cmdpu As New SqlClient.SqlCommand("Select " &
        "Max(ID) as LastID from PickUps", cnpu)
        cmdpu.CommandType = CommandType.Text
        Dim drpu As SqlClient.SqlDataReader = cmdpu.ExecuteReader
        If drpu.HasRows Then
            While drpu.Read
                If drpu("LastID") IsNot DBNull.Value _
                Then Pid = drpu("LastID") + 1
            End While
        End If
        cnpu.Close()
        cnpu = Nothing
        Return Pid
    End Function

    Private Sub ConvertEdit()
        txtID.Enabled = True : btnPickUpLook.Enabled = True : btnProviderLook.Enabled = False
        txtProviderID.Text = "" : txtProviderID.Enabled = True : txtRouteID.Text = "" : btnSave.Enabled = True
        txtID.Text = "" : txtContact.Text = "" : txtContact.Enabled = True : txtClient.Text = ""
        txtAddress.Text = "" : txtPhone.Text = "" : txtPhone.Enabled = True : txtCell.Text = ""
        txtCell.Enabled = True : txtEmail.Text = "" : dtpDate.Enabled = True
        txtTime.Text = "" : txtTime.Enabled = True : txtCourier.Text = "" : txtCourier.Enabled = True
        txtNote.Text = "" : txtNote.Enabled = True : chkVoid.Checked = False : chkVoid.Enabled = True

        'txtDate.Text = "" :
    End Sub

    Private Sub ConvertView()
        txtID.Enabled = True : btnPickUpLook.Enabled = True : btnProviderLook.Enabled = False
        txtProviderID.Text = "" : txtProviderID.Enabled = False : txtRouteID.Text = "" : btnSave.Enabled = False
        txtID.Text = "" : txtContact.Text = "" : txtContact.Enabled = False : txtClient.Text = ""
        txtAddress.Text = "" : txtPhone.Text = "" : txtPhone.Enabled = False : txtCell.Text = ""
        txtCell.Enabled = False : txtEmail.Text = "" : dtpDate.Enabled = False
        txtTime.Text = "" : txtTime.Enabled = False : txtCourier.Text = "" : txtCourier.Enabled = False
        txtNote.Text = "" : txtNote.Enabled = False : chkVoid.Checked = False : chkVoid.Enabled = False

        'txtDate.Text = "" :
    End Sub

    Private Sub cmbMode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMode.LostFocus
        If cmbMode.SelectedIndex = 0 Then   'New
            ConvertNew()
        ElseIf cmbMode.SelectedIndex = 1 Then   'Edit
            ConvertEdit()
        Else
            ConvertView()
        End If
    End Sub

    Private Sub btnProviderLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProviderLook.Click
        Dim ProviderID As String = frmProviderLookup.ShowDialog
        If ProviderID <> "" Then
            DisplayProvider(Val(ProviderID))
        End If
    End Sub

    Private Sub DisplayProvider(ByVal ProviderID As Long)
        ClearProvider()
        Dim Provider As String = ""
        Dim cnpu As New SqlClient.SqlConnection(connString)
        cnpu.Open()
        Dim cmdpu As New SqlClient.SqlCommand("Select " &
        "* from Providers where ID = " & ProviderID, cnpu)
        cmdpu.CommandType = CommandType.Text
        Dim drpu As SqlClient.SqlDataReader = cmdpu.ExecuteReader
        If drpu.HasRows Then
            While drpu.Read
                If drpu("IsIndividual") IsNot DBNull.Value AndAlso drpu("IsIndividual") = 0 Then     'Entity
                    Provider = drpu("LastName_BSN")
                Else
                    If drpu("Degree") IsNot DBNull.Value _
                    AndAlso drpu("Degree") <> "" Then
                        Provider = drpu("LastName_BSN") & ", " &
                        drpu("FirstName") & " " & drpu("Degree")
                    Else
                        Provider = drpu("LastName_BSN") & ", " &
                        drpu("FirstName")
                    End If
                End If
                txtProviderID.Text = ProviderID.ToString
                txtClient.Text = Provider
                txtAddress.Text = GetAddress(drpu("Address_ID"))
                If drpu("Contact") IsNot DBNull.Value Then txtContact.Text = drpu("Contact")
                If drpu("Phone") IsNot DBNull.Value Then txtPhone.Text = PhoneNeat(drpu("Phone"))
                If drpu("Cell") IsNot DBNull.Value Then txtCell.Text = drpu("Cell")
                If drpu("Email") IsNot DBNull.Value Then txtEmail.Text = drpu("Email")
                'txtDate.Text = Format(Date.Today, SystemConfig.DateFormat)
                'dtpDate.CustomFormat = Format(Date.Today, SystemConfig.DateFormat)
                'txtTime.Text = Format(Date.Now, "HH:mm")
                txtRouteID.Text = drpu("Route_ID").ToString
                txtCourier.Text = GetCourier(drpu("Route_ID"))
            End While
        End If
        cnpu.Close()
        cnpu = Nothing
    End Sub

    Private Function GetCourier(ByVal RouteID As Integer) As String
        Dim Courier As String = ""
        Dim cngc As New SqlClient.SqlConnection(connString)
        cngc.Open()
        Dim cmdgc As New SqlClient.SqlCommand(
        "Select * from Routes where ID = " & RouteID, cngc)
        cmdgc.CommandType = CommandType.Text
        Dim drgc As SqlClient.SqlDataReader = cmdgc.ExecuteReader
        If drgc.HasRows Then
            While drgc.Read
                If drgc("Courier") IsNot DBNull.Value Then _
                 Courier = drgc("Courier")
            End While
        End If
        cngc.Close()
        cngc = Nothing
        Return Courier
    End Function

    Private Sub chkVoid_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkVoid.CheckedChanged
        If chkVoid.Checked = False Then
            chkVoid.Text = "No"
        Else
            chkVoid.Text = "Yes"
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtID.Text <> "" And txtProviderID.Text <> "" And txtRouteID.Text <> "" And
        IsDate(dtpDate.Text & " " & txtTime.Text) & txtCourier.Text <> "" Then
            If cmbMode.SelectedIndex = 0 Then   'New
                Dim ReservID As Long = SavePickUp(Val(txtID.Text))
                MsgBox("Confirmed Reservation ID: " & ReservID.ToString, MsgBoxStyle.Information, "Prolis")
            Else
                SavePickUp(Val(txtID.Text))
            End If
            ClearForm()
            If cmbMode.SelectedIndex = 0 Then   'New
                txtID.Text = GetNextPickUpID()
            Else
                txtID.Text = ""
            End If
        Else
            MsgBox("In order to save a pick up schedule, required fileds (with maroon labels) must be provided with valid information", MsgBoxStyle.Information, "PROLIS")
        End If
    End Sub

    Private Function IsPickUpIdUsed(ByVal PickUpID As Long) As Boolean
        Dim used As Boolean = False
        Dim cniu As New SqlClient.SqlConnection(connString)
        cniu.Open()
        Dim cmdiu As New SqlClient.SqlCommand("Select " &
        "* from PickUps where ID = " & PickUpID, cniu)
        cmdiu.CommandType = CommandType.Text
        Dim driu As SqlClient.SqlDataReader = cmdiu.ExecuteReader
        If driu.HasRows Then used = True
        cniu.Close()
        cniu = Nothing
        Return used
    End Function

    Private Function SavePickUp(ByVal PickUpID As Long)
        Dim cnsp As New SqlClient.SqlConnection(connString)
        cnsp.Open()
        Dim cmdupsert As New SqlClient.SqlCommand("PickUps_SP", cnsp)
        cmdupsert.CommandType = CommandType.StoredProcedure
        cmdupsert.Parameters.AddWithValue("@act", "Upsert")
        cmdupsert.Parameters.AddWithValue("@ID", PickUpID)
        cmdupsert.Parameters.AddWithValue("@PickupDate", CDate(dtpDate.Text & " " & txtTime.Text))
        cmdupsert.Parameters.AddWithValue("@Provider_ID", Val(txtProviderID.Text))
        cmdupsert.Parameters.AddWithValue("@Route_ID", Val(txtRouteID.Text))
        cmdupsert.Parameters.AddWithValue("@RequestedBy", Trim(txtContact.Text))
        cmdupsert.Parameters.AddWithValue("@RequesterPhone", PhoneNeat(txtPhone.Text))
        cmdupsert.Parameters.AddWithValue("@RequesterCell", PhoneNeat(txtCell.Text))
        If cmbMode.SelectedIndex = 0 Then
            cmdupsert.Parameters.AddWithValue("@OriginDate", Date.Now)
        End If
        cmdupsert.Parameters.AddWithValue("@User_ID", ThisUser.ID)
        cmdupsert.Parameters.AddWithValue("@Note", Trim(txtNote.Text))
        cmdupsert.Parameters.AddWithValue("@IsVoid", chkVoid.Checked)
        Try
            cmdupsert.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cnsp.Close()
            cnsp = Nothing
        End Try
        Return PickUpID
    End Function

    Private Sub ClearForm()
        txtID.Text = ""
        ClearProvider() : txtContact.Text = ""
        If IsDate(dtpDate.Text & " " & txtTime.Text) Then
            'dtpDate.Text = Format(DateAdd(DateInterval.Minute, 20, CDate(dtpDate.Text & " " & txtTime.Text)), SystemConfig.DateFormat)
            ClearDateTimePicker(dtpDate)
            txtTime.Text = Format(DateAdd(DateInterval.Minute, 20, CDate(dtpDate.Text & " " & txtTime.Text)), "HH:mm")
        Else
            dtpDate.Text = Format(Date.Today, SystemConfig.DateFormat)
            txtTime.Text = Format(Date.Now, "HH:mm")
        End If
        txtCourier.Text = "" : txtRouteID.Text = "" : txtNote.Text = "" : chkVoid.Checked = False
    End Sub

    Private Sub ClearProvider()
        txtProviderID.Text = "" : txtClient.Text = "" : txtAddress.Text = ""
        txtPhone.Text = "" : txtCell.Text = "" : txtEmail.Text = "" : txtCourier.Text = ""
    End Sub

    Private Sub btnPickUpLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPickUpLook.Click
        Dim PickupID As String = frmPickupLookup.ShowDialog()
        If PickupID <> "" Then DisplayPickup(Val(PickupID))
    End Sub

    Private Sub DisplayPickup(ByVal PickupID As Long)
        ClearForm()
        Dim MaxDate As Date = GetMaxDate()
        Dim cndp As New SqlClient.SqlConnection(connString)
        cndp.Open()
        Dim cmddp As New SqlClient.SqlCommand("Select * from Pickups " &
        "where PickupDate >= '" & DateAdd(DateInterval.Day, -3, MaxDate) _
        & "' and ID = " & PickupID, cndp)
        cmddp.CommandType = CommandType.Text
        Dim drdp As SqlClient.SqlDataReader = cmddp.ExecuteReader
        If drdp.HasRows Then
            While drdp.Read
                txtID.Text = PickupID
                DisplayProvider(drdp("Provider_ID"))
                txtRouteID.Text = drdp("Route_ID")
                txtCourier.Text = GetCourier(drdp("Route_ID"))
                If drdp("RequestedBy") IsNot DBNull.Value Then _
                txtContact.Text = drdp("RequestedBy")
                If drdp("RequesterPhone") IsNot DBNull.Value Then _
                txtPhone.Text = drdp("RequesterPhone")
                If drdp("RequesterCell") IsNot DBNull.Value Then _
                txtCell.Text = drdp("RequesterCell")
                dtpDate.CustomFormat = SystemConfig.DateFormat
                dtpDate.Value = Format(drdp("PickupDate"), SystemConfig.DateFormat)

                txtTime.Text = Format(drdp("PickupDate"), "HH:mm")
                If drdp("Note") IsNot DBNull.Value Then _
                txtNote.Text = drdp("Note")
                chkVoid.Checked = drdp("IsVoid")
            End While
        End If
        cndp.Close()
        cndp = Nothing
    End Sub

    Private Function GetMaxDate() As Date
        Dim MaxDate As Date = Date.Now
        Dim cnmd As New SqlClient.SqlConnection(connString)
        cnmd.Open()
        Dim cmdmd As New SqlClient.SqlCommand("Select " & _
        "max(PickupDate) as LastDate from Pickups", cnmd)
        cmdmd.CommandType = CommandType.Text
        Dim drmd As SqlClient.SqlDataReader = cmdmd.ExecuteReader
        If drmd.HasRows Then
            While drmd.Read
                MaxDate = drmd("LastDate")
            End While
        End If
        cnmd.Close()
        cnmd = Nothing
        Return MaxDate
    End Function

    Private Sub txtID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtID.GotFocus
        txtID.BackColor = FCOLOR
    End Sub

    Private Sub txtID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtID.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtID.LostFocus
        txtID.BackColor = NFCOLOR
    End Sub

    Private Sub txtID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtID.Validated
        If txtID.Text <> "" Then
            DisplayPickup(Val(txtID.Text))
        Else
            ClearForm()
        End If
    End Sub

    Private Sub txtProviderID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProviderID.GotFocus
        txtProviderID.BackColor = FCOLOR
    End Sub

    Private Sub txtProviderID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProviderID.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtProviderID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtProviderID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtProviderID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProviderID.LostFocus
        txtProviderID.BackColor = NFCOLOR
    End Sub

    Private Sub txtProviderID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProviderID.Validated
        If txtProviderID.Text <> "" Then
            DisplayProvider(Val(txtProviderID.Text))
        Else
            ClearProvider()
        End If
    End Sub

    Private Sub txtClient_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtClient.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtContact_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtContact.GotFocus
        txtContact.BackColor = FCOLOR
    End Sub

    Private Sub txtContact_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtContact.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtContact_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtContact.LostFocus
        txtContact.BackColor = NFCOLOR
    End Sub

    Private Sub txtCourier_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCourier.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtDate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtEmail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtEmail.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtPhone_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPhone.GotFocus
        txtPhone.BackColor = FCOLOR
    End Sub

    Private Sub txtPhone_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPhone.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtTime_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTime.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtRouteID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRouteID.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtRep_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRep.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtCell_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCell.GotFocus
        txtCell.BackColor = FCOLOR
    End Sub

    Private Sub txtCell_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCell.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtAddress_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAddress.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtPhone_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPhone.LostFocus
        txtPhone.BackColor = NFCOLOR
    End Sub

    Private Sub txtCell_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCell.LostFocus
        txtCell.BackColor = NFCOLOR
    End Sub

    Private Sub txtDate_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
        'If UserEnteredText(dtpDate) <> "" Then
        '    If Not IsDate(dtpDate.Text) Then
        '        MsgBox("Invalid Date", MsgBoxStyle.Critical, "Prolis")
        '        dtpDate.Text = ""
        '        dtpDate.Focus()
        '    End If
        'End If
    End Sub
    Private Sub dtpDate_CloseUp(sender As Object, e As EventArgs) Handles dtpDate.CloseUp
        ' After selecting a valid date, revert to the standard date format
        CloseUpDateTimePicker(dtpDate)
    End Sub

End Class
