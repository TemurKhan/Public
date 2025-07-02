Imports System.Windows.Forms
Imports Microsoft.Data.SqlClient
Imports DataRow = System.Data.DataRow
Imports DataTable = System.Data.DataTable

Public Class frmQuickQuote
    Public Shared PMT(0) As String

    Private Sub frmQuickQuote_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dgvCharges.RowCount = 1
        txtQuoteID.Text = NextQuoteID()
        btnQuoteLookUp.Enabled = False
        txtDiscount.Text = "0"
        txtPatHPhone.Mask = SystemConfig.PhoneMask
        UpdateQuote()
    End Sub

    Private Sub chkNewEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNewEdit.Click
        If chkNewEdit.Checked = False Then
            chkNewEdit.Text = "New"
            chkNewEdit.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\New.ico")
            ClearForm()
            btnQuoteLookUp.Enabled = False
            txtQuoteID.Text = NextQuoteID()
            txtQuoteID.Focus()
        Else
            chkNewEdit.Text = "Edit"
            chkNewEdit.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Edit.ico")
            ClearForm()
            btnQuoteLookUp.Enabled = True
            txtQuoteID.Focus()
        End If
        btnSave.Enabled = False
        btnDelete.Enabled = False
        btnPrint.Enabled = False
    End Sub

    Private Sub ClearForm()
        txtQuoteID.Text = ""
        txtProviderID.Text = ""
        txtNPI.Text = ""
        txtProvLName.Text = ""
        txtProvFName.Text = ""
        ClearPatient()
        dgvCharges.Rows.Clear()
        dgvCharges.RowCount = 1
        txtDiscount.Text = "0"
        txtTotal.Text = "0.00"
        txtPayment.Text = "0.00"
        txtBalance.Text = "0.00"
        txtPaymentID.Text = ""
        txtPaymentType.Text = ""
        ReDim PMT(0)
        btnSave.Enabled = False
        btnDelete.Enabled = False
        btnPrint.Enabled = False
    End Sub

    Private Function NextQuoteID() As Long
        ' Dim CNI As New ADODB.Connection
        ' CNI.Open(connString)
        ' Dim Rs As New ADODB.Recordset
        Dim LastID As Object = Nothing

        Using connection As New SqlConnection(connString) ' Old: Dim CNI As New ADODB.Connection
            connection.Open() ' Old: CNI.Open(connString)

            Dim query As String = "SELECT MAX(ID) AS LastID FROM Quotes" ' Old: Rs.Open("Select Max(ID) as LastID from Quotes", CNI, ...)
            Using command As New SqlCommand(query, connection)
                LastID = command.ExecuteScalar() ' Gets a single value from the query (MAX ID)
            End Using
        End Using ' Old: CNI.Close(), CNI = Nothing

        If IsDBNull(LastID) OrElse LastID Is Nothing Then ' Old: If Rs.Fields("LastID").Value Is System.DBNull.Value Then
            Return 1 ' Old: NextQuoteID = 1
        Else
            Return CLng(LastID) + 1 ' Old: NextQuoteID = Rs.Fields("LastID").Value + 1
        End If

        ' Rs cleanup replaced with Using block for ADO.NET
        ' Rs.Close()
        ' Rs = Nothing
    End Function

    Private Sub txtDiscount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDiscount.KeyPress
        Prices(txtDiscount, e)
    End Sub

    Private Sub txtDiscount_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDiscount.Validated
        If Val(txtDiscount.Text) > 100 Then
            MsgBox("Discount can not be more than 100%", MsgBoxStyle.Critical, "Prolis")
            txtDiscount.Text = "100"
        ElseIf txtDiscount.Text = "" Then
            txtDiscount.Text = "0"
        Else
            UpdateQuote()
        End If
    End Sub

    Private Sub UpdateQuote()
        Dim i As Integer
        Dim Discount As Single = 0
        If Val(txtDiscount.Text) > 0 Then
            Discount = Val(txtDiscount.Text) / 100
        End If
        Dim Amt As Single = 0
        For i = 0 To dgvCharges.RowCount - 1
            If dgvCharges.Rows(i).Cells(7).Value IsNot Nothing Then
                Amt += Val(dgvCharges.Rows(i).Cells(7).Value)
            End If
        Next
        txtTotal.Text = Format(Amt - (Discount * Amt), "##0.00")
        If txtPayment.Text <> "" Then
            txtBalance.Text = Format(Val(txtTotal.Text) - Val(txtPayment.Text), "###0.00")
        Else
            txtBalance.Text = Format(Val(txtTotal.Text), "###0.00")
        End If
    End Sub

    Private Sub dgvCharges_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCharges.CellContentClick
        If e.RowIndex <> -1 Then
            If e.ColumnIndex = 0 And dgvCharges.Rows(e.RowIndex).Cells(3).Value <> "" Then
                Dim RetVal As Integer = MsgBox("Are you certain to delete this record line?",
                MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
                If RetVal = vbYes Then
                    ExecuteSqlProcedure("Delete from Quote_TGP where Quote_ID = " &
                    Val(txtQuoteID.Text) & " and TGP_ID = " &
                    Val(dgvCharges.Rows(e.RowIndex).Cells(1).Value))
                    If e.RowIndex < dgvCharges.RowCount - 1 Then
                        dgvCharges.Rows.RemoveAt(e.RowIndex)
                    Else
                        ClearChargeLine(e.RowIndex)
                    End If
                End If
            ElseIf e.ColumnIndex = 2 Then
                Dim TGPID As String = frmTGPLookup.ShowDialog
                If TGPID <> "" Then
                    If Not IsDuplicate(TGPID) Then
                        Dim tgpdata(4) As String
                        Dim TGPType As String = GetTGPType(TGPID)
                        tgpdata = GetTGPData(TGPID, TGPType)
                        PopulateChargeLine(e.RowIndex, tgpdata)
                        If e.RowIndex = dgvCharges.RowCount - 1 _
                        Then dgvCharges.RowCount += 1
                    Else
                        MsgBox("Duplicate component is not allowed.")
                    End If
                End If
            End If
        End If
        UpdateQuote()
        UpdateQuoteProgress()
    End Sub

    Private Function IsDuplicate(ByVal TGPID As String) As Boolean
        Dim CNT As Integer
        Dim i As Integer
        For i = 0 To dgvCharges.RowCount - 1
            If dgvCharges.Rows(i).Cells(1).Value = TGPID Then CNT += 1
        Next
        If CNT > 0 Then
            IsDuplicate = True
        Else
            IsDuplicate = False
        End If
    End Function

    Private Sub PopulateChargeLine(ByVal LineNo As Integer, ByVal TGPData() As String)
        dgvCharges.Rows(LineNo).Cells(0).Value = System.Drawing.Image.FromFile(Application.StartupPath _
        & "\Images\Eraser.ico")
        '0=ID, 1=Name, 2=CPT, 3="", 4=Price
        dgvCharges.Rows(LineNo).Cells(1).Value = TGPData(0)
        dgvCharges.Rows(LineNo).Cells(3).Value = TGPData(1)
        dgvCharges.Rows(LineNo).Cells(4).Value = TGPData(2)
        dgvCharges.Rows(LineNo).Cells(5).Value = Val(TGPData(4)).ToString("0.00")
        dgvCharges.Rows(LineNo).Cells(6).Value = "1"
        dgvCharges.Rows(LineNo).Cells(7).Value = Val(TGPData(4)).ToString("0.00")
    End Sub

    Private Function GetTGPData(ByVal TGPID As Integer, ByVal TGPType As String) As String()
        ' Dim CNQQ As New ADODB.Connection
        ' CNQQ.Open(connstring)
        ' Dim Rs As New ADODB.Recordset
        Dim TGP() As String = {"", "", "", "", ""} ' Old: Dim TGP() As String = {"", "", "", "", ""}
        ' 0=ID, 1=Name, 2=CPT, 3="", 4=Price, 5=M1, 6=M2, 7=M3, 8=M4, 9=POS

        If TGPType <> "" Then
            Dim query As String = ""

            ' Determine the query based on TGPType
            If TGPType = "T" Then   ' Test
                ' Rs.Open("Select * from Tests where ID = " & TGPID, CNQQ, ...)
                query = "SELECT * FROM Tests WHERE ID = @TGPID"
            ElseIf TGPType = "G" Then   ' Group
                ' Rs.Open("Select * from Groups where ID = " & TGPID, CNQQ, ...)
                query = "SELECT * FROM Groups WHERE ID = @TGPID"
            ElseIf TGPType = "P" Then   ' Profile
                ' Rs.Open("Select * from Profiles where ID = " & TGPID, CNQQ, ...)
                query = "SELECT * FROM Profiles WHERE ID = @TGPID"
            End If

            ' Execute query using ADO.NET
            Using connection As New SqlConnection(connString) ' Old: Dim CNQQ As New ADODB.Connection
                connection.Open() ' Old: CNQQ.Open(connstring)
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@TGPID", TGPID) ' Replaced direct concatenation with parameterized query

                    Using adapter As New SqlDataAdapter(command)
                        Dim DtRecords As New DataTable()
                        adapter.Fill(DtRecords) ' Fill DataTable with results

                        ' Check if there are rows (equivalent to Rs.BOF check)
                        If DtRecords.Rows.Count > 0 Then
                            Dim row As DataRow = DtRecords.Rows(0)
                            TGP(0) = row("ID").ToString() ' Old: TGP(0) = Rs.Fields("ID").Value.ToString
                            TGP(1) = row("Name").ToString() ' Old: TGP(1) = Rs.Fields("Name").Value

                            If Not IsDBNull(row("CPT_Code")) AndAlso Trim(row("CPT_Code").ToString()) <> "" Then
                                TGP(2) = Trim(row("CPT_Code").ToString()) ' Old: TGP(2) = Trim(Rs.Fields("CPT_Code").Value)
                            End If
                            TGP(3) = "" ' Old: TGP(3) = ""
                            TGP(4) = GetLocalPrice(row, SystemConfig.PatientPriceLevel) ' Updated with DataRow handling
                        End If
                    End Using
                End Using
            End Using
        End If

        Return TGP
    End Function

    Private Function GetLocalPrice(ByVal row As DataRow, ByVal Level As Integer) As Single
        Dim Price As Single

        If Level = 1 Then
            Price = Convert.ToSingle(row("Price1")) ' Old: Price = Rs.Fields("Price1").Value
        ElseIf Level = 2 Then
            Price = Convert.ToSingle(row("Price2")) ' Old: Price = Rs.Fields("Price2").Value
        ElseIf Level = 3 Then
            Price = Convert.ToSingle(row("Price3")) ' Old: Price = Rs.Fields("Price3").Value
        ElseIf Level = 4 Then
            Price = Convert.ToSingle(row("Price4")) ' Old: Price = Rs.Fields("Price4").Value
        ElseIf Level = 5 Then
            Price = Convert.ToSingle(row("Price5")) ' Old: Price = Rs.Fields("Price5").Value
        ElseIf Level = 6 Then
            Price = Convert.ToSingle(row("Price6")) ' Old: Price = Rs.Fields("Price6").Value
        ElseIf Level = 7 Then
            Price = Convert.ToSingle(row("Price7")) ' Old: Price = Rs.Fields("Price7").Value
        ElseIf Level = 8 Then
            Price = Convert.ToSingle(row("Price8")) ' Old: Price = Rs.Fields("Price8").Value
        ElseIf Level = 9 Then
            Price = Convert.ToSingle(row("Price9")) ' Old: Price = Rs.Fields("Price9").Value
        Else
            Price = Convert.ToSingle(row("ListPrice")) ' Old: Price = Rs.Fields("ListPrice").Value
        End If

        Return Price
    End Function
    Private Sub ClearChargeLine(ByVal LineNo As Integer)
        dgvCharges.Rows(LineNo).Cells(0).Value = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Blank.ico")
        dgvCharges.Rows(LineNo).Cells(1).Value = ""
        dgvCharges.Rows(LineNo).Cells(3).Value = ""
        dgvCharges.Rows(LineNo).Cells(4).Value = ""
        dgvCharges.Rows(LineNo).Cells(5).Value = ""
        dgvCharges.Rows(LineNo).Cells(6).Value = ""
        dgvCharges.Rows(LineNo).Cells(7).Value = ""
    End Sub

    Private Sub dgvCharges_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCharges.CellEndEdit
        If e.RowIndex <> -1 Then
            If e.ColumnIndex = 1 Then   'Componet
                If Not dgvCharges.Rows(e.RowIndex).Cells(1).Value Is Nothing AndAlso
                dgvCharges.Rows(e.RowIndex).Cells(1).Value.ToString <> "" Then
                    If IsNumeric(dgvCharges.Rows(e.RowIndex).Cells(1).Value) Then
                        Dim tgpdata() As String
                        Dim TGPType As String = GetTGPType(dgvCharges.Rows(e.RowIndex).Cells(1).Value)
                        tgpdata = GetTGPData(dgvCharges.Rows(e.RowIndex).Cells(1).Value, TGPType)
                        If tgpdata(0) = "" Then
                            ClearChargeLine(e.RowIndex)
                        Else
                            PopulateChargeLine(e.RowIndex, tgpdata)
                            dgvCharges.RowCount += 1
                        End If
                    Else
                        MsgBox("Component ID is a non-alpha characters only")
                        ClearChargeLine(e.RowIndex)
                    End If
                Else
                    ClearChargeLine(e.RowIndex)
                End If
            ElseIf e.ColumnIndex = 5 Then   'Price
                If Not IsNumeric(dgvCharges.Rows(e.RowIndex).Cells(5).Value) Then
                    MsgBox("Only numeric entry is allowed")
                    dgvCharges.Rows(e.RowIndex).Cells(5).Value = "0.00"
                Else
                    dgvCharges.Rows(e.RowIndex).Cells(7).Value =
                    (Val(dgvCharges.Rows(e.RowIndex).Cells(5).Value) *
                    Val(dgvCharges.Rows(e.RowIndex).Cells(6).Value)).ToString("0.00")
                End If
            ElseIf e.ColumnIndex = 11 Then  'unit
                If Not IsNumeric(dgvCharges.Rows(e.RowIndex).Cells(11).Value) Then
                    MsgBox("Only numeric entry is allowed")
                    dgvCharges.Rows(e.RowIndex).Cells(11).Value = "1.0"
                Else
                    dgvCharges.Rows(e.RowIndex).Cells(7).Value =
                    (Val(dgvCharges.Rows(e.RowIndex).Cells(5).Value) *
                    Val(dgvCharges.Rows(e.RowIndex).Cells(6).Value)).ToString("0.00")
                End If
            End If
            UpdateQuote()
        End If
    End Sub

    Private Sub btnPament_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPament.Click
        Dim Payment As String = frmAccPayment.ShowDialog
        If Payment <> "" Then
            PMT = Split(Payment, "|")
            If PMT(1) = "0" Then
                'Pmt(2) = txtOrdID.Text
                'ElseIf PMT(0) = "1" Then
                '    Dim ItemX As MyList = cmbPIns.SelectedItem
                '    PMT(1) = ItemX.ItemData
            Else
                PMT(2) = txtPatientID.Text
            End If
            SavePayment(PMT)
            txtPayment.Text = Format(Val(PMT(6)), "###0.00")
            If Val(PMT(6)) <= Val(txtTotal.Text) Then
                txtBalance.Text = Format(Val(txtTotal.Text) - Val(PMT(6)), "##0.00")
            Else
                txtBalance.Text = "0.00"
            End If
        Else
            ReDim PMT(0)
            txtPayment.Text = "0.00"
        End If
        UpdateQuote()
    End Sub

    Private Function GetPaymentInfos(ByVal PaymentID As Long) As String()
        ' Dim PMT() As String = {"", "", "", "", "", "", "", ""}
        Dim PMT() As String = {"", "", "", "", "", "", "", ""} ' Old: Dim PMT() As String = {"", "", "", "", "", "", "", ""}
        ' Dim CNP As New ADODB.Connection
        ' CNP.Open(connString)
        ' Dim Rs As New ADODB.Recordset

        Dim query As String = "SELECT * FROM Payments WHERE ID = @PaymentID" ' Old: Rs.Open("Select * from Payments where ID = " & PaymentID, ...)
        Using connection As New SqlConnection(connString) ' Old: Dim CNP As New ADODB.Connection
            connection.Open() ' Old: CNP.Open(connString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@PaymentID", PaymentID) ' Replaced direct concatenation with a parameterized query
                Using adapter As New SqlDataAdapter(command)
                    Dim DtRecords As New DataTable()
                    adapter.Fill(DtRecords) ' Fill the DataTable with query results

                    ' Check if there are rows (equivalent to Rs.BOF)
                    If DtRecords.Rows.Count > 0 Then
                        Dim row As DataRow = DtRecords.Rows(0) ' Retrieve the first row
                        PMT(0) = row("ID").ToString() ' Old: PMT(0) = Rs.Fields("ID").Value.ToString
                        PMT(1) = row("ArType").ToString() ' Old: PMT(1) = Rs.Fields("ArType").Value.ToString
                        PMT(2) = row("Ar_ID").ToString() ' Old: PMT(2) = Rs.Fields("Ar_ID").Value.ToString
                        PMT(3) = row("PaymentType").ToString() ' Old: PMT(3) = Rs.Fields("PaymentType").Value.ToString
                        PMT(4) = Format(Convert.ToDateTime(row("PaymentDate")), SystemConfig.DateFormat) ' Old: Format(Rs.Fields("PaymentDate").Value, ...)
                        PMT(5) = row("DocNo").ToString() ' Old: PMT(5) = Rs.Fields("DocNo").Value
                        PMT(6) = row("Amount").ToString() ' Old: PMT(6) = Rs.Fields("Amount").Value.ToString
                        PMT(7) = row("UnApplied").ToString() ' Old: PMT(7) = Rs.Fields("UnApplied").Value.ToString
                    End If
                End Using
            End Using
        End Using ' Old: Rs.Close(), Rs = Nothing, CNP.Close(), CNP = Nothing

        Return PMT ' Return the populated array
    End Function
    Private Sub SavePayment(ByVal PMT() As String)
        ' 0=ID, 1=ArType, 2=ArID, 3= PType, 4= PDate, 5= DocNo, 6=AMT, 7= UnApp
        ' Dim CNSP As New ADODB.Connection
        ' CNSP.Open(connString)
        ' Dim Rs As New ADODB.Recordset

        If PMT(0) = "" Then PMT(0) = NextPaymentID().ToString() ' Old: If PMT(0) = "" Then PMT(0) = NextPaymentID().ToString

        Dim query As String
        Using connection As New SqlConnection(connString) ' Old: Dim CNSP As New ADODB.Connection
            connection.Open() ' Old: CNSP.Open(connString)

            ' Check if the record exists
            query = "SELECT COUNT(*) FROM Payments WHERE ID = @ID"
            Using commandCheck As New SqlCommand(query, connection)
                commandCheck.Parameters.AddWithValue("@ID", Val(PMT(0)))
                Dim recordExists As Integer = Convert.ToInt32(commandCheck.ExecuteScalar()) ' Checks if record exists

                If recordExists = 0 Then
                    ' Insert new record
                    query = "INSERT INTO Payments (ID, ArType, Ar_ID, PaymentType, PaymentDate, DocNo, Amount, UnApplied) " &
                        "VALUES (@ID, @ArType, @ArID, @PaymentType, @PaymentDate, @DocNo, @Amount, @UnApplied)"
                Else
                    ' Update existing record
                    query = "UPDATE Payments SET ArType = @ArType, Ar_ID = @ArID, PaymentType = @PaymentType, " &
                        "PaymentDate = @PaymentDate, DocNo = @DocNo, Amount = @Amount, UnApplied = @UnApplied WHERE ID = @ID"
                End If
            End Using

            ' Execute the query
            Using commandSave As New SqlCommand(query, connection)
                commandSave.Parameters.AddWithValue("@ID", Val(PMT(0)))
                commandSave.Parameters.AddWithValue("@ArType", Val(PMT(1)))
                commandSave.Parameters.AddWithValue("@ArID", Val(PMT(2)))
                commandSave.Parameters.AddWithValue("@PaymentType", Val(PMT(3)))
                commandSave.Parameters.AddWithValue("@PaymentDate", CDate(PMT(4)))
                commandSave.Parameters.AddWithValue("@DocNo", Trim(PMT(5)))
                commandSave.Parameters.AddWithValue("@Amount", Val(PMT(6)))
                commandSave.Parameters.AddWithValue("@UnApplied", Val(PMT(7)))

                commandSave.ExecuteNonQuery() ' Executes the Insert/Update query
            End Using
        End Using ' Old: CNSP.Close(), CNSP = Nothing
    End Sub

    Private Sub txtPatientID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatientID.GotFocus
        txtPatientID.BackColor = NFCOLOR ' Maintain the original behavior for highlighting the text box

        If txtPatientID.Text <> "" Then
            Dim PatientID As Long = Val(txtPatientID.Text) ' Convert the input to a long value
            'btnPatDx.Enabled = True
            ' Dim CNI As New ADODB.Connection
            ' CNI.Open(connString)
            ' Dim Rs As New ADODB.Recordset

            Dim query As String = "SELECT * FROM Patients WHERE ID = @PatientID"
            Using connection As New SqlConnection(connString) ' Old: Dim CNI As New ADODB.Connection
                connection.Open() ' Old: CNI.Open(connString)
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@PatientID", PatientID) ' Use parameterized query to prevent SQL injection
                    Using reader As SqlDataReader = command.ExecuteReader() ' Old: Rs.Open(...)
                        If reader.HasRows Then ' Old: If Not Rs.BOF Then
                            reader.Read()
                            DisplayPatient(reader("ID")) ' Old: DisplayPatient(Rs.Fields("ID").Value)
                        Else
                            MsgBox("The Patient ID provided is not valid. Use Search") ' Old: MsgBox(...)
                            txtPatientID.Text = "" ' Clear the input if invalid
                            txtPatientID.Focus() ' Set focus back to the input field
                        End If
                    End Using
                End Using
            End Using ' Old: CNI.Close(), CNI = Nothing
        Else
            ClearPatient() ' Clear patient data if no input is provided
        End If

        UpdateQuoteProgress() ' Maintain the original behavior for progress updates
    End Sub

    Private Sub PatientProgress()
        If txtLName.Text <> "" And txtFName.Text <> "" And IsDate(txtDOB.Text) _
        And cmbSex.SelectedIndex <> -1 Then txtPatientID.Text = UpdatePatient(
        txtPatientID.Text, txtLName.Text, txtFName.Text, txtMName.Text,
        Microsoft.VisualBasic.Left(cmbSex.SelectedItem.ToString, 1),
        CDate(txtDOB.Text), SSNNeat(txtSSN.Text), PhoneNeat(txtPatHPhone.Text),
        Trim(txtPatEmail.Text), Trim(txtPatAdr1.Text), Trim(txtPatAdr2.Text),
        Trim(txtPatCity.Text), Trim(txtPatState.Text), Trim(txtPatZip.Text),
        Trim(txtPatCountry.Text))
        If txtPatientID.Text <> "" Then btnRemPat.Enabled = True
        UpdateQuoteProgress()
    End Sub

    Private Function UpdatePatient(ByVal PatientID As String, ByVal LName As String,
ByVal FName As String, ByVal MName As String, ByVal Sex As String, ByVal DOB As _
Date, ByVal SSN As String, ByVal HPhone As String, ByVal Email As String, ByVal _
Add1 As String, ByVal Add2 As String, ByVal City As String, ByVal State As String,
Zip As String, ByVal Country As String) As String

        ' Old: Dim CNP As New ADODB.Connection
        ' CNP.Open(connString)
        Dim query As String
        Dim isNewRecord As Boolean = False

        Using connection As New SqlConnection(connString) ' Old: Dim CNP As New ADODB.Connection
            connection.Open() ' Old: CNP.Open(connString)

            ' Check if PatientID exists
            If Trim(PatientID) <> "" Then
                query = "SELECT COUNT(*) FROM Patients WHERE ID = @PatientID"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@PatientID", Val(PatientID))
                    Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
                    isNewRecord = (count = 0) ' Check if the patient record needs to be added
                End Using
            Else
                ' Check if a matching patient exists
                query = "SELECT ID FROM Patients WHERE LastName = @LastName AND FirstName = @FirstName AND Sex = @Sex AND DOB = @DOB"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@LastName", Trim(LName))
                    command.Parameters.AddWithValue("@FirstName", Trim(FName))
                    command.Parameters.AddWithValue("@Sex", Trim(Sex))
                    command.Parameters.AddWithValue("@DOB", DOB)
                    Dim existingID = command.ExecuteScalar()
                    If existingID Is Nothing Then
                        isNewRecord = True
                    Else
                        PatientID = existingID.ToString()
                    End If
                End Using
            End If

            If isNewRecord Then
                PatientID = NextPatientID() ' Generate the next patient ID
                query = "INSERT INTO Patients (ID, LastName, FirstName, MiddleName, Sex, DOB, SSN, Address_ID, HomePhone, Email) " &
                    "VALUES (@ID, @LastName, @FirstName, @MiddleName, @Sex, @DOB, @SSN, @Address_ID, @HomePhone, @Email)"
            Else
                query = "UPDATE Patients SET LastName = @LastName, FirstName = @FirstName, MiddleName = @MiddleName, " &
                    "Sex = @Sex, DOB = @DOB, SSN = @SSN, Address_ID = @Address_ID, HomePhone = @HomePhone, " &
                    "Email = @Email WHERE ID = @ID"
            End If

            ' Execute Insert or Update query
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@ID", Val(PatientID))
                command.Parameters.AddWithValue("@LastName", Trim(LName))
                command.Parameters.AddWithValue("@FirstName", Trim(FName))
                command.Parameters.AddWithValue("@MiddleName", Trim(MName))
                command.Parameters.AddWithValue("@Sex", Trim(Sex))
                command.Parameters.AddWithValue("@DOB", DOB)
                command.Parameters.AddWithValue("@SSN", SSNNeat(txtSSN.Text)) ' Adjusted for ADO.NET
                If Trim(Add1) <> "" And Trim(City) <> "" And Trim(State) <> "" And Trim(Zip) <> "" Then
                    command.Parameters.AddWithValue("@Address_ID", GetAddressID(Trim(Add1), Trim(Add2), Trim(City), Trim(State), Trim(Zip), Trim(Country)))
                Else
                    command.Parameters.AddWithValue("@Address_ID", DBNull.Value)
                End If
                command.Parameters.AddWithValue("@HomePhone", PhoneNeat(txtPatHPhone.Text)) ' Adjusted for ADO.NET
                command.Parameters.AddWithValue("@Email", Trim(txtPatEmail.Text))

                command.ExecuteNonQuery() ' Executes the Insert or Update query
            End Using
        End Using ' Old: CNP.Close(), CNP = Nothing

        Return PatientID ' Old: Return PatientID
    End Function
    Private Function NextPatientID() As String
        ' Dim CNI As New ADODB.Connection
        ' CNI.Open(connString)
        ' Dim Rs As New ADODB.Recordset
        Dim LastID As Object = Nothing

        Using connection As New SqlConnection(connString) ' Old: Dim CNI As New ADODB.Connection
            connection.Open() ' Old: CNI.Open(connString)
            Dim query As String = "SELECT MAX(ID) AS LastID FROM Patients" ' Old: Rs.Open("Select max(ID) as LastID from Patients", ...)
            Using command As New SqlCommand(query, connection)
                LastID = command.ExecuteScalar() ' Retrieve the maximum ID directly
            End Using
        End Using ' Old: CNI.Close(), CNI = Nothing

        If IsDBNull(LastID) OrElse LastID Is Nothing Then ' Old: If Rs.Fields("LastID").Value Is System.DBNull.Value Then
            Return "1" ' Old: NextPatientID = "1"
        Else
            Return CStr(Convert.ToInt32(LastID) + 1) ' Old: NextPatientID = CStr(Rs.Fields("LastID").Value + 1)
        End If

        ' Rs.Close(), Rs = Nothing replaced with ExecuteScalar for efficiency
    End Function

    Private Sub UpdateQuoteProgress()
        If txtQuoteID.Text <> "" And txtPatientID.Text <> "" And
        ((dgvCharges.Rows(0).Cells(7).Value IsNot Nothing Or
        dgvCharges.Rows(0).Cells(7).Value IsNot System.DBNull.Value) _
        AndAlso Val(dgvCharges.Rows(0).Cells(7).Value) > 0) Then
            btnSave.Enabled = True
            If chkNewEdit.Checked = True Then btnDelete.Enabled = True
            btnPrint.Enabled = True
        End If
    End Sub
    Friend Sub DisplayPatient(ByVal PatID As Long)
        ' Dim CNP As New ADODB.Connection
        ' CNP.Open(connString)
        ' Dim Rs As New ADODB.Recordset
        Dim i As Integer

        Using connection As New SqlConnection(connString) ' Old: Dim CNP As New ADODB.Connection
            connection.Open() ' Old: CNP.Open(connString)
            Dim query As String = "SELECT * FROM Patients WHERE ID = @PatID" ' Old: Rs.Open("Select * from Patients where ID = " & PatID, ...)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@PatID", PatID) ' Replaced concatenation with parameterized query
                Using reader As SqlDataReader = command.ExecuteReader() ' Old: Rs.Open(...)
                    ClearPatient() ' Clear the patient data before populating new details
                    If reader.HasRows Then ' Old: If Not Rs.BOF Then
                        reader.Read()
                        txtPatientID.Text = reader("ID").ToString() ' Old: txtPatientID.Text = Rs.Fields("ID").Value
                        btnRemPat.Enabled = True ' Old: btnRemPat.Enabled = True
                        txtLName.Text = reader("LastName").ToString() ' Old: txtLName.Text = Rs.Fields("LastName").Value
                        txtFName.Text = reader("FirstName").ToString() ' Old: txtFName.Text = Rs.Fields("FirstName").Value
                        txtMName.Text = reader("MiddleName").ToString() ' Old: txtMName.Text = Rs.Fields("MiddleName").Value

                        ' Populate the gender dropdown
                        For i = 0 To cmbSex.Items.Count - 1
                            If cmbSex.Items(i).ToString().Substring(0, 1) = reader("Sex").ToString() Then
                                cmbSex.SelectedIndex = i
                                Exit For
                            End If
                        Next

                        txtDOB.Text = Format(Convert.ToDateTime(reader("DOB")), SystemConfig.DateFormat) ' Old: Format(Rs.Fields("DOB").Value, ...)
                        If Not IsDBNull(reader("SSN")) Then txtSSN.Text = reader("SSN").ToString() ' Old: txtSSN.Text = Rs.Fields("SSN").Value

                        If Not IsDBNull(reader("Address_ID")) Then
                            Dim AddressID As Long = Convert.ToInt64(reader("Address_ID")) ' Get Address_ID safely
                            txtPatAdr1.Text = GetAddress1(AddressID) ' Old: GetAddress1(Rs.Fields("Address_ID").Value)
                            txtPatAdr2.Text = GetAddress2(AddressID) ' Old: GetAddress2(Rs.Fields("Address_ID").Value)
                            txtPatCity.Text = GetAddressCity(AddressID) ' Old: GetAddressCity(Rs.Fields("Address_ID").Value)
                            txtPatState.Text = GetAddressState(AddressID) ' Old: GetAddressState(Rs.Fields("Address_ID").Value)
                            txtPatZip.Text = GetAddressZip(AddressID) ' Old: GetAddressZip(Rs.Fields("Address_ID").Value)
                            txtPatCountry.Text = GetAddressCountry(AddressID) ' Old: GetAddressCountry(Rs.Fields("Address_ID").Value)
                        End If

                        If Not IsDBNull(reader("Email")) Then txtPatEmail.Text = reader("Email").ToString() ' Old: txtPatEmail.Text = Rs.Fields("Email").Value
                        If Not IsDBNull(reader("HomePhone")) Then txtPatHPhone.Text = PhoneNeat(reader("HomePhone").ToString()) ' Old: PhoneNeat(Rs.Fields("HomePhone").Value)
                        btnRemPat.Enabled = True ' Old: btnRemPat.Enabled = True
                    End If
                End Using
            End Using
        End Using ' Old: Rs.Close(), Rs = Nothing, CNP.Close(), CNP = Nothing

        UpdateQuoteProgress() ' Maintain the original behavior for progress updates
    End Sub
    Private Sub ClearPatient()
        txtPatientID.Text = ""
        btnRemPat.Enabled = False
        txtLName.Text = ""
        txtFName.Text = ""
        cmbSex.SelectedIndex = -1
        txtDOB.Text = ""
        txtSSN.Text = ""
        txtMName.Text = ""
        txtPatAdr1.Text = ""
        txtPatAdr2.Text = ""
        txtPatCity.Text = ""
        txtPatState.Text = ""
        txtPatZip.Text = ""
        txtPatEmail.Text = ""
        txtPatHPhone.Text = ""
        UpdateQuoteProgress()
    End Sub

    Private Sub txtFName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFName.GotFocus
        txtFName.BackColor = FCOLOR
    End Sub

    Private Sub txtFName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFName.LostFocus
        txtFName.BackColor = NFCOLOR
        PatientProgress()
    End Sub

    Private Sub txtDOB_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDOB.GotFocus
        txtDOB.BackColor = FCOLOR
    End Sub

    Private Sub txtDOB_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDOB.LostFocus
        txtDOB.BackColor = NFCOLOR
        PatientProgress()
    End Sub

    Private Sub txtPatZip_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatZip.GotFocus
        txtPatZip.BackColor = FCOLOR
    End Sub

    Private Sub txtPatZip_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatZip.LostFocus
        txtPatZip.BackColor = NFCOLOR
        PatientProgress()
    End Sub

    Private Sub txtPatCountry_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatCountry.GotFocus
        txtPatCountry.BackColor = FCOLOR
    End Sub

    Private Sub txtPatCountry_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatCountry.LostFocus
        txtPatCountry.BackColor = NFCOLOR
        PatientProgress()
    End Sub

    Private Sub txtSSN_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSSN.GotFocus
        txtSSN.BackColor = FCOLOR
    End Sub

    Private Sub txtSSN_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSSN.LostFocus
        txtSSN.BackColor = NFCOLOR
        PatientProgress()
    End Sub

    Private Sub txtPatHPhone_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatHPhone.GotFocus
        txtPatHPhone.BackColor = FCOLOR
    End Sub

    Private Sub txtPatHPhone_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatHPhone.LostFocus
        txtPatHPhone.BackColor = NFCOLOR
        PatientProgress()
    End Sub

    Private Sub txtPatEmail_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatEmail.GotFocus
        txtPatEmail.BackColor = FCOLOR
    End Sub

    Private Sub txtPatEmail_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatEmail.LostFocus
        txtPatEmail.BackColor = NFCOLOR
        PatientProgress()
    End Sub

    Private Sub btnRemPat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemPat.Click
        ClearPatient()
        PatientProgress()
    End Sub

    Private Sub btnPatLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatLook.Click
        Dim PatientID As String = frmPatLookUp.ShowDialog()
        If PatientID <> "" Then
            DisplayPatient(Val(PatientID))
        End If
    End Sub

    Private Sub btnQuoteLookUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuoteLookUp.Click
        Dim QuoteID As String = frmQuoteLookUp.ShowDialog
        If QuoteID <> "" Then
            ' Old: Dim CNI As New ADODB.Connection
            ' Old: CNI.Open(connString)
            ' Old: Dim Rs As New ADODB.Recordset
            ' Old: Rs.Open("Select * from Quotes where ID = " & Val(QuoteID), CNI, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

            Dim query As String = "SELECT * FROM Quotes WHERE ID = @QuoteID"

            Using connection As New SqlConnection(connString) ' Old: Dim CNI As New ADODB.Connection
                connection.Open() ' Old: CNI.Open(connString)
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@QuoteID", Val(QuoteID)) ' Parameterized query for security

                    Using reader As SqlDataReader = command.ExecuteReader() ' Old: Rs.Open(...)
                        If Not reader.HasRows Then ' Old: If Rs.BOF Then
                            MsgBox("Invalid ID", MsgBoxStyle.Critical, "Prolis") ' Old: MsgBox("Invalid ID", MsgBoxStyle.Critical, "Prolis")
                            txtQuoteID.Focus() ' Old: txtQuoteID.Focus()
                        Else
                            DisplayQuote(Val(QuoteID)) ' Old: DisplayQuote(Val(QuoteID))
                        End If
                    End Using
                End Using
            End Using ' Old: Rs.Close(), Rs = Nothing, CNI.Close(), CNI = Nothing
        End If
    End Sub

    Friend Sub DisplayQuote(ByVal QuoteID As Long)
        Dim i As Integer
        ' Dim CNQ As New ADODB.Connection
        ' CNQ.Open(connString)
        ' Dim Rs As New ADODB.Recordset
        ' Rs.Open("Select * from Quotes where ID = " & Val(QuoteID), CNQ, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

        Using connection As New SqlConnection(connString) ' Old: Dim CNQ As New ADODB.Connection
            connection.Open() ' Old: CNQ.Open(connString)

            Dim queryQuotes As String = "SELECT * FROM Quotes WHERE ID = @QuoteID"
            Using commandQuotes As New SqlCommand(queryQuotes, connection)
                commandQuotes.Parameters.AddWithValue("@QuoteID", QuoteID) ' Parameterized query for security

                Using readerQuotes As SqlDataReader = commandQuotes.ExecuteReader() ' Old: Rs.Open(...)
                    If readerQuotes.HasRows Then ' Old: If Not Rs.BOF Then
                        readerQuotes.Read()
                        txtQuoteID.Text = readerQuotes("ID").ToString() ' Old: txtQuoteID.Text = Rs.Fields("ID").Value
                        dtpDate.Value = Convert.ToDateTime(readerQuotes("Dated")) ' Old: dtpDate.Value = Rs.Fields("Dated").Value

                        ' Display patient details
                        If Not IsDBNull(readerQuotes("Patient_ID")) Then
                            DisplayPatient(Convert.ToInt64(readerQuotes("Patient_ID"))) ' Old: DisplayPatient(Rs.Fields("Patient_ID").Value)
                        End If

                        ' Display provider details
                        If Not IsDBNull(readerQuotes("Provider_ID")) Then
                            DisplayProvider(Convert.ToInt64(readerQuotes("Provider_ID"))) ' Old: DisplayProvider(Rs.Fields("Provider_ID").Value)
                        Else
                            txtProviderID.Text = ""
                            txtNPI.Text = ""
                            txtProvLName.Text = ""
                            txtProvFName.Text = ""
                        End If

                        ' Display quote amounts
                        txtTotal.Text = Format(Convert.ToDecimal(readerQuotes("QuoteAmount")), "##0.00") ' Old: txtTotal.Text = Format(Rs.Fields("QuoteAmount").Value, "##0.00")
                        txtDiscount.Text = Format(Convert.ToDecimal(readerQuotes("Discount")), "##0.0") ' Old: txtDiscount.Text = Format(Rs.Fields("Discount").Value, "##0.0")

                        ' Handle payment details
                        If Not IsDBNull(readerQuotes("Payment_ID")) Then
                            ReDim PMT(7)
                            PMT = GetPaymentInfos(Convert.ToInt64(readerQuotes("Payment_ID"))) ' Old: PMT = GetPaymentInfos(Rs.Fields("Payment_ID").Value)
                            txtPaymentID.Text = PMT(0)
                            txtPaymentType.Text = PMT(3)
                            txtPayment.Text = Format(Convert.ToDecimal(PMT(6)), "##0.00")
                            txtBalance.Text = Format(Convert.ToDecimal(txtTotal.Text) - Convert.ToDecimal(txtPayment.Text), "##0.00")
                        Else
                            txtPaymentID.Text = ""
                            txtPaymentType.Text = ""
                            txtPayment.Text = ""
                            txtBalance.Text = txtTotal.Text
                        End If
                    End If
                End Using
            End Using

            Dim queryQuoteTGP As String = "SELECT * FROM Quote_TGP WHERE Quote_ID = @QuoteID"
            Using commandQuoteTGP As New SqlCommand(queryQuoteTGP, connection)
                commandQuoteTGP.Parameters.AddWithValue("@QuoteID", QuoteID)

                Using readerQuoteTGP As SqlDataReader = commandQuoteTGP.ExecuteReader() ' Old: Rs.Open(...)
                    If readerQuoteTGP.HasRows Then ' Old: If Not Rs.BOF Then
                        While readerQuoteTGP.Read() ' Old: Do Until Rs.EOF
                            If i >= dgvCharges.RowCount Then dgvCharges.RowCount += 1
                            dgvCharges.Rows(i).Cells(1).Value = readerQuoteTGP("TGP_ID").ToString() ' Old: dgvCharges.Rows(i).Cells(1).Value = Rs.Fields("TGP_ID").Value.ToString
                            dgvCharges.Rows(i).Cells(3).Value = GetTGPName(Convert.ToInt64(readerQuoteTGP("TGP_ID"))) ' Old: GetTGPName(Rs.Fields("TGP_ID").Value)
                            dgvCharges.Rows(i).Cells(4).Value = readerQuoteTGP("CPT").ToString() ' Old: dgvCharges.Rows(i).Cells(4).Value = Rs.Fields("CPT").Value
                            dgvCharges.Rows(i).Cells(5).Value = Format(Convert.ToDecimal(readerQuoteTGP("Price")), "##0.00") ' Old: dgvCharges.Rows(i).Cells(5).Value = Format(Rs.Fields("Price").Value, "##0.00")
                            dgvCharges.Rows(i).Cells(6).Value = Format(Convert.ToDecimal(readerQuoteTGP("Unit")), "#0.0") ' Old: dgvCharges.Rows(i).Cells(6).Value = Format(Rs.Fields("Unit").Value, "#0.0")
                            dgvCharges.Rows(i).Cells(7).Value = Format(Convert.ToDecimal(readerQuoteTGP("Extend")), "##0.00") ' Old: dgvCharges.Rows(i).Cells(7).Value = Format(Rs.Fields("Extend").Value, "##0.00")
                            i += 1
                        End While
                    End If
                End Using
            End Using
        End Using ' Old: Rs.Close(), Rs = Nothing, CNQ.Close(), CNQ = Nothing

        UpdateQuoteProgress() ' Maintain original functionality for progress updates
    End Sub
    Private Sub DisplayProvider(ByVal ProviderID As Long)
        ' Old: Dim CNDP As New ADODB.Connection
        ' Old: CNDP.Open(connString)
        ' Old: Dim Rs As New ADODB.Recordset
        ' Old: Rs.Open("Select * from Providers where ID = " & ProviderID, CNDP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

        Dim query As String = "SELECT * FROM Providers WHERE ID = @ProviderID"

        Using connection As New SqlConnection(connString) ' Old: Dim CNDP As New ADODB.Connection
            connection.Open() ' Old: CNDP.Open(connString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@ProviderID", ProviderID) ' Use parameterized query for security

                Using reader As SqlDataReader = command.ExecuteReader() ' Old: Rs.Open(...)
                    If reader.HasRows Then ' Old: If Not Rs.BOF Then
                        reader.Read()
                        txtProviderID.Text = reader("ID").ToString() ' Old: txtProviderID.Text = Rs.Fields("ID").Value
                        txtNPI.Text = If(IsDBNull(reader("NPI")), "", reader("NPI").ToString()) ' Old: txtNPI.Text = Rs.Fields("NPI").Value
                        txtProvLName.Text = reader("LastName_BSN").ToString() ' Old: txtProvLName.Text = Rs.Fields("LastName_BSN").Value
                        txtProvFName.Text = If(IsDBNull(reader("FirstName")), "", reader("FirstName").ToString()) ' Old: txtProvFName.Text = Rs.Fields("FirstName").Value
                    End If
                End Using
            End Using
        End Using ' Old: Rs.Close(), Rs = Nothing, CNDP.Close(), CNDP = Nothing
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtQuoteID.Text <> "" And txtPatientID.Text <> "" And
        dgvCharges.Rows(0).Cells(7).Value IsNot Nothing AndAlso
        dgvCharges.Rows(0).Cells(7).Value.ToString <> "" Then
            SaveQuote()
            ClearForm()
            If chkNewEdit.Checked = False Then txtQuoteID.Text = NextQuoteID()
        End If
    End Sub

    Private Sub SaveQuote()
        If txtQuoteID.Text <> "" And txtPatientID.Text <> "" AndAlso
       dgvCharges.Rows(0).Cells(7).Value IsNot Nothing AndAlso
       dgvCharges.Rows(0).Cells(7).Value.ToString() <> "" Then

            Using connection As New SqlConnection(connString) ' Old: Dim CNQ As New ADODB.Connection
                connection.Open() ' Old: CNQ.Open(connString)

                ' Save or update the quote
                Dim queryQuote As String
                Dim isNewQuote As Boolean

                ' Check if the quote exists
                queryQuote = "SELECT COUNT(*) FROM Quotes WHERE ID = @QuoteID"
                Using commandCheckQuote As New SqlCommand(queryQuote, connection)
                    commandCheckQuote.Parameters.AddWithValue("@QuoteID", Val(txtQuoteID.Text))
                    isNewQuote = (Convert.ToInt32(commandCheckQuote.ExecuteScalar()) = 0) ' Old: If Rs.BOF Then Rs.AddNew()
                End Using

                If isNewQuote Then
                    queryQuote = "INSERT INTO Quotes (ID, Dated, Patient_ID, Provider_ID, Discount, Payment_ID, PaymentType_ID, QuoteAmount) " &
                             "VALUES (@ID, @Dated, @Patient_ID, @Provider_ID, @Discount, @Payment_ID, @PaymentType_ID, @QuoteAmount)"
                Else
                    queryQuote = "UPDATE Quotes SET Dated = @Dated, Patient_ID = @Patient_ID, Provider_ID = @Provider_ID, Discount = @Discount, " &
                             "Payment_ID = @Payment_ID, PaymentType_ID = @PaymentType_ID, QuoteAmount = @QuoteAmount WHERE ID = @ID"
                End If

                Using commandSaveQuote As New SqlCommand(queryQuote, connection)
                    commandSaveQuote.Parameters.AddWithValue("@ID", Val(txtQuoteID.Text))
                    commandSaveQuote.Parameters.AddWithValue("@Dated", dtpDate.Value)
                    commandSaveQuote.Parameters.AddWithValue("@Patient_ID", Val(txtPatientID.Text))
                    commandSaveQuote.Parameters.AddWithValue("@Provider_ID", If(txtProviderID.Text <> "", Val(txtProviderID.Text), DBNull.Value))
                    commandSaveQuote.Parameters.AddWithValue("@Discount", Val(txtDiscount.Text))
                    If PMT(0) <> "" Then
                        commandSaveQuote.Parameters.AddWithValue("@Payment_ID", PMT(0))
                        SavePayment(PMT) ' Call SavePayment to handle payment logic
                    Else
                        commandSaveQuote.Parameters.AddWithValue("@Payment_ID", DBNull.Value)
                    End If
                    commandSaveQuote.Parameters.AddWithValue("@PaymentType_ID", If(PMT.Length > 3, Val(PMT(3)), DBNull.Value))
                    commandSaveQuote.Parameters.AddWithValue("@QuoteAmount", Val(txtTotal.Text))
                    commandSaveQuote.ExecuteNonQuery() ' Old: Rs.Update()
                End Using

                ' Save or update the quote charges
                For i = 0 To dgvCharges.RowCount - 1
                    If Not (dgvCharges.Rows(i).Cells(1).Value Is Nothing OrElse dgvCharges.Rows(i).Cells(1).Value Is System.DBNull.Value) AndAlso
                   dgvCharges.Rows(i).Cells(1).Value.ToString() <> "" Then

                        Dim queryQuoteTGP As String = "SELECT COUNT(*) FROM Quote_TGP WHERE Quote_ID = @QuoteID AND TGP_ID = @TGP_ID"
                        Dim isNewCharge As Boolean

                        Using commandCheckCharge As New SqlCommand(queryQuoteTGP, connection)
                            commandCheckCharge.Parameters.AddWithValue("@QuoteID", Val(txtQuoteID.Text))
                            commandCheckCharge.Parameters.AddWithValue("@TGP_ID", Val(dgvCharges.Rows(i).Cells(1).Value))
                            isNewCharge = (Convert.ToInt32(commandCheckCharge.ExecuteScalar()) = 0)
                        End Using

                        If isNewCharge Then
                            queryQuoteTGP = "INSERT INTO Quote_TGP (Quote_ID, TGP_ID, Ordinal, Price, Unit, Extend) " &
                                        "VALUES (@Quote_ID, @TGP_ID, @Ordinal, @Price, @Unit, @Extend)"
                        Else
                            queryQuoteTGP = "UPDATE Quote_TGP SET Ordinal = @Ordinal, Price = @Price, Unit = @Unit, Extend = @Extend " &
                                        "WHERE Quote_ID = @Quote_ID AND TGP_ID = @TGP_ID"
                        End If

                        Using commandSaveCharge As New SqlCommand(queryQuoteTGP, connection)
                            commandSaveCharge.Parameters.AddWithValue("@Quote_ID", Val(txtQuoteID.Text))
                            commandSaveCharge.Parameters.AddWithValue("@TGP_ID", Val(dgvCharges.Rows(i).Cells(1).Value))
                            commandSaveCharge.Parameters.AddWithValue("@Ordinal", i)
                            commandSaveCharge.Parameters.AddWithValue("@Price", Val(dgvCharges.Rows(i).Cells(5).Value))
                            commandSaveCharge.Parameters.AddWithValue("@Unit", Val(dgvCharges.Rows(i).Cells(6).Value))
                            commandSaveCharge.Parameters.AddWithValue("@Extend", Val(dgvCharges.Rows(i).Cells(7).Value))
                            commandSaveCharge.ExecuteNonQuery() ' Old: Rs.Update()
                        End Using
                    End If
                Next
            End Using ' Old: CNQ.Close(), CNQ = Nothing
        End If
    End Sub
    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If txtQuoteID.Text <> "" And txtPatientID.Text <> "" And
        dgvCharges.Rows(0).Cells(7).Value IsNot Nothing AndAlso
        dgvCharges.Rows(0).Cells(7).Value.ToString <> "" Then
            SaveQuote()
            'TODO: Crystal Report
            '============================
            'Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            'oRpt.Load(GetReportPath("Quote.rpt"))
            'ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)
            'oRpt.RecordSelectionFormula = "{Quotes.ID} = " & Val(txtQuoteID.Text)
            'frmRV.CRRV.ReportSource = oRpt
            'frmRV.Show()
            'frmRV.MdiParent = frmDashboard
            '============================
        End If
    End Sub
    Private Sub txtQuoteID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQuoteID.Validated
        If txtQuoteID.Text <> "" Then
            ' Old: Dim CNQ As New ADODB.Connection
            ' Old: CNQ.Open(connString)
            ' Old: Dim Rs As New ADODB.Recordset
            ' Old: Rs.Open("Select * from Quotes where ID = " & Val(txtQuoteID.Text), ...)

            Dim query As String = "SELECT * FROM Quotes WHERE ID = @QuoteID"

            Using connection As New SqlConnection(connString) ' Old: Dim CNQ As New ADODB.Connection
                connection.Open() ' Old: CNQ.Open(connString)
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@QuoteID", Val(txtQuoteID.Text)) ' Use a parameterized query

                    Using reader As SqlDataReader = command.ExecuteReader() ' Old: Rs.Open(...)
                        If Not reader.HasRows Then ' Old: If Rs.BOF Then
                            MsgBox("Invalid ID", MsgBoxStyle.Critical, "Prolis") ' Old: MsgBox(...)
                            txtQuoteID.Focus() ' Old: txtQuoteID.Focus()
                        Else
                            DisplayQuote(Val(txtQuoteID.Text)) ' Old: DisplayQuote(...)
                        End If
                    End Using
                End Using
            End Using ' Old: CNQ.Close(), CNQ = Nothing, Rs.Close(), Rs = Nothing
        End If
    End Sub
End Class
