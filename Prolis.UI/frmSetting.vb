Imports System.Windows.Forms
Imports LicenseManager
Imports Microsoft.Win32
Imports Prolis.BLL
Imports Prolis.Utils

Public Class frmSetting
    Private IsDirty As Boolean

    Private Sub frmSetting_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Trim(txtDatabase.Text) <> "" And Trim(txtUserID.Text) <> "" And
        Trim(txtPassword.Text) <> "" And Trim(txtProlisServer.Text) <> "" Then
            Try
                ' Get the user input
                Dim server = txtProlisServer.Text
                Dim database = txtDatabase.Text
                Dim username = txtUserID.Text
                Dim password = txtPassword.Text

                ' Create instance of ConnectionManager from BLL
                Dim connectionManager As New ConnectionManagerBLL()

                Try
                    Dim connStr As String = $"Server={server};Database={database};User Id={username};Password={password};TrustServerCertificate=True;MultipleActiveResultSets=True;"

                    ' Call the BLL method to validate, encrypt and save the connection string
                    connectionManager.EncryptAndSaveConnectionString(connStr)

                    My.Settings.ProlisServer = txtProlisServer.Text
                    My.Settings.Database = txtDatabase.Text
                    My.Settings.UID = CryptoHelper.Encrypt(txtUserID.Text)
                    My.Settings.PWD = CryptoHelper.Encrypt(txtPassword.Text)
                    My.Settings.Save()

                    ' Save the connection string to the MAIN settings
                    connString = connStr

                    Me.DialogResult = DialogResult.OK
                    '    MessageBox.Show("Connection string saved and encrypted successfully!")
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Prolis", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ' Show any error (like failed connection)
                    e.Cancel = True
                End Try

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Prolis", MessageBoxButtons.OK, MessageBoxIcon.Error)


                ' txtProlisServer.Text = ""
                e.Cancel = True
            End Try
        Else
            MsgBox("All RED color labeld fields must be filled.", MsgBoxStyle.Critical, "Prolis")
            e.Cancel = True
        End If
    End Sub

    Private Sub frmSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadSetting()
    End Sub

    Private Sub LoadSetting()
        If My.Settings.ProlisServer <> "" And My.Settings.Database <> "" _
            And My.Settings.UID <> "" And My.Settings.PWD <> "" Then

            txtUserID.Text = CryptoHelper.Decrypt(My.Settings.UID)
        txtPassword.Text = CryptoHelper.Decrypt(My.Settings.PWD)
        txtDatabase.Text = My.Settings.Database
        txtProlisServer.Text = My.Settings.ProlisServer

        End If
        'Dim conManager As New ConnectionManagerBLL()

        'Dim connString As String = conManager.DecryptAndLoadConnectionString()

        'If Not String.IsNullOrWhiteSpace(connString) Then
        '    LoadConnectionStringToForm(connString)

        'End If


    End Sub

    Public Sub LoadConnectionStringToForm(connStr As String)
        Dim parts = connStr.Split(";"c)

        For Each part In parts
            If String.IsNullOrWhiteSpace(part) Then Continue For

            Dim keyValue = part.Split("="c, 2)
            If keyValue.Length = 2 Then
                Dim key = keyValue(0).Trim().ToLower()
                Dim value = keyValue(1).Trim()

                Select Case key
                    Case "server", "data source"
                        txtProlisServer.Text = value
                    Case "database", "initial catalog"
                        txtDatabase.Text = value
                    Case "user id", "uid"
                        txtUserID.Text = value
                    Case "password", "pwd"
                        txtPassword.Text = value
                        ' Optional: handle TrustServerCertificate, Encrypt, etc., if needed
                End Select
            End If
        Next
    End Sub

End Class
