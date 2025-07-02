Imports System.Windows.Forms
Imports System.data

Public Class frmLabLook

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        Me.Close()
    End Sub

    Private Sub dgvLabs_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLabs.CellClick
        If e.RowIndex <> -1 Then
            Me.Tag = dgvLabs.Rows(e.RowIndex).Cells(0).Value.ToString
            btnAccept.Enabled = True
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub frmLabLook_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Tag = ""
        txtSearch.Text = ""
        dgvLabs.Rows.Clear()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim LabName As String = ""
        If txtSearch.Text <> "" Then LabName = Sanitize(txtSearch.Text)
        Dim sSQL As String = ""
        If LabName <> "" Then
            sSQL = "Select * from Labs where LabName like '" & LabName & "%' order by LabName"
        Else
            ssql = "Select * from Labs order by LabName"
        End If
        dgvLabs.Rows.Clear()
        Dim cnlab As New SqlClient.SqlConnection(connString)
        cnlab.Open()
        Dim cmdlab As New SqlClient.SqlCommand(sSQL, cnlab)
        cmdlab.CommandType = CommandType.Text
        Dim drlab As SqlClient.SqlDataReader = cmdlab.ExecuteReader
        If drlab.HasRows Then
            While drlab.Read
                dgvLabs.Rows.Add(drlab("ID"), drlab("LabName"), _
                drlab("Active"), drlab("IsPrimary"), _
                GetAddress(drlab("Address_ID")))
            End While
        End If
        cnlab.Close()
        cnlab = Nothing
        txtSearch.Text = ""
    End Sub

    Private Function Sanitize(ByVal STR As String) As String
        STR = Trim(STR)
        If InStr(STR, "drop table") > 0 Or InStr(STR, "xp_") > 0 Then
            STR = ""
        Else
            STR = Replace(STR, ";", "")
            STR = Replace(STR, "\", "")
            STR = Replace(STR, "*", "")
            STR = STR & "%"
        End If
        Sanitize = STR
    End Function
End Class
