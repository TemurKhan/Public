Imports System.IO
Imports System.Windows.Forms

Public Class frmLabMapping

    Private Sub frmLabMapping_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtLabID.Text = frmLabMgmt.txtLabID.Text
        cmbDelim.SelectedIndex = 0
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        ' Try
        '    On Error Resume Next
        If txtFile.Text <> "" And txtLabID.Text <> "" And FieldsMapped() Then
            Dim LabID As Integer = Val(txtLabID.Text)
            Dim i As Integer
            Dim FR As New StreamReader(txtFile.Text)
            Do Until FR.ReadLine = Nothing
                i += 1
            Loop
            FR.Close()
            FR = Nothing
            pb.Maximum = i + 1
            Dim SR As New StreamReader(txtFile.Text)
            Dim Ordinal As Integer = 0
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
            Dim TgpID As Integer = -1
            Dim OrderID As Integer = -1
            Dim ResultID As Integer = -1
            For i = 0 To dgvFieldMap.RowCount - 1
                If dgvFieldMap.Rows(i).Cells(0).Value = True Then
                    If InStr(dgvFieldMap.Rows(i).Cells(2).Value, "Prolis ID") > 0 Then
                        TgpID = i
                    ElseIf InStr(dgvFieldMap.Rows(i).Cells(2).Value, "Lab Order ID") > 0 Then
                        OrderID = 0
                    ElseIf InStr(dgvFieldMap.Rows(i).Cells(2).Value, "Lab Result ID") > 0 Then
                        ResultID = i
                    End If
                End If
            Next
            i = 1
            '
            If TgpID <> -1 And OrderID <> -1 And ResultID <> -1 Then
                If chkDelExist.Checked Then
                    ExecuteSqlProcedure("Delete from Lab_TGP where Lab_ID = " & LabID)
                End If
                Dim Ln As String ' Going to hold one line at a time
                'Ln = SR.ReadLine
                While Not SR.EndOfStream
                    Ln = SR.ReadLine
                    pb.Value = i
                    'ioLines = ioLines & vbCrLf & Ln
                    If Delim = "," Then
                        Ln = Replace(Ln, Chr(10), "")
                        If InStr(Ln, Chr(34)) > 0 Then
                            Dim p1 As Integer
                            Dim p2 As Integer
                            Dim oTmp As String = ""
                            Dim nTmp As String = ""
                            Do Until InStr(Ln, Chr(34)) = 0
                                p1 = InStr(Ln, Chr(34))
                                p2 = InStr(p1 + 1, Ln, Chr(34))
                                oTmp = Ln.Substring(p1 - 1, p2 - p1 + 1)
                                nTmp = Replace(oTmp, ",", "|")
                                nTmp = Replace(nTmp, Chr(34), "")
                                Ln = Replace(Ln, oTmp, nTmp)
                            Loop
                        End If
                        Dim Segs() As String = Split(Ln, ",")  'comma delimited string
                        For c As Integer = 0 To Segs.Length - 1
                            If Segs(c) Is Nothing Then
                                Segs(c) = ""
                            Else
                                Segs(c) = Replace(Segs(c), "|", ",")
                            End If
                        Next
                    End If
                    Dim Fields() As String = Split(Ln, Delim)
                    If Fields.Length >= 3 Then
                        If IsNumeric(Fields(TgpID)) And Val(Fields(TgpID)) > 0 And _
                        (Fields(OrderID) <> "" Or Fields(ResultID) <> "") Then
                            UpdateMapping(LabID, Fields(TgpID), Fields(OrderID), Fields(ResultID), 0, Ordinal)
                            Ordinal += 1
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
                If InStr(dgvFieldMap.Rows(i).Cells(2).Value, "Prolis ID") > 0 Then
                    Mapped += 1
                ElseIf InStr(dgvFieldMap.Rows(i).Cells(2).Value, "Lab Order ID") > 0 Then
                    Mapped += 1
                ElseIf InStr(dgvFieldMap.Rows(i).Cells(2).Value, "Lab Result ID") > 0 Then
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

    Private Sub UpdateMapping(ByVal LabID As Integer, ByVal TGPID As Integer, ByVal OrderID _
    As String, ByVal ResultID As String, ByVal Price As Single, ByVal Ordinal As Integer)
        ExecuteSqlProcedure("If Exists (Select * from Lab_TGP where Lab_ID = " & LabID & _
        " and TGP_ID = " & TGPID & " and LabResultID = " & Trim(ResultID) & ") Update Lab_TGP set LabComponentID = '" & Trim(OrderID) & _
        "', LabResultID = '" & Trim(ResultID) & "', Price = " & Price & ", Ordinal = " & _
        Ordinal & " where Lab_ID = " & LabID & " and TGP_ID = " & TGPID & " Else Insert " & _
        "into Lab_TGP (Lab_ID, TGP_ID, LabComponentID, LabResultID, Price, Ordinal) values (" & _
        LabID & ", " & TGPID & ", '" & Trim(OrderID) & "', '" & Trim(ResultID) & "', " & _
        Price & ", " & Ordinal & ")")
    End Sub

    Private Sub btnOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpen.Click
        OpenFileDialog1.DefaultExt = "*.CAP"
        OpenFileDialog1.InitialDirectory = "C:\"
        OpenFileDialog1.Filter = "*.CSV Files|*.CSV|*.TXT Files|*.TXT|*.* All Files|*.*"
        If MsgBoxResult.Ok = OpenFileDialog1.ShowDialog Then
            txtFile.Text = OpenFileDialog1.FileName
            OpenFileDialog1.Dispose()
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

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
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
            If i < 3 Then
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
End Class
