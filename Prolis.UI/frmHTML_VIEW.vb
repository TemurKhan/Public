'''spire
Imports System.IO

Public Class frmHTML_VIEW

    Public Shadows ERA_path_or_content As String = ""




    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If ERA_path_or_content <> "" Then
            Dim htmlReport As String = ""
            ' htmlReport = parser.GenerateHTMLReport(templatePath, eraDataPath)
            Me.KeyPreview = True

            Dim htmlOutput As String = ""

            Dim processor As New ERAProcessor()
            htmlOutput = processor.ParseERAFile(ERA_path_or_content)
            If Not htmlOutput = "" Then
                WebBrowser1.DocumentText = htmlOutput
            Else
                Me.Close()
            End If
        End If


    End Sub

    Private Sub HTML_VIEW_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            ' Print the current content of the WebBrowser control
            WebBrowser1.Print()
        End If
    End Sub

    Private Sub WebBrowser1_ParentChanged(sender As Object, e As EventArgs) Handles WebBrowser1.ParentChanged

        ' Print the current content of the WebBrowser control
        WebBrowser1.Print()

    End Sub
End Class