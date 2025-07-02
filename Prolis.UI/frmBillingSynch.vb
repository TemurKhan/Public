Option Compare Text
Imports System.Windows.Forms
Imports System.Data

Public Class frmBillingSynch
    Private IsRunning As Boolean
    Private sSQL As String
    Private IgnoreResults As Boolean
    Private EXCEP As String

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If BW.IsBusy Then
            Dim RetVal As Integer = MsgBox("Data Synchronization in progress. " &
            "Do you want to cancel the process?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
            If RetVal = vbYes Then
                BW.CancelAsync()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        sSQL = "Select ID from Requisitions where Received <> 0 and Rejected = 0"
        Dim i As Integer
        EXCEP = ""
        IgnoreResults = Not chkResults.Checked
        Dim ItemX As MyList
        If cmbBillType.SelectedIndex = 0 Then   'Client
            sSQL += " and BillingType_ID = 0"
        ElseIf cmbBillType.SelectedIndex = 1 Then   'TP
            sSQL += " and BillingType_ID = 1"
        ElseIf cmbBillType.SelectedIndex = 2 Then   'Patient
            sSQL += " and BillingType_ID = 2"
        End If
        If chkSpecAll.Checked = True Then   'All
            sSQL += " and Not ID in (Select distinct Accession_ID from " & _
            "Req_Billable where Bill_Status = 'B')"
        Else                                'specific
            If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                sSQL += " and AccessionDate between '" & dtpDateFrom.Text & "' and '" &
                dtpDateFrom.Text & " 23:59:00' and not ID in (Select distinct " &
                "Accession_ID from Req_Billable where Bill_Status = 'B')"
            ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                sSQL += " and AccessionDate between '" & dtpDateTo.Text & "' and '" &
                dtpDateTo.Text & " 23:59:00' and not ID in (Select distinct " &
                "Accession_ID from Req_Billable where Bill_Status = 'B')"
            ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                sSQL += " and AccessionDate between '" & dtpDateFrom.Text & "' and '" &
                dtpDateTo.Text & " 23:59:00' and not ID in (Select distinct " &
                "Accession_ID from Req_Billable where Bill_Status = 'B')"
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                sSQL += " and not ID in (Select distinct Accession_ID from Req_Billable where " &
                "Bill_Status = 'B') and ID = " & Val(txtAccFrom.Text)
            ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                sSQL += " and not ID in (Select distinct Accession_ID from Req_Billable where " &
                "Bill_Status = 'B') and ID = " & Val(txtAccTo.Text)
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                sSQL += " and not ID in (Select distinct Accession_ID from Req_Billable where " &
                "Bill_Status = 'B') and ID between " & Val(txtAccFrom.Text) & " and " &
                Val(txtAccTo.Text)
            ElseIf dgvDiscrete.RowCount > 1 AndAlso GetDiscreteAccIDs() <> "" Then
                sSQL += " and not ID in (Select distinct Accession_ID from Req_Billable where " &
                "Bill_Status = 'B') and ID in (" & GetDiscreteAccIDs() & ")"
            End If
        End If
        '
        Dim Targets As String = ""
        For i = 0 To lstTargets.Items.Count - 1
            If cmbBillType.SelectedIndex <> 3 Then
                If lstTargets.GetItemChecked(i) = True Then
                    ItemX = lstTargets.Items(i)
                    Targets += ItemX.ItemData.ToString & ", "
                End If
            Else
                Targets = "*"
            End If
        Next
        If Targets.EndsWith(", ") Then Targets = Microsoft.VisualBasic.Mid(Targets, 1, Len(Targets) - 2)
        '
        If Targets = "" Then    'kill every thing
            sSQL = ""
        Else
            If cmbBillType.SelectedIndex = 0 Then   'Client
                sSQL += " and OrderingProvider_ID in (" & Targets & ")"
            ElseIf cmbBillType.SelectedIndex = 1 Then   'TP
                sSQL += " and ID in (Select Accession_ID from Req_Coverage where " &
                "Preference = 'P' and Payer_ID in (" & Targets & "))"
            ElseIf cmbBillType.SelectedIndex = 2 Then   'Patient
                sSQL += " and Patient_ID in (" & Targets & ")"
            End If
            '
            If chkResults.Checked = True Then 'consider results
                If chkComplete.Checked = True Then  'Only complete
                    sSQL += " and Not (Reported_Final is NULL) and Reported_Final <> '12:00:00 AM'"
                Else    'partial
                    sSQL += " and not (ReportedOn is NULL) and ReportedOn <> '12:00:00 AM'"
                End If
                If chkDotSuppress.Checked = True Then EXCEP = "."
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
                If chkSkip.Checked = True Then
                    If EXCEP = "" Then
                        EXCEP = "TNP"
                    Else
                        EXCEP += "|TNP"
                    End If
                End If
            End If
        End If
        '
        If sSQL <> "" Then
            btnProcess.Enabled = False
            IsRunning = True
            BW.RunWorkerAsync()
        End If
    End Sub

    Private Sub DoBackGroundSynch(ByVal sSQL As String, ByVal _
    IgnoreResults As Boolean, ByVal EXCEP As String)
        Dim TBL As New DataTable
        Try
            If connString <> "" Then
                Dim cnbgs As New SqlClient.SqlConnection(connString)
                cnbgs.Open()
                Dim cmdbgs As New SqlClient.SqlCommand(sSQL, cnbgs)
                cmdbgs.CommandType = CommandType.Text
                Dim dabgs As New SqlClient.SqlDataAdapter(cmdbgs)
                dabgs.Fill(TBL)
                cnbgs.Close()
                cnbgs = Nothing
                'Else    'ODBC
                '    Dim cnbgs As New Odbc.OdbcConnection(connstring)
                '    cnbgs.Open()
                '    Dim cmdbgs As New Odbc.OdbcCommand(sSQL, cnbgs)
                '    cmdbgs.CommandType = CommandType.Text
                '    Dim dabgs As New Odbc.OdbcDataAdapter(cmdbgs)
                '    dabgs.Fill(TBL)
                '    cnbgs.Close()
                '    cnbgs = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        '
        For i As Integer = 0 To TBL.Rows.Count - 1
            If BW.CancellationPending = False Then
                SynchronizeBillables(TBL.Rows(i).Item("ID"), IgnoreResults, EXCEP)
                BW.ReportProgress(CInt((i + 1) * 100 / TBL.Rows.Count),
                CStr(CInt((i + 1) * 100 / TBL.Rows.Count)) & " %")
            Else
                MsgBox("Synchronization interrupted at the record: " &
                (i + 1).ToString & " of Total recoreds: " & TBL.Rows.Count.ToString,
                MsgBoxStyle.Information, "Prolis")
                Exit For
            End If
        Next
    End Sub

    'Private Sub dtpDateFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    dtpDateFrom.BackColor = FCOLOR
    'End Sub

    'Private Sub dtpDateFrom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    If e.KeyCode = Keys.Enter Then
    '        SendKeys.Send("{TAB}")
    '    End If
    'End Sub

    'Private Sub dtpDateFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    dtpDateFrom.BackColor = NFCOLOR
    '    If UserEnteredText(dtpDateFrom) <> "" Then
    '        If IsDate(dtpDateFrom.Text) = False Then
    '            MsgBox("Invalid date")
    '            dtpDateFrom.Text = ""
    '        Else
    '            txtAccFrom.Text = ""
    '            txtAccTo.Text = ""
    '            dgvDiscrete.Rows.Clear()
    '            dgvDiscrete.RowCount = 1
    '        End If
    '    End If
    '    ProcessProgress()
    'End Sub

    Private Sub ProcessProgress()
        If (chkSpecAll.Checked = False And (IsDate(dtpDateFrom.Text) = True Or
        IsDate(dtpDateTo.Text) = True Or txtAccFrom.Text <> "" Or txtAccTo.Text <> "" _
        Or HasDiscreteValues())) Or (chkSpecAll.Checked = True) And cmbBillType.SelectedIndex >= 0 Then
            btnProcess.Enabled = True
        Else
            btnProcess.Enabled = False
        End If
    End Sub

    'Private Sub dtpDateTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    dtpDateTo.BackColor = FCOLOR
    'End Sub

    'Private Sub dtpDateTo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    If e.KeyCode = Keys.Enter Then
    '        SendKeys.Send("{TAB}")
    '    ElseIf e.KeyCode = Keys.Up Then
    '        SendKeys.Send("+{TAB}")
    '    End If
    'End Sub

    'Private Sub dtpDateTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    dtpDateTo.BackColor = NFCOLOR
    '    If UserEnteredText(dtpDateTo) <> "" Then
    '        If IsDate(dtpDateTo.Text) = False Then
    '            MsgBox("Invalid date")
    '            dtpDateTo.Text = ""
    '        Else
    '            txtAccFrom.Text = ""
    '            txtAccTo.Text = ""
    '            dgvDiscrete.Rows.Clear()
    '            dgvDiscrete.RowCount = 1
    '        End If
    '    End If
    '    ProcessProgress()
    'End Sub

    Private Sub txtAccFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccFrom.GotFocus
        txtAccFrom.BackColor = FCOLOR
    End Sub

    Private Sub txtAccFrom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAccFrom.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtAccFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccFrom.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAccFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccFrom.LostFocus
        txtAccFrom.BackColor = NFCOLOR
        If txtAccFrom.Text <> "" Then
            'dtpDateFrom.Text = ""
            'dtpDateTo.Text = ""
            ClearDateTimePicker(dtpDateFrom)
            ClearDateTimePicker(dtpDateTo)
            dgvDiscrete.Rows.Clear()
            dgvDiscrete.RowCount = 1
        End If
        ProcessProgress()
    End Sub

    Private Sub txtAccTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccTo.GotFocus
        txtAccTo.BackColor = FCOLOR
    End Sub

    Private Sub txtAccTo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAccTo.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtAccTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccTo.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAccTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccTo.LostFocus
        txtAccTo.BackColor = NFCOLOR
        If txtAccTo.Text <> "" Then
            'dtpDateFrom.Text = ""
            'dtpDateTo.Text = ""

            ClearDateTimePicker(dtpDateFrom)
            ClearDateTimePicker(dtpDateTo)

            dgvDiscrete.Rows.Clear()
            dgvDiscrete.RowCount = 1
        End If
        ProcessProgress()
    End Sub

    Private Sub dgvDiscrete_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDiscrete.CellEndEdit
        If dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value <> "" AndAlso
        IsNumeric(dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) = False Then
            MsgBox("Only digits are allowed.")
            dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
        Else
            If IsDuplicate(dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                MsgBox("Duplicate Entry is not allowed.")
                dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
            Else
                txtAccFrom.Text = "" : txtAccTo.Text = ""
                'dtpDateFrom.Text = "" : dtpDateTo.Text = ""

                ClearDateTimePicker(dtpDateFrom)
                ClearDateTimePicker(dtpDateTo)

            End If
        End If
        If e.RowIndex = dgvDiscrete.RowCount - 1 Then
            dgvDiscrete.Rows.Add()
            SendKeys.Send("{ENTER}")
        End If
        Update_Progress()
    End Sub

    Private Function IsDuplicate(ByVal AccID As Long) As Boolean
        Dim i As Integer
        Dim CT As Integer = 0
        If dgvDiscrete.RowCount > 0 Then
            For i = 0 To dgvDiscrete.RowCount - 1
                If dgvDiscrete.Rows(i).Cells(0).Value = AccID.ToString Then CT += 1
            Next
            If CT > 1 Then
                IsDuplicate = True
            Else
                IsDuplicate = False
            End If
        Else
            IsDuplicate = False
        End If
    End Function

    Private Sub DiscreteClear()
        dgvDiscrete.Rows.Clear()
        dgvDiscrete.RowCount = 1
    End Sub

    Private Sub Update_Progress()
        If txtAccFrom.Text <> "" Or txtAccFrom.Text <> "" _
        Or HasDiscreteValues() = True Or IsDate(dtpDateFrom.Text) Or
        IsDate(dtpDateTo.Text) Then
            btnProcess.Enabled = True
        Else
            btnProcess.Enabled = False
        End If
    End Sub
    Private Function HasDiscreteValues() As Boolean
        Dim HasVal As Boolean = False
        Dim i As Integer
        If dgvDiscrete.RowCount > 0 Then
            For i = 0 To dgvDiscrete.RowCount - 1
                If dgvDiscrete.Rows(i).Cells(0).Value <> "" Then
                    HasVal = True
                    Exit For
                End If
            Next
        End If
        HasDiscreteValues = HasVal
    End Function

    Private Sub chkResults_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkResults.CheckedChanged
        If chkResults.Checked = True Then
            chkResults.Text = "With Results Only"
            chkComplete.Checked = True
            chkComplete.Enabled = True
            EnableExceptions()
        Else
            chkResults.Text = "Ignore Results"
            chkComplete.Checked = False
            chkComplete.Enabled = False
            DisableExceptions()
        End If
    End Sub

    Private Sub EnableExceptions()
        chkDotSuppress.Checked = True
        chkQNS.Checked = True
        chkLAE.Checked = True
        grpExceptions.Enabled = True
    End Sub

    Private Sub DisableExceptions()
        chkDotSuppress.Checked = False
        chkQNS.Checked = False
        chkLAE.Checked = False
        grpExceptions.Enabled = False
    End Sub

    Private Sub frmBillingSynch_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If BW.IsBusy Then
            e.Cancel = True
        Else
            e.Cancel = False
        End If
    End Sub

    Private Sub frmBillingSynch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dgvDiscrete.RowCount = 1
        cmbBillType.SelectedIndex = 3
        lblFrom.Text += " (" & SystemConfig.DateFormat & ")"
        lblTo.Text += " (" & SystemConfig.DateFormat & ")"
        ConfigureDateTimePicker(dtpDateFrom)
        ConfigureDateTimePicker(dtpDateTo)
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub cmbBillType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBillType.SelectedIndexChanged
        If cmbBillType.SelectedIndex = 0 Then   'Clients
            lstTargets.Items.Clear()
            LoadClients()
            btnSelAll_Click(Nothing, Nothing)
        ElseIf cmbBillType.SelectedIndex = 1 Then   'TP
            lstTargets.Items.Clear()
            LoadThirdParties()
            btnSelAll_Click(Nothing, Nothing)
        ElseIf cmbBillType.SelectedIndex = 2 Then   'Patients
            lstTargets.Items.Clear()
            LoadPatients()
            btnSelAll_Click(Nothing, Nothing)
        ElseIf cmbBillType.SelectedIndex = 3 Then   'All
            lstTargets.Items.Clear()
            lstTargets.Items.Add("All (Clients, Third Parties, Patients)")
            lstTargets.SetItemChecked(0, True)
        Else
            lstTargets.Items.Clear()
        End If
        ProcessProgress()
    End Sub

    Private Sub LoadClients()
        Dim sSQL As String = ""
        If chkSpecAll.Checked = True Then   'All
            sSQL = "Select * from Providers where not LastName_BSN like 'zz%' and ID in (Select distinct " &
            "OrderingProvider_ID from Requisitions where Received <> 0 and Rejected = 0 and BillingType_ID = 0 " &
            "and Not ID in (Select distinct Accession_ID from Req_Billable where Bill_Status = 'B')) Union " &
            "Select * from Providers where ID in (Select distinct OrderingProvider_ID from " &
            "Requisitions where BillingType_ID = 0 and ID in (Select distinct Accession_ID from " &
            "Req_Billable where Bill_Status = 'U'))"
        Else                                'specific
            If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                sSQL = "Select * from Providers where not LastName_BSN like 'zz%' and ID in " &
                "(Select distinct OrderingProvider_ID from Requisitions where Received <> 0 " &
                "and Rejected = 0 and BillingType_ID = 0 and Not ID in (Select distinct Accession_ID from " &
                "Req_Billable where Bill_Status = 'B') and AccessionDate between '" &
                Format(CDate(dtpDateFrom.Text & " 00:00:00"), "MM/dd/yyyy HH:mm:ss") & "' and '" _
                & Format(CDate(dtpDateFrom.Text & " 23:59:00"), "MM/dd/yyyy HH:mm:ss") & "')"
            ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                sSQL = "Select * from Providers where not LastName_BSN like 'zz%' and ID in " &
                "(Select distinct OrderingProvider_ID from Requisitions where Received <> 0 " &
                "and Rejected = 0 and BillingType_ID = 0 and Not ID in (Select distinct Accession_ID from " &
                "Req_Billable where Bill_Status = 'B') and AccessionDate between '" &
                Format(CDate(dtpDateTo.Text & " 00:00:00"), "MM/dd/yyyy HH:mm:ss") & "' and '" _
                & Format(CDate(dtpDateTo.Text & " 23:59:00"), "MM/dd/yyyy HH:mm:ss") & "')"
            ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                sSQL = "Select * from Providers where not LastName_BSN like 'zz%' and ID in " &
                "(Select distinct OrderingProvider_ID from Requisitions where Received <> 0 " &
                "and Rejected = 0 and BillingType_ID = 0 and Not ID in (Select distinct Accession_ID from " &
                "Req_Billable where Bill_Status = 'B') and AccessionDate between '" &
                Format(CDate(dtpDateFrom.Text & " 00:00:00"), "MM/dd/yyyy HH:mm:ss") & "' and '" _
                & Format(CDate(dtpDateTo.Text & " 23:59:00"), "MM/dd/yyyy HH:mm:ss") & "')"
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                sSQL = "Select * from Providers where not LastName_BSN like 'zz%' and ID in " &
                "(Select distinct OrderingProvider_ID from Requisitions where Received <> 0 " &
                "and Rejected = 0 and BillingType_ID = 0 and Not ID in (Select distinct Accession_ID from " &
                "Req_Billable where Bill_Status = 'B') and ID = " & Val(txtAccFrom.Text) & ")"
            ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                sSQL = "Select * from Providers where not LastName_BSN like 'zz%' and ID in " &
                "(Select distinct OrderingProvider_ID from Requisitions where Received <> 0 " &
                "and Rejected = 0 and BillingType_ID = 0 and  and Not ID in (Select distinct Accession_ID " &
                "from Req_Billable where Bill_Status = 'B') ID = " & Val(txtAccTo.Text) & ")"
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                sSQL = "Select * from Providers where not LastName_BSN like 'zz%' and ID in " &
                "(Select distinct OrderingProvider_ID from Requisitions where Received <> 0 " &
                "and Rejected = 0 and BillingType_ID = 0 and Not ID in (Select distinct Accession_ID from " &
                "Req_Billable where Bill_Status = 'B') And ID between " & Val(txtAccFrom.Text) _
                & " And " & Val(txtAccTo.Text) & ")"
            ElseIf dgvDiscrete.RowCount > 1 AndAlso GetDiscreteAccIDs() <> "" Then
                sSQL = "Select * from Providers where not LastName_BSN like 'zz%' and ID in " &
                "(Select distinct OrderingProvider_ID from Requisitions where Received <> 0 " &
                "and Rejected = 0 and BillingType_ID = 0 and Not ID in (Select distinct Accession_ID from " &
                "Req_Billable where Bill_Status = 'B') And ID in (" & GetDiscreteAccIDs() & "))"
            End If
        End If
        '
        If sSQL <> "" Then
            Dim Provider As String = ""
            If connString <> "" Then
                Dim cncls As New SqlClient.SqlConnection(connString)
                cncls.Open()
                Dim cmdcls As New SqlClient.SqlCommand(sSQL, cncls)
                cmdcls.CommandType = CommandType.Text
                Dim drcls As SqlClient.SqlDataReader = cmdcls.ExecuteReader
                If drcls.HasRows Then
                    While drcls.Read
                        If drcls("IsIndividual") = False Then
                            Provider = drcls("LastName_BSN")
                        Else
                            If drcls("Degree") IsNot DBNull.Value _
                            AndAlso Trim(drcls("Degree")) <> "" Then
                                If drcls("MiddleName") IsNot DBNull.Value _
                                AndAlso Trim(drcls("MiddleName")) <> "" Then
                                    Provider = Trim(drcls("LastName_BSN")) & ", " &
                                    Trim(drcls("FirstName")) & " " & Trim(drcls("MiddleName")) & " " &
                                    Trim(drcls("Degree")) & " (" & drcls("ID").ToString & ")"
                                Else
                                    Provider = Trim(drcls("LastName_BSN")) & ", " & Trim(drcls("FirstName")) _
                                    & " " & Trim(drcls("Degree")) & " (" & drcls("ID").ToString & ")"
                                End If
                            Else
                                If drcls("MiddleName") IsNot DBNull.Value _
                                AndAlso Trim(drcls("MiddleName")) <> "" Then
                                    Provider = Trim(drcls("LastName_BSN")) & ", " & Trim(drcls("FirstName")) & " " &
                                    Trim(drcls("MiddleName")) & " (" & drcls("ID").ToString & ")"
                                Else
                                    Provider = Trim(drcls("LastName_BSN")) & ", " &
                                    Trim(drcls("FirstName")) & " (" & drcls("ID").ToString & ")"
                                End If
                            End If
                        End If
                        lstTargets.Items.Add(New MyList(Provider, drcls("ID")))
                    End While
                End If
                cncls.Close()
                cncls = Nothing
                'Else    'ODBC
                'Dim cncls As New Odbc.OdbcConnection(connstring)
                'cncls.Open()
                'Dim cmdcls As New Odbc.OdbcCommand(sSQL, cncls)
                'cmdcls.CommandType = CommandType.Text
                'Dim drcls As Odbc.OdbcDataReader = cmdcls.ExecuteReader
                'If drcls.HasRows Then
                '    While drcls.Read
                '        If drcls("IsIndividual") = False Then
                '            Provider = drcls("LastName_BSN")
                '        Else
                '            If drcls("Degree") IsNot DBNull.Value _
                '            AndAlso Trim(drcls("Degree")) <> "" Then
                '                If drcls("MiddleName") IsNot DBNull.Value _
                '                AndAlso Trim(drcls("MiddleName")) <> "" Then
                '                    Provider = Trim(drcls("LastName_BSN")) & ", " &
                '                    Trim(drcls("FirstName")) & " " & Trim(drcls("MiddleName")) & " " &
                '                    Trim(drcls("Degree")) & " (" & drcls("ID").ToString & ")"
                '                Else
                '                    Provider = Trim(drcls("LastName_BSN")) & ", " & Trim(drcls("FirstName")) _
                '                    & " " & Trim(drcls("Degree")) & " (" & drcls("ID").ToString & ")"
                '                End If
                '            Else
                '                If drcls("MiddleName") IsNot DBNull.Value _
                '                AndAlso Trim(drcls("MiddleName")) <> "" Then
                '                    Provider = Trim(drcls("LastName_BSN")) & ", " & Trim(drcls("FirstName")) & " " &
                '                    Trim(drcls("MiddleName")) & " (" & drcls("ID").ToString & ")"
                '                Else
                '                    Provider = Trim(drcls("LastName_BSN")) & ", " &
                '                    Trim(drcls("FirstName")) & " (" & drcls("ID").ToString & ")"
                '                End If
                '            End If
                '        End If
                '        lstTargets.Items.Add(New MyList(Provider, drcls("ID")))
                '    End While
                'End If
                'cncls.Close()
                'cncls = Nothing
            End If
            sSQL = ""
        End If
    End Sub

    Private Function GetDiscreteAccIDs() As String
        Dim DisAccIDs As String = ""
        Dim i As Integer
        For i = 0 To dgvDiscrete.RowCount - 1
            If Trim(dgvDiscrete.Rows(i).Cells(0).Value) <> "" Then _
            DisAccIDs += Trim(dgvDiscrete.Rows(i).Cells(0).Value) & ", "
        Next
        If Len(DisAccIDs) > 2 Then DisAccIDs = Microsoft.VisualBasic.Mid(DisAccIDs, 1, Len(DisAccIDs) - 2)
        Return DisAccIDs
    End Function

    Private Sub LoadThirdParties()
        Dim sSQL As String = ""
        If chkSpecAll.Checked = True Then   'All
            sSQL = "Select * from Payers where not PayerName like 'zz%' and ID in (Select " &
            "distinct PrimePayer_ID from Requisitions where Received <> 0 and Rejected = 0 and BillingType_ID = 1 " &
            "and Not ID in (Select distinct Accession_ID from Req_Billable where Bill_Status = " &
            "'B')) Union Select * from Payers where not PayerName like 'zz%' and ID in (Select " &
            "distinct PrimePayer_ID from Requisitions where BillingType_ID = 1 and ID in (Select " &
            "distinct Accession_ID from Req_Billable where Bill_Status = 'U'))"
        Else                                'specific
            If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                sSQL = "Select * from Payers where not PayerName like 'zz%' and ID in " &
                "(Select distinct PrimePayer_ID from Requisitions where Received <> 0 and Rejected = 0 and " &
                "BillingType_ID = 1 and Not ID in (Select distinct Accession_ID from " &
                "Req_Billable where Bill_Status = 'B') and AccessionDate between '" &
                Format(CDate(dtpDateFrom.Text & " 00:00:00"), "MM/dd/yyyy HH:mm:ss") & "' and '" _
                & Format(CDate(dtpDateFrom.Text & " 23:59:00"), "MM/dd/yyyy HH:mm:ss") & "')"
            ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                sSQL = "Select * from Payers where not PayerName like 'zz%' and ID in " &
                "(Select distinct PrimePayer_ID from Requisitions where Received <> 0 and Rejected = 0 and " &
                "BillingType_ID = 1 and Not ID in (Select distinct Accession_ID from " &
                "Req_Billable where Bill_Status = 'B') and AccessionDate between '" &
                Format(CDate(dtpDateTo.Text & " 00:00:00"), "MM/dd/yyyy HH:mm:ss") & "' and '" _
                & Format(CDate(dtpDateTo.Text & " 23:59:00"), "MM/dd/yyyy HH:mm:ss") & "')"
            ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                sSQL = "Select * from Payers where not PayerName like 'zz%' and ID in " &
                "(Select distinct PrimePayer_ID from Requisitions where Received <> 0 and Rejected = 0 and " &
                "BillingType_ID = 1 and Not ID in (Select distinct Accession_ID from " &
                "Req_Billable where Bill_Status = 'B') and AccessionDate between '" &
                Format(CDate(dtpDateFrom.Text & " 00:00:00"), "MM/dd/yyyy HH:mm:ss") & "' and '" _
                & Format(CDate(dtpDateTo.Text & " 23:59:00"), "MM/dd/yyyy HH:mm:ss") & "')"
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                sSQL = "Select * from Payers where not PayerName like 'zz%' and ID in (Select " &
                "distinct PrimePayer_ID from Requisitions where Received <> 0 and Rejected = 0 and BillingType_ID = 1 and Not ID " &
                "in (Select distinct Accession_ID from Req_Billable where Bill_Status " &
                "= 'B') and ID = " & Val(txtAccFrom.Text) & ")"
            ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                sSQL = "Select * from Payers where not PayerName like 'zz%' and ID in (Select " &
                "distinct PrimePayer_ID from Requisitions where Received <> 0 and Rejected = 0 and BillingType_ID = 1 and Not ID " &
                "in (Select distinct Accession_ID from Req_Billable where Bill_Status " &
                "= 'B') and ID = " & Val(txtAccTo.Text) & ")"
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                sSQL = "Select * from Payers where not PayerName like 'zz%' and ID in (Select " &
                "distinct PrimePayer_ID from Requisitions where Received <> 0 and Rejected = 0 and BillingType_ID = 1 and Not ID " &
                "in (Select distinct Accession_ID from Req_Billable where Bill_Status = 'B') And " &
                "ID between " & Val(txtAccFrom.Text) & " And " & Val(txtAccTo.Text) & ")"
            ElseIf dgvDiscrete.RowCount > 1 AndAlso GetDiscreteAccIDs() <> "" Then
                sSQL = "Select * from Payers where not PayerName like 'zz%' and ID in (Select " &
                "distinct PrimePayer_ID from Requisitions where Received <> 0 and Rejected = 0 and BillingType_ID = 1 and Not ID " &
                "in (Select distinct Accession_ID from Req_Billable where Bill_Status = 'B') And " &
                "ID in (" & GetDiscreteAccIDs() & "))"
            End If
        End If
        '
        If sSQL <> "" Then
            Dim Payer As String = ""
            If connString <> "" Then
                Dim cnprs As New SqlClient.SqlConnection(connString)
                cnprs.Open()
                Dim cmdprs As New SqlClient.SqlCommand(sSQL, cnprs)
                cmdprs.CommandType = CommandType.Text
                Dim drprs As SqlClient.SqlDataReader = cmdprs.ExecuteReader
                If drprs.HasRows Then
                    While drprs.Read
                        Payer = Trim(drprs("PayerName")) & " (" & drprs("ID").ToString & ")"
                        lstTargets.Items.Add(New MyList(Payer, drprs("ID")))
                    End While
                End If
                cnprs.Close()
                cnprs = Nothing
                'Else    'ODBC
                '    Dim cnprs As New Odbc.OdbcConnection(connstring)
                '    cnprs.Open()
                '    Dim cmdprs As New Odbc.OdbcCommand(sSQL, cnprs)
                '    cmdprs.CommandType = CommandType.Text
                '    Dim drprs As Odbc.OdbcDataReader = cmdprs.ExecuteReader
                '    If drprs.HasRows Then
                '        While drprs.Read
                '            Payer = Trim(drprs("PayerName")) & " (" & drprs("ID").ToString & ")"
                '            lstTargets.Items.Add(New MyList(Payer, drprs("ID")))
                '        End While
                '    End If
                '    cnprs.Close()
                '    cnprs = Nothing
            End If
            sSQL = ""
        End If
    End Sub

    Private Sub LoadPatients()
        Dim sSQL As String = ""
        If chkSpecAll.Checked = True Then   'All
            sSQL = "Select * from Patients where not LastName like 'zz%' and ID in (Select " &
            "distinct Patient_ID from Requisitions where Received <> 0 and Rejected = 0 and BillingType_ID = 2 and Not ID in " &
            "(Select distinct Accession_ID from Req_Billable where Bill_Status = 'B')) Union " &
            "Select * from Patients where not LastName like 'zz%' and ID in (Select distinct " &
            "Patient_ID from Requisitions where BillingType_ID = 2 and ID in (Select distinct " &
            "Accession_ID from Req_Billable where Bill_Status = 'U'))"
        Else                                'specific
            If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                sSQL = "Select * from Patients where not LastName like 'zz%' and ID in (Select " &
                "distinct Patient_ID from Requisitions where Received <> 0 and Rejected = 0 and BillingType_ID " &
                "= 2 and Not ID in (Select distinct Accession_ID from Req_Billable where " &
                "Bill_Status = 'B') and AccessionDate between '" & Format(CDate(dtpDateFrom.Text _
                & " 00:00:00"), "MM/dd/yyyy HH:mm:ss") & "' and '" & Format(CDate(dtpDateFrom.Text _
                & " 23:59:00"), "MM/dd/yyyy HH:mm:ss") & "')"
            ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                sSQL = "Select * from Patients where not LastName like 'zz%' and ID in (Select " &
                "distinct Patient_ID from Requisitions where Received <> 0 and Rejected = 0 and BillingType_ID " &
                "= 2 and Not ID in (Select distinct Accession_ID from Req_Billable where " &
                "Bill_Status = 'B') and AccessionDate between '" & Format(CDate(dtpDateTo.Text _
                & " 00:00:00"), "MM/dd/yyyy HH:mm:ss") & "' and '" & Format(CDate(dtpDateTo.Text _
                & " 23:59:00"), "MM/dd/yyyy HH:mm:ss") & "')"
            ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                sSQL = "Select * from Patients where not LastName like 'zz%' and ID in (Select " &
                "distinct Patient_ID from Requisitions where Received <> 0 and Rejected = 0 and BillingType_ID " &
                "= 2 and Not ID in (Select distinct Accession_ID from Req_Billable where " &
                "Bill_Status = 'B') and AccessionDate between '" & Format(CDate(dtpDateFrom.Text _
                & " 00:00:00"), "MM/dd/yyyy HH:mm:ss") & "' and '" & Format(CDate(dtpDateTo.Text _
                & " 23:59:00"), "MM/dd/yyyy HH:mm:ss") & "')"
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                sSQL = "Select * from Patients where not LastName like 'zz%' and ID in (Select " &
                "distinct Patient_ID from Requisitions where Received <> 0 and Rejected = 0 and BillingType_ID " &
                "= 2 and Not ID in (Select distinct Accession_ID from Req_Billable where " &
                "Bill_Status = 'B') and ID = " & Val(txtAccFrom.Text) & ")"
            ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                sSQL = "Select * from Patients where not LastName like 'zz%' and ID in (Select " &
                "distinct Patient_ID from Requisitions where Received <> 0 and Rejected = 0 and BillingType_ID " &
                "= 2 and Not ID in (Select distinct Accession_ID from Req_Billable where " &
                "Bill_Status = 'B') and ID = " & Val(txtAccTo.Text) & ")"
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                sSQL = "Select * from Patients where not LastName like 'zz%' and ID in (Select " &
                "distinct Patient_ID from Requisitions where Received <> 0 and Rejected = 0 and BillingType_ID " &
                "= 2 and Not ID in (Select distinct Accession_ID from Req_Billable where " &
                "Bill_Status = 'B') and ID between " & Val(txtAccFrom.Text) & " and " &
                Val(txtAccTo.Text) & ")"
            ElseIf dgvDiscrete.RowCount > 1 AndAlso GetDiscreteAccIDs() <> "" Then
                sSQL = "Select * from Patients where not LastName like 'zz%' and ID in (Select " &
                "distinct Patient_ID from Requisitions where Received <> 0 and Rejected = 0 and BillingType_ID " &
                "= 2 and Not ID in (Select distinct Accession_ID from Req_Billable where " &
                "Bill_Status = 'B') and ID in (" & GetDiscreteAccIDs() & "))"
            End If
        End If
        '
        If sSQL <> "" Then
            Dim Patient As String = ""
            If connString <> "" Then
                Dim cnpat As New SqlClient.SqlConnection(connString)
                cnpat.Open()
                Dim cmdpat As New SqlClient.SqlCommand(sSQL, cnpat)
                cmdpat.CommandType = CommandType.Text
                Dim drpat As SqlClient.SqlDataReader = cmdpat.ExecuteReader
                If drpat.HasRows Then
                    While drpat.Read
                        If drpat("MiddleName") IsNot DBNull.Value _
                        AndAlso Trim(drpat("MiddleName")) <> "" Then
                            Patient = Trim(drpat("LastName")) & ", " & Trim(drpat("FirstName")) _
                            & " " & Microsoft.VisualBasic.Left(drpat("MiddleName"), 1) & " - " &
                            Format(drpat("DOB"), SystemConfig.DateFormat) & " - " & drpat("Sex")
                        Else
                            Patient = Trim(drpat("LastName")) & ", " & Trim(drpat("FirstName")) _
                            & " - " & Format(drpat("DOB"), SystemConfig.DateFormat) & " - " & drpat("Sex")
                        End If
                        lstTargets.Items.Add(New MyList(Patient, drpat("ID")))
                    End While
                End If
                cnpat.Close()
                cnpat = Nothing
                'Else    'ODBC
                '    Dim cnpat As New Odbc.OdbcConnection(connstring)
                '    cnpat.Open()
                '    Dim cmdpat As New Odbc.OdbcCommand(sSQL, cnpat)
                '    cmdpat.CommandType = CommandType.Text
                '    Dim drpat As Odbc.OdbcDataReader = cmdpat.ExecuteReader
                '    If drpat.HasRows Then
                '        While drpat.Read
                '            If drpat("MiddleName") IsNot DBNull.Value _
                '           AndAlso Trim(drpat("MiddleName")) <> "" Then
                '                Patient = Trim(drpat("LastName")) & ", " & Trim(drpat("FirstName")) _
                '                & " " & Microsoft.VisualBasic.Left(drpat("MiddleName"), 1) & " - " &
                '                Format(drpat("DOB"), SystemConfig.DateFormat) & " - " & drpat("Sex")
                '            Else
                '                Patient = Trim(drpat("LastName")) & ", " & Trim(drpat("FirstName")) _
                '                & " - " & Format(drpat("DOB"), SystemConfig.DateFormat) & " - " & drpat("Sex")
                '            End If
                '            lstTargets.Items.Add(New MyList(Patient, drpat("ID")))
                '        End While
                '    End If
                '    cnpat.Close()
                '    cnpat = Nothing
            End If
            sSQL = ""
        End If
    End Sub

    Private Sub btnSelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelAll.Click
        If lstTargets.Items.Count > 0 Then
            Dim i As Integer
            For i = 0 To lstTargets.Items.Count - 1
                lstTargets.SetItemChecked(i, True)
            Next
        End If
    End Sub

    Private Sub btnDeSel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeSel.Click
        If lstTargets.Items.Count > 0 Then
            Dim i As Integer
            For i = 0 To lstTargets.Items.Count - 1
                lstTargets.SetItemChecked(i, False)
            Next
        End If
    End Sub

    Private Sub chkSpecAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSpecAll.CheckedChanged
        If chkSpecAll.Checked = False Then  'Specific
            chkSpecAll.Text = "Specific Unbilled"
            EnableSpecifics()
        Else                                'All
            chkSpecAll.Text = "All Unbilled"
            DisableSpecifics()
        End If
    End Sub

    Private Sub DisableSpecifics()
        'dtpDateFrom.Text = ""
        'dtpDateTo.Text = ""

        ConfigureDateTimePicker(dtpDateFrom)
        ConfigureDateTimePicker(dtpDateTo)

        txtAccFrom.Text = ""
        txtAccTo.Text = ""
        dgvDiscrete.Rows.Clear()
        dgvDiscrete.RowCount = 1
        'dtpDateFrom.Enabled = False
        'dtpDateTo.Enabled = False

        dtpDateFrom.Enabled = True
        dtpDateTo.Enabled = True

        txtAccFrom.Enabled = False
        txtAccTo.Enabled = False
        dgvDiscrete.Enabled = False
        cmbBillType.SelectedIndex = -1
    End Sub

    Private Sub EnableSpecifics()
        'dtpDateFrom.Enabled = True
        'dtpDateTo.Enabled = True

        dtpDateFrom.Enabled = True
        dtpDateTo.Enabled = True

        txtAccFrom.Enabled = True
        txtAccTo.Enabled = True
        dgvDiscrete.Enabled = True
        cmbBillType.SelectedIndex = -1
    End Sub

    Private Sub chkComplete_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkComplete.CheckedChanged
        If chkComplete.Checked = True Then 'Final
            chkComplete.Text = "Final Accessions Only"
        Else
            chkComplete.Text = "Final and Partial Both"
        End If
    End Sub

    Private Sub dgvDiscrete_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvDiscrete.CellMouseUp
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
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
            End If
        End If
        Update_Progress()
    End Sub

    Private Sub BW_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BW.DoWork
        DoBackGroundSynch(sSQL, IgnoreResults, EXCEP)
    End Sub

    Private Sub BW_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BW.ProgressChanged
        PB.Value = e.ProgressPercentage
        lblStatus.Text = e.UserState
    End Sub

    Private Sub BW_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BW.RunWorkerCompleted
        sSQL = ""
        ClearDateTimePicker(dtpDateFrom)
        ClearDateTimePicker(dtpDateTo)
        'dtpDateFrom.Text = "" : dtpDateTo.Text = ""
        txtAccFrom.Text = "" : txtAccTo.Text = ""
        dgvDiscrete.Rows.Clear() : dgvDiscrete.RowCount = 1
        cmbBillType.SelectedIndex = -1
        If BW.CancellationPending = True Then Me.Close()
    End Sub

    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.Click
        HelpProvider1.HelpNamespace = My.Application.Info.DirectoryPath & "\ProlisHelp.chm"
        HelpProvider1.SetHelpKeyword(Me, 25)
        HelpProvider1.SetHelpNavigator(Me, HelpNavigator.TopicId)
        Help.ShowHelp(Me, My.Application.Info.DirectoryPath & "\ProlisHelp.chm")
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

    Private Sub dtpDateTo_LostFocus(sender As Object, e As EventArgs) Handles dtpDateFrom.LostFocus, dtpDateTo.LostFocus
        ProcessProgress()
    End Sub
End Class
