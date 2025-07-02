Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmUserLook

    Private Sub frmUserLook_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbTermType.SelectedIndex = 0
        txtTerm.Text = frmUserMgmt.txtUserName.Text
        frmUserMgmt.txtUserName.Text = ""
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgvUsers.Rows.Clear()
        Dim sSQL As String = ""
        If txtTerm.Text <> "" Then
            If cmbTermType.SelectedIndex = 0 Then
                sSQL = "Select * from Users where ID <> 0 and userName = '" & Trim(txtTerm.Text) & "'"
            Else
                sSQL = "Select * from Users where ID <> 0 and FullName like '" & Trim(txtTerm.Text) & "%'"
            End If
        End If
        Dim cnts As New SqlConnection(connString)
        cnts.Open()
        Dim cmdts As New SqlCommand(sSQL, cnts)
        cmdts.CommandType = CommandType.Text
        Dim drts As SqlDataReader = cmdts.ExecuteReader
        If drts.HasRows Then
            While drts.Read
                dgvUsers.Rows.Add(drts("ID"), drts("UserName"), drts("FullName"))
            End While
        End If
        cnts.Close()
        cnts = Nothing
        txtTerm.Text = ""
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        Me.Close()
    End Sub

    Private Sub txtTerm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTerm.TextChanged
        If txtTerm.Text <> "" Then
            btnSearch.Enabled = True
        Else
            btnSearch.Enabled = False
        End If
    End Sub

    Private Sub dgvUsers_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvUsers.CellClick
        Me.Tag = dgvUsers.Rows(e.RowIndex).Cells(0).Value.ToString
        btnAccept.Enabled = True
    End Sub

End Class
