Imports System.Windows.Forms
Imports Microsoft.Data.SqlClient
Public Class frmTestLookup
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click, btn_Cancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If dgv.SelectedRows.Count > 0 Then
            'Dim Rs As New ADODB.Recordset
            'Rs.Open("Select * from Tests where ID = " & Val(dgvTests.SelectedRows(0).Cells(0).Value), CN, _
            'ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            'If Not Rs.BOF Then frmTests.DisplayTheTest(Rs)
            'If Rs.State = 1 Then Rs.Close()
            'Rs = Nothing
            Me.Tag = dgv.SelectedRows(0).Cells(0).Value
        End If

        Me.Close()
    End Sub

    Private Sub btnLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLook.Click
        Me.Cursor = Cursors.WaitCursor

        dgv.Rows.Clear()
        'Dim sSQL As String = ""
        'If txtTerm.Text <> "" Then
        '    If cmbPosition.SelectedIndex = 0 Then
        '        ssql = "Select * from Tests where Name like '%" & txtTerm.Text & "%'" _
        '        & " OR Abbr like '%" & txtTerm.Text & "%' OR Description like '%" & _
        '        txtTerm.Text & "%' order by Name"
        '    ElseIf cmbPosition.SelectedIndex = 1 Then
        '        ssql = "Select * from Tests where Name like '" & txtTerm.Text & "%'" _
        '        & " OR Abbr like '" & txtTerm.Text & "%' OR Description like '" & _
        '        txtTerm.Text & "%' order by Name"
        '    End If
        '    txtTerm.Text = ""
        'Else
        '    ssql = "Select * from Tests order by Name"
        'End If

        Dim sSql As String = "SELECT * FROM ( " &
                              "      SELECT *, CONCAT(CONVERT(varchar, ID), ' ', Name, ' ', Abbr, ' ', Description) AS Narration " &
                              "      FROM Tests " &
                              "  ) AS SubQuery WHERE 1=1 "

        If txtTerm.Text <> "" Then

            Dim keywords As String() = txtTerm.Text.Split(" "c)

            keywords = keywords.Where(Function(x) x <> "").ToArray()

            For Each keyword As String In keywords
                sSql &= "AND Narration LIKE '%" & keyword & "%' "
            Next

        End If
        sSql += " order by Name"

        Dim cnsrc As New SqlConnection(connString)
        cnsrc.Open()
        Dim cmdsrc As New SqlCommand(sSql, cnsrc)
        cmdsrc.CommandType = Data.CommandType.Text
        Dim drsrc As SqlDataReader = cmdsrc.ExecuteReader
        If drsrc.HasRows Then
            While drsrc.Read
                dgv.Rows.Add(drsrc("ID"), drsrc("Name"),
                drsrc("Abbr"), drsrc("Description"))
            End While
        End If
        cnsrc.Close()
        cnsrc = Nothing

        lbl_TotRec.Text = dgv.Rows.Count.ToString & " Record Found"

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub dgvTests_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex <> -1 Then btnOK.Enabled = True
    End Sub

    Private Sub frmTestLookup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cmbPosition.SelectedIndex = 0
        Me.Tag = ""

        If dgv.Rows.Count = 0 Then lbl_TotRec.Text = ""

        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)

        Me.txtTerm.Focus()
        Me.txtTerm.SelectionStart = 0 ' Start position of selection (beginning of text)
        Me.txtTerm.SelectionLength = Me.txtTerm.Text.Length ' Length of selection (entire text)
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub txtTerm_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTerm.KeyDown
        If e.KeyCode = Keys.Down AndAlso dgv.Rows.Count > 0 Then
            dgv.Focus()
        End If
    End Sub

    Private Sub dgv_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellDoubleClick
        Call btnOK_Click(Nothing, Nothing)
    End Sub

    Private Sub dgvTests_KeyDown(sender As Object, e As KeyEventArgs) Handles dgv.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Call btnOK_Click(Nothing, Nothing)
            Me.txtTerm.Focus()
        End If
    End Sub
End Class
