Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmProfLookup

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click, btn_Cancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If dgv.SelectedRows.Count > 0 Then
            Me.Tag = dgv.SelectedRows(0).Cells(0).Value
        End If
        '
        Me.Close()
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub btnLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLook.Click

        'Dim sSQL As String = ""
        'If txtSearch.Text <> "" Then
        '    If cmbPosition.SelectedIndex = 0 Then
        '        ssql = "Select * from Profiles where Name like '%" & txtSearch.Text & "%'" _
        '        & " OR Abbr like '%" & txtSearch.Text & "%' OR Description like '%" & _
        '        txtSearch.Text & "%' order by Name"
        '    ElseIf cmbPosition.SelectedIndex = 1 Then
        '        ssql = "Select * from Profiles where Name like '" & txtSearch.Text & "%'" _
        '        & " OR Abbr like '" & txtSearch.Text & "%' OR Description like '" & _
        '        txtSearch.Text & "%' order by Name"
        '    End If
        '    txtSearch.Text = ""
        'Else
        '    ssql = "Select * from Profiles order by Name"
        'End If
        Me.Cursor = Cursors.WaitCursor
        dgv.Rows.Clear()

        Dim sSql As String = "SELECT * FROM ( " &
                             "      SELECT *, CONCAT(CONVERT(varchar, ID), ' ', Name, ' ', Abbr, ' ', Description) AS Narration " &
                             "      FROM PROFILES " &
                             "  ) AS SubQuery WHERE 1=1 "

        If txtSearch.Text <> "" Then

            Dim keywords As String() = txtSearch.Text.Split(" "c)

            keywords = keywords.Where(Function(x) x <> "").ToArray()

            For Each keyword As String In keywords
                sSql &= "AND Narration LIKE '%" & keyword & "%' "
            Next

        End If
        sSql += " order by Name"

        Dim cnpl As New SqlConnection(connString)
        cnpl.Open()
        Dim cmdpl As New SqlCommand(sSql, cnpl)
        cmdpl.CommandType = CommandType.Text
        Dim drpl As SqlDataReader = cmdpl.ExecuteReader
        If drpl.HasRows Then
            While drpl.Read
                dgv.Rows.Add(drpl("ID"), drpl("Name"),
                drpl("Abbr"), drpl("Description"))
            End While
        End If
        cnpl.Close()
        cnpl = Nothing

        lbl_TotRec.Text = dgv.Rows.Count.ToString & " Record Found"

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub dgvProfiles_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex <> -1 Then btnOK.Enabled = True
    End Sub

    Private Sub frmProfLookup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbPosition.SelectedIndex = 0
        Me.Tag = ""

        If dgv.Rows.Count = 0 Then lbl_TotRec.Text = ""

        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)

        Me.txtSearch.Focus()
        Me.txtSearch.SelectionStart = 0 ' Start position of selection (beginning of text)
        Me.txtSearch.SelectionLength = Me.txtSearch.Text.Length
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Down AndAlso dgv.Rows.Count > 0 Then
            dgv.Focus()
        End If
    End Sub

    Private Sub dgv_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellDoubleClick
        Call btnOK_Click(sender, e)
    End Sub

    Private Sub dgv_KeyDown(sender As Object, e As KeyEventArgs) Handles dgv.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True

            Call btnOK_Click(sender, e)
            Me.txtSearch.Focus()

        End If
    End Sub
End Class
