Imports System.Drawing.Printing
Imports System.IO
Imports System.Security.Policy
Imports System.Diagnostics
Imports Microsoft.Win32

Public Class frmWebViewTesting
    Private MyUrl As String
    Public Sub New(Optional url As String = Nothing)
        InitializeComponent()
        MyUrl = url
        'InitializeBrowser(url).GetAwaiter().GetResult()

    End Sub



    Private Sub PrintPDFWithAdobe(filePath As String)
        Try
            ' Ensure the file exists
            If Not IO.File.Exists(filePath) Then
                MessageBox.Show("File not found: " & filePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Get Adobe Reader path programmatically
            Dim adobePath As String = GetAdobeReaderPath()
            If String.IsNullOrEmpty(adobePath) Then
                MessageBox.Show("Adobe Acrobat Reader not found on this system.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Start the process to print the PDF
            Dim printProcess As New Process()
            With printProcess.StartInfo
                .FileName = adobePath
                .Arguments = $"/t ""{filePath}""" ' Print silently
                .CreateNoWindow = True
                .WindowStyle = ProcessWindowStyle.Hidden ' Ensure the window is hidden
                .UseShellExecute = False
            End With

            printProcess.Start()
            printProcess.WaitForExit()

            MessageBox.Show("Printing completed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error printing PDF: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Function GetAdobeReaderPath() As String
        Try
            ' Check 64-bit and 32-bit registry paths
            Dim registryKeys As String() = {
            "SOFTWARE\Adobe\Acrobat Reader",
            "SOFTWARE\WOW6432Node\Adobe\Acrobat Reader"
        }

            For Each keyPath In registryKeys
                Using key As RegistryKey = Registry.LocalMachine.OpenSubKey(keyPath)
                    If key IsNot Nothing Then
                        ' Get the version subkey
                        Dim versions = key.GetSubKeyNames()
                        If versions.Length > 0 Then
                            Dim latestVersion = versions.Max() ' Get the latest version
                            Using versionKey As RegistryKey = key.OpenSubKey(latestVersion & "\InstallPath")
                                If versionKey IsNot Nothing Then
                                    Dim path As String = versionKey.GetValue("")?.ToString()
                                    If Not String.IsNullOrEmpty(path) Then
                                        Return IO.Path.Combine(path, "AcroRd32.exe") ' Add executable name
                                    End If
                                End If
                            End Using
                        End If
                    End If
                End Using
            Next

            ' Hardcoded fallback paths for Adobe Reader
            Dim fallbackPaths As String() = {
            "C:\Program Files (x86)\Adobe\Acrobat Reader\Reader\AcroRd32.exe",
            "C:\Program Files\Adobe\Acrobat Reader\Reader\AcroRd32.exe",
            "C:\Program Files\Adobe\Acrobat DC\Acrobat\Acrobat.exe"
        }

            ' Check fallback paths
            For Each fallbackPath In fallbackPaths
                If IO.File.Exists(fallbackPath) Then
                    Return fallbackPath
                End If
            Next

            ' Return empty if not found
            Return String.Empty
        Catch ex As Exception
            MessageBox.Show("Error finding Adobe Reader path: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return String.Empty
        End Try
    End Function


    Private Sub PrintPDF(filePath As String)
        Try
            ' Ensure the file exists
            If Not IO.File.Exists(filePath) Then
                MessageBox.Show("File not found: " & filePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Set up the process to use the "PrintTo" verb
            Dim printProcess As New Process()
            With printProcess.StartInfo
                .FileName = filePath ' Path to the PDF file
                .Verb = "PrintTo"    ' Use PrintTo verb to print
                .CreateNoWindow = True
                .WindowStyle = ProcessWindowStyle.Hidden
                .UseShellExecute = True
            End With

            ' Start the process
            printProcess.Start()
            printProcess.WaitForExit() ' Optional: Wait for the printing process to complete

            MessageBox.Show("Printing completed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error printing PDF: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PrintPDFWithEdge(filePath As String)
        Try
            ' Ensure the file exists
            If Not IO.File.Exists(filePath) Then
                MessageBox.Show("File not found: " & filePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Define the possible paths for Microsoft Edge
            Dim edgePaths As String() = {
            "C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe",
            "C:\Program Files\Microsoft\Edge\Application\msedge.exe"
        }

            ' Find the installed path of Microsoft Edge
            Dim edgePath As String = edgePaths.FirstOrDefault(Function(p) IO.File.Exists(p))
            If String.IsNullOrEmpty(edgePath) Then
                MessageBox.Show("Microsoft Edge not found on this system.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Start the process to print the PDF silently
            Dim printProcess As New Process()
            With printProcess.StartInfo
                .FileName = edgePath
                .Arguments = $"--kiosk --kiosk-printing ""{filePath}"""
                .CreateNoWindow = True
                .WindowStyle = ProcessWindowStyle.Hidden
                .UseShellExecute = False
            End With

            printProcess.Start()
            printProcess.WaitForExit() ' Wait for the printing process to complete

            MessageBox.Show("Printing completed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error printing PDF: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class