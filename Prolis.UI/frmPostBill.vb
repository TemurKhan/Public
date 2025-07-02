Imports System.Windows.Forms
Imports CrystalDecisions.CrystalReports.Engine
Imports Microsoft.Data.SqlClient

Public Class frmPostBill

    Private Sub frmPostBill_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbBillType.SelectedIndex = 1
        ConfigureDateTimePicker(dtpDateFrom)
        ConfigureDateTimePicker(dtpDateTo)

        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    'Private Sub txtDateFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    txtDateFrom.BackColor = FCOLOR
    'End Sub

    'Private Sub txtDateFrom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    If e.KeyCode = Keys.Enter Then
    '        SendKeys.Send("{TAB}")
    '    ElseIf e.KeyCode = Keys.Up Then
    '        SendKeys.Send("+{TAB}")
    '    End If
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
    '    UpdateProgress()
    'End Sub

    'Private Sub txtDateTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    txtDateTo.BackColor = FCOLOR
    'End Sub

    'Private Sub txtDateTo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    If e.KeyCode = Keys.Enter Then
    '        SendKeys.Send("{TAB}")
    '    ElseIf e.KeyCode = Keys.Up Then
    '        SendKeys.Send("+{TAB}")
    '    End If
    'End Sub

    'Private Sub txtDateTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    txtDateTo.BackColor = NFCOLOR
    'End Sub

    'Private Sub txtDateTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If UserEnteredText(txtDateTo) <> "" Then
    '        If IsDate(txtDateTo.Text) = True Then
    '            txtAccFrom.Text = ""
    '            txtAccTo.Text = ""
    '        Else
    '            MsgBox("Invalid Date")
    '            txtDateTo.Text = ""
    '            txtDateTo.Focus()
    '        End If
    '    End If
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
    End Sub

    Private Sub txtAccFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccFrom.Validated
        If txtAccFrom.Text <> "" Then
            'txtDateFrom.Text = ""
            'txtDateTo.Text = ""
            ClearDateTimePicker(dtpDateFrom)
            ClearDateTimePicker(dtpDateTo)
        End If
        UpdateProgress()
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
    End Sub

    Private Sub txtAccTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccTo.Validated
        If txtAccTo.Text <> "" Then
            'txtDateFrom.Text = ""
            'txtDateTo.Text = ""
            ClearDateTimePicker(dtpDateFrom)
            ClearDateTimePicker(dtpDateTo)
        End If
        UpdateProgress()
    End Sub

    Private Sub btnTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTarget.Click
        Dim sSQL As String = ""
        If IsDate(dtpDateFrom.Text) Or IsDate(dtpDateTo.Text) Or txtAccFrom.Text <> "" Or txtAccTo.Text <> "" Then
            If cmbBillType.SelectedIndex = 0 Then   'Client Billing
                sSQL = "Select distinct * from Providers where ID in (Select AR_ID from Charges " &
                "where ArType = 0"
            ElseIf cmbBillType.SelectedIndex = 1 Then   'Third Party
                sSQL = "Select distinct * from Payers where ID in (Select AR_ID from Charges " &
                "where ArType = 1"
            Else    'Patient
                sSQL = "Select distinct * from Patients where ID in (Select AR_ID from Charges " &
                "where ArType = 2"
            End If
            '
            Dim DateType As String = ""
            If cmbDateType.SelectedIndex = 0 Then   'Service
                DateType = "Svc_Date"
            ElseIf cmbDateType.SelectedIndex = 1 Then   'Service
                DateType = "Bill_Date"
            Else
                DateType = "837"
            End If
            Dim DocType As String = "Accession_ID"
            If chkAccInv.Checked = True Then DocType = "ID"
            '
            If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                If DateType <> "837" Then
                    sSQL += " and " & DateType & " between '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) _
                    & "' and '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) & " 23:59:00')"
                Else
                    sSQL += " and Accession_ID in (Select distinct Accession_ID from Req_Bar_History where " &
                    "Bar_Event_Type_ID = 5 and Bar_Event_Date between '" & Format(CDate(dtpDateFrom.Text),
                    SystemConfig.DateFormat) & "' and '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) & " 23:59:00'))"
                End If
            ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                If DateType <> "837" Then
                    sSQL += " and " & DateType & " between '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) _
                    & "' and '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) & " 23:59:00')"
                Else
                    sSQL += " and Accession_ID in (Select distinct Accession_ID from Req_Bar_History where " &
                    "Bar_Event_Type_ID = 5 and Bar_Event_Date between '" & Format(CDate(dtpDateTo.Text),
                    SystemConfig.DateFormat) & "' and '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) & " 23:59:00'))"
                End If
            ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                If DateType <> "837" Then
                    sSQL += " and " & DateType & " between '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) _
                    & "' and '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) & " 23:59:00')"
                Else
                    sSQL += " and Accession_ID in (Select distinct Accession_ID from Req_Bar_History where " &
                    "Bar_Event_Type_ID = 5 and Bar_Event_Date between '" & Format(CDate(dtpDateFrom.Text),
                    SystemConfig.DateFormat) & "' and '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) & " 23:59:00'))"
                End If
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                sSQL += " and " & DocType & " = " & Val(txtAccFrom.Text) & ")"
            ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                sSQL += " and " & DocType & " = " & Val(txtAccTo.Text) & ")"
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                If Val(txtAccFrom.Text) < Val(txtAccTo.Text) Then
                    sSQL += " and " & DocType & " >= " & Val(txtAccFrom.Text) &
                    " and " & DocType & " <= " & Val(txtAccTo.Text) & ")"
                ElseIf Val(txtAccFrom.Text) > Val(txtAccTo.Text) Then
                    sSQL += " and " & DocType & " <= " & Val(txtAccFrom.Text) &
                    " and " & DocType & " >= " & Val(txtAccTo.Text) & ")"
                Else
                    sSQL += " and " & DocType & " = " & Val(txtAccFrom.Text) & ")"
                End If
            End If
            '
            If cmbBillType.SelectedIndex = 0 Then   'Provider
                sSQL += " Order by LastName_BSN"
            ElseIf cmbBillType.SelectedIndex = 1 Then   'TP
                sSQL += " Order by PayerName"
            Else    'Patient
                sSQL += " Order by LastName, FirstName"
            End If
            '
            lstTargets.Items.Clear()
            'Dim Provider As String = ""
            'Dim CNP As New ADODB.Connection
            'CNP.Open(connstring)
            'Dim Rs As New ADODB.Recordset
            'Rs.Open(sSQL, CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            'If Not Rs.BOF Then
            '    Do Until Rs.EOF
            '        If cmbBillType.SelectedIndex = 0 Then   'Provider
            '            If Rs.Fields("IsIndividual").Value = 0 Then
            '                Provider = Rs.Fields("LastName_BSN").Value
            '            Else
            '                If Rs.Fields("Degree").Value Is System.DBNull.Value Then
            '                    Provider = Rs.Fields("LastName_BSN").Value & ", " & Rs.Fields("FirstName").Value & " MD"
            '                Else
            '                    Provider = Rs.Fields("LastName_BSN").Value & ", " & Rs.Fields("FirstName").Value & " " & Rs.Fields("Degree").Value
            '                End If
            '            End If
            '            lstTargets.Items.Add(New MyList(Provider, Rs.Fields("ID").Value))
            '        ElseIf cmbBillType.SelectedIndex = 1 Then   'Third Party
            '            lstTargets.Items.Add(New MyList(Rs.Fields("PayerName").Value, Rs.Fields("ID").Value))
            '        Else    'Patient
            '            lstTargets.Items.Add(New MyList(Rs.Fields("LastName").Value & ", " & Rs.Fields("FirstName").Value, Rs.Fields("ID").Value))
            '        End If
            '        lstTargets.SetItemChecked(lstTargets.Items.Count - 1, True)
            '        Rs.MoveNext()
            '    Loop
            'End If
            'Rs.Close()
            'Rs = Nothing
            'CNP.Close()
            'CNP = Nothing
            Dim Provider As String = ""

            Using connection As New SqlConnection(connString)
                connection.Open()

                Using command As New SqlCommand(sSQL, connection)
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            If cmbBillType.SelectedIndex = 0 Then ' Provider
                                If Convert.ToInt32(reader("IsIndividual")) = 0 Then
                                    Provider = reader("LastName_BSN").ToString()
                                Else
                                    Provider = reader("LastName_BSN").ToString() & ", " & reader("FirstName").ToString()
                                    If IsDBNull(reader("Degree")) Then
                                        Provider &= " MD"
                                    Else
                                        Provider &= " " & reader("Degree").ToString()
                                    End If
                                End If
                                lstTargets.Items.Add(New MyList(Provider, reader("ID")))
                            ElseIf cmbBillType.SelectedIndex = 1 Then ' Third Party
                                lstTargets.Items.Add(New MyList(reader("PayerName").ToString(), reader("ID")))
                            Else ' Patient
                                lstTargets.Items.Add(New MyList(reader("LastName").ToString() & ", " & reader("FirstName").ToString(), reader("ID")))
                            End If
                            lstTargets.SetItemChecked(lstTargets.Items.Count - 1, True)
                        End While
                    End Using
                End Using
            End Using

        Else
            lstTargets.Items.Clear()
        End If
        UpdateProgress()
    End Sub

    Private Sub UpdateProgress()
        If IsDate(dtpDateFrom.Text) Or IsDate(dtpDateTo.Text) Or lstTargets.CheckedItems.Count > 0 Then
            btnPrint.Enabled = True
        Else
            btnPrint.Enabled = False
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnDesel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDesel.Click
        Dim i As Integer
        For i = 0 To lstTargets.Items.Count - 1
            lstTargets.SetItemChecked(i, False)
        Next
        UpdateProgress()
    End Sub

    Private Sub btnSel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSel.Click
        Dim i As Integer
        For i = 0 To lstTargets.Items.Count - 1
            lstTargets.SetItemChecked(i, True)
        Next
        UpdateProgress()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If IsDate(dtpDateFrom.Text) Or IsDate(dtpDateTo.Text) Or txtAccFrom.Text <> "" Or
        txtAccTo.Text <> "" Or lstTargets.CheckedItems.Count > 0 Then
            Dim Formula As String = "{Charges.ArType} = " & cmbBillType.SelectedIndex
            Dim UID As String = My.Settings.UID.ToString
            Dim PWD As String = My.Settings.PWD.ToString
            Dim DateType As String = ""
            If cmbDateType.SelectedIndex = 0 Then   'Service
                DateType = "{Charges.Svc_Date}"
            ElseIf cmbDateType.SelectedIndex = 1 Then   'Service
                DateType = "{Charges.Bill_Date}"
            Else
                DateType = "837"
            End If
            Dim DocType As String = "{Charges.Accession_ID}"
            If chkAccInv.Checked = True Then DocType = "{Charges.ID}"
            If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                If DateType <> "837" Then
                    Formula += " and " & DateType & " in DateTime (" & Year(CDate(dtpDateFrom.Text)) _
                    & ", " & Month(CDate(dtpDateFrom.Text)) & ", " & CDate(dtpDateFrom.Text).Day &
                    ", 00, 00, 00) To DateTime(" & Year(CDate(dtpDateFrom.Text)) & ", " &
                    Month(CDate(dtpDateFrom.Text)) & ", " & CDate(dtpDateFrom.Text).Day & ", 23, 59, 59)"
                Else
                    Formula += " and {REQ_BAR_HISTORY.BAR_Event_Type_ID} = 5 and " &
                    "{REQ_BAR_HISTORY.BAR_Event_Date} in DateTime (" & Year(CDate(dtpDateFrom.Text)) _
                    & ", " & Month(CDate(dtpDateFrom.Text)) & ", " & CDate(dtpDateFrom.Text).Day &
                    ", 00, 00, 00) To DateTime(" & Year(CDate(dtpDateFrom.Text)) & ", " &
                    Month(CDate(dtpDateFrom.Text)) & ", " & CDate(dtpDateFrom.Text).Day & ", 23, 59, 59)"
                End If
            ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                If DateType <> "837" Then
                    Formula += " and " & DateType & " in DateTime (" & Year(CDate(dtpDateTo.Text)) _
                    & ", " & Month(CDate(dtpDateTo.Text)) & ", " & CDate(dtpDateTo.Text).Day &
                    ", 00, 00, 00) To DateTime(" & Year(CDate(dtpDateTo.Text)) & ", " &
                    Month(CDate(dtpDateTo.Text)) & ", " & CDate(dtpDateTo.Text).Day & ", 23, 59, 59)"
                Else
                    Formula += " and {REQ_BAR_HISTORY.BAR_Event_Type_ID} = 5 and " &
                    "{REQ_BAR_HISTORY.BAR_Event_Date} in DateTime (" & Year(CDate(dtpDateTo.Text)) _
                    & ", " & Month(CDate(dtpDateTo.Text)) & ", " & CDate(dtpDateTo.Text).Day &
                    ", 00, 00, 00) To DateTime(" & Year(CDate(dtpDateTo.Text)) & ", " &
                    Month(CDate(dtpDateTo.Text)) & ", " & CDate(dtpDateTo.Text).Day & ", 23, 59, 59)"
                End If
            ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                If DateType <> "837" Then
                    Formula += " and " & DateType & " in DateTime (" & Year(CDate(dtpDateFrom.Text)) _
                    & ", " & Month(CDate(dtpDateFrom.Text)) & ", " & CDate(dtpDateFrom.Text).Day &
                    ", 00, 00, 00) To DateTime(" & Year(CDate(dtpDateTo.Text)) & ", " &
                    Month(CDate(dtpDateTo.Text)) & ", " & CDate(dtpDateTo.Text).Day & ", 23, 59, 59)"
                Else
                    Formula += " and {REQ_BAR_HISTORY.BAR_Event_Type_ID} = 5 and " &
                    "{REQ_BAR_HISTORY.BAR_Event_Date} in DateTime (" & Year(CDate(dtpDateFrom.Text)) _
                    & ", " & Month(CDate(dtpDateFrom.Text)) & ", " & CDate(dtpDateFrom.Text).Day &
                    ", 00, 00, 00) To DateTime(" & Year(CDate(dtpDateTo.Text)) & ", " &
                    Month(CDate(dtpDateTo.Text)) & ", " & CDate(dtpDateTo.Text).Day & ", 23, 59, 59)"
                End If
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                Formula += " and " & DocType & " = " & Val(txtAccFrom.Text)
            ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                Formula += " and " & DocType & " = " & Val(txtAccTo.Text)
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                Formula += " and " & DocType & " in [" & Val(txtAccFrom.Text) & _
                " To " & Val(txtAccTo.Text) & "]"
            End If
            '
            If lstTargets.CheckedItems.Count > 0 Then
                Dim i As Integer
                Dim ARIDs As String = ""
                Dim ItemX As MyList
                For i = 0 To lstTargets.CheckedItems.Count - 1
                    ItemX = lstTargets.CheckedItems(i)
                    ARIDs += ItemX.ItemData.ToString & ","
                Next
                If ARIDs.Length > 2 Then
                    ARIDs = Microsoft.VisualBasic.Mid(ARIDs, 1, Len(ARIDs) - 1)
                    Formula += " and {Charges.Ar_ID} in [" & ARIDs & "]"
                End If
            End If
            '
            'Try
            '========================================
            'TODO: Crystal Reports Code

            'Dim gReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            'gReport.Load(Application.StartupPath & "\Reports\PostBilling.rpt", CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault)
            'gReport.SetDatabaseLogon(UID, PWD)
            'gReport.RecordSelectionFormula = Formula
            'frmRV.CRRV.ReportSource = gReport
            'frmRV.Show()
            'frmRV.MdiParent = ProlisQC
            '========================================
            'Catch ex As Exception
            'MsgBox(ex.ToString)
            'End Try
            FormClear()
        End If
    End Sub

    Private Sub cmbBillType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBillType.SelectedIndexChanged
        FormClear()
        PopulateDateTypes(cmbBillType.SelectedIndex)
    End Sub

    Private Sub PopulateDateTypes(ByVal BillType As Integer)
        cmbDateType.Items.Clear()
        If BillType = 1 Then
            cmbDateType.Items.Add("Service")
            cmbDateType.Items.Add("Billing")
            cmbDateType.Items.Add("837")
        Else
            cmbDateType.Items.Add("Service")
            cmbDateType.Items.Add("Billing")
        End If
        cmbDateType.SelectedIndex = 1
    End Sub

    Private Sub FormClear()
        'txtDateFrom.Text = "" : txtDateTo.Text = ""

        ClearDateTimePicker(dtpDateFrom)
        ClearDateTimePicker(dtpDateTo)
        txtAccFrom.Text = "" : txtAccTo.Text = ""
        lstTargets.Items.Clear()
    End Sub

    Private Sub chkAccInv_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAccInv.CheckedChanged
        If chkAccInv.Checked = False Then   'Acc
            chkAccInv.Text = "Accession"
            lblFrom.Text = "ACC From"
            lblTo.Text = "ACC To"
        Else
            chkAccInv.Text = "Invoice"
            lblFrom.Text = "INV From"
            lblTo.Text = "INV To"
        End If
    End Sub

    Private Sub lstTargets_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstTargets.GotFocus
        lstTargets.BackColor = FCOLOR
    End Sub

    Private Sub lstTargets_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstTargets.LostFocus
        lstTargets.BackColor = NFCOLOR
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
