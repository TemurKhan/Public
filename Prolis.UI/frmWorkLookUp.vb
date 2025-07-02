Imports System.Windows.Forms
Imports System.data

Public Class frmWorkLookUp

    Private Sub frmWorkLookUp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadWorksheets()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub LoadWorksheets()
        dgv.Rows.Clear()
        Dim cnw As New SqlClient.SqlConnection(connString)
        cnw.Open()
        Dim cmdw As New SqlClient.SqlCommand("Select * from Worksheets", cnw)
        cmdw.CommandType = CommandType.Text
        Dim drw As SqlClient.SqlDataReader = cmdw.ExecuteReader
        If drw.HasRows Then
            While drw.Read
                dgv.Rows.Add(drw("ID"), drw("Name"), drw("DefaultControls"))
            End While
        End If
        cnw.Close()
        cnw = Nothing
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click, btn_Cancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        If dgv.SelectedRows.Count > 0 Then
            Me.Tag = dgv.SelectedRows(0).Cells(0).Value
        End If

        Me.Close()
    End Sub

    Private Sub dgvWorks_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex <> -1 Then
            Me.Tag = dgv.Rows(e.RowIndex).Cells(0).Value.ToString
            btnOK.Enabled = True
        Else
            Me.Tag = ""
            btnOK.Enabled = False
        End If
    End Sub

    Private Sub dgv_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellDoubleClick
        Call btnOK_Click(Nothing, Nothing)
    End Sub

    Private Sub dgvTests_KeyDown(sender As Object, e As KeyEventArgs) Handles dgv.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Call btnOK_Click(Nothing, Nothing)

        End If
    End Sub

End Class
