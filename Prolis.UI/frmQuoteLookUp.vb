Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient
Imports System.Runtime.InteropServices.JavaScript.JSType

Public Class frmQuoteLookUp

    Private Sub frmQuoteLookUp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Tag = ""
        txtPatient.Text = ""
        txtDate.Text = ""
        dgv.Rows.Clear()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub UpdateSelection()
        If txtPatient.Text <> "" Or IsDate(txtDate.Text) Then
            btnSearch.Enabled = True
        Else
            btnSearch.Enabled = False
        End If
    End Sub
    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Me.Cursor = Cursors.WaitCursor

        If txtPatient.Text <> "" OrElse IsDate(txtDate.Text) = True Then
            Dim sSQL As String = "SELECT a.ID AS QuoteID, a.Dated, a.QuoteAmount, b.LastName, " &
                             "b.FirstName, b.DOB AS DOB, b.Sex AS Gender, c.DocNo, c.Amount " &
                             "FROM Payments c " &
                             "LEFT OUTER JOIN Quotes a ON a.Payment_ID = c.ID " &
                             "LEFT OUTER JOIN Patients b ON a.Patient_ID = b.ID WHERE 1=1"

            ' Build SQL dynamically based on user input
            Dim LastName As String = ""
            Dim FirstName As String = ""
            If txtPatient.Text <> "" Then
                Dim Name() As String

                If InStr(txtPatient.Text, ",") <> 0 Then
                    Name = Split(txtPatient.Text, ",")
                    LastName = Name(0).Trim().Replace("*", "%")
                    FirstName = Name(1).Trim().Replace("*", "%")
                Else
                    LastName = txtPatient.Text.Trim()
                    LastName = LastName.Replace("*", "%")
                End If

                If FirstName = "" Then
                    sSQL &= " AND b.LastName LIKE @LastName"
                Else
                    sSQL &= " AND b.LastName LIKE @LastName AND b.FirstName LIKE @FirstName"
                End If
            End If

            Dim FromDate As DateTime = CDate(txtDate.Text & " 00:00:00")
            Dim ToDate As DateTime = CDate(txtDate.Text & " 23:59:59")
            If IsDate(txtDate.Text) Then
                sSQL &= " AND a.Dated BETWEEN @FromDate AND @ToDate"
            End If

            dgv.Rows.Clear()

            ' Use ADO.NET for database access
            Using connection As New SqlConnection(connString)
                connection.Open()
                Using command As New SqlCommand(sSQL, connection)
                    command.CommandType = CommandType.Text

                    ' Add parameters to the query
                    If txtPatient.Text <> "" Then
                        command.Parameters.AddWithValue("@LastName", LastName & "%")
                        If FirstName <> "" Then
                            command.Parameters.AddWithValue("@FirstName", FirstName & "%")
                        End If
                    End If
                    If IsDate(txtDate.Text) Then
                        command.Parameters.AddWithValue("@FromDate", FromDate)
                        command.Parameters.AddWithValue("@ToDate", ToDate)
                    End If

                    Using reader As SqlDataReader = command.ExecuteReader()
                        If reader.HasRows Then
                            While reader.Read()
                                Dim Paid As String = If(IsDBNull(reader("Amount")), "", Format(reader("Amount"), "##0.00"))
                                Dim DocNo As String = If(IsDBNull(reader("DocNo")), "", Trim(reader("DocNo").ToString()))

                                If Not IsDBNull(reader("LastName")) Then
                                    dgv.Rows.Add(
                                    reader("QuoteID"),
                                    Format(reader("Dated"), SystemConfig.DateFormat),
                                    reader("LastName") & ", " & reader("FirstName"),
                                    reader("Gender"),
                                    Format(reader("DOB"), SystemConfig.DateFormat),
                                    Format(reader("QuoteAmount"), "##0.00"),
                                    DocNo,
                                    Paid
                                )
                                End If
                            End While
                        End If
                    End Using
                End Using
            End Using

            ' Clear search inputs
            txtPatient.Text = ""
            txtDate.Text = ""
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub txtPatient_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatient.Validated
        UpdateSelection()
    End Sub

    Private Sub txtDate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDate.Validated
        If UserEnteredText(txtDate) <> "" Then
            If Not IsDate(txtDate.Text) Then
                MsgBox("Invalid date", MsgBoxStyle.Critical, "Prolis")
                txtDate.Text = ""
            End If
        End If
        UpdateSelection()
    End Sub

    Private Sub dgv_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex <> -1 Then
            Me.Tag = dgv.Rows(e.RowIndex).Cells(0).Value.ToString
            btnAccept.Enabled = True
        End If
    End Sub

    Private Sub btnAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        If dgv.SelectedRows.Count > 0 Then

            Me.Tag = dgv.SelectedRows(0).Cells(0).Value
        End If

        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub txtTerm_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPatient.KeyDown, txtDate.KeyDown
        If e.KeyCode = Keys.Down AndAlso dgv.Rows.Count > 0 Then
            dgv.Focus()
        End If
    End Sub

    Private Sub dgv_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellDoubleClick
        Call btnAccept_Click(Nothing, Nothing)
    End Sub

    Private Sub dgvTests_KeyDown(sender As Object, e As KeyEventArgs) Handles dgv.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Call btnAccept_Click(Nothing, Nothing)
            Me.txtPatient.Focus()
        End If
    End Sub
End Class
