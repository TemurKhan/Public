Imports System.IO
Imports System.Security.AccessControl
 
Imports System.Data.SqlClient
Imports DataTable = System.Data.DataTable

Public Class frmRptDash
    Private origWidth As Integer
    Private origHeight As Integer
    Public IsBusy = False
    Public Shared ReportsFolder As String
    Private Sub frmRptDash_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MaximumSize = MaxSize
        cmbCriteria.SelectedIndex = -1
        lblFrom.Text += " (" & SystemConfig.DateFormat & ")"
        lblTo.Text += " (" & SystemConfig.DateFormat & ")"
        dtpFrom.Format = DateTimePickerFormat.Custom
        dtpFrom.CustomFormat = SystemConfig.DateFormat
        dtpFrom.Value = Date.Today
        dtpTo.Format = DateTimePickerFormat.Custom
        dtpTo.CustomFormat = SystemConfig.DateFormat
        dtpTo.Value = CDate(Format(Date.Today, "MM/dd/yyyy 23:59:00"))
        Dim stopWatch As New Stopwatch()
        stopWatch.Start()
        '
        DisplayReports()
        '
        stopWatch.Stop()
        lblStatus.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
        stopWatch.Elapsed.Hours, stopWatch.Elapsed.Minutes,
        stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds / 10)
        My.Application.DoEvents()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
        cmbDestination.SelectedIndex = 1
    End Sub

    Private Sub DisplayReports()
        'Try
        ClearForm()
        Dim Criteria As String = GetCriteria()
        Dim FLTR As String = ""
        '
        If Trim(txtTerm.Text) <> "" Then
            If cmbCriteria.SelectedIndex = 0 Then   'AccID
                If txtTerm.Text.Contains("-") Then
                    Dim acc = txtTerm.Text.Split("-")(0)
                    txtTerm.Text = acc
                End If
                If InStr(txtTerm.Text, ",") = 0 Then
                    FLTR += " and a.ID = " & Trim(txtTerm.Text)
                Else
                    FLTR += " and a.ID in (" & txtTerm.Text & ")"
                End If
            ElseIf cmbCriteria.SelectedIndex = 1 Then   'ProvID
                FLTR += " and a.OrderingProvider_ID = " & Trim(txtTerm.Text)
            ElseIf cmbCriteria.SelectedIndex = 2 Then   'provname
                Dim LName As String = ""
                Dim FName As String = ""
                If InStr(txtTerm.Text, ",") > 0 Then
                    Dim Names() As String = Split(txtTerm.Text, ",")
                    LName = Trim(Names(0))
                    FName = Trim(Names(1))
                Else
                    LName = Trim(txtTerm.Text)
                    FName = ""
                End If
                If FName = "" Then
                    FLTR += " and a.OrderingProvider_ID in (Select ID from Providers where " &
                    "LastName_BSN like '%" & LName & "%')"
                Else
                    FLTR += " and a.OrderingProvider_ID in (Select ID from Providers where " &
                    "LastName_BSN like '%" & LName & "%' and FirstName like '%" & FName & "%')"
                End If
            ElseIf cmbCriteria.SelectedIndex = 3 Then   'PatID
                FLTR += " and a.Patient_ID = " & Trim(txtTerm.Text)
            Else    'PatName
                Dim LName As String = ""
                Dim FName As String = ""
                If InStr(txtTerm.Text, ",") > 0 Then
                    Dim Names() As String = Split(txtTerm.Text, ",")
                    LName = Trim(Names(0))
                    FName = Trim(Names(1))
                Else
                    LName = Trim(txtTerm.Text)
                    FName = ""
                End If
                If FName = "" Then
                    FLTR += " and a.Patient_ID in (Select ID from Patients where LastName " &
                    "like '%" & LName & "%')"
                Else
                    FLTR += " and a.Patient_ID in (Select ID from Patients where LastName " &
                    "like '%" & LName & "%' and FirstName like '%" & FName & "%')"
                End If
            End If
        End If
        '
        Dim sSQL As String = "Select a.Accession_ID as AccID, convert(nvarchar, a.Event_Date, 101) " &
        "as Dated, a.Object_Status as [Status], b.Event_Name, IsNull(c.Status, '') as FxStatus from " &
        "Events b inner join (Event_Capture a Left outer join Fax_Log c on  c.Accession_ID = a.Accession_ID) " &
        "on a.Event_ID = b.ID where b.ID in (10, 11, 110, 12, 120, 13, 130, 14, 140, 15, 150, 16, " &
        "160, 17, 18, 19) and " & Criteria
        'Dim sSQL As String = "Select * from Event_Capture where Event_ID in (10, 11, " & _
        '"110, 12, 120, 13, 130, 14, 140, 15, 150, 16, 160, 17, 18, 19) and " & Criteria
        If chkPUnP.Checked = False Then 'Processed
            'btnSellAll.Enabled = False
            '  btnDeselAll.Enabled = False
            ' btnRedo.Enabled = False
            Dim media As String = ""
            Dim Status As String = ""
            '
            Dim cnp As New SqlConnection(connString)
            cnp.Open()
            Dim cmdp As New SqlCommand(sSQL, cnp)
            cmdp.CommandType = CommandType.Text
            Dim drp As SqlDataReader = cmdp.ExecuteReader
            If drp.HasRows Then
                While drp.Read
                    If drp("Event_Name").ToString.Contains("Fax") Then
                        media = drp("Event_Name") & IIf(drp("FxStatus") = "", "", "-" & drp("FxStatus"))
                    Else
                        media = drp("Event_Name")
                    End If
                    dgvAccessions.Rows.Add(drp("AccID"), drp("Dated"), drp("Status"), media, False)
                    If InStr(dgvAccessions.Rows(dgvAccessions.RowCount - 1).Cells(3).Value, "Fax") > 0 And
                    InStr(dgvAccessions.Rows(dgvAccessions.RowCount - 1).Cells(3).Value, "Fail") > 0 Then
                        dgvAccessions.Rows(dgvAccessions.RowCount - 1).Cells(4).ReadOnly = False
                    Else
                        ' dgvAccessions.Rows(dgvAccessions.RowCount - 1).Cells(4).ReadOnly = True
                    End If
                    media = ""
                End While
            End If
            cnp.Close()
            cnp = Nothing
        Else    'Unprocessed
            btnSellAll.Enabled = True
            btnDeselAll.Enabled = True
            btnRedo.Enabled = True
            Dim ACT As Boolean = False
            Dim Output() As String
            sSQL = "Select a.ID as AccID, a.AccessionDate as AccDate from Requisitions a inner join Req_RPT b " &
            "on a.ID = b.Base_ID where b.RPT_Complete <> 0 and a.Reported_Final between '" & Format(dtpFrom.Value,
            "MM/dd/yyyy 00:00:00") & "' and '" & Format(dtpTo.Value, "MM/dd/yyyy 23:59:00") & "' and b.EntrySource " &
            "= 'Accession'and not a.ID in (Select Accession_ID from Event_Capture where Event_ID in (10, 11, 12, 13, " &
            "14, 15, 16, 17, 18, 19))" & FLTR & "Union Select a.ID as AccID, a.AccessionDate as AccDate from Requisitions " &
            "a inner join Req_RPT b on a.ID = b.Base_ID where b.RPT_Complete = 0 and a.ReportedOn between '" & Format(dtpFrom.Value,
            "MM/dd/yyyy 00:00:00") & "' and '" & Format(dtpTo.Value, "MM/dd/yyyy 23:59:00") & "' and b.EntrySource = 'Accession' " &
            "and not a.ID in (Select Accession_ID from Event_Capture where Event_ID in (10, 11, 12, 13, 14, 15, 16, 17, 18, 19))" &
            FLTR & " Union Select a.ID as AccID, a.AccessionDate as AccDate from Requisitions a inner join Req_RPT b on a.ID = " &
            "b.Base_ID where b.EntryDate between '" & Format(dtpFrom.Value, "MM/dd/yyyy 00:00:00") & "' and '" & Format(dtpTo.Value,
            "MM/dd/yyyy 23:59:00") & "' and b.EntrySource <> 'Accession' and not a.ID in (Select Accession_ID from Event_Capture " &
            "where Event_ID in (10, 11, 12, 13, 14, 15, 16, 17, 18, 19))" & FLTR
            '
            Dim cnu As New SqlConnection(connString)
            cnu.Open()
            Dim cmdu As New SqlCommand(sSQL, cnu)
            cmdu.CommandType = CommandType.Text
            Dim dru As SqlDataReader = cmdu.ExecuteReader
            If dru.HasRows Then
                While dru.Read
                    Output = GetDesiredSetting(dru("AccID"))
                    If InStr(Output(0), "Fx") > 0 Or InStr(Output(0),
                    "Em") > 0 Or InStr(Output(0), "If") > 0 Then
                        ACT = True
                    Else
                        ACT = False
                    End If
                    If Output(0) IsNot Nothing AndAlso Output(0) <> "" Then
                        dgvAccessions.Rows.Add(dru("AccID"), Format(dru("AccDate"), SystemConfig.DateFormat),
                        Output(1), Output(0), ACT)
                        If ACT = True Then
                            dgvAccessions.Rows(dgvAccessions.RowCount - 1).Cells(4).ReadOnly = False
                        Else
                            ' dgvAccessions.Rows(dgvAccessions.RowCount - 1).Cells(4).ReadOnly = True
                        End If
                        Output = Nothing
                    End If
                End While
            End If
            cnu.Close()
            cnu = Nothing
        End If
        '
        If chkPUnP.Checked Then     'Failed
            txtView.Text = "0"
            txtLabPrinted.Text = GetToPrintCount()
            txtEmailed.Text = GetToEmailCount()
            txtProlisOnPrinted.Text = GetToProlisOnCount()
            txtFailFax.Text = GetToFaxCount()
            txtSuccessFax.Text = 0
            txtHL7.Text = GetToHL7Count()
            txtPDF.Text = GetToPDFCount()
            txtInitial.Text = "0"
            txtPartial.Text = GetToPartialCount()
            txtFinal.Text = GetToFinalCount()
            txtTotal.Text = dgvAccessions.RowCount
        Else    'Processed
            txtView.Text = GetViewCount()
            txtLabPrinted.Text = GetPrintCount()
            txtEmailed.Text = GetEmailCount()
            txtProlisOnPrinted.Text = GetProlisOnCount()
            txtFailFax.Text = GetFailFaxCount()
            txtSuccessFax.Text = GetSuccessFaxedCount()
            txtHL7.Text = GetHL7Count()
            txtPDF.Text = GetPDFCount()
            txtInitial.Text = GetInitialCount()
            txtPartial.Text = GetPartialCount()
            txtFinal.Text = GetFinalCount()
            txtTotal.Text = dgvAccessions.RowCount
        End If
        'TODO: Chart Control
        '=================================
        'Chart1.Series(0).Points.Clear()
        ''Chart1.ChartAreas(0).AxisX.IsLabelAutoFit = False
        ''Chart1.ChartAreas(0).AxisX.LabelStyle.Angle = -90
        'Chart1.Series(0).Points.AddXY("View", Val(txtView.Text))
        'Chart1.Series(0).Points.AddXY("Print", Val(txtLabPrinted.Text))
        'Chart1.Series(0).Points.AddXY("Email", Val(txtEmailed.Text))
        'Chart1.Series(0).Points.AddXY("EMR", Val(txtHL7.Text) + Val(txtPDF.Text))
        'Chart1.Series(0).Points.AddXY("XPrint", Val(txtProlisOnPrinted.Text))
        'Chart1.Series(0).Points.AddXY("Fax", Val(txtFailFax.Text) + Val(txtSuccessFax.Text))
        'Chart1.Series(0).Points.AddXY("Total", Val(txtTotal.Text))
        'Chart1.Refresh()
        '=================================
        'Catch Ex As Exception
        '    MsgBox(Ex.Message, MsgBoxStyle.Critical, "Prolis")
        'End Try
    End Sub

    Private Function GetDesiredSetting(ByVal AccID As Long) As String()
        Dim Output() As String = {"", ""}   '0=media, 1=status
        Dim cnrd As New SqlConnection(connString)
        cnrd.Open()
        Dim cmdrd As New SqlCommand("Select " &
        "* from Req_RPT where Base_ID = " & AccID, cnrd)
        cmdrd.CommandType = CommandType.Text
        Dim drrd As SqlDataReader = cmdrd.ExecuteReader
        If drrd.HasRows Then
            While drrd.Read
                If (drrd("Fax") IsNot DBNull.Value AndAlso
                drrd("Fax") <> "") AndAlso drrd("RPT_Fax") _
                = True Then Output(0) += "Fx,"
                If (drrd("Email") IsNot DBNull.Value AndAlso
                drrd("Email") <> "") AndAlso drrd("RPT_Email") _
                = True Then Output(0) += "Em,"
                If drrd("RPT_Print") <> 0 Then Output(0) += "Pt,"
                If drrd("RPT_Interface") <> 0 Then Output(0) += "If,"
                If drrd("RPT_ProlisOn") <> 0 Then Output(0) += "Po,"
                Output(1) = IIf(drrd("RPT_Complete") <> 0, "FINAL", "PARTIAL")
            End While
        End If
        cnrd.Close()
        cnrd = Nothing
        If Output(0).EndsWith(",") Then Output(0) = Output(0).Substring(0, Len(Output(0)) - 1)
        Return Output
    End Function

    Private Sub ClearForm()
        dgvAccessions.Rows.Clear()
        txtPatient.Text = "" : txtProvider.Text = "" : txtProcessDetail.Text = ""
        txtView.Text = "" : txtLabPrinted.Text = "" : txtEmailed.Text = ""
        txtProlisOnPrinted.Text = "" : txtFailFax.Text = "" : txtSuccessFax.Text = ""
        txtHL7.Text = "" : txtPDF.Text = ""
        txtInitial.Text = "" : txtPartial.Text = "" : txtTotal.Text = ""
        'btnRedo.Enabled = False
    End Sub

    Private Function UpdateSource(ByVal EventID As Integer, ByVal AccID As Long) As String
        Dim Source As String = ""
        Dim sSQL As String = "Select * from Events where ID = " & EventID
        '
        Dim cns As New SqlConnection(connString)
        cns.Open()
        Dim cmds As New SqlCommand(sSQL, cns)
        cmds.CommandType = CommandType.Text
        Dim drs As SqlDataReader = cmds.ExecuteReader
        If drs.HasRows Then
            While drs.Read
                Source = drs("Event_Name")
            End While
        End If
        Source = Replace(Source, "Report", "")
        Source = Replace(Source, "Screen", "")
        Source = Trim(Source)
        cns.Close()
        cns = Nothing
        '
        If EventID = 12 Or EventID = 17 Then    'Fax
            sSQL = "Select Status from Fax_Log where Accession_ID = " & AccID & " order by ID DESC"
            Dim cnf As New SqlConnection(connString)
            cnf.Open()
            Dim cmdf As New SqlCommand(sSQL, cnf)
            cmdf.CommandType = CommandType.Text
            Dim drf As SqlDataReader = cmdf.ExecuteReader
            If drf.HasRows Then
                While drf.Read
                    If Source <> "" Then
                        If drf("Status") IsNot DBNull.Value _
                        AndAlso Trim(drf("Status")) <> "" Then _
                        Source = Source & "-" & Trim(drf("Status"))
                    End If
                End While
            End If
            cnf.Close()
            cnf = Nothing
        End If
        Return Source
    End Function

    Private Function GetTotalCount(ByVal Criteria As String) As Integer
        Dim AccCount As Integer = 0
        'Criteria = Replace(Criteria, "Event_Date", "AccessionDate")
        Dim cntc As New SqlConnection(connString)
        cntc.Open()
        Dim cmdtc As New SqlCommand("Select distinct " &
        "Count(Accession_ID) as AC from Event_Capture where Event_ID in " &
        "(10, 11, 12, 13, 14, 15, 16, 17, 18, 19) and " & Criteria, cntc)
        cmdtc.CommandType = CommandType.Text
        Dim drtc As SqlDataReader = cmdtc.ExecuteReader
        If drtc.HasRows Then
            While drtc.Read
                AccCount = drtc("AC")
            End While
        End If
        cntc.Close()
        cntc = Nothing
        Return AccCount
    End Function

    Private Function GetCompleteCount(ByVal Criteria As String) As Integer
        Dim AccCount As Integer = 0
        'Criteria = Replace(Criteria, "Event_Date", "AccessionDate")
        Dim cntc As New SqlConnection(connString)
        cntc.Open()
        Dim cmdtc As New SqlCommand("Select Count(Accession_ID) " &
        "as AC from Event_Capture where Object_Status in (" &
        "'Complete', 'FINAL') and " & Criteria, cntc)
        cmdtc.CommandType = CommandType.Text
        Dim drtc As SqlDataReader = cmdtc.ExecuteReader
        If drtc.HasRows Then
            While drtc.Read
                AccCount = drtc("AC")
            End While
        End If
        cntc.Close()
        cntc = Nothing
        Return AccCount
    End Function

    Private Function GetIncompleteCount(ByVal Criteria As String) As Integer
        Dim AccCount As Integer = 0
        'Criteria = Replace(Criteria, "Event_Date", "AccessionDate")
        Dim cntc As New SqlConnection(connString)
        cntc.Open()
        Dim cmdtc As New SqlCommand("Select Count(Accession_ID) " &
        "as AC from Event_Capture where Object_Status = 'Incomplete' " &
        "and " & Criteria, cntc)
        cmdtc.CommandType = CommandType.Text
        Dim drtc As SqlDataReader = cmdtc.ExecuteReader
        If drtc.HasRows Then
            While drtc.Read
                AccCount = drtc("AC")
            End While
        End If
        cntc.Close()
        cntc = Nothing
        Return AccCount
    End Function

    Private Function GetToFinalCount() As Integer
        Dim AccCount As Integer = 0
        For i As Integer = 0 To dgvAccessions.RowCount - 1
            If InStr(dgvAccessions.Rows(i).Cells(2).Value, "FINAL") > 0 _
            Or InStr(dgvAccessions.Rows(i).Cells(2).Value, "complete") > 0 Then
                AccCount += 1
            End If
        Next
        Return AccCount
    End Function

    Private Function GetFinalCount() As Integer
        Dim AccCount As Integer = 0
        For i As Integer = 0 To dgvAccessions.RowCount - 1
            If InStr(dgvAccessions.Rows(i).Cells(2).Value, "FINAL") > 0 _
            Or InStr(dgvAccessions.Rows(i).Cells(2).Value, "complete") > 0 Then
                AccCount += 1
            End If
        Next
        Return AccCount
    End Function

    Private Function GetToPartialCount() As Integer
        Dim AccCount As Integer = 0
        For i As Integer = 0 To dgvAccessions.RowCount - 1
            If InStr(dgvAccessions.Rows(i).Cells(2).Value, "Partial") > 0 _
            Or InStr(dgvAccessions.Rows(i).Cells(2).Value, "Incomp") > 0 Then
                AccCount += 1
            End If
        Next
        Return AccCount
    End Function

    Private Function GetToInitialCount() As Integer
        Dim InitialC As Integer = 0
        Try
            Dim sSQL As String = "Select Count(ID) as InitialC from Requisitions where Reported_Initial " &
            "between '" & Format(dtpFrom.Value, SystemConfig.DateFormat) & "' and '" & Format(dtpTo.Value,
            SystemConfig.DateFormat) & " 23:59:00'" & " and Reported_Final is NULL and ReportedOn is NULL and ID in " &
            "(Select Base_ID from Req_RPT where EntrySource <> 'Accession' and EntryDate between '" &
            Format(dtpFrom.Value, SystemConfig.DateFormat) & "' and '" & Format(dtpTo.Value, SystemConfig.DateFormat) &
            " 23:59:00') and Not ID in (Select Accession_ID from Event_Capture where Event_ID in " &
            "(10,11,12,13,14,15,16,17,18,19))"
            If Trim(txtTerm.Text) <> "" Then
                If cmbCriteria.SelectedIndex = 0 Then   'AccID
                    If InStr(txtTerm.Text, ",") = 0 Then
                        sSQL += " and ID = " & Trim(txtTerm.Text)
                    Else
                        sSQL += " and ID in (" & txtTerm.Text & ")"
                    End If
                ElseIf cmbCriteria.SelectedIndex = 1 Then   'ProvID
                    sSQL += " and OrderingProvider_ID = " & Trim(txtTerm.Text)
                ElseIf cmbCriteria.SelectedIndex = 2 Then   'provname
                    Dim LName As String = ""
                    Dim FName As String = ""
                    If InStr(txtTerm.Text, ",") > 0 Then
                        Dim Names() As String = Split(txtTerm.Text, ",")
                        LName = Trim(Names(0))
                        FName = Trim(Names(1))
                    Else
                        LName = Trim(txtTerm.Text)
                        FName = ""
                    End If
                    If FName = "" Then
                        sSQL += " and OrderingProvider_ID in (Select ID from Providers where " &
                        "LastName_BSN like '%" & LName & "%')"
                    Else
                        sSQL += " and OrderingProvider_ID in (Select ID from Providers where " &
                        "LastName_BSN like '%" & LName & "%' and FirstName like '%" & FName & "%')"
                    End If
                ElseIf cmbCriteria.SelectedIndex = 3 Then   'PatID
                    sSQL += " and Patient_ID = " & Trim(txtTerm.Text)
                Else    'PatName
                    Dim LName As String = ""
                    Dim FName As String = ""
                    If InStr(txtTerm.Text, ",") > 0 Then
                        Dim Names() As String = Split(txtTerm.Text, ",")
                        LName = Trim(Names(0))
                        FName = Trim(Names(1))
                    Else
                        LName = Trim(txtTerm.Text)
                        FName = ""
                    End If
                    If FName = "" Then
                        sSQL += " and Patient_ID in (Select ID from Patients where LastName " &
                        "like '%" & LName & "%')"
                    Else
                        sSQL += " and Patient_ID in (Select ID from Patients where LastName " &
                        "like '%" & LName & "%' and FirstName like '%" & FName & "%')"
                    End If
                End If
            End If
            '
            Dim cntc As New SqlConnection(connString)
            cntc.Open()
            Dim cmdtc As New SqlCommand(sSQL, cntc)
            cmdtc.CommandType = CommandType.Text
            Dim drtc As SqlDataReader = cmdtc.ExecuteReader
            If drtc.HasRows Then
                While drtc.Read
                    InitialC = drtc("InitialC")
                End While
            End If
            cntc.Close()
            cntc = Nothing
        Catch Ex As Exception
            MsgBox(Ex.Message, MsgBoxStyle.Critical, "Prolis")
        End Try
        Return InitialC
    End Function

    Private Function GetInitialCount() As Integer
        Dim AccCount As Integer = 0
        For i As Integer = 0 To dgvAccessions.RowCount - 1
            If InStr(dgvAccessions.Rows(i).Cells(2).Value, "Initial") > 0 Then
                AccCount += 1
            End If
        Next
        Return AccCount
    End Function

    Private Function GetToPDFCount() As Integer
        Dim AccCount As Integer = 0
        For i As Integer = 0 To dgvAccessions.RowCount - 1
            If InStr(dgvAccessions.Rows(i).Cells(3).Value, "If") > 0 Then
                AccCount += 1
            End If
        Next
        Return AccCount
    End Function

    Private Function GetPDFCount() As Integer
        Dim AccCount As Integer = 0
        For i As Integer = 0 To dgvAccessions.RowCount - 1
            If InStr(dgvAccessions.Rows(i).Cells(3).Value, "PDF") > 0 Then
                AccCount += 1
            End If
        Next
        Return AccCount
    End Function

    Private Function GetToHL7Count() As Integer
        Dim AccCount As Integer = 0
        For i As Integer = 0 To dgvAccessions.RowCount - 1
            If InStr(dgvAccessions.Rows(i).Cells(3).Value, "If") > 0 Then
                AccCount += 1
            End If
        Next
        Return AccCount
    End Function

    Private Function GetHL7Count() As Integer
        Dim AccCount As Integer = 0
        For i As Integer = 0 To dgvAccessions.RowCount - 1
            If InStr(dgvAccessions.Rows(i).Cells(3).Value, "HL7") > 0 Then
                AccCount += 1
            End If
        Next
        Return AccCount
    End Function

    Private Function GetSuccessFaxedCount() As Integer
        Dim AccCount As Integer = 0
        For i As Integer = 0 To dgvAccessions.RowCount - 1
            If InStr(dgvAccessions.Rows(i).Cells(3).Value, "Fax") > 0 AndAlso
            InStr(dgvAccessions.Rows(i).Cells(3).Value, "Success") > 0 Then
                AccCount += 1
            End If
        Next
        Return AccCount
    End Function

    Private Function GetToFaxCount() As Integer
        Dim AccCount As Integer = 0
        For i As Integer = 0 To dgvAccessions.RowCount - 1
            If InStr(dgvAccessions.Rows(i).Cells(3).Value, "Fx") > 0 Then
                AccCount += 1
            End If
        Next
        Return AccCount
    End Function

    Private Function GetFailFaxCount() As Integer
        Dim AccCount As Integer = 0
        For i As Integer = 0 To dgvAccessions.RowCount - 1
            If InStr(dgvAccessions.Rows(i).Cells(3).Value, "Fax") > 0 AndAlso
            InStr(dgvAccessions.Rows(i).Cells(3).Value, "Fail") > 0 Then
                AccCount += 1
            End If
        Next
        Return AccCount
    End Function

    Private Function GetToProlisOnCount() As Integer
        Dim AccCount As Integer = 0
        For i As Integer = 0 To dgvAccessions.RowCount - 1
            If InStr(dgvAccessions.Rows(i).Cells(3).Value, "Po") > 0 Then
                AccCount += 1
            End If
        Next
        Return AccCount
    End Function

    Private Function GetProlisOnCount() As Integer
        Dim AccCount As Integer = 0
        For i As Integer = 0 To dgvAccessions.RowCount - 1
            If InStr(dgvAccessions.Rows(i).Cells(3).Value, "ProlisOn") > 0 Then
                AccCount += 1
            End If
        Next
        Return AccCount
    End Function

    Private Function GetToEmailCount() As Integer
        Dim AccCount As Integer = 0
        For i As Integer = 0 To dgvAccessions.RowCount - 1
            If InStr(dgvAccessions.Rows(i).Cells(3).Value, "Em") > 0 Then
                AccCount += 1
            End If
        Next
        Return AccCount
    End Function

    Private Function GetEmailCount() As Integer
        Dim AccCount As Integer = 0
        For i As Integer = 0 To dgvAccessions.RowCount - 1
            If InStr(dgvAccessions.Rows(i).Cells(3).Value, "Email") > 0 Then
                AccCount += 1
            End If
        Next
        Return AccCount
    End Function

    Private Function GetToViewCount() As Integer
        Dim AccCount As Integer = 0
        'Criteria = Replace(Criteria, "Event_Date", "AccessionDate")
        Dim cntc As New SqlConnection(connString)
        cntc.Open()
        Dim cmdtc As New SqlCommand("Select Count(Base_ID) as " &
        "AC from Req_RPT where EntryDate between '" & Format(dtpFrom.Value,
        SystemConfig.DateFormat) & "' and '" & Format(dtpTo.Value, SystemConfig.DateFormat) &
        " 23:59:00' and not Base_ID in (Select Accession_ID from " &
        "Event_Capture where Event_ID = 10)", cntc)
        cmdtc.CommandType = CommandType.Text
        Dim drtc As SqlDataReader = cmdtc.ExecuteReader
        If drtc.HasRows Then
            While drtc.Read
                AccCount = drtc("AC")
            End While
        End If
        cntc.Close()
        cntc = Nothing
        Return AccCount
    End Function

    Private Function GetViewCount() As Integer
        Dim AccCount As Integer = 0
        For i As Integer = 0 To dgvAccessions.RowCount - 1
            If InStr(dgvAccessions.Rows(i).Cells(3).Value, "View") > 0 Then
                AccCount += 1
            End If
        Next
        Return AccCount
    End Function

    Private Function GetToPrintCount() As Integer
        Dim AccCount As Integer = 0
        For i As Integer = 0 To dgvAccessions.RowCount - 1
            If InStr(dgvAccessions.Rows(i).Cells(3).Value, "Pt") > 0 Then
                AccCount += 1
            End If
        Next
        Return AccCount
    End Function

    Private Function GetPrintCount() As Integer
        Dim AccCount As Integer = 0
        For i As Integer = 0 To dgvAccessions.RowCount - 1
            If InStr(dgvAccessions.Rows(i).Cells(3).Value, "Print") > 0 Then
                AccCount += 1
            End If
        Next
        Return AccCount
    End Function

    Private Function GetPartialCount() As Integer
        Dim AccCount As Integer = 0
        For i As Integer = 0 To dgvAccessions.RowCount - 1
            If InStr(dgvAccessions.Rows(i).Cells(2).Value, "PARTIAL") > 0 _
            Or InStr(dgvAccessions.Rows(i).Cells(2).Value, "Incomp") > 0 Then
                AccCount += 1
            End If
        Next
        Return AccCount
    End Function

    Private Function GetUnprocessedCount(ByVal Criteria As String) As Integer
        Dim FinalC As Integer = 0
        Dim PartialC As Integer = 0
        Dim InitialC As Integer = 0
        Dim CSC As Integer = 0
        Try
            'Criteria = Replace(Criteria, "Event_Date", "AccessionDate")
            'Criteria = Replace(Criteria, "Accession_ID", "ID")
            Dim cntc As New SqlConnection(connString)
            cntc.Open()
            Dim cmdtc As New SqlCommand("Select Count(ID) as FinalC from " &
            "Requisitions where Reported_Final between '" & Format(dtpFrom.Value, SystemConfig.DateFormat) &
            "' and '" & Format(dtpTo.Value, SystemConfig.DateFormat) & " 23:59:00'" & " and ID in " &
            "(Select Base_ID from Req_RPT where EntrySource = 'Accession') " &
            "and Not ID in (Select Accession_ID from Event_Capture where Event_ID in " &
            "(10,11,12,13,14,15,16,17,18,19))", cntc)
            cmdtc.CommandType = CommandType.Text
            Dim drtc As SqlDataReader = cmdtc.ExecuteReader
            If drtc.HasRows Then
                While drtc.Read
                    FinalC = drtc("FinalC")
                End While
            End If
            cntc.Close()
            cntc = Nothing
            '
            Dim cnpc As New SqlConnection(connString)
            cnpc.Open()
            Dim cmdpc As New SqlCommand("Select Count(ID) as PartialC from " &
            "Requisitions where ReportedOn between '" & Format(dtpFrom.Value, SystemConfig.DateFormat) &
            "' and '" & Format(dtpTo.Value, SystemConfig.DateFormat) & " 23:59:00'" & " and " &
            "Reported_Final is NULL and ID in (Select Base_ID from Req_RPT where EntrySource = " &
            "'Accession' and RPT_Complete = 0) and Not ID in (Select Accession_ID from " &
            "Event_Capture where Event_ID in (10,11,12,13,14,15,16,17,18,19))", cnpc)
            cmdpc.CommandType = CommandType.Text
            Dim drpc As SqlDataReader = cmdpc.ExecuteReader
            If drpc.HasRows Then
                While drpc.Read
                    PartialC = drpc("PartialC")
                End While
            End If
            cnpc.Close()
            cnpc = Nothing
            '
            Dim cnic As New SqlConnection(connString)
            cnic.Open()
            Dim cmdic As New SqlCommand("Select Count(ID) as InitialC from " &
            "Requisitions where Reported_Initial between '" & Format(dtpFrom.Value,
            SystemConfig.DateFormat) & "' and '" & Format(dtpTo.Value, SystemConfig.DateFormat) & " 23:59:00'" &
            " and Reported_Final is NULL and ReportedOn is NULL and ID in (Select Base_ID " &
            "from Req_RPT where EntrySource <> 'Accession' and EntryDate between '" &
            Format(dtpFrom.Value, SystemConfig.DateFormat) & "' and '" & Format(dtpTo.Value, SystemConfig.DateFormat) &
            " 23:59:00') and Not ID in (Select Accession_ID from Event_Capture where Event_ID " &
            "in (10,11,12,13,14,15,16,17,18,19))", cnic)
            cmdic.CommandType = CommandType.Text
            Dim dric As SqlDataReader = cmdic.ExecuteReader
            If dric.HasRows Then
                While dric.Read
                    InitialC = dric("InitialC")
                End While
            End If
            cnic.Close()
            cnic = Nothing
            '
            Dim cnc As New SqlConnection(connString)
            cnc.Open()
            Dim cmdc As New SqlCommand("Select Count(ID) as CSC from Requisitions " &
            "where ID in (Select Base_ID from Req_RPT where EntrySource <> 'Accession' and " &
            "EntryDate between '" & Format(dtpFrom.Value, SystemConfig.DateFormat) & "' and '" &
            Format(dtpTo.Value, SystemConfig.DateFormat) & " 23:59:00') and Not ID in (Select Accession_ID " &
            "from Event_Capture where Event_ID in (10,11,12,13,14,15,16,17,18,19))", cnc)
            cmdc.CommandType = CommandType.Text
            Dim drc As SqlDataReader = cmdc.ExecuteReader
            If drc.HasRows Then
                While drc.Read
                    CSC = drc("CSC")
                End While
            End If
            cnc.Close()
            cnc = Nothing
        Catch Ex As Exception
            MsgBox(Ex.Message, MsgBoxStyle.Critical, "Prolis")
        End Try
        Return FinalC + PartialC + InitialC + CSC
    End Function

    Private Function GetCriteria() As String
        Dim Criteria As String = ""
        If dtpFrom.Value <= dtpTo.Value Then
            Criteria = "a.Event_Date between '" & Format(dtpFrom.Value, SystemConfig.DateFormat) &
            "' and '" & Format(dtpTo.Value, SystemConfig.DateFormat) & " 23:59:00'"
        Else
            Criteria = "a.Event_Date between '" & Format(dtpTo.Value, SystemConfig.DateFormat) &
            "' and '" & Format(dtpFrom.Value, SystemConfig.DateFormat) & " 23:59:00'"
        End If
        If cmbCriteria.SelectedIndex = 0 Then   'Accession ID
            If txtTerm.Text <> "" Then Criteria += " and a.Accession_ID = " & Val(txtTerm.Text)
        ElseIf cmbCriteria.SelectedIndex = 1 Then   'Provider ID
            If txtTerm.Text <> "" Then Criteria += " and a.Accession_ID in (Select ID from Requisitions " &
            "where OrderingProvider_ID = " & Val(txtTerm.Text) & ")"
        ElseIf cmbCriteria.SelectedIndex = 2 Then   'Provider Name
            If Trim(txtTerm.Text) <> "" Then
                Dim Data() As String
                If InStr(txtTerm.Text, ",") > 0 Then
                    Data = Split(txtTerm.Text, ",")
                    If Trim(Data(0)) <> "" And Trim(Data(1)) <> "" Then 'Last and First Names
                        Criteria += " and a.Accession_ID in (Select ID from Requisitions where " &
                        "OrderingProvider_ID in (Select ID from Providers where LastName_BSN like '" &
                        Trim(Data(0)) & "%' and FirstName like '" & Trim(Data(1)) & "%'))"
                    ElseIf Trim(Data(0)) <> "" And Trim(Data(1)) = "" Then 'Last Name
                        Criteria += " and a.Accession_ID in (Select ID from Requisitions where " &
                        "OrderingProvider_ID in (Select ID from Providers where LastName_BSN like '" &
                        Trim(Data(0)) & "%'))"
                    End If
                Else
                    Criteria += " and a.Accession_ID in (Select ID from Requisitions where " &
                    "OrderingProvider_ID in (Select ID from Providers where LastName_BSN like '" &
                    Trim(txtTerm.Text) & "%'))"
                End If
            End If
        ElseIf cmbCriteria.SelectedIndex = 3 Then   'Patient ID
            If txtTerm.Text <> "" Then Criteria += " and a.Accession_ID in (Select ID from Requisitions " &
            "where Patient_ID = " & Trim(txtTerm.Text) & ")"
        ElseIf cmbCriteria.SelectedIndex = 4 Then   'Patient Name
            If Trim(txtTerm.Text) <> "" Then
                Dim Data() As String
                If InStr(txtTerm.Text, ",") > 0 Then
                    Data = Split(txtTerm.Text, ",")
                    If Trim(Data(0)) <> "" And Trim(Data(1)) <> "" Then 'Last and First Names
                        Criteria += " and a.Accession_ID in (Select ID from Requisitions where Patient_ID " &
                        "in (Select ID from Patients where LastName like '" & Trim(Data(0)) &
                        "%' and FirstName like '" & Trim(Data(1)) & "%'))"
                    ElseIf Trim(Data(0)) <> "" And Trim(Data(1)) = "" Then 'Last Name
                        Criteria += " and a.Accession_ID in (Select ID from Requisitions where Patient_ID " &
                        "in (Select ID from Patients where LastName like '" & Trim(Data(0)) & "%'))"
                    End If
                Else
                    Criteria += " and a.Accession_ID in (Select ID from Requisitions where Patient_ID " &
                        "in (Select ID from Patients where LastName like '" & Trim(txtTerm.Text) & "%'))"
                End If
            End If
        End If
        Return Criteria
    End Function

    Private Function GetPatientName(ByVal PatID As Long) As String
        Dim PatName As String = ""
        Dim cnpat As New SqlConnection(connString)
        cnpat.Open()
        Dim cmdpat As New SqlCommand("Select " &
        "* from Patients where ID = " & PatID, cnpat)
        cmdpat.CommandType = CommandType.Text
        Dim drpat As SqlDataReader = cmdpat.ExecuteReader
        If drpat.HasRows Then
            While drpat.Read
                PatName = drpat("LastName") & ", " & drpat("FirstName") &
                " , Gender: " & drpat("Sex") & " , DOB: " & Format(drpat("DOB"), SystemConfig.DateFormat)
            End While
        End If
        cnpat.Close()
        cnpat = Nothing
        Return PatName
    End Function

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Dim stopWatch As New Stopwatch()
        stopWatch.Start()
        '
        AccIds.Text = ""
        AccIds.Enabled = True
        DisplayReports()
        '
        stopWatch.Stop()
        lblStatus.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
        stopWatch.Elapsed.Hours, stopWatch.Elapsed.Minutes,
        stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds / 10)
        My.Application.DoEvents()
    End Sub

    Private Sub chkPUnP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPUnP.CheckedChanged
        If chkPUnP.Checked = False Then
            chkPUnP.Text = "Processed"
        Else
            chkPUnP.Text = "Failed"
        End If
    End Sub

    Private Sub dgvAccessions_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAccessions.CellClick
        If e.RowIndex <> -1 Then
            AccIds.Text = dgvAccessions.Rows(e.RowIndex).Cells(0).Value
            If e.ColumnIndex = 4 Then
                Dim c = 0

                If dgvAccessions.RowCount > 0 Then

                    For i = 0 To dgvAccessions.RowCount - 1
                        If dgvAccessions.Rows(i).Cells(4).Value Then
                            AccIds.Text = "Selected Accessions"
                            Exit For
                        Else
                            AccIds.Text = ""
                        End If


                        If c > 1 Then

                        End If
                    Next
                End If
                If c > 1 Then
                    AccIds.Text = "Selected Accessions"
                End If
            End If
            If dgvAccessions.Rows(e.RowIndex).Cells(4).Value Then

                AccIds.Enabled = False
            Else
                AccIds.Enabled = True
            End If
            If e.ColumnIndex <> 4 Then
                DisplayPatient(dgvAccessions.Rows(e.RowIndex).Cells(0).Value)
                DisplayProvider(dgvAccessions.Rows(e.RowIndex).Cells(0).Value)
                If InStr(dgvAccessions.Rows(e.RowIndex).Cells(3).Value, "Fax") > 0 Then
                    txtProcessDetail.Text = ""
                    DisplayFaxLog(dgvAccessions.Rows(e.RowIndex).Cells(0).Value)
                ElseIf InStr(dgvAccessions.Rows(e.RowIndex).Cells(3).Value, "Print") > 0 Then
                    txtProcessDetail.Text = ""
                    DisplayPrintLog(dgvAccessions.Rows(e.RowIndex).Cells(0).Value)
                ElseIf InStr(dgvAccessions.Rows(e.RowIndex).Cells(3).Value, "ProlisOn") > 0 Then
                    txtProcessDetail.Text = ""
                    DisplayProlisonLog(dgvAccessions.Rows(e.RowIndex).Cells(0).Value)
                ElseIf InStr(dgvAccessions.Rows(e.RowIndex).Cells(3).Value, "Email") > 0 Then
                    txtProcessDetail.Text = ""
                    DisplayEmailLog(dgvAccessions.Rows(e.RowIndex).Cells(0).Value)
                ElseIf InStr(dgvAccessions.Rows(e.RowIndex).Cells(3).Value, "HL7") > 0 Then
                    txtProcessDetail.Text = ""
                    DisplayHL7Log(dgvAccessions.Rows(e.RowIndex).Cells(0).Value)
                ElseIf InStr(dgvAccessions.Rows(e.RowIndex).Cells(3).Value, "PDF") > 0 Then
                    txtProcessDetail.Text = ""
                    DisplayPDFLog(dgvAccessions.Rows(e.RowIndex).Cells(0).Value)
                Else
                    txtProcessDetail.Text = ""
                End If
            End If
        End If
    End Sub

    Private Sub DisplayPDFLog(ByVal AccID As Long)
        Dim ProcDet As String = ""
        Dim sSQL As String = "Select * from Event_Capture where Event_ID in " &
        "(15, 150) and Accession_ID = " & AccID & " order by Event_Date DESC"
        If connString <> "" Then
            Dim cnp As New SqlConnection(connString)
            cnp.Open()
            Dim cmdp As New SqlCommand(sSQL, cnp)
            cmdp.CommandType = CommandType.Text
            Dim drp As SqlDataReader = cmdp.ExecuteReader
            If drp.HasRows Then
                While drp.Read
                    ProcDet += "Accession ID: " & AccID & "  processed" & vbCrLf &
                    "by Bridge on " & Format(drp("Event_Date"), SystemConfig.DateFormat) _
                    & " at " & Format(drp("Event_Date"), "HH:mm:ss") &
                    vbCrLf & "-----------------------" & vbCrLf
                End While
                If Len(ProcDet) > Len(vbCrLf & "-----------------------" & vbCrLf) Then
                    ProcDet = Microsoft.VisualBasic.Mid(ProcDet, 1, Len(ProcDet) - Len(vbCrLf &
                    "-----------------------" & vbCrLf))
                End If
            End If
            cnp.Close()
            cnp = Nothing
        Else
            Dim cnp As New Odbc.OdbcConnection(connString)
            cnp.Open()
            Dim cmdp As New Odbc.OdbcCommand(sSQL, cnp)
            cmdp.CommandType = CommandType.Text
            Dim drp As Odbc.OdbcDataReader = cmdp.ExecuteReader
            If drp.HasRows Then
                While drp.Read
                    ProcDet += "Accession ID: " & AccID & "  processed" & vbCrLf &
                    "by Bridge on " & Format(drp("Event_Date"), SystemConfig.DateFormat) _
                    & " at " & Format(drp("Event_Date"), "HH:mm:ss") &
                    vbCrLf & "-----------------------" & vbCrLf
                End While
                If Len(ProcDet) > Len(vbCrLf & "-----------------------" & vbCrLf) Then
                    ProcDet = Microsoft.VisualBasic.Mid(ProcDet, 1, Len(ProcDet) - Len(vbCrLf &
                    "-----------------------" & vbCrLf))
                End If
            End If
            cnp.Close()
            cnp = Nothing
        End If
        txtProcessDetail.Text = ProcDet
    End Sub

    Private Sub DisplayHL7Log(ByVal AccID As Long)
        Dim ProcDet As String = ""
        Dim sSQL As String = "Select * from Event_Capture where Event_ID in " &
        "(14, 140) and Accession_ID = " & AccID & " order by Event_Date DESC"
        If connString <> "" Then
            Dim cnp As New SqlConnection(connString)
            cnp.Open()
            Dim cmdp As New SqlCommand(sSQL, cnp)
            cmdp.CommandType = CommandType.Text
            Dim drp As SqlDataReader = cmdp.ExecuteReader
            If drp.HasRows Then
                While drp.Read
                    ProcDet += "Accession ID: " & AccID & "  processed" & vbCrLf &
                    "by Bridge on " & Format(drp("Event_Date"), SystemConfig.DateFormat) _
                    & " at " & Format(drp("Event_Date"), "HH:mm:ss") &
                    vbCrLf & "-----------------------" & vbCrLf
                End While
                If Len(ProcDet) > Len(vbCrLf & "-----------------------" & vbCrLf) Then
                    ProcDet = Microsoft.VisualBasic.Mid(ProcDet, 1, Len(ProcDet) - Len(vbCrLf &
                    "-----------------------" & vbCrLf))
                End If
            End If
            cnp.Close()
            cnp = Nothing
        Else
            Dim cnp As New Odbc.OdbcConnection(connString)
            cnp.Open()
            Dim cmdp As New Odbc.OdbcCommand(sSQL, cnp)
            cmdp.CommandType = CommandType.Text
            Dim drp As Odbc.OdbcDataReader = cmdp.ExecuteReader
            If drp.HasRows Then
                While drp.Read
                    ProcDet += "Accession ID: " & AccID & "  processed" & vbCrLf &
                    "by Bridge on " & Format(drp("Event_Date"), SystemConfig.DateFormat) _
                    & " at " & Format(drp("Event_Date"), "HH:mm:ss") &
                    vbCrLf & "-----------------------" & vbCrLf
                End While
                If Len(ProcDet) > Len(vbCrLf & "-----------------------" & vbCrLf) Then
                    ProcDet = Microsoft.VisualBasic.Mid(ProcDet, 1, Len(ProcDet) - Len(vbCrLf &
                    "-----------------------" & vbCrLf))
                End If
            End If
            cnp.Close()
            cnp = Nothing
        End If
        txtProcessDetail.Text = ProcDet
    End Sub

    Private Sub DisplayProlisonLog(ByVal AccID As Long)
        Dim ProcDet As String = ""
        Dim sSQL As String = "Select * from Event_Capture where Event_ID = " &
        "16 and Accession_ID = " & AccID & " order by Event_Date DESC"
        If connString <> "" Then
            Dim cnp As New SqlConnection(connString)
            cnp.Open()
            Dim cmdp As New SqlCommand(sSQL, cnp)
            cmdp.CommandType = CommandType.Text
            Dim drp As SqlDataReader = cmdp.ExecuteReader
            If drp.HasRows Then
                While drp.Read
                    ProcDet += "Accession ID: " & AccID & "  processed" & vbCrLf &
                    "by ProlisOn on " & Format(drp("Event_Date"), SystemConfig.DateFormat) _
                    & " at " & Format(drp("Event_Date"), "HH:mm:ss") &
                    vbCrLf & "-----------------------" & vbCrLf
                End While
                If Len(ProcDet) > Len(vbCrLf & "-----------------------" & vbCrLf) Then
                    ProcDet = Microsoft.VisualBasic.Mid(ProcDet, 1, Len(ProcDet) - Len(vbCrLf &
                    "-----------------------" & vbCrLf))
                End If
            End If
            cnp.Close()
            cnp = Nothing
        Else
            Dim cnp As New Odbc.OdbcConnection(connString)
            cnp.Open()
            Dim cmdp As New Odbc.OdbcCommand(sSQL, cnp)
            cmdp.CommandType = CommandType.Text
            Dim drp As Odbc.OdbcDataReader = cmdp.ExecuteReader
            If drp.HasRows Then
                While drp.Read
                    ProcDet += "Accession ID: " & AccID & "  processed" & vbCrLf &
                    "by ProlisOn on " & Format(drp("Event_Date"), SystemConfig.DateFormat) _
                    & " at " & Format(drp("Event_Date"), "HH:mm:ss") &
                    vbCrLf & "-----------------------" & vbCrLf
                End While
                If Len(ProcDet) > Len(vbCrLf & "-----------------------" & vbCrLf) Then
                    ProcDet = Microsoft.VisualBasic.Mid(ProcDet, 1, Len(ProcDet) - Len(vbCrLf &
                    "-----------------------" & vbCrLf))
                End If
            End If
            cnp.Close()
            cnp = Nothing
        End If
        txtProcessDetail.Text = ProcDet
    End Sub

    Private Sub DisplayPrintLog(ByVal AccID As Long)
        Dim ProcDet As String = ""
        Dim sSQL As String = "Select * from Event_Capture where Event_ID = " &
        "11 and Accession_ID = " & AccID & " order by Event_Date DESC"
        If connString <> "" Then
            Dim cnp As New SqlConnection(connString)
            cnp.Open()
            Dim cmdp As New SqlCommand(sSQL, cnp)
            cmdp.CommandType = CommandType.Text
            Dim drp As SqlDataReader = cmdp.ExecuteReader
            If drp.HasRows Then
                While drp.Read
                    ProcDet += "Accession ID: " & AccID & "  processed" & vbCrLf &
                    "by the Lab User on " & Format(drp("Event_Date"), SystemConfig.DateFormat) _
                    & " at " & Format(drp("Event_Date"), "HH:mm:ss") &
                    vbCrLf & "-----------------------" & vbCrLf
                End While
                If Len(ProcDet) > Len(vbCrLf & "-----------------------" & vbCrLf) Then
                    ProcDet = Microsoft.VisualBasic.Mid(ProcDet, 1, Len(ProcDet) - Len(vbCrLf &
                    "-----------------------" & vbCrLf))
                End If
            End If
            cnp.Close()
            cnp = Nothing
        Else
            Dim cnp As New Odbc.OdbcConnection(connString)
            cnp.Open()
            Dim cmdp As New Odbc.OdbcCommand(sSQL, cnp)
            cmdp.CommandType = CommandType.Text
            Dim drp As Odbc.OdbcDataReader = cmdp.ExecuteReader
            If drp.HasRows Then
                While drp.Read
                    ProcDet += "Accession ID: " & AccID & "  processed" & vbCrLf &
                    "by the Lab User on " & Format(drp("Event_Date"), SystemConfig.DateFormat) _
                    & " at " & Format(drp("Event_Date"), "HH:mm:ss") &
                    vbCrLf & "-----------------------" & vbCrLf
                End While
                If Len(ProcDet) > Len(vbCrLf & "-----------------------" & vbCrLf) Then
                    ProcDet = Microsoft.VisualBasic.Mid(ProcDet, 1, Len(ProcDet) - Len(vbCrLf &
                    "-----------------------" & vbCrLf))
                End If
            End If
            cnp.Close()
            cnp = Nothing
        End If
        txtProcessDetail.Text = ProcDet
    End Sub

    Private Sub DisplayProvider(ByVal AccID As Long)
        Dim PrName As String = ""
        Dim sSQL As String = "Select * from Providers where ID in (Select " &
        "OrderingProvider_ID from Requisitions where ID = " & AccID & ")"
        If connString <> "" Then
            Dim cnp As New SqlConnection(connString)
            cnp.Open()
            Dim cmdp As New SqlCommand(sSQL, cnp)
            cmdp.CommandType = CommandType.Text
            Dim drp As SqlDataReader = cmdp.ExecuteReader
            If drp.HasRows Then
                While drp.Read
                    If drp("IsIndividual") = False Then
                        PrName = drp("LastName_BSN") & vbCrLf
                        If drp("Address_ID") IsNot DBNull.Value Then
                            PrName += GetAddress1(drp("Address_ID")) & " " & GetAddress2(drp("Address_ID")) &
                            vbCrLf & GetAddressCity(drp("Address_ID")) & ", " & GetAddressState(drp("Address_ID")) &
                            GetAddressZip(drp("Address_ID")) & vbCrLf & GetAddressCountry(drp("Address_ID")) &
                            IIf(drp("Phone") Is DBNull.Value, "", vbCrLf & "Phone: " & drp("Phone"))
                        End If
                    Else
                        PrName = drp("LastName_BSN") & ", " & drp("FirstName") &
                        IIf(drp("Degree") Is DBNull.Value, "", " " & drp("Degree")) & vbCrLf
                        If drp("Address_ID") IsNot DBNull.Value Then
                            PrName += GetAddress1(drp("Address_ID")) & " " & GetAddress2(drp("Address_ID")) &
                            vbCrLf & GetAddressCity(drp("Address_ID")) & ", " & GetAddressState(drp("Address_ID")) &
                            " " & GetAddressZip(drp("Address_ID")) & vbCrLf & "Phone: " &
                            IIf(drp("Phone") Is DBNull.Value, "", drp("Phone"))
                        End If
                    End If
                End While
            End If
            cnp.Close()
            cnp = Nothing
        Else
            Dim cnp As New Odbc.OdbcConnection(connString)
            cnp.Open()
            Dim cmdp As New Odbc.OdbcCommand(sSQL, cnp)
            cmdp.CommandType = CommandType.Text
            Dim drp As Odbc.OdbcDataReader = cmdp.ExecuteReader
            If drp.HasRows Then
                While drp.Read
                    If drp("IsIndividual") = False Then
                        PrName = drp("LastName_BSN") & vbCrLf
                        If drp("Address_ID") IsNot DBNull.Value Then
                            PrName += GetAddress1(drp("Address_ID")) & " " & GetAddress2(drp("Address_ID")) &
                            vbCrLf & GetAddressCity(drp("Address_ID")) & ", " & GetAddressState(drp("Address_ID")) &
                            GetAddressZip(drp("Address_ID")) & vbCrLf & GetAddressCountry(drp("Address_ID")) &
                            IIf(drp("Phone") Is DBNull.Value, "", vbCrLf & "Phone: " & drp("Phone"))
                        End If
                    Else
                        PrName = drp("LastName_BSN") & ", " & drp("FirstName") &
                        IIf(drp("Degree") Is DBNull.Value, "", " " & drp("Degree")) & vbCrLf
                        If drp("Address_ID") IsNot DBNull.Value Then
                            PrName += GetAddress1(drp("Address_ID")) & " " & GetAddress2(drp("Address_ID")) &
                            vbCrLf & GetAddressCity(drp("Address_ID")) & ", " & GetAddressState(drp("Address_ID")) &
                            " " & GetAddressZip(drp("Address_ID")) & vbCrLf & "Phone: " &
                            IIf(drp("Phone") Is DBNull.Value, "", drp("Phone"))
                        End If
                    End If
                End While
            End If
            cnp.Close()
            cnp = Nothing
        End If
        txtProvider.Text = PrName
    End Sub

    Private Sub DisplayPatient(ByVal AccID As Long)
        txtPatient.Text = ""
        Dim Pat As String = ""
        Dim sSQL As String = "Select * from Patients where ID in (Select Patient_ID from Requisitions where ID = " & AccID & ")"
        If connString <> "" Then
            Dim cnp As New SqlConnection(connString)
            cnp.Open()
            Dim cmdp As New SqlCommand(sSQL, cnp)
            cmdp.CommandType = CommandType.Text
            Dim drp As SqlDataReader = cmdp.ExecuteReader
            If drp.HasRows Then
                While drp.Read
                    Pat = "Name: " & drp("LastName") & ", " & drp("FirstName") & vbCrLf
                    Pat += "DOB: " & Format(drp("DOB"), SystemConfig.DateFormat) &
                    "   Sex: " & drp("Sex") & vbCrLf
                    If drp("Address_ID") IsNot DBNull.Value Then
                        Pat += "Address: " & GetAddress1(drp("Address_ID")) & " " &
                        GetAddress2(drp("Address_ID")) & ", " & GetAddressCity(drp("Address_ID")) &
                        ", " & GetAddressState(drp("Address_ID")) & " " & GetAddressZip(drp("Address_ID"))
                    Else
                        Pat += "Address: None"
                    End If
                    If drp("HomePhone") IsNot DBNull.Value AndAlso
                    Trim(drp("HomePhone")) <> "" Then Pat += vbCrLf &
                    "Phone: " & Trim(drp("HomePhone"))
                End While
            End If
            cnp.Close()
            cnp = Nothing
        Else
            Dim cnp As New Odbc.OdbcConnection(connString)
            cnp.Open()
            Dim cmdp As New Odbc.OdbcCommand(sSQL, cnp)
            cmdp.CommandType = CommandType.Text
            Dim drp As Odbc.OdbcDataReader = cmdp.ExecuteReader
            If drp.HasRows Then
                While drp.Read
                    Pat = "Name: " & drp("LastName") & ", " & drp("FirstName") & vbCrLf
                    Pat += "DOB: " & Format(drp("DOB"), SystemConfig.DateFormat) &
                    "   Sex: " & drp("Sex") & vbCrLf
                    If drp("Address_ID").Value IsNot DBNull.Value Then
                        Pat += "Address: " & GetAddress1(drp("Address_ID")) & " " &
                        GetAddress2(drp("Address_ID")) & ", " & GetAddressCity(drp("Address_ID")) &
                        ", " & GetAddressState(drp("Address_ID")) & " " & GetAddressZip(drp("Address_ID"))
                    Else
                        Pat += "Address: None"
                    End If
                    If drp("HomePhone") IsNot DBNull.Value AndAlso
                    Trim(drp("HomePhone")) <> "" Then Pat += vbCrLf &
                    "Phone: " & Trim(drp("HomePhone"))
                End While
            End If
            cnp.Close()
            cnp = Nothing
        End If
        txtPatient.Text = Pat
    End Sub

    Private Sub DisplayEmailLog(ByVal AccID As Long)
        Dim ProcDet As String = ""
        Dim Source As String = ""
        Dim sSQL As String = "Select a.*, b.Event_Source from Email_Log a inner join Event_Capture b on " &
        "a.Accession_ID = b.Accession_ID where b.Event_ID = 13 and a.Accession_ID = " & AccID &
        " order by a.Accession_ID DESC"
        If connString <> "" Then
            Dim cne As New SqlConnection(connString)
            cne.Open()
            Dim cmde As New SqlCommand(sSQL, cne)
            cmde.CommandType = CommandType.Text
            Dim dre As SqlDataReader = cmde.ExecuteReader
            If dre.HasRows Then
                While dre.Read
                    If dre("Event_Source") = "Accession" Or
                    dre("Event_Source") = "HL7" Then
                        Source = "Automation"
                    ElseIf dre("Event_Source") = "CS" Then
                        Source = "Client Service"
                    Else
                        Source = dre("Event_Source")
                    End If
                    ProcDet += "Accession ID: " & AccID & " Source: " & Source & vbCrLf
                    If dre("SentTime") IsNot DBNull.Value Then
                        ProcDet += "Sent Time: " & Format(dre("SentTime"),
                        SystemConfig.DateFormat & " HH:mm:ss") & vbCrLf
                    End If
                    ProcDet += "Email: " & dre("Email") & vbCrLf &
                    "Recipient: " & dre("Recipient") & vbCrLf
                    If dre("Status") IsNot DBNull.Value AndAlso
                     InStr(ProcDet, dre("Status")) = 0 Then
                        ProcDet += "Status: " & dre("Status") &
                        vbCrLf & "-----------------------" & vbCrLf
                    End If
                End While
                If Len(ProcDet) > Len(vbCrLf & "-----------------------" & vbCrLf) Then
                    ProcDet = Microsoft.VisualBasic.Mid(ProcDet, 1, Len(ProcDet) - Len(vbCrLf &
                    "-----------------------" & vbCrLf))
                End If
            End If
            cne.Close()
            cne = Nothing
        Else
            Dim cne As New Odbc.OdbcConnection(connString)
            cne.Open()
            Dim cmde As New Odbc.OdbcCommand(sSQL, cne)
            cmde.CommandType = CommandType.Text
            Dim dre As Odbc.OdbcDataReader = cmde.ExecuteReader
            If dre.HasRows Then
                While dre.Read
                    If dre("Event_Source") = "Accession" Or
                    dre("Event_Source") = "HL7" Then
                        Source = "Automation"
                    ElseIf dre("Event_Source") = "CS" Then
                        Source = "Client Service"
                    Else
                        Source = dre("Event_Source")
                    End If
                    ProcDet += "Accession ID: " & AccID & " Source: " & Source & vbCrLf
                    If dre("SentTime") IsNot DBNull.Value Then
                        ProcDet += "Sent Time: " & Format(dre("SentTime"),
                        SystemConfig.DateFormat & " HH:mm:ss") & vbCrLf
                    End If
                    ProcDet += "Email: " & dre("Email") & vbCrLf &
                    "Recipient: " & dre("Recipient") & vbCrLf
                    If dre("Status") IsNot DBNull.Value AndAlso
                     InStr(ProcDet, dre("Status")) = 0 Then
                        ProcDet += "Status: " & dre("Status") &
                        vbCrLf & "-----------------------" & vbCrLf
                    End If
                End While
                If Len(ProcDet) > Len(vbCrLf & "-----------------------" & vbCrLf) Then
                    ProcDet = Microsoft.VisualBasic.Mid(ProcDet, 1, Len(ProcDet) - Len(vbCrLf &
                    "-----------------------" & vbCrLf))
                End If
            End If
            cne.Close()
            cne = Nothing
        End If
        txtProcessDetail.Text = ProcDet
    End Sub

    Private Sub DisplayFaxLog(ByVal AccID As Long)
        Dim ProcDet As String = ""
        Dim Source As String = ""
        Dim sSQL As String = "Select a.*, b.Event_Source from Fax_Log a inner join Event_Capture b on " &
        "a.Accession_ID = b.Accession_ID where b.Event_ID = 12 and a.Accession_ID = " & AccID &
        " order by a.Accession_ID DESC"
        If connString <> "" Then
            Dim cnf As New SqlConnection(connString)
            cnf.Open()
            Dim cmdf As New SqlCommand(sSQL, cnf)
            cmdf.CommandType = CommandType.Text
            Dim drf As SqlDataReader = cmdf.ExecuteReader
            If drf.HasRows Then
                While drf.Read
                    If drf("Event_Source") = "Accession" Or
                    drf("Event_Source") = "Fax" Then
                        Source = "Automation"
                    ElseIf drf("Event_Source") = "CS" Then
                        Source = "Client Service"
                    Else
                        Source = drf("Event_Source")
                    End If
                    ProcDet += "Accession ID: " & AccID & " Source: " & Source & vbCrLf
                    If drf("SentTime") IsNot DBNull.Value Then
                        ProcDet += "Sent Time: " & Format(drf("SentTime"),
                        SystemConfig.DateFormat & " HH:mm") & vbCrLf
                    End If
                    ProcDet += "Fax: " & drf("FaxNumber") &
                    vbCrLf & "Recipient: " & drf("Recipient") & vbCrLf
                    If drf("Status") IsNot DBNull.Value AndAlso
                    InStr(ProcDet, drf("Status")) = 0 Then
                        ProcDet += "Status: " & drf("Status") &
                        vbCrLf & "-----------------------" & vbCrLf
                    End If
                End While
                If Len(ProcDet) > Len(vbCrLf & "-----------------------" & vbCrLf) Then
                    ProcDet = Microsoft.VisualBasic.Mid(ProcDet, 1, Len(ProcDet) - Len(vbCrLf &
                    "-----------------------" & vbCrLf))
                End If
            End If
            cnf.Close()
            cnf = Nothing
        Else
            Dim cnf As New Odbc.OdbcConnection(connString)
            cnf.Open()
            Dim cmdf As New Odbc.OdbcCommand(sSQL, cnf)
            cmdf.CommandType = CommandType.Text
            Dim drf As Odbc.OdbcDataReader = cmdf.ExecuteReader
            If drf.HasRows Then
                While drf.Read
                    If drf("Event_Source") = "Accession" Or
                    drf("Event_Source") = "Fax" Then
                        Source = "Automation"
                    ElseIf drf("Event_Source") = "CS" Then
                        Source = "Client Service"
                    Else
                        Source = drf("Event_Source")
                    End If
                    ProcDet += "Accession ID: " & AccID & " Source: " & Source & vbCrLf
                    If drf("SentTime") IsNot DBNull.Value Then
                        ProcDet += "Sent Time: " & Format(drf("SentTime"),
                        SystemConfig.DateFormat & " HH:mm:ss") & vbCrLf
                    End If
                    ProcDet += "Fax: " & drf("FaxNumber") &
                    vbCrLf & "Recipient: " & drf("Recipient") & vbCrLf
                    If drf("Status") IsNot DBNull.Value AndAlso
                    InStr(ProcDet, drf("Status")) = 0 Then
                        ProcDet += "Status: " & drf("Status") &
                        vbCrLf & "-----------------------" & vbCrLf
                    End If
                End While
                If Len(ProcDet) > Len(vbCrLf & "-----------------------" & vbCrLf) Then
                    ProcDet = Microsoft.VisualBasic.Mid(ProcDet, 1, Len(ProcDet) - Len(vbCrLf &
                    "-----------------------" & vbCrLf))
                End If
            End If
            cnf.Close()
            cnf = Nothing
        End If
        txtProcessDetail.Text = ProcDet
    End Sub

    Private Sub dgvAccessions_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAccessions.CellEndEdit
        If e.ColumnIndex = 4 Then
            UpdateRedos()
        End If
    End Sub

    Private Sub UpdateRedos()
        Dim i As Integer
        Dim HUSH As Boolean = False
        For i = 0 To dgvAccessions.RowCount - 1
            If dgvAccessions.Rows(i).Cells(4).Value = True Then
                HUSH = True
                Exit For
            End If
        Next
        btnRedo.Enabled = HUSH
    End Sub

    Private Sub btnDeselAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeselAll.Click
        Dim i As Integer
        AccIds.Text = ""
        AccIds.Enabled = True

        If dgvAccessions.RowCount > 0 Then
            For i = 0 To dgvAccessions.RowCount - 1
                dgvAccessions.Rows(i).Cells(4).Value = False
            Next
        End If
        btnRedo.Enabled = False
    End Sub

    Private Sub btnSellAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSellAll.Click
        Dim i As Integer
        AccIds.Text = "Selected Accessions"
        AccIds.Enabled = False
        If dgvAccessions.RowCount > 0 Then
            For i = 0 To dgvAccessions.RowCount - 1
                dgvAccessions.Rows(i).Cells(4).Value = True

                If InStr(dgvAccessions.Rows(i).Cells(3).Value, "Fx") > 0 Or
                InStr(dgvAccessions.Rows(i).Cells(3).Value, "Em") > 0 Or
                InStr(dgvAccessions.Rows(i).Cells(3).Value, "If") > 0 Then
                    dgvAccessions.Rows(i).Cells(4).Value = True
                    btnRedo.Enabled = True
                End If
            Next
        End If
    End Sub

    Private Sub btnRedo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRedo.Click
        Dim i As Integer
        If chkPUnP.Checked = False Then 'Processed
            If dgvAccessions.RowCount > 0 Then
                For i = 0 To dgvAccessions.RowCount - 1
                    If dgvAccessions.Rows(i).Cells(4).Value = True _
                    AndAlso dgvAccessions.Rows(i).Cells(3).Value <> "" Then
                        If InStr(dgvAccessions.Rows(i).Cells(3).Value, "Fax") > 0 AndAlso
                        InStr(dgvAccessions.Rows(i).Cells(3).Value, "Fail") > 0 Then
                            ExecuteSqlProcedure("Delete from Event_Capture where Event_ID = 12 " &
                            "and Accession_ID = " & dgvAccessions.Rows(i).Cells(0).Value)
                        End If
                        'If InStr(dgvAccessions.Rows(i).Cells(3).Value, "If") > 0 Then _
                        'UpdateHL7Schedule(dgvAccessions.Rows(i).Cells(0).Value, "CS")
                        'If InStr(dgvAccessions.Rows(i).Cells(3).Value, "Po") > 0 Then
                        '    CN.Execute("Update Req_RPT Set EntryDate = '" & Date.Now & "', " & _
                        '    "EntrySource = 'CS' where RPT_ProlisOn <> 0 and Base_ID = " & _
                        '    dgvAccessions.Rows(i).Cells(0).Value)
                        '    CN.Execute("Delete from Event_Capture where Event_ID = 16 and " & _
                        '    "Accession_ID = " & dgvAccessions.Rows(i).Cells(0).Value)
                        'End If
                        'If InStr(dgvAccessions.Rows(i).Cells(3).Value, "Em") > 0 Then _
                        'UpdateEmailSchedule(dgvAccessions.Rows(i).Cells(0).Value, "CS")
                    End If
                Next
            End If
        Else    'unprocessed
            If dgvAccessions.RowCount > 0 Then
                For i = 0 To dgvAccessions.RowCount - 1
                    If dgvAccessions.Rows(i).Cells(4).Value = True _
                    AndAlso dgvAccessions.Rows(i).Cells(3).Value <> "" Then
                        If InStr(dgvAccessions.Rows(i).Cells(3).Value, "Fx") > 0 Then
                            ExecuteSqlProcedure("Delete from Event_Capture where Event_ID = 12 " &
                            "and Accession_ID = " & dgvAccessions.Rows(i).Cells(0).Value)
                        End If
                        If InStr(dgvAccessions.Rows(i).Cells(3).Value, "If") > 0 Then
                            ExecuteSqlProcedure("Delete from Event_Capture where Event_ID in (" &
                            "14, 15) and Accession_ID = " & dgvAccessions.Rows(i).Cells(0).Value)
                        End If
                        'If InStr(dgvAccessions.Rows(i).Cells(3).Value, "Po") > 0 Then
                        '    CN.Execute("Update Req_RPT Set EntryDate = '" & Date.Now & "', " & _
                        '    "EntrySource = 'CS' where RPT_ProlisOn <> 0 and Base_ID = " & _
                        '    dgvAccessions.Rows(i).Cells(0).Value)
                        '    CN.Execute("Delete from Event_Capture where Event_ID = 16 and " & _
                        '    "Accession_ID = " & dgvAccessions.Rows(i).Cells(0).Value)
                        'End If
                        If InStr(dgvAccessions.Rows(i).Cells(3).Value, "Em") > 0 Then
                            ExecuteSqlProcedure("Delete from Event_Capture where Event_ID = 13 " &
                            "and Accession_ID = " & dgvAccessions.Rows(i).Cells(0).Value)
                        End If
                    End If
                Next
            End If
        End If
        ClearForm()
    End Sub

    Private Sub frmRptDash_ResizeBegin(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeBegin
        origWidth = Me.Width
        origHeight = Me.Height
    End Sub

    Private Sub frmRptDash_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        MeResize(Me, origWidth, origHeight)
    End Sub

    Private Sub btnCancelFax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer
        If dgvAccessions.RowCount > 0 Then
            For i = 0 To dgvAccessions.RowCount - 1
                If dgvAccessions.Rows(i).Cells(4).Value = True Then _
                CancelFaxSchedule(dgvAccessions.Rows(i).Cells(0).Value)
            Next
        End If
        ClearForm()
    End Sub

    Private Sub dgvAccessions_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAccessions.CellDoubleClick
        Try
            Clipboard.SetText(dgvAccessions.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
            Label1.Text = dgvAccessions.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & " has copied to clipboard"
            Label1.ForeColor = Color.Green
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

        Me.Cursor = Cursors.WaitCursor

        If cmbDestination.SelectedIndex = 6 AndAlso ReportsFolder = "" Then
            MessageBox.Show("Please Click on Destination Combo again to Select Directory to Save Reports.")
            Return
        End If

        Dim sSQL = ""
        IsBusy = True
        sSQL = "Select a.ID as AccID, b.Id as ProvID, b.Route_ID as RouteID , b.ResRPTFile as RptFile from Requisitions a  inner join Providers b on a.OrderingProvider_ID = b.ID and a.ID in ("
        'Formula = "{Requisitions.ID} in ["
        Dim i As Integer
        If dgvAccessions.RowCount > 0 Then
            For i = 0 To dgvAccessions.RowCount - 1
                If dgvAccessions.Rows(i).Cells(4).Value = True Then
                    sSQL = sSQL & dgvAccessions.Rows(i).Cells(0).Value & ", "
                End If
            Next
        End If
        If IsNumeric(AccIds.Text) Then
            sSQL = sSQL & AccIds.Text & ", "
        End If
        sSQL = sSQL.Substring(0, Len(sSQL) - 2) & ")"
        'If cmbSort.SelectedIndex = 1 Then   'Accession
        '    sSQL = sSQL & " order by a.ID"
        'ElseIf cmbSort.SelectedIndex = 2 Then
        '    sSQL = sSQL & " order by b.ID"
        'ElseIf cmbSort.SelectedIndex = 3 Then
        '    sSQL = sSQL & " order by b.Route_ID"
        'Else
        '    sSQL = sSQL & " order by a.ID"
        'End If
        sSQL = sSQL & " order by a.ID"
        ProcessSQL(sSQL)

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub ProcessSQL(ByVal sSQL As String)
        Dim Recs As Integer
        Dim Accs() As String = {""}
        'Dim UID As String = My.Settings.UID.ToString
        'Dim PWD As String = My.Settings.PWD.ToString
        Dim RPTFile As String = ""
        Dim INSTRUCTS() As String
        Dim Configs() As String = {""}
        Dim RPTStatus As String = ""
        Dim dt As New DataTable

        Using cnrpts As New SqlConnection(connString)
            Try
                cnrpts.Open()
                Using cmdrpts As New SqlCommand(sSQL, cnrpts)
                    cmdrpts.CommandType = CommandType.Text
                    Using da As New SqlDataAdapter(cmdrpts)
                        da.Fill(dt)
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End Using

        Dim IsConfigControl = False
        '
        'Dim IsCustomRPT = chkRPT.Checked
        'Dim IsRPTFinal = chkComplete.Checked
        ' Dim IsConfigControl = chkConfig.Checked
        Dim Medium = cmbDestination.SelectedIndex
        Recs = dt.Rows.Count
        If Recs > 0 Then
            If Medium = 0 Then  'Printer
                For i As Integer = 0 To Recs - 1
                    If Not BW.CancellationPending Then
                        Try
                            Dim AccID As String = dt.Rows(i).Item("AccID").ToString

                            'RPTFile = ValidateReportFile(dt.Rows(i).Item("AccID"), False)
                            If False Then   'independent
                                'LogEvent(dt.Rows(i).Item("AccID"), 11, GetOrdProvIDFromAccID(dt.Rows(i).Item("AccID")),
                                'IIf(ReportFullResulted(dt.Rows(i).Item("AccID")) = True, "FINAL", "PARTIAL"), False,
                                'ThisUser.UserName, "Result Reports")
                                'If SystemConfig.AuditTrail = True Then _
                                'LogUserEvent(ThisUser.ID, 11, Date.Now, "REPORT", dt.Rows(i).Item("AccID"), "", "")
                                'PrintPDFReport(RPTFile, dt.Rows(i).Item("AccID").ToString)

                                RPTStatus = IIf(ReportFullResulted(AccID) = True, "FINAL", "PARTIAL")
                                LogEventAndAuditTrail(AccID, 11, RPTStatus)

                                Accs = {AccID}
                                GenerateReports(Accs, Medium, False, "", txtQty.Text)

                            Else    'under control
                                Configs = GetReportConfigs(AccID)

                                For n As Integer = 0 To Configs.Length - 1
                                    If Configs(n) <> "" Then
                                        INSTRUCTS = Split(Configs(n), "|")
                                        RPTStatus = GetReportStatus(AccID)
                                        If CType(INSTRUCTS(2), Boolean) = True Then

                                            'WHAT IS THE PURPOSE OF THESE LINES?
                                            If Accs(UBound(Accs)) <> "" Then ReDim Preserve Accs(UBound(Accs) + 1)
                                            Accs(UBound(Accs)) = AccID

                                            'LogEvent(dt.Rows(i).Item("AccID"), 11, INSTRUCTS(0),
                                            'RPTStatus, False, ThisUser.UserName, "Result Reports")
                                            'If SystemConfig.AuditTrail = True Then _
                                            'LogUserEvent(ThisUser.ID, 11, Date.Now, "REPORT",
                                            'dt.Rows(i).Item("AccID"), "", "")
                                            'PrintPDFReport(RPTFile, dt.Rows(i).Item("AccID").ToString)

                                            LogEventAndAuditTrail(AccID, 11, RPTStatus, INSTRUCTS(0))

                                            GenerateReports({AccID}, Medium, False, "", txtQty.Text)
                                        End If
                                    End If
                                Next n
                            End If
                            If BW.IsBusy Then
                                BW.ReportProgress((i + 1) * 100 / Recs, (i + 1).ToString & " of " & Recs.ToString)
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                        End Try
                    End If
                Next
            ElseIf Medium = 6 Then  'save Folder
                For i As Integer = 0 To Recs - 1

                    If Not BW.CancellationPending Then
                        Try
                            Dim AccID As String = dt.Rows(i).Item("AccID").ToString
                            'RPTFile = ValidateReportFile(dt.Rows(i).Item("AccID"), False)
                            If IsConfigControl = False Then   'independent

                                'LogEvent(dt.Rows(i).Item("AccID"), 11, GetOrdProvIDFromAccID(dt.Rows(i).Item("AccID")),
                                'IIf(ReportFullResulted(dt.Rows(i).Item("AccID")) = True, "FINAL", "PARTIAL"), False,
                                'ThisUser.UserName, "Result Reports")
                                'If SystemConfig.AuditTrail = True Then _
                                'LogUserEvent(ThisUser.ID, 11, Date.Now, "REPORT", dt.Rows(i).Item("AccID"), "", "")

                                'PrintPDFReport(RPTFile, dt.Rows(i).Item("AccID").ToString, ReportsFolder & "\" & dt.Rows(i).Item("AccID").ToString)

                                RPTStatus = IIf(ReportFullResulted(AccID) = True, "FINAL", "PARTIAL")
                                LogEventAndAuditTrail(AccID, 11, RPTStatus)
                                Accs = {AccID}
                                GenerateReports(Accs, Medium, False, ReportsFolder & "\" & AccID.ToString)

                            Else    'under control
                                Configs = GetReportConfigs(AccID)
                                For n As Integer = 0 To Configs.Length - 1
                                    If Configs(n) <> "" Then
                                        INSTRUCTS = Split(Configs(n), "|")
                                        RPTStatus = GetReportStatus(AccID)
                                        If CType(INSTRUCTS(2), Boolean) = True Then

                                            If Accs(UBound(Accs)) <> "" Then ReDim Preserve Accs(UBound(Accs) + 1)
                                            Accs(UBound(Accs)) = dt.Rows(i).Item("AccID").ToString

                                            'LogEvent(dt.Rows(i).Item("AccID"), 11, INSTRUCTS(0),
                                            'RPTStatus, False, ThisUser.UserName, "Result Reports")
                                            'If SystemConfig.AuditTrail = True Then _
                                            'LogUserEvent(ThisUser.ID, 11, Date.Now, "REPORT",
                                            'dt.Rows(i).Item("AccID"), "", "")
                                            'PrintPDFReport(RPTFile, dt.Rows(i).Item("AccID").ToString, ReportsFolder & "\" & dt.Rows(i).Item("AccID").ToString)

                                            LogEventAndAuditTrail(AccID, 11, RPTStatus, INSTRUCTS(0))

                                            GenerateReports({AccID}, Medium, False, ReportsFolder & "\" & dt.Rows(i).Item("AccID").ToString)

                                        End If
                                    End If
                                Next n
                            End If
                            If BW.IsBusy Then
                                BW.ReportProgress((i + 1) * 100 / Recs, (i + 1).ToString & " of " & Recs.ToString)
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                        End Try
                    End If
                Next
                Dim inp = MessageBox.Show("Report(s) has been saved to folder, do you want to open folder", "INFO", MessageBoxButtons.OKCancel)
                If inp = DialogResult.OK Then

                    ' Open the folder containing the saved file
                    Dim folderPath As String = ReportsFolder
                    Process.Start(folderPath)

                End If
                waitingLabel.Text = "Report(s) has beed saved to folder."
            ElseIf Medium = 1 Then  'screen
                'RPTFile = ValidateReportFile(dt.Rows(0).Item("AccID"), False)
                PB.Value = 0
                For i As Integer = 0 To Recs - 1
                    If IsBusy Then
                        Try
                            Dim AccID As String = dt.Rows(i).Item("AccID").ToString

                            If Accs(UBound(Accs)) <> "" Then ReDim Preserve Accs(UBound(Accs) + 1)
                            Accs(UBound(Accs)) = dt.Rows(i).Item("AccID").ToString

                            'LogEvent(dt.Rows(i).Item("AccID"), 10, GetOrdProvIDFromAccID(dt.Rows(i).Item("AccID")),
                            'IIf(ReportFullResulted(dt.Rows(i).Item("AccID")) = True, "FINAL", "PARTIAL"), False,
                            'ThisUser.UserName, "Result Reports")
                            'If SystemConfig.AuditTrail = True Then _
                            'LogUserEvent(ThisUser.ID, 10, Date.Now, "REPORT",
                            'dt.Rows(i).Item("AccID"), "", "")
                            RPTStatus = IIf(ReportFullResulted(AccID) = True, "FINAL", "PARTIAL")
                            LogEventAndAuditTrail(AccID, 10, RPTStatus)

                            PB.Value = (i + 1) * 100 / Recs
                            lblStatus.Text = (i + 1).ToString & " of " & Recs.ToString
                            My.Application.DoEvents()

                            If BW.IsBusy Then
                                BW.ReportProgress((i + 1) * 100 / Recs, (i + 1).ToString & " of " & Recs.ToString)
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                        End Try
                    End If
                Next
                'PrintMultiReports(RPTFile, Accs, 1)

                GenerateReports(Accs, 1, False)

            ElseIf Medium = 2 Then  'Fax
                'If IsConfigControl = False Then   'independent
                '    For i As Integer = 0 To Recs - 1
                '        If IsBusy Then
                '            ExecuteSqlProcedure("Delete from Event_Capture where Event_ID = 12 " & _
                '            "and Accession_ID = " & dt.Rows(i).Item("AccID"))
                '            ExecuteSqlProcedure("Delete from Fax_Log where Status = 'Failed' " & _
                '            "and Accession_ID = " & dt.Rows(i).Item("AccID"))
                '            PB.Value = (i + 1) * 100 / Recs
                '            lblStatus.Text = (i + 1).ToString & " of " & Recs.ToString
                '            My.Application.DoEvents()
                '            'BW.ReportProgress((i + 1) * 100 / Recs, (i + 1).ToString & " of " & Recs.ToString)
                '        End If
                '    Next
                'End If
            ElseIf Medium = 3 Then  'Interface
                'If IsConfigControl = False Then   'independent
                '    For i As Integer = 0 To Recs - 1
                '        If IsBusy Then
                '            ExecuteSqlProcedure("Delete from Event_Capture where Event_ID in (" & _
                '             "14, 15) and Accession_ID = " & dt.Rows(i).Item("AccID"))
                '            '
                '            PB.Value = (i + 1) * 100 / Recs
                '            lblStatus.Text = (i + 1).ToString & " of " & Recs.ToString
                '            My.Application.DoEvents()
                '            'BW.ReportProgress((i + 1) * 100 / Recs, (i + 1).ToString & " of " & Recs.ToString)
                '        End If
                '    Next
                'End If
            ElseIf Medium = 4 Then    'Prolison
                'If IsConfigControl = False Then   'independent
                '    For i As Integer = 0 To Recs - 1
                '        If IsBusy Then
                '            ExecuteSqlProcedure("Delete from Event_Capture where Event_ID = 16 " & _
                '            "and Accession_ID = " & dt.Rows(i).Item("AccID"))
                '            '
                '            PB.Value = (i + 1) * 100 / Recs
                '            lblStatus.Text = (i + 1).ToString & " of " & Recs.ToString
                '            My.Application.DoEvents()
                '            'BW.ReportProgress((i + 1) * 100 / Recs, (i + 1).ToString & " of " & Recs.ToString)
                '        End If
                '    Next
                'End If
            Else    'Email
                'If IsConfigControl = False Then   'independent
                '    For i As Integer = 0 To Recs - 1
                '        If IsBusy Then
                '            ExecuteSqlProcedure("Delete from Event_Capture where Event_ID = 13 " & _
                '            "and Accession_ID = " & dt.Rows(i).Item("AccID"))
                '            '
                '            PB.Value = (i + 1) * 100 / Recs
                '            lblStatus.Text = (i + 1).ToString & " of " & Recs.ToString
                '            My.Application.DoEvents()
                '            'BW.ReportProgress((i + 1) * 100 / Recs, (i + 1).ToString & " of " & Recs.ToString)
                '        End If
                '    Next
                'End If
            End If
        End If
        'If Me.InvokeRequired = True Then
        '    Me.Invoke(New ClearForm(AddressOf FormClear))
        'Else
        '    FormClear()
        'End If
    End Sub

    Private Sub PrintPDFReport(ByVal RPTFile As String, ByVal AccID As String, Optional ByVal SaveTo As String = "")
        Try
            Dim SPDFS As New List(Of Byte())
            Dim FPDF As Byte() = Nothing
            Dim ExtCount As Integer = 0

            'TODO : Crystal Report
            '===============================================
            'If AccID <> "" Then
            '    Dim FolderPath As String = GetTempFolder()
            '    '
            '    Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            '    oRpt.Load(RPTFile)
            '    ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database,
            '    My.Settings.UID, My.Settings.PWD)
            '    oRpt.RecordSelectionFormula = "{Requisitions.ID} = " & AccID & " AND {Acc_Results.Result} <> '.'"


            '    'Dim S As MemoryStream = oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat)
            '    'SPDFS.Add(S.ToArray)


            '    Using S As New MemoryStream()
            '        Try
            '            oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat).CopyTo(S)
            '            S.Position = 0
            '            SPDFS.Add(S.ToArray())
            '        Catch ex As Exception
            '            Dim err As String = ex.Message

            '        End Try
            '    End Using

            '    Dim cnex As New SqlConnection(connString)
            '    cnex.Open()
            '    Dim cmdex As New SqlCommand("Select Result from Extend_Results where Accession_ID = " & AccID, cnex)
            '    cmdex.CommandType = CommandType.Text
            '    Dim drex As SqlDataReader = cmdex.ExecuteReader
            '    If drex.HasRows Then
            '        While drex.Read
            '            SPDFS.Add(drex("Result"))
            '            ExtCount += 1
            '        End While
            '    End If
            '    cnex.Close()
            '    cnex = Nothing
            '    oRpt.Close()
            '    oRpt = Nothing
            '    '
            '    FPDF = PdfHelper.MergePDFStreams(SPDFS)

            '    'For Each FlToDel As String In Directory.GetFiles(FolderPath, "*.*", SearchOption.AllDirectories)
            '    '    File.Delete(FlToDel)
            '    'Next
            '    '
            '    Dim fileName As String = FolderPath & AccID & ".PDF"
            '    Dim ms As New FileStream(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Read)
            '    ms.Write(FPDF, 0, FPDF.Length)
            '    ms.Close()
            '    ms = Nothing


            '    If SaveTo = "" Then

            '        'Dim PDFDOC As New Spire.Pdf.PdfDocument
            '        'PDFDOC.LoadFromFile(FolderPath & AccID & ".PDF")


            '        'PDFDOC.PrintSettings.PrinterName = GetDefaultPrinter()
            '        'PDFDOC.Print()

            '        Dim printerName As String = "" ' Optional: Specify printer name
            '        Dim errorMessage As String = ""

            '        Dim success As Boolean = PdfHelper.PrintPdfDocument(fileName, printerName, errorMessage)

            '        If success Then
            '            'MessageBox.Show("PDF printing initiated successfully.")
            '        Else
            '            MessageBox.Show("Error printing PDF: " & errorMessage)
            '        End If


            '    Else
            '        Try
            '            FileCopy(fileName, SaveTo & ".PDF")

            '        Catch ex As Exception
            '            MsgBox(ex.Message)
            '        End Try

            '    End If

            'End If
            '===============================================
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub cmbDestination_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDestination.SelectedIndexChanged
        If cmbDestination.SelectedIndex = 0 Then
            txtQty.Enabled = True
        Else
            txtQty.Enabled = False
        End If
        If cmbDestination.SelectedIndex = 6 Then
            Dim folderBrowserDialog1 As New FolderBrowserDialog()

            ' Set the initial directory (optional)
            folderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)

            ' Show the dialog and check if the user clicked OK
            If folderBrowserDialog1.ShowDialog() = DialogResult.OK Then
                ' Get the selected folder path
                Dim selectedFolderPath As String = folderBrowserDialog1.SelectedPath
                ReportsFolder = selectedFolderPath
            End If
        End If
    End Sub

    Private Sub BW_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BW.DoWork
        ProcessSQL(e.Argument)
    End Sub

    Private Sub BW_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BW.ProgressChanged
        PB.Value = e.ProgressPercentage
        lblStatus.Text = e.UserState
    End Sub

    Private Sub BW_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BW.RunWorkerCompleted
        'If Me.InvokeRequired = True Then
        '    Me.Invoke(New ClearForm(AddressOf FormClear))
        'Else

        'End If
        IsBusy = False
        If BW.CancellationPending = True Then Me.Close()
    End Sub
    Private Sub PrintMultiReports(ByVal RPTFile As String, ByVal AccIDs() As String, ByVal Device As Integer)
        Dim S As New MemoryStream
        'Try
        Dim AccID As String = ""
        Dim SPDFS As New List(Of Byte())
        Dim FPDF As Byte() = Nothing
        Dim ExtCount As Integer = 0
        'TODO : Crystal Report
        '===============================================
        'If AccIDs(0) <> "" Then
        '    Dim FolderPath As String = My.Application.Info.DirectoryPath & "\Temp\" & ThisUser.ID.ToString & "\"
        '    If Not Directory.Exists(FolderPath) Then
        '        Directory.CreateDirectory(FolderPath)
        '        Dim UserAccount As String = "everyone" 'Specify the user here
        '        Dim FolderInfo As DirectoryInfo = New DirectoryInfo(FolderPath)
        '        Dim FolderAcl As New DirectorySecurity
        '        FolderAcl.AddAccessRule(New FileSystemAccessRule(UserAccount,
        '        FileSystemRights.Modify, InheritanceFlags.ContainerInherit Or
        '        InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow))
        '        FolderAcl.SetAccessRuleProtection(True, False) 'uncomment to remove existing permissions
        '        FolderInfo.SetAccessControl(FolderAcl)
        '    End If
        '    For i As Integer = 0 To AccIDs.Length - 1
        '        If AccIDs(i) <> "" Then
        '            AccID = AccIDs(i)
        '            Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        '            oRpt.Load(RPTFile, CrystalDecisions.Shared.OpenReportMethod.OpenReportByTempCopy)
        '            ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database,
        '            My.Settings.UID, My.Settings.PWD)
        '            oRpt.RecordSelectionFormula = "{Requisitions.ID} = " & AccIDs(i) & " AND {Acc_Results.Result} <> '.'"
        '            Try
        '                S = oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
        '                                        )
        '                SPDFS.Add(S.ToArray)
        '                S.Close()
        '                S = Nothing
        '            Catch ex As Exception
        '                Dim err As String = ex.Message()
        '            End Try

        '            '
        '            Dim cner As New SqlConnection(connString)
        '            cner.Open()
        '            Dim cmder As New SqlCommand("Select Result from Extend_Results where Accession_ID = " & AccID, cner)
        '            cmder.CommandType = CommandType.Text
        '            Dim drer As SqlDataReader = cmder.ExecuteReader
        '            If drer.HasRows Then
        '                While drer.Read
        '                    SPDFS.Add(drer("Result"))
        '                    ExtCount += 1
        '                End While
        '            End If
        '            cner.Close()
        '            cner = Nothing
        '            '
        '            oRpt.Close()
        '            oRpt = Nothing
        '        End If
        '    Next
        '    FPDF = PdfHelper.MergePDFStreams(SPDFS)
        '    '
        '    For Each FlToDel As String In Directory.GetFiles(FolderPath, "*.*", SearchOption.TopDirectoryOnly)
        '        File.Delete(FlToDel)
        '    Next
        '    '
        '    Dim ms As New FileStream(FolderPath & AccID & ".PDF", FileMode.Create, FileAccess.ReadWrite, FileShare.Delete)
        '    ms.Write(FPDF, 0, FPDF.Length)
        '    ms.Close()
        '    ms = Nothing
        '    '
        '    If Device = 0 Then  'printer
        '        'Dim PDFDOC As New Spire.Pdf.PdfDocument
        '        'PDFDOC.LoadFromFile(FolderPath & AccID & ".PDF")
        '        'Dim PS As New System.Drawing.Printing.PrinterSettings
        '        'PDFDOC.PrintSettings.PrinterName = PS.PrinterName
        '        'PDFDOC.Print()
        '        'My.Application.DoEvents()
        '    ElseIf Device = 1 Then  'screen
        '        Try
        '            'If PDFRV_foxit.IsHandleCreated Then PDFRV_foxit.Close()
        '            'PDFRV_foxit.PdfViewer1.Refresh()
        '            'PDFRV_foxit.PdfViewer1.Open(New Foxit.PDF.Viewer.PdfDocument(FPDF))
        '            ''Temuree
        '            ''If PDFRV.IsHandleCreated Then PDFRV.Close()
        '            ''PDFRV.AxAcroPDF1.src = FolderPath & AccID & ".PDF"
        '            'PDFRV_foxit.FilePath = FolderPath & AccID & ".PDF"

        '            'PDFRV_foxit.Show()
        '        Catch ex As Exception
        '            MsgBox(ex.Message, MsgBoxStyle.Critical, "Prolis")
        '        End Try

        '    End If
        'End If
        '===============================================
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical, "Prolis")
        'Finally
        '    If oRpt IsNot Nothing Then
        '        oRpt.Close()
        '        oRpt = Nothing
        '    End If
        '    If S IsNot Nothing Then
        '        S.Close()
        '        S = Nothing
        '    End If
        'End Try
    End Sub


End Class
