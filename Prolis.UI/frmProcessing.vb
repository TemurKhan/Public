Imports System.Windows.Forms
Imports Microsoft.Data.SqlClient
Imports DataTable = System.Data.DataTable

Public Class frmProcessing

    Private CurrAcc As Integer
    Private Accessions As Integer
    'Private Shared RsA As New ADODB.Recordset
    Private dtRecords As DataTable
    Private IsDirty As Boolean = False

    Private Sub dtpDateFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        dtpDateFrom.BackColor = FCOLOR
    End Sub

    Private Sub dtpDateFrom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub dtpDateFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        dtpDateFrom.BackColor = NFCOLOR
    End Sub

    Private Sub dtpDateTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        dtpDateTo.BackColor = FCOLOR
    End Sub

    Private Sub dtpDateTo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub dtpDateTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        dtpDateTo.BackColor = NFCOLOR
    End Sub

    Private Sub txtAccFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccFrom.GotFocus
        txtAccFrom.BackColor = FCOLOR
    End Sub

    Private Sub txtAccFrom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAccFrom.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtAccFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccFrom.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAccFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccFrom.LostFocus
        txtAccFrom.BackColor = NFCOLOR
    End Sub

    Private Sub txtAccTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccTo.GotFocus
        txtAccTo.BackColor = FCOLOR
    End Sub

    Private Sub txtAccTo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAccTo.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtAccTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccTo.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAccTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccTo.LostFocus
        txtAccTo.BackColor = NFCOLOR
    End Sub

    Private Sub DisplayAccessionRecord0(ByVal AccID As Long)
        'Dim Template As String = ""
        'Dim CNP As New ADODB.Connection
        'CNP.Open(odbCS)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select * from Tests where SlideID <> '' and ID in (Select Test_ID from " &
        '"Acc_Results where Accession_ID = " & AccID & ")", CNP, ADODB.CursorTypeEnum.adOpenStatic,
        'ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then
        '    txtTest.Text = Rs.Fields("Name").Value
        '    txtTestID.Text = Rs.Fields("ID").Value
        '    txtAccID.Text = AccID.ToString
        '    Template = Rs.Fields("SlideID").Value
        'End If
        'Rs.Close()
        'dgvSlideIDs.Rows.Clear()
        'Rs.Open("Select * from Req_Slide where Accession_ID = " & AccID &
        '" and Test_ID = " & Val(txtTestID.Text), CNP, ADODB.CursorTypeEnum.adOpenStatic,
        'ADODB.LockTypeEnum.adLockReadOnly)
        'If Rs.BOF Then  'New
        '    'If InStr(Template, "yy") > 0 Then Template = _
        '    'Replace(Template, "yy", Format(Date.Today, "yy"))
        '    'If InStr(Template, "MM") > 0 Then Template = _
        '    'Replace(Template, "MM", Format(Date.Today, "MM"))
        '    'dgvSlideIDs.Rows.Add(Template & getNextSlideNo().ToString, True)
        '    dgvSlideIDs.Rows.Add("", True)
        '    'IsDirty = True
        'Else
        '    dgvSlideIDs.Rows.Add(Rs.Fields("SlideID").Value, False)
        'End If
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
    End Sub

    Private Sub DisplayAccessionRecord(ByVal AccID As Long)
        Dim Template As String = ""

        Using connection As New SqlConnection(connString)
            connection.Open()

            ' Retrieve test information
            Dim queryTest As String = "
            SELECT Name, ID, SlideID 
            FROM Tests 
            WHERE SlideID <> '' 
            AND ID IN (SELECT Test_ID FROM Acc_Results WHERE Accession_ID = @AccID)"

            Using commandTest As New SqlCommand(queryTest, connection)
                commandTest.Parameters.AddWithValue("@AccID", AccID)
                Using readerTest As SqlDataReader = commandTest.ExecuteReader()
                    If readerTest.Read() Then
                        txtTest.Text = readerTest("Name").ToString()
                        txtTestID.Text = readerTest("ID").ToString()
                        txtAccID.Text = AccID.ToString()
                        Template = readerTest("SlideID").ToString()
                    End If
                End Using
            End Using

            ' Clear existing slide IDs
            dgvSlideIDs.Rows.Clear()

            ' Retrieve slide IDs
            Dim querySlide As String = "
            SELECT SlideID 
            FROM Req_Slide 
            WHERE Accession_ID = @AccID 
            AND Test_ID = @TestID"

            Using commandSlide As New SqlCommand(querySlide, connection)
                commandSlide.Parameters.AddWithValue("@AccID", AccID)
                commandSlide.Parameters.AddWithValue("@TestID", Val(txtTestID.Text))
                Using readerSlide As SqlDataReader = commandSlide.ExecuteReader()
                    If readerSlide.HasRows Then
                        While readerSlide.Read()
                            dgvSlideIDs.Rows.Add(readerSlide("SlideID").ToString(), False)
                        End While
                    Else
                        dgvSlideIDs.Rows.Add("", True)
                    End If
                End Using
            End Using
        End Using
    End Sub

    Private Function getNextSlideNo0() As Long
        'Dim CNP As New ADODB.Connection
        'CNP.Open(odbCS)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select max(SlideNo) as LastID from Req_Slide", CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Rs.Fields("LastID").Value Is System.DBNull.Value Then
        '    getNextSlideNo = 1
        'Else
        '    getNextSlideNo = Rs.Fields("LastID").Value + 1
        'End If
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
    End Function

    Private Function getNextSlideNo() As Long
        Dim nextSlideNo As Long = 1 ' Default to 1 if no records exist

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT MAX(SlideNo) AS LastID FROM Req_Slide"

            Using command As New SqlCommand(query, connection)
                Dim result As Object = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    nextSlideNo = Convert.ToInt64(result) + 1
                End If
            End Using
        End Using

        Return nextSlideNo
    End Function


    Private Sub btnFirst_Click0(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFirst.Click
        'If IsDirty Then SaveSlides()
        'RsA.MoveFirst()
        'CurrAcc = 1
        'btnNext.Enabled = True
        'btnLast.Enabled = True
        'btnPrevious.Enabled = False
        'btnFirst.Enabled = False
        'txtNavStatus.Text = CurrAcc.ToString & " of " & Accessions.ToString
        'DisplayAccessionRecord(RsA.Fields("ID").Value)
    End Sub

    Private Sub btnFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFirst.Click
        If IsDirty Then SaveSlides()

        CurrAcc = 1
        btnNext.Enabled = True
        btnLast.Enabled = True
        btnPrevious.Enabled = False
        btnFirst.Enabled = False
        txtNavStatus.Text = $"{CurrAcc} of {Accessions}"

        DisplayAccessionRecord(dtRecords.Rows(CurrAcc - 1)("ID"))
    End Sub


    'Private Sub btnLast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLast.Click
    '    If IsDirty Then SaveSlides()
    '    RsA.MoveLast()
    '    CurrAcc = Accessions
    '    btnPrevious.Enabled = True
    '    btnFirst.Enabled = True
    '    btnNext.Enabled = False
    '    btnLast.Enabled = False
    '    txtNavStatus.Text = CurrAcc & " of " & Accessions
    '    DisplayAccessionRecord(RsA.Fields("ID").Value)
    'End Sub

    Private Sub btnLast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLast.Click
        If IsDirty Then SaveSlides()

        CurrAcc = dtRecords.Rows.Count
        btnPrevious.Enabled = True
        btnFirst.Enabled = True
        btnNext.Enabled = False
        btnLast.Enabled = False
        txtNavStatus.Text = $"{CurrAcc} of {Accessions}"

        DisplayAccessionRecord(dtRecords.Rows(CurrAcc - 1)("ID"))
    End Sub


    'Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
    '    If IsDirty Then SaveSlides()
    '    If RsA.State = 0 Then
    '        Dim CNP As New ADODB.Connection
    '        CNP.Open(odbCS)
    '        RsA.Open(RsA.Source.ToString(), CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

    '    End If

    '    RsA.MoveNext()
    '    CurrAcc += 1
    '    btnPrevious.Enabled = True
    '    btnFirst.Enabled = True
    '    If RsA.EOF Then
    '        RsA.MoveLast()
    '        CurrAcc = Accessions
    '        btnNext.Enabled = False
    '        btnLast.Enabled = False
    '    End If
    '    txtNavStatus.Text = CurrAcc & " of " & Accessions
    '    DisplayAccessionRecord(RsA.Fields("ID").Value)
    'End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If IsDirty Then SaveSlides()

        If CurrAcc < dtRecords.Rows.Count Then
            CurrAcc += 1
            btnPrevious.Enabled = True
            btnFirst.Enabled = True
        End If

        If CurrAcc = dtRecords.Rows.Count Then
            btnNext.Enabled = False
            btnLast.Enabled = False
        End If

        txtNavStatus.Text = $"{CurrAcc} of {Accessions}"
        DisplayAccessionRecord(dtRecords.Rows(CurrAcc - 1)("ID"))
    End Sub


    Private Sub SaveSlides()
        Dim SlideNo As Long
        Dim i As Integer
        Dim HasSlide As Boolean = False
        If dgvSlideIDs.RowCount > 0 Then
            For i = 0 To dgvSlideIDs.RowCount - 1
                If dgvSlideIDs.Rows(i).Cells(0).Value IsNot Nothing AndAlso
                dgvSlideIDs.Rows(i).Cells(0).Value.ToString <> "" Then
                    HasSlide = True
                    Exit For
                End If
            Next
        End If
        'If HasSlide = True And txtAccID.Text <> "" And txtTestID.Text <> "" Then
        '    Dim CNP As New ADODB.Connection
        '    CNP.Open(odbCS)
        '    For i = 0 To dgvSlideIDs.RowCount - 1
        '        If dgvSlideIDs.Rows(i).Cells(0).Value IsNot Nothing AndAlso
        '        dgvSlideIDs.Rows(i).Cells(0).Value.ToString <> "" Then
        '            Dim Rs As New ADODB.Recordset
        '            Rs.Open("Select * from Req_Slide where Accession_ID = " & Val(txtAccID.Text) &
        '            " and Test_ID = " & Val(txtTestID.Text), CNP, ADODB.CursorTypeEnum.adOpenDynamic,
        '            ADODB.LockTypeEnum.adLockOptimistic)
        '            If Rs.BOF Then Rs.AddNew()
        '            Rs.Fields("Accession_ID").Value = Val(txtAccID.Text)
        '            Rs.Fields("Test_ID").Value = Val(txtTestID.Text)
        '            Rs.Fields("SlideID").Value = Trim(dgvSlideIDs.Rows(i).Cells(0).Value.ToString)
        '            If IsNumeric(Microsoft.VisualBasic.Mid(Trim(dgvSlideIDs.Rows(i).Cells(0).Value.ToString), 4)) Then
        '                SlideNo = Val(Microsoft.VisualBasic.Mid(Trim(dgvSlideIDs.Rows(i).Cells(0).Value.ToString), 4))
        '                Rs.Fields("Slideno").Value = SlideNo
        '            Else
        '                Rs.Fields("SlideNo").Value = 0
        '            End If
        '            Rs.Update()
        '            Rs.Close()
        '            Rs = Nothing
        '        End If
        '    Next
        '    CNP.Close()
        '    CNP = Nothing
        'End If
        If HasSlide AndAlso txtAccID.Text <> "" AndAlso txtTestID.Text <> "" Then
            Using connection As New SqlConnection(connString)
                connection.Open()

                For x As Integer = 0 To dgvSlideIDs.RowCount - 1
                    If dgvSlideIDs.Rows(x).Cells(0).Value IsNot Nothing AndAlso dgvSlideIDs.Rows(x).Cells(0).Value.ToString() <> "" Then
                        Dim query As String = "SELECT COUNT(*) FROM Req_Slide WHERE Accession_ID = @AccID AND Test_ID = @TestID"

                        Dim recordExists As Boolean

                        Using checkCmd As New SqlCommand(query, connection)
                            checkCmd.Parameters.AddWithValue("@AccID", Val(txtAccID.Text))
                            checkCmd.Parameters.AddWithValue("@TestID", Val(txtTestID.Text))

                            recordExists = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0
                        End Using

                        Dim slideID As String = dgvSlideIDs.Rows(x).Cells(0).Value.ToString().Trim()
                        SlideNo = If(IsNumeric(Microsoft.VisualBasic.Mid(slideID, 4)), Val(Microsoft.VisualBasic.Mid(slideID, 4)), 0)

                        If recordExists Then
                            query = "UPDATE Req_Slide SET SlideID = @SlideID, SlideNo = @SlideNo WHERE Accession_ID = @AccID AND Test_ID = @TestID"
                        Else
                            query = "INSERT INTO Req_Slide (Accession_ID, Test_ID, SlideID, SlideNo) VALUES (@AccID, @TestID, @SlideID, @SlideNo)"
                        End If

                        Using cmd As New SqlCommand(query, connection)
                            cmd.Parameters.AddWithValue("@AccID", Val(txtAccID.Text))
                            cmd.Parameters.AddWithValue("@TestID", Val(txtTestID.Text))
                            cmd.Parameters.AddWithValue("@SlideID", slideID)
                            cmd.Parameters.AddWithValue("@SlideNo", slideNo)

                            cmd.ExecuteNonQuery()
                        End Using
                    End If
                Next
            End Using
        End If

        IsDirty = False
    End Sub

    Private Sub btnPrevious_Click0(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrevious.Click
        'If IsDirty Then SaveSlides()
        'RsA.MovePrevious()
        'CurrAcc -= 1
        'btnNext.Enabled = True
        'btnLast.Enabled = True
        'If RsA.BOF Then
        '    RsA.MoveFirst()
        '    CurrAcc = 1
        '    btnPrevious.Enabled = False
        '    btnFirst.Enabled = False
        'End If
        'txtNavStatus.Text = CurrAcc & " of " & Accessions
        'DisplayAccessionRecord(RsA.Fields("ID").Value)
    End Sub

    Private Sub btnPrevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrevious.Click
        If IsDirty Then SaveSlides()

        If CurrAcc > 1 Then
            CurrAcc -= 1
            btnNext.Enabled = True
            btnLast.Enabled = True
        End If

        If CurrAcc = 1 Then
            btnPrevious.Enabled = False
            btnFirst.Enabled = False
        End If

        txtNavStatus.Text = $"{CurrAcc} of {Accessions}"
        DisplayAccessionRecord(dtRecords.Rows(CurrAcc - 1)("ID"))
    End Sub


    Private Sub btnLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Dim sSQL As String = ""
        If (IsDate(dtpDateFrom.Text) Or IsDate(dtpDateTo.Text) Or
        txtAccFrom.Text <> "" Or txtAccTo.Text <> "") Then
            sSQL = "Select * from Requisitions where ID in (Select distinct Accession_ID " &
            "from Acc_Results where Test_ID in (Select ID from Tests where SlideID <> ''))"
            '
            If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                sSQL += " and AccessionDate between '" & Format(CDate(dtpDateFrom.Text),
                "MM/dd/yyyy HH:mm") & "' and '" & Format(CDate(dtpDateFrom.Text & " 23:59"),
                "MM/dd/yyyy HH:mm") & "'"
            ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                sSQL += " and AccessionDate between '" & Format(CDate(dtpDateTo.Text),
                "MM/dd/yyyy HH:mm") & "' and '" & Format(CDate(dtpDateTo.Text & " 23:59"),
                "MM/dd/yyyy HH:mm") & "'"
            ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                sSQL += " and AccessionDate between '" & Format(CDate(dtpDateFrom.Text),
                "MM/dd/yyyy HH:mm") & "' and '" & Format(CDate(dtpDateTo.Text & " 23:59"),
                "MM/dd/yyyy HH:mm") & "'"
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                sSQL += " and ID = " & Val(txtAccFrom.Text)
            ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                sSQL += " and ID = " & Val(txtAccTo.Text)
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                If Val(txtAccFrom.Text) < Val(txtAccTo.Text) Then
                    sSQL += " and ID >= " & Val(txtAccFrom.Text) & " and ID <= " _
                    & Val(txtAccTo.Text)
                ElseIf Val(txtAccFrom.Text) > Val(txtAccTo.Text) Then
                    sSQL += " and ID <= " & Val(txtAccFrom.Text) & " and ID >= " _
                    & Val(txtAccTo.Text)
                Else
                    sSQL += " and ID = " & Val(txtAccFrom.Text)
                End If
            End If
            '
            'If RsA.State = 1 Then RsA.Close()
            'Dim CNP As New ADODB.Connection
            'CNP.Open(odbCS)
            'RsA.Open(sSQL, CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            'If Not RsA.BOF Then
            '    RsA.MoveLast()
            '    RsA.MoveFirst()
            '    CurrAcc = 1
            '    Accessions = RsA.RecordCount
            '    txtNavStatus.Text = CurrAcc & " of " & Accessions
            '    If Accessions > 1 Then
            '        btnNext.Enabled = True
            '        btnLast.Enabled = True
            '    End If
            '    DisplayAccessionRecord(RsA.Fields("ID").Value)
            'Else
            '    txtNavStatus.Text = "" : btnFirst.Enabled = False : btnPrevious.Enabled = False
            '    btnNext.Enabled = False : btnLast.Enabled = False
            'End If
            'CNP.Close()
            'CNP = Nothing

            Using connection As New SqlConnection(connString)
                connection.Open()

                Using adapter As New SqlDataAdapter(sSQL, connection)
                    dtRecords.Clear() ' Ensure previous data is cleared
                    adapter.Fill(dtRecords) ' Populate DataTable
                End Using
            End Using

            If dtRecords.Rows.Count > 0 Then
                CurrAcc = 1
                Accessions = dtRecords.Rows.Count
                txtNavStatus.Text = $"{CurrAcc} of {Accessions}"

                If Accessions > 1 Then
                    btnNext.Enabled = True
                    btnLast.Enabled = True
                End If

                DisplayAccessionRecord(dtRecords.Rows(0)("ID"))
            Else
                txtNavStatus.Text = ""
                btnFirst.Enabled = False
                btnPrevious.Enabled = False
                btnNext.Enabled = False
                btnLast.Enabled = False
            End If


            txtAccFrom.Text = ""
            txtAccTo.Text = ""
            'dtpDateFrom.Text = ""
            'dtpDateTo.Text = ""
            ClearDateTimePicker(dtpDateFrom)
            ClearDateTimePicker(dtpDateTo)
        End If
    End Sub

    Private Sub dgvSlideIDs_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSlideIDs.CellEndEdit
        If dgvSlideIDs.Rows(e.RowIndex).Cells(0).Value IsNot Nothing AndAlso
        dgvSlideIDs.Rows(e.RowIndex).Cells(0).Value.ToString <> "" Then IsDirty = True
    End Sub

    Private Sub frmProcessing_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If IsDirty Then SaveSlides()
    End Sub

    Private Sub frmProcessing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblFrom.Text += " (" & SystemConfig.DateFormat & ")"
        lblTo.Text += " (" & SystemConfig.DateFormat & ")"

        ConfigureDateTimePicker(dtpDateFrom)
        ConfigureDateTimePicker(dtpDateTo)
    End Sub

    'Private Sub dtpDateFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If UserEnteredText(dtpDateFrom) <> "" Then
    '        If Not IsDate(dtpDateFrom.Text) Then
    '            MsgBox("Invalid date", MsgBoxStyle.Critical, "Prolis")
    '            dtpDateFrom.Text = ""
    '            dtpDateFrom.Focus()
    '        End If
    '    End If
    'End Sub

    'Private Sub dtpDateTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If UserEnteredText(dtpDateTo) <> "" Then
    '        If Not IsDate(dtpDateTo.Text) Then
    '            MsgBox("Invalid date", MsgBoxStyle.Critical, "Prolis")
    '            dtpDateTo.Text = ""
    '            dtpDateTo.Focus()
    '        End If
    '    End If
    'End Sub
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
