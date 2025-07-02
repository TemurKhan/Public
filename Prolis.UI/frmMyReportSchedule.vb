Public Class frmMyReportSchedule

    Private Sub btnRPTLookup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRPTLookup.Click
        Dim DLG As New OpenFileDialog
        DLG.Filter = "Crystal Report Files (*.RPT)|*.rpt|All files (*.*)|*.*"
        If MsgBoxResult.Ok = DLG.ShowDialog Then
            txtSource.Text = UploadRPTFile(DLG.FileName)
        Else
            txtSource.Text = ""
        End If
        DLG.Dispose()
        DLG = Nothing
        UpdateProgress()
    End Sub

    Private Function UploadRPTFile(ByVal oFile As String) As String
        Dim FLName As String = ""
        Dim TargetPath As String = "\\ASSC-PC\ProlisMyReports\"
        If Trim(oFile) <> "" Then
            FLName = Microsoft.VisualBasic.Mid(Trim(oFile), _
            InStrRev(Trim(oFile), "\") + 1)
            If IO.Directory.Exists(TargetPath) Then
                If Trim(oFile) <> TargetPath & FLName Then _
                System.IO.File.Copy(Trim(oFile), TargetPath & FLName, True)
            End If
        End If
        Return FLName
    End Function

    Private Sub UpdateProgress()
        If txtID.Text <> "" And txtSource.Text <> "" And cmbInterval.SelectedIndex <> -1 _
        And txtFrequency.Text <> "" And cmbAddressType.SelectedIndex <> -1 AndAlso _
        ((cmbAddressType.SelectedItem.ToString = "Email" And IsEmailValid(txtAddress.Text)) _
        OrElse (cmbAddressType.SelectedItem.ToString = "Folder" And txtAddress.Text <> "")) Then
            btnSave.Enabled = True
            btnDelete.Enabled = True
        Else
            btnSave.Enabled = False
            btnDelete.Enabled = False
        End If
    End Sub

    Private Sub txtFrequency_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFrequency.Validated
        If txtFrequency.Text = "" Then txtFrequency.Text = "Infinite"
        UpdateProgress()
    End Sub

    Private Sub frmMyReportSchedule_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbSourceType.SelectedIndex = 0
        cmbOutput.SelectedIndex = 0
        cmbAddressType.SelectedIndex = 0
        LoadRPTSchedules()
    End Sub

    Private Sub LoadRPTSchedules()
        dgvSchedule.Rows.Clear()
        Dim LastRunDate As String = ""
        Dim RunCount As String = ""
        Dim cnrpts As New Data.SqlClient.SqlConnection(connString)
        cnrpts.Open()
        Dim cmdrpts As New Data.SqlClient.SqlCommand("Select * from MyReportSchedule", cnrpts)
        cmdrpts.CommandType = Data.CommandType.Text
        Dim drrpts As Data.SqlClient.SqlDataReader = cmdrpts.ExecuteReader
        If drrpts.HasRows Then
            While drrpts.Read
                If drrpts("LastRunDate") Is DBNull.Value Then
                    LastRunDate = "------"
                    RunCount = drrpts("Frequency")
                Else
                    LastRunDate = Format(drrpts("LastRunDate"), SystemConfig.DateFormat)
                    If drrpts("Frequency") = "Infinite" Then
                        RunCount = drrpts("Frequency")
                    Else
                        RunCount = drrpts("RunningCount").ToString
                    End If
                End If
                dgvSchedule.Rows.Add(drrpts("ID"), Format(drrpts("StartDate"), SystemConfig.DateFormat),
                drrpts("Source"), drrpts("Interval"), drrpts("Frequency"), drrpts("DestType"), drrpts("Address"), LastRunDate, RunCount)
            End While
        End If
        cnrpts.Close()
        cnrpts = Nothing
    End Sub

    Private Sub dgvSchedule_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSchedule.CellDoubleClick
        If e.RowIndex <> -1 Then
            displaySchedule(dgvSchedule.Rows(e.RowIndex).Cells(0).Value)
        End If
        UpdateProgress()
    End Sub

    Private Sub displaySchedule(ByVal ScheduleID As Long)
        Dim cnds As New Data.SqlClient.SqlConnection(connString)
        cnds.Open()
        Dim cmdds As New Data.SqlClient.SqlCommand("Select * from MyReportSchedule where ID = " & ScheduleID, cnds)
        cmdds.CommandType = Data.CommandType.Text
        Dim drds As Data.SqlClient.SqlDataReader = cmdds.ExecuteReader
        If drds.HasRows Then
            While drds.Read
                txtID.Text = drds("ID")
                If drds("StartDate") IsNot DBNull.Value Then
                    dtpStartDate.Value = drds("StartDate")
                Else
                    dtpStartDate.Value = drds("CreatedOn")
                End If
                For i As Integer = 0 To cmbSourceType.Items.Count - 1
                    If drds("SourceType") IsNot DBNull.Value AndAlso
                    drds("SourceType") = cmbSourceType.Items(i).ToString Then
                        cmbSourceType.SelectedIndex = i
                        Exit For
                    End If
                Next
                For i As Integer = 0 To cmbAddressType.Items.Count - 1
                    If drds("destType") = cmbAddressType.Items(i).ToString Then
                        cmbAddressType.SelectedIndex = i
                        Exit For
                    End If
                Next
                txtSource.Text = drds("Source")
                For i As Integer = 0 To cmbInterval.Items.Count - 1
                    If cmbInterval.Items(i).ToString = drds("Interval") Then
                        cmbInterval.SelectedIndex = i
                        Exit For
                    End If
                Next
                txtFrequency.Text = drds("Frequency")
                txtAddress.Text = drds("Address")
                For i As Integer = 0 To cmbOutput.Items.Count - 1
                    If drds("OutputType") = cmbOutput.Items(i).ToString Then
                        cmbOutput.SelectedIndex = i
                        Exit For
                    End If
                Next
            End While
        End If
        cnds.Close()
        cnds = Nothing
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtID.Text <> "" And txtSource.Text <> "" And cmbInterval.SelectedIndex <> -1 _
        And cmbOutput.SelectedIndex <> -1 And txtFrequency.Text <> "" And
        ((cmbAddressType.SelectedItem.ToString.StartsWith("Email") And
        IsEmailValid(txtAddress.Text)) Or (cmbAddressType.SelectedItem.ToString.StartsWith("Folder") _
        And Trim(txtAddress.Text) <> "")) Then
            SaveSchedule()
            FormClear()
            LoadRPTSchedules()
        End If
        UpdateProgress()
    End Sub

    Private Sub SaveSchedule()
        Dim sSQL As String = "If Exists (Select * from MyReportSchedule where ID = " & Val(txtID.Text) & ") Update MyReportSchedule " &
        "Set StartDate = '" & dtpStartDate.Value & "', SourceType = '" & cmbSourceType.SelectedItem.ToString & "', Source = '" &
        Trim(txtSource.Text) & "', Interval = '" & cmbInterval.SelectedItem.ToString & "', Frequency = '" & Trim(txtFrequency.Text) &
        "', OutputType = '" & cmbOutput.SelectedItem.ToString & "', DestType = '" & cmbAddressType.SelectedItem.ToString &
        "', Address = '" & Trim(txtAddress.Text) & "', LastEditedBy = " & ThisUser.ID & ", LastEditedOn = '" & Date.Now & "' " &
        "where ID = " & Val(txtID.Text) & " Else Insert into MyReportSchedule (ID, StartDate, SourceType, Source, Interval, " &
        "Frequency, OutputType, DestType, Address, CreatedBy, CreatedOn, LastEditedBy, LastEditedOn) values (" & Val(txtID.Text) &
        ", '" & dtpStartDate.Value & "', '" & cmbSourceType.SelectedItem.ToString & "', '" & Trim(txtSource.Text) & "', '" &
        cmbInterval.SelectedItem.ToString & "', '" & Trim(txtFrequency.Text) & "', '" & cmbOutput.SelectedItem.ToString & "', '" &
        cmbAddressType.SelectedItem.ToString & "', '" & Trim(txtAddress.Text) & "', " & ThisUser.ID & ", '" & Date.Now & "', " &
        ThisUser.ID & ", '" & Date.Now & "')"
        ExecuteSqlProcedure(sSQL)
    End Sub

    Private Sub FormClear()
        txtID.Text = GetNextScheduleID()
        txtSource.Text = ""
        cmbInterval.SelectedIndex = 3
        txtFrequency.Text = "Infinite"
        txtAddress.Text = ""
    End Sub

    Private Function GetNextScheduleID() As Long
        Dim SchID As Long = 0
        Dim cngns As New Data.SqlClient.SqlConnection(connString)
        cngns.Open()
        Dim cmdgns As New Data.SqlClient.SqlCommand("Select Max(ID) as LastID from MyReportSchedule", cngns)
        cmdgns.CommandType = Data.CommandType.Text
        Dim drgns As Data.SqlClient.SqlDataReader = cmdgns.ExecuteReader
        If drgns.HasRows Then
            While drgns.Read
                If drgns("LastID") Is DBNull.Value Then
                    SchID = 1
                Else
                    SchID = drgns("LastID") + 1
                End If
            End While
        Else
            SchID = 1
        End If
        cngns.Close()
        cngns = Nothing
        Return SchID
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If txtID.Text <> "" And txtSource.Text <> "" Then
            Dim RetVal = MsgBox("Are you sure to delete the displayed schedule?", MsgBoxStyle.Question + MsgBoxStyle.YesNo)
            If RetVal = vbYes Then
                ExecuteSqlProcedure("Delete from MyReportSchedule where ID = " & Val(txtID.Text))
                FormClear()
                LoadRPTSchedules()
            End If
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        FormClear()
        UpdateProgress()
    End Sub

    Private Sub txtEmail_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAddress.KeyPress
        If Keys.Enter Then
            UpdateProgress()
        End If
    End Sub

    Private Sub txtEmail_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAddress.Validated
        UpdateProgress()
    End Sub

    Private Sub cmbInterval_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbInterval.SelectedIndexChanged
        UpdateProgress()
    End Sub

    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.Click
        HelpProvider1.HelpNamespace = My.Application.Info.DirectoryPath & "\ProlisHelp.chm"
        HelpProvider1.SetHelpKeyword(Me, 35)
        HelpProvider1.SetHelpNavigator(Me, HelpNavigator.TopicId)
        Help.ShowHelp(Me, My.Application.Info.DirectoryPath & "\ProlisHelp.chm")
    End Sub

    Private Sub cmbSourceType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSourceType.SelectedIndexChanged
        If cmbSourceType.SelectedItem.ToString = "Report" Then
            txtSource.ReadOnly = True
            btnRPTLookup.Enabled = True
        Else
            txtSource.ReadOnly = False
            btnRPTLookup.Enabled = False
        End If
        UpdateProgress()
    End Sub

    Private Sub txtAddress_TextChanged(sender As Object, e As EventArgs) Handles txtAddress.TextChanged
        UpdateProgress()
    End Sub

    Private Sub cmbAddressType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAddressType.SelectedIndexChanged
        If cmbAddressType.SelectedItem.ToString.Contains("Email") Then
            lblAddress.Text = "Valid Email Address"
        Else
            lblAddress.Text = "Shared Folder"
        End If
    End Sub
End Class