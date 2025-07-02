Imports System.Windows.Forms
Imports Microsoft.Data.SqlClient

Public Class frmInstantiateOrders

    Private Sub frmInstantiateOrders_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbOrderType.SelectedIndex = 0  'Infinite
        PopulateClients(cmbOrderType.SelectedIndex)
        If dgvClients.RowCount > 0 Then
            btnUpdate.Enabled = True
        Else
            btnUpdate.Enabled = False
        End If
        Progressready()
    End Sub

    Private Sub PopulateClients(ByVal OrderType As Integer)
        dgvClients.Rows.Clear()

        Dim sSQL As String
        If OrderType = 0 Then   'Infinite
            sSQL = "
            SELECT ID, LastName_BSN, FirstName, Degree, IsIndividual 
            FROM Providers 
            WHERE ID IN (SELECT DISTINCT OrderingProvider_ID FROM Orders WHERE Active <> 0 AND InfiniteTimed = 0)"
        Else    'Timed
            sSQL = "
            SELECT ID, LastName_BSN, FirstName, Degree, IsIndividual 
            FROM Providers 
            WHERE ID IN (SELECT DISTINCT OrderingProvider_ID FROM Orders WHERE Active <> 0 AND InfiniteTimed <> 0)"
        End If

        Using connection As New SqlConnection(connString)
            connection.Open()

            Using command As New SqlCommand(sSQL, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim Provider As String
                        If Convert.ToInt32(reader("IsIndividual")) = 0 Then ' Entity
                            Provider = reader("LastName_BSN").ToString()
                        Else
                            Provider = $"{reader("LastName_BSN")}, {reader("FirstName")}"
                        End If

                        If Not IsDBNull(reader("Degree")) Then
                            Provider += $" {reader("Degree")}"
                        End If

                        dgvClients.Rows.Add(True, reader("ID").ToString(), Provider)
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub PopulateOrders(ByVal OrderType As Integer, ByVal ClientIDs As String)
        dgvOrders.Rows.Clear()

        Dim sSQL As String
        If OrderType = 0 Then   ' Infinite
            sSQL = "SELECT ID, Patient_ID, OrderDate, CompleteDate, TestDays, InfiniteTimed 
                FROM Orders 
                WHERE Active <> 0 AND InfiniteTimed = 0 AND OrderingProvider_ID IN (" & ClientIDs & ")"
        Else    ' Timed
            sSQL = "SELECT ID, Patient_ID, OrderDate, CompleteDate, TestDays, InfiniteTimed 
                FROM Orders 
                WHERE Active <> 0 AND InfiniteTimed <> 0 AND OrderingProvider_ID IN (" & ClientIDs & ")"
        End If

        Using connection As New SqlConnection(connString)
            connection.Open()

            Using command As New SqlCommand(sSQL, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim PatientName As String = GetPatientName(reader("Patient_ID"))
                        Dim Started As String = Format(reader("OrderDate"), SystemConfig.DateFormat)
                        Dim Expired As String = If(Not IsDBNull(reader("CompleteDate")) AndAlso IsDate(reader("CompleteDate")),
                                              Format(reader("CompleteDate"), SystemConfig.DateFormat), "")

                        dgvOrders.Rows.Add(True, reader("ID").ToString(),
                                       If(Convert.ToInt32(reader("InfiniteTimed")) = 0, "I", "T"),
                                       PatientName, Started, reader("TestDays"), Expired)
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Function GetPatientName(ByVal PatientID As Long) As String
        Dim PatName As String = ""

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT LastName, FirstName FROM Patients WHERE ID = @PatientID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@PatientID", PatientID)

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        PatName = $"{reader("LastName")}, {reader("FirstName")}"
                    End If
                End Using
            End Using
        End Using

        Return PatName
    End Function

    Private Function HasOrders() As Boolean
        Dim HasOrder As Boolean = False
        For i As Integer = 0 To dgvOrders.RowCount - 1
            If dgvOrders.Rows(i).Cells(0).Value = True Then
                HasOrder = True
                Exit For
            End If
        Next
        Return HasOrder
    End Function

    Private Sub Progressready()
        If cmbOrderType.SelectedIndex <> -1 And HasOrders() Then
            btnProcess.Enabled = True
        Else
            btnProcess.Enabled = False
        End If
    End Sub

    Private Sub cmbOrderType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbOrderType.SelectedIndexChanged
        PopulateClients(cmbOrderType.SelectedIndex)
    End Sub

    Private Sub dgvClients_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvClients.CellClick
        If e.RowIndex <> -1 Then
            If e.ColumnIndex = 0 Then 'check or uncheck
                If dgvClients.Rows(e.RowIndex).Cells(0).Value = True Then
                    dgvClients.Rows(e.RowIndex).Cells(0).Value = False
                Else
                    dgvClients.Rows(e.RowIndex).Cells(0).Value = True
                End If
                btnUpdate.Enabled = True
                dgvOrders.Rows.Clear()
                Progressready()
            End If
        End If
    End Sub

    Private Sub dgvOrders_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvOrders.CellClick
        If e.ColumnIndex = 0 Then
            If dgvOrders.Rows(e.RowIndex).Cells(0).Value = True Then
                dgvOrders.Rows(e.RowIndex).Cells(0).Value = False
            Else
                dgvOrders.Rows(e.RowIndex).Cells(0).Value = True
            End If
        End If
        Progressready()
    End Sub

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        GenerateAccessions(dtpDate.Value)
    End Sub

    Private Sub GenerateAccessions(ByVal AccDate As Date)
        Dim OrderType As Integer = 0
        Dim OrderTGPs() As String = {""}
        Dim OrdCount As Integer = 0
        Dim DateFrom As String = Format(AccDate, SystemConfig.DateFormat)
        Dim DateTo As String = Format(AccDate, SystemConfig.DateFormat & " 23:59:00")
        Dim DName As String = AccDate.ToString("ddd")
        Dim AccCount As Integer = 0

        Using connection As New SqlConnection(connString)
            connection.Open()

            For i As Integer = 0 To dgvOrders.RowCount - 1
                If dgvOrders.Rows(i).Cells(0).Value = True Then
                    Dim sSQL As String = "
                    SELECT ID, OrderingProvider_ID, InfiniteTimed 
                    FROM Orders 
                    WHERE Active <> 0 AND OrderDate <= @DateTo 
                    AND (CompleteDate IS NULL OR CompleteDate > @DateTo) 
                    AND CHARINDEX(@DName, TestDays) > 0 
                    AND ID = @OrderID"

                    Using command As New SqlCommand(sSQL, connection)
                        command.Parameters.AddWithValue("@DateTo", DateTo)
                        command.Parameters.AddWithValue("@DName", DName)
                        command.Parameters.AddWithValue("@OrderID", Val(dgvOrders.Rows(i).Cells(1).Value))

                        Using reader As SqlDataReader = command.ExecuteReader()
                            If reader.Read() Then
                                OrderType = If(Convert.ToBoolean(reader("InfiniteTimed")), 1, 0)
                                OrderTGPs = GetOrderTGPs(reader("ID"), OrderType, AccDate)

                                If OrderTGPs(0) <> "" Then
                                    Dim PatientInfo() As String = GetPatientInfo(reader("ID"))
                                    Dim AccID As String = SaveAccession(AccDate, PatientInfo, OrderType, reader("OrderingProvider_ID"), GetWorkComment(reader("ID")))
                                    SaveReqTGP(AccID, OrderTGPs)
                                    SaveReqTests(AccID)
                                    SaveReqDx(AccID, reader("ID"))

                                    If SaveSpecimen(AccID) > 0 Then
                                        SaveReqReports(AccID)
                                        AccCount += 1
                                    Else
                                        Dim deleteQuery As String = "DELETE FROM Req_Dx WHERE Accession_ID = @AccID;
                                                                 DELETE FROM Acc_Results WHERE Accession_ID = @AccID;
                                                                 DELETE FROM Req_Info_Response WHERE Accession_ID = @AccID;
                                                                 DELETE FROM Acc_Info_Results WHERE Accession_ID = @AccID;
                                                                 DELETE FROM Req_TGP WHERE Accession_ID = @AccID;
                                                                 DELETE FROM Requisitions WHERE ID = @AccID;"

                                        Using deleteCmd As New SqlCommand(deleteQuery, connection)
                                            deleteCmd.Parameters.AddWithValue("@AccID", AccID)
                                            deleteCmd.ExecuteNonQuery()
                                        End Using
                                    End If

                                    ReDim OrderTGPs(0)
                                End If

                                OrdCount += 1
                            End If
                        End Using
                    End Using
                End If
            Next
        End Using

        MsgBox($"Process completed with processing {OrdCount} order(s) and generating {AccCount} Accessions.")
    End Sub

    Private Function GetWorkComment(ByVal OrderID As Long) As String
        Dim WCmnt As String = ""

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT WorkCmnt FROM Orders WHERE ID = @OrderID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@OrderID", OrderID)

                Dim result As Object = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    WCmnt = result.ToString()
                End If
            End Using
        End Using

        Return WCmnt
    End Function

    Private Function GetOrdOrderingProviderID(ByVal OrderID As Long) As String
        Dim ProvID As String = ""

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT OrderingProvider_ID FROM Orders WHERE ID = @OrderID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@OrderID", OrderID)

                Dim result As Object = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    ProvID = result.ToString()
                End If
            End Using
        End Using

        Return ProvID
    End Function

    Private Function GetAccOrderingProviderID(ByVal AccID As Long) As Long
        Dim ProvID As Long = 0 ' Default value in case of no result

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT OrderingProvider_ID FROM Requisitions WHERE ID = @AccID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@AccID", AccID)

                Dim result As Object = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    ProvID = Convert.ToInt64(result)
                End If
            End Using
        End Using

        Return ProvID
    End Function

    Private Sub SaveReqReports(ByVal AccID As Long)
        Dim Configs() As String = GetProviderConfigs(AccID)

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim queryCheck As String = "SELECT COUNT(*) FROM Req_RPT WHERE Rpt_Type = 'ACC' AND Base_ID = @AccID 
                                    AND Provider_ID IN (SELECT OrderingProvider_ID FROM Requisitions WHERE ID = @AccID)"

            Dim recordExists As Boolean
            Using checkCmd As New SqlCommand(queryCheck, connection)
                checkCmd.Parameters.AddWithValue("@AccID", AccID)
                recordExists = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0
            End Using

            Dim query As String
            If recordExists Then
                query = "UPDATE Req_RPT SET EntryDate = @EntryDate, EntrySource = @EntrySource, RDM_Auto = @RDM_Auto, 
                     RPT_Complete = @RPT_Complete, RPT_Print = @RPT_Print, RPT_Prolison = @RPT_Prolison, 
                     RPT_Interface = @RPT_Interface, RPT_Fax = @RPT_Fax, Fax = @Fax, RPT_Email = @RPT_Email, 
                     Email = @Email, Priority = @Priority, Executed = @Executed, Comment = @Comment 
                     WHERE Rpt_Type = 'ACC' AND Base_ID = @AccID"
            Else
                query = "INSERT INTO Req_RPT (Rpt_Type, EntryDate, EntrySource, Base_ID, Provider_ID, RDM_Auto, RPT_Complete, 
                     RPT_Print, RPT_Prolison, RPT_Interface, RPT_Fax, Fax, RPT_Email, Email, Priority, Executed, Comment) 
                     VALUES ('ACC', @EntryDate, @EntrySource, @AccID, @ProviderID, @RDM_Auto, @RPT_Complete, @RPT_Print, 
                     @RPT_Prolison, @RPT_Interface, @RPT_Fax, @Fax, @RPT_Email, @Email, @Priority, @Executed, @Comment)"
            End If

            Using cmd As New SqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@AccID", AccID)
                cmd.Parameters.AddWithValue("@ProviderID", GetAccOrderingProviderID(AccID))
                cmd.Parameters.AddWithValue("@EntryDate", Date.Now)
                cmd.Parameters.AddWithValue("@EntrySource", "Accession")
                cmd.Parameters.AddWithValue("@RDM_Auto", False)
                cmd.Parameters.AddWithValue("@RPT_Complete", If(Configs(1) = "", False, CType(Configs(1), Boolean)))
                cmd.Parameters.AddWithValue("@RPT_Print", If(Configs(2) = "", False, CType(Configs(2), Boolean)))
                cmd.Parameters.AddWithValue("@RPT_Prolison", If(Configs(3) = "", False, CType(Configs(3), Boolean)))
                cmd.Parameters.AddWithValue("@RPT_Interface", If(Configs(4) = "", False, CType(Configs(4), Boolean)))
                cmd.Parameters.AddWithValue("@RPT_Fax", If(Configs(6) = "", False, CType(Configs(6), Boolean)))
                cmd.Parameters.AddWithValue("@Fax", Configs(7))
                cmd.Parameters.AddWithValue("@RPT_Email", If(Configs(8) = "", False, CType(Configs(8), Boolean)))
                cmd.Parameters.AddWithValue("@Email", Configs(9))
                cmd.Parameters.AddWithValue("@Priority", False)
                cmd.Parameters.AddWithValue("@Executed", False)
                cmd.Parameters.AddWithValue("@Comment", "")

                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    'Private Function GetProviderConfigs(ByVal AccID As Long) As String()
    '    Dim Configs() As String = {"", "", "", "", "", "", "", "", "", "", ""}
    '    '0=ProvID, 1=RCO, 2=Print, 3=Prolison, 4=Interface, 5=Copies, 6=RPTFax,
    '    '7=Fax#, 8=RPTEmail, 9=Email, 10=AccMerge
    '    Dim CNP As New ADODB.Connection
    '    CNP.Open(odbCS)
    '    Dim Rs As New ADODB.Recordset
    '    Rs.Open("Select * from Providers where ID in (Select OrderingProvider_ID " &
    '    "from Requisitions where ID = " & AccID & ")", CNP,
    '    ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Not Rs.BOF Then
    '        Configs(0) = Rs.Fields("ID").Value.ToString
    '        If Rs.Fields("RPTComplete").Value IsNot System.DBNull.Value _
    '        Then Configs(1) = Rs.Fields("RPTComplete").Value.ToString
    '        If Rs.Fields("RDM_Print").Value IsNot System.DBNull.Value _
    '        Then Configs(2) = Rs.Fields("RDM_Print").Value.ToString
    '        If Rs.Fields("RDM_Prolison").Value IsNot System.DBNull.Value _
    '        Then Configs(3) = Rs.Fields("RDM_Prolison").Value.ToString
    '        If Rs.Fields("RDM_Interface").Value IsNot System.DBNull.Value _
    '        Then Configs(4) = Rs.Fields("RDM_Interface").Value.ToString
    '        If Rs.Fields("RPTCopies").Value IsNot System.DBNull.Value _
    '        Then Configs(5) = Rs.Fields("RPTCopies").Value.ToString
    '        If Rs.Fields("Fax").Value IsNot System.DBNull.Value AndAlso
    '        Trim(Rs.Fields("Fax").Value) <> "" Then
    '            If Rs.Fields("RDM_Fax").Value IsNot System.DBNull.Value _
    '            Then Configs(6) = Rs.Fields("RDM_Fax").Value.ToString
    '            Configs(7) = Trim(Rs.Fields("Fax").Value)
    '        Else
    '            Configs(6) = "False"
    '        End If
    '        If Rs.Fields("Email").Value IsNot System.DBNull.Value AndAlso
    '        Trim(Rs.Fields("Email").Value) <> "" Then
    '            If Rs.Fields("RDM_Email").Value IsNot System.DBNull.Value _
    '            Then Configs(8) = Rs.Fields("RDM_Email").Value.ToString
    '            Configs(9) = Trim(Rs.Fields("Email").Value)
    '        Else
    '            Configs(8) = "False"
    '        End If
    '        If Rs.Fields("AccConsolidate").Value IsNot System.DBNull.Value _
    '        Then Configs(10) = Rs.Fields("AccConsolidate").Value
    '    End If
    '    Rs.Close()
    '    Rs = Nothing
    '    CNP.Close()
    '    CNP = Nothing
    '    Return Configs
    'End Function

    Private Function GetProviderConfigs(ByVal AccID As Long) As String()
        Dim Configs() As String = {"", "", "", "", "", "", "", "", "", "", ""}

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "
            SELECT ID, RPTComplete, RDM_Print, RDM_Prolison, RDM_Interface, RPTCopies, 
                   Fax, RDM_Fax, Email, RDM_Email, AccConsolidate 
            FROM Providers 
            WHERE ID IN (SELECT OrderingProvider_ID FROM Requisitions WHERE ID = @AccID)"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@AccID", AccID)

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        Configs(0) = reader("ID").ToString()
                        Configs(1) = If(IsDBNull(reader("RPTComplete")), "", reader("RPTComplete").ToString())
                        Configs(2) = If(IsDBNull(reader("RDM_Print")), "", reader("RDM_Print").ToString())
                        Configs(3) = If(IsDBNull(reader("RDM_Prolison")), "", reader("RDM_Prolison").ToString())
                        Configs(4) = If(IsDBNull(reader("RDM_Interface")), "", reader("RDM_Interface").ToString())
                        Configs(5) = If(IsDBNull(reader("RPTCopies")), "", reader("RPTCopies").ToString())

                        If Not IsDBNull(reader("Fax")) AndAlso reader("Fax").ToString().Trim() <> "" Then
                            Configs(6) = If(IsDBNull(reader("RDM_Fax")), "False", reader("RDM_Fax").ToString())
                            Configs(7) = reader("Fax").ToString().Trim()
                        Else
                            Configs(6) = "False"
                        End If

                        If Not IsDBNull(reader("Email")) AndAlso reader("Email").ToString().Trim() <> "" Then
                            Configs(8) = If(IsDBNull(reader("RDM_Email")), "False", reader("RDM_Email").ToString())
                            Configs(9) = reader("Email").ToString().Trim()
                        Else
                            Configs(8) = "False"
                        End If

                        Configs(10) = If(IsDBNull(reader("AccConsolidate")), "", reader("AccConsolidate").ToString())
                    End If
                End Using
            End Using
        End Using

        Return Configs
    End Function

    Private Function GetMatCount(ByVal MaterialID As Integer, ByVal Mats(,) As String) As Integer
        Dim CNT As Integer = 0
        Dim i As Integer
        For i = 0 To UBound(Mats, 2)
            If Mats(0, i) = MaterialID.ToString Then
                CNT = Val(Mats(1, i))
                Exit For
            End If
        Next
        Return CNT
    End Function

    'Private Function GetTestFormula(ByVal TestID As Integer) As String
    '    Dim Formula As String = ""
    '    Dim CNP As New ADODB.Connection
    '    CNP.Open(odbCS)
    '    Dim Rs As New ADODB.Recordset
    '    Rs.Open("Select * from Tests where IsCalculated <> 0 and Formula <> '' and ID = " &
    '    TestID, CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Not Rs.BOF Then Formula = Rs.Fields("Formula").Value
    '    Rs.Close()
    '    Rs = Nothing
    '    CNP.Close()
    '    CNP = Nothing
    '    Return Formula
    'End Function
    Private Function GetTestFormula(ByVal TestID As Integer) As String
        Dim Formula As String = ""

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT Formula FROM Tests WHERE IsCalculated <> 0 AND Formula <> '' AND ID = @TestID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@TestID", TestID)

                Dim result As Object = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    Formula = result.ToString()
                End If
            End Using
        End Using

        Return Formula
    End Function


    Private Function GetTestIDsFromFormula(ByVal Formula As String) As String
        Dim TestIDs As String = ""
        Dim TestID As String = ""
        Dim NstClcTIDs As String = ""
        Dim P1 As Integer
        Dim P2 As Integer
        Do Until Formula = "" Or (InStr(Formula, "{") = 0 And InStr(Formula, "{") = 0)
            P1 = InStr(Formula, "{")
            P2 = InStr(Formula, "}")
            If P1 > 0 AndAlso P2 > P1 Then
                TestID = Microsoft.VisualBasic.Mid(Formula, P1 + 1)
                TestID = Microsoft.VisualBasic.Mid(TestID, 1, InStr(TestID, "}") - 1)
                Formula = Microsoft.VisualBasic.Mid(Formula, P2 + 1)
                If GetTestFormula(TestID) = "" Then
                    If InStr(TestIDs, TestID & ",") = 0 _
                    Then TestIDs += TestID & ", "
                Else
                    NstClcTIDs += TestID & ", "
                End If
                TestID = ""
            End If
        Loop
        '
        If NstClcTIDs.Length > 2 AndAlso Microsoft.VisualBasic.Right(NstClcTIDs, 2) = ", " _
        Then NstClcTIDs = Microsoft.VisualBasic.Mid(NstClcTIDs, 1, Len(NstClcTIDs) - 2)
        Do Until NstClcTIDs = ""
            Dim CalcdIDs() As String = Split(NstClcTIDs, ",")
            Dim Formula2 As String = ""
            For i As Integer = 0 To CalcdIDs.Length - 1
                Formula2 = GetTestFormula(Val(Trim(CalcdIDs(i))))
                NstClcTIDs = Microsoft.VisualBasic.Mid(NstClcTIDs,
                InStr(NstClcTIDs, Trim(CalcdIDs(i))) + Len(Trim(CalcdIDs(i))))
                Do Until Formula2 = "" Or (InStr(Formula2, "{") = 0 And InStr(Formula2, "{") = 0)
                    P1 = InStr(Formula2, "{")
                    P2 = InStr(Formula2, "}")
                    If P1 > 0 AndAlso P2 > P1 Then
                        TestID = Microsoft.VisualBasic.Mid(Formula2, P1 + 1)
                        TestID = Microsoft.VisualBasic.Mid(TestID, 1, InStr(TestID, "}") - 1)
                        Formula2 = Microsoft.VisualBasic.Mid(Formula2, P2 + 1)
                        If GetTestFormula(TestID) = "" Then
                            If InStr(TestIDs, TestID & ",") = 0 _
                            Then TestIDs += TestID & ", "
                        Else
                            NstClcTIDs += TestID & ", "
                        End If
                        TestID = ""
                    End If
                Loop
            Next
            If Trim(NstClcTIDs) = "," Then NstClcTIDs = ""
        Loop
        '
        'If TestIDs.Length > 2 AndAlso Microsoft.VisualBasic.Right(TestIDs, 2) = ", " _
        'Then TestIDs = Microsoft.VisualBasic.Mid(TestIDs, 1, Len(TestIDs) - 2)
        Return TestIDs
    End Function

    Private Function SaveSpecimen(ByVal AccID As Long) As Integer
        Dim Samples As Integer = 0
        Dim TestIDs As String = ""
        Dim FinalMats As String = ""
        Dim AllMats As String = ""

        Using connection As New SqlConnection(connString)
            connection.Open()

            ' Delete existing specimens
            Dim deleteQuery As String = "DELETE FROM Specimens WHERE Accession_ID = @AccID"
            Using deleteCmd As New SqlCommand(deleteQuery, connection)
                deleteCmd.Parameters.AddWithValue("@AccID", AccID)
                deleteCmd.ExecuteNonQuery()
            End Using

            ' Collect Test IDs
            Dim queryTestIDs As String = "SELECT Test_ID FROM Acc_Results WHERE Accession_ID = @AccID"
            Using commandTestIDs As New SqlCommand(queryTestIDs, connection)
                commandTestIDs.Parameters.AddWithValue("@AccID", AccID)

                Using readerTestIDs As SqlDataReader = commandTestIDs.ExecuteReader()
                    While readerTestIDs.Read()
                        Dim Formula As String = GetTestFormula(Convert.ToInt32(readerTestIDs("Test_ID")))
                        If Not String.IsNullOrEmpty(Formula) Then
                            TestIDs += GetTestIDsFromFormula(Formula) & ", "
                        ElseIf Not TestIDs.Contains(readerTestIDs("Test_ID").ToString() & ",") Then
                            TestIDs += readerTestIDs("Test_ID").ToString() & ", "
                        End If
                    End While
                End Using
            End Using

            ' Trim trailing comma
            If TestIDs.EndsWith(", ") Then TestIDs = TestIDs.Substring(0, TestIDs.Length - 2)

            ' Fetch materials
            If Not String.IsNullOrEmpty(TestIDs) Then
                Dim queryMaterials As String = "SELECT MATERIAL_ID, COUNT(MATERIAL_ID) AS Matcount 
                                             FROM Test_Material WHERE TEST_ID IN (" & TestIDs & ") 
                                             GROUP BY MATERIAL_ID ORDER BY Matcount DESC"
                Using commandMaterials As New SqlCommand(queryMaterials, connection)
                    Using readerMaterials As SqlDataReader = commandMaterials.ExecuteReader()
                        Dim materialsList As New List(Of String)
                        While readerMaterials.Read()
                            materialsList.Add(readerMaterials("MATERIAL_ID").ToString())
                        End While
                        AllMats = String.Join(", ", materialsList)
                    End Using
                End Using
            End If

            ' Extract Final Material
            If AllMats.Contains(", ") Then
                FinalMats = AllMats.Substring(0, AllMats.IndexOf(", "))
                AllMats = AllMats.Substring(AllMats.IndexOf(", ") + 2)
            Else
                FinalMats = AllMats
                AllMats = ""
            End If

            ' Save Specimens
            If Not String.IsNullOrEmpty(FinalMats) Then
                Dim materialsArray As String() = FinalMats.Split(","c)
                For Each materialID In materialsArray
                    Dim sourceIDs As String() = GetSourceID(Convert.ToInt32(materialID.Trim()))

                    For Each sourceID In sourceIDs
                        Dim insertQuery As String = "INSERT INTO Specimens (Accession_ID, Source_ID, SourceNo, SourceQuantity, SourceDate, SourceTemp, Comment) 
                                                 VALUES (@AccID, @SourceID, @SourceNo, @SourceQuantity, @SourceDate, @SourceTemp, @Comment)"
                        Using insertCmd As New SqlCommand(insertQuery, connection)
                            insertCmd.Parameters.AddWithValue("@AccID", AccID)
                            insertCmd.Parameters.AddWithValue("@SourceID", Convert.ToInt32(sourceID))
                            insertCmd.Parameters.AddWithValue("@SourceNo", AccID.ToString() & "-" & (Samples + 1).ToString())
                            insertCmd.Parameters.AddWithValue("@SourceQuantity", Math.Min((Samples \ 30) + 1, 6))
                            insertCmd.Parameters.AddWithValue("@SourceDate", Date.Now)
                            insertCmd.Parameters.AddWithValue("@SourceTemp", "Room Temp")
                            insertCmd.Parameters.AddWithValue("@Comment", "")
                            insertCmd.ExecuteNonQuery()
                        End Using
                        Samples += 1
                    Next
                Next
            End If
        End Using

        Return Samples
    End Function


    Protected Function MatInMats(ByVal Mat As String, ByVal Mats() As String) As Boolean
        Dim InMats As Boolean = False
        Dim i As Integer
        For i = 0 To Mats.Length - 1
            If Mat = Mats(i) Then
                InMats = True
                Exit For
            End If
        Next
        Return InMats
    End Function


    Private Function GetSourceID(ByVal MatID As Integer) As String()
        Dim SRCS As New List(Of String)

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT ID FROM Sources WHERE Material_ID = @MatID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@MatID", MatID)

                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        SRCS.Add(reader("ID").ToString())
                    End While
                End Using
            End Using
        End Using

        Return SRCS.ToArray()
    End Function
    Private Sub SaveReqDx(ByVal AccID As Long, ByVal OrderID As Long)
        Try
            Dim GoodDxs As New List(Of String)
            Dim i As Integer = 0

            Using connection As New SqlConnection(connString)
                connection.Open()

                ' Retrieve Dx Codes
                Dim queryDxCodes As String = "SELECT Dx_Code FROM Order_Dx WHERE Order_ID = @OrderID ORDER BY Ordinal"
                Using commandDxCodes As New SqlCommand(queryDxCodes, connection)
                    commandDxCodes.Parameters.AddWithValue("@OrderID", OrderID)

                    Using readerDxCodes As SqlDataReader = commandDxCodes.ExecuteReader()
                        While readerDxCodes.Read()
                            Dim DxCode As String = readerDxCodes("Dx_Code").ToString().Trim()

                            ' Check if Dx Code exists in Req_Dx
                            Dim queryCheckDx As String = "SELECT COUNT(*) FROM Req_Dx WHERE Accession_ID = @AccID AND Dx_Code = @DxCode"
                            Using checkCmd As New SqlCommand(queryCheckDx, connection)
                                checkCmd.Parameters.AddWithValue("@AccID", AccID)
                                checkCmd.Parameters.AddWithValue("@DxCode", DxCode)

                                Dim exists As Boolean = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0
                                If Not exists Then
                                    Dim queryInsertDx As String = "INSERT INTO Req_Dx (Accession_ID, Dx_Code, IsPrimary, Ordinal) 
                                                               VALUES (@AccID, @DxCode, @IsPrimary, @Ordinal)"
                                    Using insertCmd As New SqlCommand(queryInsertDx, connection)
                                        insertCmd.Parameters.AddWithValue("@AccID", AccID)
                                        insertCmd.Parameters.AddWithValue("@DxCode", DxCode)
                                        insertCmd.Parameters.AddWithValue("@IsPrimary", i = 0)
                                        insertCmd.Parameters.AddWithValue("@Ordinal", i)
                                        insertCmd.ExecuteNonQuery()
                                    End Using
                                End If
                            End Using

                            GoodDxs.Add($"'{DxCode}'")
                            i += 1
                        End While
                    End Using
                End Using

                ' Remove unused Dx Codes
                If GoodDxs.Count > 0 Then
                    Dim queryDeleteDx As String = $"DELETE FROM Req_Dx WHERE Accession_ID = @AccID AND Dx_Code NOT IN ({String.Join(", ", GoodDxs)})"
                    Using deleteCmd As New SqlCommand(queryDeleteDx, connection)
                        deleteCmd.Parameters.AddWithValue("@AccID", AccID)
                        deleteCmd.ExecuteNonQuery()
                    End Using
                End If
            End Using
        Catch ex As Exception
            ' Handle exception (optional logging)
        End Try
    End Sub

    Private Function GetTGPESRD(ByVal AccID As Long, ByVal TGPID As Integer) As Boolean
        Dim ESRD As Boolean = False

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT IsESRD FROM Client_Order 
                               WHERE Provider_ID IN (SELECT OrderingProvider_ID FROM Requisitions WHERE ID = @AccID) 
                               AND Patient_ID IN (SELECT Patient_ID FROM Requisitions WHERE ID = @AccID) 
                               AND TGP_ID = @TGPID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@AccID", AccID)
                command.Parameters.AddWithValue("@TGPID", TGPID)

                Dim result As Object = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    ESRD = Convert.ToBoolean(result)
                End If
            End Using
        End Using

        Return ESRD
    End Function

    Private Sub SaveReqTGP(ByVal AccID As Long, ByVal TGPS() As String)
        Try
            Using connection As New SqlConnection(connString)
                connection.Open()

                For Each TGP In TGPS
                    If Not String.IsNullOrEmpty(TGP) Then

                        Dim exists As Boolean

                        ' Check if TGP already exists
                        Dim queryCheck As String = "SELECT COUNT(*) FROM Req_TGP WHERE Accession_ID = @AccID AND TGP_ID = @TGPID"
                        Using checkCmd As New SqlCommand(queryCheck, connection)
                            checkCmd.Parameters.AddWithValue("@AccID", AccID)
                            checkCmd.Parameters.AddWithValue("@TGPID", Val(TGP))
                            exists = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0

                        End Using

                        Dim query As String
                        If exists Then
                            query = "UPDATE Req_TGP SET TGP_Type = @TGPType, Billed = @Billed, IsStat = @IsStat, 
                                 Dx_Code = @DxCode, IsESRD = @IsESRD WHERE Accession_ID = @AccID AND TGP_ID = @TGPID"
                        Else
                            query = "INSERT INTO Req_TGP (Accession_ID, TGP_ID, TGP_Type, Billed, IsStat, Dx_Code, IsESRD) 
                                 VALUES (@AccID, @TGPID, @TGPType, @Billed, @IsStat, @DxCode, @IsESRD)"
                        End If

                        Using cmd As New SqlCommand(query, connection)
                            cmd.Parameters.AddWithValue("@AccID", AccID)
                            cmd.Parameters.AddWithValue("@TGPID", Val(TGP))
                            cmd.Parameters.AddWithValue("@TGPType", GetTGPType(Val(TGP)))
                            cmd.Parameters.AddWithValue("@Billed", False)
                            cmd.Parameters.AddWithValue("@IsStat", False)
                            cmd.Parameters.AddWithValue("@DxCode", "")
                            cmd.Parameters.AddWithValue("@IsESRD", GetTGPESRD(AccID, Val(TGP)))

                            cmd.ExecuteNonQuery()
                        End Using
                    End If
                Next
            End Using
        Catch ex As Exception
            ' Handle exception (optional logging)
        End Try
    End Sub

    Private Function IsResultable(ByVal TestID As Integer) As Boolean
        Dim Resultable As Boolean = False

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT HasResult FROM Tests WHERE ID = @TestID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@TestID", TestID)

                Dim result As Object = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    Resultable = Convert.ToBoolean(result)
                End If
            End Using
        End Using

        Return Resultable
    End Function


    Private Function InReqTests(ByVal Accession_ID As Long, ByVal Test_ID As Integer) As Boolean
        Dim exists As Boolean = False

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT COUNT(*) FROM Acc_Results WHERE Accession_ID = @AccessionID AND Test_ID = @TestID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@AccessionID", Accession_ID)
                command.Parameters.AddWithValue("@TestID", Test_ID)

                exists = Convert.ToInt32(command.ExecuteScalar()) > 0
            End Using
        End Using

        Return exists
    End Function


    'Private Sub SaveReqTests(ByVal AccID As Long)
    '    'Dim i As Integer
    '    'Dim m As Integer
    '    Dim Ordinal As Integer = 0
    '    'Dim ToMarks() As String
    '    Dim GoodTests As String = ""
    '    Dim NormalRange As String = ""
    '    Dim Flag As String = ""
    '    Dim GorT As String = ""
    '    Dim TGPType As String = ""
    '    Dim SQLClean As String = "Delete from Acc_Results where Accession_ID = " &
    '    AccID & " and not Test_ID in ("
    '    Dim CNP As New ADODB.Connection
    '    CNP.Open(odbCS)

    '    Dim Rs As New ADODB.Recordset
    '    Rs.Open("Select * from Req_TGP where Accession_ID = " & AccID, CNP,
    '    ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Not Rs.BOF Then
    '        Do Until Rs.EOF
    '            TGPType = GetTGPType(Rs.Fields("TGP_ID").Value)
    '            If TGPType = "T" Then       'test
    '                If IsResultable(Rs.Fields("TGP_ID").Value) Then
    '                    If Not InReqTests(AccID, Rs.Fields("TGP_ID").Value) Then
    '                        NormalRange = GetNormalRange(AccID, Rs.Fields("TGP_ID").Value)
    '                        CNP.Execute("Insert InTo Acc_Results(Accession_ID, Test_ID, " &
    '                        "Result, Flag, NormalRange, Released, Ordinal) values (" & AccID _
    '                        & "," & Rs.Fields("TGP_ID").Value & ",'','','" &
    '                        NormalRange & "',0," & Ordinal & ")")
    '                        Ordinal += 1
    '                        NormalRange = ""
    '                    End If
    '                    GoodTests += Rs.Fields("TGP_ID").Value.ToString & ", "
    '                End If
    '                UpdateInfoResults(AccID, Rs.Fields("TGP_ID").Value)
    '                UpdateReqInfoResponse(AccID, Rs.Fields("TGP_ID").Value)
    '            ElseIf TGPType = "G" Then   'group
    '                Dim Rsg As New ADODB.Recordset
    '                Rsg.Open("Select Test_ID from Group_Test where Group_ID = " &
    '                Rs.Fields("TGP_ID").Value & " Order by Ordinal", CNP,
    '                ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '                If Not Rsg.BOF Then
    '                    Do Until Rsg.EOF
    '                        If IsResultable(Rsg.Fields("Test_ID").Value) Then
    '                            If Not InReqTests(AccID, Rsg.Fields("Test_ID").Value) Then
    '                                NormalRange = GetNormalRange(AccID, Rsg.Fields("Test_ID").Value)
    '                                CNP.Execute("Insert InTo Acc_Results(Accession_ID, Test_ID, " &
    '                                "Result, Flag, NormalRange, Released, Ordinal) values (" & AccID _
    '                                & "," & Rsg.Fields("Test_ID").Value & ",'','','" &
    '                                NormalRange & "',0," & Ordinal & ")")
    '                                Ordinal += 1
    '                                NormalRange = ""
    '                            End If
    '                            GoodTests += Rsg.Fields("Test_ID").Value.ToString & ", "
    '                        End If
    '                        UpdateInfoResults(AccID, Rsg.Fields("Test_ID").Value)
    '                        UpdateReqInfoResponse(AccID, Rsg.Fields("Test_ID").Value)
    '                        Rsg.MoveNext()
    '                    Loop
    '                End If
    '                Rsg.Close()
    '                Rsg = Nothing
    '            Else                    'Profile
    '                Dim Rsp As New ADODB.Recordset
    '                Rsp.Open("Select GrpTst_ID from Prof_GrpTst where Profile_ID = " &
    '                Rs.Fields("TGP_ID").Value & " Order by Ordinal", CNP,
    '                ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '                If Not Rsp.BOF Then
    '                    Do Until Rsp.EOF
    '                        GorT = GetTGPType(Rsp.Fields("GrpTst_ID").Value)
    '                        If GorT = "T" Then
    '                            'ToMarks = GetToMarks(Rsp.Fields("GrpTst_ID").Value)
    '                            'For m = 0 To ToMarks.Length - 1
    '                            If IsResultable(Rsp.Fields("GrpTst_ID").Value) Then
    '                                If Not InReqTests(AccID, Rsp.Fields("GrpTst_ID").Value) Then
    '                                    NormalRange = GetNormalRange(AccID, Rsp.Fields("GrpTst_ID").Value)
    '                                    CNP.Execute("Insert InTo Acc_Results(Accession_ID, Test_ID, " &
    '                                    "Result, Flag, NormalRange, Released, Ordinal) values (" & AccID _
    '                                    & "," & Rsp.Fields("GrpTst_ID").Value & ",'','','" &
    '                                    NormalRange & "',0," & Ordinal & ")")
    '                                    Ordinal += 1
    '                                    NormalRange = ""
    '                                End If
    '                                GoodTests += Rsp.Fields("GrpTst_ID").Value.ToString & ", "
    '                            End If
    '                            UpdateInfoResults(AccID, Rsp.Fields("GrpTst_ID").Value)
    '                            UpdateReqInfoResponse(AccID, Rsp.Fields("GrpTst_ID").Value)
    '                        Else
    '                            Dim Rsg As New ADODB.Recordset
    '                            Rsg.Open("Select Test_ID from Group_Test where Group_ID = " &
    '                            Rsp.Fields("GrpTst_ID").Value & " Order by Ordinal", CNP,
    '                            ADODB.CursorTypeEnum.adOpenStatic,
    '                            ADODB.LockTypeEnum.adLockReadOnly)
    '                            If Not Rsg.BOF Then
    '                                Do Until Rsg.EOF
    '                                    'ToMarks = GetToMarks(Rsg.Fields("Test_ID").Value)
    '                                    'For m = 0 To ToMarks.Length - 1
    '                                    If IsResultable(Rsg.Fields("Test_ID").Value) Then
    '                                        If Not InReqTests(AccID, Rsg.Fields("Test_ID").Value) Then
    '                                            NormalRange = GetNormalRange(AccID, Rsg.Fields("Test_ID").Value)
    '                                            CNP.Execute("Insert InTo Acc_Results(Accession_ID, Test_ID, " &
    '                                            "Result, Flag, NormalRange, Released, Ordinal) values (" & AccID _
    '                                            & "," & Rsg.Fields("Test_ID").Value & ",'','','" &
    '                                            NormalRange & "',0," & Ordinal & ")")
    '                                            Ordinal += 1
    '                                            NormalRange = ""
    '                                        End If
    '                                        GoodTests += Rsg.Fields("Test_ID").Value.ToString & ", "
    '                                    End If
    '                                    UpdateInfoResults(AccID, Rsg.Fields("Test_ID").Value)
    '                                    UpdateReqInfoResponse(AccID, Rsg.Fields("Test_ID").Value)
    '                                    Rsg.MoveNext()
    '                                Loop
    '                            End If
    '                            Rsg.Close()
    '                            Rsg = Nothing
    '                        End If
    '                        Rsp.MoveNext()
    '                    Loop
    '                End If
    '                Rsp.Close()
    '                Rsp = Nothing
    '            End If
    '            Rs.MoveNext()
    '        Loop
    '    End If
    '    Rs.Close()
    '    Rs = Nothing
    '    If Len(GoodTests) > 2 Then GoodTests = Microsoft.VisualBasic.Mid(GoodTests, 1, Len(GoodTests) - 2)
    '    SQLClean += GoodTests & ")"
    '    If GoodTests <> "" Then
    '        CNP.Execute(SQLClean)
    '        CNP.Execute("Delete from Ref_Results where Accession_ID = " & AccID &
    '        " and not Reflexer_ID in (Select Reflexed_ID from Ref_Results where " &
    '        "Accession_ID = " & AccID & " and Reflexer_ID in (" & GoodTests & "))")
    '        CNP.Execute("Delete from Ref_Results where Accession_ID = " &
    '        AccID & " and not Reflexer_ID in (" & GoodTests & ")")
    '        CNP.Execute("Delete from Req_Info_Response where Accession_ID = " &
    '        AccID & " and not TGP_ID in (" & GoodTests & ")")
    '        CNP.Execute("Delete from Acc_Info_Results where Accession_ID = " &
    '        AccID & " and not Test_ID in (" & GoodTests & ")")
    '    End If
    '    CNP.Close()
    '    CNP = Nothing
    'End Sub

    Private Sub SaveReqTests(ByVal AccID As Long)
        Try
            Dim Ordinal As Integer = 0
            Dim GoodTests As New List(Of String)

            Using connection As New SqlConnection(connString)
                connection.Open()

                ' Retrieve TGPs for the Accession
                Dim queryReqTGP As String = "SELECT TGP_ID FROM Req_TGP WHERE Accession_ID = @AccID"
                Using commandReqTGP As New SqlCommand(queryReqTGP, connection)
                    commandReqTGP.Parameters.AddWithValue("@AccID", AccID)

                    Using readerReqTGP As SqlDataReader = commandReqTGP.ExecuteReader()
                        While readerReqTGP.Read()
                            Dim TGP_ID As Integer = Convert.ToInt32(readerReqTGP("TGP_ID"))
                            Dim TGPType As String = GetTGPType(TGP_ID)

                            If TGPType = "T" Then ' Individual Test
                                If IsResultable(TGP_ID) AndAlso Not InReqTests(AccID, TGP_ID) Then
                                    InsertTestResult(AccID, TGP_ID, Ordinal, connection)
                                    Ordinal += 1
                                End If
                                GoodTests.Add(TGP_ID.ToString())
                            Else
                                ' Process Group or Profile
                                ProcessGroupOrProfile(AccID, TGP_ID, GoodTests, Ordinal, connection)
                            End If
                        End While
                    End Using
                End Using

                ' Cleanup redundant tests
                CleanUpResults(AccID, GoodTests, connection)
            End Using
        Catch ex As Exception
            ' Handle error (optional logging)
        End Try
    End Sub
    Private Sub InsertTestResult(ByVal AccID As Long, ByVal TestID As Integer, ByRef Ordinal As Integer, ByVal connection As SqlConnection)
        Dim NormalRange As String = GetNormalRange(AccID, TestID)

        Dim queryInsert As String = "INSERT INTO Acc_Results (Accession_ID, Test_ID, Result, Flag, NormalRange, Released, Ordinal) 
                                 VALUES (@AccID, @TestID, '', '', @NormalRange, 0, @Ordinal)"
        Using cmd As New SqlCommand(queryInsert, connection)
            cmd.Parameters.AddWithValue("@AccID", AccID)
            cmd.Parameters.AddWithValue("@TestID", TestID)
            cmd.Parameters.AddWithValue("@NormalRange", NormalRange)
            cmd.Parameters.AddWithValue("@Ordinal", Ordinal)
            cmd.ExecuteNonQuery()
        End Using

        UpdateInfoResults(AccID, TestID)
        UpdateReqInfoResponse(AccID, TestID)
    End Sub
    Private Sub ProcessGroupOrProfile(ByVal AccID As Long, ByVal TGP_ID As Integer, ByRef GoodTests As List(Of String), ByRef Ordinal As Integer, ByVal connection As SqlConnection)
        Dim GorT As String = GetTGPType(TGP_ID)
        Dim query As String = ""

        If GorT = "G" Then
            query = "SELECT Test_ID FROM Group_Test WHERE Group_ID = @TGP_ID ORDER BY Ordinal"
        Else
            query = "SELECT GrpTst_ID FROM Prof_GrpTst WHERE Profile_ID = @TGP_ID ORDER BY Ordinal"
        End If

        Using command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@TGP_ID", TGP_ID)

            Using reader As SqlDataReader = command.ExecuteReader()
                While reader.Read()
                    Dim TestID As Integer = Convert.ToInt32(reader(0))
                    If IsResultable(TestID) AndAlso Not InReqTests(AccID, TestID) Then
                        InsertTestResult(AccID, TestID, Ordinal, connection)
                        Ordinal += 1
                    End If
                    GoodTests.Add(TestID.ToString())
                End While
            End Using
        End Using
    End Sub
    Private Sub CleanUpResults(ByVal AccID As Long, ByVal GoodTests As List(Of String), ByVal connection As SqlConnection)
        If GoodTests.Count = 0 Then Exit Sub

        Dim testList As String = String.Join(", ", GoodTests)

        Dim queryClean As String = $"DELETE FROM Acc_Results WHERE Accession_ID = @AccID AND Test_ID NOT IN ({testList});
                                DELETE FROM Ref_Results WHERE Accession_ID = @AccID AND Reflexer_ID NOT IN ({testList});
                                DELETE FROM Req_Info_Response WHERE Accession_ID = @AccID AND TGP_ID NOT IN ({testList});
                                DELETE FROM Acc_Info_Results WHERE Accession_ID = @AccID AND Test_ID NOT IN ({testList})"

        Using cmd As New SqlCommand(queryClean, connection)
            cmd.Parameters.AddWithValue("@AccID", AccID)
            cmd.ExecuteNonQuery()
        End Using
    End Sub


    'Private Sub UpdateReqInfoResponse(ByVal AccID As Long, ByVal TestID As Integer)
    '    Dim CNP As New ADODB.Connection
    '    CNP.Open(odbCS)
    '    Dim Rs As New ADODB.Recordset
    '    Rs.Open("Select * from Tests where IsActive <> 0 and HasResult <> 0 and PreAnalytical " &
    '    "<> 0 and ID in (Select Info_ID from TGP_Info where TGP_ID = " & TestID & ")", CNP,
    '    ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Not Rs.BOF Then
    '        Dim GInfos As String = ""
    '        Do Until Rs.EOF
    '            Dim RsI As New ADODB.Recordset
    '            RsI.Open("Select * from Req_Info_Response where Accession_ID = " & AccID &
    '            " and TGP_ID = " & TestID & " and Info_ID = " & Rs.Fields("ID").Value,
    '            CNP, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
    '            If RsI.BOF Then RsI.AddNew()
    '            RsI.Fields("Accession_ID").Value = AccID
    '            RsI.Fields("TGP_ID").Value = TestID
    '            RsI.Fields("Info_ID").Value = Rs.Fields("ID").Value
    '            If RsI.Fields("Flag").Value Is System.DBNull.Value _
    '            Then RsI.Fields("Flag").Value = ""
    '            If RsI.Fields("UOM").Value Is System.DBNull.Value Then _
    '            RsI.Fields("UOM").Value = Rs.Fields("UOM").Value
    '            RsI.Update()
    '            RsI.Close()
    '            RsI = Nothing
    '            GInfos += Rs.Fields("ID").Value.ToString & ", "
    '            Rs.MoveNext()
    '        Loop
    '        If GInfos.Length > 2 And Microsoft.VisualBasic.Right(GInfos, 2) = ", " _
    '        Then GInfos = Microsoft.VisualBasic.Mid(GInfos, 1, Len(GInfos) - 2)
    '        If GInfos <> "" Then CNP.Execute("Delete from Req_Info_Response where Accession_ID = " &
    '        AccID & " and TGP_ID = " & TestID & " and Not Info_ID in (" & GInfos & ")")
    '    Else
    '        CNP.Execute("Delete from Req_Info_Response where TGP_ID = " &
    '        TestID & " and Accession_ID = " & AccID)
    '    End If
    '    Rs.Close()
    '    Rs = Nothing
    '    CNP.Close()
    '    CNP = Nothing
    'End Sub

    Private Sub UpdateReqInfoResponse(ByVal AccID As Long, ByVal TestID As Integer)
        Using connection As New SqlConnection(connString)
            connection.Open()

            ' Retrieve relevant Info_IDs from Tests and TGP_Info
            Dim queryTests As String = "SELECT ID, UOM FROM Tests 
                                    WHERE IsActive <> 0 AND HasResult <> 0 AND PreAnalytical <> 0 
                                    AND ID IN (SELECT Info_ID FROM TGP_Info WHERE TGP_ID = @TestID)"

            Dim validInfoIDs As New List(Of String)
            Using commandTests As New SqlCommand(queryTests, connection)
                commandTests.Parameters.AddWithValue("@TestID", TestID)

                Using readerTests As SqlDataReader = commandTests.ExecuteReader()
                    While readerTests.Read()
                        Dim InfoID As String = readerTests("ID").ToString()

                        ' Check if Info_ID already exists in Req_Info_Response
                        Dim queryCheck As String = "SELECT COUNT(*) FROM Req_Info_Response WHERE Accession_ID = @AccID AND TGP_ID = @TestID AND Info_ID = @InfoID"
                        Using checkCmd As New SqlCommand(queryCheck, connection)
                            checkCmd.Parameters.AddWithValue("@AccID", AccID)
                            checkCmd.Parameters.AddWithValue("@TestID", TestID)
                            checkCmd.Parameters.AddWithValue("@InfoID", InfoID)

                            Dim exists As Boolean = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0
                            If Not exists Then
                                ' Insert new record
                                Dim queryInsert As String = "INSERT INTO Req_Info_Response (Accession_ID, TGP_ID, Info_ID, Flag, UOM) 
                                                         VALUES (@AccID, @TestID, @InfoID, '', @UOM)"
                                Using insertCmd As New SqlCommand(queryInsert, connection)
                                    insertCmd.Parameters.AddWithValue("@AccID", AccID)
                                    insertCmd.Parameters.AddWithValue("@TestID", TestID)
                                    insertCmd.Parameters.AddWithValue("@InfoID", InfoID)
                                    insertCmd.Parameters.AddWithValue("@UOM", If(IsDBNull(readerTests("UOM")), "", readerTests("UOM").ToString()))
                                    insertCmd.ExecuteNonQuery()
                                End Using
                            End If
                        End Using

                        validInfoIDs.Add($"'{InfoID}'")
                    End While
                End Using
            End Using

            ' Cleanup old entries that are no longer valid
            If validInfoIDs.Count > 0 Then
                Dim queryDelete As String = $"DELETE FROM Req_Info_Response WHERE Accession_ID = @AccID AND TGP_ID = @TestID AND Info_ID NOT IN ({String.Join(", ", validInfoIDs)})"
                Using deleteCmd As New SqlCommand(queryDelete, connection)
                    deleteCmd.Parameters.AddWithValue("@AccID", AccID)
                    deleteCmd.Parameters.AddWithValue("@TestID", TestID)
                    deleteCmd.ExecuteNonQuery()
                End Using
            Else
                ' Delete all if no matching info exists
                Dim queryDeleteAll As String = "DELETE FROM Req_Info_Response WHERE Accession_ID = @AccID AND TGP_ID = @TestID"
                Using deleteCmdAll As New SqlCommand(queryDeleteAll, connection)
                    deleteCmdAll.Parameters.AddWithValue("@AccID", AccID)
                    deleteCmdAll.Parameters.AddWithValue("@TestID", TestID)
                    deleteCmdAll.ExecuteNonQuery()
                End Using
            End If
        End Using
    End Sub

    'Private Function GetAgeSex(ByVal AccID As Long) As String()
    '    Dim AgeSex() As String = {"", ""}
    '    Dim CNP As New ADODB.Connection
    '    CNP.Open(odbCS)
    '    Dim Rs As New ADODB.Recordset
    '    Rs.Open("Select a.AccessionDate as AccDate, b.DOB as DOB, b.Sex as Sex from Requisitions a " &
    '    "inner join Patients b on a.Patient_ID = b.ID where a.ID = " & AccID, CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Not Rs.BOF Then
    '        AgeSex(0) = CStr(CInt(DateDiff(DateInterval.Year, Rs.Fields("DOB").Value, Rs.Fields("AccDate").Value)))
    '        AgeSex(1) = Rs.Fields("Sex").Value
    '    End If
    '    Rs.Close()
    '    Rs = Nothing
    '    CNP.Close()
    '    CNP = Nothing
    '    Return AgeSex
    'End Function
    Private Function GetAgeSex(ByVal AccID As Long) As String()
        Dim AgeSex() As String = {"", ""}

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT a.AccessionDate AS AccDate, b.DOB, b.Sex 
                               FROM Requisitions a 
                               INNER JOIN Patients b ON a.Patient_ID = b.ID 
                               WHERE a.ID = @AccID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@AccID", AccID)

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        AgeSex(0) = CStr(CInt(DateDiff(DateInterval.Year, Convert.ToDateTime(reader("DOB")), Convert.ToDateTime(reader("AccDate")))))
                        AgeSex(1) = reader("Sex").ToString()
                    End If
                End Using
            End Using
        End Using

        Return AgeSex
    End Function


    'Private Function GetTGPExecutionInfinite(ByVal ProviderID As Long, ByVal PatID As Long,
    'ByVal TGPID As Integer, ByVal FromDate As Date, ByVal ToDate As Date) As String()
    '    Dim TGPExecData() As String = {"", ""}
    '    ToDate = CDate(Format(ToDate, SystemConfig.DateFormat) & " 23:59:59")
    '    Dim CNP As New ADODB.Connection
    '    CNP.Open(odbCS)
    '    Dim Rs As New ADODB.Recordset
    '    Rs.Open("Select distinct Count(ID) as Execs from Requisitions where AccessionLoc_ID " &
    '    "= 2 and Patient_ID = " & PatID & " and OrderingProvider_ID = " & ProviderID &
    '    " and AccessionDate between '" & FromDate & "' and '" & ToDate & "' and ID in " &
    '    "(Select Accession_ID from Req_TGP where TGP_ID = " & TGPID & ")", CNP,
    '    ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Rs.Fields("Execs").Value IsNot System.DBNull.Value _
    '    Then TGPExecData(0) = Rs.Fields("Execs").Value.ToString
    '    Rs.Close()
    '    '
    '    Rs.Open("Select Max(AccessionDate) as LastDate from Requisitions where AccessionLoc_ID " &
    '    "= 2 and Patient_ID = " & PatID & " and OrderingProvider_ID = " & ProviderID & " and " &
    '    "AccessionDate between '" & FromDate & "' and '" & ToDate & "' and ID in (Select " &
    '    "Accession_ID from Req_TGP where TGP_ID = " & TGPID & ")", CNP,
    '    ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Rs.Fields("LastDate").Value IsNot System.DBNull.Value _
    '    Then TGPExecData(1) = Format(Rs.Fields("LastDate").Value, SystemConfig.DateFormat)
    '    Rs.Close()
    '    Rs = Nothing
    '    CNP.Close()
    '    CNP = Nothing
    '    Return TGPExecData
    'End Function

    Private Function GetTGPExecutionInfinite(ByVal ProviderID As Long, ByVal PatID As Long,
                                         ByVal TGPID As Integer, ByVal FromDate As Date, ByVal ToDate As Date) As String()
        Dim TGPExecData() As String = {"", ""}
        ToDate = Convert.ToDateTime(Format(ToDate, SystemConfig.DateFormat) & " 23:59:59")

        Using connection As New SqlConnection(connString)
            connection.Open()

            ' Query for execution count
            Dim queryExecCount As String = "SELECT COUNT(DISTINCT ID) AS Execs FROM Requisitions 
                                        WHERE AccessionLoc_ID = 2 AND Patient_ID = @PatID 
                                        AND OrderingProvider_ID = @ProviderID AND AccessionDate BETWEEN @FromDate AND @ToDate 
                                        AND ID IN (SELECT Accession_ID FROM Req_TGP WHERE TGP_ID = @TGPID)"

            Using commandExec As New SqlCommand(queryExecCount, connection)
                commandExec.Parameters.AddWithValue("@PatID", PatID)
                commandExec.Parameters.AddWithValue("@ProviderID", ProviderID)
                commandExec.Parameters.AddWithValue("@FromDate", FromDate)
                commandExec.Parameters.AddWithValue("@ToDate", ToDate)
                commandExec.Parameters.AddWithValue("@TGPID", TGPID)

                Dim execResult As Object = commandExec.ExecuteScalar()
                If execResult IsNot Nothing AndAlso Not IsDBNull(execResult) Then
                    TGPExecData(0) = execResult.ToString()
                End If
            End Using

            ' Query for last execution date
            Dim queryLastDate As String = "SELECT MAX(AccessionDate) AS LastDate FROM Requisitions 
                                       WHERE AccessionLoc_ID = 2 AND Patient_ID = @PatID 
                                       AND OrderingProvider_ID = @ProviderID AND AccessionDate BETWEEN @FromDate AND @ToDate 
                                       AND ID IN (SELECT Accession_ID FROM Req_TGP WHERE TGP_ID = @TGPID)"

            Using commandLastDate As New SqlCommand(queryLastDate, connection)
                commandLastDate.Parameters.AddWithValue("@PatID", PatID)
                commandLastDate.Parameters.AddWithValue("@ProviderID", ProviderID)
                commandLastDate.Parameters.AddWithValue("@FromDate", FromDate)
                commandLastDate.Parameters.AddWithValue("@ToDate", ToDate)
                commandLastDate.Parameters.AddWithValue("@TGPID", TGPID)

                Dim lastDateResult As Object = commandLastDate.ExecuteScalar()
                If lastDateResult IsNot Nothing AndAlso Not IsDBNull(lastDateResult) Then
                    TGPExecData(1) = Format(Convert.ToDateTime(lastDateResult), SystemConfig.DateFormat)
                End If
            End Using
        End Using

        Return TGPExecData
    End Function


    'Private Function GetTGPExecsTimed(ByVal ProviderID As Long, ByVal PatID As Long,
    'ByVal TGPID As Integer, ByVal StartDate As Date, ByVal ExpireDate As Date) As String()
    '    Dim TGPExecData() As String = {"", ""}  '0 = Total Execs, 1 = Last Exec Date
    '    ExpireDate = CDate(Format(ExpireDate, SystemConfig.DateFormat) & " 23:59:00")
    '    Dim CNP As New ADODB.Connection
    '    CNP.Open(odbCS)
    '    Dim Rs As New ADODB.Recordset
    '    '
    '    Rs.Open("Select distinct Count(ID) as Execs from Requisitions where Patient_ID = " &
    '    PatID & " and AccessionLoc_ID in (5, 6) and OrderingProvider_ID = " & ProviderID &
    '    " and AccessionDate between '" & StartDate & "' and '" & ExpireDate & "' and ID " &
    '    "in (Select Accession_ID from Req_TGP where TGP_ID = " & TGPID & ")", CNP,
    '    ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Rs.Fields("Execs").Value IsNot System.DBNull.Value _
    '    Then TGPExecData(0) = Rs.Fields("Execs").Value.ToString
    '    Rs.Close()
    '    ' 
    '    Rs.Open("Select Max(AccessionDate) as LastDate from Requisitions where Patient_ID " &
    '    "= " & PatID & " and AccessionLoc_ID in (5, 6) and OrderingProvider_ID = " &
    '    ProviderID & " and AccessionDate between '" & StartDate & "' and '" & ExpireDate &
    '    "' and ID in (Select Accession_ID from Req_TGP where TGP_ID = " & TGPID & ")", CNP,
    '    ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Rs.Fields("LastDate").Value IsNot System.DBNull.Value _
    '    Then TGPExecData(1) = Format(Rs.Fields("LastDate").Value, SystemConfig.DateFormat)
    '    Rs.Close()
    '    Rs = Nothing
    '    CNP.Close()
    '    CNP = Nothing
    '    Return TGPExecData
    'End Function

    Private Function GetTGPExecsTimed(ByVal ProviderID As Long, ByVal PatID As Long,
                                  ByVal TGPID As Integer, ByVal StartDate As Date, ByVal ExpireDate As Date) As String()
        Dim TGPExecData() As String = {"", ""}  ' 0 = Total Execs, 1 = Last Exec Date
        ExpireDate = Convert.ToDateTime(Format(ExpireDate, SystemConfig.DateFormat) & " 23:59:00")

        Using connection As New SqlConnection(connString)
            connection.Open()

            ' Query for execution count
            Dim queryExecCount As String = "SELECT COUNT(DISTINCT ID) AS Execs FROM Requisitions 
                                        WHERE Patient_ID = @PatID AND AccessionLoc_ID IN (5, 6) 
                                        AND OrderingProvider_ID = @ProviderID 
                                        AND AccessionDate BETWEEN @StartDate AND @ExpireDate 
                                        AND ID IN (SELECT Accession_ID FROM Req_TGP WHERE TGP_ID = @TGPID)"

            Using commandExec As New SqlCommand(queryExecCount, connection)
                commandExec.Parameters.AddWithValue("@PatID", PatID)
                commandExec.Parameters.AddWithValue("@ProviderID", ProviderID)
                commandExec.Parameters.AddWithValue("@StartDate", StartDate)
                commandExec.Parameters.AddWithValue("@ExpireDate", ExpireDate)
                commandExec.Parameters.AddWithValue("@TGPID", TGPID)

                Dim execResult As Object = commandExec.ExecuteScalar()
                If execResult IsNot Nothing AndAlso Not IsDBNull(execResult) Then
                    TGPExecData(0) = execResult.ToString()
                End If
            End Using

            ' Query for last execution date
            Dim queryLastDate As String = "SELECT MAX(AccessionDate) AS LastDate FROM Requisitions 
                                       WHERE Patient_ID = @PatID AND AccessionLoc_ID IN (5, 6) 
                                       AND OrderingProvider_ID = @ProviderID 
                                       AND AccessionDate BETWEEN @StartDate AND @ExpireDate 
                                       AND ID IN (SELECT Accession_ID FROM Req_TGP WHERE TGP_ID = @TGPID)"

            Using commandLastDate As New SqlCommand(queryLastDate, connection)
                commandLastDate.Parameters.AddWithValue("@PatID", PatID)
                commandLastDate.Parameters.AddWithValue("@ProviderID", ProviderID)
                commandLastDate.Parameters.AddWithValue("@StartDate", StartDate)
                commandLastDate.Parameters.AddWithValue("@ExpireDate", ExpireDate)
                commandLastDate.Parameters.AddWithValue("@TGPID", TGPID)

                Dim lastDateResult As Object = commandLastDate.ExecuteScalar()
                If lastDateResult IsNot Nothing AndAlso Not IsDBNull(lastDateResult) Then
                    TGPExecData(1) = Format(Convert.ToDateTime(lastDateResult), SystemConfig.DateFormat)
                End If
            End Using
        End Using

        Return TGPExecData
    End Function

    Private Function IsTGPOrderable(ByVal TGPID As Integer, ByVal Interval As String,
    ByVal Qty As Integer, ByVal TGPExpire() As String, ByVal OrderID As Long, ByVal _
    OrderType As Integer, ByVal ProviderID As Long, ByVal PatID As Long, ByVal _
    StartDate As Date, ByVal AccDate As Date) As Boolean
        Dim Orderable As Boolean = False
        AccDate = CDate(Format(AccDate, SystemConfig.DateFormat))
        Dim MaxDate As Date = AccDate
        Dim MinDate As Date = StartDate
        Dim TGPExecData() As String = {"", ""} '0=Count, 1=date
        If UCase(Trim(Interval)) = "DAY" Then
            If OrderType = 0 Then   'Infinite
                MinDate = AccDate
                MaxDate = AccDate
                TGPExecData = GetTGPExecutionInfinite(ProviderID, PatID, TGPID, MinDate, MaxDate)
                If Val(TGPExecData(0)) < 1 Then Orderable = True
            ElseIf OrderType = 1 Then    'timed
                MinDate = StartDate
                If IsDate(TGPExpire(1)) Then
                    MaxDate = CDate(TGPExpire(1))
                Else
                    MaxDate = AccDate
                End If
                TGPExecData = GetTGPExecsTimed(ProviderID, PatID, TGPID, MinDate, MaxDate)
                '
                If TGPExpire(1) <> "" AndAlso Val(TGPExpire(1)) > Val(TGPExecData(0)) Then
                    Orderable = True
                ElseIf TGPExpire(0) <> "" AndAlso IsDate(TGPExpire(0)) AndAlso
                AccDate <= CDate(TGPExpire(0)) AndAlso (TGPExecData(1) = "" OrElse
                AccDate >= DateAdd(DateInterval.Day, 1 * Qty, CDate(TGPExecData(1)))) Then
                    Orderable = True
                End If
            End If
        ElseIf UCase(Trim(Interval)) = "WEEK" Then
            If OrderType = 0 Then    'infinite
                MinDate = DateAdd(DateInterval.Day, -7 * Qty, AccDate)
                MaxDate = AccDate
                TGPExecData = GetTGPExecutionInfinite(ProviderID, PatID, TGPID, MinDate, MaxDate)
                If Val(TGPExecData(0)) < 1 Then Orderable = True
            Else    'Timed
                MinDate = StartDate
                MaxDate = AccDate
                TGPExecData = GetTGPExecsTimed(ProviderID, PatID, TGPID, MinDate, MaxDate)
                '
                If TGPExpire(1) <> "" AndAlso Val(TGPExpire(1)) > Val(TGPExecData(0)) Then
                    Orderable = True
                ElseIf TGPExpire(0) <> "" AndAlso IsDate(TGPExpire(0)) AndAlso
                AccDate <= CDate(TGPExpire(0)) AndAlso (TGPExecData(1) = "" OrElse
                AccDate >= DateAdd(DateInterval.Day, 7 * Qty, CDate(TGPExecData(1)))) Then
                    Orderable = True
                End If
            End If
        ElseIf UCase(Trim(Interval)) = "BIWEEK" Then
            If OrderType = 0 Then    'infinite
                MinDate = DateSerial(AccDate.Year, AccDate.Month, 1)
                MaxDate = DateSerial(AccDate.Year, AccDate.Month + 1, 0)
                TGPExecData = GetTGPExecutionInfinite(ProviderID, PatID, TGPID, MinDate, MaxDate)
                If Val(TGPExecData(0)) < 2 AndAlso (TGPExecData(1) = "" OrElse
                (IsDate(TGPExecData(1)) And (CDate(TGPExecData(1)) < AccDate))) _
                Then Orderable = True
            Else    'Timed
                MinDate = StartDate
                MaxDate = AccDate
                TGPExecData = GetTGPExecsTimed(ProviderID, PatID, TGPID, MinDate, MaxDate)
                '
                If TGPExpire(1) <> "" AndAlso Val(TGPExpire(1)) > Val(TGPExecData(0)) Then
                    Orderable = True
                ElseIf TGPExpire(0) <> "" AndAlso IsDate(TGPExpire(0)) AndAlso
                AccDate <= CDate(TGPExpire(0)) AndAlso (TGPExecData(1) = "" OrElse
                AccDate >= DateAdd(DateInterval.Day, 14 * Qty, CDate(TGPExecData(1)))) Then
                    Orderable = True
                End If
            End If
        ElseIf UCase(Trim(Interval)) = "MONTH" Then
            If OrderType = 0 Then    'infinite
                MinDate = DateSerial(AccDate.Year, AccDate.Month, 1)
                MaxDate = DateSerial(AccDate.Year, AccDate.Month + 1, 0)
                TGPExecData = GetTGPExecutionInfinite(ProviderID, PatID, TGPID, MinDate, MaxDate)
                If Val(TGPExecData(0)) < 1 Then Orderable = True
            Else    'Timed
                MinDate = StartDate 'component startdate
                MaxDate = AccDate
                '
                TGPExecData = GetTGPExecsTimed(ProviderID, PatID, TGPID, MinDate, MaxDate)
                If TGPExpire(1) <> "" AndAlso Val(TGPExpire(1)) _
                > Val(TGPExecData(0)) Then
                    'Executions based
                    Orderable = True
                ElseIf TGPExpire(0) <> "" AndAlso IsDate(TGPExpire(0)) AndAlso _
                AccDate < CDate(TGPExpire(0)) AndAlso TGPExecData(1) = "" Then
                    Orderable = True
                ElseIf IsDate(TGPExecData(1)) AndAlso _
                (AccDate >= DateAdd(DateInterval.Month, 1 * Qty, CDate(TGPExecData(1)))) Then
                    'Exec date based
                    Orderable = True
                End If
            End If
        ElseIf UCase(Trim(Interval)) = "QUARTER" Then
            If OrderType = 0 Then   'Infinite
                Dim Q As Integer = DatePart(DateInterval.Quarter, AccDate)
                If Q = 1 Then
                    MinDate = DateSerial(AccDate.Year, 1, 1)
                    MaxDate = DateSerial(AccDate.Year, 3, 31)
                ElseIf Q = 2 Then
                    MinDate = DateSerial(AccDate.Year, 4, 1)
                    MaxDate = DateSerial(AccDate.Year, 6, 30)
                ElseIf Q = 3 Then
                    MinDate = DateSerial(AccDate.Year, 7, 1)
                    MaxDate = DateSerial(AccDate.Year, 9, 30)
                Else    '4
                    MinDate = DateSerial(AccDate.Year, 10, 1)
                    MaxDate = DateSerial(AccDate.Year, 12, 31)
                End If
                '
                TGPExecData = GetTGPExecutionInfinite(ProviderID, PatID, TGPID, MinDate, MaxDate)
                If Val(TGPExecData(0)) < 1 Then Orderable = True
            Else    'Timed
                MinDate = StartDate 'component startdate
                MaxDate = AccDate
                '
                TGPExecData = GetTGPExecsTimed(ProviderID, PatID, TGPID, MinDate, MaxDate)
                If TGPExpire(1) <> "" AndAlso Val(TGPExpire(1)) _
                > Val(TGPExecData(0)) Then
                    'Executions based
                    Orderable = True
                ElseIf TGPExpire(0) <> "" AndAlso IsDate(TGPExpire(0)) AndAlso _
                AccDate < CDate(TGPExpire(0)) AndAlso TGPExecData(1) = "" Then
                    Orderable = True
                ElseIf IsDate(TGPExecData(1)) AndAlso _
                (AccDate >= DateAdd(DateInterval.Quarter, 1 * Qty, CDate(TGPExecData(1)))) Then
                    'Exec date based
                    Orderable = True
                End If
            End If
        ElseIf UCase(Trim(Interval)) = "SEMI-ANNUAL" Then
            If OrderType = 0 Then   'Infinite
                Dim Q As Integer = DatePart(DateInterval.Quarter, AccDate)
                Select Case Q
                    Case 1, 2
                        MinDate = DateSerial(AccDate.Year, 1, 1)
                        MaxDate = DateSerial(AccDate.Year, 6, 30)
                    Case Else
                        MinDate = DateSerial(AccDate.Year, 7, 1)
                        MaxDate = DateSerial(AccDate.Year, 12, 31)
                End Select
                TGPExecData = GetTGPExecutionInfinite(ProviderID, PatID, TGPID, MinDate, MaxDate)
                If Val(TGPExecData(0)) < 1 Then Orderable = True
            Else    'Timed
                MinDate = StartDate 'component startdate
                MaxDate = AccDate
                '
                TGPExecData = GetTGPExecsTimed(ProviderID, PatID, TGPID, MinDate, MaxDate)
                If TGPExpire(1) <> "" AndAlso Val(TGPExpire(1)) _
                > Val(TGPExecData(0)) Then
                    'Executions based
                    Orderable = True
                ElseIf TGPExpire(0) <> "" AndAlso IsDate(TGPExpire(0)) AndAlso _
                AccDate < CDate(TGPExpire(0)) AndAlso TGPExecData(1) = "" Then
                    Orderable = True
                ElseIf IsDate(TGPExecData(1)) AndAlso _
                (AccDate >= DateAdd(DateInterval.Quarter, 2 * Qty, CDate(TGPExecData(1)))) Then
                    'Exec date based
                    Orderable = True
                End If
            End If
        ElseIf UCase(Trim(Interval)) = "ANNUAL" Then    'Annual
            If OrderType = 0 Then   'Infinite
                MinDate = DateSerial(AccDate.Year, 1, 1)
                MaxDate = DateSerial(AccDate.Year, 12, 31)
                TGPExecData = GetTGPExecutionInfinite(ProviderID, PatID, TGPID, MinDate, MaxDate)
                If Val(TGPExecData(0)) < 1 Then Orderable = True
            Else    'Timed
                MinDate = StartDate 'component startdate
                MaxDate = AccDate
                '
                TGPExecData = GetTGPExecsTimed(ProviderID, PatID, TGPID, MinDate, MaxDate)
                If TGPExpire(1) <> "" AndAlso Val(TGPExpire(1)) _
                > Val(TGPExecData(0)) Then
                    'Executions based
                    Orderable = True
                ElseIf TGPExpire(0) <> "" AndAlso IsDate(TGPExpire(0)) AndAlso _
                AccDate < CDate(TGPExpire(0)) AndAlso TGPExecData(1) = "" Then
                    Orderable = True
                ElseIf IsDate(TGPExecData(1)) AndAlso _
                (AccDate >= DateAdd(DateInterval.Year, 1 * Qty, CDate(TGPExecData(1)))) Then
                    'Exec date based
                    Orderable = True
                End If
            End If
        End If
        Return Orderable
    End Function

    'Private Function GetOrderTGPs(ByVal OrderID As Long, _
    'ByVal OrderType As Integer, ByVal AccDate As Date) As String()
    '    Dim TGPS() As String = {""}
    '    Dim TGPExpire() As String = {"", ""}
    '    Dim DateFrom As String = Format(AccDate, SystemConfig.DateFormat)
    '    Dim DateTo As String = Format(AccDate, SystemConfig.DateFormat & " 23:59:00")
    '    Dim sSQL As String = "Select a.*, b.OrderingProvider_ID as Provider_ID, " & _
    '    "b.Patient_ID from Order_TGP a inner join Orders b on a.Order_ID = b.ID " & _
    '    "where b.ID = " & OrderID & " and a.StartDate <= '" & DateTo & "'"
    '    Dim PatientID As Long = GetOrderPatientID(OrderID)
    '    Dim CNP As New ADODB.Connection
    '    CNP.Open(odbCS)
    '    Dim Rs As New ADODB.Recordset
    '    Rs.Open(sSQL, CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Not Rs.BOF Then
    '        Do Until Rs.EOF
    '            If OrderType = 0 Then   'infinite
    '                TGPExpire(0) = ""
    '                TGPExpire(1) = ""
    '            Else    'Timed
    '                If Rs.Fields("MaxCount").Value Is System.DBNull.Value Then
    '                    TGPExpire(1) = ""
    '                Else
    '                    TGPExpire(1) = Rs.Fields("MaxCount").Value.ToString
    '                End If
    '                If Rs.Fields("EndDate").Value IsNot System.DBNull.Value _
    '                 AndAlso IsDate(Rs.Fields("EndDate").Value) Then
    '                    TGPExpire(0) = Format(Rs.Fields("EndDate").Value, SystemConfig.DateFormat)
    '                Else
    '                    TGPExpire(0) = ""
    '                End If
    '            End If
    '            If IsTGPOrderable(Rs.Fields("TGP_ID").Value, Rs.Fields("Interval").Value, _
    '            Rs.Fields("QTY").Value, TGPExpire, OrderID, OrderType, _
    '            Rs.Fields("Provider_ID").Value, PatientID, Rs.Fields("StartDate").Value, AccDate) Then
    '                If TGPS(UBound(TGPS)) <> "" Then ReDim Preserve TGPS(UBound(TGPS) + 1)
    '                TGPS(UBound(TGPS)) = Rs.Fields("TGP_ID").Value.ToString
    '            End If
    '            '
    '            Rs.MoveNext()
    '        Loop
    '    End If
    '    Rs.Close()
    '    Rs = Nothing
    '    CNP.Close()
    '    CNP = Nothing
    '    Return TGPS
    'End Function

    Private Function GetOrderTGPs(ByVal OrderID As Long, ByVal OrderType As Integer, ByVal AccDate As Date) As String()
        Dim TGPS As New List(Of String)
        Dim TGPExpire() As String = {"", ""}
        Dim DateFrom As String = Format(AccDate, SystemConfig.DateFormat)
        Dim DateTo As String = Format(AccDate, SystemConfig.DateFormat & " 23:59:00")
        Dim PatientID As Long = GetOrderPatientID(OrderID)

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT a.*, b.OrderingProvider_ID AS Provider_ID, b.Patient_ID 
                               FROM Order_TGP a 
                               INNER JOIN Orders b ON a.Order_ID = b.ID 
                               WHERE b.ID = @OrderID AND a.StartDate <= @DateTo"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@OrderID", OrderID)
                command.Parameters.AddWithValue("@DateTo", DateTo)

                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        If OrderType = 0 Then   ' Infinite
                            TGPExpire(0) = ""
                            TGPExpire(1) = ""
                        Else    ' Timed
                            TGPExpire(1) = If(IsDBNull(reader("MaxCount")), "", reader("MaxCount").ToString())
                            TGPExpire(0) = If(Not IsDBNull(reader("EndDate")) AndAlso IsDate(reader("EndDate")),
                                          Format(reader("EndDate"), SystemConfig.DateFormat), "")
                        End If

                        If IsTGPOrderable(reader("TGP_ID"), reader("Interval"), reader("QTY"), TGPExpire,
                                      OrderID, OrderType, reader("Provider_ID"), PatientID,
                                      reader("StartDate"), AccDate) Then
                            TGPS.Add(reader("TGP_ID").ToString())
                        End If
                    End While
                End Using
            End Using
        End Using

        Return TGPS.ToArray()
    End Function

    'Private Function GetOrderPatientID(ByVal OrderID As Long) As Long
    '    Dim PatID As Long = -1
    '    Dim CNP As New ADODB.Connection
    '    CNP.Open(odbCS)
    '    Dim Rs As New ADODB.Recordset
    '    Rs.Open("Select Patient_ID from Orders where ID = " & OrderID, CNP, _
    '    ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Not Rs.BOF Then PatID = Rs.Fields("Patient_ID").Value
    '    Rs.Close()
    '    Rs = Nothing
    '    CNP.Close()
    '    CNP = Nothing
    '    Return PatID
    'End Function

    Private Function GetOrderPatientID(ByVal OrderID As Long) As Long
        Dim PatID As Long = -1 ' Default value if no record is found

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT Patient_ID FROM Orders WHERE ID = @OrderID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@OrderID", OrderID)

                Dim result As Object = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    PatID = Convert.ToInt64(result)
                End If
            End Using
        End Using

        Return PatID
    End Function

    'Private Function GetRecurringOrders(ByVal AccDate As Date, _
    'ByVal OrderType As Integer) As String()
    '    Dim Orders() As String = {""}
    '    Dim DName As String = AccDate.ToString("ddd")
    '    Dim sSQL As String = ""
    '    If OrderType = 0 Then   'All
    '        sSQL = "Select * from Orders where Active <> 0 and OrderDate <= " & _
    '        "convert(datetime, '" & AccDate & "', 101) and ((CompleteDate is NULL) " & _
    '        "or (CompleteDate = '') or (CompleteDate > convert(datetime, '" & _
    '        AccDate & "', 101))) and CHARINDEX('" & DName & "', TestDays) > 0"
    '    ElseIf OrderType = 1 Then   'Infinite
    '        sSQL = "Select * from Orders where Active <> 0 and InfiniteTimed = 0" & _
    '        " and OrderDate <= convert(datetime, '" & AccDate & "', 101) and " & _
    '        "((CompleteDate is NULL) or (CompleteDate = '') or (CompleteDate > " & _
    '        "convert(datetime, '" & AccDate & "', 101))) and CHARINDEX('" & DName _
    '        & "', TestDays) > 0"
    '    Else    'Timed
    '        sSQL = "Select * from Orders where Active <> 0 and InfiniteTimed <> " & _
    '        "0 and OrderDate <= convert(datetime, '" & AccDate & "', 101) and " & _
    '        "((CompleteDate is NULL) or (CompleteDate = '') or (CompleteDate > " & _
    '        "convert(datetime, '" & AccDate & "', 101))) and CHARINDEX('" & _
    '        DName & "', TestDays) > 0"
    '    End If
    '    '
    '    Dim CNP As New ADODB.Connection
    '    CNP.Open(odbCS)
    '    Dim Rs As New ADODB.Recordset
    '    Rs.Open(sSQL, CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Not Rs.BOF Then
    '        Do Until Rs.EOF
    '            If Orders(UBound(Orders)) <> "" Then _
    '            ReDim Preserve Orders(UBound(Orders) + 1)
    '            Orders(UBound(Orders)) = Rs.Fields("ID").Value.ToString
    '            Rs.MoveNext()
    '        Loop
    '    End If
    '    Rs.Close()
    '    Rs = Nothing
    '    CNP.Close()
    '    CNP = Nothing
    '    Return Orders
    'End Function

    Private Function GetRecurringOrders(ByVal AccDate As Date, ByVal OrderType As Integer) As String()
        Dim Orders As New List(Of String)
        Dim DName As String = AccDate.ToString("ddd")
        Dim query As String = ""

        If OrderType = 0 Then   ' All Orders
            query = "SELECT ID FROM Orders WHERE Active <> 0 AND OrderDate <= @AccDate 
                 AND (CompleteDate IS NULL OR CompleteDate = '' OR CompleteDate > @AccDate) 
                 AND CHARINDEX(@DName, TestDays) > 0"
        ElseIf OrderType = 1 Then   ' Infinite Orders
            query = "SELECT ID FROM Orders WHERE Active <> 0 AND InfiniteTimed = 0 
                 AND OrderDate <= @AccDate 
                 AND (CompleteDate IS NULL OR CompleteDate = '' OR CompleteDate > @AccDate) 
                 AND CHARINDEX(@DName, TestDays) > 0"
        Else    ' Timed Orders
            query = "SELECT ID FROM Orders WHERE Active <> 0 AND InfiniteTimed <> 0 
                 AND OrderDate <= @AccDate 
                 AND (CompleteDate IS NULL OR CompleteDate = '' OR CompleteDate > @AccDate) 
                 AND CHARINDEX(@DName, TestDays) > 0"
        End If

        Using connection As New SqlConnection(connString)
            connection.Open()

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@AccDate", AccDate)
                command.Parameters.AddWithValue("@DName", DName)

                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Orders.Add(reader("ID").ToString())
                    End While
                End Using
            End Using
        End Using

        Return Orders.ToArray()
    End Function

    'Private Function GetTodaysAccession(ByVal AccDate As Date, ByVal PatientInfo() As String) As String
    '    Dim strAccID As String = ""
    '    Dim FDate As String = Format(AccDate, SystemConfig.DateFormat)
    '    Dim TDate As String = Format(AccDate, SystemConfig.DateFormat) & " 23:59:00"
    '    Dim CNP As New ADODB.Connection
    '    CNP.Open(odbCS)
    '    Dim Rs As New ADODB.Recordset
    '    Rs.Open("Select ID from Requisitions where Received = 0 and Patient_ID = " &
    '    Val(PatientInfo(0)) & " and " & "AccessionDate between '" & FDate & "' and '" &
    '    TDate & "'", CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Not Rs.BOF Then strAccID = Rs.Fields("ID").Value.ToString
    '    Rs.Close()
    '    Rs = Nothing
    '    CNP.Close()
    '    CNP = Nothing
    '    Return strAccID
    'End Function

    Private Function GetTodaysAccession(ByVal AccDate As Date, ByVal PatientInfo() As String) As String
        Dim strAccID As String = ""

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT ID FROM Requisitions 
                               WHERE Received = 0 AND Patient_ID = @PatientID 
                               AND AccessionDate BETWEEN @FDate AND @TDate"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@PatientID", Convert.ToInt64(PatientInfo(0)))
                command.Parameters.AddWithValue("@FDate", AccDate.Date)
                command.Parameters.AddWithValue("@TDate", AccDate.Date.AddHours(23).AddMinutes(59))

                Dim result As Object = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    strAccID = result.ToString()
                End If
            End Using
        End Using

        Return strAccID
    End Function


    'Private Function HasAccMerge(ByVal ProvID As String) As Boolean
    '    Dim AccMerge As Boolean = False
    '    Dim CNP As New ADODB.Connection
    '    CNP.Open(odbCS)
    '    Dim Rs As New ADODB.Recordset
    '    Rs.Open("Select AccConsolidate from Providers where ID = " & Val(ProvID),
    '    CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Not Rs.BOF Then AccMerge = Rs.Fields("AccConsolidate").Value
    '    Rs.Close()
    '    Rs = Nothing
    '    CNP.Close()
    '    CNP = Nothing
    '    Return AccMerge
    'End Function

    Private Function HasAccMerge(ByVal ProvID As String) As Boolean
        Dim AccMerge As Boolean = False

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT AccConsolidate FROM Providers WHERE ID = @ProvID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@ProvID", Val(ProvID))

                Dim result As Object = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    AccMerge = Convert.ToBoolean(result)
                End If
            End Using
        End Using

        Return AccMerge
    End Function

    'Private Function SaveAccession(ByVal AccDate As Date, ByVal PatientInfo() As _
    'String, ByVal OrderType As Integer, ByVal ProviderID As String, ByVal WCmnt _
    'As String) As String
    '    Dim AccID As String = ""
    '    'Try
    '    Dim strAccID As String = ""
    '    If HasAccMerge(ProviderID) = True Then
    '        strAccID = GetTodaysAccession(AccDate, PatientInfo)
    '        If strAccID <> "" Then
    '            AccID = Trim(strAccID)
    '        Else
    '            AccID = NextAccessionID(AccDate).ToString
    '        End If
    '    Else
    '        AccID = NextAccessionID(AccDate).ToString
    '    End If
    '    '
    '    Dim CNP As New ADODB.Connection
    '    CNP.Open(odbCS)
    '    Dim Rs As New ADODB.Recordset
    '    Rs.Open("Select * from Requisitions where ID = " & AccID, CNP _
    '    , ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
    '    If Rs.BOF Then Rs.AddNew()
    '    Rs.Fields("ID").Value = AccID
    '    Rs.Fields("RequisitionNo").Value = AccID.ToString
    '    Rs.Fields("EMRNo").Value = PatientInfo(11)
    '    If PatientInfo(15) = "" Then PatientInfo(15) = "1"
    '    Rs.Fields("Shift").Value = PatientInfo(15)
    '    CNP.Execute("Update Requisitions set Shift = " & PatientInfo(15) &
    '    " where Patient_ID = " & PatientInfo(0))
    '    Rs.Fields("Fasting").Value = False
    '    Rs.Fields("Received").Value = False
    '    Rs.Fields("AccessionDate").Value = AccDate
    '    Rs.Fields("SpecimenType").Value = 0
    '    Rs.Fields("AccessionedBy").Value = ThisUser.ID
    '    If OrderType = 0 Then    ' Inlab Infinite
    '        Rs.Fields("AccessionLoc_ID").Value = 4
    '    Else                    'Inlab Timed
    '        Rs.Fields("AccessionLoc_ID").Value = 5
    '    End If
    '    Rs.Fields("AnalysisStage_ID").Value = 0
    '    Rs.Fields("Reported").Value = False
    '    Rs.Fields("ReportedOn").Value = System.DBNull.Value
    '    Rs.Fields("OrderingProvider_ID").Value = ProviderID
    '    Rs.Fields("AttendingProvider_ID").Value = PatientInfo(12)
    '    Rs.Fields("Patient_ID").Value = PatientInfo(0)
    '    Rs.Fields("IsGratis").Value = False
    '    '0=PatID, 1=PPayerID, 2=PPolicy, 3=PGroup, 4=PRelation, 5= PInsured
    '    '6=SPayerID, 7=SPolicy, 8=SGroup, 9=SRelation, 10=SInsured
    '    '11=EMR, 12=AttProviderID, 13=BillingType, 14=Phleb_Loc, 15=Shift
    '    If PatientInfo(1) <> "" And PatientInfo(2) <> "" And PatientInfo(4) <> "" Then       'Ordering Provider
    '        Rs.Fields("PrimePayer_ID").Value = PatientInfo(1)
    '        SaveReqPCoverage(AccID, PatientInfo)
    '    Else
    '        Rs.Fields("PrimePayer_ID").Value = ProviderID
    '    End If
    '    '
    '    If PatientInfo(6) <> "" And PatientInfo(7) <> "" And PatientInfo(9) <> "" Then       'Ordering Provider
    '        Rs.Fields("SecondPayer_ID").Value = PatientInfo(6)
    '        SaveReqSCoverage(AccID, PatientInfo)
    '    Else
    '        Rs.Fields("SecondPayer_ID").Value = PatientInfo(0)
    '    End If
    '    Rs.Fields("BillingType_ID").Value = PatientInfo(13)
    '    Rs.Fields("WorkCmnt").Value = WCmnt
    '    'Record Payment type
    '    'Rs.Fields("PaymentType_ID").Value = 0
    '    Rs.Fields("PaymentAmount").Value = 0
    '    Rs.Fields("LastEditedOn").Value = Date.Today
    '    Rs.Fields("EditedBy").Value = ThisUser.ID
    '    Rs.Update()
    '    Rs.Close()
    '    Rs = Nothing
    '    CNP.Close()
    '    CNP = Nothing
    '    'Catch ex As Exception
    '    'End Try
    '    Return AccID
    'End Function
    Private Function SaveAccession(ByVal AccDate As Date, ByVal PatientInfo() As String,
                               ByVal OrderType As Integer, ByVal ProviderID As String, ByVal WCmnt As String) As String
        Dim AccID As String = ""

        ' Determine Accession ID based on provider merge settings
        If HasAccMerge(ProviderID) Then
            Dim strAccID As String = GetTodaysAccession(AccDate, PatientInfo)
            AccID = If(strAccID <> "", Trim(strAccID), NextAccessionID(AccDate).ToString())
        Else
            AccID = NextAccessionID(AccDate).ToString()
        End If

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim exists As Boolean

            ' Check if Accession already exists
            Dim queryCheck As String = "SELECT COUNT(*) FROM Requisitions WHERE ID = @AccID"
            Using checkCmd As New SqlCommand(queryCheck, connection)
                checkCmd.Parameters.AddWithValue("@AccID", AccID)
                exists = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0
            End Using

            ' Insert or update Accession details
            Dim query As String
            If exists Then
                query = "UPDATE Requisitions SET Shift = @Shift WHERE Patient_ID = @PatientID"
            Else
                query = "INSERT INTO Requisitions (ID, RequisitionNo, EMRNo, Shift, Fasting, Received, AccessionDate, SpecimenType, 
                     AccessionedBy, AccessionLoc_ID, AnalysisStage_ID, Reported, ReportedOn, OrderingProvider_ID, 
                     AttendingProvider_ID, Patient_ID, IsGratis, PrimePayer_ID, SecondPayer_ID, BillingType_ID, WorkCmnt, 
                     PaymentAmount, LastEditedOn, EditedBy) 
                     VALUES (@AccID, @AccID, @EMRNo, @Shift, 0, 0, @AccessionDate, 0, @AccessionedBy, @AccessionLocID, 0, 0, NULL, 
                     @ProviderID, @AttProviderID, @PatientID, 0, @PrimePayerID, @SecondPayerID, @BillingTypeID, @WorkCmnt, 0, @LastEditedOn, @EditedBy)"
            End If

            Using cmd As New SqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@AccID", AccID)
                cmd.Parameters.AddWithValue("@EMRNo", PatientInfo(11))
                cmd.Parameters.AddWithValue("@Shift", If(PatientInfo(15) = "", "1", PatientInfo(15)))
                cmd.Parameters.AddWithValue("@AccessionDate", AccDate)
                cmd.Parameters.AddWithValue("@AccessionedBy", ThisUser.ID)
                cmd.Parameters.AddWithValue("@AccessionLocID", If(OrderType = 0, 4, 5))
                cmd.Parameters.AddWithValue("@ProviderID", ProviderID)
                cmd.Parameters.AddWithValue("@AttProviderID", PatientInfo(12))
                cmd.Parameters.AddWithValue("@PatientID", PatientInfo(0))
                cmd.Parameters.AddWithValue("@PrimePayerID", If(PatientInfo(1) <> "" And PatientInfo(2) <> "" And PatientInfo(4) <> "", PatientInfo(1), ProviderID))
                cmd.Parameters.AddWithValue("@SecondPayerID", If(PatientInfo(6) <> "" And PatientInfo(7) <> "" And PatientInfo(9) <> "", PatientInfo(6), PatientInfo(0)))
                cmd.Parameters.AddWithValue("@BillingTypeID", PatientInfo(13))
                cmd.Parameters.AddWithValue("@WorkCmnt", WCmnt)
                cmd.Parameters.AddWithValue("@LastEditedOn", Date.Today)
                cmd.Parameters.AddWithValue("@EditedBy", ThisUser.ID)
                cmd.ExecuteNonQuery()
            End Using

            ' Save Coverage Details
            If PatientInfo(1) <> "" And PatientInfo(2) <> "" And PatientInfo(4) <> "" Then
                SaveReqPCoverage(AccID, PatientInfo)
            End If
            If PatientInfo(6) <> "" And PatientInfo(7) <> "" And PatientInfo(9) <> "" Then
                SaveReqSCoverage(AccID, PatientInfo)
            End If
        End Using

        Return AccID
    End Function

    'Private Sub SaveReqPCoverage(ByVal AccID As Long, ByVal PatientInfo() As String)
    '    Dim CNP As New ADODB.Connection
    '    CNP.Open(odbCS)
    '    Dim Rs As New ADODB.Recordset
    '    CNP.Execute("Delete from Req_Coverage where Accession_ID = " _
    '    & AccID & " and Payer_ID = " & PatientInfo(1))
    '    Rs.Open("Select * from Req_Coverage where Accession_ID = " & AccID &
    '    " and Payer_ID = " & PatientInfo(1), CNP, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
    '    If Rs.BOF Then Rs.AddNew()
    '    Rs.Fields("Accession_ID").Value = AccID
    '    Rs.Fields("Payer_ID").Value = PatientInfo(1)
    '    Rs.Fields("Preference").Value = "P"
    '    Rs.Fields("Insured_ID").Value = PatientInfo(5)
    '    Rs.Fields("GroupNo").Value = PatientInfo(3)
    '    Rs.Fields("PolicyNo").Value = PatientInfo(2)
    '    Rs.Fields("Relation").Value = PatientInfo(4)
    '    Rs.Fields("Copayment").Value = 0
    '    Rs.Update()
    '    Rs.Close()
    '    Rs = Nothing
    '    CNP.Close()
    '    CNP = Nothing
    'End Sub

    Private Sub SaveReqPCoverage(ByVal AccID As Long, ByVal PatientInfo() As String)
        Using connection As New SqlConnection(connString)
            connection.Open()

            ' Delete existing coverage for this Accession_ID and Payer_ID
            Dim queryDelete As String = "DELETE FROM Req_Coverage WHERE Accession_ID = @AccID AND Payer_ID = @PayerID"
            Using deleteCmd As New SqlCommand(queryDelete, connection)
                deleteCmd.Parameters.AddWithValue("@AccID", AccID)
                deleteCmd.Parameters.AddWithValue("@PayerID", PatientInfo(1))
                deleteCmd.ExecuteNonQuery()
            End Using

            ' Insert new coverage record
            Dim queryInsert As String = "INSERT INTO Req_Coverage (Accession_ID, Payer_ID, Preference, Insured_ID, GroupNo, PolicyNo, Relation, Copayment) 
                                     VALUES (@AccID, @PayerID, @Preference, @InsuredID, @GroupNo, @PolicyNo, @Relation, @Copayment)"
            Using insertCmd As New SqlCommand(queryInsert, connection)
                insertCmd.Parameters.AddWithValue("@AccID", AccID)
                insertCmd.Parameters.AddWithValue("@PayerID", PatientInfo(1))
                insertCmd.Parameters.AddWithValue("@Preference", "P")
                insertCmd.Parameters.AddWithValue("@InsuredID", PatientInfo(5))
                insertCmd.Parameters.AddWithValue("@GroupNo", PatientInfo(3))
                insertCmd.Parameters.AddWithValue("@PolicyNo", PatientInfo(2))
                insertCmd.Parameters.AddWithValue("@Relation", PatientInfo(4))
                insertCmd.Parameters.AddWithValue("@Copayment", 0)
                insertCmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub


    'Private Sub SaveReqSCoverage(ByVal AccID As Long, ByVal PatientInfo() As String)
    '    Dim CNP As New ADODB.Connection
    '    CNP.Open(odbCS)
    '    Dim Rs As New ADODB.Recordset
    '    CNP.Execute("Delete from Req_Coverage where Accession_ID = " _
    '    & AccID & " and Payer_ID = " & PatientInfo(6))
    '    Rs.Open("Select * from Req_Coverage where Accession_ID = " & AccID &
    '    " and Payer_ID = " & PatientInfo(6), CNP,
    '    ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
    '    If Rs.BOF Then Rs.AddNew()
    '    '0=PatID, 1=PPayerID, 2=PPolicy, 3=PGroup, 4=PRelation, 5= PInsured
    '    '6=SPayerID, 7=SPolicy, 8=SGroup, 9=SRelation, 10=SInsured
    '    '11=EMR, 12=AttProviderID, 13=BillingType, 14=Phleb_Loc
    '    Rs.Fields("Accession_ID").Value = AccID
    '    Rs.Fields("Payer_ID").Value = PatientInfo(6)
    '    Rs.Fields("Preference").Value = "S"
    '    Rs.Fields("Insured_ID").Value = PatientInfo(10)
    '    Rs.Fields("GroupNo").Value = PatientInfo(8)
    '    Rs.Fields("PolicyNo").Value = PatientInfo(7)
    '    Rs.Fields("Relation").Value = PatientInfo(9)
    '    Rs.Fields("Copayment").Value = 0
    '    Rs.Update()
    '    Rs.Close()
    '    Rs = Nothing
    '    CNP.Close()
    '    CNP = Nothing
    'End Sub

    Private Sub SaveReqSCoverage(ByVal AccID As Long, ByVal PatientInfo() As String)
        Using connection As New SqlConnection(connString)
            connection.Open()

            ' Delete existing secondary coverage for this Accession_ID and Payer_ID
            Dim queryDelete As String = "DELETE FROM Req_Coverage WHERE Accession_ID = @AccID AND Payer_ID = @PayerID"
            Using deleteCmd As New SqlCommand(queryDelete, connection)
                deleteCmd.Parameters.AddWithValue("@AccID", AccID)
                deleteCmd.Parameters.AddWithValue("@PayerID", PatientInfo(6))
                deleteCmd.ExecuteNonQuery()
            End Using

            ' Insert new secondary coverage record
            Dim queryInsert As String = "INSERT INTO Req_Coverage (Accession_ID, Payer_ID, Preference, Insured_ID, GroupNo, PolicyNo, Relation, Copayment) 
                                     VALUES (@AccID, @PayerID, @Preference, @InsuredID, @GroupNo, @PolicyNo, @Relation, @Copayment)"
            Using insertCmd As New SqlCommand(queryInsert, connection)
                insertCmd.Parameters.AddWithValue("@AccID", AccID)
                insertCmd.Parameters.AddWithValue("@PayerID", PatientInfo(6))
                insertCmd.Parameters.AddWithValue("@Preference", "S")
                insertCmd.Parameters.AddWithValue("@InsuredID", PatientInfo(10))
                insertCmd.Parameters.AddWithValue("@GroupNo", PatientInfo(8))
                insertCmd.Parameters.AddWithValue("@PolicyNo", PatientInfo(7))
                insertCmd.Parameters.AddWithValue("@Relation", PatientInfo(9))
                insertCmd.Parameters.AddWithValue("@Copayment", 0)
                insertCmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    'Private Function NextAccessionID(ByVal NewDate As Date) As Long
    '    Dim AccID As Long
    '    Dim MinAccID As Long = 1
    '    Dim MaxAccID As Long = -1
    '    Dim AccSeed As String = ""
    '    Dim CNP As New ADODB.Connection
    '    CNP.Open(odbCS)
    '    Dim Rs As New ADODB.Recordset
    '    Rs.Open("Select * from System_Config where Company_ID = " & MyLab.ID, CNP, _
    '    ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Not Rs.BOF Then
    '        If Rs.Fields("AccSeed").Value IsNot System.DBNull.Value _
    '        Then AccSeed = Rs.Fields("AccSeed").Value
    '    End If
    '    Rs.Close()
    '    Dim sSQL As String = ""
    '    If AccSeed <> "" Then
    '        MinAccID = CLng(Format(NewDate, AccSeed))
    '        If AccSeed.Substring(0, InStr(AccSeed, "0") - 1) = "yyMMdd" Then 'daily
    '            MaxAccID = CLng(Format(DateAdd(DateInterval.Day, 1, NewDate), AccSeed))
    '        ElseIf AccSeed.Substring(0, InStr(AccSeed, "0") - 1) = "yyMM" Then 'monthly
    '            MaxAccID = CLng(Format(DateAdd(DateInterval.Month, 1, NewDate), AccSeed))
    '        ElseIf AccSeed.Substring(0, InStr(AccSeed, "0") - 1) = "yy" Then 'yearly
    '            MaxAccID = CLng(Format(DateAdd(DateInterval.Year, 1, NewDate), AccSeed))
    '        End If
    '        If MaxAccID <> -1 And MinAccID <> -1 Then _
    '        sSQL = "Select max(ID) as LastID from Requisitions where ID in (Select " & _
    '        "ID from Requisitions where ID < " & MaxAccID & " and ID >= " & MinAccID & ")"
    '    Else
    '        sSQL = "Select max(ID) as LastID from Requisitions"
    '    End If
    '    '
    '    Rs.Open(sSQL, CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Rs.Fields("LastID").Value Is System.DBNull.Value Then
    '        AccID = MinAccID
    '    Else
    '        AccID = Rs.Fields("LastID").Value + 1
    '    End If
    '    Rs.Close()
    '    Rs = Nothing
    '    CNP.Close()
    '    CNP = Nothing
    '    Return AccID
    'End Function
    Private Function NextAccessionID(ByVal NewDate As Date) As Long
        Dim AccID As Long
        Dim MinAccID As Long = 1
        Dim MaxAccID As Long = -1
        Dim AccSeed As String = ""

        Using connection As New SqlConnection(connString)
            connection.Open()

            ' Retrieve AccSeed from System_Config
            Dim queryAccSeed As String = "SELECT AccSeed FROM System_Config WHERE Company_ID = @CompanyID"
            Using commandAccSeed As New SqlCommand(queryAccSeed, connection)
                commandAccSeed.Parameters.AddWithValue("@CompanyID", MyLab.ID)

                Dim result As Object = commandAccSeed.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    AccSeed = result.ToString()
                End If
            End Using

            ' Determine Min and Max Accession IDs based on AccSeed format
            If AccSeed <> "" Then
                MinAccID = CLng(Format(NewDate, AccSeed))

                Select Case AccSeed.Substring(0, InStr(AccSeed, "0") - 1)
                    Case "yyMMdd" ' Daily
                        MaxAccID = CLng(Format(DateAdd(DateInterval.Day, 1, NewDate), AccSeed))
                    Case "yyMM" ' Monthly
                        MaxAccID = CLng(Format(DateAdd(DateInterval.Month, 1, NewDate), AccSeed))
                    Case "yy" ' Yearly
                        MaxAccID = CLng(Format(DateAdd(DateInterval.Year, 1, NewDate), AccSeed))
                End Select

                If MaxAccID <> -1 And MinAccID <> -1 Then
                    queryAccSeed = "SELECT MAX(ID) AS LastID FROM Requisitions 
                                WHERE ID < @MaxAccID AND ID >= @MinAccID"
                Else
                    queryAccSeed = "SELECT MAX(ID) AS LastID FROM Requisitions"
                End If
            Else
                queryAccSeed = "SELECT MAX(ID) AS LastID FROM Requisitions"
            End If

            ' Retrieve the last used Accession ID
            Using commandLastID As New SqlCommand(queryAccSeed, connection)
                commandLastID.Parameters.AddWithValue("@MaxAccID", MaxAccID)
                commandLastID.Parameters.AddWithValue("@MinAccID", MinAccID)

                Dim lastIDResult As Object = commandLastID.ExecuteScalar()
                AccID = If(lastIDResult IsNot Nothing AndAlso Not IsDBNull(lastIDResult), Convert.ToInt64(lastIDResult) + 1, MinAccID)
            End Using
        End Using

        Return AccID
    End Function

    'Private Function GetPatientInfo(ByVal OrderID As Long) As String()
    '    Dim PatientInfo() As String = {"", "", "", "", "", _
    '    "", "", "", "", "", "", "", "", "", "", ""}
    '    '0=PatID, 1=PPayerID, 2=PPolicy, 3=PGroup, 4=PRelation, 5= PInsured
    '    '6=SPayerID, 7=SPolicy, 8=SGroup, 9=SRelation, 10=SInsured
    '    '11=EMR, 12=AttProviderID, 13=BillingType, 14=Phleb_Loc, 15=Shift
    '    Dim ProviderID As String = GetOrdOrderingProviderID(OrderID)
    '    Dim CNP As New ADODB.Connection
    '    CNP.Open(odbCS)
    '    Dim Rs As New ADODB.Recordset
    '    Rs.Open("Select * from Orders where ID = " & OrderID, CNP, _
    '    ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '    If Not Rs.BOF Then
    '        PatientInfo(0) = Rs.Fields("Patient_ID").Value
    '        PatientInfo(12) = Rs.Fields("AttendingProvider_ID").Value
    '        PatientInfo(13) = Rs.Fields("BillingType_ID").Value
    '        PatientInfo(14) = Rs.Fields("Phleb_Loc").Value.ToString
    '    End If
    '    Rs.Close()
    '    '
    '    If PatientInfo(0) <> "" Then
    '        Rs.Open("Select * from Coverages where Patient_ID = " & PatientInfo(0), CNP, _
    '        ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '        If Not Rs.BOF Then
    '            Do Until Rs.EOF
    '                If Rs.Fields("Preference").Value = "P" Then
    '                    PatientInfo(1) = Rs.Fields("Insurance_ID").Value.ToString
    '                    PatientInfo(2) = Rs.Fields("PolicyNo").Value.ToString
    '                    PatientInfo(3) = Rs.Fields("GroupNo").Value.ToString
    '                    PatientInfo(4) = Rs.Fields("Relation").Value.ToString
    '                    PatientInfo(5) = Rs.Fields("Insured_ID").Value.ToString
    '                Else
    '                    PatientInfo(6) = Rs.Fields("Insurance_ID").Value.ToString
    '                    PatientInfo(7) = Rs.Fields("PolicyNo").Value.ToString
    '                    PatientInfo(8) = Rs.Fields("GroupNo").Value.ToString
    '                    PatientInfo(9) = Rs.Fields("Relation").Value.ToString
    '                    PatientInfo(10) = Rs.Fields("Insured_ID").Value.ToString
    '                End If
    '                Rs.MoveNext()
    '            Loop
    '        End If
    '        Rs.Close()
    '        Rs.Open("Select * from Client_Patient where Patient_ID = " & _
    '        PatientInfo(0) & " and Provider_ID = " & ProviderID, CNP, _
    '        ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
    '        If Not Rs.BOF Then
    '            PatientInfo(11) = Trim(Rs.Fields("EMRNo").Value)
    '            If Rs.Fields("Shift").Value IsNot System.DBNull.Value _
    '            AndAlso Rs.Fields("Shift").Value >= 1 Then
    '                PatientInfo(15) = Rs.Fields("Shift").Value
    '            Else
    '                PatientInfo(15) = 1
    '            End If
    '        End If
    '        Rs.Close()
    '        Rs = Nothing
    '        CNP.Close()
    '        CNP = Nothing
    '    End If
    '    Return PatientInfo
    'End Function
    Private Function GetPatientInfo(ByVal OrderID As Long) As String()
        Dim PatientInfo() As String = {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""}
        Dim ProviderID As String = GetOrdOrderingProviderID(OrderID)

        Using connection As New SqlConnection(connString)
            connection.Open()

            ' Retrieve Patient Information from Orders
            Dim queryOrders As String = "SELECT Patient_ID, AttendingProvider_ID, BillingType_ID, Phleb_Loc 
                                     FROM Orders WHERE ID = @OrderID"

            Using commandOrders As New SqlCommand(queryOrders, connection)
                commandOrders.Parameters.AddWithValue("@OrderID", OrderID)

                Using readerOrders As SqlDataReader = commandOrders.ExecuteReader()
                    If readerOrders.Read() Then
                        PatientInfo(0) = readerOrders("Patient_ID").ToString()
                        PatientInfo(12) = readerOrders("AttendingProvider_ID").ToString()
                        PatientInfo(13) = readerOrders("BillingType_ID").ToString()
                        PatientInfo(14) = readerOrders("Phleb_Loc").ToString()
                    End If
                End Using
            End Using

            ' Retrieve Patient Coverage Information
            If PatientInfo(0) <> "" Then
                Dim queryCoverage As String = "SELECT Insurance_ID, PolicyNo, GroupNo, Relation, Insured_ID, Preference 
                                           FROM Coverages WHERE Patient_ID = @PatientID"

                Using commandCoverage As New SqlCommand(queryCoverage, connection)
                    commandCoverage.Parameters.AddWithValue("@PatientID", PatientInfo(0))

                    Using readerCoverage As SqlDataReader = commandCoverage.ExecuteReader()
                        While readerCoverage.Read()
                            If readerCoverage("Preference").ToString() = "P" Then
                                PatientInfo(1) = readerCoverage("Insurance_ID").ToString()
                                PatientInfo(2) = readerCoverage("PolicyNo").ToString()
                                PatientInfo(3) = readerCoverage("GroupNo").ToString()
                                PatientInfo(4) = readerCoverage("Relation").ToString()
                                PatientInfo(5) = readerCoverage("Insured_ID").ToString()
                            Else
                                PatientInfo(6) = readerCoverage("Insurance_ID").ToString()
                                PatientInfo(7) = readerCoverage("PolicyNo").ToString()
                                PatientInfo(8) = readerCoverage("GroupNo").ToString()
                                PatientInfo(9) = readerCoverage("Relation").ToString()
                                PatientInfo(10) = readerCoverage("Insured_ID").ToString()
                            End If
                        End While
                    End Using
                End Using

                ' Retrieve EMR and Shift Information
                Dim queryClientPatient As String = "SELECT EMRNo, Shift FROM Client_Patient 
                                                WHERE Patient_ID = @PatientID AND Provider_ID = @ProviderID"

                Using commandClientPatient As New SqlCommand(queryClientPatient, connection)
                    commandClientPatient.Parameters.AddWithValue("@PatientID", PatientInfo(0))
                    commandClientPatient.Parameters.AddWithValue("@ProviderID", ProviderID)

                    Using readerClientPatient As SqlDataReader = commandClientPatient.ExecuteReader()
                        If readerClientPatient.Read() Then
                            PatientInfo(11) = readerClientPatient("EMRNo").ToString().Trim()
                            PatientInfo(15) = If(Not IsDBNull(readerClientPatient("Shift")) AndAlso Convert.ToInt32(readerClientPatient("Shift")) >= 1,
                                             readerClientPatient("Shift").ToString(), "1")
                        End If
                    End Using
                End Using
            End If
        End Using

        Return PatientInfo
    End Function

    Private Sub btnSelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelAll.Click
        For i As Integer = 0 To dgvOrders.RowCount - 1
            dgvOrders.Rows(i).Cells(0).Value = True
        Next
        Progressready()
    End Sub

    Private Sub btnDeselAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeselAll.Click
        For i As Integer = 0 To dgvOrders.RowCount - 1
            dgvOrders.Rows(i).Cells(0).Value = False
        Next
        Progressready()
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim ClientIDs As String = ""
        For i As Integer = 0 To dgvClients.RowCount - 1
            If dgvClients.Rows(i).Cells(0).Value = True _
            AndAlso dgvClients.Rows(i).Cells(1).Value <> "" Then
                ClientIDs += dgvClients.Rows(i).Cells(1).Value & ", "
            End If
        Next
        If ClientIDs.Length > 2 Then ClientIDs = _
        ClientIDs.Substring(0, ClientIDs.Length - 2)
        If ClientIDs <> "" Then
            PopulateOrders(cmbOrderType.SelectedIndex, ClientIDs)
        Else
            dgvOrders.Rows.Clear()
        End If
        Progressready()
        btnUpdate.Enabled = False
    End Sub

    Private Sub btnSellClients_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSellClients.Click
        For i As Integer = 0 To dgvClients.RowCount - 1
            dgvClients.Rows(i).Cells(0).Value = True
        Next
        btnUpdate.Enabled = True
        dgvOrders.Rows.Clear()
        Progressready()
    End Sub

    Private Sub btnDesellClients_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDesellClients.Click
        For i As Integer = 0 To dgvClients.RowCount - 1
            dgvClients.Rows(i).Cells(0).Value = False
        Next
        btnUpdate.Enabled = False
        dgvOrders.Rows.Clear()
        Progressready()
    End Sub
End Class
