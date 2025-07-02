Imports System.Windows.Forms
Imports System.data

Public Class frmReportLookUp

    Private Sub frmReportLookUp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Tag = ""
        txtTerm.Text = "*"
        cmbTerm.SelectedIndex = 0
        DisplayReports()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub DisplayReports()
        dgvReports.Rows.Clear()
        'Dim Rs As New ADODB.Recordset
        If txtTerm.Text <> "" Then
            If cmbTerm.SelectedIndex = 0 Then   'Name
                Dim Report As String = Replace(txtTerm.Text, "*", "")
                Report = Trim(Replace(Report, " ", "%")) & "%"
                Dim cndr As New SqlClient.SqlConnection(connString)
                cndr.Open()
                Dim cmddr As New SqlClient.SqlCommand("Select * from " & _
                "Reports where Name like '" & Report & "' or " & _
                "Description like '" & Report & "' order by Name", cndr)
                cmddr.CommandType = CommandType.Text
                Dim drdr As SqlClient.SqlDataReader = cmddr.ExecuteReader
                If drdr.HasRows Then
                    While drdr.Read
                        dgvReports.Rows.Add(drdr("ID").ToString, drdr("Name"))
                    End While
                End If
                cndr.Close()
                cndr = Nothing
            End If
            txtTerm.Text = ""
        End If
        'Rs = Nothing
    End Sub

    Private Sub txtTerm_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTerm.KeyPress
        If cmbTerm.SelectedIndex > 0 Then Numerals(sender, e)
    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        Me.Close()
    End Sub

    Private Sub dgvReports_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvReports.CellClick
        If e.RowIndex <> -1 Then
            Me.Tag = CStr(dgvReports.Rows(e.RowIndex).Cells(0).Value.ToString)
            btnAccept.Enabled = True
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub
End Class
