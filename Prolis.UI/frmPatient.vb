Imports System.Security.AccessControl
Imports System.IO
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports Microsoft.Data.SqlClient
Imports SqlConnection = Microsoft.Data.SqlClient.SqlConnection
Imports SqlCommand = Microsoft.Data.SqlClient.SqlCommand
Imports SqlDataReader = Microsoft.Data.SqlClient.SqlDataReader

Public Class frmPatient
    Private OldVals() As String = {""}
    Private NewVals() As String = {""}
    'Dim connStr As String = connString
    Dim selectedFile As Byte()
    Dim selectedFileType As String
    Private AlertFont As Font

    Private Sub frmPatient_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If frmRequisitions.IsHandleCreated Then frmRequisitions.Close()
        cmbSex.Items.Clear()
        If SystemConfig.DiagTarget = "V" Then
            cmbSex.Items.Add("F - Female")
            cmbSex.Items.Add("M - Male")
            cmbSex.Items.Add("N - Neutered")
            cmbSex.Items.Add("S - Spayed")
            '
            PopulateSpecies()
            cmbSpecies.Enabled = True
            cmbBreed.Enabled = True
        Else
            cmbSex.Items.Add("F - Female")
            cmbSex.Items.Add("M - Male")
            cmbSex.Items.Add("G - Transgender Female")
            cmbSex.Items.Add("N - Transgender Male")
            cmbSex.Items.Add("I - Indetermined")
            cmbSex.Items.Add("U - Unreported")
            '
            cmbSpecies.Items.Clear()
            cmbBreed.Items.Clear()
            cmbSpecies.Enabled = False
            cmbBreed.Enabled = False
        End If
        If frmPatLookUp.Visible = True Then btnPatLook.Enabled = False
        If frmPatLookUp.Visible = True Then btnPSubLook.Enabled = False
        If frmPatLookUp.Visible = True Then btnSSubLook.Enabled = False
        PopulatePayers()
        txtDOB.Mask = Replace(Replace(Replace(SystemConfig.DateFormat, "y", "0"), "M", "0"), "d", "0")
        cmbPRelation.SelectedIndex = 0
        cmbSRelation.SelectedIndex = 0
        PopulateRaces(cmbRace)
        cmbRace.SelectedIndex = 5
        cmbEthnicity.SelectedIndex = 0
        txtPatientID.Text = NextPatientID()
        txtLName.Focus()
        AlertFont = txtAlert.Font

        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
        LoadFiles()
    End Sub

    Private Sub PopulatePayers()
        cmbPIns.Items.Clear()
        cmbSIns.Items.Clear()
        Dim sSQL As String = "Select * from Payers order by PayerName"
        Dim cnn As New SqlConnection(connString)
        cnn.Open()
        Dim cmdsel As New SqlCommand(sSQL, cnn)
        cmdsel.CommandType = Data.CommandType.Text
        Dim DRsel As SqlDataReader = cmdsel.ExecuteReader
        If DRsel.HasRows Then
            While DRsel.Read
                cmbPIns.Items.Add(New MyList(DRsel("PayerName"), DRsel("ID")))
                cmbSIns.Items.Add(New MyList(DRsel("PayerName"), DRsel("ID")))
            End While
        End If
        cnn.Close()
        cnn = Nothing
    End Sub

    Private Sub ClearForm()
        ClearPatient()
        ClearPrimary()
        ClearSecondary()
        txtAlert.Clear()

        txtAlert.Font = AlertFont
        txtAlert.BackColor = Color.White
        txtAlert.ForeColor = Color.Black
    End Sub

    Private Sub ClearPatient()
        txtPatientID.Text = ""
        txtLName.Text = ""
        txtFName.Text = ""
        txtMName.Text = ""
        cmbSex.SelectedIndex = -1
        txtDOB.Text = ""
        txtSSN.Text = ""
        chkAlive.Checked = True
        txtHPhone.Text = ""
        txtCell.Text = ""
        txtWPhone.Text = ""
        txtFax.Text = ""
        txtEmail.Text = ""
        txtPassword.Text = ""
        cmbSecretQ.SelectedIndex = -1
        txtSecretA.Text = ""
        txtEmployer.Text = ""
        txtAdd1.Text = ""
        txtAdd2.Text = ""
        txtCity.Text = ""
        txtState.Text = ""
        txtZip.Text = ""
        txtCountry.Text = ""
        txtNote.Text = ""
        cmbEthnicity.SelectedIndex = 0
        cmbRace.SelectedIndex = 5

        txtAlert.Clear()

        txtAlert.Font = AlertFont
        txtAlert.BackColor = Color.White
        txtAlert.ForeColor = Color.Black

        If SystemConfig.DiagTarget = "V" Then
            cmbBreed.Items.Clear()
            cmbSpecies.SelectedIndex = -1
        End If
    End Sub

    Private Sub ClearSecondary()
        cmbSIns.SelectedIndex = -1
        txtSGroup.Text = ""
        txtSPolicy.Text = ""
        txtSFrom.Text = ""
        txtSTo.Text = ""
        txtSCopay.Text = ""
        cmbSRelation.SelectedIndex = 0
        ClearSSubscriber()
    End Sub

    Private Sub ClearPrimary()
        cmbPIns.SelectedIndex = -1
        txtPGroup.Text = ""
        txtPPolicy.Text = ""
        txtPFrom.Text = ""
        txtPTo.Text = ""
        txtPCopay.Text = ""
        cmbPRelation.SelectedIndex = 0
        ClearPSubscriber()
    End Sub

    Private Sub DisplayPatient(ByVal PatID As Long)
        ClearPatient()
        Dim ItemX As MyList
        Dim cnpat As New SqlConnection(connString)
        cnpat.Open()
        Dim cmdpat As New SqlCommand("Select " &
        "* from Patients where ID = " & PatID, cnpat)
        cmdpat.CommandType = CommandType.Text
        Dim drpat As SqlDataReader = cmdpat.ExecuteReader
        If drpat.HasRows Then
            While drpat.Read
                txtPatientID.Text = drpat("ID")
                txtLName.Text = drpat("LastName")
                txtFName.Text = drpat("FirstName")
                If IsDate(drpat("DOB")) Then
                    txtDOB.Text = Format(drpat("DOB"), SystemConfig.DateFormat)
                Else
                    '  txtDOB.Text = Format(drpat("DOB"), SystemConfig.DateFormat)
                End If

                For i As Integer = 0 To cmbSex.Items.Count - 1
                    If drpat("Sex") Is DBNull.Value Then
                    Else
                        If cmbSex.Items(i).ToString.Substring(0, 1) = drpat("Sex").ToString.Substring(0, 1) Then
                            cmbSex.SelectedIndex = i
                            Exit For
                        End If
                    End If

                Next
                If drpat("Tage") IsNot DBNull.Value Then txtTage.Text = drpat("Tage")
                If drpat("IsAlive") IsNot DBNull.Value Then chkAlive.Checked = drpat("IsAlive")
                If drpat("DeathDate") IsNot DBNull.Value Then _
                txtDeathDate.Text = Format(drpat("DeathDate"), SystemConfig.DateFormat)
                If drpat("SSN") IsNot DBNull.Value Then txtSSN.Text = drpat("SSN")
                If drpat("MiddleName") IsNot DBNull.Value Then txtMName.Text = drpat("MiddleName")
                If drpat("HomePhone") IsNot DBNull.Value Then txtHPhone.Text = drpat("HomePhone")
                If drpat("Cell") IsNot DBNull.Value Then txtCell.Text = drpat("Cell")
                If drpat("WorkPhone") IsNot System.DBNull.Value Then txtWPhone.Text = drpat("WorkPhone")
                If drpat("Fax") IsNot DBNull.Value Then txtFax.Text = drpat("Fax")
                If drpat("Email") IsNot DBNull.Value Then txtEmail.Text = drpat("Email")
                If drpat("Password") IsNot DBNull.Value Then txtPassword.Text = drpat("Password")
                If drpat("SecretQ") IsNot DBNull.Value Then
                    For i As Integer = 0 To cmbSecretQ.Items.Count - 1
                        If drpat("SecretQ") = cmbSecretQ.Items(i).ToString Then
                            cmbSecretQ.SelectedIndex = i
                            Exit For
                        End If
                    Next
                End If
                If drpat("SecretA") IsNot DBNull.Value Then txtSecretA.Text = drpat("SecretA")
                If drpat("Employer_ID") IsNot DBNull.Value Then txtEmployer.Text = GetEmployer(drpat("Employer_ID"))
                If drpat("Note") IsNot DBNull.Value Then txtNote.Text = drpat("Note")
                If drpat("Address_ID") IsNot DBNull.Value Then
                    txtAdd1.Text = GetAddress1(drpat("Address_ID"))
                    txtAdd2.Text = GetAddress2(drpat("Address_ID"))
                    txtCity.Text = GetAddressCity(drpat("Address_ID"))
                    txtState.Text = GetAddressState(drpat("Address_ID"))
                    txtZip.Text = GetAddressZip(drpat("Address_ID"))
                    txtCountry.Text = GetAddressCountry(drpat("Address_ID"))
                End If
                If drpat("Race_ID") IsNot DBNull.Value Then
                    For i As Integer = 0 To cmbRace.Items.Count - 1
                        If i = drpat("Race_ID") Then
                            cmbRace.SelectedIndex = i
                            Exit For
                        End If
                    Next
                Else
                    cmbRace.SelectedIndex = 7
                End If
                '
                If drpat("Ethnicity") IsNot DBNull.Value Then
                    For i As Integer = 0 To cmbEthnicity.Items.Count - 1
                        If cmbEthnicity.Items(i).ToString = drpat("Ethnicity") Then
                            cmbEthnicity.SelectedIndex = i
                            Exit For
                        End If
                    Next
                Else
                    cmbEthnicity.SelectedIndex = 0
                End If
                Try
                    If drpat("Alert") IsNot DBNull.Value Then txtAlert.Rtf = drpat("Alert")

                Catch ex As Exception

                End Try
                Try
                    If drpat("Alert_CS") IsNot DBNull.Value Then chkCS.Checked = drpat("Alert_CS")

                Catch ex As Exception

                End Try
                Try
                    If drpat("Alert_Acc") IsNot DBNull.Value Then chkAcc.Checked = drpat("Alert_Acc")

                Catch ex As Exception

                End Try
                'chkCS.Checked = drpat("Alert_CS")
                'chkAcc.Checked = drpat("Alert_Acc")

                '
                If SystemConfig.DiagTarget = "V" Then
                    For i As Integer = 0 To cmbSpecies.Items.Count - 1
                        ItemX = cmbSpecies.Items(i)
                        If ItemX.ItemData = drpat("Species_ID") Then
                            cmbSpecies.SelectedIndex = i
                            Exit For
                        End If
                    Next
                    My.Application.DoEvents()
                    For i As Integer = 0 To cmbBreed.Items.Count - 1
                        ItemX = cmbBreed.Items(i)
                        If ItemX.ItemData = drpat("Breed_ID") Then
                            cmbBreed.SelectedIndex = i
                            Exit For
                        End If
                    Next
                End If
            End While
        End If
        cnpat.Close()
        cnpat = Nothing
        '
        DisplayPCoverage(PatID)
        DisplaySCoverage(PatID)
        LoadFiles()
        OldVals = PatientSnapshot()
        btnDelete.Enabled = True
        btnSave.Enabled = True
    End Sub

    Private Sub PopulateBreeds(ByVal SpeciesID As Integer)
        cmbBreed.Items.Clear()
        Dim cnbr As New SqlConnection(connString)
        cnbr.Open()
        Dim cmdbr As New SqlCommand("Select * from Breeds " &
        "where Species_ID = " & SpeciesID & " order by Breed", cnbr)
        cmdbr.CommandType = CommandType.Text
        Dim drbr As SqlDataReader = cmdbr.ExecuteReader
        If drbr.HasRows Then
            While drbr.Read
                cmbBreed.Items.Add(New MyList(drbr("Breed"), drbr("ID")))
            End While
        End If
        cnbr.Close()
        cnbr = Nothing
    End Sub

    Private Sub PopulateSpecies()
        cmbSpecies.Items.Clear()
        Dim cnsp As New SqlConnection(connString)
        cnsp.Open()
        Dim cmdsp As New SqlCommand("Select * from Species order by Species", cnsp)
        cmdsp.CommandType = CommandType.Text
        Dim drsp As SqlDataReader = cmdsp.ExecuteReader
        If drsp.HasRows Then
            While drsp.Read
                cmbSpecies.Items.Add(New MyList(drsp("Species"), drsp("ID")))
            End While
        End If
        cnsp.Close()
        cnsp = Nothing
    End Sub

    Private Function PatientSnapshot() As String()
        Dim vals(20) As String
        vals(0) = Trim(txtPatientID.Text)
        vals(1) = Trim(txtLName.Text)
        vals(2) = Trim(txtFName.Text)
        vals(3) = Trim(txtMName.Text)
        vals(4) = Trim(txtDOB.Text)
        If cmbSex.SelectedIndex <> -1 Then
            vals(5) = cmbSex.SelectedItem.ToString.Substring(0, 1)
        Else
            vals(5) = ""
        End If
        vals(6) = CleanIt(txtSSN.Text)
        vals(7) = Trim(txtHPhone.Text)
        vals(8) = Trim(txtCell.Text)
        vals(9) = Trim(txtEmail.Text)
        vals(10) = cmbEthnicity.SelectedItem.ToString
        vals(11) = GetAddressID(Trim(txtAdd1.Text), Trim(txtAdd2.Text), Trim(txtCity.Text),
        Trim(txtState.Text), Trim(txtZip.Text), Trim(txtCountry.Text)).ToString
        If cmbPIns.SelectedIndex <> -1 Then
            Dim ItemX As MyList = cmbPIns.SelectedItem
            vals(12) = ItemX.ItemData.ToString
            vals(13) = Trim(txtPGroup.Text)
            vals(14) = Trim(txtPPolicy.Text)
            vals(15) = cmbPRelation.SelectedItem.ToString
        Else
            vals(12) = ""
            vals(13) = ""
            vals(14) = ""
            vals(15) = ""
        End If
        If cmbSIns.SelectedIndex <> -1 Then
            Dim ItemX As MyList = cmbSIns.SelectedItem
            vals(16) = ItemX.ItemData.ToString
            vals(17) = Trim(txtSGroup.Text)
            vals(18) = Trim(txtSPolicy.Text)
            vals(19) = cmbSRelation.SelectedItem.ToString
        Else
            vals(16) = ""
            vals(17) = ""
            vals(18) = ""
            vals(19) = ""
        End If
        vals(20) = ThisUser.ID.ToString
        Return vals
    End Function

    Private Function GetEmployer(ByVal ID As Long) As String
        Dim Emp As String = ""
        Dim cnge As New SqlConnection(connString)
        cnge.Open()
        Dim cmdge As New SqlCommand(
        "Select * from Employers where ID = " & ID, cnge)
        cmdge.CommandType = CommandType.Text
        Dim drge As SqlDataReader = cmdge.ExecuteReader
        If drge.HasRows Then
            While drge.Read
                If drge("Employer") Is DBNull.Value Then
                    Emp = ""
                Else
                    Emp = drge("Employer")
                End If
            End While
        End If
        cnge.Close()
        cnge = Nothing
        Return Emp
    End Function

    Private Sub DisplayPSub(ByVal PatID As Long)
        Try
            ClearPSubscriber()
            Dim cnps As New SqlConnection(connString)
            cnps.Open()
            Dim cmdps As New SqlCommand("Select " &
            "* from Patients where ID = " & PatID, cnps)
            cmdps.CommandType = CommandType.Text
            Dim drps As SqlDataReader = cmdps.ExecuteReader
            If drps.HasRows Then
                While drps.Read
                    txtPSubID.Text = drps("ID")
                    txtPSubLName.Text = drps("LastName")
                    txtPSubFName.Text = drps("FirstName")
                    txtPSubDOB.Text = Format(drps("DOB"), SystemConfig.DateFormat)
                    For i As Integer = 0 To cmbPSubSex.Items.Count - 1
                        If cmbPSubSex.Items(i).ToString.Substring(0, 1) = drps("Sex") Then
                            cmbPSubSex.SelectedIndex = i
                            Exit For
                        End If
                    Next
                    If drps("SSN") IsNot DBNull.Value Then txtPSubSSN.Text = drps("SSN")
                    If drps("MiddleName") IsNot DBNull.Value Then txtPSubMName.Text = drps("MiddleName")
                    If drps("HomePhone") IsNot DBNull.Value Then txtPSubHPhone.Text = drps("HomePhone")
                    If drps("Cell") IsNot DBNull.Value Then txtPSubCell.Text = drps("Cell")
                    If drps("WorkPhone") IsNot DBNull.Value Then txtPSubWPhone.Text = drps("WorkPhone")
                    If drps("Fax") IsNot DBNull.Value Then txtPSubFax.Text = drps("Fax")
                    If drps("Email") IsNot DBNull.Value Then txtPSubEmail.Text = drps("Email")
                    If drps("Employer_ID") IsNot DBNull.Value Then txtPSubEmployer.Text = GetEmployer(drps("Employer_ID"))
                    If drps("Address_ID") IsNot DBNull.Value Then
                        txtPSubAdd1.Text = GetAddress1(drps("Address_ID"))
                        txtPSubAdd2.Text = GetAddress2(drps("Address_ID"))
                        txtPSubCity.Text = GetAddressCity(drps("Address_ID"))
                        txtPSubState.Text = GetAddressState(drps("Address_ID"))
                        txtPSubZip.Text = GetAddressZip(drps("Address_ID"))
                        txtPSubCountry.Text = GetAddressCountry(drps("Address_ID"))
                    End If
                End While
            End If
            cnps.Close()
            cnps = Nothing
        Catch Ex As Exception
            MsgBox(Ex.Message, MsgBoxStyle.Critical, "Prolis")
            'Finally
        End Try
    End Sub

    Private Sub DisplaySSub(ByVal PatID As Long)
        Try
            ClearSSubscriber()
            Dim cnss As New SqlConnection(connString)
            cnss.Open()
            Dim cmdss As New SqlCommand("Select " &
            "* from Patients where ID = " & PatID, cnss)
            cmdss.CommandType = CommandType.Text
            Dim drss As SqlDataReader = cmdss.ExecuteReader
            If drss.HasRows Then
                While drss.Read
                    txtSSubID.Text = drss("ID")
                    txtSSubLName.Text = drss("LastName")
                    txtSSubFName.Text = drss("FirstName")
                    txtSSubDOB.Text = Format(drss("DOB"), SystemConfig.DateFormat)
                    For i As Integer = 0 To cmbSSubSex.Items.Count - 1
                        If cmbSSubSex.Items(i).ToString.Substring(0, 1) = drss("Sex") Then
                            cmbSSubSex.SelectedIndex = i
                            Exit For
                        End If
                    Next
                    If drss("SSN") IsNot DBNull.Value Then txtSSubSSN.Text = drss("SSN")
                    If drss("MiddleName") IsNot DBNull.Value Then txtSSubMName.Text = drss("MiddleName")
                    If drss("HomePhone") IsNot DBNull.Value Then txtSSubHPhone.Text = drss("HomePhone")
                    If drss("Cell") IsNot DBNull.Value Then txtSSubCell.Text = drss("Cell")
                    If drss("WorkPhone") IsNot DBNull.Value Then txtSSubWPhone.Text = drss("WorkPhone")
                    If drss("Fax") IsNot DBNull.Value Then txtSSubFax.Text = drss("Fax")
                    If drss("Email") IsNot DBNull.Value Then txtSSubEmail.Text = drss("Email")
                    If drss("Employer_ID") IsNot DBNull.Value Then txtSSubEmployer.Text = GetEmployer(drss("Employer_ID"))
                    If drss("Address_ID") IsNot DBNull.Value Then
                        txtSSubAdd1.Text = GetAddress1(drss("Address_ID"))
                        txtSSubAdd2.Text = GetAddress2(drss("Address_ID"))
                        txtSSubCity.Text = GetAddressCity(drss("Address_ID"))
                        txtSSubState.Text = GetAddressState(drss("Address_ID"))
                        txtSSubZip.Text = GetAddressZip(drss("Address_ID"))
                        txtSSubCountry.Text = GetAddressCountry(drss("Address_ID"))
                    End If
                End While
            End If
            cnss.Close()
            cnss = Nothing
        Catch Ex As Exception
            MsgBox(Ex.Message, MsgBoxStyle.Critical, "Prolis")
            'Finally
        End Try
    End Sub

    Private Sub ClearPSubscriber()
        txtPSubID.Text = ""
        txtPSubLName.Text = ""
        txtPSubFName.Text = ""
        txtPSubMName.Text = ""
        cmbPSubSex.SelectedIndex = 0
        txtPSubDOB.Text = ""
        txtPSubSSN.Text = ""
        txtPSubHPhone.Text = ""
        txtPSubCell.Text = ""
        txtPSubWPhone.Text = ""
        txtPSubFax.Text = ""
        txtPSubEmail.Text = ""
        txtPSubEmployer.Text = ""
        txtPSubAdd1.Text = ""
        txtPSubAdd2.Text = ""
        txtPSubCity.Text = ""
        txtPSubState.Text = ""
        txtPSubZip.Text = ""
        txtPSubCountry.Text = ""
    End Sub

    Private Sub ClearSSubscriber()
        txtSSubID.Text = ""
        txtSSubLName.Text = ""
        txtSSubFName.Text = ""
        txtSSubMName.Text = ""
        cmbSSubSex.SelectedIndex = -1
        txtSSubDOB.Text = ""
        txtSSubSSN.Text = ""
        txtSSubHPhone.Text = ""
        txtSSubCell.Text = ""
        txtSSubWPhone.Text = ""
        txtSSubFax.Text = ""
        txtSSubEmail.Text = ""
        txtSSubEmployer.Text = ""
        txtSSubAdd1.Text = ""
        txtSSubAdd2.Text = ""
        txtSSubCity.Text = ""
        txtSSubState.Text = ""
        txtSSubZip.Text = ""
        txtSSubCountry.Text = ""
    End Sub

    Private Sub cmbPRelation_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPRelation.SelectedIndexChanged
        If cmbPRelation.SelectedIndex = 0 Then
            ClearPSubscriber()
            grpPSubs.Enabled = False
        Else
            grpPSubs.Enabled = True
        End If
    End Sub

    Private Sub cmbSRelation_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSRelation.SelectedIndexChanged
        If cmbSRelation.SelectedIndex = 0 Then
            ClearSSubscriber()
            grpSSubs.Enabled = False
        Else
            grpSSubs.Enabled = True
        End If
    End Sub

    Private Sub chkNewEdit_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkNewEdit.CheckedChanged
        If chkNewEdit.Checked = False Then
            chkNewEdit.Text = "New"
            chkNewEdit.Image = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Images\New.ico")
            ClearForm()
            txtPatientID.Text = NextPatientID()
            txtPatientID.ReadOnly = True
            btnPatLook.Enabled = False
            btnPrint.Enabled = False
            btnImport.Enabled = False
        Else
            chkNewEdit.Text = "Edit"
            chkNewEdit.Image = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Images\Edit.ico")
            btnPrint.Enabled = True
            ClearForm()
            btnImport.Enabled = True
            txtPatientID.ReadOnly = False
            If frmPatLookUp.Visible = False Then btnPatLook.Enabled = True
        End If
    End Sub

    Private Function NextPatientID() As Long
        Dim NID As Long = 1
        Dim cnpid As New SqlConnection(connString)
        cnpid.Open()
        Dim cmdpid As New SqlCommand("Select max(ID) as LastID from Patients", cnpid)
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

    Private Sub btnPatLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatLook.Click
        Dim PatientID As String = frmPatLookUp.ShowDialog()
        If PatientID <> "" Then
            DisplayPatient(Val(PatientID))
        End If
    End Sub

    Private Sub DisplayPCoverage(ByVal PatID As Long)
        ClearPrimary()
        Dim ItemX As MyList
        Dim cndpc As New SqlConnection(connString)
        cndpc.Open()
        Dim cmddpc As New SqlCommand("Select * from " &
        "Coverages where Preference = 'P' and Patient_ID = " & PatID, cndpc)
        cmddpc.CommandType = CommandType.Text
        Dim drdpc As SqlDataReader = cmddpc.ExecuteReader
        If drdpc.HasRows Then
            While drdpc.Read
                For i As Integer = 0 To cmbPIns.Items.Count - 1
                    ItemX = cmbPIns.Items(i)
                    If ItemX.ItemData = drdpc("Insurance_ID") Then
                        cmbPIns.SelectedIndex = i
                        Exit For
                    End If
                Next
                If drdpc("GroupNo") IsNot DBNull.Value Then txtPGroup.Text = drdpc("GroupNo")
                If drdpc("PolicyNo") IsNot DBNull.Value Then txtPPolicy.Text = drdpc("PolicyNo")
                If drdpc("StartDate") IsNot DBNull.Value Then txtPFrom.Text = Format(drdpc("StartDate"), SystemConfig.DateFormat)
                If drdpc("ExpireDate") IsNot DBNull.Value Then txtPTo.Text = Format(drdpc("ExpireDate"), SystemConfig.DateFormat)
                If drdpc("Relation") IsNot DBNull.Value Then
                    cmbPRelation.SelectedIndex = drdpc("Relation")
                Else
                    cmbPRelation.SelectedIndex = 0
                End If
                If cmbPRelation.SelectedIndex > 0 Then DisplayPSub(drdpc("Insured_ID"))
                If drdpc("Copayment") IsNot DBNull.Value Then txtPCopay.Text = Format(drdpc("Copayment"), "##,##0.00")
            End While
        End If
        cndpc.Close()
        cndpc = Nothing
    End Sub

    Private Sub DisplaySCoverage(ByVal PatID As Long)
        Dim ItemX As MyList
        ClearSecondary()
        Dim cndsc As New SqlConnection(connString)
        cndsc.Open()
        Dim cmddsc As New SqlCommand("Select * from " &
        "Coverages where Preference = 'S' and Patient_ID = " & PatID, cndsc)
        cmddsc.CommandType = CommandType.Text
        Dim drdsc As SqlDataReader = cmddsc.ExecuteReader
        If drdsc.HasRows Then
            While drdsc.Read
                For i As Integer = 0 To cmbSIns.Items.Count - 1
                    ItemX = cmbSIns.Items(i)
                    If ItemX.ItemData = drdsc("Insurance_ID") Then
                        cmbSIns.SelectedIndex = i
                        Exit For
                    End If
                Next
                If drdsc("GroupNo") IsNot DBNull.Value Then txtSGroup.Text = drdsc("GroupNo")
                If drdsc("PolicyNo") IsNot DBNull.Value Then txtSPolicy.Text = drdsc("PolicyNo")
                If drdsc("StartDate") IsNot DBNull.Value Then txtSFrom.Text = Format(drdsc("StartDate"), SystemConfig.DateFormat)
                If drdsc("ExpireDate") IsNot DBNull.Value Then txtSTo.Text = Format(drdsc("ExpireDate"), SystemConfig.DateFormat)
                cmbSRelation.SelectedIndex = drdsc("Relation")
                If cmbSRelation.SelectedIndex > 0 Then DisplaySSub(drdsc("Insured_ID"))
                If drdsc("Copayment") IsNot DBNull.Value Then txtSCopay.Text = Format(drdsc("Copayment"), "##,##0.00")
            End While
        End If
        cndsc.Close()
        cndsc = Nothing
    End Sub

    Private Sub Update_Progress()
        If txtLName.Text <> "" And txtFName.Text <> "" And
        cmbSex.SelectedIndex <> -1 And IsDate(txtDOB.Text) = True Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub AddressValidate()
        If (txtAdd1.Text <> "" Or txtCity.Text <> "" Or txtState.Text <> "" Or txtZip.Text <> "") Then
            If txtCity.Text = "" Then
                MsgBox("In order to enter an address, Prolis requires the information in all of the magenta color labeled address fields.", MsgBoxStyle.Exclamation)
                txtCity.Focus()
            ElseIf txtState.Text = "" Then
                MsgBox("In order to enter an address, Prolis requires the information in all of the magenta color labeled address fields.", MsgBoxStyle.Exclamation)
                txtState.Focus()
            ElseIf txtZip.Text = "" Then
                MsgBox("In order to enter an address, Prolis requires the information in all of the magenta color labeled address fields.", MsgBoxStyle.Exclamation)
                txtZip.Focus()
            ElseIf txtAdd1.Text = "" Then
                MsgBox("In order to enter an address, Prolis requires the information in all of the magenta color labeled address fields.", MsgBoxStyle.Exclamation)
                txtAdd1.Focus()
            End If
        End If
        'If (txtAdd1.Text <> "" AndAlso (txtCity.Text = "" Or txtState.Text = "" Or txtZip.Text = "")) _
        'OrElse (txtCity.Text <> "" AndAlso (txtAdd1.Text = "" Or txtState.Text = "" Or txtZip.Text = "")) _
        'OrElse (txtState.Text <> "" AndAlso (txtAdd1.Text = "" Or txtCity.Text = "" Or txtZip.Text = "")) _
        'OrElse (txtZip.Text <> "" AndAlso (txtAdd1.Text = "" Or txtState.Text = "" Or txtCity.Text = "")) Then
        '    MsgBox("In order to enter an address, Prolis requires the information in all of the magenta color labeled address fields.", MsgBoxStyle.Exclamation)
        '    If txtAdd1.Text = "" Then
        '        txtAdd1.Focus()
        '    ElseIf txtCity.Text = "" Then
        '        txtCity.Focus()
        '    ElseIf txtState.Text = "" Then
        '        txtState.Focus()
        '    ElseIf txtZip.Text = "" Then
        '        txtZip.Focus()
        '    ElseIf txtAdd1.Text <> "" Then
        '        txtAdd2.Focus()
        '    ElseIf txtZip.Text <> "" Then
        '        txtCountry.Focus()
        '    End If
        'End If
    End Sub

    Private Sub SavePrimeCoverage(ByVal PatID As Long)
        If cmbPIns.SelectedIndex <> -1 AndAlso Trim(txtPPolicy.Text) <> "" AndAlso
       ((cmbPRelation.SelectedIndex = 0) OrElse
       (cmbPRelation.SelectedIndex <> 0 AndAlso
       txtPSubLName.Text <> "" AndAlso txtPSubFName.Text <> "" AndAlso
       cmbPSubSex.SelectedIndex <> -1 AndAlso IsDate(txtPSubDOB.Text))) Then

            Using connection As New SqlConnection(connString)
                connection.Open()

                ' Delete existing coverage for patient
                Using commandDelete As New SqlCommand("DELETE FROM Coverages WHERE Patient_ID = @PatID", connection)
                    commandDelete.Parameters.AddWithValue("@PatID", PatID)
                    commandDelete.ExecuteNonQuery()
                End Using

                ' Insert or update coverage
                Dim queryUpsert As String = "
                IF EXISTS (SELECT 1 FROM Coverages WHERE Patient_ID = @PatID AND Insurance_ID = @InsuranceID)
                    UPDATE Coverages 
                    SET Relation = @Relation, Insured_ID = @InsuredID, Preference = 'P',
                        GroupNo = @GroupNo, PolicyNo = @PolicyNo, StartDate = @StartDate,
                        ExpireDate = @ExpireDate, CoPayment = @CoPayment, LastEditedOn = @LastEditedOn, 
                        EditedBy = @EditedBy
                    WHERE Patient_ID = @PatID AND Insurance_ID = @InsuranceID
                ELSE
                    INSERT INTO Coverages 
                    (Patient_ID, Insurance_ID, Relation, Insured_ID, Preference, 
                     GroupNo, PolicyNo, StartDate, ExpireDate, CoPayment, LastEditedOn, EditedBy)
                    VALUES 
                    (@PatID, @InsuranceID, @Relation, @InsuredID, 'P', 
                     @GroupNo, @PolicyNo, @StartDate, @ExpireDate, @CoPayment, @LastEditedOn, @EditedBy)"

                Using commandUpsert As New SqlCommand(queryUpsert, connection)
                    Dim ItemX As MyList = DirectCast(cmbPIns.SelectedItem, MyList)
                    commandUpsert.Parameters.AddWithValue("@PatID", PatID)
                    commandUpsert.Parameters.AddWithValue("@InsuranceID", ItemX.ItemData)
                    commandUpsert.Parameters.AddWithValue("@Relation", cmbPRelation.SelectedIndex)

                    Dim insuredID As Long = If(cmbPRelation.SelectedIndex = 0, PatID,
                                           GetPSubID(txtPSubLName.Text, txtPSubFName.Text,
                                           cmbPSubSex.SelectedItem.ToString.Substring(0, 1),
                                           CDate(txtPSubDOB.Text)))
                    commandUpsert.Parameters.AddWithValue("@InsuredID", insuredID)

                    commandUpsert.Parameters.AddWithValue("@GroupNo", txtPGroup.Text)
                    commandUpsert.Parameters.AddWithValue("@PolicyNo", txtPPolicy.Text)
                    commandUpsert.Parameters.AddWithValue("@StartDate", If(IsDate(txtPFrom.Text), CDate(txtPFrom.Text), DBNull.Value))
                    commandUpsert.Parameters.AddWithValue("@ExpireDate", If(IsDate(txtPTo.Text), CDate(txtPTo.Text), DBNull.Value))
                    commandUpsert.Parameters.AddWithValue("@CoPayment", Val(txtPCopay.Text))
                    commandUpsert.Parameters.AddWithValue("@LastEditedOn", Date.Now)
                    commandUpsert.Parameters.AddWithValue("@EditedBy", ThisUser.ID)

                    commandUpsert.ExecuteNonQuery()
                End Using
            End Using
        End If
    End Sub
    Private Sub SaveSecondCoverage(ByVal PatID As Long)
        If cmbSIns.SelectedIndex <> -1 AndAlso Trim(txtSPolicy.Text) <> "" AndAlso
       ((cmbSRelation.SelectedIndex = 0) OrElse
       (cmbSRelation.SelectedIndex <> 0 AndAlso
       txtSSubLName.Text <> "" AndAlso txtSSubFName.Text <> "" AndAlso
       cmbSSubSex.SelectedIndex <> -1 AndAlso IsDate(txtSSubDOB.Text))) Then

            Using connection As New SqlConnection(connString)
                connection.Open()

                ' Delete existing secondary coverage for patient
                Using commandDelete As New SqlCommand("DELETE FROM Coverages WHERE Preference = 'S' AND Patient_ID = @PatID", connection)
                    commandDelete.Parameters.AddWithValue("@PatID", PatID)
                    commandDelete.ExecuteNonQuery()
                End Using

                ' Insert or update secondary coverage
                Dim queryUpsert As String = "
                IF EXISTS (SELECT 1 FROM Coverages WHERE Patient_ID = @PatID AND Insurance_ID = @InsuranceID)
                    UPDATE Coverages 
                    SET Relation = @Relation, Insured_ID = @InsuredID, Preference = 'S',
                        GroupNo = @GroupNo, PolicyNo = @PolicyNo, StartDate = @StartDate,
                        ExpireDate = @ExpireDate, CoPayment = @CoPayment, LastEditedOn = @LastEditedOn, 
                        EditedBy = @EditedBy
                    WHERE Patient_ID = @PatID AND Insurance_ID = @InsuranceID
                ELSE
                    INSERT INTO Coverages 
                    (Patient_ID, Insurance_ID, Relation, Insured_ID, Preference, 
                     GroupNo, PolicyNo, StartDate, ExpireDate, CoPayment, LastEditedOn, EditedBy)
                    VALUES 
                    (@PatID, @InsuranceID, @Relation, @InsuredID, 'S', 
                     @GroupNo, @PolicyNo, @StartDate, @ExpireDate, @CoPayment, @LastEditedOn, @EditedBy)"

                Using commandUpsert As New SqlCommand(queryUpsert, connection)
                    Dim ItemX As MyList = DirectCast(cmbSIns.SelectedItem, MyList)
                    commandUpsert.Parameters.AddWithValue("@PatID", PatID)
                    commandUpsert.Parameters.AddWithValue("@InsuranceID", ItemX.ItemData)
                    commandUpsert.Parameters.AddWithValue("@Relation", cmbSRelation.SelectedIndex)

                    Dim insuredID As Long = If(cmbSRelation.SelectedIndex = 0, PatID,
                                           GetSSubID(txtSSubLName.Text, txtSSubFName.Text,
                                           cmbSSubSex.SelectedItem.ToString.Substring(0, 1),
                                           CDate(txtSSubDOB.Text)))
                    commandUpsert.Parameters.AddWithValue("@InsuredID", insuredID)

                    commandUpsert.Parameters.AddWithValue("@GroupNo", txtSGroup.Text)
                    commandUpsert.Parameters.AddWithValue("@PolicyNo", txtSPolicy.Text)
                    commandUpsert.Parameters.AddWithValue("@StartDate", If(IsDate(txtSFrom.Text), CDate(txtSFrom.Text), DBNull.Value))
                    commandUpsert.Parameters.AddWithValue("@ExpireDate", If(IsDate(txtSTo.Text), CDate(txtSTo.Text), DBNull.Value))
                    commandUpsert.Parameters.AddWithValue("@CoPayment", Val(txtSCopay.Text))
                    commandUpsert.Parameters.AddWithValue("@LastEditedOn", Date.Today)
                    commandUpsert.Parameters.AddWithValue("@EditedBy", ThisUser.ID)

                    commandUpsert.ExecuteNonQuery()
                End Using
            End Using
        End If
    End Sub
    Private Function GetSSubID(ByVal LName As String, ByVal FName As String,
    ByVal Sex As String, ByVal DOB As Date)
        Dim PatID As Long = GetPatientIDbyNames(LName, FName, DOB, Sex)
        If PatID <= 0 Then PatID = NextPatientID()
        Dim AddressID As Long = -1
        If txtSSubAdd1.Text <> "" And txtSSubCity.Text <> "" And txtSSubCity.Text <> "" _
        And txtSSubState.Text <> "" And txtSSubZip.Text <> "" Then _
        AddressID = GetAddressID(txtSSubAdd1.Text, txtSSubAdd2.Text,
        txtSSubCity.Text, txtSSubState.Text, txtSSubZip.Text, txtSSubCountry.Text)
        Dim cnn As New SqlConnection(connString)
        cnn.Open()
        Dim CMDp As New SqlCommand("Patients_SP", cnn)
        CMDp.CommandType = Data.CommandType.StoredProcedure
        CMDp.Parameters.AddWithValue("@act", "Upsert")
        CMDp.Parameters.AddWithValue("@ID", PatID)
        CMDp.Parameters.AddWithValue("@LastName", LName)
        CMDp.Parameters.AddWithValue("@FirstName", FName)
        CMDp.Parameters.AddWithValue("@DOB", CDate(txtSSubDOB.Text))
        CMDp.Parameters.AddWithValue("@Sex", cmbSSubSex.SelectedItem.Substring(0, 1))
        CMDp.Parameters.AddWithValue("@Tage", Trim(txtSSubTage.Text))
        CMDp.Parameters.AddWithValue("@IsAlive", 1)
        CMDp.Parameters.AddWithValue("@SSN", SSNNeat(txtSSubSSN.Text))
        CMDp.Parameters.AddWithValue("@WorkPhone", PhoneNeat(txtSSubWPhone.Text))
        CMDp.Parameters.AddWithValue("@Cell", PhoneNeat(txtSSubCell.Text))
        CMDp.Parameters.AddWithValue("@HomePhone", PhoneNeat(txtSSubHPhone.Text))
        CMDp.Parameters.AddWithValue("@Fax", PhoneNeat(txtSSubFax.Text))
        CMDp.Parameters.AddWithValue("@Email", Trim(txtSSubEmail.Text))
        If Trim(txtSSubEmployer.Text) <> "" Then _
        CMDp.Parameters.AddWithValue("@Employer_ID", GetEmployerID(txtSSubEmployer.Text))
        'If Trim(txtPassword.Text) <> "" Then _
        'CMDp.Parameters.AddWithValue("@Password", LIC.encryptString(Trim(txtPassword.Text)))
        'If cmbEthnicity.SelectedIndex <> -1 Then
        '    ItemX = cmbEthnicity.SelectedItem
        '    CMDp.Parameters.AddWithValue("@RaceID", ItemX.ItemData)
        'Else
        '    CMDp.Parameters.AddWithValue("@RaceID", 7)
        'End If
        CMDp.Parameters.AddWithValue("@Address_ID", AddressID)
        CMDp.ExecuteNonQuery()
        CMDp.Dispose()
        cnn.Close()
        cnn = Nothing
        Return PatID
    End Function

    Private Function GetPSubID(ByVal LName As String, ByVal FName As String,
    ByVal Sex As String, ByVal DOB As Date)
        Dim PatID As Long = GetPatientIDbyNames(LName, FName, DOB, Sex)
        If PatID <= 0 Then PatID = NextPatientID()
        Dim AddressID As Long = -1
        If txtPSubAdd1.Text <> "" And txtPSubCity.Text <> "" And txtPSubCity.Text <> "" _
        And txtPSubState.Text <> "" And txtPSubZip.Text <> "" Then _
        AddressID = GetAddressID(txtPSubAdd1.Text, txtPSubAdd2.Text,
        txtPSubCity.Text, txtPSubState.Text, txtPSubZip.Text, txtPSubCountry.Text)
        Dim cnn As New SqlConnection(connString)
        cnn.Open()
        Dim CMDp As New SqlCommand("Patients_SP", cnn)
        CMDp.CommandType = Data.CommandType.StoredProcedure
        CMDp.Parameters.AddWithValue("@act", "Upsert")
        CMDp.Parameters.AddWithValue("@ID", PatID)
        CMDp.Parameters.AddWithValue("@LastName", LName)
        CMDp.Parameters.AddWithValue("@FirstName", FName)
        CMDp.Parameters.AddWithValue("@DOB", CDate(txtPSubDOB.Text))
        CMDp.Parameters.AddWithValue("@Sex", cmbPSubSex.SelectedItem.Substring(0, 1))
        CMDp.Parameters.AddWithValue("@Tage", Trim(txtPSubTage.Text))
        CMDp.Parameters.AddWithValue("@IsAlive", 1)
        CMDp.Parameters.AddWithValue("@SSN", SSNNeat(txtPSubSSN.Text))
        CMDp.Parameters.AddWithValue("@WorkPhone", PhoneNeat(txtPSubWPhone.Text))
        CMDp.Parameters.AddWithValue("@Cell", PhoneNeat(txtPSubCell.Text))
        CMDp.Parameters.AddWithValue("@HomePhone", PhoneNeat(txtPSubHPhone.Text))
        CMDp.Parameters.AddWithValue("@Fax", PhoneNeat(txtPSubFax.Text))
        CMDp.Parameters.AddWithValue("@Email", Trim(txtPSubEmail.Text))
        If Trim(txtPSubEmployer.Text) <> "" Then _
        CMDp.Parameters.AddWithValue("@Employer_ID", GetEmployerID(txtPSubEmployer.Text))
        'If Trim(txtPassword.Text) <> "" Then _
        'CMDp.Parameters.AddWithValue("@Password", LIC.encryptString(Trim(txtPassword.Text)))
        'If cmbEthnicity.SelectedIndex <> -1 Then
        '    ItemX = cmbEthnicity.SelectedItem
        '    CMDp.Parameters.AddWithValue("@RaceID", ItemX.ItemData)
        'Else
        '    CMDp.Parameters.AddWithValue("@RaceID", 7)
        'End If
        CMDp.Parameters.AddWithValue("@Address_ID", AddressID)
        CMDp.ExecuteNonQuery()
        CMDp.Dispose()
        cnn.Close()
        cnn = Nothing
        Return PatID
    End Function

    Private Function GetEmployerID(ByVal Employer As String) As Long
        Dim EmployerID As Long = 0
        Dim cneid As New SqlConnection(connString)
        cneid.Open()
        Dim cmdeid As New SqlCommand("Select * from " &
        "Employers where Employer like '" & Employer & "%'", cneid)
        cmdeid.CommandType = Data.CommandType.Text
        Dim dreid As SqlDataReader = cmdeid.ExecuteReader
        If dreid.HasRows Then
            While dreid.Read
                EmployerID = dreid("ID")
            End While
        End If
        cneid.Close()
        cneid = Nothing
        Return EmployerID
    End Function

    Private Function NextEmployerID() As Long
        Dim NID As Long = 1
        Dim cneid As New SqlConnection(connString)
        cneid.Open()
        Dim cmdeid As New SqlCommand("Select max(ID) as LastID from Employers", cneid)
        cmdeid.CommandType = Data.CommandType.Text
        Dim dreid As SqlDataReader = cmdeid.ExecuteReader
        If dreid.HasRows Then
            While dreid.Read
                If dreid("LastID") IsNot DBNull.Value _
                Then NID = dreid("LastID") + 1
            End While
        End If
        cneid.Close()
        cneid = Nothing
        Return NID
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim proceed = True
        If txtLName.Text <> "" And txtFName.Text <> "" _
        And cmbSex.SelectedIndex <> -1 And IsDate(txtDOB.Text) = True Then
            If cmbPRelation.SelectedIndex <> 0 Then
                If String.IsNullOrEmpty(txtPSubAdd1.Text) Then
                    Dim rs = MessageBox.Show("Subscriber's Address is required, Do you want to proceed? click 'OK' to save it, or click 'Cancel' to add it", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
                    proceed = IIf(rs = DialogResult.OK, True, False)

                End If
            End If
            If proceed = False Then
                Return
            End If
            Dim PatID As Long
            If chkNewEdit.Checked = False Then  'New
                If txtPatientID.Text <> "" Then
                    PatID = Val(txtPatientID.Text)
                Else
                    PatID = NextPatientID()
                End If
            Else
                PatID = Val(txtPatientID.Text)
            End If
            SavePatient(PatID)
            If cmbPIns.SelectedIndex <> -1 And Trim(txtPPolicy.Text) <> "" _
            And ((cmbPRelation.SelectedIndex = 0) Or
            (cmbPRelation.SelectedIndex <> 0 And txtPSubLName.Text <> "" And
            txtPSubFName.Text <> "" And cmbPSubSex.SelectedIndex <> -1 And
            IsDate(txtPSubDOB.Text))) Then
                SavePrimeCoverage(PatID)
            Else
                ExecuteSqlProcedure("Delete from Coverages where Preference = 'P' and " &
                "Patient_ID = " & PatID)
            End If
            If cmbSIns.SelectedIndex <> -1 And Trim(txtSPolicy.Text) <> "" And
            ((cmbSRelation.SelectedIndex = 0) Or
            (cmbSRelation.SelectedIndex <> 0 And txtSSubLName.Text <> "" And
            txtSSubFName.Text <> "" And cmbSSubSex.SelectedIndex <> -1 And
            IsDate(txtPSubDOB.Text))) Then
                SaveSecondCoverage(PatID)
            Else
                ExecuteSqlProcedure("Delete from Coverages where Preference <> 'P' and " &
                "Patient_ID = " & PatID)
            End If
            NewVals = PatientSnapshot()
            Dim sOld As String = ""
            Dim sNew As String = ""
            If OldVals.Length = NewVals.Length Then 'Edit
                For i As Integer = 0 To UBound(NewVals)
                    If i = 0 Then
                        sOld = OldVals(i) & "|"
                        sNew = NewVals(i) & "|"
                    ElseIf OldVals(i) <> NewVals(i) Then
                        sOld += OldVals(i) & "|"
                        sNew += NewVals(i) & "|"
                    ElseIf i = UBound(NewVals) Then
                        sOld += OldVals(i)
                        sNew += NewVals(i)
                    End If
                Next
                If sOld.EndsWith("|") Then sOld = sOld.Substring(0, Len(sOld) - 1)
                If sNew.EndsWith("|") Then sNew = sNew.Substring(0, Len(sNew) - 1)
            ElseIf OldVals.Length < NewVals.Length AndAlso OldVals(0) = "" Then 'new   
                sOld = "" : sNew = Join(NewVals, "|")
            End If
            LogUserEvent(ThisUser.ID, 321, Date.Now.ToString, "Patient", PatID, sOld, sNew)
            ClearPatient()
            ClearPrimary()
            ClearSecondary()
            If chkNewEdit.Checked = False Then txtPatientID.Text = NextPatientID()
            Update_Progress()
            btnDelete.Enabled = False
        Else
            MsgBox("All required fields must have data, in order to get saved", MsgBoxStyle.Critical, "Prolis")
            If txtLName.Text = "" Then txtLName.Focus()
            If txtFName.Text = "" Then txtFName.Focus()
            If Not IsDate(txtDOB.Text) Then txtDOB.Focus()
            If cmbSex.SelectedIndex = -1 Then cmbSex.Focus()
        End If
    End Sub

    Private Sub SavePatient(ByVal PatID As Long)
        'If PatientID = "" Then PatientID = GetPatientIDbyNames(LName, FName, DOB, Sex)
        Dim ItemX As MyList
        Dim AddressID As Long = -1
        If Trim(txtAdd1.Text) <> "" And Trim(txtCity.Text) <> "" And Trim(txtCity.Text) <> "" _
        And Trim(txtState.Text) <> "" And Trim(txtZip.Text) <> "" Then _
        AddressID = GetAddressID(Trim(txtAdd1.Text), Trim(txtAdd2.Text), Trim(txtCity.Text),
        Trim(txtState.Text), Trim(txtZip.Text), Trim(txtCountry.Text))
        Dim cnn As New SqlConnection(connString)
        cnn.Open()
        Dim CMDp As New SqlCommand("Patients_SP", cnn)
        CMDp.CommandType = Data.CommandType.StoredProcedure
        CMDp.Parameters.AddWithValue("@act", "Upsert")
        CMDp.Parameters.AddWithValue("@ID", PatID)
        CMDp.Parameters.AddWithValue("@LastName", Trim(txtLName.Text))
        CMDp.Parameters.AddWithValue("@FirstName", Trim(txtFName.Text))
        CMDp.Parameters.AddWithValue("@MiddleName", Trim(txtMName.Text))
        CMDp.Parameters.AddWithValue("@DOB", CDate(txtDOB.Text))
        CMDp.Parameters.AddWithValue("@Sex", cmbSex.SelectedItem.ToString.Substring(0, 1))
        CMDp.Parameters.AddWithValue("@Tage", Trim(txtTage.Text))
        CMDp.Parameters.AddWithValue("@IsAlive", chkAlive.Checked)
        If IsDate(txtDeathDate.Text) Then _
        CMDp.Parameters.AddWithValue("@DeathDate", CDate(txtDeathDate.Text))
        CMDp.Parameters.AddWithValue("@SSN", SSNNeat(txtSSN.Text))
        CMDp.Parameters.AddWithValue("@WorkPhone", PhoneNeat(txtWPhone.Text))
        CMDp.Parameters.AddWithValue("@Cell", PhoneNeat(txtCell.Text))
        CMDp.Parameters.AddWithValue("@HomePhone", PhoneNeat(txtHPhone.Text))
        CMDp.Parameters.AddWithValue("@Fax", PhoneNeat(txtFax.Text))
        CMDp.Parameters.AddWithValue("@Email", Trim(txtEmail.Text))
        If Trim(txtPassword.Text) <> "" Then
            CMDp.Parameters.AddWithValue("@Password", LIC.encryptString(Trim(txtPassword.Text)))
        Else
            CMDp.Parameters.AddWithValue("@Password", "")

        End If
        If cmbRace.SelectedIndex <> -1 Then
            ItemX = cmbRace.SelectedItem
            CMDp.Parameters.AddWithValue("@Race_ID", ItemX.ItemData)
        Else
            CMDp.Parameters.AddWithValue("@Race_ID", 7)
        End If
        If cmbEthnicity.SelectedIndex <> -1 Then
            CMDp.Parameters.AddWithValue("@Ethnicity", cmbEthnicity.SelectedItem.ToString)
        Else
            CMDp.Parameters.AddWithValue("@Ethnicity", "Unknown")
        End If
        If SystemConfig.DiagTarget = "V" Then
            If cmbSpecies.SelectedIndex <> -1 Then
                ItemX = cmbSpecies.SelectedItem
                CMDp.Parameters.AddWithValue("@Species_ID", ItemX.ItemData)
            Else
                CMDp.Parameters.AddWithValue("@Species_ID", 0)
            End If
            If cmbBreed.SelectedIndex <> -1 Then
                ItemX = cmbBreed.SelectedItem
                CMDp.Parameters.AddWithValue("@Breed_ID", ItemX.ItemData)
            Else
                CMDp.Parameters.AddWithValue("@Breed_ID", 0)
            End If
        Else
            CMDp.Parameters.AddWithValue("@Species_ID", 0)
            CMDp.Parameters.AddWithValue("@Breed_ID", 0)
        End If
        CMDp.Parameters.AddWithValue("@Address_ID", AddressID)
        CMDp.Parameters.AddWithValue("@Note", txtNote.Text)

        If txtAlert.Text <> "" Then CMDp.Parameters.AddWithValue("@Alert", txtAlert.Rtf)
        CMDp.Parameters.AddWithValue("@Alert_CS", chkCS.Checked)
        CMDp.Parameters.AddWithValue("@Alert_Acc", chkAcc.Checked)

        CMDp.ExecuteNonQuery()
        CMDp.Dispose()
        cnn.Close()
        cnn = Nothing
    End Sub

    Private Function UpdatePatient(ByVal LName As String, ByVal _
    FName As String, ByVal Sex As String, ByVal DOB As Date) As Long
        Dim PatID As Long = GetPatientIDbyNames(LName, FName, DOB, Sex)
        If PatID <= 0 Then PatID = NextPatientID()
        SavePatient(PatID)
        Return PatID
    End Function

    Private Sub btnSSubLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSSubLook.Click
        Dim PatientID As String = frmPatLookUp.ShowDialog()
        If PatientID <> "" Then
            DisplaySSub(Val(PatientID))
        End If
    End Sub

    Private Sub btnPSubLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPSubLook.Click
        Dim PatientID As String = frmPatLookUp.ShowDialog()
        If PatientID <> "" Then
            DisplayPSub(Val(PatientID))
        End If
    End Sub

    Private Sub btnPIns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPIns.Click
        frmPayers.ShowDialog()
        PopulatePayers()
    End Sub

    Private Sub btnSIns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSIns.Click
        frmPayers.ShowDialog()
        PopulatePayers()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub txtDOB_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDOB.Validated
        Update_Progress()
    End Sub

    Private Sub txtFName_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFName.Validated
        If txtPatientID.Text = "" Then
            If txtLName.Text <> "" And txtFName.Text <> "" Then
                frmPatLookUp.Owner = Me

                frmPatLookUp.patientName = String.Concat(txtLName.Text, ", ", txtFName.Text)

                Dim PatientID As String = frmPatLookUp.ShowDialog
                If PatientID <> "" Then DisplayPatient(Val(PatientID))
                Update_Progress()
            End If
        End If
    End Sub

    Private Sub txtSSN_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSSN.Validated
        Update_Progress()
    End Sub

    Private Sub txtAdd1_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAdd1.Validated
        Update_Progress()
    End Sub

    Private Sub txtAdd2_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAdd2.Validated
        AddressValidate()
    End Sub

    Private Sub txtCity_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCity.Validated
        AddressValidate()
    End Sub

    Private Sub txtState_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtState.Validated
        AddressValidate()
    End Sub

    Private Sub txtZip_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtZip.Validated
        AddressValidate()
    End Sub

    Private Sub txtPhone_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
        Update_Progress()
    End Sub

    Private Sub txtEmail_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmail.Validated
        Update_Progress()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim RetVal As Integer
        If txtPatientID.Text <> "" And txtLName.Text <> "" And txtFName.Text <> "" And
        cmbSex.SelectedIndex <> -1 And IsDate(txtDOB.Text) = True Then
            If Not PatientUsed(Val(txtPatientID.Text)) Then
                If ThisUser.Hard_Deletion = True Then
                    RetVal = MsgBox("It is recommended to make the patient 'Inactive'" _
                    & " instead of deleting it from the system. Are you sure you want" _
                    & " to delete it from the system?", MsgBoxStyle.Question +
                    MsgBoxStyle.YesNo, "Prolis")
                    If RetVal = vbYes Then
                        OldVals = PatientSnapshot()
                        ExecuteSqlProcedure("Delete from Patients where ID = " & Val(txtPatientID.Text))
                        ExecuteSqlProcedure("Delete from Coverages where Patient_ID = " & Val(txtPatientID.Text))
                        ExecuteSqlProcedure("Delete from Patient_Dx where Patient_ID = " & Val(txtPatientID.Text))
                        Dim PatID As Long = Val(txtPatientID.Text)
                        ClearForm()
                        NewVals = PatientSnapshot()
                        Dim sOld As String = ""
                        Dim sNew As String = ""
                        If OldVals.Length = NewVals.Length Then 'Edit
                            For i As Integer = 0 To UBound(NewVals)
                                If i = 0 Then
                                    sOld = OldVals(i) & "|"
                                    sNew = NewVals(i) & "|"
                                ElseIf OldVals(i) <> NewVals(i) Then
                                    sOld += OldVals(i) & "|"
                                    sNew += NewVals(i) & "|"
                                ElseIf i = UBound(NewVals) Then
                                    sOld += OldVals(i)
                                    sNew += NewVals(i)
                                End If
                            Next
                            If sOld.EndsWith("|") Then sOld = sOld.Substring(0, Len(sOld) - 1)
                            If sNew.EndsWith("|") Then sNew = sNew.Substring(0, Len(sNew) - 1)
                        ElseIf OldVals.Length < NewVals.Length AndAlso OldVals(0) = "" Then 'new   
                            sOld = "" : sNew = Join(NewVals, "|")
                        Else    'Delete
                            sNew = "" : sOld = Join(OldVals, "|")
                        End If
                        LogUserEvent(ThisUser.ID, 321, Date.Now.ToString, "Patient", PatID, sOld, sNew)
                        Update_Progress()
                        btnDelete.Enabled = False
                    End If
                Else
                    MsgBox("Sorry " & ThisUser.Name & "! you don't have a permission to perform" _
                    & " a delete action. You need to consult your supervisor for such permissions" _
                    , MsgBoxStyle.Critical, "Prolis")
                End If
            Else
                MsgBox("Sorry " & ThisUser.Name & "! the patient you are to delete, has " _
                & " testing records and Prolis will not allow you to perform this action" _
                , MsgBoxStyle.Critical, "Prolis")
            End If
        Else
            MsgBox("Display the record first and try again.", MsgBoxStyle.Critical, "Prolis")
        End If
    End Sub

    Private Function PatientUsed(ByVal PatID As Long) As Boolean
        Dim Used As Boolean = False
        Dim cnpu As New SqlConnection(connString)
        cnpu.Open()
        Dim cmdpu As New SqlCommand("Select * " &
        "from Requisitions where Patient_ID = " & PatID, cnpu)
        cmdpu.CommandType = Data.CommandType.Text
        Dim drpu As SqlDataReader = cmdpu.ExecuteReader
        If drpu.HasRows Then Used = True
        cnpu.Close()
        cnpu = Nothing
        Return Used
    End Function

    Private Sub chkAlive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAlive.CheckedChanged
        If chkAlive.Checked = True Then
            chkAlive.Text = "Yes"
            txtDeathDate.Text = ""
            txtDeathDate.Enabled = False
        Else
            chkAlive.Text = "No"
            txtDeathDate.Enabled = True
        End If
    End Sub

    Private Sub txtPatientID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPatientID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtPatientID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatientID.Validated
        If txtPatientID.Text <> "" Then
            If chkNewEdit.Checked = True Then  'Edit
                If IsPatientIDUsed(Val(txtPatientID.Text)) Then
                    DisplayPatient(Val(txtPatientID.Text))
                Else
                    MsgBox("Invalid Patient ID", MsgBoxStyle.Critical, "Prolis")
                    txtPatientID.Text = ""
                    txtPatientID.Focus()
                End If
            End If
        Else
            ClearForm()
        End If
    End Sub

    Private Function IsPatientIDUsed(ByVal PatID As Long) As Boolean
        Dim Used As Boolean = False
        Dim cnpu As New SqlConnection(connString)
        cnpu.Open()
        Dim cmdpu As New SqlCommand("Select " &
        "* from Patients where ID = " & PatID, cnpu)
        cmdpu.CommandType = Data.CommandType.Text
        Dim drpu As SqlDataReader = cmdpu.ExecuteReader
        If drpu.HasRows Then Used = True
        cnpu.Close()
        cnpu = Nothing
        Return Used
    End Function

    Private Sub btnDelPrime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelPrime.Click
        ClearPrimary()
    End Sub

    Private Sub btnDelSecond_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelSecond.Click
        ClearSecondary()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim RetVal As Integer = MsgBox("This action will display/print all of the patients in " &
        "Prolis system and is a time consuming process depending upon the patient records in the " &
        "system. During this operation the screen will be greyed out. Do you want to proceed?",
        MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Prolis")
        If RetVal = vbYes Then
            lblStatus.Text = ""
            My.Application.DoEvents()
            Dim stopWatch As New Stopwatch()
            stopWatch.Start()
            '

            'TODO: Crystal Report Code
            '==============================
            'Dim oRPT As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            'oRPT.Load(GetReportPath("Prolis Patients.RPT"))
            'ApplyNewServer(oRPT, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)
            '==============================
            'Dim FolderPath As String = My.Application.Info.DirectoryPath & "\Temp\"
            'If Not Directory.Exists(FolderPath) Then
            '    Directory.CreateDirectory(FolderPath)
            '    Dim UserAccount As String = "everyone" 'Specify the user here
            '    Dim FolderInfo As IO.DirectoryInfo = New DirectoryInfo(FolderPath)
            '    Dim FolderAcl As New DirectorySecurity
            '    FolderAcl.AddAccessRule(New FileSystemAccessRule(UserAccount, _
            '    FileSystemRights.Modify, InheritanceFlags.ContainerInherit Or _
            '    InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow))
            '    FolderAcl.SetAccessRuleProtection(True, False) 'uncomment to remove existing permissions
            '    FolderInfo.SetAccessControl(FolderAcl)
            'End If
            Try
                Me.Enabled = False
                'Dim ms As MemoryStream = oRPT.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat)
                'Dim FPDF As Byte() = ms.ToArray

                'If PDFRV_foxit.IsHandleCreated Then PDFRV_foxit.Close()
                'PDFRV_foxit.PdfViewer1.Refresh()
                'PDFRV_foxit.PdfViewer1.Open(New Foxit.PDF.Viewer.PdfDocument(FPDF)
                '   )
                'TODO: Crystal Report Code
                '==============================
                'Dim frmRptView As frmRV = New frmRV
                'frmRptView.CRRV.ReportSource = oRPT
                'frmRptView.MdiParent = frmDashboard
                'frmRptView.Show()

                '==============================
                ''If File.Exists(FolderPath & "Prolis_Patients.PDF") Then File.Delete(FolderPath & "Prolis_Patients.PDF")
                ''Dim FS As New FileStream(FolderPath & "Prolis_Patients.PDF", FileMode.Create, FileAccess.ReadWrite, FileShare.Read)
                ''FS.Write(FPDF, 0, FPDF.Length)
                ''FS.Close()
                ''FS = Nothing
                ''If PDFRV.IsHandleCreated Then PDFRV.Close()
                ''PDFRV.AxAcroPDF1.src = FolderPath & "Prolis_Patients.PDF"
                ''PDFRV.AxAcroPDF1.Refresh()
                ''PDFRV.Show()
                'PDFRV_foxit.FilePath = FolderPath & "Prolis_Patients.PDF"

                'PDFRV_foxit.Show()
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                Me.Enabled = True
            End Try
            '
            stopWatch.Stop()
            lblStatus.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            stopWatch.Elapsed.Hours, stopWatch.Elapsed.Minutes,
            stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds / 10)
            My.Application.DoEvents()
        End If
    End Sub

    Private Function ImportPatients(ByVal FileName As String) As Long
        Dim sSQL As String = ""
        Dim AddressID As String = ""
        Dim PatientID As String = ""
        Dim Delim As String = Chr(9)
        Dim n As Long = 0
        Dim Per As Integer = 0
        Dim lineCount As Long = File.ReadAllLines(FileName).Length
        Dim SR As New StreamReader(FileName)
        Dim Ln As String ' Going to hold one line at a time
        Do Until SR.EndOfStream Or n = lineCount - 1
            Ln = SR.ReadLine
            If Trim(Ln) <> "" Then
                BW.ReportProgress((n + 1) / 100 * lineCount) ', ((n + 1) / 100 * lineCount).ToString & " %"
                If Ln.Contains(Chr(9)) Then
                    Delim = Chr(9)
                ElseIf Ln.Contains(",") Then
                    Delim = ","
                ElseIf Ln.Contains("|") Then
                    Delim = "|"
                End If
                '
                If Delim = "," Then
                    If InStr(Ln, Chr(34)) > 0 Then
                        Dim p1 As Integer
                        Dim p2 As Integer
                        Dim oTmp As String = ""
                        Dim nTmp As String = ""
                        Do Until InStr(Ln, Chr(34)) = 0
                            p1 = InStr(Ln, Chr(34))
                            p2 = InStr(p1 + 1, Ln, Chr(34))
                            oTmp = Ln.Substring(p1 - 1, p2 - p1 + 1)
                            nTmp = Replace(oTmp, ",", "|")
                            nTmp = Replace(nTmp, Chr(34), "")
                            Ln = Replace(Ln, oTmp, nTmp)
                        Loop
                    End If
                End If
                AddressID = ""
                '0=LAST NAME, 1=FIRST NAME, 2=MIDDLE NAME, 3=DOB, 4=GENDER, 5=ADDRESS1, 6=ADDRESS2, 7=CITY, 8=STATE, 9=ZIP,
                '10=COUNTRY, 11=PRIM  INS ID, 12=PRIM GROUP, 13=PRIM POLICY, 14=PRIM RELATION, 15=SECOND INS ID, 16=SECOND GROUP,
                '17=SECOND POLICY, 18=SECOND RELATION, 19=REF PROVIDER ID, 20=CHART
                Dim Fields() As String = Split(Ln, Delim)  'tab or comma or pipe
                If Delim = "," Then
                    For c As Integer = 0 To Fields.Length - 1
                        Fields(c) = Replace(Fields(c), "|", "")
                        Fields(c) = Trim(Fields(c))
                    Next
                End If
                '
                If Fields.Length > 10 AndAlso (Trim(Fields(5)) <> "" And Trim(Fields(7)) <> "" And
                Trim(Fields(8)) <> "" And Trim(Fields(9)) <> "" And Trim(Fields(9)) <> "Zip") Then
                    AddressID = GetAddressID(Trim(Fields(5)), Trim(Fields(6)),
                    Trim(Fields(7)), Trim(Fields(8)), Trim(Fields(9)), Trim(Fields(10)))
                Else
                    AddressID = ""
                End If

                If (Fields(0) <> "" And Fields(1) <> "" And IsDate(Fields(3)) _
                And Fields(4) <> "") AndAlso Not (Fields(4).Contains("Gender")) Then
                    PatientID = GetPatientID(Trim(Fields(0)), Trim(Fields(1)), CDate(Fields(3)), Trim(Fields(4)))
                    If PatientID = "" Then PatientID = NextPatientID.ToString
                    '
                    sSQL = "If Not Exists (Select * from Patients where ID = " & PatientID &
                    ") Insert into Patients (ID, LastName, FirstName, MiddleName, DOB, Sex, " &
                    "Address_ID) values (" & PatientID & ", '" & Trim(Fields(0)) & "', '" &
                    Trim(Fields(1)) & "', '" & Trim(Fields(2)) & "', '" & CDate(Fields(3)) &
                    "', '" & Trim(Fields(4)) & "', " & IIf(AddressID = "", "Null", AddressID) & ")"
                    ExecuteSqlProcedure(sSQL)
                    '
                    If Fields.Length > 20 AndAlso (Trim(Fields(19)) <> "" And Trim(Fields(20)) <> "") Then    'Association
                        sSQL = "If Not Exists (Select * from Client_Patient where Provider_ID = " &
                        Trim(Fields(19)) & " and Patient_ID = " & PatientID & ") Insert into " &
                        "Client_Patient (Provider_ID, Patient_ID, EMRNo, Room) values (" & Trim(Fields(19)) _
                        & ", " & PatientID & ", '" & Trim(Fields(20)) & "', '')"
                        ExecuteSqlProcedure(sSQL)
                    End If
                    '
                    If Fields.Length > 14 AndAlso (Trim(Fields(11)) <> "" And Trim(Fields(13)) <> "") Then    'Prime coverage
                        sSQL = "If Not Exists (Select * from Coverages where Insurance_ID = " &
                        Trim(Fields(11)) & " and Patient_ID = " & PatientID & ") Insert into " &
                        "Coverages (Insurance_ID, Patient_ID, Ordinal, Insured_ID, Preference, " &
                        "GroupNo, PolicyNo, Relation) values (" & Trim(Fields(11)) & ", " &
                        PatientID & ", 0, " & PatientID & ", 'P', '" & Trim(Fields(12)) & "', '" &
                        Trim(Fields(13)) & "', " & Val(Fields(14)) & ")"
                        ExecuteSqlProcedure(sSQL)
                    End If
                    '
                    If Fields.Length > 18 AndAlso (Trim(Fields(15)) <> "" And Trim(Fields(17)) <> "") Then    'Second coverage
                        sSQL = "If Not Exists (Select * from Coverages where Insurance_ID = " &
                        Trim(Fields(15)) & " and Patient_ID = " & PatientID & ") Insert into " &
                        "Coverages (Insurance_ID, Patient_ID, Ordinal, Insured_ID, Preference, " &
                        "GroupNo, PolicyNo, Relation) values (" & Trim(Fields(15)) & ", " &
                        PatientID & ", 0, " & PatientID & ", 'S', '" & Trim(Fields(16)) & "', '" &
                        Trim(Fields(17)) & "', " & Val(Fields(18)) & ")"
                        ExecuteSqlProcedure(sSQL)
                    End If
                    n += 1
                    PatientID = ""
                End If
            End If
        Loop
        SR.Close()
        SR = Nothing
        Return n
    End Function

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        frmImportPatients.ShowDialog()
        'Dim FileName As String = ""
        'OpenFileDialog1.Filter = "csv files (*.csv)|*.csv|Text files (*.txt)|*.txt|All files (*.*)|*.*"
        'If Windows.Forms.DialogResult.OK = OpenFileDialog1.ShowDialog Then FileName = OpenFileDialog1.FileName
        'OpenFileDialog1.Dispose()
        'If FileName <> "" Then
        '    PB.Maximum = 100
        '    PB.Maximum = 1
        '    'AnimationShow()
        '    BW.RunWorkerAsync(FileName)
        'End If
    End Sub

    Private Function GetPatientID(ByVal LName As String, ByVal FName As String, ByVal DOB As Date, ByVal Sex As String) As String
        Dim PatID As String = ""

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT ID FROM Patients WHERE LastName = @LastName AND FirstName = @FirstName AND Sex = @Sex AND DOB = @DOB"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@LastName", LName)
                command.Parameters.AddWithValue("@FirstName", FName)
                command.Parameters.AddWithValue("@Sex", Sex.Substring(0, 1))
                command.Parameters.AddWithValue("@DOB", DOB)

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        PatID = reader("ID").ToString()
                    End If
                End Using
            End Using
        End Using

        Return PatID
    End Function

    Private Sub cmbPSubSex_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPSubSex.SelectedIndexChanged
        If cmbPSubSex.SelectedItem.ToString.StartsWith("G") Or
        cmbPSubSex.SelectedItem.ToString.StartsWith("N") Then
            txtPSubTage.ReadOnly = False
        Else
            txtPSubTage.Text = ""
            txtPSubTage.ReadOnly = True
        End If
    End Sub

    Private Sub cmbSSubSex_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSSubSex.SelectedIndexChanged
        If cmbSSubSex.SelectedItem.ToString.StartsWith("G") Or
        cmbSSubSex.SelectedItem.ToString.StartsWith("N") Then
            txtSSubTage.ReadOnly = False
        Else
            txtSSubTage.Text = ""
            txtSSubTage.ReadOnly = True
        End If
    End Sub

    Private Sub cmbSex_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSex.SelectedIndexChanged
        If cmbSex.SelectedIndex <> -1 Then
            If cmbSex.SelectedItem.ToString.StartsWith("G") Or
            cmbSex.SelectedItem.ToString.StartsWith("N") Then
                txtTage.ReadOnly = False
            Else
                txtTage.Text = ""
                txtTage.ReadOnly = True
            End If
        End If
    End Sub

    Private Sub BW_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BW.DoWork
        ImportPatients(e.Argument)
    End Sub

    Private Sub BW_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BW.ProgressChanged
        'PB.Value = e.ProgressPercentage
        lblStatus.Text = e.UserState
    End Sub

    Private Sub BW_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BW.RunWorkerCompleted
        'AnimationHide()
        MsgBox("A total of " & e.Result & " record(s) imported.")
    End Sub

    Private Sub cmbSpecies_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSpecies.SelectedIndexChanged
        If cmbSpecies.SelectedIndex <> -1 Then
            Dim ItemX As MyList = cmbSpecies.SelectedItem
            PopulateBreeds(ItemX.ItemData)
        Else
            PopulateBreeds(0)
        End If
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Dim openFileDialog As New OpenFileDialog
        openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|PDF Files|*.pdf"


        If openFileDialog.ShowDialog() = DialogResult.OK Then
            Dim filePath As String = openFileDialog.FileName
            selectedFileType = Path.GetExtension(filePath)
            selectedFile = File.ReadAllBytes(filePath)
            Dim title As String = txtTitle.Text
            If String.IsNullOrEmpty(title) Then
                title = Path.GetFileName(filePath)
            End If
            If selectedFile IsNot Nothing AndAlso Not String.IsNullOrEmpty(title) Then
                Using conn As New SqlConnection(connString)
                    Dim query As String = "INSERT INTO Patient_Documents (Title,Patient_ID, FileData, FileType) VALUES (@Title,@PatientID, @FileData, @FileType)"
                    Using cmd As New SqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@Title", title)
                        cmd.Parameters.AddWithValue("@PatientID", txtPatientID.Text)
                        cmd.Parameters.AddWithValue("@FileData", selectedFile)
                        cmd.Parameters.AddWithValue("@FileType", selectedFileType)
                        conn.Open()
                        cmd.ExecuteNonQuery()

                    End Using
                End Using
                LoadFiles()
            Else
                MessageBox.Show("Please select a file and enter a title.")
            End If
        End If
    End Sub


    Private Sub LoadFiles()
        dgvFiles.Rows.Clear()

        Using conn As New SqlConnection(connString)
            Dim query As String = "SELECT FileID, Patient_ID,Title FROM Patient_Documents where Patient_ID=" & txtPatientID.Text & ""
            Using cmd As New SqlCommand(query, conn)
                conn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                Dim rowCounter As Integer = 0
                While reader.Read()
                    ' Add to DataGridView
                    Dim btn As New DataGridViewButtonColumn()
                    dgvFiles.Rows.Add(reader("FileID").ToString(), reader("Title").ToString(), "View")


                End While
            End Using
        End Using
    End Sub


    Private Sub dgvFiles_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFiles.CellContentClick
        If e.ColumnIndex = 2 Then
            Dim id = dgvFiles.Rows(e.RowIndex).Cells(0).Value
            Dim fileID As Integer = CType(id, Integer)

            Using conn As New SqlConnection(connString)
                Dim query As String = "SELECT FileData, FileType FROM Patient_Documents WHERE FileID = @FileID"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@FileID", fileID)
                    conn.Open()
                    Dim reader As SqlDataReader = cmd.ExecuteReader()

                    If reader.Read() Then
                        Dim fileData As Byte() = CType(reader("FileData"), Byte())
                        Dim fileType As String = reader("FileType").ToString()

                        ' Display file (open with default app)
                        Dim tempFilePath As String = Path.GetTempFileName() & fileType
                        Dim f As New frmWebView()
                        f.MdiParent = frmDashboard
                        f.LoadPdfData(fileData)
                        f.Show()

                    End If
                End Using
            End Using
        ElseIf e.ColumnIndex = 3 Then
            Dim id = dgvFiles.Rows(e.RowIndex).Cells(0).Value
            Dim fileID As Integer = CType(id, Integer)
            Using conn As New SqlConnection(connString)
                Dim query As String = "delete FROM Patient_Documents WHERE FileID = @FileID"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@FileID", fileID)
                    conn.Open()
                    cmd.ExecuteReader()
                End Using
            End Using
            dgvFiles.Rows.RemoveAt(e.RowIndex)
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged

    End Sub

    Private Sub TabControl1_TabIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.TabIndexChanged

    End Sub

    Private Sub endmail_Click(sender As Object, e As EventArgs) Handles endmail.Click

        If IsValidEmail(txtEmail.Text) Then
            Try
                Dim otp = Guid.NewGuid().ToString()
                Dim pid = txtPatientID.Text
                Dim sqlQuery As String

                sqlQuery = "IF EXISTS (SELECT 1 FROM patients WHERE   id  = " & pid & " AND (password IS NULL OR password = ''))" &
                            "BEGIN " &
                            "" &
                            " " &
                            "insert into patient_portal_emails values(" & pid & ",'" & txtEmail.Text & "',1,'" & otp & "');" &
                            "update patients set password='" & otp & "' , Email='" & txtEmail.Text & "'  where id  = " & pid & " AND (password IS NULL OR password = '');" &
                            " " &
                             "END " &
                            "  " &
                            "  " &
                            "    " &
                            " "
                ExecuteSqlProcedure(sqlQuery)

                DisplayPatient(pid)
                MsgBox("An email to the patient has been sent.", MsgBoxStyle.Information, "Success")

            Catch ex As Exception

            End Try
        Else
            MsgBox("Patient's Email is not valid.", MsgBoxStyle.Critical, "An error has occurred")
        End If


    End Sub

    Public Shared Function IsValidEmail(email As String) As Boolean
        ' Regular expression for validating email
        Dim emailRegex As String = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
        Dim regex As New Regex(emailRegex)

        ' Return true if email matches the pattern, false otherwise
        Return regex.IsMatch(email)
    End Function

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

    Private Sub cmdFont_Click(sender As Object, e As EventArgs) Handles cmdFont.Click
        Dim MyFont As New FontDialog
        MyFont.Font = txtAlert.SelectionFont
        Dim RetVal As Integer = MyFont.ShowDialog
        If RetVal = System.Windows.Forms.DialogResult.OK Then
            txtAlert.SelectionFont = MyFont.Font
        End If
        MyFont = Nothing
    End Sub
End Class
