Imports System.Windows.Forms
Imports System.data

Public Class frmPartLookUp

    Private Sub frmPartLookUp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Tag = ""
        dgv.Rows.Clear()
        lbl_TotRec.Text = ""

        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
        Me.txtSearch.Clear()
        Me.txtSearch.Focus()
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgv.Rows.Clear()
        'Dim sSQL As String = "Select * from Partners "
        'If txtSearch.Text <> "" Then
        '    sSQL += " where Name like '" & txtSearch.Text & "%'"
        'End If
        Dim sSQL As String = "SELECT * " & _
                              "  FROM ( " & _
                              "      SELECT Partners.ID," & _
                              "             Partners.Name," & _
                              "             Partners.Address_ID," & _
                              "             ISNULL(vw_Addresses.Address,'') AS Address," & _
                              "             CONCAT(CONVERT(VARCHAR, Partners.ID), ' ', partners.Name) AS Narration " & _
                              "      FROM dbo.Partners " & _
                              "      LEFT OUTER JOIN dbo.vw_Addresses ON Partners.Address_ID = vw_Addresses.ID " & _
                              "  ) AS SubQuery " & _
                              "  WHERE 1=1 "

        If txtSearch.Text.Trim <> "" Then

            Dim keywords As String() = txtSearch.Text.Split(" "c)

            keywords = keywords.Where(Function(x) x <> "").ToArray()

            For Each keyword As String In keywords
                sSQL &= " AND Narration LIKE '%" & keyword & "%' "
            Next
            'Else
            '    sSql = "Select * from Tests order by Name"
        End If

        sSQL += " order by Name"

        Dim cnsp As New SqlClient.SqlConnection(connString)
        cnsp.Open()
        Dim cmdsp As New SqlClient.SqlCommand(sSQL, cnsp)
        cmdsp.CommandType = CommandType.Text
        Dim drsp As SqlClient.SqlDataReader = cmdsp.ExecuteReader
        If drsp.HasRows Then
            While drsp.Read
                dgv.Rows.Add(drsp("ID"), drsp("Name"), drsp("Address"))
            End While
        End If
        cnsp.Close()
        cnsp = Nothing

        lbl_TotRec.Text = dgv.Rows.Count.ToString & " Record Found"

        txtSearch.Text = ""
    End Sub

    Private Sub dgvPartners_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex <> -1 Then
            Me.Tag = dgv.Rows(e.RowIndex).Cells(0).Value.ToString
            btnAccept.Enabled = True
        Else
            Me.Tag = ""
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click, btn_Cancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        If dgv.CurrentRow IsNot Nothing Then

            Dim rowIndex As Integer = dgv.CurrentRow.Index

            Me.Tag = dgv.Rows(rowIndex).Cells(0).Value.ToString
        End If

        Me.Close()
    End Sub


    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Down AndAlso dgv.Rows.Count > 0 Then
            dgv.Focus()
        End If
    End Sub
    Private Sub dgvTests_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellDoubleClick
        Call btnAccept_Click(sender, e)
    End Sub
    Private Sub dgvTests_KeyDown(sender As Object, e As KeyEventArgs) Handles dgv.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Call btnAccept_Click(sender, e)
            Me.txtSearch.Focus()
        End If
    End Sub
End Class
