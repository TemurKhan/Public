<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SyncfusionForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim MessageBoxSettings1 As Syncfusion.Windows.Forms.PdfViewer.MessageBoxSettings = New Syncfusion.Windows.Forms.PdfViewer.MessageBoxSettings()
        Dim PdfViewerPrinterSettings1 As Syncfusion.Windows.PdfViewer.PdfViewerPrinterSettings = New Syncfusion.Windows.PdfViewer.PdfViewerPrinterSettings()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SyncfusionForm))
        Dim TextSearchSettings1 As Syncfusion.Windows.Forms.PdfViewer.TextSearchSettings = New Syncfusion.Windows.Forms.PdfViewer.TextSearchSettings()
        PdfDocumentView1 = New Syncfusion.Windows.Forms.PdfViewer.PdfDocumentView()
        SuspendLayout()
        ' 
        ' PdfDocumentView1
        ' 
        PdfDocumentView1.AutoScroll = True
        PdfDocumentView1.BackColor = Color.FromArgb(CByte(237), CByte(237), CByte(237))
        PdfDocumentView1.CursorMode = Syncfusion.Windows.Forms.PdfViewer.PdfViewerCursorMode.SelectTool
        PdfDocumentView1.EnableContextMenu = True
        PdfDocumentView1.HorizontalScrollOffset = 0
        PdfDocumentView1.IsTextSearchEnabled = True
        PdfDocumentView1.IsTextSelectionEnabled = True
        PdfDocumentView1.Location = New Point(1, 2)
        MessageBoxSettings1.EnableNotification = True
        PdfDocumentView1.MessageBoxSettings = MessageBoxSettings1
        PdfDocumentView1.MinimumZoomPercentage = 50
        PdfDocumentView1.Name = "PdfDocumentView1"
        PdfDocumentView1.PageBorderThickness = 1
        PdfViewerPrinterSettings1.Copies = 1
        PdfViewerPrinterSettings1.PageOrientation = Syncfusion.Windows.PdfViewer.PdfViewerPrintOrientation.Auto
        PdfViewerPrinterSettings1.PageSize = Syncfusion.Windows.PdfViewer.PdfViewerPrintSize.ActualSize
        PdfViewerPrinterSettings1.PrintLocation = CType(resources.GetObject("PdfViewerPrinterSettings1.PrintLocation"), PointF)
        PdfViewerPrinterSettings1.ShowPrintStatusDialog = True
        PdfDocumentView1.PrinterSettings = PdfViewerPrinterSettings1
        PdfDocumentView1.ReferencePath = Nothing
        PdfDocumentView1.ScrollDisplacementValue = 0
        PdfDocumentView1.ShowHorizontalScrollBar = True
        PdfDocumentView1.ShowVerticalScrollBar = True
        PdfDocumentView1.Size = New Size(1131, 672)
        PdfDocumentView1.SpaceBetweenPages = 8
        PdfDocumentView1.TabIndex = 0
        TextSearchSettings1.CurrentInstanceColor = Color.FromArgb(CByte(127), CByte(255), CByte(171), CByte(64))
        TextSearchSettings1.HighlightAllInstance = True
        TextSearchSettings1.OtherInstanceColor = Color.FromArgb(CByte(127), CByte(254), CByte(255), CByte(0))
        PdfDocumentView1.TextSearchSettings = TextSearchSettings1
        PdfDocumentView1.ThemeName = "Default"
        PdfDocumentView1.VerticalScrollOffset = 0
        PdfDocumentView1.VisualStyle = Syncfusion.Windows.Forms.PdfViewer.VisualStyle.Default
        PdfDocumentView1.ZoomMode = Syncfusion.Windows.Forms.PdfViewer.ZoomMode.Default
        ' 
        ' SyncfusionForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1135, 686)
        Controls.Add(PdfDocumentView1)
        Name = "SyncfusionForm"
        Text = "SyncfusionForm"
        ResumeLayout(False)
    End Sub

    Friend WithEvents PdfDocumentView1 As Syncfusion.Windows.Forms.PdfViewer.PdfDocumentView
End Class
