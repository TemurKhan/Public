Imports System.Windows.Forms
Imports System.data

Public Class frmReflux

    Private Sub frmReflux_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'ClearForm()
    End Sub
    Private Sub ClearForm()
        txtAccID.Text = ""
        txtID.Text = ""
        txtName.Text = ""
        lstRefluxes.Items.Clear()
    End Sub

    Private Sub frmReflux_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim TriggerID As Integer = Me.Tag

        txtID.Text = TriggerID
        txtName.Text = GetTGPName(Val(TriggerID))
        PopulateRefluxes(TriggerID)
        UpdateRefluxes()
        Me.Tag = ""
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Function IsFormOpened(ByVal frm As System.Windows.Forms.Form) As Boolean
        Dim IsOpened As Boolean = False
        Dim TheForm As System.Windows.Forms.Form
        For Each TheForm In Application.OpenForms
            If TheForm Is frm Then
                IsOpened = True
                Exit For
            End If
        Next
        Return IsOpened
    End Function

    Private Sub UpdateRefluxes()
        If txtAccID.Text <> "" And txtID.Text <> "" And lstRefluxes.Items.Count > 0 Then
            Dim ItemX As MyList
            For i As Integer = 0 To lstRefluxes.Items.Count - 1
                ItemX = lstRefluxes.Items(i)
                Dim cnpid As New SqlClient.SqlConnection(connString)
                cnpid.Open()
                Dim cmdpid As New SqlClient.SqlCommand("Select * from Ref_Results " & _
                "where Accession_ID = " & Val(txtAccID.Text) & " and Reflexer_ID = " & _
                Val(txtID.Text) & " and Reflexed_ID = " & ItemX.ItemData, cnpid)
                cmdpid.CommandType = CommandType.Text
                Dim drpid As SqlClient.SqlDataReader = cmdpid.ExecuteReader
                If drpid.HasRows Then
                    lstRefluxes.SetItemChecked(i, True)
                Else
                    lstRefluxes.SetItemChecked(i, False)
                End If
                cnpid.Close()
                cnpid = Nothing
            Next
        End If
    End Sub

    Private Sub PopulateRefluxes(ByVal TriggerID As Integer)
        lstRefluxes.Items.Clear()
        Dim Trigger As String
        Dim sSQL As String = ""
        If IsQualitative(TriggerID) Then
            sSQL = "Select Marked_ID from C_Triggers where Test_ID = " & TriggerID & " order by Ordinal"
        Else
            sSQL = "Select Marked_ID from N_Triggers where Test_ID = " & TriggerID & " order by Ordinal"
        End If
        Dim cnpid As New SqlClient.SqlConnection(connString)
        cnpid.Open()
        Dim cmdpid As New SqlClient.SqlCommand(sSQL, cnpid)
        cmdpid.CommandType = CommandType.Text
        Dim drpid As SqlClient.SqlDataReader = cmdpid.ExecuteReader
        If drpid.HasRows Then
            While drpid.Read
                Trigger = GetTGPName(drpid("Marked_ID"))
                If Not InRefluxList(drpid("Marked_ID")) Then _
                lstRefluxes.Items.Add(New MyList(Trigger, drpid("Marked_ID")))
            End While
        End If
        cnpid.Close()
        cnpid = Nothing
        If lstRefluxes.Items.Count = 1 Then lstRefluxes.SetItemChecked(0, True)
    End Sub

    Private Function InRefluxList(ByVal TGPID As Integer) As Boolean
        Dim i As Integer
        Dim InList As Boolean = False
        Dim ItemX As MyList
        For i = 0 To lstRefluxes.Items.Count - 1
            ItemX = lstRefluxes.Items(i)
            If ItemX.ItemData = TGPID Then
                InList = True
                Exit For
            End If
        Next
        Return InList
    End Function

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        Dim Reflux As String = ""
        Dim i As Integer
        Dim ItemX As MyList
        For i = 0 To lstRefluxes.Items.Count - 1
            ItemX = lstRefluxes.Items(i)
            If lstRefluxes.GetItemChecked(i) = True Then
                If InStr(Reflux, txtAccID.Text & "^" & txtID.Text & "^" & _
                ItemX.ItemData.ToString & "^Add") = 0 Then _
                Reflux += txtAccID.Text & "^" & txtID.Text & "^" & _
                ItemX.ItemData.ToString & "^Add" & "|"
            Else
                If InStr(Reflux, txtAccID.Text & "^" & txtID.Text & "^" & _
                ItemX.ItemData.ToString & "^Remove") = 0 Then _
                Reflux += txtAccID.Text & "^" & txtID.Text & "^" & _
                ItemX.ItemData.ToString & "^Remove" & "|"
            End If
        Next
        If Reflux.EndsWith("|") Then Reflux = Reflux.Substring(0, Len(Reflux) - 1)
        Me.Tag = Reflux
        Me.Close()
    End Sub
End Class
