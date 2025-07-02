
Imports System.IO
Imports System.Text

Public Class Claim837Processor

    Public Function IsValidFilePath(filePath As String) As Boolean
        If String.IsNullOrWhiteSpace(filePath) Then
            Return False
        End If

        ' Check if the path is a valid file path
        Try
            Dim pathRoot As String = Path.GetPathRoot(filePath)
            If String.IsNullOrWhiteSpace(pathRoot) Then
                Return False
            End If

            ' Check if the file exists
            Return File.Exists(filePath)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Parse837File(eraContent As String) As String
        ' Read ERA file content
        If IsValidFilePath(eraContent) Then
            eraContent = File.ReadAllText(eraContent)
        End If

        ' Split ERA into individual segments
        Dim segments As String() = eraContent.Split(New String() {"~"}, StringSplitOptions.RemoveEmptyEntries)

        ' Initialize variables to store parsed data
        Dim eraClaims As New List(Of ERAClaim837)()
        Dim currentClaim As ERAClaim837 = Nothing
        Dim currentPatientName As String = ""
        Dim PayerNameCompany As String = ""
        ' Process each segment
        Dim i As Integer = 0
        For Each segment As String In segments
            Dim segmentParts As String() = segment.Split("*"c)
            currentPatientName = ""
            ' Identify segment type and parse accordingly
            Select Case segmentParts(0)
                Case "CLM"
                    ' CLM segment, start a new claim
                    Dim statu As String = segmentParts(5)
                    If statu.Contains("7") Then
                        statu = "CORRECTED"
                    Else
                        statu = ""
                    End If
                    If i = 0 Then
                        currentClaim.ClaimID = segmentParts(1) & " -" & statu
                        eraClaims.Add(currentClaim)
                        i = 0
                    Else
                        currentClaim = New ERAClaim837()


                        currentClaim.ClaimID = segmentParts(1)
                        eraClaims.Add(currentClaim)
                    End If


                Case "REF"
                    ' REF segment, reference identification
                    If currentClaim IsNot Nothing Then
                        If segmentParts(1) = "F8" Then
                            currentClaim.ClaimID += " ORIG. REF:" & segmentParts(2)
                        ElseIf segmentParts(1) = "X4" Then
                            currentClaim.ReferenceID = " X4: " & segmentParts(2)
                        End If

                    End If

                Case "HI"
                    ' HI segment, health care diagnosis code
                    If currentClaim IsNot Nothing Then
                        currentClaim.DiagnosisCode = segmentParts(1)
                    End If

                Case "NM1"
                    ' NM1 segment, patient or provider information
                    Dim entityIdentifierCode As String = segmentParts(1)
                    If entityIdentifierCode = "QC" AndAlso currentClaim IsNot Nothing Then
                        ' Patient information
                        Dim lastName As String = segmentParts(3)
                        Dim firstName As String = segmentParts(4)
                        Dim middleName As String = If(segmentParts.Length > 5, segmentParts(5), "")
                        currentPatientName = lastName & " " & firstName & " " & middleName
                        currentClaim.PatientName = currentPatientName
                    ElseIf entityIdentifierCode = "IL" AndAlso currentClaim IsNot Nothing Then
                        ' Patient information
                        Dim lastName As String = segmentParts(3)
                        Dim firstName As String = segmentParts(4)
                        Dim middleName As String = If(segmentParts.Length > 5, segmentParts(5), "")
                        currentPatientName = lastName & " " & firstName & " " & middleName
                        If currentClaim.PatientName <> currentPatientName Then
                            currentClaim = New ERAClaim837()
                        End If

                        currentClaim.PatientName = currentPatientName
                    ElseIf entityIdentifierCode = "IL" Then
                        ' Patient information
                        Dim lastName As String = segmentParts(3)
                        Dim firstName As String = segmentParts(4)
                        Dim middleName As String = If(segmentParts.Length > 5, segmentParts(5), "")
                        currentPatientName = lastName & " " & firstName & " " & middleName
                        If i = 0 Then
                            currentClaim = New ERAClaim837()

                        End If

                        currentClaim.PatientName = currentPatientName
                    ElseIf entityIdentifierCode = "40" Then
                        ' Patient information
                        Dim lastName As String = segmentParts(3)
                        Dim firstName As String = segmentParts(4)
                        Dim middleName As String = If(segmentParts.Length > 5, segmentParts(5), "")
                        PayerNameCompany = lastName & " " & firstName & " " & middleName

                    End If

                Case "LX"
                    ' LX segment, service line number
                    If currentClaim IsNot Nothing Then
                        currentClaim.AddServiceLine(segmentParts(1))
                    End If

                Case "SV1"
                    ' SV1 segment, service line information
                    If currentClaim IsNot Nothing AndAlso currentClaim.ServiceLines.Count > 0 Then
                        Dim serviceLine As ServiceLine837 = currentClaim.ServiceLines.Last()
                        serviceLine.ServiceCode = segmentParts(1)
                        serviceLine.BilledAmount = segmentParts(2)
                        serviceLine.AllowedAmount = segmentParts(3)
                    End If

                Case "DTP"
                    ' DTP segment, date or time period
                    If segmentParts(1) = "472" AndAlso currentClaim IsNot Nothing AndAlso currentClaim.ServiceLines.Count > 0 Then
                        Dim serviceLine As ServiceLine837 = currentClaim.ServiceLines.Last()
                        serviceLine.ServiceDate = DateTime.ParseExact(segmentParts(3), "yyyyMMdd", Nothing).ToString("MM/dd/yyyy")
                    End If

                Case "SE", "GE", "IEA"
                    ' End of transaction segments
                    ' Do nothing for now

            End Select

        Next

        ' Generate HTML for all claims
        Dim htmlBuilder As New StringBuilder()
        htmlBuilder.AppendLine("<html>")
        htmlBuilder.AppendLine("<head>")
        htmlBuilder.AppendLine("<title>837 Claims</title>")
        htmlBuilder.AppendLine("<style> body { font-family: Arial, sans-serif; } .justify { text-align: justify; } table { font-size: 14px; width: 100%; border-collapse: collapse; } th, td { border: 1px solid gray; padding: 8px; text-align: center; } th { background-color: #f2f2f2; } .no-border { border: none !important; } @media print { body { margin: 0; padding: 0; } table { width: 100%; font-size: 12px; } th, td { padding: 6px; } .printable_text { width: 100%; } .no-print { display: none; } .page-break { page-break-before: always; } } </style>")

        htmlBuilder.AppendLine("</head>")
        htmlBuilder.AppendLine("<body>")
        htmlBuilder.AppendLine("<h1>837 Claims</h1>")
        htmlBuilder.AppendLine("<h3>Service Provider Name: " & PayerNameCompany & " </h3>")
        For Each claim As ERAClaim837 In eraClaims
            Dim TBilledAmount As Double = 0.0
            htmlBuilder.AppendLine("<table>")
            htmlBuilder.AppendLine("<thead>")
            htmlBuilder.AppendLine("<tr>")
            htmlBuilder.AppendLine("<th colspan='6'>Claim ID: " & claim.ClaimID & " | Patient Name: " & claim.PatientName & " | REF " & claim.ReferenceID & "</th>")
            htmlBuilder.AppendLine("</tr>")
            htmlBuilder.AppendLine("<tr>")
            htmlBuilder.AppendLine("<th>Service Line</th>")
            htmlBuilder.AppendLine("<th>Service Code</th>")
            htmlBuilder.AppendLine("<th>Service Date</th>")
            htmlBuilder.AppendLine("<th>Billed Amount</th>")
            htmlBuilder.AppendLine("<th>Allowed Amount</th>")
            htmlBuilder.AppendLine("</tr>")
            htmlBuilder.AppendLine("</thead>")
            htmlBuilder.AppendLine("<tbody>")

            For Each serviceLine As ServiceLine837 In claim.ServiceLines
                htmlBuilder.AppendLine("<tr>")
                htmlBuilder.AppendLine("<td>" & serviceLine.ServiceLineNumber & "</td>")
                htmlBuilder.AppendLine("<td>" & serviceLine.ServiceCode & "</td>")
                htmlBuilder.AppendLine("<td>" & serviceLine.ServiceDate & "</td>")
                htmlBuilder.AppendLine("<td>$" & serviceLine.BilledAmount & "</td>")
                htmlBuilder.AppendLine("<td>" & serviceLine.AllowedAmount & "</td>")
                htmlBuilder.AppendLine("</tr>")
                TBilledAmount += Convert.ToDouble(serviceLine.BilledAmount)
            Next
            htmlBuilder.AppendLine("<tr>")
            htmlBuilder.AppendLine("<td colspan='3' > Total </td>")
            htmlBuilder.AppendLine("<td>$" & TBilledAmount.ToString("0.00") & "</td>")
            htmlBuilder.AppendLine("<td></td>")
            htmlBuilder.AppendLine("</tr>")
            htmlBuilder.AppendLine("</tbody>")
            htmlBuilder.AppendLine("</table>")
            htmlBuilder.AppendLine("<br/>")
        Next

        htmlBuilder.AppendLine("</body>")
        htmlBuilder.AppendLine("</html>")

        ' Return the final HTML
        Return htmlBuilder.ToString()
    End Function

End Class

Public Class ERAClaim837
    Public Property ClaimID As String
    Public Property PatientName As String
    Public Property ReferenceID As String
    Public Property DiagnosisCode As String
    Public Property ServiceLines As New List(Of ServiceLine837)

    Public Sub AddServiceLine(serviceLineNumber As String)
        Dim serviceLine As New ServiceLine837()
        serviceLine.ServiceLineNumber = serviceLineNumber
        ServiceLines.Add(serviceLine)
    End Sub
End Class

Public Class ServiceLine837
    Public Property ServiceLineNumber As String
    Public Property ServiceCode As String
    Public Property ServiceDate As String
    Public Property BilledAmount As String
    Public Property AllowedAmount As String
End Class
