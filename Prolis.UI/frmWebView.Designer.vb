<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmWebView
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim MessageBoxSettings1 As Syncfusion.Windows.Forms.PdfViewer.MessageBoxSettings = New Syncfusion.Windows.Forms.PdfViewer.MessageBoxSettings()
        Dim PdfViewerPrinterSettings1 As Syncfusion.Windows.PdfViewer.PdfViewerPrinterSettings = New Syncfusion.Windows.PdfViewer.PdfViewerPrinterSettings()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWebView))
        Dim TextSearchSettings1 As Syncfusion.Windows.Forms.PdfViewer.TextSearchSettings = New Syncfusion.Windows.Forms.PdfViewer.TextSearchSettings()
        PdfViewerControl1 = New Syncfusion.Windows.Forms.PdfViewer.PdfViewerControl()
        SuspendLayout()
        ' 
        ' PdfViewerControl1
        ' 
        PdfViewerControl1.CursorMode = Syncfusion.Windows.Forms.PdfViewer.PdfViewerCursorMode.SelectTool
        PdfViewerControl1.Dock = DockStyle.Fill
        PdfViewerControl1.EnableContextMenu = True
        PdfViewerControl1.EnableNotificationBar = True
        PdfViewerControl1.HorizontalScrollOffset = 0
        PdfViewerControl1.IsBookmarkEnabled = True
        PdfViewerControl1.IsTextSearchEnabled = True
        PdfViewerControl1.IsTextSelectionEnabled = True
        PdfViewerControl1.Location = New Point(0, 0)
        MessageBoxSettings1.EnableNotification = True
        PdfViewerControl1.MessageBoxSettings = MessageBoxSettings1
        PdfViewerControl1.MinimumZoomPercentage = 50
        PdfViewerControl1.Name = "PdfViewerControl1"
        PdfViewerControl1.PageBorderThickness = 1
        PdfViewerPrinterSettings1.Copies = 1
        PdfViewerPrinterSettings1.PageOrientation = Syncfusion.Windows.PdfViewer.PdfViewerPrintOrientation.Auto
        PdfViewerPrinterSettings1.PageSize = Syncfusion.Windows.PdfViewer.PdfViewerPrintSize.ActualSize
        PdfViewerPrinterSettings1.PrintLocation = CType(resources.GetObject("PdfViewerPrinterSettings1.PrintLocation"), PointF)
        PdfViewerPrinterSettings1.ShowPrintStatusDialog = True
        PdfViewerControl1.PrinterSettings = PdfViewerPrinterSettings1
        PdfViewerControl1.ReferencePath = Nothing
        PdfViewerControl1.ScrollDisplacementValue = 0
        PdfViewerControl1.ShowHorizontalScrollBar = True
        PdfViewerControl1.ShowToolBar = True
        PdfViewerControl1.ShowVerticalScrollBar = True
        PdfViewerControl1.Size = New Size(1225, 713)
        PdfViewerControl1.SpaceBetweenPages = 8
        PdfViewerControl1.TabIndex = 1
        PdfViewerControl1.Text = "PdfViewerControl1"
        TextSearchSettings1.CurrentInstanceColor = Color.FromArgb(CByte(127), CByte(255), CByte(171), CByte(64))
        TextSearchSettings1.HighlightAllInstance = True
        TextSearchSettings1.OtherInstanceColor = Color.FromArgb(CByte(127), CByte(254), CByte(255), CByte(0))
        PdfViewerControl1.TextSearchSettings = TextSearchSettings1
        PdfViewerControl1.ThemeName = "Default"
        PdfViewerControl1.VerticalScrollOffset = 0
        PdfViewerControl1.VisualStyle = Syncfusion.Windows.Forms.PdfViewer.VisualStyle.Default
        PdfViewerControl1.ZoomMode = Syncfusion.Windows.Forms.PdfViewer.ZoomMode.FitWidth
        ' 
        ' frmWebView
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1225, 713)
        Controls.Add(PdfViewerControl1)
        Margin = New Padding(4, 5, 4, 5)
        Name = "frmWebView"
        Text = "Report"
        ResumeLayout(False)

    End Sub
    Friend WithEvents PdfViewerControl1 As Syncfusion.Windows.Forms.PdfViewer.PdfViewerControl
End Class
