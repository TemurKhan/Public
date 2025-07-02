Imports Syncfusion.Windows.Forms.PdfViewer

Public Class SyncfusionForm
    Public Shared path As String = "C:\Path\To\Your\PDFFile.pdf"
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load a PDF file when the form loads
        Dim pdf1 As PdfViewerControl = New PdfViewerControl()
        pdf1.Load(path)
    End Sub
End Class