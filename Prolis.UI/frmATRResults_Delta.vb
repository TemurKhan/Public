Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Data.SqlClient
Imports System.Text

Public Class frmATRResults_Delta
    'Private mRs As New ADODB.Recordset
    Private dtRecords As New DataTable
    'Private connString As String = SystemConfig.ConnectionString
    Private Extras(,) As String
    Private Override As Integer
    Private Refluxes As String
    Private Recs As Integer
    Private Rec As Integer
    Public Comment As String
    Public TestID As Integer
    Private Decs As String
    Public TriggerID As Integer
    Private ResultsAT(,) As String
    Private OldResults(,) As String
    Private NewResults(,) As String
    Private IsDirty As Boolean
    Dim PanicStyle As New DataGridViewCellStyle
    Dim AbnormalStyle As New DataGridViewCellStyle

    Public Property ResultComment()
        Get
            Return Comment
        End Get
        Set(ByVal value)
            Comment = value
        End Set
    End Property

    Private Sub frmATRResults_Delta_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'If mRs.State = 1 Then mRs.Close()
        'mRs = Nothing
    End Sub

    Private Sub frmATRResults_Delta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If frmResults.IsHandleCreated Then frmResults.Close()
        PanicStyle.ForeColor = Color.White
        PanicStyle.BackColor = Color.Red
        AbnormalStyle.BackColor = Color.Yellow
        lblFrom.Text += " (" & SystemConfig.DateFormat & ")"
        lblTo.Text += " (" & SystemConfig.DateFormat & ")"
        dtpFrom.Format = DateTimePickerFormat.Custom
        dtpFrom.CustomFormat = SystemConfig.DateFormat
        dtpTo.Format = DateTimePickerFormat.Custom
        dtpTo.CustomFormat = SystemConfig.DateFormat
        dtpFrom.Value = DateAdd(DateInterval.Day, -30, Date.Today)
        dtpTo.Value = Date.Today
        dgvExceptions.RowCount = 1
        PopulateWorksheets()
        PopulateDirectors()
        LoadAccessions(dtpFrom.Value, dtpTo.Value, "")
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub PopulateWorksheets()
        cmbWID.Items.Clear()
        Dim sSQL As String = "Select * from Worksheets"
        If connString <> "" Then
            Dim cnpw As New SqlClient.SqlConnection(connString)
            cnpw.Open()
            Dim cmdpw As New SqlClient.SqlCommand(sSQL, cnpw)
            cmdpw.CommandType = CommandType.Text
            Dim drpw As SqlClient.SqlDataReader = cmdpw.ExecuteReader
            If drpw.HasRows Then
                While drpw.Read
                    cmbWID.Items.Add(New MyList(drpw("Name"), drpw("ID")))
                End While
            End If
            cnpw.Close()
            cnpw = Nothing
        End If
    End Sub

    Private Sub PopulateDirectors()
        cmbDirector.Items.Clear()
        Dim Degree As String = ""
        Dim cnpd As New SqlClient.SqlConnection(connString)
        cnpd.Open()
        Dim cmdpd As New SqlClient.SqlCommand("Select * from Lab_Directors where Company_ID = " & MyLab.ID &
        " and (EffectiveTo is NULL or EffectiveTo > '" & Date.Now.ToString & "') order by IsDefault DESC", cnpd)
        cmdpd.CommandType = CommandType.Text
        Dim drpd As SqlClient.SqlDataReader = cmdpd.ExecuteReader
        If drpd.HasRows Then
            While drpd.Read
                If drpd("Degree") IsNot DBNull.Value _
                AndAlso drpd("Degree") <> "" Then
                    Degree = drpd("Degree")
                Else
                    Degree = ""
                End If
                cmbDirector.Items.Add(New MyList(drpd("LastName") &
                ", " & drpd("FirstName") & IIf(Degree = "", "",
                " " & Degree), drpd("ID")))
            End While
        End If
        cnpd.Close()
        cnpd = Nothing
        '
        If cmbDirector.Items.Count > 0 Then cmbDirector.SelectedIndex = 0
    End Sub


    'Private Sub LoadAccessions(ByVal DateFrom As Date, ByVal DateTo As Date, ByVal WID As String)
    '    'Dim DF As Date = CDate(Format(DateFrom, SystemConfig.DateFormat) & " 00:00:00")
    '    'Dim DT As Date = CDate(Format(DateTo, SystemConfig.DateFormat) & " 23:59:00")
    '    Dim CNM As New ADODB.Connection
    '    CNM.Open(odbCS)
    '    If mRs.State = 1 Then mRs.Close()
    '    Dim sSQL As String = ""
    '    If WID = "" Then
    '        sSQL = "Select * from Requisitions where Received <> 0 and Rejected = 0 and AccessionDate between '" & Format(DateFrom,
    '        SystemConfig.DateFormat) & "' and '" & Format(DateTo, SystemConfig.DateFormat) & " 23:59:00' and ID in (Select Distinct " &
    '        "Accession_ID from Acc_Results where Result <> '' and (Released is Null or Released = 0) Union Select Distinct " &
    '        "Accession_ID from Ref_Results where Result <> '' and (Released is Null or Released = 0) Union Select Distinct " &
    '        "Accession_ID from Acc_Info_Results where Result <> '' and (Released is Null or Released = 0))"
    '    Else    'WID supplied
    '        sSQL = "Select * from Requisitions where Received <> 0 and Rejected = 0 and AccessionDate between '" & Format(DateFrom,
    '        SystemConfig.DateFormat) & "' and '" & Format(DateTo, SystemConfig.DateFormat) & " 23:59:00' and ID in (Select " &
    '        "Distinct a.Accession_ID from Acc_Results a inner join Worksheet_Test b on b.Test_ID = a.Test_ID where " &
    '        "b.Worksheet_ID = " & WID & " and a.Result <> '' and (a.Released is Null or a.Released = 0) Union Select " &
    '        "Distinct c.Accession_ID from Ref_Results c inner join Worksheet_Test d on d.Test_ID = c.Test_ID where " &
    '        "d.Worksheet_ID = " & WID & " and c.Result <> '' and (c.Released is Null or c.Released = 0) Union Select " &
    '        "Distinct e.Accession_ID from Acc_Info_Results e inner join Worksheet_Test f on f.Test_ID = e.Test_ID where " &
    '        "f.Worksheet_ID = " & WID & " and e.Result <> '' and (e.Released is Null or e.Released = 0))"
    '    End If
    '    '
    '    mRs.Open(sSQL, CNM, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Not mRs.BOF Then
    '        mRs.MoveLast()
    '        mRs.MoveFirst()
    '        Rec = 1
    '        Recs = mRs.RecordCount
    '        lblPos.Text = Rec.ToString & " of " & Recs.ToString
    '        DisplayAccession(mRs.Fields("ID").Value, WID)
    '        If Recs = 1 Then    'Only one record
    '            btnFirst.Enabled = False
    '            btnPrevious.Enabled = False
    '            btnNext.Enabled = False
    '            btnLast.Enabled = False
    '        Else                'Multiple
    '            btnFirst.Enabled = False
    '            btnPrevious.Enabled = False
    '            btnNext.Enabled = True
    '            btnLast.Enabled = True
    '        End If
    '    Else
    '        lblPos.Text = ""
    '        txtAccID.Text = ""
    '        txtAccDate.Text = ""
    '        txtProvider.Text = ""
    '        txtPatient.Text = ""
    '        dgvResults.Rows.Clear()
    '        btnFirst.Enabled = False
    '        btnPrevious.Enabled = False
    '        btnNext.Enabled = False
    '        btnLast.Enabled = False
    '    End If
    'End Sub

    Private Sub LoadAccessions(ByVal DateFrom As Date, ByVal DateTo As Date, ByVal WID As String)
        dgvResults.Rows.Clear()
        lblPos.Text = ""

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim dtRecords As New DataTable()
            Dim sSQL As New StringBuilder("SELECT * FROM Requisitions WHERE Received <> 0 AND Rejected = 0 AND AccessionDate BETWEEN @DateFrom AND @DateTo")

            If String.IsNullOrEmpty(WID) Then
                sSQL.Append(" AND ID IN (SELECT DISTINCT Accession_ID FROM Acc_Results WHERE Result <> '' AND (Released IS NULL OR Released = 0) 
                         UNION SELECT DISTINCT Accession_ID FROM Ref_Results WHERE Result <> '' AND (Released IS NULL OR Released = 0) 
                         UNION SELECT DISTINCT Accession_ID FROM Acc_Info_Results WHERE Result <> '' AND (Released IS NULL OR Released = 0))")
            Else
                sSQL.Append(" AND ID IN (SELECT DISTINCT a.Accession_ID FROM Acc_Results a 
                         INNER JOIN Worksheet_Test b ON b.Test_ID = a.Test_ID WHERE b.Worksheet_ID = @WID AND a.Result <> '' AND (a.Released IS NULL OR a.Released = 0)
                         UNION SELECT DISTINCT c.Accession_ID FROM Ref_Results c 
                         INNER JOIN Worksheet_Test d ON d.Test_ID = c.Test_ID WHERE d.Worksheet_ID = @WID AND c.Result <> '' AND (c.Released IS NULL OR c.Released = 0)
                         UNION SELECT DISTINCT e.Accession_ID FROM Acc_Info_Results e 
                         INNER JOIN Worksheet_Test f ON f.Test_ID = e.Test_ID WHERE f.Worksheet_ID = @WID AND e.Result <> '' AND (e.Released IS NULL OR e.Released = 0))")
            End If

            Using command As New SqlCommand(sSQL.ToString(), connection)
                command.Parameters.AddWithValue("@DateFrom", DateFrom.Date)
                command.Parameters.AddWithValue("@DateTo", DateTo.Date.AddHours(23).AddMinutes(59))
                If Not String.IsNullOrEmpty(WID) Then
                    command.Parameters.AddWithValue("@WID", WID)
                End If

                Using adapter As New SqlDataAdapter(command)
                    adapter.Fill(dtRecords)
                End Using
            End Using
        End Using

        If dtRecords.Rows.Count > 0 Then
            Dim recCount As Integer = dtRecords.Rows.Count
            lblPos.Text = $"1 of {recCount}"
            DisplayAccession(dtRecords.Rows(0)("ID").ToString(), WID)

            btnFirst.Enabled = False
            btnPrevious.Enabled = False
            btnNext.Enabled = recCount > 1
            btnLast.Enabled = recCount > 1
        Else
            ClearDisplay()
        End If
    End Sub

    Private Sub ClearDisplay()
        lblPos.Text = ""
        txtAccID.Text = ""
        txtAccDate.Text = ""
        txtProvider.Text = ""
        txtPatient.Text = ""
        dgvResults.Rows.Clear()
        btnFirst.Enabled = False
        btnPrevious.Enabled = False
        btnNext.Enabled = False
        btnLast.Enabled = False
    End Sub

    Private Sub ClearForm()
        txtAccID.Text = ""
        txtMeds.Text = ""
        txtAccDate.Text = ""
        txtProvider.Text = ""
        txtPatient.Text = ""
        dgvResults.Rows.Clear()
        dgvExceptions.Rows.Clear()
        dgvExceptions.RowCount = 1
        chkRelease.Checked = False
        txtComment.Text = ""
        txtRptDate.Text = ""
        txtRptTime.Text = ""
    End Sub

    Private Sub DisplayAccession(ByVal AccID As Long, ByVal WID As String)
        'Try
        Dim UserInfo(2, 0) As String
        Dim InHouse As Boolean = True
        Dim sFlag() As String = {"", ""}
        Dim sResult As String = ""
        Dim RTFNote As String = ""
        Dim Released As Boolean = False
        Dim ReleasedBy As Long
        Dim ReleasedOn As Date
        'Dim Infoc As Integer
        'Dim RefC As Integer
        Dim sNote As String = ""
        Dim i As Integer
        'Dim n As Integer
        Dim Cause() As String = {"", "", ""}
        Dim RptComp As Boolean = ReportFullResulted(AccID)
        '
        ClearForm()
        '
        ' Populate accession details
        txtAccID.Text = AccID.ToString()
        txtMeds.Text = GetMedications(AccID.ToString())

        ' Retrieve Accession Data from `dtRecords`
        Dim row As DataRow = dtRecords.AsEnumerable().FirstOrDefault(Function(r) r.Field(Of Long)("ID") = AccID)
        If row IsNot Nothing Then
            txtAccDate.Text = Format(row("AccessionDate"), SystemConfig.DateFormat)
            txtProvider.Text = GetProviderName(row("OrderingProvider_ID"))
        End If

        ' Display patient details
        DisplayPatient(AccID)
        Dim NormalRange As String = ""
        Dim sSQL As String = ""
        If WID = "" Then
            sSQL = "Select a.Accession_ID, a.Test_ID, a.Result, a.Flag, a.Behavior, a.NormalRange, a.Comment, a.T_Result, " &
            "a.Released, a.Released_By, a.Release_Time, b.* from Acc_Results a, Tests b, Requisitions c where a.Test_ID = " &
            "b.ID and a.Accession_ID = c.ID and c.Received <> 0 and b.IsActive <> 0 and b.HasResult <> 0 and a.Accession_ID " &
            "= " & AccID & " and a.Result <> '' and (a.Released is NULL or a.Released = 0)"
            '" Union Select a.Accession_ID, a.Test_ID, a.Result, a.Flag, a.Behavior, a.NormalRange, a.Comment, a.T_Result, a.Released, " & _
            '"a.Released_By, a.Release_Time, 'REF' as Cause0, a.Reflexer_ID as Cause1, a.Reflexed_ID as Cause2, b.* from Ref_Results a, " & _
            '"Tests b, Requisitions c where a.Test_ID = b.ID and a.Accession_ID = c.ID and c.Received <> 0 and b.IsActive <> 0 and " & _
            '"b.HasResult <> 0 and a.Accession_ID = " & AccID & " and a.Result <> '' and (a.Released is NULL or a.Released = 0)"
            '"Union(" & _")
            '"Select a.Accession_ID, a.Info_ID as Test_ID, a.Result, a.Flag, a.Behavior, a.NormalRange, a.Comment, a.T_Result, " & _
            '"a.Released, a.Released_By, a.Release_Time, 'Info' as Cause0, a.Test_ID as Cause1, a.Info_ID as Cause2, b.* from " & _
            '"Acc_Info_Results a, Tests b, Requisitions c where a.Info_ID = b.ID and a.Accession_ID = c.ID and c.Received <> 0 and " & _
            '"b.IsActive <> 0 and b.HasResult <> 0 and a.Accession_ID = " & AccID & " and a.Result <> '' and (a.Released is NULL or a.Released = 0)"
        Else
            sSQL = "Select a.Accession_ID, a.Test_ID, a.Result, a.Flag, a.Behavior, a.NormalRange, a.Comment, a.T_Result, a.Released, " &
            "a.Released_By, a.Release_Time, b.* from Acc_Results a, Tests b, Requisitions c, Worksheet_Test d where a.Test_ID = b.ID " &
            "and a.Test_ID = d.Test_ID and a.Accession_ID = c.ID and c.Received <> 0 and b.IsActive <> 0 and b.HasResult <> 0 and " &
            "a.Accession_ID = " & AccID & " and d.Worksheet_ID = " & WID & " and a.Result <> '' and (a.Released is NULL or a.Released = 0)"
        End If
        Dim cnda As New SqlClient.SqlConnection(connString)
        cnda.Open()
        Dim cmdda As New SqlClient.SqlCommand(sSQL, cnda)
        cmdda.CommandType = CommandType.Text
        Dim drda As SqlClient.SqlDataReader = cmdda.ExecuteReader
        If drda.HasRows Then
            While drda.Read
                InHouse = IsInHouseTest(AccID, drda("Test_ID"))
                NormalRange = ""
                sFlag(0) = ""
                If drda("NormalRange") IsNot DBNull.Value _
                AndAlso drda("NormalRange") <> "" Then
                    NormalRange = Trim(drda("NormalRange"))
                Else
                    NormalRange = ""
                End If
                If drda("Flag") IsNot DBNull.Value _
                AndAlso drda("Flag") <> "" Then
                    sFlag(0) = Trim(drda("Flag"))
                Else
                    sFlag(0) = ""
                End If
                If drda("Behavior") IsNot DBNull.Value _
                AndAlso drda("Behavior") <> "" Then
                    sFlag(1) = Trim(drda("Behavior"))
                Else
                    If (drda("Flag") = "LP" Or drda("Flag") = "HP" Or
                    drda("Flag").ToString.Contains("Panic") Or
                    drda("Flag").ToString.Contains("Critic")) Then
                        sFlag(1) = "Panic"
                    ElseIf (drda("Flag").ToString.StartsWith("L") Or drda("Flag").ToString.StartsWith("H") Or
                    drda("Flag").ToString.StartsWith("P") Or drda("Flag").ToString.StartsWith("R") Or
                    drda("Flag").ToString.StartsWith("Inc") Or drda("Flag").ToString.StartsWith("Not")) Then
                        sFlag(1) = "Caution"
                    Else
                        sFlag(1) = "Ignore"
                    End If
                End If
                '
                If drda("Result") IsNot DBNull.Value Then
                    If drda("Qualitative") = False Then  'QN
                        If IsNumeric(drda("Result")) = True Then
                            If InHouse Then
                                sResult = CDbl(drda("Result")).ToString
                            Else
                                sResult = Trim(drda("Result"))
                            End If
                        Else
                            sResult = Trim(drda("Result"))
                        End If
                    Else
                        sResult = Trim(drda("Result"))
                    End If
                Else
                    sResult = ""
                End If
                '
                If drda("Comment") IsNot DBNull.Value Then
                    sNote = drda("Comment")
                Else
                    sNote = ""
                End If
                If drda("T_Result") IsNot DBNull.Value Then
                    RTFNote = drda("T_Result")
                Else
                    RTFNote = ""
                End If
                '
                If drda("Released") Is DBNull.Value _
                OrElse drda("Released") = False Then
                    Released = False
                    ReleasedOn = Nothing
                    ReleasedBy = Nothing
                End If
                Cause(0) = "ACC" : Cause(1) = "" : Cause(2) = ""
                '
                dgvResults.Rows.Add(drda("Test_ID"), drda("Name"), sResult, Nothing, sFlag(0),
                NormalRange, Nothing, Nothing, Nothing, Nothing, Released, drda("Qualitative"),
                AccID, Cause(0), Cause(1), Cause(2), sNote, RTFNote, Nothing, False, sFlag(1))
                '0=TID, 1=TName, 2=Result, 3=nothing, 4=Flag, 5=NormalRange, 6=nothing, 7=nothing, 8=nothing, 9=nothing, 
                '10=Release, 11=Qualitative, 12=AccID, 13=Cause0, 14=Cause1, 15=Cause2, 16=Cmnt, 17=RTF, 18=IMG, 19=Sig

                dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).Style.BackColor = Color.White
                '
                If sFlag(1) = "Panic" Then
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(4).Style = PanicStyle
                ElseIf sFlag(1) = "Caution" Then
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(4).Style = AbnormalStyle
                End If
                '
                If drda("Qualitative") = True Then
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(3).Value =
                    System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath _
                    & "\Images\Looks.ico")
                    '
                    If DeltaCheck(dgvResults.Rows(dgvResults.RowCount - 1).Cells(12).Value,
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(0).Value,
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).Value) = False Then
                        If Not dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Value.ToString.StartsWith(ChrW(8710)) _
                        Then dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Value = ChrW(8710) & " " &
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Value
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(4).Style.BackColor =
                        Color.Red
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Style.ForeColor = Color.Red
                        If InStr(dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value,
                        ChrW(8710) & " Delta Check failed" & vbCrLf) = 0 Then _
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value =
                        ChrW(8710) & " Delta Check failed" & vbCrLf &
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value
                    Else
                        If InStr(dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Value.ToString,
                        ChrW(8710)) > 0 Then dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Value =
                        Replace(dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Value, ChrW(8710) & " ", "")
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(4).Style.BackColor =
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Style.BackColor
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Style.ForeColor = Color.Black
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value =
                        Replace(dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value,
                        ChrW(8710) & " Delta Check failed" & vbCrLf, "")
                    End If
                End If
                '
                If RptComp Then     'Report Complete
                    If ThisUser.Supervisor Or ThisUser.Director Then
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).ReadOnly = False
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(6).ReadOnly = False
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).ReadOnly = False
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(9).ReadOnly = False
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(10).ReadOnly = False
                    Else
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).ReadOnly = True
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(6).ReadOnly = True
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).ReadOnly = True
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(9).ReadOnly = True
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(10).ReadOnly = True
                    End If
                Else        'Initial report
                    If ThisUser.Result_Release Then
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).ReadOnly = False
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(6).ReadOnly = False
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).ReadOnly = False
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(9).ReadOnly = False
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(10).ReadOnly = False
                    Else    'Only entry - no release
                        If dgvResults.Rows(dgvResults.RowCount - 1).Cells(10).Value = True Then 'Released
                            dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).ReadOnly = True
                            dgvResults.Rows(dgvResults.RowCount - 1).Cells(6).ReadOnly = True
                            dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).ReadOnly = True
                            dgvResults.Rows(dgvResults.RowCount - 1).Cells(9).ReadOnly = True
                            dgvResults.Rows(dgvResults.RowCount - 1).Cells(10).ReadOnly = True
                        Else    'Not released
                            dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).ReadOnly = False
                            dgvResults.Rows(dgvResults.RowCount - 1).Cells(6).ReadOnly = False
                            dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).ReadOnly = False
                            dgvResults.Rows(dgvResults.RowCount - 1).Cells(9).ReadOnly = False
                            dgvResults.Rows(dgvResults.RowCount - 1).Cells(10).ReadOnly = True
                        End If
                    End If
                End If
                '
                'Extras(0, dgvResults.RowCount - 1) = Rs.Fields("Test_ID").Value
                If Cause(0) = "REF" Then
                    'Extras(1, dgvResults.RowCount - 1) = "1"
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(3).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath _
                    & "\Images\Reflux.ico")
                    ''Else
                    ''    Extras(1, dgvResults.RowCount - 1) = "0"
                End If
                '
                If sNote <> "" Then
                    'Extras(2, dgvResults.RowCount - 1) = Rs.Fields("Comment").Value
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath &
                    "\Images\Note.ico")
                Else
                    'Extras(2, dgvResults.RowCount - 1) = ""
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath &
                    "\Images\NoteBlank.ico")
                End If
                If RTFNote <> "" Then
                    'Extras(3, dgvResults.RowCount - 1) = Rs.Fields("T_Result").Value
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(9).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath &
                    "\Images\rtf.ico")
                Else
                    'Extras(3, dgvResults.RowCount - 1) = ""
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(9).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath _
                    & "\Images\rtfBlank.ico")
                End If
            End While
        End If
        cnda.Close()
        cnda = Nothing
        ' *********  End of Acc_Results ********************************************
        ' *********  Start of Ref_Results ******************************************
        sSQL = "Select a.Accession_ID, a.Test_ID, a.Reflexer_ID, a.Reflexed_ID, a.Result, a.Flag, a.Behavior, a.NormalRange, " &
        "a.Comment, a.T_Result, a.Released, a.Released_By, a.Release_Time, b.* from Ref_Results a, Tests b, Requisitions c " &
        "where a.Test_ID = b.ID and a.Accession_ID = c.ID and b.IsActive <> 0 and b.HasResult <> 0 and a.Accession_ID = " &
        AccID & " and a.Result <> '' and (a.Released is NULL or a.Released = 0)"
        Dim cndr As New SqlClient.SqlConnection(connString)
        cndr.Open()
        Dim cmddr As New SqlClient.SqlCommand(sSQL, cndr)
        cmddr.CommandType = CommandType.Text
        Dim drdr As SqlClient.SqlDataReader = cmddr.ExecuteReader
        If drdr.HasRows Then
            While drdr.Read
                InHouse = IsInHouseTest(AccID, drdr("Test_ID"))
                NormalRange = ""
                sFlag(0) = ""
                sFlag(1) = ""
                If drdr("NormalRange") IsNot DBNull.Value _
                AndAlso drdr("NormalRange") <> "" Then
                    NormalRange = Trim(drdr("NormalRange"))
                Else
                    NormalRange = ""
                End If
                If drdr("Flag") IsNot DBNull.Value _
                AndAlso drdr("Flag") <> "" Then
                    sFlag(0) = Trim(drdr("Flag"))
                Else
                    sFlag(0) = ""
                End If
                If drdr("Behavior") IsNot DBNull.Value _
                AndAlso drdr("Behavior") <> "" Then
                    sFlag(1) = Trim(drdr("Behavior"))
                Else
                    If (drdr("Flag") = "LP" Or drdr("Flag") = "HP" Or
                    drdr("Flag").ToString.Contains("Panic") Or
                    drdr("Flag").ToString.Contains("Critic")) Then
                        sFlag(1) = "Panic"
                    ElseIf (drdr("Flag").ToString.StartsWith("L") Or drdr("Flag").ToString.StartsWith("H") Or
                    drdr("Flag").ToString.StartsWith("P") Or drdr("Flag").ToString.StartsWith("R") Or
                    drdr("Flag").ToString.StartsWith("Inc") Or drdr("Flag").ToString.StartsWith("Not")) Then
                        sFlag(1) = "Caution"
                    Else
                        sFlag(1) = "Ignore"
                    End If
                End If
                '
                If drdr("Result") IsNot DBNull.Value Then
                    If drdr("Qualitative") = False Then  'QN
                        If IsNumeric(drdr("Result")) = True Then
                            If InHouse Then
                                sResult = CDbl(drdr("Result")).ToString
                            Else
                                sResult = Trim(drdr("Result"))
                            End If
                        Else
                            sResult = Trim(drdr("Result"))
                        End If
                    Else
                        sResult = Trim(drdr("Result"))
                    End If
                Else
                    sResult = ""
                End If
                '
                If drdr("Comment") IsNot DBNull.Value Then
                    sNote = drdr("Comment")
                Else
                    sNote = ""
                End If
                If drdr("T_Result") IsNot DBNull.Value Then
                    RTFNote = drdr("T_Result")
                Else
                    RTFNote = ""
                End If
                If drdr("Released") Is DBNull.Value _
                OrElse drdr("Released") = False Then
                    Released = False
                    ReleasedOn = Nothing
                    ReleasedBy = Nothing
                End If
                Cause(0) = "REF"
                Cause(1) = drdr("Reflexer_ID").ToString
                Cause(2) = drdr("Reflexed_ID").ToString
                '
                dgvResults.Rows.Add(drdr("Test_ID"), drdr("Name"), sResult, Nothing, sFlag(0),
                NormalRange, Nothing, Nothing, Nothing, Nothing, Released, drdr("Qualitative"),
                AccID, Cause(0), Cause(1), Cause(2), sNote, RTFNote, Nothing, False, sFlag(1))
                '0=TID, 1=TName, 2=Result, 3=nothing, 4=Flag, 5=NormalRange, 6=nothing, 7=nothing, 8=nothing, 9=nothing, 
                '10=Release, 11=Qualitative, 12=AccID, 13=Cause0, 14=Cause1, 15=Cause2, 16=Cmnt, 17=RTF, 18=IMG, 19=Sig, 20=Behavior
                dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).Style.BackColor = Color.White
                '
                If sFlag(1) = "Panic" Then
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(4).Style = PanicStyle
                ElseIf sFlag(1) = "Caution" Then
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(4).Style = AbnormalStyle
                End If
                '
                If drdr("Qualitative") = True Then
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(3).Value =
                    System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath _
                    & "\Images\Looks.ico")
                    '
                    If DeltaCheck(dgvResults.Rows(dgvResults.RowCount - 1).Cells(12).Value,
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(0).Value,
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).Value) = False Then
                        If Not dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Value.ToString.StartsWith(ChrW(8710)) _
                        Then dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Value = ChrW(8710) & " " &
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Value
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(4).Style.BackColor =
                        Color.Red
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Style.ForeColor = Color.Red
                        If InStr(dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value,
                        ChrW(8710) & " Delta Check failed" & vbCrLf) = 0 Then _
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value =
                        ChrW(8710) & " Delta Check failed" & vbCrLf &
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value
                    Else
                        If InStr(dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Value.ToString,
                        ChrW(8710)) > 0 Then dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Value =
                        Replace(dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Value, ChrW(8710) & " ", "")
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(4).Style.BackColor =
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Style.BackColor
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Style.ForeColor = Color.Black
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value =
                        Replace(dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value,
                        ChrW(8710) & " Delta Check failed" & vbCrLf, "")
                    End If
                End If
                '
                If RptComp Then     'Report Complete
                    If ThisUser.Supervisor Or ThisUser.Director Then
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).ReadOnly = False
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(6).ReadOnly = False
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).ReadOnly = False
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(9).ReadOnly = False
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(10).ReadOnly = False
                    Else
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).ReadOnly = True
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(6).ReadOnly = True
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).ReadOnly = True
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(9).ReadOnly = True
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(10).ReadOnly = True
                    End If
                Else        'Initial report
                    If ThisUser.Result_Release Then
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).ReadOnly = False
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(6).ReadOnly = False
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).ReadOnly = False
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(9).ReadOnly = False
                        dgvResults.Rows(dgvResults.RowCount - 1).Cells(10).ReadOnly = False
                    Else    'Only entry - no release
                        If dgvResults.Rows(dgvResults.RowCount - 1).Cells(10).Value = True Then 'Released
                            dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).ReadOnly = True
                            dgvResults.Rows(dgvResults.RowCount - 1).Cells(6).ReadOnly = True
                            dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).ReadOnly = True
                            dgvResults.Rows(dgvResults.RowCount - 1).Cells(9).ReadOnly = True
                            dgvResults.Rows(dgvResults.RowCount - 1).Cells(10).ReadOnly = True
                        Else    'Not released
                            dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).ReadOnly = False
                            dgvResults.Rows(dgvResults.RowCount - 1).Cells(6).ReadOnly = False
                            dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).ReadOnly = False
                            dgvResults.Rows(dgvResults.RowCount - 1).Cells(9).ReadOnly = False
                            dgvResults.Rows(dgvResults.RowCount - 1).Cells(10).ReadOnly = True
                        End If
                    End If
                End If
                '
                'Extras(0, dgvResults.RowCount - 1) = Rs.Fields("Test_ID").Value
                If Cause(0) = "REF" Then
                    'Extras(1, dgvResults.RowCount - 1) = "1"
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(3).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath _
                    & "\Images\Reflux.ico")
                    ''Else
                    ''    Extras(1, dgvResults.RowCount - 1) = "0"
                End If
                '
                If sNote <> "" Then
                    'Extras(2, dgvResults.RowCount - 1) = Rs.Fields("Comment").Value
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath &
                    "\Images\Note.ico")
                Else
                    'Extras(2, dgvResults.RowCount - 1) = ""
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath &
                    "\Images\NoteBlank.ico")
                End If
                If RTFNote <> "" Then
                    'Extras(3, dgvResults.RowCount - 1) = Rs.Fields("T_Result").Value
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(9).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath &
                    "\Images\rtf.ico")
                Else
                    'Extras(3, dgvResults.RowCount - 1) = ""
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(9).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath _
                    & "\Images\rtfBlank.ico")
                End If
                sNote = "" : RTFNote = ""
            End While
        End If
        cndr.Close()
        cndr = Nothing
        '*********  End of Ref_Results ********************************************
        sSQL = "Select a.Accession_ID, a.Test_ID, a.Info_ID, a.Result, a.Flag, a.Behavior, " &
        "a.NormalRange, a.Comment, a.T_Result, a.Released, b.* from Acc_Info_Results " &
        "a, Tests b, Requisitions c where a.Info_ID = b.ID and a.Accession_ID = c.ID " &
        "and b.IsActive <> 0 and b.HasResult <> 0 and a.Accession_ID = " & AccID &
        " and a.Result <> '' and (a.Released is NULL or a.Released = 0)"
        Dim cndi As New SqlClient.SqlConnection(connString)
        cndi.Open()
        Dim cmddi As New SqlClient.SqlCommand(sSQL, cndi)
        cmddi.CommandType = CommandType.Text
        Dim drdi As SqlClient.SqlDataReader = cmddi.ExecuteReader
        If drdi.HasRows Then
            While drdi.Read
                If drdi("Result") IsNot DBNull.Value _
                AndAlso drdi("Result") <> "" Then
                    sResult = Trim(drdi("Result"))
                Else
                    sResult = ""
                End If
                If drdi("Flag") IsNot DBNull.Value _
                AndAlso drdi("Flag") <> "" Then
                    sFlag(0) = Trim(drdi("Flag"))

                Else
                    sFlag(0) = ""
                End If
                If drdi("Behavior") IsNot DBNull.Value Then
                    sFlag(1) = Trim(drdi("Behavior"))
                Else
                    If (drdi("Flag") = "LP" Or drdi("Flag") = "HP" Or
                    drdi("Flag").ToString.Contains("Panic") Or
                    drdi("Flag").ToString.Contains("Critic")) Then
                        sFlag(1) = "Panic"
                    ElseIf (drdi("Flag").ToString.StartsWith("L") Or drdi("Flag").ToString.StartsWith("H") Or
                    drdi("Flag").ToString.StartsWith("P") Or drdi("Flag").ToString.StartsWith("R") Or
                    drdi("Flag").ToString.StartsWith("Inc") Or drdi("Flag").ToString.StartsWith("Not")) Then
                        sFlag(1) = "Caution"
                    Else
                        sFlag(1) = "Ignore"
                    End If
                End If
                If drdi("NormalRange") IsNot DBNull.Value _
                AndAlso drdi("NormalRange") <> "" Then
                    NormalRange = drdi("NormalRange")
                Else
                    NormalRange = ""
                End If
                If drdi("Released") Is DBNull.Value _
                OrElse drdi("Released") = False Then
                    Released = False
                End If
                If drdi("Comment") IsNot DBNull.Value Then
                    sNote = drdi("Comment")
                Else
                    sNote = ""
                End If
                If drdi("T_Result") IsNot DBNull.Value Then
                    RTFNote = drdi("T_Result")
                Else
                    RTFNote = ""
                End If
                dgvResults.Rows.Add(drdi("Info_ID"), drdi("Name"), sResult, Nothing, sFlag,
                NormalRange, Nothing, Nothing, Nothing, Nothing, Released, drdi("Qualitative"),
                AccID, "INFO", drdi("Test_ID"), drdi("Info_ID"), sNote, RTFNote, Nothing, False, sFlag(1))
                '0=TID, 1=TName, 2=Result, 3=nothing, 4=Flag, 5=NormalRange, 6=nothing, 7=nothing, 8=nothing, 9=nothing, 
                '10=Release, 11=Qualitative, 12=AccID, 13=Cause0, 14=Cause1, 15=Cause2, 16=Cmnt, 17=RTF, 18=IMG, 19=Sig
                dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).Style.BackColor = Color.White
                If RptComp = True AndAlso (ThisUser.Supervisor = True Or ThisUser.Director = True) _
                OrElse (ThisUser.Result_Entry = True AndAlso ThisUser.Result_Release = True) Then
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).ReadOnly = False
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(6).ReadOnly = False
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).ReadOnly = False
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(9).ReadOnly = False
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(10).ReadOnly = False
                Else
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).ReadOnly = True
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(6).ReadOnly = True
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).ReadOnly = True
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(9).ReadOnly = True
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(10).ReadOnly = True
                End If
                ' *** Decor ***
                If DeltaCheck(dgvResults.Rows(dgvResults.RowCount - 1).Cells(12).Value,
                dgvResults.Rows(dgvResults.RowCount - 1).Cells(0).Value,
                dgvResults.Rows(dgvResults.RowCount - 1).Cells(2).Value) = False Then
                    If Not dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Value.ToString.StartsWith(ChrW(8710)) _
                    Then dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Value = ChrW(8710) & " " &
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Value
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(4).Style = PanicStyle
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Style.ForeColor = Color.Red
                    If InStr(dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value,
                    ChrW(8710) & " Delta Check failed" & vbCrLf) = 0 Then _
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value =
                    ChrW(8710) & " Delta Check failed" & vbCrLf &
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value
                Else
                    If InStr(dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Value.ToString,
                    ChrW(8710)) > 0 Then dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Value =
                    Replace(dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Value, ChrW(8710) & " ", "")
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(4).Style.BackColor =
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(5).Style.BackColor
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(1).Style.ForeColor =
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(5).Style.ForeColor
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value =
                    Replace(dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value,
                    ChrW(8710) & " Delta Check failed" & vbCrLf, "")
                End If
                ' *** End Delta Check ****
                If dgvResults.Rows(dgvResults.RowCount - 1).Cells(13).Value = "REF" Then
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(3).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Reflux.ico")
                ElseIf dgvResults.Rows(dgvResults.RowCount - 1).Cells(11).Value = True Then 'QL
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(3).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Looks.ico")
                End If
                '
                If dgvResults.Rows(dgvResults.RowCount - 1).Cells(16).Value <> "" Then
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath &
                    "\Images\Note.ico")
                Else
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(7).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath &
                    "\Images\NoteBlank.ico")
                End If
                If dgvResults.Rows(dgvResults.RowCount - 1).Cells(17).Value <> "" Then
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(9).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath &
                    "\Images\rtf.ico")
                Else
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(9).Value =
                    System.Drawing.Image.FromFile(Application.StartupPath _
                    & "\Images\rtfBlank.ico")
                End If
                ' *** Coloring Flag ************
                If sFlag(1) = "Panic" Then
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(4).Style = PanicStyle
                ElseIf sFlag(1) = "Caution" Then
                    dgvResults.Rows(dgvResults.RowCount - 1).Cells(4).Style = HLAStyle
                    'Else
                    '    dgvResults.Rows(dgvResults.RowCount - 1).Cells(4).Style = NormalStyle
                End If
            End While
        End If
        cndi.Close()
        cndi = Nothing
        ''*********  End of Info_Results *******************************************
        Dim COMDIRS() As String = GetAccComDir(AccID)
        txtNote.Text = COMDIRS(0)
        If COMDIRS(1) = "" Then
            If cmbDirector.Items.Count > 0 Then cmbDirector.SelectedIndex = 0
        Else
            If cmbDirector.Items.Count > 0 Then
                Dim d As Integer
                Dim ItemX As MyList
                For d = 0 To cmbDirector.Items.Count - 1
                    ItemX = cmbDirector.Items(d)
                    If ItemX.ItemData = COMDIRS(1) Then
                        cmbDirector.SelectedIndex = d
                        Exit For
                    End If
                Next
            End If
        End If
        COMDIRS = Nothing
        '
        '********** Audit Trail Code ***************
        '0=TestID, 1=Res, 2=Rel
        If SystemConfig.AuditTrail = True Then
            If dgvResults.RowCount > 0 Then
                'ReDim ResultsAT(dgvResults.RowCount - 1, 8)
                ReDim OldResults(2, dgvResults.RowCount - 1)
                For i = 0 To dgvResults.RowCount - 1
                    OldResults(0, i) = dgvResults.Rows(i).Cells(0).Value.ToString
                    OldResults(1, i) = Trim(dgvResults.Rows(i).Cells(2).Value)
                    If dgvResults.Rows(i).Cells(10).Value Is Nothing OrElse
                    dgvResults.Rows(i).Cells(10).Value = False Then
                        OldResults(2, i) = "0"
                    Else
                        OldResults(2, i) = "1"
                    End If
                Next
            End If
        End If
        '********** End Audit Trail ****************
        'Catch Ex As Exception
        '   MsgBox(Ex)
        'End Try
    End Sub

    Private Sub DisplayComment(ByVal AccID As Long)
        Dim cndc As New SqlClient.SqlConnection(connString)
        cndc.Open()
        Dim cmddc As New SqlClient.SqlCommand("Select " &
        "Comment from Requisitions where ID = " & AccID, cndc)
        cmddc.CommandType = CommandType.Text
        Dim drdc As SqlClient.SqlDataReader = cmddc.ExecuteReader
        If drdc.HasRows Then
            While drdc.Read
                If drdc("Comment") IsNot DBNull.Value _
                Then txtComment.Text = drdc("Comment")
            End While
        End If
        cndc.Close()
        cndc = Nothing
    End Sub

    Private Function DeltaCheck(ByVal AccID As Long, ByVal _
    TestID As Integer, ByVal Result As String) As Boolean
        Dim DC As Boolean = True
        Dim cndc As New SqlClient.SqlConnection(connString)
        cndc.Open()
        Dim cmddc As New SqlClient.SqlCommand("Select a.Result as Result from Tests " &
        "c inner join (Requisitions b inner join Acc_Results a on b.ID = a.Accession_ID) " &
        "on a.Test_ID = c.ID where c.DeltaCheck <> 0 and c.ID = " & TestID & " and " &
        "b.Patient_ID in (Select Patient_ID from Requisitions where ID = " & AccID &
        ") and b.ID < " & AccID & " order by b.AccessionDate DESC", cndc)
        cmddc.CommandType = CommandType.Text
        Dim drdc As SqlClient.SqlDataReader = cmddc.ExecuteReader
        If drdc.HasRows Then
            While drdc.Read
                If Result <> "" Then
                    If Trim(drdc("Result")) <> Trim(Result) Then DC = False
                End If
            End While
        End If
        cndc.Close()
        cndc = Nothing
        Return DC
    End Function

    Private Function GetOrgs(ByVal AccID As Long, ByVal TGPID As Integer) As String()
        Dim Orgs() As String = {""}
        Dim cngo As New SqlClient.SqlConnection(connString)
        cngo.Open()
        Dim cmdgo As New SqlClient.SqlCommand("Select distinct Reflexed_ID from " &
        "Ref_Results where Accession_ID = " & AccID & " and Reflexer_ID = " & TGPID, cngo)
        cmdgo.CommandType = CommandType.Text
        Dim drgo As SqlClient.SqlDataReader = cmdgo.ExecuteReader
        If drgo.HasRows Then
            While drgo.Read
                If Orgs(UBound(Orgs)) <> "" Then ReDim Preserve Orgs(UBound(Orgs) + 1)
                Orgs(UBound(Orgs)) = drgo("Reflexed_ID").ToString
            End While
        End If
        cngo.Close()
        cngo = Nothing
        Return Orgs
    End Function

    Private Function GetInfos(ByVal AccID As Long, ByVal TESTID As Integer) As String()
        Dim Infos() As String = {""}
        Dim cni As New SqlClient.SqlConnection(connString)
        cni.Open()
        Dim cmdi As New SqlClient.SqlCommand("Select Info_ID from Acc_Info_Results " &
        "where Accession_ID = " & AccID & " and Test_ID = " & TESTID, cni)
        cmdi.CommandType = CommandType.Text
        Dim dri As SqlClient.SqlDataReader = cmdi.ExecuteReader
        If dri.HasRows Then
            While dri.Read
                If Infos(UBound(Infos)) <> "" Then ReDim Preserve Infos(UBound(Infos) + 1)
                Infos(UBound(Infos)) = dri("Info_ID").ToString
            End While
        End If
        cni.Close()
        cni = Nothing
        Return Infos
    End Function

    Private Function GetAccComDir(ByVal AccID As Long) As String()
        Dim Notes() As String = {"", ""}
        Dim cncd As New SqlClient.SqlConnection(connString)
        cncd.Open()
        Dim cmdcd As New SqlClient.SqlCommand("Select " &
        "* from Requisitions where ID = " & AccID, cncd)
        cmdcd.CommandType = CommandType.Text
        Dim drcd As SqlClient.SqlDataReader = cmdcd.ExecuteReader
        If drcd.HasRows Then
            While drcd.Read
                If drcd("Comment") IsNot DBNull.Value Then
                    Notes(0) = drcd("Comment")
                End If
                If drcd("Director_ID") IsNot DBNull.Value Then
                    Notes(1) = drcd("Director_ID").ToString
                End If
            End While
        End If
        cncd.Close()
        cncd = Nothing
        Return Notes
    End Function

    Private Sub DisplayPatient(ByVal AccID As Long)
        Dim Pat As String = ""
        Dim cnpd As New SqlClient.SqlConnection(connString)
        cnpd.Open()
        Dim cmdpd As New SqlClient.SqlCommand("Select * from Patients where ID in " & _
        "(Select Patient_ID from Requisitions where ID = " & AccID & ")", cnpd)
        cmdpd.CommandType = CommandType.Text
        Dim drpd As SqlClient.SqlDataReader = cmdpd.ExecuteReader
        If drpd.HasRows Then
            While drpd.Read
                Pat = drpd("LastName") & ", " & drpd("FirstName")
                Pat += " DOB: " & Format(drpd("DOB"), SystemConfig.DateFormat) _
                & " Sex: " & drpd("Sex")
            End While
        End If
        cnpd.Close()
        cnpd = Nothing
        txtPatient.Text = Pat
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        If cmbWID.SelectedIndex <> -1 Then
            Dim ItemX As MyList = cmbWID.SelectedItem
            LoadAccessions(dtpFrom.Value, dtpTo.Value, ItemX.ItemData.ToString)
        Else
            LoadAccessions(dtpFrom.Value, dtpTo.Value, "")
        End If

    End Sub

    'Private Sub btnFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirst.Click
    '    If mRs.State = 1 Then
    '        If mRs.RecordCount > 0 Then
    '            mRs.MoveFirst()
    '            Rec = 1
    '            lblPos.Text = Rec.ToString & " of " & Recs.ToString
    '            btnFirst.Enabled = False
    '            btnPrevious.Enabled = False
    '            btnNext.Enabled = True
    '            btnLast.Enabled = True
    '            If cmbWID.SelectedIndex <> -1 Then
    '                Dim ItemX As MyList = cmbWID.SelectedItem
    '                DisplayAccession(mRs.Fields("ID").Value, ItemX.ItemData.ToString)
    '            Else
    '                DisplayAccession(mRs.Fields("ID").Value, "")
    '            End If
    '        End If
    '    End If
    'End Sub

    Private Sub btnFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirst.Click
        If dtRecords.Rows.Count > 0 Then
            Rec = 1
            lblPos.Text = $"{Rec} of {dtRecords.Rows.Count}"
            btnFirst.Enabled = False
            btnPrevious.Enabled = False
            btnNext.Enabled = dtRecords.Rows.Count > 1
            btnLast.Enabled = dtRecords.Rows.Count > 1

            Dim firstRow As DataRow = dtRecords.Rows(0)

            If cmbWID.SelectedIndex <> -1 Then
                Dim ItemX As MyList = cmbWID.SelectedItem
                DisplayAccession(firstRow("ID").ToString(), ItemX.ItemData.ToString())
            Else
                DisplayAccession(firstRow("ID").ToString(), "")
            End If
        End If
    End Sub

    'Private Sub btnPrevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevious.Click
    '    If mRs.State = 1 Then
    '        If mRs.RecordCount > 0 Then
    '            mRs.MovePrevious()
    '            If mRs.AbsolutePosition = ADODB.PositionEnum.adPosBOF Then
    '                btnFirst.Enabled = False
    '                btnPrevious.Enabled = False
    '                mRs.MoveFirst()
    '                Rec = 1
    '            Else
    '                Rec -= 1
    '                btnFirst.Enabled = True
    '                btnPrevious.Enabled = True
    '            End If
    '            lblPos.Text = Rec.ToString & " of " & Recs.ToString

    '            btnNext.Enabled = True
    '            btnLast.Enabled = True
    '            If cmbWID.SelectedIndex <> -1 Then
    '                Dim ItemX As MyList = cmbWID.SelectedItem
    '                DisplayAccession(mRs.Fields("ID").Value, ItemX.ItemData.ToString)
    '            Else
    '                DisplayAccession(mRs.Fields("ID").Value, "")
    '            End If
    '        End If
    '    End If
    'End Sub
    Private Sub btnPrevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevious.Click
        If dtRecords.Rows.Count > 0 AndAlso Rec > 1 Then
            Rec -= 1
            lblPos.Text = $"{Rec} of {dtRecords.Rows.Count}"

            btnFirst.Enabled = Rec > 1
            btnPrevious.Enabled = Rec > 1
            btnNext.Enabled = Rec < dtRecords.Rows.Count
            btnLast.Enabled = Rec < dtRecords.Rows.Count

            Dim currentRow As DataRow = dtRecords.Rows(Rec - 1)

            If cmbWID.SelectedIndex <> -1 Then
                Dim ItemX As MyList = cmbWID.SelectedItem
                DisplayAccession(currentRow("ID").ToString(), ItemX.ItemData.ToString())
            Else
                DisplayAccession(currentRow("ID").ToString(), "")
            End If
        End If
    End Sub

    'Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
    '    If mRs.State = 1 Then
    '        If mRs.RecordCount > 0 Then
    '            mRs.MoveNext()
    '            If mRs.AbsolutePosition = ADODB.PositionEnum.adPosEOF Then
    '                btnLast.Enabled = False
    '                btnNext.Enabled = False
    '                mRs.MoveLast()
    '                Rec = Recs
    '            Else
    '                Rec += 1
    '                btnLast.Enabled = True
    '                btnNext.Enabled = True
    '            End If
    '            lblPos.Text = Rec.ToString & " of " & Recs.ToString
    '            btnFirst.Enabled = True
    '            btnPrevious.Enabled = True
    '            If cmbWID.SelectedIndex <> -1 Then
    '                Dim ItemX As MyList = cmbWID.SelectedItem
    '                DisplayAccession(mRs.Fields("ID").Value, ItemX.ItemData.ToString)
    '            Else
    '                DisplayAccession(mRs.Fields("ID").Value, "")
    '            End If
    '        End If
    '    End If
    'End Sub
    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If dtRecords.Rows.Count > 0 AndAlso Rec < dtRecords.Rows.Count Then
            Rec += 1
            lblPos.Text = $"{Rec} of {dtRecords.Rows.Count}"

            btnNext.Enabled = Rec < dtRecords.Rows.Count
            btnLast.Enabled = Rec < dtRecords.Rows.Count
            btnFirst.Enabled = Rec > 1
            btnPrevious.Enabled = Rec > 1

            Dim currentRow As DataRow = dtRecords.Rows(Rec - 1)

            If cmbWID.SelectedIndex <> -1 Then
                Dim ItemX As MyList = cmbWID.SelectedItem
                DisplayAccession(currentRow("ID").ToString(), ItemX.ItemData.ToString())
            Else
                DisplayAccession(currentRow("ID").ToString(), "")
            End If
        End If
    End Sub

    'Private Sub btnLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLast.Click
    '    If mRs.State = 1 Then
    '        If mRs.RecordCount > 0 Then
    '            mRs.MoveLast()
    '            Rec = Recs
    '            lblPos.Text = Rec.ToString & " of " & Recs.ToString
    '            btnFirst.Enabled = True
    '            btnPrevious.Enabled = True
    '            btnNext.Enabled = False
    '            btnLast.Enabled = False
    '            If cmbWID.SelectedIndex <> -1 Then
    '                Dim ItemX As MyList = cmbWID.SelectedItem
    '                DisplayAccession(mRs.Fields("ID").Value, ItemX.ItemData.ToString)
    '            Else
    '                DisplayAccession(mRs.Fields("ID").Value, "")
    '            End If
    '        End If
    '    End If
    'End Sub
    Private Sub btnLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLast.Click
        If dtRecords.Rows.Count > 0 Then
            Rec = dtRecords.Rows.Count
            lblPos.Text = $"{Rec} of {dtRecords.Rows.Count}"

            btnFirst.Enabled = True
            btnPrevious.Enabled = True
            btnNext.Enabled = False
            btnLast.Enabled = False

            Dim lastRow As DataRow = dtRecords.Rows(Rec - 1)

            If cmbWID.SelectedIndex <> -1 Then
                Dim ItemX As MyList = cmbWID.SelectedItem
                DisplayAccession(lastRow("ID").ToString(), ItemX.ItemData.ToString())
            Else
                DisplayAccession(lastRow("ID").ToString(), "")
            End If
        End If
    End Sub


    'Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
    '    btnSave.Enabled = False
    '    Dim RPTDate As Date
    '    If IsDate(txtRptDate.Text & " " & txtRptTime.Text) Then _
    '    RPTDate = CDate(txtRptDate.Text & " " & txtRptTime.Text)
    '    Try
    '        If chkRelease.Checked = True Then   'all
    '            Dim Exceptions As String = ""
    '            For i As Integer = 0 To dgvExceptions.RowCount - 1
    '                If dgvExceptions.Rows(i).Cells(0).Value IsNot Nothing _
    '                AndAlso Trim(dgvExceptions.Rows(i).Cells(0).Value) <> "" Then
    '                    Exceptions += Trim(dgvExceptions.Rows(i).Cells(0).Value) & ", "
    '                End If
    '            Next
    '            If Exceptions.EndsWith(", ") Then Exceptions = _
    '            Microsoft.VisualBasic.Mid(Exceptions, 1, Len(Exceptions) - 2)
    '            Dim AccIDs As String = ""
    '            mRs.MoveLast()
    '            mRs.MoveFirst()
    '            Do Until mRs.EOF
    '                If mRs.Fields("ID").Value IsNot System.DBNull.Value Then
    '                    AccIDs += mRs.Fields("ID").Value.ToString & ", "
    '                End If
    '                mRs.MoveNext()
    '            Loop
    '            If AccIDs.EndsWith(", ") Then AccIDs = Microsoft.VisualBasic.Mid(AccIDs, 1, Len(AccIDs) - 2)
    '            If IsDate(txtRptDate.Text & " " & txtRptTime.Text) Then RPTDate = CDate(txtRptDate.Text & " " & txtRptTime.Text)
    '            If AccIDs <> "" Then
    '                If Exceptions <> "" Then
    '                    ExecuteSqlProcedure("Update Acc_Results set Released = 1, Released_By = " & ThisUser.ID & ", Release_Time = '" & _
    '                    RPTDate & "' where Not (Result = '' or Result is Null) and (CHARINDEX('LP', Flag) = 0 and CHARINDEX('HP', " & _
    '                    "Flag) = 0 and CHARINDEX('AP', Flag) = 0 and CHARINDEX('Panic', Flag) = 0) and Released = 0 and not " & _
    '                    "Accession_ID in (" & Exceptions & ") and Accession_ID in (" & AccIDs & ")")
    '                    '
    '                    ExecuteSqlProcedure("Update Acc_Info_Results set Released = 1, Released_By = " & ThisUser.ID & _
    '                    ", Release_Time = '" & RPTDate & "' where Not (Result = '' or Result is Null) and (CHARINDEX('LP', " & _
    '                    "Flag) = 0 and CHARINDEX('HP', Flag) = 0 and CHARINDEX('AP', Flag) = 0 and CHARINDEX('Panic', Flag) = 0) and " & _
    '                    "Released = 0 and not Accession_ID in (" & Exceptions & ") and Accession_ID in (" & AccIDs & ")")
    '                    '
    '                    ExecuteSqlProcedure("Update Ref_Results set Released = 1, Released_By = " & ThisUser.ID & _
    '                    ", Release_Time = '" & RPTDate & "' where Not (Result = '' or Result is Null) and (CHARINDEX('LP', " & _
    '                    "Flag) = 0 and CHARINDEX('HP', Flag) = 0 and CHARINDEX('AP', Flag) = 0 and CHARINDEX('Panic', Flag) = 0) and " & _
    '                    "Released = 0 and not Accession_ID in (" & Exceptions & ") and Accession_ID in (" & AccIDs & ")")
    '                Else
    '                    ExecuteSqlProcedure("Update Acc_Results set Released = 1, Released_By = " & ThisUser.ID & _
    '                    ", Release_Time = '" & RPTDate & "' where not (Result = '' or Result is Null) and (CHARINDEX('LP', " & _
    '                    "Flag) = 0 and CHARINDEX('HP', Flag) = 0 and CHARINDEX('AP', Flag) = 0 and CHARINDEX('Panic', Flag) = 0) and " & _
    '                    "Released = 0 and Accession_ID in (" & AccIDs & ")")
    '                    '
    '                    ExecuteSqlProcedure("Update Acc_Info_Results set Released = 1, Released_By = " & ThisUser.ID & _
    '                    ", Release_Time = '" & RPTDate & "' where Not (Result = '' or Result is Null) and (CHARINDEX('LP', " & _
    '                    "Flag) = 0 and CHARINDEX('HP', Flag) = 0 and CHARINDEX('AP', Flag) = 0 and CHARINDEX('Panic', Flag) = 0) and " & _
    '                    "Released = 0 and Accession_ID in (" & AccIDs & ")")
    '                    '
    '                    ExecuteSqlProcedure("Update Ref_Results set Released = 1, Released_By = " & ThisUser.ID & _
    '                    ", Release_Time = '" & RPTDate & "' where Not (Result = '' or Result is Null) and (CHARINDEX('LP', " & _
    '                    "Flag) = 0 and CHARINDEX('HP', Flag) = 0 and CHARINDEX('AP', Flag) = 0 and CHARINDEX('Panic', Flag) = 0) and " & _
    '                    "Released = 0 and Accession_ID in (" & AccIDs & ")")
    '                End If
    '                Dim Accs() As String = Split(AccIDs, ", ")
    '                For i As Integer = 0 To Accs.Length - 1
    '                    UpdateReportTime(Accs(i), Date.Now)
    '                    '
    '                    Dim Itemx As MyList
    '                    If cmbDirector.SelectedIndex <> -1 Then
    '                        Itemx = cmbDirector.SelectedItem
    '                    Else
    '                        Itemx = cmbDirector.Items(0)
    '                    End If
    '                    ExecuteSqlProcedure("Update Requisitions set Director_ID = " & Itemx.ItemData & _
    '                    " where ID = " & Accs(i) & " and Director_ID is NULL")
    '                    '
    '                    LogEvent(Accs(i), 22, GetOrdProvIDFromAccID(Accs(i)), _
    '                    "Result Edit", False, ThisUser.Name, "Result Edit Manual")
    '                Next
    '            End If
    '            ClearForm()
    '        Else
    '            For i As Integer = 0 To dgvResults.RowCount - 1
    '                If dgvResults.Rows(i).Cells(10).Value = True Then   'Release
    '                    If dgvResults.Rows(i).Cells(13).Value.ToString.StartsWith("ACC") Then   'Acc_Res
    '                        ExecuteSqlProcedure("Update Acc_Results set Result = '" & dgvResults.Rows(i).Cells(2).Value & _
    '                        "', Flag = '" & dgvResults.Rows(i).Cells(4).Value & "', Comment = '" & dgvResults.Rows(i).Cells(16).Value & _
    '                        "', T_Result = '" & dgvResults.Rows(i).Cells(17).Value & "', Released = 1, Released_By = " & ThisUser.ID & _
    '                        ", Release_Time = '" & RPTDate & "' where Test_ID = " & dgvResults.Rows(i).Cells(0).Value & _
    '                        " and Accession_ID = " & dgvResults.Rows(i).Cells(12).Value)
    '                    ElseIf dgvResults.Rows(i).Cells(13).Value.ToString.StartsWith("Inf") Then   'Acc_Info_Res
    '                        ExecuteSqlProcedure("Update Acc_Info_Results set Result = '" & dgvResults.Rows(i).Cells(2).Value & _
    '                        "', Flag = '" & dgvResults.Rows(i).Cells(4).Value & "', Comment = '" & dgvResults.Rows(i).Cells(16).Value & _
    '                        "', T_Result = '" & dgvResults.Rows(i).Cells(17).Value & "', Released = 1, Released_By = " & ThisUser.ID & _
    '                        ", Release_Time = '" & RPTDate & "' where Info_ID = " & dgvResults.Rows(i).Cells(0).Value & " and Test_ID = " & _
    '                        dgvResults.Rows(i).Cells(14).Value & " and Accession_ID = " & dgvResults.Rows(i).Cells(12).Value)
    '                    ElseIf dgvResults.Rows(i).Cells(13).Value.ToString.StartsWith("REF") Then   'Ref_Res
    '                        ExecuteSqlProcedure("Update Ref_Results set Result = '" & dgvResults.Rows(i).Cells(2).Value & _
    '                        "', Flag = '" & dgvResults.Rows(i).Cells(4).Value & "', Comment = '" & dgvResults.Rows(i).Cells(16).Value & _
    '                        "', T_Result = '" & dgvResults.Rows(i).Cells(17).Value & "', Released = 1, Released_By = " & ThisUser.ID & _
    '                        ", Release_Time = '" & RPTDate & "' where Test_ID = " & dgvResults.Rows(i).Cells(0).Value & " and Reflexer_ID = " & _
    '                        dgvResults.Rows(i).Cells(14).Value & " and Reflexed_ID = " & dgvResults.Rows(i).Cells(15).Value & " and " & _
    '                        "Accession_ID = " & dgvResults.Rows(i).Cells(12).Value)
    '                    End If
    '                End If
    '            Next
    '            UpdateReportTime(dgvResults.Rows(0).Cells(12).Value, RPTDate)
    '        End If
    '        MsgBox("Changes saved successfully.")
    '    Catch ex As Exception
    '        'SendMail("frmATRResults_Delta", "btnSave_Click", ex.Message)
    '    End Try
    'End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        btnSave.Enabled = False
        Dim RPTDate As Date

        If IsDate($"{txtRptDate.Text} {txtRptTime.Text}") Then
            RPTDate = CDate($"{txtRptDate.Text} {txtRptTime.Text}")
        End If

        Try
            Using connection As New SqlConnection(connString)
                connection.Open()

                If chkRelease.Checked Then ' Bulk release
                    Dim Exceptions As New List(Of String)
                    Dim AccIDs As New List(Of String)

                    For Each row As DataGridViewRow In dgvExceptions.Rows
                        If row.Cells(0).Value IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(row.Cells(0).Value.ToString()) Then
                            Exceptions.Add(row.Cells(0).Value.ToString())
                        End If
                    Next

                    For Each row As DataRow In dtRecords.Rows
                        If Not IsDBNull(row("ID")) Then
                            AccIDs.Add(row("ID").ToString())
                        End If
                    Next

                    If AccIDs.Count > 0 Then
                        Dim accIdsString As String = String.Join(", ", AccIDs)
                        Dim exceptionsString As String = If(Exceptions.Count > 0, $"AND Accession_ID NOT IN ({String.Join(", ", Exceptions)})", "")

                        Dim tables As String() = {"Acc_Results", "Acc_Info_Results", "Ref_Results"}
                        For Each table As String In tables
                            Dim updateQuery As String = $"UPDATE {table} SET Released = 1, Released_By = @UserID, Release_Time = @RPTDate 
                                                      WHERE Result IS NOT NULL AND Result <> '' AND CHARINDEX('LP', Flag) = 0 
                                                      AND CHARINDEX('HP', Flag) = 0 AND CHARINDEX('AP', Flag) = 0 AND CHARINDEX('Panic', Flag) = 0 
                                                      AND Released = 0 {exceptionsString} AND Accession_ID IN ({accIdsString})"

                            Using cmdUpdate As New SqlCommand(updateQuery, connection)
                                cmdUpdate.Parameters.AddWithValue("@UserID", ThisUser.ID)
                                cmdUpdate.Parameters.AddWithValue("@RPTDate", RPTDate)
                                cmdUpdate.ExecuteNonQuery()
                            End Using
                        Next
                    End If

                    For Each accID As String In AccIDs
                        UpdateReportTime(accID, Date.Now)

                        Dim directorID As Integer = If(cmbDirector.SelectedIndex <> -1, CType(cmbDirector.SelectedItem, MyList).ItemData, CType(cmbDirector.Items(0), MyList).ItemData)
                        Dim directorQuery As String = $"UPDATE Requisitions SET Director_ID = @DirectorID WHERE ID = @AccID AND Director_ID IS NULL"

                        Using cmdDirector As New SqlCommand(directorQuery, connection)
                            cmdDirector.Parameters.AddWithValue("@DirectorID", directorID)
                            cmdDirector.Parameters.AddWithValue("@AccID", accID)
                            cmdDirector.ExecuteNonQuery()
                        End Using

                        LogEvent(accID, 22, GetOrdProvIDFromAccID(accID), "Result Edit", False, ThisUser.Name, "Result Edit Manual")
                    Next
                Else ' Individual release
                    For Each row As DataGridViewRow In dgvResults.Rows
                        If Convert.ToBoolean(row.Cells(10).Value) Then ' Release condition
                            Dim table As String = If(row.Cells(13).Value.ToString().StartsWith("ACC"), "Acc_Results",
                                                 If(row.Cells(13).Value.ToString().StartsWith("Inf"), "Acc_Info_Results",
                                                 If(row.Cells(13).Value.ToString().StartsWith("REF"), "Ref_Results", "")))

                            If Not String.IsNullOrEmpty(table) Then
                                Dim updateQuery As String = $"UPDATE {table} SET Result = @Result, Flag = @Flag, Comment = @Comment, 
                                                          T_Result = @TResult, Released = 1, Released_By = @UserID, Release_Time = @RPTDate 
                                                          WHERE Test_ID = @TestID AND Accession_ID = @AccID"

                                Using cmdUpdate As New SqlCommand(updateQuery, connection)
                                    cmdUpdate.Parameters.AddWithValue("@Result", row.Cells(2).Value.ToString())
                                    cmdUpdate.Parameters.AddWithValue("@Flag", row.Cells(4).Value.ToString())
                                    cmdUpdate.Parameters.AddWithValue("@Comment", row.Cells(16).Value.ToString())
                                    cmdUpdate.Parameters.AddWithValue("@TResult", row.Cells(17).Value.ToString())
                                    cmdUpdate.Parameters.AddWithValue("@UserID", ThisUser.ID)
                                    cmdUpdate.Parameters.AddWithValue("@RPTDate", RPTDate)
                                    cmdUpdate.Parameters.AddWithValue("@TestID", row.Cells(0).Value.ToString())
                                    cmdUpdate.Parameters.AddWithValue("@AccID", row.Cells(12).Value.ToString())
                                    cmdUpdate.ExecuteNonQuery()
                                End Using
                            End If
                        End If
                    Next

                    UpdateReportTime(dgvResults.Rows(0).Cells(12).Value.ToString(), RPTDate)
                End If
            End Using

            MsgBox("Changes saved successfully.")
        Catch ex As Exception
            ' Handle exception (logging, sending error details via email, etc.)
        End Try
    End Sub

    Private Function IsResultPanic(ByVal Flag As String) As Boolean
        Dim IsPanic As Boolean = False
        If Flag = "LP" Or Flag = "HP" Or _
        InStr(Flag, "Panic") > 0 Or _
        InStr(Flag, "Critical") > 0 Then IsPanic = True
        Return IsPanic
    End Function

    Private Sub UpdateComment(ByVal AccID As Long)
        ExecuteSqlProcedure("Update Requisitions set Comment = '" & _
        Trim(txtNote.Text) & "' where ID = " & AccID)
    End Sub

    Private Sub btnRelease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRelease.Click
        If dgvResults.RowCount > 0 Then
            Dim i As Integer
            For i = 0 To dgvResults.RowCount - 1
                If dgvResults.Rows(i).Cells(2).Value.ToString <> "" And _
                (dgvResults.Rows(i).Cells(20).Value <> "Panic" Or _
                (dgvResults.Rows(i).Cells(20).Value = "Panic" And _
                 dgvResults.Rows(i).Cells(16).Value <> "")) Then
                    dgvResults.Rows(i).Cells(10).Value = True
                Else
                    dgvResults.Rows(i).Cells(10).Value = False
                End If
            Next
            txtRptDate.Text = Format(Date.Today, SystemConfig.DateFormat)
            txtRptTime.Text = Format(Date.Now, "HH:mm")
        End If
        RoutineProgress()
    End Sub

    Private Sub btnBlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBlock.Click
        If dgvResults.RowCount > 0 Then
            Dim i As Integer
            For i = 0 To dgvResults.RowCount - 1
                If dgvResults.Rows(i).Cells(2).Value.ToString <> "" Then _
                    dgvResults.Rows(i).Cells(10).Value = False
            Next
            txtRptDate.Text = Format(Date.Today, SystemConfig.DateFormat)
            txtRptTime.Text = Format(Date.Now, "HH:mm")
        End If
        RoutineProgress()
    End Sub

    Private Sub RoutineProgress()
        If dgvResults.RowCount > 0 Then
            Dim i As Integer
            For i = 0 To dgvResults.RowCount - 1
                If dgvResults.Rows(i).Cells(2).Value.ToString <> "" And _
                    dgvResults.Rows(i).Cells(10).Value = True Then
                    btnSave.Enabled = True
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub dgvResults_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        If e.ColumnIndex = 2 Or e.ColumnIndex = 10 Then btnSave.Enabled = True
    End Sub


    Private Sub chkRelease_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRelease.CheckedChanged
        If chkRelease.Checked = False Then  'Release Individual
            chkRelease.Text = "Release Individual"
            btnFirst.Enabled = True
            btnPrevious.Enabled = True
            btnNext.Enabled = True
            btnLast.Enabled = True
            btnRelease.Enabled = True
        Else
            chkRelease.Text = "Release All"
            btnFirst.Enabled = False
            btnPrevious.Enabled = False
            btnNext.Enabled = False
            btnLast.Enabled = False
            btnRelease.Enabled = False
        End If
    End Sub

    Private Sub dgvExceptions_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvExceptions.CellEndEdit
        If Trim(dgvExceptions.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) <> "" Then
            If IsNumeric(Trim(dgvExceptions.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)) = False Then
                MsgBox("Only digits are allowed.")
                dgvExceptions.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
            Else
                If IsDuplicate(Trim(dgvExceptions.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)) Then
                    MsgBox("Duplicate Entry is not allowed.")
                    dgvExceptions.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
                End If
            End If
            If e.RowIndex = dgvExceptions.RowCount - 1 Then
                dgvExceptions.Rows.Add()
                SendKeys.Send("{ENTER}")
            End If
        Else
            If e.RowIndex < dgvExceptions.RowCount - 1 Then
                dgvExceptions.Rows.RemoveAt(e.RowIndex)
            End If
        End If
    End Sub

    Private Function IsDuplicate(ByVal AccID As Long) As Boolean
        Dim i As Integer
        Dim CT As Integer = 0
        For i = 0 To dgvExceptions.RowCount - 1
            If dgvExceptions.Rows(i).Cells(0).Value = AccID.ToString Then CT += 1
        Next
        If CT > 1 Then
            IsDuplicate = True
        Else
            IsDuplicate = False
        End If
    End Function

    Private Sub dgvExceptions_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvExceptions.CellMouseUp
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            If Clipboard.ContainsText Then
                dgvExceptions.Rows.Clear()
                Dim Accs() As String = Split(Clipboard.GetText, vbCrLf)
                For i As Integer = 0 To Accs.Length - 1
                    If Trim(Accs(i)) <> "" Then
                        If dgvExceptions.RowCount = 0 Then dgvExceptions.Rows.Add()
                        If dgvExceptions.Rows(dgvExceptions.RowCount - 1).Cells(0).Value _
                        = "" Then
                            dgvExceptions.Rows(dgvExceptions.RowCount - 1).Cells(0).Value = Trim(Accs(i))
                        Else
                            dgvExceptions.Rows.Add()
                            dgvExceptions.Rows(dgvExceptions.RowCount - 1).Cells(0).Value = Trim(Accs(i))
                        End If
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub dgvResults_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvResults.CellClick
        If e.ColumnIndex = 3 Then   'Reflux
            '*** QL Reflexing ***************************************************             
            'If dgvResults.Rows(e.RowIndex).Cells(11).Value = True Then  'QL
            '    ChoiceTestID = dgvResults.Rows(e.RowIndex).Cells(0).Value
            '    Dim Choice As String = frmChoiceLook.ShowDialog()
            '    If Choice <> "" Then
            '        dgvResults.Rows(e.RowIndex).Cells(2).Value = Choice
            '        If ThisUser.Result_Release = True AndAlso _
            '        SystemConfig.ReleaseWithEntry Then _
            '        dgvResults.Rows(e.RowIndex).Cells(10).Value = True 'Rel if required
            '        IsDirty = True
            '        txtRptDate.Text = Format(Date.Today, SystemConfig.DateFormat)
            '        txtRptTime.Text = Format(Date.Now, "HH:mm")
            '        My.Application.DoEvents()
            '        If btnAccQC.Checked = False AndAlso _
            '        dgvResults.Rows(e.RowIndex).Cells(11).Value = True Then    'QL Test of Accession
            '            If IsAutomarker(dgvResults.Rows(e.RowIndex).Cells(0).Value) Then
            '                If ResultTriggering(dgvResults.Rows(e.RowIndex).Cells(0).Value, _
            '                dgvResults.Rows(e.RowIndex).Cells(2).Value) Then
            '                    TriggerID = dgvResults.Rows(e.RowIndex).Cells(0).Value
            '                    Dim NewVals As String = frmReflux.ShowDialog()
            '                    If NewVals <> "" Then
            '                        ProcessCMDTrigger(NewVals, dgvResults.Rows(e.RowIndex).Cells(13).Value)
            '                        TriggerID = Nothing
            '                        IsDirty = True
            '                        txtRptDate.Text = Format(Date.Now, SystemConfig.DateFormat)
            '                        txtRptTime.Text = Format(Date.Now, "HH:mm")
            '                        If SystemConfig.CumRef = False Then btnRefresh_Click(Nothing, Nothing)
            '                    End If
            '                Else
            '                    'CleanAutomarked
            '                    Dim RefCleaner As String = GetRefCleaner(dgvResults.Rows(e.RowIndex).Cells(12).Value, dgvResults.Rows(e.RowIndex).Cells(0).Value)
            '                    ProcessCMDTrigger(RefCleaner, dgvResults.Rows(e.RowIndex).Cells(13).Value.ToString)
            '                    IsDirty = True
            '                    txtRptDate.Text = Format(Date.Now, SystemConfig.DateFormat)
            '                    txtRptTime.Text = Format(Date.Now, "HH:mm")
            '                    If SystemConfig.CumRef = False Then btnRefresh_Click(Nothing, Nothing)
            '                End If
            '                'If IsDirty Then DecorateGridRow(e.RowIndex)
            '            End If
            '            dgvResults.Rows(e.RowIndex).Cells(4).Value = _
            '            GetFlag(dgvResults.Rows(e.RowIndex).Cells(12).Value, _
            '            dgvResults.Rows(e.RowIndex).Cells(2).Value, _
            '            dgvResults.Rows(e.RowIndex).Cells(0).Value)
            '            'If dgvResults.Rows(e.RowIndex).Cells(4).Value = "" Then 'Unrecognized
            '            '    RetVal = MsgBox("Your entry is outside of the prdefined scope and appears to " & _
            '            '    "be a typographical error. Are you certain of the correctness of your entry?", _
            '            '    MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
            '            '    If RetVal = vbNo Then dgvResults.Rows(e.RowIndex).Cells(2).Value = ""
            '            'End If
            '        End If
            '    End If
            'End If
        ElseIf e.ColumnIndex = 6 Then       'History
            frmHistory.TestID = dgvResults.Rows(e.RowIndex).Cells(0).Value
            frmHistory.AccessionID = txtAccID.Text

            frmHistory.ShowDialog()
        ElseIf e.ColumnIndex = 7 Then       'Note
            txtComment.Text = dgvResults.Rows(e.RowIndex).Cells(16).Value
            'Dim Note As String = frmResultNote.ShowDialog()
            Dim Note As String = dgvResults.Rows(e.RowIndex).Cells(16).Value

            Using frm As New frmResultNote()
                frm.SavedNote = Trim(dgvResults.Rows(e.RowIndex).Cells(16).Value)
                frm.ShowDialog()

                If frm.DialogResult = DialogResult.OK Then
                    Note = frm.txtNote.Text.Trim
                End If
            End Using

            If Note <> "" Then
                dgvResults.Rows(e.RowIndex).Cells(16).Value = Note
                dgvResults.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = _
                System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Note.ico")
            Else
                dgvResults.Rows(e.RowIndex).Cells(16).Value = ""
                dgvResults.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = _
                System.Drawing.Image.FromFile(Application.StartupPath & "\Images\NoteBlank.ico")
            End If
        ElseIf e.ColumnIndex = 9 Then       'Extended Result
            txtRTF.Rtf = dgvResults.Rows(e.RowIndex).Cells(17).Value
            txtRTF.Rtf = frmRTF.ShowDialog()
            If txtRTF.Text <> "" Then
                dgvResults.Rows(e.RowIndex).Cells(17).Value = txtRTF.Rtf
                dgvResults.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = _
                System.Drawing.Image.FromFile(Application.StartupPath & "\Images\RTF.ico")
            Else
                dgvResults.Rows(e.RowIndex).Cells(17).Value = ""
                dgvResults.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = _
                System.Drawing.Image.FromFile(Application.StartupPath & "\Images\RTFBlank.ico")
            End If
        ElseIf e.ColumnIndex = 10 Then       'Release Result
            If (CType(dgvResults.Rows(e.RowIndex).Cells(10).Value, Boolean) = False _
            AndAlso Trim(dgvResults.Rows(e.RowIndex).Cells(2).Value.ToString) <> "") _
            And ((IsResultPanic(dgvResults.Rows(e.RowIndex).Cells(4).Value) = _
            False) Or (IsResultPanic(dgvResults.Rows(e.RowIndex).Cells(4).Value) _
            = True And dgvResults.Rows(e.RowIndex).Cells(16).Value <> "")) Then
                dgvResults.Rows(e.RowIndex).Cells(10).Value = True
            Else
                If dgvResults.Rows(e.RowIndex).Cells(16).Value = "" AndAlso _
                IsResultPanic(dgvResults.Rows(e.RowIndex).Cells(4).Value) Then _
                MsgBox("The result will not get released, as Prolis requires verification", MsgBoxStyle.Critical, "Prolis")
                dgvResults.Rows(e.RowIndex).Cells(10).Value = False
            End If
            txtRptDate.Text = Format(Date.Now, SystemConfig.DateFormat)
            txtRptTime.Text = Format(Date.Now, "HH:mm")
            btnSave.Enabled = True
        End If
    End Sub

    Private Sub dgvResults_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvResults.CellEndEdit
        If e.ColumnIndex = 10 Then
            If dgvResults.Rows(e.RowIndex).Cells(10).Value = True Then
                If Trim(dgvResults.Rows(e.RowIndex).Cells(2).Value.ToString) <> "" _
                And ((dgvResults.Rows(e.RowIndex).Cells(20).Value <> "Panic") Or _
                (dgvResults.Rows(e.RowIndex).Cells(20).Value = "Panic" And _
                dgvResults.Rows(e.RowIndex).Cells(16).Value <> "")) Then
                    dgvResults.Rows(e.RowIndex).Cells(10).Value = True
                    'Exit Sub
                Else
                    If dgvResults.Rows(e.RowIndex).Cells(16).Value = "" AndAlso _
                    dgvResults.Rows(e.RowIndex).Cells(20).Value = "Panic" Then
                        MsgBox("The result will not get released, as Prolis requires verification", MsgBoxStyle.Critical, "Prolis")
                        dgvResults.Rows(e.RowIndex).Cells(10).Value = False
                        'Exit Sub
                    End If
                End If
            End If
            txtRptDate.Text = Format(Date.Today, SystemConfig.DateFormat)
            txtRptTime.Text = Format(Date.Now, "HH:mm")
            '
            RoutineProgress()
        End If
    End Sub
End Class
