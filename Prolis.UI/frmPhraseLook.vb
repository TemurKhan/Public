Option Compare Text
Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmPhraseLook

    Private Sub frmPhraseLook_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbSearch.SelectedIndex = 0
        PopulatePhraseTypes()
        cmbType.SelectedIndex = 0
        Me.Tag = ""
        dgvPhrases.Rows.Clear()
        btnAccept.Enabled = False
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub PopulatePhraseTypes()
        cmbType.Items.Clear()
        Dim cnpt As New SqlConnection(connString)
        cnpt.Open()
        Dim cmdpt As New SqlCommand("Select * from PhraseTypes", cnpt)
        cmdpt.CommandType = CommandType.Text
        Dim drpt As SqlDataReader = cmdpt.ExecuteReader
        If drpt.HasRows Then
            cmbType.Items.Add(New MyList("*** ALL ***", -1))
            While drpt.Read
                cmbType.Items.Add(New MyList(drpt("PhraseType"), drpt("ID")))
            End While
        End If
        cnpt.Close()
        cnpt = Nothing
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub cmbSearch_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSearch.SelectedIndexChanged
        If cmbSearch.SelectedIndex = 0 Then     'Phrase ID
            Label1.Text = "Phrase ID"
        Else
            Label1.Text = "Phrase Key"
        End If
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        If cmbSearch.SelectedIndex = 0 Then
            Numerals(sender, e)
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Tag = ""
        Me.Close()
    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        Me.Close()
    End Sub

    Private Sub dgvPhrases_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPhrases.CellClick
        If e.RowIndex <> -1 Then
            Me.Tag = dgvPhrases.Rows(e.RowIndex).Cells(0).Value.ToString
            btnAccept.Enabled = True
        End If
    End Sub


    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgvPhrases.Rows.Clear()
        Dim CTEXT As String = ""
        Dim sSQL As String = "SELECT * FROM Phrases"

        ' Build SQL query with conditions
        If txtSearch.Text <> "" Then
            If cmbSearch.SelectedIndex = 0 Then ' Search by ID
                sSQL += " WHERE ID = @SearchValue"
            Else ' Search by PhraseKey
                sSQL += " WHERE PhraseKey LIKE @SearchValue"
            End If
        End If

        If cmbType.SelectedIndex > 0 Then
            Dim ItemX As MyList = DirectCast(cmbType.SelectedItem, MyList)
            sSQL += If(sSQL.Contains("WHERE"), " AND ", " WHERE ") & "PhraseType_ID = @PhraseTypeID"
        End If

        Using connection As New SqlConnection(connString)
            connection.Open()

            Using command As New SqlCommand(sSQL, connection)
                ' Add parameters to prevent SQL injection
                If txtSearch.Text <> "" Then
                    If cmbSearch.SelectedIndex = 0 Then
                        command.Parameters.AddWithValue("@SearchValue", txtSearch.Text)
                    Else
                        command.Parameters.AddWithValue("@SearchValue", txtSearch.Text & "%")
                    End If
                End If

                If cmbType.SelectedIndex > 0 Then
                    Dim ItemX As MyList = DirectCast(cmbType.SelectedItem, MyList)
                    command.Parameters.AddWithValue("@PhraseTypeID", ItemX.ItemData)
                End If

                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        CTEXT = If(reader("Phrase").ToString().StartsWith("{\rtf1\"), RTF_To_Text(reader("Phrase").ToString()), reader("Phrase").ToString())
                        dgvPhrases.Rows.Add(reader("ID"), reader("PhraseKey"), CTEXT)
                    End While
                End Using
            End Using
        End Using
    End Sub
    Private Sub btnPhrases_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhrases.Click
        frmPhrases.Show()
        frmPhrases.MdiParent = frmDashboard
        Me.Close()
    End Sub
End Class
