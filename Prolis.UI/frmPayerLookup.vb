Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmPayerLookup

    Private Sub cmbSearch_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSearch.SelectedIndexChanged
        If cmbSearch.SelectedIndex = 0 Then
            Label1.Text = "Payer Name (even partial)"
        Else
            Label1.Text = "Payer ID (Complete)"
        End If
        txtSearch.Text = ""
    End Sub

    Friend Sub FindPayerByName(ByVal PayerName As String)
        dgv.Rows.Clear()
        If PayerName <> "" Then
            PayerName = Replace(PayerName, "*", "")
            PayerName = Replace(PayerName, "'", "''") & "%"
            Dim cnfp As New SqlConnection(connString)
            cnfp.Open()
            Dim cmdfp As New SqlCommand("Select * " &
            "from Payers where PayerName like '" & PayerName & "'", cnfp)
            cmdfp.CommandType = CommandType.Text
            Dim drfp As SqlDataReader = cmdfp.ExecuteReader
            If drfp.HasRows Then
                While drfp.Read
                    If drfp("Address_ID") Is DBNull.Value Then
                        dgv.Rows.Add(drfp("ID"), drfp("PayerName"),
                        HasPayerContract(drfp("ID")), "")
                    Else
                        dgv.Rows.Add(drfp("ID"), drfp("PayerName"),
                        HasPayerContract(drfp("ID")), GetAddress(drfp("Address_ID")))
                    End If
                End While
            End If
            cnfp.Close()
            cnfp = Nothing
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
        dgv.Rows.Clear()

        Dim sSql As String = "SELECT * " &
                              " FROM (SELECT Payers.ID, Payers.PayerName, CASE WHEN EXISTS (SELECT 1 FROM dbo.Payer_TGP pt WHERE pt.Payer_ID = Payers.ID) THEN 1 ELSE 0 END AS HasPayerContract, Payers.Address_ID, vw_Addresses.Address, CONCAT(CONVERT(VARCHAR, Payers.ID), ' ', PayerName) AS Narration " &
                              " FROM dbo.Payers " &
                              " LEFT OUTER JOIN dbo.vw_Addresses ON Payers.Address_ID = vw_Addresses.ID) AS SubQuery " &
                              " WHERE 1=1 "

        If txtSearch.Text.Trim <> "" Then

            Dim keywords As String() = txtSearch.Text.Split(" "c)

            keywords = keywords.Where(Function(x) x <> "").ToArray()

            For Each keyword As String In keywords
                sSql &= " AND Narration LIKE '%" & keyword & "%' "
            Next
            'Else
            '    sSql = "Select * from Tests order by Name"
        End If

        sSql += " order by PayerName"

        Dim cn As New SqlConnection(connString)
        cn.Open()
        Dim cmdsrc As New SqlCommand(sSql, cn)
        cmdsrc.CommandType = CommandType.Text
        Dim dr As SqlDataReader = cmdsrc.ExecuteReader

        If dr.HasRows Then
            While dr.Read
                dgv.Rows.Add(dr("ID"), dr("PayerName"),
                         (dr("HasPayerContract")), dr("Address"))

            End While

        End If
        cn.Close()
        cn = Nothing

        lbl_TotRec.Text = dgv.Rows.Count.ToString & " Record Found"

    End Sub

    Private Sub FindPayerByID(ByVal PayerID As Long)
        dgv.Rows.Clear()
        Dim cnfp As New SqlConnection(connString)
        cnfp.Open()
        Dim cmdfp As New SqlCommand("Select * " &
        "from Payers where ID = " & PayerID, cnfp)
        cmdfp.CommandType = CommandType.Text
        Dim drfp As SqlDataReader = cmdfp.ExecuteReader
        If drfp.HasRows Then
            While drfp.Read
                If drfp("Address_ID") Is DBNull.Value Then
                    dgv.Rows.Add(drfp("ID"), drfp("PayerName"),
                    HasPayerContract(drfp("ID")), "")
                Else
                    dgv.Rows.Add(drfp("ID"), drfp("PayerName"),
                    HasPayerContract(drfp("ID")), GetAddress(drfp("Address_ID")))
                End If
            End While
        End If
        cnfp.Close()
        cnfp = Nothing
    End Sub

    Private Function HasPayerContract(ByVal PayerID As Long) As Boolean
        Dim Has As Boolean = False
        Dim cnfp As New SqlConnection(connString)
        cnfp.Open()
        Dim cmdfp As New SqlCommand("Select * " &
        "from Payer_TGP where Payer_ID = " & PayerID, cnfp)
        cmdfp.CommandType = CommandType.Text
        Dim drfp As SqlDataReader = cmdfp.ExecuteReader
        If drfp.HasRows Then Has = True
        cnfp.Close()
        cnfp = Nothing
        Return Has
    End Function

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function
    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        'Me.Tag = dgvPatients.Rows(e.RowIndex).Cells(0).Value

        If dgv.CurrentRow IsNot Nothing Then

            Dim rowIndex As Integer = dgv.CurrentRow.Index

            Me.Tag = dgv.Rows(rowIndex).Cells(0).Value.ToString & _
            "|" & dgv.Rows(rowIndex).Cells(1).Value.ToString & _
            "|" & dgv.Rows(rowIndex).Cells(2).Value.ToString

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
        dgv.Rows.Clear()
        lbl_TotRec.Text = ""

        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
        Me.txtSearch.Clear()
        Me.txtSearch.Focus()
    End Sub

    Private Sub dgv_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex <> -1 Then
            Me.Tag = dgv.Rows(e.RowIndex).Cells(0).Value.ToString & _
            "|" & dgv.Rows(e.RowIndex).Cells(1).Value.ToString & _
            "|" & dgv.Rows(e.RowIndex).Cells(2).Value.ToString
            btnAccept.Enabled = True
        Else
            Me.Tag = ""
        End If
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Down AndAlso dgv.Rows.Count > 0 Then
            dgv.Focus()
        End If
    End Sub
    Private Sub dgvTests_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellDoubleClick
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
