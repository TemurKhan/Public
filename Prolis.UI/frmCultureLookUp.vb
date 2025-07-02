Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmCultureLookUp
    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub dgvCultures_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCultures.CellClick
        If e.RowIndex <> -1 Then
            Me.Tag = dgvCultures.Rows(e.RowIndex).Cells(0).Value & _
            "|" & dgvCultures.Rows(e.RowIndex).Cells(1).Value & "|" _
            & dgvCultures.Rows(e.RowIndex).Cells(2).Value
            btnAccept.Enabled = True
        Else
            Me.Tag = ""
            btnAccept.Enabled = False
        End If
    End Sub

    Private Sub frmCultureLookUp_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        PopulateCultures()
        If dgvCultures.RowCount > 0 Then
            If Trim(frmSystemConfig.txtCulture.Text) <> "" Then
                For i As Integer = 0 To dgvCultures.RowCount - 1
                    If Trim(frmSystemConfig.txtCulture.Text) = _
                    Trim(dgvCultures.Rows(i).Cells(2).Value) Then
                        dgvCultures.Rows(i).Selected = True
                        Me.Tag = dgvCultures.Rows(i).Cells(0).Value & _
                        "|" & dgvCultures.Rows(i).Cells(1).Value & "|" _
                        & dgvCultures.Rows(i).Cells(2).Value
                        btnAccept.Enabled = True
                        Exit For
                    End If
                Next
            End If
        End If

    End Sub

    Private Sub PopulateCultures()
        Me.Cursor = Cursors.WaitCursor

        dgvCultures.Rows.Clear()
        Dim cncul As New SqlConnection(connString)
        cncul.Open()
        Dim cmdcul As New SqlCommand("Select * from " &
        "Environments order by Country, Language", cncul)
        cmdcul.CommandType = CommandType.Text
        Dim drcul As SqlDataReader = cmdcul.ExecuteReader
        If drcul.HasRows Then
            While drcul.Read
                dgvCultures.Rows.Add(Trim(drcul("Country")), _
                Trim(drcul("Language")), Trim(drcul("Culture2")), _
                Trim(drcul("CountryCd2")), Trim(drcul("CountryCd3")), _
                Trim(drcul("LangCd2")), Trim(drcul("LangCd3")))
            End While
        End If
        cncul.Close()
        cncul = Nothing
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        If dgvCultures.CurrentRow IsNot Nothing Then

            Dim rowIndex As Integer = dgvCultures.CurrentRow.Index

            Me.Tag = dgvCultures.Rows(rowIndex).Cells(0).Value & _
            "|" & dgvCultures.Rows(rowIndex).Cells(1).Value & "|" _
            & dgvCultures.Rows(rowIndex).Cells(2).Value

        End If

        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click, btn_Cancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub dgvCultures_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCultures.CellDoubleClick
        Call btnAccept_Click(Nothing, Nothing)
    End Sub
    Private Sub dgvCultures_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvCultures.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Call btnAccept_Click(Nothing, Nothing)

        End If
    End Sub
End Class
