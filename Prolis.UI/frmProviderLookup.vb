Imports System.Windows.Forms
Imports Microsoft.Data.SqlClient

Public Class frmProviderLookup
    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        'frmRequisitions.DisplayProvider(Val(Me.Tag))
        'Me.Tag = ""
        If dgv.SelectedRows.Count > 0 Then

            Me.Tag = dgv.SelectedRows(0).Cells(0).Value
        End If

        Me.Close()
    End Sub

    Private Sub dgvProviders_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex <> -1 Then
            Me.Tag = dgv.Rows(e.RowIndex).Cells(0).Value.ToString
            btnAccept.Enabled = True
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click, btn_Cancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub frmProviderLookup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Tag = ""
        txtSearch.Text = ""
        dgv.Rows.Clear()

        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)

        Me.txtSearch.Focus()
        'Me.txtSearch.SelectionStart = 0 ' Start position of selection (beginning of text)
        'Me.txtSearch.SelectionLength = Me.txtSearch.Text.Length

    End Sub

    Private Sub btnOrdMgmt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOrdMgmt.Click
        frmAddProvider.ShowDialog()
        'frmAddProvider.MdiParent = ProlisQC
    End Sub
    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub btnOrdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOrdSearch.Click
        Me.Cursor = Cursors.WaitCursor

        If txtSearch.Text <> "" Then
            Dim sSQL As String = ""
            Dim LastName As String = ""
            Dim FirstName As String = ""
            Dim Name() As String
            Dim Address As String = ""
            '
            If InStr(txtSearch.Text, ",") <> 0 Then
                Name = Split(txtSearch.Text, ",")
                LastName = Sanitize(Name(0))
                FirstName = Sanitize(Name(1))
            Else
                LastName = Sanitize(txtSearch.Text)
            End If
            If FirstName = "" Then
                sSQL = "Select a.ID as ID, a.LastName_BSN as LastName, IsNull(a.FirstName, '') as " & _
                "FirstName, IsNull(b.Address1 + ' ' + b.Address2 + ', ' + b.City + ', ' + b.State + " & _
                "' ' + ltrim(rtrim(b.Zip)) + ' ' + b.Country, '') as [Address] from Providers a Left " & _
                "outer join Addresses b on b.ID = a.Address_ID where a.LastName_BSN like '" & LastName & _
                "' order by a.LastName_BSN"
            Else
                sSQL = "Select a.ID as ID, a.LastName_BSN as LastName, IsNull(a.FirstName, '') as " & _
                "FirstName, IsNull(b.Address1 + ' ' + b.Address2 + ', ' + b.City + ', ' + b.State + " & _
                "' ' + ltrim(rtrim(b.Zip)) + ' ' + b.Country, '') as [Address] from Providers a Left " & _
                "outer join Addresses b on b.ID = a.Address_ID where a.LastName_BSN like '" & LastName & _
                "' and a.FirstName like '" & FirstName & "' order by a.LastName_BSN, a.FirstName"
            End If
            dgv.Rows.Clear()
            Dim cnSrch As New SqlConnection(connString)
            cnSrch.Open()
            Dim cmdSrch As New SqlCommand(sSQL, cnSrch)
            cmdSrch.CommandType = Data.CommandType.Text
            Dim drSrch As SqlDataReader = cmdSrch.ExecuteReader
            If drSrch.HasRows Then
                While drSrch.Read
                    dgv.Rows.Add(drSrch("ID"), drSrch("LastName"), drSrch("FirstName"), drSrch("Address"))
                End While
            End If
            cnSrch.Close()
            cnSrch = Nothing
            txtSearch.Text = ""
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Function Sanitize(ByVal STR As String) As String
        STR = Trim(STR)
        If InStr(STR, "drop table") > 0 Or InStr(STR, "xp_") > 0 Then
            STR = ""
        Else
            STR = Replace(STR, ";", "")
            STR = Replace(STR, "\", "")
            STR = Replace(STR, "*", "")
            STR = STR & "%"
        End If
        Sanitize = STR
    End Function
    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
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
            Me.txtSearch.Focus()
        End If
    End Sub
End Class
