Imports System.Windows.Forms
Imports System.Globalization
Imports Dymo
Imports System.IO
Imports Prolis.BLL
Imports Prolis.Utils

Public Class frmDashboard
    'Public DymoAddIn As Dymo.DymoAddIn
    'Public DymoLabel As Dymo.DymoLabels
    Private updateTimer As Threading.Timer

    'Private qcProcs As QCPROCS

    Private Sub frmDashboard_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'If ALO IsNot Nothing Then ALO = Nothing
        'If ALOTIMER IsNot Nothing Then ALOTIMER = Nothing
        'If updateTimer IsNot Nothing Then updateTimer = Nothing
        'If LIC IsNot Nothing Then LIC = Nothing
    End Sub

    Private Sub UpdateServerName(ByVal SRVNAME As String, ByVal UID As Long)
        Dim sSQL As String = "If Exists (Select * from Users where IsActive <> 0 and ID = " & UID &
        ") Update Users Set ProlisServer = '" & SRVNAME & "' where IsActive <> 0 and ID = " & UID
        '  ExecuteSqlProcedure(sSQL)

        ConnectionManagerBLL.ExecuteCommand(sSQL)
    End Sub

    Private Sub frmDashboard_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F6 Then
            If lblUser.Text <> "" Then btnLogout_Click(Nothing, Nothing)
            frmSetting.ShowDialog()
            If frmSetting.DialogResult = DialogResult.OK Then
                lblConnection.Text = GetVersionText("version.txt")
                LIC = Nothing
                InitializeLabData()
            Else

                MsgBox("Incorrect Connection supplied, Please reconfigure. You may press F6 " &
                    "key to configure it in optimized mode.", MsgBoxStyle.Information, "Prolis")
                My.Settings.ProlisServer = ""
                My.Settings.Save()
                '  lblConnection.Text = GetVersionText("version.txt")
            End If
            'Else
            '        MsgBox("Incorrect Connection supplied, Please reconfigure.",
            '        MsgBoxStyle.Critical, "Prolis")
            'End If
        End If
    End Sub

    Function GetVersionText(ByVal filePath As String) As String
        ' Check if the file exists
        If Not File.Exists(filePath) Then
            ' Create the file and write "2.0" inside
            File.WriteAllText(filePath, "2.0")
            Return "2.0"
        Else
            ' Read and return the file content
            Return File.ReadAllText(filePath).Trim()
        End If
    End Function
    Private Sub frmDashboard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF1cWWhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEBjW39WcHxRQWNUVUF1Vg==")
        'DymoSDK.App.Init()

        InitializeContextMenu()


        PanicStyle.ForeColor = Color.White

        PanicStyle.BackColor = Color.Red
        HLAStyle.BackColor = Color.Yellow '

        ' Enable double buffering for the form
        Me.DoubleBuffered = True
        ' Enable double buffering for the panel
        SetDoubleBuffered(ToolStrip, True)


        mnuEResults.Visible = False
        mnuPublish.Visible = False
        '*******************************************
        ' Assuming "HelpToolStripMenuItem" is the name of your "Help" menu item
        Dim helpMenuItem As ToolStripItem = mnuHelp

        ' Get the location of the Help menu item
        Dim helpMenuItemLocation As Point = helpMenuItem.Bounds.Location

        ' Convert the location to screen coordinates if necessary
        helpMenuItemLocation = MenuStrip.PointToScreen(helpMenuItemLocation)

        ' lblAlert.Location = New Point(helpMenuItemLocation.X + helpMenuItem.Width + 100, helpMenuItemLocation.Y - 10)
        '*******************************************

        Try
            If My.Settings.ProlisServer <> "" And My.Settings.Database <> "" _
            And My.Settings.UID <> "" And My.Settings.PWD <> "" Then

                connString = $"Data Source={My.Settings.ProlisServer}; Initial Catalog={My.Settings.Database}; Integrated Security=False;User Id={CryptoHelper.Decrypt(My.Settings.UID)};Password={CryptoHelper.Decrypt(My.Settings.PWD)};TrustServerCertificate=True;MultipleActiveResultSets=True;"
                '
                InitializeLabData()
                If connString <> "" Then
                    lblConnection.Text = GetVersionText("version.txt")

                    'SetGlobalSettings()

                End If
            Else
                frmSetting.ShowDialog()
            End If
        Catch ex As Exception
            MsgBox("An error '" & ex.Message & "' has occured. Prolis can't connect to its database. " _
            & "Please use F6 key to reset the configuration.", MsgBoxStyle.Critical, "Prolis")
        End Try
        '<<<<<<< Updated upstream
        '      StartUpdaterCheck()
        '=======

        AppLogger.SetupLogger() ' Initialize logging when the form loads
        LoggerHelper.LogInfo("frmDashboard loaded successfully.")
        '>>>>>>> Stashed changes
    End Sub

    'Private Shared Sub SetGlobalSettings()
    '    GlobalSettings.sqlCS = connString
    '    GlobalSettings.ServerName = My.Settings.ProlisServer
    '    GlobalSettings.DbName = My.Settings.Database
    '    GlobalSettings.DbUser = CryptoHelper.Decrypt(My.Settings.UID)
    '    GlobalSettings.PWD = CryptoHelper.Decrypt(My.Settings.PWD)
    'End Sub

    Private Sub InitializeLabData()

        If LIC Is Nothing Then LIC = New LicenseManager.ProlisLicense(ConvertToOdbcConnectionString(connString), My.Application.Info.AssemblyName)
        'Get SeatCount from License
        If LIC IsNot Nothing Then
            LicDate = Date.Today
            Seats = LIC.SEATS
            SEAT = ValidateMySeat(Seats, My.Computer.Name)
            If SEAT = "" Then
                MsgBox("Prolis Is licensed For " & Seats & " Workstation(s). " &
                "This execution exceeds the allowed limit. You may contact " &
                "Prolis Support, For help", MsgBoxStyle.Critical, "Prolis License")
                Application.Exit()
            End If
            Dim comID = Trim(LIC.Licensee.Substring(0, InStr(LIC.Licensee, "-") - 1))
            If CommonData.IsExistsInTable("Company", "ID", comID) Then
                InitializeMylab(Trim(LIC.Licensee.Substring(0, InStr(LIC.Licensee, "-") - 1)))
            Else
                InitializeMylab(1)
            End If

            InitializeConfiguration(MyLab.ID)
            If LIC.DiagTarget = "H" Then
                Me.Text = "Prolis (H) - " & Trim(Microsoft.VisualBasic.Mid(LIC.Licensee.ToString,
                InStr(LIC.Licensee.ToString, "-") + 1)) & " - Environment: " & SystemConfig.Environment
            Else
                Me.Text = "Prolis (V) - " & Trim(Microsoft.VisualBasic.Mid(LIC.Licensee.ToString,
                InStr(LIC.Licensee.ToString, "-") + 1)) & " - Environment: " & SystemConfig.Environment
            End If
            '**** Temp ****************
            'CN = New ADODB.Connection
            'CN.Open(connstring)
            '**************************
        Else
            MsgBox("Invalid License", MsgBoxStyle.Critical, "Prolis License")
            Me.Text += " - Unlicensed - Environment: " & SystemConfig.Environment
        End If
    End Sub

    Private Sub StartUpdaterCheck()
        ' Convert minutes to milliseconds (10 minutes = 600,000 ms)
        Dim interval As Integer = 20 * 60 * 1000

        ' Create a Timer that calls EnsureUpdaterExists() every 10 minutes
        updateTimer = New Threading.Timer(AddressOf TimerCallback, Nothing, 0, interval)
    End Sub

    Private Sub TimerCallback(state As Object)
        Try
            Console.WriteLine("Checking for updater updates...")
            EnsureUpdaterExists()
        Catch ex As Exception
            Console.WriteLine("Error in updater check: " & ex.Message)
        End Try
    End Sub
    'Private Function InitializesqlCN(ByVal UID As Long) As SqlConnection
    '    'connString = ""
    '    Dim Rs As New ADODB.Recordset
    '    Try
    '        Rs.Open("Select ProlisServer from Users where IsActive <> 0 and ID = " & UID, _
    '        CN, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
    '        If Not Rs.BOF Then
    '            If Rs.Fields("ProlisServer").Value IsNot DBNull.Value _
    '            AndAlso Trim(Rs.Fields("ProlisServer").Value) <> "" Then
    '                connString = "Data Source=" & Rs.Fields("ProlisServer").Value & "; Initial Catalog=" & _
    '                My.Settings.Database & "; Integrated Security=False;User Id=" & _
    '                My.Settings.UID & "; " & "Password=" & My.Settings.PWD & ";"
    '                My.Settings.ProlisServer = Rs.Fields("ProlisServer").Value
    '            End If
    '        End If
    '        Rs.Close()
    '    Catch ex As Exception
    '    Finally
    '        If Rs.State <> 0 Then Rs.Close()
    '        Rs = Nothing
    '    End Try
    '    '
    '    If connString <> "" Then
    '        Try
    '            sqlCN = New SqlConnection(connString)
    '            sqlCN.Open()
    '        Catch ex As Exception
    '            sqlCN = Nothing
    '        End Try
    '    Else
    '        sqlCN = Nothing
    '    End If
    '    Return sqlCN
    'End Function

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        frmLogin.ShowDialog()
    End Sub

    Public Sub LoginUser(ByVal User_ID As Integer)
        'On Error Resume Next
        If LIC Is Nothing Then LIC = New LicenseManager.ProlisLicense(connString, My.Application.Info.AssemblyName)
        If LIC IsNot Nothing Then
            SEAT = ValidateMySeat(Seats, My.Computer.Name)
            If LIC.AppRun = True Then
                InitializeConfiguration(MyLab.ID)
                btnLogin.Enabled = False
                mnuLogin.Enabled = False
                btnLogout.Enabled = True
                mnuLogout.Enabled = True
                ThisUser.IsLoggedIn = SetDBLogIn(User_ID)
                StatusStrip.Text = "Hello! " & ThisUser.Name
                'txtMessage.Text = "Message for You"
                mnuHelp.Enabled = True
                If LIC.CS = True Then
                    mnuResInq.Enabled = ThisUser.Cus_Svc
                    mnuMsg.Enabled = ThisUser.Cus_Svc
                    mnuTestInq.Enabled = ThisUser.Cus_Svc
                    mnuProvInquiry.Enabled = ThisUser.Cus_Svc
                    mnuPickup.Enabled = ThisUser.Cus_Svc
                    mnuPickupRpt.Enabled = ThisUser.Cus_Svc
                    mnuARInq.Enabled = IIf(ThisUser.ARC = True And ThisUser.Cus_Svc = True, True, False)
                    mnuPrintReqs.Enabled = ThisUser.Cus_Svc
                    mnuPanicMgmt.Enabled = ThisUser.Cus_Svc
                    mnuPrintPanicReport.Enabled = ThisUser.Cus_Svc
                    mnuPanicHistory.Enabled = ThisUser.Cus_Svc
                Else
                    mnuResInq.Enabled = False
                    mnuMsg.Enabled = False
                    mnuTestInq.Enabled = False
                    mnuProvInquiry.Enabled = False
                    mnuPickup.Enabled = False
                    mnuPickupRpt.Enabled = False
                    mnuARInq.Enabled = False
                    mnuPrintReqs.Enabled = False
                    mnuPanicMgmt.Enabled = False
                    mnuPrintPanicReport.Enabled = False
                    mnuPanicHistory.Enabled = False
                End If
                mnuRequisitions.Enabled = ThisUser.Accession
                btnRequisitions.Enabled = ThisUser.Accession
                mnuRemoteAcc.Enabled = ThisUser.Accession
                mnuAutoAccessions.Enabled = ThisUser.Accession And LIC.AutoAccession
                mnuPrintLabels.Enabled = ThisUser.Accession
                mnuPreAnalytics.Enabled = ThisUser.Accession
                mnuPrintLog.Enabled = ThisUser.Accession
                mnuAccDash.Enabled = ThisUser.Accession
                mnuOrders.Enabled = ThisUser.Accession
                mnuInitialize.Enabled = ThisUser.Accession
                mnuPhlebotomy.Enabled = IIf(ThisUser.Accession = True And ThisUser.Report_Process = True, True, False)
                mnuSendOuts.Enabled = ThisUser.Accession
                mnuPrintSendouts.Enabled = ThisUser.Accession
                mnuQCMgmt.Enabled = ThisUser.QC_Layout
                mnuBatch.Enabled = ThisUser.Result_Entry
                mnuProcess.Enabled = ThisUser.Result_Entry
                If LIC.AER = True Then
                    mnuApplyRes.Enabled = ThisUser.Result_Entry
                Else
                    mnuApplyRes.Enabled = False
                End If
                mnuATRResults.Enabled = ThisUser.Result_Entry
                DeltaCheckReqReultsToolStripMenuItem.Enabled = ThisUser.Result_Entry
                If LIC.ALR = True Then
                    mnuApplyResRefLab.Enabled = ThisUser.Result_Entry
                Else
                    mnuApplyResRefLab.Enabled = False
                End If
                mnuEnterRes.Enabled = ThisUser.Result_Entry
                mnuResultDash.Enabled = ThisUser.Result_Entry
                'mnuExtendResults.Enabled = ThisUser.Result_Entry
                mnuRefluxResults.Enabled = ThisUser.Result_Entry
                mnuBatchResults.Enabled = ThisUser.Result_Entry
                mnuUserMgmt.Enabled = ThisUser.User_Mgmt
                mnuAnalyteManagement.Enabled = ThisUser.Test_Mgmt
                mnuGroups.Enabled = ThisUser.Test_Mgmt
                mnuProfiles.Enabled = ThisUser.Test_Mgmt
                mnuAnalysisSetup.Enabled = ThisUser.Test_Mgmt And ThisUser.Interfaces
                mnuCompRpts.Enabled = ThisUser.Test_Mgmt
                mnuPayerMgmt.Enabled = ThisUser.Dictionary And ThisUser.Billing
                mnuPayerMapping.Enabled = ThisUser.Dictionary And ThisUser.Billing
                mnuPartners.Enabled = ThisUser.Dictionary And ThisUser.Billing
                mnuPayerRpt.Enabled = ThisUser.Dictionary And ThisUser.Billing
                mnuPartnerRPT.Enabled = ThisUser.Dictionary And ThisUser.Billing
                mnuProviders.Enabled = ThisUser.Dictionary And ThisUser.Supervisor
                mnuSales.Enabled = ThisUser.Supervisor
                mnuRoutes.Enabled = ThisUser.Supervisor
                mnuClientRpts.Enabled = ThisUser.Supervisor
                mnuEditPat.Enabled = ThisUser.Accession And ThisUser.Dictionary
                mnuRemDupPat.Enabled = ThisUser.Accession And ThisUser.Dictionary
                mnuOutSource.Enabled = ThisUser.Dictionary And ThisUser.Supervisor
                mnuFacility.Enabled = ThisUser.Supervisor And ThisUser.Director
                mnuDept.Enabled = ThisUser.Supervisor
                mnuEquips.Enabled = ThisUser.Supervisor
                mnuSysConfig.Enabled = ThisUser.System_Config
                mnuWorksetup.Enabled = ThisUser.Dictionary
                mnuRptBuild.Enabled = ThisUser.Report_Build
                mnuRptOrder.Enabled = ThisUser.Report_Build
                mnuResReports.Enabled = ThisUser.Report_Process
                btnReports.Enabled = ThisUser.Report_Process
                mnuQCReports.Enabled = ThisUser.Report_Process
                mnuEResults.Enabled = ThisUser.Report_Process
                mnuMyReports.Enabled = ThisUser.Report_Process And ThisUser.Report_Build
                mnuAdhoc.Enabled = ThisUser.Report_Process And ThisUser.Report_Build
                mnuMyRptSchedule.Enabled = LIC.MyRpts And ThisUser.Report_Process
                mnuInternalReports.Enabled = ThisUser.Report_Process
                mnuRptDash.Enabled = ThisUser.Report_Process
                mnuPublish.Enabled = ThisUser.Result_Release
                mnuProlisOnControl.Enabled = ThisUser.Report_Process
                btnResultEntry.Enabled = ThisUser.Result_Entry
                mnuWorksheets.Enabled = ThisUser.Result_Entry
                mnuPhrases.Enabled = ThisUser.Dictionary
                mnuDxs.Enabled = ThisUser.Dictionary
                mnuCPTs.Enabled = ThisUser.Dictionary
                mnuBillDataSynch.Enabled = ThisUser.Billing
                mnuSuperBills.Enabled = ThisUser.Billing
                If LIC.Bill = True Then
                    mnuART.Enabled = ThisUser.Billing And ThisUser.ARC
                    mnuERA.Enabled = ThisUser.Billing And ThisUser.ARC
                    ProcessedERAToolStripMenuItem.Enabled = ThisUser.Billing And ThisUser.ARC


                    frmERA_Processed.Enabled = ThisUser.Billing And ThisUser.ARC


                    mnuPayments.Enabled = ThisUser.Billing And ThisUser.ARC
                    mnuDebits.Enabled = ThisUser.Billing And ThisUser.ARC
                    mnuStatement.Enabled = ThisUser.ARC = True And ThisUser.Report_Process
                    mnuCompReimburse.Enabled = ThisUser.ARC = True And ThisUser.Report_Process
                    mnuChargesPayments.Enabled = ThisUser.ARC = True And ThisUser.Report_Process
                    mnuPBReport.Enabled = ThisUser.Billing = True And ThisUser.ARC
                    mnuChargesByClients.Enabled = ThisUser.Billing = True And ThisUser.ARC
                    mnuPmtByClients.Enabled = ThisUser.Billing = True And ThisUser.ARC
                    mnuPmtsCollectedByPaymentDate.Enabled = ThisUser.Billing = True And ThisUser.ARC
                    mnuPaymentsCollectedByPostedDates.Enabled = ThisUser.Billing = True And ThisUser.ARC
                    mnuEbillout.Enabled = ThisUser.Billing
                    mnuBillOut.Enabled = ThisUser.Billing = True And ThisUser.Report_Process
                    mnuBatchBill.Enabled = ThisUser.Billing And LIC.BillDashboard
                    mnuBillEdit.Enabled = ThisUser.Billing
                    mnuBarAdhoc.Enabled = ThisUser.Billing
                    mnuPostBill.Enabled = ThisUser.Billing = True And ThisUser.Report_Process
                    mnuImportNecessity.Enabled = ThisUser.Billing
                    mnuImportPrices.Enabled = ThisUser.Billing
                    mnuUpdateRoster.Enabled = ThisUser.Billing
                    mnuUpdateMileageCode.Enabled = ThisUser.Billing
                    mnuCreditCards.Enabled = ThisUser.Billing And ThisUser.ARC
                    mnuBillReqs.Enabled = ThisUser.Billing
                    mnuScrubber.Enabled = ThisUser.Billing = True And ThisUser.Cus_Svc = True
                    mnuPreAuth.Enabled = ThisUser.Billing And ThisUser.Cus_Svc And LIC.PreAuth
                    mnuQuickQuote.Enabled = ThisUser.Billing = True And ThisUser.Cus_Svc = True
                Else
                    mnuART.Enabled = False
                    mnuERA.Enabled = False
                    mnuPayments.Enabled = False
                    mnuStatement.Enabled = False
                    mnuCompReimburse.Enabled = False
                    mnuChargesPayments.Enabled = False
                    mnuPBReport.Enabled = False
                    mnuChargesByClients.Enabled = False
                    mnuPmtByClients.Enabled = False
                    mnuPmtsCollectedByPaymentDate.Enabled = False
                    mnuEbillout.Enabled = False
                    mnuBillOut.Enabled = False
                    mnuBatchBill.Enabled = False
                    mnuBillEdit.Enabled = False
                    mnuBarAdhoc.Enabled = False
                    mnuPostBill.Enabled = False
                    mnuImportNecessity.Enabled = False
                    mnuUpdateRoster.Enabled = False
                    mnuBillReqs.Enabled = False
                    mnuScrubber.Enabled = False
                    mnuPreAuth.Enabled = False
                    mnuQuickQuote.Enabled = False
                End If
                '
                ExecuteSqlProcedure("Update SystemStations Set LastRunTime = '" & Format(Date.Today,
                SystemConfig.DateFormat) & "', LastUserID = " & ThisUser.ID & " where MacName = '" & SEAT & "'")
                lblUser.Text = ThisUser.Name & " on " & SEAT
                '
                If ThisUser.Cus_Svc And ThisUser.Supervisor Then
                    Dim HasPanic As Boolean = RunPanicAlert()
                    If HasPanic Then Blink(True, "Panic Management Routine requires your attention")
                End If
                '
                If ThisUser.LogoutMins > 0 Then
                    ALOTIMER = New Timer
                    ALOTIMER.Interval = 60000
                    ALOTIMER.Enabled = True
                    ALOTIMER.Start()
                    ALO = New AutoLogout
                    ALO.maxNumberMinutesIdle = ThisUser.LogoutMins
                    ALO.WatchControl(Me)
                End If
                '**** Temp ****************
                'If CN IsNot Nothing AndAlso CN.State <> 0 Then
                '    CN.Close()
                '    CN.Open(connstring)
                'Else
                '    CN = New ADODB.Connection
                '    CN.Open(connstring)
                'End If
                '**************************
                UpsertUser_Event(ThisUser.ID, 101, Date.Now, "USER", ThisUser.ID, "", "")   'Login
            Else
                MsgBox("License validation failed. Contact Prolis Support", MsgBoxStyle.Critical, "Prolis")
                'Application.Exit()
            End If
        Else
            MsgBox("License validation failed. Contact Prolis Support", MsgBoxStyle.Critical, "Prolis")
        End If
    End Sub

    Private Sub Blink(ByVal OnOff As Boolean, ByVal Msg As String)
        lblAlert.Text = Msg
        If OnOff = True Then
            Timer1.Start()
        Else
            Timer1.Stop()
            lblAlert.Visible = False
        End If
    End Sub

    Private Sub mnuLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLogin.Click
        frmLogin.ShowDialog()
    End Sub

    Public Sub Logout()
        Dim frm As Form
        For Each frm In Me.MdiChildren
            frm.Close()
        Next
        frmLogin.Close()
        ThisUser.Accession = Nothing
        ThisUser.ARC = Nothing
        ThisUser.Billing = Nothing
        ThisUser.Cus_Svc = Nothing
        ThisUser.Supervisor = Nothing
        ThisUser.Director = Nothing
        ThisUser.DicOnFly = Nothing
        ThisUser.Dictionary = Nothing
        ThisUser.Equips = Nothing
        ThisUser.Hard_Deletion = Nothing
        ThisUser.ID = Nothing
        ThisUser.LogoutMins = Nothing
        ThisUser.Insurances = Nothing
        ThisUser.Interfaces = Nothing
        ThisUser.IsLoggedIn = SetDBLogout(ThisUser.ID)
        ThisUser.Name = Nothing
        ThisUser.Password = Nothing
        ThisUser.Payment = Nothing
        ThisUser.Pouring = Nothing
        ThisUser.QC_Layout = Nothing
        ThisUser.Report_Build = Nothing
        ThisUser.Report_Process = Nothing
        ThisUser.Result_Entry = Nothing
        ThisUser.Result_Release = Nothing
        ThisUser.Soft_Deletion = Nothing
        ThisUser.System_Config = Nothing
        ThisUser.Testing = Nothing
        ThisUser.Test_Mgmt = Nothing
        ThisUser.User_Mgmt = Nothing
        '
        btnLogin.Enabled = True
        mnuLogin.Enabled = True
        btnLogout.Enabled = False
        mnuLogout.Enabled = False
        StatusStrip.Text = ""
        'txtMessage.Text = "Message for You"
        mnuUserMgmt.Enabled = False
        mnuAnalyteManagement.Enabled = False
        mnuRequisitions.Enabled = False
        mnuRemoteAcc.Enabled = False
        mnuAutoAccessions.Enabled = False
        mnuPrintLabels.Enabled = False
        mnuPreAnalytics.Enabled = False
        mnuPrintLog.Enabled = False
        mnuAccDash.Enabled = False
        btnRequisitions.Enabled = False
        mnuOrders.Enabled = False
        mnuInitialize.Enabled = False
        mnuPrintSendouts.Enabled = False
        mnuPhlebotomy.Enabled = False
        mnuQCMgmt.Enabled = False
        mnuBatch.Enabled = False
        mnuProcess.Enabled = False
        mnuResultDash.Enabled = False
        mnuApplyRes.Enabled = False
        DeltaCheckReqReultsToolStripMenuItem.Enabled = False
        mnuATRResults.Enabled = False
        mnuApplyResRefLab.Enabled = False
        mnuEnterRes.Enabled = False
        'mnuExtendResults.Enabled = False
        mnuBatchResults.Enabled = False
        mnuRefluxResults.Enabled = False
        mnuGroups.Enabled = False
        mnuProfiles.Enabled = False
        mnuAnalysisSetup.Enabled = False
        mnuCompRpts.Enabled = False
        mnuClientRpts.Enabled = False
        mnuPayerMgmt.Enabled = False
        mnuPayerMapping.Enabled = False
        mnuPartners.Enabled = False
        mnuPayerRpt.Enabled = False
        mnuPartnerRPT.Enabled = False
        mnuProviders.Enabled = False
        mnuSales.Enabled = False
        mnuRoutes.Enabled = False
        mnuEditPat.Enabled = False
        mnuRemDupPat.Enabled = False
        mnuOutSource.Enabled = False
        mnuFacility.Enabled = False
        mnuDept.Enabled = False
        mnuEquips.Enabled = False
        mnuSysConfig.Enabled = False
        mnuRptBuild.Enabled = False
        mnuRptOrder.Enabled = False
        mnuResReports.Enabled = False
        mnuQCReports.Enabled = False
        mnuInternalReports.Enabled = False
        mnuEResults.Enabled = False
        mnuMyReports.Enabled = False
        mnuMyRptSchedule.Enabled = False
        mnuAdhoc.Enabled = False
        mnuRptDash.Enabled = False
        mnuPublish.Enabled = False
        mnuProlisOnControl.Enabled = False
        btnReports.Enabled = False
        btnResultEntry.Enabled = False
        mnuWorksheets.Enabled = False
        mnuPhrases.Enabled = False
        mnuDxs.Enabled = False
        mnuCPTs.Enabled = False
        mnuART.Enabled = False
        mnuERA.Enabled = False
        mnuPayments.Enabled = False
        mnuDebits.Enabled = False
        '*********AR Reports ***************
        mnuStatement.Enabled = False
        mnuCompReimburse.Enabled = False
        mnuChargesPayments.Enabled = False
        mnuPBReport.Enabled = False
        mnuChargesByClients.Enabled = False
        mnuPmtByClients.Enabled = False
        mnuPmtsCollectedByPaymentDate.Enabled = False
        '***********************************
        mnuBillOut.Enabled = False
        mnuEbillout.Enabled = False
        mnuBatchBill.Enabled = False
        mnuBillEdit.Enabled = False
        mnuBarAdhoc.Enabled = False
        mnuPostBill.Enabled = False
        mnuBillDataSynch.Enabled = False
        mnuSuperBills.Enabled = False
        mnuImportNecessity.Enabled = False
        mnuImportPrices.Enabled = False
        mnuUpdateRoster.Enabled = False
        mnuUpdateMileageCode.Enabled = False
        mnuCreditCards.Enabled = False
        mnuBillReqs.Enabled = False
        mnuResInq.Enabled = False
        mnuMsg.Enabled = False
        mnuTestInq.Enabled = False
        mnuProvInquiry.Enabled = False
        mnuPickup.Enabled = False
        mnuPickupRpt.Enabled = False
        mnuARInq.Enabled = False
        mnuPrintReqs.Enabled = False
        mnuPanicMgmt.Enabled = False
        mnuPanicHistory.Enabled = False
        mnuPrintPanicReport.Enabled = False
        mnuScrubber.Enabled = False
        mnuPreAuth.Enabled = False
        mnuQuickQuote.Enabled = False
        mnuWorksetup.Enabled = False
        mnuSendOuts.Enabled = False
        '
        lblUser.Text = ""
        '
        UpsertUser_Event(ThisUser.ID, 103, Date.Now, "USER", ThisUser.ID, "", "")   'Logout
        '
        Timer1.Stop()
        lblAlert.Visible = False
        '
        ALO = Nothing
        If Not ALOTIMER Is Nothing Then
            ALOTIMER.Stop()
            ALOTIMER = Nothing
        End If
        If LIC IsNot Nothing Then LIC = Nothing
    End Sub

    Private Function SetDBLogout(ByVal User_ID As Long) As Boolean
        Dim Loged As Boolean = True
        Try
            ExecuteSqlProcedure("Update Users set IsLoggedIn = 0 where ID = " & User_ID)
            Loged = False
        Catch ex As Exception
        End Try
        Return Loged
    End Function

    Private Function SetDBLogIn(ByVal User_ID As Long) As Boolean
        ExecuteSqlProcedure("Update Users set IsLoggedIn = 'True' where ID = " & User_ID)
        SetDBLogIn = True
    End Function
    Public Sub btnLogout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogout.Click
        Logout()
    End Sub

    Private Sub mnuLogout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLogout.Click
        Logout()
    End Sub

    Private Sub mnuAnalyteManagement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAnalyteManagement.Click

        ShowForm(GetType(frmTests))
        ShowForm(GetType(frmDYMOtesting))
        '' Check if the form is already open
        'For Each frm As Form In Me.MdiChildren
        '    If TypeOf frm Is frmTests Then
        '        frm.BringToFront()
        '        Return
        '    End If
        'Next

        'frmTests.MdiParent = Me
        'frmTests.Show()
    End Sub

    Private Sub mnuUserMgmt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUserMgmt.Click
        'frmUserMgmt.Show()
        'frmUserMgmt.MdiParent = Me
        ShowForm(GetType(frmUserMgmt))
    End Sub

    Private Sub btnRequisitions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRequisitions.Click
        'frmRequisitions.Show()
        'frmRequisitions.MdiParent = Me

        ShowForm(GetType(frmRequisitions))
    End Sub
    Private Sub mnuRequisitions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRequisitions.Click
        'frmRequisitions.Show()
        'frmRequisitions.MdiParent = Me

        ShowForm(GetType(frmRequisitions))
        'frmRequisitions.StartPosition = FormStartPosition.CenterScreen
    End Sub

    Private Sub mnuGroups_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGroups.Click
        'frmGrps.Show()
        'frmGrps.MdiParent = Me
        ShowForm(GetType(frmGroups))
    End Sub

    Private Sub mnuProfiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuProfiles.Click
        'frmProfiles.Show()
        'frmProfiles.MdiParent = Me
        ShowForm(GetType(frmProfiles))
    End Sub

    Private Sub mnuAnalysisSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAnalysisSetup.Click
        'frmRunSetup.Show()
        'frmRunSetup.MdiParent = Me
        ShowForm(GetType(frmRunSetup))
    End Sub

    Private Sub mnuBatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBatch.Click
        'frmBatches.Show()
        'frmBatches.MdiParent = Me

        ShowForm(GetType(frmBatches))
    End Sub

    Private Sub mnuEquips_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEquips.Click
        'frmEquipments.Show()
        'frmEquipments.MdiParent = Me
        ShowForm(GetType(frmEquipments))
    End Sub

    Private Sub mnuEnterRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEnterRes.Click
        'frmResults.Show()
        'frmResults.MdiParent = Me
        ShowForm(GetType(frmResults))
    End Sub

    Private Sub btnResultEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResultEntry.Click
        'frmResults.Show()
        'frmResults.MdiParent = Me
        ShowForm(GetType(frmResults))
    End Sub

    Private Sub mnuResReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuResReports.Click
        'frmResultReports.Show()
        'frmResultReports.MdiParent = Me

        ShowForm(GetType(frmResultReports))
    End Sub

    Private Sub mnuFacility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFacility.Click
        'frmCompany.Show()
        'frmCompany.MdiParent = Me

        ShowForm(GetType(frmCompany))
    End Sub

    Private Sub btnReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReports.Click
        'frmResultReports.Show()
        'frmResultReports.MdiParent = Me

        ShowForm(GetType(frmResultReports))
    End Sub

    Private Sub mnuProviders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuProviders.Click
        'frmProviders.Show()
        'frmProviders.MdiParent = Me

        ShowForm(GetType(frmProviders))
    End Sub

    Private Sub mnuSales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSales.Click
        'frmSalesPersons.Show()
        'frmSalesPersons.MdiParent = Me

        ShowForm(GetType(frmSalesPersons))
    End Sub

    Private Sub mnuRoutes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRoutes.Click
        'frmRoutes.Show()
        'frmRoutes.MdiParent = Me

        ShowForm(GetType(frmRoutes))
    End Sub

    Private Sub mnuRemoteAcc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRemoteAcc.Click

        'frmRemoteAcc.Show()
        'frmRemoteAcc.MdiParent = Me

        ShowForm(GetType(frmRemoteAcc))
    End Sub

    Private Sub mnuResInq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuResInq.Click
        'frmResInq.Show()
        'frmResInq.MdiParent = Me

        ShowForm(GetType(frmResInq))
    End Sub

    Private Sub mnuTestInq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTestInq.Click
        'frmCompInq.Show()
        'frmCompInq.MdiParent = Me

        ShowForm(GetType(frmCompInq))
    End Sub

    Private Sub mnuBillOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBillSep1.Click
        'frmBillOut.Show()
        'frmBillOut.MdiParent = Me

        ShowForm(GetType(frmBillOut))


    End Sub

    Private Sub mnuSysConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSysConfig.Click
        'frmSystemConfig.Show()
        'frmSystemConfig.MdiParent = Me

        ShowForm(GetType(frmSystemConfig))
    End Sub

    Private Sub mnuPrintLabels_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrintLabels.Click
        'frmPrintLabels.Show()
        'frmPrintLabels.MdiParent = Me

        ShowForm(GetType(frmPrintLabels))
    End Sub

    Private Sub mnuPrintLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrintLog.Click
        'frmPrintAccLog.Show()
        'frmPrintAccLog.MdiParent = Me

        ShowForm(GetType(frmPrintAccLog))
    End Sub

    Private Sub mnuDept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDept.Click
        'frmDepts.Show()
        'frmDepts.MdiParent = Me

        ShowForm(GetType(frmDepts))
    End Sub

    Private Sub mnuQCReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuQCReports.Click
        'frmPrintQCReports.Show()
        'frmPrintQCReports.MdiParent = Me

        ShowForm(GetType(frmPrintQCReports))
    End Sub

    Private Sub QCExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QCExit.Click
        Application.Exit()
    End Sub

    Private Sub mnuPhrases_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPhrases.Click
        'frmPhrases.Show()
        'frmPhrases.MdiParent = Me

        ShowForm(GetType(frmPhrases))

    End Sub

    Private Sub mnuBatchBill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBatchBill.Click
        'frmBillingDash.Show()
        'frmBillingDash.MdiParent = Me

        ShowForm(GetType(frmBillingDash))

    End Sub

    Private Sub mnuBillEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBillEdit.Click
        'frmBillingEdit.Show()
        'frmBillingEdit.MdiParent = Me

        ShowForm(GetType(frmBillingEdit))

    End Sub

    Private Sub mnuBillOut_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBillOut.Click
        'frmBillOut.Show()
        'frmBillOut.MdiParent = Me

        ShowForm(GetType(frmBillOut))

    End Sub

    Private Sub mnuPBReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPBReport.Click
        'frmPrintIntegrity.Show()
        'frmPrintIntegrity.MdiParent = Me

        ShowForm(GetType(frmPrintIntegrity))

    End Sub

    Private Sub mnuSorksheets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuWorksheets.Click
        'frmPrintWorksheets.Show()
        'frmPrintWorksheets.MdiParent = Me

        ShowForm(GetType(frmPrintWorksheets))

    End Sub

    Private Sub mnuApplyRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuApplyRes.Click
        'frmApplyRes.Show()
        'frmApplyRes.MdiParent = Me

        ShowForm(GetType(frmApplyRes))

    End Sub

    Private Sub mnuPrintReqs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrintReqs.Click
        'frmPrintReqs.Show()
        'frmPrintReqs.MdiParent = Me

        ShowForm(GetType(frmPrintReqs))

    End Sub

    Private Sub mnuMyReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMyReports.Click
        'frmMyReports.Show()
        'frmMyReports.MdiParent = Me

        ShowForm(GetType(frmMyReports))

    End Sub

    Private Sub mnuCompRpts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCompRpts.Click
        'frmComponentReports.Show()
        'frmComponentReports.MdiParent = Me

        ShowForm(GetType(frmComponentReports))

    End Sub

    Private Sub mnuClientRpts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuClientRpts.Click
        'frmClientReports.Show()
        'frmClientReports.MdiParent = Me

        ShowForm(GetType(frmClientReports))
    End Sub

    Private Sub mnuPayerMgmt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPayerMgmt.Click
        'frmPayers.Show()
        'frmPayers.MdiParent = Me

        ShowForm(GetType(frmPayers))

    End Sub

    Private Sub mnuPayerRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPayerRpt.Click
        'frmPayerRpt.Show()
        'frmPayerRpt.MdiParent = Me

        ShowForm(GetType(frmPayerRpt))

    End Sub

    Private Sub mnuPartners_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPartners.Click
        'frmPartners.Show()
        'frmPartners.MdiParent = Me

        ShowForm(GetType(frmPartners))
    End Sub

    Private Sub mnuPartnerRPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPartnerRPT.Click
        'frmPartnerRpt.Show()
        'frmPartnerRpt.MdiParent = Me

        ShowForm(GetType(frmPartnerRpt))
    End Sub

    Private Sub mnuEbillout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEbillout.Click
        'frmEBillOut.Show()
        'frmEBillOut.MdiParent = Me

        ShowForm(GetType(frmEBillOut))
    End Sub

    Private Sub mnuPayments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPayments.Click
        'frmPayments.Show()
        'frmPayments.MdiParent = Me

        ShowForm(GetType(frmPayments))
    End Sub

    Private Sub mnuPickup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPickup.Click
        'frmPickUpMgmt.Show()
        'frmPickUpMgmt.MdiParent = Me

        ShowForm(GetType(frmPickUpMgmt))
    End Sub



    Private Sub mnuPickupRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPickupRpt.Click
        'frmPrintPickUp.Show()
        'frmPrintPickUp.MdiParent = Me

        ShowForm(GetType(frmPrintPickUp))
    End Sub

    Private Sub mnuBatchResults_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBatchResults.Click
        'frmBatchResults.Show()
        'frmBatchResults.MdiParent = Me

        ShowForm(GetType(frmBatchResults))
    End Sub

    Private Sub mnuWorksetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuWorksetup.Click
        'frmWorkSetUp.Show()
        'frmWorkSetUp.MdiParent = Me

        ShowForm(GetType(frmWorkSetUp))
    End Sub

    Private Sub mnuARInq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuARInq.Click
        'frmMissingInfo.Show()
        'frmMissingInfo.MdiParent = Me

        ShowForm(GetType(frmMissingInfo))
    End Sub

    Private Sub mnuPostBill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPostBill.Click
        'frmPostBill.Show()
        'frmPostBill.MdiParent = Me

        ShowForm(GetType(frmPostBill))
    End Sub

    Private Sub mnuSendOuts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSendOuts.Click
        'frmSendOuts.Show()
        'frmSendOuts.MdiParent = Me

        ShowForm(GetType(frmSendOuts))
    End Sub

    Private Sub mnuAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAbout.Click
        'frmAbout.Show()
        'frmAbout.MdiParent = Me

        ShowForm(GetType(frmAbout))
    End Sub

    Private Sub mnuSupport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSupport.Click
        System.Diagnostics.Process.Start("http://www.fastsupport.com")
    End Sub

    Private Sub mnuOutSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOutSource.Click
        'frmLabMgmt.Show()
        'frmLabMgmt.MdiParent = Me

        ShowForm(GetType(frmLabMgmt))
    End Sub

    Private Sub mnuAccDash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAccDash.Click
        'frmAccDash.Show()
        'frmAccDash.MdiParent = Me

        ShowForm(GetType(frmAccDash))
    End Sub

    Private Sub mnuOrders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOrders.Click
        'frmOrders.Show()
        'frmOrders.MdiParent = Me

        ShowForm(GetType(frmOrders))
    End Sub

    Private Sub mnuRptBuild_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRptBuild.Click
        'frmRptBuild.Show()
        'frmRptBuild.MdiParent = Me

        ShowForm(GetType(frmRptBuild))
    End Sub

    Private Sub mnuRptOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRptOrder.Click
        'frmReportOrder.Show()
        'frmReportOrder.MdiParent = Me

        ShowForm(GetType(frmReportOrder))
    End Sub

    Private Sub mnuRptDash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRptDash.Click
        'frmRptDash.Show()
        'frmRptDash.MdiParent = Me

        ShowForm(GetType(frmRptDash))
    End Sub

    Private Sub mnuProvInquiry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuProvInquiry.Click
        'frmProvInquiry.Show()
        'frmProvInquiry.MdiParent = Me

        ShowForm(GetType(frmProvInquiry))
    End Sub

    Private Sub mnuATRResults_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuATRResults.Click
        'frmATRResults.Show()
        'frmATRResults.MdiParent = Me

        ShowForm(GetType(frmATRResults))
    End Sub

    Private Sub mnuPrintSendouts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrintSendouts.Click
        'frmPrintSendouts.Show()
        'frmPrintSendouts.MdiParent = Me

        ShowForm(GetType(frmPrintSendouts))
    End Sub

    Private Sub mnuPrintPanicReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrintPanicReport.Click
        'Dim RPT As String = Application.StartupPath & "\Reports\PanicResults.RPT"
        'Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        'oRpt.Load(RPT)
        'ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)
        'frmRV.CRRV.ReportSource = oRpt
        'frmRV.Show()
        'frmRV.MdiParent = Me
    End Sub

    Private Sub mnuPanicMgmt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPanicMgmt.Click
        'frmManagePanics.Show()
        'frmManagePanics.MdiParent = Me

        ShowForm(GetType(frmManagePanics))
    End Sub

    Private Sub mnuBillDataSynch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBillDataSynch.Click
        'frmBillingSynch.Show()
        'frmBillingSynch.MdiParent = Me

        ShowForm(GetType(frmBillingSynch))
    End Sub

    Private Sub mnuImportNecessity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuImportNecessity.Click
        'frmImportNec.Show()
        'frmImportNec.MdiParent = Me

        ShowForm(GetType(frmImportNec))
    End Sub

    Private Sub mnuApplyResRefLab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuApplyResRefLab.Click
        'frmApplyLabResults.Show()
        'frmApplyLabResults.MdiParent = Me

        ShowForm(GetType(frmApplyLabResults))
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If lblAlert.Visible = False Then
            lblAlert.Visible = True
        Else
            lblAlert.Visible = False
        End If
    End Sub

    Friend Sub lblAlert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblAlert.Click
        Timer1.Stop()
        lblAlert.Visible = False
        'frmManagePanics.Show()
        'frmManagePanics.MdiParent = Me

        ShowForm(GetType(frmManagePanics))
    End Sub

    Private Sub mnuEditPat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEditPat.Click
        'frmPatient.Show()
        'frmPatient.MdiParent = Me

        ShowForm(GetType(frmPatient))
    End Sub

    Private Sub mnuRemDupPat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRemDupPat.Click
        'frmMergePats.Show()
        'frmMergePats.MdiParent = Me

        ShowForm(GetType(frmMergePats))
    End Sub

    Private Sub mnuBillReqs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBillReqs.Click
        'frmBillReqs.Show()
        'frmBillReqs.MdiParent = Me

        ShowForm(GetType(frmBillReqs))
    End Sub

    Private Sub mnuPhlebotomy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPhlebotomy.Click
        'Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        'oRpt.Load(Application.StartupPath & "\Reports\Phlebotomy.rpt")
        'ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database,
        'My.Settings.UID, My.Settings.PWD)
        'frmRV.CRRV.ReportSource = oRpt
        'frmRV.MdiParent = Me
        'frmRV.Show()
    End Sub

    'Private Sub mnuBill2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    frmBillingEdit2.Show()
    '    frmBillingEdit2.MdiParent = Me
    'End Sub

    Private Sub mnuART_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuART.Click
        'frmARInquiry.Show()
        'frmARInquiry.MdiParent = Me

        ShowForm(GetType(frmARInquiry))
    End Sub

    Private Sub mnuSuperBills_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSuperBills.Click
        'frmExportSuperBills.Show()
        'frmExportSuperBills.MdiParent = Me

        ShowForm(GetType(frmExportSuperBills))
    End Sub

    Private Sub mnuPreAnalytics_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPreAnalytics.Click
        'frmPreanalytics.Show()
        'frmPreanalytics.MdiParent = Me

        ShowForm(GetType(frmPreanalytics))
    End Sub

    Private Sub mnuProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuProcess.Click
        'frmProcessing.Show()
        'frmProcessing.MdiParent = Me

        ShowForm(GetType(frmProcessing))
    End Sub

    Private Sub mnuScrubber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuScrubber.Click
        'frmScrubber.Show()
        'frmScrubber.MdiParent = Me

        ShowForm(GetType(frmScrubber))
    End Sub

    Private Sub mnuQCMgmt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuQCMgmt.Click
        'frmQCManagement.Show()
        'frmQCManagement.MdiParent = Me

        ShowForm(GetType(frmQCManagement))
    End Sub

    Private Sub mnuQuickQuote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuQuickQuote.Click
        'frmQuickQuote.Show()
        'frmQuickQuote.MdiParent = Me

        ShowForm(GetType(frmQuickQuote))
    End Sub

    Private Sub mnuAdhoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAdhoc.Click
        'frmAdhocQuery.Show()
        'frmAdhocQuery.MdiParent = Me

        ShowForm(GetType(frmAdhocQuery))
    End Sub

    'Private Sub mnuExtendResults_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    frmExtendResults.Show()
    '    frmExtendResults.MdiParent = Me
    'End Sub

    Private Sub mnuDxs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDxs.Click
        'frmDiagnosis.Show()
        'frmDiagnosis.MdiParent = Me

        ShowForm(GetType(frmDiagnosis))
    End Sub

    Private Sub mnuERA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuERA.Click
        'frmERA.Show()
        'frmERA.MdiParent = Me

        ShowForm(GetType(frmERA))
    End Sub

    Private Sub mnuResultDash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuResultDash.Click
        'frmResultDash.Show()
        'frmResultDash.MdiParent = Me

        ShowForm(GetType(frmResultDash))
    End Sub

    Private Sub mnuPayerMapping_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPayerMapping.Click
        'frmPayerMapping.Show()
        'frmPayerMapping.MdiParent = Me

        ShowForm(GetType(frmPayerMapping))
    End Sub

    Private Sub mnuInitialize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInitialize.Click
        'frmInstantiateOrders.Show()
        'frmInstantiateOrders.MdiParent = Me

        ShowForm(GetType(frmInstantiateOrders))
    End Sub

    Private Sub mnuDebits_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDebits.Click
        'frmRefund.Show()
        'frmRefund.MdiParent = Me

        ShowForm(GetType(frmRefund))
    End Sub

    Private Sub mnuPanicHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPanicHistory.Click
        'Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        'oRpt.Load(Application.StartupPath & "\Reports\Panic Notification History.rpt")
        'ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database,
        'My.Settings.UID, My.Settings.PWD)
        'frmRV.CRRV.ReportSource = oRpt
        'frmRV.MdiParent = Me
        'frmRV.Show()
    End Sub

    Private Sub mnunetHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnunetHelp.Click
        HelpProvider1.HelpNamespace = My.Application.Info.DirectoryPath & "\ProlisHelp.chm"
        HelpProvider1.SetHelpKeyword(Me, 1)
        HelpProvider1.SetHelpNavigator(Me, HelpNavigator.TopicId)
        Help.ShowHelp(Me, My.Application.Info.DirectoryPath & "\ProlisHelp.chm")
        'MsgBox("Context F1 Help is under development. Will be available soon.", MsgBoxStyle.Information, "Prolis")
    End Sub

    Private Sub mnuLocalHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLocalHelp.Click
        System.Diagnostics.Process.Start("http://prolishelp.americansoftsolutions.com")
        'MsgBox("Help documentation is under development. Will be available soon.", MsgBoxStyle.Information, "Prolis")
    End Sub

    Private Sub mnuStatement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuStatement.Click
        If IO.File.Exists(GetReportPath("Statement.rpt")) Then
            'Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            'oRpt.Load(GetReportPath("Statement.rpt"))
            'ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database,
            'My.Settings.UID, My.Settings.PWD)
            'frmRV.CRRV.ReportSource = oRpt
            'frmRV.MdiParent = Me
            'frmRV.Show()
        Else
            MsgBox("The statement report does not exist in the app directory. Please contact Prolis support.", MsgBoxStyle.Critical, "Prolis")
        End If
    End Sub

    Private Sub mnuMyRptSchedule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMyRptSchedule.Click
        'frmMyReportSchedule.Show()
        'frmMyReportSchedule.MdiParent = Me

        ShowForm(GetType(frmMyReportSchedule))
    End Sub

    Private Sub mnuCompReimburse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCompReimburse.Click
        If IO.File.Exists(GetReportPath("Component Reimbursement.rpt")) Then
            'Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            'oRpt.Load(GetReportPath("Component Reimbursement.rpt"))
            'ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database,
            'My.Settings.UID, My.Settings.PWD)
            'frmRV.CRRV.ReportSource = oRpt
            'frmRV.MdiParent = Me
            'frmRV.Show()
        Else
            MsgBox("The Component Reimbursement report does not exist in the app directory. Please contact Prolis support.", MsgBoxStyle.Critical, "Prolis")
        End If
    End Sub

    Private Sub mnuChargesPayments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuChargesPayments.Click
        If IO.File.Exists(GetReportPath("Charges and Payments.rpt")) Then
            'Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            'oRpt.Load(GetReportPath("Charges and Payments.rpt"))
            'ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database,
            'My.Settings.UID, My.Settings.PWD)
            'frmRV.CRRV.ReportSource = oRpt
            'frmRV.MdiParent = Me
            'frmRV.Show()
        Else
            MsgBox("The 'Charges and Payments' report does not exist in the app directory. Please contact Prolis support.", MsgBoxStyle.Critical, "Prolis")
        End If
    End Sub

    Private Sub mnuChargesByClients_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuChargesByClients.Click
        If IO.File.Exists(GetReportPath("Charges by Clients.rpt")) Then
            'Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            'oRpt.Load(GetReportPath("Charges by Clients.rpt"))
            'ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database,
            'My.Settings.UID, My.Settings.PWD)
            'frmRV.CRRV.ReportSource = oRpt
            'frmRV.MdiParent = Me
            'frmRV.Show()
        Else
            MsgBox("The 'Charges by Clients' report does not exist in the app directory. Please contact Prolis support.", MsgBoxStyle.Critical, "Prolis")
        End If
    End Sub

    Private Sub mnuPmtByClients_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPmtByClients.Click
        If IO.File.Exists(GetReportPath("Payment by Clients.rpt")) Then
            'Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            'oRpt.Load(GetReportPath("Payment by Clients.rpt"))
            'ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database,
            'My.Settings.UID, My.Settings.PWD)
            'frmRV.CRRV.ReportSource = oRpt
            'frmRV.MdiParent = Me
            'frmRV.Show()
        Else
            MsgBox("The 'Payment by Clients' report does not exist in the app directory. Please contact Prolis support.", MsgBoxStyle.Critical, "Prolis")
        End If
    End Sub

    Private Sub mnuPmtsCollected_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPmtsCollectedByPaymentDate.Click
        If IO.File.Exists(GetReportPath("Payments Collected.rpt")) Then
            'Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            'oRpt.Load(GetReportPath("Payments Collected.rpt"))
            'ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database,
            'My.Settings.UID, My.Settings.PWD)
            'frmRV.CRRV.ReportSource = oRpt
            'frmRV.MdiParent = Me
            'frmRV.Show()
        Else
            MsgBox("The 'Payments Collected' report does not exist in the app directory. Please contact Prolis support.", MsgBoxStyle.Critical, "Prolis")
        End If
    End Sub

    Private Sub mnuAccessionSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAccessionSummary.Click
        If IO.File.Exists(GetReportPath("Accession Summary.rpt")) Then
            'Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            'oRpt.Load(GetReportPath("Accession Summary.rpt"))
            'ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database,
            'My.Settings.UID, My.Settings.PWD)
            'frmRV.CRRV.ReportSource = oRpt
            'frmRV.MdiParent = Me
            'frmRV.Show()
        Else
            MsgBox("The 'Accession Summary' report does not exist in the app directory. Please contact Prolis support.", MsgBoxStyle.Critical, "Prolis")
        End If
    End Sub

    Private Sub mnuAccessionsRejected_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAccessionsRejected.Click
        If IO.File.Exists(GetReportPath("Accessions Rejected.rpt")) Then
            'Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            'oRpt.Load(GetReportPath("Accessions Rejected.rpt"))
            'ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database,
            'My.Settings.UID, My.Settings.PWD)
            'frmRV.CRRV.ReportSource = oRpt
            'frmRV.MdiParent = Me
            'frmRV.Show()
        Else
            MsgBox("The 'Accessions Rejected' report does not exist in the app directory. Please contact Prolis support.", MsgBoxStyle.Critical, "Prolis")
        End If
    End Sub

    Private Sub mnuPreAuth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPreAuth.Click
        'frmPreAuth.Show()
        'frmPreAuth.MdiParent = Me

        ShowForm(GetType(frmPreAuth))
    End Sub

    Private Sub mnuImportPrices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuImportPrices.Click
        'frmImportPrices.Show()
        'frmImportPrices.MdiParent = Me

        ShowForm(GetType(frmImportPrices))
    End Sub

    Private Sub frmDashboard_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        Me.Refresh()
    End Sub

    Private Sub MdiContainerForm_Resize(sender As Object, e As EventArgs) Handles Me.Resize

        'On Error Resume Next
        ' Maintain the aspect ratio of the background image
        Try
            Dim bgImage As Image = Me.BackgroundImage

            If bgImage IsNot Nothing Then
                ' Calculate the aspect ratio of the image
                Dim aspectRatio As Single = bgImage.Width / bgImage.Height

                ' Calculate the new dimensions
                Dim newWidth As Integer = Me.ClientSize.Width
                Dim newHeight As Integer = CInt(newWidth / aspectRatio)

                ' If the calculated height is greater than the form's client height, adjust accordingly
                If newHeight > Me.ClientSize.Height Then
                    newHeight = Me.ClientSize.Height
                    newWidth = CInt(newHeight * aspectRatio)
                End If

                ' Set the size of the background image
                Dim rect As New Rectangle(0, 0, newWidth, newHeight)
                Me.BackgroundImage = New Bitmap(bgImage, rect.Size)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub mnuBarAdhoc_Click(sender As Object, e As EventArgs) Handles mnuBarAdhoc.Click
        'frmBARAdhoc.Show()
        'frmBARAdhoc.MdiParent = Me

        ShowForm(GetType(frmBARAdhoc))
    End Sub

    Private Sub mnuUpdateRoster_Click(sender As Object, e As EventArgs) Handles mnuUpdateRoster.Click
        'frmUpdateRoster.Show()
        'frmUpdateRoster.MdiParent = Me

        ShowForm(GetType(frmUpdateRoster))
    End Sub

    Private Sub mnuUpdateMileageCode_Click(sender As Object, e As EventArgs) Handles mnuUpdateMileageCode.Click
        'frmUpdateMileage.Show()
        'frmUpdateMileage.MdiParent = Me

        ShowForm(GetType(frmUpdateMileage))
    End Sub

    Private Sub mnuCreditCards_Click(sender As Object, e As EventArgs) Handles mnuCreditCards.Click
        System.Diagnostics.Process.Start("https://go.cardknox.com/prolis")
    End Sub

    Private Sub mnuAutoAccessions_Click(sender As Object, e As EventArgs) Handles mnuAutoAccessions.Click
        'frmAutoAccession.Show()
        'frmAutoAccession.MdiParent = Me

        ShowForm(GetType(frmAutoAccession))
    End Sub

    Private Sub DeltaCheckReqReultsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeltaCheckReqReultsToolStripMenuItem.Click
        'frmATRResults_Delta.Show()
        'frmATRResults_Delta.MdiParent = Me

        ShowForm(GetType(frmATRResults_Delta))
    End Sub

    Private Sub mnuCPTs_Click(sender As Object, e As EventArgs) Handles mnuCPTs.Click
        'frmCPT.Show()
        'frmCPT.MdiParent = Me

        ShowForm(GetType(frmCPT))
    End Sub

    Private Sub ProcessedERAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProcessedERAToolStripMenuItem.Click
        'ERA_Processed.Show()
        'ERA_Processed.MdiParent = Me

        ShowForm(GetType(frmERA_Processed))
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs)
        Try
            'DymoAddIn = New Dymo.DymoAddIn
            'DymoLabel = New Dymo.DymoLabels
        Catch ex As Exception

        End Try
    End Sub

    Private Sub mnuMsg_Click(sender As Object, e As EventArgs) Handles mnuMsg.Click

        ShowForm(GetType(frmChatWithList))

        'frmChatWithList.txtUserID.Text = ThisUser.ID
        'frmChatWithList.txtUserName.Text = ThisUser.UserName
    End Sub

    Private Sub ShowForm(formType As Type)
        Dim formName As String = formType.Name
        Dim formInstance As Form = Application.OpenForms(formName)

        If formInstance Is Nothing Then
            formInstance = DirectCast(Activator.CreateInstance(formType), Form)
            formInstance.MdiParent = Me
            formInstance.StartPosition = FormStartPosition.CenterScreen
            formInstance.Show()
            AddToolStripButton(formInstance)
        Else
            formInstance.BringToFront()
        End If
    End Sub
    Private Sub AddToolStripButton(form As Form)
        Dim button As New ToolStripButton()
        button.Text = form.Text
        button.Tag = form
        button.Image = form.Icon.ToBitmap()
        button.TextImageRelation = TextImageRelation.ImageBeforeText ' Ensures text is to the right of the image
        button.DisplayStyle = ToolStripItemDisplayStyle.Image  ' Ensures both image and text are displayed
        button.TextAlign = ContentAlignment.MiddleLeft ' Ensures text is aligned to the left within the button
        button.ImageAlign = ContentAlignment.MiddleLeft ' Ensures image is aligned to the left within the button
        AddHandler button.Click, AddressOf ToolStripButton_Click
        AddHandler button.MouseUp, AddressOf ToolStripButton_MouseUp ' Handle right-clicks
        ToolStrip.Items.Add(button)
        AddHandler form.FormClosed, AddressOf Form_FormClosed
    End Sub

    Private Sub ToolStripButton_Click(sender As Object, e As EventArgs)
        Dim button As ToolStripButton = DirectCast(sender, ToolStripButton)
        Dim form As Form = DirectCast(button.Tag, Form)
        form.BringToFront()
    End Sub

    Private Sub CloseMenuItem_Click(sender As Object, e As EventArgs)
        Dim clickedItem As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        Dim contextMenu As ContextMenuStrip = DirectCast(clickedItem.Owner, ContextMenuStrip)
        Dim button As ToolStripButton = DirectCast(contextMenu.Tag, ToolStripButton)
        Dim form As Form = DirectCast(button.Tag, Form)
        form.Close()
    End Sub
    Private Sub ToolStripButton_MouseUp(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Right Then
            Dim button As ToolStripButton = DirectCast(sender, ToolStripButton)
            closeContextMenu.Tag = button ' Store the clicked button
            closeContextMenu.Show(Cursor.Position)
        End If
    End Sub

    Private Sub Form_FormClosed(sender As Object, e As FormClosedEventArgs)
        Dim form As Form = DirectCast(sender, Form)
        For Each item As ToolStripItem In ToolStrip.Items
            If TypeOf item Is ToolStripButton AndAlso item.Tag Is form Then
                ToolStrip.Items.Remove(item)
                Exit For
            End If
        Next
    End Sub

    Private closeContextMenu As ContextMenuStrip
    Private closeMenuItem As ToolStripMenuItem

    Private Sub InitializeContextMenu()
        closeContextMenu = New ContextMenuStrip()
        closeMenuItem = New ToolStripMenuItem("Close")
        AddHandler closeMenuItem.Click, AddressOf CloseMenuItem_Click
        closeContextMenu.Items.Add(closeMenuItem)
    End Sub

    Private Sub mnuRefluxResults_Click(sender As Object, e As EventArgs) Handles mnuRefluxResults.Click
        ShowForm(GetType(frmRefluxResultReport))
    End Sub

    Private Sub ToolStrip_MouseEnter(sender As Object, e As EventArgs) Handles ToolStrip.MouseEnter
        ToolStrip.Width = 200
        ShowButtonText(ToolStrip, True)

    End Sub

    Private Sub ToolStrip_MouseLeave(sender As Object, e As EventArgs) Handles ToolStrip.MouseLeave
        ToolStrip.Width = 50
        ShowButtonText(ToolStrip, False)
    End Sub

    Private Sub ShowButtonText(toolStrip As ToolStrip, show As Boolean)
        For Each item As ToolStripItem In toolStrip.Items
            If TypeOf item Is ToolStripButton Then
                Dim button As ToolStripButton = DirectCast(item, ToolStripButton)
                'button.Text = ""
                If show = True Then
                    button.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText  ' Ensures both image and text are displayed
                Else
                    button.DisplayStyle = ToolStripItemDisplayStyle.Image  ' Ensures both image and text are displayed
                End If
            End If
        Next
    End Sub

    Private Sub SetDoubleBuffered(control As Control, value As Boolean)
        Dim pi As System.Reflection.PropertyInfo =
            GetType(Control).GetProperty("DoubleBuffered",
            System.Reflection.BindingFlags.NonPublic Or
            System.Reflection.BindingFlags.Instance)
        pi.SetValue(control, value, Nothing)
    End Sub

    Private Sub PaymentsCollectedByPostedDatesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnuPaymentsCollectedByPostedDates.Click
        If IO.File.Exists(GetReportPath("Payments Collected by Posted Date.rpt")) Then
            'Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            'oRpt.Load(GetReportPath("Payments Collected by Posted Date.rpt"))
            'ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database,
            'My.Settings.UID, My.Settings.PWD)
            'frmRV.CRRV.ReportSource = oRpt
            'frmRV.MdiParent = Me
            'frmRV.Show()
        Else
            MsgBox("The 'Payments Collected by Posted Date' report does not exist in the app directory. Please contact Prolis support.", MsgBoxStyle.Critical, "Prolis")
        End If
    End Sub

    Private Sub RequisitionCopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RequisitionCopyToolStripMenuItem.Click
        'TempComment-ShowForm(GetType(frmRequisitions_Copy))
    End Sub


    Private Sub UpdateResultNoteInTestsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateResultNoteInTestsToolStripMenuItem.Click

        ConvertAndUpdateRTF()


    End Sub

    Private Sub TestingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TestingToolStripMenuItem.Click
        'TempComment-ShowForm(GetType(Form1))

    End Sub
End Class