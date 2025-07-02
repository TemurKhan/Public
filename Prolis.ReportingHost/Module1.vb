Imports System.IO
Imports Microsoft.VisualBasic.ApplicationServices
Imports Newtonsoft.Json
Imports Prolis.Reporting
Imports Prolis.Reporting.ReportEngine

Module Module1

    Sub Main(args As String())

        If args.Length = 0 Then
            args = New String() {"C:\Users\hp\AppData\Local\Temp\report_input.json"}
            Console.WriteLine("No input file provided.")
            'Console.ReadKey()
            'Return
        End If

        Dim inputFile = args(0)
        Console.WriteLine("Input file: " & inputFile)
        inputFile = "C:\Users\hp\AppData\Local\Temp\report_input.json"

        If Not File.Exists(inputFile) Then
            Console.WriteLine("Input file not found.")

            Return
        End If

        ' Read and deserialize JSON
        Dim myReportEngine As ReportEngine
        Dim connInfo As New ReportEngine.ConnInfo()

        Dim json = File.ReadAllText(inputFile)
        Dim jsn = JsonConvert.DeserializeObject(Of ReportEngine.ConnInfo)(json)


        myReportEngine = New ReportEngine(jsn)



        Dim result As ReportEngine.ReportGenerationResult = myReportEngine.GenerateReportsAsync(jsn.accids, jsn.IsCustomRPT, jsn.ReportPath).Result
        'return result.path
        Dim resultObj As New With {
    .Success = True,
    .Message = "Report created",
    .ReportPath = result.path
}
        Console.WriteLine(JsonConvert.SerializeObject(resultObj))


    End Sub


End Module
