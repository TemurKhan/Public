Imports System.IO
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports System.Text

Public Class frmAutoAccession
    Private Delim As String = ""
    Private PARAMS(19) As String
    Private IsCancelled As Boolean = False

    Private Sub chkIntExt_CheckedChanged(sender As Object, e As EventArgs) Handles chkIntExt.CheckedChanged
        If chkIntExt.Checked = False Then
            chkIntExt.Text = "Internal Patients"
            dgvDiscrete.Rows.Clear()
            dgvDiscrete.RowCount = 1
            dgvDiscrete.Enabled = True
            dgvOrders.Rows.Clear()
            dgvOrders.Enabled = True
            gbExt.Enabled = False
        Else
            chkIntExt.Text = "External Patients"
            dgvDiscrete.Rows.Clear()
            dgvDiscrete.RowCount = 1
            dgvDiscrete.Enabled = False
            dgvOrders.Rows.Clear()
            dgvOrders.Enabled = False
            gbExt.Enabled = True
        End If
        txtOutput.Text = ""
        RoutineProgress()
    End Sub

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        If cmbDelimiter.SelectedIndex = 0 Then
            OpenFileDialog1.DefaultExt = "*.CSV"
        Else
            OpenFileDialog1.DefaultExt = "*.*"
        End If
        OpenFileDialog1.InitialDirectory = "C:\"
        OpenFileDialog1.Filter = "*.CSV Files|*.CSV|*.CAP Files|*.CAP|*.* All Files|*.*"
        If MsgBoxResult.Ok = OpenFileDialog1.ShowDialog Then
            txtFile.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub frmAutoAccession_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dgvDiscrete.RowCount = 1
        cmbDelimiter.SelectedIndex = 0
        If cmbDelimiter.SelectedIndex = 0 Then
            Delim = ","
        End If
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        If txtFile.Text <> "" Then
            Dim FL As New StreamReader(txtFile.Text)
            Dim Ln As String ' Going to hold one line at a time
            Ln = FL.ReadLine
            If InStr(Ln, Chr(34)) > 0 Then
                Dim p1 As Integer
                Dim p2 As Integer
                Dim oTmp As String = ""
                Dim nTmp As String = ""
                Do Until InStr(Ln, Chr(34)) = 0
                    p1 = InStr(Ln, Chr(34))
                    p2 = InStr(p1 + 1, Ln, Chr(34))
                    oTmp = Ln.Substring(p1 - 1, p2 - p1 + 1)
                    nTmp = Replace(oTmp, ",", "|")
                    nTmp = Replace(nTmp, Chr(34), "")
                    Ln = Replace(Ln, oTmp, nTmp)
                Loop
            End If
            Dim Fields() As String = Split(Ln, Delim)
            For i As Integer = 0 To Fields.Length - 1
                If Fields(i) Is Nothing Then
                    Fields(i) = ""
                Else
                    Fields(i) = Replace(Fields(i), "|", ",")
                End If
            Next
            dgvFieldMap.Rows.Clear()
            For i As Integer = 0 To Fields.Length - 1
                dgvFieldMap.Rows.Add(False, Fields(i), "")
            Next
            FL.Close()
            FL = Nothing
        End If
    End Sub

    Private Sub btnDesel_Click(sender As Object, e As EventArgs) Handles btnDesel.Click
        For i As Integer = 0 To dgvFieldMap.RowCount - 1
            dgvFieldMap.Rows(i).Cells(0).Value = False
        Next
    End Sub

    Private Sub btnSel_Click(sender As Object, e As EventArgs) Handles btnSel.Click
        For i As Integer = 0 To dgvFieldMap.RowCount - 1
            dgvFieldMap.Rows(i).Cells(0).Value = True
        Next
    End Sub

    Private Sub RoutineProgress()
        If chkIntExt.Checked = False Then
            Dim ClCount As Integer = 0
            For i As Integer = 0 To dgvDiscrete.RowCount - 1
                If dgvDiscrete.Rows(i).Cells(0).Value IsNot Nothing _
                AndAlso Val(dgvDiscrete.Rows(i).Cells(0).Value) > 0 Then
                    ClCount += 1
                End If
            Next
            If ClCount > 0 Then
                btnFetchOrders.Enabled = True
            Else
                btnFetchOrders.Enabled = False
            End If
            '
            Dim oCount As Integer = 0
            For i As Integer = 0 To dgvOrders.RowCount - 1
                If dgvOrders.Rows(i).Cells(0).Value = True Then
                    oCount += 1
                End If
            Next
            If oCount > 0 Then
                btnGenerate.Enabled = True
            Else
                btnGenerate.Enabled = False
            End If
        Else
            Dim Reqgoods As Integer = 0
            Dim Optgoods As Integer = 0
            For i As Integer = 0 To dgvFieldMap.RowCount - 1
                If CType(dgvFieldMap.Rows(i).Cells(0).Value, Boolean) = True Then
                    If (dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Last") Or _
                    dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("First") Or _
                    dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("DOB") Or _
                    dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Gend") Or _
                    dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("ACC") Or _
                    dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("CLIENT") Or _
                    dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("ATT") Or _
                    dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("COMPONENT")) Then
                        Reqgoods += 1
                    ElseIf (dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Middle") Or _
                    dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Address") Or _
                    dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("City") Or _
                    dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("State") Or _
                    dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Zip") Or _
                    dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Req") Or _
                    dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Country") Or _
                    dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Email") Or _
                    dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Phone")) Then
                        Optgoods += 1
                    End If
                End If
            Next
            If (Reqgoods = 8 Or Optgoods >= 5) Then
                btnGenerate.Enabled = True
            Else
                btnGenerate.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        If chkIntExt.Checked = False Then   'Internal patients
            Dim ClientID As String = ""
            Dim AccCount As Integer = 0
            Dim TGPS(1, 0) As String
            Dim Dxs() As String = {""}
            For i As Integer = 0 To dgvOrders.RowCount - 1
                If dgvOrders.Rows(i).Cells(0).Value = True Then
                    If TGPS(0, UBound(TGPS, 2)) <> "" Then ReDim Preserve TGPS(1, UBound(TGPS, 2) + 1)
                    TGPS(0, UBound(TGPS, 2)) = dgvOrders.Rows(i).Cells(1).Value.ToString
                    TGPS(1, UBound(TGPS, 2)) = dgvOrders.Rows(i).Cells(3).Value
                End If
            Next
            For i As Integer = 0 To dgvDiscrete.RowCount - 1
                If dgvDiscrete.Rows(i).Cells(0).Value IsNot Nothing _
                AndAlso Val(dgvDiscrete.Rows(i).Cells(0).Value) > 0 Then
                    ClientID = Val(dgvDiscrete.Rows(i).Cells(0).Value)
                    AccCount = 0
                    Dim AttProvID As String = ""
                    Dim BillType As Int16 = 0
                    Dim sSQL As String = "Select distinct a.Patient_ID, IsNull(b.Insurance_ID, 0) as PayerID, " & _
                    "IsNull(b.PolicyNo, '') as PolicyNo, IsNull(b.GroupNo, '') as GroupNo, IsNull(b.Relation, 0) " & _
                    "as Relation, IsNull(b.Insured_ID, 0) as InsuredID from Requisitions a left outer join " & _
                    "Coverages b on b.Patient_ID = a.Patient_ID where b.Preference = 'P' and IsDate(a.Reported_Final) " & _
                    "<> 0 and a.OrderingProvider_ID = " & ClientID
                    '
                    Dim cnpat As New SqlClient.SqlConnection(connString)
                    cnpat.Open()
                    Dim cmdpat As New SqlClient.SqlCommand(sSQL, cnpat)
                    cmdpat.CommandType = CommandType.Text
                    Dim drpat As SqlClient.SqlDataReader = cmdpat.ExecuteReader
                    If drpat.HasRows Then
                        While drpat.Read
                            AttProvID = GetattendingProviderID(drpat("Patient_ID"))
                            If drpat("PayerID") > 0 AndAlso drpat("PolicyNo") <> "" Then
                                BillType = 1
                            Else
                                BillType = 0
                            End If
                            '
                            Try
                                CreateInternalAccession(drpat("Patient_ID"), Date.Now, "", ClientID, AttProvID, BillType, drpat("PayerID"),
                                drpat("PolicyNo"), drpat("GroupNo"), drpat("Relation"), drpat("InsuredID"), TGPS, Dxs)
                                AccCount += 1
                            Catch ex As Exception
                            End Try
                        End While
                    End If
                    cnpat.Close()
                    cnpat = Nothing
                End If
                txtOutput.AppendText("For Client: " & ClientID &
                ", accessions created: " & AccCount.ToString & vbCrLf)
            Next
            dgvDiscrete.Rows.Clear()
            dgvOrders.Rows.Clear()
            RoutineProgress()
        Else    'External Patients
            If txtFile.Text <> "" Then
                PARAMS(0) = Trim(txtFile.Text)
                For i As Integer = 1 To PARAMS.Length - 1
                    PARAMS(i) = ""
                Next
                '
                For i As Integer = 0 To dgvFieldMap.RowCount - 1
                    If dgvFieldMap.Rows(i).Cells(0).Value = True Then
                        If dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Last") Then
                            PARAMS(1) = i.ToString
                        ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("First") Then
                            PARAMS(2) = i.ToString
                        ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Middle") Then
                            PARAMS(3) = i.ToString
                        ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("DOB") Then
                            PARAMS(4) = i.ToString
                        ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Gend") Then
                            PARAMS(5) = i.ToString
                        ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Address1") Then
                            PARAMS(6) = i.ToString
                        ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Address2") Then
                            PARAMS(7) = i.ToString
                        ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("City") Then
                            PARAMS(8) = i.ToString
                        ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("State") Then
                            PARAMS(9) = i.ToString
                        ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Zip") Then
                            PARAMS(10) = i.ToString
                        ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Country") Then
                            PARAMS(11) = i.ToString
                        ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Req") Then
                            PARAMS(12) = i.ToString
                        ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Acc") Then
                            PARAMS(13) = i.ToString
                        ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Client") Then
                            PARAMS(14) = i.ToString
                        ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Att.") Then
                            PARAMS(15) = i.ToString
                        ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Component") Then
                            PARAMS(16) = i.ToString
                        ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Dx") Then
                            PARAMS(17) = i.ToString
                        ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Email") Then
                            PARAMS(18) = i.ToString
                        ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Phone") Then
                            PARAMS(19) = i.ToString
                        End If
                    End If
                Next
                If PARAMS(0) <> "" And PARAMS(1) <> "" And PARAMS(2) <> "" And
                PARAMS(4) <> "" And PARAMS(5) <> "" And PARAMS(13) <> "" And
                PARAMS(14) <> "" And PARAMS(15) <> "" And PARAMS(16) <> "" Then
                    BW.RunWorkerAsync()
                Else
                    MsgBox("Invalid selection.", MsgBoxStyle.Critical, "Prolis")
                End If
            End If
        End If
    End Sub

    Private Sub ImportFromFile()
        Dim LogoutMins As Integer = ThisUser.LogoutMins
        'ThisUser.LogoutMins = 0
        Dim n As Long = 1
        Dim Per As Integer = 0
        Dim lineCount As Long = File.ReadAllLines(PARAMS(0)).Length
        Dim SR As New StreamReader(PARAMS(0))
        Dim Ln As String ' Going to hold one line at a time
        Do Until SR.EndOfStream
            If Not BW.CancellationPending Then
                Ln = SR.ReadLine
                If Ln IsNot Nothing AndAlso Ln <> "" Then
                    If InStr(Ln, Chr(34)) > 0 Then
                        Dim p1 As Integer
                        Dim p2 As Integer
                        Dim oTmp As String = ""
                        Dim nTmp As String = ""
                        Do Until InStr(Ln, Chr(34)) = 0
                            p1 = InStr(Ln, Chr(34))
                            p2 = InStr(p1 + 1, Ln, Chr(34))
                            oTmp = Ln.Substring(p1 - 1, p2 - p1 + 1)
                            nTmp = Replace(oTmp, ",", "|")
                            nTmp = Replace(nTmp, Chr(34), "")
                            Ln = Replace(Ln, oTmp, nTmp)
                        Loop
                    End If
                    Dim Fields() As String = Split(Ln, Delim)
                    For i As Integer = 0 To Fields.Length - 1
                        If Fields(i) Is Nothing Then
                            Fields(i) = ""
                        Else
                            Fields(i) = Replace(Fields(i), "|", ",")
                        End If
                    Next
                    'Fields(CInt(PARAMS(1)))
                    If IsDate(Fields(CInt(PARAMS(4)))) Then 'DOB
                        If Fields(CInt(PARAMS(3))) = Nothing Then Fields(CInt(PARAMS(3))) = ""
                        If Fields(CInt(PARAMS(5))) = Nothing Then Fields(CInt(PARAMS(5))) = "U"
                        If Fields(CInt(PARAMS(6))) = Nothing Then Fields(CInt(PARAMS(6))) = ""
                        If Fields(CInt(PARAMS(7))) = Nothing Then Fields(CInt(PARAMS(7))) = ""
                        If Fields(CInt(PARAMS(8))) = Nothing Then Fields(CInt(PARAMS(8))) = ""
                        If Fields(CInt(PARAMS(9))) IsNot Nothing AndAlso Fields(CInt(PARAMS(9))) <> "" Then
                            Fields(CInt(PARAMS(9))) = GetStateCode(Fields(CInt(PARAMS(9))))
                        Else
                            Fields(CInt(PARAMS(9))) = ""
                        End If
                        If Fields(CInt(PARAMS(10))) = Nothing Then Fields(CInt(PARAMS(10))) = ""
                        If Fields(CInt(PARAMS(11))) = Nothing Then Fields(CInt(PARAMS(11))) = ""
                        If Fields(CInt(PARAMS(18))) = Nothing Then Fields(CInt(PARAMS(18))) = ""
                        If Fields(CInt(PARAMS(19))) = Nothing Then Fields(CInt(PARAMS(19))) = ""
                        '
                        If Fields(CInt(PARAMS(1))) <> "" And Fields(CInt(PARAMS(2))) <> "" And
                        Fields(CInt(PARAMS(5))) <> "" Then
                            Dim PatientID As Long = UpdatePatient(connString, Fields(CInt(PARAMS(1))), Fields(CInt(PARAMS(2))),
                            Fields(CInt(PARAMS(3))), CDate(Fields(CInt(PARAMS(4)))), Fields(CInt(PARAMS(5))).Substring(0, 1) _
                            , Fields(CInt(PARAMS(19))), "", 7, Fields(CInt(PARAMS(6))), Fields(CInt(PARAMS(7))), Fields(CInt(PARAMS(8))),
                            Fields(CInt(PARAMS(9))), Fields(CInt(PARAMS(10))), Fields(CInt(PARAMS(11))), Fields(CInt(PARAMS(18))))
                            Dim AccDate As Date = CDate(Fields(CInt(PARAMS(13))) & " " & Format(Date.Now, "HH:mm:ss"))
                            Dim Coverage() As String = GetPatientPCoverage(PatientID)
                            Dim BillType As Int16 = 0
                            If Coverage(0) <> "" And Coverage(2) <> "" Then
                                BillType = 1
                            Else
                                BillType = 0
                            End If
                            Dim COMPS() As String = Split(Fields(CInt(PARAMS(16))), ",")
                            Dim TGPS(1, 0) As String
                            For i As Integer = 0 To COMPS.Length - 1
                                If Trim(COMPS(i)) <> "" Then
                                    If TGPS(0, UBound(TGPS, 2)) <> "" Then _
                                    ReDim Preserve TGPS(1, UBound(TGPS, 2) + 1)
                                    TGPS(0, UBound(TGPS, 2)) = Trim(Replace(Trim(COMPS(i)), Chr(34), ""))
                                    TGPS(1, UBound(TGPS, 2)) = GetTGPType(Trim(Replace(Trim(COMPS(i)), Chr(34), "")))
                                End If
                            Next
                            '
                            Dim Dxs() As String = Split(Trim(Fields(CInt(PARAMS(17)))), ",")
                            For i As Integer = 0 To Dxs.Length - 1
                                Dxs(i) = Trim(Dxs(i))
                            Next
                            Dim ReqNo As String = ""
                            If PARAMS(12) <> "" Then ReqNo = Fields(CInt(PARAMS(12)))
                            If ValidateClient(Fields(CInt(PARAMS(14)))) Then
                                CreateInternalAccession(PatientID, AccDate, ReqNo, Fields(CInt(PARAMS(14))), Fields(CInt(PARAMS(15))),
                                BillType, Val(Coverage(0)), Coverage(2), Coverage(1), Val(Coverage(3)), Val(Coverage(4)), TGPS, Dxs)
                                Per = n * 100 / lineCount
                                BW.ReportProgress(Per, Per.ToString & " %")
                                n += 1
                            Else
                                MsgBox("Invalid Client ID specified. System requires an existing Client, in Prolis")
                            End If
                        End If
                    End If
                End If
            Else
                Exit Do
            End If
            ThisUser.LogoutMins = LogoutMins
        Loop
        SR.Close()
        SR = Nothing
        ThisUser.LogoutMins = LogoutMins
    End Sub

    Private Function ValidateClient(ByVal ProvID As Long) As Boolean
        Dim Valid As Boolean = False
        Dim cnpid As New SqlClient.SqlConnection(connString)
        cnpid.Open()
        Dim cmdpid As New SqlClient.SqlCommand("Select * from " &
        "Providers where ID = " & ProvID, cnpid)
        cmdpid.CommandType = CommandType.Text
        Dim drpid As SqlClient.SqlDataReader = cmdpid.ExecuteReader
        If drpid.HasRows Then Valid = True
        cnpid.Close()
        cnpid = Nothing
        Return Valid
    End Function

    Private Function UpdatePatient(ByVal odbCS As String, ByVal LName As String, ByVal FName As String,
    ByVal MName As String, ByVal DOB As Date, ByVal Sex As String, ByVal Phone As String, ByVal SSN As _
    String, ByVal RaceID As Integer, ByVal Add1 As String, ByVal Add2 As String, ByVal City As String,
    ByVal State As String, ByVal Zip As String, ByVal Country As String, ByVal Email As String) As String
        Dim PatID As String = GetPatientID(LName, FName, DOB, Sex)
        If PatID = "" Then PatID = GetNextPatID()
        If Phone IsNot Nothing AndAlso Phone <> "" Then
            Phone = CleanIt(Phone)
        Else
            Phone = ""
        End If
        If Email Is Nothing Then Email = ""
        '
        Dim AddressID As String = ""
        If Add1 <> "" And City <> "" And State <> "" And Zip <> "" Then
            AddressID = GetAddressID(Add1, Add2, City, State, Zip, Country)
        End If
        Dim Retval As String = ExecuteSqlProcedure("If Exists (Select * from Patients where " &
        "ID = " & PatID & ") Update Patients Set HomePhone = '" & Phone & "', SSN = '" & CleanIt(SSN) &
        "', " & "Email = IsNull(Email, '" & Trim(Email) & "'), Race_ID = " & RaceID & ", Address_ID = " &
        IIf(AddressID <> "", AddressID, "Null") & " where ID = " & PatID & " Else Insert into Patients " &
        "(ID, LastName, FirstName, MiddleName, Sex, DOB, HomePhone, Email, SSN, Race_ID, Address_ID, " &
        "IsAlive) values (" & PatID & ", '" & Replace(Trim(LName), "'", "''") & "', '" & Replace(Trim(FName),
        "'", "''") & "', '" & Replace(Trim(MName), "'", "''") & "', '" & Sex.Substring(0, 1) & "', '" &
        DOB & "', '" & Phone & "', '" & Trim(Email) & "', '" & CleanIt(SSN) & "', " & RaceID & ", " &
        IIf(AddressID <> "", AddressID, "Null") & ", 1)")
        '
        If Retval <> "" Then PatID = ""
        Return PatID
    End Function

    'Private Function GetPatientID(ByVal odbCS As String, ByVal LName As String,
    'ByVal FName As String, ByVal DOB As Date, ByVal Sex As String) As String
    '    Dim PatID As String = ""
    '    Dim cnpid As New Odbc.OdbcConnection(odbCS)
    '    cnpid.Open()
    '    Dim cmdpid As New Odbc.OdbcCommand("Select ID from Patients where LastName = '" &
    '    Replace(Trim(LName), "'", "''") & "' and FirstName = '" & Replace(Trim(FName),
    '    "'", "''") & "' and Sex = '" & Sex.Substring(0, 1) & "' and DOB = '" & DOB & "'", cnpid)
    '    cmdpid.CommandType = CommandType.Text
    '    Dim drpid As Odbc.OdbcDataReader = cmdpid.ExecuteReader
    '    If drpid.HasRows Then
    '        While drpid.Read
    '            PatID = drpid("ID").ToString
    '        End While
    '    End If
    '    cnpid.Close()
    '    cnpid = Nothing
    '    Return PatID
    'End Function
    Private Function GetPatientID(ByVal LName As String, ByVal FName As String, ByVal DOB As Date, ByVal Sex As String) As String
        Dim PatID As String = ""

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT ID FROM Patients WHERE LastName = @LastName AND FirstName = @FirstName AND Sex = @Sex AND DOB = @DOB"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@LastName", Trim(LName).Replace("'", "''"))
                command.Parameters.AddWithValue("@FirstName", Trim(FName).Replace("'", "''"))
                command.Parameters.AddWithValue("@Sex", Sex.Substring(0, 1))
                command.Parameters.AddWithValue("@DOB", DOB)

                Dim result As Object = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    PatID = result.ToString()
                End If
            End Using
        End Using

        Return PatID
    End Function

    'Private Function GetNextPatID(ByVal odbCS As String)
    '    Dim NextID As Long = 1
    '    Dim cnnid As New Odbc.OdbcConnection(odbCS)
    '    cnnid.Open()
    '    Dim cmdnid As New Odbc.OdbcCommand("Select " &
    '    "max(ID) as LastID from Patients", cnnid)
    '    cmdnid.CommandType = CommandType.Text
    '    Dim drnid As Odbc.OdbcDataReader = cmdnid.ExecuteReader
    '    If drnid.HasRows Then
    '        While drnid.Read
    '            NextID = drnid("LastID") + 1
    '        End While
    '    End If
    '    cnnid.Close()
    '    cnnid = Nothing
    '    Return NextID
    'End Function

    Private Function GetNextPatID() As Long
        Dim NextID As Long = 1

        Using connection As New SqlConnection(connString) ' Using predefined connection string
            connection.Open()

            Dim query As String = "SELECT MAX(ID) AS LastID FROM Patients"

            Using command As New SqlCommand(query, connection)
                Dim result As Object = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    NextID = Convert.ToInt64(result) + 1
                End If
            End Using
        End Using

        Return NextID
    End Function

    Private Function ConsolidateAccession(ByVal ProviderID As Long) As Boolean
        Dim Consolidate As Boolean = False
        Dim MyConnection As New SqlConnection(connString)
        If MyConnection.State = ConnectionState.Closed Then
            MyConnection.Open()
        End If
        Dim selcmd As New SqlCommand("Select AccConsolidate from Providers where ID = " & ProviderID, MyConnection)
        selcmd.CommandType = CommandType.Text
        Try
            Dim dr As SqlDataReader = selcmd.ExecuteReader()
            If dr.HasRows Then
                While dr.Read
                    Consolidate = dr.GetValue(0)
                End While
            End If
        Catch ex As Exception
            SendMail("Accession", "ConsolidateAction", ex.Message)
        Finally
            If MyConnection.State = ConnectionState.Open Then
                MyConnection.Close()
            End If
        End Try

        Return Consolidate
    End Function

    Private Sub CreateInternalAccession(ByVal PatientID As Long, ByVal AccDate As Date, ByVal ReqNo As String, ByVal _
    OrdProvID As String, ByVal AttProvID As String, ByVal BillType As Int16, ByVal PayerID As Long, ByVal PolicyNo As String,
    ByVal GroupNo As String, ByVal Rel As Int16, ByVal InsuredID As Long, ByVal TGPS(,) As String, ByVal Dxs() As String)
        Dim AccID As String = ""
        Dim MergeAccs As Boolean = ConsolidateAccession(OrdProvID)
        If MergeAccs Then AccID = GetPatientAcc(PatientID, AccDate)
        If AccID = "" Then AccID = NextAccessionID(AccDate, PatientID)
        '
        Dim cnn As New Data.SqlClient.SqlConnection(connString)
        cnn.Open()
        Dim CMD As New Data.SqlClient.SqlCommand("Requisitions_SP", cnn)
        CMD.CommandType = Data.CommandType.StoredProcedure
        CMD.Parameters.AddWithValue("@act", "Upsert")
        CMD.Parameters.AddWithValue("@ID", AccID)
        If ReqNo Is Nothing OrElse ReqNo = "" Then
            CMD.Parameters.AddWithValue("@RequisitionNo", AccID.ToString)
        Else
            CMD.Parameters.AddWithValue("@RequisitionNo", ReqNo)
        End If
        CMD.Parameters.AddWithValue("@EMRNo", "")
        CMD.Parameters.AddWithValue("@Room", "")
        CMD.Parameters.AddWithValue("@AccessionDate", AccDate)
        CMD.Parameters.AddWithValue("@AccessionedBy", ThisUser.ID)
        CMD.Parameters.AddWithValue("@AnalysisStage_ID", 0)
        CMD.Parameters.AddWithValue("@Comment", "")
        CMD.Parameters.AddWithValue("@WorkCmnt", "")
        CMD.Parameters.AddWithValue("@RejectReason", "")
        CMD.Parameters.AddWithValue("@Rejected", False)
        CMD.Parameters.AddWithValue("@ResultHistory", SystemConfig.RESHistory)
        CMD.Parameters.AddWithValue("@AccessionLoc_ID", 0)
        CMD.Parameters.AddWithValue("@Received", 0)
        CMD.Parameters.AddWithValue("@OrderingProvider_ID", Val(OrdProvID))
        CMD.Parameters.AddWithValue("@AttendingProvider_ID", Val(AttProvID))
        CMD.Parameters.AddWithValue("@SpecimenType", 0)
        CMD.Parameters.AddWithValue("@Patient_ID", PatientID)
        CMD.Parameters.AddWithValue("@Fasting", False)
        CMD.Parameters.AddWithValue("@IsGratis", False)
        CMD.Parameters.AddWithValue("@SalesPerson_ID", GetSalesPersonID(Val(OrdProvID)))
        CMD.Parameters.AddWithValue("@BillingType_ID", BillType)
        If BillType = 0 Then
            CMD.Parameters.AddWithValue("@PrimePayer_ID", Val(OrdProvID))
        ElseIf BillType = 1 Then
            If PayerID > 0 And PolicyNo <> "" Then     'primary
                CMD.Parameters.AddWithValue("@PrimePayer_ID", PayerID)
                SaveReqPCoverage(AccID, PayerID, PolicyNo, GroupNo, Rel, InsuredID)
            End If
            'UpdateGuarantor()
        Else
            CMD.Parameters.AddWithValue("@PrimePayer_ID", PatientID)
            'UpdateGuarantor()
        End If
        Dim DirID As String = GetDefaultDirectorID()
        If DirID <> "" Then CMD.Parameters.AddWithValue("@Director_ID", Val(DirID))
        CMD.Parameters.AddWithValue("@InHouse", 1)
        CMD.Parameters.AddWithValue("@InEditReason", "")
        CMD.Parameters.AddWithValue("@Verbal", 0)
        CMD.Parameters.AddWithValue("@Shift", 1)
        CMD.Parameters.AddWithValue("@LastEditedOn", Date.Now)
        CMD.Parameters.AddWithValue("@EditedBy", ThisUser.ID)
        CMD.ExecuteNonQuery()
        CMD.Dispose()
        cnn.Close()
        cnn = Nothing
        '
        SaveReqTGP(AccID, TGPS, MergeAccs)
        SaveReqTests(AccID, MergeAccs)
        SaveReqReports(AccID, OrdProvID)
        SaveSpecimen(AccID, AccDate, MergeAccs)
        '
        If Dxs(0) <> "" Then
            SaveReqDxs(AccID, Dxs, MergeAccs)
        Else
            Dxs = GetHistoryDxs(OrdProvID, PatientID)
            If Dxs(0) <> "" Then SaveReqDxs(AccID, Dxs, MergeAccs)
        End If
    End Sub

    Private Function GetHistoryDxs(ByVal OrdProvID As Long, ByVal PatientID As Long) As String()
        Dim Dxs() As String = {""}
        Dim sSQL As String = "Select distinct a.Dx_Code from Req_Dx a inner join Requisitions b on " &
        "b.ID = a.Accession_ID where IsDate(b.Reported_Final) <> 0 and b.OrderingProvider_ID = " &
        OrdProvID & " and b.Patient_ID = " & PatientID
        '
        Dim cnhdx As New SqlClient.SqlConnection(connString)
        cnhdx.Open()
        Dim cmdhdx As New SqlClient.SqlCommand(sSQL, cnhdx)
        cmdhdx.CommandType = CommandType.Text
        Dim drhdx As SqlClient.SqlDataReader = cmdhdx.ExecuteReader
        If drhdx.HasRows Then
            While drhdx.Read
                If Dxs(UBound(Dxs)) <> "" Then ReDim Preserve Dxs(UBound(Dxs) + 1)
                Dxs(UBound(Dxs)) = drhdx("Dx_Code")
            End While
        End If
        cnhdx.Close()
        cnhdx = Nothing
        Return Dxs
    End Function

    Private Sub SaveReqDxs(ByVal AccID As Long, ByVal Dxs() As String, ByVal MergeAccs As Boolean)
        Dim i As Integer
        Dim P As Integer = 0
        If MergeAccs = False Then _
        ExecuteSqlProcedure("Delete from Req_Dx where Accession_ID = " & AccID)
        For i = 0 To Dxs.Length - 1
            If Trim(Dxs(i)) <> "" Then
                ExecuteSqlProcedure("Insert into Req_Dx (Accession_ID, Dx_Code, " &
                "IsPrimary, Ordinal) values (" & AccID & ", '" & Trim(Dxs(i)) & "', " &
                IIf(i = P, 1, 0) & ", " & i & ")")
            End If
        Next
    End Sub

    Protected Function GetPatientAcc(ByVal PatientID As Long, ByVal AccDate As Date) As String
        Dim AccID As String = ""

        Dim ssql As String = "Select ID from Requisitions where Received = 0 and Patient_ID = " &
        PatientID & " and AccessionDate between '" & AccDate.ToString("MM/dd/yyyy") & "' and '" &
        AccDate.ToString("MM/dd/yyyy") & " 23:59:00'"
        Dim MyConnection As New SqlClient.SqlConnection(connString)
        If MyConnection.State = ConnectionState.Closed Then
            MyConnection.Open()
        End If
        Dim selcmd As New SqlClient.SqlCommand(ssql, MyConnection)
        selcmd.CommandType = CommandType.Text
        Try
            Dim dr As SqlClient.SqlDataReader = selcmd.ExecuteReader()
            If dr.HasRows Then
                While dr.Read
                    AccID = dr.GetValue(0)
                End While
            End If
        Catch ex As Exception
            SendMail("Accession", "GetPatientAcc", ex.Message)
        Finally
            If MyConnection.State = ConnectionState.Open Then
                MyConnection.Close()
            End If
        End Try
        Return AccID
    End Function

    Private Sub SaveReqReports(ByVal AccID As Long, ByVal ProvID As Long)
        '0=ProvID, 1=RCO, 2=Print, 3=Prolison, 4=Interface, 
        '5=Copies, 6=RPTFax,7=Fax#, 8=RPTEmail, 9=Email
        Dim Configs() As String = GetProviderConfigs(ProvID)
        Try
            Dim sSQL As String = "If Exists (Select * from Req_RPT where Rpt_Type = 'ACC' and Base_ID = " &
            AccID & " and Provider_ID = " & ProvID & ") Update Req_RPT set EntryDate = '" & Date.Now & "', " &
            "EntrySource = 'Accession', RDM_Auto = 0, RPT_Complete = " & Convert.ToInt16(Configs(1)) & ", " &
            "RPT_Print = " & Convert.ToInt16(Configs(2)) & ", RPT_Prolison = " & Convert.ToInt16(Configs(3)) &
            ", RPT_Interface = " & Convert.ToInt16(Configs(4)) & ", RPT_Fax = " & Convert.ToInt16(Configs(6)) &
            ", Fax = '" & Configs(7) & "', RPT_Email = " & Convert.ToInt16(Configs(8)) & ", Email = '" &
            Configs(9) & "', Priority = 0, Executed = 0, Comment = '' where Rpt_Type = 'ACC' and Base_ID = " &
            AccID & " and Provider_ID = " & ProvID & " Else Insert into Req_RPT (Rpt_Type, EntryDate, " &
            "EntrySource, Base_ID, Provider_ID, RDM_Auto, Rpt_Complete, Rpt_Print, Rpt_Prolison, " &
            "Rpt_Interface, Rpt_Fax, Fax, Rpt_Email, Email, Priority, Executed, Comment) Values ('ACC', '" &
            Date.Now & "', 'Accession', " & AccID & ", " & ProvID & ", 0, " & Convert.ToInt16(Configs(1)) &
            ", " & Convert.ToInt16(Configs(2)) & ", " & Convert.ToInt16(Configs(3)) & ", " &
            Convert.ToInt16(Configs(4)) & ", " & Convert.ToInt16(Configs(6)) & ", '" & Configs(7) & "', " &
            Convert.ToInt16(Configs(8)) & ", '" & Configs(9) & "', 0, 0, '')"
            ExecuteSqlProcedure(sSQL)
        Catch ex As Exception

        End Try
    End Sub

    'Protected Function GetProviderConfigs(ByVal ProviderID As Long) As String()
    '    Dim Configs() As String = {"", "", "", "", "", "", "", "", "", ""}
    '    '0=ProvID, 1=RCO, 2=Print, 3=Prolison, 4=Interface, 5=Copies, 6=RPTFax,
    '    '7=Fax#, 8=RPTEmail, 9=Email
    '    Dim cngpc As New Odbc.OdbcConnection(odbCS)
    '    cngpc.Open()
    '    Dim cmdgpc As New Odbc.OdbcCommand("Select * from Providers where ID = " & ProviderID, cngpc)
    '    cmdgpc.CommandType = CommandType.Text
    '    Dim drgpc As Odbc.OdbcDataReader = cmdgpc.ExecuteReader
    '    If drgpc.HasRows Then
    '        While drgpc.Read
    '            Configs(0) = drgpc("ID").ToString
    '            If drgpc("RPTComplete") IsNot DBNull.Value Then
    '                Configs(1) = Convert.ToInt16(drgpc("RPTComplete")).ToString
    '            Else
    '                Configs(1) = "0"
    '            End If
    '            If drgpc("RDM_Print") IsNot DBNull.Value Then
    '                Configs(2) = Convert.ToInt16(drgpc("RDM_Print")).ToString
    '            Else
    '                Configs(2) = "0"
    '            End If
    '            If drgpc("RDM_Prolison") IsNot DBNull.Value Then
    '                Configs(3) = Convert.ToInt16(drgpc("RDM_Prolison")).ToString
    '            Else
    '                Configs(3) = "0"
    '            End If
    '            If drgpc("RDM_Interface") IsNot DBNull.Value Then
    '                Configs(4) = Convert.ToInt16(drgpc("RDM_Interface")).ToString
    '            Else
    '                Configs(4) = "0"
    '            End If
    '            If drgpc("RPTCopies") IsNot DBNull.Value _
    '            AndAlso drgpc("RPTCopies").ToString <> "" Then
    '                Configs(5) = drgpc("RPTCopies").ToString
    '            Else
    '                Configs(5) = "1"
    '            End If
    '            If drgpc("Fax") IsNot DBNull.Value AndAlso Trim(drgpc("Fax")) <> "" Then
    '                If drgpc("RDM_Fax") IsNot DBNull.Value Then
    '                    Configs(6) = Convert.ToInt16(drgpc("RDM_Fax")).ToString
    '                Else
    '                    Configs(6) = "0"
    '                End If
    '                Configs(7) = Trim(drgpc("Fax"))
    '            Else
    '                Configs(7) = ""
    '                Configs(6) = "0"
    '            End If
    '            If drgpc("Email") IsNot DBNull.Value AndAlso Trim(drgpc("Email")) <> "" Then
    '                If drgpc("RDM_Email") IsNot DBNull.Value Then
    '                    Configs(8) = Convert.ToInt16(drgpc("RDM_Email")).ToString
    '                Else
    '                    Configs(8) = "0"
    '                End If
    '                Configs(9) = Trim(drgpc("Email"))
    '            Else
    '                Configs(9) = ""
    '                Configs(8) = "0"
    '            End If
    '        End While
    '    End If
    '    cngpc.Close()
    '    cngpc = Nothing
    '    Return Configs
    'End Function
    Protected Function GetProviderConfigs(ByVal ProviderID As Long) As String()
        Dim Configs() As String = {"", "", "", "", "", "", "", "", "", ""}

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT ID, RPTComplete, RDM_Print, RDM_Prolison, RDM_Interface, RPTCopies, RDM_Fax, Fax, RDM_Email, Email FROM Providers WHERE ID = @ProviderID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@ProviderID", ProviderID)

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        Configs(0) = reader("ID").ToString()
                        Configs(1) = If(Not IsDBNull(reader("RPTComplete")), Convert.ToInt16(reader("RPTComplete")).ToString(), "0")
                        Configs(2) = If(Not IsDBNull(reader("RDM_Print")), Convert.ToInt16(reader("RDM_Print")).ToString(), "0")
                        Configs(3) = If(Not IsDBNull(reader("RDM_Prolison")), Convert.ToInt16(reader("RDM_Prolison")).ToString(), "0")
                        Configs(4) = If(Not IsDBNull(reader("RDM_Interface")), Convert.ToInt16(reader("RDM_Interface")).ToString(), "0")
                        Configs(5) = If(Not IsDBNull(reader("RPTCopies")) AndAlso reader("RPTCopies").ToString() <> "", reader("RPTCopies").ToString(), "1")
                        Configs(6) = If(Not IsDBNull(reader("RDM_Fax")), Convert.ToInt16(reader("RDM_Fax")).ToString(), "0")
                        Configs(7) = If(Not IsDBNull(reader("Fax")) AndAlso Trim(reader("Fax").ToString()) <> "", Trim(reader("Fax").ToString()), "")
                        Configs(8) = If(Not IsDBNull(reader("RDM_Email")), Convert.ToInt16(reader("RDM_Email")).ToString(), "0")
                        Configs(9) = If(Not IsDBNull(reader("Email")) AndAlso Trim(reader("Email").ToString()) <> "", Trim(reader("Email").ToString()), "")
                    End If
                End Using
            End Using
        End Using

        Return Configs
    End Function

    'Private Function GetAccessionTests(ByVal AccID As Long) As String
    '    Dim TestIDs As String = ""
    '    Dim sSQL As String = "Select a.* from Tests a inner join " &
    '    "Acc_Results b on a.ID = b.Test_ID where b.Accession_ID = " & AccID
    '    Dim cngat As New Odbc.OdbcConnection(odbCS)
    '    cngat.Open()
    '    Dim cmdgat As New Odbc.OdbcCommand(sSQL, cngat)
    '    cmdgat.CommandType = Data.CommandType.Text
    '    Dim drgat As Odbc.OdbcDataReader = cmdgat.ExecuteReader()
    '    If drgat.HasRows Then
    '        While drgat.Read
    '            If drgat("IsCalculated") <> 0 AndAlso drgat("Formula") <> "" Then
    '                TestIDs += GetTestIDsFromFormula(odbCS, drgat("Formula"))
    '                If Not TestIDs.EndsWith(", ") Then TestIDs += ", "
    '            Else
    '                If InStr(TestIDs, drgat("ID").ToString & ",") = 0 Then
    '                    TestIDs += drgat("ID").ToString & ", "
    '                End If
    '            End If
    '        End While
    '    End If
    '    cngat.Close()
    '    cngat = Nothing
    '    If TestIDs.EndsWith(", ") Then TestIDs = TestIDs.Remove(TestIDs.Length - 2, 2)
    '    Return TestIDs
    'End Function

    Private Function GetAccessionTests(ByVal AccID As Long) As String
        Dim TestIDs As New StringBuilder()

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT a.ID, a.IsCalculated, a.Formula FROM Tests a 
                               INNER JOIN Acc_Results b ON a.ID = b.Test_ID 
                               WHERE b.Accession_ID = @AccID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@AccID", AccID)

                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        If Convert.ToInt32(reader("IsCalculated")) <> 0 AndAlso Not String.IsNullOrWhiteSpace(reader("Formula").ToString()) Then
                            TestIDs.Append(GetTestIDsFromFormula(reader("Formula").ToString()))
                            If Not TestIDs.ToString().EndsWith(", ") Then TestIDs.Append(", ")
                        Else
                            If Not TestIDs.ToString().Contains(reader("ID").ToString() & ",") Then
                                TestIDs.Append(reader("ID").ToString() & ", ")
                            End If
                        End If
                    End While
                End Using
            End Using
        End Using

        ' Remove the trailing comma if present
        If TestIDs.ToString().EndsWith(", ") Then
            TestIDs.Length -= 2
        End If

        Return TestIDs.ToString()
    End Function

    'Private Function SaveSpecimen(ByVal odbCS As String, ByVal AccID _
    'As Long, ByVal CollDate As Date, ByVal MergeAccs As Boolean) As Integer
    '    Dim RetVal As Integer = 1
    '    Dim AllMats As String = ""
    '    Dim MATS(1, 0) As String    '0=MatID, 1=MatCount
    '    Dim TestIDs As String = GetAccessionTests(odbCS, AccID)
    '    'MATS(1, 0) = ""
    '    Dim sSQL As String = "Select a.MATERIAL_ID as MatID, count(a.MATERIAL_ID) as Matcount from " &
    '    "Test_Material a where a.Material_ID in (Select Top 1 b.Material_ID from Test_Material b where " &
    '    "b.TEST_ID = a.Test_ID) and a.Test_ID in (" & TestIDs & ") group by Material_ID order by Matcount desc"
    '    Dim cnss As New Odbc.OdbcConnection(odbCS)
    '    cnss.Open()
    '    Dim cmdss As New Odbc.OdbcCommand(sSQL, cnss)
    '    cmdss.CommandType = CommandType.Text
    '    Dim drss As Odbc.OdbcDataReader = cmdss.ExecuteReader
    '    If drss.HasRows Then
    '        While drss.Read
    '            If drss("MatID") IsNot DBNull.Value _
    '            AndAlso drss("MatID").ToString <> "" Then
    '                If MATS(0, UBound(MATS, 2)) IsNot Nothing AndAlso
    '                MATS(0, UBound(MATS, 2)) <> "" Then _
    '                ReDim Preserve MATS(1, UBound(MATS, 2) + 1)
    '                MATS(0, UBound(MATS, 2)) = drss("MatID").ToString
    '                MATS(1, UBound(MATS, 2)) = drss("Matcount").ToString
    '                AllMats += drss("MatID").ToString & ", "
    '            End If
    '        End While
    '    End If
    '    cnss.Close()
    '    cnss = Nothing
    '    '
    '    If MergeAccs = False Then _
    '    ExecuteSqlProcedure("Delete from Specimens where Accession_ID = " & AccID)
    '    Dim SRCID As String = ""
    '    Dim Qty As Integer = 1
    '    For i As Integer = 0 To UBound(MATS, 2) 'mats
    '        SRCID = GetSourceID(odbCS, Val(MATS(0, i)))
    '        If SRCID <> "" Then
    '            If Val(MATS(1, i)) <= 30 Then
    '                Qty = 1
    '            ElseIf Val(MATS(1, i)) > 30 And MATS(1, i) <= 60 Then
    '                Qty = 2
    '            ElseIf Val(MATS(1, i)) > 60 And MATS(1, i) <= 90 Then
    '                Qty = 3
    '            ElseIf Val(MATS(1, i)) > 90 And MATS(1, i) <= 120 Then
    '                Qty = 4
    '            ElseIf Val(MATS(1, i)) > 120 And MATS(1, i) <= 150 Then
    '                Qty = 5
    '            Else
    '                Qty = 6
    '            End If
    '            ExecuteSqlProcedure("Insert into Specimens (Accession_ID, Source_ID, " &
    '            "SourceNo, SourceQuantity, SourceDate, SourceTemp, IsReadyToUse, Comment) " &
    '            "values (" & AccID & ", " & SRCID & ", '" & AccID.ToString & "-" &
    '            (i + 1).ToString & "', " & Qty & ", '" & CollDate & "', 'Room Temp', 1, '')")
    '            '
    '            RetVal = 0
    '        End If
    '    Next
    '    Return RetVal
    'End Function
    Private Function SaveSpecimen(ByVal AccID As Long, ByVal CollDate As Date, ByVal MergeAccs As Boolean) As Integer
        Dim RetVal As Integer = 1
        Dim AllMats As New StringBuilder()
        Dim MATS As New List(Of Tuple(Of String, Integer))

        Using connection As New SqlConnection(connString)
            connection.Open()

            ' Retrieve TestIDs associated with Accession
            Dim TestIDs As String = GetAccessionTests(AccID)

            Dim query As String = "SELECT a.MATERIAL_ID AS MatID, COUNT(a.MATERIAL_ID) AS MatCount 
                               FROM Test_Material a 
                               WHERE a.Material_ID IN (SELECT TOP 1 b.Material_ID FROM Test_Material b WHERE b.TEST_ID = a.Test_ID) 
                               AND a.Test_ID IN (" & TestIDs & ") 
                               GROUP BY Material_ID 
                               ORDER BY MatCount DESC"

            Using command As New SqlCommand(query, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        If Not IsDBNull(reader("MatID")) AndAlso Not String.IsNullOrWhiteSpace(reader("MatID").ToString()) Then
                            MATS.Add(Tuple.Create(reader("MatID").ToString(), Convert.ToInt32(reader("MatCount"))))
                            AllMats.Append(reader("MatID").ToString() & ", ")
                        End If
                    End While
                End Using
            End Using

            ' Remove trailing comma
            If AllMats.ToString().EndsWith(", ") Then
                AllMats.Length -= 2
            End If

            ' Delete existing specimens if merging is disabled
            If Not MergeAccs Then
                Using deleteCmd As New SqlCommand("DELETE FROM Specimens WHERE Accession_ID = @AccID", connection)
                    deleteCmd.Parameters.AddWithValue("@AccID", AccID)
                    deleteCmd.ExecuteNonQuery()
                End Using
            End If

            ' Insert new specimens
            Dim Qty As Integer = 1
            For i As Integer = 0 To MATS.Count - 1
                Dim SRCID As String = GetSourceID(MATS(i).Item1)

                If Not String.IsNullOrEmpty(SRCID) Then
                    Select Case MATS(i).Item2
                        Case <= 30 : Qty = 1
                        Case 31 To 60 : Qty = 2
                        Case 61 To 90 : Qty = 3
                        Case 91 To 120 : Qty = 4
                        Case 121 To 150 : Qty = 5
                        Case Else : Qty = 6
                    End Select

                    Using insertCmd As New SqlCommand("INSERT INTO Specimens (Accession_ID, Source_ID, SourceNo, SourceQuantity, SourceDate, SourceTemp, IsReadyToUse, Comment) 
                                                   VALUES (@AccID, @SourceID, @SourceNo, @SourceQuantity, @SourceDate, @SourceTemp, @IsReadyToUse, @Comment)", connection)
                        insertCmd.Parameters.AddWithValue("@AccID", AccID)
                        insertCmd.Parameters.AddWithValue("@SourceID", SRCID)
                        insertCmd.Parameters.AddWithValue("@SourceNo", $"{AccID}-{i + 1}")
                        insertCmd.Parameters.AddWithValue("@SourceQuantity", Qty)
                        insertCmd.Parameters.AddWithValue("@SourceDate", CollDate)
                        insertCmd.Parameters.AddWithValue("@SourceTemp", "Room Temp")
                        insertCmd.Parameters.AddWithValue("@IsReadyToUse", 1)
                        insertCmd.Parameters.AddWithValue("@Comment", "")
                        insertCmd.ExecuteNonQuery()
                    End Using

                    RetVal = 0
                End If
            Next
        End Using

        Return RetVal
    End Function

    'Private Function GetTestFormula(ByVal TestID As Integer) As String
    '    Dim Formula As String = ""
    '    Dim cnf As New Odbc.OdbcConnection(odbCS)
    '    cnf.Open()
    '    Dim cmdf As New Odbc.OdbcCommand("Select * from Tests where " &
    '    "IsCalculated <> 0 and Formula <> '' and ID = " & TestID, cnf)
    '    cmdf.CommandType = CommandType.Text
    '    Dim drf As Odbc.OdbcDataReader = cmdf.ExecuteReader
    '    If drf.HasRows Then
    '        While drf.Read
    '            Formula = drf("Formula")
    '        End While
    '    End If
    '    cnf.Close()
    '    cnf = Nothing
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

    'Private Function GetSourceID(ByVal odbCS As String, ByVal MatID As Integer) As String
    '    Dim SRCID As String = ""
    '    Dim cnsid As New Odbc.OdbcConnection(odbCS)
    '    cnsid.Open()
    '    Dim cmdsid As New Odbc.OdbcCommand("Select ID from Sources where Material_ID = " & MatID, cnsid)
    '    cmdsid.CommandType = CommandType.Text
    '    Dim drsid As Odbc.OdbcDataReader = cmdsid.ExecuteReader
    '    If drsid.HasRows Then
    '        While drsid.Read
    '            SRCID = drsid("ID").ToString
    '        End While
    '    End If
    '    cnsid.Close()
    '    cnsid = Nothing
    '    Return SRCID
    'End Function

    Private Function GetSourceID(ByVal MatID As Integer) As String
        Dim SRCID As String = ""

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT ID FROM Sources WHERE Material_ID = @MatID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@MatID", MatID)

                Dim result As Object = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    SRCID = result.ToString()
                End If
            End Using
        End Using

        Return SRCID
    End Function

    Private Sub SaveReqTests(ByVal AccID As Long, ByVal MergeAccs As Boolean)
        Dim GoodTests As String = ""
        Dim NormalRange As String = ""
        Dim TestIDs() As String = GetTestIDsfromReqTGPS(AccID)
        '
        For i As Integer = 0 To TestIDs.Length - 1
            If TestIDs(i) <> "" Then
                NormalRange = GetNormalRange(AccID, TestIDs(i))
                Dim cnrt As New Data.SqlClient.SqlConnection(connString)
                cnrt.Open()
                Dim cmdrt As New Data.SqlClient.SqlCommand("Acc_Results_SP", cnrt)
                cmdrt.CommandType = Data.CommandType.StoredProcedure
                cmdrt.Parameters.AddWithValue("@ACT", "Upsert")
                cmdrt.Parameters.AddWithValue("@Accession_ID", AccID)
                cmdrt.Parameters.AddWithValue("@Test_ID", TestIDs(i))
                cmdrt.Parameters.AddWithValue("@Ordinal", i)
                cmdrt.Parameters.AddWithValue("@NormalRange", NormalRange)
                cmdrt.ExecuteNonQuery()
                cnrt.Close()
                cnrt = Nothing
                '
                UpdateInfoResults(AccID, TestIDs(i))
                'UpdateReqInfoResponse(AccID, TestIDs(i))
                GoodTests += TestIDs(i) & ", "
            End If
        Next
        If GoodTests.EndsWith(", ") Then GoodTests =
        Microsoft.VisualBasic.Mid(GoodTests, 1, Len(GoodTests) - 2)
        '
        If MergeAccs = False Then
            If GoodTests <> "" Then
                ExecuteSqlProcedure("Delete from Acc_Results where " &
                "Accession_ID = " & AccID & " and not Test_ID in (" & GoodTests & ")")
                ExecuteSqlProcedure("Delete from Acc_Info_Results where " &
                "Accession_ID = " & AccID & " and not Test_ID in (" & GoodTests & ")")
                ExecuteSqlProcedure("Delete from Req_Info_Response " &
                "where Accession_ID = " & AccID & " and not TGP_ID in (" & GoodTests & ")")
            End If
        End If
    End Sub

    Private Sub SaveReqTGP(ByVal AccID As Long, ByVal TGPS(,) As String, ByVal MergeAccs As Boolean)
        Dim GoodTGPs As String = ""
        TGPS = RemoveDuplicates2(TGPS)
        '
        For t As Integer = 0 To UBound(TGPS, 2)
            If Trim(TGPS(0, t)) <> "" AndAlso IsNumeric(TGPS(0, t)) Then
                'TGPType = GetTGPType(MyTGPs(0, t))
                Dim cnrtgp As New Data.SqlClient.SqlConnection(connString)
                cnrtgp.Open()
                '@Accession_ID bigint = null, @TGP_ID int = null, @TGP_Type char(1) = null, @Billed bit = 0, 
                '@Ordinal int = 0, @IsStat bit = 0, @Verbal bit = 0, @Skip_Billing bit = 0, @Dx_Code nvarchar(12) = '', @IsESRD bit = 0, @act varchar(10)
                Dim cmdupsert As New Data.SqlClient.SqlCommand("Req_TGP_SP", cnrtgp)
                cmdupsert.CommandType = Data.CommandType.StoredProcedure
                cmdupsert.Parameters.AddWithValue("@act", "Upsert")
                cmdupsert.Parameters.AddWithValue("@Accession_ID", AccID)
                cmdupsert.Parameters.AddWithValue("@TGP_ID", TGPS(0, t))
                cmdupsert.Parameters.AddWithValue("@TGP_Type", TGPS(1, t))
                cmdupsert.Parameters.AddWithValue("@Billed", 0)
                cmdupsert.Parameters.AddWithValue("@Ordinal", t)
                cmdupsert.Parameters.AddWithValue("@IsStat", 0)
                cmdupsert.Parameters.AddWithValue("@Verbal", False)
                cmdupsert.Parameters.AddWithValue("@Skip_Billing", False)
                cmdupsert.Parameters.AddWithValue("@Dx_Code", "")
                cmdupsert.Parameters.AddWithValue("@IsESRD", False)
                cmdupsert.ExecuteNonQuery()
                cmdupsert.Dispose()
                GoodTGPs += TGPS(0, t) & ", "
                cnrtgp.Close()
                cnrtgp = Nothing
            End If
        Next
        '
        If GoodTGPs.EndsWith(", ") Then GoodTGPs = Microsoft.VisualBasic.Mid(GoodTGPs, 1, Len(GoodTGPs) - 2)
        If MergeAccs = False Then
            If GoodTGPs <> "" Then
                ExecuteSqlProcedure("Delete from Req_TGP where " &
                "Accession_ID = " & AccID & " and Not TGP_ID in (" & GoodTGPs & ")")
            End If
        End If
    End Sub

    Private Sub SaveReqPCoverage(ByVal AccID As Long, ByVal PayerID As Long,
    PolicyNo As String, ByVal GroupNo As String, ByVal Rel As Int16, ByVal InsuredID As Long)
        Dim cnrp As New Data.SqlClient.SqlConnection(connString)
        cnrp.Open()
        '@Accession_ID bigint = null, @Payer_ID bigint = null, @Ordinal int = 0, @Insured_ID bigint = null, @Preference nchar(1) = '', @GroupNo nvarchar(25) = null, @PolicyNo nvarchar(25) = null, @Relation tinyint = 0, @CoPayment real = null, @WorkmanComp bit = 0, @InstanceDate smalldatetime = null, @Comment nvarchar(400) = null, @act varchar(10)
        Dim cmdupsert As New Data.SqlClient.SqlCommand("Req_Coverage_SP", cnrp)
        cmdupsert.CommandType = Data.CommandType.StoredProcedure
        cmdupsert.Parameters.AddWithValue("@act", "Upsert")
        cmdupsert.Parameters.AddWithValue("@Accession_ID", AccID)
        cmdupsert.Parameters.AddWithValue("@Payer_ID", PayerID)
        cmdupsert.Parameters.AddWithValue("@Ordinal", 0)
        cmdupsert.Parameters.AddWithValue("@Insured_ID", InsuredID)
        cmdupsert.Parameters.AddWithValue("@Preference", "P")
        cmdupsert.Parameters.AddWithValue("@GroupNo", GroupNo)
        cmdupsert.Parameters.AddWithValue("@PolicyNo", PolicyNo)
        cmdupsert.Parameters.AddWithValue("@Relation", Rel)
        cmdupsert.Parameters.AddWithValue("@CoPayment", 0)
        cmdupsert.Parameters.AddWithValue("@workmancomp", 0)
        cmdupsert.Parameters.AddWithValue("@comment", "")
        Try
            cmdupsert.ExecuteNonQuery()
        Catch ex As Exception
        End Try
        cmdupsert.Dispose()
        cnrp.Close()
        cnrp = Nothing
        '
        ExecuteSqlProcedure("Delete from Req_Coverage where " &
        "Accession_ID = " & AccID & " and Preference = 'P' and Payer_ID <> " & PayerID)
    End Sub

    Private Function NextAccessionID(ByVal AccDate As Date, ByVal PatID As String) As Long
        Dim AccID As String = ""
        Dim MinAccID As Long = 1
        Dim MaxAccID As Long = -1
        Dim sSQL As String = ""
        '
        If SystemConfig.AccSeed Is Nothing Then InitializeConfiguration(1)
        If AccID = "" Then
            If SystemConfig.AccSeed <> "" Then
                Dim AccSeed As String = SystemConfig.AccSeed
                MinAccID = CLng(Format(AccDate, AccSeed))
                If AccSeed.Substring(0, InStr(AccSeed, "0") - 1) = "yyMMdd" Then 'daily
                    MaxAccID = CLng(Format(DateAdd(DateInterval.Day, 1, AccDate), AccSeed) - 2)
                ElseIf AccSeed.Substring(0, InStr(AccSeed, "0") - 1) = "yyMM" Then 'monthly
                    MaxAccID = CLng(Format(DateAdd(DateInterval.Month, 1, AccDate), AccSeed) - 2)
                ElseIf AccSeed.Substring(0, InStr(AccSeed, "0") - 1) = "yy" Then 'yearly
                    MaxAccID = CLng(Format(DateAdd(DateInterval.Year, 1, AccDate), AccSeed) - 2)
                End If
                If MaxAccID <> -1 And MinAccID <> -1 Then _
                sSQL = "Select max(ID) as LastID from Requisitions " &
                "where ID >= " & MinAccID & " and ID < " & MaxAccID
            Else
                sSQL = "Select max(ID) as LastID from Requisitions"
            End If
            '
            Dim cnna As New SqlClient.SqlConnection(connString)
            cnna.Open()
            Dim cmdna As New SqlClient.SqlCommand(sSQL, cnna)
            cmdna.CommandType = CommandType.Text
            Dim drna As SqlClient.SqlDataReader = cmdna.ExecuteReader
            If drna.HasRows Then
                While drna.Read
                    If drna("LastID") Is DBNull.Value Then
                        AccID = MinAccID.ToString
                    Else
                        AccID = CStr(drna("LastID") + 1)
                    End If
                End While
            End If
            cnna.Close()
            cnna.Dispose()
        End If
        Return CLng(AccID)
    End Function

    Private Function GetattendingProviderID(ByVal PatientID As Long) As String
        Dim PrID As String = ""
        Dim cnatt As New SqlClient.SqlConnection(connString)
        cnatt.Open()
        Dim cmdatt As New SqlClient.SqlCommand("Select top 1 * from Requisitions " &
        "where IsDate(Reported_Final) <> 0 and Patient_ID = " & PatientID, cnatt)
        cmdatt.CommandType = CommandType.Text
        Dim dratt As SqlClient.SqlDataReader = cmdatt.ExecuteReader
        If dratt.HasRows Then
            While dratt.Read
                PrID = dratt("AttendingProvider_ID").ToString
            End While
        End If
        cnatt.Close()
        cnatt = Nothing
        Return PrID
    End Function

    Private Sub dgvDiscrete_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDiscrete.CellEndEdit
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
        Else
            If e.RowIndex < dgvDiscrete.RowCount - 1 Then
                dgvDiscrete.Rows.RemoveAt(e.RowIndex)
            End If
        End If
        RoutineProgress()
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

    Private Sub DiscreteClear()
        Dim i As Integer
        For i = dgvDiscrete.RowCount - 1 To 0 Step -1
            dgvDiscrete.Rows(i).Cells(0).Value = ""
            If i > 0 Then dgvDiscrete.Rows.RemoveAt(i)
        Next
    End Sub

    Private Sub btnFetchOrders_Click(sender As Object, e As EventArgs) Handles btnFetchOrders.Click
        Dim ClientIDs As String = ""
        dgvOrders.Rows.Clear()
        For i As Integer = 0 To dgvDiscrete.RowCount - 1
            If dgvDiscrete.Rows(i).Cells(0).Value IsNot Nothing _
            AndAlso Val(dgvDiscrete.Rows(i).Cells(0).Value) > 0 Then
                ClientIDs += Trim(dgvDiscrete.Rows(i).Cells(0).Value) & ", "
            End If
        Next
        If ClientIDs.EndsWith(", ") Then ClientIDs = ClientIDs.Substring(0, InStr(ClientIDs, ",") - 1)
        If ClientIDs <> "" Then
            Dim sSQL As String = "Select distinct 0 as Sel, a.TGP_ID, (Select Name from Tests where " &
            "ID = a.TGP_ID Union Select Name from Groups where ID = a.TGP_ID Union Select Name " &
            "from Profiles where ID = a.TGP_ID) as Component, (Select ComponentType from Tests where " &
            "ID = a.TGP_ID Union Select ComponentType from Groups where ID = a.TGP_ID Union Select " &
            "ComponentType from Profiles where ID = a.TGP_ID) as CompType from Req_TGP a inner join " &
            "Requisitions b on b.ID = a.Accession_ID where b.OrderingProvider_ID in (" & ClientIDs & ")"
            Dim cnfo As New SqlClient.SqlConnection(connString)
            cnfo.Open()
            Dim cmdfo As New SqlClient.SqlCommand(sSQL, cnfo)
            cmdfo.CommandType = CommandType.Text
            Dim drfo As SqlClient.SqlDataReader = cmdfo.ExecuteReader
            If drfo.HasRows Then
                While drfo.Read
                    dgvOrders.Rows.Add(drfo("Sel"), drfo("TGP_ID"), drfo("Component"), drfo("CompType"))
                End While
            End If
            cnfo.Close()
            cnfo = Nothing
        End If
    End Sub

    Private Sub dgvOrders_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvOrders.CellEndEdit
        RoutineProgress()
    End Sub

    Private Sub dgvDiscrete_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvDiscrete.CellMouseUp
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
            End If
            RoutineProgress()
        End If
    End Sub

    Private Sub txtID_Validated(sender As Object, e As EventArgs) Handles txtID.Validated
        If Trim(txtID.Text) <> "" Then
            txtName.Text = GetTGPName(Trim(txtID.Text))
            If txtName.Text <> "" Then
                btnAdd.Enabled = True
            Else
                MsgBox("Invalid ID, typed!")
                txtID.Text = ""
                txtName.Text = ""
                btnAdd.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If Trim(txtID.Text) <> "" And txtName.Text <> "" Then
            If Not IsTGPDuplicate(Trim(txtID.Text), dgvOrders) Then
                dgvOrders.Rows.Add(False, Trim(txtID.Text), _
                txtName.Text, GetTGPType(Trim(txtID.Text)))
                txtID.Text = ""
                txtName.Text = ""
                btnAdd.Enabled = False
            End If
        End If
    End Sub

    Private Function IsTGPDuplicate(ByVal TGPID As Integer, ByVal dgv As DataGridView) As Boolean
        Dim Dup As Boolean = False
        For i As Integer = 0 To dgv.RowCount - 1
            If dgv.Rows(i).Cells(1).Value = TGPID Then
                Dup = True
                Exit For
            End If
        Next
        Return Dup
    End Function

    Private Sub dgvFieldMap_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFieldMap.CellEndEdit
        RoutineProgress()
    End Sub

    Private Sub BW_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BW.DoWork
        ImportFromFile()
    End Sub

    Private Sub BW_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BW.ProgressChanged
        Try
            PB.Value = e.ProgressPercentage
            lblStatus.Text = e.UserState
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BW_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BW.RunWorkerCompleted
        txtFile.Text = ""
        dgvFieldMap.Rows.Clear()
        cmbDelimiter.SelectedIndex = 0
    End Sub

    Private Sub cmbDelimiter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDelimiter.SelectedIndexChanged
        If cmbDelimiter.SelectedIndex = 0 Then
            Delim = ","
        ElseIf cmbDelimiter.SelectedIndex = 1 Then
            Delim = "|"
        Else
            Delim = Chr(9)
        End If
    End Sub
End Class