Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmTimeSensitive

    Private Sub frmTimeSensitive_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SaveTimeSensitives()
    End Sub

    Private Sub frmTimeSensitive_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PopulateTimeSensitives()
    End Sub

    Private Sub PopulateTimeSensitives()
        dgvTGPs.Rows.Clear()
        Dim cnts As New SqlConnection(connString)
        cnts.Open()
        Dim cmdts As New SqlCommand("Select * from " &
        "TimeSensitiveTests where Company_ID = " & MyLab.ID, cnts)
        cmdts.CommandType = CommandType.Text
        Dim drts As SqlDataReader = cmdts.ExecuteReader
        If drts.HasRows Then
            While drts.Read
                dgvTGPs.Rows.Add(Nothing, drts("TGP_ID"), Nothing, GetTGPName(drts("TGP_ID")))
                dgvTGPs.Rows(dgvTGPs.RowCount - 1).Cells(0).Value = Drawing.Image.FromFile( _
                My.Application.Info.DirectoryPath & "\Images\Eraser.ico")
            End While
        End If
        cnts.Close()
        cnts = Nothing
        dgvTGPs.Rows.Add()
    End Sub

    Private Sub SaveTimeSensitives()
        Dim TGPIDs As String = ""
        ExecuteSqlProcedure("Delete from TimeSensitiveTests where Company_ID = " & MyLab.ID)
        For i = 0 To dgvTGPs.RowCount - 1
            If dgvTGPs.Rows(i).Cells(1).Value IsNot Nothing AndAlso _
            dgvTGPs.Rows(i).Cells(1).Value.ToString <> "" Then
                ExecuteSqlProcedure("If not Exists(Select * from TimeSensitiveTests where " & _
                "Company_ID = " & MyLab.ID & " and TGP_ID = " & dgvTGPs.Rows(i).Cells(1).Value & _
                ") Insert into TimeSensitiveTests (Company_ID, TGP_ID, LastEditedOn, EditedBy) " & _
                "Values (" & MyLab.ID & ", " & dgvTGPs.Rows(i).Cells(1).Value & ", '" & Date.Now & _
                "', " & ThisUser.ID & ")")
                '
                TGPIDs += dgvTGPs.Rows(i).Cells(1).Value.ToString & ", "
            End If
        Next
        If TGPIDs.EndsWith(", ") Then TGPIDs = TGPIDs.Substring(0, TGPIDs.Length - 2)
        If TGPIDs <> "" Then
            ExecuteSqlProcedure("Delete from TimeSensitiveTests " & _
            "where Company_ID = " & MyLab.ID & " and not TGP_ID in (" & TGPIDs & ")")
        End If
    End Sub

    Private Sub dgvTGPs_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTGPs.CellContentClick
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
