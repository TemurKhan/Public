Imports System.Windows.Forms
Imports System.Data.SqlClient

Public Class frmResultDash
    Private origWidth As Integer
    Private origHeight As Integer

    Private Sub frmResultDash_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MaximumSize = MaxSize
        cmbCriteria.SelectedIndex = -1
        lblFrom.Text += " (" & SystemConfig.DateFormat & ")"
        lblTo.Text += " (" & SystemConfig.DateFormat & ")"
        dtpFrom.Format = DateTimePickerFormat.Custom
        dtpFrom.CustomFormat = SystemConfig.DateFormat
        dtpFrom.Value = DateAdd(DateInterval.Day, -1, Date.Today)
        dtpTo.Format = DateTimePickerFormat.Custom
        dtpTo.CustomFormat = SystemConfig.DateFormat
        dtpTo.Value = Date.Now
        btnGo_Click(Nothing, Nothing)
        '
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub DisplayResults()   '0=Acc, 1=Coll, 2=Rec
        dgvTests.Rows.Clear()
        Dim Criteria As String = ""
        If cmbCriteria.SelectedIndex = 0 AndAlso Trim(txtTerm.Text) <> "" And IsNumeric(Trim(txtTerm.Text)) Then
            If txtTerm.Text.Contains("-") Then
                Dim acc = txtTerm.Text.Split("-")(0)
                txtTerm.Text = acc
            End If
            Criteria = " Received <> 0 and ID = " & Trim(txtTerm.Text)
        Else
            Criteria = " Received <> 0 and ReceivedTime between '" & Format(dtpFrom.Value, _
            SystemConfig.DateFormat) & "' and '" & Format(dtpTo.Value, SystemConfig.DateFormat) & " 23:59:00'"
        End If
        If cmbCriteria.SelectedIndex = 1 Then   'AttRequiring
            Criteria += " and ID in (Select distinct Accession_ID from Acc_Results where Not(Result is NULL or Result = '' and Released <> 0)" & _
            " Union Select distinct Accession_ID from Acc_Info_Results where Not(Result is NULL or Result = '' and Released <> 0) Union " & _
            " Select distinct Accession_ID from Ref_Results where Not(Result is NULL or Result = '' and Released <> 0))"
        ElseIf cmbCriteria.SelectedIndex = 2 Then   'FalseFinal
            Criteria += " and ((not Reported_Final is NULL and Charindex('FINAL', RPT_Status) " & _
            "= 0) or (Reported_Final is NULL and Charindex('FINAL', RPT_Status) > 0))"
        ElseIf cmbCriteria.SelectedIndex = 3 Then   'Finals
            Criteria += " and Not Reported_Final is Null"
        ElseIf cmbCriteria.SelectedIndex = 4 Then   'Partials
            Criteria += " and Reported_Final is Null and (not ReportedOn is Null or RPT_Status = 'PARTIAL')"
        ElseIf cmbCriteria.SelectedIndex = 5 Then   'Outsourced
            Criteria += " and ID in (Select distinct Accession_ID from Sendouts)"
        ElseIf cmbCriteria.SelectedIndex = 6 Then   'Over aged
            'Criteria += " and ID in (Select distinct b.Accession_ID from Acc_Results b inner join Requisitions a on a.ID = b.Accession_ID " & _
            '"where b.Released = 0 and b.Test_ID in (Select c.Test_ID from Acc_Results c inner join Requisitions d on d.ID = c.Accession_ID " & _
            '"where Datediff(hh, a.ReceivedTime, getdate()) > Datediff(hh, d.ReceivedTime, c.Release_Time) and d.Received <> 0 and c.Released <> 0))"
            Criteria += " and ID in (Select distinct b.Accession_ID from Acc_Results b inner join Requisitions a on a.ID = b.Accession_ID " & _
            "where b.Released = 0 and b.Test_ID in (Select distinct c.Test_ID from Acc_Results c inner join Requisitions d on d.ID = c.Accession_ID " & _
            "where not d.Reported_Final is NULL and d.ReceivedTime between '" & DateAdd(DateInterval.Day, -2, dtpFrom.Value) & "' and '" & _
            Format(dtpFrom.Value, SystemConfig.DateFormat) & " 23:59:00' and Datediff(hh, a.ReceivedTime, getdate()) > Datediff(hh, d.ReceivedTime, " & _
            "c.Release_Time) and c.Released <> 0))"
        ElseIf cmbCriteria.SelectedIndex = 7 Then   'Un-resulted
            Criteria += " and Not ID in (Select Accession_ID from Acc_Results where Result <> '' Union Select Accession_ID " & _
            "from Acc_Info_Results where Result <> '' Union Select Accession_ID from Ref_Results where Result <> '')"
        End If
        txtInitials.Text = GetInitialsCount(Criteria)
        txtPartials.Text = GetPartialsCount(Criteria)
        txtAttReqs.Text = GetAttReqsCount(Criteria)
        txtFinals.Text = GetFinalsCount(Criteria)
        txtOutSrcd.Text = GetOSCount(Criteria)
        txtFalseFinals.Text = GetFalseFinalCount(Criteria)
        txtOverAged.Text = GetOverAgedCount(Criteria)
        '
        Dim Age As String = ""
        Dim Status As String = ""
        dgvAccessions.Rows.Clear()
        Dim sSQL As String = "Select ID as AccID, ReceivedTime as Received, RPT_Status as " & _
        "Status, Reported_Initial, ReportedOn, Reported_Final from Requisitions where " & Criteria
        Dim cndr As New SqlConnection(connString)
        cndr.Open()
        Dim cmddr As New SqlCommand(sSQL, cndr)
        cmddr.CommandTimeout = 180
        cmddr.CommandType = Data.CommandType.Text
        Dim drdr As SqlDataReader = cmddr.ExecuteReader
        If drdr.HasRows Then
            While drdr.Read
                If drdr("Status") IsNot DBNull.Value _
                AndAlso drdr("Status") <> "" Then
                    Status = drdr("Status")
                Else
                    If drdr("Reported_Final") IsNot DBNull.Value Then
                        Status = "FINAL"
                    ElseIf drdr("ReportedOn") IsNot DBNull.Value Then
                        Status = "PARTIAL"
                    ElseIf drdr("Reported_Initial") IsNot DBNull.Value Then
                        Status = "INITIAL"
                    Else
                        Status = "PENDING"
                    End If
                End If

                If InStr(Status, "FINAL") > 0 AndAlso drdr("Reported_Final") IsNot DBNull.Value Then
                    If DateDiff(DateInterval.Hour, drdr("Received"), drdr("Reported_Final")) >= 24 Then
                        Age = (DateDiff(DateInterval.Hour, drdr("Received"), drdr("Reported_Final")) \ 24).ToString _
                        & " D " & (DateDiff(DateInterval.Hour, drdr("Received"), drdr("Reported_Final")) Mod 24).ToString & " H"
                    ElseIf DateDiff(DateInterval.Hour, drdr("Received"), drdr("Reported_Final")) < 1 Then
                        Age = DateDiff(DateInterval.Minute, drdr("Received"), drdr("Reported_Final")).ToString & " M"
                    Else
                        Age = DateDiff(DateInterval.Hour, drdr("Received"), drdr("Reported_Final")).ToString & " H"
                    End If
                ElseIf Status = "PARTIAL" AndAlso drdr("ReportedOn") IsNot DBNull.Value Then
                    If DateDiff(DateInterval.Hour, drdr("Received"), drdr("ReportedOn")) >= 24 Then
                        Age = (DateDiff(DateInterval.Hour, drdr("Received"), drdr("ReportedOn")) \ 24).ToString _
                        & " D " & (DateDiff(DateInterval.Hour, drdr("Received"), drdr("ReportedOn")) Mod 24).ToString & " H"
                    ElseIf DateDiff(DateInterval.Hour, drdr("Received"), drdr("ReportedOn")) < 1 Then
                        Age = DateDiff(DateInterval.Minute, drdr("Received"), drdr("ReportedOn")).ToString & " M"
                    Else
                        Age = DateDiff(DateInterval.Hour, drdr("Received"), drdr("ReportedOn")).ToString & " H"
                    End If
                ElseIf Status = "INITIAL" AndAlso drdr("Reported_Initial") IsNot DBNull.Value Then
                    If DateDiff(DateInterval.Hour, drdr("Received"), drdr("Reported_Initial")) >= 24 Then
                        Age = (DateDiff(DateInterval.Hour, drdr("Received"), drdr("Reported_Initial")) \ 24).ToString _
                        & " D " & (DateDiff(DateInterval.Hour, drdr("Received"), drdr("Reported_Initial")) Mod 24).ToString & " H"
                    ElseIf DateDiff(DateInterval.Hour, drdr("Received"), drdr("Reported_Initial")) < 1 Then
                        Age = DateDiff(DateInterval.Minute, drdr("Received"), drdr("Reported_Initial")).ToString & " M"
                    Else
                        Age = DateDiff(DateInterval.Hour, drdr("Received"), drdr("Reported_Initial")).ToString & " H"
                    End If
                Else
                    If DateDiff(DateInterval.Hour, drdr("Received"), Date.Now) >= 24 Then
                        Age = (DateDiff(DateInterval.Hour, drdr("Received"), Date.Now) \ 24).ToString _
                        & " D " & (DateDiff(DateInterval.Hour, drdr("Received"), Date.Now) Mod 24).ToString & " H"
                    ElseIf DateDiff(DateInterval.Hour, drdr("Received"), Date.Now) < 1 Then
                        Age = DateDiff(DateInterval.Minute, drdr("Received"), Date.Now).ToString & " M"
                    Else
                        Age = DateDiff(DateInterval.Hour, drdr("Received"), Date.Now).ToString & " H"
                    End If
                End If
                dgvAccessions.Rows.Add(drdr("AccID"), Format(drdr("Received"),
                SystemConfig.DateFormat), Status, Age)
                Status = ""
            End While
        End If
        cndr.Close()
        cndr = Nothing
        txtTotal.Text = dgvAccessions.RowCount.ToString & " of " & GetTotalCount(Criteria)
        'If dgvAccessions.RowCount > 0 Then
        '    btnPrintSum.Enabled = True
        '    btnPrintDet.Enabled = True
        'End If
    End Sub

    Private Function GetOverAgedCount(ByVal Criteria As String) As Long
        Dim OACount As Long = 0
        Dim sSQL As String = "Select Count(ID) as OACount from Requisitions where ID in (Select distinct b.Accession_ID " &
        "from Acc_Results b inner join Requisitions a on a.ID = b.Accession_ID where b.Released = 0 and b.Test_ID in " &
        "(Select distinct c.Test_ID from Acc_Results c inner join Requisitions d on d.ID = c.Accession_ID where not " &
        "d.Reported_Final is NULL and d.ReceivedTime between '" & DateAdd(DateInterval.Day, -2, dtpFrom.Value) & "' and '" &
        Format(dtpFrom.Value, SystemConfig.DateFormat) & " 23:59:00' and Datediff(hh, a.ReceivedTime, getdate()) > Datediff(hh, " &
        "d.ReceivedTime, c.Release_Time) and c.Released <> 0)) and " & Criteria
        Dim cnoac As New SqlConnection(connString)
        cnoac.Open()
        Dim cmdoac As New SqlCommand(sSQL, cnoac)
        cmdoac.CommandTimeout = 180
        cmdoac.CommandType = Data.CommandType.Text
        Dim droac As SqlDataReader = cmdoac.ExecuteReader
        If droac.HasRows Then
            While droac.Read
                OACount = droac("OACount")
            End While
        End If
        cnoac.Close()
        cnoac = Nothing
        Return OACount
    End Function

    Private Function GetFalseFinalCount(ByVal Criteria As String) As Long
        Dim FFCount As Long = 0
        Dim sSQL As String = "Select Count(ID) as FFCount from Requisitions where ((not Reported_Final is NULL and " &
        "Charindex('FINAL', RPT_Status) = 0) or (Reported_Final is NULL and Charindex('FINAL', RPT_Status) > 0)) and " & Criteria
        Dim cnffc As New SqlConnection(connString)
        cnffc.Open()
        Dim cmdffc As New SqlCommand(sSQL, cnffc)
        cmdffc.CommandType = Data.CommandType.Text
        Dim drffc As SqlDataReader = cmdffc.ExecuteReader
        If drffc.HasRows Then
            While drffc.Read
                FFCount = drffc("FFCount")
            End While
        End If
        cnffc.Close()
        cnffc = Nothing
        Return FFCount
    End Function

    Private Function GetOSCount(ByVal Criteria As String) As Long
        Dim OSCount As Long = 0
        Dim sSQL As String = "Select Count(ID) as OSCount from Requisitions where ID in (Select distinct Accession_ID from Sendouts) and " & Criteria
        Dim cnosc As New SqlConnection(connString)
        cnosc.Open()
        Dim cmdosc As New SqlCommand(sSQL, cnosc)
        cmdosc.CommandType = Data.CommandType.Text
        Dim drosc As SqlDataReader = cmdosc.ExecuteReader
        If drosc.HasRows Then
            While drosc.Read
                OSCount = drosc("OSCount")
            End While
        End If
        cnosc.Close()
        cnosc = Nothing
        Return OSCount
    End Function

    Private Function GetTotalCount(ByVal Criteria As String) As Long
        Dim TotalCount As Long = 0
        Dim sSQL As String = "Select Count(ID) as TotalCount from Requisitions where " & Criteria
        Dim cngtc As New SqlConnection(connString)
        cngtc.Open()
        Dim cmdgtc As New SqlCommand(sSQL, cngtc)
        cmdgtc.CommandType = Data.CommandType.Text
        Dim drgtc As SqlDataReader = cmdgtc.ExecuteReader
        If drgtc.HasRows Then
            While drgtc.Read
                TotalCount = drgtc("TotalCount")
            End While
        End If
        cngtc.Close()
        cngtc = Nothing
        Return TotalCount
    End Function

    Private Function GetFinalsCount(ByVal Criteria As String) As Long
        Dim FinalCount As Long = 0
        Dim sSQL As String = "Select Count(ID) as FinalCount from Requisitions where Not Reported_Final is Null and " & Criteria
        Dim cngfc As New SqlConnection(connString)
        cngfc.Open()
        Dim cmdgfc As New SqlCommand(sSQL, cngfc)
        cmdgfc.CommandType = Data.CommandType.Text
        Dim drgfc As SqlDataReader = cmdgfc.ExecuteReader
        If drgfc.HasRows Then
            While drgfc.Read
                FinalCount = drgfc("FinalCount")
            End While
        End If
        cngfc.Close()
        cngfc = Nothing
        Return FinalCount
    End Function

    Private Function GetAttReqsCount(ByVal Criteria As String) As Long
        Dim AttCount As Long = 0
        Dim sSQL As String = "Select Count(ID) as AttCount from Requisitions where ID in (Select distinct Accession_ID " &
        "from Acc_Results where Not(Result is NULL or Result = '') and Released = 0 Union Select distinct Accession_ID " &
        "from Ref_Results where Not(Result is NULL or Result = '') and Released = 0 Union Select distinct Accession_ID " &
        "from Acc_Info_Results where Not(Result is NULL or Result = '') and Released = 0) and " & Criteria
        Dim cnarc As New SqlConnection(connString)
        cnarc.Open()
        Dim cmdarc As New SqlCommand(sSQL, cnarc)
        cmdarc.CommandType = Data.CommandType.Text
        Dim drarc As SqlDataReader = cmdarc.ExecuteReader
        If drarc.HasRows Then
            While drarc.Read
                AttCount = drarc("AttCount")
            End While
        End If
        cnarc.Close()
        cnarc = Nothing
        Return AttCount
    End Function

    Private Function GetPartialsCount(ByVal Criteria As String) As Long
        Dim PartCount As Long = 0
        Dim sSQL As String = "Select Count(ID) as PartCount from Requisitions where Reported_Final is " &
        "Null and (not ReportedOn is Null or RPT_Status = 'PARTIAL') and " & Criteria
        Dim cngpc As New SqlConnection(connString)
        cngpc.Open()
        Dim cmdgpc As New SqlCommand(sSQL, cngpc)
        cmdgpc.CommandType = Data.CommandType.Text
        Dim drgpc As SqlDataReader = cmdgpc.ExecuteReader
        If drgpc.HasRows Then
            While drgpc.Read
                PartCount = drgpc("PartCount")
            End While
        End If
        cngpc.Close()
        cngpc = Nothing
        Return PartCount
    End Function

    Private Function GetInitialsCount(ByVal Criteria As String) As Long
        Dim InitCount As Long = 0
        Dim sSQL As String = "Select Count(ID) as InitCount from Requisitions where (Reported_Final is Null and ReportedOn is Null) and " & Criteria
        Dim cngic As New SqlConnection(connString)
        cngic.Open()
        Dim cmdgic As New SqlCommand(sSQL, cngic)
        cmdgic.CommandType = Data.CommandType.Text
        Dim drgic As SqlDataReader = cmdgic.ExecuteReader
        If drgic.HasRows Then
            While drgic.Read
                InitCount = drgic("InitCount")
            End While
        End If
        cngic.Close()
        cngic = Nothing
        Return InitCount
    End Function

    Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        DisplayResults()
    End Sub

    Private Sub dgvAccessions_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAccessions.CellClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            DisplayAccResults(dgvAccessions.Rows(e.RowIndex).Cells(0).Value, dgvAccessions.Rows(e.RowIndex).Cells(2).Value)
        End If
    End Sub

    Private Sub DisplayAccResults(ByVal AccID As Long, ByVal Status As String)
        dgvTests.Rows.Clear()
        Dim Criteria As String = "Select ID from Requisitions where Received <> 0 and ReceivedTime between '" &
        Format(dtpFrom.Value, SystemConfig.DateFormat) & "' and '" & Format(dtpTo.Value, SystemConfig.DateFormat) & " 23:59:00'"
        If cmbCriteria.SelectedIndex = 0 Then   'AttRequiring
            Criteria += " and ID in (Select distinct Accession_ID from Acc_Results where Not(Result is NULL or Result = '' and Released <> 0)" &
            " Union Select distinct Accession_ID from Acc_Info_Results where Not(Result is NULL or Result = '' and Released <> 0) Union " &
            " Select distinct Accession_ID from Ref_Results where Not(Result is NULL or Result = '' and Released <> 0))"
        ElseIf cmbCriteria.SelectedIndex = 1 Then   'FalseFinal
            Criteria += " and ((not Reported_Final is NULL and Charindex('FINAL', RPT_Status) " &
            "= 0) or (Reported_Final is NULL and Charindex('FINAL', RPT_Status) > 0))"
        ElseIf cmbCriteria.SelectedIndex = 2 Then   'Finals
            Criteria += " and Not Reported_Final is Null"
        ElseIf cmbCriteria.SelectedIndex = 3 Then   'Partials
            Criteria += " and Reported_Final is Null and (not ReportedOn is Null or RPT_Status = 'PARTIAL')"
        ElseIf cmbCriteria.SelectedIndex = 4 Then   'Outsourced
            Criteria += " and ID in (Select distinct Accession_ID from Sendouts)"
        ElseIf cmbCriteria.SelectedIndex = 5 Then   'Over aged
            Criteria += " and ID in (Select distinct b.Accession_ID from Acc_Results b inner join Requisitions a on a.ID = b.Accession_ID " &
            "where b.Released = 0 and b.Test_ID in (Select c.Test_ID from Acc_Results c inner join Requisitions d on d.ID = c.Accession_ID " &
            "where Datediff(hh, a.ReceivedTime, getdate()) > Datediff(hh, d.ReceivedTime, c.Release_Time) and d.Received <> 0 and c.Released <> 0))"
        ElseIf cmbCriteria.SelectedIndex = 6 Then   'Un-resulted
            Criteria += " and Not ID in (Select Accession_ID from Acc_Results where Result <> '' Union Select Accession_ID " &
            "from Acc_Info_Results where Result <> '' Union Select Accession_ID from Ref_Results where Result <> '')"
        End If
        Dim sSQL As String = "Select a.Test_ID as TestID, b.Name as TestName, a.Result as Result, a.Flag as Flag, 'User' as TestType, " &
        "c.ReceivedTime as ReceivedTime, a.Release_Time as ReleasedTime from Tests b inner join(Requisitions c inner join Acc_Results " &
        "a on a.Accession_ID = c.ID) on a.Test_ID = b.ID where c.ID in (" & Criteria & ") and c.ID = " & AccID & " Union " &
        "Select d.Test_ID as TestID, e.Name as TestName, d.Result as Result, d.Flag as Flag, 'Reflex' as TestType, f.ReceivedTime as " &
        "ReceivedTime, d.Release_Time as ReleasedTime from Tests e inner join(Requisitions f inner join Ref_Results d on d.Accession_ID " &
        "= f.ID) on d.Test_ID = e.ID where f.ID in (" & Criteria & ") and f.ID = " & AccID & " Union " &
        "Select g.Info_ID as TestID, h.Name as TestName, g.Result as Result, g.Flag as Flag, 'Child' as TestType, i.ReceivedTime as " &
        "ReceivedTime, g.Release_Time as ReleasedTime from Tests h inner join(Requisitions i inner join Acc_Info_Results g on " &
        "g.Accession_ID = i.ID) on g.Info_ID = h.ID where i.ID in (" & Criteria & ") and i.ID = " & AccID & " order by TestType DESC"
        If connString <> "" Then
            Dim cnn As New SqlConnection(connString)
            cnn.Open()
            Dim cmdsel As New SqlCommand(sSQL, cnn)
            cmdsel.CommandTimeout = 120
            cmdsel.CommandType = Data.CommandType.Text
            Dim DR As SqlDataReader = cmdsel.ExecuteReader
            If DR.HasRows Then
                While DR.Read
                    Dim Age As String = ""
                    If DR("ReleasedTime") IsNot DBNull.Value Then
                        If DateDiff(DateInterval.Hour, DR("ReceivedTime"), DR("ReleasedTime")) >= 24 Then
                            Age = (DateDiff(DateInterval.Hour, DR("ReceivedTime"), DR("ReleasedTime")) \ 24).ToString _
                            & " D " & (DateDiff(DateInterval.Hour, DR("ReceivedTime"), DR("ReleasedTime")) Mod 24).ToString & " H"
                        ElseIf DateDiff(DateInterval.Hour, DR("ReceivedTime"), DR("ReleasedTime")) < 1 Then
                            Age = DateDiff(DateInterval.Minute, DR("ReceivedTime"), DR("ReleasedTime")).ToString & " M"
                        Else
                            Age = DateDiff(DateInterval.Hour, DR("ReceivedTime"), DR("ReleasedTime")).ToString & " H"
                        End If
                    Else
                        If DateDiff(DateInterval.Hour, DR("ReceivedTime"), Date.Now) >= 24 Then
                            Age = (DateDiff(DateInterval.Hour, DR("ReceivedTime"), Date.Now) \ 24).ToString _
                            & " D " & (DateDiff(DateInterval.Hour, DR("ReceivedTime"), Date.Now) Mod 24).ToString & " H"
                        ElseIf DateDiff(DateInterval.Hour, DR("ReceivedTime"), Date.Now) < 1 Then
                            Age = DateDiff(DateInterval.Minute, DR("ReceivedTime"), Date.Now).ToString & " M"
                        Else
                            Age = DateDiff(DateInterval.Hour, DR("ReceivedTime"), Date.Now).ToString & " H"
                        End If
                    End If
                    Dim Result As String = ""
                    Dim Flag As String = ""
                    If DR("Result") IsNot DBNull.Value Then Result = DR("Result")
                    If DR("Flag") IsNot DBNull.Value Then Flag = DR("Flag")
                    dgvTests.Rows.Add(DR("TestID"), DR("TestName"), Result, Flag, DR("TestType"), Age)
                End While
            End If
            cnn.Close()
            cnn = Nothing
            'Else
            '    Dim cnn As New Data.Odbc.OdbcConnection(odbCS)
            '    cnn.Open()
            '    Dim cmdsel As New Data.Odbc.OdbcCommand(sSQL, cnn)
            '    cmdsel.CommandTimeout = 120
            '    cmdsel.CommandType = Data.CommandType.Text
            '    Dim DR As Data.Odbc.OdbcDataReader = cmdsel.ExecuteReader
            '    If DR.HasRows Then
            '        While DR.Read
            '            Dim Age As String = ""
            '            If DR("ReleasedTime") IsNot DBNull.Value Then
            '                If DateDiff(DateInterval.Hour, DR("ReceivedTime"), DR("ReleasedTime")) >= 24 Then
            '                    Age = (DateDiff(DateInterval.Hour, DR("ReceivedTime"), DR("ReleasedTime")) \ 24).ToString _
            '                    & " D " & (DateDiff(DateInterval.Hour, DR("ReceivedTime"), DR("ReleasedTime")) Mod 24).ToString & " H"
            '                Else
            '                    Age = DateDiff(DateInterval.Hour, DR("ReceivedTime"), DR("ReleasedTime")).ToString & " H"
            '                End If
            '            Else
            '                If DateDiff(DateInterval.Hour, DR("ReceivedTime"), Date.Now) >= 24 Then
            '                    Age = (DateDiff(DateInterval.Hour, DR("ReceivedTime"), Date.Now) \ 24).ToString _
            '                    & " D " & (DateDiff(DateInterval.Hour, DR("ReceivedTime"), Date.Now) Mod 24).ToString & " H"
            '                Else
            '                    Age = DateDiff(DateInterval.Hour, DR("ReceivedTime"), Date.Now).ToString & " H"
            '                End If
            '            End If
            '            Dim Result As String = ""
            '            Dim Flag As String = ""
            '            If DR("Result") IsNot DBNull.Value Then Result = DR("Result")
            '            If DR("Flag") IsNot DBNull.Value Then Flag = DR("Flag")
            '            dgvTests.Rows.Add(DR("TestID"), DR("TestName"), Result, Flag, DR("TestType"), Age)
            '        End While
            '    End If
            '    cnn.Close()
            '    cnn = Nothing
        End If
    End Sub

    Private Sub dgvAccessions_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAccessions.CellDoubleClick
        Try
            Clipboard.SetText(dgvAccessions.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)

            'dgvAccessions.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Green
        Catch ex As Exception

        End Try
        Try
            Clipboard.SetText(dgvAccessions.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
            clipmsg.Text = dgvAccessions.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & " copied to clipboard"
            clipmsg.ForeColor = Color.Red
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim rr = dgvAccessions.SelectedRows(0).Cells(0).Value.ToString()

        Me.Cursor = Cursors.WaitCursor
        If rr = "" Then
            Return
        End If
        Dim RPTFile = ValidateReportFile(rr, False)

        ' Convert single AccID value to an array
        Dim accIDsArray() As String = {rr}
        GenerateReports(accIDsArray, 1, False)
        Me.Cursor = Cursors.Default
    End Sub
End Class
