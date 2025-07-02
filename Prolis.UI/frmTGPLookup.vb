Imports System.Windows.Forms
Imports Microsoft.Data.SqlClient

Public Class frmTGPLookup

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click, btn_Cancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If dgv.SelectedRows.Count > 0 Then

            Me.Tag = dgv.SelectedRows(0).Cells(0).Value

        End If

        Me.Close()
    End Sub

    Private Sub btnLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLook.Click
        Me.Cursor = Cursors.WaitCursor

        dgv.Rows.Clear()

        'If txtTerm.Text <> "" Then
        '    If cmbPosition.SelectedIndex = 0 Then
        '        sSQL = "Select ID, Name, Abbr, Description, ComponentType from Tests" _
        '        & " where IsMarkable <> 0 and (Name like '%" & txtTerm.Text & "%' OR " _
        '        & "Abbr like '%" & txtTerm.Text & "%' OR Description like '%" & _
        '        txtTerm.Text & "%') " & _
        '        "Union Select ID, Name, Abbr, Description, ComponentType from Groups" _
        '        & " where IsMarkable <> 0 and (Name like '%" & txtTerm.Text & "%' OR" _
        '        & " Abbr like '%" & txtTerm.Text & "%' OR Description like '%" & _
        '        txtTerm.Text & "%') " & _
        '        "Union Select ID, Name, Abbr, Description, ComponentType from " & _
        '        "Profiles where IsMarkable <> 0 and (Name like '%" & txtTerm.Text & _
        '        "%' OR Abbr like '%" & txtTerm.Text & "%' OR Description like '%" _
        '        & txtTerm.Text & "%') order by Name"
        '    ElseIf cmbPosition.SelectedIndex = 1 Then
        '        sSQL = "Select ID, Name, Abbr, Description, ComponentType from Tests" _
        '        & " where IsMarkable <> 0 and (Name like '" & txtTerm.Text & "%' OR Abbr like '" & _
        '        txtTerm.Text & "%' OR Description like '" & txtTerm.Text & _
        '        "%') Union Select ID, Name, Abbr, Description, ComponentType from " _
        '        & "Groups where IsMarkable <> 0 and (Name like '" & txtTerm.Text & "%' OR Abbr like '" & _
        '        txtTerm.Text & "%' OR Description like '" & txtTerm.Text & _
        '        "%') Union Select ID, Name, Abbr, Description, ComponentType from " _
        '        & "Profiles where IsMarkable <> 0 and (Name like '" & txtTerm.Text & "%' OR Abbr like '" & _
        '        txtTerm.Text & "%' OR Description like '" & txtTerm.Text & _
        '        "%') order by Name"
        '    End If
        '    txtTerm.Text = ""
        'Else
        '    sSQL = "Select ID, Name, Abbr, Description, ComponentType from Tests " & _
        '    "where IsMarkable <> 0 and IsActive <> 0" & _
        '    "Union Select ID, Name, Abbr, Description, ComponentType from Groups " & _
        '    "where IsMarkable <> 0 and IsActive <> 0" & _
        '    "Union Select ID, Name, Abbr, Description, ComponentType from " & _
        '    "Profiles where IsMarkable <> 0 and IsActive <> 0"
        'End If
        '

        Dim sSQL As String = "SELECT * " & _
                                " FROM (SELECT *, CONCAT(CONVERT(varchar, ID), ' ', Name, ' ', Abbr, ' ', Description) AS Narration" & _
                                " FROM (SELECT ID, Name, Abbr, Description, ComponentType" & _
                                " FROM Tests" & _
                                " WHERE IsMarkable <> 0 And" & _
                                " IsActive <> 0" & _
                                " UNION " & _
                                " SELECT ID, Name, Abbr, Description, ComponentType" & _
                                " FROM Groups" & _
                                " WHERE IsMarkable <> 0 AND" & _
                                " IsActive <> 0" & _
                                " UNION" & _
                                " SELECT ID, Name, Abbr, Description, ComponentType" & _
                                " FROM Profiles" & _
                                " WHERE IsMarkable <> 0 AND " & _
                                " IsActive <> 0) A) AS SubQuery" & _
                                " WHERE 1 = 1"

        If txtTerm.Text <> "" Then

            Dim keywords As String() = txtTerm.Text.Split(" "c)

            keywords = keywords.Where(Function(x) x <> "").ToArray()

            For Each keyword As String In keywords
                sSQL &= "AND Narration LIKE '%" & keyword & "%' "
            Next

        End If

        sSQL += " order by Name"

        Dim cnsql As New SqlConnection(connString)
        cnsql.Open()
        Dim selcmd As New SqlCommand(sSQL, cnsql)
        selcmd.CommandType = Data.CommandType.Text
        Dim selDR As SqlDataReader = selcmd.ExecuteReader
        If selDR.HasRows Then
            While selDR.Read
                dgv.Rows.Add(selDR("ID"), IIf(selDR("ComponentType") = "T", _
                System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Test.Ico"), _
                IIf(selDR("ComponentType") = "G", System.Drawing.Image.FromFile(Application.StartupPath & _
                "\Images\Group.Ico"), System.Drawing.Image.FromFile(Application.StartupPath & _
                "\Images\Profile.Ico"))), selDR("Name"), selDR("Abbr"), selDR("Description"))
            End While
        End If
        cnsql.Close()
        cnsql = Nothing
        lbl_TotRec.Text = dgv.Rows.Count.ToString & " Record Found"

        Me.Cursor = Cursors.Default
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub frmTGPLookup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cmbPosition.SelectedIndex = 0
        Me.Tag = ""

        If dgv.Rows.Count = 0 Then lbl_TotRec.Text = ""

        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)

        Me.txtTerm.Focus()
        Me.txtTerm.SelectionStart = 0 ' Start position of selection (beginning of text)
        Me.txtTerm.SelectionLength = Me.txtTerm.Text.Length

    End Sub

    Private Sub dgvTGs_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex <> -1 Then
            Me.Tag = CStr(dgv.Rows(e.RowIndex).Cells(0).Value)
            btnOK.Enabled = True
        End If
    End Sub

    Private Sub txtTerm_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTerm.KeyDown
        If e.KeyCode = Keys.Down AndAlso dgv.Rows.Count > 0 Then
            dgv.Focus()
        End If
    End Sub

    Private Sub dgv_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellDoubleClick
        Call btnOK_Click(Nothing, Nothing)
    End Sub

    Private Sub dgv_KeyDown(sender As Object, e As KeyEventArgs) Handles dgv.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Call btnOK_Click(Nothing, Nothing)
            Me.txtTerm.Focus()
        End If
    End Sub

End Class
