Imports Prolis.frmPayments
Imports QR

Public Class frmEob_Attachments
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex <> -1 Then
            Dim ID = DataGridView1.Rows(e.RowIndex).Cells(3).Value

            If e.ColumnIndex = 1 Then


                Dim contents = CommonData.RetrieveColumnValueList("EOB_Attachments", "Attachment_Contents", "ID", "'" & ID & "'", "ALL")


                If contents Is DBNull.Value Then
                    Return
                End If


                ' Combine the arrays
                Dim combinedBytes As Byte() = Nothing
                Dim Attachments As List(Of Attachments) = New List(Of Attachments)()
                ' combinedBytes now holds the merged content

                For Each cntents In contents

                    Dim attcntents = cntents("Attachment_Contents")
                    Dim fileBytes As Byte() = CType(attcntents, Byte())
                    combinedBytes = fileBytes.Concat(fileBytes).ToArray()

                Next

                Dim f As New frmWebView()

                f.LoadPdfData(combinedBytes)

                f.Show(Me)

                f.Owner = Nothing

            ElseIf e.ColumnIndex = 2 Then
                ExecuteSqlProcedure("delete from EOB_Attachments where ID = " & ID)
                DataGridView1.Rows.RemoveAt(e.RowIndex)
                Me.Tag = "Ref"
            End If
        End If
    End Sub
    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()

        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function
    Private Sub Eob_Attachments_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed

    End Sub
End Class