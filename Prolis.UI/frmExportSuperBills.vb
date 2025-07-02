Imports System.Windows.Forms
Imports System.Data

Public Class frmExportSuperBills
    Private FPATH As String = ""
    Private Accessions() As String = {""}
    Private Exported As Integer
    Private Failed As Integer

    Private Sub frmExportSuperBills_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If BW.IsBusy Then
            e.Cancel = True
        Else
            e.Cancel = False
        End If
    End Sub

    Private Sub frmExportSuperBills_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dgvDiscrete.RowCount = 1
        cmbDateType.SelectedIndex = 0
        lblFrom.Text += " (" & SystemConfig.DateFormat & ")"
        lblTo.Text += " (" & SystemConfig.DateFormat & ")"
        If LIC.BillExport Then
            cmbFormat.Items.Add("Screen(Printable)")
            cmbFormat.Items.Add("HL7 Protocol")
        Else
            cmbFormat.Items.Add("Screen(Printable)")
        End If

        ConfigureDateTimePicker(dtpDateFrom)
        ConfigureDateTimePicker(dtpDateTo)

        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Function IsDuplicate(ByVal AccID As Long) As Boolean
        Dim i As Integer
        Dim CT As Integer = 0
        If dgvDiscrete.RowCount > 0 Then
            For i = 0 To dgvDiscrete.RowCount - 1
                If dgvDiscrete.Rows(i).Cells(0).Value = AccID.ToString Then CT += 1
            Next
            If CT > 1 Then
                IsDuplicate = True
            Else
                IsDuplicate = False
            End If
        Else
            IsDuplicate = False
        End If
    End Function

    Private Sub DiscreteClear()
        dgvDiscrete.Rows.Clear()
        dgvDiscrete.RowCount = 1
    End Sub

    Private Sub Update_Progress()
        If txtAccFrom.Text <> "" Or txtAccFrom.Text <> "" _
        Or HasDiscreteValues() = True Or IsDate(dtpDateFrom.Text) Or
        IsDate(dtpDateTo.Text) Then
            btnProcess.Enabled = True
        Else
            btnProcess.Enabled = False
        End If
    End Sub
    Private Function HasDiscreteValues() As Boolean
        Dim HasVal As Boolean = False
        Dim i As Integer
        If dgvDiscrete.RowCount > 0 Then
            For i = 0 To dgvDiscrete.RowCount - 1
                If dgvDiscrete.Rows(i).Cells(0).Value <> "" Then
                    HasVal = True
                    Exit For
                End If
            Next
        End If
        HasDiscreteValues = HasVal
    End Function

    Private Sub dgvDiscrete_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDiscrete.CellEndEdit
        If Trim(dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) <> "" AndAlso _
        IsNumeric(Trim(dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)) = False Then
            MsgBox("Only digits are allowed.")
            dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
        ElseIf Trim(dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) <> "" AndAlso _
        IsDuplicate(Trim(dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)) Then
            MsgBox("Duplicate Entry is not allowed.")
            dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
        ElseIf Trim(dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) <> "" AndAlso _
        IsNumeric(Trim(dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)) = True Then
            txtAccFrom.Text = "" : txtAccTo.Text = ""

            ClearDateTimePicker(dtpDateFrom)
            ClearDateTimePicker(dtpDateTo)

            'txtDateFrom.Text = "" : txtDateTo.Text = ""
            dgvDiscrete.Rows.Add()
            SendKeys.Send("{ENTER}")
        End If
        Update_Progress()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If BW.IsBusy Then
            Dim RetVal As Integer = MsgBox("Data Synchronization in progress. " & _
            "Do you want to cancel the process?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
            If RetVal = vbYes Then
                BW.CancelAsync()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        If ((IsDate(dtpDateFrom.Text) Or IsDate(dtpDateTo.Text)) OrElse
        (txtAccFrom.Text <> "" And txtAccTo.Text <> "") OrElse HasDiscreteValues()) _
        AndAlso (cmbBillType.SelectedIndex >= 0 And lstTargets.CheckedItems.Count > 0) _
        AndAlso cmbFormat.SelectedIndex >= 0 Then
            Dim sSQL As String = ""
            Dim Formula As String = ""
            Dim ItemX As MyList
            Dim i As Integer
            If cmbBillType.SelectedIndex = 0 Then   'Client
                sSQL = "Select ID as AccID from Requisitions where Received <> 0 " &
                "and BillingType_ID = 0"
            ElseIf cmbBillType.SelectedIndex = 1 Then   'Insurance
                sSQL = "Select ID as AccID from Requisitions where Received <> 0 " &
                "and BillingType_ID = 1"
            ElseIf cmbBillType.SelectedIndex = 2 Then   'Patient
                sSQL = "Select ID as AccID from Requisitions where Received <> 0 " &
                "and BillingType_ID = 2"
            Else    'All
                sSQL = "Select ID as AccID from Requisitions where Received <> 0"
            End If
            '
            If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                If cmbDateType.SelectedIndex = 0 Then   'ACC
                    sSQL += " and AccessionDate between '" & Format(CDate(dtpDateFrom.Text &
                    " 00:00:00"), "MM/dd/yyyy HH:mm") & "' and '" & Format(CDate(dtpDateFrom.Text _
                    & " 23:59:00"), "MM/dd/yyyy HH:mm") & "'"
                    Formula = "{Requisitions.AccessionDate} in [Date(" & CDate(dtpDateFrom.Text).Year _
                    & ", " & CDate(dtpDateFrom.Text).Month & ", " & CDate(dtpDateFrom.Text).Day &
                    ") to Date(" & CDate(dtpDateFrom.Text).Year & ", " & CDate(dtpDateFrom.Text).Month _
                    & ", " & CDate(dtpDateFrom.Text).Day & ")]"
                ElseIf cmbDateType.SelectedIndex = 1 Then   'COLL
                    sSQL += " and in (Select distinct Accession_ID from Specimens where " &
                    "SourceDate between '" & Format(CDate(dtpDateFrom.Text & " 00:00:00"),
                    "MM/dd/yyyy HH:mm") & "' and '" & Format(CDate(dtpDateFrom.Text &
                    " 23:59:00"), "MM/dd/yyyy HH:mm") & "')"
                    Formula = "{Specimens.SourceDate} in [Date(" & CDate(dtpDateFrom.Text).Year &
                    ", " & CDate(dtpDateFrom.Text).Month & ", " & CDate(dtpDateFrom.Text).Day &
                    ") to Date(" & CDate(dtpDateFrom.Text).Year & ", " & CDate(dtpDateFrom.Text).Month _
                    & ", " & CDate(dtpDateFrom.Text).Day & ")]"
                ElseIf cmbDateType.SelectedIndex = 2 Then   'Rec
                    sSQL += " and ReceivedTime between '" & Format(CDate(dtpDateFrom.Text &
                    " 00:00:00"), "MM/dd/yyyy HH:mm") & "' and '" & Format(CDate(dtpDateFrom.Text _
                    & " 23:59:00"), "MM/dd/yyyy HH:mm") & "'"
                    Formula = "{Requisitions.ReceivedTime} in [Date(" & CDate(dtpDateFrom.Text).Year &
                    ", " & CDate(dtpDateFrom.Text).Month & ", " & CDate(dtpDateFrom.Text).Day &
                    ") to Date(" & CDate(dtpDateFrom.Text).Year & ", " & CDate(dtpDateFrom.Text).Month _
                    & ", " & CDate(dtpDateFrom.Text).Day & ")]"
                ElseIf cmbDateType.SelectedIndex = 3 Then   'Final Date
                    sSQL += " and Reported_Final between '" & Format(CDate(dtpDateFrom.Text &
                    " 00:00:00"), "MM/dd/yyyy HH:mm") & "' and '" & Format(CDate(dtpDateFrom.Text _
                    & " 23:59:00"), "MM/dd/yyyy HH:mm") & "'"
                    Formula = "{Requisitions.Reported_Final} in [Date(" & CDate(dtpDateFrom.Text).Year &
                    ", " & CDate(dtpDateFrom.Text).Month & ", " & CDate(dtpDateFrom.Text).Day &
                    ") to Date(" & CDate(dtpDateFrom.Text).Year & ", " & CDate(dtpDateFrom.Text).Month _
                    & ", " & CDate(dtpDateFrom.Text).Day & ")]"
                Else                                        'Svc
                    sSQL += " and ID in (Select distinct Accession_ID from Req_Billable where " &
                    "Svc_Date between '" & Format(CDate(dtpDateFrom.Text & " 00:00:00"),
                    "MM/dd/yyyy HH:mm") & "' and '" & Format(CDate(dtpDateFrom.Text &
                    " 23:59:00"), "MM/dd/yyyy HH:mm") & "')"
                    Formula = "{Req_Billable.Svc_Date} in [Date(" & CDate(dtpDateFrom.Text).Year &
                    ", " & CDate(dtpDateFrom.Text).Month & ", " & CDate(dtpDateFrom.Text).Day &
                    ") to Date(" & CDate(dtpDateFrom.Text).Year & ", " & CDate(dtpDateFrom.Text).Month _
                    & ", " & CDate(dtpDateFrom.Text).Day & ")]"
                End If
            ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                If cmbDateType.SelectedIndex = 0 Then   'ACC
                    sSQL += " and AccessionDate between '" & Format(CDate(dtpDateTo.Text &
                    " 00:00:00"), "MM/dd/yyyy HH:mm") & "' and '" & Format(CDate(dtpDateTo.Text _
                    & " 23:59:00"), "MM/dd/yyyy HH:mm") & "'"
                    Formula = "{Requisitions.AccessionDate} in [Date(" & CDate(dtpDateTo.Text).Year &
                    ", " & CDate(dtpDateTo.Text).Month & ", " & CDate(dtpDateTo.Text).Day &
                    ") to Date(" & CDate(dtpDateTo.Text).Year & ", " & CDate(dtpDateTo.Text).Month _
                    & ", " & CDate(dtpDateTo.Text).Day & ")]"
                ElseIf cmbDateType.SelectedIndex = 1 Then   'COLL
                    sSQL += " and ID in (Select distinct Accession_ID from Specimens where " &
                    "SourceDate between '" & Format(CDate(dtpDateTo.Text & " 00:00:00"),
                    "MM/dd/yyyy HH:mm") & "' and '" & Format(CDate(dtpDateTo.Text &
                    " 23:59:00"), "MM/dd/yyyy HH:mm") & "')"
                    Formula = "{Specimens.SourceDate} in [Date(" & CDate(dtpDateTo.Text).Year &
                    ", " & CDate(dtpDateTo.Text).Month & ", " & CDate(dtpDateTo.Text).Day &
                    ") to Date(" & CDate(dtpDateTo.Text).Year & ", " & CDate(dtpDateTo.Text).Month _
                    & ", " & CDate(dtpDateTo.Text).Day & ")]"
                ElseIf cmbDateType.SelectedIndex = 2 Then   'Rec
                    sSQL += " and ReceivedTime between '" & Format(CDate(dtpDateTo.Text &
                    " 00:00:00"), "MM/dd/yyyy HH:mm") & "' and '" & Format(CDate(dtpDateTo.Text _
                    & " 23:59:00"), "MM/dd/yyyy HH:mm") & "'"
                    Formula = "{Requisitions.ReceivedTime} in [Date(" & CDate(dtpDateTo.Text).Year &
                    ", " & CDate(dtpDateTo.Text).Month & ", " & CDate(dtpDateTo.Text).Day &
                    ") to Date(" & CDate(dtpDateTo.Text).Year & ", " & CDate(dtpDateTo.Text).Month _
                    & ", " & CDate(dtpDateTo.Text).Day & ")]"
                ElseIf cmbDateType.SelectedIndex = 3 Then   'Final Date
                    sSQL += " and Reported_Final between '" & Format(CDate(dtpDateTo.Text &
                    " 00:00:00"), "MM/dd/yyyy HH:mm") & "' and '" & Format(CDate(dtpDateTo.Text _
                    & " 23:59:00"), "MM/dd/yyyy HH:mm") & "'"
                    Formula = "{Requisitions.Reported_Final} in [Date(" & CDate(dtpDateTo.Text).Year &
                    ", " & CDate(dtpDateTo.Text).Month & ", " & CDate(dtpDateTo.Text).Day &
                    ") to Date(" & CDate(dtpDateTo.Text).Year & ", " & CDate(dtpDateTo.Text).Month _
                    & ", " & CDate(dtpDateTo.Text).Day & ")]"
                Else                                        'Svc
                    sSQL += " and ID in (Select distinct Accession_ID from Req_Billable " &
                    "where Svc_Date between '" & Format(CDate(dtpDateTo.Text & " 00:00:00"),
                    "MM/dd/yyyy HH:mm") & "' and '" & Format(CDate(dtpDateTo.Text &
                    " 23:59:00"), "MM/dd/yyyy HH:mm") & "')"
                    Formula = "{Req_Billable.Svc_Date} in [Date(" & CDate(dtpDateTo.Text).Year &
                    ", " & CDate(dtpDateTo.Text).Month & ", " & CDate(dtpDateTo.Text).Day &
                    ") to Date(" & CDate(dtpDateTo.Text).Year & ", " & CDate(dtpDateTo.Text).Month _
                    & ", " & CDate(dtpDateTo.Text).Day & ")]"
                End If
            ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                If cmbDateType.SelectedIndex = 0 Then   'ACC
                    sSQL += " and AccessionDate between '" & dtpDateFrom.Text & "' and '" &
                    dtpDateTo.Text & " 23:59:00'"
                    Formula = "{Requisitions.AccessionDate} in [Date(" & CDate(dtpDateFrom.Text).Year &
                    ", " & CDate(dtpDateFrom.Text).Month & ", " & CDate(dtpDateFrom.Text).Day &
                    ") to Date(" & CDate(dtpDateTo.Text).Year & ", " & CDate(dtpDateTo.Text).Month _
                    & ", " & CDate(dtpDateTo.Text).Day & ")]"
                ElseIf cmbDateType.SelectedIndex = 1 Then   'COLL
                    sSQL += " and ID in (Select distinct Accession_ID from Specimens where " &
                    "SourceDate between '" & dtpDateFrom.Text & "' and '" & dtpDateTo.Text & " 23:59:00')"
                    Formula = "{Specimens.SourceDate} in [Date(" & CDate(dtpDateFrom.Text).Year &
                    ", " & CDate(dtpDateFrom.Text).Month & ", " & CDate(dtpDateFrom.Text).Day &
                    ") to Date(" & CDate(dtpDateTo.Text).Year & ", " & CDate(dtpDateTo.Text).Month _
                    & ", " & CDate(dtpDateTo.Text).Day & ")]"
                ElseIf cmbDateType.SelectedIndex = 2 Then   'Rec
                    sSQL += " and ReceivedTime between '" & dtpDateFrom.Text & "' and '" &
                    dtpDateTo.Text & " 23:59:00'"
                    Formula = "{Requisitions.ReceivedTime} in [Date(" & CDate(dtpDateFrom.Text).Year &
                    ", " & CDate(dtpDateFrom.Text).Month & ", " & CDate(dtpDateFrom.Text).Day &
                    ") to Date(" & CDate(dtpDateTo.Text).Year & ", " & CDate(dtpDateTo.Text).Month _
                    & ", " & CDate(dtpDateTo.Text).Day & ")]"
                ElseIf cmbDateType.SelectedIndex = 3 Then   'Final Date
                    sSQL += " and Reported_Final between '" & dtpDateFrom.Text & "' and '" &
                    dtpDateTo.Text & " 23:59:00'"
                    Formula = "{Requisitions.Reported_Final} in [Date(" & CDate(dtpDateFrom.Text).Year &
                    ", " & CDate(dtpDateFrom.Text).Month & ", " & CDate(dtpDateFrom.Text).Day &
                    ") to Date(" & CDate(dtpDateTo.Text).Year & ", " & CDate(dtpDateTo.Text).Month _
                    & ", " & CDate(dtpDateTo.Text).Day & ")]"
                Else                                        'Svc
                    sSQL += " and ID in (Select distinct Accession_ID from Req_Billable where " &
                    "Svc_Date between '" & dtpDateFrom.Text & "' and '" & dtpDateTo.Text & " 23:59:00')"
                    Formula = "{Req_Billable.Svc_Date} in [Date(" & CDate(dtpDateFrom.Text).Year &
                    ", " & CDate(dtpDateFrom.Text).Month & ", " & CDate(dtpDateFrom.Text).Day &
                    ") to Date(" & CDate(dtpDateTo.Text).Year & ", " & CDate(dtpDateTo.Text).Month _
                    & ", " & CDate(dtpDateTo.Text).Day & ")]"
                End If
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                sSQL += " and ID = " & Val(txtAccFrom.Text)
                Formula = "{Req_Billable.Accession_ID} = " & Val(txtAccFrom.Text)
            ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                sSQL += " and ID = " & Val(txtAccTo.Text)
                Formula = "{Req_Billable.Accession_ID} = " & Val(txtAccTo.Text)
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                sSQL += " and ID between " & Val(txtAccFrom.Text) & " and " & Val(txtAccTo.Text)
                Formula = "{Req_Billable.Accession_ID} in [" & txtAccFrom.Text & _
                " To " & txtAccTo.Text & "]"
            ElseIf dgvDiscrete.RowCount > 0 And (dgvDiscrete.Rows(0).Cells(0).Value _
            IsNot Nothing AndAlso dgvDiscrete.Rows(0).Cells(0).Value <> "") Then
                sSQL += " and ID in ("
                Formula = "{Requisitions.ID} in ["
                Dim Accs As String = ""
                For i = 0 To dgvDiscrete.RowCount - 1
                    If dgvDiscrete.Rows(i).Cells(0).Value IsNot Nothing _
                    AndAlso dgvDiscrete.Rows(i).Cells(0).Value <> "" Then
                        Accs += dgvDiscrete.Rows(i).Cells(0).Value & ", "
                    End If
                Next
                If Accs.EndsWith(", ") Then Accs = Accs.Remove(Len(Accs) - 2, 2)
                sSQL += Accs & ")"
                Formula += Accs & "]"
            End If
            '
            Dim Targets As String = ""
            For i = 0 To lstTargets.Items.Count - 1
                If cmbBillType.SelectedIndex <> 3 Then
                    If lstTargets.GetItemChecked(i) = True Then
                        ItemX = lstTargets.Items(i)
                        Targets += ItemX.ItemData.ToString & ", "
                    End If
                Else
                    Targets = "*"
                End If
            Next
            If Targets.EndsWith(", ") Then Targets = Microsoft.VisualBasic.Mid(Targets, 1, Len(Targets) - 2)
            '
            If Targets = "" Then    'kill every thing
                sSQL = ""
            Else
                If cmbBillType.SelectedIndex = 0 Then   'Client
                    sSQL += " and OrderingProvider_ID in (" & Targets & ")"
                    Formula += " and {Requisitions.OrderingProvider_ID} in [" & Targets & "]"
                ElseIf cmbBillType.SelectedIndex = 1 Then   'TP
                    sSQL += " and PrimePayer_ID in (" & Targets & ")"
                    Formula += " and {Requisitions.PrimePayer_ID} in [" & Targets & "]"
                ElseIf cmbBillType.SelectedIndex = 2 Then   'Patient
                    sSQL += " and Patient_ID in (" & Targets & ")"
                    Formula += " and {Requisitions.Patient_ID} in [" & Targets & "]"
                End If
            End If
            '
            If sSQL <> "" Then
                sSQL += " and ID in (Select distinct Accession_ID from Req_Billable where Bill_Status = 'U')"
                If chkClaims.Checked = False Then   'Unprocessed
                    sSQL += " and not ID in (Select Accession_ID from Req_Exported)"
                    'Formula += " and {Req_Billable.HL7Output} = False and {Req_Billable.Bill_Status} = 'U'"
                End If
            Else
                sSQL = "Select Distinct Accession_ID as AccID from Req_Billable where Bill_Status = 'U'"
                If chkClaims.Checked = False Then   'Unprocessed
                    sSQL += " and not Accession_ID in (Select Accession_ID from Req_Exported)"
                End If
            End If
            '
            If connString <> "" Then
                Dim cnexp As New SqlClient.SqlConnection(connString)
                cnexp.Open()
                Dim cmdexp As New SqlClient.SqlCommand(sSQL, cnexp)
                cmdexp.CommandType = CommandType.Text
                Dim drexp As SqlClient.SqlDataReader = cmdexp.ExecuteReader
                If drexp.HasRows Then
                    While drexp.Read
                        If Accessions(UBound(Accessions)) <> "" Then _
                         ReDim Preserve Accessions(UBound(Accessions) + 1)
                        Accessions(UBound(Accessions)) = drexp("AccID").ToString
                    End While
                End If
                cnexp.Close()
                cnexp = Nothing
                'Else
                '    Dim cnexp As New Odbc.OdbcConnection(connstring)
                '    cnexp.Open()
                '    Dim cmdexp As New Odbc.OdbcCommand(sSQL, cnexp)
                '    cmdexp.CommandType = CommandType.Text
                '    Dim drexp As Odbc.OdbcDataReader = cmdexp.ExecuteReader
                '    If drexp.HasRows Then
                '        While drexp.Read
                '            If Accessions(UBound(Accessions)) <> "" Then _
                '             ReDim Preserve Accessions(UBound(Accessions) + 1)
                '            Accessions(UBound(Accessions)) = drexp("AccID").ToString
                '        End While
                '    End If
                '    cnexp.Close()
                '    cnexp = Nothing
            End If
            sSQL = ""
            '
            If Accessions(0) <> "" Then
                If cmbFormat.SelectedIndex = 0 Then 'Report
                    If Formula <> "" Then
                        '=============================
                        'TODO: Crystal Reports Code
                        'Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                        'oRpt.Load(My.Application.Info.DirectoryPath _
                        '& "\Reports\Billables.RPT")
                        'ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database,
                        'My.Settings.UID, My.Settings.PWD)
                        'oRpt.RecordSelectionFormula = Formula
                        'frmRV.CRRV.ReportSource = oRpt
                        'frmRV.MdiParent = ProlisQC
                        'frmRV.Show()
                        '=============================
                        If SystemConfig.AuditTrail = True Then _
                        LogUserEvent(ThisUser.ID, 38, Date.Now, "Accession Count",
                        Accessions.Length, "Accessions = " & Join(Accessions, ", "), "Report")
                    Else
                        MsgBox("No selection was made", MsgBoxStyle.Information, "Prolis")
                    End If
                Else    'HL7 format
                    If LIC.BillExport Then
                        If System.Windows.Forms.DialogResult.OK = FolderBrowserDialog1.ShowDialog Then
                            FPATH = FolderBrowserDialog1.SelectedPath
                            DisableActions()
                            BW.RunWorkerAsync()
                        End If
                    Else
                        MsgBox("Billing Export License validation failed.",
                        MsgBoxStyle.Critical, "Prolis")
                    End If
                End If
            Else
                MsgBox("System found no accession record, to export.",
                MsgBoxStyle.Critical, "Prolis")
            End If
        Else
            MsgBox("Invalid selection.", MsgBoxStyle.Critical, "Prolis")
        End If
    End Sub

    Private Sub DisableActions()
        btnProcess.Enabled = False
        cmbDateType.Enabled = False
        cmbBillType.Enabled = False
        btnSelAll.Enabled = False
        btnDeSel.Enabled = False
        lstTargets.Enabled = False
        'txtDateFrom.Enabled = False
        'txtDateTo.Enabled = False

        dtpDateFrom.Enabled = True
        dtpDateTo.Enabled = True

        txtAccFrom.Enabled = False
        txtAccTo.Enabled = False
        dgvDiscrete.Enabled = False
        cmbFormat.Enabled = False
        chkClaims.Enabled = False
    End Sub

    Private Sub EnableActions()
        btnProcess.Enabled = True
        cmbDateType.Enabled = True
        cmbBillType.Enabled = True
        btnSelAll.Enabled = True
        btnDeSel.Enabled = True
        lstTargets.Enabled = True
        'txtDateFrom.Enabled = True
        'txtDateTo.Enabled = True

        dtpDateFrom.Enabled = True
        dtpDateTo.Enabled = True

        txtAccFrom.Enabled = True
        txtAccTo.Enabled = True
        dgvDiscrete.Enabled = True
        cmbFormat.Enabled = True
        chkClaims.Enabled = True
    End Sub

    Private Sub DoHL7Export(ByVal Accessions() As String, ByVal FPATH As String)
        Dim RetVal As Integer
        Exported = 0
        Failed = 0
        Dim ExpObj As Object
        Dim DLLName As String = GetLabSuperbillDLL()
        Dim DLL As String = My.Application.Info.DirectoryPath &
        "\" & DLLName
        My.Application.DoEvents()
        Dim Plugin As System.Reflection.Assembly =
        System.Reflection.Assembly.LoadFrom(DLL)
        ExpObj = Plugin.CreateInstance(Plugin.GetTypes(5).FullName)
        For n As Integer = 0 To Accessions.Length - 1
            If Not BW.CancellationPending Then
                RetVal = ExpObj.BillablesToHL7File(connString, Accessions(n), FPATH)
                If RetVal = 0 Then
                    UpdateAccessionExported(Accessions(n))
                    Exported += 1
                Else
                    Failed += 1
                End If
                BW.ReportProgress((n + 1) * 100 / Accessions.Length,
                (n + 1).ToString & " of " & Accessions.Length.ToString)
            Else
                Exit For
            End If
        Next
    End Sub

    Private Sub UpdateAccessionExported(ByVal AccID As String)
        If AccID <> "" AndAlso Val(AccID) > 0 Then

        End If
        Dim sSQL As String = "If Exists (Select * from Req_Exported where Accession_ID = " & Val(AccID) &
        ") Update Req_Exported Set ExportDate = '" & Date.Now & "', ExportedBy = " & ThisUser.ID &
        " where Accession_ID = " & Val(AccID) & " Else Insert into Req_Exported (Accession_ID, " &
        "ExportDate, ExportedBy) Values (" & Val(AccID) & ", '" & Date.Now & "', " & ThisUser.ID & ")"
        ExecuteSqlProcedure(sSQL)
    End Sub

    Private Function GetLabSuperbillDLL() As String
        Dim DLLName As String = "ProlisESuperBills.DLL"
        Dim sSQL As String = "Select * from Company where ID = 1"
        If connString <> "" Then
            Dim cnsb As New SqlClient.SqlConnection(connString)
            cnsb.Open()
            Dim cmdsb As New SqlClient.SqlCommand(sSQL, cnsb)
            cmdsb.CommandType = CommandType.Text
            Dim drsb As SqlClient.SqlDataReader = cmdsb.ExecuteReader
            If drsb.HasRows Then
                While drsb.Read
                    If drsb("InterfaceDLL") IsNot DBNull.Value _
                    AndAlso drsb("InterfaceDLL").ToString <> "" _
                    Then DLLName = drsb("InterfaceDLL")
                End While
            End If
            cnsb.Close()
            cnsb = Nothing
            'Else
            '    Dim cnsb As New Odbc.OdbcConnection(connstring)
            '    cnsb.Open()
            '    Dim cmdsb As New Odbc.OdbcCommand(sSQL, cnsb)
            '    cmdsb.CommandType = CommandType.Text
            '    Dim drsb As Odbc.OdbcDataReader = cmdsb.ExecuteReader
            '    If drsb.HasRows Then
            '        While drsb.Read
            '            If drsb("InterfaceDLL") IsNot DBNull.Value _
            '            AndAlso drsb("InterfaceDLL").ToString <> "" _
            '            Then DLLName = drsb("InterfaceDLL")
            '        End While
            '    End If
            '    cnsb.Close()
            '    cnsb = Nothing
        End If
        Return DLLName
    End Function

    Private Function GetAccessionCount(ByVal Accessions As String) As Integer
        Dim Accs() As String = Split(Accessions, ",")
        Return Accs.Length
    End Function

    'Private Sub txtDateFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    txtDateFrom.BackColor = FCOLOR
    'End Sub

    'Private Sub txtDateFrom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    If e.KeyCode = Keys.Enter Then
    '        SendKeys.Send("{TAB}")
    '    End If
    'End Sub

    'Private Sub txtDateFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    txtDateFrom.BackColor = NFCOLOR
    '    If UserEnteredText(txtDateFrom) <> "" Then
    '        If IsDate(txtDateFrom.Text) = False Then
    '            MsgBox("Invalid date")
    '            txtDateFrom.Text = ""
    '        Else
    '            txtAccFrom.Text = ""
    '            txtAccTo.Text = ""
    '            dgvDiscrete.Rows.Clear()
    '            dgvDiscrete.RowCount = 1
    '        End If
    '    End If
    '    ProcessProgress()
    'End Sub

    Private Sub ProcessProgress()
        If (IsDate(dtpDateFrom.Text) = True Or IsDate(dtpDateTo.Text) = True Or
        txtAccFrom.Text <> "" Or txtAccTo.Text <> "" Or HasDiscreteValues()) And
        (cmbBillType.SelectedIndex >= 0 And lstTargets.CheckedItems.Count > 0) _
        And cmbFormat.SelectedIndex >= 0 Then
            btnProcess.Enabled = True
        Else
            btnProcess.Enabled = False
        End If
    End Sub

    'Private Sub txtDateTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    txtDateTo.BackColor = FCOLOR
    'End Sub

    'Private Sub txtDateTo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    If e.KeyCode = Keys.Enter Then
    '        SendKeys.Send("{TAB}")
    '    ElseIf e.KeyCode = Keys.Up Then
    '        SendKeys.Send("+{TAB}")
    '    End If
    'End Sub

    'Private Sub txtDateTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    txtDateTo.BackColor = NFCOLOR
    '    If UserEnteredText(txtDateTo) <> "" Then
    '        If IsDate(txtDateTo.Text) = False Then
    '            MsgBox("Invalid date")
    '            txtDateTo.Text = ""
    '        Else
    '            txtAccFrom.Text = ""
    '            txtAccTo.Text = ""
    '            dgvDiscrete.Rows.Clear()
    '            dgvDiscrete.RowCount = 1
    '        End If
    '    End If
    '    ProcessProgress()
    'End Sub

    Private Sub txtAccFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccFrom.GotFocus
        txtAccFrom.BackColor = FCOLOR
    End Sub

    Private Sub txtAccFrom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAccFrom.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtAccFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccFrom.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAccFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccFrom.LostFocus
        txtAccFrom.BackColor = NFCOLOR
        If txtAccFrom.Text <> "" Then
            'txtDateFrom.Text = ""
            'txtDateTo.Text = ""

            ClearDateTimePicker(dtpDateFrom)
            ClearDateTimePicker(dtpDateTo)
            dgvDiscrete.Rows.Clear()
            dgvDiscrete.RowCount = 1
        End If
        ProcessProgress()
    End Sub

    Private Sub txtAccTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccTo.GotFocus
        txtAccTo.BackColor = FCOLOR
    End Sub

    Private Sub txtAccTo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAccTo.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub txtAccTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccTo.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAccTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccTo.LostFocus
        txtAccTo.BackColor = NFCOLOR
        If txtAccTo.Text <> "" Then
            'txtDateFrom.Text = ""
            'txtDateTo.Text = ""

            ClearDateTimePicker(dtpDateFrom)
            ClearDateTimePicker(dtpDateTo)

            dgvDiscrete.Rows.Clear()
            dgvDiscrete.RowCount = 1
        End If
        ProcessProgress()
    End Sub

    Private Function GetDiscreteAccIDs() As String
        Dim DisAccIDs As String = ""
        Dim i As Integer
        For i = 0 To dgvDiscrete.RowCount - 1
            If Trim(dgvDiscrete.Rows(i).Cells(0).Value) <> "" Then _
            DisAccIDs += Trim(dgvDiscrete.Rows(i).Cells(0).Value) & ", "
        Next
        If Len(DisAccIDs) > 2 Then DisAccIDs = Microsoft.VisualBasic.Mid(DisAccIDs, 1, Len(DisAccIDs) - 2)
        Return DisAccIDs
    End Function

    Private Sub chkClaims_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkClaims.CheckedChanged
        If chkClaims.Checked = False Then   'Unprocessed
            chkClaims.Text = "Unprocessed"
        Else
            chkClaims.Text = "All Claims"
        End If
    End Sub

    Private Sub cmbFormat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFormat.SelectedIndexChanged
        ProcessProgress()
    End Sub

    Private Sub btnSelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelAll.Click
        If lstTargets.Items.Count > 0 Then
            Dim i As Integer
            For i = 0 To lstTargets.Items.Count - 1
                lstTargets.SetItemChecked(i, True)
            Next
        End If
        ProcessProgress()
    End Sub

    Private Sub btnDeSel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeSel.Click
        If lstTargets.Items.Count > 0 Then
            Dim i As Integer
            For i = 0 To lstTargets.Items.Count - 1
                lstTargets.SetItemChecked(i, False)
            Next
        End If
        ProcessProgress()
    End Sub

    Private Sub cmbBillType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBillType.SelectedIndexChanged
        If cmbBillType.SelectedIndex = 0 Then   'Clients
            lstTargets.Items.Clear()
            LoadClients()
            btnSelAll_Click(Nothing, Nothing)
        ElseIf cmbBillType.SelectedIndex = 1 Then   'TP
            lstTargets.Items.Clear()
            LoadThirdParties()
            btnSelAll_Click(Nothing, Nothing)
        ElseIf cmbBillType.SelectedIndex = 2 Then   'Patients
            lstTargets.Items.Clear()
            LoadPatients()
            btnSelAll_Click(Nothing, Nothing)
        ElseIf cmbBillType.SelectedIndex = 3 Then   'All
            lstTargets.Items.Clear()
            lstTargets.Items.Add("All (Clients, Third Parties, Patients)")
            lstTargets.SetItemChecked(0, True)
        Else
            lstTargets.Items.Clear()
        End If
        'ProcessProgress()
    End Sub

    Private Sub LoadClients()
        Dim sSQL As String = ""
        'If chkSpecAll.Checked = True Then   'All
        '    sSQL = "Select * from Providers where not LastName_BSN like 'zz%' and ID in (Select " & _
        '    "distinct OrderingProvider_ID from Requisitions where Received <> 0 and BillingType_ID = 0 and Not ID " & _
        '    "in (Select distinct Accession_ID from Req_Billable where Bill_Status = 'B')) Union " & _
        '    "Select * from Providers where ID in (Select distinct OrderingProvider_ID from " & _
        '    "Requisitions where BillingType_ID = 0 and ID in (Select distinct Accession_ID from " & _
        '    "Req_Billable where Bill_Status = 'U'))"
        'Else                                'specific
        If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
            sSQL = "Select * from Providers where not LastName_BSN like 'zz%' and ID in " &
            "(Select distinct OrderingProvider_ID from Requisitions where Received <> 0 and " &
            "BillingType_ID = 0 and Not ID in (Select distinct Accession_ID from Req_Billable " &
            "where Bill_Status = 'B') and AccessionDate between '" & Format(CDate(dtpDateFrom.Text),
            SystemConfig.DateFormat) & "' and '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) & " 23:59:00')" &
            " Union Select * from Providers where not LastName_BSN like 'zz%' and ID in (Select " &
            "distinct OrderingProvider_ID from Requisitions where BillingType_ID = 0 and ID in (Select " &
            "distinct Accession_ID from Req_Billable where Bill_Status = 'U') and AccessionDate between '" &
            Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) & "' and '" & Format(CDate(dtpDateFrom.Text),
            SystemConfig.DateFormat) & " 23:59:00')"
        ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
            sSQL = "Select * from Providers where not LastName_BSN like 'zz%' and ID in " &
            "(Select distinct OrderingProvider_ID from Requisitions where Received <> 0 and " &
            "BillingType_ID = 0 and Not ID in (Select distinct Accession_ID from Req_Billable " &
            "where Bill_Status = 'B') and AccessionDate between '" & Format(CDate(dtpDateTo.Text),
            SystemConfig.DateFormat) & "' and '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) & " 23:59:00')" &
            " Union Select * from Providers where not LastName_BSN like 'zz%' and ID in (Select " &
            "distinct OrderingProvider_ID from Requisitions where BillingType_ID = 0 and ID in (Select " &
            "distinct Accession_ID from Req_Billable where Bill_Status = 'U') and AccessionDate between '" &
            Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) & "' and '" & Format(CDate(dtpDateTo.Text),
            SystemConfig.DateFormat) & " 23:59:00')"
        ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
            sSQL = "Select * from Providers where not LastName_BSN like 'zz%' and ID in " &
            "(Select distinct OrderingProvider_ID from Requisitions where Received <> 0 and " &
            "BillingType_ID = 0 and Not ID in (Select distinct Accession_ID from Req_Billable " &
            "where Bill_Status = 'B') and AccessionDate between '" & Format(CDate(dtpDateFrom.Text),
            SystemConfig.DateFormat) & "' and '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) & " 23:59:00')" &
            " Union Select * from Providers where not LastName_BSN like 'zz%' and ID in (Select " &
            "distinct OrderingProvider_ID from Requisitions where BillingType_ID = 0 and ID in (Select " &
            "distinct Accession_ID from Req_Billable where Bill_Status = 'U') and AccessionDate between '" &
            Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) & "' and '" & Format(CDate(dtpDateTo.Text),
            SystemConfig.DateFormat) & " 23:59:00')"
        ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
            sSQL = "Select * from Providers where not LastName_BSN like 'zz%' and ID in " &
            "(Select distinct OrderingProvider_ID from Requisitions where Received <> 0 and BillingType_ID " &
            "= 0 and Not ID in (Select distinct Accession_ID from Req_Billable where " &
            "Bill_Status = 'B') and ID = " & Val(txtAccFrom.Text) & ")"
        ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
            sSQL = "Select * from Providers where not LastName_BSN like 'zz%' and ID in " &
            "(Select distinct OrderingProvider_ID from Requisitions where Received <> 0 and BillingType_ID " &
            "= 0 and  and Not ID in (Select distinct Accession_ID from Req_Billable where " &
            "Bill_Status = 'B') ID = " & Val(txtAccTo.Text) & ")"
        ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
            sSQL = "Select * from Providers where not LastName_BSN like 'zz%' and ID in " &
            "(Select distinct OrderingProvider_ID from Requisitions where Received <> 0 and BillingType_ID " &
            "= 0 and Not ID in (Select distinct Accession_ID from Req_Billable where " &
            "Bill_Status = 'B') And ID between " & Val(txtAccFrom.Text) & " And " &
            Val(txtAccTo.Text) & ") Union Select * from Providers where not LastName_BSN " &
            "like 'zz%' and ID in (Select distinct OrderingProvider_ID from Requisitions " &
            "where BillingType_ID = 0 and ID in (Select distinct Accession_ID from " &
            "Req_Billable where Bill_Status = 'U') and ID between " & Val(txtAccFrom.Text) &
            " and " & Val(txtAccTo.Text) & ")"
        ElseIf dgvDiscrete.RowCount > 1 AndAlso GetDiscreteAccIDs() <> "" Then
            sSQL = "Select * from Providers where not LastName_BSN like 'zz%' and ID in " &
            "(Select distinct OrderingProvider_ID from Requisitions where Received <> 0 and BillingType_ID = " &
            "0 and Not ID in (Select distinct Accession_ID from Req_Billable where " &
            "Bill_Status = 'B') And ID in (" & GetDiscreteAccIDs() & "))"
        End If
        'End If
        '
        If sSQL <> "" Then
            Dim Provider As String = ""
            If connString <> "" Then
                Dim cnlc As New SqlClient.SqlConnection(connString)
                cnlc.Open()
                Dim cmdlc As New SqlClient.SqlCommand(sSQL, cnlc)
                cmdlc.CommandType = CommandType.Text
                Dim drlc As SqlClient.SqlDataReader = cmdlc.ExecuteReader
                If drlc.HasRows Then
                    While drlc.Read
                        If drlc("IsIndividual") = False Then
                            Provider = drlc("LastName_BSN")
                        Else
                            If drlc("Degree") IsNot DBNull.Value AndAlso
                            drlc("Degree").ToString <> "" Then
                                If drlc("MiddleName") IsNot DBNull.Value AndAlso drlc("MiddleName") <> "" Then
                                    Provider = Trim(drlc("LastName_BSN")) & ", " & Trim(drlc("FirstName")) & " " &
                                    Trim(drlc("MiddleName")) & " " & Trim(drlc("Degree")) & " [" & drlc("ID").ToString & "]"
                                Else
                                    Provider = Trim(drlc("LastName_BSN")) & ", " & Trim(drlc("FirstName")) _
                                    & " " & Trim(drlc("Degree")) & " [" & drlc("ID").ToString & ")"
                                End If
                            Else
                                If drlc("MiddleName") IsNot DBNull.Value AndAlso drlc("MiddleName") <> "" Then
                                    Provider = Trim(drlc("LastName_BSN")) & ", " & Trim(drlc("FirstName")) &
                                    " " & Trim(drlc("MiddleName")) & " [" & drlc("ID").ToString & "]"
                                Else
                                    Provider = Trim(drlc("LastName_BSN")) & ", " & Trim(drlc("FirstName")) _
                                    & " [" & drlc("ID").ToString & "]"
                                End If
                            End If
                        End If
                        lstTargets.Items.Add(New MyList(Provider, drlc("ID")))
                    End While
                End If
                cnlc.Close()
                cnlc = Nothing
                'Else
                '    Dim cnlc As New Odbc.OdbcConnection(connstring)
                '    cnlc.Open()
                '    Dim cmdlc As New Odbc.OdbcCommand(sSQL, cnlc)
                '    cmdlc.CommandType = CommandType.Text
                '    Dim drlc As Odbc.OdbcDataReader = cmdlc.ExecuteReader
                '    If drlc.HasRows Then
                '        While drlc.Read
                '            If drlc("IsIndividual") = False Then
                '                Provider = drlc("LastName_BSN")
                '            Else
                '                If drlc("Degree") IsNot DBNull.Value AndAlso
                '                drlc("Degree").ToString <> "" Then
                '                    If drlc("MiddleName") IsNot DBNull.Value AndAlso drlc("MiddleName") <> "" Then
                '                        Provider = Trim(drlc("LastName_BSN")) & ", " & Trim(drlc("FirstName")) & " " &
                '                        Trim(drlc("MiddleName")) & " " & Trim(drlc("Degree")) & " [" & drlc("ID").ToString & "]"
                '                    Else
                '                        Provider = Trim(drlc("LastName_BSN")) & ", " & Trim(drlc("FirstName")) _
                '                        & " " & Trim(drlc("Degree")) & " [" & drlc("ID").ToString & ")"
                '                    End If
                '                Else
                '                    If drlc("MiddleName") IsNot DBNull.Value AndAlso drlc("MiddleName") <> "" Then
                '                        Provider = Trim(drlc("LastName_BSN")) & ", " & Trim(drlc("FirstName")) &
                '                        " " & Trim(drlc("MiddleName")) & " [" & drlc("ID").ToString & "]"
                '                    Else
                '                        Provider = Trim(drlc("LastName_BSN")) & ", " & Trim(drlc("FirstName")) _
                '                        & " [" & drlc("ID").ToString & "]"
                '                    End If
                '                End If
                '            End If
                '            lstTargets.Items.Add(New MyList(Provider, drlc("ID")))
                '        End While
                '    End If
                '    cnlc.Close()
                '    cnlc = Nothing
            End If
        End If
    End Sub

    'Private Function GetDiscreteAccIDs() As String
    '    Dim DisAccIDs As String = ""
    '    Dim i As Integer
    '    For i = 0 To dgvDiscrete.RowCount - 1
    '        If Trim(dgvDiscrete.Rows(i).Cells(0).Value) <> "" Then _
    '        DisAccIDs += Trim(dgvDiscrete.Rows(i).Cells(0).Value) & ", "
    '    Next
    '    If Len(DisAccIDs) > 2 Then DisAccIDs = Microsoft.VisualBasic.Mid(DisAccIDs, 1, Len(DisAccIDs) - 2)
    '    Return DisAccIDs
    'End Function

    Private Sub LoadThirdParties()
        Dim sSQL As String = ""
        'If chkSpecAll.Checked = True Then   'All
        '    sSQL = "Select * from Payers where not PayerName like 'zz%' and ID in (Select " & _
        '    "distinct PrimePayer_ID from Requisitions where Received <> 0 and BillingType_ID = 1 " & _
        '    "and Not ID in (Select distinct Accession_ID from Req_Billable where Bill_Status = " & _
        '    "'B')) Union Select * from Payers where not PayerName like 'zz%' and ID in (Select " & _
        '    "distinct PrimePayer_ID from Requisitions where BillingType_ID = 1 and ID in (Select " & _
        '    "distinct Accession_ID from Req_Billable where Bill_Status = 'U'))"
        'Else                                'specific
        If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
            sSQL = "Select * from Payers where not PayerName like 'zz%' and ID in (Select " &
            "distinct PrimePayer_ID from Requisitions where Received <> 0 and BillingType_ID " &
            "= 1 and Not ID in (Select distinct Accession_ID from Req_Billable where Bill_Status " &
            "= 'B') and AccessionDate between '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) &
            "' and '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) & " 23:59:00') Union Select " &
            "* from Payers where not PayerName like 'zz%' and ID in (Select distinct PrimePayer_ID " &
            "from Requisitions where BillingType_ID = 1 and ID in (Select distinct Accession_ID " &
            "from Req_Billable where Bill_Status = 'U') and AccessionDate between '" &
            Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) & "' and '" & Format(CDate(dtpDateFrom.Text),
            SystemConfig.DateFormat) & " 23:59:00')"
        ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
            sSQL = "Select * from Payers where not PayerName like 'zz%' and ID in (Select " &
            "distinct PrimePayer_ID from Requisitions where Received <> 0 and BillingType_ID " &
            "= 1 and Not ID in (Select distinct Accession_ID from Req_Billable where Bill_Status " &
            "= 'B') and AccessionDate between '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) &
            "' and '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) & " 23:59:00') Union Select " &
            "* from Payers where not PayerName like 'zz%' and ID in (Select distinct PrimePayer_ID " &
            "from Requisitions where BillingType_ID = 1 and ID in (Select distinct Accession_ID " &
            "from Req_Billable where Bill_Status = 'U') and AccessionDate between '" &
            Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) & "' and '" & Format(CDate(dtpDateTo.Text),
            SystemConfig.DateFormat) & " 23:59:00')"
        ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
            sSQL = "Select * from Payers where not PayerName like 'zz%' and ID in (Select " &
            "distinct PrimePayer_ID from Requisitions where Received <> 0 and BillingType_ID " &
            "= 1 and Not ID in (Select distinct Accession_ID from Req_Billable where Bill_Status " &
            "= 'B') and AccessionDate between '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) &
            "' and '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) & " 23:59:00') Union Select " &
            "* from Payers where not PayerName like 'zz%' and ID in (Select distinct PrimePayer_ID " &
            "from Requisitions where BillingType_ID = 1 and ID in (Select distinct Accession_ID " &
            "from Req_Billable where Bill_Status = 'U') and AccessionDate between '" &
            Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) & "' and '" & Format(CDate(dtpDateTo.Text),
            SystemConfig.DateFormat) & " 23:59:00')"
        ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
            sSQL = "Select * from Payers where not PayerName like 'zz%' and ID in (Select " &
            "distinct PrimePayer_ID from Requisitions where Received <> 0 and BillingType_ID = " &
            "1 and Not ID in (Select distinct Accession_ID from Req_Billable where Bill_Status " &
            "= 'B') and ID = " & Val(txtAccFrom.Text) & ")"
        ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
            sSQL = "Select * from Payers where not PayerName like 'zz%' and ID in (Select " &
            "distinct PrimePayer_ID from Requisitions where Received <> 0 and BillingType_ID = " &
            "1 and Not ID in (Select distinct Accession_ID from Req_Billable where Bill_Status " &
            "= 'B') and ID = " & Val(txtAccTo.Text) & ")"
        ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
            sSQL = "Select * from Payers where not PayerName like 'zz%' and ID in (Select " &
            "distinct PrimePayer_ID from Requisitions where Received <> 0 and BillingType_ID = " &
            "1 and Not ID in (Select distinct Accession_ID from Req_Billable where Bill_Status " &
            "= 'B') And ID between " & Val(txtAccFrom.Text) & " And " & Val(txtAccTo.Text) &
            ") Union Select * from Payers where not PayerName like 'zz%' and ID in (Select " &
            "distinct PrimePayer_ID from Requisitions where BillingType_ID = 1 and ID in (Select " &
            "distinct Accession_ID from Req_Billable where Bill_Status = 'U') and ID between " _
            & Val(txtAccFrom.Text) & " and " & Val(txtAccTo.Text) & ")"
        ElseIf dgvDiscrete.RowCount > 1 AndAlso GetDiscreteAccIDs() <> "" Then
            sSQL = "Select * from Payers where not PayerName like 'zz%' and ID in (Select " &
            "distinct PrimePayer_ID from Requisitions where Received <> 0 and BillingType_ID = " &
            "1 and Not ID in (Select distinct Accession_ID from Req_Billable where Bill_Status " &
            "= 'B') And ID in (" & GetDiscreteAccIDs() & ")) Union Select * from Payers where not " &
            "PayerName like 'zz%' and ID in (Select distinct PrimePayer_ID from Requisitions " &
            "where BillingType_ID = 1 and ID in (Select distinct Accession_ID from " &
            "Req_Billable where Bill_Status = 'U') And ID in (" & GetDiscreteAccIDs() & "))"
        End If
        'End If
        '
        If sSQL <> "" Then
            Dim Payer As String = ""
            If connString <> "" Then
                Dim cntp As New SqlClient.SqlConnection(connString)
                cntp.Open()
                Dim cmdtp As New SqlClient.SqlCommand(sSQL, cntp)
                cmdtp.CommandType = CommandType.Text
                Dim drtp As SqlClient.SqlDataReader = cmdtp.ExecuteReader
                If drtp.HasRows Then
                    While drtp.Read
                        Payer = Trim(drtp("PayerName")) & " [" & drtp("ID").ToString & "]"
                        lstTargets.Items.Add(New MyList(Payer, drtp("ID")))
                    End While
                End If
                cntp.Close()
                cntp = Nothing
                'Else
                '    Dim cntp As New Odbc.OdbcConnection(connstring)
                '    cntp.Open()
                '    Dim cmdtp As New Odbc.OdbcCommand(sSQL, cntp)
                '    cmdtp.CommandType = CommandType.Text
                '    Dim drtp As Odbc.OdbcDataReader = cmdtp.ExecuteReader
                '    If drtp.HasRows Then
                '        While drtp.Read
                '            Payer = Trim(drtp("PayerName")) & " [" & drtp("ID").ToString & "]"
                '            lstTargets.Items.Add(New MyList(Payer, drtp("ID")))
                '        End While
                '    End If
                '    cntp.Close()
                '    cntp = Nothing
            End If
        End If
    End Sub

    Private Sub LoadPatients()
        Dim sSQL As String = ""
        'If chkSpecAll.Checked = True Then   'All
        '    sSQL = "Select * from Patients where not LastName like 'zz%' and ID in (Select " & _
        '    "distinct Patient_ID from Requisitions where Received <> 0 and BillingType_ID = 2 and Not ID in " & _
        '    "(Select distinct Accession_ID from Req_Billable where Bill_Status = 'B')) Union " & _
        '    "Select * from Patients where not LastName like 'zz%' and ID in (Select distinct " & _
        '    "Patient_ID from Requisitions where BillingType_ID = 2 and ID in (Select distinct " & _
        '    "Accession_ID from Req_Billable where Bill_Status = 'U'))"
        'Else                                'specific
        If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
            sSQL = "Select * from Patients where not LastName like 'zz%' and ID in (Select distinct " &
            "Patient_ID from Requisitions where Received <> 0 and BillingType_ID = 2 and Not ID in " &
            "(Select distinct Accession_ID from Req_Billable where Bill_Status = 'B') and " &
            "AccessionDate between '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) & "' and '" &
            Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) & " 23:59:00') Union Select * from Patients " &
            "where not LastName like 'zz%' and ID in (Select distinct Patient_ID from Requisitions " &
            "where BillingType_ID = 2 and ID in (Select distinct Accession_ID from Req_Billable where " &
            "Bill_Status = 'U') and AccessionDate between '" & Format(CDate(dtpDateFrom.Text),
            SystemConfig.DateFormat) & "' and '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) & " 23:59:00')"
        ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
            sSQL = "Select * from Patients where not LastName like 'zz%' and ID in (Select distinct " &
            "Patient_ID from Requisitions where Received <> 0 and BillingType_ID = 2 and Not ID in " &
            "(Select distinct Accession_ID from Req_Billable where Bill_Status = 'B') and " &
            "AccessionDate between '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) & "' and '" &
            Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) & " 23:59:00') Union Select * from Patients " &
            "where not LastName like 'zz%' and ID in (Select distinct Patient_ID from Requisitions " &
            "where BillingType_ID = 2 and ID in (Select distinct Accession_ID from Req_Billable where " &
            "Bill_Status = 'U') and AccessionDate between '" & Format(CDate(dtpDateTo.Text),
            SystemConfig.DateFormat) & "' and '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) & " 23:59:00')"
        ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
            sSQL = "Select * from Patients where not LastName like 'zz%' and ID in (Select distinct " &
            "Patient_ID from Requisitions where Received <> 0 and BillingType_ID = 2 and Not ID in " &
            "(Select distinct Accession_ID from Req_Billable where Bill_Status = 'B') and " &
            "AccessionDate between '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) & "' and '" &
            Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) & " 23:59:00') Union Select * from Patients " &
            "where not LastName like 'zz%' and ID in (Select distinct Patient_ID from Requisitions " &
            "where BillingType_ID = 2 and ID in (Select distinct Accession_ID from Req_Billable where " &
            "Bill_Status = 'U') and AccessionDate between '" & Format(CDate(dtpDateFrom.Text),
            SystemConfig.DateFormat) & "' and '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) & " 23:59:00')"
        ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
            sSQL = "Select * from Patients where not LastName like 'zz%' and ID in (Select " &
            "distinct Patient_ID from Requisitions where Received <> 0 and BillingType_ID = 2 and Not ID in " &
            "(Select distinct Accession_ID from Req_Billable where Bill_Status = 'B') and " &
            "ID = " & Val(txtAccFrom.Text) & ")"
        ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
            sSQL = "Select * from Patients where not LastName like 'zz%' and ID in (Select " &
            "distinct Patient_ID from Requisitions where Received <> 0 and BillingType_ID = 2 and Not ID in " &
            "(Select distinct Accession_ID from Req_Billable where Bill_Status = 'B') and " &
            "ID = " & Val(txtAccTo.Text) & ")"
        ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
            sSQL = "Select * from Patients where not LastName like 'zz%' and ID in (Select " &
            "distinct Patient_ID from Requisitions where Received <> 0 and BillingType_ID = 2 and Not ID in " &
            "(Select distinct Accession_ID from Req_Billable where Bill_Status = 'B') and " &
            "ID between " & Val(txtAccFrom.Text) & " and " & Val(txtAccTo.Text) & ") Union " &
            "Select * from Patients where not LastName like 'zz%' and ID in (Select " &
            "distinct Patient_ID from Requisitions where BillingType_ID = 2 and ID in " &
            "(Select distinct Accession_ID from Req_Billable where Bill_Status = 'U') " &
            "and ID between " & Val(txtAccFrom.Text) & " and " & Val(txtAccTo.Text) & ")"
        ElseIf dgvDiscrete.RowCount > 1 AndAlso GetDiscreteAccIDs() <> "" Then
            sSQL = "Select * from Patients where not LastName like 'zz%' and ID in (Select " &
            "distinct Patient_ID from Requisitions where Received <> 0 and BillingType_ID = 2 and Not ID in " &
            "(Select distinct Accession_ID from Req_Billable where Bill_Status = 'B') and " &
            "ID in (" & GetDiscreteAccIDs() & "))"
        End If
        'End If
        '
        If sSQL <> "" Then
            Dim Patient As String = ""
            If connString <> "" Then
                Dim cnpat As New SqlClient.SqlConnection(connString)
                cnpat.Open()
                Dim cmdpat As New SqlClient.SqlCommand(sSQL, cnpat)
                cmdpat.CommandType = CommandType.Text
                Dim drpat As SqlClient.SqlDataReader = cmdpat.ExecuteReader
                If drpat.HasRows Then
                    While drpat.Read
                        If drpat("MiddleName") IsNot DBNull.Value AndAlso drpat("MiddleName") <> "" Then
                            Patient = Trim(drpat("LastName")) & ", " & Trim(drpat("FirstName")) _
                            & " " & Microsoft.VisualBasic.Left(drpat("MiddleName"), 1) & " - " & _
                            Format(drpat("DOB"), SystemConfig.DateFormat) & " - " & drpat("Sex")
                        Else
                            Patient = Trim(drpat("LastName")) & ", " & Trim(drpat("FirstName")) _
                            & " - " & Format(drpat("DOB"), SystemConfig.DateFormat) & " - " & drpat("Sex")
                        End If
                        lstTargets.Items.Add(New MyList(Patient, drpat("ID")))
                    End While
                End If
                cnpat.Close()
                cnpat = Nothing
                'Else
                '    Dim cnpat As New Odbc.OdbcConnection(connstring)
                '    cnpat.Open()
                '    Dim cmdpat As New Odbc.OdbcCommand(sSQL, cnpat)
                '    cmdpat.CommandType = CommandType.Text
                '    Dim drpat As Odbc.OdbcDataReader = cmdpat.ExecuteReader
                '    If drpat.HasRows Then
                '        While drpat.Read
                '            If drpat("MiddleName") IsNot DBNull.Value AndAlso drpat("MiddleName") <> "" Then
                '                Patient = Trim(drpat("LastName")) & ", " & Trim(drpat("FirstName")) _
                '                & " " & Microsoft.VisualBasic.Left(drpat("MiddleName"), 1) & " - " & _
                '                Format(drpat("DOB"), SystemConfig.DateFormat) & " - " & drpat("Sex")
                '            Else
                '                Patient = Trim(drpat("LastName")) & ", " & Trim(drpat("FirstName")) _
                '                & " - " & Format(drpat("DOB"), SystemConfig.DateFormat) & " - " & drpat("Sex")
                '            End If
                '            lstTargets.Items.Add(New MyList(Patient, drpat("ID")))
                '        End While
                '    End If
                '    cnpat.Close()
                '    cnpat = Nothing
            End If
        End If
    End Sub

    Private Sub lstTargets_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles lstTargets.ItemCheck
        ProcessProgress()
    End Sub

    Private Sub dgvDiscrete_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvDiscrete.CellMouseUp
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
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
                        ProcessProgress()
                    End If
                Next
            End If
        End If
    End Sub


    Private Sub BW_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BW.DoWork
        DoHL7Export(Accessions, FPATH)
    End Sub

    Private Sub BW_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BW.ProgressChanged
        PB.Value = e.ProgressPercentage
        lblStatus.Text = e.UserState
    End Sub

    Private Sub BW_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BW.RunWorkerCompleted
        MsgBox("Billable Accession records exported " & _
        "successfully: " & Exported & vbCrLf & _
        "Accession records failed: " & Failed & vbCrLf & _
        "Export file directory: '" & FPATH & "'", _
        MsgBoxStyle.Information, "Prolis")
        'txtDateFrom.Text = ""
        'txtDateTo.Text = ""
        ClearDateTimePicker(dtpDateFrom)
        ClearDateTimePicker(dtpDateTo)
        txtAccFrom.Text = ""
        txtAccTo.Text = ""
        lstTargets.Items.Clear()
        lstTargets.Items.Add("All (Clients, Third Parties, Patients)")
        dgvDiscrete.Rows.Clear()
        dgvDiscrete.RowCount = 1
        cmbFormat.SelectedIndex = -1
        ProcessProgress()
        EnableActions()
        If BW.CancellationPending Then Me.Close()
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
        ProcessProgress()
    End Sub
End Class
