Option Compare Text
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmCompany
    Private ORLic As LicenseManager.ProlisLicense
    Private MyLic As String

    Private Sub frmCompany_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SaveMe()
        'If ORLic.AppRun = True Then
        '    SaveMyTheme()
        'End If
        'ORLic = Nothing
        InitializeConfiguration(MyLab.ID)
    End Sub

    Private Sub SaveMyTheme()
        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "
            SELECT * FROM UserTheme WHERE Company_ID = @CompanyID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@CompanyID", MyLab.ID)

                Using adapter As New SqlDataAdapter(command)
                    Using builder As New SqlCommandBuilder(adapter)
                        Dim dataTable As New DataTable()
                        adapter.Fill(dataTable)

                        Dim row As DataRow
                        If dataTable.Rows.Count = 0 Then
                            row = dataTable.NewRow()
                            row("Company_ID") = 1
                            dataTable.Rows.Add(row)
                        Else
                            row = dataTable.Rows(0)
                        End If

                        row("Banner") = If(String.IsNullOrEmpty(txtBanner.Text), DBNull.Value, txtBanner.Text)
                        row("BackgroundColor") = If(String.IsNullOrEmpty(txtBGColor.Text), DBNull.Value, txtBGColor.Text)
                        row("BackgroundImg") = If(String.IsNullOrEmpty(txtBGImage.Text), DBNull.Value, txtBGImage.Text)
                        row("MenuBar") = If(String.IsNullOrEmpty(txtMenuColor.Text), DBNull.Value, txtMenuColor.Text)
                        row("HomeImg") = If(String.IsNullOrEmpty(txtHomeImage.Text), DBNull.Value, txtHomeImage.Text)
                        row("ProvLoginImg") = If(String.IsNullOrEmpty(txtProvImage.Text), DBNull.Value, txtProvImage.Text)
                        row("SalesLogin") = If(String.IsNullOrEmpty(txtEmpImage.Text), DBNull.Value, txtEmpImage.Text)
                        row("FrmColor") = If(String.IsNullOrEmpty(txtFormColor.Text), DBNull.Value, txtFormColor.Text)
                        row("FrmImage") = If(String.IsNullOrEmpty(txtFormImage.Text), DBNull.Value, txtFormImage.Text)
                        If Not String.IsNullOrEmpty(txtIntroText.Text) Then
                            row("IntroText") = Rtf_To_Html(txtIntroText.Rtf)
                        End If

                        adapter.Update(dataTable)
                    End Using
                End Using
            End Using
        End Using
    End Sub
    Private Sub SaveMe()
        Dim DirName As String = ""
        Dim DirPhone As String = ""
        Dim DirEmail As String = ""
        Dim DirAddID As Long = -1
        If dgvDirectors.RowCount > 0 Then
            For i As Integer = 0 To dgvDirectors.RowCount - 1
                If dgvDirectors.Rows(i).Cells(7).Value = True And _
                dgvDirectors.Rows(i).Cells(1).Value <> "" And _
                dgvDirectors.Rows(i).Cells(2).Value <> "" And _
                dgvDirectors.Rows(i).Cells(4).Value <> "" Then
                    DirName = dgvDirectors.Rows(i).Cells(1).Value & ", " & dgvDirectors.Rows(i).Cells(2).Value & _
                    IIf(dgvDirectors.Rows(i).Cells(3).Value Is Nothing, " " & dgvDirectors.Rows(i).Cells(3).Value, "")
                    If dgvDirectors.Rows(i).Cells(5).Value IsNot DBNull.Value AndAlso _
                    Trim(dgvDirectors.Rows(i).Cells(5).Value) <> "" Then DirPhone = PhoneNeat(dgvDirectors.Rows(i).Cells(5).Value)
                    If dgvDirectors.Rows(i).Cells(6).Value IsNot DBNull.Value _
                    AndAlso dgvDirectors.Rows(i).Cells(6).Value <> "" Then _
                    DirEmail = Trim(dgvDirectors.Rows(i).Cells(6).Value)
                    If dgvDirectors.Rows(i).Cells(10).Value <> "" Then
                        Dim ADDRS() As String = Split(dgvDirectors.Rows(i).Cells(10).Value, "|")
                        DirAddID = GetAddressID(ADDRS(0), ADDRS(1), ADDRS(2), ADDRS(3), ADDRS(4), ADDRS(5))
                    End If
                    Exit For
                End If
            Next
        End If
        '
        Dim cnme As New SqlConnection(connString)
        cnme.Open()
        Dim cmdme As New SqlCommand("Company_SP", cnme)
        cmdme.CommandType = CommandType.StoredProcedure
        '(@ID tinyint = NULL, @FacilityType_ID tinyint = NULL, @LastName_BSN nvarchar(60) = NULL ,@FirstName nvarchar(35) = NULL ,@MiddleName nvarchar(35) = NULL ,
        '@Degree nvarchar(25) = NULL ,@IsIndividual bit = NULL, @Abbr nchar(10) = NULL ,@Description nvarchar(4000) = NULL ,@Adv_Line nvarchar(125) = NULL ,@Logo image = NULL, 
        '@SSN nchar(9) = NULL ,@Biller nvarchar(60) = NULL ,@Address_ID bigint = NULL, @Location_Code nvarchar(20) = NULL ,@LICENSE nvarchar(25) = NULL ,@Licensee nvarchar(25) = NULL ,
        '@UPIN nvarchar(25) = NULL ,@CLIA nvarchar(25) = NULL ,@CAP nvarchar(25) = NULL ,@NPI nvarchar(25) = NULL ,@PARTNO nvarchar(250) = NULL ,@Medicare nvarchar(25) = NULL ,
        '@Medicaid nvarchar(25) = NULL ,@BCBS nvarchar(25) = NULL ,@InterfaceDLL nvarchar(50) = NULL ,@System_Params nvarchar(1) = NULL ,@Active bit = NULL, 
        '@Phone nvarchar(25) = NULL ,@Fax nvarchar(25) = NULL ,@Email nvarchar(50) = NULL ,@SMTPClient nvarchar(50) = NULL ,@Password nvarchar(20) = NULL ,
        cmdme.Parameters.AddWithValue("@Command", "Upsert")
        cmdme.Parameters.AddWithValue("@ID", txtID.Text)
        cmdme.Parameters.AddWithValue("@FacilityType_ID", 1)
        cmdme.Parameters.AddWithValue("@LastName_BSN", Trim(txtLName_BSN.Text))
        cmdme.Parameters.AddWithValue("@FirstName", Trim(txtFName.Text))
        cmdme.Parameters.AddWithValue("@MiddleName", Trim(txtMName.Text))
        cmdme.Parameters.AddWithValue("@Degree", Trim(txtDegree.Text))
        cmdme.Parameters.AddWithValue("@IsIndividual", chkBusInd.Checked)
        cmdme.Parameters.AddWithValue("@Abbr", "")
        cmdme.Parameters.AddWithValue("@Description", Trim(txtDescription.Text))
        cmdme.Parameters.AddWithValue("@Adv_Line", Trim(txtAd.Text))
        cmdme.Parameters.AddWithValue("@Logo", Img_To_Array(pctLogo.Image, pctLogo.Image.RawFormat))
        cmdme.Parameters.AddWithValue("@SSN", SSNNeat(txtSSN.Text))
        cmdme.Parameters.AddWithValue("@Biller", Trim(txtBiller.Text))
        If Trim(txtAdd1.Text) <> "" And Trim(txtCity.Text) <> "" _
        And Trim(txtState.Text) <> "" And Trim(txtZip.Text) <> "" Then
            cmdme.Parameters.AddWithValue("@Address_ID", GetAddressID(Trim(txtAdd1.Text), Trim(txtAdd2.Text),
            Trim(txtCity.Text), Trim(txtState.Text), Trim(txtZip.Text), Trim(txtCountry.Text)))
        Else
            cmdme.Parameters.AddWithValue("@Address_ID", -1)
        End If
        cmdme.Parameters.AddWithValue("@Location_Code", Trim(txtLOC.Text))
        'cmdme.Parameters.AddWithValue("@ProlisServer", My.Settings.ProlisServer)
        cmdme.Parameters.AddWithValue("@LICENSE", Trim(txtLicense.Text))
        cmdme.Parameters.AddWithValue("@Licensee", "")
        cmdme.Parameters.AddWithValue("@UPIN", Trim(txtUPIN.Text))
        cmdme.Parameters.AddWithValue("@CLIA", Trim(txtCLIA.Text))
        cmdme.Parameters.AddWithValue("@CAP", Trim(txtCAP.Text))
        cmdme.Parameters.AddWithValue("@NPI", Trim(txtNPI.Text))
        cmdme.Parameters.AddWithValue("@PARTNO", Trim(txtPartNo.Text))
        cmdme.Parameters.AddWithValue("@Medicare", Trim(txtMCR.Text))
        cmdme.Parameters.AddWithValue("@Medicaid", Trim(txtMCD.Text))
        cmdme.Parameters.AddWithValue("@BCBS", Trim(txtBCBS.Text))
        cmdme.Parameters.AddWithValue("@InterfaceDLL", Trim(txtCommDLL.Text))
        cmdme.Parameters.AddWithValue("@System_Params", txtSystemParams.Text)
        cmdme.Parameters.AddWithValue("@Active", chkActive.Checked)
        cmdme.Parameters.AddWithValue("@Phone", PhoneNeat(txtPhone.Text))
        cmdme.Parameters.AddWithValue("@Fax", PhoneNeat(txtFax.Text))
        cmdme.Parameters.AddWithValue("@Email", Trim(txtEMail.Text))
        cmdme.Parameters.AddWithValue("@SMTPClient", Trim(txtSMTP.Text))
        cmdme.Parameters.AddWithValue("@Password", Trim(txtPassword.Text))
        cmdme.Parameters.AddWithValue("@Website", Trim(txtWebsite.Text))
        '@Website nvarchar(50) = NULL ,@MonLunchStart nchar(8) = NULL ,@MonLunchStop nchar(8) = NULL ,@MonStart nchar(8) = NULL ,@MonStop nchar(8) = NULL ,@TueStart nchar(8) = NULL ,
        '@TueLunchStart nchar(8) = NULL ,@TueLunchStop nchar(8) = NULL ,@TueStop nchar(8) = NULL ,@WedStart nchar(8) = NULL ,@WedLunchStart nchar(8) = NULL ,
        '@WedLunchStop nchar(8) = NULL ,@WedStop nchar(8) = NULL ,@ThuStart nchar(8) = NULL ,@ThuLunchStart nchar(8) = NULL ,@ThuLunchStop nchar(8) = NULL ,@ThuStop nchar(8) = NULL,
        '@FriStart nchar(8) = NULL ,@FriLunchStart nchar(8) = NULL ,@FriLunchStop nchar(8) = NULL ,@FriStop nchar(8) = NULL ,@SatStart nchar(8) = NULL ,@SatLunchStart nchar(8) = NULL,
        '@SatLunchStop nchar(8) = NULL ,@SatStop nchar(8) = NULL ,@SunStart nchar(8) = NULL ,@SunLunchStart nchar(8) = NULL ,@SunLunchStop nchar(8) = NULL ,@SunStop nchar(8) = NULL,
        '@DirectorName nvarchar(60) = NULL ,@DirectorPhone nvarchar(25) = NULL ,@DirectorEmail nvarchar(50) = NULL ,@DirectorAddress_ID bigint = NULL, 
        '@Disclaimer nvarchar(4000) = NULL, @LastEditedOn smalldatetime = NULL, @EditedBy bigint = NULL  , @Command nvarchar(10)
        cmdme.Parameters.AddWithValue("@MonLunchStart", dgvHours.Rows(0).Cells(2).Value)
        cmdme.Parameters.AddWithValue("@MonLunchStop", dgvHours.Rows(0).Cells(3).Value)
        cmdme.Parameters.AddWithValue("@MonStart", dgvHours.Rows(0).Cells(1).Value)
        cmdme.Parameters.AddWithValue("@MonStop", dgvHours.Rows(0).Cells(4).Value)
        cmdme.Parameters.AddWithValue("@TueStart", dgvHours.Rows(1).Cells(1).Value)
        cmdme.Parameters.AddWithValue("@TueLunchStart", dgvHours.Rows(1).Cells(2).Value)
        cmdme.Parameters.AddWithValue("@TueLunchStop", dgvHours.Rows(1).Cells(3).Value)
        cmdme.Parameters.AddWithValue("@TueStop", dgvHours.Rows(1).Cells(4).Value)
        cmdme.Parameters.AddWithValue("@WedStart", dgvHours.Rows(2).Cells(1).Value)
        cmdme.Parameters.AddWithValue("@WedLunchStart", dgvHours.Rows(2).Cells(2).Value)
        cmdme.Parameters.AddWithValue("@WedLunchStop", dgvHours.Rows(2).Cells(3).Value)
        cmdme.Parameters.AddWithValue("@WedStop", dgvHours.Rows(2).Cells(4).Value)
        cmdme.Parameters.AddWithValue("@ThuStart", dgvHours.Rows(3).Cells(1).Value)
        cmdme.Parameters.AddWithValue("@ThuLunchStart", dgvHours.Rows(3).Cells(2).Value)
        cmdme.Parameters.AddWithValue("@ThuLunchStop", dgvHours.Rows(3).Cells(3).Value)
        cmdme.Parameters.AddWithValue("@ThuStop", dgvHours.Rows(3).Cells(4).Value)
        cmdme.Parameters.AddWithValue("@FriStart", dgvHours.Rows(4).Cells(1).Value)
        cmdme.Parameters.AddWithValue("@FriLunchStart", dgvHours.Rows(4).Cells(2).Value)
        cmdme.Parameters.AddWithValue("@FriLunchStop", dgvHours.Rows(4).Cells(3).Value)
        cmdme.Parameters.AddWithValue("@FriStop", dgvHours.Rows(4).Cells(4).Value)
        cmdme.Parameters.AddWithValue("@SatStart", dgvHours.Rows(5).Cells(1).Value)
        cmdme.Parameters.AddWithValue("@SatLunchStart", dgvHours.Rows(5).Cells(2).Value)
        cmdme.Parameters.AddWithValue("@SatLunchStop", dgvHours.Rows(5).Cells(3).Value)
        cmdme.Parameters.AddWithValue("@SatStop", dgvHours.Rows(5).Cells(4).Value)
        cmdme.Parameters.AddWithValue("@SunStart", dgvHours.Rows(6).Cells(1).Value)
        cmdme.Parameters.AddWithValue("@SunLunchStart", dgvHours.Rows(6).Cells(2).Value)
        cmdme.Parameters.AddWithValue("@SunLunchStop", dgvHours.Rows(6).Cells(3).Value)
        cmdme.Parameters.AddWithValue("@SunStop", dgvHours.Rows(6).Cells(4).Value)
        cmdme.Parameters.AddWithValue("@DirectorName", DirName)
        cmdme.Parameters.AddWithValue("@DirectorPhone", DirPhone)
        cmdme.Parameters.AddWithValue("@DirectorEmail", DirEmail)
        cmdme.Parameters.AddWithValue("@DirectorAddress_ID", DirAddID)
        If Trim(txtDisclaimer.Text) <> "" Then
            cmdme.Parameters.AddWithValue("@Disclaimer", txtDisclaimer.Rtf)
        Else
            cmdme.Parameters.AddWithValue("@Disclaimer", DBNull.Value)
        End If
        cmdme.Parameters.AddWithValue("@LastEditedOn", Date.Now)
        cmdme.Parameters.AddWithValue("@EditedBy", ThisUser.ID)
        Try
            cmdme.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Your facility record did not get saved because of the error '" & ex.Message & "'")
        Finally
            cnme.Close()
            cnme = Nothing
        End Try
        '
        If txtCompanyID.Text <> "" And txtMerchantID.Text <> "" And
        txtProcesserKey.Text <> "" And txtProcesserName.Text <> "" Then
            ExecuteSqlProcedure("If Exists (Select * from Merchant_Setting where Company_ID = " &
            txtID.Text & ") Update Merchant_Setting Set Merchant_ID = '" & Trim(txtMerchantID.Text) &
            "', ProcesserKey = '" & Trim(txtProcesserKey.Text) & "', ProcesserName = '" &
            Trim(txtProcesserName.Text) & "', LastEditedOn = '" & Date.Now & "', EditedBy = " &
            ThisUser.ID & " where Company_ID = " & txtID.Text & " Else Insert into " &
            "Merchant_Setting (Company_ID, Merchant_ID, ProcesserKey, ProcesserName, SetupOn, " &
            "LastEditedOn, EditedBy) values (" & txtID.Text & ", '" & Trim(txtMerchantID.Text) &
            "', '" & Trim(txtProcesserKey.Text) & "', '" & Trim(txtProcesserName.Text) & "', '" &
            Date.Now & "', '" & Date.Now & "', " & ThisUser.ID & ")")
        End If
        '
        Using connection As New SqlConnection(connString)
            connection.Open()

            If dgvDirectors.RowCount > 0 Then
                Dim GoodDirIDs As String = ""

                For i As Integer = 0 To dgvDirectors.RowCount - 1
                    If Not String.IsNullOrEmpty(dgvDirectors.Rows(i).Cells(1).Value?.ToString()) AndAlso
               Not String.IsNullOrEmpty(dgvDirectors.Rows(i).Cells(2).Value?.ToString()) AndAlso
               Not String.IsNullOrEmpty(dgvDirectors.Rows(i).Cells(4).Value?.ToString()) Then

                        Dim query As String = "
                    SELECT * FROM Lab_Directors 
                    WHERE Company_ID = @CompanyID 
                    AND License = @License"
                        Using command As New SqlCommand(query, connection)
                            command.Parameters.AddWithValue("@CompanyID", txtCompanyID.Text)
                            command.Parameters.AddWithValue("@License", dgvDirectors.Rows(i).Cells(4).Value)

                            Using adapter As New SqlDataAdapter(command)
                                Using builder As New SqlCommandBuilder(adapter)
                                    Dim dataTable As New DataTable()
                                    adapter.Fill(dataTable)

                                    Dim row As DataRow
                                    Dim DirID As Long

                                    If dataTable.Rows.Count = 0 Then
                                        row = dataTable.NewRow()
                                        DirID = GetNextDirectorID()
                                        row("ID") = DirID
                                        dataTable.Rows.Add(row)
                                    Else
                                        row = dataTable.Rows(0)
                                        DirID = row("ID")
                                    End If

                                    GoodDirIDs += $"{DirID}, "

                                    row("LastName") = dgvDirectors.Rows(i).Cells(1).Value
                                    row("FirstName") = dgvDirectors.Rows(i).Cells(2).Value
                                    row("Degree") = dgvDirectors.Rows(i).Cells(3).Value
                                    row("License") = dgvDirectors.Rows(i).Cells(4).Value
                                    row("Cell") = If(Not String.IsNullOrEmpty(dgvDirectors.Rows(i).Cells(5).Value?.ToString()),
                                             PhoneNeat(dgvDirectors.Rows(i).Cells(5).Value), DBNull.Value)
                                    row("Email") = If(Not String.IsNullOrEmpty(dgvDirectors.Rows(i).Cells(6).Value?.ToString()),
                                              dgvDirectors.Rows(i).Cells(6).Value, DBNull.Value)
                                    row("IsDefault") = dgvDirectors.Rows(i).Cells(7).Value
                                    row("EffectiveFrom") = If(IsDate(dgvDirectors.Rows(i).Cells(8).Value),
                                                      CDate(dgvDirectors.Rows(i).Cells(8).Value).ToString("MM/dd/yyyy 00:00:00"), DBNull.Value)
                                    row("EffectiveTo") = If(IsDate(dgvDirectors.Rows(i).Cells(9).Value),
                                                    CDate(dgvDirectors.Rows(i).Cells(9).Value).ToString("MM/dd/yyyy 23:59:00"), DBNull.Value)

                                    If Not String.IsNullOrEmpty(dgvDirectors.Rows(i).Cells(10).Value?.ToString()) Then
                                        Dim ADDRS() As String = dgvDirectors.Rows(i).Cells(10).Value.ToString().Split("|"c)
                                        If ADDRS.Length >= 5 AndAlso Not String.IsNullOrEmpty(ADDRS(0)) AndAlso
                                   Not String.IsNullOrEmpty(ADDRS(2)) AndAlso
                                   Not String.IsNullOrEmpty(ADDRS(3)) AndAlso
                                   Not String.IsNullOrEmpty(ADDRS(4)) Then
                                            row("Address_ID") = GetAddressID(ADDRS(0), ADDRS(1), ADDRS(2), ADDRS(3), ADDRS(4), ADDRS(5))
                                        Else
                                            row("Address_ID") = -1
                                        End If
                                    Else
                                        row("Address_ID") = DBNull.Value
                                    End If

                                    row("Company_ID") = txtID.Text
                                    row("LastEdited_On") = Date.Now
                                    row("Edited_By") = ThisUser.ID

                                    adapter.Update(dataTable)
                                End Using
                            End Using
                        End Using
                    End If
                Next

                If GoodDirIDs.Length > 2 Then
                    GoodDirIDs = GoodDirIDs.Substring(0, GoodDirIDs.Length - 2)
                    Dim deleteQuery As String = "DELETE FROM Lab_Directors WHERE NOT ID IN (" & GoodDirIDs & ")"
                    Using deleteCommand As New SqlCommand(deleteQuery, connection)
                        deleteCommand.ExecuteNonQuery()
                    End Using
                End If
            End If
        End Using
    End Sub

    Private Function GetNextDirectorID() As Long
        Dim DirID As Long
        Dim sSQL As String = "Select Max(ID) as LastID from Lab_Directors where Company_ID = " & txtID.Text
        Dim cnndi As New SqlConnection(connString)
        cnndi.Open()
        Dim cmdndi As New SqlCommand(sSQL, cnndi)
        cmdndi.CommandType = CommandType.Text
        Dim DRndi As SqlDataReader = cmdndi.ExecuteReader
        If DRndi.HasRows Then
            While DRndi.Read
                If DRndi("LastID") IsNot DBNull.Value Then
                    DirID = DRndi("LastID") + 1
                Else
                    DirID = 1
                End If
            End While
        End If
        cnndi.Close()
        cnndi = Nothing
        Return DirID
    End Function

    Private Function Img_To_Array(ByVal Img As Image, ByVal ImgFmt As Imaging.ImageFormat) As Byte()
        Dim Ret As Byte() = Nothing
        Dim ms As New IO.MemoryStream
        Try
            Img.Save(ms, ImgFmt)
            Ret = ms.ToArray()
        Catch

        End Try
        Return Ret
    End Function

    Private Sub frmCompany_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtPhone.Mask = SystemConfig.PhoneMask
        txtFax.Mask = SystemConfig.PhoneMask
        txtDCell.Mask = SystemConfig.PhoneMask
        txtFrom.Text = Format(Date.Now, SystemConfig.DateFormat)
        PopulateHours()

        Dim comID = Trim(LIC.Licensee.Substring(0, InStr(LIC.Licensee, "-") - 1))
        If CommonData.IsExistsInTable("Company", "ID", comID) Then
            DisplayMe(Trim(LIC.Licensee.Substring(0, InStr(LIC.Licensee, "-") - 1)))
        Else
            DisplayMe(1)
        End If

        If txtID.Text <> "" Then DisplayMerchantSetting(txtID.Text)
        'ORLic = New LicenseManager.ProlisLicense(CN, "ProlisOutreach")
        'If ORLic.AppRun = True Then
        '    gbTheme.Enabled = True
        '    DisplayTheme()
        'Else
        '    gbTheme.Enabled = False
        'End If
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub DisplayMerchantSetting(ByVal CompanyID As Long)
        txtCompanyID.Text = CompanyID.ToString
        Dim cnms As New SqlConnection(connString)
        cnms.Open()
        Dim cmdms As New SqlCommand("Select * from " &
        "Merchant_Setting where Company_ID = " & CompanyID, cnms)
        cmdms.CommandType = CommandType.Text
        Dim drms As SqlDataReader = cmdms.ExecuteReader
        If drms.HasRows Then
            While drms.Read
                txtMerchantID.Text = drms("Merchant_ID")
                If txtMerchantID.Text <> "" Then txtMerchantID.ReadOnly = True
                txtProcesserKey.Text = drms("ProcesserKey")
                If txtProcesserKey.Text <> "" Then txtProcesserKey.ReadOnly = True
                txtProcesserName.Text = drms("ProcesserName")
                If txtProcesserName.Text <> "" Then txtProcesserName.ReadOnly = True
            End While
        End If
        cnms.Close()
        cnms = Nothing
    End Sub

    Private Sub DisplayTheme()
        Dim sSQL As String = "Select * from UserTheme where Company_ID = " & txtID.Text
        Dim cndt As New SqlConnection(connString)
        cndt.Open()
        Dim cmddt As New SqlCommand(sSQL, cndt)
        cmddt.CommandType = CommandType.Text
        Dim DRdt As SqlDataReader = cmddt.ExecuteReader
        If DRdt.HasRows Then
            While DRdt.Read
                If DRdt("Banner") IsNot DBNull.Value Then txtBanner.Text = DRdt("Banner")
                If DRdt("BackgroundColor") IsNot DBNull.Value Then
                    txtBGColor.Text = DRdt("BackgroundColor")
                    gbTheme.BackColor = DRdt("BackgroundColor")
                Else
                    txtBGColor.Text = ""
                    gbTheme.BackColor = Color.Transparent
                End If
                If DRdt("BackgroundImg") IsNot DBNull.Value Then txtBGImage.Text = DRdt("BackgroundImg")
                If DRdt("MenuBar") IsNot DBNull.Value Then
                    txtMenuColor.Text = DRdt("MenuBar")
                    lblTop.BackColor = System.Drawing.ColorTranslator.FromHtml(DRdt("MenuBar"))
                    lblBottom.BackColor = System.Drawing.ColorTranslator.FromHtml(DRdt("MenuBar"))
                    lblIntroText.BackColor = System.Drawing.ColorTranslator.FromHtml(DRdt("MenuBar"))
                Else
                    txtMenuColor.Text = ""
                    lblTop.BackColor = Color.Transparent
                    lblBottom.BackColor = Color.Transparent
                    lblIntroText.BackColor = Color.Transparent
                End If
                If DRdt("HomeImg") IsNot DBNull.Value Then txtHomeImage.Text = DRdt("HomeImg")
                If DRdt("ProvLoginImg") IsNot DBNull.Value Then txtProvImage.Text = DRdt("ProvLoginImg")
                If DRdt("SalesLogin") IsNot DBNull.Value Then txtEmpImage.Text = DRdt("SalesLogin")
                If DRdt("FrmColor") IsNot DBNull.Value Then txtFormColor.Text = DRdt("FrmColor")
                If DRdt("FrmImage") IsNot DBNull.Value Then txtFormImage.Text = DRdt("FrmImage")
                If DRdt("IntroText") IsNot DBNull.Value Then
                    Using reportWebBrowser As New WebBrowser
                        reportWebBrowser.CreateControl()
                        reportWebBrowser.DocumentText = DRdt("IntroText")
                        While reportWebBrowser.DocumentText <> DRdt("IntroText")
                            Application.DoEvents()
                        End While
                        reportWebBrowser.Document.ExecCommand("SelectAll", False, Nothing)
                        reportWebBrowser.Document.ExecCommand("Copy", False, Nothing)
                        txtIntroText.Paste()
                    End Using
                End If
            End While
        End If
        cndt.Close()
        cndt = Nothing
    End Sub

    Private Sub DisplayMe(ByVal CompanyID As Long)
        Dim sSQL As String = "Select * from Company where ID = " & CompanyID
        Dim cndme As New SqlConnection(connString)
        cndme.Open()
        Dim cmddme As New SqlCommand(sSQL, cndme)
        cmddme.CommandType = CommandType.Text
        Dim DRdme As SqlDataReader = cmddme.ExecuteReader
        If DRdme.HasRows Then
            While DRdme.Read
                txtID.Text = DRdme("ID")
                chkBusInd.Checked = DRdme("IsIndividual")
                txtLName_BSN.Text = DRdme("LastName_BSN")
                If chkBusInd.Checked = True Then
                    txtFName.Text = DRdme("FirstName")
                    txtMName.Text = DRdme("MiddleName")
                    txtDegree.Text = DRdme("Degree")
                End If
                txtAd.Text = DRdme("Adv_Line")
                txtSSN.Text = DRdme("SSN")
                If DRdme("Logo") IsNot DBNull.Value Then
                    Dim Arr As Byte() = CType(DRdme("Logo"), Byte())
                    pctLogo.Image = Array_To_Img(Arr)
                End If
                If DRdme("License") IsNot DBNull.Value Then txtLicense.Text = DRdme("License")
                If DRdme("UPIN") IsNot DBNull.Value Then txtUPIN.Text = DRdme("UPIN")
                If DRdme("CLIA") IsNot System.DBNull.Value Then txtCLIA.Text = DRdme("CLIA")
                If DRdme("Biller") IsNot System.DBNull.Value Then txtBiller.Text = DRdme("Biller")
                If DRdme("NPI") IsNot DBNull.Value Then txtNPI.Text = DRdme("NPI")
                If DRdme("PartNo") IsNot DBNull.Value Then txtPartNo.Text = DRdme("PartNo")
                If DRdme("Medicare") IsNot DBNull.Value Then txtMCR.Text = DRdme("Medicare")
                If DRdme("Medicaid") IsNot DBNull.Value Then txtMCD.Text = DRdme("Medicaid")
                If DRdme("BCBS") IsNot DBNull.Value Then txtBCBS.Text = DRdme("BCBS")
                If DRdme("InterfaceDLL") IsNot DBNull.Value Then txtCommDLL.Text = DRdme("InterfaceDLL")
                'MyLic = DRdme("System_Params")
                txtSystemParams.Text = DRdme("System_Params")
                If DRdme("Phone") IsNot DBNull.Value Then txtPhone.Text = DRdme("Phone")
                If DRdme("Fax") IsNot DBNull.Value Then txtFax.Text = DRdme("Fax")
                If DRdme("Email") IsNot DBNull.Value Then txtEMail.Text = DRdme("Email")
                If DRdme("SMTPClient") IsNot DBNull.Value Then txtSMTP.Text = DRdme("SMTPClient")
                If DRdme("Password") IsNot DBNull.Value Then txtPassword.Text = DRdme("Password")
                If DRdme("Website") IsNot DBNull.Value Then txtWebsite.Text = DRdme("Website")
                txtAdd1.Text = GetAddress1(DRdme("Address_ID"))
                txtAdd2.Text = GetAddress2(DRdme("Address_ID"))
                txtCity.Text = GetAddressCity(DRdme("Address_ID"))
                txtState.Text = GetAddressState(DRdme("Address_ID"))
                txtZip.Text = GetAddressZip(DRdme("Address_ID"))
                txtCountry.Text = GetAddressCountry(DRdme("Address_ID"))
                If DRdme("Location_Code") IsNot DBNull.Value Then txtLOC.Text = DRdme("Location_Code")
                If DRdme("MonStart") IsNot DBNull.Value Then dgvHours.Rows(0).Cells(1).Value = DRdme("MonStart").ToString
                If DRdme("MonLunchStart") IsNot DBNull.Value Then dgvHours.Rows(0).Cells(2).Value = DRdme("MonLunchStart").ToString
                If DRdme("MonLunchStop") IsNot DBNull.Value Then dgvHours.Rows(0).Cells(3).Value = DRdme("MonLunchStop").ToString
                If DRdme("MonStop") IsNot DBNull.Value Then dgvHours.Rows(0).Cells(4).Value = DRdme("MonStop").ToString
                If DRdme("TueStart") IsNot DBNull.Value Then dgvHours.Rows(1).Cells(1).Value = DRdme("TueStart").ToString
                If DRdme("TueLunchStart") IsNot DBNull.Value Then dgvHours.Rows(1).Cells(2).Value = DRdme("TueLunchStart").ToString
                If DRdme("TueLunchStop") IsNot DBNull.Value Then dgvHours.Rows(1).Cells(3).Value = DRdme("TueLunchStop").ToString
                If DRdme("TueStop") IsNot DBNull.Value Then dgvHours.Rows(1).Cells(4).Value = DRdme("TueStop").ToString
                If DRdme("WedStart") IsNot DBNull.Value Then dgvHours.Rows(2).Cells(1).Value = DRdme("WedStart").ToString
                If DRdme("WedLunchStart") IsNot DBNull.Value Then dgvHours.Rows(2).Cells(2).Value = DRdme("WedLunchStart").ToString
                If DRdme("WedLunchStop") IsNot DBNull.Value Then dgvHours.Rows(2).Cells(3).Value = DRdme("WedLunchStop").ToString
                If DRdme("WedStop") IsNot DBNull.Value Then dgvHours.Rows(2).Cells(4).Value = DRdme("WedStop").ToString
                If DRdme("ThuStart") IsNot DBNull.Value Then dgvHours.Rows(3).Cells(1).Value = DRdme("ThuStart").ToString
                If DRdme("ThuLunchStart") IsNot DBNull.Value Then dgvHours.Rows(3).Cells(2).Value = DRdme("ThuLunchStart").ToString
                If DRdme("ThuLunchStop") IsNot DBNull.Value Then dgvHours.Rows(3).Cells(3).Value = DRdme("ThuLunchStop").ToString
                If DRdme("ThuStop") IsNot DBNull.Value Then dgvHours.Rows(3).Cells(4).Value = DRdme("ThuStop").ToString
                If DRdme("FriStart") IsNot DBNull.Value Then dgvHours.Rows(4).Cells(1).Value = DRdme("FriStart").ToString
                If DRdme("FriLunchStart") IsNot DBNull.Value Then dgvHours.Rows(4).Cells(2).Value = DRdme("FriLunchStart").ToString
                If DRdme("FriLunchStop") IsNot DBNull.Value Then dgvHours.Rows(4).Cells(3).Value = DRdme("FriLunchStop").ToString
                If DRdme("FriStop") IsNot DBNull.Value Then dgvHours.Rows(4).Cells(4).Value = DRdme("FriStop").ToString
                If DRdme("SatStart") IsNot DBNull.Value Then dgvHours.Rows(5).Cells(1).Value = DRdme("SatStart").ToString
                If DRdme("SatLunchStart") IsNot DBNull.Value Then dgvHours.Rows(5).Cells(2).Value = DRdme("SatLunchStart").ToString
                If DRdme("SatLunchStop") IsNot DBNull.Value Then dgvHours.Rows(5).Cells(3).Value = DRdme("SatLunchStop").ToString
                If DRdme("SatStop") IsNot DBNull.Value Then dgvHours.Rows(5).Cells(4).Value = DRdme("SatStop").ToString
                If DRdme("SunStart") IsNot DBNull.Value Then dgvHours.Rows(6).Cells(1).Value = DRdme("SunStart").ToString
                If DRdme("SunLunchStart") IsNot DBNull.Value Then dgvHours.Rows(6).Cells(2).Value = DRdme("SunLunchStart").ToString
                If DRdme("SunLunchStop") IsNot DBNull.Value Then dgvHours.Rows(6).Cells(3).Value = DRdme("SunLunchStop").ToString
                If DRdme("SunStop") IsNot DBNull.Value Then dgvHours.Rows(6).Cells(4).Value = DRdme("SunStop").ToString
                If DRdme("Disclaimer") IsNot DBNull.Value Then txtDisclaimer.Rtf = DRdme("Disclaimer")
            End While
        End If
        cndme.Close()
        cndme = Nothing
        '
        dgvDirectors.Rows.Clear()
        Dim Degree As String = ""
        Dim DAddress As String = ""
        Dim EffFrom As String = ""
        Dim EffTo As String = ""
        sSQL = "Select * from Lab_Directors where Company_ID = " & MyLab.ID
        Dim cnld As New SqlConnection(connString)
        cnld.Open()
        Dim cmdld As New SqlCommand(sSQL, cnld)
        cmdld.CommandType = CommandType.Text
        Dim DRld As SqlDataReader = cmdld.ExecuteReader
        If DRld.HasRows Then
            While DRld.Read
                If DRld("Degree") IsNot DBNull.Value _
                AndAlso Trim(DRld("Degree")) <> "" Then
                    Degree = Trim(DRld("Degree"))
                Else
                    Degree = ""
                End If
                If DRld("EffectiveFrom") IsNot DBNull.Value _
                AndAlso DRld("EffectiveFrom") <> "12:00:00 AM" Then
                    EffFrom = Format(DRld("EffectiveFrom"), SystemConfig.DateFormat)
                Else
                    EffFrom = ""
                End If
                If DRld("EffectiveTo") IsNot DBNull.Value _
                AndAlso DRld("EffectiveTo") <> "12:00:00 AM" Then
                    EffTo = Format(DRld("EffectiveTo"), SystemConfig.DateFormat)
                Else
                    EffTo = ""
                End If
                If DRld("Address_ID") IsNot DBNull.Value Then
                    DAddress = GetAddress1(DRld("Address_ID")) & "|" & GetAddress2(DRld("Address_ID")) & "|" & _
                    GetAddressCity(DRld("Address_ID")) & "|" & GetAddressState(DRld("Address_ID")) & "|" & _
                    GetAddressZip(DRld("Address_ID")) & "|" & GetAddressCountry(DRld("Address_ID"))
                Else
                    DAddress = ""
                End If
                dgvDirectors.Rows.Add(DRld("ID"), DRld("LastName"), DRld("FirstName"), Degree, DRld("License"), _
                DRld("Cell"), DRld("Email"), DRld("IsDefault"), EffFrom, EffTo, DAddress)
            End While
        End If
        cnld.Close()
        cnld = Nothing
    End Sub

    Private Function Array_To_Img(ByVal Arr As Byte()) As System.Drawing.Image
        Dim Img As System.Drawing.Image
        Dim ms As New IO.MemoryStream(Arr, 0, Arr.Length)
        ms.Write(Arr, 0, Arr.Length)
        Img = Image.FromStream(ms, True)
        Return Img
    End Function

    Private Sub chkActive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkActive.CheckedChanged
        If chkActive.Checked = True Then
            chkActive.Text = "Active"
        Else
            chkActive.Text = "Inactive"
        End If
    End Sub

    Private Sub chkBusInd_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBusInd.CheckedChanged
        If chkBusInd.Checked = False Then
            chkBusInd.Text = "Corporation"
            txtFName.Enabled = False
            txtMName.Enabled = False
            txtDegree.Enabled = False
            lblLName.Text = "Entity Name"
            lblSSN.Text = "Federal ID"
            txtSSN.Mask = "00-0000000"
        Else
            chkBusInd.Text = "Individual"
            txtFName.Enabled = True
            txtMName.Enabled = True
            txtDegree.Enabled = True
            lblLName.Text = "Last Name"
            lblSSN.Text = "SSN"
            txtSSN.Mask = "000-00-0000"

        End If
        txtLName_BSN.Text = "" : txtFName.Text = "" : txtMName.Text = ""
        txtDegree.Text = "" : DirectorEmpty()
    End Sub

    Private Sub DirectorEmpty()
        txtDLName.Text = "" : txtDCell.Text = "" : txtDEmail.Text = ""
        chkDefault.Checked = False : txtFrom.Text = "" : txtTo.Text = ""
        txtDAdd1.Text = "" : txtDAdd2.Text = "" : txtDCity.Text = ""
        txtDState.Text = "" : txtDZip.Text = "" : txtDCountry.Text = ""
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        OpenFileDialog1.InitialDirectory = "c:\Program Files\Prolis\Images"
        OpenFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.PNG;*.ico)|*.BMP;*.JPG;*.PNG;*.ico|All Files(*.*|*.*"
        Try
            OpenFileDialog1.ShowDialog()
            If OpenFileDialog1.FileName <> "" Then
                Dim FLInfo As IO.FileInfo = New IO.FileInfo(OpenFileDialog1.FileName)
                If FLInfo.Length <= 1000000 Then
                    pctLogo.Image = Image.FromFile(OpenFileDialog1.FileName, True)
                Else
                    MsgBox("The logo file size is too large. Please reduce the file size less " & _
                    "than oe equal to the supported, 1 megabyte.", MsgBoxStyle.Critical, "Prolis")
                End If
            End If
        Catch Ex As Exception
            MessageBox.Show("Selection process aborted by the user.")
        End Try
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

    Private Sub dgvHours_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvHours.DataError
        If e.ThrowException = True Then Resume
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedIndex = 1 Then
            If chkBusInd.Checked = True Then
                DirectorEmpty()
                txtDLName.Text = Trim(txtLName_BSN.Text) & ", " & Trim(txtFName.Text) _
                & IIf(Trim(txtDegree.Text) <> "", " " & Trim(txtDegree.Text), "")
                txtDCell.Text = txtPhone.Text
                txtDEmail.Text = txtEMail.Text
            End If
        End If
    End Sub

    Private Sub txtDName_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDLName.Validated
        EditingProgress()
    End Sub

    Private Sub EditingProgress()
        If txtDLName.Text <> "" And txtDFName.Text <> "" And txtDLicense.Text <> "" Then
            btnAddDir.Enabled = True
        Else
            btnAddDir.Enabled = False
        End If
    End Sub

    Private Sub txtDFName_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDFName.Validated
        EditingProgress()
    End Sub

    Private Sub txtDLicense_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDLicense.Validated
        EditingProgress()
    End Sub

    Private Function DirectorInList(ByVal LName As String, ByVal FName As String, _
    ByVal License As String) As Boolean
        Dim InList As Boolean = False
        Dim i As Integer
        For i = 0 To dgvDirectors.RowCount - 1
            If dgvDirectors.Rows(i).Cells(4).Value = License Then
                InList = True
                Exit For
            End If
        Next
        Return InList
    End Function

    Private Function UpdateDirectorsList(ByVal LName As String, ByVal FName As String, ByVal Degree As _
    String, ByVal License As String, ByVal Cell As String, ByVal Email As String, ByVal IsDef As _
    Boolean, ByVal EffFrom As String, ByVal EffTo As String, ByVal DAddress As String) As Integer
        Dim i As Integer
        Dim RowID As Integer = 0
        For i = 0 To dgvDirectors.RowCount - 1
            If dgvDirectors.Rows(i).Cells(4).Value = License Then
                dgvDirectors.Rows(i).Cells(1).Value = LName
                dgvDirectors.Rows(i).Cells(2).Value = FName
                dgvDirectors.Rows(i).Cells(3).Value = Degree
                dgvDirectors.Rows(i).Cells(4).Value = License
                dgvDirectors.Rows(i).Cells(5).Value = Cell
                dgvDirectors.Rows(i).Cells(6).Value = Email
                dgvDirectors.Rows(i).Cells(7).Value = IsDef
                dgvDirectors.Rows(i).Cells(8).Value = EffFrom
                dgvDirectors.Rows(i).Cells(9).Value = EffTo
                dgvDirectors.Rows(i).Cells(10).Value = DAddress
                RowID = i
                Exit For
            End If
        Next
        Return RowID
    End Function


    Private Sub btnAddDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddDir.Click
        If txtDLName.Text <> "" And txtDFName.Text <> "" And txtDLicense.Text <> "" Then
            Dim DAddress As String = ""
            Dim RowID As Integer = 0
            If txtDAdd1.Text <> "" And txtDCity.Text <> "" And txtDState.Text <> "" _
            And txtDZip.Text <> "" Then DAddress = Trim(txtDAdd1.Text) & "|" & _
            Trim(txtDAdd2.Text) & "|" & Trim(txtDCity.Text) & "|" & Trim(txtDState.Text) _
            & "|" & Trim(txtDZip.Text) & "|" & Trim(txtDCountry.Text)
            Dim Cell As String = IIf(UserEnteredText(txtDCell) = "", "", txtDCell.Text)
            Dim EffFrom As String = IIf(UserEnteredText(txtFrom) = "", "", txtFrom.Text)
            Dim EffTo As String = IIf(UserEnteredText(txtTo) = "", "", txtTo.Text)
            '
            Dim IsDef As Boolean = chkDefault.Checked
            If IsDef = True AndAlso IsDate(txtTo.Text) AndAlso _
            CDate(txtTo.Text) < Date.Today Then IsDef = False
            '
            If Not DirectorInList(Trim(txtDLName.Text), Trim(txtDFName.Text), Trim(txtDLicense.Text)) Then
                dgvDirectors.Rows.Add("", Trim(txtDLName.Text), Trim(txtDFName.Text), _
                Trim(txtDDegree.Text), Trim(txtDLicense.Text), Cell, Trim(txtDEmail.Text), _
                IsDef, EffFrom, EffTo, DAddress)
                RowID = dgvDirectors.RowCount - 1
            Else
                RowID = UpdateDirectorsList(Trim(txtDLName.Text), Trim(txtDFName.Text), _
                Trim(txtDDegree.Text), Trim(txtDLicense.Text), Cell, Trim(txtDEmail.Text), _
                IsDef, EffFrom, EffTo, DAddress)
            End If
            If dgvDirectors.Rows(RowID).Cells(7).Value = True Then  'default
                For i As Integer = 0 To dgvDirectors.RowCount - 1
                    If i <> RowID Then dgvDirectors.Rows(i).Cells(7).Value = False
                Next
            End If
            txtDLName.Text = ""
            txtDFName.Text = ""
            txtDDegree.Text = ""
            txtDLicense.Text = ""
            txtDCell.Text = ""
            txtDEmail.Text = ""
            chkDefault.Checked = False
            txtFrom.Text = ""
            txtTo.Text = ""
            txtDAdd1.Text = ""
            txtDAdd2.Text = ""
            txtDCity.Text = ""
            txtDState.Text = ""
            txtDZip.Text = ""
            txtDCountry.Text = ""
            btnAddDir.Enabled = False
        End If
    End Sub

    Private Sub dgvDirectors_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDirectors.CellDoubleClick
        If dgvDirectors.Rows(e.RowIndex).Cells(1).Value <> "" And _
        dgvDirectors.Rows(e.RowIndex).Cells(2).Value <> "" And _
        dgvDirectors.Rows(e.RowIndex).Cells(4).Value <> "" Then
            txtDLName.Text = dgvDirectors.Rows(e.RowIndex).Cells(1).Value
            txtDFName.Text = dgvDirectors.Rows(e.RowIndex).Cells(2).Value
            If dgvDirectors.Rows(e.RowIndex).Cells(3).Value IsNot System.DBNull.Value _
            Then txtDDegree.Text = dgvDirectors.Rows(e.RowIndex).Cells(3).Value
            If dgvDirectors.Rows(e.RowIndex).Cells(4).Value IsNot System.DBNull.Value _
            Then txtDLicense.Text = dgvDirectors.Rows(e.RowIndex).Cells(4).Value
            If dgvDirectors.Rows(e.RowIndex).Cells(5).Value IsNot System.DBNull.Value _
            Then txtDCell.Text = dgvDirectors.Rows(e.RowIndex).Cells(5).Value
            If dgvDirectors.Rows(e.RowIndex).Cells(6).Value IsNot System.DBNull.Value _
            Then txtDEmail.Text = dgvDirectors.Rows(e.RowIndex).Cells(6).Value
            chkDefault.Checked = dgvDirectors.Rows(e.RowIndex).Cells(7).Value
            txtFrom.Text = dgvDirectors.Rows(e.RowIndex).Cells(8).Value
            txtTo.Text = dgvDirectors.Rows(e.RowIndex).Cells(9).Value
            If dgvDirectors.Rows(e.RowIndex).Cells(10).Value <> "" Then
                Dim ADDRS() As String = Split(dgvDirectors.Rows(e.RowIndex).Cells(10).Value, "|")
                txtDAdd1.Text = ADDRS(0)
                txtDAdd2.Text = ADDRS(1)
                txtDCity.Text = ADDRS(2)
                txtDState.Text = ADDRS(3)
                txtDZip.Text = ADDRS(4)
                txtDCountry.Text = ADDRS(5)
            End If
            btnAddDir.Enabled = True
            btnDelDir.Enabled = True
        Else
            btnAddDir.Enabled = False
            btnDelDir.Enabled = False
        End If
    End Sub

    Private Sub btnDelDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelDir.Click
        Dim RetVal As Integer = MsgBox("Reports associated with this Director will no longer display any information about the Director. Are you certain to delete this record.?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")

        If RetVal = vbYes Then
            If dgvDirectors.SelectedRows.Count > 0 Then
                dgvDirectors.Rows.Remove(dgvDirectors.SelectedRows(0))
                txtDLName.Text = ""
                txtDFName.Text = ""
                txtDDegree.Text = ""
                txtDLicense.Text = ""
                txtDCell.Text = ""
                txtDEmail.Text = ""
                chkDefault.Checked = False
                txtFrom.Text = ""
                txtTo.Text = ""
                txtDAdd1.Text = ""
                txtDAdd2.Text = ""
                txtDCity.Text = ""
                txtDState.Text = ""
                txtDZip.Text = ""
                txtDCountry.Text = ""
                btnAddDir.Enabled = False
                btnDelDir.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFont.Click
        Dim MyFont As New FontDialog
        MyFont.Font = txtDisclaimer.SelectionFont
        Dim RetVal As Integer = MyFont.ShowDialog
        If RetVal = System.Windows.Forms.DialogResult.OK Then
            txtDisclaimer.SelectionFont = MyFont.Font
        End If
        MyFont = Nothing
    End Sub

    Private Sub btnColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnColor.Click
        Dim MyFC As New ColorDialog
        MyFC.Color = txtDisclaimer.SelectionColor
        Dim RetVal As Integer = MyFC.ShowDialog
        If RetVal = System.Windows.Forms.DialogResult.OK Then
            txtDisclaimer.SelectionColor = MyFC.Color
        End If
        MyFC = Nothing
    End Sub

    Private Sub chkLeft_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLeft.CheckedChanged
        If chkLeft.Checked = True Then
            txtDisclaimer.SelectionAlignment = HorizontalAlignment.Left
            chkCenter.Checked = False
            chkRight.Checked = False
        End If
    End Sub

    Private Sub chkCenter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCenter.CheckedChanged
        If chkCenter.Checked = True Then
            txtDisclaimer.SelectionAlignment = HorizontalAlignment.Center
            chkLeft.Checked = False
            chkRight.Checked = False
        Else
            chkLeft.Checked = True
        End If
    End Sub

    Private Sub chkRight_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRight.CheckedChanged
        If chkRight.Checked = True Then
            txtDisclaimer.SelectionAlignment = HorizontalAlignment.Right
            chkLeft.Checked = False
            chkCenter.Checked = False
        Else
            chkLeft.Checked = True
        End If
    End Sub

    Private Sub chkLAlign_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLAlign.CheckedChanged
        If chkLAlign.Checked = True Then
            txtIntroText.SelectionAlignment = HorizontalAlignment.Left
            chkCAlign.Checked = False
            chkRAlign.Checked = False
        End If
    End Sub

    Private Sub chkCAlign_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCAlign.CheckedChanged
        If chkCAlign.Checked = True Then
            txtIntroText.SelectionAlignment = HorizontalAlignment.Center
            chkLAlign.Checked = False
            chkRAlign.Checked = False
        End If
    End Sub

    Private Sub chkRAlign_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRAlign.CheckedChanged
        If chkRAlign.Checked = True Then
            txtIntroText.SelectionAlignment = HorizontalAlignment.Right
            chkCAlign.Checked = False
            chkLAlign.Checked = False
        End If
    End Sub

    Private Sub btnIntroFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIntroFont.Click
        FontDialog1.Font = txtIntroText.SelectionFont
        FontDialog1.Color = txtIntroText.SelectionColor
        FontDialog1.ShowDialog()
        txtIntroText.SelectionFont = FontDialog1.Font
    End Sub

    Private Sub btnBGCLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBGCLook.Click
        If Trim(txtBGColor.Text) <> "" Then _
        ColorDialog1.Color = System.Drawing.ColorTranslator.FromHtml(txtBGColor.Text)
        If System.Windows.Forms.DialogResult.OK = ColorDialog1.ShowDialog Then
            txtBGColor.Text = System.Drawing.ColorTranslator.ToHtml(ColorDialog1.Color)
            gbTheme.BackColor = System.Drawing.ColorTranslator.FromHtml(txtBGColor.Text)
        Else
            txtBGColor.Text = ""
            gbTheme.BackColor = Color.Transparent
        End If
    End Sub

    Private Sub btnBGILook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBGILook.Click
        OpenFileDialog1.FileName = txtBGImage.Text
        If System.Windows.Forms.DialogResult.OK = OpenFileDialog1.ShowDialog Then
            txtBGImage.Text = OpenFileDialog1.FileName
            gbTheme.BackgroundImage = Image.FromFile(OpenFileDialog1.FileName)
        Else
            If txtBGImage.Text <> "" Then txtBGImage.Text = ""
            gbTheme.BackgroundImage = Nothing
        End If
    End Sub

    Private Sub btnMCLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMCLook.Click
        If txtMenuColor.Text <> "" Then _
        ColorDialog1.Color = System.Drawing.ColorTranslator.FromHtml(txtMenuColor.Text)
        If System.Windows.Forms.DialogResult.OK = ColorDialog1.ShowDialog Then
            txtMenuColor.Text = System.Drawing.ColorTranslator.ToHtml(ColorDialog1.Color)
            lblTop.BackColor = System.Drawing.ColorTranslator.FromHtml(txtMenuColor.Text)
            lblBottom.BackColor = System.Drawing.ColorTranslator.FromHtml(txtMenuColor.Text)
            lblIntroText.BackColor = System.Drawing.ColorTranslator.FromHtml(txtMenuColor.Text)
        Else
            txtMenuColor.Text = ""
            lblTop.BackColor = Color.Transparent
            lblBottom.BackColor = Color.Transparent
            lblIntroText.BackColor = Color.Transparent
        End If
    End Sub

    Private Sub btnIntroColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIntroColor.Click
        ColorDialog1.Color = txtIntroText.SelectionColor
        If System.Windows.Forms.DialogResult.OK = ColorDialog1.ShowDialog Then
            txtIntroText.SelectionColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub txtMerchantID_KeyUp(sender As Object, e As KeyEventArgs) Handles txtMerchantID.KeyUp
        If e.KeyCode = Keys.F9 Then
            txtMerchantID.ReadOnly = False
        End If
    End Sub

    Private Sub txtProcesserKey_KeyUp(sender As Object, e As KeyEventArgs) Handles txtProcesserKey.KeyUp
        If e.KeyCode = Keys.F9 Then
            txtProcesserKey.ReadOnly = False
        End If
    End Sub

    Private Sub txtProcesserName_KeyUp(sender As Object, e As KeyEventArgs) Handles txtProcesserName.KeyUp
        If e.KeyCode = Keys.F9 Then
            txtProcesserName.ReadOnly = False
        End If
    End Sub
End Class
