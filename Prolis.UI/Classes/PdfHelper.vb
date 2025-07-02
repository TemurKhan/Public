Imports System.IO
'Imports iTextSharp.text
'Imports iTextSharp.text.pdf
Imports Syncfusion.Pdf.Parsing
Imports Syncfusion.Pdf
Imports PdfDocument = Syncfusion.Pdf.PdfDocument
Imports Syncfusion.Windows.Forms.PdfViewer
Imports System.Drawing.Printing
Public Class PdfHelper

    'Public Shared Function MergePDFStreamsOLD(ByVal SourcePDFFiles As List(Of Byte())) As Byte()
    '    If SourcePDFFiles.Count > 1 Then
    '        '    Dim finalPdf As PdfReader
    '        '    Dim pdfContainer As Document
    '        '    Dim pdfCopy As PdfWriter
    '        '    Dim msFinalPdf As New MemoryStream()
    '        '    'Dim msFinalPdf As Byte()
    '        '    finalPdf = New PdfReader(SourcePDFFiles(0))
    '        '    pdfContainer = New Document()
    '        '    pdfCopy = New PdfSmartCopy(pdfContainer, msFinalPdf)
    '        '    pdfContainer.Open()
    '        '    For k As Integer = 0 To SourcePDFFiles.Count - 1
    '        '        finalPdf = New PdfReader(SourcePDFFiles(k))
    '        '        For i As Integer = 1 To finalPdf.NumberOfPages
    '        '            DirectCast(pdfCopy, PdfSmartCopy).AddPage(pdfCopy.GetImportedPage(finalPdf, i))
    '        '        Next
    '        '        pdfCopy.FreeReader(finalPdf)
    '        '    Next
    '        '    finalPdf.Close()
    '        '    pdfCopy.Close()
    '        '    pdfContainer.Close()
    '        '    Return msFinalPdf.ToArray
    '        'ElseIf SourcePDFFiles.Count = 1 Then
    '        '    Return SourcePDFFiles(0)
    '        'Else
    '        Return Nothing
    '    End If
    'End Function

    'Public Shared Sub MergePDFFiles(ByVal destinationFile As String, ByVal sourceFiles As String())
    '    Try
    '        'Dim f As Integer = 0
    '        '' we create a reader for a certain document
    '        'Dim reader As New PdfReader(sourceFiles(f))
    '        '' we retrieve the total number of pages
    '        'Dim n As Integer = reader.NumberOfPages
    '        ''Console.WriteLine("There are " + n + " pages in the original file.");
    '        '' step 1: creation of a document-object
    '        'Dim document As New Document(reader.GetPageSizeWithRotation(1))
    '        '' step 2: we create a writer that listens to the document
    '        'Dim writer As PdfWriter = PdfWriter.GetInstance(document, New FileStream(destinationFile, FileMode.Create))
    '        '' step 3: we open the document
    '        'document.Open()
    '        'Dim cb As PdfContentByte = writer.DirectContent
    '        'Dim page As PdfImportedPage
    '        'Dim rotation As Integer
    '        '' step 4: we add content
    '        'While f < sourceFiles.Length
    '        '    Dim i As Integer = 0
    '        '    While i < n
    '        '        i += 1
    '        '        document.SetPageSize(reader.GetPageSizeWithRotation(i))
    '        '        document.NewPage()
    '        '        page = writer.GetImportedPage(reader, i)
    '        '        rotation = reader.GetPageRotation(i)
    '        '        If rotation = 90 OrElse rotation = 270 Then
    '        '            cb.AddTemplate(page, 0, -1.0F, 1.0F, 0, 0,
    '        '             reader.GetPageSizeWithRotation(i).Height)
    '        '        Else
    '        '            cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, 0,
    '        '             0)
    '        '            'Console.WriteLine("Processed page " + i);
    '        '        End If
    '        '    End While
    '        '    f += 1
    '        '    If f < sourceFiles.Length Then
    '        '        reader = New PdfReader(sourceFiles(f))
    '        '        ' we retrieve the total number of pages
    '        '        'Console.WriteLine("There are " + n + " pages in the original file.");
    '        '        n = reader.NumberOfPages
    '        '    End If
    '        'End While
    '        '' step 5: we close the document
    '        'document.Close()
    '    Catch e As Exception
    '        Dim strOb As String = e.Message
    '    End Try
    'End Sub

    'Public Function CountPageNo(ByVal strFileName As String) As Integer
    '    '' we create a reader for a certain document
    '    'Dim reader As New PdfReader(strFileName)
    '    '' we retrieve the total number of pages
    '    'Return reader.NumberOfPages
    'End Function

    Public Shared Function MergePDFStreams(ByVal SourcePDFFiles As List(Of Byte())) As Byte()
        ' Create a new PDF document
        'TODO: Implement the merging logic using Syncfusion or any other library
        'Using mergedDocument As New PdfDocument()
        '    ' List to hold loaded documents
        '    Dim loadedDocuments As New List(Of PdfLoadedDocument)()

        '    ' Iterate through each PDF byte array in the list
        '    For Each pdfBytes As Byte() In SourcePDFFiles
        '        ' Load the PDF from the byte array
        '        Dim stream As New MemoryStream(pdfBytes)
        '        Dim loadedDocument As New PdfLoadedDocument(stream)
        '        loadedDocuments.Add(loadedDocument) ' Keep reference to loaded document
        '        ' Append the loaded document to the merged document
        '        mergedDocument.Append(loadedDocument)
        '    Next

        '    ' Save the merged document to a memory stream
        '    Using outputStream As New MemoryStream()
        '        mergedDocument.Save(outputStream)
        '        ' Return the merged PDF as a byte array
        '        Return outputStream.ToArray()
        '    End Using
        'End Using
    End Function

    ''' <summary>
    ''' Prints a PDF document from a file path.  This method handles resource management and error handling
    ''' to ensure proper cleanup.
    ''' </summary>
    ''' <param name="filePath">The path to the PDF file to print.</param>
    ''' <param name="printerName">Optional. The name of the printer to use.  If null or empty, the default printer will be used.</param>
    ''' <param name="errorString">Optional. String to store the error message</param>
    ''' <returns>True if the print job was initiated successfully; otherwise, False.</returns>
    Public Shared Function PrintPdfDocument(ByVal filePath As String, Optional ByVal printerName As String = Nothing, Optional ByRef errorString As String = "") As Boolean

        'TODO: Code till End Try is commented out.  Uncomment and implement the printing logic using Syncfusion or any other library

        'Dim PDFDOC As PdfLoadedDocument = Nothing
        'Dim pdfViewer As PdfDocumentView = Nothing

        'Try
        '    ' 1. Load the PDF document from file.
        '    PDFDOC = New PdfLoadedDocument(filePath)

        '    ' 2. Create a PdfDocumentView instance.
        '    pdfViewer = New PdfDocumentView()

        '    ' 3. Assign the loaded document to the PdfDocumentView.
        '    pdfViewer.Load(PDFDOC)

        '    ' 4. Configure PrinterSettings.
        '    Dim printerSettings As New PrinterSettings()

        '    If Not String.IsNullOrEmpty(printerName) Then
        '        Try
        '            printerSettings.PrinterName = printerName  ' Attempt to set specified printer

        '            ' Check if printer exists in installed printers
        '            Dim printerFound As Boolean = False
        '            For Each installedPrinter As String In PrinterSettings.InstalledPrinters
        '                If installedPrinter = printerName Then
        '                    printerFound = True
        '                    Exit For
        '                End If
        '            Next

        '            If Not printerFound Then
        '                errorString = "Invalid printer specified: " & printerName & ". Printer not found in installed printers."
        '                Return False
        '            End If

        '        Catch ex As Exception
        '            errorString = "Error setting printer: " & ex.Message
        '            Return False
        '        End Try
        '    End If

        '    ' Create a PrintDocument and associate the PrinterSettings (REQUIRED for PdfViewer.Print to work correctly).
        '    Dim printDocument As New PrintDocument()
        '    printDocument.PrinterSettings = printerSettings

        '    ' 5. Print the PDF document.
        '    pdfViewer.PrintDocument.Print()

        '    Return True ' Indicate successful print initiation

        'Catch ex As Exception
        '    errorString = "Error printing PDF: " & ex.Message
        '    Return False ' Indicate failure

        'Finally
        '    ' 6. Release Resources.  This is critical.
        '    If PDFDOC IsNot Nothing Then
        '        Try
        '            PDFDOC.Close() ' Or False, depending on if you want to save changes
        '        Catch ex As Exception
        '            ' Log or handle the PDFDOC.Close() exception.  If the PDF is corrupt or locked, this might fail.
        '            If String.IsNullOrEmpty(errorString) Then
        '                errorString = "Error closing PDF document: " & ex.Message
        '            Else
        '                errorString += vbCrLf + "Error closing PDF document: " & ex.Message
        '            End If
        '        Finally
        '            PDFDOC.Dispose() ' Ensure disposal even if close fails
        '        End Try
        '    End If

        '    If pdfViewer IsNot Nothing Then
        '        pdfViewer.Dispose()
        '    End If

        'End Try

    End Function

    'Public Shared Function MergePDFStreams1(ByVal SourcePDFFiles As List(Of Byte())) As Byte()
    '    ' Create a new PDF document for merging
    '    Dim mergedDocument As New PdfDocument()

    '    ' Merge all PDFs from memory streams in SPDFS
    '    For Each pdfBytes As Byte() In SourcePDFFiles
    '        Using pdfStream As New MemoryStream(pdfBytes)
    '            Dim loadedDocument As PdfLoadedDocument = New PdfLoadedDocument(pdfStream)
    '            mergedDocument.ImportPageRange(loadedDocument, 0, loadedDocument.Pages.Count - 1)
    '            loadedDocument.Close(True) ' Close the loaded document after importing
    '        End Using
    '    Next

    '    ' Save the merged PDF to a memory stream and return as byte array
    '    Using outputStream As New MemoryStream()
    '        mergedDocument.Save(outputStream)
    '        mergedDocument.Close(True)
    '        Return outputStream.ToArray()
    '    End Using

    '    '' Save the merged PDF to a file
    '    'Dim outputStream As New MemoryStream()
    '    'mergedDocument.Save(outputStream)
    '    'Return outputStream.ToArray

    '    '' Get the temporary folder path
    '    'Dim FolderPath As String = GetTempFolder()

    '    '' Delete all files in the folder
    '    'For Each fileToDelete As String In Directory.GetFiles(FolderPath, "*.*", SearchOption.AllDirectories)
    '    '    Try
    '    '        File.Delete(fileToDelete)
    '    '    Catch ex As Exception
    '    '        ' Log or handle any exception if a file cannot be deleted
    '    '    End Try
    '    'Next

    '    '' Define the output PDF file path
    '    'Dim outputFilePath As String = Path.Combine(FolderPath, AccID & ".PDF")

    '    '' Save the merged PDF document to the file
    '    'Using outputStream As New FileStream(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.Read)
    '    '    mergedDocument.Save(outputStream)
    '    'End Using

    '    '' Close and dispose of the merged document
    '    'mergedDocument.Close(True)

    '    'Return outputFilePath
    'End Function


End Class

