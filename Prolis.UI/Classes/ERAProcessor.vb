Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Security.Policy

Public Class ERAProcessor
    Public Sub New()

    End Sub

    Function IsValidFilePath(filePath As String) As Boolean
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
            Dim dd = File.Exists(filePath)
            Return File.Exists(filePath)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function IsValidFileExtension(filePath As String) As Boolean
        Dim validExtensions As String() = {".dat", ".txt", ".835", ".837"}
        Dim fileExtension As String = Path.GetExtension(filePath).ToLower()

        Return validExtensions.Contains(fileExtension)
    End Function
    Public Function ParseERAFile(eraContent As String) As String
        ' Read ERA file content
        If IsValidFilePath(eraContent) Then
            If Not IsValidFileExtension(eraContent) Then
                Return ""
            End If
            eraContent = File.ReadAllText(eraContent)
        End If
        If eraContent Is Nothing Then
            MessageBox.Show("ERA does not exist in system.")
            Return ""
        End If
        eraContent = eraContent.Replace(Environment.NewLine, "").Replace(vbCrLf, "").Replace(vbCr, "").Replace(vbLf, "").Replace(" & vbLf & ", "")
        Dim detector As New FileTypeDetector()
        Dim fileType As String = detector.DetectFileType(eraContent)
        If fileType = "837" Then
            Dim Claim837Processor As Claim837Processor = New Claim837Processor()
            Return Claim837Processor.Parse837File(eraContent)
        End If


        ' Split ERA into individual segments
        Dim segments As String() = eraContent.Split(New String() {"~"}, StringSplitOptions.RemoveEmptyEntries)

        ' Initialize variables to store parsed data
        Dim eraClaims As New List(Of ERAClaim)()
        Dim currentClaim As ERAClaim = Nothing
        Dim currentPayer As PayerInfo = Nothing
        Dim currentPayee As PayeeInfo = Nothing
        Dim currentPatientName As String = ""
        Dim codes = ""
        ' Process each segment
        For Each segment As String In segments
            If segment.Contains("MOA") Then
                Dim ddd = ""
            End If
            Dim segmentParts As String() = segment.Split("*"c)

            ' Identify segment type and parse accordingly
            Select Case segmentParts(0)
                Case "CLP"
                    ' CLP segment, start a new claim
                    currentClaim = New ERAClaim()
                    currentClaim.ClaimID = segmentParts(1)
                    currentClaim.PatientName = currentPatientName ' Assign patient name to the current claim
                    currentClaim.ICN = segmentParts(7)
                    currentClaim.TotalAllowed = segmentParts(3)
                    currentClaim.TotalBilled = segmentParts(3)
                    currentClaim.PR = segmentParts(5)
                    eraClaims.Add(currentClaim)

                Case "NM1"
                    ' NM1 segment, patient or payer information
                    Dim entityIdentifierCode As String = segmentParts(1)
                    Dim entityTypeQualifier As String = segmentParts(2)
                    If segmentParts(1) = "QC" AndAlso currentClaim IsNot Nothing Then
                        ' Assuming NM1*QC*1*LASTNAME*FIRSTNAME*MIDDLENAME*SUFFIX
                        Dim lastName As String = segmentParts(3)
                        Dim firstName As String = segmentParts(4)
                        Dim middleName As String = If(segmentParts.Length > 5, segmentParts(5), "")
                        currentClaim.PatientName = lastName & " " & firstName & " " & middleName
                        Try
                            currentClaim.HIC = segmentParts(9)
                        Catch ex As Exception

                        End Try

                    End If
                    If entityTypeQualifier = "QC" Then
                        ' Patient information
                        currentPatientName = segmentParts(3)
                    ElseIf entityTypeQualifier = "2" Then
                        ' Payer information
                        currentPayer = New PayerInfo()
                        currentPayer.Name = segmentParts(3)
                        Try
                            currentClaim.Payer = currentPayer
                        Catch ex As Exception

                        End Try

                    ElseIf entityTypeQualifier = "1" Then
                        ' Payee information
                        currentPayee = New PayeeInfo()
                        currentPayee.Name = segmentParts(3)
                        currentClaim.Payee = currentPayee
                    End If

                Case "N3"
                    ' N3 segment, address information
                    If currentPayer IsNot Nothing AndAlso currentClaim IsNot Nothing Then
                        currentPayer.Address = segmentParts(1)
                    ElseIf currentPayee IsNot Nothing AndAlso currentClaim IsNot Nothing Then
                        currentPayee.Address = segmentParts(1)
                    End If
                Case "MOA"
                    Dim dsd = segmentParts
                    Try
                        codes += "|" + segmentParts(3)
                    Catch ex As Exception

                    End Try

                    Try
                        codes += "|" + segmentParts(4)
                    Catch ex As Exception

                    End Try


                Case "N4"
                    ' N4 segment, city/state/ZIP information
                    If currentPayer IsNot Nothing AndAlso currentClaim IsNot Nothing Then
                        currentPayer.City = segmentParts(1)
                        currentPayer.State = segmentParts(2)
                        currentPayer.Zip = segmentParts(3)
                    ElseIf currentPayee IsNot Nothing AndAlso currentClaim IsNot Nothing Then
                        currentPayee.City = segmentParts(1)
                        currentPayee.State = segmentParts(2)
                        currentPayee.Zip = segmentParts(3)
                    End If

                Case "DTM"
                    ' DTM segment, typically for dates
                    Dim qualifier As String = segmentParts(1)
                    Dim dateValue As String = segmentParts(2)

                    If qualifier = "472" AndAlso currentClaim IsNot Nothing Then
                        ' Service Date
                        currentClaim.ServiceDate = DateTime.ParseExact(dateValue, "yyyyMMdd", Nothing).ToString("MM/dd/yyyy")
                    ElseIf qualifier = "232" AndAlso currentClaim IsNot Nothing Then
                        ' Service Date
                        currentClaim.ServiceDate = DateTime.ParseExact(dateValue, "yyyyMMdd", Nothing).ToString("MM/dd/yyyy")
                    ElseIf qualifier = "233" AndAlso currentClaim IsNot Nothing Then
                        ' Service Date
                        If currentClaim.ServiceDate <> DateTime.ParseExact(dateValue, "yyyyMMdd", Nothing).ToString("MM/dd/yyyy") Then
                            currentClaim.ServiceDate += "-" + DateTime.ParseExact(dateValue, "yyyyMMdd", Nothing).ToString("MM/dd/yyyy")

                        Else
                            currentClaim.ServiceDate = DateTime.ParseExact(dateValue, "yyyyMMdd", Nothing).ToString("MM/dd/yyyy")

                        End If

                    End If

                Case "SVC"
                    ' SVC segment, service information
                    Dim serviceCode As String = segmentParts(1)
                    Dim billedAmount As String = segmentParts(2)
                    Dim paidAmount As String = segmentParts(3)
                    ' Add service code and billed amount to the last claim
                    If currentClaim IsNot Nothing Then
                        currentClaim.AddServiceCode(serviceCode, billedAmount, paidAmount)
                    End If

                Case "AMT"
                    ' AMT segment, amounts
                    Dim amountQualifier As String = segmentParts(1)
                    Dim amountValue As String = segmentParts(2)

                    If amountQualifier = "B6" AndAlso currentClaim IsNot Nothing Then
                        ' Allowed Amount
                        currentClaim.AddAllowedAmount(amountValue)
                    End If

                Case "CAS"
                    ' CAS segment, adjustment information
                    Dim adjustmentReasonCode As String = "CO-" + segmentParts(2)
                    Dim ddk = segmentParts
                    If segmentParts(1) = "OA" Then
                        adjustmentReasonCode = "OA-" + segmentParts(2)
                        Try
                            Try
                                adjustmentReasonCode += "|" + "OA-" + segmentParts(5)
                            Catch ex As Exception

                            End Try
                            Dim adjustmentAmount As String = segmentParts(3)
                            Try
                                adjustmentAmount += "|" + segmentParts(6)
                            Catch ex As Exception

                            End Try

                            ' Add adjustment details to the last claim's last service line
                            If currentClaim IsNot Nothing AndAlso currentClaim.ServiceLines.Count > 0 Then
                                Dim lastServiceLine As ServiceLine = currentClaim.ServiceLines.Last()
                                lastServiceLine.AdjustmentReasonCode = adjustmentReasonCode
                                lastServiceLine.AdjustmentAmount = adjustmentAmount
                            End If
                        Catch ex As Exception

                        End Try

                    ElseIf segmentParts(1) = "CO" Then
                        adjustmentReasonCode = "CO-" + segmentParts(2)
                        Try
                            Try
                                adjustmentReasonCode += "|" + "CO-" + segmentParts(5)
                            Catch ex As Exception

                            End Try
                            Dim adjustmentAmount As String = segmentParts(3)
                            Try
                                adjustmentAmount += "|" + segmentParts(6)
                            Catch ex As Exception

                            End Try

                            ' Add adjustment details to the last claim's last service line
                            If currentClaim IsNot Nothing AndAlso currentClaim.ServiceLines.Count > 0 Then
                                Dim lastServiceLine As ServiceLine = currentClaim.ServiceLines.Last()
                                lastServiceLine.AdjustmentReasonCode = adjustmentReasonCode
                                lastServiceLine.AdjustmentAmount = adjustmentAmount
                            End If
                        Catch ex As Exception

                        End Try
                    ElseIf segmentParts(1) = "PR" Then

                        If segmentParts(2) = "1" Then
                            Try

                                Dim adjustmentAmount As String = segmentParts(3)
                                ' Add deduct details to the last claim's last service line
                                If currentClaim IsNot Nothing AndAlso currentClaim.ServiceLines.Count > 0 Then
                                    Dim lastServiceLine As ServiceLine = currentClaim.ServiceLines.Last()


                                    lastServiceLine.Deduct = adjustmentAmount
                                End If
                            Catch ex As Exception

                            End Try
                            If segmentParts.Length > 4 Then
                                If segmentParts(5) = "2" Then
                                    Try

                                        Dim adjustmentAmount As String = segmentParts(6)
                                        ' Add deduct details to the last claim's last service line
                                        If currentClaim IsNot Nothing AndAlso currentClaim.ServiceLines.Count > 0 Then
                                            Dim lastServiceLine As ServiceLine = currentClaim.ServiceLines.Last()
                                            ' lastServiceLine.AdjustmentReasonCode += " | " + segmentParts(1) + "-" + segmentParts(2)
                                            lastServiceLine.Coins = adjustmentAmount
                                            lastServiceLine.COSIdentifierCode = "2"

                                        End If
                                    Catch ex As Exception

                                    End Try

                                End If
                            End If
                        ElseIf segmentParts(2) = "3" Then
                            Try

                                Dim adjustmentAmount As String = segmentParts(3)
                                ' Add deduct details to the last claim's last service line
                                If currentClaim IsNot Nothing AndAlso currentClaim.ServiceLines.Count > 0 Then
                                    Dim lastServiceLine As ServiceLine = currentClaim.ServiceLines.Last()
                                    lastServiceLine.AdjustmentReasonCode += " | " + segmentParts(1) + "-" + segmentParts(2)
                                    lastServiceLine.Copy = adjustmentAmount
                                    lastServiceLine.COSIdentifierCode = "3"
                                End If
                            Catch ex As Exception

                            End Try
                        ElseIf segmentParts(2) = "2" Then
                            Try

                                Dim adjustmentAmount As String = segmentParts(3)
                                ' Add deduct details to the last claim's last service line
                                If currentClaim IsNot Nothing AndAlso currentClaim.ServiceLines.Count > 0 Then
                                    Dim lastServiceLine As ServiceLine = currentClaim.ServiceLines.Last()
                                    ' lastServiceLine.AdjustmentReasonCode += " | " + segmentParts(1) + "-" + segmentParts(2)
                                    lastServiceLine.Coins = adjustmentAmount
                                    lastServiceLine.COSIdentifierCode = "2"

                                End If
                            Catch ex As Exception

                            End Try
                        ElseIf segmentParts(2) = "49" Then
                            Try


                                Dim adjustmentAmount As String = segmentParts(3)
                                ' Add deduct details to the last claim's last service line
                                If currentClaim IsNot Nothing AndAlso currentClaim.ServiceLines.Count > 0 Then
                                    Dim lastServiceLine As ServiceLine = currentClaim.ServiceLines.Last()
                                    lastServiceLine.AdjustmentReasonCode += " | " + segmentParts(1) + "-" + segmentParts(2)
                                    lastServiceLine.AdjustmentAmount = adjustmentAmount
                                    lastServiceLine.COSIdentifierCode = "49"

                                End If
                            Catch ex As Exception

                            End Try
                        ElseIf segmentParts(2) = "204" Then
                            Try


                                Dim adjustmentAmount As String = segmentParts(3)
                                ' Add deduct details to the last claim's last service line
                                If currentClaim IsNot Nothing AndAlso currentClaim.ServiceLines.Count > 0 Then
                                    Dim lastServiceLine As ServiceLine = currentClaim.ServiceLines.Last()
                                    lastServiceLine.AdjustmentReasonCode += " | " + segmentParts(1) + "-" + segmentParts(2)
                                    lastServiceLine.AdjustmentAmount = adjustmentAmount
                                    lastServiceLine.COSIdentifierCode = "204"

                                End If
                            Catch ex As Exception

                            End Try
                        End If
                    End If



                Case "REF"
                    ' CAS segment, adjustment information


                    ' Add adjustment details to the last claim's last service line
                    If currentClaim IsNot Nothing AndAlso currentClaim.ServiceLines.Count > 0 Then
                        Dim lastServiceLine As ServiceLine = currentClaim.ServiceLines.Last()

                    End If
                Case "LQ"
                    ' LQ segment, remarks
                    If segmentParts(1) = "HE" AndAlso currentClaim IsNot Nothing AndAlso currentClaim.ServiceLines.Count > 0 Then
                        ' Assuming LQ*HE*REMARK_CODE
                        Dim remarkCode As String = segmentParts(2)
                        currentClaim.ServiceLines.Last().Remarks = remarkCode
                    End If

            End Select
        Next

        ' Generate HTML for all claims
        Dim htmlBuilder As New StringBuilder()
        Dim totalAllowedAmt As Double = 0.0
        Dim totalPaidAmt As Double = 0.0
        Dim totalAdjAmt As Double = 0.0
        Dim totalDeductAmt As Double = 0.0
        Dim totalcopyAmt As Double = 0.0
        Dim totalCoinsAmt As Double = 0.0
        Dim AlltotalAllowedAmt As Double = 0.0
        Dim AlltotalPaidAmt As Double = 0.0
        Dim AlltotalAdjAmt As Double = 0.0
        Dim AlltotalBillAmt As Double = 0.0
        Dim AllTotalDeduct As Double = 0.00
        Dim AllTotalCopy As Double = 0.00
        Dim AllTotalCoIns As Double = 0.00
        Dim iscopy As Boolean = False

        For Each claim As ERAClaim In eraClaims
            If claim.ServiceLines.Any(Function(v) Not v.COSIdentifierCode Is Nothing AndAlso v.COSIdentifierCode = "3") Then

                iscopy = True
            Else
                iscopy = False
            End If
            ' Service Lines Table
            htmlBuilder.AppendLine("<table border='1'>")
            htmlBuilder.AppendLine("<thead>")
            htmlBuilder.AppendLine("<tr>")
            If iscopy Then
                htmlBuilder.AppendLine("<th colspan='10'>" & "Claim ID: " & claim.ClaimID & " | Name: " & claim.PatientName & " | HIC " & claim.HIC & " |ICN " & claim.ICN & "</th>")
            Else
                htmlBuilder.AppendLine("<th colspan='10'>" & "Claim ID: " & claim.ClaimID & " | Name: " & claim.PatientName & " | HIC " & claim.HIC & " |ICN " & claim.ICN & "</th>")


            End If
            htmlBuilder.AppendLine("</tr>")
            htmlBuilder.AppendLine("<tr>")

            htmlBuilder.AppendLine("<th>Service Date</th>")
            htmlBuilder.AppendLine("<th>PROC</th>")
            htmlBuilder.AppendLine("<th>Billed ($)</th>")
            htmlBuilder.AppendLine("<th>Allowed ($)</th>")
            htmlBuilder.AppendLine("<th>DEDUCT ($)</th>")
            htmlBuilder.AppendLine("<th>COINS ($)</th>")
            If iscopy Then
                htmlBuilder.AppendLine("<th>COPAY ($)</th>")

            Else

            End If
            htmlBuilder.AppendLine("<th>GRP CD</th>")
            htmlBuilder.AppendLine("<th>RC-AMT ($)</th>")

            htmlBuilder.AppendLine("<th>PROV PD ($)</th>")

            ' htmlBuilder.AppendLine("<th>Remarks</th>")
            htmlBuilder.AppendLine("</tr>")
            htmlBuilder.AppendLine("</thead>")
            htmlBuilder.AppendLine("<tbody>")
            totalAllowedAmt = 0.0
            totalPaidAmt = 0.0
            totalAdjAmt = 0.0
            totalDeductAmt = 0.0
            totalcopyAmt = 0.0
            totalCoinsAmt = 0.0

            For Each serviceLine As ServiceLine In claim.ServiceLines
                If Not serviceLine.AllowedAmount Is Nothing Then
                    totalAllowedAmt += Convert.ToDouble(serviceLine.AllowedAmount.Trim())

                End If

                If Not serviceLine.PaidAmount Is Nothing Then
                    totalPaidAmt += Convert.ToDouble(serviceLine.PaidAmount.Trim())
                End If

                If Not serviceLine.Deduct Is Nothing Then
                    totalDeductAmt += Convert.ToDouble(serviceLine.Deduct.Trim())
                End If
                If Not serviceLine.Copy Is Nothing Then
                    totalcopyAmt += Convert.ToDouble(serviceLine.Copy.Trim())
                End If

                If Not serviceLine.Coins Is Nothing Then
                    totalCoinsAmt += Convert.ToDouble(serviceLine.Coins.Trim())
                End If


                If Not serviceLine.AdjustmentAmount Is Nothing Then
                    Dim adjamts = serviceLine.AdjustmentAmount.Split("|")
                    For Each adj As String In adjamts
                        Dim adjamt As Double = 0.0
                        If Double.TryParse(adj, adjamt) Then
                            totalAdjAmt += adjamt
                        End If
                    Next
                End If

                htmlBuilder.AppendLine("<tr>")

                htmlBuilder.AppendLine("<td>" & claim.ServiceDate & "</td>")
                htmlBuilder.AppendLine("<td>" & serviceLine.ServiceCode & "</td>")
                htmlBuilder.AppendLine("<td>" & Convert.ToDouble(serviceLine.BilledAmount).ToString("0.00") & "</td>")
                htmlBuilder.AppendLine("<td>" & IIf(serviceLine.AllowedAmount Is Nothing, "0.00", Convert.ToDouble(serviceLine.AllowedAmount).ToString("0.00")) & "</td>")
                htmlBuilder.AppendLine("<td>" & IIf(serviceLine.Deduct Is Nothing, "0.00", serviceLine.Deduct) & "</td>")
                htmlBuilder.AppendLine("<td>" & IIf(serviceLine.Coins Is Nothing, "0.00", Convert.ToDouble(serviceLine.Coins).ToString("0.00")) & "</td>")
                If iscopy Then
                    htmlBuilder.AppendLine("<td>" & serviceLine.Copy & "</td>")
                Else

                End If
                If serviceLine.AdjustmentReasonCode Is Nothing Then
                    htmlBuilder.AppendLine("<td></td>")
                Else
                    htmlBuilder.AppendLine("<td>" & serviceLine.AdjustmentReasonCode.Replace(" ", "").TrimStart("|") & "</td>")

                End If
                htmlBuilder.AppendLine("<td>" & IIf(serviceLine.AdjustmentAmount Is Nothing, "0.00", serviceLine.AdjustmentAmount) & "</td>")

                htmlBuilder.AppendLine("<td>" & Convert.ToDouble(serviceLine.PaidAmount).ToString("0.00") & "</td>")
                ' htmlBuilder.AppendLine("<td>" & serviceLine.Remarks & "</td>")
                htmlBuilder.AppendLine("</tr>")
                codes += "|" + serviceLine.AdjustmentReasonCode
                codes += "|" + serviceLine.Remarks

            Next
            htmlBuilder.AppendLine("<tr>")
            htmlBuilder.AppendLine("<td colspan='2'>CLAIM TOTALS</td>")

            htmlBuilder.AppendLine("<td>" & Convert.ToDouble(claim.TotalBilled).ToString("0.00") & "</td>")
            htmlBuilder.AppendLine("<td>" & totalAllowedAmt.ToString("0.00") & "</td>")
            htmlBuilder.AppendLine("<td>" & totalDeductAmt.ToString("0.00") & "</td>")
            htmlBuilder.AppendLine("<td>" & totalCoinsAmt.ToString("0.00") & "</td>")
            If iscopy Then
                htmlBuilder.AppendLine("<td>" & totalcopyAmt.ToString("0.00") & "</td>")
            Else

            End If
            htmlBuilder.AppendLine("<td></td>")
            htmlBuilder.AppendLine("<td>" & totalAdjAmt.ToString("0.00") & "</td>")

            htmlBuilder.AppendLine("<td>" & totalPaidAmt.ToString("0.00") & "</td>")
            htmlBuilder.AppendLine("</tr>")
            htmlBuilder.AppendLine("<tr style='border: none;font-size:smaller'>")
            If claim.PR = "" Then
                htmlBuilder.AppendLine("<td colspan='2'>PT Resp  </td>")

            Else
                htmlBuilder.AppendLine("<td colspan='2'>PT Resp " & Convert.ToDouble(claim.PR).ToString("0.00") & " </td>")

            End If


            htmlBuilder.AppendLine("</tr>")
            htmlBuilder.AppendLine("</tbody>")
            htmlBuilder.AppendLine("</table>")
            htmlBuilder.AppendLine("<br/>")

            AlltotalBillAmt += Convert.ToDouble(claim.TotalBilled)
            AlltotalAllowedAmt += totalAllowedAmt
            AlltotalAdjAmt += totalAdjAmt
            AlltotalPaidAmt += totalPaidAmt
            AllTotalDeduct += totalDeductAmt
            AllTotalCopy += totalcopyAmt
            AllTotalCoIns += totalCoinsAmt
        Next
        codes = codes.TrimStart("|")

        Dim groupedCodes = codes.TrimStart("|").Split("|").ToList().GroupBy(Function(c) c).Select(Function(g) g.First()).ToList()

        htmlBuilder.AppendLine("<table border='1'>")
        htmlBuilder.AppendLine("<thead>")
        htmlBuilder.AppendLine("<tr>")
        htmlBuilder.AppendLine("<th colspan='9'>TOTALS</th>")
        htmlBuilder.AppendLine("</tr>")
        htmlBuilder.AppendLine("<tr>")
        htmlBuilder.AppendLine("<th>NO OF CLAIMS</th>")
        htmlBuilder.AppendLine("<th>BILLED AMT</th>")
        htmlBuilder.AppendLine("<th>ALLOWED AMT</th>")
        htmlBuilder.AppendLine("<th>DEDUCT AMT</th>")
        htmlBuilder.AppendLine("<th>COINS AMT</th>")
        htmlBuilder.AppendLine("<th>TOTAL RC-AMT</th>")
        htmlBuilder.AppendLine("<th>PROV PD AMT</th>")
        htmlBuilder.AppendLine("<th>PROV ADJ AMT</th>")
        htmlBuilder.AppendLine("<th>CHECK</th>")
        htmlBuilder.AppendLine("</tr>")
        htmlBuilder.AppendLine("</thead>")
        htmlBuilder.AppendLine("<tbody>")
        htmlBuilder.AppendLine("<tr>")
        htmlBuilder.AppendLine("<td>" & eraClaims.Count() & "</td>")
        htmlBuilder.AppendLine("<td>" & AlltotalBillAmt.ToString("0.00") & "</td>")
        htmlBuilder.AppendLine("<td>" & AlltotalAllowedAmt.ToString("0.00") & "</td>")
        htmlBuilder.AppendLine("<td>" & AllTotalDeduct.ToString("0.00") & "</td>")
        htmlBuilder.AppendLine("<td>" & AllTotalCoIns.ToString("0.00") & "</td>")
        htmlBuilder.AppendLine("<td>" & (AlltotalAdjAmt + AllTotalCopy).ToString("0.00") & "</td>")
        htmlBuilder.AppendLine("<td>" & AlltotalPaidAmt.ToString("0.00") & "</td>")
        htmlBuilder.AppendLine("<td>0.00</td>")
        htmlBuilder.AppendLine("<td>" & AlltotalPaidAmt.ToString("0.00") & "</td>")
        htmlBuilder.AppendLine("</tr>")
        htmlBuilder.AppendLine("</tbody>")
        htmlBuilder.AppendLine("</table>")
        ' Replace $CLAIM_DETAILS$ placeholder in template with generated HTML
        'CODESHERE'
        Dim dd = ""
        For Each c In groupedCodes
            If c = "" Then
                Continue For
            End If
            If c.Contains("-") Then
                c = c.Split("-")(1)

            End If

            Dim description = CommonData.RetrieveColumnValue("Remittance_Advice_Remark_Codes", "Description", "Code", "'" + c + "'", "")
            If IsNumeric(c) Then

                description = CommonData.RetrieveColumnValue("Claim_Adjustment_Reason_Codes", "Description", "Code", "'" + c + "'", "")
            End If
            If description Is Nothing Then
                Continue For
            End If

            dd += "<tr Class='no-border'>" & vbCrLf &
"                <td class='justify no-border'>" & vbCrLf &
               c + "- " + description & vbCrLf &
"                   " & vbCrLf &
"                </td>" & vbCrLf &
"            </tr>"
        Next


        Dim _835 = HTMl_Temp._835.Replace("$$$", dd)

        Dim finalHtml As String = _835.Replace("$CLAIM_DETAILS$", htmlBuilder.ToString())

        ' Return the final HTML
        Return finalHtml
    End Function




End Class

Public Class PatientInfo
    Public Property Name As String
    Public Property Address As String
    Public Property City As String
    Public Property State As String
    Public Property Zip As String
End Class

Public Class ERAClaim

    Friend TotalAllowed As String
    Friend TotalBilled As String
    Friend PR As String
    Public Property ClaimID As String
    Public Property ServiceDate As String
    Public Property PatientName As String
    Public Property HIC As String
    Public Property ICN As String
    Public Property ServiceLines As New List(Of ServiceLine)
    Public Property Payer As PayerInfo
    Public Property Payee As PayeeInfo

    Public Sub New()
        ' Constructor
        Payer = New PayerInfo()
        Payee = New PayeeInfo()
    End Sub
    Public Sub AddAllowedAmount(amount As String)
        ' Add allowed amount to the last service line
        If ServiceLines.Count > 0 Then
            If amount Is Nothing Then
                amount = "0"
            End If
            ServiceLines(ServiceLines.Count - 1).AllowedAmount = amount
        End If
    End Sub
    Public Sub AddServiceCode(serviceCode As String, billedAmount As String, Paid As String)
        ' Add service code and billed amount to service lines
        Dim serviceLine As New ServiceLine()
        serviceLine.ServiceCode = serviceCode
        serviceLine.BilledAmount = billedAmount
        serviceLine.PaidAmount = Paid
        ServiceLines.Add(serviceLine)
    End Sub

    Public Sub SetPayerInfo(name As String, address As String, city As String, state As String, zip As String)
        Payer.Name = name
        Payer.Address = address
        Payer.City = city
        Payer.State = state
        Payer.Zip = zip
    End Sub

    Public Sub SetPayeeInfo(name As String, address As String, city As String, state As String, zip As String)
        Payee.Name = name
        Payee.Address = address
        Payee.City = city
        Payee.State = state
        Payee.Zip = zip
    End Sub
End Class

Public Class PayerInfo
    Public Property Name As String
    Public Property Address As String
    Public Property City As String
    Public Property State As String
    Public Property Zip As String
End Class

Public Class PayeeInfo
    Public Property Name As String
    Public Property Address As String
    Public Property City As String
    Public Property State As String
    Public Property Zip As String
End Class
Public Class ServiceLine
    Public Property ServiceCode As String
    Public Property BilledAmount As String = "$" & BilledAmount
    Public Property AllowedAmount As String
    Public Property AdjustmentReasonCode As String
    Public Property AdjustmentAmount As String
    Public Property Deduct As String
    Public Property Copy As String
    Public Property COSIdentifierCode As String
    Public Property Coins As String
    Public Property PaidAmount As String
    Public Property Remarks As String

    Public Sub New()
        ' Constructor
    End Sub
End Class
Public Class FileTypeDetector
    Public Function DetectFileType(content As String) As String


        If Is835File(content) Then
            Return "835"
        ElseIf Is837File(content) Then
            Return "837"
        Else
            Return "Unknown File Type"
        End If
    End Function

    Private Function IsValidFilePath(filePath As String) As Boolean
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

    Private Function Is835File(content As String) As Boolean
        ' Check for specific segment that identifies an 835 file
        ' Typically, an 835 file contains segments like "CLP" and "TRN"
        Return content.Contains("CLP*") AndAlso content.Contains("TRN*")
    End Function

    Private Function Is837File(content As String) As Boolean
        ' Check for specific segment that identifies an 837 file
        ' Typically, an 837 file contains segments like "CLM" and "NM1"
        Return content.Contains("CLM*") AndAlso content.Contains("NM1*")
    End Function
End Class

Class HTMl_Temp
    Public Shared _835 As String = "<!DOCTYPE html>" & vbCrLf &
"<html lang='en'>" & vbCrLf &
"<head>" & vbCrLf &
"    <meta charset='UTF-8'>" & vbCrLf &
"    <meta name='viewport' content='width=device-width, initial-scale=1.0'>" & vbCrLf &
"    <title>ERA Claim Details</title>" & vbCrLf &
"    <style>" & vbCrLf &
"        body {" & vbCrLf &
"            font-family: Arial, sans-serif;" & vbCrLf &
"        }" & vbCrLf &
"        .justify {" & vbCrLf &
"            text-align: justify;" & vbCrLf &
"        }" & vbCrLf &
"        table {" & vbCrLf &
"            font-size: 14px;" & vbCrLf &
"            width: 100%;" & vbCrLf &
"            border-collapse: collapse;" & vbCrLf &
"        }" & vbCrLf &
"        th, td {" & vbCrLf &
"            border: 1px solid gray;" & vbCrLf &
"            padding: 8px;" & vbCrLf &
"            text-align: center;" & vbCrLf &
"        }" & vbCrLf &
"        th {" & vbCrLf &
"            background-color: #f2f2f2;" & vbCrLf &
"        }" & vbCrLf &
"        .no-border {" & vbCrLf &
"            border: none !important;" & vbCrLf &
"        }" & vbCrLf &
"        @media print {" & vbCrLf &
"            body {" & vbCrLf &
"                margin: 0;" & vbCrLf &
"                padding: 0;" & vbCrLf &
"            }" & vbCrLf &
"            table {" & vbCrLf &
"                width: 100%;" & vbCrLf &
"                font-size: 12px;" & vbCrLf &
"            }" & vbCrLf &
"            th, td {" & vbCrLf &
"                padding: 6px;" & vbCrLf &
"            }" & vbCrLf &
"            .printable_text {" & vbCrLf &
"                width: 100%;" & vbCrLf &
"            }" & vbCrLf &
"            .no-print {" & vbCrLf &
"                display: none;" & vbCrLf &
"            }" & vbCrLf &
"            .page-break {" & vbCrLf &
"                page-break-before: always;" & vbCrLf &
"            }" & vbCrLf &
"        }" & vbCrLf &
"    </style>" & vbCrLf &
"</head>" & vbCrLf &
"<body>" & vbCrLf &
"    <h2>ERA Claim Details</h2>" & vbCrLf &
"    $CLAIM_DETAILS$" & vbCrLf &
"    <table class='printable_text' align='center'>" & vbCrLf &
"        <tbody>" & vbCrLf &
"            <tr class='no-border'>" & vbCrLf &
"                <td colspan='10' class='no-border'>" & vbCrLf &
"                    <hr>" & vbCrLf &
"                </td>" & vbCrLf &
"            </tr>" & vbCrLf &
"            <tr class='no-border'>" & vbCrLf &
"                <td class='no-border' style='text-align: center;'>" & vbCrLf &
"                    Rejection Reason and Adjustment Codes" & vbCrLf &
"                </td>" & vbCrLf &
"            </tr>" & vbCrLf &
"            <tr class='no-border'>" & vbCrLf &
"                <td colspan='10' class='no-border'>" & vbCrLf &
"                    <hr>" & vbCrLf &
"                </td>" & vbCrLf &
"            </tr>$$$" & vbCrLf &
"        </tbody>" & vbCrLf &
"    </table>" & vbCrLf &
"</body>" & vbCrLf &
"</html>"

End Class