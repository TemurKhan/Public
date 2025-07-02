Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmPartRPTQualifiers

    Private Sub frmPartRPTQualifiers_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SavePartQualifiers()
    End Sub

    Private Sub frmPartRPTQualifiers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PopulatePartQualifiers()
    End Sub

    Private Sub PopulatePartQualifiers()
        dgvTGPs.Rows.Clear()
        Dim cnpq As New SqlConnection(connString)
        cnpq.Open()
        Dim cmdpq As New SqlCommand("Select * from " &
        "Partial_Qualifiers where Company_ID = " & MyLab.ID, cnpq)
        cmdpq.CommandType = CommandType.Text
        Dim drpq As SqlDataReader = cmdpq.ExecuteReader
        If drpq.HasRows Then
            While drpq.Read
                dgvTGPs.Rows.Add(Nothing, drpq("TGP_ID"), _
                Nothing, GetTGPName(drpq("TGP_ID")))
                dgvTGPs.Rows(dgvTGPs.RowCount - 1).Cells(0).Value = Drawing.Image.FromFile( _
                My.Application.Info.DirectoryPath & "\Images\Eraser.ico")
            End While
        End If
        cnpq.Close()
        cnpq = Nothing
        dgvTGPs.Rows.Add()
    End Sub

    Private Sub SavePartQualifiers()
        Dim i As Integer
        ExecuteSqlProcedure("Delete from Partial_Qualifiers where Company_ID = " & MyLab.ID)
        For i = 0 To dgvTGPs.RowCount - 1
            If dgvTGPs.Rows(i).Cells(1).Value IsNot Nothing AndAlso _
            Trim(dgvTGPs.Rows(i).Cells(1).Value.ToString) <> "" Then
                ExecuteSqlProcedure("Insert into Partial_Qualifiers (Company_ID, TGP_ID, " & _
                "LastEditedOn, EditedBy) values (" & MyLab.ID & ", " & dgvTGPs.Rows(i).Cells(1).Value & _
                ", '" & Date.Now & "', " & ThisUser.ID & ")")
            End If
        Next
    End Sub

    Private Sub dgvTGPs_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTGPs.CellContentClick
        If e.ColumnIndex = 0 Then   'Delete
            Dim RetVal As Integer
            If dgvTGPs.Rows(e.RowIndex).Cells(1).Value IsNot Nothing _
            AndAlso dgvTGPs.Rows(e.RowIndex).Cells(1).Value.ToString <> "" _
            AndAlso dgvTGPs.Rows(e.RowIndex).Cells(3).Value <> "" Then
                RetVal = MsgBox("Are you certain to delete this component?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
                If RetVal = vbYes Then
                    If e.RowIndex = dgvTGPs.RowCount - 1 Then   'Last
                        dgvTGPs.Rows(e.RowIndex).Cells(0).Value = Drawing.Image.FromFile( _
                        My.Application.Info.DirectoryPath & "\Images\Blank.ico")
                        dgvTGPs.Rows(e.RowIndex).Cells(1).Value = ""
                        dgvTGPs.Rows(e.RowIndex).Cells(3).Value = ""
                    Else
                        dgvTGPs.Rows.RemoveAt(e.RowIndex)
                    End If
                End If
            End If
        ElseIf e.ColumnIndex = 2 Then   'LookUp
            Dim TGPID As String = frmTGPLookup.ShowDialog
            If TGPID <> "" Then
                dgvTGPs.Rows(e.RowIndex).Cells(0).Value = Drawing.Image.FromFile( _
                My.Application.Info.DirectoryPath & "\Images\Eraser.ico")
                dgvTGPs.Rows(e.RowIndex).Cells(1).Value = Val(TGPID)
                dgvTGPs.Rows(e.RowIndex).Cells(3).Value = GetTGPName(Val(TGPID))
                If e.RowIndex = dgvTGPs.RowCount - 1 _
                Then dgvTGPs.Rows.Add()
            End If
        End If
    End Sub

    Private Sub dgvTGPs_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTGPs.CellEndEdit
        If e.ColumnIndex = 1 Then   'ID
            If dgvTGPs.Rows(e.RowIndex).Cells(1).Value IsNot Nothing _
            AndAlso Trim(dgvTGPs.Rows(e.RowIndex).Cells(1).Value) <> "" Then
                dgvTGPs.Rows(e.RowIndex).Cells(3).Value = GetTGPName(dgvTGPs.Rows(e.RowIndex).Cells(1).Value)
                If dgvTGPs.Rows(e.RowIndex).Cells(3).Value = "" Then
                    MsgBox("Invalid code", MsgBoxStyle.Critical, "Prolis")
                    dgvTGPs.Rows(e.RowIndex).Cells(1).Value = ""
                Else
                    If Not TGPInList(dgvTGPs.Rows(e.RowIndex).Cells(1).Value) Then
                        dgvTGPs.Rows(e.RowIndex).Cells(0).Value = Drawing.Image.FromFile( _
                        My.Application.Info.DirectoryPath & "\Images\Eraser.ico")
                        If e.RowIndex = dgvTGPs.RowCount - 1 _
                        Then dgvTGPs.Rows.Add()
                    Else
                        MsgBox("Duplicate entries not allowed", MsgBoxStyle.Critical, "Prolis")
                        dgvTGPs.Rows(e.RowIndex).Cells(1).Value = ""
                        dgvTGPs.Rows(e.RowIndex).Cells(3).Value = ""
                        dgvTGPs.Rows(e.RowIndex).Cells(0).Value = Drawing.Image.FromFile( _
                        My.Application.Info.DirectoryPath & "\Images\Blank.ico")
                    End If
                End If
            End If
        End If
    End Sub

    Private Function TGPInList(ByVal TGPID As Integer) As Boolean
        Dim CNT As Integer = 0
        For i As Integer = 0 To dgvTGPs.RowCount - 1
            If dgvTGPs.Rows(i).Cells(1).Value IsNot Nothing AndAlso _
            Val(dgvTGPs.Rows(i).Cells(1).Value) = TGPID Then CNT += 1
        Next
        If CNT > 1 Then
            TGPInList = True
        Else
            TGPInList = False
        End If
    End Function
End Class
