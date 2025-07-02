Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmInfoLookUp

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click, btn_Cancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If dgvTGs.CurrentRow IsNot Nothing Then

            Dim rowIndex As Integer = dgvTGs.CurrentRow.Index

            Me.Tag = CStr(dgvTGs.Rows(rowIndex).Cells(0).Value)
        End If
        Me.Close()
    End Sub

    Private Sub btnLook_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLook.Click
        'Dim sSQL As String = ""
        'If txtTerm.Text <> "" Then
        '    If cmbPosition.SelectedIndex = 0 Then
        '        sSQL = "Select ID, Name, Abbr, Description, ComponentType from Tests" _
        '        & " where PreAnalytical <> 0 and (Name like '%" & txtTerm.Text & "%' OR " _
        '        & "Abbr like '%" & txtTerm.Text & "%' OR Description like '%" & _
        '        txtTerm.Text & "%')  order by Name"
        '    ElseIf cmbPosition.SelectedIndex = 1 Then
        '        sSQL = "Select ID, Name, Abbr, Description, ComponentType from Tests" _
        '        & " where PreAnalytical <> 0 and (Name like '" & txtTerm.Text & "%' OR " & _
        '        "Abbr like '" & txtTerm.Text & "%' OR Description like '" & txtTerm.Text & _
        '        "%') order by Name"
        '    End If
        '    txtTerm.Text = ""
        'Else
        '    sSQL = "Select ID, Name, Abbr, Description, ComponentType from Tests " & _
        '    "where PreAnalytical <> 0 and IsActive <> 0 order by Name"
        'End If
        Me.Cursor = Cursors.WaitCursor

        dgvTGs.Rows.Clear()

        Dim sSQL As String = "SELECT * FROM (" &
                              " Select ID, Name, Abbr, Description, ComponentType, " &
                              " CONCAT(CONVERT(VARCHAR, ID), ' ', Name, ' ',Abbr, ' ',Description) AS Narration " &
                              " from Tests where PreAnalytical <> 0 ) AS SUBQUERY Where 1=1 "

        If txtTerm.Text.Trim <> "" Then

            Dim keywords As String() = txtTerm.Text.Split(" "c)

            keywords = keywords.Where(Function(x) x <> "").ToArray()

            For Each keyword As String In keywords
                sSQL &= " AND Narration LIKE '%" & keyword & "%' "
            Next

        End If

        sSQL += " order by Name"

        Dim cninf As New SqlConnection(connString)
        cninf.Open()
        Dim cmdinf As New SqlCommand(sSQL, cninf)
        cmdinf.CommandType = CommandType.Text
        Dim drinf As SqlDataReader = cmdinf.ExecuteReader

        Dim count As Integer = 0

        If drinf.HasRows Then
            While drinf.Read
                dgvTGs.Rows.Add(drinf("ID"), IIf(drinf("ComponentType") = "T",
                System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Test.Ico"),
                IIf(drinf("ComponentType") = "G",
                System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Group.Ico"),
                System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Profile.Ico"))),
                drinf("Name"), drinf("Abbr"), drinf("Description"))
                count += 1
            End While

        End If
        cninf.Close()
        cninf = Nothing

        lbl_TotRec.Text = dgvTGs.Rows.Count.ToString & " Record Found"

        Me.Cursor = Cursors.Default
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub frmInfoLookUp_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cmbPosition.SelectedIndex = 0
        Me.Tag = ""
        dgvTGs.Rows.Clear()
        lbl_TotRec.Text = ""

        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)

        Me.txtTerm.Clear()
        Me.txtTerm.Focus()
    End Sub
    Private Sub dgvTGs_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTGs.CellClick
        If e.RowIndex <> -1 Then
            Me.Tag = CStr(dgvTGs.Rows(e.RowIndex).Cells(0).Value)
            btnOK.Enabled = True
        Else
            Me.Tag = ""
        End If
    End Sub
    Private Sub txtTerm_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTerm.KeyDown
        If e.KeyCode = Keys.Down AndAlso dgvTGs.Rows.Count > 0 Then
            dgvTGs.Focus()
        End If
    End Sub
    Private Sub dgvTGs_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTGs.CellDoubleClick
        Call btnOK_Click(Nothing, Nothing)
    End Sub
    Private Sub dgvTGs_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvTGs.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Call btnOK_Click(Nothing, Nothing)
            Me.txtTerm.Focus()
        End If
    End Sub

End Class
