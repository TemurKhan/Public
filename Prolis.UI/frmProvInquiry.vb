Imports System.Windows.Forms
Imports System.data

Public Class frmProvInquiry

    Private Sub txtProviderID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtProviderID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub btnProvLookUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProvLookUp.Click
        Dim ProviderID As String = frmProviderLookup.ShowDialog()
        If ProviderID <> "" Then
            FormClear()
            DisplayProvider(Val(ProviderID))
        End If
    End Sub

    Private Sub FormClear()
        txtProviderID.Text = ""
        txtProviderName.Text = ""
        dgvProviders.Rows.Clear()
        txtAddress.Text = ""
        txtContact.Text = ""
        txtHours.Text = ""
        txtConfiguration.Text = ""
    End Sub

    Private Sub DisplayProvider(ByVal ProviderID As Long)
        'Dim i As Integer
        Dim Contact As String = ""
        Dim Config As String = ""
        Dim Bus As String = ""
        Dim cndp As New SqlClient.SqlConnection(connString)
        cndp.Open()
        Dim cmddp As New SqlClient.SqlCommand("Select * " &
        "from Providers where ID = " & Val(ProviderID), cndp)
        cmddp.CommandType = CommandType.Text
        Dim drdp As SqlClient.SqlDataReader = cmddp.ExecuteReader
        If drdp.HasRows Then
            While drdp.Read
                txtProviderID.Text = drdp("ID")
                If drdp("IsIndividual") IsNot DBNull.Value AndAlso drdp("IsIndividual") = 0 Then 'Entity
                    txtProviderName.Text = drdp("LastName_BSN")
                Else
                    If drdp("Degree") IsNot DBNull.Value _
                    AndAlso drdp("Degree") <> "" Then
                        txtProviderName.Text = drdp("LastName_BSN") & ", " &
                        drdp("FirstName") & " " & drdp("Degree")
                    Else
                        txtProviderName.Text = drdp("LastName_BSN") & ", " &
                        drdp("FirstName")
                    End If
                End If
                If drdp("address_ID") IsNot DBNull.Value Then _
                txtAddress.Text = GetAddress(drdp("address_ID"))
                If drdp("Contact") IsNot DBNull.Value Then Contact = Contact & "Contact: " & drdp("Contact") & vbCrLf
                If drdp("Phone") IsNot DBNull.Value Then Contact = Contact & "Office Phone: " & drdp("Phone") & vbCrLf
                If drdp("Fax") IsNot DBNull.Value Then Contact = Contact & "Fax: " & drdp("Fax") & vbCrLf
                If drdp("Cell") IsNot DBNull.Value Then Contact = Contact & "Cell Phone: " & drdp("Cell") & vbCrLf
                If drdp("Email") IsNot DBNull.Value Then Contact = Contact & "Email: " & drdp("Email") & vbCrLf
                If drdp("Note") IsNot DBNull.Value Then Contact = Contact & "Note: " & drdp("Note")
                txtContact.Text = Contact
                '
                If drdp("Report_ID") = 0 Then    'Acc
                    Config = Config & "Result Report: Accession Report" & vbCrLf
                Else
                    Config = Config & "Result Report: History Report" & vbCrLf
                End If
                Config = Config & "Deliver Report To: " & IIf(drdp("SetExtend") = 0,
                "Ordering Provider only", "Both, Ordering and Attending Providers") & vbCrLf
                If drdp("RegularRDM_ID") = 0 Then    'Email
                    Config = Config & "Non Critical Report delivery default: Email" & vbCrLf
                ElseIf drdp("RegularRDM_ID") = 1 Then    'Fax
                    Config = Config & "Non Critical Report delivery default: Fax" & vbCrLf
                ElseIf drdp("RegularRDM_ID") = 2 Then    'Phone Call
                    Config = Config & "Non Critical Report delivery default: Phone Call" & vbCrLf
                Else
                    Config = Config & "Non Critical Report delivery default: Print" & vbCrLf
                End If
                If drdp("PanicRDM_ID") = 0 Then    'Email
                    Config = Config & "Critical Report delivery default: Email" & vbCrLf
                ElseIf drdp("PanicRDM_ID") = 1 Then    'Fax
                    Config = Config & "Critical Report delivery default: Fax" & vbCrLf
                ElseIf drdp("PanicRDM_ID") = 2 Then    'Phone
                    Config = Config & "Critical Report delivery default: Phone Call" & vbCrLf
                Else
                    Config = Config & "Critical Report delivery default: Print" & vbCrLf
                End If
                If drdp("RptComplete") IsNot DBNull.Value Then Config = Config &
                IIf(drdp("RptComplete") = 0, "Report Status: Partial OK", "Report Status: Complete Only") & vbCrLf
                If drdp("RptCopies") IsNot DBNull.Value Then Config = Config &
                "Copies to print: " & drdp("RptCopies").ToString & vbCrLf
                If drdp("Rpt_Autoprint_Start") IsNot DBNull.Value Then Config = Config &
                "ProlisOn Start Time: " & drdp("Rpt_Autoprint_Start") & vbCrLf
                If drdp("RptComp_Span") IsNot DBNull.Value Then Config = Config &
                "Complete Report Span: " & drdp("RptComp_Span") & " day(s)" & vbCrLf
                If drdp("RptIncomp_Span") IsNot DBNull.Value Then Config = Config &
                "Incomplete Report Span: " & drdp("RptIncomp_Span") & " day(s)" & vbCrLf
                Config = Config & "Configuration" & vbCrLf
                If drdp("PickUp") = 0 Then
                    Config = Config & "Pick Up Arrangement: On Call" & vbCrLf
                Else
                    Config = Config & "Pick Up Arrangement: Regular" & vbCrLf
                End If
                If drdp("Route_ID") IsNot DBNull.Value Then Config = Config & "Courier: " & GetCourier(drdp("Route_ID")) & vbCrLf
                txtConfiguration.Text = Config
                '
                If (drdp("MonStart") IsNot DBNull.Value And
                drdp("MonStop") Is DBNull.Value) Then
                    Bus = Bus & "Monday: " & Trim(drdp("MonStart")) & " To " & Trim(drdp("MonStop")) & vbCrLf
                Else
                    Bus = Bus & "Monday: OFF" & vbCrLf
                End If
                If (drdp("TueStart") IsNot DBNull.Value And
                    drdp("TueStop") Is DBNull.Value) Then
                    Bus = Bus & "Tuesday: " & Trim(drdp("TueStart")) & " To " & Trim(drdp("TueStop")) & vbCrLf
                Else
                    Bus = Bus & "Tuesday: OFF" & vbCrLf
                End If
                If (drdp("WedStart") IsNot DBNull.Value And
                drdp("WedStop") Is DBNull.Value) Then
                    Bus = Bus & "Wednesday: " & Trim(drdp("WedStart")) & " To " & Trim(drdp("WedStop")) & vbCrLf
                Else
                    Bus = Bus & "Wednesday: OFF" & vbCrLf
                End If
                If (drdp("ThuStart") IsNot DBNull.Value And
                drdp("ThuStop") Is DBNull.Value) Then
                    Bus = Bus & "Thursday: " & Trim(drdp("ThuStart")) & " To " & Trim(drdp("ThuStop")) & vbCrLf
                Else
                    Bus = Bus & "Thursday: OFF" & vbCrLf
                End If
                If (drdp("FriStart") IsNot DBNull.Value And
                drdp("FriStop") Is System.DBNull.Value) Then
                    Bus = Bus & "Friday: " & Trim(drdp("FriStart")) & " To " & Trim(drdp("FriStop")) & vbCrLf
                Else
                    Bus = Bus & "Friday: OFF" & vbCrLf
                End If
                If (drdp("SatStart") IsNot DBNull.Value And
                drdp("SatStop") Is DBNull.Value) Then
                    Bus = Bus & "Saturday: " & Trim(drdp("SatStart")) & " To " & Trim(("SatStop")) & vbCrLf
                Else
                    Bus = Bus & "Saturday: OFF" & vbCrLf
                End If
                If (drdp("SunStart") IsNot DBNull.Value And
                drdp("SunStop") Is DBNull.Value) Then
                    Bus = Bus & "Sunday: " & Trim(drdp("SunStart")) & " To " & Trim(drdp("SunStop")) & vbCrLf
                Else
                    Bus = Bus & "Sunday: OFF" & vbCrLf
                End If
                txtHours.Text = Bus
            End While
        End If
        cndp.Close()
        cndp = Nothing
        DisplayAssociation(ProviderID)
    End Sub

    Private Sub DisplayAssociation(ByVal ProviderID As Long)
        dgvProviders.Rows.Clear()
        Dim cnpr As New SqlClient.SqlConnection(connString)
        cnpr.Open()
        Dim cmdpr As New SqlClient.SqlCommand("Select * from " &
        "Clinic_Provider where Clinic_ID = " & ProviderID, cnpr)
        cmdpr.CommandType = CommandType.Text
        Dim drpr As SqlClient.SqlDataReader = cmdpr.ExecuteReader
        If drpr.HasRows Then
            While drpr.Read
                dgvProviders.Rows.Add(drpr("Provider_ID"), GetProviderName(drpr("Provider_ID")))
            End While
        End If
        cnpr.Close()
        cnpr = Nothing
        '
        Dim cnp1 As New SqlClient.SqlConnection(connString)
        cnp1.Open()
        Dim cmdp1 As New SqlClient.SqlCommand("Select * from " &
        "Clinic_Provider where Provider_ID = " & ProviderID, cnp1)
        cmdp1.CommandType = CommandType.Text
        Dim drp1 As SqlClient.SqlDataReader = cmdp1.ExecuteReader
        If drp1.HasRows Then
            While drp1.Read
                dgvProviders.Rows.Add(drp1("Clinic_ID"), GetProviderName(drp1("Clinic_ID")))
            End While
        End If
        cnp1.Close()
        cnp1 = Nothing
    End Sub

    Private Function GetCourier(ByVal RouteID As Integer) As String
        Dim Crr As String = ""
        Dim cnc As New SqlClient.SqlConnection(connString)
        cnc.Open()
        Dim cmdc As New SqlClient.SqlCommand("Select " & _
        "* from Routes where ID = " & RouteID, cnc)
        cmdc.CommandType = CommandType.Text
        Dim drc As SqlClient.SqlDataReader = cmdc.ExecuteReader
        If drc.HasRows Then
            While drc.Read
                Crr = drc("Courier")
            End While
        End If
        cnc.Close()
        cnc = Nothing
        Return Crr
    End Function

    Private Sub txtProviderID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProviderID.Validated
        If txtProviderID.Text <> "" Then
            DisplayProvider(Val(txtProviderID.Text))
        Else
            FormClear()
        End If
    End Sub

    Private Sub frmProvInquiry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub
End Class
