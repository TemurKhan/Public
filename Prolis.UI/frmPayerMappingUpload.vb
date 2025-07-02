Imports System.Windows.Forms
Imports System.IO

Public Class frmPayerMappingUpload

    Private Sub btnOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpen.Click
        OpenFileDialog1.DefaultExt = "*.txt"
        OpenFileDialog1.InitialDirectory = "C:\"
        OpenFileDialog1.Filter = "*.CSV Files|*.CSV|*.TXT Files|*.TXT|*.* All Files|*.*"
        If MsgBoxResult.Ok = OpenFileDialog1.ShowDialog Then
            txtFile.Text = OpenFileDialog1.FileName
            OpenFileDialog1.Dispose()
        End If
    End Sub

    Private Sub btnDesel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDesel.Click
        Dim i As Integer
        For i = 0 To dgvFieldMap.RowCount - 1
            dgvFieldMap.Rows(i).Cells(0).Value = False
        Next
    End Sub

    Private Sub btnSel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSel.Click
        Dim i As Integer
        For i = 0 To dgvFieldMap.RowCount - 1
            dgvFieldMap.Rows(i).Cells(0).Value = True
        Next
    End Sub

    Private Sub btnLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        If txtFile.Text <> "" Then
            dgvFieldMap.Rows.Clear()
            Dim FL As New StreamReader(txtFile.Text)
            Dim Ln As String ' Going to hold one line at a time
            Dim i As Integer
            Dim Delim As String = ""
            If cmbDelim.SelectedIndex = 0 Then
                Delim = ","
            ElseIf cmbDelim.SelectedIndex = 1 Then
                Delim = "|"
            Else
                Delim = Chr(9)
            End If
            Ln = FL.ReadLine
            Dim sTMP As String = Ln
            Do Until sTMP = ""
                If sTMP.Length > 1 Then
                    If InStr(sTMP, Delim) > 0 Then
                        i += 1
                        sTMP = Microsoft.VisualBasic.Mid(sTMP, InStr(sTMP, Delim) + 1)
                    Else
                        sTMP = ""
                    End If
                End If
            Loop
            If i < 2 Then
                MsgBox("Invalid file format")
                Exit Sub
            End If
            Dim Fields() As String = Split(Ln, Delim)
            dgvFieldMap.Rows.Clear()
            For i = 0 To Fields.Length - 1
                dgvFieldMap.Rows.Add(False, Fields(i), "")
            Next
            FL.Close()
            FL = Nothing
        End If
    End Sub

    Private Sub frmPayerMappingUpload_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cmbDelim.SelectedIndex = 0
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        ' Try
        '    On Error Resume Next
        If txtFile.Text <> "" And FieldsMapped() Then
            Dim i As Integer
            Dim FR As New StreamReader(txtFile.Text)
            Do Until FR.ReadLine = Nothing
                i += 1
            Loop
            FR.Close()
            FR = Nothing
            pb.Maximum = i + 1
            Dim SR As New StreamReader(txtFile.Text)

            Dim Delim As String = ""
            'Dim P1 As Integer = 0
            'Dim P2 As Integer = 0
            'Dim P3 As Integer = 0
            If cmbDelim.SelectedIndex = 0 Then
                Delim = ","
            ElseIf cmbDelim.SelectedIndex = 1 Then
                Delim = "|"
            Else
                Delim = Chr(9)
            End If
            Dim PayerID As Integer = -1
            Dim ExtSystem As Integer = -1
            Dim ExtID As Integer = -1
            For i = 0 To dgvFieldMap.RowCount - 1
                If dgvFieldMap.Rows(i).Cells(0).Value = True Then
                    If InStr(dgvFieldMap.Rows(i).Cells(2).Value, "Payer ID") > 0 Then
                        PayerID = i
                    ElseIf InStr(dgvFieldMap.Rows(i).Cells(2).Value, "External System") > 0 Then
                        ExtSystem = i
                    ElseIf InStr(dgvFieldMap.Rows(i).Cells(2).Value, "External ID") > 0 Then
                        ExtID = i
                    End If
                End If
            Next
            i = 1
            '
            If PayerID <> -1 And ExtSystem <> -1 And ExtID <> -1 Then
                Dim Ln As String ' Going to hold one line at a time
                'Ln = SR.ReadLine
                While Not SR.EndOfStream
                    Ln = SR.ReadLine
                    pb.Value = i
                    'ioLines = ioLines & vbCrLf & Ln
                    Dim Fields() As String = Split(Ln, Delim)
                    If Fields.Length >= 3 Then
                        If IsNumeric(Fields(PayerID)) And Val(Fields(PayerID)) > 0 And _
                        (Fields(ExtSystem) <> "" Or Fields(ExtID) <> "") Then
                            UpdatePayerMapping(Fields(PayerID), Fields(ExtSystem), Fields(ExtID))
                        End If
                    End If
                    i += 1
                End While
            End If
            SR.Close()
            SR = Nothing
            txtFile.Text = ""
            dgvFieldMap.Rows.Clear()
        End If
        'Catch Ex As Exception

        'End Try
    End Sub

    Private Function FieldsMapped() As Boolean
        Dim Mapped As Integer = 0
        Dim i As Integer
        For i = 0 To dgvFieldMap.RowCount - 1
            If dgvFieldMap.Rows(i).Cells(0).Value = True Then
                If InStr(dgvFieldMap.Rows(i).Cells(2).Value, "Payer ID") > 0 Then
                    Mapped += 1
                ElseIf InStr(dgvFieldMap.Rows(i).Cells(2).Value, "External System") > 0 Then
                    Mapped += 1
                ElseIf InStr(dgvFieldMap.Rows(i).Cells(2).Value, "External ID") > 0 Then
                    Mapped += 1
                End If
            End If
        Next
        If Mapped = 3 Then
            FieldsMapped = True
        Else
            FieldsMapped = False
        End If
    End Function

    Private Sub UpdatePayerMapping(ByVal PayerID As Integer, ByVal ExtSystem As String, ByVal ExtID As String)
        ExecuteSqlProcedure("If Exists (Select * from PayerMapping where Payer_ID = " & PayerID & _
        " and ExternalSystem = '" & ExtSystem & "') Update PayerMapping set ExternalID = '" & _
        Trim(ExtID) & "' where Payer_ID = " & PayerID & " and ExternalSystem = '" & ExtSystem & _
        "' Else Insert into PayerMapping (Payer_ID, ExternalSystem, ExternalID) values (" & _
        PayerID & ", '" & Trim(ExtSystem) & "', '" & Trim(ExtID) & "')")
    End Sub
End Class
