Imports System.Data.SqlClient
Imports System.IO
Imports System.Runtime.Remoting
Imports System.Windows.Forms
Imports Microsoft.Data

Public Class frmPayments
    Public ArType As Integer = 1
    Public curRow As Integer = 0
    Private CellVal As String
    Private InvUnApp As Double = 0
    Private chkAmt As Double = 0
    Private SavedAmount As Double = 0
    Private SavedUnApp As Double = 0
    Private SavedApp As Double = 0
    Private BADACCTGP(1, 0) As String
    Private BADINV() As String
    Private CommentDirty As Boolean = False

    Private Sub frmPayments_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(13) Then
            e.KeyChar = Chr(0)
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub frmPayments_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtID.Text = GetNextPaymentID()
        cmbARType.SelectedIndex = 1
        ArType = cmbARType.SelectedIndex
        lblDated.Text += " (" & SystemConfig.DateFormat & ")"
        dtpEOBDate.Value = Format(Date.Today, SystemConfig.DateFormat)
        mtxtPostDate.Text = Now.Date.ToString(SystemConfig.DateFormat)
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
        newInv.Checked = True
        If AttachEOB.Text = "Viwe EOB" Then

            deleteeob.Show()

        Else

            eobPath.Text = ""
            deleteeob.Hide()
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub chkEditNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEditNew.Click
        FormClear()
        If chkEditNew.Checked = False Then  'New
            chkEditNew.Text = "New"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\New.ico")
            txtID.ReadOnly = True
            txtID.Text = GetNextPaymentID()
            btnPayLookUp.Enabled = False
            newInv.Checked = True
            mtxtPostDate.Text = Now.Date.ToString(SystemConfig.DateFormat)
        Else
            chkEditNew.Text = "Edit"
            newInv.Checked = False
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Edit.ico")
            txtID.ReadOnly = False
            txtID.Text = ""
            btnPayLookUp.Enabled = True
        End If
    End Sub

    Private Sub btnPayLookUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPayLookUp.Click
        Dim PmntID As String = frmPmntLookUp.ShowDialog()
        If PmntID <> "" Then DisplayPayment(Val(PmntID))
    End Sub

    'Private Sub DisplayPayment(ByVal PmtID As Long)
    '    Dim Billed As Double = 0
    '    Dim CNP As New ADODB.Connection
    '    CNP.Open(connString)
    '    Dim Rs As New ADODB.Recordset
    '    Rs.Open("Select * from Payments where ID = " & PmtID, CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Not Rs.BOF Then
    '        txtID.Text = Rs.Fields("ID").Value
    '        txtArID.Text = Rs.Fields("Ar_ID").Value
    '        cmbARType.SelectedIndex = Rs.Fields("ArType").Value
    '        txtARName.Text = GetARName(Rs.Fields("Ar_ID").Value, Rs.Fields("ArType").Value)
    '        cmbPaymentType.SelectedIndex = Rs.Fields("PaymentType").Value
    '        dtpEOBDate.Value = Format(Rs.Fields("PaymentDate").Value, SystemConfig.DateFormat)

    '        If IsDBNull(Rs.Fields("PostDate").Value) Then
    '            mtxtPostDate.Text = ""
    '        Else
    '            mtxtPostDate.Text = Format(Rs.Fields("PostDate").Value, SystemConfig.DateFormat)
    '        End If

    '        txtDoc.Text = Rs.Fields("DocNo").Value
    '        txtAmount.Text = Format(Rs.Fields("Amount").Value, "0.00")
    '        SavedAmount = Val(txtAmount.Text)
    '        'If txtDoc.Text <> "" And Val(txtAmount.Text) > 0 Then
    '        '    btnAutoApply.Enabled = True
    '        'Else
    '        '    btnAutoApply.Enabled = False
    '        'End If
    '        If Rs.Fields("UnApplied").Value IsNot System.DBNull.Value Then
    '            txtCKUnApplied.Text = Format(Rs.Fields("UnApplied").Value, "0.00")
    '        Else
    '            txtCKUnApplied.Text = "0.00"
    '        End If
    '        'SavedApp = GetSavedAppliedAmount(PmntID)
    '        'txtCKUnApplied.Text = Format(SavedAmount - SavedApp, "0.00")
    '        'SavedUnApp = SavedAmount - SavedApp
    '        txtCKApplied.Text = Format(Rs.Fields("Amount").Value _
    '        - Val(txtCKUnApplied.Text), "0.00")
    '        txtInvApplied.Text = "" : txtInvUnApplied.Text = ""
    '        btnVoidCK.Enabled = True
    '    End If
    '    Rs.Close()
    '    dgvPayment.Rows.Clear()
    '    Dim Invoices() As String = GetPaidInvoices(PmtID)
    '    dgvInvoices.Rows.Clear()
    '    For i As Integer = 0 To Invoices.Length - 1
    '        If Invoices(i) <> "" Then
    '            PopulateInvoice(Invoices(i))
    '        End If
    '    Next
    '    Dim IApp As Double = 0
    '    Rs.Open("Select * from Payment_Detail where Payment_ID = " & PmtID, CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Not Rs.BOF Then
    '        Dim BillData() As String = {"", ""}
    '        Dim TSTCPT As String = ""
    '        Dim Balance As Double = 0
    '        Dim Unapplied As Double = 0
    '        'Dim RBDATA() As String
    '        Do Until Rs.EOF
    '            'BillData = Rs.Fields("ChargeAmount").Value
    '            TSTCPT = GetTstCPT(Rs.Fields("TGP_ID").Value)
    '            If Rs.Fields("ChargeAmount").Value IsNot System.DBNull.Value Then
    '                BillData(1) = Rs.Fields("ChargeAmount").Value.ToString
    '            Else
    '                BillData = GetBillData(Rs.Fields("Charge_ID").Value,
    '                Rs.Fields("TGP_ID").Value)
    '            End If
    '            '
    '            If Rs.Fields("Balance").Value Is System.DBNull.Value Then
    '                Balance = 0
    '            Else
    '                Balance = Rs.Fields("Balance").Value
    '            End If
    '            '- Rs.Fields("AppliedAmount").Value, 2)
    '            dgvPayment.Rows.Add(Rs.Fields("Charge_ID").Value,
    '            Rs.Fields("TGP_ID").Value, TSTCPT, Format(Val(BillData(1)), "#0.00"),
    '            Format(Rs.Fields("AppliedAmount").Value, "#0.00"), Format(
    '            Rs.Fields("WrittenOff").Value, "0.00"), Format(Balance, "#0.00"),
    '            False, IIf(Rs.Fields("RebillAmount").Value Is System.DBNull.Value,
    '            "0.00", Format(Rs.Fields("RebillAmount").Value, "0.00")), "")
    '            If Val(dgvPayment.Rows(dgvPayment.RowCount - 1).Cells(8).Value) = 0 Then 'no WO
    '                dgvPayment.Rows(dgvPayment.RowCount - 1).Cells(9).Value = False
    '            Else
    '                dgvPayment.Rows(dgvPayment.RowCount - 1).Cells(9).Value = True
    '            End If
    '            Billed += Val(dgvPayment.Rows(dgvPayment.RowCount - 1).Cells(3).Value)
    '            IApp += Val(dgvPayment.Rows(dgvPayment.RowCount - 1).Cells(4).Value)
    '            Rs.MoveNext()
    '        Loop
    '        If dgvPayment.RowCount > 0 Then
    '            btnUnApply.Enabled = True
    '            btnSave.Enabled = True
    '        End If
    '    End If
    '    Rs.Close()
    '    Rs = Nothing
    '    CNP.Close()
    '    CNP = Nothing
    '    txtCKUnApplied.Text = Format(Val(txtAmount.Text) - IApp, "#0.00")
    '    txtCKApplied.Text = Format(IApp, "#0.00")
    '    txtInvApplied.Text = Format(IApp, "#0.00")
    '    txtBilled.Text = Format(Billed, "#0.00")
    '    ReDim BADACCTGP(1, 0) : ReDim BADINV(0)
    'End Sub
    Private Sub DisplayPayment(ByVal PmtID As Long)
        Dim Billed As Double = 0
        Dim queryPayment As String = "SELECT * FROM Payments WHERE ID = @PaymentID"
        Dim queryPaymentDetail As String = "SELECT * FROM Payment_Detail WHERE Payment_ID = @PaymentID"

        Using connection As New SqlConnection(connString) ' Old: Dim CNP As New ADODB.Connection
            connection.Open() ' Old: CNP.Open(connString)

            ' Retrieve payment data
            Using commandPayment As New SqlCommand(queryPayment, connection)
                commandPayment.Parameters.AddWithValue("@PaymentID", PmtID) ' Parameterized query for security

                Using readerPayment As SqlDataReader = commandPayment.ExecuteReader() ' Old: Rs.Open(...)
                    If readerPayment.HasRows Then ' Old: If Not Rs.BOF Then
                        readerPayment.Read()

                        txtID.Text = readerPayment("ID").ToString() ' Old: txtID.Text = Rs.Fields("ID").Value
                        txtArID.Text = readerPayment("Ar_ID").ToString() ' Old: txtArID.Text = Rs.Fields("Ar_ID").Value
                        cmbARType.SelectedIndex = Convert.ToInt32(readerPayment("ArType")) ' Old: cmbARType.SelectedIndex = Rs.Fields("ArType").Value
                        txtARName.Text = GetARName(Convert.ToInt32(readerPayment("Ar_ID")), Convert.ToInt32(readerPayment("ArType"))) ' Old: GetARName(...)
                        cmbPaymentType.SelectedIndex = Convert.ToInt32(readerPayment("PaymentType")) ' Old: cmbPaymentType.SelectedIndex = Rs.Fields("PaymentType").Value
                        dtpEOBDate.Value = Convert.ToDateTime(readerPayment("PaymentDate")) ' Old: Format(...)

                        mtxtPostDate.Text = If(IsDBNull(readerPayment("PostDate")), "", Format(Convert.ToDateTime(readerPayment("PostDate")), SystemConfig.DateFormat)) ' Old: Format(...)

                        txtDoc.Text = readerPayment("DocNo").ToString() ' Old: txtDoc.Text = Rs.Fields("DocNo").Value
                        txtAmount.Text = Format(Convert.ToDecimal(readerPayment("Amount")), "0.00") ' Old: Format(...)

                        SavedAmount = Convert.ToDouble(txtAmount.Text) ' Old: SavedAmount = Val(...)

                        txtCKUnApplied.Text = If(IsDBNull(readerPayment("UnApplied")), "0.00", Format(Convert.ToDecimal(readerPayment("UnApplied")), "0.00")) ' Old: Format(...)

                        txtCKApplied.Text = Format(Convert.ToDecimal(readerPayment("Amount")) - Convert.ToDecimal(txtCKUnApplied.Text), "0.00") ' Old: txtCKApplied.Text = ...
                        txtInvApplied.Text = "" : txtInvUnApplied.Text = ""
                        btnVoidCK.Enabled = True
                    End If
                End Using
            End Using

            dgvPayment.Rows.Clear()
            Dim Invoices() As String = GetPaidInvoices(PmtID)
            dgvInvoices.Rows.Clear()
            For i As Integer = 0 To Invoices.Length - 1
                If Invoices(i) <> "" Then
                    PopulateInvoice(Invoices(i)) ' Retained original logic
                End If
            Next

            ' Retrieve payment detail data
            Dim IApp As Double = 0
            Using commandPaymentDetail As New SqlCommand(queryPaymentDetail, connection)
                commandPaymentDetail.Parameters.AddWithValue("@PaymentID", PmtID)

                Using readerPaymentDetail As SqlDataReader = commandPaymentDetail.ExecuteReader() ' Old: Rs.Open(...)
                    Dim BillData() As String = {"", ""}
                    Dim TSTCPT As String = ""
                    Dim Balance As Double = 0

                    While readerPaymentDetail.Read() ' Old: Do Until Rs.EOF
                        TSTCPT = GetTstCPT(Convert.ToInt32(readerPaymentDetail("TGP_ID"))) ' Old: GetTstCPT(...)

                        BillData(1) = If(IsDBNull(readerPaymentDetail("ChargeAmount")), GetBillData(Convert.ToInt32(readerPaymentDetail("Charge_ID")), Convert.ToInt32(readerPaymentDetail("TGP_ID")))(1), readerPaymentDetail("ChargeAmount").ToString())

                        Balance = If(IsDBNull(readerPaymentDetail("Balance")), 0, Convert.ToDouble(readerPaymentDetail("Balance")))

                        dgvPayment.Rows.Add(
                        readerPaymentDetail("Charge_ID").ToString(),
                        readerPaymentDetail("TGP_ID").ToString(),
                        TSTCPT,
                        Format(Convert.ToDouble(BillData(1)), "#0.00"),
                        Format(Convert.ToDouble(readerPaymentDetail("AppliedAmount")), "#0.00"),
                        Format(Convert.ToDouble(readerPaymentDetail("WrittenOff")), "0.00"),
                        Format(Balance, "#0.00"),
                        False,
                        If(IsDBNull(readerPaymentDetail("RebillAmount")), "0.00", Format(Convert.ToDouble(readerPaymentDetail("RebillAmount")), "0.00")),
                        ""
                    )

                        If Convert.ToDouble(dgvPayment.Rows(dgvPayment.RowCount - 1).Cells(8).Value) = 0 Then
                            dgvPayment.Rows(dgvPayment.RowCount - 1).Cells(9).Value = False
                        Else
                            dgvPayment.Rows(dgvPayment.RowCount - 1).Cells(9).Value = True
                        End If

                        Billed += Convert.ToDouble(dgvPayment.Rows(dgvPayment.RowCount - 1).Cells(3).Value)
                        IApp += Convert.ToDouble(dgvPayment.Rows(dgvPayment.RowCount - 1).Cells(4).Value)
                    End While

                    If dgvPayment.RowCount > 0 Then
                        btnUnApply.Enabled = True
                        btnSave.Enabled = True
                    End If
                End Using
            End Using

            txtCKUnApplied.Text = Format(Convert.ToDouble(txtAmount.Text) - IApp, "#0.00")
            txtCKApplied.Text = Format(IApp, "#0.00")
            txtInvApplied.Text = Format(IApp, "#0.00")
            txtBilled.Text = Format(Billed, "#0.00")
            ReDim BADACCTGP(1, 0)
            ReDim BADINV(0)
        End Using
    End Sub
    Private Function GetPaidInvoices(ByVal PmntID As Long) As String()
        Dim INVS() As String = {""} ' Old: Dim INVS() As String = {""}

        Dim query As String = "SELECT DISTINCT Charge_ID FROM Payment_Detail WHERE Payment_ID = @PaymentID"

        Using connection As New SqlConnection(connString) ' Old: Dim CNP As New ADODB.Connection
            connection.Open() ' Old: CNP.Open(connString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@PaymentID", PmntID) ' Replaced direct concatenation with parameterized query

                Using reader As SqlDataReader = command.ExecuteReader() ' Old: Rs.Open(...)
                    While reader.Read() ' Old: Do Until Rs.EOF
                        If INVS(UBound(INVS)) <> "" Then ReDim Preserve INVS(UBound(INVS) + 1) ' Preserve array size
                        INVS(UBound(INVS)) = reader("Charge_ID").ToString() ' Old: Rs.Fields("Charge_ID").Value.ToString
                    End While
                End Using
            End Using
        End Using ' Old: Rs.Close(), Rs = Nothing, CNP.Close(), CNP = Nothing

        Return INVS
    End Function

    Private Function GetOtherPayments(ByVal PmntID As Long, ByVal TGPID As Integer, ByVal ChargeID As Long) As Double
        Dim OPMNT As Double = 0 ' Old: Dim OPMNT As Double = 0

        Dim query As String = "SELECT * FROM Payment_Detail WHERE TGP_ID = @TGPID AND Charge_ID = @ChargeID AND Payment_ID <> @PaymentID"

        Using connection As New SqlConnection(connString) ' Old: Dim CNP As New ADODB.Connection
            connection.Open() ' Old: CNP.Open(connString)
            Using command As New SqlCommand(query, connection)
                ' Adding parameters to ensure security
                command.Parameters.AddWithValue("@TGPID", TGPID)
                command.Parameters.AddWithValue("@ChargeID", ChargeID)
                command.Parameters.AddWithValue("@PaymentID", PmntID)

                Using reader As SqlDataReader = command.ExecuteReader() ' Old: Rs.Open(...)
                    If reader.HasRows Then ' Old: If Not Rs.BOF Then
                        reader.Read()
                        OPMNT = If(IsDBNull(reader("AppliedAmount")), 0, Convert.ToDouble(reader("AppliedAmount"))) + ' Old: Rs.Fields("AppliedAmount").Value
                            If(IsDBNull(reader("WrittenOff")), 0, Convert.ToDouble(reader("WrittenOff"))) + ' Old: Rs.Fields("WrittenOff").Value
                            If(IsDBNull(reader("RebillAmount")), 0, Convert.ToDouble(reader("RebillAmount"))) ' Old: Rs.Fields("RebillAmount").Value
                    End If
                End Using
            End Using
        End Using ' Old: Rs.Close(), Rs = Nothing, CNP.Close(), CNP = Nothing

        Return OPMNT ' Old: Return OPMNT
    End Function

    Private Function GetRBDATA(ByVal AccID As Long, ByVal TGPID As Integer) As String()
        Dim RBDATA() As String = {"", "", "", ""} ' Old: Dim RBDATA() As String = {"", "", "", ""}
        Dim query As String = "SELECT BillingType_ID, LinePrice, Reason_Code FROM Req_Rebillable WHERE Accession_ID = @AccID AND TGP_ID = @TGPID"

        Using connection As New SqlConnection(connString) ' Old: Dim CNP As New ADODB.Connection
            connection.Open() ' Old: CNP.Open(connString)
            Using command As New SqlCommand(query, connection)
                ' Add parameters to prevent SQL injection
                command.Parameters.AddWithValue("@AccID", AccID)
                command.Parameters.AddWithValue("@TGPID", TGPID)

                Using reader As SqlDataReader = command.ExecuteReader() ' Old: Rs.Open(...)
                    While reader.Read() ' Old: Do Until Rs.EOF
                        Select Case Convert.ToInt32(reader("BillingType_ID")) ' Old: Rs.Fields("BillingType_ID").Value
                            Case 0
                                RBDATA(0) = reader("LinePrice").ToString() ' Old: Rs.Fields("LinePrice").Value.ToString
                            Case 1
                                RBDATA(1) = reader("LinePrice").ToString() ' Old: Rs.Fields("LinePrice").Value.ToString
                            Case Else
                                RBDATA(2) = reader("LinePrice").ToString() ' Old: Rs.Fields("LinePrice").Value.ToString
                        End Select
                        RBDATA(3) = If(IsDBNull(reader("Reason_Code")), "", Trim(reader("Reason_Code").ToString())) ' Old: Rs.Fields("Reason_Code").Value
                    End While
                End Using
            End Using
        End Using ' Old: Rs.Close(), Rs = Nothing, CNP.Close(), CNP = Nothing

        Return RBDATA ' Old: Return RBDATA
    End Function

    Private Function GetBillData(ByVal ChargeID As Long, ByVal TGPID As Integer) As String()
        Dim BillData() As String = {"", ""} ' Old: Dim BillData() As String = {"", ""}

        Dim query As String = "SELECT a.Accession_ID AS AccID, b.Extend " &
                          "FROM Charges a INNER JOIN Charge_Detail b ON a.ID = b.Charge_ID " &
                          "WHERE a.ID = @ChargeID AND b.TGP_ID = @TGPID"

        Using connection As New SqlConnection(connString) ' Old: Dim CNP As New ADODB.Connection
            connection.Open() ' Old: CNP.Open(connString)
            Using command As New SqlCommand(query, connection)
                ' Add parameters to secure the query
                command.Parameters.AddWithValue("@ChargeID", ChargeID)
                command.Parameters.AddWithValue("@TGPID", TGPID)

                Using reader As SqlDataReader = command.ExecuteReader() ' Old: Rs.Open(...)
                    If reader.HasRows Then ' Old: If Not Rs.BOF Then
                        reader.Read()
                        BillData(0) = reader("AccID").ToString() ' Old: Rs.Fields("AccID").Value.ToString
                        BillData(1) = reader("Extend").ToString() ' Old: Rs.Fields("Extend").Value.ToString
                    End If
                End Using
            End Using
        End Using ' Old: Rs.Close(), Rs = Nothing, CNP.Close(), CNP = Nothing

        Return BillData ' Old: Return BillData
    End Function

    Private Function GetARName(ByVal ArID As Long, ByVal ArType As Integer) As String
        Dim ArName As String = "" ' Old: Dim ArName As String = ""

        Using connection As New SqlConnection(connString) ' Old: Dim CNP As New ADODB.Connection
            connection.Open() ' Old: CNP.Open(connString)

            Dim query As String = ""
            If ArType = 0 Then ' Client
                query = "SELECT IsIndividual, LastName_BSN, FirstName, Degree FROM Providers WHERE ID = @ArID"
            ElseIf ArType = 1 Then ' TP
                query = "SELECT PayerName FROM Payers WHERE ID = @ArID"
            Else ' Patient
                query = "SELECT LastName, FirstName FROM Patients WHERE ID = @ArID"
            End If

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@ArID", ArID) ' Use parameterized query for security

                Using reader As SqlDataReader = command.ExecuteReader() ' Old: Rs.Open(...)
                    If reader.HasRows Then ' Old: If Not Rs.BOF Then
                        reader.Read()

                        If ArType = 0 Then ' Client
                            If Convert.ToInt32(reader("IsIndividual")) = 0 Then
                                ArName = reader("LastName_BSN").ToString() ' Old: Rs.Fields("LastName_BSN").Value
                            Else
                                ArName = reader("LastName_BSN").ToString() & ", " & reader("FirstName").ToString() &
                                If(IsDBNull(reader("Degree")), " MD", " " & reader("Degree").ToString()) ' Old: IIf(Rs.Fields("Degree").Value Is System.DBNull.Value, ...)
                            End If
                        ElseIf ArType = 1 Then ' TP
                            ArName = reader("PayerName").ToString() ' Old: Rs.Fields("PayerName").Value
                        Else ' Patient
                            ArName = reader("LastName").ToString() & ", " & reader("FirstName").ToString() ' Old: Rs.Fields("LastName").Value & ", " & Rs.Fields("FirstName").Value
                        End If
                    End If
                End Using
            End Using
        End Using ' Old: Rs.Close(), Rs = Nothing, CNP.Close(), CNP = Nothing

        Return ArName ' Old: Return ArName
    End Function


    Private Function GetNextPaymentID() As Long
        Dim query As String = "SELECT MAX(ID) AS LastID FROM Payments" ' Old: Rs.Open("Select max(ID) as LastID from Payments", ...)
        Dim LastID As Object = Nothing

        Using connection As New SqlConnection(connString) ' Old: Dim CNP As New ADODB.Connection
            connection.Open() ' Old: CNP.Open(connString)
            Using command As New SqlCommand(query, connection)
                LastID = command.ExecuteScalar() ' Retrieves a single value from the query (MAX ID)
            End Using
        End Using ' Old: Rs.Close(), Rs = Nothing, CNP.Close(), CNP = Nothing

        If IsDBNull(LastID) OrElse LastID Is Nothing Then ' Old: If Rs.Fields("LastID").Value Is System.DBNull.Value Then
            Return 1 ' Old: GetNextPaymentID = 1
        Else
            Return Convert.ToInt64(LastID) + 1 ' Old: GetNextPaymentID = Rs.Fields("LastID").Value + 1
        End If
    End Function

    Private Sub cmbARType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbARType.SelectedIndexChanged
        If cmbARType.SelectedIndex = 0 Or cmbARType.SelectedIndex = 2 Then     'Client or Patient
            cmbPaymentType.Items.Clear()
            cmbPaymentType.Items.Add("Cash")
            cmbPaymentType.Items.Add("Cheque")
            cmbPaymentType.Items.Add("Credit/Debit Card")
        Else
            cmbPaymentType.Items.Clear()
            cmbPaymentType.Items.Add("Cash")
            cmbPaymentType.Items.Add("Cheque")
            cmbPaymentType.Items.Add("Credit/Debit Card")
            cmbPaymentType.Items.Add("835 Transaction")
        End If
        ArType = cmbARType.SelectedIndex
        cmbPaymentType.SelectedIndex = 1
        ARClear()
    End Sub
    Private Sub ARClear()
        txtARName.Text = ""
        dtpEOBDate.Value = Format(Date.Today, SystemConfig.DateFormat)
        mtxtPostDate.Text = ""
        dgvPayment.Rows.Clear()
    End Sub

    Private Sub txtAmount_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmount.GotFocus
        chkAmt = Val(txtAmount.Text)
    End Sub
    Private Sub txtAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmount.KeyPress
        Prices(txtAmount, e)
    End Sub

    'Private Sub txtCCExpire_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If UserEnteredText(txtCCExpire) <> "" Then
    '        Dim MM As Integer = Val(Microsoft.VisualBasic.Mid(txtCCExpire.Text, 1, 2))
    '        Dim YY As Integer = Val(Microsoft.VisualBasic.Right(txtCCExpire.Text, 2))
    '        If MM > 12 And YY < (Date.Today.Year / 100) Then
    '            MsgBox("Not a valid expiration date")
    '            txtCCExpire.Text = ""
    '            txtCCExpire.Focus()
    '        End If
    '    End If
    'End Sub

    Private Sub txtAmount_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmount.Validated
        If txtAmount.Text <> "" Then
            If Val(txtAmount.Text) < Val(txtCKApplied.Text) Then
                MsgBox("The amount is less than the one already applied against claims. " &
                "Enter an amount higher than " & Format(Val(txtCKApplied.Text), "#0.00") & ".")
                txtAmount.Text = chkAmt
            ElseIf Val(txtAmount.Text) = Val(txtCKApplied.Text) Then
                txtCKUnApplied.Text = "0.00"
            Else    'higher
                If Val(txtCKApplied.Text) = 0 Then
                    txtCKUnApplied.Text = Format(Val(txtAmount.Text), "#0.00")
                    txtCKApplied.Text = "0.00"
                Else
                    txtCKUnApplied.Text = Format(Val(txtAmount.Text) - Val(txtCKApplied.Text), "#0.00")
                End If
            End If
        End If
        UpdateAllFields()
        UpdateProgress()
    End Sub

    Private Function GetPAMTVALS(ByVal ArType As Integer, ByVal ArID As Long, ByVal DOC As String) As String()
        Dim VALS() As String = {"", ""} ' Old: Dim VALS() As String = {"", ""}

        Dim query As String = "SELECT Amount, UnApplied FROM Payments WHERE ArType = @ArType AND Ar_ID = @ArID AND DocNo = @DOC"

        Using connection As New SqlConnection(connString) ' Old: Dim CNP As New ADODB.Connection
            connection.Open() ' Old: CNP.Open(connString)
            Using command As New SqlCommand(query, connection)
                ' Add parameters to prevent SQL injection
                command.Parameters.AddWithValue("@ArType", ArType)
                command.Parameters.AddWithValue("@ArID", ArID)
                command.Parameters.AddWithValue("@DOC", DOC)

                Using reader As SqlDataReader = command.ExecuteReader() ' Old: Rs.Open(...)
                    If reader.HasRows Then ' Old: If Not Rs.BOF Then
                        reader.Read()
                        VALS(0) = reader("Amount").ToString() ' Old: Rs.Fields("Amount").Value.ToString
                        VALS(1) = reader("UnApplied").ToString() ' Old: Rs.Fields("UnApplied").Value.ToString
                    End If
                End Using
            End Using
        End Using ' Old: Rs.Close(), Rs = Nothing, CNP.Close(), CNP = Nothing

        Return VALS ' Old: Return VALS
    End Function

    Private Function GetSavedAppliedAmount(ByVal PaymentID As Long) As Double
        Dim AppAmt As Double = 0 ' Old: Dim AppAmt As Double = 0

        Dim query As String = "SELECT SUM(AppliedAmount) AS AppAmt FROM Payment_Detail WHERE Payment_ID = @PaymentID"

        Using connection As New SqlConnection(connString) ' Old: Dim CNP As New ADODB.Connection
            connection.Open() ' Old: CNP.Open(connString)
            Using command As New SqlCommand(query, connection)
                ' Add parameter to prevent SQL injection
                command.Parameters.AddWithValue("@PaymentID", PaymentID)

                Dim result As Object = command.ExecuteScalar() ' Executes the query and retrieves the result
                If IsDBNull(result) OrElse result Is Nothing Then ' Old: If Rs.Fields("AppAmt").Value IsNot System.DBNull.Value Then
                    AppAmt = 0 ' Old: AppAmt = 0
                Else
                    AppAmt = Convert.ToDouble(result) ' Old: AppAmt = Rs.Fields("AppAmt").Value
                End If
            End Using
        End Using ' Old: Rs.Close(), Rs = Nothing, CNP.Close(), CNP = Nothing

        Return AppAmt ' Old: Return AppAmt
    End Function

    Private Sub UpdateAllFields()
        Dim IApp As Double = 0
        If dgvPayment.RowCount > 0 Then
            Dim i As Integer
            For i = 0 To dgvPayment.RowCount - 1
                If Val(dgvPayment.Rows(i).Cells(4).Value) > 0 Then
                    IApp += Val(dgvPayment.Rows(i).Cells(4).Value)
                End If
            Next
        End If
        '
        txtInvApplied.Text = Format(IApp, "#0.00")
        If Val(txtInvUnApplied.Text) >= IApp Then
            txtInvUnApplied.Text = Format(Val(txtInvUnApplied.Text) - IApp, "#0.00")
        Else
            txtInvUnApplied.Text = ""
        End If
        If IApp > 0 Then btnUnApply.Enabled = True
    End Sub

    Private Sub btnARLookUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnARLookUp.Click

        frmARLookUp.ArType = cmbARType.SelectedIndex
        Dim ARID As String = frmARLookUp.ShowDialog
        If ARID <> "" Then DisplayAR(Val(ARID))
    End Sub

    Private Sub FormClear()
        eobPath.Text = ""
        AttachEOB.Text = "Attach EOB"
        deleteeob.Hide()
        txtArID.Text = "" : txtARName.Text = "" : txtDoc.Text = ""
        txtAmount.Text = "" : txtID.Text = "" : btnAutoApply.Enabled = False
        btnUnApply.Enabled = False : dgvPayment.Rows.Clear()
        txtCKUnApplied.Text = "" : txtCKApplied.Text = ""
        txtInvApplied.Text = "" : txtInvUnApplied.Text = ""
        txtInvID.Text = ""
        txtBilled.Text = ""
        cmbPaymentType.SelectedIndex = 1
        dgvInvoices.Rows.Clear()
        SavedAmount = 0 : SavedUnApp = 0
    End Sub

    Private Sub ProcessInvoiceClear()
        If Val(txtAmount.Text) = Val(txtCKApplied.Text) Then
            txtArID.Text = "" : txtARName.Text = "" : txtDoc.Text = ""
            txtAmount.Text = "" : txtID.Text = "" : btnAutoApply.Enabled = False
            btnUnApply.Enabled = False : dgvPayment.Rows.Clear()
            txtCKUnApplied.Text = "" : txtCKApplied.Text = ""
            txtInvApplied.Text = "" : txtInvUnApplied.Text = ""
            txtInvID.Text = ""
            dgvInvoices.Rows.Clear()
            SavedAmount = 0 : SavedUnApp = 0
        Else
            btnAutoApply.Enabled = False : btnUnApply.Enabled = False
            dgvPayment.Rows.Clear()
            txtInvApplied.Text = "" : txtInvUnApplied.Text = ""
            txtInvID.Text = "" : dgvInvoices.Rows.Clear()
            SavedAmount = Val(txtAmount.Text) : SavedUnApp = Val(txtCKUnApplied.Text)
        End If
    End Sub
    Private Sub DisplayAR(ByVal ArID As Long)
        ' FormClear()
        Dim Paid As Double = 0
        Dim TSTCPT As String = ""
        Dim i As Integer = 1

        Using connection As New SqlConnection(connString) ' Old: Dim CNP As New ADODB.Connection
            connection.Open() ' Old: CNP.Open(connString)

            If cmbARType.SelectedIndex = 0 Then ' Client
                Dim queryProvider As String = "SELECT * FROM Providers WHERE ID = @ArID"
                Using command As New SqlCommand(queryProvider, connection)
                    command.Parameters.AddWithValue("@ArID", ArID) ' Parameterized query
                    Using reader As SqlDataReader = command.ExecuteReader() ' Old: Rs.Open(...)
                        If reader.HasRows Then ' Old: If Not Rs.BOF Then
                            reader.Read()
                            txtArID.Text = reader("ID").ToString() ' Old: txtArID.Text = Rs.Fields("ID").Value
                            Dim Provider As String = ""
                            If Convert.ToInt32(reader("IsIndividual")) = 0 Then
                                Provider = reader("LastName_BSN").ToString() & ", " & GetAddress(Convert.ToInt32(reader("Address_ID")))
                            Else
                                Provider = reader("LastName_BSN").ToString() & ", " & reader("FirstName").ToString() &
                                       If(IsDBNull(reader("Degree")), " MD", " " & reader("Degree").ToString()) &
                                       ", " & GetAddress(Convert.ToInt32(reader("Address_ID")))
                            End If
                            txtARName.Text = Provider
                        End If
                    End Using
                End Using

            ElseIf cmbARType.SelectedIndex = 1 Then ' Third Party
                Dim queryPayers As String = "SELECT * FROM Payers WHERE ID = @ArID"
                Using command As New SqlCommand(queryPayers, connection)
                    command.Parameters.AddWithValue("@ArID", ArID) ' Parameterized query
                    Using reader As SqlDataReader = command.ExecuteReader() ' Old: Rs.Open(...)
                        If reader.HasRows Then ' Old: If Not Rs.BOF Then
                            reader.Read()
                            txtArID.Text = reader("ID").ToString() ' Old: txtArID.Text = Rs.Fields("ID").Value
                            txtARName.Text = reader("PayerName").ToString() & ", " & GetAddress(Convert.ToInt32(reader("Address_ID")))
                        End If
                    End Using
                End Using

            Else ' Patient
                Dim queryPatients As String = "SELECT * FROM Patients WHERE ID = @ArID"
                Using command As New SqlCommand(queryPatients, connection)
                    command.Parameters.AddWithValue("@ArID", ArID) ' Parameterized query
                    Using reader As SqlDataReader = command.ExecuteReader() ' Old: Rs.Open(...)
                        If reader.HasRows Then ' Old: If Not Rs.BOF Then
                            reader.Read()
                            txtArID.Text = reader("ID").ToString() ' Old: txtArID.Text = Rs.Fields("ID").Value
                            Dim Address As String = ""
                            If Not IsDBNull(reader("Address_ID")) AndAlso Convert.ToInt32(reader("Address_ID")) > 0 Then
                                Address = ", " & GetAddress(Convert.ToInt32(reader("Address_ID")))
                            End If
                            txtARName.Text = reader("LastName").ToString() & ", " & reader("FirstName").ToString() & Address
                        End If
                    End Using
                End Using
            End If
        End Using ' Old: Rs.Close(), Rs = Nothing, CNP.Close(), CNP = Nothing

        If chkEditNew.Checked = False Then txtID.Text = GetNextPaymentID().ToString() ' Old: txtID.Text = GetNextPaymentID().ToString
    End Sub
    Private Function GetTstCPT(ByVal TestID As Integer) As String
        Dim TSTCPT As String = "" ' Old: Dim TSTCPT As String = ""

        Using connection As New SqlConnection(connString) ' Old: Dim CNP As New ADODB.Connection
            connection.Open() ' Old: CNP.Open(connString)

            Dim queries As String() = {
            "SELECT Abbr, CPT_Code FROM Tests WHERE ID = @TestID",
            "SELECT Abbr, CPT_Code FROM Groups WHERE ID = @TestID",
            "SELECT Abbr, CPT_Code FROM Profiles WHERE ID = @TestID"
        }

            For Each query In queries
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@TestID", TestID)

                    Using reader As SqlDataReader = command.ExecuteReader() ' Old: Rs.Open(...)
                        If reader.HasRows Then ' Old: If Not Rs.BOF Then
                            reader.Read()
                            TSTCPT = Trim(reader("Abbr").ToString()) ' Old: Rs.Fields("Abbr").Value
                            If Not IsDBNull(reader("CPT_Code")) AndAlso reader("CPT_Code").ToString() <> "" Then
                                TSTCPT += "-" & Trim(reader("CPT_Code").ToString()) ' Old: Rs.Fields("CPT_Code").Value
                            End If
                            Exit For ' Stop loop after finding a valid result
                        End If
                    End Using
                End Using
            Next
        End Using ' Old: Rs.Close(), Rs = Nothing, CNP.Close(), CNP = Nothing

        Return TSTCPT ' Old: Return TSTCPT
    End Function

    Private Function GetPaidAmount(ByVal ChargeID As Long, ByVal TGPID As Long) As Double
        Dim Amount As Double = 0 ' Old: Dim Amount As Double = 0

        Dim query As String = "SELECT AppliedAmount, WrittenOff FROM Payment_Detail WHERE Charge_ID = @ChargeID AND TGP_ID = @TGPID"

        Using connection As New SqlConnection(connString) ' Old: Dim CNP As New ADODB.Connection
            connection.Open() ' Old: CNP.Open(connString)
            Using command As New SqlCommand(query, connection)
                ' Add parameters for security
                command.Parameters.AddWithValue("@ChargeID", ChargeID)
                command.Parameters.AddWithValue("@TGPID", TGPID)

                Using reader As SqlDataReader = command.ExecuteReader() ' Old: Rs.Open(...)
                    While reader.Read() ' Old: Do Until Rs.EOF
                        Amount += If(IsDBNull(reader("AppliedAmount")), 0, Convert.ToDouble(reader("AppliedAmount"))) + ' Old: Rs.Fields("AppliedAmount").Value
                              If(IsDBNull(reader("WrittenOff")), 0, Convert.ToDouble(reader("WrittenOff"))) ' Old: Rs.Fields("WrittenOff").Value
                    End While
                End Using
            End Using
        End Using ' Old: Rs.Close(), Rs = Nothing, CNP.Close(), CNP = Nothing

        Return Amount ' Old: Return Amount
    End Function
    Private Sub dgvPayment_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPayment.CellEndEdit
        If e.RowIndex <> -1 Then
            If e.ColumnIndex = 4 Then   'Paid
                Dim Appl As Double = 0
                If Trim(dgvPayment.Rows(e.RowIndex).Cells(4).Value) <> "" Then
                    If IsNumeric(dgvPayment.Rows(e.RowIndex).Cells(4).Value) _
                    AndAlso Val(dgvPayment.Rows(e.RowIndex).Cells(4).Value) _
                    <= Val(txtInvUnApplied.Text) Then    'meets inv assigned amount
                        If cmbARType.SelectedIndex = 1 Then 'Insurance
                            If Val(dgvPayment.Rows(e.RowIndex).Cells(4).Value) <
                            Val(dgvPayment.Rows(e.RowIndex).Cells(3).Value) Then    'Pmt < billed
                                dgvPayment.Rows(e.RowIndex).Cells(5).Value =
                                Format(Val(dgvPayment.Rows(e.RowIndex).Cells(3).Value) -
                                Val(dgvPayment.Rows(e.RowIndex).Cells(4).Value), "0.00")
                                dgvPayment.Rows(e.RowIndex).Cells(6).Value = "0.00"
                            Else    'pmt >= billed
                                dgvPayment.Rows(e.RowIndex).Cells(6).Value =
                                Format(Val(dgvPayment.Rows(e.RowIndex).Cells(3).Value) -
                                Val(dgvPayment.Rows(e.RowIndex).Cells(4).Value), "0.00")
                                dgvPayment.Rows(e.RowIndex).Cells(5).Value = "0.00"
                            End If
                        Else    'Client or patient
                            dgvPayment.Rows(e.RowIndex).Cells(6).Value =
                            Format(Val(dgvPayment.Rows(e.RowIndex).Cells(3).Value) -
                            Val(dgvPayment.Rows(e.RowIndex).Cells(4).Value), "0.00")
                            dgvPayment.Rows(e.RowIndex).Cells(5).Value = "0.00"
                        End If
                        Appl = Val(dgvPayment.Rows(e.RowIndex).Cells(4).Value)
                        dgvPayment.Rows(e.RowIndex).Cells(5).Value = Val(dgvPayment.Rows(e.RowIndex).Cells(3).Value) - (Val(dgvPayment.Rows(e.RowIndex).Cells(4).Value) + Val(dgvPayment.Rows(e.RowIndex).Cells(8).Value))
                        AdjustInvBalances(CellVal - Appl)
                        dgvPayment.Rows(e.RowIndex).Cells(9).Value = "Process"
                        If Val(txtInvUnApplied.Text) = 0 Then
                            btnSave.Enabled = True
                        Else
                            btnSave.Enabled = False
                        End If
                        btnUnApply.Enabled = True
                    Else
                        MsgBox("Value must be numeric and equal to or less than the Invoice " &
                        "unapplied amount.", MsgBoxStyle.Critical, "Prolis")
                        dgvPayment.Rows(e.RowIndex).Cells(4).Value = ""
                    End If
                End If
            ElseIf e.ColumnIndex = 5 Then   'WO
                If Trim(dgvPayment.Rows(e.RowIndex).Cells(5).Value) <> "" Then  'WO
                    If IsNumeric(dgvPayment.Rows(e.RowIndex).Cells(5).Value) _
                    AndAlso Val(dgvPayment.Rows(e.RowIndex).Cells(5).Value) <=
                    Val(dgvPayment.Rows(e.RowIndex).Cells(3).Value) Then    'Only less or equal to billed
                        dgvPayment.Rows(e.RowIndex).Cells(6).Value = Format(
                        Val(dgvPayment.Rows(e.RowIndex).Cells(3).Value) -
                        Val(dgvPayment.Rows(e.RowIndex).Cells(4).Value) -
                        Val(dgvPayment.Rows(e.RowIndex).Cells(5).Value), "0.00")
                        If Val(dgvPayment.Rows(e.RowIndex).Cells(5).Value) <= 0 Then
                            dgvPayment.Rows(e.RowIndex).Cells(5).Value = Format(dgvPayment.Rows(e.RowIndex).Cells(5).Value, "0.00")

                        Else
                            '  dgvPayment.Rows(e.RowIndex).Cells(5).Value = Val(dgvPayment.Rows(e.RowIndex).Cells(3).Value) - (Val(dgvPayment.Rows(e.RowIndex).Cells(4).Value) + Val(dgvPayment.Rows(e.RowIndex).Cells(8).Value))

                        End If
                        'dgvPayment.Rows(e.RowIndex).Cells(4).Value = Format(dgvPayment.Rows(e.RowIndex).Cells(4).Value, "0.00")
                    Else
                        MsgBox("Value must be numeric and equal to or less than " &
                        "the balance of amount.", MsgBoxStyle.Critical, "Prolis")

                    End If
                End If
            ElseIf e.ColumnIndex = 7 Then   'bill pr
                If CType(dgvPayment.Rows(e.RowIndex).Cells(7).Value, Boolean) = True Then
                    If Val(dgvPayment.Rows(e.RowIndex).Cells(8).Value) <
                    Val(dgvPayment.Rows(e.RowIndex).Cells(5).Value) Then
                        dgvPayment.Rows(e.RowIndex).Cells(8).Value =
                        Format(Val(dgvPayment.Rows(e.RowIndex).Cells(5).Value), "0.00")
                        dgvPayment.Rows(e.RowIndex).Cells(5).Value = "0.00"
                    End If
                Else
                    dgvPayment.Rows(e.RowIndex).Cells(5).Value = Format(
                    Val(dgvPayment.Rows(e.RowIndex).Cells(3).Value) -
                    Val(dgvPayment.Rows(e.RowIndex).Cells(4).Value), "0.00")
                    dgvPayment.Rows(e.RowIndex).Cells(8).Value = ""
                End If
            ElseIf e.ColumnIndex = 8 Then   'PR
                'If cmbARType.SelectedIndex = 1 Then
                If Trim(dgvPayment.Rows(e.RowIndex).Cells(8).Value) <> "" Then
                    If IsNumeric(dgvPayment.Rows(e.RowIndex).Cells(8).Value) _
                    AndAlso Val(dgvPayment.Rows(e.RowIndex).Cells(8).Value) <=
                    (Val(dgvPayment.Rows(e.RowIndex).Cells(3).Value) -
                    Val(dgvPayment.Rows(e.RowIndex).Cells(4).Value)) Then
                        dgvPayment.Rows(e.RowIndex).Cells(5).Value = Format(
                        Val(dgvPayment.Rows(e.RowIndex).Cells(3).Value) -
                        Val(dgvPayment.Rows(e.RowIndex).Cells(4).Value) -
                        Val(dgvPayment.Rows(e.RowIndex).Cells(8).Value), "0.00")
                        dgvPayment.Rows(e.RowIndex).Cells(9).Value = "Process"
                        txtReason.Text = "Patient Responsibility"
                        dgvPayment.Rows(e.RowIndex).Cells(5).Value = Val(dgvPayment.Rows(e.RowIndex).Cells(3).Value) - (Val(dgvPayment.Rows(e.RowIndex).Cells(4).Value) + Val(dgvPayment.Rows(e.RowIndex).Cells(8).Value))
                        dgvPayment.Rows(e.RowIndex).Cells(6).Value = Val(dgvPayment.Rows(e.RowIndex).Cells(8).Value)
                        dgvPayment.Rows(e.RowIndex).Cells(7).Value = Val(dgvPayment.Rows(e.RowIndex).Cells(8).Value) > 0
                    Else
                        MsgBox("Value must be numeric and equal to or less than the Write Off " &
                        "amount and the balance amount.", MsgBoxStyle.Critical, "Prolis")
                        dgvPayment.Rows(e.RowIndex).Cells(8).Value = ""
                    End If
                End If
                'End If
            End If
        End If
        'AdjustInvBalances(CellVal - Val(dgvPayment.Rows(e.RowIndex).Cells(4).Value))
    End Sub

    Private Sub AdjustInvBalances(ByVal CHG As Double)
        Dim IApp As Double = 0
        Dim WO As Double = 0
        Dim Bal As Double = 0
        Dim i As Integer
        'Dim UnApp As double = Val(txtInvUnApplied.Text)
        For i = 0 To dgvPayment.RowCount - 1
            IApp += Val(dgvPayment.Rows(i).Cells(4).Value)
            WO += Val(dgvPayment.Rows(i).Cells(5).Value)
            Bal += Val(dgvPayment.Rows(i).Cells(6).Value)
        Next
        txtInvApplied.Text = Format(IApp, "#0.00")
        txtInvUnApplied.Text = Format(Val(txtInvUnApplied.Text) + CHG, "#0.00")
        txtInvWO.Text = Format(WO, "#0.00")
        txtInvBal.Text = Format(Bal, "#0.00")
    End Sub
    Private Sub dgvPayment_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPayment.CellEnter
        If e.ColumnIndex = 4 Or e.ColumnIndex = 5 Then _
        CellVal = Val(dgvPayment.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
    End Sub

    Private Function IsEOBProcessed(ByVal DocNo As String, ByVal ArType As Byte) As Boolean
        Dim EOBDone As Boolean = False ' Old: Dim EOBDone As Boolean = False

        Dim query As String = "SELECT COUNT(*) FROM Payments WHERE ArType = @ArType AND DocNo = @DocNo"

        Using connection As New SqlConnection(connString) ' Old: Dim CNP As New ADODB.Connection
            connection.Open() ' Old: CNP.Open(connString)
            Using command As New SqlCommand(query, connection)
                ' Add parameters to prevent SQL injection
                command.Parameters.AddWithValue("@ArType", cmbARType.SelectedIndex)
                command.Parameters.AddWithValue("@DocNo", DocNo)

                Dim result As Integer = Convert.ToInt32(command.ExecuteScalar()) ' Executes the query and returns the count
                If result > 0 Then ' Old: If Not Rs.BOF Then
                    EOBDone = True
                End If
            End Using
        End Using ' Old: Rs.Close(), Rs = Nothing, CNP.Close(), CNP = Nothing

        Return EOBDone ' Old: Return EOBDone
    End Function

    Private Sub PopulatePaidInvoices(ByVal PaymentID As String)
        dgvInvoices.Rows.Clear() ' Clear the DataGridView

        Dim AppAmt As Double = 0 ' Initialize the total applied amount
        Dim query As String = "SELECT a.ID AS InvID, a.Svc_Date, a.GrossAmount, a.Accession_ID AS AccID, " &
                          "b.LastName, b.FirstName, " &
                          "(SELECT ROUND(SUM(c.AppliedAmount), 2) FROM Payment_Detail c WHERE c.Charge_ID = a.ID AND c.Payment_ID = @PaymentID) AS PmtAmt " &
                          "FROM Patients b " &
                          "INNER JOIN (Requisitions d INNER JOIN Charges a ON a.Accession_ID = d.ID) ON d.Patient_ID = b.ID " &
                          "WHERE a.ID IN (SELECT DISTINCT Charge_ID FROM Payment_Detail WHERE Payment_ID = @PaymentID)"

        Using connection As New SqlConnection(connString) ' Old: Dim CNP As New ADODB.Connection
            connection.Open() ' Old: CNP.Open(connString)
            Using command As New SqlCommand(query, connection)
                ' Add parameter to prevent SQL injection
                command.Parameters.AddWithValue("@PaymentID", PaymentID)

                Using reader As SqlDataReader = command.ExecuteReader() ' Old: Rs.Open(...)
                    While reader.Read() ' Old: Do Until Rs.EOF
                        Dim svcDate As String = If(IsDBNull(reader("Svc_Date")), "", Format(Convert.ToDateTime(reader("Svc_Date")), SystemConfig.DateFormat)) ' Old: Rs.Fields("Svc_Date").Value
                        Dim grossAmount As String = If(IsDBNull(reader("GrossAmount")), "0.00", Format(Convert.ToDecimal(reader("GrossAmount")), "0.00")) ' Old: Rs.Fields("GrossAmount").Value
                        dgvInvoices.Rows.Add(
                        reader("InvID").ToString(), ' Old: Rs.Fields("InvID").Value
                        reader("AccID").ToString(), ' Old: Rs.Fields("AccID").Value
                        reader("LastName").ToString() & ", " & reader("FirstName").ToString(), ' Old: Rs.Fields("LastName").Value & ", " & Rs.Fields("FirstName").Value
                        svcDate,
                        grossAmount
                    )

                        AppAmt += If(IsDBNull(reader("PmtAmt")), 0, Convert.ToDouble(reader("PmtAmt"))) ' Old: Rs.Fields("PmtAmt").Value
                    End While
                End Using
            End Using
        End Using ' Old: Rs.Close(), Rs = Nothing, CNP.Close(), CNP = Nothing

        ' Update the text boxes with the calculated values
        txtCKApplied.Text = Format(AppAmt, "0.00") ' Old: txtCKApplied.Text = Format(AppAmt, "0.00")
        txtCKUnApplied.Text = Format(Val(txtAmount.Text) - AppAmt, "0.00") ' Old: txtCKUnApplied.Text = Format(Val(txtAmount.Text) - AppAmt, "0.00")
    End Sub
    Private Sub txtDoc_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDoc.Validated
        If AttachEOB.Text = "Viwe EOB" Then
            deleteeob.Show()
        Else
            deleteeob.Hide()
            'View in Browser
        End If
        If chkEditNew.Checked = False Then  'New
            If Trim(txtDoc.Text) <> "" Then
                If IsEOBProcessed(Trim(txtDoc.Text), cmbARType.SelectedIndex) Then
                    MsgBox("Payment record exists for this Document#. Use the Edit operation " &
                    "to view or edit the record.", MsgBoxStyle.Information, "Prolis")
                    txtDoc.Text = ""
                    txtDoc.Focus()
                End If
            End If
            btnVoidCK.Enabled = False
        Else    'Edit
            If Trim(txtDoc.Text) <> "" Then
                Dim cnpmt As New SqlConnection(connString)
                cnpmt.Open()
                Dim cmdpmt As New SqlCommand("Select a.ID, a.Amount,a.EOBPath, a.PaymentType, " &
                "a.Ar_ID, (Select sum(AppliedAmount) from Payment_Detail where Payment_ID = a.ID) " &
                "as AppliedAmt, a.PaymentDate, a.PostDate from Payments a where a.ArType = " &
                cmbARType.SelectedIndex & " and a.DocNo = '" & Trim(txtDoc.Text) & "'", cnpmt)
                cmdpmt.CommandType = CommandType.Text
                Dim drpmt As SqlDataReader = cmdpmt.ExecuteReader
                If drpmt.HasRows Then
                    While drpmt.Read
                        txtID.Text = drpmt("ID").ToString
                        txtAmount.Text = Format(drpmt("Amount"), "#0.00")
                        SavedAmount = drpmt("Amount")
                        cmbPaymentType.SelectedIndex = drpmt("PaymentType")
                        dtpEOBDate.Value = Format(drpmt("PaymentDate"), SystemConfig.DateFormat)

                        If IsDBNull(drpmt("PostDate")) Then
                            mtxtPostDate.Text = ""
                        Else
                            mtxtPostDate.Text = Format(drpmt("PostDate"), SystemConfig.DateFormat)
                        End If

                        'SavedApp = GetSavedAppliedAmount(Rs.Fields("ID").Value)
                        'txtCKUnApplied.Text = Format(Rs.Fields("UnApplied").Value, "#0.00")
                        'SavedUnApp = SavedAmount - SavedApp
                        If drpmt("AppliedAmt") IsNot DBNull.Value Then
                            txtCKApplied.Text = Format(drpmt("AppliedAmt"), "#0.00")
                        Else
                            txtCKApplied.Text = "0.00"
                        End If
                        Dim contents = CommonData.RetrieveColumnValue("EOB_Attachments", "Attachment_Contents", "DocNo", "'" & txtDoc.Text & "'", "")

                        Try
                            If Not contents Is Nothing AndAlso contents IsNot DBNull.Value AndAlso contents.ToString() <> "" Then
                                AttachEOB.Text = "Viwe EOB"
                                deleteeob.Show()
                                eobPath.Text = drpmt("EOBPath").ToString()
                            Else
                                AttachEOB.Text = "Attach EOB"
                                eobPath.Text = ""
                                deleteeob.Hide()
                            End If

                        Catch ex As Exception
                            AttachEOB.Text = "DB Error"
                        End Try
                        txtArID.Text = drpmt("Ar_ID").ToString
                        txtARName.Text = GetPayerName(drpmt("Ar_ID"))
                        '
                        PopulatePaidInvoices(Trim(txtID.Text))
                        btnVoidCK.Enabled = True
                    End While
                Else
                    MsgBox("Payment record does not exists for this Document#.",
                    MsgBoxStyle.Information, "Prolis")
                    txtDoc.Text = ""
                    txtDoc.Focus()
                    SavedAmount = 0 : SavedUnApp = 0 : SavedApp = 0
                    btnVoidCK.Enabled = False : btnSave.Enabled = False
                End If
                cnpmt.Close()
                cnpmt = Nothing
            End If
        End If
        UpdateProgress()
    End Sub

    Private Function GetPayerName(ByVal PayerID As Long) As String
        Dim Payer As String = "" ' Old: Dim Payer As String = ""

        Dim query As String = "SELECT PayerName FROM Payers WHERE ID = @PayerID"

        Using connection As New SqlConnection(connString) ' Old: Dim CNP As New ADODB.Connection
            connection.Open() ' Old: CNP.Open(connString)
            Using command As New SqlCommand(query, connection)
                ' Add parameter to prevent SQL injection
                command.Parameters.AddWithValue("@PayerID", PayerID)

                Using reader As SqlDataReader = command.ExecuteReader() ' Old: Rs.Open(...)
                    If reader.HasRows Then ' Old: If Not Rs.BOF Then
                        reader.Read()
                        Payer = reader("PayerName").ToString() ' Old: Payer = Rs.Fields("PayerName").Value
                    End If
                End Using
            End Using
        End Using ' Old: Rs.Close(), Rs = Nothing, CNP.Close(), CNP = Nothing

        Return Payer ' Old: Return Payer
    End Function

    Private Sub UpdateProgress()
        If cmbPaymentType.SelectedIndex <> 3 Then   'Non 835
            txtDoc.ReadOnly = False : txtAmount.ReadOnly = False
            If txtArID.Text <> "" And txtDoc.Text <> "" And txtID.Text <> "" Then
                If Val(txtCKUnApplied.Text) > 0 Or HasValidEOBDetail() Then
                    btnAutoApply.Enabled = True
                    'txtApplied.Text = "0.00" ': txtUnApplied.Text = txtAmount.Text
                    btnSave.Enabled = True
                Else
                    btnAutoApply.Enabled = False
                    btnSave.Enabled = False
                End If
            Else

                txtCKApplied.Text = "" : txtCKUnApplied.Text = ""
                btnAutoApply.Enabled = False
                btnSave.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnAutoApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAutoApply.Click
        AutoApply()
    End Sub

    Private Sub AutoApply()
        Dim i As Integer
        Dim UnApplied As Double = Val(txtInvUnApplied.Text)
        Dim Applied As Double = 0
        Dim InvApp As Double = 0
        Dim InvWO As Double = 0
        Dim PayerPrice As Double = 0
        Dim Pmt As Double = 0
        'If chkEditNew.Checked = False Then  'new
        '    UnApplied = Val(txtInvUnApplied.Text)
        '    InvApp = Val(txtInvApplied.Text)
        'Else
        '    UnApplied = Val(txtCKUnApplied.Text)
        'End If
        If UnApplied > 0 Then
            For i = 0 To dgvPayment.RowCount - 1
                If UnApplied > 0 Then
                    If dgvPayment.Rows(i).Cells(4).Value Is Nothing _
                    OrElse dgvPayment.Rows(i).Cells(4).Value = "" Then
                        PayerPrice = GetPayerPrice(Val(dgvPayment.Rows(i).Cells(0).Value),
                         Val(dgvPayment.Rows(i).Cells(1).Value))
                        If PayerPrice > 0 Then
                            If PayerPrice <= UnApplied Then
                                If PayerPrice <= Val(dgvPayment.Rows(i).Cells(3).Value) Then    'billed
                                    Pmt = PayerPrice
                                Else
                                    Pmt = Val(dgvPayment.Rows(i).Cells(3).Value)
                                End If
                            Else
                                If UnApplied <= Val(dgvPayment.Rows(i).Cells(3).Value) Then
                                    Pmt = UnApplied
                                Else
                                    Pmt = Val(dgvPayment.Rows(i).Cells(3).Value)
                                End If
                            End If
                        Else    'balance less than line item
                            If Val(dgvPayment.Rows(i).Cells(3).Value) <= UnApplied Then ''has money
                                Pmt = Val(dgvPayment.Rows(i).Cells(3).Value)
                            Else
                                Pmt = UnApplied
                            End If
                        End If
                        UnApplied -= Pmt : Applied += Pmt
                        dgvPayment.Rows(i).Cells(4).Value = Format(Pmt, "#0.00")
                        If cmbARType.SelectedIndex = 1 Then
                            dgvPayment.Rows(i).Cells(5).Value =
                            Format(Val(dgvPayment.Rows(i).Cells(3).Value) - Pmt, "#0.00")
                            dgvPayment.Rows(i).Cells(6).Value = "0.00"
                            InvWO += Val(dgvPayment.Rows(i).Cells(5).Value)
                        Else
                            dgvPayment.Rows(i).Cells(6).Value =
                            Format(Val(dgvPayment.Rows(i).Cells(3).Value) - Pmt, "#0.00")
                            dgvPayment.Rows(i).Cells(5).Value = "0.00"
                            InvWO += Val(dgvPayment.Rows(i).Cells(5).Value)
                        End If
                        btnUnApply.Enabled = True
                        If UnApplied = 0 Then Exit For
                    End If
                End If
            Next
            txtInvApplied.Text = Format(Applied, "#0.00")
            txtInvUnApplied.Text = Format(UnApplied, "#0.00")
            txtInvWO.Text = Format(InvWO, "#0.00")
            txtInvBal.Text = Format(Val(txtBilled.Text) - (Applied + InvWO), "#0.00")
            If Val(txtInvUnApplied.Text) = 0 Then
                btnAutoApply.Enabled = False
                btnUnApply.Enabled = True
            End If
        End If
        If Val(txtInvUnApplied.Text) = 0 Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If
        'UpdateAllFields()
    End Sub
    Private Function GetPayerPrice(ByVal InvID As Long, ByVal TGPID As Integer) As Double
        Dim PP As Double = 0 ' Old: Dim PP As Double = 0

        Dim query As String = "SELECT Amount FROM PaymentHistory " &
                          "WHERE ArType IN (SELECT ArType FROM Charges WHERE ID = @InvID) " &
                          "AND Ar_ID IN (SELECT Ar_ID FROM Charges WHERE ID = @InvID) " &
                          "AND TGP_ID = @TGPID"

        Using connection As New SqlConnection(connString) ' Old: Dim CNP As New ADODB.Connection
            connection.Open() ' Old: CNP.Open(connString)
            Using command As New SqlCommand(query, connection)
                ' Add parameters for secure querying
                command.Parameters.AddWithValue("@InvID", InvID)
                command.Parameters.AddWithValue("@TGPID", TGPID)

                Using reader As SqlDataReader = command.ExecuteReader() ' Old: Rs.Open(...)
                    If reader.HasRows Then ' Old: If Not Rs.BOF Then
                        reader.Read()
                        PP = If(IsDBNull(reader("Amount")), 0, Convert.ToDouble(reader("Amount"))) ' Old: Rs.Fields("Amount").Value
                    End If
                End Using
            End Using
        End Using ' Old: Rs.Close(), Rs = Nothing, CNP.Close(), CNP = Nothing

        Return PP ' Old: Return PP
    End Function

    Private Sub btnUnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnApply.Click
        Dim i As Integer
        Dim IUnApp As Double = 0
        For i = 0 To dgvPayment.RowCount - 1
            IUnApp += Val(dgvPayment.Rows(i).Cells(4).Value)
            dgvPayment.Rows(i).Cells(4).Value = ""
            dgvPayment.Rows(i).Cells(5).Value = ""
            dgvPayment.Rows(i).Cells(6).Value = dgvPayment.Rows(i).Cells(3).Value
        Next
        txtInvWO.Text = ""
        txtInvBal.Text = txtBilled.Text
        txtInvUnApplied.Text = Format(Val(txtInvUnApplied.Text) + IUnApp, "#0.00")
        txtInvApplied.Text = Format(Val(txtInvApplied.Text) - IUnApp, "#0.00")
        If Val(txtInvUnApplied.Text) > 0 Then
            btnAutoApply.Enabled = True
            btnSave.Enabled = False
        End If
        btnUnApply.Enabled = False
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If txtArID.Text <> "" And txtID.Text <> "" And txtDoc.Text <> "" And
        (Val(txtAmount.Text) > 0 Or HasValidEOBDetail()) Then
            Dim ChargeID As Long = -1
            If Corrected.Checked Or VoidClaim.Checked Then
                If OrgClaimNumber.Text = "" Or OrgClaimNumber.Text = "claim number" Then
                    MessageBox.Show("Please write Orignal Claim Number")
                    Return

                End If
            End If
            If dgvPayment.RowCount > 0 Then
                ChargeID = dgvPayment.Rows(0).Cells(0).Value
            End If
            If chkEditNew.Checked = False And cmbARType.SelectedIndex = 1 Then
                ' here i have to check if the accession already exists in some document 
                Dim invcid = 0
                If dgvInvoices.SelectedRows().Count <= 0 Then
                    invcid = txtInvID.Text
                Else
                    invcid = dgvInvoices.SelectedRows(0).Cells(0).Value
                End If

                Dim sql = "select * from Payments where  ArType  = 1 and ID in ( select Payment_ID  from Payment_Detail where Charge_ID in( select ID  from Charges where ID = " & invcid & "))"

                Dim Data = CommonData.ExecuteQuery(sql)
                Dim DocNo As String = ""
                If Data.Count() > 0 Then
                    For Each d In Data
                        Dim ID = d("ID")
                        DocNo = d("DocNo").ToString() + " , "
                        Dim dt = d("LastEditedOn")
                    Next

                    CustomMessageBox.ShowMe("This accession is already posted under document(s) " & DocNo & " . Do you want to proceed.?", "Warning")
                    Dim dd = CustomMessageBox.reply
                    If CustomMessageBox.reply = "NO" Then Return
                End If
            End If
            SavePayment()
            If ChargeID <> -1 Then
                Dim AccID As Long = GetAccIDFromAR(ChargeID)
                If SystemConfig.BARHistory = True Then
                    If Val(txtInvApplied.Text) > 0 Then
                        Log_BAR_Event(AccID, cmbARType.SelectedIndex, Val(txtArID.Text),
                        3, Trim(txtDoc.Text), Val(txtInvApplied.Text))
                    Else    'Adjustment
                        Dim AdjAmt As Double = 0
                        For i As Integer = 0 To dgvPayment.RowCount - 1
                            If dgvPayment.Rows(i).Cells(6).Value = True And
                            (Val(dgvPayment.Rows(i).Cells(5).Value) > 0 Or
                            Val(dgvPayment.Rows(i).Cells(9).Value) > 0) Then    'WO
                                If Val(dgvPayment.Rows(i).Cells(5).Value) > 0 Then
                                    AdjAmt += Val(dgvPayment.Rows(i).Cells(5).Value)
                                ElseIf Val(dgvPayment.Rows(i).Cells(9).Value) > 0 Then
                                    AdjAmt += Val(dgvPayment.Rows(i).Cells(9).Value)
                                End If
                            End If
                        Next
                        Log_BAR_Event(AccID, cmbARType.SelectedIndex, Val(txtArID.Text),
                        4, Trim(txtDoc.Text), Math.Round(AdjAmt * -1, 2))
                    End If
                End If
                If CommentDirty Then UpdatePComments()
                PopulatePaidInvoices(Trim(txtID.Text))
            End If
            ClearForm()
            If chkEditNew.Checked = False Then  'New
                If Val(txtAmount.Text) = Val(txtCKApplied.Text) _
                Then txtID.Text = GetNextPaymentID()
            End If
            'End If
            'Else
            'SavePayment()
            'ClearForm()
            'If chkEditNew.Checked = False Then txtID.Text = GetNextPaymentID()
            'End If
            UpdateProgress()
        Else
            MsgBox("The payment record can not be saved because of missing information")
        End If
    End Sub

    Private Function HasValidEOBDetail() As Boolean
        Dim EOB As Boolean = False
        Dim i As Integer
        For i = 0 To dgvPayment.RowCount - 1
            If Val(dgvPayment.Rows(i).Cells(4).Value) > 0 Or
            Val(dgvPayment.Rows(i).Cells(5).Value) > 0 Or
            (Val(dgvPayment.Rows(i).Cells(8).Value) > 0 And
            dgvPayment.Rows(i).Cells(7).Value = True) Then
                EOB = True
                Exit For
                '9 or 10+11+12
            End If
        Next
        Return EOB
    End Function

    Private Function GetAccIDFromAR(ByVal InvID As Long) As Long
        Dim AccID As Long = 0 ' Old: Dim AccID As Long = 0

        Dim query As String = "SELECT Accession_ID FROM Charges WHERE ID = @InvID"

        Using connection As New SqlConnection(connString) ' Old: Dim CNP As New ADODB.Connection
            connection.Open() ' Old: CNP.Open(connString)
            Using command As New SqlCommand(query, connection)
                ' Add parameter for security
                command.Parameters.AddWithValue("@InvID", InvID)

                Using reader As SqlDataReader = command.ExecuteReader() ' Old: Rs.Open(...)
                    If reader.HasRows Then ' Old: If Not Rs.BOF Then
                        reader.Read()
                        AccID = Convert.ToInt64(reader("Accession_ID")) ' Old: Rs.Fields("Accession_ID").Value
                    End If
                End Using
            End Using
        End Using ' Old: Rs.Close(), Rs = Nothing, CNP.Close(), CNP = Nothing

        Return AccID ' Old: Return AccID
    End Function

    Private Sub ClearForm()
        If Val(txtAmount.Text) = Val(txtCKApplied.Text) Then
            txtArID.Text = "" : txtARName.Text = "" : txtDoc.Text = ""
            txtAmount.Text = "" : txtID.Text = "" : btnAutoApply.Enabled = False
            btnUnApply.Enabled = False : dgvPayment.Rows.Clear()
            txtCKUnApplied.Text = "" : txtCKApplied.Text = ""
            txtInvApplied.Text = "" : txtInvUnApplied.Text = ""
            txtInvID.Text = "" : dgvInvoices.Rows.Clear()
            SavedAmount = 0 : SavedUnApp = 0
        Else
            btnAutoApply.Enabled = False : btnUnApply.Enabled = False
            dgvPayment.Rows.Clear()
            txtInvApplied.Text = "" : txtInvUnApplied.Text = ""
            txtInvID.Text = ""
            'If dgvInvoices.RowCount >= 1 Then
            '    If dgvInvoices.SelectedRows.Count > 0 Then _
            '    dgvInvoices.Rows.RemoveAt(dgvInvoices.SelectedRows(0).Index)
            'End If
            SavedAmount = Val(txtAmount.Text) : SavedUnApp = Val(txtCKUnApplied.Text)
        End If
        dgvComments.Rows.Clear()
        dgvComments.Rows.Add()
    End Sub

    Private Sub SavePayment()
        Dim ArType As Integer = cmbARType.SelectedIndex
        'Dim PaymentType As Integer = cmbPaymentType.SelectedIndex
        Dim Applied As Double = 0
        Dim ChargeID As String = ""

        Using conn As New SqlConnection(connString)
            conn.Open()
            Using trans As SqlTransaction = conn.BeginTransaction()
                Try
                    Dim paymentID As Integer = 0

                    ' OLD:
                    ' Rs.Open("Select * from Payments where ArType = " ...
                    Dim checkCmd As New SqlCommand("SELECT ID FROM Payments WHERE ArType = @ArType AND Ar_ID = @Ar_ID AND DocNo = @DocNo", conn, trans)
                    checkCmd.Parameters.AddWithValue("@ArType", ArType)
                    checkCmd.Parameters.AddWithValue("@Ar_ID", Val(txtArID.Text))
                    checkCmd.Parameters.AddWithValue("@DocNo", Trim(txtDoc.Text))

                    Dim result = checkCmd.ExecuteScalar()

                    Dim query As String

                    If result Is Nothing Then
                        paymentID = GetNextPaymentID()
                        ' txtID.Text = paymentID.ToString()
                        query = "INSERT INTO Payments (ID, ArType, Ar_ID, PaymentType, PaymentDate, PostDate, DocNo, Amount, Unapplied, LastEditedOn, EditedBy) VALUES (@ID, @ArType, @Ar_ID, @PaymentType, @PaymentDate, @PostDate, @DocNo, @Amount, @Unapplied, @LastEditedOn, @EditedBy)"

                    Else
                        paymentID = CInt(result)
                        query = "UPDATE Payments SET ArType = @ArType, Ar_ID = @Ar_ID, PaymentType = @PaymentType, PaymentDate = @PaymentDate, PostDate = @PostDate, DocNo = @DocNo, Amount = @Amount, Unapplied = @Unapplied, LastEditedOn = @LastEditedOn, EditedBy = @EditedBy WHERE ID = @ID"

                    End If

                    txtID.Text = paymentID.ToString()
                    '======================================

                    Using cmd As New SqlCommand(query, conn)
                        ' Add common parameters
                        cmd.Parameters.AddWithValue("@ID", Val(txtID.Text))
                        cmd.Parameters.AddWithValue("@ArType", cmbARType.SelectedIndex)
                        cmd.Parameters.AddWithValue("@Ar_ID", Val(txtArID.Text))
                        cmd.Parameters.AddWithValue("@PaymentType", cmbPaymentType.SelectedIndex)
                        cmd.Parameters.AddWithValue("@PaymentDate", dtpEOBDate.Value)
                        cmd.Parameters.AddWithValue("@PostDate", If(mtxtPostDate.Text = "  /  /", DBNull.Value, CDate(mtxtPostDate.Text)))
                        cmd.Parameters.AddWithValue("@DocNo", Trim(txtDoc.Text))
                        cmd.Parameters.AddWithValue("@Amount", Val(txtAmount.Text))

                        ' Calculate Unapplied value
                        Dim unapplied As Double
                        If Val(txtAmount.Text) = Val(txtInvApplied.Text) Then
                            unapplied = 0
                        Else
                            If Val(txtAmount.Text) >= (Val(txtInvApplied.Text) + Val(txtCKApplied.Text)) Then
                                txtCKApplied.Text = Format((Val(txtInvApplied.Text) + Val(txtCKApplied.Text)), "#0.00")
                                txtCKUnApplied.Text = Format(Val(txtAmount.Text) - Val(txtCKApplied.Text), "#0.00")
                                If Val(txtCKUnApplied.Text) <= 0.02 Then
                                    unapplied = 0
                                    txtCKUnApplied.Text = "0.00"
                                Else
                                    unapplied = Val(txtCKUnApplied.Text)
                                End If
                            End If
                        End If

                        cmd.Parameters.AddWithValue("@Unapplied", unapplied)
                        txtCKUnApplied.Text = Format(unapplied, "#0.00")

                        cmd.Parameters.AddWithValue("@LastEditedOn", Date.Now)
                        cmd.Parameters.AddWithValue("@EditedBy", ThisUser.ID)

                        ' Execute the command
                        cmd.ExecuteNonQuery()
                    End Using
                    'End Using
                    '======================================

                    txtBilled.Text = ""

                    ' === Payment_Detail handling ===
                    Dim deleteDetailCmd As New SqlCommand("DELETE FROM Payment_Detail WHERE Payment_ID = @PID", conn, trans)
                    deleteDetailCmd.Parameters.AddWithValue("@PID", paymentID)
                    deleteDetailCmd.ExecuteNonQuery()

                    Dim RebillInfo As New List(Of String)
                    Dim PaidTGPs As New List(Of String)
                    If dgvPayment.RowCount > 0 Then
                        ChargeID = dgvPayment.Rows(0).Cells(0).Value
                        Dim SvcDate As Date = GetServiceDate(Val(dgvPayment.Rows(0).Cells(0).Value))

                        For i = 0 To dgvPayment.RowCount - 1

                            ' Dim pApplied = Val(dgvPayment.Rows(i).Cells(4).Value)
                            Dim pApplied As Double = 0 ' Default value to prevent errors

                            If dgvPayment.Rows(i).Cells(4).Value IsNot Nothing AndAlso dgvPayment.Rows(i).Cells(4).Value.ToString().Trim() <> "" Then
                                pApplied = Val(dgvPayment.Rows(i).Cells(4).Value)
                            End If

                            Dim pWriteOff = Val(dgvPayment.Rows(i).Cells(5).Value)

                            Dim pRebill As Double = 0 ' Val(dgvPayment.Rows(i).Cells(8).Value)

                            If dgvPayment.Rows(i).Cells(8).Value IsNot Nothing AndAlso dgvPayment.Rows(i).Cells(8).Value.ToString().Trim() <> "" Then
                                pRebill = Val(dgvPayment.Rows(i).Cells(8).Value)
                            End If

                            If pApplied > 0 Or pWriteOff > 0 Or pRebill > 0 Then

                                Dim tgpID = Val(dgvPayment.Rows(i).Cells(1).Value)
                                PaidTGPs.Add(tgpID.ToString())

                                Dim writtenOff As Double = 0
                                Dim rebillAmt As Double = 0
                                If pRebill > 0 AndAlso dgvPayment.Rows(i).Cells(7).Value = True AndAlso newInv.Checked Then
                                    RebillInfo.Add($"{tgpID}|{dgvPayment.Rows(i).Cells(2).Value}|{pRebill}")
                                    ' writtenOff = Val(dgvPayment.Rows(i).Cells(3).Value) - (pRebill + pApplied)
                                Else
                                    'writtenOff = Val(dgvPayment.Rows(i).Cells(3).Value) - (pRebill + pApplied)
                                End If

                                'writtenOff = Val(dgvPayment.Rows(i).Cells(3).Value) - (pRebill + pApplied)
                                writtenOff = Math.Round(Val(dgvPayment.Rows(i).Cells(5).Value), 2)

                                Dim insertDetailCmd As New SqlCommand("
                            INSERT INTO Payment_Detail (
                                Payment_ID, Charge_ID, TGP_ID, AppliedAmount, 
                                ChargeAmount, UnappliedAmount, Balance, WORB,
                                WrittenOff, RebillAmount
                            ) VALUES (
                                @Payment_ID, @Charge_ID, @TGP_ID, @AppliedAmount,
                                @ChargeAmount, @UnappliedAmount, @Balance, @WORB,
                                @WrittenOff, @RebillAmount)", conn, trans)

                                insertDetailCmd.Parameters.AddWithValue("@Payment_ID", paymentID)
                                insertDetailCmd.Parameters.AddWithValue("@Charge_ID", ChargeID)
                                insertDetailCmd.Parameters.AddWithValue("@TGP_ID", tgpID)
                                insertDetailCmd.Parameters.AddWithValue("@AppliedAmount", pApplied)
                                insertDetailCmd.Parameters.AddWithValue("@ChargeAmount", Val(dgvPayment.Rows(i).Cells(3).Value))
                                insertDetailCmd.Parameters.AddWithValue("@UnappliedAmount", 0)
                                insertDetailCmd.Parameters.AddWithValue("@Balance", dgvPayment.Rows(i).Cells(6).Value)
                                insertDetailCmd.Parameters.AddWithValue("@WORB", dgvPayment.Rows(i).Cells(7).Value)
                                insertDetailCmd.Parameters.AddWithValue("@WrittenOff", Math.Round(writtenOff, 2))
                                insertDetailCmd.Parameters.AddWithValue("@RebillAmount", rebillAmt)

                                insertDetailCmd.ExecuteNonQuery()
                                Applied += pApplied

                                If pApplied > 0 Then
                                    UpdatePaymentHistory(ArType, Val(txtArID.Text), tgpID, pApplied)
                                End If
                            End If

                            ' Handle deletions/reversals
                            Dim action As String = CStr(dgvPayment.Rows(i).Cells(9).Value)
                            If action = "Delete" Or action = "Write Off" Then
                                Dim delTGP = Val(dgvPayment.Rows(i).Cells(1).Value)
                                Dim delCharge = Val(dgvPayment.Rows(i).Cells(0).Value)
                                ExecuteNonQuery(conn, trans, $"DELETE FROM Req_Billable WHERE TGP_ID = {delTGP} AND Accession_ID IN (SELECT Accession_ID FROM Charges WHERE ID = {delCharge})")
                                ExecuteNonQuery(conn, trans, $"UPDATE Req_TGP SET Billed = 0 WHERE TGP_ID = {delTGP} AND Accession_ID IN (SELECT Accession_ID FROM Charges WHERE ID = {delCharge})")
                                ExecuteNonQuery(conn, trans, $"DELETE FROM Charge_Detail WHERE TGP_ID = {delTGP} AND Charge_ID = {delCharge}")
                            ElseIf action = "Reverse" Then
                                Dim revTGP = Val(dgvPayment.Rows(i).Cells(1).Value)
                                Dim revCharge = Val(dgvPayment.Rows(i).Cells(0).Value)
                                ExecuteNonQuery(conn, trans, $"UPDATE Req_Billable SET Bill_Status = 'H' WHERE TGP_ID = {revTGP} AND Accession_ID IN (SELECT Accession_ID FROM Charges WHERE ID = {revCharge})")
                                ExecuteNonQuery(conn, trans, $"UPDATE Req_TGP SET Billed = 0 WHERE TGP_ID = {revTGP} AND Accession_ID IN (SELECT Accession_ID FROM Charges WHERE ID = {revCharge})")
                                ExecuteNonQuery(conn, trans, $"DELETE FROM Charge_Detail WHERE TGP_ID = {revTGP} AND Charge_ID = {revCharge}")
                            End If
                        Next

                        If PaidTGPs.Any() Then

                            Dim paidTGPsString As String = String.Join(", ", PaidTGPs) ' Convert List to comma-separated string
                            ExecuteNonQuery(conn, trans, $"DELETE FROM Payment_Detail WHERE Payment_ID = {Val(txtID.Text)} AND Charge_ID = {ChargeID} AND NOT TGP_ID IN ({paidTGPsString})")

                            UpdatePayments(paymentID)
                            SynchronizeChargeToDetail(Val(ChargeID))
                            If cmbARType.SelectedIndex = 1 Then
                                UpdateEOBClaim(Trim(txtDoc.Text), GetARName(Val(txtArID.Text), cmbARType.SelectedIndex), Val(ChargeID), ChargeID)
                                Dim EOB() As String = {
                                Trim(txtDoc.Text),
                                GetARName(Val(txtArID.Text), cmbARType.SelectedIndex),
                                Trim(txtArID.Text),
                                Format(dtpEOBDate.Value, SystemConfig.DateFormat),
                                Format(Applied, "#0.00")
                            }
                                SaveEOB(EOB)
                            End If
                        End If
                    End If

                    ' Handle rebill
                    If RebillInfo.Count > 0 Then
                        Dim AccPatInfo() As String = GetAccPatIDFromInvoice(dgvPayment.Rows(0).Cells(0).Value)
                        RebillPatient(AccPatInfo, RebillInfo.ToArray())
                    End If

                    trans.Commit()
                Catch ex As Exception
                    trans.Rollback()
                    MessageBox.Show("Error saving payment: " & ex.Message)
                End Try
            End Using
        End Using

        dgvPayment.Rows.Clear()
        txtReason.Text = ""
    End Sub

    Private Sub ExecuteNonQuery(conn As SqlConnection, trans As SqlTransaction, sql As String)
        Using cmd As New SqlCommand(sql, conn, trans)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub SavePayment0()
        'Dim ArType As Integer = cmbARType.SelectedIndex
        'Dim PaymentType As Integer = cmbPaymentType.SelectedIndex
        'Dim i As Integer
        'Dim BillInfo() As String = {"", "", ""}
        'Dim Applied As Double = 0
        'Dim ChargeID As String = ""
        ''Dim CNP As New ADODB.Connection
        ''CNP.Open(connString)
        ''Dim Rs As New ADODB.Recordset
        ''Rs.Open("Select * from Payments where ArType = " & cmbARType.SelectedIndex &
        ''" and Ar_ID = " & Val(txtArID.Text) & " and DocNo = '" & Trim(txtDoc.Text) &
        ''"'", CNP, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        ''If Rs.BOF Then
        ''    Rs.AddNew()
        ''    txtID.Text = CStr(GetNextPaymentID())
        ''Else
        ''    txtID.Text = CStr(Rs.Fields("ID").Value)
        ''End If
        ''Rs.Fields("ID").Value = Val(txtID.Text)
        ''Rs.Fields("ArType").Value = ArType
        ''Rs.Fields("Ar_ID").Value = Val(txtArID.Text)
        ''Rs.Fields("PaymentType").Value = PaymentType
        ''Rs.Fields("PaymentDate").Value = dtpEOBDate.Value
        ''If mtxtPostDate.Text = "  /  /" Then
        ''    Rs.Fields("PostDate").Value = DBNull.Value
        ''Else
        ''    Rs.Fields("PostDate").Value = mtxtPostDate.Text 'ToString("yyyy-MM-dd")

        ''End If
        ''Rs.Fields("DocNo").Value = Trim(txtDoc.Text)
        ''Rs.Fields("Amount").Value = Val(txtAmount.Text)
        ''If Val(txtAmount.Text) = Val(txtInvApplied.Text) Then   'Edit scenario
        ''    Rs.Fields("Unapplied").Value = 0
        ''    txtCKUnApplied.Text = "0.00"
        ''Else
        ''    If Val(txtAmount.Text) >= (Val(txtInvApplied.Text) + Val(txtCKApplied.Text)) Then
        ''        txtCKApplied.Text = Format((Val(txtInvApplied.Text) + Val(txtCKApplied.Text)), "#0.00")
        ''        txtCKUnApplied.Text = Format(Val(txtAmount.Text) -
        ''        Val(txtCKApplied.Text), "#0.00")
        ''        If Val(txtCKUnApplied.Text) <= 0.02 Then
        ''            Rs.Fields("Unapplied").Value = 0
        ''            txtCKUnApplied.Text = "0.00"
        ''        Else
        ''            Rs.Fields("Unapplied").Value = Val(txtCKUnApplied.Text)
        ''        End If
        ''    End If
        ''End If
        ''txtBilled.Text = ""
        ''Rs.Fields("LastEditedOn").Value = Date.Now
        ''Rs.Fields("EditedBy").Value = ThisUser.ID
        ''Rs.Update()
        ''Rs.Close()
        ''
        'Using connection As New SqlConnection(connString) ' Old: Dim CNP As New ADODB.Connection
        '    connection.Open() ' Old: CNP.Open(connString)

        '    Dim queryPayments As String = "SELECT * FROM Payments WHERE ArType = @ArType AND Ar_ID = @ArID AND DocNo = @DocNo"
        '    Using command As New SqlCommand(queryPayments, connection)
        '        ' Add parameters for secure querying
        '        command.Parameters.AddWithValue("@ArType", cmbARType.SelectedIndex)
        '        command.Parameters.AddWithValue("@ArID", Val(txtArID.Text))
        '        command.Parameters.AddWithValue("@DocNo", Trim(txtDoc.Text))

        '        Using reader As SqlDataReader = command.ExecuteReader() ' Old: Rs.Open(...)
        '            If Not reader.HasRows Then ' Old: If Rs.BOF Then
        '                txtID.Text = CStr(GetNextPaymentID())
        '            Else
        '                reader.Read()
        '                txtID.Text = reader("ID").ToString() ' Old: Rs.Fields("ID").Value
        '            End If
        '        End Using
        '    End Using

        '    ' Query for inserting or updating the Payments table
        '    Dim upsertQuery As String =
        '"IF NOT EXISTS (SELECT * FROM Payments WHERE ID = @ID) " &
        '"BEGIN " &
        '"INSERT INTO Payments (ID, ArType, Ar_ID, PaymentType, PaymentDate, PostDate, DocNo, Amount, Unapplied, LastEditedOn, EditedBy) " &
        '"VALUES (@ID, @ArType, @ArID, @PaymentType, @PaymentDate, @PostDate, @DocNo, @Amount, @Unapplied, @LastEditedOn, @EditedBy) " &
        '"END ELSE " &
        '"BEGIN " &
        '"UPDATE Payments SET ArType = @ArType, Ar_ID = @ArID, PaymentType = @PaymentType, " &
        '"PaymentDate = @PaymentDate, PostDate = @PostDate, DocNo = @DocNo, Amount = @Amount, " &
        '"Unapplied = @Unapplied, LastEditedOn = @LastEditedOn, EditedBy = @EditedBy WHERE ID = @ID " &
        '"END"

        '    Using commandUpsert As New SqlCommand(upsertQuery, connection)
        '        ' Add parameters for insertion/update
        '        commandUpsert.Parameters.AddWithValue("@ID", Val(txtID.Text))
        '        commandUpsert.Parameters.AddWithValue("@ArType", cmbARType.SelectedIndex)
        '        commandUpsert.Parameters.AddWithValue("@ArID", Val(txtArID.Text))
        '        commandUpsert.Parameters.AddWithValue("@PaymentType", PaymentType)
        '        commandUpsert.Parameters.AddWithValue("@PaymentDate", dtpEOBDate.Value)
        '        commandUpsert.Parameters.AddWithValue("@PostDate", If(mtxtPostDate.Text = "  /  /", DBNull.Value, mtxtPostDate.Text))
        '        commandUpsert.Parameters.AddWithValue("@DocNo", Trim(txtDoc.Text))
        '        commandUpsert.Parameters.AddWithValue("@Amount", Val(txtAmount.Text))

        '        ' Handle Unapplied logic
        '        If Val(txtAmount.Text) = Val(txtInvApplied.Text) Then ' Old: Edit scenario
        '            commandUpsert.Parameters.AddWithValue("@Unapplied", 0)
        '            txtCKUnApplied.Text = "0.00"
        '        ElseIf Val(txtAmount.Text) >= (Val(txtInvApplied.Text) + Val(txtCKApplied.Text)) Then
        '            txtCKApplied.Text = Format((Val(txtInvApplied.Text) + Val(txtCKApplied.Text)), "#0.00")
        '            txtCKUnApplied.Text = Format(Val(txtAmount.Text) - Val(txtCKApplied.Text), "#0.00")

        '            If Val(txtCKUnApplied.Text) <= 0.02 Then
        '                commandUpsert.Parameters.AddWithValue("@Unapplied", 0)
        '                txtCKUnApplied.Text = "0.00"
        '            Else
        '                commandUpsert.Parameters.AddWithValue("@Unapplied", Val(txtCKUnApplied.Text))
        '            End If
        '        End If

        '        txtBilled.Text = "" ' Old: txtBilled.Text = ""

        '        commandUpsert.Parameters.AddWithValue("@LastEditedOn", Date.Now)
        '        commandUpsert.Parameters.AddWithValue("@EditedBy", ThisUser.ID)

        '        commandUpsert.ExecuteNonQuery() ' Execute the insert/update operation
        '    End Using
        'End Using ' Old: Rs.Close(), Rs = Nothing, CNP.Close(), CNP = Nothing
        'Dim RebillInfo() As String = {""}
        ''
        'Dim PaidTGPs As String = ""
        'If dgvPayment.RowCount > 0 Then
        '    ChargeID = dgvPayment.Rows(0).Cells(0).Value
        '    Dim SvcDate As Date = GetServiceDate(Val(dgvPayment.Rows(0).Cells(0).Value))
        '    For i = 0 To dgvPayment.RowCount - 1
        '        If Val(dgvPayment.Rows(i).Cells(4).Value) > 0 _
        '        Or Val(dgvPayment.Rows(i).Cells(5).Value) > 0 _
        '        Or Val(dgvPayment.Rows(i).Cells(8).Value) > 0 Then  'pmt or wo or pr
        '            PaidTGPs += dgvPayment.Rows(i).Cells(1).Value.ToString & ", "
        '            'Rs.Open("Select * from Payment_Detail where Payment_ID = " & Val(txtID.Text) _
        '            '& " and Charge_ID = " & Val(dgvPayment.Rows(i).Cells(0).Value) &
        '            '" and TGP_ID = " & Val(dgvPayment.Rows(i).Cells(1).Value), CNP,
        '            'ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        '            'If Rs.BOF Then Rs.AddNew()
        '            'Rs.Fields("Payment_ID").Value = Val(txtID.Text)
        '            'Rs.Fields("Charge_ID").Value = Val(dgvPayment.Rows(i).Cells(0).Value)
        '            'Rs.Fields("TGP_ID").Value = Val(dgvPayment.Rows(i).Cells(1).Value)
        '            'If dgvPayment.Rows(i).Cells(4).Value IsNot Nothing Then
        '            '    Rs.Fields("AppliedAmount").Value = Val(dgvPayment.Rows(i).Cells(4).Value)
        '            'Else
        '            '    Rs.Fields("AppliedAmount").Value = 0
        '            'End If
        '            'Applied += Rs.Fields("AppliedAmount").Value
        '            'Rs.Fields("ChargeAmount").Value = Val(dgvPayment.Rows(i).Cells(3).Value)
        '            ''Rs.Fields("WrittenOff").Value = Math.Round( _
        '            ''Val(dgvPayment.Rows(i).Cells(5).Value), 2)
        '            'Rs.Fields("UnappliedAmount").Value = 0
        '            'Rs.Fields("Balance").Value = dgvPayment.Rows(i).Cells(6).Value
        '            'Rs.Fields("WORB").Value = dgvPayment.Rows(i).Cells(7).Value
        '            ''''  AndAlso Val(dgvPayment.Rows(i).Cells(8).Value) > 0 _
        '            'If dgvPayment.Rows(i).Cells(8).Value IsNot Nothing _
        '            'AndAlso Val(dgvPayment.Rows(i).Cells(8).Value) > 0 _
        '            'AndAlso dgvPayment.Rows(i).Cells(7).Value = True AndAlso newInv.Checked Then    'Rebill
        '            '    If RebillInfo(UBound(RebillInfo)) <> "" Then _
        '            '    ReDim Preserve RebillInfo(UBound(RebillInfo) + 1)
        '            '    RebillInfo(UBound(RebillInfo)) =
        '            '    dgvPayment.Rows(i).Cells(1).Value & "|" &
        '            '    dgvPayment.Rows(i).Cells(2).Value & "|" &
        '            '    dgvPayment.Rows(i).Cells(8).Value
        '            '    '


        '            '    Rs.Fields("WrittenOff").Value = Math.Round(
        '            '    Val(dgvPayment.Rows(i).Cells(5).Value) +
        '            '    Val(dgvPayment.Rows(i).Cells(8).Value), 2)

        '            '    Rs.Fields("WrittenOff").Value = Val(dgvPayment.Rows(i).Cells(3).Value) - (Val(dgvPayment.Rows(i).Cells(8).Value) + Val(dgvPayment.Rows(i).Cells(4).Value))

        '            '    Rs.Fields("WrittenOff").Value = Math.Round(
        '            '    Val(dgvPayment.Rows(i).Cells(5).Value), 2)

        '            '    Rs.Fields("RebillAmount").Value = 0
        '            'Else    'no WO
        '            '    Rs.Fields("RebillAmount").Value = 0
        '            '    Rs.Fields("WrittenOff").Value = Math.Round(
        '            '    Val(dgvPayment.Rows(i).Cells(5).Value), 2)
        '            '    Rs.Fields("WrittenOff").Value = Val(dgvPayment.Rows(i).Cells(3).Value) - (Val(dgvPayment.Rows(i).Cells(8).Value) + Val(dgvPayment.Rows(i).Cells(4).Value))


        '            '    Rs.Fields("WrittenOff").Value = Math.Round(
        '            '    Val(dgvPayment.Rows(i).Cells(5).Value), 2)

        '            'End If
        '            'Rs.Update()
        '            'Rs.Close()
        '            '
        '            Using connection As New SqlConnection(connString) ' Old: Dim CNP As New ADODB.Connection
        '                connection.Open() ' Old: CNP.Open(connString)

        '                'Dim queryPaymentDetail As String = "SELECT * FROM Payment_Detail WHERE Payment_ID = @PaymentID AND Charge_ID = @ChargeID AND TGP_ID = @TGPID"

        '                ' Check if record exists
        '                Dim queryPaymentDetail As String = "DELETE FROM Payment_Detail WHERE Payment_ID = @PaymentID AND Charge_ID = @ChargeID AND TGP_ID = @TGPID"
        '                Using command As New SqlCommand(queryPaymentDetail, connection)
        '                    ' Add parameters for secure querying
        '                    command.Parameters.AddWithValue("@PaymentID", Val(txtID.Text))
        '                    command.Parameters.AddWithValue("@ChargeID", Val(dgvPayment.Rows(i).Cells(0).Value))
        '                    command.Parameters.AddWithValue("@TGPID", Val(dgvPayment.Rows(i).Cells(1).Value))

        '                    Using reader As SqlDataReader = command.ExecuteReader() ' Old: Rs.Open(...)
        '                        If Not reader.HasRows Then ' Old: If Rs.BOF Then
        '                            Dim insertQuery As String = "INSERT INTO Payment_Detail (Payment_ID, Charge_ID, TGP_ID, AppliedAmount, ChargeAmount, Balance, WORB, WrittenOff, RebillAmount) " &
        '                                    "VALUES (@PaymentID, @ChargeID, @TGPID, @AppliedAmount, @ChargeAmount, @Balance, @WORB, @WrittenOff, @RebillAmount)"
        '                            Using insertCommand As New SqlCommand(insertQuery, connection)
        '                                ' Add parameters for insertion
        '                                insertCommand.Parameters.AddWithValue("@PaymentID", Val(txtID.Text))
        '                                insertCommand.Parameters.AddWithValue("@ChargeID", Val(dgvPayment.Rows(i).Cells(0).Value))
        '                                insertCommand.Parameters.AddWithValue("@TGPID", Val(dgvPayment.Rows(i).Cells(1).Value))
        '                                insertCommand.Parameters.AddWithValue("@AppliedAmount", If(dgvPayment.Rows(i).Cells(4).Value IsNot Nothing, Val(dgvPayment.Rows(i).Cells(4).Value), 0))
        '                                insertCommand.Parameters.AddWithValue("@ChargeAmount", Val(dgvPayment.Rows(i).Cells(3).Value))
        '                                insertCommand.Parameters.AddWithValue("@Balance", Val(dgvPayment.Rows(i).Cells(6).Value))
        '                                insertCommand.Parameters.AddWithValue("@WORB", dgvPayment.Rows(i).Cells(7).Value)

        '                                ' Handle WrittenOff and Rebill logic
        '                                If dgvPayment.Rows(i).Cells(8).Value IsNot Nothing AndAlso
        '               Val(dgvPayment.Rows(i).Cells(8).Value) > 0 AndAlso
        '               dgvPayment.Rows(i).Cells(7).Value = True AndAlso newInv.Checked Then ' Rebill
        '                                    If RebillInfo(UBound(RebillInfo)) <> "" Then ReDim Preserve RebillInfo(UBound(RebillInfo) + 1)
        '                                    RebillInfo(UBound(RebillInfo)) =
        '                    dgvPayment.Rows(i).Cells(1).Value & "|" &
        '                    dgvPayment.Rows(i).Cells(2).Value & "|" &
        '                    dgvPayment.Rows(i).Cells(8).Value

        '                                    insertCommand.Parameters.AddWithValue("@WrittenOff", Math.Round(Val(dgvPayment.Rows(i).Cells(5).Value) + Val(dgvPayment.Rows(i).Cells(8).Value), 2))
        '                                    insertCommand.Parameters.AddWithValue("@RebillAmount", 0)
        '                                Else ' No Write-Off (WO)
        '                                    insertCommand.Parameters.AddWithValue("@WrittenOff", Math.Round(Val(dgvPayment.Rows(i).Cells(5).Value), 2))
        '                                    insertCommand.Parameters.AddWithValue("@RebillAmount", 0)
        '                                End If

        '                                insertCommand.ExecuteNonQuery() ' Perform insertion
        '                            End Using
        '                        End If
        '                    End Using
        '                End Using
        '            End Using ' Old: Rs.Close(), Rs = Nothing, CNP.Close(), CNP = Nothing

        '            ' Increment applied amount
        '            Applied += If(dgvPayment.Rows(i).Cells(4).Value IsNot Nothing, Val(dgvPayment.Rows(i).Cells(4).Value), 0)
        '            If Val(dgvPayment.Rows(i).Cells(4).Value) > 0 Then _
        '            UpdatePaymentHistory(ArType, Val(txtArID.Text),
        '            dgvPayment.Rows(i).Cells(1).Value,
        '            Val(dgvPayment.Rows(i).Cells(4).Value))
        '        ElseIf dgvPayment.Rows(i).Cells(9).Value = "Delete" _
        '        Or dgvPayment.Rows(i).Cells(9).Value = "Write Off" Then
        '            CNP.Execute("Delete from Req_Billable where TGP_ID = " &
        '            dgvPayment.Rows(i).Cells(1).Value & " and " &
        '            "Accession_ID in (Select Accession_ID from Charges where " &
        '            "ID = " & dgvPayment.Rows(i).Cells(0).Value & ")")
        '            '
        '            CNP.Execute("Update Req_TGP set Billed = 0 where TGP_ID = " &
        '            dgvPayment.Rows(i).Cells(1).Value & " and " &
        '            "Accession_ID in (Select Accession_ID from Charges where " &
        '            "ID = " & dgvPayment.Rows(i).Cells(0).Value & ")")
        '            '
        '            CNP.Execute("Delete from Charge_Detail where TGP_ID = " &
        '            dgvPayment.Rows(i).Cells(1).Value & " and " &
        '            "Charge_ID = " & dgvPayment.Rows(i).Cells(0).Value)
        '            '
        '        ElseIf dgvPayment.Rows(i).Cells(9).Value = "Reverse" Then
        '            CNP.Execute("Update Req_Billable Set Bill_Status = 'H' where " &
        '            "TGP_ID = " & dgvPayment.Rows(i).Cells(1).Value &
        '            " and Accession_ID in (Select Accession_ID from Charges where " &
        '            "ID = " & dgvPayment.Rows(i).Cells(0).Value & ")")
        '            '
        '            CNP.Execute("Update Req_TGP set Billed = 0 where TGP_ID = " &
        '            dgvPayment.Rows(i).Cells(1).Value & " and " &
        '            "Accession_ID in (Select Accession_ID from Charges where " &
        '            "ID = " & dgvPayment.Rows(i).Cells(0).Value & ")")
        '            '
        '            CNP.Execute("Delete from Charge_Detail where TGP_ID = " &
        '            dgvPayment.Rows(i).Cells(1).Value & " and " &
        '            "Charge_ID = " & dgvPayment.Rows(i).Cells(0).Value)
        '            '
        '        End If
        '    Next
        '    '
        '    If PaidTGPs.Length > 2 And Microsoft.VisualBasic.Right(PaidTGPs, 2) = ", " _
        '    Then PaidTGPs = Microsoft.VisualBasic.Mid(PaidTGPs, 1, Len(PaidTGPs) - 2)
        '    If PaidTGPs <> "" Then _
        '    CNP.Execute("Delete from Payment_Detail where Payment_ID = " & Val(txtID.Text) &
        '    " and Charge_ID = " & ChargeID & " and Not TGP_ID in (" & PaidTGPs & ")")
        '    '
        '    UpdatePayments(Val(txtID.Text))
        '    SynchronizeChargeToDetail(Val(ChargeID))
        '    If cmbARType.SelectedIndex = 1 Then
        '        UpdateEOBClaim(Trim(txtDoc.Text), GetARName(Val(txtArID.Text),
        '        cmbARType.SelectedIndex), Val(ChargeID), ChargeID)
        '        Dim EOB() As String = {"", "", "", "", ""}
        '        '0=DocNo, 1=Payer, 2=PayerCode, 3=Date, 4=Amount, 5=applied
        '        EOB(0) = Trim(txtDoc.Text)
        '        EOB(1) = GetARName(Val(txtArID.Text), cmbARType.SelectedIndex)
        '        EOB(2) = Trim(txtArID.Text)
        '        EOB(3) = Format(dtpEOBDate.Value, SystemConfig.DateFormat)
        '        EOB(4) = Format(Applied, "#0.00")
        '        SaveEOB(EOB)
        '    End If
        'End If
        ''
        'If RebillInfo(0) <> "" Then
        '    Dim AccPatInfo() As String =
        '    GetAccPatIDFromInvoice(dgvPayment.Rows(0).Cells(0).Value)
        '    RebillPatient(AccPatInfo, RebillInfo)
        'End If
        'dgvPayment.Rows.Clear()
        'txtReason.Text = ""
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
    End Sub

    Private Sub SaveEOB0(ByVal EOB() As String)
        '0=DocNo, 1=Payer, 2=PayerCode, 3=Date, 4=Amount, 5=Applied
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connString)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select * from EOBS where DocNo = '" & EOB(0) & "' and " &
        '"Payer = '" & EOB(1) & "'", CNP, ADODB.CursorTypeEnum.adOpenDynamic,
        'ADODB.LockTypeEnum.adLockOptimistic)
        'If Rs.BOF Then Rs.AddNew()
        'Rs.Fields("DocNo").Value = EOB(0)
        'Rs.Fields("Payer").Value = EOB(1)
        'Rs.Fields("PayerCode").Value = EOB(2)
        'Rs.Fields("EOBDate").Value = EOB(3)
        'Rs.Fields("EOBAmount").Value = EOB(4)
        'Rs.Update()
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
    End Sub
    Private Sub SaveEOB(ByVal EOB() As String)
        ' 0=DocNo, 1=Payer, 2=PayerCode, 3=Date, 4=Amount, 5=Applied
        Dim query As String

        Using connection As New SqlConnection(connString)
            connection.Open()

            ' Check if the record exists
            Dim recordExists As Boolean
            Using commandCheck As New SqlCommand("SELECT COUNT(*) FROM EOBS WHERE DocNo = @DocNo AND Payer = @Payer", connection)
                commandCheck.Parameters.AddWithValue("@DocNo", EOB(0))
                commandCheck.Parameters.AddWithValue("@Payer", EOB(1))
                recordExists = Convert.ToInt32(commandCheck.ExecuteScalar()) > 0
            End Using

            ' Define query based on existence of the record
            If recordExists Then
                query = "UPDATE EOBS SET PayerCode = @PayerCode, EOBDate = @EOBDate, EOBAmount = @EOBAmount WHERE DocNo = @DocNo AND Payer = @Payer"
            Else
                query = "INSERT INTO EOBS (DocNo, Payer, PayerCode, EOBDate, EOBAmount) VALUES (@DocNo, @Payer, @PayerCode, @EOBDate, @EOBAmount)"
            End If

            ' Execute Insert/Update command
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@DocNo", EOB(0))
                command.Parameters.AddWithValue("@Payer", EOB(1))
                command.Parameters.AddWithValue("@PayerCode", EOB(2))
                command.Parameters.AddWithValue("@EOBDate", EOB(3))
                command.Parameters.AddWithValue("@EOBAmount", EOB(4))
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub
    Private Sub UpdateEOBClaim0(ByVal DocNo As String, ByVal PayerName _
    As String, ByVal ChargeID As Long, ByVal PayerClaimID As String)
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connString)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select a.* from EOB_Claim a where a.DocNo = '" & DocNo &
        '"' and a.Payer = '" & PayerName & "' and a.Charge_ID = " & ChargeID, CNP,
        'ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        'If Rs.BOF Then Rs.AddNew()
        'Rs.Fields("DocNo").Value = DocNo
        'Rs.Fields("Payer").Value = Microsoft.VisualBasic.Mid(PayerName, 1, 60)
        'Rs.Fields("Charge_ID").Value = ChargeID
        'Rs.Fields("PayerClaimID").Value = PayerClaimID
        'Rs.Update()
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
    End Sub
    Private Sub UpdateEOBClaim(ByVal DocNo As String, ByVal PayerName As String, ByVal ChargeID As Long, ByVal PayerClaimID As String)
        Using connection As New SqlConnection(connString)
            connection.Open()

            ' Check if the record exists
            Dim recordExists As Boolean
            Using commandCheck As New SqlCommand("SELECT COUNT(*) FROM EOB_Claim WHERE DocNo = @DocNo AND Payer = @Payer AND Charge_ID = @ChargeID", connection)
                commandCheck.Parameters.AddWithValue("@DocNo", DocNo)
                commandCheck.Parameters.AddWithValue("@Payer", PayerName)
                commandCheck.Parameters.AddWithValue("@ChargeID", ChargeID)
                recordExists = Convert.ToInt32(commandCheck.ExecuteScalar()) > 0
            End Using

            ' Define query dynamically
            Dim query As String = If(recordExists,
            "UPDATE EOB_Claim SET PayerClaimID = @PayerClaimID WHERE DocNo = @DocNo AND Payer = @Payer AND Charge_ID = @ChargeID",
            "INSERT INTO EOB_Claim (DocNo, Payer, Charge_ID, PayerClaimID) VALUES (@DocNo, @Payer, @ChargeID, @PayerClaimID)"
        )

            ' Execute Insert/Update command
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@DocNo", DocNo)
                command.Parameters.AddWithValue("@Payer", PayerName.Substring(0, Math.Min(PayerName.Length, 60))) ' Ensure max 60 characters
                command.Parameters.AddWithValue("@ChargeID", ChargeID)
                command.Parameters.AddWithValue("@PayerClaimID", PayerClaimID)
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub
    Private Sub SynchronizeChargeToDetail0(ByVal ChargeID As Long)
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connString)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select Sum(Extend) as Total from Charge_Detail where Charge_ID = " &
        'ChargeID, CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then
        '    If Rs.Fields("Total").Value Is System.DBNull.Value _
        '    OrElse Rs.Fields("Total").Value = 0 Then
        '        CNP.Execute("Delete from Charges where ID = " & ChargeID)
        '    Else
        '        CNP.Execute("Update Charges Set NetAmount = " &
        '        Math.Round(Rs.Fields("Total").Value, 2) & ", GrossAmount = " &
        '        Math.Round(Rs.Fields("Total").Value, 2) & " where ID = " & ChargeID)
        '    End If
        'End If
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
    End Sub
    Private Sub SynchronizeChargeToDetail(ByVal ChargeID As Long)
        Using connection As New SqlConnection(connString)
            connection.Open()

            ' Retrieve the total Extend amount
            Dim totalExtend As Object = Nothing
            Using commandTotal As New SqlCommand("SELECT SUM(Extend) AS Total FROM Charge_Detail WHERE Charge_ID = @ChargeID", connection)
                commandTotal.Parameters.AddWithValue("@ChargeID", ChargeID)
                totalExtend = commandTotal.ExecuteScalar()
            End Using

            ' Determine action based on total value
            If totalExtend Is DBNull.Value OrElse Convert.ToDecimal(totalExtend) = 0 Then
                Using commandDelete As New SqlCommand("DELETE FROM Charges WHERE ID = @ChargeID", connection)
                    commandDelete.Parameters.AddWithValue("@ChargeID", ChargeID)
                    commandDelete.ExecuteNonQuery()
                End Using
            Else
                Using commandUpdate As New SqlCommand("UPDATE Charges SET NetAmount = @NetAmount, GrossAmount = @GrossAmount WHERE ID = @ChargeID", connection)
                    commandUpdate.Parameters.AddWithValue("@ChargeID", ChargeID)
                    commandUpdate.Parameters.AddWithValue("@NetAmount", Math.Round(Convert.ToDecimal(totalExtend), 2))
                    commandUpdate.Parameters.AddWithValue("@GrossAmount", Math.Round(Convert.ToDecimal(totalExtend), 2))
                    commandUpdate.ExecuteNonQuery()
                End Using
            End If
        End Using
    End Sub
    Private Sub UpdatePayments0(ByVal PmtID As Long)
        'Dim DetApplied As Double = 0
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connString)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select sum(AppliedAmount) as Applied from Payment_Detail where " &
        '"Payment_ID = " & PmtID, CNP, ADODB.CursorTypeEnum.adOpenStatic,
        'ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then
        '    If Rs.Fields("Applied").Value Is System.DBNull.Value Then
        '        DetApplied = 0
        '    Else
        '        DetApplied = Math.Round(Rs.Fields("Applied").Value, 2)
        '    End If
        'End If
        'Rs.Close()
        'CNP.Execute("Update Payments Set UnApplied = Amount - " & DetApplied _
        '& " where " & "ID = " & PmtID)
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
    End Sub

    Private Sub UpdatePayments(ByVal PmtID As Long)
        Dim DetApplied As Double = 0

        Using connection As New SqlConnection(connString)
            connection.Open()

            ' Retrieve sum of AppliedAmount
            Dim query As String = "SELECT SUM(AppliedAmount) FROM Payment_Detail WHERE Payment_ID = @PaymentID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@PaymentID", PmtID)

                Dim result As Object = command.ExecuteScalar()
                DetApplied = If(result Is DBNull.Value OrElse result Is Nothing, 0, Math.Round(Convert.ToDouble(result), 2))
            End Using

            ' Update UnApplied amount in Payments table
            Dim updateQuery As String = "UPDATE Payments SET UnApplied = Amount - @DetApplied WHERE ID = @PaymentID"

            Using updateCommand As New SqlCommand(updateQuery, connection)
                updateCommand.Parameters.AddWithValue("@PaymentID", PmtID)
                updateCommand.Parameters.AddWithValue("@DetApplied", DetApplied)

                updateCommand.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Function IsBilled0(ByVal ChargeID As Long) As Boolean
        'Dim Billed As Boolean = False
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connString)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select * from Charges where ID = " & ChargeID, CNP,
        'ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then Billed = True
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
        'Return Billed
    End Function
    Private Function IsBilled(ByVal ChargeID As Long) As Boolean

        Dim Billed As Boolean = False
        Dim query As String = "SELECT COUNT(*) FROM Charges WHERE ID = @ChargeID"

        Using connection As New SqlConnection(connString)
            connection.Open()
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@ChargeID", ChargeID)
                Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
                If count > 0 Then
                    Billed = True
                End If
            End Using
        End Using

        Return Billed
    End Function


    Private Function GetNextChargeID0() As Long

        'Dim CNP As New ADODB.Connection
        'CNP.Open(connString)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select Max(ID) as LastID from Charges", CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Rs.Fields("LastID").Value Is System.DBNull.Value Then
        '    GetNextChargeID = 1
        'Else
        '    GetNextChargeID = Rs.Fields("LastID").Value + 1
        'End If
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
    End Function
    Private Function GetNextChargeID() As Long
        Dim query As String = "SELECT MAX(ID) AS LastID FROM Charges"
        Dim LastID As Object = Nothing

        Using connection As New SqlConnection(connString)
            connection.Open()
            Using command As New SqlCommand(query, connection)
                LastID = command.ExecuteScalar()
            End Using
        End Using

        If IsDBNull(LastID) OrElse LastID Is Nothing Then
            Return 1
        Else
            Return Convert.ToInt64(LastID) + 1
        End If
    End Function


    Private Function GetNextPatChargeID0(ByVal AccID As Long, ByVal Primary As _
    Boolean, ByVal ArType As Int16, ByVal SvcDate As Date) As Long  'IsPrimary = False, ArType= Pat, svcdate
        'Dim ChargeID As Long = Nothing
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connString)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select * from Accession_Charge where Accession_ID = " & AccID & " and IsPrimary = 0 " &
        '"and ArType = 2 and not Charge_ID in (Select ID from Charges )",'where IsPrimary = 0 and ArType = 2
        'CNP, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        'If Rs.BOF Then
        '    Rs.AddNew()
        '    Rs.Fields("Accession_ID").Value = AccID
        '    Rs.Fields("Charge_ID").Value = GetNextChargeID()
        '    ChargeID = Rs.Fields("Charge_ID").Value
        '    Rs.Fields("IsPrimary").Value = Primary
        '    Rs.Fields("ArType").Value = ArType
        '    Rs.Fields("Created_On").Value = Date.Now
        '    Rs.Fields("Created_By").Value = ThisUser.ID
        '    Rs.Fields("Edited_On").Value = Date.Now
        '    Rs.Fields("Edited_By").Value = ThisUser.ID
        '    Rs.Update()
        'Else
        '    ChargeID = Rs.Fields("Charge_ID").Value
        'End If
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
        'Return ChargeID

    End Function
    Private Function GetNextPatChargeID(ByVal AccID As Long, ByVal Primary As Boolean, ByVal ArType As Int16, ByVal SvcDate As Date) As Long
        ' IsPrimary = False, ArType = Pat, svcdate
        Dim ChargeID As Long = 0
        Dim querySelect As String = "SELECT * FROM Accession_Charge WHERE Accession_ID = @AccID AND IsPrimary = 0 AND ArType = 2 AND NOT Charge_ID IN (SELECT ID FROM Charges)"
        Dim queryInsert As String = "INSERT INTO Accession_Charge (Accession_ID, Charge_ID, IsPrimary, ArType, Created_On, Created_By, Edited_On, Edited_By) VALUES (@AccID, @ChargeID, @IsPrimary, @ArType, @CreatedOn, @CreatedBy, @EditedOn, @EditedBy)"
        Dim queryUpdate As String = "UPDATE Accession_Charge SET IsPrimary = @IsPrimary, ArType = @ArType, Edited_On = @EditedOn, Edited_By = @EditedBy WHERE Accession_ID = @AccID AND Charge_ID = @ChargeID"

        Using connection As New SqlClient.SqlConnection(connString)
            connection.Open()
            Using commandSelect As New SqlClient.SqlCommand(querySelect, connection)
                commandSelect.Parameters.AddWithValue("@AccID", AccID)
                Using reader As SqlClient.SqlDataReader = commandSelect.ExecuteReader()
                    If reader.HasRows Then
                        reader.Read()
                        ChargeID = Convert.ToInt64(reader("Charge_ID"))
                    End If
                End Using
            End Using

            If ChargeID = 0 Then
                ChargeID = GetNextChargeID()
                Using commandInsert As New SqlClient.SqlCommand(queryInsert, connection)
                    commandInsert.Parameters.AddWithValue("@AccID", AccID)
                    commandInsert.Parameters.AddWithValue("@ChargeID", ChargeID)
                    commandInsert.Parameters.AddWithValue("@IsPrimary", Primary)
                    commandInsert.Parameters.AddWithValue("@ArType", ArType)
                    commandInsert.Parameters.AddWithValue("@CreatedOn", Date.Now)
                    commandInsert.Parameters.AddWithValue("@CreatedBy", ThisUser.ID)
                    commandInsert.Parameters.AddWithValue("@EditedOn", Date.Now)
                    commandInsert.Parameters.AddWithValue("@EditedBy", ThisUser.ID)
                    commandInsert.ExecuteNonQuery()
                End Using
            Else
                Using commandUpdate As New SqlClient.SqlCommand(queryUpdate, connection)
                    commandUpdate.Parameters.AddWithValue("@IsPrimary", Primary)
                    commandUpdate.Parameters.AddWithValue("@ArType", ArType)
                    commandUpdate.Parameters.AddWithValue("@EditedOn", Date.Now)
                    commandUpdate.Parameters.AddWithValue("@EditedBy", ThisUser.ID)
                    commandUpdate.Parameters.AddWithValue("@AccID", AccID)
                    commandUpdate.Parameters.AddWithValue("@ChargeID", ChargeID)
                    commandUpdate.ExecuteNonQuery()
                End Using
            End If
        End Using
        Return ChargeID
    End Function

    Private Sub RebillPatient(ByVal AccPatInfo() As String, ByVal RebillInfo() As String)
        Dim AccID As String = AccPatInfo(0)
        Dim PatID As String = AccPatInfo(1)
        Dim SvcDate As String = AccPatInfo(2)
        Dim ChargeID As Long = GetNextPatChargeID(AccID, 0, 2, SvcDate)  'IsPrimary = False, ArType= Pat
        Dim CLMCharge As Double = 0
        Dim Reason As String = Trim(txtReason.Text)

        ' Determine parent charge id
        Dim prnt As Object = DBNull.Value
        If txtInvID.Text = "" Then
            For ii As Integer = 0 To dgvInvoices.RowCount - 1
                If dgvInvoices.Rows(ii).Selected = True Then
                    prnt = dgvInvoices.Rows(ii).Cells(0).Value
                    Exit For
                End If
            Next
        Else
            prnt = txtInvID.Text
        End If

        Using connection As New SqlClient.SqlConnection(connString)
            connection.Open()

            ' Insert Charges record
            Dim insertChargesQuery As String = "INSERT INTO Charges (ID, Accession_ID, Ar_ID, ArType, IsPrimary, BillReason, NetAmount, TaxAmount, GrossAmount, Bill_Date, Svc_Date, Due_Date, Term, Note, Output, LastEditedOn, EditedBy, Billing_Status_Code, Orignal_Claim_Number, parent_charge_id) " &
                    "VALUES (@ID, @Accession_ID, @Ar_ID, @ArType, @IsPrimary, @BillReason, @NetAmount, @TaxAmount, @GrossAmount, @Bill_Date, @Svc_Date, @Due_Date, @Term, @Note, @Output, @LastEditedOn, @EditedBy, @Billing_Status_Code, @Orignal_Claim_Number, @ParentChargeID)"

            Using cmdInsertCharges As New SqlClient.SqlCommand(insertChargesQuery, connection)
                cmdInsertCharges.Parameters.AddWithValue("@ID", ChargeID)
                cmdInsertCharges.Parameters.AddWithValue("@Accession_ID", AccID)
                cmdInsertCharges.Parameters.AddWithValue("@Ar_ID", PatID)
                cmdInsertCharges.Parameters.AddWithValue("@ArType", 2)
                cmdInsertCharges.Parameters.AddWithValue("@IsPrimary", 0)
                cmdInsertCharges.Parameters.AddWithValue("@BillReason", Reason)
                cmdInsertCharges.Parameters.AddWithValue("@NetAmount", 0) ' Will update after details
                cmdInsertCharges.Parameters.AddWithValue("@TaxAmount", 0)
                cmdInsertCharges.Parameters.AddWithValue("@GrossAmount", 0) ' Will update after details
                cmdInsertCharges.Parameters.AddWithValue("@Bill_Date", dtpEOBDate.Value)
                cmdInsertCharges.Parameters.AddWithValue("@Svc_Date", SvcDate)
                cmdInsertCharges.Parameters.AddWithValue("@Due_Date", DateAdd(DateInterval.Day, 15, dtpEOBDate.Value))
                cmdInsertCharges.Parameters.AddWithValue("@Term", "Net 15 Days")
                cmdInsertCharges.Parameters.AddWithValue("@Note", "")
                cmdInsertCharges.Parameters.AddWithValue("@Output", 0)
                cmdInsertCharges.Parameters.AddWithValue("@LastEditedOn", dtpEOBDate.Value)
                cmdInsertCharges.Parameters.AddWithValue("@EditedBy", ThisUser.ID)
                cmdInsertCharges.Parameters.AddWithValue("@Billing_Status_Code", If(Corrected.Checked, "7", If(VoidClaim.Checked, "8", "")))
                cmdInsertCharges.Parameters.AddWithValue("@Orignal_Claim_Number", OrgClaimNumber.Text)
                If prnt Is Nothing OrElse prnt.ToString() = "" Then
                    cmdInsertCharges.Parameters.AddWithValue("@ParentChargeID", DBNull.Value)
                Else
                    cmdInsertCharges.Parameters.AddWithValue("@ParentChargeID", prnt)
                End If
                cmdInsertCharges.ExecuteNonQuery()
            End Using

            ' Insert Charge_Detail records
            For i As Integer = 0 To RebillInfo.Length - 1
                If RebillInfo(i) <> "" AndAlso InStr(RebillInfo(i), "|") > 0 Then
                    Dim Data() As String = Split(RebillInfo(i), "|")
                    Dim TGPID As Integer = CInt(Data(0))
                    Dim CPT As String = Data(1)
                    Dim Price As Double = Val(Data(2))

                    ' Remove all dashes from CPT
                    Do Until InStr(CPT, "-") = 0
                        CPT = Microsoft.VisualBasic.Mid(CPT, InStr(CPT, "-") + 1)
                    Loop

                    ' Check if Charge_Detail exists
                    Dim selectDetailQuery As String = "SELECT * FROM Charge_Detail WHERE Charge_ID = @ChargeID AND TGP_ID = @TGPID"
                    Dim exists As Boolean = False
                    Using cmdSelectDetail As New SqlClient.SqlCommand(selectDetailQuery, connection)
                        cmdSelectDetail.Parameters.AddWithValue("@ChargeID", ChargeID)
                        cmdSelectDetail.Parameters.AddWithValue("@TGPID", TGPID)
                        Using reader As SqlClient.SqlDataReader = cmdSelectDetail.ExecuteReader()
                            exists = reader.HasRows
                        End Using
                    End Using

                    Dim insertDetailQuery As String = "INSERT INTO Charge_Detail (Charge_ID, TGP_ID, Ordinal, CPT_Code, ICD9, Unit, LinePrice, Extend, Mod1, Mod2, Mod3, Mod4, POS_Code, Billed_On, Billed_By) VALUES (@ChargeID, @TGPID, @Ordinal, @CPT_Code, @ICD9, @Unit, @LinePrice, @Extend, @Mod1, @Mod2, @Mod3, @Mod4, @POS_Code, @Billed_On, @Billed_By)"

                    Dim updateDetailQuery As String = "UPDATE Charge_Detail SET Ordinal = @Ordinal, CPT_Code = @CPT_Code, ICD9 = @ICD9, Unit = @Unit,  LinePrice = ISNULL(LinePrice,0) + @LinePrice, Extend = ISNULL(Extend,0) + @Extend,  Mod1 = @Mod1, Mod2 = @Mod2, Mod3 = @Mod3, Mod4 = @Mod4, POS_Code = @POS_Code, Billed_On = @Billed_On, Billed_By = @Billed_By  WHERE Charge_ID = @ChargeID AND TGP_ID = @TGPID"

                    Dim query As String
                    If Not exists Then
                        query = insertDetailQuery
                    Else
                        query = updateDetailQuery
                    End If

                    Using cmdInsertDetail As New SqlClient.SqlCommand(query, connection)
                        cmdInsertDetail.Parameters.AddWithValue("@ChargeID", ChargeID)
                        cmdInsertDetail.Parameters.AddWithValue("@TGPID", TGPID)
                        cmdInsertDetail.Parameters.AddWithValue("@Ordinal", i)
                        cmdInsertDetail.Parameters.AddWithValue("@CPT_Code", CPT)
                        cmdInsertDetail.Parameters.AddWithValue("@ICD9", "")
                        cmdInsertDetail.Parameters.AddWithValue("@Unit", 1)
                        cmdInsertDetail.Parameters.AddWithValue("@LinePrice", Price)
                        cmdInsertDetail.Parameters.AddWithValue("@Extend", Price)
                        cmdInsertDetail.Parameters.AddWithValue("@Mod1", "")
                        cmdInsertDetail.Parameters.AddWithValue("@Mod2", "")
                        cmdInsertDetail.Parameters.AddWithValue("@Mod3", "")
                        cmdInsertDetail.Parameters.AddWithValue("@Mod4", "")
                        cmdInsertDetail.Parameters.AddWithValue("@POS_Code", "81")
                        cmdInsertDetail.Parameters.AddWithValue("@Billed_On", dtpEOBDate.Value)
                        cmdInsertDetail.Parameters.AddWithValue("@Billed_By", ThisUser.ID)
                        cmdInsertDetail.ExecuteNonQuery()
                    End Using

                    CLMCharge += Price

                    'If Not exists Then
                    'Else
                    '    Dim getExtendQuery As String = "SELECT Extend FROM Charge_Detail WHERE Charge_ID = @ChargeID AND TGP_ID = @TGPID"
                    '    Using cmdGetExtend As New SqlClient.SqlCommand(getExtendQuery, connection)
                    '        cmdGetExtend.Parameters.AddWithValue("@ChargeID", ChargeID)
                    '        cmdGetExtend.Parameters.AddWithValue("@TGPID", TGPID)
                    '        Dim extendVal = cmdGetExtend.ExecuteScalar()
                    '        If Not IsDBNull(extendVal) Then
                    '            CLMCharge += Convert.ToDouble(extendVal)
                    '        End If
                    '    End Using
                    'End If

                End If
            Next

            ' Update Charges with correct NetAmount and GrossAmount
            Dim updateChargesQuery As String = "UPDATE Charges SET NetAmount = @NetAmount, GrossAmount = @GrossAmount WHERE ID = @ChargeID"
            Using cmdUpdateCharges As New SqlClient.SqlCommand(updateChargesQuery, connection)
                cmdUpdateCharges.Parameters.AddWithValue("@NetAmount", Math.Round(CLMCharge, 2))
                cmdUpdateCharges.Parameters.AddWithValue("@GrossAmount", Math.Round(CLMCharge, 2))
                cmdUpdateCharges.Parameters.AddWithValue("@ChargeID", ChargeID)
                cmdUpdateCharges.ExecuteNonQuery()
            End Using
        End Using


    End Sub

    Private Sub RebillPatient0(ByVal AccPatInfo() As String, ByVal RebillInfo() As String)
        'Dim AccID As String = AccPatInfo(0)
        'Dim PatID As String = AccPatInfo(1)
        'Dim SvcDate As String = AccPatInfo(2)
        'Dim ChargeID As Long = GetNextPatChargeID(AccID, 0, 2, SvcDate)  'IsPrimary = False, ArType= Pat
        'Dim CLMCharge As Double = 0
        'Dim CNP As New ADODB.Connection
        ''CNP.Open(connString)
        'Dim Reason As String = Trim(txtReason.Text)
        ''Try
        ''    For i As Integer = 0 To RebillInfo.Length - 1
        ''        If RebillInfo(i) <> "" AndAlso InStr(RebillInfo(i), "|") > 0 Then
        ''            Dim Data() As String = Split(RebillInfo(i), "|")
        ''            CLMCharge += Data(2)
        ''        End If

        ''    Next

        ''    If CLMCharge > 0 Then
        ''        CNP.Execute("Delete from Charges where ID = " & ChargeID)
        ''        '
        ''        CNP.Execute("Insert into Charges (ID, Accession_ID, Ar_ID, " &
        ''        "ArType, IsPrimary, BillReason, NetAmount, TaxAmount, GrossAmount, " &
        ''        "Bill_Date, Svc_Date, Due_Date, Term, Note, Output, LastEditedOn, " &
        ''        "EditedBy,Billing_Status_Code,Orignal_Claim_Number) values(" & ChargeID & ", " & AccID & ", " & PatID &
        ''        ", 2, 0, '" & Reason & "', " & Math.Round(CLMCharge, 2) & ", 0, " &
        ''        Math.Round(CLMCharge, 2) & ", '" & dtpEOBDate.Value & "', '" &
        ''        SvcDate & "', '" & DateAdd(DateInterval.Day, 15, dtpEOBDate.Value) &
        ''        "', 'Net 15 Days', '', 0, '" & dtpEOBDate.Value & "', " &
        ''        ThisUser.ID & ",'" & IIf(Corrected.Checked, "7", IIf(VoidClaim.Checked, "8", "")) & "','" & OrgClaimNumber.Text & "')")
        ''        SynchronizeChargeToDetail(ChargeID)
        ''    Else
        ''        CNP.Execute("Delete from Charges where ID = " & ChargeID)
        ''    End If
        ''    CNP.Close()
        ''    'CNP = Nothing
        ''Catch ex As Exception

        ''End Try

        'CNP.Open(connString)

        'Dim prnt = ""

        'If txtInvID.Text = "" Then
        '    For ii As Integer = 0 To dgvInvoices.RowCount - 1
        '        If dgvInvoices.Rows(ii).Selected = True Then
        '            prnt = dgvInvoices.Rows(ii).Cells(0).Value
        '            Exit For
        '        End If

        '    Next
        'Else
        '    prnt = txtInvID.Text
        'End If

        'Dim query As String = "Insert into Charges (ID, Accession_ID, Ar_ID, " &
        '        "ArType, IsPrimary, BillReason, NetAmount, TaxAmount, GrossAmount, " &
        '        "Bill_Date, Svc_Date, Due_Date, Term, Note, Output, LastEditedOn, " &
        '        "EditedBy,Billing_Status_Code,Orignal_Claim_Number --,parent_charge_id
        '        ) values(" & ChargeID & ", " & AccID & ", " & PatID &
        '        ", 2, 0, '" & Reason & "', " & Math.Round(CLMCharge, 2) & ", 0, " &
        '        Math.Round(CLMCharge, 2) & ", '" & dtpEOBDate.Value & "', '" &
        '        SvcDate & "', '" & DateAdd(DateInterval.Day, 15, dtpEOBDate.Value) &
        '        "', 'Net 15 Days', '', 0, '" & dtpEOBDate.Value & "', " &
        '        ThisUser.ID & ",'" & IIf(Corrected.Checked, "7", IIf(VoidClaim.Checked, "8", "")) & "','" & OrgClaimNumber.Text & "'  --, " & IIf(prnt = "", DBNull.Value, prnt) & "
        '        )"
        'CNP.Execute(query)



        'For i As Integer = 0 To RebillInfo.Length - 1
        '    If RebillInfo(i) <> "" AndAlso InStr(RebillInfo(i), "|") > 0 Then
        '        Dim Data() As String = Split(RebillInfo(i), "|")
        '        '0=TGPID, 1=CPT, 2=Price
        '        Dim Rs As New ADODB.Recordset
        '        Rs.Open("Select * from Charge_Detail where Charge_ID = " & ChargeID &
        '        " and TGP_ID = " & Data(0), CNP, ADODB.CursorTypeEnum.adOpenDynamic,
        '        ADODB.LockTypeEnum.adLockOptimistic)
        '        If Rs.BOF Then Rs.AddNew()
        '        Rs.Fields("Charge_ID").Value = ChargeID
        '        Rs.Fields("TGP_ID").Value = Data(0)
        '        If Rs.Fields("Ordinal").Value Is Nothing OrElse
        '        Rs.Fields("Ordinal").Value Is System.DBNull.Value Then _
        '        Rs.Fields("Ordinal").Value = i
        '        Do Until InStr(Data(1), "-") = 0
        '            Data(1) = Microsoft.VisualBasic.Mid(Data(1), InStr(Data(1), "-") + 1)
        '        Loop
        '        Rs.Fields("CPT_Code").Value = Data(1)
        '        Rs.Fields("ICD9").Value = ""
        '        Rs.Fields("Unit").Value = 1
        '        If Rs.Fields("LinePrice").Value Is Nothing OrElse
        '        Rs.Fields("LinePrice").Value Is System.DBNull.Value Then
        '            Rs.Fields("LinePrice").Value = Data(2)
        '        Else
        '            Rs.Fields("LinePrice").Value = Math.Round(
        '            Rs.Fields("LinePrice").Value + Val(Data(2)), 2)
        '        End If
        '        If Rs.Fields("Extend").Value Is Nothing OrElse
        '        Rs.Fields("Extend").Value Is System.DBNull.Value Then
        '            Rs.Fields("Extend").Value = Data(2)
        '        Else
        '            Rs.Fields("Extend").Value = Math.Round(
        '            Rs.Fields("Extend").Value + Val(Data(2)), 2)
        '        End If
        '        Rs.Fields("Mod1").Value = ""
        '        Rs.Fields("Mod2").Value = ""
        '        Rs.Fields("Mod3").Value = ""
        '        Rs.Fields("Mod4").Value = ""
        '        Rs.Fields("POS_Code").Value = "81"
        '        Rs.Fields("Billed_On").Value = dtpEOBDate.Value
        '        Rs.Fields("Billed_By").Value = ThisUser.ID
        '        CLMCharge += Rs.Fields("Extend").Value


        '        Try
        '            'Dim prnt = ""
        '            If txtInvID.Text = "" Then
        '                For ii As Integer = 0 To dgvInvoices.RowCount - 1
        '                    If dgvInvoices.Rows(ii).Selected = True Then
        '                        prnt = dgvInvoices.Rows(ii).Cells(0).Value
        '                        Exit For
        '                    End If

        '                Next
        '            Else
        '                prnt = txtInvID.Text
        '            End If




        '        Catch ex As Exception

        '        End Try

        '        Rs.Update()
        '        Rs.Close()
        '        Rs = Nothing
        '    End If
        'Next
        ''

        'query = $" update Charges set NetAmount =  { Math.Round(CLMCharge, 2)}, GrossAmount = {Math.Round(CLMCharge, 2)} where ID =  {ChargeID }"

        'CNP.Execute(query)
    End Sub

    Private Function GetAccPatIDFromInvoice0(ByVal ChargeID As Long) As String()
        'Dim AccPatID() As String = {"", "", ""} '0=AccID, 1=PatID, 2=SvcDate
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connString)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select a.*, b.Patient_ID from Charges a inner join " &
        '"Requisitions b on a.Accession_ID = b.ID where a.ID = " & ChargeID,
        'CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then
        '    AccPatID(0) = Rs.Fields("Accession_ID").Value.ToString
        '    AccPatID(1) = Rs.Fields("Patient_ID").Value.ToString
        '    AccPatID(2) = Rs.Fields("Svc_Date").Value
        '    Rs.Update()
        'End If
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
        'Return AccPatID
    End Function

    Private Function GetAccPatIDFromInvoice(ByVal ChargeID As Long) As String()
        Dim AccPatID() As String = {"", "", ""} ' 0=AccID, 1=PatID, 2=SvcDate

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT a.Accession_ID, b.Patient_ID, a.Svc_Date " &
                              "FROM Charges a INNER JOIN Requisitions b " &
                              "ON a.Accession_ID = b.ID WHERE a.ID = @ChargeID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@ChargeID", ChargeID)

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        AccPatID(0) = reader("Accession_ID").ToString()
                        AccPatID(1) = reader("Patient_ID").ToString()
                        AccPatID(2) = reader("Svc_Date").ToString()
                    End If
                End Using
            End Using
        End Using

        Return AccPatID
    End Function

    Private Function LineItemRebilled0(ByVal AccID As Long, ByVal BillType As Byte,
    ByVal ArID As Long, ByVal IsbillPrimary As Boolean, ByVal TGPID As Integer) As String()
        'Dim BillInfo() As String = {"", "", ""}
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connString)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select a.ID as InvID, b.Billed_On, b.Billed_By from Charges a inner join " &
        '"Charge_Detail b on a.ID = b.Charge_ID where (a.IsPrimary = NULL or a.IsPrimary = " &
        'Val(IsbillPrimary) & ") and a.Accession_ID = " & AccID & " and a.ArType = " & BillType _
        '& " and a.Ar_Id = " & ArID & " and b.TGP_ID = " & TGPID, CNP, ADODB.CursorTypeEnum.adOpenStatic,
        'ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then
        '    BillInfo(0) = Rs.Fields("InvID").Value.ToString
        '    BillInfo(1) = Rs.Fields("Billed_On").Value.ToString
        '    BillInfo(2) = Rs.Fields("Billed_By").Value.ToString
        'End If
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
        'Return BillInfo
    End Function

    Private Function LineItemRebilled(ByVal AccID As Long, ByVal BillType As Byte,
    ByVal ArID As Long, ByVal IsbillPrimary As Boolean, ByVal TGPID As Integer) As String()

        Dim BillInfo() As String = {"", "", ""} ' 0=InvID, 1=Billed_On, 2=Billed_By

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT a.ID AS InvID, b.Billed_On, b.Billed_By " &
                              "FROM Charges a INNER JOIN Charge_Detail b ON a.ID = b.Charge_ID " &
                              "WHERE (a.IsPrimary IS NULL OR a.IsPrimary = @IsPrimary) " &
                              "AND a.Accession_ID = @AccID AND a.ArType = @BillType " &
                              "AND a.Ar_Id = @ArID AND b.TGP_ID = @TGPID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@IsPrimary", IsbillPrimary)
                command.Parameters.AddWithValue("@AccID", AccID)
                command.Parameters.AddWithValue("@BillType", BillType)
                command.Parameters.AddWithValue("@ArID", ArID)
                command.Parameters.AddWithValue("@TGPID", TGPID)

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        BillInfo(0) = reader("InvID").ToString()
                        BillInfo(1) = reader("Billed_On").ToString()
                        BillInfo(2) = reader("Billed_By").ToString()
                    End If
                End Using
            End Using
        End Using

        Return BillInfo
    End Function
    Private Function GetPatientID0(ByVal AccID As Long) As Long
        'Dim PatID As Long
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connString)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select Patient_ID from Requisitions where ID = " & AccID,
        'CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then PatID = Rs.Fields("Patient_ID").Value
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
        'Return PatID
    End Function

    Private Function GetPatientID(ByVal AccID As Long) As Long
        Dim PatID As Long = 0 ' Default value to avoid uninitialized return

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT Patient_ID FROM Requisitions WHERE ID = @AccID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@AccID", AccID)

                Dim result As Object = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    PatID = Convert.ToInt64(result)
                End If
            End Using
        End Using

        Return PatID
    End Function

    Private Function GetServiceDate0(ByVal ChargeID As Long) As Date
        'Dim SvcDate As Date = Date.Now
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connString)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select Svc_Date from Charges where ID = " & ChargeID,
        'CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then SvcDate = Rs.Fields("Svc_Date").Value
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
        'Return SvcDate
    End Function

    Private Function GetServiceDate(ByVal ChargeID As Long) As Date
        Dim SvcDate As Date = Date.Now ' Default to current date in case of no result

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT Svc_Date FROM Charges WHERE ID = @ChargeID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@ChargeID", ChargeID)

                Dim result As Object = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    SvcDate = Convert.ToDateTime(result)
                End If
            End Using
        End Using

        Return SvcDate
    End Function

    Private Function GetCPT0(ByVal TGPID As Integer) As String
        'Dim CPT As String = ""
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connString)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select CPT_Code from Tests where ID = " & TGPID,
        'CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then CPT = Rs.Fields("CPT_Code").Value
        'Rs.Close()
        'If CPT = "" Then
        '    Rs.Open("Select CPT_Code from Groups where ID = " & TGPID,
        '    CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        '    If Not Rs.BOF Then CPT = Rs.Fields("CPT_Code").Value
        '    Rs.Close()
        'End If
        'If CPT = "" Then
        '    Rs.Open("Select CPT_Code from Profiles where ID = " & TGPID,
        '    CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        '    If Not Rs.BOF Then CPT = Rs.Fields("CPT_Code").Value
        '    Rs.Close()
        'End If
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
        'Return CPT
    End Function

    Private Function GetCPT(ByVal TGPID As Integer) As String
        Dim CPT As String = ""

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim queries As String() = {
            "SELECT CPT_Code FROM Tests WHERE ID = @TGPID",
            "SELECT CPT_Code FROM Groups WHERE ID = @TGPID",
            "SELECT CPT_Code FROM Profiles WHERE ID = @TGPID"
        }

            For Each query In queries
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@TGPID", TGPID)

                    Dim result As Object = command.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        CPT = result.ToString()
                        Exit For ' Stop checking once a valid CPT_Code is found
                    End If
                End Using
            Next
        End Using

        Return CPT
    End Function

    Private Function GetOrdProviderID0(ByVal AccID As Long) As Long
        'Dim ProvID As Long
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connString)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select OrderingProvider_ID from Requisitions where ID = " & AccID,
        'CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then ProvID = Rs.Fields("OrderingProvider_ID").Value
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
        'Return ProvID
    End Function

    Private Function GetOrdProviderID(ByVal AccID As Long) As Long
        Dim ProvID As Long = 0 ' Default value to avoid uninitialized return

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT OrderingProvider_ID FROM Requisitions WHERE ID = @AccID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@AccID", AccID)

                Dim result As Object = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    ProvID = Convert.ToInt64(result)
                End If
            End Using
        End Using

        Return ProvID
    End Function
    Private Sub UpdatePaymentHistory0(ByVal ArType As Integer, ByVal ArID As Long, ByVal TGPID As Integer, ByVal Pmnt As Double)
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connString)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select * from PaymentHistory where ArType = " & ArType &
        '" and Ar_ID = " & ArID & " and TGP_ID = " & TGPID, CNP,
        'ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        'If Rs.BOF Then Rs.AddNew()
        'Rs.Fields("ArType").Value = ArType
        'Rs.Fields("Ar_ID").Value = ArID
        'Rs.Fields("TGP_ID").Value = TGPID
        'Rs.Fields("Amount").Value = Pmnt
        'Rs.Fields("Edited_On").Value = Date.Now
        'Rs.Fields("Edited_By").Value = ThisUser.ID
        'Rs.Update()
        'Rs.Close()
        'CNP.Close()
        'CNP = Nothing
        'Rs = Nothing
    End Sub

    Private Sub UpdatePaymentHistory(ByVal ArType As Integer, ByVal ArID As Long, ByVal TGPID As Integer, ByVal Pmnt As Double)
        Using connection As New SqlConnection(connString)
            connection.Open()

            ' Check if record exists
            Dim checkQuery As String = "SELECT COUNT(*) FROM PaymentHistory WHERE ArType = @ArType AND Ar_ID = @ArID AND TGP_ID = @TGPID"
            Dim recordExists As Boolean = False

            Using checkCommand As New SqlCommand(checkQuery, connection)
                checkCommand.Parameters.AddWithValue("@ArType", ArType)
                checkCommand.Parameters.AddWithValue("@ArID", ArID)
                checkCommand.Parameters.AddWithValue("@TGPID", TGPID)

                recordExists = Convert.ToInt32(checkCommand.ExecuteScalar()) > 0
            End Using

            ' Insert or Update payment history
            Dim query As String
            If recordExists Then
                query = "UPDATE PaymentHistory SET Amount = @Amount, Edited_On = @EditedOn, Edited_By = @EditedBy WHERE ArType = @ArType AND Ar_ID = @ArID AND TGP_ID = @TGPID"
            Else
                query = "INSERT INTO PaymentHistory (ArType, Ar_ID, TGP_ID, Amount, Edited_On, Edited_By) VALUES (@ArType, @ArID, @TGPID, @Amount, @EditedOn, @EditedBy)"
            End If

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@ArType", ArType)
                command.Parameters.AddWithValue("@ArID", ArID)
                command.Parameters.AddWithValue("@TGPID", TGPID)
                command.Parameters.AddWithValue("@Amount", Pmnt)
                command.Parameters.AddWithValue("@EditedOn", Date.Now)
                command.Parameters.AddWithValue("@EditedBy", ThisUser.ID)

                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub
    Private Function InvoiceInGrid(ByVal InvID As Long) As Boolean
        Dim InList As Boolean = False
        For i As Integer = 0 To dgvInvoices.RowCount - 1
            If dgvInvoices.Rows(i).Cells(0).Value = InvID Then
                InList = True
                Exit For
            End If
        Next
        Return InList
    End Function

    Private Sub PopulateInvoice0(ByVal INVID As Long)
        ''dgvInvoices.Rows.Clear()
        'Dim AccID As String = ""
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connString)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select * from Charges where ID = " & INVID, CNP,
        'ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then AccID = Rs.Fields("Accession_ID").Value
        'Rs.Close()
        'If AccID <> "" Then
        '    Rs.Open("Select a.*, c.LastName, c.FirstName from Charges a inner join " &
        '    "(Requisitions b inner join Patients c on c.ID = b.Patient_ID) on b.ID " &
        '    "=a.Accession_ID where a.Accession_ID = " & AccID, CNP,
        '    ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        '    If Not Rs.BOF Then
        '        Do Until Rs.EOF
        '            If Not InvoiceInGrid(Rs.Fields("ID").Value) Then
        '                If Rs.Fields("Svc_Date").Value IsNot System.DBNull.Value AndAlso
        '                Rs.Fields("GrossAmount").Value IsNot System.DBNull.Value Then
        '                    dgvInvoices.Rows.Add(Rs.Fields("ID").Value, AccID,
        '                    Rs.Fields("LastName").Value & ", " & Rs.Fields("FirstName").Value,
        '                    Format(Rs.Fields("Svc_Date").Value, SystemConfig.DateFormat),
        '                    Format(Rs.Fields("GrossAmount").Value, "0.00"))
        '                Else
        '                    dgvInvoices.Rows.Add(Rs.Fields("ID").Value, AccID,
        '                    Rs.Fields("LastName").Value & ", " & Rs.Fields("FirstName").Value,
        '                    "", "0.00")
        '                End If
        '            End If
        '            Rs.MoveNext()
        '        Loop
        '    End If
        '    Rs.Close()
        '    Rs = Nothing
        'End If
        'CNP.Close()
        'CNP = Nothing
    End Sub

    Private Sub PopulateInvoice(ByVal INVID As Long)
        dgvInvoices.Rows.Clear() ' Ensure grid is cleared before populating

        Using connection As New SqlConnection(connString)
            connection.Open()

            ' Get Accession_ID from Charges
            Dim AccID As String = ""
            Dim queryAccID As String = "SELECT Accession_ID FROM Charges WHERE ID = @INVID"

            Using cmdAccID As New SqlCommand(queryAccID, connection)
                cmdAccID.Parameters.AddWithValue("@INVID", INVID)
                Dim result As Object = cmdAccID.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    AccID = result.ToString()
                End If
            End Using

            ' If Accession_ID exists, retrieve related patient details
            If AccID <> "" Then
                Dim query As String = "
                SELECT a.ID, a.Accession_ID, c.LastName, c.FirstName, a.Svc_Date, a.GrossAmount
                FROM Charges a 
                INNER JOIN Requisitions b ON a.Accession_ID = b.ID
                INNER JOIN Patients c ON c.ID = b.Patient_ID
                WHERE a.Accession_ID = @AccID"

                Using cmd As New SqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@AccID", AccID)

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            If Not InvoiceInGrid(reader("ID")) Then
                                dgvInvoices.Rows.Add(reader("ID"), AccID,
                                $"{reader("LastName")}, {reader("FirstName")}",
                                If(IsDBNull(reader("Svc_Date")), "", Format(reader("Svc_Date"), SystemConfig.DateFormat)),
                                If(IsDBNull(reader("GrossAmount")), "0.00", Format(reader("GrossAmount"), "0.00")))
                            End If
                        End While
                    End Using
                End Using
            End If
        End Using
    End Sub

    Private Sub DisplayARName0(ByVal ArID As Long)
        'txtARName.Text = ""
        'Dim Rs As New ADODB.Recordset
        ''Dim Paid As double = 0
        ''Dim TSTCPT As String = ""
        ''Dim i As Integer = 1
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connString)

        'If cmbARType.SelectedIndex = 0 Then     'Client
        '    Rs.Open("Select * from Providers where ID = " & ArID, CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        '    If Not Rs.BOF Then
        '        txtArID.Text = Rs.Fields("ID").Value
        '        Dim Provider As String = ""
        '        If Rs.Fields("IsIndividual").Value = 0 Then
        '            Provider = Rs.Fields("LastName_BSN").Value & ", " & GetAddress(Rs.Fields("Address_ID").Value)
        '        Else
        '            Provider = Rs.Fields("LastName_BSN").Value & ", " & Rs.Fields("FirstName").Value _
        '            & IIf(Rs.Fields("Degree").Value Is System.DBNull.Value, " MD", " " & Rs.Fields("Degree").Value) _
        '            & ", " & GetAddress(Rs.Fields("Address_ID").Value)
        '        End If
        '        txtARName.Text = Provider
        '    Else
        '        MsgBox("Invalid input", MsgBoxStyle.Critical, "Prolis")
        '        txtArID.Text = "" : txtARName.Text = ""
        '    End If
        '    Rs.Close()
        'ElseIf cmbARType.SelectedIndex = 1 Then     'Third Party
        '    Rs.Open("Select * from Payers where ID = " & ArID, CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        '    If Not Rs.BOF Then
        '        txtArID.Text = Rs.Fields("ID").Value
        '        txtARName.Text = Rs.Fields("PayerName").Value & ", " & GetAddress(Rs.Fields("Address_ID").Value)
        '    Else
        '        MsgBox("Invalid input", MsgBoxStyle.Critical, "Prolis")
        '        txtArID.Text = "" : txtARName.Text = ""
        '    End If
        '    Rs.Close()
        'Else    'Patient
        '    Rs.Open("Select * from Patients where ID = " & ArID, CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        '    If Not Rs.BOF Then
        '        txtArID.Text = Rs.Fields("ID").Value
        '        txtARName.Text = Rs.Fields("LastName").Value & ", " & Rs.Fields("FirstName").Value
        '        If Rs.Fields("Address_ID").Value IsNot System.DBNull.Value Then _
        '        txtARName.Text += ", " & GetAddress(Rs.Fields("Address_ID").Value)
        '    Else
        '        MsgBox("Invalid input", MsgBoxStyle.Critical, "Prolis")
        '        txtArID.Text = "" : txtARName.Text = ""
        '    End If
        '    Rs.Close()
        'End If
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
        ''If chkEditNew.Checked = False Then txtID.Text = GetNextPaymentID().ToString
    End Sub

    Private Sub DisplayARName(ByVal ArID As Long)
        txtARName.Text = ""
        txtArID.Text = ""

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = ""
            Select Case cmbARType.SelectedIndex
                Case 0 ' Client
                    query = "SELECT ID, LastName_BSN, FirstName, Degree, IsIndividual, Address_ID FROM Providers WHERE ID = @ArID"
                Case 1 ' Third Party
                    query = "SELECT ID, PayerName, Address_ID FROM Payers WHERE ID = @ArID"
                Case Else ' Patient
                    query = "SELECT ID, LastName, FirstName, Address_ID FROM Patients WHERE ID = @ArID"
            End Select

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@ArID", ArID)

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        txtArID.Text = reader("ID").ToString()

                        Select Case cmbARType.SelectedIndex
                            Case 0 ' Client
                                txtARName.Text = reader("LastName_BSN").ToString()
                                If Convert.ToBoolean(reader("IsIndividual")) Then
                                    txtARName.Text &= ", " & reader("FirstName").ToString() _
                                    & If(IsDBNull(reader("Degree")), " MD", " " & reader("Degree").ToString())
                                End If
                                txtARName.Text &= ", " & GetAddress(reader("Address_ID"))

                            Case 1 ' Third Party
                                txtARName.Text = reader("PayerName").ToString() & ", " & GetAddress(reader("Address_ID"))

                            Case Else ' Patient
                                txtARName.Text = reader("LastName").ToString() & ", " & reader("FirstName").ToString()
                                If Not IsDBNull(reader("Address_ID")) Then
                                    txtARName.Text &= ", " & GetAddress(reader("Address_ID"))
                                End If
                        End Select
                    Else
                        MsgBox("Invalid input", MsgBoxStyle.Critical, "Prolis")
                        txtArID.Text = "" : txtARName.Text = ""
                    End If
                End Using
            End Using
        End Using
    End Sub
    Private Sub DisplayPaymentDetail0(ByVal PmtID As Long, ByVal InvID As Long)
        'dgvPayment.Rows.Clear()
        'Dim IApp As Double = 0
        'Dim Billed As Double = 0
        'Dim OrigBal As Double = 0
        'Dim WO As Double = 0
        'Dim InvWO As Double = 0
        'Dim TstCPT As String = ""
        'Dim Paid As Double = 0
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connString)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select a.*,b.WORB,b.Balance, b.AppliedAmount, b.WrittenOff from Charge_Detail a " &
        '"left outer join Payment_Detail b on a.TGP_ID = b.TGP_ID and a.Charge_ID = " &
        '"b.Charge_ID where a.Charge_ID = " & InvID & " and b.Payment_ID = " & PmtID, CNP,
        'ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then
        '    Do Until Rs.EOF
        '        If Rs.Fields("AppliedAmount").Value Is System.DBNull.Value Then
        '            Paid = 0
        '        Else
        '            Paid = Math.Round(Rs.Fields("AppliedAmount").Value, 2)
        '        End If
        '        If Rs.Fields("WrittenOff").Value Is System.DBNull.Value Then
        '            WO = 0
        '        Else
        '            WO = Math.Round(Rs.Fields("WrittenOff").Value, 2)
        '        End If
        '        InvWO += WO
        '        Dim PR = 0.0


        '        'Paid = GetPaidAmount(Rs.Fields("Charge_ID").Value, Rs.Fields("TGP_ID").Value)
        '        TstCPT = GetTstCPT(Rs.Fields("TGP_ID").Value)
        '        OrigBal = GetOriginalBal(InvID, Rs.Fields("TGP_ID").Value, PmtID)
        '        If Rs.Fields("Balance").Value Is DBNull.Value Then
        '            PR = 0.0
        '        Else
        '            PR = Val(Rs.Fields("Balance").Value)
        '        End If


        '        WO = OrigBal - (Paid + PR)
        '        dgvPayment.Rows.Add(Rs.Fields("Charge_ID").Value,
        '        Rs.Fields("TGP_ID").Value, TstCPT, Format(OrigBal, "#0.00"),
        '        Format(Paid, "0.00"), Format(WO, "0.00"), Format(OrigBal - (Paid + WO),
        '        "#0.00"), Convert.ToBoolean(Rs.Fields("WORB").Value), "", "Process", IIf(PR > 0, PR, ""))  'Temur here i added , Convert.ToBoolean(Rs.Fields("WORB").Value),  Instead of,False,
        '        '
        '        Billed += Math.Round(OrigBal, 2)
        '        IApp += Math.Round(Paid, 2)
        '        Rs.MoveNext()
        '    Loop
        'End If
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
        'txtInvApplied.Text = Format(IApp, "#0.00")
        'txtInvWO.Text = Format(InvWO, "#0.00")
        'txtInvUnApplied.Text = "0.00"
        'btnAutoApply.Enabled = False
        'txtBilled.Text = Format(Billed, "#0.00")
        'txtInvBal.Text = Format(Billed - (IApp + InvWO), "#0.00")
        'If IApp > 0 Then btnUnApply.Enabled = True
        'ReDim BADACCTGP(1, 0) : ReDim BADINV(0)
    End Sub
    Private Sub DisplayPaymentDetail(ByVal PmtID As Long, ByVal InvID As Long)
        dgvPayment.Rows.Clear()

        Dim IApp As Double = 0
        Dim Billed As Double = 0
        Dim OrigBal As Double = 0
        Dim InvWO As Double = 0

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "
            SELECT a.*, b.WORB, b.Balance, b.AppliedAmount, b.WrittenOff 
            FROM Charge_Detail a 
            LEFT JOIN Payment_Detail b ON a.TGP_ID = b.TGP_ID AND a.Charge_ID = b.Charge_ID 
            WHERE a.Charge_ID = @InvID AND b.Payment_ID = @PmtID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@InvID", InvID)
                command.Parameters.AddWithValue("@PmtID", PmtID)

                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim Paid As Double = If(IsDBNull(reader("AppliedAmount")), 0, Math.Round(Convert.ToDouble(reader("AppliedAmount")), 2))
                        Dim WO As Double = If(IsDBNull(reader("WrittenOff")), 0, Math.Round(Convert.ToDouble(reader("WrittenOff")), 2))
                        InvWO += WO

                        Dim TstCPT As String = GetTstCPT(reader("TGP_ID"))
                        OrigBal = GetOriginalBal(InvID, reader("TGP_ID"), PmtID)
                        Dim PR As Double = If(IsDBNull(reader("Balance")), 0, Convert.ToDouble(reader("Balance")))

                        WO = OrigBal - (Paid + PR)

                        dgvPayment.Rows.Add(
                        reader("Charge_ID"), reader("TGP_ID"), TstCPT,
                        Format(OrigBal, "#0.00"), Format(Paid, "0.00"),
                        Format(WO, "0.00"), Format(OrigBal - (Paid + WO), "#0.00"),
                        Convert.ToBoolean(reader("WORB")), "", "Process",
                        If(PR > 0, PR, "")
                    )

                        Billed += Math.Round(OrigBal, 2)
                        IApp += Math.Round(Paid, 2)
                    End While
                End Using
            End Using
        End Using

        ' Update UI elements
        txtInvApplied.Text = Format(IApp, "#0.00")
        txtInvWO.Text = Format(InvWO, "#0.00")
        txtInvUnApplied.Text = "0.00"
        txtBilled.Text = Format(Billed, "#0.00")
        txtInvBal.Text = Format(Billed - (IApp + InvWO), "#0.00")
        btnAutoApply.Enabled = False
        If IApp > 0 Then btnUnApply.Enabled = True
        ReDim BADACCTGP(1, 0) : ReDim BADINV(0)
    End Sub
    Private Function GetOriginalBal0(ByVal ChargeID As Long, ByVal TGPID As Integer, ByVal PMNTID As Long) As Double
        'Dim OrigBal As Double = 0
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connString)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select a.Extend, (Select sum(b.AppliedAmount) + sum(b.Writtenoff) from " &
        '"Payment_Detail b where b.Charge_ID = a.Charge_ID and b.TGP_ID = a.tgp_id " &
        '"and b.Payment_ID < " & PMNTID & ") as PMNTWO from Charge_Detail a where " &
        '"a.Charge_ID = " & ChargeID & " and a.TGP_ID = " & TGPID, CNP,
        'ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then
        '    If Rs.Fields("PMNTWO").Value IsNot System.DBNull.Value Then
        '        OrigBal = Math.Round(Rs.Fields("Extend").Value _
        '        - Rs.Fields("PMNTWO").Value, 2)
        '    Else
        '        OrigBal = Math.Round(Rs.Fields("Extend").Value, 2)
        '    End If
        'End If
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
        'Return OrigBal
    End Function
    Private Function GetOriginalBal(ByVal ChargeID As Long, ByVal TGPID As Integer, ByVal PMNTID As Long) As Double
        Dim OrigBal As Double = 0

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "
            SELECT a.Extend, 
                   (SELECT COALESCE(SUM(b.AppliedAmount) + SUM(b.WrittenOff), 0) 
                    FROM Payment_Detail b 
                    WHERE b.Charge_ID = a.Charge_ID AND b.TGP_ID = a.TGP_ID AND b.Payment_ID < @PMNTID) AS PMNTWO
            FROM Charge_Detail a 
            WHERE a.Charge_ID = @ChargeID AND a.TGP_ID = @TGPID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@ChargeID", ChargeID)
                command.Parameters.AddWithValue("@TGPID", TGPID)
                command.Parameters.AddWithValue("@PMNTID", PMNTID)

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        Dim ExtendValue As Double = Convert.ToDouble(reader("Extend"))
                        Dim PMNTWO As Double = If(IsDBNull(reader("PMNTWO")), 0, Convert.ToDouble(reader("PMNTWO")))

                        OrigBal = Math.Round(ExtendValue - PMNTWO, 2)
                    End If
                End Using
            End Using
        End Using

        Return OrigBal
    End Function
    Private Sub DisplayChargeBalance0(ByVal ArType As Integer, ByVal InvID As Long)
        'dgvPayment.Rows.Clear()
        'Dim Billed As Double = 0
        'Dim OrigBal As Double = 0
        'Dim Paid As Double = 0
        'Dim WO As Double = 0
        'Dim IApp As Double = 0
        'Dim InvWO As Double = 0
        'Dim TstCPT As String = ""
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connString)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select a.*, b.AppliedAmount, b.WrittenOff from Charge_Detail a left outer join Payment_Detail b " &
        '"on a.TGP_ID = b.TGP_ID and a.Charge_ID = b.Charge_ID where a.Charge_ID in (Select ID from Charges where " &
        '"ArType = " & ArType & " and ID = " & InvID & ") order by a.Ordinal", CNP, ADODB.CursorTypeEnum.adOpenStatic,
        'ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then
        '    Do Until Rs.EOF
        '        If Rs.Fields("AppliedAmount").Value Is System.DBNull.Value Then
        '            Paid = 0
        '        Else
        '            Paid = Math.Round(Rs.Fields("AppliedAmount").Value, 2)
        '        End If
        '        If Rs.Fields("WrittenOff").Value Is System.DBNull.Value Then
        '            WO = 0
        '        Else
        '            WO = Math.Round(Rs.Fields("WrittenOff").Value, 2)
        '        End If
        '        InvWO += WO
        '        'Paid = GetPaidAmount(Rs.Fields("Charge_ID").Value, Rs.Fields("TGP_ID").Value)
        '        TstCPT = GetTstCPT(Rs.Fields("TGP_ID").Value)
        '        OrigBal = GetOriginalBal(InvID, Rs.Fields("TGP_ID").Value, Val(txtID.Text))
        '        dgvPayment.Rows.Add(Rs.Fields("Charge_ID").Value,
        '        Rs.Fields("TGP_ID").Value, TstCPT, Format(OrigBal, "#0.00"),
        '        "", "", "", False, "", "Process")
        '        '
        '        If Val(dgvPayment.Rows(dgvPayment.RowCount - 1).Cells(3).Value) = 0 Then
        '            dgvPayment.Rows(dgvPayment.RowCount - 1).Cells(4).ReadOnly = True
        '            dgvPayment.Rows(dgvPayment.RowCount - 1).Cells(8).ReadOnly = True
        '        Else
        '            dgvPayment.Rows(dgvPayment.RowCount - 1).Cells(4).ReadOnly = False
        '            dgvPayment.Rows(dgvPayment.RowCount - 1).Cells(8).ReadOnly = False
        '        End If
        '        '
        '        Billed += Math.Round(OrigBal, 2)
        '        IApp += Math.Round(Paid, 2)
        '        Rs.MoveNext()
        '    Loop
        'End If
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
        'txtInvApplied.Text = ""
        'txtInvWO.Text = ""
        'txtInvUnApplied.Text = ""
        'btnAutoApply.Enabled = False
        'txtBilled.Text = Format(Billed, "#0.00")
        'txtInvBal.Text = ""
        ''If IApp > 0 Then btnUnApply.Enabled = True
    End Sub

    Private Sub DisplayChargeBalance(ByVal ArType As Integer, ByVal InvID As Long)
        dgvPayment.Rows.Clear()

        Dim Billed As Double = 0
        Dim OrigBal As Double = 0
        Dim Paid As Double = 0
        Dim WO As Double = 0
        Dim IApp As Double = 0
        Dim InvWO As Double = 0
        Dim TstCPT As String = ""

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "
            SELECT a.*, b.AppliedAmount, b.WrittenOff 
            FROM Charge_Detail a 
            LEFT JOIN Payment_Detail b ON a.TGP_ID = b.TGP_ID AND a.Charge_ID = b.Charge_ID 
            WHERE a.Charge_ID IN (SELECT ID FROM Charges WHERE ArType = @ArType AND ID = @InvID)
            ORDER BY a.Ordinal"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@ArType", ArType)
                command.Parameters.AddWithValue("@InvID", InvID)

                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Paid = If(IsDBNull(reader("AppliedAmount")), 0, Math.Round(Convert.ToDouble(reader("AppliedAmount")), 2))
                        WO = If(IsDBNull(reader("WrittenOff")), 0, Math.Round(Convert.ToDouble(reader("WrittenOff")), 2))
                        InvWO += WO

                        TstCPT = GetTstCPT(reader("TGP_ID"))
                        OrigBal = GetOriginalBal(InvID, reader("TGP_ID"), Convert.ToInt64(txtID.Text))

                        dgvPayment.Rows.Add(
                        reader("Charge_ID"), reader("TGP_ID"), TstCPT,
                        Format(OrigBal, "#0.00"), "", "", "",
                        False, "", "Process"
                    )

                        ' Handle ReadOnly conditions
                        Dim lastRowIndex As Integer = dgvPayment.RowCount - 1
                        Dim balance As Double = Val(dgvPayment.Rows(lastRowIndex).Cells(3).Value)

                        dgvPayment.Rows(lastRowIndex).Cells(4).ReadOnly = (balance = 0)
                        dgvPayment.Rows(lastRowIndex).Cells(8).ReadOnly = (balance = 0)

                        Billed += Math.Round(OrigBal, 2)
                        IApp += Math.Round(Paid, 2)
                    End While
                End Using
            End Using
        End Using

        ' Update UI elements
        txtInvApplied.Text = ""
        txtInvWO.Text = ""
        txtInvUnApplied.Text = ""
        btnAutoApply.Enabled = False
        txtBilled.Text = Format(Billed, "#0.00")
        txtInvBal.Text = ""

        ' If IApp > 0 Then btnUnApply.Enabled = True
    End Sub
    Private Sub txtInvUnApplied_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInvUnApplied.GotFocus
        txtInvUnApplied.BackColor = FCOLOR
    End Sub

    Private Sub txtInvUnApplied_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInvUnApplied.Validated
        If txtInvUnApplied.Text <> "" Then
            If Val(txtInvUnApplied.Text) > Val(txtCKUnApplied.Text) Then
                MsgBox("Amount can not be greater than the payment's unapplied amount.", MsgBoxStyle.Critical, "Prolis")
                txtInvUnApplied.Text = Format(Val(txtCKUnApplied.Text) - Val(txtInvApplied.Text), "#0.00")
            Else
                If Val(txtInvUnApplied.Text) > Val(txtBilled.Text) - Val(txtInvApplied.Text) Then
                    MsgBox("Amount can not be Less than the one to apply amount.", MsgBoxStyle.Critical, "Prolis")
                    'btnUnApply_Click(Nothing, Nothing)
                    txtInvUnApplied.Text = Format(Val(txtCKUnApplied.Text) - Val(txtInvApplied.Text),
                    "#0.00")
                Else
                    btnAutoApply.Enabled = True
                End If
            End If
        Else
            'MsgBox("Amount assigned, to an invoice, can not be higher than the Unapllied amount.", _
            'MsgBoxStyle.Critical, "Prolis")
            txtInvUnApplied.Text = Val(txtBilled.Text)
        End If
    End Sub

    Private Sub txtID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtID.Validated
        If txtID.Text <> "" Then
            DisplayPayment(Val(txtID.Text))
        End If
    End Sub

    Private Sub txtArID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtArID.Validated
        If txtArID.Text <> "" Then
            If cmbARType.SelectedIndex <> -1 Then
                DisplayARName(Val(txtArID.Text))
                'txtID.Text = CStr(GetNextPaymentID())
                'txtCKUnApplied.Text = "" : txtCKApplied.Text = ""
                'txtInvUnApplied.Text = "" : txtInvApplied.Text = ""
            End If
        End If
    End Sub

    Private Sub DisplayComments0(ByVal ChargeID As Long)
        'dgvComments.Rows.Clear()
        'txtReason.Text = ""
        'Corrected.Checked = False
        'VoidClaim.Checked = False
        'OrgClaimNumber.Text = ""
        'Dim Dated As String = ""
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connString)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select * from Req_Comments where Accession_ID in (Select " &
        '"Accession_ID from Charges where ID = " & ChargeID & ")", CNP,
        'ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then
        '    Do Until Rs.EOF
        '        If Rs.Fields("Dated").Value IsNot System.DBNull.Value _
        '        AndAlso IsDate(Rs.Fields("Dated").Value) Then
        '            Dated = Format(Rs.Fields("Dated").Value, SystemConfig.DateFormat & " HH:mm")
        '        Else
        '            Dated = ""
        '        End If
        '        dgvComments.Rows.Add(Dated, Trim(Rs.Fields("Associated_To").Value),
        '        Trim(Rs.Fields("Comment").Value), GetUserName(Rs.Fields("User_ID").Value))
        '        If Rs.Fields("User_ID").Value = ThisUser.ID Then
        '            dgvComments.Rows(dgvComments.RowCount - 1).Cells(2).ReadOnly = False
        '            dgvComments.Rows(dgvComments.RowCount - 1).Cells(2).Style.BackColor = Color.White
        '        Else
        '            dgvComments.Rows(dgvComments.RowCount - 1).Cells(2).ReadOnly = True
        '        End If
        '        Rs.MoveNext()
        '    Loop
        'End If
        'dgvComments.Rows.Add()
        'dgvComments.Rows(dgvComments.RowCount - 1).Cells(2).ReadOnly = False
        'dgvComments.Rows(dgvComments.RowCount - 1).Cells(2).Style.BackColor = Color.White
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
    End Sub

    Private Sub DisplayComments(ByVal ChargeID As Long)
        dgvComments.Rows.Clear()
        txtReason.Text = ""
        Corrected.Checked = False
        VoidClaim.Checked = False
        OrgClaimNumber.Text = ""

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "
            SELECT rc.Dated, rc.Associated_To, rc.Comment, rc.User_ID 
            FROM Req_Comments rc 
            WHERE rc.Accession_ID IN (
                SELECT Accession_ID FROM Charges WHERE ID = @ChargeID
            )"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@ChargeID", ChargeID)

                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim Dated As String = If(IsDBNull(reader("Dated")) OrElse Not IsDate(reader("Dated")), "",
                                             Format(reader("Dated"), SystemConfig.DateFormat & " HH:mm"))

                        dgvComments.Rows.Add(Dated, Trim(reader("Associated_To").ToString()),
                                         Trim(reader("Comment").ToString()),
                                         GetUserName(reader("User_ID")))

                        ' Set edit permissions based on the user ID
                        Dim lastRowIndex As Integer = dgvComments.RowCount - 1
                        dgvComments.Rows(lastRowIndex).Cells(2).ReadOnly = (reader("User_ID") <> ThisUser.ID)
                        dgvComments.Rows(lastRowIndex).Cells(2).Style.BackColor = If(reader("User_ID") = ThisUser.ID, Color.White, dgvComments.DefaultCellStyle.BackColor)
                    End While
                End Using
            End Using
        End Using

        ' Add an empty row for new comments
        dgvComments.Rows.Add()
        dgvComments.Rows(dgvComments.RowCount - 1).Cells(2).ReadOnly = False
        dgvComments.Rows(dgvComments.RowCount - 1).Cells(2).Style.BackColor = Color.White
    End Sub
    Private Sub UpdatePComments0()
        'If CommentDirty = True And dgvInvoices.SelectedRows.Count > 0 Then
        '    Dim AccID As Long = GetAccIDFromAR(dgvInvoices.SelectedRows(0).Cells(0).Value)
        '    If AccID > 0 Then
        '        Dim i As Integer
        '        Dim CNP As New ADODB.Connection
        '        CNP.Open(connString)
        '        For i = 0 To dgvComments.RowCount - 1
        '            If Not dgvComments.Rows(i).Cells(2).Value Is Nothing AndAlso
        '            dgvComments.Rows(i).Cells(2).Value.ToString <> "" Then
        '                Dim Rs As New ADODB.Recordset                                                     'new entry
        '                Rs.Open("Select * from Req_Comments where Accession_ID = " &
        '                AccID & " and Serial_ID = " & i, CNP,
        '                ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        '                If Rs.BOF Then Rs.AddNew()
        '                Rs.Fields("Accession_ID").Value = AccID
        '                Rs.Fields("Serial_ID").Value = i
        '                If Rs.Fields("User_ID").Value Is Nothing OrElse
        '                Rs.Fields("User_ID").Value Is System.DBNull.Value Then _
        '                Rs.Fields("User_ID").Value = ThisUser.ID
        '                Rs.Fields("Comment").Value = Trim(dgvComments.Rows(i).Cells(2).Value)
        '                If Rs.Fields("Dated").Value Is System.DBNull.Value Or
        '                Rs.Fields("Dated").Value Is Nothing Then _
        '                Rs.Fields("Dated").Value = Format(Date.Now, SystemConfig.DateFormat & " HH:mm")
        '                Rs.Fields("Associated_To").Value = "P"
        '                Rs.Update()
        '                Rs.Close()
        '                Rs = Nothing
        '            End If
        '        Next
        '        CNP.Close()
        '        CNP = Nothing
        '    End If
        '    CommentDirty = False
        'End If
    End Sub
    Private Sub UpdatePComments()
        If CommentDirty AndAlso dgvInvoices.SelectedRows.Count > 0 Then
            Dim AccID As Long = GetAccIDFromAR(dgvInvoices.SelectedRows(0).Cells(0).Value)

            If AccID > 0 Then
                Using connection As New SqlConnection(connString)
                    connection.Open()

                    For i As Integer = 0 To dgvComments.RowCount - 1
                        Dim commentValue = dgvComments.Rows(i).Cells(2).Value

                        If commentValue IsNot Nothing AndAlso commentValue.ToString() <> "" Then
                            Dim queryCheck As String = "SELECT COUNT(*) FROM Req_Comments WHERE Accession_ID = @AccID AND Serial_ID = @SerialID"

                            Dim recordExists As Boolean
                            Using cmdCheck As New SqlCommand(queryCheck, connection)
                                cmdCheck.Parameters.AddWithValue("@AccID", AccID)
                                cmdCheck.Parameters.AddWithValue("@SerialID", i)
                                recordExists = Convert.ToInt32(cmdCheck.ExecuteScalar()) > 0
                            End Using

                            Dim query As String
                            If recordExists Then
                                query = "UPDATE Req_Comments SET User_ID = @UserID, Comment = @Comment, Dated = @Dated, Associated_To = @AssociatedTo WHERE Accession_ID = @AccID AND Serial_ID = @SerialID"
                            Else
                                query = "INSERT INTO Req_Comments (Accession_ID, Serial_ID, User_ID, Comment, Dated, Associated_To) VALUES (@AccID, @SerialID, @UserID, @Comment, @Dated, @AssociatedTo)"
                            End If

                            Using cmd As New SqlCommand(query, connection)
                                cmd.Parameters.AddWithValue("@AccID", AccID)
                                cmd.Parameters.AddWithValue("@SerialID", i)
                                cmd.Parameters.AddWithValue("@UserID", ThisUser.ID)
                                cmd.Parameters.AddWithValue("@Comment", Trim(commentValue.ToString()))
                                cmd.Parameters.AddWithValue("@Dated", Format(Date.Now, SystemConfig.DateFormat & " HH:mm"))
                                cmd.Parameters.AddWithValue("@AssociatedTo", "P") ' Corrected: Added Associated_To field

                                cmd.ExecuteNonQuery()
                            End Using
                        End If
                    Next
                End Using
            End If

            CommentDirty = False
        End If
    End Sub
    Private Function GetTGPIDbyCPT0(ByVal CPT As String) As Integer
        'Dim TGPID As Integer = -1
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connString)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("select * from Tests where IsActive <> 0 and Tbillable <> 0 and CPT_Code = '" &
        'CPT & "'", CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then
        '    TGPID = Rs.Fields("ID").Value
        'Else
        '    Rs.Close()
        '    Rs.Open("select * from Groups where IsActive <> 0 and Tbillable <> 0 and CPT_Code = '" &
        '    CPT & "'", CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        '    If Not Rs.BOF Then
        '        TGPID = Rs.Fields("ID").Value
        '    Else
        '        Rs.Close()
        '        Rs.Open("select * from Profiles where IsActive <> 0 and Tbillable <> 0 and CPT_Code = '" &
        '        CPT & "'", CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        '        If Not Rs.BOF Then
        '            TGPID = Rs.Fields("ID").Value
        '        End If
        '    End If
        'End If
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
        'Return TGPID
    End Function

    Private Function GetTGPIDbyCPT(ByVal CPT As String) As Integer
        Dim TGPID As Integer = -1
        Dim queries As String() = {
        "SELECT ID FROM Tests WHERE IsActive <> 0 AND Tbillable <> 0 AND CPT_Code = @CPT",
        "SELECT ID FROM Groups WHERE IsActive <> 0 AND Tbillable <> 0 AND CPT_Code = @CPT",
        "SELECT ID FROM Profiles WHERE IsActive <> 0 AND Tbillable <> 0 AND CPT_Code = @CPT"
    }

        Using connection As New SqlConnection(connString)
            connection.Open()

            For Each query In queries
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@CPT", CPT)

                    Dim result As Object = command.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        TGPID = Convert.ToInt32(result)
                        Exit For ' Stop checking once a valid TGPID is found
                    End If
                End Using
            Next
        End Using

        Return TGPID
    End Function
    Private Function GetTGPIDbyName0(ByVal TGPName As String) As Integer
        'Dim TGPID As Integer = -1
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connString)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("select * from Tests where IsActive <> 0 and Tbillable <> 0 and Name Like '%" &
        'TGPName & "%'", CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then
        '    TGPID = Rs.Fields("ID").Value
        'Else
        '    Rs.Close()
        '    Rs.Open("select * from Groups where IsActive <> 0 and Tbillable <> 0 and Name Like '%" &
        '    TGPName & "%'", CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        '    If Not Rs.BOF Then
        '        TGPID = Rs.Fields("ID").Value
        '    Else
        '        Rs.Close()
        '        Rs.Open("select * from Profiles where IsActive <> 0 and Tbillable <> 0 and Name Like '%" &
        '        TGPName & "%'", CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        '        If Not Rs.BOF Then
        '            TGPID = Rs.Fields("ID").Value
        '        End If
        '    End If
        'End If
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
        'Return TGPID
    End Function
    Private Function GetTGPIDbyName(ByVal TGPName As String) As Integer
        Dim TGPID As Integer = -1
        Dim queries As String() = {
        "SELECT ID FROM Tests WHERE IsActive <> 0 AND Tbillable <> 0 AND Name LIKE '%' + @TGPName + '%'",
        "SELECT ID FROM Groups WHERE IsActive <> 0 AND Tbillable <> 0 AND Name LIKE '%' + @TGPName + '%'",
        "SELECT ID FROM Profiles WHERE IsActive <> 0 AND Tbillable <> 0 AND Name LIKE '%' + @TGPName + '%'"
    }

        Using connection As New SqlConnection(connString)
            connection.Open()

            For Each query In queries
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@TGPName", TGPName)

                    Dim result As Object = command.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        TGPID = Convert.ToInt32(result)
                        Exit For ' Stop searching once a valid ID is found
                    End If
                End Using
            Next
        End Using

        Return TGPID
    End Function
    Private Function GetPayerbyName0(ByVal Name As String) As Payer
        'Dim Payer As Payer = Nothing
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connString)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("select * from Payers where Active <> 0 and PayerName Like '%" &
        'Name & "%'", CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then
        '    Payer.ID = Rs.Fields("ID").Value
        '    Payer.Name = Rs.Fields("PayerName").Value
        '    Payer.PayerCode = Rs.Fields("PayerCode").Value
        'Else
        '    Payer.ID = -1
        '    Payer.Name = ""
        '    Payer.PayerCode = ""
        'End If
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
        'Return Payer
    End Function
    Private Function GetPayerbyName(ByVal Name As String) As Payer
        Dim Payer As New Payer()

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT ID, PayerName, PayerCode FROM Payers WHERE Active <> 0 AND PayerName LIKE '%' + @Name + '%'"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Name", Name)

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        Payer.ID = Convert.ToInt32(reader("ID"))
                        Payer.Name = reader("PayerName").ToString()
                        Payer.PayerCode = reader("PayerCode").ToString()
                    Else
                        Payer.ID = -1
                        Payer.Name = ""
                        Payer.PayerCode = ""
                    End If
                End Using
            End Using
        End Using

        Return Payer
    End Function
    Private Function GetPayerbyClaimID0(ByVal Claim As Long) As Payer
        'Dim Payer As Payer = Nothing
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connString)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("select * from Payers where Active <> 0 and ID in " &
        '"(Select Ar_ID from Charges where ArType = 1 and ID = " & Claim & ")", CNP,
        'ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then
        '    Payer.ID = Rs.Fields("ID").Value
        '    Payer.Name = Rs.Fields("PayerName").Value
        '    Payer.PayerCode = Rs.Fields("PayerCode").Value
        'Else
        '    Payer.ID = -1
        '    Payer.Name = ""
        '    Payer.PayerCode = ""
        'End If
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
        'Return Payer
    End Function
    Private Function GetPayerbyClaimID(ByVal Claim As Long) As Payer
        Dim Payer As New Payer()

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "
            SELECT ID, PayerName, PayerCode 
            FROM Payers 
            WHERE Active <> 0 AND ID IN (
                SELECT Ar_ID FROM Charges WHERE ArType = 1 AND ID = @Claim
            )"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Claim", Claim)

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        Payer.ID = Convert.ToInt32(reader("ID"))
                        Payer.Name = reader("PayerName").ToString()
                        Payer.PayerCode = reader("PayerCode").ToString()
                    Else
                        Payer.ID = -1
                        Payer.Name = ""
                        Payer.PayerCode = ""
                    End If
                End Using
            End Using
        End Using

        Return Payer
    End Function
    Private Sub dgvInvoices_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvInvoices.CellClick

        If e.RowIndex <> -1 Then

            Try
                Clipboard.SetText(dgvInvoices.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                clipboardMsg.ForeColor = Color.Red
                clipboardMsg.Text = dgvInvoices.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & ""
            Catch ex As Exception

            End Try
        End If
        If chkEditNew.Checked = False Then  'New
            DisplayChargeBalance(cmbARType.SelectedIndex,
            dgvInvoices.SelectedRows(0).Cells(0).Value)
        Else
            DisplayPaymentDetail(Val(txtID.Text),
            dgvInvoices.SelectedRows(0).Cells(0).Value)
            If Val(txtInvUnApplied.Text) = 0 And dgvPayment.RowCount > 0 Then
                btnSave.Enabled = True
            End If
            btnDelInvPmt.Enabled = True
        End If
        DisplayComments(dgvInvoices.SelectedRows(0).Cells(0).Value)
    End Sub

    Private Sub txtInvID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInvID.Validated
        If Trim(txtInvID.Text) <> "" Then
            Dim fnd As Boolean = False

            For i As Integer = 0 To dgvInvoices.RowCount - 1

                If dgvInvoices.Rows(i).Cells(0).Value = txtInvID.Text Then
                    fnd = True
                    Exit For
                ElseIf dgvInvoices.Rows(i).Cells(0).Value <> txtInvID.Text Then

                End If
            Next
            If fnd = False Then
                newInv.Checked = True
            End If
            DisplayChargeBalance(cmbARType.SelectedIndex, Val(txtInvID.Text))

            If dgvPayment.RowCount = 0 Then
                MsgBox("Either enter a valid invoice ID or use the LookUp button")
                txtInvID.Text = ""
                Return
            End If
            If Not IsNumeric(Trim(txtInvID.Text)) Then
                Return
            End If
            PopulateInvoice(Trim(txtInvID.Text))
        End If
    End Sub

    Private Sub btnInvLookUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInvLookUp.Click
        Dim InvID As String = frmInvoiceLookUp.ShowDialog
        If InvID <> "" Then PopulateInvoice(InvID)
    End Sub

    Private Sub dgvInvoices_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvInvoices.DataError
        On Error Resume Next
    End Sub

    Private Sub dgvPayment_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvPayment.DataError
        On Error Resume Next
    End Sub

    Private Sub dgvComments_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvComments.CellEndEdit
        CommentDirty = True
    End Sub

    Private Sub btnVoidCK_Click0(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVoidCK.Click
        'If txtID.Text <> "" Then
        '    Dim Msg As String = ""
        '    If cmbARType.SelectedIndex = 1 Then
        '        Msg = "This command will void the displayed Cheque, delete payment of all " &
        '        "associated invoices and delete the secondary invoices generated as the " &
        '        "result of this payment process but leaving the payments entered against " &
        '        "the secondary invoice thus causing the system in an out of balance " &
        '        "state. Rather you should use the 'Delete Payment' button instead. Are " &
        '        "you certain ?"
        '    Else
        '        Msg = "This command will void the displayed Cheque, delete payment of all " &
        '        "associated invoices. If you want to delete the payment against any one " &
        '        "invoice, use the 'Delete Payment' button instead. Are you certain ?"
        '    End If


        '    Dim retval As Integer = MsgBox(Msg, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
        '    If retval = vbYes Then
        '        Dim CNP As New ADODB.Connection
        '        CNP.Open(connString)
        '        If cmbARType.SelectedIndex = 1 Then 'Insurance
        '            'delete the seconday charge detail
        '            CNP.Execute("Delete from Charge_Detail where Charge_ID in (Select " &
        '            "ID from Charges where  ArType = " & 2 & " and Accession_ID " &
        '            "in (Select Accession_ID from Charges where ID in (Select distinct " &
        '            "Charge_ID from Payment_Detail where Payment_ID = " & Val(txtID.Text) &
        '            ")))")
        '            'delete the seconday charges
        '            CNP.Execute("Delete from Charges where  ArType = " & 2 & " " &
        '            "and Accession_ID in (Select Accession_ID from Charges where ID in " &
        '            "(Select distinct Charge_ID from Payment_Detail where Payment_ID = " &
        '            Val(txtID.Text) & "))")




        '        ElseIf cmbARType.SelectedIndex = 2 Then 'patient
        '            'delete the seconday charge detail

        '            ' del dependent 
        '            CNP.Execute($"delete from Charge_Detail where Charge_ID In (Select ID from Charges 
        '            where parent_charge_id in (select   ID from Charges where   ArType = " & cmbARType.SelectedIndex & "  And ID In
        '                (Select distinct Charge_ID from Payment_Detail where Payment_ID = " & Val(txtID.Text) & " )))")

        '            'delete the seconday charges
        '            CNP.Execute("Delete from Charges where   ArType = " & cmbARType.SelectedIndex & " " &
        '            "And   parent_charge_id In " &
        '            "(Select distinct Charge_ID from Payment_Detail where Payment_ID = " &
        '            Val(txtID.Text) & ")")

        '            ' then parent

        '            'CNP.Execute("Delete from Charge_Detail where Charge_ID In (Select " &
        '            '"ID from Charges where   ArType = " & cmbARType.SelectedIndex & " And  " &
        '            '" ID In (Select distinct " &
        '            '"Charge_ID from Payment_Detail where Payment_ID = " & Val(txtID.Text) &
        '            '"))")
        '            ''delete the seconday charges
        '            'CNP.Execute("Delete from Charges where   ArType = " & cmbARType.SelectedIndex & " " &
        '            '"And   ID In " &
        '            '"(Select distinct Charge_ID from Payment_Detail where Payment_ID = " &
        '            'Val(txtID.Text) & ")")

        '        End If
        '        'delete the Payment detail
        '        CNP.Execute("Delete from Payment_Detail where Payment_ID = " & Val(txtID.Text))
        '        'delete the EOB_Claim
        '        CNP.Execute("Delete from EOB_Claim where DocNo In (Select DocNo " &
        '        "from Payments where ID = " & Val(txtID.Text) & ")")
        '        'delete the EOBs
        '        CNP.Execute("Delete from EOBs where DocNo In (Select DocNo " &
        '        "from Payments where ID = " & Val(txtID.Text) & ")")
        '        'delete the payment
        '        CNP.Execute("Delete from Payments where ID = " & Val(txtID.Text))
        '        ' CNP.Execute("delete from Payment_Detail where Payment_ID in  (select id from Payments where  DocNo = '" & txtDoc.Text & "')")
        '        '''CNP.''Execute'("delete from Payments where   DocNo = '" & txtDoc.Text & "'")
        '        CNP.Close()
        '        CNP = Nothing
        '        dgvPayment.Rows.Clear()
        '        dgvInvoices.Rows.Clear()
        '        FormClear() : txtID.Text = "" : txtArID.Text = ""
        '        txtARName.Text = "" : txtDoc.Text = "" : txtAmount.Text = ""
        '        txtCKUnApplied.Text = "" : txtCKApplied.Text = ""
        '        txtBilled.Text = "" : txtInvWO.Text = "" : txtInvApplied.Text = ""
        '        txtInvBal.Text = "" : txtInvUnApplied.Text = ""
        '        txtReason.Text = "" : btnSave.Enabled = False
        '        btnDelInvPmt.Enabled = False : btnAutoApply.Enabled = False
        '        btnUnApply.Enabled = False : btnVoidCK.Enabled = False
        '    End If
        'End If
    End Sub

    Private Sub btnVoidCK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVoidCK.Click
        If txtID.Text <> "" Then

            Dim Msg As String = ""
            If cmbARType.SelectedIndex = 1 Then
                Msg = "This command will void the displayed Cheque, delete payment of all " &
                "associated invoices and delete the secondary invoices generated as the " &
                "result of this payment process but leaving the payments entered against " &
                "the secondary invoice thus causing the system in an out of balance " &
                "state. Rather you should use the 'Delete Payment' button instead. Are " &
                "you certain ?"
            Else
                Msg = "This command will void the displayed Cheque, delete payment of all " &
                "associated invoices. If you want to delete the payment against any one " &
                "invoice, use the 'Delete Payment' button instead. Are you certain ?"
            End If

            Dim retval As Integer = MsgBox(Msg, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
            If retval = vbYes Then
                Using connection As New SqlConnection(connString)
                    connection.Open()

                    Dim queries As New List(Of String)

                    If cmbARType.SelectedIndex = 1 Then ' Insurance
                        queries.Add("
                        DELETE FROM Charge_Detail WHERE Charge_ID IN 
                        (SELECT ID FROM Charges WHERE ArType = 2 AND Accession_ID IN 
                        (SELECT Accession_ID FROM Charges WHERE ID IN 
                        (SELECT DISTINCT Charge_ID FROM Payment_Detail WHERE Payment_ID = @PaymentID)))")

                        queries.Add("
                        DELETE FROM Charges WHERE ArType = 2 AND Accession_ID IN 
                        (SELECT Accession_ID FROM Charges WHERE ID IN 
                        (SELECT DISTINCT Charge_ID FROM Payment_Detail WHERE Payment_ID = @PaymentID))")

                    ElseIf cmbARType.SelectedIndex = 2 Then ' Patient
                        'delete the seconday charge detail

                        ' del dependent 
                        queries.Add("
                        DELETE FROM Charge_Detail WHERE Charge_ID IN 
                        (SELECT ID FROM Charges WHERE parent_charge_id IN 
                        (SELECT ID FROM Charges WHERE ArType = @ArType AND ID IN 
                        (SELECT DISTINCT Charge_ID FROM Payment_Detail WHERE Payment_ID = @PaymentID)))")

                        'delete the seconday charges
                        queries.Add("
                        DELETE FROM Charges WHERE ArType = @ArType AND parent_charge_id IN 
                        (SELECT DISTINCT Charge_ID FROM Payment_Detail WHERE Payment_ID = @PaymentID)")
                    End If

                    'delete the Payment detail
                    queries.Add("DELETE FROM Payment_Detail WHERE Payment_ID = @PaymentID")
                    'delete the EOB_Claim
                    queries.Add("DELETE FROM EOB_Claim WHERE DocNo IN (SELECT DocNo FROM Payments WHERE ID = @PaymentID)")
                    'delete the EOBs
                    queries.Add("DELETE FROM EOBs WHERE DocNo IN (SELECT DocNo FROM Payments WHERE ID = @PaymentID)")
                    'delete the payment
                    queries.Add("DELETE FROM Payments WHERE ID = @PaymentID")

                    For Each query In queries
                        Using command As New SqlCommand(query, connection)
                            command.Parameters.AddWithValue("@PaymentID", Val(txtID.Text))
                            command.Parameters.AddWithValue("@ArType", cmbARType.SelectedIndex)
                            command.ExecuteNonQuery()
                        End Using
                    Next
                End Using

                dgvPayment.Rows.Clear()
                dgvInvoices.Rows.Clear()
                FormClear()
                txtID.Text = "" : txtArID.Text = "" : txtARName.Text = ""
                txtDoc.Text = "" : txtAmount.Text = "" : txtCKUnApplied.Text = "" : txtCKApplied.Text = ""
                txtBilled.Text = "" : txtInvWO.Text = "" : txtInvApplied.Text = "" : txtInvBal.Text = "" : txtInvUnApplied.Text = "" : txtReason.Text = ""
                btnSave.Enabled = False : btnDelInvPmt.Enabled = False : btnAutoApply.Enabled = False : btnUnApply.Enabled = False : btnVoidCK.Enabled = False
            End If
        End If
    End Sub
    Private Sub btnDelInvPmt_Click0(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelInvPmt.Click
        'If txtID.Text <> "" And dgvInvoices.SelectedRows.Count > 0 Then
        '    Dim Msg As String = ""
        '    If cmbARType.SelectedIndex = 1 Then 'Insurance
        '        Msg = "This command will delete the payment against the selected invoice " &
        '        "And delete the secondary invoices generated As the result Of this " &
        '        "payment process. Are you certain ?"
        '    Else
        '        Msg = "This command will delete the payment against the selected " &
        '        "invoice . Are you certain ?"
        '    End If
        '    Dim retval As Integer = MsgBox(Msg, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
        '    If retval = vbYes Then
        '        Dim CNP As New ADODB.Connection
        '        CNP.Open(connString)
        '        If cmbARType.SelectedIndex = 1 Then 'Insurance
        '            'delete the seconday charge detail
        '            CNP.Execute("Delete from Charge_Detail where Charge_ID In (Select " &
        '            "ID from Charges where IsPrimary = 0 And ArType = 2 And Accession_ID " &
        '            "In (Select Accession_ID from Charges where ID In (Select distinct " &
        '            "Charge_ID from Payment_Detail where Payment_ID = " & Val(txtID.Text) &
        '            " And Charge_ID = " & dgvInvoices.SelectedRows(0).Cells(0).Value & ")))")
        '            'delete the seconday charges
        '            CNP.Execute("Delete from Charges where IsPrimary = 0 And ArType = 2 " &
        '            "And Accession_ID In (Select Accession_ID from Charges where ID In " &
        '            "(Select distinct Charge_ID from Payment_Detail where Payment_ID = " &
        '            Val(txtID.Text) & " And Charge_ID = " &
        '            dgvInvoices.SelectedRows(0).Cells(0).Value & "))")
        '            '
        '            CNP.Execute("Delete from Charge_Detail where Charge_ID In (Select " &
        '             "ID from Charges where IsPrimary = 0 And ArType = 2 And Accession_ID " &
        '             "In (Select Accession_ID from Charges where ID = " &
        '             dgvInvoices.SelectedRows(0).Cells(0).Value & "))")
        '            '
        '            CNP.Execute("Delete from Charges where IsPrimary = 0 And ArType = 2 And " &
        '            "Accession_ID In (Select Accession_ID from Charges where ID = " &
        '            dgvInvoices.SelectedRows(0).Cells(0).Value & ")")
        '        End If
        '        '
        '        Dim InvPmt As Double = GetInvoicePayment(Val(txtID.Text),
        '        dgvInvoices.SelectedRows(0).Cells(0).Value)
        '        CNP.Execute("Update Payments Set UnApplied = UnApplied + " & InvPmt &
        '        "where ID = " & Val(txtID.Text))
        '        'delete the Payment detail
        '        CNP.Execute("Delete from Payment_Detail where Payment_ID = " &
        '        Val(txtID.Text) & "  And Charge_ID = " &
        '        dgvInvoices.SelectedRows(0).Cells(0).Value)
        '        'delete the EOB_Claim
        '        CNP.Execute("Delete from EOB_Claim where DocNo In (Select DocNo " &
        '        "from Payments where ID = " & Val(txtID.Text) & ") And Charge_ID = " &
        '        dgvInvoices.SelectedRows(0).Cells(0).Value)
        '        '  
        '        CNP.Close()
        '        CNP = Nothing
        '        dgvPayment.Rows.Clear()
        '        dgvInvoices.Rows.RemoveAt(dgvInvoices.SelectedRows(0).Index)
        '        txtCKUnApplied.Text = Format(Val(txtCKUnApplied.Text) + InvPmt, "0.00")
        '        txtCKApplied.Text = Format(Val(txtCKApplied.Text) - InvPmt, "0.00")
        '        txtBilled.Text = "" : txtInvWO.Text = "" : txtInvApplied.Text = ""
        '        txtInvBal.Text = "" : txtInvUnApplied.Text = ""
        '        txtReason.Text = "" : btnSave.Enabled = False
        '        btnDelInvPmt.Enabled = False : btnAutoApply.Enabled = False
        '        btnUnApply.Enabled = False
        '    End If
        'End If
    End Sub

    Private Sub btnDelInvPmt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelInvPmt.Click
        If txtID.Text <> "" AndAlso dgvInvoices.SelectedRows.Count > 0 Then
            Dim Msg As String = ""
            If cmbARType.SelectedIndex = 1 Then 'Insurance
                Msg = "This command will delete the payment against the selected invoice " &
                "And delete the secondary invoices generated As the result Of this " &
                "payment process. Are you certain ?"
            Else
                Msg = "This command will delete the payment against the selected " &
                "invoice . Are you certain ?"
            End If

            Dim InvPmt As Double = 0
            Dim retval As Integer = MsgBox(Msg, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
            If retval = vbYes Then
                Using connection As New SqlConnection(connString)
                    connection.Open()

                    Dim selectedChargeID As Long = dgvInvoices.SelectedRows(0).Cells(0).Value
                    Dim queries As New List(Of String)

                    If cmbARType.SelectedIndex = 1 Then ' Insurance
                        'delete the seconday charge detail
                        queries.Add("
                        DELETE FROM Charge_Detail WHERE Charge_ID IN 
                        (SELECT ID FROM Charges WHERE IsPrimary = 0 AND ArType = 2 AND Accession_ID IN 
                        (SELECT Accession_ID FROM Charges WHERE ID IN 
                        (SELECT DISTINCT Charge_ID FROM Payment_Detail WHERE Payment_ID = @PaymentID AND Charge_ID = @ChargeID)))")

                        'delete the seconday charges
                        queries.Add("
                        DELETE FROM Charges WHERE IsPrimary = 0 AND ArType = 2 AND Accession_ID IN 
                        (SELECT Accession_ID FROM Charges WHERE ID IN 
                        (SELECT DISTINCT Charge_ID FROM Payment_Detail WHERE Payment_ID = @PaymentID AND Charge_ID = @ChargeID))")

                        queries.Add("
                        DELETE FROM Charge_Detail WHERE Charge_ID IN 
                        (SELECT ID FROM Charges WHERE IsPrimary = 0 AND ArType = 2 AND Accession_ID IN 
                        (SELECT Accession_ID FROM Charges WHERE ID = @ChargeID))")

                        queries.Add("
                        DELETE FROM Charges WHERE IsPrimary = 0 AND ArType = 2 AND Accession_ID IN 
                        (SELECT Accession_ID FROM Charges WHERE ID = @ChargeID)")
                    End If

                    InvPmt = GetInvoicePayment(Val(txtID.Text), selectedChargeID)

                    queries.Add("UPDATE Payments SET UnApplied = UnApplied + @InvPmt WHERE ID = @PaymentID")
                    queries.Add("DELETE FROM Payment_Detail WHERE Payment_ID = @PaymentID AND Charge_ID = @ChargeID")
                    queries.Add("DELETE FROM EOB_Claim WHERE DocNo IN (SELECT DocNo FROM Payments WHERE ID = @PaymentID) AND Charge_ID = @ChargeID")

                    For Each query In queries
                        Using command As New SqlCommand(query, connection)
                            command.Parameters.AddWithValue("@PaymentID", Val(txtID.Text))
                            command.Parameters.AddWithValue("@ChargeID", selectedChargeID)
                            command.Parameters.AddWithValue("@InvPmt", InvPmt)
                            command.ExecuteNonQuery()
                        End Using
                    Next
                End Using

                dgvPayment.Rows.Clear()
                dgvInvoices.Rows.RemoveAt(dgvInvoices.SelectedRows(0).Index)
                txtCKUnApplied.Text = Format(Val(txtCKUnApplied.Text) + InvPmt, "0.00")
                txtCKApplied.Text = Format(Val(txtCKApplied.Text) - InvPmt, "0.00")
                txtBilled.Text = "" : txtInvWO.Text = "" : txtInvApplied.Text = "" : txtInvBal.Text = "" : txtInvUnApplied.Text = "" : txtReason.Text = ""
                btnSave.Enabled = False : btnDelInvPmt.Enabled = False : btnAutoApply.Enabled = False : btnUnApply.Enabled = False
            End If
        End If
    End Sub

    Private Function GetInvoicePayment0(ByVal PmntID As Long, ByVal ChargeID As Long) As Double
        'Dim Pmt As Double = 0
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connString)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select AppliedAmount from Payment_Detail where Charge_ID = " & ChargeID &
        '" And Payment_ID = " & PmntID, CNP, ADODB.CursorTypeEnum.adOpenStatic,
        'ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then
        '    Do Until Rs.EOF
        '        Pmt += Rs.Fields("AppliedAmount").Value
        '        Rs.MoveNext()
        '    Loop
        'End If
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
        'Return Math.Round(Pmt, 2)
    End Function
    Private Function GetInvoicePayment(ByVal PmntID As Long, ByVal ChargeID As Long) As Double
        Dim Pmt As Double = 0

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT COALESCE(SUM(AppliedAmount), 0) FROM Payment_Detail WHERE Charge_ID = @ChargeID AND Payment_ID = @PmntID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@ChargeID", ChargeID)
                command.Parameters.AddWithValue("@PmntID", PmntID)

                Dim result As Object = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    Pmt = Convert.ToDouble(result)
                End If
            End Using
        End Using

        Return Math.Round(Pmt, 2)
    End Function
    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        txtDoc.Text = Clipboard.GetText()
    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click
        txtInvID.Text = Clipboard.GetText()
    End Sub

    Private Sub dgvPayment_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPayment.CellClick
        If e.RowIndex <> -1 Then
            If e.ColumnIndex = 7 Then
                'If dgvPayment.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = False Then
                '    If txtReason.Text.Contains("Patient Responsibility") Then
                '    Else
                '        txtReason.Text = txtReason.Text + " Patient Responsibility"
                '    End If

                'Else
                '    txtReason.Text.Replace("Patient Responsibility", "")
                'End If


            End If
            Try
                Clipboard.SetText(dgvPayment.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                clipboardMsg.ForeColor = Color.Red
                clipboardMsg.Text = dgvPayment.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & ""
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub Corrected_CheckedChanged(sender As Object, e As EventArgs) Handles Corrected.CheckedChanged
        If Corrected.Checked Then
            OrgClaimNumber.Focus()
            If OrgClaimNumber.Text = "" Then
                OrgClaimNumber.Text = "claim number"

            End If
            VoidClaim.Checked = False
            'txtReason.Text = "CORRECTED REF="
        End If
    End Sub

    Private Sub VoidClaim_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub OrgClaimNumber_TextChanged(sender As Object, e As EventArgs)
        'txtReason.Text = "CORRECTED REF=" & OrgClaimNumber.Text

    End Sub

    Private Sub dgvPayment_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPayment.CellValueChanged
        Corrected.Checked = False
        VoidClaim.Checked = False
        txtReason.Text = ""
    End Sub

    Private Sub ViewEra_Click(sender As Object, e As EventArgs) Handles ViewEra.Click
        Dim contents = CommonData.RetrieveColumnValue("EOBS", "EOB_Contents", "DocNo", "'" & txtDoc.Text & "'", "")

        frmHTML_VIEW.Close()
        If contents Is System.DBNull.Value Then
            Return
        End If


        Try

        Catch ex As Exception

        End Try
        frmHTML_VIEW.ERA_path_or_content = contents
        frmHTML_VIEW.Show()
    End Sub
    Public Function CreateERAFolders() As (ERAsFolder As String, UnprocessedFolder As String, ProcessedFolder As String)
        ' Get the path to the current user's Documents folder
        Dim documentsPath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

        ' Define full folder paths
        Dim erasFolderPath As String = Path.Combine(documentsPath, "ERAs")
        Dim unprocessedPath As String = Path.Combine(erasFolderPath, "Unprocessed")
        Dim processedPath As String = Path.Combine(erasFolderPath, "Processed")

        ' Create folders if they do not exist
        If Not Directory.Exists(erasFolderPath) Then
            Directory.CreateDirectory(erasFolderPath)
        End If

        If Not Directory.Exists(unprocessedPath) Then
            Directory.CreateDirectory(unprocessedPath)
        End If

        If Not Directory.Exists(processedPath) Then
            Directory.CreateDirectory(processedPath)
        End If

        ' Return the paths as a tuple
        Return (erasFolderPath, unprocessedPath, processedPath)
    End Function
    Private Sub AttachEOB_Click(sender As Object, e As EventArgs) Handles AttachEOB.Click
        If AttachEOB.Text = "Attach EOB" Then
            deleteeob.Hide()
            Dim paths = CreateERAFolders()
            ' Set the initial directory and enable multiselect
            OpenFileDialog1.InitialDirectory = paths.UnprocessedFolder
            OpenFileDialog1.Multiselect = True ' Allow selecting multiple files

            ' Show the dialog and check if the user pressed OK
            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                ' Combine selected file paths into a single string
                eobPath.Text = String.Join("; ", OpenFileDialog1.FileNames)
            Else
                eobPath.Text = ""
            End If


            If eobPath.Text <> "" And txtDoc.Text <> "" Then
                For Each path1ass In eobPath.Text.Split(";")
                    Dim fileBytes As Byte() = File.ReadAllBytes(path1ass.Trim())
                    Dim fname = Path.GetFileName(path1ass.Trim())
                    ' Insert or update the byte array into the database
                    Using connection As New SqlConnection(connString)
                        connection.Open()

                        ' SQL query to insert the binary data
                        Dim query As String = "insert into EOB_Attachments(DocName,DocNo,Attachment_Contents)  values(@Name,@Doc, @FileData )"


                        Using command As New SqlCommand(query, connection)
                            ' Add the byte array as a parameter
                            command.Parameters.Add("@FileData", SqlDbType.VarBinary).Value = fileBytes
                            command.Parameters.Add("@Doc", SqlDbType.NVarChar).Value = txtDoc.Text
                            command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = fname

                            ' Execute the query
                            command.ExecuteNonQuery()
                        End Using

                        'query = "update payments set EOBPath=@FileData where DocNo=@Doc"

                        'Using command As New SqlCommand(query, connection)
                        '    ' Add the byte array as a parameter
                        '    command.Parameters.Add("@FileData", SqlDbType.VarBinary).Value = fileBytes
                        '    command.Parameters.Add("@Doc", SqlDbType.NVarChar).Value = txtDoc.Text

                        '    ' Execute the query
                        '    command.ExecuteNonQuery()
                        'End Using
                    End Using
                Next

                AttachEOB.Text = "Viwe EOB"
                deleteeob.Show()
            End If
        ElseIf AttachEOB.Text = "Viwe EOB" Then
            deleteeob.Show()
            Dim contents = CommonData.RetrieveColumnValueList("EOB_Attachments", "Attachment_Contents", "DocNo", "'" & txtDoc.Text & "'", "ALL")

            If contents Is Nothing Then
                contents = CommonData.RetrieveColumnValue("Payments", "EOBPath", "DocNo", "'" & txtDoc.Text & "'", "")
                If contents Is Nothing Then
                    contents = CommonData.RetrieveColumnValue("EOBS", "EOBPath", "DocNo", "'" & txtDoc.Text & "'", "")

                End If

            End If
            If contents Is DBNull.Value Then
                Return
            End If


            ' Combine the arrays
            Dim combinedBytes As Byte() = Nothing
            Dim Attachments As List(Of Attachments) = New List(Of Attachments)()
            ' combinedBytes now holds the merged content

            For Each cntents In contents

                Dim attcntents = cntents("Attachment_Contents")
                Dim name = cntents("DocName")
                Dim ID As Long = cntents("ID")
                If name Is DBNull.Value Then
                    name = "Attachment"
                End If
                Dim Attachment As Attachments = New Attachments()
                Attachment.ID = ID
                Attachment.attName = name
                Attachment.combinedBytes = attcntents

                Attachments.Add(Attachment)
                Dim fileBytes As Byte() = CType(attcntents, Byte())
                combinedBytes = fileBytes.Concat(fileBytes).ToArray()

            Next
            Dim myForm As New frmEob_Attachments()

            For Each Attachment As Attachments In Attachments
                myForm.DataGridView1.Rows.Add(Attachment.attName, Nothing, Nothing, Attachment.ID)
            Next
            'myForm.TopLevel = True ' Ensure it is a top-level form
            Dim ref = myForm.ShowDialog()

            If myForm.Tag = "Ref" Then
                txtDoc.Focus()
                SendKeys.Send("{TAB}")
            End If

        End If


    End Sub

    Private Sub deleteeob_Click(sender As Object, e As EventArgs) Handles deleteeob.Click
        eobPath.Text = ""
        deleteeob.Hide()
        Dim paths = CreateERAFolders()
        ' Set the initial directory and enable multiselect
        OpenFileDialog1.InitialDirectory = paths.UnprocessedFolder
        OpenFileDialog1.Multiselect = True ' Allow selecting multiple files

        ' Show the dialog and check if the user pressed OK
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            ' Combine selected file paths into a single string
            eobPath.Text = String.Join("; ", OpenFileDialog1.FileNames)
        Else
            eobPath.Text = ""
        End If


        If eobPath.Text <> "" And txtDoc.Text <> "" Then
            For Each path1ass In eobPath.Text.Split(";")
                Dim fileBytes As Byte() = File.ReadAllBytes(path1ass.Trim())
                Dim fname = Path.GetFileName(path1ass.Trim())
                ' Insert or update the byte array into the database
                Using connection As New SqlConnection(connString)
                    connection.Open()

                    ' SQL query to insert the binary data
                    Dim query As String = "insert into EOB_Attachments(DocName,DocNo,Attachment_Contents)  values(@Name,@Doc, @FileData )"


                    Using command As New SqlCommand(query, connection)
                        ' Add the byte array as a parameter
                        command.Parameters.Add("@FileData", SqlDbType.VarBinary).Value = fileBytes
                        command.Parameters.Add("@Doc", SqlDbType.NVarChar).Value = txtDoc.Text
                        command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = fname

                        ' Execute the query
                        command.ExecuteNonQuery()
                    End Using

                    'query = "update payments set EOBPath=@FileData where DocNo=@Doc"

                    'Using command As New SqlCommand(query, connection)
                    '    ' Add the byte array as a parameter
                    '    command.Parameters.Add("@FileData", SqlDbType.VarBinary).Value = fileBytes
                    '    command.Parameters.Add("@Doc", SqlDbType.NVarChar).Value = txtDoc.Text

                    '    ' Execute the query
                    '    command.ExecuteNonQuery()
                    'End Using
                End Using
            Next

            AttachEOB.Text = "Viwe EOB"
            deleteeob.Show()
            MessageBox.Show("Attachment uploaded")
        End If

    End Sub

    Private Sub AttachEOB_TextChanged(sender As Object, e As EventArgs) Handles AttachEOB.TextChanged
        If AttachEOB.Text = "Attach EOB" Then
            deleteeob.Hide()
            eobPath.Text = ""
        Else
            deleteeob.Show()

        End If
    End Sub

    Private Sub VoidClaim_CheckedChanged_1(sender As Object, e As EventArgs) Handles VoidClaim.CheckedChanged
        If VoidClaim.Checked Then
            OrgClaimNumber.Focus()
            If OrgClaimNumber.Text = "" Then
                OrgClaimNumber.Text = "claim number"

            End If
            Corrected.Checked = False
            'txtReason.Text = "CORRECTED REF="
        End If
    End Sub

    Private Sub OrgClaimNumber_TextChanged_1(sender As Object, e As EventArgs) Handles OrgClaimNumber.TextChanged

    End Sub

    Private Sub OrgClaimNumber_Click(sender As Object, e As EventArgs) Handles OrgClaimNumber.Click
        If OrgClaimNumber.Text = "claim number" Then
            OrgClaimNumber.Text = ""
        End If
    End Sub

    Private Sub newInv_CheckedChanged(sender As Object, e As EventArgs) Handles newInv.CheckedChanged

    End Sub
End Class
