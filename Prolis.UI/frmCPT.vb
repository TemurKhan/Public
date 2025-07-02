Imports System.Windows.Forms
Imports System.Data

Public Class frmCPT
    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        If dgvCPT.SelectedRows.Count > 0 Then
            Me.Tag = dgvCPT.SelectedRows(0).Cells(0).Value
            btnAccept.Enabled = False
            ClearForm()
            Me.Close()
        End If
    End Sub

    Private Sub cmbMode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMode.SelectedIndexChanged
        ClearForm()
        btnAccept.Enabled = False
        If cmbMode.SelectedIndex = 0 Then   'View
            dgvCPT.Enabled = True
            btnSave.Enabled = False
            btnDelete.Enabled = False
            txtCode.Enabled = False
            txtWEF.Enabled = False
            txtDescription.Enabled = False
            txtSearch.Enabled = True : btnSearch.Enabled = True
            btnImport.Enabled = False : chkOS.Enabled = False
        ElseIf cmbMode.SelectedIndex = 1 Then   'Edit
            dgvCPT.Enabled = True
            btnSave.Enabled = False
            btnDelete.Enabled = False
            txtCode.Enabled = True
            txtWEF.Enabled = True
            txtDescription.Enabled = True
            txtSearch.Enabled = True : btnSearch.Enabled = True
            btnImport.Enabled = True : chkOS.Enabled = True
        Else
            txtCode.Enabled = True
            txtWEF.Enabled = True
            txtDescription.Enabled = True
            dgvCPT.Enabled = False
            txtSearch.Enabled = False : btnSearch.Enabled = False
            btnImport.Enabled = False : chkOS.Enabled = False
        End If
    End Sub

    Private Sub ClearForm()
        txtCode.Text = ""
        txtWEF.Clear()
        txtDescription.Text = ""
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub frmCPT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CN.Open("DSN=ProlisQC;UID=sa;PWD=
        cmbMode.SelectedIndex = 0
        If frmRequisitions.TCode <> "" Then
            'If frmRequisitions.dtpDate.Value >= "10/01/2015" Then
            '    cmbOutput.SelectedIndex = 0
            'Else
            '    cmbOutput.SelectedIndex = 1
            'End If
            txtSearch.Text = frmRequisitions.TCode
            btnSearch_Click(Nothing, Nothing)
        ElseIf frmBillingEdit.TCode <> "" Then
            'If CDate(frmBillingEdit.txtSvcDate.Text) >= "10/01/2015" Then
            '    cmbOutput.SelectedIndex = 0
            'Else
            '    cmbOutput.SelectedIndex = 1
            'End If
            txtSearch.Text = frmBillingEdit.TCode
            btnSearch_Click(Nothing, Nothing)
        ElseIf frmScrubber.TCode <> "" Then
            'If CDate(frmScrubber.txtAccDate.Text) >= "10/01/2015" Then
            '    cmbOutput.SelectedIndex = 0
            'Else
            '    cmbOutput.SelectedIndex = 1
            'End If
            txtSearch.Text = frmScrubber.TCode
            btnSearch_Click(Nothing, Nothing)
        Else
            txtSearch.Text = ""
            dgvCPT.Rows.Clear()
        End If
        Me.Tag = ""
        PopulateCodes("")
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Public Shadows Function ShowDialog() As String
        If Me.IsHandleCreated Then Me.Close()
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub PopulateCodes(ByVal SRTRM As String)
        dgvCPT.Rows.Clear()
        SRTRM = Replace(SRTRM, " ", "%")
        Dim sSQL As String = ""
        'If cmbOutput.SelectedIndex = 0 Then 'ICD10
        '    sSQL = "Select * from CPT_Codes where Status = '1' and Code = '" & SRTRM & "' Union " & _
        '    "Select * from CPT_Codes where Status = '1' and Description Like '%" & SRTRM & "%'"
        'ElseIf cmbOutput.SelectedIndex = 1 Then 'ICD9
        '    sSQL = "Select * from CPT_Codes where Status <> '1' and Code = '" & SRTRM & "' Union " & _
        '    "Select * from CPT_Codes where Status <> '1' and Description Like '%" & SRTRM & "%'"
        'Else
        'sSQL = "Select * from CPT_Codes where Code = '" & SRTRM & "' Union Select * " & _
        '"from CPT_Codes where Status in ('1', 'C') and Description Like '%" & SRTRM & "%'"
        'End If
        sSQL = "Select * from CPT_Codes where Narration like '%" & SRTRM & "%'  "

        Dim cndx As New SqlClient.SqlConnection(connString)
        cndx.Open()
        Dim cmddx As New SqlClient.SqlCommand(sSQL, cndx)
        cmddx.CommandType = CommandType.Text
        Dim drdx As SqlClient.SqlDataReader = cmddx.ExecuteReader
        If drdx.HasRows Then
            While drdx.Read
                dgvCPT.Rows.Add(drdx("Code"), drdx("WEF"), drdx("Description"))
            End While
        End If
        cndx.Close()
        cndx = Nothing
    End Sub

    Private Sub txtDxName_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDescription.Validated
        If cmbMode.SelectedIndex = 1 Or cmbMode.SelectedIndex = 2 Then
            If txtDescription.Text <> "" And txtCode.Text <> "" Then
                btnSave.Enabled = True
            Else
                btnSave.Enabled = False
            End If
        End If
    End Sub

    Private Sub txtDxCode_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.Validated
        If cmbMode.SelectedIndex = 1 Or cmbMode.SelectedIndex = 2 Then
            If txtCode.Text <> "" Then
                btnSave.Enabled = True
            Else
                btnSave.Enabled = False
            End If
        End If
    End Sub

    Private Sub dgvDxs_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCPT.CellClick
        If cmbMode.SelectedIndex = 0 Then
            If e.RowIndex <> -1 Then btnAccept.Enabled = True
        End If

    End Sub

    Private Sub dgvDxs_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCPT.CellDoubleClick
        If cmbMode.SelectedIndex = 1 Then
            If e.RowIndex <> -1 Then
                'DisplayCPT(dgvCPT.Rows(e.RowIndex).Cells(0).Value)

                '==============
                txtCode.Text = dgvCPT.Rows(e.RowIndex).Cells(0).Value.ToString
                txtWEF.Text = dgvCPT.Rows(e.RowIndex).Cells(1).Value.ToString
                txtDescription.Text = dgvCPT.Rows(e.RowIndex).Cells(2).Value.ToString
                '==================
                btnDelete.Enabled = True
                txtCode.Enabled = True
                txtDescription.Enabled = True
                txtWEF.Enabled = True
            End If
        End If
    End Sub

    Private Sub DisplayCPT(ByVal Code As String)

        Dim cndx As New SqlClient.SqlConnection(connString)
        cndx.Open()
        Dim cmddx As New SqlClient.SqlCommand("Select * from CPT_Codes where Code = '" & Code & "'", cndx)
        cmddx.CommandType = CommandType.Text
        Dim drdx As SqlClient.SqlDataReader = cmddx.ExecuteReader
        If drdx.HasRows Then
            While drdx.Read
                txtCode.Text = drdx("Code")
                txtWEF.Text = drdx("WEF")

                'For i As Integer = 0 To cmbWEF.Items.Count - 1
                '    If drdx("WEF") = cmbWEF.Items(i).ToString.Substring(0, 1) Then
                '        cmbWEF.SelectedIndex = i
                '        Exit For
                '    End If
                'Next
                txtDescription.Text = drdx("Description")
            End While
        End If
        cndx.Close()
        cndx = Nothing
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtDescription.Text <> "" And Trim(txtCode.Text) <> "" And Trim(txtWEF.Text) <> "" Then
            ExecuteSqlProcedure("If Exists (Select * from CPT_Codes where Code = '" & Trim(txtCode.Text) & _
            "') Update CPT_Codes set WEF = '" & Trim(txtWEF.Text) & "', Description = '" & _
            Trim(txtDescription.Text) & "' where Code = '" & Trim(txtCode.Text) & "' Else Insert into " & _
            "CPT_Codes (Code, WEF, Description) Values ('" & Trim(txtCode.Text) & "', '" & _
            Trim(txtWEF.Text) & "', '" & Trim(txtDescription.Text) & "')")
            'PopulateDxs()
            ClearForm()
            btnSave.Enabled = False
            btnDelete.Enabled = False
            If cmbMode.SelectedIndex = 1 Then   'Edit
                txtCode.Enabled = False
                txtWEF.Enabled = False
                txtDescription.Enabled = False
                cmbMode.Focus()
            ElseIf cmbMode.SelectedIndex = 2 Then   'New
                txtCode.Enabled = True
                txtWEF.Enabled = True
                txtDescription.Enabled = True
                txtCode.Focus()
            End If
        Else
            MsgBox("All red labeled are required fields.")
            If txtCode.Text = "" Then
                txtCode.Focus()
            ElseIf String.IsNullOrEmpty(txtWEF.Text) Then
                txtWEF.Focus()
            Else
                txtDescription.Focus()
            End If
        End If
    End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        'On Error Resume Next
        Try

            Dim CMDSTR As String = frmDxCopy.ShowDialog
            If Not CMDSTR Is Nothing AndAlso CMDSTR <> "" Then
                If InStr(CMDSTR, "|") > 0 Then
                    Dim CMD() As String = CMDSTR.Split("|")
                    Dim File As String = CMD(0)
                    Dim Delim As String
                    'Dim Rs As New ADODB.Recordset
                    Dim Fields() As String = New String() {}
                    Dim FLD As Integer = Val(CMD(2))
                    Dim TBL As String = CMD(UBound(CMD))
                    If CMD(1) = "0" Then
                        Delim = ","
                    ElseIf CMD(1) = "1" Then
                        Delim = Chr(9)
                    ElseIf CMD(1) = "2" Then
                        Delim = "|"
                    Else
                        Delim = vbCrLf
                    End If
                    Dim SR As New System.IO.StreamReader(File)

                    '==================
                    Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(File)
                        MyReader.TextFieldType = FileIO.FieldType.Delimited
                        MyReader.SetDelimiters(Delim)
                        Dim currentRow As String()
                        While Not MyReader.EndOfData
                            Try
                                currentRow = MyReader.ReadFields()
                                Dim len As Integer = currentRow.Length - 1
                                Dim currentField As String
                                Dim x As Integer = 0
                                Dim y As Integer = 1 'for checking the empty elements of myField, if an element will blank then y =0
                                Dim myField(len) As String
                                For Each currentField In currentRow

                                    myField(x) = Replace(currentField, "'", "''")
                                    If String.IsNullOrEmpty(myField(x)) Then y = 0
                                    x += 1
                                    y *= y

                                Next

                                If y = 1 Then
                                    ExecuteSqlProcedure("If Exists (Select * from CPT_Codes where Code = '" & _
                                    Trim(myField(0)) & "') Update CPT_Codes set WEF = '" & Trim(myField(2)) & _
                                    "', Description = '" & Replace(Trim(myField(1)), Chr(34), "") & "' where Code = '" & Trim(myField(0)) & _
                                    "' Else Insert into CPT_Codes (Code, Description, WEF) Values ('" & _
                                    Trim(myField(0)) & "', '" & Trim(myField(1)) & "', '" & Replace(Trim(myField(2)), Chr(34), "") & "')")
                                End If
                            Catch ex As Microsoft.VisualBasic.
                                        FileIO.MalformedLineException
                                MsgBox("Line " & ex.Message &
                                "is not valid and will be skipped.")
                            End Try
                        End While
                    End Using
                    ' ======================

                    'Do Until SR.Peek = -1
                    '    Data = SR.ReadLine
                    '    Data = Replace(Data, Chr(34), "")
                    '    If Data.Length > 0 Then
                    '        Fields = Data.Split(Delim)
                    '        MyData = GetMyData(Fields, CMD)
                    '        MyData(2) = Replace(MyData(2), "'", "''")
                    '        If Trim(MyData(0)) <> "" And Trim(MyData(1)) <> "" And Trim(MyData(UBound(MyData))) <> "" Then
                    '            '0=code, 1=WEF, 2=description 
                    '            ExecuteSqlProcedure("If Exists (Select * from CPT_Codes where Code = '" & _
                    '            Trim(MyData(0)) & "') Update CPT_Codes set WEF = '" & Trim(MyData(1)) & _
                    '            "', Description = '" & Replace(Trim(MyData(2)), Chr(34), "") & "' where Code = '" & Trim(MyData(0)) & _
                    '            "' Else Insert into CPT_Codes (Code, WEF, Description) Values ('" & _
                    '            Trim(MyData(0)) & "', '" & Trim(MyData(1)) & "', '" & Replace(Trim(MyData(2)), Chr(34), "") & "')")
                    '        End If
                    '    End If
                    'Loop
                    'SR.Close()
                    'SR = Nothing


                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function GetMyData(ByVal Fields() As String, ByVal CMD() As String) As String()
        Dim MyData(CMD.Length - 4) As String
        Dim Data() As String
        'Dim i As Integer
        'Dim n As Integer = 0
        For n As Integer = 0 To Fields.Length - 1
            'For i = 2 To CMD.Length - 2
            If CMD(n + 2).Length > 3 Then
                Data = Split(CMD(n + 2), "^")
                If Data.Length > 1 Then
                    MyData(n) = Fields(Val(Data(1))).ToString
                End If
            End If
        Next
        'Next
        Return MyData
    End Function

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If txtSearch.Text <> "" Then
            PopulateCodes(txtSearch.Text)
            'txtSearch.Text = ""
        End If
    End Sub

    Private Sub chkOS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOS.CheckedChanged
        If chkOS.Checked = True Then
            chkOS.Text = "Yes"
        Else
            chkOS.Text = "No"
        End If
    End Sub


    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Not String.IsNullOrWhiteSpace(txtCode.Text) AndAlso MessageBox.Show("Are You Sure to Delete this Record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                Me.Tag = "Delete"
                ExecuteSqlProcedure("Delete from CPT_Codes where Code = '" & Trim(txtCode.Text) & "'")

                ClearForm()
                Me.dgvCPT.Rows.RemoveAt(Me.dgvCPT.CurrentRow.Index)
                'btnSave.Enabled = False
                'btnDelete.Enabled = False
            End If
            ' for set focus bcz focus has gone
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
