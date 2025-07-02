Option Compare Binary
Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmUserMgmt
    Private UserID As Long = -1
    Private IsNew As Boolean
    Private SearchMode As Boolean = True
    Private IsDirty As Boolean

    Private Sub chkCS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCS.CheckedChanged
        If chkCS.Checked = False Then
            chkCS.Text = "No"
        Else
            chkCS.Text = "Yes"
        End If
        btnSave.Enabled = True
    End Sub

    Private Sub chkAccession_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAccession.CheckedChanged
        If chkAccession.Checked = False Then
            chkAccession.Text = "No"
        Else
            chkAccession.Text = "Yes"
        End If
        btnSave.Enabled = True
    End Sub

    Private Sub chkRE_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRE.CheckedChanged
        If chkRE.Checked = False Then
            chkRE.Text = "No"
        Else
            chkRE.Text = "Yes"
        End If
        btnSave.Enabled = True
    End Sub

    Private Sub chkRR_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRR.CheckedChanged
        If chkRR.Checked = False Then
            chkRR.Text = "No"
        Else
            chkRR.Text = "Yes"
        End If
        btnSave.Enabled = True
    End Sub

    Private Sub chkQCL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkQCL.CheckedChanged
        If chkQCL.Checked = False Then
            chkQCL.Text = "No"
        Else
            chkQCL.Text = "Yes"
        End If
        btnSave.Enabled = True
    End Sub

    Private Sub chkTestMgmt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTestMgmt.CheckedChanged
        If chkTestMgmt.Checked = False Then
            chkTestMgmt.Text = "No"
        Else
            chkTestMgmt.Text = "Yes"
        End If
        btnSave.Enabled = True
    End Sub

    Private Sub chkBilling_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBilling.CheckedChanged
        If chkBilling.Checked = False Then
            chkBilling.Text = "No"
        Else
            chkBilling.Text = "Yes"
        End If
        btnSave.Enabled = True
    End Sub

    Private Sub chkReport_Build_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkReport_Build.CheckedChanged
        If chkReport_Build.Checked = False Then
            chkReport_Build.Text = "No"
        Else
            chkReport_Build.Text = "Yes"
        End If
        btnSave.Enabled = True
    End Sub

    Private Sub chkReport_Process_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkReport_Process.CheckedChanged
        If chkReport_Process.Checked = False Then
            chkReport_Process.Text = "No"
            gbReports.Enabled = False
        Else
            chkReport_Process.Text = "Yes"
            gbReports.Enabled = True
        End If
        btnSave.Enabled = True
    End Sub

    Private Sub chkHD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkHD.CheckedChanged
        If chkHD.Checked = False Then
            chkHD.Text = "No"
        Else
            chkHD.Text = "Yes"
        End If
        btnSave.Enabled = True
    End Sub

    Private Sub chkSD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSD.CheckedChanged
        If chkSD.Checked = False Then
            chkSD.Text = "No"
        Else
            chkSD.Text = "Yes"
        End If
        btnSave.Enabled = True
    End Sub

    Private Sub chkSysConfig_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSysConfig.CheckedChanged
        If chkSysConfig.Checked = False Then
            chkSysConfig.Text = "No"
        Else
            chkSysConfig.Text = "Yes"
        End If
        btnSave.Enabled = True
    End Sub

    Private Function IsUserNameOk(ByVal UserName As String) As Boolean
        Dim IsGood As Boolean = True
        If Len(Trim(UserName)) < 3 Then IsGood = False
        Dim cnun As New SqlConnection(connString)
        cnun.Open()
        Dim cmdun As New SqlCommand("Select * from Users where UserName = '" & Trim(UserName) & "'", cnun)
        cmdun.CommandType = CommandType.Text
        Dim drun As SqlDataReader = cmdun.ExecuteReader
        If drun.HasRows Then
            If IsNew = True Then IsGood = False
        End If
        cnun.Close()
        cnun = Nothing
        IsUserNameOk = IsGood
    End Function

    Private Sub chkUM_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUM.CheckedChanged
        If chkUM.Checked = False Then
            chkUM.Text = "No"
        Else
            chkUM.Text = "Yes"
        End If
        btnSave.Enabled = True
    End Sub

    Private Sub chkDictionary_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDictionary.CheckedChanged
        If chkDictionary.Checked = False Then
            chkDictionary.Text = "No"
        Else
            chkDictionary.Text = "Yes"
        End If
        btnSave.Enabled = True
    End Sub

    Private Sub chkDicOnFly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDicOnFly.CheckedChanged
        If chkDicOnFly.Checked = False Then
            chkDicOnFly.Text = "No"
        Else
            chkDicOnFly.Text = "Yes"
        End If
        btnSave.Enabled = True
    End Sub

    Private Sub chkARC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkARC.CheckedChanged
        If chkARC.Checked = False Then
            chkARC.Text = "No"
        Else
            chkARC.Text = "Yes"
        End If
        btnSave.Enabled = True
    End Sub

    Private Sub frmUserMgmt_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If IsDirty Then MsgBox("You are closing the Activity Screen before the changes " _
        & "to the user record because of your most recent action, " _
        & "could be saved.")
    End Sub

    Private Sub Fill_Managers()
        cmbManager.Items.Clear()
        Dim cnu As New SqlConnection(connString)
        cnu.Open()
        Dim cmdu As New SqlCommand("Select * from " &
        "Users where User_Type_ID in (1,2,3,4,7,10,11,12)", cnu)
        cmdu.CommandType = CommandType.Text
        Dim dru As SqlDataReader = cmdu.ExecuteReader
        If dru.HasRows Then
            While dru.Read
                cmbManager.Items.Add(New MyList(dru("UserName") _
                & " [" & dru("FullName") & "]", dru("ID")))
            End While
        End If
        cnu.Close()
        cnu = Nothing
    End Sub

    Private Sub frmUserMgmt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error Resume Next
        If LIC Is Nothing Then LIC = New LicenseManager.ProlisLicense(connString, My.Application.Info.AssemblyName)
        If LIC IsNot Nothing Then ApplyLicense()
        Fill_User_Types()
        Fill_Managers()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
        If ThisUser.UserName.ToLower.Contains("techteam") Then
            Button1.Show()
        Else
            Button1.Hide()
        End If
    End Sub

    Private Sub ApplyLicense()
        If LIC.CS = True Then
            chkCS.Enabled = True
        Else
            chkCS.Checked = False
            chkCS.Enabled = False
        End If
        '
        If LIC.Bill = True Then
            chkBilling.Enabled = True
            chkInsurances.Enabled = True
            chkARC.Enabled = True
            chkPayment.Enabled = True
        Else
            chkBilling.Checked = False
            chkInsurances.Checked = False
            chkARC.Checked = False
            chkPayment.Checked = False
            chkBilling.Enabled = False
            chkInsurances.Enabled = False
            chkARC.Enabled = False
            chkPayment.Enabled = False
        End If
        '
        If LIC.MyRpts = True Then
            Fill_All_Reports()
        Else
            btnATR.Enabled = False
            btnSTR.Enabled = False
            btnSTL.Enabled = False
            btnATL.Enabled = False
        End If
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub Fill_All_Reports()
        Dim di As New IO.DirectoryInfo(Application.StartupPath & "\MyReports")
        Dim RptFi As IO.FileInfo() = di.GetFiles("*.rpt")
        Dim fi As IO.FileInfo
        Dim i As Integer = 1
        Dim FL As String = ""
        Dim FrdName As String = ""
        For Each fi In RptFi
            FL = fi.Name
            FrdName = Replace(Microsoft.VisualBasic.Mid(FL, 1, InStr(FL, ".") - 1), "_", " ")
            lstRptAll.Items.Add(New MyList(fi.Name, i))
            i += 1
        Next
        btnATR.Enabled = True
    End Sub

    Private Sub Fill_User_Types()
        cmbUserType.Items.Clear()
        Dim cnu As New SqlConnection(connString)
        cnu.Open()
        Dim cmdu As New SqlCommand("Select * from User_Types where ID <> 0", cnu)
        cmdu.CommandType = CommandType.Text
        Dim dru As SqlDataReader = cmdu.ExecuteReader
        If dru.HasRows Then
            While dru.Read
                cmbUserType.Items.Add(New MyList(dru("Name"), dru("ID")))
            End While
        End If
        cnu.Close()
        cnu = Nothing
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub chkEditNew_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEditNew.CheckedChanged
        ClearForm()
        btnDelete.Enabled = False
        btnSave.Enabled = False
        If chkEditNew.Checked = False Then
            chkEditNew.Text = "&Edit"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath &
            "\Images\Edit.ico")
            btnUserLookup.Enabled = True
        Else
            chkEditNew.Text = "&New"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath &
            "\Images\New.ico")
            btnUserLookup.Enabled = False
            txtUserID.Text = NextUserID()
        End If
    End Sub

    Private Function NextUserID() As Long
        Dim UID As Long = 1
        Dim cnu As New SqlConnection(connString)
        cnu.Open()
        Dim cmdu As New SqlCommand("Select max(ID) as TopID from Users", cnu)
        cmdu.CommandType = CommandType.Text
        Dim dru As SqlDataReader = cmdu.ExecuteReader
        If dru.HasRows Then
            While dru.Read
                UID = dru("TopID") + 1
            End While
        End If
        cnu.Close()
        cnu = Nothing
        EnableDataEdits()
        Return UID
    End Function

    Private Sub ClearForm()
        ApplyLicense()
        txtUserID.Text = ""
        txtUserName.Text = ""
        txtName.Text = ""
        txtLogout.Text = "0"
        ChkActive.Checked = True
        cmbUserType.SelectedIndex = -1
        cmbManager.SelectedIndex = -1
        txtEmail.Text = ""
        chkAccession.Checked = False
        chktest.Checked = False
        chkRE.Checked = False
        chkRR.Checked = False
        chkQCL.Checked = False
        chkTestMgmt.Checked = False
        chkReport_Build.Checked = False
        chkReport_Process.Checked = False
        chkHD.Checked = False
        chkSD.Checked = False
        chkSysConfig.Checked = False
        chkUM.Checked = False
        chkDictionary.Checked = False
        chkDicOnFly.Checked = False
        chkEquips.Checked = False
        chkInterfaces.Checked = False
        chkPour.Checked = False
        chktest.Checked = False
        chkSup.Checked = False
        chkDir.Checked = False
        chkCytoTech.Checked = False
        chkPath.Checked = False
        chkResearch.Checked = False
        txtPWD.Text = ""
        txtConfirmPWD.Text = ""
        txtUserName.BackColor = Color.White
        txtName.BackColor = Color.White
        txtPWD.BackColor = Color.White
        txtConfirmPWD.BackColor = Color.White
        lstRptSelected.Items.Clear()
        btnSave.Enabled = False
        btnDelete.Enabled = False
        SearchMode = True
        DisableDataEdits()
    End Sub

    Private Sub EnableDataEdits()
        txtName.ReadOnly = False
        txtEmail.ReadOnly = False
        txtUserName.ReadOnly = False
        txtPWD.ReadOnly = False
        txtConfirmPWD.ReadOnly = False
    End Sub

    Private Sub DisableDataEdits()
        txtName.ReadOnly = True
        txtEmail.ReadOnly = True
        txtUserName.ReadOnly = True
        txtPWD.ReadOnly = True
        txtConfirmPWD.ReadOnly = True
    End Sub

    Private Sub txtUserName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserName.LostFocus
        If txtUserName.Text <> "" Then
            If IsUserNameOk(txtUserName.Text) = False Then
                MsgBox("The UserName you are specifying is either less than 3 characters long or it " _
                & "is not Unique. Please type a unique User Name between 3 and 20 characters long.")
                txtUserName.Text = ""
                txtUserName.Focus()
            End If
        End If
    End Sub

    Private Sub txtUserName_ModifiedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserName.ModifiedChanged
        btnSave.Enabled = True
    End Sub

    Friend Sub OpenTheRecord(ByVal UserID As Long)
        Dim Itemx As MyList
        Dim cnu As New SqlConnection(connString)
        cnu.Open()
        Dim cmdu As New SqlCommand("Select * from Users where ID = " & UserID, cnu)
        cmdu.CommandType = CommandType.Text
        Dim dru As SqlDataReader = cmdu.ExecuteReader
        If dru.HasRows Then
            While dru.Read
                txtUserID.Text = dru("ID")
                txtUserName.Text = dru("UserName")
                txtName.Text = dru("FullName")
                txtLogout.Text = dru("Logoutmins")
                ChkActive.Checked = dru("IsActive")
                If dru("Email") IsNot DBNull.Value Then txtEmail.Text = dru("Email")
                If dru("Manager_ID") = -1 Then
                    cmbManager.SelectedIndex = -1
                Else
                    For i As Integer = 0 To cmbManager.Items.Count - 1
                        Itemx = cmbManager.Items.Item(i)
                        If dru("Manager_ID") = Itemx.ItemData Then
                            cmbManager.SelectedItem = Itemx
                            Exit For
                        End If
                    Next
                End If
                For i As Integer = 0 To cmbUserType.Items.Count - 1
                    Itemx = cmbUserType.Items.Item(i)
                    If Itemx.ItemData = dru("User_Type_ID") Then
                        cmbUserType.SelectedIndex = i
                        Exit For
                    End If
                Next
                If LIC.CS = True Then
                    chkCS.Enabled = True
                    chkCS.Checked = dru("Cus_Svc")
                Else
                    chkCS.Checked = False
                    chkCS.Enabled = False
                End If
                chkAccession.Checked = dru("Accession")
                chkRE.Checked = dru("Result_Entry")
                chkRR.Checked = dru("Result_Release")
                chkQCL.Checked = dru("QC_Layout")
                chkTestMgmt.Checked = dru("Test_Mgmt")
                If LIC.Bill = True Then
                    chkBilling.Enabled = True
                    chkBilling.Checked = dru("Billing")
                    chkARC.Enabled = True
                    chkARC.Checked = dru("ARC")
                    chkPayment.Enabled = True
                    chkPayment.Checked = dru("Payment")
                    chkInsurances.Enabled = True
                    chkInsurances.Checked = dru("Insurances")
                Else
                    chkBilling.Checked = False
                    chkBilling.Enabled = False
                    chkARC.Checked = False
                    chkARC.Enabled = False
                    chkPayment.Checked = False
                    chkPayment.Enabled = False
                    chkInsurances.Checked = False
                    chkInsurances.Enabled = False
                End If
                chkReport_Build.Checked = dru("Report_Build")
                chkReport_Process.Checked = dru("Report_Process")
                chkHD.Checked = dru("Hard_Deletion")
                chkSD.Checked = dru("Soft_Deletion")
                chkSysConfig.Checked = dru("System_Config")
                chkUM.Checked = dru("User_Mgmt")
                chkDictionary.Checked = dru("Dictionary")
                chkDicOnFly.Checked = dru("DicOnFly")
                chkEquips.Checked = dru("Equips")
                chkInterfaces.Checked = dru("Interfaces")
                chkPour.Checked = dru("Pouring")
                chktest.Checked = dru("Testing")
                chkSup.Checked = dru("Supervisor")
                chkDir.Checked = dru("Director")
                chkCytoTech.Checked = dru("CytoTech")
                chkPath.Checked = dru("Pathologist")
                chkResearch.Checked = dru("Research")
                txtPWD.Text = decryptString(dru("Password"))
                'txtPWD.Text = DecryptIt(Rs.Fields("ID").Value, Rs.Fields("Password").Value)
                'txtPWD.Text = Rs.Fields("Password").Value
                txtConfirmPWD.Text = txtPWD.Text
                '
                If LIC.MyRpts = True Then
                    Dim cn1 As New SqlConnection(connString)
                    cn1.Open()
                    Dim cmd1 As New SqlCommand("Select * from Report_User where User_ID = " & UserID, cn1)
                    cmd1.CommandType = CommandType.Text
                    Dim dr1 As SqlDataReader = cmd1.ExecuteReader
                    If dr1.HasRows Then
                        While dr1.Read
                            For i As Integer = 0 To lstRptAll.Items.Count - 1
                                Itemx = lstRptAll.Items.Item(i)
                                If dr1("Report_File") = Itemx.Name Then
                                    lstRptSelected.Items.Add(Itemx)
                                    btnATL.Enabled = True
                                    Exit For
                                End If
                            Next
                        End While
                    End If
                    cn1.Close()
                    cn1 = Nothing
                Else
                    btnATL.Enabled = False
                    btnSTL.Enabled = False
                    btnATR.Enabled = False
                    btnSTR.Enabled = False
                End If
                '
                EnableDataEdits()
            End While
            btnDelete.Enabled = True
            SearchMode = False
        End If
        cnu.Close()
        cnu = Nothing
    End Sub

    Private Sub cmbUserType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUserType.SelectedIndexChanged
        If cmbUserType.SelectedIndex <> -1 Then
            Dim ItemX As MyList = cmbUserType.SelectedItem
            NegateAllPermissions()
            Dim cnu As New SqlConnection(connString)
            cnu.Open()
            Dim cmdu As New SqlCommand("Select * from User_Types where ID = " & ItemX.ItemData, cnu)
            cmdu.CommandType = CommandType.Text
            Dim dru As SqlDataReader = cmdu.ExecuteReader
            If dru.HasRows Then
                While dru.Read
                    chkCS.Checked = dru("Cus_Svc")
                    chkAccession.Checked = dru("Accession")
                    chkRE.Checked = dru("Result_Entry")
                    chkRR.Checked = dru("Result_Release")
                    chkQCL.Checked = dru("QC_Layout")
                    chkTestMgmt.Checked = dru("Test_Mgmt")
                    chkBilling.Checked = dru("Billing")
                    chkReport_Build.Checked = dru("Report_Build")
                    chkReport_Process.Checked = dru("Report_Process")
                    chkHD.Checked = dru("Hard_Deletion")
                    chkSD.Checked = dru("Soft_Deletion")
                    chkSysConfig.Checked = dru("System_Config")
                    chkUM.Checked = dru("User_Mgmt")
                    chkDictionary.Checked = dru("Dictionary")
                    chkDicOnFly.Checked = dru("DicOnFly")
                    chkARC.Checked = dru("ARC")
                    chkPayment.Checked = dru("Payment")
                    chkEquips.Checked = dru("Equips")
                    chkInterfaces.Checked = dru("Interfaces")
                    chkInsurances.Checked = dru("Insurances")
                    chkPour.Checked = dru("Pouring")
                    chktest.Checked = dru("Testing")
                    chkSup.Checked = dru("Supervisor")
                    chkDir.Checked = dru("Director")
                End While
            End If
            cnu.Close()
            cnu = Nothing
            btnSave.Enabled = True
        End If
    End Sub

    Private Sub NegateAllPermissions()
        chkCS.Checked = False
        chkAccession.Checked = False
        chkRE.Checked = False
        chkRR.Checked = False
        chkQCL.Checked = False
        chkTestMgmt.Checked = False
        chkBilling.Checked = False
        chkReport_Build.Checked = False
        chkReport_Process.Checked = False
        chkHD.Checked = False
        chkSD.Checked = False
        chkSysConfig.Checked = False
        chkUM.Checked = False
        chkDictionary.Checked = False
        chkDicOnFly.Checked = False
        chkARC.Checked = False
        chkPayment.Checked = False
        chkEquips.Checked = False
        chkInterfaces.Checked = False
        chkInsurances.Checked = False
        chkPour.Checked = False
        chktest.Checked = False
        chkSup.Checked = False
        chkDir.Checked = False
    End Sub

    Private Sub SaveTheRecord(ByVal UserID As Long, ByVal UserName As String, ByVal Name As String, ByVal PWD As String)
        Dim Itemx As MyList
        Dim ManagerID As Long = -1
        Dim UTypeID As Integer = -1
        If cmbManager.SelectedIndex <> -1 Then
            Itemx = cmbManager.SelectedItem
            ManagerID = Itemx.ItemData
        End If
        If cmbUserType.SelectedIndex <> -1 Then
            Itemx = cmbUserType.SelectedItem
            UTypeID = Itemx.ItemData
        End If
        Dim sSQL As String = "If Exists (Select * from Users where ID = " & UserID & ") Update Users set " &
        "UserName = '" & Trim(UserName) & "', FullName = '" & Trim(Name) & "', Email = '" & Trim(txtEmail.Text) _
        & "', Manager_ID = " & ManagerID & ", Password = '" & encryptString(Trim(PWD)) & "', Change_PWD = " &
        IIf(ThisUser.ID <> UserID, 1, 0) & ", IsLoggedIn = " & IIf(ThisUser.ID <> UserID, 0, 1) & ", " &
        "User_Type_ID = " & UTypeID & ", Logoutmins = " & Val(txtLogout.Text) & ", IsActive = " &
        Convert.ToInt16(ChkActive.Checked) & ", Cus_Svc = " & Convert.ToInt16(chkCS.Checked) & ", Accession = " &
        Convert.ToInt16(chkAccession.Checked) & ", " & "Result_Entry = " & Convert.ToInt16(chkRE.Checked) & ", " &
        "Result_Release = " & Convert.ToInt16(chkRR.Checked) & ", QC_Layout = " & Convert.ToInt16(chkQCL.Checked) &
        ", Test_Mgmt = " & Convert.ToInt16(chkTestMgmt.Checked) & ", Billing = " & Convert.ToInt16(chkBilling.Checked) &
        ", " & "Report_Build = " & Convert.ToInt16(chkReport_Build.Checked) & ", Report_Process = " &
        Convert.ToInt16(chkReport_Process.Checked) & ", Hard_Deletion = " & Convert.ToInt16(chkHD.Checked) & ", " &
        "Soft_Deletion = " & Convert.ToInt16(chkSD.Checked) & ", System_Config = " & Convert.ToInt16(chkSysConfig.Checked) &
        ", User_Mgmt = " & Convert.ToInt16(chkUM.Checked) & ", Dictionary = " & Convert.ToInt16(chkDictionary.Checked) &
        ", DicOnFly = " & Convert.ToInt16(chkDicOnFly.Checked) & ", ARC = " & Convert.ToInt16(chkARC.Checked) & ",  Payment = " &
        Convert.ToInt16(chkPayment.Checked) & ",  Equips = " & Convert.ToInt16(chkEquips.Checked) & ", Interfaces = " &
        Convert.ToInt16(chkInterfaces.Checked) & ", Insurances = " & Convert.ToInt16(chkInsurances.Checked) & ", Pouring = " &
        Convert.ToInt16(chkPour.Checked) & ", Testing = " & Convert.ToInt16(chktest.Checked) & ", Supervisor = " &
        Convert.ToInt16(chkSup.Checked) & ", Director = " & Convert.ToInt16(chkDir.Checked) & ", " & "CytoTech = " &
        Convert.ToInt16(chkCytoTech.Checked) & ", Pathologist = " & Convert.ToInt16(chkPath.Checked) & ", Research = " &
        Convert.ToInt16(chkResearch.Checked) & ", LastEditedOn = '" & Date.Today & "', EditedBy = " & ThisUser.ID & " where " &
        "ID = " & UserID & " Else Insert into Users (ID, UserName, FullName, Email, Manager_ID, Password, " &
        "Change_PWD, IsLoggedIn, User_Type_ID, Logoutmins, IsActive, Cus_Svc, Accession, Result_Entry, " &
        "Result_Release, QC_Layout, Test_Mgmt, Billing, Report_Build, Report_Process, Hard_Deletion, " &
        "Soft_Deletion, System_Config, User_Mgmt, Dictionary, DicOnFly, ARC, Payment, Equips, Interfaces, " &
        "Insurances, Pouring, Testing, Supervisor, Director, CytoTech, Pathologist, Research, LastEditedOn, " &
        "EditedBy) values (" & UserID & ", '" & Trim(UserName) & "', '" & Trim(Name) & "', '" &
        Trim(txtEmail.Text) & "', " & ManagerID & ", '" & encryptString(Trim(PWD)) & "', " & IIf(ThisUser.ID <>
        UserID, 1, 0) & ", " & IIf(ThisUser.ID <> UserID, 0, 1) & ", " & UTypeID & ", " & Val(txtLogout.Text) &
        ", " & Convert.ToInt16(ChkActive.Checked) & ", " & Convert.ToInt16(chkCS.Checked) & ", " & Convert.ToInt16(
        chkAccession.Checked) & ", " & Convert.ToInt16(chkRE.Checked) & ", " & Convert.ToInt16(chkRR.Checked) & ", " &
        Convert.ToInt16(chkQCL.Checked) & ", " & Convert.ToInt16(chkTestMgmt.Checked) & ", " & Convert.ToInt16(chkBilling.Checked) &
        ", " & Convert.ToInt16(chkReport_Build.Checked) & ", " & Convert.ToInt16(chkReport_Process.Checked) & ", " &
        Convert.ToInt16(chkHD.Checked) & ", " & Convert.ToInt16(chkSD.Checked) & ", " & Convert.ToInt16(chkSysConfig.Checked) & ", " &
        Convert.ToInt16(chkUM.Checked) & ", " & Convert.ToInt16(chkDictionary.Checked) & ", " & Convert.ToInt16(chkDicOnFly.Checked) &
        ", " & Convert.ToInt16(chkARC.Checked) & ", " & Convert.ToInt16(chkPayment.Checked) & ", " & Convert.ToInt16(chkEquips.Checked) &
        ", " & Convert.ToInt16(chkInterfaces.Checked) & ", " & Convert.ToInt16(chkInsurances.Checked) & ", " & Convert.ToInt16(chkPour.Checked) _
        & ", " & Convert.ToInt16(chktest.Checked) & ", " & Convert.ToInt16(chkSup.Checked) & ", " & Convert.ToInt16(chkDir.Checked) & ", " &
        Convert.ToInt16(chkCytoTech.Checked) & ", " & Convert.ToInt16(chkPath.Checked) & ", " & Convert.ToInt16(chkResearch.Checked) & ", '" &
        Date.Today & "', " & ThisUser.ID & ")"
        ExecuteSqlProcedure(sSQL)
        '
        If lstRptSelected.Items.Count > 0 Then
            ExecuteSqlProcedure("Delete from Report_User where User_ID = " & UserID)
            For i As Integer = 0 To lstRptSelected.Items.Count - 1
                ExecuteSqlProcedure("If Not Exists (Select * from Report_User where " &
                "Report_File = '" & lstRptSelected.Items(i).ToString & "'and User_ID = " &
                UserID & ") Insert into Report_User (Report_File, User_ID) " &
                "values ('" & lstRptSelected.Items(i).ToString & "', " & UserID & ")")
            Next
        Else
            ExecuteSqlProcedure("Delete from Report_User where User_ID = " & UserID)
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtUserID.Text <> "" And txtUserName.Text <> "" And txtName.Text <> "" And IsPWDValid(txtPWD.Text) Then
            If txtPWD.Text = txtConfirmPWD.Text Then
                SaveTheRecord(Val(txtUserID.Text), txtUserName.Text, txtName.Text, txtPWD.Text)
                Fill_Managers()
                Me.Tag = txtUserID.Text & "|" & txtUserName.Text & "|" & txtPWD.Text
                ClearForm()
                IsNew = False
                If chkEditNew.Checked = True Then txtUserID.Text = NextUserID()
            Else
                MsgBox("You must type the same password in the confirmation box.")
            End If
        Else
            MsgBox("You have not provided all the required information to save a " &
            "user's record. Make sure the Password is between 8 and 20 characters long " &
            "and does contain one capital letter (A thru Z), at least one lower case " &
            "letter (a thru z), at least one number (0 thru 9) at least one of the " &
            "following character" & vbCrLf & "'!', '@', '#', '$', '%', '^', '&', " &
            "'*', '(', ')'", MsgBoxStyle.Critical, "Prolis")
        End If
    End Sub

    Private Sub lstRptAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstRptAll.Click
        If lstRptAll.SelectedIndex <> -1 Then
            btnSTR.Enabled = True
        Else
            btnSTR.Enabled = False
        End If
    End Sub

    Private Sub lstRptAll_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstRptAll.SelectedIndexChanged
        If lstRptAll.SelectedIndex <> -1 Then
            btnSTR.Enabled = True
        Else
            btnSTR.Enabled = False
        End If
    End Sub

    Private Sub lstRptSelected_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstRptSelected.Click
        If lstRptSelected.SelectedIndex <> -1 Then
            btnSTL.Enabled = True
        Else
            btnSTL.Enabled = False
        End If
    End Sub


    Private Sub lstRptSelected_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstRptSelected.SelectedIndexChanged
        If lstRptSelected.SelectedIndex <> -1 Then
            btnSTL.Enabled = True
        Else
            btnSTL.Enabled = False
        End If
    End Sub

    Private Sub btnSTR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSTR.Click
        Dim i As Integer
        Dim IsThere As Boolean = False
        If lstRptSelected.Items.Count > 0 Then
            For i = 0 To lstRptSelected.Items.Count - 1
                If lstRptAll.SelectedItem Is lstRptSelected.Items.Item(i) Then IsThere = True
            Next
            If IsThere = False Then
                lstRptSelected.Items.Add(lstRptAll.SelectedItem)
                lstRptAll.SelectedIndex = -1
                btnSTR.Enabled = False
                btnATL.Enabled = True
            End If
        Else
            lstRptSelected.Items.Add(lstRptAll.SelectedItem)
            lstRptAll.SelectedIndex = -1
            btnSTR.Enabled = False
            btnATL.Enabled = True
        End If

    End Sub

    Private Sub btnATR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnATR.Click
        Dim i As Integer
        lstRptSelected.Items.Clear()
        For i = 0 To lstRptAll.Items.Count - 1
            lstRptSelected.Items.Add(lstRptAll.Items.Item(i))
        Next
        If lstRptSelected.Items.Count > 0 Then btnATL.Enabled = True
    End Sub

    Private Sub btnATL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnATL.Click
        lstRptSelected.Items.Clear()
        btnSTL.Enabled = False
        btnATL.Enabled = False
    End Sub

    Private Sub btnSTL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSTL.Click
        lstRptSelected.Items.Remove(lstRptSelected.SelectedItem)
        lstRptSelected.SelectedIndex = -1
        btnSTL.Enabled = False
        If lstRptSelected.Items.Count = 0 Then btnATL.Enabled = False
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If UserID <> -1 Then
            If UserID <> ThisUser.ID Then
                If ThisUser.Hard_Deletion = True Then
                    Dim RetVal As Integer
                    RetVal = MsgBox("Though you may delete this user record from the system, " &
                    "Prolis designer strongly recommends not to delete any user record as this " _
                    & "action may end up with an unstability of the data." & vbCrLf &
                    "Are you sure to delete this user record from the system?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo)
                    If RetVal = vbYes Then
                        ExecuteSqlProcedure("Delete from Users where ID = " & UserID)
                        ClearForm()
                    End If
                Else
                    MsgBox("You are not allowed to delete any user record from the system")
                End If
            Else
                MsgBox("System is designed not to allow you to delete your own record. ")
            End If
        Else
            MsgBox("you need to select a valid user first and then delete it.")
        End If
    End Sub

    Private Sub ChkActive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkActive.CheckedChanged
        If ChkActive.Checked = True Then
            ChkActive.Text = "Yes"
        Else
            ChkActive.Text = "No"
        End If
    End Sub

    Private Sub chkPour_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPour.CheckedChanged
        If chkPour.Checked = True Then
            chkPour.Text = "Yes"
        Else
            chkPour.Text = "No"
        End If
    End Sub

    Private Sub chktest_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chktest.CheckedChanged
        If chktest.Checked = True Then
            chktest.Text = "Yes"
        Else
            chktest.Text = "No"
        End If
    End Sub

    Private Sub chkInterfaces_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkInterfaces.CheckedChanged
        If chkInterfaces.Checked = True Then
            chkInterfaces.Text = "Yes"
        Else
            chkInterfaces.Text = "No"
        End If
    End Sub

    Private Sub chkEquips_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEquips.CheckedChanged
        If chkEquips.Checked = True Then
            chkEquips.Text = "Yes"
        Else
            chkEquips.Text = "No"
        End If
    End Sub

    Private Sub chkInsurances_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkInsurances.CheckedChanged
        If chkInsurances.Checked = True Then
            chkInsurances.Text = "Yes"
        Else
            chkInsurances.Text = "No"
        End If
    End Sub

    Private Sub chkPayment_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPayment.CheckedChanged
        If chkPayment.Checked = True Then
            chkPayment.Text = "Yes"
        Else
            chkPayment.Text = "No"
        End If
    End Sub

    Private Sub txtUserID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUserID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub btnUserLookup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUserLookup.Click
        Dim UserID As String = frmUserLook.ShowDialog
        If UserID <> "" Then
            OpenTheRecord(Val(UserID))
            Me.Tag = txtUserID.Text & "|" & txtUserName.Text & "|" & txtPWD.Text
        End If
    End Sub

    Private Sub txtUserID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserID.Validated
        If txtUserID.Text <> "" Then
            If txtUserID.Text <> "0" Then
                If chkEditNew.Checked = False Then  'Edit mode
                    If IsUserIDValid(Val(txtUserID.Text)) = True Then
                        OpenTheRecord(Val(txtUserID.Text))
                    Else
                        MsgBox("Invalid User ID", MsgBoxStyle.Critical, "Prolis")
                        txtUserID.Text = ""
                        txtUserID.Focus()
                    End If
                Else        'Add mode
                    If IsUserIDValid(Val(txtUserID.Text)) Then
                        Dim RetVal As Integer
                        RetVal = MsgBox("The user id you typed, has been used already. Either provide an " _
                        & "unused ID or simply accept the system assigned ID. Click 'Yes' if " _
                        & "you want to accept the system assigned ID or click 'No' if you " _
                        & "want to type in an unused ID your self. Do you want Prolis to " _
                        & "assign the ID?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
                        If RetVal = vbYes Then
                            txtUserID.Text = NextUserID()
                        Else
                            txtUserID.Text = ""
                            txtUserID.Focus()
                        End If
                    End If
                End If
            Else
                ClearForm()
            End If
        Else
            ClearForm()
        End If
    End Sub

    Private Function IsUserIDValid(ByVal UserID As Long) As Boolean
        Dim Valid As Boolean = False
        Dim cnu As New SqlConnection(connString)
        cnu.Open()
        Dim cmdu As New SqlCommand("Select * from Users where ID = " & UserID, cnu)
        cmdu.CommandType = CommandType.Text
        Dim dru As SqlDataReader = cmdu.ExecuteReader
        If dru.HasRows Then Valid = True
        cnu.Close()
        cnu = Nothing
        Return Valid
    End Function

    Private Sub txtPWD_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPWD.Validated
        If txtPWD.Text <> "" Then
            If Not IsPWDValid(txtPWD.Text) Then
                MsgBox("The password invalid. A valid one needs to be between 8 and 20 " & _
                "characters long, must have atleast one Capital letter (A thru Z), at " & _
                "least one lower case letter (a thru z), at least one number (0 thru 9) " & _
                "and at least one special characher like '!', '@', '#', '$', '%', '^', " & _
                "'&', '*', '(', or ')'", MsgBoxStyle.Critical, "Prolis")
                txtPWD.Focus()
            End If
        End If
    End Sub

    Private Sub txtConfirmPWD_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtConfirmPWD.Validated
        If txtConfirmPWD.Text <> "" Then
            If Trim(txtConfirmPWD.Text) <> Trim(txtPWD.Text) Then
                MsgBox("You need to type exactly the same password in the Confirm " & _
                "box what you typed in the password box")
                txtConfirmPWD.Text = ""
                txtConfirmPWD.Focus()
            End If
        End If
    End Sub

    Private Sub txtLogout_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLogout.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtLogout_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLogout.Validated
        If txtLogout.Text = "" Then txtLogout.Text = "0"
    End Sub

    Private Sub chkSup_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSup.CheckedChanged
        If chkSup.Checked = False Then
            chkSup.Text = "No"
        Else
            chkSup.Text = "Yes"
            chkRE.Checked = True
            chkRR.Checked = True
        End If
        btnSave.Enabled = True
    End Sub

    Private Sub chkDir_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDir.CheckedChanged
        If chkDir.Checked = False Then
            chkDir.Text = "No"
        Else
            chkDir.Text = "Yes"
            chkRE.Checked = True
            chkRR.Checked = True
        End If
        btnSave.Enabled = True
    End Sub

    Private Sub chkCytoTech_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCytoTech.CheckedChanged
        If chkCytoTech.Checked = False Then
            chkCytoTech.Text = "No"
        Else
            chkCytoTech.Text = "Yes"
            chkRE.Checked = True
            chkRR.Checked = True
        End If
        btnSave.Enabled = True
    End Sub

    Private Sub chkPath_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPath.CheckedChanged
        If chkPath.Checked = False Then
            chkPath.Text = "No"
        Else
            chkPath.Text = "Yes"
            chkRE.Checked = True
            chkRR.Checked = True
        End If
        btnSave.Enabled = True
    End Sub

    Private Sub chkResearch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkResearch.CheckedChanged
        If chkResearch.Checked = False Then
            chkResearch.Text = "No"
        Else
            chkResearch.Text = "Yes"
            chkRE.Checked = True
            chkRR.Checked = True
        End If
        btnSave.Enabled = True
    End Sub

    Private Sub Label23_Click(sender As Object, e As EventArgs) Handles Label23.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ThisUser.UserName.ToLower.Contains("techteam") Then
            Clipboard.SetText(txtPWD.Text)
        End If
    End Sub
End Class