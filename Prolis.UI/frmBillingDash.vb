Imports System.Data
Imports System.IO
'''spire
Imports System.Security.AccessControl
Imports System.Globalization
Imports Microsoft.Data.SqlClient

Public Class frmBillingDash
    Private origWidth As Integer
    Private origHeight As Integer
    Private connStringS() As String = {connString, connString}

    Private Sub frmBillingDash_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If BW.IsBusy Then
            e.Cancel = True
        End If
    End Sub

    Private Sub frmBillingDash_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MaximumSize = MaxSize
        lblFrom.Text += " (" & SystemConfig.DateFormat & ")"
        lblTo.Text += " (" & SystemConfig.DateFormat & ")"
        dgvDiscrete.RowCount = 1
        cmbBillType.SelectedIndex = 1
        'txtDateFrom.Text = Format(Date.Now.AddDays(-1), SystemConfig.DateFormat)
        'txtDateTo.Text = Format(Date.Now, SystemConfig.DateFormat)

        ConfigureDateTimePicker(dtpDateFrom)
        ConfigureDateTimePicker(dtpDateTo)
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
        clipboardMsg.Text = ""
    End Sub

    Private Sub DisableActions()
        btnLoad.Enabled = False
    End Sub

    Private Sub EnableActions()
        btnLoad.Enabled = True
    End Sub

    Private Sub ClearForm()
        dgvUnsynchAccs.Rows.Clear()
        txtOutput.Text = ""
        btnSave.Enabled = False
        btnSynchronize.Enabled = False
    End Sub


    Private Sub btnLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        DisableActions()
        txtOutput.Text = ""
        Dim sSQL As String = ""
        Dim ItemX As MyList
        Dim Billis As String = ""
        Dim Billee As String = ""
        dgvUnsynchAccs.Rows.Clear()
        dgvSynchAccs.Rows.Clear()
        dgvBilled.Rows.Clear()
        dgvProcTPs.Rows.Clear()
        dgvProcPats.Rows.Clear()
        lblStatus.Text = ""
        PB.Value = 0
        My.Application.DoEvents()
        If ((IsDate(dtpDateFrom.Text) Or IsDate(dtpDateTo.Text)) OrElse (txtAccFrom.Text <> "" Or
        txtAccTo.Text <> "") OrElse HasDiscretes() And lstTargets.CheckedItems.Count > 0) Then
            If TB.SelectedTab Is TB.TabPages(0) Then    'Unsynch
                sSQL = "Select 1 as selected, a.ID as AccID, b.LastName + ', ' + b.FirstName as [Patient (L, F)], " &
                "Convert(nvarchar, a.ReceivedTime, 101) as [Rec Date], Convert(nvarchar, a.Reported_Final, 101) " &
                "as [Rpt Date], c.IsIndividual, c.LastName_BSN, c.FirstName, c.Degree, c.ID as ProvID from Providers " &
                "c inner join (Requisitions a inner join Patients b on b.ID = a.Patient_ID) on a.OrderingProvider_ID " &
                "= c.ID where a.Rejected = 0"
                'Base Criteria
                'If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                '    sSQL += " and a.ReceivedTime between '" & dtpDateFrom.Text & "' and '" & dtpDateFrom.Text & " 23:59:00'"
                'ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                '    sSQL += " and a.ReceivedTime between '" & dtpDateTo.Text & "' and '" & dtpDateTo.Text & " 23:59:00'"
                'ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                '    sSQL += " and a.ReceivedTime between '" & dtpDateFrom.Text & "' and '" & dtpDateTo.Text & " 23:59:00'"
                'ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                '    sSQL += " and a.ID = " & txtAccFrom.Text
                'ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                '    sSQL += " and a.ID = " & txtAccTo.Text
                'ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                '    sSQL += " and a.ID between " & txtAccFrom.Text & " and " & txtAccTo.Text
                'ElseIf HasDiscretes() Then
                '    Dim accs As String = ""
                '    For i As Integer = 0 To dgvDiscrete.RowCount - 1
                '        If dgvDiscrete.Rows(i).Cells(0).Value IsNot Nothing _
                '        AndAlso dgvDiscrete.Rows(i).Cells(0).Value <> "" Then
                '            accs += dgvDiscrete.Rows(i).Cells(0).Value & ", "
                '        End If
                '    Next
                '    If accs.EndsWith(", ") Then accs = accs.Substring(0, Len(accs) - 2)
                '    If accs <> "" Then sSQL += " and a.ID in (" & accs & ")"
                'End If
                '
                If cmbBillType.SelectedIndex = 0 Then   'client
                    sSQL += " and a.BillingType_ID = 0"
                    For i As Integer = 0 To lstTargets.Items.Count - 1
                        If lstTargets.GetItemChecked(i) = True Then
                            ItemX = lstTargets.Items(i)
                            Billis += ItemX.ItemData.ToString & ", "
                        End If
                    Next
                    If Billis.EndsWith(", ") Then Billis = Billis.Substring(0, Len(Billis) - 2)
                    sSQL += " and a.OrderingProvider_ID in (" & Billis & ")"
                ElseIf cmbBillType.SelectedIndex = 1 Then   'TP
                    sSQL += " and a.BillingType_ID = 1"
                    For i As Integer = 0 To lstTargets.Items.Count - 1
                        If lstTargets.GetItemChecked(i) = True Then
                            ItemX = lstTargets.Items(i)
                            Billis += ItemX.ItemData.ToString & ", "
                        End If
                    Next
                    If Billis.EndsWith(", ") Then Billis = Billis.Substring(0, Len(Billis) - 2)
                    sSQL += " and a.ID in (Select distinct Accession_ID from Req_Coverage " &
                    "where Preference = 'P' and Payer_ID in (" & Billis & "))"
                Else    'Patient
                    sSQL += " and a.BillingType_ID = 2"
                    For i As Integer = 0 To lstTargets.Items.Count - 1
                        If lstTargets.GetItemChecked(i) = True Then
                            ItemX = lstTargets.Items(i)
                            Billis += ItemX.ItemData.ToString & ", "
                        End If
                    Next
                    If Billis.EndsWith(", ") Then Billis = Billis.Substring(0, Len(Billis) - 2)
                    sSQL += " and a.Patient_ID in (" & Billis & ")"
                End If
                sSQL += " and not a.ID in (Select distinct Accession_ID from Req_Billable)"
            ElseIf TB.SelectedTab Is TB.TabPages(1) Then    'Unbilled or Synched
                sSQL = "Select 1 as selected, a.ID as AccID, b.LastName + ', ' + b.FirstName as [Patient (L, F)], " &
               "Convert(nvarchar, (Select Min(SourceDate) from Specimens where Accession_ID = a.ID), 101) as " &
               "[Svc Date], (Select Sum(Extend) from Req_Billable where Accession_ID = a.ID) as [Amount]"

                If cmbBillType.SelectedIndex = 0 Then   'client
                    sSQL += ", c.IsIndividual, c.LastName_BSN, c.FirstName, c.Degree, c.ID as ProvID from " &
                    "Providers c inner join (Requisitions a inner join Patients b on b.ID = a.Patient_ID) " &
                    "on a.OrderingProvider_ID = c.ID where a.BillingType_ID = 0 and a.Rejected = 0"
                    '
                    For i As Integer = 0 To lstTargets.Items.Count - 1
                        If lstTargets.GetItemChecked(i) = True Then
                            ItemX = lstTargets.Items(i)
                            Billis += ItemX.ItemData.ToString & ", "
                        End If
                    Next
                    If Billis.EndsWith(", ") Then Billis = Billis.Substring(0, Len(Billis) - 2)
                    sSQL += " and a.OrderingProvider_ID in (" & Billis & ")"
                ElseIf cmbBillType.SelectedIndex = 1 Then   'TP
                    sSQL += ", c.PayerName, c.ID as PayerID from Payers c inner join (Requisitions a inner join " &
                    "Patients b on b.ID = a.Patient_ID) on a.PrimePayer_ID = c.ID  where a.BillingType_ID = 1 " &
                    " and a.Rejected = 0"
                    For i As Integer = 0 To lstTargets.Items.Count - 1
                        If lstTargets.GetItemChecked(i) = True Then
                            ItemX = lstTargets.Items(i)
                            Billis += ItemX.ItemData.ToString & ", "
                        End If
                    Next
                    If Billis.EndsWith(", ") Then Billis = Billis.Substring(0, Len(Billis) - 2)
                    sSQL += " and a.ID in (Select distinct Accession_ID from Req_Coverage " &
                    "where Preference = 'P' and Payer_ID in (" & Billis & "))"
                Else    'Patient
                    sSQL += ", b.ID as PatientID from Requisitions a inner join Patients b on " &
                    "b.ID = a.Patient_ID where a.BillingType_ID = 2 and a.Rejected = 0"
                    For i As Integer = 0 To lstTargets.Items.Count - 1
                        If lstTargets.GetItemChecked(i) = True Then
                            ItemX = lstTargets.Items(i)
                            Billis += ItemX.ItemData.ToString & ", "
                        End If
                    Next
                    If Billis.EndsWith(", ") Then Billis = Billis.Substring(0, Len(Billis) - 2)
                    sSQL += " and a.Patient_ID in (" & Billis & ")"
                End If
                sSQL += " and a.ID in (Select distinct Accession_ID from Req_Billable) and " &
                "not a.ID in (Select distinct Accession_ID from Charges)"
            ElseIf TB.SelectedTab Is TB.TabPages(2) Then    'Billed
                sSQL = "Select 1 as selected, d.ID as InvID, a.ID as AccID, b.LastName + ', ' + b.FirstName as " &
                "[Patient (L, F)], Isnull(convert(nvarchar, d.Svc_Date, 101), '') as [Svc Date], IsNull(Convert(nvarchar, " &
                "d.Bill_Date, 101), '') as [Bill Date], IsNull(Round(d.GrossAmount - IsNull((Select Sum(AppliedAmount + " &
                "WrittenOff) from Payment_Detail where Charge_ID = d.ID), 0), 2), 0) as [Amount]"
                If cmbBillType.SelectedIndex = 0 Then   'client
                    sSQL += ", c.IsIndividual, c.LastName_BSN, c.FirstName, c.Degree, c.ID as ProvID, IsNull(c.Email, '') " &
                    "as Email from Providers c inner join (Charges d inner join (Requisitions a inner join Patients b on " &
                    "b.ID = a.Patient_ID) on a.ID = d.Accession_ID) on a.OrderingProvider_ID = c.ID where d.ArType = 0"
                    For i As Integer = 0 To lstTargets.Items.Count - 1
                        If lstTargets.GetItemChecked(i) = True Then
                            ItemX = lstTargets.Items(i)
                            Billis += ItemX.ItemData.ToString & ", "
                        End If
                    Next
                    If Billis.EndsWith(", ") Then Billis = Billis.Substring(0, Len(Billis) - 2)
                    sSQL += " and a.OrderingProvider_ID in (" & Billis & ")"
                    '
                    If chkNZero.Checked Then
                        sSQL += " and a.ID in (Select a.Accession_ID from Charges a where Round(a.GrossAmount - " &
                        "IsNull((Select Sum(AppliedAmount + WrittenOff) from Payment_Detail where Charge_ID = a.ID), 0), 2) > 0)"
                    End If
                ElseIf cmbBillType.SelectedIndex = 1 Then   'TP
                    sSQL += ", c.PayerName, c.ID as PayerID, '' as Email from Payers c inner join (Charges d " &
                    "inner join (Requisitions a inner join Patients b on b.ID = a.Patient_ID) on a.ID = " &
                    "d.Accession_ID) on a.PrimePayer_ID = c.ID where d.ArType = 1 and d.IsPrimary <> 0"
                    For i As Integer = 0 To lstTargets.Items.Count - 1
                        If lstTargets.GetItemChecked(i) = True Then
                            ItemX = lstTargets.Items(i)
                            Billis += ItemX.ItemData.ToString & ", "
                        End If
                    Next
                    If Billis.EndsWith(", ") Then Billis = Billis.Substring(0, Len(Billis) - 2)
                    sSQL += " and a.ID in (Select distinct Accession_ID from Req_Coverage " &
                    "where Preference = 'P' and Payer_ID in (" & Billis & "))"
                    If chkFileless.Checked = True Then  'Unprocessed
                        sSQL += " and d.Output = 0"
                        'sSQL += " and not a.ID in (Select a.Accession_ID from Charges a inner join " & _
                        '"ChargesEOutput b on b.Charge_ID = a.ID where a.Ar_ID in (" & Billis & "))"
                    End If
                    If chkNZero.Checked Then
                        sSQL += " and not d.ID in (Select distinct Charge_ID from Payment_Detail)"
                    End If
                Else    'Patient
                    sSQL += ", b.ID as PatientID, IsNull(b.Email, '') as Email from Charges d inner join (Requisitions " &
                    "a inner join Patients b on b.ID = a.Patient_ID) on d.Accession_ID = a.ID where d.ArType = 2"
                    For i As Integer = 0 To lstTargets.Items.Count - 1
                        If lstTargets.GetItemChecked(i) = True Then
                            ItemX = lstTargets.Items(i)
                            Billis += ItemX.ItemData.ToString & ", "
                        End If
                    Next
                    If Billis.EndsWith(", ") Then Billis = Billis.Substring(0, Len(Billis) - 2)
                    sSQL += " and a.Patient_ID in (" & Billis & ")"
                    '
                    If chkNZero.Checked Then
                        sSQL += " and a.ID in (Select a.Accession_ID from Charges a where Round(a.GrossAmount - " &
                        "IsNull((Select Sum(AppliedAmount + WrittenOff) from Payment_Detail where Charge_ID = a.ID), 0), 2) > 0)"
                    End If
                    If chkFileless.Checked = True Then  'Unprocessed
                        sSQL += " and not a.ID in (Select a.Accession_ID from Charges a inner join ChargePrints b " &
                        "on b.Charge_ID = a.ID where a.Ar_ID in (" & Billis & ") and b.Print_Count >= 4)"
                    End If
                End If
                'sSQL += " and a.ID in (Select distinct Accession_ID from Req_Billable) and " & _
                '"not a.ID in (Select distinct Accession_ID from Charges)"
                'Base Criteria
                'If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                '    sSQL += " and a.ReceivedTime between '" & dtpDateFrom.Text & "' and '" & dtpDateFrom.Text & " 23:59:00'"
                'ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                '    sSQL += " and a.ReceivedTime between '" & dtpDateTo.Text & "' and '" & dtpDateTo.Text & " 23:59:00'"
                'ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                '    sSQL += " and a.ReceivedTime between '" & dtpDateFrom.Text & "' and '" & dtpDateTo.Text & " 23:59:00'"
                'ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                '    sSQL += " and a.ID = " & txtAccFrom.Text
                'ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                '    sSQL += " and a.ID = " & txtAccTo.Text
                'ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                '    sSQL += " and a.ID between " & txtAccFrom.Text & " and " & txtAccTo.Text
                'ElseIf HasDiscretes() Then
                '    Dim accs As String = ""
                '    For i As Integer = 0 To dgvDiscrete.RowCount - 1
                '        If dgvDiscrete.Rows(i).Cells(0).Value IsNot Nothing _
                '        AndAlso dgvDiscrete.Rows(i).Cells(0).Value <> "" Then
                '            accs += dgvDiscrete.Rows(i).Cells(0).Value & ", "
                '        End If
                '    Next
                '    If accs.EndsWith(", ") Then accs = accs.Substring(0, Len(accs) - 2)
                '    If accs <> "" Then sSQL += " and a.ID in (" & accs & ")"
                'End If
                '
            Else    'Processed Tab
                If cmbBillType.SelectedIndex = 1 Then   'Insurance
                    sSQL = "Select distinct 1 as selected, a.FileNo, (Select Name from Partners where ID = a.Partner_ID) " &
                    "as [Clearing House], convert(nvarchar, a.Dated, 101) as [Created on], (Select Count(Charge_ID) from " &
                    "ChargesEOutput where FileNo = a.FileNo) as Invoices, (Select Sum(Amount) from ChargesEOutput where " &
                    "FileNo = a.FileNo) as [File Amount] from ChargesEOutput a"
                    If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                        sSQL += " where a.Dated between '" & dtpDateFrom.Text &
                        "' and '" & dtpDateFrom.Text & " 23:59:00'"
                    ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                        sSQL += " where a.Dated between '" & dtpDateTo.Text &
                        "' and '" & dtpDateTo.Text & " 23:59:00'"
                    ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                        sSQL += " where a.Dated between '" & dtpDateFrom.Text &
                        "' and '" & dtpDateTo.Text & " 23:59:00'"
                    End If
                    '
                    For i As Integer = 0 To lstTargets.Items.Count - 1
                        If lstTargets.GetItemChecked(i) = True Then
                            ItemX = lstTargets.Items(i)
                            Billis += ItemX.ItemData.ToString & ", "
                        End If
                    Next
                    If Billis.EndsWith(", ") Then Billis = Billis.Substring(0, Len(Billis) - 2)
                    sSQL += " and a.Payer_ID in (" & Billis & ")"
                ElseIf cmbBillType.SelectedIndex = 2 Then   'Patient
                    sSQL = "Select 1 as selected, d.ID as InvID, a.ID as AccID, b.LastName + ', ' + b.FirstName as " &
                    "[Patient (L, F)], Isnull(convert(nvarchar, d.Svc_Date, 101), '') as [Svc Date], IsNull(Convert(nvarchar, " &
                    "d.Bill_Date, 101), '') as [Bill Date], IsNull(d.GrossAmount, 0) as [Amount], IsNull((Select Print_Count " &
                    "from ChargePrints where Charge_ID = d.ID), 0) as Prints, IsNull(b.Email, '') as Email from Charges d " &
                    "inner join (Requisitions a inner join Patients b on b.ID = a.Patient_ID) on d.Accession_ID = a.ID where " &
                    "d.ArType = 2 and d.ID in (Select Charge_ID from ChargePrints where Print_Count >= 3)"
                    ''Base Criteria
                    'If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                    '    sSQL += " and a.ReceivedTime between '" & dtpDateFrom.Text & "' and '" & dtpDateFrom.Text & " 23:59:00'"
                    'ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                    '    sSQL += " and a.ReceivedTime between '" & dtpDateTo.Text & "' and '" & dtpDateTo.Text & " 23:59:00'"
                    'ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                    '    sSQL += " and a.ReceivedTime between '" & dtpDateFrom.Text & "' and '" & dtpDateTo.Text & " 23:59:00'"
                    'ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                    '    sSQL += " and a.ID = " & txtAccFrom.Text
                    'ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                    '    sSQL += " and a.ID = " & txtAccTo.Text
                    'ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                    '    sSQL += " and a.ID between " & txtAccFrom.Text & " and " & txtAccTo.Text
                    'ElseIf HasDiscretes() Then
                    '    Dim accs As String = ""
                    '    For i As Integer = 0 To dgvDiscrete.RowCount - 1
                    '        If dgvDiscrete.Rows(i).Cells(0).Value IsNot Nothing _
                    '        AndAlso dgvDiscrete.Rows(i).Cells(0).Value <> "" Then
                    '            accs += dgvDiscrete.Rows(i).Cells(0).Value & ", "
                    '        End If
                    '    Next
                    '    If accs.EndsWith(", ") Then accs = accs.Substring(0, Len(accs) - 2)
                    '    If accs <> "" Then sSQL += " and a.ID in (" & accs & ")"
                    'End If
                    ''
                    For i As Integer = 0 To lstTargets.Items.Count - 1
                        If lstTargets.GetItemChecked(i) = True Then
                            ItemX = lstTargets.Items(i)
                            Billis += ItemX.ItemData.ToString & ", "
                        End If
                    Next
                    If Billis.EndsWith(", ") Then Billis = Billis.Substring(0, Len(Billis) - 2)
                    sSQL += " and a.Patient_ID in (" & Billis & ")"
                End If

            End If
            '
            If Not TB.SelectedTab Is TB.TabPages(3) Then    'processed
                If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                    sSQL += " and a.ReceivedTime between '" & dtpDateFrom.Text &
                    "' and '" & dtpDateFrom.Text & " 23:59:00'"
                ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                    sSQL += " and a.ReceivedTime between '" & dtpDateTo.Text &
                    "' and '" & dtpDateTo.Text & " 23:59:00'"
                ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                    sSQL += " and a.ReceivedTime between '" & dtpDateFrom.Text &
                    "' and '" & dtpDateTo.Text & " 23:59:00'"
                ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                    sSQL += " and a.ID = " & Val(txtAccFrom.Text)
                ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                    sSQL += " and a.ID = " & Val(txtAccTo.Text)
                ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                    sSQL += " and a.ID between " & txtAccFrom.Text & " and " & txtAccTo.Text
                ElseIf HasDiscretes() Then
                    Dim VALS As String = ""
                    For i As Integer = 0 To dgvDiscrete.RowCount - 1
                        If dgvDiscrete.Rows(i).Cells(0).Value IsNot Nothing _
                        AndAlso Trim(dgvDiscrete.Rows(i).Cells(0).Value) <> "" Then
                            VALS += Trim(dgvDiscrete.Rows(i).Cells(0).Value) & ", "
                        End If
                    Next
                    If VALS.EndsWith(", ") Then VALS = Microsoft.VisualBasic.Mid(VALS, 1, Len(VALS) - 2)
                    If VALS <> "" Then sSQL += " and a.ID in (" & VALS & ")"
                End If
            End If
            '
            Try
                If sSQL = "" Then
                    If dgvUnsynchAccs.RowCount > 0 Then btnSynchronize.Enabled = True
                    txtUnsynchCount.Text = dgvUnsynchAccs.RowCount
                    '
                    If dgvSynchAccs.RowCount > 0 Then
                        btnUnsynch.Enabled = True
                        btnBill.Enabled = True
                    End If
                    txtSynchCount.Text = dgvSynchAccs.RowCount
                    '
                    If dgvBilled.RowCount > 0 Then
                        btnReverse.Enabled = True
                        btnProcess.Enabled = True
                        UpdateDestination()
                    End If
                    txtBCount.Text = dgvBilled.RowCount
                    If cmbBillType.SelectedIndex = 1 Then
                        txtProcessed.Text = dgvProcTPs.RowCount
                    ElseIf cmbBillType.SelectedIndex = 2 Then
                        txtProcessed.Text = dgvProcPats.RowCount
                    End If
                    Return
                End If
                If txtPatientID.Text <> "" Then
                    If Not TB.SelectedTab Is TB.TabPages(3) Then
                        Dim patientId = Val(txtPatientID.Text)
                        sSQL += "and a.patient_id=" & patientId
                    End If

                End If

                Dim cnt As New SqlConnection(connString)
                cnt.Open()
                Dim cmdt As New SqlCommand(sSQL, cnt)
                cmdt.CommandTimeout = 90
                cmdt.CommandType = New CommandType()
                cmdt.CommandType = CommandType.Text
                Dim drt As SqlDataReader = cmdt.ExecuteReader
                If drt.HasRows Then
                    While drt.Read
                        If TB.SelectedTab Is TB.TabPages(0) Then    'Unsynch
                            If drt("IsIndividual") = False Then
                                Billee = drt("LastName_BSN") & " [" & drt("ProvID") & "]"
                            Else
                                Billee = drt("LastName_BSN") & ", " & drt("FirstName")
                                If drt("Degree") IsNot DBNull.Value AndAlso
                                Trim(drt("Degree")) <> "" Then Billee += " " & Trim(drt("Degree"))
                                Billee += " [" & drt("ProvID") & "]"
                            End If
                            dgvUnsynchAccs.Rows.Add(drt("Selected"), drt("AccID"),
                            drt("Patient (L, F)"), drt("Rec Date"), drt("Rpt Date"), Billee)
                        ElseIf TB.SelectedTab Is TB.TabPages(1) Then    'synch
                            If cmbBillType.SelectedIndex = 0 Then
                                If drt("IsIndividual") = False Then
                                    Billee = drt("LastName_BSN") & " [" & drt("ProvID") & "]"
                                Else
                                    Billee = drt("LastName_BSN") & ", " & drt("FirstName")
                                    If drt("Degree") IsNot DBNull.Value AndAlso
                                    Trim(drt("Degree")) <> "" Then Billee += " " & Trim(drt("Degree"))
                                    Billee += " [" & drt("ProvID") & "]"
                                End If
                                dgvSynchAccs.Rows.Add(drt("Selected"), drt("AccID"),
                                drt("Patient (L, F)"), drt("Svc Date"), Format(drt("Amount"), "0.00"), Billee)
                            ElseIf cmbBillType.SelectedIndex = 1 Then
                                Billee = drt("PayerName") & " [" & drt("PayerID") & "]"
                                dgvSynchAccs.Rows.Add(drt("Selected"), drt("AccID"),
                                drt("Patient (L, F)"), drt("Svc Date"), Format(drt("Amount"), "0.00"), Billee)
                            Else
                                Billee = drt("Patient (L, F)") & " [" & drt("PatientID") & "]"
                                dgvSynchAccs.Rows.Add(drt("Selected"), drt("AccID"),
                                drt("Patient (L, F)"), drt("Svc Date"), Format(drt("Amount"), "0.00"), Billee)
                            End If
                        ElseIf TB.SelectedTab Is TB.TabPages(2) Then    'Billed
                            If cmbBillType.SelectedIndex = 0 Then
                                If drt("IsIndividual") = False Then
                                    Billee = drt("LastName_BSN") & " [" & drt("ProvID") & "]"
                                Else
                                    Billee = drt("LastName_BSN") & ", " & drt("FirstName")
                                    If drt("Degree") IsNot DBNull.Value AndAlso
                                    Trim(drt("Degree")) <> "" Then Billee += " " & Trim(drt("Degree"))
                                    Billee += " [" & drt("ProvID") & "]"
                                End If
                                dgvBilled.Rows.Add(drt("Selected"), drt("InvID"), drt("AccID"), drt("Patient (L, F)"),
                                drt("Svc Date"), drt("Bill Date"), Format(drt("Amount"), "0.00"), Billee, drt("email"))
                            ElseIf cmbBillType.SelectedIndex = 1 Then
                                Billee = drt("PayerName") & " [" & drt("PayerID") & "]"
                                dgvBilled.Rows.Add(drt("Selected"), drt("InvID"), drt("AccID"), drt("Patient (L, F)"),
                                drt("Svc Date"), drt("Bill Date"), Format(drt("Amount"), "0.00"), Billee, drt("email"))
                            Else
                                Billee = drt("Patient (L, F)") & " [" & drt("PatientID") & "]"
                                dgvBilled.Rows.Add(drt("Selected"), drt("InvID"), drt("AccID"), drt("Patient (L, F)"),
                                drt("Svc Date"), drt("Bill Date"), Format(drt("Amount"), "0.00"), Billee, drt("email"))
                            End If
                        Else    'processed
                            If cmbBillType.SelectedIndex = 1 Then
                                dgvProcTPs.Rows.Add(drt("Selected"), drt("FileNo"), drt("Clearing House"),
                                drt("Created on"), drt("Invoices"), Format(drt("File Amount"), "$ 0,000.00"))
                            ElseIf cmbBillType.SelectedIndex = 2 Then
                                dgvProcPats.Rows.Add(drt("Selected"), drt("InvID"), drt("AccID"), drt("Patient (L, F)"),
                                drt("Svc Date"), drt("Bill Date"), Format(drt("Amount"), "0.00"), drt("Prints"), drt("email"))
                            End If
                        End If
                    End While
                End If
                cnt.Close()
                cnt = Nothing
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Prolis")
            End Try
            If dgvUnsynchAccs.RowCount > 0 Then btnSynchronize.Enabled = True
            txtUnsynchCount.Text = dgvUnsynchAccs.RowCount
            '
            If dgvSynchAccs.RowCount > 0 Then
                btnUnsynch.Enabled = True
                btnBill.Enabled = True
            End If
            txtSynchCount.Text = dgvSynchAccs.RowCount
            '
            If dgvBilled.RowCount > 0 Then
                btnReverse.Enabled = True
                btnProcess.Enabled = True
                UpdateDestination()
            End If
            txtBCount.Text = dgvBilled.RowCount
            If cmbBillType.SelectedIndex = 1 Then
                txtProcessed.Text = dgvProcTPs.RowCount
            ElseIf cmbBillType.SelectedIndex = 2 Then
                txtProcessed.Text = dgvProcPats.RowCount
            End If
        ElseIf txt837File.Text <> "" Then
            Dim SR As New StreamReader(txt837File.Text)
            Dim STR As String = SR.ReadToEnd
            Dim InvSegs() As String = Split(STR, "CLM")
            Dim sData() As String = Split(InvSegs(0), "~")
            Dim InvoiceInfo() As String = {"", "", "", "", ""} '0=AccID, 1=Patient, 2=SvcDate, 3=BillDate, 4=Billee
            Dim Data() As String = Split(sData(0), "*")
            Dim FileAmount As Single = 0
            Dim Output As String = "837 File: " & Val(Data(13)).ToString &
            " dated: " & Date.ParseExact(Data(9), "yyMMdd", CultureInfo.InvariantCulture) & ", contents" & vbCrLf
            Output += "----------------------------------" & vbCrLf
            Data = Split(sData(6), "*")
            Output += "Clearing House: " & Data(3) & vbCrLf
            Data = Split(sData(9), "*")
            Output += "Biller: " & Data(3) & vbCrLf
            dgvBilled.Rows.Clear()
            For i As Integer = 1 To InvSegs.Length - 1
                Data = Split(InvSegs(i), "*")
                InvoiceInfo = GetInvoiceInfo(Data(1))   '0=AccID, 1=Patient, 2=SvcDate, 3=BillDate, 4=Billee
                dgvBilled.Rows.Add(1, Data(1), InvoiceInfo(0), InvoiceInfo(1),
                InvoiceInfo(2), InvoiceInfo(3), Data(2), InvoiceInfo(4))
                FileAmount += Val(Data(2))
            Next
            txtBCount.Text = dgvBilled.RowCount
            Output += "File Amount: " & Math.Round(FileAmount, 2).ToString("0.00") & vbCrLf
            SR.Close()
            SR = Nothing
            DisplayOutput(Output)
        Else
            dgvUnsynchAccs.Rows.Clear()
            dgvSynchAccs.Rows.Clear()
            dgvBilled.Rows.Clear()
        End If
        EnableActions()
    End Sub

    Private Function GetInvoiceInfo(ByVal InvoiceID As Long) As String()
        Dim Info() As String = {"", "", "", "", ""}    '0=AccID, 1=Patient, 2=SvcDate, 3=BillDate, 4=Billee
        Dim sSQL As String = "Select a.Accession_ID, b.LastName + ', ' + b.FirstName as Patient, " &
        "convert(nvarchar, a.Svc_Date, 101) as SvcDate, convert(nvarchar, a.Bill_Date, 101) as BillDate, " &
        "c.PayerName from Patients b inner join (Payers c inner join (Requisitions d inner join Charges a " &
        "on a.Accession_ID = d.ID) on d.PrimePayer_ID = c.ID) on d.Patient_ID = b.ID where a.ID = " & InvoiceID
        Dim cnii As New SqlConnection(connString)
        cnii.Open()
        Dim cmdii As New SqlCommand(sSQL, cnii)
        cmdii.CommandType = CommandType.Text
        Dim drii As SqlDataReader = cmdii.ExecuteReader
        If drii.HasRows Then
            While drii.Read
                Info(0) = drii("Accession_ID")
                Info(1) = drii("Patient")
                Info(2) = drii("SvcDate")
                Info(3) = drii("BillDate")
                Info(4) = drii("PayerName")
            End While
        End If
        cnii.Close()
        cnii = Nothing
        Return Info
    End Function


    Private Sub chkFinals_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFinals.CheckedChanged
        If chkFinals.Checked = True Then
            chkFinals.Text = "FINALS"
            chkDot.Checked = True
            chkQNS.Checked = True
            chkTNP.Checked = True
            chkLAE.Checked = True
            chkDot.Enabled = True
            chkQNS.Enabled = True
            chkTNP.Enabled = True
            chkLAE.Enabled = True
        Else
            chkFinals.Text = "Ignore Results"
            chkDot.Checked = False
            chkQNS.Checked = False
            chkTNP.Checked = False
            chkLAE.Checked = False
            chkDot.Enabled = False
            chkQNS.Enabled = False
            chkTNP.Enabled = False
            chkLAE.Enabled = False
        End If
    End Sub

    Private Sub frmBillingDash_ResizeBegin(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeBegin
        origWidth = Me.Width
        origHeight = Me.Height
    End Sub

    Private Sub frmBillingDash_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        MeResize(Me, origWidth, origHeight)
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

    Private Sub txtAccFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccFrom.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAccTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccTo.KeyPress
        Numerals(sender, e)
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
                Try
                    dgvDiscrete.Rows.RemoveAt(e.RowIndex)

                Catch ex As Exception

                End Try
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
                If dgvDiscrete.Rows(0).Cells(0).Value <> "" Then
                    txt837File.Text = ""
                    txtAccFrom.Text = ""
                    txtAccTo.Text = ""
                    'txtDateFrom.Text = ""
                    'txtDateTo.Text = ""
                    ClearDateTimePicker(dtpDateFrom)
                    ClearDateTimePicker(dtpDateTo)
                    dgvDiscrete.Rows.Add()
                End If
            End If
        End If
    End Sub

    Private Sub btnSynchronize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSynchronize.Click
        DisableActions()
        txtOutput.Text = ""
        Dim Accessions() As String = {""}
        For i As Integer = 0 To dgvUnsynchAccs.RowCount - 1
            If CType(dgvUnsynchAccs.Rows(i).Cells(0).Value, Boolean) = True Then
                If chkFinals.Checked = True Then    'Finals
                    If IsDate(dgvUnsynchAccs.Rows(i).Cells(4).Value) Then
                        If Accessions(UBound(Accessions)) <> "" Then ReDim Preserve Accessions(UBound(Accessions) + 1)
                        Accessions(UBound(Accessions)) = Trim(dgvUnsynchAccs.Rows(i).Cells(1).Value)
                    End If
                Else    'Ignore Results
                    If Accessions(UBound(Accessions)) <> "" Then ReDim Preserve Accessions(UBound(Accessions) + 1)
                    Accessions(UBound(Accessions)) = Trim(dgvUnsynchAccs.Rows(i).Cells(1).Value)
                End If
            End If
        Next
        If Accessions(0) <> "" Then
            Dim Wrapper As WorkerArgs = New WorkerArgs
            Wrapper.Task = "Synchronize"
            Wrapper.Accessions = Accessions
            BW.RunWorkerAsync(Wrapper)
        End If
        EnableActions()
    End Sub

    Public Class WorkerArgs
        Public Task As String
        Public Accessions() As String
        Public BillType As String
        Public Billees() As String
        Public Invoices As String
        Public Dest() As String
        Public Validate As Boolean
    End Class

    Private Sub Synchronize(ByVal Accessions() As String)
        Dim Processed As Integer = 0
        Dim Synchs As Integer = 0
        Dim EXCEP As String = ""
        Dim Fails As Integer = 0
        Dim Output As String = ""
        Dim Temp As String
        Dim Data() As String
        If chkDot.Checked = True Then EXCEP = "."
        If chkQNS.Checked = True Then
            If EXCEP = "" Then
                EXCEP = "QNS"
            Else
                EXCEP += "|QNS"
            End If
        End If
        If chkLAE.Checked = True Then
            If EXCEP = "" Then
                EXCEP = "LAE"
            Else
                EXCEP += "|LAE"
            End If
        End If
        If chkTNP.Checked = True Then
            If EXCEP = "" Then
                EXCEP = "TNP"
            Else
                EXCEP += "|TNP"
            End If
        End If
        ' Try
        For i As Integer = 0 To Accessions.Length - 1
            If BW.CancellationPending = False Then
                If Accessions(i) <> "" Then
                    Data = SynchronizeAccession(Accessions(i), chkFinals.Checked, EXCEP)
                    If Data(0) = Accessions(i) Then
                        If Data(1).StartsWith("Success") Then
                            Synchs += 1
                        Else
                            Fails += 1
                        End If
                        Processed += 1
                        Temp = Join(Data, Chr(9))
                        Output += Temp & vbCrLf
                        Temp = ""
                    End If
                End If
                BW.ReportProgress(CInt((i + 1) * 100 / Accessions.Length),
                CStr(CInt((i + 1) * 100 / Accessions.Length)) & " %")
            Else
                Exit For
            End If
        Next
        'Catch ex As Exception
        '    Output += "Error '" & ex.Message & "' occured." & vbCrLf
        'End Try
        Output += "--------------------------" & vbCrLf
        If Synchs + Fails < Accessions.Length Then
            Output += "Process ended premature. Processed: " & Processed.ToString & vbCrLf &
            "Succeeded: " & Synchs.ToString & " of Total: " & Accessions.Length.ToString & vbCrLf &
            "Failed: " & Fails.ToString & " of Total: " & Accessions.Length.ToString & vbCrLf
        Else
            Output += "Process completed successfully. Processed: " & Processed.ToString & vbCrLf &
            "Succeeded: " & Synchs.ToString & " of Total: " & Accessions.Length.ToString & vbCrLf &
            "Failed: " & Fails.ToString & " of Total: " & Accessions.Length.ToString & vbCrLf
        End If
        DisplayOutput(Output)
    End Sub

    Private Sub BW_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BW.DoWork
        Dim Wrapper As WorkerArgs = e.Argument
        If Wrapper.Task = "Synchronize" Then
            Synchronize(Wrapper.Accessions)
        ElseIf Wrapper.Task = "Reverse" Then
            ReverseBilling(Wrapper.Accessions)
        ElseIf Wrapper.Task = "Bill" Then
            InvokeBilling(Wrapper.BillType, Wrapper.Accessions, chkValidate.Checked)
        ElseIf Wrapper.Task = "Process" Then
            ProcessReports(Wrapper.BillType, Wrapper.Billees, Wrapper.Invoices, Wrapper.Dest)
        End If
    End Sub

    Private Sub BWS_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BW.ProgressChanged
        PB.Value = e.ProgressPercentage
        lblStatus.Text = e.UserState
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

    Private Sub DisplayOutput(ByVal Msg As String)
        If txtOutput.InvokeRequired Then
            txtOutput.Invoke(New ClearDisplay(AddressOf ClearOutput))
        Else
            ClearOutput()
        End If
        '
        If txtOutput.InvokeRequired Then
            txtOutput.Invoke(New UpdateDisplay(AddressOf UpdateOutput), Msg)
        Else
            UpdateOutput(Msg)
        End If
    End Sub

    Public Delegate Sub ClearDisplay()

    Public Sub ClearOutput()
        txtOutput.Text = ""
    End Sub


    Public Delegate Sub UpdateDisplay(ByVal STR As String)

    Public Sub UpdateOutput(ByVal STR As String)
        txtOutput.AppendText(STR)
    End Sub

    Private Sub btnTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTarget.Click
        DisableActions()
        Dim Billis As String = ""
        'Dim ItemX As MyList
        Dim sSQL As String = ""
        If IsDate(dtpDateFrom.Text) Or IsDate(dtpDateTo.Text) Or (txtAccFrom.Text <> "" Or txtAccTo.Text <> "" Or HasDiscretes()) Then
            If txtPatientID.Text <> "" Then
                Dim patientId = Val(txtPatientID.Text)
                If cmbBillType.SelectedIndex = 0 Then   'client
                    sSQL = "Select * from Providers where ID in (Select distinct OrderingProvider_ID from " &
                    "Requisitions where Rejected = 0 and BillingType_ID = 0 and patient_id =" & patientId
                ElseIf cmbBillType.SelectedIndex = 1 Then   'Insurance
                    If TB.SelectedTab Is TB.TabPages(3) Then    'processed
                        sSQL = "Select * from Payers where ID in (Select distinct " &
                        "Payer_ID from ChargesEOutput"
                    Else
                        sSQL = "Select * from Payers where ID in (Select distinct PrimePayer_ID from " &
                        "Requisitions where Rejected = 0 and BillingType_ID = 1 and patient_id =" & patientId
                    End If
                Else
                    If TB.SelectedTab Is TB.TabPages(2) Then    'Billed
                        sSQL = "Select * from Patients where ID in (Select " &
                        "Patient_ID from Requisitions where Rejected = 0 and patient_id =" & patientId
                    Else
                        sSQL = "Select * from Patients where ID in (Select Patient_ID " &
                        "from Requisitions where Rejected = 0 and BillingType_ID = 2 and patient_id =" & patientId
                    End If
                End If
            Else
                If cmbBillType.SelectedIndex = 0 Then   'client
                    sSQL = "Select * from Providers where ID in (Select distinct OrderingProvider_ID from " &
                    "Requisitions where Rejected = 0 and BillingType_ID = 0"
                ElseIf cmbBillType.SelectedIndex = 1 Then   'Insurance
                    If TB.SelectedTab Is TB.TabPages(3) Then    'processed
                        sSQL = "Select * from Payers where ID in (Select distinct " &
                        "Payer_ID from ChargesEOutput"
                    Else
                        sSQL = "Select * from Payers where ID in (Select distinct PrimePayer_ID from " &
                        "Requisitions where Rejected = 0 and BillingType_ID = 1"
                    End If
                Else
                    If TB.SelectedTab Is TB.TabPages(2) Then    'Billed
                        sSQL = "Select * from Patients where ID in (Select " &
                        "Patient_ID from Requisitions where Rejected = 0 "
                    Else
                        sSQL = "Select * from Patients where ID in (Select Patient_ID " &
                        "from Requisitions where Rejected = 0 and BillingType_ID = 2 "
                    End If
                End If
            End If

            '
            If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                sSQL += " and ReceivedTime between '" & dtpDateFrom.Text &
                "' and '" & dtpDateFrom.Text & " 23:59:00'"
            ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                sSQL += " and ReceivedTime between '" & dtpDateTo.Text &
               "' and '" & dtpDateTo.Text & " 23:59:00'"
            ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                If TB.SelectedTab Is TB.TabPages(3) Then    'processed
                    If cmbBillType.SelectedIndex = 1 Then   'Insurance
                        sSQL += " where dated between '" & dtpDateFrom.Text &
                        "' and '" & dtpDateTo.Text & " 23:59:00'"
                    Else
                        sSQL += " and ReceivedTime between '" & dtpDateFrom.Text &
                        "' and '" & dtpDateTo.Text & " 23:59:00'"
                    End If
                Else
                    sSQL += " and ReceivedTime between '" & dtpDateFrom.Text &
                    "' and '" & dtpDateTo.Text & " 23:59:00'"
                End If
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                If Not sSQL.Contains("where") Then
                    sSQL += " where Charge_ID in (Select ID from Charges " &
                    "where Accession_ID = " & Val(txtAccFrom.Text) & ")"
                Else
                    sSQL += " and ID = " & Val(txtAccFrom.Text)
                End If
            ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                If Not sSQL.Contains("where") Then
                    sSQL += " where Charge_ID in (Select ID from Charges " &
                    "where Accession_ID = " & Val(txtAccTo.Text) & ")"
                Else
                    sSQL += " and ID = " & Val(txtAccTo.Text)
                End If
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                If Not sSQL.Contains("where") Then
                    sSQL += " where Charge_ID in (Select ID from Charges " &
                    "where Accession_ID between " & Val(txtAccFrom.Text) &
                    " and " & Val(txtAccTo.Text) & ")"
                Else
                    sSQL += " and ID between " & Val(txtAccFrom.Text) &
                    " and " & Val(txtAccTo.Text)
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
                    If TB.SelectedTab Is TB.TabPages(3) Then    'processed
                        If cmbBillType.SelectedIndex = 2 Then
                            If sSQL.Contains("Select Patient_ID from Requisitions where") Then
                                If txtPatientID.Text <> "" Then
                                    Dim patientId = Val(txtPatientID.Text)
                                    sSQL += " and ID in (" & VALS & ") and patient_id =" & patientId
                                Else
                                    sSQL += " and ID in (" & VALS & ")"
                                End If

                            Else
                                sSQL += " where ID in (" & VALS & ")"

                                If txtPatientID.Text <> "" Then
                                    Dim patientId = Val(txtPatientID.Text)
                                    sSQL += " where ID in (" & VALS & ") and patient_id  =" & patientId
                                Else
                                    sSQL += " where ID in (" & VALS & ")"
                                End If
                            End If

                        ElseIf cmbBillType.SelectedIndex = 1 Then
                            If sSQL.Contains("Select distinct Payer_ID from ChargesEOutput where") Then

                                sSQL += " and Partner_ID in (" & VALS & ")"
                            Else
                                sSQL += " where Partner_ID in (" & VALS & ")"
                            End If
                        End If


                    Else

                        sSQL += " and ID in (" & VALS & ")"


                    End If
                End If
            End If
            If TB.SelectedTab Is TB.TabPages(0) Then    'Unsynch
                sSQL += " and not ID in (Select distinct Accession_ID from Req_Billable))"
            ElseIf TB.SelectedTab Is TB.TabPages(1) Then    'Unbilled
                sSQL += " and ID in (Select distinct Accession_ID from Req_Billable) and " &
                "not ID in (Select Accession_ID from Charges))"
            ElseIf TB.SelectedTab Is TB.TabPages(2) Then    'Billed
                If cmbBillType.SelectedIndex = 2 Then   'Patient
                    sSQL += " and ID in (Select Accession_ID from Charges where ArType = 2))"
                Else
                    sSQL += " and ID in (Select Accession_ID from Charges))"
                End If
            Else    'Submitted
                sSQL += ")"
            End If
            '
            lstTargets.Items.Clear()
            Dim Provider As String = ""
            Dim cnt As New SqlConnection(connString)
            cnt.Open()
            Dim cmdt As New SqlCommand(sSQL, cnt)
            cmdt.CommandType = CommandType.Text
            cmdt.CommandTimeout = 180
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
                        Provider += " [" & drt("ID") & "]"
                        lstTargets.Items.Add(New MyList(Provider, drt("ID")))
                    ElseIf cmbBillType.SelectedIndex = 1 Then   'Third Party
                        lstTargets.Items.Add(New MyList(drt("PayerName") & " [" & drt("ID") & "]", drt("ID")))
                    Else    'Patient
                        lstTargets.Items.Add(New MyList(drt("LastName") & ", " & drt("FirstName") & " [" & drt("ID") & "]", drt("ID")))
                    End If
                    'lstTargets.SetItemChecked(lstTargets.Items.Count - 1, True)
                End While
            End If
            cnt.Close()
            cnt = Nothing
            If lstTargets.Items.Count > 0 Then btnSellT_Click(Nothing, Nothing)
        End If
        EnableActions()
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

    Private Sub btnSellT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSellT.Click
        For i As Integer = 0 To lstTargets.Items.Count - 1
            lstTargets.SetItemChecked(i, True)
        Next
    End Sub

    Private Sub btnDeselT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeselT.Click
        For i As Integer = 0 To lstTargets.Items.Count - 1
            lstTargets.SetItemChecked(i, False)
        Next
    End Sub

    Private Sub btnSelUs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelUs.Click
        For i As Integer = 0 To dgvUnsynchAccs.RowCount - 1
            dgvUnsynchAccs.Rows(i).Cells(0).Value = True
        Next
    End Sub

    Private Sub btnDeselUS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeselUS.Click
        For i As Integer = 0 To dgvUnsynchAccs.RowCount - 1
            dgvUnsynchAccs.Rows(i).Cells(0).Value = False
        Next
    End Sub

    Private Sub TB_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TB.SelectedIndexChanged
        lstTargets.Items.Clear()

        txtOutput.Text = ""
        Dim tabControlObj As TabControl = DirectCast(sender, TabControl)
        If tabControlObj.SelectedTab.Name.Contains("Billed") Then
            viewClaim.Hide()
        Else
            viewClaim.Hide()
        End If
        dgvUnsynchAccs.Rows.Clear()
    End Sub

    Private Sub chkNZero_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNZero.CheckedChanged
        If chkNZero.Checked = True Then
            chkNZero.Text = "Non Zero"
        Else
            chkNZero.Text = "All"
        End If
    End Sub

    Private Sub cmbBillType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBillType.SelectedIndexChanged
        lstTargets.Items.Clear()
        dgvUnsynchAccs.Rows.Clear()
        dgvSynchAccs.Rows.Clear()
        dgvBilled.Rows.Clear()
        UpdateDestination()
        If cmbBillType.SelectedIndex = 1 Then
            chkValidate.Checked = True
            chkValidate.Enabled = True
            dgvProcTPs.Visible = True
            dgvProcPats.Visible = False
        ElseIf cmbBillType.SelectedIndex = 2 Then
            dgvProcTPs.Visible = False
            dgvProcPats.Visible = True
        Else
            dgvProcTPs.Visible = False
            dgvProcPats.Visible = False
            chkValidate.Checked = False
            chkValidate.Enabled = False
        End If
    End Sub

    Private Sub UpdateDestination()
        cmbDestination.Items.Clear()
        cmbDestination.SelectedIndex = -1
        Dim SelCount As Integer = 0
        If cmbBillType.SelectedIndex = 0 Then   'Client
            SelCount = lstTargets.CheckedItems.Count
            If SelCount = 1 Then
                cmbDestination.Items.Add("Printer")
                cmbDestination.Items.Add("Screen")
                cmbDestination.Items.Add("Email")
                cmbDestination.SelectedIndex = 1
            ElseIf SelCount > 1 Then
                cmbDestination.Items.Add("Printer")
                cmbDestination.Items.Add("Email")
                cmbDestination.SelectedIndex = 0
            Else
                cmbDestination.Items.Clear()
                cmbDestination.SelectedIndex = -1
            End If
        ElseIf cmbBillType.SelectedIndex = 1 Then   'TP
            SelCount = GetBilledSelected()
            If SelCount > 0 And SelCount < 5 Then
                cmbDestination.Items.Add("Printer")
                cmbDestination.Items.Add("Screen")
                cmbDestination.Items.Add("837 File")
                cmbDestination.SelectedIndex = 1
            ElseIf SelCount > 4 Then
                cmbDestination.Items.Add("Printer")
                cmbDestination.Items.Add("837 File")
                cmbDestination.SelectedIndex = 1
            Else
                cmbDestination.Items.Clear()
                cmbDestination.SelectedIndex = -1
            End If
        Else  'patient
            SelCount = GetBilledSelected()
            If SelCount > 0 And SelCount < 5 Then
                cmbDestination.Items.Add("Printer")
                cmbDestination.Items.Add("Screen")
                cmbDestination.Items.Add("Email")
                cmbDestination.SelectedIndex = 1
            ElseIf SelCount > 4 Then
                cmbDestination.Items.Add("Printer")
                cmbDestination.Items.Add("Email")
                cmbDestination.SelectedIndex = 0
            Else
                cmbDestination.Items.Clear()
                cmbDestination.SelectedIndex = -1
            End If
        End If
        UpdateProcessProgress()
    End Sub

    Private Sub UpdateProcessProgress()
        If cmbDestination.SelectedIndex <> -1 Then
            btnProcess.Enabled = True
        Else
            btnProcess.Enabled = False
        End If
    End Sub

    Private Function GetBilledSelected() As Integer
        Dim CNT As Integer = 0
        For i As Integer = 0 To dgvBilled.RowCount - 1
            If CType(dgvBilled.Rows(i).Cells(0).Value, Boolean) = True Then CNT += 1
        Next
        Return CNT
    End Function

    'Private Sub dgvBilled_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvBilled.CellContentClick
    '    UpdateDestination()
    'End Sub

    Private Sub dgvBilled_CellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dgvBilled.CellValueChanged
        If e.ColumnIndex = 0 AndAlso e.RowIndex >= 0 Then ' Assuming checkbox column is at index 0
            UpdateDestination()
        End If
    End Sub


    Private Sub btnSelB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelB.Click

        ' Detach the CellValueChanged event handler
        RemoveHandler dgvBilled.CellValueChanged, AddressOf dgvBilled_CellValueChanged


        For i As Integer = 0 To dgvBilled.RowCount - 1
            dgvBilled.Rows(i).Cells(0).Value = True
        Next
        UpdateDestination()

        ' Reattach the CellValueChanged event handler
        AddHandler dgvBilled.CellValueChanged, AddressOf dgvBilled_CellValueChanged

    End Sub

    Private Sub btnDeselB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeselB.Click

        ' Detach the CellValueChanged event handler
        RemoveHandler dgvBilled.CellValueChanged, AddressOf dgvBilled_CellValueChanged

        For i As Integer = 0 To dgvBilled.RowCount - 1
            dgvBilled.Rows(i).Cells(0).Value = False
        Next
        UpdateDestination()

        ' Reattach the CellValueChanged event handler
        AddHandler dgvBilled.CellValueChanged, AddressOf dgvBilled_CellValueChanged

    End Sub

    Private Sub btnSelS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelS.Click
        For i As Integer = 0 To dgvSynchAccs.RowCount - 1
            dgvSynchAccs.Rows(i).Cells(0).Value = True
        Next
    End Sub

    Private Sub btnDeselS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeselS.Click
        For i As Integer = 0 To dgvSynchAccs.RowCount - 1
            dgvSynchAccs.Rows(i).Cells(0).Value = False
        Next
    End Sub

    Private Sub btnUnsynch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnsynch.Click
        Dim Accs As String = ""
        Dim CNT As Integer = 0
        For i As Integer = 0 To dgvSynchAccs.RowCount - 1
            If CType(dgvSynchAccs.Rows(i).Cells(0).Value, Boolean) = True Then
                Accs += dgvSynchAccs.Rows(i).Cells(1).Value & ", "
                CNT += 1
            End If
        Next
        If Accs.EndsWith(", ") Then Accs = Accs.Substring(0, Len(Accs) - 2)
        If Accs <> "" Then
            Dim RetVal As Integer = MsgBox("You are about to unsynch " & CNT.ToString & " accession(s). " &
            "Are you sure of your action?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
            If RetVal = vbYes Then
                ExecuteSqlProcedure("Delete from Req_Billable where Accession_ID in (" & Accs & ")")
                UpsertUser_Event(ThisUser.ID, 39, Date.Now, "Accession", CNT, "Synched " & Accs, "Unsynched " & Accs)
                txtOutput.AppendText("On " & Date.Now & " " & GetUserName(ThisUser.ID) &
                " unsynched " & CNT & " following Accessions:" & vbCrLf)
                Dim AccIDs() As String = Split(Accs, ", ")
                For i As Integer = 0 To AccIDs.Length - 1
                    txtOutput.AppendText(AccIDs(i) & vbCrLf)
                Next
            End If
        End If
    End Sub

    Private Sub btnBill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBill.Click
        Dim Accessions() As String = {""}
        For i As Integer = 0 To dgvSynchAccs.RowCount - 1
            If CType(dgvSynchAccs.Rows(i).Cells(0).Value, Boolean) = True Then
                If Accessions(UBound(Accessions)) <> "" Then ReDim Preserve Accessions(UBound(Accessions) + 1)
                Accessions(UBound(Accessions)) = dgvSynchAccs.Rows(i).Cells(1).Value.ToString
            End If
        Next
        If Accessions(0) <> "" Then
            Dim Wrapper As WorkerArgs = New WorkerArgs
            Wrapper.Task = "Bill"
            Wrapper.BillType = cmbBillType.SelectedIndex
            Wrapper.Accessions = Accessions
            BW.RunWorkerAsync(Wrapper)
        End If
    End Sub

    Public Function ValidateBillables(ByVal AccID As String) As Boolean
        Dim Done As Boolean = False
        Dim Requisits() As String = {"", "", "", "", "", "", "", ""}
        '0=AddID, 1=NPI, 2=Policy, 3=Group, 4=Dx, 5=CPT, 6=Pointer, 7=Price
        Dim i As Integer = 0
        Dim sSQL As String = "Select Required from Payer_Requisit where Payer_ID in (Select " &
        "PrimePayer_ID from Requisitions where BillingType_ID = 1 and ID = " & AccID & ")"
        Dim cnpr As New SqlConnection(connString)
        cnpr.Open()
        Dim cmdpr As New SqlCommand(sSQL, cnpr)
        cmdpr.CommandType = CommandType.Text
        Dim drpr As SqlDataReader = cmdpr.ExecuteReader
        If drpr.HasRows Then
            While drpr.Read
                If i <= UBound(Requisits) Then
                    Requisits(i) = Convert.ToInt16(drpr("Required")).ToString
                    i += 1
                End If
            End While
        End If
        cnpr.Close()
        cnpr = Nothing
        '
        If Requisits(0) <> "" And Requisits(1) <> "" And Requisits(2) <> "" And Requisits(3) <> "" _
        And Requisits(4) <> "" And Requisits(5) <> "" And Requisits(6) <> "" And Requisits(7) <> "" Then  'requisites fetched
            Dim B1 As Boolean = False : Dim B2 As Boolean = False
            Dim B3 As Boolean = False : Dim B4 As Boolean = False
            Dim B5 As Boolean = False : Dim B6 As Boolean = True
            Dim B7 As Boolean = True : Dim B8 As Boolean = True
            sSQL = "Select p.Address_ID, o.NPI, c.PolicyNo, c.GroupNo from Patients p inner join " &
            "(Providers o inner join(Requisitions a inner join  Req_Coverage c on c.Accession_ID = a.ID) " &
            "on o.ID = a.AttendingProvider_ID) on a.Patient_ID = p.ID where c.Preference = 'P' and a.ID = " & AccID
            Dim cnvb As New SqlConnection(connString)
            cnvb.Open()
            Dim cmdvb As New SqlCommand(sSQL, cnvb)
            cmdvb.CommandType = CommandType.Text
            Dim drvb As SqlDataReader = cmdvb.ExecuteReader
            If drvb.HasRows Then
                While drvb.Read
                    If Requisits(0) = "1" Then    'address required
                        If drvb("Address_ID") IsNot DBNull.Value Then B1 = True
                    Else    'Not required
                        B1 = True
                    End If
                    '
                    If Requisits(1) = "1" Then    'NPI required
                        If drvb("NPI") IsNot DBNull.Value AndAlso
                        Trim(drvb("NPI")) <> "" Then B2 = True
                    Else    'Not required
                        B2 = True
                    End If
                    '
                    If Requisits(2) = 1 Then    'Policy required
                        If drvb("PolicyNo") IsNot DBNull.Value AndAlso
                        Trim(drvb("PolicyNo")) <> "" Then B3 = True
                    Else    'Not required
                        B3 = True
                    End If
                    '
                    If Requisits(3) = 1 Then    'Group required
                        If drvb("GroupNo") IsNot DBNull.Value AndAlso
                        Trim(drvb("GroupNo")) <> "" Then B4 = True
                    Else    'Not required
                        B4 = True
                    End If
                End While
            End If
            cnvb.Close()
            cnvb = Nothing
            '0=AddID, 1=NPI, 2=Policy, 3=Group, 4=Dx, 5=CPT, 6=Pointer, 7=Price
            Dim Dxs As String = ""
            sSQL = "Select Dx_Code from Req_Dx where Accession_ID = " & AccID & " order by Ordinal"
            Dim cndx As New SqlConnection(connString)
            cndx.Open()
            Dim cmddx As New SqlCommand(sSQL, cndx)
            cmddx.CommandType = CommandType.Text
            Dim drdx As SqlDataReader = cmddx.ExecuteReader
            If drdx.HasRows Then
                While drdx.Read
                    If drdx("Dx_Code") IsNot DBNull.Value AndAlso
                    Trim(drdx("Dx_Code")) <> "" Then Dxs += Trim(drdx("Dx_Code")) & "|"
                End While
                If Dxs.EndsWith("|") Then Dxs = Dxs.Substring(0, Len(Dxs) - 1)
            End If
            cndx.Close()
            cndx = Nothing
            If Requisits(4) = 1 Then    'Dx required
                If Dxs <> "" Then B5 = True
            Else    'Not required
                B5 = True
            End If
            If Dxs <> "" Then
                Dim ReqDxs() As String = Split(Dxs, "|")
                Dim DxPtr As String = ""
                'B6 = CPT, B7 = Pointer, B8 = price
                sSQL = "Select TGP_ID, CPT_Code, ICD9, Extend from Req_Billable where Accession_ID = " & AccID
                Dim cnrb As New SqlConnection(connString)
                cnrb.Open()
                Dim cmdrb As New SqlCommand(sSQL, cnrb)
                cmdrb.CommandType = CommandType.Text
                Dim drrb As SqlDataReader = cmdrb.ExecuteReader
                If drrb.HasRows Then
                    While drrb.Read
                        If Requisits(5) = 1 Then    'CPT req
                            If drrb("CPT_Code") Is DBNull.Value _
                            OrElse Trim(drrb("CPT_Code")) = "" Then
                                B6 = False
                                Exit While
                            End If
                        End If
                        If Requisits(7) = 1 Then    'price req
                            If drrb("Extend") Is DBNull.Value _
                             OrElse drrb("Extend") = 0 Then
                                B8 = False
                                Exit While
                            End If
                        End If
                        If Requisits(6) = 1 Then    'Pointer req
                            If drrb("ICD9") Is DBNull.Value _
                             OrElse Trim(drrb("ICD9")) = "" Then
                                DxPtr = GetTGPDxPointer(drrb("TGP_ID"), ReqDxs)
                                If DxPtr = "" Then
                                    B7 = False
                                    Exit While
                                Else
                                    ExecuteSqlProcedure("Update Req_Billable set ICD9 = '" & DxPtr & "' " &
                                    "where Accession_ID = " & AccID & " and TGP_ID = " & drrb("TGP_ID"))
                                End If
                            End If
                        End If
                    End While
                End If
                cnrb.Close()
                cnrb = Nothing
            End If
            If B1 = True And B2 = True And B3 = True And B4 = True And
            B5 = True And B6 = True And B7 = True And B8 = True Then
                Done = True
            End If
        End If
        Return Done
    End Function

    Private Function GetTGPDxPointer(ByVal TGPID As Integer, ByVal ReqDxs() As String) As String
        Dim Ptr As String = ""
        Dim n As Int16 = 0
        Dim sSQL As String = "Select Dx_Code from Necessity where TGP_ID = " & TGPID
        Dim cnnc As New SqlConnection(connString)
        cnnc.Open()
        Dim cmdnc As New SqlCommand(sSQL, cnnc)
        cmdnc.CommandType = CommandType.Text
        Dim drnc As SqlDataReader = cmdnc.ExecuteReader
        If drnc.HasRows Then
            While drnc.Read
                For i As Integer = 0 To ReqDxs.Length - 1
                    If ReqDxs(i).ToUpper = drnc("Dx_Code").ToString.ToUpper Then
                        Ptr += (i + 1).ToString & ","
                        n += 1
                        If n >= 4 Then Exit While
                    End If
                Next
            End While
            If Ptr.EndsWith(",") Then Ptr = Ptr.Substring(0, Len(Ptr) - 1)
        Else
            Ptr = "1"
        End If
        cnnc.Close()
        cnnc = Nothing
        Return Ptr
    End Function

    Private Sub InvokeBilling(ByVal BillType As Integer, ByVal Accessions() As String, ByVal Validate As Boolean)
        Dim AccAmt As Single = 0
        Dim BilledAmt As Single = 0
        Dim CNT As Integer = 0
        Dim Output As String = ""
        Try
            For i As Integer = 0 To Accessions.Length - 1
                If Validate = True Then
                    Dim vlid = SubscriberAddExists(Accessions(i))
                    If ValidateBillables(Accessions(i)) = True And vlid = True Then
                        Try
                            AccAmt = BillAccession(Accessions(i), "", "")
                            Output += Accessions(i) & " billed amount $ " & Format(AccAmt, "0.00") & vbCrLf
                            BilledAmt += AccAmt
                            AccAmt = 0
                            BW.ReportProgress(CInt((i + 1) / Accessions.Length * 100),
                            CStr(CInt((i + 1) / Accessions.Length * 100)) & " %")
                            CNT += 1
                        Catch ex As Exception
                            Output += Accessions(i) & " Skipped  " & vbCrLf
                            AccAmt = 0
                            BW.ReportProgress(CInt((i + 1) / Accessions.Length * 100),
                            CStr(CInt((i + 1) / Accessions.Length * 100)) & " %")
                            CNT += 1
                        End Try

                    Else    'validation failed
                        Output += Accessions(i) & " validation failed - not billed" & vbCrLf
                    End If
                Else    'other billtypes 0 & 2
                    AccAmt = BillAccession(Accessions(i), "", "")
                    Output += Accessions(i) & " billed amount $ " & Format(AccAmt, "0.00") & vbCrLf
                    BilledAmt += AccAmt
                    AccAmt = 0
                    BW.ReportProgress(CInt((i + 1) / Accessions.Length * 100),
                    CStr(CInt((i + 1) / Accessions.Length * 100)) & " %")
                    CNT += 1
                End If
            Next
        Catch ex As Exception
            Output += "Error '" & ex.Message & "' occured."
        End Try
        If Output <> "" Then
            Output += "--------------------------------" & vbCrLf
            Output += "Accessions: " & CNT.ToString &
            " Amount: " & Format(BilledAmt, "0.00")
            DisplayOutput(Output)
        Else
            DisplayOutput("Accessions: 0 Amount: 0.00")
        End If
    End Sub

    Private Function GetAccArInfo(ByVal AccID As String) As String()
        Dim ArInfo() As String = {"", "", "", ""}
        '0=ArType, 1=ArID, 2=Policy, 3=Group
        Dim cnari As New SqlConnection(connString)
        cnari.Open()
        Dim cmdari As New SqlCommand("Select a.BillingType_ID as ArType, a.PrimePayer_ID as ArID, " &
        "IsNull((Select PolicyNo from Req_Coverage where Preference = 'P' and Accession_ID = a.ID), '') as " &
        "PolicyNo, IsNull((Select GroupNo from Req_Coverage where Preference = 'P' and Accession_ID = a.ID), " &
        "'') as GroupNo from Requisitions a where a.ID = " & AccID, cnari)
        cmdari.CommandType = CommandType.Text
        Dim drari As SqlDataReader = cmdari.ExecuteReader
        If drari.HasRows Then
            While drari.Read
                ArInfo(0) = drari("ArType")
                ArInfo(1) = drari("ArID")
                If drari("ArType") = 1 Then
                    If drari("PolicyNo") IsNot DBNull.Value _
                    AndAlso Trim(drari("PolicyNo")) <> "" _
                    Then ArInfo(2) = Trim(drari("PolicyNo"))
                    If drari("GroupNo") IsNot DBNull.Value _
                    AndAlso Trim(drari("GroupNo")) <> "" _
                    Then ArInfo(2) = Trim(drari("GroupNo"))
                End If
            End While
        End If
        cnari.Close()
        cnari = Nothing
        Return ArInfo
    End Function

    Private Function BillAccession(ByVal AccID As Long, ByVal Reason As String, ByVal PreAuth As String) As Single
        Dim ArInfo() As String = GetAccArInfo(AccID)
        '0=ArType, 1=ArID, 2=Policy, 3=Group
        Dim AccAmount As Single = 0
        Dim Discreteds As String = ""
        Dim Recs As Integer = 0
        Dim InvAmt As Single = 0
        Dim ChargeID As Long = 0
        Dim ChargeItems() As String
        'Dim ChargeItems() As String = UpdateChargeItems()
        '0=T, 1=U, 2=N, 3=H, 4=P
        '
        Dim sSQL As String = ""
        If ArInfo(0) = 1 Then   'TP
            sSQL = "Select TGP_ID from Req_Billable where Bill_Status = 'U' and " &
            "Accession_ID = " & AccID & " and CPT_Code in (Select CPT_Code from DiscreteClaimCodes)"
            Dim cnbc As New SqlConnection(connString)
            cnbc.Open()
            Dim cmdbc As New SqlCommand(sSQL, cnbc)
            cmdbc.CommandType = CommandType.Text
            Dim drbc As SqlDataReader = cmdbc.ExecuteReader
            If drbc.HasRows Then
                ChargeID = GetChargeID(ArInfo(0), ArInfo(1), AccID, Reason, PreAuth)
                While drbc.Read
                    Discreteds += drbc("TGP_ID").ToString & "|"
                End While
                If Discreteds.EndsWith("|") Then Discreteds = Discreteds.Substring(0, Len(Discreteds) - 1)
                '
                InvAmt = BillChargeDetail(AccID, ChargeID, Discreteds)
                UpdateCharges(ChargeID, ArInfo(0), ArInfo(1), AccID, Math.Round(InvAmt, 2))
            End If
            cnbc.Close()
            cnbc = Nothing
            '
            If Discreteds <> "" And Not Discreteds.EndsWith("|") Then Discreteds += "|"
            InvAmt = 0
            ChargeID = GetChargeID(ArInfo(0), ArInfo(1), AccID, Reason, PreAuth)
            ChargeItems = GetBillables(AccID)   'Unbilled
            For i As Integer = 0 To ChargeItems.Length - 1
                Dim Fields() As String = Split(ChargeItems(i), "|")
                If InStr(Discreteds, Fields(0) & "|") = 0 Then
                    If Recs < 50 Then
                        InvAmt += BillChargeDetailItem(ChargeID, i, Fields)
                        ExecuteSqlProcedure("Update Req_TGP Set Billed = 1 where " &
                        "Accession_ID = " & AccID & " and TGP_ID = " & Fields(0))
                        ExecuteSqlProcedure("Update Req_Billable Set Bill_Status = 'B' where " &
                        "Accession_ID = " & AccID & " and TGP_ID = " & Fields(0))
                        Recs += 1
                    Else
                        UpdateCharges(ChargeID, ArInfo(0), ArInfo(1), AccID, Math.Round(InvAmt, 2))
                        AccAmount += InvAmt
                        InvAmt = 0
                        Recs = 0
                        ChargeID = GetChargeID(ArInfo(0), ArInfo(1), AccID, Reason, PreAuth)
                        InvAmt += BillChargeDetailItem(ChargeID, i, Fields)
                        ExecuteSqlProcedure("Update Req_TGP Set Billed = 1 where " &
                        "Accession_ID = " & AccID & " and TGP_ID = " & Fields(0))
                        ExecuteSqlProcedure("Update Req_Billable Set Bill_Status = 'B' where " &
                        "Accession_ID = " & AccID & " and TGP_ID = " & Fields(0))
                        Recs += 1
                    End If
                End If
            Next
            If Recs > 0 And Recs <= 50 Then
                UpdateCharges(ChargeID, ArInfo(0), ArInfo(1), AccID, Math.Round(InvAmt, 2))
                AccAmount += InvAmt
                InvAmt = 0
                Recs = 0
            End If
        Else    '0 and 2
            InvAmt = 0
            ChargeID = GetChargeID(ArInfo(0), ArInfo(1), AccID, Reason, PreAuth)
            ChargeItems = GetBillables(AccID)   'Unbilled
            For i As Integer = 0 To ChargeItems.Length - 1
                Dim Fields() As String = Split(ChargeItems(i), "|")
                If Fields.Length > 11 Then
                    InvAmt += BillChargeDetailItem(ChargeID, i, Fields)
                    ExecuteSqlProcedure("Update Req_TGP Set Billed = 1 where " &
                    "Accession_ID = " & AccID & " and TGP_ID = " & Fields(0))
                    ExecuteSqlProcedure("Update Req_Billable Set Bill_Status = 'B' where " &
                    "Accession_ID = " & AccID & " and TGP_ID = " & Fields(0))
                End If
            Next
            UpdateCharges(ChargeID, ArInfo(0), ArInfo(1), AccID, Math.Round(InvAmt, 2))
            AccAmount += InvAmt
            InvAmt = 0
            Recs = 0
        End If
        '
        Return AccAmount
    End Function

    Private Function BillChargeDetailItem(ByVal ChargeID As Long, ByVal Ordinal As Integer, ByVal Fields() As String) As Double
        '0=TGP_ID, 1=CPT, 2=DxPointer, 3=Unit, 4=LinePrice, 5=Extend, 6=Mod1, 7=Mod2, 8=Mod3, 9=Mod4, 10=Svc_Date, 11=POS_Code
        Dim sSQL As String = "If Exists (Select * from Charge_Detail where Charge_ID = " & ChargeID & " and TGP_ID = " &
        Fields(0) & ") Update Charge_Detail Set Ordinal = " & Ordinal & ", CPT_Code = '" & Fields(1) & "', ICD9 = '" &
        Fields(2) & "', Mod1 = '" & Fields(6) & "', Mod2 = '" & Fields(7) & "', Mod3 = '" & Fields(8) & "', Mod4 = '" &
        Fields(9) & "', Unit = " & Fields(3) & ", LinePrice = " & Fields(4) & ", Extend = " & Math.Round(Val(Fields(5)), 2) _
        & ", POS_Code = '" & Fields(11) & "' where Charge_ID = " & ChargeID & " and TGP_ID = " & Fields(0) & " Else " &
        "Insert into Charge_Detail (Charge_ID, TGP_ID, Ordinal, CPT_Code, ICD9, Mod1, Mod2, Mod3, Mod4, Unit, LinePrice, " &
        "Extend, POS_Code, Billed_On, Billed_By) values (" & ChargeID & ", " & Fields(0) & ", " & Ordinal & ", '" & Fields(1) &
        "', '" & Fields(2) & "', '" & Fields(6) & "', '" & Fields(7) & "', '" & Fields(8) & "', '" & Fields(9) & "', " & Fields(3) _
        & ", " & Fields(4) & ", " & Math.Round(Val(Fields(5)), 2) & ", '" & Fields(11) & "', '" & dtpBillDate.Value & "', " & ThisUser.ID & ")"
        ExecuteSqlProcedure(sSQL)
        '
        Return Math.Round(Val(Fields(5)), 2)
    End Function

    Private Function GetBillables(ByVal AccID As String) As String()
        Dim Billables() As String = {""}
        Dim sSQL = "Select * from Req_Billable where Bill_Status = 'U' and Accession_ID = " & AccID & " order by Ordinal"
        Dim cngb As New SqlConnection(connString)
        cngb.Open()
        Dim cmdgb As New SqlCommand(sSQL, cngb)
        cmdgb.CommandType = CommandType.Text
        Dim drgb As SqlDataReader = cmdgb.ExecuteReader
        If drgb.HasRows Then
            While drgb.Read
                If Billables(UBound(Billables)) <> "" Then ReDim Preserve Billables(UBound(Billables) + 1)
                Billables(UBound(Billables)) = drgb("TGP_ID").ToString & "|" & Trim(drgb("CPT_Code")) & "|" &
                Trim(drgb("ICD9")) & "|" & drgb("Unit").ToString & "|" & drgb("LinePrice").ToString & "|" &
                drgb("Extend").ToString & "|" & Trim(drgb("Mod1")) & "|" & Trim(drgb("Mod2")) & "|" & Trim(drgb("Mod3")) _
                & "|" & Trim(drgb("Mod4")) & "|" & drgb("Svc_Date").ToString & "|" & Trim(drgb("POS_Code"))
            End While
        End If
        cngb.Close()
        cngb = Nothing
        Return Billables
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
            Dim cmdbcd As New SqlCommand("Select * " &
            "from Req_Billable where Accession_ID = " &
            AccID & " and TGP_ID in (" & ChargeItems & ")", cnbcd)
            cmdbcd.CommandType = CommandType.Text
            Dim drbcd As SqlDataReader = cmdbcd.ExecuteReader
            If drbcd.HasRows Then
                While drbcd.Read
                    If drbcd("Bill_Status") = "U" Then
                        Dim cncd As New SqlConnection(connString)
                        cncd.Open()
                        Dim cmdupsert As New SqlCommand("Charge_Detail_SP", cncd)
                        cmdupsert.CommandType = CommandType.StoredProcedure
                        cmdupsert.Parameters.AddWithValue("@act", "Upsert")
                        cmdupsert.Parameters.AddWithValue("@Charge_ID", ChargeID)
                        cmdupsert.Parameters.AddWithValue("@TGP_ID", drbcd("TGP_ID"))
                        cmdupsert.Parameters.AddWithValue("@Ordinal", i)
                        cmdupsert.Parameters.AddWithValue("@CPT_Code", drbcd("CPT_Code"))
                        cmdupsert.Parameters.AddWithValue("@ICD9", drbcd("ICD9"))
                        cmdupsert.Parameters.AddWithValue("@Unit", drbcd("Unit"))
                        cmdupsert.Parameters.AddWithValue("@LinePrice", drbcd("LinePrice"))
                        cmdupsert.Parameters.AddWithValue("@Extend", drbcd("Extend"))
                        cmdupsert.Parameters.AddWithValue("@Mod1", drbcd("Mod1"))
                        cmdupsert.Parameters.AddWithValue("@Mod2", drbcd("Mod2"))
                        cmdupsert.Parameters.AddWithValue("@Mod3", drbcd("Mod3"))
                        cmdupsert.Parameters.AddWithValue("@Mod4", drbcd("Mod4"))
                        cmdupsert.Parameters.AddWithValue("@POS_Code", drbcd("CPT_Code"))
                        cmdupsert.Parameters.AddWithValue("@Billed_On", Date.Now)
                        cmdupsert.Parameters.AddWithValue("@Billed_By", ThisUser.ID)
                        Try
                            cmdupsert.ExecuteNonQuery()
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        Finally
                            cncd.Close()
                            cncd = Nothing
                        End Try
                        AccPrice += drbcd("Unit") * drbcd("LinePrice")
                        '
                        ExecuteSqlProcedure("Update Req_Billable set Bill_Status = 'B', " &
                        "Billed_On = '" & Date.Now & "', Billed_By = " & ThisUser.ID &
                        " where Accession_ID = " & AccID & " and TGP_ID = " & drbcd("TGP_ID"))
                    End If
                End While
            End If
            cnbcd.Close()
            cnbcd = Nothing
            ExecuteSqlProcedure("Update Req_TGP Set Billed = 1 where Accession_ID = " &
            AccID & " and TGP_ID in (" & ChargeItems & ")")
        End If
        '
        BillChargeDetail = AccPrice
    End Function

    Private Function GetChargeID(ByVal ArType As Byte, ByVal ArID As Long,
    ByVal AccID As Long, ByVal Reason As String, ByVal PreAuth As String) As Long
        Dim ChargeID As Long
        If ArType = 0 Then  'Client
            ChargeID = GetNextCChargeID(AccID)
        ElseIf ArType = 1 Then  'TP
            ChargeID = GetNextTChargeID(AccID)
        Else    'P
            ChargeID = GetNextPChargeID(AccID)
        End If
        Dim BillDate As Date = dtpBillDate.Value
        Dim SvcDate As Date = GetServiceDate(AccID)
        Dim sSQL As String = "If Exists (Select * from Charges where ID = " & ChargeID & ") Update Charges " &
        "Set ArType = " & ArType & ", Ar_ID = " & ArID & ", IsPrimary = 1, Accession_ID = " & AccID & ", " &
        "BillReason = '" & Trim(Reason) & "', PreAuthorization = '" & Trim(PreAuth) & "', Bill_Date = '" &
        BillDate & "', " & "Svc_Date = '" & SvcDate & "', Due_Date = '" & BillDate.AddDays(15) & "', Term = '" &
        "Due upon receipt', Note = '', Output = 0, ECC = " & IIf(ArType = 1, 1, 0) & ", LastEditedOn = '" &
        Date.Now & "', EditedBy = " & ThisUser.ID & " where ID = " & ChargeID & " Else Insert into Charges " &
        "(ID, ArType, IsPrimary, Ar_ID, Accession_ID, BillReason, PreAuthorization, Bill_Date, Svc_Date, " &
        "Due_Date, Term, Note, Output, ECC, LastEditedOn, EditedBy) values (" & ChargeID & ", " & ArType &
        ", 1, " & ArID & ", " & AccID & ", '" & Trim(Reason) & "', '" & Trim(PreAuth) & "', '" & BillDate &
        "', '" & SvcDate & "', '" & BillDate.AddDays(15) & "', 'Due upon receipt', '', 0, " &
        IIf(ArType = 1, 1, 0) & ", '" & Date.Now & "', " & ThisUser.ID & ")"
        ExecuteSqlProcedure(sSQL)
        '
        Return ChargeID
    End Function

    Private Function UpdateCharges(ByVal ChargeID As Long, ByVal ArType As Byte,
    ByVal ArID As Long, ByVal AccID As Long, ByVal Amt As Single) As Single
        Dim Tax As Single = 0 ' Math.Round(Val(txtTax.Text) * Amt / 100, 2)
        Dim sSQL As String = "Update Charges Set NetAmount = " & Math.Round(Amt, 2) & ", TaxAmount = " &
        Tax & ", GrossAmount = " & Math.Round(Tax + Amt, 2) & " where ID = " & ChargeID
        ExecuteSqlProcedure(sSQL)
        '
        If SystemConfig.BARHistory = True Then _
        Log_BAR_Event(AccID, ArType, ArID, 1, ChargeID, Amt + Tax)
        UpdateCharges = Amt + Tax
    End Function

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
            End While
        Else
            ChargeID = NextChargeID()
            ExecuteSqlProcedure("Insert into Accession_Charge (Accession_ID, Charge_ID, " &
            "IsPrimary, ArType, Created_On, Created_By, Edited_On, Edited_By) values (" &
            AccID & ", " & ChargeID & ", 1, 1, '" & Date.Now & "', " & ThisUser.ID &
            ", '" & Date.Now & "', " & ThisUser.ID & ")")
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
        Dim ChargeID As Long = 1
        Dim cncid As New SqlConnection(connString)
        cncid.Open()
        Dim cmdcid As New SqlCommand("Select * from Accession_Charge " &
        "where Accession_ID = " & AccID & " and ArType = 2 and Not Charge_ID " &
        "in (Select ID from Charges where ArType = 2)", cncid)
        cmdcid.CommandType = CommandType.Text
        Dim drcid As SqlDataReader = cmdcid.ExecuteReader
        If drcid.HasRows Then
            While drcid.Read
                If drcid("Charge_ID") IsNot DBNull.Value _
                Then ChargeID = drcid("Charge_ID")
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

    Private Sub txtOutput_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOutput.TextChanged
        If txtOutput.Text <> "" Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim FileName As String = ""
        Dim sw As System.IO.StreamWriter = Nothing
        Try
            With SaveFileDialog1
                .FileName = FileName
                .Filter = "Capture files (*.cap)|*.cap|" & "All files|*.*"
                If .ShowDialog() = DialogResult.OK Then
                    FileName = .FileName
                    sw = New System.IO.StreamWriter(FileName)
                    'using streamwriter to write text from richtextbox and saving it
                    If txtOutput.SelectionLength > 0 Then
                        sw.Write(txtOutput.SelectedText)
                    Else
                        sw.Write(txtOutput.Text)
                    End If
                End If
            End With
        Catch es As Exception
            MessageBox.Show(es.Message)
        Finally
            If Not (sw Is Nothing) Then
                sw.Close()
            End If
        End Try
    End Sub

    Private Function GetDLLName(ByVal PartnerID As Long) As String
        Dim DLL As String = ""
        Dim sSQL As String = "Select CommDLL from Partners where ID = " & PartnerID
        Dim cndll As New SqlConnection(connString)
        cndll.Open()
        Dim cmddll As New SqlCommand(sSQL, cndll)
        cmddll.CommandType = CommandType.Text
        Dim drdll As SqlDataReader = cmddll.ExecuteReader
        If drdll.HasRows Then
            While drdll.Read
                If drdll("CommDLL") IsNot DBNull.Value _
                AndAlso Trim(drdll("CommDLL")) <> "" Then _
                DLL = Trim(drdll("CommDLL"))
            End While
        End If
        cndll.Close()
        cndll = Nothing
        '
        Return DLL
    End Function

    Private Sub UpdateChargePrints(ByVal ChargeID As Long)
        Dim sSQL As String = "If Exists (Select * from ChargePrints where Charge_ID = " & ChargeID &
        ") Update ChargePrints set Print_Date = '" & Date.Now & "', Print_Count = Print_Count + 1 " &
        "where DateDiff(MM, Print_Date, '" & Date.Now & "') > 0 and Charge_ID = " & ChargeID &
        " Else Insert into ChargePrints (Charge_ID, Print_Date, Print_Count) values (" & ChargeID &
        ", '" & Date.Now & "', 1)"
        ExecuteSqlProcedure(sSQL)
    End Sub

    Private Function GetPrintConfigs(ByVal ChargeID As Long) As Boolean
        Dim ToPrint As Boolean = True
        Dim sSQL As String = "Select * from ChargePrints where Charge_ID = " & ChargeID
        Dim cngc As New SqlConnection(connString)
        cngc.Open()
        Dim cmdgc As New SqlCommand(sSQL, cngc)
        cmdgc.CommandType = CommandType.Text
        Dim drgc As SqlDataReader = cmdgc.ExecuteReader
        If drgc.HasRows Then
            While drgc.Read
                If drgc("Print_Count") >= 4 OrElse
                DateDiff(DateInterval.Month, drgc("Print_Date"), Date.Now) < 1 Then ToPrint = False
            End While
        End If
        cngc.Close()
        cngc = Nothing
        Return ToPrint
    End Function

    Private Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Dim BillType As Integer = cmbBillType.SelectedIndex
        Dim ItemX As MyList
        Dim Billees() As String = {""}
        Dim Invoices As String = ""
        Dim Dest() As String = {"", "", ""} '0 = destination, 1 = Printer name, 2 = Track?
        For i As Integer = 0 To lstTargets.Items.Count - 1
            If lstTargets.GetItemChecked(i) = True Then
                ItemX = lstTargets.Items(i)
                If Billees(UBound(Billees)) <> "" Then ReDim Preserve Billees(UBound(Billees) + 1)
                Billees(UBound(Billees)) = ItemX.ItemData.ToString
            End If
        Next
        For i As Integer = 0 To dgvBilled.RowCount - 1
            If CType(dgvBilled.Rows(i).Cells(0).Value, Boolean) = True Then
                Invoices += dgvBilled.Rows(i).Cells(1).Value.ToString & ", "
            End If
        Next
        If Invoices.EndsWith(", ") Then Invoices = Invoices.Substring(0, Len(Invoices) - 2)
        If cmbDestination.SelectedIndex <> -1 Then
            Dest(0) = cmbDestination.SelectedItem.ToString
            If Dest(0).StartsWith("Printer") Then
                If PrintDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    Dest(1) = PrintDialog1.PrinterSettings.PrinterName
                End If
                If cmbBillType.SelectedIndex = 2 AndAlso chkFileless.Checked Then
                    Dest(2) = "1"
                Else
                    Dest(2) = "0"
                End If
            ElseIf Dest(0).StartsWith("837") Then
                ItemX = cmbClearingHouse.SelectedItem
                Dest(1) = ItemX.ItemData.ToString
            ElseIf Dest(0).StartsWith("Email") Then
                'Dest(1) = GetTargetEmail(BillType, Billees)
            End If
        End If
        If BillType <> -1 And Billees(0) <> "" And Invoices <> "" And Dest(0) <> "" Then
            If Not Dest(0).StartsWith("Printer") Then
                ProcessReports(BillType, Billees, Invoices, Dest)
            Else    'Printer
                Dim Wrapper As WorkerArgs = New WorkerArgs
                Wrapper.Task = "Process"
                Wrapper.BillType = cmbBillType.SelectedIndex
                Wrapper.Billees = Billees
                Wrapper.Invoices = Invoices
                Wrapper.Dest = Dest
                BW.RunWorkerAsync(Wrapper)
            End If
        Else
            MsgBox("To process, you need to setup a proper value of the following;" & vbCrLf &
            IIf(Billees(0) = "", "Select atleast one Billee" & vbCrLf, "") &
            IIf(Invoices = "", "Select atleast one Accession" & vbCrLf, "") &
            IIf(Dest(0) = "", "Select the Destination." & vbCrLf, ""), MsgBoxStyle.Critical, "Prolis")
        End If
    End Sub

    Private Sub ProcessReports(ByVal BillType As Integer, ByVal Billees() As String, ByVal Invoices As String, ByVal destination() As String)
        'TODO: Crystal Reports

        'Dim output As String = ""
        'Dim CNT As Integer = 0
        ''Dim FolderPath As String = Path.GetTempPath
        'Dim FolderPath As String = My.Application.Info.DirectoryPath & "\Temp\"
        'If destination(0) = "Screen" Then
        '    'destination  0=screen, 1="", 2=""
        '    'Dim FolderPath As String = My.Application.Info.DirectoryPath & "\Temp\"
        '    If Not Directory.Exists(FolderPath) Then
        '        Directory.CreateDirectory(FolderPath)
        '        Dim UserAccount As String = "everyone" 'Specify the user here
        '        Dim FolderInfo As DirectoryInfo = New DirectoryInfo(FolderPath)
        '        Dim FolderAcl As New DirectorySecurity
        '        FolderAcl.AddAccessRule(New FileSystemAccessRule(UserAccount,
        '        FileSystemRights.Modify, InheritanceFlags.ContainerInherit Or
        '        InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow))
        '        FolderAcl.SetAccessRuleProtection(True, False) 'uncomment to remove existing permissions
        '        FolderInfo.SetAccessControl(FolderAcl)
        '    End If
        'End If
        'Dim pt = FolderPath & Guid.NewGuid.ToString() & " TmpRpt.PDF"
        'Try
        '    For Each FlToDel As String In Directory.GetFiles(FolderPath, "*.*", SearchOption.TopDirectoryOnly)
        '        File.Delete(FlToDel)
        '    Next
        'Catch ex As Exception

        'End Try
        'Dim SPDFS As New List(Of Byte())
        'Dim FPDF As Byte() = Nothing
        'If BillType = 0 Then   'Client
        '    output += "Following Client Invoices proccessed:" & vbCrLf
        '    Dim ClientInfo() As String = {"", ""} '0=ClientInvoiceFile, 1=Email
        '    For i As Integer = 0 To Billees.Length - 1
        '        ClientInfo = GetClientInfo(Billees(i))
        '        If Not ClientInfo(0).Contains("\Reports\") Then ClientInfo(0) =
        '        GetReportPath("" & ClientInfo(0))
        '        If File.Exists(ClientInfo(0)) Then
        '            Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        '            oRpt.Load(ClientInfo(0), CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault)
        '            ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)
        '            oRpt.RecordSelectionFormula = "{Charges.ArType} = 0 and {Charges.Ar_ID} = " &
        '            Billees(i) & " And {Charges.ID} in [" & Invoices & "];"
        '            If destination(0) = "Printer" Then  'destination  0=Printer, 1=PrinterName, 2=""
        '                oRpt.PrintOptions.PrinterName = destination(1)
        '                oRpt.PrintToPrinter(1, True, 0, 0)
        '                CNT += 1
        '            ElseIf destination(0) = "Screen" Then
        '                'Dim S As New MemoryStream
        '                'S = oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat)
        '                'SPDFS.Add(S.ToArray)
        '                'S.Close()
        '                'S = Nothing

        '                Using ms As New MemoryStream()
        '                    Try
        '                        oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat).CopyTo(ms)
        '                        ms.Position = 0
        '                        SPDFS.Add(ms.ToArray())
        '                    Catch ex As Exception
        '                        Dim err As String = ex.Message()
        '                    End Try
        '                End Using

        '                CNT += 1
        '            ElseIf destination(0) = "Email" Then
        '                If ClientInfo(1) <> "" Then
        '                    'Dim S As New MemoryStream
        '                    'S = oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat)
        '                    'SPDFS.Add(S.ToArray)
        '                    'S.Close()
        '                    'S = Nothing

        '                    Using ms As New MemoryStream()
        '                        Try
        '                            oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat).CopyTo(ms)
        '                            ms.Position = 0
        '                            SPDFS.Add(ms.ToArray())
        '                        Catch ex As Exception
        '                            Dim err As String = ex.Message()
        '                        End Try
        '                    End Using

        '                    CNT += 1
        '                Else
        '                    output += "Client not configured with a valid Email ID. So, the report can not be emailed:" & vbCrLf
        '                End If
        '            End If
        '            oRpt.Close()
        '            oRpt = Nothing
        '        Else
        '            output += "The report file '" & ClientInfo(0) & "' missing in Prolis!" & vbCrLf
        '        End If
        '    Next
        '    If SPDFS.Count > 0 Then
        '        FPDF = PdfHelper.MergePDFStreams(SPDFS)


        '        '
        '        'For Each FlToDel As String In Directory.GetFiles(FolderPath, "*.*", SearchOption.TopDirectoryOnly)
        '        '    File.Delete(FlToDel)
        '        'Next
        '        '
        '        Dim ms As New FileStream(pt, FileMode.Create, FileAccess.ReadWrite, FileShare.Delete)
        '        ms.Write(FPDF, 0, FPDF.Length)
        '        ms.Close()
        '        ms = Nothing
        '        '
        '        If destination(0) = "Screen" Then




        '            Dim f As New frmWebView()
        '            f.ShowFromPath(pt)
        '            f.MdiParent = frmDashboard
        '            f.Show()

        '        ElseIf destination(0) = "Email" AndAlso ClientInfo(1) <> "" Then 'email report

        '        End If
        '    End If
        'ElseIf BillType = 1 Then   'TP
        '    output += "Following Insurance Invoices proccessed:" & vbCrLf
        '    Dim InvIDs() As String = Split(Invoices, ", ")
        '    If destination(0).StartsWith("837") Then    '837 file
        '        'destination  0=837, 1=PartnerID, 2=File to save
        '        Dim FC As Object
        '        Dim ToFile As String = ""
        '        Dim Path As String = My.Application.Info.DirectoryPath & "\"
        '        '  Path = "C:\Users\hp\Documents\GitHub\Prolis_Interfaces\Prolis_DLL_Interfaces\ProlisEmdeon\ProlisEmdeon\bin\Debug\"
        '        Dim DLL As String = Path & GetDLLName(destination(1))
        '        If DLL <> "" Then
        '            Dim Retvals() As String
        '            Dim Plugin As System.Reflection.Assembly =
        '            System.Reflection.Assembly.LoadFrom(DLL)
        '            'Dim CLS As String = Plugin.GetName & ".ANSI837"
        '            'FC = CreateObject("ProlisAvaility500.ANSI837")
        '            FC = Plugin.CreateInstance(Plugin.GetTypes(5).FullName)
        '            'FC = Plugin.CreateInstance("ProlisAvaility500.ANSI837")
        '            Retvals = FC.InvoicesTo837(connStringS, destination(1), InvIDs, True)


        '            FC = Nothing
        '            CNT = InvIDs.Length
        '            If SystemConfig.BARHistory = True Then
        '                Dim VALS() As String
        '                For i As Integer = 0 To Retvals.Length - 1
        '                    VALS = GetBARValuesB(Val(Retvals(i)))
        '                    '0=AccID, 1=ArType, 2=ArID, 3=1, 4=InvID, 5=Amount
        '                    Log_BAR_Event(Val(Retvals(i)), Val(VALS(1)), Val(VALS(2)), 5, "837", Val(VALS(5)))
        '                Next
        '            End If
        '            '
        '            If SystemConfig.AuditTrail = True Then
        '                LogUserEvent(ThisUser.ID, 37, Date.Now.ToString, "837", 0, Invoices, ToFile)
        '            End If
        '            '
        '            Dim Missed As String = ""
        '            If Retvals(0) <> "" AndAlso Retvals.Length > 1 Then
        '                output += "Prolis generated following 837 file containing " & (Retvals.Length - 1).ToString & " claims." &
        '                vbCrLf & Retvals(0) & vbCrLf
        '                If IsValidFilePath(Retvals(0)) Then
        '                    claimpath.Text = Retvals(0)
        '                    viewClaim.Show()
        '                Else
        '                    viewClaim.Hide()
        '                End If

        '                If InvIDs.Length = Retvals.Length - 1 Then  'all transfered
        '                    output += "File contains all 100% scheduled claims, listed below:" & vbCrLf
        '                    output += Invoices & vbCrLf
        '                Else
        '                    For n As Integer = 0 To InvIDs.Length - 1
        '                        If Not TESTinTESTS(InvIDs(n), Retvals) Then
        '                            Missed += InvIDs(n) & ", "
        '                        End If
        '                    Next
        '                    If Missed.EndsWith(", ") Then Missed = Microsoft.VisualBasic.Mid(Missed, 1, Len(Missed) - 2)
        '                    If Missed <> "" Then
        '                        output += "Process was scheduled for " & InvIDs.Length.ToString & " claims but following claims " &
        '                        "could not be transfered. Print this list and use the Editor to rectify the issue." & vbCrLf &
        '                        "*********************" & vbCrLf
        '                        ToFile += Missed & vbCrLf & "*********************" & vbCrLf
        '                    End If
        '                End If
        '            End If
        '        Else
        '            output = "837 Builder DLL is missing. Contact Prolis Support"
        '        End If
        '    Else    'Printer or screen
        '        Dim RPTFile As String = ""

        '        'For i As Integer = 0 To Billees.Length - 1
        '        'RPTFile = GetPayerRPTFile(Billees(i))

        '        For n As Integer = 0 To InvIDs.Length - 1
        '            'If Not RPTFile.Contains("\Reports\") Then RPTFile = GetReportPath(RPTFile)
        '            RPTFile = GetPayerRPTFileByInvoice(InvIDs(n))

        '            RPTFile = GetReportPath(RPTFile)
        '            If Not File.Exists(RPTFile) Then

        '                MessageBox.Show(RPTFile & " Not Found.")
        '                Return
        '            End If

        '            Dim accID As String = GetAccIDfromChargeID(InvIDs(n))

        '            Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        '            oRpt.Load(RPTFile, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault)
        '            ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)

        '            oRpt.SetParameterValue("AccessionID", accID)

        '            If destination(0) = "Printer" Then
        '                oRpt.PrintOptions.PrinterName = destination(1)
        '                oRpt.PrintToPrinter(1, True, 0, 0)
        '                CNT += 1
        '                output += InvIDs(n) & vbCrLf
        '            ElseIf destination(0) = "Screen" Then
        '                'Dim S As New MemoryStream
        '                'S = oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat)
        '                'SPDFS.Add(S.ToArray)
        '                'S.Close()
        '                'S = Nothing

        '                Using ms As New MemoryStream()
        '                    Try
        '                        oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat).CopyTo(ms)
        '                        ms.Position = 0
        '                        SPDFS.Add(ms.ToArray())
        '                    Catch ex As Exception
        '                        Dim err As String = ex.Message()
        '                    End Try
        '                End Using

        '                CNT += 1
        '                output += InvIDs(n) & vbCrLf
        '            End If
        '            oRpt.Close()
        '            oRpt = Nothing
        '        Next
        '        'End If
        '        'Next
        '        If SPDFS.Count > 0 Then
        '            FPDF = PdfHelper.MergePDFStreams(SPDFS)
        '            '


        '            '
        '            Dim ms As New FileStream(pt, FileMode.Create, FileAccess.ReadWrite, FileShare.Delete)
        '            ms.Write(FPDF, 0, FPDF.Length)
        '            ms.Close()
        '            ms = Nothing

        '            Dim f As New frmWebView()
        '            f.ShowFromStream(FPDF)
        '            f.MdiParent = frmDashboard
        '            f.Show()




        '        End If
        '    End If
        'Else    'Patient
        '    output += "Following Patient Invoices proccessed:" & vbCrLf
        '    Dim InvIDs() As String = Split(Invoices, ", ")
        '    Dim RPTFile As String = GetReportPath("Patient Invoice.rpt")
        '    If File.Exists(RPTFile) Then
        '        For i As Integer = 0 To InvIDs.Length - 1
        '            Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        '            oRpt.Load(RPTFile, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault)
        '            ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)
        '            oRpt.RecordSelectionFormula = "(if {Charges.IsPrimary} = False then " &
        '            "{Charges_1.ArType} = 1 else {Charges_1.ArType} = 2;) and {Charges.ID} = " &
        '            InvIDs(i) & ";"
        '            If destination(0) = "Printer" Then  'destination  0=Printer, 1=PrinterName, 2=""
        '                If destination(2) = "1" Then   'tracked
        '                    If GetPrintConfigs(InvIDs(i)) Then
        '                        UpdateChargePrints(InvIDs(i))

        '                        Try
        '                            oRpt.PrintOptions.PrinterName = destination(1)

        '                            CNT += 1
        '                            output += InvIDs(i) & " printed and tracked" & vbCrLf
        '                        Catch ex As Exception
        '                            MessageBox.Show("Default printer is not set.")
        '                            Return
        '                        End Try

        '                    Else
        '                        output += InvIDs(i) & " invoice does not qualify for patient invoice printing" & vbCrLf
        '                    End If
        '                Else    'not tracked
        '                    oRpt.PrintOptions.PrinterName = destination(1)
        '                    oRpt.PrintToPrinter(1, True, 0, 0)
        '                    CNT += 1
        '                    output += InvIDs(i) & " printed" & vbCrLf
        '                End If
        '            ElseIf destination(0) = "Screen" Then
        '                'Dim S As New MemoryStream
        '                'S = oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat)
        '                'SPDFS.Add(S.ToArray)
        '                'S.Close()
        '                'S = Nothing

        '                Using ms As New MemoryStream()
        '                    Try
        '                        oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat).CopyTo(ms)
        '                        ms.Position = 0
        '                        SPDFS.Add(ms.ToArray())
        '                    Catch ex As Exception
        '                        Dim err As String = ex.Message()
        '                    End Try
        '                End Using


        '                CNT += 1
        '            ElseIf destination(0) = "Email" Then
        '                Dim PatEmail As String = GetPatientEmailFromInvoice(InvIDs(i))
        '                If PatEmail <> "" Then
        '                    'Dim S As New MemoryStream
        '                    'S = oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat)
        '                    ''SPDFS.Add(S.ToArray)
        '                    'S.Close()
        '                    'S = Nothing
        '                    ' email report (S)

        '                    Using ms As New MemoryStream()
        '                        Try
        '                            oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat).CopyTo(ms)
        '                            ms.Position = 0
        '                            'SPDFS.Add(ms.ToArray())
        '                        Catch ex As Exception
        '                            Dim err As String = ex.Message()
        '                        End Try
        '                    End Using

        '                End If
        '            End If
        '            oRpt.Close()
        '            oRpt = Nothing
        '        Next
        '        If SPDFS.Count > 0 Then
        '            FPDF = PdfHelper.MergePDFStreams(SPDFS)
        '            Try
        '                For Each FlToDel As String In Directory.GetFiles(FolderPath, "*.*", SearchOption.TopDirectoryOnly)
        '                    File.Delete(FlToDel)
        '                Next
        '            Catch ex As Exception

        '            End Try

        '            '
        '            Dim ms As New FileStream(pt, FileMode.Create, FileAccess.ReadWrite, FileShare.Delete)
        '            ms.Write(FPDF, 0, FPDF.Length)
        '            ms.Close()
        '            ms = Nothing
        '            Dim f As New frmWebView()
        '            f.ShowFromStream(FPDF)
        '            f.MdiParent = frmDashboard
        '            f.Show()


        '        End If
        '    End If
        'End If
        'If destination(0).StartsWith("Printer") Then
        '    output += "Destination: Printer" & vbCrLf
        'ElseIf destination(0).StartsWith("Screen") Then
        '    output += "Destination: Screen" & vbCrLf
        'ElseIf destination(0).StartsWith("837") Then
        '    output += "Destination: 837 File" & vbCrLf
        'Else
        '    output += "Destination: Email" & vbCrLf
        'End If
        ''
        'output += "------------------------------" & vbCrLf
        'output += "Total Invoices processed: " & CNT.ToString & vbCrLf
        'DisplayOutput(output)

    End Sub

    Private Function GetPayerRPTFile(ByVal PayerID As String) As String
        Dim Info As String = "HCFA1500.rpt"
        Dim sSql As String = "Select IsNull(DocFile, '') as RPTFile, " &
        "IsNull(Email, '') as Email from Payers where ID = " & PayerID
        Dim cngci As New SqlConnection(connString)
        cngci.Open()
        Dim cmdgci As New SqlCommand(sSql, cngci)
        cmdgci.CommandType = CommandType.Text
        Dim drgci As SqlDataReader = cmdgci.ExecuteReader
        If drgci.HasRows Then
            While drgci.Read
                If Trim(drgci("RPTFile")) <> "" Then Info = Trim(drgci("RPTFile"))
            End While
        End If
        cngci.Close()
        cngci = Nothing
        Return Info
    End Function

    Private Function GetPayerRPTFileByInvoice(ByVal invoice As String) As String
        Dim Info As String = "HCFA1500.rpt"
        Dim sSql As String = $"Select IsNull(DocFile, '') as RPTFile, IsNull(Email, '') as Email from Payers where ID = ( select Ar_ID from Charges where id = {invoice})"
        Dim cngci As New SqlConnection(connString)
        cngci.Open()
        Dim cmdgci As New SqlCommand(sSql, cngci)
        cmdgci.CommandType = CommandType.Text
        Dim drgci As SqlDataReader = cmdgci.ExecuteReader
        If drgci.HasRows Then
            While drgci.Read
                If Trim(drgci("RPTFile")) <> "" Then Info = Trim(drgci("RPTFile"))
            End While
        End If
        cngci.Close()
        cngci = Nothing
        Return Info
    End Function

    Private Function GetClientInfo(ByVal ClientID As String) As String()
        Dim Info() As String = {"", ""}
        Dim sSql As String = "Select IsNull(InvoiceRPTFile, '') as RPTFile, " &
        "IsNull(Email, '') as Email from Providers where ID = " & ClientID
        Dim cngci As New SqlConnection(connString)
        cngci.Open()
        Dim cmdgci As New SqlCommand(sSql, cngci)
        cmdgci.CommandType = CommandType.Text
        Dim drgci As SqlDataReader = cmdgci.ExecuteReader
        If drgci.HasRows Then
            While drgci.Read
                If Trim(drgci("RPTFile")) <> "" Then
                    Info(0) = Trim(drgci("RPTFile"))
                Else
                    Info(0) = "Provider Invoice.rpt"
                End If
                Info(1) = Trim(drgci("Email"))
            End While
        End If
        cngci.Close()
        cngci = Nothing
        Return Info
    End Function

    Private Sub btnReverse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Dim AccIDs() As String = {""}
        For i As Integer = 0 To dgvBilled.RowCount - 1
            If CType(dgvBilled.Rows(i).Cells(0).Value, Boolean) = True Then
                If AccIDs(UBound(AccIDs)) <> "" Then ReDim Preserve AccIDs(UBound(AccIDs) + 1)
                AccIDs(UBound(AccIDs)) = dgvBilled.Rows(i).Cells(2).Value.ToString
            End If
        Next
        If AccIDs(0) <> "" Then
            Dim Wrapper As WorkerArgs = New WorkerArgs
            Wrapper.Task = "Reverse"
            Wrapper.Accessions = AccIDs
            BW.RunWorkerAsync(Wrapper)
        End If
        'BW.RunWorkerAsync(AccIDs)
    End Sub

    Private Sub ReverseBilling(ByVal AccIDs() As String)
        Dim Output As String = ""
        Dim RevAmt As Single = 0
        Dim TGPIDs As String = ""
        Dim CNT As Integer = 0
        Dim ChargeIDs As String = ""
        Dim AccBillInfo() As String = {"", ""}
        For n As Integer = 0 To AccIDs.Length - 1
            Dim BARVALS() As String = GetBARValuesB(AccIDs(n))
            '0=AccID, 1=ArType, 2=ArID, 3=1, 4=InvID, 5=Amount
            TGPIDs = GetBilledTGPIDS(AccIDs(n))
            If TGPIDs <> "" Then
                ExecuteSqlProcedure("Update Req_TGP Set Billed = 0 where Accession_ID = " &
                AccIDs(n) & " and TGP_ID in (" & TGPIDs & ")")
                '
                ExecuteSqlProcedure("Update Req_Billable Set Bill_Status = 'U', Billed_On = " &
                "NULL, Billed_By = NULL where Accession_ID = " & AccIDs(n) _
                & " and TGP_ID in (" & TGPIDs & ")")
                '
                ChargeIDs = GetAccTGPInvoiceID(AccIDs(n), TGPIDs)
                If ChargeIDs.EndsWith(", ") Then ChargeIDs = ChargeIDs.Substring(0, Len(ChargeIDs) - 2)
                ExecuteSqlProcedure("Delete from Charge_Detail where Charge_ID in (" &
                ChargeIDs & ") and TGP_ID in (" & TGPIDs & ")")
                '
                ExecuteSqlProcedure("Delete from Charge_Detail where Charge_ID in (" & ChargeIDs & ")")
                ExecuteSqlProcedure("Delete from Charges where Accession_ID = " & AccIDs(n))
                '
                TGPIDs = "" : ChargeIDs = ""
                If SystemConfig.BARHistory = True Then Log_BAR_Event(Val(BARVALS(0)),
                Val(BARVALS(1)), Val(BARVALS(2)), 2, Val(BARVALS(4)), Val(BARVALS(5)))
                RevAmt += Val(BARVALS(5))
                Output += BARVALS(0) & " with value $ " & Format(Val(BARVALS(5)), "0.00") & " reversed."
                CNT += 1
            End If
            BW.ReportProgress(CInt((n + 1) * 100 / AccIDs.Length), CStr(CInt((n + 1) * 100 / AccIDs.Length)) & " %")
        Next
        Output += "----------------------------" & vbCrLf & CNT.ToString &
        " accession(s), value $ " & Format(RevAmt, "0.00") & " reversed."
        DisplayOutput(Output)
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

    Private Function GetBilledTGPIDS(ByVal AccID As String) As String
        Dim TGPIDS As String = ""
        Dim sSQL = "Select TGP_ID from Req_Billable where Bill_Status = 'B' and Accession_ID = " & AccID &
        " Union Select TGP_ID from Charge_Detail where Charge_ID in (Select ID from Charges where Accession_ID = " & AccID & ")"
        Dim cngb As New SqlConnection(connString)
        cngb.Open()
        Dim cmdgb As New SqlCommand(sSQL, cngb)
        cmdgb.CommandType = CommandType.Text
        Dim drgb As SqlDataReader = cmdgb.ExecuteReader
        If drgb.HasRows Then
            While drgb.Read
                TGPIDS += drgb("TGP_ID").ToString & ", "
            End While
        End If
        cngb.Close()
        cngb = Nothing
        If TGPIDS.EndsWith(", ") Then TGPIDS = TGPIDS.Substring(0, Len(TGPIDS) - 2)
        Return TGPIDS
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If BW.IsBusy Then BW.CancelAsync()
    End Sub

    Private Sub cmbDestination_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDestination.SelectedIndexChanged
        If cmbDestination.SelectedItem.ToString.StartsWith("837") Then
            PopulateClearingHouses()
            If cmbClearingHouse.Items.Count = 1 Then
                cmbClearingHouse.SelectedIndex = 0
            ElseIf cmbClearingHouse.Items.Count > 1 Then
                For i As Integer = 0 To cmbClearingHouse.Items.Count - 1
                    If cmbClearingHouse.Items(i).ToString.StartsWith("Ability") Then
                        cmbClearingHouse.SelectedIndex = i
                        Exit For
                    End If
                Next
            End If
            cmbClearingHouse.Enabled = True
        Else
            cmbClearingHouse.SelectedIndex = -1
            cmbClearingHouse.Enabled = False
        End If
    End Sub

    Private Sub PopulateClearingHouses()
        cmbClearingHouse.Items.Clear()
        Dim sSQL = "Select * from Partners where Active <> 0 order by Name"
        Dim cnch As New SqlConnection(connString)
        cnch.Open()
        Dim cmdch As New SqlCommand(sSQL, cnch)
        cmdch.CommandType = CommandType.Text
        Dim drch As SqlDataReader = cmdch.ExecuteReader
        If drch.HasRows Then
            While drch.Read
                cmbClearingHouse.Items.Add(New MyList(drch("Name"), drch("ID")))
            End While
        End If
        cnch.Close()
        cnch = Nothing
    End Sub

    Private Sub chkFileless_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFileless.CheckedChanged
        dgvBilled.Rows.Clear()
        If chkFileless.Checked = True Then
            chkFileless.Text = "Unprocessed"
        Else
            chkFileless.Text = "All"
        End If
    End Sub

    Private Sub dgvProcessed_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProcTPs.CellContentClick
        UpdateProcessCommands()
    End Sub

    Private Sub UpdateProcessCommands()
        If cmbBillType.SelectedIndex = 1 Then
            Dim CNT As Integer = 0
            For i As Integer = 0 To dgvProcTPs.RowCount - 1
                If CType(dgvProcTPs.Rows(i).Cells(0).Value, Boolean) = True Then CNT += 1
            Next
            If CNT = 1 Then
                btnUnprocess.Enabled = True
                btnFileOutput.Enabled = True
            Else
                btnUnprocess.Enabled = False
                btnFileOutput.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnUnprocess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnprocess.Click
        Dim RetVal As Integer = MsgBox("Do you have a good reason to reverse this " &
        "information of 837 file?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
        If RetVal = vbYes Then
            For i As Integer = 0 To dgvProcTPs.RowCount - 1
                If CType(dgvProcTPs.Rows(i).Cells(0).Value, Boolean) = True Then
                    ExecuteSqlProcedure("Update Charges set Output = 0 where ID in (Select Charge_ID " &
                    "from ChargesEOutput where FileNo = " & dgvProcTPs.Rows(i).Cells(1).Value & ")")
                    '
                    ExecuteSqlProcedure("Delete from ChargesEOutput where FileNo = " & dgvProcTPs.Rows(i).Cells(1).Value)
                End If
            Next
        End If
        btnLoad_Click(Nothing, Nothing)
        UpdateProcessCommands()
    End Sub

    Private Sub btnFileOutput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFileOutput.Click
        Dim Output As String = ""
        For i As Integer = 0 To dgvProcTPs.RowCount - 1
            If CType(dgvProcTPs.Rows(i).Cells(0).Value, Boolean) = True Then
                Output = "The File: " & dgvProcTPs.Rows(i).Cells(1).Value.ToString &
                " containing " & dgvProcTPs.Rows(i).Cells(4).Value & " following invoices;" & vbCrLf
                Output += "--------------------------------------" & vbCrLf
                Dim sSQL = "Select a.*, c.Accession_ID, b.Name as Payer from Partners b inner join " &
                "(ChargesEOutput a inner join Charges c on c.ID = a.Charge_ID) on a.Partner_ID = b.ID " &
                "where a.FileNo = " & dgvProcTPs.Rows(i).Cells(1).Value
                Dim cnfo As New SqlConnection(connString)
                cnfo.Open()
                Dim cmdfo As New SqlCommand(sSQL, cnfo)
                cmdfo.CommandType = CommandType.Text
                Dim drfo As SqlDataReader = cmdfo.ExecuteReader
                If drfo.HasRows Then
                    While drfo.Read
                        Output += drfo("Accession_ID").ToString & Chr(9) & drfo("Charge_ID").ToString &
                        Chr(9) & Format(drfo("Amount"), "0.00") & Chr(9) & drfo("Payer") & vbCrLf
                    End While
                End If
                cnfo.Close()
                cnfo = Nothing
                '
                DisplayOutput(Output)
                Exit For
            End If
        Next
    End Sub

    Private Sub btnSelP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelP.Click
        If cmbBillType.SelectedIndex = 1 Then
            For i As Integer = 0 To dgvProcTPs.RowCount - 1
                dgvProcTPs.Rows(i).Cells(0).Value = True
            Next
            UpdateProcessCommands()
        ElseIf cmbBillType.SelectedIndex = 2 Then
            For i As Integer = 0 To dgvProcPats.RowCount - 1
                dgvProcPats.Rows(i).Cells(0).Value = True
            Next
            UpdateProcessCommands()
        End If

    End Sub

    Private Sub btnDeselP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeselP.Click
        If cmbBillType.SelectedIndex = 1 Then
            For i As Integer = 0 To dgvProcTPs.RowCount - 1
                dgvProcTPs.Rows(i).Cells(0).Value = False
            Next
            UpdateProcessCommands()
        ElseIf cmbBillType.SelectedIndex = 2 Then
            For i As Integer = 0 To dgvProcPats.RowCount - 1
                dgvProcPats.Rows(i).Cells(0).Value = False
            Next
            UpdateProcessCommands()
        End If

    End Sub

    Private Sub btn837Lookup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn837Lookup.Click
        If cmbBillType.SelectedIndex = 1 Then
            If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                txt837File.Text = OpenFileDialog1.FileName
                lstTargets.Items.Clear()
                dgvDiscrete.Rows.Clear()
                dgvDiscrete.RowCount = 1
                txtAccFrom.Text = ""
                txtAccTo.Text = ""
                'txtDateFrom.Text = ""
                'txtDateTo.Text = ""

                ClearDateTimePicker(dtpDateFrom)
                ClearDateTimePicker(dtpDateTo)
                TB.TabPages(2).Select()
                chkFileless.Checked = False
            Else
                txt837File.Text = ""
            End If
        End If
    End Sub

    Private Function SubscriberAddExists(Accessions As String) As Boolean
        Dim cnfo As New SqlConnection(connString)
        Dim rslt = False
        cnfo.Open()
        Dim sSQL = "select * from Addresses where id in(select address_id  from Patients where id in( select Insured_ID from Req_Coverage where Accession_ID= " & Accessions & " and Preference='P'))"
        Dim cmdfo As New SqlCommand(sSQL, cnfo)
        cmdfo.CommandType = CommandType.Text
        Dim drfo As SqlDataReader = cmdfo.ExecuteReader
        If drfo.HasRows Then
            rslt = True
        End If
        cnfo.Close()
        cnfo = Nothing
        Return rslt
    End Function


    Private Sub dgvUnsynchAccs_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUnsynchAccs.CellDoubleClick


        If e.RowIndex <> -1 Then

            Try
                Clipboard.SetText(dgvUnsynchAccs.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                clipboardMsg.ForeColor = Color.Red
                clipboardMsg.Text = dgvUnsynchAccs.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & ""
            Catch ex As Exception

            End Try
        End If

    End Sub

    Private Sub dgvSynchAccs_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSynchAccs.CellDoubleClick
        If e.RowIndex <> -1 Then

            Try
                Clipboard.SetText(dgvSynchAccs.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                clipboardMsg.ForeColor = Color.Red
                clipboardMsg.Text = dgvSynchAccs.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & ""
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub dgvBilled_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvBilled.CellDoubleClick
        If e.RowIndex <> -1 Then
            Try
                Clipboard.SetText(dgvBilled.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                clipboardMsg.ForeColor = Color.Red
                clipboardMsg.Text = dgvBilled.Rows(e.RowIndex).Cells(e.ColumnIndex).Value

                frmBillingEdit.Close()

                frmBillingEdit.Show()
                ' frmBillingEdit.MdiParent = frmDashboard
                frmBillingEdit.dgvDiscrete.Rows.Clear()
                frmBillingEdit.dgvDiscrete.Rows.Add(clipboardMsg.Text)
                frmBillingEdit.cmbABU.SelectedIndex = 0
                frmBillingEdit.btnTarget.PerformClick()
                frmBillingEdit.btnLoad.PerformClick()

            Catch ex As Exception

            End Try
            Try
                Clipboard.SetText(dgvBilled.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                clipboardMsg.ForeColor = Color.Red
                clipboardMsg.Text = dgvBilled.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & ""
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub dgvProcPats_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvProcPats.CellDoubleClick
        If e.RowIndex <> -1 Then

            Try
                Clipboard.SetText(dgvProcPats.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                clipboardMsg.ForeColor = Color.Red
                clipboardMsg.Text = dgvProcPats.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & ""
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub btnPatLook_Click(sender As Object, e As EventArgs) Handles btnPatLook.Click

        Dim PatientID As String = frmPatLookUp.ShowDialog()
        If PatientID <> "" Then
            txtPatientID.Text = (Val(PatientID))
            'PopulatePatientDxs(PatientID)
        End If



    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        SaveFileDialog1.Filter = "CSV Files (*.csv)|*.csv"
        SaveFileDialog1.Title = "Save CSV File"
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            ExportToCSV(dgvUnsynchAccs, SaveFileDialog1.FileName)
        End If
    End Sub
    Private Sub ExportToCSV(dataGridView As DataGridView, fileName As String)
        ' Create a StreamWriter to write to the CSV file
        Using writer As New StreamWriter(fileName)
            ' Write the column headers
            For i As Integer = 0 To dataGridView.Columns.Count - 1
                writer.Write(dataGridView.Columns(i).HeaderText)
                If i < dataGridView.Columns.Count - 1 Then
                    writer.Write(",")
                End If
            Next
            writer.WriteLine()

            ' Write the data rows
            For i As Integer = 0 To dataGridView.Rows.Count - 1
                For j As Integer = 0 To dataGridView.Columns.Count - 1
                    writer.Write(dataGridView.Rows(i).Cells(j).Value)
                    If j < dataGridView.Columns.Count - 1 Then
                        writer.Write(",")
                    End If
                Next
                writer.WriteLine()
            Next
        End Using

        MessageBox.Show("Data exported to " & fileName, "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        SaveFileDialog1.Filter = "CSV Files (*.csv)|*.csv"
        SaveFileDialog1.Title = "Save CSV File"
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            ExportToCSV(dgvSynchAccs, SaveFileDialog1.FileName)
        End If
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        SaveFileDialog1.Filter = "CSV Files (*.csv)|*.csv"
        SaveFileDialog1.Title = "Save CSV File"
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            ExportToCSV(dgvBilled, SaveFileDialog1.FileName)
        End If
    End Sub

    Private Sub cmbBillType_Click(sender As Object, e As EventArgs) Handles cmbBillType.Click

    End Sub

    Private Sub dgvBilled_TabIndexChanged(sender As Object, e As EventArgs) Handles dgvBilled.TabIndexChanged

    End Sub

    Private Sub TB_TabIndexChanged(sender As Object, e As EventArgs) Handles TB.TabIndexChanged
        Dim dd = 0
    End Sub

    Private Sub viewClaim_Click(sender As Object, e As EventArgs) Handles viewClaim.Click

        frmHTML_VIEW.Close()
        frmHTML_VIEW.ERA_path_or_content = claimpath.Text
        frmHTML_VIEW.Show()
    End Sub

    Private Sub dgvBilled_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvBilled.CellClick
        Try
            Clipboard.SetText(dgvBilled.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
            clipboardMsg.ForeColor = Color.Red
            clipboardMsg.Text = dgvBilled.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & ""
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgvProcPats_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvProcPats.CellClick
        Try
            Clipboard.SetText(dgvProcPats.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
            clipboardMsg.ForeColor = Color.Red
            clipboardMsg.Text = dgvProcPats.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & ""
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgvSynchAccs_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSynchAccs.CellClick
        Try
            Clipboard.SetText(dgvSynchAccs.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
            clipboardMsg.ForeColor = Color.Red
            clipboardMsg.Text = dgvSynchAccs.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & ""
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgvUnsynchAccs_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUnsynchAccs.CellClick
        Try
            Clipboard.SetText(dgvUnsynchAccs.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
            clipboardMsg.ForeColor = Color.Red
            clipboardMsg.Text = dgvUnsynchAccs.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & ""
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dtpDateFrom_CloseUp(sender As Object, e As EventArgs) Handles dtpDateFrom.CloseUp
        ' After selecting a valid date, revert to the standard date format
        CloseUpDateTimePicker(dtpDateFrom)
    End Sub
    Private Sub dtpDateTo_CloseUp(sender As Object, e As EventArgs) Handles dtpDateTo.CloseUp
        CloseUpDateTimePicker(dtpDateTo)
    End Sub

    Private Sub lblClearDates_Click(sender As Object, e As EventArgs) Handles lblClearDates.Click
        ClearDateTimePicker(dtpDateFrom)
        ClearDateTimePicker(dtpDateTo)
    End Sub
End Class