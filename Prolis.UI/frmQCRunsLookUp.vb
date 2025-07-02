Imports System.Windows.Forms
Imports System.data

Public Class frmQCRunsLookUp

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub frmQCRunsLookUp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Tag = ""
        txtRunName.Text = ""
        txtDate.Text = ""
        dgv.Rows.Clear()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        If dgv.SelectedRows.Count > 0 Then
            
            Me.Tag = dgv.SelectedRows(0).Cells(0).Value
        End If

        Me.Close()
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub dgvRuns_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex <> -1 Then
            Me.Tag = CStr(dgv.Rows(e.RowIndex).Cells(0).Value)
            btnAccept.Enabled = True
        End If
    End Sub

    Private Sub btnAccSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccSearch.Click
        If txtRunName.Text <> "" Or IsDate(txtDate.Text) = True Then
            Dim sSQL As String = ""
            If txtRunName.Text <> "" And Not IsDate(txtDate.Text) Then
                sSQL = "Select * from Runs where Name Like '%" & Trim(txtRunName.Text) & _
                "%' order by Name"
            ElseIf txtRunName.Text = "" And IsDate(txtDate.Text) Then
                sSQL = "Select * from Runs where RunDate between '" & Format(CDate(txtDate.Text), SystemConfig.DateFormat) _
                & "' and '" & Format(CDate(txtDate.Text), SystemConfig.DateFormat) & " 23:59:00' order by Name"
            ElseIf txtRunName.Text <> "" And IsDate(txtDate.Text) Then
                sSQL = "Select * from Runs where Name Like '%" & Trim(txtRunName.Text) & "%' and " & _
                "RunDate between '" & Format(CDate(txtDate.Text), SystemConfig.DateFormat) & "' and '" & _
                Format(CDate(txtDate.Text), SystemConfig.DateFormat) & " 23:59:00' order by Name"
            End If
            dgv.Rows.Clear()
            Dim cnqc As New SqlClient.SqlConnection(connString)
            cnqc.Open()
            Dim cmdqc As New SqlClient.SqlCommand(sSQL, cnqc)
            cmdqc.CommandType = CommandType.Text
            Dim drqc As SqlClient.SqlDataReader = cmdqc.ExecuteReader
            If drqc.HasRows Then
                While drqc.Read
                    dgv.Rows.Add(drqc("ID"), drqc("Name"), _
                    Format(drqc("RunDate"), SystemConfig.DateFormat))
                End While
            End If
            cnqc.Close()
            cnqc = Nothing
            txtRunName.Text = ""
            txtDate.Text = ""
        End If
    End Sub

    Private Sub txtRunName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRunName.TextChanged
        If Trim(txtRunName.Text) <> "" Or IsDate(txtDate.Text) Then
            btnAccSearch.Enabled = True
        Else
            btnAccSearch.Enabled = True
        End If
    End Sub

    Private Sub txtDate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDate.Validated
        If Trim(txtRunName.Text) <> "" Or IsDate(txtDate.Text) Then
            btnAccSearch.Enabled = True
        Else
            btnAccSearch.Enabled = True
        End If
    End Sub

    Private Sub txtRunName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRunName.KeyDown, txtDate.KeyDown
        If e.KeyCode = Keys.Down AndAlso dgv.Rows.Count > 0 Then
            dgv.Focus()
        End If
    End Sub

    Private Sub dgv_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellDoubleClick
        Call btnAccept_Click(Nothing, Nothing)
    End Sub

    Private Sub dgv_KeyDown(sender As Object, e As KeyEventArgs) Handles dgv.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Call btnAccept_Click(Nothing, Nothing)
            Me.txtRunName.Focus()
        End If
    End Sub
End Class
