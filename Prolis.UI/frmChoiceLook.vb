Imports System.Windows.Forms
Imports System.Data

Public Class frmChoiceLook

    Private Sub frmChoiceLook_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'ChoiceTestID = 0
    End Sub

    Private Sub frmChoiceLook_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Tag = ""
        If ChoiceTestID <> 0 Then PopulateChoices(ChoiceTestID)
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub PopulateChoices(ByVal TestID As Integer)
        lstChoices.Items.Clear()
        Dim cnpc As New SqlClient.SqlConnection(connString)
        cnpc.Open()
        Dim cmdpc As New SqlClient.SqlCommand("Select * from C_Ranges where Test_ID = " & TestID, cnpc)
        cmdpc.CommandType = CommandType.Text
        Dim drpc As SqlClient.SqlDataReader = cmdpc.ExecuteReader
        If drpc.HasRows Then
            While drpc.Read
                lstChoices.Items.Add(Trim(drpc("Choice")))
            End While
        End If
        cnpc.Close()
        cnpc = Nothing
    End Sub

    Private Sub lstChoices_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstChoices.SelectedIndexChanged
        If lstChoices.SelectedIndex <> -1 Then
            btnOK.Enabled = True
        Else
            btnOK.Enabled = False
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If lstChoices.SelectedItems.Count = 0 Then
            Me.Tag = ""
        Else
            Me.Tag = lstChoices.SelectedItem.ToString
        End If
        Me.Close()
    End Sub
End Class
