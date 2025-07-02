Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class frmDiagnosis
    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        If dgvDxs.SelectedRows.Count > 0 Then
            Me.Tag = dgvDxs.SelectedRows(0).Cells(0).Value
            btnAccept.Enabled = False
            ClearForm()
            Me.Close()
        End If
    End Sub

    Private Sub cmbMode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMode.SelectedIndexChanged
        ClearForm()
        btnAccept.Enabled = False
        If cmbMode.SelectedIndex = 0 Then   'View
            dgvDxs.Enabled = True
            btnSave.Enabled = False
            btnDelete.Enabled = False
            txtDxCode.Enabled = False
            cmbStatus.Enabled = False
            txtDxName.Enabled = False
            txtSearch.Enabled = True : btnSearch.Enabled = True
            btnImport.Enabled = False : chkOS.Enabled = False
        ElseIf cmbMode.SelectedIndex = 1 Then   'Edit
            dgvDxs.Enabled = True
            btnSave.Enabled = False
            btnDelete.Enabled = False
            txtDxCode.Enabled = True
            cmbStatus.Enabled = True
            txtDxName.Enabled = True
            txtSearch.Enabled = True : btnSearch.Enabled = True
            btnImport.Enabled = True : chkOS.Enabled = True
        Else
            txtDxCode.Enabled = True
            cmbStatus.Enabled = True
            txtDxName.Enabled = True
            dgvDxs.Enabled = False
            txtSearch.Enabled = False : btnSearch.Enabled = False
            btnImport.Enabled = False : chkOS.Enabled = False
        End If
    End Sub

    Private Sub ClearForm()
        txtDxCode.Text = ""
        cmbStatus.SelectedIndex = -1
        txtDxName.Text = ""
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub frmDiagnosis_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: UnComment this code 

        'cmbMode.SelectedIndex = 0
        'If frmRequisitions.TCode <> "" Then
        '    If frmRequisitions.dtpDate.Value >= "10/01/2015" Then
        '        cmbOutput.SelectedIndex = 0
        '    Else
        '        cmbOutput.SelectedIndex = 1
        '    End If
        '    txtSearch.Text = frmRequisitions.TCode
        '    btnSearch_Click(Nothing, Nothing)
        'ElseIf frmBillingEdit.TCode <> "" Then
        '    If CDate(frmBillingEdit.txtSvcDate.Text) >= "10/01/2015" Then
        '        cmbOutput.SelectedIndex = 0
        '    Else
        '        cmbOutput.SelectedIndex = 1
        '    End If
        '    txtSearch.Text = frmBillingEdit.TCode
        '    btnSearch_Click(Nothing, Nothing)
        'ElseIf frmScrubber.TCode <> "" Then
        '    If CDate(frmScrubber.txtAccDate.Text) >= "10/01/2015" Then
        '        cmbOutput.SelectedIndex = 0
        '    Else
        '        cmbOutput.SelectedIndex = 1
        '    End If
        '    txtSearch.Text = frmScrubber.TCode
        '    btnSearch_Click(Nothing, Nothing)
        'Else
        '    txtSearch.Text = ""
        '    dgvDxs.Rows.Clear()
        'End If
        'Me.Tag = ""
        ''populateDxs()
        'If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Public Shadows Function ShowDialog() As String
        If Me.IsHandleCreated Then Me.Close()
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Sub PopulateDxs(ByVal SRTRM As String)
        dgvDxs.Rows.Clear()
        SRTRM = Replace(SRTRM, " ", "%")
        Dim sSQL As String = ""
        If cmbOutput.SelectedIndex = 0 Then 'ICD10
            sSQL = "Select * from DiagCodes where Status = '1' and Code = '" & SRTRM & "' Union " & _
            "Select * from DiagCodes where Status = '1' and Description Like '%" & SRTRM & "%'"
        ElseIf cmbOutput.SelectedIndex = 1 Then 'ICD9
            sSQL = "Select * from DiagCodes where Status <> '1' and Code = '" & SRTRM & "' Union " & _
            "Select * from DiagCodes where Status <> '1' and Description Like '%" & SRTRM & "%'"
        Else
            sSQL = "Select * from DiagCodes where Code = '" & SRTRM & "' Union Select * " & _
            "from DiagCodes where Status in ('1', 'C') and Description Like '%" & SRTRM & "%'"
        End If
        Dim cndx As New SqlConnection(connString)
        cndx.Open()
        Dim cmddx As New SqlCommand(sSQL, cndx)
        cmddx.CommandType = CommandType.Text
        Dim drdx As SqlDataReader = cmddx.ExecuteReader
        If drdx.HasRows Then
            While drdx.Read
                dgvDxs.Rows.Add(drdx("Code"), drdx("Status"), drdx("Description"))
            End While
        End If
        cndx.Close()
        cndx = Nothing
    End Sub

    Private Sub txtDxName_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDxName.Validated
        If cmbMode.SelectedIndex = 1 Or cmbMode.SelectedIndex = 2 Then
            If txtDxName.Text <> "" And txtDxCode.Text <> "" Then
                btnSave.Enabled = True
            Else
                btnSave.Enabled = False
            End If
        End If
    End Sub

    Private Sub txtDxCode_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDxCode.Validated
        If cmbMode.SelectedIndex = 1 Or cmbMode.SelectedIndex = 2 Then
            If txtDxCode.Text <> "" Then
                btnSave.Enabled = True
            Else
                btnSave.Enabled = False
            End If
        End If
    End Sub

    Private Sub dgvDxs_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDxs.CellClick
        If cmbMode.SelectedIndex = 0 Then
            If e.RowIndex <> -1 Then btnAccept.Enabled = True
        End If

    End Sub

    Private Sub dgvDxs_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDxs.CellDoubleClick
        If cmbMode.SelectedIndex = 1 Then
            If e.RowIndex <> -1 Then
                DisplayDx(dgvDxs.Rows(e.RowIndex).Cells(0).Value)
                btnDelete.Enabled = True
                txtDxCode.Enabled = True
                txtDxName.Enabled = True
            End If
        End If
    End Sub

    Private Sub DisplayDx(ByVal Code As String)
        Dim cndx As New SqlConnection(connString)
        cndx.Open()
        Dim cmddx As New SqlCommand("Select * from DiagCodes where Code = '" & Code & "'", cndx)
        cmddx.CommandType = CommandType.Text
        Dim drdx As SqlDataReader = cmddx.ExecuteReader
        If drdx.HasRows Then
            While drdx.Read
                txtDxCode.Text = drdx("Code")
                For i As Integer = 0 To cmbStatus.Items.Count - 1
                    If drdx("Status") = cmbStatus.Items(i).ToString.Substring(0, 1) Then
                        cmbStatus.SelectedIndex = i
                        Exit For
                    End If
                Next
                txtDxName.Text = drdx("Description")
            End While
        End If
        cndx.Close()
        cndx = Nothing
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtDxName.Text <> "" And Trim(txtDxCode.Text) <> "" And cmbStatus.SelectedIndex <> -1 Then
            ExecuteSqlProcedure("If Exists (Select * from DiagCodes where Code = '" & Trim(txtDxCode.Text) & _
            "') Update DiagCodes set Status = '" & cmbStatus.SelectedIndex.ToString & "', Description = '" & _
            Trim(txtDxName.Text) & "' where Code = '" & Trim(txtDxCode.Text) & "' Else Insert into " & _
            "DiagCodes (Code, Status, Description) Values ('" & Trim(txtDxCode.Text) & "', '" & _
            cmbStatus.SelectedIndex.ToString & "', '" & Trim(txtDxName.Text) & "')")
            'PopulateDxs()
            ClearForm()
            btnSave.Enabled = False
            btnDelete.Enabled = False
            If cmbMode.SelectedIndex = 1 Then   'Edit
                txtDxCode.Enabled = False
                cmbStatus.Enabled = False
                txtDxName.Enabled = False
                cmbMode.Focus()
            ElseIf cmbMode.SelectedIndex = 2 Then   'New
                txtDxCode.Enabled = True
                cmbStatus.Enabled = True
                txtDxName.Enabled = True
                txtDxCode.Focus()
            End If
        Else
            MsgBox("All red labeled are required fields.")
            If txtDxCode.Text = "" Then
                txtDxCode.Focus()
            ElseIf cmbStatus.SelectedIndex = -1 Then
                cmbStatus.Focus()
            Else
                txtDxName.Focus()
            End If
        End If
    End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Dim CMDSTR As String = frmDxCopy.ShowDialog
        If Not CMDSTR Is Nothing AndAlso CMDSTR <> "" Then
            If InStr(CMDSTR, "|") > 0 Then
                Dim CMD() As String = CMDSTR.Split("|")
                Dim File As String = CMD(0)
                Dim Delim As String
                Dim MyData() As String
                Dim Data As String
                Dim Fields() As String
                Dim FLD As Integer = Val(CMD(2))
                Dim TBL As String = CMD(UBound(CMD))

                Select Case CMD(1)
                    Case "0"
                        Delim = ","
                    Case "1"
                        Delim = Chr(9)
                    Case "2"
                        Delim = "|"
                    Case Else
                        Delim = vbCrLf
                End Select

                Using SR As New System.IO.StreamReader(File)
                    Using connection As New SqlConnection(connString)
                        connection.Open()

                        Do Until SR.Peek = -1
                            Data = SR.ReadLine
                            Data = Replace(Data, Chr(34), "")
                            If Data.Length > 0 Then
                                Fields = Data.Split(Delim)
                                MyData = GetMyData(Fields, CMD)
                                MyData(2) = Replace(MyData(2), "'", "''")

                                If Trim(MyData(0)) <> "" And Trim(MyData(1)) <> "" And Trim(MyData(UBound(MyData))) <> "" Then
                                    Dim query As String = "
                                    IF EXISTS (SELECT * FROM DiagCodes WHERE Code = @Code)
                                    BEGIN
                                        UPDATE DiagCodes 
                                        SET Status = @Status, Description = @Description 
                                        WHERE Code = @Code
                                    END
                                    ELSE
                                    BEGIN
                                        INSERT INTO DiagCodes (Code, Status, Description) 
                                        VALUES (@Code, @Status, @Description)
                                    END"

                                    Using command As New SqlCommand(query, connection)
                                        command.Parameters.AddWithValue("@Code", Trim(MyData(0)))
                                        command.Parameters.AddWithValue("@Status", Trim(MyData(1)))
                                        command.Parameters.AddWithValue("@Description", Replace(Trim(MyData(2)), Chr(34), ""))
                                        command.ExecuteNonQuery()
                                    End Using
                                End If
                            End If
                        Loop
                    End Using
                End Using
            End If
        End If
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
            PopulateDxs(txtSearch.Text)
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
End Class
