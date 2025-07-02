Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmMedsLookUp

    Private Sub frmMedsLookUp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.txtSearch.Clear()
        Me.txtSearch.Focus()

        PopulateMeds()
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub PopulateMeds()
        Me.Cursor = Cursors.WaitCursor

        Dim sSQL As String = "Select AlternateNames from Tests where IsActive <> 0 "

        If txtSearch.Text.Trim <> "" Then

            Dim keywords As String() = txtSearch.Text.Split(" "c)

            keywords = keywords.Where(Function(x) x <> "").ToArray()

            For Each keyword As String In keywords
                sSQL &= " AND AlternateNames LIKE '%" & keyword & "%' "
            Next

        End If

        dgvMeds.Rows.Clear()
        Dim PipedMeds As String = ""
        Dim cnmd As New SqlConnection(connString)
        cnmd.Open()
        Dim cmdmd As New SqlCommand(sSQL, cnmd)
        cmdmd.CommandType = CommandType.Text
        Dim drmd As SqlDataReader = cmdmd.ExecuteReader

        If drmd.HasRows Then
            While drmd.Read
                If drmd("AlternateNames") IsNot DBNull.Value _
                AndAlso drmd("AlternateNames") <> "" Then
                    If Not Trim(PipedMeds).EndsWith("|") Then PipedMeds += "|"
                    PipedMeds += drmd("AlternateNames")
                End If

            End While

        End If
        cnmd.Close()
        cnmd = Nothing
        '
        Dim Meds() As String = Split(PipedMeds, "|")
        Meds = RemoveDuplicates(Meds)
        Array.Sort(Meds)
        For i As Long = 0 To Meds.Length - 1
            If Trim(Meds(i)) <> "" Then
                dgvMeds.Rows.Add(Trim(Meds(i)).Replace("""", ""))
            End If
        Next

        lbl_TotRec.Text = dgvMeds.Rows.Count.ToString + " Record Found"
        Me.Cursor = Cursors.Default

        'dgvMeds.Sort(dgvMeds.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
    End Sub

    Private Function IsMedDuplicate(ByVal Med As String) As Boolean
        Dim i As Integer
        Dim CT As Integer = 0
        For i = 0 To dgvMeds.RowCount - 1
            If dgvMeds.Rows(i).Cells(0).Value = Med Then CT += 1
        Next
        If CT > 0 Then
            IsMedDuplicate = True
        Else
            IsMedDuplicate = False
        End If
    End Function

    Private Sub dgvMeds_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMeds.CellClick
        If e.RowIndex <> -1 Then
            btnAccept.Enabled = True
            Me.Tag = dgvMeds.Rows(e.RowIndex).Cells(0).Value
        Else
            btnAccept.Enabled = False
            Me.Tag = ""
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click, btn_Cancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub btnAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        Me.Close()
    End Sub

    Private Sub dgvMeds_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvMeds.CellDoubleClick
        Call btnAccept_Click(sender, e)
    End Sub

    Private Sub txtTerm_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Down AndAlso dgvMeds.Rows.Count > 0 Then
            dgvMeds.Focus()
        End If
    End Sub

    Private Sub dgvMeds_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvMeds.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            If dgvMeds.CurrentRow IsNot Nothing Then

                Dim rowIndex As Integer = dgvMeds.CurrentRow.Index

                Me.Tag = dgvMeds.Rows(rowIndex).Cells(0).Value.ToString
                Call btnAccept_Click(sender, e)
            End If
        End If

    End Sub

    Private Sub btnLook_Click(sender As Object, e As EventArgs) Handles btnLook.Click
        PopulateMeds()
    End Sub
End Class
