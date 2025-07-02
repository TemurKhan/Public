Imports Microsoft.Data.SqlClient

Public Class frmRefund
    Private InvRowIndex As Integer

    Private Sub chkEditNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEditNew.Click
        FormClear()
        If chkEditNew.Checked = False Then  'New
            chkEditNew.Text = "New"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\New.ico")
            txtID.Text = GetNextRefundID()
            txtID.ReadOnly = True
            btnRefLookUp.Enabled = False
        Else
            chkEditNew.Text = "Edit"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Edit.ico")
            txtID.ReadOnly = False
            txtID.Text = ""
            btnRefLookUp.Enabled = True
        End If
    End Sub

    Private Sub frmDebits_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtID.Text = GetNextRefundID()
    End Sub

    'Private Function GetNextRefundID() As Long
    '    Dim CNP As New ADODB.Connection
    '    CNP.Open(odbCS)
    '    Dim Rs As New ADODB.Recordset
    '    Rs.Open("Select max(ID) as LastID from Refunds", CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Rs.Fields("LastID").Value Is DBNull.Value Then
    '        GetNextRefundID = 1
    '    Else
    '        GetNextRefundID = Rs.Fields("LastID").Value + 1
    '    End If
    '    Rs.Close()
    '    Rs = Nothing
    '    CNP.Close()
    '    CNP = Nothing
    'End Function

    Private Function GetNextRefundID() As Long
        Dim nextRefundID As Long = 1

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT MAX(ID) AS LastID FROM Refunds"

            Using command As New SqlCommand(query, connection)
                Dim result As Object = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    nextRefundID = Convert.ToInt64(result) + 1
                End If
            End Using
        End Using

        Return nextRefundID
    End Function


    Private Sub FormClear()
        txtPaymentID.Text = "" : txtARName.Text = ""
        txtDoc.Text = "" : txtReason.Text = ""
        txtAmount.Text = "" : txtID.Text = ""
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtID.Text <> "" And txtPaymentID.Text <> "" And txtAmount.Text <> "" Then
            SaveRefund()
            dgvInvoices.Rows.Clear()
            dgvPayment.Rows.Clear()
            FormClear()
            If chkEditNew.Checked = False Then  'New
                txtID.Text = GetNextRefundID()
            End If
        End If
    End Sub

    'Private Sub SaveRefund()
    '    Dim CNR As New ADODB.Connection
    '    CNR.Open(odbCS)
    '    Dim Rs As New ADODB.Recordset
    '    For i As Integer = 0 To dgvPayment.RowCount - 1
    '        If dgvPayment.Rows(i).Cells(5).Value IsNot Nothing _
    '        AndAlso Val(dgvPayment.Rows(i).Cells(5).Value) > 0 Then
    '            Rs.Open("Select * from Refund_Detail where Refund_ID = " & Val(txtID.Text) & " and Charge_ID = " & _
    '            Val(dgvPayment.Rows(i).Cells(1).Value) & " and TGP_ID = " & Val(dgvPayment.Rows(i).Cells(2).Value), _
    '            CNR, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
    '            If Rs.BOF Then Rs.AddNew()
    '            Rs.Fields("Refund_ID").Value = Val(txtID.Text)
    '            Rs.Fields("Charge_ID").Value = Val(dgvPayment.Rows(i).Cells(1).Value)
    '            Rs.Fields("TGP_ID").Value = Val(dgvPayment.Rows(i).Cells(2).Value)
    '            Rs.Fields("Ordinal").Value = i
    '            Rs.Fields("Amount").Value = Val(dgvPayment.Rows(i).Cells(5).Value)
    '            Rs.Fields("LastEditedOn").Value = Date.Now
    '            Rs.Fields("EditedBy").Value = ThisUser.ID
    '            Rs.Update()
    '            Rs.Close()
    '            If Math.Round(Val(dgvPayment.Rows(i).Cells(4).Value) - _
    '            Val(dgvPayment.Rows(i).Cells(5).Value)) = 0 Then    'complete refund 
    '                CNR.Execute("Delete from Payment_Detail where Payment_ID = " & Val(txtPaymentID.Text) & _
    '                " and Charge_ID = " & Val(dgvPayment.Rows(i).Cells(1).Value) & " and TGP_ID = " & _
    '                Val(dgvPayment.Rows(i).Cells(2).Value))
    '            Else
    '                CNR.Execute("Update Payment_Detail set AppliedAmount = " & Math.Round(Val(dgvPayment.Rows(i).Cells(4).Value) - _
    '                Val(dgvPayment.Rows(i).Cells(5).Value)) & " where Payment_ID = " & Val(txtPaymentID.Text) & " and Charge_ID " & _
    '                "= " & Val(dgvPayment.Rows(i).Cells(1).Value) & " and TGP_ID = " & Val(dgvPayment.Rows(i).Cells(2).Value))
    '            End If
    '        End If
    '    Next
    '    '
    '    Rs.Open("Select * from Refunds where ID = " & Val(txtID.Text), CNR, _
    '    ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
    '    If Rs.BOF Then Rs.AddNew()
    '    Rs.Fields("ID").Value = Val(txtID.Text)
    '    Rs.Fields("Payment_ID").Value = Val(txtPaymentID.Text)
    '    Rs.Fields("DocNo").Value = Trim(txtDoc.Text)
    '    Rs.Fields("RefundDate").Value = dtpRefundDate.Value
    '    Rs.Fields("Amount").Value = Val(txtAmount.Text)
    '    Rs.Fields("OutCheckNo").Value = Val(txtCheckNo.Text)
    '    Rs.Fields("Reason").Value = Val(txtReason.Text)
    '    Rs.Fields("LastEditedOn").Value = Date.Now
    '    Rs.Fields("EditedBy").Value = ThisUser.ID
    '    Rs.Update()
    '    Rs.Close()
    '    Rs = Nothing
    '    CNR.Close()
    '    CNR = Nothing
    'End Sub
    Private Sub SaveRefund()
        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim exists As Boolean

            For Each row As DataGridViewRow In dgvPayment.Rows
                If row.Cells(5).Value IsNot Nothing AndAlso Val(row.Cells(5).Value) > 0 Then
                    Dim queryCheck As String = "SELECT COUNT(*) FROM Refund_Detail WHERE Refund_ID = @RefundID AND Charge_ID = @ChargeID AND TGP_ID = @TGPID"
                    Using checkCmd As New SqlCommand(queryCheck, connection)
                        checkCmd.Parameters.AddWithValue("@RefundID", Val(txtID.Text))
                        checkCmd.Parameters.AddWithValue("@ChargeID", Val(row.Cells(1).Value))
                        checkCmd.Parameters.AddWithValue("@TGPID", Val(row.Cells(2).Value))

                        exists = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0
                    End Using

                    Dim query As String
                    If exists Then
                        query = "UPDATE Refund_Detail SET Amount = @Amount, LastEditedOn = @LastEditedOn, EditedBy = @EditedBy WHERE Refund_ID = @RefundID AND Charge_ID = @ChargeID AND TGP_ID = @TGPID"
                    Else
                        query = "INSERT INTO Refund_Detail (Refund_ID, Charge_ID, TGP_ID, Ordinal, Amount, LastEditedOn, EditedBy) 
                             VALUES (@RefundID, @ChargeID, @TGPID, @Ordinal, @Amount, @LastEditedOn, @EditedBy)"
                    End If

                    Using cmd As New SqlCommand(query, connection)
                        cmd.Parameters.AddWithValue("@RefundID", Val(txtID.Text))
                        cmd.Parameters.AddWithValue("@ChargeID", Val(row.Cells(1).Value))
                        cmd.Parameters.AddWithValue("@TGPID", Val(row.Cells(2).Value))
                        cmd.Parameters.AddWithValue("@Ordinal", row.Index)
                        cmd.Parameters.AddWithValue("@Amount", Val(row.Cells(5).Value))
                        cmd.Parameters.AddWithValue("@LastEditedOn", Date.Now)
                        cmd.Parameters.AddWithValue("@EditedBy", ThisUser.ID)
                        cmd.ExecuteNonQuery()
                    End Using

                    ' Handle payment adjustments
                    Dim remainingAmount As Double = Math.Round(Val(row.Cells(4).Value) - Val(row.Cells(5).Value))
                    If remainingAmount = 0 Then
                        Dim deleteQuery As String = "DELETE FROM Payment_Detail WHERE Payment_ID = @PaymentID AND Charge_ID = @ChargeID AND TGP_ID = @TGPID"
                        Using deleteCmd As New SqlCommand(deleteQuery, connection)
                            deleteCmd.Parameters.AddWithValue("@PaymentID", Val(txtPaymentID.Text))
                            deleteCmd.Parameters.AddWithValue("@ChargeID", Val(row.Cells(1).Value))
                            deleteCmd.Parameters.AddWithValue("@TGPID", Val(row.Cells(2).Value))
                            deleteCmd.ExecuteNonQuery()
                        End Using
                    Else
                        Dim updateQuery As String = "UPDATE Payment_Detail SET AppliedAmount = @AppliedAmount WHERE Payment_ID = @PaymentID AND Charge_ID = @ChargeID AND TGP_ID = @TGPID"
                        Using updateCmd As New SqlCommand(updateQuery, connection)
                            updateCmd.Parameters.AddWithValue("@AppliedAmount", remainingAmount)
                            updateCmd.Parameters.AddWithValue("@PaymentID", Val(txtPaymentID.Text))
                            updateCmd.Parameters.AddWithValue("@ChargeID", Val(row.Cells(1).Value))
                            updateCmd.Parameters.AddWithValue("@TGPID", Val(row.Cells(2).Value))
                            updateCmd.ExecuteNonQuery()
                        End Using
                    End If
                End If
            Next

            ' Save refund details
            Dim queryRefund As String = "INSERT INTO Refunds (ID, Payment_ID, DocNo, RefundDate, Amount, OutCheckNo, Reason, LastEditedOn, EditedBy) 
                                     VALUES (@ID, @PaymentID, @DocNo, @RefundDate, @Amount, @OutCheckNo, @Reason, @LastEditedOn, @EditedBy)
                                     ON DUPLICATE KEY UPDATE RefundDate = @RefundDate, Amount = @Amount, OutCheckNo = @OutCheckNo, Reason = @Reason, LastEditedOn = @LastEditedOn, EditedBy = @EditedBy"

            Using cmdRefund As New SqlCommand(queryRefund, connection)
                cmdRefund.Parameters.AddWithValue("@ID", Val(txtID.Text))
                cmdRefund.Parameters.AddWithValue("@PaymentID", Val(txtPaymentID.Text))
                cmdRefund.Parameters.AddWithValue("@DocNo", Trim(txtDoc.Text))
                cmdRefund.Parameters.AddWithValue("@RefundDate", dtpRefundDate.Value)
                cmdRefund.Parameters.AddWithValue("@Amount", Val(txtAmount.Text))
                cmdRefund.Parameters.AddWithValue("@OutCheckNo", Val(txtCheckNo.Text))
                cmdRefund.Parameters.AddWithValue("@Reason", Val(txtReason.Text))
                cmdRefund.Parameters.AddWithValue("@LastEditedOn", Date.Now)
                cmdRefund.Parameters.AddWithValue("@EditedBy", ThisUser.ID)
                cmdRefund.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub PmtLookUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PmtLookUp.Click
        Dim PayInfo As String = frmPmntLookUp.ShowDialog
        If PayInfo <> "" Then
            Dim data() As String = Split(PayInfo, "|")
            If data.Length > 1 Then
                txtPaymentID.Text = Trim(data(0))
                txtDoc.Text = Trim(data(1))
            Else
                txtPaymentID.Text = Trim(data(0))
            End If
            DisplayInvoices(txtPaymentID.Text)
        End If
    End Sub

    'Private Sub DisplayInvoices(ByVal PayID As Long)
    '    dgvInvoices.Rows.Clear()
    '    Dim CNI As New ADODB.Connection
    '    CNI.Open(odbCS)
    '    Dim sSQL As String = "Select a.ID as ChargeID, a.Accession_ID as AccID, b.LastName as LName, " & _
    '    "b.FirstName as FName, a.Svc_Date as SvcDate, (Select sum(AppliedAmount) from Payment_Detail " & _
    '    "where Charge_ID = a.ID and Payment_ID = " & PayID & ") as PmtAmt from Patients b inner join " & _
    '    "(Requisitions c inner join Charges a on a.Accession_ID = c.ID) on b.ID = c.Patient_ID where " & _
    '    "a.ID in (Select distinct Charge_ID from Payment_Detail where Payment_ID = " & PayID & ")"
    '    Dim Rs As New ADODB.Recordset
    '    Rs.Open(sSQL, CNI, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Not Rs.BOF Then
    '        Do Until Rs.EOF
    '            dgvInvoices.Rows.Add(Rs.Fields("ChargeID").Value, Rs.Fields("AccID").Value, _
    '            Rs.Fields("LName").Value & ", " & Rs.Fields("FName").Value, Rs.Fields("SvcDate").Value, _
    '            Format(Rs.Fields("PmtAmt").Value, "0.00"), "")
    '            Rs.MoveNext()
    '        Loop
    '    End If
    '    Rs.Close()
    '    Rs = Nothing
    '    CNI.Close()
    '    CNI = Nothing
    'End Sub

    Private Sub DisplayInvoices(ByVal PayID As Long)
        dgvInvoices.Rows.Clear()

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT a.ID AS ChargeID, a.Accession_ID AS AccID, b.LastName AS LName, 
                               b.FirstName AS FName, a.Svc_Date AS SvcDate, 
                               (SELECT SUM(AppliedAmount) FROM Payment_Detail 
                                WHERE Charge_ID = a.ID AND Payment_ID = @PayID) AS PmtAmt 
                               FROM Patients b 
                               INNER JOIN (Requisitions c INNER JOIN Charges a ON a.Accession_ID = c.ID) 
                               ON b.ID = c.Patient_ID 
                               WHERE a.ID IN (SELECT DISTINCT Charge_ID FROM Payment_Detail WHERE Payment_ID = @PayID)"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@PayID", PayID)

                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        dgvInvoices.Rows.Add(reader("ChargeID"), reader("AccID"),
                                         $"{reader("LName")}, {reader("FName")}", reader("SvcDate"),
                                         Format(reader("PmtAmt"), "0.00"), "")
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub dgvInvoices_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvInvoices.CellClick
        If e.RowIndex <> -1 Then
            txtAmount.Text = ""
            dgvInvoices.Rows(e.RowIndex).Cells(5).Value = ""
            dgvPayment.Rows.Clear()
            DisplayPaymentDetail(dgvInvoices.Rows(e.RowIndex).Cells(0).Value)
            InvRowIndex = e.RowIndex
        Else
            InvRowIndex = e.RowIndex
        End If
        RefundProgress()
    End Sub

    'Private Sub DisplayPaymentDetail(ByVal InvID As Long)
    '    dgvPayment.Rows.Clear()
    '    Dim CNPD As New ADODB.Connection
    '    CNPD.Open(odbCS)
    '    Dim sSQL As String = "Select a.TGP_ID, (Select Name + ' [' + LTrim(RTrim(CPT_Code)) + ']' from Tests " & _
    '    "where ID = a.TGP_ID Union Select Name + ' [' + LTrim(RTrim(CPT_Code)) + ']' from Groups where ID = " & _
    '    "a.TGP_ID Union Select Name + ' [' + LTrim(RTrim(CPT_Code)) + ']' from Profiles where ID = a.TGP_ID) " & _
    '    "as TestName, a.AppliedAmount from Payment_Detail a where a.Payment_ID = " & Val(txtPaymentID.Text) & _
    '    " and a.Charge_ID = " & InvID
    '    Dim Rs As New ADODB.Recordset
    '    Rs.Open(sSQL, CNPD, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Not Rs.BOF Then
    '        Do Until Rs.EOF
    '            dgvPayment.Rows.Add(False, InvID, Rs.Fields("TGP_ID").Value, _
    '            Rs.Fields("TestName").Value, Format(Rs.Fields("AppliedAmount").Value, "0.00"), "")
    '            Rs.MoveNext()
    '        Loop
    '    End If
    '    Rs.Close()
    '    Rs = Nothing
    '    CNPD.Close()
    '    CNPD = Nothing
    '    If dgvPayment.RowCount > 0 Then
    '        btnRefAll.Enabled = True
    '        ClearAll.Enabled = True
    '    Else
    '        btnRefAll.Enabled = False
    '        ClearAll.Enabled = False
    '    End If
    'End Sub
    Private Sub DisplayPaymentDetail(ByVal InvID As Long)
        dgvPayment.Rows.Clear()

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT a.TGP_ID, 
                               (SELECT Name + ' [' + LTrim(RTrim(CPT_Code)) + ']' FROM Tests WHERE ID = a.TGP_ID
                                UNION 
                                SELECT Name + ' [' + LTrim(RTrim(CPT_Code)) + ']' FROM Groups WHERE ID = a.TGP_ID
                                UNION 
                                SELECT Name + ' [' + LTrim(RTrim(CPT_Code)) + ']' FROM Profiles WHERE ID = a.TGP_ID) 
                               AS TestName, a.AppliedAmount 
                               FROM Payment_Detail a 
                               WHERE a.Payment_ID = @PaymentID AND a.Charge_ID = @ChargeID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@PaymentID", Val(txtPaymentID.Text))
                command.Parameters.AddWithValue("@ChargeID", InvID)

                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        dgvPayment.Rows.Add(False, InvID, reader("TGP_ID"), reader("TestName"),
                                       Format(reader("AppliedAmount"), "0.00"), "")
                    End While
                End Using
            End Using
        End Using

        btnRefAll.Enabled = dgvPayment.RowCount > 0
        ClearAll.Enabled = dgvPayment.RowCount > 0
    End Sub

    Private Sub btnRefAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefAll.Click
        Dim RefAmt As Single = 0
        For i As Integer = 0 To dgvPayment.RowCount - 1
            dgvPayment.Rows(i).Cells(5).Value = dgvPayment.Rows(i).Cells(4).Value
            RefAmt += Val(dgvPayment.Rows(i).Cells(4).Value)
        Next
        dgvInvoices.Rows(InvRowIndex).Cells(5).Value = Format(RefAmt, "0.00")
        txtAmount.Text = Format(RefAmt, "0.00")
        RefundProgress()
    End Sub

    Private Sub ClearAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearAll.Click
        For i As Integer = 0 To dgvPayment.RowCount - 1
            dgvPayment.Rows(i).Cells(5).Value = ""
        Next
        dgvInvoices.Rows(InvRowIndex).Cells(5).Value = ""
        txtAmount.Text = ""
        RefundProgress()
    End Sub

    Private Sub RefundProgress()
        If txtID.Text <> "" And txtPaymentID.Text <> "" And txtAmount.Text <> "" Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If
    End Sub

End Class