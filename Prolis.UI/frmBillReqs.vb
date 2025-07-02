Imports System.Windows.Forms
Imports Microsoft.Data.SqlClient

Public Class frmBillReqs

    Private Sub chkEditNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEditNew.Click
        If chkEditNew.Checked = False Then
            chkEditNew.Text = "Edit"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Edit.ico")
            dgvReqs.Enabled = True
            txtReqID.Text = ""
        Else
            chkEditNew.Text = "New"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\New.ico")
            dgvReqs.Enabled = False
            txtReqID.Text = NextBillReqID()
            btnDelete.Enabled = False
            btnSave.Enabled = True
        End If
    End Sub

    Private Function NextBillReqID0() As Integer
        'Dim BRID As Integer
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connstring)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select Max(ID) as LastID from BillingRequisits", CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Rs.Fields("LastID").Value Is System.DBNull.Value Then
        '    BRID = 1
        'Else
        '    BRID = Rs.Fields("LastID").Value + 1
        'End If
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
        'Return BRID
    End Function

    Private Function NextBillReqID() As Integer
        Dim BRID As Integer = 1 ' Default to 1 if no records exist

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT MAX(ID) AS LastID FROM BillingRequisits"

            Using command As New SqlCommand(query, connection)
                Dim result As Object = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    BRID = Convert.ToInt32(result) + 1
                End If
            End Using
        End Using

        Return BRID
    End Function


    Private Sub dgvReqs_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvReqs.CellDoubleClick
        If e.RowIndex <> -1 Then
            Dim i As Integer
            txtReqID.Text = dgvReqs.Rows(e.RowIndex).Cells(0).Value
            For i = 0 To cmbBillType.Items.Count - 1
                If dgvReqs.Rows(e.RowIndex).Cells(1).Value = cmbBillType.Items(i).ToString Then
                    cmbBillType.SelectedIndex = i
                    Exit For
                End If
            Next
            For i = 0 To cmbCategory.Items.Count - 1
                If dgvReqs.Rows(e.RowIndex).Cells(2).Value = cmbCategory.Items(i).ToString Then
                    cmbCategory.SelectedIndex = i
                    Exit For
                End If
            Next
            If cmbCategory.SelectedIndex = -1 Then cmbCategory.Text = dgvReqs.Rows(e.RowIndex).Cells(2).Value
            txtName.Text = dgvReqs.Rows(e.RowIndex).Cells(3).Value
            chkRequired.Checked = dgvReqs.Rows(e.RowIndex).Cells(4).Value
            btnDelete.Enabled = True
            btnSave.Enabled = True
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtReqID.Text <> "" And cmbBillType.SelectedIndex <> -1 And _
        cmbCategory.Text <> "" And txtName.Text <> "" Then
            SaveBillReq()
            ClearEditFields()
            LoadBillingReqs()
            btnDelete.Enabled = False
            btnSave.Enabled = False
            If chkEditNew.Checked = True Then txtReqID.Text = NextBillReqID() 'new
        End If
    End Sub

    Private Sub LoadBillingReqs0()
        'dgvReqs.Rows.Clear()
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connstring)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select * from BillingRequisits", CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then
        '    Do Until Rs.EOF
        '        dgvReqs.Rows.Add(Rs.Fields("ID").Value, Rs.Fields("BillingType").Value, _
        '        Rs.Fields("Category").Value, Rs.Fields("BillingRequisit").Value, _
        '        Rs.Fields("Required").Value)
        '        Rs.MoveNext()
        '    Loop
        'End If
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
    End Sub

    Private Sub LoadBillingReqs()
        dgvReqs.Rows.Clear()

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT ID, BillingType, Category, BillingRequisit, Required FROM BillingRequisits"

            Using command As New SqlCommand(query, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        dgvReqs.Rows.Add(reader("ID"), reader("BillingType"), reader("Category"), reader("BillingRequisit"), reader("Required"))
                    End While
                End Using
            End Using
        End Using
    End Sub


    Private Sub ClearEditFields()
        txtReqID.Text = ""
        cmbBillType.SelectedIndex = -1
        cmbCategory.SelectedIndex = -1
        cmbCategory.Text = ""
        txtName.Text = ""
        chkRequired.Checked = False
    End Sub

    Private Sub SaveBillReq0()
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connstring)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select * from BillingRequisits where ID = " & Val(txtReqID.Text), CNP,
        'ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        'If Rs.BOF Then Rs.AddNew()
        'Rs.Fields("ID").Value = Val(txtReqID.Text)
        'Rs.Fields("BillingType").Value = cmbBillType.SelectedItem.ToString
        'Rs.Fields("Category").Value = cmbCategory.Text
        'Rs.Fields("BillingRequisit").Value = txtName.Text
        'Rs.Fields("Required").Value = chkRequired.Checked
        'Rs.Update()
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
    End Sub

    Private Sub SaveBillReq()
        Using connection As New SqlConnection(connString)
            connection.Open()

            ' Check if record exists
            Dim recordExists As Boolean
            Dim checkQuery As String = "SELECT COUNT(*) FROM BillingRequisits WHERE ID = @ReqID"

            Using checkCmd As New SqlCommand(checkQuery, connection)
                checkCmd.Parameters.AddWithValue("@ReqID", Val(txtReqID.Text))
                recordExists = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0
            End Using

            ' Insert or update logic
            Dim query As String
            If recordExists Then
                query = "UPDATE BillingRequisits SET BillingType = @BillingType, Category = @Category, BillingRequisit = @BillingRequisit, Required = @Required WHERE ID = @ReqID"
            Else
                query = "INSERT INTO BillingRequisits (ID, BillingType, Category, BillingRequisit, Required) VALUES (@ReqID, @BillingType, @Category, @BillingRequisit, @Required)"
            End If

            Using cmd As New SqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@ReqID", Val(txtReqID.Text))
                cmd.Parameters.AddWithValue("@BillingType", cmbBillType.SelectedItem.ToString())
                cmd.Parameters.AddWithValue("@Category", cmbCategory.Text)
                cmd.Parameters.AddWithValue("@BillingRequisit", txtName.Text)
                cmd.Parameters.AddWithValue("@Required", chkRequired.Checked)

                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub


    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If txtReqID.Text <> "" And cmbBillType.SelectedIndex <> -1 And _
        cmbCategory.Text <> "" And txtName.Text <> "" Then
            Dim RetVal As Integer = MsgBox("Are you sure you want to delete the displayed Requisit?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
            If RetVal = vbYes Then
                ExecuteSqlProcedure("Delete from BillingRequisits where ID = " & Val(txtReqID.Text))
                ClearEditFields()
                LoadBillingReqs()
                btnDelete.Enabled = False
                btnSave.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub chkRequired_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRequired.CheckedChanged
        If chkRequired.Checked = False Then 'No
            chkRequired.Text = "No"
        Else
            chkRequired.Text = "Yes"
        End If
    End Sub

    Private Sub txtName_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.Validated
        Validate_Progress()
    End Sub

    Private Sub Validate_Progress()
        If txtReqID.Text <> "" And cmbBillType.SelectedIndex <> -1 And _
        cmbCategory.Text <> "" And txtName.Text <> "" Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub cmbCategory_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCategory.Validated
        Validate_Progress()
    End Sub

    Private Sub cmbBillType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBillType.SelectedIndexChanged
        Validate_Progress()
    End Sub

    Private Sub txtReqID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtReqID.Validated
        Validate_Progress()
    End Sub

    Private Sub frmBillReqs_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadBillingReqs()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub
End Class
