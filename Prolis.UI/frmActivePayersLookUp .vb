Imports System.Windows.Forms
Imports Microsoft.Data.SqlClient
Imports Microsoft.SqlClient

Public Class frmActivePayersLookUp

    Private Sub cmbSearch_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSearch.SelectedIndexChanged
        If cmbSearch.SelectedIndex = 0 Then
            Label1.Text = "Payer Name (even partial)"
        Else
            Label1.Text = "Payer ID (Complete)"
        End If
        txtSearch.Text = ""
    End Sub

    Friend Sub FindPayerByName(ByVal PayerName As String)
        dgvPayers.Rows.Clear()
        If PayerName <> "" Then
            PayerName = Replace(PayerName, "*", "") & "%"
            Dim cnpn As New SqlConnection(connString)
            cnpn.Open()
            Dim cmdpn As New SqlCommand("Select * from Payers " &
            "where Active <> 0 and PayerName like '" & PayerName & "'", cnpn)
            cmdpn.CommandType = CommandType.Text
            Dim drpn As SqlDataReader = cmdpn.ExecuteReader
            If drpn.HasRows Then
                While drpn.Read
                    If drpn("Address_ID") Is DBNull.Value Then
                        dgvPayers.Rows.Add(drpn("ID"),
                        drpn("PayerName"),
                        HasPayerContract(drpn("ID")), "")
                    Else
                        dgvPayers.Rows.Add(drpn("ID"), drpn("PayerName"),
                        HasPayerContract(drpn("ID")), GetAddress(drpn("Address_ID")))
                    End If
                End While
            End If
            cnpn.Close()
            cnpn = Nothing
        End If
    End Sub

    Private Sub btnPatSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatSearch.Click
        If txtSearch.Text <> "" Then
            'If cmbSearch.SelectedIndex = 0 Then
            '    FindPayerByName(txtSearch.Text)
            'Else
            '    FindPayerByID(Val(txtSearch.Text))
            'End If
            FilterData()
            'txtSearch.Text = ""
        End If
    End Sub

    Private Sub FilterData()
        Me.Cursor = Cursors.WaitCursor
        dgvPayers.Rows.Clear()

        Dim sSql As String = "SELECT * " &
                              " FROM (SELECT Payers.ID, Payers.PayerName, CASE WHEN EXISTS (SELECT 1 FROM dbo.Payer_TGP pt WHERE pt.Payer_ID = Payers.ID) THEN 1 ELSE 0 END AS HasPayerContract, Payers.Address_ID, vw_Addresses.Address, CONCAT(CONVERT(VARCHAR, Payers.ID), ' ', PayerName) AS Narration " &
                              "  , Active FROM dbo.Payers " &
                              " LEFT OUTER JOIN dbo.vw_Addresses ON Payers.Address_ID = vw_Addresses.ID) AS SubQuery " &
                              " Where Active <> 0 "

        If txtSearch.Text.Trim <> "" Then

            Dim keywords As String() = txtSearch.Text.Split(" "c)

            keywords = keywords.Where(Function(x) x <> "").ToArray()

            For Each keyword As String In keywords
                sSql &= "AND Narration LIKE '%" & keyword & "%' "
            Next
            'Else
            '    sSql = "Select * from Tests order by Name"
        End If

        Dim cn As New SqlConnection(connString)
        cn.Open()
        Dim cmdsrc As New SqlCommand(sSql, cn)
        cmdsrc.CommandType = Data.CommandType.Text
        Dim dr As SqlDataReader = cmdsrc.ExecuteReader

        If dr.HasRows Then
            While dr.Read
                dgvPayers.Rows.Add(dr("ID"), dr("PayerName"),
                         (dr("HasPayerContract")), dr("Address"))
            End While
        End If
        cn.Close()
        cn = Nothing

        lbl_TotRec.Text = dgvPayers.Rows.Count.ToString & " Record Found"

        Me.Cursor = Cursors.Default
    End Sub
    Private Sub FindPayerByID(ByVal PayerID As Long)
        dgvPayers.Rows.Clear()
        Dim cnpid As New SqlConnection(connString)
        cnpid.Open()
        Dim cmdpid As New SqlCommand("Select * " &
        "from Payers where Active <> 0 and ID = " & PayerID, cnpid)
        cmdpid.CommandType = CommandType.Text
        Dim drpid As SqlDataReader = cmdpid.ExecuteReader
        If drpid.HasRows Then
            While drpid.Read
                dgvPayers.Rows.Add(drpid("ID"), drpid("PayerName"),
                HasPayerContract(drpid("ID")), GetAddress(drpid("Address_ID")))
            End While
        End If
        cnpid.Close()
        cnpid = Nothing
    End Sub

    Private Function HasPayerContract(ByVal PayerID As Long) As Boolean
        Dim Has As Boolean = False
        Dim cnphc As New SqlConnection(connString)
        cnphc.Open()
        Dim cmdphc As New SqlCommand("Select * " &
        "from Payer_TGP where Payer_ID = " & PayerID, cnphc)
        cmdphc.CommandType = CommandType.Text
        Dim drphc As SqlDataReader = cmdphc.ExecuteReader
        If drphc.HasRows Then Has = True
        cnphc.Close()
        cnphc = Nothing
        Return Has
    End Function

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        'Me.Tag = dgvPatients.Rows(e.RowIndex).Cells(0).Value

        'If dgvPayers.SelectedRows.Count > 0 Then

        If dgvPayers.CurrentRow IsNot Nothing Then

            Dim rowIndex As Integer = dgvPayers.CurrentRow.Index

            Me.Tag = dgvPayers.Rows(rowIndex).Cells(0).Value.ToString & _
            "|" & dgvPayers.Rows(rowIndex).Cells(1).Value.ToString & _
            "|" & dgvPayers.Rows(rowIndex).Cells(2).Value.ToString

        End If

        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click, btn_Cancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If txtSearch.Text = "" Then
            btnPatSearch.Enabled = False
        Else
            btnPatSearch.Enabled = True
        End If
    End Sub

    Private Sub frmPayerLookup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbSearch.SelectedIndex = 0
        Me.Tag = ""
        dgvPayers.Rows.Clear()
        lbl_TotRec.Text = ""

        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
        Me.txtSearch.Clear()
        Me.txtSearch.Focus()
    End Sub

    Private Sub dgvPayers_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPayers.CellClick
        If e.RowIndex <> -1 Then
            Me.Tag = dgvPayers.Rows(e.RowIndex).Cells(0).Value.ToString & _
            "|" & dgvPayers.Rows(e.RowIndex).Cells(1).Value.ToString & _
            "|" & dgvPayers.Rows(e.RowIndex).Cells(2).Value.ToString
            btnAccept.Enabled = True
        Else
            Me.Tag = ""
        End If
    End Sub
    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Down AndAlso dgvPayers.Rows.Count > 0 Then
            dgvPayers.Focus()
        End If
    End Sub
    Private Sub dgvTests_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPayers.CellDoubleClick
        Call btnAccept_Click(Nothing, Nothing)
    End Sub
    Private Sub dgvTests_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvPayers.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Call btnAccept_Click(Nothing, Nothing)
            Me.txtSearch.Focus()
        End If
    End Sub

End Class
