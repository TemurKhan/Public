Imports System.Data.Odbc
Imports System.IO
Imports System.Net
Imports Microsoft.Data.SqlClient

Module ProlisUpdaterCheck

    ' FTP Credentials
    Private ftpServer As String = ""
    Private ftpUsername As String = ""
    Private ftpPassword As String = ""

    ' FTP Path for AutoUpdater
    ' Private updaterFtpPath As String = "/Labs/Prolis_AutoUpdater/Prolis_AutoUpdater.exe"
    Private updaterFtpPath_Starter As String = "/Labs/Prolis_AutoUpdater/Prolis_Updates_Starter.exe"

    ' Local Paths
    Private localAppPath As String = AppDomain.CurrentDomain.BaseDirectory
    ' Private localUpdaterPath_updater As String = Path.Combine(localAppPath, "Prolis_AutoUpdater.exe")
    Private localUpdaterPath_starter As String = Path.Combine(localAppPath, "Prolis_Updates_Starter.exe")

    ' Define the StarLab folder name

    Public Class FTPConfig
        Public Property FTPServer As String
        Public Property FTPUsername As String
        Public Property FTPPassword As String
        Public Property StarLabFolder As String
        Public Property LocalAppPath As String
        Public Property LabName As String
    End Class
    Public Function GetFTPSettings0() As FTPConfig
        Dim dsn As String = connString
        Dim query As String = "SELECT TOP 1 FTP_Server, FTP_Username, FTP_Password, StarLabFolder, LocalAppPath FROM FTP_Settings"

        Dim ftpConfig As New FTPConfig()

        Try

            'Using conn As New OdbcConnection(dsn)
            '    conn.Open()
            '    Using cmd As New OdbcCommand(query, conn)
            '        Using reader As OdbcDataReader = cmd.ExecuteReader()
            '            If reader.Read() Then
            '                ftpConfig.FTPServer = reader("FTP_Server").ToString()
            '                ftpConfig.FTPUsername = reader("FTP_Username").ToString()
            '                ftpConfig.FTPPassword = reader("FTP_Password").ToString()
            '                ftpConfig.StarLabFolder = reader("StarLabFolder").ToString()
            '                ftpConfig.LocalAppPath = reader("LocalAppPath").ToString()
            '            End If
            '        End Using
            '    End Using
            'End Using
        Catch ex As Exception
        End Try
        'query = "select  REPLACE(LastName_BSN,' ','_') as LabName  from Company "
        'Try
        '    Using conn As New OdbcConnection(dsn)
        '        conn.Open()
        '        Using cmd As New OdbcCommand(query, conn)
        '            Using reader As OdbcDataReader = cmd.ExecuteReader()
        '                If reader.Read() Then
        '                    ftpConfig.LabName = reader("LabName").ToString()

        '                End If
        '            End Using
        '        End Using
        '    End Using
        'Catch ex As Exception
        'End Try
        Return ftpConfig
    End Function


    Public Function GetFTPSettings() As FTPConfig
        Dim query As String = "SELECT TOP 1 FTP_Server, FTP_Username, FTP_Password, StarLabFolder, LocalAppPath FROM FTP_Settings"
        Dim ftpConfig As New FTPConfig()

        Try
            Using conn As New SqlConnection(connString) ' Use SqlConnection instead of OdbcConnection
                conn.Open()

                Using cmd As New SqlCommand(query, conn) ' Use SqlCommand instead of OdbcCommand
                    Using reader As SqlDataReader = cmd.ExecuteReader() ' Use SqlDataReader instead of OdbcDataReader
                        If reader.Read() Then
                            ftpConfig.FTPServer = reader("FTP_Server").ToString()
                            ftpConfig.FTPUsername = reader("FTP_Username").ToString()
                            ftpConfig.FTPPassword = reader("FTP_Password").ToString()
                            ftpConfig.StarLabFolder = reader("StarLabFolder").ToString()
                            ftpConfig.LocalAppPath = reader("LocalAppPath").ToString()
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Handle the exception (e.g., logging the error)
            Console.WriteLine("Error: " & ex.Message)
        End Try

        Return ftpConfig
    End Function


    Public Sub EnsureUpdaterExists()
        Try

            Dim ft As FTPConfig = New FTPConfig()

            If ft.FTPUsername Is Nothing OrElse ft.FTPUsername = "" Then
                ft = GetFTPSettings()

            End If
            ftpServer = ft.FTPServer
            ftpUsername = ft.FTPUsername
            ftpPassword = ft.FTPPassword
            localAppPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)

            Console.WriteLine("Ensuring Updater is fresh and running...")


            KillProcess("Prolis_Updates_Starter")

            ' Always download the latest updater
            Console.WriteLine("Downloading the latest Prolis_AutoUpdater.exe...")
            DownloadFileFromFTP(updaterFtpPath_Starter, localUpdaterPath_starter)

            Console.WriteLine("Updater downloaded successfully.")


            ' Prepare to start updater as an independent process
            Dim psi As New ProcessStartInfo()
            psi.FileName = localUpdaterPath_starter
            ' Start the updater independently
            Process.Start(psi)


        Catch ex As Exception
            Console.WriteLine("Error in EnsureUpdaterExists: " & ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Kills a process by name if running.
    ''' </summary>
    Private Sub KillProcess(processName As String)
        Try
            For Each p As Process In Process.GetProcessesByName(processName)
                p.Kill()
                p.WaitForExit()
                Console.WriteLine($"Killed process: {processName}")
            Next
        Catch ex As Exception
            Console.WriteLine($"Error killing process {processName}: {ex.Message}")
        End Try
    End Sub


    ''' <summary>
    ''' Downloads a file from FTP
    ''' </summary>
    Private Sub DownloadFileFromFTP(ftpPath As String, localPath As String)
        Dim request As FtpWebRequest = CType(WebRequest.Create(ftpServer & ftpPath), FtpWebRequest)
        request.Credentials = New NetworkCredential(ftpUsername, ftpPassword)
        request.Method = WebRequestMethods.Ftp.DownloadFile

        Using response As FtpWebResponse = CType(request.GetResponse(), FtpWebResponse)
            Using stream As Stream = response.GetResponseStream()
                Using fileStream As New FileStream(localPath, FileMode.Create)
                    stream.CopyTo(fileStream)
                End Using
            End Using
        End Using
    End Sub

End Module
