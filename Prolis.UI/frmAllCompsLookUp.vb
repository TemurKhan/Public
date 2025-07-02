Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmAllCompsLookUp

    Private Sub AllCompsLookUp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbPosition.SelectedIndex = 0
        Me.Tag = ""
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)

        If dgvTGs.Rows.Count = 0 Then lbl_TotRec.Text = ""

        Me.txtTerm.Focus()
        Me.txtTerm.SelectionStart = 0 ' Start position of selection (beginning of text)
        Me.txtTerm.SelectionLength = Me.txtTerm.Text.Length ' Length of selection (entire text)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click, btn_Cancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If dgvTGs.SelectedRows.Count > 0 Then

            'If dgvTGs.CurrentRow IsNot Nothing Then

            '    Dim rowIndex As Integer = dgvTGs.CurrentRow.Index

            Me.Tag = dgvTGs.SelectedRows(0).Cells(0).Value.ToString &
                        "|" & dgvTGs.SelectedRows(0).Cells(2).Value & " (" &
                        dgvTGs.SelectedRows(0).Cells(0).Value.ToString & ")"

        End If
        Me.Close()
    End Sub

    Private Sub btnLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLook.Click

        Me.Cursor = Cursors.WaitCursor

        Dim sSQL As String = "SELECT *  " &
                            "FROM (SELECT ID,                       " &
                            "             Name,                     " &
                            "             Abbr,                     " &
                            "             Description,              " &
                            "             ComponentType,            " &
                            "             CONCAT( CONVERT( VARCHAR, ID ), ' ', Name, ' ', Abbr, ' ', Description ) AS Narration " &
                            "        FROM Tests                     " &
                            "        WHERE IsActive <> 0            " &
                            "        UNION                          " &
                            "  SELECT ID,                           " &
                            "         Name,                         " &
                            "         Abbr,                         " &
                            "         Description,                  " &
                            "         ComponentType,                " &
                            "         CONCAT( CONVERT( VARCHAR, ID ), ' ', Name, ' ', Abbr, ' ', Description ) AS Narration   " &
                            "  FROM Groups                                                                                    " &
                            "  WHERE IsActive                                                                                 " &
                            "        <>0                                                                                      " &
                            "  UNION                                                                                          " &
                            "  SELECT ID,                                                                                     " &
                            "         Name,                                                                                   " &
                            "         Abbr,                                                                                   " &
                            "         Description,                                                                            " &
                            "         ComponentType,                                                                          " &
                            "         CONCAT( CONVERT( VARCHAR, ID ), ' ', Name, ' ', Abbr, ' ', Description ) AS Narration   " &
                            "  FROM Profiles                                                                                  " &
                            "  WHERE IsActive<>0) AS SubQuery                                                                 " &
                            "WHERE 1=1                                                                                         "

        If txtTerm.Text <> "" Then

            Dim keywords As String() = txtTerm.Text.Split(" "c)

            keywords = keywords.Where(Function(x) x <> "").ToArray()

            For Each keyword As String In keywords
                sSQL &= " AND Narration LIKE '%" & keyword & "%' "
            Next

        End If

        sSQL &= " order by Name"

        dgvTGs.Rows.Clear()

        'If txtTerm.Text <> "" Then
        '    If cmbPosition.SelectedIndex = 0 Then
        '        sSQL = "Select ID, Name, Abbr, Description, ComponentType " & _
        '        "from Tests where IsActive <> 0 and (Name like '%" & txtTerm.Text _
        '        & "%' OR Abbr like '%" & txtTerm.Text & "%' OR Description " & _
        '        "like '%" & txtTerm.Text & "%') " & "Union Select ID, Name, " & _
        '        "Abbr, Description, ComponentType from Groups where IsActive " & _
        '        "<> 0 and (Name like '%" & txtTerm.Text & "%' OR Abbr like '%" & _
        '        txtTerm.Text & "%' OR Description like '%" & txtTerm.Text & _
        '        "%') Union Select ID, Name, Abbr, Description, ComponentType " & _
        '        "from Profiles where IsActive <> 0 and (Name like '%" & _
        '        txtTerm.Text & "%' OR Abbr like '%" & txtTerm.Text & "%' OR " & _
        '        "Description like '%" & txtTerm.Text & "%') order by Name"
        '    ElseIf cmbPosition.SelectedIndex = 1 Then
        '        sSQL = "Select ID, Name, Abbr, Description, ComponentType " & _
        '        "from Tests where IsActive <> 0 and (Name like '" & txtTerm.Text _
        '        & "%' OR Abbr like '" & txtTerm.Text & "%' OR Description " & _
        '        "like '" & txtTerm.Text & "%') " & "Union Select ID, Name, " & _
        '        "Abbr, Description, ComponentType from Groups where IsActive " & _
        '        "<> 0 and (Name like '" & txtTerm.Text & "%' OR Abbr like '" & _
        '        txtTerm.Text & "%' OR Description like '" & txtTerm.Text & _
        '        "%') Union Select ID, Name, Abbr, Description, ComponentType " & _
        '        "from Profiles where IsActive <> 0 and (Name like '" & _
        '        txtTerm.Text & "%' OR Abbr like '" & txtTerm.Text & "%' OR " & _
        '        "Description like '" & txtTerm.Text & "%') order by Name"
        '    End If
        '    txtTerm.Text = ""
        'Else
        '    sSQL = "Select ID, Name, Abbr, Description, ComponentType from " & _
        '    "Tests where IsActive <> 0 Union Select ID, Name, Abbr, " & _
        '    "Description, ComponentType from Groups where IsActive <> 0 " & _
        '    "Union Select ID, Name, Abbr, Description, ComponentType from " & _
        '    "Profiles where IsActive <> 0"
        'End If
        Dim cn1 As New SqlConnection(connString)
        cn1.Open()
        Dim cmd1 As New SqlCommand(sSQL, cn1)
        cmd1.CommandType = CommandType.Text
        Dim dr1 As SqlDataReader = cmd1.ExecuteReader

        If dr1.HasRows Then

            While dr1.Read
                dgvTGs.Rows.Add(dr1("ID"), IIf(dr1("ComponentType") = "T", _
                System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Test.Ico"), _
                IIf(dr1("ComponentType") = "G", _
                System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Group.Ico"), _
                System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Profile.Ico"))), _
                dr1("Name"), dr1("Abbr"), dr1("Description"))

            End While

        End If
        cn1.Close()
        cn1 = Nothing

        lbl_TotRec.Text = dgvTGs.Rows.Count.ToString & " Record Found"

        Me.Cursor = Cursors.Default
    End Sub
    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub dgvTGs_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTGs.CellContentClick
        If e.RowIndex <> -1 Then
            Me.Tag = dgvTGs.Rows(e.RowIndex).Cells(0).Value.ToString & _
            "|" & dgvTGs.Rows(e.RowIndex).Cells(2).Value & " (" & _
            dgvTGs.Rows(e.RowIndex).Cells(0).Value.ToString & ")"
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

    Private Sub dgvTgs_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTGs.CellDoubleClick
        Call btnOK_Click(Nothing, Nothing)
    End Sub

    Private Sub dgvTgs_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvTGs.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True

            Call btnOK_Click(Nothing, Nothing)
            Me.txtTerm.Focus()
        End If
    End Sub
End Class
