Imports System.IO
Imports System.Windows.Forms

Public Class frmImportNec
    Private Delim As String = ""
    Private PARAMS(2) As String
    Private IsCancelled As Boolean = False

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
        Dim i As Integer
        For i = 0 To dgvFieldMap.RowCount - 1
            dgvFieldMap.Rows(i).Cells(0).Value = False
        Next
    End Sub

    Private Sub btnSel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSel.Click
        Dim i As Integer
        For i = 0 To dgvFieldMap.RowCount - 1
            dgvFieldMap.Rows(i).Cells(0).Value = True
        Next
    End Sub

    Private Function FieldsMapped() As Boolean
        Dim Cond As Boolean = False
        Dim DxFld As Boolean = False
        Dim i As Integer
        For i = 0 To dgvFieldMap.RowCount - 1
            If dgvFieldMap.Rows(i).Cells(0).Value = True And _
            (InStr(dgvFieldMap.Rows(i).Cells(2).Value, "Component") > 0 Or _
            InStr(dgvFieldMap.Rows(i).Cells(2).Value, "CPT") > 0 Or _
            InStr(dgvFieldMap.Rows(i).Cells(2).Value, "LOINC") > 0) Then
                Cond = True
            ElseIf dgvFieldMap.Rows(i).Cells(0).Value = True And _
            InStr(dgvFieldMap.Rows(i).Cells(2).Value, "Dx") > 0 Then
                DxFld = True
            End If
        Next
        If Cond And DxFld Then
            FieldsMapped = True
        Else
            FieldsMapped = False
        End If
    End Function

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        If txtFile.Text <> "" And FieldsMapped() Then
            btnProcess.Enabled = False
            Dim i As Integer
            '0=File, 1=CondID, 2=CondType, 3=TFldID
            PARAMS(0) = Trim(txtFile.Text)
            PARAMS(1) = ""
            PARAMS(2) = ""
            For i = 0 To dgvFieldMap.RowCount - 1
                If dgvFieldMap.Rows(i).Cells(0).Value = True And _
                InStr(dgvFieldMap.Rows(i).Cells(2).Value, "CPT") > 0 Then
                    PARAMS(1) = i.ToString
                ElseIf dgvFieldMap.Rows(i).Cells(0).Value = True And _
                InStr(dgvFieldMap.Rows(i).Cells(2).Value, "Dx") > 0 Then
                    PARAMS(2) = i.ToString
                End If
            Next
            If PARAMS(0) <> "" And PARAMS(1) <> "" And PARAMS(2) <> "" Then
                BW.RunWorkerAsync()
            Else
                MsgBox("Invalid selection.", MsgBoxStyle.Critical, "Prolis")
            End If
        End If
    End Sub

    Private Sub ImportFromFile()
        Dim LogoutMins As Integer = ThisUser.LogoutMins
        ThisUser.LogoutMins = 10080
        Dim n As Long = 0
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
                    UpdateComponent(Fields(CInt(PARAMS(1))), Fields(CInt(PARAMS(2))))
                    Per = n * 100 / lineCount
                    BW.ReportProgress(Per, Per.ToString & " %")
                    n += 1
                End If
            Else
                Exit Do
            End If
        Loop
        SR.Close()
        SR = Nothing
        ThisUser.LogoutMins = LogoutMins
    End Sub

    Private Function GetTGPIDByTGPID(ByVal TGPID As Integer) As String()
        Dim TempIDs() As String = {""}
        Dim sSQL As String = "Select ID from Tests where ID = " & TGPID & " Union Select ID from " & _
        "Groups where ID = " & TGPID & " Union Select ID from Profiles where ID = " & TGPID
        Dim cntgp As New Data.SqlClient.SqlConnection(connString)
        cntgp.Open()
        Dim cmdtgp As New Data.SqlClient.SqlCommand(sSQL, cntgp)
        cmdtgp.CommandType = Data.CommandType.Text
        Dim drtgp As Data.SqlClient.SqlDataReader = cmdtgp.ExecuteReader
        If drtgp.HasRows Then
            While drtgp.Read
                If TempIDs(UBound(TempIDs)) <> "" Then _
                ReDim Preserve TempIDs(UBound(TempIDs) + 1)
                TempIDs(UBound(TempIDs)) = drtgp("ID").ToString
            End While
        End If
        cntgp.Close()
        cntgp = Nothing
        Return TempIDs
    End Function

    Private Function GetTGPIDByCPT(ByVal CPT As String) As String()
        Dim TempIDs() As String = {""}
        Dim sSQL As String = "Select ID from Tests where CPT_Code = '" & Trim(CPT) &
        "' Union Select ID from Groups where CPT_Code = '" & Trim(CPT) & "' Union " &
        "Select ID from Profiles where CPT_Code = '" & Trim(CPT) & "'"
        Dim cncpt As New Data.SqlClient.SqlConnection(connString)
        cncpt.Open()
        Dim cmdcpt As New Data.SqlClient.SqlCommand(sSQL, cncpt)
        cmdcpt.CommandType = Data.CommandType.Text
        Dim drcpt As Data.SqlClient.SqlDataReader = cmdcpt.ExecuteReader
        If drcpt.HasRows Then
            While drcpt.Read
                If TempIDs(UBound(TempIDs)) <> "" Then _
                ReDim Preserve TempIDs(UBound(TempIDs) + 1)
                TempIDs(UBound(TempIDs)) = drcpt("ID").ToString
            End While
        End If
        cncpt.Close()
        cncpt = Nothing
        Return TempIDs
    End Function

    Private Function GetTGPIDByLOINC(ByVal LOINC As String) As String()
        Dim TempIDs() As String = {""}
        Dim sSQL As String = "Select ID from Tests where Loinc = '" & Trim(LOINC) &
        "' Union Select ID from Groups where Loinc = '" & Trim(LOINC) & "' Union " &
        "Select ID from Profiles where Loinc = '" & Trim(LOINC) & "'"
        Dim cnloi As New Data.SqlClient.SqlConnection(connString)
        cnloi.Open()
        Dim cmdloi As New Data.SqlClient.SqlCommand(sSQL, cnloi)
        cmdloi.CommandType = Data.CommandType.Text
        Dim drloi As Data.SqlClient.SqlDataReader = cmdloi.ExecuteReader
        If drloi.HasRows Then
            While drloi.Read
                If TempIDs(UBound(TempIDs)) <> "" Then _
                ReDim Preserve TempIDs(UBound(TempIDs) + 1)
                TempIDs(UBound(TempIDs)) = drloi("ID").ToString
            End While
        End If
        cnloi.Close()
        cnloi = Nothing
        Return TempIDs
    End Function

    Private Sub UpdateComponent(ByVal CPT As String, ByVal Dx As String)
        If Trim(CPT) <> "" And Trim(Dx) <> "" Then
            ExecuteSqlProcedure("If Not Exists(Select * from MedicalNecessity where CPT_Code = '" & _
            Trim(CPT) & "' and Dx_Code = '" & Trim(Dx) & "') Insert into MedicalNecessity " & _
            "(CPT_Code, Dx_Code) values ('" & Trim(CPT) & "', '" & Trim(Dx) & "')")
        End If
    End Sub

    Private Sub frmImportNec_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
    End Sub
End Class
