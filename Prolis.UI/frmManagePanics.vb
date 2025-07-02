Imports System.Windows.Forms
Imports System.Data

Public Class frmManagePanics
    Private AccessionID As Long = -1
    Private ProviderID As Long = -1
    Private PanicAccIDs As String = ""
    Private Faxed As Boolean = False

    Private Sub frmManagePanics_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbCriteria.SelectedIndex = -1
        lblFrom.Text += " (" & SystemConfig.DateFormat & ")"
        lblTo.Text += " (" & SystemConfig.DateFormat & ")"
        dtpFrom.Format = DateTimePickerFormat.Custom
        dtpFrom.CustomFormat = SystemConfig.DateFormat
        dtpFrom.Value = DateAdd(DateInterval.Day, -SystemConfig.PanicSpan, Date.Today)
        dtpTo.Format = DateTimePickerFormat.Custom
        dtpTo.CustomFormat = SystemConfig.DateFormat
        dtpTo.Value = Date.Today
        'PanicAccIDs = GetPanicAccessions(dtpFrom.Value, dtpTo.Value)
        'If PanicAccIDs <> "" Then DisplayAccessions(PanicAccIDs)
        DisplayAccessions()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub DisplayAccessions()
        Dim Criteria As String = GetCriteria()
        'Dim Accessions As String = GetPanicAccessions(dtpFrom.Value, dtpTo.Value)
        txtProviders.Text = GetProviderCount(Criteria)
        txtPatients.Text = GetPatientCount(Criteria)
        txtPanics.Text = Get3PBilledCount(Criteria)
        txtTotal.Text = GetAccessionCount(Criteria)
        '
        dgvAccessions.Rows.Clear()
        '
        Dim sSQL As String = "Select distinct a.ID as AccID, a.AccessionDate as AccDate, " & _
        "d.IsIndividual, d.LastName_BSN, d.FirstName, d.Degree from Providers d inner join " & _
        "(Patients b inner join (Requisitions a inner join Acc_Results c on c.Accession_ID " & _
        "= a.ID) on a.Patient_ID = b.ID) on d.ID = a.OrderingProvider_ID where c.Released " & _
        "<> 0 and (c.Behavior = 'Panic' or c.Flag like '%Panic%' or c.Flag like '%LP%' or " & _
        "c.Flag like '%HP%' or c.Flag like '%AP%') and Not a.ID in (Select Accession_ID " & _
        "from Panic_Notifications where Test_ID = c.Test_ID) and " & Criteria
        '
        Dim Clinic As String
        Dim cndacc As New SqlClient.SqlConnection(connString)
        cndacc.Open()
        Dim cmddacc As New SqlClient.SqlCommand(sSQL, cndacc)
        cmddacc.CommandType = CommandType.Text
        Dim drdacc As SqlClient.SqlDataReader = cmddacc.ExecuteReader
        If drdacc.HasRows Then
            While drdacc.Read
                If drdacc("IsIndividual") IsNot DBNull.Value AndAlso drdacc("IsIndividual") = 0 Then
                    Clinic = drdacc("LastName_BSN")
                Else
                    Clinic = drdacc("LastName_BSN") & ", " & drdacc("FirstName")
                    If drdacc("Degree") IsNot DBNull.Value AndAlso
                    Trim(drdacc("Degree")) <> "" Then Clinic += " " & Trim(drdacc("Degree"))
                End If
                dgvAccessions.Rows.Add(drdacc("AccID"), Format(drdacc("AccDate"), SystemConfig.DateFormat), Clinic)
            End While
        End If
        cndacc.Close()
        cndacc = Nothing
    End Sub

    Private Function GetProviderName(ByVal ProviderID As Long) As String
        Dim PrName As String = ""
        Dim sSQL As String = "Select * from Providers where ID = " & ProviderID
        '
        Dim cngpr As New SqlClient.SqlConnection(connString)
        cngpr.Open()
        Dim cmdgpr As New SqlClient.SqlCommand(sSQL, cngpr)
        cmdgpr.CommandType = CommandType.Text
        Dim drgpr As SqlClient.SqlDataReader = cmdgpr.ExecuteReader
        If drgpr.HasRows Then
            While drgpr.Read
                If drgpr("IsIndividual") IsNot DBNull.Value AndAlso drgpr("IsIndividual") = 0 Then
                    PrName = drgpr("LastName_BSN")
                Else
                    PrName = drgpr("LastName_BSN") & ", " & drgpr("FirstName") &
                    IIf(drgpr("Degree") IsNot DBNull.Value, " " & drgpr("Degree"), "")
                End If
            End While
        End If
        cngpr.Close()
        cngpr = Nothing
        Return PrName
    End Function

    Private Function GetPatientName(ByVal PatID As Long) As String
        Dim PatName As String = ""
        Dim sSQL As String = "Select * from Patients where ID = " & PatID
        '
        Dim cngpa As New SqlClient.SqlConnection(connString)
        cngpa.Open()
        Dim cmdgpa As New SqlClient.SqlCommand(sSQL, cngpa)
        cmdgpa.CommandType = CommandType.Text
        Dim drgpa As SqlClient.SqlDataReader = cmdgpa.ExecuteReader
        If drgpa.HasRows Then
            While drgpa.Read
                PatName = drgpa("LastName") & ", " & drgpa("FirstName") & " , Gender: " &
                drgpa("Sex") & " , DOB: " & Format(drgpa("DOB"), SystemConfig.DateFormat)
            End While
        End If
        cngpa.Close()
        cngpa = Nothing
        Return PatName
    End Function

    Private Function GetAccessionCount(ByVal Criteria As String) As Integer
        Dim AccCount As Integer = 0
        Dim sSQL As String = "Select Distinct count(a.ID) as AccCount from Requisitions a inner join " &
        "Acc_Results b on a.ID = b.Accession_ID where b.Released <> 0 and (b.Behavior = 'Panic' or " &
        "b.Flag like '%Panic%' or b.Flag like '%LP%' or b.Flag like '%HP%' or b.Flag like '%AP%') and " &
        "Not a.ID in (Select c.Accession_ID from Panic_Notifications c where c.Test_ID = b.Test_ID) " &
        "and " & Criteria
        '
        Dim cnacc As New SqlClient.SqlConnection(connString)
        cnacc.Open()
        Dim cmdacc As New SqlClient.SqlCommand(sSQL, cnacc)
        cmdacc.CommandType = CommandType.Text
        Dim dracc As SqlClient.SqlDataReader = cmdacc.ExecuteReader
        If dracc.HasRows Then
            While dracc.Read
                If dracc("AccCount") IsNot DBNull.Value _
                Then AccCount = dracc("AccCount")
            End While
        End If
        cnacc.Close()
        cnacc = Nothing
        Return AccCount
    End Function

    Private Function Get3PBilledCount(ByVal Criteria As String) As Integer
        Dim IBCount As Integer = 0
        Dim sSQL As String = "Select distinct count(a.ID) as IBCount from Requisitions a inner join " &
        "Acc_Results b on a.ID = b.Accession_ID where a.BillingType_ID = 1 and b.Released <> 0 and " &
        "(b.Behavior = 'Panic' or b.Flag like '%Panic%' or b.Flag like '%LP%' or b.Flag like '%HP%' " &
        "or b.Flag like '%AP%') and Not a.ID in (Select c.Accession_ID from Panic_Notifications c " &
        "where c.Test_ID = b.Test_ID) and " & Criteria
        '
        Dim cnibc As New SqlClient.SqlConnection(connString)
        cnibc.Open()
        Dim cmdibc As New SqlClient.SqlCommand(sSQL, cnibc)
        cmdibc.CommandType = CommandType.Text
        Dim dribc As SqlClient.SqlDataReader = cmdibc.ExecuteReader
        If dribc.HasRows Then
            While dribc.Read
                If dribc("IBCount") IsNot DBNull.Value _
                Then IBCount = dribc("IBCount")
            End While
        End If
        cnibc.Close()
        cnibc = Nothing
        Return IBCount
    End Function

    Private Function GetPatBilledCount(ByVal Criteria As String) As Integer
        Dim PBCount As Integer = 0
        Dim sSQL As String = "Select distinct count(a.ID) as PBCount from Requisitions a inner join Acc_Results " &
        "b on a.ID = b.Accession_ID where a.BillingType_ID = 2 and (b.Behavior = 'Panic' or b.Flag like '%Panic%' " &
        "or b.Flag like '%LP%' or b.Flag like '%HP%' or c.Flag like '%AP%') and Not a.ID in (Select c.Accession_ID " &
        "from Panic_Notifications c where c.Test_ID = b.Test_ID) and " & Criteria
        '
        Dim cnpbc As New SqlClient.SqlConnection(connString)
        cnpbc.Open()
        Dim cmdpbc As New SqlClient.SqlCommand(sSQL, cnpbc)
        cmdpbc.CommandType = CommandType.Text
        Dim drpbc As SqlClient.SqlDataReader = cmdpbc.ExecuteReader
        If drpbc.HasRows Then
            While drpbc.Read
                If drpbc("CBCount") IsNot DBNull.Value _
                Then PBCount = drpbc("CBCount")
            End While
        End If
        cnpbc.Close()
        cnpbc = Nothing
        Return PBCount
    End Function

    Private Function GetClientBilledCount(ByVal Criteria As String) As Integer
        Dim CBCount As Integer = 0
        Dim sSQL As String = "Select distinct count(a.ID) as CBCount from Requisitions a inner join Acc_Results " &
        "b on a.ID = b.Accession_ID where a.BillingType_ID = 0 and (b.Behavior = 'Panic' or b.Flag like '%Panic%' " &
        "or b.Flag like '%LP%' or b.Flag like '%HP%' or c.Flag like '%AP%') and Not a.ID in (Select c.Accession_ID " &
        "from Panic_Notifications c where c.Test_ID = b.Test_ID) and " & Criteria
        '
        Dim cncbc As New SqlClient.SqlConnection(connString)
        cncbc.Open()
        Dim cmdcbc As New SqlClient.SqlCommand(sSQL, cncbc)
        cmdcbc.CommandType = CommandType.Text
        Dim drcbc As SqlClient.SqlDataReader = cmdcbc.ExecuteReader
        If drcbc.HasRows Then
            While drcbc.Read
                If drcbc("CBCount") IsNot DBNull.Value _
                Then CBCount = drcbc("CBCount")
            End While
        End If
        cncbc.Close()
        cncbc = Nothing
        Return CBCount
    End Function

    Private Function GetGratisCount(ByVal Criteria As String) As Integer
        Dim GCount As Integer = 0
        Dim sSQL As String = "Select distinct count(a.ID) as GCount from Requisitions a inner join Acc_Results " &
        "b on a.ID = b.Accession_ID where a.IsGratis <> 0 and (b.Behavior = 'Panic' or b.Flag like '%Panic%' " &
        "or b.Flag like '%LP%' or b.Flag like '%HP%' or c.Flag like '%AP%') and Not a.ID in (Select c.Accession_ID " &
        "from Panic_Notifications c where c.Test_ID = b.Test_ID) and " & Criteria
        '
        Dim cnggc As New SqlClient.SqlConnection(connString)
        cnggc.Open()
        Dim cmdggc As New SqlClient.SqlCommand(sSQL, cnggc)
        cmdggc.CommandType = CommandType.Text
        Dim drggc As SqlClient.SqlDataReader = cmdggc.ExecuteReader
        If drggc.HasRows Then
            While drggc.Read
                If drggc("GCount") IsNot DBNull.Value _
                Then GCount = drggc("GCount")
            End While
        End If
        cnggc.Close()
        cnggc = Nothing
        Return GCount
    End Function

    Private Function GetPatientCount(ByVal Criteria As String) As Integer
        Dim PaCount As Integer = 0
        Dim sSQL As String = "Select count(ID) as PaCount from Patients where ID in (Select " &
        "distinct a.Patient_ID from Requisitions a, Acc_Results b where a.ID = b.Accession_ID " &
        "and b.Released <> 0 and (b.Behavior = 'Panic' or b.Flag like '%Panic%' or b.Flag like " &
        "'%LP%' or b.Flag like '%HP%' or b.Flag like '%AP%') and Not a.ID in (Select Accession_ID " &
        "from Panic_Notifications where Test_ID = b.Test_ID) and " & Criteria & ")"
        '
        Dim cngpa As New SqlClient.SqlConnection(connString)
        cngpa.Open()
        Dim cmdgpa As New SqlClient.SqlCommand(sSQL, cngpa)
        cmdgpa.CommandType = CommandType.Text
        Dim drgpa As SqlClient.SqlDataReader = cmdgpa.ExecuteReader
        If drgpa.HasRows Then
            While drgpa.Read
                If drgpa("PaCount") IsNot DBNull.Value _
                Then PaCount = drgpa("PaCount")
            End While
        End If
        cngpa.Close()
        cngpa = Nothing
        Return PaCount
    End Function

    Private Function GetProviderCount(ByVal Criteria As String) As Integer
        Dim PrCount As Integer = 0
        Dim sSQL As String = "Select Count(ID) as PrCount from Providers where ID in (Select distinct " &
        "a.OrderingProvider_ID from Requisitions a, Acc_Results b where a.ID = b.Accession_ID and " &
        "b.Released <> 0 and (b.Behavior = 'Panic' or b.Flag like '%Panic%' or b.Flag like '%LP%' or " &
        "b.Flag like '%HP%' or b.Flag like '%AP%') and Not a.ID in (Select Accession_ID from " &
        "Panic_Notifications where Test_ID = b.Test_ID) and " & Criteria & ")"
        '
        Dim cngpc As New SqlClient.SqlConnection(connString)
        cngpc.Open()
        Dim cmdgpc As New SqlClient.SqlCommand(sSQL, cngpc)
        cmdgpc.CommandType = CommandType.Text
        Dim drgpc As SqlClient.SqlDataReader = cmdgpc.ExecuteReader
        If drgpc.HasRows Then
            While drgpc.Read
                If drgpc("PrCount") IsNot DBNull.Value _
                Then PrCount = drgpc("PrCount")
            End While
        End If
        cngpc.Close()
        cngpc = Nothing
        Return PrCount
    End Function

    Private Function GetCriteria() As String
        Dim Criteria As String = ""
        Criteria = " a.AccessionDate between '" & Format(dtpFrom.Value, SystemConfig.DateFormat) &
        "' and '" & Format(dtpTo.Value, SystemConfig.DateFormat) & " 23:59:00'"
        If cmbCriteria.SelectedIndex = 0 Then   'Accession ID
            If txtTerm.Text <> "" Then Criteria += " and a.ID = " & Val(txtTerm.Text)
        ElseIf cmbCriteria.SelectedIndex = 1 Then   'Provider ID
            If txtTerm.Text <> "" Then Criteria += " and a.OrderingProvider_ID = " & Val(txtTerm.Text)
        ElseIf cmbCriteria.SelectedIndex = 2 Then   'Provider Name
            If Trim(txtTerm.Text) <> "" Then
                Dim Data() As String
                If InStr(txtTerm.Text, ",") > 0 Then
                    Data = Split(txtTerm.Text, ",")
                    If Trim(Data(0)) <> "" And Trim(Data(1)) <> "" Then 'Last and First Names
                        Criteria += " and a.OrderingProvider_ID in (Select ID from Providers where " &
                        "LastName_BSN like '" & Trim(Data(0)) & "%' and FirstName like '" &
                        Trim(Data(1)) & "%')"
                    ElseIf Trim(Data(0)) <> "" And Trim(Data(1)) = "" Then 'Last Name
                        Criteria += " and a.OrderingProvider_ID in (Select ID from Providers where " &
                        "LastName_BSN like '" & Trim(Data(0)) & "%')"
                    End If
                Else
                    Criteria += " and a.OrderingProvider_ID in (Select ID from Providers where " &
                    "LastName_BSN like '" & Trim(txtTerm.Text) & "%')"
                End If
            End If
        ElseIf cmbCriteria.SelectedIndex = 3 Then   'Patient Name
            If Trim(txtTerm.Text) <> "" Then
                Dim Data() As String
                If InStr(txtTerm.Text, ",") > 0 Then
                    Data = Split(txtTerm.Text, ",")
                    If Trim(Data(0)) <> "" And Trim(Data(1)) <> "" Then 'Last and First Names
                        Criteria += " and a.Patient_ID in (Select ID from Patients where " &
                        "LastName like '" & Trim(Data(0)) & "%' and FirstName like '" &
                        Trim(Data(1)) & "%')"
                    ElseIf Trim(Data(0)) <> "" And Trim(Data(1)) = "" Then 'Last Name
                        Criteria += " and a.Patient_ID in (Select ID from Patients where " &
                        "LastName like '" & Trim(Data(0)) & "%')"
                    End If
                Else
                    Criteria += " and a.Patient_ID in (Select ID from Patients where " &
                    "LastName like '" & Trim(txtTerm.Text) & "%')"
                End If
            End If
        End If
        Return Criteria
    End Function

    Private Sub txtTerm_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTerm.KeyPress
        Select Case cmbCriteria.SelectedIndex
            Case 0, 1, 3
                Numerals(sender, e)
        End Select
    End Sub

    Private Sub cmbCriteria_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCriteria.SelectedIndexChanged
        If cmbCriteria.SelectedIndex > 4 Then
            txtTerm.ReadOnly = True
        Else
            txtTerm.ReadOnly = False
        End If
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        DisplayAccessions()
    End Sub

    Private Sub dgvAccessions_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAccessions.CellClick
        If e.RowIndex <> -1 Then
            AccessionID = dgvAccessions.Rows(e.RowIndex).Cells(0).Value
            txtAccID.Text = AccessionID.ToString
            DisplayPatient(AccessionID)
            DisplayResults(AccessionID)
            DisplayAccProvider(AccessionID)
            btnReport.Enabled = True
            'btnDismis.Enabled = True
        End If
    End Sub

    Private Sub DisplayPatient(ByVal AccID As Long)
        txtPatient.Text = ""
        Dim Pat As String = ""
        Dim sSQL As String = "Select * from Patients where ID in (Select Patient_ID from Requisitions where ID = " & AccID & ")"
        If connString <> "" Then
            Dim cndpa As New SqlClient.SqlConnection(connString)
            cndpa.Open()
            Dim cmddpa As New SqlClient.SqlCommand(sSQL, cndpa)
            cmddpa.CommandType = CommandType.Text
            Dim drdpa As SqlClient.SqlDataReader = cmddpa.ExecuteReader
            If drdpa.HasRows Then
                While drdpa.Read
                    Pat = "Name: " & drdpa("LastName") & ", " & drdpa("FirstName") & vbCrLf
                    Pat += "DOB: " & Format(drdpa("DOB"), SystemConfig.DateFormat) & "   Sex: " & drdpa("Sex") & vbCrLf
                    If drdpa("Address_ID") IsNot DBNull.Value Then
                        Pat += "Address: " & GetAddress1(drdpa("Address_ID")) & " " & GetAddress2(drdpa("Address_ID")) _
                        & ", " & GetAddressCity(drdpa("Address_ID")) & ", " &
                        GetAddressState(drdpa("Address_ID")) & " " & GetAddressZip(drdpa("Address_ID"))
                    Else
                        Pat += "Address: None"
                    End If
                    Pat += IIf(drdpa("HomePhone") Is DBNull.Value, "", vbCrLf & "Phone: " & drdpa("HomePhone"))
                End While
            End If
            cndpa.Close()
            cndpa = Nothing
            'Else
            '    Dim cndpa As New Odbc.OdbcConnection(connstring)
            '    cndpa.Open()
            '    Dim cmddpa As New Odbc.OdbcCommand(sSQL, cndpa)
            '    cmddpa.CommandType = CommandType.Text
            '    Dim drdpa As Odbc.OdbcDataReader = cmddpa.ExecuteReader
            '    If drdpa.HasRows Then
            '        While drdpa.Read
            '            Pat = "Name: " & drdpa("LastName") & ", " & drdpa("FirstName") & vbCrLf
            '            Pat += "DOB: " & Format(drdpa("DOB"), SystemConfig.DateFormat) & "   Sex: " & drdpa("Sex") & vbCrLf
            '            If drdpa("Address_ID") IsNot DBNull.Value Then
            '                Pat += "Address: " & GetAddress1(drdpa("Address_ID")) & " " & GetAddress2(drdpa("Address_ID")) _
            '                & ", " & GetAddressCity(drdpa("Address_ID")) & ", " &
            '                GetAddressState(drdpa("Address_ID")) & " " & GetAddressZip(drdpa("Address_ID"))
            '            Else
            '                Pat += "Address: None"
            '            End If
            '            Pat += IIf(drdpa("HomePhone") Is DBNull.Value, "", vbCrLf & "Phone: " & drdpa("HomePhone"))
            '        End While
            '    End If
            '    cndpa.Close()
            '    cndpa = Nothing
        End If
        txtPatient.Text = Pat
    End Sub

    Private Sub DisplayAccProvider(ByVal AccID As Long)
        Dim PrName As String = ""
        Dim Phone As String = ""
        Dim Fax As String = ""
        dgvFaxNos.Rows.Clear()
        Dim sSQL As String = "Select * from Providers where ID in (Select OrderingProvider_ID from Requisitions where ID = " & AccID & ")"
        '
        Dim cndpr As New SqlClient.SqlConnection(connString)
        cndpr.Open()
        Dim cmddpr As New SqlClient.SqlCommand(sSQL, cndpr)
        cmddpr.CommandType = CommandType.Text
        Dim drdpr As SqlClient.SqlDataReader = cmddpr.ExecuteReader
        If drdpr.HasRows Then
            While drdpr.Read
                ProviderID = drdpr("ID")
                If drdpr("IsIndividual") = False Then
                    PrName = drdpr("LastName_BSN") & " (" & drdpr("ID") & ")"
                Else
                    PrName = drdpr("LastName_BSN") & ", " & drdpr("FirstName") & IIf(drdpr("Degree") _
                    IsNot DBNull.Value, " " & drdpr("Degree"), "") & " (" & drdpr("ID") & ")"
                End If
                If drdpr("Address_ID") IsNot DBNull.Value Then
                    PrName += ", " & GetAddress1(drdpr("Address_ID")) & GetAddressCity(drdpr("Address_ID")) _
                    & ", " & GetAddressState(drdpr("Address_ID")) & " " & GetAddressZip(drdpr("Address_ID"))
                End If
                If drdpr("Phone") IsNot DBNull.Value AndAlso Trim(drdpr("Phone")) <> "" Then
                    Phone = PhoneNeat(drdpr("Phone"))
                    txtOrdPhone.Text = "(" & Microsoft.VisualBasic.Mid(
                    Phone, 1, 3) & ") " & Microsoft.VisualBasic.Mid(
                    Phone, 4, 3) & "-" & Microsoft.VisualBasic.Mid(Phone, 7)
                End If
                '
                If drdpr("Fax") IsNot DBNull.Value AndAlso Trim(drdpr("Fax")) <> "" Then
                    Fax = PhoneNeat(drdpr("Fax"))
                    dgvFaxNos.Rows.Add(False, "(" & Microsoft.VisualBasic.Mid(
                    Fax, 1, 3) & ") " & Microsoft.VisualBasic.Mid(Fax, 4, 3) &
                    "-" & Microsoft.VisualBasic.Mid(Fax, 7))
                End If
                '
                If drdpr("Panic_Instructions") Is DBNull.Value Then
                    txtInstructions.Text = ""
                Else
                    txtInstructions.Rtf = drdpr("Panic_Instructions")
                End If
            End While
        End If
        cndpr.Close()
        cndpr = Nothing
        Dim AttPhone As String = ""
        Dim AttFax As String = ""
        sSQL = "Select * from Providers where ID in (Select AttendingProvider_ID from Requisitions where ID = " & AccID & ")"
        Dim cnatt As New SqlClient.SqlConnection(connString)
        cnatt.Open()
        Dim cmdatt As New SqlClient.SqlCommand(sSQL, cnatt)
        cmdatt.CommandType = CommandType.Text
        Dim dratt As SqlClient.SqlDataReader = cmdatt.ExecuteReader
        If dratt.HasRows Then
            While dratt.Read
                If ProviderID <> dratt("ID") Then
                    If dratt("Phone") IsNot DBNull.Value AndAlso Trim(dratt("Phone")) <> "" Then
                        AttPhone = PhoneNeat(dratt("Phone"))
                        If AttPhone <> Phone Then
                            txtAttPhone.Text = "(" & Microsoft.VisualBasic.Mid(
                            AttPhone, 1, 3) & ") " & Microsoft.VisualBasic.Mid(
                            AttPhone, 4, 3) & "-" & Microsoft.VisualBasic.Mid(AttPhone, 7)
                        End If
                    End If
                    If dratt("Fax") IsNot DBNull.Value AndAlso Trim(dratt("Fax")) <> "" Then
                        AttFax = PhoneNeat(dratt("Fax"))
                        If AttFax <> Fax Then
                            dgvFaxNos.Rows.Add(False, "(" & Microsoft.VisualBasic.Mid(
                            AttFax, 1, 3) & ") " & Microsoft.VisualBasic.Mid(AttFax, 4, 3) &
                            "-" & Microsoft.VisualBasic.Mid(AttFax, 7))
                        End If
                    End If
                End If
            End While
        End If
        cnatt.Close()
        cnatt = Nothing
        txtProvider.Text = PrName
    End Sub

    Private Function GetPatient(ByVal PatID As Long) As String
        Dim Pat As String = ""
        Dim cngp As New SqlClient.SqlConnection(connString)
        cngp.Open()
        Dim cmdgp As New SqlClient.SqlCommand("Select * from Patients where ID = " & PatID, cngp)
        cmdgp.CommandType = CommandType.Text
        Dim drgp As SqlClient.SqlDataReader = cmdgp.ExecuteReader
        If drgp.HasRows Then
            While drgp.Read
                Pat = drgp("LastName") & ", " & drgp("FirstName") & vbCrLf
                If drgp("Address_ID") IsNot DBNull.Value Then
                    Pat += GetAddress1(drgp("Address_ID")) & " " & GetAddress2(drgp("Address_ID")) _
                    & vbCrLf & GetAddressCity(drgp("Address_ID")) & ", " &
                    GetAddressState(drgp("Address_ID")) & " " & GetAddressZip(drgp("Address_ID")) &
                    vbCrLf & "Phone: " &
                    IIf(drgp("HomePhone") Is DBNull.Value, "", drgp("HomePhone"))
                End If
            End While
        End If
        cngp.Close()
        cngp = Nothing
        Return Pat
    End Function

    Private Sub DisplayResults(ByVal AccID As Long)
        dgvResults.Rows.Clear()
        Dim sSQL As String = "Select * from Acc_Results where Accession_ID = " & AccID & " and " &
        "(Behavior = 'Panic' or Flag like '%HP%' or Flag like '%LP%' or Flag like '%Panic%') and " &
        "Not Test_ID in (Select Test_ID from Panic_Notifications where Accession_ID = " & AccID & ")"
        '
        Dim cndrs As New SqlClient.SqlConnection(connString)
        cndrs.Open()
        Dim cmddrs As New SqlClient.SqlCommand(sSQL, cndrs)
        cmddrs.CommandType = CommandType.Text
        Dim drdrs As SqlClient.SqlDataReader = cmddrs.ExecuteReader
        If drdrs.HasRows Then
            While drdrs.Read
                dgvResults.Rows.Add(drdrs("Test_ID"), GetTGPName(drdrs("Test_ID")), drdrs("Result"),
                Trim(drdrs("Flag")), drdrs("NormalRange"), GetUnit(drdrs("Test_ID")))
            End While
        End If
        cndrs.Close()
        cndrs = Nothing
    End Sub

    Private Function GetUnit(ByVal TestID As Integer) As String
        Dim UOM As String = ""
        Dim sSQL As String = "Select UOM from Tests where ID = " & TestID
        '
        Dim cnuom As New SqlClient.SqlConnection(connString)
        cnuom.Open()
        Dim cmduom As New SqlClient.SqlCommand(sSQL, cnuom)
        cmduom.CommandType = CommandType.Text
        Dim druom As SqlClient.SqlDataReader = cmduom.ExecuteReader
        If druom.HasRows Then
            While druom.Read
                If druom("UOM") IsNot DBNull.Value _
                Then UOM = Trim(druom("UOM"))
            End While
        End If
        cnuom.Close()
        cnuom = Nothing
        Return UOM
    End Function

    Private Sub txtComment_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtComment.TextChanged
        If txtComment.Text = "" Then
            btnLog.Enabled = False
        Else
            btnLog.Enabled = True
        End If
    End Sub

    Private Sub btnLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLog.Click
        If AccessionID <> -1 And txtComment.Text <> "" Then
            Dim TestResults As String = ""
            Dim i As Integer
            For i = 0 To dgvResults.RowCount - 1
                If Not dgvResults.Rows(i).Cells(1).Value Is Nothing AndAlso
                dgvResults.Rows(i).Cells(1).Value.ToString <> "" Then
                    Dim cnpn As New SqlClient.SqlConnection(connString)
                    cnpn.Open()
                    Dim cmdpn As New SqlClient.SqlCommand("Panic_Notifications_SP", cnpn)
                    cmdpn.CommandType = CommandType.StoredProcedure
                    cmdpn.Parameters.AddWithValue("@act", "Upsert")
                    cmdpn.Parameters.AddWithValue("@Accession_ID", AccessionID)
                    cmdpn.Parameters.AddWithValue("@Test_ID", dgvResults.Rows(i).Cells(0).Value)
                    cmdpn.Parameters.AddWithValue("@NotifyDate", Date.Now)
                    cmdpn.Parameters.AddWithValue("@RecipientName", Trim(txtComment.Text))
                    cmdpn.Parameters.AddWithValue("@TestResults", dgvResults.Rows(i).Cells(2).Value)
                    cmdpn.Parameters.AddWithValue("@Faxed", Faxed)
                    cmdpn.Parameters.AddWithValue("@User_ID", ThisUser.ID)
                    cmdpn.Parameters.AddWithValue("@UserName", ThisUser.Name)
                    Try
                        cmdpn.ExecuteNonQuery()
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    Finally
                        cnpn.Close()
                        cnpn = Nothing
                    End Try
                End If
            Next
            txtComment.Text = ""
            dgvResults.Rows.Clear()
            txtProvider.Text = ""
            txtInstructions.Text = ""
            txtAccID.Text = ""
            txtPatient.Text = ""
            txtOrdPhone.Text = ""
            txtAttPhone.Text = ""
            dgvFaxNos.Rows.Clear()
            btnFax.Enabled = False
            frmDashboard.lblAlert_Click(Nothing, Nothing)
            DisplayAccessions()
        End If
    End Sub

    Private Sub btnFax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFax.Click
        Faxed = True
        UpdateFaxScheduler()
    End Sub

    Private Sub UpdateFaxScheduler()
        Dim i As Integer
        Dim FaxNos As String = ""
        For i = 0 To dgvFaxNos.RowCount - 1
            If dgvFaxNos.Rows(i).Cells(0).Value = True And
            ProviderID <> -1 And Trim(txtAccID.Text) <> "" Then 'Fax
                If dgvFaxNos.Rows(i).Cells(1).Value IsNot Nothing AndAlso
                PhoneNeat(dgvFaxNos.Rows(i).Cells(1).Value) <> "" Then
                    Dim conn As New SqlClient.SqlConnection(connString)
                    conn.Open()
                    Dim cmdupsert As New SqlClient.SqlCommand("Req_RPT_SP", conn)
                    cmdupsert.CommandType = CommandType.StoredProcedure
                    cmdupsert.Parameters.AddWithValue("@act", "Upsert")
                    cmdupsert.Parameters.AddWithValue("@Provider_ID", ProviderID)
                    cmdupsert.Parameters.AddWithValue("@Base_ID", Val(txtAccID.Text))
                    cmdupsert.Parameters.AddWithValue("@EntrySource", "Panic Mgmt")
                    cmdupsert.Parameters.AddWithValue("@RPT_Type", "ACC")
                    cmdupsert.Parameters.AddWithValue("@EntryDate", Date.Now)
                    cmdupsert.Parameters.AddWithValue("@Ordinal", 1)
                    cmdupsert.Parameters.AddWithValue("@RDM_Auto", 0)
                    cmdupsert.Parameters.AddWithValue("@RPT_Complete", 0)
                    cmdupsert.Parameters.AddWithValue("@RPT_Print", 0)
                    cmdupsert.Parameters.AddWithValue("@RPT_Prolison", 0)
                    cmdupsert.Parameters.AddWithValue("@RPT_Interface", 0)
                    cmdupsert.Parameters.AddWithValue("@RPT_Fax", 1)
                    cmdupsert.Parameters.AddWithValue("@Fax", PhoneNeat(dgvFaxNos.Rows(i).Cells(1).Value))
                    cmdupsert.Parameters.AddWithValue("@Priority", 1)
                    cmdupsert.Parameters.AddWithValue("@RPT_Email", 0)
                    cmdupsert.Parameters.AddWithValue("@Email", "")
                    cmdupsert.Parameters.AddWithValue("@Executed", 0)

                    cmdupsert.Parameters.AddWithValue("@Comment", "Panic Results")
                    Try
                        cmdupsert.ExecuteNonQuery()
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    Finally
                        conn.Close()
                        conn = Nothing
                    End Try
                    ExecuteSqlProcedure("Delete from Event_Capture where Accession_ID = " &
                    Val(txtAccID.Text) & " and Event_ID = 12 and Provider_ID = " & ProviderID)
                    FaxNos += dgvFaxNos.Rows(i).Cells(1).Value & ", "
                End If
            End If
        Next
        If FaxNos.EndsWith(", ") Then FaxNos = FaxNos.Substring(0, Len(FaxNos) - 2)
        If FaxNos <> "" Then UpdatePanicLog(FaxNos)
    End Sub

    Private Sub UpdatePanicLog(ByVal FaxNos As String)
        If txtAccID.Text <> "" And FaxNos <> "" Then
            Dim TestResults As String = ""
            Dim i As Integer
            For i = 0 To dgvResults.RowCount - 1
                If Not dgvResults.Rows(i).Cells(1).Value Is Nothing AndAlso
                dgvResults.Rows(i).Cells(1).Value.ToString <> "" Then
                    TestResults += dgvResults.Rows(i).Cells(0).Value.ToString & "-" &
                    dgvResults.Rows(i).Cells(1).Value.ToString & " = " &
                    dgvResults.Rows(i).Cells(2).Value.ToString & ", "
                End If
            Next
            If TestResults.EndsWith(", ") Then TestResults = TestResults.Substring(0, Len(TestResults) - 2)
            Dim cnpn As New SqlClient.SqlConnection(connString)
            cnpn.Open()
            Dim cmdpn As New SqlClient.SqlCommand("Panic_Notifications_SP", cnpn)
            cmdpn.CommandType = CommandType.StoredProcedure
            cmdpn.Parameters.AddWithValue("@act", "Upsert")
            cmdpn.Parameters.AddWithValue("@Accession_ID", AccessionID)
            cmdpn.Parameters.AddWithValue("@Test_ID", dgvResults.Rows(0).Cells(0).Value)
            cmdpn.Parameters.AddWithValue("@NotifyDate", Date.Now)
            cmdpn.Parameters.AddWithValue("@RecipientName", GetProviderName(ProviderID))
            cmdpn.Parameters.AddWithValue("@TestResults", TestResults)
            cmdpn.Parameters.AddWithValue("@Faxed", 1)
            cmdpn.Parameters.AddWithValue("@Fax", FaxNos)
            cmdpn.Parameters.AddWithValue("@User_ID", ThisUser.ID)
            cmdpn.Parameters.AddWithValue("@UserName", ThisUser.Name)
            Try
                cmdpn.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                cnpn.Close()
                cnpn = Nothing
            End Try
            txtComment.Text = ""
            dgvResults.Rows.Clear()
            txtProvider.Text = ""
            txtAccID.Text = ""
            txtPatient.Text = ""
            txtOrdPhone.Text = ""
            txtAttPhone.Text = ""
            dgvFaxNos.Rows.Clear()
            btnFax.Enabled = False
            frmDashboard.lblAlert_Click(Nothing, Nothing)
            DisplayAccessions()
        End If
    End Sub

    Private Sub btnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click
        If AccessionID <> -1 Then
            '=========================================================
            'TODO: Crystal Reports Code
            'Dim ProvChoice() As String
            'ProvChoice = GetProviderChoice(AccessionID)
            ''0=ProviderID, 1=BaseID, 2=RPT, 3=Complete, 4=Delivery, 5=DeliveryParam
            'Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            'If SystemConfig.CustomRPT = False Then
            '    If ProvChoice(2) <> "Hist" Then
            '        oRpt.Load(GetReportPath(SystemConfig.GenericResults))
            '        ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database, My.Settings.UID,
            '        My.Settings.PWD)
            '        oRpt.RecordSelectionFormula = "{Requisitions.ID} = " & AccessionID.ToString
            '    Else
            '        oRpt.Load(GetReportPath("History_Results.rpt"))
            '        ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database, My.Settings.UID,
            '        My.Settings.PWD)
            '        oRpt.RecordSelectionFormula = "{Requisitions.Patient_ID} = " &
            '        GetPatientID(AccessionID)
            '    End If
            'Else
            '    oRpt.Load(GetReportPath(SystemConfig.CustomResults))
            '    ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database, My.Settings.UID,
            '        My.Settings.PWD)
            '    oRpt.RecordSelectionFormula = "{Requisitions.ID} = " & AccessionID.ToString
            'End If
            'frmRV.CRRV.ReportSource = oRpt
            'frmRV.Show()
            'frmRV.MdiParent = ProlisQC
            ''UpdateReportTime(AccessionID, False)
            'LogEvent(AccessionID, 10, GetOrdProvIDFromAccID(AccessionID),
            'IIf(ReportFullResulted(AccessionID) = True, "FINAL", "PARTIAL"),
            'False, ThisUser.UserName, "Panic Mgmt")
            '=========================================================
        End If
    End Sub

    Private Function GetPatientID(ByVal AccID As Long) As Long
        Dim PatientID As Long = -1
        Dim cngp As New SqlClient.SqlConnection(connString)
        cngp.Open()
        Dim cmdgp As New SqlClient.SqlCommand("Select " &
        "Patient_ID from Requisitions where ID = " & AccID, cngp)
        cmdgp.CommandType = CommandType.Text
        Dim drgp As SqlClient.SqlDataReader = cmdgp.ExecuteReader
        If drgp.HasRows Then
            While drgp.Read
                PatientID = drgp("Patient_ID")
            End While
        End If
        cngp.Close()
        cngp = Nothing
        Return PatientID
    End Function

    Private Function GetProviderChoice(ByVal AccID As Long) As String()
        Dim Choice() As String = {"", "", "", "", "", ""}
        '0=ProviderID, 1=BaseID, 2=RPT, 3=Complete, 4=Delivery, 5=DeliveryParam
        Dim cngp As New SqlClient.SqlConnection(connString)
        cngp.Open()
        Dim cmdgp As New SqlClient.SqlCommand("Select * from " &
        "Providers where ID in (Select OrderingProvider_ID " &
        "from Requisitions where ID = " & AccID & ")", cngp)
        cmdgp.CommandType = CommandType.Text
        Dim drgp As SqlClient.SqlDataReader = cmdgp.ExecuteReader
        If drgp.HasRows Then
            While drgp.Read
                Choice(0) = drgp("ID").ToString
                If drgp("Report_ID") IsNot DBNull.Value Then
                    If drgp("Report_ID") = 1 Then
                        Choice(1) = AccID.ToString
                        Choice(2) = "Acc"
                    Else
                        Choice(1) = GetPatientID(AccID)
                        Choice(2) = "Hist"
                    End If
                Else
                    Choice(1) = AccID.ToString
                    Choice(2) = "Acc"
                End If
                If drgp("RptComplete") IsNot DBNull.Value Then
                    If drgp("RptComplete") = 0 Then
                        Choice(3) = "PARTIAL"
                    Else
                        Choice(3) = "FINAL"
                    End If
                Else
                    Choice(3) = "PARTIAL"
                End If
                If drgp("RegularRDM_ID") IsNot DBNull.Value Then
                    'Email, Fax, Interface, Print, ProlisOn, Phone
                    If drgp("RegularRDM_ID") = 0 Then
                        Choice(4) = "Email"
                        If drgp("Email") Is DBNull.Value Then
                            Choice(5) = ""
                        Else
                            Choice(5) = drgp("Email")
                        End If
                    ElseIf drgp("RegularRDM_ID") = 1 Then
                        Choice(4) = "Fax"
                        If drgp("Fax") Is DBNull.Value Then
                            Choice(5) = ""
                        Else
                            Choice(5) = PhoneNeat(drgp("Fax"))
                        End If
                    ElseIf drgp("RegularRDM_ID") = 2 Then
                        Choice(4) = "Interface"
                        Choice(5) = ""
                    ElseIf drgp("RegularRDM_ID") = 3 Then
                        Choice(4) = "Print"
                        If drgp("RptCopies") IsNot DBNull.Value Then
                            Choice(5) = drgp("RptCopies").ToString
                        Else
                            Choice(5) = "1"
                        End If
                    ElseIf drgp("RegularRDM_ID") = 4 Then
                        Choice(4) = "ProlisOn"
                        Choice(5) = IIf(drgp("ProlisOn") = 0, "0", "1")
                    Else
                        Choice(4) = "Phone"
                        If drgp("Phone") IsNot DBNull.Value Then
                            Choice(5) = PhoneNeat(drgp("Phone"))
                        Else
                            Choice(5) = ""
                        End If
                    End If
                End If
            End While
        End If
        cngp.Close()
        cngp = Nothing
        Return Choice
    End Function

    Private Sub btnDismis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDismis.Click
        If AccessionID <> -1 Then
            Dim cnpn As New SqlClient.SqlConnection(connString)
            cnpn.Open()
            Dim cmdpn As New SqlClient.SqlCommand("Panic_Notifications_SP", cnpn)
            cmdpn.CommandType = CommandType.StoredProcedure
            cmdpn.Parameters.AddWithValue("@act", "Upsert")
            cmdpn.Parameters.AddWithValue("@Accession_ID", AccessionID)
            cmdpn.Parameters.AddWithValue("@Test_ID", dgvResults.Rows(0).Cells(0).Value)
            cmdpn.Parameters.AddWithValue("@NotifyDate", Date.Now)
            cmdpn.Parameters.AddWithValue("@RecipientName", "Dismissed")
            cmdpn.Parameters.AddWithValue("@TestResults", "")
            cmdpn.Parameters.AddWithValue("@Faxed", 0)
            cmdpn.Parameters.AddWithValue("@Fax", "")
            cmdpn.Parameters.AddWithValue("@User_ID", ThisUser.ID)
            cmdpn.Parameters.AddWithValue("@UserName", ThisUser.Name)
            Try
                cmdpn.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                cnpn.Close()
                cnpn = Nothing
            End Try
            txtComment.Text = ""
            dgvResults.Rows.Clear()
            txtProvider.Text = ""
            txtAccID.Text = ""
            txtPatient.Text = ""
            txtOrdPhone.Text = ""
            txtAttPhone.Text = ""
            dgvFaxNos.Rows.Clear()
            btnFax.Enabled = False
            frmDashboard.lblAlert_Click(Nothing, Nothing)
            DisplayAccessions()
            '
            'btnDismis.Enabled = False
        End If
    End Sub

    Private Sub dgvFaxNos_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvFaxNos.CellContentClick
        If e.RowIndex <> -1 Then
            If e.ColumnIndex = 0 Then
                If dgvFaxNos.Rows(e.RowIndex).Cells(0).Value = True Then
                    dgvFaxNos.Rows(e.RowIndex).Cells(0).Value = False
                Else
                    dgvFaxNos.Rows(e.RowIndex).Cells(0).Value = True
                End If
                UpdateFaxing()
            End If
        End If
    End Sub

    Private Sub UpdateFaxing()
        Dim i As Integer
        Dim FxEntry As Boolean = False
        For i = 0 To dgvFaxNos.RowCount - 1
            If dgvFaxNos.Rows(i).Cells(0).Value = True Then    'Fax
                If dgvFaxNos.Rows(i).Cells(1).Value IsNot Nothing AndAlso
                PhoneNeat(dgvFaxNos.Rows(i).Cells(1).Value) <> "" Then
                    FxEntry = True
                    Exit For
                End If
            End If
        Next
        btnFax.Enabled = FxEntry
    End Sub

    Private Sub dgvFaxNos_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvFaxNos.CellEndEdit
        If e.RowIndex <> -1 Then
            If e.ColumnIndex = 1 Then
                UpdateFaxing()
            End If
        End If
    End Sub

    Private Sub btnFailure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFailure.Click
        If dgvResults.RowCount > 0 And txtAccID.Text <> "" And txtReason.Text <> "" Then
            Dim LastFail As String = GetLastFailedTime(Val(txtAccID.Text), 201)
            If LastFail = "" OrElse Date.Now >
            DateAdd(DateInterval.Minute, 10, CDate(LastFail)) Then
                LogUserEvent(ThisUser.ID, 201, Date.Now, "Accession", txtAccID.Text, "", txtReason.Text)
                MsgBox("Panic Notification attempt failure logged.", MsgBoxStyle.Information, "Prolis")
                txtReason.Text = ""
            Else
                MsgBox("Too soon to record the failure attampt. Wait 10 or more minutes and try again.", MsgBoxStyle.Critical, "Prolis")
            End If
        End If
    End Sub

    Private Function GetLastFailedTime(ByVal AccID As Long, ByVal EventID As Integer) As String
        Dim LastFail As String = ""
        Dim cngp As New SqlClient.SqlConnection(connString)
        cngp.Open()
        Dim cmdgp As New SqlClient.SqlCommand("Select Max(Event_Time) " & _
        "as LastFail from User_Event where Event_ID = " & EventID & _
        " and Object_ID = " & AccID, cngp)
        cmdgp.CommandType = CommandType.Text
        Dim drgp As SqlClient.SqlDataReader = cmdgp.ExecuteReader
        If drgp.HasRows Then
            While drgp.Read
                If drgp("LastFail") IsNot DBNull.Value _
                AndAlso drgp("LastFail") <> "#12:00:00 AM#" _
                Then LastFail = drgp("LastFail").ToString
            End While
        End If
        cngp.Close()
        cngp = Nothing
        Return LastFail
    End Function

    Private Sub txtReason_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtReason.TextChanged
        If txtReason.Text = "" Then
            btnFailure.Enabled = False
        Else
            btnFailure.Enabled = True
        End If
    End Sub
End Class
