Imports System.Windows.Forms
Imports System.Data

Public Class frmScriptLookUp

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex <> -1 Then
            Me.Tag = Trim(dgv.Rows(e.RowIndex).Cells(0).Value) _
            & "|" & Trim(dgv.Rows(e.RowIndex).Cells(1).Value)
            btnOK.Enabled = True
        Else
            Me.Tag = ""
            btnOK.Enabled = False
        End If
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        'Me.Close()

        If dgv.CurrentRow IsNot Nothing Then

            Dim rowIndex As Integer = dgv.CurrentRow.Index

            Me.Tag = Trim(dgv.Rows(rowIndex).Cells(0).Value) & "|" & Trim(dgv.Rows(rowIndex).Cells(1).Value)
        End If

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Tag = ""
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Me.Cursor = Cursors.WaitCursor

        dgv.Rows.Clear()
        Dim sSQL As String = ""
        If Trim(txtIdentifier.Text) = "" Then
            sSQL = "Select * from Scripts"
        Else
            sSQL = "Select * from Scripts where Identifier like '" & Trim(txtIdentifier.Text) & "%'"
        End If
        '
        If connString <> "" Then
            Dim cnss As New SqlClient.SqlConnection(connString)
            cnss.Open()
            Dim cmdss As New SqlClient.SqlCommand(sSQL, cnss)
            cmdss.CommandType = CommandType.Text
            Dim drss As SqlClient.SqlDataReader = cmdss.ExecuteReader
            If drss.HasRows Then
                While drss.Read
                    dgv.Rows.Add(drss("Identifier"), drss("Script"))
                End While
            End If
            cnss.Close()
            cnss = Nothing
            'Else
            '    Dim cnss As New Odbc.OdbcConnection(odbCS)
            '    cnss.Open()
            '    Dim cmdss As New Odbc.OdbcCommand(sSQL, cnss)
            '    cmdss.CommandType = CommandType.Text
            '    Dim drss As Odbc.OdbcDataReader = cmdss.ExecuteReader
            '    If drss.HasRows Then
            '        While drss.Read
            '            dgv.Rows.Add(drss("Identifier"), drss("Script"))
            '        End While
            '    End If
            '    cnss.Close()
            '    cnss = Nothing
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub frmScriptLookUp_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dgv.Rows.Clear()
        btnOK.Enabled = False
    End Sub

    Private Sub txtIdentifier_KeyDown(sender As Object, e As KeyEventArgs) Handles txtIdentifier.KeyDown
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
            Me.txtIdentifier.Focus()
        End If
    End Sub
End Class
