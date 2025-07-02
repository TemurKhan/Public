Imports System.Windows.Forms
Imports System.Data
Imports System.Reflection
Imports Org.BouncyCastle.Asn1
Imports Microsoft.Data
Imports Microsoft.Data.SqlClient
Public Class frmScrubber
    Private origWidth As Integer
    Private origHeight As Integer
    Private DCOLOR As Color
    Private BADACCSS As String = ""
    Private BADACCS(,) As String
    'Private RsA As New ADODB.Recordset
    Private dtRecords As DataTable
    Public TCode As String = ""
    Private Accessions As Integer
    Private AccDirty As Boolean = False
    Private PatientDirty As Boolean = False
    Private DxDirty As Boolean = False
    Private PayerDirty As Boolean = False
    Private CommentDirty As Boolean = False
    Private EECC As Boolean = False 'Eligibility
    Private CurrAcc As Integer
    Private Curow As Integer


    Private Sub frmScrubber_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MaximumSize = MaxSize

        ConfigureDateTimePicker(dtpDateFrom)
        ConfigureDateTimePicker(dtpDateTo)

        'dtpDateFrom.MinDate = New DateTime(2000, 1, 1)
        'dtpDateFrom.Format = DateTimePickerFormat.Custom
        'dtpDateFrom.CustomFormat = Format(Date.Today, SystemConfig.DateFormat)   '" "

        'dtpDateTo.MinDate = New DateTime(2000, 1, 1)
        'dtpDateTo.Format = dtpDateFrom.Format
        'dtpDateTo.CustomFormat = dtpDateFrom.CustomFormat

        Dim i As Integer
        lblFrom.Text += " (" & SystemConfig.DateFormat & ")"
        lblTo.Text += " (" & SystemConfig.DateFormat & ")"
        Dim POINTERS() As String = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"}
        InitializeConfiguration(MyLab.ID)
        dgvDXs.RowCount = 12
        For i = 0 To dgvDXs.RowCount - 1
            dgvDXs.Rows(i).Cells(0).Value = (i + 1).ToString
        Next
        DCOLOR = lblPatient.BackColor
        dgvCharges.RowCount = 1
        cmbABU.SelectedIndex = 2
        btnSave.Enabled = False
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
        Try

        Catch ex As Exception

        End Try


    End Sub

    Private Sub chkClientAllSpec_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkClientAllSpec.CheckedChanged
        If chkClientAllSpec.Checked = False Then
            chkClientAllSpec.Text = "All Clients"
            txtClientID.Text = ""
            txtClientID.Enabled = False
        Else
            chkClientAllSpec.Text = "Specific Client"
            txtClientID.Enabled = True
        End If
    End Sub

    'Private Sub txtDateTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    dtpDateTo.BackColor = FCOLOR
    'End Sub

    'Private Sub txtDateTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    txtDateTo.BackColor = NFCOLOR
    'End Sub

    Private Sub txtDateTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
        'If UserEnteredText(txtDateTo) <> "" Then
        '    If IsDate(txtDateTo.Text) = True Then
        '        txtAccFrom.Text = ""
        '        txtAccTo.Text = ""
        '    Else
        '        MsgBox("Invalid Date")
        '        txtDateTo.Text = ""
        '        txtDateTo.Focus()
        '    End If
        'End If
    End Sub

    'Private Sub txtDateFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    txtDateFrom.BackColor = FCOLOR
    'End Sub

    'Private Sub txtDateFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    txtDateFrom.BackColor = NFCOLOR
    'End Sub

    'Private Sub txtDateFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If UserEnteredText(txtDateFrom) <> "" Then
    '        If IsDate(txtDateFrom.Text) = True Then
    '            txtAccFrom.Text = ""
    '            txtAccTo.Text = ""
    '        Else
    '            MsgBox("Invalid Date")
    '            txtDateFrom.Text = ""
    '            txtDateFrom.Focus()
    '        End If
    '    End If
    'End Sub

    Private Sub txtAccFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccFrom.GotFocus
        txtAccFrom.BackColor = FCOLOR
    End Sub

    Private Sub txtAccFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccFrom.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAccFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccFrom.LostFocus
        txtAccFrom.BackColor = NFCOLOR
    End Sub

    Private Sub txtAccFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccFrom.Validated
        If txtAccFrom.Text <> "" Then
            'txtDateFrom.Text = ""
            'txtDateTo.Text = ""
        End If
    End Sub

    Private Sub txtAccTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccTo.GotFocus
        txtAccTo.BackColor = FCOLOR
    End Sub

    Private Sub txtAccTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccTo.LostFocus
        txtAccTo.BackColor = NFCOLOR
    End Sub

    Private Sub txtAccTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccTo.Validated
        If txtAccTo.Text <> "" Then
            'txtDateFrom.Text = ""
            'txtDateTo.Text = ""
        End If
    End Sub

    Private Sub txtClientID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtClientID.GotFocus
        txtClientID.BackColor = FCOLOR
    End Sub

    Private Sub txtClientID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtClientID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtClientID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtClientID.LostFocus
        txtClientID.BackColor = NFCOLOR
    End Sub

    Private Sub txtClientID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtClientID.TextChanged

    End Sub

    Private Sub lstTargets_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstTargets.GotFocus
        lstTargets.BackColor = FCOLOR
    End Sub

    Private Sub lstTargets_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstTargets.LostFocus
        lstTargets.BackColor = NFCOLOR
    End Sub

    Private Function GetTGPMissingDx() As String
        Dim i As Integer
        Dim MissTGPs As String = ""
        If dgvCharges.RowCount > 0 Then
            For i = 0 To dgvCharges.RowCount - 1
                If (dgvCharges.Rows(i).Cells(1).Value IsNot Nothing AndAlso
                dgvCharges.Rows(i).Cells(1).Value.ToString <> "") AndAlso
                (dgvCharges.Rows(i).Cells(4).Value Is Nothing _
                Or dgvCharges.Rows(i).Cells(4).Value = "") Then
                    MissTGPs += dgvCharges.Rows(i).Cells(1).Value.ToString & ", "
                End If
            Next
            If MissTGPs.Length > 2 And Microsoft.VisualBasic.Right(MissTGPs, 2) = ", " _
            Then MissTGPs = Microsoft.VisualBasic.Mid(MissTGPs, 1, Len(MissTGPs) - 2)
        End If
        Return MissTGPs
    End Function

    Private Sub ClearForm()
        txtNavStatus.Text = ""
        lblClient.BackColor = DCOLOR
        lbl3P.BackColor = DCOLOR
        lblPatient.BackColor = DCOLOR
        lblCodes.BackColor = DCOLOR
        lblEligibility.BackColor = DCOLOR
        ClearAccession()
        'If RsA.State = 1 Then RsA.Close()
        dtRecords.Clear() ' Clear the DataTable
        ClearDxs()
        BADACCSS = ""
        EECC = False
        txtMissingRecs.Text = "0"
    End Sub

    Private Sub ClearDxs()
        Dim i As Integer
        For i = 0 To dgvDXs.RowCount - 1
            dgvDXs.Rows(i).Cells(1).Value = ""
        Next
    End Sub

    Private Sub ClearClient()
        txtProviderID.Text = ""
        txtProviderName.Text = ""
        txtProviderAddress.Text = ""
        txtProvPhone.Text = ""
        txtProvFax.Text = ""
        lstAttending.Items.Clear()
        cmbProvPrice.SelectedIndex = -1
    End Sub

    Private Sub DisableActions()
        btnSave.Enabled = False
        btnTarget.Enabled = False
        btnLoad.Enabled = False
        btnFirst.Enabled = False
        btnNext.Enabled = False
        btnPrevious.Enabled = False
        btnLast.Enabled = False
    End Sub

    Private Sub EnableActions0()
        ''btnSave.Enabled = True
        'btnTarget.Enabled = True
        'btnLoad.Enabled = True
        'If RsA.State = 1 Then
        '    If Not RsA.BOF And Not RsA.EOF Then     'in the middle
        '        btnFirst.Enabled = True
        '        btnNext.Enabled = True
        '        btnPrevious.Enabled = True
        '        btnLast.Enabled = True
        '    ElseIf RsA.BOF And RsA.RecordCount > 0 Then
        '        btnFirst.Enabled = False
        '        btnNext.Enabled = True
        '        btnPrevious.Enabled = False
        '        btnLast.Enabled = True
        '    ElseIf RsA.EOF And RsA.RecordCount > 0 Then
        '        btnFirst.Enabled = True
        '        btnNext.Enabled = False
        '        btnPrevious.Enabled = True
        '        btnLast.Enabled = True
        '    Else
        '        btnFirst.Enabled = False
        '        btnNext.Enabled = False
        '        btnPrevious.Enabled = False
        '        btnLast.Enabled = False
        '    End If
        'End If
    End Sub

    Private Sub EnableActions()
        ' btnSave.Enabled = True
        btnTarget.Enabled = True
        btnLoad.Enabled = True

        If dtRecords.Rows.Count > 0 Then
            btnFirst.Enabled = (CurrAcc > 1)
            btnNext.Enabled = (CurrAcc < dtRecords.Rows.Count)
            btnPrevious.Enabled = (CurrAcc > 1)
            btnLast.Enabled = (CurrAcc < dtRecords.Rows.Count)

            txtNavStatus.Text = $"{CurrAcc} of {dtRecords.Rows.Count}"
        Else
            txtNavStatus.Text = "0 of 0"
            btnFirst.Enabled = False
            btnNext.Enabled = False
            btnPrevious.Enabled = False
            btnLast.Enabled = False
        End If
    End Sub


    Private Sub btnTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTarget.Click
        DisableActions()
        lblEligibility.BackColor = Color.PeachPuff
        EECC = False
        Dim sSQL As String = ""
        If (IsDate(dtpDateFrom.Text) Or IsDate(dtpDateTo.Text) Or txtAccFrom.Text <> "" _
        Or txtAccTo.Text <> "") And ((chkClientAllSpec.Checked = False) Or
        (chkClientAllSpec.Checked = True And txtClientID.Text <> "")) Then
            If cmbABU.SelectedIndex = 0 Then    'All
                sSQL = "Select distinct * from Payers where ID in (Select distinct PrimePayer_ID " &
                "from Requisitions where Received <> 0 and BillingType_ID = 1 and ID in (Select " &
                "distinct AccID from vReqTBillable) and Not ID in (Select distinct Accession_ID " &
                "from Charges)"
            ElseIf cmbABU.SelectedIndex = 1 Then    'Scrubbed
                sSQL = "Select distinct * from Payers where ID in (Select distinct PrimePayer_ID " &
                "from Requisitions where Received <> 0 and Scrubbed <> 0 and BillingType_ID " &
                "= 1 and Not ID in (Select Distinct Accession_ID from Charges)"
            Else    'Unscrubbed
                sSQL = "Select distinct * from Payers where ID in (Select distinct PrimePayer_ID " &
                "from Requisitions where Received <> 0 and Scrubbed = 0 and BillingType_ID " &
                "= 1 and ID in (Select distinct AccID from vReqTBillable) and Not ID in " &
                "(Select distinct Accession_ID from Charges)"
            End If

        Else
            EnableActions()
            Return
        End If
        '

        If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
            sSQL += " and AccessionDate between '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) &
            "' and '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) & " 23:59:00')"
        ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
            sSQL += " and AccessionDate between '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) &
            "' and '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) & " 23:59:00')"
        ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
            sSQL += " and AccessionDate between '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) &
            "' and '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) & " 23:59:00')"
        ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
            sSQL += " and ID = " & Val(txtAccFrom.Text) & ")"
        ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
            sSQL += " and ID = " & Val(txtAccTo.Text) & ")"
        ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
            If Val(txtAccFrom.Text) < Val(txtAccTo.Text) Then
                sSQL += " and ID between " & Val(txtAccFrom.Text) &
                " and " & Val(txtAccTo.Text) & ")"
            ElseIf Val(txtAccFrom.Text) > Val(txtAccTo.Text) Then
                sSQL += " and ID between " & Val(txtAccTo.Text) &
                " and " & Val(txtAccFrom.Text) & ")"
            Else
                sSQL += " and ID = " & Val(txtAccFrom.Text) & ")"
            End If
        End If
        '
        If txtClientID.Text <> "" Then sSQL += " and ID = " & Val(txtClientID.Text)
        sSQL += " and Not PayerName like 'zz%' Order by PayerName"
        '
        lstTargets.Items.Clear()
        Dim cnpr As New SqlConnection(connString)
        cnpr.Open()
        Dim cmdpr As New SqlCommand(sSQL, cnpr)
        cmdpr.CommandType = CommandType.Text
        Dim drpr As SqlDataReader = cmdpr.ExecuteReader
        If drpr.HasRows Then
            While drpr.Read
                lstTargets.Items.Add(New MyList(drpr("PayerName"), drpr("ID")))
                lstTargets.SetItemChecked(lstTargets.Items.Count - 1, True)
            End While
        End If
        cnpr.Close()
        cnpr = Nothing
        ClearForm()
        EnableActions()
    End Sub

    Private Sub btnDxSync_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDxSync.Click
        SyncronizeDxs()
    End Sub

    Private Sub SyncronizeDxs()
        'On Error Resume Next
        Dim Ptr As String = ""
        Dim Codes As String = ""
        For i As Integer = 0 To dgvDXs.RowCount - 1
            If Not dgvDXs.Rows(i).Cells(1).Value Is Nothing AndAlso
            Trim(dgvDXs.Rows(i).Cells(1).Value.ToString) <> "" Then   'ICD9 present
                If InStr(Codes, Trim(dgvDXs.Rows(i).Cells(1).Value.ToString)) = 0 Then _
                Codes += Trim(dgvDXs.Rows(i).Cells(1).Value.ToString) & "|"
            End If
        Next
        If Codes.EndsWith("|") Then Codes = Codes.Substring(0, Len(Codes) - 1)
        If Codes <> "" Then
            For i As Integer = 0 To dgvCharges.RowCount - 1
                If dgvCharges.Rows(i).Cells(1).Value IsNot Nothing AndAlso
                dgvCharges.Rows(i).Cells(1).Value.ToString <> "" Then
                    'If dgvCharges.Rows(i).Cells(4).Value Is Nothing Then
                    '    dgvCharges.Rows(i).Cells(4).Value = GetNecessityPtrs(dgvCharges.Rows(i).Cells(1).Value, Codes)
                    'ElseIf Trim(dgvCharges.Rows(i).Cells(4).Value.ToString) = "" Then
                    dgvCharges.Rows(i).Cells(4).Value = GetNecessityPtrs(dgvCharges.Rows(i).Cells(3).Value, Codes)
                    'End If
                End If
            Next
        Else
            For i = 0 To dgvCharges.RowCount - 1
                dgvCharges.Rows(i).Cells(4).Value = ""
            Next
        End If
    End Sub

    Private Function IsTGPOpenMileage(ByVal CPT As String) As Boolean
        Dim OPM As Boolean = False
        If CPT = "P9603" Then OPM = True
        'Dim cnb As New SqlConnection(connstring)
        'cnb.Open()
        'Dim cmdb As New SqlCommand("Select ID from Tests where " & _
        '"CPT_Code = 'P9603' and ID = " & TGPID & " Union Select ID from " & _
        '"Groups where CPT_Code = 'P9603' and ID = " & TGPID & " Union " & _
        '"Select ID from Profiles where CPT_Code = 'P9603' and ID = " & TGPID, cnb)
        'cmdb.CommandType = CommandType.Text
        'Dim drb As SqlDataReader = cmdb.ExecuteReader
        'If drb.HasRows Then OPM = True
        'cnb.Close()
        'cnb = Nothing
        Return OPM
    End Function

    Private Function GetNecessityPtrs(ByVal CPT As String, ByVal ICD9S As String) As String
        Dim POINTERS() As String = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"}
        Dim Ptr As String = ""
        Dim Codes() As String = Split(ICD9S, "|")
        Dim Dxs As String = "'" & Join(Codes, "', '") & "'"
        Dim IsOpenMileage As Boolean = IsTGPOpenMileage(CPT)
        If TGPHasNecessity(CPT) = False Then    'No Necessity
            If Trim(Codes(0)) <> "" And Not IsOpenMileage Then Ptr = "1"
        Else    'Found Necessity
            Dim i As Integer
            For i = 0 To Codes.Length - 1
                If Trim(Codes(i)) <> "" Then
                    If IsDxValid(CPT, Trim(Codes(i))) Then
                        If InStr(Ptr, POINTERS(i)) = 0 And GetPtrVal(Ptr) < 5 Then
                            Ptr += POINTERS(i) & ","
                        End If
                    End If
                End If
                If GetPtrVal(Ptr) >= 4 Then Exit For
            Next
        End If
        If Ptr.EndsWith(",") Then _
        Ptr = Microsoft.VisualBasic.Mid(Ptr, 1, Len(Ptr) - 1)
        Return Ptr
    End Function

    Private Function TGPHasNecessity(ByVal CPT As String) As Boolean
        Dim HasNec As Boolean = False
        Dim cnb As New SqlConnection(connString)
        cnb.Open()
        Dim cmdb As New SqlCommand("Select Dx_Code " &
        "from MedicalNecessity where CPT_Code = '" & CPT & "'", cnb)
        cmdb.CommandType = CommandType.Text
        Dim drb As SqlDataReader = cmdb.ExecuteReader
        If drb.HasRows Then HasNec = True
        cnb.Close()
        cnb = Nothing
        Return HasNec
    End Function
    Private Function IsDxValid(ByVal CPT As String, ByVal Dx As String) As Boolean
        Dim DxValid As Boolean = False
        Dim cnb As New SqlConnection(connString)
        cnb.Open()
        Dim cmdb As New SqlCommand("Select * from MedicalNecessity " &
        "where CPT_Code = '" & CPT & "' and Dx_Code = '" & Dx & "'", cnb)
        cmdb.CommandType = CommandType.Text
        Dim drb As SqlDataReader = cmdb.ExecuteReader
        If drb.HasRows Then DxValid = True
        cnb.Close()
        cnb = Nothing
        Return DxValid
    End Function

    Private Function GetPtrVal(ByVal Ptr As String) As Integer
        Dim VAL As Integer = 0
        Dim PTRS() As String
        If Ptr <> "" Then
            If InStr(Ptr, ",") > 0 Then 'has coma
                PTRS = Split(Ptr, ",")
                VAL = PTRS.Length
            Else
                VAL = 1
            End If
        End If
        Return VAL
    End Function

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        DisableActions()
        lblEligibility.BackColor = Color.PeachPuff
        EECC = False
        Dim sSQL As String = ""
        If (IsDate(dtpDateFrom.Text) Or IsDate(dtpDateTo.Text) Or txtAccFrom.Text <> "" Or
        txtAccTo.Text <> "") And lstTargets.CheckedItems.Count > 0 Then
            If cmbABU.SelectedIndex = 0 Then    'All
                sSQL = "Select ID from Requisitions where Received <> 0 and BillingType_ID = 1 " &
                "and ID in (Select distinct AccID from vReqTBillable) and Not ID in (Select " &
                "distinct Accession_ID from Charges)"
            ElseIf cmbABU.SelectedIndex = 1 Then    'Scrubbed
                sSQL = "Select ID from Requisitions where Received <> 0 and Scrubbed <> 0 and " &
                "BillingType_ID = 1 and Not ID in (Select Distinct Accession_ID from Charges)"
            Else    'Unscrubbed
                sSQL = "Select ID from Requisitions where Received <> 0 and Scrubbed = 0 and " &
                "BillingType_ID = 1 and ID in (Select distinct AccID from vReqTBillable) and " &
                "Not ID in (Select distinct Accession_ID from Charges)"
            End If
            '
            If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                sSQL += " and AccessionDate between '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) &
                "' and '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) & " 23:59:00'"
            ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                sSQL += " and AccessionDate between '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) &
                "' and '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) & " 23:59:00'"
            ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                sSQL += " and AccessionDate between '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) &
                "' and '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) & " 23:59:00'"
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                sSQL += " and ID = " & Val(txtAccFrom.Text)
            ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                sSQL += " and ID = " & Val(txtAccTo.Text)
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                If Val(txtAccFrom.Text) < Val(txtAccTo.Text) Then
                    sSQL += " and ID between " & Val(txtAccFrom.Text) &
                    " and " & Val(txtAccTo.Text)
                ElseIf Val(txtAccFrom.Text) > Val(txtAccTo.Text) Then
                    sSQL += " and ID between " & Val(txtAccTo.Text) &
                    " and " & Val(txtAccFrom.Text)
                Else
                    sSQL += " and ID = " & Val(txtAccFrom.Text)
                End If
            End If
            '
            Dim i As Integer
            Dim ItemX As MyList

            sSQL += " and ID in(Select Accession_ID " &
            "from Req_Coverage where Preference = 'P' and Payer_ID in("
            For i = 0 To lstTargets.CheckedItems.Count - 1
                ItemX = lstTargets.CheckedItems(i)
                sSQL += ItemX.ItemData.ToString & ","
            Next
            sSQL = Microsoft.VisualBasic.Mid(sSQL, 1, Len(sSQL) - 1) & "))"
            '
            If sSQL <> "" Then sSQL += " order by ID"


            'If RsA.State = 1 Then RsA.Close()
            'Dim CNS As New ADODB.Connection
            'CNS.Open(odbCS)
            'RsA.Open(sSQL, CNS, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            'If Not RsA.BOF Then
            '    RsA.MoveLast()
            '    RsA.MoveFirst()
            '    CurrAcc = 1
            '    Accessions = RsA.RecordCount
            '    txtNavStatus.Text = CurrAcc & " of " & Accessions
            '    If Accessions > 1 Then
            '        btnNext.Enabled = True
            '        btnLast.Enabled = True
            '    End If
            '    DisplayAccession(RsA.Fields("ID").Value)
            '    ReDim BADACCS(1, 1)
            'Else
            '    txtNavStatus.Text = "0 of 0"
            '    btnNext.Enabled = False
            '    btnLast.Enabled = False
            '    ClearAccession()
            'End If

            ' Fill dtRecords first
            Using connection As New SqlConnection(connString)
                connection.Open()

                Using adapter As New SqlDataAdapter(sSQL, connection)
                    dtRecords.Clear() ' Ensure no previous data remains
                    adapter.Fill(dtRecords) ' Fill DataTable with results
                End Using
            End Using

            If dtRecords.Rows.Count > 0 Then
                CurrAcc = 1
                Accessions = dtRecords.Rows.Count
                txtNavStatus.Text = $"{CurrAcc} of {Accessions}"

                If Accessions > 1 Then
                    btnNext.Enabled = True
                    btnLast.Enabled = True
                End If

                DisplayAccession(dtRecords.Rows(0)("ID"))
                ReDim BADACCS(1, 1)
            Else
                txtNavStatus.Text = "0 of 0"
                btnNext.Enabled = False
                btnLast.Enabled = False
                ClearAccession()
            End If

            'ClearForm()
        End If
        EnableActions()
    End Sub

    Private Sub ClearThirdParty()
        cmbTPPrice.SelectedIndex = 0
        txtPayerID.Text = ""
        txtPayerName.Text = ""
        txtPolicy.Text = ""
        txtGroup.Text = ""
        pctInsSex.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Blank.ico")
        txtInsSex.Text = ""
        txtInsDOB.Text = ""
        txtCopay.Text = ""
        cmbRelation.SelectedIndex = 0
        txtInsuredID.Text = ""
        txtInsName.Text = ""
        txtInsAddress.Text = ""
    End Sub

    Private Sub ClearPatient()
        txtPatientID.Text = ""
        txtPatLName.Text = ""
        txtPatFName.Text = ""
        txtPatMName.Text = ""
        txtPatSex.Text = ""
        pctPatSex.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Blank.ico")
        txtPatDOB.Text = ""
        txtPatSSN.Text = ""
        txtPatAdd1.Text = ""
        txtPatAdd2.Text = ""
        txtPatCity.Text = ""
        txtPatState.Text = ""
        txtPatZip.Text = ""
        txtPatHPhone.Text = ""
        txtPatCell.Text = ""
        txtPatEmail.Text = ""
        cmbProvPrice.SelectedIndex = -1
    End Sub

    Private Sub ClearAccession()
        txtAccID.Text = ""
        txtAccDate.Text = ""
        ClearClient()
        ClearPatient()
        ClearThirdParty()
        dgvCharges.Rows.Clear()
        dgvCharges.RowCount += 1
    End Sub

    Private Sub DisplayClient(ByVal ProviderID As Long)
        ClearClient()
        Dim Clinic As String = ""
        Dim ProvName As String = ""
        Dim cndc As New SqlConnection(connString)
        cndc.Open()
        Dim cmddc As New SqlCommand("Select * " &
        "from Providers where ID = " & ProviderID, cndc)
        cmddc.CommandType = CommandType.Text
        Dim drdc As SqlDataReader = cmddc.ExecuteReader
        If drdc.HasRows Then
            While drdc.Read
                txtProviderID.Text = drdc("ID")
                If drdc("IsIndividual") IsNot DBNull.Value AndAlso drdc("IsIndividual") = 0 Then
                    Clinic = drdc("LastName_BSN")
                Else
                    Clinic = Trim(drdc("LastName_BSN")) & ", " & Trim(drdc("FirstName"))
                    If drdc("MiddleName") IsNot DBNull.Value _
                    AndAlso Trim(drdc("MiddleName")) <> "" Then _
                    Clinic += " " & Trim(drdc("MiddleName"))
                    If drdc("Degree") IsNot DBNull.Value _
                    AndAlso Trim(drdc("Degree")) <> "" Then Clinic += " " & Trim(drdc("Degree"))
                End If
                txtProviderName.Text = Clinic
                txtProviderAddress.Text = GetAddress(drdc("Address_ID"))
                If drdc("Phone") IsNot DBNull.Value Then _
                txtProvPhone.Text = PhoneNeat(drdc("Phone"))
                If drdc("Fax") IsNot DBNull.Value Then _
                txtProvFax.Text = PhoneNeat(drdc("Fax"))
                If drdc("PriceLevel") IsNot DBNull.Value Then
                    cmbProvPrice.SelectedIndex = Trim(drdc("PriceLevel"))
                Else
                    cmbProvPrice.SelectedIndex = 0
                End If
                '
                Dim cnda As New SqlConnection(connString)
                cnda.Open()
                Dim cmdda As New SqlCommand("Select * from Providers where ID in (Select " &
                "Provider_ID from Clinic_Provider where Clinic_ID = " & ProviderID & ")", cnda)
                cmdda.CommandType = CommandType.Text
                Dim drda As SqlDataReader = cmdda.ExecuteReader
                If drda.HasRows Then
                    While drda.Read
                        If drda("IsIndividual") IsNot DBNull.Value AndAlso drda("IsIndividual") = 0 Then
                            ProvName = drda("LastName_BSN")
                        Else
                            ProvName = Trim(drda("LastName_BSN")) & ", " & Trim(drda("FirstName"))
                            If drda("MiddleName") IsNot DBNull.Value _
                            AndAlso Trim(drda("MiddleName")) <> "" Then _
                            ProvName += " " & Trim(drda("MiddleName"))
                            If drda("Degree") IsNot DBNull.Value _
                            AndAlso Trim(drda("Degree")) <> "" Then _
                            ProvName += " " & Trim(drda("Degree"))
                        End If
                        lstAttending.Items.Add(New MyList(ProvName, drda("ID")))
                        If drda("ID") = GetAttendingProviderID(Val(txtAccID.Text)) Then
                            lstAttending.SetItemChecked(lstAttending.Items.Count - 1, True)
                            txtNPI.Text = GetProviderNPI(drda("ID"))
                        End If
                    End While
                Else
                    lstAttending.Items.Add(New MyList(Clinic, ProviderID))
                    lstAttending.SetItemChecked(lstAttending.Items.Count - 1, True)
                    txtNPI.Text = GetProviderNPI(ProviderID)
                End If
                cnda.Close()
                cnda = Nothing
            End While
        End If
        cndc.Close()
        cndc = Nothing
        If lstAttending.Items.Count > 0 Then
            If lstAttending.CheckedItems.Count = 0 Then
                lstAttending.SetItemChecked(0, True)
                Dim ItemX As MyList = lstAttending.Items(0)
                txtNPI.Text = GetProviderNPI(ItemX.ItemData)
            End If
        End If
    End Sub

    Private Function GetProviderNPI(ByVal ProvID As Long) As String
        Dim NPI As String = ""
        Dim cnnpi As New SqlConnection(connString)
        cnnpi.Open()
        Dim cmdnpi As New SqlCommand("Select " &
        "NPI from Providers where ID = " & ProvID, cnnpi)
        cmdnpi.CommandType = CommandType.Text
        Dim drnpi As SqlDataReader = cmdnpi.ExecuteReader
        If drnpi.HasRows Then
            While drnpi.Read
                If drnpi("NPI") IsNot DBNull.Value _
                Then NPI = drnpi("NPI")
            End While
        End If
        cnnpi.Close()
        cnnpi = Nothing
        Return NPI
    End Function

    Private Function GetAttendingProviderID(ByVal AccID As Long) As Long
        Dim AttID As Long = -1
        Dim cnap As New SqlConnection(connString)
        cnap.Open()
        Dim cmdap As New SqlCommand("Select AttendingProvider_ID " &
        "from Requisitions where ID = " & AccID, cnap)
        cmdap.CommandType = CommandType.Text
        Dim drap As SqlDataReader = cmdap.ExecuteReader
        If drap.HasRows Then
            While drap.Read
                AttID = drap("AttendingProvider_ID")
            End While
        End If
        cnap.Close()
        cnap = Nothing
        Return AttID
    End Function

    Private Sub DisplayDxs(ByVal AccID As Long)
        Dim i As Integer = 0
        For i = 0 To dgvDXs.RowCount - 1
            dgvDXs.Rows(i).Cells(1).Value = ""
            dgvDXs.Rows(i).Cells(1).ReadOnly = False
        Next
        i = 0
        Dim cndx As New SqlConnection(connString)
        cndx.Open()
        Dim cmddx As New SqlCommand("Select * from Req_Dx " &
        "where Accession_ID = " & AccID & " order by Ordinal", cndx)
        cmddx.CommandType = CommandType.Text
        Dim drdx As SqlDataReader = cmddx.ExecuteReader
        If drdx.HasRows Then
            While drdx.Read
                If i = dgvDXs.RowCount - 1 Then
                    dgvDXs.RowCount += 1
                    dgvDXs.Rows(dgvDXs.RowCount - 1).Cells(1).ReadOnly = False
                    dgvDXs.Rows(dgvDXs.RowCount - 1).Cells(0).Value = dgvDXs.RowCount.ToString
                End If
                If Trim(drdx("Dx_Code")) <> "" Then
                    dgvDXs.Rows.Add()
                    dgvDXs.Rows(i).Cells(1).Value = Trim(drdx("Dx_Code"))
                    i += 1
                End If
            End While
        End If
        cndx.Close()
        cndx = Nothing
    End Sub

    Private Function GetTPPrice(ByVal PayerID As Long) As Integer
        Dim Price As Integer = 0
        Dim cntp As New SqlConnection(connString)
        cntp.Open()
        Dim cmdtp As New SqlCommand("Select " &
        "PriceLevel from Payers where ID = " & PayerID, cntp)
        cmdtp.CommandType = CommandType.Text
        Dim drtp As SqlDataReader = cmdtp.ExecuteReader
        If drtp.HasRows Then
            While drtp.Read
                Price = drtp("PriceLevel")
            End While
        End If
        cntp.Close()
        cntp = Nothing
        Return Price
    End Function

    Private Function GetPayerName(ByVal PayerID As Long) As String
        Dim Payer As String = ""
        Dim cntp As New SqlConnection(connString)
        cntp.Open()
        Dim cmdtp As New SqlCommand("Select " &
        "PayerNAme from Payers where ID = " & PayerID, cntp)
        cmdtp.CommandType = CommandType.Text
        Dim drtp As SqlDataReader = cmdtp.ExecuteReader
        If drtp.HasRows Then
            While drtp.Read
                Payer = drtp("PayerName")
            End While
        End If
        cntp.Close()
        cntp = Nothing
        Return Payer
    End Function

    Private Sub DisplayThirdParty(ByVal AccID As Long)
        Dim cntp As New SqlConnection(connString)
        cntp.Open()
        Dim cmdtp As New SqlCommand("Select a.*, b.LastName, c.PayerName, b.FirstName, " &
        "b.Sex, b.DOB, b.Address_ID from Payers c inner join (Req_Coverage a inner join Patients " &
        "b on b.ID = a.Insured_ID) on a.Payer_ID = c.ID where a.Preference " &
        "= 'P' and a.Accession_ID = " & AccID, cntp)
        cmdtp.CommandType = CommandType.Text
        Dim drtp As SqlDataReader = cmdtp.ExecuteReader
        If drtp.HasRows Then
            While drtp.Read
                cmbTPPrice.SelectedIndex = GetTPPrice(drtp("Payer_ID"))
                txtPayerID.Text = drtp("Payer_ID")
                txtPayerName.Text = drtp("PayerName")
                txtPolicy.Text = drtp("PolicyNo")
                txtGroup.Text = drtp("GroupNo")
                If drtp("CoPayment") IsNot DBNull.Value Then
                    txtCopay.Text = drtp("CoPayment")
                Else
                    txtCopay.Text = "0"
                End If

                cmbRelation.SelectedIndex = drtp("Relation")
                txtInsuredID.Text = drtp("Insured_ID")
                txtInsName.Text = drtp("LastName") & ", " & drtp("FirstName")
                txtInsSex.Text = drtp("Sex")
                If txtInsSex.Text = "F" Then
                    pctInsSex.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Female.ico")
                ElseIf txtInsSex.Text = "M" Then
                    pctInsSex.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\male.ico")
                End If
                txtInsDOB.Text = Format(drtp("DOB"), SystemConfig.DateFormat)
                If drtp("Address_ID") IsNot DBNull.Value Then _
                txtInsAddress.Text = GetAddress(drtp("Address_ID"))
            End While
        End If
        cntp.Close()
        cntp = Nothing
        PayerDirty = False
    End Sub

    Private Sub DisplayPatient(ByVal PatientID As Long)
        ClearPatient()
        Dim cndp As New SqlConnection(connString)
        cndp.Open()
        Dim cmddp As New SqlCommand("Select * " &
        "from Patients where ID = " & PatientID, cndp)
        cmddp.CommandType = CommandType.Text
        Dim drdp As SqlDataReader = cmddp.ExecuteReader
        If drdp.HasRows Then
            While drdp.Read
                txtPatientID.Text = drdp("ID")
                txtPatLName.Text = drdp("LastName")
                txtPatFName.Text = drdp("FirstName")
                If drdp("MiddleName") IsNot DBNull.Value Then txtPatMName.Text = drdp("MiddleName")
                txtPatSex.Text = Trim(drdp("Sex"))
                If txtPatSex.Text = "F" Then
                    pctPatSex.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Female.ico")
                ElseIf txtPatSex.Text = "M" Then
                    pctPatSex.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Male.ico")
                End If
                txtPatDOB.Text = Format(drdp("DOB"), SystemConfig.DateFormat)
                If drdp("SSN") IsNot DBNull.Value Then txtPatSSN.Text = drdp("SSN")
                If drdp("Address_ID") IsNot DBNull.Value Then
                    txtPatAdd1.Text = GetAddress1(drdp("Address_ID"))
                    txtPatAdd2.Text = GetAddress2(drdp("Address_ID"))
                    txtPatCity.Text = GetAddressCity(drdp("Address_ID"))
                    txtPatState.Text = GetAddressState(drdp("Address_ID"))
                    txtPatZip.Text = GetAddressZip(drdp("Address_ID"))
                End If

                If drdp("HomePhone") IsNot DBNull.Value Then txtPatHPhone.Text = Trim(drdp("homePhone"))
                If drdp("Cell") IsNot DBNull.Value Then txtPatCell.Text = Trim(drdp("cell"))
                If drdp("Email") IsNot DBNull.Value Then txtPatEmail.Text = Trim(drdp("Email"))
                cmbProvPrice.SelectedIndex = SystemConfig.PatientPriceLevel
            End While
        End If
        cndp.Close()
        cndp = Nothing
    End Sub

    Private Sub DisplayCharges0(ByVal AccID As Long)
        'dgvCharges.Rows.Clear()
        'Dim sSQL As String = "Select a.ID as TGPID, a.Name as TGPName, a.CPT_Code as CPT, a.ListPrice " &
        '"from Tests a inner join vReqTBillable b on a.ID = b.TBBID where b.AccID = " & AccID & " Union " &
        '"Select c.ID as TGPID, c.Name as TGPName, c.CPT_Code as CPT, c.ListPrice from Groups c inner " &
        '"join vReqTBillable d on c.ID = d.TBBID where d.AccID = " & AccID & " Union Select e.ID as " &
        '"TGPID, e.Name as TGPName, e.CPT_Code as CPT, e.ListPrice from Profiles e inner join " &
        '"vReqTBillable f on e.ID = f.TBBID where f.AccID = " & AccID
        ''
        'Dim cnsql As New Data.SqlConnection(connString)
        'cnsql.Open()
        'Dim selcmd As New Data.SqlCommand(sSQL, cnsql)
        'Dim selDR As Data.SqlDataReader = selcmd.ExecuteReader
        'If selDR.HasRows Then
        '    While selDR.Read
        '        If selDR("ListPrice") IsNot DBNull.Value Then
        '            dgvCharges.Rows.Add(System.Drawing.Image.FromFile(
        '            My.Application.Info.DirectoryPath & "\Images\Eraser.ico"),
        '            selDR("TGPID"), selDR("TGPName"), selDR("CPT"), "",
        '            Format(selDR("ListPrice"), "0.00"))
        '        Else
        '            dgvCharges.Rows.Add(System.Drawing.Image.FromFile(
        '            My.Application.Info.DirectoryPath & "\Images\Eraser.ico"),
        '            selDR("TGPID"), selDR("TGPName"), selDR("CPT"), "", "0.00")
        '        End If
        '    End While
        'End If
        'cnsql.Close()
        'cnsql = Nothing
        ''
        'dgvCharges.RowCount += 1
    End Sub

    Private Sub DisplayCharges(ByVal AccID As Long)
        dgvCharges.Rows.Clear()

        Dim sSQL As String = "
        SELECT ID as TGPID, Name as TGPName, CPT_Code as CPT, ListPrice FROM Tests WHERE ID IN 
        (SELECT TBBID FROM vReqTBillable WHERE AccID = @AccID)
        UNION
        SELECT ID as TGPID, Name as TGPName, CPT_Code as CPT, ListPrice FROM Groups WHERE ID IN 
        (SELECT TBBID FROM vReqTBillable WHERE AccID = @AccID)
        UNION
        SELECT ID as TGPID, Name as TGPName, CPT_Code as CPT, ListPrice FROM Profiles WHERE ID IN 
        (SELECT TBBID FROM vReqTBillable WHERE AccID = @AccID)
    "

        Using connection As New SqlConnection(connString)
            connection.Open()

            Using command As New SqlCommand(sSQL, connection)
                command.Parameters.AddWithValue("@AccID", AccID)

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.HasRows Then
                        While reader.Read()
                            Dim listPrice As String = If(IsDBNull(reader("ListPrice")), "0.00", Format(reader("ListPrice"), "0.00"))

                            dgvCharges.Rows.Add(
                            System.Drawing.Image.FromFile($"{My.Application.Info.DirectoryPath}\Images\Eraser.ico"),
                            reader("TGPID"), reader("TGPName"), reader("CPT"), "", listPrice
                        )
                        End While
                    End If
                End Using
            End Using
        End Using

        dgvCharges.RowCount += 1
    End Sub


    Private Sub DisplayAccession(ByVal AccID As Long)
        Dim Requisits() As Long = {0, 0, 0, 0, 0, 0, 0, 0, 0}
        '0=PayerID, 1=AddID, 2=NPI, 3=Policy, 4=Group, 5=Dx, 6=CPT, 7=Pointer, 8=Price
        Dim cnda As New SqlConnection(connString)
        cnda.Open()
        Dim cmdda As New SqlCommand("Select " &
        "* from Requisitions where ID = " & AccID, cnda)
        cmdda.CommandType = CommandType.Text
        Dim drda As SqlDataReader = cmdda.ExecuteReader
        If drda.HasRows Then
            While drda.Read
                txtAccID.Text = drda("ID")
                txtAccDate.Text = Format(drda("AccessionDate"), SystemConfig.DateFormat)
                Requisits = GetBillRequisits(drda("PrimePayer_ID"))
                If drda("WorkCmnt") IsNot DBNull.Value Then
                    txtWorkCmnt.Text = drda("WorkCmnt")
                Else
                    txtWorkCmnt.Text = ""
                End If
                DisplayDxs(drda("ID"))
                DisplayClient(drda("OrderingProvider_ID"))
                DisplayThirdParty(drda("ID"))
                DisplayPatient(drda("Patient_ID"))
                DisplayCharges(drda("ID"))
            End While
        End If
        cnda.Close()
        cnda = Nothing
        btnDxSync_Click(Nothing, Nothing)
        If txtPayerID.Text <> "" Then _
        UpdateRequisits(Requisits)
    End Sub

    Private Sub UpdateRequisits(ByVal Requisits() As Long)
        '0=PayerID, 1=AddID, 2=NPI, 3=Policy, 4=Group, 5=Dx, 6=CPT, 7=Pointer, 8=Price
        Dim DefColor As Color = lblPatient.BackColor
        If Requisits(1) = 0 Then
            lblPatient.BackColor = DefColor
        Else
            If txtPatAdd1.Text <> "" And txtPatCity.Text <> "" And
            txtPatState.Text <> "" And txtPatZip.Text <> "" Then
                lblPatient.BackColor = Color.PaleGreen
            Else
                lblPatient.BackColor = Color.PeachPuff
            End If
        End If
        If Requisits(2) = 0 Then    'NPI
            lblClient.BackColor = DefColor
        Else
            If txtNPI.Text <> "" Then
                lblClient.BackColor = Color.PaleGreen
            Else
                lblClient.BackColor = Color.PeachPuff
            End If
        End If
        '
        If Requisits(3) = 0 Then    'Policy
            lbl3P.BackColor = DefColor
        Else
            If txtPayerID.Text <> "" And txtPolicy.Text <> "" And
            ((Requisits(4) = 0) Or (Requisits(4) <> 0 And
            Trim(txtGroup.Text) <> "")) And ((cmbRelation.SelectedIndex = 0) Or
            (cmbRelation.SelectedIndex > 0 And txtInsuredID.Text <> "")) Then
                lbl3P.BackColor = Color.PaleGreen
            Else
                lbl3P.BackColor = Color.PeachPuff
            End If
        End If
        '

        If Requisits(5) = 0 And Requisits(6) = 0 And Requisits(7) = 0 And Requisits(8) = 0 Then
            lblCodes.BackColor = DefColor
        Else
            lblCodes.BackColor = Color.PaleGreen
        End If
        If Requisits(6) <> 0 Then   'CPT required
            If MissingCPT() = True Then lblCodes.BackColor = Color.PeachPuff
        End If
        If Requisits(7) <> 0 Then    'pointers
            If GetTGPMissingDx() <> "" Then lblCodes.BackColor = Color.PeachPuff
        End If
        If Requisits(8) <> 0 Then   'non zero price required
            If ZeroPricedItems() = True Then lblCodes.BackColor = Color.PeachPuff
        End If
        '
        If EECC = True Then
            lblEligibility.BackColor = Color.PaleGreen
        Else
            lblEligibility.BackColor = Color.PeachPuff
        End If
        checkelig()
    End Sub

    Private Function ZeroPricedItems() As Boolean
        Dim ZeroPriced As Boolean = False
        Dim i As Integer
        For i = 0 To dgvCharges.RowCount - 1
            If dgvCharges.Rows(i).Cells(1).Value <> Nothing AndAlso
            dgvCharges.Rows(i).Cells(1).Value.ToString <> "" Then
                If dgvCharges.Rows(i).Cells(5).Value <> Nothing AndAlso
                Val(dgvCharges.Rows(i).Cells(5).Value) <= 0 Then
                    ZeroPriced = True
                    Exit For
                End If
            End If
        Next
        Return ZeroPriced
    End Function

    Private Function MissingCPT() As Boolean
        Dim CPTMissing As Boolean = False
        Dim i As Integer
        For i = 0 To dgvCharges.RowCount - 1
            If dgvCharges.Rows(i).Cells(1).Value <> Nothing AndAlso
            dgvCharges.Rows(i).Cells(1).Value.ToString <> "" Then
                If dgvCharges.Rows(i).Cells(3).Value = Nothing Then
                    CPTMissing = True
                    Exit For
                ElseIf dgvCharges.Rows(i).Cells(3).Value = "" Then
                    CPTMissing = True
                    Exit For
                End If
            End If
        Next
        Return CPTMissing
    End Function

    Private Sub btnFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirst.Click
        DisableActions()
        If PatientDirty Then
            If txtPatientID.Text <> "" And txtPatLName.Text <> "" And txtPatFName.Text _
            <> "" And IsDate(txtPatDOB.Text) And txtPatSex.Text <> "" And txtPatAdd1.Text <> "" And
            txtPatCity.Text <> "" And txtPatState.Text <> "" And txtPatZip.Text <> "" Then _
            ApplyPatientChanges()
            PatientDirty = False
        End If
        If DxDirty Then
            UpdateDx()
            DxDirty = False
        End If
        If PayerDirty Then
            UpdateTP(Val(txtAccID.Text))
            PayerDirty = False
        End If
        If CommentDirty Then
            UpdateComment()
            CommentDirty = False
        End If
        checkelig()
        If lblClient.BackColor = Color.PaleGreen And lbl3P.BackColor = Color.PaleGreen And
        lblPatient.BackColor = Color.PaleGreen And lblCodes.BackColor = Color.PaleGreen Then
            ExecuteSqlProcedure("Update Requisitions set Scrubbed = 1 where ID = " & Val(txtAccID.Text))
        Else
            ExecuteSqlProcedure("Update Requisitions set Scrubbed = 0 where ID = " & Val(txtAccID.Text))
            If InStr(BADACCSS, txtAccID.Text & ",") = 0 Then    'does not exist
                BADACCSS += txtAccID.Text & ","
                If BADACCS(0, UBound(BADACCS, 2)) <> "" Then ReDim _
                BADACCS(UBound(BADACCS, 1), UBound(BADACCS, 2) + 1)
                BADACCS(0, UBound(BADACCS, 2)) = txtAccID.Text
                BADACCS(1, UBound(BADACCS, 2)) = GetTGPMissingDx()
                txtMissingRecs.Text = CStr(Val(txtMissingRecs.Text) + 1)
            End If
        End If
        'UpdateSave()
        'RsA.MoveFirst()
        CurrAcc = 1
        txtNavStatus.Text = CurrAcc & " of " & Accessions
        ' DisplayAccession(RsA.Fields("ID").Value)

        DisplayAccession(dtRecords.Rows(CurrAcc - 1)("ID"))

        btnNext.Enabled = True
        btnLast.Enabled = True
        btnPrevious.Enabled = False
        btnFirst.Enabled = False
        EnableActions()
    End Sub

    Private Sub UpdateTP(ByVal AccID As Long)
        If txtPayerID.Text <> "" And Trim(txtPolicy.Text) <> "" Then
            ExecuteSqlProcedure("Delete from Req_Coverage where Preference = 'P' and Accession_ID = " & AccID)
            If txtPatientID.Text <> "" Then _
            ExecuteSqlProcedure("Delete from Coverages where Preference = 'P' and " &
            "Patient_ID = " & Val(txtPatientID.Text))
            '
            Dim cnr As New SqlConnection(connString)
            cnr.Open()
            Dim cmdr As New SqlCommand("Req_Coverage_SP", cnr)
            cmdr.CommandType = CommandType.StoredProcedure
            cmdr.Parameters.AddWithValue("@act", "Upsert")
            cmdr.Parameters.AddWithValue("@Accession_ID", Val(txtAccID.Text))
            cmdr.Parameters.AddWithValue("@Payer_ID", Trim(txtPayerID.Text))
            cmdr.Parameters.AddWithValue("@Ordinal", 0)
            If cmbRelation.SelectedIndex = 0 Then
                cmdr.Parameters.AddWithValue("@Insured_ID", Val(txtPatientID.Text))
            Else
                cmdr.Parameters.AddWithValue("@Insured_ID", Val(txtInsuredID.Text))
            End If
            cmdr.Parameters.AddWithValue("@Preference", "P")
            cmdr.Parameters.AddWithValue("@GroupNo", Trim(txtGroup.Text))
            cmdr.Parameters.AddWithValue("@PolicyNo", Trim(txtPolicy.Text))
            cmdr.Parameters.AddWithValue("@Relation", cmbRelation.SelectedIndex)
            cmdr.Parameters.AddWithValue("@CoPayment", Val(txtCopay.Text))
            cmdr.Parameters.AddWithValue("@WorkmanComp", 0)
            cmdr.Parameters.AddWithValue("@InstanceDate", Nothing)
            cmdr.Parameters.AddWithValue("@Comment", "")
            Try
                cmdr.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                cnr.Close()
                cnr = Nothing
            End Try
            '
            Dim conn As New SqlConnection(connString)
            conn.Open()
            Dim cmdupsert As New SqlCommand("Coverages_SP", conn)
            cmdupsert.CommandType = CommandType.StoredProcedure
            cmdupsert.Parameters.AddWithValue("@act", "Upsert")
            cmdupsert.Parameters.AddWithValue("@Patient_ID", Val(txtPatientID.Text))
            cmdupsert.Parameters.AddWithValue("@Insurance_ID", Trim(txtPayerID.Text))
            cmdupsert.Parameters.AddWithValue("@Ordinal", 0)
            If cmbRelation.SelectedIndex = 0 Then
                cmdupsert.Parameters.AddWithValue("@Insured_ID", Val(txtPatientID.Text))
            Else
                cmdupsert.Parameters.AddWithValue("@Insured_ID", Val(txtInsuredID.Text))
            End If
            cmdupsert.Parameters.AddWithValue("@Preference", "P")
            cmdupsert.Parameters.AddWithValue("@GroupNo", Trim(txtGroup.Text))
            cmdupsert.Parameters.AddWithValue("@PolicyNo", Trim(txtPolicy.Text))
            cmdupsert.Parameters.AddWithValue("@StartDate", Nothing)
            cmdupsert.Parameters.AddWithValue("@ExpireDate", Nothing)
            cmdupsert.Parameters.AddWithValue("@Relation", cmbRelation.SelectedIndex)
            cmdupsert.Parameters.AddWithValue("@Copayment", Val(txtCopay.Text))
            cmdupsert.Parameters.AddWithValue("@LastEditedOn", Date.Now)
            cmdupsert.Parameters.AddWithValue("@EditedBy", ThisUser.ID)
            Try
                cmdupsert.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                conn.Close()
                conn = Nothing
            End Try
            '
            ExecuteSqlProcedure("Update Requisitions set PrimePayer_ID = " &
            Trim(txtPayerID.Text) & ", BillingType_ID = 1 where ID = " & AccID)
        End If
    End Sub

    Private Sub UpdateComment()
        If CommentDirty = True And txtAccID.Text <> "" Then
            ExecuteSqlProcedure("Update Requisitions set WorkCmnt = '" &
            txtWorkCmnt.Text & "' where ID = " & Val(txtAccID.Text))
            CommentDirty = False
        End If
    End Sub

    Private Sub UpdateDx()
        If Trim(txtAccID.Text) <> "" Then
            ExecuteSqlProcedure("Delete from Req_Dx where Accession_ID = " & Val(txtAccID.Text))
            For i As Integer = 0 To dgvDXs.RowCount - 1
                If dgvDXs.Rows(i).Cells(1).Value IsNot Nothing AndAlso
                Trim(dgvDXs.Rows(i).Cells(1).Value) <> "" Then
                    ExecuteSqlProcedure("Insert into Req_Dx (Accession_ID, Dx_Code, " &
                    "Ordinal) values (" & Val(txtAccID.Text) & ", '" &
                    Trim(dgvDXs.Rows(i).Cells(1).Value) & "', " & i & ")")
                End If
            Next
        End If
    End Sub

    Private Sub ApplyPatientChanges()
        Dim cnpat As New SqlConnection(connString)
        cnpat.Open()
        Dim cmdupsert As New SqlCommand("Patients_SP", cnpat)
        cmdupsert.CommandType = CommandType.StoredProcedure
        cmdupsert.Parameters.AddWithValue("@act", "Upsert")
        cmdupsert.Parameters.AddWithValue("@ID", Val(txtPatientID.Text))
        cmdupsert.Parameters.AddWithValue("@LastName", Trim(txtPatLName.Text))
        cmdupsert.Parameters.AddWithValue("@FirstName", Trim(txtPatFName.Text))
        cmdupsert.Parameters.AddWithValue("@MiddleName", Trim(txtPatMName.Text))
        cmdupsert.Parameters.AddWithValue("@Sex", txtPatSex.Text)
        cmdupsert.Parameters.AddWithValue("@DOB", txtPatDOB.Text)
        cmdupsert.Parameters.AddWithValue("@Ethnicity", "Unknown")
        cmdupsert.Parameters.AddWithValue("@Race_ID", 7)
        cmdupsert.Parameters.AddWithValue("@IsAlive", 1)
        cmdupsert.Parameters.AddWithValue("@Address_ID", GetAddressID(txtPatAdd1.Text,
        txtPatAdd2.Text, txtPatCity.Text, txtPatState.Text, txtPatZip.Text, ""))
        cmdupsert.Parameters.AddWithValue("@HomePhone", PhoneNeat(txtPatHPhone.Text))
        cmdupsert.Parameters.AddWithValue("@Email", Trim(txtPatEmail.Text))
        cmdupsert.Parameters.AddWithValue("@Cell", PhoneNeat(txtPatCell.Text))
        Try
            cmdupsert.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cnpat.Close()
            cnpat = Nothing
        End Try
        ExecuteSqlProcedure("Update Requisitions set Patient_ID = " &
        Val(txtPatientID.Text) & " where ID = " & Val(txtAccID.Text))
    End Sub

    Private Sub btnPrevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevious.Click
        DisableActions()
        lblEligibility.BackColor = Color.PeachPuff
        If PatientDirty Then
            If txtPatientID.Text <> "" And txtPatLName.Text <> "" And txtPatFName.Text _
            <> "" And IsDate(txtPatDOB.Text) And txtPatSex.Text <> "" And txtPatAdd1.Text <> "" And
            txtPatCity.Text <> "" And txtPatState.Text <> "" And txtPatZip.Text <> "" Then _
            ApplyPatientChanges()
            PatientDirty = False
        End If
        If DxDirty Then
            UpdateDx()
            DxDirty = False
        End If
        If PayerDirty Then
            UpdateTP(Val(txtAccID.Text))
            PayerDirty = False
        End If
        If CommentDirty Then
            UpdateComment()
            CommentDirty = False
        End If

        If lblClient.BackColor = Color.PaleGreen And lbl3P.BackColor = Color.PaleGreen And
        lblPatient.BackColor = Color.PaleGreen And lblCodes.BackColor = Color.PaleGreen Then
            ExecuteSqlProcedure("Update Requisitions set Scrubbed = 1 where ID = " & Val(txtAccID.Text))
        Else
            ExecuteSqlProcedure("Update Requisitions set Scrubbed = 0 where ID = " & Val(txtAccID.Text))
            If InStr(BADACCSS, txtAccID.Text & ",") = 0 Then    'does not exist
                BADACCSS += txtAccID.Text & ","
                If BADACCS(0, UBound(BADACCS, 2)) <> "" Then ReDim _
                BADACCS(UBound(BADACCS, 1), UBound(BADACCS, 2) + 1)
                BADACCS(0, UBound(BADACCS, 2)) = txtAccID.Text
                BADACCS(1, UBound(BADACCS, 2)) = GetTGPMissingDx()
                txtMissingRecs.Text = CStr(Val(txtMissingRecs.Text) + 1)
            End If
        End If
        ''UpdateSave()
        'RsA.MovePrevious()
        'If Not RsA.BOF Then
        '    CurrAcc -= 1
        '    txtNavStatus.Text = CurrAcc & " of " & Accessions
        '    DisplayAccession(RsA.Fields("ID").Value)
        'End If

        If CurrAcc > 1 Then
            CurrAcc -= 1
            txtNavStatus.Text = $"{CurrAcc} of {Accessions}"
            DisplayAccession(dtRecords.Rows(CurrAcc - 1)("ID"))
        End If

        EnableActions()
        checkelig()
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        DisableActions()
        lblEligibility.BackColor = Color.PeachPuff
        If PatientDirty Then
            If txtPatientID.Text <> "" And txtPatLName.Text <> "" And txtPatFName.Text _
            <> "" And IsDate(txtPatDOB.Text) And txtPatSex.Text <> "" And txtPatAdd1.Text <> "" And
            txtPatCity.Text <> "" And txtPatState.Text <> "" And txtPatZip.Text <> "" Then _
            ApplyPatientChanges()
            PatientDirty = False
        End If
        If DxDirty Then
            UpdateDx()
            DxDirty = False
        End If
        If PayerDirty Then
            UpdateTP(Val(txtAccID.Text))
            PayerDirty = False
        End If
        If CommentDirty Then
            UpdateComment()
            CommentDirty = False
        End If
        'UpdateSave()

        If lblClient.BackColor = Color.PaleGreen And lbl3P.BackColor = Color.PaleGreen And
        lblPatient.BackColor = Color.PaleGreen And lblCodes.BackColor = Color.PaleGreen Then
            ExecuteSqlProcedure("Update Requisitions set Scrubbed = 1 where ID = " & Val(txtAccID.Text))
        Else
            ExecuteSqlProcedure("Update Requisitions set Scrubbed = 0 where ID = " & Val(txtAccID.Text))
            If InStr(BADACCSS, txtAccID.Text & ",") = 0 Then    'does not exist
                BADACCSS += txtAccID.Text & ","
                If BADACCS(0, UBound(BADACCS, 2)) <> "" Then ReDim _
                BADACCS(UBound(BADACCS, 1), UBound(BADACCS, 2) + 1)
                BADACCS(0, UBound(BADACCS, 2)) = txtAccID.Text
                BADACCS(1, UBound(BADACCS, 2)) = GetTGPMissingDx()
                txtMissingRecs.Text = CStr(Val(txtMissingRecs.Text) + 1)
            End If
        End If
        'RsA.MoveNext()
        'If Not RsA.EOF Then
        '    CurrAcc += 1
        '    txtNavStatus.Text = CurrAcc & " of " & Accessions
        '    DisplayAccession(RsA.Fields("ID").Value)
        'End If
        If CurrAcc < dtRecords.Rows.Count Then
            CurrAcc += 1
            txtNavStatus.Text = $"{CurrAcc} of {Accessions}"
            DisplayAccession(dtRecords.Rows(CurrAcc - 1)("ID"))
        End If

        btnPrevious.Enabled = True
        btnFirst.Enabled = True
        EnableActions()
        checkelig()
    End Sub

    Private Sub btnLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLast.Click
        DisableActions()
        If PatientDirty Then
            If txtPatientID.Text <> "" And txtPatLName.Text <> "" And txtPatFName.Text _
            <> "" And IsDate(txtPatDOB.Text) And txtPatSex.Text <> "" And txtPatAdd1.Text <> "" And
            txtPatCity.Text <> "" And txtPatState.Text <> "" And txtPatZip.Text <> "" Then _
            ApplyPatientChanges()
            PatientDirty = False
        End If
        If DxDirty Then
            UpdateDx()
            DxDirty = False
        End If
        If PayerDirty Then
            UpdateTP(Val(txtAccID.Text))
            PayerDirty = False
        End If
        If CommentDirty Then
            UpdateComment()
            CommentDirty = False
        End If
        'UpdateSave()

        If lblClient.BackColor = Color.PaleGreen And lbl3P.BackColor = Color.PaleGreen And
        lblPatient.BackColor = Color.PaleGreen And lblCodes.BackColor = Color.PaleGreen Then
            ExecuteSqlProcedure("Update Requisitions set Scrubbed = 1 where ID = " & Val(txtAccID.Text))
        Else
            ExecuteSqlProcedure("Update Requisitions set Scrubbed = 0 where ID = " & Val(txtAccID.Text))
            If InStr(BADACCSS, txtAccID.Text & ",") = 0 Then    'does not exist
                BADACCSS += txtAccID.Text & ","
                If BADACCS(0, UBound(BADACCS, 2)) <> "" Then ReDim _
                BADACCS(UBound(BADACCS, 1), UBound(BADACCS, 2) + 1)
                BADACCS(0, UBound(BADACCS, 2)) = txtAccID.Text
                BADACCS(1, UBound(BADACCS, 2)) = GetTGPMissingDx()
                txtMissingRecs.Text = CStr(Val(txtMissingRecs.Text) + 1)
            End If
        End If
        'RsA.MoveLast()
        'CurrAcc = Accessions
        'txtNavStatus.Text = CurrAcc & " of " & Accessions
        'DisplayAccession(RsA.Fields("ID").Value)

        CurrAcc = dtRecords.Rows.Count
        txtNavStatus.Text = $"{CurrAcc} of {Accessions}"
        DisplayAccession(dtRecords.Rows(CurrAcc - 1)("ID"))


        EnableActions()
        checkelig()
    End Sub

    Private Sub btnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click
        Dim i As Integer
        Dim AccID As String = ""
        Dim TestIDs As String = ""
        If BADACCS(0, 0) <> "" And BADACCS(1, 0) <> "" Then
            For i = 0 To UBound(BADACCS, 2)
                If BADACCS(0, i) <> "" And BADACCS(1, i) <> "" Then
                    AccID = BADACCS(0, i)
                    TestIDs = BADACCS(0, i)

                End If
            Next
        End If

    End Sub

    Private Function IsDuplicateDx(ByVal Dx As String) As Boolean
        Dim i As Integer
        Dim DxCount As Integer = 0
        For i = 0 To dgvDXs.RowCount - 1
            If Trim(dgvDXs.Rows(i).Cells(1).Value) = Dx Then
                DxCount = DxCount + 1
            End If
        Next
        If DxCount > 1 Then
            IsDuplicateDx = True
        Else
            IsDuplicateDx = False
        End If
    End Function

    Private Sub DGVICD9s_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDXs.CellEndEdit
        If e.ColumnIndex = 1 Then
            If Trim(dgvDXs.Rows(e.RowIndex).Cells(1).Value) <> "" Then
                If IsDuplicateDx(dgvDXs.Rows(e.RowIndex).Cells(1).Value) Then
                    MsgBox("Grid already contains the code you just typed")
                    dgvDXs.Rows(e.RowIndex).Cells(1).Value = ""
                Else
                    If Not IsDate(txtAccDate.Text) Then _
                    txtAccDate.Text = Format(Date.Today, SystemConfig.DateFormat)
                    If IsCodeComplete(dgvDXs.Rows(e.RowIndex).Cells(1).Value) Then
                        If e.RowIndex = dgvDXs.RowCount - 1 Then
                            dgvDXs.RowCount += 1
                            dgvDXs.CurrentCell = dgvDXs.Rows(dgvDXs.RowCount - 1).Cells(1)
                        End If
                    Else
                        TCode = dgvDXs.Rows(e.RowIndex).Cells(1).Value
                        If TCode.Length >= 3 Then
                            TCode = frmDiagnosis.ShowDialog
                            If TCode <> "" Then
                                dgvDXs.RowCount += 1
                                dgvDXs.CurrentCell.Value = TCode
                                TCode = ""
                            Else
                                dgvDXs.Rows(e.RowIndex).Cells(1).Value = TCode
                            End If
                        Else
                            MsgBox("Minimum 3 characters required", MsgBoxStyle.Critical, "Prolis")
                            dgvDXs.Rows(e.RowIndex).Cells(1).Value = ""
                        End If
                    End If
                End If
            End If
            DxDirty = True
            UpdateSave()
        End If
    End Sub

    Private Sub txtPatientID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatientID.GotFocus
        txtPatientID.BackColor = FCOLOR
    End Sub

    Private Sub txtPatientID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPatientID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtPatientID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatientID.Validated
        txtPatientID.BackColor = NFCOLOR
        If txtPatientID.Text <> "" Then
            DisplayPatient(Val(txtPatientID.Text))
        Else
            ClearPatient()
        End If
        PatientDirty = True
        UpdateSave()
    End Sub

    Private Sub txtPatLName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatLName.GotFocus
        txtPatLName.BackColor = FCOLOR
    End Sub

    Private Sub txtPatLName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatLName.LostFocus
        txtPatLName.BackColor = NFCOLOR
        PatientDirty = True
        UpdateSave()
    End Sub

    Private Sub txtPatMName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatMName.GotFocus
        txtPatFName.BackColor = FCOLOR
    End Sub

    Private Sub txtPatMName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatMName.LostFocus
        txtPatFName.BackColor = NFCOLOR
        PatientDirty = True
        UpdateSave()
    End Sub

    Private Sub txtPatFName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatFName.GotFocus
        txtPatFName.BackColor = FCOLOR
    End Sub

    Private Sub txtPatFName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatFName.LostFocus
        txtPatFName.BackColor = NFCOLOR
        PatientDirty = True
        UpdateSave()
    End Sub

    Private Sub txtPatSex_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatSex.GotFocus
        txtPatSex.BackColor = FCOLOR
    End Sub

    Private Sub txtPatSex_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatSex.LostFocus
        txtPatSex.BackColor = NFCOLOR
        PatientDirty = True
        UpdateSave()
    End Sub

    Private Sub txtPatDOB_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatDOB.GotFocus
        txtPatDOB.BackColor = FCOLOR
    End Sub

    Private Sub txtPatDOB_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatDOB.LostFocus
        txtPatDOB.BackColor = NFCOLOR
        If Not IsDate(txtPatDOB.Text) Then
            MsgBox("Invalid date.", MsgBoxStyle.Critical, "Prolis")
            txtPatDOB.Text = ""
            txtPatDOB.Focus()
        Else
            PatientDirty = True
            UpdateSave()
        End If
    End Sub

    Private Sub txtPatAdd1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatAdd1.GotFocus
        txtPatAdd1.BackColor = FCOLOR
    End Sub

    Private Sub txtPatAdd1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatAdd1.LostFocus
        txtPatAdd1.BackColor = NFCOLOR
        PatientDirty = True
        UpdateSave()
    End Sub

    Private Sub txtPatAdd2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatAdd2.GotFocus
        txtPatAdd2.BackColor = FCOLOR
    End Sub

    Private Sub txtPatAdd2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatAdd2.LostFocus
        txtPatAdd2.BackColor = NFCOLOR
        PatientDirty = True
        UpdateSave()
    End Sub

    Private Sub txtPatCity_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatCity.GotFocus
        txtPatCity.BackColor = FCOLOR
    End Sub

    Private Sub txtPatCity_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatCity.LostFocus
        txtPatCity.BackColor = NFCOLOR
        PatientDirty = True
        UpdateSave()
    End Sub

    Private Sub txtPatState_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatState.GotFocus
        txtPatState.BackColor = FCOLOR
    End Sub

    Private Sub txtPatState_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatState.LostFocus
        txtPatState.BackColor = NFCOLOR
        PatientDirty = True
        UpdateSave()
    End Sub

    Private Sub txtPatZip_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatZip.GotFocus
        txtPatZip.BackColor = FCOLOR
    End Sub

    Private Sub txtPatZip_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatZip.LostFocus
        txtPatZip.BackColor = FCOLOR
        PatientDirty = True
        UpdateSave()
    End Sub

    Private Sub UpdateSave()
        If AccDirty = True Or PatientDirty = True Or PayerDirty _
        = True Or DxDirty = True Or CommentDirty = True Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If PatientDirty Then
            If txtPatientID.Text <> "" And txtPatLName.Text <> "" And txtPatFName.Text _
            <> "" And IsDate(txtPatDOB.Text) And txtPatSex.Text <> "" And txtPatAdd1.Text <> "" And
            txtPatCity.Text <> "" And txtPatState.Text <> "" And txtPatZip.Text <> "" Then _
            ApplyPatientChanges()
            PatientDirty = False
        End If
        If DxDirty Then
            UpdateDx()
            DxDirty = False
        End If
        If PayerDirty Then
            UpdateTP(Val(txtAccID.Text))
            PayerDirty = False
        End If
        If CommentDirty Then
            UpdateComment()
            CommentDirty = False
        End If
        If txtPayerID.Text <> "" Then _
        UpdateRequisits(GetBillRequisits(Val(txtPayerID.Text)))
        UpdateSave()
    End Sub

    Private Sub DGVICD9s_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDXs.CellContentClick
        If e.ColumnIndex = 2 Then
            If e.RowIndex <> -1 Then
                TCode = dgvDXs.Rows(Curow).Cells(1).Value
                TCode = frmDiagnosis.ShowDialog
                If TCode <> "" Then
                    dgvDXs.Rows(e.RowIndex).Cells(1).Value = TCode
                    TCode = ""
                End If
            End If
        End If
    End Sub

    Private Sub DGVICD9s_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDXs.CellEnter
        Curow = e.RowIndex
    End Sub

    Private Sub txtWorkCmnt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWorkCmnt.GotFocus
        txtWorkCmnt.BackColor = FCOLOR
    End Sub

    Private Sub txtWorkCmnt_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWorkCmnt.LostFocus
        txtWorkCmnt.BackColor = NFCOLOR
        CommentDirty = True
        UpdateSave()
    End Sub

    Private Sub txtPolicy_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPolicy.GotFocus
        txtPolicy.BackColor = FCOLOR
    End Sub

    Private Sub txtPolicy_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPolicy.LostFocus
        txtPolicy.BackColor = NFCOLOR
        PayerDirty = True
        UpdateSave()
    End Sub

    Private Sub txtGroup_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGroup.GotFocus
        txtGroup.BackColor = FCOLOR
    End Sub

    Private Sub txtGroup_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGroup.LostFocus
        txtGroup.BackColor = NFCOLOR
        PayerDirty = True
        UpdateSave()
    End Sub

    Private Sub txtPayerID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPayerID.GotFocus
        txtPayerID.BackColor = FCOLOR
    End Sub

    Private Sub txtPayerID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPayerID.LostFocus
        txtPayerID.BackColor = NFCOLOR
        PayerDirty = True
        UpdateSave()
    End Sub

    Private Sub cmbRelation_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbRelation.GotFocus
        cmbRelation.BackColor = FCOLOR
    End Sub

    Private Sub cmbRelation_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbRelation.LostFocus
        cmbRelation.BackColor = NFCOLOR
    End Sub

    Private Sub cmbRelation_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbRelation.SelectedIndexChanged
        PayerDirty = True
        UpdateSave()
    End Sub

    Private Sub btnDesel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDesel.Click
        Dim i As Integer
        For i = 0 To lstTargets.Items.Count - 1
            lstTargets.SetItemChecked(i, False)
        Next
    End Sub
    Private Sub btnSel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSel.Click
        Dim i As Integer
        For i = 0 To lstTargets.Items.Count - 1
            lstTargets.SetItemChecked(i, True)
        Next
    End Sub
    Private Sub btnPayerLookUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPayerLookUp.Click
        If txtAccID.Text <> "" Then 'Accession loaded
            Dim Payer As String = frmPayerLookup.ShowDialog
            If Payer <> "" Then '10002|NY Medicaid|False
                Dim PayerInfo() As String = Split(Payer, "|")
                If PayerInfo(0) <> txtPayerID.Text Then
                    txtPayerID.Text = PayerInfo(0)
                    txtPayerName.Text = PayerInfo(1)
                    txtPolicy.Text = ""
                End If
            End If
        End If
    End Sub
    'Private Function GetMyUserInfo() As String()
    '    Dim LIC As New LicenseManager.ProlisLicense(odbCS, "Prolis")
    '    Dim UserInfo() As String = {"", ""}
    '    Dim CNP As New ADODB.Connection
    '    CNP.Open(odbCS)
    '    Dim Rs As New ADODB.Recordset
    '    Rs.Open("select * from partners where Name like 'Ability%'",
    '    CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Not Rs.BOF Then
    '        If Rs.Fields("UserName").Value IsNot DBNull.Value _
    '        AndAlso Trim(Rs.Fields("UserName").Value) <> "" Then _
    '        UserInfo(0) = Trim(Rs.Fields("UserName").Value)
    '        '
    '        If Rs.Fields("Password").Value IsNot DBNull.Value _
    '        AndAlso Trim(Rs.Fields("Password").Value) <> "" Then _
    '        UserInfo(1) = LIC.decryptString(Trim(Rs.Fields("Password").Value))
    '    End If
    '    Rs.Close()
    '    Rs = Nothing
    '    Return UserInfo
    'End Function
    Private Function GetMyUserInfo() As String()
        Dim UserInfo As String() = {"", ""}

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT UserName, Password FROM partners WHERE Name LIKE 'Ability%'"

            Using command As New SqlCommand(query, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        If Not IsDBNull(reader("UserName")) AndAlso reader("UserName").ToString().Trim() <> "" Then
                            UserInfo(0) = reader("UserName").ToString().Trim()
                        End If
                        If Not IsDBNull(reader("Password")) AndAlso reader("Password").ToString().Trim() <> "" Then
                            If LIC Is Nothing Then LIC = New LicenseManager.ProlisLicense(connString, "Prolis")
                            UserInfo(1) = LIC.decryptString(reader("Password").ToString().Trim())
                        End If
                    End If
                End Using
            End Using
        End Using

        Return UserInfo
    End Function

    Private Sub btnEECC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEECC.Click
        If txtAccID.Text = "" Then
            Return
        End If
        Cursor.Current = Cursors.WaitCursor
        loading.Show()
        If LIC.EECC = False Then
            MsgBox("Check Eligibility is a subscription based service. In " &
            "order to subscribe to Check Eligibility service, please contact " &
            "American Soft Solutions Corp., support.", MsgBoxStyle.Information, "Prolis")
        Else

            Dim s271 As String = ""
            Dim q As String = "select   Msg  from EBMessages  where Accession_ID = " & txtAccID.Text & " order by MsgDate desc"

            Dim meds = CommonData.ExecuteQuery(q)
            Dim med As String = ""
            For Each row In meds

                If row("Msg").Contains("Active Coverage Health") Then
                    med = row("Msg")
                    Exit For
                End If
            Next
            If med.Contains("Active Coverage Health") Then
                EECC = True
                lblEligibility.BackColor = Color.PaleGreen
                lblEligibility.PerformClick()
            Else
                'Dim CNS As New ADODB.Connection
                'CNS.Open(odbCS)
                'Dim ck As CheckEligibility = New CheckEligibility()
                'Dim result = ck.Eligibility(txtAccID.Text, CNS, odbCS)
                'CNS.Close()
                'If result.Contains("Active Coverage Health") Then
                '    EECC = True
                '    lblEligibility.BackColor = Color.PaleGreen
                '    lblEligibility.PerformClick()


                'Else
                '    lblEligibility.BackColor = Color.PeachPuff
                'End If
                'lblEligibility.BackColor = Color.PeachPuff

                Using connection As New SqlConnection(connString)
                    connection.Open()

                    Dim ck As New CheckEligibility()
                    Dim result As String = ck.Eligibility(txtAccID.Text)

                    If result.Contains("Active Coverage Health") Then
                        EECC = True
                        lblEligibility.BackColor = Color.PaleGreen
                        lblEligibility.PerformClick()
                    Else
                        lblEligibility.BackColor = Color.PeachPuff
                    End If
                End Using

            End If
        End If
        loading.Hide()
        Cursor.Current = Cursors.Default
        checkelig()
    End Sub

    Private Function newfunc() As String
        Dim xmlResponse As String = "<?xml version=""1.0"" encoding=""utf-8""?>" &
    "<soap:Envelope xmlns:soap=""http://www.w3.org/2003/05/soap-envelope"" " &
    "xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" " &
    "xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">" &
    "<soap:Body>" &
    "<COREEnvelopeRealTimeRequestResponse xmlns=""http://www.caqh.org/SOAP/WSDL/CORERule2.2.0.xsd"">" &
    "<COREEnvelopeRealTimeResponse>" &
    "<PayloadType>X12_271_Response_005010X279A1</PayloadType>" &
    "<ProcessingMode>RealTimeMode</ProcessingMode>" &
    "<PayloadID>017061843</PayloadID>" &
    "<TimeStamp>2024-10-17T11:18:44Z</TimeStamp>" &
    "<SenderID>ABILITY</SenderID>" &
    "<ReceiverID>B050005B-1D42-4C48-AD6B-EA00C82DC013</ReceiverID>" &
    "<CORERuleVersion>2.2.0</CORERuleVersion>" &
    "<Payload>&lt;![CDATA[ISA*00*          *00*          *ZZ*IVANS-CS-RT    *ZZ*EO-IVANS-RT    *241017*0618*^*00501*912598058*0*P*:~" &
    "GS*HB*IVANS-CS-RT*EO-IVANS-RT*20241017*0618*912598058*X*005010X279A1~" &
    "ST*271*912598059*005010X279A1~" &
    "BHT*0022*11*017061843*20241017*061844~" &
    "HL*1**20*1~NM1*PR*2*UnitedHealthcare - WEA*****PI*10863~PER*IC**UR*WWW.UHCPROVIDER.COM~HL*2*1*21*1~NM1*1P*2*ONPOINT LAB, LLC*****XX*1861895500~HL*3*2*22*0~NM1*IL*1*THOMPSON*MICHAEL*C***MI*932774998~N3*1530 WAYSIDE DRIVE~N4*TEXAS CITY*TX*77590~DMG*D8*19580907*M~EB*1**30*HN*HMOPOS-AARP MEDICARE ADVANTAGE FROM UHC TX-0015 (H******Y~REF*1L*90123~REF*N6*H4527-037-000~DTP*346*RD8*20240101-20240331~MSG*PCP TO SUBMIT A SPECIALIST REFERRAL~LS*2120~NM1*PR*2*UNITEDHEALTHCARE*****PI*WELM2~N3*P.O. BOX 31362~N4*SALT LAKE CITY*UT*841310362~PER*IC**UR*WWW.UHCPROVIDER.COM~NM1*X3*2*WELLMED MEDICAL MANAGEMENT INC~PER*IC**TE*8777574440~LE*2120~EB*C*IND*30***23*0*****W~DTP*346*RD8*20240101-20240331~EB*C*FAM*30***23*0*****W~DTP*346*RD8*20240101-20240331~EB*G*IND*30*HN**23*3800*****Y~DTP*346*RD8*20240101-20240331~EB*G*FAM*30*HN**23*99999.99*****Y~DTP*346*RD8*20240101-20240331~EB*L~LS*2120~NM1*P3*1*RAFIQ*ADNAN****XX*1831352269~N3*11920 ASTORIA BLVD STE 270~N4*HOUSTON*TX*77089~PER*IC**TE*8329344400*FX*8322808371~PRV*PC*PXC*207R00000X~LE*2120~EB*1**98*********Y~DTP*346*RD8*20240101-20240331~MSG*OFFICE VISIT PCP~EB*1**48*********Y~DTP*346*RD8*20240101-20240331~MSG*INPATIENT HOSPITAL~EB*1**50*********Y~DTP*346*RD8*20240101-20240331~MSG*OUTPATIENT SURGERY~EB*1**33*********Y~DTP*346*RD8*20240101-20240331~MSG*CHIROPRACTIC~EB*1**96^1*********Y~DTP*346*RD8*20240101-20240331~MSG*OFFICE VISIT SPECIALIST~EB*1**47^50*********Y~DTP*346*RD8*20240101-20240331~MSG*OUTPATIENT HOSPITAL~EB*1**MH*********Y~DTP*346*RD8*20240101-20240331~MSG*MENTAL HEALTH INDIVIDUAL VISIT~EB*1**86^UC*********Y~DTP*346*RD8*20240101-20240331~EB*1**PT*********Y~DTP*346*RD8*20240101-20240331~MSG*PHYSICAL THERAPY~EB*A*IND*33***7**0****Y~DTP*346*RD8*20240101-20240331~MSG*CHIROPRACTIC~EB*A*IND*9***27**0****Y~DTP*346*RD8*20240101-20240331~MSG*VIRTUAL VISITS/TELEMEDICINE~EB*A*IND*86^UC***7**0****W~DTP*346*RD8*20240101-20240331~EB*A*IND*48***36**0****Y~DTP*346*RD8*20240101-20240331~MSG*INPATIENT HOSPITAL~EB*A*IND*96***7**0****Y~DTP*346*RD8*20240101-20240331~MSG*OFFICE VISIT SPECIALIST~EB*A*IND*50***7**0****Y~DTP*346*RD8*20240101-20240331~MSG*OUTPATIENT SURGERY~EB*A*IND*PT***7**0****Y~DTP*346*RD8*20240101-20240331~MSG*PHYSICAL THERAPY~EB*A*IND*50***7**0****Y~DTP*346*RD8*20240101-20240331~MSG*OUTPATIENT HOSPITAL~EB*A*IND*98***27**0****Y~DTP*346*RD8*20240101-20240331~MSG*OFFICE VISIT PCP~EB*B*IND*UC***7*40*****W~DTP*346*RD8*20240101-20240331~EB*B*IND*PT***7*20*****Y~DTP*346*RD8*20240101-20240331~MSG*PHYSICAL THERAPY~EB*B*IND*48***36*350*****Y~DTP*346*RD8*20240101-20240331~MSG*INPATIENT HOSPITAL~EB*B*IND*98***27*0*****Y~DTP*346*RD8*20240101-20240331~MSG*OFFICE VISIT PCP~EB*B*IND*86***7*135*****W~DTP*346*RD8*20240101-20240331~EB*B*IND*50***7*250*****Y~DTP*346*RD8*20240101-20240331~MSG*OUTPATIENT HOSPITAL~EB*B*IND*33***7*15*****Y~DTP*346*RD8*20240101-20240331~MSG*CHIROPRACTIC~EB*B*IND*50***7*250*****Y~DTP*346*RD8*20240101-20240331~MSG*OUTPATIENT SURGERY~EB*B*IND*96***7*25*****Y~DTP*346*RD8*20240101-20240331~MSG*OFFICE VISIT SPECIALIST~EB*B*IND*9***27*0*****Y~DTP*346*RD8*20240101-20240331~MSG*VIRTUAL VISITS/TELEMEDICINE~EB*1**30**SPECTERA VISION PLAN~DTP*346*RD8*20240101-20240331~LS*2120~NM1*PR*2*UnitedHealthcare - WEA*****PI*10863~N3*PO BOX 30978~N4*SALT LAKE CITY*UT*841300978~LE*2120~EB*C*IND*30***23*0*****W~DTP*346*RD8*20240101-20240331~EB*C*FAM*30***23*0*****W~DTP*346*RD8*20240101-20240331~EB*G*IND*30***23*0*****W~DTP*346*RD8*20240101-20240331~EB*G*FAM*30***23*0*****W~DTP*346*RD8*20240101-20240331~EB*1**AL~DTP*346*RD8*20240101-20240331~EB*U**35~LS*2120~NM1*VN*2*UNITEDHEALTHCARE DENTAL~PER*IC**UR*WWW.DBP.COM~LE*2120~EB*U**88~LS*2120~NM1*VN*2*OPTUMRX~PER*IC**UR*PROFESSIONALS.OPTUMRX.COM~LE*2120~EB*X~LS*2120~NM1*1P*2*ONPOINT LAB, LLC*****XX*1861895500~LE*2120~" &
    "SE*153*912598059~" &
    "GE*1*912598058~" &
    "IEA*1*912598058~" &
    "]]&gt;</Payload>" &
    "<ErrorCode>Success</ErrorCode>" &
    "<ErrorMessage /></COREEnvelopeRealTimeResponse>" &
    "</COREEnvelopeRealTimeRequestResponse>" &
    "</soap:Body>" &
    "</soap:Envelope>"
        Return xmlResponse
    End Function
    Private Sub checkelig()
        loading.Show()
        If LIC.EECC = False Then
            'MsgBox("Check Eligibility is a subscription based service. In " &
            '"order to subscribe to Check Eligibility service, please contact " &
            '"American Soft Solutions Corp., support.", MsgBoxStyle.Information, "Prolis")
        Else
            Dim s271 As String = ""
            Dim q As String = "select top 1 Msg  from EBMessages  where Accession_ID = " & txtAccID.Text & " order by MsgDate desc"

            Dim meds = CommonData.ExecuteQuery(q)
            Dim med As String = ""
            For Each row In meds
                med = row("Msg")
            Next
            If med.Contains("Active Coverage Health") Then
                EECC = True
                lblEligibility.BackColor = Color.PaleGreen
            Else
                lblEligibility.BackColor = Color.PeachPuff
            End If
        End If
        loading.Hide()
    End Sub
    Private Sub lblEligibility_Click_1(sender As Object, e As EventArgs) Handles lblEligibility.Click
        If txtAccID.Text = "" Then
            Return
        End If
        Dim q As String = "select top 1 xmlResponse  from EBMessages  where Accession_ID = " & txtAccID.Text & " order by MsgDate desc"

        Dim meds = CommonData.ExecuteQuery(q)
        Dim med As String = ""
        For Each row In meds
            med = row("xmlResponse")
        Next
        'EligDetails.PatientName.Text = "Patient Name: " + txtPatLName.Text + " " + txtPatFName.Text
        'EligDetails.AccID.Text = "Accession: " + txtAccID.Text
        Dim ckd = New CheckEligibility()
        frmHTML_VIEW.Close()
        frmHTML_VIEW.WebBrowser1.DocumentText = ckd.GetAndParseX12_271(med)
        frmHTML_VIEW.MdiParent = frmDashboard
        frmHTML_VIEW.Show()
    End Sub

    Private Sub dtpDateFrom_CloseUp(sender As Object, e As EventArgs) Handles dtpDateFrom.CloseUp
        ' After selecting a valid date, revert to the standard date format
        CloseUpDateTimePicker(dtpDateFrom)
    End Sub
    Private Sub dtpDateTo_CloseUp(sender As Object, e As EventArgs) Handles dtpDateTo.CloseUp
        CloseUpDateTimePicker(dtpDateTo)
        '' After selecting a valid date, revert to the standard date format
        'If dtpDateTo.Value <> DateTime.MinValue Then
        '    dtpDateTo.Format = DateTimePickerFormat.Short  ' Or another format
        '    dtpDateTo.CustomFormat = "dd/MM/yyyy"          ' Adjust the format
        'End If
    End Sub

    Private Sub lblClearDates_Click(sender As Object, e As EventArgs) Handles lblClearDates.Click
        ClearDateTimePicker(dtpDateFrom)
        ClearDateTimePicker(dtpDateTo)
        'dtpDateTo.Value = dtpDateTo.MinDate
        'dtpDateTo.Format = DateTimePickerFormat.Custom
        'dtpDateTo.CustomFormat = " "  ' Clear date placeholder
    End Sub

End Class
