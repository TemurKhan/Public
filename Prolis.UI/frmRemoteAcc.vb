Imports System.IO
Imports Microsoft.Data.SqlClient
Imports Syncfusion.Pdf

Public Class frmRemoteAcc
    'TODO : Dymo Code
    'Public DymoAddIn As DYMO.DymoAddIn
    'Public DymoLabel As DYMO.DymoLabels

    Public Shared pagesize As Integer = 20000
    Private Sub txtAccession_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Numerals(sender, e)
    End Sub

    Private Sub frmRemoteAcc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CN.Open("DSN=ProlisQC;UID=sa;PWD=''")
        '  RecAll.Hide()
        ' RecAll.Text = ChrW(8595) + " " + RecAll.Text
        dtpAccDate.CustomFormat = Format(Date.Today, SystemConfig.DateFormat)
        cmbTerm.SelectedIndex = 2
        cmbEntry.SelectedIndex = 0
        PopulateProviders()
        PopulateRoutes()
        'PopulateAccessions()
        cmbLabelReq.SelectedIndex = 0

        Dim values = CommonData.ExecuteQuery("select * from Users where UserName ='" & ThisUser.UserName.Trim() & "'")
        For Each v In values
            Try
                Dim UseRemotePrinter = v("UseRemotePrinter")
                If UseRemotePrinter IsNot DBNull.Value Then

                    Dim result = False
                    If UseRemotePrinter Then
                        result = True
                    End If
                    ThisUser.UseRemotePrinter = result
                Else
                    ThisUser.UseRemotePrinter = False
                End If
            Catch ex As Exception
                ThisUser.UseRemotePrinter = False
            End Try

        Next
        Dim Printer As String = GetLabelPrinterName()
        If Not ThisUser.SpecificPrinter = "Default" Then
            Printer = ThisUser.SpecificPrinter
        End If
        ChkRemotePrint.Checked = (ThisUser.UseRemotePrinter And Printer.ToLower().Contains("remote"))
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub PopulateProviders()
        cmbProvider.Items.Clear()
        Dim sSQL As String = ""
        If chkReqOrd.Checked = False Then
            sSQL = "Select * from Providers where ID in (Select distinct OrderingProvider_ID " &
            "from Requisitions where Received = 0 and LabDraw = 0) order by LastName_BSN"
        Else
            sSQL = "Select * from Providers where ID in (Select distinct OrderingProvider_ID " &
            "from Requisitions where Received = 0 and LabDraw <> 0) order by LastName_BSN"
        End If
        Dim cnsp As New SqlConnection(connString)
        cnsp.Open()
        Dim cmdsp As New SqlCommand(sSQL, cnsp)
        cmdsp.CommandType = CommandType.Text
        Dim drsp As SqlDataReader = cmdsp.ExecuteReader
        If drsp.HasRows Then
            While drsp.Read
                If drsp("IsIndividual") IsNot DBNull.Value AndAlso drsp("IsIndividual") = 0 Then
                    cmbProvider.Items.Add(New MyList(drsp("LastName_BSN") &
                    " [" & drsp("ID").ToString & "]", drsp("ID")))
                Else
                    If drsp("Degree") Is DBNull.Value Then
                        cmbProvider.Items.Add(New MyList(drsp("LastName_BSN") &
                        ", " & drsp("FirstName") & " [" &
                        drsp("ID").ToString & "]", drsp("ID")))
                    Else
                        cmbProvider.Items.Add(New MyList(drsp("LastName_BSN") &
                        ", " & drsp("FirstName") & " " & drsp("Degree") _
                        & " [" & drsp("ID").ToString & "]", drsp("ID")))
                    End If
                End If
            End While
        End If
        cnsp.Close()
        cnsp = Nothing
    End Sub

    Private Sub PopulateRoutes()
        cmbRoute.Items.Clear()
        Dim cnsp As New SqlConnection(connString)
        cnsp.Open()
        Dim cmdsp As New SqlCommand("Select * " &
        "from Routes where ID <> 0 order by Name", cnsp)
        cmdsp.CommandType = CommandType.Text
        Dim drsp As SqlDataReader = cmdsp.ExecuteReader
        If drsp.HasRows Then
            While drsp.Read
                cmbRoute.Items.Add(New MyList(drsp("Name"), drsp("ID")))
            End While
        End If
        cnsp.Close()
        cnsp = Nothing
    End Sub
    Private Function InitialRec() As String
        dgvAccessions.Rows.Clear()
        Dim EntryPoint As String = ""
        '0=Inlab Random
        '1=OR Random
        '2=OR Infinite
        '3=EMR Random
        '4=OR Timed
        '5=InLab Infinite
        '6=Inlab Timed
        If cmbEntry.SelectedIndex = 1 Then  'EMR Random
            EntryPoint = "3"
        ElseIf cmbEntry.SelectedIndex = 2 Then  'In Lab Infinite
            EntryPoint = "5"
        ElseIf cmbEntry.SelectedIndex = 3 Then  'In Lab Timed
            EntryPoint = "6"
        ElseIf cmbEntry.SelectedIndex = 4 Then  'In Lab Un-Received
            EntryPoint = "0"
        ElseIf cmbEntry.SelectedIndex = 5 Then  'Outreach Infinite
            EntryPoint = "2"
        ElseIf cmbEntry.SelectedIndex = 6 Then  'Outreach Random
            EntryPoint = "1"
        ElseIf cmbEntry.SelectedIndex = 7 Then  'Outreach Timed
            EntryPoint = "4"
        Else
            EntryPoint = ""
        End If
        Dim ItemX As MyList
        Dim sSQL As String = ""
        If EntryPoint <> "" Then
            sSQL = "Select count(*) over() as total, a.ID as ID, a.RequisitionNo as ReqID, a.AccessionDate as AccDate, " &
            "b.ID as PrID, b.LastName_BSN as PrLName, b.FirstName as PrFName, b.Degree as " &
            "Degree, b.IsIndividual as IsIndividual, b.Address_ID as Address_ID, c.LastName as " &
            "PLName, c.FirstName as PFName, c.DOB as DOB, c.Sex as Sex, a.EMRNo as MRN from " &
            "Providers b inner join (Requisitions a left outer join Patients c on c.ID = " &
            "a.Patient_ID) on b.ID = a.OrderingProvider_ID where (a.Received is NULL or " &
            "a.Received = 0) and a.AccessionLoc_ID = " & Val(EntryPoint)
        Else
            sSQL = "Select count(*) over() as total, a.ID as ID, a.RequisitionNo as ReqID, a.AccessionDate as AccDate, " &
            "b.ID as PrID, b.LastName_BSN as PrLName, b.FirstName as PrFName, b.Degree as " &
            "Degree, b.IsIndividual as IsIndividual, b.Address_ID as Address_ID, c.LastName as " &
            "PLName, c.FirstName as PFName, c.DOB as DOB, c.Sex as Sex, a.EMRNo as MRN from " &
            "Providers b inner join (Requisitions a left outer join Patients c on c.ID = " &
            "a.Patient_ID) on b.ID = a.OrderingProvider_ID where (a.Received is NULL or " &
            "a.Received = 0)"
        End If
        If IsDate(dtpAccDate.Text) Then
            sSQL += " and a.AccessionDate < '" & Format(CDate(dtpAccDate.Text & " 23:59"), "MM/dd/yyyy HH:mm") & "'"
        Else
            sSQL += " and a.AccessionDate < '" & Date.Now & "'"
        End If
        '
        If cmbTerm.SelectedIndex = 3 Then
            sSQL += " and a.Patient_ID = " & Trim(txtPatientID.Text) & ""
        End If
        If Trim(txtTerm.Text) <> "" Then
            If cmbTerm.SelectedIndex = 0 Then   'Accession
                If IsNumeric(Trim(txtTerm.Text)) Then _
                sSQL += " and a.ID = " & Trim(txtTerm.Text)
            ElseIf cmbTerm.SelectedIndex = 1 Then   'MRN
                sSQL += " and a.EMRNo = '" & Trim(txtTerm.Text) & "'"
            Else    'Requisition
                sSQL += " and a.RequisitionNo = '" & Trim(txtTerm.Text) & "'"
            End If
        End If
        '
        If cmbRoute.SelectedIndex <> -1 Then
            ItemX = cmbRoute.SelectedItem
            sSQL = sSQL & " and b.Route_ID = " & ItemX.ItemData
        End If
        If cmbProvider.SelectedIndex <> -1 Then
            ItemX = cmbProvider.SelectedItem
            sSQL = sSQL & " and b.ID = " & ItemX.ItemData
        End If
        '

        Dim Provider As String = ""
        Dim Patient As String = ""
        Dim ReqID As String = ""
        Dim MRN As String = ""
        Dim CNT As Integer = 0
        '
        Dim cnsp As New SqlConnection(connString)
        cnsp.Open()
        Dim cmdsp As New SqlCommand(sSQL, cnsp)
        cmdsp.CommandType = CommandType.Text
        Dim drsp As SqlDataReader = cmdsp.ExecuteReader
        If drsp.HasRows Then
            While drsp.Read
                txtAccCount.Text = drsp("total").ToString

                txtAccPageCount.Text = Convert.ToInt32(Math.Ceiling(Convert.ToInt32(txtAccCount.Text) / pagesize)).ToString
                txtAccPageCount.Text = Math.Max(Convert.ToInt32(txtAccPageCount.Text), 1)
                Exit While

            End While
        End If
        cnsp.Close()
        cnsp = Nothing

        If chkReqOrd.Checked = True And dgvAccessions.RowCount > 0 Then
            btnReport.Enabled = True
        Else
            btnReport.Enabled = False
        End If
        Return ""
    End Function

    Private Sub PopulateAccessions(Optional skp As String = "", Optional take As String = "")
        dgvAccessions.Rows.Clear()
        Dim EntryPoint As String = ""
        '0=Inlab Random
        '1=OR Random
        '2=OR Infinite
        '3=EMR Random
        '4=OR Timed
        '5=InLab Infinite
        '6=Inlab Timed
        If cmbEntry.SelectedIndex = 1 Then  'EMR Random
            EntryPoint = "3"
        ElseIf cmbEntry.SelectedIndex = 2 Then  'In Lab Infinite
            EntryPoint = "5"
        ElseIf cmbEntry.SelectedIndex = 3 Then  'In Lab Timed
            EntryPoint = "6"
        ElseIf cmbEntry.SelectedIndex = 4 Then  'In Lab Un-Received
            EntryPoint = "0"
        ElseIf cmbEntry.SelectedIndex = 5 Then  'Outreach Infinite
            EntryPoint = "2"
        ElseIf cmbEntry.SelectedIndex = 6 Then  'Outreach Random
            EntryPoint = "1"
        ElseIf cmbEntry.SelectedIndex = 7 Then  'Outreach Timed
            EntryPoint = "4"
        Else
            EntryPoint = ""
        End If
        Dim ItemX As MyList
        Dim sSQL As String = ""
        If EntryPoint <> "" Then
            sSQL = "Select count(*) over() as total, a.ID as ID, a.RequisitionNo as ReqID, a.AccessionDate as AccDate, " &
            "b.ID as PrID, b.LastName_BSN as PrLName, b.FirstName as PrFName, b.Degree as " &
            "Degree, b.IsIndividual as IsIndividual, b.Address_ID as Address_ID, c.LastName as " &
            "PLName, c.FirstName as PFName, c.DOB as DOB, c.Sex as Sex, a.EMRNo as MRN from " &
            "Providers b inner join (Requisitions a left outer join Patients c on c.ID = " &
            "a.Patient_ID) on b.ID = a.OrderingProvider_ID where (a.Received is NULL or " &
            "a.Received = 0) and a.AccessionLoc_ID = " & Val(EntryPoint)
        Else
            sSQL = "Select count(*) over() as total, a.ID as ID, a.RequisitionNo as ReqID, a.AccessionDate as AccDate, " &
            "b.ID as PrID, b.LastName_BSN as PrLName, b.FirstName as PrFName, b.Degree as " &
            "Degree, b.IsIndividual as IsIndividual, b.Address_ID as Address_ID, c.LastName as " &
            "PLName, c.FirstName as PFName, c.DOB as DOB, c.Sex as Sex, a.EMRNo as MRN from " &
            "Providers b inner join (Requisitions a left outer join Patients c on c.ID = " &
            "a.Patient_ID) on b.ID = a.OrderingProvider_ID where (a.Received is NULL or " &
            "a.Received = 0)"
        End If
        If IsDate(dtpAccDate.Text) Then
            sSQL += " and a.AccessionDate < '" & Format(CDate(dtpAccDate.Text & " 23:59"), "MM/dd/yyyy HH:mm") & "'"
        Else
            sSQL += " and a.AccessionDate < '" & Date.Now & "'"
        End If
        '
        If cmbTerm.SelectedIndex = 3 Then
            sSQL += " and a.Patient_ID = " & Trim(txtPatientID.Text) & ""
        End If


        If Trim(txtTerm.Text) <> "" Then
            If cmbTerm.SelectedIndex = 0 Then   'Accession
                If txtTerm.Text.Contains(",") Then
                    Dim values As String = txtTerm.Text.TrimEnd(",")
                    Dim valueArray As String() = values.Split(","c)

                    ' Assigning the array to a variable
                    Dim arrayOfValues As String() = valueArray
                    values = ""
                    ' Display each value in single quotes
                    For Each value As String In arrayOfValues
                        values += ("'" & value & "'")
                    Next
                    sSQL += " and a.ID  in (" & (values) & ")"
                Else
                    If IsNumeric(Trim(txtTerm.Text)) Then

                        sSQL += " and a.ID = " & Trim(txtTerm.Text)
                    End If
                End If


            ElseIf cmbTerm.SelectedIndex = 1 Then   'MRN
                sSQL += " and a.EMRNo = '" & Trim(txtTerm.Text) & "'"
            ElseIf cmbTerm.SelectedIndex = 3 Then
                sSQL += " and a.Patient_ID = " & Trim(txtPatientID.Text) & ""
            Else    'Requisition
                If txtTerm.Text.Contains(",") Then
                    Dim values As String = txtTerm.Text.TrimEnd(",")
                    Dim valueArray As String() = values.Split(","c)

                    ' Assigning the array to a variable
                    Dim arrayOfValues As String() = valueArray
                    values = ""
                    ' Display each value in single quotes
                    For Each value As String In arrayOfValues
                        values += ("'" & value & "'") & ","
                    Next
                    sSQL += " and a.RequisitionNo in (" & values.TrimEnd(",") & ")"
                Else
                    sSQL += " and a.RequisitionNo = '" & Trim(txtTerm.Text) & "'"
                End If

            End If
        End If
        '
        If cmbRoute.SelectedIndex <> -1 Then
            ItemX = cmbRoute.SelectedItem
            sSQL = sSQL & " and b.Route_ID = " & ItemX.ItemData
        End If
        If cmbProvider.SelectedIndex <> -1 Then
            ItemX = cmbProvider.SelectedItem
            sSQL = sSQL & " and b.ID = " & ItemX.ItemData
        End If
        '
        If String.IsNullOrEmpty(skp) <> True AndAlso String.IsNullOrEmpty(take) <> True Then

            sSQL = sSQL & " order by c.LastName, c.FirstName OFFSET " & skp & " rows FETCH NEXT " & take & " rows only;"
        End If
        Dim Provider As String = ""
        Dim Patient As String = ""
        Dim ReqID As String = ""
        Dim MRN As String = ""
        Dim CNT As Integer = 0
        '
        Dim cnsp As New SqlConnection(connString)
        cnsp.Open()
        Dim cmdsp As New SqlCommand(sSQL, cnsp)
        cmdsp.CommandType = CommandType.Text
        Dim drsp As SqlDataReader = cmdsp.ExecuteReader
        If drsp.HasRows Then
            While drsp.Read
                txtAccCount.Text = drsp("total").ToString
                txtAccPageCount.Text = Convert.ToInt32(Math.Ceiling(Convert.ToInt32(txtAccCount.Text)) / pagesize).ToString
                If drsp("IsIndividual") IsNot DBNull.Value AndAlso drsp("IsIndividual") = 0 Then
                    Provider = drsp("PrLName") & " [" & drsp("PrID").ToString & "]"
                Else
                    If drsp("Degree") Is DBNull.Value Then
                        Provider = drsp("PrLName") & ", " &
                        drsp("PrFName") & " [" & drsp("PrID").ToString & "]"
                    Else
                        Provider = drsp("PrLName") & ", " &
                        drsp("PrFName") & " " & drsp("Degree") _
                        & " [" & drsp("PrID").ToString & "]"
                    End If
                End If
                If drsp("Address_ID") IsNot DBNull.Value Then
                    Provider += " - " & GetAddress(drsp("Address_ID"))
                End If
                If drsp("PLName") IsNot DBNull.Value Then
                    Patient = drsp("PLName") & ", " & drsp("PFName") &
                    " - " & Format(drsp("DOB"), SystemConfig.DateFormat) & " - " & drsp("Sex")
                End If
                If drsp("ReqID") IsNot DBNull.Value _
                AndAlso Trim(drsp("ReqID")) <> "" Then
                    ReqID = Trim(drsp("ReqID"))
                Else
                    ReqID = ""
                End If
                If drsp("MRN") IsNot DBNull.Value _
                AndAlso Trim(drsp("MRN")) <> "" Then
                    MRN = Trim(drsp("MRN"))
                Else
                    MRN = ""
                End If
                dgvAccessions.Rows.Add(drsp("ID").ToString, ReqID,
                Format(drsp("AccDate"), SystemConfig.DateFormat), Patient, MRN, Provider)
                CNT += 1
            End While
        End If
        cnsp.Close()
        cnsp = Nothing
        txtAccCountOnWindow.Text = CNT
        If chkReqOrd.Checked = True And dgvAccessions.RowCount > 0 Then
            btnReport.Enabled = True
        Else
            btnReport.Enabled = False
        End If
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        dgvSources.Rows.Clear()
        Dim crnt = Convert.ToInt32(pagtxt.Text)
        If crnt <= 0 Then
            Return
        End If
        Dim page = (crnt - 1) * pagesize
        If txtAccPageCount.Text = "jjj" Then
            InitialRec()
            PopulateAccessions(page.ToString, pagesize)
            Return
        End If
        PopulateAccessions(page.ToString, pagesize)
    End Sub

    Private Sub btnReceive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReceive.Click
        Dim count As Integer = 0
        Dim req = ""
        Dim Accidss = ""
        For r As Integer = 0 To dgvAccessions.Rows.Count - 1
            If dgvAccessions.Rows(r).Cells(8).Value Then
                If BupliCate(dgvAccessions.Rows(r).Cells(1).Value) Then
                    dgvAccessions.Rows(r).Cells(8).Value = False
                    req += "," & dgvAccessions.Rows(r).Cells(1).Value
                    Accidss += "," & dgvAccessions.Rows(r).Cells(0).Value
                Else
                End If

            End If


        Next
        If Not req = "" Then
            MessageBox.Show("Some Requisition Number(s) has copied to clipboard as they are Duplicate in the system. You can paste it in some text file to further search.", "Info")
        End If
        For r As Integer = 0 To dgvAccessions.Rows.Count - 1
            If dgvAccessions.Rows(r).Cells(8).Value Then



                count += 1
            End If

        Next
        If count > 1 Then
            Dim result As DialogResult = MessageBox.Show("You have selected to receive (" & count & ") samples. Do you wish to continue? If you have selected these by mistake please click cancel. Or click OK to continue.",
                              "Alert",
                              MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
            If result = DialogResult.Cancel Then
                Return
            End If
        End If
        If dgvSources.Rows.Count <= 0 Then
            populatDataIntoRec()
        End If

        Dim correctDate = True
        Dim rcDate As Date = DateTime.Now


        For i As Integer = 0 To dgvSources.Rows.Count - 1
            Dim drDate = dgvSources.Rows(i).Cells(4).Value & " " & dgvSources.Rows(i).Cells(5).Value

            If rcDate < CDate(drDate) Then
                MessageBox.Show("For your information Receive Date cannot be before  Drawn Date", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                'correctDate = False --- Removed for starLab
            End If
        Next
        Dim ids = ""

        If dgvSources.RowCount > 0 And correctDate Then

            For i As Integer = 0 To dgvSources.RowCount - 1
                Dim accid = Split(dgvSources.Rows(i).Cells(1).Value, "-")(0)
                If Accidss.Contains(accid) Then
                    Continue For
                End If
                Dim UnreceivedSps As String = ""
                Dim ReceivedSps As String = ""
                If dgvSources.Rows(i).Cells(7).Value = 0 Then
                    ExecuteSqlProcedure("Delete from Specimens where Accession_ID = " &
                    accid & " and Source_ID = " &
                    dgvSources.Rows(i).Cells(0).Value)
                    UnreceivedSps += dgvSources.Rows(i).Cells(3).Value & " " &
                    dgvSources.Rows(i).Cells(2).Value & ", Received = No|"
                Else
                    ExecuteSqlProcedure("If Exists (Select * from Specimens where Accession_ID = " &
                    accid & " and Source_ID = " &
                    dgvSources.Rows(i).Cells(0).Value & ") Update Specimens set SourceNo = '" &
                    dgvSources.Rows(i).Cells(1).Value & "', SourceQuantity = " &
                    dgvSources.Rows(i).Cells(3).Value & ", SourceDate = '" &
                    dgvSources.Rows(i).Cells(4).Value & " " & dgvSources.Rows(i).Cells(5).Value &
                    "', SourceTemp = '" & dgvSources.Rows(i).Cells(6).Value & "', Comment = '' " &
                    "where Accession_ID = " & accid &
                    " and Source_ID = " & dgvSources.Rows(i).Cells(0).Value & " Else Insert into " &
                    "Specimens (Accession_ID, Source_ID, SourceNo, SourceQuantity, SourceDate, " &
                    "SourceTemp, Comment) values (" & accid &
                    ", " & dgvSources.Rows(i).Cells(0).Value & ", '" & dgvSources.Rows(i).Cells(1).Value &
                    "', " & dgvSources.Rows(i).Cells(3).Value & ", '" & dgvSources.Rows(i).Cells(4).Value _
                    & " " & dgvSources.Rows(i).Cells(5).Value & "', '" & dgvSources.Rows(i).Cells(6).Value _
                    & "', '')")
                    '
                    ReceivedSps += dgvSources.Rows(i).Cells(3).Value & " " &
                    dgvSources.Rows(i).Cells(2).Value & ", Received = Yes|"
                End If

                If UnreceivedSps.EndsWith("|") Then UnreceivedSps = UnreceivedSps.Substring(0, Len(UnreceivedSps) - 1)
                If ReceivedSps.EndsWith("|") Then ReceivedSps = ReceivedSps.Substring(0, Len(ReceivedSps) - 1)
                LogUserEvent(ThisUser.ID, 2, Date.Now.ToString, "Accession",
                accid, UnreceivedSps, ReceivedSps)


                If i = 0 Then
                    ids = accid
                Else
                    If Not ids.Contains(accid) Then
                        ids += "," + accid
                    End If

                End If

            Next

            '
            dgvSources.Rows.Clear()
            ExecuteSqlProcedure("Update Requisitions set Received = 1, ReceivedTime = '" &
            Format(Date.Now, "MM/dd/yyyy HH:mm:ss") & "', Comment = '" & txtComment.Text _
            & "' where ID in " & "(" & ids & ")")
            btnDelete.Enabled = False
            dgvSources.Rows.Clear()
            Dim crnt = Convert.ToInt32(pagtxt.Text)
            If crnt <= 0 Then
                Return
            End If
            Dim page = (crnt - 1) * pagesize
            If txtAccPageCount.Text = "jjj" Then
                InitialRec()
                PopulateAccessions(page.ToString, pagesize)
                Return
            End If
            PopulateAccessions(page.ToString, pagesize)
        End If
    End Sub
    Private Sub populatDataIntoRec()
        Dim anyselected = False
        Dim req1 = ""
        If dgvAccessions.RowCount > 0 Then
            btnDelete.Enabled = False
            dgvSources.Rows.Clear()
            Dim ff = dgvAccessions.Rows(0).Cells(8).Value
            Dim ids = ""
            For r As Integer = 0 To dgvAccessions.Rows.Count - 1
                If dgvAccessions.Rows(r).Cells(8).Value = True Then
                    anyselected = True
                    Exit For
                End If
            Next
            For r As Integer = 0 To dgvAccessions.Rows.Count - 1
                If anyselected = False Then
                    MessageBox.Show("Please select an accession")
                    Exit For
                End If
                If r >= dgvAccessions.Rows.Count Then
                    Exit For
                End If
                If dgvAccessions.Rows(r).Cells(8).Value = False Then
                    Continue For

                End If


                If dgvAccessions.Rows(r).Cells(8).Value Then
                    If BupliCate(dgvAccessions.Rows(r).Cells(1).Value) Then
                        dgvAccessions.Rows(r).Cells(8).Value = False
                        req1 += "," & dgvAccessions.Rows(r).Cells(1).Value
                        Continue For
                    Else
                    End If

                End If
                Dim cnsp As New SqlConnection(connString)
                cnsp.Open()
                Dim cmdsp As New SqlCommand("Select * from Specimens where " &
           "Accession_ID = " & dgvAccessions.Rows(r).Cells(0).Value, cnsp)
                cmdsp.CommandType = CommandType.Text
                Dim drsp As SqlDataReader = cmdsp.ExecuteReader
                If drsp.HasRows Then
                    While drsp.Read
                        Try
                            dgvSources.Rows.Add(drsp("Source_ID"), drsp("SourceNo"),
GetSourceName(drsp("Source_ID")), drsp("SourceQuantity"),
Format(drsp("SourceDate"), SystemConfig.DateFormat),
Format(drsp("SourceDate"), "HH:mm"), drsp("SourceTemp"), True)
                        Catch ex As Exception

                        End Try

                    End While
                End If
                cnsp.Close()
                cnsp = Nothing
                btnDelete.Enabled = True
            Next
            If Not req1 = "" Then
                MessageBox.Show("Some Requisition Number(s) has copied to clipboard as they are Duplicate in the system. You can paste it in some text file to further search.", "Info")
                Clipboard.SetText(req1)
            End If

        End If
    End Sub
    Private Sub chkReqOrd_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkReqOrd.CheckedChanged
        If chkReqOrd.Checked = False Then
            chkReqOrd.Text = "Accession"
            chkReqOrd.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Req.ico")
        Else
            chkReqOrd.Text = "Instance"
            chkReqOrd.Image = System.Drawing.Image.FromFile(Application.StartupPath & "\Images\Ord.ico")
        End If
        btnReport.Enabled = False
    End Sub

    Private Sub dgvAccessions_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAccessions.CellDoubleClick
        For r As Integer = 0 To dgvAccessions.Rows.Count - 1
            dgvAccessions.Rows(r).Cells(8).Value = False
        Next
        dgvAccessions.Rows(e.RowIndex).Cells(0).Style.ForeColor = Color.Green
        dgvSources.Rows.Clear()
        If e.RowIndex <> -1 Then
            btnDelete.Enabled = False
            dgvSources.Rows.Clear()
            If BupliCate(dgvAccessions.Rows(e.RowIndex).Cells(1).Value) Then
                CustomMessageBox.ShowMe("The Requisition Number (  " & dgvAccessions.Rows(e.RowIndex).Cells(1).Value & "  ) with related info Already Exists in System. Click YES to see duplicates in Accession Dashboard.", "Info")
                If CustomMessageBox.reply = "OK" Then
                    Dim reqN = dgvAccessions.Rows(e.RowIndex).Cells(1).Value
                    If Not String.IsNullOrEmpty(reqN.ToString()) Then
                        frmAccDash.txtTerm.Text = reqN
                        frmAccDash.cmbCriteria.SelectedIndex = 11
                        frmAccDash.cmbStatus.SelectedIndex = 2
                        frmAccDash.Show()
                        frmAccDash.MdiParent = frmDashboard
                    End If


                End If
                'MessageBox.Show("The Requisition Number (  " & dgvAccessions.Rows(e.RowIndex).Cells(1).Value & "  ) with related info Already Exists in System. You 'can check from Accession Dashboard.", "Info")
                Return
            Else
            End If
            Dim cnsp As New SqlConnection(connString)
            cnsp.Open()
            Dim cmdsp As New SqlCommand("Select * from Specimens where " &
            "Accession_ID = " & dgvAccessions.Rows(e.RowIndex).Cells(0).Value, cnsp)
            cmdsp.CommandType = CommandType.Text
            Dim drsp As SqlDataReader = cmdsp.ExecuteReader
            If drsp.HasRows Then
                While drsp.Read
                    dgvSources.Rows.Add(drsp("Source_ID"), drsp("SourceNo"),
                    GetSourceName(drsp("Source_ID")), drsp("SourceQuantity"),
                    Format(drsp("SourceDate"), SystemConfig.DateFormat),
                    Format(drsp("SourceDate"), "HH:mm"), drsp("SourceTemp"), True)
                End While
            Else
                MessageBox.Show("Specimen is missing for this accession.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            cnsp.Close()
            cnsp = Nothing
            btnDelete.Enabled = True
        End If

    End Sub

    Private Function GetSourceName(ByVal ID As Integer) As String
        Dim SName As String = ""
        Dim cnsp As New SqlConnection(connString)
        cnsp.Open()
        Dim cmdsp As New SqlCommand("Select * from Sources where ID = " & ID, cnsp)
        cmdsp.CommandType = CommandType.Text
        Dim drsp As SqlDataReader = cmdsp.ExecuteReader
        If drsp.HasRows Then
            While drsp.Read
                SName = Trim(drsp("Name"))
            End While
        End If
        cnsp.Close()
        cnsp = Nothing
        Return SName
    End Function

    Private Sub btnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click
        Dim UID As String = My.Settings.UID.ToString
        Dim PWD As String = My.Settings.PWD.ToString

        'TODO: Crystal Reports
        '==================================
        'Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        'oRpt.Load(Application.StartupPath & "\Reports\Phlebotomy.rpt")
        'oRpt.SetDatabaseLogon(UID, PWD)
        'frmRV.CRRV.ReportSource = oRpt
        'frmRV.MdiParent = frmDashboard
        'frmRV.Show()
        '==================================
    End Sub


    Private Sub cmbTerm_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTerm.SelectedIndexChanged
        If cmbTerm.SelectedIndex = 3 Then
            searchPanel.Hide()
            txtTerm.Hide()
            patientpanel.Show()
        ElseIf cmbTerm.SelectedIndex <> -1 Then
            lblTerm.Text = cmbTerm.SelectedItem.ToString
            patientpanel.Hide()
            searchPanel.Show()
            txtTerm.Show()

        Else
            patientpanel.Hide()
            searchPanel.Show()
            txtTerm.Show()
            lblTerm.Text = ""
        End If
    End Sub

    Private Sub btnDesel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDesel.Click
        If cmbLabelReq.SelectedIndex = 1 Then   'Label
            For i As Integer = 0 To dgvAccessions.RowCount - 1
                dgvAccessions.Rows(i).Cells(7).Value = False
            Next
        ElseIf cmbLabelReq.SelectedIndex = 3 Then 'Req
            For i As Integer = 0 To dgvAccessions.RowCount - 1
                dgvAccessions.Rows(i).Cells(6).Value = False
            Next
        ElseIf cmbLabelReq.SelectedIndex = 0 Then 'Select
            For i As Integer = 0 To dgvAccessions.RowCount - 1
                dgvAccessions.Rows(i).Cells(8).Value = False
            Next
        End If
    End Sub

    Private Sub btnSel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSel.Click
        If cmbLabelReq.SelectedIndex = 1 Then   'Label
            For i As Integer = 0 To dgvAccessions.RowCount - 1
                dgvAccessions.Rows(i).Cells(7).Value = True
            Next
        ElseIf cmbLabelReq.SelectedIndex = 3 Then 'Re
            For i As Integer = 0 To dgvAccessions.RowCount - 1
                dgvAccessions.Rows(i).Cells(6).Value = True
            Next
        ElseIf cmbLabelReq.SelectedIndex = 0 Then 'Selet
            For i As Integer = 0 To dgvAccessions.RowCount - 1
                dgvAccessions.Rows(i).Cells(8).Value = True
            Next
        End If
    End Sub

    Private Sub dgvAccessions_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAccessions.CellContentClick
        Dim i As Integer
        If cmbLabelReq.SelectedIndex < 2 Then   'Label
            If e.ColumnIndex = 7 Then   'Label
                If dgvAccessions.Rows(e.RowIndex).Cells(7).Value = True Then
                    dgvAccessions.Rows(e.RowIndex).Cells(7).Value = False
                Else
                    dgvAccessions.Rows(e.RowIndex).Cells(7).Value = True
                End If
            End If
            For i = 0 To dgvAccessions.RowCount - 1
                dgvAccessions.Rows(i).Cells(6).Value = False
            Next
        Else    'Req
            If e.ColumnIndex = 6 Then   'Req
                If dgvAccessions.Rows(e.RowIndex).Cells(6).Value = True Then
                    dgvAccessions.Rows(e.RowIndex).Cells(6).Value = False
                Else
                    dgvAccessions.Rows(e.RowIndex).Cells(6).Value = True
                End If
            End If
            For i = 0 To dgvAccessions.RowCount - 1
                dgvAccessions.Rows(i).Cells(7).Value = False
            Next
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If cmbLabelReq.SelectedIndex < 2 Then   'Label
                If LabelsSelected() = True And cmbDestination.SelectedIndex <> -1 Then
                    Me.Enabled = False
                    Dim Printer As String = ""
                    Printer = GetLabelPrinterName()


                    If Not ThisUser.UseRemotePrinter Then
                        'DymoAddIn = New Dymo.DymoAddIn
                        ' Printer = DymoAddIn.GetCurrentPrinterName()
                    Else
                        Printer = "Prolis Remote DYMO"
                    End If
                    If Not ThisUser.SpecificPrinter = "Default" Then
                        Printer = ThisUser.SpecificPrinter
                    End If
                    For i As Integer = 0 To dgvAccessions.RowCount - 1
                        If dgvAccessions.Rows(i).Cells(7).Value = True Then
                            Dim lbls = Convert.ToInt32(countLaberls.Text)

                            PrintLabels(Printer, dgvAccessions.Rows(i).Cells(0).Value, lbls)

                            My.Application.DoEvents()
                        End If
                    Next
                    btnDesel_Click(Nothing, Nothing)
                    Me.Enabled = True
                End If
            Else    'Req
                If AccessionsSelected() = True And cmbDestination.SelectedIndex <> -1 Then
                    Me.Enabled = False
                    Dim Accessions() As String = {""}
                    For i As Integer = 0 To dgvAccessions.RowCount - 1
                        If dgvAccessions.Rows(i).Cells(6).Value = True Then
                            If Accessions(UBound(Accessions)) <> "" Then _
                            ReDim Preserve Accessions(UBound(Accessions) + 1)
                            Accessions(UBound(Accessions)) =
                            dgvAccessions.Rows(i).Cells(0).Value
                        End If
                    Next
                    PrintMultiReports(Accessions, cmbDestination.SelectedIndex)
                    btnDesel_Click(Nothing, Nothing)
                    Me.Enabled = True
                End If
            End If

        Catch Ex As Exception
            MsgBox("Error: '" & Ex.Message & "' occured.", MsgBoxStyle.Critical, "Prolis")
            Me.Enabled = True
        End Try
    End Sub

    Private Sub PrintLabels(ByVal Printer As String, ByVal Accession As String, ByVal Labels As Integer)
        Dim i As Integer
        If Printer = "Brady" Then
            Dim ReqNo = ""
            If String.IsNullOrEmpty(ReqNo) Then
                Dim req = CommonData.RetrieveColumnValue("Requisitions", "RequisitionNo", "ID", Accession, "")
                ReqNo = req.ToString()
            End If
            Dim Initial = ""
            Dim Date1 = ""
            Dim P = ""
            Dim par() As Object = {Accession + "|" + ReqNo.Trim() + "|" + P + "|" + Initial, Labels.ToString()}
            Dim res = Services.InvokeMethod("BradyPrinter.dll", "Automation", "SaveTextFile", par)

            Return
        End If
        Dim lblPath As String = ""
        If SystemConfig.AccLabel <> "" Then
            lblPath = ValidateLabelFile(SystemConfig.AccLabel)
        Else
            If InStr(Printer, "DYMO") > 0 Then
                lblPath = GetReportPath("Dymo30334Tst.Label")
            Else    'Zebra
                lblPath = GetReportPath("ZebraAccTst.rpt")
            End If
        End If
        'TODO: Crystal Reports & Dymo Code  
        '==================================
        'If InStr(Printer, "DYMO") > 0 Then
        '    Dim LabelInfo() As String = GetLabelInfo(Accession, Labels)
        '    Dim RawData As String = ""
        '    If InStr(Printer, "Prolis Remote") > 0 And ThisUser.UseRemotePrinter Then
        '        For i = LBound(LabelInfo) To UBound(LabelInfo)
        '            RawData = LabelInfo(i).ToString
        '            If InStr(Printer, "Prolis Remote") > 0 Then
        '                ExecuteSqlProcedure("insert into LabelPrintJobs(Processed,PrintJob,PrinterPC) values(0,'" & RawData & "','" & ThisUser.PrinterPC & "')")

        '            End If
        '        Next
        '        Return
        '    End If

        '    For i = LBound(LabelInfo) To UBound(LabelInfo)
        '        RawData = LabelInfo(i).ToString
        '        Dim Data() As String = Split(RawData, "|")
        '        DymoAddIn = New DYMO.DymoAddIn
        '        DymoLabel = New DYMO.DymoLabels
        '        'lblPath = "C:\Reports\DymoAcc1x2-18.Label"
        '        If DymoAddIn.Open(lblPath) Then
        '            DymoLabel.SetField("Provider", Data(0))
        '            DymoLabel.SetField("Patient", Data(1))
        '            Try
        '                If lblPath.ToLower().Contains("skippa") Then
        '                    Dim pname = Data(1).Split("-")(0).TrimEnd(",")
        '                    Dim Gender = "" & Data(1).Split("-")(1)
        '                    Dim dob = "" & Data(1).Split("-")(2)
        '                    Dim currentDate As Date = Format(Date.Now, SystemConfig.DateFormat)
        '                    Dim birthdate As Date = dob

        '                    Dim age As Integer = currentDate.Year - birthdate.Year

        '                    DymoLabel.SetField("Patient", pname)
        '                    DymoLabel.SetField("Patient_1", "DOB:" & dob & "    Age:" & age & " " & Gender)
        '                End If
        '            Catch ex As Exception

        '            End Try
        '            DymoLabel.SetField("AccID", Data(2))
        '            DymoLabel.SetField("AccDate", Data(3))
        '            DymoLabel.SetField("Tests", Data(4))
        '            DymoLabel.SetField("EMRNo", Data(5))

        '            DymoAddIn.Print(Val(Data(6)), False)
        '        Else
        '            MsgBox("Dymo Label file can not be opened", MsgBoxStyle.Critical, "Prolis")
        '        End If
        '        DymoAddIn = Nothing
        '        DymoLabel = Nothing
        '    Next
        'Else
        '    Dim UID As String = My.Settings.UID.ToString
        '    Dim PWD As String = My.Settings.PWD.ToString
        '    Dim gReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        '    If Not SystemConfig.AccLabel Is Nothing AndAlso SystemConfig.AccLabel <> "" Then
        '        gReport.Load(lblPath)
        '    Else
        '        If InStr(Printer, "Zebra") > 0 Then
        '            gReport.Load(Application.StartupPath & "\Reports\ZebraLabel.rpt")
        '        Else
        '            gReport.Load(Application.StartupPath & "\Reports\GenLabel.rpt")
        '        End If
        '    End If
        '    Dim pSize As CrystalDecisions.Shared.PaperSize = gReport.PrintOptions.PaperSize
        '    gReport.SetDatabaseLogon(UID, PWD)
        '    gReport.RecordSelectionFormula = "{Requisitions.ID} = " & Val(Accession)
        '    gReport.PrintOptions.PrinterName = Printer
        '    gReport.PrintOptions.PaperSize = pSize
        '    gReport.PrintToPrinter(Labels, False, 0, 0)
        '    gReport.Close()
        '    gReport = Nothing
        'End If
        '==================================

    End Sub

    Private Function GetAccessionSources(ByVal AccID As Long) As String()
        Dim Sources() As String = {""}
        Dim cnsp As New SqlConnection(connString)
        cnsp.Open()
        Dim cmdsp As New SqlCommand("Select a.ID as ID, a.Name as Source, " &
        "b.SourceQuantity as QTY from Sources a inner join Specimens b on a.ID = " &
        "b.Source_ID where b.Accession_ID = " & AccID, cnsp)
        cmdsp.CommandType = CommandType.Text
        Dim drsp As SqlDataReader = cmdsp.ExecuteReader
        If drsp.HasRows Then
            While drsp.Read
                If Sources(UBound(Sources)) <> "" Then ReDim Preserve Sources(UBound(Sources) + 1)
                Sources(UBound(Sources)) = drsp("ID").ToString & "^" &
                Trim(drsp("Source")) & "^" & drsp("QTY")
            End While
        End If
        cnsp.Close()
        cnsp = Nothing
        Return Sources
    End Function

    Private Function GetLabelInfo(ByVal AccID As Long, ByVal QTY As Integer, ByVal connString As String) As String()
        Dim labelInfo() As String = {""} ' Old: Dim labelInfo() As String = {""}
        Dim Sources() As String = {""} ' Old: Dim Sources() As String = {""}
        Dim Provider As String = "" ' Old: Dim Provider As String = ""
        Dim Patient As String = "" ' Old: Dim Patient As String = ""
        Dim Tests As String = "" ' Old: Dim Tests As String = ""
        Dim DOC As String = "" ' Old: Dim DOC As String = ""
        Dim EMRNo As String = "" ' Old: Dim EMRNo As String = ""
        Dim Comps As String = "" ' Old: Dim Comps As String = ""
        Dim i As Integer ' Old: Dim i As Integer

        If cmbLabelReq.SelectedIndex = 1 Then ' Old: If cmbLabelReq.SelectedIndex = 1 Then
            Sources = GetAccessionSources(AccID) ' Old: Sources = GetAccessionSources(AccID)
        End If

        Using connection As New SqlConnection(connString)
            connection.Open()

            ' Query 1: Fetch provider information
            Dim queryProvider As String = "
            SELECT * 
            FROM Providers 
            WHERE ID IN (SELECT OrderingProvider_ID FROM Requisitions WHERE ID = @AccID)"
            Using cmdProvider As New SqlCommand(queryProvider, connection)
                cmdProvider.Parameters.AddWithValue("@AccID", AccID)

                Using readerProvider As SqlDataReader = cmdProvider.ExecuteReader()
                    If readerProvider.HasRows Then
                        While readerProvider.Read()
                            If Not Convert.ToBoolean(readerProvider("IsIndividual")) Then
                                If readerProvider("LastName_BSN") IsNot DBNull.Value Then
                                    Provider = readerProvider("LastName_BSN").ToString().Trim()
                                End If
                            Else
                                Provider = readerProvider("LastName_BSN").ToString().Trim() & ", " &
                                       readerProvider("FirstName").ToString().Trim() &
                                       If(readerProvider("Degree") IsNot DBNull.Value, " " & readerProvider("Degree").ToString().Trim(), "")
                            End If
                        End While
                    End If
                End Using
            End Using

            ' Query 2: Fetch patient information
            Dim queryPatient As String = "
            SELECT * 
            FROM Patients 
            WHERE ID IN (SELECT Patient_ID FROM Requisitions WHERE ID = @AccID)"
            Using cmdPatient As New SqlCommand(queryPatient, connection)
                cmdPatient.Parameters.AddWithValue("@AccID", AccID)

                Using readerPatient As SqlDataReader = cmdPatient.ExecuteReader()
                    If readerPatient.HasRows Then
                        While readerPatient.Read()
                            Patient = readerPatient("LastName") & ", " &
                                  readerPatient("FirstName") & "-" &
                                  readerPatient("Sex") & "-" &
                                  Format(Convert.ToDateTime(readerPatient("DOB")), SystemConfig.DateFormat)
                        End While
                    End If
                End Using
            End Using

            ' Query 3: Fetch tests information
            Dim queryTests As String = "SELECT * FROM Req_TGP WHERE Accession_ID = @AccID"
            Using cmdTests As New SqlCommand(queryTests, connection)
                cmdTests.Parameters.AddWithValue("@AccID", AccID)

                Using readerTests As SqlDataReader = cmdTests.ExecuteReader()
                    If readerTests.HasRows Then
                        While readerTests.Read()
                            Comps += Trim(GetTGPShortName(readerTests("TGP_ID"))) & ","
                        End While
                        If Comps.EndsWith(",") Then Comps = Comps.Substring(0, Comps.Length - 1)
                    End If
                End Using
            End Using

            ' Query 4: Fetch DOC and EMR number
            Dim queryDOC_EMR As String = "
            SELECT 
                (SELECT MIN(SourceDate) FROM Specimens WHERE Accession_ID = @AccID) AS DOC, 
                EMRNo 
            FROM Requisitions 
            WHERE ID = @AccID"
            Using cmdDOC_EMR As New SqlCommand(queryDOC_EMR, connection)
                cmdDOC_EMR.Parameters.AddWithValue("@AccID", AccID)

                Using readerDOC_EMR As SqlDataReader = cmdDOC_EMR.ExecuteReader()
                    If readerDOC_EMR.HasRows Then
                        While readerDOC_EMR.Read()
                            DOC = Format(Convert.ToDateTime(readerDOC_EMR("DOC")), SystemConfig.DateFormat)
                            If readerDOC_EMR("EMRNo") IsNot DBNull.Value AndAlso Trim(readerDOC_EMR("EMRNo").ToString()) <> "" Then
                                EMRNo = readerDOC_EMR("EMRNo").ToString().Trim()
                            End If
                        End While
                    End If
                End Using
            End Using
        End Using

        ' Build label information
        Dim SRC() As String ' Old: Dim SRC() As String
        If Sources(0) <> "" Then
            For i = LBound(Sources) To UBound(Sources)
                SRC = Split(Sources(i), "^")
                If labelInfo(UBound(labelInfo)) <> "" Then ReDim Preserve labelInfo(UBound(labelInfo) + 1)
                labelInfo(UBound(labelInfo)) = Provider & "|" & Patient & "|" & AccID.ToString() & "-" & SRC(0) & "|" &
                                           DOC & "|" & SRC(1) & "|" & EMRNo & "|" & Val(SRC(2))
            Next
        End If

        If QTY > 0 Then
            If labelInfo(UBound(labelInfo)) <> "" Then ReDim Preserve labelInfo(UBound(labelInfo) + 1)
            labelInfo(UBound(labelInfo)) = Provider & "|" & Patient & "|" & AccID.ToString() & "|" &
                                       DOC & "|" & Tests & "|" & EMRNo & "|" & QTY
        End If

        Return labelInfo ' Old: Return labelInfo
    End Function

    Private Function LabelsSelected() As Boolean
        Dim Selected As Boolean = False
        For i As Integer = 0 To dgvAccessions.RowCount - 1
            If dgvAccessions.Rows(i).Cells(7).Value = True Then
                Selected = True
                Exit For
            End If
        Next
        Return Selected
    End Function

    Private Function AccessionsSelected() As Boolean
        Dim Selected As Boolean = False
        For i As Integer = 0 To dgvAccessions.RowCount - 1
            If dgvAccessions.Rows(i).Cells(6).Value = True Then
                Selected = True
                Exit For
            End If
        Next
        Return Selected
    End Function

    Private Sub PrintMultiReports(ByVal AccIDs() As String, ByVal Device As Integer)
        Dim SPDFS As New List(Of Byte())
        Dim FPDF As Byte() = Nothing
        Dim ExtCount As Integer = 0

        Dim fileName As String = "C:\ProgramData\Prolis\Temp\MultiAccs.PDF"

        Dim RPTFile As String = My.Application.Info.DirectoryPath &
        "\Reports\LabReqAck.RPT"
        If AccIDs(0) <> "" Then
            If Not IO.Directory.Exists("C:\ProgramData\Prolis\Temp\") Then _
            IO.Directory.CreateDirectory("C:\ProgramData\Prolis\Temp\")
            'TODO: Crystal Reports
            '==================================
            'For i As Integer = 0 To AccIDs.Length - 1
            '    If AccIDs(i) <> "" Then
            '        Dim oRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            '        oRpt.Load(RPTFile)
            '        ApplyNewServer(oRpt, My.Settings.DSN, My.Settings.Database,
            '        My.Settings.UID, My.Settings.PWD)
            '        oRpt.RecordSelectionFormula = "{Requisitions.ID} = " & AccIDs(i)
            '        'Dim S As IO.MemoryStream = oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat)
            '        'SPDFS.Add(S.ToArray)

            '        Using ms As New MemoryStream()
            '            Try
            '                oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat).CopyTo(ms)
            '                ms.Position = 0
            '                SPDFS.Add(ms.ToArray())
            '            Catch ex As Exception
            '                Dim err As String = ex.Message()
            '            End Try
            '        End Using

            '        oRpt.Close()
            '        oRpt = Nothing
            '    End If
            'Next
            '==================================

            FPDF = PdfHelper.MergePDFStreams(SPDFS)


            Try
                'If IO.File.Exists("C:\ProgramData\Prolis\Temp\MultiAccs.PDF") Then IO.File.Delete("C:\ProgramData\Prolis\Temp\MultiAccs.PDF")
                'Dim ms As New IO.FileStream("C:\ProgramData\Prolis\Temp\MultiAccs.PDF", IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.ReadWrite)

                If IO.File.Exists(fileName) Then IO.File.Delete(fileName)
                Dim ms As New IO.FileStream(fileName, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.ReadWrite)

                ms.Write(FPDF, 0, FPDF.Length)
                ms.Close()
                ms = Nothing
            Catch ex As Exception

            End Try

        End If
        '
        If Device = 0 Then  'printer

            'Dim PDFDOC As New Spire.Pdf.PdfDocument
            'PDFDOC.LoadFromBytes(FPDF)

            'Wait(5 + (SPDFS.Count \ 4) + (ExtCount * 5))
            'Dim PS As New System.Drawing.Printing.PrinterSettings
            'PDFDOC.PrintSettings.PrinterName = PS.PrinterName

            'PDFDOC.Print()

            Dim printerName As String = "" ' Optional: Specify printer name
            Dim errorMessage As String = ""

            Dim success As Boolean = PdfHelper.PrintPdfDocument(fileName, printerName, errorMessage)

            If success Then
                'MessageBox.Show("PDF printing initiated successfully.")
            Else
                MessageBox.Show("Error printing PDF: " & errorMessage)
            End If

            'Try

            '    Dim PDFDOC As PdfLoadedDocument = Nothing ' Important: Initialize to Nothing

            '    Try
            '        ' 1. Load the PDF document from file.
            '        PDFDOC = New PdfLoadedDocument(fileName)

            '        ' 2. Create a PdfDocumentView instance.
            '        Dim pdfViewer As New PdfDocumentView()

            '        ' 3. Assign the loaded document to the PdfDocumentView (but don't show it).
            '        pdfViewer.Load(PDFDOC)

            '        Dim printerSettings As New System.Drawing.Printing.PrinterSettings()
            '        '  If you need a specific printer, set it here. Otherwise, remove this line to use the default printer.
            '        ' printerSettings.PrinterName = "Your Printer Name"

            '        ' Create a PrintDocument and associate the PrinterSettings (REQUIRED for PdfViewer.Print to work correctly).
            '        Dim printDocument As New PrintDocument()
            '        printDocument.PrinterSettings = printerSettings

            '        ' 4. Print the PDF document (using the PrintDocument).
            '        pdfViewer.PrintDocument.Print()

            '        ' 5.  Dispose of the pdfViewer
            '        pdfViewer.Dispose()

            '    Finally
            '        ' 6.  Release Resources. Important:  Use a Finally block to ensure this happens!
            '        If PDFDOC IsNot Nothing Then
            '            PDFDOC.Close(True) ' Or False, depending on if you want to save changes
            '            PDFDOC.Dispose() ' Dispose of the document, also important
            '        End If
            '    End Try

            'Catch ex As Exception
            '    Console.WriteLine("Error printing PDF: " & ex.Message)
            'End Try

        ElseIf Device = 1 Then  'screen

            Dim f As New frmWebView()
            f.MdiParent = frmDashboard
            f.LoadPdfData(FPDF)
            f.Show()

        End If
    End Sub

    Private Sub cmbLabelReq_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLabelReq.SelectedIndexChanged
        cmbDestination.Items.Clear()
        Dim i As Integer
        If cmbLabelReq.SelectedIndex < 2 Then   'Label
            cmbDestination.Items.Add("Printer")
            For i = 0 To dgvAccessions.RowCount - 1
                dgvAccessions.Rows(i).Cells(6).Value = False
            Next
        Else    'Req
            cmbDestination.Items.Add("Printer")
            cmbDestination.Items.Add("Screen")
            For i = 0 To dgvAccessions.RowCount - 1
                dgvAccessions.Rows(i).Cells(7).Value = False
            Next
        End If
        cmbDestination.SelectedIndex = 0
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If dgvAccessions.SelectedRows(0).Cells(0).Value <> "" Then
            Dim Retval As Integer = MsgBox("Are you sure you want to delete the " &
            "selected accession?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Prolis")
            If Retval = vbYes Then
                Dim CurAccVals As String = GetCurrentAccVals(Val(dgvAccessions.SelectedRows(0).Cells(0).Value))
                ExecuteSqlProcedure("Delete from Requisitions Where ID = " & Val(dgvAccessions.SelectedRows(0).Cells(0).Value))
                ExecuteSqlProcedure("Delete from Req_TGP where Accession_ID = " & Val(dgvAccessions.SelectedRows(0).Cells(0).Value))
                ExecuteSqlProcedure("Delete from Acc_Results where Accession_ID = " & Val(dgvAccessions.SelectedRows(0).Cells(0).Value))
                ExecuteSqlProcedure("Delete from Acc_Info_Results where Accession_ID = " & Val(dgvAccessions.SelectedRows(0).Cells(0).Value))
                ExecuteSqlProcedure("Delete from Ref_Results where Accession_ID = " & Val(dgvAccessions.SelectedRows(0).Cells(0).Value))
                ExecuteSqlProcedure("Delete from Req_DX where Accession_ID = " & Val(dgvAccessions.SelectedRows(0).Cells(0).Value))
                ExecuteSqlProcedure("Delete from Req_Coverage where Accession_ID = " & Val(dgvAccessions.SelectedRows(0).Cells(0).Value))
                ExecuteSqlProcedure("Delete from Specimens where Accession_ID = " & Val(dgvAccessions.SelectedRows(0).Cells(0).Value))
                ExecuteSqlProcedure("Delete from Req_Med where Accession_ID = " & Val(dgvAccessions.SelectedRows(0).Cells(0).Value))
                ExecuteSqlProcedure("Delete from Req_RPT where Base_ID = " & Val(dgvAccessions.SelectedRows(0).Cells(0).Value))
                LogUserEvent(ThisUser.ID, 4, Date.Now, "Accession",
                Val(dgvAccessions.SelectedRows(0).Cells(0).Value), CurAccVals, "")
                dgvSources.Rows.Clear()
                PopulateAccessions()
                btnDelete.Enabled = False
            End If
        End If
    End Sub

    Private Function GetCurrentAccVals(ByVal AccID As Long) As String
        Dim AccVals As String = ""
        'Dim i As Integer
        Dim TempVal As String = ""
        Dim VALS() As String = {"", "", "", "", "", "", "", ""} 'AccId, AccDate, Specimen, Provider, Patient, Orders, Reports, Billing
        Dim sSQL As String = "Select a.ID as AccID, a.AccessionDate as AccDate, a.OrderingProvider_ID as OrdPrID, " &
        "a.AttendingProvider_ID as AttPrID, a.BillingType_ID as BillingType, b.LastName as LName, b.FirstName " &
        "as FName, b.DOB as DOB, b.Sex as Sex from Requisitions a inner join Patients b on b.ID = a.Patient_ID " &
        "where a.ID = " & AccID
        Dim cnav As New SqlConnection(connString)
        cnav.Open()
        Dim cmdav As New SqlCommand(sSQL, cnav)
        cmdav.CommandType = CommandType.Text
        Dim drav As SqlDataReader = cmdav.ExecuteReader
        If drav.HasRows Then
            While drav.Read
                VALS(0) = "AccID = " & AccID.ToString
                VALS(1) = "AccDate = " & Format(drav("AccDate"), SystemConfig.DateFormat & " HH:mm")
                VALS(2) = "Specimen = "
                VALS(3) = "Client = " & drav("OrdPrID").ToString & "^" & drav("AttPrID").ToString
                VALS(4) = "Patient = " & drav("LName") & "^" & drav("FName") _
                & "^" & Format(drav("DOB"), SystemConfig.DateFormat) & "^" &
                Microsoft.VisualBasic.Left(drav("Sex"), 1)
                If drav("BillingType") = 0 Then
                    TempVal = "Billing = Client"
                ElseIf drav("BillingType") = 2 Then
                    TempVal = "Billing = Patient"
                Else
                    TempVal = "Billing = Insurance"
                    Dim cnsp3 As New SqlConnection(connString)
                    cnsp3.Open()
                    Dim cmdsp3 As New SqlCommand("Select * from Req_Coverage " &
                    "where Accession_ID = " & AccID & " order by Preference", cnsp3)
                    cmdsp3.CommandType = CommandType.Text
                    Dim drsp3 As SqlDataReader = cmdsp3.ExecuteReader
                    If drsp3.HasRows Then
                        While drsp3.Read
                            If drsp3("Preference") = "P" Then
                                TempVal += "|Primary Payer ID: " & drsp3("Payer_ID").ToString &
                                "^Policy: " & drsp3("PolicyNo") & "^Relation: " & drsp3("Relation") &
                                "^Insured ID: " & drsp3("Insured_ID").ToString
                            Else
                                TempVal += "|Secondary Payer ID: " & drsp3("Payer_ID").ToString &
                                "^Policy: " & drsp3("PolicyNo") & "^Relation: " & drsp3("Relation") &
                                "^Insured ID: " & drsp3("Insured_ID").ToString
                            End If
                        End While
                    End If
                    cnsp3.Close()
                    cnsp3 = Nothing
                    VALS(7) = TempVal
                End If
            End While
        End If
        cnav.Close()
        cnav = Nothing
        '
        TempVal = ""
        Dim cnav1 As New SqlConnection(connString)
        cnav1.Open()
        Dim cmdav1 As New SqlCommand("Select * from Req_TGP " &
        "where Accession_ID = " & AccID & " order by Ordinal", cnav1)
        cmdav1.CommandType = CommandType.Text
        Dim drav1 As SqlDataReader = cmdav1.ExecuteReader
        If drav1.HasRows Then
            While drav1.Read
                TempVal += drav1("TGP_ID").ToString & "^"
            End While
            If TempVal.EndsWith("^") Then TempVal = TempVal.Substring(0, Len(TempVal) - 1)
            VALS(5) = TempVal
        End If
        cnav1.Close()
        cnav1 = Nothing
        '
        TempVal = ""
        Dim cnavr As New SqlConnection(connString)
        cnavr.Open()
        Dim cmdavr As New SqlCommand("Select * from Req_RPT where Base_ID = " & AccID, cnavr)
        cmdavr.CommandType = CommandType.Text
        Dim dravr As SqlDataReader = cmdavr.ExecuteReader
        If dravr.HasRows Then
            TempVal = "Reports = "
            While dravr.Read
                TempVal += dravr("Provider_ID").ToString &
                 "^RCO: " & IIf(dravr("RPT_Complete") = 0, "No", "Yes") &
                 "^Print: " & IIf(dravr("RPT_Print") = 0, "No", "Yes") &
                 "^ProlisOn: " & IIf(dravr("RPT_ProlisOn") = 0, "No", "Yes") &
                 "^Interface: " & IIf(dravr("RPT_Interface") = 0, "No", "Yes") &
                 "^Fax: " & IIf(dravr("RPT_Fax") = 0, "No", "Yes") &
                 "^Email: " & IIf(dravr("RPT_Email") = 0, "No", "Yes") & "|"
            End While
            If TempVal.EndsWith("|") Then TempVal = TempVal.Substring(0, Len(TempVal) - 1)
            VALS(6) = TempVal
        End If
        cnavr.Close()
        cnavr = Nothing
        '
        AccVals = Join(VALS, "|")
        Return AccVals
    End Function



    Private Sub SelectAll_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
        For r As Integer = 0 To dgvAccessions.Rows.Count - 1
            dgvAccessions.Rows(r).Cells(8).Value = False
        Next
        dgvSources.Rows.Clear()


    End Sub

    Private Sub dgvAccessions_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAccessions.CellClick
        Try
            Clipboard.SetText(dgvAccessions.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
            TextBox1.Text = dgvAccessions.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & " Copied to Clipboard"
        Catch ex As Exception

        End Try
        dgvSources.Rows.Clear()

    End Sub

    Private Sub nxtbtn_Click(sender As Object, e As EventArgs)
        dgvSources.Rows.Clear()
        Dim crnt = Convert.ToInt32(pagtxt.Text)
        crnt += 1
        If txtAccPageCount.Text = "" Then
            txtAccPageCount.Text = "0"
        End If
        If crnt <= 0 Or crnt > Convert.ToInt32(txtAccPageCount.Text) Then
            'Return
        End If

        Dim page = (crnt - 1) * pagesize
        PopulateAccessions(page.ToString, pagesize)
        pagtxt.Text = crnt.ToString
    End Sub

    Private Sub prevbtn_Click(sender As Object, e As EventArgs)
        dgvSources.Rows.Clear()
        Dim crnt = Convert.ToInt32(pagtxt.Text)
        crnt -= 1
        If crnt <= 0 Then
            Return
        End If

        Dim page = (crnt - 1) * pagesize
        PopulateAccessions(page.ToString, pagesize)
        pagtxt.Text = crnt.ToString
    End Sub


    Private Sub SelectChk_CheckedChanged(sender As Object, e As EventArgs) Handles SelectChk.CheckedChanged
        If SelectChk.Checked Then
            ' RecAll.Show()
            For i As Integer = 0 To dgvAccessions.RowCount - 1
                If BupliCate(dgvAccessions.Rows(i).Cells(1).Value) Then
                    ' MessageBox.Show("The Requisition Number (  " & dgvAccessions.Rows(i).Cells(1).Value & "  ) with related info Already Exists in System. You can check from Accession Dashboard.", "Info")

                Else
                    dgvAccessions.Rows(i).Cells(8).Value = True

                End If

            Next
        Else
            'RecAll.Hide()
            For i As Integer = 0 To dgvAccessions.RowCount - 1
                dgvAccessions.Rows(i).Cells(8).Value = False
            Next
        End If
    End Sub

    Private Sub LabelChk_CheckedChanged(sender As Object, e As EventArgs) Handles LabelChk.CheckedChanged
        If LabelChk.Checked Then
            For i As Integer = 0 To dgvAccessions.RowCount - 1
                dgvAccessions.Rows(i).Cells(7).Value = True
            Next
        Else
            For i As Integer = 0 To dgvAccessions.RowCount - 1
                dgvAccessions.Rows(i).Cells(7).Value = False
            Next
        End If
    End Sub

    Private Sub ReqChk_CheckedChanged(sender As Object, e As EventArgs) Handles ReqChk.CheckedChanged
        If ReqChk.Checked Then
            For i As Integer = 0 To dgvAccessions.RowCount - 1
                dgvAccessions.Rows(i).Cells(6).Value = True
            Next
        Else
            For i As Integer = 0 To dgvAccessions.RowCount - 1
                dgvAccessions.Rows(i).Cells(6).Value = False
            Next
        End If
    End Sub


    Private Sub RecAll_Click(sender As Object, e As EventArgs) Handles RecAll.Click
        Dim anyselected = False
        Dim req1 = ""
        If dgvAccessions.RowCount > 0 Then
            btnDelete.Enabled = False
            dgvSources.Rows.Clear()
            Dim ff = dgvAccessions.Rows(0).Cells(8).Value
            Dim ids = ""
            For r As Integer = 0 To dgvAccessions.Rows.Count - 1
                If dgvAccessions.Rows(r).Cells(8).Value = True Then
                    anyselected = True
                    Exit For
                End If
            Next
            For r As Integer = 0 To dgvAccessions.Rows.Count - 1
                If anyselected = False Then
                    MessageBox.Show("Please select an accession")
                    Exit For
                End If
                If r >= dgvAccessions.Rows.Count Then
                    Exit For
                End If
                If dgvAccessions.Rows(r).Cells(8).Value = False Then
                    Continue For

                End If

                If BupliCate(dgvAccessions.Rows(r).Cells(1).Value) Then
                    dgvAccessions.Rows(r).Cells(8).Value = False
                    req1 += "," & dgvAccessions.Rows(r).Cells(1).Value
                    Continue For
                Else
                End If


                Dim cnsp As New SqlConnection(connString)
                cnsp.Open()
                Dim cmdsp As New SqlCommand("Select * from Specimens where " &
           "Accession_ID = " & dgvAccessions.Rows(r).Cells(0).Value, cnsp)
                cmdsp.CommandType = CommandType.Text
                Dim drsp As SqlDataReader = cmdsp.ExecuteReader
                If drsp.HasRows Then
                    While drsp.Read
                        Try
                            dgvSources.Rows.Add(drsp("Source_ID"), drsp("SourceNo"),
                                            GetSourceName(drsp("Source_ID")), drsp("SourceQuantity"),
                                            Format(drsp("SourceDate"), SystemConfig.DateFormat),
                                            Format(drsp("SourceDate"), "HH:mm"), drsp("SourceTemp"), True)
                        Catch ex As Exception

                        End Try

                    End While
                Else
                    dgvAccessions.Rows(r).Cells(0).Style.ForeColor = Color.Red
                End If
                cnsp.Close()
                cnsp = Nothing
                btnDelete.Enabled = True
            Next
            If Not req1 = "" Then
                MessageBox.Show("Some Requisition Number(s) has copied to clipboard as they are Duplicate in the system. You can paste it in some text file to further search.", "Info")
                Clipboard.SetText(req1)
            End If

        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        dgvSources.Rows.Clear()
        Dim crnt = Convert.ToInt32(txtAccPageCount.Text)
        crnt += 1
        If txtAccPageCount.Text = "" Then
            txtAccPageCount.Text = "0"
        End If
        If crnt <= 0 Or crnt > Convert.ToInt32(txtAccPageCount.Text) Then
            '  Return
        End If

        Dim page = (crnt - 1) * pagesize
        PopulateAccessions(page.ToString, pagesize)
        pagtxt.Text = crnt.ToString
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        dgvSources.Rows.Clear()
        Dim crnt = 1
        If txtAccPageCount.Text = "" Then
            txtAccPageCount.Text = "0"
        End If
        If crnt <= 0 Or crnt > Convert.ToInt32(txtAccPageCount.Text) Then
            '  Return
        End If

        Dim page = (crnt - 1) * pagesize
        PopulateAccessions(page.ToString, pagesize)
        pagtxt.Text = crnt.ToString
    End Sub

    Private Sub dgvAccessions_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAccessions.CellContentDoubleClick
        Try
            Clipboard.SetText(dgvAccessions.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
            TextBox1.Text = dgvAccessions.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & " Copied to Clipboard"
        Catch ex As Exception

        End Try

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

    Private Sub frmRemoteAcc_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

    End Sub

    Private Function BupliCate(ID As String) As Boolean
        Dim rs = False
        Dim cnsp As New SqlConnection(connString)
        cnsp.Open()
        Dim qu = "WITH DuplicateIDs1 AS (" &
                                            "SELECT Patient_ID, ID, RequisitionNO, AccessionDate, Reported_Initial, Received, COUNT(*) OVER (PARTITION BY RequisitionNO) AS count " &
                                            "FROM Requisitions " &
                                            "WHERE  RequisitionNO='" & ID & "' and RequisitionNO <> '') " &
                                            "SELECT Patient_ID, ID, RequisitionNO, AccessionDate, Reported_Initial, Received " &
                                            "FROM DuplicateIDs1 " &
                                            "WHERE count > 1 " &
                                            "ORDER BY RequisitionNO, AccessionDate, Received"

        Dim cmdsp As New SqlCommand(qu, cnsp)
        cmdsp.CommandType = CommandType.Text
        Dim drsp As SqlDataReader = cmdsp.ExecuteReader
        If drsp.HasRows Then
            rs = True
        End If
        cnsp.Close()
        cnsp = Nothing
        Return rs
    End Function

    Private Sub dgvSources_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSources.CellClick
        Try
            Clipboard.SetText(dgvSources.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
            TextBox1.Text = dgvSources.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & " Copied to Clipboard"
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Label19_Click(sender As Object, e As EventArgs)
        txtTerm.Text = Clipboard.GetText()
    End Sub

    Private Sub btnPatLook_Click(sender As Object, e As EventArgs) Handles btnPatLook.Click
        Dim PatientID As String = frmPatLookUp.ShowDialog()
        If PatientID <> "" Then
            txtPatientID.Text = (Val(PatientID))
            btnBrowse.PerformClick()
            'PopulatePatientDxs(PatientID)
        End If
    End Sub

    Private Sub Label19_Click_1(sender As Object, e As EventArgs) Handles Label19.Click
        txtTerm.Text = Clipboard.GetText()
    End Sub
End Class
