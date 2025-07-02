Option Compare Text
Imports System.data

Public Class frmMissingInfo

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    'Private Sub txtDateFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If UserEnteredText(txtDateFrom) <> "" Then
    '        If Not IsDate(txtDateFrom.Text) Then
    '            MsgBox("Invalid date")
    '            txtDateFrom.Text = ""
    '        Else
    '            txtAccFrom.Text = ""
    '            txtAccTo.Text = ""
    '        End If
    '    End If
    '    UpdateProviders()
    'End Sub

    Private Sub txtAccFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccFrom.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAccFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccFrom.Validated
        If txtAccFrom.Text <> "" Then
            'txtDateFrom.Text = ""
            'txtDateTo.Text = ""
            ClearDateTimePicker(dtpDateFrom)
            ClearDateTimePicker(dtpDateTo)

        End If
        UpdateProviders()
    End Sub

    Private Sub txtAccTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccTo.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAccTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccTo.Validated
        If txtAccTo.Text <> "" Then
            ClearDateTimePicker(dtpDateFrom)
            ClearDateTimePicker(dtpDateTo)

        End If
        UpdateProviders()
    End Sub

    Private Sub UpdateProviders()
        dgvMIS.Rows.Clear()
        dgvInfo.Rows.Clear()
        Dim Accessions As String = ""
        btnLoad.Enabled = False
        '
        Dim sSQL As String = "Select ID from Requisitions where ID in (Select distinct " &
        "AccID from vReqTBillable) and not ID in (Select distinct Accession_ID from Charges)"
        '
        If IsDate(dtpDateFrom.Text) Or IsDate(dtpDateTo.Text) Or txtAccFrom.Text <> "" Or txtAccTo.Text <> "" Then
            If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                sSQL += " and AccessionDate between '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) _
                & " 00:00:00' and '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) & " 23:59:00'"
            ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                sSQL += " and AccessionDate between '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) _
                & " 00:00:00' and '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) & " 23:59:00'"
            ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                sSQL += " and AccessionDate between '" & Format(CDate(dtpDateFrom.Text), SystemConfig.DateFormat) _
                & " 00:00:00' and '" & Format(CDate(dtpDateTo.Text), SystemConfig.DateFormat) & " 23:59:00'"
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                sSQL += " and ID = " & Val(txtAccFrom.Text)
            ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                sSQL += " and ID = " & Val(txtAccTo.Text)
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                If Val(txtAccFrom.Text) < Val(txtAccTo.Text) Then
                    sSQL += " and ID >= " & Val(txtAccFrom.Text) & " and ID <= " &
                    Val(txtAccTo.Text)
                Else
                    sSQL += " and ID >= " & Val(txtAccTo.Text) & " and a.ID <= " &
                    Val(txtAccFrom.Text)
                End If
            End If
            Dim cnmi As New SqlClient.SqlConnection(connString)
            cnmi.Open()
            Dim cmdmi As New SqlClient.SqlCommand(sSQL, cnmi)
            cmdmi.CommandType = CommandType.Text
            Dim drmi As SqlClient.SqlDataReader = cmdmi.ExecuteReader
            If drmi.HasRows Then
                While drmi.Read
                    If AccMissingInfo(drmi("ID")) Then _
                    Accessions += drmi("ID").ToString & ", "
                End While
            End If
            cnmi.Close()
            cnmi = Nothing
            If Accessions.EndsWith(", ") Then Accessions = Accessions.Substring(0, Len(Accessions) - 2)
            '
            If Accessions <> "" Then
                sSQL = "Select * from Providers where Not LastName_BSN like 'zz%' and  ID in " &
                "(Select distinct OrderingProvider_ID from Requisitions where ID in (" & Accessions & "))"
                Dim Provider As String = ""
                Dim Fax As String = ""
                Dim Email As String = ""
                Dim Formula As String = ""
                Dim cnml As New SqlClient.SqlConnection(connString)
                cnml.Open()
                Dim cmdml As New SqlClient.SqlCommand(sSQL, cnml)
                cmdml.CommandType = CommandType.Text
                Dim drml As SqlClient.SqlDataReader = cmdml.ExecuteReader
                If drml.HasRows Then
                    While drml.Read
                        If drml("IsIndividual") IsNot DBNull.Value AndAlso drml("IsIndividual") = 0 Then
                            Provider = drml("LastName_BSN")
                        Else
                            Provider = drml("LastName_BSN") & ", " & drml("FirstName") &
                            IIf(drml("Degree") Is DBNull.Value, "", " " & drml("Degree"))
                        End If
                        If drml("Fax") Is DBNull.Value Then
                            Fax = ""
                        Else
                            Fax = drml("Fax")
                        End If
                        If drml("Email") Is DBNull.Value Then
                            Email = ""
                        Else
                            Email = drml("Email")
                        End If
                        Formula = GetReportFormula(drml("ID"))
                        dgvMIS.Rows.Add(drml("ID"), Provider, True,
                        IIf(Fax <> "", True, False), Fax, IIf(Email <> "", True, False), Email, "MIS",
                        Formula)
                    End While
                End If
                cnml.Close()
                cnml = Nothing
            End If
        End If
        btnLoad.Enabled = True
    End Sub

    Private Function GetReportFormula(ByVal ProviderID As Long) As String
        Dim Formula As String = ""
        If IsDate(dtpDateFrom.Text) Or IsDate(dtpDateTo.Text) Or txtAccFrom.Text <> "" Or txtAccTo.Text <> "" Then
            Formula = "{Requisitions.Received} = True and " &
            " {Requisitions.OrderingProvider_ID} = " & ProviderID
            If chkTPAll.Checked = False Then    'Third Party
                Formula += " and {Requisitions.BillingType_ID} = 1"
            End If
            '
            If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                Formula += " and {Requisitions.AccessionDate} in DateTime(" & CDate(dtpDateFrom.Text).Year _
                & "," & CDate(dtpDateFrom.Text).Month & "," & CDate(dtpDateFrom.Text).Day & ",00,00,01) " &
                "To DateTime(" & CDate(dtpDateFrom.Text).Year & "," & CDate(dtpDateFrom.Text).Month & "," &
                CDate(dtpDateFrom.Text).Day & ",23,59,59);"
            ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                Formula += " and {Requisitions.AccessionDate} in DateTime(" & CDate(dtpDateTo.Text).Year _
                & "," & CDate(dtpDateTo.Text).Month & "," & CDate(dtpDateTo.Text).Day & ",00,00,01) " &
                "To DateTime(" & CDate(dtpDateTo.Text).Year & "," & CDate(dtpDateTo.Text).Month & "," &
                CDate(dtpDateTo.Text).Day & ",23,59,59);"
            ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                Formula += " and {Requisitions.AccessionDate} in DateTime(" & CDate(dtpDateFrom.Text).Year _
                & "," & CDate(dtpDateFrom.Text).Month & "," & CDate(dtpDateFrom.Text).Day & ",00,00,01) " &
                "To DateTime(" & CDate(dtpDateTo.Text).Year & "," & CDate(dtpDateTo.Text).Month & "," &
                CDate(dtpDateTo.Text).Day & ",23,59,59);"
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                Formula += " and {Requisitions.ID} = " & Val(txtAccFrom.Text)
            ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                Formula += " and {Requisitions.ID} = " & Val(txtAccTo.Text)
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                Formula += " and {Requisitions.ID} in " & Val(txtAccFrom.Text) & " To " & Val(txtAccTo.Text)
            End If
        End If
        Return Formula
    End Function

    Private Sub chkTPAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTPAll.CheckedChanged
        If chkTPAll.Checked = False Then
            chkTPAll.Text = "3rd Parties"
        Else
            chkTPAll.Text = "All Claims"
        End If
        UpdateProviders()
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Dim i As Integer
        Dim ProviderIDs As String = ""
        If dgvMIS.RowCount > 0 Then
            For i = 0 To dgvMIS.RowCount - 1
                If dgvMIS.Rows(i).Cells(2).Value = True Or
                dgvMIS.Rows(i).Cells(3).Value = True Or dgvMIS.Rows(i).Cells(5).Value = True Then
                    ProviderIDs += dgvMIS.Rows(i).Cells(0).Value.ToString & ", "
                End If
            Next
            If ProviderIDs = "" Then
                Return
            End If
            ProviderIDs = Microsoft.VisualBasic.Mid(ProviderIDs, 1, Len(ProviderIDs) - 2)
            DisplayICD9Updater(ProviderIDs)
        End If
    End Sub

    Private Sub DisplayICD9Updater(ByVal ProviderIDs As String)
        dgvInfo.Rows.Clear()
        Dim sSQL As String = "Select a.*, b.ID as AccID, b.AccessionDate as " &
        "AccDate, b.EMRNo as Chart from Patients a inner join Requisitions b " &
        "on a.ID = b.Patient_ID where b.ID in (Select AccID from vReqTBillable)" &
        "and Not b.ID in (Select Accession_ID from Charges) and b.OrderingProvider_ID " &
        "in (" & ProviderIDs & ")"
        If chkTPAll.Checked = False Then sSQL += " and b.BillingType_ID = 1"
        If IsDate(dtpDateFrom.Text) Or IsDate(dtpDateTo.Text) Or txtAccFrom.Text <> "" Or txtAccTo.Text <> "" Then
            If IsDate(dtpDateFrom.Text) And Not IsDate(dtpDateTo.Text) Then
                sSQL += " and b.AccessionDate between '" & dtpDateFrom.Text &
                "' and '" & dtpDateFrom.Text & " 23:59:00'"
            ElseIf Not IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                sSQL += " and b.AccessionDate between '" & dtpDateTo.Text &
                "' and '" & dtpDateTo.Text & " 23:59:00'"
            ElseIf IsDate(dtpDateFrom.Text) And IsDate(dtpDateTo.Text) Then
                sSQL += " and b.AccessionDate between '" & dtpDateFrom.Text &
                 "' and '" & dtpDateTo.Text & " 23:59:00'"
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                sSQL += " and b.ID = " & Val(txtAccFrom.Text)
            ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                sSQL += " and b.ID = " & Val(txtAccTo.Text)
            ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                sSQL += " and b.ID between " & Val(txtAccFrom.Text) & " and " & Val(txtAccTo.Text)
            End If
            '
            Dim cnmin As New SqlClient.SqlConnection(connString)
            cnmin.Open()
            Dim cmdmin As New SqlClient.SqlCommand(sSQL, cnmin)
            cmdmin.CommandType = CommandType.Text
            Dim drmin As SqlClient.SqlDataReader = cmdmin.ExecuteReader
            If drmin.HasRows Then
                While drmin.Read
                    Dim ff = drmin("AccDate")
                    If AccMissingInfo(drmin("AccID")) Then
                        dgvInfo.Rows.Add(drmin("LastName") & ", " & drmin("FirstName"),
                        Format(drmin("DOB"), SystemConfig.DateFormat), drmin("Sex"),
                        drmin("Chart"), drmin("AccID"), Format(drmin("AccDate"),
                        SystemConfig.DateFormat), "")
                    End If
                End While
                If dgvInfo.RowCount > 0 Then btnPrint.Enabled = True
            End If
            cnmin.Close()
            cnmin = Nothing
        End If
    End Sub

    Private Function AccMissingInfo(ByVal AccID As Long) As Boolean
        Dim AccDirty As Boolean = False
        Dim Codes() As String = {""}
        Dim cnam As New SqlClient.SqlConnection(connString)
        cnam.Open()
        Dim cmdam As New SqlClient.SqlCommand("Select * " &
        "from Req_Dx where Accession_ID = " & AccID, cnam)
        cmdam.CommandType = CommandType.Text
        Dim dram As SqlClient.SqlDataReader = cmdam.ExecuteReader
        If dram.HasRows Then
            While dram.Read
                If dram("Dx_Code") IsNot DBNull.Value _
                AndAlso Trim(dram("Dx_Code")) <> "" Then
                    If Codes(UBound(Codes)) <> "" Then _
                    ReDim Preserve Codes(UBound(Codes) + 1)
                    Codes(UBound(Codes)) = Trim(dram("Dx_Code"))
                End If
            End While
        Else
            AccDirty = True
        End If
        cnam.Close()
        cnam = Nothing
        If AccDirty = False And Codes(0) <> "" Then
            Dim Billables() As String = {""}
            Dim cndx As New SqlClient.SqlConnection(connString)
            cndx.Open()
            Dim cmddx As New SqlClient.SqlCommand("Select TBBID " &
            "from vReqTBillable where AccID = " & AccID, cndx)
            cmddx.CommandType = CommandType.Text
            Dim drdx As SqlClient.SqlDataReader = cmddx.ExecuteReader
            If drdx.HasRows Then
                While drdx.Read
                    If drdx("TBBID") IsNot DBNull.Value Then
                        If Billables(UBound(Billables)) <> "" Then _
                        ReDim Preserve Billables(UBound(Billables) + 1)
                        Billables(UBound(Billables)) = drdx("TBBID").ToString
                    End If
                End While
            End If
            cndx.Close()
            cndx = Nothing
            If Billables(0) <> "" Then
                For i As Integer = 0 To Billables.Length - 1
                    If TGPDirty(Billables(i), Codes) Then
                        AccDirty = True
                        Exit For
                    End If
                Next
            End If
        End If
        Return AccDirty
    End Function

    Private Function TGPDirty(ByVal TGPID As String, ByVal Codes() As String) As Boolean
        Dim IsDirty As Boolean = True
        Dim cndm As New SqlClient.SqlConnection(connString)
        cndm.Open()
        Dim cmddm As New SqlClient.SqlCommand("Select * " &
        "from Necessity where TGP_ID = " & Val(TGPID), cndm)
        cmddm.CommandType = CommandType.Text
        Dim drdm As SqlClient.SqlDataReader = cmddm.ExecuteReader
        If drdm.HasRows Then
            While drdm.Read
                For i As Integer = 0 To Codes.Length - 1
                    If drdm("ICD9") IsNot Nothing Then
                        If Not drdm("ICD9") Is System.DBNull.Value Then
                            If Codes(i) = Trim(drdm("ICD9")) Then
                                IsDirty = False
                                Exit For
                            End If
                        End If

                    End If

                Next
                If IsDirty = False Then Exit While
            End While
        Else
            If Codes(0) <> "" Then IsDirty = False
        End If
        cndm.Close()
        cndm = Nothing
        Return IsDirty
    End Function

    Private Sub UpdateAccession(ByVal AccID As Long, ByVal CodeStr As String)
        Dim Codes() As String = Split(CodeStr, ",")
        For i As Integer = 0 To Codes.Length - 1
            If Trim(Codes(i)) <> "" Then
                ExecuteSqlProcedure("If Exists (Select * from Req_Dx where Accession_ID = " &
                AccID & " and Dx_Code = '" & Trim(Codes(i)) & "') Update Req_Dx Set " &
                "Ordinal = " & GetDxOrdinal(AccID) & ", IsPrimary = " & IIf(HasPrimaryDx(AccID),
                0, 1) & " where Accession_ID = " & AccID & " and Dx_Code = '" & Trim(Codes(i)) &
                "' Else Insert into Req_Dx (Accession_ID, Dx_Code, Ordinal, IsPrimary) " &
                "values (" & AccID & ", '" & Trim(Codes(i)) & "', " & GetDxOrdinal(AccID) + 1 &
                ", " & IIf(HasPrimaryDx(AccID), 0, 1) & ")")
            End If
        Next
    End Sub

    Private Function GetDxOrdinal(ByVal AccID As Long) As Integer
        Dim MaxO As Integer = 0
        Dim cno As New SqlClient.SqlConnection(connString)
        cno.Open()
        Dim cmdo As New SqlClient.SqlCommand("Select max(Ordinal) as " &
        "MaxOrdinal from Req_Dx where Accession_ID = " & AccID, cno)
        cmdo.CommandType = CommandType.Text
        Dim dro As SqlClient.SqlDataReader = cmdo.ExecuteReader
        If dro.HasRows Then
            While dro.Read
                MaxO = dro("MaxOrdinal")
            End While
        End If
        cno.Close()
        cno = Nothing
        Return MaxO
    End Function

    Private Function HasPrimaryDx(ByVal AccID As Long) As Boolean
        Dim HasP As Boolean = False
        Dim cnip As New SqlClient.SqlConnection(connString)
        cnip.Open()
        Dim cmdip As New SqlClient.SqlCommand("Select " &
        "IsPrimary from Req_Dx where Accession_ID = " & AccID, cnip)
        cmdip.CommandType = CommandType.Text
        Dim drip As SqlClient.SqlDataReader = cmdip.ExecuteReader
        If drip.HasRows Then
            While drip.Read
                If drip("IsPrimary") <> 0 Then
                    HasP = True
                    Exit While
                End If
            End While
        End If
        cnip.Close()
        cnip = Nothing
        Return HasP
    End Function

    Private Sub txtDateTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
        'If UserEnteredText(txtDateTo) <> "" Then
        '    If Not IsDate(txtDateTo.Text) Then
        '        MsgBox("Invalid date")
        '        txtDateTo.Text = ""
        '    Else
        '        txtAccFrom.Text = ""
        '        txtAccTo.Text = ""
        '    End If
        'End If
        'UpdateProviders()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        '{Requisitions.Received} = True and 
        '{Requisitions.OrderingProvider_ID} = 400 and
        '{Requisitions.BillingType_ID} = 1 and
        '(IsNull({Req_Dx.Dx_Code}) or {Req_Dx.Dx_Code} = "") and
        '{Requisitions.AccessionDate} in DateTime (2010, 01, 01, 0, 0, 0) to DateTime (2010, 12, 31, 23, 59, 59)
        'If IsDate(txtDateFrom.Text) Or IsDate(txtDateTo.Text) Or txtAccFrom.Text <> "" Or txtAccTo.Text <> "" Then
        If dgvMIS.Rows.Count > 0 And dgvInfo.Rows.Count > 0 Then
            Dim Formula As String = ""  '"{Requisitions.Received} = True" 
            Dim i As Integer
            Dim ProviderIDs As String = ""
            Dim Accessions As String = ""
            If dgvMIS.RowCount > 0 Then
                For i = 0 To dgvMIS.RowCount - 1
                    If dgvMIS.Rows(i).Cells(2).Value = True Then _
                    ProviderIDs += dgvMIS.Rows(i).Cells(0).Value.ToString & ", "
                Next
                If Len(ProviderIDs) > 2 Then _
                ProviderIDs = Microsoft.VisualBasic.Mid(ProviderIDs, 1, Len(ProviderIDs) - 2)
                If ProviderIDs <> "" Then
                    If InStr(ProviderIDs, ",") > 0 Then
                        Formula += "{Requisitions.OrderingProvider_ID} in [" & ProviderIDs & "]"
                    Else
                        Formula += "{Requisitions.OrderingProvider_ID} = " & ProviderIDs
                    End If
                End If
            End If
            If chkTPAll.Checked = False Then    'Third Party
                Formula += " and {Requisitions.BillingType_ID} = 1"
            End If
            '
            For i = 0 To dgvInfo.RowCount - 1
                Accessions += dgvInfo.Rows(i).Cells(4).Value.ToString & ", "
            Next
            If Len(Accessions) > 2 Then Accessions =
            Microsoft.VisualBasic.Mid(Accessions, 1, Len(Accessions) - 2)
            If Accessions <> "" Then
                If InStr(Accessions, ",") > 0 Then
                    Formula += " and {Requisitions.ID} in [" & Accessions & "]"
                Else
                    Formula += " and {Requisitions.ID} = " & Accessions
                End If
            End If
            If Formula <> "" Then
                Formula += " and {Requisitions.Received} = True"
                '
                'If IsDate(txtDateFrom.Text) And Not IsDate(txtDateTo.Text) Then
                '    Formula += " and {Requisitions.AccessionDate} in DateTime(" & CDate(txtDateFrom.Text).Year _
                '    & "," & CDate(txtDateFrom.Text).Month & "," & CDate(txtDateFrom.Text).Day & ",00,00,01) " & _
                '    "To DateTime(" & CDate(txtDateFrom.Text).Year & "," & CDate(txtDateFrom.Text).Month & "," & _
                '    CDate(txtDateFrom.Text).Day & ",23,59,59);"
                'ElseIf Not IsDate(txtDateFrom.Text) And IsDate(txtDateTo.Text) Then
                '    Formula += " and {Requisitions.AccessionDate} in DateTime(" & CDate(txtDateTo.Text).Year _
                '    & "," & CDate(txtDateTo.Text).Month & "," & CDate(txtDateTo.Text).Day & ",00,00,01) " & _
                '    "To DateTime(" & CDate(txtDateTo.Text).Year & "," & CDate(txtDateTo.Text).Month & "," & _
                '    CDate(txtDateTo.Text).Day & ",23,59,59);"
                'ElseIf IsDate(txtDateFrom.Text) And IsDate(txtDateTo.Text) Then
                '    Formula += " and {Requisitions.AccessionDate} in DateTime(" & CDate(txtDateFrom.Text).Year _
                '    & "," & CDate(txtDateFrom.Text).Month & "," & CDate(txtDateFrom.Text).Day & ",00,00,01) " & _
                '    "To DateTime(" & CDate(txtDateTo.Text).Year & "," & CDate(txtDateTo.Text).Month & "," & _
                '    CDate(txtDateTo.Text).Day & ",23,59,59);"
                'ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                '    Formula += " and {Requisitions.ID} = " & Val(txtAccFrom.Text)
                'ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                '    Formula += " and {Requisitions.ID} = " & Val(txtAccTo.Text)
                'ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                '    Formula += " and {Requisitions.ID} in [" & Val(txtAccFrom.Text) & " To " & Val(txtAccTo.Text) & "]"
                'End If
                '
                Dim UID As String = My.Settings.UID.ToString
                Dim PWD As String = My.Settings.PWD
                '===========================
                'TODO: Crystal Report Code
                'Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                'oRpt.Load(Application.StartupPath & "\Reports\MissingInfo.rpt")
                'ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database, UID, PWD)
                ''oRpt.SetDatabaseLogon(UID, PWD)
                'oRpt.RecordSelectionFormula = Formula
                'frmRV.CRRV.ReportSource = oRpt
                'frmRV.Show()
                'frmRV.MdiParent = ProlisQC
                '===============================
            End If
        Else
            MsgBox("The Missing Info letter can only be printed if the Provider grid and " &
            "the Patient grid, both have entries with proper selections.")
        End If
    End Sub

    Private Function PhoneNeat(ByVal Phone As String) As String
        Phone = Replace(Phone, "(", "")
        Phone = Replace(Phone, ")", "")
        Phone = Replace(Phone, "-", "")
        Phone = Replace(Phone, "_", "")
        Phone = Replace(Phone, ".", "")
        Phone = Replace(Phone, "/", "")
        Phone = Replace(Phone, "\", "")
        Phone = Replace(Phone, "*", "")
        Return Phone
    End Function

    Private Sub dgvInfo_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvInfo.CellEndEdit
        If e.RowIndex <> -1 Then
            If e.ColumnIndex = 6 Then
                If Not dgvInfo.Rows(e.RowIndex).Cells(6).Value Is System.DBNull.Value _
                AndAlso Trim(dgvInfo.Rows(e.RowIndex).Cells(6).Value) <> "" Then
                    UpdateAccession(dgvInfo.Rows(e.RowIndex).Cells(4).Value, Trim(dgvInfo.Rows(e.RowIndex).Cells(6).Value))
                    'dgvInfo.Rows.RemoveAt(e.RowIndex)
                End If
            End If
        End If
    End Sub

    Private Sub btnPSel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPSel.Click
        If dgvMIS.RowCount > 0 Then
            Dim i As Integer
            For i = 0 To dgvMIS.RowCount - 1
                dgvMIS.Rows(i).Cells(2).Value = True
            Next
        End If
    End Sub

    Private Sub btnPDSel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDSel.Click
        If dgvMIS.RowCount > 0 Then
            Dim i As Integer
            For i = 0 To dgvMIS.RowCount - 1
                dgvMIS.Rows(i).Cells(2).Value = False
            Next
        End If
    End Sub

    Private Sub btnFax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFax.Click
        If dgvMIS.RowCount > 0 Then
            Dim i As Integer
            For i = 0 To dgvMIS.RowCount - 1
                If dgvMIS.Rows(i).Cells(4).Value <> "" Then _
                dgvMIS.Rows(i).Cells(3).Value = True
            Next
        End If
    End Sub

    Private Sub btnDeFax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeFax.Click
        If dgvMIS.RowCount > 0 Then
            Dim i As Integer
            For i = 0 To dgvMIS.RowCount - 1
                dgvMIS.Rows(i).Cells(3).Value = False
            Next
        End If
    End Sub

    Private Sub btnEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmail.Click
        If dgvMIS.RowCount > 0 Then
            Dim i As Integer
            For i = 0 To dgvMIS.RowCount - 1
                If dgvMIS.Rows(i).Cells(6).Value <> "" Then _
                dgvMIS.Rows(i).Cells(5).Value = True
            Next
        End If
    End Sub

    Private Sub btnDemail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDemail.Click
        If dgvMIS.RowCount > 0 Then
            Dim i As Integer
            For i = 0 To dgvMIS.RowCount - 1
                dgvMIS.Rows(i).Cells(5).Value = False
            Next
        End If
    End Sub

    Private Sub frmMissingInfo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ConfigureDateTimePicker(dtpDateFrom)
        ConfigureDateTimePicker(dtpDateTo)
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub


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