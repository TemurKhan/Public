'Imports System.Windows.Forms
'Imports System.data

'Public Class frmPreanalytics
'    Private IsDirty As Boolean
'    Private RsA As New ADODB.Recordset
'    Private Accessions As Integer
'    Private CurrAcc As Integer

'    Private Sub frmPreanalytics_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
'        If IsDirty Then
'            SaveChanges()
'            IsDirty = False
'        End If
'    End Sub

'    Private Sub frmPreanalytics_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
'        ConfigureDateTimePicker(dtpDateFrom)
'        ConfigureDateTimePicker(dtpDateTo)
'    End Sub

'    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
'        Dim sSQL As String = ""
'        If (IsDate(dtpDateFrom.Text) Or IsDate(dtpDateTo.Text) Or
'        txtAccFrom.Text <> "" Or txtAccTo.Text <> "") Then
'            sSQL = "Select ID from Requisitions where ID in (Select distinct Accession_ID " &
'            "from Req_Info_Response) and Received <> 0"
'            '
'            If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
'                sSQL += " and AccessionDate between '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) &
'                "' and '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) & " 23:59:00'"
'            ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
'                sSQL += " and AccessionDate between '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) &
'                "' and '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) & " 23:59:00'"
'            ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
'                sSQL += " and AccessionDate between '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) &
'                "' and '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) & " 23:59:00'"
'            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
'                sSQL += " and ID = " & Val(txtAccFrom.Text)
'            ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
'                sSQL += " and ID = " & Val(txtAccTo.Text)
'            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
'                If Val(txtAccFrom.Text) < Val(txtAccTo.Text) Then
'                    sSQL += " and ID >= " & Val(txtAccFrom.Text) & " and ID <= " _
'                    & Val(txtAccTo.Text)
'                ElseIf Val(txtAccFrom.Text) > Val(txtAccTo.Text) Then
'                    sSQL += " and ID <= " & Val(txtAccFrom.Text) & " and ID >= " _
'                    & Val(txtAccTo.Text)
'                Else
'                    sSQL += " and ID = " & Val(txtAccFrom.Text)
'                End If
'            End If
'            '
'            Dim CNPA As New ADODB.Connection
'            CNPA.Open(connstring)
'            If RsA.State = 1 Then RsA.Close()
'            RsA.Open(sSQL, CNPA, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
'            If Not RsA.BOF Then
'                RsA.MoveLast()
'                RsA.MoveFirst()
'                CurrAcc = 1
'                Accessions = RsA.RecordCount
'                txtNavStatus.Text = CurrAcc & " of " & Accessions
'                If Accessions > 1 Then
'                    btnNext.Enabled = True
'                    btnLast.Enabled = True
'                End If
'                DisplayInfoRecord(RsA.Fields("ID").Value)
'            Else
'                txtNavStatus.Text = "" : btnFirst.Enabled = False : btnPrevious.Enabled = False
'                btnNext.Enabled = False : btnLast.Enabled = False
'            End If
'            txtAccFrom.Text = "" : txtAccTo.Text = "" : dtpDateFrom.Text = "" : dtpDateTo.Text = ""
'        End If
'    End Sub




'   

'    Private Sub dgvInfos_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvInfos.CellEndEdit
'        If e.ColumnIndex = 2 Then IsDirty = True
'    End Sub

'    Private Sub dgvInfos_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvInfos.DataError
'        On Error Resume Next
'    End Sub

'    Private Sub txtDateFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
'        'txtDateFrom.BackColor = FCOLOR
'    End Sub

'    Private Sub txtDateFrom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
'        If e.KeyCode = Keys.Enter Then
'            SendKeys.Send("{TAB}")
'        End If
'    End Sub

'    Private Sub txtDateFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
'        'txtDateFrom.BackColor = NFCOLOR
'        'If UserEnteredText(txtDateFrom) <> "" Then
'        '    If IsDate(txtDateFrom.Text) = False Then
'        '        MsgBox("Invalid date")
'        '        txtDateFrom.Text = ""
'        '    Else
'        '        txtAccFrom.Text = ""
'        '        txtAccTo.Text = ""
'        '    End If
'        'End If
'    End Sub

'    Private Sub txtDateTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
'        'txtDateTo.BackColor = FCOLOR
'    End Sub

'    Private Sub txtDateTo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
'        If e.KeyCode = Keys.Enter Then
'            SendKeys.Send("{TAB}")
'        ElseIf e.KeyCode = Keys.Up Then
'            SendKeys.Send("+{TAB}")
'        End If
'    End Sub

'    Private Sub txtDateTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
'        'txtDateTo.BackColor = NFCOLOR
'        'If UserEnteredText(txtDateTo) <> "" Then
'        '    If IsDate(txtDateTo.Text) = False Then
'        '        MsgBox("Invalid date")
'        '        txtDateTo.Text = ""
'        '    Else
'        '        txtAccFrom.Text = ""
'        '        txtAccTo.Text = ""
'        '    End If
'        'End If
'    End Sub

'    Private Sub txtAccFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccFrom.GotFocus
'        txtAccFrom.BackColor = FCOLOR
'    End Sub

'    Private Sub txtAccFrom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAccFrom.KeyDown
'        If e.KeyCode = Keys.Enter Then
'            SendKeys.Send("{TAB}")
'        ElseIf e.KeyCode = Keys.Up Then
'            SendKeys.Send("+{TAB}")
'        End If
'    End Sub

'    Private Sub txtAccFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccFrom.KeyPress
'        Numerals(sender, e)
'    End Sub

'    Private Sub txtAccFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccFrom.LostFocus
'        txtAccFrom.BackColor = NFCOLOR
'        If txtAccFrom.Text <> "" Then
'            ClearDateTimePicker(dtpDateFrom)
'            ClearDateTimePicker(dtpDateTo)
'        End If
'    End Sub

'    Private Sub txtAccTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccTo.GotFocus
'        txtAccTo.BackColor = FCOLOR
'    End Sub

'    Private Sub txtAccTo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAccTo.KeyDown
'        If e.KeyCode = Keys.Enter Then
'            SendKeys.Send("{TAB}")
'        ElseIf e.KeyCode = Keys.Up Then
'            SendKeys.Send("+{TAB}")
'        End If
'    End Sub

'    Private Sub txtAccTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccTo.KeyPress
'        Numerals(sender, e)
'    End Sub

'    Private Sub txtAccTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccTo.LostFocus
'        txtAccTo.BackColor = NFCOLOR
'        If txtAccTo.Text <> "" Then
'            ClearDateTimePicker(dtpDateFrom)
'            ClearDateTimePicker(dtpDateTo)
'        End If
'    End Sub

'    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
'        If IsDirty Then
'            SaveChanges()
'            IsDirty = False
'        End If
'        RsA.MoveNext()
'        CurrAcc += 1
'        btnPrevious.Enabled = True
'        btnFirst.Enabled = True
'        If RsA.EOF Then
'            RsA.MoveLast()
'            CurrAcc = Accessions
'            btnNext.Enabled = False
'            btnLast.Enabled = False
'        End If
'        txtNavStatus.Text = CurrAcc & " of " & Accessions
'        DisplayInfoRecord(RsA.Fields("ID").Value)
'    End Sub

'    Private Sub btnLast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLast.Click
'        If IsDirty Then
'            SaveChanges()
'            IsDirty = False
'        End If
'        RsA.MoveLast()
'        CurrAcc = Accessions
'        btnPrevious.Enabled = True
'        btnFirst.Enabled = True
'        btnNext.Enabled = False
'        btnLast.Enabled = False
'        txtNavStatus.Text = CurrAcc & " of " & Accessions
'        DisplayInfoRecord(RsA.Fields("ID").Value)
'    End Sub

'    Private Sub btnPrevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrevious.Click
'        If IsDirty Then
'            SaveChanges()
'            IsDirty = False
'        End If
'        RsA.MovePrevious()
'        CurrAcc -= 1
'        btnNext.Enabled = True
'        btnLast.Enabled = True
'        If RsA.BOF Then
'            RsA.MoveFirst()
'            CurrAcc = 1
'            btnPrevious.Enabled = False
'            btnFirst.Enabled = False
'        End If
'        txtNavStatus.Text = CurrAcc & " of " & Accessions
'        DisplayInfoRecord(RsA.Fields("ID").Value)
'    End Sub

'    Private Sub btnFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFirst.Click
'        If IsDirty Then
'            SaveChanges()
'            IsDirty = False
'        End If
'        RsA.MoveFirst()
'        CurrAcc = 1
'        btnNext.Enabled = True
'        btnLast.Enabled = True
'        btnPrevious.Enabled = False
'        btnFirst.Enabled = False
'        txtNavStatus.Text = CurrAcc & " of " & Accessions
'        DisplayInfoRecord(RsA.Fields("ID").Value)
'    End Sub

'    Private Sub SaveChanges()
'        Dim i As Integer
'        For i = 0 To dgvInfos.RowCount - 1
'            If dgvInfos.Rows(i).Cells(5).Value = "D" Then
'                ExecuteSqlProcedure("Update Req_Info_Response set Response = '" &
'                Trim(dgvInfos.Rows(i).Cells(2).Value) & "' where Info_ID = " &
'                dgvInfos.Rows(i).Cells(0).Value & " and TGP_ID = " &
'                dgvInfos.Rows(i).Cells(6).Value & " and Accession_ID = " &
'                dgvInfos.Rows(i).Cells(7).Value)
'            End If
'        Next
'    End Sub

'    Private Sub dtpDateFrom_CloseUp(sender As Object, e As EventArgs) Handles dtpDateFrom.CloseUp
'        ' After selecting a valid date, revert to the standard date format
'        CloseUpDateTimePicker(dtpDateFrom)
'    End Sub
'    Private Sub dtpDateTo_CloseUp(sender As Object, e As EventArgs) Handles dtpDateTo.CloseUp
'        CloseUpDateTimePicker(dtpDateTo)
'    End Sub

'    Private Sub lblClearDates_Click(sender As Object, e As EventArgs) Handles lblClearDates.Click
'        ClearDateTimePicker(dtpDateFrom)
'        ClearDateTimePicker(dtpDateTo)
'    End Sub

'End Class

Imports System.Windows.Forms
Imports System.Data
Imports System.Data.SqlClient

Public Class frmPreanalytics
    Private IsDirty As Boolean
    Private DtAccessions As DataTable
    Private CurrAcc As Integer
    Private Accessions As Integer

    Private Sub frmPreanalytics_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If IsDirty Then
            SaveChanges()
            IsDirty = False
        End If
    End Sub

    Private Sub frmPreanalytics_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        ConfigureDateTimePicker(dtpDateFrom)
        ConfigureDateTimePicker(dtpDateTo)
    End Sub

    Private Sub btnLoad_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLoad.Click
        If (IsDate(dtpDateFrom.Text) Or IsDate(dtpDateTo.Text) Or txtAccFrom.Text <> "" Or txtAccTo.Text <> "") Then
            Dim sSQL As String = "SELECT ID FROM Requisitions WHERE ID IN (" &
                "SELECT DISTINCT Accession_ID FROM Req_Info_Response) AND Received <> 0"

            If IsDate(dtpDateFrom.Text) AndAlso Not IsDate(dtpDateTo.Text) Then
                sSQL += $" AND AccessionDate BETWEEN '{Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat)}' AND '{Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat)} 23:59:00'"
            ElseIf Not IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                sSQL += $" AND AccessionDate BETWEEN '{Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat)}' AND '{Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat)} 23:59:00'"
            ElseIf IsDate(dtpDateFrom.Text) AndAlso IsDate(dtpDateTo.Text) Then
                sSQL += $" AND AccessionDate BETWEEN '{Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat)}' AND '{Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat)} 23:59:00'"
            End If

            If txtAccFrom.Text <> "" AndAlso txtAccTo.Text = "" Then
                sSQL += $" AND ID = {Val(txtAccFrom.Text)}"
            ElseIf txtAccFrom.Text = "" AndAlso txtAccTo.Text <> "" Then
                sSQL += $" AND ID = {Val(txtAccTo.Text)}"
            ElseIf txtAccFrom.Text <> "" AndAlso txtAccTo.Text <> "" Then
                Dim fromVal As Integer = Val(txtAccFrom.Text)
                Dim toVal As Integer = Val(txtAccTo.Text)
                If fromVal < toVal Then
                    sSQL += $" AND ID BETWEEN {fromVal} AND {toVal}"
                Else
                    sSQL += $" AND ID BETWEEN {toVal} AND {fromVal}"
                End If
            End If

            Using conn As New SqlConnection(connString)
                Using da As New SqlDataAdapter(sSQL, conn)
                    DtAccessions = New DataTable()
                    da.Fill(DtAccessions)
                End Using
            End Using

            If DtAccessions.Rows.Count > 0 Then
                CurrAcc = 1
                Accessions = DtAccessions.Rows.Count
                txtNavStatus.Text = $"{CurrAcc} of {Accessions}"
                If Accessions > 1 Then
                    btnNext.Enabled = True
                    btnLast.Enabled = True
                End If
                DisplayInfoRecord(DtAccessions.Rows(CurrAcc - 1)("ID"))
            Else
                txtNavStatus.Text = ""
                btnFirst.Enabled = False
                btnPrevious.Enabled = False
                btnNext.Enabled = False
                btnLast.Enabled = False
            End If

            txtAccFrom.Clear()
            txtAccTo.Clear()
            dtpDateFrom.Value = Date.Today
            dtpDateTo.Value = Date.Today
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If IsDirty Then SaveChanges() : IsDirty = False

        CurrAcc += 1
        If CurrAcc > Accessions Then CurrAcc = Accessions
        txtNavStatus.Text = $"{CurrAcc} of {Accessions}"
        DisplayInfoRecord(DtAccessions.Rows(CurrAcc - 1)("ID"))

        btnPrevious.Enabled = True
        btnFirst.Enabled = True
        btnNext.Enabled = CurrAcc < Accessions
        btnLast.Enabled = CurrAcc < Accessions
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If IsDirty Then SaveChanges() : IsDirty = False

        CurrAcc -= 1
        If CurrAcc < 1 Then CurrAcc = 1
        txtNavStatus.Text = $"{CurrAcc} of {Accessions}"
        DisplayInfoRecord(DtAccessions.Rows(CurrAcc - 1)("ID"))

        btnNext.Enabled = True
        btnLast.Enabled = True
        btnPrevious.Enabled = CurrAcc > 1
        btnFirst.Enabled = CurrAcc > 1
    End Sub

    Private Sub btnFirst_Click(sender As Object, e As EventArgs) Handles btnFirst.Click
        If IsDirty Then SaveChanges() : IsDirty = False

        CurrAcc = 1
        txtNavStatus.Text = $"{CurrAcc} of {Accessions}"
        DisplayInfoRecord(DtAccessions.Rows(CurrAcc - 1)("ID"))

        btnNext.Enabled = True
        btnLast.Enabled = True
        btnPrevious.Enabled = False
        btnFirst.Enabled = False
    End Sub

    Private Sub btnLast_Click(sender As Object, e As EventArgs) Handles btnLast.Click
        If IsDirty Then SaveChanges() : IsDirty = False

        CurrAcc = Accessions
        txtNavStatus.Text = $"{CurrAcc} of {Accessions}"
        DisplayInfoRecord(DtAccessions.Rows(CurrAcc - 1)("ID"))

        btnPrevious.Enabled = True
        btnFirst.Enabled = True
        btnNext.Enabled = False
        btnLast.Enabled = False
    End Sub

    Private Sub SaveChanges()
        Using conn As New SqlConnection(connString)
            conn.Open()
            For Each row As DataGridViewRow In dgvInfos.Rows
                If row.Cells(5).Value = "D" Then
                    Dim cmd As New SqlCommand("UPDATE Req_Info_Response SET Response = @Response WHERE Info_ID = @InfoID AND TGP_ID = @TGPID AND Accession_ID = @AccID", conn)
                    cmd.Parameters.AddWithValue("@Response", row.Cells(2).Value.ToString())
                    cmd.Parameters.AddWithValue("@InfoID", CInt(row.Cells(0).Value))
                    cmd.Parameters.AddWithValue("@TGPID", CInt(row.Cells(6).Value))
                    cmd.Parameters.AddWithValue("@AccID", CInt(row.Cells(7).Value))
                    cmd.ExecuteNonQuery()
                End If
            Next
        End Using
    End Sub

    Friend Sub DisplayInfoRecord(ByVal AccID As Long)
        dgvInfos.Rows.Clear()
        Dim HRow As Boolean = False
        Dim RunningTGP(0, 1) As String
        Dim Insured As Long = -1
        Dim cnir As New SqlClient.SqlConnection(connString)
        cnir.Open()
        Dim cmdir As New SqlClient.SqlCommand("Select a.*, b.Required from " &
        "Req_Info_Response a inner join TGP_Info b on a.Info_ID = b.Info_ID " &
        "where a.TGP_ID = b.TGP_ID and a.Accession_ID in (Select ID from " &
        "Requisitions where Received <> 0 and ID = " & AccID & ") order by a.Ordinal", cnir)
        cmdir.CommandType = CommandType.Text
        Dim drir As SqlClient.SqlDataReader = cmdir.ExecuteReader
        If drir.HasRows Then
            DisplayProvider(AccID)
            DisplayPatient(AccID)
            While drir.Read
                RunningTGP(0, 0) = drir("TGP_ID").ToString
                RunningTGP(0, 1) = "0"
                If RunningTGP(0, 0) = drir("TGP_ID").ToString _
                And RunningTGP(0, 1) = "0" Then
                    dgvInfos.Rows.Add(drir("TGP_ID"),
                    GetTGPName(drir("TGP_ID")), "", "", False, "H", "", "")
                    Dim cTCol As New DataGridViewTextBoxCell
                    dgvInfos.Rows(dgvInfos.RowCount - 1).Cells(4) = cTCol
                    dgvInfos.Rows(dgvInfos.RowCount - 1).Cells(2).ReadOnly = True
                    dgvInfos.Rows(dgvInfos.RowCount - 1).Cells(2).Style.BackColor =
                    dgvInfos.Rows(dgvInfos.RowCount - 1).Cells(1).Style.BackColor
                    dgvInfos.Rows(dgvInfos.RowCount - 1).Cells(1).Style.ForeColor = Color.Blue
                    RunningTGP(0, 1) = "1"
                    '
                    dgvInfos.Rows.Add(drir("Info_ID"),
                    GetTGPName(drir("Info_ID")), drir("Response"),
                    "", drir("Required"), "D", drir("TGP_ID"), drir("Accession_ID"))
                    Dim cCol As New DataGridViewCheckBoxCell
                    dgvInfos.Rows(dgvInfos.RowCount - 1).Cells(4) = cCol
                    dgvInfos.Rows(dgvInfos.RowCount - 1).Cells(2).ReadOnly = False
                    dgvInfos.Rows(dgvInfos.RowCount - 1).Cells(2).Style.BackColor = Color.White
                    dgvInfos.Rows(dgvInfos.RowCount - 1).Cells(1).Style.ForeColor = Color.Black
                ElseIf RunningTGP(0, 0) = drir("TGP_ID").ToString _
                And RunningTGP(0, 1) = "1" Then
                    dgvInfos.Rows.Add(drir("Info_ID"),
                    GetTGPName(drir("Info_ID")), drir("Response"),
                    "", drir("Required"), "D", drir("TGP_ID"), drir("Accession_ID"))
                    Dim cCol As New DataGridViewCheckBoxCell
                    dgvInfos.Rows(dgvInfos.RowCount - 1).Cells(4) = cCol
                    dgvInfos.Rows(dgvInfos.RowCount - 1).Cells(2).ReadOnly = False
                    dgvInfos.Rows(dgvInfos.RowCount - 1).Cells(2).Style.BackColor = Color.White
                    dgvInfos.Rows(dgvInfos.RowCount - 1).Cells(1).Style.ForeColor = Color.Black
                Else    'New
                    dgvInfos.Rows.Add(drir("TGP_ID"),
                    GetTGPName(drir("TGP_ID")), "", "", False, "H", "", "")
                    Dim cTCol As New DataGridViewTextBoxCell
                    dgvInfos.Rows(dgvInfos.RowCount - 1).Cells(4) = cTCol
                    dgvInfos.Rows(dgvInfos.RowCount - 1).Cells(2).ReadOnly = True
                    dgvInfos.Rows(dgvInfos.RowCount - 1).Cells(2).Style.BackColor =
                    dgvInfos.Rows(dgvInfos.RowCount - 1).Cells(1).Style.BackColor
                    dgvInfos.Rows(dgvInfos.RowCount - 1).Cells(1).Style.ForeColor = Color.Blue
                    RunningTGP(0, 1) = "1"
                    '
                    dgvInfos.Rows.Add(drir("Info_ID"),
                    GetTGPName(drir("Info_ID")), drir("Response"),
                    "", drir("Required"), "D", drir("TGP_ID"), drir("Accession_ID"))
                    Dim cCol As New DataGridViewCheckBoxCell
                    dgvInfos.Rows(dgvInfos.RowCount - 1).Cells(4) = cCol
                    dgvInfos.Rows(dgvInfos.RowCount - 1).Cells(2).ReadOnly = False
                    dgvInfos.Rows(dgvInfos.RowCount - 1).Cells(2).Style.BackColor = Color.White
                    dgvInfos.Rows(dgvInfos.RowCount - 1).Cells(1).Style.ForeColor = Color.Black
                    RunningTGP(0, 0) = drir("TGP_ID").ToString
                End If
            End While
        End If
        cnir.Close()
        cnir = Nothing
    End Sub

    Private Sub DisplayProvider(ByVal AccID As Long)
        txtProvider.Text = ""
        Dim Provider As String = ""
        Dim Address As String = ""
        Dim cndp As New SqlClient.SqlConnection(connString)
        cndp.Open()
        Dim cmddp As New SqlClient.SqlCommand("Select * from " &
        "Providers where ID in (Select OrderingProvider_ID " &
        "from Requisitions where ID = " & AccID & ")", cndp)
        cmddp.CommandType = CommandType.Text
        Dim drdp As SqlClient.SqlDataReader = cmddp.ExecuteReader
        If drdp.HasRows Then
            While drdp.Read
                If drdp("IsIndividual") <> 0 Then
                    Provider = Trim(drdp("LastName_BSN")) &
                    ", " & Trim(drdp("FirstName")) _
                    & IIf(drdp("Degree") Is DBNull.Value, "", " " _
                    & Trim(drdp("Degree")))
                Else
                    Provider = drdp("LastName_BSN")
                End If
                If drdp("Address_ID") IsNot DBNull.Value Then _
                Address = GetAddress(drdp("Address_ID"))
                txtProvider.Text = Provider & ", " & Address
            End While
        End If
        cndp.Close()
        cndp = Nothing
    End Sub
    Private Sub DisplayPatient(ByVal AccID As Long)
        txtPatient.Text = ""
        Dim PatName As String = ""
        Dim Address As String = ""
        Dim cndp As New SqlClient.SqlConnection(connString)
        cndp.Open()
        Dim cmddp As New SqlClient.SqlCommand("Select * from " &
        "Patients where ID in (Select Patient_ID from Requisitions " &
        "where ID = " & AccID & ")", cndp)
        cmddp.CommandType = CommandType.Text
        Dim drdp As SqlClient.SqlDataReader = cmddp.ExecuteReader
        If drdp.HasRows Then
            While drdp.Read
                PatName = Trim(drdp("LastName")) & ", " & Trim(drdp("FirstName")) &
                " DOB: " & Format(drdp("DOB"), SystemConfig.DateFormat) & " GENDER: " &
                Microsoft.VisualBasic.Left(drdp("Sex"), 1)
                If drdp("Address_ID") IsNot DBNull.Value Then _
                Address = GetAddress(drdp("Address_ID"))
                txtPatient.Text = PatName & IIf(Address <> "", ", " & Address, "")
            End While
        End If
        cndp.Close()
        cndp = Nothing
    End Sub
End Class

