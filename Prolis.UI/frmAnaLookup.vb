Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmAnaLookup

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

    Private Sub btnLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLook.Click
        'Dim sSQL As String = ""
        'If txtSearch.Text <> "" Then
        '    If cmbPosition.SelectedIndex = 0 Then
        '        sSQL = "Select * from Anas where Name like '%" & txtSearch.Text & "%' order by Name"
        '    ElseIf cmbPosition.SelectedIndex = 1 Then
        '        ssql = "Select * from Anas where Name like '" & txtSearch.Text & "%' order by Name"
        '    End If
        '    txtSearch.Text = ""
        'Else
        '    ssql = "Select * from Anas order by Name"
        'End If
        Me.Cursor = Cursors.WaitCursor

        Dim sSQL As String = "Select * from Anas where 1=1 "

        If txtSearch.Text.Trim <> "" Then
            Dim keywords As String() = txtSearch.Text.Split(" "c)

            keywords = keywords.Where(Function(x) x <> "").ToArray()

            For Each keyword As String In keywords
                sSQL &= "AND Name LIKE '%" & keyword & "%' "
            Next
            txtSearch.Text = ""
        Else
        End If
        sSQL += " order by Name"

        dgv.Rows.Clear()
        Dim cngpr As New SqlConnection(connString)
        cngpr.Open()
        Dim cmdgpr As New SqlCommand(sSQL, cngpr)
        cmdgpr.CommandType = CommandType.Text
        Dim drgpr As SqlDataReader = cmdgpr.ExecuteReader

        If drgpr.HasRows Then
            While drgpr.Read
                dgv.Rows.Add(drgpr("ID"), drgpr("Name"),
                GetEquipmentName(drgpr("Equipment_ID")),
                drgpr("Controls"), drgpr("Validaters"),
                drgpr("InRangePercent") & " %")
            End While
        End If

        cngpr.Close()
        cngpr = Nothing

        lbl_TotRec.Text = dgv.Rows.Count.ToString & " Record Found"

        Me.Cursor = Cursors.Default
    End Sub

    Private Function GetEquipmentName(ByVal EquipID As Integer) As String
        Dim EqName As String = ""
        Dim cne As New SqlConnection(connString)
        cne.Open()
        Dim cmde As New SqlCommand("Select " &
        "Name from Equipments where ID = " & EquipID, cne)
        cmde.CommandType = CommandType.Text
        Dim dre As SqlDataReader = cmde.ExecuteReader
        If dre.HasRows Then
            While dre.Read
                EqName = dre("Name")
            End While
        End If
        cne.Close()
        cne = Nothing
        Return EqName
    End Function

    Private Sub dgvAnas_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex <> -1 Then btnOK.Enabled = True
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub frmAnaLookup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbPosition.SelectedIndex = 0
        Me.Tag = ""
        dgv.Rows.Clear()
        lbl_TotRec.Text = ""

        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)

        Me.txtSearch.Clear()
        Me.txtSearch.Focus()
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
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
            Me.txtSearch.Focus()
        End If
    End Sub
End Class
