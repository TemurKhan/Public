Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks ' Required for async operations
Imports System.Windows.Forms
Imports DymoSDK.Connect ' Namespace for the Connect SDK
Imports DymoSDK ' <--- Add or ensure this line is present

Public Class frmDYMO_Testing2 ' Or your relevant Form class

    ' --- Add necessary Imports at the top ---

    ' --- Button Click Handler to Trigger Printing ---
    'Private Async Sub btnTestDymoConnect_Click(sender As Object, e As EventArgs) '  Handles btnTestDymoConnect.Click
    '    ' --- 1. Get Data ---
    '    Dim myLabelData As New Dictionary(Of String, String) From {
    '        {"Name", "Alice Connect (VB)"},      ' Key MUST match object name in label file
    '        {"Address", "789 Service Lane"},     ' Key MUST match object name in label file
    '        {"CSZ", "Connectville, Svc 10001"}, ' Key MUST match object name in label file
    '        {"Zip", "10001"}
    '        }
    '    ' Key MUST match object name in label file
    '    ' Add more key/value pairs for other fields


    '    ' --- 2. Get Preferred Label File Name ---
    '    Dim labelFileNameToUse As String = "CustomerAddress.label" ' Example

    '    ' --- 3. Get Printer Name ---
    '    ' IMPORTANT: Get this from user selection or configuration.
    '    ' The Connect SDK might provide ways to list printers *via the service*.
    '    Dim selectedPrinter As String = "DYMO LabelWriter 450" ' << Use a name for testing

    '    ' --- 4. Get Quantity ---
    '    Dim quantity As Integer = 1

    '    ' --- 5. Call the Async Printing Method ---
    '    MessageBox.Show("Attempting to print using DYMO.Connect.SDK...", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    Await PrintDymoLabelConnectSDK(labelFileNameToUse, myLabelData, selectedPrinter, quantity)
    '    MessageBox.Show("Dymo Connect SDK function executed (check console/output window for details).",
    '                    "Test Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
    'End Sub


    '''' <summary>
    '''' Prints a Dymo label using the DYMO.Connect.SDK. Requires Dymo Connect software to be running.
    '''' </summary>
    '''' <param name="preferredLabelFileName">The name of the primary .label file (e.g., "MySpecificLabel.label").</param>
    '''' <param name="labelDataMap">Dictionary where Key is the object name in the label file, Value is the data.</param>
    '''' <param name="printerName">The exact name of the target Dymo printer.</param>
    '''' <param name="qty">The number of labels to print.</param>
    'Public Async Function PrintDymoLabelConnectSDK(ByVal preferredLabelFileName As String, ByVal labelDataMap As Dictionary(Of String, String), ByVal printerName As String, ByVal qty As Integer) As Task

    '    ' --- Input Validation ---
    '    If labelDataMap Is Nothing OrElse Not labelDataMap.Any() Then
    '        MessageBox.Show("No label data provided.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        Return
    '    End If
    '    If String.IsNullOrWhiteSpace(printerName) Then
    '        MessageBox.Show("No printer name specified.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        Return
    '    End If
    '    If qty <= 0 Then
    '        MessageBox.Show("Quantity must be greater than zero.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        Return
    '    End If

    '    ' --- Determine Label File Path ---
    '    Dim reportsPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports")
    '    Dim primaryLabelPath As String = Path.Combine(reportsPath, preferredLabelFileName)
    '    Dim fallbackLabelPath As String = Path.Combine(reportsPath, "Dymo30336Adr.Label") ' Your fallback
    '    Dim labelPathToUse As String = Nothing

    '    If File.Exists(primaryLabelPath) Then
    '        labelPathToUse = primaryLabelPath
    '    ElseIf File.Exists(fallbackLabelPath) Then
    '        labelPathToUse = fallbackLabelPath
    '        Console.WriteLine($"WARNING: Primary label '{preferredLabelFileName}' not found. Using fallback: {fallbackLabelPath}")
    '    Else
    '        MessageBox.Show($"Dymo Label file could not be found.{vbCrLf}Checked paths:{vbCrLf}- {primaryLabelPath}{vbCrLf}- {fallbackLabelPath}",
    '                        "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return
    '    End If

    '    ' --- Interact with Dymo Connect Service ---
    '    Try
    '        Console.WriteLine("Checking Dymo Connect Service status...")
    '        ' Check if the Dymo Connect Service is available
    '        Dim isServiceAvailable As Boolean = Await DymoService.IsServiceAvailableAsync()

    '        If Not isServiceAvailable Then
    '            MessageBox.Show("The DYMO Connect Service is not running or could not be reached." & vbCrLf &
    '                            "Please ensure the Dymo Connect software is installed and running.",
    '                            "DYMO Service Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            Console.WriteLine("DYMO Connect Service is not available.")
    '            Return
    '        End If
    '        Console.WriteLine("Dymo Connect Service is available.")

    '        ' --- Load Label Content ---
    '        ' Read the label file XML content as a string
    '        Dim labelXml As String = File.ReadAllText(labelPathToUse)
    '        Console.WriteLine($"Label file content loaded from: {labelPathToUse}")

    '        ' --- Prepare Print Parameters ---
    '        Dim printParams As New PrintParameters()
    '        For Each kvp In labelDataMap
    '            printParams.AddLabelObjectData(kvp.Key, kvp.Value)
    '            Console.WriteLine($"Adding data for field '{kvp.Key}'")
    '        Next
    '        printParams.Copies = qty
    '        Console.WriteLine($"Print parameters prepared for {qty} copies.")


    '        ' --- Check Printer (Simulated/Real) ---
    '        ' Get printers *via the service*
    '        Console.WriteLine("Getting printer list from Dymo Connect Service...")
    '        Dim printers = Await DymoService.GetPrintersAsync()
    '        Dim targetPrinterInfo = printers?.FirstOrDefault(Function(p) p.DisplayName.Equals(printerName, StringComparison.OrdinalIgnoreCase) OrElse
    '                                                                  p.Name.Equals(printerName, StringComparison.OrdinalIgnoreCase)) ' Check both names

    '        If targetPrinterInfo Is Nothing Then
    '            Dim availablePrintersMsg As String = If(printers?.Any(),
    '                                                   String.Join(vbCrLf & " - ", printers.Select(Function(p) $"{p.DisplayName} ({p.ModelName})")),
    '                                                   "None detected by the service.")
    '            MessageBox.Show($"Printer Check: The specified Dymo printer '{printerName}' was not found by the DYMO Connect Service.{vbCrLf}{vbCrLf}" &
    '                            $"Printers reported by service:{vbCrLf} - {availablePrintersMsg}{vbCrLf}{vbCrLf}" &
    '                            "(Cannot print without a printer recognized by the service).",
    '                            "Printer Not Found by Service", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '            Console.WriteLine($"Printer '{printerName}' not found via Dymo Connect Service.")
    '            Return ' Cannot proceed without a valid printer recognized by the service
    '        Else
    '            Console.WriteLine($"Printer '{targetPrinterInfo.DisplayName}' found via Dymo Connect Service.")
    '        End If


    '        ' --- Send Print Job to Service ---
    '        Console.WriteLine($"Sending print job to Dymo Connect Service for printer '{targetPrinterInfo.DisplayName}'...")
    '        ' Use the PrintLabelAsync method, passing label XML, parameters, and printer name
    '        Await DymoService.PrintLabelAsync(labelXml, printParams, targetPrinterInfo.Name) ' Use the 'Name' property for printing

    '        Console.WriteLine("Print job successfully sent to the DYMO Connect Service.")
    '        MessageBox.Show($"Print job for {qty} label(s) sent to the DYMO Connect Service for printer '{targetPrinterInfo.DisplayName}'.",
    '                         "Print Job Sent", MessageBoxButtons.OK, MessageBoxIcon.Information)

    '    Catch serviceEx As Exception When TypeOf serviceEx Is DymoServiceException
    '        ' Specific handling for Dymo Service errors
    '        MessageBox.Show($"A DYMO Connect Service error occurred:{vbCrLf}{serviceEx.Message}",
    '                       "DYMO Service Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Console.WriteLine($"DYMO SERVICE ERROR: {serviceEx}")
    '    Catch ioEx As IOException
    '        MessageBox.Show($"Error reading label file '{Path.GetFileName(labelPathToUse)}':{vbCrLf}{ioEx.Message}",
    '                      "File Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Console.WriteLine($"FILE READ ERROR: {ioEx}")
    '    Catch ex As Exception
    '        ' Catch other potential errors (file access, general exceptions)
    '        MessageBox.Show($"An unexpected error occurred:{vbCrLf}{ex.Message}",
    '                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Console.WriteLine($"GENERAL ERROR: {ex}")
    '    End Try

    'End Function

End Class