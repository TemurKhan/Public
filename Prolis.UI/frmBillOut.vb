Imports System.Windows.Forms
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data
Imports Microsoft.Data.SqlClient

Imports System.IO ' Namespace for the Connect SDK



Imports System.Linq
Imports System.Threading.Tasks ' Required for async operations

Imports DymoSDK.Connect ' Namespace for the Connect SDK

Public Class frmBillOut

    'TODO: for Dymo Label update here
    'Public DymoAddIn As Dymo.DymoAddIn
    'Public DymoLabel As Dymo.DymoLabels
    '==========================
    Private Sub PopulatePayers(ByVal Sel As Boolean, ByVal BilledAccs() As String)
        Dim i As Integer
        Dim Accs As String = ""
        For i = LBound(BilledAccs) To UBound(BilledAccs)
            If BilledAccs(i) <> "" Then Accs += BilledAccs(i) & ", "
        Next
        If Accs.Length >= 2 Then Accs = Accs.Substring(0, Len(Accs) - 2)
        Dim sSQL As String = "Select distinct ID, PayerName, Address_ID from " _
        & "Payers where ID in (Select distinct Ar_ID from Charges where ArType " _
        & "= 1 and ID in (" & Accs & "))"
        lstPayers.Items.Clear()
        If Accs.Length Then
            Dim Address As String = ""
            Dim cnpp As New SqlConnection(connString)
            cnpp.Open()
            Dim cmdpp As New SqlCommand(sSQL, cnpp)
            cmdpp.CommandType = CommandType.Text
            Dim drpp As SqlDataReader = cmdpp.ExecuteReader
            If drpp.HasRows Then
                While drpp.Read
                    If drpp("Address_ID") Is DBNull.Value Then
                        Address = ""
                    Else
                        Address = GetAddress(drpp("Address_ID"))
                    End If
                    lstPayers.Items.Add(New MyList(drpp("PayerName") &
                    IIf(Address = "", "", ", " & Address), drpp("ID")))
                    lstPayers.SetItemChecked(lstPayers.Items.Count - 1, Sel)
                End While
            End If
            cnpp.Close()
            cnpp = Nothing
        End If
    End Sub

    Private Sub PopulateAccessions(ByVal Sel As Boolean, ByVal BilledAccs() As String)
        Dim Address As String = ""
        Dim Email As String = ""
        dgvAccessions.Rows.Clear()
        Dim INVIDS As String = Join(BilledAccs, ", ")
        If INVIDS <> "" Then
            Dim sSQL As String = "Select 1 as Sel, a.ID as Invoice, c.LastName + ', ' + c.FirstName as Patient, c.Sex, " &
            "Convert(nvarchar(12), c.DOB, 101) as DOB, d.Address1 + ', ' + d.City + ', ' + d.State + ' ' + d.Zip as Address, c.Email " &
            "from Charges a inner join (Requisitions b inner join (Patients c left outer join Addresses d on d.ID = c.Address_ID) " &
            "on c.ID = b.Patient_ID) on b.ID = a.Accession_ID where b.SpecimenType = 0 and a.ID in (" & INVIDS & ") Union Select " &
            "1 as Sel, e.ID as Invoice, 'Non Patient Invoice' as Patient, '' as Sex, '' as DOB, '' as Address, '' as Email from " &
            "Charges e inner join Requisitions f on e.Accession_ID = f.ID where f.SpecimenType <> 0 and e.ID in (" & INVIDS & ")"
            '
            Dim cnn As New SqlConnection(connString)
            cnn.Open()
            Dim CMD As New SqlCommand(sSQL, cnn)
            CMD.CommandType = Data.CommandType.Text
            Dim DR As SqlDataReader = CMD.ExecuteReader
            If DR.HasRows Then
                While DR.Read
                    If DR("Email") IsNot DBNull.Value Then
                        Email = Trim(DR("Email"))
                    Else
                        Email = ""
                    End If
                    dgvAccessions.Rows.Add(Sel, DR("Invoice"),
                    DR("Patient"), DR("Sex"), DR("DOB"), DR("Address"), Email)
                End While
            End If
            cnn.Close()
            cnn = Nothing
        End If
    End Sub

    Private Sub PopulateClients(ByVal Sel As Boolean, ByVal BilledAccs() As String)
        Dim Accs As String = Join(BilledAccs, ", ")
        Dim sSQL As String = "Select 1 as Sel, a.ID, a.LastName_BSN, a.FirstName, a.Degree, " &
        "a.IsIndividual, b.Address1, b.City, b.State, b.Zip from Providers a left outer join " &
        "Addresses b on a.Address_ID = b.ID where a.ID in (Select distinct " &
        "Ar_ID from Charges where ArType = 0 and ID in (" & Accs & "))"
        lstProviders.Items.Clear()
        If Accs <> "" Then
            Dim Provider As String = ""
            Dim Address As String = ""
            Dim cnpc As New SqlConnection(connString)
            cnpc.Open()
            Dim cmdpc As New SqlCommand(sSQL, cnpc)
            cmdpc.CommandType = CommandType.Text
            Dim drpc As SqlDataReader = cmdpc.ExecuteReader
            If drpc.HasRows Then
                While drpc.Read
                    If drpc("IsIndividual") IsNot DBNull.Value AndAlso drpc("IsIndividual") = 0 Then
                        Provider = drpc("LastName_BSN")
                    Else
                        If drpc("Degree") Is DBNull.Value Then
                            Provider = drpc("LastName_BSN") & ", " &
                            drpc("FirstName")
                        Else
                            Provider = drpc("LastName_BSN") & ", " &
                            drpc("FirstName") & " " & drpc("Degree")
                        End If
                    End If
                    If drpc("Address1") Is DBNull.Value Then
                        Address = ""
                    Else
                        Address = drpc("Address1") & ", " & drpc("City") _
                        & ", " & drpc("State") & " " & drpc("Zip")
                    End If
                    lstProviders.Items.Add(New MyList(Provider & Address, drpc("ID")))
                    lstProviders.SetItemChecked(lstProviders.Items.Count - 1, drpc("Sel"))
                End While
            End If
            cnpc.Close()
            cnpc = Nothing
        End If
    End Sub
    Private Sub FormDisable()
        grpDates.Enabled = False
        grpPayers.Enabled = False
        grpProviders.Enabled = False
        grpPrice.Enabled = False
    End Sub
    Private Sub FormEnable()
        grpDates.Enabled = True
        grpPayers.Enabled = True
        grpProviders.Enabled = True
        grpPrice.Enabled = True
    End Sub

    Private Sub frmBillOut_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CN.Open("DSN=ProlisQC")
        cmbBillingType.SelectedIndex = 1
        cmbMedium.SelectedIndex = 0
        btnProcess.Enabled = False
        txtCopies.Text = "1"

        ConfigureDateTimePicker(dtpDateFrom)
        ConfigureDateTimePicker(dtpDateTo)

        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    'Private Sub cmbBillingType_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBillingType.Validated
    '    If cmbBillingType.SelectedIndex = 0 Then    'Provider
    '        cmbMedium.Items.Clear()
    '        cmbMedium.Items.Add("Generic Provider Invoice")
    '        cmbMedium.SelectedIndex = 0
    '    ElseIf cmbBillingType.SelectedIndex = 1 Then    'Insurance
    '        cmbMedium.Items.Clear()
    '        cmbMedium.Items.Add("HCFA1500")
    '        cmbMedium.Items.Add("ANSI x12 837")
    '        cmbMedium.SelectedIndex = 1
    '    Else
    '        cmbMedium.Items.Clear()
    '        cmbMedium.Items.Add("Generic Patient Invoice")
    '        cmbMedium.SelectedIndex = 0
    '    End If
    '    FormClear()
    'End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Dim sSQL As String = ""
        Dim SelAccBase As Boolean
        If IsDate(dtpDateFrom.Text) = True And IsDate(dtpDateTo.Text) = False Then
            If chkSvcBill.Checked = False Then
                sSQL = "Select * from Charges where ArType = " & cmbBillingType.SelectedIndex _
                & " and Svc_Date between '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) &
                "' and '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) & " 23:59:00'"
            Else
                sSQL = "Select * from Charges where ArType = " & cmbBillingType.SelectedIndex _
                & " and Bill_Date between '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) &
                "' and '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) & " 23:59:00'"
            End If
            SelAccBase = False
        ElseIf IsDate(dtpDateFrom.Text) = False And IsDate(dtpDateTo.Text) = True Then
            If chkSvcBill.Checked = False Then
                sSQL = "Select * from Charges where ArType = " & cmbBillingType.SelectedIndex _
                & " and Svc_Date between '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) &
                "' and '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) & " 23:59:00'"
            Else
                sSQL = "Select * from Charges where ArType = " & cmbBillingType.SelectedIndex _
                & " and Bill_Date between '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) &
                "' and '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) & " 23:59:00'"
            End If
            SelAccBase = False
        ElseIf IsDate(dtpDateFrom.Text) = True And IsDate(dtpDateTo.Text) = True Then
            If chkSvcBill.Checked = False Then
                sSQL = "Select * from Charges where ArType = " & cmbBillingType.SelectedIndex _
                & " and Svc_Date between '" & dtpDateFrom.Text & "' and '" & dtpDateTo.Text & " 23:59:00'"
            Else
                sSQL = "Select * from Charges where ArType = " & cmbBillingType.SelectedIndex _
                & " and Bill_Date between '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) &
                "' and '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) & " 23:59:00'"
            End If
            SelAccBase = False
        ElseIf txtAccFrom.Text <> "" Or txtAccTo.Text <> "" Then
            If chkAccInv.Checked = False Then   'Accession
                If txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                    sSQL = "Select * from Charges where ArType = " & cmbBillingType.SelectedIndex _
                    & " and Accession_ID = " & Val(txtAccFrom.Text)
                    SelAccBase = True
                ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                    sSQL = "Select * from Charges where ArType = " & cmbBillingType.SelectedIndex _
                    & " and Accession_ID = " & Val(txtAccTo.Text)
                    SelAccBase = True
                ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                    sSQL = "Select * from Charges where ArType = " & cmbBillingType.SelectedIndex _
                    & " and Accession_ID between " & Val(txtAccFrom.Text) & " and " &
                    Val(txtAccTo.Text)
                    SelAccBase = True
                End If
            Else    'Invoice
                If txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                    sSQL = "Select * from Charges where ArType = " & cmbBillingType.SelectedIndex _
                    & " and ID = " & Val(txtAccFrom.Text)
                    SelAccBase = False
                ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                    sSQL = "Select * from Charges where ArType = " & cmbBillingType.SelectedIndex _
                    & " and ID = " & Val(txtAccTo.Text)
                    SelAccBase = False
                ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                    sSQL = "Select * from Charges where ArType = " & cmbBillingType.SelectedIndex _
                    & " and ID between " & Val(txtAccFrom.Text) & " and " &
                    Val(txtAccTo.Text)
                    SelAccBase = False
                End If
            End If
        End If
        '
        If sSQL <> "" Then
            '
            Dim BilledAccs() As String = {""}
            Dim cnchs As New SqlConnection(connString)
            cnchs.Open()
            Dim cmdchs As New SqlCommand(sSQL, cnchs)
            cmdchs.CommandType = CommandType.Text
            Try
                Dim drchs As SqlDataReader = cmdchs.ExecuteReader
                If drchs.HasRows Then
                    While drchs.Read
                        If cmbBillingType.SelectedIndex = 2 And chkInvs.Checked = False Then  ' non Zero bal Pat
                            If InvoiceBalance(drchs("ID")) <> 0 Then
                                If BilledAccs(UBound(BilledAccs)) <> "" Then _
                                ReDim Preserve BilledAccs(UBound(BilledAccs) + 1)
                                BilledAccs(UBound(BilledAccs)) = drchs("ID").ToString
                            End If
                        Else    'All
                            If BilledAccs(UBound(BilledAccs)) <> "" Then _
                             ReDim Preserve BilledAccs(UBound(BilledAccs) + 1)
                            BilledAccs(UBound(BilledAccs)) = drchs("ID").ToString
                        End If
                    End While
                    btnProcess.Enabled = True
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                cnchs.Close()
                cnchs = Nothing
            End Try
            PopulateAccessions(True, BilledAccs)
            txtPrintCount.Text = dgvAccessions.RowCount
            Dim ECs As Integer = 0
            For i As Integer = 0 To dgvAccessions.RowCount - 1
                If dgvAccessions.Rows(i).Cells(6).Value <> "" Then ECs += 1
            Next
            txtEmailCount.Text = ECs.ToString
            If cmbBillingType.SelectedIndex = 0 Then    'Client billing
                PopulateClients(True, BilledAccs)
            ElseIf cmbBillingType.SelectedIndex = 1 Then    'Third Party
                PopulatePayers(True, BilledAccs)
            End If
            'dtpDateFrom.Text = "" : dtpDateTo.Text = ""
            'txtAccFrom.Text = "" : txtAccTo.Text = ""
        Else
            FormClear()
        End If
        BillProcessProgress()
    End Sub

    Private Function InvoiceBalance(ByVal InvID As Long) As Single
        Dim Bal As Single = 0
        Dim sSQL As String = "Select a.GrossAmount - IsNull((Select Sum(AppliedAmount + WrittenOff) " &
        "from Payment_Detail where Charge_ID = a.ID), 0) as Bal from Charges a where a.ID = " & InvID
        Dim cnib As New SqlConnection(connString)
        cnib.Open()
        Dim cmdib As New SqlCommand(sSQL, cnib)
        cmdib.CommandType = CommandType.Text
        Dim drib As SqlDataReader = cmdib.ExecuteReader
        If drib.HasRows Then
            While drib.Read
                Bal = Math.Round(drib("Bal"), 2)
            End While
        End If
        cnib.Close()
        cnib = Nothing
        '
        Return Bal
    End Function

    Private Sub BillProcessProgress()
        Dim Payer As Boolean = False
        Dim Acc As Boolean = False
        Dim Provider As Boolean = False
        Dim Patient As Boolean = False
        Dim Media As Boolean = False
        Dim i As Integer
        For i = 0 To dgvAccessions.RowCount - 1
            If dgvAccessions.Rows(i).Cells(0).Value = True Then
                Acc = True
                Exit For
            End If
        Next
        '
        If cmbBillingType.SelectedIndex = 0 Then    'Provider
            If lstProviders.CheckedItems.Count > 0 Then Provider = True
            'If cmbMedium.SelectedIndex <> -1 Then Media = True
            Media = True
        ElseIf cmbBillingType.SelectedIndex = 1 Then    'Insurance
            Payer = True
            Media = True
        Else
            Patient = True
            Media = True
        End If
        If Acc = True And Media = True And (Provider = True Or Payer = True _
        Or Patient = True) Then
            btnProcess.Enabled = True
        Else
            btnProcess.Enabled = False
        End If
    End Sub
    Private Function HasRandomValues() As Boolean
        Dim HasVal As Boolean = False
        Dim i As Integer
        For i = 0 To dgvAccessions.RowCount - 1
            If dgvAccessions.Rows(i).Cells(1).Value.ToString <> String.Empty AndAlso
            dgvAccessions.Rows(i).Cells(1).Value.ToString <> "" Then
                HasVal = True
                Exit For
            End If
        Next
        HasRandomValues = HasVal
    End Function
    Private Sub FormClear()
        'txtDateFrom.Text = ""
        'txtDateTo.Text = ""

        ClearDateTimePicker(dtpDateFrom)
        ClearDateTimePicker(dtpDateTo)

        txtAccFrom.Text = ""
        txtAccTo.Text = ""
        DetailClear()
    End Sub
    Private Sub DetailClear()
        On Error Resume Next

        lstProviders.Items.Clear()
        lstPayers.Items.Clear()
        dgvAccessions.Rows.Clear()
        txtPrintCount.Clear()
        txtEmailCount.Clear()
    End Sub
    Private Sub ClearRandomAccessions()
        Dim i As Integer
        For i = dgvAccessions.RowCount - 1 To 0 Step -1
            If i = 0 Then
                dgvAccessions.Rows(i).Cells(1).Value = ""
                dgvAccessions.Rows(i).Cells(0).Value = False
                'System.Drawing.Image.FromFile(Application.StartupPath &
                '"\Images\Blank.ico")
            Else
                dgvAccessions.Rows.RemoveAt(i)
            End If
        Next
    End Sub

    Private Sub dgvAccessions_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAccessions.CellClick
        If e.ColumnIndex = 0 Then
            If dgvAccessions.Rows(e.RowIndex).Cells(1).Value.ToString <> String.Empty AndAlso
            dgvAccessions.Rows(e.RowIndex).Cells(1).Value.ToString <> "" Then
                If dgvAccessions.RowCount <> 1 Then
                    'dgvAccessions.Rows.RemoveAt(e.RowIndex)
                Else
                    dgvAccessions.Rows(e.RowIndex).Cells(1).Value = ""
                    dgvAccessions.Rows(e.RowIndex).Cells(0).Value = False
                    'System.Drawing.Image.FromFile(Application.StartupPath &
                    '    "\Images\Blank.ico")
                End If
            End If
        End If
    End Sub

    Private Sub dgvAccessions_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAccessions.CellValidated
        If e.ColumnIndex = 1 Then
            If dgvAccessions.Rows(e.RowIndex).Cells(1).Value.ToString <> String.Empty AndAlso
            dgvAccessions.Rows(e.RowIndex).Cells(1).Value.ToString <> "" Then
                If IsNumeric(dgvAccessions.Rows(e.RowIndex).Cells(1).Value) Then
                    dgvAccessions.Rows(e.RowIndex).Cells(0).Value = False
                    'System.Drawing.Image.FromFile(Application.StartupPath &
                    '"\Images\Delete.ico")
                End If
            End If
        End If
    End Sub

    Private Sub dgvAccessions_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAccessions.CellEndEdit
        If IsNumeric(dgvAccessions.Rows(e.RowIndex).Cells(1).Value) Then
            'FormClear()
            If e.RowIndex = dgvAccessions.RowCount - 1 Then _
            dgvAccessions.RowCount += 1
        Else
            dgvAccessions.Rows(e.RowIndex).Cells(1).Value = ""
            dgvAccessions.Rows(e.RowIndex).Cells(0).Value = False
            'System.Drawing.Image.FromFile(Application.StartupPath &
            '"\Images\Blank.ico")
        End If
    End Sub

    Private Sub txtAccFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccFrom.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAccTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccTo.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAccFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccFrom.Validated
        If txtAccFrom.Text <> "" Then
            'txtDateFrom.Text = ""
            'txtDateTo.Text = ""


            ClearDateTimePicker(dtpDateFrom)
            ClearDateTimePicker(dtpDateTo)
            DetailClear()
        End If
        BillProcessProgress()
    End Sub

    Private Sub txtAccTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccTo.Validated
        If txtAccTo.Text <> "" Then
            'txtDateFrom.Text = ""
            'txtDateTo.Text = ""
            ClearDateTimePicker(dtpDateFrom)
            ClearDateTimePicker(dtpDateTo)
            DetailClear()
        End If
        BillProcessProgress()
    End Sub

    Private Sub btnSelProv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelProv.Click
        If lstProviders.Items.Count > 0 Then
            Dim i As Integer
            For i = 0 To lstProviders.Items.Count - 1
                lstProviders.SetItemChecked(i, True)
            Next
            BillProcessProgress()
        End If
    End Sub

    Private Sub btnDeselProv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeselProv.Click
        If lstProviders.Items.Count > 0 Then
            Dim i As Integer
            For i = 0 To lstProviders.Items.Count - 1
                lstProviders.SetItemChecked(i, False)
            Next
            BillProcessProgress()
        End If
    End Sub

    Private Sub btnSelPayers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelPayers.Click
        If lstPayers.Items.Count > 0 Then
            Dim i As Integer
            For i = 0 To lstPayers.Items.Count - 1
                lstPayers.SetItemChecked(i, True)
            Next
            BillProcessProgress()
        End If
    End Sub

    Private Sub btnDeselPayers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeselPayers.Click
        If lstPayers.Items.Count > 0 Then
            Dim i As Integer
            For i = 0 To lstPayers.Items.Count - 1
                lstPayers.SetItemChecked(i, False)
            Next
            BillProcessProgress()
        End If
    End Sub

    Private Sub dgvAccessions_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvAccessions.LostFocus
        BillProcessProgress()
    End Sub

    'Private Sub txtDateFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If txtDateFrom.Text <> "" Then
    '        If IsDate(txtDateFrom.Text) = True Then
    '            txtAccFrom.Text = ""
    '            txtAccTo.Text = ""
    '            DetailClear()
    '        Else
    '            txtDateFrom.Text = ""
    '        End If
    '    End If
    '    BillProcessProgress()
    'End Sub

    'Private Sub txtDateTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If txtDateTo.Text <> "" Then
    '        If IsDate(txtDateTo.Text) = True Then
    '            txtAccFrom.Text = ""
    '            txtAccTo.Text = ""
    '            DetailClear()
    '        Else
    '            txtDateTo.Text = ""
    '        End If
    '    End If
    '    BillProcessProgress()
    'End Sub

    Private Sub txtCopies_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCopies.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtCopies_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCopies.Validated
        If txtCopies.Text = "" Then txtCopies.Text = "1"
    End Sub

    Private Function GetTPInvoiceFile(ByVal InvoiceID As Long) As String
        Dim InvFile As String = ""
        Dim sSQL As String = "Select DocFile from Payers where ID in (Select Ar_ID from Charges where ArType = 1 and ID = " & InvoiceID & ")"
        If connString <> "" Then
            Dim cntpi As New SqlConnection(connString)
            cntpi.Open()
            Dim cmdtpi As New SqlCommand(sSQL, cntpi)
            cmdtpi.CommandType = CommandType.Text
            Dim drtpi As SqlDataReader = cmdtpi.ExecuteReader
            If drtpi.HasRows Then
                While drtpi.Read
                    If drtpi("DocFile") IsNot DBNull.Value AndAlso Trim(drtpi("DocFile")) <> "" Then InvFile = Trim(drtpi("DocFile"))
                End While
            End If
            cntpi.Close()
            cntpi = Nothing
            'ElseIf connstring <> "" Then
            '    Dim cntpi As New Odbc.OdbcConnection(connstring)
            '    cntpi.Open()
            '    Dim cmdtpi As New Odbc.OdbcCommand(sSQL, cntpi)
            '    cmdtpi.CommandType = CommandType.Text
            '    Dim drtpi As Odbc.OdbcDataReader = cmdtpi.ExecuteReader
            '    If drtpi.HasRows Then
            '        While drtpi.Read
            '            If drtpi("DocFile") IsNot DBNull.Value AndAlso Trim(drtpi("DocFile")) <> "" Then InvFile = Trim(drtpi("DocFile"))
            '        End While
            '    End If
            '    cntpi.Close()
            '    cntpi = Nothing
        End If
        If InvFile <> "" Then
            If Not InvFile.Contains(My.Application.Info.DirectoryPath & "\Reports\") _
            Then InvFile = My.Application.Info.DirectoryPath & "\Reports\" & InvFile
            If Not System.IO.File.Exists(InvFile) Then InvFile = ""
        End If
        Return InvFile
    End Function

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click

        If cmbMedium.SelectedIndex = 1 AndAlso CountSelectedAccessions() > 4 Then
            btnProcess.Enabled = False
            MsgBox("The system allows maximum of 4 invoices to be viewed, at a time. " &
            "Check 4 or fewer invoices and try again.", MsgBoxStyle.Critical, "Prolis")

            Return
        End If

        Dim Formula As String = ""
        'Dim PatAccs As String = ""
        'Dim TPAccs As String = ""
        'Dim CLAccs As String = ""
        Dim LabelInfo() As String
        Dim i As Integer
        Dim ItemX As MyList
        Dim UID As String = My.Settings.UID.ToString
        Dim PWD As String = My.Settings.PWD.ToString
        Dim AccCount As Integer = GetAccCount()
        Dim Providers As String = GetProviders()
        Dim Accs As String = GetAccs()
        If cmbBillingType.SelectedIndex = 0 Then    'Client
            If lstProviders.CheckedItems.Count > 0 Then
                For i = 0 To lstProviders.CheckedItems.Count - 1
                    ItemX = lstProviders.CheckedItems(i)
                    If chkINVLBL.Checked = False Then   'Invoice

                        'TODO: for Crystal Report update here
                        '=============================
                        'Dim gReport As New ReportDocument
                        'gReport.Load(GetReportPath("Provider Invoice.RPT"))
                        'ApplyNewServer(gReport, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)
                        '=============================
                        ItemX = lstProviders.CheckedItems(i)
                        If txtAccFrom.Text <> "" Or txtAccTo.Text <> "" Then
                            If txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                                If chkAccInv.Checked = False Then   'Acc
                                    Formula = "{Charges.ArType} = 0 and {Charges.Ar_ID} = " &
                                    ItemX.ItemData & " and {Charges.Accession_ID} = " & Val(txtAccFrom.Text)
                                Else
                                    Formula = "{Charges.ArType} = 0 and {Charges.Ar_ID} = " &
                                    ItemX.ItemData & " and {Charges.ID} = " & Val(txtAccFrom.Text)
                                End If
                            ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                                If chkAccInv.Checked = False Then   'Acc
                                    Formula = "{Charges.ArType} = 0 and {Charges.Ar_ID} = " &
                                    ItemX.ItemData & " and {Charges.Accession_ID} = " & Val(txtAccTo.Text)
                                Else
                                    Formula = "{Charges.ArType} = 0 and {Charges.Ar_ID} = " &
                                    ItemX.ItemData & " and {Charges.ID} = " & Val(txtAccTo.Text)
                                End If
                            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                                If chkAccInv.Checked = False Then   'Acc
                                    Formula = "{Charges.ArType} = 0 and {Charges.Ar_ID} = " &
                                    ItemX.ItemData & " and {Charges.Accession_ID} in [" &
                                    Val(txtAccFrom.Text) & " To " & Val(txtAccTo.Text) & "]"
                                Else
                                    Formula = "{Charges.ArType} = 0 and {Charges.Ar_ID} = " &
                                    ItemX.ItemData & " and {Charges.ID} in [" &
                                    Val(txtAccFrom.Text) & " To " & Val(txtAccTo.Text) & "]"
                                End If
                            End If
                        Else
                            If chkSvcBill.Checked = False Then
                                If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                                    Formula = "{Charges.ArType} = 0 and {Charges.Ar_ID} = " _
                                    & ItemX.ItemData & " and {Charges.Svc_Date} in CDateTime(" &
                                    CDate(dtpDateFrom.Text).Year & ", " &
                                    CDate(dtpDateFrom.Text).Month & ", " &
                                    CDate(dtpDateFrom.Text).Day & ", 00, 00, 00) " &
                                    "To CDateTime(" & CDate(dtpDateFrom.Text).Year &
                                    ", " & CDate(dtpDateFrom.Text).Month & ", " &
                                    CDate(dtpDateFrom.Text).Day & ", 23, 59, 00)"
                                ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                                    Formula = "{Charges.ArType} = 0 and {Charges.Ar_ID} = " _
                                    & ItemX.ItemData & " and {Charges.Svc_Date} in " &
                                    "CDateTime(" & CDate(dtpDateTo.Text).Year & ", " &
                                    CDate(dtpDateTo.Text).Month & ", " &
                                    CDate(dtpDateTo.Text).Day & ", 00, 00, 00) To " &
                                    "CDateTime(" & CDate(dtpDateTo.Text).Year & ", " &
                                    CDate(dtpDateTo.Text).Month & ", " &
                                    CDate(dtpDateTo.Text).Day & ", 23, 59, 00)"
                                ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                                    Formula = "{Charges.ArType} = 0 and {Charges.Ar_ID} = " _
                                    & ItemX.ItemData & " and {Charges.Svc_Date} in CDateTime(" &
                                    CDate(dtpDateFrom.Text).Year & ", " &
                                    CDate(dtpDateFrom.Text).Month & ", " &
                                    CDate(dtpDateFrom.Text).Day & ", 00,00,00) To " &
                                    "CDateTime(" & CDate(dtpDateTo.Text).Year & ", " &
                                    CDate(dtpDateTo.Text).Month & ", " &
                                    CDate(dtpDateTo.Text).Day & ", 23, 59, 00)"
                                End If
                            Else    'Bill
                                If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                                    Formula = "{Charges.ArType} = 0 and {Charges.Ar_ID} = " _
                                    & ItemX.ItemData & " and {Charges.Bill_Date} in CDateTime(" &
                                    CDate(dtpDateFrom.Text).Year & ", " &
                                    CDate(dtpDateFrom.Text).Month & ", " &
                                    CDate(dtpDateFrom.Text).Day & ", 00, 00, 00) " &
                                    "To CDateTime(" & CDate(dtpDateFrom.Text).Year &
                                    ", " & CDate(dtpDateFrom.Text).Month & ", " &
                                    CDate(dtpDateFrom.Text).Day & ", 23, 59, 00)"
                                ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                                    Formula = "{Charges.ArType} = 0 and {Charges.Ar_ID} = " _
                                    & ItemX.ItemData & " and {Charges.Bill_Date} in " &
                                    "CDateTime(" & CDate(dtpDateTo.Text).Year & ", " &
                                    CDate(dtpDateTo.Text).Month & ", " &
                                    CDate(dtpDateTo.Text).Day & ", 00, 00, 00) To " &
                                    "CDateTime(" & CDate(dtpDateTo.Text).Year & ", " &
                                    CDate(dtpDateTo.Text).Month & ", " &
                                    CDate(dtpDateTo.Text).Day & ", 23, 59, 00)"
                                ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                                    Formula = "{Charges.ArType} = 0 and {Charges.Ar_ID} = " _
                                    & ItemX.ItemData & " and {Charges.Bill_Date} in CDateTime(" &
                                    CDate(dtpDateFrom.Text).Year & ", " &
                                    CDate(dtpDateFrom.Text).Month & ", " &
                                    CDate(dtpDateFrom.Text).Day & ", 00, 00, 00) To " &
                                    "CDateTime(" & CDate(dtpDateTo.Text).Year & ", " &
                                    CDate(dtpDateTo.Text).Month & ", " &
                                    CDate(dtpDateTo.Text).Day & ", 23, 59, 00)"
                                End If
                            End If
                        End If
                        '
                        If chkInvs.Checked = False Then 'non zero invoices
                            Formula += "and Not (if Not IsNull({Payment_Detail.TGP_ID}) then ({Charge_Detail.Extend} " &
                            "- {Payment_Detail.AppliedAmount} - {Payment_Detail.WrittenOff}) = 0;);"
                        End If
                        'TODO: for Crystal Report update here
                        '=============================

                        'gReport.RecordSelectionFormula = Formula
                        'If cmbMedium.SelectedIndex = 0 Then 'Printer
                        '    gReport.PrintOptions.PrinterName = GetDefaultPrinter()
                        '    gReport.PrintToPrinter(Val(txtCopies.Text), False, 0, 0)
                        '    ExecuteSqlProcedure("Update Charges Set Output = 1 where ArType" _
                        '    & " = 0 and Ar_ID = " & ItemX.ItemData)
                        '    My.Application.DoEvents()
                        '    gReport.Close()
                        '    gReport = Nothing
                        'Else    'Screen
                        '    frmRV.CRRV.ReportSource = gReport
                        '    frmRV.MdiParent = ProlisQC
                        '    frmRV.Show()
                        'End If

                        '=============================
                    Else    'Labels
                        LabelInfo = GetLabelInfo(0, ItemX.ItemData)
                        If LabelInfo(0) <> "" Then _
                        PrintLabels(LabelInfo, Val(txtCopies.Text))
                    End If
                Next
            End If
        ElseIf cmbBillingType.SelectedIndex = 1 Then    'Third Party
            If lstPayers.CheckedItems.Count > 0 And AccCount > 0 Then
                If chkINVLBL.Checked = False Then   'Invoice
                    If cmbMedium.SelectedIndex = 0 Then 'Printer
                        Dim RPTFile As String = GetReportPath("HCFA1500.RPT")
                        For i = 0 To dgvAccessions.RowCount - 1
                            If dgvAccessions.Rows(i).Cells(0).Value = True Then
                                Dim TempFile As String = GetTPInvoiceFile(dgvAccessions.Rows(i).Cells(1).Value)
                                If TempFile <> "" Then RPTFile = TempFile

                                'TODO: for Crystal Report update here
                                '=============================
                                'If chkInvs.Checked = False Then  'All
                                '    Dim gReport As New ReportDocument
                                '    gReport.Load(RPTFile)
                                '    ApplyNewServer(gReport, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)
                                '    gReport.RecordSelectionFormula = "{Charges.ArType} = 1 and {Charges.ID} = " _
                                '    & dgvAccessions.Rows(i).Cells(1).Value & ";"
                                '    gReport.PrintOptions.PrinterName = GetDefaultPrinter()
                                '    gReport.PrintToPrinter(Val(txtCopies.Text), False, 0, 0)
                                '    ExecuteSqlProcedure("Update Charges Set Output = 1 where ID = " &
                                '    dgvAccessions.Rows(i).Cells(1).Value)
                                '    My.Application.DoEvents()
                                '    gReport.Close()
                                '    gReport = Nothing
                                'Else    'Unprocessed
                                '    Dim gReport As New ReportDocument
                                '    gReport.Load(RPTFile)
                                '    ApplyNewServer(gReport, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)
                                '    gReport.RecordSelectionFormula = "{Charges.ArType} = 1 and {Charges.ID} = " _
                                '    & dgvAccessions.Rows(i).Cells(1).Value & " and {Charges.Output} = False;"
                                '    gReport.PrintOptions.PrinterName = GetDefaultPrinter()
                                '    gReport.PrintToPrinter(Val(txtCopies.Text), False, 0, 0)
                                '    ExecuteSqlProcedure("Update Charges Set Output = 1 where ID = " &
                                '    dgvAccessions.Rows(i).Cells(1).Value)
                                '    My.Application.DoEvents()
                                '    gReport.Close()
                                '    gReport = Nothing
                                'End If


                                '=============================
                            End If
                        Next
                    End If
                Else    'Labels
                    For i = 0 To lstPayers.CheckedItems.Count - 1
                        ItemX = lstPayers.CheckedItems(i)
                        LabelInfo = GetLabelInfo(1, ItemX.ItemData)
                        If LabelInfo(0) <> "" Then _
                        PrintLabels(LabelInfo, Val(txtCopies.Text))
                    Next
                End If
            End If
        Else    'Patient
            If AccCount > 0 Then
                Dim PatRPT As String = GetPatientInvoiceFile()
                If cmbMedium.SelectedIndex = 0 Then 'Printer
                    For i = 0 To dgvAccessions.RowCount - 1
                        If dgvAccessions.Rows(i).Cells(0).Value = True Then
                            If chkINVLBL.Checked = False Then   'Invoice
                                If chkPlanned.Checked = False OrElse
                                GetPrintConfigs(dgvAccessions.Rows(i).Cells(1).Value) Then

                                    'TODO: for Crystal Report update here
                                    '=============================

                                    'Dim gReport As New ReportDocument
                                    'gReport.Load(GetReportPath(PatRPT))
                                    'ApplyNewServer(gReport, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)
                                    'gReport.RecordSelectionFormula = "(if {Charges.IsPrimary} = False then {Charges_1.ArType} = 1 " &
                                    '"else {Charges_1.ArType} = 2;) and {Charges.ID} = " & dgvAccessions.Rows(i).Cells(1).Value & ";"
                                    'If chkPlanned.Checked = True Then _
                                    'UpdateChargePrints(dgvAccessions.Rows(i).Cells(1).Value)
                                    'gReport.PrintOptions.PrinterName = GetDefaultPrinter()
                                    'gReport.PrintToPrinter(Val(txtCopies.Text), False, 0, 0)
                                    'ExecuteSqlProcedure("Update Charges Set Output = 1 where ID = " &
                                    'dgvAccessions.Rows(i).Cells(1).Value)
                                    'My.Application.DoEvents()
                                    'gReport.Close()
                                    'gReport = Nothing

                                    '=============================
                                End If
                                'Else    'Unprocessed
                                '    Dim gReport As New ReportDocument
                                '    gReport.Load(My.Application.Info.DirectoryPath & "\Reports\Patient Invoice.RPT")
                                '    ApplyNewServer(gReport, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)
                                '    gReport.RecordSelectionFormula = "{Charges.ID} = " _
                                '    & dgvAccessions.Rows(i).Cells(1).Value & " and {Charges.ArType} " & _
                                '    "= 2 and {Charges.Output} = False;"
                                '    gReport.PrintOptions.PrinterName = GetDefaultPrinter()
                                '    gReport.PrintToPrinter(Val(txtCopies.Text), False, 0, 0)
                                '    CN.Execute("Update Charges Set Output = 1 where ID = " & _
                                '    dgvAccessions.Rows(i).Cells(1).Value)
                                '    My.Application.DoEvents()
                                '    gReport.Close()
                                '    gReport = Nothing
                                'End If
                            Else        'Label
                                LabelInfo = GetLabelInfo(2, dgvAccessions.Rows(i).Cells(1).Value)
                                If LabelInfo(0) <> "" Then _
                                PrintLabels(LabelInfo, Val(txtCopies.Text))
                            End If
                        End If
                    Next
                ElseIf cmbMedium.SelectedIndex = 1 Then  'screen
                    For i = 0 To dgvAccessions.RowCount - 1
                        If dgvAccessions.Rows(i).Cells(0).Value = True Then
                            If chkINVLBL.Checked = False Then   'Invoice
                                'If chkInvs.Checked = False Then  'All

                                'TODO: for Crystal Report update here
                                '=============================

                                'Dim gReport As New ReportDocument
                                'gReport.Load(GetReportPath(PatRPT))
                                'ApplyNewServer(gReport, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)
                                'gReport.RecordSelectionFormula = "(if {Charges.IsPrimary} = False then {Charges_1.ArType} = 1 " &
                                '"else {Charges_1.ArType} = 2;) and {Charges.ID} = " & dgvAccessions.Rows(i).Cells(1).Value & ";"
                                ''gReport.RecordSelectionFormula = "(if {Charges.IsPrimary} = False then " & _
                                '' "{Charges_1.ArType} = 1 Else {Charges_1.ArType} = 2;) and {Charges.ID} " & _
                                '' "= " & dgvAccessions.Rows(i).Cells(1).Value & ";"
                                'If chkPlanned.Checked = True Then _
                                'UpdateChargePrints(dgvAccessions.Rows(i).Cells(1).Value)
                                'frmRV.CRRV.ReportSource = gReport
                                'frmRV.ShowDialog()

                                'ExecuteSqlProcedure("Update Charges Set Output = 1 where ID = " &
                                'dgvAccessions.Rows(i).Cells(1).Value)
                                'My.Application.DoEvents()
                                'gReport.Close()
                                'gReport = Nothing
                                '=============================

                                ''Else    'Unprocessed
                                ''    Dim gReport As New ReportDocument
                                ''    gReport.Load(My.Application.Info.DirectoryPath & "\Reports\Patient Invoice.RPT")
                                ''    ApplyNewServer(gReport, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)
                                ''    gReport.RecordSelectionFormula = "{Charges.ID} = " _
                                ''    & dgvAccessions.Rows(i).Cells(1).Value & " and {Charges.ArType} " & _
                                ''    "= 2 and {Charges.Output} = False;"
                                ''    frmRV.CRRV.ReportSource = gReport
                                ''    frmRV.ShowDialog()
                                ''    CN.Execute("Update Charges Set Output = 1 where ID = " & _
                                ''    dgvAccessions.Rows(i).Cells(1).Value)
                                ''    My.Application.DoEvents()
                                ''    gReport.Close()
                                ''    gReport = Nothing
                                ''End If
                            Else        'Label
                                LabelInfo = GetLabelInfo(2, dgvAccessions.Rows(i).Cells(1).Value)
                                If LabelInfo(0) <> "" Then _
                                PrintLabels(LabelInfo, Val(txtCopies.Text))
                            End If
                        End If
                    Next
                Else    'Email
                    For n As Integer = 0 To dgvAccessions.RowCount - 1
                        If dgvAccessions.Rows(n).Cells(0).Value = True AndAlso
                        IsEmailValid(dgvAccessions.Rows(n).Cells(6).Value) Then
                            EmailInvoiceToPatient(dgvAccessions.Rows(n).Cells(1).Value, dgvAccessions.Rows(n).Cells(6).Value)
                        End If
                    Next
                End If
            End If
        End If
        FormClear()
        btnProcess.Enabled = False
    End Sub

    Private Function GetPatientInvoiceFile() As String
        Dim RPTFile As String = "Patient Invoice.RPT"
        Dim cnpif As New SqlConnection(connString)
        cnpif.Open()
        Dim cmdpif As New SqlCommand("Select PatientInvoiceFile " &
        "from System_Config where Company_ID = " & MyLab.ID, cnpif)
        cmdpif.CommandType = CommandType.Text
        Dim drpif As SqlDataReader = cmdpif.ExecuteReader
        If drpif.HasRows Then
            While drpif.Read
                If drpif("PatientInvoiceFile") IsNot DBNull.Value _
                AndAlso Trim(drpif("PatientInvoiceFile")) <> "" Then _
                RPTFile = drpif("PatientInvoiceFile")
            End While
        End If
        cnpif.Close()
        cnpif = Nothing
        Return RPTFile
    End Function

    Private Sub UpdateChargePrints(ByVal ChargeID As Long)
        ExecuteSqlProcedure("If Exists (Select * from ChargePrints where Charge_ID = " &
        ChargeID & ") Update ChargePrints set Print_Date = '" & Date.Now & "', " &
        "Print_Count = Print_Count + 1 where Charge_ID = " & ChargeID & " Else Insert " &
        "into ChargePrints (Charge_ID, Print_Date, Print_Count) values (" & ChargeID &
        ", '" & Date.Now & "', 1)")
    End Sub

    Private Function GetPrintConfigs(ByVal ChargeID As Long) As Boolean
        Dim ToPrint As Boolean = True
        Dim cnpc As New SqlConnection(connString)
        cnpc.Open()
        Dim cmdpc As New SqlCommand("Select * from " &
        "ChargePrints where Charge_ID = " & ChargeID, cnpc)
        cmdpc.CommandType = CommandType.Text
        Dim drpc As SqlDataReader = cmdpc.ExecuteReader
        If drpc.HasRows Then
            While drpc.Read
                If drpc("Print_Count") >= 3 Or
                Date.Today < DateAdd(DateInterval.Month, 1,
                drpc("Print_Date")) Then ToPrint = False
            End While
        End If
        cnpc.Close()
        cnpc = Nothing
        Return ToPrint
    End Function

    Private Sub PrintLabels(ByVal LabelInfo() As String, ByVal QTY As Integer)
        Dim Printer As String = GetLabelPrinterName()
        Dim lblFile As String = SystemConfig.AdrLblFile
        If lblFile = "" Then lblFile = "Dymo30334Adr.Label"
        'TODO: for Dymo Label update here

        'If InStr(Printer, "DYMO") > 0 Then
        '    DymoAddIn = New Dymo.DymoAddIn
        '    DymoLabel = New Dymo.DymoLabels
        '    If DymoAddIn.Open(My.Application.Info.DirectoryPath _
        '    & "\Reports\" & lblFile) Then
        '        DymoLabel.SetField("Name", LabelInfo(0))
        '        DymoLabel.SetField("Address", LabelInfo(1))
        '        DymoLabel.SetField("CSZ", LabelInfo(2))
        '        DymoLabel.SetField("Zip", LabelInfo(3))
        '        DymoAddIn.SelectPrinter(Printer)
        '        DymoAddIn.Print(QTY, False)
        '    ElseIf DymoAddIn.Open(My.Application.Info.DirectoryPath &
        '    "\Reports\Dymo30336Adr.Label") Then
        '        DymoLabel.SetField("Name", LabelInfo(0))
        '        DymoLabel.SetField("Address", LabelInfo(1))
        '        DymoLabel.SetField("CSZ", LabelInfo(2))
        '        DymoLabel.SetField("Zip", LabelInfo(3))
        '        DymoAddIn.SelectPrinter(Printer)
        '        DymoAddIn.Print(QTY, False)
        '    Else
        '        MsgBox("Dymo Label file can not be opened", MsgBoxStyle.Critical, "Prolis")
        '    End If
        '    DymoAddIn = Nothing
        '    DymoLabel = Nothing
        'End If
    End Sub

    Private Function GetLabelInfo(ByVal ArType As Integer, ByVal ArID As Long) As String()
        Dim LabelInfo() As String = {"", "", "", ""}
        Dim sSQL As String = ""
        Dim Add2 As String = ""
        If ArType = 0 Then    'Client
            sSQL = "Select * from Providers where ID = " & ArID
        ElseIf ArType = 1 Then    'TP
            sSQL = "Select * from Payers where ID = " & ArID
        Else    'Patient
            sSQL = "Select * from Patients where ID in (Select Patient_ID from Requisitions " &
            "where ID in (Select Accession_ID from Charges where ID = " & ArID & "))"
        End If
        Dim cnlbl As New SqlConnection(connString)
        cnlbl.Open()
        Dim cmdlbl As New SqlCommand(sSQL, cnlbl)
        cmdlbl.CommandType = CommandType.Text
        Dim drlbl As SqlDataReader = cmdlbl.ExecuteReader
        If drlbl.HasRows Then
            While drlbl.Read
                If ArType = 0 Then    'Client
                    If drlbl("IsIndividual") = True Then
                        LabelInfo(0) = Trim(drlbl("LastName_BSN")) _
                        & ", " & Trim(drlbl("FirstName")) &
                        IIf(drlbl("MiddleName") Is DBNull.Value,
                        "", " " & Trim(drlbl("MiddleName")))
                    Else
                        LabelInfo(0) = Trim(drlbl("LastName_BSN"))
                    End If
                    If drlbl("Address_ID") IsNot DBNull.Value _
                    AndAlso drlbl("Address_ID") > 0 Then
                        LabelInfo(1) = GetAddress1(drlbl("Address_ID"))
                        Add2 = GetAddress2(drlbl("Address_ID"))
                        If Add2 <> "" Then LabelInfo(1) += " " & Add2
                        LabelInfo(2) = GetAddressCSZ(drlbl("Address_ID"))
                        LabelInfo(3) = GetAddressZip(drlbl("Address_ID"))
                    Else
                        LabelInfo(1) = ""
                        LabelInfo(2) = ""
                        LabelInfo(3) = ""
                    End If
                ElseIf ArType = 1 Then    'TP
                    LabelInfo(0) = Trim(drlbl("PayerName"))
                    If drlbl("Address_ID") IsNot DBNull.Value _
                    AndAlso drlbl("Address_ID") > 0 Then
                        LabelInfo(1) = GetAddress1(drlbl("Address_ID"))
                        Add2 = GetAddress2(drlbl("Address_ID"))
                        If Add2 <> "" Then LabelInfo(1) += " " & Add2
                        LabelInfo(2) = GetAddressCSZ(drlbl("Address_ID"))
                        LabelInfo(3) = GetAddressZip(drlbl("Address_ID"))
                    Else
                        LabelInfo(1) = ""
                        LabelInfo(2) = ""
                        LabelInfo(3) = ""
                    End If
                Else    'Patient
                    LabelInfo(0) = Trim(drlbl("LastName")) _
                    & ", " & Trim(drlbl("FirstName")) &
                    IIf(drlbl("MiddleName") Is DBNull.Value,
                    "", " " & Trim(drlbl("MiddleName")))
                    If drlbl("Address_ID") IsNot DBNull.Value _
                    AndAlso drlbl("Address_ID") > 0 Then
                        LabelInfo(1) = GetAddress1(drlbl("Address_ID"))
                        Add2 = GetAddress2(drlbl("Address_ID"))
                        If Add2 <> "" Then LabelInfo(1) += " " & Add2
                        LabelInfo(2) = GetAddressCSZ(drlbl("Address_ID"))
                        LabelInfo(3) = GetAddressZip(drlbl("Address_ID"))
                    Else
                        LabelInfo(1) = ""
                        LabelInfo(2) = ""
                        LabelInfo(3) = ""
                    End If
                End If
            End While
        Else
            LabelInfo(0) = ""
            LabelInfo(1) = ""
            LabelInfo(2) = ""
            LabelInfo(3) = ""
        End If
        cnlbl.Close()
        cnlbl = Nothing
        Return LabelInfo
    End Function

    Private Function GetDLL(ByVal CLS As String) As String
        Dim DLL As String = ""
        Dim Data() As String
        Data = Split(CLS, ".")
        If Data.Length > 0 Then
            DLL = Trim(Data(0)) & ".DLL"
        End If
        Return DLL
    End Function

    Private Function GetProviders() As String
        Dim Providers As String = ""
        Dim ItemX As MyList
        Dim i As Integer
        If lstProviders.CheckedItems.Count > 0 Then
            For i = 0 To lstProviders.CheckedItems.Count - 1
                ItemX = lstProviders.CheckedItems(i)
                Providers += ItemX.ItemData.ToString & ", "
            Next
            Providers = Providers.Substring(0, Len(Providers) - 2)
        End If
        Return Providers
    End Function

    Private Function GetAccs() As String
        Dim Accs As String = ""
        Dim i As Integer
        For i = 0 To dgvAccessions.RowCount - 1
            If dgvAccessions.Rows(i).Cells(0).Value = True Then _
            Accs += dgvAccessions.Rows(i).Cells(1).Value.ToString & ", "
        Next
        If Accs.Length > 2 Then Accs = Accs.Substring(0, Len(Accs) - 2)
        Return Accs
    End Function

    Private Function GetAccCount() As Integer
        Dim Accs As Integer = 0
        Dim i As Integer
        For i = 0 To dgvAccessions.RowCount - 1
            If dgvAccessions.Rows(i).Cells(0).Value = True Then Accs += 1
        Next
        Return Accs
    End Function

    Private Sub cmbCHP_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCHP.SelectedIndexChanged
        BillProcessProgress()
    End Sub

    Private Sub lstProviders_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstProviders.SelectedValueChanged
        BillProcessProgress()
    End Sub

    Private Sub lstPayers_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstPayers.SelectedValueChanged
        BillProcessProgress()
    End Sub

    Private Sub btnSelAcc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelAcc.Click
        Dim Choice As Int16 = cmbMedium.SelectedIndex
        Dim i As Integer
        For i = 0 To dgvAccessions.RowCount - 1
            dgvAccessions.Rows(i).Cells(0).Value = True
        Next
        cmbMedium_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub btnDeselAcc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeselAcc.Click
        Dim i As Integer
        For i = 0 To dgvAccessions.RowCount - 1
            dgvAccessions.Rows(i).Cells(0).Value = False
        Next
    End Sub

    Private Sub dgvAccessions_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAccessions.CellValueChanged
        If e.RowIndex <> -1 And e.ColumnIndex = 0 Then
            BillProcessProgress()
        End If
    End Sub

    Private Sub btnTPACCS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTPACCS.Click
        Dim sSQL As String = ""
        'Dim SelAccBase As Boolean
        If IsDate(dtpDateFrom.Text) = True And IsDate(dtpDateTo.Text) = False Then
            sSQL = "Select * from Charges where ArType = " & cmbBillingType.SelectedIndex _
            & " and Svc_Date >= '" & CDate(dtpDateFrom.Text & " 00:00:01 AM") &
            "' and Svc_Date <= '" & CDate(dtpDateFrom.Text & " 11:59:59 PM") & "'"
            'SelAccBase = False
        ElseIf IsDate(dtpDateFrom.Text) = False And IsDate(dtpDateTo.Text) = True Then
            sSQL = "Select * from Charges where ArType = " & cmbBillingType.SelectedIndex _
            & " and Svc_Date >= '" & CDate(dtpDateTo.Text & " 00:00:01 AM") &
            "' and Svc_Date <= '" & CDate(dtpDateTo.Text & " 11:59:59 PM") & "'"
            'SelAccBase = False
        ElseIf IsDate(dtpDateFrom.Text) = True And IsDate(dtpDateTo.Text) = True Then
            sSQL = "Select * from Charges where ArType = " & cmbBillingType.SelectedIndex _
            & " and Svc_Date >= '" & CDate(dtpDateFrom.Text & " 00:00:01 AM") &
            "' and Svc_Date <= '" & CDate(dtpDateTo.Text & " 11:59:59 PM") & "'"
            'SelAccBase = False
        ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
            sSQL = "Select * from Charges where ArType = " & cmbBillingType.SelectedIndex _
            & " and Accession_ID = " & Val(txtAccFrom.Text)
            'SelAccBase = True
        ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
            sSQL = "Select * from Charges where ArType = " & cmbBillingType.SelectedIndex _
            & " and Accession_ID = " & Val(txtAccTo.Text)
            'SelAccBase = True
        ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
            If Val(txtAccFrom.Text) < Val(txtAccTo.Text) Then
                sSQL = "Select * from Charges where ArType = " & cmbBillingType.SelectedIndex _
                & " and Accession_ID >= " & Val(txtAccFrom.Text) & " and Accession_ID" _
                & " <= " & Val(txtAccTo.Text)
            Else
                sSQL = "Select * from Charges where ArType = " & cmbBillingType.SelectedIndex _
                & " and Accession_ID >= " & Val(txtAccTo.Text) & " and Accession_ID" _
                                & " <= " & Val(txtAccFrom.Text)
            End If
            'SelAccBase = True
        End If
        '
        If lstPayers.CheckedItems.Count = 0 Then
            sSQL = ""
        Else
            Dim ItemX As MyList
            Dim i As Integer
            Dim PAYERIDs As String = ""
            For i = 0 To lstPayers.Items.Count - 1
                If lstPayers.GetItemChecked(i) = True Then
                    ItemX = lstPayers.Items(i)
                    PAYERIDs += ItemX.ItemData.ToString & ", "
                End If
            Next
            If PAYERIDs.Length > 2 Then PAYERIDs = Microsoft.VisualBasic.Left(PAYERIDs, Len(PAYERIDs) - 2)
            sSQL += " and Ar_ID in (" & PAYERIDs & ")"
        End If
        If sSQL <> "" Then
            Dim BilledAccs() As String = {""}
            Dim cngba As New SqlConnection(connString)
            cngba.Open()
            Dim cmdgba As New SqlCommand(sSQL, cngba)
            cmdgba.CommandType = CommandType.Text
            Dim drgba As SqlDataReader = cmdgba.ExecuteReader
            If drgba.HasRows Then
                While drgba.Read
                    If BilledAccs(UBound(BilledAccs)) <> "" Then _
                    ReDim Preserve BilledAccs(UBound(BilledAccs) + 1)
                    BilledAccs(UBound(BilledAccs)) = drgba("Accession_ID").ToString
                End While
                btnProcess.Enabled = True
            End If
            cngba.Close()
            cngba = Nothing
            PopulateAccessions(True, BilledAccs)
        Else
            FormClear()
        End If
    End Sub

    Private Sub dgvAccessions_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAccessions.CellContentClick
        If e.RowIndex <> -1 Then
            If e.ColumnIndex = 0 Then
                If dgvAccessions.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = True Then
                    dgvAccessions.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = False
                Else
                    dgvAccessions.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = True
                End If
            End If
            'cmbMedium_SelectedIndexChanged(Nothing, Nothing)
        End If
    End Sub

    Private Sub chkSvcBill_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSvcBill.CheckedChanged
        If chkSvcBill.Checked = False Then  'SVC
            chkSvcBill.Text = "SVC"
        Else
            chkSvcBill.Text = "BILL"
        End If
    End Sub

    Private Sub chkINVLBL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkINVLBL.CheckedChanged
        If chkINVLBL.Checked = False Then   'Invoice
            chkINVLBL.Text = "Invoice"
        Else
            chkINVLBL.Text = "Label"
        End If
    End Sub

    Private Sub chkAccInv_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAccInv.CheckedChanged
        If chkAccInv.Checked = False Then
            chkAccInv.Text = "Accession Range"
        Else
            chkAccInv.Text = "Invoice Range"
        End If
    End Sub

    Private Sub chkInvs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkInvs.CheckedChanged
        If chkInvs.Checked = False Then
            chkInvs.Text = "Non Zero Invoices"
        Else
            chkInvs.Text = "All Invoices"
        End If
    End Sub

    Private Sub chkPlanned_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPlanned.CheckedChanged
        If chkPlanned.Checked = False Then
            chkPlanned.Text = "Unplanned"
        Else
            chkPlanned.Text = "Planned"
        End If
    End Sub

    Private Sub cmbBillingType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBillingType.SelectedIndexChanged
        If cmbBillingType.SelectedIndex = 2 Then    'Patient
            chkPlanned.Enabled = True
        Else
            chkPlanned.Checked = False
            chkPlanned.Enabled = False
        End If
    End Sub

    Private Sub cmbMedium_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMedium.SelectedIndexChanged
        If cmbMedium.SelectedIndex = 2 Then 'Email
            If Val(txtEmailCount.Text) > 0 Then
                btnProcess.Enabled = True
            Else
                btnProcess.Enabled = False
                MsgBox("None of your selected recipients has an email ID. In order " &
                "for the system to email the invoice, email ID is required. Update " &
                "patients with the email addresses and try again.", MsgBoxStyle.Critical, "Prolis")
            End If
        ElseIf cmbMedium.SelectedIndex = 1 Then 'screen
            'Dim Sels As Integer = 0
            'For i As Integer = 0 To dgvAccessions.RowCount - 1
            '    If dgvAccessions.Rows(i).Cells(0).Value = True Then Sels += 1
            '    If Sels > 4 Then Exit For
            'Next
            If CountSelectedAccessions() > 4 Then
                btnProcess.Enabled = False
                MsgBox("The system allows maximum of 4 invoices to be viewed, at a time. " &
                "Check 4 or fewer invoices and try again.", MsgBoxStyle.Critical, "Prolis")
            Else
                btnProcess.Enabled = True
            End If
        Else
            btnProcess.Enabled = True
        End If
    End Sub
    Private Function CountSelectedAccessions() As Integer
        Dim selectedCount As Integer = 0
        For Each row As DataGridViewRow In dgvAccessions.Rows
            If Convert.ToBoolean(row.Cells(0).Value) = True Then
                selectedCount += 1

            End If
        Next
        Return selectedCount
    End Function
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

    Private Sub dtpDateTo_LostFocus(sender As Object, e As EventArgs) Handles dtpDateTo.LostFocus, dtpDateFrom.LostFocus
        BillProcessProgress()
    End Sub




End Class
