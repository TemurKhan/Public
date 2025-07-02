Imports Microsoft.Data.SqlClient

Public Class frmQuestions
    Dim EditImg As Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Edit_16.ico")
    Dim DeleteImg As Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Delete.ico")

    Private Sub btn_Add_Q_to_List_Click(sender As Object, e As EventArgs) Handles btn_Save.Click
        If Not String.IsNullOrWhiteSpace(txt_Question.Text) Then

            Dim query As String = ""

            If String.IsNullOrWhiteSpace(txt_Q_ID.Text) Then

                query = $"INSERT INTO AOE_Questions (Question) VALUES ('{txt_Question.Text.Trim.Replace("'", "''")}');
                          SELECT SCOPE_IDENTITY();"
                Dim newID As Integer = ExecuteSqlQuery(query)
                ' Add new row to DataGridView
                dgv.Rows.Add(newID, txt_Question.Text, DeleteImg)
            Else
                '' Edit an existing row based on SrNo
                'Dim found As Boolean = False
                query = $"Update AOE_Questions set Question='{txt_Question.Text.Trim.Replace("'", "''")}' where Q_ID = {txt_Q_ID.Text};"
                ExecuteSqlProcedure(query)

                ' Loop through the rows to find the one with matching SrNo
                For Each row As DataGridViewRow In dgv.Rows
                    ' Assuming "SrNo" is in the first column (index 0)
                    If row.Cells(0).Value.ToString() = txt_Q_ID.Text Then
                        ' Update the values in the matching row
                        row.Cells(1).Value = txt_Question.Text

                        'found = True
                        Exit For ' Exit the loop once the row is found and updated
                    End If
                Next
            End If

            txt_Question.Clear()
            txt_Q_ID.Clear()
        End If
    End Sub


    Private Function ExecuteSqlQuery(ByVal sSql As String) As Integer
        Dim newID As Integer

        Dim cnx As New SqlConnection(connString)
        cnx.Open()
        Dim cmdx As New SqlCommand(sSql, cnx)
        cmdx.CommandType = CommandType.Text

        Try
            newID = Convert.ToInt32(cmdx.ExecuteScalar())

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            cnx.Close()
            cnx = Nothing
        End Try
        Return newID
    End Function

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick

        If e.ColumnIndex = dgv.Columns("Col_Delete").Index AndAlso dgv.Rows.Count > 0 Then

            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this row?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            If result = DialogResult.Yes Then

                Dim query As String = $"Delete from AOE_Questions where Q_ID = {txt_Q_ID.Text};"
                ExecuteSqlProcedure(query)

                dgv.Rows.RemoveAt(e.RowIndex)
            End If

        End If
    End Sub

    Private Sub frmQuestions_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim query As String = $"Select * from AOE_Questions order by Question"

        Dim cndn As New SqlConnection(connString)
        cndn.Open()
        Dim cmddn As New SqlCommand(query, cndn)
        Dim drdn As SqlDataReader = cmddn.ExecuteReader
        If drdn.HasRows Then
            While drdn.Read
                dgv.Rows.Add(drdn("Q_ID"), drdn("Question"), DeleteImg)
            End While

        End If
        cndn.Close()
        cndn = Nothing
    End Sub

    Private Sub dgv_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellDoubleClick
        ' Ensure the double-click is on a valid row
        If e.RowIndex >= 0 Then
            ' Get the selected row
            Dim selectedRow As DataGridViewRow = dgv.Rows(e.RowIndex)

            ' Load the values from the selected row into TextBox controls
            txt_Q_ID.Text = selectedRow.Cells(0).Value.ToString()
            txt_Question.Text = selectedRow.Cells(1).Value.ToString()

        End If
    End Sub
End Class