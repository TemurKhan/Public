Imports System.Windows.Forms
Imports System.data

Public Class frmPayerMapping
    Private Sub frmPayerMapping_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PopulateSystems()
        PopulateMapping(cmbSystems.SelectedItem.ToString)
    End Sub

    Private Sub PopulateMapping(ByVal SelSystem As String)
        dgvMapping.Rows.Clear()
        Dim sSQL As String = ""
        If SelSystem = "*ALL*" Then
            ssql = "Select a.*, b.PayerName as Payer from PayerMapping a inner " & _
            "join Payers b on a.Payer_ID = b.ID order by a.ExternalSystem"
        Else
            ssql = "Select a.*, b.PayerName as Payer from PayerMapping a " & _
            "inner join Payers b on a.Payer_ID = b.ID where ExternalSystem " & _
            "= '" & SelSystem & "'"
        End If
        Dim cnpm As New SqlClient.SqlConnection(connString)
        cnpm.Open()
        Dim cmdpm As New SqlClient.SqlCommand(sSQL, cnpm)
        cmdpm.CommandType = CommandType.Text
        Dim drpm As SqlClient.SqlDataReader = cmdpm.ExecuteReader
        If drpm.HasRows Then
            While drpm.Read
                dgvMapping.Rows.Add(Image.FromFile(My.Application.Info.DirectoryPath &
                "\Images\Eraser.ico"), drpm("Payer_ID"), drpm("Payer"),
                drpm("ExternalSystem"), drpm("ExternalID"))
            End While
        End If
        cnpm.Close()
        cnpm = Nothing
        dgvMapping.Rows.Add()
    End Sub

    Private Sub PopulateSystems()
        cmbSystems.Items.Clear()
        cmbSystems.Items.Add("*ALL*")
        Dim cnps As New SqlClient.SqlConnection(connString)
        cnps.Open()
        Dim cmdps As New SqlClient.SqlCommand("Select " &
        "distinct ExternalSystem from PayerMapping", cnps)
        cmdps.CommandType = CommandType.Text
        Dim drps As SqlClient.SqlDataReader = cmdps.ExecuteReader
        If drps.HasRows Then
            While drps.Read
                cmbSystems.Items.Add(drps("ExternalSystem"))
            End While
        End If
        cnps.Close()
        cnps = Nothing
        cmbSystems.SelectedIndex = 0
    End Sub

    Private Sub dgvMapping_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMapping.CellClick
        If e.ColumnIndex = 0 Then   'Eraser
            If dgvMapping.Rows(e.RowIndex).Cells(2).Value <> "" Then    'poplated
                Dim RetVal As Integer = MsgBox("This action deletes the record, independent of " &
                "the Prolis Payer record. Are you sure you want to delete this mapping?",
                MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
                If RetVal = vbYes Then
                    ExecuteSqlProcedure("Delete from PayerMapping where Payer_ID = " &
                    dgvMapping.Rows(e.RowIndex).Cells(1).Value & " and " &
                    "ExternalSystem = '" & dgvMapping.Rows(e.RowIndex).Cells(3).Value & "'")
                    If e.RowIndex = dgvMapping.RowCount - 1 Then 'Last
                        ClearLine(e.RowIndex)
                    Else
                        dgvMapping.Rows.RemoveAt(e.RowIndex)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub ClearLine(ByVal RowIndex As Integer)
        dgvMapping.Rows(RowIndex).Cells(0).Value =
        System.Drawing.Image.FromFile(Application.StartupPath &
        "\Images\Blank.ico")
        dgvMapping.Rows(RowIndex).Cells(1).Value = ""
        dgvMapping.Rows(RowIndex).Cells(2).Value = ""
        dgvMapping.Rows(RowIndex).Cells(3).Value = ""
        dgvMapping.Rows(RowIndex).Cells(4).Value = ""
    End Sub

    Private Sub dgvMapping_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMapping.CellEndEdit
        If e.ColumnIndex = 1 Then   'ID
            If IsNumeric(Trim(dgvMapping.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)) = True Then
                Dim Payer As String =
                GetPayerName(Val(dgvMapping.Rows(e.RowIndex).Cells(1).Value))
                If Payer <> "" Then
                    dgvMapping.Rows(e.RowIndex).Cells(2).Value = Payer
                Else
                    MsgBox("Invalid ID entered.", MsgBoxStyle.Critical, "Prolis")
                    ClearLine(e.RowIndex)
                End If
            Else
                MsgBox("Payer ID in Prolis must be numeric", MsgBoxStyle.Critical, "Prolis")
                ClearLine(e.RowIndex)
            End If
        ElseIf e.ColumnIndex = 3 Then   'System
            If MappingComplete(e.RowIndex) = True Then
                SaveMappedLine(e.RowIndex)
                PopulateSystems()
                dgvMapping.Rows(e.RowIndex).Cells(0).Value =
                Image.FromFile(My.Application.Info.DirectoryPath & "\Images\Eraser.ico")
                If e.RowIndex = dgvMapping.RowCount - 1 _
                Then dgvMapping.Rows.Add()
            End If
        ElseIf e.ColumnIndex = 4 Then   'System
            If MappingComplete(e.RowIndex) = True Then
                SaveMappedLine(e.RowIndex)
                PopulateSystems()
                dgvMapping.Rows(e.RowIndex).Cells(0).Value =
                Image.FromFile(My.Application.Info.DirectoryPath & "\Images\Eraser.ico")
                If e.RowIndex = dgvMapping.RowCount - 1 _
                Then dgvMapping.Rows.Add()
            End If
        End If
    End Sub

    Private Sub SaveMappedLine(ByVal RowIndex As Integer)
        If dgvMapping.Rows(RowIndex).Cells(1).Value <> "" _
        And dgvMapping.Rows(RowIndex).Cells(2).Value <> "" _
        And Trim(dgvMapping.Rows(RowIndex).Cells(3).Value) <> "" _
        And Trim(dgvMapping.Rows(RowIndex).Cells(4).Value) <> "" Then
            ExecuteSqlProcedure("If Exists (Select * from PayerMapping where Payer_ID = " &
            Val(dgvMapping.Rows(RowIndex).Cells(1).Value) & " and ExternalSystem = '" &
            Trim(dgvMapping.Rows(RowIndex).Cells(3).Value) & "' Update PayerMapping " &
            "Set ExternalID = '" & Trim(dgvMapping.Rows(RowIndex).Cells(4).Value) &
            "' where Payer_ID = " & Val(dgvMapping.Rows(RowIndex).Cells(1).Value) & " " &
            "and ExternalSystem = '" & Trim(dgvMapping.Rows(RowIndex).Cells(3).Value) &
            "' Else Insert into PayerMapping (Payer_ID, ExternalSystem, ExternalID) " &
            "values (" & dgvMapping.Rows(RowIndex).Cells(1).Value & ", '" &
            Trim(dgvMapping.Rows(RowIndex).Cells(3).Value) & "', '" &
            Trim(dgvMapping.Rows(RowIndex).Cells(4).Value) & "')")
        End If
    End Sub

    Private Function MappingComplete(ByVal RowIndex As Integer) As Boolean
        If dgvMapping.Rows(RowIndex).Cells(1).Value <> "" _
        And dgvMapping.Rows(RowIndex).Cells(2).Value <> "" _
        And Trim(dgvMapping.Rows(RowIndex).Cells(3).Value) <> "" _
        And Trim(dgvMapping.Rows(RowIndex).Cells(4).Value) <> "" Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function GetPayerName(ByVal PayerID As Long) As String
        Dim Payer As String = ""
        Dim cnpn As New SqlClient.SqlConnection(connString)
        cnpn.Open()
        Dim cmdpn As New SqlClient.SqlCommand("Select " & _
        "PayerName from Payers where ID = " & PayerID, cnpn)
        cmdpn.CommandType = CommandType.Text
        Dim drpn As SqlClient.SqlDataReader = cmdpn.ExecuteReader
        If drpn.HasRows Then
            While drpn.Read
                Payer = drpn("PayerName")
            End While
        End If
        cnpn.Close()
        cnpn = Nothing
        Return Payer
    End Function

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        frmPayerMappingUpload.ShowDialog()
        PopulateMapping("*ALL*")
        PopulateSystems()
    End Sub

    Private Sub cmbSystems_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSystems.SelectedIndexChanged
        PopulateMapping(cmbSystems.SelectedItem.ToString)
    End Sub
End Class
