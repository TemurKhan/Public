Option Compare Text
Imports System.Windows.Forms
Imports System.Data
Imports System.IO

Public Class frmPrintLabels

    'Todo: Dymo Code
    '=============================
    'Public DymoAddIn As Dymo.DymoAddIn
    'Public DymoLabel As Dymo.DymoLabels
    '===============================
    Private Sub frmPrintLabels_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CN.Open("DSN=ProlisQC")
        cmbDataType.SelectedIndex = 0
        dgvDiscrete.RowCount = 1
        txtQty.Text = "1"
        Try
            If SystemConfig.P_inputOnLabel Then
                BradyPnal.Show()
                txtP.Show()
                txtInitial.Show()
                LabelP.Show()
                dtpDate.Show()
                initlabel.Show()
                dateLabel.Show()
            Else
                BradyPnal.Hide()
                txtP.Hide()
                dtpDate.Hide()
                initlabel.Hide()
                dateLabel.Hide()
                LabelP.Hide()
                txtInitial.Hide()
            End If
        Catch ex As Exception

        End Try

        Dim values = CommonData.ExecuteQuery("select * from Users where UserName ='" & ThisUser.UserName.Trim() & "'")
        For Each v In values
            Dim SpecificPrinter = ""
            Try
                SpecificPrinter = v("SpecificPrinter")
            Catch ex As Exception
                SpecificPrinter = "Default"
            End Try
            Try
                Dim UseRemotePrinter = v("UseRemotePrinter")

                If UseRemotePrinter IsNot DBNull.Value Then

                    Dim result = False
                    If UseRemotePrinter Then
                        result = True
                        ChkRemotePrint.Visible = True
                    Else
                        result = False
                        ChkRemotePrint.Visible = False
                    End If
                    ThisUser.UseRemotePrinter = result
                Else
                    ThisUser.UseRemotePrinter = False

                    ChkRemotePrint.Visible = False
                End If
                '.............
                If SpecificPrinter IsNot DBNull.Value Then
                    ThisUser.SpecificPrinter = SpecificPrinter
                Else
                    ThisUser.SpecificPrinter = "Default"
                End If
                '..............
            Catch ex As Exception
                ThisUser.UseRemotePrinter = False

            End Try

        Next
        ChkRemotePrint.Checked = ThisUser.UseRemotePrinter
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        DiscreteClear()
        Update_Progress()
    End Sub

    Private Sub txtQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAccTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccTo.KeyPress
        If cmbDataType.SelectedIndex = 0 Then Numerals(sender, e)
    End Sub

    Private Sub txtAccFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccFrom.KeyPress
        If cmbDataType.SelectedIndex = 0 Then Numerals(sender, e)
    End Sub

    Private Sub txtAccFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccFrom.Validated
        If txtAccFrom.Text.Contains("-") Then
            Dim acc = txtAccFrom.Text.Split("-")(0)
            txtAccFrom.Text = acc
        End If
        If txtAccFrom.Text <> "" Then DiscreteClear()
        If cmbDataType.SelectedIndex <> 2 Then UpdateSources()
        Update_Progress()
    End Sub

    Private Sub UpdateSources()
        dgvSources.Rows.Clear()
        Dim i As Integer
        Dim Accs As String = ""
        Dim sSQL As String = ""
        If txtAccFrom.Text <> "" And txtAccTo.Text = "" And dgvDiscrete.RowCount = 1 Then
            If cmbDataType.SelectedIndex = 0 Then
                sSQL = "Select a.ID as ID, a.Name as Source, b.Name as Material " &
                "from Sources a inner join Materials b on a.Material_ID = b.ID " &
                "where a.ID in (Select distinct Source_ID from Specimens where " &
                "Accession_ID = " & Val(txtAccFrom.Text) & ")"
            Else
                sSQL = "Select a.ID as ID, a.Name as Source, b.Name as Material " &
                "from Sources a inner join Materials b on a.Material_ID = b.ID " &
                "where a.ID in (Select distinct Source_ID from Specimens where " &
                "Accession_ID in (Select ID from Requisitions where RequisitionNo " &
                "= '" & Trim(txtAccFrom.Text) & "'))"
            End If
        ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" And dgvDiscrete.RowCount = 1 Then
            If cmbDataType.SelectedIndex = 0 Then
                sSQL = "Select a.ID as ID, a.Name as Source, b.Name as Material " &
                "from Sources a inner join Materials b on a.Material_ID = b.ID " &
                "where a.ID in (Select distinct Source_ID from Specimens where " &
                "Accession_ID = " & Val(txtAccTo.Text) & ")"
            ElseIf cmbDataType.SelectedIndex = 1 Then
                sSQL = "Select a.ID as ID, a.Name as Source, b.Name as Material " &
                "from Sources a inner join Materials b on a.Material_ID = b.ID " &
                "where a.ID in (Select distinct Source_ID from Specimens where " &
                "Accession_ID in (Select ID from Requisitions where RequisitionNo " &
                "= '" & Trim(txtAccTo.Text) & "'))"
            End If
        ElseIf txtAccFrom.Text <> "" And txtAccTo.Text <> "" And dgvDiscrete.RowCount = 1 Then
            If cmbDataType.SelectedIndex = 0 Then
                sSQL = "Select a.ID as ID, a.Name as Source, b.Name as Material " &
                "from Sources a inner join Materials b on a.Material_ID = b.ID " &
                "where a.ID in (Select distinct Source_ID from Specimens where " &
                "Accession_ID between " & Val(txtAccFrom.Text) & " and " &
                Val(txtAccTo.Text) & ")"
            ElseIf cmbDataType.SelectedIndex = 1 Then
                sSQL = "Select a.ID as ID, a.Name as Source, b.Name as Material " &
                "from Sources a inner join Materials b on a.Material_ID = b.ID " &
                "where a.ID in (Select distinct Source_ID from Specimens where " &
                "Accession_ID in (Select ID from Requisitions where RequisitionNo " &
                "between '" & Trim(txtAccFrom.Text) & "' and '" & Trim(txtAccTo.Text) & "'))"
            End If
        ElseIf txtAccFrom.Text = "" And txtAccTo.Text = "" And dgvDiscrete.RowCount > 1 Then
            If cmbDataType.SelectedIndex = 0 Then
                sSQL = "Select a.ID as ID, a.Name as Source, b.Name as Material " &
                "from Sources a inner join Materials b on a.Material_ID = b.ID " &
                "where a.ID in (Select distinct Source_ID from Specimens where " &
                "Accession_ID in ("
            ElseIf cmbDataType.SelectedIndex = 1 Then
                sSQL = "Select a.ID as ID, a.Name as Source, b.Name as Material " &
                "from Sources a inner join Materials b on a.Material_ID = b.ID " &
                "where a.ID in (Select distinct Source_ID from Specimens where " &
                "Accession_ID in (Select ID from Requisitions where RequisitionNo in ("
            End If
            For i = 0 To dgvDiscrete.RowCount - 1
                If dgvDiscrete.Rows(i).Cells(0).Value IsNot Nothing _
                AndAlso dgvDiscrete.Rows(i).Cells(0).Value <> "" Then
                    If cmbDataType.SelectedIndex = 0 Then
                        Accs += dgvDiscrete.Rows(i).Cells(0).Value & ", "
                    Else
                        Accs += "'" & Trim(dgvDiscrete.Rows(i).Cells(0).Value) & "', "
                    End If
                End If
            Next
            If Accs.Length > 2 Then Accs = Microsoft.VisualBasic.Mid(Accs,
            1, Len(Accs) - 2)
            If cmbDataType.SelectedIndex = 0 Then
                sSQL += Accs & "))"
            Else
                sSQL += Accs & ")))"
            End If
        End If
        If sSQL <> "" Then
            Dim cnlbl As New SqlClient.SqlConnection(connString)
            cnlbl.Open()
            Dim cmdlbl As New SqlClient.SqlCommand(sSQL, cnlbl)
            cmdlbl.CommandType = CommandType.Text
            Dim drlbl As SqlClient.SqlDataReader = cmdlbl.ExecuteReader
            If drlbl.HasRows Then
                While drlbl.Read
                    dgvSources.Rows.Add(drlbl("ID"), False, drlbl("Source") _
                    & " [" & drlbl("Material") & "]", "1")
                End While
            End If
            cnlbl.Close()
            cnlbl = Nothing
        End If
    End Sub

    Private Sub FormClear()
        txtAccFrom.Text = ""
        txtAccTo.Text = ""
        DiscreteClear()
        txtQty.Text = "1"
        dgvSources.Rows.Clear()
        btnProcess.Enabled = False
        cmbDataType.SelectedIndex = 0
    End Sub
    Private Function GetLabelInfo(ByVal AccID As Long, ByVal QTY As Integer) As String()
        Dim labelInfo() As String = {""}
        Dim Sources() As String = {""}
        Dim Provider As String = ""
        Dim Patient As String = ""
        Dim Tests As String = ""
        Dim AccDate As String = ""
        Dim EMRNo As String = ""
        Dim ReqNo As String = ""
        Dim i As Integer
        ' Dim Rs As New ADODB.Recordset
        For i = 0 To dgvSources.RowCount - 1
            If CType(dgvSources.Rows(i).Cells(1).Value, Boolean) = True AndAlso
            Val(dgvSources.Rows(i).Cells(3).Value) > 0 Then
                Dim cnlin As New SqlClient.SqlConnection(connString)
                cnlin.Open()
                Dim cmdlin As New SqlClient.SqlCommand("Select a.ID as ID, " &
                "a.Name as Source from Sources a inner join Specimens b on " &
                "a.ID = b.Source_ID where a.ID = " & Val(dgvSources.Rows(i).Cells(0).Value) &
                " and b.Accession_ID = " & AccID, cnlin)
                cmdlin.CommandType = CommandType.Text
                Dim drlin As SqlClient.SqlDataReader = cmdlin.ExecuteReader
                If drlin.HasRows Then
                    While drlin.Read
                        If Sources(UBound(Sources)) <> "" Then _
                        ReDim Preserve Sources(UBound(Sources) + 1)
                        Sources(UBound(Sources)) = drlin("ID").ToString &
                        "^" & Trim(drlin("Source")) & "^" &
                        dgvSources.Rows(i).Cells(3).Value
                    End While
                End If
                cnlin.Close()
                cnlin = Nothing
            End If
        Next
        '
        Dim cnprv As New SqlClient.SqlConnection(connString)
        cnprv.Open()
        Dim cmdprv As New SqlClient.SqlCommand("Select * from Providers where ID in " &
        "(Select OrderingProvider_ID from Requisitions where ID = " & AccID & ")", cnprv)
        cmdprv.CommandType = CommandType.Text
        Dim drprv As SqlClient.SqlDataReader = cmdprv.ExecuteReader
        If drprv.HasRows Then
            While drprv.Read
                If drprv("IsIndividual") IsNot DBNull.Value AndAlso drprv("IsIndividual") = 0 Then
                    Provider = Trim(drprv("LastName_BSN"))
                Else
                    If drprv("Degree") IsNot DBNull.Value _
                    AndAlso drprv("Degree") = "" Then
                        Provider = Trim(drprv("LastName_BSN")) & ", " &
                        Trim(drprv("FirstName")) & " " & Trim(drprv("Degree"))
                    Else
                        Provider = Trim(drprv("LastName_BSN")) &
                        ", " & Trim(drprv("FirstName"))
                    End If
                End If
            End While
        End If
        cnprv.Close()
        cnprv = Nothing

        '==============================================
        Dim cnreq As New SqlClient.SqlConnection(connString)
        cnreq.Open()
        Dim cmdreq As New SqlClient.SqlCommand("Select RequisitionNo from Requisitions where ID = " & AccID & "", cnreq)
        cmdreq.CommandType = CommandType.Text
        Dim drreq As SqlClient.SqlDataReader = cmdreq.ExecuteReader
        If drreq.HasRows Then
            While drreq.Read

                If drreq("RequisitionNo") IsNot DBNull.Value Then

                    ReqNo = Trim(drreq("RequisitionNo"))

                End If
            End While
        End If
        cnreq.Close()
        cnreq = Nothing
        '==============================================

        '
        Dim cnpat As New SqlClient.SqlConnection(connString)
        cnpat.Open()
        Dim cmdpat As New SqlClient.SqlCommand("Select * from Patients where ID in " &
        "(Select Patient_ID from Requisitions where ID = " & AccID & ")", cnpat)
        cmdpat.CommandType = CommandType.Text
        Dim drpat As SqlClient.SqlDataReader = cmdpat.ExecuteReader
        If drpat.HasRows Then
            While drpat.Read
                Patient = drpat("LastName") & ", " & drpat("FirstName") & "-" &
                drpat("Sex") & "-" & Format(drpat("DOB"), SystemConfig.DateFormat)
            End While
        End If
        cnpat.Close()
        cnpat = Nothing
        '
        Dim Comps As String = ""
        'Dim Rsp As New ADODB.Recordset
        Dim CSN As String
        Dim cnnm As New SqlClient.SqlConnection(connString)
        cnnm.Open()
        Dim cmdnm As New SqlClient.SqlCommand("Select * " &
        "from Req_TGP where Accession_ID = " & AccID, cnnm)
        cmdnm.CommandType = CommandType.Text
        Dim drnm As SqlClient.SqlDataReader = cmdnm.ExecuteReader
        If drnm.HasRows Then
            While drnm.Read
                If drnm("TGP_Type") = "P" Then


                    Dim cngt As New SqlClient.SqlConnection(connString)
                    cngt.Open()
                    Dim cmdgt As New SqlClient.SqlCommand("Select * from " &
                    "Prof_GrpTst where Profile_ID = " & drnm("TGP_ID"), cngt)
                    cmdgt.CommandType = CommandType.Text
                    Dim drgt As SqlClient.SqlDataReader = cmdgt.ExecuteReader
                    If drgt.HasRows Then
                        While drgt.Read
                            CSN = Trim(GetTGPShortName(drgt("Grptst_ID")))
                            If InStr(Comps, CSN) = 0 Then Comps += CSN & ","
                        End While
                    End If
                    cngt.Close()
                    cngt = Nothing
                Else
                    CSN = Trim(GetTGPShortName(drnm("TGP_ID")))
                    If InStr(Comps, CSN) = 0 Then Comps += CSN & ","
                End If
            End While
        End If
        cnnm.Close()
        cnnm = Nothing
        '
        Tests = Comps
        'Dim cn3 As New Data.SqlClient.SqlConnection(connstring)
        'cn3.Open()
        'Dim csncmd As New Data.SqlClient.SqlCommand("Select (Select b.Abbr from Tests b where b.ID = a.TGP_ID " & _
        '"Union Select c.Abbr from Groups c where c.ID = a.TGP_ID Union Select d.Abbr from Profiles d where " & _
        '"d.ID = a.TGP_ID ) as TGPName from Req_TGP a where a.Accession_ID = " & AccID, cn3)
        'csncmd.CommandType = Data.CommandType.Text
        'Dim csnDR As Data.SqlClient.SqlDataReader = csncmd.ExecuteReader
        'If csnDR.HasRows Then
        '    While csnDR.Read
        '        If InStr(Comps, Trim(csnDR("TGPName"))) = 0 Then Comps += Trim(csnDR("TGPName")) & ","
        '    End While
        '    Comps = Comps.Substring(0, Len(Comps) - 1)
        '    Tests = Comps
        'End If
        'csnDR.Close()
        'csncmd.Dispose()
        'cn3.Close()
        'cn3 = Nothing
        Dim cncd As New SqlClient.SqlConnection(connString)
        cncd.Open()
        Dim cmdcd As New SqlClient.SqlCommand("Select (Select " &
        "Min(SourceDate) from Specimens where Accession_ID = " & AccID &
        ") as DOC, EMRNo from Requisitions where ID = " & AccID, cncd)
        cmdcd.CommandType = CommandType.Text
        Dim drcd As SqlClient.SqlDataReader = cmdcd.ExecuteReader
        If drcd.HasRows Then
            While drcd.Read
                AccDate = Format(drcd("DOC"), SystemConfig.DateFormat)
                If drcd("EMRNo") IsNot DBNull.Value _
                AndAlso Trim(drcd("EMRNo")) <> "" Then _
                EMRNo = Trim(drcd("EMRNo"))
            End While
        End If
        cncd.Close()
        cncd = Nothing
        '
        Dim SRC() As String
        '
        If Sources(0) <> "" Then
            For i = LBound(Sources) To UBound(Sources)
                SRC = Split(Sources(i), "^")
                If labelInfo(UBound(labelInfo)) <> "" Then _
                ReDim Preserve labelInfo(UBound(labelInfo) + 1)
                labelInfo(UBound(labelInfo)) = Provider & "|" &
                Patient & "|" & AccID.ToString & "-" & SRC(0) &
                "|" & AccDate & "|" & SRC(1) & "|" & EMRNo &
                "|" & Val(SRC(2) & "|" & ReqNo)
            Next
        End If
        If QTY > 0 Then
            If labelInfo(UBound(labelInfo)) <> "" Then _
            ReDim Preserve labelInfo(UBound(labelInfo) + 1)
            labelInfo(UBound(labelInfo)) = Provider & "|" &
            Patient & "|" & AccID.ToString & "|" &
            AccDate & "|" & Tests & "|" & EMRNo & "|" & QTY & "|" & ReqNo
        End If
        '
        Return labelInfo
    End Function

    Private Sub PrintLabel_old(ByVal Printer As String, ByVal Accession As String, ByVal Labels As Integer, Optional ReqNo As String = "")
        Dim i As Integer
        If Printer = "Brady" Then
            If String.IsNullOrEmpty(ReqNo) Then
                Dim req = CommonData.RetrieveColumnValue("Requisitions", "RequisitionNo", "ID", Accession, "")

                ReqNo = req.ToString()

            End If
            Dim Initial = txtInitial.Text
            Dim Date1 = dtpDate.Value
            Dim P = txtP.Text
            Dim par() As Object = {Accession + "|" + ReqNo.Trim() + "|" + P + "|" + Initial, Labels.ToString()}
            Dim res = Services.InvokeMethod("BradyPrinter.dll", "Automation", "SaveTextFile", par)
            Return
        End If
        Dim lblPath As String = ""
        ''______________________________________________________

        ''______________________________________________________
        If SystemConfig.AccLabel <> "" Then
            'lblPath = ValidateLabelFile(SystemConfig.AccLabel)
            lblPath = GetReportPath(SystemConfig.AccLabel)
        Else
            If InStr(Printer, "DYMO") > 0 Then
                'lblPath = My.Application.Info.DirectoryPath & "\Reports\Dymo30334Tst.Label"
                lblPath = GetReportPath("Dymo30334Tst.Label")
            Else    'Zebra
                'lblPath = My.Application.Info.DirectoryPath & "\Reports\ZebraAccTst.rpt"
                lblPath = GetReportPath("ZebraAccTst.rpt")
            End If
        End If
        If InStr(Printer, "DYMO") > 0 Then
            Dim LabelInfo() As String = GetLabelInfo(Accession, Labels)
            Dim RawData As String = ""

            If InStr(Printer, "Prolis Remote") > 0 And ThisUser.UseRemotePrinter Then
                For i = LBound(LabelInfo) To UBound(LabelInfo)
                    RawData = LabelInfo(i).ToString
                    If InStr(Printer, "Prolis Remote") > 0 Then
                        ExecuteSqlProcedure("insert into LabelPrintJobs(Processed,PrintJob,PrinterPC) values(0,'" & RawData & "','" & ThisUser.PrinterPC & "')")

                    End If
                Next
                Return
            End If

            For i = LBound(LabelInfo) To UBound(LabelInfo)
                RawData = LabelInfo(i).ToString
                Dim Data() As String = Split(RawData, "|")
                ' WriteTextToFile("Log" & i & ".txt", RawData)

                'Todo: Dymo Code
                '=============================

                'DymoAddIn = New DYMO.DymoAddIn
                'DymoLabel = New DYMO.DymoLabels
                ''lblPath = "C:\Reports\DymoAcc1x2-18.Label"
                'If DymoAddIn.Open(lblPath) Then
                '    DymoLabel.SetField("Provider", Data(0))

                '    If Not lblPath.ToLower().Contains("skippa") Then
                '        DymoLabel.SetField("Patient", Data(1))
                '    End If

                '    DymoLabel.SetField("AccID", Data(2))
                '    Try
                '        If lblPath.ToLower().Contains("skippa") Then
                '            Dim pname = Data(1).Split("-")(0).TrimEnd(",")
                '            Dim Gender = "" & Data(1).Split("-")(1)
                '            Dim dob = "" & Data(1).Split("-")(2)
                '            Dim currentDate As Date = Format(Date.Now, SystemConfig.DateFormat)
                '            Dim birthdate As Date = dob

                '            Dim age As Integer = currentDate.Year - birthdate.Year

                '            DymoLabel.SetField("Patient", pname)
                '            DymoLabel.SetField("Patient_1", "DOB:" & dob & "    Age:" & age & " " & Gender.ToUpper())
                '        End If
                '    Catch ex As Exception

                '    End Try
                '    DymoLabel.SetField("AccDate", Data(3))
                '    DymoLabel.SetField("Tests", Data(4))
                '    DymoLabel.SetField("EMRNo", Data(5))
                '    If Data.Length > 7 Then
                '        Try

                '            DymoLabel.SetField("ReqNo", Data(7))
                '        Catch ex As Exception
                '            'MsgBox("Data(7): " & ex.Message)
                '        End Try
                '    End If


                '    '==========================
                '    If SystemConfig.P_inputOnLabel Then
                '        DymoLabel.SetField("Initial", txtInitial.Text)
                '        DymoLabel.SetField("Date", dtpDate.Value)
                '        DymoLabel.SetField("P?", txtP.Text)
                '    End If


                '    '==========================
                '    Try
                '        DymoAddIn.Print(Val(Data(6)), False)
                '    Catch ex As Exception
                '        DymoAddIn.Print(1, False)
                '    End Try

                'Else
                '    MsgBox("Dymo Label file can not be opened", MsgBoxStyle.Critical, "Prolis")
                'End If
                'DymoAddIn = Nothing
                'DymoLabel = Nothing
                '=======================================================
            Next
        Else
            Dim UID As String = My.Settings.UID.ToString
            Dim PWD As String = My.Settings.PWD.ToString
            'Todo: Crystal Reports Code
            '=============================
            'Dim gReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            'If Not SystemConfig.AccLabel Is Nothing AndAlso SystemConfig.AccLabel <> "" Then
            '    gReport.Load(lblPath)
            'Else
            '    If InStr(Printer, "Zebra") > 0 Then
            '        'gReport.Load(Application.StartupPath & "\Reports\ZebraLabel.rpt")
            '        gReport.Load(GetReportPath("ZebraLabel.rpt"))
            '    Else
            '        'gReport.Load(Application.StartupPath & "\Reports\GenLabel.rpt")
            '        gReport.Load(GetReportPath("GenLabel.rpt"))
            '    End If
            'End If
            'Dim pSize As CrystalDecisions.Shared.PaperSize = gReport.PrintOptions.PaperSize
            'gReport.SetDatabaseLogon(UID, PWD)
            'gReport.RecordSelectionFormula = "{Requisitions.ID} = " & Val(Accession)
            'gReport.PrintOptions.PrinterName = Printer
            'gReport.PrintOptions.PaperSize = pSize
            'gReport.PrintToPrinter(Labels, False, 0, 0)
            'gReport.Close()
            'gReport = Nothing
            '===================================
        End If
    End Sub
    Function WriteTextToFile(filePath As String, content As String) As Boolean
        Try
            ' If the file doesn't exist, create it. If it exists, overwrite it.
            Using writer As New StreamWriter(filePath, False) ' The second parameter (False) indicates to overwrite the file.
                writer.Write(content)
            End Using

            Return True
        Catch ex As Exception
            ' Handle exceptions or log them as needed.
            Console.WriteLine("Error writing to the file: " & ex.Message)
            Return False
        End Try
    End Function
    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Dim sSQL As String = ""

        Dim Printer As String = GetLabelPrinterName()
        If Not ThisUser.SpecificPrinter = "Default" Then
            Printer = ThisUser.SpecificPrinter
        End If
        If Printer = "" Then
            MsgBox("The label printer is not configured. Configure it using the System Configuration and try.")
        Else
            If cmbDataType.SelectedIndex <> 2 Then  'regular
                If txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                    If cmbDataType.SelectedIndex = 0 Then
                        sSQL = "Select ID ,RequisitionNo from Requisitions where ID between " &
                        Val(txtAccFrom.Text) & " and " & Val(txtAccTo.Text)
                    ElseIf cmbDataType.SelectedIndex = 1 Then    'Req
                        sSQL = "Select ID,RequisitionNo from Requisitions where RequisitionNo between '" &
                        Trim(txtAccFrom.Text) & "' and '" & Trim(txtAccTo.Text) & "'"
                    End If
                ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
                    If cmbDataType.SelectedIndex = 0 Then
                        sSQL = "Select ID,RequisitionNo from Requisitions where ID = " & Val(txtAccFrom.Text)
                    ElseIf cmbDataType.SelectedIndex = 1 Then
                        sSQL = "Select ID,RequisitionNo from Requisitions where RequisitionNo = '" &
                        Trim(txtAccFrom.Text) & "'"
                    End If
                ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
                    If cmbDataType.SelectedIndex = 0 Then
                        sSQL = "Select ID,RequisitionNo from Requisitions where ID = " & Val(txtAccTo.Text)
                    ElseIf cmbDataType.SelectedIndex = 1 Then
                        sSQL = "Select ID,RequisitionNo from Requisitions where RequisitionNo = '" &
                        Trim(txtAccTo.Text) & "'"
                    End If
                ElseIf HasDiscreteValues() = True Then
                    If cmbDataType.SelectedIndex = 0 Then
                        sSQL = "Select ID,RequisitionNo from Requisitions where ID in ("
                    ElseIf cmbDataType.SelectedIndex = 1 Then
                        sSQL = "Select ID,RequisitionNo from Requisitions where RequisitionNo in ("
                    End If
                    '
                    For i As Integer = 0 To dgvDiscrete.RowCount - 1
                        If cmbDataType.SelectedIndex = 0 Then
                            If dgvDiscrete.Rows(i).Cells(0).Value <> "" Then _
                            sSQL += dgvDiscrete.Rows(i).Cells(0).Value & ", "
                        Else
                            If dgvDiscrete.Rows(i).Cells(0).Value <> "" Then _
                            sSQL += "'" & Trim(dgvDiscrete.Rows(i).Cells(0).Value) & "', "
                        End If
                    Next
                    sSQL = sSQL.Substring(0, Len(sSQL) - 2) & ")"
                End If
                '
                If sSQL <> "" Then
                    Dim NestedQty As Integer = 0
                    Dim specimenList As New List(Of String)
                    For i = 0 To dgvSources.RowCount - 1
                        If CType(dgvSources.Rows(i).Cells(1).Value, Boolean) = True AndAlso Val(dgvSources.Rows(i).Cells(3).Value) > 0 Then
                            NestedQty = Val(dgvSources.Rows(i).Cells(3).Value)
                            Exit For
                        End If
                    Next

                    For i = 0 To dgvSources.RowCount - 1
                        If CType(dgvSources.Rows(i).Cells(1).Value, Boolean) = True AndAlso Val(dgvSources.Rows(i).Cells(3).Value) > 0 Then
                            specimenList.Add($"{dgvSources.Rows(i).Cells(0).Value}|{dgvSources.Rows(i).Cells(3).Value * CInt(txtQty.Text)}")
                        End If
                    Next

                    If Val(txtQty.Text) > 0 Or NestedQty > 0 Then
                        If Printer <> "" Then
                            Dim cnprn As New SqlClient.SqlConnection(connString)
                            cnprn.Open()
                            Dim cmdprn As New SqlClient.SqlCommand(sSQL, cnprn)
                            cmdprn.CommandType = CommandType.Text
                            Dim drprn As SqlClient.SqlDataReader = cmdprn.ExecuteReader
                            If drprn.HasRows Then
                                While drprn.Read
                                    If HasDiscreteValues() Then
                                        PrintLabels(Printer, drprn("ID").ToString, CInt(txtQty.Text), drprn("RequisitionNo"), specimenList, 2)

                                    Else
                                        PrintLabels(Printer, drprn("ID").ToString, CInt(txtQty.Text), drprn("RequisitionNo"), specimenList, 1)

                                    End If
                                End While
                            End If
                            cnprn.Close()
                            cnprn = Nothing
                        Else
                            MsgBox("You need to install the label printer and configure it." _
                            , MsgBoxStyle.Information, "Prolis Accession")
                        End If
                    End If
                    FormClear()
                    'Catch Ex As Exception
                    'End Try
                Else
                    MsgBox("You must make a selection, to process the report", MsgBoxStyle.Critical, "Prolis")
                End If
            Else    'Free Form
                If txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
                    Dim Count As Integer = 0    'Val(txtAccTo.Text)
                    Dim i As Long = Val(txtAccFrom.Text)
                    Do Until Count >= Val(txtAccTo.Text)
                        If IsUniqueAccession(i) Then
                            PrintLabels(Printer, i, CInt(txtQty.Text),,, 1)
                            Count += 1
                        End If
                        i += 1
                    Loop
                ElseIf HasDiscreteValues() = True Then
                    For i As Integer = 0 To dgvDiscrete.RowCount - 1
                        If dgvDiscrete.Rows(i).Cells(0).Value IsNot Nothing _
                        AndAlso dgvDiscrete.Rows(i).Cells(0).Value <> "" _
                        AndAlso IsNumeric(dgvDiscrete.Rows(i).Cells(0).Value) Then
                            PrintLabels(Printer, Val(dgvDiscrete.Rows(i).Cells(0).Value), CInt(txtQty.Text),,, 1)
                        End If
                    Next
                End If
                FormClear()
            End If
        End If
    End Sub

    Private Function IsUniqueAccession(ByVal AccID As Long) As Boolean
        Dim Unique As Boolean = True
        Dim sSQL As String = "Select ID from Requisitions where ID = " & AccID
        Dim cnua As New SqlClient.SqlConnection(connString)
        cnua.Open()
        Dim cmdua As New SqlClient.SqlCommand(sSQL, cnua)
        cmdua.CommandType = CommandType.Text
        Dim drua As SqlClient.SqlDataReader = cmdua.ExecuteReader
        If drua.HasRows Then Unique = False
        cnua.Close()
        cnua = Nothing
        Return Unique
    End Function

    Private Function HasDiscreteValues() As Boolean
        Dim HasVal As Boolean = False
        Dim i As Integer
        For i = 0 To dgvDiscrete.RowCount - 1
            If dgvDiscrete.Rows(i).Cells(0).Value <> "" Then
                HasVal = True
                Exit For
            End If
        Next
        HasDiscreteValues = HasVal
    End Function

    Private Sub Update_Progress()
        If (txtAccFrom.Text <> "" Or txtAccFrom.Text <> "" _
        Or HasDiscreteValues() = True) And Val(txtQty.Text) > 0 Then
            btnProcess.Enabled = True
        Else
            btnProcess.Enabled = False
        End If
    End Sub

    Private Sub dgvDiscrete_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDiscrete.CellEndEdit
        If dgvDiscrete.Rows(e.RowIndex).Cells(0).Value <> String.Empty Then
            If IsNumeric(dgvDiscrete.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) = False Then
                MsgBox("Only digits are allowed.")
                If e.RowIndex = dgvDiscrete.RowCount - 1 Then
                    dgvDiscrete.Rows(e.RowIndex).Cells(0).Value = ""
                Else
                    dgvDiscrete.Rows.RemoveAt(e.RowIndex)
                End If
            Else
                If e.RowIndex = dgvDiscrete.RowCount - 1 Then dgvDiscrete.Rows.Add()
                txtAccFrom.Text = ""
                txtAccTo.Text = ""
                SendKeys.Send("{DOWN}")
            End If
        Else
            If e.RowIndex = dgvDiscrete.RowCount - 1 Then
                dgvDiscrete.Rows(e.RowIndex).Cells(0).Value = ""
            Else
                dgvDiscrete.Rows.RemoveAt(e.RowIndex)
            End If
        End If
        If HasDiscreteValues() = True Then
            btnClear.Enabled = True
        Else
            btnClear.Enabled = False
        End If
        Update_Progress()
    End Sub

    Private Sub DiscreteClear()
        Dim i As Integer
        For i = dgvDiscrete.RowCount - 1 To 0 Step -1
            dgvDiscrete.Rows(i).Cells(0).Value = ""
            If i > 0 Then dgvDiscrete.Rows.RemoveAt(i)
        Next
    End Sub

    Private Sub txtQty_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.Validated
        If txtQty.Text = "" Then txtQty.Text = "1"
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub txtAccTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccTo.Validated
        If txtAccTo.Text <> "" Then DiscreteClear()
        UpdateSources()
        Update_Progress()
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
            Update_Progress()
        End If
    End Sub

    Private Sub dgvDiscrete_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvDiscrete.Validated
        UpdateSources()
        Update_Progress()
    End Sub

    Private Sub btnDeselAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer
        For i = 0 To dgvSources.RowCount - 1
            dgvSources.Rows(i).Cells(1).Value = False
        Next
    End Sub

    Private Sub btnSelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer
        For i = 0 To dgvSources.RowCount - 1
            dgvSources.Rows(i).Cells(1).Value = True
        Next
    End Sub

    Private Sub ClearForm()
        txtAccFrom.Text = ""
        txtAccTo.Text = ""
        dgvSources.Rows.Clear()
        If dgvDiscrete.RowCount > 0 Then
            Dim i As Integer = 0
            For i = 0 To dgvDiscrete.RowCount - 1
                dgvDiscrete.Rows(i).Cells(0).Value = ""
            Next
        End If
    End Sub

    Private Sub dgvSources_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSources.CellEndEdit
        If e.ColumnIndex = 3 Then  'QTY
            If Not IsNumeric(Trim(dgvSources.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)) _
            OrElse Trim(dgvSources.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) = "" Then
                dgvSources.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "1"
            End If
        End If
    End Sub

    Private Sub cmbDataType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDataType.SelectedIndexChanged
        If cmbDataType.SelectedIndex = 2 Then   'Free Form
            lblTo.Text = "Count"
            txtAccTo.MaxLength = 3
        Else
            lblTo.Text = "To"
            txtAccTo.MaxLength = 16
        End If
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        txtAccFrom.Text = Clipboard.GetText()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        txtAccTo.Text = Clipboard.GetText()
    End Sub

    Private Sub ChkRemotePrint_CheckedChanged(sender As Object, e As EventArgs) Handles ChkRemotePrint.CheckedChanged
        ThisUser.UseRemotePrinter = ChkRemotePrint.Checked
        Dim result = 0
        If ThisUser.UseRemotePrinter Then
            result = 1
        End If
        Dim sd = "if exists (select * from Users where UserName = '" & ThisUser.UserName.Trim() & "') update users set UseRemotePrinter =" & result & " where UserName = '" & ThisUser.UserName.Trim() & "'"
        ExecuteSqlProcedure(sd)
    End Sub

    Private Sub frmPrintLabels_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'ThisUser.UseRemotePrinter = ChkRemotePrint.Checked
        'Dim result = 0
        'If ThisUser.UseRemotePrinter Then
        '    result = 1
        'End If
        'Dim sd = "if exists (select * from Users where UserName = '" & ThisUser.UserName.Trim() & "') update users set UseRemotePrinter =" & result & " where UserName = '" & ThisUser.UserName.Trim() & "'"
        'ExecuteSqlProcedure(sd)
    End Sub

    Private Sub dgvSources_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSources.CellClick
        If e.ColumnIndex = 1 Then
            If dgvSources.Rows(e.RowIndex).Cells(e.ColumnIndex).Value Then
                dgvSources.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = False
            Else
                dgvSources.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = True

            End If

        End If
    End Sub
End Class
