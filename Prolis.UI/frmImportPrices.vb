Imports System.io
Imports System.Windows.Forms

Public Class frmImportPrices
    Private Delim As String = ""
    Private PARAMS(3) As String
    Private IsCancelled As Boolean = False
    Private ucs As Integer = 0

    Private Sub btnOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpen.Click
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

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
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

    Private Sub btnDesel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDesel.Click
        For i As Integer = 0 To dgvFieldMap.RowCount - 1
            dgvFieldMap.Rows(i).Cells(0).Value = False
        Next
    End Sub

    Private Sub btnSel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSel.Click
        For i As Integer = 0 To dgvFieldMap.RowCount - 1
            dgvFieldMap.Rows(i).Cells(0).Value = False
        Next
    End Sub

    Private Function FieldsMapped() As Boolean
        Dim Cond As Boolean = False
        Dim list As Boolean = False
        Dim level As Boolean = False
        For i As Integer = 0 To dgvFieldMap.RowCount - 1
            If dgvFieldMap.Rows(i).Cells(0).Value = True AndAlso _
            (InStr(dgvFieldMap.Rows(i).Cells(2).Value, "CPT") > 0 Or _
            InStr(dgvFieldMap.Rows(i).Cells(2).Value, "HCPCS") > 0) Then
                Cond = True
            ElseIf dgvFieldMap.Rows(i).Cells(0).Value = True AndAlso _
            InStr(dgvFieldMap.Rows(i).Cells(2).Value, "List") > 0 Then
                list = True
            ElseIf dgvFieldMap.Rows(i).Cells(0).Value = True AndAlso _
            InStr(dgvFieldMap.Rows(i).Cells(2).Value, "Level") > 0 Then
                level = True
            End If
        Next
        If Cond And list And level Then
            FieldsMapped = True
        Else
            FieldsMapped = False
        End If
    End Function

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        If txtFile.Text <> "" And FieldsMapped() Then
            btnProcess.Enabled = False
            Dim i As Integer
            '0=File, 1=CPT, 2=ListPrice, 3=Level1
            PARAMS(0) = Trim(txtFile.Text)
            PARAMS(1) = ""
            PARAMS(2) = ""
            PARAMS(3) = ""
            For i = 0 To dgvFieldMap.RowCount - 1
                If dgvFieldMap.Rows(i).Cells(0).Value = True And _
                (InStr(dgvFieldMap.Rows(i).Cells(2).Value, "CPT") > 0 Or _
                InStr(dgvFieldMap.Rows(i).Cells(2).Value, "HCPCS") > 0) Then
                    PARAMS(1) = i.ToString
                ElseIf dgvFieldMap.Rows(i).Cells(0).Value = True And _
                InStr(dgvFieldMap.Rows(i).Cells(2).Value, "List") > 0 Then
                    PARAMS(2) = i.ToString
                ElseIf dgvFieldMap.Rows(i).Cells(0).Value = True And _
                InStr(dgvFieldMap.Rows(i).Cells(2).Value, "Level") > 0 Then
                    PARAMS(3) = i.ToString
                End If
                If PARAMS(0) <> "" And PARAMS(1) <> "" And _
                PARAMS(2) <> "" And PARAMS(3) <> "" Then Exit For
            Next
            If PARAMS(0) <> "" And PARAMS(1) <> "" And _
            PARAMS(2) <> "" And PARAMS(3) <> "" Then
                'BW.RunWorkerAsync()
                Dim Retval As Integer = ImportFromFile()
                txtFile.Text = ""
                dgvFieldMap.Rows.Clear()
                cmbDelimiter.SelectedIndex = -1
                MsgBox("Updated records: " & ucs.ToString & ".")
            Else
                MsgBox("Invalid selection.", MsgBoxStyle.Critical, "Prolis")
            End If
        End If
    End Sub

    Private Function ImportFromFile() As Integer
        Dim LogoutMins As Integer = ThisUser.LogoutMins
        ThisUser.LogoutMins = 10080
        Dim n As Long = 0

        Dim RetVal As Integer = 0
        Dim Per As Integer = 0
        Dim lineCount As Long = File.ReadAllLines(PARAMS(0)).Length
        Dim SR As New StreamReader(PARAMS(0))
        Dim Ln As String ' Going to hold one line at a time
        ucs = 0
        Do Until SR.EndOfStream
            'If Not BW.CancellationPending Then
            Ln = SR.ReadLine
            If Ln IsNot Nothing AndAlso Ln <> "" Then
                Dim Fields() As String = Split(Ln, Delim)
                If PARAMS(1) <> "" And PARAMS(2) <> "" And PARAMS(3) <> "" Then
                    If IsNumeric(Fields(CInt(PARAMS(2)))) And IsNumeric(Fields(CInt(PARAMS(3)))) Then
                        RetVal = UpdateComponent(Fields(CInt(PARAMS(1))), Val(Fields(CInt(PARAMS(2)))), Val(Fields(CInt(PARAMS(3)))))
                        If RetVal = 0 Then ucs += 1
                    End If
                End If
                Per = n * 100 / lineCount
                PB.Value = Per
                lblStatus.Text = Per.ToString & " %"
                My.Application.DoEvents()
                'BW.ReportProgress(Per, Per.ToString & " %")
                n += 1
            End If
            'Else
            'Exit Do
            'End If
        Loop
        SR.Close()
        SR = Nothing
        ThisUser.LogoutMins = LogoutMins
        Return ucs
    End Function

    Private Function UpdateComponent(ByVal CPT As String, ByVal ListPrice As Single, ByVal Level1 As Single) As Integer
        Dim retval As Integer = 0
        Try
            If CPT <> "" And ListPrice > 0 And Level1 > 0 Then
                ExecuteSqlProcedure("Update Tests set ListPrice = " & ListPrice & _
                ", Price1 = " & Level1 & " where CPT_Code = '" & CPT & "'")
                '
                ExecuteSqlProcedure("Update Groups set ListPrice = " & ListPrice & _
                ", Price1 = " & Level1 & " where CPT_Code = '" & CPT & "'")
                '
                ExecuteSqlProcedure("Update Profiless set ListPrice = " & ListPrice & _
                ", Price1 = " & Level1 & " where CPT_Code = '" & CPT & "'")
            End If
            retval = 0
        Catch ex As Exception
            retval = 1
        End Try
        Return retval
    End Function

    Private Sub frmImportPrices_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cmbDelimiter.SelectedIndex = 0
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        IsCancelled = True
        If BW.IsBusy Then
            BW.CancelAsync()
        Else
            Me.Close()
        End If
    End Sub

    Private Sub cmbDelimiter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDelimiter.SelectedIndexChanged
        If cmbDelimiter.SelectedIndex = 0 Then
            Delim = ","
        ElseIf cmbDelimiter.SelectedIndex = 1 Then
            Delim = "|"
        Else
            Delim = Chr(9)
        End If
    End Sub

    Private Sub dgvFieldMap_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvFieldMap.CellValidated
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
        If dgvFieldMap.RowCount >= goods And goods >= bads Then
            btnProcess.Enabled = True
        Else
            btnProcess.Enabled = False
        End If
    End Sub

    Private Sub BW_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BW.DoWork
        ImportFromFile()
    End Sub

    Private Sub BW_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BW.ProgressChanged
        PB.Value = e.ProgressPercentage
        lblStatus.Text = e.UserState
    End Sub

    Private Sub BW_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BW.RunWorkerCompleted
        txtFile.Text = ""
        dgvFieldMap.Rows.Clear()
        cmbDelimiter.SelectedIndex = -1
        MsgBox("Updated records: " & ucs & ".")
    End Sub
End Class