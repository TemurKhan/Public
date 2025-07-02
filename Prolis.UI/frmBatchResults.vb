
Public Class frmBatchResults

    Private Sub frmBatchResults_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnRelease.Enabled = ThisUser.Result_Release
        btnBlock.Enabled = ThisUser.Result_Release

        ConfigureDateTimePicker(dtpDateFrom)
        ConfigureDateTimePicker(dtpDateTo)

        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    'Private Sub txtDateFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    NFCOLOR = txtDateFrom.BackColor
    '    txtDateFrom.BackColor = FCOLOR
    'End Sub

    'Private Sub txtDateFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    If e.KeyChar = vbCr Then
    '        e.KeyChar = Chr(0)
    '        SendKeys.Send("{TAB}")
    '    End If
    'End Sub

    'Private Sub txtDateFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    txtDateFrom.BackColor = NFCOLOR
    'End Sub

    'Private Sub txtDateFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If UserEnteredText(txtDateFrom) <> "" Then
    '        If IsDate(txtDateFrom.Text) Then
    '            txtAccFrom.Text = ""
    '            txtAccTo.Text = ""
    '            UpdateTests()
    '        End If
    '    End If
    'End Sub

    Private Sub UpdateTests()
        Dim sSQL As String = String.Empty
        If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
            sSQL = "Select * from Tests where ID in (Select distinct Test_ID from Acc_Results " &
            "where Accession_ID in (Select ID from Requisitions where AccessionDate between '" &
            dtpDateFrom.Text & "' and '" & dtpDateFrom.Text & " 23:59:00')) order by Name"
        ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
            sSQL = "Select * from Tests where ID in (Select distinct Test_ID from Acc_Results " &
            "where Accession_ID in (Select ID from Requisitions where AccessionDate between '" &
            dtpDateFrom.Text & "' and '" & dtpDateTo.Text & " 23:59:00')) order by Name"
        ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
            sSQL = "Select * from Tests where ID in (Select distinct Test_ID from Acc_Results " &
            "where Accession_ID in (Select ID from Requisitions where AccessionDate between '" &
            dtpDateTo.Text & "' and '" & dtpDateTo.Text & " 23:59:00')) order by Name"
        ElseIf txtAccFrom.Text <> String.Empty And txtAccTo.Text = String.Empty Then
            sSQL = "Select * from Tests where ID in (Select distinct Test_ID from Acc_Results " &
            "where Accession_ID = " & Val(txtAccFrom.Text) & ") order by Name"
        ElseIf txtAccFrom.Text = String.Empty And txtAccTo.Text <> String.Empty Then
            sSQL = "Select * from Tests where ID in (Select distinct Test_ID from Acc_Results " &
            "where Accession_ID = " & Val(txtAccTo.Text) & ") order by Name"
        ElseIf txtAccFrom.Text <> String.Empty And txtAccTo.Text <> String.Empty Then
            sSQL = "Select * from Tests where ID in (Select distinct Test_ID from Acc_Results where " &
            "Accession_ID between " & Val(txtAccFrom.Text) & " and " & Val(txtAccTo.Text) & ") order by Name"
        End If
        If sSQL <> String.Empty Then
            cmbTests.Items.Clear()
            Dim cnts As New SqlClient.SqlConnection(connString)
            cnts.Open()
            Dim cmdts As New SqlClient.SqlCommand(sSQL, cnts)
            cmdts.CommandType = CommandType.Text
            Dim drts As SqlClient.SqlDataReader = cmdts.ExecuteReader
            If drts.HasRows Then
                While drts.Read
                    cmbTests.Items.Add(New MyList(drts("Name") &
                    " (" & drts("ID") & ")", drts("ID")))
                End While
            End If
            cnts.Close()
            cnts = Nothing
        End If
    End Sub

    'Private Sub txtDateTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    NFCOLOR = txtDateTo.BackColor
    '    txtDateTo.BackColor = FCOLOR
    'End Sub

    'Private Sub txtDateTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    If e.KeyChar = vbCr Then
    '        e.KeyChar = Chr(0)
    '        SendKeys.Send("{TAB}")
    '    End If
    'End Sub

    'Private Sub txtDateTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    txtDateTo.BackColor = NFCOLOR
    'End Sub

    'Private Sub txtDateTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If UserEnteredText(txtDateTo) <> "" Then
    '        If IsDate(txtDateTo.Text) Then
    '            txtAccFrom.Text = ""
    '            txtAccTo.Text = ""
    '            UpdateTests()
    '        End If
    '    End If
    'End Sub

    Private Sub txtAccFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccFrom.GotFocus
        NFCOLOR = txtAccFrom.BackColor
        txtAccFrom.BackColor = FCOLOR
    End Sub

    Private Sub txtAccFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccFrom.KeyPress
        If e.KeyChar = vbCr Then
            e.KeyChar = Chr(0)
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtAccFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccFrom.LostFocus
        txtAccFrom.BackColor = NFCOLOR
    End Sub

    Private Sub txtAccFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccFrom.Validated
        If txtAccFrom.Text <> String.Empty Then
            'txtDateFrom.Text = ""
            'txtDateTo.Text = ""
            ClearDateTimePicker(dtpDateFrom)
            ClearDateTimePicker(dtpDateTo)

            UpdateTests()
        End If
    End Sub

    Private Sub txtAccTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccTo.GotFocus
        NFCOLOR = txtAccTo.BackColor
        txtAccTo.BackColor = FCOLOR
    End Sub

    Private Sub txtAccTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccTo.KeyPress
        If e.KeyChar = vbCr Then
            e.KeyChar = Chr(0)
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtAccTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccTo.LostFocus
        txtAccTo.BackColor = NFCOLOR
    End Sub

    Private Sub txtAccTo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAccTo.TextChanged
        If txtAccTo.Text <> String.Empty Then
            'txtDateFrom.Text = ""
            'txtDateTo.Text = ""

            ClearDateTimePicker(dtpDateFrom)
            ClearDateTimePicker(dtpDateTo)
        End If
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Dim sSQL As String = String.Empty
        Dim Patient As String = String.Empty
        Dim ItemX As MyList
        If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) And cmbTests.SelectedIndex <> -1 Then
            ItemX = cmbTests.SelectedItem
            sSQL = "Select * from Acc_Results where Accession_ID in (Select ID from Requisitions " &
            "where Received <> 0 and AccessionDate between '" & dtpDateFrom.Text & "' and '" &
            dtpDateFrom.Text & " 23:59:00') and Test_ID = " & ItemX.ItemData
        ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) And cmbTests.SelectedIndex <> -1 Then
            ItemX = cmbTests.SelectedItem
            sSQL = "Select * from Acc_Results where Accession_ID in (Select ID from Requisitions " &
            "where Received <> 0 and AccessionDate between '" & dtpDateFrom.Text & "' and '" &
            dtpDateTo.Text & " 23:59:00 ') and Test_ID = " & ItemX.ItemData
        ElseIf txtAccFrom.Text <> String.Empty And txtAccTo.Text = String.Empty And cmbTests.SelectedIndex <> -1 Then
            ItemX = cmbTests.SelectedItem
            sSQL = "Select * from Acc_Results where Accession_ID in (Select ID from Requisitions " &
            "where Received <> 0 and ID = " & Val(txtAccFrom.Text) & ") and Test_ID = " & ItemX.ItemData
        ElseIf txtAccFrom.Text = String.Empty And txtAccTo.Text <> String.Empty And cmbTests.SelectedIndex <> -1 Then
            ItemX = cmbTests.SelectedItem
            sSQL = "Select * from Acc_Results where Accession_ID in (Select ID from Requisitions " &
            "where Received <> 0 and ID = " & Val(txtAccTo.Text) & ") and Test_ID = " & ItemX.ItemData
        ElseIf txtAccFrom.Text <> String.Empty And txtAccTo.Text <> String.Empty And cmbTests.SelectedIndex <> -1 Then
            ItemX = cmbTests.SelectedItem
            sSQL = "Select * from Acc_Results where Accession_ID in (Select ID from " &
            "Requisitions where Received <> 0 and ID between " & Val(txtAccFrom.Text) &
            " and " & Val(txtAccTo.Text) & ") and Test_ID = " & ItemX.ItemData
        End If
        If sSQL <> String.Empty Then
            dgvResults.Rows.Clear()
            Dim cnrs As New SqlClient.SqlConnection(connString)
            cnrs.Open()
            Dim cmdrs As New SqlClient.SqlCommand(sSQL, cnrs)
            cmdrs.CommandType = CommandType.Text
            Dim drrs As SqlClient.SqlDataReader = cmdrs.ExecuteReader
            If drrs.HasRows Then
                While drrs.Read
                    Patient = GetPatientName(drrs("Accession_ID"))
                    If drrs("Result") IsNot DBNull.Value AndAlso drrs("Result") <> String.Empty Then
                        dgvResults.Rows.Add(drrs("Accession_ID"), Patient, drrs("Result"), drrs("Released"))
                    Else
                        If ThisUser.Result_Entry = True Then
                            dgvResults.Rows.Add(drrs("Accession_ID"), Patient, cmbDefault.Text, drrs("Released"))
                            dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).ReadOnly = False
                        Else
                            dgvResults.Rows.Add(drrs("Accession_ID"), Patient, String.Empty, drrs("Released"))
                            dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).ReadOnly = True
                        End If
                    End If
                    If ThisUser.Result_Release = True Then
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(3).ReadOnly = False
                    Else
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(3).ReadOnly = True
                    End If
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).Style.BackColor = Color.White
                End While
            End If
            cnrs.Close()
            cnrs = Nothing
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Dim i As Integer
        For i = 0 To dgvResults.RowCount - 1
            dgvResults.Rows(i).Cells(2).Value = String.Empty
            dgvResults.Rows(i).Cells(3).Value = False
        Next
    End Sub

    Private Sub btnBlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBlock.Click
        Dim i As Integer
        For i = 0 To dgvResults.RowCount - 1
            dgvResults.Rows(i).Cells(3).Value = False
        Next
    End Sub

    Private Sub btnRelease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRelease.Click
        Dim i As Integer
        For i = 0 To dgvResults.RowCount - 1
            If dgvResults.Rows(i).Cells(2).Value <> String.Empty Then
                dgvResults.Rows(i).Cells(3).Value = True
            Else
                dgvResults.Rows(i).Cells(3).Value = False
            End If
        Next
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If dgvResults.RowCount > 0 And cmbTests.SelectedIndex <> -1 Then
            Dim ItemX As MyList = cmbTests.SelectedItem
            Dim sSQL As String = String.Empty
            Dim Flag() As String = {String.Empty, String.Empty}
            For i As Integer = 0 To dgvResults.RowCount - 1
                If Trim(dgvResults.Rows(i).Cells(2).Value) <> String.Empty _
                AndAlso Trim(dgvResults.Rows(i).Cells(0).Value) _
                AndAlso ItemX.ItemData > 0 Then
                    Flag = GetFlag(dgvResults.Rows(i).Cells(0).Value,
                    Trim(dgvResults.Rows(i).Cells(2).Value), ItemX.ItemData)
                    If dgvResults.Rows(i).Cells(3).Value = True Then
                        sSQL = "Update Acc_Results set Result = '" & Trim(dgvResults.Rows(i).Cells(2).Value) &
                        "', Flag = '" & Flag(0) & "', Behavior = '" & Flag(1) & "', Released = 1, Released_By = " &
                        ThisUser.ID & ", Release_Time = '" & Date.Now & "' where Accession_ID = " &
                        dgvResults.Rows(i).Cells(0).Value & " and Test_ID = " & ItemX.ItemData
                    Else
                        sSQL = "Update Acc_Results set Result = '" & Trim(dgvResults.Rows(i).Cells(2).Value) &
                        "', Flag = '" & Flag(0) & "', Behavior = '" & Flag(1) & "', Released = 0, " &
                        "Released_By = Null, Release_Time = Null where Accession_ID = " &
                        dgvResults.Rows(i).Cells(0).Value & " and Test_ID = " & ItemX.ItemData
                    End If
                    ExecuteSqlProcedure(sSQL)
                    Flag(0) = String.Empty : Flag(1) = String.Empty
                End If
                '
                If ReportFullResulted(Val(dgvResults.Rows(i).Cells(0).Value)) Then _
                UpdateReportTime(Val(dgvResults.Rows(i).Cells(0).Value), Date.Now)
            Next
            ClearForm()
        Else
            MsgBox("In order to save record(s), select the Test and the result(s)", MsgBoxStyle.Information, "PROLIS")
        End If
    End Sub

    Private Sub ClearForm()
        'txtDateFrom.Text = "" : txtDateTo.Text = ""
        ClearDateTimePicker(dtpDateFrom)
        ClearDateTimePicker(dtpDateTo)

        txtAccFrom.Text = String.Empty
        txtAccTo.Text = String.Empty : cmbTests.SelectedIndex = -1 : cmbTests.Items.Clear()
        cmbDefault.SelectedIndex = -1 : dgvResults.Rows.Clear()
    End Sub

    Private Sub txtAccTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccTo.Validated
        UpdateTests()
    End Sub

    Private Sub dgvResults_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvResults.CellEndEdit
        If e.ColumnIndex = 2 Then       'Result
            If Not dgvResults.Rows(e.RowIndex).Cells(e.ColumnIndex).Value Is Nothing AndAlso
            Trim(dgvResults.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) <> String.Empty Then
                If ThisUser.Result_Release = True Then  'Got permission
                    If SystemConfig.ReleaseWithEntry = True Then
                        dgvResults.Rows(e.RowIndex).Cells(3).Value = True
                    End If
                End If
            Else
                dgvResults.Rows(e.RowIndex).Cells(3).Value = False
            End If
        ElseIf e.ColumnIndex = 3 Then   'Release
            If dgvResults.Rows(e.RowIndex).Cells(2).Value Is Nothing Then
                dgvResults.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = False
            ElseIf Trim(dgvResults.Rows(e.RowIndex).Cells(2).Value.ToString) = String.Empty Then
                dgvResults.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = False
            End If
        End If
    End Sub

    Private Sub dgvResults_CellLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvResults.CellLeave
        If e.ColumnIndex = 2 Then
            If dgvResults.Rows(e.RowIndex).Cells(e.ColumnIndex).Value Is Nothing Or
            dgvResults.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = String.Empty Then
                dgvResults.Rows(e.RowIndex).Cells(3).Value = 0
            End If
        End If
    End Sub

    Private Sub cmbDefault_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDefault.GotFocus
        NFCOLOR = cmbDefault.BackColor
        cmbDefault.BackColor = FCOLOR
    End Sub

    Private Sub cmbDefault_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbDefault.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbDefault_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDefault.LostFocus
        cmbDefault.BackColor = NFCOLOR
    End Sub

    Private Sub cmbTests_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbTests.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub dtpDateFrom_CloseUp(sender As Object, e As EventArgs) Handles dtpDateFrom.CloseUp
        ' After selecting a valid date, revert to the standard date format
        CloseUpDateTimePicker(dtpDateFrom)
    End Sub
    Private Sub dtpDateTo_CloseUp(sender As Object, e As EventArgs) Handles dtpDateTo.CloseUp
        CloseUpDateTimePicker(dtpDateTo)
    End Sub

    Private Sub lblClearDates_Click(sender As Object, e As EventArgs) Handles lblClearDates.Click
        ClearDateTimePicker(dtpDateFrom)
        ClearDateTimePicker(dtpDateTo)
    End Sub
End Class
