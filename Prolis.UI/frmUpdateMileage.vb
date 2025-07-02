Imports System.IO
Imports System.Windows.Forms

Public Class frmUpdateMileage
    Private Delim As String = ""
    Private PARAMS(6) As String
    Private IsCancelled As Boolean = False

    Private Sub frmUpdateMileage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbDelimiter.SelectedIndex = 0
    End Sub

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        If cmbDelimiter.SelectedIndex = 0 Then
            OpenFileDialog1.DefaultExt = "*.CSV"
        Else
            OpenFileDialog1.DefaultExt = "*.*"
        End If
        OpenFileDialog1.InitialDirectory = "C:\"
        OpenFileDialog1.Filter = "*.CSV Files|*.CSV|*.CAP Files|*.CAP|*.* All Files|*.*"
        If MsgBoxResult.Ok = OpenFileDialog1.ShowDialog Then
            txtFile.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        If txtFile.Text <> "" Then
            Dim FL As New StreamReader(txtFile.Text)
            Dim Ln As String ' Going to hold one line at a time
            Ln = FL.ReadLine
            Dim Fields() As String = Split(Ln, Delim)
            dgvFieldMap.Rows.Clear()
            For i As Integer = 0 To Fields.Length - 1
                dgvFieldMap.Rows.Add(False, Fields(i), "")
            Next
            FL.Close()
            FL = Nothing
        End If
    End Sub

    Private Sub btnDesel_Click(sender As Object, e As EventArgs) Handles btnDesel.Click
        For i As Integer = 0 To dgvFieldMap.RowCount - 1
            dgvFieldMap.Rows(i).Cells(0).Value = False
        Next
    End Sub

    Private Sub btnSel_Click(sender As Object, e As EventArgs) Handles btnSel.Click
        For i As Integer = 0 To dgvFieldMap.RowCount - 1
            dgvFieldMap.Rows(i).Cells(0).Value = True
        Next
    End Sub

    Private Function FieldsMapped() As Boolean
        Dim mapped As Integer = 0
        For i As Integer = 0 To dgvFieldMap.RowCount - 1
            If dgvFieldMap.Rows(i).Cells(0).Value = True And _
            (InStr(dgvFieldMap.Rows(i).Cells(2).Value, "Accession") > 0 Or _
            InStr(dgvFieldMap.Rows(i).Cells(2).Value, "Orig") > 0 Or _
            InStr(dgvFieldMap.Rows(i).Cells(2).Value, "Target") > 0 Or _
            InStr(dgvFieldMap.Rows(i).Cells(2).Value, "Mileage") > 0 ) Then
                mapped += 1
            End If
        Next
        If mapped = 4 Then
            FieldsMapped = True
        Else
            FieldsMapped = False
        End If
    End Function

    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        If txtFile.Text <> "" And FieldsMapped() Then
            btnProcess.Enabled = False
            Dim i As Integer
            '0=File, 1=CondID, 2=CondType, 3=TFldID
            PARAMS(0) = Trim(txtFile.Text)
            PARAMS(1) = ""
            PARAMS(2) = ""
            PARAMS(3) = ""
            PARAMS(4) = ""
            For i = 0 To dgvFieldMap.RowCount - 1
                If dgvFieldMap.Rows(i).Cells(0).Value = True And _
                InStr(dgvFieldMap.Rows(i).Cells(2).Value, "Accession") > 0 Then
                    PARAMS(1) = i.ToString
                ElseIf dgvFieldMap.Rows(i).Cells(0).Value = True And _
                InStr(dgvFieldMap.Rows(i).Cells(2).Value, "Orig") > 0 Then
                    PARAMS(2) = i.ToString
                ElseIf dgvFieldMap.Rows(i).Cells(0).Value = True And _
                InStr(dgvFieldMap.Rows(i).Cells(2).Value, "Target") > 0 Then
                    PARAMS(3) = i.ToString
                ElseIf dgvFieldMap.Rows(i).Cells(0).Value = True And _
                InStr(dgvFieldMap.Rows(i).Cells(2).Value, "Mileage") > 0 Then
                    PARAMS(4) = i.ToString
                End If
            Next
            If PARAMS(0) <> "" And PARAMS(1) <> "" And PARAMS(2) <> "" _
            And PARAMS(3) <> "" And PARAMS(4) <> "" Then
                BW.RunWorkerAsync()
            Else
                MsgBox("Invalid selection.", MsgBoxStyle.Critical, "Prolis")
            End If
        End If
    End Sub

    Private Sub ImportFromFile()
        Dim LogoutMins As Integer = ThisUser.LogoutMins
        'ThisUser.LogoutMins = 0
        Dim n As Long = 1
        Dim Per As Integer = 0
        Dim lineCount As Long = File.ReadAllLines(PARAMS(0)).Length
        Dim SR As New StreamReader(PARAMS(0))
        Dim Ln As String ' Going to hold one line at a time
        Do Until SR.EndOfStream
            If Not BW.CancellationPending Then
                Ln = SR.ReadLine
                If Ln IsNot Nothing AndAlso Ln <> "" Then
                    Dim Fields() As String = Split(Ln, Delim)
                    '0=File, 1=CPT, 2=Dx
                    If IsNumeric(Fields(CInt(PARAMS(1)))) Then
                        UpdateReqBillables(Fields(CInt(PARAMS(1))), Fields(CInt(PARAMS(2))), _
                        Fields(CInt(PARAMS(3))), Trim(Fields(CInt(PARAMS(4)))))
                        Per = n * 100 / lineCount
                        BW.ReportProgress(Per, Per.ToString & " %")
                        n += 1
                    End If
                End If
            Else
                Exit Do
            End If
            ThisUser.LogoutMins = LogoutMins
        Loop
        SR.Close()
        SR = Nothing
        ThisUser.LogoutMins = LogoutMins
    End Sub

    Private Sub UpdateReqBillables(ByVal AccID As String, ByVal OrigCode As _
    Integer, ByVal TargCode As Integer, ByVal Mileage As Integer)
        If Trim(AccID) <> "" And OrigCode > 0 And TargCode > 0 And Mileage > 1 Then
            ExecuteSqlProcedure("Update Req_Billable set TGP_ID = " & TargCode & _
            ", Unit = " & Mileage & ", Extend = LinePrice * " & Mileage & " where " & _
            "Accession_ID = " & AccID & " and TGP_ID = " & OrigCode)
        End If
    End Sub

    Private Sub BW_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BW.DoWork
        ImportFromFile()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        IsCancelled = True
        If BW.IsBusy Then
            BW.CancelAsync()
        Else
            Me.Close()
        End If
    End Sub

    Private Sub cmbDelimiter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDelimiter.SelectedIndexChanged
        If cmbDelimiter.SelectedIndex = 0 Then
            Delim = ","
        ElseIf cmbDelimiter.SelectedIndex = 1 Then
            Delim = "|"
        Else
            Delim = Chr(9)
        End If
    End Sub

    Private Sub dgvFieldMap_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFieldMap.CellValidated
        ProcessProgress()
    End Sub

    Private Sub ProcessProgress()
        Dim bads As Integer = 0
        Dim goods As Integer = 0
        For i As Integer = 0 To dgvFieldMap.RowCount - 1
            If CType(dgvFieldMap.Rows(i).Cells(0).Value, Boolean) = True _
            And dgvFieldMap.Rows(i).Cells(2).Value IsNot Nothing _
            AndAlso dgvFieldMap.Rows(i).Cells(2).Value <> "" Then
                goods += 1
            Else
                bads += 1
            End If
        Next
        If goods >= 4 Then
            btnProcess.Enabled = True
        Else
            btnProcess.Enabled = False
        End If
    End Sub

    Private Sub BW_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BW.ProgressChanged
        Try
            PB.Value = e.ProgressPercentage
            lblStatus.Text = e.UserState
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BW_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BW.RunWorkerCompleted
        txtFile.Text = ""
        dgvFieldMap.Rows.Clear()
        cmbDelimiter.SelectedIndex = 0
    End Sub
End Class