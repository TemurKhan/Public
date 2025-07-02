Imports System.IO

Public Class frmImportPatients
    Private Delim As String
    Private PARAMS(24) As String

    Private Sub frmImportPatients_Load(sender As Object, e As EventArgs) Handles Me.Load
        cmbDelimiter.SelectedIndex = 0
        If cmbDelimiter.SelectedIndex = 0 Then
            Delim = ","
        End If
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

    Private Sub cmbDelimiter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDelimiter.SelectedIndexChanged
        If cmbDelimiter.SelectedIndex = 0 Then
            Delim = ","
        ElseIf cmbDelimiter.SelectedIndex = 1 Then
            Delim = "|"
        Else
            Delim = Chr(9)
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
        Dim Patgoods As Integer = 0
        For i As Integer = 0 To dgvFieldMap.RowCount - 1
            If CType(dgvFieldMap.Rows(i).Cells(0).Value, Boolean) = True Then
                If dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Last") Or _
                dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("First") Or _
                dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("DOB") Or _
                dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Gend") Then
                    Patgoods += 1
                End If
            End If
        Next
        If Patgoods >= 4 Then
            btnImport.Enabled = True
        Else
            btnImport.Enabled = False
        End If
    End Sub

    Private Function GetPatientID(ByVal LName As String, ByVal FName As String, ByVal DOB As Date, ByVal Sex As String) As String
        Dim PatID As String = ""
        Dim cnpid As New Odbc.OdbcConnection(connString)
        cnpid.Open()
        Dim cmdpid As New Odbc.OdbcCommand("Select ID from Patients where LastName = '" & _
        Replace(LName, "'", "''") & "' and FirstName = '" & Replace(FName, "'", "''") & _
        "' and Sex = '" & Sex.Substring(0, 1) & "' and DOB = '" & DOB & "'", cnpid)
        cmdpid.CommandType = CommandType.Text
        Dim drpid As Odbc.OdbcDataReader = cmdpid.ExecuteReader
        If drpid.HasRows Then
            While drpid.Read
                PatID = drpid("ID").ToString
            End While
        End If
        cnpid.Close()
        cnpid = Nothing
        Return PatID
    End Function

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        If txtFile.Text <> "" Then
            btnImport.Enabled = False
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
                    ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("SSN") Then
                        PARAMS(12) = i.ToString
                    ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Phone") Then
                        PARAMS(13) = i.ToString
                    ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Bill") Then
                        PARAMS(14) = i.ToString
                    ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Prime Payer") Then
                        PARAMS(15) = i.ToString
                    ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Prime Policy") Then
                        PARAMS(16) = i.ToString
                    ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Prime Group") Then
                        PARAMS(17) = i.ToString
                    ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Prime Rel") Then
                        PARAMS(18) = i.ToString
                    ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Second Payer") Then
                        PARAMS(19) = i.ToString
                    ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Second Policy") Then
                        PARAMS(20) = i.ToString
                    ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Second Group") Then
                        PARAMS(21) = i.ToString
                    ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Second Rel") Then
                        PARAMS(22) = i.ToString
                    ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Client") Then
                        PARAMS(23) = i.ToString
                    ElseIf dgvFieldMap.Rows(i).Cells(2).Value.ToString.Contains("Chart") Then
                        PARAMS(24) = i.ToString
                    End If
                End If
            Next
            '
            Dim sSQL As String = ""
            Dim AddressID As Long = Nothing
            Dim PatientID As String = ""
            Dim Delim As String = Chr(9)
            Dim n As Long = 1
            Dim Per As Integer = 0
            Dim lineCount As Long = File.ReadAllLines(Trim(txtFile.Text)).Length
            Dim SR As New StreamReader(Trim(txtFile.Text))
            Dim Ln As String ' Going to hold one line at a time
            Do Until SR.EndOfStream Or n = lineCount - 1
                Ln = SR.ReadLine
                If Trim(Ln) <> "" Then
                    'BW.ReportProgress((n + 1) / 100 * lineCount) ', ((n + 1) / 100 * lineCount).ToString & " %"
                    If Ln.Contains(Chr(9)) Then
                        Delim = Chr(9)
                    ElseIf Ln.Contains(",") Then
                        Delim = ","
                    ElseIf Ln.Contains("|") Then
                        Delim = "|"
                    End If
                    '
                    If Delim = "," Then
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
                    End If
                    AddressID = Nothing
                    '1=LAST NAME, 2=FIRST NAME, 3=MIDDLE NAME, 4=DOB, 5=GENDER, 6=ADDRESS1, 7=ADDRESS2, 8=CITY, 9=STATE, 10=ZIP,
                    '11=COUNTRY, 12=BillType, 13=PRIM  INS ID, 14=PRIM GROUP, 15=PRIM POLICY, 16=PRIM RELATION, 17=SECOND INS ID, 
                    '18=SECOND GROUP, 19=SECOND POLICY, 20=SECOND RELATION, 21=REF PROVIDER ID, 22=CHART
                    Dim Fields() As String = Split(Ln, Delim)  'tab or comma or pipe
                    If Delim = "," Then
                        For c As Integer = 0 To Fields.Length - 1
                            If Fields(c) Is Nothing Then
                                Fields(c) = ""
                            Else
                                Fields(c) = Replace(Fields(c), "|", ",")
                                Fields(c) = Trim(Fields(c))
                            End If
                        Next
                    End If
                    '
                    If IsDate(Fields(CInt(PARAMS(4)))) Then 'DOB
                        If Fields(CInt(PARAMS(1))) <> "" And Fields(CInt(PARAMS(2))) <> "" And _
                        Fields(CInt(PARAMS(4))) <> "" And Fields(CInt(PARAMS(5))) <> "" Then
                            PatientID = GetPatientID(Fields(CInt(PARAMS(1))), Fields(CInt(PARAMS(2))), _
                            CDate(Fields(CInt(PARAMS(4)))), Fields(CInt(PARAMS(5))))
                            If PatientID = "" Then PatientID = NextPatientID.ToString
                            '
                            If Fields(CInt(PARAMS(6))) <> "" And Fields(CInt(PARAMS(8))) <> "" And _
                            Fields(CInt(PARAMS(9))) <> "" And Fields(CInt(PARAMS(10))) <> "" Then
                                AddressID = GetAddressID(Fields(CInt(PARAMS(6))), Fields(CInt(PARAMS(7))), _
                                Fields(CInt(PARAMS(8))), Fields(CInt(PARAMS(9))), Fields(CInt(PARAMS(10))), Fields(CInt(PARAMS(11))))
                            Else
                                AddressID = Nothing
                            End If
                            '
                            Dim SSN As String = ""
                            Dim Phone As String = ""
                            If PARAMS(12) <> "" Then SSN = CleanIt(Fields(CInt(PARAMS(12))))
                            If PARAMS(13) <> "" Then Phone = CleanIt(Fields(CInt(PARAMS(13))))
                            sSQL = "If Not Exists (Select * from Patients where ID = " & PatientID & ") Insert into Patients " & _
                            "(ID, LastName, FirstName, MiddleName, DOB, Sex, Address_ID, SSN, HomePhone) values (" & PatientID & _
                            ", '" & Fields(CInt(PARAMS(1))) & "', '" & Fields(CInt(PARAMS(2))) & "', '" & Fields(CInt(PARAMS(3))) _
                            & "', '" & CDate(Fields(CInt(PARAMS(4)))) & "', '" & Fields(CInt(PARAMS(5))).Substring(0, 1) & "', " & _
                            AddressID & ", '" & SSN & "', '" & Phone & "')"
                            ExecuteSqlProcedure(sSQL)
                            '
                            If Trim(Fields(CInt(PARAMS(23)))) <> "" Then    'Association
                                Dim Chart As String = ""
                                If PARAMS(24) <> "" Then Chart = Trim(Fields(CInt(PARAMS(24))))
                                sSQL = "If Not Exists (Select * from Client_Patient where Provider_ID = " & _
                                Trim(Fields(CInt(PARAMS(23)))) & " and Patient_ID = " & PatientID & ") Insert into " & _
                                "Client_Patient (Provider_ID, Patient_ID, EMRNo, Room) values (" & Trim(Fields(CInt(PARAMS(23)))) _
                                & ", " & PatientID & ", '" & Chart & "', '')"
                                ExecuteSqlProcedure(sSQL)
                            End If
                            '
                            If PARAMS(15) <> "" AndAlso PARAMS(16) <> "" AndAlso PARAMS(18) <> "" AndAlso Trim(Fields(CInt(PARAMS(15)))) <> "" _
                            AndAlso Trim(Fields(CInt(PARAMS(16)))) <> "" AndAlso Trim(Fields(CInt(PARAMS(18)))) <> "" Then    'Prime coverage
                                If Trim(Fields(CInt(PARAMS(18)))) = "0" Or Trim(Fields(CInt(PARAMS(18)))) = "Self" Then
                                    Dim Rel As Int16 = 0
                                    Dim PGroup As String = ""
                                    If PARAMS(17) <> "" AndAlso Trim(Fields(CInt(PARAMS(17)))) <> "" _
                                    Then PGroup = Trim(Fields(CInt(PARAMS(17))))
                                    sSQL = "If Not Exists (Select * from Coverages where Insurance_ID = " & _
                                    Trim(Fields(CInt(PARAMS(15)))) & " and Patient_ID = " & PatientID & ") Insert into " & _
                                    "Coverages (Insurance_ID, Patient_ID, Ordinal, Insured_ID, Preference, GroupNo, " & _
                                    "PolicyNo, Relation) values (" & Trim(Fields(CInt(PARAMS(15)))) & ", " & PatientID & _
                                    ", 0, " & PatientID & ", 'P', '" & PGroup & "', '" & Trim(Fields(CInt(PARAMS(16)))) & _
                                    "', " & Rel & ")"
                                    ExecuteSqlProcedure(sSQL)
                                End If
                            End If
                            '
                            If PARAMS(19) <> "" AndAlso PARAMS(20) <> "" AndAlso PARAMS(22) <> "" AndAlso Trim(Fields(CInt(PARAMS(19)))) <> "" _
                            AndAlso Trim(Fields(CInt(PARAMS(20)))) <> "" AndAlso Trim(Fields(CInt(PARAMS(22)))) <> "" Then    'Second coverage
                                If Trim(Fields(CInt(PARAMS(22)))) = "0" Or Trim(Fields(CInt(PARAMS(22)))) = "Self" Then
                                    Dim Rel As Int16 = 0
                                    Dim SGroup As String = ""
                                    If PARAMS(21) <> "" AndAlso Trim(Fields(CInt(PARAMS(21)))) <> "" _
                                    Then SGroup = Trim(Fields(CInt(PARAMS(21))))
                                    sSQL = "If Not Exists (Select * from Coverages where Insurance_ID = " & _
                                    Trim(Fields(CInt(PARAMS(19)))) & " and Patient_ID = " & PatientID & ") Insert into " & _
                                    "Coverages (Insurance_ID, Patient_ID, Ordinal, Insured_ID, Preference, GroupNo, " & _
                                    "PolicyNo, Relation) values (" & Trim(Fields(CInt(PARAMS(19)))) & ", " & PatientID & _
                                    ", 0, " & PatientID & ", 'P', '" & SGroup & ", '" & Trim(Fields(CInt(PARAMS(20)))) & _
                                    "', " & Rel & ")"
                                    ExecuteSqlProcedure(sSQL)
                                End If
                            End If
                            '
                        End If
                    End If
                End If
                PB.Value = CInt(n / 100 * lineCount)
                lblStatus.Text = CInt(n / 100 * lineCount).ToString & " %"
                My.Application.DoEvents()
                n += 1
            Loop
            txtFile.Text = ""
            dgvFieldMap.Rows.Clear()
            'btnImport.Enabled = True
        End If
        'Return n
    End Sub

    Private Sub dgvFieldMap_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFieldMap.CellEndEdit
        RoutineProgress()
    End Sub
End Class