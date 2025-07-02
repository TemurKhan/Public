Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmTGLookup

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click, btn_Cancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If dgv.SelectedRows.Count > 0 Then
            'Dim Rs As New ADODB.Recordset
            'Rs.Open("Select * from Tests where ID = " & Val(dgv.SelectedRows(0).Cells(0).Value), CN, _
            'ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            'If Not Rs.BOF Then frmTests.DisplayTheTest(Rs)
            'If Rs.State = 1 Then Rs.Close()
            'Rs = Nothing
            Me.Tag = dgv.SelectedRows(0).Cells(0).Value
        End If
        '

        Me.Close()
    End Sub

    Private Sub btnLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLook.Click
        Me.Cursor = Cursors.WaitCursor

        dgv.Rows.Clear()
        ''Dim Rs As New ADODB.Recordset
        'Dim sSQL As String = ""
        'If txtTerm.Text <> "" Then
        '    If cmbPosition.SelectedIndex = 0 Then
        '        sSQL = "Select ID, Name, Abbr, Description, ComponentType from Tests" _
        '        & " where Name like '%" & txtTerm.Text & "%' OR Abbr like '%" & _
        '        txtTerm.Text & "%' OR Description like '%" & txtTerm.Text & "%' " & _
        '        "Union Select ID, Name, Abbr, Description, ComponentType from Groups" _
        '        & " where Name like '%" & txtTerm.Text & "%' OR Abbr like '%" & _
        '        txtTerm.Text & "%' OR Description like '%" & txtTerm.Text & _
        '        "%' order by Name"
        '    ElseIf cmbPosition.SelectedIndex = 1 Then
        '        sSQL = "Select ID, Name, Abbr, Description, ComponentType from Tests" _
        '        & " where Name like '" & txtTerm.Text & "%' OR Abbr like '" & _
        '        txtTerm.Text & "%' OR Description like '" & txtTerm.Text & _
        '        "%' Union Select ID, Name, Abbr, Description, ComponentType from " _
        '        & "Groups where Name like '" & txtTerm.Text & "%' OR Abbr like '" & _
        '        txtTerm.Text & "%' OR Description like '" & txtTerm.Text & _
        '        "%' order by Name"
        '    End If
        '    txtTerm.Text = ""
        'Else
        '    sSQL = "Select ID, Name, Abbr, Description, ComponentType from Tests " & _
        '    "Union Select ID, Name, Abbr, Description, ComponentType from Groups"
        'End If
        Dim sSQL As String = "SELECT * " & _
        " FROM (SELECT * , CONCAT(CONVERT(VARCHAR , ID) , ' ' , Name , ' ' , Abbr , ' ' , Description) AS Narration" & _
        "     FROM (SELECT ID , Name , Abbr , Description , ComponentType" & _
        "         FROM Tests" & _
        "         UNION " & _
        "         SELECT ID , Name , Abbr , Description , ComponentType" & _
        "         FROM Groups" & _
        "     ) A" & _
        " ) AS SubQuery" & _
        " WHERE 1 = 1 "

        If txtTerm.Text <> "" Then

            Dim keywords As String() = txtTerm.Text.Split(" "c)

            keywords = keywords.Where(Function(x) x <> "").ToArray()

            For Each keyword As String In keywords
                sSQL &= "AND Narration LIKE '%" & keyword & "%' "
            Next

        End If
        sSQL += " order by Name"

        Dim cntg As New SqlConnection(connString)
        cntg.Open()
        Dim cmdtg As New SqlCommand(sSQL, cntg)
        cmdtg.CommandType = CommandType.Text
        Dim drtg As SqlDataReader = cmdtg.ExecuteReader
        If drtg.HasRows Then
            While drtg.Read
                dgv.Rows.Add(drtg("ID"), _
                IIf(drtg("ComponentType") = "T", _
                System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Test.Ico"), _
                System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Group.Ico")), _
                drtg("Name"), drtg("Abbr"), drtg("Description"))
            End While
        End If
        cntg.Close()
        cntg = Nothing

        lbl_TotRec.Text = dgv.Rows.Count.ToString & " Record Found"

        Me.Cursor = Cursors.Default
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub dgv_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick  ', dgv.SelectionChanged
        If e.RowIndex <> -1 Then btnOK.Enabled = True
    End Sub

    Private Sub frmTestLookup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cmbPosition.SelectedIndex = 0
        Me.Tag = ""

        If dgv.Rows.Count = 0 Then lbl_TotRec.Text = ""

        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)

        Me.txtTerm.Focus()
        Me.txtTerm.SelectionStart = 0
        Me.txtTerm.SelectionLength = Me.txtTerm.Text.Length
    End Sub

    Private Sub txtTerm_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTerm.KeyDown
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
            Me.txtTerm.Focus()
        End If
    End Sub

End Class
