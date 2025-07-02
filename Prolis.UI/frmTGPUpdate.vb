Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmTGPUpdate

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub frmDxCopy_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbDelimiter.SelectedIndex = 0
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub


    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If dgvFields.RowCount > 0 And cmbDelimiter.SelectedIndex <> -1 _
        And HasProperMapping() Then
            'On Error Resume Next
            Dim SR As New System.IO.StreamReader(txtFile.Text)
            Dim Data As String = ""
            Dim Fields() As String
            Dim CFields() As String = GetConditionFields()
            Dim UVALS(,) As String = GetUpdatingValues()
            Dim i As Integer
            Do Until SR.EndOfStream
                Data = SR.ReadLine()
                Fields = Split(Data, ",")
                Dim sSQL As String = "Update Tests set "
                For i = LBound(UVALS, 2) To UBound(UVALS, 2)
                    If UVALS(0, i) <> CFields(0) Then
                        sSQL += UVALS(0, i) & " = " & Fields(CInt(UVALS(1, i))) & ", "
                    End If
                Next
                If sSQL.Length > 18 Then sSQL = Microsoft.VisualBasic.Mid(sSQL, 1, Len(sSQL) - 2)
                sSQL += " where " & CFields(0) & " = " & Fields(CInt(CFields(1)))
                ExecuteSqlProcedure(sSQL)
                sSQL = ""
                Data = ""
            Loop
            SR.Close()
            SR = Nothing
            dgvFields.Rows.Clear()
            txtFile.Text = ""
            btnOK.Enabled = False
        End If
    End Sub

    Private Function GetUpdatingValues() As String(,)
        Dim UVALS(1, 0) As String
        Dim i As Integer
        For i = 0 To dgvFields.RowCount - 1
            If InStr(dgvFields.Rows(i).Cells(1).Value,
            dgvFields.Rows(i).Cells(0).Value) > 0 Then
                If UVALS(0, UBound(UVALS, 2)) <> "" Then _
                ReDim Preserve UVALS(1, UBound(UVALS, 2) + 1)
                UVALS(0, UBound(UVALS, 2)) = Trim(dgvFields.Rows(i).Cells(0).Value)
                UVALS(1, UBound(UVALS, 2)) = Trim(Microsoft.VisualBasic.Mid(dgvFields.Rows(i).Cells(1).Value,
                1, InStr(dgvFields.Rows(i).Cells(1).Value, "=") - 1))
            End If
        Next
        Return UVALS
    End Function

    Private Function GetConditionFields() As String()
        Dim CFLDS() As String = {"", ""}
        Dim i As Integer
        For i = 0 To dgvFields.RowCount - 1
            If (InStr(dgvFields.Rows(i).Cells(0).Value, "ID") > 0 _
            And InStr(dgvFields.Rows(i).Cells(1).Value, "ID") > 0) _
            Or (InStr(dgvFields.Rows(i).Cells(0).Value, "CPT") > 0 _
            And InStr(dgvFields.Rows(i).Cells(1).Value, "CPT") > 0) Then
                CFLDS(0) = Trim(dgvFields.Rows(i).Cells(0).Value)
                CFLDS(1) = Trim(Microsoft.VisualBasic.Mid(
                dgvFields.Rows(i).Cells(1).Value, 1,
                InStr(dgvFields.Rows(i).Cells(1).Value, "=") - 1))
                Exit For
            End If
        Next
        Return CFLDS
    End Function

    Private Function HasProperMapping() As Boolean
        Dim Mapped As Boolean = False
        Dim i As Integer
        For i = 0 To dgvFields.RowCount - 1
            If (InStr(dgvFields.Rows(i).Cells(0).Value, "ID") > 0 _
            And InStr(dgvFields.Rows(i).Cells(1).Value, "ID") > 0) Or
            (InStr(dgvFields.Rows(i).Cells(0).Value, "CPT") > 0 _
            And InStr(dgvFields.Rows(i).Cells(1).Value, "CPT") > 0) _
            Then Mapped = True
            Exit For
        Next
        Return Mapped
    End Function

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        If txtFile.Text <> "" And cmbDelimiter.SelectedIndex <> -1 Then
            Dim Delim As String
            If cmbDelimiter.SelectedIndex = 0 Then
                Delim = ","
            ElseIf cmbDelimiter.SelectedIndex = 1 Then
                Delim = Chr(9)
            ElseIf cmbDelimiter.SelectedIndex = 2 Then
                Delim = "|"
            Else
                Delim = vbCrLf
            End If
            dgvFields.Rows.Clear()
            Dim SR As New System.IO.StreamReader(txtFile.Text)
            Dim Data As String = SR.ReadLine
            Dim Fields() As String = Data.Split(Delim)
            Dim n As Integer
            Dim cnt As New SqlConnection(connString)
            cnt.Open()
            Dim cmdt As New SqlCommand("Select * from Tests", cnt)
            cmdt.CommandType = CommandType.Text
            Dim drt As SqlDataReader = cmdt.ExecuteReader
            If drt.HasRows Then
                For i As Integer = 0 To drt.FieldCount - 1
                    Dim fldSource As New DataGridViewComboBoxCell
                    'fldSource.Items.Clear()
                    For n = 0 To Fields.Length - 1
                        fldSource.Items.Add(n.ToString & " = " & Fields(n).ToString)
                    Next
                    dgvFields.Rows.Add(drt.GetName(i), "")
                    dgvFields.Rows(dgvFields.RowCount - 1).Cells(1) = fldSource
                    fldSource = Nothing
                Next
            End If
            cnt.Close()
            cnt = Nothing
            SR.Close()
            SR = Nothing
        End If
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Dim OFD As New OpenFileDialog
        OFD.Title = "Delimited Text file"
        If OFD.ShowDialog = DialogResult.OK Then
            txtFile.Text = OFD.FileName
        Else
            txtFile.Text = ""
        End If
        Update_Progress()
    End Sub

    Private Sub Update_Progress()
        If txtFile.Text <> "" And cmbDelimiter.SelectedIndex <> -1 Then
            Dim i As Integer
            Dim Selected As Boolean = False
            For i = 0 To dgvFields.RowCount - 1
                If dgvFields.Rows(i).Cells(0).Value <> "" And dgvFields.Rows(i).Cells(1).Value <> "" Then Selected = True
            Next
            btnOK.Enabled = Selected
        End If
    End Sub

    Private Sub cmbDelimiter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDelimiter.SelectedIndexChanged
        If cmbDelimiter.SelectedIndex <> -1 Then Update_Progress()
    End Sub

    Private Sub dgvFields_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvFields.CellValueChanged
        Update_Progress()
    End Sub

    Private Sub dgvFields_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvFields.DataError
        On Error Resume Next
    End Sub
End Class
