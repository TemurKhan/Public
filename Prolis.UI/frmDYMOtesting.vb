
Imports System.IO
Imports System.Net.Http
Imports System.Text
Imports System.Threading.Tasks
Imports System.Xml




'Public Class DymoService


'Private ReadOnly _labelFramework As ILabelFramework

'Public Sub New()
'    _labelFramework = New LabelFramework()
'    _labelFramework.Initialize()
'End Sub


'Public Function PrintLabel() As Task
'    Dim printers = _labelFramework.GetPrinters()

'    If printers.Count = 0 Then
'        Throw New Exception("Install Dymo Connect Software first")
'    End If

'    Dim label = _labelFramework.OpenLabel("template.label")
'    label.SetObjectText("TEXT_OBJECT", "Hello .NET 8")

'    Return _labelFramework.Print(
'        printerName:=printers.First().Name,
'        labelXml:=label.Xml,
'        copies:=1
'    )
'End Function
'End Class
Imports DYMO.LabelAPI.Helpers
Imports DymoSDK.Implementations
Imports DymoSDK.Interfaces
Public Class frmDYMOtesting




    Dim LabelQRObject As DymoSDK.Interfaces.ILabelObject


    Private Async Sub PrintDymoLabel(labelPath As String, printerName As String)
        Try
            ' Load label XML from file
            Dim labelXml As String = System.IO.File.ReadAllText(labelPath)

            ' JSON payload format
            Dim jsonPayload As String = $"
        {{
            ""labelXml"": ""{EscapeJson(labelXml)}"",
            ""labelSet"": null,
            ""printerName"": ""{printerName}"",
            ""printParamsXml"": """",
            ""printQuality"": ""Auto"",
            ""copies"": 1
        }}"

            ' Setup HTTP client
            Dim client As New HttpClient()
            client.Timeout = TimeSpan.FromSeconds(10)

            ' DYMO Web Service usually runs on this port (may vary)
            Dim dymoServiceUrl As String = "https://localhost:41951/DYMO/DLS/Printing/PrintLabel"

            Dim content As New StringContent(jsonPayload, Encoding.UTF8, "application/json")
            Dim response = Await client.PostAsync(dymoServiceUrl, content)

            If response.IsSuccessStatusCode Then
                MessageBox.Show("Label sent to printer successfully.")
            Else
                Dim err = Await response.Content.ReadAsStringAsync()
                MessageBox.Show("Failed to print label: " & err)
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Function EscapeJson(raw As String) As String
        Return raw.Replace("\", "\\").Replace("""", "\""")
    End Function





    Private Sub testing()
        Dim dymoSDKLabel As IDymoLabel = DymoLabel.Instance ' Use the singleton instance instead of 'New'  

        Dim LabelTextObject As ILabelObject

        dymoSDKLabel.LoadLabelFromFilePath("D:\Office Work\Prolis.Net\Reports\Dymo30334Adr.label")

        LabelTextObject = dymoSDKLabel.GetLabelObject("TextObject")

        dymoSDKLabel.UpdateLabelObject(LabelTextObject, "abc")

        LabelQRObject = dymoSDKLabel.GetLabelObject("ImageObject")

        dymoSDKLabel.UpdateLabelObject(LabelQRObject, "123")

        DymoPrinter.Instance.PrintLabel(dymoSDKLabel, "SelectedPrinter", 1)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button2.Click



        'For Each printer As String In System.Drawing.Printing.PrinterSettings.InstalledPrinters
        '    Console.WriteLine(printer)

        'Next

        Dim labelPath = "D:\Office Work\Prolis.Net\Reports\Dymo30334Adr_D.dymo"
        GetDymoLabelObjectNames(labelPath)


        Dim objectNames As List(Of String) = New List(Of String)()

        '**************************************
        'If File.Exists(filePath) Then
        ' Read the contents of the label file
        Dim labelXml As String = File.ReadAllText(labelPath)

        ' Parse the XML to extract object names
        objectNames = ExtractObjectNames(labelXml, Path.GetExtension(labelPath))

        ' End If

        '' Dim printerName As String = "Microsoft Print to PDF" '' "DYMO LabelWriter 450" ' Replace with your printer name
        Dim printerName = "Virtual" ' Replace with your printer name
        ' PrintDymoLabel(labelPath, printerName)


        'testing()
        'Dim labelPath As String = "D:\Office Work\Prolis.Net\Reports\samplelabel.Dymo"
        'Dim dymoLabel As DymoSDK.Implementations.DymoLabel = DymoLabel.Instance
        'dymoLabel.LoadLabelFromFilePath(labelPath)

        'Dim labelObject As DymoSDK.Interfaces.ILabelObject = dymoLabel.GetLabelObject("PlaceholderName")
        'dymoLabel.UpdateLabelObject(labelObject, "Your Dynamic Value")


        'Dim printers As List(Of String) = DymoPrinter.Instance.GetPrinters()


        ''Dim printers As List(Of String) = DymoPrinter.Instance.GetPrinters()


        'If printers.Count = 0 Then
        '    MessageBox.Show("No printers found. Please install Dymo Connect software.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return
        'End If
        'Dim selectedPrinter As String = printers(0) ' Select the first printer in the list

        'DymoPrinter.Instance.PrintLabel(dymoLabel, selectedPrinter, 1, False)



    End Sub

    Private Async Sub CheckDymoStatus()
        Try
            Dim client As New HttpClient()
            Dim url As String = "https://localhost:41951/DYMO/DLS/StatusConnected"
            Dim response = Await client.GetAsync(url)

            If response.IsSuccessStatusCode Then
                Dim status = Await response.Content.ReadAsStringAsync()
                MessageBox.Show("DYMO Web Service is running: " & status)
            Else
                MessageBox.Show("DYMO service not available.")
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        CheckDymoStatus()
    End Sub


    Private Sub GetDymoLabelObjectNames(labelFilePath As String)
        If Not System.IO.File.Exists(labelFilePath) Then
            MessageBox.Show("Label file not found.")
            Return
        End If

        Try
            Dim doc As New XmlDocument()
            doc.Load(labelFilePath)

            ' Get all ObjectInfo nodes (they hold fields like text/barcode)
            Dim objectNodes As XmlNodeList = doc.GetElementsByTagName("ObjectInfo")

            Dim objectNames As New List(Of String)

            For Each node As XmlNode In objectNodes
                Dim nameNode As XmlNode = node.SelectSingleNode(".//Name")
                If nameNode IsNot Nothing Then
                    objectNames.Add(nameNode.InnerText)
                End If
            Next

            If objectNames.Count = 0 Then
                MessageBox.Show("No named objects found in label.")
            Else
                Dim msg As String = "Named objects found:" & vbCrLf & String.Join(vbCrLf, objectNames)
                MessageBox.Show(msg, "Label Fields")
            End If

        Catch ex As Exception
            MessageBox.Show("Error reading label file: " & ex.Message)
        End Try
    End Sub
    Function ExtractObjectNames(labelXml As String, fileExt As String) As List(Of String)
        Dim objectNames As New List(Of String)()

        ' Load the label XML document
        Dim xmlDoc As New XmlDocument()
        xmlDoc.LoadXml(labelXml)

        Dim mainNode, childNode As String

        If fileExt = ".label" Then
            mainNode = "//ObjectInfo"
            childNode = ".//*[local-name()='Name']"
        Else
            mainNode = "//LabelObjects/*"
            childNode = "Name"

        End If

        ' Select all ObjectInfo nodes
        Dim objectNodes As XmlNodeList = xmlDoc.SelectNodes(mainNode)

        ' Extract object names
        For Each objNode As XmlNode In objectNodes
            ' Select the Name node irrespective of the parent node type
            Dim nameNode As XmlNode = objNode.SelectSingleNode(childNode)
            If nameNode IsNot Nothing Then
                Dim objName As String = nameNode.InnerText
                If Not String.IsNullOrEmpty(objName) Then
                    objectNames.Add(objName)

                    LoggerHelper.LogInfo($"Object Name: {objName}")
                End If
            End If
        Next

        Return objectNames
    End Function

End Class

''If lblFile = "" Then lblFile = "Dymo30334Adr.Label"
'TODO: for Dymo Label update here

'If InStr(Printer, "DYMO") > 0 Then
'    DymoAddIn = New Dymo.DymoAddIn
'    DymoLabel = New Dymo.DymoLabels
'    If DymoAddIn.Open(My.Application.Info.DirectoryPath _
'    & "\Reports\" & lblFile) Then
'        DymoLabel.SetField("Name", LabelInfo(0))
'        DymoLabel.SetField("Address", LabelInfo(1))
'        DymoLabel.SetField("CSZ", LabelInfo(2))
'        DymoLabel.SetField("Zip", LabelInfo(3))
'        DymoAddIn.SelectPrinter(Printer)
'        DymoAddIn.Print(QTY, False)
'    ElseIf DymoAddIn.Open(My.Application.Info.DirectoryPath &
'    "\Reports\Dymo30336Adr.Label") Then
'        DymoLabel.SetField("Name", LabelInfo(0))
'        DymoLabel.SetField("Address", LabelInfo(1))
'        DymoLabel.SetField("CSZ", LabelInfo(2))
'        DymoLabel.SetField("Zip", LabelInfo(3))
'        DymoAddIn.SelectPrinter(Printer)
'        DymoAddIn.Print(QTY, False)
'    Else

