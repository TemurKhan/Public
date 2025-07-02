Imports System.Windows.Forms
Imports System.data

Public Class frmResultNote
    Public SavedNote As String = ""

    Private Sub frmResultNote_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'txtNote.Text = ""
        'If frmResults.IsHandleCreated Then
        '    txtNote.Text = frmResults.txtComment.Text
        'ElseIf frmATRResults.IsHandleCreated Then
        '    txtNote.Text = frmATRResults.txtComment.Text
        'End If

        If Not String.IsNullOrEmpty(SavedNote) Then
            txtNote.Text = SavedNote
        End If
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    'Public Shadows Function ShowDialog() As String
    '    MyBase.ShowDialog()
    '    Return txtNote.Text 'CType(TextBox1.Text, Integer)
    'End Function

    Private Function GetPhrase(ByVal PhraseKey As String) As String
        Dim Phr As String = ""
        Dim cngpr As New SqlClient.SqlConnection(connString)
        cngpr.Open()
        Dim cmdgpr As New SqlClient.SqlCommand("Select Phrase " & _
        "from Phrases where PhraseKey = '" & PhraseKey & "'", cngpr)
        cmdgpr.CommandType = CommandType.Text
        Dim drgpr As SqlClient.SqlDataReader = cmdgpr.ExecuteReader
        If drgpr.HasRows Then
            While drgpr.Read
                If drgpr("Phrase").ToString.StartsWith("{\rtf1\") Then
                    Phr = RTF_To_Text(drgpr("Phrase"))
                Else
                    Phr = drgpr("Phrase")
                End If
            End While
        End If
        cngpr.Close()
        cngpr = Nothing
        Return Phr
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'Me.Close()
        DialogResult = DialogResult.OK
    End Sub

    Private Sub UpdatePhrase(ByVal PhraseKey As String, ByVal Phrase As String)
        ExecuteSqlProcedure("If Exists(Select * from Phrases where PhraseKey = '" & _
        PhraseKey & "') Update Phrases set Phrase = '" & Phrase & "' where PhraseKey " & _
        "= '" & PhraseKey & "' Else Insert into Phrases (ID, PhraseKey, Phrase) " & _
        "values (" & NextPhraseID() & ", '" & PhraseKey & "', '" & Phrase & "')")
    End Sub

    Private Function NextPhraseID() As Integer
        Dim PhrID As Integer = 1
        Dim cngpr As New SqlClient.SqlConnection(connString)
        cngpr.Open()
        Dim cmdgpr As New SqlClient.SqlCommand("Select " & _
        "max(ID) as LastID from Phrases", cngpr)
        cmdgpr.CommandType = CommandType.Text
        Dim drgpr As SqlClient.SqlDataReader = cmdgpr.ExecuteReader
        If drgpr.HasRows Then
            While drgpr.Read
                If drgpr("LastID") IsNot DBNull.Value _
                Then PhrID = drgpr("LastID") + 1
            End While
        End If
        cngpr.Close()
        cngpr = Nothing
        Return PhrID
    End Function

    Private Sub txtNote_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNote.KeyPress
        Dim DotPoint As Integer
        Dim term As String
        If e.KeyChar = Chr(13) Then
            DotPoint = InStrRev(txtNote.Text, ".", txtNote.SelectionStart)
            If DotPoint <> 0 Then
                term = txtNote.Text.Substring(DotPoint, txtNote.SelectionStart - 1 -
                DotPoint + 1)
                If InStr(term, " ") <> 0 Then
                    Exit Sub
                Else
                    txtNote.SelectionStart = txtNote.SelectionStart - Len(term) - 1
                    txtNote.SelectionLength = Len(term) + 2
                    txtNote.SelectedText = GetPhrase(term)
                    e.KeyChar = Chr(0)
                End If
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub btnPhraseLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhraseLook.Click
        Dim PhraseID As String = frmPhraseLook.ShowDialog
        If PhraseID <> "" Then
            txtNote.AppendText(GetPhraseByID(Val(PhraseID)))
        End If
    End Sub

    Private Function GetPhraseByID(ByVal PhraseID As Integer) As String
        Dim Phr As String = ""
        Dim cngpr As New SqlClient.SqlConnection(connString)
        cngpr.Open()
        Dim cmdgpr As New SqlClient.SqlCommand("Select " & _
        "* from Phrases where ID = " & PhraseID, cngpr)
        cmdgpr.CommandType = CommandType.Text
        Dim drgpr As SqlClient.SqlDataReader = cmdgpr.ExecuteReader
        If drgpr.HasRows Then
            While drgpr.Read
                If drgpr("Phrase").ToString.StartsWith("{\rtf1\") Then
                    Phr = RTF_To_Text(drgpr("Phrase"))
                Else
                    Phr = drgpr("Phrase")
                End If
            End While
        End If
        cngpr.Close()
        cngpr = Nothing
        Return Phr
    End Function
End Class
