Imports System.IO

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDashboard
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub


    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDashboard))
        MenuStrip = New MenuStrip()
        YouMenu = New ToolStripMenuItem()
        mnuLogin = New ToolStripMenuItem()
        mnuLogout = New ToolStripMenuItem()
        ToolStripSeparator3 = New ToolStripSeparator()
        QCExit = New ToolStripMenuItem()
        mnuCS = New ToolStripMenuItem()
        mnuResInq = New ToolStripMenuItem()
        mnuMsg = New ToolStripMenuItem()
        mnuTestInq = New ToolStripMenuItem()
        mnuProvInquiry = New ToolStripMenuItem()
        ToolStripMenuItem3 = New ToolStripSeparator()
        mnuPickup = New ToolStripMenuItem()
        mnuPickupRpt = New ToolStripMenuItem()
        ToolStripMenuItem4 = New ToolStripSeparator()
        mnuARInq = New ToolStripMenuItem()
        mnuScrubber = New ToolStripMenuItem()
        mnuPreAuth = New ToolStripMenuItem()
        ToolStripSeparator6 = New ToolStripSeparator()
        mnuPrintReqs = New ToolStripMenuItem()
        mnuManagePanics = New ToolStripMenuItem()
        mnuPanicMgmt = New ToolStripMenuItem()
        mnuPrintPanicReport = New ToolStripMenuItem()
        mnuPanicHistory = New ToolStripMenuItem()
        ToolStripSeparator18 = New ToolStripSeparator()
        mnuQuickQuote = New ToolStripMenuItem()
        mnuAccession = New ToolStripMenuItem()
        mnuRequisitions = New ToolStripMenuItem()
        mnuRemoteAcc = New ToolStripMenuItem()
        mnuAutoAccessions = New ToolStripMenuItem()
        ToolStripSeparator21 = New ToolStripSeparator()
        mnuPrintLabels = New ToolStripMenuItem()
        mnuPreAnalytics = New ToolStripMenuItem()
        mnuPrintLog = New ToolStripMenuItem()
        mnuAccDash = New ToolStripMenuItem()
        ToolStripSeparator5 = New ToolStripSeparator()
        mnuOrders = New ToolStripMenuItem()
        mnuInitialize = New ToolStripMenuItem()
        mnuPhlebotomy = New ToolStripMenuItem()
        ToolStripSeparator10 = New ToolStripSeparator()
        mnuSendOuts = New ToolStripMenuItem()
        mnuPrintSendouts = New ToolStripMenuItem()
        mnuAnalysis = New ToolStripMenuItem()
        mnuQCMgmt = New ToolStripMenuItem()
        mnuBatch = New ToolStripMenuItem()
        ToolStripMenuItem1 = New ToolStripSeparator()
        mnuProcess = New ToolStripMenuItem()
        ToolStripSeparator17 = New ToolStripSeparator()
        mnuResultDash = New ToolStripMenuItem()
        mnuApplyRes = New ToolStripMenuItem()
        mnuATRResults = New ToolStripMenuItem()
        DeltaCheckReqReultsToolStripMenuItem = New ToolStripMenuItem()
        mnuApplyResRefLab = New ToolStripMenuItem()
        ToolStripMenuItem2 = New ToolStripSeparator()
        mnuEnterRes = New ToolStripMenuItem()
        mnuRefluxResults = New ToolStripMenuItem()
        mnuBatchResults = New ToolStripMenuItem()
        ToolStripSeparator4 = New ToolStripSeparator()
        mnuWorksheets = New ToolStripMenuItem()
        mnuReports = New ToolStripMenuItem()
        mnuResReports = New ToolStripMenuItem()
        mnuQCReports = New ToolStripMenuItem()
        mnuInternalReports = New ToolStripMenuItem()
        mnuAccessionSummary = New ToolStripMenuItem()
        mnuAccessionsRejected = New ToolStripMenuItem()
        mnuBillReports = New ToolStripSeparator()
        mnuEResults = New ToolStripMenuItem()
        mnuPublish = New ToolStripMenuItem()
        ToolStripSeparator2 = New ToolStripSeparator()
        mnuMyReports = New ToolStripMenuItem()
        mnuMyRptSchedule = New ToolStripMenuItem()
        mnuAdhoc = New ToolStripMenuItem()
        ToolStripSeparator11 = New ToolStripSeparator()
        mnuRptDash = New ToolStripMenuItem()
        ToolStripSeparator12 = New ToolStripSeparator()
        mnuProlisOnControl = New ToolStripMenuItem()
        ToolStripSeparator20 = New ToolStripSeparator()
        mnuBillingAR = New ToolStripMenuItem()
        mnuARC = New ToolStripMenuItem()
        mnuART = New ToolStripMenuItem()
        mnuERA = New ToolStripMenuItem()
        ProcessedERAToolStripMenuItem = New ToolStripMenuItem()
        mnuPayments = New ToolStripMenuItem()
        mnuDebits = New ToolStripMenuItem()
        mnuBarAdhoc = New ToolStripMenuItem()
        ToolStripSeparator19 = New ToolStripSeparator()
        mnuStatement = New ToolStripMenuItem()
        mnuCompReimburse = New ToolStripMenuItem()
        mnuChargesPayments = New ToolStripMenuItem()
        mnuChargesByClients = New ToolStripMenuItem()
        mnuPmtByClients = New ToolStripMenuItem()
        mnuPmtsCollectedByPaymentDate = New ToolStripMenuItem()
        mnuPaymentsCollectedByPostedDates = New ToolStripMenuItem()
        mnuBilling = New ToolStripMenuItem()
        mnuPBReport = New ToolStripMenuItem()
        mnuBillSep1 = New ToolStripSeparator()
        mnuBillEdit = New ToolStripMenuItem()
        mnuBatchBill = New ToolStripMenuItem()
        ToolStripMenuItem5 = New ToolStripSeparator()
        mnuEbillout = New ToolStripMenuItem()
        mnuBillOut = New ToolStripMenuItem()
        mnuPostBill = New ToolStripMenuItem()
        ToolStripSeparator13 = New ToolStripSeparator()
        mnuBillDataSynch = New ToolStripMenuItem()
        mnuSuperBills = New ToolStripMenuItem()
        ToolStripSeparator16 = New ToolStripSeparator()
        mnuImportNecessity = New ToolStripMenuItem()
        mnuImportPrices = New ToolStripMenuItem()
        ToolStripSeparator14 = New ToolStripSeparator()
        mnuBillReqs = New ToolStripMenuItem()
        mnuCreditCards = New ToolStripMenuItem()
        ToolStripSeparator15 = New ToolStripSeparator()
        mnuUpdateRoster = New ToolStripMenuItem()
        mnuUpdateMileageCode = New ToolStripMenuItem()
        mnuDictionary = New ToolStripMenuItem()
        mnuSvc = New ToolStripMenuItem()
        mnuAnalyteManagement = New ToolStripMenuItem()
        mnuGroups = New ToolStripMenuItem()
        mnuProfiles = New ToolStripMenuItem()
        mnuAnalysisSetup = New ToolStripMenuItem()
        ToolStripSeparator7 = New ToolStripSeparator()
        mnuCompRpts = New ToolStripMenuItem()
        mnuClients = New ToolStripMenuItem()
        mnuProviders = New ToolStripMenuItem()
        mnuSales = New ToolStripMenuItem()
        mnuRoutes = New ToolStripMenuItem()
        ToolStripSeparator8 = New ToolStripSeparator()
        mnuClientRpts = New ToolStripMenuItem()
        mnuPayers = New ToolStripMenuItem()
        mnuPayerMgmt = New ToolStripMenuItem()
        mnuPayerMapping = New ToolStripMenuItem()
        mnuPartners = New ToolStripMenuItem()
        ToolStripSeparator9 = New ToolStripSeparator()
        mnuPayerRpt = New ToolStripMenuItem()
        mnuPartnerRPT = New ToolStripMenuItem()
        mnuPatients = New ToolStripMenuItem()
        mnuEditPat = New ToolStripMenuItem()
        mnuRemDupPat = New ToolStripMenuItem()
        mnuOutSource = New ToolStripMenuItem()
        mnuEquips = New ToolStripMenuItem()
        mnuSysConfig = New ToolStripMenuItem()
        mnuRptSetup = New ToolStripMenuItem()
        mnuRptBuild = New ToolStripMenuItem()
        mnuRptOrder = New ToolStripMenuItem()
        mnuUserMgmt = New ToolStripMenuItem()
        FacilityMgmtToolStripMenuItem = New ToolStripMenuItem()
        mnuFacility = New ToolStripMenuItem()
        mnuDept = New ToolStripMenuItem()
        mnuCodes = New ToolStripMenuItem()
        mnuPhrases = New ToolStripMenuItem()
        mnuDxs = New ToolStripMenuItem()
        mnuCPTs = New ToolStripMenuItem()
        mnuWorksetup = New ToolStripMenuItem()
        mnuHelp = New ToolStripMenuItem()
        mnuLocalHelp = New ToolStripMenuItem()
        mnunetHelp = New ToolStripMenuItem()
        mnuSupport = New ToolStripMenuItem()
        mnuAbout = New ToolStripMenuItem()
        RequisitionCopyToolStripMenuItem = New ToolStripMenuItem()
        UpdateResultNoteInTestsToolStripMenuItem = New ToolStripMenuItem()
        ToolStrip = New ToolStrip()
        btnLogin = New ToolStripButton()
        btnLogout = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnMessages = New ToolStripButton()
        btnRequisitions = New ToolStripButton()
        btnResultInquiry = New ToolStripButton()
        btnResultEntry = New ToolStripButton()
        btnReports = New ToolStripButton()
        ToolStripButton1 = New ToolStripButton()
        StatusStrip = New StatusStrip()
        lblUser = New ToolStripStatusLabel()
        lblStatus = New ToolStripStatusLabel()
        lblConnection = New ToolStripStatusLabel()
        ToolTip = New ToolTip(components)
        ProlisHelp = New HelpProvider()
        Timer1 = New Timer(components)
        lblAlert = New Label()
        HelpProvider1 = New HelpProvider()
        TestingToolStripMenuItem = New ToolStripMenuItem()
        ToolStripButton2 = New ToolStripButton()
        MenuStrip.SuspendLayout()
        ToolStrip.SuspendLayout()
        StatusStrip.SuspendLayout()
        SuspendLayout()
        ' 
        ' MenuStrip
        ' 
        MenuStrip.ImageScalingSize = New Size(24, 24)
        MenuStrip.Items.AddRange(New ToolStripItem() {YouMenu, mnuCS, mnuAccession, mnuAnalysis, mnuReports, mnuBillingAR, mnuDictionary, mnuHelp})
        MenuStrip.Location = New Point(0, 0)
        MenuStrip.MdiWindowListItem = mnuReports
        MenuStrip.Name = "MenuStrip"
        MenuStrip.Padding = New Padding(7, 2, 0, 2)
        MenuStrip.Size = New Size(1530, 40)
        MenuStrip.Stretch = False
        MenuStrip.TabIndex = 5
        MenuStrip.Text = "MenuStrip"
        ' 
        ' YouMenu
        ' 
        YouMenu.DropDownItems.AddRange(New ToolStripItem() {mnuLogin, mnuLogout, ToolStripSeparator3, QCExit})
        YouMenu.ForeColor = Color.DarkBlue
        YouMenu.Image = CType(resources.GetObject("YouMenu.Image"), Image)
        YouMenu.ImageScaling = ToolStripItemImageScaling.None
        YouMenu.ImageTransparentColor = SystemColors.ActiveBorder
        YouMenu.Name = "YouMenu"
        YouMenu.Size = New Size(89, 36)
        YouMenu.Text = "&You"
        YouMenu.ToolTipText = "You, the Prolis user"
        ' 
        ' mnuLogin
        ' 
        mnuLogin.Image = CType(resources.GetObject("mnuLogin.Image"), Image)
        mnuLogin.ImageTransparentColor = Color.Black
        mnuLogin.Name = "mnuLogin"
        mnuLogin.ShortcutKeys = Keys.Control Or Keys.N
        mnuLogin.Size = New Size(236, 34)
        mnuLogin.Text = "Logi&n"
        ' 
        ' mnuLogout
        ' 
        mnuLogout.Enabled = False
        mnuLogout.Image = CType(resources.GetObject("mnuLogout.Image"), Image)
        mnuLogout.ImageTransparentColor = Color.Black
        mnuLogout.Name = "mnuLogout"
        mnuLogout.ShortcutKeys = Keys.Control Or Keys.O
        mnuLogout.Size = New Size(236, 34)
        mnuLogout.Text = "Logou&t"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(233, 6)
        ' 
        ' QCExit
        ' 
        QCExit.Image = CType(resources.GetObject("QCExit.Image"), Image)
        QCExit.Name = "QCExit"
        QCExit.Size = New Size(236, 34)
        QCExit.Text = "E&xit"
        ' 
        ' mnuCS
        ' 
        mnuCS.DropDownItems.AddRange(New ToolStripItem() {mnuResInq, mnuMsg, mnuTestInq, mnuProvInquiry, ToolStripMenuItem3, mnuPickup, mnuPickupRpt, ToolStripMenuItem4, mnuARInq, mnuScrubber, mnuPreAuth, ToolStripSeparator6, mnuPrintReqs, mnuManagePanics, ToolStripSeparator18, mnuQuickQuote})
        mnuCS.ForeColor = Color.DarkBlue
        mnuCS.Image = CType(resources.GetObject("mnuCS.Image"), Image)
        mnuCS.ImageScaling = ToolStripItemImageScaling.None
        mnuCS.Name = "mnuCS"
        mnuCS.Size = New Size(164, 36)
        mnuCS.Text = "&Client Service"
        ' 
        ' mnuResInq
        ' 
        mnuResInq.Enabled = False
        mnuResInq.Image = CType(resources.GetObject("mnuResInq.Image"), Image)
        mnuResInq.Name = "mnuResInq"
        mnuResInq.Size = New Size(272, 34)
        mnuResInq.Text = "Result Inquiry"
        ' 
        ' mnuMsg
        ' 
        mnuMsg.Enabled = False
        mnuMsg.Image = CType(resources.GetObject("mnuMsg.Image"), Image)
        mnuMsg.Name = "mnuMsg"
        mnuMsg.Size = New Size(272, 34)
        mnuMsg.Text = "Messages"
        ' 
        ' mnuTestInq
        ' 
        mnuTestInq.Enabled = False
        mnuTestInq.Image = CType(resources.GetObject("mnuTestInq.Image"), Image)
        mnuTestInq.Name = "mnuTestInq"
        mnuTestInq.Size = New Size(272, 34)
        mnuTestInq.Text = "Component Inquiry"
        ' 
        ' mnuProvInquiry
        ' 
        mnuProvInquiry.Enabled = False
        mnuProvInquiry.Image = CType(resources.GetObject("mnuProvInquiry.Image"), Image)
        mnuProvInquiry.Name = "mnuProvInquiry"
        mnuProvInquiry.Size = New Size(272, 34)
        mnuProvInquiry.Text = "Provider Inquiry"
        ' 
        ' ToolStripMenuItem3
        ' 
        ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        ToolStripMenuItem3.Size = New Size(269, 6)
        ' 
        ' mnuPickup
        ' 
        mnuPickup.Enabled = False
        mnuPickup.Image = CType(resources.GetObject("mnuPickup.Image"), Image)
        mnuPickup.Name = "mnuPickup"
        mnuPickup.Size = New Size(272, 34)
        mnuPickup.Text = "Pickup Mgmt"
        ' 
        ' mnuPickupRpt
        ' 
        mnuPickupRpt.Enabled = False
        mnuPickupRpt.Image = CType(resources.GetObject("mnuPickupRpt.Image"), Image)
        mnuPickupRpt.Name = "mnuPickupRpt"
        mnuPickupRpt.Size = New Size(272, 34)
        mnuPickupRpt.Text = "Pickup Report"
        ' 
        ' ToolStripMenuItem4
        ' 
        ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        ToolStripMenuItem4.Size = New Size(269, 6)
        ' 
        ' mnuARInq
        ' 
        mnuARInq.Enabled = False
        mnuARInq.Image = CType(resources.GetObject("mnuARInq.Image"), Image)
        mnuARInq.Name = "mnuARInq"
        mnuARInq.Size = New Size(272, 34)
        mnuARInq.Text = "Missing Info Inquiry"
        ' 
        ' mnuScrubber
        ' 
        mnuScrubber.Enabled = False
        mnuScrubber.Image = CType(resources.GetObject("mnuScrubber.Image"), Image)
        mnuScrubber.Name = "mnuScrubber"
        mnuScrubber.Size = New Size(272, 34)
        mnuScrubber.Text = "Scrubber"
        ' 
        ' mnuPreAuth
        ' 
        mnuPreAuth.Enabled = False
        mnuPreAuth.Image = CType(resources.GetObject("mnuPreAuth.Image"), Image)
        mnuPreAuth.Name = "mnuPreAuth"
        mnuPreAuth.Size = New Size(272, 34)
        mnuPreAuth.Text = "Pre Authorizations"
        ' 
        ' ToolStripSeparator6
        ' 
        ToolStripSeparator6.Name = "ToolStripSeparator6"
        ToolStripSeparator6.Size = New Size(269, 6)
        ' 
        ' mnuPrintReqs
        ' 
        mnuPrintReqs.Enabled = False
        mnuPrintReqs.Image = CType(resources.GetObject("mnuPrintReqs.Image"), Image)
        mnuPrintReqs.Name = "mnuPrintReqs"
        mnuPrintReqs.Size = New Size(272, 34)
        mnuPrintReqs.Text = "Print Requisitions"
        ' 
        ' mnuManagePanics
        ' 
        mnuManagePanics.DropDownItems.AddRange(New ToolStripItem() {mnuPanicMgmt, mnuPrintPanicReport, mnuPanicHistory})
        mnuManagePanics.Image = CType(resources.GetObject("mnuManagePanics.Image"), Image)
        mnuManagePanics.Name = "mnuManagePanics"
        mnuManagePanics.Size = New Size(272, 34)
        mnuManagePanics.Text = "Manage Panics"
        ' 
        ' mnuPanicMgmt
        ' 
        mnuPanicMgmt.Enabled = False
        mnuPanicMgmt.Image = CType(resources.GetObject("mnuPanicMgmt.Image"), Image)
        mnuPanicMgmt.Name = "mnuPanicMgmt"
        mnuPanicMgmt.Size = New Size(264, 34)
        mnuPanicMgmt.Text = "Panic Management"
        ' 
        ' mnuPrintPanicReport
        ' 
        mnuPrintPanicReport.Enabled = False
        mnuPrintPanicReport.Image = CType(resources.GetObject("mnuPrintPanicReport.Image"), Image)
        mnuPrintPanicReport.Name = "mnuPrintPanicReport"
        mnuPrintPanicReport.Size = New Size(264, 34)
        mnuPrintPanicReport.Text = "Print Panic Report"
        ' 
        ' mnuPanicHistory
        ' 
        mnuPanicHistory.Enabled = False
        mnuPanicHistory.Image = CType(resources.GetObject("mnuPanicHistory.Image"), Image)
        mnuPanicHistory.Name = "mnuPanicHistory"
        mnuPanicHistory.Size = New Size(264, 34)
        mnuPanicHistory.Text = "Panic History"
        ' 
        ' ToolStripSeparator18
        ' 
        ToolStripSeparator18.Name = "ToolStripSeparator18"
        ToolStripSeparator18.Size = New Size(269, 6)
        ' 
        ' mnuQuickQuote
        ' 
        mnuQuickQuote.Enabled = False
        mnuQuickQuote.Image = CType(resources.GetObject("mnuQuickQuote.Image"), Image)
        mnuQuickQuote.Name = "mnuQuickQuote"
        mnuQuickQuote.Size = New Size(272, 34)
        mnuQuickQuote.Text = "POS Transactions"
        ' 
        ' mnuAccession
        ' 
        mnuAccession.DropDownItems.AddRange(New ToolStripItem() {mnuRequisitions, mnuRemoteAcc, mnuAutoAccessions, ToolStripSeparator21, mnuPrintLabels, mnuPreAnalytics, mnuPrintLog, mnuAccDash, ToolStripSeparator5, mnuOrders, mnuInitialize, mnuPhlebotomy, ToolStripSeparator10, mnuSendOuts, mnuPrintSendouts})
        mnuAccession.ForeColor = Color.DarkBlue
        mnuAccession.Image = CType(resources.GetObject("mnuAccession.Image"), Image)
        mnuAccession.ImageScaling = ToolStripItemImageScaling.None
        mnuAccession.Name = "mnuAccession"
        mnuAccession.Size = New Size(138, 36)
        mnuAccession.Text = "&Accession"
        ' 
        ' mnuRequisitions
        ' 
        mnuRequisitions.Enabled = False
        mnuRequisitions.Image = CType(resources.GetObject("mnuRequisitions.Image"), Image)
        mnuRequisitions.Name = "mnuRequisitions"
        mnuRequisitions.Size = New Size(300, 34)
        mnuRequisitions.Text = "Accessions"
        ' 
        ' mnuRemoteAcc
        ' 
        mnuRemoteAcc.Enabled = False
        mnuRemoteAcc.Image = CType(resources.GetObject("mnuRemoteAcc.Image"), Image)
        mnuRemoteAcc.Name = "mnuRemoteAcc"
        mnuRemoteAcc.Size = New Size(300, 34)
        mnuRemoteAcc.Text = "Remote Accessions"
        ' 
        ' mnuAutoAccessions
        ' 
        mnuAutoAccessions.Enabled = False
        mnuAutoAccessions.Image = CType(resources.GetObject("mnuAutoAccessions.Image"), Image)
        mnuAutoAccessions.Name = "mnuAutoAccessions"
        mnuAutoAccessions.ShowShortcutKeys = False
        mnuAutoAccessions.Size = New Size(300, 34)
        mnuAutoAccessions.Text = "Aut Accessions"
        ' 
        ' ToolStripSeparator21
        ' 
        ToolStripSeparator21.Name = "ToolStripSeparator21"
        ToolStripSeparator21.Size = New Size(297, 6)
        ' 
        ' mnuPrintLabels
        ' 
        mnuPrintLabels.Enabled = False
        mnuPrintLabels.Image = CType(resources.GetObject("mnuPrintLabels.Image"), Image)
        mnuPrintLabels.Name = "mnuPrintLabels"
        mnuPrintLabels.Size = New Size(300, 34)
        mnuPrintLabels.Text = "Print Acc Labels"
        ' 
        ' mnuPreAnalytics
        ' 
        mnuPreAnalytics.Enabled = False
        mnuPreAnalytics.Image = CType(resources.GetObject("mnuPreAnalytics.Image"), Image)
        mnuPreAnalytics.Name = "mnuPreAnalytics"
        mnuPreAnalytics.Size = New Size(300, 34)
        mnuPreAnalytics.Text = "Preanalyticals"
        ' 
        ' mnuPrintLog
        ' 
        mnuPrintLog.Enabled = False
        mnuPrintLog.Image = CType(resources.GetObject("mnuPrintLog.Image"), Image)
        mnuPrintLog.Name = "mnuPrintLog"
        mnuPrintLog.Size = New Size(300, 34)
        mnuPrintLog.Text = "Print Acc Log"
        ' 
        ' mnuAccDash
        ' 
        mnuAccDash.Enabled = False
        mnuAccDash.Image = CType(resources.GetObject("mnuAccDash.Image"), Image)
        mnuAccDash.Name = "mnuAccDash"
        mnuAccDash.Size = New Size(300, 34)
        mnuAccDash.Text = "Accession Dashboard"
        ' 
        ' ToolStripSeparator5
        ' 
        ToolStripSeparator5.Name = "ToolStripSeparator5"
        ToolStripSeparator5.Size = New Size(297, 6)
        ' 
        ' mnuOrders
        ' 
        mnuOrders.Enabled = False
        mnuOrders.Image = CType(resources.GetObject("mnuOrders.Image"), Image)
        mnuOrders.Name = "mnuOrders"
        mnuOrders.Size = New Size(300, 34)
        mnuOrders.Text = "Future/Standing Orders"
        ' 
        ' mnuInitialize
        ' 
        mnuInitialize.Enabled = False
        mnuInitialize.Image = CType(resources.GetObject("mnuInitialize.Image"), Image)
        mnuInitialize.Name = "mnuInitialize"
        mnuInitialize.Size = New Size(300, 34)
        mnuInitialize.Text = "Order Instantiation"
        ' 
        ' mnuPhlebotomy
        ' 
        mnuPhlebotomy.Enabled = False
        mnuPhlebotomy.Image = CType(resources.GetObject("mnuPhlebotomy.Image"), Image)
        mnuPhlebotomy.Name = "mnuPhlebotomy"
        mnuPhlebotomy.Size = New Size(300, 34)
        mnuPhlebotomy.Text = "Phlebotomy Report"
        ' 
        ' ToolStripSeparator10
        ' 
        ToolStripSeparator10.Name = "ToolStripSeparator10"
        ToolStripSeparator10.Size = New Size(297, 6)
        ' 
        ' mnuSendOuts
        ' 
        mnuSendOuts.Enabled = False
        mnuSendOuts.Image = CType(resources.GetObject("mnuSendOuts.Image"), Image)
        mnuSendOuts.Name = "mnuSendOuts"
        mnuSendOuts.Size = New Size(300, 34)
        mnuSendOuts.Text = "Send Outs"
        ' 
        ' mnuPrintSendouts
        ' 
        mnuPrintSendouts.Enabled = False
        mnuPrintSendouts.Image = CType(resources.GetObject("mnuPrintSendouts.Image"), Image)
        mnuPrintSendouts.Name = "mnuPrintSendouts"
        mnuPrintSendouts.Size = New Size(300, 34)
        mnuPrintSendouts.Text = "Print Sendouts"
        ' 
        ' mnuAnalysis
        ' 
        mnuAnalysis.DropDownItems.AddRange(New ToolStripItem() {mnuQCMgmt, mnuBatch, ToolStripMenuItem1, mnuProcess, ToolStripSeparator17, mnuResultDash, mnuApplyRes, mnuATRResults, DeltaCheckReqReultsToolStripMenuItem, mnuApplyResRefLab, ToolStripMenuItem2, mnuEnterRes, mnuRefluxResults, mnuBatchResults, ToolStripSeparator4, mnuWorksheets})
        mnuAnalysis.ForeColor = Color.DarkBlue
        mnuAnalysis.Image = CType(resources.GetObject("mnuAnalysis.Image"), Image)
        mnuAnalysis.ImageScaling = ToolStripItemImageScaling.None
        mnuAnalysis.Name = "mnuAnalysis"
        mnuAnalysis.Size = New Size(124, 36)
        mnuAnalysis.Text = "A&nalysis"
        ' 
        ' mnuQCMgmt
        ' 
        mnuQCMgmt.Enabled = False
        mnuQCMgmt.Image = CType(resources.GetObject("mnuQCMgmt.Image"), Image)
        mnuQCMgmt.Name = "mnuQCMgmt"
        mnuQCMgmt.Size = New Size(316, 34)
        mnuQCMgmt.Text = "QC Mgmt"
        ' 
        ' mnuBatch
        ' 
        mnuBatch.Enabled = False
        mnuBatch.Image = CType(resources.GetObject("mnuBatch.Image"), Image)
        mnuBatch.Name = "mnuBatch"
        mnuBatch.Size = New Size(316, 34)
        mnuBatch.Text = "Batch Samples"
        ' 
        ' ToolStripMenuItem1
        ' 
        ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        ToolStripMenuItem1.Size = New Size(313, 6)
        ' 
        ' mnuProcess
        ' 
        mnuProcess.Enabled = False
        mnuProcess.Image = CType(resources.GetObject("mnuProcess.Image"), Image)
        mnuProcess.Name = "mnuProcess"
        mnuProcess.Size = New Size(316, 34)
        mnuProcess.Text = "Processing"
        ' 
        ' ToolStripSeparator17
        ' 
        ToolStripSeparator17.Name = "ToolStripSeparator17"
        ToolStripSeparator17.Size = New Size(313, 6)
        ' 
        ' mnuResultDash
        ' 
        mnuResultDash.Enabled = False
        mnuResultDash.Image = CType(resources.GetObject("mnuResultDash.Image"), Image)
        mnuResultDash.Name = "mnuResultDash"
        mnuResultDash.Size = New Size(316, 34)
        mnuResultDash.Text = "Resulting Dashboard"
        ' 
        ' mnuApplyRes
        ' 
        mnuApplyRes.Enabled = False
        mnuApplyRes.Image = CType(resources.GetObject("mnuApplyRes.Image"), Image)
        mnuApplyRes.Name = "mnuApplyRes"
        mnuApplyRes.Size = New Size(316, 34)
        mnuApplyRes.Text = "Apply Equip Results"
        ' 
        ' mnuATRResults
        ' 
        mnuATRResults.Enabled = False
        mnuATRResults.Image = CType(resources.GetObject("mnuATRResults.Image"), Image)
        mnuATRResults.Name = "mnuATRResults"
        mnuATRResults.Size = New Size(316, 34)
        mnuATRResults.Text = "Att Req Results"
        ' 
        ' DeltaCheckReqReultsToolStripMenuItem
        ' 
        DeltaCheckReqReultsToolStripMenuItem.Enabled = False
        DeltaCheckReqReultsToolStripMenuItem.Name = "DeltaCheckReqReultsToolStripMenuItem"
        DeltaCheckReqReultsToolStripMenuItem.Size = New Size(316, 34)
        DeltaCheckReqReultsToolStripMenuItem.Text = "∆ Delta check Req Results"
        ' 
        ' mnuApplyResRefLab
        ' 
        mnuApplyResRefLab.Enabled = False
        mnuApplyResRefLab.Image = CType(resources.GetObject("mnuApplyResRefLab.Image"), Image)
        mnuApplyResRefLab.Name = "mnuApplyResRefLab"
        mnuApplyResRefLab.Size = New Size(316, 34)
        mnuApplyResRefLab.Text = "Apply Ref Lab Results"
        ' 
        ' ToolStripMenuItem2
        ' 
        ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        ToolStripMenuItem2.Size = New Size(313, 6)
        ' 
        ' mnuEnterRes
        ' 
        mnuEnterRes.Enabled = False
        mnuEnterRes.Image = CType(resources.GetObject("mnuEnterRes.Image"), Image)
        mnuEnterRes.Name = "mnuEnterRes"
        mnuEnterRes.Size = New Size(316, 34)
        mnuEnterRes.Text = "Enter/Review Results"
        ' 
        ' mnuRefluxResults
        ' 
        mnuRefluxResults.Enabled = False
        mnuRefluxResults.Image = CType(resources.GetObject("mnuRefluxResults.Image"), Image)
        mnuRefluxResults.Name = "mnuRefluxResults"
        mnuRefluxResults.Size = New Size(316, 34)
        mnuRefluxResults.Text = "Reflux Results"
        ' 
        ' mnuBatchResults
        ' 
        mnuBatchResults.Enabled = False
        mnuBatchResults.Image = CType(resources.GetObject("mnuBatchResults.Image"), Image)
        mnuBatchResults.Name = "mnuBatchResults"
        mnuBatchResults.Size = New Size(316, 34)
        mnuBatchResults.Text = "Result Entry by Batch"
        ' 
        ' ToolStripSeparator4
        ' 
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New Size(313, 6)
        ' 
        ' mnuWorksheets
        ' 
        mnuWorksheets.Enabled = False
        mnuWorksheets.Image = CType(resources.GetObject("mnuWorksheets.Image"), Image)
        mnuWorksheets.Name = "mnuWorksheets"
        mnuWorksheets.Size = New Size(316, 34)
        mnuWorksheets.Text = "Worksheets"
        ' 
        ' mnuReports
        ' 
        mnuReports.DropDownItems.AddRange(New ToolStripItem() {mnuResReports, mnuQCReports, mnuInternalReports, mnuBillReports, mnuEResults, mnuPublish, ToolStripSeparator2, mnuMyReports, mnuMyRptSchedule, mnuAdhoc, ToolStripSeparator11, mnuRptDash, ToolStripSeparator12, mnuProlisOnControl, ToolStripSeparator20})
        mnuReports.ForeColor = Color.DarkBlue
        mnuReports.Image = CType(resources.GetObject("mnuReports.Image"), Image)
        mnuReports.ImageScaling = ToolStripItemImageScaling.None
        mnuReports.Name = "mnuReports"
        mnuReports.Size = New Size(138, 36)
        mnuReports.Text = "&Reporting"
        ' 
        ' mnuResReports
        ' 
        mnuResReports.Enabled = False
        mnuResReports.Image = CType(resources.GetObject("mnuResReports.Image"), Image)
        mnuResReports.Name = "mnuResReports"
        mnuResReports.Size = New Size(287, 34)
        mnuResReports.Text = "Result Reports"
        ' 
        ' mnuQCReports
        ' 
        mnuQCReports.Enabled = False
        mnuQCReports.Image = CType(resources.GetObject("mnuQCReports.Image"), Image)
        mnuQCReports.Name = "mnuQCReports"
        mnuQCReports.Size = New Size(287, 34)
        mnuQCReports.Text = "QC Reports"
        ' 
        ' mnuInternalReports
        ' 
        mnuInternalReports.DropDownItems.AddRange(New ToolStripItem() {mnuAccessionSummary, mnuAccessionsRejected})
        mnuInternalReports.Enabled = False
        mnuInternalReports.Image = CType(resources.GetObject("mnuInternalReports.Image"), Image)
        mnuInternalReports.Name = "mnuInternalReports"
        mnuInternalReports.Size = New Size(287, 34)
        mnuInternalReports.Text = "Internal Reports"
        ' 
        ' mnuAccessionSummary
        ' 
        mnuAccessionSummary.Image = CType(resources.GetObject("mnuAccessionSummary.Image"), Image)
        mnuAccessionSummary.Name = "mnuAccessionSummary"
        mnuAccessionSummary.Size = New Size(273, 34)
        mnuAccessionSummary.Text = "Accession Summary"
        ' 
        ' mnuAccessionsRejected
        ' 
        mnuAccessionsRejected.Image = CType(resources.GetObject("mnuAccessionsRejected.Image"), Image)
        mnuAccessionsRejected.Name = "mnuAccessionsRejected"
        mnuAccessionsRejected.Size = New Size(273, 34)
        mnuAccessionsRejected.Text = "Accessions Rejected"
        ' 
        ' mnuBillReports
        ' 
        mnuBillReports.Name = "mnuBillReports"
        mnuBillReports.Size = New Size(284, 6)
        mnuBillReports.Visible = False
        ' 
        ' mnuEResults
        ' 
        mnuEResults.Enabled = False
        mnuEResults.Image = CType(resources.GetObject("mnuEResults.Image"), Image)
        mnuEResults.Name = "mnuEResults"
        mnuEResults.Size = New Size(287, 34)
        mnuEResults.Text = "Electronic Results"
        ' 
        ' mnuPublish
        ' 
        mnuPublish.Enabled = False
        mnuPublish.Image = CType(resources.GetObject("mnuPublish.Image"), Image)
        mnuPublish.Name = "mnuPublish"
        mnuPublish.Size = New Size(287, 34)
        mnuPublish.Text = "Result Publish Control"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(284, 6)
        ' 
        ' mnuMyReports
        ' 
        mnuMyReports.Enabled = False
        mnuMyReports.Image = CType(resources.GetObject("mnuMyReports.Image"), Image)
        mnuMyReports.Name = "mnuMyReports"
        mnuMyReports.Size = New Size(287, 34)
        mnuMyReports.Text = "My Reports"
        ' 
        ' mnuMyRptSchedule
        ' 
        mnuMyRptSchedule.Enabled = False
        mnuMyRptSchedule.Image = CType(resources.GetObject("mnuMyRptSchedule.Image"), Image)
        mnuMyRptSchedule.Name = "mnuMyRptSchedule"
        mnuMyRptSchedule.Size = New Size(287, 34)
        mnuMyRptSchedule.Text = "My Report Schedule"
        ' 
        ' mnuAdhoc
        ' 
        mnuAdhoc.Enabled = False
        mnuAdhoc.Image = CType(resources.GetObject("mnuAdhoc.Image"), Image)
        mnuAdhoc.Name = "mnuAdhoc"
        mnuAdhoc.Size = New Size(287, 34)
        mnuAdhoc.Text = "Adhoc SQL Query"
        ' 
        ' ToolStripSeparator11
        ' 
        ToolStripSeparator11.Name = "ToolStripSeparator11"
        ToolStripSeparator11.Size = New Size(284, 6)
        ' 
        ' mnuRptDash
        ' 
        mnuRptDash.Enabled = False
        mnuRptDash.Image = CType(resources.GetObject("mnuRptDash.Image"), Image)
        mnuRptDash.Name = "mnuRptDash"
        mnuRptDash.Size = New Size(287, 34)
        mnuRptDash.Text = "Reporting Dashboard"
        ' 
        ' ToolStripSeparator12
        ' 
        ToolStripSeparator12.Name = "ToolStripSeparator12"
        ToolStripSeparator12.Size = New Size(284, 6)
        ' 
        ' mnuProlisOnControl
        ' 
        mnuProlisOnControl.Enabled = False
        mnuProlisOnControl.Image = CType(resources.GetObject("mnuProlisOnControl.Image"), Image)
        mnuProlisOnControl.Name = "mnuProlisOnControl"
        mnuProlisOnControl.Size = New Size(287, 34)
        mnuProlisOnControl.Text = "ProlisOn Control"
        ' 
        ' ToolStripSeparator20
        ' 
        ToolStripSeparator20.Name = "ToolStripSeparator20"
        ToolStripSeparator20.Size = New Size(284, 6)
        ' 
        ' mnuBillingAR
        ' 
        mnuBillingAR.DropDownItems.AddRange(New ToolStripItem() {mnuARC, mnuBilling, ToolStripSeparator13, mnuBillDataSynch, mnuSuperBills, ToolStripSeparator16, mnuImportNecessity, mnuImportPrices, ToolStripSeparator14, mnuBillReqs, mnuCreditCards, ToolStripSeparator15, mnuUpdateRoster, mnuUpdateMileageCode})
        mnuBillingAR.ForeColor = Color.DarkBlue
        mnuBillingAR.Image = CType(resources.GetObject("mnuBillingAR.Image"), Image)
        mnuBillingAR.ImageScaling = ToolStripItemImageScaling.None
        mnuBillingAR.Name = "mnuBillingAR"
        mnuBillingAR.Size = New Size(135, 36)
        mnuBillingAR.Text = "&Billing AR"
        ' 
        ' mnuARC
        ' 
        mnuARC.DropDownItems.AddRange(New ToolStripItem() {mnuART, mnuERA, ProcessedERAToolStripMenuItem, mnuPayments, mnuDebits, mnuBarAdhoc, ToolStripSeparator19, mnuStatement, mnuCompReimburse, mnuChargesPayments, mnuChargesByClients, mnuPmtByClients, mnuPmtsCollectedByPaymentDate, mnuPaymentsCollectedByPostedDates})
        mnuARC.Image = CType(resources.GetObject("mnuARC.Image"), Image)
        mnuARC.Name = "mnuARC"
        mnuARC.Size = New Size(312, 34)
        mnuARC.Text = "Accounts Receivables"
        ' 
        ' mnuART
        ' 
        mnuART.Enabled = False
        mnuART.Image = CType(resources.GetObject("mnuART.Image"), Image)
        mnuART.Name = "mnuART"
        mnuART.Size = New Size(407, 34)
        mnuART.Text = "AR Inquiry"
        ' 
        ' mnuERA
        ' 
        mnuERA.Enabled = False
        mnuERA.Image = CType(resources.GetObject("mnuERA.Image"), Image)
        mnuERA.Name = "mnuERA"
        mnuERA.Size = New Size(407, 34)
        mnuERA.Text = "ERA Processing"
        ' 
        ' ProcessedERAToolStripMenuItem
        ' 
        ProcessedERAToolStripMenuItem.Enabled = False
        ProcessedERAToolStripMenuItem.Name = "ProcessedERAToolStripMenuItem"
        ProcessedERAToolStripMenuItem.Size = New Size(407, 34)
        ProcessedERAToolStripMenuItem.Text = "Processed ERA"
        ' 
        ' mnuPayments
        ' 
        mnuPayments.Enabled = False
        mnuPayments.Image = CType(resources.GetObject("mnuPayments.Image"), Image)
        mnuPayments.Name = "mnuPayments"
        mnuPayments.Size = New Size(407, 34)
        mnuPayments.Text = "Payments"
        ' 
        ' mnuDebits
        ' 
        mnuDebits.Enabled = False
        mnuDebits.Image = CType(resources.GetObject("mnuDebits.Image"), Image)
        mnuDebits.Name = "mnuDebits"
        mnuDebits.Size = New Size(407, 34)
        mnuDebits.Text = "AR Adjustments"
        ' 
        ' mnuBarAdhoc
        ' 
        mnuBarAdhoc.Enabled = False
        mnuBarAdhoc.Image = CType(resources.GetObject("mnuBarAdhoc.Image"), Image)
        mnuBarAdhoc.Name = "mnuBarAdhoc"
        mnuBarAdhoc.Size = New Size(407, 34)
        mnuBarAdhoc.Text = "BAR Adhoc Report"
        ' 
        ' ToolStripSeparator19
        ' 
        ToolStripSeparator19.Name = "ToolStripSeparator19"
        ToolStripSeparator19.Size = New Size(404, 6)
        ' 
        ' mnuStatement
        ' 
        mnuStatement.Enabled = False
        mnuStatement.Image = CType(resources.GetObject("mnuStatement.Image"), Image)
        mnuStatement.Name = "mnuStatement"
        mnuStatement.Size = New Size(407, 34)
        mnuStatement.Text = "Statement"
        ' 
        ' mnuCompReimburse
        ' 
        mnuCompReimburse.Enabled = False
        mnuCompReimburse.Image = CType(resources.GetObject("mnuCompReimburse.Image"), Image)
        mnuCompReimburse.Name = "mnuCompReimburse"
        mnuCompReimburse.Size = New Size(407, 34)
        mnuCompReimburse.Text = "Component Reimbursement"
        ' 
        ' mnuChargesPayments
        ' 
        mnuChargesPayments.Enabled = False
        mnuChargesPayments.Image = CType(resources.GetObject("mnuChargesPayments.Image"), Image)
        mnuChargesPayments.Name = "mnuChargesPayments"
        mnuChargesPayments.Size = New Size(407, 34)
        mnuChargesPayments.Text = "Charges and Payments"
        ' 
        ' mnuChargesByClients
        ' 
        mnuChargesByClients.Enabled = False
        mnuChargesByClients.Image = CType(resources.GetObject("mnuChargesByClients.Image"), Image)
        mnuChargesByClients.Name = "mnuChargesByClients"
        mnuChargesByClients.Size = New Size(407, 34)
        mnuChargesByClients.Text = "Charges by Clients"
        ' 
        ' mnuPmtByClients
        ' 
        mnuPmtByClients.Enabled = False
        mnuPmtByClients.Image = CType(resources.GetObject("mnuPmtByClients.Image"), Image)
        mnuPmtByClients.Name = "mnuPmtByClients"
        mnuPmtByClients.Size = New Size(407, 34)
        mnuPmtByClients.Text = "Payment by Clients"
        ' 
        ' mnuPmtsCollectedByPaymentDate
        ' 
        mnuPmtsCollectedByPaymentDate.Enabled = False
        mnuPmtsCollectedByPaymentDate.Image = CType(resources.GetObject("mnuPmtsCollectedByPaymentDate.Image"), Image)
        mnuPmtsCollectedByPaymentDate.Name = "mnuPmtsCollectedByPaymentDate"
        mnuPmtsCollectedByPaymentDate.Size = New Size(407, 34)
        mnuPmtsCollectedByPaymentDate.Text = "Payments Collected By Payment Date"
        ' 
        ' mnuPaymentsCollectedByPostedDates
        ' 
        mnuPaymentsCollectedByPostedDates.Enabled = False
        mnuPaymentsCollectedByPostedDates.Image = CType(resources.GetObject("mnuPaymentsCollectedByPostedDates.Image"), Image)
        mnuPaymentsCollectedByPostedDates.Name = "mnuPaymentsCollectedByPostedDates"
        mnuPaymentsCollectedByPostedDates.Size = New Size(407, 34)
        mnuPaymentsCollectedByPostedDates.Text = "Payments Collected By Posted Date"
        ' 
        ' mnuBilling
        ' 
        mnuBilling.DropDownItems.AddRange(New ToolStripItem() {mnuPBReport, mnuBillSep1, mnuBillEdit, mnuBatchBill, ToolStripMenuItem5, mnuEbillout, mnuBillOut, mnuPostBill})
        mnuBilling.Image = CType(resources.GetObject("mnuBilling.Image"), Image)
        mnuBilling.Name = "mnuBilling"
        mnuBilling.Size = New Size(312, 34)
        mnuBilling.Text = "Billing"
        ' 
        ' mnuPBReport
        ' 
        mnuPBReport.Enabled = False
        mnuPBReport.Image = CType(resources.GetObject("mnuPBReport.Image"), Image)
        mnuPBReport.Name = "mnuPBReport"
        mnuPBReport.Size = New Size(266, 34)
        mnuPBReport.Text = "Pre Billing Reports"
        ' 
        ' mnuBillSep1
        ' 
        mnuBillSep1.Name = "mnuBillSep1"
        mnuBillSep1.Size = New Size(263, 6)
        ' 
        ' mnuBillEdit
        ' 
        mnuBillEdit.Enabled = False
        mnuBillEdit.Image = CType(resources.GetObject("mnuBillEdit.Image"), Image)
        mnuBillEdit.Name = "mnuBillEdit"
        mnuBillEdit.Size = New Size(266, 34)
        mnuBillEdit.Text = "Billing Editor"
        ' 
        ' mnuBatchBill
        ' 
        mnuBatchBill.Enabled = False
        mnuBatchBill.Image = CType(resources.GetObject("mnuBatchBill.Image"), Image)
        mnuBatchBill.Name = "mnuBatchBill"
        mnuBatchBill.Size = New Size(266, 34)
        mnuBatchBill.Text = "Billing Dashboard"
        ' 
        ' ToolStripMenuItem5
        ' 
        ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        ToolStripMenuItem5.Size = New Size(263, 6)
        ' 
        ' mnuEbillout
        ' 
        mnuEbillout.Enabled = False
        mnuEbillout.Image = CType(resources.GetObject("mnuEbillout.Image"), Image)
        mnuEbillout.Name = "mnuEbillout"
        mnuEbillout.Size = New Size(266, 34)
        mnuEbillout.Text = "E Bill Out"
        ' 
        ' mnuBillOut
        ' 
        mnuBillOut.Enabled = False
        mnuBillOut.Image = CType(resources.GetObject("mnuBillOut.Image"), Image)
        mnuBillOut.Name = "mnuBillOut"
        mnuBillOut.Size = New Size(266, 34)
        mnuBillOut.Text = "Billing Output"
        ' 
        ' mnuPostBill
        ' 
        mnuPostBill.Enabled = False
        mnuPostBill.Image = CType(resources.GetObject("mnuPostBill.Image"), Image)
        mnuPostBill.Name = "mnuPostBill"
        mnuPostBill.Size = New Size(266, 34)
        mnuPostBill.Text = "Post Billing Reports"
        ' 
        ' ToolStripSeparator13
        ' 
        ToolStripSeparator13.Name = "ToolStripSeparator13"
        ToolStripSeparator13.Size = New Size(309, 6)
        ' 
        ' mnuBillDataSynch
        ' 
        mnuBillDataSynch.Enabled = False
        mnuBillDataSynch.Image = CType(resources.GetObject("mnuBillDataSynch.Image"), Image)
        mnuBillDataSynch.Name = "mnuBillDataSynch"
        mnuBillDataSynch.Size = New Size(312, 34)
        mnuBillDataSynch.Text = "Synchronize Data"
        ' 
        ' mnuSuperBills
        ' 
        mnuSuperBills.Enabled = False
        mnuSuperBills.Image = CType(resources.GetObject("mnuSuperBills.Image"), Image)
        mnuSuperBills.Name = "mnuSuperBills"
        mnuSuperBills.Size = New Size(312, 34)
        mnuSuperBills.Text = "Export Billables"
        ' 
        ' ToolStripSeparator16
        ' 
        ToolStripSeparator16.Name = "ToolStripSeparator16"
        ToolStripSeparator16.Size = New Size(309, 6)
        ' 
        ' mnuImportNecessity
        ' 
        mnuImportNecessity.Enabled = False
        mnuImportNecessity.Image = CType(resources.GetObject("mnuImportNecessity.Image"), Image)
        mnuImportNecessity.Name = "mnuImportNecessity"
        mnuImportNecessity.Size = New Size(312, 34)
        mnuImportNecessity.Text = "Import Necessity"
        ' 
        ' mnuImportPrices
        ' 
        mnuImportPrices.Enabled = False
        mnuImportPrices.Image = CType(resources.GetObject("mnuImportPrices.Image"), Image)
        mnuImportPrices.Name = "mnuImportPrices"
        mnuImportPrices.Size = New Size(312, 34)
        mnuImportPrices.Text = "Import Pricing"
        ' 
        ' ToolStripSeparator14
        ' 
        ToolStripSeparator14.Name = "ToolStripSeparator14"
        ToolStripSeparator14.Size = New Size(309, 6)
        ' 
        ' mnuBillReqs
        ' 
        mnuBillReqs.Enabled = False
        mnuBillReqs.Image = CType(resources.GetObject("mnuBillReqs.Image"), Image)
        mnuBillReqs.Name = "mnuBillReqs"
        mnuBillReqs.Size = New Size(312, 34)
        mnuBillReqs.Text = "Billing Requisits"
        ' 
        ' mnuCreditCards
        ' 
        mnuCreditCards.Enabled = False
        mnuCreditCards.Image = CType(resources.GetObject("mnuCreditCards.Image"), Image)
        mnuCreditCards.Name = "mnuCreditCards"
        mnuCreditCards.Size = New Size(312, 34)
        mnuCreditCards.Text = "Accept Credit Cards"
        ' 
        ' ToolStripSeparator15
        ' 
        ToolStripSeparator15.Name = "ToolStripSeparator15"
        ToolStripSeparator15.Size = New Size(309, 6)
        ' 
        ' mnuUpdateRoster
        ' 
        mnuUpdateRoster.Enabled = False
        mnuUpdateRoster.Image = CType(resources.GetObject("mnuUpdateRoster.Image"), Image)
        mnuUpdateRoster.Name = "mnuUpdateRoster"
        mnuUpdateRoster.Size = New Size(312, 34)
        mnuUpdateRoster.Text = "Update Uninsured Roster"
        ' 
        ' mnuUpdateMileageCode
        ' 
        mnuUpdateMileageCode.Enabled = False
        mnuUpdateMileageCode.Image = CType(resources.GetObject("mnuUpdateMileageCode.Image"), Image)
        mnuUpdateMileageCode.Name = "mnuUpdateMileageCode"
        mnuUpdateMileageCode.Size = New Size(312, 34)
        mnuUpdateMileageCode.Text = "Update Mileage Code"
        ' 
        ' mnuDictionary
        ' 
        mnuDictionary.DropDownItems.AddRange(New ToolStripItem() {mnuSvc, mnuClients, mnuPayers, mnuPatients, mnuOutSource, mnuEquips, mnuSysConfig, mnuRptSetup, mnuUserMgmt, FacilityMgmtToolStripMenuItem, mnuCodes, mnuWorksetup})
        mnuDictionary.ForeColor = Color.DarkBlue
        mnuDictionary.Image = CType(resources.GetObject("mnuDictionary.Image"), Image)
        mnuDictionary.ImageScaling = ToolStripItemImageScaling.None
        mnuDictionary.Name = "mnuDictionary"
        mnuDictionary.Size = New Size(140, 36)
        mnuDictionary.Text = "&Dictionary"
        ' 
        ' mnuSvc
        ' 
        mnuSvc.DropDownItems.AddRange(New ToolStripItem() {mnuAnalyteManagement, mnuGroups, mnuProfiles, mnuAnalysisSetup, ToolStripSeparator7, mnuCompRpts})
        mnuSvc.Image = CType(resources.GetObject("mnuSvc.Image"), Image)
        mnuSvc.Name = "mnuSvc"
        mnuSvc.Size = New Size(249, 34)
        mnuSvc.Text = "Components"
        ' 
        ' mnuAnalyteManagement
        ' 
        mnuAnalyteManagement.Enabled = False
        mnuAnalyteManagement.Image = CType(resources.GetObject("mnuAnalyteManagement.Image"), Image)
        mnuAnalyteManagement.Name = "mnuAnalyteManagement"
        mnuAnalyteManagement.Size = New Size(263, 34)
        mnuAnalyteManagement.Text = "Analyte Mgmt"
        ' 
        ' mnuGroups
        ' 
        mnuGroups.Enabled = False
        mnuGroups.Image = CType(resources.GetObject("mnuGroups.Image"), Image)
        mnuGroups.Name = "mnuGroups"
        mnuGroups.Size = New Size(263, 34)
        mnuGroups.Text = "Group Mgmt"
        ' 
        ' mnuProfiles
        ' 
        mnuProfiles.Enabled = False
        mnuProfiles.Image = CType(resources.GetObject("mnuProfiles.Image"), Image)
        mnuProfiles.Name = "mnuProfiles"
        mnuProfiles.Size = New Size(263, 34)
        mnuProfiles.Text = "Profile Mgmt"
        ' 
        ' mnuAnalysisSetup
        ' 
        mnuAnalysisSetup.Enabled = False
        mnuAnalysisSetup.Image = CType(resources.GetObject("mnuAnalysisSetup.Image"), Image)
        mnuAnalysisSetup.Name = "mnuAnalysisSetup"
        mnuAnalysisSetup.Size = New Size(263, 34)
        mnuAnalysisSetup.Text = "Analysis Setup"
        ' 
        ' ToolStripSeparator7
        ' 
        ToolStripSeparator7.Name = "ToolStripSeparator7"
        ToolStripSeparator7.Size = New Size(260, 6)
        ' 
        ' mnuCompRpts
        ' 
        mnuCompRpts.Enabled = False
        mnuCompRpts.Image = CType(resources.GetObject("mnuCompRpts.Image"), Image)
        mnuCompRpts.Name = "mnuCompRpts"
        mnuCompRpts.ShowShortcutKeys = False
        mnuCompRpts.Size = New Size(263, 34)
        mnuCompRpts.Text = "Component Reports"
        ' 
        ' mnuClients
        ' 
        mnuClients.DropDownItems.AddRange(New ToolStripItem() {mnuProviders, mnuSales, mnuRoutes, ToolStripSeparator8, mnuClientRpts})
        mnuClients.Image = CType(resources.GetObject("mnuClients.Image"), Image)
        mnuClients.Name = "mnuClients"
        mnuClients.Size = New Size(249, 34)
        mnuClients.Text = "Client Mgmt"
        ' 
        ' mnuProviders
        ' 
        mnuProviders.Enabled = False
        mnuProviders.Image = CType(resources.GetObject("mnuProviders.Image"), Image)
        mnuProviders.Name = "mnuProviders"
        mnuProviders.Size = New Size(234, 34)
        mnuProviders.Text = "Provider Mgmt"
        ' 
        ' mnuSales
        ' 
        mnuSales.Enabled = False
        mnuSales.Image = CType(resources.GetObject("mnuSales.Image"), Image)
        mnuSales.Name = "mnuSales"
        mnuSales.Size = New Size(234, 34)
        mnuSales.Text = "Sales Persons"
        ' 
        ' mnuRoutes
        ' 
        mnuRoutes.Enabled = False
        mnuRoutes.Image = CType(resources.GetObject("mnuRoutes.Image"), Image)
        mnuRoutes.Name = "mnuRoutes"
        mnuRoutes.Size = New Size(234, 34)
        mnuRoutes.Text = "Route Mgmt"
        ' 
        ' ToolStripSeparator8
        ' 
        ToolStripSeparator8.Name = "ToolStripSeparator8"
        ToolStripSeparator8.Size = New Size(231, 6)
        ' 
        ' mnuClientRpts
        ' 
        mnuClientRpts.Enabled = False
        mnuClientRpts.Image = CType(resources.GetObject("mnuClientRpts.Image"), Image)
        mnuClientRpts.Name = "mnuClientRpts"
        mnuClientRpts.Size = New Size(234, 34)
        mnuClientRpts.Text = "Client Reports"
        ' 
        ' mnuPayers
        ' 
        mnuPayers.DropDownItems.AddRange(New ToolStripItem() {mnuPayerMgmt, mnuPayerMapping, mnuPartners, ToolStripSeparator9, mnuPayerRpt, mnuPartnerRPT})
        mnuPayers.Image = CType(resources.GetObject("mnuPayers.Image"), Image)
        mnuPayers.Name = "mnuPayers"
        mnuPayers.Size = New Size(249, 34)
        mnuPayers.Text = "Payer Mgmt"
        ' 
        ' mnuPayerMgmt
        ' 
        mnuPayerMgmt.Enabled = False
        mnuPayerMgmt.Image = CType(resources.GetObject("mnuPayerMgmt.Image"), Image)
        mnuPayerMgmt.Name = "mnuPayerMgmt"
        mnuPayerMgmt.Size = New Size(279, 34)
        mnuPayerMgmt.Text = "Payer Management"
        ' 
        ' mnuPayerMapping
        ' 
        mnuPayerMapping.Enabled = False
        mnuPayerMapping.Image = CType(resources.GetObject("mnuPayerMapping.Image"), Image)
        mnuPayerMapping.Name = "mnuPayerMapping"
        mnuPayerMapping.Size = New Size(279, 34)
        mnuPayerMapping.Text = "Payer Mapping"
        ' 
        ' mnuPartners
        ' 
        mnuPartners.Enabled = False
        mnuPartners.Image = CType(resources.GetObject("mnuPartners.Image"), Image)
        mnuPartners.Name = "mnuPartners"
        mnuPartners.Size = New Size(279, 34)
        mnuPartners.Text = "Partner Management"
        ' 
        ' ToolStripSeparator9
        ' 
        ToolStripSeparator9.Name = "ToolStripSeparator9"
        ToolStripSeparator9.Size = New Size(276, 6)
        ' 
        ' mnuPayerRpt
        ' 
        mnuPayerRpt.Checked = True
        mnuPayerRpt.CheckState = CheckState.Checked
        mnuPayerRpt.Enabled = False
        mnuPayerRpt.Image = CType(resources.GetObject("mnuPayerRpt.Image"), Image)
        mnuPayerRpt.Name = "mnuPayerRpt"
        mnuPayerRpt.Size = New Size(279, 34)
        mnuPayerRpt.Text = "Payers Report"
        ' 
        ' mnuPartnerRPT
        ' 
        mnuPartnerRPT.Enabled = False
        mnuPartnerRPT.Image = CType(resources.GetObject("mnuPartnerRPT.Image"), Image)
        mnuPartnerRPT.Name = "mnuPartnerRPT"
        mnuPartnerRPT.Size = New Size(279, 34)
        mnuPartnerRPT.Text = "Partners Report"
        ' 
        ' mnuPatients
        ' 
        mnuPatients.DropDownItems.AddRange(New ToolStripItem() {mnuEditPat, mnuRemDupPat})
        mnuPatients.Image = CType(resources.GetObject("mnuPatients.Image"), Image)
        mnuPatients.Name = "mnuPatients"
        mnuPatients.Size = New Size(249, 34)
        mnuPatients.Text = "Patient Mgmt"
        ' 
        ' mnuEditPat
        ' 
        mnuEditPat.Enabled = False
        mnuEditPat.Image = CType(resources.GetObject("mnuEditPat.Image"), Image)
        mnuEditPat.Name = "mnuEditPat"
        mnuEditPat.Size = New Size(253, 34)
        mnuEditPat.Text = "Edit Patients"
        ' 
        ' mnuRemDupPat
        ' 
        mnuRemDupPat.Enabled = False
        mnuRemDupPat.Image = CType(resources.GetObject("mnuRemDupPat.Image"), Image)
        mnuRemDupPat.Name = "mnuRemDupPat"
        mnuRemDupPat.ShowShortcutKeys = False
        mnuRemDupPat.Size = New Size(253, 34)
        mnuRemDupPat.Text = "Remove Duplicates"
        ' 
        ' mnuOutSource
        ' 
        mnuOutSource.Checked = True
        mnuOutSource.CheckState = CheckState.Checked
        mnuOutSource.Enabled = False
        mnuOutSource.Image = CType(resources.GetObject("mnuOutSource.Image"), Image)
        mnuOutSource.Name = "mnuOutSource"
        mnuOutSource.Size = New Size(249, 34)
        mnuOutSource.Text = "OutSourcing"
        ' 
        ' mnuEquips
        ' 
        mnuEquips.Enabled = False
        mnuEquips.Image = CType(resources.GetObject("mnuEquips.Image"), Image)
        mnuEquips.Name = "mnuEquips"
        mnuEquips.ShowShortcutKeys = False
        mnuEquips.Size = New Size(249, 34)
        mnuEquips.Text = "Equipments"
        ' 
        ' mnuSysConfig
        ' 
        mnuSysConfig.Enabled = False
        mnuSysConfig.Image = CType(resources.GetObject("mnuSysConfig.Image"), Image)
        mnuSysConfig.Name = "mnuSysConfig"
        mnuSysConfig.Size = New Size(249, 34)
        mnuSysConfig.Text = "System Config"
        ' 
        ' mnuRptSetup
        ' 
        mnuRptSetup.DropDownItems.AddRange(New ToolStripItem() {mnuRptBuild, mnuRptOrder})
        mnuRptSetup.Image = CType(resources.GetObject("mnuRptSetup.Image"), Image)
        mnuRptSetup.Name = "mnuRptSetup"
        mnuRptSetup.Size = New Size(249, 34)
        mnuRptSetup.Text = "Report Setup"
        ' 
        ' mnuRptBuild
        ' 
        mnuRptBuild.Enabled = False
        mnuRptBuild.Image = CType(resources.GetObject("mnuRptBuild.Image"), Image)
        mnuRptBuild.Name = "mnuRptBuild"
        mnuRptBuild.Size = New Size(218, 34)
        mnuRptBuild.Text = "Report Build"
        ' 
        ' mnuRptOrder
        ' 
        mnuRptOrder.Enabled = False
        mnuRptOrder.Image = CType(resources.GetObject("mnuRptOrder.Image"), Image)
        mnuRptOrder.Name = "mnuRptOrder"
        mnuRptOrder.Size = New Size(218, 34)
        mnuRptOrder.Text = "Report Order"
        ' 
        ' mnuUserMgmt
        ' 
        mnuUserMgmt.Enabled = False
        mnuUserMgmt.Image = CType(resources.GetObject("mnuUserMgmt.Image"), Image)
        mnuUserMgmt.Name = "mnuUserMgmt"
        mnuUserMgmt.Size = New Size(249, 34)
        mnuUserMgmt.Text = "User Mgmt"
        ' 
        ' FacilityMgmtToolStripMenuItem
        ' 
        FacilityMgmtToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {mnuFacility, mnuDept})
        FacilityMgmtToolStripMenuItem.Image = CType(resources.GetObject("FacilityMgmtToolStripMenuItem.Image"), Image)
        FacilityMgmtToolStripMenuItem.Name = "FacilityMgmtToolStripMenuItem"
        FacilityMgmtToolStripMenuItem.Size = New Size(249, 34)
        FacilityMgmtToolStripMenuItem.Text = "Facility Mgmt"
        ' 
        ' mnuFacility
        ' 
        mnuFacility.Enabled = False
        mnuFacility.Image = CType(resources.GetObject("mnuFacility.Image"), Image)
        mnuFacility.Name = "mnuFacility"
        mnuFacility.Size = New Size(217, 34)
        mnuFacility.Text = "Facility Setup"
        ' 
        ' mnuDept
        ' 
        mnuDept.Enabled = False
        mnuDept.Image = CType(resources.GetObject("mnuDept.Image"), Image)
        mnuDept.Name = "mnuDept"
        mnuDept.Size = New Size(217, 34)
        mnuDept.Text = "Dept Setup"
        ' 
        ' mnuCodes
        ' 
        mnuCodes.DropDownItems.AddRange(New ToolStripItem() {mnuPhrases, mnuDxs, mnuCPTs})
        mnuCodes.Image = CType(resources.GetObject("mnuCodes.Image"), Image)
        mnuCodes.Name = "mnuCodes"
        mnuCodes.Size = New Size(249, 34)
        mnuCodes.Text = "Code Mgmt"
        ' 
        ' mnuPhrases
        ' 
        mnuPhrases.Enabled = False
        mnuPhrases.Image = CType(resources.GetObject("mnuPhrases.Image"), Image)
        mnuPhrases.Name = "mnuPhrases"
        mnuPhrases.Size = New Size(190, 34)
        mnuPhrases.Text = "Phrases"
        ' 
        ' mnuDxs
        ' 
        mnuDxs.Enabled = False
        mnuDxs.Image = CType(resources.GetObject("mnuDxs.Image"), Image)
        mnuDxs.Name = "mnuDxs"
        mnuDxs.Size = New Size(190, 34)
        mnuDxs.Text = "Dx Codes"
        ' 
        ' mnuCPTs
        ' 
        mnuCPTs.Enabled = False
        mnuCPTs.Image = CType(resources.GetObject("mnuCPTs.Image"), Image)
        mnuCPTs.Name = "mnuCPTs"
        mnuCPTs.Size = New Size(190, 34)
        mnuCPTs.Text = "CPT"
        ' 
        ' mnuWorksetup
        ' 
        mnuWorksetup.Enabled = False
        mnuWorksetup.Image = CType(resources.GetObject("mnuWorksetup.Image"), Image)
        mnuWorksetup.Name = "mnuWorksetup"
        mnuWorksetup.Size = New Size(249, 34)
        mnuWorksetup.Text = "Worksheet Setup"
        ' 
        ' mnuHelp
        ' 
        mnuHelp.DropDownItems.AddRange(New ToolStripItem() {mnuLocalHelp, mnunetHelp, mnuSupport, mnuAbout, RequisitionCopyToolStripMenuItem, UpdateResultNoteInTestsToolStripMenuItem})
        mnuHelp.ForeColor = Color.DarkBlue
        mnuHelp.Image = CType(resources.GetObject("mnuHelp.Image"), Image)
        mnuHelp.ImageScaling = ToolStripItemImageScaling.None
        mnuHelp.Name = "mnuHelp"
        mnuHelp.Size = New Size(97, 36)
        mnuHelp.Text = "&Help"
        ' 
        ' mnuLocalHelp
        ' 
        mnuLocalHelp.Name = "mnuLocalHelp"
        mnuLocalHelp.Size = New Size(330, 34)
        mnuLocalHelp.Text = "Prolis Help"
        ' 
        ' mnunetHelp
        ' 
        mnunetHelp.Name = "mnunetHelp"
        mnunetHelp.Size = New Size(330, 34)
        mnunetHelp.Text = "Context F1 Help"
        ' 
        ' mnuSupport
        ' 
        mnuSupport.Name = "mnuSupport"
        mnuSupport.Size = New Size(330, 34)
        mnuSupport.Text = "Support Tech"
        ' 
        ' mnuAbout
        ' 
        mnuAbout.Name = "mnuAbout"
        mnuAbout.Size = New Size(330, 34)
        mnuAbout.Text = "About Prolis"
        ' 
        ' RequisitionCopyToolStripMenuItem
        ' 
        RequisitionCopyToolStripMenuItem.Name = "RequisitionCopyToolStripMenuItem"
        RequisitionCopyToolStripMenuItem.Size = New Size(330, 34)
        RequisitionCopyToolStripMenuItem.Text = "Requisition_Copy"
        RequisitionCopyToolStripMenuItem.Visible = False
        ' 
        ' UpdateResultNoteInTestsToolStripMenuItem
        ' 
        UpdateResultNoteInTestsToolStripMenuItem.Name = "UpdateResultNoteInTestsToolStripMenuItem"
        UpdateResultNoteInTestsToolStripMenuItem.Size = New Size(330, 34)
        UpdateResultNoteInTestsToolStripMenuItem.Text = "Update Result Note in Tests"
        UpdateResultNoteInTestsToolStripMenuItem.Visible = False
        ' 
        ' ToolStrip
        ' 
        ToolStrip.Dock = DockStyle.Left
        ToolStrip.ImageScalingSize = New Size(24, 24)
        ToolStrip.Items.AddRange(New ToolStripItem() {btnLogin, btnLogout, ToolStripSeparator1, btnMessages, btnRequisitions, btnResultInquiry, btnResultEntry, btnReports, ToolStripButton1, ToolStripButton2})
        ToolStrip.Location = New Point(0, 40)
        ToolStrip.Name = "ToolStrip"
        ToolStrip.Padding = New Padding(7, 0, 3, 0)
        ToolStrip.Size = New Size(56, 1100)
        ToolStrip.TabIndex = 6
        ToolStrip.Text = "ToolStrip"
        ' 
        ' btnLogin
        ' 
        btnLogin.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnLogin.Image = CType(resources.GetObject("btnLogin.Image"), Image)
        btnLogin.ImageAlign = ContentAlignment.MiddleLeft
        btnLogin.ImageTransparentColor = Color.Black
        btnLogin.Name = "btnLogin"
        btnLogin.Size = New Size(35, 28)
        btnLogin.Text = "Login"
        btnLogin.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' btnLogout
        ' 
        btnLogout.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnLogout.Enabled = False
        btnLogout.Image = CType(resources.GetObject("btnLogout.Image"), Image)
        btnLogout.ImageAlign = ContentAlignment.MiddleLeft
        btnLogout.ImageTransparentColor = Color.Black
        btnLogout.Name = "btnLogout"
        btnLogout.Size = New Size(35, 28)
        btnLogout.Text = "Logout"
        btnLogout.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(35, 6)
        ' 
        ' btnMessages
        ' 
        btnMessages.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnMessages.Enabled = False
        btnMessages.Image = CType(resources.GetObject("btnMessages.Image"), Image)
        btnMessages.ImageAlign = ContentAlignment.MiddleLeft
        btnMessages.ImageTransparentColor = Color.Black
        btnMessages.Name = "btnMessages"
        btnMessages.Size = New Size(35, 28)
        btnMessages.Text = "Messages"
        btnMessages.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' btnRequisitions
        ' 
        btnRequisitions.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnRequisitions.Enabled = False
        btnRequisitions.Image = CType(resources.GetObject("btnRequisitions.Image"), Image)
        btnRequisitions.ImageAlign = ContentAlignment.MiddleLeft
        btnRequisitions.ImageTransparentColor = Color.Black
        btnRequisitions.Name = "btnRequisitions"
        btnRequisitions.Size = New Size(35, 28)
        btnRequisitions.Text = "Accession"
        btnRequisitions.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' btnResultInquiry
        ' 
        btnResultInquiry.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnResultInquiry.Enabled = False
        btnResultInquiry.Image = CType(resources.GetObject("btnResultInquiry.Image"), Image)
        btnResultInquiry.ImageAlign = ContentAlignment.MiddleLeft
        btnResultInquiry.ImageScaling = ToolStripItemImageScaling.None
        btnResultInquiry.ImageTransparentColor = Color.Magenta
        btnResultInquiry.Name = "btnResultInquiry"
        btnResultInquiry.Size = New Size(35, 36)
        btnResultInquiry.Text = "Result Inquiry"
        btnResultInquiry.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' btnResultEntry
        ' 
        btnResultEntry.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnResultEntry.Enabled = False
        btnResultEntry.Image = CType(resources.GetObject("btnResultEntry.Image"), Image)
        btnResultEntry.ImageAlign = ContentAlignment.MiddleLeft
        btnResultEntry.ImageTransparentColor = Color.Magenta
        btnResultEntry.Name = "btnResultEntry"
        btnResultEntry.Size = New Size(35, 28)
        btnResultEntry.Text = "Result Entry"
        btnResultEntry.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' btnReports
        ' 
        btnReports.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnReports.Enabled = False
        btnReports.Image = CType(resources.GetObject("btnReports.Image"), Image)
        btnReports.ImageAlign = ContentAlignment.MiddleLeft
        btnReports.ImageScaling = ToolStripItemImageScaling.None
        btnReports.ImageTransparentColor = Color.Magenta
        btnReports.Name = "btnReports"
        btnReports.Size = New Size(35, 36)
        btnReports.Text = "Reporting"
        btnReports.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' ToolStripButton1
        ' 
        ToolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton1.Image = My.Resources.Resources.ViewHistory
        ToolStripButton1.ImageTransparentColor = Color.Magenta
        ToolStripButton1.Name = "ToolStripButton1"
        ToolStripButton1.Size = New Size(35, 28)
        ToolStripButton1.Text = "ToolStripButton1"
        ' 
        ' StatusStrip
        ' 
        StatusStrip.BackColor = SystemColors.Control
        StatusStrip.ImageScalingSize = New Size(24, 24)
        StatusStrip.Items.AddRange(New ToolStripItem() {lblUser, lblStatus, lblConnection})
        StatusStrip.Location = New Point(0, 1140)
        StatusStrip.Name = "StatusStrip"
        StatusStrip.Padding = New Padding(3, 0, 23, 0)
        StatusStrip.Size = New Size(1530, 24)
        StatusStrip.TabIndex = 7
        StatusStrip.Text = "StatusStrip"
        ' 
        ' lblUser
        ' 
        lblUser.Name = "lblUser"
        lblUser.Size = New Size(0, 17)
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = False
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(200, 17)
        ' 
        ' lblConnection
        ' 
        lblConnection.AutoSize = False
        lblConnection.Name = "lblConnection"
        lblConnection.Size = New Size(120, 17)
        ' 
        ' Timer1
        ' 
        Timer1.Interval = 500
        ' 
        ' lblAlert
        ' 
        lblAlert.AutoSize = True
        lblAlert.BackColor = Color.AliceBlue
        lblAlert.Font = New Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblAlert.ForeColor = Color.Red
        lblAlert.Location = New Point(169, 106)
        lblAlert.Margin = New Padding(4, 0, 4, 0)
        lblAlert.Name = "lblAlert"
        lblAlert.Size = New Size(67, 29)
        lblAlert.TabIndex = 12
        lblAlert.Text = "Alert"
        lblAlert.Visible = False
        ' 
        ' HelpProvider1
        ' 
        HelpProvider1.HelpNamespace = "ProlisHelp.chm"
        ' 
        ' TestingToolStripMenuItem
        ' 
        TestingToolStripMenuItem.Name = "TestingToolStripMenuItem"
        TestingToolStripMenuItem.Size = New Size(270, 34)
        TestingToolStripMenuItem.Text = "Testing"
        ' 
        ' ToolStripButton2
        ' 
        ToolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton2.Image = My.Resources.Resources.icons8_waiting_24
        ToolStripButton2.ImageTransparentColor = Color.Magenta
        ToolStripButton2.Name = "ToolStripButton2"
        ToolStripButton2.Size = New Size(35, 28)
        ToolStripButton2.Text = "ToolStripButton2"
        ' 
        ' frmDashboard
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        AutoSizeMode = AutoSizeMode.GrowAndShrink
        BackColor = Color.LightCyan
        BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), Image)
        BackgroundImageLayout = ImageLayout.Stretch
        ClientSize = New Size(1530, 1164)
        Controls.Add(lblAlert)
        Controls.Add(ToolStrip)
        Controls.Add(MenuStrip)
        Controls.Add(StatusStrip)
        HelpButton = True
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        IsMdiContainer = True
        KeyPreview = True
        MainMenuStrip = MenuStrip
        Margin = New Padding(4, 6, 4, 6)
        MinimumSize = New Size(1284, 1021)
        Name = "frmDashboard"
        ProlisHelp.SetShowHelp(Me, True)
        Text = "Prolis"
        WindowState = FormWindowState.Maximized
        MenuStrip.ResumeLayout(False)
        MenuStrip.PerformLayout()
        ToolStrip.ResumeLayout(False)
        ToolStrip.PerformLayout()
        StatusStrip.ResumeLayout(False)
        StatusStrip.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents mnuBillingAR As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnRequisitions As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents lblUser As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents btnMessages As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnLogin As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents btnLogout As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents QCExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLogin As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents YouMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLogout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuCS As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAccession As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAnalysis As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuResInq As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDictionary As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnResultInquiry As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuMsg As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnResultEntry As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnReports As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuSvc As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAnalyteManagement As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuClients As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPayers As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuGroups As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuProfiles As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAnalysisSetup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPatients As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuOutSource As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEquips As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSysConfig As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRptSetup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUserMgmt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRequisitions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FacilityMgmtToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFacility As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDept As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBatch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuApplyRes As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuEnterRes As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuResReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuQCReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEResults As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMyReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuProviders As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSales As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRoutes As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRemoteAcc As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuARC As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBilling As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuART As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPayments As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuStatement As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPBReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBillDataSynch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTestInq As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPickup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPickupRpt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuARInq As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBillEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPrintLabels As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPrintLog As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCodes As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPhrases As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDxs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCPTs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProlisHelp As System.Windows.Forms.HelpProvider
    Friend WithEvents mnuBillSep1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuBatchBill As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuBillOut As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPostBill As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuWorksheets As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuOrders As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuInitialize As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPhlebotomy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPrintReqs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRefluxResults As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuCompRpts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuClientRpts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPayerMgmt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPayerRpt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPartners As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPartnerRPT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEbillout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBillReports As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuManagePanics As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBatchResults As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuWorksetup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuSendOuts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLocalHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnunetHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSupport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAccDash As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRptBuild As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRptOrder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuRptDash As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuProlisOnControl As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuProvInquiry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuATRResults As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPrintSendouts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPanicMgmt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPrintPanicReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPublish As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuImportNecessity As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator13 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuApplyResRefLab As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents lblAlert As System.Windows.Forms.Label
    Friend WithEvents mnuEditPat As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRemDupPat As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator14 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuBillReqs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSuperBills As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator16 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPreAnalytics As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuProcess As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator17 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuScrubber As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuQCMgmt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator18 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuQuickQuote As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAdhoc As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuERA As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuResultDash As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPayerMapping As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblConnection As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents mnuDebits As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPanicHistory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMyRptSchedule As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents mnuCompReimburse As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuChargesPayments As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuChargesByClients As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPmtsCollectedByPaymentDate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator19 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPmtByClients As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuInternalReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAccessionSummary As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator20 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuAccessionsRejected As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPreAuth As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuImportPrices As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBarAdhoc As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator15 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuUpdateRoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUpdateMileageCode As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCreditCards As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAutoAccessions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator21 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DeltaCheckReqReultsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProcessedERAToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPaymentsCollectedByPostedDates As ToolStripMenuItem
    Friend WithEvents RequisitionCopyToolStripMenuItem As ToolStripMenuItem
 
    Friend WithEvents UpdateResultNoteInTestsToolStripMenuItem As ToolStripMenuItem
 
    Friend WithEvents TestingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents ToolStripButton2 As ToolStripButton

End Class