Imports System.Windows.Forms
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmMyReports

    Dim folderPath As String

    Private Sub frmMyReports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'btnRptBuild.Enabled = ThisUser.Report_Build

        folderPath = My.Application.Info.DirectoryPath & "\MyReports\"
        PopulateReports()
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub PopulateReports()

        If Directory.Exists(folderPath) Then
            Dim folderNames As String() = Directory.GetDirectories(folderPath)

            cbxReportType.Items.Clear()
            cbxReportType.Items.Add("Select All")
            cbxReportType.SelectedIndex = 0
            For Each folderName As String In folderNames
                Dim folderInfo As New DirectoryInfo(folderName)

                cbxReportType.Items.Add(folderInfo.Name)

            Next
        Else
            MsgBox("Prolis core directory 'MyReports' is missing")
        End If

        
    End Sub

    Private Sub dgvReports_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvReports.CellClick
        btnPrint.Enabled = True
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If dgvReports.SelectedRows(0).Index <> -1 Then
            Dim FL As String = Microsoft.VisualBasic.Mid(dgvReports.SelectedRows(0).Cells(2).Value, InStrRev(dgvReports.SelectedRows(0).Cells(2).Value, "\") + 1)
            If UserPermittedReport(FL, ThisUser.ID) Then
                Try
                    'TODO: Crystal Reports
                    '====================================
                    'Dim gRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'gRpt.Load(dgvReports.SelectedRows(0).Cells(2).Value)
                    'ApplyNewServer(gRpt, My.Settings.DSN, My.Settings.Database,
                    'My.Settings.UID.ToString, My.Settings.PWD.ToString)
                    ''gRpt.SetDatabaseLogon(UID, PWD)
                    'frmRV.CRRV.ReportSource = gRpt
                    'frmRV.Show()
                    '====================================
                Catch Ex As Exception
                    MsgBox(Ex.Message, MsgBoxStyle.Critical, "Prolis")
                End Try
            Else
                MsgBox("Oops! you don't have the permission to process this report", MsgBoxStyle.Critical, "Prolis")
            End If
        End If
    End Sub

    Private Sub btnRptBuild_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmRptBuild.ShowDialog()
        PopulateReports()
    End Sub


    Private Sub cbxReportType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxReportType.SelectedIndexChanged

        If cbxReportType.SelectedItem = "Select All" Then
         
            If Directory.Exists(folderPath) Then
                Dim fileNames As String() = Directory.GetFiles(folderPath, "*.rpt", SearchOption.AllDirectories)

                Dim i As Integer = 1
                For Each fileName As String In fileNames

                    Dim fileInfo As New FileInfo(fileName)
                    Dim fileNameWithoutExtension As String = Path.GetFileNameWithoutExtension(fileInfo.Name).Replace("_", " ")
                    dgvReports.Rows.Add(i.ToString, fileNameWithoutExtension, fileInfo.ToString)
                    i += 1
                Next
            Else
                MsgBox("Prolis core directory 'MyReports' is missing")
            End If

        Else
            Dim subFolderPath As String = Path.Combine(folderPath, cbxReportType.SelectedItem)
            dgvReports.Rows.Clear()

            If Directory.Exists(subFolderPath) Then
                Dim fileNames As String() = Directory.GetFiles(subFolderPath, "*.rpt")

                Dim i As Integer = 1
                For Each fileName As String In fileNames

                    Dim fileInfo As New FileInfo(fileName)
                    Dim fileNameWithoutExtension As String = Path.GetFileNameWithoutExtension(fileInfo.Name).Replace("_", " ")
                    dgvReports.Rows.Add(i.ToString, fileNameWithoutExtension, fileInfo.ToString)
                    i += 1
                Next
            End If
        End If

    End Sub

    Private Sub txtSearchRptName_TextChanged(sender As Object, e As EventArgs) Handles txtSearchRptName.TextChanged
        Dim filterText As String = txtSearchRptName.Text.Trim()

        For Each row As DataGridViewRow In dgvReports.Rows
            If String.IsNullOrEmpty(filterText) Then
                ' If filter text is empty, show all rows
                row.Visible = True
            Else
                ' Get the value of the desired column (assuming it's the first column)
                Dim cellValue As String = row.Cells(1).Value.ToString()

                ' Case-insensitive check for the filter text within the cell value
                If cellValue.IndexOf(filterText, StringComparison.OrdinalIgnoreCase) >= 0 Then
                    row.Visible = True ' Show the row if there's a match
                Else
                    row.Visible = False ' Hide the row if there's no match
                End If
            End If
        Next
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        txtSearchRptName.Clear()
    End Sub
End Class
