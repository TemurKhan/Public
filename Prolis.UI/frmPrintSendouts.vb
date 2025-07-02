Imports System.Windows.Forms
Imports Microsoft.Data.SqlClient

Public Class frmPrintSendouts
    'TODO: Dymo Code
    'Public DymoAddIn As DYMO.DymoAddIn
    'Public DymoLabel As DYMO.DymoLabels

    Private Sub chkOrderLabel_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOrderLabel.CheckedChanged
        If chkOrderLabel.Checked = False Then
            chkOrderLabel.Text = "Lab Orders"
            chkOrderLabel.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\ReqGen.ico")
        Else
            chkOrderLabel.Text = "Labels"
            chkOrderLabel.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Worksheet.ico")
        End If
    End Sub

    Private Sub txtAccFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccFrom.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtAccTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccTo.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub frmPrintSendouts_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtQty.Text = "1"
        dgvDiscrete.RowCount = 1
        LoadLabs()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

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
        If chkPT.Checked = False Then
            If ((txtAccFrom.Text <> "" Or txtAccFrom.Text <> "" Or HasDiscreteValues() = True) Or
            (IsDate(txtDateFrom.Text) Or IsDate(txtDateTo.Text))) And Val(txtQty.Text) > 0 Then
                btnProcess.Enabled = True
            Else
                btnProcess.Enabled = False
            End If
        Else
            If ((txtAccFrom.Text <> "" Or txtAccFrom.Text <> "" Or HasDiscreteValues() = True) Or
            (IsDate(txtDateFrom.Text) Or IsDate(txtDateTo.Text))) And cmbRefLab.SelectedIndex > 0 Then
                btnProcess.Enabled = True
            Else
                btnProcess.Enabled = False
            End If
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
                If e.RowIndex = dgvDiscrete.RowCount - 1 Then
                    dgvDiscrete.Rows.Add()
                    SendKeys.Send("{ENTER}")
                End If
                txtAccFrom.Text = ""
                txtAccTo.Text = ""
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

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub DiscreteClear()
        Dim i As Integer
        For i = dgvDiscrete.RowCount - 1 To 0 Step -1
            dgvDiscrete.Rows(i).Cells(0).Value = ""
            If i > 0 Then dgvDiscrete.Rows.RemoveAt(i)
        Next
    End Sub

    Private Sub FormClear()
        txtAccFrom.Text = ""
        txtAccTo.Text = ""
        txtDateFrom.Text = ""
        txtDateTo.Text = ""
        DiscreteClear()
        txtQty.Text = "1"
        btnProcess.Enabled = False
    End Sub

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Dim sSQL As String = ""
        Dim Accs As String = ""
        Dim ItemX As MyList
        If txtAccFrom.Text <> "" And txtAccTo.Text <> "" Then
            If Val(txtAccFrom.Text) <= Val(txtAccTo.Text) Then
                sSQL = "Select * from Sendouts where Accession_ID >= " &
                Val(txtAccFrom.Text) & " and Accession_ID <= " & Val(txtAccTo.Text)
            Else
                sSQL = "Select * from Sendouts where Accession_ID >= " &
                Val(txtAccTo.Text) & " and Accession_ID <= " & Val(txtAccFrom.Text)
            End If
        ElseIf txtAccFrom.Text <> "" And txtAccTo.Text = "" Then
            sSQL = "Select * from Sendouts where Accession_ID = " &
                Val(txtAccFrom.Text)
        ElseIf txtAccFrom.Text = "" And txtAccTo.Text <> "" Then
            sSQL = "Select * from Sendouts where Accession_ID = " &
                Val(txtAccTo.Text)
        ElseIf HasDiscreteValues() = True Then
            sSQL = "Select * from Sendouts where Accession_ID in ("
            Dim i As Integer
            For i = 0 To dgvDiscrete.RowCount - 1
                If dgvDiscrete.Rows(i).Cells(0).Value <> "" Then _
                sSQL += dgvDiscrete.Rows(i).Cells(0).Value & ", "
            Next
            sSQL = sSQL.Substring(0, Len(sSQL) - 2) & ")"
        End If
        '
        If IsDate(txtDateFrom.Text) And IsDate(txtDateTo.Text) Then 'Both dates
            If sSQL <> "" Then
                sSQL += " and SentDate between '" & Format(CDate(txtDateFrom.Text), SystemConfig.DateFormat) &
                "' and '" & Format(CDate(txtDateTo.Text), SystemConfig.DateFormat) & " 23:59:00'"
            Else
                sSQL = "Select * from Sendouts where SentDate between '" & Format(CDate(txtDateFrom.Text),
                SystemConfig.DateFormat) & "' and '" & Format(CDate(txtDateTo.Text), SystemConfig.DateFormat) & " 23:59:00'"
            End If
        ElseIf IsDate(txtDateFrom.Text) And Not IsDate(txtDateTo.Text) Then 'from date
            If sSQL <> "" Then
                sSQL += " and SentDate between '" & Format(CDate(txtDateFrom.Text), SystemConfig.DateFormat) &
                "' and '" & Format(CDate(txtDateFrom.Text), SystemConfig.DateFormat) & " 23:59:00'"
            Else
                sSQL = "Select * from Sendouts where SentDate between '" & Format(CDate(txtDateFrom.Text),
                SystemConfig.DateFormat) & "' and '" & Format(CDate(txtDateFrom.Text), SystemConfig.DateFormat) & " 23:59:00'"
            End If
        ElseIf Not IsDate(txtDateFrom.Text) And IsDate(txtDateTo.Text) Then 'To date
            If sSQL <> "" Then
                sSQL += " and SentDate between '" & Format(CDate(txtDateTo.Text), SystemConfig.DateFormat) &
                "' and '" & Format(CDate(txtDateTo.Text), SystemConfig.DateFormat) & " 23:59:00'"
            Else
                sSQL = "Select * from Sendouts where SentDate between '" & Format(CDate(txtDateTo.Text),
                SystemConfig.DateFormat) & "' and '" & Format(CDate(txtDateTo.Text), SystemConfig.DateFormat) & " 23:59:00'"
            End If
        End If
        '
        If cmbRefLab.SelectedIndex > 0 Then     'Lab Selected
            ItemX = cmbRefLab.SelectedItem
            If sSQL <> "" Then sSQL += " and Lab_ID = " & ItemX.ItemData
        End If
        '
        'If sSQL <> "" Then
        '    Dim CNP As New ADODB.Connection
        '    CNP.Open(connstring)
        '    If chkPT.Checked = False Then   'Printing
        '        If chkOrderLabel.Checked = True Then   'Label
        '            'Try
        '            If Val(txtQty.Text) > 0 Then
        '                Dim Printer As String = GetLabelPrinterName()
        '                If Not ThisUser.SpecificPrinter = "Default" Then
        '                    Printer = ThisUser.SpecificPrinter
        '                End If
        '                If Printer <> "" Then
        '                    Dim LabDocs() As String

        '                    Dim Rs As New ADODB.Recordset
        '                    Rs.Open(sSQL, CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        '                    If Not Rs.BOF Then
        '                        Do Until Rs.EOF
        '                            LabDocs = GetLabDocuments(Rs.Fields("Lab_ID").Value)
        '                            If LabDocs(1) <> "" Then _
        '                            PrintLabels(Rs.Fields("ID").Value, LabDocs(1), CInt(txtQty.Text))
        '                            Rs.MoveNext()
        '                        Loop
        '                    End If
        '                    Rs.Close()
        '                    Rs = Nothing
        '                Else
        '                    MsgBox("You need to install the label printer and configure it." _
        '                    , MsgBoxStyle.Information, "Prolis Sendouts")
        '                End If
        '            End If
        '            FormClear()
        '            'Catch Ex As Exception
        '            'End Try
        '        Else    'Order
        '            If cmbRefLab.SelectedIndex > 0 Then
        '                ItemX = cmbRefLab.SelectedItem
        '                Dim Rs As New ADODB.Recordset

        '                Rs.Open(sSQL, CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        '                If Not Rs.BOF Then
        '                    Do Until Rs.EOF
        '                        Accs += Rs.Fields("Accession_ID").Value.ToString & ", "
        '                        'PrintLabDocument(Rs.Fields("ID").Value.ToString, CInt(txtQty.Text))
        '                        Rs.MoveNext()
        '                    Loop
        '                    If Len(Accs) > 2 Then Accs = Microsoft.VisualBasic.Mid(Accs, 1, Len(Accs) - 2)
        '                End If
        '                Rs.Close()
        '                Rs = Nothing
        '                PrintLabDocument(ItemX.ItemData, Accs)
        '            Else
        '                MsgBox("Inorder to print, Reference Lab must be selected" _
        '                , MsgBoxStyle.Information, "Prolis Sendouts")
        '            End If
        '        End If
        '    Else    'Sent to Interface
        '        If cmbRefLab.SelectedIndex > 0 Then
        '            ItemX = cmbRefLab.SelectedItem
        '            Dim Rs As New ADODB.Recordset
        '            Rs.Open(sSQL, CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        '            If Not Rs.BOF Then
        '                Do Until Rs.EOF
        '                    PerformInterfaceDisposition(Rs.Fields("ID").Value.ToString, ItemX.ItemData)
        '                    Rs.MoveNext()
        '                Loop
        '            End If
        '            Rs.Close()
        '            Rs = Nothing
        '        Else
        '            MsgBox("Reference Lab must be selected for the Interface communication" _
        '            , MsgBoxStyle.Information, "Prolis Sendouts")
        '        End If
        '    End If
        '    CNP.Close()
        '    CNP = Nothing
        '    FormClear()
        'End If
        If Not String.IsNullOrEmpty(sSQL) Then
            Using connection As New SqlConnection(connString)
                connection.Open()

                If Not chkPT.Checked Then ' Printing
                    If chkOrderLabel.Checked Then ' Label Printing
                        If Val(txtQty.Text) > 0 Then
                            Dim Printer As String = If(ThisUser.SpecificPrinter = "Default", GetLabelPrinterName(), ThisUser.SpecificPrinter)

                            If Not String.IsNullOrEmpty(Printer) Then
                                Using command As New SqlCommand(sSQL, connection)
                                    Using reader As SqlDataReader = command.ExecuteReader()
                                        While reader.Read()
                                            Dim LabDocs() As String = GetLabDocuments(reader("Lab_ID"))
                                            If Not String.IsNullOrEmpty(LabDocs(1)) Then
                                                PrintLabels(reader("ID"), LabDocs(1), CInt(txtQty.Text))
                                            End If
                                        End While
                                    End Using
                                End Using
                            Else
                                MsgBox("You need to install the label printer and configure it.", MsgBoxStyle.Information, "Prolis Sendouts")
                            End If
                        End If
                        FormClear()
                    Else ' Order Processing
                        If cmbRefLab.SelectedIndex > 0 Then
                            ItemX = cmbRefLab.SelectedItem
                            'Dim Accs As String = ""

                            Using command As New SqlCommand(sSQL, connection)
                                Using reader As SqlDataReader = command.ExecuteReader()
                                    While reader.Read()
                                        Accs &= reader("Accession_ID").ToString() & ", "
                                    End While
                                End Using
                            End Using

                            If Accs.Length > 2 Then Accs = Accs.Substring(0, Accs.Length - 2)
                            PrintLabDocument(ItemX.ItemData, Accs)
                        Else
                            MsgBox("In order to print, Reference Lab must be selected.", MsgBoxStyle.Information, "Prolis Sendouts")
                        End If
                    End If
                Else ' Sent to Interface
                    If cmbRefLab.SelectedIndex > 0 Then
                        ItemX = cmbRefLab.SelectedItem

                        Using command As New SqlCommand(sSQL, connection)
                            Using reader As SqlDataReader = command.ExecuteReader()
                                While reader.Read()
                                    PerformInterfaceDisposition(reader("ID").ToString(), ItemX.ItemData)
                                End While
                            End Using
                        End Using
                    Else
                        MsgBox("Reference Lab must be selected for the Interface communication.", MsgBoxStyle.Information, "Prolis Sendouts")
                    End If
                End If
            End Using

            FormClear()
        End If

    End Sub

    Private Sub PerformInterfaceDisposition0(ByVal SendoutID As Long, ByVal LabID As Integer)
        'Dim Rs As New ADODB.Recordset
        'Dim InterfaceID As Integer = GetInterfaceID(4, LabID)
        'If InterfaceID <> -1 Then
        '    Dim CNP As New ADODB.Connection
        '    CNP.Open(connstring)
        '    Rs.Open("Select * from Sendout_Disbursement where Sendout_ID = " & SendoutID &
        '    " and Interface_ID = " & InterfaceID, CNP, ADODB.CursorTypeEnum.adOpenDynamic,
        '    ADODB.LockTypeEnum.adLockOptimistic)
        '    If Rs.BOF Then Rs.AddNew()
        '    Rs.Fields("Interface_ID").Value = InterfaceID
        '    Rs.Fields("Sendout_ID").Value = SendoutID
        '    Rs.Fields("DisburseDate").Value = Date.Now
        '    Rs.Fields("Routed").Value = 0
        '    Rs.Update()
        '    Rs.Close()
        '    CNP.Close()
        '    CNP = Nothing
        'End If
        'Rs = Nothing
    End Sub

    Private Sub PerformInterfaceDisposition(ByVal SendoutID As Long, ByVal LabID As Integer)
        Dim InterfaceID As Integer = GetInterfaceID(4, LabID)

        If InterfaceID <> -1 Then
            Using connection As New SqlConnection(connString)
                connection.Open()

                Dim queryCheck As String = "SELECT COUNT(*) FROM Sendout_Disbursement WHERE Sendout_ID = @SendoutID AND Interface_ID = @InterfaceID"
                Dim recordExists As Boolean

                Using cmdCheck As New SqlCommand(queryCheck, connection)
                    cmdCheck.Parameters.AddWithValue("@SendoutID", SendoutID)
                    cmdCheck.Parameters.AddWithValue("@InterfaceID", InterfaceID)
                    recordExists = Convert.ToInt32(cmdCheck.ExecuteScalar()) > 0
                End Using

                Dim query As String
                If recordExists Then
                    query = "UPDATE Sendout_Disbursement SET DisburseDate = @DisburseDate, Routed = @Routed WHERE Sendout_ID = @SendoutID AND Interface_ID = @InterfaceID"
                Else
                    query = "INSERT INTO Sendout_Disbursement (Interface_ID, Sendout_ID, DisburseDate, Routed) VALUES (@InterfaceID, @SendoutID, @DisburseDate, @Routed)"
                End If

                Using cmd As New SqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@InterfaceID", InterfaceID)
                    cmd.Parameters.AddWithValue("@SendoutID", SendoutID)
                    cmd.Parameters.AddWithValue("@DisburseDate", Date.Now)
                    cmd.Parameters.AddWithValue("@Routed", 0)

                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End If
    End Sub

    Private Sub PrintLabDocument(ByVal LabID As Long, ByVal Accs As String)
        Dim UID As String = My.Settings.UID.ToString
        Dim PWD As String = My.Settings.PWD.ToString

        'TODO: Crystal Reports Code
        '================================================
        'Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        'oRpt.Load(GetReportPath("LabOrderManifest.RPT"))
        'ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database, My.Settings.UID, My.Settings.PWD)
        ''oRpt.SetDatabaseLogon(UID, PWD)
        'oRpt.RecordSelectionFormula = "{SendOuts.Lab_ID} = " & LabID & " and {SendOuts.Accession_ID} in [" & Accs & "];"
        'oRpt.PrintToPrinter(Val(txtQty.Text), False, 0, 0)
        'My.Application.DoEvents()
        'oRpt.Close()
        'oRpt = Nothing
        '================================================
    End Sub

    Private Function GetLabDocuments0(ByVal LabID As Integer) As String()
        'Dim LabDocs() As String = {"", ""}
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connstring)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select * from Labs where ID = " & LabID, CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then
        '    If Not Rs.Fields("DocFile").Value Is System.DBNull.Value AndAlso Rs.Fields("DocFile").Value <>
        '    "" Then LabDocs(0) = Rs.Fields("DocFile").Value
        '    If Not Rs.Fields("LabelFile").Value Is System.DBNull.Value AndAlso Rs.Fields("LabelFile").Value _
        '    <> "" Then LabDocs(1) = Rs.Fields("LabelFile").Value
        'End If
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
        'Return LabDocs
    End Function

    Private Function GetLabDocuments(ByVal LabID As Integer) As String()
        Dim LabDocs() As String = {"", ""}

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String = "SELECT DocFile, LabelFile FROM Labs WHERE ID = @LabID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@LabID", LabID)

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        If Not IsDBNull(reader("DocFile")) AndAlso reader("DocFile").ToString() <> "" Then
                            LabDocs(0) = reader("DocFile").ToString()
                        End If
                        If Not IsDBNull(reader("LabelFile")) AndAlso reader("LabelFile").ToString() <> "" Then
                            LabDocs(1) = reader("LabelFile").ToString()
                        End If
                    End If
                End Using
            End Using
        End Using

        Return LabDocs
    End Function

    Private Sub PrintLabels(ByVal SendoutID As Long, ByVal LabelFile As String, ByVal Labels As Integer)
        Dim Printer As String = GetLabelPrinterName()
        If Not ThisUser.SpecificPrinter = "Default" Then
            Printer = ThisUser.SpecificPrinter
        End If
        If Printer <> "" Then
            If InStr(Printer, "DYMO") > 0 Then    'Dymo setting
                Dim LabelInfo As String() = GetLabelInfo(SendoutID)
                'TODO: Dymo Code
                '============================
                'DymoAddIn = New DYMO.DymoAddIn
                'DymoLabel = New DYMO.DymoLabels
                'If DymoAddIn.Open(LabelFile) Then
                '    DymoLabel.SetField("CLIENT", LabelInfo(0))
                '    DymoLabel.SetField("REQID", LabelInfo(1))
                '    DymoLabel.SetField("Pat", LabelInfo(2))
                '    DymoLabel.SetField("CLINIC", LabelInfo(3))
                '    DymoLabel.SetField("Tests", LabelInfo(4))
                '    DymoAddIn.Print(Labels, False)
                'Else
                '    MsgBox("Dymo Label file can not be opened", MsgBoxStyle.Critical, "Prolis")
                'End If
                'DymoAddIn = Nothing
                'DymoLabel = Nothing
                '============================
            ElseIf InStr(Printer, "Zebra") > 0 Or InStr(Printer, "ZDesigner") > 0 Or
            InStr(Printer, "LP 2824") > 0 Then
                Dim UID As String = My.Settings.UID.ToString
                Dim PWD As String = My.Settings.PWD.ToString
                'TODO: Crystal Reports Code
                '================================================================================
                'Dim gReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                'gReport.Load(LabelFile)
                ''Dim pSize As CrystalDecisions.Shared.PaperSize = gReport.PrintOptions.PaperSize
                'gReport.SetDatabaseLogon(UID, PWD)
                'gReport.RecordSelectionFormula = "{Sendouts.ID} = " & SendoutID.ToString
                'gReport.PrintOptions.PrinterName = Printer
                ''gReport.PrintOptions.PaperSize = pSize
                'gReport.PrintToPrinter(Labels, False, 0, 0)
                'My.Application.DoEvents()
                'gReport.Close()
                'gReport = Nothing
                '================================================================================
            Else
                MsgBox("Prolis does not support '" & Printer & "' for label printing",
                MsgBoxStyle.Critical, "Prolis")
            End If
        Else
            MsgBox("No Label printer configured", MsgBoxStyle.Critical, "Prolis")
        End If
    End Sub

    Private Function GetLabelInfo0(ByVal SendoutID As Long) As String()
        'Dim labelInfo() As String = {""}
        'Dim Acct As String = ""
        'Dim TGPName As String = ""
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connstring)
        'Dim Rs As New ADODB.Recordset
        'Rs.Open("Select * from Labs where ID in(Select Lab_ID from Sendouts where ID = " & SendoutID &
        '")", CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then
        '    If Not Rs.Fields("Account").Value Is System.DBNull.Value Then _
        '        Acct = Trim(Rs.Fields("Account").Value)
        'End If
        'Rs.Close()
        'Rs.Open("Select * from Company where ID = 1", CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then
        '    If Rs.Fields("IsIndividual").Value = 0 Then
        '        If Acct <> "" Then
        '            labelInfo(0) = "Client: " & Acct & "-" & Trim(Rs.Fields("LastName_BSN").Value)
        '        Else
        '            labelInfo(0) = "Client: " & Trim(Rs.Fields("LastName_BSN").Value)
        '        End If
        '    Else    'Individual
        '        If Acct <> "" Then
        '            If Not Rs.Fields("Degree").Value Is System.DBNull.Value AndAlso
        '            Trim(Rs.Fields("Degree").Value) <> "" Then
        '                labelInfo(0) = "Client: " & Acct & "-" & Trim(Rs.Fields("LastName_BSN").Value) &
        '                ", " & Trim(Rs.Fields("FirstName").Value) & " " & Trim(Rs.Fields("Degree").Value)
        '            Else
        '                labelInfo(0) = "Client: " & Acct & "-" & Trim(Rs.Fields("LastName_BSN").Value) &
        '                ", " & Trim(Rs.Fields("FirstName").Value) & " MD"
        '            End If
        '        Else
        '            If Not Rs.Fields("Degree").Value Is System.DBNull.Value AndAlso
        '            Trim(Rs.Fields("Degree").Value) <> "" Then
        '                labelInfo(0) = "Client: " & Trim(Rs.Fields("LastName_BSN").Value) & ", " &
        '                Trim(Rs.Fields("FirstName").Value) & " " & Trim(Rs.Fields("Degree").Value)
        '            Else
        '                labelInfo(0) = "Client: " & Trim(Rs.Fields("LastName_BSN").Value) & ", " &
        '                Trim(Rs.Fields("FirstName").Value) & " MD"
        '            End If
        '        End If
        '    End If
        'End If
        'Rs.Close()
        'ReDim Preserve labelInfo(UBound(labelInfo) + 1)
        'Rs.Open("Select Accession_ID from Sendouts where ID = " & SendoutID, CNP,
        'ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then labelInfo(UBound(labelInfo)) = "Req ID: " &
        'Rs.Fields("Accession_ID").Value.ToString
        'Rs.Close()
        'ReDim Preserve labelInfo(UBound(labelInfo) + 1)
        'Rs.Open("Select * from Patients where ID in (Select Patient_ID from Requisitions where ID in (" &
        '"Select Accession_ID from Sendouts where ID = " & SendoutID & "))", CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then _
        '    labelInfo(UBound(labelInfo)) = "Patient: " & Rs.Fields("LastName").Value _
        '    & ", " & Rs.Fields("FirstName").Value
        'Rs.Close()
        'ReDim Preserve labelInfo(UBound(labelInfo) + 1)
        'Rs.Open("Select * from Providers where ID in (Select AttendingProvider_ID from Requisitions where ID in (" &
        '"Select Accession_ID from Sendouts where ID = " & SendoutID & "))", CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then
        '    If Not Rs.Fields("Degree").Value Is System.DBNull.Value _
        '    AndAlso Trim(Rs.Fields("Degree").Value) <> "" Then
        '        labelInfo(UBound(labelInfo)) = "Provider: " & Rs.Fields("LastName_BSN").Value _
        '        & ", " & Rs.Fields("FirstName").Value & " " & Trim(Rs.Fields("Degree").Value)
        '    Else
        '        labelInfo(UBound(labelInfo)) = "Provider: " & Rs.Fields("LastName_BSN").Value _
        '        & ", " & Rs.Fields("FirstName").Value & " MD"
        '    End If
        'End If
        'Rs.Close()
        'ReDim Preserve labelInfo(UBound(labelInfo) + 1)
        'Rs.Open("Select * from Lab_TGP where TGP_ID in (Select TGP_ID from Sendout_TGP where " &
        '"Sendout_ID = " & SendoutID & ")", CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'If Not Rs.BOF Then
        '    labelInfo(UBound(labelInfo)) = "Orders: "
        '    Do Until Rs.EOF
        '        TGPName = GetTGPShortName(Rs.Fields("TGP_ID").Value)
        '        If Not Rs.Fields("LabComponentID").Value Is System.DBNull.Value _
        '        AndAlso Trim(Rs.Fields("LabComponentID").Value) <> "" Then
        '            labelInfo(UBound(labelInfo)) += Trim(Rs.Fields("LabComponentID").Value) _
        '            & TGPName & ", "
        '        Else
        '            labelInfo(UBound(labelInfo)) += TGPName & ", "
        '        End If
        '        TGPName = ""
        '        Rs.MoveNext()
        '    Loop
        '    If Microsoft.VisualBasic.Right(labelInfo(UBound(labelInfo)), 2) = ", " Then _
        '        labelInfo(UBound(labelInfo)) = Microsoft.VisualBasic.Left(labelInfo(UBound(labelInfo)),
        '        Len(labelInfo(UBound(labelInfo))) - 2)
        'End If
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
        'Return labelInfo
    End Function

    Private Function GetLabelInfo(ByVal SendoutID As Long) As String()
        Dim labelInfo As New List(Of String)
        Dim Acct As String = ""
        Dim TGPName As String = ""

        Using connection As New SqlConnection(connString)
            connection.Open()

            ' Fetch Lab Account
            Dim queryLab As String = "SELECT Account FROM Labs WHERE ID IN (SELECT Lab_ID FROM Sendouts WHERE ID = @SendoutID)"
            Using cmdLab As New SqlCommand(queryLab, connection)
                cmdLab.Parameters.AddWithValue("@SendoutID", SendoutID)
                Dim result As Object = cmdLab.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then Acct = Trim(result.ToString())
            End Using

            ' Fetch Company Info
            Dim queryCompany As String = "SELECT LastName_BSN, FirstName, Degree, IsIndividual FROM Company WHERE ID = 1"
            Using cmdCompany As New SqlCommand(queryCompany, connection)
                Using reader As SqlDataReader = cmdCompany.ExecuteReader()
                    If reader.Read() Then
                        Dim lastName As String = Trim(reader("LastName_BSN").ToString())
                        Dim firstName As String = Trim(reader("FirstName").ToString())
                        Dim degree As String = If(IsDBNull(reader("Degree")), "MD", Trim(reader("Degree").ToString()))
                        Dim isIndividual As Boolean = Convert.ToBoolean(reader("IsIndividual"))

                        labelInfo.Add(If(Acct <> "",
                        $"Client: {Acct}-{lastName}",
                        $"Client: {lastName}"))

                        If isIndividual Then
                            labelInfo(labelInfo.Count - 1) &= $", {firstName} {degree}"
                        End If
                    End If
                End Using
            End Using

            ' Fetch Accession ID
            Dim queryAccession As String = "SELECT Accession_ID FROM Sendouts WHERE ID = @SendoutID"
            Using cmdAccession As New SqlCommand(queryAccession, connection)
                cmdAccession.Parameters.AddWithValue("@SendoutID", SendoutID)
                Dim result As Object = cmdAccession.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then labelInfo.Add($"Req ID: {result}")
            End Using

            ' Fetch Patient Info
            Dim queryPatient As String = "
            SELECT LastName, FirstName FROM Patients 
            WHERE ID IN (
                SELECT Patient_ID FROM Requisitions WHERE ID IN (
                    SELECT Accession_ID FROM Sendouts WHERE ID = @SendoutID
                )
            )"
            Using cmdPatient As New SqlCommand(queryPatient, connection)
                cmdPatient.Parameters.AddWithValue("@SendoutID", SendoutID)
                Using reader As SqlDataReader = cmdPatient.ExecuteReader()
                    If reader.Read() Then
                        labelInfo.Add($"Patient: {reader("LastName")}, {reader("FirstName")}")
                    End If
                End Using
            End Using

            ' Fetch Provider Info
            Dim queryProvider As String = "
            SELECT LastName_BSN, FirstName, Degree FROM Providers 
            WHERE ID IN (
                SELECT AttendingProvider_ID FROM Requisitions WHERE ID IN (
                    SELECT Accession_ID FROM Sendouts WHERE ID = @SendoutID
                )
            )"
            Using cmdProvider As New SqlCommand(queryProvider, connection)
                cmdProvider.Parameters.AddWithValue("@SendoutID", SendoutID)
                Using reader As SqlDataReader = cmdProvider.ExecuteReader()
                    If reader.Read() Then
                        Dim lastName As String = Trim(reader("LastName_BSN").ToString())
                        Dim firstName As String = Trim(reader("FirstName").ToString())
                        Dim degree As String = If(IsDBNull(reader("Degree")), "MD", Trim(reader("Degree").ToString()))
                        labelInfo.Add($"Provider: {lastName}, {firstName} {degree}")
                    End If
                End Using
            End Using

            ' Fetch Orders
            Dim queryOrders As String = "
            SELECT LabComponentID, TGP_ID FROM Lab_TGP 
            WHERE TGP_ID IN (
                SELECT TGP_ID FROM Sendout_TGP WHERE Sendout_ID = @SendoutID
            )"
            Using cmdOrders As New SqlCommand(queryOrders, connection)
                cmdOrders.Parameters.AddWithValue("@SendoutID", SendoutID)
                Using reader As SqlDataReader = cmdOrders.ExecuteReader()
                    Dim orders As New List(Of String)
                    While reader.Read()
                        TGPName = GetTGPShortName(Convert.ToInt32(reader("TGP_ID")))
                        Dim labComponentID As String = If(IsDBNull(reader("LabComponentID")), "", Trim(reader("LabComponentID").ToString()))
                        orders.Add(If(labComponentID <> "", $"{labComponentID}{TGPName}", TGPName))
                    End While
                    If orders.Count > 0 Then labelInfo.Add($"Orders: {String.Join(", ", orders)}")
                End Using
            End Using
        End Using

        Return labelInfo.ToArray()
    End Function


    Private Sub txtAccFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccFrom.Validated
        Update_Progress()
    End Sub

    Private Sub txtAccTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccTo.Validated
        Update_Progress()
    End Sub

    Private Sub txtDateFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDateFrom.Validated
        If UserEnteredText(txtDateFrom) <> "" Then
            If Not IsDate(txtDateFrom.Text) Then
                MsgBox("Not a valid date", MsgBoxStyle.Critical, "Prolis")
                txtDateFrom.Text = ""
                txtDateFrom.Focus()
            Else
                Update_Progress()
            End If
        End If
    End Sub

    Private Sub txtDateTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDateTo.Validated
        If UserEnteredText(txtDateTo) <> "" Then
            If Not IsDate(txtDateTo.Text) Then
                MsgBox("Not a valid date", MsgBoxStyle.Critical, "Prolis")
                txtDateTo.Text = ""
                txtDateTo.Focus()
            Else
                Update_Progress()
            End If
        End If
    End Sub

    Private Sub chkPT_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPT.CheckedChanged
        If chkPT.Checked = False Then   'Default
            chkPT.Text = "Print"
            chkOrderLabel.Enabled = True
            txtQty.Enabled = True
        Else
            chkPT.Text = "eTransfer"
            chkOrderLabel.Checked = False
            chkOrderLabel.Enabled = False
            txtQty.Enabled = False
        End If
        LoadLabs()
    End Sub

    Private Sub LoadLabs0()
        'cmbRefLab.Items.Clear()
        'cmbRefLab.Text = ""
        'Dim CNP As New ADODB.Connection
        'CNP.Open(connstring)
        'Dim Rs As New ADODB.Recordset
        'If chkPT.Checked = False Then   'all labs
        '    Rs.Open("Select * from Labs where Active <> 0", CNP, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'Else
        '    Rs.Open("Select * from Labs where Active <> 0 and ID in (Select Facility_ID from " &
        '    "External_Interfaces where FacilityType_ID = 4 and IsActive <> 0)", CNP,
        '    ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        'End If
        'If Not Rs.BOF Then
        '    cmbRefLab.Items.Add("")
        '    Do Until Rs.EOF
        '        cmbRefLab.Items.Add(New MyList(Rs.Fields("LabName").Value, Rs.Fields("ID").Value))
        '        Rs.MoveNext()
        '    Loop
        'End If
        'Rs.Close()
        'Rs = Nothing
        'CNP.Close()
        'CNP = Nothing
    End Sub

    Private Sub LoadLabs()
        cmbRefLab.Items.Clear()
        cmbRefLab.Text = ""

        Using connection As New SqlConnection(connString)
            connection.Open()

            Dim query As String
            If chkPT.Checked = False Then ' All labs
                query = "SELECT ID, LabName FROM Labs WHERE Active <> 0"
            Else ' Filtered labs
                query = "
                SELECT ID, LabName FROM Labs 
                WHERE Active <> 0 
                AND ID IN (
                    SELECT Facility_ID FROM External_Interfaces 
                    WHERE FacilityType_ID = 4 AND IsActive <> 0
                )"
            End If

            Using command As New SqlCommand(query, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    cmbRefLab.Items.Add("") ' Adding an empty default item
                    While reader.Read()
                        cmbRefLab.Items.Add(New MyList(reader("LabName").ToString(), reader("ID")))
                    End While
                End Using
            End Using
        End Using
    End Sub


    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        dgvDiscrete.Rows.Clear()
        dgvDiscrete.Rows.Add()
    End Sub

    Private Sub cmbRefLab_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbRefLab.Click
        Update_Progress()
    End Sub

    Private Sub txtQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
        Numerals(sender, e)
    End Sub

    Private Sub txtQty_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.Validated
        If Val(txtQty.Text) = 0 Then txtQty.Text = "1"
    End Sub
End Class
