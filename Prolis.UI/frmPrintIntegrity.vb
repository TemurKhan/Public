Imports System.Windows.Forms

Public Class frmPrintIntegrity

    Private Sub txtCopies_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCopies.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtCopies_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCopies.Validated
        If txtCopies.Text = "" Then txtCopies.Text = "1"
    End Sub

    Private Sub frmPrintIntegrity_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbDestination.SelectedIndex = 0
        dgvAccessions.RowCount = 1
        ConfigureDateTimePicker(dtpDateFrom)
        ConfigureDateTimePicker(dtpDateTo)

        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    'Private Sub txtDateFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If txtDateFrom.Text <> "" Then
    '        If IsDate(txtDateFrom.Text) = True Then
    '            txtAccFrom.Text = ""
    '            txtAccTo.Text = ""
    '            ClearRandomAccessions()
    '        Else
    '            txtDateFrom.Text = ""
    '        End If
    '    End If
    '    PrintProgress()
    'End Sub

    Private Function HasRandomValues() As Boolean
        Dim HasVal As Boolean = False
        Dim i As Integer
        For i = 0 To dgvAccessions.RowCount - 1
            If dgvAccessions.Rows(i).Cells(1).Value <> String.Empty AndAlso
            dgvAccessions.Rows(i).Cells(1).Value <> "" Then
                HasVal = True
                Exit For
            End If
        Next
        HasRandomValues = HasVal
    End Function

    Private Sub ClearRandomAccessions()
        Dim i As Integer
        For i = dgvAccessions.RowCount - 1 To 0 Step -1
            If i = 0 Then
                dgvAccessions.Rows(i).Cells(1).Value = ""
                dgvAccessions.Rows(i).Cells(0).Value =
                System.Drawing.Image.FromFile(Application.StartupPath &
                "\Images\Blank.ico")
            Else
                dgvAccessions.Rows.RemoveAt(i)
            End If
        Next
    End Sub

    Private Sub PrintProgress()
        If HasRandomValues() Or txtAccFrom.Text <> "" Or txtAccTo.Text <> "" Or
        IsDate(dtpDateFrom.Text) = True Or IsDate(dtpDateTo.Text) = True Then
            btnPrint.Enabled = True
        Else
            btnPrint.Enabled = False
        End If
    End Sub

    'Private Sub txtDateTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If txtDateTo.Text <> "" Then
    '        If IsDate(txtDateTo.Text) = True Then
    '            txtAccFrom.Text = ""
    '            txtAccTo.Text = ""
    '            ClearRandomAccessions()
    '        Else
    '            txtDateTo.Text = ""
    '        End If
    '    End If
    '    PrintProgress()
    'End Sub

    Private Sub dgvAccessions_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAccessions.CellClick
        If e.ColumnIndex = 0 Then
            If dgvAccessions.Rows(e.RowIndex).Cells(1).Value <> String.Empty AndAlso
            dgvAccessions.Rows(e.RowIndex).Cells(1).Value <> "" Then
                If dgvAccessions.RowCount <> 1 Then
                    dgvAccessions.Rows.RemoveAt(e.RowIndex)
                Else
                    dgvAccessions.Rows(e.RowIndex).Cells(1).Value = ""
                    dgvAccessions.Rows(e.RowIndex).Cells(0).Value =
                        System.Drawing.Image.FromFile(Application.StartupPath &
                        "\Images\Blank.ico")
                End If
            End If
        End If
        PrintProgress()
    End Sub

    Private Sub dgvAccessions_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAccessions.CellEndEdit
        If IsNumeric(dgvAccessions.Rows(e.RowIndex).Cells(1).Value) Then
            txtAccFrom.Text = ""
            txtAccTo.Text = ""
            'txtDateFrom.Text = ""
            'txtDateTo.Text = ""

            ClearDateTimePicker(dtpDateFrom)
            ClearDateTimePicker(dtpDateTo)

            If e.RowIndex = dgvAccessions.RowCount - 1 Then _
            dgvAccessions.RowCount += 1
        Else
            dgvAccessions.Rows(e.RowIndex).Cells(1).Value = ""
            dgvAccessions.Rows(e.RowIndex).Cells(0).Value =
            System.Drawing.Image.FromFile(Application.StartupPath &
            "\Images\Blank.ico")
        End If
        PrintProgress()
    End Sub

    Private Sub dgvAccessions_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAccessions.CellValidated
        If e.ColumnIndex = 1 Then
            If dgvAccessions.Rows(e.RowIndex).Cells(1).Value <> String.Empty AndAlso
            dgvAccessions.Rows(e.RowIndex).Cells(1).Value.ToString <> "" Then
                If IsNumeric(dgvAccessions.Rows(e.RowIndex).Cells(1).Value) Then
                    dgvAccessions.Rows(e.RowIndex).Cells(0).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath &
                    "\Images\Delete.ico")
                    PrintProgress()
                End If
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Function GetRandomAccessions() As String
        Dim Accessions As String = ""
        Dim i As Integer
        For i = 0 To dgvAccessions.RowCount - 1
            If Not dgvAccessions.Rows(i).Cells(1).Value Is Nothing AndAlso
            dgvAccessions.Rows(i).Cells(1).Value.ToString <> "" Then
                Accessions += dgvAccessions.Rows(i).Cells(1).Value.ToString & ","
            End If
        Next
        If Accessions.Length > 0 Then Accessions = Accessions.Substring(0, Len(Accessions) - 1)
        Return Accessions
    End Function

    Private Sub txtAccFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccFrom.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAccTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccTo.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If HasRandomValues() Or txtAccFrom.Text <> "" Or txtAccTo.Text <> "" Or
        IsDate(dtpDateFrom.Text) = True Or IsDate(dtpDateTo.Text) = True Then
            Dim Formula As String = "{Requisitions.Received} = True and " &
            "{Req_TGP.Billed} = False" '& and {Requisitions.ID} in [1006 to 1001];"
            If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                Formula += " and {Requisitions.AccessionDate} in [CDateTime(" &
                CDate(dtpDateFrom.Text).Year & "," & CDate(dtpDateFrom.Text).Month _
                & "," & CDate(dtpDateFrom.Text).Day & ",00,00,01) To CDateTime(" &
                CDate(dtpDateFrom.Text).Year & "," & CDate(dtpDateFrom.Text).Month _
                & "," & CDate(dtpDateFrom.Text).Day & ",23,59,59)];"
            ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                Formula += " and {Requisitions.AccessionDate} in [CDateTime(" &
                CDate(dtpDateTo.Text).Year & "," & CDate(dtpDateTo.Text).Month _
                & "," & CDate(dtpDateTo.Text).Day & ",00,00,01) To CDateTime(" &
                CDate(dtpDateTo.Text).Year & "," & CDate(dtpDateTo.Text).Month _
                & "," & CDate(dtpDateTo.Text).Day & ",23,59,59)];"
            ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                Formula += " and {Requisitions.AccessionDate} in [CDateTime(" &
                CDate(dtpDateFrom.Text).Year & "," & CDate(dtpDateFrom.Text).Month _
                & "," & CDate(dtpDateFrom.Text).Day & ",00,00,01) To CDateTime(" &
                CDate(dtpDateTo.Text).Year & "," & CDate(dtpDateTo.Text).Month _
                & "," & CDate(dtpDateTo.Text).Day & ",23,59,59)];"
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                Formula += " and {Requisitions.ID} = " & Val(txtAccFrom.Text) & ";"
            ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                Formula += " and {Requisitions.ID} = " & Val(txtAccTo.Text) & ";"
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                Formula += " and {Requisitions.ID} in [" & Val(txtAccFrom.Text) &
                " To " & Val(txtAccTo.Text) & "];"
            ElseIf HasRandomValues() Then
                Dim Accessions As String = GetRandomAccessions()
                Formula += " and {Requisitions.ID} in [" & Accessions & "];"
            End If
            Dim UID As String = My.Settings.UID.ToString
            Dim PWD As String = My.Settings.PWD.ToString

            'TODO: Crystal Reports
            '===========================
            'Dim gReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            'gReport.Load(Application.StartupPath & "\Reports\Integrity.RPT")
            'ApplyNewServer(gReport, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)
            'gReport.RecordSelectionFormula = Formula
            'If cmbDestination.SelectedIndex = 0 Then    'Printer
            '    gReport.PrintToPrinter(Val(txtCopies.Text), False, 0, 0)
            '    System.Threading.Thread.Sleep(1000)
            '    gReport.Close()
            '    gReport = Nothing
            'Else    'Screen
            '    frmRV.CRRV.ReportSource = gReport
            '    frmRV.Show()
            'End If
            '===========================
            'dtpDateFrom.Text = ""
            'dtpDateTo.Text = ""

            ClearDateTimePicker(dtpDateFrom)
            ClearDateTimePicker(dtpDateTo)

            txtAccFrom.Text = ""
            txtAccTo.Text = ""
            ClearRandomAccessions()
            dtpDateFrom.Focus()
        End If
    End Sub

    Private Sub txtAccFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccFrom.Validated
        If txtAccFrom.Text <> "" Then
            'txtDateFrom.Text = ""
            'txtDateTo.Text = ""
            ClearDateTimePicker(dtpDateFrom)
            ClearDateTimePicker(dtpDateTo)
            ClearRandomAccessions()
        End If
        PrintProgress()
    End Sub

    Private Sub txtAccTo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAccTo.TextChanged
        If txtAccTo.Text <> "" Then
            'txtDateFrom.Text = ""
            'txtDateTo.Text = ""
            ClearDateTimePicker(dtpDateFrom)
            ClearDateTimePicker(dtpDateTo)
            ClearRandomAccessions()
        End If
        PrintProgress()
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

    Private Sub dtpDateFrom_LostFocus(sender As Object, e As EventArgs) Handles dtpDateFrom.LostFocus, dtpDateTo.LostFocus
        PrintProgress()
    End Sub
End Class
