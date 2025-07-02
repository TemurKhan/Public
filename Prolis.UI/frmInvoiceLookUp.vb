Imports Microsoft.Data.SqlClient

Public Class frmInvoiceLookUp

    Private Sub frmInvoiceLookUp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Tag = ""
        txtAccID.Text = ""
        txtTerm.Text = ""
        dtpSvcDate.Value = Date.Now
        dgv.Rows.Clear()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)

    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Private Sub btnLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLook.Click

        Me.Cursor = Cursors.WaitCursor

        dgv.Rows.Clear()
        Dim sSQL As String = ""
        'Dim Rs As New ADODB.Recordset
        Dim Names() As String
        If Trim(txtAccID.Text) <> "" Then
            sSQL = "Select a.*, c.LastName, c.FirstName from Charges a inner join " &
            "(Requisitions b inner join Patients c on c.ID = b.Patient_ID) on b.ID " &
            "=a.Accession_ID where a.Accession_ID = " & Trim(txtAccID.Text)
        ElseIf Trim(txtTerm.Text) <> "" Then    'Name
            Names = Split(Trim(txtTerm.Text), ",")
            If Names.Length > 1 AndAlso Trim(Names(1)) <> "" Then   'both names
                sSQL = "Select a.*, c.LastName, c.FirstName from Charges a inner " & _
                "join (Requisitions b inner join Patients c on c.ID = b.Patient_ID) " _
                & "on b.ID = a.Accession_ID where a.Svc_Date between '" & _
                Format(dtpSvcDate.Value, SystemConfig.DateFormat) & "' and '" & _
                Format(dtpSvcDate.Value, SystemConfig.DateFormat) & " 23:59:00' and c.LastName " _
                & "like '" & Trim(Names(0)) & "%' and c.FirstName Like '" & _
                Trim(Names(1)) & "%'"
            Else    'Just Last
                sSQL = "Select a.*, c.LastName, c.FirstName from Charges a inner " & _
                "join (Requisitions b inner join Patients c on c.ID = b.Patient_ID) " _
                & "on b.ID = a.Accession_ID where a.Svc_Date between '" & _
                Format(dtpSvcDate.Value, SystemConfig.DateFormat) & "' and '" & _
                Format(dtpSvcDate.Value, SystemConfig.DateFormat) & " 23:59:00' and c.LastName " _
                & "like '" & Trim(Names(0)) & "%'"
            End If
        End If
        If sSQL <> "" Then
            Dim cnfin As New SqlConnection(connString)
            cnfin.Open()
            Dim cmdfin As New SqlCommand(sSQL, cnfin)
            cmdfin.CommandType = CommandType.Text
            Dim drfin As SqlDataReader = cmdfin.ExecuteReader
            If drfin.HasRows Then
                While drfin.Read
                    dgv.Rows.Add(drfin("ID"), drfin("Accession_ID"), _
                    drfin("LastName") & ", " & _
                    drfin("LastName"), Format(drfin("GrossAmount"), "0.00"))
                End While
            End If
            cnfin.Close()
            cnfin = Nothing
        End If

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub dgvInvoices_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex <> -1 Then
            Me.Tag = dgv.Rows(e.RowIndex).Cells(0).Value.ToString
            btnOK.Enabled = True
        End If
    End Sub

    Private Sub txtTerm_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTerm.KeyDown
        If e.KeyCode = Keys.Down AndAlso dgv.Rows.Count > 0 Then
            dgv.Focus()
        End If
    End Sub

    Private Sub dgv_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellDoubleClick
        Call btnOK_Click(Nothing, Nothing)
    End Sub
    Private Sub dgvTests_KeyDown(sender As Object, e As KeyEventArgs) Handles dgv.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Call btnOK_Click(Nothing, Nothing)
            Me.txtTerm.Focus()
        End If
    End Sub

    Private Sub Label19_Click(sender As Object, e As EventArgs) Handles Label19.Click
        txtAccID.Text = Clipboard.GetText()
        btnLook.PerformClick()
    End Sub
End Class


