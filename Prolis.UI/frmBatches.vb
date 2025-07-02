Imports System.Windows.Forms
Imports Microsoft.Data.SqlClient
Imports DataRow = System.Data.DataRow
Imports DataTable = System.Data.DataTable

Public Class frmBatches
    Private SearchMode As Boolean = False
    Private Tests(0) As Long
    Private Accessions(0) As Long

    Private Sub frmBatches_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'If CN.BeginTrans = True Then CN.RollbackTrans()
    End Sub

    Private Sub frmBatches_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CN.Open("DSN=ProlisQC")
        lblDate.Text += " (" & SystemConfig.DateFormat & ")"
        dtpDate.Format = DateTimePickerFormat.Custom
        dtpDate.CustomFormat = SystemConfig.DateFormat
        dtpDate.Value = Date.Today
        PopulateAnas()
        PopulateTechs()
        Dim i As Integer
        Dim Itemx As MyList
        For i = 0 To cmbTech.Items.Count - 1
            Itemx = cmbTech.Items(i)
            If Itemx.ItemData = ThisUser.ID Then
                cmbTech.SelectedIndex = i
                Exit For
            End If
        Next
        txtTime.Text = Format(Date.Now, "HH:mm")
        'CN.BeginTrans()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub PopulateAnas()
        cmbAnas.Items.Clear()

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "
            SELECT DISTINCT a.ID, a.Name 
            FROM Anas a
            INNER JOIN Ana_Test b ON a.ID = b.Ana_ID
            INNER JOIN Acc_Results c ON b.Test_ID = c.Test_ID
            INNER JOIN Requisitions d ON d.ID = c.Accession_ID
            WHERE d.Received <> 0 AND c.Released = 0 AND c.Run_ID IS NULL"

            Using command As New SqlCommand(query, connection)
                command.CommandTimeout = 50

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.HasRows Then
                        While reader.Read()
                            cmbAnas.Items.Add(New MyList(reader("Name").ToString(), reader("ID")))
                        End While
                    End If
                End Using
            End Using
        End Using
    End Sub
    Private Sub PopulateTechs()
        cmbTech.Items.Clear()

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT ID, FullName FROM Users WHERE Testing <> 0"
            Using command As New SqlCommand(query, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        cmbTech.Items.Add(New MyList(reader("FullName").ToString(), reader("ID")))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub chkEditNew_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEditNew.CheckedChanged
        ClearForm()
        If chkEditNew.Checked = True Then
            chkEditNew.Text = "Edit"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Edit.ico")
            cmbAnas.Enabled = False
            'dtpDate.Enabled = False
            txtTime.ReadOnly = True
            cmbAnas.SelectedIndex = -1
            PopulateRunsByDate(dtpDate.Value)
        Else
            chkEditNew.Text = "New"
            chkEditNew.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\New.ico")
            cmbAnas.Enabled = True
            dtpDate.Enabled = True
            txtTime.ReadOnly = False
            cmbRunName.SelectedIndex = -1
            cmbRunName.Items.Clear()
            PopulateAnas()
        End If
    End Sub

    Private Sub PopulateRunsByDate(ByVal RunDate As Date)
        cmbRunName.Items.Clear()

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT ID, Name FROM Runs WHERE Validated = 0 AND RunDate = @RunDate ORDER BY RunDate DESC"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@RunDate", RunDate)

                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        cmbRunName.Items.Add(New MyList(reader("Name").ToString(), reader("ID")))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Function PopulateUnResultedRuns(ByVal AnaID As Integer, ByVal RunDate As Date) As Integer
        If Not chkEditNew.Checked Then cmbRunName.Items.Clear()
        Dim i As Integer = 0

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "
            SELECT ID, Name 
            FROM Runs 
            WHERE Analysis_ID = @AnaID AND Validated = 0 
            AND RunDate BETWEEN @StartDate AND @EndDate 
            ORDER BY RunDate DESC"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@AnaID", AnaID)
                command.Parameters.AddWithValue("@StartDate", RunDate.Date)
                command.Parameters.AddWithValue("@EndDate", RunDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59))

                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        cmbRunName.Items.Add(New MyList(reader("Name").ToString(), reader("ID")))
                        i += 1
                    End While
                End Using
            End Using
        End Using

        Return i
    End Function
    Private Sub ClearForm()
        Dim i As Integer
        Dim ItemX As MyList
        cmbAnas.SelectedIndex = -1
        'dtpDate.Value = Date.Today
        'txtTime.Text = Format(Date.Now, "hh:mm tt")
        cmbRunName.Text = ""
        cmbRunName.SelectedIndex = -1
        'cmbRunName.Items.Clear()
        For i = 0 To cmbTech.Items.Count - 1
            ItemX = cmbTech.SelectedItem
            If ItemX.ItemData = ThisUser.ID Then
                cmbTech.SelectedIndex = i
                Exit For
            End If
        Next
        dgvAccCtl.Rows.Clear()
    End Sub

    Private Function GetNextRunID() As Long
        Dim nextRunID As Long = 1

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT MAX(ID) AS LastID FROM Runs"
            Using command As New SqlCommand(query, connection)
                Dim result As Object = command.ExecuteScalar()
                If result IsNot DBNull.Value AndAlso result IsNot Nothing Then
                    nextRunID = Convert.ToInt64(result) + 1
                End If
            End Using
        End Using

        Return nextRunID
    End Function

    Private Sub UpdateReq_Tests()
        Dim ItemX As MyList = cmbAnas.SelectedItem
        Dim sSQL As String
        Dim Accessions As String = ""
        Dim i As Integer
        For i = 0 To dgvAccCtl.RowCount - 1
            If dgvAccCtl.Rows(i).Cells(1).Value > 0 And
            dgvAccCtl.Rows(i).Cells(5).Value = 0 Then
                Accessions = Accessions & CStr(dgvAccCtl.Rows(i).Cells(1).Value) & ", "
            End If
        Next
        Accessions = Accessions.Substring(0, Len(Accessions) - 2)
        sSQL = "Update Req_Tests set Batched = -1 where Accession_ID in (" & Accessions &
        ")"
        ExecuteSqlProcedure(sSQL)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If cmbAnas.SelectedIndex <> -1 And IsDate(txtTime.Text) _
        And cmbRunName.SelectedIndex <> -1 And cmbTech.SelectedIndex <> -1 _
        And RunContentsValid() = True Then
            Dim ItemX As MyList = cmbRunName.SelectedItem
            SaveRun(ItemX.ItemData)
            SaveQCDetail(ItemX.ItemData)
            SaveAccDetail(ItemX.ItemData)
            'UpdateReq_Tests()
            PopulateAnas()
            If chkEditNew.Checked = False Then  'New
                cmbRunName.SelectedIndex = -1
                cmbRunName.Items.Clear()
            Else
                PopulateRunsByDate(dtpDate.Value)
            End If
            ClearForm()
            If chkDeleteOrphan.Checked = True Then DeleteOrphanRuns()
            btnSave.Enabled = False
            btnDelete.Enabled = False
        Else
            MsgBox("The batch can not be saved because of either improper Batch " _
            & "Run Name, ID, no Tech selection, missing controls or Accessions")
        End If
    End Sub

    Private Sub DeleteOrphanRuns()
        ExecuteSqlProcedure("Delete from Runs where ID not in (Select Run_ID from Acc_Results)")
    End Sub

    Private Function RunContentsValid() As Boolean
        Dim i As Integer
        Dim Controls As Integer = 0
        Dim Accessions As Integer = 0
        For i = 0 To dgvAccCtl.RowCount - 1
            If dgvAccCtl.Rows(i).Cells(3).Value = 0 Then
                Accessions = Accessions + 1
            Else
                If dgvAccCtl.Rows(i).Cells(1).Value > 0 Then
                    Accessions = Accessions + 1
                    Controls = Controls + 1
                Else
                    Controls = Controls + 1
                End If
            End If
        Next
        If Accessions > 0 And Controls > 0 Then
            RunContentsValid = True
        Else
            RunContentsValid = False
        End If
    End Function

    Private Sub SaveRun(ByVal RunID As Long)
        Dim ItemX As MyList = DirectCast(cmbAnas.SelectedItem, MyList)

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "
            IF EXISTS (SELECT 1 FROM Runs WHERE ID = @RunID)
                UPDATE Runs SET 
                    Name = @Name, 
                    Analysis_ID = @AnalysisID, 
                    Validated = @Validated, 
                    RunDate = @RunDate, 
                    Tech_ID = @TechID, 
                    LastEditedOn = @LastEditedOn, 
                    EditedBy = @EditedBy 
                WHERE ID = @RunID
            ELSE
                INSERT INTO Runs (ID, Name, Analysis_ID, Validated, RunDate, Tech_ID, LastEditedOn, EditedBy)
                VALUES (@RunID, @Name, @AnalysisID, @Validated, @RunDate, @TechID, @LastEditedOn, @EditedBy)"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@RunID", RunID)
                command.Parameters.AddWithValue("@Name", Trim(cmbRunName.SelectedItem.ToString()))
                command.Parameters.AddWithValue("@AnalysisID", ItemX.ItemData)
                command.Parameters.AddWithValue("@Validated", False)
                command.Parameters.AddWithValue("@RunDate", CDate(Format(dtpDate.Value, SystemConfig.DateFormat) & " " & txtTime.Text))
                command.Parameters.AddWithValue("@TechID", ThisUser.ID)
                command.Parameters.AddWithValue("@LastEditedOn", Date.Now)
                command.Parameters.AddWithValue("@EditedBy", ThisUser.ID)

                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub
    Private Function GetValValue(ByVal Ana_ID As Integer) As Boolean
        Dim result As Boolean = False

        Using connection As New SqlConnection(connString)
            connection.Open()
            Dim query As String = "SELECT Validaters FROM Anas WHERE ID = @Ana_ID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Ana_ID", Ana_ID)

                Dim value As Object = command.ExecuteScalar()
                If value IsNot DBNull.Value AndAlso Convert.ToInt32(value) = 0 Then
                    result = True
                End If
            End Using
        End Using

        Return result
    End Function
    Private Function GetControlTests(ByVal Ana_ID As Integer) As DataTable
        Dim sSQL As String
        If Not chkEditNew.Checked Then
            sSQL = "SELECT * FROM Ana_Test WHERE Test_ID IN (SELECT Test_ID FROM Acc_Results WHERE Released = 0 AND Accession_ID IN ("
        Else
            sSQL = "SELECT * FROM Ana_Test WHERE Test_ID IN (SELECT Test_ID FROM Acc_Results WHERE Accession_ID IN ("
        End If

        Dim accessionIDs As List(Of String) = dgvAccCtl.Rows.Cast(Of DataGridViewRow) _
        .Where(Function(row) row.Cells(1).Value > 0 AndAlso Not Convert.ToBoolean(row.Cells(5).Value)) _
        .Select(Function(row) row.Cells(1).Value.ToString()) _
        .ToList()

        If accessionIDs.Count = 0 Then Return New DataTable()

        sSQL &= String.Join(", ", accessionIDs) & ")) AND Ana_ID = @Ana_ID"

        Using connection As New SqlConnection(connString)
            connection.Open()
            Using command As New SqlCommand(sSQL, connection)
                command.Parameters.AddWithValue("@Ana_ID", Ana_ID)

                Using adapter As New SqlDataAdapter(command)
                    Dim dataTable As New DataTable()
                    adapter.Fill(dataTable)
                    Return dataTable
                End Using
            End Using
        End Using
    End Function
    Private Sub SaveQCDetail(ByVal Run_ID As Integer)
        Dim ItemX As MyList = DirectCast(cmbAnas.SelectedItem, MyList)
        Dim controlTests As Data.DataTable = GetControlTests(ItemX.ItemData)
        Dim badTests As New List(Of String)

        Using connection As New SqlConnection(connString)
            connection.Open()

            For Each row As DataGridViewRow In dgvAccCtl.Rows
                If Convert.ToInt32(row.Cells(3).Value) <> 0 AndAlso Not Convert.ToBoolean(row.Cells(5).Value) Then
                    For Each testRow As DataRow In controlTests.Rows
                        Dim controlID As Integer = If(Convert.ToInt32(row.Cells(1).Value) < 0, -Convert.ToInt32(row.Cells(1).Value), Convert.ToInt32(row.Cells(1).Value))

                        Dim query As String = "IF NOT EXISTS (SELECT 1 FROM QC_Results WHERE Run_ID = @Run_ID AND Control_ID = @Control_ID AND Test_ID = @Test_ID) " &
                                          "INSERT INTO QC_Results (Run_ID, Control_ID, Test_ID, Released) VALUES (@Run_ID, @Control_ID, @Test_ID, 0)"

                        Using command As New SqlCommand(query, connection)
                            command.Parameters.AddWithValue("@Run_ID", Run_ID)
                            command.Parameters.AddWithValue("@Control_ID", controlID)
                            command.Parameters.AddWithValue("@Test_ID", Convert.ToInt32(testRow("Test_ID")))

                            command.ExecuteNonQuery()
                        End Using

                        If Not badTests.Contains(testRow("Test_ID").ToString()) Then
                            badTests.Add(testRow("Test_ID").ToString())
                        End If
                    Next

                    ' Clean up RUN_CONTROL table
                    Using commandDelete As New SqlCommand("DELETE FROM RUN_CONTROL WHERE Run_ID = @Run_ID AND Control_ID = @Control_ID", connection)
                        commandDelete.Parameters.AddWithValue("@Run_ID", Run_ID)
                        commandDelete.Parameters.AddWithValue("@Control_ID", row.Cells(1).Value)
                        commandDelete.ExecuteNonQuery()
                    End Using

                    ' Insert into RUN_CONTROL table if necessary
                    If Not String.IsNullOrEmpty(row.Cells(6).Value?.ToString()) AndAlso IsDate(row.Cells(7).Value) Then
                        Dim queryInsert As String = "INSERT INTO RUN_CONTROL (Run_ID, Control_ID, Lot, ExpireDate) VALUES (@Run_ID, @Control_ID, @Lot, @ExpireDate)"
                        Using commandInsert As New SqlCommand(queryInsert, connection)
                            commandInsert.Parameters.AddWithValue("@Run_ID", Run_ID)
                            commandInsert.Parameters.AddWithValue("@Control_ID", row.Cells(1).Value)
                            commandInsert.Parameters.AddWithValue("@Lot", row.Cells(6).Value.ToString().Trim())
                            commandInsert.Parameters.AddWithValue("@ExpireDate", Convert.ToDateTime(row.Cells(7).Value))

                            commandInsert.ExecuteNonQuery()
                        End Using
                    End If
                End If
            Next

            ' Clean up QC_Results table
            If badTests.Count > 0 Then
                Dim queryCleanQC As String = "DELETE FROM QC_Results WHERE Run_ID = @Run_ID AND Test_ID NOT IN (" & String.Join(", ", badTests) & ")"
                Using commandCleanQC As New SqlCommand(queryCleanQC, connection)
                    commandCleanQC.Parameters.AddWithValue("@Run_ID", Run_ID)
                    commandCleanQC.ExecuteNonQuery()
                End Using
            End If
        End Using
    End Sub
    Private Function GetLotID(ByVal AnaID As Integer, ByVal ControlID As Long) As Long
        Dim lotID As Long = -1

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT ID FROM Lots WHERE Ana_ID = @AnaID AND Control_ID = @ControlID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@AnaID", AnaID)
                command.Parameters.AddWithValue("@ControlID", ControlID)

                Dim result As Object = command.ExecuteScalar()
                If result IsNot DBNull.Value AndAlso result IsNot Nothing Then
                    lotID = Convert.ToInt64(result)
                End If
            End Using
        End Using

        Return lotID
    End Function
    Private Sub SaveAccDetail(ByVal Run_ID As Long)
        Dim i As Integer
        Dim ItemX As MyList = cmbAnas.SelectedItem
        For i = 0 To dgvAccCtl.RowCount - 1
            If dgvAccCtl.Rows(i).Cells(1).Value > 0 And
            dgvAccCtl.Rows(i).Cells(5).Value = False Then 'Accessioned and not excluded
                ExecuteSqlProcedure("Update Acc_Results Set Run_ID = " & Run_ID & " where " _
                & "Accession_ID = " & dgvAccCtl.Rows(i).Cells(1).Value & " and " &
                "Test_ID in (Select Test_ID from Ana_Test where Ana_ID = " &
                ItemX.ItemData & ")")
            End If
        Next
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If cmbRunName.SelectedIndex <> -1 Then
            Dim Retval As Integer
            Dim ItemX As MyList = cmbRunName.SelectedItem
            Dim ItemA As MyList = cmbAnas.SelectedItem
            If Not RunValidated(ItemX.ItemData) Then
                If ThisUser.Hard_Deletion = True Then
                    Retval = MsgBox("Are you firm to delete this batch?", MsgBoxStyle.Question + MsgBoxStyle.YesNo)
                    If Retval = vbYes Then
                        ExecuteSqlProcedure("Update Req_Tests set Batched = 0 where Accession_ID in" _
                        & "(Select Accession_ID from Acc_Results where Run_ID = " &
                        ItemX.ItemData & ")")
                        ExecuteSqlProcedure("Delete from acc_Results where Run_ID = " &
                        ItemX.ItemData)
                        ExecuteSqlProcedure("Delete from QC_Results where Run_ID = " &
                        ItemX.ItemData)
                        ExecuteSqlProcedure("Delete from Runs where ID = " & ItemX.ItemData)
                        cmbRunName.Items.Clear()
                        If chkEditNew.Checked = True Then
                            PopulateRunsByDate(dtpDate.Value)
                        End If
                        ClearForm()
                        btnSave.Enabled = False
                        btnDelete.Enabled = False
                    End If
                Else
                    MsgBox("You are not allowed to delete any batch.", MsgBoxStyle.Critical, "Prolis")
                End If
            Else
                MsgBox("Validated batches can not be deleted. If you must delete this batch, invalidate" _
                & " the batch by deleting results of all QC levels first and delete the batch.", MsgBoxStyle.Information, "Prolis")
            End If
        End If
    End Sub

    Private Function RunValidated(ByVal Run_ID As Long) As Boolean
        Dim isValidated As Boolean = False

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT Validated FROM Runs WHERE ID = @RunID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@RunID", Run_ID)

                Dim result As Object = command.ExecuteScalar()
                If result IsNot DBNull.Value AndAlso Convert.ToBoolean(result) Then
                    isValidated = True
                End If
            End Using
        End Using

        Return isValidated
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Function PopulateNewRun() As Long
        Dim RunID As Long = 1
        If cmbAnas.SelectedIndex <> -1 Then
            Dim ItemX As MyList = cmbAnas.SelectedItem
            Dim ItemT As MyList = cmbTech.SelectedItem
            RunID = GetNextRunID()
            'CN.Execute("INSERT INTO Runs (ID, Name, Analysis_ID, RunDate, Tech_ID, " & _
            '"Validated, LastEditedOn, EditedBy) VALUES (" & RunID & ", '" & _
            'cmbAnas.Text & "-" & Format(dtpDate.Value, "MM/dd/yyyy hh:mm tt") & "', " & _
            'ItemX.ItemData & ", '" & dtpDate.Value & "', " & ItemT.ItemData & ", " & _
            '"0, '" & Date.Today & "', " & ThisUser.ID & ")")
            '
            cmbRunName.Items.Add(New MyList(cmbAnas.Text & "-" &
            Format(dtpDate.Value, SystemConfig.DateFormat & " HH:mm"), RunID))
        End If
        Return RunID
    End Function

    Private Sub cmbAnas_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAnas.SelectedIndexChanged
        If cmbAnas.SelectedIndex <> -1 Then
            If chkEditNew.Checked = False Then
                cmbRunName.Text = ""
                cmbRunName.SelectedIndex = -1
                Dim ItemX As MyList = cmbAnas.SelectedItem
                PopulateUnResultedRuns(ItemX.ItemData, dtpDate.Value)
                PopulateNewRun()
            End If
        End If
    End Sub

    Private Sub NewRunControls()
        Dim ItemX As MyList = DirectCast(cmbAnas.SelectedItem, MyList)
        dgvAccCtl.Rows.Clear()

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT Control_ID, ControlName, Lot, ExpireDate FROM Ana_Control WHERE Ana_ID = @Ana_ID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Ana_ID", ItemX.ItemData)

                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        dgvAccCtl.Rows.Add(
                        dgvAccCtl.RowCount + 1,
                        Convert.ToInt32(reader("Control_ID")) * -1,
                        reader("ControlName").ToString(),
                        -1, 0, 0,
                        If(reader("Lot") Is DBNull.Value, "", reader("Lot").ToString()),
                        If(reader("ExpireDate") Is DBNull.Value, "", Convert.ToDateTime(reader("ExpireDate")).ToString(SystemConfig.DateFormat))
                    )

                        Dim lastRow As DataGridViewRow = dgvAccCtl.Rows(dgvAccCtl.RowCount - 1)
                        lastRow.Cells(3).ReadOnly = True
                        lastRow.Cells(4).ReadOnly = True
                        lastRow.Cells(5).ReadOnly = True
                    End While
                End Using
            End Using
        End Using
    End Sub
    Private Sub NewRunAccessions()
        Dim ItemX As MyList = DirectCast(cmbAnas.SelectedItem, MyList)
        dgvAccCtl.Rows.Clear()

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "
            SELECT DISTINCT Accession_ID 
            FROM Acc_Results 
            WHERE Released = 0 AND Run_ID IS NULL 
            AND Test_ID IN (SELECT Test_ID FROM Ana_Test WHERE Ana_ID = @Ana_ID)"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Ana_ID", ItemX.ItemData)

                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim accessionID As Integer = Convert.ToInt32(reader("Accession_ID"))

                        If Not AccessionInTheList(accessionID) Then
                            dgvAccCtl.Rows.Add(
                            dgvAccCtl.RowCount + 1,
                            accessionID,
                            accessionID.ToString(), 0, 0, 0, "", ""
                        )

                            Dim lastRow As DataGridViewRow = dgvAccCtl.Rows(dgvAccCtl.RowCount - 1)
                            lastRow.Cells(3).ReadOnly = False
                            lastRow.Cells(4).ReadOnly = True
                            lastRow.Cells(5).ReadOnly = False
                            lastRow.Cells(6).ReadOnly = True
                            lastRow.Cells(7).ReadOnly = True
                        End If
                    End While
                End Using
            End Using
        End Using
    End Sub
    Private Function AccessionInTheList(ByVal Accession_ID As Long) As Boolean
        Dim i As Integer
        Dim InTheList As Boolean = False
        For i = 0 To dgvAccCtl.RowCount - 1
            If dgvAccCtl.Rows(i).Cells(1).Value = Accession_ID Then
                InTheList = True
                Exit For
            End If
        Next
        AccessionInTheList = InTheList
    End Function

    Private Sub Populate_Acc_Result_File()
        Dim ItemX As MyList = DirectCast(cmbRunName.SelectedItem, MyList)

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT Accession_ID, TGP_ID, TGP_Type FROM Req_TGP 
                               WHERE Accession_ID NOT IN 
                               (SELECT Accession_ID FROM Acc_Results WHERE Run_ID = @RunID)"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@RunID", ItemX.ItemData)

                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim accessionID As Integer = Convert.ToInt32(reader("Accession_ID"))
                        Dim tgpID As Integer = Convert.ToInt32(reader("TGP_ID"))
                        Dim tgpType As String = reader("TGP_Type").ToString()

                        If tgpType = "T" Then
                            If Not TestInTheArray(accessionID, tgpID) Then
                                AddToArrays(accessionID, tgpID)
                            End If
                        ElseIf tgpType = "G" Then
                            PopulateTestsFromGroup(connection, accessionID, tgpID)
                        Else
                            PopulateTestsFromProfile(connection, accessionID, tgpID)
                        End If
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub PopulateTestsFromGroup(ByVal connection As SqlConnection, ByVal accessionID As Integer, ByVal groupID As Integer)
        Dim query As String = "SELECT Test_ID FROM Group_Test WHERE Group_ID = @GroupID"

        Using command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@GroupID", groupID)

            Using reader As SqlDataReader = command.ExecuteReader()
                While reader.Read()
                    Dim testID As Integer = Convert.ToInt32(reader("Test_ID"))
                    If Not TestInTheArray(accessionID, testID) Then
                        AddToArrays(accessionID, testID)
                    End If
                End While
            End Using
        End Using
    End Sub

    Private Sub PopulateTestsFromProfile(ByVal connection As SqlConnection, ByVal accessionID As Integer, ByVal profileID As Integer)
        Dim query As String = "SELECT GrpTst_ID FROM Prof_GrpTst WHERE Profile_ID = @ProfileID"

        Using command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@ProfileID", profileID)

            Using reader As SqlDataReader = command.ExecuteReader()
                While reader.Read()
                    Dim grpTstID As Integer = Convert.ToInt32(reader("GrpTst_ID"))
                    If GetTGPType(grpTstID) = "T" Then
                        If Not TestInTheArray(accessionID, grpTstID) Then
                            AddToArrays(accessionID, grpTstID)
                        End If
                    Else
                        PopulateTestsFromGroup(connection, accessionID, grpTstID)
                    End If
                End While
            End Using
        End Using
    End Sub

    Private Sub AddToArrays(ByVal accessionID As Integer, ByVal testID As Integer)
        If Accessions.Length > 0 AndAlso Accessions(UBound(Accessions)) <> 0 Then
            ReDim Preserve Accessions(UBound(Accessions) + 1)
        End If

        If Tests.Length > 0 AndAlso Tests(UBound(Tests)) <> 0 Then
            ReDim Preserve Tests(UBound(Tests) + 1)
        End If

        Accessions(UBound(Accessions)) = accessionID
        Tests(UBound(Tests)) = testID
    End Sub
    Private Function TestInTheArray(ByVal Accession_ID As Long, ByVal Test_ID As Integer) As Boolean
        Dim InTheArray As Boolean = False
        Dim at As Long
        For at = LBound(Tests) To UBound(Tests)
            If Accessions(at) = Accession_ID And Tests(at) = Test_ID Then
                InTheArray = True
                Exit For
            End If
        Next
        InTheArray = TestInTheArray
        Return InTheArray
    End Function

    Private Sub txtAccID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccID.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAccID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccID.Validated
        If txtAccID.Text <> "" And txtSampleID.Text = "" Then
            txtSampleID.Text = txtAccID.Text
            btnAddtoList.Enabled = True
        Else
            btnAddtoList.Enabled = False
        End If

    End Sub

    Private Sub txtSampleID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSampleID.Validated
        If txtAccID.Text <> "" And txtSampleID.Text <> "" Then
            btnAddtoList.Enabled = True
        Else
            btnAddtoList.Enabled = False
        End If

    End Sub

    Private Sub btnAddtoList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddtoList.Click
        If cmbRunName.SelectedIndex <> -1 And dgvAccCtl.RowCount > 0 Then
            dgvAccCtl.Rows.Add(dgvAccCtl.RowCount + 1,
            Val(txtAccID.Text), txtSampleID.Text,
            chkCtl.Checked, chkRep.Checked, chkExclude.Checked, "")
            txtAccID.Text = ""
            txtSampleID.Text = ""
            chkCtl.Checked = False
            chkRep.Checked = False
            chkExclude.Checked = False
            btnAddtoList.Enabled = False
        End If
    End Sub

    Private Sub cmbRunName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbRunName.SelectedIndexChanged
        If cmbRunName.SelectedIndex <> -1 Then
            Dim ItemX As MyList = cmbRunName.SelectedItem
            Populate_RunData(ItemX.ItemData)
            If dgvAccCtl.RowCount = 0 Then NewRunControls()
            If chkEditNew.Checked = False Then NewRunAccessions()
            DeleteOrphanRuns()
            btnSave.Enabled = True
            btnDelete.Enabled = True
        Else
            dgvAccCtl.Rows.Clear()
        End If
    End Sub
    Private Function GetAnalysis(ByVal RunID As Long) As MyList
        Dim ItemX As MyList

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "
            SELECT Name, ID 
            FROM Anas 
            WHERE ID IN (SELECT Analysis_ID FROM Runs WHERE ID = @RunID)"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@RunID", RunID)

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.HasRows Then
                        reader.Read()
                        ItemX = New MyList(reader("Name").ToString(), Convert.ToInt64(reader("ID")))
                    Else
                        ItemX = New MyList("", -1)
                    End If
                End Using
            End Using
        End Using

        Return ItemX
    End Function

    Private Sub Populate_RunData(ByVal Run_ID As Long)
        dgvAccCtl.Rows.Clear()
        Dim ItemX As MyList

        If Not chkEditNew.Checked Then
            ItemX = DirectCast(cmbAnas.SelectedItem, MyList)
        Else
            ItemX = GetAnalysis(Run_ID)
            cmbAnas.Items.Clear()
            cmbAnas.Items.Add(ItemX)
            cmbAnas.SelectedIndex = 0
        End If

        Using connection As New SqlConnection(connString)
            connection.Open()

            ' Retrieve RUN_CONTROL data
            Dim queryControl As String = "SELECT DISTINCT Control_ID, Lot, ExpireDate FROM RUN_CONTROL WHERE Run_ID = @Run_ID"
            Using commandControl As New SqlCommand(queryControl, connection)
                commandControl.Parameters.AddWithValue("@Run_ID", Run_ID)

                Using readerControl As SqlDataReader = commandControl.ExecuteReader()
                    While readerControl.Read()
                        dgvAccCtl.Rows.Add(
                        dgvAccCtl.RowCount + 1,
                        Convert.ToInt32(readerControl("Control_ID")) * -1,
                        GetControlName(ItemX.ItemData, Convert.ToInt32(readerControl("Control_ID"))),
                        -1, 0, 0,
                        If(readerControl("Lot") Is DBNull.Value, "", readerControl("Lot").ToString()),
                        If(readerControl("ExpireDate") Is DBNull.Value, Format(Date.Today, SystemConfig.DateFormat),
                           Format(Convert.ToDateTime(readerControl("ExpireDate")), SystemConfig.DateFormat))
                    )

                        Dim lastRow As DataGridViewRow = dgvAccCtl.Rows(dgvAccCtl.RowCount - 1)
                        lastRow.Cells(3).ReadOnly = True
                        lastRow.Cells(4).ReadOnly = True
                        lastRow.Cells(5).ReadOnly = False
                    End While
                End Using
            End Using

            ' Retrieve Acc_Results data
            Dim queryResults As String = "
            SELECT DISTINCT Accession_ID 
            FROM Acc_Results 
            WHERE Run_ID = @Run_ID AND Test_ID IN 
            (SELECT Test_ID FROM Ana_Test WHERE Ana_ID = @Ana_ID)"
            Using commandResults As New SqlCommand(queryResults, connection)
                commandResults.Parameters.AddWithValue("@Run_ID", Run_ID)
                commandResults.Parameters.AddWithValue("@Ana_ID", ItemX.ItemData)

                Using readerResults As SqlDataReader = commandResults.ExecuteReader()
                    While readerResults.Read()
                        dgvAccCtl.Rows.Add(
                        dgvAccCtl.RowCount + 1,
                        Convert.ToInt32(readerResults("Accession_ID")),
                        readerResults("Accession_ID").ToString(),
                        0, 0, 0, "", ""
                    )

                        Dim lastRow As DataGridViewRow = dgvAccCtl.Rows(dgvAccCtl.RowCount - 1)
                        lastRow.Cells(3).ReadOnly = False
                        lastRow.Cells(4).ReadOnly = False
                        lastRow.Cells(5).ReadOnly = False
                    End While
                End Using
            End Using
        End Using
    End Sub



    Private Function GetControlName(ByVal Ana_ID As Integer, ByVal Control_ID As Long) As String
        Dim controlName As String = ""

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT ControlName FROM Ana_Control WHERE Ana_ID = @Ana_ID AND Control_ID = @Control_ID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Ana_ID", Ana_ID)
                command.Parameters.AddWithValue("@Control_ID", Control_ID)

                Dim result As Object = command.ExecuteScalar()
                If result IsNot DBNull.Value AndAlso result IsNot Nothing Then
                    controlName = result.ToString()
                End If
            End Using
        End Using

        Return controlName
    End Function
    Private Sub dtpDate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpDate.Validated
        If chkEditNew.Checked = False Then      'New
            PopulateAnas()
            cmbRunName.Items.Clear()
            cmbRunName.SelectedIndex = -1
        Else
            cmbAnas.SelectedIndex = -1
            cmbAnas.Items.Clear()
            PopulateRunsByDate(dtpDate.Value)
        End If
    End Sub

    Private Sub dgvAccCtl_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAccCtl.CellContentClick
        Select Case e.ColumnIndex
            Case 3, 4, 5
                dgvAccCtl.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = _
                CType(dgvAccCtl.Rows(e.RowIndex).Cells(e.ColumnIndex).Value, Boolean)
                dgvAccCtl.CommitEdit(DataGridViewDataErrorContexts.LeaveControl)
        End Select
    End Sub
End Class
