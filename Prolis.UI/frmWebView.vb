
'Imports iTextSharp.text
'Imports iTextSharp.text.pdf
Imports System.IO
Imports System.Net.NetworkInformation
Imports Syncfusion.Pdf
Imports Syncfusion.Pdf.Graphics
Imports Syncfusion.Windows.Forms.PdfViewer
Public Class frmWebView

    'Sub New(fp As String)
    '    ' TODO: Complete member initialization 
    '    InitializeComponent()
    '    filePath = fp
    'End Sub

    Sub New()
        ' TODO: Complete member initialization 
        InitializeComponent()

    End Sub

    Public AccIDs() As String
    'Sub New(ByVal IDs() As String)
    '    ' TODO: Complete member initialization 
    '    InitializeComponent()
    '    AccIDs = IDs
    'End Sub

    Private Sub frmWebView_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Hook into the ToolbarItemClicked event

        AddHandler PdfViewerControl1.BeginPrint, AddressOf PrintButton_Click

        PdfViewerControl1.CursorMode = Syncfusion.Windows.Forms.PdfViewer.PdfViewerCursorMode.HandTool


    End Sub

    Private Sub PrintButton_Click(sender As Object, e As BeginPrintEventArgs)
        'MessageBox.Show("Custom print logic triggered!")  'working 

        If AccIDs.Length > 0 Then

            For Each AccID As String In AccIDs
                Dim RPTStatus = IIf(ReportFullResulted(AccID) = True, "FINAL", "PARTIAL")
                LogEventAndAuditTrail(AccID, 11, RPTStatus)
            Next

        End If

    End Sub


    Public Sub ShowFromPath(pdfPath As String)

        'PdfViewer1.Document = PdfiumViewer.PdfDocument.Load(pdfPath)
        PdfViewerControl1.Load(pdfPath)
    End Sub
    Public Sub ShowFromStream(imageData As Byte())
        Dim pdfStream As New MemoryStream(imageData)
        'PdfViewer1.Document = PdfiumViewer.PdfDocument.Load(pdfStream)
        PdfViewerControl1.Load(pdfStream)
    End Sub

    Function ConvertImageToPdf(imageData As Byte()) As String
        Try
            Dim directoryPath = Path.GetTempPath()

            ' Get all files in the directory with "tempImage" in their names
            Dim filesToDelete As String() = Directory.GetFiles(directoryPath, "*tempImage*")

            ' Loop through and delete each file
            For Each filePath As String In filesToDelete
                File.Delete(filePath)
            Next

        Catch ex As Exception
            ' Handle exceptions as needed
        End Try

        Dim pdfPath As String = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() & "tempImage.pdf")

        ' Create a new PDF document
        Using document As New PdfDocument()
            ' Add a page to the document
            Dim page As PdfPage = document.Pages.Add()

            ' Load the image from the binary data
            Using imageStream As New MemoryStream(imageData)
                Dim image As PdfBitmap = New PdfBitmap(imageStream)

                ' Draw the image on the page
                Dim width As Double = page.GetClientSize().Width - 20
                Dim height As Double = page.GetClientSize().Height - 20
                page.Graphics.DrawImage(image, New RectangleF(10, 10, width, height))
            End Using

            ' Save the document to the specified path
            document.Save(pdfPath)
        End Using

        Return pdfPath
    End Function

    ' Function to convert image data to a temporary PDF file
    'Function ConvertImageToPdfOLD(imageData As Byte()) As String
    '    'Try
    '    '    Dim directoryPath = Path.GetTempPath()

    '    '    ' Get all files in the directory with "tempImage" in their names
    '    '    Dim filesToDelete As String() = Directory.GetFiles(directoryPath, "*tempImage*")

    '    '    ' Loop through and delete each file
    '    '    For Each filePath As String In filesToDelete
    '    '        File.Delete(filePath)
    '    '    Next


    '    'Catch ex As Exception

    '    'End Try
    '    'Dim pdfPath As String = Path.Combine(Path.GetTempPath(), Guid.NewGuid.ToString() & "tempImage.pdf")

    '    '' Create a new PDF document
    '    'Using document As New Document()
    '    '    PdfWriter.GetInstance(document, New FileStream(pdfPath, FileMode.Create))
    '    '    document.Open()

    '    '    ' Load the image from the binary data
    '    '    Dim img As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(imageData)
    '    '    img.Alignment = Element.ALIGN_CENTER
    '    '    img.ScaleToFit(document.PageSize.Width - 20, document.PageSize.Height - 20)

    '    '    ' Add the image to the document
    '    '    document.Add(img)
    '    '    document.Close()
    '    'End Using

    '    'Return pdfPath
    'End Function

    ' Code to load and display the image as a PDF
    Sub DisplayImageAsPdf(imageData As Byte())
        ' Convert the image to PDF and get the file path
        Dim pdfPath As String = ConvertImageToPdf(imageData)
        'PdfViewer1.Document = PdfiumViewer.PdfDocument.Load(pdfPath)
        PdfViewerControl1.Load(pdfPath)


    End Sub
    Private Sub LoadDocument(fileData As Byte())
        ' Connection and query setup


        ' Determine file type
        Dim fileType As String = GetFileType(fileData)

        ' Display the file
        If fileType = "PDF" Then

            ' Display PDF
            Dim pdfStream As New MemoryStream(fileData)
            'PdfViewer1.Document = PdfiumViewer.PdfDocument.Load(pdfStream)
            PdfViewerControl1.Load(pdfStream)
        ElseIf fileType = "JPEG" OrElse fileType = "PNG" OrElse fileType = "GIF" Then

            DisplayImageAsPdf((fileData))
        Else
            MessageBox.Show("Unsupported file type.")
        End If
    End Sub

    ' Function to determine file type
    Private Function GetFileType(fileData As Byte()) As String
        If fileData.Length > 4 Then
            ' Check for PDF header
            Dim pdfHeader As String = System.Text.Encoding.ASCII.GetString(fileData, 0, 4)
            If pdfHeader = "%PDF" Then
                Return "PDF"
            End If

            ' Check for JPEG header
            If fileData(0) = &HFF AndAlso fileData(1) = &HD8 Then
                Return "JPEG"
            End If

            ' Check for PNG header
            If fileData(0) = &H89 AndAlso fileData(1) = &H50 Then
                Return "PNG"
            End If

            ' Check for GIF header
            If fileData(0) = &H47 AndAlso fileData(1) = &H49 Then
                Return "GIF"
            End If
        End If
        Return "Unknown"
    End Function

    Public Async Sub LoadPdfData(pdfData As Byte())
        LoadDocument(pdfData)
    End Sub
    Private Function SavePdfWithRandomName(base64String As String) As String
        Try
            Dim pdfBytes() As Byte = Convert.FromBase64String(base64String)
            Dim folderPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyPdfFiles")
            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If
            Dim randomFileName As String = Path.GetRandomFileName().Replace(".", "") & ".pdf"

            Dim pdfFilePath As String = Path.Combine(folderPath, randomFileName)
            File.WriteAllBytes(pdfFilePath, pdfBytes)

            Return pdfFilePath
        Catch ex As Exception
            MessageBox.Show("Error saving PDF: " & ex.Message)
            Return Nothing
        End Try
    End Function


End Class