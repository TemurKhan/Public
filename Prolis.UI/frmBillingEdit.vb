Imports System.Collections
Imports System.Globalization
Imports System.Data
Imports Org.BouncyCastle.Asn1.Cms
Imports System.Reflection
Imports Microsoft.Data.SqlClient

Public Class frmBillingEdit
    Private origWidth As Integer
    Private origHeight As Integer
    'Private RsA As New ADODB.Recordset
    Private dtRecords As New DataTable 'alternate of Private RsA As New ADODB.Recordset
    Private Accessions As Integer
    Private AccDirty As Boolean = False
    Private ChargeDirty As Boolean = False
    Private PatientDirty As Boolean = False
    Private DxDirty As Boolean = False
    Private PayerDirty As Boolean = False
    Private SPayerDirty As Boolean = False
    Private CommentDirty As Boolean = False
    Private CurrAcc As Integer
    Public TCode As String
    Private LinePrice As Single
    Private Ch_Bill As Boolean
    Private Ch_Reverse As Boolean
    Private Ch_Acc As Boolean
    Private Ch_TP As Boolean
    Private Ch_Lines As Boolean
    Private Requisits() As Long = {0, 0, 0, 0, 0, 0, 0, 0, 0}
    Private FullBill As Boolean = False
    Private EECC As Boolean = False 'Eligibility

    Private MainQuery As String

    Private Sub BillingEditProgress()
        If cmbSRelation.SelectedIndex > 0 Then
            grpSSubs.Enabled = True
        Else
            grpSSubs.Enabled = False
        End If
        If Ch_Acc = True Or Ch_TP = True Or Ch_Lines = True Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub frmBillingEdit_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'If RsA.State <> 0 Then RsA.Close()
        'RsA = Nothing
    End Sub

    Private Sub frmBillingEdit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MaximumSize = MaxSize
        Dim i As Integer
        lblFrom.Text += " (" & SystemConfig.DateFormat & ")"
        lblTo.Text += " (" & SystemConfig.DateFormat & ")"
        Dim POINTERS() As String = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"}
        cmbBillType.SelectedIndex = 1
        dgvDiscrete.RowCount = 1
        InitializeConfiguration(MyLab.ID)
        cmbABU.SelectedIndex = 3
        DGVICD9s.RowCount = 12
        For i = 0 To DGVICD9s.RowCount - 1
            DGVICD9s.Rows(i).Cells(0).Value = POINTERS(i)
        Next
        dgvCharges.RowCount = 1
        PopulatePayers()
        chkBillDate.Checked = True
        txtDueDays.Text = SystemConfig.DueDays.ToString
        cmbPatPrice.SelectedIndex = SystemConfig.PatientPriceLevel
        btnSave.Enabled = False

        txtPatHPhone.Mask = SystemConfig.PhoneMask
        txtPatCell.Mask = SystemConfig.PhoneMask
        txtProvPhone.Mask = SystemConfig.PhoneMask
        txtProvFax.Mask = SystemConfig.PhoneMask

        ConfigureDateTimePicker(dtpDateFrom)
        ConfigureDateTimePicker(dtpDateTo)

        ' Temp code to repair billing
        'Dim Invoices() As String = GetDetailInvoices()
        'Dim DUPS As String = ""
        'For i = 0 To Invoices.Length - 1
        '    Try
        '        CN.Execute("Insert into Charges (ID, Accession_ID, Ar_ID, ArType, IsPrimary, BillReason, " & _
        '         "NetAmount, TaxAmount, GrossAmount, Bill_Date, Svc_Date, Due_Date, Term, Note, ECC, Output, " & _
        '         "LastEditedOn, EditedBy) " & _
        '         "Select b.Charge_ID, b.Accession_ID, c.PrimePayer_ID, c.BillingType_ID, 1, '', Round((Select " & _
        '         "Sum(d.Extend) from Charge_Detail d where d.Charge_ID = b.Charge_ID), 2), 0, Round((Select " & _
        '         "Sum(d.Extend) from Charge_Detail d where d.Charge_ID = b.Charge_ID), 2), b.Created_On, (Select " & _
        '         "Min(SourceDate) from Specimens where Accession_ID = b.Accession_ID), DateAdd(dd, 15, b.Created_On), " & _
        '         "'Net 15 Days', '', 1, 1, b.Created_On, b.Created_By from Accession_Charge b inner join Requisitions " & _
        '         "c on c.ID = b.Accession_ID where b.Charge_ID = " & Invoices(i))
        '    Catch ex As Exception
        '        DUPS += Invoices(i) & ", "
        '    End Try
        'Next
        'Printer.Print("Processed: " & Invoices.Length.ToString & vbCrLf & _
        '"( " & Invoices(0) & " To " & Invoices(i - 1) & " )" & vbCrLf & "Exceptions:" & vbCrLf & DUPS)
        'DUPS = ""
        '
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub PopulatePayers()
        cmbPayer.Items.Clear()
        Dim sSQL As String = "Select * from Payers where Active <> 0 order by PayerName"
        '
        Dim cnn As New SqlConnection(connString)
        cnn.Open()
        Dim cmdsel As New SqlCommand(sSQL, cnn)
        cmdsel.CommandType = Data.CommandType.Text
        Dim DRsel As SqlDataReader = cmdsel.ExecuteReader
        If DRsel.HasRows Then
            While DRsel.Read
                cmbPayer.Items.Add(New MyList(DRsel("PayerName"), DRsel("ID")))
                cmbSIns.Items.Add(New MyList(DRsel("PayerName"), DRsel("ID")))
            End While
        End If
        cnn.Close()
        cnn = Nothing
    End Sub

    Private Sub chkClientBill_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkClientBill.CheckedChanged
        If chkClientBill.Checked = False Then
            chkClientBill.Text = "No"
        Else
            chkClientBill.Text = "Yes"
        End If
    End Sub

    Private Sub chkTPBill_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTPBill.CheckedChanged
        If chkTPBill.Checked = False Then
            chkTPBill.Text = "No"
        Else
            chkTPBill.Text = "Yes"
        End If
    End Sub

    Private Sub chkPatientBill_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPatientBill.CheckedChanged
        If chkPatientBill.Checked = False Then
            chkPatientBill.Text = "No"
        Else
            chkPatientBill.Text = "Yes"
        End If
    End Sub

    Private Sub txtCopay_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCopay.KeyPress
        Prices(txtCopay, e)
    End Sub

    Private Sub txtAccFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccFrom.KeyPress
        Numerals(sender, e)
    End Sub

    'Private Sub txtDateFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If UserEnteredText(txtDateFrom) <> "" Then
    '        If IsDate(txtDateFrom.Text) = True Then
    '            txtAccFrom.Text = ""
    '            txtAccTo.Text = ""
    '            dgvDiscrete.Rows.Clear()
    '            dgvDiscrete.RowCount = 1
    '        Else
    '            MsgBox("Invalid Date")
    '            txtDateFrom.Text = ""
    '            txtDateFrom.Focus()
    '        End If
    '    End If
    'End Sub

    'Private Sub txtDateTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If UserEnteredText(txtDateTo) <> "" Then
    '        If IsDate(txtDateTo.Text) = True Then
    '            txtAccFrom.Text = ""
    '            txtAccTo.Text = ""
    '            dgvDiscrete.Rows.Clear()
    '            dgvDiscrete.RowCount = 1
    '        Else
    '            MsgBox("Invalid Date")
    '            txtDateTo.Text = ""
    '            txtDateTo.Focus()
    '        End If
    '    End If
    'End Sub

    Private Sub txtAccTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccTo.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAccFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccFrom.Validated
        If txtAccFrom.Text <> "" Then
            'txtDateFrom.Text = ""
            'txtDateTo.Text = ""
            ClearDateTimePicker(dtpDateFrom)
            ClearDateTimePicker(dtpDateTo)

            dgvDiscrete.Rows.Clear()
            dgvDiscrete.RowCount = 1
        End If
    End Sub

    Private Sub chkBillDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBillDate.CheckedChanged
        If chkBillDate.Checked = False Then
            chkBillDate.Text = "Service Date"
            txtDueDate.Text = ""
            txtBillDate.Text = ""
            txtBillDate.Enabled = False
            txtDueDate.Enabled = False
        Else
            chkBillDate.Text = "Specify"
            txtBillDate.Text = Format(Date.Today, SystemConfig.DateFormat)
            txtDueDate.Text = Format(DateAdd(DateInterval.Day, Val(txtDueDays.Text),
            CDate(txtBillDate.Text)), SystemConfig.DateFormat)
            txtDueDate.Enabled = True
            txtBillDate.Enabled = True
        End If
    End Sub

    Private Sub txtDueDays_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDueDays.Validated
        If txtDueDays.Text = "" Then txtDueDays.Text = "15"
        txtTerm.Text = "Net " & txtDueDays.Text & " Days"
        If chkBillDate.Checked = True Then
            txtDueDate.Text = Format(CDate(txtBillDate.Text).AddDays(Val(txtDueDays.Text)), SystemConfig.DateFormat)
        Else
            txtDueDate.Text = ""
        End If
    End Sub

    Private Sub DGVICD9s_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVICD9s.CellEndEdit
        Try
            If e.ColumnIndex = 1 Then
                If Trim(DGVICD9s.Rows(e.RowIndex).Cells(1).Value) <> "" Then
                    If IsDuplicateDx(DGVICD9s.Rows(e.RowIndex).Cells(1).Value) Then
                        MsgBox("Grid already contains the code you just typed")
                        DGVICD9s.Rows(e.RowIndex).Cells(1).Value = ""
                    Else
                        If IsCodeComplete(Trim(DGVICD9s.Rows(e.RowIndex).Cells(1).Value)) Then
                            If e.RowIndex = DGVICD9s.RowCount - 1 Then
                                DGVICD9s.RowCount += 1
                                DGVICD9s.CurrentCell = DGVICD9s.Rows(DGVICD9s.RowCount - 1).Cells(1)
                            End If
                        Else
                            TCode = Trim(DGVICD9s.Rows(e.RowIndex).Cells(1).Value)
                            If TCode.Length >= 3 Then
                                TCode = frmDiagnosis.ShowDialog
                                If TCode <> "" Then
                                    DGVICD9s.RowCount += 1
                                    DGVICD9s.CurrentCell.Value = TCode
                                    TCode = ""
                                Else
                                    DGVICD9s.Rows(e.RowIndex).Cells(1).Value = TCode
                                End If
                            Else
                                MsgBox("Minimum 3 characters required", MsgBoxStyle.Critical, "Prolis")
                                DGVICD9s.Rows(e.RowIndex).Cells(1).Value = ""
                            End If
                        End If
                    End If
                Else

                End If
            End If
            'If DGVICD9s.Rows(e.RowIndex).Cells(1).Value <> "" Then
            '    If e.RowIndex = DGVICD9s.RowCount - 1 Then
            '        DGVICD9s.RowCount += 1
            '        DGVICD9s.Rows(DGVICD9s.RowCount - 1).Cells(0).Value = DGVICD9s.RowCount
            '        SendKeys.SendWait("{DOWN}")
            '    End If
            'End If
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
        Ch_Acc = True
        DxDirty = True
        BillingEditProgress()
    End Sub

    Private Function IsDuplicateDx(ByVal Dx As String) As Boolean
        Dim i As Integer
        Dim DxCount As Integer = 0
        For i = 0 To DGVICD9s.RowCount - 1
            If Trim(DGVICD9s.Rows(i).Cells(1).Value) = Dx Then
                DxCount = DxCount + 1
            End If
        Next
        If DxCount > 1 Then
            IsDuplicateDx = True
        Else
            IsDuplicateDx = False
        End If
    End Function

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        DisableActions()

        lblEligibility.BackColor = Color.PeachPuff
        Corrected.Checked = False
        VoidClaim.Checked = False
        conractS.Checked = False
        Dim sSQL As String = ""
        Dim i As Integer
        If (IsDate(dtpDateFrom.Text) Or IsDate(dtpDateTo.Text) Or txtAccFrom.Text <> "" Or
        txtAccTo.Text <> "") Or HasDiscretes() And lstTargets.CheckedItems.Count > 0 Then
            If cmbABU.SelectedIndex = 0 Then    'All
                sSQL = "Select ID from Requisitions where ID in (Select distinct " &
                "Accession_ID from Req_Billable)"
            ElseIf cmbABU.SelectedIndex = 1 Then    'Billed
                sSQL = "Select ID from Requisitions where ID in (Select distinct " &
                "Accession_ID from Req_Billable where Bill_Status = 'B') and Not ID in " &
                "(Select distinct Accession_ID from Req_Billable where Bill_Status in " &
                "('U', 'H')) and Not ID in (Select distinct Accession_ID from Charges " &
                "where ID in (Select distinct Charge_ID from Payment_Detail))"
            ElseIf cmbABU.SelectedIndex = 2 Then    'Part Billed
                sSQL = "Select ID from Requisitions where ID in (Select distinct " &
                "Accession_ID from Req_Billable where Not Bill_Status in ('U', 'H')) " &
                "and ID in (Select distinct Accession_ID from Req_Billable where " &
                "Bill_Status in ('U', 'H'))"
            Else    'Unbilled
                sSQL = "Select ID from Requisitions where ID in (Select distinct " &
                "Accession_ID from Req_Billable where Bill_Status <> 'B') and Not " &
                "ID in (Select distinct Accession_ID from Req_Billable where " &
                "Bill_Status = 'B')"
            End If
            '
            If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                sSQL += " and AccessionDate between '" & dtpDateFrom.Text &
                "' and '" & dtpDateFrom.Text & " 23:59:00'"
            ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                sSQL += " and AccessionDate between '" & dtpDateTo.Text &
                "' and '" & dtpDateTo.Text & " 23:59:00'"
            ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                sSQL += " and AccessionDate between '" & dtpDateFrom.Text &
                "' and '" & dtpDateTo.Text & " 23:59:00'"
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                If chkAI.Checked = False Then
                    sSQL += " and ID = " & Val(txtAccFrom.Text)
                Else
                    sSQL += " and ID in (Select Accession_ID from Charges " &
                    "where ID = " & Val(txtAccFrom.Text) & ")"
                End If
            ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                If chkAI.Checked = False Then
                    sSQL += " and ID = " & Val(txtAccTo.Text)
                Else
                    sSQL += " and ID in (Select Accession_ID from Charges " &
                    "where ID = " & Val(txtAccTo.Text) & ")"
                End If
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                If Val(txtAccFrom.Text) < Val(txtAccTo.Text) Then
                    If chkAI.Checked = False Then
                        sSQL += " and ID between " & Val(txtAccFrom.Text) &
                        " and " & Val(txtAccTo.Text)
                    Else
                        sSQL += " and ID in (Select Accession_ID from Charges " &
                        "where ID between " & Val(txtAccFrom.Text) & " and " &
                        Val(txtAccTo.Text) & ")"
                    End If
                ElseIf Val(txtAccFrom.Text) > Val(txtAccTo.Text) Then
                    If chkAI.Checked = False Then
                        sSQL += " and ID between " & Val(txtAccTo.Text) &
                        " and " & Val(txtAccFrom.Text)
                    Else
                        sSQL += " and ID in (Select Accession_ID from Charges " &
                        "where ID between " & Val(txtAccTo.Text) & " and " &
                        Val(txtAccFrom.Text) & ")"
                    End If
                Else
                    If chkAI.Checked = False Then
                        sSQL += " and ID = " & Val(txtAccFrom.Text)
                    Else
                        sSQL += " and ID in (Select Accession_ID from Charges " &
                        "where ID = " & Val(txtAccFrom.Text) & ")"
                    End If
                End If
            ElseIf HasDiscretes() Then
                Dim VALS As String = ""
                For i = 0 To dgvDiscrete.RowCount - 1
                    If dgvDiscrete.Rows(i).Cells(0).Value IsNot Nothing _
                    AndAlso Trim(dgvDiscrete.Rows(i).Cells(0).Value) <> "" Then
                        VALS += Trim(dgvDiscrete.Rows(i).Cells(0).Value) & ", "
                    End If
                Next
                If VALS.EndsWith(", ") Then VALS = Microsoft.VisualBasic.Mid(VALS, 1, Len(VALS) - 2)
                If VALS <> "" Then
                    If chkAI.Checked = False Then
                        sSQL += " and ID in (" & VALS & ")"
                    Else
                        sSQL += " and ID in (Select Accession_ID from Charges " &
                        "where ID in (" & VALS & "))"
                    End If
                End If
            End If
            '
            Dim ItemX As MyList
            If cmbBillType.SelectedIndex = 0 Then   'Provider
                sSQL += " and BillingType_ID = 0 and OrderingProvider_ID in("
                For i = 0 To lstTargets.CheckedItems.Count - 1
                    ItemX = lstTargets.CheckedItems(i)
                    sSQL += ItemX.ItemData.ToString & ","
                Next
                sSQL = Microsoft.VisualBasic.Mid(sSQL, 1, Len(sSQL) - 1) & ")"
            ElseIf cmbBillType.SelectedIndex = 1 Then   'TP
                sSQL += " and BillingType_ID = 1"
                If lstTargets.CheckedItems.Count > 0 Then
                    sSQL += " and ID in(Select Accession_ID " &
                    "from Req_Coverage where Preference = 'P' and Payer_ID in("
                    For i = 0 To lstTargets.CheckedItems.Count - 1
                        ItemX = lstTargets.CheckedItems(i)
                        sSQL += ItemX.ItemData.ToString & ","
                    Next
                    sSQL = Microsoft.VisualBasic.Mid(sSQL, 1, Len(sSQL) - 1) & "))"
                End If
            Else 'Patient
                sSQL += " and BillingType_ID = 2"
                If lstTargets.CheckedItems.Count > 0 Then
                    sSQL += " and Patient_ID in ("
                    For i = 0 To lstTargets.CheckedItems.Count - 1
                        ItemX = lstTargets.CheckedItems(i)
                        sSQL += ItemX.ItemData.ToString & ","
                    Next
                    sSQL = Microsoft.VisualBasic.Mid(sSQL, 1, Len(sSQL) - 1) & ")"
                End If
            End If
            If sSQL <> "" Then sSQL += " order by ID"

            MainQuery = sSQL
            FillData(MainQuery)

            If dtRecords.Rows.Count > 0 Then ' Old: If Not RsA.BOF Then
                Dim CurrAcc As Integer = 1
                Dim Accessions As Integer = dtRecords.Rows.Count ' Old: Accessions = RsA.RecordCount

                txtNavStatus.Text = CurrAcc & " of " & Accessions ' Old: txtNavStatus.Text = CurrAcc & " of " & Accessions

                If Accessions > 1 Then ' Old: If Accessions > 1 Then
                    btnNext.Enabled = True ' Old: btnNext.Enabled = True
                    btnLast.Enabled = True ' Old: btnLast.Enabled = True
                End If

                DisplayAccession(dtRecords.Rows(0)("ID").ToString()) ' Old: DisplayAccession(RsA.Fields("ID").Value)
            Else
                txtNavStatus.Text = "0 of 0" ' Old: txtNavStatus.Text = "0 of 0"
                btnNext.Enabled = False ' Old: btnNext.Enabled = False
                btnLast.Enabled = False ' Old: btnLast.Enabled = False
                ClearAccession() ' Old: ClearAccession()
            End If

            'CNB.Close()
            'CNB = Nothing
        End If
        EnableActions()
        checkelig()
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

    Private Sub checkelig()
        loading.Show()
        If LIC.EECC = False Then
            'MsgBox("Check Eligibility is a subscription based service. In " &
            '"order to subscribe to Check Eligibility service, please contact " &
            '"American Soft Solutions Corp., support.", MsgBoxStyle.Information, "Prolis")
        Else
            Dim s271 As String = ""
            Dim q As String = "select top 1 Msg  from EBMessages  where Accession_ID = " & txtAccessionID.Text & " order by MsgDate desc"

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
    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        DisableActions()
        lblEligibility.BackColor = Color.PeachPuff
        orgChargID.Text = ""
        conractS.Checked = False
        If AccDirty Then
            UpdateAccession(Val(txtAccessionID.Text))
            AccDirty = False
        End If
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
        If ChargeDirty Then
            For i As Integer = 0 To dgvCharges.RowCount - 1
                UpdateBillable(i, dgvCharges.Rows(i).Cells(14).Value)
            Next
            ChargeDirty = False
        End If
        If PayerDirty Then
            UpdateTP(Val(txtAccessionID.Text))
            PayerDirty = False
        End If

        If SPayerDirty Then
            If Not String.IsNullOrEmpty(txtSSubID.Text) Then
                SaveReqSCoverage(Val(txtAccessionID.Text), txtSSubID.Text)
            End If

            SPayerDirty = False
        End If
        If CommentDirty Then
            UpdateBComments()
            CommentDirty = False
        End If

        'RsA.MoveNext()
        'If Not RsA.EOF Then
        '    CurrAcc += 1
        '    txtNavStatus.Text = CurrAcc & " of " & Accessions
        '    DisplayAccession(RsA.Fields("ID").Value)
        'End If

        ' Assuming DtRecords is the DataTable already populated with data from the database
        If CurrAcc < dtRecords.Rows.Count Then
            CurrAcc += 1 ' Increment the current record index
            txtNavStatus.Text = CurrAcc & " of " & dtRecords.Rows.Count ' Update navigation status
            DisplayAccession(dtRecords.Rows(CurrAcc - 1)("ID").ToString()) ' Display the current accession
        End If

        btnPrevious.Enabled = True
        btnFirst.Enabled = True
        'If RsA.EOF Then
        '    RsA.MoveLast()
        '    CurrAcc = Accessions
        '    btnNext.Enabled = False
        '    btnLast.Enabled = False
        'Else
        '    btnNext.Enabled = True
        '    btnLast.Enabled = True
        'End If
        If Corrected.Checked Or VoidClaim.Checked Then
            If orgChargID.Text = "" Then
                MessageBox.Show("Please enter orignal claim number.")
                Return
            Else
                Dim invid = cmbInvoices.SelectedItem

                If Corrected.Checked Then
                    ExecuteSqlProcedure("update charges set Billing_Status_Code ='7' ,Orignal_Claim_Number= '" & orgChargID.Text & "' where Id=" & invid)
                ElseIf VoidClaim.Checked Then
                    ExecuteSqlProcedure("update charges set Billing_Status_Code ='8' ,Orignal_Claim_Number= '" & orgChargID.Text & "' where Id=" & invid)

                End If

            End If

        End If
        EnableActions()
        checkelig()
    End Sub

    Private Sub btnLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLast.Click
        DisableActions()
        lblEligibility.BackColor = Color.PeachPuff
        Corrected.Checked = False
        VoidClaim.Checked = False
        conractS.Checked = False
        orgChargID.Text = ""
        If AccDirty Then
            UpdateAccession(Val(txtAccessionID.Text))
            AccDirty = False
        End If
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
        If ChargeDirty Then
            For i As Integer = 0 To dgvCharges.RowCount - 1
                UpdateBillable(i, dgvCharges.Rows(i).Cells(14).Value)
            Next
            ChargeDirty = False
        End If
        If PayerDirty Then
            UpdateTP(Val(txtAccessionID.Text))
            PayerDirty = False
        End If

        If SPayerDirty Then
            If Not String.IsNullOrEmpty(txtSSubID.Text) Then
                SaveReqSCoverage(Val(txtAccessionID.Text), txtSSubID.Text)
            End If

            SPayerDirty = False
        End If
        If CommentDirty Then
            UpdateBComments()
            CommentDirty = False
        End If
        'RsA.MoveLast()
        'CurrAcc = Accessions
        'txtNavStatus.Text = CurrAcc & " of " & Accessions
        'DisplayAccession(RsA.Fields("ID").Value)

        CurrAcc = dtRecords.Rows.Count ' Old: RsA.MoveLast() and Accessions calculation
        txtNavStatus.Text = CurrAcc & " of " & dtRecords.Rows.Count ' Old: txtNavStatus.Text = CurrAcc & " of " & Accessions

        ' Display the last accession from DtRecords
        DisplayAccession(dtRecords.Rows(CurrAcc - 1)("ID").ToString()) ' Old: DisplayAccession(RsA.Fields("ID").Value)

        'btnPrevious.Enabled = True
        'btnFirst.Enabled = True
        'btnNext.Enabled = False
        'btnLast.Enabled = False
        EnableActions()
        checkelig()
    End Sub

    Private Sub btnPrevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevious.Click
        DisableActions()
        lblEligibility.BackColor = Color.PeachPuff
        orgChargID.Text = ""
        Corrected.Checked = False
        conractS.Checked = False
        VoidClaim.Checked = False
        orgChargID.Text = ""
        If AccDirty Then
            UpdateAccession(Val(txtAccessionID.Text))
            AccDirty = False
        End If
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
        If ChargeDirty Then
            For i As Integer = 0 To dgvCharges.RowCount - 1
                UpdateBillable(i, dgvCharges.Rows(i).Cells(14).Value)
            Next
            ChargeDirty = False
        End If
        If PayerDirty Then
            UpdateTP(Val(txtAccessionID.Text))
            PayerDirty = False
        End If

        If SPayerDirty Then
            If Not String.IsNullOrEmpty(txtSSubID.Text) Then
                SaveReqSCoverage(Val(txtAccessionID.Text), txtSSubID.Text)
            End If

            SPayerDirty = False
        End If
        If CommentDirty Then
            UpdateBComments()
            CommentDirty = False
        End If

        'RsA.MovePrevious()
        'If Not RsA.BOF Then
        '    CurrAcc -= 1
        '    txtNavStatus.Text = CurrAcc & " of " & Accessions
        '    DisplayAccession(RsA.Fields("ID").Value)
        'End If

        If CurrAcc > 1 Then ' Old: If Not RsA.BOF Then
            CurrAcc -= 1 ' Old: RsA.MovePrevious()
            txtNavStatus.Text = CurrAcc & " of " & dtRecords.Rows.Count ' Old: txtNavStatus.Text = CurrAcc & " of " & Accessions
            DisplayAccession(dtRecords.Rows(CurrAcc - 1)("ID").ToString()) ' Old: DisplayAccession(RsA.Fields("ID").Value)
        End If

        'btnNext.Enabled = True
        'btnLast.Enabled = True
        'If RsA.BOF Then
        '    RsA.MoveFirst()
        '    CurrAcc = 1
        '    btnPrevious.Enabled = False
        '    btnFirst.Enabled = False
        'End If
        If Corrected.Checked Or VoidClaim.Checked Then
            If orgChargID.Text = "" Then
                MessageBox.Show("Please enter orignal claim number.")
                Return
            Else
                Dim invid = cmbInvoices.SelectedItem

                If Corrected.Checked Then

                    ExecuteSqlProcedure("update charges set Billing_Status_Code ='7' ,Orignal_Claim_Number= '" & orgChargID.Text & "' where Id=" & invid)
                ElseIf VoidClaim.Checked Then
                    'ExecuteSqlProcedure("update charges set Billing_Status_Code ='" & "VOID REF=" & orgChargID.Text & "' where Id=" & invid)


                    ExecuteSqlProcedure("update charges set Billing_Status_Code ='8' ,Orignal_Claim_Number= '" & orgChargID.Text & "' where Id=" & invid)
                End If

            End If

        End If
        EnableActions()
        checkelig()
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

    'Private Sub EnableActions()
    '    btnSave.Enabled = True
    '    btnTarget.Enabled = True
    '    btnLoad.Enabled = True
    '    If RsA.State = 1 Then
    '        If Not RsA.BOF And Not RsA.EOF Then     'in the middle
    '            btnFirst.Enabled = True
    '            btnNext.Enabled = True
    '            btnPrevious.Enabled = True
    '            btnLast.Enabled = True
    '        ElseIf RsA.BOF And RsA.RecordCount > 0 Then
    '            btnFirst.Enabled = False
    '            btnNext.Enabled = True
    '            btnPrevious.Enabled = False
    '            btnLast.Enabled = True
    '        ElseIf RsA.EOF And RsA.RecordCount > 0 Then
    '            btnFirst.Enabled = True
    '            btnNext.Enabled = False
    '            btnPrevious.Enabled = True
    '            btnLast.Enabled = True
    '        Else
    '            btnFirst.Enabled = False
    '            btnNext.Enabled = False
    '            btnPrevious.Enabled = False
    '            btnLast.Enabled = False
    '        End If
    '    End If
    'End Sub

    Private Sub EnableActions()
        btnSave.Enabled = True ' Old: btnSave.Enabled = True
        btnTarget.Enabled = True ' Old: btnTarget.Enabled = True
        btnLoad.Enabled = True ' Old: btnLoad.Enabled = True

        If dtRecords.Rows.Count > 0 Then ' Old: If RsA.State = 1 Then
            If CurrAcc > 1 And CurrAcc < dtRecords.Rows.Count Then ' Old: If Not RsA.BOF And Not RsA.EOF Then 'in the middle
                btnFirst.Enabled = True ' Old: btnFirst.Enabled = True
                btnNext.Enabled = True ' Old: btnNext.Enabled = True
                btnPrevious.Enabled = True ' Old: btnPrevious.Enabled = True
                btnLast.Enabled = True ' Old: btnLast.Enabled = True
            ElseIf CurrAcc = 1 Then ' Old: RsA.BOF And RsA.RecordCount > 0
                btnFirst.Enabled = False ' Old: btnFirst.Enabled = False
                btnNext.Enabled = True ' Old: btnNext.Enabled = True
                btnPrevious.Enabled = False ' Old: btnPrevious.Enabled = False
                btnLast.Enabled = True ' Old: btnLast.Enabled = True
            ElseIf CurrAcc = dtRecords.Rows.Count Then ' Old: RsA.EOF And RsA.RecordCount > 0
                btnFirst.Enabled = True ' Old: btnFirst.Enabled = True
                btnNext.Enabled = False ' Old: btnNext.Enabled = False
                btnPrevious.Enabled = True ' Old: btnPrevious.Enabled = True
                btnLast.Enabled = True ' Old: btnLast.Enabled = True
            Else ' Old: RsA.RecordCount = 0
                btnFirst.Enabled = False ' Old: btnFirst.Enabled = False
                btnNext.Enabled = False ' Old: btnNext.Enabled = False
                btnPrevious.Enabled = False ' Old: btnPrevious.Enabled = False
                btnLast.Enabled = False ' Old: btnLast.Enabled = False
            End If
        Else
            btnFirst.Enabled = False ' Old: btnFirst.Enabled = False
            btnNext.Enabled = False ' Old: btnNext.Enabled = False
            btnPrevious.Enabled = False ' Old: btnPrevious.Enabled = False
            btnLast.Enabled = False ' Old: btnLast.Enabled = False
        End If
    End Sub

    Private Sub btnFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirst.Click
        DisableActions()
        lblEligibility.BackColor = Color.PeachPuff
        Corrected.Checked = False
        conractS.Checked = False
        VoidClaim.Checked = False
        If AccDirty Then
            UpdateAccession(Val(txtAccessionID.Text))
            AccDirty = False
        End If
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
        If ChargeDirty Then
            For i As Integer = 0 To dgvCharges.RowCount - 1
                UpdateBillable(i, dgvCharges.Rows(i).Cells(14).Value)
            Next
            ChargeDirty = False
        End If
        If PayerDirty Then
            UpdateTP(Val(txtAccessionID.Text))
            PayerDirty = False
        End If

        If SPayerDirty Then
            If Not String.IsNullOrEmpty(txtSSubID.Text) Then
                SaveReqSCoverage(Val(txtAccessionID.Text), txtSSubID.Text)
            End If

            SPayerDirty = False
        End If
        If CommentDirty Then
            UpdateBComments()
            CommentDirty = False
        End If
        'RsA.MoveFirst()
        'CurrAcc = 1
        'txtNavStatus.Text = CurrAcc & " of " & Accessions
        'DisplayAccession(RsA.Fields("ID").Value)

        CurrAcc = 1 ' Old: RsA.MoveFirst()
        txtNavStatus.Text = CurrAcc & " of " & dtRecords.Rows.Count ' Old: txtNavStatus.Text = CurrAcc & " of " & Accessions
        DisplayAccession(dtRecords.Rows(CurrAcc - 1)("ID").ToString()) ' Old: DisplayAccession(RsA.Fields("ID").Value)

        btnNext.Enabled = True
        btnLast.Enabled = True
        btnPrevious.Enabled = False
        btnFirst.Enabled = False
        If Corrected.Checked Or VoidClaim.Checked Then
            If orgChargID.Text = "" Then
                MessageBox.Show("Please enter orignal claim number.")
                Return
            Else
                Dim invid = cmbInvoices.SelectedItem

                If Corrected.Checked Then

                    ExecuteSqlProcedure("update charges set Billing_Status_Code ='7' ,Orignal_Claim_Number= '" & orgChargID.Text & "' where Id=" & invid)
                ElseIf VoidClaim.Checked Then

                    ExecuteSqlProcedure("update charges set Billing_Status_Code ='8' ,Orignal_Claim_Number= '" & orgChargID.Text & "' where Id=" & invid)
                End If

            End If

        End If
        EnableActions()
        checkelig()
    End Sub

    Private Sub ClearAccession()
        txtAccessionID.Text = ""
        txtSvcDate.Text = ""
        chkIsBilled.Checked = False
        chkIsGratis.Checked = False
        cmbInvoices.Items.Clear()
        txtStatus.Text = ""
        txtStatus.BackColor = Color.LightGray
        txtStatus.ForeColor = Color.Black
        ClearClient()
        ClearPatient()
        ClearThirdPart()
        ClearSecondary()
        dgvCharges.Rows.Clear()
        chkECC.Checked = True
        If chkECC.Checked Then
            chkECC.Text = "837"
        ElseIf Not chkECC.Checked Then
            chkECC.Text = "Paper"
        Else
            chkECC.Checked = True
            chkECC.Text = "837"
        End If
        dgvCharges.RowCount += 1
        txtBCharges.Text = "0.00"
        txtUCharges.Text = "0.00"
        chkBillNow.Checked = False : chkBillNow.Enabled = False
        chkRev.Checked = False : chkRev.Enabled = False
    End Sub

    Private Function GetInvoiceECC(ByVal AccID As Long) As Boolean
        Dim ECC As Boolean = True '837
        Dim cnecc As New SqlConnection(connString)
        cnecc.Open()
        Dim cmdecc As New SqlCommand("Select c.ECC as cECC, p.IsECC as " &
        "pECC from Payers p inner join (Requisitions a left outer join Charges c " &
        "on c.Accession_ID = a.ID) on a.PrimePayer_ID = p.ID where a.BillingType_ID " &
        "= 1 and c.Accession_ID = " & AccID, cnecc)
        cmdecc.CommandType = CommandType.Text
        Dim drecc As SqlDataReader = cmdecc.ExecuteReader
        If drecc.HasRows Then
            While drecc.Read
                If drecc("cECC") IsNot DBNull.Value Then
                    ECC = drecc("cECC")
                Else
                    ECC = drecc("pECC")
                End If
            End While
        End If
        cnecc.Close()
        cnecc = Nothing
        Return ECC
    End Function

    Private Function GetPayerClaim(ByVal AccID As Long) As String
        Dim PC As String = ""
        Dim cngpc As New SqlConnection(connString)
        cngpc.Open()
        Dim cmdgpc As New SqlCommand("Select PayerClaim from " &
        "Accession_Charge where ArType = 1 and Accession_ID = " & AccID, cngpc)
        cmdgpc.CommandType = CommandType.Text
        Dim drgpc As SqlDataReader = cmdgpc.ExecuteReader
        If drgpc.HasRows Then
            While drgpc.Read
                If drgpc("PayerClaim") IsNot DBNull.Value _
                Then PC = Trim(drgpc("PayerClaim"))
            End While
        End If
        cngpc.Close()
        cngpc = Nothing
        Return PC
    End Function

    Private Sub DisplayAccession(ByVal AccID As Long)
        'Requisits(8)
        '0=PayerID, 1=AddID, 2=NPI, 3=Policy, 4=Group, 5=Dx, 6=CPT, 7=Pointer, 8=Price
        Dim OrdProviderID As Long = -1
        Dim PatientID As Long = -1
        Dim PayerID As Long = -1
        Dim cnn As New SqlConnection(connString)
        cnn.Open()
        Dim cmdsel As New SqlCommand("Select * from Requisitions where ID = " & AccID, cnn)
        cmdsel.CommandType = Data.CommandType.Text
        Dim DRsel As SqlDataReader = cmdsel.ExecuteReader
        If DRsel.HasRows Then
            While DRsel.Read
                txtAccessionID.Text = DRsel("ID")
                chkIsGratis.Checked = DRsel("IsGratis")
                If DRsel("PrimePayer_ID") IsNot DBNull.Value _
                Then PayerID = DRsel("PrimePayer_ID")
                If DRsel("OrderingProvider_ID") IsNot DBNull.Value _
                Then OrdProviderID = DRsel("OrderingProvider_ID")
                If DRsel("Patient_ID") IsNot DBNull.Value _
                Then PatientID = DRsel("Patient_ID")
            End While
        End If
        cnn.Close()
        cnn = Nothing
        '
        Dim IsBilled As Boolean = IsAccessionBilled(AccID)
        txtSvcDate.Text = Format(GetServiceDate(AccID), SystemConfig.DateFormat)
        chkIsBilled.Checked = IsBilled
        chkECC.Checked = GetInvoiceECC(AccID)
        If chkECC.Checked Then
            chkECC.Text = "837"
        ElseIf Not chkECC.Checked Then
            chkECC.Text = "Paper"
        Else
            chkECC.Checked = True
            chkECC.Text = "837"
        End If
        ' Dim tasks As List(Of Task) = New List(Of Task)()

        'tasks.Add(Task.Run(Sub() DisplayDxs(AccID, IsBilled)))
        'tasks.Add(Task.Run(Sub() DisplayThirdParty(AccID)))
        'tasks.Add(Task.Run(Sub() DisplaySecondIns(AccID)))
        ' tasks.Add(Task.Run(Sub() DisplayCharges(AccID)))
        'tasks.Add(Task.Run(Sub() DisplayPatient(PatientID)))

        ' Wait for all tasks to complete
        'Task.WaitAll(tasks.ToArray())


        DisplayDxs(AccID, IsBilled)
        DisplayThirdParty(AccID)
        DisplaySecondIns(AccID)
        If OrdProviderID <> -1 Then _
        DisplayClient(OrdProviderID)
        txtBox19.Text = GetPayerClaim(AccID)
        txtPreAuth.Text = GetPreAuth(AccID)
        If PatientID <> -1 Then _
        DisplayPatient(PatientID)
        DisplayCharges(AccID)
        Requisits = GetBillRequisits(PayerID)
        If chkIsBilled.Checked = False Then         'Unbilled
            chkBillDate.Enabled = True
            txtBillDate.ReadOnly = False
            txtDueDays.ReadOnly = False
            txtDueDate.ReadOnly = False
            txtTax.ReadOnly = False
            txtTerm.ReadOnly = False
            chkECC.Enabled = True

            txtBox19.Enabled = True
            txtPreAuth.Enabled = True
            txtStatus.Text = "Unbilled"
            txtStatus.BackColor = Color.Green
            txtStatus.ForeColor = Color.White
            cmbInvoices.Items.Clear()
            cmbInvoices.Text = ""
            chkIsGratis.Enabled = True
            'cmbBillType.Enabled = True
            EnablePatient()
            'DGVICD9s.ReadOnly = False
            gbTP.Enabled = True
            btnDxSync.Enabled = True
            dgvCharges.ReadOnly = False
            dgvCharges.Columns(3).ReadOnly = True : dgvCharges.Columns(12).ReadOnly = True
            dgvCharges.Columns(16).ReadOnly = True : dgvCharges.Columns(17).ReadOnly = True
            dgvCharges.Columns(1).ReadOnly = False : dgvCharges.Columns(4).ReadOnly = False
            dgvCharges.Columns(5).ReadOnly = False : dgvCharges.Columns(6).ReadOnly = False
            dgvCharges.Columns(7).ReadOnly = False : dgvCharges.Columns(8).ReadOnly = False
            dgvCharges.Columns(9).ReadOnly = False : dgvCharges.Columns(10).ReadOnly = False
            txtProviderID.ReadOnly = False
            btnProviderLook.Enabled = True
            lstAttending.Enabled = True
            btnDxSync_Click(Nothing, Nothing)
            ApplyEdits(Requisits)
        Else    'Billed
            'DisplayBCharges(RsA.Fields("ID").Value)
            'chkBillDate.Enabled = False
            'txtBillDate.ReadOnly = True
            'txtDueDays.ReadOnly = True
            'txtDueDate.ReadOnly = True
            'txtTax.ReadOnly = True
            'txtTerm.ReadOnly = True
            txtStatus.Text = "Billed"
            txtStatus.BackColor = Color.Red
            txtStatus.ForeColor = Color.White
            cmbInvoices.Items.Clear()
            Dim INVIDS() As String = GetInvoiceID(AccID)
            If INVIDS.Length > 0 Then
                Dim i As Integer
                For i = 0 To INVIDS.Length - 1
                    If INVIDS(i) <> "" Then
                        cmbInvoices.Items.Add(INVIDS(i))
                    End If
                Next
            End If
            If cmbInvoices.Items.Count > 0 Then cmbInvoices.SelectedIndex = 0
            chkECC.Enabled = False
            chkIsGratis.Enabled = False
            'txtBox19.Enabled = False
            'txtPreAuth.Enabled = False
            DisablePatient()
            gbTP.Enabled = False
            'DGVICD9s.ReadOnly = True
            'btnDxSync.Enabled = False
            'dgvCharges.ReadOnly = True
            txtProviderID.ReadOnly = True
            btnProviderLook.Enabled = False
            lstAttending.Enabled = False
            chkRev.Enabled = True
        End If
        'If IsBilled = False Then

        'End If

        'chkBillNow.Checked = False
        'chkRev.Checked = False
        Dim ChargeItems() As String = UpdateChargeItems()
        '0=T, 1=U, 2=B, 3=H, 4=P
        chkBillNow.Checked = False
        chkRev.Checked = False
        If chkIsBilled.Checked = False And Val(ChargeItems(0)) =
        Val(ChargeItems(1)) Then 'full bill
            chkBillNow.Enabled = True
            chkRev.Enabled = False
        ElseIf chkIsBilled.Checked = True And Val(ChargeItems(2)) _
        > 0 And Val(ChargeItems(1)) = 0 Then 'Reverse possible but no bill
            chkBillNow.Enabled = False
            chkRev.Enabled = True
        ElseIf Val(ChargeItems(0)) = Val(ChargeItems(4)) Then 'paid
            chkBillNow.Enabled = False
            chkRev.Enabled = False
        ElseIf Val(ChargeItems(3)) > 0 Or Val(ChargeItems(1)) > 0 Then 'possible bill
            chkBillNow.Enabled = True
            If chkIsBilled.Checked = True Then chkRev.Enabled = True
        End If
        InitializeChanges()
        DisplayComments(AccID)
    End Sub

    Private Function GetPreAuth(accID As Long) As String
        Dim PC As String = ""
        Dim cngpc As New SqlConnection(connString)
        cngpc.Open()
        Dim cmdgpc As New SqlCommand("Select PreAuthorization from " &
        "charges where   Accession_ID = " & accID, cngpc)
        cmdgpc.CommandType = CommandType.Text
        Dim drgpc As SqlDataReader = cmdgpc.ExecuteReader
        If drgpc.HasRows Then
            While drgpc.Read
                If drgpc("PreAuthorization") IsNot DBNull.Value _
                Then PC = Trim(drgpc("PreAuthorization"))
            End While
        End If
        cngpc.Close()
        cngpc = Nothing
        Return PC
    End Function

    Private Sub ApplyEdits(ByVal Requisits() As Long)
        '0=PayerID, 1=AddID, 2=NPI, 3=Policy, 
        '4=Group, 5=Dx, 6=CPT, 7=Pointer, 8=Price
        If Requisits(0) <> 0 Then
            Dim i As Integer
            Dim PayerID As Long = 0
            If cmbBillType.SelectedIndex = 0 Then   'Client
                PayerID = Requisits(0) : Requisits(1) = 0 : Requisits(2) = 0
                Requisits(3) = 0 : Requisits(4) = 0 : Requisits(5) = 0
                Requisits(6) = 0 : Requisits(7) = 0 : Requisits(8) = 0
            ElseIf cmbBillType.SelectedIndex = 1 Then   'Insurance
                Dim ItemX As MyList
                If cmbPayer.SelectedIndex <> -1 Then
                    ItemX = cmbPayer.SelectedItem
                    PayerID = ItemX.ItemData
                Else
                    PayerID = 0
                End If
            Else
                PayerID = Requisits(0) : Requisits(1) = 0 : Requisits(2) = 0
                Requisits(3) = 0 : Requisits(4) = 0 : Requisits(5) = 0
                Requisits(6) = 0 : Requisits(7) = 0 : Requisits(8) = 0
            End If
            If Requisits(0) = PayerID Then   'Proper payer
                '0=PayerID, 1=AddID, 2=NPI, 3=Policy, 
                '4=Group, 5=Dx, 6=CPT, 7=Pointer, 8=Price
                Dim B1 As Boolean = False : Dim B2 As Boolean = False
                Dim B3 As Boolean = False : Dim B4 As Boolean = False
                Dim B5 As Boolean = False
                If Requisits(1) = 1 Then    'address required
                    If Trim(txtPatAdd1.Text) <> "" And Trim(txtPatAdd1.Text) _
                    <> "" And Trim(txtPatAdd1.Text) <> "" And
                    Trim(txtPatAdd1.Text) <> "" Then B1 = True
                Else    'Not required
                    B1 = True
                End If
                '
                If Requisits(2) = 1 Then    'NPI required
                    If Trim(txtNPI.Text) <> "" Then B2 = True
                Else    'Not required
                    B2 = True
                End If
                '
                If Requisits(3) = 1 Then    'Policy required
                    If Trim(txtPolicy.Text) <> "" Then B3 = True
                Else    'Not required
                    B3 = True
                End If
                '
                If Requisits(4) = 1 Then    'Group required
                    If Trim(txtGroup.Text) <> "" Then B4 = True
                Else    'Not required
                    B4 = True
                End If
                '
                If Requisits(5) = 1 Then    'Dx required
                    Dim DxCount As Integer = 0
                    For i = 0 To DGVICD9s.RowCount - 1
                        If DGVICD9s.Rows(i).Cells(1).Value IsNot Nothing _
                        AndAlso DGVICD9s.Rows(i).Cells(1).Value <> "" Then
                            DxCount += 1
                            Exit For
                        End If
                    Next
                    If DxCount > 0 Then B5 = True
                Else    'Not required
                    B5 = True
                End If
                '
                '0=PayerID, 1=AddID, 2=NPI, 3=Policy, 
                '4=Group, 5=Dx, 6=CPT, 7=Pointer, 8=Price
                If B1 = False Or B2 = False Or B3 = False Or B4 = False Or B5 = False Then
                    FullBill = False
                    For i = 0 To dgvCharges.RowCount - 1
                        If dgvCharges.Rows(i).Cells(3).Value IsNot Nothing _
                        AndAlso dgvCharges.Rows(i).Cells(3).Value <> "" Then
                            If dgvCharges.Rows(i).Cells(14).Value = "B" Then
                                dgvCharges.Rows(i).Cells(15).ReadOnly = False
                            Else
                                dgvCharges.Rows(i).Cells(15).ReadOnly = True
                                dgvCharges.Rows(i).Cells(14).Value = "R"
                                dgvCharges.Rows(i).Cells(14).Style.BackColor = Color.Black
                                dgvCharges.Rows(i).Cells(14).Style.ForeColor = Color.White
                                dgvCharges.Rows(i).Cells(14).ReadOnly = True
                            End If
                        End If
                    Next
                Else
                    FullBill = True
                    Dim BillLine As Boolean = True
                    For i = 0 To dgvCharges.RowCount - 1
                        BillLine = True
                        If dgvCharges.Rows(i).Cells(3).Value IsNot Nothing _
                        AndAlso dgvCharges.Rows(i).Cells(3).Value <> "" Then
                            If dgvCharges.Rows(i).Cells(14).Value = "U" Then
                                If Requisits(6) = 1 Then    'CPT required
                                    If dgvCharges.Rows(i).Cells(4).Value Is Nothing Then
                                        BillLine = False
                                    ElseIf dgvCharges.Rows(i).Cells(4).Value = "" Then
                                        BillLine = False
                                    End If
                                Else
                                    BillLine = True
                                End If
                                '
                                If Requisits(7) = 1 Then    'Pointer required
                                    If dgvCharges.Rows(i).Cells(5).Value Is Nothing Then
                                        BillLine = False
                                    ElseIf Trim(dgvCharges.Rows(i).Cells(5).Value) = "" Then
                                        BillLine = False
                                    End If
                                Else
                                    BillLine = True
                                End If
                                '
                                If Requisits(8) = 1 Then    'non-zero price required
                                    If dgvCharges.Rows(i).Cells(12).Value Is Nothing Then
                                        BillLine = False
                                    ElseIf Val(dgvCharges.Rows(i).Cells(12).Value) = 0 Then
                                        BillLine = False
                                    End If
                                Else
                                    BillLine = True
                                End If
                                '
                                If BillLine Then
                                    dgvCharges.Rows(i).Cells(14).Value = "U"
                                    dgvCharges.Rows(i).Cells(14).Style.BackColor = Color.Green
                                    dgvCharges.Rows(i).Cells(14).Style.ForeColor = Color.White
                                    dgvCharges.Rows(i).Cells(14).ReadOnly = False
                                    dgvCharges.Rows(i).Cells(15).ReadOnly = False
                                Else
                                    dgvCharges.Rows(i).Cells(14).Value = "R"
                                    dgvCharges.Rows(i).Cells(14).Style.BackColor = Color.Black
                                    dgvCharges.Rows(i).Cells(14).Style.ForeColor = Color.White
                                    dgvCharges.Rows(i).Cells(14).ReadOnly = True
                                    dgvCharges.Rows(i).Cells(15).ReadOnly = True
                                End If
                            End If
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Private Function GetInvoiceID(ByVal AccID As Long) As String()
        Dim INVIDS() As String = {""}
        Dim cnii As New SqlConnection(connString)
        cnii.Open()
        Dim cmdii As New SqlCommand("Select * " &
        "from Charges where Accession_ID = " & AccID, cnii)
        cmdii.CommandType = CommandType.Text
        Dim drii As SqlDataReader = cmdii.ExecuteReader
        If drii.HasRows Then
            While drii.Read
                If INVIDS(UBound(INVIDS)) <> "" Then _
                ReDim Preserve INVIDS(UBound(INVIDS) + 1)
                INVIDS(UBound(INVIDS)) = drii("ID").ToString
            End While
        End If
        cnii.Close()
        cnii = Nothing
        Return INVIDS
    End Function

    Private Function UpdateChargeItems() As String()
        Dim ChargeItems() As String = {"", "", "", "", ""}  'Total, Unbilled, Billed, Held, Paid
        Dim CTT As Integer : Dim CTU As Integer : Dim CTB As Integer
        Dim CTH As Integer : Dim CTP As Integer : Dim i As Integer
        For i = 0 To dgvCharges.RowCount - 1

            If dgvCharges.Rows(i).Cells(1).Value IsNot Nothing AndAlso
            dgvCharges.Rows(i).Cells(1).Value.ToString <> "" Then
                CTT += 1
                If Not dgvCharges.Rows(i).Cells(14).Value Is Nothing Then
                    If dgvCharges.Rows(i).Cells(14).Value.ToString = "U" Then
                        CTU += 1
                    ElseIf dgvCharges.Rows(i).Cells(14).Value.ToString = "B" Then
                        CTB += 1
                    ElseIf dgvCharges.Rows(i).Cells(14).Value.ToString = "P" Then
                        CTP += 1
                    Else
                        CTH += 1
                    End If
                End If
            End If
        Next
        '0=T, 1=U, 2=N, 3=H, 4=P
        ChargeItems(0) = CTT.ToString : ChargeItems(1) = CTU.ToString
        ChargeItems(2) = CTB.ToString : ChargeItems(3) = CTH.ToString
        ChargeItems(4) = CTP.ToString
        Return ChargeItems
    End Function

    Private Sub DisplayComments(ByVal AccID As Long)
        dgvComments.Rows.Clear()
        Dim Dated As String = ""
        Dim cndc As New SqlConnection(connString)
        cndc.Open()
        Dim cmddc As New SqlCommand("Select * from " &
        "Req_Comments where Accession_ID = " & AccID, cndc)
        cmddc.CommandType = CommandType.Text
        Dim drdc As SqlDataReader = cmddc.ExecuteReader
        If drdc.HasRows Then
            While drdc.Read
                If drdc("Dated") IsNot DBNull.Value _
                AndAlso IsDate(drdc("Dated")) Then
                    Dated = Format(drdc("Dated"), SystemConfig.DateFormat & " HH:mm")
                Else
                    Dated = ""
                End If
                dgvComments.Rows.Add(Dated, Trim(drdc("Associated_To")),
                Trim(drdc("Comment")), GetUserName(drdc("User_ID")))
                If drdc("User_ID") = ThisUser.ID Then
                    dgvComments.Rows(dgvComments.RowCount - 1).Cells(2).ReadOnly = False
                    dgvComments.Rows(dgvComments.RowCount - 1).Cells(2).Style.BackColor = Color.White
                Else
                    dgvComments.Rows(dgvComments.RowCount - 1).Cells(2).ReadOnly = True
                End If
            End While
            dgvComments.Rows.Add()
        Else
            dgvComments.Rows.Add()
        End If
        cndc.Close()
        cndc = Nothing
    End Sub

    Private Sub DisablePatient()
        txtPatientID.ReadOnly = True
        btnPatLook.Enabled = False
        txtPatLName.ReadOnly = True
        txtPatFName.ReadOnly = True
        txtPatMName.ReadOnly = True
        txtPatDOB.ReadOnly = True
        txtPatSex.ReadOnly = True
        txtPatAdd1.ReadOnly = True
        txtPatAdd2.ReadOnly = True
        txtPatCity.ReadOnly = True
        txtPatState.ReadOnly = True
        txtPatZip.ReadOnly = True
        txtPatEmail.ReadOnly = True
        txtPatHPhone.ReadOnly = True
        txtPatCell.ReadOnly = True
        txtPatSSN.ReadOnly = True
    End Sub

    Private Sub EnablePatient()
        txtPatientID.ReadOnly = False
        btnPatLook.Enabled = True
        txtPatLName.ReadOnly = False
        txtPatFName.ReadOnly = False
        txtPatMName.ReadOnly = False
        txtPatDOB.ReadOnly = False
        txtPatSex.ReadOnly = False
        txtPatAdd1.ReadOnly = False
        txtPatAdd2.ReadOnly = False
        txtPatCity.ReadOnly = False
        txtPatState.ReadOnly = False
        txtPatZip.ReadOnly = False
        txtPatEmail.ReadOnly = False
        txtPatHPhone.ReadOnly = False
        txtPatCell.ReadOnly = False
        txtPatSSN.ReadOnly = False
    End Sub

    Private Sub InitializeChanges()
        Ch_Bill = False
        Ch_Reverse = False
        Ch_Acc = False
        Ch_TP = False
        Ch_Lines = False
        'BillingEditProgress()
    End Sub

    Private Function IsAccessionBilled(ByVal AccID As Long) As Boolean
        Dim billed As Boolean = False
        Dim cniab As New SqlConnection(connString)
        cniab.Open()
        Dim cmdiab As New SqlCommand("Select * " &
        "from Charges where Accession_ID = " & AccID, cniab)
        cmdiab.CommandType = Data.CommandType.Text
        Dim driab As SqlDataReader = cmdiab.ExecuteReader
        If driab.HasRows Then billed = True
        cniab.Close()
        cniab = Nothing
        Return billed
    End Function

    Private Sub DisplayCharges(ByVal AccID As Long)
        'SynchronizeBillables(AccID)
        Dim BillInfo() As String
        Dim CStatus As String = ""
        Dim TGP() As String = {"", "", "", "", "", "", "", "", "", ""}
        '0=TestName, 1=CPT, 2=ICD9, 3=M1, 4=M2, 5=M3, 6=M4, 7=POS, 8=DateFiller, 9=Biller 
        dgvCharges.Rows.Clear()
        Dim cndc As New SqlConnection(connString)
        cndc.Open()
        Dim cmddc As New SqlCommand("Select * from Req_Billable where Accession_ID = " & AccID & " Order by Ordinal", cndc)
        cmddc.CommandType = Data.CommandType.Text
        Dim drdc As SqlDataReader = cmddc.ExecuteReader
        If drdc.HasRows Then
            While drdc.Read
                TGP(0) = GetTGPName(drdc("TGP_ID"))
                If drdc("CPT_Code") IsNot DBNull.Value Then TGP(1) = Trim(drdc("CPT_Code"))
                If drdc("ICD9") IsNot DBNull.Value Then TGP(2) = Trim(drdc("ICD9"))
                If drdc("Mod1") IsNot DBNull.Value Then TGP(3) = Trim(drdc("Mod1"))
                If drdc("Mod2") IsNot DBNull.Value Then TGP(4) = Trim(drdc("Mod2"))
                If drdc("Mod3") IsNot DBNull.Value Then TGP(5) = Trim(drdc("Mod3"))
                If drdc("Mod4") IsNot DBNull.Value Then TGP(6) = Trim(drdc("Mod4"))
                If drdc("POS_Code") IsNot DBNull.Value Then TGP(7) = Trim(drdc("POS_Code"))
                If drdc("Bill_Status") IsNot DBNull.Value Then
                    CStatus = drdc("Bill_Status")
                Else
                    CStatus = "R"
                End If
                If CStatus = "U" Or CStatus = "H" Or CStatus = "R" Then
                    TGP(8) = "__/__/____"
                    TGP(9) = ""
                Else
                    BillInfo = GetBillInfo(AccID, drdc("TGP_ID"))
                    CStatus = BillInfo(2)
                    CStatus = UpdatePaymentStatus(AccID, drdc("TGP_ID"), CStatus)
                    If BillInfo(0) <> "" Then
                        TGP(8) = BillInfo(0)
                    Else
                        TGP(8) = "__/__/____"
                    End If
                    If BillInfo(1) <> "" Then
                        TGP(9) = GetUserName(BillInfo(1))
                    Else
                        TGP(9) = ""
                    End If
                End If
                If drdc("LinePrice") IsNot DBNull.Value And drdc("Unit") IsNot
                DBNull.Value And drdc("Extend") IsNot DBNull.Value Then
                    dgvCharges.Rows.Add(System.Drawing.Image.FromFile(
                    My.Application.Info.DirectoryPath & "\Images\Eraser.ico"),
                    drdc("TGP_ID"), System.Drawing.Image.FromFile(
                    My.Application.Info.DirectoryPath & "\Images\Looks.ico"),
                    TGP(0), TGP(1), TGP(2), TGP(3), TGP(4), TGP(5), TGP(6),
                    Format(drdc("LinePrice"), "0.00"), drdc("Unit"), Format(drdc("Extend"),
                    "0.00"), TGP(7), CStatus, "0", TGP(8), TGP(9))
                Else
                    dgvCharges.Rows.Add(System.Drawing.Image.FromFile(
                    My.Application.Info.DirectoryPath & "\Images\Eraser.ico"),
                    drdc("TGP_ID"), System.Drawing.Image.FromFile(
                    My.Application.Info.DirectoryPath & "\Images\Looks.ico"),
                    TGP(0), TGP(1), TGP(2), TGP(3), TGP(4), TGP(5), TGP(6),
                    "0.00", "", "0.00", TGP(7), CStatus, "0", TGP(8), TGP(9))
                End If
                dgvCharges.Rows(dgvCharges.RowCount - 1).Cells(15).ReadOnly = True
                If dgvCharges.Rows(dgvCharges.RowCount - 1).Cells(14).Value.ToString = "U" Then
                    dgvCharges.Rows(dgvCharges.RowCount - 1).Cells(14).Style.BackColor = Color.Green
                    dgvCharges.Rows(dgvCharges.RowCount - 1).Cells(14).Style.ForeColor = Color.White
                    EnableChargeLine(dgvCharges.RowCount - 1)
                ElseIf dgvCharges.Rows(dgvCharges.RowCount - 1).Cells(14).Value.ToString = "P" Then
                    dgvCharges.Rows(dgvCharges.RowCount - 1).Cells(14).Style.BackColor = Color.BlueViolet
                    dgvCharges.Rows(dgvCharges.RowCount - 1).Cells(14).Style.ForeColor = Color.White
                    DisableChargeLine(dgvCharges.RowCount - 1)
                ElseIf dgvCharges.Rows(dgvCharges.RowCount - 1).Cells(14).Value.ToString = "B" Then
                    dgvCharges.Rows(dgvCharges.RowCount - 1).Cells(14).Style.BackColor = Color.Red
                    dgvCharges.Rows(dgvCharges.RowCount - 1).Cells(14).Style.ForeColor = Color.White
                    DisableChargeLine(dgvCharges.RowCount - 1)
                Else
                    dgvCharges.Rows(dgvCharges.RowCount - 1).Cells(14).Style.BackColor = Color.Yellow
                    dgvCharges.Rows(dgvCharges.RowCount - 1).Cells(14).Style.ForeColor = Color.Black
                    EnableChargeLine(dgvCharges.RowCount - 1)
                End If
                dgvCharges.Rows(dgvCharges.RowCount - 1).Cells(14).ReadOnly = True
            End While
        End If
        cndc.Close()
        cndc = Nothing
        '
        dgvCharges.RowCount += 1
        CalculateBTotal()
        CalculateUTotal()
    End Sub

    Private Function UpdatePaymentStatus(ByVal AccID As Long, ByVal TGPID As Integer, ByVal CStatus As String) As String
        Dim sSQL As String = "Select * from Payment_Detail where TGP_ID = " & TGPID &
        " and Charge_ID in (Select ID from Charges where Accession_ID = " & AccID & ")"
        Dim cnups As New SqlConnection(connString)
        cnups.Open()
        Dim cmdups As New SqlCommand(sSQL, cnups)
        cmdups.CommandType = Data.CommandType.Text
        Dim drups As SqlDataReader = cmdups.ExecuteReader
        If drups.HasRows Then CStatus = "P"
        cnups.Close()
        cnups = Nothing
        Return CStatus
    End Function

    Private Function GetBillInfo(ByVal AccID As Long, ByVal TGPID As Integer) As String()
        Dim BillInfo() As String = {"", "", "U"}
        Dim sSQL As String = "Select a.Billed_On as BillDate, a.Billed_By as " &
        "BillerID from Charge_Detail a inner join Charges b on a.Charge_ID = " &
        "b.ID where b.Accession_ID = " & AccID & " and a.TGP_ID = " & TGPID
        Dim cngbi As New SqlConnection(connString)
        cngbi.Open()
        Dim cmdgbi As New SqlCommand(sSQL, cngbi)
        cmdgbi.CommandType = Data.CommandType.Text
        Dim drgbi As SqlDataReader = cmdgbi.ExecuteReader
        If drgbi.HasRows Then
            While drgbi.Read
                BillInfo(0) = Format(drgbi("BillDate"), SystemConfig.DateFormat)
                BillInfo(1) = drgbi("BillerID").ToString
                BillInfo(2) = "B"
            End While
        End If
        cngbi.Close()
        cngbi = Nothing
        Return BillInfo
    End Function

    Private Sub DisableChargeLine(ByVal Line As Integer)
        dgvCharges.Rows(Line).Cells(0).ReadOnly = True
        dgvCharges.Rows(Line).Cells(1).ReadOnly = True
        dgvCharges.Rows(Line).Cells(2).ReadOnly = True
        dgvCharges.Rows(Line).Cells(3).ReadOnly = True
        dgvCharges.Rows(Line).Cells(4).ReadOnly = True
        dgvCharges.Rows(Line).Cells(5).ReadOnly = True
        dgvCharges.Rows(Line).Cells(6).ReadOnly = True
        dgvCharges.Rows(Line).Cells(7).ReadOnly = True
        dgvCharges.Rows(Line).Cells(8).ReadOnly = True
        dgvCharges.Rows(Line).Cells(9).ReadOnly = True
        dgvCharges.Rows(Line).Cells(10).ReadOnly = True
        dgvCharges.Rows(Line).Cells(11).ReadOnly = True
        dgvCharges.Rows(Line).Cells(12).ReadOnly = True
        dgvCharges.Rows(Line).Cells(13).ReadOnly = True
        dgvCharges.Rows(Line).Cells(14).ReadOnly = True
        If dgvCharges.Rows(Line).Cells(14).Value = "P" Then
            dgvCharges.Rows(Line).Cells(15).ReadOnly = True
        Else
            dgvCharges.Rows(Line).Cells(15).ReadOnly = False
        End If
        dgvCharges.Rows(Line).Cells(16).ReadOnly = True
        dgvCharges.Rows(Line).Cells(17).ReadOnly = True
        chkECC.Enabled = False
    End Sub
    Private Sub EnableChargeLine(ByVal Line As Integer)
        dgvCharges.Rows(Line).Cells(0).ReadOnly = False
        dgvCharges.Rows(Line).Cells(1).ReadOnly = False
        dgvCharges.Rows(Line).Cells(2).ReadOnly = False
        dgvCharges.Rows(Line).Cells(3).ReadOnly = True
        dgvCharges.Rows(Line).Cells(4).ReadOnly = False
        dgvCharges.Rows(Line).Cells(5).ReadOnly = False
        dgvCharges.Rows(Line).Cells(6).ReadOnly = False
        dgvCharges.Rows(Line).Cells(7).ReadOnly = False
        dgvCharges.Rows(Line).Cells(8).ReadOnly = False
        dgvCharges.Rows(Line).Cells(9).ReadOnly = False
        dgvCharges.Rows(Line).Cells(10).ReadOnly = False
        dgvCharges.Rows(Line).Cells(11).ReadOnly = False
        dgvCharges.Rows(Line).Cells(12).ReadOnly = True
        dgvCharges.Rows(Line).Cells(13).ReadOnly = False
        dgvCharges.Rows(Line).Cells(14).ReadOnly = False
        dgvCharges.Rows(Line).Cells(15).ReadOnly = False
        dgvCharges.Rows(Line).Cells(16).ReadOnly = True
        dgvCharges.Rows(Line).Cells(17).ReadOnly = True
        chkECC.Enabled = True
    End Sub

    Private Function TGPInTheList(ByVal TGPID As Integer) As Boolean
        Dim i As Integer
        Dim InList As Boolean = False
        For i = 0 To dgvCharges.RowCount - 1
            If Not dgvCharges.Rows(i).Cells(1).Value Is System.DBNull.Value _
            AndAlso dgvCharges.Rows(i).Cells(1).Value = TGPID Then
                InList = True
                Exit For
            End If
        Next
        Return InList
    End Function

    Private Sub CalculateBTotal()
        Dim i As Integer
        Dim Total As Single = 0
        For i = 0 To dgvCharges.RowCount - 1
            If Not dgvCharges.Rows(i).Cells(1).Value Is Nothing AndAlso
            dgvCharges.Rows(i).Cells(1).Value.ToString <> "" And
            (dgvCharges.Rows(i).Cells(14).Value = "B" Or
            dgvCharges.Rows(i).Cells(14).Value = "P") Then _
            Total += Val(dgvCharges.Rows(i).Cells(12).Value)
        Next
        txtBCharges.Text = Total.ToString("0.00")
    End Sub

    Private Sub CalculateUTotal()
        Dim i As Integer
        Dim Total As Single = 0
        For i = 0 To dgvCharges.RowCount - 1
            If Not dgvCharges.Rows(i).Cells(1).Value Is Nothing AndAlso
            dgvCharges.Rows(i).Cells(1).Value.ToString <> "" And
            (dgvCharges.Rows(i).Cells(14).Value = "U" Or
            dgvCharges.Rows(i).Cells(14).Value = "H" Or
            dgvCharges.Rows(i).Cells(14).Value = "R") Then _
            Total += Val(dgvCharges.Rows(i).Cells(12).Value)
        Next
        txtUCharges.Text = Total.ToString("0.00")
    End Sub

    Private Sub SyncronizeDxs()
        'On Error Resume Next
        Dim i As Integer
        Dim Ptr As String = ""
        Dim Codes As String = ""
        For i = 0 To DGVICD9s.RowCount - 1
            If Not DGVICD9s.Rows(i).Cells(1).Value Is Nothing AndAlso
            Trim(DGVICD9s.Rows(i).Cells(1).Value.ToString) <> "" Then   'ICD9 present
                If InStr(Codes, Trim(DGVICD9s.Rows(i).Cells(1).Value.ToString)) = 0 Then _
                Codes += Trim(DGVICD9s.Rows(i).Cells(1).Value.ToString) & "|"
            End If
        Next
        If Codes.EndsWith("|") Then Codes = Microsoft.VisualBasic.Mid(Codes, 1, Len(Codes) - 1)
        If Codes <> "" Then
            For i = 0 To dgvCharges.RowCount - 1
                If dgvCharges.Rows(i).Cells(1).Value IsNot Nothing AndAlso
                dgvCharges.Rows(i).Cells(1).Value.ToString <> "" Then
                    If dgvCharges.Rows(i).Cells(5).Value Is Nothing OrElse
                    Trim(dgvCharges.Rows(i).Cells(5).Value.ToString) = "" Then
                        dgvCharges.Rows(i).Cells(5).Value = GetNecessityPtrs(dgvCharges.Rows(i).Cells(4).Value, Codes)
                        If dgvCharges.Rows(i).Cells(5).Value <> "" Then
                            dgvCharges.Rows(i).Cells(14).Value = "U"
                            dgvCharges.Rows(i).Cells(14).Style.BackColor = Color.Green
                            dgvCharges.Rows(i).Cells(14).Style.ForeColor = Color.White
                            chkRev.Enabled = False
                        End If
                    End If
                End If
            Next
            'If cmbPayer.SelectedIndex <> -1 Then
            '    Dim ItemX As MyList = cmbPayer.SelectedItem
            If Ch_Lines Then ApplyEdits(Requisits)
            'End If
        Else
            For i = 0 To dgvCharges.RowCount - 1
                dgvCharges.Rows(i).Cells(5).Value = ""
            Next
        End If
    End Sub

    Private Function IsPointerValid(ByVal ICD9S As String, ByVal Ptr As String) As Boolean
        Dim PtrValid As Boolean = True
        Dim POINTERS() As String = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"}
        Ptr = Replace(Ptr, ".", "")
        If ICD9S = "" Then
            PtrValid = False
        Else
            Dim Codes() As String = Split(ICD9S, "|")
            Dim StrPtr As String = Microsoft.VisualBasic.Mid("112345678910", 1, Codes.Length)
            Dim P() As String = Split(Ptr, ",")
            Dim n As Integer
            If P.Length > Codes.Length Then
                PtrValid = False
            Else
                For n = 0 To P.Length - 1
                    If Trim(P(n)) <> "" Then
                        If Val(P(n)) > Codes.Length Then   'bad
                            PtrValid = False
                            Exit For
                        ElseIf InStr(Ptr, ".") > 0 Then
                            PtrValid = False
                            Exit For
                        ElseIf InStr(Ptr, ",,") > 0 Then
                            PtrValid = False
                            Exit For
                        ElseIf Val(P(n)) < 1 Then
                            PtrValid = False
                            Exit For
                        End If
                    Else
                        PtrValid = False
                        Exit For
                    End If
                Next
            End If
        End If
        Return PtrValid
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
        If Ptr.EndsWith(",") Then Ptr = Ptr.Substring(0, Len(Ptr) - 1)
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

    Private Function IsTGPOpenMileage(ByVal CPT As String) As Boolean
        Dim OPM As Boolean = False
        If CPT = "P9603" Then OPM = True
        'Dim cnb As New SqlConnection(connString)
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

    Private Function GetTGPData(ByVal TGPID As Integer, ByVal TGPType As String) As String()
        Dim ConPrice As Single
        Dim TGP() As String = {"", "", "", "", "", "", "", "", "", ""}
        '0=ID, 1=Name, 2=CPT, 3="", 4=Price, 5=M1, 6=M2, 7=M3, 8=M4, 9=POS
        Dim cnb As New SqlConnection(connString)
        cnb.Open()
        Dim cmdb As New SqlCommand("Select ID, Name, CPT_Code, Mod1, Mod2, " &
        "Mod3, Mod4, POS_Code from Tests where ID = " & TGPID & " Union Select ID, " &
        "Name, CPT_Code, Mod1, Mod2, Mod3, Mod4, POS_Code from Groups where ID = " &
        TGPID & " Union Select ID, Name, CPT_Code, Mod1, Mod2, Mod3, Mod4, POS_Code " &
        "from Profiles where ID = " & TGPID, cnb)
        cmdb.CommandType = CommandType.Text
        Dim drb As SqlDataReader = cmdb.ExecuteReader
        If drb.HasRows Then
            While drb.Read
                TGP(0) = drb("ID").ToString
                TGP(1) = drb("Name")
                If drb("CPT_Code") IsNot DBNull.Value _
                AndAlso Trim(drb("CPT_Code")) <> "" Then _
                    TGP(2) = Trim(drb("CPT_Code"))
                TGP(3) = ""
                If cmbBillType.SelectedIndex = 0 Then   'Client
                    ConPrice = GetContractPrice(0, TGPID)
                    If ConPrice <= 0 Then
                        TGP(4) = GetLocalPrice(TGPID, cmbProvPrice.SelectedIndex)
                    Else
                        TGP(4) = ConPrice.ToString
                    End If
                ElseIf cmbBillType.SelectedIndex = 1 Then   'TP
                    ConPrice = GetContractPrice(1, TGPID)
                    If ConPrice <= 0 Then
                        TGP(4) = GetLocalPrice(TGPID, cmbTPPrice.SelectedIndex)
                    Else
                        TGP(4) = ConPrice.ToString
                    End If
                Else   'Patient
                    TGP(4) = GetLocalPrice(TGPID, cmbPatPrice.SelectedIndex)
                End If
                If drb("Mod1") IsNot DBNull.Value Then TGP(5) = Trim(drb("Mod1"))
                If drb("Mod2") IsNot DBNull.Value Then TGP(6) = Trim(drb("Mod2"))
                If drb("Mod3") IsNot DBNull.Value Then TGP(7) = Trim(drb("Mod3"))
                If drb("Mod4") IsNot DBNull.Value Then TGP(8) = Trim(drb("Mod4"))
                If drb("POS_Code") IsNot DBNull.Value Then TGP(9) = Trim(drb("POS_Code"))
            End While
        End If
        cnb.Close()
        cnb = Nothing
        Return TGP
    End Function

    Private Function GetLocalPrice(ByVal TGPID As Integer, ByVal Level As Integer) As Single
        Dim Price As Single
        Dim sSQL As String = "Select ListPrice, Price1, Price2, Price3, Price4, " &
        "Price5, Price6, Price7, Price8, Price8 from Tests where ID = " & TGPID &
        " Union Select ListPrice, Price1, Price2, Price3, Price4, Price5, Price6, " &
        "Price7, Price8, Price8 from Groups where ID = " & TGPID & " Union Select " &
        "ListPrice, Price1, Price2, Price3, Price4, Price5, Price6, Price7, " &
        "Price8, Price8 from Profiles where ID = " & TGPID
        Dim cncp As New SqlConnection(connString)
        cncp.Open()
        Dim cmdcp As New SqlCommand(sSQL, cncp)
        cmdcp.CommandType = CommandType.Text
        Dim drcp As SqlDataReader = cmdcp.ExecuteReader
        If drcp.HasRows Then
            While drcp.Read
                If Level = 1 Then   'Level 1
                    Price = drcp("Price1")
                ElseIf Level = 2 Then   'Level 2
                    Price = drcp("Price2")
                ElseIf Level = 3 Then   'Level 3
                    Price = drcp("Price3")
                ElseIf Level = 4 Then   'Level 4
                    Price = drcp("Price4")
                ElseIf Level = 5 Then   'Level 5
                    Price = drcp("Price5")
                ElseIf Level = 6 Then   'Level 6
                    Price = drcp("Price6")
                ElseIf Level = 7 Then   'Level 7
                    Price = drcp("Price7")
                ElseIf Level = 8 Then   'Level 8
                    Price = drcp("Price8")
                ElseIf Level = 9 Then   'Level 9
                    Price = drcp("Price9")
                Else    'List default
                    Price = drcp("ListPrice")
                End If
            End While
        End If
        cncp.Close()
        cncp = Nothing
        Return Price
    End Function

    Private Function GetContractPrice(ByVal BillType As Byte, ByVal TGPID As Integer) As Single
        Dim Price As Single = -1
        Dim PayerID As Long = -1
        If cmbPayer.SelectedIndex <> -1 Then
            Dim ItemX As MyList = cmbPayer.SelectedItem
            PayerID = ItemX.ItemData
        End If
        Dim sSQL As String = ""
        If BillType = 0 Then    'Client
            sSQL = "Select * from Provider_TGP where Provider_ID = " &
            Val(txtProviderID.Text) & " and TGP_ID = " & TGPID
        ElseIf BillType = 1 Then     'TP
            sSQL = "Select * from Payer_TGP where Payer_ID = " &
            PayerID & " and TGP_ID = " & TGPID
        End If
        Dim cncp As New SqlConnection(connString)
        cncp.Open()
        Dim cmdcp As New SqlCommand(sSQL, cncp)
        cmdcp.CommandType = CommandType.Text
        Dim drcp As SqlDataReader = cmdcp.ExecuteReader
        If drcp.HasRows Then
            While drcp.Read
                Price = drcp("Price")
            End While
        End If
        cncp.Close()
        cncp = Nothing
        Return Price
    End Function

    Private Sub DisplayThirdParty(ByVal AccID As Long)
        Dim i As Integer
        Dim ItemX As MyList
        Dim cntp As New SqlConnection(connString)
        cntp.Open()
        Dim cmdtp As New SqlCommand("Select a.*, b.LastName, " &
        "b.FirstName, b.Sex, b.DOB, b.Address_ID from Req_Coverage a " &
        "inner join Patients b on b.ID = a.Insured_ID where a.Preference " &
        "= 'P' and a.Accession_ID = " & AccID, cntp)
        cmdtp.CommandType = CommandType.Text
        Dim drtp As SqlDataReader = cmdtp.ExecuteReader
        If drtp.HasRows Then
            While drtp.Read
                cmbTPPrice.SelectedIndex = GetTPPriceLevel(drtp("Payer_ID"))
                For i = 0 To cmbPayer.Items.Count - 1
                    ItemX = cmbPayer.Items(i)
                    If drtp("Payer_ID") = ItemX.ItemData Then
                        cmbPayer.SelectedIndex = i
                        Exit For
                    End If
                Next
                txtPolicy.Text = drtp("PolicyNo")
                txtGroup.Text = IIf(IsDBNull(drtp("GroupNo")), "", drtp("GroupNo"))

                If drtp("CoPayment") IsNot DBNull.Value Then txtCopay.Text = drtp("CoPayment")
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

    Private Sub DisplaySecondIns(ByVal AccID As Long)
        ClearSecondary()
        Dim sSQL As String = "Select a.*, b.PayerName from Req_Coverage a inner join Payers b on a.Payer_ID = b.ID " &
        "where a.Accession_ID = " & AccID & " and   a.Preference = 'S'"
        If connString <> "" Then
            Dim cnsp As New SqlConnection(connString)
            cnsp.Open()
            Dim ItemX As MyList
            Dim cmdsp As New SqlCommand(sSQL, cnsp)
            cmdsp.CommandType = CommandType.Text
            Dim drdsc As SqlDataReader = cmdsp.ExecuteReader
            If drdsc.HasRows Then
                While drdsc.Read
                    For i As Integer = 0 To cmbSIns.Items.Count - 1
                        ItemX = cmbSIns.Items(i)
                        If ItemX.ItemData = drdsc("Payer_ID") Then
                            cmbSIns.SelectedIndex = i
                            Exit For
                        End If
                    Next
                    If drdsc("GroupNo") IsNot DBNull.Value Then txtSGroup.Text = drdsc("GroupNo")
                    If drdsc("PolicyNo") IsNot DBNull.Value Then txtSPolicy.Text = drdsc("PolicyNo")

                    'If drdsc("StartDate") IsNot DBNull.Value Then txtSFrom.Text = Format(drdsc("StartDate"), SystemConfig.DateFormat)
                    'If drdsc("ExpireDate") IsNot DBNull.Value Then txtSTo.Text = Format(drdsc("ExpireDate"), SystemConfig.DateFormat)
                    cmbSRelation.SelectedIndex = drdsc("Relation")
                    If cmbSRelation.SelectedIndex > 0 Then
                        DisplaySSub(drdsc("Insured_ID"))
                        grpSSubs.Enabled = True
                    Else
                        grpSSubs.Enabled = False

                    End If
                    Try
                        If drdsc("useforbill") IsNot DBNull.Value Then
                            If Convert.ToBoolean(drdsc("useforbill").ToString()) Then
                                conractS.Checked = True
                            Else
                                conractS.Checked = False
                            End If
                        End If

                    Catch ex As Exception

                    End Try
                    If drdsc("Copayment") IsNot DBNull.Value Then txtSCopay.Text = Format(drdsc("Copayment"), "##,##0.00")
                End While
            End If
            cnsp.Close()
            cnsp = Nothing


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

        txtSSubEmail.Text = ""
        txtSSubEmployer.Text = ""
        txtSSubAdd1.Text = ""
        txtSSubAdd2.Text = ""
        txtSSubCity.Text = ""
        txtSSubState.Text = ""
        txtSSubZip.Text = ""
        txtSSubCountry.Text = ""
    End Sub
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
    Private Function GetTPPriceLevel(ByVal PayerID As Long) As Integer
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
                    Dim add = GetAddressInfo(drdp("Address_ID"))
                    txtPatAdd1.Text = add.Address1 ' GetAddress1(drdp("Address_ID"))
                    txtPatAdd2.Text = add.Address2 ' GetAddress2(drdp("Address_ID"))
                    txtPatCity.Text = add.City ' GetAddressCity(drdp("Address_ID"))
                    txtPatState.Text = add.State ' GetAddressState(drdp("Address_ID"))
                    txtPatZip.Text = add.Zip ' GetAddressZip(drdp("Address_ID"))
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

    Private Sub ClearThirdPart()
        cmbTPPrice.SelectedIndex = 0
        cmbPayer.SelectedIndex = -1
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
                If drdc("IsIndividual") = 0 Then
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
                Dim attid = GetAttendingProviderID(Val(txtAccessionID.Text))
                If drda.HasRows Then
                    While drda.Read
                        If drda("IsIndividual") = 0 Then
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
                        If drda("ID") = attid Then
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
        Try
            SortCheckedItemsToTop()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub SortCheckedItemsToTop()
        ' Create lists for checked and unchecked items
        Dim checkedItems As New List(Of Object)()
        Dim uncheckedItems As New List(Of Object)()

        ' Separate checked and unchecked items
        For Each item In lstAttending.Items
            If lstAttending.GetItemChecked(lstAttending.Items.IndexOf(item)) Then
                checkedItems.Add(item)
            Else
                uncheckedItems.Add(item)
            End If
        Next

        ' Clear the CheckedListBox
        lstAttending.Items.Clear()

        ' Add checked items first
        For Each item In checkedItems
            lstAttending.Items.Add(item, True)
        Next

        ' Add unchecked items afterward
        For Each item In uncheckedItems
            lstAttending.Items.Add(item, False)
        Next
    End Sub

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

    Private Sub DisplayDxs(ByVal AccID As Long, ByVal IsBilled As Boolean)
        Dim i As Integer = 0
        For i = 0 To DGVICD9s.RowCount - 1
            DGVICD9s.Rows(i).Cells(1).Value = ""
            DGVICD9s.Rows(i).Cells(1).ReadOnly = False
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
                If i = DGVICD9s.RowCount - 1 Then
                    DGVICD9s.RowCount += 1
                    DGVICD9s.Rows(DGVICD9s.RowCount - 1).Cells(1).ReadOnly = False
                    DGVICD9s.Rows(DGVICD9s.RowCount - 1).Cells(0).Value = DGVICD9s.RowCount.ToString
                End If
                If Trim(drdx("Dx_Code")) <> "" Then
                    DGVICD9s.Rows(i).Cells(1).Value = Trim(drdx("Dx_Code"))
                    If IsBilled Then
                        DGVICD9s.Rows(i).Cells(1).ReadOnly = True
                    End If
                    i += 1
                End If
            End While
        End If
        cndx.Close()
        cndx = Nothing
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

    Private Sub btnDxSync_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDxSync.Click
        SyncronizeDxs()
        'Ch_Lines = True
        BillingEditProgress()
    End Sub

    Private Sub ClearChargeLine(ByVal LineNo As Integer)
        dgvCharges.Rows(LineNo).Cells(0).Value = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Blank.ico")
        dgvCharges.Rows(LineNo).Cells(1).Value = ""
        dgvCharges.Rows(LineNo).Cells(3).Value = ""
        dgvCharges.Rows(LineNo).Cells(4).Value = ""
        dgvCharges.Rows(LineNo).Cells(5).Value = ""
        dgvCharges.Rows(LineNo).Cells(6).Value = ""
        dgvCharges.Rows(LineNo).Cells(7).Value = ""
        dgvCharges.Rows(LineNo).Cells(8).Value = ""
        dgvCharges.Rows(LineNo).Cells(9).Value = ""
        dgvCharges.Rows(LineNo).Cells(10).Value = ""
        dgvCharges.Rows(LineNo).Cells(11).Value = ""
        dgvCharges.Rows(LineNo).Cells(12).Value = ""
        dgvCharges.Rows(LineNo).Cells(13).Value = ""
        dgvCharges.Rows(LineNo).Cells(14).Value = ""
        dgvCharges.Rows(LineNo).Cells(14).Style.BackColor = dgvCharges.Rows(LineNo).Cells(13).Style.BackColor
        dgvCharges.Rows(LineNo).Cells(14).Style.ForeColor = Color.Black
        dgvCharges.Rows(LineNo).Cells(15).Value = False
        dgvCharges.Rows(LineNo).Cells(16).Value = "__/__/____"
        dgvCharges.Rows(LineNo).Cells(17).Value = ""
    End Sub

    Private Sub PopulateChargeLine(ByVal LineNo As Integer, ByVal TGPData() As String)
        dgvCharges.Rows(LineNo).Cells(0).Value = System.Drawing.Image.FromFile(Application.StartupPath _
        & "\Images\Eraser.ico")
        '0=ID, 1=Name, 2=CPT, 3="", 4=Price, 5=M1, 6=M2, 7=M3, 8=M4, 9=POS
        dgvCharges.Rows(LineNo).Cells(1).Value = TGPData(0)
        dgvCharges.Rows(LineNo).Cells(3).Value = TGPData(1)
        dgvCharges.Rows(LineNo).Cells(4).Value = TGPData(2)
        dgvCharges.Rows(LineNo).Cells(5).Value = TGPData(3)
        dgvCharges.Rows(LineNo).Cells(6).Value = ""
        dgvCharges.Rows(LineNo).Cells(7).Value = ""
        dgvCharges.Rows(LineNo).Cells(8).Value = ""
        dgvCharges.Rows(LineNo).Cells(9).Value = ""
        dgvCharges.Rows(LineNo).Cells(10).Value = Val(TGPData(4)).ToString("0.00")
        dgvCharges.Rows(LineNo).Cells(11).Value = "1.0"
        dgvCharges.Rows(LineNo).Cells(12).Value = Val(TGPData(4)).ToString("0.00")
        dgvCharges.Rows(LineNo).Cells(13).Value = TGPData(9)
        dgvCharges.Rows(LineNo).Cells(14).Value = "U"
        dgvCharges.Rows(LineNo).Cells(14).Style.BackColor = Color.LightGreen
        dgvCharges.Rows(LineNo).Cells(14).Style.ForeColor = Color.Black
        dgvCharges.Rows(LineNo).Cells(15).Value = False
        dgvCharges.Rows(LineNo).Cells(16).Value = "__/__/____"
        dgvCharges.Rows(LineNo).Cells(17).Value = ""
    End Sub

    Private Sub dgvCharges_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCharges.CellEndEdit
        If e.RowIndex <> -1 Then
            If e.ColumnIndex = 1 Then   'Componet
                If Not dgvCharges.Rows(e.RowIndex).Cells(1).Value Is Nothing AndAlso
                dgvCharges.Rows(e.RowIndex).Cells(1).Value.ToString <> "" Then
                    If IsNumeric(dgvCharges.Rows(e.RowIndex).Cells(1).Value) Then
                        Dim tgpdata() As String
                        Dim TGPType As String = GetTGPType(dgvCharges.Rows(e.RowIndex).Cells(1).Value)
                        tgpdata = GetTGPData(dgvCharges.Rows(e.RowIndex).Cells(1).Value, TGPType)
                        If tgpdata(0) = "" Then
                            ClearChargeLine(e.RowIndex)
                        Else
                            PopulateChargeLine(e.RowIndex, tgpdata)
                            dgvCharges.RowCount += 1
                        End If
                    Else
                        MsgBox("Component ID is a non-alpha characters only")
                        ClearChargeLine(e.RowIndex)
                    End If
                Else
                    ClearChargeLine(e.RowIndex)
                End If
            ElseIf e.ColumnIndex = 5 Then   'Pointer
                If Not dgvCharges.Rows(e.RowIndex).Cells(e.ColumnIndex).Value Is Nothing AndAlso
                Trim(dgvCharges.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString) <> "" Then
                    Dim ICD9S As String = ""
                    Dim i As Integer
                    For i = 0 To DGVICD9s.RowCount - 1
                        If Not DGVICD9s.Rows(i).Cells(1).Value Is Nothing AndAlso
                        DGVICD9s.Rows(i).Cells(1).Value.ToString <> "" Then
                            If InStr(ICD9S, Trim(DGVICD9s.Rows(i).Cells(1).Value.ToString)) = 0 Then _
                            ICD9S += Trim(DGVICD9s.Rows(i).Cells(1).Value.ToString) & "|"
                        End If
                    Next
                    If ICD9S.EndsWith("|") Then ICD9S = ICD9S.Substring(0, Len(ICD9S) - 1)
                    If ICD9S <> "" Then
                        If Not IsPointerValid(ICD9S, dgvCharges.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString) Then
                            MsgBox("Invalid Pointer", MsgBoxStyle.Critical, "Prolis Billing")
                            dgvCharges.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
                        Else
                            dgvCharges.Rows(e.RowIndex).Cells(14).Value = "U"
                            dgvCharges.Rows(e.RowIndex).Cells(14).Style.BackColor = Color.Green
                            dgvCharges.Rows(e.RowIndex).Cells(14).Style.ForeColor = Color.White
                            chkRev.Enabled = False
                        End If
                    Else
                        MsgBox("Invalid Pointer", MsgBoxStyle.Critical, "Prolis Billing")
                        dgvCharges.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
                    End If
                Else
                    chkBillNow.Checked = False
                End If
                ApplyEdits(Requisits)
            ElseIf e.ColumnIndex = 10 Then   'Price
                If Not IsNumeric(dgvCharges.Rows(e.RowIndex).Cells(10).Value) Then
                    MsgBox("Only numeric entry is allowed")
                    dgvCharges.Rows(e.RowIndex).Cells(10).Value = LinePrice.ToString("0.00")
                Else
                    dgvCharges.Rows(e.RowIndex).Cells(12).Value =
                    (Val(dgvCharges.Rows(e.RowIndex).Cells(10).Value) *
                    Val(dgvCharges.Rows(e.RowIndex).Cells(11).Value)).ToString("0.00")
                End If
            ElseIf e.ColumnIndex = 11 Then  'unit
                If Not IsNumeric(dgvCharges.Rows(e.RowIndex).Cells(11).Value) Then
                    MsgBox("Only numeric entry is allowed")
                    dgvCharges.Rows(e.RowIndex).Cells(11).Value = "1.0"
                Else
                    dgvCharges.Rows(e.RowIndex).Cells(12).Value =
                    (Val(dgvCharges.Rows(e.RowIndex).Cells(10).Value) *
                    Val(dgvCharges.Rows(e.RowIndex).Cells(11).Value)).ToString("0.00")
                    LinePrice = Val(dgvCharges.Rows(e.RowIndex).Cells(10).Value)
                End If
            ElseIf e.ColumnIndex = 14 Then   'STATUS
                If dgvCharges.Rows(e.RowIndex).Cells(14).Value.ToString = "H" Then
                    dgvCharges.Rows(e.RowIndex).Cells(14).Style.BackColor = Color.Yellow
                    dgvCharges.Rows(e.RowIndex).Cells(14).Style.ForeColor = Color.Black
                ElseIf dgvCharges.Rows(e.RowIndex).Cells(14).Value.ToString = "U" Then
                    dgvCharges.Rows(e.RowIndex).Cells(14).Style.BackColor = Color.LightGreen
                    dgvCharges.Rows(e.RowIndex).Cells(14).Style.ForeColor = Color.Black
                Else
                    MsgBox("Invalid entry. Valid entry is either 'H' for Hold or " &
                    "'U' for Unbilled", MsgBoxStyle.Critical, "Prolis")
                    dgvCharges.Rows(e.RowIndex).Cells(14).Value = "U"
                    dgvCharges.Rows(e.RowIndex).Cells(14).Style.BackColor = Color.LightGreen
                    dgvCharges.Rows(e.RowIndex).Cells(14).Style.ForeColor = Color.Black
                End If
            End If
            ChargeDirty = True
            CalculateBTotal()
            CalculateUTotal()
            Ch_Lines = True
            BillingEditProgress()
        End If
    End Sub

    Private Sub dgvCharges_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCharges.CellContentClick
        If chkIsBilled.Checked = False Then
            If e.RowIndex <> -1 Then
                If e.ColumnIndex = 0 And dgvCharges.Rows(e.RowIndex).Cells(3).Value <> "" Then
                    Dim RetVal As Integer = MsgBox("Are you certain to delete this record line?",
                    MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis Billing")
                    If RetVal = vbYes Then
                        ExecuteSqlProcedure("Delete from Req_Billable where Accession_ID = " &
                        Val(txtAccessionID.Text) & " and TGP_ID = " &
                        Val(dgvCharges.Rows(e.RowIndex).Cells(1).Value))
                        ClearChargeLine(e.RowIndex)
                        Ch_Lines = True
                        BillingEditProgress()
                    End If
                ElseIf e.ColumnIndex = 2 Then
                    Dim TGPID As String = frmTGPLookup.ShowDialog
                    If TGPID <> "" Then
                        Dim tgpdata(4) As String
                        Dim TGPType As String = GetTGPType(TGPID)
                        tgpdata = GetTGPData(TGPID, TGPType)
                        PopulateChargeLine(e.RowIndex, tgpdata)
                        dgvCharges.RowCount += 1
                        Ch_Lines = True
                        BillingEditProgress()
                    End If
                ElseIf e.ColumnIndex = 14 Then   'STATUS
                    If dgvCharges.Rows(e.RowIndex).Cells(14).Value = "H" Then
                        dgvCharges.Rows(e.RowIndex).Cells(14).Value = "U"
                        dgvCharges.Rows(e.RowIndex).Cells(14).Style.BackColor = Color.Green
                        dgvCharges.Rows(e.RowIndex).Cells(14).Style.ForeColor = Color.White
                    ElseIf dgvCharges.Rows(e.RowIndex).Cells(14).Value = "U" Then
                        dgvCharges.Rows(e.RowIndex).Cells(14).Value = "H"
                        dgvCharges.Rows(e.RowIndex).Cells(14).Style.BackColor = Color.Yellow
                        dgvCharges.Rows(e.RowIndex).Cells(14).Style.ForeColor = Color.Black
                    End If
                    'ElseIf e.ColumnIndex = 15 Then  'FN
                    '    If FullBill = True Then
                    '        If dgvCharges.Rows(e.RowIndex).Cells(15).Value = True Then
                    '            dgvCharges.Rows(e.RowIndex).Cells(15).Value = False
                    '        Else
                    '            If dgvCharges.Rows(e.RowIndex).Cells(14).Value = "U" Then _
                    '            dgvCharges.Rows(e.RowIndex).Cells(15).Value = True
                    '        End If
                    '    End If
                End If
            End If
            CalculateBTotal()
            CalculateUTotal()
        End If
    End Sub

    Private Sub chkIsGratis_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIsGratis.CheckedChanged
        If chkIsGratis.Checked = False Then
            chkIsGratis.Text = "Charge"
            chkBillNow.Enabled = True
        Else
            chkIsGratis.Text = "Gratis"
            chkBillNow.Enabled = False
        End If
        Ch_Acc = True
        Ch_Lines = True
        BillingEditProgress()
    End Sub

    Private Sub chkIsBilled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIsBilled.CheckedChanged
        If chkIsBilled.Checked = False Then
            chkIsBilled.Text = "Unbilled"
        Else
            chkIsBilled.Text = "Billed"
        End If
    End Sub

    Private Sub btnProviderLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProviderLook.Click
        Dim ProvID As String = frmProviderLookup.ShowDialog
        If ProvID <> "" Then
            DisplayClient(Val(ProvID))
            AccDirty = True
            Ch_Acc = True
            BillingEditProgress()
        End If
    End Sub

    Private Sub btnPayers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPayers.Click
        frmPayers.ShowDialog()
        PopulatePayers()
    End Sub

    Private Sub cmbPayer_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPayer.SelectedIndexChanged
        Ch_TP = True : PayerDirty = True
        BillingEditProgress()
    End Sub

    Private Sub chkTPContract_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTPContract.CheckedChanged
        Ch_TP = chkTPContract.Checked
        BillingEditProgress()
    End Sub

    Private Sub cmbTPPrice_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTPPrice.SelectedIndexChanged
        If cmbBillType.SelectedIndex = 1 Then
            Dim PriceLevel As Integer = cmbTPPrice.SelectedIndex
            For i As Integer = 0 To dgvCharges.RowCount - 1
                If dgvCharges.Rows(i).Cells(1).Value IsNot Nothing _
                AndAlso dgvCharges.Rows(i).Cells(1).Value.ToString <> "" Then
                    dgvCharges.Rows(i).Cells(10).Value =
                    GetLevelPrice(dgvCharges.Rows(i).Cells(1).Value, PriceLevel)
                End If
            Next
        End If
        Ch_TP = True
        BillingEditProgress()
    End Sub

    Private Sub txtGroup_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGroup.Validated
        Ch_TP = True : PayerDirty = True
        BillingEditProgress()
    End Sub

    Private Sub txtPolicy_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPolicy.Validated
        Ch_TP = True : PayerDirty = True
        BillingEditProgress()
    End Sub

    Private Sub txtCopay_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCopay.Validated
        If txtCopay.Text = "" Then txtCopay.Text = "0.00"
        Ch_TP = True
        BillingEditProgress()
    End Sub

    Private Sub cmbRelation_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbRelation.SelectedIndexChanged
        Ch_TP = True : PayerDirty = True
        BillingEditProgress()
    End Sub

    Private Sub btnPatLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatLook.Click
        Dim PatID As String = frmPatLookUp.ShowDialog
        If PatID <> "" Then
            DisplayPatient(Val(PatID))
            Ch_Acc = True
            BillingEditProgress()
        End If
    End Sub

    Private Function GetNextCChargeID(ByVal AccID As Long) As Long  'IsPrimary = true, ArType= 0
        Dim ChargeID As Long = Nothing
        Dim cncid As New SqlConnection(connString)
        cncid.Open()
        Dim cmdcid As New SqlCommand("Select * from " &
        "Accession_Charge where Accession_ID = " & AccID &
        " and Not Charge_ID in (Select ID from Charges) " &
        "and IsPrimary <> 0 and ArType = 0", cncid)
        cmdcid.CommandType = CommandType.Text
        Dim drcid As SqlDataReader = cmdcid.ExecuteReader
        If drcid.HasRows Then
            While drcid.Read
                ChargeID = drcid("Charge_ID")
            End While
        Else
            ChargeID = NextChargeID()
            ExecuteSqlProcedure("Insert into Accession_Charge (Accession_ID, Charge_ID, " &
            "IsPrimary, ArType, Created_On, Created_By, Edited_On, Edited_By) values (" &
            AccID & ", " & ChargeID & ", 1, 0, '" & Date.Now & "', " & ThisUser.ID &
            ", '" & Date.Now & "', " & ThisUser.ID & ")")
        End If
        cncid.Close()
        cncid = Nothing
        ExecuteSqlProcedure("Update Requisitions set BillingType_ID = 0 where ID = " & AccID)
        Return ChargeID
    End Function

    Private Function GetNextTChargeID(ByVal AccID As Long) As Long  'IsPrimary = true, ArType= 1
        Dim ChargeID As Long = Nothing
        Dim cncid As New SqlConnection(connString)
        cncid.Open()
        Dim cmdcid As New SqlCommand("Select * from Accession_Charge " &
        "where Accession_ID = " & AccID & " and IsPrimary <> 0 and ArType = 1 and Not " &
        "Charge_ID in (Select ID from Charges where IsPrimary <> 0 and ArType = 1)", cncid)
        cmdcid.CommandType = CommandType.Text
        Dim drcid As SqlDataReader = cmdcid.ExecuteReader
        If drcid.HasRows Then
            While drcid.Read
                ChargeID = drcid("Charge_ID")
                Exit While
            End While
        Else
            ChargeID = NextChargeID()
            ExecuteSqlProcedure("If not Exists (Select * from Accession_Charge where " &
            "Accession_ID = " & AccID & " and Charge_ID = " & ChargeID & ") Insert into " &
            "Accession_Charge (Accession_ID, Charge_ID, IsPrimary, ArType, Created_On, " &
            "Created_By, Edited_On, Edited_By) values (" & AccID & ", " & ChargeID & ", 1, " &
            "1, '" & Date.Now & "', " & ThisUser.ID & ", '" & Date.Now & "', " & ThisUser.ID & ")")
        End If
        cncid.Close()
        cncid = Nothing
        ExecuteSqlProcedure("Update Requisitions set BillingType_ID = 1 " &
        "where ID = " & AccID & " and BillingType_ID <> 1")
        Return ChargeID
    End Function

    Private Function ChargeIDinCharges(ByVal ChargeID As Long) As Boolean
        Dim IDIN As Boolean = False
        Dim cncid As New SqlConnection(connString)
        cncid.Open()
        Dim cmdcid As New SqlCommand("Select " &
        "* from Charges where ID = " & ChargeID, cncid)
        cmdcid.CommandType = CommandType.Text
        Dim drcid As SqlDataReader = cmdcid.ExecuteReader
        If drcid.HasRows Then IDIN = True
        cncid.Close()
        cncid = Nothing
        Return IDIN
    End Function

    Private Function GetNextPChargeID(ByVal AccID As Long) As Long  'IsPrimary = true, ArType= 2
        Dim ChargeID As Long = Nothing
        Dim cncid As New SqlConnection(connString)
        cncid.Open()
        Dim cmdcid As New SqlCommand("Select * from Accession_Charge " &
        "where Accession_ID = " & AccID & " and ArType = 2 and Not Charge_ID " &
        "in (Select ID from Charges where ArType = 2)", cncid)
        cmdcid.CommandType = CommandType.Text
        Dim drcid As SqlDataReader = cmdcid.ExecuteReader
        If drcid.HasRows Then
            While drcid.Read
                ChargeID = drcid("Charge_ID")
            End While
        Else
            ChargeID = NextChargeID()
            ExecuteSqlProcedure("Insert into Accession_Charge (Accession_ID, Charge_ID, " &
            "IsPrimary, ArType, Created_On, Created_By, Edited_On, Edited_By) values (" &
            AccID & ", " & ChargeID & ", 1, 2, '" & Date.Now & "', " & ThisUser.ID &
            ", '" & Date.Now & "', " & ThisUser.ID & ")")
        End If
        cncid.Close()
        cncid = Nothing
        ExecuteSqlProcedure("Update Requisitions set BillingType_ID = 2 where ID = " & AccID)
        Return ChargeID
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        DisableActions()
        Try
            If txtAccessionID.Text <> "" Then
                If Ch_Acc = True Then
                    UpdateAccession(Val(txtAccessionID.Text))
                    Ch_Acc = False
                End If
                If CommentDirty Then
                    UpdateBComments()
                    CommentDirty = False
                End If
                '
                If DxDirty Then
                    UpdateDx()
                    DxDirty = False
                End If
                '
                SaveReqSCoverage(Val(txtAccessionID.Text), 1)
                If Ch_TP = True Then
                    If cmbRelation.SelectedIndex <> 0 And cmbRelation.SelectedIndex <> -1 And
                     String.IsNullOrEmpty(txtInsAddress.Text) Then
                        MsgBox("Subscriber's address is required",
                        MsgBoxStyle.Critical, "Prolis Billing")
                        Return
                    End If
                    If cmbPayer.SelectedIndex <> -1 And txtPolicy.Text <> "" And
                    ((cmbRelation.SelectedIndex = 0 And txtPatientID.Text <> "") Or
                    (cmbRelation.SelectedIndex <> 0 And txtInsuredID.Text <> "" And
                    txtPatientID.Text <> "" And (txtPatientID.Text <>
                    txtInsuredID.Text))) Then
                        UpdateTP(Val(txtAccessionID.Text))
                    Else
                        MsgBox("Insurance coverage information is not valid, so has " _
                        & "not been saved. Correct the information according to the " _
                        & "PROLIS billing guideline and try again.",
                        MsgBoxStyle.Critical, "Prolis Billing")
                    End If

                    ''Temuree




                End If
                '
                If ValidLines() > 0 Then
                    If Ch_Lines = True Then
                        UpdateLines(Val(txtAccessionID.Text))
                        Ch_Lines = False
                    End If
                    If chkBillNow.Checked = True And LinesToBill() > 0 Then
                        If cmbRelation.SelectedIndex <> 0 And cmbRelation.SelectedIndex <> -1 And
                     String.IsNullOrEmpty(txtInsAddress.Text) Then
                            MsgBox("Subscriber's address is required",
                            MsgBoxStyle.Critical, "Prolis Billing")
                            Return
                        End If
                        BillAccession(Val(txtAccessionID.Text), Trim(txtBox19.Text), Trim(txtPreAuth.Text))
                    ElseIf chkRev.Checked = True And (LinesToReverse() > 0 _
                    Or chkIsBilled.Checked = True) Then
                        ReverseBilling(Val(txtAccessionID.Text))
                    End If
                    txtBox19.Text = "" : txtBox19.Enabled = True : txtBox19.ReadOnly = False
                    txtPreAuth.Text = "" : txtPreAuth.Enabled = True : txtPreAuth.ReadOnly = False
                End If

                'RsA.Find("ID = " & Val(txtAccessionID.Text))
                'DisplayAccession(RsA.Fields("ID").Value)

                ' Filter the DataTable to find the specific row by ID
                Dim foundRows As DataRow() = dtRecords.Select("ID = " & Val(txtAccessionID.Text))

                If foundRows.Length > 0 Then
                    ' Display the accession if a matching row is found
                    DisplayAccession(foundRows(0)("ID").ToString()) ' Old: DisplayAccession(RsA.Fields("ID").Value)
                Else
                    MessageBox.Show("Accession not found!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                'RsA.AbsolutePosition
                If Corrected.Checked Or VoidClaim.Checked Then
                    If orgChargID.Text = "" Then
                        MessageBox.Show("Please enter orignal claim number.")
                        Return
                    Else

                        Dim invid = cmbInvoices.SelectedItem

                        If Corrected.Checked Then

                            ExecuteSqlProcedure("update charges set Billing_Status_Code ='7' ,Orignal_Claim_Number= '" & orgChargID.Text & "' where Id=" & invid)
                        ElseIf VoidClaim.Checked Then

                            ExecuteSqlProcedure("update charges set Billing_Status_Code ='8' ,Orignal_Claim_Number= '" & orgChargID.Text & "' where Id=" & invid)
                        End If
                    End If

                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        InitializeChanges()
        BillingEditProgress()
        EnableActions()
    End Sub
    Private Sub SaveReqSCoverage(ByVal AccID As Long, ByVal PayerID As Long)
        If cmbSIns.SelectedIndex = -1 Or String.IsNullOrEmpty(txtSPolicy.Text) Then
            Return
        End If
        Dim cnrs As New SqlConnection(connString)
        Dim ItemX As MyList

        ItemX = cmbSIns.Items(cmbSIns.SelectedIndex)

        ExecuteSqlProcedure("Delete from Req_Coverage where " &
       "Accession_ID = " & AccID & " and Preference = 'S' and Payer_ID <> " & ItemX.ItemData)
        PayerID = Val(ItemX.ItemData)
        'If txtPatientID.Text <> "" Then _
        '  ExecuteSqlProcedure("Delete from Coverages where Preference = 'S' and " & _
        '  "Patient_ID = " & Val(txtPatientID.Text))
        '

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

        '

        UpdateSInsured()

        Dim values = CommonData.ExecuteQuery("Select count(*) as cnt from Coverages where Patient_ID = " & Val(txtPatientID.Text) & " And Preference = 'S' ")
        For Each v In values
            Try
                Dim cnt = v("cnt")
                If cnt IsNot DBNull.Value Then
                    Dim s = Integer.Parse(cnt)
                    If s < 1 Then
                        UpdatePatSCoverage(Val(txtPatientID.Text), Val(PayerID),
                        Trim(txtSPolicy.Text), Trim(txtSGroup.Text), cmbSRelation.SelectedIndex,
                        IIf(cmbSRelation.SelectedIndex = 0, Val(txtPatientID.Text), Val(txtSSubID.Text)), Nothing, Nothing)
                    End If
                End If
            Catch ex As Exception

            End Try

        Next
        If conractS.Checked Then
            ExecuteSqlProcedure("if Exists ( select * from Req_Coverage where    Accession_ID =" & AccID & " and Payer_ID =" & PayerID & ")  begin  update Req_Coverage set UseForBill=1 where   Accession_ID =" & AccID & " and Payer_ID =" & PayerID & ";  End")
        Else
            ExecuteSqlProcedure("if Exists ( select * from Req_Coverage where    Accession_ID =" & AccID & " and Payer_ID =" & PayerID & ")  begin  update Req_Coverage set UseForBill=0 where   Accession_ID =" & AccID & " and Payer_ID =" & PayerID & ";  End")

        End If
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
            cmdupsert.Parameters.AddWithValue("@HomePhone", PhoneNeat(txtSSubHPhone.Text))

            cmdupsert.Parameters.AddWithValue("@WorkPhone", txtSSubWPhone.Text)
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
    Private Function LinesToBill() As Integer
        Dim LTB As Integer = 0
        Dim i As Integer
        For i = 0 To dgvCharges.RowCount - 1
            If dgvCharges.Rows(i).Cells(1).Value IsNot Nothing AndAlso
            dgvCharges.Rows(i).Cells(1).Value.ToString <> "" Then
                If dgvCharges.Rows(i).Cells(14).Value = "U" And
                dgvCharges.Rows(i).Cells(15).Value = True Then _
                LTB += 1
            End If
        Next
        Return LTB
    End Function

    Private Function LinesToReverse() As Integer
        Dim LTR As Integer = 0
        For i As Integer = 0 To dgvCharges.RowCount - 1
            If dgvCharges.Rows(i).Cells(1).Value IsNot Nothing AndAlso
            dgvCharges.Rows(i).Cells(1).Value.ToString <> "" Then
                If (dgvCharges.Rows(i).Cells(14).Value = "B" Or
                dgvCharges.Rows(i).Cells(14).Value = "R") And
                dgvCharges.Rows(i).Cells(15).Value = "1" Then _
                LTR += 1
            End If
        Next
        Return LTR
    End Function

    Private Function ValidLines() As Integer
        Dim Lines As Integer = IIf(Val(txtCopay.Text) > 0, 1, 0)
        If Lines = 0 Then
            For i As Integer = 0 To dgvCharges.RowCount - 1
                If (dgvCharges.Rows(i).Cells(1).Value IsNot Nothing _
                AndAlso dgvCharges.Rows(i).Cells(1).Value.ToString <> "") Then
                    Lines += 1
                    Exit For
                End If
            Next
        End If
        Return Lines
    End Function

    Private Sub UpdateAccession(ByVal AccID As Long)
        Dim PPayerID As Long
        Dim SPayerID As Long
        Dim ItemX As MyList
        Dim ItemT As MyList = lstAttending.CheckedItems(0)
        If cmbBillType.SelectedIndex = 0 Then   'Client
            PPayerID = Val(txtProviderID.Text)
        ElseIf cmbBillType.SelectedIndex = 1 Then   'TP
            If cmbPayer.SelectedIndex <> -1 Then
                ItemX = cmbPayer.SelectedItem
                PPayerID = ItemX.ItemData
                If txtPatientID.Text <> "" Then
                    SPayerID = Val(txtPatientID.Text)
                Else
                    SPayerID = Val(txtProviderID.Text)
                End If
            ElseIf txtPatientID.Text <> "" Then
                PPayerID = Val(txtPatientID.Text)
            Else
                SPayerID = Val(txtProviderID.Text)
            End If
            'Temur
            If cmbSIns.SelectedIndex <> -1 Then
                ItemX = cmbSIns.SelectedItem
                SPayerID = ItemX.ItemData
            End If
        End If
        '
        ExecuteSqlProcedure("Update Requisitions set IsGratis = " & Convert.ToInt16(chkIsGratis.Checked) &
        ", BillingType_ID = " & cmbBillType.SelectedIndex & ", Patient_ID = " & IIf(Trim(txtPatientID.Text) _
        <> "", Val(txtPatientID.Text), Nothing) & ", OrderingProvider_ID = " & Val(txtProviderID.Text) &
        ", AttendingProvider_ID = " & ItemT.ItemData & ", PrimePayer_ID = " & PPayerID &
        ", SecondPayer_ID = " & SPayerID & " where ID = " & AccID)
        '
        If DxDirty Then
            UpdateDx()
            DxDirty = False
        End If
        If ChargeDirty Then
            For i As Integer = 0 To dgvCharges.RowCount - 1
                UpdateBillable(i, dgvCharges.Rows(i).Cells(14).Value)
            Next
            ChargeDirty = False
        End If
    End Sub

    Private Sub UpdateDx()
        If Trim(txtAccessionID.Text) <> "" Then
            ExecuteSqlProcedure("Delete from Req_Dx where Accession_ID = " & Val(txtAccessionID.Text))
            For i As Integer = 0 To DGVICD9s.RowCount - 1
                If DGVICD9s.Rows(i).Cells(1).Value IsNot Nothing AndAlso
                Trim(DGVICD9s.Rows(i).Cells(1).Value) <> "" Then
                    ExecuteSqlProcedure("Insert into Req_Dx (Accession_ID, Dx_Code, " &
                    "Ordinal) values (" & Val(txtAccessionID.Text) & ", '" &
                    Trim(DGVICD9s.Rows(i).Cells(1).Value) & "', " & i & ")")
                End If
            Next
        End If
    End Sub

    Private Sub UpdateBillable(ByVal RowIndex As Integer, ByVal Status As String)
        If dgvCharges.Rows(RowIndex).Cells(1).Value IsNot Nothing AndAlso
        dgvCharges.Rows(RowIndex).Cells(1).Value.ToString <> "" Then
            Dim conn As New SqlConnection(connString)
            conn.Open()
            Dim cmdupsert As New SqlCommand("Req_Billable_SP", conn)
            cmdupsert.CommandType = CommandType.StoredProcedure
            cmdupsert.Parameters.AddWithValue("@act", "Upsert")
            cmdupsert.Parameters.AddWithValue("@Accession_ID", Val(txtAccessionID.Text))
            cmdupsert.Parameters.AddWithValue("@TGP_ID", dgvCharges.Rows(RowIndex).Cells(1).Value)
            cmdupsert.Parameters.AddWithValue("@Ordinal", RowIndex)
            cmdupsert.Parameters.AddWithValue("@CPT_Code", dgvCharges.Rows(RowIndex).Cells(4).Value)
            cmdupsert.Parameters.AddWithValue("@ICD9", dgvCharges.Rows(RowIndex).Cells(5).Value)
            cmdupsert.Parameters.AddWithValue("@Unit", Val(dgvCharges.Rows(RowIndex).Cells(11).Value))
            cmdupsert.Parameters.AddWithValue("@LinePrice", Val(dgvCharges.Rows(RowIndex).Cells(10).Value))
            cmdupsert.Parameters.AddWithValue("@Extend", Val(dgvCharges.Rows(RowIndex).Cells(12).Value))
            cmdupsert.Parameters.AddWithValue("@Mod1", Trim(dgvCharges.Rows(RowIndex).Cells(6).Value))
            cmdupsert.Parameters.AddWithValue("@Mod2", Trim(dgvCharges.Rows(RowIndex).Cells(7).Value))
            cmdupsert.Parameters.AddWithValue("@Mod3", Trim(dgvCharges.Rows(RowIndex).Cells(8).Value))
            cmdupsert.Parameters.AddWithValue("@Mod4", Trim(dgvCharges.Rows(RowIndex).Cells(9).Value))
            cmdupsert.Parameters.AddWithValue("@Svc_Date", GetServiceDate(Val(txtAccessionID.Text)))
            cmdupsert.Parameters.AddWithValue("@POS_Code", Trim(dgvCharges.Rows(RowIndex).Cells(13).Value))
            cmdupsert.Parameters.AddWithValue("@Bill_Status", Status)
            If Status = "B" AndAlso IsDate(txtBillDate.Text) Then
                cmdupsert.Parameters.AddWithValue("@Billed_On", CDate(txtBillDate.Text))
                cmdupsert.Parameters.AddWithValue("@Billed_By", ThisUser.ID)
            Else
                cmdupsert.Parameters.AddWithValue("@Billed_On", Nothing)
                cmdupsert.Parameters.AddWithValue("@Billed_By", Nothing)
            End If
            cmdupsert.Parameters.AddWithValue("@HL7Output", 0)
            Try
                cmdupsert.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                conn.Close()
                conn = Nothing
            End Try
        End If
    End Sub

    Private Sub UpdateTP(ByVal AccID As Long)
        If cmbPayer.SelectedIndex <> -1 And Trim(txtPolicy.Text) <> "" Then
            Dim ItemX As MyList = cmbPayer.SelectedItem
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
            cmdr.Parameters.AddWithValue("@Accession_ID", Val(txtAccessionID.Text))
            cmdr.Parameters.AddWithValue("@Payer_ID", ItemX.ItemData)
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
            cmdupsert.Parameters.AddWithValue("@Insurance_ID", ItemX.ItemData)
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
            ExecuteSqlProcedure("Update Requisitions set PrimePayer_ID = " & ItemX.ItemData &
            ", BillingType_ID = " & cmbBillType.SelectedIndex & " where ID = " & AccID)
        End If
    End Sub

    Private Sub UpdateLineItem(ByVal AccID As Long, ByVal RowIndex As Integer)
        Dim billinfo() As String = GetBillInfo(AccID, dgvCharges.Rows(RowIndex).Cells(1).Value)
        UpdateBillable(RowIndex, billinfo(2))
    End Sub

    Private Sub UpdateLines(ByVal AccID As Long)
        Dim ValidTGPs As String = ""
        For i As Integer = 0 To dgvCharges.RowCount - 1
            If dgvCharges.Rows(i).Cells(1).Value IsNot Nothing AndAlso
            dgvCharges.Rows(i).Cells(1).Value.ToString <> "" AndAlso
            dgvCharges.Rows(i).Cells(14).Value = "U" Then     'Valid row
                UpdateLineItem(AccID, i)
                ValidTGPs += dgvCharges.Rows(i).Cells(1).Value & ", "
            End If
        Next
        If ValidTGPs.EndsWith(", ") Then ValidTGPs = Microsoft.VisualBasic.Left(ValidTGPs, Len(ValidTGPs) - 2)
        'If ValidTGPs <> "" Then _
        'Executesqlprocedure("Delete from Req_Billable where Accession_ID = " & _
        'AccID & " and Not TGP_ID in (" & ValidTGPs & ")")
    End Sub

    Private Sub UpdateBComments()
        If CommentDirty = True And Trim(txtAccessionID.Text) <> "" Then
            Dim i As Integer
            For i = 0 To dgvComments.RowCount - 1
                If dgvComments.Rows(i).Cells(2).Value IsNot Nothing AndAlso
                Trim(dgvComments.Rows(i).Cells(2).Value.ToString) <> "" Then
                    ExecuteSqlProcedure("If not Exists (Select * from Req_Comments where " &
                    "Accession_ID = " & Val(txtAccessionID.Text) & " and Serial_ID = " & i &
                    ") Insert into Req_Comments (Accession_ID, Serial_ID, User_ID, Comment, " &
                    "Dated, Associated_To) values (" & Val(txtAccessionID.Text) & ", " & i &
                    ", " & ThisUser.ID & ", '" & Trim(dgvComments.Rows(i).Cells(2).Value) &
                    "', '" & Format(Date.Now, SystemConfig.DateFormat & " HH:mm") & "', 'B')")
                End If
            Next
            CommentDirty = False
        End If
    End Sub

    Private Function BillAccession(ByVal AccID As Long, ByVal Reason As String, ByVal PreAuth As String) As Single
        Dim ArID As Long
        Dim AccAmount As Single = 0
        Dim Discreteds As String = ""
        Dim Recs As Integer = 0
        Dim IsPrimary As Integer = 1
        Dim InsuranceType As String = "Primary"
        Dim InvAmt As Single = 0
        Dim ChargeID As Long = 0
        'Dim ChargeItems() As String = UpdateChargeItems()
        '0=T, 1=U, 2=N, 3=H, 4=P

        Dim ItemX As MyList = cmbPayer.SelectedItem
        If conractS.Checked Then
            ItemX = cmbSIns.SelectedItem
            InsuranceType = "Secondary"
        End If
        If cmbBillType.SelectedIndex = 0 Then  'Client
            ArID = Val(txtProviderID.Text)
        ElseIf cmbBillType.SelectedIndex = 1 Then  'TP
            ArID = ItemX.ItemData
        Else
            ArID = Val(txtPatientID.Text)
        End If
        '
        If cmbBillType.SelectedIndex = 1 Then
            Dim cnb As New SqlConnection(connString)
            cnb.Open()
            Dim cmdb As New SqlCommand("Select TGP_ID from " &
            "Req_Billable where Bill_Status = 'U' and Accession_ID = " &
            AccID & " and CPT_Code in (Select CPT_Code from " &
            "DiscreteClaimCodes)", cnb)
            cmdb.CommandType = CommandType.Text
            Dim drb As SqlDataReader = cmdb.ExecuteReader
            If drb.HasRows Then
                ChargeID = GetChargeID(cmbBillType.SelectedIndex, ArID, AccID, Reason, PreAuth)
                While drb.Read
                    Discreteds += drb("TGP_ID").ToString & "|"
                End While
                If Discreteds.Length > 1 Then Discreteds =
                Microsoft.VisualBasic.Mid(Discreteds, 1, Len(Discreteds) - 1)
                '
                InvAmt = BillChargeDetail(AccID, ChargeID, Discreteds)
                UpdateCharges(ChargeID,
                cmbBillType.SelectedIndex, ArID, AccID, Math.Round(InvAmt, 2))
            End If
            cnb.Close()
            cnb = Nothing
            '
            If Discreteds <> "" And Not Discreteds.EndsWith("|") Then Discreteds += "|"
            'Non discreteds below
            InvAmt = 0
            ChargeID = GetChargeID(cmbBillType.SelectedIndex, ArID, AccID, Reason, PreAuth, IsPrimary, InsuranceType)
            For i As Integer = 0 To dgvCharges.RowCount - 1
                If dgvCharges.Rows(i).Cells(14).Value = "U" AndAlso
                dgvCharges.Rows(i).Cells(15).Value = "1" AndAlso
                InStr(Discreteds,
                dgvCharges.Rows(i).Cells(1).Value.ToString & "|") = 0 Then
                    If Recs < 50 Then
                        InvAmt += BillLineItem(ChargeID, i)
                        Recs += 1
                    Else
                        UpdateCharges(ChargeID, cmbBillType.SelectedIndex,
                        ArID, AccID, Math.Round(InvAmt, 2))
                        AccAmount += InvAmt
                        InvAmt = 0
                        Recs = 0
                        ChargeID = GetChargeID(cmbBillType.SelectedIndex, ArID, AccID, Reason, PreAuth)
                        InvAmt += BillLineItem(ChargeID, i)
                        Recs += 1
                    End If
                End If
            Next
            If Recs > 0 And Recs <= 50 Then
                UpdateCharges(ChargeID, cmbBillType.SelectedIndex,
                ArID, AccID, Math.Round(InvAmt, 2))
                AccAmount += InvAmt
                InvAmt = 0
                Recs = 0
            End If
        Else    'other 2 bill types
            InvAmt = 0
            ChargeID = GetChargeID(cmbBillType.SelectedIndex, ArID, AccID, Reason, PreAuth)
            For i As Integer = 0 To dgvCharges.RowCount - 1
                If dgvCharges.Rows(i).Cells(14).Value = "U" AndAlso
                dgvCharges.Rows(i).Cells(15).Value = True Then
                    InvAmt += BillLineItem(ChargeID, i)
                End If
            Next
            UpdateCharges(ChargeID, cmbBillType.SelectedIndex,
            ArID, AccID, Math.Round(InvAmt, 2))
            AccAmount += InvAmt
            InvAmt = 0
            Recs = 0
        End If
        '
        If cmbBillType.SelectedIndex = 1 And Val(txtCopay.Text) > 0 Then
            Dim CoChargeID As Long = GetChargeID(2, Val(txtPatientID.Text), AccID, "Copayment", PreAuth)
            BillCoChargeDetail(CoChargeID, Val(txtCopay.Text))
            UpdateCharges(CoChargeID, 2, Val(txtPatientID.Text), AccID, Val(txtCopay.Text))
        End If
        Return AccAmount + Val(txtCopay.Text)
    End Function

    Private Function BillLineItem(ByVal ChargeID As Long,
    ByVal Line As Integer) As Double
        Dim cnbli As New SqlConnection(connString)
        cnbli.Open()
        Dim cmdupsert As New SqlCommand("Charge_Detail_SP", cnbli)
        cmdupsert.CommandType = CommandType.StoredProcedure
        cmdupsert.Parameters.AddWithValue("@act", "Upsert")
        cmdupsert.Parameters.AddWithValue("@Charge_ID", ChargeID)
        cmdupsert.Parameters.AddWithValue("@TGP_ID", dgvCharges.Rows(Line).Cells(1).Value)
        cmdupsert.Parameters.AddWithValue("@Ordinal", Line)
        cmdupsert.Parameters.AddWithValue("@CPT_Code", Trim(dgvCharges.Rows(Line).Cells(4).Value))
        cmdupsert.Parameters.AddWithValue("@ICD9", Trim(dgvCharges.Rows(Line).Cells(5).Value))
        cmdupsert.Parameters.AddWithValue("@Unit", dgvCharges.Rows(Line).Cells(11).Value)
        cmdupsert.Parameters.AddWithValue("@LinePrice", dgvCharges.Rows(Line).Cells(10).Value)
        cmdupsert.Parameters.AddWithValue("@Extend", Math.Round(Val(dgvCharges.Rows(Line).Cells(12).Value), 2))
        cmdupsert.Parameters.AddWithValue("@Mod1", Trim(dgvCharges.Rows(Line).Cells(6).Value))
        cmdupsert.Parameters.AddWithValue("@Mod2", Trim(dgvCharges.Rows(Line).Cells(7).Value))
        cmdupsert.Parameters.AddWithValue("@Mod3", Trim(dgvCharges.Rows(Line).Cells(8).Value))
        cmdupsert.Parameters.AddWithValue("@Mod4", Trim(dgvCharges.Rows(Line).Cells(9).Value))
        cmdupsert.Parameters.AddWithValue("@POS_Code", Trim(dgvCharges.Rows(Line).Cells(13).Value))
        cmdupsert.Parameters.AddWithValue("@Billed_On", txtBillDate.Text & " " & Format(Date.Now, "HH:mm"))
        cmdupsert.Parameters.AddWithValue("@Billed_By", ThisUser.ID)
        Try
            cmdupsert.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cnbli.Close()
            cnbli = Nothing
        End Try
        '
        UpdateLineItem(Val(txtAccessionID.Text), Line)
        '
        ExecuteSqlProcedure("Update Req_TGP Set Billed = 1 where Accession_ID = " &
        Val(txtAccessionID.Text) & " and TGP_ID = " & dgvCharges.Rows(Line).Cells(1).Value)
        '
        Return Math.Round(Val(dgvCharges.Rows(Line).Cells(12).Value), 2)
    End Function

    Private Sub BillCoChargeDetail(ByVal ChargeID As Long, ByVal Copay As Single)
        Dim TGPID As Integer = GetTGPID("CoPayment")
        Dim cnbli As New SqlConnection(connString)
        cnbli.Open()
        Dim cmdupsert As New SqlCommand("Charge_Detail_SP", cnbli)
        cmdupsert.CommandType = CommandType.StoredProcedure
        cmdupsert.Parameters.AddWithValue("@act", "Upsert")
        cmdupsert.Parameters.AddWithValue("@Charge_ID", ChargeID)
        cmdupsert.Parameters.AddWithValue("@TGP_ID", TGPID)
        cmdupsert.Parameters.AddWithValue("@Ordinal", 0)
        cmdupsert.Parameters.AddWithValue("@CPT_Code", "")
        cmdupsert.Parameters.AddWithValue("@ICD9", "")
        cmdupsert.Parameters.AddWithValue("@Unit", 1.0)
        cmdupsert.Parameters.AddWithValue("@LinePrice", Copay)
        cmdupsert.Parameters.AddWithValue("@Extend", Copay)
        cmdupsert.Parameters.AddWithValue("@Mod1", "")
        cmdupsert.Parameters.AddWithValue("@Mod2", "")
        cmdupsert.Parameters.AddWithValue("@Mod3", "")
        cmdupsert.Parameters.AddWithValue("@Mod4", "")
        cmdupsert.Parameters.AddWithValue("@POS_Code", "81")
        cmdupsert.Parameters.AddWithValue("@Billed_On", Date.Now)
        cmdupsert.Parameters.AddWithValue("@Billed_By", ThisUser.ID)
        Try
            cmdupsert.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cnbli.Close()
            cnbli = Nothing
        End Try
    End Sub

    Private Function GetTGPID(ByVal TGPName As String) As Integer
        Dim TGPID As Integer = -1
        Dim cnb As New SqlConnection(connString)
        cnb.Open()
        Dim cmdb As New SqlCommand("Select ID " &
        "from Tests where Name = '" & TGPName & "%'", cnb)
        cmdb.CommandType = CommandType.Text
        Dim drb As SqlDataReader = cmdb.ExecuteReader
        If drb.HasRows Then
            While drb.Read
                TGPID = drb("ID")
            End While
        End If
        cnb.Close()
        cnb = Nothing
        Return TGPID
    End Function

    Private Sub ReverseBilling(ByVal AccID As Long)
        Dim BARVALS() As String = GetBARValuesB(AccID)
        'GetBARValuesB(AccID)  gives billing values
        ' 0 = AccID, 1 = ArType, 2 = ArID, 3 = "1", 4 = DocNo, 5 = Amount 
        'GetBARValuesP(AccID)  gives Payment values
        ' 0 = AccID, 1 = ArType, 2 = ArID, 3 = "3", 4 = DocNo, 5 = Amount 
        If dgvCharges.RowCount > 0 Then
            Dim RevAmt As Single = 0
            Dim TGPIDs As String = ""
            'Dim InvVALS() As Single
            For i As Integer = 0 To dgvCharges.RowCount - 1
                If (dgvCharges.Rows(i).Cells(1).Value IsNot Nothing _
                AndAlso dgvCharges.Rows(i).Cells(1).Value.ToString <> "") _
                AndAlso dgvCharges.Rows(i).Cells(15).Value = "1" _
                AndAlso dgvCharges.Rows(i).Cells(14).Value <> "P" Then
                    TGPIDs += dgvCharges.Rows(i).Cells(1).Value & ", "
                    If dgvCharges.Rows(i).Cells(14).Value = "B" Then
                        RevAmt += Val(dgvCharges.Rows(i).Cells(12).Value)
                    End If
                End If
            Next
            If TGPIDs.EndsWith(", ") Then TGPIDs = TGPIDs.Substring(0, Len(TGPIDs) - 2)
            Dim ChargeIDs As String = ""
            If TGPIDs <> "" Then
                ExecuteSqlProcedure("Update Req_TGP Set Billed = 0 where " &
                "Accession_ID = " & AccID & " and TGP_ID in (" & TGPIDs & ")")
                '
                ExecuteSqlProcedure("Update Req_Billable Set Bill_Status = 'U', Billed_On = " &
                "NULL, Billed_By = NULL where Accession_ID = " & AccID & " and TGP_ID in (" & TGPIDs & ")")
                '
                ChargeIDs = GetAccTGPInvoiceID(AccID, TGPIDs)
                If ChargeIDs.EndsWith(", ") Then ChargeIDs = ChargeIDs.Substring(0, Len(ChargeIDs) - 2)
                If ChargeIDs <> "" Then
                    ExecuteSqlProcedure("Delete from Charge_Detail where Charge_ID in (" &
                    ChargeIDs & ") and TGP_ID in (" & TGPIDs & ")")
                    '
                    ExecuteSqlProcedure("Delete from Charge_Detail where Charge_ID in (" & ChargeIDs & ")")
                    ExecuteSqlProcedure("Delete from Charges where ID in (" & ChargeIDs & ")")
                End If
            End If
            '
            'ChargeIDs = GetAccTGPInvoiceID(AccID, TGPIDs)
            'If ChargeIDs <> "" Then _
            'SynchronizeInvoice(ChargeIDs)
            If SystemConfig.BARHistory = True Then Log_BAR_Event(Val(BARVALS(0)),
            Val(BARVALS(1)), Val(BARVALS(2)), 2, Val(BARVALS(4)), Val(BARVALS(5)) -
            RevAmt)
        End If
    End Sub

    Private Function GetAccTGPInvoiceID(ByVal AccID As Long, ByVal TGPIDs As String) As String
        Dim InvIDs As String = ""
        Dim cninv As New SqlConnection(connString)
        cninv.Open()
        Dim cmdinv As New SqlCommand("Select ID from Charges where Accession_ID = " &
        AccID & " and not ID in (Select Charge_ID from Payment_Detail where Charge_ID in " &
        "(Select ID from Charges where Accession_ID = " & AccID & "))", cninv)
        cmdinv.CommandType = CommandType.Text
        Dim drinv As SqlDataReader = cmdinv.ExecuteReader
        If drinv.HasRows Then
            While drinv.Read
                InvIDs += drinv("ID").ToString & ", "
            End While
            If InvIDs.EndsWith(", ") Then InvIDs = InvIDs.Substring(0, Len(InvIDs) - 2)
        End If
        cninv.Close()
        cninv = Nothing
        Return InvIDs
    End Function

    Private Function GetChargeID(ByVal ArType As Byte, ByVal ArID As Long,
    ByVal AccID As Long, ByVal Reason As String, ByVal PreAuth As String, Optional ByVal isprimary As Integer = 1, Optional ByVal InsuranceType As String = "Primary") As Long
        Dim ChargeID As Long


        If ArType = 0 Then  'Client
            ChargeID = GetNextCChargeID(AccID)
        ElseIf ArType = 1 Then  'TP
            ChargeID = GetNextTChargeID(AccID)
        Else    'P
            ChargeID = GetNextPChargeID(AccID)
        End If
        Dim BillDate As Date
        Dim SvcDate As Date = CDate(txtSvcDate.Text & " " & Format(Date.Now, "HH:mm"))
        If chkBillDate.Checked = False Then
            BillDate = DateAdd(DateInterval.Hour, 3, SvcDate)
        Else
            BillDate = CDate(txtBillDate.Text & " " & Format(Date.Now, "HH:mm"))
        End If
        'If chkECC.Checked Then
        '    chkECC.Checked = False
        'ElseIf chkECC.Checked = False Then
        '    chkECC.Checked = True

        'End If
        Dim sSQL As String = "If Exists (Select * from Charges where ID = " & ChargeID & ") Update Charges " &
        "Set ArType = " & ArType & ", Ar_ID = " & ArID & ", IsPrimary = " & isprimary & ",InsuranceType='" & InsuranceType & "', Accession_ID = " & AccID & ", " &
        "BillReason = '" & Trim(Reason) & "', PreAuthorization = '" & Trim(PreAuth) & "', Bill_Date = '" &
        BillDate & "', " & "Svc_Date = '" & SvcDate & "', Due_Date = '" & BillDate.AddDays(Val(txtDueDays.Text)) &
        "', Term = '" & Trim(txtTerm.Text) & "', Note = '', Output = 0, ECC = " & CType(chkECC.Checked, Int16) &
        ", LastEditedOn = '" & Date.Now & "', EditedBy = " & ThisUser.ID & " where ID = " & ChargeID &
        " Else Insert into Charges (ID, ArType, IsPrimary,InsuranceType, Ar_ID, Accession_ID, BillReason, PreAuthorization, " &
        "Bill_Date, Svc_Date, Due_Date, Term, Note, Output, ECC, LastEditedOn, EditedBy) values (" & ChargeID &
        ", " & ArType & ", " & isprimary & ",'" & InsuranceType & "', " & ArID & ", " & AccID & ", '" & Trim(Reason) & "', '" & Trim(PreAuth) & "', '" &
        BillDate & "', '" & SvcDate & "', '" & BillDate.AddDays(Val(txtDueDays.Text)) & "', '" & Trim(txtTerm.Text) &
        "', '', 0, " & CType(chkECC.Checked, Int16) & ", '" & Date.Now & "', " & ThisUser.ID & ")"
        ExecuteSqlProcedure(sSQL)
        '
        Return ChargeID
    End Function

    Private Function UpdateCharges(ByVal ChargeID As Long, ByVal ArType As Byte,
    ByVal ArID As Long, ByVal AccID As Long, ByVal Amt As Single) As Single
        Dim Tax As Single = Math.Round(Val(txtTax.Text) * Amt / 100, 2)
        Dim sSQL As String = "Update Charges Set NetAmount = " & Math.Round(Amt, 2) & ", TaxAmount = " &
        Tax & ", GrossAmount = " & Math.Round(Tax + Amt, 2) & " where ID = " & ChargeID
        ExecuteSqlProcedure(sSQL)
        '
        If SystemConfig.BARHistory = True Then _
        Log_BAR_Event(AccID, ArType, ArID, 1, ChargeID, Amt + Tax)
        UpdateCharges = Amt + Tax
    End Function

    Private Function BillChargeDetail(ByVal AccID As Long, ByVal ChargeID As Long, ByVal ChargeItems As String) As Single
        Dim i As Integer
        Dim AccPrice As Single = 0
        Dim TGPIDs As String = ""
        '
        ChargeItems = Replace(ChargeItems, "|", ", ")
        If ChargeItems <> "" Then
            Dim cnbcd As New SqlConnection(connString)
            cnbcd.Open()
            Dim cmdbcd As New SqlCommand("Select * from Req_Billable " &
            "where Accession_ID = " & AccID & " and TGP_ID in (" & ChargeItems & ")", cnbcd)
            cmdbcd.CommandType = CommandType.Text
            Dim drbcd As SqlDataReader = cmdbcd.ExecuteReader
            If drbcd.HasRows Then
                While drbcd.Read
                    If drbcd("Bill_Status") = "U" Then
                        Dim cnbli As New SqlConnection(connString)
                        cnbli.Open()
                        Dim cmdupsert As New SqlCommand("Charge_Detail_SP", cnbli)
                        cmdupsert.CommandType = CommandType.StoredProcedure
                        cmdupsert.Parameters.AddWithValue("@act", "Upsert")
                        cmdupsert.Parameters.AddWithValue("@Charge_ID", ChargeID)
                        cmdupsert.Parameters.AddWithValue("@TGP_ID", drbcd("TGP_ID"))
                        cmdupsert.Parameters.AddWithValue("@Ordinal", i)
                        cmdupsert.Parameters.AddWithValue("@CPT_Code", Trim(drbcd("CPT_Code")))
                        cmdupsert.Parameters.AddWithValue("@ICD9", Trim(drbcd("ICD9")))
                        cmdupsert.Parameters.AddWithValue("@Unit", drbcd("Unit"))
                        cmdupsert.Parameters.AddWithValue("@LinePrice", drbcd("LinePrice"))
                        cmdupsert.Parameters.AddWithValue("@Extend",
                        Math.Round(drbcd("Unit") * drbcd("LinePrice"), 2))
                        cmdupsert.Parameters.AddWithValue("@Mod1", Trim(drbcd("Mod1")))
                        cmdupsert.Parameters.AddWithValue("@Mod2", Trim(drbcd("Mod2")))
                        cmdupsert.Parameters.AddWithValue("@Mod3", Trim(drbcd("Mod3")))
                        cmdupsert.Parameters.AddWithValue("@Mod4", Trim(drbcd("Mod4")))
                        cmdupsert.Parameters.AddWithValue("@POS_Code", Trim(drbcd("POS_Code")))
                        cmdupsert.Parameters.AddWithValue("@Billed_On", Date.Now)
                        cmdupsert.Parameters.AddWithValue("@Billed_By", ThisUser.ID)
                        Try
                            cmdupsert.ExecuteNonQuery()
                            i += 1
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        Finally
                            cnbli.Close()
                            cnbli = Nothing
                        End Try
                        AccPrice += drbcd("Unit") * drbcd("LinePrice")
                        ExecuteSqlProcedure("Update Req_Billable set Bill_Status = 'B', " &
                        "Billed_On = '" & Date.Now & "', Billed_By = " & ThisUser.ID &
                        " where Accession_ID = " & AccID & " and TGP_ID = " & drbcd("TGP_ID"))
                    End If
                End While
            End If
            cnbcd.Close()
            cnbcd = Nothing
            ExecuteSqlProcedure("Update Req_TGP Set Billed = 1 where Accession_ID = " &
            Val(txtAccessionID.Text) & " and TGP_ID in (" & ChargeItems & ")")
        End If
        '
        Return AccPrice
    End Function

    Private Function NextChargeID() As Long
        Dim CID As Long = 1
        Dim cncid As New SqlConnection(connString)
        cncid.Open()
        Dim cmdcid As New SqlCommand("Select " &
        "Max(ID) as LastID from Charges", cncid)
        cmdcid.CommandType = CommandType.Text
        Dim drcid As SqlDataReader = cmdcid.ExecuteReader
        If drcid.HasRows Then
            While drcid.Read
                If drcid("LastID") IsNot DBNull.Value _
                Then CID = drcid("LastID") + 1
            End While
        End If
        cncid.Close()
        cncid = Nothing
        Return CID
    End Function

    Private Sub txtTax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTax.KeyPress
        Prices(txtTax, e)
    End Sub

    Private Sub txtTax_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTax.Validated
        If txtTax.Text = "" Then txtTax.Text = "0.00"
    End Sub

    Private Sub txtTerm_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTerm.Validated
        If txtTerm.Text = "" Then txtTerm.Text = "Net " & txtDueDays.Text & " Days"
    End Sub

    Private Sub btnTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTarget.Click
        DisableActions()
        lblEligibility.BackColor = Color.PeachPuff
        conractS.Checked = False
        Dim sSQL As String = ""
        If IsDate(dtpDateFrom.Text) Or IsDate(dtpDateTo.Text) Or txtAccFrom.Text <> "" Or txtAccTo.Text <> "" Or HasDiscretes() Then
            If cmbBillType.SelectedIndex = 0 Then   'Client Billing
                If cmbABU.SelectedIndex = 0 Then    'All
                    sSQL = "Select distinct * from Providers where ID in (Select " &
                    "OrderingProvider_ID from Requisitions where BillingType_ID = 0 " &
                    "and ID in (Select distinct Accession_ID from Req_Billable)"
                ElseIf cmbABU.SelectedIndex = 1 Then    'Billed
                    sSQL = "Select distinct * from Providers where ID in (Select " &
                    "OrderingProvider_ID from Requisitions where BillingType_ID = 0 " &
                    "and ID in (Select distinct Accession_ID from Req_Billable where " &
                    "Bill_Status <> 'U' and Bill_Status <> 'H')"
                ElseIf cmbABU.SelectedIndex = 2 Then    'Part Billed
                    sSQL = "Select distinct * from Providers where ID in (Select " &
                    "OrderingProvider_ID from Requisitions where BillingType_ID = 0 " &
                    "and ID in (Select distinct Accession_ID from Req_Billable where " &
                    "Bill_Status = 'B') and ID in (Select distinct Accession_ID from " &
                    "Req_Billable where Bill_Status <> 'B')"
                Else    'Unbilled
                    sSQL = "Select distinct * from Providers where ID in (Select " &
                    "OrderingProvider_ID from Requisitions where BillingType_ID = 0 " &
                    "and ID in (Select distinct Accession_ID from Req_Billable where " &
                    "Bill_Status <> 'B')"
                End If
            ElseIf cmbBillType.SelectedIndex = 1 Then   'Third Party
                If cmbABU.SelectedIndex = 0 Then    'All
                    sSQL = "Select distinct * from Payers where ID in (Select distinct " &
                    "PrimePayer_ID from Requisitions where BillingType_ID = 1 and ID " &
                    "in (Select distinct Accession_ID from Req_Billable)"
                ElseIf cmbABU.SelectedIndex = 1 Then    'Billed
                    sSQL = "Select distinct * from Payers where ID in (Select distinct " &
                    "PrimePayer_ID from Requisitions where BillingType_ID = 1 and ID " &
                    "in (Select distinct Accession_ID from Req_Billable where Not " &
                    "Bill_Status in ('U', 'H'))"
                ElseIf cmbABU.SelectedIndex = 2 Then    'Part Billed
                    sSQL = "Select distinct * from Payers where ID in (Select distinct " &
                    "PrimePayer_ID from Requisitions where BillingType_ID = 1 and ID " &
                    "in (Select distinct Accession_ID from Req_Billable where Bill_Status " &
                    "= 'B') and ID in (Select distinct Accession_ID from Req_Billable " &
                    "where Bill_Status <> 'B')"
                Else    'Unbilled
                    sSQL = "Select distinct * from Payers where ID in (Select distinct " &
                    "PrimePayer_ID from Requisitions where BillingType_ID = 1 and ID " &
                    "in (Select distinct Accession_ID from Req_Billable where " &
                    "Bill_Status <> 'B')"
                End If
            Else    'Patient
                If cmbABU.SelectedIndex = 0 Then    'All
                    sSQL = "Select * from Patients where ID in (Select Patient_ID from " &
                    "Requisitions where BillingType_ID = 2 and ID in (Select distinct " &
                    "Accession_ID from Req_Billable)"
                ElseIf cmbABU.SelectedIndex = 1 Then    'Billed"
                    sSQL = "Select * from Patients where ID in (Select Patient_ID from " &
                    "Requisitions where BillingType_ID = 2 and ID in (Select distinct " &
                    "Accession_ID from Req_Billable where Not Bill_Status in ('U', 'H'))"
                ElseIf cmbABU.SelectedIndex = 2 Then    'Part Billed
                    sSQL = "Select distinct * from Patients where ID in (Select Patient_ID " &
                    "from Requisitions where BillingType_ID = 2 and ID in (Select distinct " &
                    "Accession_ID from Req_Billable where Bill_Status = 'B') and ID in (Select " &
                    "distinct Accession_ID from Req_Billable where Bill_Status <> 'B')"
                Else    'Unbilled
                    sSQL = "Select * from Patients where ID in (Select Patient_ID from " &
                    "Requisitions where BillingType_ID = 2 and ID in (Select distinct " &
                    "Accession_ID from Req_Billable where Bill_Status <> 'B')"
                End If
            End If

            '
            If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                sSQL += " and AccessionDate between '" & dtpDateFrom.Text &
                "' and '" & dtpDateFrom.Text & " 23:59:00')"
            ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                sSQL += " and AccessionDate between '" & dtpDateTo.Text &
               "' and '" & dtpDateTo.Text & " 23:59:00')"
            ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                sSQL += " and AccessionDate between '" & dtpDateFrom.Text &
                "' and '" & dtpDateTo.Text & " 23:59:00')"
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                If chkAI.Checked = False Then
                    sSQL += " and ID = " & Val(txtAccFrom.Text) & ")"
                Else
                    sSQL += " and ID in (Select Accession_ID from Charges " &
                    "where ID = " & Val(txtAccFrom.Text) & "))"
                End If
            ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                If chkAI.Checked = False Then
                    sSQL += " and ID = " & Val(txtAccTo.Text) & ")"
                Else
                    sSQL += " and ID in (Select Accession_ID from Charges " &
                    "where ID = " & Val(txtAccTo.Text) & "))"
                End If
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                If chkAI.Checked = False Then
                    sSQL += " and ID between " & Val(txtAccFrom.Text) &
                    " and " & Val(txtAccTo.Text) & ")"
                Else
                    sSQL += " and ID in (Select Accession_ID from Charges " &
                    "where ID between " & Val(txtAccFrom.Text) & " and " &
                    Val(txtAccTo.Text) & "))"
                End If
            ElseIf HasDiscretes() Then
                Dim VALS As String = ""
                For i As Integer = 0 To dgvDiscrete.RowCount - 1
                    If dgvDiscrete.Rows(i).Cells(0).Value IsNot Nothing _
                    AndAlso Trim(dgvDiscrete.Rows(i).Cells(0).Value) <> "" Then
                        VALS += Trim(dgvDiscrete.Rows(i).Cells(0).Value) & ", "
                    End If
                Next
                If VALS.EndsWith(", ") Then VALS = Microsoft.VisualBasic.Mid(VALS, 1, Len(VALS) - 2)
                If VALS <> "" Then
                    If chkAI.Checked = False Then
                        sSQL += " and ID in (" & VALS & "))"
                    Else
                        sSQL += " and ID in (Select Accession_ID from Charges " &
                        "where ID in (" & VALS & ")))"
                    End If
                End If
            End If
            '
            If cmbBillType.SelectedIndex = 0 Then   'Provider
                sSQL += " and Not LastName_BSN like 'zz%' Order by LastName_BSN"
            ElseIf cmbBillType.SelectedIndex = 1 Then   'TP
                sSQL += " and Not PayerName like 'zz%' Order by PayerName"
            Else    'Patient
                sSQL += " and Not LastName like 'zz%' Order by LastName, FirstName"
            End If
            '
            lstTargets.Items.Clear()
            Dim Provider As String = ""
            Dim cnt As New SqlConnection(connString)
            cnt.Open()
            Dim cmdt As New SqlCommand(sSQL, cnt)
            cmdt.CommandType = CommandType.Text
            Dim drt As SqlDataReader = cmdt.ExecuteReader
            If drt.HasRows Then
                While drt.Read
                    If cmbBillType.SelectedIndex = 0 Then   'Provider
                        If drt("IsIndividual") = False Then
                            Provider = drt("LastName_BSN")
                        Else
                            Provider = drt("LastName_BSN") & ", " & drt("FirstName")
                            If drt("Degree") IsNot DBNull.Value AndAlso
                            Trim(drt("Degree")) <> "" Then Provider += " " & Trim(drt("Degree"))
                        End If
                        lstTargets.Items.Add(New MyList(Provider, drt("ID")))
                    ElseIf cmbBillType.SelectedIndex = 1 Then   'Third Party
                        lstTargets.Items.Add(New MyList(drt("PayerName"), drt("ID")))
                    Else    'Patient
                        lstTargets.Items.Add(New MyList(drt("LastName") & ", " & drt("FirstName"), drt("ID")))
                    End If
                    lstTargets.SetItemChecked(lstTargets.Items.Count - 1, True)
                End While
            End If
            cnt.Close()
            cnt = Nothing
        End If
        ClearForm()
        EnableActions()
    End Sub

    Private Function HasDiscretes() As Boolean
        Dim HasVal As Boolean = False
        For i As Integer = 0 To dgvDiscrete.RowCount - 1
            If dgvDiscrete.Rows(i).Cells(0).Value IsNot Nothing _
            AndAlso Trim(dgvDiscrete.Rows(i).Cells(0).Value) <> "" Then
                HasVal = True
                Exit For
            End If
        Next
        Return HasVal
    End Function

    Private Sub ClearForm()
        txtNavStatus.Text = ""
        txtBox19.Text = ""
        txtPreAuth.Text = ""
        ClearAccession()
        'If RsA.State = 1 Then RsA.Close()
        ClearDxs()
    End Sub

    Private Sub ClearDxs()
        Dim i As Integer
        For i = 0 To DGVICD9s.RowCount - 1
            DGVICD9s.Rows(i).Cells(1).Value = ""
        Next
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

    Private Sub txtAccessionID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAccessionID.TextChanged
        If txtAccessionID.Text <> "" Then
            btnHistory.Enabled = True
        Else
            btnHistory.Enabled = True
        End If
    End Sub

    Private Sub btnHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHistory.Click
        frmViewBARHistory.Show()
        frmViewBARHistory.MdiParent = frmDashboard
    End Sub

    Private Sub tpClient_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpClient.Leave
        If AccDirty Then
            ExecuteSqlProcedure("Update Requisitions set OrderingProvider_ID = " & Val(txtProviderID.Text) &
            " where ID = " & Val(txtAccessionID.Text))
            If lstAttending.CheckedItems.Count > 0 Then
                Dim ItemX As MyList
                ItemX = lstAttending.CheckedItems(0)
                ExecuteSqlProcedure("Update Requisitions set AttendingProvider_ID = " & ItemX.ItemData &
                " where ID = " & Val(txtAccessionID.Text))
                'If RsA IsNot Nothing Then RsA.Requery()

                FillData(MainQuery)

            Else
                MsgBox("Attending Provider needs to be checked.", MsgBoxStyle.Critical, "Prolis")
            End If
            AccDirty = False
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
        Val(txtPatientID.Text) & " where ID = " & Val(txtAccessionID.Text))
    End Sub

    Private Sub tpPatient_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpPatient.Leave
        If PatientDirty Then
            If txtPatientID.Text <> "" And txtPatLName.Text <> "" And txtPatFName.Text _
                   <> "" And IsDate(txtPatDOB.Text) And txtPatSex.Text <> "" And txtPatAdd1.Text <> "" And
                   txtPatCity.Text <> "" And txtPatState.Text <> "" And txtPatZip.Text <> "" Then _
                   ApplyPatientChanges()
            PatientDirty = False
            'If RsA IsNot Nothing AndAlso RsA.State <> 0 Then RsA.Requery()

            FillData(MainQuery)

        End If
    End Sub

    Private Sub txtBillDate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBillDate.LostFocus
        If Not IsDate(txtBillDate.Text) OrElse
        CDate(txtBillDate.Text) < CDate(txtSvcDate.Text) Then
            MsgBox("Enter a valid Bill date greater or equal to the service date, '" & txtSvcDate.Text & "'.")
            txtBillDate.Text = Format(Date.Today, SystemConfig.DateFormat)
        End If
    End Sub

    Private Sub tpCharges_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpCharges.Leave
        If DxDirty Then
            UpdateDx()
            DxDirty = False
        End If
        If ChargeDirty Then
            For i As Integer = 0 To dgvCharges.RowCount - 1
                UpdateBillable(i, dgvCharges.Rows(i).Cells(14).Value)
            Next
            ChargeDirty = False
        End If
        'RsA.Requery()
    End Sub

    Private Sub txtPatLName_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatLName.Validated
        PatientDirty = True
    End Sub

    Private Sub txtPatFName_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatFName.Validated
        PatientDirty = True
    End Sub

    Private Sub txtPatHPhone_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatHPhone.Validated
        PatientDirty = True
    End Sub

    Private Sub txtPatAdd1_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatAdd1.Validated
        PatientDirty = True
    End Sub

    Private Sub txtPatCell_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatCell.Validated
        PatientDirty = True
    End Sub

    Private Sub txtPatCity_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatCity.Validated
        PatientDirty = True
    End Sub

    Private Sub txtPatDOB_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatDOB.Validated
        PatientDirty = True
    End Sub

    Private Sub txtPatEmail_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatEmail.Validated
        PatientDirty = True
    End Sub

    Private Sub lstAttending_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstAttending.SelectedIndexChanged
        Dim i As Integer
        Dim ItemX As MyList
        For i = 0 To lstAttending.Items.Count - 1
            If lstAttending.SelectedIndex = i Then
                lstAttending.SetItemChecked(i, True)
                ItemX = lstAttending.Items(i)
                txtNPI.Text = GetProviderNPI(ItemX.ItemData)
            Else
                lstAttending.SetItemChecked(i, False)
            End If
        Next
        AccDirty = True
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

    Private Sub txtPatientID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatientID.Validated
        If txtPatientID.Text <> "" Then
            DisplayPatient(Val(txtPatientID.Text))
        Else
            ClearPatient()
        End If
        PatientDirty = True
    End Sub

    Private Sub txtProviderID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProviderID.Validated
        If txtProviderID.Text <> "" Then
            DisplayClient(Val(txtProviderID.Text))
        Else
            ClearClient()
        End If
        AccDirty = True
    End Sub

    Private Sub chkBillNow_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBillNow.CheckedChanged
        Dim chks As Integer = 0
        If chkBillNow.Checked = True Then
            If dgvCharges.RowCount > 0 Then
                For i As Integer = 0 To dgvCharges.RowCount - 1
                    If dgvCharges.Rows(i).Cells(14).Value = "U" Then
                        dgvCharges.Rows(i).Cells(15).Value = "1"
                        chks += 1
                    End If
                Next
            End If
            'Ch_Bill = True
            If chkRev.Checked = True Then chkRev.Checked = False
        Else
            For i As Integer = 0 To dgvCharges.RowCount - 1
                If dgvCharges.Rows(i).Cells(14).Value = "U" Then
                    dgvCharges.Rows(i).Cells(15).Value = "0"
                End If
            Next
        End If
        BillingProgress(chks)
    End Sub

    Private Sub chkRev_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRev.CheckedChanged
        Dim unchks As Integer = 0
        If chkRev.Checked = True Then
            If dgvCharges.RowCount > 0 Then
                For i As Integer = 0 To dgvCharges.RowCount - 1
                    If dgvCharges.Rows(i).Cells(14).Value = "B" Then
                        dgvCharges.Rows(i).Cells(15).Value = "1"
                        unchks += 1
                    End If
                Next
            End If
            'Ch_Reverse = True
            If chkBillNow.Checked = True Then chkBillNow.Checked = False
        Else
            For i As Integer = 0 To dgvCharges.RowCount - 1
                If dgvCharges.Rows(i).Cells(14).Value = "B" Then
                    dgvCharges.Rows(i).Cells(15).Value = "0"
                End If
            Next
        End If
        ReversingProgress(unchks)
    End Sub

    Private Sub ReversingProgress(ByVal unchks As Integer)
        Dim Lines As Integer = 0
        For i As Integer = 0 To dgvCharges.RowCount - 1
            If dgvCharges.Rows(i).Cells(3).Value IsNot Nothing _
            AndAlso dgvCharges.Rows(i).Cells(3).Value <> "" Then
                Lines += 1
            End If
        Next
        If Lines > 0 And unchks > 0 Then
            btnBillRev.Enabled = True
        Else
            btnBillRev.Enabled = False
        End If
    End Sub

    Private Sub BillingProgress(ByVal chks As Integer)
        Dim Lines As Integer = 0
        For i As Integer = 0 To dgvCharges.RowCount - 1
            If dgvCharges.Rows(i).Cells(3).Value IsNot Nothing _
            AndAlso dgvCharges.Rows(i).Cells(3).Value <> "" Then
                Lines += 1
            End If
        Next
        If Lines > 0 And chks > 0 Then
            btnBillRev.Enabled = True
        Else
            btnBillRev.Enabled = False
        End If
    End Sub

    Private Sub DGVICD9s_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVICD9s.CellContentClick
        If e.ColumnIndex = 2 Then
            If e.RowIndex <> -1 Then
                TCode = DGVICD9s.Rows(e.RowIndex).Cells(1).Value
                TCode = frmDiagnosis.ShowDialog
                If TCode <> "" Then
                    DGVICD9s.Rows(e.RowIndex).Cells(1).Value = TCode
                    TCode = ""
                End If
            End If
        End If
    End Sub

    Private Sub chkAI_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAI.CheckedChanged
        If chkAI.Checked = False Then
            chkAI.Text = "ACCESSION"
            Label4.Text = "Accession From"
            Label5.Text = "Accession To"
        Else
            chkAI.Text = "INVOICE"
            Label4.Text = "Invoice From"
            Label5.Text = "Invoice To"
        End If
    End Sub

    'Private Sub btnSelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelAll.Click
    '    If dgvCharges.RowCount > 0 Then
    '        Dim i As Integer
    '        For i = 0 To dgvCharges.RowCount - 1
    '            If dgvCharges.Rows(i).Cells(14).Value = "U" Or _
    '            dgvCharges.Rows(i).Cells(14).Value = "B" Then
    '                dgvCharges.Rows(i).Cells(15).Value = True
    '            End If
    '        Next
    '    End If
    'End Sub

    Private Sub btnDeSelAll_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If dgvCharges.RowCount > 0 Then
            Dim i As Integer
            For i = 0 To dgvCharges.RowCount - 1
                dgvCharges.Rows(i).Cells(15).Value = False
            Next
        End If
    End Sub

    'Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
    '    If TabControl1.SelectedIndex = 3 Then
    '        If dgvCharges.Rows(0).Cells(1).Value IsNot Nothing _
    '        AndAlso dgvCharges.Rows(0).Cells(1).Value.ToString <> "" Then
    '            btnSelAll.Enabled = True
    '            btnDeSelAll.Enabled = True
    '        End If
    '    Else
    '        btnSelAll.Enabled = False
    '        btnDeSelAll.Enabled = False
    '    End If
    'End Sub

    Private Sub txtInsuredID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInsuredID.Validated
        PayerDirty = True
    End Sub

    Private Sub frmBillingEdit_ResizeBegin(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeBegin
        origWidth = Me.Width
        origHeight = Me.Height
    End Sub

    Private Sub frmBillingEdit_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        MeResize(Me, origWidth, origHeight)
    End Sub

    Private Sub txtAccTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccTo.Validated
        If txtAccTo.Text <> "" Then
            'txtDateFrom.Text = ""
            'txtDateTo.Text = ""

            ClearDateTimePicker(dtpDateFrom)
            ClearDateTimePicker(dtpDateTo)
            dgvDiscrete.Rows.Clear()
            dgvDiscrete.RowCount = 1
        End If
    End Sub

    Private Sub chkECC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkECC.CheckedChanged
        'If chkECC.Checked = True Then  'default paper
        '    chkECC.Text = "837"
        'Else
        '    chkECC.Text = "Paper"
        'End If
    End Sub

    Private Sub dgvCharges_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCharges.RowEnter
        If dgvCharges.Rows(e.RowIndex).Cells(14).Value = "P" _
        Or dgvCharges.Rows(e.RowIndex).Cells(14).Value = "B" Then
            DisableChargeLine(e.RowIndex)
        Else
            EnableChargeLine(e.RowIndex)
        End If
    End Sub

    Private Sub dgvComments_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvComments.CellEndEdit
        CommentDirty = True
    End Sub


    Private Sub cmbBillType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBillType.Click
        If cmbBillType.SelectedIndex = 0 Then   'Client
            chkClientBill.Checked = True
            chkTPBill.Checked = False
            chkPatientBill.Checked = False
        ElseIf cmbBillType.SelectedIndex = 1 Then   'Third Party
            chkClientBill.Checked = False
            chkTPBill.Checked = True
            If Val(txtCopay.Text) > 0 Then
                chkPatientBill.Checked = True
            Else
                chkPatientBill.Checked = False
            End If
        Else
            chkClientBill.Checked = False
            chkTPBill.Checked = False
            chkPatientBill.Checked = True
        End If
        Ch_Acc = True
        Ch_Lines = True
        BillingEditProgress()
    End Sub

    Private Sub dgvDiscrete_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDiscrete.CellEndEdit
        If Trim(dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) <> "" Then
            If IsNumeric(Trim(dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)) = False Then
                MsgBox("Only digits are allowed.")
                dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
            Else
                If IsDuplicate(Trim(dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)) Then
                    MsgBox("Duplicate Entry is not allowed.")
                    dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
                End If
            End If
            If e.RowIndex = dgvDiscrete.RowCount - 1 Then
                dgvDiscrete.Rows.Add()
                SendKeys.Send("{ENTER}")
            End If
            '
            txtAccFrom.Text = ""
            txtAccTo.Text = ""
            'txtDateFrom.Text = ""
            'txtDateTo.Text = ""
            ClearDateTimePicker(dtpDateFrom)
            ClearDateTimePicker(dtpDateTo)
        Else
            If e.RowIndex < dgvDiscrete.RowCount - 1 Then
                dgvDiscrete.Rows.RemoveAt(e.RowIndex)
            End If
        End If
    End Sub

    Private Function IsDuplicate(ByVal AccID As Long) As Boolean
        Dim i As Integer
        Dim CT As Integer = 0
        For i = 0 To dgvDiscrete.RowCount - 1
            If dgvDiscrete.Rows(i).Cells(0).Value = AccID.ToString Then CT += 1
        Next
        If CT > 1 Then
            IsDuplicate = True
        Else
            IsDuplicate = False
        End If
    End Function

    Private Sub dgvDiscrete_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvDiscrete.CellMouseUp
        If e.Button = MouseButtons.Right Then
            If Clipboard.ContainsText Then
                dgvDiscrete.Rows.Clear()
                Dim Accs() As String = Split(Clipboard.GetText, vbCrLf)
                For i As Integer = 0 To Accs.Length - 1
                    If Trim(Accs(i)) <> "" Then
                        If dgvDiscrete.RowCount = 0 Then dgvDiscrete.Rows.Add()
                        If dgvDiscrete.Rows(dgvDiscrete.RowCount - 1).Cells(0).Value _
                        = "" Then
                            dgvDiscrete.Rows(dgvDiscrete.RowCount - 1).Cells(0).Value = Trim(Accs(i))
                        Else
                            dgvDiscrete.Rows.Add()
                            dgvDiscrete.Rows(dgvDiscrete.RowCount - 1).Cells(0).Value = Trim(Accs(i))
                        End If
                    End If
                Next
                txtAccFrom.Text = ""
                txtAccTo.Text = ""
                'txtDateFrom.Text = ""
                'txtDateTo.Text = ""

                ClearDateTimePicker(dtpDateFrom)
                ClearDateTimePicker(dtpDateTo)
                dgvDiscrete.Rows.Add()
            End If
        End If
    End Sub

    Private Sub btnBillRev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBillRev.Click
        btnSave_Click(Nothing, Nothing)
    End Sub

    Private Sub cmbPatPrice_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPatPrice.SelectedIndexChanged
        If cmbBillType.SelectedIndex = 2 Then
            Dim PriceLevel As Integer = cmbPatPrice.SelectedIndex
            For i As Integer = 0 To dgvCharges.RowCount - 1
                If dgvCharges.Rows(i).Cells(1).Value IsNot Nothing _
                AndAlso dgvCharges.Rows(i).Cells(1).Value.ToString <> "" Then
                    dgvCharges.Rows(i).Cells(10).Value =
                    GetLevelPrice(dgvCharges.Rows(i).Cells(1).Value, PriceLevel)
                End If
            Next
        End If
        Ch_TP = True
        BillingEditProgress()
    End Sub

    Private Function GetLevelPrice(ByVal TGPID As Integer, ByVal Level As Integer) As Single
        Dim Price As Single = 0
        Dim Field As String = "ListPrice"
        If Level = 1 Then
            Field = "Price1"
        ElseIf Level = 2 Then
            Field = "Price2"
        ElseIf Level = 3 Then
            Field = "Price3"
        ElseIf Level = 4 Then
            Field = "Price4"
        ElseIf Level = 5 Then
            Field = "Price5"
        ElseIf Level = 6 Then
            Field = "Price6"
        ElseIf Level = 7 Then
            Field = "Price7"
        ElseIf Level = 8 Then
            Field = "Price8"
        Else
            Field = "Price9"
        End If
        Dim TGPType As String = GetTGPType(TGPID)
        Dim sSQL As String = ""
        sSQL = "Select " & Field & " from Tests where ID = " & TGPID &
        " Union Select " & Field & " from Groups where ID = " & TGPID &
        " Union Select " & Field & " from Profiles where ID = " & TGPID
        Dim cnpr As New SqlConnection(connString)
        cnpr.Open()
        Dim cmdpr As New SqlCommand(sSQL, cnpr)
        cmdpr.CommandType = CommandType.Text
        Dim drpr As SqlDataReader = cmdpr.ExecuteReader
        If drpr.HasRows Then
            While drpr.Read
                Price = drpr(Field)
            End While
        End If
        cnpr.Close()
        cnpr = Nothing
        Return Price
    End Function

    Private Sub cmbProvPrice_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProvPrice.SelectedIndexChanged
        If cmbBillType.SelectedIndex = 0 Then
            Dim PriceLevel As Integer = cmbProvPrice.SelectedIndex
            For i As Integer = 0 To dgvCharges.RowCount - 1
                If dgvCharges.Rows(i).Cells(1).Value IsNot Nothing _
                AndAlso dgvCharges.Rows(i).Cells(1).Value.ToString <> "" Then
                    dgvCharges.Rows(i).Cells(10).Value =
                    GetLevelPrice(dgvCharges.Rows(i).Cells(1).Value, PriceLevel)
                End If
            Next
        End If
        Ch_TP = True
        BillingEditProgress()
    End Sub

    Private Sub chkECC_Click(sender As Object, e As EventArgs) Handles chkECC.Click
        If chkECC.Checked Then
            chkECC.Text = "837"
        ElseIf Not chkECC.Checked Then
            chkECC.Text = "Paper"
        Else
            chkECC.Checked = True
            chkECC.Text = "837"
        End If
    End Sub

    Private Sub btnSIns_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub cmbSIns_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSIns.SelectedIndexChanged
        Ch_TP = True : SPayerDirty = True
        BillingEditProgress()
    End Sub

    Private Sub txtSPolicy_Validated(sender As Object, e As EventArgs) Handles txtSPolicy.Validated

        Ch_TP = True : SPayerDirty = True
        BillingEditProgress()
    End Sub

    Private Sub txtSGroup_Validated(sender As Object, e As EventArgs) Handles txtSGroup.Validated
        Ch_TP = True : SPayerDirty = True
        BillingEditProgress()
    End Sub

    Private Sub cmbSRelation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSRelation.SelectedIndexChanged
        Ch_TP = True : SPayerDirty = True
        BillingEditProgress()
    End Sub

    Private Sub txtSCopay_Validated(sender As Object, e As EventArgs) Handles txtSCopay.Validated
        Ch_TP = True : SPayerDirty = True
        BillingEditProgress()
    End Sub

    Private Sub conractS_CheckedChanged(sender As Object, e As EventArgs) Handles conractS.CheckedChanged
        'Ch_TP = conractS.Checked
        'Dim AccID = Val(txtAccessionID.Text)

        'Dim PayerIDS = 0
        'Dim PayerIDP = 0
        'If cmbPayer.SelectedIndex <> -1 Then
        '    Dim ItemX As MyList = cmbPayer.SelectedItem
        '    PayerIDP = ItemX.ItemData
        'End If
        'If cmbSIns.SelectedIndex <> -1 Then
        '    Dim ItemX As MyList = cmbSIns.SelectedItem
        '    PayerIDS = ItemX.ItemData
        'End If
        'Dim pt = Val(txtPatientID.Text)
        'If conractS.Checked Then
        '    ExecuteSqlProcedure("if Exists ( select * from Req_Coverage where Insured_ID =  " & pt & " and Accession_ID =" & AccID & " and Payer_ID =" & PayerIDS & ")  begin update Req_Coverage set UseForBill=0 where Insured_ID =  " & pt & " and Accession_ID = " & AccID & ";  update Req_Coverage set UseForBill=1 where Insured_ID =  " & pt & " and Accession_ID =" & AccID & " and Payer_ID =" & PayerIDS & ";  End")
        'Else
        '    ExecuteSqlProcedure("if Exists (select * from Req_Coverage where Insured_ID =  " & pt & " and Accession_ID =" & AccID & " and Payer_ID =" & PayerIDP & ")   begin   update Req_Coverage set UseForBill=0 where Insured_ID =  " & pt & " and Accession_ID =" & AccID & ";   update Req_Coverage set UseForBill=1 where Insured_ID =" & pt & "  and Accession_ID =" & AccID & " and Payer_ID =" & PayerIDP & ";   End")
        'End If
    End Sub



    Private Sub Corrected_CheckedChanged_1(sender As Object, e As EventArgs) Handles Corrected.CheckedChanged
        If Corrected.Checked Then
            VoidClaim.Checked = False
            If orgChargID.Text = "" Then
                orgChargID.Focus()
            End If

        End If
    End Sub

    Private Sub VoidClaim_CheckedChanged_1(sender As Object, e As EventArgs) Handles VoidClaim.CheckedChanged
        If VoidClaim.Checked Then
            If orgChargID.Text = "" Then
                orgChargID.Focus()
            End If
            Corrected.Checked = False

        End If
    End Sub

    Private Sub btnEECC_Click(sender As Object, e As EventArgs) Handles btnEECC.Click
        If txtAccessionID.Text = "" Then
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
            Dim q As String = "select Msg  from EBMessages  where Accession_ID = " & txtAccessionID.Text & " order by MsgDate desc"

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


            Else



                'Dim CNS As New ADODB.Connection
                'CNS.Open(connstring)
                'Dim ck As CheckEligibility = New CheckEligibility()
                'Dim result = ck.Eligibility(txtAccessionID.Text, CNS, connstring)
                'CNS.Close()
                Dim result
                Using CNS As New SqlConnection(connString)
                    CNS.Open()
                    Dim ck As CheckEligibility = New CheckEligibility()
                    result = ck.Eligibility(txtAccessionID.Text)
                End Using

                If result.Contains("Active Coverage Health") Then
                    EECC = True
                    lblEligibility.BackColor = Color.PaleGreen
                    lblEligibility.PerformClick()

                Else
                    lblEligibility.BackColor = Color.PeachPuff
                End If
                lblEligibility.BackColor = Color.PeachPuff
            End If
        End If
        loading.Hide()
        Cursor.Current = Cursors.Default
        checkelig()
    End Sub

    Private Sub lblEligibility_Click(sender As Object, e As EventArgs) Handles lblEligibility.Click
        If txtAccessionID.Text = "" Then
            Return
        End If
        Dim q As String = "select top 1 xmlResponse  from EBMessages  where Accession_ID = " & txtAccessionID.Text & " order by MsgDate desc"

        Dim meds = CommonData.ExecuteQuery(q)
        Dim med As String = ""
        For Each row In meds
            med = row("xmlResponse")
        Next
        'EligDetails.PatientName.Text = "Patient Name: " + txtPatLName.Text + " " + txtPatFName.Text
        'EligDetails.AccID.Text = "Accession: " + txtAccessionID.Text
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
    End Sub

    Public Sub lblClearDates_Click1(sender As Object, e As EventArgs) Handles lblClearDates.Click
        ClearDateTimePicker(dtpDateFrom)
        ClearDateTimePicker(dtpDateTo)
    End Sub
    Private Sub lblClearDates_Click(sender As Object, e As EventArgs) Handles lblClearDates.Click
        ClearDateTimePicker(dtpDateFrom)
        ClearDateTimePicker(dtpDateTo)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        frmPatient.Close()
        frmPatient.Show()
        frmPatient.Activate()
        frmPatient.chkNewEdit.PerformClick()
        frmPatient.txtPatientID.Text = txtPatientID.Text
        frmPatient.txtPatientID.Focus()
    End Sub
End Class