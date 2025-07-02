Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmNecCopy

    Private Sub btnLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLook.Click
        Dim sSQL As String = ""
        If txtTerm.Text <> "" Then
            If cmbPosition.SelectedIndex = 0 Then
                sSQL = "Select ID, Name, Abbr, Description, ComponentType from Tests" _
                & " where (Name like '%" & txtTerm.Text & "%' OR " _
                & "Abbr like '%" & txtTerm.Text & "%' OR Description like '%" & _
                txtTerm.Text & "%') " & _
                "Union Select ID, Name, Abbr, Description, ComponentType from Groups" _
                & " where (Name like '%" & txtTerm.Text & "%' OR" _
                & " Abbr like '%" & txtTerm.Text & "%' OR Description like '%" & _
                txtTerm.Text & "%') " & _
                "Union Select ID, Name, Abbr, Description, ComponentType from " & _
                "Profiles where (Name like '%" & txtTerm.Text & _
                "%' OR Abbr like '%" & txtTerm.Text & "%' OR Description like '%" _
                & txtTerm.Text & "%') order by Name"
            ElseIf cmbPosition.SelectedIndex = 1 Then
                sSQL = "Select ID, Name, Abbr, Description, ComponentType from Tests" _
                & " where (Name like '" & txtTerm.Text & "%' OR Abbr like '" & _
                txtTerm.Text & "%' OR Description like '" & txtTerm.Text & _
                "%') Union Select ID, Name, Abbr, Description, ComponentType from " _
                & "Groups where (Name like '" & txtTerm.Text & "%' OR Abbr like '" & _
                txtTerm.Text & "%' OR Description like '" & txtTerm.Text & _
                "%') Union Select ID, Name, Abbr, Description, ComponentType from " _
                & "Profiles where (Name like '" & txtTerm.Text & "%' OR Abbr like '" & _
                txtTerm.Text & "%' OR Description like '" & txtTerm.Text & _
                "%') order by Name"
            End If
            txtTerm.Text = ""
        Else
            sSQL = "Select ID, Name, Abbr, Description, ComponentType from Tests " & _
            "Union Select ID, Name, Abbr, Description, ComponentType from Groups " & _
            "Union Select ID, Name, Abbr, Description, ComponentType from Profiles"
        End If
        dgvTGs.Rows.Clear()
        Dim cnnec As New SqlConnection(connString)
        cnnec.Open()
        Dim cmdnec As New SqlCommand(sSQL, cnnec)
        cmdnec.CommandType = CommandType.Text
        Dim drnec As SqlDataReader = cmdnec.ExecuteReader
        If drnec.HasRows Then
            While drnec.Read
                dgvTGs.Rows.Add(drnec("ID"), _
                IIf(drnec("ComponentType") = "T", System.Drawing.Image.FromFile(Application.StartupPath & _
                "\Images\Test.Ico"), IIf(drnec("ComponentType") = "G", _
                System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Group.Ico"), _
                System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Profile.Ico"))), _
                drnec("Name"), drnec("Abbr"), drnec("Description"))
            End While
        End If
        cnnec.Close()
        cnnec = Nothing
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub frmNecCopy_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbPosition.SelectedIndex = 0
        Me.Tag = ""
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub dgvTGs_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTGs.CellClick
        If e.RowIndex <> -1 Then
            Me.Tag = CStr(dgvTGs.Rows(e.RowIndex).Cells(0).Value)
            btnOK.Enabled = True
        End If
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub
End Class
