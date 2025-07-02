Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmEBillOut
    'Private connStringS() As String = {connString, connstring}

    Private Sub chk8371500_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk8371500.CheckedChanged
        If chk8371500.Checked = False Then  '837
            chk8371500.Text = "837"
        Else
            chk8371500.Text = "1500"
        End If
    End Sub

    Private Sub frmEBillOut_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PopulatePartners()
        cmbStatus.SelectedIndex = 2 'Unprocessed
        dgvDiscrete.RowCount = 1
        ConfigureDateTimePicker(dtpDateFrom)
        ConfigureDateTimePicker(dtpDateTo)
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub PopulatePartners()
        cmbPartner.Items.Clear() ' Clear the combo box items

        ' Old: Dim CNP As New ADODB.Connection
        ' Old: CNP.Open(connstring)
        ' Old: Dim Rs As New ADODB.Recordset
        ' Old: Rs.Open("Select * from Partners", ...)

        Dim query As String = "SELECT Name, ID FROM Partners"

        Using connection As New SqlConnection(connString) ' Old: Dim CNP As New ADODB.Connection
            connection.Open() ' Old: CNP.Open(connstring)
            Using command As New SqlCommand(query, connection)
                Using reader As SqlDataReader = command.ExecuteReader() ' Old: Rs.Open(...)
                    While reader.Read() ' Old: Do Until Rs.EOF
                        cmbPartner.Items.Add(New MyList(reader("Name").ToString(), reader("ID").ToString())) ' Old: Rs.Fields("Name").Value, Rs.Fields("ID").Value
                    End While
                End Using
            End Using
        End Using ' Old: Rs.Close(), Rs = Nothing, CNP.Close(), CNP = Nothing
    End Sub

    Private Sub EBilloutProgress()
        If cmbPartner.SelectedIndex <> -1 And ((txtAccFrom.Text <> "" And
        txtAccTo.Text <> "") Or (IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text)) Or HasDiscreteVals()) And lstPayers.CheckedItems.Count > 0 Then
            btnProcess.Enabled = True
        Else
            btnProcess.Enabled = False
        End If
    End Sub

    Private Function HasDiscreteVals() As Boolean
        Dim Has As Boolean = False
        For i As Integer = 0 To dgvDiscrete.RowCount - 1
            If dgvDiscrete.Rows(i).Cells(0).Value IsNot Nothing _
            AndAlso Trim(dgvDiscrete.Rows(i).Cells(0).Value) <> "" Then
                Has = True
                Exit For
            End If
        Next
        Return Has
    End Function

    Private Sub cmbPartner_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPartner.SelectedIndexChanged
        LoadPayers()
        EBilloutProgress()
    End Sub

    Private Sub LoadPayers()
        lstPayers.Items.Clear()
        Dim ItemX As MyList
        If cmbPartner.SelectedIndex <> -1 Then
            ItemX = cmbPartner.SelectedItem
            Dim sSQL As String = ""
            If txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                If chkAccInv.Checked = False Then   'Accession
                    sSQL = "Select * from Payers where ID in (Select distinct a.Payer_ID from Partner_Payer a inner join " &
                    "Charges b on b.Ar_ID = a.Payer_ID where a.Partner_ID = " & ItemX.ItemData & " and b.ECC <> 0 and " &
                    "b.ArType = 1 and not b.ID in (Select distinct Charge_ID from Payment_Detail) and b.Accession_ID between " &
                    Val(txtAccFrom.Text) & " and " & Val(txtAccTo.Text) & ")"
                Else    'Invoice
                    sSQL = "Select * from Payers where ID in (Select distinct a.Payer_ID from Partner_Payer a inner join " &
                   "Charges b on b.Ar_ID = a.Payer_ID where a.Partner_ID = " & ItemX.ItemData & " and b.ECC <> 0 and " &
                   "b.ArType = 1 and not b.ID in (Select distinct Charge_ID from Payment_Detail) and b.ID between " &
                   Val(txtAccFrom.Text) & " and " & Val(txtAccTo.Text) & ")"
                End If
            ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                If chkSvcBill.Checked = False Then  'Service
                    sSQL = "Select * from Payers where ID in (Select distinct a.Payer_ID from Partner_Payer a inner join " &
                    "Charges b on b.Ar_ID = a.Payer_ID where a.Partner_ID = " & ItemX.ItemData & " and b.ECC <> 0 and " &
                    "b.ArType = 1 and not b.ID in (Select distinct Charge_ID from Payment_Detail) and b.Svc_Date between '" &
                    dtpDateFrom.Text & " 00:00:00' and '" & dtpDateTo.Text & " 23:59:00')"
                Else    'Bill
                    sSQL = "Select * from Payers where ID in (Select distinct a.Payer_ID from Partner_Payer a inner join " &
                    "Charges b on b.Ar_ID = a.Payer_ID where a.Partner_ID = " & ItemX.ItemData & " and b.ECC <> 0 and " &
                    "b.ArType = 1 and not b.ID in (Select distinct Charge_ID from Payment_Detail) and b.Bill_Date between '" &
                    dtpDateFrom.Text & " 00:00:00' and '" & dtpDateTo.Text & " 23:59:00')"
                End If
            ElseIf HasDiscreteVals() Then
                If chkAccInv.Checked = False Then   'Accession
                    sSQL = "Select * from Payers where ID in (Select distinct a.Payer_ID from Partner_Payer a inner join " &
                    "Charges b on b.Ar_ID = a.Payer_ID where a.Partner_ID = " & ItemX.ItemData & " and b.ECC <> 0 and " &
                    "b.ArType = 1 and not b.ID in (Select distinct Charge_ID from Payment_Detail) and b.Accession_ID in ("
                Else    'Invoice
                    sSQL = "Select * from Payers where ID in (Select distinct a.Payer_ID from Partner_Payer a inner join " &
                    "Charges b on b.Ar_ID = a.Payer_ID where a.Partner_ID = " & ItemX.ItemData & " and b.ECC <> 0 and " &
                    "b.ArType = 1 and not b.ID in (Select distinct Charge_ID from Payment_Detail) and b.ID in ("
                End If
                For d As Integer = 0 To dgvDiscrete.RowCount - 1
                    If dgvDiscrete.Rows(d).Cells(0).Value IsNot Nothing _
                    AndAlso Trim(dgvDiscrete.Rows(d).Cells(0).Value) <> "" Then
                        sSQL += Trim(dgvDiscrete.Rows(d).Cells(0).Value) & ", "
                    End If
                Next
                If sSQL.EndsWith(", ") Then sSQL = Microsoft.VisualBasic.Mid(sSQL, 1, Len(sSQL) - 2) & "))"
            End If
            If sSQL <> "" Then
                If connString <> "" Then 'sql
                    Dim cnpr As New SqlConnection(connString)
                    cnpr.Open()
                    Dim cmdpr As New SqlCommand(sSQL, cnpr)
                    cmdpr.CommandTimeout = 180
                    cmdpr.CommandType = CommandType.Text
                    Dim drpr As SqlDataReader = cmdpr.ExecuteReader
                    If drpr.HasRows Then
                        While drpr.Read
                            lstPayers.Items.Add(New MyList(drpr("PayerName"), drpr("ID")))
                        End While
                    End If
                    cnpr.Close()
                    cnpr = Nothing
                    'Else    'odbc
                    '    Dim cnpr As New Odbc.OdbcConnection(connstring)
                    '    cnpr.Open()
                    '    Dim cmdpr As New Odbc.OdbcCommand(sSQL, cnpr)
                    '    cmdpr.CommandType = CommandType.Text
                    '    Dim drpr As Odbc.OdbcDataReader = cmdpr.ExecuteReader
                    '    If drpr.HasRows Then
                    '        While drpr.Read
                    '            lstPayers.Items.Add(New MyList(drpr("PayerName"), drpr("ID")))
                    '        End While
                    '    End If
                    '    cnpr.Close()
                    '    cnpr = Nothing
                End If
                For n As Integer = 0 To lstPayers.Items.Count - 1
                    lstPayers.SetItemChecked(n, True)
                Next
            End If
        End If
    End Sub

    Private Sub txtAccFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccFrom.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAccTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccTo.KeyPress
        Numerals(sender, e)
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
        EBilloutProgress()
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
        EBilloutProgress()
    End Sub

    'Private Sub txtDateFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If UserEnteredText(txtDateFrom) <> "" Then
    '        If IsDate(txtDateFrom.Text) Then
    '            txtAccFrom.Text = ""
    '            txtAccTo.Text = ""
    '            dgvDiscrete.Rows.Clear()
    '            dgvDiscrete.RowCount = 1
    '        Else
    '            MsgBox("Invalid date")
    '            txtDateFrom.Text = ""
    '        End If
    '    End If
    '    EBilloutProgress()
    'End Sub

    'Private Sub txtDateTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If UserEnteredText(txtDateTo) <> "" Then
    '        If IsDate(txtDateTo.Text) Then
    '            txtAccFrom.Text = ""
    '            txtAccTo.Text = ""
    '            dgvDiscrete.Rows.Clear()
    '            dgvDiscrete.RowCount = 1
    '        Else
    '            MsgBox("Invalid date")
    '            txtDateTo.Text = ""
    '        End If
    '    End If
    '    EBilloutProgress()
    'End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        If cmbPartner.SelectedIndex <> -1 And ((txtAccFrom.Text <> "" And txtAccTo.Text <> "") Or
        (IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text)) Or HasDiscreteVals()) And lstPayers.CheckedItems.Count > 0 Then
            Dim ItemX As MyList = cmbPartner.SelectedItem
            Dim sSQL As String = ""
            Dim Claims() As String = {""}
            Dim Invoices As String = ""
            Dim Payers As String = ""
            Dim ItemP As MyList
            Dim i As Integer
            Dim ToFile As String = ""
            Dim Accs() As String = {""}
            Dim FC As Object
            For i = 0 To lstPayers.Items.Count - 1
                If lstPayers.GetItemChecked(i) = True Then
                    ItemP = lstPayers.Items(i)
                    Payers += ItemP.ItemData.ToString & ", "
                End If
            Next
            If Payers.EndsWith(", ") Then Payers = Microsoft.VisualBasic.Mid(Payers, 1, Len(Payers) - 2)
            'Dim Rs As New ADODB.Recordset
            If txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                If chkAccInv.Checked = False Then   'Accession
                    sSQL = "Select * from Charges where Not ID in (Select distinct Charge_ID from Payment_Detail) " &
                    "and ECC <> 0 and ArType = 1 and Ar_ID in (" & Payers & ") and Accession_ID between " &
                    Val(txtAccFrom.Text) & " and " & Val(txtAccTo.Text)
                Else    'Invoice
                    sSQL = "Select * from Charges where Not ID in (Select distinct Charge_ID from Payment_Detail) " &
                    "and ECC <> 0 and ArType = 1 and Ar_ID in (" & Payers & ") and ID between " &
                    Val(txtAccFrom.Text) & " and " & Val(txtAccTo.Text)
                End If
            ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                If chkSvcBill.Checked = False Then  'Service
                    sSQL = "Select * from Charges where Not ID in (Select distinct Charge_ID from Payment_Detail) " &
                    "and ECC <> 0 and ArType = 1 and Ar_ID in (" & Payers & ") and Svc_Date between '" &
                    dtpDateFrom.Text & " 00:00:00' and '" & dtpDateTo.Text & " 23:59:00'"
                Else    'Bill
                    sSQL = "Select * from Charges where Not ID in (Select distinct Charge_ID from Payment_Detail) " &
                    "and ECC <> 0 and ArType = 1 and Ar_ID in (" & Payers & ") and Bill_Date between '" &
                    dtpDateFrom.Text & " 00:00:00' and '" & dtpDateTo.Text & " 23:59:00'"
                End If
            ElseIf HasDiscreteVals() Then
                If chkAccInv.Checked = False Then   'Accession
                    sSQL = "Select * from Charges where Not ID in (Select distinct Charge_ID from Payment_Detail) " &
                    "and ECC <> 0 and ArType = 1 and Ar_ID in (" & Payers & ") and Accession_ID in ("
                Else    'Invoice
                    sSQL = "Select * from Charges where Not ID in (Select distinct Charge_ID from Payment_Detail) " &
                    "and ECC <> 0 and ArType = 1 and Ar_ID in (" & Payers & ") and ID in ("
                End If
                For d As Integer = 0 To dgvDiscrete.RowCount - 1
                    If dgvDiscrete.Rows(d).Cells(0).Value IsNot Nothing _
                    AndAlso Trim(dgvDiscrete.Rows(d).Cells(0).Value) <> "" Then
                        sSQL += Trim(dgvDiscrete.Rows(d).Cells(0).Value) & ", "
                    End If
                Next
                If sSQL.EndsWith(", ") Then sSQL = Microsoft.VisualBasic.Mid(sSQL, 1, Len(sSQL) - 2) & ")"
            End If
            If sSQL <> "" Then
                If cmbStatus.SelectedIndex = 2 Then     'Unprocessed - default
                    sSQL += " and Output = 0"
                ElseIf cmbStatus.SelectedIndex = 1 Then     'Processed 
                    If Not IsDate(txtProcessDate.Text) Then
                        sSQL += " and Output <> 0 and Accession_ID in (Select " &
                        "Accession_ID from Req_BAR_History where BAR_Event_Type_ID " &
                        "= 5)"
                    Else
                        sSQL += " and Output <> 0 and Accession_ID in (Select Accession_ID from " &
                        "Req_BAR_History where BAR_Event_Type_ID = 5 and BAR_Event_Date between '" &
                        txtProcessDate.Text & " 00:00:00' and '" & txtProcessDate.Text & " 23:59:00')"
                    End If
                End If

                '
                If connString <> "" Then
                    Dim cn837 As New SqlConnection(connString)
                    cn837.Open()
                    Dim cmd837 As New SqlCommand(sSQL, cn837)
                    cmd837.CommandType = CommandType.Text
                    Dim dr837 As SqlDataReader = cmd837.ExecuteReader
                    If Not dr837.HasRows Then
                        MsgBox("No record found based on your criteria")
                    Else
                        While dr837.Read
                            If Claims(UBound(Claims)) <> "" Then ReDim Preserve Claims(UBound(Claims) + 1)
                            Claims(UBound(Claims)) = dr837("ID").ToString
                            'Invoices += Rs.Fields("ID").Value.ToString & ", "
                            If Accs(UBound(Accs)) <> "" Then ReDim Preserve Accs(UBound(Accs) + 1)
                            Accs(UBound(Accs)) = dr837("Accession_ID").ToString
                        End While
                    End If
                    cn837.Close()
                    cn837 = Nothing
                    'Else    'odbc
                    '    Dim cn837 As New Odbc.OdbcConnection(connstring)
                    '    cn837.Open()
                    '    Dim cmd837 As New Odbc.OdbcCommand(sSQL, cn837)
                    '    cmd837.CommandType = CommandType.Text
                    '    Dim dr837 As Odbc.OdbcDataReader = cmd837.ExecuteReader
                    '    If Not dr837.HasRows Then
                    '        MsgBox("No record found based on your criteria")
                    '    Else
                    '        While dr837.Read
                    '            If Claims(UBound(Claims)) <> "" Then ReDim Preserve Claims(UBound(Claims) + 1)
                    '            Claims(UBound(Claims)) = dr837("ID").ToString
                    '            'Invoices += Rs.Fields("ID").Value.ToString & ", "
                    '            If Accs(UBound(Accs)) <> "" Then ReDim Preserve Accs(UBound(Accs) + 1)
                    '            Accs(UBound(Accs)) = dr837("Accession_ID").ToString
                    '        End While
                    '    End If
                    '    cn837.Close()
                    '    cn837 = Nothing
                End If
                '
                Dim Path As String = My.Application.Info.DirectoryPath & "\"
                Dim DLL As String = Path & GetDLLName(ItemX.ItemData)
                If DLL <> "" Then
                    Dim Retvals() As String
                    Dim Plugin As System.Reflection.Assembly =
                    System.Reflection.Assembly.LoadFrom(DLL)
                    'Dim CLS As String = Plugin.GetName & ".ANSI837"
                    'FC = CreateObject("ProlisAvaility500.ANSI837")
                    FC = Plugin.CreateInstance(Plugin.GetTypes(5).FullName)
                    'FC = Plugin.CreateInstance("ProlisAvaility500.ANSI837")
                    Retvals = FC.InvoicesTo837(ItemX.ItemData, Claims, chkPT.Checked)
                    'FC.close()
                    FC = Nothing
                    If SystemConfig.BARHistory = True Then
                        Dim VALS() As String
                        For i = 0 To Retvals.Length - 1
                            VALS = GetBARValuesB(Val(Retvals(i)))
                            '0=AccID, 1=ArType, 2=ArID, 3=1, 4=InvID, 5=Amount
                            Log_BAR_Event(Val(Retvals(i)), Val(VALS(1)), Val(VALS(2)), 5, "837", Val(VALS(5)))
                        Next
                    End If
                    '
                    If SystemConfig.AuditTrail = True Then
                        LogUserEvent(ThisUser.ID, 37, Date.Now.ToString, "837", 0, Invoices, ToFile)
                    End If
                    '
                    Dim outclaims As String
                    Dim Missed As String = ""
                    If Retvals(0) <> "" AndAlso Retvals.Length > 1 Then
                        ToFile = "Prolis generated following 837 file containing " & (Retvals.Length - 1).ToString & " claims." &
                        vbCrLf & Retvals(0) & vbCrLf
                        If Claims.Length = Retvals.Length - 1 Then  'all transfered
                            ToFile += "File contains all 100% scheduled claims, listed below:" & vbCrLf
                            outclaims = Join(Claims, ", ")
                            ToFile += outclaims & vbCrLf
                        Else
                            For n As Integer = 0 To Claims.Length - 1
                                If Not TESTinTESTS(Claims(n), Retvals) Then
                                    Missed += Claims(n) & ", "
                                End If
                            Next
                            If Missed.EndsWith(", ") Then Missed = Microsoft.VisualBasic.Mid(Missed, 1, Len(Missed) - 2)
                            If Missed <> "" Then
                                ToFile += "Process was scheduled for " & Claims.Length.ToString & " claims but following claims " &
                                "could not be transfered. Print this list and use the Editor to rectify the issue." & vbCrLf &
                                "*********************" & vbCrLf
                                ToFile += Missed & vbCrLf & "*********************" & vbCrLf
                            End If
                        End If
                    End If
                    ToFile += vbCrLf & "Do you want to print this information ?"
                    Dim Retval As Integer = MsgBox(ToFile, MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Prolis")
                    If Retval = vbYes Then
                        Printer.Print(ToFile)

                    End If
                Else
                    MsgBox("837 Builder DLL is missing. Contact Prolis Support")
                End If
                '
                chk8371500.Checked = False : txtAccFrom.Text = "" : txtAccTo.Text = ""
                dtpDateFrom.Text = "" : dtpDateTo.Text = "" : txtProcessDate.Text = ""
                dgvDiscrete.Rows.Clear() : dgvDiscrete.RowCount = 1
                lstPayers.Items.Clear()
                cmbStatus.SelectedIndex = 2
                btnProcess.Enabled = False
            End If
        End If
    End Sub

    Private Function GetDLLName(ByVal PartnerID As Long) As String
        Dim DLL As String = ""
        Dim sSQL As String = "Select CommDLL from Partners where ID = " & PartnerID
        If connString <> "" Then
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
            'Else    'odbc
            '    Dim cndll As New Odbc.OdbcConnection(connstring)
            '    cndll.Open()
            '    Dim cmddll As New Odbc.OdbcCommand(sSQL, cndll)
            '    cmddll.CommandType = CommandType.Text
            '    Dim drdll As Odbc.OdbcDataReader = cmddll.ExecuteReader
            '    If drdll.HasRows Then
            '        While drdll.Read
            '            If drdll("CommDLL") IsNot DBNull.Value _
            '            AndAlso Trim(drdll("CommDLL")) <> "" Then _
            '            DLL = Trim(drdll("CommDLL"))
            '        End While
            '    End If
            '    cndll.Close()
            '    cndll = Nothing
        End If
        '
        Return DLL
    End Function

    Private Sub chkPT_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPT.CheckedChanged
        If chkPT.Checked = True Then
            chkPT.Text = "Production"
        Else
            chkPT.Text = "Test"
        End If
    End Sub

    Private Sub btnSelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelAll.Click
        If lstPayers.Items.Count > 0 Then
            Dim i As Integer
            For i = 0 To lstPayers.Items.Count - 1
                lstPayers.SetItemChecked(i, True)
            Next
        End If
        EBilloutProgress()
    End Sub

    Private Sub btnDeSel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeSel.Click
        If lstPayers.Items.Count > 0 Then
            Dim i As Integer
            For i = 0 To lstPayers.Items.Count - 1
                lstPayers.SetItemChecked(i, False)
            Next
        End If
        EBilloutProgress()
    End Sub

    Private Sub cmbStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbStatus.SelectedIndexChanged
        If cmbStatus.SelectedIndex = 1 Then     'Processed
            txtProcessDate.Enabled = True
        Else
            txtProcessDate.Text = ""
            txtProcessDate.Enabled = False
        End If
    End Sub

    Private Sub chkSvcBill_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSvcBill.CheckedChanged
        If chkSvcBill.Checked = False Then
            chkSvcBill.Text = "SERVICE"
        Else
            chkSvcBill.Text = "BILLING"
        End If
    End Sub

    Private Sub chkAccInv_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAccInv.CheckedChanged
        If chkAccInv.Checked = False Then
            chkAccInv.Text = "ACCESSION"
            grpAccRange.Text = "Accession Range"
        Else
            chkAccInv.Text = "INVOICE"
            grpAccRange.Text = "Invoice Range"
        End If
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
        Else
            If e.RowIndex < dgvDiscrete.RowCount - 1 Then
                dgvDiscrete.Rows.RemoveAt(e.RowIndex)
            End If
        End If
        EBilloutProgress()
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
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            If Clipboard.ContainsText Then
                dgvDiscrete.Rows.Clear()
                Dim Accs() As String = Split(Clipboard.GetText, vbCrLf)
                For i As Integer = 0 To Accs.Length - 1
                    If Trim(Accs(i)) <> "" Then
                        If dgvDiscrete.RowCount = 0 Then dgvDiscrete.Rows.Add()
                        If dgvDiscrete.Rows(dgvDiscrete.RowCount - 1).Cells(0).Value = "" Then
                            dgvDiscrete.Rows(dgvDiscrete.RowCount - 1).Cells(0).Value = Trim(Accs(i))
                        Else
                            dgvDiscrete.Rows.Add()
                            dgvDiscrete.Rows(dgvDiscrete.RowCount - 1).Cells(0).Value = Trim(Accs(i))
                        End If
                        txtAccFrom.Text = "" : txtAccTo.Text = ""
                        'txtDateFrom.Text = "" : txtDateTo.Text = ""
                        ClearDateTimePicker(dtpDateFrom)
                        ClearDateTimePicker(dtpDateTo)
                    End If
                Next
                If dgvDiscrete.Rows(dgvDiscrete.RowCount - 1).Cells(0).Value IsNot Nothing _
                AndAlso dgvDiscrete.Rows(dgvDiscrete.RowCount - 1).Cells(0).Value <> "" _
                Then dgvDiscrete.RowCount += 1
            End If
            EBilloutProgress()
        End If
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

    Private Sub dtpDateFrom_LostFocus(sender As Object, e As EventArgs) Handles dtpDateFrom.LostFocus, dtpDateTo.LostFocus
        EBilloutProgress()
    End Sub
End Class
