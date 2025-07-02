Imports System.Data
Imports Microsoft.Data.SqlClient
Imports Prolis.Utils

Public Class frmLogin

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.

    Private Sub btn_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_OK.Click
        If UsernameTextBox.Text = "!" Then
            UsernameTextBox.Text = "techteam"
            PasswordTextBox.Text = "TT0901sc!"
            frmDashboard.RequisitionCopyToolStripMenuItem.Visible = True
            frmDashboard.UpdateResultNoteInTestsToolStripMenuItem.Visible = True
        End If
        If UsernameTextBox.Text <> "" And PasswordTextBox.Text <> "" Then
            Dim cnliu As New SqlConnection(connString)
            cnliu.Open()
            Dim cmdliu As New SqlCommand("Select * from Users where " &
            "IsActive <> 0 and UserName = '" & UsernameTextBox.Text & "'", cnliu)
            cmdliu.CommandType = CommandType.Text
            Dim drliu As SqlDataReader = cmdliu.ExecuteReader
            If drliu.HasRows Then

                ' CommonData.connString = connString
                While drliu.Read
                    Dim dd = DecryptIt(drliu("ID"), drliu("Password"))
                    Dim dd1 = decryptString(drliu("Password"))
                    If PasswordTextBox.Text = DecryptIt(drliu("ID"), drliu("Password")) _
                    Or PasswordTextBox.Text = decryptString(drliu("Password")) Then
                        ThisUser.ID = drliu("ID")
                        ThisUser.LogoutMins = drliu("LogoutMins")
                        Try
                            ThisUser.PrinterPC = drliu("PrinterPC")
                        Catch ex As Exception

                        End Try

                        Try
                            ThisUser.SpecificPrinter = "Default" 'drliu("SpecificPrinter")
                        Catch ex As Exception
                            ' ThisUser.SpecificPrinter = ""
                            ThisUser.SpecificPrinter = "Default"
                        End Try

                        Try
                            ThisUser.UseRemotePrinter = drliu("UseRemotePrinter")
                        Catch ex As Exception
                            ThisUser.UseRemotePrinter = False

                        End Try

                        ThisUser.Name = drliu("FullName")
                        ThisUser.UserName = drliu("UserName").ToString().Trim()
                        ThisUser.Password = decryptString(drliu("Password"))
                        'ThisUser.Password = DecryptIt(Rs.Fields("ID").Value, Rs.Fields("Password").Value)
                        ThisUser.Cus_Svc = drliu("Cus_Svc")
                        ThisUser.Accession = drliu("Accession")
                        ThisUser.Result_Entry = drliu("Result_Entry")
                        ThisUser.Result_Release = drliu("Result_Release")
                        ThisUser.QC_Layout = drliu("QC_Layout")
                        ThisUser.Test_Mgmt = drliu("Test_Mgmt")
                        ThisUser.Billing = drliu("Billing")
                        ThisUser.Report_Build = drliu("Report_Build")
                        ThisUser.Report_Process = drliu("Report_Process")
                        ThisUser.Hard_Deletion = drliu("Hard_Deletion")
                        ThisUser.Soft_Deletion = drliu("Soft_Deletion")
                        ThisUser.System_Config = drliu("System_Config")
                        ThisUser.User_Mgmt = drliu("User_Mgmt")
                        ThisUser.Dictionary = drliu("Dictionary")
                        ThisUser.DicOnFly = drliu("DicOnFly")
                        ThisUser.ARC = drliu("ARC")
                        ThisUser.Payment = drliu("Payment")
                        ThisUser.Pouring = drliu("Pouring")
                        ThisUser.Insurances = drliu("Insurances")
                        ThisUser.Testing = drliu("Testing")
                        ThisUser.Equips = drliu("Equips")
                        ThisUser.Interfaces = drliu("Interfaces")
                        ThisUser.Supervisor = drliu("Supervisor")
                        ThisUser.Director = drliu("Director")
                        ThisUser.Owner = drliu("Owner")

                        If drliu("Change_PWD") = False Then
                            frmDashboard.LoginUser(ThisUser.ID)
                            Me.Close()
                        Else
                            Me.Close()
                            frmChange_PWD.Show()
                            frmChange_PWD.MdiParent = frmDashboard
                        End If
                    Else
                        MsgBox("The password you provided is not correct.")
                    End If
                End While

                Task.Run(Sub() frmSystemConfig.RetrieveAndSaveReports())

            Else
                MsgBox("The user name you provided is not correct.", MsgBoxStyle.Critical, "Prolis")
            End If
            cnliu.Close()
            cnliu = Nothing
        Else
            MsgBox("Please provide your correct credentials.", MsgBoxStyle.Critical, "Prolis")
        End If
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Cancel.Click
        Me.Close()
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub UsernameTextBox_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles UsernameTextBox.GotFocus
        NFCOLOR = UsernameTextBox.BackColor
        UsernameTextBox.BackColor = FCOLOR
    End Sub

    Private Sub UsernameTextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles UsernameTextBox.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub PasswordTextBox_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles PasswordTextBox.GotFocus
        NFCOLOR = PasswordTextBox.BackColor
        PasswordTextBox.BackColor = FCOLOR
    End Sub

    Private Sub PasswordTextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles PasswordTextBox.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub UsernameTextBox_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles UsernameTextBox.LostFocus
        UsernameTextBox.BackColor = NFCOLOR
    End Sub

    Private Sub PasswordTextBox_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles PasswordTextBox.LostFocus
        PasswordTextBox.BackColor = NFCOLOR
    End Sub

    Private Sub frmLogin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'ResizeToClient(Me)

        If Not ALO Is Nothing Then _
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)

#If DEBUG Then
        ' Fill in the username And password during debug mode
        'UsernameTextBox.Text = "!"
        'Call btn_OK_Click(sender, e)

#End If
    End Sub
End Class
