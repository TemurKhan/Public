Imports System.Data

Public Class frmPreAuth

    Private Sub btnTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTarget.Click
        DisableActions()
        Dim Billis As String = ""
        Dim sSQL As String = ""
        If IsDate(txtDateFrom.Text) Or IsDate(txtDateTo.Text) Or (txtAccFrom.Text <> "" Or txtAccTo.Text <> "" Or HasDiscretes()) Then
            sSQL = "Select * from Payers where PreAuthFile <> '' and ID in (Select distinct " & _
            "PrimePayer_ID from Requisitions where Rejected = 0 and BillingType_ID = 1"
            '
            If IsDate(txtDateFrom.Text) And Not IsDate(txtDateTo.Text) Then
                sSQL += " and ReceivedTime between '" & txtDateFrom.Text & _
                "' and '" & txtDateFrom.Text & " 23:59:00'"
            ElseIf Not IsDate(txtDateFrom.Text) And IsDate(txtDateTo.Text) Then
                sSQL += " and ReceivedTime between '" & txtDateTo.Text & _
               "' and '" & txtDateTo.Text & " 23:59:00'"
            ElseIf IsDate(txtDateFrom.Text) And IsDate(txtDateTo.Text) Then
                sSQL += " and ReceivedTime between '" & txtDateFrom.Text & _
                "' and '" & txtDateTo.Text & " 23:59:00'"
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                sSQL += " and ID = " & Val(txtAccFrom.Text)
            ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                sSQL += " and ID = " & Val(txtAccTo.Text)
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                sSQL += " and ID between " & Val(txtAccFrom.Text) & _
                " and " & Val(txtAccTo.Text)
            ElseIf HasDiscretes() Then
                Dim VALS As String = ""
                For i As Integer = 0 To dgvDiscrete.RowCount - 1
                    If dgvDiscrete.Rows(i).Cells(0).Value IsNot Nothing _
                    AndAlso Trim(dgvDiscrete.Rows(i).Cells(0).Value) <> "" Then
                        VALS += Trim(dgvDiscrete.Rows(i).Cells(0).Value) & ", "
                    End If
                Next
                If VALS.EndsWith(", ") Then VALS = Microsoft.VisualBasic.Mid(VALS, 1, Len(VALS) - 2)
                If VALS <> "" Then
                    sSQL += " and ID in (" & VALS & ")"
                End If
            End If
            '
            If TB.SelectedTab Is TB.TabPages(0) Then    'Requests
                If chkUnprocessed.Checked Then
                    sSQL += " and not ID in (Select distinct Accession_ID from Req_PreAuth))"
                Else
                    sSQL += ")"
                End If
            ElseIf TB.SelectedTab Is TB.TabPages(1) Then    'Responsed
                sSQL += " and ID in (Select distinct Accession_ID from Req_PreAuth))"
            End If
            '
            lstTargets.Items.Clear()
            Dim cnt As New SqlClient.SqlConnection(connString)
            cnt.Open()
            Dim cmdt As New SqlClient.SqlCommand(sSQL, cnt)
            cmdt.CommandType = CommandType.Text
            Dim drt As SqlClient.SqlDataReader = cmdt.ExecuteReader
            If drt.HasRows Then
                While drt.Read
                    lstTargets.Items.Add(New MyList(drt("PayerName") & " [" & drt("ID") & "]", drt("ID")))
                End While
            End If
            cnt.Close()
            cnt = Nothing
            If lstTargets.Items.Count > 0 Then btnSellT_Click(Nothing, Nothing)
        End If
        EnableActions()
    End Sub

    Private Function HasDiscretes() As Boolean
        Dim HasVal As Boolean = False
        For i As Integer = 0 To dgvDiscrete.RowCount - 1
            If dgvDiscrete.Rows(i).Cells(0).Value IsNot Nothing _
            AndAlso Trim(dgvDiscrete.Rows(i).Cells(0).Value) <> "" Then
                HasVal = True
                Exit For
            End If
        Next
        Return HasVal
    End Function

    Private Sub DisableActions()
        btnTarget.Enabled = False
        btnLoad.Enabled = False
        btnPrint.Enabled = False
        btnSave.Enabled = False
    End Sub

    Private Sub EnableActions()
        btnTarget.Enabled = True
        btnLoad.Enabled = True
        'btnPrint.Enabled = False
        'btnSave.Enabled = False
    End Sub

    Private Sub btnSellT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSellT.Click
        For i As Integer = 0 To lstTargets.Items.Count - 1
            lstTargets.SetItemChecked(i, True)
        Next
    End Sub

    Private Sub btnDeselT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeselT.Click
        For i As Integer = 0 To lstTargets.Items.Count - 1
            lstTargets.SetItemChecked(i, False)
        Next
    End Sub

    Private Sub txtAccFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccFrom.Validated
        If txtAccFrom.Text <> "" Then
            txtDateFrom.Text = ""
            txtDateTo.Text = ""
            dgvDiscrete.Rows.Clear()
            dgvDiscrete.RowCount = 1
        End If
    End Sub

    Private Sub txtAccTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccTo.Validated
        If txtAccTo.Text <> "" Then
            txtDateFrom.Text = ""
            txtDateTo.Text = ""
            dgvDiscrete.Rows.Clear()
            dgvDiscrete.RowCount = 1
        End If
    End Sub

    Private Sub txtDateFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDateFrom.Validated
        If UserEnteredText(txtDateFrom) <> "" Then
            If IsDate(txtDateFrom.Text) = True Then
                txtAccFrom.Text = ""
                txtAccTo.Text = ""
                dgvDiscrete.Rows.Clear()
                dgvDiscrete.RowCount = 1
            Else
                MsgBox("Invalid Date")
                txtDateFrom.Text = ""
                txtDateFrom.Focus()
            End If
        End If
    End Sub

    Private Sub txtDateTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDateTo.Validated
        If UserEnteredText(txtDateTo) <> "" Then
            If IsDate(txtDateTo.Text) = True Then
                txtAccFrom.Text = ""
                txtAccTo.Text = ""
                dgvDiscrete.Rows.Clear()
                dgvDiscrete.RowCount = 1
            Else
                MsgBox("Invalid Date")
                txtDateTo.Text = ""
                txtDateTo.Focus()
            End If
        End If
    End Sub

    Private Sub dgvDiscrete_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDiscrete.CellEndEdit
        If Trim(dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) <> "" Then
            If IsNumeric(Trim(dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)) = False Then
                MsgBox("Only digits are allowed.")
                dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
            Else
                If IsDuplicate(Trim(dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)) Then
                    MsgBox("Duplicate Entry is not allowed.")
                    dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
                End If
            End If
            If e.RowIndex = dgvDiscrete.RowCount - 1 Then
                dgvDiscrete.Rows.Add()
                SendKeys.Send("{ENTER}")
            End If
            '
            txtAccFrom.Text = ""
            txtAccTo.Text = ""
            txtDateFrom.Text = ""
            txtDateTo.Text = ""
        Else
            If e.RowIndex < dgvDiscrete.RowCount - 1 Then
                dgvDiscrete.Rows.RemoveAt(e.RowIndex)
            End If
        End If
    End Sub

    Private Function IsDuplicate(ByVal AccID As Long) As Boolean
        Dim i As Integer
        Dim CT As Integer = 0
        For i = 0 To dgvDiscrete.RowCount - 1
            If dgvDiscrete.Rows(i).Cells(0).Value = AccID.ToString Then CT += 1
        Next
        If CT > 1 Then
            IsDuplicate = True
        Else
            IsDuplicate = False
        End If
    End Function

    Private Sub dgvDiscrete_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvDiscrete.CellMouseUp
        If e.Button = MouseButtons.Right Then
            If Clipboard.ContainsText Then
                dgvDiscrete.Rows.Clear()
                Dim Accs() As String = Split(Clipboard.GetText, vbCrLf)
                For i As Integer = 0 To Accs.Length - 1
                    If Trim(Accs(i)) <> "" Then
                        If dgvDiscrete.RowCount = 0 Then dgvDiscrete.Rows.Add()
                        If dgvDiscrete.Rows(dgvDiscrete.RowCount - 1).Cells(0).Value _
                        = "" Then
                            dgvDiscrete.Rows(dgvDiscrete.RowCount - 1).Cells(0).Value = Trim(Accs(i))
                        Else
                            dgvDiscrete.Rows.Add()
                            dgvDiscrete.Rows(dgvDiscrete.RowCount - 1).Cells(0).Value = Trim(Accs(i))
                        End If
                    End If
                Next
                If dgvDiscrete.Rows(0).Cells(0).Value <> "" Then
                    txtAccFrom.Text = ""
                    txtAccTo.Text = ""
                    txtDateFrom.Text = ""
                    txtDateTo.Text = ""
                    dgvDiscrete.Rows.Add()
                End If
            End If
        End If
    End Sub

    Private Function GetReqSelected() As Integer
        Dim CNT As Integer = 0
        For i As Integer = 0 To dgvRequests.RowCount - 1
            If CType(dgvRequests.Rows(i).Cells(0).Value, Boolean) = True Then CNT += 1
        Next
        Return CNT
    End Function

    Private Sub UpdateDestination()
        cmbDestination.Items.Clear()
        cmbDestination.SelectedIndex = -1
        Dim SelCount As Integer = GetReqSelected()
        If SelCount > 0 And SelCount < 5 Then
            cmbDestination.Items.Add("Printer")
            cmbDestination.Items.Add("Screen")
            cmbDestination.SelectedIndex = 1
        ElseIf SelCount > 4 Then
            cmbDestination.Items.Add("Printer")
            cmbDestination.SelectedIndex = 0
        Else
            cmbDestination.Items.Clear()
            cmbDestination.SelectedIndex = -1
        End If
        UpdateProcessProgress()
    End Sub

    Private Sub UpdateProcessProgress()
        If cmbDestination.SelectedIndex <> -1 Then
            btnPrint.Enabled = True
        Else
            btnPrint.Enabled = False
        End If
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        DisableActions()
        Dim sSQL As String = ""
        Dim ItemX As MyList
        Dim Billis As String = ""
        Dim Billee As String = ""
        Dim Client As String = ""
        dgvRequests.Rows.Clear()
        dgvResponses.Rows.Clear()
        My.Application.DoEvents()
        If (IsDate(txtDateFrom.Text) Or IsDate(txtDateTo.Text) Or txtAccFrom.Text <> "" Or
        txtAccTo.Text <> "" Or HasDiscretes()) And lstTargets.CheckedItems.Count > 0 Then
            If TB.SelectedTab Is TB.TabPages(0) Then    'Requests
                sSQL = "Select 1 as selected, a.ID as AccID, b.LastName + ', ' + b.FirstName as [Patient (L, F)], " &
                "IsNull(Convert(nvarchar, a.ReceivedTime, 101), '') as [Rec Date], IsNull(convert(nvarchar, (Select " &
                "min(SourceDate) from Specimens where Accession_ID = a.ID), 101), '') as [Svc Date], c.IsIndividual, " &
                "c.LastName_BSN, c.FirstName, c.Degree, c.ID as ProvID, d.ID as PayerID, d.PayerName from Payers d " &
                "inner join (Providers c inner join (Requisitions a inner join Patients b on b.ID = a.Patient_ID) on " &
                "a.OrderingProvider_ID = c.ID) on d.ID = a.PrimePayer_ID where a.Rejected = 0 and a.BillingType_ID = 1"
                For i As Integer = 0 To lstTargets.Items.Count - 1
                    If lstTargets.GetItemChecked(i) = True Then
                        ItemX = lstTargets.Items(i)
                        Billis += ItemX.ItemData.ToString & ", "
                    End If
                Next
                If Billis.EndsWith(", ") Then Billis = Billis.Substring(0, Len(Billis) - 2)
                sSQL += " and a.ID in (Select distinct Accession_ID from Req_Coverage " &
                "where Preference = 'P' and Payer_ID in (" & Billis & "))"
                '
                If chkUnprocessed.Checked = True Then
                    sSQL += " and not a.ID in (Select distinct Accession_ID from Req_PreAuth)"
                End If
            ElseIf TB.SelectedTab Is TB.TabPages(1) Then    'Responses
                sSQL = "Select 1 as selected, a.ID as AccID, b.LastName + ', ' + b.FirstName as [Patient (L, F)], " &
                "IsNull(Convert(nvarchar, a.ReceivedTime, 101), '') as [Rec Date], IsNull(convert(nvarchar, (Select " &
                "min(SourceDate) from Specimens where Accession_ID = a.ID), 101), '') as [Svc Date], d.ID as PayerID, " &
                "d.PayerName, IsNull(c.Response, '') as Response from Payers d inner join (Req_PreAuth c right outer " &
                "join (Requisitions a inner join Patients b on b.ID = a.Patient_ID) on a.ID = c.Accession_ID) on d.ID " &
                "= a.PrimePayer_ID where a.Rejected = 0 and a.BillingType_ID = 1"
                For i As Integer = 0 To lstTargets.Items.Count - 1
                    If lstTargets.GetItemChecked(i) = True Then
                        ItemX = lstTargets.Items(i)
                        Billis += ItemX.ItemData.ToString & ", "
                    End If
                Next
                If Billis.EndsWith(", ") Then Billis = Billis.Substring(0, Len(Billis) - 2)
                sSQL += " and a.ID in (Select distinct Accession_ID from Req_Coverage " &
                "where Preference = 'P' and Payer_ID in (" & Billis & "))"
                '
                sSQL += " and a.ID in (Select distinct Accession_ID from Req_PreAuth)"
            End If
            '
            If IsDate(txtDateFrom.Text) And Not IsDate(txtDateTo.Text) Then
                sSQL += " and a.ReceivedTime between '" & txtDateFrom.Text &
                "' and '" & txtDateFrom.Text & " 23:59:00'"
            ElseIf Not IsDate(txtDateFrom.Text) And IsDate(txtDateTo.Text) Then
                sSQL += " and a.ReceivedTime between '" & txtDateTo.Text &
                "' and '" & txtDateTo.Text & " 23:59:00'"
            ElseIf IsDate(txtDateFrom.Text) And IsDate(txtDateTo.Text) Then
                sSQL += " and a.ReceivedTime between '" & txtDateFrom.Text &
                "' and '" & txtDateTo.Text & " 23:59:00'"
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                sSQL += " and a.ID = " & Val(txtAccFrom.Text)
            ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                sSQL += " and a.ID = " & Val(txtAccTo.Text)
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                If Val(txtAccFrom.Text) < Val(txtAccTo.Text) Then
                    sSQL += " and a.ID between " & Val(txtAccFrom.Text) & " and " & Val(txtAccTo.Text)
                End If
            ElseIf HasDiscretes() Then
                Dim VALS As String = ""
                For i As Integer = 0 To dgvDiscrete.RowCount - 1
                    If dgvDiscrete.Rows(i).Cells(0).Value IsNot Nothing _
                    AndAlso Trim(dgvDiscrete.Rows(i).Cells(0).Value) <> "" Then
                        VALS += Trim(dgvDiscrete.Rows(i).Cells(0).Value) & ", "
                    End If
                Next
                If VALS.EndsWith(", ") Then VALS = Microsoft.VisualBasic.Mid(VALS, 1, Len(VALS) - 2)
                If VALS <> "" Then sSQL += " and a.ID in (" & VALS & ")"
            End If
            '
            Try
                Dim cnt As New SqlClient.SqlConnection(connString)
                cnt.Open()
                Dim cmdt As New SqlClient.SqlCommand(sSQL, cnt)
                cmdt.CommandTimeout = 90
                cmdt.CommandType = CommandType.Text
                Dim drt As SqlClient.SqlDataReader = cmdt.ExecuteReader
                If drt.HasRows Then
                    While drt.Read
                        If TB.SelectedTab Is TB.TabPages(0) Then    'Requests
                            If drt("IsIndividual") = False Then
                                Client = drt("LastName_BSN") & " [" & drt("ProvID") & "]"
                            Else
                                Client = drt("LastName_BSN") & ", " & drt("FirstName")
                                If drt("Degree") IsNot DBNull.Value AndAlso
                                Trim(drt("Degree")) <> "" Then Client += " " & Trim(drt("Degree"))
                                Client += " [" & drt("ProvID") & "]"
                            End If
                            Billee = drt("PayerName") & " [" & drt("PayerID") & "]"
                            dgvRequests.Rows.Add(drt("Selected"), drt("AccID"), drt("Patient (L, F)"),
                            drt("Rec Date"), drt("Svc Date"), Client, Billee)
                        ElseIf TB.SelectedTab Is TB.TabPages(1) Then    'Responses
                            Billee = drt("PayerName") & " [" & drt("PayerID") & "]"
                            dgvResponses.Rows.Add(drt("Selected"), drt("AccID"), drt("Patient (L, F)"),
                            drt("Rec Date"), drt("Svc Date"), Billee, drt("Response"))
                        End If
                    End While
                End If
                cnt.Close()
                cnt = Nothing
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Prolis")
            End Try
            If dgvRequests.RowCount > 0 Then btnPrint.Enabled = True
            txtReqCount.Text = dgvRequests.RowCount
            '
            txtResCount.Text = dgvResponses.RowCount
            UpdateDestination()
            UpdateSave()
        Else
            dgvRequests.Rows.Clear()
            dgvResponses.Rows.Clear()
        End If
        EnableActions()
    End Sub

    Private Sub btnSelQs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelQs.Click
        For i As Integer = 0 To dgvRequests.RowCount - 1
            dgvRequests.Rows(i).Cells(0).Value = True
        Next
        UpdateDestination()
    End Sub

    Private Sub btnDeselQS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeselQS.Click
        For i As Integer = 0 To dgvRequests.RowCount - 1
            dgvRequests.Rows(i).Cells(0).Value = False
        Next
        UpdateDestination()
    End Sub

    Private Sub btnSelS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelS.Click
        For i As Integer = 0 To dgvResponses.RowCount - 1
            dgvResponses.Rows(i).Cells(0).Value = True
        Next
    End Sub

    Private Sub btnDeselS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeselS.Click
        For i As Integer = 0 To dgvResponses.RowCount - 1
            dgvResponses.Rows(i).Cells(0).Value = False
        Next
    End Sub

    Private Sub dgvRequests_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvRequests.CellEndEdit
        If e.ColumnIndex = 0 Then   'select
            UpdateDestination()
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim Accs As String = ""
        For i As Integer = 0 To dgvRequests.RowCount - 1
            If CType(dgvRequests.Rows(i).Cells(0).Value, Boolean) = True Then
                Accs += dgvRequests.Rows(i).Cells(1).Value & ", "
            End If
        Next
        If Accs.EndsWith(", ") Then Accs = Accs.Substring(0, Len(Accs) - 2)
        If Accs <> "" Then
            Dim MyPrinter As String = ""
            If cmbDestination.SelectedItem.ToString = "Printer" Then
                If DialogResult.OK = PrintDialog1.ShowDialog Then
                    MyPrinter = PrintDialog1.PrinterSettings.PrinterName
                End If
            End If
            Dim RPT As String = ""
            Dim AccIDs() As String
            Dim ItemX As MyList
            For i As Integer = 0 To lstTargets.Items.Count - 1
                If lstTargets.GetItemChecked(i) = True Then
                    ItemX = lstTargets.Items(i)
                    RPT = GetPreAuthFile(ItemX.ItemData)
                    If RPT <> "" AndAlso IO.File.Exists(GetReportPath(RPT)) Then
                        ''If RPT <> "" AndAlso IO.File.Exists("//prolisresources.americansoftsolutions.com/Reports/" & RPT) Then

                        'TODO: Crystal Reports Code
                        '=============================================
                        'Dim oRPT As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                        'oRPT.Load(GetReportPath(RPT), CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault)
                        'ApplyNewServer(oRPT, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)
                        'oRPT.RecordSelectionFormula = "{Payers.ID} = " & ItemX.ItemData & " and {Requisitions.ID} in [" & Accs & "];"
                        'If cmbDestination.SelectedItem.ToString = "Screen" Then
                        '    frmRV.CRRV.ReportSource = oRPT
                        '    frmRV.Show()
                        'Else    'printer
                        '    oRPT.PrintOptions.PrinterName = MyPrinter
                        '    oRPT.PrintToPrinter(1, True, 0, 0)
                        '    '
                        '    AccIDs = Split(Accs, ", ")
                        '    For n As Integer = 0 To AccIDs.Length - 1
                        '        ExecuteSqlProcedure("If not Exists (Select * from Req_PreAuth where Accession_ID = " &
                        '        AccIDs(n) & " and Payer_ID = " & ItemX.ItemData & ") Insert into Req_PreAuth (Accession_ID, " &
                        '        "Payer_ID, Response) values (" & AccIDs(n) & ", " & ItemX.ItemData & ", '')")
                        '    Next
                        'End If
                        '=============================================
                    Else
                        MsgBox("The applicable PreAuth report does not exist in the 'Prolis\Reports\' folder.")
                    End If
                End If
            Next
        End If

    End Sub

    Private Function GetPreAuthFile(ByVal PayerID As Long) As String
        Dim RPT As String = ""
        Dim cnrpt As New SqlClient.SqlConnection(connString)
        cnrpt.Open()
        Dim cmdrpt As New SqlClient.SqlCommand("Select * from Payers where ID = " & PayerID, cnrpt)
        cmdrpt.CommandTimeout = 90
        cmdrpt.CommandType = CommandType.Text
        Dim drrpt As SqlClient.SqlDataReader = cmdrpt.ExecuteReader
        If drrpt.HasRows Then
            While drrpt.Read
                If drrpt("PreAuthFile") IsNot DBNull.Value Then
                    RPT = Trim(drrpt("PreAuthFile"))
                End If
            End While
        End If
        cnrpt.Close()
        cnrpt = Nothing
        Return RPT
    End Function

    Private Sub chkUnprocessed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUnprocessed.CheckedChanged
        If chkUnprocessed.Checked = True Then
            chkUnprocessed.Text = "Unprocessed"
        Else
            chkUnprocessed.Text = "All"
        End If
    End Sub

    Private Sub dgvResponses_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvResponses.CellEndEdit
        UpdateSave()
    End Sub

    Private Sub UpdateSave()
        Dim CNT As Integer = 0
        For i As Integer = 0 To dgvResponses.RowCount - 1
            If CType(dgvResponses.Rows(i).Cells(0).Value, Boolean) = True Then CNT += 1
        Next
        If CNT > 0 Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        For i As Integer = 0 To dgvResponses.RowCount - 1
            If CType(dgvResponses.Rows(i).Cells(0).Value, Boolean) = True Then
                ExecuteSqlProcedure("If Exists (Select * from Req_PreAuth where " & _
                "Accession_ID = " & dgvResponses.Rows(i).Cells(1).Value & ") Update " & _
                "Req_PreAuth set Response = '" & Trim(dgvResponses.Rows(i).Cells(6).Value) & _
                "' where Accession_ID = " & dgvResponses.Rows(i).Cells(1).Value)
            End If
        Next
        dgvResponses.Rows.Clear()
        txtResCount.Text = dgvResponses.RowCount
        UpdateSave()
    End Sub
End Class