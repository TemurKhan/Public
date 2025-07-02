Option Compare Text
Imports System.Drawing.Printing
Imports System.Text
Imports System.Data
Imports System.Globalization
Imports System.Reflection
Imports System.SqlClient
Imports Newtonsoft.Json
Imports Microsoft.Data.SqlClient

Public Class frmRequisitions
    'Public DymoAddIn As Dymo.DymoAddIn
    'Public DymoLabel As Dymo.DymoLabels
    Private SearchMode As Boolean = False
    Private origWidth As Integer
    Private origHeight As Integer
    Private gBilled As Boolean = False
    Private Curow As Integer
    Public CurTGP As String
    Public Shared PMT() As String = {"", "", "", "", "", "", "", "", "", "", "", "", "", "", ""}
    Public TCode As String
    Private Sources As String = "Service, "
    Private Accs As DataTable
    'Private RsA As New ADODB.Recordset
    Private mainQuery As String = ""
    Private dtRecords As DataTable
    Private Accessions As Integer
    Private ReqInfoResp As String = ""
    Private CurrAcc As Integer
    Private ExistTGPs() As String = {""}
    Private ReqDirty As Boolean = False
    Private ProviderDirty As Boolean = False
    Private MyAttender As Long = -1
    Private PatDirty As Boolean = False
    Private DxDirty As Boolean = False
    Private OrderDirty As Boolean = False
    Private RptDirty As Boolean = False
    Private BillDirty As Boolean = False
    Private AccPrevVals As String = ""
    Private UnreceivedSrcs As String = ""
    Private MergeAccs As Boolean = False

    Public Shared Req_Info_Response_MainList As New List(Of String)

    Private Sub frmRequisitions_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        On Error Resume Next
        'If RsA IsNot Nothing Then
        '    If RsA.State = 1 Then RsA.Close()
        '    RsA = Nothing
        'End If
    End Sub

    Private Sub frmRequisitions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.KeyPreview = True

        If frmPatient.IsHandleCreated Then frmPatient.Close()
        Me.MaximumSize = MaxSize
        'ResizeToClient(Me)
        SearchMode = False
        InitializeConfiguration(MyLab.ID)
        If SystemConfig.AccSeed <> "" Then
            txtAccFrom.MaxLength = SystemConfig.AccSeed.Length
            txtAccID.MaxLength = SystemConfig.AccSeed.Length
            txtAccTo.MaxLength = SystemConfig.AccSeed.Length
        End If
        dtpDate.Format = DateTimePickerFormat.Custom
        dtpDate.CustomFormat = SystemConfig.DateFormat
        lblAccDate.Text += " (" & SystemConfig.DateFormat & ")"
        dtpDate.Value = Date.Today
        txtTime.Text = Format(Date.Now, "HH:mm")
        dtpDateDrawn.Format = DateTimePickerFormat.Custom
        dtpDateDrawn.CustomFormat = SystemConfig.DateFormat
        dtpDateDrawn.Value = dtpDate.Value
        cmbSpecimenType.SelectedIndex = 0
        txtRecDate.Text = Format(dtpDate.Value, SystemConfig.DateFormat)
        txtRecTime.Text = txtTime.Text
        txtAccID.Text = NextAccessionID(dtpDate.Value, txtPatientID.Text)
        txtDOB.Mask = Replace(Replace(Replace(SystemConfig.DateFormat, "y", "0"), "M", "0"), "d", "0")
        txtRecDate.Mask = Replace(Replace(Replace(SystemConfig.DateFormat, "y", "0"), "M", "0"), "d", "0")

        cmbSex.Items.Clear()
        If SystemConfig.DiagTarget = "V" Then
            cmbSex.Items.Add("F - Female")
            cmbSex.Items.Add("M - Male")
            cmbSex.Items.Add("N - Neutered")
            cmbSex.Items.Add("S - Spayed")
            tcDxMeds.Visible = False
        Else
            tcDxMeds.Visible = True
            cmbSex.Items.Add("F - Female")
            cmbSex.Items.Add("M - Male")
            cmbSex.Items.Add("G - Transgender Female")
            cmbSex.Items.Add("N - Transgender Male")
            cmbSex.Items.Add("I - Indetermined")
            cmbSex.Items.Add("U - Unreported")
        End If
        Dim DS As String = Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.DateSeparator.ToString
        txtDateFrom.Mask = "00" & DS & "00" & DS & "0000"
        txtDateTo.Mask = "00" & DS & "00" & DS & "0000"
        Dim datemask As String = ""
        txtPatHPhone.Mask = SystemConfig.PhoneMask
        txtWPhone.Mask = SystemConfig.PhoneMask
        txtCell.Mask = SystemConfig.PhoneMask
        txtFax.Mask = SystemConfig.PhoneMask
        txtOrdPhone.Mask = SystemConfig.PhoneMask
        txtOrdFax.Mask = SystemConfig.PhoneMask
        txtPsubHPhone.Mask = SystemConfig.PhoneMask
        txtSSubPhone.Mask = SystemConfig.PhoneMask
        txtRPTFax.Mask = SystemConfig.PhoneMask
        txtGPhone.Mask = SystemConfig.PhoneMask
        If SystemConfig.DiagTarget = "V" Then
            gbVeterinary.Visible = True
            PopulateSpecies()
            cmbSpecies.SelectedIndex = 0
            btnRemDxAll.Enabled = False
        Else
            gbVeterinary.Visible = False
            btnRemDxAll.Enabled = True
        End If
        cmbGRelation.Items.Clear()
        cmbGRelation.Items.Add("Self")
        cmbGRelation.Items.Add("Spouse")
        cmbGRelation.Items.Add("Son/Daughter")
        cmbGRelation.Items.Add("Other(Dependent)")
        'txtAccessionedBy.Text = ThisUser.Name
        txtAnalysisStage.Text = GetAnalysisStage(0)
        chkInHouse.Enabled = ThisUser.Supervisor
        'txtEditOn.Text = Format(Date.Now, "MM/dd/yyyy hh:mm tt")
        If LIC.Bill = True Then
            BillingLicEnabled()
            If SystemConfig.DefaultBilling = 0 Then 'C
                rbC.Select()
            ElseIf SystemConfig.DefaultBilling = 1 Then 'T
                rbT.Select()
            Else
                rbP.Select()
            End If
        Else
            BillingLicDisabled()
        End If
        UpdateReqStatus()
        PopulateSources()
        PopulateRaces(cmbRace)
        cmbRace.SelectedIndex = 5
        cmbEthnicity.SelectedIndex = 2
        chkProfile.Checked = SystemConfig.ProfileBreak
        dgvTGPMarked.RowCount = 100
        dgvMeds.RowCount = 1
        dgvDxs.RowCount = 1
        For ii As Integer = 0 To dgvMeds.RowCount - 1
            If dgvMeds.Rows(ii).Cells(0).Value Is Nothing Or dgvMeds.Rows(ii).Cells(0).Value = "" Then
                dgvMeds.Rows(ii).Cells(2).Value = Nothing
            End If
        Next
        For ii As Integer = 0 To dgvMeds.RowCount - 1
            If dgvDxs.Rows(ii).Cells(0).Value Is Nothing Or dgvDxs.Rows(ii).Cells(0).Value = "" Then
                dgvDxs.Rows(ii).Cells(2).Value = Nothing
            End If
        Next

        If String.IsNullOrEmpty(MyLab.QRSec.Token) Or String.IsNullOrEmpty(MyLab.QRSec.DNS) Then
            QR.Qualify = False
            QrChk.Enabled = False
        Else
            QrChk.Enabled = True
            QR.Qualify = True
            QrChk.Checked = MyLab.QRSec.QRChecked

        End If

        barcode.Visible = ColorAPI.Active
        SendTocolorBtn.Visible = ColorAPI.Active
        'UpdatePrimResp()
        Dim i As Integer
        For i = 0 To cmbSource.Items.Count - 1
            If cmbSource.Items(i).ToString = SystemConfig.DefSource Then
                cmbSource.SelectedIndex = i
                Exit For
            End If
        Next
        For i = 0 To cmbTemp.Items.Count - 1
            If cmbTemp.Items(i).ToString = SystemConfig.DefTemp Then
                cmbTemp.SelectedIndex = i
                Exit For
            End If
        Next
        txtLabels.Text = CalculateLabels()
        'TabControl1.SelectedTab = tpSpecimen
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)

    End Sub
    Private Sub frmRequisitions_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        ' Check if Ctrl key is pressed
        If e.Control Then

            If e.KeyCode = Keys.Right Then
                MoveToNextTab()
                e.SuppressKeyPress = True
            End If

            If e.KeyCode = Keys.Left Then
                MoveToPreviousTab()
                e.SuppressKeyPress = True
            End If
        End If
    End Sub

    Private Sub MoveToNextTab()
        If TabControl1.SelectedIndex < TabControl1.TabCount - 1 Then
            TabControl1.SelectedIndex += 1
        End If
    End Sub

    Private Sub MoveToPreviousTab()
        If TabControl1.SelectedIndex > 0 Then
            TabControl1.SelectedIndex -= 1
        End If
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

    Private Sub BillingLicEnabled()
        dgvDxs.RowCount = 20
        chkSvcGratis.Enabled = True
        rbC.Enabled = True
        rbT.Enabled = True
        rbP.Enabled = True
        'btnCalculate.Enabled = True
        'btnPmnt.Enabled = True
        chkPhlebotomy.Enabled = True
        chkHomeBound.Enabled = True
        chkCare.Enabled = True
    End Sub

    Private Sub BillingLicDisabled()
        chkSvcGratis.Enabled = False
        rbC.Checked = True
        rbC.Enabled = False
        rbT.Enabled = False
        rbP.Enabled = False
        btnCalculate.Enabled = False
        btnPmnt.Enabled = False
        chkPhlebotomy.Enabled = False
        chkHomeBound.Enabled = False
        chkCare.Enabled = False
    End Sub

    Friend Sub PopulateSources()
        Dim sSQL As String = "Select * from Sources"
        '
        Dim cnsr As New SqlConnection(connString)
        cnsr.Open()
        Dim cmdsr As New SqlCommand(sSQL, cnsr)
        cmdsr.CommandType = CommandType.Text
        Dim drsr As SqlDataReader = cmdsr.ExecuteReader
        If drsr.HasRows Then
            While drsr.Read
                cmbSource.Items.Add(New MyList(drsr("Name"), drsr("ID")))
            End While
        End If
        cnsr.Close()
        cnsr = Nothing
    End Sub

    Private Function NextAccessionID(ByVal AccDate As Date, ByVal PatID As String) As Long
        Dim AccID As String = ""
        Dim MinAccID As Long = 1
        Dim MaxAccID As Long = -1
        Dim sSQL As String = ""
        '
        If SystemConfig.AccSeed <> "" Then
            Dim AccSeed As String = SystemConfig.AccSeed
            MinAccID = CLng(Format(AccDate, AccSeed))
            If AccSeed.Substring(0, InStr(AccSeed, "0") - 1) = "yyMMdd" Then 'daily
                MaxAccID = CLng(Format(DateAdd(DateInterval.Day, 1, AccDate), AccSeed) - 2)
            ElseIf AccSeed.Substring(0, InStr(AccSeed, "0") - 1) = "yyMM" Then 'monthly
                MaxAccID = CLng(Format(DateAdd(DateInterval.Month, 1, AccDate), AccSeed) - 2)
            ElseIf AccSeed.Substring(0, InStr(AccSeed, "0") - 1) = "yy" Then 'yearly
                MaxAccID = CLng(Format(DateAdd(DateInterval.Year, 1, AccDate), AccSeed) - 2)
            End If
            If MaxAccID <> -1 And MinAccID <> -1 Then _
            sSQL = "Select max(ID) as LastID from Requisitions " &
            "where ID >= " & MinAccID & " and ID < " & MaxAccID
        Else
            sSQL = "Select max(ID) as LastID from Requisitions"
        End If
        '
        Dim cnna As New SqlConnection(connString)
        cnna.Open()
        Dim cmdna As New SqlCommand(sSQL, cnna)
        cmdna.CommandType = CommandType.Text
        Dim drna As SqlDataReader = cmdna.ExecuteReader
        If drna.HasRows Then
            While drna.Read
                If drna("LastID") Is DBNull.Value Then
                    AccID = MinAccID.ToString
                Else
                    AccID = CStr(drna("LastID") + 1)
                End If
            End While
        End If
        cnna.Close()
        cnna = Nothing
        Return CLng(AccID)
    End Function

    Private Sub cmbSpecimenType_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        cmbSpecimenType.BackColor = FCOLOR
    End Sub

    Private Sub cmbSpecimenType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbSpecimenType.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.PageUp Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub cmbSpecimenType_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        cmbSpecimenType.BackColor = NFCOLOR
    End Sub

    Private Sub cmbSpecimenType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSpecimenType.SelectedIndexChanged
        If cmbSpecimenType.SelectedIndex = 0 Then
            grpPatient.Enabled = True
        Else
            ClearPatient()
            grpPatient.Enabled = False
        End If
        Update_Patient_Status()
        UpdateRequisitionProgress()
    End Sub

    Private Sub ClearForm()
        txtAccID.Text = ""
        txtRequisition.Text = ""
        dtpDate.Value = Date.Today
        dtpDateDrawn.Value = dtpDate.Value
        txtTime.Text = Format(Date.Now, "HH:mm")
        txtDrawnTime.Text = txtTime.Text
        cmbSpecimenType.SelectedIndex = 0
        txtRecDate.Text = ""
        txtRecTime.Text = ""
        chkReject.Checked = False
        MergeAccs = False
        'txtAccessionedBy.Text = ""
        Dim i As Integer
        For i = 0 To cmbSource.Items.Count - 1
            If cmbSource.Items(i).ToString = SystemConfig.DefSource Then
                cmbSource.SelectedIndex = i
                Exit For
            End If
        Next
        For i = 0 To cmbTemp.Items.Count - 1
            If cmbTemp.Items(i).ToString = SystemConfig.DefTemp Then
                cmbTemp.SelectedIndex = i
                Exit For
            End If
        Next
        txtAnalysisStage.Text = ""
        'txtLastEditedBy.Text = ""
        'txtEditOn.Text = ""
        UnreceivedSrcs = ""
        txtInEditReason.Text = ""
        chkInHouse.Checked = True
        chkInHouse.Text = "Yes"
        txtWorkCmnt.Text = ""
        txtComment.Text = ""
        lblRequisition.BackColor = Color.PeachPuff
        dgvSources.Rows.Clear()
        Update_SourceString()
        txtLabels.Text = CalculateLabels()
        lblSpecimen.BackColor = Color.PeachPuff
        'txtOrdID.Text = ""
        gBilled = False
        ClearOrderer() : grpClient.Enabled = True : lstProviders.Enabled = True
        ClearPatient() : grpPatient.Enabled = True
        ClearDxs()
        ClearMeds()
        chkVerbal.CheckState = CheckState.Unchecked
        ClearOrders() : dgvTGPMarked.ReadOnly = False
        chkCare.Enabled = True
        chkHomeBound.Enabled = True
        chkPhlebotomy.Enabled = True
        chkProfile.Enabled = True
        chkVerbal.Enabled = True
        chkProfile.Checked = SystemConfig.ProfileBreak
        dgvRptProviders.Rows.Clear()
        gbReports.Enabled = True
        lblReports.BackColor = Color.PeachPuff
        rbC.Checked = True
        txtCopay.Text = ""
        txtPayment.Text = ""
        chkPhlebotomy.Checked = False
        rbC.Enabled = True
        rbT.Enabled = True
        rbP.Enabled = True
        grpPrimary.Enabled = True
        grpPSubs.Enabled = True
        grpSecondary.Enabled = True
        grpSSubs.Enabled = True
        ClearGuarantor()
        CurTGP = ""
        AccPrevVals = ""
        chkSvcGratis.Checked = False
        MyAttender = -1
        ReDim ExistTGPs(0)
        lblOrderer.BackColor = Color.PeachPuff
        UpdateRequisitionProgress()
    End Sub

    Private Sub btnNew_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.CheckedChanged
        If btnNew.Checked = False Then
            btnNew.Text = "New"
            btnNew.Image = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Images\New.ico")
            ClearForm()
            txtAccID.Text = NextAccessionID(dtpDate.Value, txtPatientID.Text)
            txtAccID.ReadOnly = False
            lblRequisition.BackColor = Color.PaleGreen
            grpSearch.Enabled = False
            'If RsA.State = 1 Then RsA.Close()
            txtNavStatus.Text = ""
            btnAccLook.Enabled = False
            SearchMode = False
            chkPostPrePhleb.Enabled = True
            txtRecDate.Text = Format(dtpDate.Value, SystemConfig.DateFormat)
            txtRecTime.Text = txtTime.Text
            txtAccID.Focus()
        Else
            btnNew.Text = "Edit"
            txtAccID.ReadOnly = True
            btnNew.Image = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Images\Edit.ico")
            ClearForm()
            btnAccLook.Enabled = True
            grpSearch.Enabled = True
            SearchMode = True
            chkPostPrePhleb.Enabled = False
            txtDateFrom.Focus()
        End If
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnSources_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSources.Click
        frmSources.ShowDialog()
    End Sub

    Private Sub btnAddSrc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSrc.Click
        Dim ItemX As MyList
        Dim i As Integer
        If cmbSource.SelectedIndex <> -1 And txtQty.Text <> "" And IsDate(txtDrawnTime.Text) And
        cmbTemp.Text <> "" And txtAccID.Text <> "" Then

            ItemX = cmbSource.SelectedItem
            If Not SourceSelected(ItemX.ItemData) Then
                dgvSources.Rows.Add(ItemX.ItemData, txtAccID.Text & "-" & CStr(dgvSources.RowCount _
                + 1), ItemX.Name, txtQty.Text, Format(dtpDateDrawn.Value, SystemConfig.DateFormat),
                txtDrawnTime.Text, cmbTemp.SelectedItem.ToString, txtSrcComment.Text)
                cmbSource.SelectedIndex = -1

                txtQty.Text = ""
                For i = 0 To cmbTemp.Items.Count - 1
                    If cmbTemp.Items(i).ToString = SystemConfig.DefTemp Then
                        cmbTemp.SelectedIndex = i
                        Exit For
                    End If
                Next
                'cmbTemp.SelectedIndex = 0
                txtSrcComment.Text = ""
                Dim Labels As Integer = CalculateLabels()
                txtLabels.Text = Labels.ToString
                cmbSource.Focus()
                btnRemAllSrc.Enabled = True

                btnAddSrc.Enabled = False


                'Dim lastRowIndex As Integer = dgvSources.Rows.Count - 1
                'Dim FFF = (CType(dgvSources.Rows(lastRowIndex).Cells(8), DataGridViewComboBoxCell))
                'FFF.Items.Add("Refrigerated")
                'FFF.Items.Add("Room Temp")
                'FFF.Items.Add("Frozen")
                'FFF.Items.Add("Incubated")
                'dgvSources.Rows(lastRowIndex).Cells(8) = FFF

            Else
                MsgBox("Duplicate Entry not allowed!", MsgBoxStyle.Critical, "Prolis")
                cmbSource.Focus()
            End If
        Else
            MsgBox("Some required fields were not filled")
        End If
        Update_SourceString()
        Update_Specimen_Status()
        UpdateRequisitionProgress()
    End Sub

    Private Function CalculateLabels() As Integer
        Dim Labels As Integer = 0
        Dim AddLabels As Integer = GetAddLabels()
        Dim i As Integer
        For i = 0 To dgvSources.RowCount - 1
            Labels += CInt(dgvSources.Rows(i).Cells(3).Value)
        Next
        Return Labels + AddLabels
    End Function

    Private Function GetAddLabels() As Integer
        Dim AddLabels As Integer = 0
        If ThisUser.UseRemotePrinter Then
            Dim sSQL As String = "Select AddLabels from LabelPrinters where UserID = '" & ThisUser.Name.Trim() & "' "
            If connString <> "" Then
                Dim cnl As New SqlConnection(connString)
                cnl.Open()
                Dim cmdl As New SqlCommand(sSQL, cnl)
                cmdl.CommandType = CommandType.Text
                Dim drl As SqlDataReader = cmdl.ExecuteReader
                If drl.HasRows Then
                    While drl.Read
                        AddLabels = drl("AddLabels")
                    End While
                End If
                cnl.Close()
                cnl = Nothing
            Else
                Dim cnl As New Odbc.OdbcConnection(connString)
                cnl.Open()
                Dim cmdl As New Odbc.OdbcCommand(sSQL, cnl)
                cmdl.CommandType = CommandType.Text
                Dim drl As Odbc.OdbcDataReader = cmdl.ExecuteReader
                If drl.HasRows Then
                    While drl.Read
                        AddLabels = drl("AddLabels")
                    End While
                End If
                cnl.Close()
                cnl = Nothing
            End If
            Return AddLabels
        Else
            Dim sSQL As String = "Select AddLabels from LabelPrinters where PC_Name = '" & My.Computer.Name & "'"
            If connString <> "" Then
                Dim cnl As New SqlConnection(connString)
                cnl.Open()
                Dim cmdl As New SqlCommand(sSQL, cnl)
                cmdl.CommandType = CommandType.Text
                Dim drl As SqlDataReader = cmdl.ExecuteReader
                If drl.HasRows Then
                    While drl.Read
                        AddLabels = drl("AddLabels")
                    End While
                End If
                cnl.Close()
                cnl = Nothing
            Else
                Dim cnl As New Odbc.OdbcConnection(connString)
                cnl.Open()
                Dim cmdl As New Odbc.OdbcCommand(sSQL, cnl)
                cmdl.CommandType = CommandType.Text
                Dim drl As Odbc.OdbcDataReader = cmdl.ExecuteReader
                If drl.HasRows Then
                    While drl.Read
                        AddLabels = drl("AddLabels")
                    End While
                End If
                cnl.Close()
                cnl = Nothing
            End If
        End If

        Return AddLabels
    End Function

    Private Function SourceSelected(ByVal Source_ID As Integer) As Boolean
        Dim i As Integer
        SourceSelected = False
        For i = 0 To dgvSources.RowCount - 1
            If dgvSources.Rows(i).Cells(0).Value = Source_ID Then
                SourceSelected = True
                Exit For
            End If
        Next
    End Function

    Private Sub cmbSource_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        cmbSource.BackColor = FCOLOR
    End Sub

    Private Sub cmbSource_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbSource.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.PageUp Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub cmbSource_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        cmbSource.BackColor = NFCOLOR
    End Sub
    Private Sub cmbSource_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSource.SelectedIndexChanged
        If cmbSource.SelectedIndex <> -1 Then
            Dim i As Integer
            txtQty.Text = "1"
            'stpDateDrawn.Value = dtpDate.Value
            If Not IsDate(txtDrawnTime.Text) Then txtDrawnTime.Text = txtTime.Text
            For i = 0 To cmbTemp.Items.Count - 1
                If cmbTemp.Items(i).ToString = SystemConfig.DefTemp Then
                    cmbTemp.SelectedIndex = i
                    Exit For
                End If
            Next
            If Not chkPostPrePhleb.Checked Then btnAddSrc.Enabled = True
        End If
    End Sub

    Private Sub txtQty_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtQty.BackColor = FCOLOR
    End Sub

    Private Sub txtQty_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtQty.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
        Prices(txtQty, e)
    End Sub

    Private Sub btnRemAllSrc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemAllSrc.Click
        dgvSources.Rows.Clear()
        txtLabels.Text = txtLabels.Text = CalculateLabels()
        Update_SourceString()
        lblSpecimen.BackColor = Color.PeachPuff
        UpdateRequisitionProgress()
        cmbSource.Focus()
        btnRemAllSrc.Enabled = False
        btnRemSrc.Enabled = False

    End Sub

    Private Sub btnRemSrc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemSrc.Click
        If dgvSources.SelectedRows(0).Index <> -1 Then
            dgvSources.Rows.Remove(dgvSources.SelectedRows(0))
            If dgvSources.RowCount = 0 Then
                btnRemAllSrc.Enabled = False
                lblSpecimen.BackColor = Color.PeachPuff
            End If
            txtLabels.Text = CalculateLabels()
            cmbSource.Focus()
            btnRemSrc.Enabled = False

        End If
        Update_SourceString()
        Update_Specimen_Status()
        UpdateRequisitionProgress()
    End Sub

    Private Sub dgvSources_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSources.CellClick
        If e.RowIndex <> -1 Then
            btnRemSrc.Enabled = True

        End If

    End Sub

    Private Sub txtOrdID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtOrdID.BackColor = FCOLOR
    End Sub

    Private Sub txtOrdID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtOrdID.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            TabControl1.SelectTab("tpSpecimen")
        End If
    End Sub

    Private Sub txtOrdID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtOrdID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub Update_Billing_Status()
        If chkSvcGratis.Checked = False Then
            If (rbC.Checked = True And txtOrdID.Text <> "") _
            Or (rbP.Checked = True And txtPatientID.Text <> "") _
            Or (rbT.Checked = True And txtPInsID.Text <> "" _
            And txtPPolicy.Text <> "" And cmbPRelation.SelectedIndex = 0) _
            Or (rbT.Checked = True And txtPInsID.Text <> "" _
            And txtPPolicy.Text <> "" And cmbPRelation.SelectedIndex > 0 _
            And ((txtPSubID.Text <> "") Or (txtPSubLName.Text <> "" And
            txtPSubFName.Text <> "" And IsDate(txtPSubDOB.Text) And
            cmbPSubSex.SelectedIndex <> -1))) Then 'Orderer or patient
                lblBilling.BackColor = Color.PaleGreen
            Else
                lblBilling.BackColor = Color.PeachPuff
            End If
        Else
            lblBilling.BackColor = Color.PaleGreen
        End If
        'Or (rbT.Checked = True And txtSInsID.Text <> "" And txtSPolicy.Text <> "" And _
        'cmbSRelation.SelectedIndex = 0) Or (rbt.Enabled = True And txtSInsID.Text <> _
        '"" And txtSPolicy.Text <> "" And cmbSRelation.SelectedIndex > 0 And txtSSubID.Text <> "")
    End Sub

    Private Sub ClearOrderer()
        txtOrdID.Text = ""
        lblOrderer.BackColor = Color.PeachPuff
        btnRemProv.Enabled = False
        txtOrdName.Text = ""
        txtOrdAddress.Text = ""
        txtOrdCSZ.Text = ""
        txtCountry.Text = ""
        txtEMRNo.Text = ""
        lblChart.ForeColor = Color.DarkBlue
        txtOrdPhone.Text = ""
        txtOrdFax.Text = ""
        txtProvEmail.Text = ""
        'dgvRptProviders.Rows.Clear()
        lstProviders.Items.Clear()
        If Not frmProviderAlert Is Nothing Then frmProviderAlert.Close()
        rbC.Checked = True
        Update_Billing_Status()
    End Sub

    Private Function AttendeeInList(ByVal ProviderID As Long, ByVal lstProviders As CheckedListBox) As Boolean
        Dim InList As Boolean = False
        Dim ItemX As MyList
        For i As Integer = 0 To lstProviders.Items.Count - 1
            ItemX = lstProviders.Items(i)
            If ItemX.ItemData = ProviderID Then
                InList = True
                Exit For
            End If
        Next
        Return InList
    End Function

    Friend Sub DisplayProvider(ByVal ProviderID As Long)
        Dim Provider As String = ""
        Dim i As Integer
        Dim ItemX As MyList
        Dim sSQL As String = ""
        If btnNew.Checked = False Then  'New
            sSQL = "Select * from Providers where Active <> 0 and ID = " & ProviderID
        Else    'Edit
            sSQL = "Select * from Providers where ID = " & ProviderID
        End If
        If connString <> "" Then
            Dim cndp As New SqlConnection(connString)
            cndp.Open()
            Dim cmddp As New SqlCommand(sSQL, cndp)
            cmddp.CommandType = CommandType.Text
            Dim drdp As SqlDataReader = cmddp.ExecuteReader
            If drdp.HasRows Then
                While drdp.Read
                    txtOrdID.Text = ProviderID
                    btnRemProv.Enabled = True
                    If drdp("IsIndividual") <> 0 Then
                        Provider = Trim(drdp("LastName_BSN")) & ", " & Trim(drdp("FirstName"))
                        If drdp("Degree") IsNot DBNull.Value AndAlso
                        Trim(drdp("Degree")) <> "" Then Provider += " " & Trim(drdp("Degree"))
                    Else
                        Provider = drdp("LastName_BSN")
                    End If
                    txtOrdName.Text = Provider
                    If drdp("Address_ID") IsNot DBNull.Value Then
                        txtOrdAddress.Text = GetAddressLines(drdp("Address_ID"))
                        txtOrdCSZ.Text = GetAddressCSZ(drdp("Address_ID"))
                        txtCountry.Text = GetAddressCountry(drdp("Address_ID"))
                    End If
                    MergeAccs = drdp("AccConsolidate")
                    If drdp("Phone") IsNot DBNull.Value Then txtOrdPhone.Text = drdp("Phone")
                    If drdp("Fax") IsNot DBNull.Value Then txtOrdFax.Text = drdp("Fax")
                    If drdp("Email") IsNot DBNull.Value Then txtProvEmail.Text = drdp("Email")
                    If drdp("EMRNoRequired") IsNot DBNull.Value AndAlso drdp("EMRNoRequired") = True Then
                        lblChart.ForeColor = Color.Red
                    Else
                        lblChart.ForeColor = Color.DarkBlue
                    End If
                    lblOrderer.BackColor = Color.PaleGreen
                End While
            Else
                MsgBox("Invalid ID or the provider is not Active")
            End If
            cndp.Close()
            cndp = Nothing
            '
            lstProviders.Items.Clear()
            If btnNew.Checked = False Then  'New
                sSQL = "Select * from Providers where ID in (Select Provider_ID from Clinic_Provider " &
                "where Active <> 0 and Clinic_ID = " & ProviderID & ") order by LastName_BSN"
            Else    'Edit
                sSQL = "Select * from Providers where ID in (Select Provider_ID from " &
                "Clinic_Provider where Clinic_ID = " & ProviderID & ") order by LastName_BSN"
            End If
            Dim cnap As New SqlConnection(connString)
            cnap.Open()
            Dim cmdap As New SqlCommand(sSQL, cnap)
            cmdap.CommandType = CommandType.Text
            Dim drap As SqlDataReader = cmdap.ExecuteReader
            If drap.HasRows Then
                While drap.Read
                    If drap("IsIndividual") IsNot DBNull.Value AndAlso drap("IsIndividual") <> 0 Then
                        If drap("MiddleName") IsNot DBNull.Value _
                        AndAlso drap("MiddleName") <> "" Then
                            Provider = Trim(drap("LastName_BSN")) & ", " & Trim(drap("FirstName")) & " " & drap("MiddleName")
                            If drap("Degree") IsNot DBNull.Value _
                            AndAlso drap("Degree") <> "" Then
                                Provider += " " & drap("Degree")
                            End If
                        Else
                            Provider = Trim(drap("LastName_BSN")) & ", " & Trim(drap("FirstName"))
                            If drap("Degree") IsNot DBNull.Value AndAlso drap("Degree") <> "" Then
                                Provider += " " & drap("Degree")
                            End If
                        End If
                    Else
                        Provider = drap("LastName_BSN")
                    End If
                    If Not AttendeeInList(drap("ID"), lstProviders) Then _
                    lstProviders.Items.Add(New MyList(Provider, drap("ID")))
                End While
            Else
                lstProviders.Items.Add(New MyList(txtOrdName.Text, ProviderID))
            End If
            cnap.Close()
            cnap = Nothing
            '
            If btnNew.Checked = False Then  'New Accession
                lstProviders.SetItemChecked(0, True)
                ItemX = lstProviders.CheckedItems(0)
                Dim Alert As String = GetProviderAccAlert(ItemX.ItemData)
                If Alert <> "" Then
                    DisplayProviderAlert(Alert)
                Else
                    Alert = GetProviderAccAlert(ProviderID)
                    If Alert <> "" Then
                        DisplayProviderAlert(Alert)
                    Else
                        If Not frmProviderAlert Is Nothing Then frmProviderAlert.Close()
                    End If
                End If
                'lblOrderer.BackColor = Color.PaleGreen
            Else
                Dim AttenderID As Long = GetAttenderID(Val(txtAccID.Text))
                If MyAttender <> -1 Then AttenderID = MyAttender
                If AttenderID <> -1 Then
                    'If AttenderDirty = False Then
                    For i = 0 To lstProviders.Items.Count - 1
                        ItemX = lstProviders.Items(i)
                        If ItemX.ItemData = AttenderID Then
                            lstProviders.SetItemChecked(i, True)
                            'lblOrderer.BackColor = Color.PaleGreen
                            Exit For
                        End If
                    Next
                    'End If
                Else
                    MsgBox("System has detected some discrepency for attending provider. " &
                    "It is suggested to verify the validity of the Attending Provider. " &
                    "System will guess and force the most appropriate selection",
                    MsgBoxStyle.Information, "Prolis")
                    If lstProviders.Items.Count > 0 Then lstProviders.SetItemChecked(0, True)
                End If
            End If
        Else
            Dim cndp As New Odbc.OdbcConnection(connString)
            cndp.Open()
            Dim cmddp As New Odbc.OdbcCommand(sSQL, cndp)
            cmddp.CommandType = CommandType.Text
            Dim drdp As Odbc.OdbcDataReader = cmddp.ExecuteReader
            If drdp.HasRows Then
                While drdp.Read
                    txtOrdID.Text = ProviderID
                    btnRemProv.Enabled = True
                    If drdp("IsIndividual") <> 0 Then
                        Provider = Trim(drdp("LastName_BSN")) & ", " & Trim(drdp("FirstName"))
                        If drdp("Degree") IsNot DBNull.Value AndAlso
                        Trim(drdp("Degree")) <> "" Then Provider += " " & Trim(drdp("Degree"))
                    Else
                        Provider = drdp("LastName_BSN")
                    End If
                    txtOrdName.Text = Provider
                    If drdp("Address_ID") IsNot DBNull.Value Then
                        txtOrdAddress.Text = GetAddressLines(drdp("Address_ID"))
                        txtOrdCSZ.Text = GetAddressCSZ(drdp("Address_ID"))
                        txtCountry.Text = GetAddressCountry(drdp("Address_ID"))
                    End If
                    If drdp("Phone") IsNot DBNull.Value Then txtOrdPhone.Text = drdp("Phone")
                    If drdp("Fax") IsNot DBNull.Value Then txtOrdFax.Text = drdp("Fax")
                    If drdp("Email") IsNot DBNull.Value Then txtProvEmail.Text = drdp("Email")
                    If drdp("EMRNoRequired") IsNot DBNull.Value AndAlso drdp("EMRNoRequired") = True Then
                        lblChart.ForeColor = Color.Red
                    Else
                        lblChart.ForeColor = Color.DarkBlue
                    End If
                    lblOrderer.BackColor = Color.PaleGreen
                End While
            Else
                MsgBox("Invalid ID or the provider is not Active")
            End If
            cndp.Close()
            cndp = Nothing
            '
            lstProviders.Items.Clear()
            If btnNew.Checked = False Then  'New
                sSQL = "Select * from Providers where ID in (Select Provider_ID from Clinic_Provider " &
                "where Active <> 0 and Clinic_ID = " & ProviderID & ") order by LastName_BSN"
            Else    'Edit
                sSQL = "Select * from Providers where ID in (Select Provider_ID from " &
                "Clinic_Provider where Clinic_ID = " & ProviderID & ") order by LastName_BSN"
            End If
            Dim cnap As New Odbc.OdbcConnection(connString)
            cnap.Open()
            Dim cmdap As New Odbc.OdbcCommand(sSQL, cnap)
            cmdap.CommandType = CommandType.Text
            Dim drap As Odbc.OdbcDataReader = cmdap.ExecuteReader
            If drap.HasRows Then
                While drap.Read
                    If drap("IsIndividual") IsNot DBNull.Value AndAlso drap("IsIndividual") <> 0 Then
                        If drap("MiddleName").Value IsNot DBNull.Value _
                        AndAlso drap("MiddleName") <> "" Then
                            Provider = Trim(drap("LastName_BSN")) & ", " & Trim(drap("FirstName")) & " " & drap("MiddleName")
                            If drap("Degree") IsNot DBNull.Value _
                            AndAlso drap("Degree") <> "" Then
                                Provider += " " & drap("Degree")
                            End If
                        Else
                            Provider = Trim(drap("LastName_BSN")) & ", " & Trim(drap("FirstName"))
                            If drap("Degree") IsNot DBNull.Value AndAlso drap("Degree") <> "" Then
                                Provider += " " & drap("Degree")
                            End If
                        End If
                    Else
                        Provider = drap("LastName_BSN")
                    End If
                    lstProviders.Items.Add(New MyList(Provider, drap("ID")))
                End While
            Else
                lstProviders.Items.Add(New MyList(txtOrdName.Text, ProviderID))
            End If
            cnap.Close()
            cnap = Nothing
            '
            If btnNew.Checked = False Then  'New Accession
                lstProviders.SetItemChecked(0, True)
                ItemX = lstProviders.CheckedItems(0)
                Dim Alert As String = GetProviderAccAlert(ItemX.ItemData)
                If Alert <> "" Then
                    DisplayProviderAlert(Alert)
                Else
                    Alert = GetProviderAccAlert(ProviderID)
                    If Alert <> "" Then
                        DisplayProviderAlert(Alert)
                    Else
                        If Not frmProviderAlert Is Nothing Then frmProviderAlert.Close()
                    End If
                End If
                'lblOrderer.BackColor = Color.PaleGreen
            Else
                Dim AttenderID As Long = GetAttenderID(Val(txtAccID.Text))
                If MyAttender <> -1 Then AttenderID = MyAttender
                If AttenderID <> -1 Then
                    'If AttenderDirty = False Then
                    For i = 0 To lstProviders.Items.Count - 1
                        ItemX = lstProviders.Items(i)
                        If ItemX.ItemData = AttenderID Then
                            lstProviders.SetItemChecked(i, True)
                            'lblOrderer.BackColor = Color.PaleGreen
                            Exit For
                        End If
                    Next
                    'End If
                Else
                    MsgBox("System has detected some discrepency for attending provider. " &
                    "It is suggested to verify the validity of the Attending Provider. " &
                    "System will guess and force the most appropriate selection",
                    MsgBoxStyle.Information, "Prolis")
                    If lstProviders.Items.Count > 0 Then lstProviders.SetItemChecked(0, True)
                End If
            End If
        End If
        Update_Provider_Status()
    End Sub

    Private Function GetAttenderID(ByVal AccID As Long) As Long
        Dim AttenderID As Long = -1
        Dim sSQL As String = "Select AttendingProvider_ID from Requisitions where ID = " & AccID
        If connString <> "" Then
            Dim cnat As New SqlConnection(connString)
            cnat.Open()
            Dim cmdat As New SqlCommand(sSQL, cnat)
            cmdat.CommandType = CommandType.Text
            Dim drat As SqlDataReader = cmdat.ExecuteReader
            If drat.HasRows Then
                While drat.Read
                    If drat("AttendingProvider_ID") IsNot DBNull.Value Then _
                    AttenderID = drat("AttendingProvider_ID")
                End While
            End If
            cnat.Close()
            cnat = Nothing
        Else
            Dim cnat As New Odbc.OdbcConnection(connString)
            cnat.Open()
            Dim cmdat As New Odbc.OdbcCommand(sSQL, cnat)
            cmdat.CommandType = CommandType.Text
            Dim drat As Odbc.OdbcDataReader = cmdat.ExecuteReader
            If drat.HasRows Then
                While drat.Read
                    If drat("AttendingProvider_ID") IsNot DBNull.Value Then _
                    AttenderID = drat("AttendingProvider_ID")
                End While
            End If
            cnat.Close()
            cnat = Nothing
        End If
        Return AttenderID
    End Function

    Private Sub btnOrdLookup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOrdLookup.Click
        Dim ProvID As String = frmProviderLookup.ShowDialog()
        If ProvID <> "" Then
            ClearOrderer()
            DisplayProvider(Val(ProvID))
            ProviderDirty = True
            dgvRptProviders.Rows.Clear()
            If lstProviders.CheckedItems.Count > 0 Then
                Dim ItemX As MyList = lstProviders.CheckedItems(0)
                Dim RptSetting() As String = GetOrdAtndSetting(Val(txtOrdID.Text), ItemX.ItemData)
                Update_Rpt_Sec(RptSetting)
                RptDirty = True
            End If
        End If
        'Update_Provider_Status()
        Update_Rpt_Status()
        Update_Billing_Status()
        UpdateRequisitionProgress()
    End Sub

    Private Sub txtAccID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtAccID.BackColor = FCOLOR
    End Sub

    Private Sub txtAccID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtAccID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Numerals(sender, e)
    End Sub
    Private Sub UpdateReqStatus()
        If txtAccID.Text <> "" And txtTime.Text <> "" And cmbSpecimenType.SelectedIndex <> -1 Then
            lblRequisition.BackColor = Color.PaleGreen
        Else
            lblRequisition.BackColor = Color.PeachPuff
        End If
    End Sub

    Private Function HasTGPOrdered() As Boolean
        Dim IsThere As Boolean = False
        Dim i As Integer
        For i = 0 To dgvTGPMarked.RowCount - 1
            If dgvTGPMarked.Rows(i).Cells(0).Value IsNot Nothing _
            AndAlso dgvTGPMarked.Rows(i).Cells(0).Value.ToString <> "" Then
                IsThere = True
                Exit For
            End If
        Next
        Return IsThere
    End Function

    Private Sub txtAccID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtAccID.BackColor = NFCOLOR
        Dim RetVal As Integer
        If txtAccID.Text <> "" Then
            If btnNew.Checked = False Then              'Add new mode
                If Not IsUniqueAccession(Val(txtAccID.Text)) Then
                    RetVal = MsgBox("The Accession ID you typed is not unique. Either type a unique " _
                    & "(unused) value or simply accept the system assigned value by clicking 'No' button." _
                    & " Do you want to type the unique value yourself?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo)
                    If RetVal = vbYes Then
                        txtAccID.Text = ""
                        txtAccID.Focus()
                    Else
                        txtAccID.Text = NextAccessionID(dtpDate.Value, txtPatientID.Text)
                        'txtRequisition.Text = txtAccID.Text
                    End If
                End If
            Else                                    'Edit mode
                If AccessionExists(Val(txtAccID.Text)) Then
                    DisplayAccessionRecord(Val(txtAccID.Text))
                Else
                    MsgBox("The Accession ID you typed, either does not exit in the " &
                    "system or specimen not received. Try again with a valid ID or use search",
                    MsgBoxStyle.Critical)
                    txtAccID.Text = ""
                    txtRequisition.Text = ""
                    txtAccID.Focus()
                End If
            End If
        End If
        UpdateReqStatus()
        UpdateRequisitionProgress()
    End Sub

    Private Sub UpdateRequisitionProgress()
        If lblRequisition.BackColor = Color.PaleGreen And lblSpecimen.BackColor = Color.PaleGreen _
        And lblOrderer.BackColor = Color.PaleGreen And lblPatient.BackColor = Color.PaleGreen _
        And lblOrders.BackColor = Color.PaleGreen And lblReports.BackColor = Color.PaleGreen _
        And lblBilling.BackColor = Color.PaleGreen And (chkReject.Checked = False Or
        chkReject.Checked = True And Trim(txtRejectReason.Text) <> "") Then
            btnSave.Enabled = True
            btnSupportive.Enabled = True
            If btnNew.Checked = True Then
                btnDelete.Enabled = True
            Else
                btnDelete.Enabled = False
            End If
        Else
            btnSave.Enabled = False
            btnDelete.Enabled = False
            btnSupportive.Enabled = False
        End If
        UpdateReqStatus()
    End Sub

    Private Function IsAccBilled(ByVal AccID As Long) As Boolean
        Dim Billed As Boolean = False
        Dim sSQL As String = "Select * from Charge_Detail where Charge_ID " &
        "in (Select ID from Charges where Accession_ID = " & AccID & ")"
        If connString <> "" Then
            Dim cnb As New SqlConnection(connString)
            cnb.Open()
            Dim cmdb As New SqlCommand(sSQL, cnb)
            Dim drb As SqlDataReader = cmdb.ExecuteReader
            If drb.HasRows Then Billed = True
            cnb.Close()
            cnb = Nothing
        Else
            Dim cnb As New Odbc.OdbcConnection(connString)
            cnb.Open()
            Dim cmdb As New Odbc.OdbcCommand(sSQL, cnb)
            Dim drb As Odbc.OdbcDataReader = cmdb.ExecuteReader
            If drb.HasRows Then Billed = True
            cnb.Close()
            cnb = Nothing
        End If
        Return Billed
    End Function

    Friend Sub DisplayAccessionRecord(ByVal AccID As Long)
        Dim Billed As Boolean = IsAccBilled(AccID)
        gBilled = Billed
        Dim sSQL As String = "Select * from Requisitions where Received <> 0 and ID = " & AccID
        '
        Dim cnd As New SqlConnection(connString)
        cnd.Open()
        Dim cmdd As New SqlCommand(sSQL, cnd)
        Dim drd As SqlDataReader = cmdd.ExecuteReader
        If drd.HasRows Then
            While drd.Read
                txtAccID.Text = drd("ID").ToString
                txtRequisition.Text = Trim(drd("RequisitionNo"))
                dtpDate.Value = drd("AccessionDate")
                txtTime.Text = Format(drd("AccessionDate"), "HH:mm")
                cmbSpecimenType.SelectedIndex = drd("SpecimenType")
                lblRequisition.BackColor = Color.PaleGreen
                dtpDateDrawn.Value = dtpDate.Value
                txtDrawnTime.Text = txtTime.Text
                '
                txtRecDate.Text = Format(drd("ReceivedTime"), SystemConfig.DateFormat)
                txtRecTime.Text = Format(drd("ReceivedTime"), "HH:mm")
                'txtAccessionedBy.Text = GetUserName(Rs.Fields("AccessionedBy").Value)
                txtAnalysisStage.Text = GetAnalysisStage(drd("AnalysisStage_ID"))
                'txtLastEditedBy.Text = GetUserName(Rs.Fields("EditedBy").Value)
                'txtEditOn.Text = Rs.Fields("LastEditedon").Value
                If drd("WorkCmnt") IsNot DBNull.Value Then
                    txtWorkCmnt.Text = drd("WorkCmnt")
                Else
                    txtWorkCmnt.Text = ""
                End If
                If drd("Comment") IsNot DBNull.Value Then
                    txtComment.Text = drd("Comment")
                Else
                    txtComment.Text = ""
                End If
                If drd("RejectReason") IsNot DBNull.Value Then _
                txtRejectReason.Text = drd("RejectReason")
                If drd("Rejected") IsNot DBNull.Value Then
                    chkReject.Checked = drd("Rejected")
                Else
                    chkReject.Checked = False
                End If
                '
                DisplaySpecimen(drd("ID"))
                If dgvSources.RowCount > 0 Then btnRemAllSrc.Enabled = True
                If drd("EMRNo") IsNot DBNull.Value Then txtEMRNo.Text = drd("EMRNo")
                If drd("Room") IsNot DBNull.Value Then txtRoom.Text = drd("Room")
                '
                chkInHouse.Checked = drd("InHouse")
                If chkInHouse.Checked = True Then
                    chkInHouse.Text = "Yes"
                Else
                    chkInHouse.Text = "No"
                End If
                If drd("InEditReason") IsNot DBNull.Value Then txtInEditReason.Text = drd("InEditReason")
                '
                DisplayProvider(drd("OrderingProvider_ID"))
                If Billed Then
                    grpClient.Enabled = False
                    lstProviders.Enabled = False
                Else
                    grpClient.Enabled = True
                    lstProviders.Enabled = True
                End If
                'If Not Rs.Fields("AttendingProvider_ID").Value Is System.DBNull.Value Then _
                'DisplayAssociation(Rs.Fields("AttendingProvider_ID").Value)
                If cmbSpecimenType.SelectedIndex = 0 Then
                    If drd("Patient_ID") IsNot DBNull.Value Then DisplayPatient(drd("Patient_ID"))
                    UpdatePatStatus()
                    If drd("Fasting") IsNot DBNull.Value Then chkFasting.Checked = drd("Fasting")
                    ClearDxs()
                    If Not (SystemConfig.DiagTarget = "V") Then
                        DisplayDxs(drd("ID"))
                        DisplayMeds(drd("ID"))
                    End If
                    If Billed Then
                        grpPatient.Enabled = False
                    Else
                        grpPatient.Enabled = True
                    End If
                End If
                chkVerbal.CheckState = drd("Verbal")
                DisplayOrders(drd("ID"))
                If Billed Then
                    dgvTGPMarked.ReadOnly = True
                    chkCare.Enabled = False
                    chkHomeBound.Enabled = False
                    chkPhlebotomy.Enabled = False
                    chkProfile.Enabled = False
                    chkVerbal.Enabled = False
                Else
                    dgvTGPMarked.ReadOnly = False
                    chkCare.Enabled = True
                    chkHomeBound.Enabled = True
                    chkPhlebotomy.Enabled = True
                    chkProfile.Enabled = True
                    chkVerbal.Enabled = True
                End If
                DisplayRPTOrders("ACC", drd("ID"))
                '
                chkSvcGratis.Checked = drd("IsGratis")
                If drd("BillingType_ID") = 0 Then
                    rbC.Checked = True
                ElseIf drd("BillingType_ID") = 1 Then
                    rbT.Checked = True
                Else
                    rbP.Checked = True
                End If
                If drd("BillingType_ID") = 1 Then
                    If drd("PrimePayer_ID") IsNot DBNull.Value Then
                        DisplayPrimeIns(drd("ID"), drd("PrimePayer_ID"))
                        'DisplayPrimInsured(GetInsuredID(Rs.Fields("ID").Value, Rs.Fields("PrimePayer_ID").Value))
                    Else
                        ClearPrimary()
                    End If
                    If drd("SecondPayer_ID") IsNot DBNull.Value Then
                        DisplaySecondIns(drd("ID"), drd("SecondPayer_ID"))
                        'DisplaySecondInsured(GetInsuredID(Rs.Fields("ID").Value, Rs.Fields("SecondPayer_ID").Value))
                    Else
                        ClearSecondary()
                    End If
                End If
                'DisplayGuarantor(AccID)
                If drd("Payment_ID") IsNot DBNull.Value AndAlso
                drd("Payment_ID") > 0 Then
                    PMT = GetPaymentInfo(drd("Payment_ID"))
                    If PMT(0) <> "" AndAlso (PMT.Length > 0 And PMT(6) <> "") Then
                        txtPayment.Text = Format(Val(PMT(6)), "##,##0.00")
                    Else
                        txtPayment.Text = ""
                    End If
                Else
                    txtPayment.Text = ""
                End If
                Try
                    If drd("CreateQR") IsNot DBNull.Value Then
                        If QrChk.Enabled Then
                            QrChk.Checked = drd("CreateQR")
                        End If

                    Else

                    End If
                Catch ex As Exception

                End Try

            End While
        End If
        cnd.Close()
        cnd = Nothing
        '
        If Billed Then
            rbC.Enabled = False
            rbT.Enabled = False
            rbP.Enabled = False
            chkSvcGratis.Enabled = False
            btnPmnt.Enabled = False
            btnCalculate.Enabled = False
            grpPrimary.Enabled = False
            grpPSubs.Enabled = False
            grpSecondary.Enabled = False
            grpSSubs.Enabled = False
            btnSwitchCarriers.Enabled = False
        Else
            rbC.Enabled = True
            rbT.Enabled = True
            rbP.Enabled = True
            chkSvcGratis.Enabled = True
            btnPmnt.Enabled = True
            btnCalculate.Enabled = True
            grpPrimary.Enabled = True
            grpPSubs.Enabled = True
            grpSecondary.Enabled = True
            grpSSubs.Enabled = True
            If ThisUser.Billing = True Then
                If txtPInsID.Text <> "" And txtPPolicy.Text <> "" And
                txtSInsID.Text <> "" And txtSPolicy.Text <> "" Then
                    btnSwitchCarriers.Enabled = True
                Else
                    btnSwitchCarriers.Enabled = False
                End If
            Else
                btnSwitchCarriers.Enabled = False
            End If
        End If
        Update_Billing_Status()
        AccPrevVals = GetCurrentAccVals(AccID)
        UpdateRequisitionProgress()
    End Sub

    Private Sub DisplayMeds(ByVal AccID As Long)
        ClearMeds()
        Dim i As Integer = 0
        Dim sSQL As String = "Select Medication from Req_Med where Accession_ID = " & AccID & " order by Ordinal"
        If connString <> "" Then
            Dim cnm As New SqlConnection(connString)
            cnm.Open()
            Dim cmdm As New SqlCommand(sSQL, cnm)
            cmdm.CommandType = CommandType.Text
            Dim drm As SqlDataReader = cmdm.ExecuteReader
            If drm.HasRows Then
                While drm.Read
                    If i = dgvMeds.RowCount - 1 Then dgvMeds.RowCount += 1
                    dgvMeds.Rows(i).Cells(0).Value = Trim(drm("Medication"))
                    i += 1
                End While
            End If
            cnm.Close()
            cnm = Nothing
        Else
            Dim cnm As New Odbc.OdbcConnection(connString)
            cnm.Open()
            Dim cmdm As New Odbc.OdbcCommand(sSQL, cnm)
            cmdm.CommandType = CommandType.Text
            Dim drm As Odbc.OdbcDataReader = cmdm.ExecuteReader
            If drm.HasRows Then
                While drm.Read
                    If i = dgvMeds.RowCount - 1 Then dgvMeds.RowCount += 1
                    dgvMeds.Rows(i).Cells(0).Value = Trim(drm("Medication"))
                    i += 1
                End While
            End If
            cnm.Close()
            cnm = Nothing
        End If
    End Sub

    Private Sub UpdatePatStatus()
        If txtPatientID.Text <> "" Or (txtLName.Text <> "" And txtFName.Text <> "" And
        IsDate(txtDOB.Text) And cmbSex.SelectedIndex <> -1) Then
            If lblChart.ForeColor = Color.Red Then 'required
                If Trim(txtEMRNo.Text) <> "" Then
                    lblPatient.BackColor = Color.PaleGreen
                Else
                    lblPatient.BackColor = Color.PeachPuff
                End If
            Else
                lblPatient.BackColor = Color.PaleGreen
            End If
        Else
            lblPatient.BackColor = Color.PeachPuff
        End If
    End Sub

    Private Sub PopulateCurrentBilling()
        Dim CB As String = "BillType="
        'BType, PArID, PPolicy, PGroup, PRel, PInsrd, SArID, SPolicy, SGroup, SRel, SInsrd, GArID, GIndEnt, GRel  
        If rbC.Checked = True Then   'Client
            CB += "0^ARID=" & txtOrdID.Text
        ElseIf rbP.Checked = True Then   'patient
            CB += "2^ARID=" & txtPatientID.Text
        Else
            If txtPInsID.Text <> "" And txtPPolicy.Text <> "" Then
                CB += "1^PARID=" & txtPInsID.Text & "^PPolicy=" & Trim(txtPPolicy.Text) &
                "^PGroup=" & Trim(txtPGroup.Text) & "^PRelation=" & cmbPRelation.SelectedIndex &
                IIf(cmbPRelation.SelectedIndex > 0, "^PInsuredID=" & txtPSubID.Text, "")
            End If
            If txtSInsID.Text <> "" And txtSPolicy.Text <> "" Then
                CB += "1^SARID=" & txtSInsID.Text & "^SPolicy=" & Trim(txtSPolicy.Text) &
                "^SGroup=" & Trim(txtSGroup.Text) & "^SRelation=" & cmbSRelation.SelectedIndex &
                IIf(cmbSRelation.SelectedIndex > 0, "^SInsuredID=" & txtSSubID.Text, "")
            End If
            If txtGID.Text <> "" Then
                CB += "3^GARID=" & txtGID.Text & "^GIndividual=" & chkGIsIndividual.Checked
            End If
        End If
    End Sub

    Private Sub DisplayGuarantor(ByVal AccID As Long)
        txtGID.Text = ""
        Dim i As Integer
        Dim sSQL As String = "Select * from Req_Guarantor where Accession_ID = " & AccID
        If connString <> "" Then
            Dim cng As New SqlConnection(connString)
            cng.Open()
            Dim cmdg As New SqlCommand(sSQL, cng)
            cmdg.CommandType = CommandType.Text
            Dim drg As SqlDataReader = cmdg.ExecuteReader
            If drg.HasRows Then
                While drg.Read
                    chkGIsIndividual.Checked = drg("GuarantorEntity")
                    txtGID.Text = drg("Guarantor_ID").ToString
                    cmbGRelation.SelectedIndex = drg("Relation")
                End While
            End If
            cng.Close()
            cng = Nothing
            '
            If chkGIsIndividual.Checked = False Then 'Entity
                sSQL = "Select * from Employers where ID = " & Val(txtGID.Text)
                Dim cnem As New SqlConnection(connString)
                cnem.Open()
                Dim cmdem As New SqlCommand(sSQL, cnem)
                cmdem.CommandType = CommandType.Text
                Dim drem As SqlDataReader = cmdem.ExecuteReader
                If drem.HasRows Then
                    While drem.Read
                        txtGLName_BSN.Text = drem("Employer")
                        If drem("Phone") IsNot DBNull.Value Then txtGPhone.Text = drem("Phone")
                        If drem("Email") IsNot DBNull.Value Then txtGEmail.Text = drem("Email")
                        If drem("Address_ID") IsNot DBNull.Value Then
                            txtGAdd1.Text = GetAddress1(drem("Address_ID"))
                            txtGAdd2.Text = GetAddress2(drem("Address_ID"))
                            txtGCity.Text = GetAddressCity(drem("Address_ID"))
                            txtGState.Text = GetAddressState(drem("Address_ID"))
                            txtGZip.Text = GetAddressZip(drem("Address_ID"))
                            txtGCountry.Text = GetAddressCountry(drem("Address_ID"))
                        End If
                    End While
                End If
                cnem.Close()
                cnem = Nothing
            Else
                sSQL = "Select * from Patients where ID = " & Val(txtGID.Text)
                Dim cnp As New SqlConnection(connString)
                cnp.Open()
                Dim cmdp As New SqlCommand(sSQL, cnp)
                cmdp.CommandType = CommandType.Text
                Dim drp As SqlDataReader = cmdp.ExecuteReader
                If drp.HasRows Then
                    While drp.Read
                        txtGLName_BSN.Text = drp("LastName")
                        txtGFName.Text = drp("FirstName")
                        txtGMName.Text = drp("MiddleName")
                        For i = 0 To cmbGSex.Items.Count - 1
                            If drp("Sex") = Microsoft.VisualBasic.Left(cmbGSex.Items(i), 1) Then
                                cmbGSex.SelectedIndex = i
                                Exit For
                            End If
                        Next
                        txtGDOB.Text = Format(drp("DOB"), SystemConfig.DateFormat)
                        If drp("HomePhone") IsNot DBNull.Value Then txtGPhone.Text = drp("HomePhone")
                        If drp("Email") IsNot DBNull.Value Then txtGEmail.Text = drp("Email")
                        If drp("Address_ID") IsNot DBNull.Value Then
                            txtGAdd1.Text = GetAddress1(drp("Address_ID"))
                            txtGAdd2.Text = GetAddress2(drp("Address_ID"))
                            txtGCity.Text = GetAddressCity(drp("Address_ID"))
                            txtGState.Text = GetAddressState(drp("Address_ID"))
                            txtGZip.Text = GetAddressZip(drp("Address_ID"))
                            txtGCountry.Text = GetAddressCountry(drp("Address_ID"))
                        End If
                    End While
                End If
                cnp.Close()
                cnp = Nothing
            End If
        Else
            Dim cng As New Odbc.OdbcConnection(connString)
            cng.Open()
            Dim cmdg As New Odbc.OdbcCommand(sSQL, cng)
            cmdg.CommandType = CommandType.Text
            Dim drg As Odbc.OdbcDataReader = cmdg.ExecuteReader
            If drg.HasRows Then
                While drg.Read
                    chkGIsIndividual.Checked = drg("GuarantorEntity")
                    txtGID.Text = drg("Guarantor_ID").ToString
                    cmbGRelation.SelectedIndex = drg("Relation")
                End While
            End If
            cng.Close()
            cng = Nothing
            '
            If chkGIsIndividual.Checked = False Then 'Entity
                sSQL = "Select * from Employers where ID = " & Val(txtGID.Text)
                Dim cnem As New Odbc.OdbcConnection(connString)
                cnem.Open()
                Dim cmdem As New Odbc.OdbcCommand(sSQL, cnem)
                cmdem.CommandType = CommandType.Text
                Dim drem As Odbc.OdbcDataReader = cmdem.ExecuteReader
                If drem.HasRows Then
                    While drem.Read
                        txtGLName_BSN.Text = drem("Employer")
                        If drem("Phone") IsNot DBNull.Value Then txtGPhone.Text = drem("Phone")
                        If drem("Email") IsNot DBNull.Value Then txtGEmail.Text = drem("Email")
                        If drem("Address_ID") IsNot DBNull.Value Then
                            txtGAdd1.Text = GetAddress1(drem("Address_ID"))
                            txtGAdd2.Text = GetAddress2(drem("Address_ID"))
                            txtGCity.Text = GetAddressCity(drem("Address_ID"))
                            txtGState.Text = GetAddressState(drem("Address_ID"))
                            txtGZip.Text = GetAddressZip(drem("Address_ID"))
                            txtGCountry.Text = GetAddressCountry(drem("Address_ID"))
                        End If
                    End While
                End If
                cnem.Close()
                cnem = Nothing
            Else
                sSQL = "Select * from Patients where ID = " & Val(txtGID.Text)
                Dim cnp As New Odbc.OdbcConnection(connString)
                cnp.Open()
                Dim cmdp As New Odbc.OdbcCommand(sSQL, cnp)
                cmdp.CommandType = CommandType.Text
                Dim drp As Odbc.OdbcDataReader = cmdp.ExecuteReader
                If drp.HasRows Then
                    While drp.Read
                        txtGLName_BSN.Text = drp("LastName")
                        txtGFName.Text = drp("FirstName")
                        txtGMName.Text = drp("MiddleName")
                        For i = 0 To cmbGSex.Items.Count - 1
                            If drp("Sex") = Microsoft.VisualBasic.Left(cmbGSex.Items(i), 1) Then
                                cmbGSex.SelectedIndex = i
                                Exit For
                            End If
                        Next
                        txtGDOB.Text = Format(drp("DOB"), SystemConfig.DateFormat)
                        If drp("HomePhone") IsNot DBNull.Value Then txtGPhone.Text = drp("HomePhone")
                        If drp("Email") IsNot DBNull.Value Then txtGEmail.Text = drp("Email")
                        If drp("Address_ID") IsNot DBNull.Value Then
                            txtGAdd1.Text = GetAddress1(drp("Address_ID"))
                            txtGAdd2.Text = GetAddress2(drp("Address_ID"))
                            txtGCity.Text = GetAddressCity(drp("Address_ID"))
                            txtGState.Text = GetAddressState(drp("Address_ID"))
                            txtGZip.Text = GetAddressZip(drp("Address_ID"))
                            txtGCountry.Text = GetAddressCountry(drp("Address_ID"))
                        End If
                    End While
                End If
                cnp.Close()
                cnp = Nothing
            End If
        End If
    End Sub

    Private Function GetInsuredID(ByVal AccID As Long, ByVal PayerID As Long) As Long
        Dim InsuredID As Long = -1
        Dim sSQL As String = "Select * from Req_Coverage where Accession_ID = " & AccID & " and Payer_ID = " & PayerID
        If connString <> "" Then
            Dim cni As New SqlConnection(connString)
            cni.Open()
            Dim cmdi As New SqlCommand(sSQL, cni)
            cmdi.CommandType = CommandType.Text
            Dim dri As SqlDataReader = cmdi.ExecuteReader
            If dri.HasRows Then
                While dri.Read
                    InsuredID = dri("Insured_ID")
                End While
            End If
            cni.Close()
            cni = Nothing
        Else
            Dim cni As New Odbc.OdbcConnection(connString)
            cni.Open()
            Dim cmdi As New Odbc.OdbcCommand(sSQL, cni)
            cmdi.CommandType = CommandType.Text
            Dim dri As Odbc.OdbcDataReader = cmdi.ExecuteReader
            If dri.HasRows Then
                While dri.Read
                    InsuredID = dri("Insured_ID")
                End While
            End If
            cni.Close()
            cni = Nothing
        End If
        Return InsuredID
    End Function

    Private Function GetPaymentInfo(ByVal PaymentID As Long) As String()
        Dim sSQL As String = "Select * from Payments where ID = " & PaymentID
        Dim cnpmt As New SqlConnection(connString)
        cnpmt.Open()
        Dim cmdpmt As New SqlCommand(sSQL, cnpmt)
        cmdpmt.CommandType = CommandType.Text
        Dim drpmt As SqlDataReader = cmdpmt.ExecuteReader
        If drpmt.HasRows Then
            ReDim PMT(14)    '0=ID, 1=ArType, 2=ArID, 3= PType, 4= PDate, 5= DocNo, 6=AMT, 7= UnApp, 8=CCID 
            For i As Integer = 0 To PMT.Length - 1
                PMT(i) = ""
            Next
            While drpmt.Read
                PMT(0) = drpmt("ID").ToString
                PMT(1) = drpmt("ArType").ToString
                PMT(2) = drpmt("Ar_ID").ToString
                PMT(3) = drpmt("PaymentType").ToString
                PMT(4) = Format(drpmt("PaymentDate"), SystemConfig.DateFormat)
                PMT(5) = drpmt("DocNo")
                PMT(6) = drpmt("Amount").ToString
                PMT(7) = drpmt("UnApplied").ToString
                If drpmt("CC_ID") IsNot DBNull.Value AndAlso
                drpmt("CC_ID") > 0 Then PMT(8) = drpmt("CC_ID")
            End While
        End If
        cnpmt.Close()
        cnpmt = Nothing
        '
        If PMT.Length = 9 AndAlso PMT(8) <> "" AndAlso Val(PMT(8)) > 0 Then
            sSQL = "Select * from CreditCards where ID = " & Val(PMT(8))
            Dim cncc As New SqlConnection(connString)
            cncc.Open()
            Dim cmdcc As New SqlCommand(sSQL, cncc)
            cmdcc.CommandType = CommandType.Text
            Dim drcc As SqlDataReader = cmdcc.ExecuteReader
            If drcc.HasRows Then
                ReDim Preserve PMT(14)
                For i As Integer = 9 To 14
                    PMT(i) = ""
                Next
                While drcc.Read
                    '0=ID, 1=ArType, 2=ArID, 3= PType, 4= PDate, 5= DocNo, 6=AMT, 7= UnApp, 8=CCID
                    '9 = CCType, 10=CCNo, 11=ExpDate, 12=CVV, 13=BillName, 14=BillZip
                    PMT(9) = drcc("CCType")
                    PMT(10) = drcc("CCNo")
                    PMT(11) = drcc("ExpireDate")
                    PMT(12) = drcc("CVV")
                    PMT(13) = drcc("BillName")
                    PMT(14) = drcc("BillZip")
                End While
            End If
            cncc.Close()
            cncc = Nothing
        End If
        Return PMT
    End Function

    Private Sub DisplayAssociation(ByVal AttendID As Long)
        Dim i As Integer
        Dim ItemX As MyList
        For i = 0 To lstProviders.Items.Count - 1
            lstProviders.SetItemChecked(i, False)
            ItemX = lstProviders.Items(i)
            If AttendID = ItemX.ItemData Then
                lstProviders.SetItemChecked(i, True)
            End If
        Next
    End Sub

    Private Sub DisplayDxs(ByVal AccID As Long)
        Dim i As Integer
        Dim sSQL As String = "Select Dx_Code from Req_Dx where Accession_ID = " & AccID & " order by Ordinal"
        If connString <> "" Then
            Dim cnd As New SqlConnection(connString)
            cnd.Open()
            Dim cmdd As New SqlCommand(sSQL, cnd)
            cmdd.CommandType = CommandType.Text
            Dim drd As SqlDataReader = cmdd.ExecuteReader
            If drd.HasRows Then
                While drd.Read
                    If i = dgvDxs.RowCount - 1 Then dgvDxs.RowCount += 1
                    dgvDxs.Rows(i).Cells(0).Value = Trim(drd("Dx_Code").ToString)
                    i += 1
                End While
            End If
            cnd.Close()
            cnd = Nothing
        Else
            Dim cnd As New Odbc.OdbcConnection(connString)
            cnd.Open()
            Dim cmdd As New Odbc.OdbcCommand(sSQL, cnd)
            cmdd.CommandType = CommandType.Text
            Dim drd As Odbc.OdbcDataReader = cmdd.ExecuteReader
            If drd.HasRows Then
                While drd.Read
                    If i = dgvDxs.RowCount - 1 Then dgvDxs.RowCount += 1
                    dgvDxs.Rows(i).Cells(0).Value = Trim(drd("Dx_Code").ToString)
                    i += 1
                End While
            End If
            cnd.Close()
            cnd = Nothing
        End If
    End Sub

    Private Sub DisplayPrimeIns(ByVal AccID As Long, ByVal PayerID As Long)
        ClearPrimary()
        Dim sSQL As String = "Select a.*, b.PayerName from Req_Coverage a inner join Payers b on a.Payer_ID = " &
        "b.ID where a.Accession_ID = " & AccID & " and a.Payer_ID = " & PayerID & " and a.Preference = 'P'"
        If connString <> "" Then
            Dim cnpp As New SqlConnection(connString)
            cnpp.Open()
            Dim cmdpp As New SqlCommand(sSQL, cnpp)
            cmdpp.CommandType = CommandType.Text
            Dim drpp As SqlDataReader = cmdpp.ExecuteReader
            If drpp.HasRows Then
                While drpp.Read
                    txtPInsID.Text = drpp("Payer_ID").ToString
                    txtPInsName.Text = drpp("PayerName")
                    If drpp("GroupNo") IsNot DBNull.Value _
                    Then txtPGroup.Text = drpp("GroupNo")
                    txtPPolicy.Text = drpp("PolicyNo")
                    cmbPRelation.SelectedIndex = drpp("Relation")
                    If drpp("Copayment") IsNot DBNull.Value Then _
                    txtPCopay.Text = Format(drpp("Copayment"), "##,##0.00")
                    chkWorkman.Checked = drpp("WorkmanComp")
                    If drpp("InstanceDate") IsNot DBNull.Value _
                    AndAlso IsDate(drpp("InstanceDate")) Then
                        txtDOI.Text = Format(drpp("InstanceDate"), SystemConfig.DateFormat)
                    Else
                        txtDOI.Text = ""
                    End If
                    If drpp("Comment") IsNot DBNull.Value Then
                        txtCovCmnt.Text = Trim(drpp("Comment"))
                    Else
                        txtCovCmnt.Text = ""
                    End If
                    If drpp("Relation") <> 0 Then _
                    DisplayPrimInsured(drpp("Insured_ID"))
                End While
            End If
            cnpp.Close()
            cnpp = Nothing
        Else
            Dim cnpp As New Odbc.OdbcConnection(connString)
            cnpp.Open()
            Dim cmdpp As New Odbc.OdbcCommand(sSQL, cnpp)
            cmdpp.CommandType = CommandType.Text
            Dim drpp As Odbc.OdbcDataReader = cmdpp.ExecuteReader
            If drpp.HasRows Then
                While drpp.Read
                    txtPInsID.Text = drpp("Payer_ID").ToString
                    txtPInsName.Text = drpp("PayerName")
                    If drpp("GroupNo") IsNot DBNull.Value _
                    Then txtPGroup.Text = drpp("GroupNo")
                    txtPPolicy.Text = drpp("PolicyNo")
                    cmbPRelation.SelectedIndex = drpp("Relation")
                    If drpp("Copayment") IsNot DBNull.Value Then _
                    txtPCopay.Text = Format(drpp("Copayment"), "##,##0.00")
                    chkWorkman.Checked = drpp("WorkmanComp")
                    If drpp("InstanceDate") IsNot DBNull.Value _
                    AndAlso IsDate(drpp("InstanceDate")) Then
                        txtDOI.Text = Format(drpp("InstanceDate"), SystemConfig.DateFormat)
                    Else
                        txtDOI.Text = ""
                    End If
                    If drpp("Comment") IsNot DBNull.Value Then
                        txtCovCmnt.Text = Trim(drpp("Comment"))
                    Else
                        txtCovCmnt.Text = ""
                    End If
                    If drpp("Relation") <> 0 Then _
                    DisplayPrimInsured(drpp("Insured_ID"))
                End While
            End If
            cnpp.Close()
            cnpp = Nothing
        End If
    End Sub

    Private Sub DisplaySecondIns(ByVal AccID As Long, ByVal PayerID As Long)
        ClearSecondary()
        Dim sSQL As String = "Select a.*, b.PayerName from Req_Coverage a inner join Payers b on a.Payer_ID = b.ID " &
        "where a.Accession_ID = " & AccID & " and a.Payer_ID = " & PayerID & " and a.Preference = 'S'"
        If connString <> "" Then
            Dim cnsp As New SqlConnection(connString)
            cnsp.Open()
            Dim cmdsp As New SqlCommand(sSQL, cnsp)
            cmdsp.CommandType = CommandType.Text
            Dim drsp As SqlDataReader = cmdsp.ExecuteReader
            If drsp.HasRows Then
                While drsp.Read
                    txtSInsID.Text = drsp("Payer_ID").ToString
                    txtSInsName.Text = drsp("PayerName")
                    If drsp("GroupNo") IsNot DBNull.Value _
                    Then txtSGroup.Text = drsp("GroupNo")
                    txtSPolicy.Text = drsp("PolicyNo")
                    cmbSRelation.SelectedIndex = drsp("Relation")
                    If drsp("Copayment") IsNot DBNull.Value Then _
                    txtSCopay.Text = Format(drsp("Copayment"), "##,##0.00")
                    If drsp("Relation") <> 0 Then _
                    DisplaySecondInsured(drsp("Insured_ID"))
                End While
            End If
            cnsp.Close()
            cnsp = Nothing
        Else
            Dim cnsp As New Odbc.OdbcConnection(connString)
            cnsp.Open()
            Dim cmdsp As New Odbc.OdbcCommand(sSQL, cnsp)
            cmdsp.CommandType = CommandType.Text
            Dim drsp As Odbc.OdbcDataReader = cmdsp.ExecuteReader
            If drsp.HasRows Then
                While drsp.Read
                    txtSInsID.Text = drsp("Payer_ID").ToString
                    txtSInsName.Text = drsp("PayerName")
                    If drsp("GroupNo") IsNot DBNull.Value _
                    Then txtSGroup.Text = drsp("GroupNo")
                    txtSPolicy.Text = drsp("PolicyNo")
                    cmbSRelation.SelectedIndex = drsp("Relation")
                    If drsp("Copayment") IsNot DBNull.Value Then _
                    txtSCopay.Text = Format(drsp("Copayment"), "##,##0.00")
                    If drsp("Relation") <> 0 Then _
                    DisplaySecondInsured(drsp("Insured_ID"))
                End While
            End If
            cnsp.Close()
            cnsp = Nothing
        End If
    End Sub

    Private Sub DisplayRPTOrders(ByVal Rpt_Type As String, ByVal AccID As Long)
        dgvRptProviders.Rows.Clear()
        Dim sSQL As String = "Select * from Req_RPT where Rpt_Type = 'ACC' and Base_ID = " & AccID
        If connString <> "" Then
            Dim cnro As New SqlConnection(connString)
            cnro.Open()
            Dim cmdro As New SqlCommand(sSQL, cnro)
            cmdro.CommandType = CommandType.Text
            Dim drro As SqlDataReader = cmdro.ExecuteReader
            If drro.HasRows Then
                While drro.Read
                    If Not Report_Recipient_In(drro("Provider_ID")) Then
                        dgvRptProviders.Rows.Add(drro("Provider_ID"), GetProviderName(drro("Provider_ID")),
                        drro("RDM_Auto"), drro("RPT_Complete"), drro("RPT_Print"), drro("RPT_Prolison"),
                        drro("RPT_Interface"), drro("RPT_Fax"), drro("Fax"), drro("RPT_Email"), drro("Email"))
                    End If
                End While
                lblReports.BackColor = Color.PaleGreen
            End If
            cnro.Close()
            cnro = Nothing
        Else
            Dim cnro As New Odbc.OdbcConnection(connString)
            cnro.Open()
            Dim cmdro As New Odbc.OdbcCommand(sSQL, cnro)
            cmdro.CommandType = CommandType.Text
            Dim drro As Odbc.OdbcDataReader = cmdro.ExecuteReader
            If drro.HasRows Then
                While drro.Read
                    If Not Report_Recipient_In(drro("Provider_ID")) Then
                        dgvRptProviders.Rows.Add(drro("Provider_ID"), GetProviderName(drro("Provider_ID")),
                        drro("RDM_Auto"), drro("RPT_Complete"), drro("RPT_Print"), drro("RPT_Prolison"),
                        drro("RPT_Interface"), drro("RPT_Fax"), drro("Fax"), drro("RPT_Email"), drro("Email"))
                    End If
                End While
                lblReports.BackColor = Color.PaleGreen
            End If
            cnro.Close()
            cnro = Nothing
        End If
    End Sub

    Private Function GetProviderName(ByVal ProviderID As Long) As String
        Dim PName As String = ""
        Dim sSQL As String = "Select * from Providers where ID = " & ProviderID
        If connString <> "" Then
            Dim cnpr As New SqlConnection(connString)
            cnpr.Open()
            Dim cmdpr As New SqlCommand(sSQL, cnpr)
            cmdpr.CommandType = CommandType.Text
            Dim drpr As SqlDataReader = cmdpr.ExecuteReader
            If drpr.HasRows Then
                While drpr.Read
                    If drpr("IsIndividual") = False Then     'Entity
                        PName = Trim(drpr("LastName_BSN"))
                    Else
                        PName = Trim(drpr("LastName_BSN")) & ", " & Trim(drpr("FirstName"))
                        If drpr("Degree") IsNot DBNull.Value AndAlso
                        Trim(drpr("Degree")) <> "" Then PName += " " & Trim(drpr("Degree"))
                    End If
                End While
            End If
            cnpr.Close()
            cnpr = Nothing
        Else
            Dim cnpr As New Odbc.OdbcConnection(connString)
            cnpr.Open()
            Dim cmdpr As New Odbc.OdbcCommand(sSQL, cnpr)
            cmdpr.CommandType = CommandType.Text
            Dim drpr As Odbc.OdbcDataReader = cmdpr.ExecuteReader
            If drpr.HasRows Then
                While drpr.Read
                    If drpr("IsIndividual") = False Then     'Entity
                        PName = Trim(drpr("LastName_BSN"))
                    Else
                        PName = Trim(drpr("LastName_BSN")) & ", " & Trim(drpr("FirstName"))
                        If drpr("Degree") IsNot DBNull.Value AndAlso
                        Trim(drpr("Degree")) <> "" Then PName += " " & Trim(drpr("Degree"))
                    End If
                End While
            End If
            cnpr.Close()
            cnpr = Nothing
        End If
        Return PName
    End Function

    Private Sub DisplayOrders(ByVal Accession_ID As Long)
        ClearOrders()
        Dim i As Integer
        Dim CompType As String = ""
        Dim sSQL As String = "Select * from Req_TGP where Accession_ID = " & Accession_ID & " order by Ordinal"
        If connString <> "" Then
            Dim cno As New SqlConnection(connString)
            cno.Open()
            Dim cmdo As New SqlCommand(sSQL, cno)
            cmdo.CommandType = CommandType.Text
            Dim dro As SqlDataReader = cmdo.ExecuteReader
            If dro.HasRows Then
                While dro.Read
                    CompType = GetTGPType(dro("TGP_ID"))
                    If i >= dgvTGPMarked.RowCount Then dgvTGPMarked.RowCount += 1
                    dgvTGPMarked.Rows(i).Cells(0).Value = dro("TGP_ID").ToString
                    dgvTGPMarked.Rows(i).Cells(2).Value = GetTGPName(dro("TGP_ID"))
                    If CompType = "T" Then
                        dgvTGPMarked.Rows(i).Cells(3).Value =
                        System.Drawing.Image.FromFile(Application.StartupPath &
                        "\Images\Test.ico")
                    ElseIf CompType = "G" Then
                        dgvTGPMarked.Rows(i).Cells(3).Value =
                        System.Drawing.Image.FromFile(Application.StartupPath &
                        "\Images\Group.ico")
                    ElseIf CompType = "P" Then
                        dgvTGPMarked.Rows(i).Cells(3).Value =
                        System.Drawing.Image.FromFile(Application.StartupPath &
                        "\Images\Profile.ico")
                    End If
                    dgvTGPMarked.Rows(i).Cells(4).Value = dro("IsStat")
                    dgvTGPMarked.Rows(i).Cells(5).Value = dro("Verbal")
                    i = i + 1
                    '
                    If ExistTGPs(UBound(ExistTGPs)) <> "" Then ReDim Preserve _
                    ExistTGPs(UBound(ExistTGPs) + 1)
                    ExistTGPs(UBound(ExistTGPs)) = dro("TGP_ID").ToString
                End While
                lblOrders.BackColor = Color.PaleGreen
            End If
            cno.Close()
            cno = Nothing
        Else
            Dim cno As New Odbc.OdbcConnection(connString)
            cno.Open()
            Dim cmdo As New Odbc.OdbcCommand(sSQL, cno)
            cmdo.CommandType = CommandType.Text
            Dim dro As Odbc.OdbcDataReader = cmdo.ExecuteReader
            If dro.HasRows Then
                While dro.Read
                    CompType = GetTGPType(dro("TGP_ID"))
                    If i >= dgvTGPMarked.RowCount Then dgvTGPMarked.RowCount += 1
                    dgvTGPMarked.Rows(i).Cells(0).Value = dro("TGP_ID").ToString
                    dgvTGPMarked.Rows(i).Cells(2).Value = GetTGPName(dro("TGP_ID"))
                    If CompType = "T" Then
                        dgvTGPMarked.Rows(i).Cells(3).Value =
                        System.Drawing.Image.FromFile(Application.StartupPath &
                        "\Images\Test.ico")
                    ElseIf CompType = "G" Then
                        dgvTGPMarked.Rows(i).Cells(3).Value =
                        System.Drawing.Image.FromFile(Application.StartupPath &
                        "\Images\Group.ico")
                    ElseIf CompType = "P" Then
                        dgvTGPMarked.Rows(i).Cells(3).Value =
                        System.Drawing.Image.FromFile(Application.StartupPath &
                        "\Images\Profile.ico")
                    End If
                    dgvTGPMarked.Rows(i).Cells(4).Value = dro("IsStat")
                    dgvTGPMarked.Rows(i).Cells(5).Value = dro("Verbal")
                    i = i + 1
                    '
                    If ExistTGPs(UBound(ExistTGPs)) <> "" Then ReDim Preserve _
                    ExistTGPs(UBound(ExistTGPs) + 1)
                    ExistTGPs(UBound(ExistTGPs)) = dro("TGP_ID").ToString
                End While
                lblOrders.BackColor = Color.PaleGreen
            End If
            cno.Close()
            cno = Nothing
        End If
        If SystemConfig.InPhlebTGP <> "" And
        SystemConfig.HPhlebTGP <> "" And
        SystemConfig.CPhlebTGP <> "" Then
            Dim BillDx As String = ""
            For i = 0 To dgvTGPMarked.RowCount - 1
                If dgvTGPMarked.Rows(i).Cells(0).Value IsNot Nothing AndAlso
                (dgvTGPMarked.Rows(i).Cells(0).Value = SystemConfig.InPhlebTGP Or
                dgvTGPMarked.Rows(i).Cells(0).Value = SystemConfig.HPhlebTGP Or
                dgvTGPMarked.Rows(i).Cells(0).Value = SystemConfig.CPhlebTGP) Then   'Billing code
                    BillDx = dgvTGPMarked.Rows(i).Cells(0).Value
                    Exit For
                End If
            Next
            If BillDx = SystemConfig.InPhlebTGP Then
                chkPhlebotomy.Checked = True
            ElseIf BillDx = SystemConfig.HPhlebTGP Then
                chkHomeBound.Checked = True
            ElseIf BillDx = SystemConfig.CPhlebTGP Then
                chkCare.Checked = True
            Else
                chkPhlebotomy.Checked = False
            End If
        End If
    End Sub

    Private Sub DisplaySpecimen(ByVal AccID As Long)
        dgvSources.Rows.Clear()
        Dim CMNT As String = ""
        Dim sSQL As String = "Select * from Specimens where Accession_ID = " & AccID
        If connString <> "" Then
            Dim cns As New SqlConnection(connString)
            cns.Open()
            Dim cmds As New SqlCommand(sSQL, cns)
            cmds.CommandType = CommandType.Text
            Dim drs As SqlDataReader = cmds.ExecuteReader
            If drs.HasRows Then
                While drs.Read
                    If drs("Comment") IsNot DBNull.Value _
                    AndAlso Trim(drs("Comment")) <> "" Then
                        CMNT = Trim(drs("Comment"))
                    Else
                        CMNT = ""
                    End If
                    dgvSources.Rows.Add(drs("Source_ID"), drs("SourceNo"), GetSourceName(drs("Source_ID")),
                     drs("SourceQuantity"), Format(drs("SourceDate"), SystemConfig.DateFormat),
                     Format(drs("SourceDate"), "HH:mm"), drs("SourceTemp"), CMNT)
                End While
                lblSpecimen.BackColor = Color.PaleGreen
            End If
            cns.Close()
            cns = Nothing
        Else
            Dim cns As New Odbc.OdbcConnection(connString)
            cns.Open()
            Dim cmds As New Odbc.OdbcCommand(sSQL, cns)
            cmds.CommandType = CommandType.Text
            Dim drs As Odbc.OdbcDataReader = cmds.ExecuteReader
            If drs.HasRows Then
                While drs.Read
                    If drs("Comment") IsNot DBNull.Value _
                    AndAlso Trim(drs("Comment")) <> "" Then
                        CMNT = Trim(drs("Comment"))
                    Else
                        CMNT = ""
                    End If
                    dgvSources.Rows.Add(drs("Source_ID"), drs("SourceNo"), GetSourceName(drs("Source_ID")),
                     drs("SourceQuantity"), Format(drs("SourceDate"), SystemConfig.DateFormat),
                     Format(drs("SourceDate"), "HH:mm"), drs("SourceTemp"), CMNT)
                End While
                lblSpecimen.BackColor = Color.PaleGreen
            End If
            cns.Close()
            cns = Nothing
        End If
        If dgvSources.RowCount() > 0 Then
            txtLabels.Text = dgvSources.RowCount()
        End If
        Update_SourceString()
    End Sub

    Private Function GetSourceName(ByVal ID As Integer) As String
        Dim SName As String = ""
        Dim sSQL As String = "Select * from Sources where ID = " & ID
        If connString <> "" Then
            Dim cnsn As New SqlConnection(connString)
            cnsn.Open()
            Dim cmdsn As New SqlCommand(sSQL, cnsn)
            cmdsn.CommandType = CommandType.Text
            Dim drsn As SqlDataReader = cmdsn.ExecuteReader
            If drsn.HasRows Then
                While drsn.Read
                    SName = Trim(drsn("Name"))
                End While
            End If
            cnsn.Close()
            cnsn = Nothing
        Else
            Dim cnsn As New Odbc.OdbcConnection(connString)
            cnsn.Open()
            Dim cmdsn As New Odbc.OdbcCommand(sSQL, cnsn)
            cmdsn.CommandType = CommandType.Text
            Dim drsn As Odbc.OdbcDataReader = cmdsn.ExecuteReader
            If drsn.HasRows Then
                While drsn.Read
                    SName = Trim(drsn("Name"))
                End While
            End If
            cnsn.Close()
            cnsn = Nothing
        End If
        Return SName
    End Function

    Private Function GetUserName(ByVal ID As Long) As String
        Dim UName As String = ""
        Dim sSQL As String = "Select * from Users where ID = " & ID
        If connString <> "" Then
            Dim cnu As New SqlConnection(connString)
            cnu.Open()
            Dim cmdu As New SqlCommand(sSQL, cnu)
            cmdu.CommandType = CommandType.Text
            Dim dru As SqlDataReader = cmdu.ExecuteReader
            If dru.HasRows Then
                While dru.Read
                    UName = Trim(dru("FullName"))
                End While
            End If
            cnu.Close()
            cnu = Nothing
        Else
            Dim cnu As New Odbc.OdbcConnection(connString)
            cnu.Open()
            Dim cmdu As New Odbc.OdbcCommand(sSQL, cnu)
            cmdu.CommandType = CommandType.Text
            Dim dru As Odbc.OdbcDataReader = cmdu.ExecuteReader
            If dru.HasRows Then
                While dru.Read
                    UName = Trim(dru("FullName"))
                End While
            End If
            cnu.Close()
            cnu = Nothing
        End If
        Return UName
    End Function

    Private Function GetAnalysisStage(ByVal ID As Integer) As String
        Dim stage As String = ""
        Dim sSQL As String = "Select * from AnalysisStages where ID = " & ID
        If connString <> "" Then
            Dim cna As New SqlConnection(connString)
            cna.Open()
            Dim cmda As New SqlCommand(sSQL, cna)
            cmda.CommandType = CommandType.Text
            Dim dra As SqlDataReader = cmda.ExecuteReader
            If dra.HasRows Then
                While dra.Read
                    stage = Trim(dra("Stage"))
                End While
            End If
            cna.Close()
            cna = Nothing
        Else
            Dim cna As New Odbc.OdbcConnection(connString)
            cna.Open()
            Dim cmda As New Odbc.OdbcCommand(sSQL, cna)
            cmda.CommandType = CommandType.Text
            Dim dra As Odbc.OdbcDataReader = cmda.ExecuteReader
            If dra.HasRows Then
                While dra.Read
                    stage = Trim(dra("Stage"))
                End While
            End If
            cna.Close()
            cna = Nothing
        End If
        Return stage
    End Function

    Private Function AccessionExists(ByVal AccID As Long) As Boolean
        Dim Exists As Boolean = False
        Dim sSQL As String = "Select ID from Requisitions where Received <> 0 and ID = " & AccID
        If connString <> "" Then
            Dim cnae As New SqlConnection(connString)
            cnae.Open()
            Dim cmdae As New SqlCommand(sSQL, cnae)
            cmdae.CommandType = CommandType.Text
            Dim drae As SqlDataReader = cmdae.ExecuteReader
            If drae.HasRows Then Exists = True
            cnae.Close()
            cnae = Nothing
        Else
            Dim cnae As New Odbc.OdbcConnection(connString)
            cnae.Open()
            Dim cmdae As New Odbc.OdbcCommand(sSQL, cnae)
            cmdae.CommandType = CommandType.Text
            Dim drae As Odbc.OdbcDataReader = cmdae.ExecuteReader
            If drae.HasRows Then Exists = True
            cnae.Close()
            cnae = Nothing
        End If
        Return Exists
    End Function

    Private Function IsUniqueAccession(ByVal AccID As Long) As Boolean
        Dim Unique As Boolean = True
        Dim sSQL As String = "Select ID from Requisitions where ID = " & AccID
        Dim cnua As New SqlConnection(connString)
        cnua.Open()
        Dim cmdua As New SqlCommand(sSQL, cnua)
        cmdua.CommandType = CommandType.Text
        Dim drua As SqlDataReader = cmdua.ExecuteReader
        If drua.HasRows Then Unique = False
        cnua.Close()
        cnua = Nothing
        Return Unique
    End Function

    Private Sub FindPatientBySSN(ByVal SSN As String)
        ClearPatient()
        SSN = Replace(SSN, "-", "")
        SSN = Replace(SSN, " ", "")
        Dim sSQL As String = "Select * from Patients where SSN like '" & SSN & "'"
        If connString <> "" Then
            Dim cnpat As New SqlConnection(connString)
            cnpat.Open()
            Dim cmdpat As New SqlCommand(sSQL, cnpat)
            cmdpat.CommandType = CommandType.Text
            Dim drpat As SqlDataReader = cmdpat.ExecuteReader
            If drpat.HasRows Then
                While drpat.Read
                    DisplayPatient(drpat("ID"))
                End While
            End If
            cnpat.Close()
            cnpat = Nothing
        Else
            Dim cnpat As New Odbc.OdbcConnection(connString)
            cnpat.Open()
            Dim cmdpat As New Odbc.OdbcCommand(sSQL, cnpat)
            cmdpat.CommandType = CommandType.Text
            Dim drpat As Odbc.OdbcDataReader = cmdpat.ExecuteReader
            If drpat.HasRows Then
                While drpat.Read
                    DisplayPatient(drpat("ID"))
                End While
            End If
            cnpat.Close()
            cnpat = Nothing
        End If
    End Sub

    Private Sub ClearPatient()
        txtPatientID.Text = ""
        UnLockPatVitalFields()
        btnRemPat.Enabled = False
        lblPatient.BackColor = Color.PeachPuff
        txtLName.Text = ""
        txtFName.Text = ""
        cmbSex.SelectedIndex = -1
        txtTage.Text = ""
        txtDOB.Text = ""
        txtSSN.Text = ""
        txtMName.Text = ""
        txtPatAdr1.Text = ""
        txtPatAdr2.Text = ""
        txtPatCity.Text = ""
        txtPatState.Text = ""
        txtPatZip.Text = ""
        txtPatEmail.Text = ""
        Try
            cmbRace.SelectedIndex = 5
        Catch ex As Exception

        End Try

        cmbEthnicity.SelectedIndex = 2
        txtPatHPhone.Text = ""
        txtWPhone.Text = ""
        txtCell.Text = ""
        chkFasting.Checked = False
        txtEMRNo.Text = ""
        txtRoom.Text = ""
        If rbP.Checked Then rbC.Checked = True
        'UpdatePrimResp()
        Update_Billing_Status()
    End Sub

    Private Sub LockPatVitalFields()
        If txtPatientID.Text <> "" Then
            txtLName.ReadOnly = True
            txtFName.ReadOnly = True
            cmbSex.Enabled = False
            txtTage.ReadOnly = True
            txtDOB.ReadOnly = True
        End If
    End Sub

    Private Sub UnLockPatVitalFields()
        txtLName.ReadOnly = False
        txtFName.ReadOnly = False
        cmbSex.Enabled = True
        txtTage.ReadOnly = False
        txtDOB.ReadOnly = False
    End Sub

    Friend Sub DisplayPatient(ByVal PatID As Long)
        ClearPatient()
        Dim ItemX As MyList
        Dim ItemS As MyList = Nothing
        Dim ItemB As MyList = Nothing
        Dim sSQL As String = "Select * from Patients where ID = " & PatID
        Dim cnp As New SqlConnection(connString)
        cnp.Open()
        Dim cmdp As New SqlCommand(sSQL, cnp)
        cmdp.CommandType = CommandType.Text
        Dim drp As SqlDataReader = cmdp.ExecuteReader
        If drp.HasRows Then
            While drp.Read
                txtPatientID.Text = drp("ID").ToString
                btnRemPat.Enabled = True
                txtLName.Text = drp("LastName")
                txtFName.Text = drp("FirstName")
                txtMName.Text = drp("MiddleName")
                For i As Integer = 0 To cmbSex.Items.Count - 1
                    If cmbSex.Items(i).ToString.Substring(0, 1) = drp("Sex").ToString.Substring(0, 1) Then
                        cmbSex.SelectedIndex = i
                        Exit For
                    End If
                Next
                If drp("Tage") IsNot DBNull.Value Then txtTage.Text = Trim(drp("Tage"))
                If drp("DOB") IsNot DBNull.Value AndAlso IsDate(drp("DOB")) Then
                    txtDOB.Mask = Replace(Replace(Replace(SystemConfig.DateFormat, "y", "0"), "M", "0"), "d", "0")
                    txtDOB.Text = Format(drp("DOB"), SystemConfig.DateFormat)

                End If
                If SystemConfig.DiagTarget = "V" Then
                    If drp("Species_ID") IsNot DBNull.Value _
                    AndAlso drp("Species_ID") > 0 Then
                        For i As Integer = 0 To cmbSpecies.Items.Count - 1
                            ItemS = cmbSpecies.Items(i)
                            If ItemS.ItemData = drp("Species_ID") Then
                                cmbSpecies.SelectedIndex = i
                                Exit For
                            End If
                        Next
                    End If
                    '
                    If drp("Breed_ID") IsNot DBNull.Value _
                     AndAlso drp("Breed_ID") > 0 Then
                        For i As Integer = 0 To cmbBreed.Items.Count - 1
                            ItemB = cmbBreed.Items(i)
                            If ItemB.ItemData = drp("Breed_ID") Then
                                cmbBreed.SelectedIndex = i
                                Exit For
                            End If
                        Next
                    End If
                End If
                If drp("SSN") IsNot DBNull.Value Then txtSSN.Text = drp("SSN")
                If drp("Address_ID") IsNot DBNull.Value Then
                    txtPatAdr1.Text = GetAddress1(drp("Address_ID"))
                    txtPatAdr2.Text = GetAddress2(drp("Address_ID"))
                    txtPatCity.Text = GetAddressCity(drp("Address_ID"))
                    txtPatState.Text = GetAddressState(drp("Address_ID"))
                    txtPatZip.Text = GetAddressZip(drp("Address_ID"))
                    txtPatCountry.Text = GetAddressCountry(drp("Address_ID"))
                End If
                If drp("Race_ID") IsNot DBNull.Value Then
                    For i As Integer = 0 To cmbRace.Items.Count - 1
                        ItemX = cmbRace.Items(i)
                        If ItemX.ItemData = drp("Race_ID") Then
                            cmbRace.SelectedIndex = i
                            Exit For
                        End If
                    Next
                Else
                    cmbRace.SelectedIndex = 5
                End If
                If drp("Ethnicity") IsNot DBNull.Value Then
                    For i As Integer = 0 To cmbEthnicity.Items.Count - 1
                        If cmbEthnicity.Items(i).ToString = drp("Ethnicity") Then
                            cmbEthnicity.SelectedIndex = i
                            Exit For
                        End If
                    Next
                Else
                    cmbEthnicity.SelectedIndex = 2
                End If
                If drp("Email") IsNot DBNull.Value Then txtPatEmail.Text = drp("Email")
                If drp("HomePhone") IsNot DBNull.Value Then txtPatHPhone.Text = PhoneNeat(drp("HomePhone"))
                If drp("Cell") IsNot DBNull.Value Then txtCell.Text = PhoneNeat(drp("Cell"))
                If drp("WorkPhone") IsNot DBNull.Value Then txtWPhone.Text = PhoneNeat(drp("WorkPhone"))
                If drp("Fax") IsNot DBNull.Value Then txtFax.Text = PhoneNeat(drp("Fax"))
            End While

            If txtPatientID.Text <> "" Then
                LockPatVitalFields()

                Dim Alert As String = GetPatientAccAlert(txtPatientID.Text)

                If Alert <> "" Then
                    DisplayProviderAlert(Alert, "Patient Alert")
                Else
                    If Not frmProviderAlert Is Nothing Then frmProviderAlert.Close()
                End If
            End If

            lblPatient.BackColor = Color.LightGreen

        Else
            lblPatient.BackColor = Color.PeachPuff
        End If
        cnp.Close()
        cnp = Nothing
        If txtOrdID.Text <> "" Then
            Dim EMRRoom() As String = GetEMRRoom(Val(txtOrdID.Text), PatID)
            txtEMRNo.Text = EMRRoom(0)
            txtRoom.Text = EMRRoom(1)
        End If
        '
        'If lblChart.ForeColor = Color.Red Then
        'End If
        If btnNew.Checked = False Then  'New
            DisplayPatientCoverage(PatID)
            DisplayPatientGuarantor(PatID)
        End If
    End Sub

    Private Sub DisplayPatientGuarantor(ByVal PatID As Long)
        Dim i As Integer
        Dim sSQL As String = "Select * from Guarantors where Patient_ID = " & PatID
        If connString <> "" Then
            Dim cng As New SqlConnection(connString)
            cng.Open()
            Dim cmdg As New SqlCommand(sSQL, cng)
            cmdg.CommandType = CommandType.Text
            Dim drg As SqlDataReader = cmdg.ExecuteReader
            If drg.HasRows Then
                While drg.Read
                    chkGIsIndividual.Checked = drg("GuarantorEntity")
                    txtGID.Text = drg("Guarantor_ID").ToString
                    cmbGRelation.SelectedIndex = drg("Relation")
                End While
            End If
            cng.Close()
            cng = Nothing
            '
            If chkGIsIndividual.Checked = False Then 'Entity
                sSQL = "Select * from Employers where ID = " & Val(txtGID.Text)
                Dim cnem As New SqlConnection(connString)
                cnem.Open()
                Dim cmdem As New SqlCommand(sSQL, cnem)
                cmdem.CommandType = CommandType.Text
                Dim drem As SqlDataReader = cmdem.ExecuteReader
                If drem.HasRows Then
                    While drem.Read
                        txtGLName_BSN.Text = drem("Employer")
                        If drem("Phone") IsNot DBNull.Value Then txtGPhone.Text = drem("Phone")
                        If drem("Email") IsNot DBNull.Value Then txtGEmail.Text = drem("Email")
                        If drem("Address_ID") IsNot DBNull.Value Then
                            txtGAdd1.Text = GetAddress1(drem("Address_ID"))
                            txtGAdd2.Text = GetAddress2(drem("Address_ID"))
                            txtGCity.Text = GetAddressCity(drem("Address_ID"))
                            txtGState.Text = GetAddressState(drem("Address_ID"))
                            txtGZip.Text = GetAddressZip(drem("Address_ID"))
                            txtGCountry.Text = GetAddressCountry(drem("Address_ID"))
                        End If
                    End While
                End If
                cnem.Close()
                cnem = Nothing
            Else
                sSQL = "Select * from Patients where ID = " & Val(txtGID.Text)
                Dim cnp As New SqlConnection(connString)
                cnp.Open()
                Dim cmdp As New SqlCommand(sSQL, cnp)
                cmdp.CommandType = CommandType.Text
                Dim drp As SqlDataReader = cmdp.ExecuteReader
                If drp.HasRows Then
                    While drp.Read
                        txtGLName_BSN.Text = drp("LastName")
                        txtGFName.Text = drp("FirstName")
                        txtGMName.Text = drp("MiddleName")
                        For i = 0 To cmbGSex.Items.Count - 1
                            If drp("Sex") = Microsoft.VisualBasic.Left(cmbGSex.Items(i), 1) Then
                                cmbGSex.SelectedIndex = i
                                Exit For
                            End If
                        Next
                        txtGDOB.Text = Format(drp("DOB"), SystemConfig.DateFormat)
                        If drp("HomePhone") IsNot DBNull.Value Then txtGPhone.Text = drp("HomePhone")
                        If drp("Email") IsNot DBNull.Value Then txtGEmail.Text = drp("Email")
                        If drp("Address_ID") IsNot DBNull.Value Then
                            txtGAdd1.Text = GetAddress1(drp("Address_ID"))
                            txtGAdd2.Text = GetAddress2(drp("Address_ID"))
                            txtGCity.Text = GetAddressCity(drp("Address_ID"))
                            txtGState.Text = GetAddressState(drp("Address_ID"))
                            txtGZip.Text = GetAddressZip(drp("Address_ID"))
                            txtGCountry.Text = GetAddressCountry(drp("Address_ID"))
                        End If
                    End While
                End If
                cnp.Close()
                cnp = Nothing
            End If
        Else
            Dim cng As New Odbc.OdbcConnection(connString)
            cng.Open()
            Dim cmdg As New Odbc.OdbcCommand(sSQL, cng)
            cmdg.CommandType = CommandType.Text
            Dim drg As Odbc.OdbcDataReader = cmdg.ExecuteReader
            If drg.HasRows Then
                While drg.Read
                    chkGIsIndividual.Checked = drg("GuarantorEntity")
                    txtGID.Text = drg("Guarantor_ID").ToString
                    cmbGRelation.SelectedIndex = drg("Relation")
                End While
            End If
            cng.Close()
            cng = Nothing
            '
            If chkGIsIndividual.Checked = False Then 'Entity
                sSQL = "Select * from Employers where ID = " & Val(txtGID.Text)
                Dim cnem As New Odbc.OdbcConnection(connString)
                cnem.Open()
                Dim cmdem As New Odbc.OdbcCommand(sSQL, cnem)
                cmdem.CommandType = CommandType.Text
                Dim drem As Odbc.OdbcDataReader = cmdem.ExecuteReader
                If drem.HasRows Then
                    While drem.Read
                        txtGLName_BSN.Text = drem("Employer")
                        If drem("Phone") IsNot DBNull.Value Then txtGPhone.Text = drem("Phone")
                        If drem("Email") IsNot DBNull.Value Then txtGEmail.Text = drem("Email")
                        If drem("Address_ID") IsNot DBNull.Value Then
                            txtGAdd1.Text = GetAddress1(drem("Address_ID"))
                            txtGAdd2.Text = GetAddress2(drem("Address_ID"))
                            txtGCity.Text = GetAddressCity(drem("Address_ID"))
                            txtGState.Text = GetAddressState(drem("Address_ID"))
                            txtGZip.Text = GetAddressZip(drem("Address_ID"))
                            txtGCountry.Text = GetAddressCountry(drem("Address_ID"))
                        End If
                    End While
                End If
                cnem.Close()
                cnem = Nothing
            Else
                sSQL = "Select * from Patients where ID = " & Val(txtGID.Text)
                Dim cnp As New Odbc.OdbcConnection(connString)
                cnp.Open()
                Dim cmdp As New Odbc.OdbcCommand(sSQL, cnp)
                cmdp.CommandType = CommandType.Text
                Dim drp As Odbc.OdbcDataReader = cmdp.ExecuteReader
                If drp.HasRows Then
                    While drp.Read
                        txtGLName_BSN.Text = drp("LastName")
                        txtGFName.Text = drp("FirstName")
                        txtGMName.Text = drp("MiddleName")
                        For i = 0 To cmbGSex.Items.Count - 1
                            If drp("Sex") = Microsoft.VisualBasic.Left(cmbGSex.Items(i), 1) Then
                                cmbGSex.SelectedIndex = i
                                Exit For
                            End If
                        Next
                        txtGDOB.Text = Format(drp("DOB"), SystemConfig.DateFormat)
                        If drp("HomePhone") IsNot DBNull.Value Then txtGPhone.Text = drp("HomePhone")
                        If drp("Email") IsNot DBNull.Value Then txtGEmail.Text = drp("Email")
                        If drp("Address_ID") IsNot DBNull.Value Then
                            txtGAdd1.Text = GetAddress1(drp("Address_ID"))
                            txtGAdd2.Text = GetAddress2(drp("Address_ID"))
                            txtGCity.Text = GetAddressCity(drp("Address_ID"))
                            txtGState.Text = GetAddressState(drp("Address_ID"))
                            txtGZip.Text = GetAddressZip(drp("Address_ID"))
                            txtGCountry.Text = GetAddressCountry(drp("Address_ID"))
                        End If
                    End While
                End If
                cnp.Close()
                cnp = Nothing
            End If
        End If
    End Sub

    Private Sub DisplayPatientCoverage(ByVal PatID As Long)
        Dim sSQL As String = "Select * from Coverages where Patient_ID = " & PatID
        If connString <> "" Then
            Dim cnpc As New SqlConnection(connString)
            cnpc.Open()
            Dim cmdpc As New SqlCommand(sSQL, cnpc)
            cmdpc.CommandType = CommandType.Text
            Dim drpc As SqlDataReader = cmdpc.ExecuteReader
            If drpc.HasRows Then
                rbT.Checked = True
                While drpc.Read
                    If drpc("Preference") = "P" Then
                        txtPInsID.Text = drpc("Insurance_ID").ToString
                        txtPInsName.Text = GetPayerName(drpc("Insurance_ID"))
                        txtPPolicy.Text = Trim(drpc("PolicyNo"))
                        txtPGroup.Text = Trim(drpc("GroupNo"))
                        cmbPRelation.SelectedIndex = drpc("Relation")
                        If drpc("Relation") <> 0 Then _
                        DisplayPrimInsured(drpc("Insured_ID"))
                        lblBilling.BackColor = Color.PaleGreen
                    Else
                        txtSInsID.Text = drpc("Insurance_ID").ToString
                        txtSInsName.Text = GetPayerName(drpc("Insurance_ID"))
                        txtSPolicy.Text = Trim(drpc("PolicyNo"))
                        txtSGroup.Text = Trim(drpc("GroupNo"))
                        cmbSRelation.SelectedIndex = drpc("Relation")
                        If drpc("Relation") <> 0 Then _
                        DisplaySecondInsured(drpc("Insured_ID"))
                    End If
                End While
            End If
            cnpc.Close()
            cnpc = Nothing
        Else
            Dim cnpc As New Odbc.OdbcConnection(connString)
            cnpc.Open()
            Dim cmdpc As New Odbc.OdbcCommand(sSQL, cnpc)
            cmdpc.CommandType = CommandType.Text
            Dim drpc As Odbc.OdbcDataReader = cmdpc.ExecuteReader
            If drpc.HasRows Then
                rbT.Checked = True
                While drpc.Read
                    If drpc("Preference") = "P" Then
                        txtPInsID.Text = drpc("Insurance_ID").ToString
                        txtPInsName.Text = GetPayerName(drpc("Insurance_ID"))
                        txtPPolicy.Text = Trim(drpc("PolicyNo"))
                        txtPGroup.Text = Trim(drpc("GroupNo"))
                        cmbPRelation.SelectedIndex = drpc("Relation")
                        If drpc("Relation") <> 0 Then _
                        DisplayPrimInsured(drpc("Insured_ID"))
                        lblBilling.BackColor = Color.PaleGreen
                    Else
                        txtSInsID.Text = drpc("Insurance_ID").ToString
                        txtSInsName.Text = GetPayerName(drpc("Insurance_ID"))
                        txtSPolicy.Text = Trim(drpc("PolicyNo"))
                        txtSGroup.Text = Trim(drpc("GroupNo"))
                        cmbSRelation.SelectedIndex = drpc("Relation")
                        If drpc("Relation") <> 0 Then _
                        DisplaySecondInsured(drpc("Insured_ID"))
                    End If
                End While
            End If
            cnpc.Close()
            cnpc = Nothing
        End If
    End Sub

    Private Function GetPayerName(ByVal PayerID As Long) As String
        Dim Payer As String = ""
        Dim sSQL As String = "Select PayerName from Payers where ID = " & PayerID
        If connString <> "" Then
            Dim cnpn As New SqlConnection(connString)
            cnpn.Open()
            Dim cmdpn As New SqlCommand(sSQL, cnpn)
            cmdpn.CommandType = CommandType.Text
            Dim drpn As SqlDataReader = cmdpn.ExecuteReader
            If drpn.HasRows Then
                While drpn.Read
                    Payer = Trim(drpn("PayerName"))
                End While
            End If
            cnpn.Close()
            cnpn = Nothing
        Else
            Dim cnpn As New Odbc.OdbcConnection(connString)
            cnpn.Open()
            Dim cmdpn As New Odbc.OdbcCommand(sSQL, cnpn)
            cmdpn.CommandType = CommandType.Text
            Dim drpn As Odbc.OdbcDataReader = cmdpn.ExecuteReader
            If drpn.HasRows Then
                While drpn.Read
                    Payer = Trim(drpn("PayerName"))
                End While
            End If
            cnpn.Close()
            cnpn = Nothing
        End If
        Return Payer
    End Function

    Private Function GetEMRRoom(ByVal ProviderID As Long, ByVal PatID As Long) As String()
        Dim ClientPat() As String = {"", ""}    '0=EMR, 1=Room
        Dim sSQL As String = "Select EMRNo, Room from Client_Patient where Provider_ID = " & ProviderID & " and Patient_ID = " & PatID
        If connString <> "" Then
            Dim cner As New SqlConnection(connString)
            cner.Open()
            Dim cmder As New SqlCommand(sSQL, cner)
            cmder.CommandType = CommandType.Text
            Dim drer As SqlDataReader = cmder.ExecuteReader
            If drer.HasRows Then
                While drer.Read
                    If drer("EMRNo") IsNot DBNull.Value Then ClientPat(0) = Trim(drer("EMRNo"))
                    If drer("Room") IsNot DBNull.Value Then ClientPat(1) = Trim(drer("Room"))
                End While
            End If
            cner.Close()
            cner = Nothing
            If ClientPat(0) = "" Or ClientPat(1) = "" Then
                sSQL = "Select EMRNo, Room from Requisitions where OrderingProvider_ID = " &
                ProviderID & " and Patient_ID = " & PatID & " order by ID DESC"
                Dim cnr As New SqlConnection(connString)
                cnr.Open()
                Dim cmdr As New SqlCommand(sSQL, cnr)
                cmdr.CommandType = CommandType.Text
                Dim drr As SqlDataReader = cmdr.ExecuteReader
                If drr.HasRows Then
                    While drr.Read
                        If drr("EMRNo") IsNot DBNull.Value AndAlso ClientPat(0) = "" Then ClientPat(0) = Trim(drr("EMRNo"))
                        If drr("Room") IsNot DBNull.Value AndAlso ClientPat(1) = "" Then ClientPat(1) = Trim(drr("Room"))
                        If ClientPat(0) <> "" And ClientPat(1) <> "" Then Exit While
                    End While
                End If
                cnr.Close()
                cnr = Nothing
            End If
        Else
            Dim cner As New Odbc.OdbcConnection(connString)
            cner.Open()
            Dim cmder As New Odbc.OdbcCommand(sSQL, cner)
            cmder.CommandType = CommandType.Text
            Dim drer As Odbc.OdbcDataReader = cmder.ExecuteReader
            If drer.HasRows Then
                While drer.Read
                    If drer("EMRNo") IsNot DBNull.Value Then ClientPat(0) = Trim(drer("EMRNo"))
                    If drer("Room") IsNot DBNull.Value Then ClientPat(1) = Trim(drer("Room"))
                End While
            End If
            cner.Close()
            cner = Nothing
            If ClientPat(0) = "" Or ClientPat(1) = "" Then
                sSQL = "Select EMRNo, Room from Requisitions where OrderingProvider_ID = " &
                ProviderID & " and Patient_ID = " & PatID & " order by ID DESC"
                Dim cnr As New Odbc.OdbcConnection(connString)
                cnr.Open()
                Dim cmdr As New Odbc.OdbcCommand(sSQL, cnr)
                cmdr.CommandType = CommandType.Text
                Dim drr As Odbc.OdbcDataReader = cmdr.ExecuteReader
                If drr.HasRows Then
                    While drr.Read
                        If drr("EMRNo") IsNot DBNull.Value AndAlso ClientPat(0) = "" Then ClientPat(0) = Trim(drr("EMRNo"))
                        If drr("Room") IsNot DBNull.Value AndAlso ClientPat(1) = "" Then ClientPat(1) = Trim(drr("Room"))
                        If ClientPat(0) <> "" And ClientPat(1) <> "" Then Exit While
                    End While
                End If
                cnr.Close()
                cnr = Nothing
            End If
        End If
        Return ClientPat
    End Function

    Private Sub Update_Patient_Status()
        If cmbSpecimenType.SelectedIndex <> 0 Then
            lblPatient.BackColor = Color.PaleGreen
        Else
            If Trim(txtLName.Text) <> "" And Trim(txtFName.Text) <> "" _
            And cmbSex.SelectedIndex <> -1 And IsDate(txtDOB.Text) And
            ((lblChart.ForeColor = Color.Red And Trim(txtEMRNo.Text) _
            <> "") Or (lblChart.ForeColor <> Color.Red)) Then
                lblPatient.BackColor = Color.PaleGreen
            Else
                lblPatient.BackColor = Color.PeachPuff
            End If
        End If
    End Sub

    Private Sub btnPatLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatLook.Click
        Dim PatientID As String = frmPatLookUp.ShowDialog()
        If PatientID <> "" Then
            DisplayPatient(Val(PatientID))
            'PopulatePatientDxs(PatientID)
            'ClearOrders()
            UpdatePrimResp(Val(PatientID))
            If PatientCovered(Val(PatientID)) = True Then
                DisplayCoverage(Val(PatientID))
                rbT.Checked = True
            Else
                rbC.Checked = True
            End If
        End If
        Update_Patient_Status()
        Update_Billing_Status()
        UpdateRequisitionProgress()
    End Sub

    Private Sub UpdatePrimResp(ByVal PatID As Long)
        chkSvcGratis.Checked = False
        Dim BillType As Integer = 0
        Dim sSQL As String = "Select BillingType_ID from Requisitions where Patient_ID = " & PatID & " order by AccessionDate DESC"
        Dim cnur As New SqlConnection(connString)
        cnur.Open()
        Dim cmdur As New SqlCommand(sSQL, cnur)
        cmdur.CommandType = CommandType.Text
        Dim drur As SqlDataReader = cmdur.ExecuteReader
        If drur.HasRows Then
            While drur.Read
                BillType = drur("BillingType_ID")
            End While
        End If
        cnur.Close()
        cnur = Nothing
        'If btnNew.Checked = False Then
        If BillType = 0 Then
            rbC.Checked = True
        ElseIf BillType = 1 Then
            rbT.Checked = True
        Else
            rbP.Checked = True
        End If
        'lblBilling.BackColor = Color.PaleGreen
        'End If
    End Sub

    Private Sub btnRemPat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemPat.Click
        ClearPatient()
        ClearPrimary()
        ClearSecondary()
        Update_Patient_Status()
        UpdateRequisitionProgress()
    End Sub

    Private Sub btnRemProv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemProv.Click
        txtOrdID.Text = ""
        ClearOrderer()
        dgvRptProviders.Rows.Clear()
        Update_Rpt_Status()
        Update_Provider_Status()
        Update_Billing_Status()
        UpdateRequisitionProgress()
    End Sub

    Private Sub txtPatientID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtPatientID.BackColor = FCOLOR
    End Sub

    Private Sub txtPatientID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPatientID.KeyDown
        'On Error Resume Next
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
            If Not TabControl1.TabPages("tpOrderer") Is Nothing Then _
            TabControl1.SelectTab("tpOrderer")
        ElseIf e.KeyCode = Keys.PageDown Then
            If Not TabControl1.TabPages("tpOrders") Is Nothing Then _
                TabControl1.SelectTab("tpOrders")
        End If
    End Sub

    Private Sub txtPatientID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPatientID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub DisplayCoverage(ByVal PatientID As Long)
        Dim sSQL As String = "Select a.*, b.PayerName from Coverages a inner join " &
        "Payers b on a.Insurance_ID = b.ID where a.Patient_ID = " & PatientID
        Dim cnc As New SqlConnection(connString)
        cnc.Open()
        Dim cmdc As New SqlCommand(sSQL, cnc)
        cmdc.CommandType = CommandType.Text
        Dim drc As SqlDataReader = cmdc.ExecuteReader
        If drc.HasRows Then
            While drc.Read
                If drc("Preference") = "P" Then     'Primary
                    txtPInsID.Text = drc("Insurance_ID")
                    txtPInsName.Text = drc("PayerName")
                    If drc("GroupNo") IsNot DBNull.Value Then txtPGroup.Text = drc("GroupNo")
                    If drc("PolicyNo") IsNot DBNull.Value Then txtPPolicy.Text = drc("PolicyNo")
                    If drc("StartDate") IsNot DBNull.Value Then txtPFrom.Text = Format(drc("StartDate"), SystemConfig.DateFormat)
                    If drc("ExpireDate") IsNot DBNull.Value Then txtPTo.Text = Format(drc("ExpireDate"), SystemConfig.DateFormat)
                    If drc("Copayment") IsNot DBNull.Value Then txtPCopay.Text = drc("Copayment")
                    cmbPRelation.SelectedIndex = drc("Relation")
                    If cmbPRelation.SelectedIndex > 0 AndAlso drc("Insured_ID") _
                    IsNot DBNull.Value Then DisplayPrimInsured(drc("Insured_ID"))
                Else                                            'Secondary
                    txtSInsID.Text = drc("Insurance_ID")
                    txtSInsName.Text = drc("PayerName")
                    If drc("GroupNo") IsNot DBNull.Value Then txtSGroup.Text = drc("GroupNo")
                    If drc("PolicyNo") IsNot DBNull.Value Then txtSPolicy.Text = drc("PolicyNo")
                    If drc("StartDate") IsNot DBNull.Value Then txtSFrom.Text = Format(drc("StartDate"), SystemConfig.DateFormat)
                    If drc("ExpireDate") IsNot DBNull.Value Then txtSTo.Text = Format(drc("ExpireDate"), SystemConfig.DateFormat)
                    If drc("Copayment") IsNot DBNull.Value Then txtSCopay.Text = drc("Copayment")
                    cmbSRelation.SelectedIndex = drc("Relation")
                    If cmbSRelation.SelectedIndex > 0 AndAlso drc("Insured_ID") _
                    IsNot DBNull.Value Then DisplaySecondInsured(drc("Insured_ID"))
                End If
            End While
        End If
        cnc.Close()
        cnc = Nothing
    End Sub

    Private Sub DisplayPrimInsured(ByVal InsuredID As Long)
        Dim sSQL As String = "Select * from Patients where ID = " & InsuredID
        If connString <> "" Then
            Dim cndpi As New SqlConnection(connString)
            cndpi.Open()
            Dim cmddpi As New SqlCommand(sSQL, cndpi)
            cmddpi.CommandType = CommandType.Text
            Dim drdpi As SqlDataReader = cmddpi.ExecuteReader
            If drdpi.HasRows Then
                While drdpi.Read
                    txtPSubID.Text = drdpi("ID").ToString
                    txtPSubLName.Text = drdpi("LastName")
                    txtPSubFName.Text = drdpi("FirstName")
                    txtPSubMName.Text = If(IsDBNull(drdpi("MiddleName")), String.Empty, drdpi("MiddleName").ToString())
                    For i As Integer = 0 To cmbPSubSex.Items.Count - 1
                        If cmbPSubSex.Items(i).ToString.Substring(0, 1) = drdpi("Sex") Then
                            cmbPSubSex.SelectedIndex = i
                            Exit For
                        End If
                    Next
                    txtPSubDOB.Text = Format(drdpi("DOB"), SystemConfig.DateFormat)
                    If drdpi("SSN") IsNot DBNull.Value Then txtPSubSSN.Text = drdpi("SSN")
                    If drdpi("Address_ID") IsNot DBNull.Value Then
                        txtPSubAdd1.Text = GetAddress1(drdpi("Address_ID"))
                        txtPSubAdd2.Text = GetAddress2(drdpi("Address_ID"))
                        txtPSubCity.Text = GetAddressCity(drdpi("Address_ID"))
                        txtPSubState.Text = GetAddressState(drdpi("Address_ID"))
                        txtPSubZip.Text = GetAddressZip(drdpi("Address_ID"))
                        txtPSubCountry.Text = GetAddressCountry(drdpi("Address_ID"))
                    End If
                    If drdpi("HomePhone") IsNot DBNull.Value Then _
                    txtPsubHPhone.Text = PhoneNeat(drdpi("HomePhone"))
                    If drdpi("Email") IsNot DBNull.Value Then _
                    txtPSubEmail.Text = drdpi("Email")
                End While
            End If
            cndpi.Close()
            cndpi = Nothing
        Else
            Dim cndpi As New Odbc.OdbcConnection(connString)
            cndpi.Open()
            Dim cmddpi As New Odbc.OdbcCommand(sSQL, cndpi)
            cmddpi.CommandType = CommandType.Text
            Dim drdpi As Odbc.OdbcDataReader = cmddpi.ExecuteReader
            If drdpi.HasRows Then
                While drdpi.Read
                    txtPSubID.Text = drdpi("ID").ToString
                    txtPSubLName.Text = drdpi("LastName")
                    txtPSubFName.Text = drdpi("FirstName")
                    txtPSubMName.Text = drdpi("MiddleName")
                    For i As Integer = 0 To cmbPSubSex.Items.Count - 1
                        If cmbPSubSex.Items(i).ToString.Substring(0, 1) = drdpi("Sex") Then
                            cmbPSubSex.SelectedIndex = i
                            Exit For
                        End If
                    Next
                    txtPSubDOB.Text = Format(drdpi("DOB"), SystemConfig.DateFormat)
                    If drdpi("SSN") IsNot DBNull.Value Then txtPSubSSN.Text = drdpi("SSN")
                    If drdpi("Address_ID") IsNot DBNull.Value Then
                        txtPSubAdd1.Text = GetAddress1(drdpi("Address_ID"))
                        txtPSubAdd2.Text = GetAddress2(drdpi("Address_ID"))
                        txtPSubCity.Text = GetAddressCity(drdpi("Address_ID"))
                        txtPSubState.Text = GetAddressState(drdpi("Address_ID"))
                        txtPSubZip.Text = GetAddressZip(drdpi("Address_ID"))
                        txtPSubCountry.Text = GetAddressCountry(drdpi("Address_ID"))
                    End If
                    If drdpi("HomePhone") IsNot DBNull.Value Then _
                    txtPsubHPhone.Text = PhoneNeat(drdpi("HomePhone"))
                    If drdpi("Email") IsNot DBNull.Value Then _
                    txtPSubEmail.Text = drdpi("Email")
                End While
            End If
            cndpi.Close()
            cndpi = Nothing
        End If
    End Sub

    Private Sub DisplaySecondInsured(ByVal InsuredID As Long)
        Dim sSQL As String = "Select * from Patients where ID = " & InsuredID
        If connString <> "" Then
            Dim cndsi As New SqlConnection(connString)
            cndsi.Open()
            Dim cmddsi As New SqlCommand(sSQL, cndsi)
            cmddsi.CommandType = CommandType.Text
            Dim drdsi As SqlDataReader = cmddsi.ExecuteReader
            If drdsi.HasRows Then
                While drdsi.Read
                    txtSSubID.Text = drdsi("ID").ToString
                    txtSSubLName.Text = drdsi("LastName")
                    txtSSubFName.Text = drdsi("FirstName")
                    txtSSubMName.Text = drdsi("MiddleName")
                    For i As Integer = 0 To cmbSSubSex.Items.Count - 1
                        If cmbSSubSex.Items(i).ToString.Substring(0, 1) = drdsi("Sex") Then
                            cmbSSubSex.SelectedIndex = i
                            Exit For
                        End If
                    Next
                    txtSSubDOB.Text = Format(drdsi("DOB"), SystemConfig.DateFormat)
                    If drdsi("SSN") IsNot DBNull.Value Then txtSSubSSN.Text = drdsi("SSN")
                    If drdsi("Address_ID") IsNot DBNull.Value Then
                        txtSSubAdd1.Text = GetAddress1(drdsi("Address_ID"))
                        txtSSubAdd2.Text = GetAddress2(drdsi("Address_ID"))
                        txtSSubCity.Text = GetAddressCity(drdsi("Address_ID"))
                        txtSSubState.Text = GetAddressState(drdsi("Address_ID"))
                        txtSSubZip.Text = GetAddressZip(drdsi("Address_ID"))
                        txtSSubCountry.Text = GetAddressCountry(drdsi("Address_ID"))
                    End If
                    If drdsi("HomePhone") IsNot DBNull.Value Then _
                    txtSSubPhone.Text = PhoneNeat(drdsi("HomePhone"))
                    If drdsi("Email") IsNot DBNull.Value Then _
                    txtSSubEmail.Text = drdsi("Email")
                End While
            End If
            cndsi.Close()
            cndsi = Nothing
        Else
            Dim cndsi As New Odbc.OdbcConnection(connString)
            cndsi.Open()
            Dim cmddsi As New Odbc.OdbcCommand(sSQL, cndsi)
            cmddsi.CommandType = CommandType.Text
            Dim drdsi As Odbc.OdbcDataReader = cmddsi.ExecuteReader
            If drdsi.HasRows Then
                While drdsi.Read
                    txtSSubID.Text = drdsi("ID").ToString
                    txtSSubLName.Text = drdsi("LastName")
                    txtSSubFName.Text = drdsi("FirstName")
                    txtSSubMName.Text = drdsi("MiddleName")
                    For i As Integer = 0 To cmbSSubSex.Items.Count - 1
                        If cmbSSubSex.Items(i).ToString.Substring(0, 1) = drdsi("Sex") Then
                            cmbSSubSex.SelectedIndex = i
                            Exit For
                        End If
                    Next
                    txtSSubDOB.Text = Format(drdsi("DOB"), SystemConfig.DateFormat)
                    If drdsi("SSN") IsNot DBNull.Value Then txtSSubSSN.Text = drdsi("SSN")
                    If drdsi("Address_ID") IsNot DBNull.Value Then
                        txtSSubAdd1.Text = GetAddress1(drdsi("Address_ID"))
                        txtSSubAdd2.Text = GetAddress2(drdsi("Address_ID"))
                        txtSSubCity.Text = GetAddressCity(drdsi("Address_ID"))
                        txtSSubState.Text = GetAddressState(drdsi("Address_ID"))
                        txtSSubZip.Text = GetAddressZip(drdsi("Address_ID"))
                        txtSSubCountry.Text = GetAddressCountry(drdsi("Address_ID"))
                    End If
                    If drdsi("HomePhone") IsNot DBNull.Value Then _
                    txtSSubPhone.Text = PhoneNeat(drdsi("HomePhone"))
                    If drdsi("Email") IsNot DBNull.Value Then _
                    txtSSubEmail.Text = drdsi("Email")
                End While
            End If
            cndsi.Close()
            cndsi = Nothing
        End If
    End Sub

    Private Sub ClearOrders()
        'dgvTGPMarked.Rows.Clear()
        dgvTGPMarked.RowCount = 100
        Dim i As Integer
        For i = 0 To dgvTGPMarked.RowCount - 1
            dgvTGPMarked.Rows(i).Cells(0).Value = ""
            dgvTGPMarked.Rows(i).Cells(2).Value = ""
            dgvTGPMarked.Rows(i).Cells(3).Value = System.Drawing.Image.FromFile(
            Application.StartupPath & "\Images\Blank.ico")
            dgvTGPMarked.Rows(i).Cells(4).Value = 0
            dgvTGPMarked.Rows(i).Cells(5).Value = 0
        Next
        chkPhlebotomy.Checked = False
        lblOrders.BackColor = Color.PeachPuff
    End Sub

    Private Function PatientCovered(ByVal PatientID As Long) As Boolean
        Dim Cov As Boolean = False
        Dim cnpc As New SqlConnection(connString)
        cnpc.Open()
        Dim cmdpc As New SqlCommand("Select * from Coverages where Patient_ID = " & PatientID, cnpc)
        cmdpc.CommandType = CommandType.Text
        Dim drpc As SqlDataReader = cmdpc.ExecuteReader
        If drpc.HasRows Then Cov = True
        cnpc.Close()
        cnpc = Nothing
        Return Cov
    End Function

    Private Sub UpdateOrdStatus()
        Dim i As Integer
        Dim HasComponent As Boolean = False
        For i = 0 To dgvTGPMarked.RowCount - 1
            If dgvTGPMarked.Rows(i).Cells(2).Value <> "" Then HasComponent = True
        Next
        If HasComponent = True Then
            lblOrders.BackColor = Color.PaleGreen
        Else
            lblOrders.BackColor = Color.PeachPuff
        End If
    End Sub

    Private Function GetTGPMaterials(ByVal TGPID As String) As String()
        Dim MATS() As String = {""}
        Dim sSQL As String = ""
        sSQL = "Select distinct Name from Materials where ID in (Select Material_ID from Test_Material where Test_ID in " &
        "(Select d.ID as TestID from Tests d inner join (Group_Test c inner join Prof_GrpTst e on e.GrpTst_ID = c.Group_ID) " &
        "on c.Test_ID = d.ID where d.IsActive <> 0 and e.Profile_ID = " & TGPID & " Union Select a.GrpTst_ID as TestID " &
        "from Prof_GrpTst a inner join Tests b on a.GrpTst_ID = b.ID where b.IsActive <> 0 and a.Profile_ID = " & TGPID &
        " Union Select f.Test_ID as TestID from Group_Test f inner join Tests g on g.ID = f.Test_ID where g.IsActive <> " &
        "0 and f.Group_ID = " & TGPID & " Union Select ID as TestID from Tests where IsActive <> 0 and ID = " & TGPID & "))"
        If connString <> "" Then
            Dim cnn As New SqlConnection(connString)
            cnn.Open()
            Dim selcmd As New SqlCommand(sSQL, cnn)
            Dim selDR As SqlDataReader = selcmd.ExecuteReader
            If selDR.HasRows Then
                While selDR.Read
                    If Not TESTinTESTS(selDR("Name"), MATS) Then
                        MATS = AddTESTinTESTS(selDR("Name"), MATS)
                    End If
                End While
            End If
            selDR.Close()
            selcmd.Dispose()
            cnn.Close()
            cnn = Nothing
        Else
            Dim cnn As New Odbc.OdbcConnection(connString)
            cnn.Open()
            Dim selcmd As New Odbc.OdbcCommand(sSQL, cnn)
            Dim selDR As Odbc.OdbcDataReader = selcmd.ExecuteReader
            If selDR.HasRows Then
                While selDR.Read
                    If Not TESTinTESTS(selDR("Name"), MATS) Then
                        MATS = AddTESTinTESTS(selDR("Name"), MATS)
                    End If
                End While
            End If
            selDR.Close()
            selcmd.Dispose()
            cnn.Close()
            cnn = Nothing
        End If
        Return MATS
    End Function

    Private Function ExtMarkable(ByVal TGPID As Integer) As Boolean
        Dim Markable As Boolean = False
        If txtPatientID.Text <> "" Then
            Dim Sex As String = ""
            Dim Age As Integer = 1
            '
            Dim cnn As New SqlConnection(connString)
            cnn.Open()
            Dim selcmd As New SqlCommand("Select * from Patients where ID = " & Val(txtPatientID.Text), cnn)
            selcmd.CommandType = Data.CommandType.Text
            Dim selDR As SqlDataReader = selcmd.ExecuteReader
            If selDR.HasRows Then
                While selDR.Read
                    Sex = selDR("Sex")
                    Age = DateDiff(DateInterval.Year, selDR("DOB"), System.DateTime.Today)
                End While
            End If
            selDR.Close()
            selcmd.Dispose()
            cnn.Close()
            cnn = Nothing
            '
            Dim cn1 As New SqlConnection(connString)
            cn1.Open()
            Dim Ecmd As New SqlCommand("Select * from MarkingModifiers where Test_ID = " & TGPID, cn1)
            Ecmd.CommandType = Data.CommandType.Text
            Dim EDR As SqlDataReader = Ecmd.ExecuteReader
            If EDR.HasRows Then
                While EDR.Read
                    If EDR("Sex") = Sex And EDR("AgeFrom") <= Age _
                    And EDR("AgeTo") >= Age Then Markable = True
                End While
            Else
                Markable = True
            End If
            EDR.Close()
            Ecmd.Dispose()
            cn1.Close()
            cn1 = Nothing
        Else
            Markable = True
        End If
        Return Markable
    End Function

    Private Function TGPMarked(ByVal TGP_ID As Integer) As Integer
        Dim TGP As Integer = 0
        Dim i As Integer
        For i = 0 To dgvTGPMarked.RowCount - 1
            'If dgvTGPMarked.Rows(i).Cells(0).Value.ToString = "" Then dgvTGPMarked.Rows(i).Cells(0).Value = Nothing
            If dgvTGPMarked.Rows(i).Cells(0).Value IsNot Nothing _
            AndAlso dgvTGPMarked.Rows(i).Cells(0).Value =
            TGP_ID.ToString Then TGP = TGP + 1
        Next
        TGPMarked = TGP
    End Function

    Private Sub DisplayProviderProfile(ByVal ProvID As Long)
        Dim cnpp As New SqlConnection(connString)
        cnpp.Open()
        Dim cmdpp As New SqlCommand("Select * from Providers where Active <> 0 and ID = " & ProvID, cnpp)
        cmdpp.CommandType = Data.CommandType.Text
        Dim drpp As SqlDataReader = cmdpp.ExecuteReader
        If drpp.HasRows Then
            While drpp.Read
                txtRptRcptID.Text = ProvID.ToString
                If drpp("IsIndividual") = True Then
                    txtRptRcptName.Text = drpp("LastName_BSN") & ", " & drpp("FirstName")
                Else
                    txtRptRcptName.Text = drpp("LastName_BSN")
                End If
                chkRDMAuto.Checked = drpp("RDM_Auto")
                chkRptComplete.Checked = drpp("RptComplete")
                chkPrint.Checked = drpp("RDM_Print")
                chkProlison.Checked = drpp("RDM_Prolison")
                chkInterface.Checked = drpp("RDM_Interface")
                chkRptFax.Checked = drpp("RDM_Fax")
                If drpp("Fax") IsNot DBNull.Value Then txtRPTFax.Text = drpp("Fax")
                chkrptEmail.Checked = drpp("RDM_Email")
                If drpp("Email") IsNot DBNull.Value Then txtRptEmail.Text = drpp("Email")
                btnRptAdd.Enabled = True
            End While
        Else
            MsgBox("Invalid provider ID")
            txtRptRcptID.Text = ""
            UpdateProv_Status()
            txtRptRcptID.Focus()
        End If
        cnpp.Close()
        cnpp = Nothing
    End Sub

    Private Sub txtRptRcptID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.Up Then
            TabControl1.SelectTab("tpOrders")
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtRptRcptID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Numerals(sender, e)
    End Sub
    Private Sub UpdateProv_Status()
        If txtAccID.Text <> "" And txtRptRcptID.Text <> "" And txtRptRcptName.Text <> "" Then
            btnRptAdd.Enabled = True
            If chkrptEmail.Checked = True And txtRptEmail.Text = "" Then btnRptAdd.Enabled = True
            If chkRptFax.Checked = True And txtRPTFax.Text = "" Then btnRptAdd.Enabled = False
        Else
            btnRptAdd.Enabled = False
        End If
    End Sub
    Private Sub Update_Provider_Status()
        If txtOrdID.Text <> "" And lstProviders.CheckedItems.Count > 0 Then
            lblOrderer.BackColor = Color.PaleGreen
        Else
            lblOrderer.BackColor = Color.PeachPuff
        End If
    End Sub
    Private Sub Prov_ProfileClear()
        txtRptRcptID.Text = ""
        txtRptRcptName.Text = ""
        chkRptComplete.Checked = False
        chkPrint.Checked = True
        chkrptEmail.Checked = False
        txtRptEmail.Text = ""
        chkRptFax.Checked = False
        txtRPTFax.Text = ""
    End Sub

    Private Sub Update_Rpt_Sec(ByVal RptSetting() As String)
        Dim i As Integer
        Dim n As Integer
        Dim Data() As String
        Dim KPS() As String
        Dim Comps() As String
        Dim ProviderNAme As String
        Dim RDMAuto As Boolean = False
        Dim IsComplete As Boolean = False
        Dim IsPrint As Boolean
        Dim Prolison As Boolean
        Dim RDMInterface As Boolean
        Dim IsEmail As Boolean
        Dim Email As String = ""
        Dim IsFax As Boolean
        Dim Fax As String = ""
        For i = 0 To RptSetting.Length - 1
            If RptSetting(i).ToString <> "" Then    'Valid setting
                Data = Split(RptSetting(i).ToString, "|")
                ProviderNAme = Data(1)
                For n = 2 To Data.Length - 1
                    KPS = Split(Data(n), "=")
                    If KPS(0) = "RDM_Auto" Then
                        RDMAuto = KPS(1)
                    ElseIf KPS(0) = "RPTComplete" Then
                        IsComplete = KPS(1)
                    ElseIf KPS(0) = "RDM_Print" Then
                        IsPrint = KPS(1)
                    ElseIf KPS(0) = "RDM_Prolison" Then
                        Prolison = KPS(1)
                    ElseIf KPS(0) = "RDM_Interface" Then
                        RDMInterface = KPS(1)
                    ElseIf KPS(0) = "RDM_Fax" Then
                        If InStr(KPS(1), "^") > 0 Then
                            Comps = Split(KPS(1), "^")
                            IsFax = Comps(0)
                            Fax = Comps(1)
                        Else
                            IsFax = KPS(1)
                            Fax = ""
                        End If
                    ElseIf KPS(0) = "RDM_Email" Then
                        If InStr(KPS(1), "^") > 0 Then
                            Comps = Split(KPS(1), "^")
                            IsEmail = Comps(0)
                            Email = Comps(1)
                        Else
                            IsEmail = KPS(1)
                            Email = ""
                        End If
                    End If
                Next
                '
                If txtAccID.Text <> "" And Data(0).ToString <> "" Then
                    If Not Report_Recipient_In(Data(0).ToString) Then
                        dgvRptProviders.Rows.Add(Data(0).ToString, ProviderNAme,
                        RDMAuto, IsComplete, IsPrint, Prolison, RDMInterface,
                        IsFax, Fax, IsEmail, Email)
                        lblReports.BackColor = Color.PaleGreen
                    End If
                End If
            End If
        Next
    End Sub
    Private Sub RemoveFromRpt_Sec(ByVal Prov_ID As Long)
        Dim i As Integer
        For i = 0 To dgvRptProviders.RowCount - 1
            If dgvRptProviders.Rows(i).Cells(0).Value = Prov_ID Then
                dgvRptProviders.Rows.Remove(dgvRptProviders.Rows(i))
                Exit For
            End If
        Next
    End Sub

    Private Sub Update_Rpt_Status()
        If dgvRptProviders.RowCount > 0 Then
            btnRemRptAll.Enabled = True
            lblReports.BackColor = Color.PaleGreen
        Else
            lblReports.BackColor = Color.PeachPuff
            btnRemRptAll.Enabled = False
        End If
    End Sub
    Private Sub Update_Specimen_Status()
        If dgvSources.RowCount > 0 Then
            lblSpecimen.BackColor = Color.PaleGreen
        Else
            lblSpecimen.BackColor = Color.PeachPuff
        End If
    End Sub
    Private Function Report_Recipient_In(ByVal Prov_ID As Long) As Boolean
        Dim i As Integer
        Dim Prov_IN As Boolean = False
        For i = 0 To dgvRptProviders.RowCount - 1
            If dgvRptProviders.Rows(i).Cells(0).Value = Prov_ID Then
                Prov_IN = True
                Exit For
            End If
        Next
        Report_Recipient_In = Prov_IN
    End Function

    Private Sub txtRptEmail_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtRptEmail.BackColor = FCOLOR
    End Sub

    Private Sub txtRptEmail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtRptEmail_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtRptEmail.BackColor = NFCOLOR
        UpdateProv_Status()
    End Sub

    Private Sub txtRptFax_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtRPTFax.BackColor = FCOLOR
    End Sub

    Private Sub txtRptFax_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtRptFax_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtRPTFax.BackColor = NFCOLOR
        UpdateProv_Status()
    End Sub

    Private Sub Update_SourceString()
        Sources = "Service, "
        Dim i As Integer
        If dgvSources.RowCount > 0 Then
            For i = 0 To dgvSources.RowCount - 1
                Sources = Sources & GetSourceMaterial(dgvSources.Rows(i).Cells(0).Value) & ", "
            Next
            'Sources = Sources.Substring(0, Len(Sources) - 2)
        Else
            Sources = "Service, "
        End If
    End Sub

    Private Function GetSourceMaterial(ByVal Source_ID As Integer) As String
        Dim Mat As String = ""
        Dim cngsm As New SqlConnection(connString)
        cngsm.Open()
        Dim cmdgsm As New SqlCommand("Select Name from Materials where ID " &
        "in (Select Material_ID from Sources where ID = " & Source_ID & ")", cngsm)
        cmdgsm.CommandType = CommandType.Text
        Dim drgsm As SqlDataReader = cmdgsm.ExecuteReader
        If drgsm.HasRows Then
            While drgsm.Read
                Mat = drgsm("Name")
            End While
        End If
        cngsm.Close()
        cngsm = Nothing
        Return Mat
    End Function

    Private Sub BillingClear()
        rbC.Checked = True
        txtCopay.Text = ""
    End Sub
    Private Sub chkSvcGratis_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSvcGratis.CheckedChanged
        If chkSvcGratis.Checked = False Then
            chkSvcGratis.Text = "Charge"
            BillingEnabled()
            Update_Billing_Status()
        Else
            chkSvcGratis.Text = "Gratis"
            BillingClear()
            BillingDisabled()
            lblBilling.BackColor = Color.PaleGreen
        End If
        UpdateRequisitionProgress()
    End Sub
    Private Sub BillingEnabled()
        rbC.Enabled = True
        rbT.Enabled = True
        rbP.Enabled = True
        btnCalculate.Enabled = True
        txtCopay.Enabled = True
        btnPmnt.Enabled = True
        TabControl2.Enabled = True
    End Sub
    Private Sub BillingDisabled()
        rbC.Enabled = False
        rbT.Enabled = False
        rbP.Enabled = False
        btnCalculate.Enabled = False
        txtCopay.Enabled = False
        btnPmnt.Enabled = False
        TabControl2.Enabled = False
    End Sub

    Private Sub ClearGuarantor()
        txtGID.Text = ""
        txtGLName_BSN.Text = ""
        txtGFName.Text = ""
        txtGMName.Text = ""
        txtGDOB.Text = ""
        txtGSSN.Text = ""
        txtGPhone.Text = ""
        txtGEmail.Text = ""
        txtGAdd1.Text = ""
        txtGAdd2.Text = ""
        txtGCity.Text = ""
        txtGState.Text = ""
        txtGZip.Text = ""
        txtGCountry.Text = ""
        chkGIsIndividual.Checked = True
    End Sub

    Private Sub ClearSecondary()
        txtSInsID.Text = ""
        txtSInsName.Text = ""
        txtSGroup.Text = ""
        txtSPolicy.Text = ""
        txtSFrom.Text = ""
        txtSTo.Text = ""
        txtSCopay.Text = ""
        cmbSRelation.SelectedIndex = -1
        ClearSSubscriber()
    End Sub

    Private Sub ClearPrimary()
        txtPInsID.Text = ""
        txtPInsName.Text = ""
        txtPGroup.Text = ""
        txtPPolicy.Text = ""
        txtPFrom.Text = ""
        txtPTo.Text = ""
        txtPCopay.Text = ""
        chkWorkman.Checked = False
        txtCovCmnt.Text = ""
        cmbPRelation.SelectedIndex = -1
        ClearPSubscriber()
    End Sub

    Private Sub ClearPSubscriber()
        txtPSubID.Text = ""
        txtPSubLName.Text = ""
        txtPSubFName.Text = ""
        txtPSubMName.Text = ""
        cmbPSubSex.SelectedIndex = -1
        txtPSubDOB.Text = ""
        txtPSubSSN.Text = ""
        txtPSubAdd1.Text = ""
        txtPSubAdd2.Text = ""
        txtPSubCity.Text = ""
        txtPSubState.Text = ""
        txtPSubZip.Text = ""
        txtPSubCountry.Text = ""
        txtPsubHPhone.Text = ""
        txtPSubEmail.Text = ""
    End Sub

    Private Sub ClearSSubscriber()
        txtSSubID.Text = ""
        txtSSubLName.Text = ""
        txtSSubFName.Text = ""
        txtSSubMName.Text = ""
        cmbSSubSex.SelectedIndex = -1
        txtSSubDOB.Text = ""
        txtSSubSSN.Text = ""
        txtSSubAdd1.Text = ""
        txtSSubAdd2.Text = ""
        txtSSubCity.Text = ""
        txtSSubState.Text = ""
        txtSSubZip.Text = ""
        txtSSubCountry.Text = ""
        txtSSubPhone.Text = ""
        txtSSubEmail.Text = ""
    End Sub

    Private Function GetPayerPar(ByVal PayerID As Long) As Boolean
        Dim IsPAR As Boolean = False
        Dim cnpp As New SqlConnection(connString)
        cnpp.Open()
        Dim cmdpp As New SqlCommand("Select IsPar from " &
        "Payers where ID = " & PayerID, cnpp)
        cmdpp.CommandType = CommandType.Text
        Dim drpp As SqlDataReader = cmdpp.ExecuteReader
        If drpp.HasRows Then
            While drpp.Read
                IsPAR = drpp("IsPar")
            End While
        End If
        cnpp.Close()
        cnpp = Nothing
        Return IsPAR
    End Function

    Private Sub btnPIns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPIns.Click
        Dim PayerInfo As String = frmActivePayersLookUp.ShowDialog()
        If PayerInfo <> "" Then
            Dim PRS() As String = Split(PayerInfo, "|")
            If PRS(0) <> txtSInsID.Text Then
                txtPInsID.Text = PRS(0)
                If SystemConfig.ParInHouse = True Then
                    If txtInEditReason.Text = "" Then _
                    chkInHouse.Checked = GetPayerPar(Val(PRS(0)))
                End If
                If PRS.Length >= 2 Then
                    txtPInsName.Text = PRS(1)
                    If cmbPRelation.SelectedIndex = -1 Then _
                    cmbPRelation.SelectedIndex = 0
                    Update_Billing_Status()
                    UpdateRequisitionProgress()
                End If
            Else
                MsgBox("The Insurance you selected, is the secondary " &
                "coverage in the accession.", MsgBoxStyle.Critical, "Prolis")
            End If
        End If
    End Sub

    Private Sub btnPSubLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPSubLook.Click
        Dim PatID As String = frmPatLookUp.ShowDialog()
        If PatID <> "" Then
            DisplayPrimInsured(Val(PatID))
            Update_Billing_Status()
            UpdateRequisitionProgress()
        End If
    End Sub

    Private Sub btnSSubLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSSubLook.Click
        Dim PatID As String = frmPatLookUp.ShowDialog()
        If PatID <> "" Then
            DisplaySecondInsured(Val(PatID))
            Update_Billing_Status()
        End If
    End Sub

    Private Sub cmbSRelation_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSRelation.SelectedIndexChanged
        If cmbSRelation.SelectedIndex = 0 Then
            ClearSSubscriber()
            'If txtPatientID.Text <> "" Then DisplaySecondInsured(Val(txtPatientID.Text))
        Else
            ClearSSubscriber()
            If txtSSubID.Text <> "" Then DisplaySecondInsured(Val(txtSSubID.Text))
        End If
        Update_Billing_Status()
        UpdateRequisitionProgress()
    End Sub

    Private Sub cmbPRelation_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPRelation.GotFocus
        cmbPRelation.BackColor = FCOLOR
    End Sub

    Private Sub cmbPRelation_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbPRelation.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbPRelation_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPRelation.LostFocus
        cmbPRelation.BackColor = NFCOLOR
    End Sub

    Private Sub cmbPRelation_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPRelation.SelectedIndexChanged
        If cmbPRelation.SelectedIndex = 0 Then
            ClearPSubscriber()
            btnPSubLook.Enabled = False
            'If txtPatientID.Text <> "" Then DisplayPrimInsured(Val(txtPatientID.Text))
        Else
            ClearPSubscriber()
            btnPSubLook.Enabled = True
            If txtPSubID.Text <> "" Then DisplayPrimInsured(Val(txtPSubID.Text))
        End If
        Update_Billing_Status()
        UpdateRequisitionProgress()
    End Sub

    Private Sub btnCalculate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalculate.Click
        txtCopay.Text = ""
        If rbC.Checked = True Then   'Orderer
            txtCopay.Text = Format(GetOrdererCharges(), "##,##0.00")
        ElseIf rbT.Checked = True Then   '3rd Party
            txtCopay.Text = IIf(txtPCopay.Text <> "", Format(Val(txtPCopay.Text), "##,##0.00"),
            IIf(txtSCopay.Text <> "", Format(Val(txtSCopay.Text), "##,##0.00"), "0.00"))
        Else                                        'Patient
            txtCopay.Text = Format(GetPatientCharges(), "##,##0.00")
        End If
    End Sub

    Private Function GetPatientCharges() As Single
        Dim i As Integer
        Dim Charge As Single = 0
        If dgvTGPMarked.RowCount = 0 Then
            GetPatientCharges = 0
        Else
            For i = 0 To dgvTGPMarked.RowCount - 1
                If Not dgvTGPMarked.Rows(i).Cells(0).Value Is Nothing AndAlso
                dgvTGPMarked.Rows(i).Cells(0).Value.ToString <> "" Then _
                    Charge = Charge + GetPatientTGP(dgvTGPMarked.Rows(i).Cells(0).Value)
            Next
            GetPatientCharges = Charge
        End If
    End Function

    Private Function Get3rdPartyCharges() As Single
        Dim i As Integer
        Dim Charge As Single = 0
        Dim InsID As Long = CLng(txtPInsID.Text)
        If dgvTGPMarked.RowCount = 0 Then
            Get3rdPartyCharges = 0
        Else
            For i = 0 To dgvTGPMarked.RowCount - 1
                Charge = Charge + Get3rdPartyTGP(InsID, dgvTGPMarked.Rows(i).Cells(1).Value)
            Next
            Get3rdPartyCharges = Charge
        End If
    End Function

    Private Function GetOrdererCharges() As Single
        Dim i As Integer
        Dim Charge As Single = 0
        If dgvTGPMarked.RowCount = 0 Then
            GetOrdererCharges = 0
        Else
            For i = 0 To dgvTGPMarked.RowCount - 1
                If dgvTGPMarked.Rows(i).Cells(0).Value.ToString <> "" Then _
                Charge = Charge + GetOrdererTGP(Val(txtOrdID.Text), dgvTGPMarked.Rows(i).Cells(0).Value)
            Next
            GetOrdererCharges = Charge
        End If
    End Function

    Private Function GetPayerPriceLevel(ByVal PayerID As Long) As Integer
        Dim PL As Integer = 0
        Dim cnpl As New SqlConnection(connString)
        cnpl.Open()
        Dim cmdpl As New SqlCommand("Select " &
        "PriceLevel from Payers where ID = " & PayerID, cnpl)
        cmdpl.CommandType = CommandType.Text
        Dim drpl As SqlDataReader = cmdpl.ExecuteReader
        If drpl.HasRows Then
            While drpl.Read
                PL = drpl("PriceLevel")
            End While
        End If
        cnpl.Close()
        cnpl = Nothing
        Return PL
    End Function

    Private Function GetProviderPriceLevel(ByVal ProviderID As Long) As Integer
        Dim PL As Integer = 0
        Dim cnpl As New SqlConnection(connString)
        cnpl.Open()
        Dim cmdpl As New SqlCommand("Select " &
        "PriceLevel from Providers where ID = " & ProviderID, cnpl)
        cmdpl.CommandType = CommandType.Text
        Dim drpl As SqlDataReader = cmdpl.ExecuteReader
        If drpl.HasRows Then
            While drpl.Read
                PL = drpl("PriceLevel")
            End While
        End If
        cnpl.Close()
        cnpl = Nothing
        Return PL
    End Function

    Private Function GetPatientPriceLevel() As Integer
        Dim PL As Integer = 0
        Dim cnpl As New SqlConnection(connString)
        cnpl.Open()
        Dim cmdpl As New SqlCommand("Select PatientPriceLevel " &
        "from System_Config where Company_ID = " & MyLab.ID, cnpl)
        cmdpl.CommandType = CommandType.Text
        Dim drpl As SqlDataReader = cmdpl.ExecuteReader
        If drpl.HasRows Then
            While drpl.Read
                PL = drpl("PatientPriceLevel")
            End While
        End If
        cnpl.Close()
        cnpl = Nothing
        Return PL
    End Function

    Private Function GetPatientTGP(ByVal TGPID As Integer) As Single
        Dim Price As Single = 0
        Dim PriceLevel As Integer = GetPatientPriceLevel()
        Dim cnpl As New SqlConnection(connString)
        cnpl.Open()
        Dim cmdpl As New SqlCommand("Select Price1, Price2, Price3, Price4, Price5, Price6, " &
        "Price7, Price8, Price9, ListPrice from Tests where ID = " & TGPID & " Union Select Price1, " &
        "Price2, Price3, Price4, Price5, Price6, Price7, Price8, Price9, ListPrice from Groups where " &
        "ID = " & TGPID & " Union Select Price1, Price2, Price3, Price4, Price5, Price6, Price7, " &
        "Price8, Price9, ListPrice from Profiles where ID = " & TGPID, cnpl)
        cmdpl.CommandType = CommandType.Text
        Dim drpl As SqlDataReader = cmdpl.ExecuteReader
        If drpl.HasRows Then
            While drpl.Read
                If PriceLevel = 1 Then
                    Price = drpl("Price1")
                ElseIf PriceLevel = 2 Then
                    Price = drpl("Price2")
                ElseIf PriceLevel = 3 Then
                    Price = drpl("Price3")
                ElseIf PriceLevel = 4 Then
                    Price = drpl("Price4")
                ElseIf PriceLevel = 5 Then
                    Price = drpl("Price5")
                ElseIf PriceLevel = 6 Then
                    Price = drpl("Price6")
                ElseIf PriceLevel = 7 Then
                    Price = drpl("Price7")
                ElseIf PriceLevel = 8 Then
                    Price = drpl("Price8")
                ElseIf PriceLevel = 9 Then
                    Price = drpl("Price9")
                Else
                    Price = drpl("ListPrice")
                End If
            End While
        End If
        cnpl.Close()
        cnpl = Nothing
        Return Price
    End Function

    Private Function Get3rdPartyTGP(ByVal PayerID As Long, ByVal TGPID As Integer) As Single
        Dim Price As Single = -1
        Dim PriceLevel As Integer = -1
        Dim cnppl As New SqlConnection(connString)
        cnppl.Open()
        Dim cmdppl As New SqlCommand("Select a.Price, b.PriceLevel " &
        "from Payers b Left outer join Payer_TGP a on b.ID = a.Payer_ID where " &
        "a.TGP_ID = " & TGPID & " and b.ID = " & PayerID, cnppl)
        cmdppl.CommandType = CommandType.Text
        Dim drppl As SqlDataReader = cmdppl.ExecuteReader
        If drppl.HasRows Then
            While drppl.Read
                If drppl("Price") IsNot DBNull.Value Then
                    Price = drppl("Price")
                Else
                    PriceLevel = drppl("PriceLevel")
                End If
            End While
        End If
        cnppl.Close()
        cnppl = Nothing
        '
        If Price < 0 AndAlso PriceLevel >= 0 Then
            Dim cnpl As New SqlConnection(connString)
            cnpl.Open()
            Dim cmdpl As New SqlCommand("Select Price1, Price2, Price3, Price4, " &
            "Price5, Price6, Price7, Price8, Price9, ListPrice from Tests where ID = " & TGPID &
            " Union Select Price1, Price2, Price3, Price4, Price5, Price6, Price7, Price8, " &
            "Price9, ListPrice from Groups where ID = " & TGPID & " Union Select Price1, " &
            "Price2, Price3, Price4, Price5, Price6, Price7, Price8, Price9, ListPrice from " &
            "Profiles where ID = " & TGPID, cnpl)
            cmdpl.CommandType = CommandType.Text
            Dim drpl As SqlDataReader = cmdpl.ExecuteReader
            If drpl.HasRows Then
                While drpl.Read
                    If PriceLevel = 1 Then
                        Price = drpl("Price1")
                    ElseIf PriceLevel = 2 Then
                        Price = drpl("Price2")
                    ElseIf PriceLevel = 3 Then
                        Price = drpl("Price3")
                    ElseIf PriceLevel = 4 Then
                        Price = drpl("Price4")
                    ElseIf PriceLevel = 5 Then
                        Price = drpl("Price5")
                    ElseIf PriceLevel = 6 Then
                        Price = drpl("Price6")
                    ElseIf PriceLevel = 7 Then
                        Price = drpl("Price7")
                    ElseIf PriceLevel = 8 Then
                        Price = drpl("Price8")
                    ElseIf PriceLevel = 9 Then
                        Price = drpl("Price9")
                    Else
                        Price = drpl("ListPrice")
                    End If
                End While
            End If
            cnpl.Close()
            cnpl = Nothing
        End If
        Return Price
    End Function

    Private Function GetOrdererTGP(ByVal ProvID As Long, ByVal TGPID As Integer) As Single
        Dim Price As Single = -1
        Dim PriceLevel As Integer = -1
        Dim cnppl As New SqlConnection(connString)
        cnppl.Open()
        Dim cmdppl As New SqlCommand("Select a.Price, b.PriceLevel " &
        "from Providers b Left outer join Provider_TGP a on b.ID = a.Provider_ID " &
        "where a.TGP_ID = " & TGPID & " and b.ID = " & ProvID, cnppl)
        cmdppl.CommandType = CommandType.Text
        Dim drppl As SqlDataReader = cmdppl.ExecuteReader
        If drppl.HasRows Then
            While drppl.Read
                If drppl("Price") IsNot DBNull.Value Then
                    Price = drppl("Price")
                Else
                    PriceLevel = drppl("PriceLevel")
                End If
            End While
        End If
        cnppl.Close()
        cnppl = Nothing
        '
        If Price < 0 AndAlso PriceLevel >= 0 Then
            Dim cnpl As New SqlConnection(connString)
            cnpl.Open()
            Dim cmdpl As New SqlCommand("Select Price1, Price2, Price3, Price4, Price5, Price6, " &
            "Price7, Price8, Price9, ListPrice from Tests where ID = " & TGPID & " Union Select Price1, " &
            "Price2, Price3, Price4, Price5, Price6, Price7, Price8, Price9, ListPrice from Groups where " &
            "ID = " & TGPID & " Union Select Price1, Price2, Price3, Price4, Price5, Price6, Price7, " &
            "Price8, Price9, ListPrice from Profiles where ID = " & TGPID, cnpl)
            cmdpl.CommandType = CommandType.Text
            Dim drpl As SqlDataReader = cmdpl.ExecuteReader
            If drpl.HasRows Then
                While drpl.Read
                    If PriceLevel = 1 Then
                        Price = drpl("Price1")
                    ElseIf PriceLevel = 2 Then
                        Price = drpl("Price2")
                    ElseIf PriceLevel = 3 Then
                        Price = drpl("Price3")
                    ElseIf PriceLevel = 4 Then
                        Price = drpl("Price4")
                    ElseIf PriceLevel = 5 Then
                        Price = drpl("Price5")
                    ElseIf PriceLevel = 6 Then
                        Price = drpl("Price6")
                    ElseIf PriceLevel = 7 Then
                        Price = drpl("Price7")
                    ElseIf PriceLevel = 8 Then
                        Price = drpl("Price8")
                    ElseIf PriceLevel = 9 Then
                        Price = drpl("Price9")
                    Else
                        Price = drpl("ListPrice")
                    End If
                End While
            End If
            cnpl.Close()
            cnpl = Nothing
        End If
        Return Price
    End Function

    Private Sub txtTime_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtTime.BackColor = FCOLOR
    End Sub

    Private Sub txtTime_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtTime_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        TimeEntry(sender, e)
    End Sub

    Private Sub txtTime_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtTime.BackColor = NFCOLOR
        If Not IsDate(txtTime.Text) Then
            txtTime.Text = Format(Date.Now, "hh:mm tt")
            txtTime.Focus()
        End If
    End Sub

    Private Sub txtRequisition_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtRequisition.BackColor = FCOLOR
    End Sub

    Private Sub txtRequisition_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtRequisition_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtRequisition.BackColor = NFCOLOR
        'If txtRequisition.Text = "" Then txtRequisition.Text = txtAccID.Text
    End Sub

    Private Sub txtPInsID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPPolicy.GotFocus
        txtPPolicy.BackColor = FCOLOR
    End Sub

    Private Sub txtPInsID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPPolicy.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtPInsID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPPolicy.LostFocus
        txtPPolicy.BackColor = NFCOLOR
        If txtPPolicy.Text <> "" Then
            If cmbPRelation.SelectedIndex = -1 Then _
            cmbPRelation.SelectedIndex = 0
            Update_Billing_Status()
            UpdateRequisitionProgress()
        End If
    End Sub

    Private Sub cmbPIns_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtSInsID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSInsID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtSInsID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSInsID.LostFocus
        If txtSInsID.Text <> "" Then
            Update_Billing_Status()
            UpdateRequisitionProgress()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim proceed = True
        If cmbPRelation.SelectedIndex <> 0 And cmbPRelation.SelectedIndex <> -1 Then

            If String.IsNullOrEmpty(txtPSubAdd1.Text) Then
                Dim rs = MessageBox.Show("Subscriber's Address is required, Do you want to proceed? click 'OK' to save it, or click 'Cancel' to add it", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
                proceed = IIf(rs = DialogResult.OK, True, False)
            End If
        End If
        If cmbSRelation.SelectedIndex <> 0 And cmbSRelation.SelectedIndex <> -1 Then
            If String.IsNullOrEmpty(txtSSubAdd1.Text) Then
                Dim rs = MessageBox.Show("Subscriber's Address is required, Do you want to proceed? click 'OK' to save it, or click 'Cancel' to add it", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
                proceed = IIf(rs = DialogResult.OK, True, False)
            End If
        End If
        barcode.Text = ""
        If cmbGRelation.SelectedIndex <> 0 And cmbGRelation.SelectedIndex <> -1 Then
            If String.IsNullOrEmpty(txtGAdd1.Text) Then
                Dim rs = MessageBox.Show("Subscriber's Address is required, Do you want to proceed? click 'OK' to save it, or click 'Cancel' to add it", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
                proceed = IIf(rs = DialogResult.OK, True, False)
            End If
        End If
        If proceed = False Then
            Return
        End If

        Dim correctDate = True
        Dim rcDate As Date = CDate(Format(dtpDate.Value,
            SystemConfig.DateFormat) & " " & txtTime.Text)
        If txtRecDate.Text.Contains(" ") Then
            rcDate = CDate(DateTime.Now)

        Else
            rcDate = CDate(txtRecDate.Text & " " & txtRecTime.Text)

        End If
        For i As Integer = 0 To dgvSources.Rows.Count - 1
            Dim vv = dgvSources.Rows(i).Cells(4).Value & " " & dgvSources.Rows(i).Cells(5).Value
            Dim dddd = CDate(vv)
            If rcDate < CDate(vv) Then
                MessageBox.Show("Receive Date cannot be before  Drawn Date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                correctDate = False
            End If
        Next
        If lblRequisition.BackColor = Color.PaleGreen And lblSpecimen.BackColor =
        Color.PaleGreen And lblOrderer.BackColor = Color.PaleGreen And correctDate And
        lblPatient.BackColor = Color.PaleGreen And lblOrders.BackColor =
        Color.PaleGreen And lblReports.BackColor = Color.PaleGreen And
        lblBilling.BackColor = Color.PaleGreen Then
            lblStatus.Text = ""
            My.Application.DoEvents()
            Dim stopWatch As New Stopwatch()
            stopWatch.Start()
            '
            btnSave.Enabled = False
            '
            If cmbSpecimenType.SelectedIndex = 0 Then
                Dim ItemX As MyList = cmbRace.SelectedItem
                Dim ItemS As MyList
                Dim ItemB As MyList
                If SystemConfig.DiagTarget = "V" Then
                    If cmbSpecies.SelectedIndex <> -1 Then
                        ItemS = cmbSpecies.SelectedItem
                    Else
                        ItemS = New MyList("Human", 0)
                    End If
                    If cmbBreed.SelectedIndex <> -1 Then
                        ItemB = cmbBreed.SelectedItem
                    Else
                        ItemB = New MyList("Human", 0)
                    End If
                Else
                    ItemS = New MyList("Human", 0)
                    ItemB = New MyList("Human", 0)
                End If
                txtPatientID.Text = UpdatePatient(txtPatientID.Text, txtLName.Text, txtFName.Text, txtMName.Text,
                cmbSex.SelectedItem.ToString.Substring(0, 1), Trim(txtTage.Text), CDate(txtDOB.Text), ItemS.ItemData,
                ItemB.ItemData, Trim(txtSSN.Text), Trim(txtPatEmail.Text), PhoneNeat(txtPatHPhone.Text), PhoneNeat(txtWPhone.Text),
                PhoneNeat(txtFax.Text), PhoneNeat(txtCell.Text), ItemX.ItemData, cmbEthnicity.SelectedItem.ToString, txtPatAdr1.Text,
                txtPatAdr2.Text, txtPatCity.Text, txtPatState.Text, txtPatZip.Text, txtPatCountry.Text)
                'If Trim(txtEMRNo.Text) <> "" Then UpdateClientPatient(Val(txtOrdID.Text), _
                'Val(txtPatientID.Text), Trim(txtEMRNo.Text), Trim(txtRoom.Text))
                If txtPatientID.Text <> "" Then LockPatVitalFields()
            End If
            If (dgvSources.RowCount > 0 Or chkPostPrePhleb.Checked) And
            (txtOrdID.Text <> "" And lstProviders.CheckedItems.Count > 0) And
            ((cmbSpecimenType.SelectedIndex = 0 And txtPatientID.Text <> "") Or
            (cmbSpecimenType.SelectedIndex > 0)) And HasTGPOrdered() = True And
            dgvRptProviders.RowCount > 0 And ((chkSvcGratis.Checked = True) Or
            ((chkSvcGratis.Checked = False And rbC.Checked = True And
            txtOrdID.Text <> "") Or (chkSvcGratis.Checked = False And rbP.Checked = True And
            txtPatientID.Text <> "") Or (chkSvcGratis.Checked = False And
            rbT.Checked = True And txtPInsID.Text <> "" And txtPPolicy.Text <> "" And
            cmbPRelation.SelectedIndex = 0) Or (chkSvcGratis.Checked = False And
            rbT.Checked = True And txtPInsID.Text <> "" And txtPPolicy.Text <> "" And
            cmbPRelation.SelectedIndex > 0 And txtPSubID.Text <> ""))) Then     '2nd check
                If UnreceivedSrcs.EndsWith(", ") Then _
                UnreceivedSrcs = Microsoft.VisualBasic.Mid(UnreceivedSrcs,
                1, Len(UnreceivedSrcs) - 2)
                If UnreceivedSrcs <> "" Then _
                txtWorkCmnt.Text += UnreceivedSrcs & " not received - Prolis" & vbCrLf
                Dim AccID As Long = SaveRequisition()
                If AccID = 0 Then

                End If
                SaveReqDxs(AccID)
                SaveReqMeds(AccID)
                SaveReqTGP(AccID)
                SaveReqTests(AccID)
                If ReqInfoResp <> "" Then UpdatePreAnaResponse_OLD(AccID, ReqInfoResp)

                'If Req_Info_Response_MainList.Count > 0 Then UpdatePreAnaResponse(AccID, Req_Info_Response_MainList)
                'Req_Info_Response_MainList.Clear()

                SaveReqReports(AccID)
                '---------------------------------------------------

                '---------------------------------------------------
                If Not chkPostPrePhleb.Checked Then 'ACC
                    SaveSpecimen(AccID)
                    If chkReject.Checked = True Then
                        ExecuteSqlProcedure("Update Acc_Results set Result = 'TNP', Released = 1, Released_By = " & ThisUser.ID &
                        ", Release_Time = '" & Date.Now & "' where (Result is Null or Result = '') and Accession_ID = " & Val(txtAccID.Text))
                        ExecuteSqlProcedure("Update Ref_Results set Result = 'TNP', Released = 1, Released_By = " & ThisUser.ID &
                        ", Release_Time = '" & Date.Now & "' where (Result is Null or Result = '') and Accession_ID = " & Val(txtAccID.Text))
                        ExecuteSqlProcedure("Update Acc_Info_Results set Result = 'TNP', Released = 1, Released_By = " & ThisUser.ID &
                        ", Release_Time = '" & Date.Now & "' where (Result is Null or Result = '') and Accession_ID = " & Val(txtAccID.Text))
                        ExecuteSqlProcedure("Update Requisitions set RPT_Status = 'REJECTED', Comment = '" & Trim(txtRejectReason.Text) &
                        "' where ID = " & Val(txtAccID.Text))
                        ExecuteSqlProcedure("If Exists (Select * from Requisitions where IsDate(ReportedOn) <> 0 and ID = " &
                        Val(txtAccID.Text) & ") Update Requisitions set Reported_Final = ReportedOn where Reported_Final is " &
                        "Null and ID = " & Val(txtAccID.Text) & " Else If Exists (Select * from Requisitions where " &
                        "IsDate(Reported_Initial) <> 0 and ID = " & Val(txtAccID.Text) & ") Update Requisitions set " &
                        "Reported_Final = Reported_Initial where Reported_Final is Null and ID = " & Val(txtAccID.Text) &
                        " Else Update Requisitions set Reported_Final = '" & Date.Now & "' where Reported_Final is " &
                        "Null and ID = " & Val(txtAccID.Text))
                        '
                        UpsertUser_Event(ThisUser.ID, 5, Date.Now, "Accession",
                        Trim(txtAccID.Text), "", Trim(txtRejectReason.Text))
                    Else
                        If ReportFullResulted(AccID) = True Then
                            ExecuteSqlProcedure("Update Requisitions Set Reported_Final = '" & Format(Date.Now,
                            "MM/dd/yyyy HH:mm") & "', RPT_Status = 'FINAL' where ID = " & AccID &
                            " and Reported_Final is NULL")
                        ElseIf ReportPartialResulted(AccID) Then
                            ExecuteSqlProcedure("Update Requisitions Set Reported_Final =NULL , RPT_Status = 'PARTIAL' where ID = " & AccID &
                           " ;")
                        Else
                            ExecuteSqlProcedure("Update Requisitions Set Reported_Final = NULL, RPT_Status = '' where ID = " & AccID)
                            ExecuteSqlProcedure("Update Acc_Results set Result = '', Released = 0, Released_By = Null, Release_Time = Null " &
                            "Where Result = 'TNP' and Accession_ID = " & Val(txtAccID.Text))
                            ExecuteSqlProcedure("Update Ref_Results set Result = '', Released = 0, Released_By = Null, Release_Time = Null " &
                            "Where Result = 'TNP' and Accession_ID = " & Val(txtAccID.Text))
                            ExecuteSqlProcedure("Update Acc_Info_Results set Result = '', Released = 0, Released_By = Null, Release_Time = " &
                            "Null where Result = 'TNP' and Accession_ID = " & Val(txtAccID.Text))
                            ExecuteSqlProcedure("Update Requisitions set RPT_Status = 'INITIAL', RejectReason = '" & Trim(txtRejectReason.Text) &
                            "', Comment = '" & Replace(txtComment.Text, txtRejectReason.Text, "") & "' where ID = " & Val(txtAccID.Text))
                        End If
                    End If
                    '

                    Dim IsVerbal As Boolean = False
                    Dim i As Integer
                    For i = 0 To dgvTGPMarked.RowCount - 1
                        If dgvTGPMarked.Rows(i).Cells(5).Value = True Then
                            IsVerbal = True
                            Exit For
                        End If
                    Next
                    If IsVerbal Then PrintVerbalAuthorization(AccID)
                    '
                    If Val(txtLabels.Text) > 0 Then
                        Dim Printer As String = GetLabelPrinterName()
                        If Not ThisUser.SpecificPrinter = "Default" Then
                            Printer = ThisUser.SpecificPrinter
                        End If
                        If Printer <> "" Then
                            If txtNavStatus.Text = "" Then
                                PrintLabels(Printer, AccID.ToString, CInt(txtLabels.Text),,, 0)
                            End If

                        Else
                            MsgBox("The accession record has been saved under accession " &
                            "number: " & AccID.ToString, MsgBoxStyle.Information, "Prolis Accession")

                        End If
                    End If
                    '
                    If SystemConfig.AuditTrail = True Then
                        'Dim AT363 As String = GetBillingChanges(CB)
                        Dim AccCurVals As String = GetCurrentAccVals(AccID)
                        If btnNew.Checked = False Then  'New
                            LogUserEvent(ThisUser.ID, 1, Date.Now, "Accession", AccID, "", AccCurVals)
                            'LogUserEvent(ThisUser.ID, 363, Date.Now, "Accession", AccID, "", "")
                        Else    'Edit
                            'If Microsoft.VisualBasic.Left(AT363, InStr(AT363, ":") - 1) = "Changed" Then _
                            'LogUserEvent(ThisUser.ID, 351, Date.Now, _
                            '"Accession", AccID, "Last Billing: " & CB, "Changed to: " & AT363)
                            'Dim AT351 As String = GetNewOrders(ExistTGPs)
                            'If AT351 <> "" Then LogUserEvent(ThisUser.ID, 351, Date.Now, _
                            '"Accession", AccID, "Orders", "Added=" & AT351)
                            'AT351 = ""
                            'Dim AT352 As String = GetDeletedOrders(ExistTGPs)
                            'If AT352 <> "" Then 
                            If AccCurVals <> AccPrevVals Then LogUserEvent(ThisUser.ID,
                            3, Date.Now, "Accession", AccID, AccPrevVals, AccCurVals)
                        End If
                    End If
                    If QrChk.Checked Then
                        Dim success As Boolean = QR.GenerateQrForAccession(AccID, MyLab.QRSec.Token, MyLab.QRSec.DNS, MyLab.ID.ToString())
                    End If
                Else
                    UpdateSpecimen(AccID)
                    If QrChk.Checked Then
                        Dim success As Boolean = QR.GenerateQrForAccession(AccID, MyLab.QRSec.Token, MyLab.QRSec.DNS, MyLab.ID.ToString())
                    End If
                End If
                '
                Clipboard.SetText(AccID)
                ClearForm()
                UpdateReqStatus()
                If btnNew.Checked = False Then  'New
                    AccID += 1
                    If IsUniqueAccession(AccID) Then
                        txtAccID.Text = AccID.ToString
                    Else
                        txtAccID.Text = NextAccessionID(dtpDate.Value, txtPatientID.Text)
                    End If
                    'txtRequisition.Text = txtAccID.Text
                    txtAccID.Focus()
                    SearchMode = False
                Else
                    SearchMode = True
                End If
            End If
            '
            stopWatch.Stop()
            lblStatus.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            stopWatch.Elapsed.Hours, stopWatch.Elapsed.Minutes,
            stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds / 10)
            My.Application.DoEvents()
            '

        Else

            If correctDate Then
                MsgBox("Accession ID, Specimen Type, the ordering and attending " &
                           "providers, Specimen Entry, Test orders and at least one report " &
                           "delivery option is required. If the specimen is clinical, the " &
                           "patient entry is also required.")
            End If

        End If

    End Sub

    Private Function GetTestFormula(ByVal TestID As Integer) As String
        Dim Formula As String = ""
        Dim cntf As New SqlConnection(connString)
        cntf.Open()
        Dim cmdtf As New SqlCommand("Select * from Tests where " &
        "IsCalculated <> 0 and Formula <> '' and ID = " & TestID, cntf)
        cmdtf.CommandType = CommandType.Text
        Dim drtf As SqlDataReader = cmdtf.ExecuteReader
        If drtf.HasRows Then
            While drtf.Read
                Formula = drtf("Formula")
            End While
        End If
        cntf.Close()
        cntf = Nothing
        Return Formula
    End Function

    Private Function GetTestIDsFromFormula(ByVal Formula As String) As String
        Dim TestIDs As String = ""
        Dim TestID As String = ""
        Dim NstClcTIDs As String = ""
        Dim P1 As Integer
        Dim P2 As Integer
        Do Until Formula = "" Or (InStr(Formula, "{") = 0 And InStr(Formula, "{") = 0)
            P1 = InStr(Formula, "{")
            P2 = InStr(Formula, "}")
            If P1 > 0 AndAlso P2 > P1 Then
                TestID = Microsoft.VisualBasic.Mid(Formula, P1 + 1)
                TestID = Microsoft.VisualBasic.Mid(TestID, 1, InStr(TestID, "}") - 1)
                Formula = Microsoft.VisualBasic.Mid(Formula, P2 + 1)
                If GetTestFormula(TestID) = "" Then
                    If InStr(TestIDs, TestID & ",") = 0 _
                    Then TestIDs += TestID & ", "
                Else
                    NstClcTIDs += TestID & ", "
                End If
                TestID = ""
            End If
        Loop
        '
        If NstClcTIDs.Length > 2 AndAlso Microsoft.VisualBasic.Right(NstClcTIDs, 2) = ", " _
        Then NstClcTIDs = Microsoft.VisualBasic.Mid(NstClcTIDs, 1, Len(NstClcTIDs) - 2)
        Do Until NstClcTIDs = ""
            Dim CalcdIDs() As String = Split(NstClcTIDs, ",")
            Dim Formula2 As String = ""
            For i As Integer = 0 To CalcdIDs.Length - 1
                Formula2 = GetTestFormula(Val(Trim(CalcdIDs(i))))
                NstClcTIDs = Microsoft.VisualBasic.Mid(NstClcTIDs,
                InStr(NstClcTIDs, Trim(CalcdIDs(i))) + Len(Trim(CalcdIDs(i))))
                Do Until Formula2 = "" Or (InStr(Formula2, "{") = 0 And InStr(Formula2, "{") = 0)
                    P1 = InStr(Formula2, "{")
                    P2 = InStr(Formula2, "}")
                    If P1 > 0 AndAlso P2 > P1 Then
                        TestID = Microsoft.VisualBasic.Mid(Formula2, P1 + 1)
                        TestID = Microsoft.VisualBasic.Mid(TestID, 1, InStr(TestID, "}") - 1)
                        Formula2 = Microsoft.VisualBasic.Mid(Formula2, P2 + 1)
                        If GetTestFormula(TestID) = "" Then
                            If InStr(TestIDs, TestID & ",") = 0 _
                            Then TestIDs += TestID & ", "
                        Else
                            NstClcTIDs += TestID & ", "
                        End If
                        TestID = ""
                    End If
                Loop
            Next
            If Trim(NstClcTIDs) = "," Then NstClcTIDs = ""
        Loop
        '
        'If TestIDs.Length > 2 AndAlso Microsoft.VisualBasic.Right(TestIDs, 2) = ", " _
        'Then TestIDs = Microsoft.VisualBasic.Mid(TestIDs, 1, Len(TestIDs) - 2)
        Return TestIDs
    End Function

    Private Function GetAccessTests(ByVal AccID As Long) As String
        Dim TestIDs As String = ""
        Dim sSQL As String = "Select a.* from Tests a inner join " &
        "Acc_Results b on a.ID = b.Test_ID where b.Accession_ID = " & AccID
        Dim cngat As New SqlConnection(connString)
        cngat.Open()
        Dim cmdgat As New SqlCommand(sSQL, cngat)
        cmdgat.CommandType = Data.CommandType.Text
        Dim drgat As SqlDataReader = cmdgat.ExecuteReader()
        If drgat.HasRows Then
            While drgat.Read
                If drgat("IsCalculated") <> 0 AndAlso drgat("Formula") <> "" Then
                    TestIDs += GetTestIDsFromFormula(drgat("Formula"))
                    If Not TestIDs.EndsWith(", ") Then TestIDs += ", "
                Else
                    If InStr(TestIDs, drgat("ID").ToString & ",") = 0 Then
                        TestIDs += drgat("ID").ToString & ", "
                    End If
                End If
            End While
        End If
        cngat.Close()
        cngat = Nothing
        If TestIDs.EndsWith(", ") Then TestIDs = TestIDs.Remove(TestIDs.Length - 2, 2)
        Return TestIDs
    End Function

    Private Sub UpdateSpecimen(ByVal AccID As Long)
        Dim TestIDs As String = GetAccessTests(AccID)
        Dim AllMats As String = ""
        Dim MATS(1, 0) As String    '0=MatID, 1=MatCount
        '
        If btnNew.Checked = True OrElse MergeAccs = False Then  'edit or no merge
            ExecuteSqlProcedure("Delete from Specimens where Accession_ID = " & AccID)  'delete specimen
        End If
        '
        Dim cnn As New SqlConnection(connString)
        cnn.Open()
        Dim sSQL As String = "Select a.MATERIAL_ID as MatID, count(a.MATERIAL_ID) as Matcount from " &
        "Test_Material a where a.Material_ID in (Select Top 1 b.Material_ID from Test_Material b where " &
        "b.TEST_ID = a.Test_ID) and a.Test_ID in (" & TestIDs & ") group by Material_ID order by Matcount desc"
        Dim selcmd As New SqlCommand(sSQL, cnn)
        selcmd.CommandType = Data.CommandType.Text
        Dim selDR As SqlDataReader = selcmd.ExecuteReader()
        If selDR.HasRows Then
            While selDR.Read
                If selDR("MatID") IsNot DBNull.Value _
                AndAlso selDR("MatID").ToString <> "" Then
                    If MATS(0, UBound(MATS, 2)) <> "" Then _
                    ReDim Preserve MATS(1, UBound(MATS, 2) + 1)
                    MATS(0, UBound(MATS, 2)) = selDR("MatID").ToString
                    MATS(1, UBound(MATS, 2)) = selDR("Matcount").ToString
                    AllMats += selDR("MatID").ToString & ", "
                End If
            End While
        End If
        selDR.Close()
        selcmd.Dispose()
        cnn.Close()
        cnn = Nothing
        '
        Dim SRCID As String = ""
        For i As Integer = 0 To UBound(MATS, 2) 'mats
            SRCID = GetSourceID(Val(MATS(0, i)))
            If SRCID <> "" Then
                Dim cn1 As New SqlConnection(connString)
                cn1.Open()
                Dim spcmd As New SqlCommand("SaveSpecimenUpsert_SP_OR", cn1)
                spcmd.CommandType = Data.CommandType.StoredProcedure
                '(@acc int,@sid smallint,@sno nvarchar(25)=null,@sq real=null,@sdate datetime=null,@stemp nvarchar(25)=null,@isready bit=true,@comm nvarchar(250)=null)
                spcmd.Parameters.AddWithValue("@acc", AccID)
                spcmd.Parameters.AddWithValue("@SID", Val(SRCID))
                spcmd.Parameters.AddWithValue("@SNo", AccID.ToString & "-" & (i + 1).ToString)
                If Val(MATS(1, i)) <= 30 Then
                    spcmd.Parameters.AddWithValue("@SQ", 1)
                ElseIf Val(MATS(1, i)) > 30 And MATS(1, i) <= 60 Then
                    spcmd.Parameters.AddWithValue("@SQ", 2)
                ElseIf Val(MATS(1, i)) > 60 And MATS(1, i) <= 90 Then
                    spcmd.Parameters.AddWithValue("@SQ", 3)
                ElseIf Val(MATS(1, i)) > 90 And MATS(1, i) <= 120 Then
                    spcmd.Parameters.AddWithValue("@SQ", 4)
                ElseIf Val(MATS(1, i)) > 120 And MATS(1, i) <= 150 Then
                    spcmd.Parameters.AddWithValue("@SQ", 5)
                Else
                    spcmd.Parameters.AddWithValue("@SQ", 6)
                End If
                spcmd.Parameters.AddWithValue("@SDate", CDate(Format(dtpDateDrawn.Value,
                SystemConfig.DateFormat) & " " & txtDrawnTime.Text.Trim))
                spcmd.Parameters.AddWithValue("@STemp", "Room Temp")
                spcmd.Parameters.AddWithValue("@IsReady", 1)
                spcmd.Parameters.AddWithValue("@Comm", "")
                spcmd.ExecuteNonQuery()
                cn1.Close()
                cn1 = Nothing
            End If
        Next
    End Sub

    Private Function MatInMats(ByVal Mat As String, ByVal Mats() As String) As Boolean
        Dim InMats As Boolean = False
        Dim i As Integer
        For i = 0 To Mats.Length - 1
            If Mat = Mats(i) Then
                InMats = True
                Exit For
            End If
        Next
        Return InMats
    End Function

    Private Function GetSourceID(ByVal MatID As Integer) As String
        Dim SRCID As String = ""
        Dim cnsid As New SqlConnection(connString)
        cnsid.Open()
        Dim cmdsid As New SqlCommand("Select ID from Sources where Material_ID = " & MatID, cnsid)
        cmdsid.CommandType = CommandType.Text
        Dim drsid As SqlDataReader = cmdsid.ExecuteReader
        If drsid.HasRows Then
            While drsid.Read
                SRCID = drsid("ID").ToString
            End While
        End If
        cnsid.Close()
        cnsid = Nothing
        Return SRCID
    End Function

    Private Function GetPatientAcc(ByVal PatientID As Long, ByVal AccDate As Date) As String
        Dim AccID As String = ""
        Dim sSQL As String = "Select ID from Requisitions where Patient_ID = " &
        PatientID & " And AccessionDate between '" & Format(AccDate, "MM/dd/yyyy") &
        "' and '" & Format(AccDate, "MM/dd/yyyy") & " 23:59:00'"
        Dim cnpa As New SqlConnection(connString)
        cnpa.Open()
        Dim cmdpa As New SqlCommand(sSQL, cnpa)
        cmdpa.CommandType = CommandType.Text
        Dim drpa As SqlDataReader = cmdpa.ExecuteReader
        If drpa.HasRows Then
            While drpa.Read
                AccID = drpa("ID").ToString
            End While
        End If
        cnpa.Close()
        cnpa = Nothing
        Return AccID
    End Function

    Private Function GetCurrentAccVals(ByVal AccID As Long) As String
        Dim AccVals As String = ""
        Dim i As Integer
        Dim TempVal As String = ""
        Dim VALS() As String = {"", "", "", "", "", "", "", ""} 'AccId, AccDate, Specimen, Provider, Patient, Orders, Reports, Billing
        VALS(0) = "AccID = " & AccID.ToString
        VALS(1) = "AccDate = " & Format(dtpDate.Value, SystemConfig.DateFormat) & " " & txtTime.Text
        TempVal = "Specimen = "
        For i = 0 To dgvSources.RowCount - 1
            TempVal += Trim(dgvSources.Rows(i).Cells(2).Value) & ", " &
            Trim(dgvSources.Rows(i).Cells(3).Value.ToString) & ", " &
            dgvSources.Rows(i).Cells(4).Value & "^"
        Next
        If TempVal <> "" And Microsoft.VisualBasic.Right(TempVal, 1) = "^" Then _
        TempVal = Microsoft.VisualBasic.Mid(TempVal, 1, Len(TempVal) - 1)
        VALS(2) = TempVal
        TempVal = ""
        Dim ItemX As MyList
        If lstProviders.CheckedItems.Count > 0 Then
            ItemX = lstProviders.CheckedItems(0)
            VALS(3) = "Client = " & txtOrdID.Text & "^" & ItemX.ItemData.ToString
        Else
            VALS(3) = "Client = " & txtOrdID.Text & "^"
        End If
        If cmbSex.SelectedIndex <> -1 Then _
        VALS(4) = "Patient = " & txtLName.Text & ", " & txtFName.Text & " DOB: " & txtDOB.Text &
        " Sex: " & Microsoft.VisualBasic.Left(cmbSex.SelectedItem.ToString, 1) & "^" &
        txtPatAdr1.Text & ", " & txtPatCity.Text & ", " & txtPatState.Text & ", " & txtPatZip.Text
        TempVal = "Orders = "
        For i = 0 To dgvTGPMarked.RowCount - 1
            If dgvTGPMarked.Rows(i).Cells(0).Value IsNot Nothing AndAlso
            dgvTGPMarked.Rows(i).Cells(0).Value.ToString <> "" Then
                TempVal += dgvTGPMarked.Rows(i).Cells(0).Value & "^"
            End If
        Next
        If TempVal <> "" And Microsoft.VisualBasic.Right(TempVal, 1) = "^" Then _
        TempVal = Microsoft.VisualBasic.Mid(TempVal, 1, Len(TempVal) - 1)
        VALS(5) = TempVal
        TempVal = "Reports = "
        For i = 0 To dgvRptProviders.RowCount - 1
            TempVal += "ProviderID: " & dgvRptProviders.Rows(i).Cells(0).Value &
            ", RCO: " & dgvRptProviders.Rows(i).Cells(3).Value & ", Print: " &
            dgvRptProviders.Rows(i).Cells(4).Value & ", ProlisOn: " &
            dgvRptProviders.Rows(i).Cells(5).Value & ", Interface: " &
            dgvRptProviders.Rows(i).Cells(6).Value & ", Fax: " &
            dgvRptProviders.Rows(i).Cells(7).Value & "^"
        Next
        If TempVal <> "" And Microsoft.VisualBasic.Right(TempVal, 1) = "^" Then _
        TempVal = Microsoft.VisualBasic.Mid(TempVal, 1, Len(TempVal) - 1)
        VALS(6) = TempVal
        If rbC.Checked = True Then
            TempVal = "Billing = Client"
        ElseIf rbP.Checked = True Then
            TempVal = "Billing = Patient"
        Else
            TempVal = "Billing = Insurance"
            If txtPInsID.Text <> "" Then
                TempVal += ", Primary Payer ID: " & txtPInsID.Text & ", Policy: " &
                txtPPolicy.Text & ", Relation: " & cmbPRelation.SelectedIndex &
                ", Insured ID: " & IIf(cmbPRelation.SelectedIndex = 0, txtPatientID.Text,
                txtPSubID.Text)
            End If
            If txtSInsID.Text <> "" Then
                TempVal += "^Secondary Payer ID: " & txtSInsID.Text & ", Policy: " &
                txtSPolicy.Text & ", Relation: " & cmbSRelation.SelectedIndex &
                ", Insured ID: " & IIf(cmbSRelation.SelectedIndex = 0, txtPatientID.Text,
                txtSSubID.Text)
            End If
        End If
        VALS(7) = TempVal
        TempVal = ""
        AccVals = Join(VALS, "|")
        Return AccVals
    End Function

    Private Function GetBillingChanges(ByVal CB As String) As String
        Dim FB As String = "BillType="
        'BType, PArID, PPolicy, PGroup, PRel, PInsrd, SArID, SPolicy, SGroup, SRel, SInsrd, GArID, GIndEnt, GRel  
        If rbC.Checked = True Then   'Client
            FB += "0^ARID=" & txtOrdID.Text
        ElseIf rbP.Checked = True Then   'patient
            FB += "2^ARID=" & txtPatientID.Text
        Else
            If txtPInsID.Text <> "" And txtPPolicy.Text <> "" Then
                FB += "1^PARID=" & txtPInsID.Text & "^PPolicy=" & Trim(txtPPolicy.Text) &
                "^PGroup=" & Trim(txtPGroup.Text) & "^PRelation=" & cmbPRelation.SelectedIndex &
                IIf(cmbPRelation.SelectedIndex > 0, "^PInsuredID=" & txtPSubID.Text, "")
            End If
            If txtSInsID.Text <> "" And txtSPolicy.Text <> "" Then
                FB += "1^SARID=" & txtSInsID.Text & "^SPolicy=" & Trim(txtSPolicy.Text) &
                "^SGroup=" & Trim(txtSGroup.Text) & "^SRelation=" & cmbSRelation.SelectedIndex &
                IIf(cmbSRelation.SelectedIndex > 0, "^SInsuredID=" & txtSSubID.Text, "")
            End If
            If txtGID.Text <> "" Then
                FB += "1^GARID=" & txtGID.Text & "^GIndividual=" & chkGIsIndividual.Checked
            End If
        End If
        If CB = "" Then 'new 
            FB += "Added: " & FB
        Else
            If CB <> FB Then
                FB = "Changed: " & FB
            Else
                FB += ": " & FB
            End If
        End If
        Return FB
    End Function

    Private Sub UpdateClientPatient(ByVal ClientID As Long, ByVal PatID As Long, ByVal EMRNo As String, ByVal Room As String)
        ExecuteSqlProcedure("If Exists (Select * from Client_Patient where Provider_ID = " &
        ClientID & " and Patient_ID = " & PatID & ") Update EMRNo = '" & Trim(EMRNo) & "', " &
        "Room = '" & Trim(Room) & "', ClientUser_ID = " & ClientID & " where Provider_ID = " &
        ClientID & " and Patient_ID = " & PatID & " Else Insert into Client_Patient (" &
        "Provider_ID, Patient_ID, EMRNo, Room, ClientUser_ID) values (" & ClientID & ", " &
        PatID & ", '" & Trim(EMRNo) & "', '" & Trim(Room) & "', " & ClientID & ")")
    End Sub

    Private Function GetDeletedOrders(ByVal curtgps() As String) As String
        Dim i As Integer
        Dim n As Integer
        Dim sTGPs As String = ""
        Dim dTGP As String = ""
        'If curtgps.Length > 0 Then
        For i = 0 To curtgps.Length - 1
            dTGP = curtgps(i)
            For n = 0 To dgvTGPMarked.RowCount - 1
                If dgvTGPMarked.Rows(n).Cells(0).Value IsNot Nothing _
                AndAlso IsNumeric(dgvTGPMarked.Rows(n).Cells(0).Value) = True Then
                    If dTGP = dgvTGPMarked.Rows(i).Cells(0).Value Then
                        dTGP = ""
                        Exit For
                    End If
                End If
            Next
            If dTGP <> "" Then
                sTGPs += dTGP & "^"
                dTGP = ""
            End If
        Next
        If sTGPs.Length > 2 Then sTGPs = Microsoft.VisualBasic.Mid(sTGPs, 1, Len(sTGPs) - 1)
        'End If
        '
        Return sTGPs
    End Function

    Private Function GetNewOrders(ByVal Curtgps() As String) As String
        Dim i As Integer
        Dim n As Integer
        Dim sTGPs As String = ""
        Dim nTGP As String = ""
        For i = 0 To dgvTGPMarked.RowCount - 1
            If dgvTGPMarked.Rows(i).Cells(0).Value IsNot Nothing _
            AndAlso IsNumeric(dgvTGPMarked.Rows(i).Cells(0).Value) = True Then
                nTGP = dgvTGPMarked.Rows(i).Cells(0).Value
                For n = 0 To Curtgps.Length - 1
                    If nTGP = Curtgps(n) Then
                        nTGP = ""
                        Exit For
                    End If
                Next
                If nTGP <> "" Then
                    sTGPs += nTGP & "^"
                    nTGP = ""
                End If
            End If
        Next
        If sTGPs.Length > 2 Then sTGPs = Microsoft.VisualBasic.Mid(sTGPs, 1, Len(sTGPs) - 1)
        Return sTGPs
    End Function

    Private Sub PrintVerbalAuthorization(ByVal AccID As Long)
        'TODO: Crystal Report issue till End
        'Try
        '    Dim UID As String = My.Settings.UID.ToString
        '    Dim PWD As String = My.Settings.PWD.ToString
        '    Dim gReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        '    gReport.Load(Application.StartupPath & "\Reports\VerbalAuthorization.rpt")
        '    'Dim pSize As CrystalDecisions.Shared.PaperSize = gReport.PrintOptions.PaperSize
        '    gReport.SetDatabaseLogon(UID, PWD)
        '    gReport.RecordSelectionFormula = "{Req_TGP.Verbal} = True and {Requisitions.ID} = " & AccID
        '    'gReport.PrintOptions.PrinterName = Printer
        '    'gReport.PrintOptions.PaperSize = pSize
        '    gReport.PrintToPrinter(1, False, 0, 0)
        '    gReport.Close()
        '    gReport = Nothing
        'Catch Ex As Exception
        '    MsgBox("While printing Verbal Authorization, error encountered: " &
        '    Ex.Message, MsgBoxStyle.Critical, "Prolis")
        'End Try
    End Sub

    Private Function SavePayment(ByVal PMT() As String, ByVal AccID As Long) As Long
        Dim PaymentID As Long = GetNextPaymentID()
        Dim cnpid As New SqlConnection(connString)
        cnpid.Open()
        Dim cmdpid As New SqlCommand("Select * from Payments where ArType = " &
        Val(PMT(1)) & " and Ar_ID = " & Val(PMT(2)) & " and PaymentDate between '" &
        PMT(4) & "' and '" & PMT(4) & " 23:59:00' and DocNo = '" & PMT(5) & "'", cnpid)
        cmdpid.CommandType = CommandType.Text
        Dim drpid As SqlDataReader = cmdpid.ExecuteReader
        If drpid.HasRows Then
            While drpid.Read
                PaymentID = drpid("ID")
            End While
        End If
        cnpid.Close()
        cnpid = Nothing
        '
        ExecuteSqlProcedure("If Exists (Select * from Payments where ID = " & PaymentID & ") " &
        "Update Payments set ArType = " & Val(PMT(1)) & ", Ar_ID = " & Val(PMT(2)) & ", " &
        "PaymentType = " & Val(PMT(3)) & ", PaymentDate = '" & PMT(4) & "', DocNo = '" & PMT(5) &
        "', Accession_ID = " & AccID & ", Amount = " & Val(PMT(6)) & ", UnApplied = " & Val(PMT(6)) &
        " where ID = " & PaymentID & " Else Insert into Payments " & "(ID, ArType, Ar_ID, " &
        "PaymentType,  PaymentDate, Accession_ID, DocNo, Amount, UnApplied) values (" & PaymentID &
        ", " & Val(PMT(1)) & ", " & Val(PMT(2)) & ", " & Val(PMT(3)) & ", '" & PMT(4) & "', " &
        AccID & ", '" & PMT(5) & "', " & Val(PMT(6)) & ", " & Val(PMT(6)) & ")")
        '
        Return PaymentID
    End Function

    Private Function GetNextPaymentID() As Long
        Dim PMTID As Long = 1
        Dim cnpid As New SqlConnection(connString)
        cnpid.Open()
        Dim cmdpid As New SqlCommand("Select Max(ID) as LastID from Payments", cnpid)
        cmdpid.CommandType = CommandType.Text
        Dim drpid As SqlDataReader = cmdpid.ExecuteReader
        If drpid.HasRows Then
            While drpid.Read
                If drpid("LastID") IsNot DBNull.Value _
                Then PMTID = drpid("LastID") + 1
            End While
        End If
        cnpid.Close()
        cnpid = Nothing
        Return PMTID
    End Function

    Private Sub SaveReqMeds(ByVal AccID As Long)
        Dim GoodMeds As String = ""
        '
        For i As Integer = 0 To dgvMeds.RowCount - 1
            If dgvMeds.Rows(i).Cells(0).Value IsNot Nothing _
            AndAlso Trim(dgvMeds.Rows(i).Cells(0).Value) <> "" Then
                Dim cnn As New SqlConnection(connString)
                cnn.Open()
                Dim cmdupsert As New SqlCommand("Req_Med_SP", cnn)
                cmdupsert.CommandType = Data.CommandType.StoredProcedure
                cmdupsert.Parameters.AddWithValue("@act", "Upsert")
                cmdupsert.Parameters.AddWithValue("@Accession_ID", AccID)
                cmdupsert.Parameters.AddWithValue("@Medication", Trim(dgvMeds.Rows(i).Cells(0).Value))
                cmdupsert.Parameters.AddWithValue("@Ordinal", i)
                cmdupsert.ExecuteNonQuery()
                cmdupsert.Dispose()
                cmdupsert = Nothing
                GoodMeds += "'" & Trim(dgvMeds.Rows(i).Cells(0).Value) & "', "
                cnn.Close()
                cnn = Nothing
            End If
        Next
        If GoodMeds.EndsWith(", ") Then GoodMeds =
        Microsoft.VisualBasic.Mid(GoodMeds, 1, Len(GoodMeds) - 2)
        If btnNew.Checked = True OrElse MergeAccs = False Then  'edit or no merge
            If GoodMeds <> "" Then
                ExecuteSqlProcedure("Delete from Req_Med where Accession_ID = " _
                & AccID & " and Not Medication in (" & GoodMeds & ")")
            Else
                ExecuteSqlProcedure("Delete from Req_Med where Accession_ID = " & AccID)
            End If
        End If
    End Sub

    Private Sub SaveReqDxs(ByVal AccID As Long)
        Dim GoodCodes As String = ""
        For i As Integer = 0 To dgvDxs.RowCount - 1
            If dgvDxs.Rows(i).Cells(0).Value IsNot Nothing _
            AndAlso Trim(dgvDxs.Rows(i).Cells(0).Value) <> "" Then
                Dim cnn As New SqlConnection(connString)
                cnn.Open()
                Dim cmdupsert As New SqlCommand("Req_Dx_SP", cnn)
                cmdupsert.CommandType = Data.CommandType.StoredProcedure
                cmdupsert.Parameters.AddWithValue("@act", "Upsert")
                cmdupsert.Parameters.AddWithValue("@Accession_ID", AccID)
                cmdupsert.Parameters.AddWithValue("@Dx_Code", Trim(dgvDxs.Rows(i).Cells(0).Value))
                If i = 0 Then
                    cmdupsert.Parameters.AddWithValue("@IsPrimary", 1)
                Else
                    cmdupsert.Parameters.AddWithValue("@IsPrimary", 0)
                End If
                cmdupsert.Parameters.AddWithValue("@Ordinal", i)
                cmdupsert.ExecuteNonQuery()
                cmdupsert.Dispose()
                cmdupsert = Nothing
                GoodCodes += "'" & Trim(dgvDxs.Rows(i).Cells(0).Value) & "', "
                cnn.Close()
                cnn = Nothing
            End If
        Next
        If GoodCodes.EndsWith(", ") Then GoodCodes =
        Microsoft.VisualBasic.Mid(GoodCodes, 1, Len(GoodCodes) - 2)
        If btnNew.Checked = True OrElse MergeAccs = False Then  'edit or no merge
            If GoodCodes <> "" Then
                ExecuteSqlProcedure("Delete from Req_Dx where Accession_ID = " _
                & AccID & " and Not Dx_Code in (" & GoodCodes & ")")
            Else
                ExecuteSqlProcedure("Delete from Req_Dx where Accession_ID = " & AccID)
            End If
        End If
    End Sub

    '<<<<<<< HEAD
    '=======
    Private Function GetLabelInfo(ByVal AccID As Long, ByVal QTY As Integer) As String()
        Dim labelInfo() As String = {""}
        Dim Sources() As String = {""}
        Dim Provider As String = ""
        Dim Patient As String = ""
        Dim Tests As String = ""
        Dim AccDate As String = ""
        Dim EMRNo As String = ""
        '
        Dim cnn As New SqlConnection(connString)
        cnn.Open()
        Dim selcmd As New SqlCommand("Select a.ID as ID, a.Name as Source, " &
        "b.SourceQuantity as QTY from Sources a inner join Specimens b on a.ID = b.Source_ID " &
        "where b.Accession_ID = " & AccID, cnn)
        selcmd.CommandType = Data.CommandType.Text
        Dim DR As SqlDataReader = selcmd.ExecuteReader
        If DR.HasRows Then
            While DR.Read
                If Sources(UBound(Sources)) <> "" Then ReDim Preserve Sources(UBound(Sources) + 1)
                Sources(UBound(Sources)) = DR("ID").ToString & "^" &
                Trim(DR("Source")) & "^" & DR("QTY").ToString
            End While
        End If
        DR.Close()
        selcmd.Dispose()
        selcmd = Nothing
        cnn.Close()
        cnn = Nothing
        '
        Dim cn1 As New SqlConnection(connString)
        cn1.Open()
        Dim prcmd As New SqlCommand("Select a.OrderingProvider_ID as ClinicID, b.* from " &
        "Requisitions a inner join Providers b on a.AttendingProvider_ID = b.ID where a.ID = " & AccID, cn1)
        prcmd.CommandType = Data.CommandType.Text
        Dim pDR As SqlDataReader = prcmd.ExecuteReader
        If pDR.HasRows Then
            While pDR.Read
                If pDR("IsIndividual") IsNot DBNull.Value AndAlso pDR("IsIndividual") = 0 Then
                    Provider = pDR("ClinicID").ToString & "-" & Trim(pDR("LastName_BSN"))
                Else
                    If pDR("Degree") Is DBNull.Value OrElse pDR("Degree") = "" Then
                        Provider = pDR("ClinicID").ToString & "-" & Trim(pDR("LastName_BSN")) & ", " & Trim(pDR("FirstName"))
                    Else
                        Provider = pDR("ClinicID").ToString & "-" & Trim(pDR("LastName_BSN")) & ", " & Trim(pDR("FirstName")) &
                        " " & Trim(pDR("Degree"))
                    End If
                End If
            End While
        End If
        pDR.Close()
        prcmd.Dispose()
        prcmd = Nothing
        cn1.Close()
        cn1 = Nothing
        '
        Dim cn2 As New SqlConnection(connString)
        cn2.Open()
        Dim patcmd As New SqlCommand("Select * from Patients where ID in " &
        "(Select Patient_ID from Requisitions where ID = " & AccID & ")", cn2)
        patcmd.CommandType = Data.CommandType.Text
        Dim tDR As SqlDataReader = patcmd.ExecuteReader
        If tDR.HasRows Then
            While tDR.Read
                If tDR("MiddleName") IsNot DBNull.Value AndAlso Trim(tDR("MiddleName")) <> "" Then
                    Patient = tDR("LastName") & ", " & tDR("FirstName") & " " & tDR("MiddleName").ToString.Substring(0, 1) _
                    & "-" & Microsoft.VisualBasic.Left(tDR("Sex"), 1) & "-" & Format(tDR("DOB"), SystemConfig.DateFormat)
                Else
                    Patient = tDR("LastName") & ", " & tDR("FirstName") & "-" & tDR("Sex").ToString.Substring(0, 1) &
                    "-" & Format(tDR("DOB"), SystemConfig.DateFormat)
                End If
            End While
        End If
        tDR.Close()
        patcmd.Dispose()
        patcmd = Nothing
        cn2.Close()
        cn2 = Nothing
        '
        Dim Comps As String = ""
        Dim cn3 As New SqlConnection(connString)
        cn3.Open()
        Dim csncmd As New SqlCommand("Select (Select b.Abbr from Tests b where b.ID = a.TGP_ID " &
        "Union Select c.Abbr from Groups c where c.ID = a.TGP_ID Union Select d.Abbr from Profiles d where " &
        "d.ID = a.TGP_ID ) as TGPName from Req_TGP a where a.Accession_ID = " & AccID, cn3)
        csncmd.CommandType = Data.CommandType.Text
        Dim csnDR As SqlDataReader = csncmd.ExecuteReader
        If csnDR.HasRows Then
            While csnDR.Read
                If InStr(Comps, Trim(csnDR("TGPName"))) = 0 Then Comps += Trim(csnDR("TGPName")) & ","
            End While
            Comps = Comps.Substring(0, Len(Comps) - 1)
            Tests = Comps
        End If
        csnDR.Close()
        csncmd.Dispose()
        cn3.Close()
        cn3 = Nothing
        '
        Dim cn4 As New SqlConnection(connString)
        cn4.Open()
        Dim srccmd As New SqlCommand("Select (Select Min(SourceDate) from Specimens where " &
        "Accession_ID = " & AccID & ") as DOC, EMRNo from Requisitions where ID = " & AccID, cn4)
        srccmd.CommandType = Data.CommandType.Text
        Dim srcDR As SqlDataReader = srccmd.ExecuteReader
        If srcDR.HasRows Then
            While srcDR.Read
                AccDate = Format(srcDR("DOC"), SystemConfig.DateFormat)
                If srcDR("EMRNo") IsNot DBNull.Value AndAlso
                Trim(srcDR("EMRNo")) <> "" Then EMRNo = Trim(srcDR("EMRNo"))
            End While
        End If
        srcDR.Close()
        srccmd.Dispose()
        srccmd = Nothing
        cn4.Close()
        cn4 = Nothing
        '
        Dim SRC() As String
        Dim SUFX As String = ""
        If QTY >= Sources.Length Then
            For i As Integer = LBound(Sources) To UBound(Sources)
                SRC = Split(Sources(i), "^")
                For n As Integer = 0 To Val(SRC(2)) - 1
                    If labelInfo(UBound(labelInfo)) <> "" Then _
                    ReDim Preserve labelInfo(UBound(labelInfo) + 1)
                    labelInfo(UBound(labelInfo)) = Provider & "|" &
                    Patient & "|" & AccID.ToString & "-" & SRC(0) &
                    "|" & AccDate & "|" & SRC(1) & "|" & EMRNo
                Next
            Next
            If QTY > labelInfo.Length Then
                For n As Integer = 1 To QTY - labelInfo.Length
                    If labelInfo(UBound(labelInfo)) <> "" Then _
                    ReDim Preserve labelInfo(UBound(labelInfo) + 1)
                    labelInfo(UBound(labelInfo)) = Provider & "|" &
                    Patient & "|" & AccID.ToString & "|" &
                    AccDate & "|" & Tests & "|" & EMRNo
                Next
            End If
        Else
            For n As Integer = 1 To QTY
                If labelInfo(UBound(labelInfo)) <> "" Then _
                ReDim Preserve labelInfo(UBound(labelInfo) + 1)
                labelInfo(UBound(labelInfo)) = Provider & "|" &
                Patient & "|" & AccID.ToString & "|" &
                AccDate & "|" & Tests & "|" & EMRNo
            Next
        End If
        Return labelInfo
    End Function



    '>>>>>>> Prolis-Temur
    Private Function ConsolidateAccession(ByVal ProviderID As Long) As Boolean
        Dim Consolidate As Boolean = False
        Dim MyConnection As New SqlConnection(connString)
        If MyConnection.State = ConnectionState.Closed Then
            MyConnection.Open()
        End If
        Dim selcmd As New SqlCommand("Select AccConsolidate from Providers where ID = " & ProviderID, MyConnection)
        selcmd.CommandType = CommandType.Text
        Try
            Dim dr As SqlDataReader = selcmd.ExecuteReader()
            If dr.HasRows Then
                While dr.Read
                    Consolidate = dr.GetValue(0)
                End While
            End If
        Catch ex As Exception
            SendMail("Accession", "ConsolidateAction", ex.Message)
        Finally
            If MyConnection.State = ConnectionState.Open Then
                MyConnection.Close()
            End If
        End Try

        Return Consolidate
    End Function

    Private Function SaveRequisition() As Long
        If btnNew.Checked = False Then  'New Accession
            Dim AccID As String = ""
            Dim AccDate As Date = CDate(dtpDate.Value.ToString("MM/dd/yyyy") & " " & txtTime.Text)
            'Industrial Temur
            MergeAccs = NotIndustril(cmbSpecimenType.SelectedIndex)
            MergeAccs = SystemConfig.MergeSameDayAccession
            If MergeAccs Then AccID = GetPatientAcc(Trim(txtPatientID.Text), AccDate)
            If AccID = "" Then
                If Not IsUniqueAccession(txtAccID.Text) Then
                    txtAccID.Text = NextAccessionID(AccDate, Trim(txtPatientID.Text))
                End If
            Else
                txtAccID.Text = AccID
            End If
        End If
        'If Trim(txtRequisition.Text) = "" Then txtRequisition.Text = txtAccID.Text
        Dim ItemX As MyList
        '
        Dim cnn As New SqlConnection(connString)
        cnn.Open()
        Dim CMD As New SqlCommand("Requisitions_SP", cnn)
        CMD.CommandType = Data.CommandType.StoredProcedure
        CMD.Parameters.AddWithValue("@act", "Upsert")
        CMD.Parameters.AddWithValue("@ID", Val(txtAccID.Text))
        If Trim(txtRequisition.Text) <> "" Then
            CMD.Parameters.AddWithValue("@RequisitionNo", Trim(txtRequisition.Text))
        Else
            CMD.Parameters.AddWithValue("@RequisitionNo", txtAccID.Text)
        End If
        CMD.Parameters.AddWithValue("@EMRNo", Trim(txtEMRNo.Text))
        CMD.Parameters.AddWithValue("@Room", Trim(txtRoom.Text))
        CMD.Parameters.AddWithValue("@AccessionDate", Format(dtpDate.Value, SystemConfig.DateFormat) & " " & txtTime.Text)
        CMD.Parameters.AddWithValue("@AccessionedBy", ThisUser.ID)
        CMD.Parameters.AddWithValue("@AnalysisStage_ID", 0)
        CMD.Parameters.AddWithValue("@Comment", txtComment.Text)
        CMD.Parameters.AddWithValue("@WorkCmnt", txtWorkCmnt.Text)
        CMD.Parameters.AddWithValue("@RejectReason", (txtRejectReason.Text))
        CMD.Parameters.AddWithValue("@Rejected", chkReject.Checked)
        CMD.Parameters.AddWithValue("@ResultHistory", SystemConfig.RESHistory)
        CMD.Parameters.AddWithValue("@AccessionLoc_ID", 0)

        If btnNew.Checked = False Then  'new
            If chkPostPrePhleb.Checked = False Then 'Acc
                If IsDate(txtRecDate.Text & " " & txtRecTime.Text) Then

                    CMD.Parameters.AddWithValue("@ReceivedTime", CDate(txtRecDate.Text & " " & txtRecTime.Text))
                Else
                    CMD.Parameters.AddWithValue("@ReceivedTime", CDate(Format(dtpDate.Value,
                    SystemConfig.DateFormat) & " " & txtTime.Text))
                End If
                CMD.Parameters.AddWithValue("@Received", 1)
            Else
                CMD.Parameters.AddWithValue("@Received", 0)
            End If
        Else
            CMD.Parameters.AddWithValue("@Received", 1)
            If IsDate(txtRecDate.Text & " " & txtRecTime.Text) Then
                CMD.Parameters.AddWithValue("@ReceivedTime", CDate(txtRecDate.Text & " " & txtRecTime.Text))

            Else
                CMD.Parameters.AddWithValue("@ReceivedTime", CDate(Format(dtpDate.Value,
                SystemConfig.DateFormat) & " " & txtTime.Text))
            End If
        End If



        CMD.Parameters.AddWithValue("@OrderingProvider_ID", Val(txtOrdID.Text))
        ItemX = lstProviders.CheckedItems(0)
        CMD.Parameters.AddWithValue("@AttendingProvider_ID", ItemX.ItemData)
        CMD.Parameters.AddWithValue("@SpecimenType", cmbSpecimenType.SelectedIndex)
        If cmbSpecimenType.SelectedIndex = 0 AndAlso Val(txtPatientID.Text) > 0 Then   'Clinical
            CMD.Parameters.AddWithValue("@Patient_ID", Val(txtPatientID.Text))
            CMD.Parameters.AddWithValue("@Fasting", chkFasting.Checked)
        End If
        CMD.Parameters.AddWithValue("@IsGratis", chkSvcGratis.Checked)
        CMD.Parameters.AddWithValue("@SalesPerson_ID", GetSalesPersonID(Val(txtOrdID.Text)))
        If rbC.Checked = True Then
            CMD.Parameters.AddWithValue("@BillingType_ID", 0)
            CMD.Parameters.AddWithValue("@PrimePayer_ID", Val(txtOrdID.Text))
        ElseIf rbT.Checked = True Then
            CMD.Parameters.AddWithValue("@BillingType_ID", 1)
            If txtPInsID.Text <> "" And txtPPolicy.Text <> "" Then     'primary
                CMD.Parameters.AddWithValue("@PrimePayer_ID", Val(txtPInsID.Text))
                SaveReqPCoverage(Val(txtAccID.Text), Val(txtPInsID.Text))
                If chkWorkman.Checked = False Then
                    UpdatePatPCoverage(Val(txtPatientID.Text), Val(txtPInsID.Text),
                    Trim(txtPPolicy.Text), Trim(txtPGroup.Text), cmbPRelation.SelectedIndex,
                    IIf(cmbPRelation.SelectedIndex = 0, Trim(txtPatientID.Text),
                    Val(txtPSubID.Text)), Nothing, Nothing)
                End If
            Else
                ExecuteSqlProcedure("Delete from Req_Coverage where " &
                "Accession_ID = " & Val(txtAccID.Text) & " and Preference = 'P'")
            End If
            If txtSInsID.Text <> "" And txtSPolicy.Text <> "" Then    'Secondary
                CMD.Parameters.AddWithValue("@SecondPayer_ID", Val(txtSInsID.Text))
                SaveReqSCoverage(Val(txtAccID.Text), Val(txtSInsID.Text))

                UpdatePatSCoverage(Val(txtPatientID.Text), Val(txtSInsID.Text),
                Trim(txtSPolicy.Text), Trim(txtSGroup.Text), cmbSRelation.SelectedIndex,
                IIf(cmbSRelation.SelectedIndex = 0, Val(txtPatientID.Text), Val(txtSSubID.Text)), Nothing, Nothing)
            Else
                ExecuteSqlProcedure("Delete from Req_Coverage where " &
                "Accession_ID = " & Val(txtAccID.Text) & " and Preference = 'S'")
            End If
            'UpdateGuarantor()
        Else
            CMD.Parameters.AddWithValue("@BillingType_ID", 2)
            CMD.Parameters.AddWithValue("@PrimePayer_ID", Val(txtPatientID.Text))
            'UpdateGuarantor()
        End If
        If txtPayment.Text <> "" AndAlso PMT.Length > 6 AndAlso Val(PMT(6)) > 0 Then  '0=ID, 1=ArType, 2=ArID, 3= PType, 4= PDate, 5= DocNo, 6=AMT, 7= UnApp
            CMD.Parameters.AddWithValue("@Payment_ID", SavePayment(PMT, Val(txtAccID.Text)))
            CMD.Parameters.AddWithValue("@PaymentAmount", Val(txtPayment.Text))
            ReDim PMT(14)
            PMT(0) = ""
            txtPayment.Text = ""
        End If
        Dim DirID As String = GetDefaultDirectorID()
        If DirID <> "" Then CMD.Parameters.AddWithValue("@Director_ID", Val(DirID))
        CMD.Parameters.AddWithValue("@InHouse", Convert.ToInt16(chkInHouse.Checked))
        CMD.Parameters.AddWithValue("@InEditReason", txtInEditReason.Text)
        CMD.Parameters.AddWithValue("@Verbal", chkVerbal.CheckState)
        CMD.Parameters.AddWithValue("@Shift", 1)
        CMD.Parameters.AddWithValue("@LastEditedOn", Date.Now)
        CMD.Parameters.AddWithValue("@EditedBy", ThisUser.ID)
        If QR.Qualify = True Then
            CMD.Parameters.AddWithValue("@CreateQR", QrChk.Checked)
        End If
        CMD.ExecuteNonQuery()
        CMD.Dispose()
        cnn.Close()
        cnn = Nothing
        '
        Return Val(txtAccID.Text)
    End Function

    Private Function UpdateEmployer(ByVal EmpID As String, ByVal EmpName As String,
    ByVal Add1 As String, ByVal Add2 As String, ByVal City As String, ByVal State _
    As String, ByVal Zip As String, ByVal Country As String) As Long
        If EmpID = "" Then EmpID = GetNextEmployerID()
        Dim AddressID As Long = -1
        If Add1 <> "" And City <> "" And State <> "" And Zip <> "" Then _
        AddressID = GetAddressID(Add1, Add2, City, State, Zip, Country)
        ExecuteSqlProcedure("If not Exists (Select * from Employers where ID = " & EmpID &
        " and Employer = '" & EmpName & "') Insert into Employers (ID, Employer, Contact, " &
        "Phone, Fax, Address_ID, Note) values (" & EmpID & ", '" & EmpName & "', '', '" &
        PhoneNeat(txtGPhone.Text) & "', '', " & AddressID & ", '')")
        Return EmpID
    End Function

    Private Function GetNextEmployerID() As Long
        Dim NID As Long = 1
        Dim cnnid As New SqlConnection(connString)
        cnnid.Open()
        Dim cmdnid As New SqlCommand("Select Max(ID) as lastID from Employers", cnnid)
        cmdnid.CommandType = Data.CommandType.Text
        Dim drnid As SqlDataReader = cmdnid.ExecuteReader
        If drnid.HasRows Then
            While drnid.Read
                If drnid("LastID") IsNot DBNull.Value _
                Then NID = drnid("LastID") + 1
            End While
        End If
        cnnid.Close()
        cnnid = Nothing
        Return NID
    End Function

    Private Sub UpdateGuarantor()
        If chkGIsIndividual.Checked = True And Trim(txtGLName_BSN.Text) <> "" And
            Trim(txtGFName.Text) <> "" And cmbGSex.SelectedIndex <> -1 And IsDate(txtGDOB.Text) _
            And cmbGRelation.SelectedIndex <> -1 And Trim(txtGAdd1.Text) <> "" And
            Trim(txtGCity.Text) <> "" And Trim(txtGState.Text) <> "" And Trim(txtGZip.Text) <> "" Then
            Dim ItemS As MyList
            Dim ItemB As MyList
            If SystemConfig.DiagTarget = "V" Then
                If cmbSpecies.SelectedIndex <> -1 Then
                    ItemS = cmbSpecies.SelectedItem
                Else
                    ItemS = New MyList("Human", 0)
                End If
                If cmbBreed.SelectedIndex <> -1 Then
                    ItemB = cmbBreed.SelectedItem
                Else
                    ItemB = New MyList("Human", 0)
                End If
            Else
                ItemS = New MyList("Human", 0)
                ItemB = New MyList("Human", 0)
            End If
            txtGID.Text = UpdatePatient("", Trim(txtGLName_BSN.Text), Trim(txtGFName.Text), Trim(txtGMName.Text),
            cmbGSex.SelectedItem.ToString.Substring(0, 1), Trim(txtGTage.Text), CDate(txtGDOB.Text), ItemS.ItemData,
            ItemB.ItemData, Trim(txtSSN.Text), Trim(txtGEmail.Text), PhoneNeat(txtGPhone.Text), "", "", "", 5, 2,
            txtGAdd1.Text, txtGAdd2.Text, txtGCity.Text, txtGState.Text, txtGZip.Text, txtGCountry.Text)
        ElseIf chkGIsIndividual.Checked = False And Trim(txtGLName_BSN.Text) <> "" And
        cmbGRelation.SelectedIndex <> -1 And Trim(txtGAdd1.Text) <> "" And
        Trim(txtGCity.Text) <> "" And Trim(txtGState.Text) <> "" And Trim(txtGZip.Text) <> "" Then
            txtGID.Text = UpdateEmployer("", Trim(txtGLName_BSN.Text), Trim(txtGAdd1.Text),
            Trim(txtGAdd2.Text), Trim(txtGCity.Text), Trim(txtGState.Text), Trim(txtGZip.Text),
            Trim(txtGCountry.Text))
        End If
        '
        If txtGID.Text <> "" Then   'existing
            ExecuteSqlProcedure("Delete from Req_Guarantor where Accession_ID = " & Val(txtAccID.Text))
            ExecuteSqlProcedure("If Exists (Select * from Req_Guarantor where Accession_ID = " & Val(txtAccID.Text) _
            & " and Guarantor_ID = " & Val(txtGID.Text) & ") Update Req_Guarantor set Accession_ID = " &
            Val(txtAccID.Text) & ", Guarantor_ID = " & Val(txtGID.Text) & ", GuarantorEntity = " &
            Convert.ToInt16(chkGIsIndividual.Checked) & ", Relation = " & cmbGRelation.SelectedIndex &
            " where Accession_ID = " & Val(txtAccID.Text) & " and Guarantor_ID = " & Val(txtGID.Text) & " Else " &
            "Insert into Req_Guarantor (Accession_ID, Guarantor_ID, GuarantorEntity, Relation) values (" &
            Val(txtAccID.Text) & ", " & Val(txtGID.Text) & ", " & Convert.ToInt16(chkGIsIndividual.Checked) &
            ", " & cmbGRelation.SelectedIndex & ")")
            If txtPatientID.Text <> "" Then
                ExecuteSqlProcedure("Delete from Guarantors where Patient_ID = " & Val(txtPatientID.Text))
                ExecuteSqlProcedure("If Exists (Select * from Guarantors where Patient_ID = " & Val(txtPatientID.Text) _
                & " And Guarantor_ID = " & Val(txtGID.Text) & ") Update Guarantors set Patient_ID = " & Val(txtPatientID.Text) &
                ", Guarantor_ID = " & Val(txtGID.Text) & ", GuarantorEntity = " & Convert.ToInt16(chkGIsIndividual.Checked) &
                ", Relation = " & cmbGRelation.SelectedIndex & " where Patient_ID = " & Val(txtPatientID.Text) & " and " &
                "Guarantor_ID = " & Val(txtGID.Text) & " Else Insert into Guarantors (Patient_ID, Guarantor_ID, GuarantorEntity, " &
                "Relation) values (" & Val(txtPatientID.Text) & ", " & Val(txtGID.Text) & ", " &
                Convert.ToInt16(chkGIsIndividual.Checked) & ", " & cmbGRelation.SelectedIndex & ")")
            End If
        End If
    End Sub

    Private Sub SaveReqPCoverage(ByVal AccID As Long, ByVal PayerID As Long)
        Dim cnrp As New SqlConnection(connString)
        cnrp.Open()
        '@Accession_ID bigint = null, @Payer_ID bigint = null, @Ordinal int = 0, @Insured_ID bigint = null, @Preference nchar(1) = '', @GroupNo nvarchar(25) = null, @PolicyNo nvarchar(25) = null, @Relation tinyint = 0, @CoPayment real = null, @WorkmanComp bit = 0, @InstanceDate smalldatetime = null, @Comment nvarchar(400) = null, @act varchar(10)
        Dim cmdupsert As New SqlCommand("Req_Coverage_SP", cnrp)
        cmdupsert.CommandType = Data.CommandType.StoredProcedure
        cmdupsert.Parameters.AddWithValue("@act", "Upsert")
        cmdupsert.Parameters.AddWithValue("@Accession_ID", AccID)
        cmdupsert.Parameters.AddWithValue("@Payer_ID", PayerID)
        cmdupsert.Parameters.AddWithValue("@Ordinal", 0)
        If cmbPRelation.SelectedIndex = 0 Then
            cmdupsert.Parameters.AddWithValue("@Insured_ID", Val(txtPatientID.Text))
        Else
            If Trim(txtPSubID.Text) <> "" Then UpdatePInsured()
            cmdupsert.Parameters.AddWithValue("@Insured_ID", Val(txtPSubID.Text))
        End If
        cmdupsert.Parameters.AddWithValue("@Preference", "P")
        cmdupsert.Parameters.AddWithValue("@GroupNo", Trim(txtPGroup.Text))
        cmdupsert.Parameters.AddWithValue("@PolicyNo", Trim(txtPPolicy.Text))
        cmdupsert.Parameters.AddWithValue("@Relation", cmbPRelation.SelectedIndex)
        cmdupsert.Parameters.AddWithValue("@CoPayment", Val(txtPCopay.Text))
        cmdupsert.Parameters.AddWithValue("@workmancomp", chkWorkman.Checked)
        If IsDate(txtDOI.Text) Then
            cmdupsert.Parameters.AddWithValue("@InstanceDate", CDate(txtDOI.Text))
        Else
            cmdupsert.Parameters.AddWithValue("@InstanceDate", Nothing)
        End If
        cmdupsert.Parameters.AddWithValue("@comment", txtCovCmnt.Text)
        Try
            cmdupsert.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        cmdupsert.Dispose()
        cnrp.Close()
        cnrp = Nothing
        '
        ExecuteSqlProcedure("Delete from Req_Coverage where " &
        "Accession_ID = " & AccID & " and Preference = 'P' and Payer_ID <> " & PayerID)
    End Sub

    Private Sub SaveReqSCoverage(ByVal AccID As Long, ByVal PayerID As Long)
        Dim cnrs As New SqlConnection(connString)
        cnrs.Open()
        Dim cmdupsert As New SqlCommand("Req_Coverage_SP", cnrs)
        cmdupsert.CommandType = Data.CommandType.StoredProcedure
        cmdupsert.Parameters.AddWithValue("@act", "Upsert")
        cmdupsert.Parameters.AddWithValue("@Accession_ID", AccID)
        cmdupsert.Parameters.AddWithValue("@Payer_ID", PayerID)
        cmdupsert.Parameters.AddWithValue("@Ordinal", 0)
        If cmbSRelation.SelectedIndex = 0 Then
            cmdupsert.Parameters.AddWithValue("@Insured_ID", Val(txtPatientID.Text))
        Else
            If Trim(txtSSubID.Text) <> "" Then UpdateSInsured()
            cmdupsert.Parameters.AddWithValue("@Insured_ID", Val(txtSSubID.Text))
        End If
        cmdupsert.Parameters.AddWithValue("@Preference", "S")
        cmdupsert.Parameters.AddWithValue("@GroupNo", Trim(txtSGroup.Text))
        cmdupsert.Parameters.AddWithValue("@PolicyNo", Trim(txtSPolicy.Text))
        cmdupsert.Parameters.AddWithValue("@Relation", cmbSRelation.SelectedIndex)
        cmdupsert.Parameters.AddWithValue("@CoPayment", Val(txtSCopay.Text))
        cmdupsert.Parameters.AddWithValue("@workmancomp", False)
        cmdupsert.Parameters.AddWithValue("@InstanceDate", Nothing)
        cmdupsert.Parameters.AddWithValue("@comment", "")
        cmdupsert.ExecuteNonQuery()
        cmdupsert.Dispose()
        cmdupsert = Nothing
        cnrs.Close()
        cnrs = Nothing
        '
        ExecuteSqlProcedure("Delete from Req_Coverage where " &
        "Accession_ID = " & AccID & " and Preference = 'S' and Payer_ID <> " & PayerID)
        '
    End Sub


    Private Sub SaveSpecimen(ByVal AccID As Long)
        Dim SIDS As String = ""
        For i As Integer = 0 To dgvSources.RowCount - 1
            Dim cnsp As New SqlConnection(connString)
            cnsp.Open()
            Dim cmdupsert As New SqlCommand("Specimens_SP", cnsp)
            cmdupsert.CommandType = Data.CommandType.StoredProcedure
            cmdupsert.Parameters.AddWithValue("@act", "Upsert")
            cmdupsert.Parameters.AddWithValue("@Accession_ID", AccID)
            cmdupsert.Parameters.AddWithValue("@Source_ID", dgvSources.Rows(i).Cells(0).Value)
            cmdupsert.Parameters.AddWithValue("@SourceNo", AccID.ToString & "-" & (i + 1).ToString)
            cmdupsert.Parameters.AddWithValue("@SourceQuantity", dgvSources.Rows(i).Cells(3).Value)
            cmdupsert.Parameters.AddWithValue("@SourceDate", CDate(dgvSources.Rows(i).Cells(4).Value _
            & " " & dgvSources.Rows(i).Cells(5).Value))
            cmdupsert.Parameters.AddWithValue("@SourceTemp", dgvSources.Rows(i).Cells(6).Value)
            cmdupsert.Parameters.AddWithValue("@IsReadyToUse", 1)
            cmdupsert.Parameters.AddWithValue("@Comment", dgvSources.Rows(i).Cells(7).Value)
            cmdupsert.ExecuteNonQuery()
            cmdupsert.Dispose()
            cmdupsert = Nothing
            SIDS += dgvSources.Rows(i).Cells(0).Value & ", "
            cnsp.Close()
            cnsp = Nothing
        Next
        If SIDS.EndsWith(", ") Then SIDS = Microsoft.VisualBasic.Mid(SIDS, 1, Len(SIDS) - 2)
        If btnNew.Checked = True OrElse MergeAccs = False Then  'edit or no merge
            If SIDS <> "" Then
                ExecuteSqlProcedure("Delete from Specimens where Accession_ID = " &
                AccID & " and not Source_ID in (" & SIDS & ")")
            End If
        End If
    End Sub

    Private Function GetGoodTGPs(ByVal AccID As Long) As String
        Dim GoodTGPs As String = ""
        Dim cnnid As New SqlConnection(connString)
        cnnid.Open()
        Dim cmdnid As New SqlCommand("Select * from Req_TGP where Accession_ID = " & AccID, cnnid)
        cmdnid.CommandType = Data.CommandType.Text
        Dim drnid As SqlDataReader = cmdnid.ExecuteReader
        If drnid.HasRows Then
            While drnid.Read
                GoodTGPs += drnid("TGP_ID").ToString & ", "
            End While
        End If
        cnnid.Close()
        cnnid = Nothing
        Return GoodTGPs
    End Function

    Private Function BreakProfile(ByVal ProfileID As Integer) As String()
        Dim TGPS() As String = {""}
        Dim sSQL As String = "Select GrpTst_ID as TGPID from Prof_GrpTst where Profile_ID = " & ProfileID & " order by Ordinal"
        '
        Dim cnbp As New SqlConnection(connString)
        cnbp.Open()
        Dim cmdbp As New SqlCommand(sSQL, cnbp)
        cmdbp.CommandType = Data.CommandType.Text
        Dim drbp As SqlDataReader = cmdbp.ExecuteReader
        If drbp.HasRows Then
            While drbp.Read
                If TGPS(UBound(TGPS)) <> "" Then ReDim Preserve TGPS(UBound(TGPS) + 1)
                TGPS(UBound(TGPS)) = drbp("TGPID").ToString
            End While
        End If
        cnbp.Close()
        cnbp = Nothing
        Return TGPS
    End Function

    Private Sub SaveReqTGP(ByVal AccID As Long)
        Dim MyTGPs(1, 0) As String
        Dim TmpTGPS() As String = {""}
        Dim GoodTGPs As String = ""
        Dim TGPType As String = ""
        Dim Stat As Boolean = False
        For i As Integer = 0 To dgvTGPMarked.RowCount - 1
            If dgvTGPMarked.Rows(i).Cells(2).Value IsNot Nothing _
            AndAlso Trim(dgvTGPMarked.Rows(i).Cells(2).Value) <> "" Then
                If dgvTGPMarked.Rows(i).Cells(4).Value Is Nothing Then
                    Stat = False
                Else
                    Stat = dgvTGPMarked.Rows(i).Cells(4).Value
                End If
                If chkProfile.Checked = False Then  'Integral
                    If Not TGPinTGPS(dgvTGPMarked.Rows(i).Cells(0).Value.ToString, MyTGPs) _
                    Then MyTGPs = AddTGPinTGPS(dgvTGPMarked.Rows(i).Cells(0).Value.ToString, Stat, MyTGPs)
                Else    'break
                    If GetTGPType(dgvTGPMarked.Rows(i).Cells(0).Value) = "P" Then
                        TmpTGPS = BreakProfile(dgvTGPMarked.Rows(i).Cells(0).Value)
                        For n As Integer = 0 To TmpTGPS.Length - 1
                            If Not TGPinTGPS(TmpTGPS(n), MyTGPs) _
                            Then MyTGPs = AddTGPinTGPS(TmpTGPS(n), Stat, MyTGPs)
                        Next
                    Else
                        If Not TGPinTGPS(dgvTGPMarked.Rows(i).Cells(0).Value.ToString, MyTGPs) _
                        Then MyTGPs = AddTGPinTGPS(dgvTGPMarked.Rows(i).Cells(0).Value.ToString, Stat, MyTGPs)
                    End If
                End If
            End If
        Next
        MyTGPs = RemoveDuplicates2(MyTGPs)
        '
        For t As Integer = 0 To UBound(MyTGPs, 2)
            If Trim(MyTGPs(0, t)) <> "" AndAlso IsNumeric(MyTGPs(0, t)) Then
                TGPType = GetTGPType(MyTGPs(0, t))
                Dim cnrtgp As New SqlConnection(connString)
                cnrtgp.Open()
                '@Accession_ID bigint = null, @TGP_ID int = null, @TGP_Type char(1) = null, @Billed bit = 0, 
                '@Ordinal int = 0, @IsStat bit = 0, @Verbal bit = 0, @Skip_Billing bit = 0, @Dx_Code nvarchar(12) = '', @IsESRD bit = 0, @act varchar(10)
                Dim cmdupsert As New SqlCommand("Req_TGP_SP", cnrtgp)
                cmdupsert.CommandType = Data.CommandType.StoredProcedure
                cmdupsert.Parameters.AddWithValue("@act", "Upsert")
                cmdupsert.Parameters.AddWithValue("@Accession_ID", AccID)
                cmdupsert.Parameters.AddWithValue("@TGP_ID", MyTGPs(0, t))
                cmdupsert.Parameters.AddWithValue("@TGP_Type", TGPType)
                cmdupsert.Parameters.AddWithValue("@Billed", 0)
                cmdupsert.Parameters.AddWithValue("@Ordinal", t)
                If MyTGPs(1, t) = "" Then MyTGPs(1, t) = "0"
                cmdupsert.Parameters.AddWithValue("@IsStat", CType(MyTGPs(1, t), Boolean))
                cmdupsert.Parameters.AddWithValue("@Verbal", False)
                cmdupsert.Parameters.AddWithValue("@Skip_Billing", False)
                cmdupsert.Parameters.AddWithValue("@Dx_Code", "")
                cmdupsert.Parameters.AddWithValue("@IsESRD", False)
                cmdupsert.ExecuteNonQuery()
                cmdupsert.Dispose()
                GoodTGPs += MyTGPs(0, t) & ", "
                cnrtgp.Close()
                cnrtgp = Nothing
            End If
        Next
        '
        If GoodTGPs.EndsWith(", ") Then GoodTGPs = Microsoft.VisualBasic.Mid(GoodTGPs, 1, Len(GoodTGPs) - 2)
        If btnNew.Checked = True OrElse MergeAccs = False Then  'edit or no merge
            If GoodTGPs <> "" Then
                ExecuteSqlProcedure("Delete from Req_TGP where " &
                "Accession_ID = " & AccID & " and Not TGP_ID in (" & GoodTGPs & ")")
            End If
        End If
    End Sub

    Private Function TGPinReq_TGP(ByVal AccID As Long, ByVal TGPID As Integer) As Boolean
        Dim TGPIn As Boolean = False
        Dim cnin As New SqlConnection(connString)
        cnin.Open()
        Dim cmdin As New SqlCommand("Select * from " &
        "Req_TGP where Accession_ID = " & AccID & " and TGP_ID = " & TGPID, cnin)
        cmdin.CommandType = Data.CommandType.Text
        Dim drin As SqlDataReader = cmdin.ExecuteReader
        If drin.HasRows Then TGPIn = True
        cnin.Close()
        cnin = Nothing
        Return TGPIn
    End Function

    Private Function TGPOutsourced(ByVal AccID As Long, ByVal TGPID As Integer) As Boolean
        Dim TGPOuted As Boolean = False
        Dim cnin As New SqlConnection(connString)
        cnin.Open()
        Dim cmdin As New SqlCommand("Select a.* from " &
        "Sendout_TGP a inner join Sendouts b on b.ID = a.Sendout_ID " &
        "where a.TGP_ID = " & TGPID & " and b.Accession_ID = " & AccID, cnin)
        cmdin.CommandType = Data.CommandType.Text
        Dim drin As SqlDataReader = cmdin.ExecuteReader
        If drin.HasRows Then TGPOuted = True
        cnin.Close()
        cnin = Nothing
        Return TGPOuted
    End Function

    Private Sub SaveReqTests(ByVal AccID As Long)
        Dim GoodTests As String = ""
        Dim NormalRange As String = ""
        Dim TestIDs() As String = GetTestIDsfromReqTGPS(AccID)
        '
        For i As Integer = 0 To TestIDs.Length - 1
            If TestIDs(i) <> "" Then
                NormalRange = GetNormalRange(AccID, TestIDs(i))
                Dim cnrt As New SqlConnection(connString)
                cnrt.Open()
                Dim cmdrt As New SqlCommand("Acc_Results_SP", cnrt)
                cmdrt.CommandType = Data.CommandType.StoredProcedure
                cmdrt.Parameters.AddWithValue("@ACT", "Upsert")
                cmdrt.Parameters.AddWithValue("@Accession_ID", AccID)
                cmdrt.Parameters.AddWithValue("@Test_ID", TestIDs(i))
                cmdrt.Parameters.AddWithValue("@Ordinal", i)
                cmdrt.Parameters.AddWithValue("@NormalRange", NormalRange)
                cmdrt.ExecuteNonQuery()
                cnrt.Close()
                cnrt = Nothing
                '
                UpdateInfoResults(AccID, TestIDs(i))
                'UpdateReqInfoResponse(AccID, TestIDs(i))
                GoodTests += TestIDs(i) & ", "
            End If
        Next
        If GoodTests.EndsWith(", ") Then GoodTests =
        Microsoft.VisualBasic.Mid(GoodTests, 1, Len(GoodTests) - 2)
        '
        If btnNew.Checked = True OrElse MergeAccs = False Then  'edit or no merge
            If GoodTests <> "" Then
                ExecuteSqlProcedure("Delete from Acc_Results where " &
                "Accession_ID = " & AccID & " and not Test_ID in (" & GoodTests & ")")
                ExecuteSqlProcedure("Delete from Acc_Info_Results where " &
                "Accession_ID = " & AccID & " and not Test_ID in (" & GoodTests & ")")
                ExecuteSqlProcedure("Delete from Req_Info_Response " &
                "where Accession_ID = " & AccID & " and not TGP_ID in (" & GoodTests & ")")
            End If
        End If
    End Sub

    Private Sub UpdatePreAnaResponse_OLD(ByVal AccID As Long, ByVal ReqInfoResp As String)
        Dim ReqInfos() As String = Split(ReqInfoResp, "|")
        Dim FB() As String = {"", ""}
        For i As Integer = 0 To ReqInfos.Length - 1
            Dim Infos() As String = Split(ReqInfos(i), "^")
            If Infos.Length = 3 Then
                '
                Dim cninf As New SqlConnection(connString)
                cninf.Open()
                Dim cmdinf As New SqlCommand("Req_Info_Response_SP", cninf)
                cmdinf.CommandType = Data.CommandType.StoredProcedure
                cmdinf.Parameters.AddWithValue("@act", "Upsert")
                cmdinf.Parameters.AddWithValue("@Accession_ID", AccID)
                cmdinf.Parameters.AddWithValue("@TGP_ID", Val(Infos(0)))
                cmdinf.Parameters.AddWithValue("@Info_ID", Val(Infos(1)))
                cmdinf.Parameters.AddWithValue("@Ordinal", i)
                cmdinf.Parameters.AddWithValue("@Response", Infos(2))
                FB = GetFlag(AccID, Infos(2), Infos(1))
                cmdinf.Parameters.AddWithValue("@Flag", FB(0))
                cmdinf.Parameters.AddWithValue("@Behavior", FB(1))
                cmdinf.Parameters.AddWithValue("@UOM", "")
                cmdinf.Parameters.AddWithValue("@LastEdited_On", Date.Now)
                cmdinf.Parameters.AddWithValue("@LastEdited_By", ThisUser.ID)
                cmdinf.ExecuteNonQuery()
                cmdinf.Dispose()
                cninf.Close()
                cninf = Nothing
            End If
        Next
    End Sub

    Private Sub UpdatePreAnaResponse(ByVal AccID As Long, ByVal ReqInfoRespList As List(Of String))

        Dim FB() As String = {"", ""}
        For i As Integer = 0 To ReqInfoRespList.Count - 1
            'Dim Infos() As String = Split(ReqInfos(i), "^")

            Dim Infos() As String = Split(ReqInfoRespList.Item(i), "|")
            If Infos.Length = 3 Then
                '
                Dim cninf As New SqlConnection(connString)
                cninf.Open()
                Dim cmdinf As New SqlCommand("Req_Info_Response_SP", cninf)
                cmdinf.CommandType = Data.CommandType.StoredProcedure
                cmdinf.Parameters.AddWithValue("@act", "Upsert")
                cmdinf.Parameters.AddWithValue("@Accession_ID", AccID)
                cmdinf.Parameters.AddWithValue("@TGP_ID", Val(Infos(0)))
                cmdinf.Parameters.AddWithValue("@Info_ID", Val(Infos(1)))
                cmdinf.Parameters.AddWithValue("@Ordinal", i)
                cmdinf.Parameters.AddWithValue("@Response", Infos(2))
                FB = GetFlag(AccID, Infos(2), Infos(1))
                cmdinf.Parameters.AddWithValue("@Flag", FB(0))
                cmdinf.Parameters.AddWithValue("@Behavior", FB(1))
                cmdinf.Parameters.AddWithValue("@UOM", "")
                cmdinf.Parameters.AddWithValue("@LastEdited_On", Date.Now)
                cmdinf.Parameters.AddWithValue("@LastEdited_By", ThisUser.ID)
                cmdinf.ExecuteNonQuery()
                cmdinf.Dispose()
                cninf.Close()
                cninf = Nothing
            End If
        Next
    End Sub

    Private Sub UpdateReqInfoResponse(ByVal AccID As Long, ByVal TestID As Integer)
        Dim Infos() As String = {""}
        Dim InfoTests As String = ""
        Dim sSQL As String = "Select a.* from Tests a inner join TGP_Info b on a.ID = b.Info_ID where a.IsActive " &
        "<> 0 and a.HasResult <> 0 and a.PreAnalytical <> 0 and b.TGP_ID = " & TestID & " order by Ordinal"
        '
        Dim cnr As New SqlConnection(connString)
        cnr.Open()
        Dim selcmd As New SqlCommand(sSQL, cnr)
        selcmd.CommandType = Data.CommandType.Text
        Dim selDA As SqlDataReader = selcmd.ExecuteReader
        If selDA.HasRows Then
            While selDA.Read
                If Infos(UBound(Infos)) <> "" Then ReDim Preserve Infos(UBound(Infos) + 1)
                Infos(UBound(Infos)) = selDA("ID").ToString
            End While
        End If
        selcmd.Dispose()
        selDA.Close()
        cnr.Close()
        cnr = Nothing
        '
        For i As Integer = 0 To Infos.Length - 1
            Dim cnf As New SqlConnection(connString)
            cnf.Open()
            Dim cmdupsert As New SqlCommand("Req_Info_Response_SP", cnf)
            cmdupsert.CommandType = Data.CommandType.StoredProcedure
            cmdupsert.Parameters.AddWithValue("@act", "Upsert")
            cmdupsert.Parameters.AddWithValue("@Accession_ID", AccID)
            cmdupsert.Parameters.AddWithValue("@TGP_ID", TestID)
            cmdupsert.Parameters.AddWithValue("@Info_ID", Infos(i))
            cmdupsert.Parameters.AddWithValue("@Ordinal", i)
            cmdupsert.Parameters.AddWithValue("@Response", "")
            cmdupsert.Parameters.AddWithValue("@Flag", "")
            cmdupsert.Parameters.AddWithValue("@UOM", "")
            cmdupsert.Parameters.AddWithValue("@LastEdited_On", Date.Now)
            cmdupsert.Parameters.AddWithValue("@LastEdited_By", ThisUser.ID)
            cmdupsert.ExecuteNonQuery()
            cmdupsert.Dispose()
            InfoTests += Infos(i).ToString & ", "
            cnf.Close()
            cnf = Nothing
        Next
        If InfoTests.EndsWith(", ") Then InfoTests = Microsoft.VisualBasic.Mid(InfoTests, 1, Len(InfoTests) - 2)
        If InfoTests <> "" Then
            ExecuteSqlProcedure("Delete from Req_Info_Response where Accession_ID = " &
            AccID & " and TGP_ID = " & TestID & " and Not Info_ID in (" & InfoTests & ")")
        Else
            ExecuteSqlProcedure("Delete from Req_Info_Response where TGP_ID = " &
            TestID & " and Accession_ID = " & AccID)
        End If
    End Sub

    Private Sub UpdateReqInfo(ByVal AccID As Long, ByVal TGPIDs As String)
        Dim HasInfo As Boolean = False
        Dim InfoTGPs As String = ""
        Dim TGPInfos As String = ""
        Dim cnri As New SqlConnection(connString)
        cnri.Open()
        Dim cmdri As New SqlCommand("Select ID as TGP_ID " &
        "from Tests where PreAnalytical <> 0 And ID in (" & TGPIDs & ") Union " &
        "Select TGP_ID from TGP_Info where TGP_ID in (" & TGPIDs & ")", cnri)
        cmdri.CommandType = Data.CommandType.Text
        Dim drri As SqlDataReader = cmdri.ExecuteReader
        If drri.HasRows Then HasInfo = True
        cnri.Close()
        cnri = Nothing
        If HasInfo Then 'Save the Info
            Dim TestIDs() As String = Split(TGPIDs, ",")
            For i As Integer = 0 To TestIDs.Length - 1
                If Trim(TestIDs(i)) <> "" Then
                    Dim cninf As New SqlConnection(connString)
                    cninf.Open()
                    Dim cmdinf As New SqlCommand("Select * from TGP_Info where Info_ID in (Select ID " &
                    "from Tests where PreAnalytical <> 0) And TGP_ID = " & Trim(TestIDs(i)) & " order by Ordinal", cninf)
                    cmdri.CommandType = Data.CommandType.Text
                    Dim drinf As SqlDataReader = cmdinf.ExecuteReader
                    If drinf.HasRows Then
                        While drinf.Read
                            ExecuteSqlProcedure("If Exists (Select * from Req_Info_Response where Accession_ID = " &
                            AccID & " And TGP_ID = " & drinf("TGP_ID") & " And Info_ID = " & drinf("Info_ID") &
                            ") Update Req_Info_Response Set Accession_ID = " & AccID & ", TGP_ID = " & drinf("TGP_ID") &
                            ", Info_ID = " & drinf("Info_ID") & ", Ordinal = " & i & " where  Accession_ID = " &
                            AccID & " And TGP_ID = " & drinf("TGP_ID") & " And Info_ID = " & drinf("Info_ID") &
                            " Else Insert into Req_Info_Response (Accession_ID, TGP_ID, Info_ID, Ordinal, Response) " &
                            "Values (" & AccID & ", " & drinf("TGP_ID") & ", " & drinf("Info_ID") & ", " & i & ", '')")
                            '
                            If InStr(InfoTGPs, drinf("TGP_ID").ToString) = 0 Then InfoTGPs += drinf("TGP_ID").ToString & ", "
                            If InStr(TGPInfos, drinf("Info_ID").ToString) = 0 Then TGPInfos += drinf("Info_ID").ToString & ", "
                        End While
                    End If
                    cninf.Close()
                    cninf = Nothing
                End If
            Next
            If InfoTGPs.EndsWith(", ") Then InfoTGPs = InfoTGPs.Substring(0, Len(InfoTGPs) - 2)
            If TGPInfos.EndsWith(", ") Then TGPInfos = TGPInfos.Substring(0, Len(TGPInfos) - 2)
            If InfoTGPs <> "" Then ExecuteSqlProcedure("Delete from Req_Info_Response " &
            "where Accession_ID = " & AccID & " And Not TGP_ID In (" & InfoTGPs & ")")
            If TGPInfos <> "" Then ExecuteSqlProcedure("Delete from Req_Info_Response " &
            "where Accession_ID = " & AccID & " And Not Info_ID In (" & TGPInfos & ")")
        End If
    End Sub

    Private Function IsResultable(ByVal TestID As Integer) As Boolean
        Dim Resultable As Boolean = True
        Dim sSQL As String = "Select HasResult from Tests where ID = " & TestID
        '
        Dim cnir As New SqlConnection(connString)
        cnir.Open()
        Dim cmdir As New SqlCommand(sSQL, cnir)
        cmdir.CommandType = Data.CommandType.Text
        Dim drir As SqlDataReader = cmdir.ExecuteReader
        If drir.HasRows Then
            While drir.Read
                If drir("HasResult") IsNot DBNull.Value Then
                    Resultable = drir("HasResult")
                Else
                    Resultable = False
                End If
            End While
        End If
        cnir.Close()
        cnir = Nothing
        '
        Return Resultable
    End Function

    Private Function GetToMarks(ByVal TestID As Integer) As String()
        Dim ToMarks() As String = {""}
        Dim Tests As String = GetOprands(TestID)
        If InStr(Tests, ",") > 0 Then
            ToMarks = Split(Tests, ",")
            ReDim Preserve ToMarks(UBound(ToMarks) + 1)
            ToMarks(UBound(ToMarks)) = CStr(TestID)
        Else
            If Tests <> "" Then 'Has one test
                ToMarks(0) = Tests
                ReDim Preserve ToMarks(UBound(ToMarks) + 1)
                ToMarks(UBound(ToMarks)) = CStr(TestID)
            Else
                ToMarks(0) = CStr(TestID)
            End If
        End If
        Return ToMarks
    End Function

    Private Function InAccResults(ByVal AccID As Long, ByVal TestID As Integer) As Boolean
        Dim IsIn As Boolean = False
        Dim sSQL As String = "Select * from Acc_Results where Accession_ID = " & AccID & " and Test_ID = " & TestID
        '
        Dim cnii As New SqlConnection(connString)
        cnii.Open()
        Dim cmdii As New SqlCommand(sSQL, cnii)
        cmdii.CommandType = Data.CommandType.Text
        Dim drii As SqlDataReader = cmdii.ExecuteReader
        If drii.HasRows Then IsIn = True
        cnii.Close()
        cnii = Nothing
        '
        Return IsIn
    End Function

    Private Sub SaveReqReports(ByVal AccID As Long)
        Dim ProvIDs As String = ""
        '
        For i As Integer = 0 To dgvRptProviders.RowCount - 1
            Dim cnp As New SqlConnection(connString)
            cnp.Open()
            Dim cmdupsert As New SqlCommand("Req_RPT_SP", cnp)
            cmdupsert.CommandType = Data.CommandType.StoredProcedure
            cmdupsert.Parameters.AddWithValue("@act", "Upsert")
            cmdupsert.Parameters.AddWithValue("@Provider_ID", dgvRptProviders.Rows(i).Cells(0).Value)
            cmdupsert.Parameters.AddWithValue("@Base_ID", AccID)
            cmdupsert.Parameters.AddWithValue("@EntrySource", "Accession")
            cmdupsert.Parameters.AddWithValue("@RPT_Type", "ACC")
            cmdupsert.Parameters.AddWithValue("@EntryDate",
            CDate(Format(dtpDate.Value, SystemConfig.DateFormat) & " " & txtTime.Text))
            cmdupsert.Parameters.AddWithValue("@Ordinal", i)
            cmdupsert.Parameters.AddWithValue("@RDM_Auto", dgvRptProviders.Rows(i).Cells(2).Value)
            cmdupsert.Parameters.AddWithValue("@RPT_Complete", dgvRptProviders.Rows(i).Cells(3).Value)
            cmdupsert.Parameters.AddWithValue("@RPT_Print", dgvRptProviders.Rows(i).Cells(4).Value)
            cmdupsert.Parameters.AddWithValue("@RPT_Prolison", dgvRptProviders.Rows(i).Cells(5).Value)
            cmdupsert.Parameters.AddWithValue("@RPT_Interface", dgvRptProviders.Rows(i).Cells(6).Value)
            cmdupsert.Parameters.AddWithValue("@RPT_Fax", dgvRptProviders.Rows(i).Cells(7).Value)
            If dgvRptProviders.Rows(i).Cells(7).Value = True AndAlso
            (dgvRptProviders.Rows(i).Cells(8).Value Is Nothing OrElse
            Trim(dgvRptProviders.Rows(i).Cells(8).Value.ToString) = "") Then
                cmdupsert.Parameters.AddWithValue("@RPT_Fax", 0)
            End If
            cmdupsert.Parameters.AddWithValue("@Fax", dgvRptProviders.Rows(i).Cells(8).Value)
            cmdupsert.Parameters.AddWithValue("@Priority", 1)
            cmdupsert.Parameters.AddWithValue("@RPT_Email", dgvRptProviders.Rows(i).Cells(9).Value)
            If dgvRptProviders.Rows(i).Cells(9).Value = True AndAlso
            (dgvRptProviders.Rows(i).Cells(10).Value Is Nothing OrElse
             Trim(dgvRptProviders.Rows(i).Cells(10).Value.ToString) = "") Then
                cmdupsert.Parameters.AddWithValue("@RPT_Email", 0)
            End If
            cmdupsert.Parameters.AddWithValue("@Email", dgvRptProviders.Rows(i).Cells(10).Value)
            cmdupsert.Parameters.AddWithValue("@Executed", 0)
            cmdupsert.Parameters.AddWithValue("@Executor", DBNull.Value)
            cmdupsert.Parameters.AddWithValue("@ExecutedOn", DBNull.Value)
            cmdupsert.Parameters.AddWithValue("@Comment", "")
            cmdupsert.ExecuteNonQuery()
            cmdupsert.Dispose()
            cmdupsert = Nothing
            cnp.Close()
            cnp = Nothing
            '
            ProvIDs += dgvRptProviders.Rows(i).Cells(0).Value.ToString & ", "
        Next
        If ProvIDs.EndsWith(", ") Then ProvIDs = Microsoft.VisualBasic.Mid(ProvIDs, 1, Len(ProvIDs) - 2)
        If ProvIDs <> "" Then
            ExecuteSqlProcedure("Delete from Req_RPT where EntrySource = " &
            "'Accession' and Base_ID = " & AccID & " and not Provider_ID in (" & ProvIDs & ")")
        End If
    End Sub

    Private Function DxInTheList(ByVal Dx As String) As Boolean
        Dim i As Integer
        Dim InTheList As Boolean = False
        For i = 0 To dgvDxs.RowCount - 1
            If dgvDxs.Rows(i).Cells(0).Value = Dx Then
                InTheList = True
                Exit For
            End If
        Next
        DxInTheList = InTheList
    End Function

    Private Sub dgvDxs_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDxs.CellEndEdit
        If e.ColumnIndex = 0 Then
            If Trim(dgvDxs.Rows(e.RowIndex).Cells(0).Value) <> "" Then
                If IsDuplicateDx(dgvDxs.Rows(e.RowIndex).Cells(0).Value) Then
                    MsgBox("Grid already contains the code you just typed")
                    dgvDxs.Rows(e.RowIndex).Cells(0).Value = ""
                Else
                    If IsCodeComplete(dgvDxs.Rows(e.RowIndex).Cells(0).Value) Then
                        If e.RowIndex = dgvDxs.RowCount - 1 Then
                            dgvDxs.RowCount += 1
                            dgvDxs.CurrentCell = dgvDxs.Rows(dgvDxs.RowCount - 1).Cells(0)
                        End If
                    Else
                        TCode = dgvDxs.Rows(e.RowIndex).Cells(0).Value
                        If TCode.Length >= 3 Then
                            TCode = frmDiagnosis.ShowDialog
                            If TCode <> "" Then
                                dgvDxs.RowCount += 1
                                dgvDxs.CurrentCell.Value = TCode
                                TCode = ""
                            Else
                                dgvDxs.Rows(e.RowIndex).Cells(0).Value = TCode
                            End If
                        Else
                            MsgBox("Minimum 3 characters required", MsgBoxStyle.Critical, "Prolis")
                            dgvDxs.Rows(e.RowIndex).Cells(0).Value = ""
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Function IsDuplicateDx(ByVal Dx As String) As Boolean
        Dim i As Integer
        Dim DxCount As Integer = 0
        For i = 0 To dgvDxs.RowCount - 1
            If Trim(dgvDxs.Rows(i).Cells(0).Value) = Dx Then
                DxCount = DxCount + 1
            End If
        Next
        If DxCount > 1 Then
            IsDuplicateDx = True
        Else
            IsDuplicateDx = False
        End If
    End Function

    Private Sub btnRemDxAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ClearDxs()
    End Sub

    Private Sub ClearDxs()
        dgvDxs.Rows.Clear()
        dgvDxs.RowCount = 20
    End Sub

    Private Sub btnPatDx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If txtPatientID.Text <> "" Then _
        PopulatePatientDxs(Val(txtPatientID.Text))
    End Sub

    Private Sub PopulatePatientDxs(ByVal PatientID As Long)
        Dim cnppd As New SqlConnection(connString)
        cnppd.Open()
        Dim cmdppd As New SqlCommand("Select Dx_Code " &
        "from Patient_Dx where Patient_ID = " & Val(txtPatientID.Text), cnppd)
        cmdppd.CommandType = Data.CommandType.Text
        Dim drppd As SqlDataReader = cmdppd.ExecuteReader
        If drppd.HasRows Then
            While drppd.Read
                If Not DxInTheList(drppd("Dx_Code")) Then
                    For i As Integer = 0 To dgvDxs.RowCount - 1
                        If dgvDxs.Rows(i).Cells(0).Value = "" Then
                            dgvDxs.Rows(i).Cells(0).Value = drppd("Dx_Code")
                            Exit For
                        End If
                    Next
                End If
            End While
        End If
        cnppd.Close()
        cnppd = Nothing
    End Sub

    Private Function GetTGPInfo(ByVal TGPID As Integer) As String()
        Dim Info() As String = {"", "", ""}
        Dim sSQL As String = "Select Name, InHouse ,IsActive  from Tests where ID = " & TGPID & " Union Select Name, InHouse ,IsActive " &
        "from Groups where ID = " & TGPID & " Union Select Name, InHouse ,IsActive from Profiles where ID = " & TGPID
        '
        Dim cnn As New SqlConnection(connString)
        cnn.Open()
        Dim cmdsel As New SqlCommand(sSQL, cnn)
        cmdsel.CommandType = Data.CommandType.Text
        Dim drsel As SqlDataReader = cmdsel.ExecuteReader
        If drsel.HasRows Then
            While drsel.Read
                Info(0) = drsel("Name")
                If drsel("InHouse") IsNot DBNull.Value Then
                    Info(1) = Convert.ToInt16(drsel("InHouse")).ToString
                Else
                    Info(1) = "1"
                End If
                Info(2) = drsel("isActive")
            End While
        End If
        cnn.Close()
        cnn = Nothing
        '
        Return Info
    End Function
    Private Sub dgvTGPMarked_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTGPMarked.CellClick
        Dim RecMis() As String
        Dim Billed As Boolean = False
        Dim IsComplete As Boolean = False
        Dim TGPInfo() As String = {"", ""}
        If txtAccID.Text <> "" Then
            Billed = IsAccBilled(txtAccID.Text)
            IsComplete = ReportFullResulted(txtAccID.Text)
        End If
        If Not Billed Then
            If e.RowIndex <> -1 Then
                If e.ColumnIndex = 1 Then
                    Dim TGPID As String = frmTGPLookup.ShowDialog
                    If TGPID <> "" Then
                        If TGPMarked(Val(TGPID)) <= 1 Then
                            If ExtMarkable(Val(TGPID)) Then
                                Dim CompType = GetTGPType(Val(TGPID))
                                If Not chkPostPrePhleb.Checked Then 'Acc
                                    '0=Cleared, 1=Missed, 2=MissedSources
                                    RecMis = SourcedProper(Val(TGPID), Sources)
                                    If (CompType = "T" Or CompType = "G") And Val(RecMis(0)) > 0 Then
                                        dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value = TGPID
                                        TGPInfo = GetTGPInfo(TGPID) 'Name and OS,isActive
                                        If TGPInfo(2) = False Then
                                            MessageBox.Show("This TGP is not Active", "Prolis", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            CurTGP = ""
                                            Return
                                        End If
                                        dgvTGPMarked.Rows(e.RowIndex).Cells(2).Value = TGPInfo(0)
                                        dgvTGPMarked.Rows(e.RowIndex).Cells(6).Value = Not CType(TGPInfo(1), Boolean)
                                        CurTGP = TGPID
                                        If CompType = "T" Then
                                            dgvTGPMarked.Rows(e.RowIndex).Cells(3).Value =
                                            System.Drawing.Image.FromFile(Application.StartupPath &
                                            "\Images\Test.ico")
                                        ElseIf CompType = "G" Then
                                            dgvTGPMarked.Rows(e.RowIndex).Cells(3).Value =
                                            System.Drawing.Image.FromFile(Application.StartupPath &
                                            "\Images\Group.ico")
                                        End If
                                    ElseIf (Val(RecMis(0)) > 0 And CompType = "P") Then
                                        dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value = TGPID
                                        CurTGP = TGPID
                                        TGPInfo = GetTGPInfo(TGPID) 'Name and OS, isActive
                                        If TGPInfo(2) = False Then
                                            MessageBox.Show("This TGP is not Active", "Prolis", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            CurTGP = ""
                                            Return
                                        End If
                                        dgvTGPMarked.Rows(e.RowIndex).Cells(6).Value = Not CType(TGPInfo(1), Boolean)
                                        dgvTGPMarked.Rows(e.RowIndex).Cells(2).Value = TGPInfo(0)
                                        dgvTGPMarked.Rows(e.RowIndex).Cells(3).Value =
                                        System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Profile.ico")
                                        If Val(RecMis(1)) > 0 Then  'missing a source
                                            chkProfile.Checked = True   'break
                                            MsgBox(RecMis(2) & " missing in the specimen contents required for the profile " &
                                            "being ordered. Prolis allows such components but will be saved, by default, " &
                                            "broken in to profiles contents. It is suggested to open the accession in the " &
                                            "Edit mode and hoover over all orders to identify the un-synched constituent of " &
                                            "the profile and delete the mismatched component", MsgBoxStyle.Exclamation, "Prolis")
                                            If InStr(UnreceivedSrcs, RecMis(2)) = 0 _
                                            Then UnreceivedSrcs += RecMis(2)
                                        End If
                                    Else
                                        MsgBox("The component being ordered requires '" & RecMis(2) & "' and is missing " &
                                        "in the accession/specimen contents.", MsgBoxStyle.Critical, "Prolis")
                                        CurTGP = ""
                                    End If
                                Else
                                    dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value = TGPID
                                    CurTGP = TGPID
                                    dgvTGPMarked.Rows(e.RowIndex).Cells(2).Value = GetTGPName(TGPID)
                                    If CompType = "T" Then
                                        dgvTGPMarked.Rows(e.RowIndex).Cells(3).Value =
                                        System.Drawing.Image.FromFile(Application.StartupPath &
                                        "\Images\Test.ico")
                                    ElseIf CompType = "G" Then
                                        dgvTGPMarked.Rows(e.RowIndex).Cells(3).Value =
                                        System.Drawing.Image.FromFile(Application.StartupPath &
                                        "\Images\Group.ico")
                                    Else
                                        dgvTGPMarked.Rows(e.RowIndex).Cells(3).Value =
                                        System.Drawing.Image.FromFile(Application.StartupPath &
                                        "\Images\Profile.ico")
                                    End If
                                End If
                            Else
                                MsgBox("The component does not meet extended marking conditions", MsgBoxStyle.Critical, "Prolis")
                                CurTGP = ""
                            End If
                        Else
                            MsgBox("Duplicate ID", MsgBoxStyle.Critical, "Prolis")
                            CurTGP = ""
                        End If
                    End If
                End If
            End If
        End If
        UpdateOrdStatus()
        UpdateRequisitionProgress()
    End Sub

    Private Function GetMissedTests(ByVal ClcID As Integer) As String
        Dim MisTests As String = ""
        Dim Tests As String = GetOprands(ClcID)
        Dim TSTS() As String = Split(Tests, ",")
        Dim TGPS() As String = {""}
        Dim Matched As Boolean = False
        For a As Integer = 0 To dgvTGPMarked.RowCount - 1
            If dgvTGPMarked.Rows(a).Cells(0).Value IsNot Nothing AndAlso
            dgvTGPMarked.Rows(a).Cells(0).Value.ToString <> "" AndAlso
            dgvTGPMarked.Rows(a).Cells(0).Value.ToString <> ClcID.ToString Then
                If TGPS(UBound(TGPS)) <> "" Then ReDim Preserve TGPS(UBound(TGPS) + 1)
                TGPS(UBound(TGPS)) = dgvTGPMarked.Rows(a).Cells(0).Value.ToString
            End If
        Next
        If TGPS(0) = "" And Trim(TSTS(0)) <> "" Then    'fail
            For t As Integer = 0 To TSTS.Length - 1
                If Trim(TSTS(t)) <> "" Then
                    If InStr(MisTests, GetTGPName(TSTS(t)) &
                    ", ") = 0 Then MisTests += GetTGPName(TSTS(t)) & ", "
                End If
            Next
            If MisTests.EndsWith(", ") Then MisTests = Microsoft.VisualBasic.Mid(MisTests, 1, Len(MisTests) - 2)
        ElseIf TGPS(0) <> "" And Trim(TSTS(0)) <> "" Then    'process further
            Dim TIDS() As String = GetTestIDsfromTGPS(TGPS)
            For i As Integer = 0 To TSTS.Length - 1
                Matched = False
                If Trim(TSTS(i)) <> "" Then
                    For n As Integer = 0 To TIDS.Length - 1
                        If Trim(TSTS(i)) = TIDS(n) Then
                            Matched = True
                            Exit For
                        End If
                    Next
                End If
                If Matched = False Then
                    If InStr(MisTests, GetTGPName(TSTS(i)) &
                    ", ") = 0 Then MisTests += GetTGPName(TSTS(i)) & ", "
                End If
            Next
            If MisTests.EndsWith(", ") Then MisTests = Microsoft.VisualBasic.Mid(MisTests, 1, Len(MisTests) - 2)
        End If
        Return MisTests
    End Function

    Private Function GetTestIDsfromTGPS(ByVal TGPS() As String) As String()
        Dim TestIDs() As String = {""}
        Dim sSQL As String = ""
        '
        For i As Integer = 0 To TGPS.Length - 1
            If TGPS(i) <> "" Then
                If GetTGPType(TGPS(i)) = "P" Then   'P
                    sSQL = "Select c.Test_ID as TestID from (Group_Test c inner join Tests d on c.Test_ID = d.ID) " &
                    "inner join Prof_GrpTst e on e.GrpTst_ID = c.Group_ID where d.IsActive <> 0 and d.HasResult <> 0 " &
                    "and e.GrpTst_ID in (Select ID from Groups where IsActive <> 0) and e.Profile_ID = " & Val(TGPS(i)) &
                    " order by e.Ordinal, c.Ordinal"
                    Dim cnp As New SqlConnection(connString)
                    cnp.Open()
                    Dim pgtcmd As New SqlCommand(sSQL, cnp)
                    pgtcmd.CommandType = Data.CommandType.Text
                    Dim pgtDR As SqlDataReader = pgtcmd.ExecuteReader
                    If pgtDR.HasRows Then
                        While pgtDR.Read
                            If Not TESTinTESTS(pgtDR("TestID"), TestIDs) Then
                                TestIDs = AddTESTinTESTS(pgtDR("TestID"), TestIDs)
                            End If
                        End While
                    End If
                    pgtDR.Close()
                    pgtcmd.Dispose()
                    cnp.Close()
                    cnp = Nothing
                    'Tests in Profile
                    sSQL = "Select a.GrpTst_ID as TestID from Prof_GrpTst a inner join Tests b on b.ID = a.GrpTst_ID where " &
                    "b.IsActive <> 0 and b.HasResult <> 0 and a.Profile_ID = " & Val(TGPS(i)) & " order by a.Ordinal"
                    Dim cnpt As New SqlConnection(connString)
                    cnpt.Open()
                    Dim ptcmd As New SqlCommand(sSQL, cnpt)
                    ptcmd.CommandType = Data.CommandType.Text
                    Dim ptDR As SqlDataReader = ptcmd.ExecuteReader
                    If ptDR.HasRows Then
                        While ptDR.Read
                            If Not TESTinTESTS(ptDR("TestID"), TestIDs) Then
                                TestIDs = AddTESTinTESTS(ptDR("TestID"), TestIDs)
                            End If
                        End While
                    End If
                    ptDR.Close()
                    ptcmd.Dispose()
                    cnpt.Close()
                    cnpt = Nothing
                ElseIf GetTGPType(TGPS(i)) = "G" Then   'G
                    sSQL = "Select a.Test_ID as TestID from Group_Test a inner join Tests b on b.ID = a.Test_ID " &
                    "where b.IsActive <> 0 and b.HasResult <> 0 and a.Group_ID = " & TGPS(i) & " order by a.ordinal"
                    Dim cngt As New SqlConnection(connString)
                    cngt.Open()
                    Dim gtcmd As New SqlCommand(sSQL, cngt)
                    gtcmd.CommandType = Data.CommandType.Text
                    Dim gtDR As SqlDataReader = gtcmd.ExecuteReader
                    If gtDR.HasRows Then
                        While gtDR.Read
                            If Not TESTinTESTS(gtDR("TestID").ToString, TestIDs) Then
                                TestIDs = AddTESTinTESTS(gtDR("TestID"), TestIDs)
                            End If
                        End While
                    End If
                    gtDR.Close()
                    gtcmd.Dispose()
                    cngt.Close()
                    cngt = Nothing
                Else    'T
                    sSQL = "Select ID from Tests where IsActive <> 0 and HasResult <> 0 and ID = " & TGPS(i)
                    Dim cnt As New SqlConnection(connString)
                    cnt.Open()
                    Dim tcmd As New SqlCommand(sSQL, cnt)
                    tcmd.CommandType = Data.CommandType.Text
                    Dim tDR As SqlDataReader = tcmd.ExecuteReader
                    If tDR.HasRows Then
                        While tDR.Read
                            If Not TESTinTESTS(tDR("ID").ToString, TestIDs) Then
                                TestIDs = AddTESTinTESTS(tDR("ID").ToString, TestIDs)
                            End If
                        End While
                    End If
                    tDR.Close()
                    tcmd.Dispose()
                    cnt.Close()
                    cnt = Nothing
                End If
            End If
        Next
        Return TestIDs
    End Function

    Private Function SourcedProper(ByVal TGPID As Integer, ByVal Source As String) As String()
        If Source.EndsWith(", ") Then Source = Microsoft.VisualBasic.Mid(Source, 1, Len(Source) - 2)
        Dim SrcData() As String = {"0", "0", ""}    '0=Cleared, 1=Missed, 2=missedsources
        If IsCalculated(TGPID) Then
            Dim MisTests As String = GetMissedTests(TGPID)
            If MisTests <> "" Then
                Dim TSTS() As String = Split(MisTests, ", ")
                SrcData(1) = TSTS.Length.ToString
                SrcData(2) = MisTests
            Else
                SrcData(0) = "1"
            End If
        Else
            Dim TGPMats() As String = GetTGPMaterials(TGPID)
            Dim SourceMats() As String = Split(Source, ", ")
            Dim Cleared As Boolean = False
            Dim Missed() As String = {""}
            Dim Matched As Integer = 0
            For i As Integer = 0 To TGPMats.Length - 1
                For n As Integer = 0 To SourceMats.Length - 1
                    If Trim(TGPMats(i)) <> "" And Trim(SourceMats(n)) <> "" Then
                        If Trim(TGPMats(i)) = Trim(SourceMats(n)) Then
                            Cleared = True
                            SrcData(0) += Val(SrcData(0)) + 1
                            Exit For
                        End If
                    End If
                Next
                If Cleared = False Then
                    SrcData(1) += Val(SrcData(1)) + 1
                    If Missed(UBound(Missed)) <> "" Then _
                    ReDim Preserve Missed(UBound(Missed) + 1)
                    Missed(UBound(Missed)) = GetSource(Trim(TGPMats(i)))
                End If
            Next
            If Missed(0) <> "" Then SrcData(2) = Join(Missed, ", ")
        End If
        Return SrcData
    End Function

    Private Function GetSource(ByVal Mat As String) As String
        Dim Src As String = ""
        '
        Dim cns As New SqlConnection(connString)
        cns.Open()
        Dim selcmd As New SqlCommand("Select Name from Sources where " &
        "Material_ID in (Select ID from Materials where Name like '" & Mat & "%')", cns)
        selcmd.CommandType = Data.CommandType.Text
        Dim selDR As SqlDataReader = selcmd.ExecuteReader
        If selDR.HasRows Then
            While selDR.Read
                Src = selDR("Name")
            End While
        End If
        selDR.Close()
        selcmd.Dispose()
        cns.Close()
        cns = Nothing
        '
        Return Src
    End Function

    Private Sub dgvTGPMarked_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvTGPMarked.Validated
        UpdateOrdStatus()
        UpdateRequisitionProgress()
    End Sub

    Private Sub btnAccLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccLook.Click
        Dim AccID As String = frmReqLookUp.ShowDialog()
        If AccID <> "" Then DisplayAccessionRecord(Val(AccID))
        AccID = ""
    End Sub

    Private Sub btnSIns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSIns.Click
        Dim PayerInfo As String = frmActivePayersLookUp.ShowDialog()
        If PayerInfo <> "" Then
            Dim PRS() As String = Split(PayerInfo, "|")
            If PRS(0) <> txtSInsID.Text Then
                txtSInsID.Text = PRS(0)
                If PRS.Length >= 2 Then
                    txtSInsName.Text = PRS(1)
                    If cmbSRelation.SelectedIndex = -1 Then _
                    cmbSRelation.SelectedIndex = 0
                    Update_Billing_Status()
                    UpdateRequisitionProgress()
                End If
            Else
                MsgBox("The Insurance you selected, is the primary " &
                "coverage in the accession.", MsgBoxStyle.Critical, "Prolis")
            End If
        End If
    End Sub

    Private Function TGPMarkable(ByVal TGPID As Integer) As Boolean
        Dim Markable As Boolean = False
        Dim cncm As New SqlConnection(connString)
        cncm.Open()
        Dim cmdcm As New SqlCommand("Select ID from Tests where IsMarkable <> 0 " &
        "and ID = " & TGPID & " Union Select ID from Groups where IsMarkable <> 0 and ID = " &
        TGPID & " Union Select ID from Profiles where IsMarkable <> 0 and ID = " & TGPID, cncm)
        cmdcm.CommandType = Data.CommandType.Text
        Dim drcm As SqlDataReader = cmdcm.ExecuteReader
        If drcm.HasRows Then Markable = True
        cncm.Close()
        cncm = Nothing
        Return Markable
    End Function

    Private Sub dgvTGPMarked_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTGPMarked.CellEnter
        Curow = e.RowIndex
        CurTGP = dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value
    End Sub

    Private Sub dgvTGPMarked_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTGPMarked.CellValidated
        If (btnNew.Checked = False And CurTGP = "") _
        OrElse btnNew.Checked = True Then
            If e.RowIndex <> -1 Then
                If e.ColumnIndex = 0 Then
                    Dim TGPInfo() As String
                    If dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value IsNot Nothing _
                    AndAlso Trim(dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value.ToString) <> "" Then
                        If IsNumeric(dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value) AndAlso
                        TGPMarkable(dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value) = True Then
                            Dim TGPID As String = dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value
                            TGPInfo = GetTGPInfo(TGPID) 'Name and OS, IsActive
                            Dim TGP As String = TGPInfo(0)
                            If TGPInfo(2) = False Then  'if tgp is not active

                                MessageBox.Show("This TGP is not Active", "Prolis", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value = ""
                                dgvTGPMarked.Rows(e.RowIndex).Cells(2).Value = ""
                                dgvTGPMarked.Rows(e.RowIndex).Cells(6).Value = False
                                Return
                            End If

                            If TGP <> "" Then
                                If TGPMarked(Val(TGPID)) <= 1 Then
                                    If ExtMarkable(Val(TGPID)) Then
                                        Dim CompType = GetTGPType(Val(TGPID))
                                        If Not chkPostPrePhleb.Checked Then 'Acc
                                            '0=Cleared, 1=Missed, 2=MissedSources
                                            Dim RecMis() As String = SourcedProper(Val(TGPID), Sources)
                                            dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value = TGPID
                                            dgvTGPMarked.Rows(e.RowIndex).Cells(2).Value = TGPInfo(0)
                                            dgvTGPMarked.Rows(e.RowIndex).Cells(6).Value = Not CType(TGPInfo(1), Boolean)
                                            If (CompType = "T" Or CompType = "G") And Val(RecMis(0)) > 0 Then
                                                If CompType = "T" Then
                                                    dgvTGPMarked.Rows(e.RowIndex).Cells(3).Value =
                                                    System.Drawing.Image.FromFile(Application.StartupPath &
                                                    "\Images\Test.ico")
                                                ElseIf CompType = "G" Then
                                                    dgvTGPMarked.Rows(e.RowIndex).Cells(3).Value =
                                                    System.Drawing.Image.FromFile(Application.StartupPath &
                                                    "\Images\Group.ico")
                                                End If
                                                CurTGP = TGPID
                                            ElseIf (Val(RecMis(0)) > 0 And CompType = "P") Then
                                                dgvTGPMarked.Rows(e.RowIndex).Cells(3).Value =
                                                System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Profile.ico")
                                                If Val(RecMis(1)) > 0 Then  'missing a source
                                                    chkProfile.Checked = True   'break
                                                    MsgBox(RecMis(2) & " missing in the specimen contents required for the profile " &
                                                    "being ordered. Prolis allows such components but will be saved, by default, " &
                                                    "broken in to profiles contents. It is suggested to open the accession in the " &
                                                    "Edit mode and hoover over all orders to identify the un-synched constituent of " &
                                                    "the profile and delete the mismatched component", MsgBoxStyle.Exclamation, "Prolis")
                                                    If InStr(UnreceivedSrcs, RecMis(2)) = 0 _
                                                    Then UnreceivedSrcs += RecMis(2)
                                                    CurTGP = TGPID
                                                End If
                                            Else
                                                If (CompType = "T" Or CompType = "G") And Val(RecMis(0)) > 0 Then


                                                    If CompType = "T" Then
                                                        dgvTGPMarked.Rows(e.RowIndex).Cells(3).Value =
                                                        System.Drawing.Image.FromFile(Application.StartupPath &
                                                        "\Images\Test.ico")
                                                    ElseIf CompType = "G" Then
                                                        dgvTGPMarked.Rows(e.RowIndex).Cells(3).Value =
                                                        System.Drawing.Image.FromFile(Application.StartupPath &
                                                        "\Images\Group.ico")
                                                    End If
                                                    CurTGP = TGPID
                                                Else
                                                    MsgBox("The component being ordered requires '" & RecMis(2) & "' and is missing " &
                                               "in the Accession/specimen contents.", MsgBoxStyle.Critical, "Prolis")
                                                    CurTGP = ""
                                                    dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value = ""
                                                    dgvTGPMarked.Rows(e.RowIndex).Cells(2).Value = ""
                                                    dgvTGPMarked.Rows(e.RowIndex).Cells(6).Value = False
                                                End If

                                            End If
                                            '
                                            '' while working with AOE it is useless
                                            If HasPreanas(TGPID) Then

                                                frmPreAnaPop.xtAccID = txtAccID.Text
                                                frmPreAnaPop.urTGP = TGPID
                                                frmPreAnaPop.btnNewChecked = btnNew.Checked
                                                frmPreAnaPop.sHandleCreated = Me.IsHandleCreated
                                                ReqInfoResp = frmPreAnaPop.ShowDialog()

                                            End If

                                            'If Not String.IsNullOrEmpty(CurTGP) AndAlso HasAOE(TGPID) Then

                                            '    'frmAOE_popup.xtAccID = txtAccID.Text
                                            '    'frmAOE_popup.urTGP = TGPID
                                            '    'frmAOE_popup.btnNewChecked = btnNew.Checked
                                            '    'frmAOE_popup.sHandleCreated = Me.IsHandleCreated
                                            '    'ReqInfoResp = frmAOE_popup.ShowDialog()

                                            '    Dim f As New frmAOE_popup
                                            '    With f
                                            '        .xtAccID = txtAccID.Text
                                            '        .urTGP = TGPID
                                            '        .btnNewChecked = btnNew.Checked
                                            '        .sHandleCreated = Me.IsHandleCreated
                                            '    End With

                                            '    'frmAOE_popup.ShowDialog()

                                            '    f.ShowDialog()
                                            '    If f.DialogResult = DialogResult.OK Then
                                            '        For Each item As String In f.Req_Info_Response_List
                                            '            Req_Info_Response_MainList.Add(item)
                                            '        Next
                                            '    End If

                                            'End If

                                        Else
                                            dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value = TGPID
                                            CurTGP = TGPID
                                            dgvTGPMarked.Rows(e.RowIndex).Cells(2).Value = GetTGPName(TGPID)
                                            If CompType = "T" Then
                                                dgvTGPMarked.Rows(e.RowIndex).Cells(3).Value =
                                                System.Drawing.Image.FromFile(Application.StartupPath &
                                                "\Images\Test.ico")
                                            ElseIf CompType = "G" Then
                                                dgvTGPMarked.Rows(e.RowIndex).Cells(3).Value =
                                                System.Drawing.Image.FromFile(Application.StartupPath &
                                                "\Images\Group.ico")
                                            Else
                                                dgvTGPMarked.Rows(e.RowIndex).Cells(3).Value =
                                                System.Drawing.Image.FromFile(Application.StartupPath &
                                                "\Images\Profile.ico")
                                            End If
                                        End If
                                    Else
                                        MsgBox("The component does not meet extended marking conditions", MsgBoxStyle.Critical, "Prolis")
                                    End If
                                Else
                                    MsgBox("Duplicate ID", MsgBoxStyle.Critical, "Prolis")
                                    CurTGP = ""
                                End If
                            Else
                                MsgBox("Invalid ID", MsgBoxStyle.Critical, "Prolis")
                                dgvTGPMarked.Rows(Curow).Cells(0).Value = ""
                                dgvTGPMarked.Rows(Curow).Cells(4).Value = False
                                dgvTGPMarked.Rows(Curow).Cells(5).Value = False
                                dgvTGPMarked.Rows(Curow).Cells(6).Value = False
                                CurTGP = ""
                            End If
                        Else
                            MsgBox("Invalid ID", MsgBoxStyle.Critical, "Prolis")
                            dgvTGPMarked.Rows(Curow).Cells(0).Value = ""
                            dgvTGPMarked.Rows(Curow).Cells(4).Value = False
                            dgvTGPMarked.Rows(Curow).Cells(5).Value = False
                            dgvTGPMarked.Rows(Curow).Cells(6).Value = False
                            CurTGP = ""
                        End If
                    Else
                        'If CurTGP <> SystemConfig.InPhlebTGP And CurTGP <> SystemConfig.OutPhlebTGP Then
                        dgvTGPMarked.Rows(Curow).Cells(2).Value = ""
                        dgvTGPMarked.Rows(Curow).Cells(4).Value = False
                        dgvTGPMarked.Rows(Curow).Cells(5).Value = False
                        dgvTGPMarked.Rows(Curow).Cells(6).Value = False
                        dgvTGPMarked.Rows(Curow).Cells(3).Value =
                        System.Drawing.Image.FromFile(Application.StartupPath &
                        "\Images\Blank.ico")
                        UpdatePhlebotomy()
                        'Else
                        'dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value = CurTGP
                        'End If
                    End If
                ElseIf e.ColumnIndex = 5 Then   'Verbal
                    Dim VCt As Integer = 0 : Dim NVCt As Integer = 0 : Dim TGPC As Integer = 0
                    Dim i As Integer
                    For i = 0 To dgvTGPMarked.RowCount - 1
                        If Not dgvTGPMarked.Rows(i).Cells(0).Value Is Nothing _
                        AndAlso dgvTGPMarked.Rows(i).Cells(0).Value.ToString <> "" Then
                            If dgvTGPMarked.Rows(i).Cells(5).Value = 0 Then
                                NVCt += 1
                            Else
                                VCt += 1
                            End If
                            TGPC += 1
                        End If
                    Next
                    If VCt = TGPC Then
                        chkVerbal.CheckState = CheckState.Checked
                    ElseIf NVCt = TGPC Then
                        chkVerbal.CheckState = CheckState.Unchecked
                        'Else
                        'chkVerbal.CheckState = CheckState.Unchecked
                    End If
                End If
            End If
            UpdateOrdStatus()
            UpdateRequisitionProgress()
        Else
            If dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value Is Nothing OrElse
            Trim(dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value.ToString) = "" Then

                '====================================================
                'remove from list
                For i As Integer = Req_Info_Response_MainList.Count - 1 To 0 Step -1
                    Dim pipeIndex As Integer = Req_Info_Response_MainList(i).IndexOf("|")

                    If pipeIndex >= 0 Then
                        Dim firstPart As String = Req_Info_Response_MainList(i).Substring(0, pipeIndex)
                        If firstPart = CurTGP Then
                            Req_Info_Response_MainList.RemoveAt(i)
                        End If
                    End If
                Next
                '====================================================

                CurTGP = ""
                dgvTGPMarked.Rows(e.RowIndex).Cells(2).Value = ""
                dgvTGPMarked.Rows(Curow).Cells(4).Value = False
                dgvTGPMarked.Rows(Curow).Cells(5).Value = False
                dgvTGPMarked.Rows(Curow).Cells(6).Value = False
                dgvTGPMarked.Rows(e.RowIndex).Cells(3).Value =
                System.Drawing.Image.FromFile(Application.StartupPath &
                "\Images\Blank.ico")
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If (txtAccID.Text <> String.Empty AndAlso IsNumeric(txtAccID.Text)) And
        txtOrdID.Text <> "" And dgvSources.RowCount > 0 _
        And dgvTGPMarked.RowCount > 0 And dgvRptProviders.RowCount > 0 And
        ((cmbSpecimenType.SelectedIndex = 0 And txtPatientID.Text <> "") Or
        (cmbSpecimenType.SelectedIndex <> 0)) Then
            If ThisUser.Supervisor = True And ThisUser.Hard_Deletion = True Then
                If AccessionResulted(Val(txtAccID.Text)) = True Then
                    MsgBox("The accession record you are trying to delete, " &
                    "has been resulted. Prolis does not allow a resulted " &
                    "accession to be deleted.", MsgBoxStyle.Critical, "Prolis System")
                Else
                    If Not AccessionBilled(Val(txtAccID.Text)) Then
                        Dim RetVal As Integer
                        RetVal = MsgBox("Prolis recommeds never to delete any accession " _
                        & "record the results of which have been reported. However, Prolis" _
                        & " does support deleting the non-billed accession records. It is " _
                        & "further suggested and assumed you have checked that the record " _
                        & "has not been reported. Are you certain to delete this accession " _
                        & "record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo,
                        "Prolis Accession")
                        If RetVal = vbYes Then
                            ExecuteSqlProcedure("Delete from Req_Billable where Accession_ID = " & Val(txtAccID.Text))
                            ExecuteSqlProcedure("Delete from Ref_Results where Accession_ID = " & Val(txtAccID.Text))
                            ExecuteSqlProcedure("Delete from Acc_Info_Results where Accession_ID = " & Val(txtAccID.Text))
                            ExecuteSqlProcedure("Delete from Acc_Results where Accession_ID = " & Val(txtAccID.Text))
                            ExecuteSqlProcedure("Delete from Req_Info_Response where Accession_ID = " & Val(txtAccID.Text))
                            ExecuteSqlProcedure("Delete from Req_Tests where Accession_ID = " & Val(txtAccID.Text))
                            ExecuteSqlProcedure("Delete from Req_TGP where Accession_ID = " & Val(txtAccID.Text))
                            ExecuteSqlProcedure("Delete from Req_Coverage where Accession_ID = " & Val(txtAccID.Text))
                            ExecuteSqlProcedure("Delete from Specimens where Accession_ID = " & Val(txtAccID.Text))
                            ExecuteSqlProcedure("Delete from Req_Dx where Accession_ID = " & Val(txtAccID.Text))
                            ExecuteSqlProcedure("Delete from Req_Rpt where Rpt_Type = 'ACC' and Base_ID = " & Val(txtAccID.Text))
                            ExecuteSqlProcedure("Delete from Payments where Accession_ID = " & Val(txtAccID.Text))
                            ExecuteSqlProcedure("Delete from Requisitions where ID = " & Val(txtAccID.Text))
                            If SystemConfig.AuditTrail = True Then
                                Dim CurAccVals As String = GetCurrentAccVals(Val(txtAccID.Text))
                                LogUserEvent(ThisUser.ID, 4, Date.Now, "Accession",
                                Val(txtAccID.Text), CurAccVals, "")
                            End If
                            ClearForm()
                            btnDelete.Enabled = False
                            btnSave.Enabled = False
                        End If
                    Else
                        MsgBox("The accession record you are trying to delete, has been " _
                        & "billed. Prolis does not allow a billed accession to be deleted" _
                        & ", to keep the system stable. If you have to delete this record" _
                        & ", reverse its billing first and then delete it.", MsgBoxStyle.Critical, "Prolis System")
                    End If
                End If
            Else
                MsgBox(ThisUser.Name & "! you don't have the authorization to " &
                "execute this procedure. Contact the system administrator.",
                MsgBoxStyle.Critical, "Prolis System")
            End If
        End If
    End Sub

    Private Function AccessionBilled(ByVal AccID As Long) As Boolean
        Dim Billed As Boolean = False
        Dim cnvp As New SqlConnection(connString)
        cnvp.Open()
        Dim cmdvp As New SqlCommand("Select * " &
        "from Charges where Accession_ID = " & AccID, cnvp)
        cmdvp.CommandType = Data.CommandType.Text
        Dim drvp As SqlDataReader = cmdvp.ExecuteReader
        If drvp.HasRows Then Billed = True
        cnvp.Close()
        cnvp = Nothing
        AccessionBilled = Billed
    End Function

    Private Sub txtLabels_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Numerals(sender, e)
    End Sub

    Private Sub chkIsReady_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIsReady.CheckedChanged
        If chkIsReady.Checked = False Then
            chkIsReady.Text = "No"
        Else
            chkIsReady.Text = "Yes"
        End If
    End Sub

    Private Sub dgvTGPMarked_RowValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTGPMarked.RowValidated
        If (btnNew.Checked = False And CurTGP = "") _
        OrElse btnNew.Checked = True Then
            If e.RowIndex <> -1 Then
                If Not dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value Is Nothing _
                AndAlso dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value.ToString <> "" Then
                    If IsNumeric(dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value) AndAlso
                    TGPMarkable(dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value) = True Then
                        Dim TGP As String = GetTGPName(Val(dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value))
                        Dim CompType = GetTGPType(Val(dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value))
                        If TGP <> "" Then
                            If Not chkPostPrePhleb.Checked Then 'Acc
                                Dim RecMis() As String = SourcedProper(Val(dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value), Sources)
                                If Val(RecMis(0)) > 0 Then
                                    If ExtMarkable(Val(dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value)) Then
                                        If TGPMarked(Val(dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value)) < 2 Then
                                            dgvTGPMarked.Rows(e.RowIndex).Cells(2).Value = TGP
                                            If CompType = "T" Then
                                                dgvTGPMarked.Rows(e.RowIndex).Cells(3).Value =
                                               System.Drawing.Image.FromFile(Application.StartupPath &
                                               "\Images\Test.ico")
                                            ElseIf CompType = "G" Then
                                                dgvTGPMarked.Rows(e.RowIndex).Cells(3).Value =
                                                System.Drawing.Image.FromFile(Application.StartupPath &
                                                "\Images\Group.ico")
                                            Else
                                                dgvTGPMarked.Rows(e.RowIndex).Cells(3).Value =
                                                System.Drawing.Image.FromFile(Application.StartupPath &
                                                "\Images\Profile.ico")
                                                If Val(RecMis(1)) > 0 Then
                                                    chkProfile.Checked = True   'break
                                                    MsgBox("One or more of the profile's contents could " &
                                                    "not be synched with the sources provided. After " &
                                                    "saving, you open this accession in the Edit mode " &
                                                    "and delete the mismatched component",
                                                    MsgBoxStyle.Exclamation, "Prolis")
                                                End If
                                            End If
                                            CurTGP = dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value.ToString
                                            lblOrders.BackColor = Color.PaleGreen
                                            'If e.RowIndex = dgvTGPMarked.RowCount - 1 Then dgvTGPMarked.RowCount += 1
                                        Else
                                            MsgBox("Duplicate ID", MsgBoxStyle.Critical, "Prolis")
                                            dgvTGPMarked.Rows(Curow).Cells(0).Value = ""
                                            CurTGP = ""
                                            dgvTGPMarked.Rows(Curow).Cells(2).Value = ""
                                            dgvTGPMarked.Rows(Curow).Cells(3).Value =
                                            System.Drawing.Image.FromFile(Application.StartupPath &
                                            "\Images\Blank.ico")
                                        End If
                                    Else
                                        MsgBox("The component does not meet extended marking conditions", MsgBoxStyle.Critical, "Prolis")
                                        dgvTGPMarked.Rows(Curow).Cells(0).Value = ""
                                        CurTGP = ""
                                        dgvTGPMarked.Rows(Curow).Cells(2).Value = ""
                                        dgvTGPMarked.Rows(Curow).Cells(3).Value =
                                        System.Drawing.Image.FromFile(Application.StartupPath &
                                        "\Images\Blank.ico")
                                    End If
                                Else
                                    MsgBox("The component being ordered requires '" & RecMis(2) & "' and is missing " &
                                    "in the Accession/specimen contents.", MsgBoxStyle.Critical, "Prolis")
                                    dgvTGPMarked.Rows(Curow).Cells(0).Value = ""
                                    CurTGP = ""
                                    dgvTGPMarked.Rows(Curow).Cells(2).Value = ""
                                    dgvTGPMarked.Rows(Curow).Cells(3).Value =
                                    System.Drawing.Image.FromFile(Application.StartupPath &
                                    "\Images\Blank.ico")
                                End If
                            Else    'Pre - order

                            End If
                        Else
                            MsgBox("Invalid ID", MsgBoxStyle.Critical, "Prolis")
                            dgvTGPMarked.Rows(Curow).Cells(0).Value = ""
                            CurTGP = ""
                            dgvTGPMarked.Rows(Curow).Cells(2).Value = ""
                            dgvTGPMarked.Rows(Curow).Cells(3).Value =
                            System.Drawing.Image.FromFile(Application.StartupPath &
                            "\Images\Blank.ico")
                        End If
                    Else
                        'MsgBox("Invalid ID", MsgBoxStyle.Critical, "Prolis")
                        dgvTGPMarked.Rows(Curow).Cells(0).Value = ""
                        CurTGP = ""
                        dgvTGPMarked.Rows(Curow).Cells(2).Value = ""
                        dgvTGPMarked.Rows(Curow).Cells(3).Value =
                        System.Drawing.Image.FromFile(Application.StartupPath &
                        "\Images\Blank.ico")
                    End If
                Else
                    dgvTGPMarked.Rows(Curow).Cells(0).Value = ""
                    CurTGP = ""
                    dgvTGPMarked.Rows(Curow).Cells(2).Value = ""
                    dgvTGPMarked.Rows(Curow).Cells(3).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath &
                    "\Images\Blank.ico")
                End If
            End If
            UpdateOrdStatus()
            UpdateRequisitionProgress()
        Else
            If dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value Is Nothing OrElse
            dgvTGPMarked.Rows(e.RowIndex).Cells(0).Value.ToString = "" Then
                CurTGP = ""
                dgvTGPMarked.Rows(e.RowIndex).Cells(2).Value = ""
                dgvTGPMarked.Rows(e.RowIndex).Cells(3).Value =
                System.Drawing.Image.FromFile(Application.StartupPath &
                "\Images\Blank.ico")
            End If
        End If
    End Sub

    Private Function GetProviderAccAlert(ByVal ProviderID As Long) As String
        Dim Alert As String = ""
        Dim cnvp As New SqlConnection(connString)
        cnvp.Open()
        Dim cmdvp As New SqlCommand("Select Alert, " &
        "Alert_Acc from Providers where ID = " & ProviderID, cnvp)
        cmdvp.CommandType = Data.CommandType.Text
        Dim drvp As SqlDataReader = cmdvp.ExecuteReader
        If drvp.HasRows Then
            While drvp.Read
                If drvp("Alert") IsNot DBNull.Value AndAlso
                (drvp("Alert") <> "" And drvp("Alert_Acc") <> 0) Then
                    Alert = drvp("Alert")
                End If
            End While
        End If
        cnvp.Close()
        cnvp = Nothing
        Return Alert
    End Function

    Private Function GetPatientAccAlert(ByVal PatientID As Long) As String
        Dim Alert As String = ""
        Dim cnvp As New SqlConnection(connString)
        cnvp.Open()
        Dim cmdvp As New SqlCommand("Select Alert, " &
        "Alert_Acc from Patients where ID = " & PatientID, cnvp)
        cmdvp.CommandType = Data.CommandType.Text
        Dim drvp As SqlDataReader = cmdvp.ExecuteReader
        If drvp.HasRows Then
            While drvp.Read
                If drvp("Alert") IsNot DBNull.Value AndAlso
                (drvp("Alert") <> "" And drvp("Alert_Acc") <> 0) Then
                    Alert = drvp("Alert")
                End If
            End While
        End If
        cnvp.Close()
        cnvp = Nothing
        Return Alert
    End Function

    Private Sub DisplayProviderAlert(ByVal Alert As String, Optional SenderInfo As String = "")
        frmProviderAlert.txtAlert.Rtf = Alert
        If Not String.IsNullOrEmpty(SenderInfo) Then
            frmProviderAlert.Text = SenderInfo
        End If
        frmProviderAlert.Show()
        frmProviderAlert.MdiParent = frmDashboard
        frmProviderAlert.TopMost = True
    End Sub

    Private Sub tpPatient_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpPatient.Validated
        btnPatUpdate_Click(Nothing, Nothing)
    End Sub

    Private Function UpdatePatient(ByVal PatientID As String, ByVal LName As String, ByVal FName As String, ByVal MName As String,
    ByVal Sex As String, ByVal Tage As String, ByVal DOB As Date, ByVal SpeciesID As Integer, ByVal BreedID As Integer, ByVal SSN _
    As String, ByVal Email As String, ByVal HPhone As String, ByVal WPhone As String, ByVal Fax As String, ByVal Cell As String,
    ByVal RaceID As Integer, ByVal Ethnicity As String, ByVal Add1 As String, ByVal Add2 As String, ByVal City As String, ByVal _
    State As String, ByVal Zip As String, ByVal Country As String) As String
        '
        If PatientID = "" Then PatientID = GetPatientIDbyNames(LName, FName, DOB, Sex)
        Dim AddressID As Long = -1
        If Add1 <> "" And City <> "" And State <> "" And Zip <> "" Then _
        AddressID = GetAddressID(Add1, Add2, City, State, Zip, Country)
        Dim cnn As New SqlConnection(connString)
        cnn.Open()
        Dim CMDp As New SqlCommand("Patients_SP", cnn)
        CMDp.CommandType = Data.CommandType.StoredProcedure
        CMDp.Parameters.AddWithValue("@act", "Upsert")
        CMDp.Parameters.AddWithValue("@ID", PatientID)
        CMDp.Parameters.AddWithValue("@LastName", LName)
        CMDp.Parameters.AddWithValue("@FirstName", FName)

        If StoredProcExpectsParameter("@MiddleName") Then
            CMDp.Parameters.AddWithValue("@MiddleName", MName)
        End If

        If StoredProcExpectsParameter("@DOB") Then
            CMDp.Parameters.AddWithValue("@DOB", DOB)
        End If

        CMDp.Parameters.AddWithValue("@Sex", Sex.Substring(0, 1))

        If StoredProcExpectsParameter("@Tage") Then
            CMDp.Parameters.AddWithValue("@Tage", Tage)
        End If

        If StoredProcExpectsParameter("@Species_ID") Then
            CMDp.Parameters.AddWithValue("@Species_ID", SpeciesID)
        End If

        If StoredProcExpectsParameter("@Breed_ID") Then
            CMDp.Parameters.AddWithValue("@Breed_ID", BreedID)
        End If

        CMDp.Parameters.AddWithValue("@IsAlive", 1)
        CMDp.Parameters.AddWithValue("@SSN", SSNNeat(SSN))

        If StoredProcExpectsParameter("@WorkPhone") Then
            CMDp.Parameters.AddWithValue("@WorkPhone", WPhone)
        End If

        If StoredProcExpectsParameter("@Cell") Then
            CMDp.Parameters.AddWithValue("@Cell", Cell)
        End If

        If StoredProcExpectsParameter("@HomePhone") Then
            CMDp.Parameters.AddWithValue("@HomePhone", HPhone)
        End If

        If StoredProcExpectsParameter("@Fax") Then
            CMDp.Parameters.AddWithValue("@Fax", Fax)
        End If

        CMDp.Parameters.AddWithValue("@Email", Email)

        If StoredProcExpectsParameter("@Race_ID") Then
            CMDp.Parameters.AddWithValue("@Race_ID", RaceID)
        Else
            If StoredProcExpectsParameter("@RaceID") Then
                CMDp.Parameters.AddWithValue("@RaceID", RaceID)
            End If


        End If

        CMDp.Parameters.AddWithValue("@Ethnicity", Ethnicity)

        If StoredProcExpectsParameter("@Address_ID") Then
            CMDp.Parameters.AddWithValue("@Address_ID", AddressID)
        End If

        Try
            CMDp.ExecuteNonQuery()
        Catch ex As Exception
            Dim d = ex.Message
        End Try

        CMDp.Dispose()
        cnn.Close()
        cnn = Nothing
        '
        If Trim(txtEMRNo.Text) <> "" And txtOrdID.Text <> "" Then
            Dim cn1 As New SqlConnection(connString)
            cn1.Open()
            Dim cmdupsert As New SqlCommand("Client_Patient_SP", cn1)
            cmdupsert.CommandType = Data.CommandType.StoredProcedure
            cmdupsert.Parameters.AddWithValue("@act", "Upsert")
            cmdupsert.Parameters.AddWithValue("@Provider_ID", txtOrdID.Text)
            cmdupsert.Parameters.AddWithValue("@Patient_ID", PatientID)
            cmdupsert.Parameters.AddWithValue("@EMRNo", Trim(txtEMRNo.Text))
            cmdupsert.Parameters.AddWithValue("@Shift", 1)
            cmdupsert.Parameters.AddWithValue("@Room", Trim(txtRoom.Text))
            'cmdupsert.Parameters.AddWithValue("@ClientUser_ID", DBNull.Value)
            'cmdupsert.Parameters.AddWithValue("@AdmitDate", DBNull.Value)
            'cmdupsert.Parameters.AddWithValue("@DischargeDate", DBNull.Value)
            'cmdupsert.Parameters.AddWithValue("@TestDays", "")
            'cmdupsert.Parameters.AddWithValue("@AttendingProvider_ID", DBNull.Value)
            'cmdupsert.Parameters.AddWithValue("@BillingType_ID", DBNull.Value)
            'cmdupsert.Parameters.AddWithValue("@Phleb_Loc", DBNull.Value)
            cmdupsert.ExecuteNonQuery()
            cmdupsert.Dispose()
            cn1.Close()
            cn1 = Nothing
        End If
        '
        Return PatientID
    End Function
    Private Function StoredProcExpectsParameter(parameterName As String) As Boolean
        Dim query As String = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.PARAMETERS " &
                              "WHERE SPECIFIC_NAME = 'Patients_SP' AND PARAMETER_NAME = @ParameterName"
        Dim connection As SqlConnection = New SqlConnection(connString)
        connection.Open()
        Using cmd As New SqlCommand(query, connection)
            cmd.Parameters.AddWithValue("@ParameterName", parameterName)
            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            Return count > 0
        End Using
        connection.Close()
    End Function


    Private Sub txtLName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtLName.BackColor = FCOLOR
    End Sub

    Private Sub txtLName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLName.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtLName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtLName.BackColor = NFCOLOR
    End Sub

    Private Sub btnPatUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatUpdate.Click
        If txtLName.Text <> "" And txtFName.Text <> "" And cmbSex.SelectedIndex <> -1 And
        IsDate(txtDOB.Text) = True And ((lblChart.ForeColor <> Color.Red) Or
        (lblChart.ForeColor = Color.Red And Trim(txtEMRNo.Text) <> "")) Then
            If txtPatientID.Text = "" Then
                Dim ItemX As MyList = cmbRace.SelectedItem
                Dim ItemS As MyList
                Dim ItemB As MyList
                If SystemConfig.DiagTarget = "V" Then
                    If cmbSpecies.SelectedIndex <> -1 Then
                        ItemS = cmbSpecies.SelectedItem
                    Else
                        ItemS = New MyList("Human", 0)
                    End If
                    If cmbBreed.SelectedIndex <> -1 Then
                        ItemB = cmbBreed.SelectedItem
                    Else
                        ItemB = New MyList("Human", 0)
                    End If
                Else
                    ItemS = New MyList("Human", 0)
                    ItemB = New MyList("Human", 0)
                End If
                txtPatientID.Text = UpdatePatient(txtPatientID.Text, txtLName.Text, txtFName.Text, txtMName.Text,
                cmbSex.SelectedItem.ToString.Substring(0, 1), Trim(txtTage.Text), CDate(txtDOB.Text), ItemS.ItemData,
                ItemB.ItemData, Trim(txtSSN.Text), Trim(txtPatEmail.Text), txtPatHPhone.Text, txtWPhone.Text, txtFax.Text,
                txtCell.Text, ItemX.ItemData, cmbEthnicity.SelectedItem.ToString, txtPatAdr1.Text, txtPatAdr2.Text,
                txtPatCity.Text, txtPatState.Text, txtPatZip.Text, txtPatCountry.Text)
            End If
            LockPatVitalFields()
            '
            lblPatient.BackColor = Color.PaleGreen
            btnRemPat.Enabled = True
        End If
        UpdateRequisitionProgress()
    End Sub

    Private Sub txtDOB_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDOB.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtDOB_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDOB.Validated
        If UserEnteredText(txtDOB) <> "" Then
            If Not IsDate(txtDOB.Text) Then
                MsgBox("Invalid Date", MsgBoxStyle.Critical, "Prolis")
                txtDOB.Text = ""
                txtDOB.Focus()
            Else
                If CDate(txtDOB.Text) > dtpDate.Value Then
                    MsgBox("Birth Date must be earlier than the Accession Date", MsgBoxStyle.Critical, "Prolis")
                    txtDOB.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub txtLabels_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
        If txtLabels.Text = "" Then txtLabels.Text = "0"
    End Sub

    Private Sub btnPmnt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPmnt.Click
        Dim Payment As String = frmAccPayment.ShowDialog
        If Payment <> "" Then
            PMT = Split(Payment, "|")
            '0=ID, 1=ArType, 2=ArID, 3= PType, 4= PDate, 5= DocNo, 6=AMT, 7= UnApp
            If cmbSpecimenType.SelectedIndex = 0 Then   'clinical
                If rbC.Checked = True Then  'Client billing
                    PMT(1) = "0"
                    PMT(2) = Trim(txtOrdID.Text)
                Else
                    PMT(1) = "2"
                    PMT(2) = Trim(txtPatientID.Text)
                End If
            Else
                PMT(1) = "0"
                PMT(2) = Trim(txtOrdID.Text)
            End If
            txtPayment.Text = PMT(6)
        Else
            ReDim PMT(0)
            txtPayment.Text = ""
        End If
    End Sub

    Private Sub dgvTGPMarked_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgvTGPMarked.KeyPress
        If e.KeyChar = Chr(13) Then
            If Curow = dgvTGPMarked.RowCount - 1 And
            (Not dgvTGPMarked.Rows(Curow).Cells(0).Value Is Nothing AndAlso
            dgvTGPMarked.Rows(Curow).Cells(0).Value.ToString <> "") Then
                dgvTGPMarked.Rows.Add()
            End If
        End If
    End Sub

    Private Sub dgvDxs_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDxs.CellEnter
        Curow = e.RowIndex
    End Sub

    Private Sub txtOrdID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOrdID.Validated
        If txtOrdID.Text = "" Then
            ClearOrderer()
            dgvRptProviders.Rows.Clear()
            ProviderDirty = True
            RptDirty = True
        Else
            ProviderDirty = True
            RptDirty = True
            Dim cnvp As New SqlConnection(connString)
            cnvp.Open()
            Dim cmdvp As New SqlCommand("Select * from Providers where Active <> 0 and ID = " & Val(txtOrdID.Text), cnvp)
            cmdvp.CommandType = Data.CommandType.Text
            Dim drvp As SqlDataReader = cmdvp.ExecuteReader
            If drvp.HasRows Then
                While drvp.Read
                    DisplayProvider(Val(txtOrdID.Text))
                    If btnNew.Checked = False Then  'new
                        dgvRptProviders.Rows.Clear()
                        If lstProviders.CheckedItems.Count > 0 Then
                            Dim ItemX As MyList = lstProviders.CheckedItems(0)
                            Dim RptSetting() As String = GetOrdAtndSetting(Val(txtOrdID.Text), ItemX.ItemData)
                            Update_Rpt_Sec(RptSetting)
                        End If
                    End If
                End While
            Else
                MsgBox("Invalid ID")
                txtOrdID.Text = ""
                'ClearOrderer()
                txtOrdID.Focus()
            End If
            cnvp.Close()
            cnvp = Nothing
        End If
        'Update_Provider_Status()
        Update_Rpt_Status()
        Update_Billing_Status()
        UpdateRequisitionProgress()
    End Sub

    Private Sub chkProfile_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkProfile.CheckedChanged
        If chkProfile.Checked = False Then
            chkProfile.Text = "Profile Integral"
        Else
            chkProfile.Text = "Profile Break"
        End If
    End Sub

    Private Sub dtpDate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpDate.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub dtpDate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpDate.Validated
        dtpDateDrawn.Value = dtpDate.Value
    End Sub

    Private Sub txtFName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtFName.BackColor = FCOLOR
    End Sub

    Private Sub txtFName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFName.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtMName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtMName.BackColor = FCOLOR
    End Sub

    Private Sub txtMName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMName.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtSSN_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtSSN.BackColor = FCOLOR
    End Sub

    Private Sub txtSSN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSSN.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtPatAdr1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtPatAdr1.BackColor = FCOLOR
    End Sub

    Private Sub txtPatAdr1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPatAdr1.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPatAdr2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtPatAdr2.BackColor = FCOLOR
    End Sub

    Private Sub txtPatAdr2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPatAdr2.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPatCity_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtPatCity.BackColor = FCOLOR
    End Sub

    Private Sub txtPatCity_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPatCity.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPatCountry_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtPatCountry.BackColor = FCOLOR
    End Sub

    Private Sub txtPatCountry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPatCountry.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPatEmail_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtPatEmail.BackColor = FCOLOR
    End Sub

    Private Sub txtPatEmail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPatEmail.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPatState_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtPatState.BackColor = FCOLOR
    End Sub

    Private Sub txtPatState_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPatState.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPatZip_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtPatZip.BackColor = FCOLOR
    End Sub

    Private Sub txtPatZip_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPatZip.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub chkFasting_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles chkFasting.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub cmbSex_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbSex.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtComment_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.PageDown Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtWorkCmnt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.PageDown Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub tpBilling_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpBilling.Enter
        SendKeys.Send("{TAB}")
    End Sub

    Private Sub tpReports_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpReports.Enter
        SendKeys.Send("{TAB}")
    End Sub

    Private Sub tpSpecimen_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpSpecimen.Enter
        SendKeys.Send("{TAB}")
    End Sub

    Private Sub txtCountry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtDrawnTime_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtDrawnTime.BackColor = FCOLOR
    End Sub

    Private Sub txtDrawnTime_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtEMRNo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        NFCOLOR = txtEMRNo.BackColor
        txtEMRNo.BackColor = FCOLOR
    End Sub

    Private Sub txtEMRNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtEMRNo.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtEMRNo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtEMRNo.BackColor = NFCOLOR
    End Sub

    Private Sub txtFName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtFName.BackColor = NFCOLOR
    End Sub

    Private Sub txtMName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtMName.BackColor = NFCOLOR
    End Sub

    Private Sub txtPatAdr1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtPatAdr1.BackColor = NFCOLOR
    End Sub

    Private Sub txtPatAdr2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtPatAdr2.BackColor = NFCOLOR
    End Sub

    Private Sub txtPatCity_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtPatCity.BackColor = NFCOLOR
    End Sub

    Private Sub txtPatState_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtPatState.BackColor = NFCOLOR
    End Sub

    Private Sub txtPatZip_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtPatZip.BackColor = NFCOLOR
    End Sub

    Private Sub txtPatCountry_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtPatCountry.BackColor = NFCOLOR
    End Sub

    Private Sub txtPatEmail_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtPatEmail.BackColor = NFCOLOR
    End Sub

    Private Sub txtSSN_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtSSN.BackColor = NFCOLOR
    End Sub

    Private Sub txtFName_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFName.Validated
        If txtPatientID.Text = "" Then
            If txtLName.Text <> "" And txtFName.Text <> "" Then

                frmPatLookUp.Owner = Me
                frmPatLookUp.patientName = String.Concat(txtLName.Text, ", ", txtFName.Text)

                Dim PatientID As String = frmPatLookUp.ShowDialog
                If PatientID <> "" Then DisplayPatient(Val(PatientID))
                UpdateRequisitionProgress()
                LockPatVitalFields()
            End If
        End If
    End Sub

    Private Sub txtPatientID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtPatientID.BackColor = NFCOLOR
        If txtPatientID.Text <> "" Then
            Dim PatientID As Long = Val(txtPatientID.Text)
            Dim cnvp As New SqlConnection(connString)
            cnvp.Open()
            Dim cmdvp As New SqlCommand("Select * from Patients where ID = " & PatientID, cnvp)
            cmdvp.CommandType = Data.CommandType.Text
            Dim drvp As SqlDataReader = cmdvp.ExecuteReader
            If drvp.HasRows Then
                While drvp.Read
                    DisplayPatient(drvp("ID"))
                    If PatientCovered(drvp("ID")) = True Then
                        DisplayCoverage(drvp("ID"))
                        rbT.Checked = True
                    End If
                End While
            Else
                MsgBox("The Patient ID provided is not valid. Use Search")
                txtPatientID.Text = ""
                UnLockPatVitalFields()
                If rbP.Checked = True Then rbC.Checked = True
                txtPatientID.Focus()
            End If
            cnvp.Close()
            cnvp = Nothing
        Else
            'ClearPatient()
            'ClearDxs()
            'ClearOrders()
            'btnPatDx.Enabled = False
            rbC.Checked = True
        End If
        Update_Patient_Status()
        Update_Billing_Status()
        UpdateRequisitionProgress()
    End Sub

    Private Sub txtPatHPhone_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtPatHPhone.BackColor = FCOLOR
    End Sub

    Private Sub txtPatHPhone_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPatHPhone.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPatHPhone_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtPatHPhone.BackColor = NFCOLOR
    End Sub

    Private Sub dtpDateDrawn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpDateDrawn.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtDrawnTime_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtDrawnTime.BackColor = NFCOLOR
    End Sub

    Private Sub cmbTemp_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbTemp.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtCopay_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCopay.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPayment_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPayment.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPGroup_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPGroup.GotFocus
        txtPGroup.BackColor = FCOLOR
    End Sub

    Private Sub txtPGroup_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPGroup.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPGroup_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPGroup.LostFocus
        txtPGroup.BackColor = NFCOLOR
    End Sub

    Private Sub txtPFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPFrom.GotFocus
        txtPFrom.BackColor = FCOLOR
    End Sub

    Private Sub txtPFrom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPFrom.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPFrom.LostFocus
        txtPFrom.BackColor = NFCOLOR
    End Sub

    Private Sub txtPTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPTo.GotFocus
        txtPTo.BackColor = FCOLOR
    End Sub

    Private Sub txtPTo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPTo.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtPTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPTo.LostFocus
        txtPTo.BackColor = NFCOLOR
    End Sub

    Private Sub tpOrders_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpOrders.Leave
        dgvTGPMarked.CurrentCell = dgvTGPMarked.Rows(0).Cells(0)
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        ' RsA.MoveNext()
        If CurrAcc < Accessions Then ' Ensure we are not at the end of the DataTable
            CurrAcc += 1 ' Old: CurrAcc += 1
            btnPrevious.Enabled = True ' Old: btnPrevious.Enabled = True
            btnFirst.Enabled = True ' Old: btnFirst.Enabled = True

            If CurrAcc = Accessions Then ' Equivalent to RsA.EOF (End of File)
                ' RsA.MoveLast()
                btnNext.Enabled = False ' Old: btnNext.Enabled = False
                btnLast.Enabled = False ' Old: btnLast.Enabled = False
            End If

            ' Display the current record
            txtNavStatus.Text = CurrAcc & " of " & Accessions ' Old: txtNavStatus.Text = CurrAcc & " of " & Accessions
            DisplayAccessionRecord(dtRecords.Rows(CurrAcc - 1)("ID").ToString()) ' Old: DisplayAccessionRecord(RsA.Fields("ID").Value)
        End If
    End Sub
    Private Sub btnLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLast.Click
        ' RsA.MoveLast()
        CurrAcc = Accessions ' Old: CurrAcc = Accessions
        btnPrevious.Enabled = True ' Old: btnPrevious.Enabled = True
        btnFirst.Enabled = True ' Old: btnFirst.Enabled = True
        btnNext.Enabled = False ' Old: btnNext.Enabled = False
        btnLast.Enabled = False ' Old: btnLast.Enabled = False
        txtNavStatus.Text = CurrAcc & " of " & Accessions ' Old: txtNavStatus.Text = CurrAcc & " of " & Accessions
        DisplayAccessionRecord(dtRecords.Rows(CurrAcc - 1)("ID").ToString()) ' Old: DisplayAccessionRecord(RsA.Fields("ID").Value)
    End Sub
    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click

        Dim sSQL As String = ""
        If (IsDate(txtDateFrom.Text) Or IsDate(txtDateTo.Text) Or
        txtAccFrom.Text <> "" Or txtAccTo.Text <> "") Then
            sSQL = "Select * from Requisitions where Received <> 0"
            '
            If IsDate(txtDateFrom.Text) And Not IsDate(txtDateTo.Text) Then
                sSQL += " and AccessionDate between '" & Format(CDate(txtDateFrom.Text), SystemConfig.DateFormat) &
                "' and '" & Format(CDate(txtDateFrom.Text), SystemConfig.DateFormat) & " 23:59:00'"
            ElseIf Not IsDate(txtDateFrom.Text) And IsDate(txtDateTo.Text) Then
                sSQL += " and AccessionDate between '" & Format(CDate(txtDateTo.Text), SystemConfig.DateFormat) &
                 "' and '" & Format(CDate(txtDateTo.Text), SystemConfig.DateFormat) & " 23:59:00'"
            ElseIf IsDate(txtDateFrom.Text) And IsDate(txtDateTo.Text) Then
                sSQL += " and AccessionDate between '" & Format(CDate(txtDateFrom.Text), SystemConfig.DateFormat) &
                 "' and '" & Format(CDate(txtDateTo.Text), SystemConfig.DateFormat) & " 23:59:00'"
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                sSQL += " and ID = " & Val(txtAccFrom.Text)
            ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                sSQL += " and ID = " & Val(txtAccTo.Text)
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                If Val(txtAccFrom.Text) < Val(txtAccTo.Text) Then
                    sSQL += " and ID >= " & Val(txtAccFrom.Text) & " and ID <= " _
                    & Val(txtAccTo.Text)
                ElseIf Val(txtAccFrom.Text) > Val(txtAccTo.Text) Then
                    sSQL += " and ID <= " & Val(txtAccFrom.Text) & " and ID >= " _
                    & Val(txtAccTo.Text)
                Else
                    sSQL += " and ID = " & Val(txtAccFrom.Text)
                End If
            End If
            If sSQL <> "" Then sSQL += " order by ID"

            mainQuery = sSQL
            FillData(mainQuery)

            'If RsA.State <> 0 Then RsA.Close()
            'Dim CNA As New ADODB.Connection
            'CNA.Open(connstring)
            'RsA.Open(sSQL, CNA, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            'If Not RsA.BOF Then
            '    RsA.MoveLast()
            '    RsA.MoveFirst()
            'End If
            'Accessions = RsA.RecordCount
            'If Accessions > 0 Then
            '    CurrAcc = 1
            '    btnNext.Enabled = True
            '    btnLast.Enabled = True
            '    DisplayAccessionRecord(RsA.Fields("ID").Value)
            '    txtNavStatus.Text = CurrAcc & " of " & Accessions
            'Else
            '    txtNavStatus.Text = "" : btnFirst.Enabled = False : btnPrevious.Enabled = False
            '    btnNext.Enabled = False : btnLast.Enabled = False
            'End If

            ' If RsA.State <> 0 Then RsA.Close()
            If dtRecords.Rows.Count > 0 Then
                ' Dim CNA As New ADODB.Connection
                ' CNA.Open(connstring)
                ' RsA.Open(sSQL, CNA, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
                ' If Not RsA.BOF Then
                '     RsA.MoveLast()
                '     RsA.MoveFirst()
                ' End If

                Accessions = dtRecords.Rows.Count ' Old: Accessions = RsA.RecordCount
                If Accessions > 0 Then
                    CurrAcc = 1 ' Old: CurrAcc = 1
                    btnNext.Enabled = True ' Old: btnNext.Enabled = True
                    btnLast.Enabled = True ' Old: btnLast.Enabled = True
                    DisplayAccessionRecord(dtRecords.Rows(CurrAcc - 1)("ID").ToString()) ' Old: DisplayAccessionRecord(RsA.Fields("ID").Value)
                    txtNavStatus.Text = CurrAcc & " of " & Accessions ' Old: txtNavStatus.Text = CurrAcc & " of " & Accessions
                Else
                    txtNavStatus.Text = "" ' Old: txtNavStatus.Text = ""
                    btnFirst.Enabled = False ' Old: btnFirst.Enabled = False
                    btnPrevious.Enabled = False ' Old: btnPrevious.Enabled = False
                    btnNext.Enabled = False ' Old: btnNext.Enabled = False
                    btnLast.Enabled = False ' Old: btnLast.Enabled = False
                End If
            Else
                txtNavStatus.Text = "" ' Old: txtNavStatus.Text = ""
                btnFirst.Enabled = False ' Old: btnFirst.Enabled = False
                btnPrevious.Enabled = False ' Old: btnPrevious.Enabled = False
                btnNext.Enabled = False ' Old: btnNext.Enabled = False
                btnLast.Enabled = False ' Old: btnLast.Enabled = False
            End If

            'CNA.Close()
            'CNA = Nothing
            txtAccFrom.Text = "" : txtAccTo.Text = "" : txtDateFrom.Text = "" : txtDateTo.Text = ""
        End If
    End Sub

    Private Sub FillData(sSQL As String)
        Using CNB As New SqlConnection(connString) ' Old: Dim CNB As New ADODB.Connection
            CNB.Open() ' Old: CNB.Open(connstring)

            ' Old: CNB.CommandTimeout = 120
            ' ADO.NET sets command timeout directly in the SqlCommand below; no explicit timeout needed on SqlConnection.

            Using cmd As New SqlCommand(sSQL, CNB) ' Old: RsA.Open(sSQL, CNB, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
                cmd.CommandTimeout = 120

                Dim adapter As New SqlDataAdapter(cmd) ' No equivalent in ADODB; SqlDataAdapter is used to fill DataTable.

                adapter.Fill(dtRecords) ' Old: RsA.Open(sSQL, CNB, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

            End Using
        End Using
    End Sub

    Private Sub btnPrevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevious.Click
        ' RsA.MovePrevious()
        If CurrAcc > 1 Then ' Ensure we are not at the beginning of the DataTable
            CurrAcc -= 1 ' Old: CurrAcc -= 1
            btnNext.Enabled = True ' Old: btnNext.Enabled = True
            btnLast.Enabled = True ' Old: btnLast.Enabled = True

            If CurrAcc = 1 Then ' Equivalent to RsA.BOF (Beginning of File)
                ' RsA.MoveFirst()
                btnPrevious.Enabled = False ' Old: btnPrevious.Enabled = False
                btnFirst.Enabled = False ' Old: btnFirst.Enabled = False
            End If

            ' Display the current record
            txtNavStatus.Text = CurrAcc & " of " & Accessions ' Old: txtNavStatus.Text = CurrAcc & " of " & Accessions
            DisplayAccessionRecord(dtRecords.Rows(CurrAcc - 1)("ID").ToString()) ' Old: DisplayAccessionRecord(RsA.Fields("ID").Value)
        End If
    End Sub

    Private Sub btnFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirst.Click
        ' RsA.MoveFirst()
        CurrAcc = 1 ' Old: CurrAcc = 1
        btnNext.Enabled = True ' Old: btnNext.Enabled = True
        btnLast.Enabled = True ' Old: btnLast.Enabled = True
        btnPrevious.Enabled = False ' Old: btnPrevious.Enabled = False
        btnFirst.Enabled = False ' Old: btnFirst.Enabled = False
        txtNavStatus.Text = CurrAcc & " of " & Accessions ' Old: txtNavStatus.Text = CurrAcc & " of " & Accessions
        DisplayAccessionRecord(dtRecords.Rows(CurrAcc - 1)("ID").ToString()) ' Old: DisplayAccessionRecord(RsA.Fields("ID").Value)
    End Sub
    Private Sub txtDateFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtDateFrom.BackColor = FCOLOR
    End Sub

    Private Sub txtDateFrom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDateFrom.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtDateFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtDateFrom.BackColor = NFCOLOR
        If UserEnteredText(txtDateFrom) <> "" Then
            If IsDate(txtDateFrom.Text) = False Then
                MsgBox("Invalid date")
                txtDateFrom.Text = ""
            Else
                txtAccFrom.Text = ""
                txtAccTo.Text = ""
            End If
        End If
    End Sub

    Private Sub txtDateTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtDateTo.BackColor = FCOLOR
    End Sub

    Private Sub txtDateTo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDateTo.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtDateTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtDateTo.BackColor = NFCOLOR
        If UserEnteredText(txtDateTo) <> "" Then
            If IsDate(txtDateTo.Text) = False Then
                MsgBox("Invalid date")
                txtDateTo.Text = ""
            Else
                txtAccFrom.Text = ""
                txtAccTo.Text = ""
            End If
        End If
    End Sub

    Private Sub txtAccFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtAccFrom.BackColor = FCOLOR
    End Sub

    Private Sub txtAccFrom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        ElseIf e.KeyCode = Keys.ControlKey Then


        End If
    End Sub

    Private Sub txtAccFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Numerals(sender, e)
    End Sub

    Private Sub txtAccFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtAccFrom.BackColor = NFCOLOR
        If txtAccFrom.Text <> "" Then
            txtDateFrom.Text = ""
            txtDateTo.Text = ""
        End If
    End Sub

    Private Sub txtAccTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtAccTo.BackColor = FCOLOR
    End Sub

    Private Sub txtAccTo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtAccTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Numerals(sender, e)
    End Sub

    Private Sub txtAccTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtAccTo.BackColor = NFCOLOR
        If txtAccTo.Text <> "" Then
            txtDateFrom.Text = ""
            txtDateTo.Text = ""
        End If
    End Sub

    Private Sub txtQty_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtQty.BackColor = NFCOLOR
    End Sub

    Private Sub btnAddSrc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Up Then  'up
            SendKeys.Send("+{TAB}")
        ElseIf e.KeyCode = Keys.PageDown Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub dgvDxs_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvDxs.KeyDown
        If e.KeyCode = Keys.PageDown Then
            TabControl1.SelectTab("tpOrders")
        ElseIf e.KeyCode = Keys.PageUp Then
            TabControl1.SelectTab("tpProvider")
        End If
    End Sub

    Private Sub dgvTGPMarked_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvTGPMarked.KeyDown
        If e.KeyCode = Keys.PageDown Then
            TabControl1.SelectTab("tpReports")
        ElseIf e.KeyCode = Keys.PageUp Then
            TabControl1.SelectTab("tpPatient")
        End If
    End Sub

    Private Sub btnRptAdd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.PageDown Then
            TabControl1.SelectTab("tpBilling")
        ElseIf e.KeyCode = Keys.PageUp Then
            TabControl1.SelectTab("tpOrders")
        End If
    End Sub

    Private Sub chkRptComplete_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        chkRptComplete.BackColor = FCOLOR
    End Sub

    Private Sub chkRptComplete_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub btn122_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn122.Click
        TabControl1.SelectTab("tpPatient")
    End Sub

    Private Sub btn120_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn120.Click
        TabControl1.SelectTab("tpSpecimen")
    End Sub

    Private Sub chkVerbal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkVerbal.CheckedChanged
        Dim i As Integer
        For i = 0 To dgvTGPMarked.RowCount - 1
            If Not dgvTGPMarked.Rows(i).Cells(0).Value Is Nothing _
            AndAlso dgvTGPMarked.Rows(i).Cells(0).Value.ToString <> "" Then
                dgvTGPMarked.Rows(i).Cells(5).Value = chkVerbal.Checked
            End If
        Next
    End Sub

    'Private Sub dgvTGPMarked_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTGPMarked.CellValueChanged
    '    If e.ColumnIndex = 5 Then   'Verbal
    '        Dim VCt As Integer = 0 : Dim NVCt As Integer = 0 : Dim TGPC As Integer = 0
    '        Dim i As Integer
    '        For i = 0 To dgvTGPMarked.RowCount - 1
    '            If Not dgvTGPMarked.Rows(i).Cells(0).Value Is Nothing _
    '            AndAlso dgvTGPMarked.Rows(i).Cells(0).Value.ToString <> "" Then
    '                If dgvTGPMarked.Rows(i).Cells(5).Value = 0 Then
    '                    NVCt += 1
    '                Else
    '                    VCt += 1
    '                End If
    '                TGPC += 1
    '            End If
    '        Next
    '        If VCt = TGPC Then
    '            chkVerbal.Checked = True
    '        ElseIf NVCt = TGPC Then
    '            chkVerbal.Checked = False
    '        Else
    '            chkVerbal.CheckState = CheckState.Indeterminate
    '        End If
    '        SendKeys.Send("{TAB}")
    '    End If
    'End Sub 

    Private Sub dgvDxs_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDxs.CellContentClick
        If e.ColumnIndex = 1 Then
            If e.RowIndex <> -1 Then
                TCode = dgvDxs.Rows(e.RowIndex).Cells(0).Value
                TCode = frmDiagnosis.ShowDialog
                If TCode <> "" Then
                    dgvDxs.Rows(e.RowIndex).Cells(0).Value = TCode
                    TCode = ""
                End If
            End If
        ElseIf e.ColumnIndex = 2 Then

        End If
    End Sub

    Private Sub txtTime_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTime.Validated
        txtDrawnTime.Text = txtTime.Text
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedTab.Name = "tpOrders" Then
            If ThisUser.Supervisor Or ThisUser.Director Then
                chkProfile.Enabled = True
            Else
                chkProfile.Enabled = False
            End If
        End If
    End Sub

    Private Sub UpdatePhlebotomy()
        Dim VPTGP As String = SystemConfig.InPhlebTGP
        Dim HCTGP As String = SystemConfig.HPhlebTGP
        Dim CCTGP As String = SystemConfig.CPhlebTGP
        If VPTGP <> "" And HCTGP <> "" And CCTGP <> "" Then 'Has entry
            If chkPhlebotomy.Checked = True And chkHomeBound.Checked _
            = False And chkCare.Checked = False Then
                RemoveBillingTGPs()
                OrderTGP(Val(VPTGP))
            ElseIf chkPhlebotomy.Checked = True And chkHomeBound.Checked _
            = True And chkCare.Checked = False Then
                RemoveBillingTGPs()
                OrderTGP(Val(HCTGP))
            ElseIf chkPhlebotomy.Checked = True And chkHomeBound.Checked _
            = False And chkCare.Checked = True Then
                RemoveBillingTGPs()
                OrderTGP(Val(CCTGP))
            Else
                RemoveBillingTGPs()
            End If
        End If
    End Sub

    Private Sub RemoveBillingTGPs()
        Dim i As Integer
        For i = 0 To dgvTGPMarked.RowCount - 1
            If dgvTGPMarked.Rows(i).Cells(0).Value = SystemConfig.InPhlebTGP _
            Or dgvTGPMarked.Rows(i).Cells(0).Value = SystemConfig.HPhlebTGP _
            Or dgvTGPMarked.Rows(i).Cells(0).Value = SystemConfig.CPhlebTGP Then
                dgvTGPMarked.Rows(i).Cells(0).Value = ""
                dgvTGPMarked.Rows(i).Cells(2).Value = ""
                dgvTGPMarked.Rows(i).Cells(3).Value =
                System.Drawing.Image.FromFile(Application.StartupPath &
                "\Images\blank.ico")
                Exit For
            End If
        Next
    End Sub


    Private Sub OrderTGP(ByVal TGPID As Integer)
        Dim i As Integer
        Dim TGPType As String = GetTGPType(TGPID)
        For i = 0 To dgvTGPMarked.RowCount - 1
            If dgvTGPMarked.Rows(i).Cells(0).Value Is Nothing Then
                dgvTGPMarked.Rows(i).Cells(0).Value = TGPID.ToString
                dgvTGPMarked.Rows(i).Cells(2).Value = GetTGPName(TGPID)
                If TGPType = "T" Then
                    dgvTGPMarked.Rows(i).Cells(3).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath &
                    "\Images\Test.ico")
                ElseIf TGPType = "G" Then
                    dgvTGPMarked.Rows(i).Cells(3).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath &
                    "\Images\Group.ico")
                Else
                    dgvTGPMarked.Rows(i).Cells(3).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath &
                    "\Images\Profile.ico")
                End If
                Exit For
            ElseIf dgvTGPMarked.Rows(i).Cells(0).Value.ToString = "" Then
                dgvTGPMarked.Rows(i).Cells(0).Value = TGPID.ToString
                dgvTGPMarked.Rows(i).Cells(2).Value = GetTGPName(TGPID)
                If TGPType = "T" Then
                    dgvTGPMarked.Rows(i).Cells(3).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath &
                    "\Images\Test.ico")
                ElseIf TGPType = "G" Then
                    dgvTGPMarked.Rows(i).Cells(3).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath &
                    "\Images\Group.ico")
                Else
                    dgvTGPMarked.Rows(i).Cells(3).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath &
                    "\Images\Profile.ico")
                End If
                Exit For
            End If
        Next
    End Sub

    Private Sub chkPhlebotomy_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPhlebotomy.CheckedChanged
        If chkPhlebotomy.Checked = False Then
            chkHomeBound.Checked = False
            chkCare.Checked = False
        End If
        UpdatePhlebotomy()
    End Sub

    Private Sub chkHomeBound_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkHomeBound.CheckedChanged
        If chkHomeBound.Checked = True Then
            chkPhlebotomy.Checked = True
            chkCare.Checked = False
        End If
        UpdatePhlebotomy()
    End Sub

    Private Sub frmRequisitions_ResizeBegin(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeBegin
        origWidth = Me.Width
        origHeight = Me.Height
    End Sub

    Private Sub frmRequisitions_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        MeResize(Me, origWidth, origHeight)
    End Sub

    Private Sub chkCare_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCare.CheckedChanged
        If chkCare.Checked = True Then
            chkPhlebotomy.Checked = True
            chkHomeBound.Checked = False
        End If
        UpdatePhlebotomy()
    End Sub

    'Private Sub lstProviders_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles lstProviders.ItemCheck
    'Dim i As Integer
    'Dim ItemX As MyList
    'For i = 0 To lstProviders.Items.Count - 1
    '    If lstProviders.GetItemChecked(i) = True Then
    '        'lblOrderer.BackColor = Color.PaleGreen
    '        dgvRptProviders.Rows.Clear()
    '        ItemX = lstProviders.CheckedItems(0)
    '        Dim RptSetting() As String = GetOrdAtndSetting(Val(txtOrdID.Text), ItemX.ItemData)
    '        Update_Rpt_Sec(RptSetting)
    '        'Else
    '        '    lstProviders.SetItemChecked(i, False)
    '        'lblOrderer.BackColor = Color.PaleGreen
    '    End If
    'Next
    'End Sub

    Private Sub lstProviders_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstProviders.SelectedIndexChanged
        Dim i As Integer
        Dim ItemX As MyList = lstProviders.Items(lstProviders.SelectedIndex)
        lstProviders.SetItemChecked(lstProviders.SelectedIndex, True)
        MyAttender = ItemX.ItemData
        dgvRptProviders.Rows.Clear()
        Dim RptSetting() As String = GetOrdAtndSetting(Val(txtOrdID.Text), ItemX.ItemData)
        Update_Rpt_Sec(RptSetting)
        For i = 0 To lstProviders.Items.Count - 1
            If lstProviders.SelectedIndex <> i Then _
            lstProviders.SetItemChecked(i, False)
        Next
        If lstProviders.CheckedItems.Count > 0 Then
            'ItemX = lstProviders.CheckedItems(0)
            Dim Alert As String = GetProviderAccAlert(ItemX.ItemData)
            If Alert <> "" Then
                DisplayProviderAlert(Alert)
            Else
                If Not frmProviderAlert Is Nothing Then frmProviderAlert.Close()
            End If
        End If
        Update_Provider_Status()
        UpdateRequisitionProgress()
    End Sub

    Private Sub btnSwitchCarriers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSwitchCarriers.Click
        If txtPatientID.Text <> "" And txtAccID.Text <> "" Then
            If txtPPolicy.Text <> "" And txtPInsID.Text <> "" And
            txtSInsID.Text <> "" And txtSPolicy.Text <> "" Then
                Dim PInsID As Long = Val(txtPInsID.Text)
                Dim SInsiD As Long = Val(txtSInsID.Text)
                ExecuteSqlProcedure("Update Requisitions Set PrimePayer_ID = " &
                SInsiD & ", SecondPayer_ID = " & PInsID &
                " where ID = " & Val(txtAccID.Text))
                ExecuteSqlProcedure("Update Req_Coverage Set Preference = 'P' where " &
                "Accession_ID = " & Val(txtAccID.Text) & " and Payer_ID = " &
                SInsiD)
                ExecuteSqlProcedure("Update Req_Coverage Set Preference = 'S' where " &
                "Accession_ID = " & Val(txtAccID.Text) & " and Payer_ID = " &
                PInsID)
                ExecuteSqlProcedure("Update Coverages Set Preference = 'P' where " &
                "Patient_ID = " & Val(txtPatientID.Text) & " and Insurance_ID = " &
                SInsiD)
                ExecuteSqlProcedure("Update Coverages Set Preference = 'S' where " &
                "Patient_ID = " & Val(txtPatientID.Text) & " and Insurance_ID = " &
                PInsID)
                DisplayPrimeIns(Val(txtAccID.Text), SInsiD)
                'DisplayPrimInsured(SInsured)
                DisplaySecondIns(Val(txtAccID.Text), PInsID)
                'DisplaySecondInsured(PInsured)
            End If
        End If
    End Sub

    Private Sub txtPInsID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPInsID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtPInsID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPInsID.Validated
        If txtPInsID.Text <> "" Then
            If txtPInsID.Text <> txtSInsID.Text Then
                Dim cngp As New SqlConnection(connString)
                cngp.Open()
                Dim cmdgp As New SqlCommand("Select * from " &
                "Payers where Active <> 0 And ID = " & Val(txtPInsID.Text), cngp)
                cmdgp.CommandType = Data.CommandType.Text
                Dim drgp As SqlDataReader = cmdgp.ExecuteReader
                If drgp.HasRows Then
                    While drgp.Read
                        txtPInsName.Text = drgp("PayerName")
                        If cmbPRelation.SelectedIndex = -1 Then _
                        cmbPRelation.SelectedIndex = 0
                        If SystemConfig.ParInHouse = True Then
                            If txtInEditReason.Text = "" Then _
                        chkInHouse.Checked = drgp("IsPar")
                        End If
                    End While
                Else
                    MsgBox("Invalid ID", MsgBoxStyle.Critical, "Prolis")
                    txtPInsID.Text = ""
                    txtPInsID.Focus()
                End If
                cngp.Close()
                cngp = Nothing
            Else
                MsgBox("Primary And secondary coverages need to be different.", MsgBoxStyle.Critical, "Prolis")
                txtPInsID.Text = ""
                txtPInsID.Focus()
            End If
        Else
            txtPInsName.Text = ""
        End If
        Update_Billing_Status()
        UpdateRequisitionProgress()
    End Sub

    Private Sub txtSInsID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSInsID.Validated
        If txtSInsID.Text <> "" Then
            If txtSInsID.Text <> txtPInsID.Text Then
                Dim cngp As New SqlConnection(connString)
                cngp.Open()
                Dim cmdgp As New SqlCommand("Select PayerName from " &
                "Payers where Active <> 0 And ID = " & Val(txtSInsID.Text), cngp)
                cmdgp.CommandType = Data.CommandType.Text
                Dim drgp As SqlDataReader = cmdgp.ExecuteReader
                If drgp.HasRows Then
                    While drgp.Read
                        txtSInsName.Text = drgp("PayerName")
                        If cmbSRelation.SelectedIndex = -1 Then _
                        cmbSRelation.SelectedIndex = 0
                    End While
                Else
                    MsgBox("Invalid ID", MsgBoxStyle.Critical, "Prolis")
                    txtSInsID.Text = ""
                    txtSInsID.Focus()
                End If
                cngp.Close()
                cngp = Nothing
            Else
                MsgBox("Primary And secondary coverages need to be different.", MsgBoxStyle.Critical, "Prolis")
                txtSInsID.Text = ""
                txtSInsID.Focus()
            End If
        Else
            txtSInsName.Text = ""
        End If
        Update_Billing_Status()
        UpdateRequisitionProgress()
    End Sub

    Private Sub txtSPolicy_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSPolicy.GotFocus
        txtSPolicy.BackColor = FCOLOR
    End Sub

    Private Sub txtSPolicy_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSPolicy.LostFocus
        txtSPolicy.BackColor = NFCOLOR
        If txtSPolicy.Text <> "" Then
            If cmbSRelation.SelectedIndex = -1 Then _
            cmbSRelation.SelectedIndex = 0
            Update_Billing_Status()
            UpdateRequisitionProgress()
        End If
    End Sub

    Private Sub txtEMRNo_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
        Update_Patient_Status()
    End Sub

    Private Sub chkInHouse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkInHouse.Click
        'Dim Reason As String = frmInHouseOverride.ShowDialog
        If chkInHouse.Checked = True Then
            chkInHouse.Text = "Yes"
        Else
            chkInHouse.Text = "No"
        End If
    End Sub

    Private Sub txtPSubDOB_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPSubDOB.LostFocus
        UpdatePInsured()
        Update_Billing_Status()
    End Sub

    Private Sub UpdateSInsured()
        If Trim(txtSSubLName.Text) <> "" And Trim(txtSSubFName.Text) <> "" And
        IsDate(txtSSubDOB.Text) And cmbSSubSex.SelectedIndex <> -1 Then
            Dim SSubID As Long = GetPatientIDbyNames(Trim(txtSSubLName.Text),
            Trim(txtSSubFName.Text), CDate(txtSSubDOB.Text), Microsoft.VisualBasic.Left(cmbSSubSex.SelectedItem.ToString, 1))
            '
            Dim cnis As New SqlConnection(connString)
            cnis.Open()
            Dim cmdupsert As New SqlCommand("Patients_SP", cnis)
            cmdupsert.CommandType = Data.CommandType.StoredProcedure
            cmdupsert.Parameters.AddWithValue("@act", "Upsert")
            cmdupsert.Parameters.AddWithValue("@ID", SSubID)
            cmdupsert.Parameters.AddWithValue("@LastName", Trim(txtSSubLName.Text))
            cmdupsert.Parameters.AddWithValue("@FirstName", Trim(txtSSubFName.Text))
            cmdupsert.Parameters.AddWithValue("@MiddleName", Trim(txtSSubMName.Text))
            cmdupsert.Parameters.AddWithValue("@Sex", Microsoft.VisualBasic.Left(cmbSSubSex.SelectedItem.ToString, 1))
            cmdupsert.Parameters.AddWithValue("@DOB", CDate(txtSSubDOB.Text))
            cmdupsert.Parameters.AddWithValue("@Ethnicity", "Unknown")
            cmdupsert.Parameters.AddWithValue("@SSN", SSNNeat(txtSSubSSN.Text))
            cmdupsert.Parameters.AddWithValue("@IsAlive", 1)
            cmdupsert.Parameters.AddWithValue("@DeathDate", DBNull.Value)
            If Trim(txtSSubAdd1.Text) <> "" And Trim(txtSSubCity.Text) <> "" And
            Trim(txtSSubState.Text) <> "" And Trim(txtSSubZip.Text) <> "" Then
                cmdupsert.Parameters.AddWithValue("@Address_ID", GetAddressID(Trim(txtSSubAdd1.Text),
                Trim(txtSSubAdd2.Text), Trim(txtSSubCity.Text), Trim(txtSSubState.Text),
                Trim(txtSSubZip.Text), Trim(txtSSubCountry.Text)))
            Else
                cmdupsert.Parameters.AddWithValue("@Address_ID", DBNull.Value)
            End If
            cmdupsert.Parameters.AddWithValue("@HomePhone", PhoneNeat(txtSSubPhone.Text))
            cmdupsert.Parameters.AddWithValue("@WorkPhone", "")
            cmdupsert.Parameters.AddWithValue("@Email", Trim(txtSSubEmail.Text))
            cmdupsert.Parameters.AddWithValue("@Password", "")
            cmdupsert.Parameters.AddWithValue("@SecretQ", "")
            cmdupsert.Parameters.AddWithValue("@SecretA", "")
            cmdupsert.Parameters.AddWithValue("@Fax", "")
            cmdupsert.Parameters.AddWithValue("@Cell", "")
            cmdupsert.Parameters.AddWithValue("@Employer_ID", "")
            cmdupsert.Parameters.AddWithValue("@Note", "")
            cmdupsert.ExecuteNonQuery()
            cmdupsert.Dispose()
            cmdupsert = Nothing
            cnis.Close()
            cnis = Nothing
            '
            txtSSubID.Text = SSubID
        End If
    End Sub

    Private Sub UpdatePInsured()
        If Trim(txtPSubLName.Text) <> "" And Trim(txtPSubFName.Text) <> "" And
        IsDate(txtPSubDOB.Text) And cmbPSubSex.SelectedIndex <> -1 Then
            Dim PSubID As Long = GetPatientIDbyNames(Trim(txtPSubLName.Text),
            Trim(txtPSubFName.Text), CDate(txtPSubDOB.Text), Microsoft.VisualBasic.Left(cmbPSubSex.SelectedItem.ToString, 1))
            '
            Dim cnip As New SqlConnection(connString)
            cnip.Open()
            Dim cmdupsert As New SqlCommand("Patients_SP", cnip)
            cmdupsert.CommandType = Data.CommandType.StoredProcedure
            cmdupsert.Parameters.AddWithValue("@act", "Upsert")
            cmdupsert.Parameters.AddWithValue("@ID", PSubID)
            cmdupsert.Parameters.AddWithValue("@LastName", Trim(txtPSubLName.Text))
            cmdupsert.Parameters.AddWithValue("@FirstName", Trim(txtPSubFName.Text))
            cmdupsert.Parameters.AddWithValue("@MiddleName", Trim(txtPSubMName.Text))
            cmdupsert.Parameters.AddWithValue("@Sex", Microsoft.VisualBasic.Left(cmbPSubSex.SelectedItem.ToString, 1))
            cmdupsert.Parameters.AddWithValue("@DOB", CDate(txtPSubDOB.Text))
            cmdupsert.Parameters.AddWithValue("@Ethnicity", "Unknown")
            cmdupsert.Parameters.AddWithValue("@SSN", SSNNeat(txtPSubSSN.Text))
            cmdupsert.Parameters.AddWithValue("@IsAlive", 1)
            cmdupsert.Parameters.AddWithValue("@DeathDate", DBNull.Value)
            If Trim(txtPSubAdd1.Text) <> "" And Trim(txtPSubCity.Text) <> "" And
            Trim(txtPSubState.Text) <> "" And Trim(txtPSubZip.Text) <> "" Then
                cmdupsert.Parameters.AddWithValue("@Address_ID", GetAddressID(Trim(txtPSubAdd1.Text),
                Trim(txtPSubAdd2.Text), Trim(txtPSubCity.Text), Trim(txtPSubState.Text),
                Trim(txtPSubZip.Text), Trim(txtPSubCountry.Text)))
            Else
                cmdupsert.Parameters.AddWithValue("@Address_ID", DBNull.Value)
            End If
            cmdupsert.Parameters.AddWithValue("@HomePhone", PhoneNeat(txtPsubHPhone.Text))
            cmdupsert.Parameters.AddWithValue("@WorkPhone", "")
            cmdupsert.Parameters.AddWithValue("@Email", Trim(txtPSubEmail.Text))
            cmdupsert.Parameters.AddWithValue("@Password", "")
            cmdupsert.Parameters.AddWithValue("@SecretQ", "")
            cmdupsert.Parameters.AddWithValue("@SecretA", "")
            cmdupsert.Parameters.AddWithValue("@Fax", "")
            cmdupsert.Parameters.AddWithValue("@Cell", "")
            cmdupsert.Parameters.AddWithValue("@Employer_ID", "")
            cmdupsert.Parameters.AddWithValue("@Note", "")
            cmdupsert.Parameters.AddWithValue("@Race_ID", 0)
            cmdupsert.ExecuteNonQuery()
            cmdupsert.Dispose()
            cmdupsert = Nothing
            cnip.Close()
            cnip = Nothing
            '
            txtPSubID.Text = PSubID.ToString
        End If
    End Sub

    Private Sub tpBilling_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpBilling.LostFocus
        UpdatePInsured()
        UpdateSInsured()
    End Sub

    Private Sub chkGIsIndividual_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGIsIndividual.CheckedChanged
        txtGID.Text = ""
        If chkGIsIndividual.Checked = True Then
            chkGIsIndividual.Text = "Individual"
            lblGLName.Text = "Last Name"
            lblGFName.Text = "First Name"
            txtGFName.Enabled = True
            lblGMName.Text = "Middle Name"
            txtGMName.Enabled = True
            lblGSex.Text = "Gender"
            cmbGSex.Enabled = True
            lblGDOB.Text = "D.O.B."
            txtGDOB.Enabled = True
            lblGSSN.Text = "SSN"
            txtGSSN.Mask = "000-00-0000"
            cmbGRelation.Items.Clear()
            cmbGRelation.Items.Add("Spouse")
            cmbGRelation.Items.Add("Son/Daughter")
            cmbGRelation.Items.Add("Other(Dependent)")
        Else
            chkGIsIndividual.Text = "Entity"
            lblGLName.Text = "Entity Name"
            lblGFName.Text = ""
            txtGFName.Text = ""
            txtGFName.Enabled = False
            lblGMName.Text = ""
            txtGMName.Text = ""
            txtGMName.Enabled = False
            lblGSex.Text = ""
            cmbGSex.SelectedIndex = -1
            cmbGSex.Enabled = False
            lblGDOB.Text = ""
            txtGDOB.Text = ""
            txtGDOB.Enabled = False
            lblGSSN.Text = "Emp ID"
            txtGSSN.Mask = ""
            cmbGRelation.Items.Clear()
            cmbGRelation.Items.Add("Employer")
            cmbGRelation.Items.Add("Other(Charity)")
        End If
    End Sub

    Private Sub btnGLookUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGLookUp.Click
        If chkGIsIndividual.Checked = True Then
            Dim GPatID As String = frmPatLookUp.ShowDialog
            If GPatID <> "" Then
                DisplayGuarantorByID(Val(GPatID))
            End If
        End If
    End Sub

    Private Sub DisplayGuarantorByID(ByVal GuarantorID As Long)
        ClearGuarantor()
        Dim sSQL As String = ""
        If chkGIsIndividual.Checked = True Then 'idividual
            sSQL = "Select * from Patients where ID = " & GuarantorID
        Else
            sSQL = "Select * from Employers where ID = " & GuarantorID
        End If
        Dim cndg As New SqlConnection(connString)
        cndg.Open()
        Dim cmddg As New SqlCommand(sSQL, cndg)
        cmddg.CommandType = Data.CommandType.Text
        Dim drdg As SqlDataReader = cmddg.ExecuteReader
        If drdg.HasRows Then
            While drdg.Read
                If chkGIsIndividual.Checked = True Then 'idividual
                    txtGID.Text = drdg("ID")
                    txtGLName_BSN.Text = drdg("LastName")
                    txtGFName.Text = drdg("FirstName")
                    If drdg("MiddleName") IsNot DBNull.Value Then txtGMName.Text = drdg("MiddleName")
                    For i As Integer = 0 To cmbGSex.Items.Count - 1
                        If drdg("Sex") = cmbGSex.Items(i).ToString.Substring(0, 1) Then
                            cmbGSex.SelectedIndex = i
                            Exit For
                        End If
                    Next
                    txtGDOB.Text = drdg("DOB")
                    If drdg("HomePhone") IsNot DBNull.Value Then txtGPhone.Text = drdg("HomePhone")
                    If drdg("Email") IsNot DBNull.Value Then txtGEmail.Text = drdg("Email")
                    If drdg("Address_ID") IsNot DBNull.Value Then
                        txtGAdd1.Text = GetAddress1(drdg("Address_ID"))
                        txtGAdd2.Text = GetAddress2(drdg("Address_ID"))
                        txtGCity.Text = GetAddressCity(drdg("Address_ID"))
                        txtGState.Text = GetAddressState(drdg("Address_ID"))
                        txtGZip.Text = GetAddressZip(drdg("Address_ID"))
                        txtGCountry.Text = GetAddressCountry(drdg("Address_ID"))
                    End If
                Else    'Employer
                    txtGLName_BSN.Text = drdg("Employer")
                    txtGPhone.Text = drdg("Phone")
                    txtGEmail.Text = drdg("Email")
                    If drdg("Address_ID") IsNot DBNull.Value Then
                        txtGAdd1.Text = GetAddress1(drdg("Address_ID"))
                        txtGAdd2.Text = GetAddress2(drdg("Address_ID"))
                        txtGCity.Text = GetAddressCity(drdg("Address_ID"))
                        txtGState.Text = GetAddressState(drdg("Address_ID"))
                        txtGZip.Text = GetAddressZip(drdg("Address_ID"))
                        txtGCountry.Text = GetAddressCountry(drdg("Address_ID"))
                    End If
                End If
            End While
        End If
        cndg.Close()
        cndg = Nothing
    End Sub

    Private Sub rbT_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbT.CheckedChanged
        If rbT.Checked = True Then
            TabControl2.Enabled = True
            cmbPRelation.SelectedIndex = 0
            cmbSRelation.SelectedIndex = 0
            grpPrimary.Enabled = True
            grpSecondary.Enabled = True
            grpPSubs.Enabled = True
            grpSSubs.Enabled = True
        End If
        Update_Billing_Status()
    End Sub

    Private Sub rbP_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbP.CheckedChanged
        If rbP.Checked = True Then
            TabControl2.Enabled = True
            ClearPrimary()
            ClearSecondary()
            cmbPSubSex.SelectedIndex = -1
            cmbSSubSex.SelectedIndex = -1
            grpPrimary.Enabled = False
            grpSecondary.Enabled = False
            grpPSubs.Enabled = False
            grpSSubs.Enabled = False
        End If
        Update_Billing_Status()
    End Sub

    Private Sub rbC_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbC.CheckedChanged
        If rbC.Checked = True Then
            ClearPrimary()
            ClearSecondary()
            cmbPSubSex.SelectedIndex = -1
            cmbSSubSex.SelectedIndex = -1
            TabControl2.Enabled = False
        End If
        Update_Billing_Status()
    End Sub

    Private Function IsMedValid(ByVal Med As String) As Boolean
        Dim MedValid As Boolean = False
        Dim PipedMeds As String = ""
        Dim cnmv As New SqlConnection(connString)
        cnmv.Open()
        Dim cmdmv As New SqlCommand("Select Name, " &
        "AlternateNames from Tests where IsActive <> 0", cnmv)
        cmdmv.CommandType = Data.CommandType.Text
        Dim drmv As SqlDataReader = cmdmv.ExecuteReader
        If drmv.HasRows Then
            While drmv.Read
                If drmv("Name") = Med Then
                    MedValid = True
                    Exit While
                Else
                    If drmv("AlternateNames") IsNot DBNull.Value _
                    AndAlso drmv("AlternateNames") <> "" Then
                        If Trim(PipedMeds) <> "" AndAlso
                        Microsoft.VisualBasic.Right(Trim(PipedMeds), 1) <> "|" Then PipedMeds += "|"
                        PipedMeds += drmv("AlternateNames")
                    End If
                End If
            End While
        End If
        cnmv.Close()
        cnmv = Nothing
        If MedValid = False Then
            Dim Meds() As String = Split(PipedMeds, "|")
            For i As Integer = 0 To Meds.Length - 1
                If Trim(Meds(i)) = Med Then
                    MedValid = True
                    Exit For
                End If
            Next
        End If
        Return MedValid
    End Function

    Private Sub dgvMeds_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMeds.CellClick
        If e.ColumnIndex = 1 Then   'lookup
            Dim MED As String = frmMedsLookUp.ShowDialog()
            If MED <> "" Then
                If IsMedDuplicate(MED, -1) Then
                    MsgBox("Duplicate entries not allowed", MsgBoxStyle.Critical, "Prolis")
                Else

                    dgvMeds.Rows.Add()
                    If e.RowIndex = -1 Then
                        dgvMeds.Rows(0).Cells(0).Value = MED
                    Else

                        dgvMeds.Rows(e.RowIndex).Cells(0).Value = MED
                    End If

                    If e.RowIndex = dgvMeds.RowCount - 1 Then
                        dgvMeds.Rows.Add()
                        SendKeys.Send("{ENTER}")
                    End If
                End If
            End If
        ElseIf e.ColumnIndex = 2 Then
            If dgvMeds.Rows().Count = 1 Then
                If dgvMeds.Rows(e.RowIndex).Cells(0).Value = Nothing Or dgvMeds.Rows(e.RowIndex).Cells(0).Value = "" Then
                    Return
                End If
            End If
            dgvMeds.Rows.RemoveAt(e.RowIndex)
            If dgvMeds.Rows().Count = 0 Then
                dgvMeds.Rows.Add()
            End If
        End If
    End Sub

    Private Sub dgvMeds_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMeds.CellEndEdit
        On Error Resume Next
        If e.ColumnIndex = 0 Then
            If Trim(dgvMeds.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) <> "" Then
                If IsMedDuplicate(dgvMeds.Rows(e.RowIndex).Cells(e.ColumnIndex).Value, e.RowIndex) Then
                    MsgBox("Duplicate Entry is not allowed.")
                    dgvMeds.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
                Else
                    If Not IsMedValid(Trim(dgvMeds.Rows(e.RowIndex).Cells(0).Value)) Then
                        dgvMeds.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
                        Dim MED As String = frmMedsLookUp.ShowDialog()
                        If MED <> "" Then
                            If IsMedDuplicate(MED, -1) Then
                                MsgBox("Duplicate entries not allowed", MsgBoxStyle.Critical, "Prolis")
                            Else
                                dgvMeds.Rows(e.RowIndex).Cells(0).Value = MED
                                If e.RowIndex = dgvMeds.RowCount - 1 Then
                                    dgvMeds.Rows.Add()
                                    SendKeys.Send("{ENTER}")
                                End If
                            End If
                        End If
                    Else
                        If e.RowIndex = dgvMeds.RowCount - 1 Then
                            dgvMeds.Rows.Add()
                            SendKeys.Send("{ENTER}")
                        End If
                    End If
                End If
            Else
                If e.RowIndex > 0 Then dgvMeds.Rows.RemoveAt(e.RowIndex)
            End If
        End If
    End Sub

    Private Function IsMedDuplicate(ByVal Med As String, ByVal rowindex As Integer) As Boolean
        Dim i As Integer
        Dim CT As Integer = 0
        For i = 0 To dgvMeds.RowCount - 1
            If rowindex < 0 OrElse rowindex <> i Then
                If dgvMeds.Rows(i).Cells(0).Value = Med Then CT += 1
            End If
        Next
        If CT > 0 Then
            IsMedDuplicate = True
        Else
            IsMedDuplicate = False
        End If
    End Function

    Private Sub ClearMeds()
        Dim i As Integer
        For i = dgvMeds.RowCount - 1 To 0 Step -1
            dgvMeds.Rows(i).Cells(0).Value = ""
            If i > 0 Then dgvMeds.Rows.RemoveAt(i)
        Next
        If dgvMeds.Rows.Count = 0 Then
            dgvMeds.Rows.Add()
        End If
    End Sub

    Private Sub dgvRptProviders_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvRptProviders.CellClick
        If e.RowIndex <> -1 Then
            If gBilled = False Then btnRemRpt.Enabled = True
        End If
    End Sub

    Private Sub btnRemRpt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemRpt.Click
        If dgvRptProviders.SelectedRows(0).Index <> -1 Then
            dgvRptProviders.Rows.Remove(dgvRptProviders.SelectedRows(0))
            btnRemRpt.Enabled = False
            Update_SourceString()
            Update_Rpt_Status()
            UpdateRequisitionProgress()
            RptDirty = True
        End If
    End Sub

    Private Sub btnRemRptAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemRptAll.Click
        dgvRptProviders.Rows.Clear()
        Update_SourceString()
        Update_Rpt_Status()
        UpdateRequisitionProgress()
        RptDirty = True
    End Sub

    Private Sub btnRptAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRptAdd.Click
        If txtAccID.Text <> "" And txtRptRcptID.Text <> "" Then

            Dim RptSetting() As String = {""}
            Dim SB As New StringBuilder
            SB.Append(txtRptRcptID.Text & "|" & txtRptRcptName.Text)
            SB.Append("|RDM_Auto=" & chkRDMAuto.Checked)
            SB.Append("|RptComplete=" & chkRptComplete.Checked)
            SB.Append("|RDM_Print=" & chkPrint.Checked)
            SB.Append("|RDM_Prolison=" & chkProlison.Checked)
            SB.Append("|RDM_Interface=" & chkInterface.Checked)
            If txtRPTFax.Text <> "" Then
                If chkRptFax.Checked = True Then
                    SB.Append("|RDM_Fax=True^" & txtRPTFax.Text)
                Else
                    SB.Append("|RDM_Fax=False")
                End If
            Else
                SB.Append("|RDM_Fax=False")
            End If
            If txtRptEmail.Text <> "" Then
                If chkrptEmail.Checked = True Then
                    SB.Append("|RDM_Email=True^" & txtRptEmail.Text)
                Else
                    SB.Append("|RDM_Email=False")
                End If
            Else
                SB.Append("|RDM_Email=False")
            End If

            'ProviderID, ProviderName, RptComplete, IsPrint, IsFax, Fax, IsEmail, Email, Comment
            RptSetting(0) = SB.ToString
            SB = Nothing



            'RptSetting(0) = txtRptRcptID.Text & "|" & txtRptRcptName.Text & "|" & IIf(chkRptComplete.Checked _
            '= True, "1|", "0|") & IIf(chkCourier.Checked = True, "1|", "0|") & IIf(chkRptFax.Checked = _
            'True, "1|", "0|") & txtRptFax.Text & "|" & IIf(chkrptEmail.Checked = True, "1|", "0|") & _
            'txtRptEmail.Text & "|" & IIf(chkCourier.Checked = True, "1", "")
            'RptSetting(0) = txtRptRcptID.Text & "|" & txtRptRcptName.Text & "|" & IIf(chkRptComplete.Checked _
            '= True, "1|    ", "0|") & IIf(chkPrint.Checked = True, "1|", "0|") & IIf(chkrptEmail.Checked = _
            'True, "1|", "0|") & txtRptEmail.Text & "|" & IIf(chkRptFax.Checked = True, "1|", "0|") & _
            'txtRPTFax.Text & "|" & IIf(chkPrint.Checked = True, "1", "")
            Update_Rpt_Sec(RptSetting)
            'Update_SourceString()
            Update_Rpt_Status()
            Prov_ProfileClear()
            UpdateRequisitionProgress()
            txtRptRcptID.Focus()
            RptDirty = True
        Else
            MsgBox("Required element 'report destination' missing!", MsgBoxStyle.Critical, "Prolis")
            If chkPrint.Checked = False Then
                chkPrint.Focus()
            ElseIf chkRptFax.Checked And txtRPTFax.Text = "" Then
                txtRPTFax.Focus()
            ElseIf chkrptEmail.Checked And txtRptEmail.Text = "" Then
                txtRptEmail.Focus()
            ElseIf chkRptFax.Checked = False Then
                chkRptFax.Focus()
            ElseIf chkrptEmail.Checked = False Then
                chkrptEmail.Focus()
            Else
                txtRptRcptID.Focus()
            End If
        End If
    End Sub

    Private Sub txtRptRcptID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtRptRcptID.BackColor = FCOLOR
    End Sub

    Private Sub txtRptRcptID_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Numerals(sender, e)
    End Sub

    Private Sub txtRptRcptID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtRptRcptID.BackColor = NFCOLOR
        If txtRptRcptID.Text <> "" Then
            DisplayProviderProfile(Val(txtRptRcptID.Text))
        Else
            Prov_ProfileClear()
        End If
        UpdateProv_Status()
    End Sub

    Private Sub btnRPTLookUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRPTLookUp.Click
        Dim ProvID As String = frmProviderLookup.ShowDialog()
        If ProvID <> "" Then DisplayProviderProfile(Val(ProvID))
    End Sub

    Private Sub chkRptComplete_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRptComplete.CheckedChanged
        If chkRptComplete.Checked = False Then
            chkRptComplete.Text = "Partial"
        Else
            chkRptComplete.Text = "Complete"
        End If
        UpdateProv_Status()
    End Sub

    Private Sub btnRefProf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefProf.Click
        If txtRptRcptID.Text <> "" Then
            DisplayProviderProfile(Val(txtRptRcptID.Text))
            UpdateProv_Status()
        End If
    End Sub

    Private Sub chkRDMAuto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRDMAuto.CheckedChanged
        If chkRDMAuto.Checked = False Then
            chkRDMAuto.Text = "Batch"
        Else
            chkRDMAuto.Text = "Auto"
        End If
    End Sub

    Private Sub chkrptEmail_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkrptEmail.CheckedChanged
        If chkrptEmail.Checked = False Then
            chkrptEmail.Text = "No"
            txtRptEmail.Text = ""
            txtRptEmail.Enabled = False
        Else
            chkrptEmail.Text = "Yes"
            txtRptEmail.Enabled = True
        End If
        UpdateProv_Status()
    End Sub

    Private Sub chkRptFax_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRptFax.CheckedChanged
        If chkRptFax.Checked = False Then
            chkRptFax.Text = "No"
            txtRPTFax.Text = ""
            txtRPTFax.Enabled = False
        Else
            chkRptFax.Text = "Yes"
            txtRPTFax.Enabled = True
        End If
        UpdateProv_Status()
    End Sub

    Private Sub chkPostPrePhleb_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPostPrePhleb.CheckedChanged
        If chkPostPrePhleb.Checked = False Then 'ACC
            chkPostPrePhleb.Text = "POST"
            btnAddSrc.Enabled = True
            txtRecDate.Text = Format(dtpDate.Value, SystemConfig.DateFormat)
            txtRecTime.Text = txtTime.Text
            txtRecDate.ReadOnly = False
            txtRecTime.ReadOnly = False
            lblSpecimen.BackColor = Color.PeachPuff
            QrChk.Enabled = True


        Else    'Order
            chkPostPrePhleb.Text = "PRE"
            btnAddSrc.Enabled = False
            txtRecDate.Text = ""
            txtRecTime.Text = ""
            txtRecDate.ReadOnly = True
            txtRecTime.ReadOnly = True
            QrChk.Enabled = False
            dgvSources.Rows.Clear()
            lblSpecimen.BackColor = Color.PaleGreen
        End If
    End Sub

    Private Sub dgvDxs_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvDxs.CellMouseUp
        If e.ColumnIndex = 0 Then   'dx
            If e.Button = System.Windows.Forms.MouseButtons.Right Then
                If Clipboard.ContainsText Then
                    Dim RowCount As Integer = dgvDxs.Rows.Count
                    dgvDxs.Rows.Clear()
                    dgvDxs.RowCount = RowCount
                    Dim Dxs() As String = Split(Clipboard.GetText, vbCrLf)
                    For i As Integer = 0 To IIf(Dxs.Length <= 12, Dxs.Length - 1, 11)
                        If Trim(Dxs(i)) <> "" Then
                            dgvDxs.Rows(i).Cells(0).Value = Trim(Dxs(i))
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Private Sub chkWorkman_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkWorkman.CheckedChanged
        If chkWorkman.Checked = False Then
            txtDOI.Text = ""
            txtDOI.ReadOnly = True
        Else
            txtDOI.ReadOnly = False
        End If
    End Sub

    Private Sub chkReject_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkReject.CheckedChanged
        If chkReject.Checked = False Then
            chkReject.Text = "Accepted"
            txtRejectReason.Text = ""
            lblRejectReason.Visible = False
            txtRejectReason.Visible = False
        Else
            chkReject.Text = "Rejected"
            lblRejectReason.Visible = True
            txtRejectReason.Visible = True
            txtRejectReason.Focus()
        End If
        UpdateRequisitionProgress()
    End Sub

    Private Sub txtRejectReason_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
        If txtRejectReason.Text = "" Then
            Dim Retval As Integer = MsgBox("In order to reject a sample, the reason must be provided." &
            vbCrLf & "Do you want to reject this sample?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo, "Prolis")
            If Retval = vbYes Then
                txtRejectReason.Focus()
            Else
                chkReject.Checked = False
            End If
        End If
        UpdateRequisitionProgress()
    End Sub

    Private Sub chkReject_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        'If chkReject.Checked = True And Trim(txtRejectReason.Text) = "" Then
        '    Dim retval As Integer = MsgBox("You marked the sample as rejected but did not provide the reason " & _
        '    "which is required." & vbCrLf & "Do you want to reject the sample?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo, "Prolis")
        '    If retval = vbYes Then
        '        txtRejectReason.Focus()
        '    Else
        '        chkReject.Checked = False
        '    End If
        'End If
    End Sub

    Private Sub cmbSex_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSex.SelectedIndexChanged
        If cmbSex.SelectedIndex <> -1 Then
            If cmbSex.SelectedItem.ToString.StartsWith("G") OrElse
            cmbSex.SelectedItem.ToString.StartsWith("N") Then
                txtTage.ReadOnly = False
            Else
                txtTage.Text = ""
                txtTage.ReadOnly = True
            End If
        End If
    End Sub

    Private Sub cmbSpecies_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSpecies.SelectedIndexChanged
        Dim ItemX As MyList = cmbSpecies.SelectedItem
        PopulateBreeds(ItemX.ItemData)
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

    Private Sub txtSSubDOB_LostFocus(sender As Object, e As EventArgs) Handles txtSSubDOB.LostFocus
        UpdateSInsured()
    End Sub



    Private Sub SendTocolorBtn_Click(sender As Object, e As EventArgs)
        'Call  to color interface
        If String.IsNullOrEmpty(barcode.Text) Then
            MessageBox.Show("Barcode number is required", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If barcode.Text.Count(Function(x) x = "-") > 1 Then
            barcode.Text = barcode.Text.Replace("-", "").Insert(1, "-")
        End If
        txtRequisition.Text = ""
        txtRejectReason.Text = ""

        Dim response = ColorAPI.Sample(barcode.Text, MyLab.ColorAPIStruct.Token_BaseUrl)
        If response.ToLower().Contains("not found") Then
            MessageBox.Show("Barcode does not exists in color's record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If response.Contains("is_approved_for_processing") Then
            Dim Success = JsonConvert.DeserializeObject(Of ColorRes)(response)
            txtRequisition.Text = Success.accession_number
            cmbSpecimenType.SelectedIndex = 1
            If Success.is_approved_for_processing Then
                chkReject.Checked = False
                txtWorkCmnt.Text = barcode.Text
            ElseIf Success.is_approved_for_processing = False Then
                chkReject.Checked = True
                txtRejectReason.Text = Success.rejection_reason
                'desroy
                Dim dstructed = ColorAPI.Destroy(barcode.Text, MyLab.ColorAPIStruct.Token_BaseUrl)
                If String.IsNullOrEmpty(dstructed) = False Then
                    MessageBox.Show(dstructed, "DestroyError", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
            End If
        Else
            Try
                Dim msg = JsonConvert.DeserializeObject(Of ColorMessage)(response)
                MessageBox.Show(msg.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception

            End Try

        End If

    End Sub

    Private Function NotIndustril(p1 As Integer) As Boolean
        If p1 = 1 Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub SendTocolorBtn_MouseHover(sender As Object, e As EventArgs)
        If barcode.Text.Count() > 4 Then
            If barcode.Text.Count(Function(x) x = "-") > 1 Then
                barcode.Text = barcode.Text.Replace("-", "").Insert(1, "-")
            End If
        End If

    End Sub

    Private Sub txtAccFrom_MouseClick(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Right Then

        End If
    End Sub

    Private Sub txtAccFrom_MouseEnter(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtAccFrom_MouseDown(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Right Then

            '  txtAccFrom.Text = Clipboard.GetText()
        End If
    End Sub

    Private Sub txtAccFrom_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles txtAccFrom.MouseDoubleClick
        txtAccFrom.Text = Clipboard.GetText()
    End Sub

    Private Sub tpSpecimen_Click(sender As Object, e As EventArgs) Handles tpSpecimen.Click

    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        If txtAccID.Text = "" Then

            Return

        End If
        If Not CommonData.IsExistsInTable("Requisitions", "ID", txtAccID.Text) Then
            MessageBox.Show("Accession not found.", "Prolis Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        If Val(txtLabels.Text) > 0 Then
            Dim Printer As String = GetLabelPrinterName()
            If Not ThisUser.SpecificPrinter = "Default" Then
                Printer = ThisUser.SpecificPrinter
            End If
            If Printer <> "" Then
                PrintLabels(Printer, txtAccID.Text, CInt(txtLabels.Text),,, 0)
            Else
                MsgBox("Please set Label printer from System Config. " &
                "number: " & txtAccID.Text, MsgBoxStyle.Information, "Prolis Labels Print")
            End If
        End If
    End Sub


    Private Sub dgvSources_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSources.CellDoubleClick
        If Not cmbSource.SelectedIndex = -1 Then
            Return
        End If
        Dim c2 = dgvSources.Rows(e.RowIndex).Cells(2).Value.ToString()
        Dim c3 = dgvSources.Rows(e.RowIndex).Cells(3).Value.ToString()
        Dim c4 = dgvSources.Rows(e.RowIndex).Cells(4).Value.ToString()
        Dim c5 = dgvSources.Rows(e.RowIndex).Cells(5).Value.ToString()
        Dim c6 = dgvSources.Rows(e.RowIndex).Cells(6).Value.ToString()
        Dim c7 = dgvSources.Rows(e.RowIndex).Cells(7).Value.ToString()

        dgvSources.Rows().RemoveAt(e.RowIndex)

        txtQty.Text = c3
        dtpDateDrawn.Value = c4
        txtDrawnTime.Text = c5
        txtSrcComment.Text = c7
        Dim i As Integer
        For i = 0 To cmbSource.Items.Count - 1
            If cmbSource.Items(i).ToString = c2 Then
                cmbSource.SelectedIndex = i
                Exit For
            End If
        Next
        For i = 0 To cmbTemp.Items.Count - 1
            If cmbTemp.Items(i).ToString = c6 Then
                cmbTemp.SelectedIndex = i
                Exit For
            End If
        Next
    End Sub

    Private Sub RemMedAll_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub tcDxMeds_TabIndexChanged(sender As Object, e As EventArgs) Handles tcDxMeds.TabIndexChanged

    End Sub

    Private Sub RemMedSelected_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub RemMedAll_Click_1(sender As Object, e As EventArgs) Handles RemMedAll.Click
        ClearMeds()
    End Sub

    Private Sub btnRemDxAll_Click_1(sender As Object, e As EventArgs) Handles btnRemDxAll.Click
        ClearDxs()
    End Sub

    Private Sub dgvDxs_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDxs.CellClick
        If dgvDxs.Rows().Count = 1 Then
            If dgvDxs.Rows(e.RowIndex).Cells(0).Value = Nothing Or dgvDxs.Rows(e.RowIndex).Cells(0).Value = "" Then
                Return
            End If
        End If
        dgvDxs.Rows.RemoveAt(e.RowIndex)
        If dgvDxs.Rows().Count = 0 Then
            dgvDxs.Rows.Add()
        End If
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If txtPatientID.Text = "" Then
            Return

        End If
        If btnNew.Checked Then
            'For ii = dgvMeds.RowCount - 1 To 0 Step -1
            '    If Not dgvMeds.Rows(ii).Cells(0).Value Is Nothing Then

            '    End If
            'Next
            Dim ysno = MessageBox.Show("Do you want to replace the existing Medications ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If ysno = DialogResult.No Then
                Return
            End If
        End If
        Dim q As String = "select distinct Medication  from Req_Med where Accession_ID in (select top 1 ID from Requisitions where Patient_ID = " & txtPatientID.Text & " order by AccessionDate desc)"
        Dim i As Integer
        For i = dgvMeds.RowCount - 1 To 0 Step -1
            dgvMeds.Rows.RemoveAt(i)
        Next
        Dim meds = CommonData.ExecuteQuery(q)
        Dim med As String = ""
        For Each row In meds
            med = row("Medication")
            dgvMeds.Rows.Add(med, Nothing)
        Next
        dgvMeds.Rows.Add()
    End Sub

    Private Sub LoadDx_Click(sender As Object, e As EventArgs) Handles LoadDx.Click
        If txtPatientID.Text = "" Then
            MessageBox.Show("Please select a patient.", "Prolis Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return

        End If
        If btnNew.Checked Then
            Dim ysno = MessageBox.Show("Do you want to replace the existing DX Codes", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If ysno = DialogResult.No Then
                Return
            End If
        End If
        Dim q As String = "select distinct Dx_Code  from Req_Dx where Accession_ID in (select top 1 ID from Requisitions where Patient_ID = " & txtPatientID.Text & " order by AccessionDate desc)"
        Dim i As Integer
        For i = dgvDxs.RowCount - 1 To 0 Step -1
            dgvDxs.Rows.RemoveAt(i)
        Next
        Dim meds = CommonData.ExecuteQuery(q)
        Dim med As String = ""
        For Each row In meds
            med = row("Dx_Code")
            dgvDxs.Rows.Add(med, Nothing)
        Next
        dgvDxs.Rows.Add()
    End Sub








    Private Sub txtAccFrom_MouseDoubleClick_1(sender As Object, e As MouseEventArgs)
        txtAccFrom.Text = Clipboard.GetText()
    End Sub


    Private Sub printLabel_Click(sender As Object, e As EventArgs) Handles printLabel.Click
        If txtAccID.Text = "" Then

            Return

        End If
        If Not CommonData.IsExistsInTable("Requisitions", "ID", txtAccID.Text) Then
            MessageBox.Show("Accession not found.", "Prolis Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        If Val(txtLabels.Text) > 0 Then
            Dim Printer As String = GetLabelPrinterName()
            If Not ThisUser.SpecificPrinter = "Default" Then
                Printer = ThisUser.SpecificPrinter
            End If
            If Printer <> "" Then
                PrintLabels(Printer, txtAccID.Text, CInt(txtLabels.Text),, , 0)
            Else
                MsgBox("Please set Label printer from System Config. " &
                "number: " & txtAccID.Text, MsgBoxStyle.Information, "Prolis Labels Print")


            End If
        End If
    End Sub

    Private Sub txtRejectReason_Validated_1(sender As Object, e As EventArgs) Handles txtRejectReason.Validated
        UpdateRequisitionProgress()
    End Sub

    Private Sub txtRejectReason_TextChanged(sender As Object, e As EventArgs) Handles txtRejectReason.TextChanged
        UpdateRequisitionProgress()
    End Sub

    Private Sub txtAccFrom_TextChanged(sender As Object, e As EventArgs) Handles txtAccFrom.TextChanged
        If txtAccFrom.Text.Contains("-") Then
            Dim acc = txtAccFrom.Text.Split("-")(0)
            txtAccFrom.Text = acc
        End If
    End Sub


End Class